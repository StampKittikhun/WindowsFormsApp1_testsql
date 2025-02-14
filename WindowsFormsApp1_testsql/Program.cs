using System;
using System.Windows.Forms;

namespace WindowsFormsApp1_testsql
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // เปิด LoginForm เป็นหน้าจอแรก
            Application.Run(new Form2());
        }
    }
}
