using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace b2bclient.market
{
    public partial class AdviceReply : Form
    {
        BLL.IAdvice bll;
        private Model.advice advice;
        public AdviceReply(string av_id)
        {
            try
            {
                InitializeComponent();
                GlobalData.InitForm(this);
                bll = new BLL.Advice();
                advice = bll.Select(av_id);
                lbl_nick.Text = advice.nickname + "：";
                txt_ask.Text = advice.use_ask;
                txt_reply.Text = advice.mc_reply;
            }
            catch (Exception ex)
            {
                
                Program.frmMsg(ex.Message);
            }
        }

        private void pnlimg3_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.Gray);
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            e.Graphics.DrawRectangle(p, new Rectangle(0, 0, panel3.Width - 1, panel3.Height - 1));
        }


        private void pnlimg2_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.Gray);
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            e.Graphics.DrawRectangle(p, new Rectangle(0, 0, pnl_context.Width - 1, pnl_context.Height - 1));
        }
        private void pnlCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pnlOK_Click(object sender, EventArgs e)
        {
            try
            {
                advice.mc_reply = txt_reply.Text;
                bll.Reply(advice.av_id, advice.mc_reply);
                Program.frmMsg("回复成功!");
                this.Close();
            }
            catch (Exception ex)
            {
                
                Program.frmMsg(ex.Message);
            }
        }

    }
}
