using JPM_SilverTrade.Class;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp1_testsql.Class;

namespace WindowsFormsApp1_testsql.CkeckWork_Form
{
    public partial class Foundry : Form
    {
        // ตัวแปรที่ใช้เก็บค่าต่าง ๆ ที่จะใช้ในกระบวนการคำนวณและสร้างรายงาน
        private int day, minQty, modelDay, chunkDay, devideQty, deductModel;

        // Constructor: เรียกใช้งานเมื่อฟอร์มถูกสร้างขึ้น
        public Foundry()
        {
            InitializeComponent();
            try
            {
                GetData(); // ดึงข้อมูลเริ่มต้นจากฐานข้อมูล
            }
            catch (Exception ex)
            {
                // แสดงข้อความข้อผิดพลาดหากดึงข้อมูลล้มเหลว
                MessageBox.Show($"เกิดข้อผิดพลาดในการโหลดข้อมูล: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ฟังก์ชันนี้ถูกเรียกเมื่อกดปุ่ม Enter บนปุ่ม Preview
        private void btnPreview_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnPreview_Click(sender, e); // เรียกใช้ฟังก์ชันคลิกปุ่ม Preview
            }
        }

        // ฟังก์ชันที่ทำงานเมื่อคลิกปุ่ม Preview
        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInv.Text)) // ตรวจสอบว่าช่องกรอกข้อมูล IV ไม่ว่าง
            {
                // แสดงข้อความเตือนถ้าไม่ได้กรอกเลข IV
                MessageBox.Show("กรุณากรอกเลขที่ IV.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                GenerateReport(txtInv.Text); // เรียกฟังก์ชันสำหรับสร้างรายงานโดยส่ง IV ที่ผู้ใช้กรอก
            }
            catch (Exception ex)
            {
                // แสดงข้อผิดพลาดถ้ามีปัญหาในการสร้างรายงาน
                MessageBox.Show($"เกิดข้อผิดพลาดในการแสดงรายงาน: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ฟังก์ชันสำหรับดึงข้อมูลเริ่มต้นจากฐานข้อมูล
        private void GetData()
        {
            // คำสั่ง SQL สำหรับดึงข้อมูลล่าสุดจากตาราง SetDateAndSilver
            const string sqlQuery = "SELECT TOP 1 Foundry, minQty, ChunkDay, ModelDay, DevideQty, DeductModel FROM SetDateAndSilver ORDER BY cDate DESC;";
            DatabaseConnections db = new DatabaseConnections(1); // สร้างอ็อบเจ็กต์สำหรับเชื่อมต่อฐานข้อมูล
            DataTable dt = db.ExecuteQuery(sqlQuery); // ดึงข้อมูลในรูปแบบ DataTable

            if (dt.Rows.Count > 0) // ตรวจสอบว่ามีข้อมูลที่ดึงมาได้หรือไม่
            {
                // กำหนดค่าตัวแปรจากข้อมูลในฐานข้อมูล
                day = Convert.ToInt32(dt.Rows[0]["Foundry"]);
                minQty = Convert.ToInt32(dt.Rows[0]["minQty"]);
                modelDay = Convert.ToInt32(dt.Rows[0]["ModelDay"]);
                chunkDay = Convert.ToInt32(dt.Rows[0]["ChunkDay"]);
                devideQty = Convert.ToInt32(dt.Rows[0]["DevideQty"]);
                deductModel = Convert.ToInt32(dt.Rows[0]["DeductModel"]);
            }
            else
            {
                SetDefaultValues(); // ใช้ค่าตั้งต้นถ้าไม่มีข้อมูลในฐานข้อมูล
            }
        }

        // ฟังก์ชันสำหรับกำหนดค่าตั้งต้นในกรณีที่ไม่มีข้อมูลในฐานข้อมูล
        private void SetDefaultValues()
        {
            // กำหนดค่าตัวแปรให้เป็นค่าตั้งต้น
            day = 10;
            minQty = 30;
            modelDay = 23;
            chunkDay = 20;
            devideQty = 300;
            deductModel = 1000;

            // แสดงข้อความแจ้งเตือนว่ากำลังใช้ค่าตั้งต้น
            MessageBox.Show("ไม่พบข้อมูลในฐานข้อมูล จะใช้ค่าตั้งต้นแทน.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ฟังก์ชันสำหรับสร้างรายงาน
        private void GenerateReport(string invNumber)
        {
            // สร้างอ็อบเจ็กต์สำหรับเชื่อมต่อฐานข้อมูล
            DatabaseConnections db = new DatabaseConnections(1);
            // กำหนดพารามิเตอร์สำหรับ Stored Procedure
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Day", day),
                new SqlParameter("@Inv", invNumber),
                new SqlParameter("@minQty", minQty),
                new SqlParameter("@modelDay", modelDay),
                new SqlParameter("@chunkDay", chunkDay),
                new SqlParameter("@devideQty", devideQty),
                new SqlParameter("@DeductModel", deductModel)
            };
            // เรียกใช้ Stored Procedure "sp_Find_Inv_Foundry" พร้อมพารามิเตอร์
            DataTable dt = db.ExecuteQuery("sp_Find_Inv_Foundry", param, true);

            if (dt.Rows.Count > 0) // ตรวจสอบว่ามีข้อมูลใน DataTable
            {
                // สร้างรายงานโดยใช้ข้อมูลที่ดึงมา
                ReportGenerator report = new ReportGenerator();
                report.ShowReport("FoundryReport.rpt", dt);
            }
            else
            {
                // แสดงข้อความข้อผิดพลาดถ้าไม่มีข้อมูลที่ตรงกับ IV ที่กรอก
                MessageBox.Show("ไม่พบข้อมูล IV ที่ท่านกรอก", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
