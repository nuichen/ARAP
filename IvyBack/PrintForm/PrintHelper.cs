using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using IvyBack.Properties;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing;
using IvyBack.Helper;
using Model;
using IvyBack.BLL;
using PrintHelper.BLL;

namespace IvyBack.PrintForm
{
    public class PrintHelper
    {
        public enum PrintType
        {
            PrintV,
            PrintD,
            PrintP,
            // A,b,c,d,e,f,g
        }
        static PrintHelper()
        {
            //if (!Directory.Exists(style_path))
            //{
            //    Directory.CreateDirectory(style_path);
            //}
            //LoadStyle();
        }

        private static string style_path = AppSetting.path + "\\Resource\\print\\";

        private static Dictionary<string, Model.sys_t_print_style> _myStyle;
        public static Dictionary<string, Model.sys_t_print_style> myStyle
        {
            get
            {
                IBLL.IPrint bll = new BLL.PrintBLL();
                _myStyle = bll.GetStyleDic(new Model.sys_t_print_style()
                {
                    oper_id = Program.oper == null ? "" : Program.oper.oper_id,
                });

                return _myStyle;
            }
            set
            {
                _myStyle = value;
            }
        }
        public static class Factory
        {

            /// <summary>
            /// 创建打印
            /// </summary>
            /// <param name="type">指定对象</param>
            /// <param name="customerId">绑定ID</param>
            /// <param name="reportId">模块ID</param>
            public static void Print(PrintType type, string customerId, string reportId)
            {
                if (string.IsNullOrWhiteSpace(customerId))
                {
                    throw new Exception("供应商/客户编码或其他绑定编码不能为空!");
                }

                var defaultStyle =GetDefaultPrintStyle(reportId, customerId);

                global::PrintHelper.IBLL.IPrint print = GetPrint(type);
                print.Print(defaultStyle.style_data,
                    GetStyle(defaultStyle.style_data),
                    tb_main,
                    tb_detail);

            }
            /// <summary>
            /// 获取默认打印样式
            /// </summary>
            /// <param name="reportId">模块ID</param>
            /// <param name="supplierOrCustomerId">供应商或客户编号</param>
            /// <returns></returns>
            public static sys_t_print_style GetDefaultPrintStyle(string reportId, string supplierOrCustomerId)
            {
                var style = PrintBLL.GetDefaultPrintStyle(new sys_t_print_style_default
                {
                    cust_no = supplierOrCustomerId,
                    oper_id = Program.oper?.oper_id ?? "",
                    report_id = reportId
                }).FirstOrDefault();

                if (style == null)
                {
                    throw new Exception("您还没有设置默认样式呢.");
                }

                return PrintBLL.GetStyleById(style.style_id);
            }
            /// <summary>
            /// 工厂创建实例
            /// </summary>
            /// <param name="type"></param>
            /// <returns></returns>
            public static global::PrintHelper.IBLL.IPrint GetPrint(PrintType type)
            {
                switch (type)
                {
                    case PrintType.PrintV:
                        return new PrintV();
                    case PrintType.PrintD:
                        return new PrintD();
                    case PrintType.PrintP:
                        return new PrintP();
                }

                throw new Exception($"未知打印对象, {type}");
            }
        }

        private static Dictionary<string, Model.sys_t_print_style_default> _myDefault;
        public static Dictionary<string, Model.sys_t_print_style_default> myDefault
        {
            get
            {
                IBLL.IPrint bll = new BLL.PrintBLL();
                _myDefault = bll.GetDefaultDic(new Model.sys_t_print_style_default()
                {
                    oper_id = Program.oper == null ? "" : Program.oper.oper_id,
                });

                return _myDefault;
            }
            set
            {
                _myDefault = value;
            }
        }

        private static DataTable _tb_main;
        public static DataTable tb_main
        {
            get
            {
                if (_tb_main == null)
                    _tb_main = new DataTable();

                return _tb_main;
            }
            set
            {
                _tb_main = value;
            }
        }

        private static DataTable _tb_detail;
        public static DataTable tb_detail
        {
            get
            {
                if (_tb_detail == null)
                    _tb_detail = new DataTable();

                return _tb_detail;
            }
            set
            {
                _tb_detail = value;
            }
        }

        private static void LoadStyle()
        {
            try
            {

                //IBLL.IPrint bll = new BLL.PrintBLL();
                //Dictionary<string, Model.sys_t_print_style_data> dic = bll.GetPrintStyleData("");

                //foreach (string key in myStyle.Keys)
                //{
                //    Model.sys_t_print_style style = myStyle[key];

                //    //打印样式  写成文件
                //    using (FileStream fs = new FileStream(style_path + style.style_data + ".txt", FileMode.Create))
                //    {
                //        using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                //        {

                //            if (dic.Keys.Contains(style.style_data))
                //            {
                //                sw.Write(dic[style.style_data].style_str);
                //            }
                //            else
                //            {
                //                sw.Write(defaultStyle);
                //            }
                //            sw.Flush();
                //        }
                //    }

                //}

            }
            catch (Exception e)
            {
                MsgForm.ShowFrom(e);
            }
        }

