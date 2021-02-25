using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace IvyBack.IBLL
{
    public interface IPayment
    {
        DataTable GetAllList();
        DataTable GetPayment(bi_t_payment_info pay);
        void Add(bi_t_payment_info pay);
        void Del(bi_t_payment_info pay);
        void Upload(bi_t_payment_info pay);
        DataTable Getlist();

    }
}
