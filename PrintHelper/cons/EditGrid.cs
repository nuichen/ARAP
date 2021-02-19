using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PrintHelper.cons
{
    public partial class EditGrid : Form,IEditGrid 
    {
        public EditGrid()
        {
            InitializeComponent();
            //
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToAddRows = false;
            foreach (DataGridViewColumn col in this.dataGridView1.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
           
        }

        private void EditGrid_Load(object sender, EventArgs e)
        {

        }

        private GridStyleInfo info
        {
            get
            {
                GridStyleInfo res = new GridStyleInfo();
                res.left = Convert.ToInt16((float)Helper.Conv.ToDecimal(textBox1.Text.Trim()) * Helper.Conv.getAnCMInterval());
                if (checkBox1.Checked == true)
                {

                    res.AppendEmptyRow = 1;
                }
                else
                {
                    res.AppendEmptyRow = 0;
                }
              
                res.font = new Font("宋体", Convert.ToInt16(comboBox1.Text));
                return res;
            }
            set
            {
                textBox1.Text = ((float)Convert.ToDecimal(value.left) / Helper.Conv.getAnCMInterval()).ToString("0.##");
                if (value.AppendEmptyRow  == 1)
                {
                    checkBox1.Checked = true;
                }
                else
                {
                    checkBox1.Checked = false;
                }
                
                foreach (object item in comboBox1.Items)
                {
                    if (item.ToString() == Convert.ToInt16( value.font.Size).ToString())
                    {
                        comboBox1.Text = item.ToString();
                        break;
                    }
                }
                if (comboBox1.Text.Trim() == "")
                {
                    comboBox1.Text = "9";
                }
            }
        }

        bool IEditGrid.Edit(System.Data.DataTable tb, GridStyleInfo info, out System.Data.DataTable tb2, out GridStyleInfo info2)
        {
            //

            this.dataGridView1.DataSource = tb.Copy();
            this.info = info;
            //
            if (this.ShowDialog() == DialogResult.OK)
            {
                tb2 = (DataTable)this.dataGridView1.DataSource;
                info2 = this.info;
                return true;
            }
            else
            {

                tb2 = null;
                info2 = null;
                return false;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            //
            this.DialogResult = DialogResult.OK;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show(e.Exception.Message);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                DataGridViewColumn col = this.dataGridView1.Columns[e.ColumnIndex];
                if (col.Name  == colFormat.Name )
                {
                 
                    DataRowView drv =(DataRowView) this.dataGridView1.Rows[e.RowIndex].DataBoundItem;
                    DataRow dr = drv.Row;
                    string format = dr["format"].ToString();
                    IInput input = new InputFormatString();
                    if (input.Input(format, out format) == true)
                    {
                        dr["format"] = format;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var tb = (DataTable)this.dataGridView1.DataSource;
            foreach (DataRow row in tb.Rows)
            {
                row["display"] = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var tb = (DataTable)this.dataGridView1.DataSource;
            foreach (DataRow row in tb.Rows)
            {
                row["display"] = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var tb = (DataTable)this.dataGridView1.DataSource;
            if (this.dataGridView1.CurrentRow != null)
            {
                DataRowView drv =(DataRowView) this.dataGridView1.CurrentRow.DataBoundItem;
                DataRow dr = drv.Row;
                object[] itemarr = dr.ItemArray;
                tb.Rows.Remove(dr);
                dr.ItemArray = itemarr;
                tb.Rows.InsertAt(dr, 0);
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0].Cells[0];
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var tb = (DataTable)this.dataGridView1.DataSource;
            if (this.dataGridView1.CurrentRow != null)
            {
                DataRowView drv = (DataRowView)this.dataGridView1.CurrentRow.DataBoundItem;
                DataRow dr = drv.Row;

                int index = tb.Rows.IndexOf(dr);
               
                if (index != 0)
                {
                    index--;
                    object[] itemarr = dr.ItemArray;
                    tb.Rows.Remove(dr);
                    dr.ItemArray = itemarr;
                    tb.Rows.InsertAt(dr, index);
                    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[index].Cells[0];
                }

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var tb = (DataTable)this.dataGridView1.DataSource;
            if (this.dataGridView1.CurrentRow != null)
            {
                DataRowView drv = (DataRowView)this.dataGridView1.CurrentRow.DataBoundItem;
                DataRow dr = drv.Row;

                int index = tb.Rows.IndexOf(dr);

                if (index !=tb.Rows.Count-1)
                {
                    index++;
                    object[] itemarr = dr.ItemArray;
                    tb.Rows.Remove(dr);
                    dr.ItemArray = itemarr;
                    tb.Rows.InsertAt(dr, index);
                    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[index].Cells[0];
                }

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var tb = (DataTable)this.dataGridView1.DataSource;
            if (this.dataGridView1.CurrentRow != null)
            {
                DataRowView drv = (DataRowView)this.dataGridView1.CurrentRow.DataBoundItem;
                DataRow dr = drv.Row;
                object[] itemarr = dr.ItemArray;
                tb.Rows.Remove(dr);
                dr.ItemArray = itemarr;
                tb.Rows.Add(dr);
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[tb.Rows.Count-1].Cells[0];
            }
        }

    }
}
