using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var tbMain = new DataTable();
            tbMain.Columns.Add("单号");
            tbMain.Columns.Add("单据日期");
            tbMain.Columns.Add("操作员");
            
            tbMain.Rows.Add("FH00001", "2018-10-10", "1001");
            //
            var tbDetail = new DataTable();
            tbDetail.Columns.Add("货号");
            tbDetail.Columns.Add("品名");
            tbDetail.Columns.Add("数量", typeof(decimal));
            tbDetail.Columns.Add("单价", typeof(decimal));
            tbDetail.Columns.Add("金额", typeof(decimal));
            for (int i = 1; i <= Convert.ToInt16(textBox1.Text.Trim()); i++)
            {
                tbDetail.Rows.Add(i.ToString(), i.ToString(), 10, 20, 200);
            }
           
            //
           
        }

      
    }
}
