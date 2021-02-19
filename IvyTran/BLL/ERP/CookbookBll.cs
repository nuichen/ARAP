using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DB;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;
using Model.StaticType;

namespace IvyTran.BLL.ERP
{
    public class CookbookBll : ICookbook
    {
        DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
        public DataTable GetCookbooks(string formulaName, string itemName, string type, string formulaClsNo)
        {
            string sql = $@"SELECT DISTINCT
       m.formula_no,
       m.formula_name,
	   m.formula_clsno,
	  CASE WHEN  c.formula_clsname IS NULL THEN '不定' ELSE c.formula_clsname END formula_clsname,
       m.price,
       m.display_flag,
       m.formula_type,
       m.oper_id,
       m.oper_date,
       m.formula_pro
FROM dbo.bi_t_formula_main m
    LEFT JOIN dbo.bi_t_formula_detail d
        ON d.formula_no = m.formula_no
    LEFT JOIN dbo.bi_t_item_info i
        ON i.item_no = d.item_no
    LEFT JOIN dbo.bi_t_formula_cls c
        ON c.formula_clsno = m.formula_clsno
WHERE m.formula_name LIKE '%{formulaName}%'
      AND i.item_name LIKE '%{itemName}%'  ";

            if (!string.IsNullOrEmpty(type))
            {
                sql += $@"  AND m.formula_type = '{type}' ";
            }

            if (!string.IsNullOrEmpty(formulaClsNo))
            {
                sql += $@"  AND m.formula_clsno = '{formulaClsNo}' ";
            }

            sql += "ORDER BY m.formula_name";

            DataTable tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        public DataTable GetCookbookDetail(string formulaNo)
        {
            string sql = $@" SELECT d.flow_id,
       d.formula_no,
       d.item_no,
	   i.item_subno,
       i.item_name,
       d.qty,
       d.price,
       d.unit_no,
       d.loss_rate
FROM dbo.bi_t_formula_detail d
    LEFT JOIN dbo.bi_t_item_info i
        ON i.item_no = d.item_no
WHERE d.formula_no = '{formulaNo}'; ";
            DataTable tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count < 1)
            {
                throw new Exception("该食谱不存在、或已被删除");
            }
            return tb;
        }

