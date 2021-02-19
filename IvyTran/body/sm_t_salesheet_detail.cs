using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyTran.body
{
    public class sm_t_salesheet_detail : Model.sm_t_salesheet_detail
    {
        public string offline_sheet_no = "";

        public new sm_t_salesheet_detail Clone()
        {
            return (sm_t_salesheet_detail)this.MemberwiseClone();
        }

    }

}