        public static void UpdateStyle(string style_data, string style_str)
        {
            try
            {
                //using (FileStream fs = new FileStream(style_path + style_data + ".txt", FileMode.Create))
                //{
                //    fs.Seek(0, SeekOrigin.Begin);
                //    using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                //    {
                //        sw.Write(style_str);
                //        sw.Flush();
                //    }
                //}

                List<Model.sys_t_print_style_data> lis = new List<Model.sys_t_print_style_data>();
                lis.Add(new Model.sys_t_print_style_data()
                {
                    style_data = style_data,
                    style_str = style_str,
                });

                IBLL.IPrint bll = new BLL.PrintBLL();
                bll.UpdateStyleData(lis);
            }
            catch (Exception e)
            {
                MsgForm.ShowFrom(e);
            }
        }

        public static void SaveStyle()
        {
            try
            {
                //List<Model.sys_t_print_style_data> lis = new List<Model.sys_t_print_style_data>();

                //foreach (Model.sys_t_print_style style in myStyle.Values)
                //{
                //    using (StreamReader sr = new StreamReader(style_path + style.style_data + ".txt", Encoding.UTF8))
                //    {
                //        string txt = sr.ReadToEnd();
                //        lis.Add(new Model.sys_t_print_style_data()
                //        {
                //            style_data = style.style_data,
                //            style_str = txt,
                //        });
                //    }
                //}

                //IBLL.IPrint bll = new BLL.PrintBLL();
                //bll.UpdateStyleData(lis);                //List<Model.sys_t_print_style_data> lis = new List<Model.sys_t_print_style_data>();

                //foreach (Model.sys_t_print_style style in myStyle.Values)
                //{
                //    using (StreamReader sr = new StreamReader(style_path + style.style_data + ".txt", Encoding.UTF8))
                //    {
                //        string txt = sr.ReadToEnd();
                //        lis.Add(new Model.sys_t_print_style_data()
                //        {
                //            style_data = style.style_data,
                //            style_str = txt,
                //        });
                //    }
                //}

                //IBLL.IPrint bll = new BLL.PrintBLL();
                //bll.UpdateStyleData(lis);
            }
            catch (Exception e)
            {
                MsgForm.ShowFrom(e);
            }
        }

        public static void DelStyle(string style_id)
        {
            try
            {
                if (myStyle.Keys.Contains(style_id))
                {
                    Model.sys_t_print_style style = myStyle[style_id];
                }
            }
            catch (Exception e)
            {
                MsgForm.ShowFrom(e);
            }
        }

        public static void PrintStyle(string path, DataTable tb_main, DataTable tb_detail)
        {
            try
            {
                string style = "";
                using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
                {
                    style = sr.ReadToEnd();
                }

                global::PrintHelper.IBLL.IPrint print = new global::PrintHelper.BLL.PrintP();
                print.Print(new Guid().ToString(), style, tb_main, tb_detail);

            }
            catch (Exception)
            {
                throw;
            }
        }

        const string defaultStyle = @"<xml><Page><Width>819</Width><Height>1158</Height><BarHeight>25</BarHeight>
                            <AHeight>80</AHeight><BHeight>80</BHeight><CHeight>30</CHeight><DHeight>80</DHeight>
                            <EHeight>80</EHeight><GridLeft>20</GridLeft><ExistGrid>1</ExistGrid><AppendEmptyRow>0</AppendEmptyRow>
                            <GridFont>宋体, 9pt</GridFont></Page></xml>";
        public static string GetStyle(string style_data, string parent_data = "")
        {
            string style = "";
            try
            {
                IBLL.IPrint bll = new BLL.PrintBLL();
                Dictionary<string, Model.sys_t_print_style_data> dic = bll.GetPrintStyleData(style_data);
                if (dic.TryGetValue(style_data, out Model.sys_t_print_style_data value))
                {
                    return value.style_str;
                }

                if (!string.IsNullOrWhiteSpace(parent_data))
                {
                    dic = bll.GetPrintStyleData(parent_data);
                    if (dic.TryGetValue(parent_data, out value))
                    {
                        return value.style_str;
                    }
                }
                //string path = style_path + style_data + ".txt";
                //if (!File.Exists(path))
                //{
                //    File.AppendAllText(path, "");
                //}
                //using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
                //{
                //    style = sr.ReadToEnd();
                //    if (string.IsNullOrEmpty(style))
                //    {
                //        return defaultStyle;
                //    }
                //}
                return defaultStyle;
            }
            catch (Exception)
            {
                throw;
            }

        }


