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


namespace IvyBack.PaymentForm
{
    public partial class frmSupcustSelect : Form
    {
        public frmSupcustSelect()
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
                    this.Text = "客户选择";
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
        IBLL.IARAP_SCPaymentBLL bll = new BLL.ARAP_SCPaymentBLL();

        private void LoadDataGrid()
        {
            this.dgvSup.AddColumn("supcust_no", "编号", "", 80, 1, "");
            this.dgvSup.AddColumn("sup_name", "名称", "", 200, 1, "");
            this.dgvSup.AddColumn("sup_fullname", "全名称", "", 240, 1, "");
            this.dgvSup.AddColumn("sup_pyname", "拼音码", "", 100, 1, "");
            this.dgvSup.AddColumn("account_period", "信用期限", "", 100, 1, "");
            this.dgvSup.AddColumn("pay_kind", "借贷", "", 150, 2, "{0:平,1:借,2:贷}");
            this.dgvSup.AddColumn("begin_balance", "期初金额", "", 150, 3, "");
            this.dgvSup.AddColumn("credit_amt", "信用额度", "", 80, 3, "0.0");
            if (_runType == 1)
            {              
                this.dgvSup.AddColumn("show_num", "标号", "", 60, 2, "");
                this.dgvSup.AddColumn("car_id", "车牌号", "", 100, 2, "");
            }
            else
            {
                this.dgvSup.AddColumn("is_factory", "是否厂家", "", 100, 2, "{:供应商,1:厂家}");
                this.dgvSup.AddColumn("reach_time", "到货时间", "", 100, 2, "");
            }
            this.dgvSup.AddColumn("supcust_group", "供应商组", "", 80, 2, "");
            this.dgvSup.AddColumn("sup_addr", "地址", "", 200, 2, "");
            this.dgvSup.AddColumn("sup_man", "联系人", "", 80, 2, "");
            this.dgvSup.AddColumn("sup_tel", "电话", "", 100, 2, "");
            this.dgvSup.AddColumn("delete_flag", "删除标记", "", 80, 3, "0.0");
            this.dgvSup.AddColumn("display_flag", "状态", "", 60, 2, "{0:已停用,1:已启用}");


            if (_runType == 1)
            {
                this.dgvSup.AddColumn("is_retail", "是否零售", "", 80, 2, "{0:否,1:是}");
            }

            this.dgvSup.DataSource = page.Assign<ot_supcust_beginbalance>();
        }
        private void LoadCb()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            if (_runType == 0)
            {
                dic = new Dictionary<int, string>()
                {
                    {1,"1 — 区域"},
                    {2,"2 — 类型"},
                    {3,"3 — 经销方式"}
                };
            }
            else if (_runType == 1)
            {
                dic = new Dictionary<int, string>()
                {
                    {1,"1 — 区域"},
                    {2,"2 — 类型"},
                    {3,"3 — 客户组"}
                };
            }
            this.cb.DisplayMember = "Value";
            this.cb.ValueMember = "Key";
            this.cb.DataSource = new BindingSource(dic, null);
            this.cb.SelectedIndex = 0;
        }
        private List<bi_t_region_info> region_lis;
        private void LoadTv()
        {
            if (region_lis != null && region_lis.Count > 0)
                this.region_lis.Clear();
            this.tv.Nodes.Clear();
            int cb_index = Conv.ToInt(cb.SelectedValue);
            TreeNode tn = new TreeNode("所有");
            if (cb_index == 1)
            {
                tn = new TreeNode("所有区域");
                IBLL.IRegion bll = new BLL.RegionBLL();
                string is_cs = runType == 1 ? "C" : "S";
                region_lis = bll.GetAllList(is_cs);
            }
            else if (cb_index == 2)
            {
                tn = new TreeNode("所有分类");
            }
            else if (cb_index == 3)
            {
                if (_runType == 0)
                    tn = new TreeNode("所有经销");
                else if (_runType == 1)
                    tn = new TreeNode("所有客户组");
            }

            this.tv.Nodes.Add(tn);
            LoadTreeView();
            this.tv.Nodes[0].Expand();
        }
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

