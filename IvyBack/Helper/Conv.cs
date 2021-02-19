using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Security.Cryptography;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using IvyBack.cons;

namespace IvyBack.Helper
{
    public static class Conv
    {

        public static string ToMD5(string value)
        {
            if (value == null || value == "")
            {
                return "";
            }
            byte[] data = System.Text.Encoding.Default.GetBytes(value);
            var md = new MD5CryptoServiceProvider();
            var data2 = md.ComputeHash(data);
            var v = BitConverter.ToString(data2);
            v = v.Replace("-", "");
            v = v.ToLower();
            return v;
        }

        public static string ToString(object obj)
        {
            if (obj == null)
            {
                return "";
            }
            return obj.ToString();
        }

        public static DateTime ToDateTime(object obj)
        {
            if (obj == null || obj.ToString() == "")
            {
                return DateTime.MinValue;
            }
            DateTime dt;
            DateTime.TryParse(obj.ToString(), out dt);
            return dt;
        }

        public static string DaXie(string money)
        {


            //将小写金额转换成大写金额  
            money = ToDecimal(money).ToString("0.##");
            String[] MyScale = { "分", "角", "元", "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿", "拾", "佰", "仟", "兆", "拾", "佰", "仟" };
            String[] MyBase = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
            String M = "";
            bool isPoint = false;
            if (money.IndexOf(".") != -1)
            {
                money = money.Remove(money.IndexOf("."), 1);
                isPoint = true;
            }
            for (int i = money.Length; i > 0; i--)
            {
                int MyData = Convert.ToInt16(money[money.Length - i].ToString());//?
                M += MyBase[MyData];//?
                if (isPoint == true)
                {
                    M += MyScale[i - 1];//?
                }
                else
                {
                    M += MyScale[i + 1];//?
                }
            }
            return M;
        }

        public static DataTable Group(DataTable tb, string group_fields, string sum_fields)
        {
            var dt = tb.Copy();
            tb = tb.Copy();
            string[] g_fields = group_fields.Split(',');
            string[] s_fields = sum_fields.Split(',');
            tb.DefaultView.Sort = group_fields;
            tb = tb.DefaultView.ToTable();
            tb.Columns.Add("rows", typeof(List<DataRow>));
            var tb2 = tb.Clone();
            DataRow dr = null;
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                DataRow row = tb2.NewRow();
                row.ItemArray = tb.Rows[i].ItemArray;
                int flag = 0;
                if (i == 0)
                {
                    flag = 1;
                }
                else
                {
                    int flag2 = 0;
                    foreach (string field in g_fields)
                    {
                        if (dr[field].ToString() != row[field].ToString())
                        {
                            flag2 = 1;
                            break;
                        }
                    }
                    if (flag2 == 0)
                    {
                        if (sum_fields != "")
                        {
                            foreach (string field in s_fields)
                            {
                                dr[field] = Conv.ToDecimal(dr[field]) + Conv.ToDecimal(row[field]);
                            }
                        }
                        var lst = (List<DataRow>)dr["rows"];
                        lst.Add(row);
                    }
                    else
                    {
                        flag = 1;
                    }
                }
                if (flag == 1)
                {
                    dr = tb2.NewRow();
                    var lst = new List<DataRow>();
                    lst.Add(row);
                    dr["rows"] = lst;
                    foreach (string field in g_fields)
                    {
                        dr[field] = row[field];
                    }
                    if (sum_fields != "")
                    {
                        foreach (string field in s_fields)
                        {
                            dr[field] = row[field];
                        }
                    }

                    tb2.Rows.Add(dr);
                }

            }
            return tb2;
        }

        public static Single getAnCMInterval()
        {
            return System.Drawing.Printing.PrinterUnitConvert.Convert(100,
            System.Drawing.Printing.PrinterUnit.TenthsOfAMillimeter, System.Drawing.Printing.PrinterUnit.Display);
        }

        public static decimal ToDecimal(object obj)
        {
            if (obj == null)
            {
                return 0;
            }
            else
            {
                decimal val;
                decimal.TryParse(obj.ToString(), out val);
                return val;
            }
        }

        public static Int16 ToInt16(object obj)
        {
            if (obj == null)
            {
                return 0;
            }
            else
            {
                Int16 val;
                Int16.TryParse(obj.ToString(), out val);
                return val;
            }
        }

