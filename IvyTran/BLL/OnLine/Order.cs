using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using DB;
using IvyTran.DAL;
using IvyTran.Helper;
using IvyTran.IBLL;
using IvyTran.IBLL.OnLine;
using Model;

namespace IvyTran.BLL.OnLine
{
    public class Order : IOrder
    {
        public void GetOrderNew(out string key)
        {
            var dt = System.DateTime.Now;
            dt = dt.AddDays(-7);
            // string sql = "select * from tr_order where build_status='1' and status='0' ";
            //   string sql = "select * from tr_order left join bi_t_supcust_info on tr_order.cus_no=bi_t_supcust_info.supcust_no where build_status='1' and status='0' and bi_t_supcust_info.supcust_flag='C' ";
            //   sql += "and convert(varchar(10),create_time,120)>='" + dt.ToString("yyyy-MM-dd") + "' order by create_time";

            //  string sql= "select * from tr_order left join bi_t_supcust_info on tr_order.cus_no=bi_t_supcust_info.supcust_no where build_status='1' and status='0' and bi_t_supcust_info.supcust_flag='C' ";
            //  sql += "and convert(varchar(10),tr_order.create_time,120)>='" + dt.ToString("yyyy-MM-dd") + "' order by tr_order.create_time";
            string sql = @"select tr_order.*,bi_t_supcust_info.sup_name from tr_order left join (select * from bi_t_supcust_info where supcust_flag='C') bi_t_supcust_info
on tr_order.cus_no = bi_t_supcust_info.supcust_no
where build_status = '1' and status = '0' and bi_t_supcust_info.supcust_flag = 'C'
 and convert(varchar(10),tr_order.create_time,120)>='" + dt.ToString("yyyy-MM-dd") + "' order by tr_order.create_time";
            var db = new DB.DBByAutoClose(AppSetting.conn);
            DB.IDB d = db;
            var tb = d.ExecuteToTable(sql, null);
            int flag = 0;
            foreach (DataRow row in tb.Rows)
            {
                if (row["sup_name"].ToString() != "")
                {
                    flag = 1;

                }
            }
            LogHelper.writeLog("order -> sup_name", "flag:" + flag.ToString());
            key = System.Guid.NewGuid().ToString().Replace("-", "");
            HttpContext.Current.Session[key] = tb;
        }

