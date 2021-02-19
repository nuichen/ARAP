using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IvyBack.IBLL;
using Model;
using IvyBack.Helper;
using System.Data;
namespace IvyBack.BLL
{
    public class PaymentBLL : IPayment
    {
        public DataTable GetAllList()
        {
            JsonRequest r = new JsonRequest();

            r.request("/payment?t=get_list");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            //tb.Columns.Add("状态");
            //tb.Columns.Add("name");
            //foreach (DataRow dr in tb.Rows)
            //{
            //    dr["name"] = NPinyin.Pinyin.GetInitials(dr["pay_name"].ToString());
            //    if (dr["display"].ToString().Equals("1"))
            //    {
            //        dr["状态"] = "已启用";
            //    }
            //    else if (dr["display"].ToString().Equals("0"))
            //    {
            //        dr["状态"] = "已禁用";
            //    }
            //}

            return tb;
        }

        public DataTable GetPayment(Model.bi_t_payment_info pay)
        {
            JsonRequest r = new JsonRequest();

            r.Write<bi_t_payment_info>(pay);

            r.request("/payment?t=get_item");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable dt = r.GetDataTable();
            return dt;
        }

        public void Add(Model.bi_t_payment_info pay)
        {
            JsonRequest r = new JsonRequest();

            r.Write<bi_t_payment_info>(pay);

            r.request("/payment?t=add");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void Del(Model.bi_t_payment_info pay)
        {
            JsonRequest r = new JsonRequest();

            r.Write<bi_t_payment_info>(pay);

            r.request("/payment?t=delete");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void Upload(Model.bi_t_payment_info pay)
        {
            JsonRequest r = new JsonRequest();

            r.Write<bi_t_payment_info>(pay);

            r.request("/payment?t=change");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }
    }
}
