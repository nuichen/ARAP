using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace IvyBack.IBLL
{
    public interface ICheckBLL
    {
        DataTable GetCheckSheetList(DateTime date1, DateTime date2);
        DataTable GetCheckInitList(string branch_no);
        //---------------------My Add Code-------------------------
        //DataTable GetCheckInitListByBranch_no(string branch_no);
        //----------------------------------------------

        DataTable GetBranchStockList(string branch_no);
        void GetCheckSheet(string sheet_no, out DataTable tb1, out DataTable tb2);
        void AddCheckSheet(Model.ic_t_check_master ord, List<Model.ic_t_check_detail> lines, out string sheet_no);
        void ChangeCheckSheet(Model.ic_t_check_master ord, List<Model.ic_t_check_detail> lines);
        void DeleteCheckSheet(string sheet_no, DateTime update_time);

        void CheckPDSheet(string sheet_no);
        void CheckPCSheet(ic_t_check_init ini);

        void AddChectInitSheet(ic_t_check_init check_init, out string sheet_no);
        void UpdateChectInitSheet(ic_t_check_init init);
        void DeleteChectInitSheet(ic_t_check_init init);
        ic_t_check_init GetCheckInitSheet(ic_t_check_init init);
        DataTable GetCheckInitSheets(ic_t_check_init init);

        DataTable GetCheckBak(ic_t_check_bak bak);
        DataTable GetCheckItemBak(ic_t_check_bak bak );

        DataTable GetCheckFinish(ic_t_check_finish finish);

        void UpdateCheckFinish(List<ic_t_check_finish> finishs);

        DataTable GetAllCheckItemBak(ic_t_check_bak bak);
        void CreateUnCheckSheet(string sheet_no);


    }
}
