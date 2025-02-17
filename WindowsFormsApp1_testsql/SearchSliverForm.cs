using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1_testsql.Class;
using static WindowsFormsApp1_testsql.SearchSliverForm;
using JPM_SilverTrade.Class;

namespace WindowsFormsApp1_testsql
{
    public partial class SearchSliverForm : Form
    {
        // ✅ คลาสเก็บข้อมูลพนักงาน
        public class Emp
        {
            public int EmpCode { get; set; }
            public string EmpName { get; set; }
            public string Display
            {

                get
                {
                    return EmpCode + " - " + EmpName;
                }
            }
        }

        public SearchSliverForm()
        {
            InitializeComponent();
            LoadData();
        }

        // ✅ โหลดรายชื่อพนักงานลง `comboBox1`
        private void LoadData()
        {
            string sqlQuery = "select EmpCode, TitleName + ' ' + Name from TEmpProfile where Complete = 1 order by EmpCode asc";
            DatabaseConnections db = new DatabaseConnections("Princess");
            DataTable dt = db.ExecuteQuery(sqlQuery);
            if (dt.Rows.Count > 0)
            {
                List<Emp> emps = new List<Emp>();
                foreach (DataRow dr in dt.Rows)
                {
                    emps.Add(new Emp
                    {
                        EmpCode = Convert.ToInt32(dr["EmpCode"]),
                        EmpName = dr[1].ToString()
                    });
                }

                comboBox1.DataSource = emps;
                        comboBox1.DisplayMember = "Display"; 
                        comboBox1.ValueMember = "EmpCode";
                        comboBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
                        comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
                    }
                }

