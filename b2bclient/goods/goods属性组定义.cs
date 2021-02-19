using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace b2bclient.goods
{
    public partial class goods属性组定义 : UserControl
    {
        private List<Model.goods_std> lststd;
        public goods属性组定义( List<Model.goods_std> lststd)
        {
            InitializeComponent();
            //
            this.lststd = lststd;
            //
            control.DataGridViewStyle.SetStyle(this.dataGridView1);
          
            this.dataGridView1.Font = new Font("微软雅黑", 11);
            this.dataGridView1.AutoGenerateColumns = false;
            foreach (System.Windows.Forms.DataGridViewColumn col in this.dataGridView1.Columns)
            {
                col.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            }
         
            dataGridView1.AllowUserToAddRows = false;
            //
            int row_index = 0;
            foreach (Model.goods_std std in lststd)
            {
                row_index++;
                int index = this.dataGridView1.Rows.Add();
                DataGridViewRow row = this.dataGridView1.Rows[index];
                row.Cells[0].Value =row_index ;
                row.Cells[5].Value = std.qty.ToString();
                if (std.is_default == "1")
                {
                    row.Cells[1].Value =true ;
                }
                else
                {
                    row.Cells[1].Value = false ;
                }
                if (std.prices != "")
                {
                    string[] arr = std.prices.Split(',');
                    if (arr.Length >= 1)
                    {
                        row.Cells[2].Value = arr[0];
                    }
                    else
                    {
                        row.Cells[2].Value = "";
                    }
                    if (arr.Length >= 2)
                    {
                        row.Cells[3].Value = arr[1];
                    }
                    else
                    {
                        row.Cells[3].Value = "";
                    }
                    if (arr.Length >= 3)
                    {
                        row.Cells[4].Value = arr[2];
                    }
                    else
                    {
                        row.Cells[4].Value = "";
                    }
                }
            }
            if (row_index <= 0) {
                row_index++;
                int index = this.dataGridView1.Rows.Add();
                DataGridViewRow row = this.dataGridView1.Rows[index];
                row.Cells[0].Value = row_index;
                row.Cells[1].Value = false;
                row.Cells[2].Value = "";
                row.Cells[3].Value = "";
                row.Cells[4].Value = "";
                row.Cells[5].Value = "";
            }
        }


        public List<Model.goods_std> GetData()
        {
            var lst = new List<Model.goods_std>();
            
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((row.Cells[4].Value != null && row.Cells[4].Value.ToString() != ""))
                {
                     
                    var std = new Model.goods_std();
                    if (row.Cells[1].Value.ToString().ToLower() == "true".ToLower())
                    {
                        std.is_default = "1";
                    }
                    else
                    {
                        std.is_default = "0";
                    }

                    decimal p1 = Conv.ToDecimal(row.Cells[2].Value.ToString());
                    decimal p2 = Conv.ToDecimal(row.Cells[3].Value.ToString());
                    decimal p3 = Conv.ToDecimal(row.Cells[4].Value.ToString());
                    float qty = Conv.ToFloat(row.Cells[5].Value.ToString());

                    std.prices = p1 + "," + p2 + "," + p3 + ",,";
                    std.qty = qty;
                    lst.Add(std);
                }
            }
            //
            int flag = 0;
            foreach (Model.goods_std std in lst)
            {
                if (std.is_default == "1")
                {
                    flag = 1;
                }
            }
            if (flag == 0)
            {
                if (lst.Count != 0)
                {
                    lst[0].is_default = "1";
                }
            }
            return lst;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1)
            {
                return;
            }
            if (e.RowIndex == -1)
            {
                return;
            }
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            if (e.ColumnIndex == 1)
            {
                if (row.Cells[1].Value.ToString().ToLower() != "true".ToLower())
                {
                    foreach (DataGridViewRow r in this.dataGridView1.Rows)
                    {
                        r.Cells[1].Value = false;
                    }
                    row.Cells[1].Value = true;
                }
                
            }
        }

    }
}
