using System;
using System.Data;
using System.Web;
using IvyTran.IBLL.OnLine;

namespace IvyTran.BLL.OnLine
{
    public class Pager : IPager
    {
         void IPager.ClearData(string key)
        {
            HttpContext.Current.Session[key] = null;
        }

        void IPager.GetData(string key, out System.Data.DataTable dt, int pageSize, int pageIndex, out int total)
        {
            var tb = (DataTable)HttpContext.Current.Session[key];
            if (tb == null)
            {
                throw new Exception("数据已过期，请重新查询");
            }
            //返回总行数
            total = tb.Rows.Count;
            //返回数据
            pageIndex -= 1;
            DataTable dt2 = tb.Clone();
            for (int i = pageIndex * pageSize; i < pageIndex * pageSize + pageSize; i++)
            {
                if (tb.Rows.Count - 1 < i)
                {
                    continue; 
                }
                dt2.Rows.Add(tb.Rows[i].ItemArray);
            }
            dt = dt2;
        }

        void IPager.GetDataWithTotal(string key, out System.Data.DataTable dt, int pageSize, int pageIndex, out int total, string field, string fields)
        {
            var tb = (DataTable)HttpContext.Current.Session[key];
            if (tb == null)
            {
                throw new Exception("数据已过期，请重新查询");
            }
            //返回总行数
            total = tb.Rows.Count;
            //返回数据
            pageIndex -= 1;
            DataTable dt2 = tb.Clone();
            dt2.Columns[field].DataType = typeof(string);
            for (int i = pageIndex * pageSize; i < pageIndex * pageSize + pageSize; i++)
            {
                if (tb.Rows.Count - 1 < i)
                {
                    continue;
                }
                dt2.Rows.Add(tb.Rows[i].ItemArray);
            }
            dt = dt2;
            //添加合计行
            DataRow row = dt.NewRow();
            dt.Rows.Add(row);
            row[field] = "合计";
            if (fields == "")
            {

            }
            else if (fields.Contains(",") == false)
            {
                row[fields] = tb.Compute("sum(" + fields + ")","");
            }
            else
            {
                foreach (string f in fields.Split(','))
                {
                    row[f] = tb.Compute("sum(" + f + ")", "");
                }
            }
            
        }
    }
}