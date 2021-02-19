using System.Collections.Generic;

namespace Model.StaticType
{
    public class IvyTransFunction
    {
        static IvyTransFunction()
        {
            ini_data();
        }

        //业务类型：trans_no字典表
        public static Dictionary<string, string> trans_dic;
        //财务往来账发生类型：oper_type字典表
        public static Dictionary<string, string> oper_type_dic;
        //库存流水单据类型：sheet_type字典表
        public static Dictionary<string, string> sheet_type_dic;
        //合集
        public static Dictionary<string, string> all_type_dic;

        private static void ini_data()
        {
            //all_type
            {
                all_type_dic = new Dictionary<string, string>();
                all_type_dic.Add("", "全部");
                all_type_dic.Add("01", "其它入库");
                all_type_dic.Add("02", "归还");
                all_type_dic.Add("03", "其它出库");
                all_type_dic.Add("04", "领用出库");
                all_type_dic.Add("05", "报损出库");
                all_type_dic.Add("06", "借出");
                all_type_dic.Add("07", "报溢入库");
                all_type_dic.Add("09", "库存调整");
                all_type_dic.Add("A", "进货入库");
                all_type_dic.Add("B", "期初帐款");
                all_type_dic.Add("C", "成本变更");
                all_type_dic.Add("D", "退货入库");
                all_type_dic.Add("E", "赠送入库");
                all_type_dic.Add("F", "退货出库");
                all_type_dic.Add("G", "调拨");
                all_type_dic.Add("P", "盘点");
                all_type_dic.Add("R", "盘盈盘亏");
                all_type_dic.Add("S", "前台销售");
                all_type_dic.Add("T", "期初库存");
                all_type_dic.Add("W", "拆分");
                all_type_dic.Add("I", "销售出库");
                all_type_dic.Add("M", "组装");

                all_type_dic.Add("PO", "采购订单");
                all_type_dic.Add("GM", "其他应付单");
                all_type_dic.Add("CM", "其他应收单");
                all_type_dic.Add("MI", "配入单");
                all_type_dic.Add("MO", "配出单");
                all_type_dic.Add("LY", "联营商品结算");
                all_type_dic.Add("BM", "供应商费用收入");
                all_type_dic.Add("BK", "银行日记帐");
                all_type_dic.Add("CL", "客户返利单");
                all_type_dic.Add("CP", "客户结算单");
                all_type_dic.Add("HM", "其他应收单");
                all_type_dic.Add("KZ", "现金支出单");
                all_type_dic.Add("RC", "供应商结算对账单");
                all_type_dic.Add("RI", "客户退货单");
                all_type_dic.Add("RO", "退货单");
                all_type_dic.Add("RP", "供应商付款单");
                all_type_dic.Add("RS", "客户结算对账单");
                all_type_dic.Add("SO", "批发销售单");
                all_type_dic.Add("SR", "现金收支单");
                all_type_dic.Add("PI", "采购入库单");
                all_type_dic.Add("OM", "领料出库单");
                all_type_dic.Add("RM", "退料入库单");
                all_type_dic.Add("PE", "成品入库单");
                all_type_dic.Add("OR", "应收应付冲账");
                all_type_dic.Add("RR", "应收转应收");
                all_type_dic.Add("PR", "应付转应付");
                all_type_dic.Add("Q1", "客户期初");
                all_type_dic.Add("Q2", "供应商期初");
            }

            //trans
            {
                trans_dic = new Dictionary<string, string>();
                trans_dic.Add("", "全部");
                trans_dic.Add("01", "其它入库");
                trans_dic.Add("02", "归还");
                trans_dic.Add("03", "其它出库");
                trans_dic.Add("04", "领用出库");
                trans_dic.Add("05", "报损出库");
                trans_dic.Add("06", "借出");
                trans_dic.Add("07", "报溢入库");
                trans_dic.Add("09", "库存调整");
                trans_dic.Add("A", "进货入库");
                trans_dic.Add("B", "期初帐款");
                trans_dic.Add("C", "成本变更");
                trans_dic.Add("D", "退货入库");
                trans_dic.Add("E", "赠送入库");
                trans_dic.Add("F", "退货出库");
                trans_dic.Add("G", "调拨");

                trans_dic.Add("PO", "采购订单");
                trans_dic.Add("GM", "供应商费用单");
                trans_dic.Add("CM", "客户费用单");
                trans_dic.Add("I", "销售出库");
                trans_dic.Add("M", "组装");
                trans_dic.Add("MI", "配入单");
                trans_dic.Add("MO", "配出单");
                trans_dic.Add("P", "盘点");
                trans_dic.Add("R", "盘盈盘亏");
                trans_dic.Add("S", "前台销售");
                trans_dic.Add("T", "期初库存");
                trans_dic.Add("W", "拆分");
                trans_dic.Add("OM", "领料出库单");
                trans_dic.Add("RM", "退料入库单");
                trans_dic.Add("PE", "成品入库单");
                trans_dic.Add("Q1", "客户期初");
                trans_dic.Add("Q2", "供应商期初");
            }

            //oper_type
            {
                oper_type_dic = new Dictionary<string, string>();
                oper_type_dic.Add("", "全部");
                oper_type_dic.Add("A", "进货入库");
                oper_type_dic.Add("B", "期初帐款");
                oper_type_dic.Add("P", "结算标记");
                oper_type_dic.Add("F", "退货出库");
                oper_type_dic.Add("E", "赠送入库");
                oper_type_dic.Add("G", "调拨");
                oper_type_dic.Add("GM", "供应商费用或返点");
                oper_type_dic.Add("LY", "联营商品结算");
                oper_type_dic.Add("BM", "供应商费用收入");
                oper_type_dic.Add("CM", "客户费用");
                oper_type_dic.Add("I", "销售");
                oper_type_dic.Add("D", "销售退货");
                oper_type_dic.Add("OM", "领料出库单");
                oper_type_dic.Add("RM", "退料入库单");
                oper_type_dic.Add("PE", "成品入库单");
                oper_type_dic.Add("Q1", "客户期初");
                oper_type_dic.Add("Q2", "供应商期初");
            }

            //sheet_type_dic
            {
                sheet_type_dic = new Dictionary<string, string>();
                sheet_type_dic.Add("", "全部");
                sheet_type_dic.Add("BK", "银行日记帐");
                sheet_type_dic.Add("BM", "供应商费用单");
                sheet_type_dic.Add("CL", "客户返利单");
                sheet_type_dic.Add("CM", "客户费用单");
                sheet_type_dic.Add("CP", "客户结算单");
                sheet_type_dic.Add("GM", "供应商费用单");
                sheet_type_dic.Add("HM", "客户费用单");
                sheet_type_dic.Add("KZ", "现金支出单");
                sheet_type_dic.Add("RC", "供应商结算对账单");
                sheet_type_dic.Add("RI", "客户退货单");
                sheet_type_dic.Add("RO", "退货单");
                sheet_type_dic.Add("RP", "供应商结算单");
                sheet_type_dic.Add("RS", "客户结算对账单");
                sheet_type_dic.Add("SO", "批发销售单");
                sheet_type_dic.Add("SR", "现金收支单");
                sheet_type_dic.Add("PI", "采购入库单");
                sheet_type_dic.Add("OM", "领料出库单");
                sheet_type_dic.Add("RM", "退料入库单");
                sheet_type_dic.Add("PE", "成品入库单");
                sheet_type_dic.Add("Q1", "客户期初");
                sheet_type_dic.Add("Q2", "供应商期初");
            }

        }

