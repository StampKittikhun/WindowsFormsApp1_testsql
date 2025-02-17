using JPM_SilverTrade.Class; // ใช้สำหรับอ้างอิงคลาสที่เกี่ยวข้องจากโปรเจกต์ JPM_SilverTrade
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient; // ใช้สำหรับการเชื่อมต่อและจัดการฐานข้อมูล SQL Server
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; // ใช้สำหรับสร้างฟอร์ม Windows Forms Application
using WindowsFormsApp1_testsql.Class; // ใช้สำหรับอ้างอิงคลาสที่เกี่ยวข้องจากโปรเจกต์ WindowsFormsApp1_testsql

namespace WindowsFormsApp1_testsql.CkeckWork_Form
{
    public partial class Bury : Form
    {
        // ตัวแปรสำหรับเก็บจำนวนวันและจำนวนขั้นต่ำ
        private int day, minQty;

        // เมธอดคอนสตรัคเตอร์สำหรับเริ่มต้นฟอร์ม
        public Bury()
        {
            InitializeComponent(); // เรียกใช้เมธอดสำหรับเริ่มต้นการตั้งค่าของฟอร์ม
            try
            {
                GetData(); // เรียกใช้เมธอดเพื่อดึงข้อมูลที่จำเป็น
            }
            catch (Exception ex)
            {
                // แสดงข้อความแจ้งเตือนหากเกิดข้อผิดพลาด
                MessageBox.Show($"เกิดข้อผิดพลาดในการเริ่มต้นฟอร์ม: {ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetDefaultValues(); // ตั้งค่าดีฟอลต์ให้กับตัวแปรในกรณีเกิดข้อผิดพลาด
            }
        }

        // เมธอดสำหรับดึงข้อมูลจากฐานข้อมูล
        private void GetData()
        {
            try
            {
                // คำสั่ง SQL เพื่อดึงข้อมูล Bury และ minQty
                string query = "SELECT TOP 1 Bury, minQty FROM SetDateAndSilver ORDER BY cDate DESC;";
                DatabaseConnections db = new DatabaseConnections(1); // สร้างอ็อบเจกต์เชื่อมต่อฐานข้อมูล
                DataTable dt = db.ExecuteQuery(query); // เรียกใช้คำสั่ง SQL และเก็บผลลัพธ์ใน DataTable

                if (dt.Rows.Count > 0) // ตรวจสอบว่ามีข้อมูลที่ดึงมาหรือไม่
                {
                    // กำหนดค่าจากฐานข้อมูลให้กับตัวแปร
                    day = Convert.ToInt32(dt.Rows[0]["Bury"]);
                    minQty = Convert.ToInt32(dt.Rows[0]["minQty"]);
                }
                else
                {
                    SetDefaultValues(); // ตั้งค่าดีฟอลต์หากไม่มีข้อมูลในฐานข้อมูล
                }
            }
            catch (Exception ex)
            {
                // ตั้งค่าดีฟอลต์และแสดงข้อความแจ้งเตือนหากเกิดข้อผิดพลาด
                SetDefaultValues();
                MessageBox.Show($"เกิดข้อผิดพลาดในการดึงข้อมูล: {ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // เมธอดสำหรับตั้งค่าดีฟอลต์
        private void SetDefaultValues()
        {
            day = 10; // กำหนดค่าดีฟอลต์สำหรับจำนวนวัน
            minQty = 30; // กำหนดค่าดีฟอลต์สำหรับจำนวนขั้นต่ำ
        }

        // เมธอด Event Handler เมื่อกดปุ่ม Preview
        private void btnPreview_Click(object sender, EventArgs e)
        {
            PreviewReport(); // เรียกใช้เมธอดสำหรับประมวลผลและแสดงรายงาน
        }

        // เมธอด Event Handler เมื่อกดปุ่ม Enter ใน TextBox
        private void txtInv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // ตรวจสอบว่าปุ่มที่กดคือ Enter หรือไม่
            {
                PreviewReport(); // เรียกใช้เมธอดสำหรับประมวลผลและแสดงรายงาน
            }
        }

        // เมธอดสำหรับประมวลผลและแสดงรายงาน
        private void PreviewReport()
        {
            if (string.IsNullOrWhiteSpace(txtInv.Text)) // ตรวจสอบว่าข้อมูลใน TextBox ว่างหรือมีแค่ช่องว่างหรือไม่
            {
                MessageBox.Show("กรุณากรอกเลขที่ IV."); // แจ้งเตือนให้กรอกข้อมูล
                return; // หยุดการทำงานของเมธอด
            }

            try
            {
                DatabaseConnections db = new DatabaseConnections(2); // สร้างอ็อบเจกต์เชื่อมต่อฐานข้อมูล
                SqlParameter[] param = new SqlParameter[] // กำหนดพารามิเตอร์สำหรับส่งไปยังคำสั่ง SQL
                {
                    new SqlParameter("@Day", day),
                    new SqlParameter("@Inv", txtInv.Text),
                    new SqlParameter("@minQty", minQty)
                };

                // เรียกใช้คำสั่ง SQL แบบ Stored Procedure และเก็บผลลัพธ์ใน DataTable
                DataTable dt = db.ExecuteQuery("sp_Find_Inv_Bury", param, true);

                if (dt.Rows.Count > 0) // ตรวจสอบว่ามีข้อมูลที่ดึงมาหรือไม่
                {
                    ReportGenerator report = new ReportGenerator(); // สร้างอ็อบเจกต์สำหรับการสร้างรายงาน
                    report.ShowReport("BuryReport3.rpt", dt); // แสดงรายงานโดยใช้ข้อมูลใน DataTable
                }
                else
                {
                    // แจ้งเตือนหากไม่พบข้อมูลตามที่กรอก
                    MessageBox.Show("ไม่พบข้อมูล IV ที่ท่านกรอก", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // แจ้งเตือนหากเกิดข้อผิดพลาดในการสร้างรายงาน
                MessageBox.Show($"เกิดข้อผิดพลาดในการสร้างรายงาน: {ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
