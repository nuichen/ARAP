using System;
using System.Drawing;
using System.Windows.Forms;
using IvyFront.Helper;


namespace IvyFront.Forms
{
    public partial class NetSvrForm : Form
    {
        public string svr = "";
        public string is_bind_ip = "";
        bool is_conn = false;
        public NetSvrForm(string svr, string is_bind_ip)
        {
            InitializeComponent();
            foreach (System.Windows.Forms.Control con in this.panel1.Controls)
            {
                Label lbl = (Label)con;
                cons.AddClickAct.Add(lbl);
                lbl.Click += lbl_click;
            }
            chk_bind_ip.Checked = is_bind_ip.Equals("1");
            string http_svr = svr.Replace("http://", "");
            string port = http_svr;
            http_svr = http_svr.Substring(0, http_svr.IndexOf(':'));
            port = port.Substring(port.IndexOf(':')+1);
            textBox1.Text = http_svr;
            lbl_port.Text = port;
        }

        void lbl_click(object sender, EventArgs e)
        {
            try
            {
                TextBox textBox = null;
                if (textBox1.Focused == true)
                {
                    textBox = textBox1;
                }

                Label lbl = (Label)sender;
                if (lbl.Text == "←")
                {
                    if (textBox.Text != "")
                    {
                        textBox.Text = textBox.Text.Substring(0, textBox.Text.Length - 1);
                        textBox.SelectionStart = textBox.Text.Length;
                    }
                }
                else if (lbl.Text.ToLower() == "清空".ToLower())
                {
                    textBox.Text = "";
                }
                else if (lbl.Text.ToLower() == "确定".ToLower())
                {
                    button1_Click(button1, null);
                }
                else if (lbl.Text.ToLower() == "取消".ToLower())
                {
                    this.Close();
                }
                else
                {
                    if (textBox.SelectionLength != 0)
                    {
                        textBox.Text = textBox.Text.Substring(0, textBox.SelectionStart) + lbl.Text + textBox.Text.Substring(textBox.SelectionStart + textBox.SelectionLength);
                    }
                    else
                    {
                        textBox.Text = textBox.Text + lbl.Text;
                    }

                    textBox.SelectionStart = textBox.Text.Length;

                    //textBox.Text += lbl.Text;
                    //textBox.SelectionStart = textBox.Text.Length;
                }
            }
            catch (Exception ex)
            {
                new MsgForm(ex.GetMessage()).ShowDialog();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length == 0)
            {
                new MsgForm("IP/服务不能为空").ShowDialog();
                return;
            }
            if (!is_conn)
            {
                new MsgForm("IP/服务无法连接，不能保存").ShowDialog();
                return;
            }
            svr = "http://" + textBox1.Text.Trim() + ":" + lbl_port.Text;
            is_bind_ip = chk_bind_ip.Checked ? "1" : "0";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void NetSvrForm_Load(object sender, EventArgs e)
        {
        }

        private void NetSvrForm_Shown(object sender, EventArgs e)
        {
        }

        private void NetSvrForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(128, 128, 255)), new Rectangle(0, 0, this.Width - 1, this.Height - 1));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length == 0)
            {
                new MsgForm("IP/服务不能为空").ShowDialog();
                return;
            }
            lbl_msg.Visible = true;
            svr = "http://" + textBox1.Text.Trim() + ":" + lbl_port.Text;
            BLL.TestNetBLL bll = new BLL.TestNetBLL();
            var err_msg = "";
            if (bll.CheckConnect(svr, out err_msg))
            {
                is_conn = true;
                new MsgForm("IP/服务连接成功").ShowDialog();
            }
            else
            {
                is_conn = false;
                new MsgForm(err_msg).ShowDialog();
            }
            lbl_msg.Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            is_conn = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var frm = new Forms.InputNumerForm("请输入端口", 0);
            decimal port = 0;
            if (frm.Input(out port))
            {
                lbl_port.Text = port.ToString();
            }
        }


    }
}