using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyTran.body
{
    public class ic_t_inout_store_detail : Model.ic_t_inout_store_detail
    {
        public string offline_sheet_no = "";//??
        public new ic_t_inout_store_detail Clone()
        {
            return (ic_t_inout_store_detail)this.MemberwiseClone();
        }
    }
}
