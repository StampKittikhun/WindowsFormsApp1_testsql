using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JPM_Accountant.Class
{
    internal class Center
    {
        //  ฟังก์ชันคืนค่า Connection String สำหรับเชื่อมต่อกับฐานข้อมูล SQL Server
        private static string GetConnectionString()
        {
            string conString = "Data Source=SERVER;Initial Catalog=PrincessData;Persist Security Info=True;User ID=admin;Password=jp;Encrypt=True;TrustServerCertificate=True";
            return conString;
        }

        //  ตัวแปรสำหรับเชื่อมต่อฐานข้อมูล SQL Server
        private static SqlConnection con = new SqlConnection();

        //  ตัวแปรสำหรับดึงข้อมูลจากฐานข้อมูลมาใส่ DataTable
        private static SqlDataAdapter da;

        //  ตัวแปรใช้สำหรับผูกข้อมูล (Binding) กับ UI เช่น DataGridView
        private static BindingSource bs;

        //  ตัวแปรเก็บคำสั่ง SQL ที่ต้องการ Execute
        private static string sql;

        //  ตัวแปรสำหรับเก็บค่าพารามิเตอร์ของ SQL Command
        private static SqlParameter SqlParameter;

        //  คำสั่ง SQL ที่ใช้ในการ Execute คำสั่งต่าง ๆ
        public static SqlCommand cmd = new SqlCommand("", con);

        //  ใช้เก็บข้อมูลหลาย DataTable (DataSet)
        public static DataSet ds;

        //  ใช้เก็บข้อมูลที่ดึงจากฐานข้อมูล (DataTable)
        public static DataTable dt;

        //  ใช้สำหรับอ่านข้อมูลจากฐานข้อมูลแบบทีละแถว
        public static SqlDataReader dr;

        //  ฟังก์ชันเปิดการเชื่อมต่อฐานข้อมูล
        private static void openConnection()
        {
            if (con.State != ConnectionState.Open) // เช็คว่ายังไม่มีการเชื่อมต่ออยู่
            {
                con.ConnectionString = GetConnectionString(); // ตั้งค่า Connection String
                con.Open(); // เปิดการเชื่อมต่อ
            }
        }

        //  ฟังก์ชันปิดการเชื่อมต่อฐานข้อมูล
        private static void closeConnection()
        {
            if (con.State != ConnectionState.Closed) { con.Close(); } // ปิดการเชื่อมต่อถ้ายังเปิดอยู่
        }

        //  ฟังก์ชันใช้โหลดข้อมูลจากฐานข้อมูลและคืนค่าเป็น DataTable
        public static DataTable Load()
        {
            openConnection(); // เปิดการเชื่อมต่อ

            // ตั้งค่า SqlCommand ใหม่จากคำสั่งปัจจุบัน
            cmd = new SqlCommand(cmd.CommandText, con);

            // ใช้ SqlDataAdapter เพื่อดึงข้อมูลจาก SQL Server
            da = new SqlDataAdapter(cmd);

            // สร้าง DataTable สำหรับเก็บข้อมูล
            dt = new DataTable();
            da.Fill(dt); // ดึงข้อมูลจากฐานข้อมูลลง DataTable

            closeConnection(); // ปิดการเชื่อมต่อ
            return dt; // คืนค่า DataTable
        }

        //  ฟังก์ชันใช้ Execute คำสั่ง SQL และคืนค่าเป็น DataTable
        public static DataTable Execute(string CommandText)
        {
            try
            {
                openConnection(); // เปิดการเชื่อมต่อ

                // ใช้ SqlDataAdapter สำหรับดึงข้อมูล
                da = new SqlDataAdapter(cmd);

                // สร้าง DataTable ใหม่เพื่อเก็บข้อมูล
                dt = new DataTable();
                da.Fill(dt); // เติมข้อมูลลง DataTable

                closeConnection(); // ปิดการเชื่อมต่อ
            }
            catch (System.Exception)
            {
                throw; // ถ้าเกิดข้อผิดพลาด ให้โยน Exception ออกไป
            }
            finally
            {
                cmd.Parameters.Clear(); // ล้างพารามิเตอร์ใน Command หลังจาก Execute
            }
            return dt; // คืนค่า DataTable
        }

        //  ฟังก์ชันคืนค่า DataTable ล่าสุดที่ถูกโหลด
        public static DataTable CurrentDataTable()
        {
            return dt;
        }

        //  ฟังก์ชันใช้เพิ่มพารามิเตอร์ให้กับ SQL Command
        public static void AddParameter(IDbCommand cmd, string Name, object value)
        {
            // สร้าง SqlParameter และกำหนดค่าให้กับพารามิเตอร์
            DbParameter result = new SqlParameter(Name, value.GetType());

            IDataParameter dataParameter = result;
            dataParameter.Value = value;

            // เพิ่มพารามิเตอร์เข้าไปใน SqlCommand
            cmd.Parameters.Add(dataParameter);
        }
    }
}
