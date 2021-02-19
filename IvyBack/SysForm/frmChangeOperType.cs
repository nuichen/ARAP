using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;

namespace IvyBack.SysForm
{
    public partial class frmChangeOperType : Form
    {
        public frmChangeOperType()
        {
            InitializeComponent();
        }
        sa_t_oper_type oper_type;



        public DialogResult ChangeOperType(sa_t_oper_type oper_type)
        {
            this.oper_type = oper_type;
            return this.ShowDialog();
        }

        private void frmChangeOperType_Load(object sender, EventArgs e)
        {
            if (oper_type != null)
            {
                this.txtGrout.Text = oper_type.type_name;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtGrout.Text))
                {
                    this.txtGrout.Focus();
                    return;
                }

                IBLL.IOper bll = new BLL.OperBLL();

                if (oper_type == null)
                {
                    oper_type = new sa_t_oper_type()
                    {
                        type_name = this.txtGrout.Text,
                        update_time = DateTime.Now
                    };

                    bll.AddOperType(oper_type);
                }
                else
                {
                    oper_type.type_name = this.txtGrout.Text;
                    oper_type.update_time = DateTime.Now;

                    bll.ChangeOperType(oper_type);
                }

                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
