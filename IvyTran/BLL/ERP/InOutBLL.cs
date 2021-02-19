using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DB;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;
using ic_t_inout_store_detail = Model.ic_t_inout_store_detail;
using ic_t_inout_store_master = Model.ic_t_inout_store_master;
using item = IvyTran.body.item;
using rp_t_recpay_record_detail = Model.rp_t_recpay_record_detail;
using sm_t_salesheet = Model.sm_t_salesheet;
using sm_t_salesheet_detail = Model.sm_t_salesheet_detail;

namespace IvyTran.BLL.ERP
{
    public class InOutBLL : IInOutBLL
    {
        public DataTable GetReceiveOrderDetail(DateTime start_time, DateTime end_time, string item_no, string is_build)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = @"   SELECT '0' Sel,d.flow_id 行号,
       d.po_sheet_no,
       d.sup_no 供应商编号,
s.sup_name 供应商名称,
d.batch_num ,
	   d.trans_no,
       d.item_no 商品编号,
       d.item_name 商品名称,
       d.barcode 条码,
       d.unit_no 单位,
       d.unit_factor,
       d.price 价格,
       d.order_qnty,
       d.receive_qty 数量,
       d.oper_date,
       d.oper_id,
       d.create_time 操作时间,
       s.sup_name 供应商名称,
       d.is_build 是否生成,
cust.supcust_no 客户编号,
cust.sup_name 客户名称
FROM dbo.co_t_receive_order_detail d
LEFT JOIN dbo.bi_t_supcust_info s
  ON d.sup_no = s.supcust_no AND s.supcust_flag = 'S'
LEFT JOIN dbo.bi_t_supcust_info cust
  ON d.cust_no = cust.supcust_no 
  AND cust.supcust_flag = 'C'
  left join bi_t_item_info ii on d.item_no=ii.item_no
WHERE CONVERT(VARCHAR(10),d.create_time,20) BETWEEN '" + start_time.Toyyyy_MM_dd() + "' AND '" + end_time.Toyyyy_MM_dd() + "' and  d.is_build like '%" + is_build + "%'";
            int a = 0;
            if (int.TryParse(item_no, out a))
            {
                sql += @" and ii.item_subno like '%" + item_no + "%'";
            }
            else
            {
                sql += @" and ii.item_name like '%" + item_no + "%'";
            }


            DataTable tb = db.ExecuteToTable(sql, null);
            return tb;
        }
        public void UpdateReceiveOrderDetail(DataTable dt)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            StringBuilder sb = new StringBuilder();
            string sql = "";
            foreach (DataRow dataRow in dt.Rows)
            {
                sb.Append($@"update co_t_receive_order_detail set sup_no='{dataRow["供应商编号"]}' where flow_id='{dataRow["行号"]}'");
                sb.Append("\r\n");
                if (sb.Length >= 3000)
                {
                    db.ExecuteScalar(sb.ToString(), null);
                }
            }

