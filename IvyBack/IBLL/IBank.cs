using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace IvyBack.IBLL
{
    public interface IBank
    {
        DataTable GetAllList();
        System.Data.DataTable GetSubjectList();
        bi_t_bank_info GetItemCls(bi_t_bank_info bank);
        void Add(bi_t_bank_info bank);
        void Del(bi_t_bank_info bank);
        void Upload(bi_t_bank_info bank);
    }
}
