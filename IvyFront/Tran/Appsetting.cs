using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tran
{
    class Appsetting
    {

        public static string conn
        {
            get
            {
                return "Data Source=localhost;Port=5002;Database=ivytalkag_01;Persist Security Info=yes;UserId=root;PWD=ivy004168;pooling=true";
            }
        }

        private static string _user_id;
        public static string user_id
        {
            get
            {
                if (_user_id == null)
                {
                    if (System.IO.File.Exists(Program.path + "\\setting\\user_id.txt") == false)
                    {
                        System.IO.File.WriteAllText(Program.path + "\\setting\\user_id.txt", "00000000000");
                    }
                    _user_id = System.IO.File.ReadAllText(Program.path + "\\setting\\user_id.txt",System.Text.Encoding.GetEncoding("gb2312"));
                }
                return _user_id;
            }
            set
            {
                _user_id = value;
            }
        }

     



        private static string _port;
        public static string port
        {
            get
            {
                if (_port == null)
                {
                    if (System.IO.File.Exists(Program.path + "\\setting\\port.txt") == false)
                    {
                        System.IO.File.WriteAllText(Program.path + "\\setting\\port.txt", "8383");
                    }
                    _port = System.IO.File.ReadAllText(Program.path + "\\setting\\port.txt", System.Text.Encoding.GetEncoding("gb2312"));
                }
                return _port;
            }
        }

    }
}
