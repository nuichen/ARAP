
using IvyTran.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using IvyTran.BLL.ERP;
using IvyTran.IBLL.ERP;

namespace IvyTran.svr.ERP
{
    public class batch_stock : BaseService
    {

        protected override string ProcessRequestGetHandler(string t)
        {
            return base.ProcessRequestGetHandler(t);
        }

        protected override string ProcessRequestPostHandler(string t, Dictionary<string, object> kv)
        {
            WebHelper web = new WebHelper(base.ReadContext);

            try
            {
                web.ReflectionMethod(this, t, kv);
                web.WriteSuccess();
            }
            catch (Exception ex)
            {
                web.WriteError(ex);
            }

            return web.NmJson();
        }
        BatchProcessing bp = new BatchProcessing();
        public void get_branch_stock(WebHelper w, Dictionary<string, object> kv)
        {
            IBranch branch = new Branch();
            //System.Data.DataTable IBranch.GetBranchStock(string item_no, string batch_no)
            string item_no = w.Read("item_no");
            string batch_no = w.Read("batch_no");
            DataTable dt = branch.GetBranchStock(item_no, batch_no);
            w.Write("server_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            w.Write("data", dt);
        }
        public void GetAcailableBatches(WebHelper w, Dictionary<string, object> kv)
        {
            string branch_no = w.Read("branch_no");
            string item_no = w.Read("item_no");
            var lis = bp.GetAcailableBatches(branch_no, item_no);

            w.Write(lis);
        }
        public void GetAcailableBatchesTable(WebHelper w, Dictionary<string, object> kv)
        {
            string branch_no = w.Read("branch_no");
            string item_no = w.Read("item_no");
            var tb = bp.GetAcailableBatchesTable(branch_no, item_no);

            w.Write(tb);
        }
        public void GetAllAcailableBatchesTable(WebHelper w, Dictionary<string, object> kv)
        {
            string branch_no = w.Read("branch_no");
            string item_no = w.Read("item_no");
            DateTime endTime = w.ObjectToDate("endTime");
            DateTime startTime = w.ObjectToDate("startTime");
            var tb = bp.GetAllAcailableBatchesTable(branch_no, item_no, startTime, endTime);

            w.Write(tb);
        }
        public void BatchWriteOff(WebHelper w, Dictionary<string, object> kv)
        {
            bp.BatchWriteOff();
        }

        public void SearchLowBatchSheet(WebHelper w, Dictionary<string, object> kv)
        {
            decimal minStock = w.ObjectToDecimal("minStock");

            DataTable tb = bp.SearchLowBatchSheet(minStock);
            w.Write(tb);
        }

        public void SearchWatchedBatchSheet(WebHelper w, Dictionary<string, object> kv, decimal minStock)
        {
            DataTable tb = bp.SearchWatchedBatchSheet(minStock);
            w.Write(tb);
        }

        public void SearchLowBatchStock(WebHelper w, Dictionary<string, object> kv)
        {
            decimal minStock = w.ObjectToDecimal("minStock");

            DataTable tb = bp.SearchLowBatchStock(minStock);
            w.Write(tb);
        }

        public void BatchWriteOffSheet(WebHelper w, Dictionary<string, object> kv)
        {
            DataTable lowTable = w.GetDataTable("lowTable");
            DataTable writeTable = w.GetDataTable("writeTable");
            decimal minStock = w.ObjectToDecimal("minStock");
            bp.BatchWriteOffSheet(lowTable, writeTable, minStock);
        }

        public void BatchWriteOffBatch(WebHelper w, Dictionary<string, object> kv)
        {
            DataTable batchTable = w.GetDataTable("batchTable");
            bp.BatchWriteOffBatch(batchTable);
        }
    }
}