            if (sb.Length > 0)
            {
                db.ExecuteScalar(sb.ToString(), null);
            }
        }
        System.Data.DataTable IInOutBLL.GetInOutTZSheet(string sheet_no)
        {
            string sql = @"select a.sheet_no,a.voucher_no,b.branch_no,b.branch_name branch
,sup_name supcust ,oper_date,a.supcust_no
from [dbo].[ic_t_inout_store_master] a 

left join [dbo].[bi_t_branch_info] b on a.branch_no=b.branch_no
left join [dbo].[bi_t_supcust_info] c on a.supcust_no=c.supcust_no 
and supcust_flag='S' where trans_no='A'";
            if (!string.IsNullOrEmpty(sheet_no))
            {
                sql += "and sheet_no like '%" + sheet_no + "%'";
            }
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        void IInOutBLL.DeleteInOutTZ(string sheet_no,string type)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();

                db.BeginTran();
                if (type == "0")
                {
                    string sql = @"select * from ic_t_inoutstore_recpay_main where sheet_no='" + sheet_no + "'";

                    DataTable dt = d.ExecuteToTable(sql, null);
                    if (dt.Rows.Count > 0)
                    {
                        sql = "DELETE FROM ic_t_inoutstore_recpay_detail WHERE sheet_no = '" + sheet_no + "'";
                        d.ExecuteScalar(sql, null);
                        sql = "DELETE FROM ic_t_inoutstore_recpay_main WHERE sheet_no = '" + sheet_no + "'";
                        d.ExecuteScalar(sql, null);
                    }
                }
                if(type=="1")
                {
                    string sql = @"select * from [dbo].[sm_t_salesheet_recpay_main] where sheet_no='" + sheet_no + "'";

                    DataTable dt = d.ExecuteToTable(sql, null);
                    if (dt.Rows.Count > 0)
                    {
                        sql = "DELETE FROM [dbo].[sm_t_salesheet_recpay_detail] WHERE sheet_no = '" + sheet_no + "'";
                        d.ExecuteScalar(sql, null);
                        sql = "DELETE [dbo].[sm_t_salesheet_recpay_main] WHERE sheet_no = '" + sheet_no + "'";
                        d.ExecuteScalar(sql, null);
                    }

                }
                    db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("InOutBLL.DeleteTZ()", ex.ToString(), sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
        void IInOutBLL.InOutTZSheetDetail(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"select a.sheet_no,a.oper_id,a.oper_date,a.voucher_no,a.cust_no
,isnull(a.approve_flag,0) approve_flag,a.approve_man
,a.approve_date,a.create_time,a.update_time
,b.branch_name,c.oper_name
                ,e.oper_name as approve_man_name,f.sup_name 
				from [dbo].[ic_t_inoutstore_recpay_main] a
            left join(select* from bi_t_branch_info ) b on a.branch_no = b.branch_no
            left join(select supcust_no, sup_name from bi_t_supcust_info where supcust_flag= 'S') f on a.cust_no = f.supcust_no
            left join(select* from sa_t_operator_i) c on a.oper_id = c.oper_id
            left join(select* from sa_t_operator_i) e on a.approve_man = e.oper_id where  1=1";
            sql += " and a.sheet_no= '" + sheet_no + "' ";
            tb1 = db.ExecuteToTable(sql, null);
            sql = @"select a.flow_id, a.task_flow_id,a.sheet_no,  b.branch_no, bi.branch_name
, a.item_no,b.unit_no,a.real_qnty,b.item_name,b.item_subno,a.other1,d.batch_num
from [dbo].[ic_t_inoutstore_recpay_detail] a  
left join bi_t_item_info b on a.item_no=b.item_no  
left join [dbo].[ic_t_inoutstore_recpay_main] ma on ma.sheet_no=a.sheet_no 
LEFT JOIN dbo.bi_t_branch_info bi ON bi.branch_no=b.branch_no  
LEFT JOIN dbo.k3_t_item_info_expand ie ON ie.item_no = b.item_no 
left join [dbo].[ic_t_inout_store_detail] d on d.flow_id=a.task_flow_id
";
            sql += " where a.sheet_no='" + sheet_no + "' order by a.flow_id ";
            tb2 = db.ExecuteToTable(sql, null);
        }
        void IInOutBLL.InOutTZSheetDetail_D(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"select a.sheet_no,a.voucher_no,b.branch_no,b.branch_name,a.supcust_no cust_no
,sup_name ,oper_date
from [dbo].[ic_t_inout_store_master] a 
left join [dbo].[bi_t_branch_info] b on a.branch_no=b.branch_no
left join [dbo].[bi_t_supcust_info] c on a.supcust_no=c.supcust_no 
and supcust_flag='S' where  1=1";
            sql += " and a.sheet_no= '" + sheet_no + "' ";
            tb1 = db.ExecuteToTable(sql, null);
            sql = @"select a.flow_id task_flow_id,a.sheet_no,a.item_no,c.item_name
,c.item_subno,a.other1
,a.in_qty real_qnty,a.batch_num,c.unit_no,c.barcode,c.branch_no
,b.branch_name from  [dbo].[ic_t_inout_store_detail] a 

left join [dbo].[bi_t_item_info] c on c.item_no=a.item_no
left join  [dbo].[bi_t_branch_info] b on c.branch_no=b.branch_no
";
            sql += " where a.sheet_no='" + sheet_no + "' order by a.flow_id ";
            tb2 = db.ExecuteToTable(sql, null);
        }
        System.Data.DataTable IInOutBLL.InOutTZList(string date1, string date2, string cust_no)
        {
            var condition_sql = "";
            if (cust_no != "") condition_sql += " and a.supcust_no  = '" + cust_no + "' ";
            string sql = @"select a.sheet_no,a.oper_id,a.oper_date,a.voucher_no,a.cust_no,isnull(a.approve_flag,0) approve_flag,a.approve_man
,a.approve_date,a.create_time,a.update_time
,b.branch_name,c.oper_name
                ,e.oper_name as approve_man_name,f.sup_name 
				from [dbo].[ic_t_inoutstore_recpay_main] a
            left join(select* from bi_t_branch_info ) b on a.branch_no = b.branch_no
            left join(select supcust_no, sup_name from bi_t_supcust_info where supcust_flag= 'S') f on a.cust_no = f.supcust_no
            left join(select* from sa_t_operator_i) c on a.oper_id = c.oper_id
            left join(select* from sa_t_operator_i) e on a.approve_man = e.oper_id
";
            sql += "where a.oper_date>='" + date1 + " 00:00:00.000' and a.oper_date<='" + date2 + " 23:59:59.999' " +
                   condition_sql;
            sql += "order by a.sheet_no desc ";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }
        void IInOutBLL.InOutTZ(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql =
                @"select a.sheet_no,a.oper_id,a.oper_date,a.voucher_no,a.cust_no
,isnull(a.approve_flag,0) approve_flag,a.approve_man
,a.approve_date,a.create_time,a.update_time
,b.branch_name,c.oper_name
                ,e.oper_name as approve_man_name,f.sup_name 
				from [dbo].[ic_t_inoutstore_recpay_main] a
            left join(select* from bi_t_branch_info ) b on a.branch_no = b.branch_no
            left join(select supcust_no, sup_name from bi_t_supcust_info where supcust_flag= 'C') f on a.cust_no = f.supcust_no
            left join(select* from sa_t_operator_i) c on a.oper_id = c.oper_id
            left join(select* from sa_t_operator_i) e on a.approve_man = e.oper_id";
            sql += " where a.sheet_no='" + sheet_no + "' ";
            tb1 = db.ExecuteToTable(sql, null);
            sql = @"select a.flow_id, a.task_flow_id,a.sheet_no,  b.branch_no, bi.branch_name
, a.item_no,b.unit_no,a.real_qnty,b.item_name,b.item_subno,a.other1,d.batch_num
from [dbo].[ic_t_inoutstore_recpay_detail] a  
left join bi_t_item_info b on a.item_no=b.item_no  
left join [dbo].[ic_t_inoutstore_recpay_main] ma on ma.sheet_no=a.sheet_no 
LEFT JOIN dbo.bi_t_branch_info bi ON bi.branch_no=b.branch_no  
LEFT JOIN dbo.k3_t_item_info_expand ie ON ie.item_no = b.item_no 
left join [dbo].[ic_t_inout_store_detail] d on d.flow_id=a.task_flow_id
 ";
            sql += " where a.sheet_no='" + sheet_no + "' order by a.flow_id ";
            tb2 = db.ExecuteToTable(sql, null);


        }


        void IInOutBLL.add_InOutTZ(List<Model.InOutModel.ic_t_inoutstore_recpay_detail> lr, Model.InOutModel.ic_t_inoutstore_recpay_main main)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();

                db.BeginTran();
                StringBuilder sb = new StringBuilder();
                string sql = @"select * from ic_t_inoutstore_recpay_main where sheet_no='" + main.sheet_no + "'";
                if (d.ExecuteToTable(sql, null).Rows.Count <= 0)
                {
                    try
                    {
                        d.Insert(main);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                }
                foreach (var item in lr)
                {
                    sql = @"select * from ic_t_inoutstore_recpay_detail where sheet_no='" + item.sheet_no + "' and task_flow_id='" + item.task_flow_id + "'";
                    if (d.ExecuteToTable(sql, null).Rows.Count > 0)
                    {
                        sb.Append("update [dbo].[ic_t_inoutstore_recpay_detail] set  real_qnty='" + item.real_qnty +
                                  "',batch_num='" + item.batch_num + "' where flow_id ='" + item.flow_id + "'");
                        if (sb.Length >= 3000)
                        {
                            try
                            {
                                d.ExecuteScalar(sb.ToString(), null);
                                sb.Clear();
                            }
                            catch (Exception e)
                            {
                               throw new Exception(e.ToString());
                            }
                           
                        }

                    }
                    else
                    {
                        sql = "select in_qty from [dbo].[ic_t_inout_store_detail] where flow_id='" + item.task_flow_id + "'";
                        DataTable dt = d.ExecuteToTable(sql, null);
                        if (dt.Rows.Count > 0)
                        {
                            if (item.real_qnty != dt.Rows[0]["in_qty"].ToDecimal())
                            {
                                sql = @"select Max(isnull(flow_id,0))+1 flow_id from ic_t_inoutstore_recpay_detail";

                                try
                                {
                                    item.flow_id = d.ExecuteToTable(sql, null).Rows[0]["flow_id"].ToString().ToDecimal();
                                    d.Insert(item);
                                }
                                catch (Exception e)
                                {
                                    throw new Exception(e.ToString());
                                }

                            }
                        }
                    }
                }
                sb.Append("update [dbo].[ic_t_inoutstore_recpay_main] set  update_time='" + DateTime.Now +
                          "' where sheet_no ='" + main.sheet_no + "'");
                if (sb.Length > 0)
                {
                    try
                    {
                        d.ExecuteScalar(sb.ToString(), null);
                        sb.Clear();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.ToString());
                    }
                   
                }
                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("InOutBLL.AddInoutTZ()", ex.ToString(), main.sheet_no);
                db.RollBackTran();
                throw new Exception("保存失败！"); 
            }
            finally
            {
                db.Close();
            }

        }
        IOrder orderBLL = new Order();
        System.Data.DataTable IInOutBLL.GetSaleSheetJGSheet(string sheet_no,DateTime date1,DateTime date2)
        {
            string sql = @"select a.sheet_no,b.branch_no,b.branch_name branch
,sup_no,sup_name supcust ,oper_date,approve_flag,a.update_time,
 e.oper_name as approve_man_name,c.sup_name,approve_date,d.oper_name
from [dbo].[co_t_order_main] a 

left join [dbo].[bi_t_branch_info] b on a.branch_no=b.branch_no
left join [dbo].[bi_t_supcust_info] c on a.sup_no=c.supcust_no 
and supcust_flag='S'
left join(select* from sa_t_operator_i) d on a.oper_id = d.oper_id
left join(select* from sa_t_operator_i) e on a.approve_man = e.oper_id
where sheet_no like '%PP%' and oper_date between '" + date1+ "' and '" + date2 + "' ";
            if (!string.IsNullOrEmpty(sheet_no))
            {
                sql += " and sheet_no like '%" + sheet_no + "%'";
            }
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }
        void IInOutBLL.GetSaleSheetJGSheetDetail(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"select a.sheet_no,b.branch_no,b.branch_name branch,b.branch_name
,sup_no,sup_name supcust ,oper_date,a.update_time,approve_man,approve_flag
,d.oper_name,e.oper_name as order_man_name,f.oper_name as approve_man_name,a.oper_id,a.approve_date
from [dbo].[co_t_order_main] a 
left join [dbo].[bi_t_branch_info] b on a.branch_no=b.branch_no
left join [dbo].[bi_t_supcust_info] c on a.sup_no=c.supcust_no 
and supcust_flag='C'
left join  sa_t_operator_i d on a.oper_id = d.oper_id
left join bi_t_people_info e on a.order_man = e.oper_id
left join  sa_t_operator_i f on a.approve_man = f.oper_id
where a.sheet_no like '%PP%' ";
            sql += " and a.sheet_no='" + sheet_no + "' ";
            tb1 = db.ExecuteToTable(sql, null);
            sql = @"select a.flow_id task_flow_id,a.sheet_no,a.item_no,c.item_name
,c.item_subno,a.other1,a.real_qty,a.batch_num,c.unit_no,c.barcode,c.branch_no,a.voucher_no,a.other1
,b.branch_name from  [dbo].[co_t_order_child] a 

left join [dbo].[bi_t_item_info] c on c.item_no=a.item_no
left join  [dbo].[bi_t_branch_info] b on c.branch_no=b.branch_no
";
            sql += " where a.sheet_no='" + sheet_no + "'  order by a.flow_id ";
            tb2 = db.ExecuteToTable(sql, null);
        }
        System.Data.DataTable IInOutBLL.GetSaleSheetTZSheet(string sheet_no)
        {
            string sql = @"select a.sheet_no,a.voucher_no,b.branch_no,b.branch_name branch,cust_no,sup_name supcust ,oper_date
from [dbo].[sm_t_salesheet] a 

left join [dbo].[bi_t_branch_info] b on a.branch_no=b.branch_no
left join [dbo].[bi_t_supcust_info] c on a.cust_no=c.supcust_no 
and supcust_flag='C' where sheet_no like '%SO%'";
            if (!string.IsNullOrEmpty(sheet_no))
            {
                sql += " where sheet_no like '%"+sheet_no+"%'";
            }
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }
        void IInOutBLL.GetSaleSheetTZSheetDetail(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"select a.sheet_no,a.voucher_no,b.branch_no,b.branch_name,cust_no,sup_name ,oper_date
from [dbo].[sm_t_salesheet] a 
left join [dbo].[bi_t_branch_info] b on a.branch_no=b.branch_no
left join [dbo].[bi_t_supcust_info] c on a.cust_no=c.supcust_no 
and supcust_flag='C'";
            sql += " where a.sheet_no= '" + sheet_no + "' ";
            tb1 = db.ExecuteToTable(sql, null);
            sql = @"select a.flow_id task_flow_id,a.sheet_no,a.item_no,c.item_name
,c.item_subno,a.other1,a.sale_qnty real_qnty,a.batch_num,c.unit_no,c.barcode,c.branch_no
,b.branch_name from  [dbo].[sm_t_salesheet_detail] a 

left join [dbo].[bi_t_item_info] c on c.item_no=a.item_no
left join  [dbo].[bi_t_branch_info] b on c.branch_no=b.branch_no
";
            sql += " where a.sheet_no='" + sheet_no + "' order by a.flow_id ";
            tb2 = db.ExecuteToTable(sql, null);
        }
        System.Data.DataTable IInOutBLL.GetSaleSheetTZList(string date1, string date2, string cust_no)
        {
            var condition_sql = "";
            if (cust_no != "") condition_sql += " and a.cust_no  = '" + cust_no + "' ";
            string sql = @"select a.sheet_no,a.oper_id,a.oper_date,a.voucher_no,a.cust_no,isnull(a.approve_flag,0) approve_flag,a.approve_man
,a.approve_date,a.create_time,a.update_time
,b.branch_name,c.oper_name
                ,e.oper_name as approve_man_name,f.sup_name from sm_t_salesheet_recpay_main a
            left join(select* from bi_t_branch_info ) b on a.branch_no = b.branch_no
            left join(select supcust_no, sup_name from bi_t_supcust_info where supcust_flag= 'C') f on a.cust_no = f.supcust_no
            left join(select* from sa_t_operator_i) c on a.oper_id = c.oper_id
            left join(select* from sa_t_operator_i) e on a.approve_man = e.oper_id
";
            sql += "where a.oper_date>='" + date1 + " 00:00:00.000' and a.oper_date<='" + date2 + " 23:59:59.999' " +
                   condition_sql;
            sql += "order by a.sheet_no desc ";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }
        void IInOutBLL.GetSaleSheetTZ(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql =
                @"select a.*,b.branch_name,c.oper_name
,e.oper_name as approve_man_name,f.sup_name cust_name,
f.sup_fullname,f.sup_addr,f.sup_tel,f.sup_man,isnull(g.remark,1) remark
from [dbo].[sm_t_salesheet_recpay_main] a
left join(select* from bi_t_branch_info ) b on a.branch_no = b.branch_no
left join(select* from bi_t_supcust_info where supcust_flag= 'C') f on a.cust_no = f.supcust_no
left join(select* from sa_t_operator_i) c on a.oper_id = c.oper_id
left join sm_t_salesheet_receive g on g.sheet_no=a.sheet_no
left join(select* from sa_t_operator_i) e on a.approve_man = e.oper_id";
            sql += " where a.sheet_no='" + sheet_no + "' ";
            tb1 = db.ExecuteToTable(sql, null);
            sql = @"select a.flow_id, a.task_flow_id,a.sheet_no,  b.branch_no, bi.branch_name
, a.item_no,b.unit_no,a.real_qnty,b.item_name,b.item_subno,a.other1,d.batch_num
from sm_t_salesheet_recpay_detail a  
left join bi_t_item_info b on a.item_no=b.item_no  
left join [dbo].[sm_t_salesheet_recpay_main] ma on ma.sheet_no=a.sheet_no 
LEFT JOIN dbo.bi_t_branch_info bi ON bi.branch_no=b.branch_no  
LEFT JOIN dbo.k3_t_item_info_expand ie ON ie.item_no = b.item_no 
left join [dbo].[sm_t_salesheet_detail] d on d.flow_id=a.task_flow_id
 ";
            sql += " where a.sheet_no='" + sheet_no + "' order by a.flow_id ";
            tb2 = db.ExecuteToTable(sql, null);

           
        }


        void IInOutBLL.add_TZ(List<Model.InOutModel.sm_t_salesheet_recpay_detail> lr, Model.InOutModel.sm_t_salesheet_recpay_main main)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();

                db.BeginTran();
                StringBuilder sb=new StringBuilder();
            string sql = @"select * from sm_t_salesheet_recpay_main where sheet_no='"+main.sheet_no+"'";
            if (d.ExecuteToTable(sql, null).Rows.Count <= 0)
            {
                try
                {
                    d.Insert(main);
                    }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            foreach (var item in lr)
            { 
                sql = @"select * from sm_t_salesheet_recpay_detail where sheet_no='"+ item.sheet_no+"' and task_flow_id='"+ item.task_flow_id+"'";
                if (d.ExecuteToTable(sql, null).Rows.Count > 0)
                {
                    sb.Append("update [dbo].[sm_t_salesheet_recpay_detail] set  real_qnty='" + item.real_qnty +
                              "',batch_num='" + item.batch_num + "' where flow_id ='" + item.flow_id + "'");
                    if (sb.Length >= 3000)
                    {
                        d.ExecuteScalar(sb.ToString(), null);
                        sb.Clear();
                    }

                }
                else
                {
                    sql = "select sale_qnty from [dbo].[sm_t_salesheet_detail] where flow_id='" + item.task_flow_id+"'";
                    DataTable dt=d.ExecuteToTable(sql,null);
                    if (dt.Rows.Count > 0)
                    {
                        if (item.real_qnty != dt.Rows[0]["sale_qnty"].ToDecimal())
                        {
                            sql = @"select Max(isnull(flow_id,0))+1 flow_id from sm_t_salesheet_recpay_detail";
                            item.flow_id = d.ExecuteToTable(sql, null).Rows[0]["flow_id"].ToString().ToDecimal();
                            d.Insert(item);
                        }
                        }

                    
                   
                }
            }
            sb.Append("update [dbo].[sm_t_salesheet_recpay_main] set  update_time='" + DateTime.Now +
                      "' where sheet_no ='" + main.sheet_no + "'");
            if (sb.Length > 0)
            {
                d.ExecuteScalar(sb.ToString(), null);
                sb.Clear();
            }
            db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("InOutBLL.AddSaleSheetTZ()", ex.ToString(), main.sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }

        }
        public string MaxCode(DB.IDB db, string sheet_type)
        {
            string sql = "select sheet_value from sys_t_sheet_no where sheet_id='" + sheet_type + "'";

            object obj = db.ExecuteScalar(sql, null);
            if (obj == null || obj == DBNull.Value)
            {
                return "";
            }
            else
            {
                int index = Helper.Conv.ToInt(obj);
                index += 1;
                if (index > 9999)
                {
                    index = 1;
                }

                sql = "update sys_t_sheet_no set sheet_value=" + index + " where sheet_id='" + sheet_type + "'";
                db.ExecuteScalar(sql, null);
                return sheet_type + "00" + System.DateTime.Now.ToString("yyMMdd") + index.ToString().PadLeft(4, '0');
            }

        }

        System.Data.DataTable IInOutBLL.GetSaleSheetList(string date1, string date2, string cust_no, string sale_man)
        {
            var condition_sql = "";
            if (cust_no != "") condition_sql += " and a.cust_no  = '" + cust_no + "' ";
            if (sale_man != "") condition_sql += " and a.sale_man = '" + sale_man + "' ";
            string sql =
                "select a.*,b.branch_name,c.oper_name,d.oper_name as sale_man_name,e.oper_name as approve_man_name,f.sup_name ";
            sql += "from sm_t_salesheet a ";
            sql += "left join (select * from bi_t_branch_info ) b on a.branch_no=b.branch_no ";
            sql +=
                "left join (select supcust_no,sup_name from bi_t_supcust_info where supcust_flag='C') f on a.cust_no=f.supcust_no ";
            sql += "left join (select * from sa_t_operator_i) c on a.oper_id=c.oper_id ";
            sql += "left join bi_t_people_info d on a.sale_man=d.oper_id ";
            sql += "left join (select * from sa_t_operator_i) e on a.approve_man=e.oper_id ";
            sql += "where a.oper_date>='" + date1 + " 00:00:00.000' and a.oper_date<='" + date2 + " 23:59:59.999' " +
                   condition_sql;
            sql += "order by a.sheet_no desc ";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);

            return tb;
        }

        System.Data.DataTable IInOutBLL.GetSimpleSaleSheetList(string date1, string date2, string cust_no)
        {
            string sql = @" select *,(a.branch_no+'/'+b.branch_name) 仓库,(a.cust_no+'/'+s.sup_name) 客户
from sm_t_salesheet a
left join bi_t_branch_info b on b.branch_no=a.branch_no
left join bi_t_supcust_info s on s.supcust_no=a.cust_no and s.supcust_flag='C' ";
            sql += "where a.oper_date>='" + date1 + " 00:00:00.000' and a.oper_date<='" + date2 +
                   " 23:59:59.999' and a.cust_no=@cust_no and approve_flag='1' ";
            sql += "order by a.sheet_no desc ";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var pars = new[]
            {
                new System.Data.SqlClient.SqlParameter("@cust_no", cust_no)
            };
            var tb = db.ExecuteToTable(sql, pars);

            return tb;
        }


        void IInOutBLL.GetSaleSheet(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql =
                @"select a.*,b.branch_name,c.oper_name,d.oper_name as sale_man_name,e.oper_name as approve_man_name,f.sup_name cust_name,
f.sup_fullname,f.sup_addr,f.sup_tel,f.sup_man,isnull(g.remark,1) remark
from sm_t_salesheet a
left join(select* from bi_t_branch_info ) b on a.branch_no = b.branch_no
left join(select* from bi_t_supcust_info where supcust_flag= 'C') f on a.cust_no = f.supcust_no
left join(select* from sa_t_operator_i) c on a.oper_id = c.oper_id
left join bi_t_people_info d on a.sale_man = d.oper_id
left join sm_t_salesheet_receive g on g.sheet_no=a.sheet_no
left join(select* from sa_t_operator_i) e on a.approve_man = e.oper_id";
            sql += " where a.sheet_no='" + sheet_no + "' ";
            tb1 = db.ExecuteToTable(sql, null);


            sql = @"select a.flow_id,   a.sheet_no,  a.branch_no_d, bi.branch_name
,    a.item_no,a.unit_no,   a.unit_factor,a.sale_price,a.real_price
,a.cost_price,a.sale_qnty, a.sale_money
,a.sale_tax,a.is_tax, a.other1,a.other2
, a.other3,a.other4,a.num1, a.num2,a.num3
,a.num4, a.num5,a.num6, a.barcode, a.sheet_sort
, a.ret_qnty,a.discount,a.voucher_no, a.cost_notax
, a.packqty,a.sgqty,a.branch_no_d,a.ly_sup_no
,a.ly_rate,a.num7,a.other5,a.num8
,a.produce_day,a.batch_num,b.item_subno
,b.item_name,b.item_size,b.unit_no ,b.min_stock,0 stock_qty,ie.is_batch 
,a.order_qnty yingjie,a.order_qnty dinghuo
,isnull(d1.real_qnty,a.sale_qnty) shifa
,a.order_qnty-isnull(d1.real_qnty,a.sale_qnty) tiaozheng
from sm_t_salesheet_detail a  left join bi_t_item_info b on a.item_no=b.item_no  
LEFT JOIN dbo.bi_t_branch_info bi ON bi.branch_no=a.branch_no_d  
left join sm_t_salesheet_recpay_detail d1 on d1.sheet_no=a.sheet_no and d1.task_flow_id=a.flow_id and  a.item_no=d1.item_no
LEFT JOIN dbo.k3_t_item_info_expand ie ON ie.item_no = b.item_no 
 ";
            sql += " where a.sheet_no='" + sheet_no + "' order by a.flow_id ";
            tb2 = db.ExecuteToTable(sql, null);


        }

        void IInOutBLL.AddSaleSheet(body.sm_t_salesheet ord, List<body. sm_t_salesheet_detail> lines,
            out string sheet_no)
        {
            sheet_no = "";
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();

                db.BeginTran();
                sheet_no = MaxCode(d, "SO");
                ord.sheet_no = sheet_no;
                ord.create_time = DateTime.Now;
                ord.oper_date = DateTime.Now;
                ord.update_time = ord.create_time;
                //sm_t_salesheet_recpay_main recpayMain = new sm_t_salesheet_recpay_main();
                //recpayMain.sheet_no = ord.sheet_no;
                //recpayMain.approve_date = ord.approve_date;
                //recpayMain.approve_flag = ord.approve_flag;
                //recpayMain.approve_man = ord.approve_man;
                //recpayMain.branch_no = ord.branch_no;
                //recpayMain.create_time = ord.create_time;
                //recpayMain.cust_no = ord.cust_no;
                //recpayMain.memo = ord.payfee_memo;
                //recpayMain.num1 = ord.num1;
                //recpayMain.real_qnty = ord.real_qnty;
                //recpayMain.num3 = ord.num3;
                //recpayMain.other1 = ord.other1;
                //recpayMain.other2 = ord.other2;
                //recpayMain.other3 = ord.other3;
                //recpayMain.oper_id = ord.oper_id;
                //recpayMain.oper_date = ord.oper_date;
                //recpayMain.pay_date = ord.pay_date;
                //recpayMain.voucher_no = ord.voucher_no;
                //recpayMain.update_time = ord.update_time;

                BatchProcessing.SaveSaleSheetBatch(d, ord.branch_no, lines, out lines);
                ord.total_amount = lines.Sum(l => l.sale_money);

                //
                d.Insert(ord);
                //d.Insert(recpayMain);


                foreach (sm_t_salesheet_detail line in lines)
                {
                    body.sm_t_salesheet_recpay_detail detail = new body.sm_t_salesheet_recpay_detail();
                    string sql = "select isnull(max(flow_id),0) + 1 as flow_id from sm_t_salesheet_detail ";
                    line.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    line.sheet_no = ord.sheet_no;
                    sql = @" select isnull(order_qnty,0) order_qnty from [dbo].[co_t_order_child] 
                    where item_no = '" + line.item_no + "'  and sheet_no = '" + ord.voucher_no + "'";
                    foreach (DataRow row in d.ExecuteToTable(sql, null).Rows)
                    {
                        line.order_qnty = row["order_qnty"].ToDecimal();
                    }

                    // = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    line.voucher_no = ord.voucher_no;

                    if (string.IsNullOrWhiteSpace(line.batch_num) && !string.IsNullOrWhiteSpace(ord.voucher_no))
                    {
                        sql = @" select batch_num from [dbo].[co_t_order_child] 
                    where item_no = '" + line.item_no + "'  and sheet_no = '" + ord.voucher_no + "'";
                        line.batch_num = Helper.Conv.ToString(d.ExecuteScalar(sql, null));
                    }

                    d.Insert(line);
                    //detail.sheet_no = ord.sheet_no;
                    //sql = "select isnull(max(flow_id),0)+1 from sm_t_salesheet_recpay_detail";
                    //detail.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    //detail.task_flow_id = line.flow_id;
                    //detail.item_no = line.item_no;
                    //detail.sheet_no = line.sheet_no;
                    //detail.real_qnty = line.sale_qnty;
                    //detail.real_qnty = detail.real_qnty;
                    ////sql = @" select isnull(order_qnty,0) from [dbo].[co_t_order_child] 
                    ////where item_no = '" + line.item_no + "'  and sheet_no = '" + ord.voucher_no + "'";
                    //detail.num1 = line.order_qnty;//订货数量
                    //detail.batch_num = line.batch_num;
                    //detail.voucher_no = ord.voucher_no;
                    //d.Insert(detail);
                }

                //
                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("InOutBLL.AddSaleSheet()", ex.ToString(), ord.sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        void IInOutBLL.ChangeSaleSheet(body.sm_t_salesheet ord, List<body.sm_t_salesheet_detail> lines)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                //
                string sql = "select approve_flag,update_time from sm_t_salesheet where sheet_no='" + ord.sheet_no +
                             "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在[" + ord.sheet_no + "]");
                }
                else
                {
                    var row = tb.Rows[0];
                    if (row["approve_flag"].ToString() == "1")
                    {
                        throw new Exception("单据已审核[" + ord.sheet_no + "]");
                    }

                    if (Helper.Conv.ToDateTime(row["update_time"]) > ord.update_time)
                    {
                        throw new Exception("单据已被他人修改[" + ord.sheet_no + "]");
                    }
                }

                sql = "delete from sm_t_salesheet_detail where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from sm_t_salesheet where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from sm_t_salesheet_recpay_main where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from sm_t_salesheet_recpay_detail where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                BatchProcessing.SaveSaleSheetBatch(d, ord.branch_no, lines, out lines);
                ord.total_amount = lines.Sum(l => l.sale_money);

                ord.update_time = DateTime.Now;
                //sm_t_salesheet_recpay_main recpayMain = new sm_t_salesheet_recpay_main();
                //recpayMain.sheet_no = ord.sheet_no;
                //recpayMain.approve_date = ord.approve_date;
                //recpayMain.approve_flag = ord.approve_flag;
                //recpayMain.approve_man = ord.approve_man;
                //recpayMain.branch_no = ord.branch_no;
                //recpayMain.create_time = ord.create_time;
                //recpayMain.cust_no = ord.cust_no;
                //recpayMain.memo = ord.payfee_memo;
                //recpayMain.num1 = ord.num1;
                //recpayMain.real_qnty = ord.real_qnty;
                //recpayMain.num3 = ord.num3;
                //recpayMain.other1 = ord.other1;
                //recpayMain.other2 = ord.other2;
                //recpayMain.other3 = ord.other3;
                //recpayMain.oper_id = ord.oper_id;
                //recpayMain.oper_date = ord.oper_date;
                //recpayMain.pay_date = ord.pay_date;
                //recpayMain.voucher_no = ord.voucher_no;
                //recpayMain.update_time = ord.update_time;
                d.Insert(ord);
                //d.Insert(recpayMain);
                foreach (sm_t_salesheet_detail line in lines)
                {
                    body.sm_t_salesheet_recpay_detail detail = new body.sm_t_salesheet_recpay_detail();
                    sql = "select isnull(max(flow_id),0) + 1 as flow_id from sm_t_salesheet_detail ";
                    line.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    line.sheet_no = ord.sheet_no;
                    sql = @" select isnull(order_qnty,0) order_qnty from [dbo].[co_t_order_child] 
                    where item_no = '" + line.item_no + "'  and sheet_no = '" + ord.voucher_no + "'";
                    foreach (DataRow row in d.ExecuteToTable(sql, null).Rows)
                    {
                        line.order_qnty = row["order_qnty"].ToDecimal();
                    }

                    // = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    line.voucher_no = ord.voucher_no;
                    if (string.IsNullOrWhiteSpace(line.batch_num))
                    {
                        sql = @" select batch_num from [dbo].[co_t_order_child] where item_no = '" + line.item_no + "'  and sheet_no = '" + ord.voucher_no + "'";
                        line.batch_num = Helper.Conv.ToString(d.ExecuteScalar(sql, null));
                    }


                    d.Insert(line);
                    //detail.sheet_no = ord.sheet_no;
                    //sql = "select isnull(max(flow_id),0)+1 from sm_t_salesheet_recpay_detail";
                    //detail.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    //detail.task_flow_id = line.flow_id;
                    //detail.item_no = line.item_no;
                    //detail.sheet_no = line.sheet_no;
                    //detail.real_qnty = line.sale_qnty;
                    //detail.real_qnty = detail.real_qnty;
                    ////sql = @" select isnull(order_qnty,0) from [dbo].[co_t_order_child] 
                    ////where item_no = '" + line.item_no + "'  and sheet_no = '" + ord.voucher_no + "'";
                    //detail.num1 = line.order_qnty;//订货数量
                    //detail.batch_num = line.batch_num;
                    //detail.voucher_no = ord.voucher_no;
                    //d.Insert(detail);
                }

                //
                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("InOutBLL.ChangeSaleSheet()", ex.ToString(), ord.sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        void IInOutBLL.DeleteSaleSheet(string sheet_no, DateTime update_time)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();

                db.BeginTran();
                //
                string sql = "select approve_flag,update_time from sm_t_salesheet where sheet_no='" + sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在[" + sheet_no + "]");
                }
                else
                {
                    var row = tb.Rows[0];
                    if (row["approve_flag"].ToString() == "1")
                    {
                        throw new Exception("单据已审核[" + sheet_no + "]");
                    }

                    if (Helper.Conv.ToDateTime(row["update_time"]) > update_time)
                    {
                        throw new Exception("单据已被他人修改[" + sheet_no + "]");
                    }
                }

                sql = "delete from sm_t_salesheet_detail where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from sm_t_salesheet where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("InOutBLL.DeleteSaleSheet()", ex.ToString(), sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        void IInOutBLL.CheckSaleSheet(string sheet_no, string approve_man, DateTime update_time)
        {
            CheckSheet check = new CheckSheet();
            check.CheckSOSheet(sheet_no, approve_man, update_time);
            CheckSaleSheetRecpay(sheet_no, approve_man, update_time);
        }

        void CheckSaleSheetRecpay(string sheet_no, string approve_man, DateTime update_time)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"select * from sm_t_salesheet_recpay_main where sheet_no='" + sheet_no + "'";
            if (db.ExecuteToTable(sql, null).Rows.Count > 0)
            {
                sql = "update sm_t_salesheet_recpay_main set approve_man='" + approve_man +
                      "',approve_flag='1',update_time='" + update_time + "' where sheet_no='" + sheet_no + "'";
                db.ExecuteToTable(sql, null);
            }


        }

        DataTable IInOutBLL.GetSaleSheetExport(string sheet_no, string sup_no, DateTime oper_date)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = $@"
