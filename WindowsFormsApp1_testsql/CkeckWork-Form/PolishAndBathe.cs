using JPM_SilverTrade.Class;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp1_testsql.Class;

namespace WindowsFormsApp1_testsql.CkeckWork_Form
{
    public partial class PolishAndBathe : Form
    {
        // ตัวแปรสำหรับเก็บค่าพารามิเตอร์ที่ใช้ในรายงาน
        int day, minQty;
        double silver, pc;

        // ฟังก์ชันที่ทำงานเมื่อผู้ใช้คลิกที่ปุ่ม Preview
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
                    new SqlParameter("@Day", day), // จำนวนวันที่ดึงจากฐานข้อมูล
                    new SqlParameter("@Inv", txtInv.Text), // หมายเลข IV ที่ผู้ใช้กรอก
                    new SqlParameter("@Silver", silver), // ค่าราคาสีเงินจากฐานข้อมูล
                    new SqlParameter("@minQty", minQty), // จำนวนขั้นต่ำที่ดึงจากฐานข้อมูล
                    new SqlParameter("@PC", pc) // ค่าร้อยละที่ดึงจากฐานข้อมูล
                };
                DataTable dt = db.ExecuteQuery("sp_Find_Inv_Polish_And_Bathe", param, true); // เรียก Stored Procedure และเก็บผลลัพธ์

                // ตรวจสอบว่าพบข้อมูลหรือไม่
                if (dt.Rows.Count > 0)
                {
                    // แสดงรายงานโดยใช้ข้อมูลที่ดึงมา
                    ReportGenerator report = new ReportGenerator();
                    report.ShowReport("PolishAndBatheReport2.rpt", dt); // ใช้ ReportGenerator แสดงรายงาน
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

        // คอนสตรัคเตอร์ของฟอร์ม
        public PolishAndBathe()
        {
            InitializeComponent(); // เริ่มต้นค่าควบคุมในฟอร์ม
            GetData(); // เรียกข้อมูลเริ่มต้นจากฐานข้อมูล
        }

        // ฟังก์ชันสำหรับดึงข้อมูลเริ่มต้นจากฐานข้อมูล
        private void GetData()
        {
            // ดึงค่าพารามิเตอร์เริ่มต้นจากฐานข้อมูล
            string query = "select top 1 PolishAndBathe, SilverRate, minQty, PercentMat from SetDateAndSilver order by cDate desc;";
            DatabaseConnections db = new DatabaseConnections(1);
            DataTable dt = db.ExecuteQuery(query); // เรียกข้อมูลจากฐานข้อมูล

            if (dt.Rows.Count > 0) // ตรวจสอบว่าพบข้อมูลหรือไม่
            {
                // กำหนดค่าให้กับตัวแปรตามที่ดึงจากฐานข้อมูล
                day = Convert.ToInt32(dt.Rows[0]["PolishAndBathe"]);
                silver = Convert.ToDouble(dt.Rows[0]["SilverRate"]);
                minQty = Convert.ToInt32(dt.Rows[0]["minQty"]);
                pc = Convert.ToDouble(dt.Rows[0]["PercentMat"]) / 100;
            }
            else
            {
                // กำหนดค่าดีฟอลต์ในกรณีไม่พบข้อมูล
                day = 10;
                silver = 33.5;
                minQty = 30;
                pc = 0.2;
            }
        }

    }
}
