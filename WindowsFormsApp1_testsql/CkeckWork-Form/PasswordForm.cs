using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1_testsql.Class;

namespace WindowsFormsApp1_testsql.CkeckWork_Form
{
    public partial class PasswordForm : Form
    {
        public PasswordForm()
        {
            InitializeComponent();
        }

        private bool VeriflyPassword(string inputPassword)
        {
            string sqlQeury = "select Password from PasswordForEdit where Password = @Password and Department = 'Acountant'";
            DatabaseConnections db = new DatabaseConnections(1);
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Password", inputPassword)
            };
            object result = db.ExecuteScalar(sqlQeury, parameters);
            if (result != null)
            {
                string password = result.ToString();
                return password == inputPassword;
            }

            return false;
        }
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (VeriflyPassword(txtPassword.Text.Trim()))
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Password is incorrect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            if (VeriflyPassword(txtPassword.Text))
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Password is incorrect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
