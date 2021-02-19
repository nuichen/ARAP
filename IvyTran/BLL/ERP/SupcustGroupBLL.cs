using System;
using System.Collections.Generic;
using System.Data;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;
using Model.BaseModel;

namespace IvyTran.BLL.ERP
{
    public class SupcustGroupBLL : ISupcustGroup
    {
        DataTable ISupcustGroup.GetAll()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = @"SELECT * FROM dbo.bi_t_supcust_group
ORDER BY SupCust_Flag,SupCust_GroupNo";

            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        DataTable ISupcustGroup.GetCusGroup()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = @"SELECT * FROM dbo.bi_t_supcust_group
WHERE SupCust_Flag='1'";

            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        DataTable ISupcustGroup.GetSupGroup()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = @"SELECT * FROM dbo.bi_t_supcust_group
WHERE SupCust_Flag='2'";

            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        void ISupcustGroup.SaveGroup(List<bi_t_supcust_group> lis)
        {
            DB.DBByHandClose db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                if(lis[0].SupCust_Flag=="1")
                d.ExecuteScalar("delete from dbo.bi_t_supcust_group where SupCust_Flag='1'", null);
                else
                    d.ExecuteScalar("delete from dbo.bi_t_supcust_group where SupCust_Flag='2'", null);
               
                foreach (bi_t_supcust_group group in lis)
                {
                        d.Insert(group);
                  
                }
                if (0 == Conv.ToDecimal(d.ExecuteScalar("select count(1) from bi_t_supcust_group where SupCust_GroupNo='00' and SupCust_Flag='"+lis[0].SupCust_Flag+"'", null)))
                {
                    bi_t_supcust_group defaultType = new bi_t_supcust_group()
                    {
                        SupCust_GroupNo = "00",
                        SupCust_GroupName = "不定",
                        SupCust_Flag = lis[0].SupCust_Flag,
                    };
                    d.Insert(defaultType);
                    //bi_t_supcust_group defaultType1 = new bi_t_supcust_group()
                    //{
                    //    SupCust_GroupNo = "00",
                    //    SupCust_GroupName = "不定",
                    //    SupCust_Flag = "2",
                    //};
                    //d.Insert(defaultType1);
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
        DataTable ISupcustGroup.GetAllCls(string status)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = @"SELECT * 
FROM [dbo].[bi_t_company_type]
WHERE 1=1 ";

            if ("1".Equals(status))
            {
                sql += " and display='1' ";
            }
            else
            {
                sql += " and display='2' ";
            }

            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        void ISupcustGroup.SaveCls(List<bi_t_company_type> lis)
        {
            DB.DBByHandClose db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                if(lis[0].display=="1")
                d.ExecuteScalar("delete from dbo.bi_t_company_type where display='1'", null);
                else
                    d.ExecuteScalar("delete from dbo.bi_t_company_type where display='2'", null);

                foreach (bi_t_company_type type in lis)
                {
                    type.update_time = DateTime.Now;
                    d.Insert(type);
                }

                if (0 == Conv.ToDecimal(d.ExecuteScalar("select count(1) from bi_t_company_type where type_no='00' and display='"+lis[0].display+"'", null)))
                {
                    bi_t_company_type defaultType = new bi_t_company_type()
                    {
                        type_no = "00",
                        type_name = "不定",
                        display = lis[0].display,
                        update_time = DateTime.Now,
                    };
                    d.Insert(defaultType);
                    //bi_t_company_type defaultType1 = new bi_t_company_type()
                    //{
                    //    type_no = "00",
                    //    type_name = "不定",
                    //    display = "2",
                    //    update_time = DateTime.Now,
                    //};
                    //d.Insert(defaultType1);
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
    }
}