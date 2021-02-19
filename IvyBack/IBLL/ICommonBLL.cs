using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IvyBack.IBLL
{
    public interface ICommonBLL
    {
        string IsServer();
        string IsServer(string ip, string port);

        List<Model.bi_t_supcust_info> GetCustList(string keyword);

        List<Model.bi_t_supcust_info> GetSupList(string keyword);

        DataTable GetAllCustList(bool isJail = false);

        DataTable GetAllSupList();
       
        DataTable GetGoodsList();

        DataTable GetBranchList();

        DataTable GetPeopleList();

        DataTable GetDataTable(List<ReadWriteContext.IReadContext> lst);
    }
}
