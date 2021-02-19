using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server
{
    public class Appsetting
    {

        public static string conn
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["conn"];
            }
        }

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