SELECT  c.supcust_no,c.sup_name,CONVERT(VARCHAR(10),s.oper_date,20) oper_date,c.sup_man,c.sup_tel,ic.item_clsno,ic.item_clsname,
ic2.item_clsno item_clsno2,ic2.item_clsname item_clsname2,i.item_name,i.item_subno,i.unit_no,i.item_size,g.order_qnty,
SUM(d.sale_qnty) sale_qnty,
(SUM(CASE WHEN d.sale_money=0 THEN 0 ELSE d.sale_money end)/SUM(CASE WHEN d.sale_qnty=0 THEN 1 ELSE d.sale_qnty end)) sale_price,
SUM(d.sale_money) sale_money,d.other5
FROM dbo.sm_t_salesheet s
LEFT JOIN  dbo.sm_t_salesheet_detail d ON s.sheet_no=d.sheet_no
LEFT JOIN dbo.bi_t_item_info i ON i.item_no=d.item_no
LEFT JOIN dbo.bi_t_supcust_info c ON c.supcust_no=s.cust_no AND c.supcust_flag='C'
LEFT JOIN dbo.bi_t_item_cls ic ON ic.item_clsno=SUBSTRING(i.item_clsno,1,2) 
LEFT JOIN dbo.bi_t_item_cls ic2 ON ic2.item_clsno=i.item_clsno
LEFT JOIN (
SELECT  c.item_no,SUM(c.order_qnty) order_qnty
FROM dbo.co_t_order_main m
LEFT JOIN dbo.co_t_order_child c ON m.sheet_no=c.sheet_no
WHERE m.sheet_no LIKE 'SS%' AND CONVERT(VARCHAR(10),m.valid_date,20) = '{oper_date.Toyyyy_MM_dd()}'
and m.sup_no='{sup_no}'
GROUP BY c.item_no
) g ON g.item_no=d.item_no 
WHERE d.sheet_no IN ({sheet_no})
GROUP BY c.supcust_no,c.sup_name,CONVERT(VARCHAR(10),s.oper_date,20),c.sup_man,c.sup_tel,ic.item_clsno,ic.item_clsname,
ic2.item_clsno,ic2.item_clsname,i.item_name,i.item_subno,i.unit_no,i.item_size,g.order_qnty,d.other5
ORDER BY c.supcust_no,ic.item_clsno,ic2.item_clsno
";

            DataTable tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        DataTable IInOutBLL.GetImportSSSheet()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = $@"SELECT c.sheet_no,
       c.sup_no,
       c.sup_no + '/' + sup.sup_name supcust,
	   c.branch_no,
	   c.branch_no+'/'+b.branch_name branch,
	   c.oper_date
FROM dbo.co_t_order_main c
    LEFT JOIN dbo.sm_t_salesheet s
        ON s.voucher_no = c.sheet_no
    LEFT JOIN dbo.bi_t_branch_info b
        ON b.branch_no = c.branch_no
    LEFT JOIN dbo.bi_t_supcust_info sup
        ON sup.supcust_no = c.sup_no AND  sup.supcust_flag='C' 