        // ✅ เมื่อกดปุ่ม "ค้นหา"
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedValue == null)
                {
                    MessageBox.Show("กรุณาเลือกพนักงานก่อนค้นหา", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = @"
                SELECT JH.DocNo, JH.EmpCode, JH.EmpName, JH.JobName, JH.JobDate, JH.DueDate,
                       JD.CustCode, JD.OrderNo, JD.ListNo, JD.Article, JD.TtQty, 
                       O.OrderDate, CPS.Barcode, CPS.picture, CP.MarkJob, 
                       DATEDIFF(DAY, DATEADD(DAY, 3, JH.DueDate), O.OrderDate) AS Diff_date 
                FROM JobHead JH
                LEFT JOIN JobDetail JD ON JH.DocNo = JD.DocNo AND JH.EmpCode = JD.EmpCode
                LEFT JOIN OrdOrder O ON JD.OrderNo = O.Ordno
                LEFT JOIN CProfile CP ON JD.Article = CP.Article
                LEFT JOIN CPriceSale CPS ON JD.Article = CPS.Article AND JD.Barcode = CPS.Barcode
                WHERE JH.EmpCode = @EmpCode 
                      AND JH.JobNum = 10 
                      AND JH.ChkAccount <> 1 
                      AND JH.ChkAccountNot <> 1
                      AND JH.JobDate BETWEEN @StartDate AND @EndDate
                ORDER BY JH.DocNo ASC;";

                DateTime startDate = dtpStart.Value.Date;
                DateTime endDate = dtpEnd.Value.Date.AddDays(1).AddSeconds(-1);

                DatabaseConnections db = new DatabaseConnections("Princess");
                {
                    SqlParameter[] param = {
                        new SqlParameter("@EmpCode", comboBox1.SelectedValue),
                        new SqlParameter("@StartDate", startDate),
                        new SqlParameter("@EndDate", endDate)
                    };

                    DataTable dt = db.ExecuteQuery(query, param);

                    if (dt.Rows.Count > 0)
                    {
                        ProcessData(dt); // ✅ เรียกใช้ฟังก์ชันประมวลผลข้อมูล
                        dataGridView1.DataSource = dt;
                        FormatGrid();
                    }
                    else
                    {
                        MessageBox.Show("ไม่พบข้อมูลที่ตรงกับเงื่อนไข", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading job data: " + ex.Message);
            }
        }

        // ✅ ฟังก์ชันประมวลผล `MarkJob` และคำนวณ SilverPerUnit / TotalSilver
        private void ProcessData(DataTable dt)
        {
            try
            {
                // เพิ่มคอลัมน์ใหม่
                dt.Columns.Add("SilverPerUnit", typeof(double));
                dt.Columns.Add("TotalSilver", typeof(double));

                Regex regex = new Regex(@"\*?นน\.?\s*เบิก\s*นง\.?\*?(?:\s*\([^)]*\))?\s*[^0-9]*?(\d+\.\d{2})");

                foreach (DataRow dr in dt.Rows)
                {
                    string text = dr["MarkJob"].ToString();
                    Match match = regex.Match(text);

                    if (match.Success && double.TryParse(match.Groups[1].Value, out double silverPerUnit))
                    {
                        dr["SilverPerUnit"] = silverPerUnit;
                        dr["TotalSilver"] = Convert.ToDouble(dr["TtQty"]) * silverPerUnit;
                    }
                    else
                    {
                        dr["SilverPerUnit"] = 0;
                        dr["TotalSilver"] = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing MarkJob data: " + ex.Message);
            }
        }

        // ✅ ปรับแต่ง `DataGridView`
        private void FormatGrid()
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.Columns["picture"].Visible = false;
            dataGridView1.Columns["MarkJob"].Visible = false;

            dataGridView1.Columns["JobName"].HeaderText = "ประเภทงาน";
            dataGridView1.Columns["CustCode"].HeaderText = "รหัสลูกค้า";
            dataGridView1.Columns["OrderNo"].HeaderText = "Order No.";
            dataGridView1.Columns["ListNo"].HeaderText = "ลำดับที่";
            dataGridView1.Columns["Article"].HeaderText = "รหัสชิ้นงาน";
            dataGridView1.Columns["EmpCode"].HeaderText = "รหัสช่าง";
            dataGridView1.Columns["EmpName"].HeaderText = "ชื่อช่าง";
            dataGridView1.Columns["DocNo"].HeaderText = "เลขที่เอกสาร";
            dataGridView1.Columns["Diff_date"].HeaderText = "จำนวนวัน";
            dataGridView1.Columns["OrderDate"].HeaderText = "วันที่ตั๋วจบ";
            dataGridView1.Columns["JobDate"].HeaderText = "วันที่เริ่มงาน";
            dataGridView1.Columns["DueDate"].HeaderText = "วันที่จบงาน";
            dataGridView1.Columns["TtQty"].HeaderText = "จำนวนสั่งทำ";
            dataGridView1.Columns["SilverPerUnit"].HeaderText = "น้ำหนักเงิน/ชิ้น";
            dataGridView1.Columns["TotalSilver"].HeaderText = "น้ำหนักเงินทั้งหมด";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //if (comboBox1.SelectedValue == null)
                //{
                //    MessageBox.Show("กรุณาเลือกพนักงานก่อนค้นหา", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}

                string query = @"
                SELECT JH.DocNo, JH.EmpCode, JH.EmpName, JH.JobName, JH.JobDate, JH.DueDate,
                       JD.CustCode, JD.OrderNo, JD.ListNo, JD.Article, JD.TtQty, 
                       O.OrderDate, CPS.Barcode, CPS.picture, CP.MarkJob, 
                       DATEDIFF(DAY, DATEADD(DAY, 3, JH.DueDate), O.OrderDate) AS Diff_date 
                FROM JobHead JH
                LEFT JOIN JobDetail JD ON JH.DocNo = JD.DocNo AND JH.EmpCode = JD.EmpCode
                LEFT JOIN OrdOrder O ON JD.OrderNo = O.Ordno
                LEFT JOIN CProfile CP ON JD.Article = CP.Article
                LEFT JOIN CPriceSale CPS ON JD.Article = CPS.Article AND JD.Barcode = CPS.Barcode
                WHERE JH.EmpCode = @EmpCode 
                      AND JH.JobNum = 10 
                      AND JH.ChkAccount <> 1 
                      AND JH.ChkAccountNot <> 1
                      AND JH.JobDate BETWEEN @StartDate AND @EndDate
                ORDER BY JH.DocNo ASC;";

                DateTime startDate = dtpStart.Value.Date;
                DateTime endDate = dtpEnd.Value.Date.AddDays(1).AddSeconds(-1);

                DatabaseConnections db = new DatabaseConnections("Princess");
                {
                    SqlParameter[] param = {
                        new SqlParameter("@EmpCode", comboBox1.SelectedValue),
                        new SqlParameter("@StartDate", startDate),
                        new SqlParameter("@EndDate", endDate)
                    };

                    DataTable dt = db.ExecuteQuery(query, param);

                    if (dt.Rows.Count > 0)
                    {
                        ProcessData(dt); // ✅ เรียกใช้ฟังก์ชันประมวลผลข้อมูล
                        ReportGenerator report = new ReportGenerator();
                        report.ShowReport("CrystalReportSliver.rpt", dt);
                    }
                    else
                    {
                        MessageBox.Show("ไม่พบข้อมูลที่ตรงกับเงื่อนไข", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading job data: " + ex.Message);
            }
        }
    }
}
