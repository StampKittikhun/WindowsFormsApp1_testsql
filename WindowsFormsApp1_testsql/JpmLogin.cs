using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1_testsql.Class;



//หน้าจอ Login 
namespace WindowsFormsApp1_testsql
{
    public partial class JpmLogin : Form
    {
        public JpmLogin()
        {
            InitializeComponent();

        }
        
        private bool VerifyLogin(string username, string password) 
        {
            try
            {
                string sqlQuery = @"
                    SELECT COUNT(*) FROM [PrincessData].[dbo].[Userid] 
                    WHERE USERID = @Username AND PASSWORD = @Password";

                DatabaseConnections db = new DatabaseConnections("Princess");
                SqlParameter[] parameters = {
                    new SqlParameter("@Username", username),
                    new SqlParameter("@Password", password)
                };

                int result = Convert.ToInt32(db.ExecuteScalar(sqlQuery, parameters));
                return result > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = texPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("กรุณากรอกชื่อผู้ใช้และรหัสผ่าน", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (VerifyLogin(username, password))
            {
                string sqlQuery = @"
                    SELECT USERID, USERIDGROUP, DESCRIPTION 
                    FROM [PrincessData].[dbo].[Userid] 
                    WHERE USERID = @Username";

                DatabaseConnections db = new DatabaseConnections("Princess");
                SqlParameter[] parameters = {
                    new SqlParameter("@Username", username)
                };

                DataTable dt = db.ExecuteQuery(sqlQuery, parameters);
                if (dt.Rows.Count > 0)
                {
                    UserSession.Username = dt.Rows[0]["USERID"].ToString();
                    UserSession.UserGroup = dt.Rows[0]["USERIDGROUP"].ToString();
                    UserSession.Description = dt.Rows[0]["DESCRIPTION"].ToString();

                    // เปิดหน้าหลัก
                    this.Hide();
                    MainMenuForm mainMenu = new MainMenuForm();
                    mainMenu.FormClosed += (s, args) => this.Show();
                    mainMenu.Show();
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูลผู้ใช้", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("ชื่อผู้ใช้/รหัสผ่านไม่ถูกต้อง", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                texPassword.Focus();
            }
        }

        private void texPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(null, null);
            }
        }
    }
}
