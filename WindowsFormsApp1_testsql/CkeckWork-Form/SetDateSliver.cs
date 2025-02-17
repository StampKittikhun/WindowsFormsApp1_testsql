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
using WindowsFormsApp1_testsql.CkeckWork_Form; // เปลี่ยนเป็น namespace ที่จริงของ PasswordForm


namespace WindowsFormsApp1_testsql.CkeckWork_Form
{
    public partial class SetDateSliver : Form
    {
        public SetDateSliver()
        {
            InitializeComponent();
            GetData();
        }
        private void GetData()
        {
            string query = "select top 1 Foundry, Dress, Prolish, PolishAndBathe, Bury, Bathe, Complete, SilverRate, minQty, PercentMat, DeductModel, ModelDay, ChunkDay, DevideQty from SetDateAndSilver order by cDate desc;";
            DatabaseConnections db = new DatabaseConnections(1);
            DataTable dt = db.ExecuteQuery(query);
            if (dt.Rows.Count > 0)
            {
                txtDFoundry.Text = dt.Rows[0]["Foundry"].ToString();
                txtDDress.Text = dt.Rows[0]["Dress"].ToString();
                txtDPolish.Text = dt.Rows[0]["Prolish"].ToString();
                txtDPolishAndBathe.Text = dt.Rows[0]["PolishAndBathe"].ToString();
                txtDBury.Text = dt.Rows[0]["Bury"].ToString();
                txtDBathe.Text = dt.Rows[0]["Bathe"].ToString();
                txtDComplete.Text = dt.Rows[0]["Complete"].ToString();
                txtSilverRate.Text = dt.Rows[0]["SilverRate"].ToString();
                txtMinQty.Text = dt.Rows[0]["minQty"].ToString();
                txtPCMat.Text = dt.Rows[0]["PercentMat"].ToString();
                txtDeductModel.Text = dt.Rows[0]["DeductModel"].ToString();
                txtModelDay.Text = dt.Rows[0]["ModelDay"].ToString();
                txtChunkDay.Text = dt.Rows[0]["ChunkDay"].ToString();
                txtDevideQty.Text = dt.Rows[0]["DevideQty"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            PasswordForm passwordForm = new PasswordForm();
            if (passwordForm.ShowDialog() == DialogResult.OK)
            {
                string query = "insert into SetDateAndSilver (Foundry, Dress, Prolish, PolishAndBathe, Bury, Bathe, Complete, SilverRate, minQty, PercentMat, DeductModel, ModelDay, ChunkDay, DevideQty) values (@Foundry, @Dress, @Prolish, @PolishAndBathe, @Bury, @Bathe, @Complete, @SilverRate, @minQty, @PercentMat, @DeductModel, @ModelDay, @ChunkDay, @DevideQty)";
                DatabaseConnections db = new DatabaseConnections(1);
                SqlParameter[] param = new SqlParameter[] {
                    new SqlParameter("@Foundry", Convert.ToInt32(txtDFoundry.Text)),
                    new SqlParameter("@Dress", Convert.ToInt32(txtDDress.Text)),
                    new SqlParameter("@Prolish", Convert.ToInt32(txtDPolish.Text)),
                    new SqlParameter("@PolishAndBathe", Convert.ToInt32(txtDPolishAndBathe.Text)),
                    new SqlParameter("@Bury", Convert.ToInt32(txtDBury.Text)),
                    new SqlParameter("@Bathe", Convert.ToInt32(txtDBathe.Text)),
                    new SqlParameter("@Complete", Convert.ToInt32(txtDComplete.Text)),
                    new SqlParameter("@SilverRate", Convert.ToDouble(txtSilverRate.Text)),
                    new SqlParameter("@minQty", Convert.ToInt32(txtMinQty.Text)),
                    new SqlParameter("@PercentMat", Convert.ToDouble(txtPCMat.Text)),
                    new SqlParameter("@DeductModel", txtDeductModel.Text),
                    new SqlParameter("@ModelDay", Convert.ToInt32(txtModelDay.Text)),
                    new SqlParameter("@ChunkDay", Convert.ToInt32(txtChunkDay.Text)),
                    new SqlParameter("@DevideQty", Convert.ToInt32(txtDevideQty.Text))
                };
                int result = db.ExecuteNonQuery(query, param);
                if (result > 0)
                {
                    MessageBox.Show("บันทึกข้อมูลสำเร็จ");
                }
                else
                {
                    MessageBox.Show("บันทึกข้อมูลไม่สำเร็จ");
                    return;
                }
            }
            else
            {
                MessageBox.Show("รหัสผ่านไม่ถูกต้อง");
            }
        }

        private void txtDDress_Leave(object sender, EventArgs e)
        {
            // ตรวจสอบว่าข้อมูลใน TextBox เป็นตัวเลขหรือไม่
            if (decimal.TryParse(txtDDress.Text, out decimal number))
            {
                // ฟอร์แมตตัวเลขให้มีทศนิยม 2 ตำแหน่ง
                txtDDress.Text = number.ToString();
            }
            else
            {
                // ถ้าผู้ใช้พิมพ์ข้อความที่ไม่ใช่ตัวเลข จะเคลียร์ข้อมูล
                txtDDress.Text = "0";
            }
        }

        private void txtDPolish_Leave(object sender, EventArgs e)
        {
            // ตรวจสอบว่าข้อมูลใน TextBox เป็นตัวเลขหรือไม่
            if (decimal.TryParse(txtDDress.Text, out decimal number))
            {
                // ฟอร์แมตตัวเลขให้มีทศนิยม 2 ตำแหน่ง
                txtDDress.Text = number.ToString();
            }
            else
            {
                // ถ้าผู้ใช้พิมพ์ข้อความที่ไม่ใช่ตัวเลข จะเคลียร์ข้อมูล
                txtDDress.Text = "0";
            }
        }

        private void txtDPolishAndBathe_Leave(object sender, EventArgs e)
        {

            // ตรวจสอบว่าข้อมูลใน TextBox เป็นตัวเลขหรือไม่
            if (decimal.TryParse(txtDPolishAndBathe.Text, out decimal number))
            {
                // ฟอร์แมตตัวเลขให้มีทศนิยม 2 ตำแหน่ง
                txtDPolishAndBathe.Text = number.ToString();
            }
            else
            {
                // ถ้าผู้ใช้พิมพ์ข้อความที่ไม่ใช่ตัวเลข จะเคลียร์ข้อมูล
                txtDPolishAndBathe.Text = "0";
            }
        }

        private void txtDBury_Leave(object sender, EventArgs e)
        {

            // ตรวจสอบว่าข้อมูลใน TextBox เป็นตัวเลขหรือไม่
            if (decimal.TryParse(txtDBury.Text, out decimal number))
            {
                // ฟอร์แมตตัวเลขให้มีทศนิยม 2 ตำแหน่ง
                txtDBury.Text = number.ToString();
            }
            else
            {
                // ถ้าผู้ใช้พิมพ์ข้อความที่ไม่ใช่ตัวเลข จะเคลียร์ข้อมูล
                txtDBury.Text = "0";
            }
        }

        private void txtDBathe_Leave(object sender, EventArgs e)
        {

            // ตรวจสอบว่าข้อมูลใน TextBox เป็นตัวเลขหรือไม่
            if (decimal.TryParse(txtDBathe.Text, out decimal number))
            {
                // ฟอร์แมตตัวเลขให้มีทศนิยม 2 ตำแหน่ง
                txtDBathe.Text = number.ToString();
            }
            else
            {
                // ถ้าผู้ใช้พิมพ์ข้อความที่ไม่ใช่ตัวเลข จะเคลียร์ข้อมูล
                txtDBathe.Text = "0";
            }
        }

        private void txtDComplete_Leave(object sender, EventArgs e)
        {

            // ตรวจสอบว่าข้อมูลใน TextBox เป็นตัวเลขหรือไม่
            if (decimal.TryParse(txtDComplete.Text, out decimal number))
            {
                // ฟอร์แมตตัวเลขให้มีทศนิยม 2 ตำแหน่ง
                txtDComplete.Text = number.ToString();
            }
            else
            {
                // ถ้าผู้ใช้พิมพ์ข้อความที่ไม่ใช่ตัวเลข จะเคลียร์ข้อมูล
                txtDComplete.Text = "0";
            }
        }

        private void txtSilverRate_Leave(object sender, EventArgs e)
        {

            // ตรวจสอบว่าข้อมูลใน TextBox เป็นตัวเลขหรือไม่
            if (decimal.TryParse(txtSilverRate.Text, out decimal number))
            {
                // ฟอร์แมตตัวเลขให้มีทศนิยม 2 ตำแหน่ง
                txtSilverRate.Text = number.ToString("0.00");
            }
            else
            {
                // ถ้าผู้ใช้พิมพ์ข้อความที่ไม่ใช่ตัวเลข จะเคลียร์ข้อมูล
                txtSilverRate.Text = "0.00";
            }
        }

        private void txtMinQty_Leave(object sender, EventArgs e)
        {

            // ตรวจสอบว่าข้อมูลใน TextBox เป็นตัวเลขหรือไม่
            if (decimal.TryParse(txtMinQty.Text, out decimal number))
            {
                // ฟอร์แมตตัวเลขให้มีทศนิยม 2 ตำแหน่ง
                txtMinQty.Text = number.ToString();
            }
            else
            {
                // ถ้าผู้ใช้พิมพ์ข้อความที่ไม่ใช่ตัวเลข จะเคลียร์ข้อมูล
                txtMinQty.Text = "0";
            }
        }

        private void txtPCMat_Leave(object sender, EventArgs e)
        {

            // ตรวจสอบว่าข้อมูลใน TextBox เป็นตัวเลขหรือไม่
            if (decimal.TryParse(txtPCMat.Text, out decimal number))
            {
                // ฟอร์แมตตัวเลขให้มีทศนิยม 2 ตำแหน่ง
                txtPCMat.Text = number.ToString("0.00");
            }
            else
            {
                // ถ้าผู้ใช้พิมพ์ข้อความที่ไม่ใช่ตัวเลข จะเคลียร์ข้อมูล
                txtPCMat.Text = "0.00";
            }
        }

        private void txtDeductModel_Leave(object sender, EventArgs e)
        {

            // ตรวจสอบว่าข้อมูลใน TextBox เป็นตัวเลขหรือไม่
            if (decimal.TryParse(txtDeductModel.Text, out decimal number))
            {
                // ฟอร์แมตตัวเลขให้มีทศนิยม 2 ตำแหน่ง
                txtDeductModel.Text = number.ToString("0.00");
            }
            else
            {
                // ถ้าผู้ใช้พิมพ์ข้อความที่ไม่ใช่ตัวเลข จะเคลียร์ข้อมูล
                txtDeductModel.Text = "0.00";
            }
        }

        private void txtDevideQty_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtChunkDay_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtModelDay.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtModelDay_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtDFoundry.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtDFoundry_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtDDress.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtDDress_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtDPolish.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtDPolish_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtDPolishAndBathe.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtDPolishAndBathe_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtDBury.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtDBury_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtDBathe.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtDBathe_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtDComplete.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtDComplete_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtSilverRate.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtSilverRate_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtMinQty.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtMinQty_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtPCMat.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtPCMat_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void txtPCMat_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtDeductModel.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtDeductModel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDevideQty.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtDevideQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtChunkDay.Focus();
                e.SuppressKeyPress = true;
            }
        }
    }
}
