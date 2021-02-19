using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using IvyBack.Helper;
using Model.PaymentModel;

namespace IvyBack.PaymentForm
{
    public partial class frmPaymentDetailed : Form
    {
        System.Data.DataTable tb_bank;
        public List<rp_t_collection_way> lr = new List<rp_t_collection_way>();
        public int runType = 1;//0供应商，1客户
        public frmPaymentDetailed(DataTable dt)
        {
            InitializeComponent();
            //
            //if(runType==1)
            //editGrid1.AddColumn("pay_name", "付款方式", "", 150, 1, "", true);
            //else
            //    editGrid1.AddColumn("pay_name", "付款方式", "", 150, 1, "", true);
            editGrid1.AddColumn("pay_name", "结算方式", "", 150, 1, "", true);
            editGrid1.AddColumn("total_amount", "金额", "", 150, 1, "", true) ;
            IBLL.IARAP_SCPaymentBLL paymentbll = new BLL.ARAP_SCPaymentBLL();
           
            if (dt == null)
            {
                var tb = new DataTable();
                tb.Columns.Add("pay_name");
                tb.Columns.Add("total_amount");
                for (int i = 0; i < 1; i++)
                {
                    tb.Rows.Add(tb.NewRow());
                }
                editGrid1.DataSource = tb;
            }
            else
            {
                editGrid1.DataSource = dt;
            }
           //paymentbll.GetPaymentList();
            var send_tb = paymentbll.GetPaymentList();
            editGrid1.Bind("pay_name", send_tb, 360, 200, "pay_way", "pay_way:编号:80,pay_name:名称:280", "pay_way/pay_name->pay_name");
            // txtvisa.Bind(tb_bank, 300, 200, "subject_no", "subject_no:科目编码:120,subject_name:科目名称:150", "subject_no/subject_name->Text");
        }


       public DialogResult  ShowPayment(out DataTable ls,out string visa_id)
        {
            
            this.ShowDialog();
            DataTable dt= editGrid1.DataSource;
         
            for(int i=0;i<dt.Rows.Count;i++)
            {
               if( Conv.ToString( dt.Rows[i]["pay_name"])=="")
                {
                    dt.Rows[i].Delete();
                }
            }
            dt.AcceptChanges();
            if (dt.Rows.Count == 1)
            {
                IBLL.IPayment bll = new BLL.PaymentBLL();
                bi_t_payment_info line = new bi_t_payment_info();
                line.pay_way = Conv.ToString(dt.Rows[0]["pay_name"]).Trim().Split('/')[0];
                var tb = bll.GetPayment(line);
                visa_id = Conv.ToString(tb.Rows[0]["visa_id"])+"/"+ Conv.ToString(tb.Rows[0]["visa_nm"]);

            }
            else
            {
                visa_id = "";
            }
            ls = dt;
            return this.DialogResult;
          
            

        }

        public bi_t_payment_info payment;

        private void LoadPay()
        {
            //foreach (Control c in this.Controls)
            //{
            //    if (c is TextBox)
            //    {
            //        TextBox tb = c as TextBox;
            //        if (tb.TextAlign == HorizontalAlignment.Right)
            //            c.Text = "0.0000";
            //        else
            //            c.Text = "";
            //    }
            //}

            //if (payment != null)
            //{
            //    txtPayName.Text = payment.pay_name;
            //    txtPaynum.Text = payment.pay_way;
                
            //    var dr = this.tb_bank.Select().Where(r => r["subject_no"].Equals(payment.visa_id)).SingleOrDefault();
            //    if (dr != null)
            //    {
            //        this.txtvisa.Text = payment.visa_id + "/" + dr["subject_name"].ToString();
                    
            //    }
                   
            //    txtPaynum.ReadOnly = true;
            //    checkIsStop.Checked = payment.display.Equals("0");
            
            //}
            //else
            //{
            //    txtPayName.Text = "";
            //    txtPaynum.Text = "";
            //    txtPaynum.ReadOnly = false;
            //    checkIsStop.Checked = false;
           // }
        }
        private void OK()
        {
            //IBLL.IPayment bll = new BLL.PaymentBLL();
            //if (payment == null || string.IsNullOrEmpty(payment.pay_way))
            //{
            //    //添加
            //    bi_t_payment_info sup = new bi_t_payment_info()
            //    {
            //        display = checkIsStop.Checked ? "0" : "1",
            //        pay_flag = "0",
            //        pay_name = txtPayName.Text,
            //        pay_way = txtPaynum.Text,                  
            //        subject_no = txtvisa.Text.Split('/')[0]//visa_id=txtvisa.Text.Split('/')[0]
            //    };
            //    bll.Add(sup);
            //}
            //else
            //{
            //    //修改
            //    payment.display = this.checkIsStop.Checked ? "0" : "1";
            //    payment.pay_name = this.txtPayName.Text;
            //    payment.subject_no = txtvisa.Text.Split('/')[0];
            //    bll.Upload(payment);
            //}

        }

        private void frmPaymentUpload_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    LoadPay();
            //}
            //catch (Exception ex)
            //{
            //    LogHelper.writeLog("frmPaymentUpload_Load=>load", ex.ToString());
            //    MsgForm.ShowFrom(ex);
            //}
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
          DataTable dt=  editGrid1.DataSource;
            //foreach (DataRow dr in dt.Rows)
            //{
            //    if (Helper.Conv.ToDecimal(dr["collection_amount"]) > 0)
            //    {
            //        rp_t_collection_way temp = new rp_t_collection_way();
            //        temp.collection_type = Helper.Conv.ToString(dr["pay_name"]);
            //        temp.collection_amount= Helper.Conv.ToDecimal(dr["collection_amount"]);
            //        lr.Add(temp);
            //    }
            //    else
            //    {
            //        continue;
            //    }
            //}

            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    Control c = sender as Control;
            //    if (c.TabIndex == this.txtPayName.TabIndex)
            //    {
            //        btnOk_Click(sender, e);
            //    }
            //    else
            //    {
            //        SendKeys.Send("{Tab}");
            //    }
            //}
        }

        private void frmPaymentUpload_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmPaymentUpload_Shown(object sender, EventArgs e)
        {
            //if (payment == null)
            //    this.txtPaynum.Focus();
            //else
            //    this.txtPayName.Focus();
        }

        private void txtPaynum_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)Keys.Delete)
            //{

            //}
            //else if (e.KeyChar == (char)Keys.Back)
            //{

            //}
            //else if (Helper.KeyPressJudge.IsNum(e.KeyChar) || Helper.KeyPressJudge.IsEnglish(e.KeyChar))
            //{


            //}
            //else
            //{
            //    e.Handled = true;
            //}
        }

        private void txtPayName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)Keys.Delete)
            //{

            //}
            //else if (e.KeyChar == (char)Keys.Back)
            //{

            //}
            //else if (Helper.StringHelper.GetLength(txtPayName.Text) > 20)
            //{
            //    e.Handled = true;
            //}
            //else if (Helper.KeyPressJudge.IsStr(e.KeyChar) == true)
            //{

            //}
            //else
            //{
            //    e.Handled = true;
            //}
        }

        private void editGrid1_CellEndEdit(object sender, string column_name, DataRow row)
        {
            if (column_name.Equals("total_amount"))
            {
                if (Conv.ToDecimal(row["total_amount"])< 0)
                {
                    MsgForm.ShowFrom("金额不能为负！");
                    row["total_amount"] = 0.00;
                }
            }
         }
    }
}
