using System;
using System.Collections.Generic;
using System.Data;
using DB;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;

namespace IvyTran.BLL.ERP
{
    public class SaleOrder : ISaleOrderBLL
    {
        /// <summary>
        /// 获取新单号
        /// </summary>
        /// <returns>新单号</returns>
        public string GetNewSheetNo()
        {
            IDB db = new DBByAutoClose(AppSetting.conn);
            string sql = @"select ( isnull ( sheet_value , 0 ) + 1 )
from sys_t_sheet_no where Lower ( sheet_id ) = Lower ( 'SS' ) ";

            //SS 00180718 0001
            //  StringBuilder 单据号 = new StringBuilder("SS");
            string year = Math.Abs(DateTime.Now.Year - 2000).ToString("0000");//4位
            string month = DateTime.Now.Month.ToString("00");
            string day = DateTime.Now.Day.ToString("00");
            int num = Conv.ToInt(db.ExecuteScalar(sql, null)); //后4位
            return "SS" + year + month + day + num.ToString("0000");
        }

        /// <summary>
        /// 获取新行号
        /// </summary>
        /// <returns>新行号</returns>
        public string GetRowId()
        {
            string sql = "select isnull(MAX(IDENTITYCOL)+1,1) from co_t_order_child ";
            IDB db = new DBByAutoClose(AppSetting.conn);
            string rowId = db.ExecuteScalar(sql, null).ToString();
            return rowId;
        }

        /// <summary>
        /// 获取仓库信息
        /// </summary>
        /// <returns>仓库列表</returns>
        public DataTable GetBranchList()
        {
            string sql = @"SELECT branch_no,branch_name  FROM bi_t_branch_info ";
            IDB db = new DBByAutoClose(AppSetting.conn);

            return db.ExecuteToTable(sql, null);
        }

        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <returns>客户列表</returns>
        public DataTable GetSupList()
        {
            string sql = @"SELECT supcust_no,isnull ( sup_name , '' ) sup_name
FROM bi_t_supcust_info 
WHERE supcust_flag ='C' ";
            IDB db = new DBByAutoClose(AppSetting.conn);

            return db.ExecuteToTable(sql, null);
        }

        /// <summary>
        /// 获取操作员信息
        /// </summary>
        /// <returns>操作员列表</returns>
        public DataTable GetOperList()
        {
            string sql = @" SELECT oper_id,   
         oper_name,   
         oper_pw,   
         oper_status,   
         oper_type,   
         aa=''
    FROM sa_t_operator_i  
	WHERE	oper_type like '%'
     AND oper_type <> '0' and oper_id <> '0'";
            IDB db = new DBByAutoClose(AppSetting.conn);

            return db.ExecuteToTable(sql, null);
        }

        /// <summary>
        /// 获取指定商品列表
        /// </summary>
        /// <param name="item_name">商品名称</param>
        /// <param name="barcode">商品条码</param>
        /// <returns>商品列表</returns>
        public DataTable GetItemList(string item_name, string barcode)
        {
            string sql = @"select *
FROM bi_t_item_info
where 1=1 ";
            if (!string.IsNullOrEmpty(item_name))
                sql += " and item_name like '" + item_name + "'";
            if (!string.IsNullOrEmpty(barcode))
                sql += " and barcode = '" + barcode + "'";
            IDB db = new DBByAutoClose(AppSetting.conn);
            return db.ExecuteToTable(sql, null);
        }

        /// <summary>
        /// 新增主收货单、子收货单
        /// </summary>
        /// <param name="main">主收货单</param>
        /// <param name="childs">子收货单列表</param>
        public void InsertOrderMainAndChilds(co_t_order_main main, List<co_t_order_child> childs)
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            try
            {
                IDB d = db;
                db.Open();
                db.BeginTran();

                string sql = "update sys_t_sheet_no set sheet_value =sheet_value+1 where lower ( sheet_id ) =lower ( 'SS' ) ";

                d.ExecuteScalar(sql, null);

                d.Insert(main);

                foreach (co_t_order_child item in childs)
                {
                    d.Insert(item, "flow_id");
                }
                db.CommitTran();
                db.Close();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                throw ex;
            }
        }

        /// <summary>
        /// 获取指定主收货单列表
        /// </summary>
        /// <param name="dtStart">开始日期</param>
        /// <param name="dtEnd">结束日期</param>
        /// <returns>主收货单列表</returns>
        public DataTable GetOrderMainList(DateTime dtStart, DateTime dtEnd)
        {
            //单据号,有效期，日期，订单总金额，订单金额，仓库名称，客户   
            string sql = @"SELECT co_t_order_main.sheet_no sheet_no,  
         co_t_order_main.valid_date valid_date,   
         co_t_order_main.oper_date oper_date,   
         co_t_order_main.total_amount  total_amount,   
         co_t_order_main.paid_amount  paid_amount,   
         bi_t_branch_info.branch_name branch_name,   
         bi_t_supcust_info.sup_name sup_name
    FROM co_t_order_main,   
         bi_t_branch_info,   
         bi_t_supcust_info  
   WHERE ( co_t_order_main.branch_no = bi_t_branch_info.branch_no ) and
         ( co_t_order_main.sup_no = bi_t_supcust_info.supcust_no ) and
			( bi_t_supcust_info.supcust_flag = 'C' ) and
         ( co_t_order_main.trans_no = 'S' ) and
         ( isnull(co_t_order_main.approve_flag,'0') like '%' ) and";

            sql += "      ( convert(varchar(10),co_t_order_main.oper_date,120) >= '" + dtStart.ToString("yyyy-MM-dd") + "' ) and ";
            sql += "      ( convert(varchar(10),co_t_order_main.oper_date,120) <= '" + dtEnd.ToString("yyyy-MM-dd") + "' )";
            IDB db = new DBByAutoClose(AppSetting.conn);

            return db.ExecuteToTable(sql, null);
        }

        /// <summary>
        /// 获取指定子收货单
        /// </summary>
        /// <param name="sheet_no">单号</param>
        /// <returns>子收货单列表</returns>
        public DataTable GetOrderChildList(string sheet_no)
        {//商品编号，单位，单价，总数量，金额，折扣，条码，商品名称，规格，箱数，零数
            string sql = @"SELECT     a.item_no item_no,
           a.unit_no unit_no,
           a.in_price in_price,
           a.order_qnty order_qnty,
           a.sub_amount sub_amount,
           a.discount discount,
           a.barcode barcode,
           b.item_name item_name ,
           b.item_size item_size ,
           a.packqty packqty,
           a.sgqty sgqty
           FROM 	co_t_order_child a 
		   left join   bi_t_item_info  b  on (a.item_no = b.item_no)
           WHERE	( a.sheet_no = '???')  
           ORDER BY a.flow_id ASC  ".Replace("???", sheet_no);
            IDB db = new DBByAutoClose(AppSetting.conn);
            return db.ExecuteToTable(sql, null);
        }

    }
}