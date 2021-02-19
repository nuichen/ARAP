using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace IvyFront.Forms
{
    public class CellInfo
    {
        public CellInfo()
        {

        }

        public CellInfo(Rectangle rec, Model.t_order_detail item, string columnName)
        {
            this.rec = rec;
            this.item = item;
            this.columnName = columnName;
        }

        public Rectangle rec { get; set; }
        public Model.t_order_detail item { get; set; }
        public string columnName { get; set; }
    }
}
