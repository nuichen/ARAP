using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.body
{
    public class micropay_res
    {
        public micropay_res()
        {

        }

        public string return_code { get; set; }
        public string return_msg { get; set; }
        public string result_code { get; set; }
        public string err_code { get; set; }
        public string err_code_des { get; set; }

        public micropay_res(string context)
        {
            ReadXML read = new ReadXML(context);
            return_code = read.Read("return_code");
            return_msg = read.Read("return_msg");
            result_code = read.Read("result_code");
            err_code = read.Read("err_code");
            err_code_des = read.Read("err_code_des");
        }

        public bool is_doing(string errCode)
        {
            List<string> lst = new List<string>();
            lst.Add("SYSTEMERROR");
            lst.Add("BANKERROR");
            lst.Add("USERPAYING");
            if (lst.Contains(errCode.ToUpper()) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}