using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using IvyTran.Helper;

namespace IvyTran
{
    class AppSetting
    {
        public static string path = HttpContext.Current.Request.PhysicalApplicationPath;
        public static string versions = "v2.0.0_20181220";

        private static string _conn;
        public static string conn
        {
            get
            {
                if (string.IsNullOrEmpty(_conn))
                {
                    _conn = System.Configuration.ConfigurationManager.AppSettings["conn"];
                }
                return _conn;
            }

        }
        /// <summary>
        /// Cookie 容器
        /// </summary>
        public static CookieContainer CookieContainer { get; } = new CookieContainer();
        private static string _msg_conn;
        public static string msg_conn
        {
            get
            {
                if (_msg_conn == null)
                {
                    _msg_conn = System.Configuration.ConfigurationManager.AppSettings["msg_conn"];
                }
                return _msg_conn;
            }
        }

        public static string sort_cls
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["sort_cls"]; }
        }
        public static string ex_conn
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["ex_conn"]; }
        }
        private static string _imgsvr;
        public static string imgsvr
        {
            get
            {
                if (_imgsvr == null)
                {
                    _imgsvr = System.Configuration.ConfigurationManager.AppSettings["imgsvr"];
                }
                return _imgsvr;
            }
        }

        private static int _print_count;
        public static int print_count
        {
            get
            {
                if (_print_count == 0)
                {
                    DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                    DAL.merchant_configDAL dal = new DAL.merchant_configDAL(db);
                    var temp_count = dal.SelectValue(Global.McId, "print_count");
                    if (temp_count == "") temp_count = "1";
                    _print_count = Conv.ToInt(temp_count);
                }
                return _print_count;
            }
            set
            {
                _print_count = value;
            }
        }

        private static string _send_url;
        public static string send_url
        {
            get
            {
                if (_send_url == null)
                {
                    _send_url = System.Configuration.ConfigurationManager.AppSettings["send_url"];
                }
                return _send_url;
            }
        }
        private static string _wxsvr;
        public static string wxsvr
        {
            get
            {
                if (_wxsvr == null)
                {
                    _wxsvr = System.Configuration.ConfigurationManager.AppSettings["wxsvr"];
                }
                return _wxsvr;
            }
        }

        public static string sms_api
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["sms_api"];
            }
        }

        public static string mc_id
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["mc_id"];
            }
        }

        public static string uid
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["mc_uid"];
            }
        }

        //收货称
        public static string pay_notify_url
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["pay_notify_url"];
            }
        }
        public static string ali_notify_url
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ali_notify_url"];
            }
        }
        public static string server_ip
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["server_ip"];
            }
        }


    }
}
