using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;

namespace IvyBack.PrintForm
{
    public partial class frmPrintAdd : Form
    {
        public frmPrintAdd()
        {
            InitializeComponent();
        }

        public sys_t_print_style print_style = new sys_t_print_style();

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPrintAdd_Load(object sender, EventArgs e)
        {
            if (Program.oper.oper_type.Equals("1000"))
            {
                this.cbIsBase.Visible = true;
            }

            if (!string.IsNullOrEmpty(print_style.style_id))
            {
                //修改
                this.txtStyleName.Text = print_style.style_name;
                this.cbShare.Checked = "1".Equals(print_style.is_share);
                this.cbIsBase.Checked = "1".Equals(print_style.is_base);
                this.cbStyle.Enabled = false;
                this.Text = "修改样式";
            }
            else
            {
                IBLL.IPrint bll = new BLL.PrintBLL();
                var tb = bll.GetAll(new Model.sys_t_print_style()
                {
                    oper_id = Program.oper == null ? "" : Program.oper.oper_id,
                    report_id = print_style.report_id,
                });

                if (tb.Columns.Count < 1)
                {
                    tb = Helper.Conv.Assign<Model.sys_t_print_style>();
                }

                var dr = tb.NewRow();
                dr["style_data"] = "";
                dr["style_name"] = "空白";
                tb.Rows.InsertAt(dr, 0);

                this.cbStyle.ValueMember = "style_data";
                this.cbStyle.DisplayMember = "style_name";
                this.cbStyle.DataSource = tb;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                IBLL.IPrint bll = new BLL.PrintBLL();
                print_style.is_share = this.cbShare.Checked ? "1" : "0";
                print_style.is_base = this.cbIsBase.Checked ? "1" : "0";
                print_style.oper_id = Program.oper.oper_id;
                print_style.style_name = this.txtStyleName.Text;
                print_style.update_time = DateTime.Now;

                string parent_data = "";
                if (string.IsNullOrEmpty(print_style.style_id))
                {
                    print_style.style_id = Guid.NewGuid().ToString();
                    print_style.create_time = DateTime.Now;
                    bll.Add(print_style);

                    parent_data = this.cbStyle.SelectedValue.ToString();
                }
                else
                {
                    bll.Update(print_style);
                }

                sys_t_print_style style = PrintHelper.myStyle[print_style.style_id];

                global::PrintHelper.BLL.PrintD p = new global::PrintHelper.BLL.PrintD();
                global::PrintHelper.IBLL.IPrint print = p;
                p.SaveStyle += PrintForm.PrintHelper.UpdateStyle;
                print.Print(style.style_data,
                    PrintForm.PrintHelper.GetStyle(style.style_data,parent_data),
                    PrintHelper.tb_main,
                    PrintHelper.tb_detail);
                this.Close();
            }
            catch (Exception exception)
            {
                MsgForm.ShowFrom(exception);
            }
        }


    }
}
