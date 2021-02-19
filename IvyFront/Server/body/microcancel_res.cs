using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.body
{
    public class microcancel_res
    {
        public microcancel_res()
        {

        }
        public string return_code { get; set; }
        public string return_msg { get; set; }
        public string result_code { get; set; }
        public string err_code { get; set; }
        public string err_code_des { get; set; }
        public string recall { get; set; }

        public microcancel_res(string context)
        {
            ReadXML read = new ReadXML(context);
            return_code = read.Read("return_code");
            return_msg = read.Read("return_msg");
            result_code = read.Read("result_code");
            err_code = read.Read("err_code");
            err_code_des = read.Read("err_code_des");
            recall = read.Read("recall");
        }

        public bool need_query(string errCode)
        {
            if (errCode.ToLower() == "SYSTEMERROR".ToLower())
            {
                return true;
            }
            return false;
        }
    }
}