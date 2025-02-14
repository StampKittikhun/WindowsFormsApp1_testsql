using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JPM_SilverTrade.Class
{
    internal class ReportGenerator
    {
        // ✅ เมธอดสำหรับแสดงรายงานแบบไม่ต้องระบุ Subreport (เรียกใช้เมธอด Overload ด้านล่าง)
        public void ShowReport(string reportFileName, DataTable mainDataTable)
        {
            ShowReport(reportFileName, mainDataTable, null);
        }

        // ✅ ตัวแปรสำหรับจัดการรายงานหลัก
        ReportDocument cReport;

        // ✅ Property สำหรับสร้างชื่อไฟล์ .xsd โดยแปลงจากชื่อไฟล์ .rpt
        protected string xsdFile
        {
            get
            {
                // แปลงจากไฟล์ .rpt เป็น .xsd
                var s = cReport.FileName.Replace(".rpt", ".xsd");

                // ลบ rassdk:// ออก เพื่อให้พาธถูกต้อง
                return s.Replace("rassdk://", "");
            }
        }

        // ✅ เมธอดหลักสำหรับแสดงรายงาน Crystal Reports
        public void ShowReport(string reportFileName, DataTable mainDataTable, Dictionary<string, DataTable> subReportsData)
        {
            try
            {
                // 1️⃣ กำหนดพาธไฟล์รายงาน (Network Path)
                string reportPath = $"{Application.StartupPath}\\Reports\\" + reportFileName;

                // 2️⃣ สร้างออบเจ็กต์ ReportDocument สำหรับจัดการรายงาน
                cReport = new ReportDocument();

                // 3️⃣ สร้าง DataSet เพื่อเก็บข้อมูลจาก DataTable
                DataSet ds = new DataSet();

                // 4️⃣ ตรวจสอบว่ามี DataTable ชื่อเดียวกันอยู่แล้วหรือไม่
                if (!ds.Tables.Contains(mainDataTable.TableName))
                    ds.Tables.Add(mainDataTable.Copy()); // คัดลอก DataTable ลง DataSet

                // 5️⃣ โหลดไฟล์รายงาน Crystal Reports จากพาธที่กำหนด
                cReport.Load(reportPath);

                // 6️⃣ สร้างไฟล์ schema (.xsd) เพื่อกำหนดโครงสร้างข้อมูล
                ds.WriteXmlSchema(xsdFile);

                // 7️⃣ ตั้งค่า DataSource ให้กับรายงานหลัก
                cReport.SetDataSource(mainDataTable);

                // 8️⃣ ตั้งค่า DataSource ให้กับ Subreports (ถ้ามี)
                foreach (ReportDocument subReport in cReport.Subreports)
                {
                    // ใช้ข้อมูลจาก mainDataTable สำหรับ subreport ด้วย
                    subReport.SetDataSource(mainDataTable);
                }

                // 9️⃣ สร้าง Form เพื่อแสดงรายงาน
                Form reportForm = new Form();

                // 🔟 เพิ่ม CrystalReportViewer ลงใน Form
                CrystalReportViewer crystalReportViewer = new CrystalReportViewer
                {
                    Dock = DockStyle.Fill, // แสดงเต็มหน้าจอ
                    ReportSource = cReport // ระบุแหล่งที่มาของรายงาน
                };

                // 1️⃣1️⃣ เพิ่ม Control ลงใน Form
                reportForm.Controls.Add(crystalReportViewer);

                // 1️⃣2️⃣ กำหนดให้แสดง Form แบบเต็มหน้าจอ
                reportForm.WindowState = FormWindowState.Maximized;

                // 1️⃣3️⃣ แสดง Form
                reportForm.Show();

                // 1️⃣4️⃣ ลบไฟล์ schema (.xsd) ที่สร้างขึ้น
                System.IO.File.Delete(xsdFile);
            }
            catch (Exception ex)
            {
                // 🛑 แสดงข้อความเมื่อเกิดข้อผิดพลาด
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        // ✅ เมธอดสำหรับกำหนดข้อมูลการเชื่อมต่อฐานข้อมูลให้กับรายงาน
        private void SetDbLogonForReport(ConnectionInfo connectionInfo, ReportDocument reportDocument)
        {
            // วนลูปผ่านตารางทั้งหมดในรายงาน
            foreach (Table table in reportDocument.Database.Tables)
            {
                // สร้างออบเจ็กต์ TableLogOnInfo
                TableLogOnInfo tableLogOnInfo = table.LogOnInfo;

                // กำหนด ConnectionInfo ที่ได้รับมาให้กับตาราง
                tableLogOnInfo.ConnectionInfo = connectionInfo;

                // นำค่าการตั้งค่าไปใช้กับตาราง
                table.ApplyLogOnInfo(tableLogOnInfo);

                // 📌 ไม่จำเป็นต้องกำหนด Table.Location ใหม่ (แต่ใส่ไว้ตาม Logic เดิม)
                table.Location = table.Location;
            }
        }
    }
}
