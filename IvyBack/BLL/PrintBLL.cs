using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using IvyBack.Helper;
using Model;

namespace IvyBack.BLL
{
    public class PrintBLL : IBLL.IPrint
    {
        public DataTable GetAll(sys_t_print_style style)
        {
            JsonRequest r = new JsonRequest();

            r.Write("style", style);

            r.request("/print?t=GetAll");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            return tb;
        }

        public Dictionary<string, sys_t_print_style> GetStyleDic(sys_t_print_style style)
        {
            JsonRequest r = new JsonRequest();

            r.Write("style", style);

            r.request("/print?t=GetAll");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            Dictionary<string, sys_t_print_style> style_dic = r.GetDicOfTable<string, sys_t_print_style>("style_id");
            return style_dic;
        }

        public void Add(sys_t_print_style style)
        {
            JsonRequest r = new JsonRequest();

            r.Write("style", style);

            r.request("/print?t=Add");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void Update(sys_t_print_style style)
        {
            JsonRequest r = new JsonRequest();

            r.Write("style", style);

            r.request("/print?t=Update");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void Del(sys_t_print_style style)
        {
            JsonRequest r = new JsonRequest();

            r.Write("style", style);

            r.request("/print?t=Del");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public DataTable GetPrintStyleDefault(sys_t_print_style_default style)
        {
            JsonRequest r = new JsonRequest();

            r.Write("style", style);

            r.request("/print?t=GetPrintStyleDefault");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            return tb;
        }

        public Dictionary<string, sys_t_print_style_default> GetDefaultDic(sys_t_print_style_default style)
        {
            JsonRequest r = new JsonRequest();

            r.Write("style", style);

            r.request("/print?t=GetPrintStyleDefault");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            Dictionary<string, sys_t_print_style_default> style_dic = r.GetDicOfTable<string, sys_t_print_style_default>("report_id");
            return style_dic;
        }

        public void AddDefault(sys_t_print_style_default style)
        {
            JsonRequest r = new JsonRequest();

            r.Write("style", style);

            r.request("/print?t=AddDefault");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void UpdateDefault(sys_t_print_style_default style)
        {
            JsonRequest r = new JsonRequest();

            r.Write("style", style);

            r.request("/print?t=UpdateDefault");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void DelDefault(sys_t_print_style_default style)
        {
            JsonRequest r = new JsonRequest();

            r.Write("style", style);

            r.request("/print?t=DelDefault");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public Dictionary<string, sys_t_print_style_data> GetPrintStyleData(string style_data)
        {
            JsonRequest r = new JsonRequest();

            r.Write("style_data", style_data);

            r.request("/print?t=GetPrintStyleData");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            Dictionary<string, sys_t_print_style_data> dic = r.GetDicOfTable<string, sys_t_print_style_data>("style_data");
            return dic;
        }

        public void UpdateStyleData(List<sys_t_print_style_data> lis)
        {
            JsonRequest r = new JsonRequest();

            r.Write("lis", lis);

            r.request("/print?t=UpdateStyleData");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

        }
        public static List<sys_t_print_style_default> GetDefaultPrintStyle(sys_t_print_style_default style)
        {
            JsonRequest r = new JsonRequest();

            r.Write("style", style);

            r.request("/print?t=GetPrintStyleDefault");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            return r.GetList<sys_t_print_style_default>();
        }
        public static sys_t_print_style GetStyleById(string styleId)
        {
            JsonRequest r = new JsonRequest();

            r.Write("style_id", styleId);

            r.request("/print?t=GetStyleById");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            return r.GetObject<sys_t_print_style>();
        }

    }
}
