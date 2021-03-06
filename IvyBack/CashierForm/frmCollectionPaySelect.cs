﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IvyBack.Helper;
using Model;
using System.Threading;
using IvyBack.BLL;
using IvyBack.IBLL;
using Model.StaticType;
using System.Security.Cryptography.X509Certificates;

namespace IvyBack.PaymentForm
{
    public partial class frmCollectionPaySelect : Form
    {
        public frmCollectionPaySelect()
        {
            InitializeComponent();
            Helper.GlobalData.InitForm(this);

        }
        private int _runType = 0;//0:供应商 1:客户
        public int runType
        {
            get
            {
                return _runType;
            }
            set
            {
                if (value == 1)
                {
                    //this.Text = "客户档案";
                    //this.cbShowStopSup.Text = "显示已停止往来的客户";
                    //this.tsmiSupBindItem.Visible = false;
                }
                else
                {

                }
                _runType = value;
            }
        }
        Page<ot_supcust_beginbalance> page = new Page<ot_supcust_beginbalance>();
        DataTable tb;
        private void LoadDataGrid()
        {
            tb = new DataTable();
            tb.Columns.Add("visa_id");
            tb.Columns.Add("supcust_no");
            tb.Columns.Add("sup_name");
            tb.Columns.Add("voucher_no");
            tb.Columns.Add("oper_date");
            tb.Columns.Add("pay_way");
            tb.Columns.Add("pay_amount", typeof(decimal));

            dgvSup.AddColumn("visa_id", "账户", "", 150, 2, "");
            dgvSup.AddColumn("supcust_no", "客户编号", "", 100, 2, "");
            dgvSup.AddColumn("sup_name", "客户名称", "", 150, 2, "");
            dgvSup.AddColumn("voucher_no", "结算单号", "", 120, 2, "");
            dgvSup.AddColumn("oper_date", "结算单日期", "", 120, 2, "yyyy-MM-dd");
            dgvSup.AddColumn("pay_amount", "结算金额", "", 120, 3, "0.00");
            this.dgvSup.DataSource = page.Assign<DataTable>();
        }
        //private void LoadCb()
        //{
        //    Dictionary<int, string> dic = new Dictionary<int, string>();
        //    dic = new Dictionary<int, string>()
        //        {
        //            {1,"1 — 业务类型分类"}
        //        };
        //    this.cb.DisplayMember = "Value";
        //    this.cb.ValueMember = "Key";
        //    this.cb.DataSource = new BindingSource(dic, null);
        //    this.cb.SelectedIndex = 0;
        //}
        //private List<bi_t_region_info> region_lis;
        //private void LoadTv()
        //{
        //    //if (region_lis != null && region_lis.Count > 0)
        //    //    this.region_lis.Clear();
        //    this.tv.Nodes.Clear();
        //    int cb_index = Conv.ToInt(cb.SelectedValue);
        //    TreeNode tn = new TreeNode("所有");
        //    if (cb_index == 1)
        //    {
        //        tn = new TreeNode("所有类型");
        //        //    IBLL.IRegion bll = new BLL.RegionBLL();
        //        //    region_lis = bll.GetAllList();
        //    }
        //    //else if (cb_index == 2)
        //    //{
        //    //    tn = new TreeNode("所有日期");
        //    //}
        //    tn.Tag = "";
        //    this.tv.Nodes.Add(tn);
        //    LoadTreeView();
        //    this.tv.Nodes[0].Expand();
        //    IBLL.ICusSettle csbll = new BLL.CusSettle();
        //    if (runType == 1)
        //    {
        //        tb1 = csbll.GetAccountFlows(new Model.rp_t_accout_payrec_flow()
        //        {
        //            supcust_no = cus,
        //            supcust_flag = "C",
        //        });
        //    }
        //    else
        //    {
        //        tb1 = csbll.GetAccountFlows(new Model.rp_t_accout_payrec_flow()
        //        {
        //            supcust_no = cus,
        //            supcust_flag = "S",
        //        });
        //    }
        //    for(int i = 0; i < tb1.Rows.Count; i++)
        //    {
        //        if (Conv.ToDecimal(tb1.Rows[i]["pay_type"]) == -1)
        //        {
        //            tb1.Rows[i]["sheet_amount"] = 0 - Conv.ToDecimal(tb1.Rows[i]["sheet_amount"]);
        //        }
        //    }

