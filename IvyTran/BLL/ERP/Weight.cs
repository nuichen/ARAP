using System;
using System.Collections.Generic;
using System.Data;
using IvyTran.IBLL.ERP;
using Model;

namespace IvyTran.BLL.ERP
{
    public class Weight:IWeight
    {
        
        DataTable IWeight.GetCoOrderMain(string last_req)
        {
            if(last_req== "0001-01-01 00:00:00")
            {
                last_req = "1973-01-01 00:00:00";
            }
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            last_req = last_req.Substring(0, 10);
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select sheet_no,sup_no,convert(varchar(10),valid_date,120) valid_date,oper_date,total_amount,trans_no,approve_flag,approve_date,memo,num1 from co_t_order_main  where convert(varchar(10),valid_date,120)>=@valid_date " +
                "and oper_date>=@last_req and approve_flag='1' and trans_no='S' ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@valid_date",date),
                new System.Data.SqlClient.SqlParameter("@last_req",last_req)
            };
            var dt = db.ExecuteToTable(sql,pars);
          
            return dt;
        }

        DataTable IWeight.GetCoOrderMainNew(string last_req)
        {
            if (last_req == "0001-01-01 00:00:00")
            {
                last_req = "1973-01-01 00:00:00";
            }
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            last_req = last_req.Substring(0, 10);
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select sheet_no,sup_no,convert(varchar(10),valid_date,120) valid_date,oper_date,total_amount,trans_no,approve_flag,approve_date,memo,num1 from co_t_order_main  where convert(varchar(10),valid_date,120)>=@valid_date " +
                "and oper_date>=@last_req and approve_flag='1' and trans_no='S' and num1=1 ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@valid_date",date),
                new System.Data.SqlClient.SqlParameter("@last_req",last_req)
            };
            var dt = db.ExecuteToTable(sql, pars);

            return dt;
        }

        DataTable IWeight.GetCoOrderDetail(string valid_date)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select * from co_t_order_child where sheet_no in " +
                "(select sheet_no from co_t_order_main where convert(varchar(10),valid_date,120)>=@valid_date and trans_no='S' and approve_flag='1' ) ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@valid_date",valid_date)
            };
            var dt = db.ExecuteToTable(sql, pars);
            return dt;
        }
        DataTable IWeight.GetCoOrderDetailNew()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select * from co_t_order_child where sheet_no in " +
                "(select sheet_no from co_t_order_main where num1=1 and trans_no='S' and approve_flag='1' ) ";
           
            var dt = db.ExecuteToTable(sql,null);
            return dt;
        }


        DataTable IWeight.GetSystemPars()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select * from sys_t_system where 1=1";
            var pars = new System.Data.SqlClient.SqlParameter[]
            { 
            };
            var dt = db.ExecuteToTable(sql, null);
            return dt;
        }

        DataTable IWeight.GetOperList()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select oper_id,oper_type,oper_name,oper_pw,oper_status,update_time,is_branch,is_admin,branch_no ";
            sql += "from sa_t_operator_i where 1=1 ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
            };
            var dt = db.ExecuteToTable(sql, null);
            return dt;
        }

        DataTable IWeight.GetPiWeightList()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select * ";
            sql += "from bi_t_piweight_info where 1=1 ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
            };
            var dt = db.ExecuteToTable(sql, null);
            return dt;
        }

        DataTable IWeight.GetItemClsList()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select item_clsno,item_clsname,item_flag,display_flag ";
            sql += "from bi_t_item_cls where item_flag='0' and len(item_clsno)=2 and 1=1";//"update_time>=@sysn_time ";

            var pars = new System.Data.SqlClient.SqlParameter[]
            {
            };
            var dt = db.ExecuteToTable(sql, null);
            return dt;
        }

        DataTable IWeight.GetItemList(string sys_time)
        {
            if (sys_time == "0001-01-01 00:00:00")
            {
                sys_time = "1973-01-01 00:00:00";
            }
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select item_no,item_subno,item_subname,item_clsno,item_name,unit_no,item_size,sup_no,product_area";
            sql += ",barcode,price,base_price,base_price2,base_price3,sale_price,display_flag,isnull(bala_flag,'0') as bala_flag,weight_diff ";
            sql += "from bi_t_item_info where update_time>=@sysn_time ";


            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                    new System.Data.SqlClient.SqlParameter("@sysn_time",sys_time)
            };
            var dt = db.ExecuteToTable(sql, pars);
            return dt;
        }

        DataTable IWeight.GetItemPoList(string sys_time)
        {
            if (sys_time == "0001-01-01 00:00:00")
            {
                sys_time = "1973-01-01 00:00:00";
            }
            return new DataTable();
        }

        DataTable IWeight.GetSupCusList(string sys_time)
        {
            if (sys_time == "0001-01-01 00:00:00")
            {
                sys_time = "1973-01-01 00:00:00";
            }
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select supcust_no,supcust_flag,sup_name,display_flag,sup_tel,sup_pyname,credit_amt,other1,cust_level,sup_man ";
            sql += "from bi_t_supcust_info where update_time>=@sysn_time ";

            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                    new System.Data.SqlClient.SqlParameter("@sysn_time",sys_time)
            };
            var dt = db.ExecuteToTable(sql, pars);
            return dt;
        }

        void IWeight.UploadWeighing(List<ot_weighing> lines)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            foreach (ot_weighing line in lines)
            {
                var sql = "select isnull(max(flow_id),0) + 1 as flow_id from ot_weighing ";
                line.flow_id = Helper.Conv.ToInt64(db.ExecuteScalar(sql, null));
                line.oper_date = line.create_time;
                sql="select item_name from bi_t_item_info where item_no='"+line.item_no+"'";
                line.item_name = db.ExecuteScalar(sql, null).ToString();
                db.Insert(line, "is_upload,pick_type");
            }
        }

        void IWeight.UploadCheck(List<ot_check_flow> lines)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            foreach (ot_check_flow line in lines)
            {
               // var sql = "select isnull(max(flow_id),0) + 1 as flow_id from ot_check_flow ";
              //  line.flow_id = Helper.Conv.ToInt64(db.ExecuteScalar(sql, null));
                line.task_flow_id =line.bc_no;
                line.create_time = line.oper_date;
                line.is_append = "0";
                db.Insert(line, "is_upload,bc_no,flow_id");
            }
        }









    }
}