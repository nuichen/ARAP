﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace b2bclient.BLL
{
    interface IRep顾客汇总表
    {
        void GetKey(string clear_key, string date1, string date2, string mobile, out string key);
        DataTable GetData(string key, int pageSize, int pageIndex, out int total,string field,string fields);
    }
}