WHERE s.sheet_no IS NULL
      AND c.order_status = '0'
      AND c.approve_flag = '1'
      AND c.sheet_no LIKE 'SS%' ";

            DataTable tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        System.Data.DataTable IInOutBLL.GetSaleSSSheetList(string date1, string date2, string sup_no, string order_main)
        {
            var condition_sql = "";
            if (sup_no != "") condition_sql += " and a.sup_no  = '" + sup_no + "' ";
            if (order_main != "") condition_sql += " and a.order_main = '" + order_main + "' ";
            string sql =
                @"select a.*,b.branch_name,c.oper_name,d.oper_name as sale_man_name,e.oper_name as approve_man_name,f.sup_name
from co_t_order_main a 
left join (select * from bi_t_branch_info ) b on a.branch_no=b.branch_no 
left join (select supcust_no,sup_name from bi_t_supcust_info where supcust_flag='C') f on a.sup_no=f.supcust_no 
left join (select * from sa_t_operator_i) c on a.oper_id=c.oper_id 
left join bi_t_people_info d on a.order_man=d.oper_id 
left join (select * from sa_t_operator_i) e on a.approve_man=e.oper_id ";
            sql += "where  a.sheet_no LIKE 'SS%' and a.oper_date>='" + date1 + " 00:00:00' and a.oper_date<='" + date2 +
                   " 23:59:59' " + condition_sql;
            sql += "order by a.sheet_no desc ";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);

            return tb;
        }

        void IInOutBLL.GetSaleSSSheet(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql =
                $@"select a.*,b.branch_name,c.oper_name,d.oper_name as order_man_name,e.oper_name as approve_man_name,f.sup_name,f.sup_fullname 
from co_t_order_main a
left
join (select *from bi_t_branch_info ) b on a.branch_no = b.branch_no
left join(select supcust_no, sup_name,sup_fullname from bi_t_supcust_info where supcust_flag= 'C') f on a.sup_no = f.supcust_no
left join(select* from sa_t_operator_i) c on a.oper_id = c.oper_id
left join bi_t_people_info d on a.order_man = d.oper_id
left join(select* from sa_t_operator_i) e on a.approve_man = e.oper_id
where a.sheet_no='{sheet_no}'";
            tb1 = db.ExecuteToTable(sql, null);

            sql = $@"select b.item_name,a.*,b.item_subno, b.item_size,b.unit_no ,c.branch_name,c.branch_no branch_no_d
FROM dbo.co_t_order_child a
left join bi_t_item_info b on a.item_no = b.item_no 
left join [dbo].[bi_t_branch_info] c on b.branch_no=c.branch_no
where a.sheet_no='{sheet_no}'";

            tb2 = db.ExecuteToTable(sql, null);
        }

        void IInOutBLL.AddSaleSSSheet(co_t_order_main ord, List<co_t_order_child> lines, string is_gen_cg,
            out string sheet_no)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();

                db.BeginTran();
                sheet_no = MaxCode(d, "SS");
                ord.sheet_no = sheet_no;
                ord.oper_date = DateTime.Now;
                ord.update_time = DateTime.Now;
                //
                d.Insert(ord);

                foreach (co_t_order_child line in lines)
                {
                    //string sql = "select isnull(max(flow_id),0) + 1 as flow_id from sm_t_salesheet_detail ";
                    // line.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    line.sheet_no = sheet_no;
                    //line.batch_num = BatchNum(ord.sup_no);
                    d.Insert(line, "flow_id");
                }

                //生成采购订单
                if ("1".Equals(is_gen_cg))
                {
                    d.ExecuteScalar(
                        "UPDATE dbo.bi_t_item_info SET sup_no='00' WHERE sup_no='' or sup_no IS NULL OR LEN(sup_no)=0",
                        null);

                    IPriceBLL priceBll = new PriceBLL();
                    DataTable tb = d.ExecuteToTable("SELECT * FROM dbo.bi_t_item_info", null);
                    List<bi_t_item_info> itemInfos = (from DataRow row in tb.Rows
                                                      select ReflectionHelper.DataRowToModel<bi_t_item_info>(row)).ToList();
                    var quey = from item in lines
                               join line in itemInfos on item.item_no equals line.item_no
                               group item by line.sup_no;

                    foreach (var p in quey)
                    {
                        string sup_no = p.Key;
                        ord = new co_t_order_main
                        {
                            sheet_no = MaxCode(d, "PO"),
                            branch_no = "0001",
                            sup_no = sup_no,
                            coin_code = "RMB",
                            paid_amount = 0,
                            oper_id = ord.oper_id,
                            order_man = ord.order_man,
                            oper_date = DateTime.Now,
                            trans_no = "P",
                            order_status = "0",
                            sale_way = "A",
                            p_sheet_no = "",
                            approve_flag = "0",
                            other1 = "",
                            cm_branch = "00",
                            approve_man = "00",
                            approve_date = System.DateTime.MinValue,
                            num1 = 0,
                            num2 = 0,
                            num3 = 0,
                            valid_date = DateTime.Now.AddDays(1),
                            update_time = DateTime.Now,
                            memo = "销售订单：" + sheet_no + "，自动生成采购订单"
                        };

                        var childs = new List<co_t_order_child>();
                        int index = 0;
                        decimal total_amt = 0;
                        foreach (co_t_order_child item in p)
                        {
                            var line = new co_t_order_child
                            {
                                sheet_no = ord.sheet_no,
                                item_no = item.item_no,
                                unit_no = item.unit_no,
                                barcode = item.barcode,
                                unit_factor = 1,
                                discount = 1,
                                other1 = "",
                                other2 = "",
                                voucher_no = "",
                                sheet_sort = ++index,
                                num1 = 0,
                                num2 = 0,
                                num3 = 0,
                                packqty = 0,
                                sgqty = 0,
                                in_price = priceBll.GetSupItemPrice(sup_no, item.item_no, "0"),
                                order_qnty = item.order_qnty,

                            };
                            // line.batch_num = BatchNum(ord.sheet_no, line.item_no);
                            line.sub_amount = line.in_price * item.order_qnty;
                            total_amt += line.sub_amount;
                            childs.Add(line);
                        }

                        ord.total_amount = total_amt;
                        d.Insert(ord);
                        childs.ForEach(c => d.Insert(c, "flow_id"));
                    }
                }

                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("InOutBLL.AddSaleSSSheet()", ex.ToString(), ord.sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
        void IInOutBLL.ChangeSaleSSSheet(co_t_order_main ord, List<co_t_order_child> lines)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                //
                string sql = "select approve_flag,update_time from co_t_order_main where sheet_no='" + ord.sheet_no +
                             "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在[" + ord.sheet_no + "]");
                }
                else
                {
                    var row = tb.Rows[0];
                    if (row["approve_flag"].ToString() == "1")
                    {
                        throw new Exception("单据已审核[" + ord.sheet_no + "]");
                    }

                    if (Helper.Conv.ToDateTime(row["update_time"]) > ord.update_time)
                    {
                        throw new Exception("单据已被他人修改[" + ord.sheet_no + "]");
                    }
                }

                sql = "delete from co_t_order_child where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from co_t_order_main where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                ord.update_time = DateTime.Now;
                d.Insert(ord);
                foreach (co_t_order_child line in lines)
                {
                    // sql = "select isnull(max(flow_id),0) + 1 as flow_id from sm_t_salesheet_detail ";
                    //line.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    line.flow_id = 0;
                    //line.batch_num = BatchNum(ord.sup_no);
                    d.Insert(line, "flow_id");
                }

                //
                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("InOutBLL.ChangeSaleSSSheet()", ex.ToString(), ord.sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        void IInOutBLL.CGInSO(string Sal, string Pal)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            string sql = "";
            string ssheet_nos = Sal.Substring(0, Sal.Length - 1);
            string pal = "";
            string psheet_nos=Pal.Substring(0, Pal.Length - 1);
            try
            {
                db.Open();
                db.BeginTran();
                sql = "update co_t_order_child set batch_num='' where sheet_no in (" + ssheet_nos + ")  \r\n " +
                      "update co_t_order_child set batch_num='' where sheet_no in (" + psheet_nos + ")";
                d.ExecuteToTable(sql, null);

                #region 数量、供应商都能对的上的

                StringBuilder sb = new StringBuilder();
                    sql =
                        @"select a.flow_id,a.pick_barcode,b.oper_id,supcust_no,b.sup_no,a.sheet_no,b.p_sheet_no,a.batch_num,a.item_no,a.real_qty,order_qnty,oper_date,pick_barcode
from [dbo].[co_t_order_child] a 
left join co_t_order_main b on a.sheet_no=b.sheet_no
where 
a.sheet_no in ("+psheet_nos+") and b.p_sheet_no like 'CGDD%' " +
                        "";
                    DataTable dt11 = d.ExecuteToTable(sql, null);
                    if (dt11.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt11.Rows)
                        {
                            sql =
                                @"select flow_id,ph_sheet_no,pick_barcode,supcust_no,b.sup_no,a.sheet_no
,b.p_sheet_no,a.batch_num,a.item_no,a.real_qty,order_qnty,oper_date
from [dbo].[co_t_order_child] a 
left join co_t_order_main b on a.sheet_no=b.sheet_no
where 
 a.sheet_no like 'SS%'
and (batch_num is null or batch_num = '')
and a.item_no='" + item["item_no"].ToString() + "' and sup_no='" + item["supcust_no"].ToString() +
                                "'  and a.order_qnty='" + item["order_qnty"].ToString() +
                                "' and a.sheet_no in (" + ssheet_nos + ") ";

                            DataTable dt = d.ExecuteToTable(sql, null);
                            if (dt.Rows.Count > 0)
                            {
                                string flow_id = item["flow_id"].ToString();

                                string index = Index(flow_id);
                                string batch_num = CreateBatchNum(item["sup_no"].ToString(),
                                    DateTime.Now.ToString("yyMMdd"), index);
                                sb.Append(@"update  co_t_order_child set other3='" + flow_id +
                                          "',batch_num='" + batch_num + "'" +
                                          " where  flow_id='" + item["flow_id"] + "'\r\n " +
                                          " \r\n" +
                                          "update  co_t_order_child set voucher_no='" + item["sheet_no"].ToString() +
                                          "',batch_num='" + batch_num + "',pick_barcode='" + item["pick_barcode"] + "',supcust_no='" + item["sup_no"] + "'" +
                                          " where flow_id='" + dt.Rows[0]["flow_id"] + "' \r\n");

                                if (sb.Length >= 3000)
                                {
                                    try
                                    {
                                        d.ExecuteScalar(sb.ToString(), null);
                                        LogHelper.writeLog(sb.ToString());
                                        sb.Clear();
                                    }
                                    catch
                                    {
                                        throw new Exception("生成关联关系失败！");
                                    }
                                }
                            }

                        }

                        if (sb.Length > 0)
                        {
                            try
                            {

                                d.ExecuteScalar(sb.ToString(), null);
                                LogHelper.writeLog(sb.ToString());
                                sb.Clear();

                            }
                            catch
                            {
                                throw new Exception("生成关联关系失败！");
                            }
                        }
                    }

                    #endregion
//                    #region 合单的情况（匹配剩余的）

//                    StringBuilder sb1 = new StringBuilder();
//                    sql =
//                        @"select a.pick_barcode,flow_id,b.oper_id,supcust_no,b.sup_no,a.sheet_no,b.p_sheet_no
//,a.batch_num,a.item_no,a.real_qty,order_qnty,oper_date,pick_barcode
//from [dbo].[co_t_order_child] a 
//left join co_t_order_main b on a.sheet_no=b.sheet_no
//where 
//a.sheet_no in (" + psheet_nos + ") and b.p_sheet_no like 'CGDD%' and (batch_num is null or batch_num = '' )";
//                    dt11 = d.ExecuteToTable(sql, null);
//                    if (dt11.Rows.Count > 0)
//                    {
//                        foreach (DataRow item in dt11.Rows)
//                        {
//                            sql =
//                                @"select flow_id,ph_sheet_no, pick_barcode, supcust_no, b.sup_no,a.sheet_no
//                        ,b.p_sheet_no,a.batch_num,a.item_no,a.real_qty,order_qnty,oper_date
//                        from[dbo].[co_t_order_child] a
//                        left join co_t_order_main b on a.sheet_no=b.sheet_no
//                        where (a.sheet_no like 'PP%' or a.sheet_no like 'SS%')
//                        and  (batch_num='' or batch_num is null) 
//                        and a.item_no='" + item["item_no"].ToString() + "'" +
//                                " and sup_no = '" + item["supcust_no"].ToString() + "' " +
//                                "and  a.sheet_no in (" + ssheet_nos + ")";
//                            DataTable dd = d.ExecuteToTable(sql, null);
//                            decimal sum = 0;
//                            //foreach (DataRow row in dd.Rows)
//                            //{
//                            //    sum += row["order_qnty"].ToDecimal();
//                            //}

//                            //if (sum == item["order_qnty"].ToDecimal())
//                            //{
//                            if (dd.Rows.Count > 0)
//                            {
//                                string index = Index(dd.Rows[0]["flow_id"].ToString());
//                                string batch_num = CreateBatchNum(item["sup_no"].ToString(),
//                                    DateTime.Now.ToString("yyMMdd"), index);

//                                foreach (DataRow row in dd.Rows)
//                                {
//                                    sb1.Append(@"update  co_t_order_child set other3='" + item["sheet_no"] +
//                                               "',batch_num='" + batch_num + "',pick_barcode='" +
//                                              item["pick_barcode"] + "',supcust_no='" + item["sup_no"] + "' " +
//                                               " where flow_id='" + row["flow_id"] + "' \r\n");
//                                }

//                                sb1.Append("update co_t_order_child set batch_num='" + batch_num + "' where flow_id='" + item["flow_id"] +
//                                           "'\r\n");
//                            }

//                            if (sb1.Length >= 3000)
//                            {
//                                try
//                                {
//                                    d.ExecuteScalar(sb1.ToString(), null);
//                                    LogHelper.writeLog(sb.ToString());
//                                    sb1.Clear();
//                                }
//                                catch
//                                {
//                                    throw new Exception("生成关联关系失败！");
//                                }
//                            }

//                            //}

//                        }

//                        if (sb1.Length > 0)
//                        {
//                            try
//                            {
//                                d.ExecuteScalar(sb1.ToString(), null);
//                                LogHelper.writeLog(sb.ToString());
//                                sb1.Clear();
//                            }
//                            catch (Exception e)
//                            {
//                                throw new Exception("生成关联关系失败！");
//                            }
//                        }
//                    }

//                    #endregion
//                    #region 通过商品匹配
//                    StringBuilder sb4 = new StringBuilder();
//                    sql =
//                        @"select a.flow_id,a.pick_barcode,b.oper_id,supcust_no,b.sup_no,a.sheet_no,b.p_sheet_no,a.batch_num,a.item_no,a.real_qty,order_qnty,oper_date,pick_barcode
//from [dbo].[co_t_order_child] a 
//left join co_t_order_main b on a.sheet_no=b.sheet_no
//where 
//a.sheet_no in (" + psheet_nos + ") and b.p_sheet_no like 'CGDD%'  " +
//                        "and (batch_num='' or batch_num is null) " +
//                        "";
//                    dt11 = d.ExecuteToTable(sql, null);
//                    if (dt11.Rows.Count > 0)
//                    {
//                        foreach (DataRow item in dt11.Rows)
//                        {
//                            sql =
//                                @"select flow_id,ph_sheet_no,pick_barcode,supcust_no,b.sup_no,a.sheet_no,b.p_sheet_no,a.batch_num,a.item_no,a.real_qty,order_qnty,oper_date
//from [dbo].[co_t_order_child] a 
//left join co_t_order_main b on a.sheet_no=b.sheet_no
//where 
//(a.sheet_no like 'PP%' or a.sheet_no like 'SS%')

//and a.item_no='" + item["item_no"].ToString() + "' and  (batch_num='' or batch_num is null) " +
//                                " and a.sheet_no in (" + ssheet_nos + ")  order by flow_id desc";

//                            DataTable dt = d.ExecuteToTable(sql, null);
//                            if (dt.Rows.Count > 0)
//                            {
//                                string flow_id = item["flow_id"].ToString();
//                                string index = Index(flow_id);
//                                string batch_num = CreateBatchNum(item["sup_no"].ToString(),
//                                    DateTime.Now.ToString("yyMMdd"), index);
//                                sb.Append(@"update  co_t_order_child set other3='" + flow_id +
//                                          "',batch_num='" + batch_num + "'" +
//                                          " where  flow_id='" + item["flow_id"] + "'\r\n " +
//                                          " \r\n" +
//                                          "update  co_t_order_child set other3='" + item["sheet_no"].ToString() +
//                                          "',batch_num='" + batch_num + "',pick_barcode='" + item["pick_barcode"] + "',supcust_no='" + item["sup_no"] + "'" +
//                                          " where flow_id='" + dt.Rows[0]["flow_id"] + "' \r\n");

//                                if (sb.Length >= 3000)
//                                {
//                                    try
//                                    {
//                                        d.ExecuteScalar(sb.ToString(), null);
//                                        LogHelper.writeLog(sb.ToString());
//                                        sb.Clear();
//                                    }
//                                    catch
//                                    {
//                                        throw new Exception("生成关联关系失败！");
//                                    }
//                                }
//                            }

//                        }

//                        if (sb.Length > 0)
//                        {
//                            try
//                            {

//                                d.ExecuteScalar(sb.ToString(), null);
//                                LogHelper.writeLog(sb.ToString());
//                                sb.Clear();

//                            }
//                            catch
//                            {
//                                throw new Exception("生成关联关系失败！");
//                            }
//                        }
//                    }


//                    #endregion
//                    #region 采购订单未匹配到商品的

//                    StringBuilder sb2 = new StringBuilder();
//                    sql =
//                        @"select a.pick_barcode,flow_id,b.oper_id,supcust_no,b.sup_no,a.sheet_no,b.p_sheet_no,a.batch_num,a.item_no,a.real_qty,order_qnty,oper_date
//from [dbo].[co_t_order_child] a 
//left join co_t_order_main b on a.sheet_no=b.sheet_no
//where 
//a.sheet_no in (" + psheet_nos + ") and b.p_sheet_no like 'CGDD%' and (batch_num is null or batch_num = '' or batch_num like '000%')";
//                    dt11 = d.ExecuteToTable(sql, null);
//                    if (dt11.Rows.Count > 0)
//                    {
//                        foreach (DataRow item in dt11.Rows)
//                        {
//                            string index = Index(item["flow_id"].ToString());
//                            string batch_num = CreateBatchNum(item["sup_no"].ToString(),
//                                DateTime.Now.ToString("yyMMdd"),
//                                index);

//                            sb2.Append("update co_t_order_child set batch_num='" + batch_num + "' where flow_id='" +
//                                       item["flow_id"] + "'");

//                            if (sb.Length >= 3000)
//                            {
//                                try
//                                {
//                                    d.ExecuteScalar(sb2.ToString(), null);
//                                    LogHelper.writeLog(sb.ToString());
//                                    sb2.Clear();
//                                }
//                                catch
//                                {
//                                    throw new Exception("生成关联关系失败！");
//                                }
//                            }

//                        }

//                        if (sb2.Length > 0)
//                        {
//                            try
//                            {
//                                d.ExecuteScalar(sb2.ToString(), null);
//                                LogHelper.writeLog(sb.ToString());
//                                sb2.Clear();
//                            }
//                            catch (Exception e)
//                            {
//                                throw new Exception("生成关联关系失败！");
//                            }
//                        }
//                    }

//                    #endregion
//                    #region 未匹配到销售订货单

//                    StringBuilder sb3 = new StringBuilder();
//                    sql =
//                        @"select flow_id,ph_sheet_no,pick_barcode,supcust_no,b.sup_no,a.sheet_no,b.p_sheet_no,a.batch_num,a.item_no,a.real_qty,order_qnty,oper_date
//from [dbo].[co_t_order_child] a 
//left join co_t_order_main b on a.sheet_no=b.sheet_no
//where (a.sheet_no like 'PP%'or a.sheet_no like 'SS%') " +
//                        "" +
//                        "and (batch_num is null or batch_num = '') and " +
//                        " a.sheet_no in (" + ssheet_nos + ")";

//                    DataTable dt1 = d.ExecuteToTable(sql, null);
//                    if (dt1.Rows.Count > 0)
//                    {
//                        foreach (DataRow dataRow in dt1.Rows)
//                        {
//                            string flow_id = dataRow["flow_id"].ToString();
//                            string index = Index(flow_id);
//                            string batch_num = CreateBatchNum("000".ToString(),
//                                Convert.ToDateTime(dataRow["oper_date"]).ToString("yyMMdd"), index);
//                            sql =
//                                @"select flow_id,supcust_no,b.sup_no,a.sheet_no
//                            from[dbo].[co_t_order_child] a
//                                left join co_t_order_main b on a.sheet_no = b.sheet_no 
//								where 
//								(supcust_no is not null and supcust_no !='' ) and 
//								(a.sheet_no like 'SS%' or a.sheet_no like 'PP%') and a.item_no='" +
//                                dataRow["item_no"] + "'  order by flow_id desc";
//                            string sup_no = "000";
//                            DataTable ddd = d.ExecuteToTable(sql, null);

//                            if (ddd.Rows.Count > 0)
//                            {
//                                sup_no = ddd.Rows[0]["supcust_no"].ToString();
//                                batch_num = CreateBatchNum(sup_no,
//                                    Convert.ToDateTime(dataRow["oper_date"]).ToString("yyMMdd"), index);
//                            }

//                            sb3.Append("update co_t_order_child set batch_num='" + batch_num + "',supcust_no='" + sup_no +
//                                       "' where flow_id='" +
//                                       flow_id + "'");

//                            if (sb3.Length >= 3000)
//                            {
//                                try
//                                {
//                                    d.ExecuteScalar(sb3.ToString(), null);
//                                    sb3.Clear();
//                                }
//                                catch (Exception e)
//                                {
//                                    throw new Exception("生成关联关系失败！");
//                                }
//                            }



//                        }

//                        if (sb3.Length > 0)
//                        {
//                            try
//                            {
//                                d.ExecuteScalar(sb3.ToString(), null);
//                                sb3.Clear();
//                            }
//                            catch (Exception e)
//                            {
//                                throw new Exception("生成关联关系失败！");
//                            }
//                        }


//                    }
//                    #endregion

//                    #region 判断是否有订单是否还有未赋值的

//                    sql =
//                        "select * from [dbo].[co_t_order_child] where batch_num = '' and batch_num is null and sheet_no in (" + psheet_nos + ")";
//                    DataTable d3 = d.ExecuteToTable(sql, null);
//                    if (d3.Rows.Count > 0)
//                    {
//                        sql = @"update co_t_order_main set is_batch_num='0' where sheet_no in (" + psheet_nos + ")";
//                        d.ExecuteScalar(sql, null);
//                    }

//                    if (d3.Rows.Count == 0)
//                    {
//                        sql = @"update co_t_order_main set is_batch_num='1' where sheet_no in (" + psheet_nos + ")";
//                        d.ExecuteScalar(sql, null);
//                    }
//                    #endregion
                
                #region 给所有的销售订单赋值is_batch_num
                sql = @"select * from [dbo].[co_t_order_child] where batch_num = '' and batch_num is null and sheet_no in (" + ssheet_nos + ")";
                DataTable d4 = d.ExecuteToTable(sql, null);
                if (d4.Rows.Count == 0)
                {
                    sql = @"update co_t_order_main set is_batch_num='1' where sheet_no in (" + ssheet_nos + ")";
                    d.ExecuteScalar(sql, null);
                }

                #endregion

               db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("InOutBLL.ChangeSaleSSSheet()", ex.ToString());
                db.RollBackTran();
                throw ex;
            }
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

        string CreateBatchNum(string sup_str, string dt_str, string index)
        {
            return sup_str + "_" + dt_str + "_" + index;
        }
        public void ChangeSaleSSheetGenPC(Dictionary<string, string> dic)
        {
            string sql = "";
            var db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                foreach (var k in dic)
                {
                    d.ExecuteScalar($@"UPDATE dbo.co_t_order_main SET is_gen_pc='{k.Value}' WHERE sheet_no='{k.Key}'", null);
                }

                db.CommitTran();
            }
            catch (Exception)
            {
                db.RollBackTran();
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        void IInOutBLL.DeleteSaleSSSheet(string sheet_no, DateTime update_time)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                //
                string sql = "select approve_flag,update_time from co_t_order_main where sheet_no='" + sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在[" + sheet_no + "]");
                }
                else
                {
                    var row = tb.Rows[0];
                    if (row["approve_flag"].ToString() == "1")
                    {
                        throw new Exception("单据已审核[" + sheet_no + "]");
                    }
                    if (Helper.Conv.ToDateTime(row["update_time"]) > update_time)
                    {
                        throw new Exception("单据已被他人修改[" + sheet_no + "]");
                    }
                }
                sql = "delete from co_t_order_child where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from co_t_order_main where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("InOutBLL.DeleteSaleSSSheet()", ex.ToString(), sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
        void IInOutBLL.CheckSaleSSSheet(string sheet_no, string approve_man, DateTime update_time)
        {
            CheckSheet check = new CheckSheet();
            check.CheckSSSheet(sheet_no, approve_man, update_time);
        }

        public DataTable GetSaleSSDetailSum(List<string> sheet_nos)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            StringBuilder sheet_no = new StringBuilder("'" + sheet_nos[0] + "'");
            foreach (var s in sheet_nos)
            {
                sheet_no.Append(",'" + s + "'");
            }


            string sql = string.Format(
                @"SELECT i.item_clsno,l.item_clsname,c.item_no,i.item_name,i.unit_no,SUM(c.order_qnty) order_qnty,i.item_loss
FROM dbo.co_t_order_main m
LEFT JOIN dbo.co_t_order_child c ON c.sheet_no = m.sheet_no
LEFT JOIN dbo.bi_t_item_info i ON i.item_no = c.item_no
LEFT JOIN dbo.bi_t_item_cls l ON l.item_clsno = i.item_clsno
WHERE m.sheet_no IN ({0})
GROUP BY i.item_clsno,l.item_clsname,c.item_no,i.item_name,i.unit_no,i.item_loss
ORDER BY i.item_clsno,l.item_clsname,c.item_no,i.item_name", sheet_no);


            var tb = db.ExecuteToTable(sql, null);

            tb.Columns.Add("cg_qty", typeof(decimal));

            foreach (DataRow row in tb.Rows)
            {
                decimal order_qnty = row["order_qnty"].ToDecimal();
                decimal item_loss = row["item_loss"].ToDecimal();

                row["cg_qty"] = order_qnty + order_qnty * (item_loss / 100);
            }

            return tb;
        }


        DataTable IInOutBLL.GetImportCGOrder()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = $@"SELECT c.sheet_no,
       c.sup_no,
       c.sup_no + '/' + sup.sup_name supcust,
	   c.branch_no,
	   c.branch_no+'/'+b.branch_name branch,
	   c.oper_date
FROM dbo.co_t_order_main c
    LEFT JOIN ic_t_inout_store_master s
        ON s.voucher_no = c.sheet_no
    LEFT JOIN dbo.bi_t_branch_info b
        ON b.branch_no = c.branch_no
    LEFT JOIN dbo.bi_t_supcust_info sup
        ON sup.supcust_no = c.sup_no AND  sup.supcust_flag='S' 
WHERE s.sheet_no IS NULL
      AND c.order_status = '0'
      AND c.approve_flag = '1'
      AND c.sheet_no LIKE 'PO%' ";

            DataTable tb = db.ExecuteToTable(sql, null);
            return tb;
        }
        DataTable IInOutBLL.GetCGOrderList(string date1, string date2, string sup_no, string order_main)
        {
            var condition_sql = "";
            if (sup_no != "") condition_sql += " and a.sup_no  = '" + sup_no + "' ";
            if (order_main != "") condition_sql += " and a.order_main = '" + order_main + "' ";
            string sql = @"select a.*,b.branch_name,c.oper_name,d.oper_name as sale_man_name,e.oper_name as approve_man_name,f.sup_name 
from co_t_order_main a 
left join (select * from bi_t_branch_info ) b on a.branch_no=b.branch_no 
left join (select supcust_no,sup_name from bi_t_supcust_info where supcust_flag='S') f on a.sup_no=f.supcust_no 
left join (select * from sa_t_operator_i) c on a.oper_id=c.oper_id 
left join bi_t_people_info d on a.order_man=d.oper_id 
left join (select * from sa_t_operator_i) e on a.approve_man=e.oper_id ";
            sql += "where  a.sheet_no LIKE 'PO%' and (a.oper_date between'" + date1 + " 00:00:00.000' and '" + date2 + " 23:59:59.999') " + condition_sql;
            sql += "order by a.sheet_no desc ";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);

            return tb;
        }
        DataTable IInOutBLL.GetCGOrderList(string date1, string date2, string sup_no, string order_main, int type)
        {
            var condition_sql = "";
            if (sup_no != "") condition_sql += " and a.sup_no  = '" + sup_no + "' ";
            if (order_main != "") condition_sql += " and a.order_main = '" + order_main + "' ";
            string sql = @"select a.*,b.branch_name,c.oper_name,d.oper_name as sale_man_name,e.oper_name as approve_man_name,f.sup_name 
from co_t_order_main a 
left join (select * from bi_t_branch_info ) b on a.branch_no=b.branch_no 
left join (select supcust_no,sup_name from bi_t_supcust_info where supcust_flag='S') f on a.sup_no=f.supcust_no 
left join (select * from sa_t_operator_i) c on a.oper_id=c.oper_id 
left join bi_t_people_info d on a.order_man=d.oper_id 
left join (select * from sa_t_operator_i) e on a.approve_man=e.oper_id ";
            sql += "where (ph_sheet_no is  null or ph_sheet_no='')  and   a.sheet_no LIKE 'PO%' and (a.oper_date between'" + date1 + " 00:00:00.000' and '" + date2 + " 23:59:59.999') " + condition_sql;
            sql += "order by a.sheet_no desc ";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);

            return tb;
        }

        public DataTable GetSaleSSSheetListPS(string date1, string date2, string sup_no, string order_main)
        {
            var condition_sql = "";
            if (sup_no != "") condition_sql += " and a.sup_no  = '" + sup_no + "' ";
            if (order_main != "") condition_sql += " and a.order_main = '" + order_main + "' ";
            string sql = @"select a.*,b.branch_name,c.oper_name,d.oper_name as sale_man_name,e.oper_name as approve_man_name,f.sup_name
from co_t_order_main a 
left join (select * from bi_t_branch_info ) b on a.branch_no=b.branch_no 
left join (select supcust_no,sup_name from bi_t_supcust_info where supcust_flag='C') f on a.sup_no=f.supcust_no 
left join (select * from sa_t_operator_i) c on a.oper_id=c.oper_id 
left join bi_t_people_info d on a.order_man=d.oper_id 
left join (select * from sa_t_operator_i) e on a.approve_man=e.oper_id ";
            sql += "where  a.sheet_no LIKE 'SS%' and a.valid_date>='" + date1 + " 00:00:00' and a.valid_date<='" + date2 + " 23:59:59' " + condition_sql;
            sql += "order by a.sheet_no desc ";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);

            return tb;
        }

        void IInOutBLL.GetCGOrder(string sheet_no, out DataTable tb1, out DataTable tb2)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = $@"select a.*,b.branch_name,c.oper_name,d.oper_name as order_man_name,e.oper_name as approve_man_name,f.sup_name 
