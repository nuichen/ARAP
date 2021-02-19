using System.Collections.Generic;
using System.Data;
using Model;

namespace IvyTran.IBLL.ERP
{
    public interface IPrint
    {
        DataTable GetAll(sys_t_print_style style);
        void Add(sys_t_print_style style);
        void Update(sys_t_print_style style);
        void Del(sys_t_print_style style);

        DataTable GetPrintStyleDefault(sys_t_print_style_default style);
        void AddDefault(sys_t_print_style_default style);
        void UpdateDefault(sys_t_print_style_default style);
        void DelDefault(sys_t_print_style_default style);

        DataTable GetPrintStyleData(string style_data);
        void UpdateStyleData(List<sys_t_print_style_data> lis);
        #region 客户打印样式配对

        DataTable GetStyleGroupInfo(string type_name);

        void JoinCustomerAndStyle(sys_t_print_style_default style_Default);
        DataTable GetStyleTypeInfo();

        sys_t_print_style GetStyleById(sys_t_print_style style);

        #endregion

    }
}
