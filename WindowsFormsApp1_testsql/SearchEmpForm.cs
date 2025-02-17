using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp1_testsql.Class; // อ้างอิงคลาส DatabaseConnections


//Program ดึงข้อมูลพนักงานมาแสดงที่ช่อง DataGitview
namespace WindowsFormsApp1_testsql
{
    public partial class SearchEmpForm : Form
    {
        private DatabaseConnections dbHelper = new DatabaseConnections("Princess"); // ใช้เชื่อมต่อฐานข้อมูล

        public SearchEmpForm()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.DataSource = null; //  ทำให้ DataGridView ว่างเปล่าเมื่อเปิดฟอร์ม
        }

        private void LoadData(string filter = "")
        {
            try
            {
                // ✅ เพิ่ม ROW_NUMBER(), IDNO, TaxNO, Year1 ในการ Query
                string query = @"
            SELECT 
                ROW_NUMBER() OVER (ORDER BY Name ASC) AS [ลำดับที่], 
                IDNO AS [รหัสพนักงาน], 
                TaxNO AS [เลขประจำตัวผู้เสียภาษี], 
                Titlename AS [คำนำหน้า], 
                Name AS [ชื่อพนักงาน], 
                Year1 AS [ปีที่เข้าทำงาน]
            FROM AcEmployeeProfile";

                if (!string.IsNullOrEmpty(filter))
                {
                    query += " WHERE Name LIKE @filter"; // ใช้ Parameter เพื่อป้องกัน SQL Injection
                }

                SqlParameter[] parameters = { new SqlParameter("@filter", "%" + filter + "%") };
                DataTable dt = dbHelper.ExecuteQuery(query, parameters);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData(); // โหลดข้อมูลเมื่อกดปุ่ม
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1 != null) 
            {
                dataGridView1.DataSource = null;
            }
            else
            {
                dataGridView1 = new DataGridView();
            }
        }
        
    }
}
