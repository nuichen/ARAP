using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DB;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using IvyTran.svr.ERP;
using Model;

namespace IvyTran.BLL.ERP
{
    public class RecipeMenuBll : IRecipeMenu
    {
        DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
        public DataTable GetRecipeMenus(DateTime startTime, DateTime endTime, string cust_no)
        {
            string sql = $@"SELECT m.sheet_no,
       m.supcust_no,
       c.sup_name,
       m.display_flag,
       m.use_date1,
       m.use_date2,
       m.oper_id,
       m.oper_date
FROM dbo.co_t_menu_order_main m
    LEFT JOIN dbo.bi_t_supcust_info c
        ON c.supcust_no = m.supcust_no and c.supcust_flag='C'
WHERE m.use_date1 BETWEEN '{startTime.Toyyyy_MM_dd_HH_mm_ss()}' AND '{endTime.Toyyyy_MM_dd_HH_mm_ss()}'
{(string.IsNullOrEmpty(cust_no) ? "" : $"AND m.supcust_no = '{cust_no}'")}
";
            DataTable tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        public DataTable GetRecipeMenusDetails(string sheet_no)
        {
            string sql = $@" SELECT d.flow_id,
       d.sheet_no,
       d.formula_no,
       fm.formula_name,
       d.qty,
       d.menu_type,
       d.use_type
FROM dbo.co_t_menu_order_detail d
    LEFT JOIN dbo.bi_t_formula_main fm
        ON fm.formula_no = d.formula_no
WHERE d.sheet_no = '{sheet_no}' ";
            DataTable tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        public void GetRecipeMenusDetails(DateTime time, string cust_no, out co_t_menu_order_main main, out DataTable details, out decimal income)
        {
            income = 0;
            string sql = $@"SELECT *
FROM dbo.co_t_menu_order_main
WHERE supcust_no='{cust_no}' AND CONVERT(VARCHAR(10), use_date1, 120) = '{time.Toyyyy_MM_dd()}';";
            main = db.ExecuteToModel<co_t_menu_order_main>(sql, null) ?? new co_t_menu_order_main();

            sql = $@"SELECT d.flow_id,
       d.sheet_no,
       d.formula_no,
	   fm.formula_name,
       d.qty,
       0.00 as price,
       d.menu_type,
       d.use_type
FROM dbo.co_t_menu_order_detail d
    LEFT JOIN dbo.co_t_menu_order_main m
        ON m.sheet_no = d.sheet_no
    LEFT JOIN dbo.bi_t_formula_main fm
        ON fm.formula_no = d.formula_no
WHERE CONVERT(VARCHAR(10), m.use_date1, 120) = '{time.Toyyyy_MM_dd()}'
      AND m.supcust_no = '{cust_no}';";

            details = db.ExecuteToTable(sql, null);
            //计算菜谱成本
            foreach (DataRow dr in details.Rows)
            {
                dr["price"] = cal_formula_price(dr["formula_no"].ToString());
            }

            sql = "select top 1 * from rp_t_supcust_income_revenue where supcust_no=@supcust_no and convert(varchar(10),in_date,120)=@in_date ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@supcust_no", cust_no),
                new System.Data.SqlClient.SqlParameter("@in_date", time.Toyyyy_MM_dd())
            };
            var dt = db.ExecuteToTable(sql, pars);
            if (dt.Rows.Count > 0)
            {
                income = Conv.ToDecimal(dt.Rows[0]["in_amount"]);
            }
        }

