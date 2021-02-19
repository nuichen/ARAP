using System;
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
using NPOI.SS.Formula.Functions;
using IvyBack.PaymentForm;

namespace IvyBack.BaseForm
{
    public partial class frmSupcustInitial : Form
    {
        public frmSupcustInitial()
        {
            InitializeComponent();

            Helper.GlobalData.InitForm(this);
            
        }
        IBLL.IARAP_SCPaymentBLL bll = new BLL.ARAP_SCPaymentBLL();
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
                    this.Text = "客户期初";
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
       private DataTable data_table = new DataTable();

        private void LoadDataGrid()
        {        
            this.dgvSup.AddColumn("supcust_no", "编号", "", 80, 1, "",false);
            this.dgvSup.AddColumn("delete_flag", "删除", "", 50, 2, "", false);
            this.dgvSup.AddColumn("sup_name", "名称", "", 200, 1, "", false);
            this.dgvSup.AddColumn("sup_fullname", "全名称", "", 240, 1, "", false);
            this.dgvSup.AddColumn("sup_pyname", "拼音码", "", 100, 1, "",false);
            this.dgvSup.AddColumn("account_period", "信用期限", "", 100, 1, "",false);
            this.dgvSup.AddColumn("pay_kind", "借贷", "", 150, 2, "{0:平,1:借,2:贷}", false);
            this.dgvSup.AddColumn("begin_balance", "期初金额", "", 150, 3, "", true);
            this.dgvSup.AddColumn("credit_amt", "信用额度", "", 80, 3, "0.0", false);
            if (_runType == 1)
            {

                
                this.dgvSup.AddColumn("show_num", "标号", "", 60, 2, "", false);
                this.dgvSup.AddColumn("car_id", "车牌号", "", 100, 2, "", false);
            }
            else
            {
                this.dgvSup.AddColumn("is_factory", "是否厂家", "", 100, 2, "{:供应商,1:厂家}", false);
                this.dgvSup.AddColumn("reach_time", "到货时间", "", 100, 2, "", false);
            }
            this.dgvSup.AddColumn("supcust_group", "供应商组", "", 80, 2, "", false);
            this.dgvSup.AddColumn("sup_addr", "地址", "", 200, 2, "", false);
            this.dgvSup.AddColumn("sup_man", "联系人", "", 80, 2, "", false);
            this.dgvSup.AddColumn("sup_tel", "电话", "", 100, 2, "", false);
            this.dgvSup.AddColumn("display_flag", "状态", "", 60, 2, "{0:已停用,1:已启用}", false);
            this.dgvSup.BindCheck("delete_flag");
            if (_runType == 1)
            {
                this.dgvSup.AddColumn("is_retail", "是否零售", "", 80, 2, "{0:否,1:是}", false);
            }

            this.dgvSup.DataSource = page.Assign<bi_t_supcust_info>();
        }
        //private void LoadCb()
        //{
        //    Dictionary<int, string> dic = new Dictionary<int, string>();
        //    if (_runType == 0)
        //    {
        //        dic = new Dictionary<int, string>()
        //        {
        //            {1,"1 — 区域"},
        //            {2,"2 — 类型"},
        //            {3,"3 — 经销方式"}
        //        };
        //    }
        //    else if (_runType == 1)
        //    {
        //        dic = new Dictionary<int, string>()
        //        {
        //            {1,"1 — 区域"},
        //            {2,"2 — 类型"},
        //            {3,"3 — 客户组"}
        //        };
        //    }
        //    this.cb.DisplayMember = "Value";
        //    this.cb.ValueMember = "Key";
        //    this.cb.DataSource = new BindingSource(dic, null);
        //    this.cb.SelectedIndex = 0;
        //}
        private List<bi_t_region_info> region_lis;
        //private void LoadTv()
        //{
        //    if (region_lis != null && region_lis.Count > 0)
        //        this.region_lis.Clear();
        //    this.tv.Nodes.Clear();
        //    int cb_index = Conv.ToInt(cb.SelectedValue);
        //    TreeNode tn = new TreeNode("所有");
        //    if (cb_index == 1)
        //    {
        //        tn = new TreeNode("所有区域");
        //        IBLL.IRegion bll = new BLL.RegionBLL();
        //        string is_cs = runType == 1 ? "C" : "S";
        //        region_lis = bll.GetAllList(is_cs);
        //    }
        //    else if (cb_index == 2)
        //    {
        //        tn = new TreeNode("所有分类");
        //    }
        //    else if (cb_index == 3)
        //    {
        //        if (_runType == 0)
        //            tn = new TreeNode("所有经销");
        //        else if (_runType == 1)
        //            tn = new TreeNode("所有客户组");
        //    }
        //    //tn.Tag = "";
        //    //tn.BackColor = Color.Yellow;
        //    this.tv.Nodes.Add(tn);
        //    LoadTreeView();
        //    //LoadTreeView1();
        //    this.tv.Nodes[0].Expand();
        //    LoadSup();
        //}
        private void LoadSup1()
        {
            if (!MyLove.PermissionsBalidation(this.Text, "18"))
            {

                return;
            }
            page.PageSize = int.MaxValue;
            checkBox1.Checked = false;
            Thread th = new Thread(() =>
            {
                Helper.GlobalData.windows.ShowLoad(this);
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    string cls_no = "";

                    //this.tv.Invoke((MethodInvoker)delegate
                    //{
                    //    if (this.tv.SelectedNode != null)
                    //    {
                    //        if (this.tv.SelectedNode.Tag != null)
                    //        {
                    //            //var item = this.tv.SelectedNode.Tag as Model.bi_t_region_info;
                    //            var item = (Model.bi_t_region_info)this.tv.SelectedNode.Tag;
                    //            cls_no = item.region_no;
                    //        }
                    //    }
                    //});

                    string SelectedNode = "";
                    string Keyword = "";
                    //int ShowStopSup = 0;

                    //this.Invoke((MethodInvoker)delegate
                    //{
                    //    SelectedNode = this.tv.SelectedNode == null ? "" : cls_no;
                    //    Keyword = this.txtKeyword.Text.Trim();
                    //    //ShowStopSup = this.cbShowStopSup.Checked ? 1 : 0;
                    //});
                    var ifpage = pageAll.AsEnumerable().Where(item => item["region_no"].ToString().Substring(0, SelectedNode.Length) == SelectedNode).ToArray();
                    this.Invoke((MethodInvoker)delegate
                    {
                        foreach (DataRow dr in ifpage)
                            this.dgvSup.DataSource.ImportRow(dr);
                        dgvSup.Refresh();

                        //this.lblMaxIndex.Text = page.PageMax.ToString();
                        //this.lblIndex.Text = page.PageIndex.ToString();
                    });
                    //if (_runType == 0)
                    //{
                    //    IBLL.ISCPaymentBLL bll = new BLL.SCPaymentBLL();
                    //    page = bll.GetSupcustBeginbalance(
                    //          SelectedNode,
                    //          Keyword,
                    //          "S",
                    //          page
                    //          );
                    //}
                    //else if (_runType == 1)
                    //{
                    //    Keyword = "";
                    //    IBLL.ISCPaymentBLL bll = new BLL.SCPaymentBLL();
                    //    page = bll.GetSupcustBeginbalance(
                    //          SelectedNode,
                    //          Keyword,
                    //          "C",
                    //          page
                    //          );
                    //}

                    //Conv.AddColorTable(page.Tb, "display_flag");

                    //this.Invoke((MethodInvoker)delegate
                    //{
                    //    this.dgvSup.DataSource = page.Tb;

                    //    this.lblMaxIndex.Text = page.PageMax.ToString();
                    //    this.lblIndex.Text = page.PageIndex.ToString();
                    //});

                    Cursor.Current = Cursors.Default;

                }
                catch (Exception ex)
                {
                    LogHelper.writeLog("LoadSup", "获取商品分类出错!");
                    MsgForm.ShowFrom(ex);
                }
                Helper.GlobalData.windows.CloseLoad(this);

            });
            th.Start();
        }
        DataTable pageAll;
        private void LoadSup()
        {
            if (!MyLove.PermissionsBalidation(this.Text, "18"))
            {

                return;
            }
            page.PageSize = int.MaxValue;
            checkBox1.Checked = false;
            Thread th = new Thread(() =>
            {
                Helper.GlobalData.windows.ShowLoad(this);
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    string cls_no = "";

                    //this.tv.Invoke((MethodInvoker)delegate
                    //{
                    //    if (this.tv.SelectedNode != null)
                    //    {
                    //        if (this.tv.SelectedNode.Tag != null)
                    //        {
                    //            var item = (Model.bi_t_region_info)this.tv.SelectedNode.Tag;
                    //            cls_no = item.region_no;
                    //        }
                    //    }
                    //});

                    string SelectedNode = "";
                    string Keyword = "";
                    //int ShowStopSup = 0;

                    //this.Invoke((MethodInvoker)delegate
                    //{
                    //    SelectedNode = this.tv.SelectedNode == null ? "" : cls_no;
                        Keyword = this.txtKeyword.Text.Trim();
                    //    //ShowStopSup = this.cbShowStopSup.Checked ? 1 : 0;
                    //});

                    if (_runType == 0)
                    {
                        //IBLL.ISCPaymentBLL bll = new BLL.SCPaymentBLL();
                        page = bll.GetSupcustBeginbalance(
                              SelectedNode,
                              Keyword,
                              "S",
                              page
                              );
                    }
                    else if (_runType == 1)
                    {
                        //Keyword = "";
                        //IBLL.ISCPaymentBLL bll = new BLL.SCPaymentBLL();
                        page = bll.GetSupcustBeginbalance(
                              SelectedNode,
                              Keyword,
                              "C",
                              page
                              );
                    }

                    Conv.AddColorTable(page.Tb, "display_flag");

                    this.Invoke((MethodInvoker)delegate
                    {
                        this.dgvSup.DataSource = page.Tb;
                        pageAll = page.Tb;

                        this.lblMaxIndex.Text = page.PageMax.ToString();
                        this.lblIndex.Text = page.PageIndex.ToString();
                    });

                    Cursor.Current = Cursors.Default;

                }
                catch (Exception ex)
                {
                    LogHelper.writeLog("LoadSup", "获取商品分类出错!");
                    MsgForm.ShowFrom(ex);
                }
                Helper.GlobalData.windows.CloseLoad(this);

            });
            th.Start();
        }
        //public void LoadTreeView()
        //{
        //    foreach (var region in region_lis)
        //    {
        //        if (string.IsNullOrEmpty(region.region_parent))
        //        {
        //            TreeNode t = new TreeNode(region.region_name);
        //            t.Tag = region;
        //            //t.BackColor = Color.Yellow;
        //            this.tv.Nodes[0].Nodes.Add(t);
        //        }
        //        else
        //        {
        //            LoadTreeView(region, this.tv.Nodes[0]);
        //        }
        //    }
        //}

        int i = -1;
       
        //public void LoadTreeView(bi_t_region_info region, TreeNode tn)
        //{
           
        //    foreach (TreeNode node in tn.Nodes)
        //    {
        //        i++;
        //        var re = node.Tag as bi_t_region_info;

        //        if (region.region_parent.Equals(re.region_no))
        //        {
        //            TreeNode t = new TreeNode(region.region_name);
        //            t.Tag = region;
        //           // t.BackColor = Color.Yellow;
        //            node.Nodes.Add(t);
                   
        //            break;
        //        }
        //        else
        //        {
        //            LoadTreeView(region, node);
        //        }
        //    }
        //    //if(region.region_parent.Equals(re.region_no))
        //   // LoadTreeView(region.region_no, tn.Nodes[i]);
        //}

        void aa(TreeNode treeNode,DataRow [] dataRows)
        {

        }
        private List<ot_supcust_beginbalance> supcust_lis;
        //public void LoadTreeView1()
        //{
        //    IBLL.ISCPaymentBLL bll = new BLL.SCPaymentBLL();
        //    //DataTable dt;
        //    if (_runType == 0)
        //    {
        //        supcust_lis = bll.GetSupcustInfoList("S");
        //    }
        //    else
        //    {
        //        supcust_lis = bll.GetSupcustInfoList("C");
        //    }

        //    for (int j = 0; j < tv.Nodes[0].GetNodeCount(false); j++)
        //    {
        //        var region = (Model.bi_t_region_info)this.tv.Nodes[0].Nodes[j].Tag;
        //        if (tv.Nodes[0].Nodes[j].GetNodeCount(false) > 0)
        //        {


        //            string s = region.region_no;
        //            var tb1 = supcust_lis.AsEnumerable().Where(item => item.region_no == region.region_no).ToArray();
        //            if (tb1.Length > 0)
        //            {
        //                //TreeNode t1 = new TreeNode("--------");
        //                //t1.Tag = "--";
        //                //tv.Nodes[0].Nodes[j].Nodes.Add(t1);
        //                foreach (ot_supcust_beginbalance dr in tb1)
        //                {
        //                    TreeNode t = new TreeNode(dr.sup_name);
        //                    t.Tag = dr;
        //                    //t.BackColor = Color.Blue;
        //                    tv.Nodes[0].Nodes[j].Nodes.Add(t);
        //                }
        //            }
        //            else
        //                for (int k = 0; k < tv.Nodes[0].Nodes[j].GetNodeCount(false); k++)
        //            {
        //                    var region1 = (Model.bi_t_region_info)this.tv.Nodes[0].Nodes[j].Nodes[k].Tag;
        //                    //string region_no = Helper.Conv.ToString(tv.Nodes[0].Nodes[j].Nodes[k].Tag);
        //                    var tb = supcust_lis.AsEnumerable().Where(item => item.region_no == region.region_no).ToArray();
        //                    foreach (ot_supcust_beginbalance dr in tb)
        //                {                          
        //                    TreeNode t = new TreeNode(dr.sup_name);
        //                        t.Tag = dr;
        //                        //t.BackColor = Color.Blue;
        //                        tv.Nodes[0].Nodes[j].Nodes[k].Nodes.Add(t);
        //                }
                       
        //            }
        //        }
        //        else
        //        {
        //            //var region = (Model.bi_t_region_info)this.tv.Nodes[0].Nodes[j].Tag;
        //            //string region_no= Helper.Conv.ToString(tv.Nodes[0].Nodes[j].Tag);
        //            var tb = supcust_lis.AsEnumerable().Where(item => item.region_no == region.region_no).ToArray();
        //            foreach (ot_supcust_beginbalance dr in tb)
        //            {
        //                TreeNode t = new TreeNode(dr.sup_name);
        //                t.Tag = dr;
        //                //t.BackColor = Color.Blue;
        //                tv.Nodes[0].Nodes[j].Nodes.Add(t);
        //            }
                   
        //        }
        //    }
            //string SelectedNode = "";
            //string Keyword = "";
            //if (_runType == 0)
            //{
            //    IBLL.ISCPaymentBLL bll = new BLL.SCPaymentBLL();
            //    page = bll.GetSupcustBeginbalance(
            //          SelectedNode,
            //          Keyword,
            //          "S",
            //          page
            //          );
            //}
            //else if (_runType == 1)
            //{
            //    //Keyword = "";
            //    IBLL.ISCPaymentBLL bll = new BLL.SCPaymentBLL();
            //    page = bll.GetSupcustBeginbalance(
            //          SelectedNode,
            //          Keyword,
            //          "C",
            //          page
            //          );
            //}
            //TreeNode t = new TreeNode("AAA");
            //t.Tag = region;
            //node.Nodes.Add(t);
        //}


        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.cb.SelectedValue != null)
            //    LoadTv();
        }

        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //LoadSup1();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            
           LoadSup();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            page.HomPage();
           // LoadSup();
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            page.PrePage();
           LoadSup();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            page.NextPage();
            LoadSup();
        }

        private void btnTra_Click(object sender, EventArgs e)
        {
            page.TraPage();
           LoadSup();
        }


       

       

        private void cbShowStopSup_CheckedChanged(object sender, EventArgs e)
        {
           LoadSup();
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        

        private void tsbSave_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            var tb=this.dgvSup.DataSource;
            List<ot_supcust_beginbalance> list = new List<ot_supcust_beginbalance>();
            try
            {
                foreach (DataRow supcust in tb.Rows)
                {
                    ot_supcust_beginbalance temp = new ot_supcust_beginbalance();
                    temp.supcust_no = Convert.ToString(supcust["supcust_no"]);
                    if (Helper.Conv.ToString(supcust["begin_balance"]).Trim() == "")
                        continue;//temp.begin_balance = 0;
                    else
                        temp.begin_balance = Helper.Conv.ToDecimal(supcust["begin_balance"]);
                    
                    if(Convert.ToString(supcust["pay_kind"]) == "0")
                    {
                        temp.pay_kind = 0;
                    }else if(Convert.ToString(supcust["pay_kind"]) == "1")
                    {
                        temp.pay_kind = 1;
                    }
                    else if (Convert.ToString(supcust["pay_kind"]) == "2")
                    {
                        temp.pay_kind = 2;
                    }
                    else
                    {
                        MsgForm.ShowFrom("有非法字符！");
                        return;
                    }
                    temp.pay_kind = Helper.Conv.ToInt(supcust["pay_kind"]);
                    list.Add(temp);
                }
                //IBLL.ISCPaymentBLL bll = new BLL.SCPaymentBLL();
                if (_runType == 0)
                {
                    bll.SavaSupcustInitial(list, "S", Program.oper.oper_id);
                }
                else if (_runType == 1)
                {
                    bll.SavaSupcustInitial(list, "C", Program.oper.oper_id);
                }
                LoadSup();
                MsgForm.ShowFrom("保存成功");
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
                LogHelper.writeLog("frmSupcustInitial=>tsbSave_Click", ex.ToString());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            
            
        }

        private void frmSupcustInitial_Load(object sender, EventArgs e)
        {
            try
            {
                //LoadCb();
                LoadSup();
                LoadDataGrid();
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
                LogHelper.writeLog("frmSupcustInitial=>load", ex.ToString());
            }
            finally
            {

            }
        }

        private void frmSupcustInitial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
       // public List<DataRow> listall = new List<DataRow>();
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            //AddSupcust();
            //frmSupcustSelect frm = new frmSupcustSelect();
            checkBox1.Checked = false;
            List<DataRow> list = new List<DataRow>();
            var frm = new frmSupcustSelect();
            frm.runType = runType;
            if (frm.ShowPayment(out list) == DialogResult.Yes)
            {
                foreach (DataRow row in list)
                {
                    //DataRow row1 = row;
                   // pageAll.ImportRow(row);
                    dgvSup.DataSource.ImportRow(row);
                    
                }

                dgvSup.Refresh();
                //var dt1 = dgvSup.DataSource;
                
                //var tb1 = supcust_lis.AsEnumerable().Where(item1 => item1.region_no == item.region_no).ToArray();
                //foreach (ot_supcust_beginbalance temp in tb1)
                //{
                //    var isadd = dt1.AsEnumerable().Where(item1 => item1["supcust_no"].ToString() == temp.supcust_no).ToArray();
                //    if (isadd.Length > 0)
                //    {
                //        MsgForm.ShowFrom(temp.sup_name + "已经添加，请勿重复添加！");
                //        continue;
                //    }
                //    var row = dgvSup.DataSource.NewRow();
                //    ConvExtension.DataModelToRow(temp, row);
                //    dgvSup.DataSource.Rows.InsertAt(row, 0);
                //}
            }

        }
        //private void AddSupcust()
        //{

        //    this.tv.Invoke((MethodInvoker)delegate
        //    {
        //        Cursor.Current = Cursors.WaitCursor;
        //        string supcust_no;
        //        string is_cs = runType == 0 ? "S" : "C";
        //        string tishi = runType == 0 ? "是否要添加该区域的所有供应商？" : "是否要添加该区域的所有客户？";
        //        string tishi2 = runType == 0 ? "是否要添加所有供应商？" : "是否要添加所有客户？";
        //        var dt1 = dgvSup.DataSource;
        //        if (this.tv.SelectedNode != null)
        //        {
        //            if (this.tv.SelectedNode.Tag != null)
        //            {

        //                Model.bi_t_region_info item = this.tv.SelectedNode.Tag as Model.bi_t_region_info;
        //                if (item != null)
        //                {
        //                    if (MessageBox.Show(tishi, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
        //                    {
        //                        Cursor.Current = Cursors.Default;
        //                        return;
        //                    }
        //                    //string cls_no = item.region_no;
        //                    var tb1 = supcust_lis.AsEnumerable().Where(item1 => item1.region_no == item.region_no).ToArray();
        //                    foreach (ot_supcust_beginbalance temp in tb1)
        //                    {
        //                        var isadd = dt1.AsEnumerable().Where(item1 => item1["supcust_no"].ToString() == temp.supcust_no).ToArray();
        //                        if (isadd.Length > 0)
        //                        {
        //                            MsgForm.ShowFrom(temp.sup_name + "已经添加，请勿重复添加！");
        //                            continue;
        //                        }
        //                        var row = dgvSup.DataSource.NewRow();
        //                        ConvExtension.DataModelToRow(temp, row);
        //                        dgvSup.DataSource.Rows.InsertAt(row, 0);
        //                    }



        //                    //bll.AddSupcustInitial(cls_no, is_cs, Program.oper.oper_id, 2);
        //                }
        //                else
        //                {
        //                    supcust_no = Helper.Conv.ToString(this.tv.SelectedNode.Tag);
        //                    if (supcust_no == "")
        //                    {
        //                        if (MessageBox.Show(tishi2, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
        //                        {
        //                            Cursor.Current = Cursors.Default;
        //                            return;
        //                        }
        //                        else
        //                        {
        //                            foreach (ot_supcust_beginbalance temp in supcust_lis)
        //                            {
        //                                var isadd = dt1.AsEnumerable().Where(item1 => item1["supcust_no"].ToString() == temp.supcust_no).ToArray();
        //                                if (isadd.Length > 0)
        //                                {
        //                                    MsgForm.ShowFrom(temp.sup_name + "已经添加，请勿重复添加！");
        //                                    continue;
        //                                }
        //                                var row = dgvSup.DataSource.NewRow();
        //                                ConvExtension.DataModelToRow(temp, row);
        //                                dgvSup.DataSource.Rows.InsertAt(row, 0);

        //                            }
        //                            //bll.AddSupcustInitial(supcust_no, is_cs, Program.oper.oper_id, 3);
        //                        }

        //                    }
        //                    else
        //                    {

        //                        var row = dgvSup.DataSource.NewRow();
        //                        var item2 = this.tv.SelectedNode.Tag as Model.ot_supcust_beginbalance;
        //                        var isadd = dt1.AsEnumerable().Where(item1 => item1["supcust_no"].ToString() == item2.supcust_no).ToArray();
        //                        if (isadd.Length > 0)
        //                        {
        //                            MsgForm.ShowFrom(item2.sup_name + "已经添加，请勿重复添加！");
        //                            return;
        //                        }
        //                        ConvExtension.DataModelToRow(item2, row);

        //                        dgvSup.DataSource.Rows.InsertAt(row, 0);

        //                        //bll.AddSupcustInitial(supcust_no, is_cs, Program.oper.oper_id, 1);
        //                    }


        //                }

        //                //LoadTv();
        //                Cursor.Current = Cursors.Default;
        //                dgvSup.Refresh();
        //                MsgForm.ShowFrom("添加成功");

        //            }
        //        }

        //    });
        //}

        private void tv_DoubleClick(object sender, EventArgs e)
        {
            //AddSupcust();
        }

        private void dgvSup_CellEndEdit(object sender, string column_name, DataRow row)
        {
            if (_runType == 0)
            {
                if (Helper.Conv.ToDecimal(row["begin_balance"]) > 0)
                {
                    row["pay_kind"] = 2;

                }
                else if (Helper.Conv.ToDecimal(row["begin_balance"]) < 0)
                {
                    row["pay_kind"] = 1;
                    row["begin_balance"] = Math.Abs(Helper.Conv.ToDecimal(row["begin_balance"]));
                }
                else
                {
                    row["pay_kind"] = 0;
                }
            }
            else if (_runType == 1)
            {
                if (Helper.Conv.ToDecimal(row["begin_balance"]) > 0)
                {
                    row["pay_kind"] = 1;

                }
                else if (Helper.Conv.ToDecimal(row["begin_balance"]) < 0)
                {
                    row["pay_kind"] = 2;
                    row["begin_balance"] = Math.Abs(Helper.Conv.ToDecimal(row["begin_balance"]));
                }
                else
                {
                    row["pay_kind"] = 0;
                }
            }

           
            
         }

        private void dgvSup_ClickCell(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            if ("delete_flag".Equals(column_name))
            {
                var delete_flag = row["delete_flag"].ToString();
                if ("0".Equals(delete_flag))
                {
                    row["delete_flag"] = "1";
                }
                else
                {
                    row["delete_flag"] = "0";
                }
                this.dgvSup.Refresh();
            }
            }

        private void tsbCheck_Click_1(object sender, EventArgs e)
        {
            if (YesNoForm.ShowFrom("确认要删除选中吗?") == DialogResult.Yes)
            {
                DataTable dt = dgvSup.DataSource;
                string sup_no="";
                bool is_one = false;
                List<DataRow> lr=new List<DataRow>();
                foreach(DataRow dr in dt.Rows)
                {
                    if (Conv.ToString(dr["delete_flag"]) == "1")
                    {
                        if (is_one == false)
                        {
                            sup_no += "'" + Conv.ToString(dr["supcust_no"]) + "'";
                            is_one = true;
                            lr.Add(dr);
                        }
                        else
                        {
                            sup_no += ",'" + Conv.ToString(dr["supcust_no"]) + "'";
                            lr.Add(dr);
                        }
                    }
                
                }
                if (sup_no == "")
                {
                    MsgForm.ShowFrom("请先选择复选框");
                    return;
                }
                string is_cs = runType == 1 ? "C" : "S";
                bll.DeleteInitial(sup_no, is_cs, DateTime.Now);

                foreach (DataRow dr1 in lr)
                {
                    dt.Rows.Remove(dr1);

                    //pageAll.Rows.Remove(dr1);
                }
                dgvSup.DataSource = dt;
                dgvSup.Refresh();
                MsgForm.ShowFrom("删除成功！");
                //
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool allSelect = checkBox1.Checked;
            if (allSelect)
            {
                for (int i = 0; i < dgvSup.DataSource.Rows.Count; i++)
                {
                    //string s = dgv.Rows[i].Cells["Column1"].Value.ToString();
                    dgvSup.DataSource.Rows[i]["delete_flag"] = "1";
                }
            }
            else
            {
                for (int i = 0; i < dgvSup.DataSource.Rows.Count; i++)
                {
                    //string s = dgv.Rows[i].Cells["Column1"].Value.ToString();
                    dgvSup.DataSource.Rows[i]["delete_flag"] = "0";
                }
            }
            dgvSup.Refresh();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (runType==1)
            {
                bll.CheckSupcustInitial("C");
            }
            else
            {
                bll.CheckSupcustInitial("S");
            }
            
            MsgForm.ShowFrom("审核成功");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (runType == 1)
            {
                bll.NotCheckSupcustInitial("C");
            }
            else
            {
                bll.NotCheckSupcustInitial("S");
            }
         
            MsgForm.ShowFrom("反审成功");
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (YesNoForm.ShowFrom("确认要删除选中吗?") == DialogResult.Yes)
            {
                DataTable dt = dgvSup.DataSource;
                string sup_no = "";
                bool is_one = false;
                List<DataRow> lr = new List<DataRow>();
                foreach (DataRow dr in dt.Rows)
                {
                    if (Conv.ToString(dr["delete_flag"]) == "1")
                    {
                        if (is_one == false)
                        {
                            sup_no += "'" + Conv.ToString(dr["supcust_no"]) + "'";
                            is_one = true;
                            lr.Add(dr);
                        }
                        else
                        {
                            sup_no += ",'" + Conv.ToString(dr["supcust_no"]) + "'";
                            lr.Add(dr);
                        }
                    }

                }
                if (sup_no == "")
                {
                    MsgForm.ShowFrom("请先选择复选框");
                    return;
                }
                string is_cs = runType == 1 ? "C" : "S";
                bll.DeleteInitial(sup_no, is_cs, DateTime.Now);

                foreach (DataRow dr1 in lr)
                {
                    dt.Rows.Remove(dr1);

                    //pageAll.Rows.Remove(dr1);
                }
                dgvSup.DataSource = dt;
                dgvSup.Refresh();
                MsgForm.ShowFrom("删除成功！");
                //
            }
        }


    }
}
