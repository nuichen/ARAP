using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Model;

namespace IvyBack.IBLL
{
    public interface IPrint
    {
        DataTable GetAll(sys_t_print_style style);
        Dictionary<string, sys_t_print_style> GetStyleDic(sys_t_print_style style);
        void Add(sys_t_print_style style);
        void Update(sys_t_print_style style);
        void Del(sys_t_print_style style);

        DataTable GetPrintStyleDefault(sys_t_print_style_default style);
        Dictionary<string, sys_t_print_style_default> GetDefaultDic(sys_t_print_style_default style);
        void AddDefault(sys_t_print_style_default style);
        void UpdateDefault(sys_t_print_style_default style);
        void DelDefault(sys_t_print_style_default style);

        Dictionary<string, sys_t_print_style_data> GetPrintStyleData(string style_data);
        void UpdateStyleData(List<sys_t_print_style_data> lis);

    }
}