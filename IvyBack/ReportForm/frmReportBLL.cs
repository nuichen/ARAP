using IvyBack.Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IvyBack.BLL;
using IvyBack.MainForm;
//using IvyBack.Processing;

namespace IvyBack.ReportForm
{
    public class frmReportBLL
    {
        frmBaseReport frm;
        public frmReportBLL(frmBaseReport frm)
        {
            this.frm = frm;
        }

        /// <summary>
        /// 采购助手  计划采购生成采购单
        /// </summary>
        public void GeneratePlanCG(cons.DataGrid dgv)
        {
            try
            {
                if (dgv.DataSource.Rows.Count < 1)
                {
                    throw new Exception("表格中没有数据!");
                }
                DataTable tb = dgv.GetSelectData();
                if (tb.Rows.Count < 1)
                {
                    throw new Exception("请选择要生成采购单的数据!");
                }

                StringBuilder flow_id = new StringBuilder("''");

                foreach (DataRow dr in tb.Rows)
                {
                    flow_id.Append(",'" + dr["行号"].ToString() + "'");
                }

                IBLL.IInOutBLL bll = new BLL.InOutBLL();

                bll.AssGenPlanCG(flow_id.ToString(), Program.oper.oper_id);

                MsgForm.ShowFrom("生成采购单成功");

                frm.btnSelect_Click(null, null);
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }

        public void ImportPlanCGDeltail(DateTime start_time, DateTime end_time, string keyword)
        {
            try
            {
                IBLL.IReport reportBLL = new BLL.ReportBLL();
                DataTable tb = reportBLL.GetAssCGPlanDetailExport(start_time, end_time, keyword);
                if (tb.Rows.Count < 1)
                {
                    MsgForm.ShowFrom("无导出数据");
                    return;
                }

                System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
                sfd.Filter = "*.xlsx|*.xlsx";
                sfd.Title = "导出采购助手";
                sfd.FileName = DateTime.Now.ToString("yyyy-MM-dd HHmmss") + "-采购助手导出";
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    List<DataTable> datas = new List<DataTable>();//导出 按分类作为sheet页导出

                    var clsno_group = tb.AsEnumerable().GroupBy(r => r["item_clsno"]);
                    foreach (var rows in clsno_group)
                    {
                        DataTable ex_tb = new DataTable();
                        ex_tb.Columns.Add("供应商编号", typeof(string));
                        ex_tb.Columns.Add("供应商名称", typeof(string));
                        ex_tb.Columns.Add("商品名称", typeof(string));
                        ex_tb.Columns.Add("商品单位", typeof(string));
                        ex_tb.Columns.Add("采购价格", typeof(decimal));
                        ex_tb.Columns.Add("采购数量", typeof(decimal));
                        ex_tb.Columns.Add("小计", typeof(decimal));
                        foreach (DataRow row in rows)
                        {
                            ex_tb.TableName = row["item_clsname"].ToString();
                            DataRow dr = ex_tb.NewRow();
                            dr["供应商编号"] = row["supcust_no"];
                            dr["供应商名称"] = row["sup_name"];
                            dr["商品名称"] = row["item_name"];
                            dr["商品单位"] = row["unit_no"];
                            dr["采购价格"] = row["price"].ToDecimal().ToString("0.00");
                            dr["采购数量"] = row["cg_qty"].ToDecimal().ToString("0.00");
                            dr["小计"] = row["sub_amount"].ToDecimal().ToString("0.00");
                            ex_tb.Rows.Add(dr);
                        }
                        datas.Add(ex_tb);
                    }

                    ExcelHelper excel = new ExcelHelper();
                    excel.WriteToExcel(datas, sfd.FileName);

                    MsgForm.ShowFrom("导出成功!");
                }
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }

        /// <summary>
        /// 采购助手  预采购生成采购单
        /// </summary>
        public void GeneratePreCG(cons.DataGrid dgv)
        {
            try
            {
                if (dgv.DataSource.Rows.Count < 1)
                {
                    throw new Exception("表格中没有数据!");
                }
                DataTable tb = dgv.GetSelectData();
                if (tb.Rows.Count < 1)
                {
                    throw new Exception("请选择要生成采购单的数据!");
                }

                StringBuilder flow_id = new StringBuilder("''");

                foreach (DataRow dr in tb.Rows)
                {
                    flow_id.Append(",'" + dr["行号"].ToString() + "'");
                }

                IBLL.IInOutBLL bll = new BLL.InOutBLL();

                bll.AssGenCG(flow_id.ToString(), Program.oper.oper_id);

                MsgForm.ShowFrom("生成采购单成功");

                frm.btnSelect_Click(null, null);
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }

        /// <summary>
        /// 收货称流水生成采购单
        /// </summary>
        /// <param name="dgv"></param>
        public void GenerateReceiveCG(cons.DataGrid dgv)
        {
            try
            {
                if (dgv.DataSource.Rows.Count < 1)
                {
                    throw new Exception("表格中没有数据!");
                }
                DataTable tb = dgv.GetSelectData();
                if (tb.Rows.Count < 1)
                {
                    throw new Exception("请选择要生成采购单的数据!");
                }

                StringBuilder flow_id = new StringBuilder("''");
                bool is_gen = true;
                foreach (DataRow dr in tb.Rows)
                {
                    if ("1".Equals(dr["是否生成"]))
                    {
                        is_gen = false;
                    }
                    flow_id.Append(",'" + dr["行号"].ToString() + "'");
                }

                if (is_gen == false && YesNoForm.ShowFrom("当前有已经生成的数据，是否重新生成?") != DialogResult.Yes)
                {
                    return;
                }

                IBLL.IInOutBLL bll = new BLL.InOutBLL();

                bll.ReceiveGenCG(flow_id.ToString(), Program.oper.oper_id);

                MsgForm.ShowFrom("生成采购单成功");
                frm.btnSelect_Click(null, null);
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }

        /// <summary>
        /// 采购助手 收获流水 修改行颜色
        /// </summary>
        /// <param name="dgv"></param>
        public void ChangeRowColor(cons.DataGrid dgv)
        {
            if (!dgv.DataSource.Columns.Contains("row_color"))
            {
                dgv.DataSource.Columns.Add("row_color", typeof(Color));
            }
            foreach (DataRow row in dgv.DataSource.Rows)
            {
                if (row["是否生成"].ToInt32() == 1)
                {
                    row["row_color"] = System.Drawing.Color.Blue;
                }
            }
            dgv.Refresh();
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="tb"></param>
        public void ExportExcel(string name, DataTable tb)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "*.xlsx|*.xlsx";
            sfd.Title = "导出报表";
            sfd.FileName = name;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                ExcelHelper excel = new ExcelHelper();
                excel.WriteToExcel(tb, sfd.FileName);

                MsgForm.ShowFrom("导出成功");
            }

        }

        /// <summary>
        /// 实拣明细生成销售单  审核
        /// </summary>
        /// <param name="dgv"></param>
        public void PickingGenSale(cons.DataGrid dgv)
        {
            try
            {
                if (dgv.DataSource.Rows.Count < 1)
                {
                    throw new Exception("表格中没有数据!");
                }
                DataTable tb = dgv.GetSelectData();
                if (tb.Rows.Count < 1)
                {
                    throw new Exception("请选择要生成销售单的数据!");
                }

                StringBuilder flow_id = new StringBuilder("'-1'");

                foreach (DataRow dr in tb.Rows)
                {
                    if (!"1".Equals(dr["审核状态"]))
                    {
                        flow_id.Append(",'" + dr["flow_id"] + "'");
                    }

                }

                if ("'-1'".Equals(flow_id.ToString()))
                {
                    MsgForm.ShowFrom("没有要审核的数据");
                    return;
                }

                IBLL.IInOutBLL bll = new BLL.InOutBLL();

                bll.PickingGenSaleSheet(flow_id.ToString(), Program.oper.oper_id);

                MsgForm.ShowFrom("审核成功，已生成销售单");

                frm.btnSelect_Click(null, null);
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }

        /// <summary>
        /// 库存调整
        /// </summary>
        /// <param name="dgv"></param>
        public void InventoryAdjustment(cons.DataGrid dgv)
        {
            try
            {
                if (dgv.DataSource.Rows.Count < 1)
                {
                    throw new Exception("表格中没有数据!");
                }
                DataTable tb = dgv.GetSelectData();
                if (tb.Rows.Count < 1)
                {
                    throw new Exception("请选择数据!");
                }

                StringBuilder flow_id = new StringBuilder("'-1'");

                foreach (DataRow dr in tb.Rows)
                {
                    if (!"1".Equals(dr["已生成"]))
                    {
                        flow_id.Append(",'" + dr["flow_id"] + "'");
                    }
                }

                if ("'-1'".Equals(flow_id.ToString()))
                {
                    MsgForm.ShowFrom("没有要保存的数据");
                    return;
                }

                IBLL.IInOutBLL bll = new InOutBLL();
                bll.InventoryAdjustment(flow_id.ToString(), Program.oper.oper_id);

                MsgForm.ShowFrom("生成完成");
                frm.btnSelect_Click(null, null);
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }

        /// <summary>
        /// 加工明细 生成单
        /// </summary>
        /// <param name="dgv"></param>
        //public void ProcessGen(cons.DataGrid dgv)
        //{

        //    if (dgv.DataSource.Rows.Count < 1)
        //    {
        //        MsgForm.ShowFrom("表格中没有数据!");
        //        return;
        //    }
        //    DataTable tb = dgv.GetSelectData();
        //    if (tb.Rows.Count < 1)
        //    {
        //        MsgForm.ShowFrom("请选择要生成销售单的数据!");
        //        return;
        //    }

        //    StringBuilder flow_id = new StringBuilder("'-1'");

        //    foreach (DataRow dr in tb.Rows)
        //    {
        //        if (!"1".Equals(dr["审核状态"]))
        //        {
        //            flow_id.Append(",'" + dr["flow_id"] + "'");
        //        }

        //    }

        //    if ("'-1'".Equals(flow_id.ToString()))
        //    {
        //        MsgForm.ShowFrom("没有要审核的数据");
        //        return;
        //    }

        //    frmProcessSelectDept frmProcess = new frmProcessSelectDept();
        //    if (!frmProcess.InputDept(out var proDeptNo, out var feeDeptNo))
        //    {
        //        return;
        //    }

        //    frmLoad.LoadWait(frm, Task.Factory.StartNew(() =>
        //     {
        //         try
        //         {
        //             IBLL.IInOutBLL bll = new BLL.InOutBLL();
        //             bll.GenProcessDetail(flow_id.ToString(), Program.oper.oper_id, proDeptNo, feeDeptNo);

        //             frm.Invoke(new Action(() =>
        //             {
        //                 MsgForm.ShowFrom("审核成功");
        //                 frm.btnSelect_Click(null, null);
        //             }));
        //         }
        //         catch (Exception ex)
        //         {
        //             MsgForm.ShowFrom(ex);
        //         }
        //     }), "正在审核中");
        //}

    }
}
