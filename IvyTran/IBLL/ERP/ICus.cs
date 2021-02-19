using System.Collections.Generic;
using System.Data;
 
using Model;
using Model.BaseModel;

namespace IvyTran.IBLL.ERP
{
    interface ICus
    {
        DataTable GetSendType();
        DataTable GetCarId(string id);
        DataTable GetCar();
        DataTable GetCar(string id);
        void DeleteCar(string car_id);
        void AddCar(bi_t_supcust_car car);
        void ChangeCar(bi_t_supcust_car car);
        DataTable SupCustType(string flag);
        bool UpdateCustType(DataTable tb1);
        DataTable CustType(string cust_name, string cust_type, string flag,string region);
        System.Data.DataTable GetList(string region_no, string keyword, string is_jail, int show_stop, int page_index, int page_size, out int total_count);
        System.Data.DataTable GetItem(string supcust_no);
        string MaxCode();
        void Add(bi_t_supcust_info item);
        void Adds(List<Model.bi_t_supcust_info> supcustInfos);
        void Change(bi_t_supcust_info item);
        void Delete(string supcust_no);

        DataTable QuickSearchList(string keyword);


        DataTable GetCustItemList(string date1, string date2, string cust_no);
        void GetCustItem(string sheet_no, out DataTable tb1, out DataTable tb2);
        void AddCustItem(Model.rp_t_cust_item_master ord, List<Model.rp_t_cust_item_detail> lines, out string sheet_no);
        void ChangeCustItem(Model.rp_t_cust_item_master ord, List<Model.rp_t_cust_item_detail> lines);
        void DeleteCustItem(string sheet_no);
        void CheckCustItem(string sheet_no, string approve_man);
    }
}
