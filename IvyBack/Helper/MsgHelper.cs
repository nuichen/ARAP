using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IvyBack.BLL;
using IvyBack.IBLL;

namespace IvyBack.Helper
{
    class MsgHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dc"></param>
        /// <param name="type">0查入库价格异常  1查数量为0  2查金额为0的 3价格异常 4查销售</param>
        /// <returns></returns>
       static  ArrayList arr(DataTable dc, int type)
        {
            ArrayList al = new ArrayList();
            switch (type)
            {
                case 0:
                    //入库
                    IItem i = new ItemBLL();
                    DataTable dt = i.ItemPrice(1);
                    if (dt.Rows.Count > 0 && dc.Rows.Count > 0)
                    {
                        foreach (DataRow item in dc.Rows)
                        {
                            DataRow[] row1 = dt.Select("item_subno='" + item["item_subno"].ToString() + "'");
                            if (row1.Length <= 3)
                            {
                                foreach (DataRow dataRow in row1)
                                {
                                    //decimal d1=(0.5).ToDecimal()* (dataRow["valid_price"].ToDecimal());
                                    //decimal d = (1.5).ToDecimal() * (dataRow["valid_price"].ToDecimal());
                                    if (item["real_price"].ToDecimal() >= ((1.5).ToDecimal() * (dataRow["valid_price"].ToDecimal())) || item["real_price"].ToDecimal() <= ((0.5).ToDecimal() * (dataRow["valid_price"].ToDecimal())))
                                    {
                                        al.Add(item["item_name"]);
                                        break;
                                    }
                                }
                            }
                            else if (row1.Length == 0)
                            {
                                break;
                            }
                            else
                            {
                                int i1 = 0;
                                foreach (DataRow dataRow in row1)
                                {
                                    if (i1 <= 3)
                                    {
                                        //string bb = item["valid_price"].ToString();
                                        //string aa = dataRow["valid_price"].ToString();
                                        //decimal d = (1.5).ToDecimal() * (dataRow["valid_price"].ToDecimal());
                                        if (item["real_price"].ToDecimal() >= ((1.5).ToDecimal() * (dataRow["valid_price"].ToDecimal())) || item["real_price"].ToDecimal() <= ((0.5).ToDecimal() * (dataRow["valid_price"].ToDecimal())))
                                        {
                                            al.Add(item["item_name"]);
                                            break;
                                        }
                                    }
                                    i1++;
                                }
                            }
                        }
                    }
                    break;
                case 1:
                    foreach (DataRow item in dc.Rows)
                    {
                        try
                        {
                            if (item["in_qty"].ToDecimal() <= 0 && item["item_subno"].ToString() != ""
                                || item["in_qty"].ToString() == "" && item["item_subno"].ToString() != ""
                            )
                            {
                                al.Add(item["item_name"].ToString());
                            }
                        }
                        catch (Exception e)
                        {
                            if ( item["sale_qnty"].ToString() == "" && item["item_subno"].ToString() != ""
                                || item["sale_qnty"].ToDecimal() <= 0 && item["item_subno"].ToString() != ""
                            )
                            {
                                al.Add(item["item_name"].ToString());
                            }
                        }
                       
                    }
                    break;
                case 2:
                    foreach (DataRow item in dc.Rows)
                    {
                        try
                        {
                            if (item["sub_amount"].ToDecimal() <= 0 && item["item_subno"].ToString() != ""
                                || item["sub_amount"].ToString() == "" && item["item_subno"].ToString() != "")
                            {
                                al.Add(item["item_name"].ToString());
                            }
                        }
                        catch 
                        {
                            if (item["sale_money"].ToDecimal() <= 0 && item["item_subno"].ToString() != ""
                                || item["sale_money"].ToString() == "" && item["item_subno"].ToString() != "")
                            {
                                al.Add(item["item_name"].ToString());
                            }
                            
                          
                        }
                        
                    }
                    break;
                case 3:
                    foreach (DataRow item in dc.Rows)
                    {
                        try
                        {
                            if (item["valid_price"].ToDecimal() <= 0 && item["item_subno"].ToString() != "" || item["valid_price"].ToString() == "" && item["item_subno"].ToString() != "")
                            {
                                al.Add(item["item_name"].ToString());
                            }
                        }
                        catch 
                        {
                            if (item["real_price"].ToDecimal() <= 0 && item["item_subno"].ToString() != "" || item["real_price"].ToString() == "" && item["item_subno"].ToString() != "")
                            {
                                al.Add(item["item_name"].ToString());
                            }
                        }
                       
                    }
                    break;
                case 4:
                    //销售
                    IItem itemBLL = new ItemBLL();
                    DataTable dt1 = itemBLL.ItemPrice(0);
                    if (dt1.Rows.Count > 0 && dc.Rows.Count > 0)
                    {
                        foreach (DataRow item in dc.Rows)
                        {
                            DataRow[] row1 = dt1.Select("item_subno='" + item["item_subno"].ToString() + "'");
                            if (row1.Length <= 3)
                            {
                                foreach (DataRow dataRow in row1)
                                {
                                    //decimal d1 = (0.5).ToDecimal() * (dataRow["sale_price"].ToDecimal());
                                    //decimal d = (1.5).ToDecimal() * (dataRow["sale_price"].ToDecimal());
                                    if (item["valid_price"].ToDecimal() >= ((1.5).ToDecimal() * (dataRow["sale_price"].ToDecimal())) || item["valid_price"].ToDecimal() <= ((0.5).ToDecimal() * (dataRow["sale_price"].ToDecimal())))
                                    {
                                        al.Add(item["item_name"]);
                                        break;
                                    }
                                }
                            }
                            else if (row1.Length == 0)
                            {
                                break;
                            }
                            else
                            {
                                int i11 = 0;
                                foreach (DataRow dataRow in row1)
                                {
                                    if (i11 <= 3)
                                    {
                                        if (item["valid_price"].ToDecimal() >= ((1.5).ToDecimal() * (dataRow["sale_price"].ToDecimal())) || item["valid_price"].ToDecimal() <= ((0.5).ToDecimal() * (dataRow["sale_price"].ToDecimal())))
                                        {
                                            al.Add(item["item_name"]);
                                            break;
                                        }
                                    }
                                    i11++;
                                }
                            }
                        }
                    }
                    break;
            }
            return al;
        }
        /// <summary>
        /// 遍历ArrayList
        /// </summary>
        /// <param name="al"></param>
        /// <param name="type">0 价格差异的  1 数量为0  2 金额为0</param>
        /// <returns></returns>
     public  static  bool TraverseAlType(DataTable dc, int type)
        {
            bool t = true;
            ArrayList al = new ArrayList();
            string str = "";
            switch (type)
            {
                case 0:
                    al = arr(dc, 0);
                    if (al.Count <= 0)
                    {
                        break;
                    }
                    str = TraverseAl(al);
                    str += "的价格与最近三次的订单中的价格对比有异常,是否继续操作";
                    t = false;
                    YesNoForm yn = new YesNoForm(str);
                    if (yn.ShowDialog() == DialogResult.Yes)
                    {
                        t = true;
                    }
                    break;
                case 1:
                    al = arr(dc, 1);
                    if (al.Count <= 0)
                    {
                        break;
                    }
                    str = TraverseAl(al);
                    str += "的数量异常,无法保存";
                    MsgForm.ShowFrom(str);
                    t = false;
                    break;
                case 2:
                    al = arr(dc, 2);
                    if (al.Count <= 0)
                    {
                        break;
                    }
                    str = TraverseAl(al);
                    str += "的金额异常,无法保存";
                    t = false;
                    MsgForm.ShowFrom(str);
                    //YesNoForm yn1 = new YesNoForm(str);
                    //if (yn1.ShowDialog() == DialogResult.Yes)
                    //{
                    //    t = true;
                    //}
                    break;
                case 3:
                    al = arr(dc, 3);
                    if (al.Count <= 0)
                    {
                        break;
                    }
                    str = TraverseAl(al);
                    str += "的单价异常,无法保存";
                    t = false;
                    MsgForm.ShowFrom(str);
                    break;
                case 4:
                    al = arr(dc, 4);
                    if (al.Count <= 0)
                    {
                        break;
                    }
                    str = TraverseAl(al);
                    str += "的价格与最近三次的订单中的价格对比有异常,是否继续操作";
                    t = false;
                    YesNoForm yn1 = new YesNoForm(str);
                    if (yn1.ShowDialog() == DialogResult.Yes)
                    {
                        t = true;
                    }
                    break;
            }

            return t;
        }

     static   string TraverseAl(ArrayList al)
        {
            string str = "";
            for (int j = 0; j < al.Count; j++)
            {
                if (j == al.Count - 1)
                {
                    str += al[j];
                }
                else
                {
                    str += al[j] + ",";
                }
            }

            return str;

        }
    }
}
