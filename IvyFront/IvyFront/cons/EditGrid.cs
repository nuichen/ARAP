using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IvyFront.cons
{

    public delegate void DataGridViewColumnButtonClickHandler(DataGridViewColumn col,DataGridViewRow row,Control cell);

    class EditGrid: DataGridView 
    {

        public EditGrid()
        {
            InitializeComponent();
        }

        private  Control  iniButton()
        {
           var  btn = new Panel();
            btn.BackgroundImageLayout = ImageLayout.Center;
            btn.BackgroundImage = this.pnlsup.BackgroundImage;
            btn.Width = 30;
            btn.Dock = DockStyle.Right;
            btn.Cursor = Cursors.Default;
            cons.AddClickAct.Add(pnlsup);
            btn.Click -= this.btn_click;
            btn.Click += this.btn_click;
            return btn;
        }

        private void btn_click(object sender, EventArgs e)
        {
            DataGridViewColumn col = this.Columns[this.CurrentCell.ColumnIndex];
            if (_DataGridViewColumnButtonClick != null)
            {
                _DataGridViewColumnButtonClick.Invoke(col,this.CurrentRow,this.EditingControl);
            }
        }

        public List<DataGridViewColumn> ChooseColumns = new List<DataGridViewColumn>();
        private Panel pnlsup;

        private  DataGridViewColumnButtonClickHandler _DataGridViewColumnButtonClick;
        public event DataGridViewColumnButtonClickHandler  DataGridViewColumnButtonClick
        {
            add
            {
                _DataGridViewColumnButtonClick += value;
            }
            remove
            {
                _DataGridViewColumnButtonClick -= value;
            }

        }
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                SendKeys.SendWait("{Tab}");
                return true;
            }
            if (keyData == Keys.F2)
            {
                if (this.CurrentCell != null)
                {
                    this.BeginEdit(true);
                    DataGridViewColumn col = this.Columns[this.CurrentCell.ColumnIndex];
                    if (ChooseColumns.Contains(col))
                    {
                        if (_DataGridViewColumnButtonClick != null)
                        {
                            _DataGridViewColumnButtonClick.Invoke(col, this.CurrentRow, this.EditingControl);
                        }
                    }
                   

                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditGrid));
            this.pnlsup = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlsup
            // 
            this.pnlsup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlsup.BackgroundImage")));
            this.pnlsup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlsup.Location = new System.Drawing.Point(285, 117);
            this.pnlsup.Name = "pnlsup";
            this.pnlsup.Size = new System.Drawing.Size(34, 27);
            this.pnlsup.TabIndex = 21;
            // 
            // EditGrid
            // 
            this.RowTemplate.Height = 23;
            this.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.EditGrid_EditingControlShowing);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        private void EditGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var btn = this.iniButton();
            DataGridViewColumn col = this.Columns[this.CurrentCell.ColumnIndex];
            if (ChooseColumns.Contains(col) == true)
            {
                e.Control.Controls.Clear();
                e.Control.Controls.Add(btn);
            }
            else
            {
                e.Control.Controls.Clear();
            }
           
          
        }
    }
}
