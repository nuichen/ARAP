using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IvyTran.Helper
{
    public class SessionHelper
    {
        public static string oper_id
        {
            get
            {
                return HttpContext.Current.Session["oper_id"]?.ToString() ?? "";
            }
            set
            {
                HttpContext.Current.Session["oper_id"] = value;
            }
        }

        //1：货商；2：业务员
        public static string oper_type
        {
            get
            {
                return HttpContext.Current.Session["oper_type"]?.ToString() ?? "";
            }
            set
            {
                HttpContext.Current.Session["oper_type"] = value;
            }
        }

        //0：分部；1：总部
        public static string is_branch
        {
            get
            {
                return HttpContext.Current.Session["is_branch"]?.ToString() ?? "";
            }
            set
            {
                HttpContext.Current.Session["is_branch"] = value;
            }
        }

        //客户价格级别
        public static string cus_level
        {
            get
            {
                return HttpContext.Current.Session["cus_level"]?.ToString() ?? "";
            }
            set
            {
                HttpContext.Current.Session["cus_level"] = value;
            }
        }

        //所属客户组
        public static string supcust_group
        {
            get
            {
                return HttpContext.Current.Session["supcust_group"]?.ToString() ?? "";
            }
            set
            {
                HttpContext.Current.Session["supcust_group"] = value;
            }
        }

        public static int MerId
        {
            get
            {
                if (int.TryParse(HttpContext.Current.Session["mc_id"]?.ToString(), out int result))
                    return result;
                else
                    return 1;
            }
            set
            {
                HttpContext.Current.Session["mc_id"] = value;
            }
        }

        public static string wx_open_id
        {
            get
            {
                return HttpContext.Current.Session["open_id"]?.ToString() ?? "";
            }
            set
            {
                HttpContext.Current.Session["open_id"] = value;
            }
        }
    }
}