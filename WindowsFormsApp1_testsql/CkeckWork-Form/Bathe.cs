using CrystalDecisions.ReportAppServer.DataDefModel; // ใช้สำหรับ Crystal Reports (ไม่มีการใช้งานจริงในโค้ดนี้)
using JPM_SilverTrade.Class; // คลาสที่เกี่ยวข้องกับการใช้งานของโปรเจกต์ (อาจเป็นฟังก์ชันสนับสนุน)
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp1_testsql.Class; // คลาสที่เกี่ยวข้อง เช่น การเชื่อมต่อฐานข้อมูล

namespace WindowsFormsApp1_testsql.CkeckWork_Form
{
    public partial class Bathe : Form
    {
        // ตัวแปรที่ใช้เก็บค่าที่ได้จากการดึงข้อมูลจากฐานข้อมูล
        private int day;          // จำนวนวันที่กำหนด
        private double silver;    // อัตราเงินเงิน (Silver Rate)
        private double pc;        // เปอร์เซ็นต์ของวัตถุดิบ
        private int minQty;       // จำนวนขั้นต่ำที่กำหนด

        public Bathe()
        {
            InitializeComponent(); // เรียกใช้ฟังก์ชันเริ่มต้นของฟอร์ม
            try
            {
                GetData(); // ดึงข้อมูลจากฐานข้อมูลเพื่อกำหนดค่าตัวแปร
            }
            catch (Exception ex)
            {
                // แสดงข้อความแจ้งข้อผิดพลาดหากไม่สามารถดึงข้อมูลได้
                MessageBox.Show($"เกิดข้อผิดพลาดในการเริ่มต้นข้อมูล: {ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetData()
        {
            // คำสั่ง SQL สำหรับดึงข้อมูลล่าสุดจากฐานข้อมูล
            string query = "SELECT TOP 1 Bathe, SilverRate, minQty, PercentMat FROM SetDateAndSilver ORDER BY cDate DESC;";
            DatabaseConnections db = new DatabaseConnections(1); // สร้างการเชื่อมต่อฐานข้อมูล (Mode 1)

            try
            {
                DataTable dt = db.ExecuteQuery(query); // ดึงข้อมูลจากฐานข้อมูล

                if (dt.Rows.Count > 0)
                {
                    // หากมีข้อมูลในฐานข้อมูล ให้เก็บค่าที่ได้ลงในตัวแปร
                    day = Convert.ToInt32(dt.Rows[0]["Bathe"]);
                    silver = Convert.ToDouble(dt.Rows[0]["SilverRate"]);
                    minQty = Convert.ToInt32(dt.Rows[0]["minQty"]);
                    pc = Convert.ToDouble(dt.Rows[0]["PercentMat"]) / 100;
                }
                else
                {
                    // หากไม่มีข้อมูลในฐานข้อมูล ให้ตั้งค่าดีฟอลต์
                    SetDefaultValues();
                }
            }
            catch (Exception ex)
            {
                // หากเกิดข้อผิดพลาด ให้ตั้งค่าดีฟอลต์และแจ้งผู้ใช้
                SetDefaultValues();
                MessageBox.Show($"เกิดข้อผิดพลาดในการดึงข้อมูล: {ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SetDefaultValues()
        {
            // ตั้งค่าดีฟอลต์ให้กับตัวแปรในกรณีที่ไม่มีข้อมูล
            day = 10;
            silver = 33.5;
            minQty = 30;
            pc = 0.2;
        }

        private void txtInv_KeyDown(object sender, KeyEventArgs e)
        {
            // ตรวจสอบหากผู้ใช้กดปุ่ม Enter ใน TextBox (txtInv)
            if (e.KeyCode == Keys.Enter)
            {
                btnPreview_Click(sender, e); // เรียกใช้งานฟังก์ชัน btnPreview_Click
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่าผู้ใช้ได้กรอกค่าใน TextBox หรือไม่
            if (string.IsNullOrWhiteSpace(txtInv.Text))
            {
                MessageBox.Show("กรุณากรอกเลขที่ IV.", "ข้อผิดพลาดในการตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // หยุดการทำงานหากไม่ได้กรอกข้อมูล
            }

            try
            {
                DatabaseConnections db = new DatabaseConnections(2); // สร้างการเชื่อมต่อฐานข้อมูล (Mode 2)
                SqlParameter[] parameters =
                {
                    new SqlParameter("@Day", day),        // ส่งค่าจำนวนวันที่กำหนด
                    new SqlParameter("@Inv", txtInv.Text), // ส่งค่าเลขที่ IV จาก TextBox
                    new SqlParameter("@Silver", silver),  // ส่งค่าอัตราเงินเงิน
                    new SqlParameter("@minQty", minQty),  // ส่งค่าจำนวนขั้นต่ำ
                    new SqlParameter("@PC", pc)          // ส่งค่าคิดเปอร์เซ็นต์
                };

                // เรียกใช้ Store Procedure เพื่อดึงข้อมูลที่ตรงกับเงื่อนไข
                DataTable dt = db.ExecuteQuery("sp_Find_Inv_Bathe", parameters, true);

                if (dt.Rows.Count > 0)
                {
                    // หากพบข้อมูล ให้สร้างรายงานด้วย Crystal Reports
                    ReportGenerator report = new ReportGenerator();
                    report.ShowReport("BatheReport3.rpt", dt);
                }
                else
                {
                    // หากไม่พบข้อมูล ให้แจ้งผู้ใช้
                    MessageBox.Show("ไม่พบข้อมูล IV ที่ท่านกรอก", "ไม่พบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // หากเกิดข้อผิดพลาดในการสร้างรายงาน ให้แสดงข้อความแจ้ง
                MessageBox.Show($"เกิดข้อผิดพลาดในการสร้างรายงาน: {ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
