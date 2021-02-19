using System;
using System.Data;
using IvyTran.IBLL.ERP;

namespace IvyTran.BLL.ERP
{
    public  class Oper : IOper
    {
        void IOper.Login(string oper_id, string pwd, out string oper_name, out string oper_type)
        {
            string sql = "select * from sa_t_operator_i where oper_id='" + oper_id + "' and oper_status='1'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count == 0)
            {
                throw new Exception("不存在用户" + oper_id);
            }
            else
            {
                var row = tb.Rows[0];
                string db_pwd = row["oper_pw"].ToString();
                if (db_pwd == "")
                {
                    if (pwd == "")
                    {
                        Helper.SessionHelper.oper_id = oper_id;
                        oper_name = row["oper_name"].ToString();
                        oper_type = row["oper_type"].ToString();
                    }
                    else
                    {
                        throw new Exception("密码不正确");
                    }
                }
                else
                {
                    Helper.sec sec = new Helper.sec();
                    /*          db_pwd = IvyTran.Helper.sec.des(db_pwd);*/
                    pwd = Helper.Conv.ToMD5(pwd).ToUpper();
                    if (pwd == db_pwd)
                    {
                        Helper.SessionHelper.oper_id = oper_id;
                        oper_name = row["oper_name"].ToString();
                        oper_type = row["oper_type"].ToString();
                    }
                    else
                    {
                        throw new Exception("密码不正确");
                    }
                }
            }
        }
        void IOper.ChangePWD(string oper_id, string old_pwd, string new_pwd)
        {
            string sql = "select * from sa_t_operator_i where oper_id='" + oper_id + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count == 0)
            {
                throw new Exception("不存在用户" + oper_id);
            }
            else
            {
                var row = tb.Rows[0];
                string db_pwd = row["oper_pw"].ToString();
                if (db_pwd == "")
                {
                    if (old_pwd == "")
                    {
                        Helper.sec sec = new Helper.sec();
                        sql = "update sa_t_operator_i set oper_pw='" + IvyTran.Helper.sec.des(new_pwd) + "' where oper_id='" + oper_id + "'";
                        db.ExecuteScalar(sql, null);
                    }
                    else
                    {
                        throw new Exception("旧密码不正确");
                    }
                }
                else
                {
                    Helper.sec sec = new Helper.sec();
                    db_pwd = IvyTran.Helper.sec.des(db_pwd);
                    if (old_pwd == db_pwd)
                    {

                        sql = "update sa_t_operator_i set oper_pw='" + IvyTran.Helper.sec.des(new_pwd) + "' where oper_id='" + oper_id + "'";
                        db.ExecuteScalar(sql, null);
                    }
                    else
                    {
                        throw new Exception("旧密码不正确");
                    }
                }
            }
        }
        void IOper.Add(Model.sa_t_operator_i oper)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select count(*) from sa_t_operator_i where oper_id='" + oper.oper_id + "'";
            var count = Convert.ToInt32(db.ExecuteScalar(sql, null));
            if (count > 0)
            {
                throw new Exception("已存在用户！");
            }
            db.Insert(oper);
        }
        string IOper.GetMaxCode()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select isnull(max(oper_id) ,0)+1 from sa_t_operator_i ";
            var code = db.ExecuteScalar(sql, null).ToString();
            return code;
        }
        public System.Data.DataTable GetOperType()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select * from sa_t_oper_type ";
            DataTable tb = db.ExecuteToTable(sql, null);
            return tb;
        }
        string GetMaxTypeMode()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = " select isnull(max(oper_type) ,0)+1 from sa_t_oper_type ";
            var code = db.ExecuteScalar(sql, null).ToString();
            return code;
        }
        public void AddOperType(Model.sa_t_oper_type type)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            type.oper_type = GetMaxTypeMode();
            db.Insert(type);
        }
        public void ChangeOperType(Model.sa_t_oper_type type)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var oType = db.ExecuteToModel<Model.sa_t_oper_type>("select * from sa_t_oper_type where oper_type = '" + type.oper_type + "'", null);
            if (oType != null)
            {
                if (oType.update_time > type.update_time)
                {
                    throw new Exception("操作员组[" + type.type_name + "]已被修改");
                }
                db.Update(type, "oper_type");
            }
            else
            {
                throw new Exception("操作员组[" + type.type_name + "]不存在");
            }
        }
        public void DelOperType(Model.sa_t_oper_type type)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var oType = db.ExecuteToModel<Model.sa_t_oper_type>("select * from sa_t_oper_type where oper_type = '" + type.oper_type + "'", null);
            if (oType != null)
            {
                if (oType.update_time > type.update_time)
                {
                    throw new Exception("操作员组[" + type.type_name + "]已被修改");
                }

                var count = Convert.ToInt32(db.ExecuteScalar("select count(*) from sa_t_operator_i where oper_type='" + type.oper_type + "'", null));
                if (count > 0)
                {
                    throw new Exception("组[" + type.type_name + "]存在操作员，不能删除");
                }

                db.ExecuteScalar("delete sa_t_oper_type where oper_type='" + type.oper_type + "' ", null);
            }
            else
            {
                throw new Exception("操作员组[" + type.type_name + "]不存在");
            }
        }
        public DataTable GetOpers()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select a.*,b.type_name from sa_t_operator_i a left join sa_t_oper_type b on a.oper_type=b.oper_type";
            DataTable tb = db.ExecuteToTable(sql, null);
            return tb;
        }
        void IOper.Upload(Model.sa_t_operator_i oper)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            db.Update(oper, "oper_id");
        }
        void IOper.Del(string oper_id)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "";
            //
            sql = "select  top 1 * from pm_t_flow_main where oper_id='" + oper_id + "' ";
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("操作员已被调价单引用，不能删除!");
            }
            //
            sql = "select top 1  * from sm_t_salesheet where oper_id='" + oper_id + "' ";
            tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("操作员已被批发销售单引用，不能删除!");
            }
            //
            sql = "select  top 1 * from ic_t_inout_store_master where oper_id='" + oper_id + "' ";
            tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("操作员已被出入库单引用，不能删除!");
            }
            //
            sql = "select top 1  * from ic_t_check_master where oper_id='" + oper_id + "' ";
            tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("操作员已被盘点单引用，不能删除!");
            }
            //
            sql = "delete from sa_t_operator_i where oper_id='" + oper_id + "'";
            db.ExecuteScalar(sql, null);
        }
        public void ResetPWD(string oper_id, string new_pwd)
        {
            Helper.sec sec = new Helper.sec();
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "update sa_t_operator_i set oper_pw='" + IvyTran.Helper.sec.des(new_pwd) + "' where oper_id='" + oper_id + "'";
            db.ExecuteScalar(sql, null);

        }

    }

}
