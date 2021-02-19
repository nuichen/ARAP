using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace PrintHelper.Helper
{
    class Conv
    {

        public static string DaXie2(string money)
        {
            
            
            //将小写金额转换成大写金额  
            money= ToDecimal(money).ToString("0.##");
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
            tb = tb.Copy();
            string[] g_fields = group_fields.Split(',');
            string[] s_fields = sum_fields.Split(',');
            tb.DefaultView.Sort = group_fields;
            tb = tb.DefaultView.ToTable();
            var tb2 = tb.Clone();
            DataRow dr = null;
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                DataRow row = tb.Rows[i];

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

                    }
                    else
                    {
                        flag = 1;
                    }
                }
                if (flag == 1)
                {
                    dr = tb2.NewRow();
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

        public static  Single getAnCMInterval()
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

        public static Int32 ToInt32(object obj)
        {
            if (obj == null)
            {
                return 0;
            }
            else
            {
                Int32 val;
                Int32.TryParse(obj.ToString(), out val);
                return val;
            }
        }

        public static DateTime ToDateTime(object obj)
        {
            if (obj == null)
            {
                return DateTime.MinValue;
            }
            else
            {
                try
                {
                    DateTime val = Convert.ToDateTime(obj);
                    return val;
                }
                catch (Exception)
                {
                    return DateTime.MinValue;
                }
            }
        }

        public static string ImageToString(System.Drawing.Image img)
        {
            if (img == null)
            {
                return "";
            }
            else
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                var bytes = ms.ToArray();
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                {
                    sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
                }
                return sb.ToString();
            }
           
        }

        public static System.Drawing.Image  StringToImage(string str)
        {
            if (str == "")
            {
                return null;
            }
            else
            {
                List<byte> lst = new List<byte>();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < str.Length; i++)
                {
                    sb.Append(str[i]);
                    if (sb.Length == 8)
                    {
                        var b = Convert.ToByte(sb.ToString(), 2);
                        lst.Add(b);
                        sb.Remove(0,sb.Length);
                    }
                }
                var bytes = lst.ToArray();
                System.IO.MemoryStream ms = new System.IO.MemoryStream();

                ms.Write(bytes, 0, bytes.Length);
                return System.Drawing.Image.FromStream(ms);
            }
        }

        public static System.Drawing.StringFormat AlignToStringFormat(int align)
        {
            System.Drawing.StringFormat sf = new System.Drawing.StringFormat();
            if (align == 1)
            {
                sf.LineAlignment = System.Drawing.StringAlignment.Center;
                sf.Alignment = System.Drawing.StringAlignment.Near ;
            }
            else if (align ==2)
            {
                sf.LineAlignment = System.Drawing.StringAlignment.Center;
                sf.Alignment = System.Drawing.StringAlignment.Center;
            }
            else if (align == 3)
            {
                sf.LineAlignment = System.Drawing.StringAlignment.Center;
                sf.Alignment = System.Drawing.StringAlignment.Far;
            }
            
            return sf;
        }

        public static System.Data.DataTable Paging(System.Data.DataTable tb, int pageRowCount, int pageIndex)
        {
            var tb2 = tb.Clone();
            for (int i = (pageIndex - 1) * pageRowCount; i < pageIndex * pageRowCount; i++)
            {
                if (i < tb.Rows.Count)
                {
                    tb2.Rows.Add(tb.Rows[i].ItemArray);
                }
            }
            return tb2;
        }

    }
}