        public static float ToFloat(object obj)
        {
            if (obj == null || obj.ToString() == "")
            {
                return 0;
            }
            float value = 0;
            try
            {
                float.TryParse(obj.ToString(), out value);
            }
            catch (Exception)
            { }
            return value;
        }

        public static int ToInt(object obj)
        {
            if (obj == null)
            {
                return 0;
            }
            else
            {
                if (obj == DBNull.Value)
                {
                    return 0;
                }
                else
                {
                    int res = 0;
                    int.TryParse(obj.ToString(), out res);

                    return res;
                }
            }
        }

        public static char ToChar(object obj)
        {
            if (obj == null)
            {
                return ' ';
            }
            else
            {
                if (string.IsNullOrEmpty(obj.ToString()))
                {
                    return ' ';
                }
                else
                {
                    return Convert.ToChar(obj);
                }
            }
        }

        public static DataTable Assign<T>()
        {
            DataTable tb = new DataTable();
            var t = typeof(T);
            var pars = t.GetProperties();

            foreach (var item in pars)
            {
                tb.Columns.Add(item.Name, item.PropertyType);
            }
            return tb;
        }

        public static void AddColorTable(DataTable tb, string key)
        {
            if (tb.Columns.Contains("row_color") == false)
            {
                tb.Columns.Add("row_color", typeof(Color));
            }

            foreach (DataRow dr in tb.Rows)
            {

                dr["row_color"] = dr[key].Equals("0") ? Color.Red : Color.Black;
            }
        }
        public static bool IsDataTableNull(DataTable tb)
        {
            if (tb == null) return true;
            if (tb.Rows.Count == 0) return true;

            for (int i = 0; i < tb.Rows.Count; i++)
            {
                int null_num = 0;
                for (int j = 0; j < tb.Columns.Count; j++)
                {
                    if (tb.Rows[i][j] == null || string.IsNullOrEmpty(tb.Rows[i][j].ToString()))
                    {
                        null_num++;
                    }
                }
                if (null_num == tb.Columns.Count)
                {
                    return true;
                }
            }

            return false;
        }

        public static Dictionary<string, object> ControlsAdds(Control c, Dictionary<string, object> dic)
        {
            if (dic == null)
                dic = new Dictionary<string, object>();
            object value;
            foreach (Control con in c.Controls)
            {
                if (con is TextBox)
                {
                    TextBox txt = con as TextBox;
                    if (dic.TryGetValue(txt.Name, out value))
                        dic[txt.Name] = txt.Text;
                    else
                        dic.Add(txt.Name, txt.Text);
                }
                else if (con is MyTextBox)
                {
                    MyTextBox txt = con as MyTextBox;
                    if (dic.TryGetValue(txt.Name, out value))
                        dic[txt.Name] = txt.Text;
                    else
                        dic.Add(txt.Name, txt.Text);
                }
                else if (con is EditGrid)
                {
                    EditGrid dgv = con as EditGrid;
                    if (dic.TryGetValue(dgv.Name, out value))
                        dic[dgv.Name] = dgv.DataSource.Copy();
                    else
                        dic.Add(dgv.Name, dgv.DataSource.Copy());
                }


                if (con.Controls != null && !(con is EditGrid))
                {
                    ControlsAdds(con, dic);
                }
            }

            return dic;
        }
        public static bool ControlsCom(Control c, Dictionary<string, object> dic)
        {
            foreach (Control con in c.Controls)
            {
                if (con is TextBox)
                {
                    TextBox txt = con as TextBox;

                    if (dic.ContainsKey(txt.Name) && !dic[txt.Name].Equals(txt.Text))
                        return false;

                }
                else if (con is MyTextBox)
                {
                    MyTextBox txt = con as MyTextBox;
                    if (dic.ContainsKey(txt.Name) && !dic[txt.Name].Equals(txt.Text))
                        return false;
                }
                else if (con is EditGrid)
                {
                    EditGrid dgv = con as EditGrid;
                    if (!dic.ContainsKey(dgv.Name)) continue;
                    DataTable ord_tb = dic[dgv.Name] as DataTable;

                    if (ord_tb.Rows.Count != dgv.DataSource.Rows.Count)
                        return false;

                    for (int i = 0; i < ord_tb.Rows.Count; i++)
                    {
                        for (int j = 0; j < ord_tb.Columns.Count; j++)
                        {
                            if (!ord_tb.Rows[i][j].Equals(dgv.DataSource.Rows[i][j]))
                                return false;
                        }
                    }
                }


                if (con.Controls != null && !(con is EditGrid))
                {
                    if (!ControlsCom(con, dic))
                        return false;
                }
            }

            return true;
        }

