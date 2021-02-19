using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using IvyTran.Wxpay;

namespace IvyTran.IBLL.ERP
{
    interface ISupOffer
    {
        DataTable GetSupOffer(DateTime start_time,DateTime end_time,string sup_no,string approve_flag);
        void CheckSupOffer(string sheet_nos,DateTime approve_date,string approve_man);
    }
}
