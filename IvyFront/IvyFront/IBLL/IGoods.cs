using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyFront.IBLL
{
    interface IGoods
    {
        List<Model.bi_t_item_info> GetList(string item_clsno, string keyword);
        List<Model.bi_t_item_cls> GetClsList();
        List<Model.bi_t_item_cls> GetAllClsList();
        void StopCls(string item_clsno);
        void StartCls(string item_clsno);
        Dictionary<string, Model.bi_t_item_info> GetDic();
        /// <summary>
        /// 取成本价
        /// </summary>
        /// <param name="item_no"></param>
        /// <param name="branch_no"></param>
        /// <returns></returns>
        decimal GetCost(string item_no, string branch_no);

        Model.bi_t_item_info GetItem(string item_no);
        Model.bi_t_item_info GetItemByItemSubNo(string item_subno);

    }
}
