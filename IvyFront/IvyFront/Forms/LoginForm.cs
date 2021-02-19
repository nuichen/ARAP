using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using IvyFront.Helper;


namespace IvyFront.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            //
            foreach (System.Windows.Forms.Control con in this.panel3.Controls)
            {
                Label lbl = (Label) con;
                cons.AddClickAct.Add(lbl);
                lbl.Click += lbl_click;
            }
        }

        public bool Login() 
        {
            if (this.ShowDialog() == DialogResult.OK)
            {
                
                return true;
            }
            else
            {
                
                return false;
            }
        }

        private void do_login()
        {
            if (textBox1.Text.Trim() == "9999" && textBox2.Text.Trim() == "9999")
            {
                Program.oper_id = "9999";
                DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
                //this.Hide();
                //var frm = new frmMenu();
                //frm.ShowDialog();
                //save_exit();
            }
            else
            {
                IBLL.IOper operBLL = new BLL.Oper();
                var errMsg = "";
                if (operBLL.Login(textBox1.Text.Trim(), textBox2.Text.Trim(),out errMsg))
                {
                    Program.is_login = true;
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                    //this.Hide();
                    //var frm = new frmMenu();
                    //frm.ShowDialog();

                    //save_exit();
                }
                else
                {
                    if(errMsg != "") new MsgForm("账号/密码错误").ShowDialog();
                    else new MsgForm("登录失败").ShowDialog();
                }
            }
        }

        private void save_exit() 
        {
            if (1 == 1 && Program.is_login)
            {
                /*
                Form frmTop = new Form();
                frmTop.FormBorderStyle = FormBorderStyle.None;
                frmTop.WindowState = FormWindowState.Maximized;
                frmTop.BackColor = Color.FromArgb(30, 110, 165);
                Label lbl = new Label();
                lbl.AutoSize = false;
                lbl.ForeColor = Color.White;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Font = new Font("SimSun", 20);
                lbl.Text = "正在安全退出，请稍候……";
                lbl.Dock = DockStyle.Fill;
                frmTop.Controls.Add(lbl);
                frmTop.Show();
                Application.DoEvents();
                */
                WaitForm frm = new WaitForm("正在上传数据，请稍候","2");
                frm.ShowDialog();
            }
            else 
            {
                Application.ExitThread();
                System.Environment.Exit(0);
            }
        }

        void lbl_paint(object sender, PaintEventArgs e)
        {
            Label lbl = (Label) sender;
            e.Graphics.DrawRectangle(Pens.White, 0, 0, lbl.Width - 1, lbl.Height - 1);
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
                else if (textBox2.Focused == true)
                {
                    textBox = textBox2;
                }

                Label lbl = (Label) sender;
                if (lbl.Text == "←")
                {
                    if (textBox.Text != "")
                    {
                        textBox.Text = textBox.Text.Substring(0, textBox.Text.Length - 1);
                        textBox.SelectionStart = textBox.Text.Length;
                    }
                }
                else if (lbl.Text.ToLower() == "重输".ToLower())
                {
                    textBox.Text = "";
                }
                else if (lbl.Text.ToLower() == "确定".ToLower())
                {
                    if (textBox1.Focused == true)
                    {
                        textBox2.Focus();
                    }
                    else if (textBox2.Focused == true)
                    {
                        textBox2_KeyDown(textBox2, new KeyEventArgs(Keys.Enter));
                    }
                }
                else if (lbl.Text.ToLower() == "取消".ToLower())
                {
                    LoginForm_KeyDown(this, new KeyEventArgs(Keys.Escape));
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
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }

            if (e.KeyCode == Keys.Down)
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    do_login();
                }

                if (e.KeyCode == Keys.Up)
                {
                    textBox1.Focus();
                }
            }
            catch (Exception ex)
            {
                new MsgForm(ex.GetMessage()).ShowDialog();
            }
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Program.is_run = false;
                
                save_exit();
                //this.Close();
                
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //base.OnClosing(e);
            //Application.ExitThread();
            //System.Environment.Exit(0);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length >= 4)
            {
                textBox2.Focus();
                //textBox1.Text = textBox1.Text.Substring(0, 4);
                textBox2.SelectionStart = textBox2.Text.Length;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.textBox2_KeyDown(textBox2, new KeyEventArgs(Keys.Enter));
        }

        private void label5_Click(object sender, EventArgs e)
        {
            LoginForm_KeyDown(this, new KeyEventArgs(Keys.Escape));
        }

        private void label4_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.White, 0, 0, label4.Width - 1, label4.Height - 1);
        }

        private void label5_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.White, 0, 0, label5.Width - 1, label5.Height - 1);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.White, 0, 0, panel4.Width - 1, panel4.Height - 1);
        }

        private void LoginForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Gray), new Rectangle(0, 0, this.Width - 1, this.Height - 1));
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length >= 6)
            {
                do_login();
            }
        }

        
    }
}