﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IvyFront.Forms
{
    public partial class frmSaleSum : Form
    {
        public frmSaleSum()
        {
            InitializeComponent();

            cons.ClickActive.addActive(pnl_close, 2);
            //
            cons.ClickActive.addActive(pnldate1, 2);
            cons.ClickActive.addActive(pnldate2, 2);
            //
            pnldate1.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            pnldate2.Text = System.DateTime.Now.ToString("yyyy-MM-dd");

            this.refreshData();
        }

        private void refreshData()
        {
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                //
                IBLL.ISaleData bll = new BLL.SaleData();
                int order_count = 0;
                decimal total_amt = 0;
                decimal give_amt = 0;
                string order_key = "SO";
                if (radioButton2.Checked) order_key = "PI";
                var lst = bll.GetSaleSum(pnldate1.Text, pnldate2.Text, order_key, out order_count, out total_amt, out give_amt);

                lbl_order_count.Text = order_count.ToString();
                lbl_total_amt.Text = total_amt.ToString("F2");
                lbl_give_amt.Text = give_amt.ToString("F2");
                decimal no_pay = total_amt;
                if (lst.Count > 0)
                {
                    var cash_amt = lst.Where(d => d.pay_way == "A").Sum(d => d.pay_amount);
                    var wx_amt = lst.Where(d => d.pay_way == "W").Sum(d => d.pay_amount);
                    var zfb_amt = lst.Where(d => d.pay_way == "Z").Sum(d => d.pay_amount);
                    var ml_amt = lst.Where(d => d.pay_way == "Y").Sum(d => d.pay_amount);

                    lbl_cash_amt.Text = cash_amt.ToString("F2");
                    lbl_wx_amt.Text = wx_amt.ToString("F2");
                    lbl_zfb_amt.Text = zfb_amt.ToString("F2");
                    lbl_ml_amt.Text = ml_amt.ToString("F2");
                    no_pay = no_pay - cash_amt - wx_amt - zfb_amt - ml_amt;
                }
                lbl_no_pay.Text = no_pay.ToString("F2");

                if (order_key == "SO")
                {
                    order_count = 0;
                    total_amt = 0;
                    give_amt = 0;
                    var lst2 = bll.GetSaleSum(pnldate1.Text, pnldate2.Text, "RI", out order_count, out total_amt, out give_amt);

                    if (order_count > 0) groupBox1.Visible = true;

                    lbl_th_order_count.Text = order_count.ToString();
                    lbl_th_total_amt.Text = total_amt.ToString("F2");
                    if (lst2.Count > 0)
                    {
                        var cash_amt = lst2.Where(d => d.pay_way == "A").Sum(d => d.pay_amount);
                        var wx_amt = lst2.Where(d => d.pay_way == "W").Sum(d => d.pay_amount);
                        var zfb_amt = lst2.Where(d => d.pay_way == "Z").Sum(d => d.pay_amount);
                        var ml_amt = lst2.Where(d => d.pay_way == "Y").Sum(d => d.pay_amount);

                        lbl_th_cash_amt.Text = cash_amt.ToString("F2");
                        lbl_th_wx_amt.Text = wx_amt.ToString("F2");
                        lbl_th_zfb_amt.Text = zfb_amt.ToString("F2");
                        lbl_th_ml_amt.Text = ml_amt.ToString("F2");
                    }
                }
            }
            catch (Exception e)
            {
                var frm = new MsgForm(e.Message);
                frm.ShowDialog();
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

        private void pnldate1_Click(object sender, EventArgs e)
        {
            if (pnldate1.Text == "")
            {
                ShowDate(System.DateTime.Now.Year, System.DateTime.Now.Month, pnldate1);
            }
            else
            {
                System.DateTime d = Conv.ToDateTime(pnldate1.Text);
                ShowDate(d.Year, d.Month, pnldate1);
            }
        }

        private void pnldate2_Click(object sender, EventArgs e)
        {
            if (pnldate2.Text == "")
            {
                ShowDate(System.DateTime.Now.Year, System.DateTime.Now.Month, pnldate2);
            }
            else
            {
                System.DateTime d = Conv.ToDateTime(pnldate2.Text);
                ShowDate(d.Year, d.Month, pnldate2);
            }
        }

        private void ShowDate(int year, int month, System.Windows.Forms.Control c)
        {
            pnldate1.TabStop = true;
            pnldate1.Focus();

            pnlboard.BackColor = Color.LightGray;
            pnlboard.Size = new Size(this.Width, 250);
            pnlboard.Top = this.Height - pnlboard.Height;
            this.Controls.Add(pnlboard);
            pnlboard.TabStop = false;
            pnlboard.BringToFront();
            pnlboard.Tag = c;
            pnlboard.Controls.Clear();

            int linecount = 5;
            int columncount = 8;
            int lineheight = (pnlboard.Height - 1) / linecount;
            int columnwidth = (pnlboard.Width - 1) / columncount;
            int itop = 0;
            int ileft = 0;
            int days = DateTime.DaysInMonth(year, month);
            //显示月份
            if (1 == 1)
            {
                cons.ankey con = new cons.ankey();
                con.TabStop = false;
                con.Text = "<";
                con.Tag = year + "-" + month + "-1";
                con.Width = columnwidth - 2;
                con.Height = lineheight - 2;
                con.Top = itop;
                con.Left = ileft;
                con.MouseDown += this.date_click;
                ileft += columnwidth;
                cons.ClickActive.addActive(con, 2);
                pnlboard.Controls.Add(con);
            }
            if (1 == 1)
            {
                System.Windows.Forms.Label lbl = new Label();
                lbl.AutoSize = false;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Font = new Font("微软雅黑", 12);
                lbl.Text = year + "年" + month + "月";
                lbl.Width = columnwidth * (columncount - 2);
                lbl.Height = lineheight - 2;
                lbl.Top = itop;
                lbl.Left = 1 * columnwidth;
                pnlboard.Controls.Add(lbl);
            }
            if (1 == 1)
            {
                cons.ankey con = new cons.ankey();
                con.TabStop = false;
                con.Text = ">";
                con.Tag = year + "-" + month + "-1";
                con.Width = columnwidth - 2;
                con.Height = lineheight - 2;
                con.Top = itop;
                con.Left = (columncount - 1) * columnwidth;
                con.MouseDown += this.date_click;
                ileft += columnwidth;
                cons.ClickActive.addActive(con, 2);
                pnlboard.Controls.Add(con);
            }

            //
            ileft = 0;
            itop = lineheight;
            for (int i = 1; i <= days; i++)
            {

                cons.ankey con = new cons.ankey();
                con.Tag = year + "-" + month + "-" + i.ToString();
                con.TabStop = false;

                con.Text = i.ToString();

                con.Width = columnwidth - 2;
                con.Height = lineheight - 2;
                con.Top = itop;
                con.Left = ileft;
                con.MouseDown += this.date_click;
                ileft += columnwidth;
                cons.ClickActive.addActive(con, 2);
                pnlboard.Controls.Add(con);
                if (i == columncount)
                {
                    itop += lineheight;
                    ileft = 0;
                }
                else if (i == columncount * 2)
                {
                    itop += lineheight;
                    ileft = 0;
                }
                else if (i == columncount * 3)
                {
                    itop += lineheight;
                    ileft = 0;
                }
                else if (i == columncount * 4)
                {
                    itop += lineheight;
                    ileft = 0;
                }
            }
            pnlboard.Visible = true;
        }

        private void date_click(object sender, EventArgs e)
        {
            System.Windows.Forms.Control con = (System.Windows.Forms.Control)sender;
            if (con.Text == "<")
            {
                var d = Conv.ToDateTime(con.Tag);
                d = d.AddMonths(-1);
                ShowDate(d.Year, d.Month, (System.Windows.Forms.Control)pnlboard.Tag);
            }
            else if (con.Text == ">")
            {
                var d = Conv.ToDateTime(con.Tag);
                d = d.AddMonths(1);
                ShowDate(d.Year, d.Month, (System.Windows.Forms.Control)pnlboard.Tag);

            }
            else
            {
                System.Windows.Forms.Control c = (System.Windows.Forms.Control)pnlboard.Tag;
                c.Text = Conv.ToDateTime(con.Tag).ToString("yyyy-MM-dd");
                c.Refresh();
                pnlboard.Visible = false;

            }

        }

        Panel pnlboard = new Panel();
    
        private void con_click(object sender, EventArgs e)
        {
            var con = (System.Windows.Forms.Control)sender;
            string str = con.Text;
            if (pnlboard.Tag != null)
            {
                var con2 = (System.Windows.Forms.Control)pnlboard.Tag;
                con2.Focus();
            }
            if (str == "删除")
            {
                SendKeys.SendWait("{BACKSPACE}");
            }
            else if (str == "查询")
            {
                pnlboard.Visible = false;
                this.refreshData();
            }
            else
            {

                SendKeys.SendWait(con.Text);
            }

        }

        private void pnl_search_Click(object sender, EventArgs e)
        {
            pnlboard.Visible = false;
            clear_data();
            this.refreshData();
        }

        private void pnldate1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(pnldate1.Text, new Font("微软雅黑", 12), Brushes.Black, new PointF(5, 7));
        }

        private void pnldate2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(pnldate2.Text, new Font("微软雅黑", 12), Brushes.Black, new PointF(5, 7));
        }

        private void frmSaleFlow_Click(object sender, EventArgs e)
        {
            pnlboard.Visible = false;
        }

        private void clear_data() 
        {
            lbl_order_count.Text = "0";
            lbl_total_amt.Text = "0.00";
            lbl_give_amt.Text = "0.00";
            lbl_cash_amt.Text = "0.00";
            lbl_wx_amt.Text = "0.00";
            lbl_zfb_amt.Text = "0.00";
            lbl_ml_amt.Text = "0.00";
            lbl_no_pay.Text = "0.00";
        }

        private void pnl_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel10_Click(object sender, EventArgs e)
        {
            pnlboard.Visible = false;
        }

        private void btn_pay_flow_Click(object sender, EventArgs e)
        {
            string order_key = "SO";
            if (radioButton2.Checked) order_key = "PI";
            frmPayFlow frm = new frmPayFlow(Conv.ToDateTime(pnldate1.Text), Conv.ToDateTime(pnldate2.Text), order_key);
            frm.ShowDialog();
        }

        private void btn_sale_flow_Click(object sender, EventArgs e)
        {
            string order_key = "SO";
            if (radioButton2.Checked) order_key = "PI";
            frmSaleFlow frm = new frmSaleFlow(Conv.ToDateTime(pnldate1.Text), Conv.ToDateTime(pnldate2.Text), order_key);
            frm.ShowDialog();
        }

    }
}
