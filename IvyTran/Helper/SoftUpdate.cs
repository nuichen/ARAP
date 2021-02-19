using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using DB;
using System.Runtime.InteropServices;
using IvyTran.BLL.ERP;
using IvyTran.IBLL.ERP;

namespace IvyTran.Helper
{
    /// <summary>
    /// 软件更新
    /// </summary>
    public class SoftUpdate
    {
        private SoftUpdate() { }
        public static SoftUpdate soft_update = new SoftUpdate();

        public static void Upadte()
        {
            try
            {
                {
                    //更新中间件
                    IUpdate bll = new UpdateBLL();
                    bll.AutoUpdate();
                }
                {
                    //更新数据库
                    ISys bll = new Sys();
                    int ser_ver = Conv.ToInt(bll.Read("ser_ver").Split('_')[1]);
                    int ver = Conv.ToInt(AppSetting.versions.Split('_')[1]);
                    if (ser_ver < ver)
                    {
                        //需要更新
                        Type t = soft_update.GetType();
                        MethodInfo[] infos = t.GetMethods();
                        foreach (MethodInfo info in infos)
                        {
                            if (info.Name.IndexOf("_") > -1)
                            {
                                int time = Conv.ToInt(info.Name.Split('_')[1]);

                                if (time > ser_ver)
                                {
                                    info.Invoke(soft_update, null);
                                }
                            }
                        }
                    }
                    else if (ser_ver > ver)
                    {
                        //需要更新程序
                        LogHelper.writeLog("中间件版本过低，请更新");
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("更新失败" + Environment.NewLine + ex.ToString());
            }

        }

        public void Update_20181219()
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                string sql = @"create table 测试升级(
	id varchar(20)
)";

                d.ExecuteScalar(sql, null);

                d.ExecuteScalar(" update sys_t_system set sys_var_value='" + AppSetting.versions + "'   where sys_var_id='app_ver' ", null);
                d.ExecuteScalar(" update sys_t_system set sys_var_value='" + AppSetting.versions + "'   where sys_var_id='ser_ver' ", null);
                d.ExecuteScalar(" update sys_t_system set sys_var_value='" + AppSetting.versions + "'   where sys_var_id='db_ver' ", null);

                db.CommitTran();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        public void Update_20181220()
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                d.ExecuteScalar(" update sys_t_system set sys_var_value='" + AppSetting.versions + "'   where sys_var_id='app_ver' ", null);
                d.ExecuteScalar(" update sys_t_system set sys_var_value='" + AppSetting.versions + "'   where sys_var_id='ser_ver' ", null);
                d.ExecuteScalar(" update sys_t_system set sys_var_value='" + AppSetting.versions + "'   where sys_var_id='db_ver' ", null);

                db.CommitTran();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
    }


}
