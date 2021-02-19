using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using IvyTran.IBLL.ERP;

namespace IvyTran.BLL.ERP
{
    public class MobileBLL : IMobileBLL
    {
        DataTable IMobileBLL.SearchMobileOperList(string oper_cls, string func_id, string keyword, string is_show_stop)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var condition_sql = "";
            if (oper_cls != "")
            {
                condition_sql += "and oper_cls=@oper_cls ";
            }
            if (func_id != "")
            {
                func_id = "," + func_id + ",";
                condition_sql += "and ','+oper_auth+',' like '%'+@func_id+'%' ";
            }
            if (keyword != "")
            {
                condition_sql += "and (oper_id like '%'+@keyword+'%' or oper_name like '%'+@keyword+'%') ";
            }
            if (is_show_stop == "0")
            {
                condition_sql += "and display_flag='1' ";
            }
            string sql = "select * from sa_t_mobile_oper where 1=1 " + condition_sql + " order by oper_id ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@func_id", func_id),
                new System.Data.SqlClient.SqlParameter("@keyword", keyword),
                new System.Data.SqlClient.SqlParameter("@oper_cls", oper_cls)
            };
            DataTable tb = db.ExecuteToTable(sql, pars);
            tb.Columns.Add("oper_auth_str");
            var dic = GetFuncDic();
            foreach (DataRow dr in tb.Rows)
            {
                if (dr["oper_auth"].ToString() != "")
                {
                    var arr = dr["oper_auth"].ToString().Split(',');
                    for (int i = 0; i < arr.Length; i++)
                    {
                        var tmp_str = "";
                        if (dic.TryGetValue(arr[i], out tmp_str)) dr["oper_auth_str"] = dr["oper_auth_str"].ToString() + "[" + tmp_str + "]";
                    }
                }
            }
            return tb;
        }

        string GetMaxCode(string oper_cls)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var condition_sql = "";
            var tmp_oper_id = "1000";
            if (!string.IsNullOrEmpty(oper_cls))
            {
                condition_sql += "and oper_cls=@oper_cls ";
                tmp_oper_id = oper_cls.PadRight(4, '0');
            }
            string sql = "select isnull(max(oper_id) ," + tmp_oper_id + ")+1 from sa_t_mobile_oper where  1=1 " + condition_sql;
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@oper_cls", oper_cls)
            };
            var code = db.ExecuteScalar(sql, pars).ToString();
            return code;
        }

        DataTable IMobileBLL.GetFuncList()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select * from sa_t_mobile_func ";

            var dt = db.ExecuteToTable(sql, null);

            return dt;
        }
        DataTable IMobileBLL.GetOperCls()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select * from sa_t_mobile_opercls where display_flag='1' ";

            var dt = db.ExecuteToTable(sql, null);

            return dt;
        }
        Dictionary<string, string> GetFuncDic()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select * from sa_t_mobile_func ";

            var dt = db.ExecuteToTable(sql, null);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (DataRow dr in dt.Rows)
            {
                dic.Add(dr["func_id"].ToString(), dr["func_name"].ToString());
            }
            return dic;
        }
        DataTable IMobileBLL.GetSupcustList(string supcust_flag, string keyword)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var condition_sql = "";
            if (supcust_flag != "")
            {
                condition_sql += " and supcust_flag=@supcust_flag ";
            }
            if (keyword != "")
            {
                condition_sql += " and (supcust_no like '%'+@keyword+'%' or sup_name like '%'+@keyword+'%') ";
            }
            string sql = "select * from bi_t_supcust_info where 1=1 " + condition_sql;
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@supcust_flag", supcust_flag),
                new System.Data.SqlClient.SqlParameter("@keyword", keyword)
            };
            var dt = db.ExecuteToTable(sql, pars);

            return dt;
        }
        DataTable IMobileBLL.GetItemCls()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select * from bi_t_item_cls where len(item_clsno)=2 ";

            var dt = db.ExecuteToTable(sql, null);

            return dt;
        }
        string IMobileBLL.SaveMobileOper(Model.SysModel.sa_t_mobile_oper item)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                if (string.IsNullOrEmpty(item.oper_id))
                {
                    if (!string.IsNullOrEmpty(item.login_no))
                    {
                        string sql = "select 1 from sa_t_mobile_oper where login_no=@login_no ";
                        var pars = new System.Data.SqlClient.SqlParameter[]
                        {
                            new System.Data.SqlClient.SqlParameter("@login_no", item.login_no)
                        };
                        var dt = db.ExecuteToTable(sql, pars);
                        if (dt.Rows.Count > 0)
                        {
                            throw new Exception("登录账号已存在");
                        }
                    }
                    item.oper_id = GetMaxCode(item.oper_cls);
                    item.create_time = DateTime.Now;
                    item.update_time = DateTime.Now;
                    db.Insert(item);
                }
                else
                {
                    if (!string.IsNullOrEmpty(item.login_no))
                    {
                        string sql = "select 1 from sa_t_mobile_oper where login_no=@login_no and oper_id<>@oper_id ";
                        var pars = new System.Data.SqlClient.SqlParameter[]
                        {
                            new System.Data.SqlClient.SqlParameter("@login_no", item.login_no),
                            new System.Data.SqlClient.SqlParameter("@oper_id", item.oper_id)
                        };
                        var dt = db.ExecuteToTable(sql, pars);
                        if (dt.Rows.Count > 0)
                        {
                            throw new Exception("登录账号已存在");
                        }
                    }
                    item.update_time = DateTime.Now;
                    db.Update(item, "oper_id");
                }
                return item.oper_id;
            }
            catch (Exception ex)
            {
                Helper.LogHelper.writeLog("MobileBLL.SaveMobileOper()", ex.ToString(), item.oper_id);
                throw ex;
            }
        }
        void IMobileBLL.SaveMobileOperDataMain(string oper_id, List<Model.SysModel.sa_t_mobile_data_main> lst)
        {
            var d = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB db = d;
            try
            {
                d.Open();
                d.BeginTran();
                string sql = "delete from sa_t_mobile_data_detail where flow_id in(select flow_id from sa_t_mobile_data_main where oper_id=@oper_id) ";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@oper_id", oper_id)
                };
                db.ExecuteScalar(sql, pars);

                sql = "delete from sa_t_mobile_data_main where oper_id=@oper_id ";
                pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@oper_id", oper_id)
                };
                db.ExecuteScalar(sql, pars);
                sql = "insert into sa_t_mobile_data_main(flow_id, oper_id, func_id,supcust_no,supcust_flag, create_time) ";
                sql += "select isnull(max(flow_id),0)+1,@oper_id,@func_id,@supcust_no,@supcust_flag,getdate() from sa_t_mobile_data_main ";
                foreach (var itm in lst)
                {
                    pars = new System.Data.SqlClient.SqlParameter[]
                    {
                        new System.Data.SqlClient.SqlParameter("@oper_id", oper_id),
                        new System.Data.SqlClient.SqlParameter("@func_id", itm.func_id),
                        new System.Data.SqlClient.SqlParameter("@supcust_no", itm.supcust_no),
                        new System.Data.SqlClient.SqlParameter("@supcust_flag", itm.supcust_flag)
                     };
                    db.ExecuteScalar(sql, pars);
                }

                d.CommitTran();
            }
            catch (Exception ex)
            {
                d.RollBackTran();
                Helper.LogHelper.writeLog("MobileBLL.SaveMobileOperDataMain()", ex.ToString(), oper_id);
                throw ex;
            }
            finally
            {
                d.Close();
            }
        }
        void IMobileBLL.SaveMobileOperDataDetail(string flow_id, string item_clsnos)
        {
            var d = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB db = d;
            try
            {
                d.Open();
                d.BeginTran();
                string sql = "delete from sa_t_mobile_data_detail where flow_id=@flow_id ";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@flow_id", flow_id)
                };
                db.ExecuteScalar(sql, pars);

                sql = "insert into sa_t_mobile_data_detail(flow_id, item_clsno) values(@flow_id, @item_clsno) ";
                var arr = item_clsnos.Split(',');
                for (int i = 0; i < arr.Length; i++)
                {
                    pars = new System.Data.SqlClient.SqlParameter[]
                    {
                        new System.Data.SqlClient.SqlParameter("@flow_id", flow_id),
                        new System.Data.SqlClient.SqlParameter("@item_clsno", arr[i])
                     };
                    db.ExecuteScalar(sql, pars);
                }

                d.CommitTran();
            }
            catch (Exception ex)
            {
                d.RollBackTran();
                Helper.LogHelper.writeLog("MobileBLL.SaveMobileOperDataDetail()", ex.ToString(), flow_id, item_clsnos);
                throw ex;
            }
            finally
            {
                d.Close();
            }
        }
        DataTable IMobileBLL.GetOperDataMain(string oper_id)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var sql = "select a.*,b.sup_name from sa_t_mobile_data_main a ";
            sql += "left join bi_t_supcust_info b on a.supcust_no=b.supcust_no and a.supcust_flag=b.supcust_flag ";
            sql += "where a.oper_id=@oper_id ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@oper_id", oper_id)
            };
            var dt = db.ExecuteToTable(sql, pars);
            return dt;
        }
        DataTable IMobileBLL.GetOperDataDetail(string flow_id)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var sql = "select * from sa_t_mobile_data_detail where flow_id=@flow_id ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                    new System.Data.SqlClient.SqlParameter("@flow_id", flow_id)
            };
            var dt = db.ExecuteToTable(sql, pars);
            return dt;
        }

        void IMobileBLL.DeleteMobileOper(string oper_id)
        {
            var d = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB db = d;
            try
            {
                d.Open();
                d.BeginTran();
                string sql = "delete from sa_t_mobile_data_detail where flow_id in(select flow_id from sa_t_mobile_data_main where oper_id=@oper_id) ";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@oper_id", oper_id)
                };
                db.ExecuteScalar(sql, pars);

                sql = "delete from sa_t_mobile_data_main where oper_id=@oper_id ";
                pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@oper_id", oper_id)
                };
                db.ExecuteScalar(sql, pars);

                sql = "delete from sa_t_mobile_oper where oper_id=@oper_id ";
                pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@oper_id", oper_id)
                };
                db.ExecuteScalar(sql, pars);
                d.CommitTran();
            }
            catch (Exception ex)
            {
                d.RollBackTran();
                Helper.LogHelper.writeLog("MobileBLL.DeleteMobileOper()", ex.ToString(), oper_id);
                throw ex;
            }
            finally
            {
                d.Close();
            }
        }
    }
}