        //}
        public DataTable tb1;
        //private void LoadSup()
        //{
        //    if (!MyLove.PermissionsBalidation(this.Text, "18"))
        //    {

        //        return;
        //    }
        //    page.PageSize = int.MaxValue;

        //    Thread th = new Thread(() =>
        //    {
        //        Helper.GlobalData.windows.ShowLoad(this);
        //        try
        //        {
        //            Cursor.Current = Cursors.WaitCursor;
        //            string cls_no = "";

        //            this.tv.Invoke((MethodInvoker)delegate
        //            {
        //                if (this.tv.SelectedNode != null)
        //                {
        //                    if (this.tv.SelectedNode.Tag != null)
        //                    {
        //                        var item = (Model.bi_t_region_info)this.tv.SelectedNode.Tag;
        //                        cls_no = item.region_no;
        //                    }
        //                }
        //            });

        //            string SelectedNode = "";
        //            string Keyword = "";
        //            int ShowStopSup = 0;

        //            this.Invoke((MethodInvoker)delegate
        //            {
        //                SelectedNode = this.tv.SelectedNode == null ? "" : cls_no;
        //                Keyword = this.txtKeyword.Text.Trim();
        //                //ShowStopSup = this.cbShowStopSup.Checked ? 1 : 0;
        //            });





        //            this.Invoke((MethodInvoker)delegate
        //            {
        //                if (tb.Rows.Count != 0)
        //                {
        //                    this.Invoke((MethodInvoker)delegate
        //                    {
        //                        tb.Clear();
        //                        this.dgvSup.DataSource = tb;
        //                    });

        //                }
        //                DateTime date1 = Conv.ToDateTime(dateTextBox1.Text);
        //                DateTime date2 = Conv.ToDateTime(dateTextBox2.Text);
        //                if (txtKeyword.Text.Trim() == "")
        //                {
        //                    foreach (DataRow dr in tb1.Rows)
        //                    {
        //                        var r = tb.NewRow();
        //                        //r["select_flag"] = "1";
        //                        r["path"] = dr["pay_type"];
        //                        r["voucher_no"] = dr["voucher_no"];
        //                        r["oper_date"] = dr["oper_date"];
        //                        r["voucher_first"] = dr["trans_no"];
        //                        //if (dr["pay_type"].ToString() == "-1" || dr["pay_type"].ToString() == "-")
        //                        //{
        //                        //    r["sheet_amount"] = 0 - Conv.ToDecimal(dr["sheet_amount"]);
        //                        //}
        //                        //else
        //                            r["sheet_amount"] = dr["sheet_amount"];
        //                        r["pay_date"] = dr["pay_date"];

        //                        r["paid_amount"] = dr["已核销金额"];
        //                        r["paid_free"] = "0.00";
        //                        r["yf_amount"] = Conv.ToDecimal(r["sheet_amount"]) - Conv.ToDecimal(dr["已核销金额"]);
        //                        r["pay_free"] = 0.00;
        //                        r["memo"] = dr["memo"];
        //                        r["pay_amount"] = "0.00";
        //                        r["num1"] = Conv.ToDecimal(r["sheet_amount"]) - Conv.ToDecimal(dr["已核销金额"]);

        //                        tb.Rows.Add(r);
        //                    }
        //                    this.Invoke((MethodInvoker)delegate
        //                    {
        //                        this.dgvSup.DataSource = tb;

        //                        this.lblMaxIndex.Text = page.PageMax.ToString();
        //                        this.lblIndex.Text = page.PageIndex.ToString();
        //                    });
        //                }
        //                else
        //                {

