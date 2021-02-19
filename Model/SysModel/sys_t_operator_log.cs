using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.SysModel
{
    public class sys_t_operator_log
    {
        //方法/事件名称
        public string func_id { get; set; }
        public string oper_id { get; set; }
        public DateTime oper_date { get; set; }

        //设备类型/编码
        public string computer_name { get; set; }

        //功能模块
        public string module_name { get; set; }
        public string log_info { get; set; }

        //日志类型:系统日志;操作日志;监控日志
        public string log_type { get; set; }

        //日志等级:WARNING;ERROR;INFO;EXCEPTION
        public string log_level { get; set; }
        public string memo { get; set; }
        public string other1 { get; set; }
        public string other2 { get; set; }
        public string other3 { get; set; }
    }
}
