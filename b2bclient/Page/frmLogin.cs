using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Diagnostics;
using b2bclient.sys;
namespace b2bclient
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();

            lbl_software_version.Text = GlobalData.software_version;
            lbl_ivy_app_id.Text = GlobalData.ivy_app_id;
            //
            control.MoveHelper.Bind(this);

            if (System.IO.File.Exists(Program.path + "\\login.txt"))
            {
                var loginStr = System.IO.File.ReadAllText(Program.path + "\\login.txt");
                if (loginStr.Trim().Length > 0)
                {
                    string[] loginArr = loginStr.Split('|');
                    this.textBox1.Text = (loginArr.Length > 0 ? loginArr[0] : "");
                    this.textBox2.Text = (loginArr.Length > 1 ? loginArr[1] : "");
                    this.checkBox1.Checked = true;
                    Program.pwd = this.textBox2.Text;
                }
            }
        }

        private void frmLogin_Paint(object sender, PaintEventArgs e)
        {
           // e.Graphics.DrawRectangle(new Pen(System.Drawing.Color.Gray), new Rectangle(0, 0, this.Width - 1, this.Height - 1));
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            control.FormEsc.Bind(this);
            foreach (System.Windows.Forms.Control con in pnl.Controls)
            {
                control.ClickActive.addActive(con,2);
                con.MouseDown  += this.con_click;
            }
        }

     

        private void con_click(object sender, EventArgs e)
        {
            System.Windows.Forms.Control con = (System.Windows.Forms.Control)sender;
            if (con.Tag.ToString() == "back")
            {
                SendKeys.SendWait("{BACKSPACE}");
            }
            else if (con.Tag.ToString() == "clear")
            {
                this.clear();
            }
            else if (con.Tag.ToString() == "ok")
            {
                this.login();

            }
            else if (con.Tag.ToString() == "exit")
            {
                this.Close();
            }
            else
            {
                SendKeys.SendWait(con.Tag.ToString());
            }
           
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Application.ExitThread();
            System.Environment.Exit(0);
        }

        private void login()
        {
            try
            {
                BLL.IOper bll = new BLL.Oper();
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                string mc_id;
                string oper_type;
                string tempPwd = "";
                if (textBox2.Text.Length > 0 && textBox2.Text == Program.pwd)
                {
                    tempPwd = Program.pwd;
                }
                else
                {
                    if (textBox2.Text.Length > 0)
                    {
                        Helper.sec sec = new Helper.sec();
                        tempPwd = sec.des(textBox2.Text);
                    }
                    else 
                    {
                        tempPwd = "";
                    }
                }

                bll.Login(textBox1.Text, tempPwd, out mc_id, out oper_type);
                Program.oper_id = textBox1.Text;
                Program.pwd = tempPwd;
                Program.mc_id = mc_id;
                Program.op_type = oper_type;

                if (System.IO.File.Exists(Program.path + "\\login.txt"))
                {
                    if (this.checkBox1.Checked)
                        System.IO.File.WriteAllText(Program.path + "\\login.txt", textBox1.Text + "|" + Program.pwd);
                    else
                        System.IO.File.WriteAllText(Program.path + "\\login.txt", "");
                }
                //
                this.Hide();
                frmMain frm = new frmMain();
                frm.Show();
              
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("frmLogin ->login()",ex.ToString(),null);
                Program.frmMsg(ex.Message);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

        private void clear()
        {
            if (this.textBox1.Focused == true)
            {
                this.textBox1.Text = "";
            }
            else if (this.textBox2.Focused == true)
            {
                this.textBox2.Text = "";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 11)
            {
                this.textBox2.Focus();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.textBox2.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.login();
            }
        }

        private void panel17_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //if (textBox2.Text.Length == 6)
            //{
            //    this.login();
            //}
        }

        private void frmLogin_Click1(object sender, EventArgs e)
        {
            
        }

    }
}