        //                    var tb2 = tb1.AsEnumerable().Where(item => Conv.ToString(item["voucher_no"]).Substring(0, txtKeyword.Text.Trim().Length) == txtKeyword.Text.Trim()).ToArray();
        //                    foreach (DataRow dr in tb2)
        //                    {
        //                        var r = tb.NewRow();
        //                        //r["select_flag"] = "1";
        //                        r["path"] = dr["pay_type"];
        //                        r["voucher_no"] = dr["voucher_no"];
        //                        r["oper_date"] = dr["oper_date"];
        //                        r["voucher_first"] = dr["trans_no"];
        //                        //if (dr["pay_type"].ToString() == "-1" || dr["pay_type"].ToString() == "-")
        //                        //{
        //                        //    r["sheet_amount"] = 0 - Conv.ToDecimal(dr["sheet_amount"]);
        //                        //}
        //                        //else
        //                            r["sheet_amount"] = dr["sheet_amount"];
        //                        r["pay_date"] = dr["pay_date"];
        //                        r["paid_amount"] = Conv.ToDecimal(dr["已核销金额"]);
        //                        r["paid_free"] = "0.00";
        //                        r["yf_amount"] = Conv.ToDecimal(r["sheet_amount"]) - Conv.ToDecimal(dr["已核销金额"]);
        //                        r["pay_free"] = 0.00;
        //                        r["memo"] = dr["memo"];
        //                        r["pay_amount"] = "0.00";
        //                        r["num1"] = Conv.ToDecimal(r["sheet_amount"]) - Conv.ToDecimal(dr["已核销金额"]);

        //                        tb.Rows.Add(r);
        //                    }
        //                    this.Invoke((MethodInvoker)delegate
        //                    {
        //                        this.dgvSup.DataSource = tb;

        //                        this.lblMaxIndex.Text = page.PageMax.ToString();
        //                        this.lblIndex.Text = page.PageIndex.ToString();
        //                    });
        //                }

        //                //Conv.AddColorTable(tb, "display_flag");




        //                Cursor.Current = Cursors.Default;

        //            });
        //        }
        //        catch (Exception ex)
        //        {
        //            LogHelper.writeLog("LoadSup", "获取数据出错!");
        //            MsgForm.ShowFrom(ex);
        //        }
        //        Helper.GlobalData.windows.CloseLoad(this);

        //    });
        //    th.Start();
        //}
        public string supcust_no="";
        public string visa_id = "";
        public string start_date = "";
        public string end_date = "";
        private void LoadSup1()
        {
            if (!MyLove.PermissionsBalidation(this.Text, "18"))
            {

                return;
            }
            page.PageSize = int.MaxValue;

            Thread th = new Thread(() =>
            {
                Helper.GlobalData.windows.ShowLoad(this);
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    IBLL.ICashierBLL bll = new BLL.CashierBLL();
                    if (runType == 1)
                    {
                        tb1 = bll.GetCollectionList("CP",supcust_no,visa_id,start_date,end_date);
                    }
                    else
                    {
                        tb1 = bll.GetCollectionList("RP", supcust_no, visa_id, start_date, end_date);
                    }
                    //for (int i = 0; i < tb1.Rows.Count; i++)
                    //{
                    //    if (Conv.ToDecimal(tb1.Rows[i]["pay_type"]) == -1)
                    //    {
                    //        tb1.Rows[i]["sheet_amount"] = 0 - Conv.ToDecimal(tb1.Rows[i]["sheet_amount"]);
                    //    }
                    //}
                    foreach (DataRow dr in tb1.Rows)
                            {
                                var r = tb.NewRow();
                                r["visa_id"] = dr["visa_id"];
                                r["supcust_no"] = dr["supcust_no"];
                                r["sup_name"] = dr["sup_name"];
                                r["voucher_no"] = dr["voucher_no"];
                                r["oper_date"] = dr["oper_date"];
                                r["oper_date"] = dr["oper_date"];
                                r["pay_way"] = dr["pay_way"];
                                r["pay_amount"] = dr["pay_amount"];
                            tb.Rows.Add(r);
                            }
                            this.Invoke((MethodInvoker)delegate
                            {
                                this.dgvSup.DataSource = tb;

                                this.lblMaxIndex.Text = page.PageMax.ToString();
                                this.lblIndex.Text = page.PageIndex.ToString();
                            });                   
                        Cursor.Current = Cursors.Default;
                }
                catch (Exception ex)
                {
                    LogHelper.writeLog("LoadSup1", "获取数据出错!");
                    MsgForm.ShowFrom(ex);
                }
                Helper.GlobalData.windows.CloseLoad(this);

            });
            th.Start();
        }
        //public void LoadTreeView()
        //{
        //    Dictionary<string, string> all_type_dic = new Dictionary<string, string>();
        //    if (runType == 1)
        //    {
        //        all_type_dic.Add("CP", "收款单");
        //        all_type_dic.Add("I", "销售出库");
        //        all_type_dic.Add("D", "退货入库");
        //        all_type_dic.Add("CM", "其它应收");
        //    }
        //    else
        //    {
        //        all_type_dic.Add("RP", "付款单");
        //        all_type_dic.Add("A", "进货入库");
        //        all_type_dic.Add("F", "退货出库");
        //        all_type_dic.Add("GM", "其它应付");
        //    }
        //    foreach (var r in all_type_dic)
        //    {
        //        TreeNode t = new TreeNode(r.Value);
        //        t.Tag = r.Key;
        //        this.tv.Nodes[0].Nodes.Add(t);
        //    }
        //    //foreach (var region in region_lis)
        //    //{
        //    //    if (string.IsNullOrEmpty(region.region_parent))
        //    //    {
        //    //        TreeNode t = new TreeNode(region.region_name);
        //    //        t.Tag = region;
        //    //        this.tv.Nodes[0].Nodes.Add(t);
        //    //    }
        //    //    else
        //    //    {
        //    //        LoadTreeView(region, this.tv.Nodes[0]);
        //    //    }
        //    //}
        //}
        //public void LoadTreeView(bi_t_region_info region, TreeNode tn)
        //{
        //    foreach (TreeNode node in tn.Nodes)
        //    {
        //        var re = node.Tag as bi_t_region_info;

