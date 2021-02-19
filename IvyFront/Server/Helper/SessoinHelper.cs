using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server
{
    public class SessoinHelper
    {
        public static string mer_no
        {
            get
            {
                var obj = HttpContext.Current.Session["mer_no"];
                if (obj == null)
                {
                    return "";
                }
                else
                {
                    return (string)obj;
                }
            }
            set
            {
                HttpContext.Current.Session["mer_no"] = value;
            }
        }

        public static string oper_id
        {
            get
            {
                var obj = HttpContext.Current.Session["oper_id"];
                if (obj == null)
                {
                    return "";
                }
                else
                {
                    return (string)obj;
                }
            }
            set
            {
                HttpContext.Current.Session["oper_id"] = value;
            }
        }

        public static string pwd
        {
            get
            {
                var obj = HttpContext.Current.Session["pwd"];
                if (obj == null)
                {
                    return "";
                }
                else
                {
                    return (string)obj;
                }
            }
            set
            {
                HttpContext.Current.Session["pwd"] = value;
            }
        }

        public static string mer_name
        {
            get
            {
                var obj = HttpContext.Current.Session["mer_name"];
                if (obj == null)
                {
                    return "";
                }
                else
                {
                    return (string)obj;
                }
            }
            set
            {
                HttpContext.Current.Session["mer_name"] = value;
            }
        }
           
    }
}