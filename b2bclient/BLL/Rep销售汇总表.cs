using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace b2bclient.BLL
{
    class Rep销售汇总表:IRep销售汇总表 
    {
        void IRep销售汇总表.GetKey(string clear_key, string date1, string date2, out string key)
        {
            var req = new Request();
            var json = req.request("/rep?t=销售汇总表", "{\"clear_key\":\"" + clear_key +
                "\",\"date1\":\"" + date1 + "\",\"date2\":\"" + date2 +  "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            key = read.Read("key");
        }

        DataTable IRep销售汇总表.GetData(string key, int pageSize, int pageIndex, out int total)
        {
            var req = new Request();
            var json = req.request("/pager?t=get_data", "{\"key\":\"" + key + "\",\"pageSize\":\"" + pageSize + "\",\"pageIndex\":\"" + pageIndex + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            total = Conv.ToInt(read.Read("total"));
            //
            var dt = new DataTable();
         
            dt.Columns.Add("单数");
            dt.Columns.Add("数量");
            dt.Columns.Add("金额");
            if (read.Read("data") != "")
            {
                foreach (ReadWriteContext.IReadContext r in read.ReadList("data"))
                {
                    dt.Rows.Add(
                        r.Read("单数"),
                        r.Read("数量"),
                        r.Read("金额")
                        );

                }

            }
            return dt;
        }
    }
}
