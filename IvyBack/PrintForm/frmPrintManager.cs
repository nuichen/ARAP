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
    public partial class frmPrintManager : Form
    {
        public frmPrintManager()
        {
            InitializeComponent();
        }

        private sys_t_print_style print_style = new sys_t_print_style();

        private void frmPrintManager_Load(object sender, EventArgs e)
        {
            try
            {
                this.dataGrid1.AddColumn("用户", "用户", "", 100, 1, "");
                this.dataGrid1.AddColumn("style_name", "样式名称", "", 150, 1, "");
                this.dataGrid1.AddColumn("is_base", "系统样式", "", 80, 2, "{1:√,0:}");
                this.dataGrid1.AddColumn("is_share", "共享", "", 60, 2, "{1:√,0:}");
                this.dataGrid1.MergeCell = false;

                Flush();
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }

        private void Flush()
        {
            IBLL.IPrint bll = new BLL.PrintBLL();
            DataTable tb = bll.GetAll(print_style);

            this.dataGrid1.DataSource = tb;
        }

        public void PrintStyle(string report_id)
        {
            print_style.oper_id = Program.oper == null ? "" : Program.oper.oper_id;
            print_style.report_id = report_id;

            this.ShowDialog();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGrid1.CurrentRow() == null) return;

                if ("1".Equals(this.dataGrid1.CurrentRow()["is_base"].ToString()) && !Program.oper.oper_id.Equals("1001"))
                {
                    MsgForm.ShowFrom("系统样式不可修改");
                    return;
                }

                frmPrintAdd add = new frmPrintAdd();
                add.print_style = DB.ReflectionHelper.DataRowToModel<Model.sys_t_print_style>(this.dataGrid1.CurrentRow());
                add.print_style.report_id = print_style.report_id;
                add.ShowDialog();

                Flush();
            }
            catch (Exception exception)
            {
                MsgForm.ShowFrom(exception);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmPrintAdd add = new frmPrintAdd();
            add.print_style.report_id = print_style.report_id;
            add.ShowDialog();

            Flush();
        }
        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGrid1.CurrentRow() == null) return;

                if ("1".Equals(this.dataGrid1.CurrentRow()["is_base"].ToString()) && !Program.oper.oper_id.Equals("1001"))
                {
                    MsgForm.ShowFrom("系统样式不可删除");
                    return;
                }

                if (YesNoForm.ShowFrom("确认要删除样式[" + this.dataGrid1.CurrentRow()["style_name"] + "]") == DialogResult.Yes)
                {
                    IBLL.IPrint bll = new BLL.PrintBLL();
                    bll.Del(new sys_t_print_style() { style_id = this.dataGrid1.CurrentRow()["style_id"].ToString(), update_time = DateTime.Now });
                    Flush();
                }
            }
            catch (Exception exception)
            {
                MsgForm.ShowFrom(exception);
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnDefault_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGrid1.CurrentRow() == null) return;
                IBLL.IPrint bll = new BLL.PrintBLL();
                bll.AddDefault(
                    new sys_t_print_style_default()
                    {
                        oper_id = Program.oper.oper_id,
                        style_id = this.dataGrid1.CurrentRow()["style_id"].ToString(),
                        report_id = print_style.report_id,
                    });
            }
            catch (Exception exception)
            {
                MsgForm.ShowFrom(exception);
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGrid1.CurrentRow() == null) return;

                global::PrintHelper.IBLL.IPrint print = new global::PrintHelper.BLL.PrintP();
                print.Print(this.dataGrid1.CurrentRow()["style_data"].ToString(),
                    PrintHelper.GetStyle(this.dataGrid1.CurrentRow()["style_data"].ToString()),
                    PrintHelper.tb_main,
                    PrintHelper.tb_detail);


            }
            catch (Exception exception)
            {
                MsgForm.ShowFrom(exception);
            }
        }

        private void dataGrid1_DoubleClickCell(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            btnPrintV_Click(sender, e);
        }

        private void btnPrintV_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGrid1.CurrentRow() == null) return;

                global::PrintHelper.IBLL.IPrint print = new global::PrintHelper.BLL.PrintV();
                print.Print(this.dataGrid1.CurrentRow()["style_data"].ToString(),
                    PrintHelper.GetStyle(this.dataGrid1.CurrentRow()["style_data"].ToString()),
                    PrintHelper.tb_main,
                    PrintHelper.tb_detail);


            }
            catch (Exception exception)
            {
                MsgForm.ShowFrom(exception);
            }
        }

    }
}
