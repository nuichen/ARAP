using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace b2bclient.BLL
{
    class Rep销售明细:IRep销售明细 
    {
        void IRep销售明细.GetKey(string clear_key, string date1, string date2, string mobile, string goods_name, out string key)
        {
            var req = new Request();
            var json = req.request("/rep?t=销售明细", "{\"clear_key\":\"" + clear_key +
                "\",\"date1\":\"" + date1 + "\",\"date2\":\"" + date2 +
                "\",\"mobile\":\"" + mobile +
                "\",\"goods_name\":\"" + goods_name + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            key = read.Read("key");
        }

        System.Data.DataTable IRep销售明细.GetData(string key, int pageSize, int pageIndex, out int total,string field,string fields)
        {
            var req = new Request();
            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("key", key);
            write.Append("pageSize", pageSize.ToString());
            write.Append("pageIndex", pageIndex.ToString());
            write.Append("field", field);
            write.Append("fields", fields);
            var json = req.request("/pager?t=get_data_with_total", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            total = Conv.ToInt(read.Read("total"));
            //
            var dt = new DataTable();
            dt.Columns.Add("订单ID");
            dt.Columns.Add("状态");
            dt.Columns.Add("付款方式");
            dt.Columns.Add("客户");
            dt.Columns.Add("收货人");
            dt.Columns.Add("收货地址");
            dt.Columns.Add("联系手机");
            dt.Columns.Add("订单创建日期");
            dt.Columns.Add("商品名称");
            dt.Columns.Add("商品单价");
            dt.Columns.Add("总数量");
            dt.Columns.Add("总金额");
            if (read.Read("data") != "")
            {
                foreach (ReadWriteContext.IReadContext r in read.ReadList("data"))
                {
                    dt.Rows.Add(
                        r.Read("订单ID"),
                        r.Read("状态"),
                        r.Read("付款方式"),
                        r.Read("客户"),
                        r.Read("收货人"),
                        r.Read("收货地址"),
                        r.Read("联系手机"),
                        r.Read("订单创建日期"),
                        r.Read("商品名称"),
                        r.Read("商品单价"),
                        r.Read("总数量"),
                        r.Read("总金额")
                        );
                }

            }
            return dt;
        }
    }
}
