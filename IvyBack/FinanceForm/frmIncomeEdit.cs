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
namespace IvyBack.FinanceForm
{
    public partial class frmIncomeEdit : Form
    {
        private bi_t_sz_type item;
        private string pay_way;
        public frmIncomeEdit(string pay_way, bi_t_sz_type sz = null)
        {
            InitializeComponent();
            init_form();
            this.pay_way = pay_way;
            if (pay_way != "") txt_pay_way.ReadOnly = true;

            if (sz != null)
            {
                item = sz;
                init_data(sz);
            }
            System.Data.DataTable tb_bank;
            IBLL.IBank bankBLL = new BLL.BankBLL();
            tb_bank = bankBLL.GetSubjectList();
            txtvisa.Bind(tb_bank, 300, 200, "subject_no", "subject_no:科目编码:120,subject_name:科目名称:150", "subject_no/subject_name->Text");
        }

        private void init_form()
        {
            //供应商分类
            Dictionary<string, string> dic = new Dictionary<string, string>()
            {
                {"0","不定"},
                {"1","供应商"},
                {"2","客户"},
                {"4","其他收支"}
            };
            this.cb_pay_flag.DisplayMember = "Value";
            this.cb_pay_flag.ValueMember = "Key";
            this.cb_pay_flag.DataSource = new BindingSource(dic, null);

        }

        private void init_data(bi_t_sz_type sz)
        {
            if (sz != null)
            {
                this.txt_pay_way.Text = sz.pay_way;
                this.txt_pay_name.Text = sz.pay_name;
                this.txt_pay_memo.Text = sz.pay_memo;
                this.cb_pay_flag.SelectedValue = sz.pay_flag;

                this.chk_path.Checked = sz.path.Equals("1");
                this.chk_auto_cashsheet.Checked = sz.auto_cashsheet.Equals("1");
                this.chk_is_account.Checked = sz.is_account.Equals("1");
                this.chk_if_CtFix.Checked = sz.if_CtFix.Equals("1");
                this.chk_is_profit.Checked = sz.is_profit.Equals("1");

                if (sz.account_flag == "1") this.rd_account_flag2.Checked = true;
                else this.rd_account_flag1.Checked = true;


                if (sz.pay_kind == "1") this.rd_pay_kind2.Checked = true;
                else this.rd_pay_kind1.Checked = true;

                if (sz.profit_type == "1") this.rd_profit_type2.Checked = true;
                else this.rd_profit_type1.Checked = true;

                if (sz.is_pay == "1") this.rd_is_pay2.Checked = true;
                else if (sz.is_pay == "2") this.rd_is_pay3.Checked = true;
                else
                {
                    this.rd_is_pay1.Checked = true;
                    this.rd_pay_kind1.Checked = false;
                    this.rd_pay_kind2.Checked = false;
                }


            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(this.Text, "14"))
            {

                return;
            }
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txt_pay_way.Text))
                {
                    txt_pay_way.Focus();
                    MsgForm.ShowFrom("请输入编号");
                    return;
                }
                if (this.cb_pay_flag.SelectedValue==null)
                {
                    MsgForm.ShowFrom("请选择类型");
                    return;
                }
                if (string.IsNullOrEmpty(txt_pay_name.Text))
                {
                    txt_pay_name.Focus();
                    MsgForm.ShowFrom("请输入名称");
                    return;
                }
                //if (string.IsNullOrEmpty(this.txtvisa.Text))
                //{
                //    txt_pay_name.Focus();
                //    MsgForm.ShowFrom("请选择科目");
                //    return;
                //}
                if (rd_is_pay2.Checked == true || rd_is_pay3.Checked == true)
                {
                    if (rd_pay_kind1.Checked == false && rd_pay_kind2.Checked == false)
                    {
                        throw new Exception("应收应付增减必未选择!");
                    }
                }
                if (item == null)
                {
                    item = new bi_t_sz_type();
                    //item.other1 = "0";
                    item.other2 = "1";
                    item.km_code = "";
                    item.if_acc = "0";
                }

                item.pay_way = this.txt_pay_way.Text;
                item.pay_name = this.txt_pay_name.Text;
                item.pay_memo = this.txt_pay_memo.Text;
                item.pay_flag = this.cb_pay_flag.SelectedValue.ToString();
                item.other1 = txtvisa.Text.Split('/')[0];
                item.path = (this.chk_path.Checked == true ? "1" : "0");
                item.auto_cashsheet = (this.chk_auto_cashsheet.Checked == true ? "1" : "0");
                item.is_account = (this.chk_is_account.Checked == true ? "1" : "0");
                item.if_CtFix = (this.chk_if_CtFix.Checked == true ? "1" : "0");
                item.is_profit = (this.chk_is_profit.Checked == true ? "1" : "0");

                item.account_flag = (this.rd_account_flag2.Checked == true ? "1" : "0");
                item.pay_kind = (this.rd_pay_kind2.Checked == true ? "1" : "0");
                item.profit_type = (this.rd_profit_type2.Checked == true ? "1" : "0");

                item.is_pay = "0";
                if (this.rd_is_pay2.Checked) item.is_pay = "1";
                else if (this.rd_is_pay3.Checked) item.is_pay = "2";
                DialogResult = System.Windows.Forms.DialogResult.Yes;
                IBLL.IFinanceBLL bll = new BLL.FinanceBLL();
                if (string.IsNullOrEmpty(this.pay_way))
                {
                    bll.InsertSZType(item);
                    if (YesNoForm.ShowFrom("新建成功，是否继续新建？") == System.Windows.Forms.DialogResult.Yes)
                    {
                        item = new bi_t_sz_type();
                        pay_way = "";
                        txt_pay_way.Text = "";
                        txt_pay_name.Text = "";
                        txt_pay_memo.Text = "";
                        frmIncomeEdit frm = new frmIncomeEdit("");
                        frm.ShowDialog();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    bll.UpdateSZType(item);
                    MsgForm.ShowFrom("保存成功");
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                LogHelper.writeLog("frmIncomeEdit=>OK", ex.ToString());
                MsgForm.ShowFrom(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void frmIncomeEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmIncomeEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            item = null;
        }

        private void txt_pay_way_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Helper.KeyPressJudge.IsNum(e.KeyChar) ||
                Helper.KeyPressJudge.IsEnglish(e.KeyChar) ||
                e.KeyChar == (char)Keys.Back ||
                e.KeyChar == (char)Keys.Delete)
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        private void chk_path_CheckedChanged(object sender, EventArgs e)
        {
            this.panel1.Enabled = chk_path.Checked == false;
        }

        private void txt_pay_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back ||
                e.KeyChar == (char)Keys.Delete)
            {

            }
            else if (Helper.StringHelper.GetLength(txt_pay_name.Text) >= 30)
            {
                e.Handled = true;
            }
            else if (Helper.KeyPressJudge.IsStr(e.KeyChar) == true)
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        private void rd_is_pay1_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_is_pay1.Checked == true)
            {
                rd_pay_kind1.Checked = false;
                rd_pay_kind2.Checked = false;
            }
        }

    }
}
