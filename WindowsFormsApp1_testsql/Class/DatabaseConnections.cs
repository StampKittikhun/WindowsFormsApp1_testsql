using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1_testsql.Class
{
    internal class DatabaseConnections
    {
        private string ConnectionString;

        public DatabaseConnections(int type)
        {
            if (type == 1)
            {
                this.ConnectionString = "Data Source=server;Initial Catalog=Best_ManageDB;Persist Security Info=True;User ID=admin; Password=jp";
            }
            else if (type == 2)
            {
                this.ConnectionString = "Data Source=server;Initial Catalog=PrincessData;Persist Security Info=True;User ID=admin; Password=jp";
            }
            else if (type == 3)
            {
                this.ConnectionString = "Data Source=LAPTOP-U1Q1F7B0\\MSSQLSERVE2022;Initial Catalog = TempData; Persist Security Info=True;User ID = admin; Password=Bestszaza123";
            }
            else if (type == 4)
            {
                this.ConnectionString = "Data Source=LAPTOP-U1Q1F7B0\\MSSQLSERVE2022;Initial Catalog = Best_ManageDB; Persist Security Info=True;User ID = admin; Password=Bestszaza123";
            }
           
        }

        public DatabaseConnections(string type) 
        {
             if (type == "Princess")
                // ใส่ข้อมูลเซิร์ฟเวอร์ของคุณที่นี่
                this.ConnectionString = "Data Source=192.168.2.13;Initial Catalog=PrincessData;Persist Security Info=True;User ID=sa;Password=Bestszaza369;Encrypt=True;TrustServerCertificate=True";
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        // 1. ฟังก์ชัน ExecuteQuery → ใช้ดึงข้อมูลจาก SQL Server
        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null , bool isProcedure = false)
        {
            using (SqlConnection conn = GetConnection())
            {
                SqlCommand comm = new SqlCommand(query, conn)
                {
                    CommandTimeout = 500
                };

                if (isProcedure)
                    comm.CommandType = CommandType.StoredProcedure; // ตั้งค่า CommandType หากเป็น Stored Procedure

                if (parameters != null)
                    comm.Parameters.AddRange(parameters);

                DataTable dt = new DataTable();
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        dt.Load(reader); // โหลดข้อมูลจาก DataReader ไปยัง DataTable
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred: " + ex.Message);
                }
                return dt;
            }
        }
        // 2. ฟังก์ชัน ExecuteNonQuery → ใช้สำหรับ INSERT, UPDATE, DELETE
        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand comm = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                        comm.Parameters.AddRange(parameters);

                    int rowsAffected = 0;
                    try
                    {
                        conn.Open();
                        rowsAffected = comm.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred: " + ex.Message);
                    }
                    return rowsAffected;
                }
            }
        }
        //3. ฟังก์ชัน ExecuteScalar → ใช้สำหรับคำสั่งที่คืนค่าเดียว เช่น COUNT, SUM, MAX, MIN
        public object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand comm = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                        comm.Parameters.AddRange(parameters);

                    object result = null;
                    try
                    {
                        conn.Open();
                        result = comm.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred: " + ex.Message);
                    }
                    return result;
                }
      
            }
        }
    }
}
