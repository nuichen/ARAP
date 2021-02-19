using DB;
using IvyTran.Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace IvyTran.BLL.ERP
{
    /// <summary>
    /// 批次操作
    /// </summary>
    public class BatchProcessing
    {
        #region 批次库存查询搜索
        public DataTable GetAcailableBatchesTable(string branch_no, string item_no)
        {
            IDB d = new DBByAutoClose(AppSetting.conn);

            string sql = $@"SELECT s.*,i.item_subno,i.item_name
FROM dbo.ic_t_batch_branch_stock s
LEFT JOIN dbo.bi_t_item_info i ON i.item_no = s.item_no
WHERE s.branch_no='{branch_no}'  
{(!string.IsNullOrEmpty(item_no) ? $"and  s.item_no='{item_no}'" : "")}  
AND s.stock_qty>0
AND s.batch_no <> ''
ORDER BY s.update_time";

            var tb = d.ExecuteToTable(sql, null);
            return tb;
        }
        public List<ic_t_batch_branch_stock> GetAcailableBatches(string branch_no, string item_no)
        {
            IDB d = new DBByAutoClose(AppSetting.conn);

            string sql = $@"SELECT s.*,i.item_subno,i.item_name
FROM dbo.ic_t_batch_branch_stock s
LEFT JOIN dbo.bi_t_item_info i ON i.item_no = s.item_no
WHERE s.branch_no='{branch_no}'  
{(!string.IsNullOrEmpty(item_no) ? $"and  s.item_no='{item_no}'" : "")}  
AND s.stock_qty>0
AND s.batch_no <> ''
ORDER BY s.update_time";

            List<ic_t_batch_branch_stock> list = d.ExecuteToList<ic_t_batch_branch_stock>(sql, null);

            return list;
        }

        public DataTable GetAllAcailableBatchesTable(string branch_no, string item_no, DateTime startTime, DateTime endTime)
        {
            IDB d = new DBByAutoClose(AppSetting.conn);

            string sql = $@"SELECT s.*,i.item_subno,i.item_name
FROM dbo.ic_t_batch_branch_stock s
LEFT JOIN dbo.bi_t_item_info i ON i.item_no = s.item_no
WHERE s.branch_no='{branch_no}'  
{(!string.IsNullOrEmpty(item_no) ? $"and  s.item_no='{item_no}'" : "")}
AND s.update_time BETWEEN '{startTime.Toyyyy_MM_ddStart()}' AND '{endTime.Toyyyy_MM_ddEnd()}'
ORDER BY s.update_time";

            var tb = d.ExecuteToTable(sql, null);
            return tb;
        }
        #endregion

        #region 批次生成保存审核
        public static void SaveInOutSheetBatch(IDB d, Model.ic_t_inout_store_master ord, List<body.ic_t_inout_store_detail> lines, out List<body.ic_t_inout_store_detail> outLines)
        {
            outLines = lines;
            switch (ord.trans_no)
            {
                case "A":
                    SaveInSheetBatch(d, ord, lines);
                    break;
                case "D":
                    break;
                case "F":
                    break;
                case "G":
                    break;
                case "PO":
                    break;
                case "01":
                    break;
                case "03":
                    break;
                case "OM":
                    SaveOutSheetBatch(d, ord.branch_no, lines, out outLines);
                    break;
                case "RM":
                    break;
                case "PE":
                    SaveInSheetBatch(d, ord, lines);
                    break;
                default:
                    break;
            }
        }
        public static void SaveInSheetBatch(IDB d, Model.ic_t_inout_store_master ord, List<body.ic_t_inout_store_detail> lines)
        {
            //仓库号+供应商+YYMMDD+4位ID
            string bathNum = (string.IsNullOrWhiteSpace(ord.supcust_no) ? "000" : ord.supcust_no) + "_" + DateTime.Now.ToString("yyMMdd") + "_" + ord.sheet_no;

            int id = 1;
            var maxBatchNum = d.ExecuteScalar($@"SELECT MAX(RIGHT(batch_no,4)) FROM dbo.rp_t_batch_flow WHERE batch_no LIKE '{bathNum}%' ", null);
            if (maxBatchNum != null && !string.IsNullOrEmpty(maxBatchNum.ToString()))
            {
                id = maxBatchNum.ToInt32() + 1;
            }

            foreach (var detail in lines)
            {
                string itemNo = detail.item_no;
                string sql = $@"SELECT is_batch FROM dbo.k3_t_item_info_expand where item_no='{itemNo}'";
                if (d.ExecuteScalar(sql, null).ToInt32() != 1)
                {
                    continue;
                }

                if (string.IsNullOrEmpty(detail.batch_num))
                {
                    //商品明细没有批次生成批次
                    detail.batch_num = bathNum + "_" + (id++).ToString("0000");

                    rp_t_batch_flow flow = new rp_t_batch_flow()
                    {
                        branch_no = ord.branch_no,
                        batch_no = detail.batch_num,
                        item_no = itemNo,
                        voucher_no = ord.sheet_no,
                        batch_type = ord.trans_no,
                        batch_property = "良品",
                        area_no = "",
                        item_size = "",
                        unit_no = detail.unit_no,
                        unit_factor = detail.unit_factor,
                        in_price = detail.valid_price,
                        cost_price = detail.cost_price,
                        total_qnty = detail.in_qty,
                        settle_qnty = detail.in_qty,
                        sub_amount = detail.sub_amount,
                        oper_date = DateTime.Now,
                        oper_id = ord.oper_id,
                        update_time = DateTime.Now,
                    };
                    d.Insert(flow);
                }

            }
        }
        public static void SaveSaleSheetBatch(IDB d, string branch_no, List<body. sm_t_salesheet_detail> lines, out List<body.sm_t_salesheet_detail> outLines)
        {
            BatchWriteOff(d, branch_no, lines, out outLines);
        }
        public static void SaveOutSheetBatch(IDB d, string branch_no, List<body.ic_t_inout_store_detail> lines, out List<body.ic_t_inout_store_detail> outLines)
        {
            BatchWriteOff(d, branch_no, lines, out outLines);
        }

        public static void CheckInOutSheet(IDB d, string sheet_no)
        {
            var type = Regex.Replace(sheet_no, "[0-9]", "", RegexOptions.IgnoreCase);
            switch (type)
            {
                case "A":
                    break;
                case "D":
                    CheckReturnInSheetBatch(d, sheet_no);
                    break;
                case "F":
                    CheckReturnOutSheetBatch(d, sheet_no);
                    break;
                case "G":
                    break;
                case "PO":
                    break;
                case "01":
                    break;
                case "03":
                    break;
                case "OM":
                    CheckOutSheetBatch(d, sheet_no);
                    break;
                case "RM":
                    CheckReturnInSheetBatch(d, sheet_no);
                    break;
                case "PE":
                    CheckInSheetBatch(d, sheet_no);
                    break;
                default:
                    break;
            }
        }
        public static void CheckInSheetBatch(IDB d, string sheet_no)
        {
            DataTable tb = d.ExecuteToTable($@"SELECT d.flow_id,m.sheet_no,m.trans_no, m.branch_no,m.supcust_no, d.item_no, d.unit_no, d.unit_factor, d.sheet_sort, d.batch_num,
       d.in_qty, d.cost_price, d.valid_price, d.sub_amount, d.valid_date, m.oper_id
FROM dbo.ic_t_inout_store_detail d
LEFT JOIN dbo.ic_t_inout_store_master m
  ON m.sheet_no = d.sheet_no
LEFT JOIN dbo.rp_t_batch_flow b
  ON b.batch_no = d.batch_num
LEFT JOIN dbo.k3_t_item_info_expand k
  ON k.item_no = b.item_no
WHERE b.batch_no IS NULL
      AND
        (
          d.batch_num IS NULL
          OR d.batch_num = ''
        )
      AND k.is_batch = '1'
      AND m.sheet_no = '{sheet_no}'", null);
            foreach (DataRow row in tb.Rows)
            {
                string batch_no = row["batch_num"].ToString();
                string branch_no = row["branch_no"].ToString();
                string item_no = row["item_no"].ToString();
                string sup_no = row["supcust_no"].ToString();
                if (string.IsNullOrEmpty(batch_no))
                {
                    batch_no = (string.IsNullOrWhiteSpace(sup_no) ? "000" : sup_no) + "_" + DateTime.Now.ToString("yyMMdd") + "_" + sheet_no;
                    int id = 1;
                    var maxBatchNum = d.ExecuteScalar($@"SELECT MAX(RIGHT(batch_no,4)) FROM dbo.rp_t_batch_flow WHERE batch_no LIKE '{batch_no}%' ", null);
                    if (maxBatchNum != null && !string.IsNullOrEmpty(maxBatchNum.ToString()))
                    {
                        id = maxBatchNum.ToInt32() + 1;
                    }
                    batch_no = batch_no + "_" + (id++).ToString("0000");

                    d.ExecuteScalar($@"UPDATE dbo.ic_t_inout_store_detail SET batch_num='{batch_no}'
WHERE flow_id='{row["flow_id"].ToString()}'", null);
                }

                rp_t_batch_flow flow = new rp_t_batch_flow()
                {
                    branch_no = branch_no,
                    batch_no = batch_no,
                    item_no = item_no,
                    voucher_no = sheet_no,
                    batch_type = row["trans_no"].ToString(),
                    batch_property = "良品",
                    area_no = branch_no,
                    unit_no = row["unit_no"].ToString(),
                    unit_factor = row["unit_factor"].ToDecimal(),
                    in_price = row["valid_price"].ToDecimal(),
                    cost_price = row["cost_price"].ToDecimal(),
                    total_qnty = row["in_qty"].ToDecimal(),
                    settle_qnty = row["in_qty"].ToDecimal(),
                    sub_amount = row["sub_amount"].ToDecimal(),
                    valid_date = row["valid_date"].ToDateTime(),
                    oper_date = DateTime.Now,
                    oper_id = row["oper_id"].ToString(),
                    update_time = DateTime.Now
                };
                d.Insert(flow);

            }


            d.ExecuteScalar($@"UPDATE dbo.ic_t_batch_branch_stock
SET stock_qty = stock_qty + d.in_qty
FROM
  (
    SELECT batch_num, SUM ( in_qty ) in_qty
    FROM dbo.ic_t_inout_store_detail
    WHERE sheet_no = '{sheet_no}'
    GROUP BY batch_num
  ) d
WHERE batch_no = d.batch_num;", null);

            d.ExecuteScalar($@"INSERT dbo.ic_t_batch_branch_stock
     ( branch_no, item_no, batch_no, area_no, unit_no, stock_qty, cost_price, unit_factor,
       unpack_qnty, display_flag, last_price, fifo_price, max_change, update_time )
SELECT m.branch_no, d.item_no, d.batch_num, m.branch_no, d.unit_no, SUM ( d.in_qty ),
       d.valid_price, d.unit_factor, '1', '1', d.valid_price, d.valid_price, 1, GETDATE ()
FROM dbo.ic_t_inout_store_detail d
LEFT JOIN dbo.ic_t_inout_store_master m
  ON m.sheet_no = d.sheet_no
LEFT JOIN dbo.ic_t_batch_branch_stock bl
  ON bl.batch_no = d.batch_num
WHERE d.batch_num <> ''
      AND bl.batch_no IS NULL
      AND m.sheet_no = '{sheet_no}'
GROUP BY m.branch_no, d.item_no, d.unit_no, d.unit_factor, d.valid_price, d.batch_num;", null);
            DateTime dt = DateTime.Now;
            string s = $@"insert into ic_t_batch_info(branch_no,batch_no,create_time,factory_no,produce_date,valid_date,update_time)
select mas.branch_no,det.batch_num batch_no,GETDATE() create_time,info.factory_no,'{dt}' produce_date,'{dt.AddDays(+240)}' valid_date,'{dt}' update_time from 
[dbo].[ic_t_inout_store_detail] det 
left join ic_t_inout_store_master mas on mas.sheet_no=det.sheet_no
left join [dbo].[ic_t_batch_branch_stock] strok 
on det.batch_num=strok.batch_no and det.item_no=strok.item_no
left join [dbo].[bi_t_item_info] info on info.item_no=det.item_no 
left join ic_t_batch_info batch on batch.batch_no=strok.batch_no 
and batch.branch_no=strok.branch_no 
where batch.batch_no is null  and det.sheet_no='{sheet_no}'
";
            d.ExecuteScalar(s, null);
        }
        //        public static void CheckInSheetBatch(IDB d, string sheet_no)
        //        {
        //            DataTable tb = d.ExecuteToTable($@"SELECT d.flow_id,m.sheet_no,m.trans_no, m.branch_no,m.supcust_no, d.item_no, d.unit_no, d.unit_factor, d.sheet_sort, d.batch_num,
        //       d.in_qty, d.cost_price, d.valid_price, d.sub_amount, d.valid_date, m.oper_id
        //FROM dbo.ic_t_inout_store_detail d
        //LEFT JOIN dbo.ic_t_inout_store_master m
        //  ON m.sheet_no = d.sheet_no
        //LEFT JOIN dbo.rp_t_batch_flow b
        //  ON b.batch_no = d.batch_num
        //LEFT JOIN dbo.k3_t_item_info_expand k
        //  ON k.item_no = b.item_no
        //WHERE b.batch_no IS NULL
        //      AND
        //        (
        //          d.batch_num IS NULL
        //          OR d.batch_num = ''
        //        )
        //      AND k.is_batch = '1'
        //      AND m.sheet_no = '{sheet_no}'", null);
        //            foreach (DataRow row in tb.Rows)
        //            {
        //                string batch_no = row["batch_num"].ToString();
        //                string branch_no = row["branch_no"].ToString();
        //                string item_no = row["item_no"].ToString();
        //                string sup_no = row["supcust_no"].ToString();
        //                if (string.IsNullOrEmpty(batch_no))
        //                {
        //                    batch_no = (string.IsNullOrWhiteSpace(sup_no) ? "000" : sup_no) + "_" + DateTime.Now.ToString("yyMMdd") + "_" + sheet_no;
        //                    int id = 1;
        //                    var maxBatchNum = d.ExecuteScalar($@"SELECT MAX(RIGHT(batch_no,4)) FROM dbo.rp_t_batch_flow WHERE batch_no LIKE '{batch_no}%' ", null);
        //                    if (maxBatchNum != null && !string.IsNullOrEmpty(maxBatchNum.ToString()))
        //                    {
        //                        id = maxBatchNum.ToInt32() + 1;
        //                    }
        //                    batch_no = batch_no + "_" + (id++).ToString("0000");

        //                    d.ExecuteScalar($@"UPDATE dbo.ic_t_inout_store_detail SET batch_num='{batch_no}'
        //WHERE flow_id='{row["flow_id"].ToString()}'", null);
        //                }

        //                rp_t_batch_flow flow = new rp_t_batch_flow()
        //                {
        //                    branch_no = branch_no,
        //                    batch_no = batch_no,
        //                    item_no = item_no,
        //                    voucher_no = sheet_no,
        //                    batch_type = row["trans_no"].ToString(),
        //                    batch_property = "良品",
        //                    area_no = branch_no,
        //                    unit_no = row["unit_no"].ToString(),
        //                    unit_factor = row["unit_factor"].ToDecimal(),
        //                    in_price = row["valid_price"].ToDecimal(),
        //                    cost_price = row["cost_price"].ToDecimal(),
        //                    total_qnty = row["in_qty"].ToDecimal(),
        //                    settle_qnty = row["in_qty"].ToDecimal(),
        //                    sub_amount = row["sub_amount"].ToDecimal(),
        //                    valid_date = row["valid_date"].ToDateTime(),
        //                    oper_date = DateTime.Now,
        //                    oper_id = row["oper_id"].ToString(),
        //                    update_time = DateTime.Now
        //                };
        //                d.Insert(flow);

        //            }


        //            d.ExecuteScalar($@"UPDATE dbo.ic_t_batch_branch_stock
        //SET stock_qty = stock_qty + d.in_qty
        //FROM
        //  (
        //    SELECT batch_num, SUM ( in_qty ) in_qty
        //    FROM dbo.ic_t_inout_store_detail
        //    WHERE sheet_no = '{sheet_no}'
        //    GROUP BY batch_num
        //  ) d
        //WHERE batch_no = d.batch_num;", null);

        //            d.ExecuteScalar($@"INSERT dbo.ic_t_batch_branch_stock
        //     ( branch_no, item_no, batch_no, area_no, unit_no, stock_qty, cost_price, unit_factor,
        //       unpack_qnty, display_flag, last_price, fifo_price, max_change, update_time )
        //SELECT m.branch_no, d.item_no, d.batch_num, m.branch_no, d.unit_no, SUM ( d.in_qty ),
        //       d.valid_price, d.unit_factor, '1', '1', d.valid_price, d.valid_price, 1, GETDATE ()
        //FROM dbo.ic_t_inout_store_detail d
        //LEFT JOIN dbo.ic_t_inout_store_master m
        //  ON m.sheet_no = d.sheet_no
        //LEFT JOIN dbo.ic_t_batch_branch_stock bl
        //  ON bl.batch_no = d.batch_num
        //WHERE d.batch_num <> ''
        //      AND bl.batch_no IS NULL
        //      AND m.sheet_no = '{sheet_no}'
        //GROUP BY m.branch_no, d.item_no, d.unit_no, d.unit_factor, d.valid_price, d.batch_num;", null);
        //            DateTime dt=DateTime.Now;
        //            string s = $@"insert into ic_t_batch_info(branch_no,batch_no,create_time,factory_no,produce_date,valid_date,update_time)
        //select mas.branch_no,det.batch_num batch_no,GETDATE() create_time,info.factory_no,'{dt}' produce_date,'{dt.AddDays(+240)}' valid_date,'{dt}' update_time from 
        //[dbo].[ic_t_inout_store_detail] det 
        //left join ic_t_inout_store_master mas on mas.sheet_no=det.sheet_no
        //left join [dbo].[ic_t_batch_branch_stock] strok 
        //on det.batch_num=strok.batch_no and det.item_no=strok.item_no
        //left join [dbo].[bi_t_item_info] info on info.item_no=det.item_no 
        //left join ic_t_batch_info batch on batch.batch_no=strok.batch_no 
        //and batch.branch_no=strok.branch_no 
        //where batch.batch_no is null  and det.sheet_no='{sheet_no}'
        //";
        //            d.ExecuteScalar(s,null);
        //        }
        public static void CheckSaleSheetBatch(IDB d, string sheet_no)
        {
            string sql = $@"SELECT d.sheet_sort
FROM dbo.sm_t_salesheet_detail d
    LEFT JOIN dbo.sm_t_salesheet m
        ON m.sheet_no = d.sheet_no
    LEFT JOIN
    (SELECT * FROM dbo.ic_t_batch_branch_stock WHERE stock_qty > 0) s
        ON d.batch_num = s.batch_no
		LEFT JOIN dbo.bi_t_item_info i ON i.item_no = d.item_no
		left join [dbo].[k3_t_item_info_expand] g on i.item_no=g.item_no
WHERE m.sheet_no = '{sheet_no}'
      AND d.batch_num <> '待定'
	  AND g.is_batch<>'0'
      AND s.batch_no IS NULL ";


            DataTable tb = d.ExecuteToTable(sql, null);

            if (tb.Rows.Count > 0)
            {
                string errorItem = "";
                foreach (DataRow row in tb.Rows)
                {
                    errorItem += "第[" + row["sheet_sort"] + "]行批次无效或已被冲销" + Environment.NewLine;
                }

                throw new Exception("批次审核出错", new Exception(errorItem));
            }


            d.ExecuteScalar($@"UPDATE dbo.ic_t_batch_branch_stock
SET stock_qty = stock_qty - d.sale_qnty
FROM dbo.sm_t_salesheet_detail d
WHERE d.sheet_no = '{sheet_no}' AND d.batch_num = batch_no;", null);
        }
        public static void CheckOutSheetBatch(IDB d, string sheet_no)
        {
            string sql = $@"SELECT d.sheet_sort
FROM dbo.ic_t_inout_store_detail d
    LEFT JOIN dbo.ic_t_inout_store_master m
        ON m.sheet_no = d.sheet_no
    LEFT JOIN
    (SELECT * FROM dbo.ic_t_batch_branch_stock WHERE stock_qty > 0) s
        ON d.batch_num = s.batch_no
 LEFT JOIN dbo.k3_t_item_info_expand e ON e.item_no = d.item_no
WHERE m.sheet_no = '{sheet_no}'
      AND d.batch_num <> '待定'
      AND e.is_batch <> '0'
      AND s.batch_no IS NULL;";
            DataTable tb = d.ExecuteToTable(sql, null);

            if (tb.Rows.Count > 0)
            {
                string errorItem = "";
                foreach (DataRow row in tb.Rows)
                {
                    errorItem += "第[" + row["sheet_sort"] + "]行批次无效或已被冲销" + Environment.NewLine;
                }

                throw new Exception("批次审核出错", new Exception(errorItem));
            }

            d.ExecuteScalar($@"UPDATE dbo.ic_t_batch_branch_stock
SET stock_qty = stock_qty-d.in_qty
FROM dbo.ic_t_inout_store_detail d
WHERE d.sheet_no='{sheet_no}' AND d.batch_num=batch_no", null);


        }
        public static void CheckReturnOutSheetBatch(IDB d, string sheet_no)
        {
            //            string sql = $@"SELECT d.sheet_sort
            //FROM dbo.ic_t_inout_store_detail d
            //    LEFT JOIN dbo.ic_t_inout_store_master m
            //        ON m.sheet_no = d.sheet_no
            //    LEFT JOIN
            //    (SELECT * FROM dbo.ic_t_batch_branch_stock WHERE stock_qty > 0) s
            //        ON d.batch_num = s.batch_no
            // LEFT JOIN dbo.k3_t_item_info_expand e ON e.item_no = d.item_no
            //WHERE m.sheet_no = '{sheet_no}'
            //      AND d.batch_num <> '待定'
            //      AND e.is_batch <> '0'
            //      AND (s.batch_no IS NULL
            //      OR d.in_qty > s.stock_qty);";
            string sql = $@"SELECT d.sheet_sort,d.in_qty , s.stock_qty

FROM dbo.ic_t_inout_store_detail d
         LEFT JOIN dbo.ic_t_inout_store_master m
                   ON m.sheet_no = d.sheet_no
         LEFT JOIN
         (SELECT * FROM dbo.ic_t_batch_branch_stock WHERE stock_qty > 0) s
         ON d.batch_num = s.batch_no
WHERE m.sheet_no = '{sheet_no}'
  AND (d.batch_num <> N'待定' OR s.batch_no IS NULL)
  AND d.in_qty > s.stock_qty;";
            DataTable tb = d.ExecuteToTable(sql, null);

            if (tb.Rows.Count > 0)
            {
                string errorItem = "";
                foreach (DataRow row in tb.Rows)
                {
                    errorItem += "第[" + row["sheet_sort"] + "]行批次无效或已被冲销" + Environment.NewLine;
                }

                throw new Exception("批次审核出错", new Exception(errorItem));
            }

            d.ExecuteScalar($@"UPDATE dbo.ic_t_batch_branch_stock
SET stock_qty = 0
WHERE batch_no IN
      (
          SELECT batch_num
          FROM dbo.ic_t_inout_store_detail
          WHERE sheet_no = '{sheet_no}'
      );", null);

        }
        public static void CheckReturnInSheetBatch(IDB d, string sheet_no)
        {
            d.ExecuteScalar($@"UPDATE dbo.ic_t_batch_branch_stock
SET stock_qty = stock_qty+d.in_qty
FROM dbo.ic_t_inout_store_detail d
WHERE d.sheet_no='{sheet_no}' AND d.batch_num=batch_no", null);
        }

        private static void BatchWriteOff<T>(IDB d, string branch_no, List<T> lines, out List<T> outLines,
            decimal minStock = 0, bool isDealNoBatch = false, List<dynamic> batchList = null)
        {
            //冲销批次
            Type type = typeof(T);

            PropertyInfo qtyInfo, priceInfo, moneyInfo;
            if (type.Name.Equals(typeof(sm_t_salesheet_detail).Name))
            {
                qtyInfo = type.GetProperty("sale_qnty");
                priceInfo = type.GetProperty("real_price");
                moneyInfo = type.GetProperty("sale_money");
            }
            else
            {
                qtyInfo = type.GetProperty("in_qty");
                priceInfo = type.GetProperty("valid_price");
                moneyInfo = type.GetProperty("sub_amount");
            }
            HashSet<string> itemBatch = new HashSet<string>();
            outLines = new List<T>();

            if (batchList == null)
            {
                batchList = new List<dynamic>();
                string sql = $@"SELECT s.*,i.item_subno,i.item_name
FROM dbo.ic_t_batch_branch_stock s
LEFT JOIN dbo.bi_t_item_info i ON i.item_no = s.item_no
WHERE s.branch_no='{branch_no}' 
AND s.stock_qty>{minStock}
ORDER BY s.update_time";
                var dt = d.ExecuteToTable(sql, null);
                foreach (DataRow row in dt.Rows)
                {
                    dynamic dyn = new ExpandoObject();
                    dyn.branch_no = row["branch_no"].ToString();
                    dyn.item_no = row["item_no"].ToString();
                    dyn.batch_num = row["batch_no"].ToString();
                    dyn.stock_qty = row["stock_qty"].ToDecimal();

                    batchList.Add(dyn);
                }

            }

            var tb = d.ExecuteToTable($@"SELECT item_no FROM dbo.k3_t_item_info_expand where is_batch='1'", null);
            foreach (DataRow row in tb.Rows)
            {
                itemBatch.Add(row["item_no"].ToString());
            }

            int index = 1;
            foreach (dynamic line in lines)
            {
                decimal qty = ConvExtension.ToDecimal(qtyInfo.GetValue(line, null));
                decimal price = ConvExtension.ToDecimal(priceInfo.GetValue(line, null));
                if (itemBatch.Contains(line.item_no))
                {
                    if (string.IsNullOrEmpty(line.batch_num))
                    {
                        if (isDealNoBatch)
                        {
                            line.batch_num = batchList.FirstOrDefault(b => b.item_no.Equals(line.item_no))?.batch_num;
                        }
                        else
                        {
                            //没有批次
                            line.batch_num = "待定";
                            line.sheet_sort = index++;
                            outLines.Add(line);
                            continue;
                        }
                    }
                    else if ("待定".Equals(line.batch_num) && isDealNoBatch)
                    {
                        line.batch_num = batchList.FirstOrDefault(b => b.branch_no.Equals(branch_no) && b.item_no.Equals(line.item_no))?.batch_num;
                    }

                    var value = batchList.FirstOrDefault(b => b.batch_num.Equals(line.batch_num));
                    if (value == null && !isDealNoBatch)
                    {
                        //批次不存在或已被冲销
                        line.sheet_sort = index++;
                        line.batch_num = "待定";
                        outLines.Add(line);
                    }
                    else
                    {
                        if (value == null && isDealNoBatch)
                        {
                            value = batchList.FirstOrDefault(b => b.branch_no.Equals(branch_no) && b.item_no.Equals(line.item_no));//重新分配批次
                            if (value == null)
                            {
                                //批次不存在或已被冲销
                                line.sheet_sort = index++;
                                line.batch_num = "待定";
                                outLines.Add(line);
                                continue;
                            }
                        }

                        if (qty > value.stock_qty)
                        {
                            //出库数量大于批次库存数量
                            decimal diffQty = qty - value.stock_qty;

                            qtyInfo.SetValue(line, value.stock_qty, null);
                            moneyInfo.SetValue(line, value.stock_qty * price, null);
                            line.sheet_sort = index++;
                            outLines.Add(line);
                            batchList.Remove(value);

                            while (diffQty > 0)
                            {
                                value = batchList.FirstOrDefault(b => b.branch_no.Equals(branch_no) && b.item_no.Equals(line.item_no));
                                if (value == null)
                                {
                                    //检索所有批次都不够出库
                                    var copyLIne = line.Clone();
                                    copyLIne.sheet_sort = index++;
                                    qtyInfo.SetValue(copyLIne, diffQty, null);
                                    moneyInfo.SetValue(copyLIne, diffQty * price, null);
                                    copyLIne.batch_num = "待定";
                                    outLines.Add(copyLIne);
                                    break;
                                }

                                if (value.stock_qty >= diffQty)
                                {
                                    //新批次库存够
                                    var copyLIne = line.Clone();
                                    copyLIne.sheet_sort = index++;
                                    qtyInfo.SetValue(copyLIne, diffQty, null);
                                    moneyInfo.SetValue(copyLIne, diffQty * price, null);
                                    copyLIne.batch_num = value.batch_num;
                                    outLines.Add(copyLIne);

                                    diffQty = 0;
                                    value.stock_qty -= diffQty;
                                    if (value.stock_qty == 0)
                                    {
                                        batchList.Remove(value);
                                    }
                                }
                                else
                                {
                                    //新批次库存不够
                                    var copyLIne = line.Clone();
                                    copyLIne.sheet_sort = index++;
                                    qtyInfo.SetValue(copyLIne, value.stock_qty, null);
                                    moneyInfo.SetValue(copyLIne, value.stock_qty * price, null);
                                    copyLIne.batch_num = value.batch_num;
                                    outLines.Add(copyLIne);

                                    diffQty -= value.stock_qty;
                                    batchList.Remove(value);
                                }
                            }
                        }
                        else
                        {
                            //出库数量小于批次库存数量
                            line.sheet_sort = index++;
                            outLines.Add(line);

                            value.stock_qty -= qty;
                            if (value.stock_qty == 0)
                            {
                                batchList.Remove(value);
                            }

                        }
                    }
                }
                else
                {
                    //商品未启用批次
                    line.sheet_sort = index++;
                    outLines.Add(line);
                }
            }

        }

        public void BatchWriteOff()
        {
            BatchWriteOff(null, null, 0);
        }
        public void BatchWriteOff(string saleIds, string detailIds, decimal minStock)
        {
            string sql = "";
            var db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();


                sql = $@"SELECT m.branch_no,d.*
FROM dbo.sm_t_salesheet_detail d
LEFT JOIN dbo.sm_t_salesheet m ON m.sheet_no = d.sheet_no
WHERE d.batch_num='待定' AND m.approve_flag='1' {(string.IsNullOrEmpty(saleIds) ? "" : $@" and d.flow_id in ({saleIds}) ")}";
                DataTable tb = d.ExecuteToTable(sql, null);

                var groupBy = tb.AsEnumerable().GroupBy(t => t["branch_no"].ToString());
                foreach (var g in groupBy)
                {
                    string branch_no = g.Key;

                    var enumerable = g.GroupBy(t => t["sheet_no"].ToString());
                    foreach (var e in enumerable)
                    {
                        string sheet_no = e.Key;

                        List<sm_t_salesheet_detail> details = new List<sm_t_salesheet_detail>();
                        foreach (var row in e)
                        {
                            details.Add(ReflectionHelper.DataRowToModel<sm_t_salesheet_detail>(row));
                        }
                        BatchWriteOff(d, branch_no, details, out details, minStock, true);

                        d.ExecuteScalar($@"DELETE FROM dbo.sm_t_salesheet_detail WHERE  sheet_no='{sheet_no}' and batch_num='待定'", null);
                        foreach (var detail in details)
                        {
                            sql = "select isnull(max(flow_id),0) + 1 as flow_id from sm_t_salesheet_detail ";
                            detail.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                            d.Insert(detail);
                            if (!"待定".Equals(detail.batch_num))
                            {
                                d.ExecuteScalar($@"UPDATE dbo.ic_t_batch_branch_stock SET stock_qty=stock_qty-{detail.sale_qnty} WHERE batch_no='{detail.batch_num}'", null);
                            }
                        }

                    }

                }

                sql = $@"SELECT m.branch_no,d.*
FROM dbo.ic_t_inout_store_detail d
LEFT JOIN dbo.ic_t_inout_store_master m ON m.sheet_no = d.sheet_no
WHERE d.batch_num='待定' AND m.db_no='-'  AND m.approve_flag='1' {(string.IsNullOrEmpty(detailIds) ? "" : $@" and d.flow_id in ({detailIds}) ")}";
                tb = d.ExecuteToTable(sql, null);

                groupBy = tb.AsEnumerable().GroupBy(t => t["branch_no"].ToString());
                foreach (var g in groupBy)
                {
                    string branch_no = g.Key;

                    var enumerable = g.GroupBy(t => t["sheet_no"].ToString());
                    foreach (var e in enumerable)
                    {
                        string sheet_no = e.Key;

                        List<ic_t_inout_store_detail> details = new List<ic_t_inout_store_detail>();
                        foreach (var row in e)
                        {
                            details.Add(ReflectionHelper.DataRowToModel<ic_t_inout_store_detail>(row));
                        }
                        BatchWriteOff(d, branch_no, details, out details, minStock, true);

                        d.ExecuteScalar($@"DELETE FROM dbo.ic_t_inout_store_detail WHERE  sheet_no='{sheet_no}' and batch_num='待定'", null);
                        foreach (var detail in details)
                        {
                            sql = "select isnull(max(flow_id),0) + 1 as flow_id from ic_t_inout_store_detail ";
                            detail.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                            d.Insert(detail);
                            if (!"待定".Equals(detail.batch_num))
                            {
                                d.ExecuteScalar($@"UPDATE dbo.ic_t_batch_branch_stock SET stock_qty=stock_qty-{detail.in_qty} WHERE batch_no='{detail.batch_num}'", null);
                            }
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
        public void BatchWriteOff(IDB d, string saleIds, string detailIds, decimal minStock)
        {
            var sql = $@"SELECT m.branch_no,d.*
FROM dbo.sm_t_salesheet_detail d
LEFT JOIN dbo.sm_t_salesheet m ON m.sheet_no = d.sheet_no
WHERE d.batch_num='待定' AND m.approve_flag='1' {(string.IsNullOrEmpty(saleIds) ? "" : $@" and d.flow_id in ({saleIds}) ")}";
            DataTable tb = d.ExecuteToTable(sql, null);

            var groupBy = tb.AsEnumerable().GroupBy(t => t["branch_no"].ToString());
            foreach (var g in groupBy)
            {
                string branch_no = g.Key;

                var enumerable = g.GroupBy(t => t["sheet_no"].ToString());
                foreach (var e in enumerable)
                {
                    string sheet_no = e.Key;

                    List<sm_t_salesheet_detail> details = new List<sm_t_salesheet_detail>();
                    foreach (var row in e)
                    {
                        details.Add(ReflectionHelper.DataRowToModel<sm_t_salesheet_detail>(row));
                    }
                    BatchWriteOff(d, branch_no, details, out details, minStock, true);

                    d.ExecuteScalar($@"DELETE FROM dbo.sm_t_salesheet_detail WHERE  sheet_no='{sheet_no}' and batch_num='待定'", null);
                    foreach (var detail in details)
                    {
                        sql = "select isnull(max(flow_id),0) + 1 as flow_id from sm_t_salesheet_detail ";
                        detail.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                        d.Insert(detail);
                        if (!"待定".Equals(detail.batch_num))
                        {
                            d.ExecuteScalar($@"UPDATE dbo.ic_t_batch_branch_stock SET stock_qty=stock_qty-{detail.sale_qnty} WHERE batch_no='{detail.batch_num}'", null);
                        }
                    }
                }

            }

            sql = $@"SELECT m.branch_no,d.*
FROM dbo.ic_t_inout_store_detail d
LEFT JOIN dbo.ic_t_inout_store_master m ON m.sheet_no = d.sheet_no
WHERE d.batch_num='待定' AND m.db_no='-'  AND m.approve_flag='1' {(string.IsNullOrEmpty(detailIds) ? "" : $@" and d.flow_id in ({detailIds}) ")}";
            tb = d.ExecuteToTable(sql, null);

            groupBy = tb.AsEnumerable().GroupBy(t => t["branch_no"].ToString());
            foreach (var g in groupBy)
            {
                string branch_no = g.Key;

                var enumerable = g.GroupBy(t => t["sheet_no"].ToString());
                foreach (var e in enumerable)
                {
                    string sheet_no = e.Key;

                    List<ic_t_inout_store_detail> details = new List<ic_t_inout_store_detail>();
                    foreach (var row in e)
                    {
                        details.Add(ReflectionHelper.DataRowToModel<ic_t_inout_store_detail>(row));
                    }
                    BatchWriteOff(d, branch_no, details, out details, minStock, true);

                    d.ExecuteScalar($@"DELETE FROM dbo.ic_t_inout_store_detail WHERE  sheet_no='{sheet_no}' and batch_num='待定'", null);
                    foreach (var detail in details)
                    {
                        sql = "select isnull(max(flow_id),0) + 1 as flow_id from ic_t_inout_store_detail ";
                        detail.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                        d.Insert(detail);
                        if (!"待定".Equals(detail.batch_num))
                        {
                            d.ExecuteScalar($@"UPDATE dbo.ic_t_batch_branch_stock SET stock_qty=stock_qty-{detail.in_qty} WHERE batch_no='{detail.batch_num}'", null);
                        }
                    }
                }

            }

        }

        #endregion

        #region 批次日结

        public DataTable SearchLowBatchSheet(decimal minStock)
        {
            IDB db = new DBByAutoClose(AppSetting.conn);

            string sql = $@"SELECT '1' select_flag, t.type, i.branch_no, t.sheet_no, t.flow_id, t.item_no, i.item_name,
       i.unit_no, i.item_flag, t.real_qnty
FROM
  (
    SELECT LEFT(d.sheet_no, 2) type, m.branch_no, m.sheet_no, d.flow_id, d.item_no, d.in_qty real_qnty
    FROM dbo.ic_t_inout_store_detail d
    LEFT JOIN dbo.ic_t_inout_store_master m
      ON m.sheet_no = d.sheet_no
    WHERE d.batch_num = '待定'
          AND m.db_no = '-'
          AND m.approve_flag = '1'
          AND d.in_qty <= '{minStock}'
    UNION ALL
    SELECT 'SO' type, m.branch_no, m.sheet_no, d.flow_id, d.item_no, d.sale_qnty
    FROM dbo.sm_t_salesheet_detail d
    LEFT JOIN dbo.sm_t_salesheet m
      ON m.sheet_no = d.sheet_no
    WHERE d.batch_num = '待定'
          AND m.approve_flag = '1'
          AND d.sale_qnty <= '{minStock}'
  ) t
LEFT JOIN dbo.bi_t_item_info i
  ON i.item_no = t.item_no
ORDER BY i.branch_no, t.item_no;";

            DataTable tb = db.ExecuteToTable(sql, null);
            return tb;
        }
        public DataTable SearchWatchedBatchSheet(decimal minStock)
        {
            IDB db = new DBByAutoClose(AppSetting.conn);

            string sql = $@"SELECT '1' select_flag, t.type, i.branch_no, t.sheet_no, t.flow_id, t.item_no, i.item_name,
       i.unit_no, i.item_flag, t.real_qnty
FROM
  (
    SELECT LEFT(d.sheet_no, 2) type, m.branch_no, m.sheet_no, d.flow_id, d.item_no,
           d.in_qty real_qnty
    FROM dbo.ic_t_inout_store_detail d
    LEFT JOIN dbo.ic_t_inout_store_master m
      ON m.sheet_no = d.sheet_no
    WHERE d.batch_num = '待定'
          AND m.db_no = '-'
          AND m.approve_flag = '1'
          AND d.in_qty <= '{minStock}'
    UNION ALL
    SELECT 'SO' type, m.branch_no, m.sheet_no, d.flow_id, d.item_no, d.sale_qnty
    FROM dbo.sm_t_salesheet_detail d
    LEFT JOIN dbo.sm_t_salesheet m
      ON m.sheet_no = d.sheet_no
    WHERE d.batch_num = '待定'
          AND m.approve_flag = '1'
          AND d.sale_qnty <= '{minStock}'
  ) t
LEFT JOIN dbo.bi_t_item_info i
  ON i.item_no = t.item_no
ORDER BY i.branch_no, t.item_no;";

            DataTable tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        public DataTable SearchLowBatchStock(decimal minStock)
        {
            IDB db = new DBByAutoClose(AppSetting.conn);
            //            string sql = $@"SELECT CASE
            //           WHEN s.stock_qty <= '{minStock}' THEN
            //               '1'
            //           ELSE
            //               '0'
            //       END select_flag,
            //       b.voucher_no,
            //       i.item_name,
            //       i.unit_no,
            //       i.item_flag,
            //       s.*
            //FROM dbo.ic_t_branch_stock s
            //LEFT JOIN dbo.bi_t_item_info i ON i.item_no = s.item_no
            //LEFT JOIN dbo.rp_t_batch_flow b ON b.batch_no = s.batch_no
            //WHERE s.stock_qty>0 AND
            //s.batch_no IN
            //      (
            //          SELECT d.batch_no
            //          FROM dbo.ic_t_inout_store_detail d
            //              LEFT JOIN dbo.ic_t_inout_store_master m
            //                  ON m.sheet_no = d.sheet_no
            //          WHERE m.approve_flag = '1'
            //                      AND m.oper_date BETWEEN '{startTime.Toyyyy_MM_ddStart()}' AND '{endTime.Toyyyy_MM_ddEnd()}'
            //          UNION
            //          SELECT d.batch_no
            //          FROM dbo.sm_t_salesheet_detail d
            //              LEFT JOIN dbo.sm_t_salesheet m
            //                  ON m.sheet_no = d.sheet_no
            //          WHERE m.approve_flag = '1'
            //                      AND m.oper_date BETWEEN '{startTime.Toyyyy_MM_ddStart()}' AND '{endTime.Toyyyy_MM_ddEnd()}'
            //      );";
            string sql = $@"SELECT '1' select_flag, b.voucher_no, i.item_name, i.unit_no, i.item_flag, s.*
FROM dbo.ic_t_batch_branch_stock s
LEFT JOIN dbo.bi_t_item_info i
  ON i.item_no = s.item_no
LEFT JOIN dbo.rp_t_batch_flow b
  ON b.batch_no = s.batch_no
WHERE  s.stock_qty != '0'
      AND s.stock_qty <= '{minStock}';";
            DataTable tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        public void BatchWriteOffSheet(DataTable lowTable, DataTable writeTable, decimal minStock)
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                InOutBLL inOutBll = new InOutBLL();

                {
                    //处理低数量出库，对冲报溢入库
                    Dictionary<string, string> batchDic = new Dictionary<string, string>();
                    var groupBy = lowTable.AsEnumerable().GroupBy(t => t["branch_no"].ToString());
                    foreach (var g in groupBy)
                    {
                        string branch_no = g.Key;
                        var items = g.GroupBy(t => t["item_no"].ToString());

                        {
                            List<body.ic_t_inout_store_detail> lines = new List<body.ic_t_inout_store_detail>();
                            ic_t_inout_store_master ord = new ic_t_inout_store_master
                            {
                                sheet_no = inOutBll.MaxCode(db, "OO"),
                                trans_no = "07",
                                branch_no = branch_no,
                                db_no = "+",
                                voucher_no = "",
                                supcust_no = "",
                                pay_way = "",
                                discount = 1,
                                coin_no = "RMB",
                                tax_amount = 0,
                                oper_date = DateTime.Now,
                                oper_id = "1001",
                                deal_man = "00",
                                approve_flag = "0",
                                approve_man = "00",
                                approve_date = System.DateTime.MinValue,
                                pay_date = System.DateTime.MinValue,
                                update_time = DateTime.Now,
                                create_time = DateTime.Now,
                                display_flag = "1",
                            };

                            int sheet_sort = 0;
                            foreach (var item in items)
                            {
                                body.ic_t_inout_store_detail line = new body.ic_t_inout_store_detail()
                                {
                                    sheet_no = ord.sheet_no,
                                    item_no = item.Key,
                                    item_name = item.First()["item_name"].ToString(),
                                    in_qty = item.Sum(t => t["real_qnty"].ToDecimal()),
                                    unit_factor = 1,
                                    unit_no = item.First()["unit_no"].ToString(),
                                    sheet_sort = ++sheet_sort,
                                };
                                lines.Add(line);
                            }

                            SaveInSheetBatch(d, ord, lines);

                            d.Insert(ord);
                            lines.ForEach(l =>
                            {
                                batchDic.Add($@"{ord.branch_no}-{l.item_no}", l.batch_num);
                                l.flow_id = d.ExecuteScalar($@"select isnull(max(flow_id),0) + 1 as flow_id from ic_t_inout_store_detail", null).ToInt64();
                                d.Insert(l);
                            });

                            CheckSheet check = new CheckSheet();
                            check.CheckOOSheet(d, ord, lines, "1001");

                        }


                    }

                    //低数量出库分配批次
                    foreach (DataRow row in lowTable.Rows)
                    {
                        string branch_no = row["branch_no"].ToString();
                        string type = row["type"].ToString();
                        string item_no = row["item_no"].ToString();
                        decimal real_qnty = row["real_qnty"].ToDecimal();
                        decimal flow_id = row["flow_id"].ToDecimal();

                        if (!batchDic.TryGetValue(branch_no + "-" + item_no, out string batch_no))
                        {
                            continue;
                        }

                        switch (type)
                        {
                            case "SO":
                                d.ExecuteScalar($@"UPDATE dbo.sm_t_salesheet_detail SET batch_num='{batch_no}' WHERE flow_id='{flow_id}'", null);
                                break;
                            default:
                                d.ExecuteScalar($@"UPDATE dbo.ic_t_inout_store_detail SET batch_num='{batch_no}' WHERE flow_id='{flow_id}'", null);
                                break;
                        }
                        d.ExecuteScalar($@"UPDATE ic_t_batch_branch_stock SET stock_qty=stock_qty-{real_qnty} WHERE batch_no='{batch_no}'", null);
                    }
                }

                {
                    //分配待定批次明细
                    string saleIds = "'0'";
                    string detailIds = "'0'";
                    foreach (DataRow row in writeTable.Rows)
                    {
                        string type = row["type"].ToString();
                        decimal flow_id = row["flow_id"].ToDecimal();
                        switch (type)
                        {
                            case "SO":
                                saleIds += $@",'{flow_id}'";
                                break;
                            default:
                                detailIds += $@",'{flow_id}'";
                                break;
                        }
                    }

                    BatchWriteOff(d, saleIds, detailIds, minStock);
                }


                db.CommitTran();
            }
            catch (Exception e)
            {
                db.RollBackTran();
                LogHelper.writeLog(e);
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        public void BatchWriteOffBatch(DataTable batchTable)
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                InOutBLL inOutBll = new InOutBLL();

                if (batchTable.Rows.Count > 0)
                {
                    var groupBy = batchTable.AsEnumerable().GroupBy(t => t["branch_no"].ToString());
                    foreach (var g in groupBy)
                    {
                        string branch_no = g.Key;

                        List<body.ic_t_inout_store_detail> lines = new List<body.ic_t_inout_store_detail>();
                        ic_t_inout_store_master ord = new ic_t_inout_store_master
                        {
                            sheet_no = inOutBll.MaxCode(db, "OO"),
                            trans_no = "05",
                            branch_no = branch_no,
                            db_no = "-",
                            voucher_no = "",
                            supcust_no = "",
                            pay_way = "",
                            discount = 1,
                            coin_no = "RMB",
                            tax_amount = 0,
                            oper_date = DateTime.Now,
                            oper_id = "1001",
                            deal_man = "00",
                            approve_flag = "0",
                            approve_man = "00",
                            approve_date = System.DateTime.MinValue,
                            pay_date = System.DateTime.MinValue,
                            update_time = DateTime.Now,
                            create_time = DateTime.Now,
                            display_flag = "1",
                        };

                        int sheet_sort = 0;
                        foreach (var row in g)
                        {
                            body.ic_t_inout_store_detail line = new body.ic_t_inout_store_detail()
                            {
                                sheet_no = ord.sheet_no,
                                item_no = row["item_no"].ToString(),
                                item_name = row["item_name"].ToString(),
                                in_qty = row["stock_qty"].ToDecimal(),
                                unit_factor = 1,
                                unit_no = row["unit_no"].ToString(),
                                batch_num = row["batch_no"].ToString(),
                                sheet_sort = ++sheet_sort,
                            };
                            lines.Add(line);
                        }

                        d.Insert(ord);
                        lines.ForEach(l =>
                        {
                            l.flow_id = d.ExecuteScalar($@"select isnull(max(flow_id),0) + 1 as flow_id from ic_t_inout_store_detail", null).ToInt64();
                            d.Insert(l);
                        });

                        CheckSheet check = new CheckSheet();
                        check.CheckOOSheet(d, ord, lines, "1001");
                    }

                }

                db.CommitTran();
            }
            catch (Exception e)
            {
                db.RollBackTran();
                LogHelper.writeLog(e);
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        #endregion

    }
}