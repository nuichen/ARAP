﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace b2bclient.BLL
{
    interface IRep销售汇总表
    {
        void GetKey(string clear_key, string date1, string date2, out string key);
        DataTable GetData(string key, int pageSize, int pageIndex, out int total);
    }
}
