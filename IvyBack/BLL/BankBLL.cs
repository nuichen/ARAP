using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IvyBack.IBLL;
using IvyBack.Helper;
using System.Data;
using Model;

namespace IvyBack.BLL
{
    public class BankBLL : IBank
    {
        public System.Data.DataTable GetAllList()
        {
            JsonRequest r = new JsonRequest();

            r.request("/bank?t=get_list");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            //tb.Columns.Add("状态");
            //tb.Columns.Add("name");
            //foreach (DataRow dr in tb.Rows)
            //{
            //    dr["name"] = NPinyin.Pinyin.GetInitials(dr["visa_nm"].ToString());
            //    if (dr["display_flag"].ToString().Equals("1"))
            //    {
            //        dr["状态"] = "已启用";
            //    }
            //    else if (dr["display_flag"].ToString().Equals("0"))
            //    {
            //        dr["状态"] = "已禁用";
            //    }
            //}

            return tb;
        }
        public System.Data.DataTable GetSubjectList()
        {
            JsonRequest r = new JsonRequest();

            r.request("/bank?t=GetSubjectList");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            //tb.Columns.Add("状态");
            //tb.Columns.Add("name");
            //foreach (DataRow dr in tb.Rows)
            //{
            //    dr["name"] = NPinyin.Pinyin.GetInitials(dr["visa_nm"].ToString());
            //    if (dr["display_flag"].ToString().Equals("1"))
            //    {
            //        dr["状态"] = "已启用";
            //    }
            //    else if (dr["display_flag"].ToString().Equals("0"))
            //    {
            //        dr["状态"] = "已禁用";
            //    }
            //}

            return tb;
        }
        public Model.bi_t_bank_info GetItemCls(Model.bi_t_bank_info bank)
        {
            JsonRequest r = new JsonRequest();

            r.Write<bi_t_bank_info>(bank);

            r.request("/bank?t=get_item");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            bank = r.GetObject<bi_t_bank_info>();

            return bank;
        }

        public void Add(Model.bi_t_bank_info bank)
        {
            JsonRequest r = new JsonRequest();

            r.Write<bi_t_bank_info>(bank);

            r.request("/bank?t=add");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void Del(Model.bi_t_bank_info bank)
        {
            JsonRequest r = new JsonRequest();

            r.Write<bi_t_bank_info>(bank);

            r.request("/bank?t=delete");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void Upload(Model.bi_t_bank_info bank)
        {
            JsonRequest r = new JsonRequest();

            r.Write<bi_t_bank_info>(bank);

            r.request("/bank?t=change");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }
    }
}
