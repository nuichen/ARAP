using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using IvyBack.Helper;
using System.Data;
using Model.BaseModel;


namespace IvyBack.IBLL
{
    public interface ICus
    {
        DataTable SendType();
        DataTable GetCarId(string id);
        DataTable GetCar();
        DataTable GetCar(string id);
        void ChangeCar(bi_t_supcust_car car);
        void AddCar(bi_t_supcust_car car);
        void DeleteCar(string car_id);
        DataTable SupCustType(string flag);
        bool Supcust_type(DataTable dt);
        DataTable Supcust_type(string sup_name, string flag, string type, string region);
        DataTable GetDataTable(string region_no, string keyword, int show_stop, int page_index, int page_size, out int total_count);
        Page<bi_t_supcust_info> GetDataTable(string region_no, string keyword, int show_stop, Page<bi_t_supcust_info> page);

        bi_t_supcust_info GetItem(string supcust_no);

        string GetMaxCode();

        void Add(bi_t_supcust_info sup);
        void Adds(List<bi_t_supcust_info> supcustInfos);
        void Update(bi_t_supcust_info sup);
        void Del(bi_t_supcust_info sup);

        DataTable QuickSearchList(string keyword);


        DataTable GetCustItemList(DateTime date1, DateTime date2, string cust_id);
        void GetCustItem(string sheet_no, out DataTable tb1, out DataTable tb2);
        void AddCustItem(Model.rp_t_cust_item_master ord, List<Model.rp_t_cust_item_detail> lines, out string sheet_no);
        void ChangeCustItem(Model.rp_t_cust_item_master ord, List<Model.rp_t_cust_item_detail> lines);
        void DeleteCustItem(string sheet_no);
        void CheckCustItem(string sheet_no, string approve_man);
    }
}
