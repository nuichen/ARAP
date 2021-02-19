using System;
using System.Collections.Generic;
using System.Data;
using DB;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;

namespace IvyTran.BLL.ERP
{
    public class BomBll : IBom
    {
        DB.IDB d = new DB.DBByAutoClose(AppSetting.conn);
        public DataTable GetProcessItem()
        {
            string sql = $@"SELECT item_subno,item_name
FROM dbo.bi_t_item_info
WHERE process_type <> 0";
            DataTable tb = d.ExecuteToTable(sql, null);
            return tb;
        }

        public DataTable GetProcessItem(string keyword)
        {
//            string sql = $@"SELECT i.*
//FROM dbo.bi_t_item_info i
//    LEFT JOIN dbo.bi_t_bom_master m
//        ON m.bom_no = i.item_bom
//WHERE i.process_type <> 0
//      AND
//      (
//          i.item_subno LIKE '%%'
//          OR i.item_name LIKE '%%'
//      )
//      AND i.item_bom IS NOT NULL
//      AND i.item_bom <> ''
//      AND m.bom_no IS NOT NULL;";
            string sql = @"SELECT i.*
FROM dbo.bi_t_item_info i
    LEFT JOIN dbo.bi_t_bom_master m
        ON m.bom_no = i.item_bom where 1=1";
            int a = 0;
            if (int.TryParse(keyword, out a))
            {
                sql += @" and i.item_name LIKE '%" + keyword +
                       "%')  and i.item_bom IS NOT NULL and m.bom_no IS NOT NULL AND i.item_bom <> ''";
            }
            else
            {
                sql += @" and i.item_subno LIKE '%" + keyword + "%'  and i.item_bom IS NOT NULL and m.bom_no IS NOT NULL AND i.item_bom <> ''";
            }
            
            DataTable tb = d.ExecuteToTable(sql, null);
            return tb;
        }

        public DataTable GetItemBomDetails(string bom_no)
        {
            string sql = $@"SELECT COUNT(1) FROM dbo.bi_t_bom_master WHERE bom_no='{bom_no}'";
            int count = d.ExecuteScalar(sql, null).ToInt32();
            if (count < 1)
            {
                throw new Exception("Bom结构不存在或已删除");
            }
            sql = $@"SELECT d.flow_id,
       d.item_no,
       i.item_subno,
       i.item_name,
       d.qty,
       d.unit_no,
       d.loss_rate,
       d.bom_no
FROM dbo.bi_t_bom_detail d
    LEFT JOIN dbo.bi_t_item_info i
        ON i.item_no = d.item_no
WHERE d.bom_no = '{bom_no}';";
            DataTable tb = d.ExecuteToTable(sql, null);
            return tb;
        }

        public void GetItemBom(string bom_no, out bi_t_item_info item, out DataTable bomDetails)
        {
            string sql = $@"SELECT COUNT(1) FROM dbo.bi_t_bom_master WHERE bom_no='{bom_no}'";
            int count = d.ExecuteScalar(sql, null).ToInt32();
            if (count < 1)
            {
                throw new Exception("Bom结构不存在或已删除");
            }

            sql = $@"SELECT * FROM dbo.bi_t_item_info WHERE item_bom='{bom_no}'";
            item = d.ExecuteToModel<bi_t_item_info>(sql, null);

            sql = $@"SELECT d.flow_id,
       d.bom_no,
       d.item_no,
	   i.item_subno,
	   i.item_name,
       d.qty,
       d.unit_no,
       d.loss_rate 
FROM dbo.bi_t_bom_detail d
LEFT JOIN dbo.bi_t_item_info i ON i.item_no = d.item_no
WHERE d.bom_no='{bom_no}'";
            bomDetails = d.ExecuteToTable(sql, null);

        }

        public void GetItemBomByItem(string item_subno, out bi_t_item_info item, out DataTable bomDetails)
        {
            string sql = $@"SELECT *  FROM dbo.bi_t_item_info WHERE item_no='{item_subno}' OR item_subno='{item_subno}'";
            item = d.ExecuteToModel<bi_t_item_info>(sql, null);
            if (item == null || string.IsNullOrWhiteSpace(item.item_no))
            {
                throw new Exception("产品不存在或已删除");
            }

            sql = $@"SELECT d.flow_id,
       d.bom_no,
       d.item_no,
       i.item_subno,
       i.item_name,
       d.qty,
       d.unit_no,
       d.loss_rate
FROM dbo.bi_t_bom_detail d
    LEFT JOIN dbo.bi_t_item_info i
        ON i.item_no = d.item_no
WHERE d.bom_no = '{item.item_bom}';";
            bomDetails = d.ExecuteToTable(sql, null);
        }

        string MaxMainCode(IDB d)
        {
            string sql = @"SELECT ISNULL(MAX(bom_no),0) FROM dbo.bi_t_bom_master";
            int id = d.ExecuteScalar(sql, null).ToInt32();
            id += 1;
            string flow_id = id.ToString().PadLeft(6, '0');

            return flow_id;
        }

        int MaxDelatilCode(IDB d)
        {
            string sql = "SELECT ISNULL(MAX(flow_id),0)+1 FROM dbo.bi_t_bom_detail";
            return d.ExecuteScalar(sql, null).ToInt32();
        }

        public void SaveItemBom(string oper_id, bi_t_item_info item, List<bi_t_bom_detail> details)
        {
            string sql = "";
            var db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                bi_t_bom_master master = d.ExecuteToModel<bi_t_bom_master>($@"SELECT * FROM dbo.bi_t_bom_master WHERE bom_no='{item.item_bom}'", null);
                if (string.IsNullOrWhiteSpace(item.item_bom) || master == null || string.IsNullOrWhiteSpace(master.bom_no))
                {
                    //新增
                    master = new bi_t_bom_master()
                    {
                        bom_no = MaxMainCode(d),
                        bom_name = item.item_name,
                        approve_flag = "1",
                        oper_id = oper_id,
                        oper_date = DateTime.Now,
                        update_time = DateTime.Now
                    };
                    d.Insert(master);

                    item.item_bom = master.bom_no;
                    d.Update(item, "item_no", "item_bom,item_property,process_type,is_mrp");
                }
                else
                {
                    //修改
                    d.ExecuteScalar($@"UPDATE dbo.bi_t_bom_master SET update_time =GETDATE() WHERE bom_no='{master.bom_no}'", null);

                    item.item_bom = master.bom_no;
                    d.Update(item, "item_no", "item_bom,item_property,process_type,is_mrp");

                    d.ExecuteScalar($@"DELETE FROM dbo.bi_t_bom_detail WHERE bom_no='{master.bom_no}'", null);
                }

                foreach (var detail in details)
                {
                    if (string.IsNullOrWhiteSpace(detail.item_no))
                    {
                        continue;
                    }
                    detail.flow_id = MaxDelatilCode(d);
                    detail.bom_no = master.bom_no;
                    d.Insert(detail);
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

        public void DelBoms(string bomNos)
        {
            string sql = "";
            var db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                d.ExecuteScalar($@"DELETE FROM dbo.bi_t_bom_master WHERE bom_no IN ('',{bomNos})", null);

                d.ExecuteScalar($@"DELETE FROM dbo.bi_t_bom_detail WHERE bom_no IN ('',{bomNos})", null);

                d.ExecuteScalar($@"UPDATE dbo.bi_t_item_info SET item_bom=NULL WHERE item_bom IN ('',{bomNos})", null);

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
    }
}