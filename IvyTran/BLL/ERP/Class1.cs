using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.BLL.ERP
{
    public class Class1
    {
        void CGInSO(DateTime start, DateTime over, ArrayList str, string s)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            string sql = "";
            ArrayList al = new ArrayList();
            List<string> ls = new List<string>();
            if (s == "0")
            {
                foreach (var strs in str)
                {
                    if (string.IsNullOrEmpty(strs.ToString()))
                    {
                        break;
                    }

                    #region 数量、供应商都能对的上的

                    sql =
                        @"select a.flow_id,a.pick_barcode,b.oper_id,supcust_no,b.sup_no,a.sheet_no,b.p_sheet_no,a.batch_num,a.item_no,a.real_qty,order_qnty,oper_date
from [dbo].[co_t_order_child] a 
left join co_t_order_main b on a.sheet_no=b.sheet_no
where 
a.sheet_no like '" + strs + "' and b.p_sheet_no like 'CGDD%' " +
                        "and (batch_num is null or batch_num = '')";
                    DataTable dt11 = d.ExecuteToTable(sql, null);
                    foreach (DataRow item in dt11.Rows)
                    {
                        sql =
                            @"select flow_id,ph_sheet_no,pick_barcode,supcust_no,b.sup_no,a.sheet_no,b.p_sheet_no,a.batch_num,a.item_no,a.real_qty,order_qnty,oper_date
from [dbo].[co_t_order_child] a 
left join co_t_order_main b on a.sheet_no=b.sheet_no
where 
(a.sheet_no like 'PP%' or a.sheet_no like 'SS%')
and (batch_num is null or batch_num = '' or batch_num like '000%')
and a.item_no='" + item["item_no"].ToString() + "' and sup_no='" + item["supcust_no"].ToString() +
                            "'  and a.order_qnty='" + item["order_qnty"].ToString() +
                            "' and  CONVERT(varchar(10),oper_date,120) between '" +
                            item["oper_date"].ToDateTime().AddDays(-1).ToString("yyyy-MM-dd") + "' and '" + item["oper_date"].ToDateTime().ToString("yyyy-MM-dd") + "'";

                        DataTable dt = d.ExecuteToTable(sql, null);
                        if (dt.Rows.Count >= 1)
                        {
                            string flow_id = item["flow_id"].ToString();

                            string index = Index(flow_id);
                            string batch_num = CreateBatchNum(item["sup_no"].ToString(),
                                DateTime.Now.ToString("yyMMdd"), index);
                            string sql1 = @"update  co_t_order_child set voucher_no='" + flow_id +
                                          "',batch_num='" + batch_num + "',pick_barcode='" +
                                          dt.Rows[0]["pick_barcode"] + "' " +
                                          " where  flow_id='" + item["flow_id"] + "'\r\n " +
                                          "update co_t_order_main set ph_sheet_no='" + dt.Rows[0]["ph_sheet_no"] +
                                          "' where  sheet_no='" + item["sheet_no"].ToString() +
                                          "'" +
                                          " \r\n" +
                                          "update  co_t_order_child set voucher_no='" + item["sheet_no"].ToString() +
                                          "',batch_num='" + batch_num + "',supcust_no='" + item["sup_no"] + "'" +

                                          " where flow_id='" + dt.Rows[0]["flow_id"] + "'";
                            try
                            {
                                d.ExecuteToTable(sql1, null);
                            }
                            catch
                            {
                                throw new Exception("生成关联关系失败！");
                            }
                        }

                    }

                    #endregion

                    #region 合单的情况

                    sql =
                        @"select a.pick_barcode,flow_id,b.oper_id,supcust_no,b.sup_no,a.sheet_no,b.p_sheet_no,a.batch_num,a.item_no,a.real_qty,order_qnty,oper_date
from [dbo].[co_t_order_child] a 
left join co_t_order_main b on a.sheet_no=b.sheet_no
where 
a.sheet_no like '" + strs + "' and b.p_sheet_no like 'CGDD%' and (batch_num is null or batch_num = '' )";
                    dt11 = d.ExecuteToTable(sql, null);
                    foreach (DataRow item in dt11.Rows)
                    {
                        sql =
                            @"select flow_id,ph_sheet_no, pick_barcode, supcust_no, b.sup_no,a.sheet_no,b.p_sheet_no,a.batch_num,a.item_no,a.real_qty,order_qnty,oper_date
                        from[dbo].[co_t_order_child]
                        a
                            left join co_t_order_main b on a.sheet_no=b.sheet_no
                        where
                            (a.sheet_no like 'PP%' or a.sheet_no like 'SS%')
                        and(batch_num is null or batch_num = '' or batch_num like '000%')
                        and a.item_no='" + item["item_no"].ToString() + "' and sup_no = '"
                            + item["supcust_no"].ToString() +
                            "' and  CONVERT(varchar(10),oper_date,120) ='" +
                            item["oper_date"].ToDateTime().ToString("yyyy-MM-dd") + "'";
                        DataTable dd = d.ExecuteToTable(sql, null);
                        decimal sum = 0;
                        foreach (DataRow row in dd.Rows)
                        {
                            sum += row["order_qnty"].ToDecimal();
                        }

                        if (sum == item["order_qnty"].ToDecimal())
                        {
                            string index = Index(dd.Rows[0]["flow_id"].ToString());
                            string batch_num = CreateBatchNum(item["sup_no"].ToString(),
                                DateTime.Now.ToString("yyMMdd"), index);
                            foreach (DataRow row in dd.Rows)
                            {
                                string sql1 = @"update  co_t_order_child set voucher_no='" + item["sheet_no"] +
                                              "',batch_num='" + batch_num + "',supcust_no='" + item["sup_no"] + "' " +
                                              " where flow_id='" + row["flow_id"] + "'";

                                try
                                {
                                    d.ExecuteToTable(sql1, null);
                                }
                                catch
                                {
                                    throw new Exception("生成关联关系失败！");
                                }

                            }

                            sql = "update co_t_order_child set batch_num='" + batch_num + "',pick_barcode='" +
                                  dd.Rows[0]["pick_barcode"] + "' where flow_id='" + item["flow_id"] + "'";
                            try
                            {
                                d.ExecuteToTable(sql, null);
                            }
                            catch
                            {
                                throw new Exception("生成关联关系失败！");
                            }
                        }


                    }

                    #endregion

                    #region 采购订单未匹配到商品的

                    sql =
                        @"select a.pick_barcode,flow_id,b.oper_id,supcust_no,b.sup_no,a.sheet_no,b.p_sheet_no,a.batch_num,a.item_no,a.real_qty,order_qnty,oper_date
from [dbo].[co_t_order_child] a 
left join co_t_order_main b on a.sheet_no=b.sheet_no
where 
a.sheet_no like '" + strs + "' and b.p_sheet_no like 'CGDD%' and (batch_num is null or batch_num = '' or batch_num like '000%')";
                    dt11 = d.ExecuteToTable(sql, null);
                    foreach (DataRow item in dt11.Rows)
                    {
                        string index = Index(item["flow_id"].ToString());
                        string batch_num = CreateBatchNum(item["sup_no"].ToString(), DateTime.Now.ToString("yyMMdd"),
                            index);

                        sql = "update co_t_order_child set batch_num='" + batch_num + "' where flow_id='" + item["flow_id"] + "'";
                        try
                        {
                            d.ExecuteToTable(sql, null);
                        }
                        catch
                        {
                            throw new Exception("生成关联关系失败！");
                        }
                    }




                    #endregion



                }
                #region

                sql =
                    @"select flow_id,ph_sheet_no,pick_barcode,supcust_no,b.sup_no,a.sheet_no,b.p_sheet_no,a.batch_num,a.item_no,a.real_qty,order_qnty,oper_date
from [dbo].[co_t_order_child] a 
left join co_t_order_main b on a.sheet_no=b.sheet_no
where pick_barcode!='' and pick_barcode is not null and
(a.sheet_no like 'PP%'or a.sheet_no like 'SS%') " +
                    "" +
                    "and (batch_num is null or batch_num = '') and " +
                    "oper_date between '" + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 00:00:00.000") + "' and '" + DateTime.Now.ToString("yyyy-MM-dd 23:59:59.999") + "'";
                DataTable dt1 = d.ExecuteToTable(sql, null);
                if (dt1.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt1.Rows)
                    {
                        string flow_id = dataRow["flow_id"].ToString();
                        string index = Index(flow_id);
                        string batch_num = CreateBatchNum("000".ToString(),
                            Convert.ToDateTime(dataRow["oper_date"]).ToString("yyMMdd"), index);
                        sql =
                            @"select flow_id,supcust_no,b.sup_no,a.sheet_no
                            from[dbo].[co_t_order_child] a
                                left join co_t_order_main b on a.sheet_no = b.sheet_no 
								where 
								(supcust_no is not null and supcust_no !='' ) and 
								(a.sheet_no like 'SS%' or a.sheet_no like 'PP%') and a.item_no='" + dataRow["item_no"] + "'  order by flow_id desc";
                        string sup_no = "000";
                        DataTable ddd = d.ExecuteToTable(sql, null);
                        if (ddd.Rows.Count > 0)
                        {
                            sup_no = ddd.Rows[0]["supcust_no"].ToString();
                            batch_num = CreateBatchNum(sup_no,
                                Convert.ToDateTime(dataRow["oper_date"]).ToString("yyMMdd"), index);
                        }

                        sql = "update co_t_order_child set batch_num='" + batch_num + "',supcust_no='" + sup_no + "' where flow_id='" +
                              flow_id + "'";
                        try
                        {
                            d.ExecuteToTable(sql, null);
                        }
                        catch (Exception e)
                        {
                            throw new Exception("生成关联关系失败！");
                        }

                    }
                }

                #endregion
            }
        }
        string CreateBatchNum(string sup_str, string dt_str, string index)
        {
            return sup_str + "_" + dt_str + "_" + index;
        }
        string Index(string flow_id)
        {
            string index = "";
            if (flow_id.Length <= 4)
            {
                switch (flow_id.Length)
                {
                    case 1:
                        index = "000" + flow_id;
                        break;
                    case 2:
                        index = "00" + flow_id;
                        break;
                    case 3:
                        index = "0" + flow_id;
                        break;
                    case 4:
                        index = flow_id;
                        break;
                }
            }
            else
            {
                index = flow_id.Substring(flow_id.Length - 4, 4);
            }

            return index;
        }
    }
}