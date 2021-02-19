using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class CheckModel
    {
        //要审核的信息
        public string check_sheet_no { get; set; }
        public string check_approve_man { get; set; }
        public DateTime check_update_time { get; set; }

    }
}
