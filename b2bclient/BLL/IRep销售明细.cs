﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace b2bclient.BLL
{
    interface IRep销售明细
    {
        void GetKey(string clear_key, string date1, string date2, string mobile, string goods_name, out string key);
        DataTable GetData(string key, int pageSize, int pageIndex, out int total,string field,string fields);
    }
}
