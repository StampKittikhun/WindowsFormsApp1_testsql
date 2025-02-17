using JPM_SilverTrade.Class;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp1_testsql.Class;

namespace WindowsFormsApp1_testsql.CkeckWork_Form
{
    public partial class Dress : Form
    {
        // ตัวแปรสำหรับเก็บค่าพารามิเตอร์ที่ใช้ในรายงาน
        int dress, minQty;

        // คอนสตรัคเตอร์ของฟอร์ม
        public Dress()
        {
            InitializeComponent(); // เริ่มต้นค่าควบคุมในฟอร์ม
            GetData(); // เรียกข้อมูลเริ่มต้นจากฐานข้อมูล
        }

        // ฟังก์ชันสำหรับการแสดงรายงานเมื่อผู้ใช้คลิกที่ปุ่ม Preview
        private void btnPreview_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่าผู้ใช้กรอกหมายเลข IV หรือยัง
            if (txtInv.Text == "")
            {
                MessageBox.Show("กรุณากรอกเลขที่ IV."); // แจ้งเตือนให้กรอกข้อมูล
                return; // หยุดการทำงานหากไม่มีข้อมูล
            }
            else
            {
                // เชื่อมต่อฐานข้อมูลเพื่อดึงข้อมูลรายงาน
                DatabaseConnections db = new DatabaseConnections(2);
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@Day", dress), // จำนวนวันที่ดึงจากฐานข้อมูล
                    new SqlParameter("@Inv", txtInv.Text), // หมายเลข IV ที่ผู้ใช้กรอก
                    new SqlParameter("@minQty", minQty) // จำนวนขั้นต่ำที่ดึงจากฐานข้อมูล
                };
                DataTable dt = db.ExecuteQuery("sp_Find_Inv_Dress", param, true); // เรียก Stored Procedure และเก็บผลลัพธ์

                // ตรวจสอบว่าพบข้อมูลหรือไม่
                if (dt.Rows.Count > 0)
                {
                    // แสดงรายงานโดยใช้ข้อมูลที่ดึงมา
                    ReportGenerator report = new ReportGenerator();
                    report.ShowReport("DressReport.rpt", dt); // ใช้ ReportGenerator แสดงรายงาน
                }
                else
                {
                    // แจ้งเตือนว่าหาไม่พบข้อมูลตามที่กรอก
                    MessageBox.Show("ไม่พบข้อมูล IV ที่ท่านกรอก", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ฟังก์ชันที่ตรวจสอบเมื่อผู้ใช้กดปุ่ม Enter ใน TextBox
        private void txtInv_KeyDown(object sender, KeyEventArgs e)
        {
            // หากผู้ใช้กด Enter ให้เรียกฟังก์ชัน btnPreview_Click เพื่อแสดงรายงาน
            if (e.KeyCode == Keys.Enter)
            {
                btnPreview_Click(sender, e); // เรียกฟังก์ชัน btnPreview_Click
            }
        }

        // ฟังก์ชันสำหรับดึงข้อมูลเริ่มต้นจากฐานข้อมูล
        private void GetData()
        {
            // ดึงค่าพารามิเตอร์เริ่มต้นจากฐานข้อมูล
            string query = "select top 1 Dress, minQty from SetDateAndSilver order by cDate desc;";
            DatabaseConnections db = new DatabaseConnections(1);
            DataTable dt = db.ExecuteQuery(query); // เรียกข้อมูลจากฐานข้อมูล

            if (dt.Rows.Count > 0) // ตรวจสอบว่าพบข้อมูลหรือไม่
            {
                // กำหนดค่าให้กับตัวแปรตามที่ดึงจากฐานข้อมูล
                dress = Convert.ToInt32(dt.Rows[0]["Dress"]);
                minQty = Convert.ToInt32(dt.Rows[0]["minQty"]);
            }
            else
            {
                // กำหนดค่าดีฟอลต์ในกรณีไม่พบข้อมูล
                dress = 10;
                minQty = 30;
            }
        }
    }
}
