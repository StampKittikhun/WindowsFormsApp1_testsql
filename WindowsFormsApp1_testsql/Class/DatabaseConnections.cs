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
        private string connectionString;

        public DatabaseConnections()
        {
            // ใส่ข้อมูลเซิร์ฟเวอร์ของคุณที่นี่
            connectionString = "Data Source=192.168.2.13;Initial Catalog=PrincessData;Persist Security Info=True;User ID=sa;Password=Bestszaza369;Encrypt=True;TrustServerCertificate=True";
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        // 1. ฟังก์ชัน ExecuteQuery → ใช้ดึงข้อมูลจาก SQL Server
        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null , bool isProcedure = false)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand comm = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                        comm.Parameters.AddRange(parameters);

                    DataTable dt = new DataTable();
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred: " + ex.Message);
                    }
                    return dt;
                }
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
