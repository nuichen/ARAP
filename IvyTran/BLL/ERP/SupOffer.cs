using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;

namespace IvyTran.BLL.ERP
{
    public class SupOffer:ISupOffer
    {
       
        public DataTable GetSupOffer(DateTime start_time, DateTime end_time, string sup_no, string approve_flag)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"";
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        public void CheckSupOffer(string sheet_nos, DateTime approve_date, string approve_man)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            string[] arr = sheet_nos.Substring(0, sheet_nos.Length - 1).Split(',');
            string sheets = "";
            for (int i = 0; i < arr.Length; i++)
            {
                sheet_nos+="'" + arr[i] + "',";
            }

            sheets = sheets.ToString().Substring(0, sheets.Length - 1);
            try
            {
                db.Open();

                db.BeginTran();
                string sql = "select approve_flag from {table} where sheet_no in ("+sheets+")";
               DataTable dt= d.ExecuteToTable(sql, null);
               DataRow[] dr = dt.Select("approve_flag='1'");
               if (dr.Length > 0)
               {
                   throw new Exception("部分单据已经审核！");
               }

               //
                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("CheckBLL.AddCheckSheet()", ex.ToString(), sheet_nos.Substring(0,sheet_nos.Length-1));
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