using System;
using System.Collections.Generic;
using System.Data;
using DB;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;

namespace IvyTran.BLL.ERP
{
    public class PrintBLL : IPrint
    {

        public DataTable GetAll(sys_t_print_style style)
        {
            IDB d = new DBByAutoClose(AppSetting.conn);
            string sql = @"select p.*,
case when p.is_base='1' then  '系统'
    else (p.oper_id+'/'+o.oper_name)
end 用户
from sys_t_print_style p
left join sa_t_operator_i o on o.oper_id=p.oper_id
where 1=1 ";

            if (!string.IsNullOrEmpty(style.report_id))
                sql += " and report_id='" + style.report_id + "' ";
            if (!string.IsNullOrEmpty(style.oper_id))
                sql += " and (p.oper_id='" + style.oper_id + "' or p.is_share='1' or p.is_base='1')";

            DataTable tb = d.ExecuteToTable(sql, null);
            return tb;
        }

        public void Add(sys_t_print_style style)
        {
            IDB d = new DBByAutoClose(AppSetting.conn);
            var num = Conv.ToInt(d.ExecuteScalar(@"SELECT ISNULL(MAX(CONVERT(INT,style_data) ),0) FROM dbo.sys_t_print_style", null));

            style.style_data = (++num).ToString();
            d.Insert(style);
        }

        public void Update(sys_t_print_style style)
        {
            IDB d = new DBByAutoClose(AppSetting.conn);
            string sql = "select * from sys_t_print_style where style_id='" + style.style_id + "'";
            var print_style = d.ExecuteToModel<sys_t_print_style>(sql, null);
            if (print_style == null)
                throw new Exception("不存在样式");
            if (print_style.update_time > style.update_time)
                throw new Exception("样式已被修改");

            style.update_time = DateTime.Now;
            d.Update(style, "style_id");
        }

        public void Del(sys_t_print_style style)
        {
            IDB d = new DBByAutoClose(AppSetting.conn);
            string sql = "select * from sys_t_print_style where style_id='" + style.style_id + "'";
            var print_style = d.ExecuteToModel<sys_t_print_style>(sql, null);
            if (print_style == null)
                throw new Exception("不存在样式");
            if (print_style.update_time > style.update_time)
                throw new Exception("样式已被修改");

            d.ExecuteScalar("delete from sys_t_print_style where style_id='" + style.style_id + "'", null);
        }

        public DataTable GetPrintStyleDefault(sys_t_print_style_default style)
        {
            IDB d = new DBByAutoClose(AppSetting.conn);
            string sql = "select * from sys_t_print_style_default where 1=1 ";

            if (!string.IsNullOrEmpty(style.report_id))
                sql += " and report_id='" + style.report_id + "' ";
            if (!string.IsNullOrEmpty(style.oper_id))
                sql += " and oper_id='" + style.oper_id + "' ";

            DataTable tb = d.ExecuteToTable(sql, null);
            return tb;
        }

        public void AddDefault(sys_t_print_style_default style)
        {
            IDB d = new DBByAutoClose(AppSetting.conn);
            string sql = "select * from sys_t_print_style_default where report_id='" + style.report_id +
                         "' and oper_id='" + style.oper_id + "' ";
            var print_style = d.ExecuteToModel<sys_t_print_style_default>(sql, null);
            if (print_style != null)
            {
                UpdateDefault(style);
            }
            else
            {
                d.Insert(style);
            }
        }

        public void UpdateDefault(sys_t_print_style_default style)
        {
            IDB d = new DBByAutoClose(AppSetting.conn);
            string sql = "select * from sys_t_print_style_default where report_id='" + style.report_id +
                         "' and oper_id='" + style.oper_id + "' ";
            var print_style = d.ExecuteToModel<sys_t_print_style_default>(sql, null);
            if (print_style == null)
                throw new Exception("不存在样式");

            d.Update(style, "oper_id,report_id");
        }

