using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1_testsql.CkeckWork_Form;
using WindowsFormsApp1_testsql.Class;

namespace WindowsFormsApp1_testsql
{
    public partial class MainMenuForm : Form
    {
        private Form activeForm = null;
        public MainMenuForm()
        {
            InitializeComponent();
            if (UserSession.UserGroup != "admin")
            {
                btnSetLate.Visible = false;
            }
           
        }
        private void OpenChildForm(Form childForm) 
        {

            // ลบฟอร์มเดิมออกจาก childPanel ก่อนเพิ่มฟอร์มใหม่
            if (childPanel.Controls.Count > 0)
                childPanel.Controls.Clear();

            // ตั้งค่าฟอร์มย่อย
            childForm.TopLevel = false; // กำหนดให้ฟอร์มไม่ทำงานในฐานะ TopLevel Form
            childForm.FormBorderStyle = FormBorderStyle.None; // ลบกรอบของฟอร์ม
            childForm.Dock = DockStyle.Fill; // ปรับขนาดให้เต็ม childPanel
            childPanel.Controls.Add(childForm); // เพิ่มฟอร์มไปยัง childPanel
            childPanel.Tag = childForm; // ตั้งค่า Tag ของ childPanel เพื่อให้รู้ว่าฟอร์มไหนถูกโหลด
            childForm.BringToFront(); // ดึงฟอร์มให้แสดงอยู่หน้า
            childForm.Show(); // แสดงฟอร์ม

            Debug.WriteLine($"OpenChildForm: {childForm.Name} added to childPanel");

        }

        private void btnFoundry_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Foundry());
        }

        private void btnDress_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Dress());
        }

        private void btnPolish_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Polish());
        }

        private void btnPolishAndBathe_Click(object sender, EventArgs e)
        {
            OpenChildForm(new PolishAndBathe());
        }

        private void btnBury_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Bury());
        }

        private void btnBathe_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Bathe());
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            OpenChildForm(new CompleteForm());
        }

        private void btnSetLate_Click(object sender, EventArgs e)
        {
            OpenChildForm(new SetDateSliver());
        }
    }
}
