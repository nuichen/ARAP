using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.BLL.ERP
{
    public class MrpBLL : IMrpBLL
    {
        /// <summary>
        /// 查询客户商品绑定供应商列表
        /// </summary>
        /// <param name="ph_sheet_no">批次号</param>
        DataTable IMrpBLL.SearchMaterialList(string ph_sheet_no, string cust_no, string keyword, string only_show_nosup)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                string sql = "select a.*,b.sup_name,c.sup_name as cust_name from ot_temp_mrp_flow a ";
                sql += "left join bi_t_supcust_info b on a.sup_no=b.supcust_no and b.supcust_flag='S' ";
                sql += "left join bi_t_supcust_info c on a.cust_no=c.supcust_no and c.supcust_flag='C' ";
                sql += "where a.ph_sheet_no=@ph_sheet_no ";
                if (cust_no != "")
                {
                    sql += "and a.cust_no=@cust_no ";
                }
                if (keyword != "")
                {
                    sql += "and (a.item_no like '%'+@keyword+'%' or a.item_name like '%'+@keyword+'%') ";
                }
                //if (only_show_nosup == "1")
                //{
                //    sql += "and isnull(a.sup_no,'')='' ";
                //}
                sql += "order by a.flow_id ";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@ph_sheet_no", ph_sheet_no),
                    new System.Data.SqlClient.SqlParameter("@keyword", keyword),
                    new System.Data.SqlClient.SqlParameter("@cust_no", cust_no)
                };
                var dt = db.ExecuteToTable(sql, pars);

                return dt;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("MrpBLL.CalCgMaterial()", ex.ToString(), ph_sheet_no, cust_no);
                throw ex;
            }
        }

        /// <summary>
        /// 批次需求计划运算
        /// </summary>
        /// <param name="ph_sheet_no">批次号</param>
        void IMrpBLL.BindItemSup(string ph_sheet_no, string oper_id, DataTable lines)
        {
            var d = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB db = d;

            try
            {
                d.Open();
                d.BeginTran();
                string sql = "update ot_temp_mrp_flow set sup_no=@sup_no where ph_sheet_no=@ph_sheet_no and flow_id=@flow_id ";
                System.Data.SqlClient.SqlParameter[] pars;
                foreach (DataRow dr in lines.Rows)
                {
                    pars = new System.Data.SqlClient.SqlParameter[]
                    {
                        new System.Data.SqlClient.SqlParameter("@ph_sheet_no", ph_sheet_no),
                        new System.Data.SqlClient.SqlParameter("@sup_no", dr["sup_no"].ToString()),
                        new System.Data.SqlClient.SqlParameter("@flow_id", dr["flow_id"].ToString()),
                      //  new System.Data.SqlClient.SqlParameter("@is_pick", dr["is_pick"].ToString())
                    };
                    db.ExecuteScalar(sql, pars);
                }

                d.CommitTran();
            }
            catch (Exception ex)
            {
                d.RollBackTran();
                LogHelper.writeLog("MrpBLL.BindItemSup()", ex.ToString(), ph_sheet_no, oper_id);
                throw ex;
            }
            finally
            {
                d.Close();
            }
        }


        /// <summary>
        /// 批次需求计划运算
        /// </summary>
        /// <param name="ph_sheet_no">批次号</param>
        void IMrpBLL.DoMrp(string ph_sheet_no, string oper_id)
        {
            var d = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB db = d;

            try
            {
                d.Open();
                d.BeginTran();
                //清空临时表数据
                string sql = "delete from ot_temp_mrp_flow where ph_sheet_no=@ph_sheet_no ";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@ph_sheet_no", ph_sheet_no)
                };
                db.ExecuteScalar(sql, pars);

                //生成领料计划
                CalProcessTask(ph_sheet_no, db);

                //运算外购物料
                CalCgMaterial(ph_sheet_no, db);

                //运算自制需求物料
                CalProcessMaterial(ph_sheet_no, db);
                d.CommitTran();
            }
            catch (Exception ex)
            {
                d.RollBackTran();
                LogHelper.writeLog("MrpBLL.DoMrp()", ex.ToString(), ph_sheet_no, oper_id);
                throw ex;
            }
            finally
            {
                d.Close();
            }
        }

        /// <summary>
        /// 按客户要货明细生成采购订单
        /// </summary>
        /// <param name="ph_sheet_no">批次号</param>
        /// <param name="op_type">2:按差异数量生成采购订单;1:按要货数量生成采购订单</param>
        /// <param name="item_nos">选中需要采购的商品</param>
        /// <param name="is_min_stock">1:采购考虑安全库存</param>
        void IMrpBLL.CreateCgOrderByDtl(string ph_sheet_no, string op_type, string item_nos, string is_min_stock, string oper_id)
        {
            var d = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB db = d;

            try
            {
                d.Open();
                d.BeginTran();
                //判断是否已经运算
                string sql = "select count(*) as total from ot_temp_mrp_flow where ph_sheet_no=@ph_sheet_no ";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@ph_sheet_no", ph_sheet_no)
                };
                var dt = db.ExecuteToTable(sql, pars);
                if (dt.Rows.Count == 0 || Conv.ToInt(dt.Rows[0]["total"]) <= 0)
                {
                    throw new Exception("请先执行物料需求运算");
                }

                //判断是否存在未绑定供应商商品明细
                sql = "select count(*) as total from ot_temp_mrp_flow where ph_sheet_no=@ph_sheet_no and isnull(sup_no,'')='' ";
                pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@ph_sheet_no", ph_sheet_no)
                };
                dt = db.ExecuteToTable(sql, pars);
                if (dt.Rows.Count > 0 && Conv.ToInt(dt.Rows[0]["total"]) > 0)
                {
                    throw new Exception("存在物料未绑定供应商");
                }

                CreateCGOrder(ph_sheet_no, op_type, item_nos, is_min_stock, oper_id, db);
                d.CommitTran();
            }
            catch (Exception ex)
            {
                d.RollBackTran();
                LogHelper.writeLog("MrpBLL.CreateCgOrderByDtl()", ex.ToString(), ph_sheet_no, oper_id);
                throw ex;
            }
            finally
            {
                d.Close();
            }
        }

        /// <summary>
        /// 按要货明细汇总生成采购订单
        /// </summary>
        /// <param name="ph_sheet_no">批次号</param>
        /// <param name="op_type">2:按差异数量生成采购订单;1:按要货数量生成采购订单</param>
        /// <param name="item_nos">选中需要采购的商品</param>
        /// <param name="is_min_stock">1:采购考虑安全库存</param>
        void IMrpBLL.CreateCgOrderBySum(string ph_sheet_no, string op_type, string item_nos, string is_min_stock, string oper_id)
        {
            var d = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB db = d;

            try
            {
                d.Open();
                d.BeginTran();
                //判断是否已经运算
                string sql = "select count(*) as total from ot_temp_mrp_flow where ph_sheet_no=@ph_sheet_no ";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@ph_sheet_no", ph_sheet_no)
                };
                var dt = db.ExecuteToTable(sql, pars);
                if (dt.Rows.Count == 0 || Conv.ToInt(dt.Rows[0]["total"]) <= 0)
                {
                    throw new Exception("请先执行物料需求运算");
                }

                //判断是否存在未绑定供应商商品明细
                sql = "select count(*) as total from ot_temp_mrp_flow where ph_sheet_no=@ph_sheet_no and isnull(sup_no,'')='' ";
                pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@ph_sheet_no", ph_sheet_no)
                };
                dt = db.ExecuteToTable(sql, pars);
                if (dt.Rows.Count > 0 && Conv.ToInt(dt.Rows[0]["total"]) > 0)
                {
                    throw new Exception("存在物料未绑定供应商");
                }

                CreateCGOrder2(ph_sheet_no, op_type, item_nos, is_min_stock, oper_id, db);
                d.CommitTran();
            }
            catch (Exception ex)
            {
                d.RollBackTran();
                LogHelper.writeLog("MrpBLL.CreateCgOrderBySum()", ex.ToString(), ph_sheet_no, oper_id);
                throw ex;
            }
            finally
            {
                d.Close();
            }
        }

        /// <summary>
        /// 领料计划运算
        /// </summary>
        /// <param name="ph_sheet_no">批次号</param>
        void CalProcessTask(string ph_sheet_no, DB.IDB db)
        {
            try
            //string sql = "select t.pro_code,t.pro_qty,t.pro_unit,c.item_bom as pro_bom,d.item_no,d.unit_no,t.pro_qty*d.qty*(1+d.loss_rate) as need_qty,t.out_date ";
            //sql += "from (select min(convert(varchar(10),b.valid_date,120)) out_date,a.item_no as pro_code,sum(a.order_qnty) as pro_qty,a.unit_no as pro_unit ";
            //sql += "from co_t_order_child a ";
            //sql += "inner join co_t_order_main b on a.sheet_no=b.sheet_no ";
            //sql += "where b.approve_flag='1' and a.sheet_no in(select ph_sheet_no from ic_t_pspc_detail where sheet_no=@ph_sheet_no and sheet_type='SS') ";
            //sql += "group by a.item_no,a.unit_no) t ";
            //sql += "inner join bi_t_item_info c on t.pro_code=c.item_no ";
            //sql += "left join bi_t_bom_detail d on c.item_bom=d.bom_no ";
            //sql += "where isnull(c.is_mrp,'0')='1' and isnull(c.process_type,'0')='1' and c.item_bom is not null ";
            //sql += "order by t.pro_code,d.item_no ";
            {
                //领料计划
                string sql = "select t.pro_code,t.pro_qty,t.pro_unit,c.item_bom as pro_bom,d.item_no,d.unit_no,t.pro_qty*d.qty*(1+d.loss_rate) as need_qty,t.out_date ";
                sql += "from (select min(convert(varchar(10),b.valid_date,120)) out_date,a.item_no as pro_code,sum(a.order_qnty) as pro_qty,a.unit_no as pro_unit ";
                sql += "from co_t_order_child a ";
                sql += "inner join co_t_order_main b on a.sheet_no=b.sheet_no ";
                sql += "where b.approve_flag='1' and a.sheet_no in(select ph_sheet_no from ic_t_pspc_detail where sheet_no=@ph_sheet_no and sheet_type='SS') ";
                sql += "group by a.item_no,a.unit_no) t ";
                sql += "inner join bi_t_item_info c on t.pro_code=c.item_no ";
                sql += "left join bi_t_bom_detail d on c.item_bom=d.bom_no ";
                sql += "where isnull(c.is_mrp,'0')='1' and isnull(c.process_type,'0')='1' and c.item_bom is not null ";
                sql += "order by t.pro_code,d.item_no ";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@ph_sheet_no", ph_sheet_no)
                };
                var dt = db.ExecuteToTable(sql, pars);

                sql = "select * from dbo.ot_processing_task where ph_sheet_no=@ph_sheet_no order by pro_code,item_no ";
                pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@ph_sheet_no", ph_sheet_no)
                };
                var tdt = db.ExecuteToTable(sql, pars);
                if (tdt.Rows.Count == 0)
                {
                    sql = "insert into ot_processing_task(flow_id,ph_sheet_no,pro_code,pro_qty,pro_unit,pro_bom,item_no,unit_no,need_qty,out_date,create_time) ";
                    sql += "select isnull(max(flow_id),0)+1,@ph_sheet_no,@pro_code,@pro_qty,@pro_unit,@pro_bom,@item_no,@unit_no,@need_qty,@out_date,getdate() ";
                    sql += "from ot_processing_task ";
                    foreach (DataRow dr in dt.Rows)
                    {
                        pars = new System.Data.SqlClient.SqlParameter[]
                        {
                            new System.Data.SqlClient.SqlParameter("@ph_sheet_no", ph_sheet_no),
                            new System.Data.SqlClient.SqlParameter("@pro_code", dr["pro_code"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@pro_qty", dr["pro_qty"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@pro_unit", dr["pro_unit"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@pro_bom", dr["pro_bom"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@item_no", dr["item_no"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@unit_no", dr["unit_no"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@need_qty", dr["need_qty"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@out_date", dr["out_date"].ToString())
                        };
                        db.ExecuteScalar(sql, pars);
                    }
                }
                else
                {
                    string in_sql = "insert into ot_processing_task(flow_id,ph_sheet_no,pro_code,pro_qty,pro_unit,pro_bom,item_no,unit_no,need_qty,out_date,create_time) ";
                    in_sql += "select isnull(max(flow_id),0)+1,@ph_sheet_no,@pro_code,@pro_qty,@pro_unit,@pro_bom,@item_no,@unit_no,@need_qty,@out_date,getdate() ";
                    in_sql += "from ot_processing_task ";

                    string upd_sql = "update ot_processing_task set pro_qty=@pro_qty,need_qty=@need_qty,create_time=getdate() where flow_id=@flow_id ";
                    foreach (DataRow dr in dt.Rows)
                    {
                        var tempdr = tdt.Select("pro_code='" + dr["pro_code"].ToString() + "' and item_no='" + dr["item_no"].ToString() + "' ");
                        if (tempdr.Length > 0)
                        {
                            pars = new System.Data.SqlClient.SqlParameter[]
                            {
                                new System.Data.SqlClient.SqlParameter("@pro_qty", tempdr[0]["pro_qty"].ToString()),
                                new System.Data.SqlClient.SqlParameter("@need_qty", tempdr[0]["need_qty"].ToString()),
                                new System.Data.SqlClient.SqlParameter("@flow_id", tempdr[0]["flow_id"].ToString())
                            };
                            db.ExecuteScalar(upd_sql, pars);
                        }
                        else
                        {
                            pars = new System.Data.SqlClient.SqlParameter[]
                            {
                                new System.Data.SqlClient.SqlParameter("@ph_sheet_no", ph_sheet_no),
                                new System.Data.SqlClient.SqlParameter("@pro_code", dr["pro_code"].ToString()),
                                new System.Data.SqlClient.SqlParameter("@pro_qty", dr["pro_qty"].ToString()),
                                new System.Data.SqlClient.SqlParameter("@pro_unit", dr["pro_unit"].ToString()),
                                new System.Data.SqlClient.SqlParameter("@pro_bom", dr["pro_bom"].ToString()),
                                new System.Data.SqlClient.SqlParameter("@item_no", dr["item_no"].ToString()),
                                new System.Data.SqlClient.SqlParameter("@unit_no", dr["unit_no"].ToString()),
                                new System.Data.SqlClient.SqlParameter("@need_qty", dr["need_qty"].ToString()),
                                new System.Data.SqlClient.SqlParameter("@out_date", dr["out_date"].ToString())
                            };
                            db.ExecuteScalar(in_sql, pars);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LogHelper.writeLog("MrpBLL.CalProcessTask()", ex.ToString(), ph_sheet_no);
                throw ex;
            }
        }

        /// <summary>
        /// 外购物料运算，只运算is_mrp='0'
        /// 20200202修改：物料供应商由原来商品档案默认供应商改为客户商品供应商配对表的供应商
        /// </summary>
        /// <param name="ph_sheet_no">批次号</param>
        void CalCgMaterial(string ph_sheet_no, DB.IDB db)
        {
            try
            {
                //外购物料需求
                string sql = @"select a.flow_id,a.item_no,a.unit_no,a.unit_factor,a.in_price as price
,a.order_qnty,a.sub_amount,a.pick_barcode,b.sup_no as cust_no,m.show_num
,b.valid_date,f1.supcust_no as sup_no,c.item_name,c.barcode,c.item_loss 
from co_t_order_child a inner join co_t_order_main b on a.sheet_no=b.sheet_no 
inner join bi_t_item_info c on a.item_no=c.item_no 
left join bi_t_supcust_info f1 on c.sup_no=f1.supcust_no  and f1.supcust_flag='C'
left join (select n.sup_no as cust_no,m.item_no,m.sup_no from rp_t_cust_item_detail m 
inner join rp_t_cust_item_master n on m.sheet_no=n.sheet_no 
where isnull(m.display_flag,'1')='1' and n.approve_flag='1') as t 
on b.sup_no=t.cust_no and a.item_no=t.item_no 
left join (select supcust_no,isnull(show_num,supcust_no) show_num 
from bi_t_supcust_info where supcust_flag='C') m on m.supcust_no=b.sup_no 
where b.approve_flag='1' and isnull(c.is_mrp,'0')='1'";
                sql += "and a.sheet_no in(select ph_sheet_no from ic_t_pspc_detail where sheet_no=@ph_sheet_no and sheet_type='SS') ";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@ph_sheet_no", ph_sheet_no)
                };
                var dt = db.ExecuteToTable(sql, pars);
                if (dt.Rows.Count > 0)
                {
                    sql = "insert into ot_temp_mrp_flow(ph_sheet_no,sup_no,item_no,item_name,barcode,unit_no,unit_factor,price,cust_no,show_num";
                    sql += ",order_qnty,cg_qty,out_date,create_time,pick_barcode,cg_rate,co_flow_id) values(@ph_sheet_no,@sup_no,@item_no,@item_name";
                    sql += ",@barcode,@unit_no,@unit_factor,@price,@cust_no,@show_num,@order_qnty,@cg_qty,@out_date,getdate(),@pick_barcode,@cg_rate,@co_flow_id) ";
                    foreach (DataRow dr in dt.Rows)
                    {
                        pars = new System.Data.SqlClient.SqlParameter[]
                        {
                            new System.Data.SqlClient.SqlParameter("@ph_sheet_no", ph_sheet_no),
                            new System.Data.SqlClient.SqlParameter("@sup_no", dr["sup_no"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@item_no", dr["item_no"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@item_name", dr["item_name"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@barcode", dr["barcode"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@unit_no", dr["unit_no"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@unit_factor", dr["unit_factor"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@price", dr["price"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@cust_no", dr["cust_no"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@show_num", dr["show_num"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@order_qnty", dr["order_qnty"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@cg_qty", Conv.ToDecimal(dr["order_qnty"])*(1+Conv.ToDecimal(dr["item_loss"]))),
                            new System.Data.SqlClient.SqlParameter("@out_date", dr["valid_date"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@pick_barcode", dr["pick_barcode"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@cg_rate", dr["item_loss"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@co_flow_id", dr["flow_id"].ToString())
                        };
                        db.ExecuteScalar(sql, pars);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("MrpBLL.CalCgMaterial()", ex.ToString(), ph_sheet_no);
                throw ex;
            }
        }

        /// <summary>
        /// 自制物料运算,只运算is_mrp='1'的多进A出加工类型
        /// 20200202修改：物料供应商由原来商品档案默认供应商改为客户商品供应商配对表的供应商
        /// </summary>
        /// <param name="ph_sheet_no">批次号</param>
        void CalProcessMaterial(string ph_sheet_no, DB.IDB db)
        {
            try
            {
                //外购物料需求
                string sql = "select a.flow_id,d.item_no,d.unit_no,1 unit_factor,e.price,a.order_qnty*d.qty*(1+d.loss_rate) as order_qnty,'1001' pick_barcode";
                sql += ",b.sup_no as cust_no,m.show_num,b.valid_date,t.sup_no,e.item_name,e.barcode,e.item_loss ";
                sql += "from co_t_order_child a ";
                sql += "inner join co_t_order_main b on a.sheet_no=b.sheet_no ";
                sql += "inner join bi_t_item_info c on a.item_no=c.item_no ";
                sql += "left join (select supcust_no,isnull(show_num,supcust_no) show_num from bi_t_supcust_info where supcust_flag='C') m on m.supcust_no=b.sup_no ";
                sql += "left join bi_t_bom_detail d on c.item_bom=d.bom_no ";
                sql += "left join bi_t_item_info e on d.item_no=e.item_no ";
                sql += "left join (select n.sup_no as cust_no,m.item_no,m.sup_no from rp_t_cust_item_detail m inner join rp_t_cust_item_master n on m.sheet_no=n.sheet_no where isnull(m.display_flag,'1')='1' and n.approve_flag='1') as t on b.sup_no=t.cust_no and d.item_no=t.item_no ";
                sql += "where b.approve_flag='1' and isnull(c.is_mrp,'0')='1' and isnull(c.process_type,'0')='1' and c.item_bom is not null ";
                sql += "and a.sheet_no in(select ph_sheet_no from ic_t_pspc_detail where sheet_no=@ph_sheet_no and sheet_type='SS') ";

                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@ph_sheet_no", ph_sheet_no)
                };
                var dt = db.ExecuteToTable(sql, pars);
                if (dt.Rows.Count > 0)
                {
                    sql = "insert into ot_temp_mrp_flow(ph_sheet_no,sup_no,item_no,item_name,barcode,unit_no,unit_factor,price,cust_no,show_num";
                    sql += ",order_qnty,cg_qty,out_date,create_time,pick_barcode,cg_rate,co_flow_id) values(@ph_sheet_no,@sup_no,@item_no,@item_name";
                    sql += ",@barcode,@unit_no,@unit_factor,@price,@cust_no,@show_num,@order_qnty,@cg_qty,@out_date,getdate(),@pick_barcode,@cg_rate,@co_flow_id) ";
                    foreach (DataRow dr in dt.Rows)
                    {
                        pars = new System.Data.SqlClient.SqlParameter[]
                        {
                            new System.Data.SqlClient.SqlParameter("@ph_sheet_no", ph_sheet_no),
                            new System.Data.SqlClient.SqlParameter("@sup_no", dr["sup_no"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@item_no", dr["item_no"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@item_name", dr["item_name"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@barcode", dr["barcode"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@unit_no", dr["unit_no"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@unit_factor", dr["unit_factor"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@price", dr["price"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@cust_no", dr["cust_no"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@show_num", dr["show_num"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@order_qnty", dr["order_qnty"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@cg_qty", Conv.ToDecimal(dr["order_qnty"])*(1+Conv.ToDecimal(dr["item_loss"]))),
                            new System.Data.SqlClient.SqlParameter("@out_date", dr["valid_date"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@pick_barcode", dr["pick_barcode"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@cg_rate", dr["item_loss"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@co_flow_id", dr["flow_id"].ToString())
                        };
                        db.ExecuteScalar(sql, pars);
                        
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("MrpBLL.CalProcessMaterial()", ex.ToString(), ph_sheet_no);
                throw ex;
            }
        }
        string BatchNum(string sheet_no, string item_no)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            string sql = "select batch_num from co_t_order_child where sheet+no='" + sheet_no + "' and item_no='" + item_no + "'";
            return Helper.Conv.ToString(d.ExecuteScalar(sql, null));
        }
        string BatchNum(string sup_no, DB.IDB d)
        { 
            string sql = "select isnull(max(batch_num),0) from co_t_order_child";
            
            string batch_num = Helper.Conv.ToString(d.ExecuteScalar(sql, null));
            DateTime xin=new DateTime();
            int a = 0;
            if (int.TryParse(batch_num, out a) == false)
            {
                return sup_no + DateTime.Now.ToString("yyyyMMdd") + "0001";
            }
            else
            {
                string str = batch_num.Substring(batch_num.Length - 13, 8);
                xin = (str.Substring(0, 4) + "-" + str.Substring(4, 2) + "-" + str.Substring(6, 2)).ToDateTime();
            }
            if (batch_num == "0")
            {
                return sup_no + DateTime.Now.ToString("yyyyMMdd") + "0001";
            }
            else if (DateTime.Compare(xin, DateTime.Now.ToString("yyyy-MM-dd").ToDateTime()) < 0)
            {
                return sup_no + DateTime.Now.ToString("yyyyMMdd") + "0001";
            }
            else if (DateTime.Compare(xin, DateTime.Now.ToString("yyyy-MM-dd").ToDateTime()) == 0)
            {
                int num = Convert.ToInt32(batch_num.Substring(batch_num.Length - 5, 4)) + 1;
                return sup_no + DateTime.Now.ToString("yyyyMMdd") + num.ToString();
            }
            else
            {
                return "";
            }
            //else
            //{
            //    return batch_num
            //}
        }
        /// <summary>
        /// 按供应商生成采购订单
        /// 生成思路：获取批次内所有批销单，循环乘损耗率，得到最终要货数
        /// 取库存数量（如果按库存指标，就直接减去库存指标）
        /// 如果要按差异生成，判断库存>要货 就不生成采购
        /// </summary>
        void CreateCGOrder(string ph_sheet_no, string op_type, string item_nos, string is_min_stock, string oper_id, DB.IDB db)
        {
            try
            {
                
                if (item_nos.Length > 0) item_nos = "'" + item_nos.Replace(",", "','") + "'";
                string sql = "select a.* from ot_temp_mrp_flow a ";
                sql += "where a.ph_sheet_no=@ph_sheet_no ";
                if (item_nos.Length > 0)
                {
                    sql += "and a.item_no in(" + item_nos + ") ";
                }
                sql += "order by a.sup_no,a.item_no,a.cust_no ";

                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@ph_sheet_no", ph_sheet_no)
                };
                var dt = db.ExecuteToTable(sql, pars);

                if (dt.Rows.Count > 0)
                {
                    sql = "select item_no from bi_t_item_info where isnull(is_self_cg,'0')='1'";
                    var tmp_dt = db.ExecuteToTable(sql, null);
                    Dictionary<string, string> item_dic = new Dictionary<string, string>();
                    foreach (DataRow dr in tmp_dt.Rows)
                    {
                        item_dic.Add(dr["item_no"].ToString(), dr["item_no"].ToString());
                    }
                    StringBuilder sb = new StringBuilder();
                    StringBuilder sb2 = new StringBuilder();
                    int index2 = 0;
                    int index3 = 0;
                    Dictionary<string, decimal> stockDic = GetStockDic(ph_sheet_no, is_min_stock);
                    ClearCgDetail(ph_sheet_no, db);
                    
                    var groupBy = dt.AsEnumerable().GroupBy(s => s["sup_no"].ToString());
                    var dt_str = DateTime.Now.ToString("yyMMdd");
                    string str1= "";
                    List<body.tmp_order_child> flow_lst = new List<body.tmp_order_child>();
                    foreach (var g in groupBy)
                    {
                        List<Model.co_t_order_child> lines = new List<Model.co_t_order_child>();
                        Model.co_t_order_main ord = new Model.co_t_order_main
                        {
                            sheet_no = MaxCode(db, "PO"),
                            
                            p_sheet_no = "",
                            ph_sheet_no = ph_sheet_no,
                            sup_no = g.First()["sup_no"].ToString(),
                            order_man = "1001",
                            oper_id = oper_id,
                            valid_date = Conv.ToDateTime(g.First()["out_date"]),
                            oper_date = DateTime.Now,
                            update_time = DateTime.Now,
                            branch_no = "0001",
                            coin_code = "RMB",
                            paid_amount = 0,
                            trans_no = "P",
                            order_status = "0",
                            sale_way = "A",
                            approve_flag = "0",
                            other1 = "",
                            cm_branch = "00",
                            approve_man = "",
                            approve_date = System.DateTime.MinValue,
                            num1 = 0,
                            num2 = 0,
                            num3 = 0,
                            memo = ""
                        };
                        
                        //po += "'" + ord.sheet_no + "',";
                        //ss += "'" + ord.sheet_no + "',";
                        int index = 0;
                        decimal total_amt = 0;
                        DataTable d = db.ExecuteToTable(sql, null);
                        
                        foreach (var row in g)
                        {
                            ++index;
                            Model.co_t_order_child line = new Model.co_t_order_child();
                            line.sheet_no = ord.sheet_no;
                            line.item_no = row["item_no"].ToString();
                            line.unit_no = row["unit_no"].ToString();
                            line.barcode = row["barcode"].ToString();
                            line.pick_barcode = row["pick_barcode"].ToString();
                            line.supcust_no = row["cust_no"].ToString();
                            line.unit_factor = Conv.ToDecimal(row["unit_factor"]);
                            line.discount = 1;
                            line.in_price = Conv.ToDecimal(row["price"]);
                            decimal stock_qty = 0;
                            if (!stockDic.TryGetValue(line.item_no, out stock_qty))
                            {
                                stock_qty = 0;
                            }
                            decimal cg_rate = 1 + Conv.ToDecimal(row["cg_rate"]);
                            decimal qty = Math.Abs(Conv.ToDecimal(row["order_qnty"])) * cg_rate;
                            line.order_qnty = qty;
                            
                            var batch_num = CreateBatchNum(ord.sup_no, dt_str, row["flow_id"].ToString());
                            // line.batch_num = batch_num;
                            flow_lst.Add(new body.tmp_order_child { flow_id=row["co_flow_id"].ToString(), sup_no=ord.sup_no, batch_num=batch_num });
                            if ("2".Equals(op_type))
                            {
                                //判断要货数量跟库存的差异
                                if (stock_qty >= line.order_qnty)
                                {
                                    //不需要生成采购
                                    stockDic[line.item_no] = stock_qty - line.order_qnty;
                                    continue;
                                }
                                else
                                {
                                    line.order_qnty -= stock_qty;
                                    stockDic[line.item_no] = 0;
                                }
                            }


                            line.batch_num = batch_num;
                            line.sub_amount = line.in_price * line.order_qnty;
                            line.discount = 1;
                            line.other1 = "";
                            line.other2 = "";
                            line.voucher_no = "";
                            line.sheet_sort = index;
                            line.num1 = 0;
                            line.num2 = 0;
                            line.num3 = 0;
                            line.packqty = 0;
                            line.sgqty = 0;
                            DataRow[] dr= dt.Select($"item_no='{line.item_no}' and sup_no='{line.supcust_no}'");
                            foreach (var dataRow in dr)
                            {
                                sql = $@"update co_t_order_child set batch_num='" + batch_num + $"',supcust_no='{line.supcust_no}' " +
                                      "where flow_id='" + dataRow["co_flow_id"].ToString() + "'";
                                db.ExecuteScalar(sql, null);
                            }
                            
                            //if (db.ExecuteToTable(sql1, null).Rows.Count <= 0)
                            //{
                            //    throw new Exception("出错了");
                            //}
                            //try
                            //{
                            //    db.ExecuteScalar(sql1, null);
                            
                            //}
                            //catch
                            //{
                            //    throw new Exception("出错了");
                            //}
                            total_amt += line.order_qnty * line.in_price;
                            lines.Add(line);
                            //写采购助手明细表
                            var tmp_item_no = "";
                            if (item_dic.TryGetValue(line.item_no, out tmp_item_no))
                            {
                                sb.Append("insert into co_t_cg_order_detail(ph_sheet_no,sup_no,item_no,item_name,barcode,unit_no,unit_factor,price,cust_no,show_num,order_qnty,cg_qty,create_time) ");
                                sb.Append("values(");
                                sb.Append("'" + ph_sheet_no + "'");
                                sb.Append(",'" + ord.sup_no + "'");
                                sb.Append(",'" + line.item_no + "'");
                                sb.Append(",'" + row["item_name"].ToString() + "'");
                                sb.Append(",'" + row["pick_barcode"].ToString() + "'");
                                sb.Append(",'" + line.unit_no + "'");
                                sb.Append(",'" + line.unit_factor + "'");
                                sb.Append(",'" + line.in_price + "'");
                                sb.Append(",'" + row["cust_no"].ToString() + "'");
                                sb.Append(",'" + row["show_num"].ToString() + "'");
                                sb.Append(",'" + row["order_qnty"].ToString() + "'");
                                sb.Append(",'" + line.order_qnty + "'");
                                sb.Append(",getdate()");
                                sb.Append(");\r\n");
                                ++index2;
                            }
                        }
                        ord.total_amount = total_amt;
                        //创建采购订单
                        if (lines.Count > 0)
                        {
                            db.Insert(ord);
                            foreach (Model.co_t_order_child line in lines)
                            {
                                line.sheet_no = ord.sheet_no;
                                db.Insert(line, "flow_id");
                            }
                            lines.Clear();
                        }

                        if (index2 > 3000)
                        {
                            db.ExecuteScalar(sb.ToString(), null);
                            sb.Clear();
                            index2 = 0;
                        }
                        //写配送批次与采购订单关联表
                        sb2.Append("insert into ic_t_pspc_detail(sheet_no,ph_sheet_no,sheet_type) ");
                        sb2.Append("values(");
                        sb2.Append("'" + ph_sheet_no + "'");
                        sb2.Append(",'" + ord.sheet_no + "'");
                        sb2.Append(",'PO'");
                        sb2.Append(");\r\n");
                        ++index3;

                        if (index3 > 3000)
                        {
                            db.ExecuteScalar(sb2.ToString(), null);
                            sb2.Clear();
                            index3 = 0;
                        }
                    }

                    if (sb.Length > 0)
                    {
                        db.ExecuteScalar(sb.ToString(), null);
                        sb.Clear();
                        index2 = 0;
                    }

                    if (sb2.Length > 0)
                    {
                        db.ExecuteScalar(sb2.ToString(), null);
                        sb2.Clear();
                        index3 = 0;
                    }

                    if (flow_lst.Count > 0)
                    {
                        //sb = new StringBuilder();
                        //index2 = 0;
                        //foreach(var item in flow_lst)
                        //{
                        //    sb.Append("update co_t_order_child set batch_num='" + item.batch_num +"',supcust_no='" + item.sup_no + "' where flow_id='" + item.flow_id +"';\r\n");
                        //    ++index2;
                        //    if (index2 > 5000)
                        //    {
                        //        LogHelper.writeLog("CreateCGOrder(1)", sb.ToString());
                        //        db.ExecuteScalar(sb.ToString(), null);
                        //        sb.Clear();
                        //        index2 = 0;
                        //    }
                        //}
                        //if (sb.Length > 0)
                        //{
                        //    LogHelper.writeLog("CreateCGOrder(2)", sb.ToString());
                        //    db.ExecuteScalar(sb.ToString(), null);
                        //    sb.Clear();
                        //    index2 = 0;
                        //}

                    }
                    IInOutBLL bll=new InOutBLL();
                   // bll.CGInSO(ss,po);
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("CreateCGOrder()", ex.ToString(), ph_sheet_no);
                throw ex;
            }
        }
        /// <summary>
        /// 改变采购订单
        /// </summary>
        void ChengeSheet(DB.IDB db)
        {
            string sql =
                @"select a.sheet_no,b.oper_date from co_t_order_child a left join  
co_t_order_main b on a.sheet_no=b.sheet_no 
where a.sheet_no like 'SS%' and b.p_sheet_no like 'XSDD%'";
          DataTable dt=  db.ExecuteToTable(sql, null);
          foreach (DataRow item in dt.Rows)
          {
              sql = @"";
          }


        }

        /// <summary>
        /// 按供应商生成采购订单
        /// 生成思路：获取批次内所有批销单，循环乘损耗率，得到最终要货数
        /// 取库存数量（如果按库存指标，就直接减去库存指标）
        /// 如果要按差异生成，判断库存>要货 就不生成采购
        /// </summary>
        void CreateCGOrder2(string ph_sheet_no, string op_type, string item_nos, string is_min_stock, string oper_id, DB.IDB db)
        {
            try
            {
                if (item_nos.Length > 0) item_nos = "'" + item_nos.Replace(",", "','") + "'";
                string sql = "select ph_sheet_no,min(out_date) out_date,sup_no,item_no,item_name,barcode,unit_no,min(unit_factor) unit_factor";
                sql += ",max(price) price,sum(order_qnty) order_qnty,sum(cg_qty) cg_qty,max(cg_rate) cg_rate ";
                sql += "from ot_temp_mrp_flow a ";
                sql += "where a.ph_sheet_no=@ph_sheet_no ";
                if (item_nos.Length > 0)
                {
                    sql += "and a.item_no in(" + item_nos + ") ";
                }
                sql += "group by a.ph_sheet_no,a.sup_no,a.item_no,a.item_name,a.barcode,a.unit_no ";
                sql += "order by a.sup_no,a.item_no ";

                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@ph_sheet_no", ph_sheet_no)
                };
                var dt = db.ExecuteToTable(sql, pars);

                if (dt.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    StringBuilder sb2 = new StringBuilder();
                    int index2 = 0;
                    int index3 = 0;
                    Dictionary<string, decimal> stockDic = GetStockDic(ph_sheet_no, is_min_stock);
                    ClearCgDetail(ph_sheet_no, db);
                    var groupBy = dt.AsEnumerable().GroupBy(s => s["sup_no"].ToString());
                    var dt_str = DateTime.Now.ToString("yyMMdd");
                    List<body.tmp_order_child> flow_lst = new List<body.tmp_order_child>();
                    foreach (var g in groupBy)
                    {
                        sql = "select item_no from bi_t_item_info where isnull(is_self_cg,'0')='1'";
                        var tmp_dt = db.ExecuteToTable(sql, null);
                        Dictionary<string, string> item_dic = new Dictionary<string, string>();
                        foreach (DataRow dr in tmp_dt.Rows)
                        {
                            item_dic.Add(dr["item_no"].ToString(), dr["item_no"].ToString());
                        }
                        List<Model.co_t_order_child> lines = new List<Model.co_t_order_child>();
                        Model.co_t_order_main ord = new Model.co_t_order_main
                        {
                            sheet_no = MaxCode(db, "PO"),
                            p_sheet_no = "",
                            ph_sheet_no = ph_sheet_no,
                            sup_no = g.First()["sup_no"].ToString(),
                            order_man = "1001",
                            oper_id = oper_id,
                            valid_date = Conv.ToDateTime(g.First()["out_date"]),
                            oper_date = DateTime.Now,
                            update_time = DateTime.Now,
                            branch_no = "0001",
                            coin_code = "RMB",
                            paid_amount = 0,
                            trans_no = "P",
                            order_status = "0",
                            sale_way = "A",
                            approve_flag = "0",
                            other1 = "",
                            cm_branch = "00",
                            approve_man = "",
                            approve_date = System.DateTime.MinValue,
                            num1 = 0,
                            num2 = 0,
                            num3 = 0,
                            memo = ""
                        };

                        int index = 0;
                        decimal total_amt = 0;
                        foreach (var row in g)
                        {
                            ++index;
                            Model.co_t_order_child line = new Model.co_t_order_child();
                            line.sheet_no = ord.sheet_no;
                            line.item_no = row["item_no"].ToString();
                            line.unit_no = row["unit_no"].ToString();
                            line.barcode = row["barcode"].ToString();
                            line.unit_factor = Conv.ToDecimal(row["unit_factor"]);
                            line.discount = 1;
                            line.in_price = Conv.ToDecimal(row["price"]);

                            decimal stock_qty = 0;
                            if (!stockDic.TryGetValue(line.item_no, out stock_qty))
                            {
                                stock_qty = 0;
                            }
                            decimal cg_rate = 1 + Conv.ToDecimal(row["cg_rate"]);
                            decimal qty = Math.Abs(Conv.ToDecimal(row["order_qnty"])) * cg_rate;
                            line.order_qnty = qty;
                            var batch_num = CreateBatchNum(ord.sup_no, dt_str, index.ToString());
                            flow_lst.Add(new body.tmp_order_child { item_no= row["item_no"].ToString(), sup_no=ord.sup_no, batch_num=batch_num });
                            if ("2".Equals(op_type))
                            {
                                //判断要货数量跟库存的差异
                                if (stock_qty >= line.order_qnty)
                                {
                                    //不需要生成采购
                                    continue;
                                }
                                else
                                {
                                    line.order_qnty -= stock_qty;
                                    stockDic[line.item_no] = 0;
                                }
                            }

                            line.sub_amount = line.in_price * line.order_qnty;
                            line.discount = 1;
                            line.other1 = "";
                            line.other2 = "";
                            line.voucher_no = "";
                            line.sheet_sort = index;
                            line.num1 = 0;
                            line.num2 = 0;
                            line.num3 = 0;
                            line.packqty = 0;
                            line.sgqty = 0;
                            line.batch_num = batch_num;
                            total_amt += line.order_qnty * line.in_price;
                            lines.Add(line);
                            
                            //写采购助手明细表
                            var tmp_item_no = "";
                            if (item_dic.TryGetValue(line.item_no, out tmp_item_no))
                            {
                                sb.Append("insert into co_t_cg_order_detail(ph_sheet_no,sup_no,item_no,item_name,barcode,unit_no,unit_factor,price,order_qnty,cg_qty,create_time) ");
                                sb.Append("values(");
                                sb.Append("'" + ph_sheet_no + "'");
                                sb.Append(",'" + ord.sup_no + "'");
                                sb.Append(",'" + line.item_no + "'");
                                sb.Append(",'" + row["item_name"].ToString() + "'");
                                sb.Append(",'" + row["barcode"].ToString() + "'");
                                sb.Append(",'" + line.unit_no + "'");
                                sb.Append(",'" + line.unit_factor + "'");
                                sb.Append(",'" + line.in_price + "'");
                                sb.Append(",'" + row["order_qnty"].ToString() + "'");
                                sb.Append(",'" + line.order_qnty + "'");
                                sb.Append(",getdate()");
                                sb.Append(");\r\n");
                                ++index2;
                            }
                        }
                        ord.total_amount = total_amt;

                        if (lines.Count > 0)
                        {
                            db.Insert(ord);
                            foreach (Model.co_t_order_child line in lines)
                            {
                                line.sheet_no = ord.sheet_no;
                                db.Insert(line, "flow_id");
                            }
                            lines.Clear();
                        }

                        if (index2 > 3000)
                        {
                            db.ExecuteScalar(sb.ToString(), null);
                            sb.Clear();
                            index2 = 0;
                        }
                        //写配送批次与采购订单关联表
                        sb2.Append("insert into ic_t_pspc_detail(sheet_no,ph_sheet_no,sheet_type) ");
                        sb2.Append("values(");
                        sb2.Append("'" + ph_sheet_no + "'");
                        sb2.Append(",'" + ord.sheet_no + "'");
                        sb2.Append(",'PO'");
                        sb2.Append(");\r\n");
                        ++index3;

                        if (index3 > 3000)
                        {
                            db.ExecuteScalar(sb2.ToString(), null);
                            sb2.Clear();
                            index3 = 0;
                        }
                    }

                    if (sb.Length > 0)
                    {
                        db.ExecuteScalar(sb.ToString(), null);
                        sb.Clear();
                        index2 = 0;
                    }
                    //更新销售订单
                    if (flow_lst.Count > 0)
                    {
                        sb = new StringBuilder();
                        index2 = 0;
                        foreach (var item in flow_lst)
                        {
                            sb.Append("update co_t_order_child set batch_num='" + item.batch_num + "',supcust_no='" + item.sup_no + "' ");
                            sb.Append("where item_no='" + item.item_no + "' and sheet_no in(select sheet_no from co_t_order_main where ph_sheet_no='" + ph_sheet_no + "' and trans_no='S');\r\n");
                            ++index2;
                            if (index2 > 3000)
                            {
                                LogHelper.writeLog("CreateCGOrder2(1)", sb.ToString());
                                db.ExecuteScalar(sb.ToString(), null);
                                sb.Clear();
                                index2 = 0;
                            }
                        }
                        if (sb.Length > 0)
                        {
                            LogHelper.writeLog("CreateCGOrder2(2)", sb.ToString());
                            db.ExecuteScalar(sb.ToString(), null);
                            sb.Clear();
                            index2 = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("CreateCGOrder2()", ex.ToString(),ph_sheet_no);
                throw ex;
            }
        }

        string MaxCode(DB.IDB db, string sheet_type)
        {
            string sql = "select sheet_value from sys_t_sheet_no where sheet_id='" + sheet_type + "'";

            object obj = db.ExecuteScalar(sql, null);
            if (obj == null || obj == DBNull.Value)
            {
                return "";
            }
            else
            {
                int index = Conv.ToInt(obj);
                if (index > 9999)
                {
                    index = 0;
                }
                index += 1;
                sql = "update sys_t_sheet_no set sheet_value=" + index + " where sheet_id='" + sheet_type + "'";
                db.ExecuteScalar(sql, null);
                return sheet_type + "00" + System.DateTime.Now.ToString("yyMMdd") + index.ToString().PadLeft(4, '0');
            }
        }

        string CreateBatchNum(string sup_str, string dt_str, string index)
        {
            string str = "";
            if (index.Length <=5)
            {
                switch (index.Length)
                {
                    case 1:
                        str = "0000" + index;
                        break;
                    case 2:
                        str = "000" + index;
                        break;
                    case 3:
                        str = "00" + index;
                        break;
                    case 4:
                        str = "0" + index;
                        break;
                    case 5:
                        str = index;
                        break;
                }
            }
            else
            {
                str = index.Substring(index.Length - 5, 5);
            }
            return sup_str + "_" + dt_str + "_" + str;
        }

        /// <summary>
        /// 库存表字典
        /// </summary>
        /// <param name="ph_sheet_no"></param>
        /// <param name="is_min_stock"></param>
        /// <returns></returns>
        Dictionary<string, decimal> GetStockDic(string ph_sheet_no, string is_min_stock)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                var sql = "select a.item_no,a.min_stock,isnull(c.stock_qty,0) stock_qty ";
                sql += "from bi_t_item_info a ";
                sql += "left join ic_t_branch_stock c on c.item_no=a.item_no and c.branch_no = '0001' ";
                sql += "where a.item_no in(select distinct item_no from ot_temp_mrp_flow where ph_sheet_no=@ph_sheet_no) ";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@ph_sheet_no", ph_sheet_no)
                };
                var tmp_dt = db.ExecuteToTable(sql, pars);
                Dictionary<string, decimal> stockDic = new Dictionary<string, decimal>();
                decimal tmp_stock = 0;
                foreach (DataRow dr in tmp_dt.Rows)
                {
                    if ("1".Equals(is_min_stock))
                    {
                        tmp_stock = Conv.ToDecimal(dr["stock_qty"]) - Conv.ToDecimal(dr["min_stock"]);
                    }
                    else
                    {
                        tmp_stock = Conv.ToDecimal(dr["stock_qty"]);
                    }
                    stockDic.Add(dr["item_no"].ToString(), tmp_stock);
                }
                return stockDic;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 清空采购明细（采购助手）
        /// </summary>
        /// <param name="ph_sheet_no">批次号</param>
        void ClearCgDetail(string ph_sheet_no, DB.IDB db)
        {
            try
            {
                var sql = "delete from co_t_cg_order_detail where ph_sheet_no=@ph_sheet_no ";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@ph_sheet_no", ph_sheet_no)
                };
                db.ExecuteScalar(sql, pars);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("MrpBLL.ClearCgDetail()", ex.ToString(), ph_sheet_no);
                throw ex;
            }
        }


        /// <summary>
        /// 大宗采购需求计划运算
        /// </summary>
        /// <param name="sheet_nos">销售订单号</param>
        void IMrpBLL.DoBulkMrp(string sheet_nos, DateTime date1, DateTime date2, string ms_other, string oper_id)
        {
            var d = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB db = d;

            try
            {
                d.Open();
                d.BeginTran();
                //清空临时表数据
                string sql = "delete from ot_cg_bulk_master where convert(varchar(10),oper_date,120)<='" + DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd") + "' ";
                db.ExecuteScalar(sql, null);

                sql = "delete from ot_cg_bulk_detail where cb_sheet_no not in(select sheet_no from ot_cg_bulk_master) ";
                db.ExecuteScalar(sql, null);

                //新建大宗采购单
                var sheet_no = MaxCode(db, "CB");
                sql = "insert into ot_cg_bulk_master(sheet_no,start_date,end_date,ms_other1,oper_id,oper_date,approve_flag) ";
                sql += "values(@sheet_no,@start_date,@end_date,@ms_other1,@oper_id,getdate(),'0') ";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@sheet_no", sheet_no),
                    new System.Data.SqlClient.SqlParameter("@start_date", date1.ToString("yyyy-MM-dd")),
                    new System.Data.SqlClient.SqlParameter("@end_date", date2.ToString("yyyy-MM-dd")),
                    new System.Data.SqlClient.SqlParameter("@ms_other1", ms_other),
                    new System.Data.SqlClient.SqlParameter("@oper_id", oper_id)
                };
                db.ExecuteScalar(sql, pars);

                //记录大宗采购单关联销售订单
                var arr = sheet_nos.Split(',');
                StringBuilder sb = new StringBuilder();
                foreach (string s_no in arr)
                {
                    sb.Append("insert into ot_cg_bulk_sheet(cb_sheet_no,sheet_no) values('" + sheet_no + "','" + s_no + "');\r\n");
                }
                if (sb.Length > 0)
                {
                    db.ExecuteScalar(sb.ToString(), null);
                }

                //清空历史数据
                sql = "delete from ot_cg_bulk_detail where cb_sheet_no='" + sheet_no + "' ";
                db.ExecuteScalar(sql, null);

                //运算外购物料
                CalBulkCgMaterial(sheet_nos, sheet_no, db);

                //运算自制需求物料
                CalBulkProcessMaterial(sheet_nos, sheet_no, db);

                d.CommitTran();
            }
            catch (Exception ex)
            {
                d.RollBackTran();
                LogHelper.writeLog("MrpBLL.DoBulkMrp()", ex.ToString(), sheet_nos, oper_id);
                throw ex;
            }
            finally
            {
                d.Close();
            }
        }

        /// <summary>
        /// 按要货明细汇总生成采购订单
        /// </summary>
        /// <param name="op_type">2:按差异数量生成采购订单;1:按要货数量生成采购订单</param>
        /// <param name="item_nos">选中需要采购的商品</param>
        /// <param name="is_min_stock">1:采购考虑安全库存</param>
        void IMrpBLL.CreateBulkCgOrder(string cb_sheet_no, string op_type, string item_nos, string is_min_stock, string oper_id)
        {
            var d = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB db = d;

            try
            {
                d.Open();
                d.BeginTran();

                //判断是否已经运算
                string sql = " select count(*) as total from ot_cg_bulk_detail where cb_sheet_no=@cb_sheet_no ";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@cb_sheet_no", cb_sheet_no)
                };
                var dt = db.ExecuteToTable(sql, pars);
                if (dt.Rows.Count > 0 && Conv.ToInt(dt.Rows[0]["total"]) > 0)
                {
                    CreateBulkCGOrder(cb_sheet_no, op_type, item_nos, is_min_stock, oper_id, db);
                }
                else
                {
                    throw new Exception("请先执行物料需求运算");
                }

                d.CommitTran();
            }
            catch (Exception ex)
            {
                d.RollBackTran();
                LogHelper.writeLog("MrpBLL.CreateBulkCgOrder()", ex.ToString(), oper_id);
                throw ex;
            }
            finally
            {
                d.Close();
            }
        }



        /// <summary>
        /// 外购物料运算，只运算is_mrp='0'
        /// </summary>
        /// <param name="sheet_nos">订单号</param>
        void CalBulkCgMaterial(string sheet_nos, string cb_sheet_no, DB.IDB db)
        {
            try
            {
                sheet_nos = "'" + sheet_nos.Replace(",", "','") + "'";
                //外购物料需求
                string sql = "select a.item_no,a.unit_no,sum(a.order_qnty) order_qnty,max(c.price) price,c.sup_no ";
                sql += "from co_t_order_child a ";
                sql += "inner join co_t_order_main b on a.sheet_no=b.sheet_no ";
                sql += "inner join bi_t_item_info c on a.item_no=c.item_no ";
                sql += "where b.approve_flag='1' and isnull(c.is_mrp,'0')='0' and a.sheet_no in(" + sheet_nos + ") ";
                sql += "group by c.sup_no,a.item_no,a.unit_no ";
                System.Data.SqlClient.SqlParameter[] pars;
                var dt = db.ExecuteToTable(sql, null);
                if (dt.Rows.Count > 0)
                {
                    sql = "insert into ot_cg_bulk_detail(cb_sheet_no,sup_no,item_no,unit_no,price,order_qnty,cg_qty) ";
                    sql += "values(@cb_sheet_no,@sup_no,@item_no,@unit_no,@price,@order_qnty,@cg_qty) ";

                    foreach (DataRow dr in dt.Rows)
                    {
                        pars = new System.Data.SqlClient.SqlParameter[]
                        {
                            new System.Data.SqlClient.SqlParameter("@cb_sheet_no", cb_sheet_no),
                            new System.Data.SqlClient.SqlParameter("@sup_no", dr["sup_no"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@item_no", dr["item_no"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@unit_no", dr["unit_no"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@price", dr["price"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@order_qnty", dr["order_qnty"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@cg_qty", Conv.ToDecimal(dr["order_qnty"]))
                        };
                        db.ExecuteScalar(sql, pars);

                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("MrpBLL.CalBulkCgMaterial()", ex.ToString(), sheet_nos, cb_sheet_no);
                throw ex;
            }
        }

        /// <summary>
        /// 自制物料运算,只运算is_mrp='1'的多进A出加工类型
        /// </summary>
        /// <param name="sheet_nos">订单号</param>
        void CalBulkProcessMaterial(string sheet_nos, string cb_sheet_no, DB.IDB db)
        {
            try
            {
                sheet_nos = "'" + sheet_nos.Replace(",", "','") + "'";
                //加工物料需求
                string sql = "select d.item_no,d.unit_no,max(e.price) price,sum(a.order_qnty*d.qty*(1+d.loss_rate)) as order_qnty,e.sup_no ";
                sql += "from co_t_order_child a ";
                sql += "inner join co_t_order_main b on a.sheet_no=b.sheet_no ";
                sql += "inner join bi_t_item_info c on a.item_no=c.item_no ";
                sql += "left join bi_t_bom_detail d on c.item_bom=d.bom_no ";
                sql += "left join bi_t_item_info e on d.item_no=e.item_no ";
                sql += "where b.approve_flag='1' and isnull(c.is_mrp,'0')='1' and isnull(c.process_type,'0')='1' and c.item_bom is not null ";
                sql += "and a.sheet_no in(" + sheet_nos + ") ";
                sql += "group by e.sup_no,d.item_no,d.unit_no ";
                System.Data.SqlClient.SqlParameter[] pars;
                var dt = db.ExecuteToTable(sql, null);
                if (dt.Rows.Count > 0)
                {
                    sql = "insert into ot_cg_bulk_detail(cb_sheet_no,sup_no,item_no,unit_no,price,order_qnty,cg_qty) ";
                    sql += "values(@cb_sheet_no,@sup_no,@item_no,@unit_no,@price,@order_qnty,@cg_qty) ";
                    foreach (DataRow dr in dt.Rows)
                    {
                        pars = new System.Data.SqlClient.SqlParameter[]
                        {
                            new System.Data.SqlClient.SqlParameter("@cb_sheet_no", cb_sheet_no),
                            new System.Data.SqlClient.SqlParameter("@sup_no", dr["sup_no"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@item_no", dr["item_no"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@unit_no", dr["unit_no"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@price", dr["price"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@order_qnty", dr["order_qnty"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@cg_qty", Conv.ToDecimal(dr["order_qnty"]))
                        };
                        db.ExecuteScalar(sql, pars);

                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("MrpBLL.CalBulkProcessMaterial()", ex.ToString(), sheet_nos);
                throw ex;
            }
        }

        /// <summary>
        /// 按供应商生成采购订单
        /// 生成思路：获取批次内所有批销单，循环乘损耗率，得到最终要货数
        /// 取库存数量（如果按库存指标，就直接减去库存指标）
        /// 如果要按差异生成，判断库存>要货 就不生成采购
        /// </summary>
        void CreateBulkCGOrder(string cb_sheet_no, string op_type, string item_nos, string is_min_stock, string oper_id, DB.IDB db)
        {
            try
            {
                if (item_nos.Length > 0) item_nos = "'" + item_nos.Replace(",", "','") + "'";
                string sql = "select t.*,a.item_name,a.barcode,'1' unit_factor ";
                sql += "from (select sup_no,item_no,unit_no,max(price) price,sum(order_qnty) order_qnty,sum(cg_qty) cg_qty ";
                sql += "from ot_cg_bulk_detail where cb_sheet_no=@cb_sheet_no group by sup_no,item_no,unit_no) as t ";
                sql += "left join bi_t_item_info a on t.item_no=a.item_no ";
                sql += "where 1=1 ";
                if (item_nos.Length > 0)
                {
                    sql += "and t.item_no in(" + item_nos + ") ";
                }
                sql += "order by t.sup_no,t.item_no ";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@cb_sheet_no", cb_sheet_no)
                };
                var dt = db.ExecuteToTable(sql, pars);

                if (dt.Rows.Count > 0)
                {
                    Dictionary<string, decimal> stockDic = GetSelectStockDic(item_nos, is_min_stock);
                    var groupBy = dt.AsEnumerable().GroupBy(s => s["sup_no"].ToString());
                    foreach (var g in groupBy)
                    {
                        List<Model.co_t_order_child> lines = new List<Model.co_t_order_child>();
                        Model.co_t_order_main ord = new Model.co_t_order_main
                        {
                            sheet_no = MaxCode(db, "PO"),
                            p_sheet_no = "",
                            sup_no = g.First()["sup_no"].ToString(),
                            order_man = "1001",
                            oper_id = oper_id,
                            valid_date = DateTime.Now.AddDays(7),
                            oper_date = DateTime.Now,
                            update_time = DateTime.Now,
                            branch_no = "0001",
                            coin_code = "RMB",
                            paid_amount = 0,
                            trans_no = "P",
                            order_status = "0",
                            sale_way = "A",
                            approve_flag = "0",
                            other1 = "",
                            cm_branch = "00",
                            approve_man = "",
                            approve_date = System.DateTime.MinValue,
                            num1 = 0,
                            num2 = 0,
                            num3 = 0,
                            memo = ""
                        };

                        int index = 0;
                        decimal total_amt = 0;
                        foreach (var row in g)
                        {
                            ++index;
                            Model.co_t_order_child line = new Model.co_t_order_child();
                            line.sheet_no = ord.sheet_no;
                            line.item_no = row["item_no"].ToString();
                            line.unit_no = row["unit_no"].ToString();
                            line.barcode = row["barcode"].ToString();
                            line.unit_factor = Conv.ToDecimal(row["unit_factor"]);
                            line.discount = 1;
                            line.in_price = Conv.ToDecimal(row["price"]);

                            decimal stock_qty = 0;
                            if (!stockDic.TryGetValue(line.item_no, out stock_qty))
                            {
                                stock_qty = 0;
                            }
                            decimal qty = Math.Abs(Conv.ToDecimal(row["cg_qty"]));
                            line.order_qnty = qty;

                            if ("2".Equals(op_type))
                            {
                                //判断要货数量跟库存的差异
                                if (stock_qty >= line.order_qnty)
                                {
                                    //不需要生成采购
                                    continue;
                                }
                                else
                                {
                                    line.order_qnty -= stock_qty;
                                    stockDic[line.item_no] = 0;
                                }
                            }

                            line.sub_amount = line.in_price * line.order_qnty;
                            line.discount = 1;
                            line.other1 = "";
                            line.other2 = "";
                            line.voucher_no = "";
                            line.sheet_sort = index;
                            line.num1 = 0;
                            line.num2 = 0;
                            line.num3 = 0;
                            line.packqty = 0;
                            line.sgqty = 0;

                            total_amt += line.order_qnty * line.in_price;
                            lines.Add(line);

                        }
                        ord.total_amount = total_amt;

                        if (lines.Count > 0)
                        {
                            db.Insert(ord);
                            foreach (Model.co_t_order_child line in lines)
                            {
                                line.sheet_no = ord.sheet_no;
                                db.Insert(line, "flow_id");
                            }
                            lines.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("CreateBulkCGOrder()", ex.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 库存表字典
        /// </summary>
        /// <param name="item_nos"></param>
        /// <param name="is_min_stock"></param>
        /// <returns></returns>
        Dictionary<string, decimal> GetSelectStockDic(string item_nos, string is_min_stock, string is_lock_inventory = "1")
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                var sql = "";
                if (is_lock_inventory == "1")
                {
                    sql = "select a.item_no,a.min_stock,isnull(c.stock_qty,0) stock_qty,isnull(b.lock_qty,0) lock_qty ";
                    sql += "from bi_t_item_info a ";
                    sql += "left join ic_t_branch_stock c on c.item_no=a.item_no and c.branch_no = '0001' ";
                    sql += "left join (select item_no,sum(lock_qty) lock_qty from ot_temp_lock_inventory group by item_no) b on a.item_no=b.item_no ";
                    sql += "where a.item_no in(" + item_nos + ") ";
                }
                else
                {
                    sql = "select a.item_no,a.min_stock,isnull(c.stock_qty,0) stock_qty,0 lock_qty ";
                    sql += "from bi_t_item_info a ";
                    sql += "left join ic_t_branch_stock c on c.item_no=a.item_no and c.branch_no = '0001' ";
                    sql += "where a.item_no in(" + item_nos + ") ";
                }

                var tmp_dt = db.ExecuteToTable(sql, null);
                Dictionary<string, decimal> stockDic = new Dictionary<string, decimal>();
                decimal tmp_stock = 0;
                foreach (DataRow dr in tmp_dt.Rows)
                {
                    if ("1".Equals(is_min_stock))
                    {
                        tmp_stock = Conv.ToDecimal(dr["stock_qty"]) - Conv.ToDecimal(dr["min_stock"]);
                    }
                    else
                    {
                        tmp_stock = Conv.ToDecimal(dr["stock_qty"]);
                    }
                    if (is_lock_inventory == "1")
                    {
                        tmp_stock -= Conv.ToDecimal(dr["lock_qty"]);
                    }
                    stockDic.Add(dr["item_no"].ToString(), tmp_stock);
                }
                return stockDic;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 大宗采购锁库存
        /// </summary>
        /// <param name="sheet_nos">销售订单号</param>
        void IMrpBLL.DoLockInventory(string cb_sheet_no, string sheet_nos, string lock_sheet_nos)
        {
            var d = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB db = d;

            try
            {
                d.Open();
                d.BeginTran();
                //清空临时锁库存表数据
                string sql = "delete from ot_temp_lock_inventory ";
                db.ExecuteScalar(sql, null);

                //运算外购物料
                CalLockBulkCgMaterial(sheet_nos, lock_sheet_nos, cb_sheet_no, db);

                //运算自制需求物料
                CalLockBulkProcessMaterial(sheet_nos, lock_sheet_nos, cb_sheet_no, db);

                d.CommitTran();
            }
            catch (Exception ex)
            {
                d.RollBackTran();
                LogHelper.writeLog("MrpBLL.DoLockInventory()", ex.ToString(), cb_sheet_no, sheet_nos);
                throw ex;
            }
            finally
            {
                d.Close();
            }
        }

        /// <summary>
        /// 锁外购物料库存，只运算is_mrp='0'
        /// </summary>
        /// <param name="sheet_nos">订单号</param>
        void CalLockBulkCgMaterial(string sheet_nos, string lock_sheet_nos, string cb_sheet_no, DB.IDB db)
        {
            try
            {
                sheet_nos = "'" + sheet_nos.Replace(",", "','") + "'";
                lock_sheet_nos = "'" + lock_sheet_nos.Replace(",", "','") + "'";
                //外购物料需求
                string sql = "select a.item_no,a.unit_no,sum(a.order_qnty) order_qnty,max(c.price) price,c.sup_no ";
                sql += "from co_t_order_child a ";
                sql += "inner join co_t_order_main b on a.sheet_no=b.sheet_no ";
                sql += "inner join bi_t_item_info c on a.item_no=c.item_no ";
                sql += "where b.approve_flag='1' and isnull(c.is_mrp,'0')='0' and (a.sheet_no in(" + sheet_nos + ") or a.sheet_no in(" + lock_sheet_nos + "))";
                sql += "group by c.sup_no,a.item_no,a.unit_no ";
                System.Data.SqlClient.SqlParameter[] pars;
                var dt = db.ExecuteToTable(sql, null);
                if (dt.Rows.Count > 0)
                {
                    sql = "insert into ot_temp_lock_inventory(cb_sheet_no,sup_no,item_no,unit_no,lock_qty) ";
                    sql += "values(@cb_sheet_no,@sup_no,@item_no,@unit_no,@lock_qty) ";

                    foreach (DataRow dr in dt.Rows)
                    {
                        pars = new System.Data.SqlClient.SqlParameter[]
                        {
                            new System.Data.SqlClient.SqlParameter("@cb_sheet_no", cb_sheet_no),
                            new System.Data.SqlClient.SqlParameter("@sup_no", dr["sup_no"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@item_no", dr["item_no"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@unit_no", dr["unit_no"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@lock_qty", Conv.ToDecimal(dr["order_qnty"]))
                        };
                        db.ExecuteScalar(sql, pars);

                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("MrpBLL.CalLockBulkCgMaterial()", ex.ToString(), sheet_nos, cb_sheet_no);
                throw ex;
            }
        }

        /// <summary>
        /// 锁自制物料库存,只运算is_mrp='1'的多进A出加工类型
        /// </summary>
        /// <param name="sheet_nos">订单号</param>
        void CalLockBulkProcessMaterial(string sheet_nos, string lock_sheet_nos, string cb_sheet_no, DB.IDB db)
        {
            try
            {
                sheet_nos = "'" + sheet_nos.Replace(",", "','") + "'";
                lock_sheet_nos = "'" + lock_sheet_nos.Replace(",", "','") + "'";
                //加工物料需求
                string sql = "select d.item_no,d.unit_no,max(e.price) price,sum(a.order_qnty*d.qty*(1+d.loss_rate)) as order_qnty,e.sup_no ";
                sql += "from co_t_order_child a ";
                sql += "inner join co_t_order_main b on a.sheet_no=b.sheet_no ";
                sql += "inner join bi_t_item_info c on a.item_no=c.item_no ";
                sql += "left join bi_t_bom_detail d on c.item_bom=d.bom_no ";
                sql += "left join bi_t_item_info e on d.item_no=e.item_no ";
                sql += "where b.approve_flag='1' and isnull(c.is_mrp,'0')='1' and isnull(c.process_type,'0')='1' and c.item_bom is not null ";
                sql += "and (a.sheet_no in(" + sheet_nos + ") or a.sheet_no in(" + lock_sheet_nos + ")) ";
                sql += "group by e.sup_no,d.item_no,d.unit_no ";
                System.Data.SqlClient.SqlParameter[] pars;
                var dt = db.ExecuteToTable(sql, null);
                if (dt.Rows.Count > 0)
                {
                    sql = "insert into ot_temp_lock_inventory(cb_sheet_no,sup_no,item_no,unit_no,lock_qty) ";
                    sql += "values(@cb_sheet_no,@sup_no,@item_no,@unit_no,@lock_qty) ";

                    foreach (DataRow dr in dt.Rows)
                    {
                        pars = new System.Data.SqlClient.SqlParameter[]
                        {
                            new System.Data.SqlClient.SqlParameter("@cb_sheet_no", cb_sheet_no),
                            new System.Data.SqlClient.SqlParameter("@sup_no", dr["sup_no"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@item_no", dr["item_no"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@unit_no", dr["unit_no"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@lock_qty", Conv.ToDecimal(dr["order_qnty"]))
                        };
                        db.ExecuteScalar(sql, pars);

                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("MrpBLL.CalBulkProcessMaterial()", ex.ToString(), sheet_nos);
                throw ex;
            }
        }

    }
}