        public void DelDefault(sys_t_print_style_default style)
        {
            IDB d = new DBByAutoClose(AppSetting.conn);
            string sql = "select * from sys_t_print_style_default where report_id='" + style.report_id +
                         "' and oper_id='" + style.oper_id + "' ";
            var print_style = d.ExecuteToModel<sys_t_print_style_default>(sql, null);
            if (print_style == null)
                throw new Exception("不存在样式");

            d.ExecuteScalar("delete from sys_t_print_style_default where style_id='" + style.style_id + "' and oper_id='" + style.oper_id + "'", null);
        }

        public DataTable GetPrintStyleData(string style_data)
        {
            IDB d = new DBByAutoClose(AppSetting.conn);
            string sql = "select * from sys_t_print_style_data where 1=1 ";
            if (!string.IsNullOrEmpty(style_data))
            {
                sql += " and  style_data='" + style_data + "' ";
            }
            var tb = d.ExecuteToTable(sql, null);
            return tb;
        }

        public void UpdateStyleData(List<sys_t_print_style_data> lis)
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                foreach (sys_t_print_style_data data in lis)
                {
                    data.update_time = DateTime.Now;
                    sys_t_print_style_data old_data = d.ExecuteToModel<sys_t_print_style_data>(
                        "select * from sys_t_print_style_data where style_data='" + data.style_data + "'", null);
                    if (old_data == null || string.IsNullOrEmpty(old_data.style_data))
                    {
                        d.Insert(data);
                    }
                    else
                    {
                        if (old_data.update_time < data.update_time)
                        {
                            d.Update(data, "style_data");
                        }
                    }
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
        #region 客户打印信息配对

        public DataTable GetStyleGroupInfo(string type_name)
        {
            string sql = $"SELECT DISTINCT ps.style_id, ps.report_id, ps.oper_id, ps.style_name, ot.type_name FROM sys_t_print_style psd LEFT JOIN sys_t_oper_type ot ON psd.report_id =ot.type_id LEFT JOIN sys_t_print_style ps ON ps.report_id =psd.report_id";
            if (type_name != string.Empty)
            {
                sql += $" WHERE type_name='{type_name}'";
            }
            IDB d = new DBByAutoClose(AppSetting.conn);

            DataTable styleInfo = d.ExecuteToTable(sql, null);
            return styleInfo;

        }

        public void JoinCustomerAndStyle(sys_t_print_style_default style_Default)
        {
            IDB d = new DBByAutoClose(AppSetting.conn);

            if (string.IsNullOrEmpty(style_Default.cust_no))
            {
                throw new Exception("客户不能为空");
            }
            string sql = $"SELECT * FROM sys_t_print_style_default where cust_no = '{style_Default.cust_no}' and report_id = '{style_Default.report_id}'";
            DataTable dataRows = d.ExecuteToTable(sql, null);
            if (dataRows.Rows.Count > 0)
            {
                d.Update(style_Default, "oper_id,cust_no,report_id");
            }
            else
            {
                d.Insert(style_Default);
            }
        }

        public DataTable GetStyleTypeInfo()
        {
            IDB d = new DBByAutoClose(AppSetting.conn);
            string sql = "SELECT DISTINCT ot.type_name, psd.report_id FROM sys_t_print_style psd LEFT JOIN sys_t_oper_type ot on ot.type_id = psd.report_id";

            DataTable typeInfo = d.ExecuteToTable(sql, null);
            return typeInfo;
        }

        public sys_t_print_style GetStyleById(sys_t_print_style style)
        {
            IDB d = new DBByAutoClose(AppSetting.conn);
            string sql = @"select * 
from sys_t_print_style 
where 1=1";

            if (!string.IsNullOrEmpty(style.report_id))
                sql += " and report_id='" + style.report_id + "' ";
            if (!string.IsNullOrEmpty(style.oper_id))
                sql += " and (p.oper_id='" + style.oper_id + "' or p.is_share='1' or p.is_base='1')";
            if (!string.IsNullOrEmpty(style.style_id))
                sql += " and style_id='" + style.style_id + "' ";
            style = d.ExecuteToModel<sys_t_print_style>(sql, null);
            return style;
        }


        #endregion
    }
}