                    this.tv.Invoke((MethodInvoker)delegate
                    {
                        if (this.tv.SelectedNode != null)
                        {
                            if (this.tv.SelectedNode.Tag != null)
                            {
                                var item = (Model.bi_t_region_info)this.tv.SelectedNode.Tag;
                                cls_no = item.region_no;
                            }
                        }
                    });

                    string SelectedNode = "";
                    string Keyword = "";
                    int ShowStopSup = 0;

                    this.Invoke((MethodInvoker)delegate
                    {
                        SelectedNode = this.tv.SelectedNode == null ? "" : cls_no;
                        Keyword = this.txtKeyword.Text.Trim();
                        //ShowStopSup = this.cbShowStopSup.Checked ? 1 : 0;
                    });

                    if (_runType == 0)
                    {
                        //IBLL.ISCPaymentBLL bll = new BLL.SCPaymentBLL();
                        page = bll.GetSupcustList(
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
                        page = bll.GetSupcustList(
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

                        this.lblMaxIndex.Text = page.PageMax.ToString();
                        this.lblIndex.Text = page.PageIndex.ToString();
                    });

                    Cursor.Current = Cursors.Default;

                }
                catch (Exception ex)
                {
                    LogHelper.writeLog("LoadSup", "获取出错!");
                    MsgForm.ShowFrom(ex);
                }
                Helper.GlobalData.windows.CloseLoad(this);

            });
            th.Start();
        }
        public void LoadTreeView()
        {
            foreach (var region in region_lis)
            {
                if (string.IsNullOrEmpty(region.region_parent))
                {
                    TreeNode t = new TreeNode(region.region_name);
                    t.Tag = region;
                    this.tv.Nodes[0].Nodes.Add(t);
                }
                else
                {
                    LoadTreeView(region, this.tv.Nodes[0]);
                }
            }
        }
        public void LoadTreeView(bi_t_region_info region, TreeNode tn)
        {
            foreach (TreeNode node in tn.Nodes)
            {
                var re = node.Tag as bi_t_region_info;

                if (region.region_parent.Equals(re.region_no))
                {
                    TreeNode t = new TreeNode(region.region_name);
                    t.Tag = region;
                    node.Nodes.Add(t);
                    break;
                }
                else
                {
                    LoadTreeView(region, node);
                }
            }
        }

        private void frmSupcust_Load(object sender, EventArgs e)
        {
            try
            {
                LoadCb();
                LoadDataGrid();
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
                LogHelper.writeLog("frmSupcustSelect=>load", ex.ToString());
            }
            finally
            {

            }
        }

        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cb.SelectedValue != null)
                LoadTv();
        }

        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            LoadSup();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            LoadSup();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            page.HomPage();
            LoadSup();
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



        private void tsbDel_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(this.Text, "02"))
            {

                return;
            }
            DataRow dr = this.dgvSup.CurrentRow();
            if (dr == null) return;

            if (YesNoForm.ShowFrom("确认要删除吗？") == DialogResult.Yes)
            {
                if (_runType == 0)
                {
                    IBLL.ISup bll = new BLL.SupBLL();
                    var sup = DB.ReflectionHelper.DataRowToModel<bi_t_supcust_info>(dr);
                    bll.Del(sup);
                }
                else if (_runType == 1)
                {
                    IBLL.ICus bll = new BLL.CusBLL();
                    var sup = DB.ReflectionHelper.DataRowToModel<bi_t_supcust_info>(dr);
                    bll.Del(sup);
                }
                LoadSup();
            }
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
            LoadSup();
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_DoubleClink(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            tsbUpload_Click(sender, e);
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool allSelect = checkBox1.Checked;
            if (allSelect)
            {
                for (int i = 0; i < dgvSup.DataSource.Rows.Count; i++)
                {
                    //string s = dgv.Rows[i].Cells["Column1"].Value.ToString();
                    dgvSup.DataSource.Rows[i]["select_flag"] = "1";
                }
            }
            else
            {
                for (int i = 0; i < dgvSup.DataSource.Rows.Count; i++)
                {
                    //string s = dgv.Rows[i].Cells["Column1"].Value.ToString();
                    dgvSup.DataSource.Rows[i]["select_flag"] = "0";
                }
            }
            dgvSup.Refresh();
        }
    }
}