from co_t_order_main a
left
join (select *from bi_t_branch_info ) b on a.branch_no = b.branch_no
left join(select supcust_no, sup_name from bi_t_supcust_info where supcust_flag= 'S') f on a.sup_no = f.supcust_no
left join(select* from sa_t_operator_i) c on a.oper_id = c.oper_id
left join bi_t_people_info d on a.order_man = d.oper_id
left join(select* from sa_t_operator_i) e on a.approve_man = e.oper_id
where a.sheet_no='{sheet_no}'";
            tb1 = db.ExecuteToTable(sql, null);

            sql = $@"select b.item_name,a.*,b.item_subno, b.item_size,b.unit_no ,c.branch_name,c.branch_no
FROM dbo.co_t_order_child a
left join bi_t_item_info b on a.item_no = b.item_no 
left join [dbo].[bi_t_branch_info] c on b.branch_no=c.branch_no
where a.sheet_no='{sheet_no}'";

            tb2 = db.ExecuteToTable(sql, null);
        }
        void IInOutBLL.AddCGOrder(co_t_order_main ord, List<co_t_order_child> lines, out string sheet_no)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();

                db.BeginTran();
                sheet_no = MaxCode(d, "PO");
                ord.sheet_no = sheet_no;
                ord.update_time = DateTime.Now;
                ord.oper_date = DateTime.Now;

                //
                d.Insert(ord);

                foreach (co_t_order_child line in lines)
                {
                    //string sql = "select isnull(max(flow_id),0) + 1 as flow_id from sm_t_salesheet_detail ";
                    // line.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    line.sheet_no = sheet_no;
                    d.Insert(line, "flow_id");
                }
                //
                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("InOutBLL.AddSaleSSSheet()", ex.ToString(), ord.sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
        void IInOutBLL.ChangeCGOrder(co_t_order_main ord, List<co_t_order_child> lines)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                //
                string sql = "select approve_flag,update_time from co_t_order_main where sheet_no='" + ord.sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在[" + ord.sheet_no + "]");
                }
                else
                {
                    var row = tb.Rows[0];
                    if (row["approve_flag"].ToString() == "1")
                    {
                        throw new Exception("单据已审核[" + ord.sheet_no + "]");
                    }
                    if (Helper.Conv.ToDateTime(row["update_time"]) > ord.update_time)
                    {
                        throw new Exception("单据已被他人修改[" + ord.sheet_no + "]");
                    }
                }
                sql = "delete from co_t_order_child where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from co_t_order_main where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                ord.update_time = DateTime.Now;
                d.Insert(ord);
                foreach (co_t_order_child line in lines)
                {
                    // sql = "select isnull(max(flow_id),0) + 1 as flow_id from sm_t_salesheet_detail ";
                    //line.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    line.flow_id = 0;
                    d.Insert(line, "flow_id");
                }
                //
                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("InOutBLL.ChangeSaleSSSheet()", ex.ToString(), ord.sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
        void IInOutBLL.DeleteCGOrder(string sheet_no, DateTime update_time)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                //
                string sql = "select approve_flag,update_time from co_t_order_main where sheet_no='" + sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在[" + sheet_no + "]");
                }
                else
                {
                    var row = tb.Rows[0];
                    if (row["approve_flag"].ToString() == "1")
                    {
                        throw new Exception("单据已审核[" + sheet_no + "]");
                    }
                    if (Helper.Conv.ToDateTime(row["update_time"]) > update_time)
                    {
                        throw new Exception("单据已被他人修改[" + sheet_no + "]");
                    }
                }
                sql = "delete from co_t_order_child where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from co_t_order_main where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "DELETE FROM dbo.ic_t_pspc_detail WHERE ph_sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("InOutBLL.DeleteSaleSSSheet()", ex.ToString(), sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
        void IInOutBLL.CheckCGOrder(string sheet_no, string approve_man, DateTime update_time)
        {
            CheckSheet check = new CheckSheet();
            check.CheckCGOrder(sheet_no, approve_man, update_time);
        }


        //trans_no: A进货入库  D退货入库  F退货出库  01其它入库   03其它出库
        System.Data.DataTable IInOutBLL.GetInOutList(string date1, string date2, string supcust_no, string trans_no)
        {
            var condition_sql = "";
            var supcust_flag = "S";
            if (supcust_no != "") condition_sql += " and a.supcust_no =@supcust_no ";
            if (trans_no == "D") supcust_flag = "C";
            string sql = "select a.*,b.branch_name,b1.branch_name d_branch_name,c.oper_name,d.oper_name as deal_man_name,e.oper_name as approve_man_name,f.sup_name ";
            sql += "from ic_t_inout_store_master a ";
            sql += "left join (select * from bi_t_branch_info ) b on a.branch_no=b.branch_no ";
            sql += "left join (select * from bi_t_branch_info ) b1 on a.d_branch_no=b1.branch_no ";
            sql += "left join (select supcust_no,sup_name from bi_t_supcust_info where supcust_flag='" + supcust_flag + "') f on a.supcust_no=f.supcust_no ";
            sql += "left join (select * from sa_t_operator_i) c on a.oper_id=c.oper_id ";
            sql += "left join bi_t_people_info d on a.deal_man=d.oper_id ";
            sql += "left join (select * from sa_t_operator_i) e on a.approve_man=e.oper_id ";
            sql += "where a.oper_date>='" + date1 + " 00:00:00.000' and a.oper_date<='" + date2 + " 23:59:59.999' and a.trans_no=@trans_no " + condition_sql;
            sql += "order by a.sheet_no desc ";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@supcust_no", supcust_no),
                new System.Data.SqlClient.SqlParameter("@trans_no", trans_no)
            };
            var tb = db.ExecuteToTable(sql, pars);

            return tb;
        }

        //trans_no: A进货入库  D退货入库  F退货出库  01其它入库   03其它出库
        System.Data.DataTable IInOutBLL.GetSimpleInOutList(string date1, string date2, string supcust_no, string trans_no)
        {
            string sql = @" select * ,(a.branch_no+'/'+b.branch_name) 仓库,(a.supcust_no+'/'+s.sup_name) 供应商
from ic_t_inout_store_master a
left join bi_t_supcust_info s on s.supcust_no=a.supcust_no and s.supcust_flag='S'
left join bi_t_branch_info b on b.branch_no=a.branch_no ";
            sql += "where a.oper_date>='" + date1 + " 00:00:00.000' and a.oper_date<='" + date2 + " 23:59:59.999' and a.supcust_no=@supcust_no and a.trans_no=@trans_no and a.approve_flag='1' ";
            sql += "order by a.sheet_no desc ";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@supcust_no", supcust_no),
                new System.Data.SqlClient.SqlParameter("@trans_no", trans_no)
            };
            var tb = db.ExecuteToTable(sql, pars);

            return tb;
        }


        //其它出入库：trans_no: 01:其它入库,02:归还,03:其它出库,04:领用出库,05:报损出库,06:借出,07:报溢入库,09:库存调整
        System.Data.DataTable IInOutBLL.GetOtherInOutList(string date1, string date2, string trans_no)
        {
            var condition_sql = "";
            if (trans_no != "") condition_sql += " and a.trans_no=@trans_no ";
            else condition_sql += " and a.trans_no in ('01','02','03','04','05','06','07','09') ";

            string sql = "select a.*,b.branch_name,c.oper_name,d.oper_name as deal_man_name,e.oper_name as approve_man_name ";
            sql += "from ic_t_inout_store_master a ";
            sql += "left join (select * from bi_t_branch_info ) b on a.branch_no=b.branch_no ";
            sql += "left join (select * from sa_t_operator_i) c on a.oper_id=c.oper_id ";
            sql += "left join bi_t_people_info d on a.deal_man=d.oper_id ";
            sql += "left join (select * from sa_t_operator_i) e on a.approve_man=e.oper_id ";
            sql += "where a.oper_date>='" + date1 + " 00:00:00.000' and a.oper_date<='" + date2 + " 23:59:59.999' " + condition_sql;
            sql += "order by a.sheet_no desc ";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@trans_no", trans_no)
            };
            var tb = db.ExecuteToTable(sql, pars);

            return tb;
        }

        void IInOutBLL.GetInOut(string sheet_no, string trans_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var supcust_flag = "S";
            if (trans_no == "D") supcust_flag = "C";
            string sql = "select a.*,b.branch_name,c.oper_name,d.oper_name as deal_man_name,e.oper_name as approve_man_name,f.sup_name,a.d_branch_no,b2.branch_name d_branch_name ";
            sql += "from ic_t_inout_store_master a ";
            sql += "left join (select * from bi_t_branch_info ) b on a.branch_no=b.branch_no ";
            sql += "LEFT JOIN (select * from bi_t_branch_info ) b2 on a.d_branch_no=b2.branch_no  ";
            sql += "left join (select supcust_no,sup_name from bi_t_supcust_info where supcust_flag='" + supcust_flag + "') f on a.supcust_no=f.supcust_no ";
            sql += "left join (select * from sa_t_operator_i) c on a.oper_id=c.oper_id ";
            sql += "left join bi_t_people_info d on a.deal_man=d.oper_id ";
            sql += "left join (select * from sa_t_operator_i) e on a.approve_man=e.oper_id ";
            sql += "where a.sheet_no='" + sheet_no + "' ";
            tb1 = db.ExecuteToTable(sql, null);


            sql = @"select a.*,b.item_subno,b.item_name,b.barcode,b.item_size,b.unit_no,b.price,bi.branch_no branch_no_d
                ,bi.branch_name,ie.is_batch,a.in_qty yingjie
, a.order_qnty dinghuo, isnull(d.real_qnty,a.in_qty) shishou, isnull(order_qnty,0.00)- isnull(d.real_qnty,a.in_qty) sunhao
            from ic_t_inout_store_detail a  left
            join bi_t_item_info b on a.item_no = b.item_no
            LEFT JOIN dbo.bi_t_branch_info bi ON bi.branch_no = b.branch_no
            LEFT JOIN dbo.k3_t_item_info_expand ie ON ie.item_no = b.item_no    
left join ic_t_inoutstore_recpay_detail d on a.sheet_no=d.sheet_no and d.task_flow_id=a.flow_id and d.item_no=a.item_no
  ";
            sql += " where a.sheet_no='" + sheet_no + "' order by a.flow_id ";
            tb2 = db.ExecuteToTable(sql, null);

        }

        //其它出入库明细
        void IInOutBLL.GetInOut(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select a.*,b.branch_name,c.oper_name,d.oper_name as deal_man_name,e.oper_name as approve_man_name ";
            sql += "from ic_t_inout_store_master a ";
            sql += "left join (select * from bi_t_branch_info ) b on a.branch_no=b.branch_no ";
            sql += "left join (select * from sa_t_operator_i) c on a.oper_id=c.oper_id ";
            sql += "left join bi_t_people_info d on a.deal_man=d.oper_id ";
            sql += "left join (select * from sa_t_operator_i) e on a.approve_man=e.oper_id ";
            sql += "where a.sheet_no='" + sheet_no + "' ";
            tb1 = db.ExecuteToTable(sql, null);

            sql = @"select a.*,b.item_subno,b.item_name,b.barcode,b.item_size
,b.unit_no,b.price,bi.branch_no branch_no_d,bi.branch_name,ie.is_batch
from ic_t_inout_store_detail a 
left join bi_t_item_info b on a.item_no = b.item_no
LEFT JOIN dbo.bi_t_branch_info bi ON bi.branch_no = b.branch_no
LEFT JOIN dbo.k3_t_item_info_expand ie ON ie.item_no = b.item_no    
left join ic_t_branch_stock c on b.item_no=c.item_no and b.branch_no=c.branch_no";
            sql += " where a.sheet_no='" + sheet_no + "' order by a.flow_id ";
            tb2 = db.ExecuteToTable(sql, null);
        }


        void IInOutBLL.AddInOut(Model.ic_t_inout_store_master ord, List<body.ic_t_inout_store_detail> lines, out string sheet_no)
        {
            sheet_no = "";
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                switch (ord.trans_no)
                {
                    case "A":
                        ord.sheet_no = sheet_no = MaxCode(d, "PI");
                        ord.db_no = "+";
                        BatchProcessing.SaveInSheetBatch(d, ord, lines);
                        break;
                    case "D":
                        ord.sheet_no = sheet_no = MaxCode(d, "RI");
                        ord.db_no = "+";
                        BatchProcessing.SaveInSheetBatch(d, ord, lines);
                        break;
                    case "F":
                        ord.sheet_no = sheet_no = MaxCode(d, "RO");
                        ord.db_no = "-";
                        BatchProcessing.SaveOutSheetBatch(d, ord.branch_no, lines, out lines);
                        break;
                    case "G":
                        ord.sheet_no = sheet_no = MaxCode(d, "IO");
                        ord.db_no = "";
                        break;
                    case "01":
                        ord.sheet_no = sheet_no = MaxCode(d, "OO");
                        ord.db_no = "+";
                        BatchProcessing.SaveInSheetBatch(d, ord, lines);
                        break;
                    case "02":
                        ord.sheet_no = sheet_no = MaxCode(d, "OO");
                        ord.db_no = "+";
                        BatchProcessing.SaveInSheetBatch(d, ord, lines);
                        break;
                    case "03":
                        ord.sheet_no = sheet_no = MaxCode(d, "OO");
                        ord.db_no = "-";
                        BatchProcessing.SaveOutSheetBatch(d, ord.branch_no, lines, out lines);
                        break;
                    case "04":
                        ord.sheet_no = sheet_no = MaxCode(d, "OO");
                        ord.db_no = "-";
                        BatchProcessing.SaveOutSheetBatch(d, ord.branch_no, lines, out lines);
                        break;
                    case "05":
                        sheet_no = MaxCode(d, "OO");
                        ord.sheet_no = ord.db_no = "-";
                        BatchProcessing.SaveOutSheetBatch(d, ord.branch_no, lines, out lines);
                        break;
                    case "06":
                        ord.sheet_no = sheet_no = MaxCode(d, "OO");
                        ord.db_no = "-";
                        BatchProcessing.SaveOutSheetBatch(d, ord.branch_no, lines, out lines);
                        break;
                    case "07":
                        ord.sheet_no = sheet_no = MaxCode(d, "OO");
                        ord.db_no = "+";
                        BatchProcessing.SaveInSheetBatch(d, ord, lines);
                        break;
                    case "10":
                        ord.sheet_no = sheet_no = MaxCode(d, "OO");
                        ord.db_no = "-";
                        BatchProcessing.SaveOutSheetBatch(d, ord.branch_no, lines, out lines);
                        break;
                    case "11":
                        ord.sheet_no = sheet_no = MaxCode(d, "OO");
                        ord.db_no = "+";
                        BatchProcessing.SaveInSheetBatch(d, ord, lines);
                        break;
                    case "12":
                        ord.sheet_no = sheet_no = MaxCode(d, "OO");
                        ord.db_no = "-";
                        BatchProcessing.SaveOutSheetBatch(d, ord.branch_no, lines, out lines);
                        break;
                    case "13":
                        ord.sheet_no = sheet_no = MaxCode(d, "OO");
                        ord.db_no = "+";
                        BatchProcessing.SaveInSheetBatch(d, ord, lines);
                        break;
                    case "14":
                        ord.sheet_no = sheet_no = MaxCode(d, "OO");
                        ord.db_no = "-";
                        BatchProcessing.SaveOutSheetBatch(d, ord.branch_no, lines, out lines);
                        break;
                    case "OM":
                        ord.sheet_no = sheet_no = MaxCode(d, "OM");
                        ord.db_no = "-";
                        ord.trans_no = "OM";
                        BatchProcessing.SaveOutSheetBatch(d, ord.branch_no, lines, out lines);
                        break;
                    case "RM":
                        ord.sheet_no = sheet_no = MaxCode(d, "RM");
                        ord.db_no = "+";
                        ord.trans_no = "RM";
                        BatchProcessing.SaveInSheetBatch(d, ord, lines);
                        break;
                    case "PE":
                        ord.sheet_no = ord.sheet_no = sheet_no = MaxCode(d, "PE");
                        ord.db_no = "+";
                        ord.trans_no = "PE";
                        BatchProcessing.SaveInSheetBatch(d, ord, lines);
                        break;
                    default:
                        ord.sheet_no = sheet_no = MaxCode(d, "OO");
                        break;
                }
                body.ic_t_inoutstore_recpay_main recpayMain = new body.ic_t_inoutstore_recpay_main();
                ord.sheet_no = sheet_no;
                ord.create_time = DateTime.Now;
                ord.update_time = ord.create_time;
                ord.oper_date = DateTime.Now;
                //recpayMain.sheet_no = ord.sheet_no;
                //recpayMain.approve_date = ord.approve_date;
                //recpayMain.approve_flag = ord.approve_flag;
                //recpayMain.approve_man = ord.approve_man;
                //recpayMain.branch_no = ord.branch_no;
                //recpayMain.create_time = ord.create_time;
                //recpayMain.cust_no = ord.supcust_no;
                //recpayMain.num1 = ord.num1;
                //recpayMain.num2 = ord.num2;
                //recpayMain.num3 = ord.num3;
                //recpayMain.other1 = ord.other1;
                //recpayMain.other2 = ord.other2;
                //recpayMain.other3 = ord.other3;
                //recpayMain.oper_id = ord.oper_id;
                //recpayMain.oper_date = ord.oper_date;
                //recpayMain.pay_date = ord.pay_date;
                //recpayMain.voucher_no = ord.voucher_no;
                //recpayMain.update_time = ord.update_time;

                //
                d.Insert(ord);
                //d.Insert(recpayMain);

                foreach (ic_t_inout_store_detail line in lines)
                {
                    string sql = "select isnull(max(flow_id),0) + 1 as flow_id from ic_t_inout_store_detail ";
                    line.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    line.voucher_no = ord.voucher_no;
                    sql = "select isnull(order_qnty,0.00) as order_qnty from co_t_order_child  where sheet_no='" + line.voucher_no + "' and item_no='" + line.item_no + "'";
                    foreach (DataRow row in d.ExecuteToTable(sql, null).Rows)
                    {
                        line.order_qnty = row["order_qnty"].ToDecimal();
                    }

                    if (!string.IsNullOrWhiteSpace(line.voucher_no) && string.IsNullOrWhiteSpace(line.batch_num))
                    {
                        sql = "select batch_num,pick_barcode from co_t_order_child  where sheet_no='" + line.voucher_no + "' and item_no='" + line.item_no + "'";
                        foreach (DataRow row in d.ExecuteToTable(sql, null).Rows)
                        {
                            line.batch_num = row["batch_num"].ToString();
                            //line.pick_barcode=row["batch_num"].ToString()
                        }
                    }

                    line.sheet_no = sheet_no;
                    d.Insert(line);
                    //ic_t_inoutstore_recpay_detail detail = new ic_t_inoutstore_recpay_detail();
                    //sql = "select isnull(max(flow_id),0) + 1 as flow_id from ic_t_inoutstore_recpay_detail ";
                    //detail.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    //detail.sheet_no = line.sheet_no;
                    //detail.batch_num = line.batch_num;
                    //detail.task_flow_id = line.flow_id;

                    //detail.voucher_no = line.voucher_no;
                    //detail.sheet_sort = line.sheet_sort;
                    //detail.real_qnty = line.in_qty;
                    //detail.num1 = line.order_qnty;

                    //sql = "select num3 from sm_t_salesheet_recpay_detail where batch_num='" + detail.batch_num + "'";
                    //detail.num2 = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    //detail.num3 = detail.real_qnty - detail.num2;
                    //detail.num4 = 0;
                    //detail.other1 = "";
                    //detail.other2 = "";
                    //detail.other3 = "";
                    //detail.other4 = "";
                    //detail.item_no = line.item_no;
                    //d.Insert(detail);
                }
                //
                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("InOutBLL.AddInOut()", ex.ToString(), ord.sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        void IInOutBLL.ChangeInOut(Model.ic_t_inout_store_master ord, List<body.ic_t_inout_store_detail> lines)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                //
                string sql = "select approve_flag,update_time from ic_t_inout_store_master where sheet_no='" + ord.sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在[" + ord.sheet_no + "]");
                }
                else
                {
                    var row = tb.Rows[0];
                    if (row["approve_flag"].ToString() == "1")
                    {
                        throw new Exception("单据已审核[" + ord.sheet_no + "]");
                    }
                    if (Helper.Conv.ToDateTime(row["update_time"]) > ord.update_time)
                    {
                        throw new Exception("单据已被他人修改[" + ord.sheet_no + "]");
                    }
                }
                sql = "delete from ic_t_inout_store_detail where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from ic_t_inout_store_master where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);

                BatchProcessing.SaveInOutSheetBatch(d, ord, lines, out lines);

                if (ord.trans_no == "A")
                {
                    ord.db_no = "+";
                }
                else if (ord.trans_no == "D")
                {
                    ord.db_no = "+";
                }
                else if (ord.trans_no == "F")
                {
                    ord.db_no = "-";
                }
                else if (ord.trans_no == "G")
                {
                    ord.db_no = "";
                }
                else if (ord.trans_no == "PO")
                {
                    ord.db_no = "";
                }
                else if (ord.trans_no == "01")
                {
                    ord.db_no = "+";
                }
                else if (ord.trans_no == "03")
                {
                    ord.db_no = "-";
                }
                else if (ord.trans_no == "OM")
                {
                    ord.db_no = "-";
                }
                else if (ord.trans_no == "RM")
                {
                    ord.db_no = "+";
                }
                else if (ord.trans_no == "PE")
                {
                    ord.db_no = "+";
                }
                else
                {
                    ord.db_no = "";
                }
                //ord.update_time = DateTime.Now;
                //d.Insert(ord);
                //foreach (ic_t_inout_store_detail line in lines)
                //{
                //    sql = "select isnull(max(flow_id),0) + 1 as flow_id from ic_t_inout_store_detail ";
                //    line.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                //    d.Insert(line);
                //}
                //
                //ic_t_inoutstore_recpay_main recpayMain = new ic_t_inoutstore_recpay_main();
                //ord.sheet_no = sheet_no;
                //ord.create_time = DateTime.Now;
                ord.update_time = ord.create_time;
                ord.oper_date = DateTime.Now;
                //recpayMain.sheet_no = ord.sheet_no;
                //recpayMain.approve_date = ord.approve_date;
                //recpayMain.approve_flag = ord.approve_flag;
                //recpayMain.approve_man = ord.approve_man;
                //recpayMain.branch_no = ord.branch_no;
                //recpayMain.create_time = ord.create_time;
                //recpayMain.cust_no = ord.supcust_no;
                //recpayMain.num1 = ord.num1;
                //recpayMain.num2 = ord.num2;
                //recpayMain.num3 = ord.num3;
                //recpayMain.other1 = ord.other1;
                //recpayMain.other2 = ord.other2;
                //recpayMain.other3 = ord.other3;
                //recpayMain.oper_id = ord.oper_id;
                //recpayMain.oper_date = ord.oper_date;
                //recpayMain.pay_date = ord.pay_date;
                //recpayMain.voucher_no = ord.voucher_no;
                //recpayMain.update_time = ord.update_time;

                //
                d.Insert(ord);
                // d.Insert(recpayMain);

                foreach (ic_t_inout_store_detail line in lines)
                {
                    sql = "select isnull(max(flow_id),0) + 1 as flow_id from ic_t_inout_store_detail ";
                    line.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    line.voucher_no = ord.voucher_no;
                    sql = "select isnull(order_qnty,0.00) as order_qnty from co_t_order_child  where sheet_no='" + line.voucher_no + "' and item_no='" + line.item_no + "'";
                    foreach (DataRow row in d.ExecuteToTable(sql, null).Rows)
                    {
                        line.order_qnty = row["order_qnty"].ToDecimal();
                    }

                    if (string.IsNullOrWhiteSpace(line.voucher_no) && string.IsNullOrWhiteSpace(line.batch_num))
                    {
                        sql = "select batch_num from co_t_order_child  where sheet_no='" + line.voucher_no + "' and item_no='" + line.item_no + "'";
                        line.batch_num = Helper.Conv.ToString(d.ExecuteScalar(sql, null));
                    }

                    line.sheet_no = ord.sheet_no;
                    d.Insert(line);
                    //ic_t_inoutstore_recpay_detail detail = new ic_t_inoutstore_recpay_detail();
                    //sql = "select isnull(max(flow_id),0) + 1 as flow_id from ic_t_inoutstore_recpay_detail ";
                    //detail.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    //detail.sheet_no = line.sheet_no;
                    //detail.batch_num = line.batch_num;
                    //detail.task_flow_id = line.flow_id;

                    //detail.voucher_no = line.voucher_no;
                    //detail.sheet_sort = line.sheet_sort;
                    //detail.real_qnty = line.in_qty;
                    //detail.num1 = line.order_qnty;

                    //sql = "select num3 from sm_t_salesheet_recpay_detail where batch_num='" + detail.batch_num + "'";
                    //detail.num2 = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    //detail.num3 = detail.real_qnty - detail.num2;
                    //detail.num4 = 0;
                    //detail.other1 = "";
                    //detail.other2 = "";
                    //detail.other3 = "";
                    //detail.other4 = "";
                    //detail.item_no = line.item_no;
                    //d.Insert(detail);
                }
                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("InOutBLL.ChangeInOut()", ex.ToString(), ord.sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        void IInOutBLL.DeleteInOut(string sheet_no, DateTime update_time)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();

                db.BeginTran();
                //
                string sql = "select trans_no,approve_flag,update_time from ic_t_inout_store_master where sheet_no='" + sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在[" + sheet_no + "]");
                }
                else
                {
                    var row = tb.Rows[0];
                    if (row["approve_flag"].ToString() == "1")
                    {
                        throw new Exception("单据已审核[" + sheet_no + "]");
                    }
                    if (Helper.Conv.ToDateTime(row["update_time"]) > update_time)
                    {
                        throw new Exception("单据已被他人修改[" + sheet_no + "]");
                    }
                }
                sql = "delete from ic_t_inout_store_detail where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from ic_t_inout_store_master where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("InOutBLL.DeleteInOut()", ex.ToString(), sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        void InSheetRecpay(string sheet_no, string approve_man, DateTime update_time)
        {
            var db = new DB.DBByAutoClose(AppSetting.conn);
            DB.IDB d = db;
            string sql = "select * from ic_t_inoutstore_recpay_main where sheet_no='" + sheet_no + "'";
            if (d.ExecuteToTable(sql, null).Rows.Count > 0)
            {
                sql = " update ic_t_inoutstore_recpay_main set approve_flag='1',approve_man='" + approve_man +
                      "',approve_date='" + DateTime.Now + "',update_time='" + update_time + "' where sheet_no='" +
                      sheet_no + "'";
                d.ExecuteToTable(sql, null);
            }
        }
        // D:客户退货单 A:采购入库 F:采购退货 G:调拨单
        void IInOutBLL.CheckInOut(string sheet_no, string approve_man, DateTime update_time)
        {
            var db = new DB.DBByAutoClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                //
                string sql = "select trans_no,approve_flag,update_time from ic_t_inout_store_master where sheet_no='" + sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在[" + sheet_no + "]");
                }
                Model.ic_t_inout_store_master master =
                    d.ExecuteToModel<Model.ic_t_inout_store_master>("select * from ic_t_inout_store_master where sheet_no='" + sheet_no + "'", null);

                CheckSheet check = new CheckSheet();
                switch (master.trans_no)
                {
                    case "A":
                        check.CheckPISheet(sheet_no, approve_man, update_time);
                        //InSheetRecpay(sheet_no, approve_man, update_time);

                        break;
                    case "D":
                        check.CheckRISheet(sheet_no, approve_man, update_time);
                        break;
                    case "F":
                        check.CheckROSheet(sheet_no, approve_man, update_time);
                        break;
                    case "G":
                        check.CheckIOSheet(sheet_no, approve_man, update_time);
                        break;
                    case "I":
                        check.CheckSOSheet(sheet_no, approve_man, update_time);
                        break;
                    case "OM":
                    case "RM":
                    case "PE":
                        check.CheckProcessSheet(sheet_no, approve_man, update_time);
                        break;
                    default:
                        check.CheckOOSheet(sheet_no, approve_man, update_time);
                        break;
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("InOutBLL.CheckInOut()", ex.ToString(), sheet_no, approve_man);
                throw new Exception(ex.ToString());
            }

        }

        void IInOutBLL.AssAddCG(Model.ic_t_inout_store_master ord, List<ic_t_inout_store_detail> lines, out string sheet_no)
        {
            sheet_no = "";
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                sheet_no = MaxCode(d, "PI");
                ord.db_no = "+";
                ord.sheet_no = sheet_no;
                ord.create_time = DateTime.Now;
                ord.update_time = ord.create_time;
                ord.oper_date = DateTime.Now;
                //
                d.Insert(ord);

                string flow_ids = "''";
                string sql = "select isnull(max(flow_id),0) + 1 as flow_id from ic_t_inout_store_detail ";
                foreach (ic_t_inout_store_detail line in lines)
                {
                    line.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    line.sheet_no = sheet_no;
                    flow_ids += ",'" + line.other2 + "'";
                    d.Insert(line);
                }
                sql = @"DELETE FROM dbo.co_t_cg_order_app_detail WHERE flow_id IN (" + flow_ids + ");";
                d.ExecuteScalar(sql, null);

                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("InOutBLL.AddInOut()", ex.ToString(), ord.sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        void IInOutBLL.AssGenCG(string flow_id, string oper_id)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                string sql = @"SELECT d.flow_id,s.supcust_no,s.sup_name,d.item_no,d.item_name,d.barcode,d.unit_no,d.price,cg_qty,d.create_time
FROM dbo.co_t_cg_order_app_detail d
LEFT JOIN dbo.bi_t_supcust_info s ON d.sup_no = s.supcust_no AND s.supcust_flag = 'S'
WHERE(d.ph_sheet_no = '' OR d.ph_sheet_no IS NULL) AND d.flow_id IN(" + flow_id + ") " +
        "ORDER BY s.supcust_no,d.item_no";

                DataTable tb = d.ExecuteToTable(sql, null);

                List<Model.ic_t_inout_store_master> mains = new List<Model.ic_t_inout_store_master>();
                List<ic_t_inout_store_detail> childs = new List<ic_t_inout_store_detail>();

                Model.ic_t_inout_store_master t_master = new Model.ic_t_inout_store_master();
                int index = 0;
                foreach (DataRow dr in tb.Rows)
                {
                    string sup_no = dr["supcust_no"].ToString();
                    if (!sup_no.Equals(t_master.supcust_no))
                    {
                        t_master = new Model.ic_t_inout_store_master
                        {
                            sheet_no = MaxCode(d, "PI"),
                            trans_no = "A",
                            branch_no = "0001",
                            voucher_no = "",
                            supcust_no = sup_no,
                            pay_way = "",
                            discount = 1,
                            coin_no = "RMB",
                            tax_amount = 0,
                            db_no = "+",
                            create_time = DateTime.Now,
                            oper_date = DateTime.Now,
                            oper_id = oper_id,
                            deal_man = "",
                            cm_branch = "00",
                            other1 = "采购助手生成",
                            other2 = "",
                            other3 = "",
                            old_no = "",
                            num1 = 0,
                            num2 = 0,
                            num3 = 0,
                            sale_no = "",
                            display_flag = "1",
                            approve_flag = "0",
                            approve_man = "",
                            approve_date = System.DateTime.MinValue,
                            pay_date = DateTime.Now,
                            update_time = DateTime.Now
                        };
                        mains.Add(t_master);
                        index = 0;
                    }
                    ++index;

                    ic_t_inout_store_detail line = new ic_t_inout_store_detail();
                    line.sheet_no = t_master.sheet_no;
                    line.item_no = dr["item_no"].ToString();
                    line.item_name = dr["item_name"].ToString();
                    line.unit_no = dr["unit_no"].ToString();
                    line.barcode = dr["barcode"].ToString();
                    line.unit_factor = 1;
                    line.in_qty = Helper.Conv.ToDecimal(dr["cg_qty"].ToString());
                    line.orgi_price = 0;
                    line.valid_price = Helper.Conv.ToDecimal(dr["price"].ToString());
                    line.cost_price = 0;
                    line.sub_amount = line.in_qty * line.valid_price;
                    line.discount = 1;
                    line.tax = 0;
                    line.is_tax = "0";
                    line.valid_date = DateTime.Now.AddDays(1);
                    line.other1 = "";
                    line.other2 = "AssPre-" + dr["flow_id"].ToString();
                    line.other3 = "";
                    line.voucher_no = "";
                    line.sheet_sort = index;
                    line.ret_qnty = 0;
                    line.num1 = 0;
                    line.num2 = 0;
                    line.num3 = 0;
                    line.num4 = 0;
                    line.num5 = 0;
                    line.num6 = 0;
                    line.cost_notax = 0;
                    line.packqty = 0;
                    line.sgqty = 0;
                    childs.Add(line);

                    t_master.inout_amount += line.in_qty * line.valid_price;
                    t_master.total_amount += line.in_qty * line.valid_price;
                }


                mains.ForEach((m) =>
                {
                    d.Insert(m);
                });

                sql = "select isnull(max(flow_id),0) + 1 as flow_id from ic_t_inout_store_detail ";
                childs.ForEach((c) =>
                {
                    c.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    d.Insert(c);
                });


                // sql = @"DELETE FROM dbo.co_t_cg_order_app_detail WHERE flow_id IN (" + flow_id + ");";
                // d.ExecuteScalar(sql, null);

                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("InOutBLL.AddInOut()", ex.ToString(), flow_id);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
        void IInOutBLL.AssGenPlanCG(string flow_id, string oper_id)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                string sql = @"SELECT d.flow_id,s.supcust_no,s.sup_name,d.item_no,d.item_name,d.barcode,d.unit_no,d.price,cg_qty,d.create_time
FROM dbo.co_t_cg_order_app_detail d
LEFT JOIN dbo.bi_t_supcust_info s ON d.sup_no = s.supcust_no AND s.supcust_flag = 'S'
WHERE(d.ph_sheet_no != '' OR d.ph_sheet_no IS NOT NULL) AND d.flow_id IN(" + flow_id + ") " +
        "ORDER BY s.supcust_no,d.item_no";

                DataTable tb = d.ExecuteToTable(sql, null);

                List<Model.ic_t_inout_store_master> mains = new List<Model.ic_t_inout_store_master>();
                List<ic_t_inout_store_detail> childs = new List<ic_t_inout_store_detail>();

                Model.ic_t_inout_store_master t_master = new Model.ic_t_inout_store_master();
                int index = 0;
                foreach (DataRow dr in tb.Rows)
                {
                    string sup_no = dr["supcust_no"].ToString();
                    if (!sup_no.Equals(t_master.supcust_no))
                    {
                        t_master = new Model.ic_t_inout_store_master
                        {
                            sheet_no = MaxCode(d, "PI"),
                            trans_no = "A",
                            branch_no = "0001",
                            voucher_no = "",
                            supcust_no = sup_no,
                            pay_way = "",
                            discount = 1,
                            coin_no = "RMB",
                            tax_amount = 0,
                            db_no = "+",
                            create_time = DateTime.Now,
                            oper_date = DateTime.Now,
                            oper_id = oper_id,
                            deal_man = "",
                            cm_branch = "00",
                            other1 = "采购助手生成",
                            other2 = "",
                            other3 = "",
                            old_no = "",
                            num1 = 0,
                            num2 = 0,
                            num3 = 0,
                            sale_no = "",
                            display_flag = "1",
                            approve_flag = "0",
                            approve_man = "",
                            approve_date = System.DateTime.MinValue,
                            pay_date = DateTime.Now,
                            update_time = DateTime.Now
                        };
                        mains.Add(t_master);
                        index = 0;
                    }
                    ++index;

                    ic_t_inout_store_detail line = new ic_t_inout_store_detail();
                    line.sheet_no = t_master.sheet_no;
                    line.item_no = dr["item_no"].ToString();
                    line.item_name = dr["item_name"].ToString();
                    line.unit_no = dr["unit_no"].ToString();
                    line.barcode = dr["barcode"].ToString();
                    line.unit_factor = 1;

                    line.in_qty = Helper.Conv.ToDecimal(dr["cg_qty"].ToString());
                    line.orgi_price = 0;
                    line.valid_price = Helper.Conv.ToDecimal(dr["price"].ToString());
                    line.cost_price = 0;
                    line.sub_amount = line.in_qty * line.valid_price;
                    line.discount = 1;
                    line.tax = 0;
                    line.is_tax = "0";
                    line.valid_date = DateTime.Now.AddDays(1);
                    line.other1 = "";
                    line.other2 = "Ass-" + dr["flow_id"].ToString();
                    line.other3 = "";
                    line.voucher_no = "";
                    line.sheet_sort = index;
                    line.ret_qnty = 0;
                    line.num1 = 0;
                    line.num2 = 0;
                    line.num3 = 0;
                    line.num4 = 0;
                    line.num5 = 0;
                    line.num6 = 0;
                    line.cost_notax = 0;
                    line.packqty = 0;
                    line.sgqty = 0;
                    childs.Add(line);

                    t_master.inout_amount += line.in_qty * line.valid_price;
                    t_master.total_amount += line.in_qty * line.valid_price;
                }


                mains.ForEach((m) =>
                {
                    d.Insert(m);
                });

                sql = "select isnull(max(flow_id),0) + 1 as flow_id from ic_t_inout_store_detail ";
                childs.ForEach((c) =>
                {
                    c.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    d.Insert(c);
                });


                // sql = @"DELETE FROM dbo.co_t_cg_order_app_detail WHERE flow_id IN (" + flow_id + ");";
                // d.ExecuteScalar(sql, null);

                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("InOutBLL.AddInOut()", ex.ToString(), flow_id);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        void IInOutBLL.ReceiveGenCG(string flow_id, string oper_id)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                string sql = $@"SELECT d.po_sheet_no, d.sup_no, d.item_no, d.item_name, d.barcode, d.unit_no, d.unit_factor,
       i.branch_no, d.is_build, d.order_qnty, d.batch_num, d.cust_no,
       SUM ( CASE
               WHEN d.trans_no <> '4'
                    OR d.trans_no IS NULL THEN d.receive_qty
               ELSE 0
             END
           ) receive_qty, SUM ( CASE
                                  WHEN d.trans_no = '4' THEN d.receive_qty
                                  ELSE 0
                                END
                              ) loss_qty
FROM dbo.co_t_receive_order_detail d
LEFT JOIN dbo.bi_t_item_info i
  ON i.item_no = d.item_no
WHERE d.flow_id in ({flow_id}) AND d.receive_qty > 0
GROUP BY d.po_sheet_no, d.sup_no, d.item_no, d.item_name, d.barcode, d.unit_no, d.unit_factor,
         i.branch_no, d.is_build, d.order_qnty, d.batch_num, d.cust_no
ORDER BY d.po_sheet_no, d.sup_no, d.item_no;";

                DataTable tb = d.ExecuteToTable(sql, null);

                List<Model.ic_t_inout_store_master> mains = new List<Model.ic_t_inout_store_master>();
                Dictionary<string, List<body.ic_t_inout_store_detail>> childs = new Dictionary<string, List<body. ic_t_inout_store_detail>>();

                IPriceBLL priceBLL = new PriceBLL();
                Model.ic_t_inout_store_master t_master = new Model.ic_t_inout_store_master();
                int index = 0;
                foreach (DataRow dr in tb.Rows)
                {
                    string sup_no = dr["sup_no"].ToString();
                    string po_sheet_no = dr["po_sheet_no"].ToString();
                    if (!po_sheet_no.Equals(t_master.voucher_no) ||
                        !sup_no.Equals(t_master.supcust_no))
                    {
                        t_master = new Model.ic_t_inout_store_master
                        {
                            sheet_no = MaxCode(d, "PI"),
                            trans_no = "A",
                            branch_no = "0001",
                            voucher_no = po_sheet_no,
                            supcust_no = sup_no,
                            pay_way = "",
                            discount = 1,
                            coin_no = "RMB",
                            tax_amount = 0,
                            db_no = "+",
                            create_time = DateTime.Now,
                            oper_date = DateTime.Now,
                            oper_id = oper_id,
                            deal_man = "",
                            cm_branch = "00",
                            other1 = "收货",
                            other2 = "",
                            other3 = "",
                            old_no = "",
                            num1 = 0,
                            num2 = 0,
                            num3 = 0,
                            sale_no = "",
                            display_flag = "1",
                            approve_flag = "0",
                            approve_man = "",
                            approve_date = System.DateTime.MinValue,
                            pay_date = DateTime.Now,
                            update_time = DateTime.Now
                        };
                        mains.Add(t_master);
                        childs.Add(t_master.sheet_no, new List<body.ic_t_inout_store_detail>());
                        index = 0;
                    }
                    ++index;

                    var lis = childs[t_master.sheet_no];

                    body.ic_t_inout_store_detail line = new body.ic_t_inout_store_detail();
                    line.sheet_no = t_master.sheet_no;
                    line.branch_no_d = dr["branch_no"].ToString();
                    line.item_no = dr["item_no"].ToString();
                    line.item_name = dr["item_name"].ToString();
                    line.unit_no = dr["unit_no"].ToString();
                    line.barcode = dr["barcode"].ToString();
                    line.order_qnty = dr["order_qnty"].ToDecimal();
                    //line.batch_num=CreateBatchNum(sup_no,DateTime.Now.ToString("yyMMdd"),)
                    line.batch_num = dr["batch_num"].ToString();
                    line.unit_factor = 1;
                    line.in_qty = Helper.Conv.ToDecimal(dr["receive_qty"].ToString());
                    line.orgi_price = 0;
                    line.valid_price = priceBLL.GetSupItemPrice(sup_no, dr["item_no"].ToString(), "0");
                    line.cost_price = 0;
                    line.sub_amount = line.in_qty * line.valid_price;
                    line.discount = 1;
                    line.tax = 0;
                    line.is_tax = "0";
                    line.valid_date = DateTime.Now.AddDays(1);
                    line.cust_no = dr["cust_no"].ToString();
                    line.other1 = "";
                    line.other2 = "";
                    line.other3 = "";
                    line.voucher_no = "";
                    line.sheet_sort = index;
                    line.ret_qnty = 0;
                    line.num1 = 0;
                    line.num2 = 0;
                    line.num3 = 0;
                    line.num4 = 0;
                    line.num5 = dr["loss_qty"].ToDecimal();
                    line.num6 = 0;
                    line.cost_notax = 0;
                    line.packqty = 0;
                    line.sgqty = 0;

                    if (!string.IsNullOrEmpty(line.batch_num))
                    {
                        rp_t_batch_flow flow = d.ExecuteToModel<rp_t_batch_flow>($@"SELECT * FROM dbo.rp_t_batch_flow WHERE batch_no='{line.batch_num}'", null);
                        if (flow == null || string.IsNullOrEmpty(flow.batch_no))
                        {
                            flow = new rp_t_batch_flow()
                            {
                                branch_no = t_master.branch_no,
                                batch_no = line.batch_num,
                                item_no = line.item_no,
                                voucher_no = t_master.sheet_no,
                                batch_type = t_master.trans_no,
                                batch_property = "良品",
                                area_no = "",
                                item_size = "",
                                unit_no = line.unit_no,
                                unit_factor = line.unit_factor,
                                in_price = line.valid_price,
                                cost_price = line.cost_price,
                                total_qnty = line.in_qty,
                                settle_qnty = line.in_qty,
                                sub_amount = line.sub_amount,
                                oper_date = DateTime.Now,
                                oper_id = t_master.oper_id,
                                update_time = DateTime.Now,
                            };
                            d.Insert(flow);
                        }
                        else
                        {
                            if (dr["is_build"].ToInt32() != 1)
                            {
                                //第一次生成并且有多条明细同个批次
                                flow.total_qnty += line.in_qty;
                                flow.settle_qnty += line.in_qty;
                                flow.sub_amount += line.sub_amount;

                                d.Update(flow, "batch_no", "total_qty,settle_qnty,sub_amount");
                            }
                        }
                    }

                    lis.Add(line);
                }

                mains.ForEach((m) =>
                {
                    var child = childs[m.sheet_no];
                    BatchProcessing.SaveInSheetBatch(d, m, child);

                    d.Insert(m);

                    child.ForEach((c) =>
                    {
                        c.flow_id = d.ExecuteScalar($@"select isnull(max(flow_id),0) + 1 as flow_id from ic_t_inout_store_detail", null).ToInt64();
                        d.Insert(c);
                    });
                });


                d.ExecuteScalar($@"update dbo.co_t_receive_order_detail set is_build='1' where flow_id in ({flow_id})", null);

                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("InOutBLL.ReceiveGenCG()", ex.ToString(), flow_id);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        public void PickingGenSaleSheet(string flow_id, string oper_id)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;

            try
            {
                db.Open();

                IPriceBLL priceBll = new PriceBLL();
                string sql = $@"SELECT w.rec_code,w.item_no,i.barcode,i.item_name,w.unit_no,SUM(w.qty) qty
                                        FROM dbo.ot_weighing w
                                        LEFT JOIN dbo.bi_t_item_info i ON i.item_no = w.item_no
                                        WHERE w.flow_id IN ({flow_id}) and (w.is_approve IS NULL OR   w.is_approve != '1')
                                        GROUP BY w.rec_code,w.item_no,i.barcode,i.item_name,w.unit_no";
                DataTable tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count < 1) throw new Exception("没有要审核的数据");

                Dictionary<sm_t_salesheet, List<sm_t_salesheet_detail>> add_dic = new Dictionary<sm_t_salesheet, List<sm_t_salesheet_detail>>();

                var group = tb.AsEnumerable().GroupBy(r => r["rec_code"]);
                foreach (var code in group)
                {
                    string sup = code.Key.ToString();

                    sm_t_salesheet ord = new sm_t_salesheet();
                    List<sm_t_salesheet_detail> lines = new List<sm_t_salesheet_detail>();

                    {
                        ord.branch_no = "0001";
                        ord.voucher_no = "";
                        ord.cust_no = sup;
                        ord.pay_way = "";
                        ord.discount = 1;
                        ord.coin_no = "RMB";
                        ord.paid_amount = 0;
                        ord.oper_date = DateTime.Now;
                        ord.oper_id = oper_id;
                        ord.sale_man = "00";
                        ord.approve_flag = "0";
                        ord.other1 = "";
                        ord.other2 = "";
                        ord.other3 = "";
                        ord.other4 = "";
                        ord.old_no = "00";
                        ord.cm_branch = "00";
                        ord.psheet_no = "";
                        ord.pay_nowmark = "";
                        ord.payfee_memo = "";
                        ord.if_back = "0";
                        ord.approve_man = "00";
                        ord.approve_date = System.DateTime.MinValue;
                        ord.num1 = 0;
                        ord.num2 = 0;
                        ord.num3 = 0;
                        ord.pay_date = DateTime.Now;
                        ord.create_time = DateTime.Now;
                        ord.update_time = DateTime.Now;
                    }

                    int index = 1;
                    foreach (DataRow row in code)
                    {
                        sm_t_salesheet_detail line = new sm_t_salesheet_detail();
                        lines.Add(line);
                        line.item_no = row["item_no"].ToString();
                        line.item_name = row["item_name"].ToString();
                        line.unit_no = row["unit_no"].ToString();
                        line.barcode = row["barcode"].ToString();
                        line.unit_factor = 1;
                        line.sale_qnty = Helper.Conv.ToDecimal(row["qty"].ToString());
                        line.sale_price = 0;
                        line.real_price = priceBll.GetSupItemPrice(sup, line.item_no, "0");
                        line.cost_price = priceBll.GetLastInPrice(line.item_no);
                        line.sale_money = line.sale_qnty * line.real_price;
                        line.discount = 1;
                        line.sale_tax = 0;
                        line.is_tax = "0";
                        line.other1 = "";
                        line.other2 = "";
                        line.other3 = "";
                        line.other4 = "";
                        line.other5 = "";
                        line.voucher_no = "";
                        line.sheet_sort = index;
                        line.ret_qnty = 0;
                        line.num1 = 0;
                        line.num2 = 0;
                        line.num3 = 0;
                        line.num4 = 0;
                        line.num5 = 0;
                        line.num6 = 0;
                        line.num7 = 0;
                        line.num8 = 0;
                        line.cost_notax = 0;
                        line.packqty = 0;
                        line.sgqty = 0;
                        index++;

                        ord.total_amount += line.sale_qnty * line.real_price;
                    }

                    add_dic.Add(ord, lines);
                }

                db.BeginTran();

                sql = "select isnull(max(flow_id),0) + 1 as flow_id from sm_t_salesheet_detail ";
                foreach (sm_t_salesheet master in add_dic.Keys)
                {
                    master.sheet_no = MaxCode(d, "SO");
                    //
                    d.Insert(master);

                    foreach (Model.sm_t_salesheet_detail line in add_dic[master])
                    {
                        line.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                        line.sheet_no = master.sheet_no;
                        d.Insert(line);
                    }
                }

                sql = @"UPDATE dbo.ot_weighing SET is_approve='1' WHERE flow_id IN (" + flow_id + ")";
                d.ExecuteScalar(sql, null);

                db.CommitTran();
            }
            catch (Exception)
            {
                db.RollBackTran();
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        public void InventoryAdjustment(string flow_id, string oper_id)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            string sql = "";
            try
            {
                db.Open();
                db.BeginTran();

                sql = $@"SELECT f.*,i.price
FROM dbo.ot_check_flow f
LEFT JOIN dbo.bi_t_item_info i ON i.item_no = f.item_no
WHERE f.flow_id IN ({flow_id})";

                DataTable tb = d.ExecuteToTable(sql, null);

                ic_t_inout_store_master ord = new ic_t_inout_store_master();
                List<ic_t_inout_store_detail> lines = new List<ic_t_inout_store_detail>();
                ord.sheet_no = "";
                ord.trans_no = "09";
                ord.branch_no = "0001";
                ord.voucher_no = "";
                ord.supcust_no = "";
                ord.pay_way = "";
                ord.discount = 1;
                ord.coin_no = "RMB";
                ord.tax_amount = 0;
                ord.oper_date = DateTime.Now;
                ord.oper_id = oper_id;
                ord.deal_man = "00";
                ord.cm_branch = "00";
                ord.num1 = 0;
                ord.num2 = 0;
                ord.num3 = 0;
                ord.sale_no = "";
                ord.approve_flag = "0";
                ord.update_time = DateTime.Now;
                ord.display_flag = "1";

                int index = 0;
                decimal total_amt = 0;
                foreach (DataRow row in tb.Rows)
                {
                    ic_t_inout_store_detail line = new ic_t_inout_store_detail();
                    lines.Add(line);
                    line.sheet_no = ord.sheet_no;
                    line.item_no = row["item_no"].ToString();
                    line.item_name = row["item_name"].ToString();
                    line.unit_no = row["unit_no"].ToString();
                    line.barcode = "";
                    line.unit_factor = 1;
                    line.in_qty = Helper.Conv.ToDecimal(row["qty"].ToString());
                    line.orgi_price = 0;
                    line.valid_price = Helper.Conv.ToDecimal(row["price"].ToString());
                    line.cost_price = 0;
                    line.sub_amount = line.in_qty * line.valid_price;
                    line.discount = 1;
                    line.tax = 0;
                    line.is_tax = "0";
                    line.valid_date = DateTime.Now.AddDays(7);
                    line.other1 = "";
                    line.other2 = "";
                    line.other3 = "";
                    line.voucher_no = "";
                    line.sheet_sort = index;
                    line.ret_qnty = 0;
                    line.num1 = 0;
                    line.num2 = 0;
                    line.num3 = 0;
                    line.num4 = 0;
                    line.num5 = 0;
                    line.num6 = 0;
                    line.cost_notax = 0;
                    line.packqty = 0;
                    line.sgqty = 0;

                    total_amt += line.in_qty * line.valid_price;
                }

                ord.inout_amount = total_amt;
                ord.total_amount = total_amt;

                ord.db_no = "";
                ord.sheet_no = MaxCode(d, "OO");
                ord.create_time = DateTime.Now;
                ord.update_time = ord.create_time;
                //
                d.Insert(ord);

                foreach (var line in lines)
                {
                    sql = "select isnull(max(flow_id),0) + 1 as flow_id from ic_t_inout_store_detail ";
                    line.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    line.sheet_no = ord.sheet_no;
                    d.Insert(line);
                }

                sql = @" UPDATE dbo.ot_check_flow SET is_append='1' WHERE flow_id IN (" + flow_id + ") ";
                d.ExecuteScalar(sql, null);

                db.CommitTran();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeLog("InOutBLL.InventoryAdjustment()", ex.ToString(), flow_id);
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        public void GenProcessDetail(string flowIds, string oper_id, string pro_dept_no, string fee_dept_no)
        {
            string sql = "";
            var db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                sql = $@"SELECT p.ph_sheet_no,
       p.process_type,
       p.bis_type,
       p.item_no,
       i.item_subno,
       i.item_name,
       i.branch_no,
       i.barcode,
       p.unit_no,
       SUM(p.need_qty) need_qty,
       SUM(p.qty) qty,
       SUM(p.weight) weight
FROM dbo.ot_processing p
    LEFT JOIN dbo.bi_t_item_info i
        ON i.item_no = p.item_no
WHERE p.is_approve <> '1'
      AND p.bis_type <> ''
      AND p.bis_type IS NOT NULL
      AND p.flow_id IN ( {flowIds} )
GROUP BY p.ph_sheet_no,
         p.process_type,
         p.bis_type,
         p.item_no,
         i.item_subno,
         i.item_name,
         i.branch_no,
         i.barcode,
         p.unit_no;";

                DataTable tb = d.ExecuteToTable(sql, null);

                IPriceBLL priceBll = new PriceBLL();

                var groupBy = tb.AsEnumerable().GroupBy(t => t["bis_type"]);
                foreach (var g in groupBy)
                {
                    string sheet_type = g.First()["bis_type"].ToString();

                    ic_t_inout_store_master ord = new ic_t_inout_store_master();
                    List<body.ic_t_inout_store_detail> lines = new List<body.ic_t_inout_store_detail>();
                    ord.sheet_no = MaxCode(d, sheet_type);
                    ord.trans_no = sheet_type;
                    ord.branch_no = "0001";
                    ord.voucher_no = "";
                    ord.supcust_no = "";
                    ord.pro_dept_no = pro_dept_no;
                    ord.fee_dept_no = fee_dept_no;
                    ord.pay_way = "";
                    ord.discount = 1;
                    ord.coin_no = "RMB";
                    ord.tax_amount = 0;
                    ord.oper_date = DateTime.Now;
                    ord.oper_id = oper_id;
                    ord.deal_man = "00";
                    ord.cm_branch = "00";
                    ord.other1 = "";
                    ord.other2 = "";
                    ord.other3 = "";
                    ord.old_no = "";
                    ord.num1 = 0;
                    ord.num2 = 0;
                    ord.num3 = 0;
                    ord.sale_no = "";
                    ord.approve_flag = "0";
                    ord.approve_man = "00";
                    ord.approve_date = System.DateTime.MinValue;
                    ord.pay_date = System.DateTime.MinValue;
                    ord.update_time = DateTime.Now;
                    ord.display_flag = "1";

                    switch (sheet_type)
                    {
                        case "OM":
                            ord.db_no = "-";
                            ord.trans_no = "OM";
                            break;
                        case "RM":
                            ord.db_no = "+";
                            ord.trans_no = "RM";
                            break;
                        case "PE":
                            ord.db_no = "+";
                            ord.trans_no = "PE";
                            break;
                        default:
                            throw new Exception("数据类型无效(-1)");
                    }

                    int index = 0;
                    foreach (DataRow row in g)
                    {
                        ++index;
                        body.ic_t_inout_store_detail line = new body.ic_t_inout_store_detail();
                        lines.Add(line);
                        line.sheet_no = ord.sheet_no;
                        line.branch_no_d = row["branch_no"].ToString();
                        line.item_no = row["item_no"].ToString();
                        line.item_name = row["item_name"].ToString();
                        line.unit_no = row["unit_no"].ToString();
                        line.barcode = row["barcode"].ToString();
                        line.unit_factor = 1;
                        line.in_qty = Helper.Conv.ToDecimal(row["qty"].ToString());
                        line.orgi_price = 0;
                        line.valid_price = priceBll.GetLastInPrice(row["item_no"].ToString());
                        line.cost_price = 0;
                        line.sub_amount = line.in_qty * line.valid_price;
                        line.discount = 1;
                        line.tax = 0;
                        line.is_tax = "0";
                        line.valid_date = DateTime.Now.AddYears(1);
                        line.other1 = row["ph_sheet_no"].ToString();
                        line.other2 = "";
                        line.other3 = "";
                        line.voucher_no = "";
                        line.sheet_sort = index;
                        line.ret_qnty = 0;
                        line.num1 = 0;
                        line.num2 = 0;
                        line.num3 = 0;
                        line.num4 = 0;
                        line.num5 = 0;
                        line.num6 = 0;
                        line.cost_notax = 0;
                        line.packqty = 0;
                        line.sgqty = 0;

                        ord.total_amount += line.sub_amount;
                    }

                    //
                    d.Insert(ord);

                    BatchProcessing.SaveInOutSheetBatch(d, ord, lines, out lines);

                    foreach (ic_t_inout_store_detail line in lines)
                    {
                        sql = "select isnull(max(flow_id),0) + 1 as flow_id from ic_t_inout_store_detail ";
                        line.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                        line.sheet_no = ord.sheet_no;
                        d.Insert(line);
                    }

                }

                d.ExecuteScalar($@"UPDATE dbo.ot_processing SET is_approve='1' WHERE flow_id IN ({flowIds})", null);

                db.CommitTran();
            }
            catch (Exception)
            {
                db.RollBackTran();
                throw;
            }
            finally
            {
                db.Close();
            }
        }
        #region 销售计划

        public void AddSaleSPSheet(IvyTran.body.co_t_order_main ord, List<IvyTran.body.co_t_order_child> lines, out string sheet_no)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();

                db.BeginTran();
                string sql = $"select sheet_no from co_t_order_main where sup_no = '{ord.sup_no}' and supply_date = '{ord.supply_date.Toyyyy_MM_dd()}'";
                DataTable tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count > 0)
                {
                    throw new Exception("当前日期已存在单据");
                }
                sheet_no = MaxCode(d, "SP");
                ord.sheet_no = sheet_no;
                ord.oper_date = DateTime.Now;
                ord.update_time = DateTime.Now;
                //
                d.Insert(ord);

                foreach (co_t_order_child line in lines)
                {
                    line.sheet_no = sheet_no;
                    d.Insert(line, "flow_id");
                }

                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("InOutBLL.AddSaleSPSheet()", ex.ToString(), ord.sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        public void ChangeSaleSPSheet(IvyTran.body.co_t_order_main ord, List<IvyTran.body.co_t_order_child> lines)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                //
                string sql = "select approve_flag,update_time from co_t_order_main where sheet_no='" + ord.sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在[" + ord.sheet_no + "]");
                }
                else
                {
                    var row = tb.Rows[0];
                    if (row["approve_flag"].ToString() == "1")
                    {
                        throw new Exception("单据已审核[" + ord.sheet_no + "]");
                    }
                    if (Helper.Conv.ToDateTime(row["update_time"]) > ord.update_time)
                    {
                        throw new Exception("单据已被他人修改[" + ord.sheet_no + "]");
                    }
                }
                sql = "delete from co_t_order_child where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from co_t_order_main where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                ord.update_time = DateTime.Now;
                d.Insert(ord);
                foreach (co_t_order_child line in lines)
                {
                    line.flow_id = 0;
                    d.Insert(line, "flow_id");
                }
                //
                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("InOutBLL.ChangeSaleSPSheet()", ex.ToString(), ord.sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        public void DeleteSaleSPSheet(string sheet_no, DateTime update_time)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                //
                string sql = "select approve_flag,update_time from co_t_order_main where sheet_no='" + sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在[" + sheet_no + "]");
                }
                else
                {
                    var row = tb.Rows[0];
                    if (row["approve_flag"].ToString() == "1")
                    {
                        throw new Exception("单据已审核[" + sheet_no + "]");
                    }
                    if (Helper.Conv.ToDateTime(row["update_time"]) > update_time)
                    {
                        throw new Exception("单据已被他人修改[" + sheet_no + "]");
                    }
                }
                sql = "delete from co_t_order_child where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from co_t_order_main where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("InOutBLL.DeleteSaleSPSheet()", ex.ToString(), sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        public void CheckSaleSPSheet(string sheet_no, string approve_man, DateTime update_time)
        {
            CheckSheet check = new CheckSheet();
            check.CheckSPSheet(sheet_no, approve_man, update_time);
        }

        public void CreateInStorage(string sheet_no)
        {
            try
            {

                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                IvyTran.body.co_t_order_main c_ord;
                List<IvyTran.body.co_t_order_child> c_child;
                get_c_data(sheet_no, db, out c_ord, out c_child);

                ic_t_inout_store_master ord = new ic_t_inout_store_master();
                List<IvyTran.body.ic_t_inout_store_detail> lines = new List<IvyTran.body.ic_t_inout_store_detail>();
                ord.sheet_no = "";
                ord.trans_no = "A";
                ord.branch_no = c_ord.branch_no;
                ord.voucher_no = c_ord.sheet_no;
                ord.supcust_no = c_ord.sup_no;
                ord.pay_way = "";
                ord.discount = 1;
                ord.display_flag = "1";
                ord.coin_no = "RMB";
                ord.tax_amount = 0;
                ord.db_no = "";
                ord.oper_date = c_ord.oper_date;
                ord.oper_id = c_ord.oper_id;
                ord.cm_branch = "00";
                ord.other2 = "";
                ord.num1 = 0;
                ord.num2 = 0;
                ord.num3 = 0;
                ord.approve_flag = "0";
                ord.approve_man = c_ord.approve_man;
                ord.approve_date = System.DateTime.MinValue;
                ord.pay_date = DateTime.MinValue;
                ord.update_time = c_ord.update_time;
                ord.other1 = "以销定采自动生成";

                int index = 0;
                decimal total_amt = 0;
                foreach (IvyTran.body.co_t_order_child row in c_child)
                {
                    ++index;
                    if (row.item_no != "")
                    {
                        IvyTran.body.ic_t_inout_store_detail line = new IvyTran.body.ic_t_inout_store_detail();
                        lines.Add(line);
                        line.sheet_no = ord.sheet_no;
                        line.item_no = row.item_no;
                        line.item_name = db.ExecuteScalar($"select item_name from bi_t_item_info where item_no = '{row.item_no}'", null).ToString();
                        line.unit_no = row.unit_no;
                        line.unit_factor = 1;
                        line.in_qty = row.real_qty;
                        line.discount = 1;
                        line.batch_num = row.batch_num;
                        line.valid_price = row.in_price;
                        line.sheet_sort = index;

                        total_amt += line.in_qty * line.valid_price;
                    }
                }
                ord.inout_amount = total_amt;
                ord.total_amount = total_amt;

                var sheet_no1 = "";
                IInOutBLL bll = new InOutBLL();
                bll.AddInOut(ord, lines, out sheet_no1);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("InOutBll>CreateInStorage", ex.ToString(), sheet_no);
                throw ex;
            }
        }

        private static void get_c_data(string sheet_no, IDB db, out IvyTran.body.co_t_order_main c_ord, out List<IvyTran.body.co_t_order_child> c_lines)
        {
            string sql = $"select * from co_t_order_main where sheet_no = '{sheet_no}'";
            c_ord = db.ExecuteToModel<IvyTran.body.co_t_order_main>(sql, null);
            sql = $"select * from co_t_order_child where sheet_no = '{sheet_no}'";
            c_lines = db.ExecuteToList<IvyTran.body.co_t_order_child>(sql, null);
        }

        public void CreateSO(string sheet_no)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                IvyTran.body.co_t_order_main c_ord;
                List<IvyTran.body.co_t_order_child> c_child;
                get_c_data(sheet_no, db, out c_ord, out c_child);

                IvyTran.body.sm_t_salesheet ord = new IvyTran.body.sm_t_salesheet();
                List<IvyTran.body.sm_t_salesheet_detail> lines = new List<IvyTran.body.sm_t_salesheet_detail>();
                ord.sheet_no = "";
                ord.branch_no = c_ord.branch_no;
                ord.voucher_no = c_ord.sheet_no;
                ord.cust_no = c_ord.sup_no;
                ord.pay_way = "";
                ord.paid_amount = c_ord.paid_amount;
                ord.oper_id = c_ord.oper_id;
                ord.sale_man = c_ord.oper_id;
                ord.approve_flag = "0";
                ord.other1 = "";
                ord.other3 = "";
                ord.other4 = "";
                ord.cm_branch = "00";
                ord.psheet_no = "";
                ord.pay_nowmark = "";
                ord.payfee_memo = "";
                ord.if_back = "0";
                ord.approve_man = c_ord.approve_man;
                ord.approve_date = System.DateTime.MinValue;
                ord.num1 = 0;
                ord.num2 = 0;
                ord.num3 = 0;
                ord.pay_date = DateTime.MinValue;


                int flag = 0;
                int index = 0;
                decimal total_amt = 0;
                foreach (var row in c_child)
                {
                    ++index;
                    if (row.item_no != "")
                    {
                        IvyTran.body.sm_t_salesheet_detail line = new IvyTran.body.sm_t_salesheet_detail();
                        lines.Add(line);
                        line.sheet_no = ord.sheet_no;
                        line.item_no = row.item_no;
                        line.item_name = db.ExecuteScalar($"select item_name from bi_t_item_info where item_no = '{row.item_no}'", null).ToString();
                        line.unit_no = row.unit_no;
                        line.sale_price = row.sale_price;
                        line.real_price = row.sale_price;
                        line.cost_price = row.in_price;
                        line.sale_qnty = row.real_qty;
                        line.sale_money = row.real_transaction_sum;
                        line.batch_num = row.batch_num;
                        line.unit_factor = 1;
                        line.sheet_sort = index;
                        line.is_tax = "0";

                        flag = 1;
                        total_amt += line.sale_qnty * line.real_price;
                    }
                }
                ord.real_amount = total_amt;
                ord.total_amount = total_amt;
                var sheet_no1 = "";
                IInOutBLL bll = new InOutBLL();
                bll.AddSaleSheet(ord, lines, out sheet_no1);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("InOutBll>CreateSO", ex.ToString());
                throw ex;
            }
        }

        public DataTable GetSaleSPSheetList(string date1, string date2, string sup_no, string order_main)
        {
            var condition_sql = "";
            if (sup_no != "") condition_sql += " and a.sup_no  = '" + sup_no + "' ";
            if (order_main != "") condition_sql += " and a.order_main = '" + order_main + "' ";
            string sql = @"select a.*,b.branch_name,c.oper_name,d.oper_name as sale_man_name,e.oper_name as approve_man_name,f.sup_name
from co_t_order_main a 
left join (select * from bi_t_branch_info ) b on a.branch_no=b.branch_no 
left join (select supcust_no,sup_name from bi_t_supcust_info where supcust_flag='C') f on a.sup_no=f.supcust_no 
left join (select * from sa_t_operator_i) c on a.oper_id=c.oper_id 
left join bi_t_people_info d on a.order_man=d.oper_id 
left join (select * from sa_t_operator_i) e on a.approve_man=e.oper_id ";
            sql += "where  a.sheet_no LIKE 'SP%' and a.oper_date>='" + date1 + " 00:00:00' and a.oper_date<='" + date2 + " 23:59:59' " + condition_sql;
            sql += "order by a.sheet_no desc ";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);

            return tb;
        }

        public void GetSaleSPSheet(string sheet_no, out DataTable tb1, out DataTable tb2)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = $@"select a.*,b.branch_name,c.oper_name,d.oper_name as order_man_name,e.oper_name as approve_man_name,f.sup_name,f.sup_fullname 
