using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using IvyBack;
using IvyBack.MainForm;
using Exception = System.Exception;

namespace UnitTest_IvyBack
{
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmLoad.LoadWait(this, Task.Factory.StartNew(() =>
            {
                try
                {
                    Thread.Sleep(2000);
                    throw new Exception();
                }
                catch (Exception exception)
                {
                    Action action = new Action(() =>
                    {
                        Clipboard.SetDataObject("你好啊");;
                    });
                    
                    this.Invoke(action);
                }
            }), "正在加载中");
        }
    }
}