        //        if (region.region_parent.Equals(re.region_no))
        //        {
        //            TreeNode t = new TreeNode(region.region_name);
        //            t.Tag = region;
        //            node.Nodes.Add(t);
        //            break;
        //        }
        //        else
        //        {
        //            LoadTreeView(region, node);
        //        }
        //    }
        //}

        private void frmSupcust_Load(object sender, EventArgs e)
        {
            try
            {
                //LoadCb();
                LoadDataGrid();
                LoadSup1();
                //this.dateTextBox1.Text = System.DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                //this.dateTextBox2.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
                LogHelper.writeLog("frmSupcust=>load", ex.ToString());
            }
            finally
            {

            }
        }

        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.cb.SelectedValue != null)
            //    LoadTv();
        }

        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            LoadSup1();
            //if (!MyLove.PermissionsBalidation(this.Text, "18"))
            //{

            //    return;
            //}
            //page.PageSize = int.MaxValue;

            //Thread th = new Thread(() =>
            //{
            //    Helper.GlobalData.windows.ShowLoad(this);
            //    try
            //    {
            //        Cursor.Current = Cursors.WaitCursor;
            //        string sheet_id = "";
            //        this.tv.Invoke((MethodInvoker)delegate
            //        {
            //            if (this.tv.SelectedNode != null)
            //            {
            //                if (this.tv.SelectedNode.Tag != null)
            //                {
            //                    sheet_id=Conv.ToString(this.tv.SelectedNode.Tag);
            //                }
            //            }
            //        });
            //        MsgForm.ShowFrom(sheet_id);
                    //string SelectedNode = "";
                    //string Keyword = "";
                    //int ShowStopSup = 0;





                    //this.Invoke((MethodInvoker)delegate
                    //{
                    //    if (tb.Rows.Count != 0)
                    //    {
                    //        this.Invoke((MethodInvoker)delegate
                    //        {
                    //            tb.Clear();
                    //            this.dgvSup.DataSource = tb;
                    //        });

                    //    }
                    //    DateTime date1 = Conv.ToDateTime(dateTextBox1.Text);
                    //    DateTime date2 = Conv.ToDateTime(dateTextBox2.Text);
                    //    if (txtKeyword.Text.Trim() == "")
                    //    {
                    //        foreach (DataRow dr in tb1.Rows)
                    //        {
                    //            var r = tb.NewRow();
                    //            //r["select_flag"] = "1";
                    //            r["path"] = dr["pay_type"];
                    //            r["voucher_no"] = dr["voucher_no"];
                    //            r["oper_date"] = dr["oper_date"];
                    //            r["voucher_first"] = dr["trans_no"];
                    //            if (dr["pay_type"].ToString() == "-1" || dr["pay_type"].ToString() == "-")
                    //            {
                    //                r["sheet_amount"] = 0 - Conv.ToDecimal(dr["sheet_amount"]);
                    //            }
                    //            else
                    //                r["sheet_amount"] = dr["sheet_amount"];
                    //            r["pay_date"] = dr["pay_date"];

                    //            r["paid_amount"] = dr["已核销金额"];
                    //            r["paid_free"] = "0.00";
                    //            r["yf_amount"] = Conv.ToDecimal(r["sheet_amount"]) - Conv.ToDecimal(dr["已核销金额"]);
                    //            r["pay_free"] = 0.00;
                    //            r["memo"] = dr["memo"];
                    //            r["pay_amount"] = "0.00";
                    //            r["num1"] = Conv.ToDecimal(r["sheet_amount"]) - Conv.ToDecimal(dr["已核销金额"]);

                    //            tb.Rows.Add(r);
                    //        }
                    //        this.Invoke((MethodInvoker)delegate
                    //        {
                    //            this.dgvSup.DataSource = tb;

                    //            this.lblMaxIndex.Text = page.PageMax.ToString();
                    //            this.lblIndex.Text = page.PageIndex.ToString();
                    //        });
                    //    }
                    //    else
                    //    {

                    //        var tb2 = tb1.AsEnumerable().Where(item => Conv.ToString(item["voucher_no"]).Substring(0, txtKeyword.Text.Trim().Length) == txtKeyword.Text.Trim()).ToArray();
                    //        foreach (DataRow dr in tb2)
                    //        {
                    //            var r = tb.NewRow();
                    //            //r["select_flag"] = "1";
                    //            r["path"] = dr["pay_type"];
                    //            r["voucher_no"] = dr["voucher_no"];
                    //            r["oper_date"] = dr["oper_date"];
                    //            r["voucher_first"] = dr["trans_no"];
                    //            if (dr["pay_type"].ToString() == "-1" || dr["pay_type"].ToString() == "-")
                    //            {
                    //                r["sheet_amount"] = 0 - Conv.ToDecimal(dr["sheet_amount"]);
                    //            }
                    //            else
                    //                r["sheet_amount"] = dr["sheet_amount"];
                    //            r["pay_date"] = dr["pay_date"];
                    //            r["paid_amount"] = "0.00";
                    //            r["paid_free"] = "0.00";
                    //            r["yf_amount"] = Conv.ToDecimal(dr["sheet_amount"]) - Conv.ToDecimal(dr["已核销金额"]);
                    //            r["pay_free"] = 0.00;
                    //            r["memo"] = dr["memo"];
                    //            r["pay_amount"] = "0.00";
                    //            r["num1"] = Conv.ToDecimal(dr["sheet_amount"]) - Conv.ToDecimal(dr["已核销金额"]);

                    //            tb.Rows.Add(r);
                    //        }
                    //        this.Invoke((MethodInvoker)delegate
                    //        {
                    //            this.dgvSup.DataSource = tb;

                    //            this.lblMaxIndex.Text = page.PageMax.ToString();
                    //            this.lblIndex.Text = page.PageIndex.ToString();
                    //        });
                    //    }

                    //    //Conv.AddColorTable(tb, "display_flag");




                    //    Cursor.Current = Cursors.Default;

            //        //});
            //    }
            //    catch (Exception ex)
            //    {
            //        LogHelper.writeLog("LoadSup", "获取数据出错!");
            //        MsgForm.ShowFrom(ex);
            //    }
            //    Helper.GlobalData.windows.CloseLoad(this);

            //});
            //th.Start();
            //LoadSup();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            LoadSup1();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            page.HomPage();
           // LoadSup();
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            page.PrePage();
          //  LoadSup();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            page.NextPage();
            LoadSup1();
        }

        private void btnTra_Click(object sender, EventArgs e)
        {
            page.TraPage();
            LoadSup1();
        }



        private void tsbDel_Click(object sender, EventArgs e)
        {
            //if (!MyLove.PermissionsBalidation(this.Text, "02"))
            //{

            //    return;
            //}
            //DataRow dr = this.dgvSup.CurrentRow();
            //if (dr == null) return;

            //if (YesNoForm.ShowFrom("确认要删除吗？") == DialogResult.Yes)
            //{
            //    if (_runType == 0)
            //    {
            //        IBLL.ISup bll = new BLL.SupBLL();
            //        var sup = DB.ReflectionHelper.DataRowToModel<bi_t_supcust_info>(dr);
            //        bll.Del(sup);
            //    }
            //    else if (_runType == 1)
            //    {
            //        IBLL.ICus bll = new BLL.CusBLL();
            //        var sup = DB.ReflectionHelper.DataRowToModel<bi_t_supcust_info>(dr);
            //        bll.Del(sup);
            //    }
            //    LoadSup1();
            //}
        }

        private void tsbUpload_Click(object sender, EventArgs e)
        {
            //if (!MyLove.PermissionsBalidation(this.Text, "14"))
            //{

            //    return;
            //}
            //DataRow dr = this.dgvSup.CurrentRow();
            //if (dr == null) return;
            //var sup = DB.ReflectionHelper.DataRowToModel<bi_t_supcust_info>(dr);

            //dynamic frm;
            ////if (runType == 0)
            ////{
            ////    frm = new frmSupInfoChange();
            ////}
            ////else
            ////{
            ////    frm = new frmCusInfoChange();
            ////}

            //frm.supcust = sup;
            //frm.ShowDialog();
            //LoadSup();
        }

        private void cbShowStopSup_CheckedChanged(object sender, EventArgs e)
        {
            LoadSup1();
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_DoubleClink(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            //tsbUpload_Click(sender, e);
        }

        private void frmSupcust_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                tsbAdd_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F3)
            {
                tsbUpload_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F4)
            {
                tsbDel_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void tsmiSupBindItem_Click(object sender, EventArgs e)
        {
            //frmSupBindItem frm = new frmSupBindItem();
            //GlobalData.windows.ShowForm(frm);
        }

        private void tsbImportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (_runType == 0)
                {
                    ImportSupExcel();
                }
                else
                {
                    ImportCusExcel();
                }

            }
            catch (Exception exception)
            {
                MsgForm.ShowFrom(exception);
            }
        }

        private void ImportCusExcel()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel|*.xls;*.xlsx";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ExcelHelper excel = new ExcelHelper();

                string mustFileds = "客户编号,客户名称,客户区域,价格级别";
                Dictionary<string, string> fileCorrDic = new Dictionary<string, string>()
                {
                    {"supcust_no","客户编号"},
                    {"sup_name","客户名称"},
                    {"region_no","客户区域"},
                    {"sup_man","联系人"},
                    {"sup_tel","电话"},
                    {"credit_amt","信誉额度"},
                    {"sup_addr","地址"},
                    {"cust_level","价格级别"},
                };
                Action<bi_t_supcust_info, DataRow> createAction = (item, row) =>
                {
                };

                List<bi_t_supcust_info> lis = excel.ImportExcel(ofd.FileName, mustFileds, fileCorrDic, createAction);
                ICus bll = new CusBLL();
                bll.Adds(lis);

                MsgForm.ShowFrom("导入成功!");
            }
        }
        private void ImportSupExcel()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel|*.xls;*.xlsx";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ExcelHelper excel = new ExcelHelper();

                string mustFileds = "供应商编号,供应商名称,供应商区域";
                Dictionary<string, string> fileCorrDic = new Dictionary<string, string>()
                {
                    {"supcust_no","供应商编号"},
                    {"sup_name","供应商名称"},
                    {"region_no","供应商区域"},
                    {"sup_man","联系人"},
                    {"sup_tel","电话"},
                    {"sup_addr","地址"},
                };
                Action<bi_t_supcust_info, DataRow> createAction = (item, row) =>
                {
                };

                List<bi_t_supcust_info> lis = excel.ImportExcel(ofd.FileName, mustFileds, fileCorrDic, createAction);
                ISup bll = new SupBLL();
                bll.Adds(lis);

                MsgForm.ShowFrom("导入成功!");
            }
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            //if (!MyLove.PermissionsBalidation(this.Text, "01"))
            //{

            //    return;
            //}

            //dynamic frm;
            //if (runType == 0)
            //{
            //    frm = new frmSupInfoChange();
            //}
            //else
            //{
            //    frm = new frmCusInfoChange();
            //}

            //if (tv.SelectedNode?.Tag != null)
            //{
            //    var region = (Model.bi_t_region_info)tv.SelectedNode.Tag;
            //    frm.region_no = region.region_no;
            //}

            //frm.ShowDialog();
            //LoadSup();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }
        public DialogResult ShowPayment(out List<DataRow> rowLis)
        {

            this.ShowDialog();
            
            rowLis = this.dgvSup.GetSelectDatas();
            return this.DialogResult;



        }
    }
}
