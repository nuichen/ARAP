using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IvyBack.cons;
using System.Data;
using System.Net;

namespace IvyBack.Helper
{
    public class AppSetting
    {
        public static void app_ini()
        {
            InI.filePath = ini_path;
            if (string.IsNullOrEmpty(InI.ReadValue("service", "svr")))
                InI.Writue("service", "svr", "");
            if (string.IsNullOrEmpty(InI.ReadValue("service", "imgsvr")))
                InI.Writue("service", "imgsvr", "");
            if (string.IsNullOrEmpty(InI.ReadValue("service", "wxsvr")))
                InI.Writue("service", "wxsvr", "");


            if (string.IsNullOrEmpty(InI.ReadValue("app", "fontSize")))
                InI.Writue("app", "fontSize", "1");

        }

        /// <summary>
        /// Cookie 容器
        /// </summary>
        public static CookieContainer CookieContainer { get; } = new CookieContainer();

        private static string _path;
        public static string path
        {
            get
            {
                if (_path == null)
                {
                    _path = System.Windows.Forms.Application.StartupPath;
                }
                return _path;
            }
            set
            {
                _path = value;
            }
        }

        private static string _ini_path;
        public static string ini_path
        {
            get
            {
                if (_ini_path == null)
                {
                    _ini_path = System.Windows.Forms.Application.StartupPath + "\\IVYTALK.ini";
                }
                return _ini_path;
            }
            set
            {
                _ini_path = value;
            }
        }

        private static string _svr;
        public static string svr
        {
            get
            {
                if (string.IsNullOrEmpty(_svr))
                {
                    _svr = InI.ReadValue("service", "svr");
                }
                return _svr;
            }
            set
            {
                if (_svr != value)
                {
                    InI.Writue("service", "svr", value);
                }
                _svr = value;
            }
        }

        private static string _imgsvr;
        public static string imgsvr
        {
            get
            {
                if (string.IsNullOrEmpty(_imgsvr))
                {
                    _imgsvr = InI.ReadValue("service", "imgsvr");
                }
                return _imgsvr;
            }
            set
            {
                _imgsvr = value;
            }
        }

        private static string _wxsvr;
        public static string wxsvr
        {
            get
            {
                if (string.IsNullOrEmpty(_wxsvr))
                {
                    _wxsvr = InI.ReadValue("service", "wxsvr");
                }
                return _wxsvr;
            }
            set
            {
                _wxsvr = value;
            }
        }

        private static int _fontSize;
        public static int fontSize
        {
            get
            {
                if (_fontSize == 0)
                {
                    _fontSize = Conv.ToInt(InI.ReadValue("app", "fontSize"));
                }
                return _fontSize;
            }
            set
            {
                _fontSize = value;
            }
        }

        private static string _isMax;
        public static string isMax
        {
            get
            {
                if (string.IsNullOrEmpty(_isMax))
                {
                    _isMax = InI.ReadValue("app", "isMax");
                }
                return _isMax;
            }
            set
            {
                _isMax = value;
            }
        }


        //所有菜单
        private static Dictionary<string, Model.sys_t_oper_type> _oper_types;
        public static Dictionary<string, Model.sys_t_oper_type> oper_types
        {
            get
            {
                return _oper_types;
            }
            set
            {
                _oper_types = value;
            }
        }

        //我的权限
        private static Dictionary<string, Model.sa_t_oper_grant> _oper_grants;
        public static Dictionary<string, Model.sa_t_oper_grant> oper_grants
        {
            get
            {
                IBLL.ISys bll = new BLL.SysBLL();

                _oper_grants = bll.GetAllGrantDic(new Model.sa_t_oper_grant()
                {
                    oper_id = Program.oper.oper_type,
                });

                return _oper_grants;
            }
            set
            {
                _oper_grants = value;
            }
        }


    }
}
