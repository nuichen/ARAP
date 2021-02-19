using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.body
{
    public class microquery_res
    {
        public microquery_res()
        {

        }
        public string return_code { get; set; }
        public string return_msg { get; set; }
        public string result_code { get; set; }
        public string err_code { get; set; }
        public string err_code_des { get; set; }
        public string trade_state { get; set; }
        public string trade_state_desc { get; set; }

        public microquery_res(string context)
        {
            ReadXML read = new ReadXML(context);
            return_code = read.Read("return_code");
            return_msg = read.Read("return_msg");
            result_code = read.Read("result_code");
            err_code = read.Read("err_code");
            err_code_des = read.Read("err_code_des");
            trade_state = read.Read("trade_state");
            trade_state_desc = read.Read("trade_state_desc");
        }

        public bool is_doing(string trade_state)
        {
            if (trade_state.ToLower() == "userpaying")
            {
                return true;
            }
            return false;
        }

        public bool is_success(string trade_state)
        {
            if (trade_state.ToLower() == "success")
            {
                return true;
            }
            return false;
        }



    }
}