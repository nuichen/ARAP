using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IvyBack.Helper;

namespace IvyBack.PaymentForm
{
    public partial class frmAgingGroup : Form
    {
        private string _runType = "C";//0:供应商 1:客户
        public string runType
        {
            get
            {
                return _runType;
            }
            set
            {
                if (value == "S")
                {
                    this.Text = "供应商账龄分组";
                    //this.cbShowStopSup.Text = "显示已停止往来的客户";
                    //this.tsmiSupBindItem.Visible = false;
                }
                else
                {

                }
                _runType = value;
            }
        }
        public frmAgingGroup(string is_cs)
        {
            InitializeComponent();
            dgv.AutoGenerateColumns = false;
            runType = is_cs;
            LoadEDG();
        }
        IBLL.IARAP_SCPaymentBLL paymentbll = new BLL.ARAP_SCPaymentBLL();
        public void LoadEDG()
        {
            //Dictionary<string, string> type_dic = new Dictionary<string, string>()
            //{
            //    { "1","客户"},
            //    { "2","供应商"},
            //};
            //this.SupCust_Flag.DisplayMember = "Value";
            //this.SupCust_Flag.ValueMember = "Key";
            //this.SupCust_Flag.DataSource = new BindingSource(type_dic, "");

        }

        public void LoadData()
        {
            //IBLL.ISupcustGroup bll = new BLL.SupcustGroupBLL();
            DataTable tb = paymentbll.GetAgingGroup(runType);
            //if (tb.Columns.Count < 1)
            //{
            //    tb = Conv.Assign<Model.bi_t_supcust_group>();
            //}

            this.dgv.DataSource = tb;
        }

        public void Save()
        {
            DataTable dt=new DataTable(); //(DataTable)this.dgv.DataSource;
            //tb.Columns.Add("sheet_amount", typeof(decimal));
            dt.Columns.Add("start_days", typeof(int));
            dt.Columns.Add("end_days", typeof(int));
            dt.Columns.Add("supcust_flag", typeof(string));
            //List<Model.bi_t_supcust_group> lis = new List<Model.bi_t_supcust_group>();
            if (dgv.Rows.Count <= 1)
            {
                MsgForm.ShowFrom("请输入数据后再保存");
                return;
            }
            if(Conv.ToString(this.dgv.Rows[0].Cells[0].Value).Trim() != "")
            {
                MsgForm.ShowFrom("有效数据区的第一行的开始天数不允许输入值！");
                return;
            }
            int n = dgv.Rows.Count-2;
            if (Conv.ToString(this.dgv.Rows[n].Cells[1].Value).Trim() != "")
            {
                MsgForm.ShowFrom("有效数据区的最后一行的结束天数不允许输入值！");
                return;
            }
            int s = 0;
            for (int i=0;i< this.dgv.Rows.Count-1;i++)
            {
                if (Conv.ToString(this.dgv.Rows[i].Cells[0].Value).Trim() == "" && Conv.ToString(this.dgv.Rows[i].Cells[1].Value).Trim() == "")
                    continue;
                if(i>0&& Conv.ToInt(this.dgv.Rows[i].Cells[0].Value) != s)
                {
                    MsgForm.ShowFrom("在任意两行数据上，下一行的开始天数和上一行的结束天数的差必须为1，否责会出现天数重合或不衔接的情况，请仔细检查数据！");
                    return;
                }
                DataRow dr1 = dt.NewRow();
                dr1["start_days"] = Conv.ToInt(this.dgv.Rows[i].Cells[0].Value);
                dr1["end_days"] = Conv.ToInt(this.dgv.Rows[i].Cells[1].Value);
                dr1["supcust_flag"] = runType;
                dt.Rows.Add(dr1);
                s = Conv.ToInt(this.dgv.Rows[i].Cells[1].Value)+1;
            }
            paymentbll.SaveAgingGroup(dt);
            //IBLL.ISupcustGroup bll = new BLL.SupcustGroupBLL();
            //bll.SaveGroup(lis);

            MsgForm.ShowFrom("保存成功");
            LoadData();
        }

        private void tsbFlush_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(this.Text, "18"))
            {

                return;
            }

            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(this.Text, "01"))
            {

                return;
            }
            ((DataTable)this.dgv.DataSource).Rows.Add();
        }

        private void tsbDel_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(this.Text, "02"))
            {

                return;
            }
            try
            {
                DataGridViewRow row = this.dgv.CurrentRow;
                if (row == null) return;
                if (row.Cells[0].Value==null)
                {
                    return;
                }
                if (YesNoForm.ShowFrom("确认要删除该行吗？") == DialogResult.Yes)
                {
                    this.dgv.Rows.RemoveAt(row.Index);
                }
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(this.Text, "04"))
            {

                return;
            }
            try
            {
                ((DataTable)this.dgv.DataSource).ClearTable();
                if (this.dgv.Rows.Count > 0)
                    Save();
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }

        private void frmSupcustGroup_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
