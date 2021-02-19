using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace b2bclient
{
    class ToExcel
    {
        public static void toExcel(DataTable dt, string file)
        {
            var sb = new StringBuilder();
            foreach (DataColumn col in dt.Columns)
            {
                sb.Append(col.ColumnName + "\t");
            }
            sb.Append("\r\n");
            //
            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sb.Append(row[i].ToString() + "\t");
                }
                sb.Append("\r\n");
            }

            System.IO.File.WriteAllText(file, sb.ToString(), System.Text.Encoding.GetEncoding("gb2312"));

        }
    }
}
