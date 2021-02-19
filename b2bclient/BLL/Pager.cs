using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace b2bclient.BLL
{
    class Pager:IPager 
    {
        void IPager.ClearData(string key)
        {
            throw new NotImplementedException();
        }

        void IPager.GetData(string key, out string data, int pageSize, int pageIndex, out int total)
        {
            throw new NotImplementedException();
        }

        void IPager.GetDataWithTotal(string key, out string data, int pageSize, int pageIndex, out int total, string field, string fields)
        {
            throw new NotImplementedException();
        }
    }
}
