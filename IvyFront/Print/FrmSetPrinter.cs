using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Print
{
    public partial class FrmSetPrinter : Form
    {
        public FrmSetPrinter()
        {
            InitializeComponent();
        }

        private string PrinterName
        {
            get
            {
                if (System.IO.File.Exists(Program.path + "\\setting\\print_name.txt") == true)
                {
                    return System.IO.File.ReadAllText(Program.path + "\\setting\\print_name.txt", System.Text.Encoding.GetEncoding("gb2312"));
                }
                else
                {
                    return "";
                }
            }
            set
            {
                System.IO.File.WriteAllText(Program.path + "\\setting\\print_name.txt", value, System.Text.Encoding.GetEncoding("gb2312"));
            }
        }

        private void FrmSetPrinter_Load(object sender, EventArgs e)
        {
            textBox1.Text = PrinterName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text) == false)
            { PrinterName = textBox1.Text; }
        }

    }
}
