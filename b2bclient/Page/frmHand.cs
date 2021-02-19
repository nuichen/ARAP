using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace b2bclient
{
    public partial class frmHand : Form
    {
        private int index = 0;
        private int pagesize = 5;
        private body.wm_order ord=new body.wm_order();
        BLL.IOrder bll = new BLL.Order();

        public frmHand(body.wm_order ord,DataTable lines,string header,int un_read_num)
        {
            InitializeComponent();
            LoadDataGrid();
            //
            this.ord = ord;
            this.Text = header;
            this.lbl_un_read_num.Text = un_read_num.ToString();
            if (ord.mobile_is_new == "1")
            {
                pnl_new.Visible = true;
            }
            else
            {
                pnl_new.Visible = false;
            }
            this.refreshData(lines);
        }

        private void LoadDataGrid()
        {
            this.dg_data.AddColumn("row_index", "行号", "", 50, 2, "");
            this.dg_data.AddColumn("goods_no", "货号", "", 80, 2, "");
            this.dg_data.AddColumn("goods_name", "品名", "", 140, 1, "");
            this.dg_data.AddColumn("qty", "数量", "", 80, 3, "0.00");
            this.dg_data.AddColumn("price", "价格", "", 80, 3, "0.00");
            this.dg_data.AddColumn("amount", "小计", "", 80, 3, "0.00");
            this.dg_data.AddColumn("remark", "备注", "", 160, 1, "");
            this.dg_data.AddColumn("enable", "状态", "", 50, 2, "{0:取消,1:正常}");

            this.dg_data.MergeCell = false;
            this.dg_data.SetTotalColumn("qty,amount");
            this.dg_data.DataSource = new DataTable();

        }

        private void refreshData(DataTable lines)
        {
            try
            {
                BLL.ICustomer cbll = new BLL.Customer();
                decimal credit = 0;
                if (ord.cus_no != null && ord.cus_no != "") {
                    decimal bal = 0;
                    decimal c_amt = 0;
                    cbll.GetBalance(ord.cus_no,out bal,out c_amt);
                    credit = bal + c_amt;
                }
                lbl_credit_amt.Text = credit.ToString("F2");
                lbl单号.Text = ord.ord_id;
                lbl下单时间.Text = Conv.ToDateTime(ord.create_time).ToString("yyyy-MM-dd HH:mm");
                lbl送达时间.Text = ord.reach_time;
                lbl姓名.Text = ord.sname;
                lbl手机.Text = ord.mobile;
                lbl地址.Text = ord.address;
                lbl备注.Text = ord.cus_remark;
                //计算合计
                decimal amount = 0;
                foreach (DataRow line in lines.Rows)
                {
                    if (line["enable"].ToString() == "1")
                    {
                        amount += Conv.ToDecimal(line["amount"]);
                    }

                }
                lab优惠.Text = "优惠金额:￥" + ord.discount_amt.ToString("F2");
                lbl合计.Text = "￥" + (amount - ord.discount_amt + ord.take_fee).ToString("0.##");
                //支付类型
                if (ord.pay_type == "0" || ord.pay_type.Equals("2"))
                {
                    pnl付款.BackgroundImage = pictureBox2.Image;
                    lab配送.Text = "配送费:￥" + ord.take_fee.ToString("F2");
                }
                else if (ord.pay_type == "1")
                {
                    pnl付款.BackgroundImage = pictureBox1.Image;
                    lab配送.Text = "配送费:￥" + ord.take_fee.ToString("F2");
                }
                //展示处理按键
                if (ord.status == "0")
                {
                    pnl_pass.Visible = true;
                    pnl_disable.Visible = true;
                }
                else
                {
                    pnl_pass.Visible = false;
                    pnl_disable.Visible = false;
                }
                //
                index = 0;
                this.DisplayLines(lines);
            }
            catch (Exception e)
            {
                Program.frmMsg(e.Message);
            }

        }

        private void DisplayLines(DataTable lines)
        {
            this.dg_data.DataSource = lines;
        }

        private void DisableRow()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                var row = this.dg_data.CurrentRow();
                if (row == null) return;
                //
                if (row["enable"].ToString() == "0")
                {
                    throw new Exception("该行已经取消");
                }
                //
               
                if (Program.frmMsgYesNo("确认要取消该行?") == DialogResult.No)
                {
                    return;
                }
                //
                bll.DisableRow(this.ord.ord_id, row["row_index"].ToString());
                //
                row["enable"] = "0";
                this.dg_data.Refresh();

                //计算合计
                decimal amount = 0;
                foreach (DataRow line in this.dg_data.DataSource.Rows)
                {
                    if (line["enable"].ToString() == "1")
                    {
                        amount += Conv.ToDecimal(line["amount"]);
                    }

                }
                lbl合计.Text = "￥" + (amount - ord.discount_amt + ord.take_fee).ToString("0.##");

            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);

            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void frmHand_Load(object sender, EventArgs e)
        {
            control.FormEsc.Bind(this);
            //
            control.ClickActive.addActive(pnl_pass, 2);
            control.ClickActive.addActive(pnl_disable, 2);
            control.ClickActive.addActive(pnl_later, 2);
            control.ClickActive.addActive(pnl_exit, 2);
            //
           
        }

        private void pnl_pass_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (this.ord.pay_type == "1" && (Conv.ToDecimal(lbl合计.Text) > Conv.ToDecimal(lbl_credit_amt.Text))) 
                {
                    if (Program.frmMsgYesNo("订单金额超出信用额度，确定下单吗？") != DialogResult.Yes) return;
                }
                
                bll.Pass(this.ord.ord_id);
                //
                var ord = new body.wm_order();
                var lines = new DataTable();
                int un_read_num = 0;
                if (bll.GetFirstNewOrder(out ord, out lines,out un_read_num ) == true)
                {
                    this.DialogResult = DialogResult.No;
                    var frm = new frmHand(ord,lines, "订单处理",un_read_num);
                    frm.ShowDialog();
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }

        private void pnl_disable_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                if (Program.frmMsgYesNo("确认拒绝该订单?") == DialogResult.Yes)
                {
                    bll.Disable(this.ord.ord_id);
                    //
                    var ord = new body.wm_order();
                    var lines = new DataTable();
                    int un_read_num = 0;
                    if (bll.GetFirstNewOrder(out ord, out lines,out un_read_num ) == true)
                    {
                        this.DialogResult = DialogResult.No;
                        var f = new frmHand(ord,lines, "订单处理",un_read_num);
                        f.ShowDialog();
                    }
                    else
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

        private void pnl_later_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                if (this.ord.status == "0")
                {
                    bll.SignRead(this.ord.ord_id);
                }
                //
                var ord = new body.wm_order();
                var lines = new DataTable();
                int un_read_num = 0;
                if (bll.GetFirstNewOrder(out ord, out lines,out un_read_num ) == true)
                {
                    this.DialogResult = DialogResult.No;
                    var frm = new frmHand(ord,lines, "订单处理",un_read_num );
                    frm.ShowDialog();
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
           
        }

        private void pnl_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_disable_row_Click(object sender, EventArgs e)
        {
            DisableRow();
        }

        private void frmHand_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.ord = null;
        }
       
    }
}
