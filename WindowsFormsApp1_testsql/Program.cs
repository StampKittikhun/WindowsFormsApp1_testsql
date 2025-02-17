using System;
using System.Windows.Forms;
using WindowsFormsApp1_testsql.CkeckWork_Form;

namespace WindowsFormsApp1_testsql
{
    static class Program 
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // เปิดหน้าจอแรก
            Application.Run(new MainMenuForm());
        }
    }
}
