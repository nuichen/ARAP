namespace IvyTran.IBLL.ERP
{
    interface IRegion
    {
        System.Data.DataTable GetList(string is_cs);
        System.Data.DataTable GetNodeList(string is_cs);
        System.Data.DataTable GetItem(string region_no);
        string MaxCode();
        void Add(Model.bi_t_region_info item);
        void Change(Model.bi_t_region_info item);
        void Delete(string region_no);
    }

}
