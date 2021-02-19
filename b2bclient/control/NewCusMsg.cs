using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace b2bclient.control
{
    public partial class NewCusMsg : UserControl
    {
        public NewCusMsg()
        {
            InitializeComponent();
        }

        private bool bShow = true;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DesignMode == true)
            {
                return;
            }
            if (Program.lstNewCus.Count != 0)
            {
                this.Visible = true;
            }
            else
            {
                this.Visible = false;
            }
            if (bShow == true)
            {
                this.BackgroundImage = Properties.Resources._11_09;
                bShow = false;
            }
            else
            {
                this.BackgroundImage = null;
                bShow = true;
            }
        }
    }
}