from co_t_order_main a
left
join (select *from bi_t_branch_info ) b on a.branch_no = b.branch_no
left join(select supcust_no, sup_name,sup_fullname from bi_t_supcust_info where supcust_flag= 'C') f on a.sup_no = f.supcust_no
left join(select* from sa_t_operator_i) c on a.oper_id = c.oper_id
left join bi_t_people_info d on a.order_man = d.oper_id
left join(select* from sa_t_operator_i) e on a.approve_man = e.oper_id
where a.sheet_no='{sheet_no}'";
            tb1 = db.ExecuteToTable(sql, null);

            sql = $@"select b.item_name,a.*,b.item_subno, b.item_size,b.unit_no , cao.sup_name
FROM dbo.co_t_order_child a
left join bi_t_item_info b on a.item_no = b.item_no 
left join bi_t_supcust_info cao on a.supcust_no = cao.supcust_no
where a.sheet_no='{sheet_no}'";

            tb2 = db.ExecuteToTable(sql, null);
        }

        public void GetSaleSPSheetWeek(string date1, string date2, string cust_id, string sale_man, out DataTable tb1, out DataTable tb2)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = $@"SELECT m.supply_date, i.item_name, cd.order_qnty, i.unit_no, s.sup_name, c.sup_name cust_name, m.sheet_no FROM co_t_order_main m LEFT JOIN co_t_order_child cd ON m.sheet_no = cd.sheet_no LEFT JOIN bi_t_item_info i ON i.item_no = cd.item_no LEFT JOIN( SELECT supcust_no, sup_name, supcust_flag FROM bi_t_supcust_info) c ON c.supcust_no = m.sup_no AND c.supcust_flag = 'C' LEFT JOIN ( SELECT supcust_no, sup_name, supcust_flag FROM bi_t_supcust_info ) s ON s.supcust_no = cd.supcust_no AND s.supcust_flag = 'S' WHERE supply_date BETWEEN '{date1}' AND '{date2}' and cd.sheet_no like 'SP%' ";
            if (cust_id != "")
            {
                sql += $" and c.supcust_no = '{cust_id}'";
            }
            tb1 = db.ExecuteToTable(sql, null);

            // 预留 tb2
            tb2 = new DataTable();
        }
        #endregion

    }
}
