using System;
using System.Collections.Generic;
using System.Data;
using DB;
using IvyTran.Helper;
using IvyTran.IBLL.ProcessingScale;
using Model;

namespace IvyTran.BLL.ProcessingScale
{
    public class ProcessBLL : IProcess
    {
        DB.IDB db = new DB.DBByHandClose(AppSetting.conn);

        public DataTable Getbi_t_bom_master(string last_time)
        {
            try
            {
                string text = "select bom_no,bom_name,approve_flag,oper_id from bi_t_bom_master " +
                    " where convert(varchar(19),update_time,120)>='" + last_time + "'";
                return db.ExecuteToTable(text, null);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("ProcessBLL—bi_t_bom_master", ex.ToString());
                return null;
            }
        }

        public DataTable Getbi_t_item_cls(string last_time)
        {
            try
            {
                string text = "select item_clsno,item_clsname,display_flag from bi_t_item_cls ";
                text = text + " where convert(varchar(19),update_time,120)>='" + last_time + "' ";
                return db.ExecuteToTable(text, null);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("ProcessBLL—bi_t_item_cls", ex.ToString());
                return null;
            }

        }

        public DataTable Getbi_t_item_info(string last_time)
        {
            try
            {
                string text = $@"SELECT *
FROM
    (
        SELECT a.*, b.pick_rate, b.std_weight, b.pick_type, b.std_volume, b.plan_qty
        FROM dbo.bi_t_item_info a
        LEFT JOIN dbo.bi_t_item_po b
          ON a.item_no = b.item_no
        WHERE CONVERT ( varchar(19), a.update_time, 120 ) >= '{last_time}'
    ) t;";
                return db.ExecuteToTable(text, null);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("ProcessBLL—bi_t_item_info", ex.ToString());
                return null;
            }
        }

        public DataTable Getbi_t_bom_detail()
        {
            try
            {
                string text = "select flow_id,bom_no,item_no,qty,unit_no,loss_rate from bi_t_bom_detail ";
                return db.ExecuteToTable(text, null);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("ProcessBLL—bi_t_bom_master", ex.ToString());
                return null;
            }
        }

        public DataTable Getco_t_order_main()
        {
            try
            {
                string text = "SELECT * FROM co_t_order_main where trans_no='PP' ";
                return db.ExecuteToTable(text, null);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("ProcessBLL—>GetParamsList", ex.ToString());
                return null;
            }
        }
        public DataTable Getco_t_order_child(string sheet_no)
        {
            try
            {
                string text = "SELECT * FROM co_t_order_child where sheet_no='" + sheet_no + "'";
                return db.ExecuteToTable(text, null);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("ProcessBLL—>GetParamsList", ex.ToString());
                return null;
            }
        }

        public DataTable Getot_processing_task(string ph_sheet_no)
        {
            try
            {
                string jiasql = "";
                if (ph_sheet_no != null && !"".Equals(ph_sheet_no))
                {
                    jiasql += $" and ph_sheet_no='{ph_sheet_no}'";
                }
                string text = "select flow_id,ph_sheet_no,pro_code,pro_qty,pro_unit,pro_bom,item_no,unit_no,need_qty,out_date" +
                    "  from ot_processing_task where 1=1 " + jiasql + "";
                return db.ExecuteToTable(text, null);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("ProcessBLL—bi_t_bom_master", ex.ToString());
                return null;
            }
        }

        public int InsertProcess(List<ot_processing> list)
        {
            DBByHandClose dbbyHandClose = new DBByHandClose(AppSetting.conn);
            IDB idb = dbbyHandClose;
            try
            {
                dbbyHandClose.Open();
                dbbyHandClose.BeginTran();
                foreach (ot_processing ot_weighing in list)
                {
                    ot_weighing.create_time = DateTime.Now;
                    idb.Insert(ot_weighing);
                }
                dbbyHandClose.CommitTran();
                return 1;
            }
            catch (Exception ex)
            {
                dbbyHandClose.RollBackTran();
                LogHelper.writeLog("ProcessBLL—InsertProcess", ex.ToString());
                return 0;
            }
            finally
            {
                dbbyHandClose.Close();
            }
        }
        decimal GetMaxId(string tb, IDB db)
        {
            string text = "select max(flow_id) flow_id from " + tb + "";

            DataTable dt = db.ExecuteToTable(text, null);
            if (dt == null || dt.Rows.Count == 0)
            {
                return 1;
            }
            else
            {
                return dt.Rows[0]["flow_id"].ToDecimal() + 1;
            }
        }

        public DataTable Getic_t_pspc_main()
        {
            try
            {
                string text = "SELECT * FROM ic_t_pspc_main";
                return db.ExecuteToTable(text, null);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("ProcessBLL—Getic_t_pspc_main", ex.ToString());
                return null;
            }
        }
        public DataTable GetOperList()
        {
            try
            {
                string text = "SELECT * FROM sa_t_operator_i";
                return db.ExecuteToTable(text, null);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("ProcessBLL—>GetOperList", ex.ToString());
                return null;
            }
        }

        public DataTable GetParamsList()
        {
            try
            {
                string text = "SELECT * FROM sys_t_system";
                return db.ExecuteToTable(text, null);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("ProcessBLL—>GetParamsList", ex.ToString());
                return null;
            }
        }
    }
}