        public static DataTable ChangeColumName(DataTable tb)
        {
            if (tb == null) return new DataTable();

            for (int i = tb.Columns.Count - 1; i >= 0; i--)
            {
                DataColumn dc = tb.Columns[i];
                if (Helper.KeyPressJudge.IsChinese(dc.ColumnName)) continue;
                switch (dc.ColumnName)
                {
                    case "sheet_no": dc.ColumnName = "单据号"; break;
                    case "voucher_no": dc.ColumnName = "关联单号"; break;
                    case "cust_no": dc.ColumnName = "客户编号"; break;
                    case "cust_name": dc.ColumnName = "客户名称"; break;
                    case "supcust_no": dc.ColumnName = "供应商编号"; break;
                    case "sup_name": dc.ColumnName = "客户名称"; break;
                    case "pay_way": dc.ColumnName = "支付方式"; break;
                    case "discount": dc.ColumnName = "折扣"; break;
                    case "real_amount": dc.ColumnName = "实际金额"; break;
                    case "total_amount": dc.ColumnName = "押金"; break;
                    case "oper_id": dc.ColumnName = "操作员"; break;
                    case "oper_date": dc.ColumnName = "操作时间"; break;
                    case "approve_man": dc.ColumnName = "审核人"; break;
                    case "approve_date": dc.ColumnName = "审核日期"; break;
                    case "old_no": dc.ColumnName = "内部编号"; break;
                    case "other1": dc.ColumnName = "备注1"; break;
                    case "other2": dc.ColumnName = "备注2"; break;
                    case "item_subno": dc.ColumnName = "货号"; break;
                    case "barcode": dc.ColumnName = "条码"; break;
                    case "item_name": dc.ColumnName = "商品名称"; break;
                    case "unit_no": dc.ColumnName = "单位"; break;
                    case "item_size": dc.ColumnName = "规格"; break;
                    case "in_qty": dc.ColumnName = "数量"; break;
                    case "valid_price": dc.ColumnName = "单价"; break;
                    case "sub_amount": dc.ColumnName = "金额"; break;
                    case "valid_date": dc.ColumnName = "有效期"; break;
                    case "price": dc.ColumnName = "参考进价"; break;
                    case "other5": dc.ColumnName = "备注"; break;
                    case "sale_qnty": dc.ColumnName = "销售数量"; break;
                    case "sale_money": dc.ColumnName = "销售金额"; break;
                    case "item_no": dc.ColumnName = "商品编号"; break;
                    case "branch_no": dc.ColumnName = "机构仓库"; break;
                    case "visa_id": dc.ColumnName = "帐户"; break;
                    case "visa_in": dc.ColumnName = "转入帐户"; break;
                    case "bill_total": dc.ColumnName = "转账金额"; break;
                    case "deal_man": dc.ColumnName = "经办人"; break;
                    case "d_branch_no": dc.ColumnName = "目标仓库"; break;
                    case "pay_date": dc.ColumnName = "限付日期"; break;
                    case "price_type": dc.ColumnName = "调价类型"; break;
                    case "memo": dc.ColumnName = "备注"; break;
                    case "old_price": dc.ColumnName = "原批发价"; break;
                    case "new_price": dc.ColumnName = "新批发价"; break;
                    case "old_price2": dc.ColumnName = "原二批发价"; break;
                    case "new_price2": dc.ColumnName = "新二批发价"; break;
                    case "old_price3": dc.ColumnName = "原三批发价"; break;
                    case "new_price3": dc.ColumnName = "新三批发价"; break;
                    case "stock_qty": dc.ColumnName = "库存数量"; break;
                    case "free_money": dc.ColumnName = "免付金额"; break;
                    case "sheet_amount": dc.ColumnName = "单据金额"; break;
                    case "paid_amount": dc.ColumnName = "已支付金额"; break;
                    case "paid_free": dc.ColumnName = "已免付金额"; break;
                    case "yf_amount": dc.ColumnName = "应付金额"; break;
                    case "pay_amount": dc.ColumnName = "付款金额"; break;
                    case "pay_free": dc.ColumnName = "免付金额"; break;
                    case "voucher_type": dc.ColumnName = "业务描述"; break;
                    case "kk_no": dc.ColumnName = "费用代码"; break;
                    case "kk_name": dc.ColumnName = "费用名称"; break;
                    case "kk_cash": dc.ColumnName = "费用"; break;
                    case "pay_kind": dc.ColumnName = "应收增减"; break;
                    case "real_qty": dc.ColumnName = "实际数"; break;
                    case "create_time": dc.ColumnName = "创建时间"; break;
                    case "begin_date": dc.ColumnName = "开始时间"; break;
                    case "end_date": dc.ColumnName = "结束时间"; break;
                    case "item_clsno": dc.ColumnName = "商品分类"; break;
                    case "check_no": dc.ColumnName = "盘点单"; break;
                    case "sup_addr": dc.ColumnName = "地址"; break;
                    case "sup_tel": dc.ColumnName = "电话"; break;
                    case "sup_man": dc.ColumnName = "联系人"; break;
                    default: tb.Columns.Remove(dc); break;
                }
            }

            return tb;
        }

    }

}
