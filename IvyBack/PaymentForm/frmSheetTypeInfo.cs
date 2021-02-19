using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IvyBack.Helper;

namespace IvyBack.VoucherForm
{
    public partial class frmSheetTypeInfo : Form
    {
        public DataTable tb1;
        public DataTable tb2;
        public frmSheetTypeInfo()
        {
            InitializeComponent();
            Helper.GlobalData.InitForm(this);
            dataGrid1.MergeCell = false;
            dataGrid1.DataSource = new DataTable();
            dataGrid2.DataSource = new DataTable();
         
        }
  
      

      

     

      

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      

        private void frmSheetTypeInfo_Load(object sender, EventArgs e)
        {
            dataGrid1.DataSource = tb1;
            dataGrid2.DataSource = tb2;
        }
    }
}
