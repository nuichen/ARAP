using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace b2bclient.BLL
{
    public interface IPager
    {
        void ClearData(string key);
        void GetData(string key, out string data, int pageSize, int pageIndex, out int total);
        void GetDataWithTotal(string key, out string data, int pageSize, int pageIndex, out int total, string field, string fields);
    }
}