        public static void CopyDataRow(DataTable tb, int row_index, DataRow dr)
        {
            foreach (DataColumn c in tb.Columns)
            {
                if (dr.Table.Columns.Contains(c.ColumnName))
                    tb.Rows[row_index][c.ColumnName] = dr[c.ColumnName];
            }


        }
        public static void CopyDataRow(DataRow row, DataRow dr)
        {
            foreach (DataColumn c in row.Table.Columns)
            {
                if (dr.Table.Columns.Contains(c.ColumnName))
                    row[c.ColumnName] = dr[c.ColumnName];
            }

        }

        public static void ClearTable(this DataTable tb)
        {
            if (tb == null) return;

            HashSet<string> hs = new HashSet<string>();

            foreach (DataColumn dc in tb.Columns)
            {
                hs.Add(dc.ColumnName);
            }

            for (int i = tb.Rows.Count - 1; i >= 0; i--)
            {
                int null_sum = 0;
                foreach (string name in hs)
                {
                    if (tb.Rows[i][name] == null || string.IsNullOrEmpty(tb.Rows[i][name].ToString()))
                    {
                        null_sum++;
                    }
                }

                if (null_sum > tb.Columns.Count / 3 * 2)
                {
                    tb.Rows.RemoveAt(i);
                }

            }

        }

        public static void ClearTable(this DataTable tb, string key_col)
        {
            if (tb == null) return;

            for (int i = tb.Rows.Count - 1; i >= 0; i--)
            {
                string value = tb.Rows[i][key_col].ToString();
                if (string.IsNullOrEmpty(value))
                {
                    tb.Rows.RemoveAt(i);
                }
            }

        }

        public static string Format(DataColumn col, object value, string format)
        {
            if (value.ToString() == "")
            {
                return "";
            }
            if (string.IsNullOrEmpty(format))
            {
                return Helper.Conv.ToString(value);
            }
            else if (format.StartsWith("{") == true && format.EndsWith("}") == true)
            {
                format = format.Substring(1, format.Length - 2);

                foreach (string str in format.Split(','))
                {
                    var arr = str.Split(':');
                    if (arr.Length > 1)
                    {
                        if (arr[0] == Helper.Conv.ToString(value))
                        {
                            return arr[1];
                        }
                    }

                }
                return Helper.Conv.ToString(value);

            }
            else if (format == "大写金额")
            {
                return Helper.Conv.DaXie(Helper.Conv.ToDecimal(value).ToString("0.00"));
            }
            else if (col.DataType == typeof(DateTime) || format.Contains("y") || format.Contains("M") || format.Contains("d")
                || format.Contains("H") || format.Contains("m") || format.Contains("s") || format.Contains("f"))
            {
                if (Convert.ToDateTime(value) == DateTime.MinValue)
                {
                    return "";
                }
                else
                {
                    return Convert.ToDateTime(value).ToString(format);
                }
            }
            else
            {
                return Helper.Conv.ToDecimal(value).ToString(format);
            }
        }

        public static DataTable GetDataTable(List<ReadWriteContext.IReadContext> lst)
        {

            DataTable dt = new DataTable();

            foreach (ReadWriteContext.IReadContext r in lst)
            {
                Dictionary<string, object> dic = r.ToDictionary();

                if (dt.Columns.Count < 1)
                {
                    foreach (string key in dic.Keys)
                    {
                        dt.Columns.Add(key);
                    }
                }

                DataRow dr = dt.NewRow();
                foreach (string key in dic.Keys)
                {
                    int i = dt.Columns.IndexOf(key);
                    dr[i] = dic[key];
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public static void DelectDir(string path)
        {
            if (!Directory.Exists(path))
            {
                return;
            }
            DirectoryInfo dir = new DirectoryInfo(path);
            FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
            foreach (FileSystemInfo i in fileinfo)
            {
                if (i is DirectoryInfo subdir)            //判断是否文件夹
                {
                    subdir.Delete(true);          //删除子目录和文件
                }
                else
                {
                    File.Delete(i.FullName);      //删除指定文件
                }
            }
        }

    }
}