        public void GetOrderPass(string date1, string date2, string keyword, out string key)
        {
            var condition_sql = "";

            if (keyword != "")
            {
                condition_sql += " and (ord_id like '%'+@keyword+'%' or sname like '%'+@keyword+'%' or tr_order.mobile like '%'+@keyword+'%') ";
            }
            string sql = "select tr_order.*,bi_t_supcust_info.sup_name from tr_order left join (select * from bi_t_supcust_info where supcust_flag='C') bi_t_supcust_info " +
                         "on tr_order.cus_no = bi_t_supcust_info.supcust_no where convert(varchar(10),tr_order.create_time,120)>=@date1 and convert(varchar(10),tr_order.create_time,120)<=@date2 ";
            sql += "and build_status='1' and send_status<>'1' and status='1' " + condition_sql + " order by tr_order.create_time ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@date1",date1),
                new System.Data.SqlClient.SqlParameter("@date2",date2),
                new System.Data.SqlClient.SqlParameter("@keyword",keyword)
            };
            var db = new DB.DBByAutoClose(AppSetting.conn);
            DB.IDB d = db;
            var tb = d.ExecuteToTable(sql, pars);
            key = System.Guid.NewGuid().ToString().Replace("-", "");
            HttpContext.Current.Session[key] = tb;
        }

        public void GetOrderDisable(string date1, string date2, string keyword, out string key)
        {
            var condition_sql = "";

            if (keyword != "")
            {
                condition_sql += " and (ord_id like '%'+@keyword+'%' or sname like '%'+@keyword+'%' or mobile like '%'+@keyword+'%') ";
            }
            string sql = "select tr_order.*,bi_t_supcust_info.sup_name from tr_order left join (select * from bi_t_supcust_info where supcust_flag='C') bi_t_supcust_info " +
                         "on tr_order.cus_no = bi_t_supcust_info.supcust_no where convert(varchar(10),tr_order.create_time,120)>=@date1 and convert(varchar(10),tr_order.create_time,120)<=@date2 ";
            sql += "and build_status='1' and status='2' " + condition_sql + " order by tr_order.create_time desc";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@date1",date1),
                new System.Data.SqlClient.SqlParameter("@date2",date2),
                new System.Data.SqlClient.SqlParameter("@keyword",keyword)
            };
            var db = new DB.DBByAutoClose(AppSetting.conn);
            DB.IDB d = db;
            var tb = d.ExecuteToTable(sql, pars);
            //
            key = System.Guid.NewGuid().ToString().Replace("-", "");
            HttpContext.Current.Session[key] = tb;
        }

        public void GetOrderComplete(string date1, string date2, string keyword, string only_show_no_receice, out string key)
        {
            var condition_sql = "";

            if (keyword != "")
            {
                condition_sql += " and (ord_id like '%'+@keyword+'%' or sname like '%'+@keyword+'%' or mobile like '%'+@keyword+'%') ";
            }
            if (only_show_no_receice == "1")
            {
                condition_sql += " and send_status='1' ";
            }
            else
            {
                condition_sql += " and send_status in('1','2') ";
            }
            string sql = "select tr_order.*,bi_t_supcust_info.sup_name from tr_order left join (select * from bi_t_supcust_info where supcust_flag='C') bi_t_supcust_info " +
                         "on tr_order.cus_no = bi_t_supcust_info.supcust_no where convert(varchar(10),tr_order.create_time,120)>=@date1 and convert(varchar(10),tr_order.create_time,120)<=@date2 ";
            sql += "and build_status='1' and status='1' " + condition_sql + " order by tr_order.create_time desc";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@date1",date1),
                new System.Data.SqlClient.SqlParameter("@date2",date2),
                new System.Data.SqlClient.SqlParameter("@keyword",keyword)
            };

            var db = new DB.DBByAutoClose(AppSetting.conn);
            DB.IDB d = db;
            var tb = d.ExecuteToTable(sql, pars);
            //
            key = System.Guid.NewGuid().ToString().Replace("-", "");
            HttpContext.Current.Session[key] = tb;
        }

        public void Pass(string ord_id, string oper_id)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                //
                tr_orderDAL orderDAL = new tr_orderDAL(d);
                var tr_order = orderDAL.SelectById(ord_id);

                if (tr_order == null)
                {
                    throw new Exception("不存在该订单！");
                }
                if (tr_order.status == "1")
                {
                    throw new Exception("该订单已审核！");
                }
                if (tr_order.status == "2")
                {
                    throw new Exception("该订单已失效！");
                }
                if (tr_order.build_status == "0")
                {
                    throw new ExceptionBase("单据创建中");
                }
                if (tr_order.build_status == "2")
                {
                    throw new ExceptionBase("单据创建失效");
                }
                string sheet_no = CreateNewSheetNo(d);
                string foot_str = "用户接单成功";
                if (tr_order.distribution_type == "1")
                {
                    tr_order.to_the_code = GetToTheCode();
                    foot_str += ",自提码：" + tr_order.to_the_code;
                }
                tr_order.status = "1";
                tr_order.sheet_no = sheet_no;
                tr_order.check_oper_id = oper_id;
                d.Update(tr_order, "ord_id", "status,to_the_code,check_oper_id,sheet_no");
                //创建Pos订单
                var dal = new tr_order_itemDAL(d);
                List<Model.tr_order_item> items = dal.SelectById(ord_id);
                CreatePosSheet(tr_order, items, sheet_no, d, oper_id);

                //接单成功发送消息推送
                try
                {
                    message_parsDAL messageDAL = new message_parsDAL(db);
                    //获取模板ID的编号
                    int message_id = 102;
                    var message_pars = messageDAL.SelectById(tr_order.mc_id, message_id, tr_order.open_type);
                    if (message_pars != null)
                    {
                        message_sale_demo food = new message_sale_demo();
                        string url = AppSetting.send_url + "/order_detail.html?order_id=" + tr_order.ord_id;
                        //string url = "";
                        string head_str = message_pars.title;
                        string data = food.get_mall_order_accept_demo(tr_order.openid, message_pars.template_id.ToString(), url, head_str, foot_str, tr_order.ord_id, tr_order.enable_amount.ToString(), GetPayType(tr_order.pay_type), DateTime.Now.ToString("yyyy-MM-dd"));
                        IMessage messagebll = new Message();
                        string ret = messagebll.Create(data, tr_order.open_type);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                db.CommitTran();
            }
            catch (Exception e)
            {
                db.RollBackTran();
                LogHelper.writeLog("Order -> Pass", e.ToString(), ord_id);
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        public void Pass(tr_order tr_order, string oper_id, IDB d)
        {
            string sheet_no = CreateNewSheetNo(d);
            string foot_str = "用户接单成功";
            if (tr_order.distribution_type == "1")
            {
                tr_order.to_the_code = GetToTheCode();
                foot_str += ",自提码：" + tr_order.to_the_code;
            }
            tr_order.status = "1";
            tr_order.sheet_no = sheet_no;
            tr_order.check_oper_id = oper_id;
            d.Update(tr_order, "ord_id", "status,to_the_code,check_oper_id,sheet_no");
            //创建Pos订单
            var dal = new tr_order_itemDAL(d);
            List<Model.tr_order_item> items = dal.SelectById(tr_order.ord_id);
            CreatePosSheet(tr_order, items, sheet_no, d, oper_id);

            //接单成功发送消息推送
            try
            {
                message_parsDAL messageDAL = new message_parsDAL(d);
                //获取模板ID的编号
                int message_id = 102;
                var message_pars = messageDAL.SelectById(tr_order.mc_id, message_id, tr_order.open_type);
                if (message_pars != null)
                {
                    message_sale_demo food = new message_sale_demo();
                    string url = AppSetting.send_url + "/order_detail.html?order_id=" + tr_order.ord_id;
                    //string url = "";
                    string head_str = message_pars.title;
                    string data = food.get_mall_order_accept_demo(tr_order.openid, message_pars.template_id.ToString(), url, head_str, foot_str, tr_order.ord_id, tr_order.enable_amount.ToString(), GetPayType(tr_order.pay_type), DateTime.Now.ToString("yyyy-MM-dd"));
                    IMessage messagebll = new Message();
                    string ret = messagebll.Create(data, tr_order.open_type);
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex);
            }
        }

        public void Preview(string ord_id)
        {
            DB.IDB d = new DB.DBByAutoClose(AppSetting.conn);
            try
            {
                tr_orderDAL orderDAL = new tr_orderDAL(d);
                var tr_order = orderDAL.SelectById(ord_id);

                if (tr_order == null)
                {
                    throw new Exception("不存在该订单！");
                }

            }
            catch (Exception e)
            {
                LogHelper.writeLog("Order -> Pass", e.ToString(), ord_id);
                throw;
            }
            finally
            {

            }
        }

        public void Disable(string ord_id)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                //
                tr_orderDAL orderDAL = new tr_orderDAL(d);
                var tr_order = orderDAL.SelectById(ord_id);

                if (tr_order == null)
                {
                    throw new ExceptionBase("不存在单据");
                }
                if (tr_order.build_status == "0")
                {
                    throw new ExceptionBase("单据创建中");
                }
                if (tr_order.build_status == "2")
                {
                    throw new ExceptionBase("单据创建失效");
                }
                if (tr_order.status == "1")
                {
                    throw new ExceptionBase("单据已审核");
                }
                if (tr_order.status == "2")
                {
                    throw new ExceptionBase("单据已失效");
                }
                tr_order.status = "2";
                d.Update(tr_order, "ord_id", "status");
                //
                db.CommitTran();
            }
            catch (Exception e)
            {
                db.RollBackTran();
                LogHelper.writeLog("Order -> Disable()", e.ToString(), ord_id);
                throw;
            }
        }

        public void DisableRow(string ord_id, int row_index)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                //提取单据
                tr_orderDAL orderDAL = new tr_orderDAL(db);
                var tr_order = orderDAL.SelectById(ord_id);
                if (tr_order == null)
                {
                    throw new ExceptionBase("不存在单据");
                }
                if (tr_order.build_status != "1")
                {
                    throw new ExceptionBase("单据未成功创建");
                }
                if (tr_order.status != "0")
                {
                    throw new ExceptionBase("单据已经审核");
                }

                //提取单据行
                tr_order_itemDAL orderitemDAL = new tr_order_itemDAL(db);
                var tr_order_item = orderitemDAL.SelectById(tr_order.ord_id, row_index);
                if (tr_order_item == null)
                {
                    throw new ExceptionBase("不存在行");
                }
                if (tr_order_item.enable == "0")
                {
                    throw new ExceptionBase("行已经失效");
                }
                //标记行为失效
                tr_order_item.enable = "0";
                d.Update(tr_order_item, "ord_id,row_index", "enable");
                //修改单据有效数量，有效金额
                tr_order.enable_qty -= tr_order_item.qty;
                tr_order.enable_amount -= tr_order_item.amount;
                if (tr_order.enable_qty == 0)//所有行都失效
                {
                    tr_order.status = "2";
                    d.Update(tr_order, "ord_id", "enable_qty,enable_amount,status");
                }
                else
                {
                    d.Update(tr_order, "ord_id", "enable_qty,enable_amount");
                }
                //
                db.CommitTran();
            }
            catch (Exception e)
            {
                db.RollBackTran();
                LogHelper.writeLog("Order -> DisableRow()", e.ToString(), ord_id, row_index.ToString());
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        public void Send(string ord_id, string oper_id)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                //
                tr_orderDAL orderDAL = new tr_orderDAL(db);
                var ord = orderDAL.SelectById(ord_id);
                if (ord == null)
                {
                    throw new ExceptionBase("不存在单据");
                }
                if (ord.mc_id != 1)
                {
                    throw new ExceptionBase("不存在单据");
                }
                if (ord.build_status == "0")
                {
                    throw new ExceptionBase("单据创建中");
                }
                if (ord.build_status == "2")
                {
                    throw new ExceptionBase("单据创建失效");
                }
                if (ord.status == "0")
                {
                    throw new ExceptionBase("单据未审核");
                }
                if (ord.status == "2")
                {
                    throw new ExceptionBase("单据已失效");
                }
                if (ord.send_status == "1")
                {
                    throw new ExceptionBase("已送出");
                }
                if (ord.send_status == "2")
                {
                    throw new ExceptionBase("已确认收货");
                }
                ord.send_status = "1";
                ord.check_oper_id = oper_id;
                d.Update(ord, "ord_id", "send_status,check_oper_id");

                //
                db.CommitTran();

                //配送发送消息推送
                try
                {
                    message_parsDAL messageDAL = new message_parsDAL(db);
                    merchant_configDAL configDAL = new merchant_configDAL(db);
                    var peisong_tel = configDAL.SelectValue(1, "peisong_tel");
                    string sql = "select top 1 mc_name from merchant ";
                    var dt = d.ExecuteToTable(sql, null);
                    var mc_name = "";
                    if (dt.Rows.Count > 0) mc_name = dt.Rows[0]["mc_name"].ToString();
                    //获取模板ID的编号
                    int message_id = 104;
                    var message_pars = messageDAL.SelectById(ord.mc_id, message_id, ord.open_type);
                    if (message_pars != null)
                    {
                        message_sale_demo food = new message_sale_demo();
                        string url = AppSetting.send_url + "/order_detail.html?order_id=" + ord.ord_id;
                        string foot_str = "您的订单已经开始配送，请注意查收！";
                        string head_str = message_pars.title;
                        string content = "单号：" + ord.ord_id + "  " + "金额：" + (ord.enable_amount - ord.discount_amt + ord.take_fee).ToString() + " 元";
                        string data = food.get_order_send_demo(ord.openid, message_pars.template_id.ToString(), url, head_str, foot_str, content, mc_name, peisong_tel);
                        IMessage messagebll = new Message();
                        string ret = messagebll.Create(data, ord.open_type);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception e)
            {
                db.RollBackTran();
                LogHelper.writeLog("Order -> Send()", e.ToString(), ord_id);
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        public void GetOrder(string ord_id, out tr_order ord, out DataTable dtLine, out int un_read_num)
        {
            var db = new DB.DBByAutoClose(AppSetting.conn);
            DB.IDB d = db;
            tr_orderDAL orderDAL = new tr_orderDAL(d);
            ord = orderDAL.SelectById(ord_id);
            if (ord == null)
            {
                throw new ExceptionBase("不存在单据");
            }
            //
            string sql = "select a.*,b.goods_no,b.goods_name from tr_order_item a ";
            sql += "left join goods b on a.goods_id=b.goods_id where a.ord_id=@ord_id order by a.row_index";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@ord_id",ord_id)
            };
            dtLine = d.ExecuteToTable(sql, pars);
            //un_read_num
            sql = "select count(ord_id) as total from tr_order where status='0' and build_status='1' and msg_hand='0' ";
            sql += "and convert(varchar(10),create_time,120) >= '" + DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + "' ";
            var tb = d.ExecuteToTable(sql, null);
            un_read_num = Conv.ToInt(tb.Rows[0]["total"]);
        }

        public List<tr_order> GetNewMsg()
        {
            var db = new DB.DBByAutoClose(AppSetting.conn);
            DB.IDB d = db;
            string sql = "select * from tr_order where status='0'and build_status='1' and msg_hand='0' ";
            sql += "and convert(varchar(10),create_time,120)>='" + DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + "' order by create_time";

            var tb = d.ExecuteToTable(sql, null);
            var lst = new List<Model.tr_order>();
            foreach (DataRow row in tb.Rows)
            {
                lst.Add(DB.ReflectionHelper.DataRowToModel<Model.tr_order>(row));
            }
            return lst;
        }

        public void GetFirstNewOrder(out tr_order ord, out DataTable dtLine, out int un_read_num)
        {
            var db = new DB.DBByAutoClose(AppSetting.conn);
            DB.IDB d = db;
            //

            string sql = "select ord_id from tr_order where status='0'and build_status='1' and msg_hand='0' ";
            sql += "and convert(varchar(10),create_time,120) >= '" + DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + "' ";
            sql += "order by create_time";

            var tb = d.ExecuteToTable(sql, null);
            un_read_num = tb.Rows.Count;
            var ord_id = "";
            if (tb.Rows.Count != 0)
            {
                ord_id = tb.Rows[0]["ord_id"].ToString();
            }
            //
            tr_orderDAL orderDAL = new tr_orderDAL(db);
            ord = orderDAL.SelectById(ord_id);
            //

            tr_order_itemDAL orderitemDAL = new tr_order_itemDAL(db);
            //
            sql = "select a.*,b.goods_no,b.goods_name from tr_order_item a ";
            sql += "left join goods b on a.goods_id=b.goods_id where a.ord_id=@ord_id order by a.row_index";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@ord_id",ord_id)
            };
            dtLine = d.ExecuteToTable(sql, pars);
        }

        public void SignRead(string ord_id)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                tr_orderDAL orderDAL = new tr_orderDAL(db);
                var tr_order = orderDAL.SelectById(ord_id);
                if (tr_order == null)
                {
                    throw new ExceptionBase("不存在单据");
                }
                if (tr_order.mc_id != 1)
                {
                    throw new ExceptionBase("不存在单据");
                }

                tr_order.msg_hand = "1";
                d.Update(tr_order, "ord_id", "msg_hand");

                db.CommitTran();
            }
            catch (Exception e)
            {
                db.RollBackTran();
                LogHelper.writeLog("Order -> SignRead()", e.ToString(), ord_id);
                throw;
            }
        }

        public string GetPayType(string pay_type)
        {
            switch (pay_type)
            {
                case "0":
                    return "微信支付";
                case "1":
                    return "信用支付";
                case "2":
                    return "支付宝支付";
                default:
                    return "其他支付";
            }
        }
        //生成六位随机码
        public string GetToTheCode()
        {
            Random ro = new Random();
            string str = ro.Next(0, 10).ToString() + ro.Next(0, 10).ToString() + ro.Next(0, 10).ToString() + ro.Next(0, 10).ToString() + ro.Next(0, 10).ToString() + ro.Next(0, 10).ToString();
            return str;
        }

        private string CreateNewSheetNo(DB.IDB db)
        {
            string sql = "select sheet_value from sys_t_sheet_no where sheet_id='SS'";
            object obj = db.ExecuteScalar(sql, null);
            if (obj == null || obj == DBNull.Value)
            {
                return "";
            }
            else
            {
                int index = 0;
                int.TryParse(obj.ToString(), out index);
                index += 1;
                if (index > 9999)
                {
                    index = 0;
                }
                sql = "update sys_t_sheet_no set sheet_value=" + index + " where sheet_id='SS'";
                db.ExecuteScalar(sql, null);
                return "SS00" + System.DateTime.Now.ToString("yyMMdd") + index.ToString().PadLeft(4, '0');
            }
        }

        private bi_t_item_info GetGoods(string item_no, DB.IDB db)
        {
            string str = "select * from bi_t_item_info where item_no='" + item_no + "'";
            return db.ExecuteToModel<bi_t_item_info>(str, null);
        }

        void CreatePosSheet(Model.tr_order ord, List<Model.tr_order_item> items, string sheet_no, DB.IDB db, string oper_id)
        {
            try
            {
                string sql = "select * from sm_t_salesheet where old_no='" + ord.ord_id + "'";
                var tb = db.ExecuteToTable(sql, null);
                if (tb.Rows.Count != 0)
                {
                    throw new Exception("单据" + ord.ord_id + "已经导入后台");
                }
                co_t_order_main o = new co_t_order_main();
                o.sheet_no = sheet_no;
                o.p_sheet_no = "";
                o.sup_no = ord.cus_no;
                o.order_man = string.IsNullOrEmpty(ord.salesman_id) ? "0" : ord.salesman_id;
                o.oper_id = oper_id;
                DateTime oper_date;
                if (ord.reach_time != "" && Conv.ToDateTime(ord.reach_time) > DateTime.Now)
                {
                    oper_date = Conv.ToDateTime(ord.reach_time);
                }
                else
                {
                    oper_date = System.DateTime.Now;
                }
                o.oper_date = oper_date;
                o.valid_date = o.oper_date;
                o.total_amount = ord.enable_amount;
                o.paid_amount = 0;
                o.trans_no = "S";
                o.order_status = "0";
                o.approve_flag = "0";
                o.agree_inhand = "";
                o.coin_code = "RMB";
                o.branch_no = "0001";
                o.sale_way = "A";
                o.memo = ord.cus_remark;
                o.other1 = ord.ord_id;
                o.other2 = "";
                o.cm_branch = "00";
                o.approve_man = "";
                o.approve_date = System.DateTime.MinValue;
                o.num1 = ord.discount_amt;
                o.num2 = 0;
                o.num3 = 0;
                o.ask_no = "";
                o.co_sheetno = "";
                o.if_promote = "";
                db.Insert(o);

                //
                int sheet_sort = 0;
                foreach (Model.tr_order_item item in items)
                {
                    if (item.enable != "1")
                    {
                        continue;
                    }
                    sheet_sort++;
                    var it = new co_t_order_child();
                    it.sheet_no = sheet_no;
                    it.item_no = item.goods_id;
                    bi_t_item_info gd = GetGoods(item.goods_id, db);
                    if (gd != null)
                    {
                        it.unit_no = gd.unit_no;
                    }
                    it.unit_factor = 1;
                    it.in_price = item.price;
                    it.order_qnty = item.qty;
                    it.sub_amount = item.amount;
                    it.real_qty = 0;
                    it.tax_rate = 0;
                    it.pay_percent = 0;
                    it.other1 = ord.ord_id;
                    it.other2 = item.remark;
                    it.other3 = "";
                    it.num1 = 0;
                    it.num2 = 0;
                    it.num3 = 0;
                    if (gd != null)
                    {
                        it.barcode = gd.barcode;
                    }
                    it.sheet_sort = sheet_sort;
                    it.discount = 1;
                    it.voucher_no = "";
                    it.out_qty = 0;
                    it.packqty = 0;
                    it.sgqty = 0;
                    it.ordmemo = "";
                    it.month_sale = 0;

                    db.Insert(it, "flow_id");
                }
                //微信支付或者支付宝支付是已结算单，需要写入预存流水
                if (ord.pay_type == "0" || ord.pay_type == "2")
                {
                    WriteToPosFlow(ord.cus_no, sheet_no, ord.pay_weixin, db);
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Order -> CreatePosSheet()", ex.ToString(), ord.ord_id);
                throw ex;
            }
        }

        string GetNewOrderCode(DB.IDB db)
        {
            string sql = "select sheet_value from sys_t_sheet_no  where sheet_id='HM'";
            object obj = db.ExecuteScalar(sql, null);
            if (obj == null || obj == DBNull.Value)
            {
                return "";
            }
            else
            {
                int index = 0;
                int.TryParse(obj.ToString(), out index);
                index += 1;
                if (index > 9999)
                {
                    index = 0;
                }
                sql = "update sys_t_sheet_no set sheet_value=" + index + " where sheet_id='HM'";
                db.ExecuteScalar(sql, null);
                return "HM00" + System.DateTime.Now.ToString("yyMMdd") + index.ToString().PadLeft(4, '0');
            }

        }

        decimal GetBalance(string cus_no, DB.IDB d)
        {
            decimal balance = 0;
            string sql = "select isnull(sum(a.pay_type*a.sheet_amount - a.pay_type*a.paid_amount - a.pay_type*a.free_money),0) as sum_money ";
            sql += "from rp_t_accout_payrec_flow a ";
            sql += "inner join bi_t_supcust_info b on a.supcust_no = b.supcust_no ";
            sql += "where (a.supcust_flag = 'c') and b.supcust_flag = 'c' and a.supcust_no=@cus_no ";
            sql += "and (a.pay_type * a.sheet_amount <> a.pay_type * (a.paid_amount + a.free_money )) ";

            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@cus_no",cus_no)
            };
            var dt = d.ExecuteToTable(sql, pars);
            if (dt != null && dt.Rows.Count > 0) balance = Conv.ToDecimal(dt.Rows[0]["sum_money"]);
            balance = 0 - balance;
            return balance;
        }

        void WriteToPosFlow(string cus_no, string ori_ord_id, decimal amount, DB.IDB db)
        {
            try
            {
                var balance = GetBalance(cus_no, db);
                var m = new body.rp_t_supcust_fy_master();
                m.sheet_no = GetNewOrderCode(db);
                m.supcust_no = cus_no;
                m.supcust_flag = "C";
                m.pay_type = "-";
                m.pay_date = System.DateTime.Now;
                m.old_no = ori_ord_id;
                m.oper_id = "1001";
                m.oper_date = System.DateTime.Now;
                m.approve_flag = "0";
                m.approve_man = "";
                m.approve_date = System.DateTime.MinValue;
                m.is_payed = "0";
                m.sale_man = "";
                m.branch_no = "0001";
                m.cm_branch = "00";
                m.other1 = "订单支付" + amount;
                m.other2 = "";
                m.other3 = "";
                m.num1 = 0;
                m.num2 = 0;
                m.num3 = balance;
                m.visa_id = "01";
                m.total_amount = 0;
                m.paid_amount = 0;
                m.pay_way = "";
                m.pay_name = "";
                db.Insert(m);
                //
                var item = new body.rp_t_supcust_fy_detail();
                item.sheet_no = m.sheet_no;
                item.kk_no = "113";
                item.kk_cash = amount;
                item.other1 = "";
                item.other2 = "";
                item.other3 = "";
                item.num1 = 0;
                item.num2 = 0;
                item.num3 = 0;
                db.Insert(item);
                //
                m.approve_flag = "1";
                m.approve_man = "1001";
                m.approve_date = System.DateTime.Now;
                db.Update(m, "sheet_no", "approve_flag,approve_man,apprve_date");
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Order -> WriteToPosFlow()", ex.ToString(), cus_no, ori_ord_id, amount.ToString());
                throw ex;
            }
        }

    }
}