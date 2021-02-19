using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace b2bclient
{
    public partial class frmIcon : Form
    {
        private frmMain frmMain;
        public frmIcon(frmMain frmMain)
        {
            InitializeComponent();
            //
            this.frmMain = frmMain;
            //
            control.MoveHelper.Bind(this.pictureBox1);
            control.MoveHelper.Bind(this.pictureBox2);
        }

        private void frmIcon_Click(object sender, EventArgs e)
        {

        }

        private void frmIcon_DoubleClick(object sender, EventArgs e)
        {
          
        }

        private void frmIcon_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width-100, 100);
           
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
            frmMain.Visible = true;
            frmMain.WindowState = FormWindowState.Maximized;
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
            frmMain.Visible = true;
            frmMain.WindowState = FormWindowState.Maximized;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Program.lstNewOrder.Count()>0)
            {
                if (pictureBox2.Visible == false)
                {
                    pictureBox2.Visible = true;
                }
                else
                {
                    pictureBox2.Visible = false;
                }
            }
        }

      
        

    }
}
