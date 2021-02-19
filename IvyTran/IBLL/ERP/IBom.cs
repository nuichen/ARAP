using System.Collections.Generic;
using System.Data;
using Model;

namespace IvyTran.IBLL.ERP
{
    public interface IBom
    {
        DataTable GetProcessItem();
        DataTable GetProcessItem(string keyword);
        DataTable GetItemBomDetails(string bom_no);
        void GetItemBom(string bom_no, out bi_t_item_info item, out DataTable bomDetails);
        void GetItemBomByItem(string item_subno, out bi_t_item_info item, out DataTable bomDetails);
        void SaveItemBom(string oper_id, bi_t_item_info item, List<bi_t_bom_detail> details);
        void DelBoms(string bomNos);
    }
}