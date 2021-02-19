using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace IvyBack.MainForm
{
    public partial class frmLoad : Form
    {
        public frmLoad()
        {
            InitializeComponent();
        }
        public static Dictionary<Form, frmLoad> frms = new Dictionary<Form, frmLoad>();
        static Dictionary<Form, Form> wait_frms = new Dictionary<Form, Form>();
        public static Action<string> changeInfoAction;
        public static void Flush()
        {
            foreach (Form frm in frms.Keys)
            {
                frms[frm].Refresh();
            }
        }

        public static void LoadWait(Form par_frm)
        {
            if (!frms.Keys.Contains(par_frm) && !wait_frms.Keys.Contains(par_frm))
            {
                frmLoad frm = new frmLoad();
                frms.Add(par_frm, frm);
                frm.Show(par_frm);

                Form f = new Form();
                f.Opacity = 0.01;
                f.BackColor = Color.White;
                f.FormBorderStyle = FormBorderStyle.None;
                f.Location = Helper.GlobalData.mainFrm.Location;
                f.Width = Helper.GlobalData.mainFrm.Width;
                f.Height = Helper.GlobalData.mainFrm.Height;
                f.Left = Helper.GlobalData.mainFrm.Left;
                f.Top = Helper.GlobalData.mainFrm.Top;
                f.WindowState = FormWindowState.Normal;
                f.StartPosition = FormStartPosition.Manual;
                wait_frms.Add(par_frm, f);
                f.Show(Helper.GlobalData.mainFrm);
            }
        }
        public static void LoadWait(Form par_frm, Task task, string info = "加载数据")
        {
            if (!frms.Keys.Contains(par_frm))
            {
                frmLoad frm = new frmLoad();
                frm.title = info;
                changeInfoAction = (s) => { frm.Invoke(new Action(() => { frm.title = s; })); };
                frms.Add(par_frm, frm);

                task?.ContinueWith(t => { par_frm.Invoke(new Action(() => { LoadClose(par_frm); })); });

                frm.ShowDialog(par_frm);
            }
        }

        public static void LoadClose(Form par_frm)
        {
            if (frms.TryGetValue(par_frm, out var frm))
            {
                frms.Remove(par_frm);
                frm.Close();
            }
            if (wait_frms.TryGetValue(par_frm, out var frmw))
            {
                wait_frms.Remove(par_frm);
                frmw.Close();
            }
        }
        private void frmLoad_Load(object sender, EventArgs e)
        {
            this.lblinfo.Text = title;
        }



        public string title = "正在加载数据";
        int num = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (num < 5)
                num++;
            else
                num = 0;

            this.lblinfo.Text = title + new string('.', num);
        }

        private void frmLoad_Paint(object sender, PaintEventArgs e)
        {
            Color c = Color.FromArgb(0, 159, 233);
            for (int i = 0; i < 2; i++)
            {
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(c.R + i, c.G + i, c.B + i), 1F),
                    new Rectangle(i, i, this.Width - i * 2 - 1, this.Height - i * 2 - 1));
            }

        }


    }

    public static class frmLoadExtension
    {
        public static void LoadWait(this Form par_frm, Task task, string info = "加载数据")
        {
            if (!frmLoad.frms.Keys.Contains(par_frm))
            {
                frmLoad frm = new frmLoad();
                frm.title = info;
                frmLoad.changeInfoAction = (s) => { frm.Invoke(new Action(() => { frm.title = s; })); };
                frmLoad.frms.Add(par_frm, frm);

                task?.ContinueWith(t => { par_frm.Invoke(new Action(() => { frmLoad.LoadClose(par_frm); })); });

                frm.ShowDialog(par_frm);
            }
        }
    }


}