        public DataTable SearchCookbook(string keyword)
        {
            string sql = $@"SELECT m.formula_no,
       m.formula_name,
       m.price,
       m.display_flag,
       m.formula_type,
       m.oper_id,
       m.oper_date,
       m.formula_pro
FROM dbo.bi_t_formula_main m
WHERE m.formula_no LIKE '%{keyword}%' OR m.formula_name LIKE '%{keyword}%' ";
            DataTable tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        public void GetCookbookDetails(string formulaNo, out bi_t_formula_main formulaMain, out DataTable formulaDetails)
        {
            string sql = $@" SELECT *
FROM dbo.bi_t_formula_main m
WHERE m.formula_no = '{formulaNo}' ";

            formulaMain = db.ExecuteToModel<bi_t_formula_main>(sql, null);

            if (formulaMain == null || string.IsNullOrEmpty(formulaMain.formula_no))
            {
                throw new Exception("该食谱不存在、或已被删除");
            }

            sql = $@"SELECT d.flow_id,
       d.formula_no,
       d.item_no,
	   i.item_subno,
	   i.item_name,
       d.qty,
       d.price,
       d.unit_no,
       d.loss_rate
FROM dbo.bi_t_formula_detail d
LEFT JOIN dbo.bi_t_item_info i ON i.item_no = d.item_no
WHERE d.formula_no = '{formulaNo}' ";

            formulaDetails = db.ExecuteToTable(sql, null);
        }

        string MaxMainCode(IDB d)
        {
            string sql = @"SELECT ISNULL(MAX(formula_no),0) FROM dbo.bi_t_formula_main";
            int id = d.ExecuteScalar(sql, null).ToInt32();
            id += 1;
            string flow_id = id.ToString().PadLeft(6, '0');

            return flow_id;
        }

        int MaxDelatilCode(IDB d)
        {
            string sql = "SELECT ISNULL(MAX(flow_id),0)+1 FROM dbo.bi_t_formula_detail";
            return d.ExecuteScalar(sql, null).ToInt32();
        }

        public void SaveCookbook(bi_t_formula_main formulaMain, List<bi_t_formula_detail> formulaDetails)
        {
            var db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                if (string.IsNullOrEmpty(formulaMain.formula_no))
                {
                    //新增
                    formulaMain.formula_no = MaxMainCode(d);
                    formulaMain.oper_date = DateTime.Now;
                    d.Insert(formulaMain);

                }
                else
                {
                    //修改
                    formulaMain.oper_date = DateTime.Now;
                    d.Update(formulaMain, "formula_no");

                    d.ExecuteScalar($@"DELETE FROM dbo.bi_t_formula_detail WHERE formula_no='{formulaMain.formula_no}'", null);
                }

                foreach (var detail in formulaDetails)
                {
                    detail.flow_id = MaxDelatilCode(d);
                    detail.formula_no = formulaMain.formula_no;
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

        public void ImportCookbook(DataTable tb)
        {
            string sql = "";
            var db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                var itemInfos = new Dictionary<string, bi_t_item_info>();
                var formulaClss = new Dictionary<string, bi_t_formula_cls>();
                {
                    var dt = d.ExecuteToTable("SELECT * FROM dbo.bi_t_item_info", null);
                    foreach (DataRow row in dt.Rows)
                    {
                        bi_t_item_info itemInfo = row.DataRowToModel<bi_t_item_info>();
                        itemInfos.Add(itemInfo.item_subno, itemInfo);
                        if (!itemInfos.TryGetValue(itemInfo.item_name, out _))
                        {
                            itemInfos.Add(itemInfo.item_name, itemInfo);
                        }
                    }

                    dt = d.ExecuteToTable("SELECT * FROM dbo.bi_t_formula_cls", null);
                    foreach (DataRow row in dt.Rows)
                    {
                        bi_t_formula_cls item = row.DataRowToModel<bi_t_formula_cls>();
                        formulaClss.Add(item.formula_clsno, item);
                        if (!formulaClss.TryGetValue(item.formula_clsname, out _))
                        {
                            formulaClss.Add(item.formula_clsname, item);
                        }
                    }
                }

                for (var index = tb.Rows.Count - 1; index >= 0; index--)
                {
                    DataRow row = tb.Rows[index];
                    string item_subno = row["原料名称/货号"].ToString();
                    string formulaCls = row["菜品分类"].ToString();
                    if (string.IsNullOrWhiteSpace(item_subno))
                    {
                        tb.Rows.RemoveAt(index);
                    }
                    else
                    {
                        if (!itemInfos.TryGetValue(item_subno, out _))
                        {
                            throw new Exception($"没有找到：{item_subno}");
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(formulaCls) && !formulaClss.TryGetValue(formulaCls, out _))
                    {
                        throw new Exception($"没有找到分类：{formulaCls}");
                    }
                }

                bi_t_formula_main formula = null;
                foreach (DataRow row in tb.Rows)
                {
                    string formulaName = row["菜品名称"].ToString();
                    if (formula == null || !formula.formula_no.Equals(formulaName) && !string.IsNullOrWhiteSpace(formulaName))
                    {
                        formula = new bi_t_formula_main()
                        {
                            formula_no = MaxMainCode(d),
                            formula_name = row["菜品名称"].ToString(),
                            formula_clsno = formulaClss[row["菜品分类"].ToString()].formula_clsno,
                            price = row["价格"].ToDecimal(),
                            oper_date = DateTime.Now,
                            display_flag = "1",
                            oper_id = "1001"
                        };

                        var type = CookbookStaticType.Examples.FormulaTypeDic.FirstOrDefault(e => e.Value.Equals(row["类型"].ToString()));
                        formula.formula_type = default(KeyValuePair<string, string>).Equals(type) ? "1" : type.Key;

                        string useTypeString = row["属性"].ToString();
                        foreach (var key in CookbookStaticType.Examples.UseTypeDic)
                        {
                            if (useTypeString.Contains(key.Value))
                            {
                                if (!string.IsNullOrWhiteSpace(formula.formula_pro))
                                {
                                    formula.formula_pro += ",";
                                }
                                formula.formula_pro += key.Key;
                            }
                        }

                        d.Insert(formula);
                    }

                    bi_t_item_info itemInfo = itemInfos[row["原料名称/货号"].ToString()];

                    d.Insert(new bi_t_formula_detail()
                    {
                        flow_id = MaxDelatilCode(d),
                        formula_no = formula.formula_no,
                        item_no = itemInfo.item_no,
                        qty = row["数量"].ToDecimal(),
                        price = row["单价"].ToDecimal(),
                        loss_rate = row["损耗率(%)"].ToDecimal(),
                        unit_no = itemInfo.formula_unit_no
                    });

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

        public void DelCookbook(string formulaNos)
        {
            string sql = "";
            var db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                sql = $"DELETE FROM dbo.bi_t_formula_main WHERE formula_no IN ('',{formulaNos})";
                d.ExecuteScalar(sql, null);

                sql = $"DELETE FROM dbo.bi_t_formula_detail WHERE formula_no IN ('',{formulaNos})";
                d.ExecuteScalar(sql, null);

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


        public DataTable GetCookbookClss(string formulaClsNo, int length)
        {
            string sql = $@" SELECT * FROM dbo.bi_t_formula_cls WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(formulaClsNo))
            {
                sql += $@" AND formula_clsno LIKE '{formulaClsNo}%' ";
            }

            if (length > 0)
            {
                sql += $@" AND LEN(formula_clsno)='{length}' ";
            }

            DataTable tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        public List<bi_t_formula_cls> GetCookbookClsLis(string formulaClsNo, int length)
        {
            string sql = $@" SELECT * FROM dbo.bi_t_formula_cls WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(formulaClsNo))
            {
                sql += $@" AND formula_clsno LIKE '{formulaClsNo}%' ";
            }

            if (length > 0)
            {
                sql += $@" AND LEN(formula_clsno)='{length}' ";
            }

            var lis = db.ExecuteToList<bi_t_formula_cls>(sql, null);
            return lis;
        }

        public bi_t_formula_cls GetCookbookCls(string formulaClsNo)
        {
            string sql = $@"SELECT * FROM dbo.bi_t_formula_cls WHERE formula_clsno = '{formulaClsNo}';";
            return db.ExecuteToModel<bi_t_formula_cls>(sql, null);
        }

        public string MaxCookbookClsCode(string par_code)
        {
            string sql = "select top 1  formula_clsno from bi_t_formula_cls where formula_clsno like '" + par_code + "%' and len(formula_clsno)=" + (par_code.Length + 2) + " order by formula_clsno desc ";

            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                var val = tb.Rows[0]["formula_clsno"].ToString();
                val = val.Substring(par_code.Length);
                int index = Helper.Conv.ToInt16(val);
                index++;
                return par_code + index.ToString().PadLeft(2, '0');
            }
            else
            {
                return par_code + "01";
            }
        }

        public bi_t_formula_cls Add(string parCode, bi_t_formula_cls item)
        {
            item.formula_clsno = MaxCookbookClsCode(parCode);
            item.display_flag = "1";
            item.update_time = DateTime.Now;
            db.Insert(item);
            return item;
        }

        public bi_t_formula_cls Change(bi_t_formula_cls item)
        {
            db.Update(item, "formula_clsno");
            return item;
        }

        public void Delete(string formula_clsno)
        {
            var tb = db.ExecuteToTable($@"SELECT * FROM dbo.bi_t_formula_cls WHERE formula_clsno LIKE '{formula_clsno}%' AND formula_clsno <> '{formula_clsno}'", null);
            if (tb.Rows.Count > 0)
            {
                throw new Exception("存在子分类，不允许删除");
            }

            var count = db.ExecuteScalar($@"SELECT COUNT(1) FROM dbo.bi_t_formula_main WHERE formula_no = '{formula_clsno}'", null).ToInt32();
            if (count > 0)
            {
                throw new Exception("当前分类已绑定菜谱，不允许删除");
            }

            db.ExecuteScalar($@"DELETE FROM dbo.bi_t_formula_cls WHERE formula_clsno='{formula_clsno}'", null);
        }
    }
}