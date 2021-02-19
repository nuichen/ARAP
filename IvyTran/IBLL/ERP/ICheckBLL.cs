using System;
using System.Collections.Generic;
using System.Data;
using Model;

namespace IvyTran.IBLL.ERP
{
    public interface ICheckBLL
    {
        void CheckSupSupItemPrice(string item_no, string sup_no, string oper_id, DateTime date);
        System.Data.DataTable GetCheckSheetList(string date1, string date2);
        System.Data.DataTable GetCheckInitList(string branch_no);//--------------------
        //------------------My Add Code-------------------------------------
        //System.Data.DataTable GetCheckInitListByBranch_no(string );
        //-------------------------------------------------------
        System.Data.DataTable GetBranchStockList(string branch_no);
        void GetCheckSheet(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2);
        void AddCheckSheet(Model.ic_t_check_master ord, List<Model.ic_t_check_detail> lines, out string sheet_no);
        void ChangeCheckSheet(Model.ic_t_check_master ord, List<Model.ic_t_check_detail> lines);
        void DeleteCheckSheet(string sheet_no, DateTime update_time);

        void CheckPDSheet(string sheet_no);
        void CheckPCSheet(ic_t_check_init ini);

        void AddChectInitSheet(ic_t_check_init check_init, out string sheet_no);
        void UpdateChectInitSheet(ic_t_check_init init);
        void DeleteChectInitSheet(ic_t_check_init init);
        DataTable GetCheckInitSheet(ic_t_check_init init);

        DataTable GetCheckBak(ic_t_check_bak bak);
        DataTable GetCheckItemBak(ic_t_check_bak bak);

        DataTable GetCheckFinish(ic_t_check_finish finish);

        void UpdateCheckFinish(List<ic_t_check_finish> finishs);
        void CreateUnCheckSheet(string sheet_no);

    }
}
