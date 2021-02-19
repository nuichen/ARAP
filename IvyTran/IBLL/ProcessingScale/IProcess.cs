using System.Collections.Generic;
using System.Data;
using Model;

namespace IvyTran.IBLL.ProcessingScale
{
    public interface IProcess
    {
        DataTable Getco_t_order_main();
        DataTable Getco_t_order_child(string sheet_no);

        //计划领料
        DataTable Getot_processing_task(string ph_sheet_no);
        //bom 单个详情
        DataTable Getbi_t_bom_detail();
        //bom 总单
        DataTable Getbi_t_bom_master(string last_time);
        //商品详情
        DataTable Getbi_t_item_info(string last_time);
        //商品分类
        DataTable Getbi_t_item_cls(string last_time);
        //添加明细
        int InsertProcess(List<ot_processing> list);
        DataTable Getic_t_pspc_main();
        DataTable GetOperList();
        DataTable GetParamsList();
    }
}
