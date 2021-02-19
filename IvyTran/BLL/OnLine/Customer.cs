using System;
using System.Data;
using IvyTran.Helper;
using IvyTran.IBLL.OnLine;
using Model;
using bi_identity = DAL.bi_identity;

namespace IvyTran.BLL.OnLine
{
    public class Customer : ICustomer
    {
        DataTable ICustomer.GetNewMsg()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var dtime = DateTime.Now.AddDays(-7);
            string sql = "select cus_no,create_time from tb_customer ";
            sql += "where convert(varchar(10),create_time,120) >= @create_time and status='0' and msg_hand='0' ";
            sql += "order by create_time ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@create_time",dtime.ToString("yyyy-MM-dd"))
            };
            var dt = db.ExecuteToTable(sql, pars);
            return dt;
        }

        void ICustomer.SignRead(string cus_no)
        {
            DB.IDB db = new DB.DBByHandClose(AppSetting.conn);
            try
            {
                string sql = "update tb_customer set msg_hand='1' where cus_no=@cus_no ";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@cus_no",cus_no)
                };
                db.ExecuteScalar(sql, pars);
            }
            catch (Exception e)
            {
                LogHelper.writeLog("Customer -> SignRead()", e.ToString(), cus_no);
                throw;
            }
        }

        void ICustomer.GetFirstNewCus(out customer cus, out int un_read_num)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select cus_no from tb_customer where msg_hand='0' and  status='0' ";
            sql += "and convert(varchar(10),create_time,120) >= @create_time order by create_time ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@create_time",DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"))
            };
            var dt = db.ExecuteToTable(sql, pars);
            un_read_num = dt.Rows.Count;
            var cus_no = "";
            if (dt.Rows.Count != 0)
            {
                cus_no = dt.Rows[0]["cus_no"].ToString();
            }
            sql = "select * from tb_customer where cus_no = @cus_no ";
            pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@cus_no",cus_no)
            };
            cus = db.ExecuteToModel<customer>(sql, pars);
        }

        DataTable ICustomer.GetCusList(string date1, string date2, string status, string salesman_id, string keyword, int page_no, int page_size, out int total)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string temp_sql = "";
            if (status != "-1")
            {
                temp_sql += " and status=@status ";
            }
            if (salesman_id != "")
            {
                temp_sql += " and salesman_id=@salesman_id ";
            }
            string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY create_time desc) AS rowid,* ";
            sql += "FROM tb_customer where convert(varchar(10),create_time,120)<=@date2 and convert(varchar(10),create_time,120)>=@date1 " + temp_sql + ") t ";
            sql += "WHERE t.rowid > " + (page_no - 1) * page_size + " AND t.rowid <= " + page_no * page_size;

            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@date1",date1),
                new System.Data.SqlClient.SqlParameter("@date2",date2),
                new System.Data.SqlClient.SqlParameter("@status",status),
                new System.Data.SqlClient.SqlParameter("@salesman_id",salesman_id),
                new System.Data.SqlClient.SqlParameter("@keyword",keyword)
            };
            var dt = db.ExecuteToTable(sql, pars);

            sql = "SELECT isnull(count(cus_no),0) as total FROM tb_customer ";
            sql += "where convert(varchar(10),create_time,120)<=@date2 and convert(varchar(10),create_time,120)>=@date1 " + temp_sql;
            pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@date1",date1),
                new System.Data.SqlClient.SqlParameter("@date2",date2),
                new System.Data.SqlClient.SqlParameter("@status",status),
                new System.Data.SqlClient.SqlParameter("@salesman_id",salesman_id),
                new System.Data.SqlClient.SqlParameter("@keyword",keyword)
            };
            var dt2 = db.ExecuteToTable(sql, pars);
            total = Conv.ToInt(dt2.Rows[0]["total"]);
            return dt;
        }

        customer ICustomer.GetCusInfo(string cus_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select * from tb_customer where cus_no=@cus_no ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@cus_no",cus_no)
            };
            return db.ExecuteToModel<customer>(sql, pars);
        }

        void ICustomer.Approve(string cus_no, string status, string oper_id, string cus_level, string supcust_group, string remark)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                string new_cus_no = cus_no;
                string sql = "";
                if (status == "1")
                {
                    sql = "select * from tb_customer where cus_no=@cus_no and status='0'";
                    var pars = new System.Data.SqlClient.SqlParameter[]
                    {
                        new System.Data.SqlClient.SqlParameter("@cus_no",cus_no)
                    };
                    var cus = d.ExecuteToModel<customer>(sql, pars);
                    if (cus == null)
                    {
                        throw new Exception("不存在原客户:" + cus_no);
                    }
                    sql = "select * from tb_company where cus_no=@cus_no";
                    pars = new System.Data.SqlClient.SqlParameter[]
                    {
                        new System.Data.SqlClient.SqlParameter("@cus_no",cus_no)
                    };
                    var com = d.ExecuteToModel<tb_company>(sql, pars);
                    //
                    var item = new body.bi_t_supcust_info();
                    item.supcust_no = CreateCusNo(db);
                    item.supcust_flag = "C";
                    item.sup_name = cus.cus_name;
                    item.region_no = "0";
                    item.sup_type = "";
                    item.sup_man = "";
                    item.sup_addr = cus.detail_address;
                    item.zip = "";
                    item.sup_email = cus.cus_email;
                    item.sup_tel = cus.cus_tel;
                    item.sup_fax = "";
                    if (com != null)
                    {
                        item.sup_acct_back = com.bank_name;
                        item.sup_acct_no = com.bank_no;
                        item.sup_tax_no = com.tax_code;
                    }
                    item.display_flag = "1";
                    item.check_out_flag = "0";
                    item.check_out_date = 0;
                    item.check_out_day = 0;
                    item.credit_amt = 0;
                    item.sale_man = cus.salesman_id;
                    item.fp = "";
                    item.other1 = "";
                    item.other2 = "";
                    item.other3 = "";
                    item.num1 = 0;
                    item.num2 = 0;
                    item.num3 = 0;
                    item.data_lock = "";
                    item.init_money = 0;
                    item.cust_level = cus_level;
                    item.sale_no = "A";
                    item.return_type = "";
                    item.month_rate = 0;
                    item.tk_txt = "";
                    item.batch = 0;
                    item.create_time = cus.create_time;
                    item.sup_pyname = "";
                    item.CT_SP_Ctrl = "";
                    item.affiliate_branch = "";
                    item.aff_pur = "";
                    item.aff_settle = "";
                    item.sup_plan = "";
                    item.if_sxsj = "";
                    item.ctrl_branch = "";
                    item.mobile = cus.mobile;
                    item.pwd = cus.cus_pwd;
                    item.open_id = "";
                    item.img_url = cus.img_url;
                    item.login_no = cus.login_no;
                    item.is_branch = cus.is_branch;
                    item.supcust_group = cus.supcust_group;
                    if (com != null)
                    {
                        item.sup_acct_name = com.banck_user;
                        item.invoice_title = com.invoice_title;
                    }
                    d.Insert(item);
                    new_cus_no = item.supcust_no;

                    //审核通过后，写入地址
                    bi_identity id = new bi_identity(db);
                    var ads_id = id.GetId("address");
                    sql = "delete from tr_address where cus_no=@cus_no or cus_no=@new_cus_no ";
                    pars = new System.Data.SqlClient.SqlParameter[]
                    {
                        new System.Data.SqlClient.SqlParameter("@cus_no",cus_no),
                        new System.Data.SqlClient.SqlParameter("@new_cus_no",new_cus_no)
                    };
                    d.ExecuteScalar(sql, pars);
                    sql = "insert into tr_address(ads_id,cus_no,sname,mobile,address,is_default,create_time) ";
                    sql += "values(@ads_id,@cus_no,@sname,@mobile,@address,@is_default,getdate())";
                    pars = new System.Data.SqlClient.SqlParameter[]
                    {
                        new System.Data.SqlClient.SqlParameter("@ads_id",ads_id),
                        new System.Data.SqlClient.SqlParameter("@cus_no",new_cus_no),
                        new System.Data.SqlClient.SqlParameter("@sname",cus.cus_name),
                        new System.Data.SqlClient.SqlParameter("@mobile",cus.mobile),
                        new System.Data.SqlClient.SqlParameter("@address",cus.detail_address),
                        new System.Data.SqlClient.SqlParameter("@is_default",'1')
                    };
                    d.ExecuteScalar(sql, pars);

                    //发短信
                    if (cus.mobile != "")
                    {
                        var to_mobile = cus.mobile;
                        SmsService sms = new SmsService();
                        sms.Send_One_Acc_Msg(to_mobile, to_mobile, to_mobile.Substring(to_mobile.Length - 6), 30);
                    }
                }
                sql = "update tb_customer set cus_no=@new_cus_no,status=@status,cus_level=@cus_level,supcust_group=@supcust_group,remark=@remark,";
                sql += "oper_id=@oper_id,approve_time=getdate(),msg_hand='1' where cus_no=@cus_no";
                var pars2 = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@new_cus_no",new_cus_no),
                    new System.Data.SqlClient.SqlParameter("@status",status),
                    new System.Data.SqlClient.SqlParameter("@cus_level",cus_level),
                    new System.Data.SqlClient.SqlParameter("@supcust_group",supcust_group),
                    new System.Data.SqlClient.SqlParameter("@remark",remark),
                    new System.Data.SqlClient.SqlParameter("@oper_id",oper_id),
                    new System.Data.SqlClient.SqlParameter("@cus_no",cus_no)
                };
                d.ExecuteScalar(sql, pars2);
                db.CommitTran();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeLog("Customer ->Approve()", ex.ToString(), cus_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        void ICustomer.Delete(string cus_no)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                string sql = "select * from v_customer where cus_no=@cus_no and status='1' ";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@cus_no",cus_no)
                };
                var dt = db.ExecuteToTable(sql, pars);
                if (dt.Rows.Count > 0)
                {
                    throw new Exception("客户已审核，不能删除");
                }
                else
                {
                    sql = "delete from bi_t_supcust_info where supcust_no=@cus_no and supcust_flag='C' ";
                    pars = new System.Data.SqlClient.SqlParameter[]
                    {
                        new System.Data.SqlClient.SqlParameter("@cus_no",cus_no)
                    };
                    db.ExecuteScalar(sql, pars);
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Customer ->Delete()", ex.ToString(), cus_no);
                throw ex;
            }
        }

        private string CreateCusNo(DB.IDB d)
        {
            string sql = "select max(supcust_no) from bi_t_supcust_info where supcust_flag='C'" +
                " and isnumeric(supcust_no)=1 and len(supcust_no)=8";
            object obj = d.ExecuteScalar(sql, null);
            if (obj == null)
            {
                return "00000001";
            }
            else
            {
                int index;
                int.TryParse(obj.ToString(), out index);
                index++;
                return index.ToString().PadLeft(8, '0');
            }
        }

        DataTable ICustomer.GetSalesmanList()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select salesman_id,salesman_name from v_salesman where status='1' order by salesman_name ";
            var dt = db.ExecuteToTable(sql, null);
            return dt;
        }

        DataTable ICustomer.GetSalesmanList(string keyword)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select * from v_salesman where status='1' and (salesman_name like '%'+@keyword+'%' or salesman_id like '%'+@keyword+'%' or mobile like '%'+@keyword+'%') ";
            sql += "order by salesman_name ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@keyword",keyword)
            };
            var dt = db.ExecuteToTable(sql, pars);
            return dt;
        }

        void ICustomer.UpdateCustomer(string cus_no, string login_no, string cus_name, string cus_tel, string mobile, string cus_idcard, string cus_area, string contact
            , string detail_addr, string cus_level, string remark, string supcust_group, string salesman_id, string start_date, string end_date, string is_branch)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                string sql = "update tb_customer set cus_name=@cus_name,cus_tel=@cus_tel,mobile=@mobile,contact_address=@contact,detail_address=@detail_addr";
                sql += ",cust_level=@cus_level,update_time=getdate(),is_branch=@is_branch,salesman_id=@salesman_id,login_no=@login_no,supcust_group=@supcust_group ";
                sql += "where cus_no=@cus_no ";

                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@cus_no",cus_no),
                    new System.Data.SqlClient.SqlParameter("@cus_name",cus_name),
                    new System.Data.SqlClient.SqlParameter("@cus_tel",cus_tel),
                    new System.Data.SqlClient.SqlParameter("@mobile",mobile),
                    new System.Data.SqlClient.SqlParameter("@contact",contact),
                    new System.Data.SqlClient.SqlParameter("@detail_addr",detail_addr),
                    new System.Data.SqlClient.SqlParameter("@cus_level",cus_level),
                    new System.Data.SqlClient.SqlParameter("@login_no",login_no),
                    new System.Data.SqlClient.SqlParameter("@salesman_id",salesman_id),
                    new System.Data.SqlClient.SqlParameter("@supcust_group",supcust_group),
                    new System.Data.SqlClient.SqlParameter("@is_branch",is_branch)
                };
                db.ExecuteScalar(sql, pars);

                sql = "update bi_t_supcust_info set sup_name=@cus_name,sup_tel=@cus_tel,mobile=@mobile,sup_man=@contact,sup_addr=@detail_addr";
                sql += ",cust_level=@cus_level,update_time=getdate(),is_branch=@is_branch,sale_man=@salesman_id,login_no=@login_no,supcust_group=@supcust_group ";
                sql += "where supcust_no=@cus_no and supcust_flag='C' ";


                pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@cus_no",cus_no),
                    new System.Data.SqlClient.SqlParameter("@cus_name",cus_name),
                    new System.Data.SqlClient.SqlParameter("@cus_tel",cus_tel),
                    new System.Data.SqlClient.SqlParameter("@mobile",mobile),
                    new System.Data.SqlClient.SqlParameter("@contact",contact),
                    new System.Data.SqlClient.SqlParameter("@detail_addr",detail_addr),
                    new System.Data.SqlClient.SqlParameter("@cus_level",cus_level),
                    new System.Data.SqlClient.SqlParameter("@login_no",login_no),
                    new System.Data.SqlClient.SqlParameter("@salesman_id",salesman_id),
                    new System.Data.SqlClient.SqlParameter("@supcust_group",supcust_group),
                    new System.Data.SqlClient.SqlParameter("@is_branch",is_branch)
                };
                db.ExecuteScalar(sql, pars);

            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Customer.UpdateCustomer()", ex.ToString(), cus_name, cus_no);
                throw ex;
            }
        }

        void ICustomer.AddCustomer(string login_no, string cus_pwd, string cus_name, string cus_tel, string mobile, string cus_idcard, string cus_area, string contact
            , string detail_addr, string cus_level, string remark, string supcust_group, string salesman_id, string start_date, string end_date, string is_branch)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                string sql = "";
                System.Data.SqlClient.SqlParameter[] pars;
                if (login_no != "")
                {
                    sql = "select 1 from bi_t_supcust_info where supcust_flag='C' and other3=@login_no ";
                    pars = new System.Data.SqlClient.SqlParameter[]
                    {
                        new System.Data.SqlClient.SqlParameter("@login_no",login_no)
                    };
                    var dt = d.ExecuteToTable(sql, pars);
                    if (dt.Rows.Count > 0)
                    {
                        throw new Exception("客户登录账号已被注册:" + login_no);
                    }
                }
                if (mobile != "")
                {
                    sql = "select 1 from bi_t_supcust_info where supcust_flag='C' and mobile=@mobile ";
                    pars = new System.Data.SqlClient.SqlParameter[]
                    {
                        new System.Data.SqlClient.SqlParameter("@mobile",mobile)
                    };
                    var dt = d.ExecuteToTable(sql, pars);
                    if (dt.Rows.Count > 0)
                    {
                        throw new Exception("客户手机号码已被注册:" + mobile);
                    }
                }
                var item = new body.bi_t_supcust_info();
                item.supcust_no = CreateCusNo(db);
                item.supcust_flag = "C";
                item.sup_name = cus_name;
                item.region_no = "0";
                item.sup_type = "";
                item.sup_man = contact;
                item.sup_addr = detail_addr;
                item.zip = "";
                item.sup_email = "";
                item.sup_tel = cus_tel;
                item.sup_fax = "";
                item.display_flag = "1";
                item.check_out_flag = "0";
                item.check_out_date = 0;
                item.check_out_day = 0;
                item.credit_amt = 0;
                item.sale_man = salesman_id;
                item.fp = "";
                item.other1 = "";
                item.other2 = "";
                item.other3 = "";
                item.num1 = 0;
                item.num2 = 0;
                item.num3 = 0;
                item.data_lock = "";
                item.init_money = 0;
                item.cust_level = cus_level;
                item.sale_no = "A";
                item.return_type = "";
                item.month_rate = 0;
                item.tk_txt = "";
                item.batch = 0;
                item.create_time = DateTime.Now;
                item.sup_pyname = "";
                item.CT_SP_Ctrl = "";
                item.affiliate_branch = "";
                item.aff_pur = "";
                item.aff_settle = "";
                item.sup_plan = "";
                item.if_sxsj = "";
                item.ctrl_branch = "";
                item.mobile = mobile;
                item.pwd = cus_pwd;
                item.open_id = "";
                item.img_url = "";
                item.is_branch = is_branch;
                item.login_no = login_no;
                item.supcust_group = supcust_group;
                d.Insert(item);

                //审核通过后，写入地址
                bi_identity id = new bi_identity(db);
                var ads_id = id.GetId("address");
                sql = "delete from tr_address where cus_no=@cus_no or cus_no=@new_cus_no ";
                pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@cus_no",mobile),
                    new System.Data.SqlClient.SqlParameter("@new_cus_no",item.supcust_no)
                };
                d.ExecuteScalar(sql, pars);

                sql = "insert into tr_address(ads_id,cus_no,sname,mobile,address,is_default,create_time) ";
                sql += "values(@ads_id,@cus_no,@sname,@mobile,@address,@is_default,getdate())";
                pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@ads_id",ads_id),
                    new System.Data.SqlClient.SqlParameter("@cus_no",item.supcust_no),
                    new System.Data.SqlClient.SqlParameter("@sname",cus_name),
                    new System.Data.SqlClient.SqlParameter("@mobile",mobile),
                    new System.Data.SqlClient.SqlParameter("@address",detail_addr),
                    new System.Data.SqlClient.SqlParameter("@is_default",'1')
                };
                d.ExecuteScalar(sql, pars);

                db.CommitTran();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeLog("Customer ->AddCustomer()", ex.ToString(), login_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }


        void ICustomer.GetCusBalance(string cus_no, out decimal balance, out decimal credit_amt)
        {
            balance = 0;
            credit_amt = 0;
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select isnull(sum(a.pay_type*a.sheet_amount - a.pay_type*a.paid_amount - a.pay_type*a.free_money),0) as sum_money ";
            sql += "from rp_t_accout_payrec_flow a ";
            sql += "inner join bi_t_supcust_info b on a.supcust_no = b.supcust_no ";
            sql += "where (a.supcust_flag = 'c') and b.supcust_flag = 'c' and a.supcust_no=@cus_no ";
            sql += "and (a.pay_type * a.sheet_amount <> a.pay_type * (a.paid_amount + a.free_money )) ";

            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@cus_no",cus_no)
            };
            var dt = db.ExecuteToTable(sql, pars);
            if (dt != null && dt.Rows.Count > 0) balance = Conv.ToDecimal(dt.Rows[0]["sum_money"]);
            balance = 0 - balance;

            sql = "select credit_amt from bi_t_supcust_info where supcust_flag = 'c' and supcust_no=@cus_no ";
            pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@cus_no",cus_no)
            };
            dt = db.ExecuteToTable(sql, pars);
            if (dt != null && dt.Rows.Count > 0) credit_amt = Conv.ToDecimal(dt.Rows[0]["credit_amt"]);
        }

        DataTable ICustomer.GetCusGroupList()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select supcust_groupno,supcust_groupname from bi_t_supcust_group where supcust_flag='1' order by supcust_groupno ";
            var dt = db.ExecuteToTable(sql, null);
            return dt;
        }

        DataTable ICustomer.SearchCusBalanceList(string keyword, string supcust_group)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var condition_sql = "";
            if (keyword != "") condition_sql += " and (b.supcust_no like '%'+@keyword+'%' or b.sup_name like '%'+@keyword+'%' or b.login_no like '%'+@keyword+'%') ";
            if (supcust_group != "") condition_sql += " and b.supcust_group=@supcust_group ";
            string sql = "select b.supcust_no,b.sup_name,isnull(max(b.credit_amt),0) credit_amt";
            sql += ",-1*isnull(sum(a.pay_type*a.sheet_amount - a.pay_type*a.paid_amount - a.pay_type*a.free_money),0) as sum_money ";
            sql += ",isnull(max(b.credit_amt),0) - 1*isnull(sum(a.pay_type*a.sheet_amount - a.pay_type*a.paid_amount - a.pay_type*a.free_money),0) as balance_amt ";
            sql += "from bi_t_supcust_info b ";
            sql += "left join (select * from rp_t_accout_payrec_flow where supcust_flag='c' ";
            sql += "and (pay_type * sheet_amount <> pay_type * (paid_amount + free_money ))) a on a.supcust_no=b.supcust_no ";
            sql += "where b.supcust_flag = 'C' " + condition_sql;
            sql += "group by b.supcust_no,b.sup_name ";

            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@keyword",keyword),
                new System.Data.SqlClient.SqlParameter("@supcust_group",supcust_group)
            };
            var dt = db.ExecuteToTable(sql, pars);
            return dt;
        }
    }
}