        decimal cal_formula_price(string formula_no)
        {
            decimal price = 0;
            string sql = "select * from bi_t_formula_detail where formula_no=@formula_no ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@formula_no", formula_no)
            };
            var dt = db.ExecuteToTable(sql, pars);
            IvyTran.IBLL.ERP.IPriceBLL bll = new PriceBLL();
            foreach (DataRow dr in dt.Rows)
            {
                price += Conv.ToDecimal(dr["qty"]) / Conv.ToDecimal(dr["unit_factor"]) * bll.GetLastInPrice(dr["item_no"].ToString());
            }
            return price;
        }


        public Dictionary<DateTime, List<co_t_menu_order_detail>> GetCalendarRecipeMenus(DateTime time, string cust_no)
        {
            string sql = $@"SELECT CONVERT(VARCHAR(10), m.use_date1, 120) use_date,
       d.flow_id,
       d.sheet_no,
       d.formula_no,
       d.qty,
       d.menu_type,
       d.use_type
FROM dbo.co_t_menu_order_detail d
    LEFT JOIN dbo.co_t_menu_order_main m
        ON m.sheet_no = d.sheet_no
WHERE CONVERT(VARCHAR(7), m.use_date1, 120) = '{time:yyyy-MM}'
      AND m.supcust_no = '{cust_no}';";

            var dic = new Dictionary<DateTime, List<co_t_menu_order_detail>>();

            DataTable tb = db.ExecuteToTable(sql, null);

            foreach (DataRow row in tb.Rows)
            {
                DateTime date = row["use_date"].ToDateTime();
                var detail = ReflectionHelper.DataRowToModel<co_t_menu_order_detail>(row);
                if (dic.TryGetValue(date, out var details))
                {
                    details.Add(detail);
                }
                else
                {
                    dic.Add(date, new List<co_t_menu_order_detail> { detail });
                }
            }

            return dic;
        }

        string CreateId()
        {
            string sql = "select sheet_value from sys_t_sheet_no where sheet_id='CD'";
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
                sql = "update sys_t_sheet_no set sheet_value=" + index + " where sheet_id='CD'";
                db.ExecuteScalar(sql, null);
                return "CD" + "00" + System.DateTime.Now.ToString("yyMMdd") + index.ToString().PadLeft(4, '0');
            }
        }
        string CreateId(IDB d)
        {
            string sql = "select sheet_value from sys_t_sheet_no where sheet_id='CD'";
            object obj = d.ExecuteScalar(sql, null);
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
                sql = "update sys_t_sheet_no set sheet_value=" + index + " where sheet_id='CD'";
                d.ExecuteScalar(sql, null);
                return "CD" + "00" + System.DateTime.Now.ToString("yyMMdd") + index.ToString().PadLeft(4, '0');
            }
        }
        int MaxDelatilCode(IDB d)
        {
            string sql = "SELECT ISNULL(MAX(flow_id),0)+1 FROM dbo.co_t_menu_order_detail";
            return d.ExecuteScalar(sql, null).ToInt32();
        }

        public void SaveRecipeMenu(co_t_menu_order_main main, List<co_t_menu_order_detail> details)
        {
            var db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                if (string.IsNullOrEmpty(main.sheet_no))
                {
                    //新增
                    main.sheet_no = CreateId();
                    main.oper_date = DateTime.Now;
                    main.display_flag = "1";
                    d.Insert(main);
                }
                else
                {
                    main.oper_date = DateTime.Now;
                    d.Update(main, "sheet_no");

                    d.ExecuteScalar($@"DELETE FROM dbo.co_t_menu_order_detail WHERE sheet_no='{main.sheet_no}'", null);
                }

                foreach (var detail in details)
                {
                    detail.flow_id = MaxDelatilCode(d);
                    detail.sheet_no = main.sheet_no;
                    d.Insert(detail);
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

        public void CopyRecipeMenu(List<DateTime> times, string sourceCustNo, string toCustNo, string oper_id)
        {
            string sql = "";
            var db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();


                foreach (var time in times)
                {
                    if (time.Date < DateTime.Now.Date)
                    {
                        continue;
                    }

                    //源菜单
                    string sheet_no = d.ExecuteScalar($@"SELECT sheet_no 
FROM dbo.co_t_menu_order_main
WHERE CONVERT(VARCHAR(10),use_date1,120) ='{time.Toyyyy_MM_dd()}'
AND supcust_no='{sourceCustNo}'", null)?.ToString();
                    if (string.IsNullOrWhiteSpace(sheet_no))
                    {
                        continue;
                    }

                    //目标原菜单
                    string ord_sheet_no = d.ExecuteScalar($@"SELECT sheet_no 
FROM dbo.co_t_menu_order_main
WHERE CONVERT(VARCHAR(10),use_date1,120) ='{time.Toyyyy_MM_dd()}'
AND supcust_no='{toCustNo}'", null)?.ToString();
                    string new_sheet_no = CreateId(d);//目标新菜单

                    sql = $@"INSERT INTO dbo.co_t_menu_order_main(sheet_no,supcust_no,display_flag,use_date1,use_date2,oper_id,oper_date)
SELECT '{new_sheet_no}','{toCustNo}',display_flag,use_date1,use_date2,oper_id,oper_date FROM dbo.co_t_menu_order_main
WHERE sheet_no='{sheet_no}'";
                    d.ExecuteScalar(sql, null);

                    int flow_id = MaxDelatilCode(d);
                    sql = $@"INSERT INTO dbo.co_t_menu_order_detail(flow_id,sheet_no,formula_no,qty,menu_type,use_type)
SELECT {flow_id}+(ROW_NUMBER() over (order by d.flow_id)),'{new_sheet_no}',d.formula_no,d.qty,d.menu_type,d.use_type FROM dbo.co_t_menu_order_detail d
WHERE sheet_no='{sheet_no}' ";
                    d.ExecuteScalar(sql, null);

                    d.ExecuteScalar($@"DELETE FROM dbo.co_t_menu_order_main WHERE sheet_no='{ord_sheet_no}';
DELETE FROM dbo.co_t_menu_order_detail WHERE sheet_no='{ord_sheet_no}';", null);
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

        public void DelRecipeMenus(string sheet_nos)
        {
            var db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                d.ExecuteScalar($@"DELETE FROM dbo.co_t_menu_order_main WHERE sheet_no in ('',{sheet_nos})", null);
                d.ExecuteScalar($@"DELETE FROM dbo.co_t_menu_order_detail WHERE sheet_no in ('',{sheet_nos})", null);

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

        private string createOrdId()
        {
            string str = "";
            //
            string year = DateTime.Now.Year.ToString().Substring(2);//2位
            string day = DateTime.Now.DayOfYear.ToString().PadLeft(3, '0');//3位
            string time = Math.Floor(System.DateTime.Now.TimeOfDay.TotalMinutes).ToString().PadLeft(5, '0');//5位
            Random random = new Random();
            string rnd = random.Next(1, 999999).ToString().PadLeft(6, '0');//6位
            //
            str = year + day + time + rnd;//16位
            //
            return str;
        }
        public void GeneratePlan(List<DateTime> times, string cust_no, string oper_id)
        {
            string sql = "";
            var db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                sql = $@"SELECT * FROM dbo.bi_t_supcust_info WHERE supcust_flag='C' AND supcust_no='{cust_no}'";
                bi_t_supcust_info cust = d.ExecuteToModel<bi_t_supcust_info>(sql, null);
                if (cust == null || string.IsNullOrEmpty(cust.supcust_no))
                {
                    throw new Exception($"客户:[{cust_no}]不存在");
                }

                string insertMainSql = $@"INSERT INTO tr_order(ord_id,status,mc_id,qty,amount,enable_qty,enable_amount,create_time,company,sname,mobile,address,
openid,msg_hand,send_status,pay_type,build_status,cus_id,card_pay,flow_no,distribution_type,open_type,discount_amt,take_fee,cus_remark,
wm_fee_bag,refund_amt,pay_weixin,reach_time,cus_no,salesman_id
)
VALUES
(@ord_id, @status, @mc_id, @qty, @amount, @enable_qty, @enable_amount, @create_time, @company, @sname, @mobile,
 @address, @openid, '0', '0', @pay_type, @build_status, @cus_id, @card_pay, @flow_no, @distribution_type, @open_type,
 @discount_amt, @take_fee, @cus_remark, @wm_fee_bag, @refund_amt, @pay_weixin, @reach_time, @cus_no, @salesman_id);";
                string insertDetailSql = @"insert into dbo.tr_order_item (ord_id,row_index,goods_id,qty,price,amount,enable) 
VALUES(@ord_id,@row_index,@goods_id,@qty,@price,@amount,'1')";
                System.Data.SqlClient.SqlParameter[] pars;

                foreach (DateTime dateTime in times)
                {
                    if (dateTime.Date < DateTime.Now.Date)
                    {
                        //生成的计划时间 小于当天
                        continue;
                    }
                    sql = $@"SELECT CONVERT(VARCHAR(10), m.use_date1, 120) use_date,
       c.item_no,
       c.unit_no,
       SUM(c.qty*a.qty) qty,
       MAX(c.price) price
FROM dbo.co_t_menu_order_detail a
    INNER JOIN dbo.co_t_menu_order_main m
        ON m.sheet_no = a.sheet_no
    INNER JOIN dbo.bi_t_formula_main b
        ON a.formula_no = b.formula_no
    INNER JOIN dbo.bi_t_formula_detail c
        ON c.formula_no = b.formula_no
WHERE CONVERT(VARCHAR(10), m.use_date1, 120) = '{dateTime.Toyyyy_MM_dd()}'
      AND m.supcust_no = '{cust_no}'
GROUP BY CONVERT(VARCHAR(10), m.use_date1, 120),
         c.item_no,
         c.unit_no;";
                    DataTable details = d.ExecuteToTable(sql, null);
                    if (details.Rows.Count < 1)
                    {
                        //某天没有菜单
                        continue;
                    }

                    Model.tr_order order = new Model.tr_order();
                    order.ord_id = createOrdId();

                    int row_index = 0;
                    decimal total_qty = 0;
                    decimal total_amt = 0;
                    foreach (DataRow row in details.Rows)
                    {
                        decimal price = row["price"].ToDecimal();
                        decimal qty = row["qty"].ToDecimal();
                        if (qty <= 0)
                        {
                            continue;
                        }
                        string unit_no = row["unit_no"].ToString();
                        decimal amount = 0;
                        if ("g".Equals(unit_no))
                        {
                            qty /= 1000;
                            amount = qty * price;
                        }
                        else
                        {
                            amount = qty * price;
                        }

                        pars = new System.Data.SqlClient.SqlParameter[]
                        {
                            new System.Data.SqlClient.SqlParameter("@ord_id",order.ord_id),
                            new System.Data.SqlClient.SqlParameter("@row_index",++row_index),
                            new System.Data.SqlClient.SqlParameter("@goods_id",row["item_no"].ToString()),
                            new System.Data.SqlClient.SqlParameter("@qty",qty),
                            new System.Data.SqlClient.SqlParameter("@price",price),
                            new System.Data.SqlClient.SqlParameter("@amount",amount),
                        };
                        d.ExecuteScalar(insertDetailSql, pars);
                        total_qty += qty;
                        total_amt += amount;
                    }

                    order.qty = order.enable_qty = total_qty;
                    order.amount = order.enable_amount = total_amt;
                    order.address = cust.sup_addr;
                    order.company = "";
                    order.mobile = cust.sup_tel;
                    order.sname = cust.sup_man;
                    order.create_time = DateTime.Now;
                    order.reach_time = dateTime.Toyyyy_MM_dd_HH_mm_ss();
                    order.pay_type = "1";
                    order.status = "0";
                    order.build_status = "1";
                    order.mc_id = 1;
                    order.distribution_type = "0";
                    order.discount_amt = 0;
                    order.cus_no = cust_no;
                    order.salesman_id = "";
                    order.card_pay = 0;
                    order.flow_no = "";

                    pars = new System.Data.SqlClient.SqlParameter[]
                    {
                        new System.Data.SqlClient.SqlParameter("@ord_id",order.ord_id),
                        new System.Data.SqlClient.SqlParameter("@status",order.status),
                        new System.Data.SqlClient.SqlParameter("@mc_id",order.mc_id),
                        new System.Data.SqlClient.SqlParameter("@qty",order.qty),
                        new System.Data.SqlClient.SqlParameter("@amount",order.amount),
                        new System.Data.SqlClient.SqlParameter("@enable_qty",order.enable_qty),
                        new System.Data.SqlClient.SqlParameter("@enable_amount",order.enable_amount),
                        new System.Data.SqlClient.SqlParameter("@create_time",order.create_time.ToString("yyyy/MM/dd HH:mm:ss")),
                        new System.Data.SqlClient.SqlParameter("@company",order.company.ToNullString()),
                        new System.Data.SqlClient.SqlParameter("@sname",order.sname.ToNullString()),
                        new System.Data.SqlClient.SqlParameter("@mobile",order.mobile.ToNullString()),
                        new System.Data.SqlClient.SqlParameter("@address",order.address.ToNullString()),
                        new System.Data.SqlClient.SqlParameter("@openid",order.openid.ToNullString()),
                        new System.Data.SqlClient.SqlParameter("@pay_type",order.pay_type),
                        new System.Data.SqlClient.SqlParameter("@build_status",order.build_status),
                        new System.Data.SqlClient.SqlParameter("@cus_id","0"),
                        new System.Data.SqlClient.SqlParameter("@card_pay",order.card_pay.ToNullString()),
                        new System.Data.SqlClient.SqlParameter("@flow_no",order.flow_no),
                        new System.Data.SqlClient.SqlParameter("@distribution_type",order.distribution_type),
                        new System.Data.SqlClient.SqlParameter("@open_type","1"),//菜谱下单1 毛菜下单0
                        new System.Data.SqlClient.SqlParameter("@discount_amt","0"),
                        new System.Data.SqlClient.SqlParameter("@take_fee","0"),
                        new System.Data.SqlClient.SqlParameter("@cus_remark","0"),
                        new System.Data.SqlClient.SqlParameter("@wm_fee_bag","0"),
                        new System.Data.SqlClient.SqlParameter("@refund_amt","0"),
                        new System.Data.SqlClient.SqlParameter("@pay_weixin","0"),
                        new System.Data.SqlClient.SqlParameter("@reach_time",order.reach_time),
                        new System.Data.SqlClient.SqlParameter("@cus_no",order.cus_no),
                        new System.Data.SqlClient.SqlParameter("@salesman_id",order.salesman_id)
                     };
                    d.ExecuteScalar(insertMainSql, pars);

                    IBLL.OnLine.IOrder bll = new OnLine.Order();
                    bll.Pass(order, oper_id, d);
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

    }
}