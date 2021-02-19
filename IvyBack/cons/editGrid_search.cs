using IvyBack.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IvyBack.cons
{
    public partial class editGrid_search : Form
    {
         EditGrid form;
       int searchRow_index=0;
        public editGrid_search(Control form)
        {
            InitializeComponent();
            this.form = form as EditGrid;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool is_searched = false;
            for (int i = searchRow_index; i < form.DataSource.Rows.Count; i++)
            {
                if (checkBox1.Checked)
                {
                    if (Conv.ToString(form.DataSource.Rows[i][this.searchLabel.Tag.ToString()]).Equals(this.searchTextBox.Text) )
                    {
                        searchRow_index = i;
                        is_searched = true;
                        break;
                    }
                }
                else
                {


                    if (Conv.ToString(form.DataSource.Rows[i][this.searchLabel.Tag.ToString()]).IndexOf(this.searchTextBox.Text) != -1)
                    {
                        searchRow_index = i;
                        is_searched = true;
                        break;
                    }
                }
             
            }

            if(!is_searched)
            {
                searchRow_index = 0;
            }
            form.v_value = 0;
            form.v_value += searchRow_index * form.rowHeight;
            if (form.v_value < 0)
            {
                form.v_value = 0;
            }
            if (form.v_value > form.DataSource.Rows.Count * form.rowHeight)
            {
                form.v_value = form.DataSource.Rows.Count * form.rowHeight;
            }

            form.cur_cell.row_index = searchRow_index;
            form.Refresh();
            searchRow_index++;
            if(!is_searched || searchRow_index > form.DataSource.Rows.Count)
            {
                searchRow_index = 0;
            }
          
        }

        private void searchTextBox_Leave(object sender, EventArgs e)
        {
            searchRow_index = 0;
        }
    }
}
