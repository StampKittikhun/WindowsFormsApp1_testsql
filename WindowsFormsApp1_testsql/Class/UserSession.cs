using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1_testsql.Class
{
    internal class UserSession
    {
        // สมาชิก static สำหรับเก็บข้อมูลผู้ใช้งาน
        public static string Username { get; set; }

        // คุณสามารถเพิ่มสมาชิกอื่น ๆ ได้ เช่น
        public static string UserGroup { get; set; }
        public static string Description { get; set; }
    }
}
