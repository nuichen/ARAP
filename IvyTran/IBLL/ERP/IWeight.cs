using System.Collections.Generic;
using System.Data;
using Model;

namespace IvyTran.IBLL.ERP
{
    public interface IWeight
    {
        DataTable GetCoOrderMain(string last_req);    //取出送货日期为当天以及之后的销售订单单头

        DataTable GetCoOrderMainNew(string last_req);

        DataTable GetCoOrderDetail(string valid_date);    //取出送货日期为当天以及之后的销售订单明细
        DataTable GetCoOrderDetailNew();


        DataTable GetSystemPars();  //获取系统参数

        DataTable GetOperList();

        DataTable GetPiWeightList();

        DataTable GetItemClsList();

        DataTable GetItemList(string sys_time);

        DataTable GetItemPoList(string sys_time);

        DataTable GetSupCusList(string sys_time);

        void UploadWeighing(List<ot_weighing> lines);

        void UploadCheck(List<ot_check_flow> lines);




    }
}