        private static string _tran_no_str;
        public static string tran_no_str
        {
            get
            {
                if (trans_dic == null) ini_data();
                if (string.IsNullOrEmpty(_tran_no_str))
                {
                    _tran_no_str += "{";

                    foreach (var r in trans_dic)
                    {
                        _tran_no_str += r.Key + ":" + r.Value + ",";
                    }
                    if (_tran_no_str.Length > 0) _tran_no_str = _tran_no_str.Substring(0, _tran_no_str.Length - 1);

                    _tran_no_str += "}";
                }
                return _tran_no_str;
            }
        }

        private static string _oper_type_str;
        public static string oper_type_str
        {
            get
            {
                if (oper_type_dic == null) ini_data();
                if (string.IsNullOrEmpty(_oper_type_str))
                {
                    _oper_type_str += "{";

                    foreach (var r in oper_type_dic)
                    {
                        _oper_type_str += r.Key + ":" + r.Value + ",";
                    }
                    if (_oper_type_str.Length > 0) _oper_type_str = _oper_type_str.Substring(0, _oper_type_str.Length - 1);

                    _oper_type_str += "}";
                }
                return _oper_type_str;
            }
        }


        private static string _sheet_type_str;
        public static string sheet_type_str
        {
            get
            {
                if (sheet_type_dic == null) ini_data();
                if (string.IsNullOrEmpty(_sheet_type_str))
                {
                    _sheet_type_str += "{";

                    foreach (var r in sheet_type_dic)
                    {
                        _sheet_type_str += r.Key + ":" + r.Value + ",";
                    }
                    if (_sheet_type_str.Length > 0) _sheet_type_str = _sheet_type_str.Substring(0, _sheet_type_str.Length - 1);

                    _sheet_type_str += "}";
                }
                return _sheet_type_str;
            }
        }

        private static string _all_type_str;
        public static string all_type_str
        {
            get
            {
                if (all_type_dic == null) ini_data();
                if (string.IsNullOrEmpty(_all_type_str))
                {
                    _all_type_str += "{";

                    foreach (var r in all_type_dic)
                    {
                        _all_type_str += r.Key + ":" + r.Value + ",";
                    }
                    if (_all_type_str.Length > 0) _all_type_str = _all_type_str.Substring(0, _all_type_str.Length - 1);

                    _all_type_str += "}";
                }
                return _all_type_str;
            }
        }

    }
}
