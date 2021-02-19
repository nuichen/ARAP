using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;//添加命名空间

namespace ParSetting
{
    public partial class Form1 : Form
    {
        List<key_value> lst = new List<key_value>();
        public Form1()
        {
            InitializeComponent();
            try
            {
                PrintDocument print = new PrintDocument();
                string sDefault = print.PrinterSettings.PrinterName;//默认打印机名

                foreach (string sPrint in PrinterSettings.InstalledPrinters)//获取所有打印机名称
                {
                    lst.Add(new key_value { key_str = sPrint, value_str = sPrint });
                }
                cb_print_name.DataSource = lst;

                IBLL.IPar par = new BLL.Par_aes();
                txt_ws_svr.Text= par.Read("app_set", "ws_svr");
                txt_svr.Text = par.Read("app_set", "svr");
                txt_mer_key.Text = par.Read("app_set", "mer_key");
                string is_bind_ip = par.Read("app_set", "is_bind_ip");
                chk_bind_ip.Checked = is_bind_ip.Equals("1");

                string is_mobile_pay = par.Read("app_set", "is_mobile_pay");
                if (is_mobile_pay == "1")
                {
                    chk_is_mobile_pay.Checked = true;
                    txt_svr.Visible = true;
                    txt_mer_key.Visible = true;
                    label10.Visible = true;
                    label14.Visible = true;
                }
                else
                {
                    chk_is_mobile_pay.Checked = false;
                    txt_svr.Visible = false;
                    txt_mer_key.Visible = false;
                    label10.Visible = false;
                    label14.Visible = false;
                }

                var print_name = par.Read("app_set", "print_name");
                string is_print = par.Read("app_set", "is_print");
                if (is_print == "1")
                {
                    chk_is_print.Checked = true;
                    label9.Visible = true;
                    cb_print_name.Visible = true;
                    var is_p = lst.FirstOrDefault(d => d.key_str == print_name);
                    if (is_p != null)
                    {
                        cb_print_name.SelectedValue = print_name;
                    }
                    else
                    {
                        cb_print_name.SelectedValue = sDefault;
                    }
                }
                else
                {
                    chk_is_print.Checked = false;
                    label9.Visible = false;
                    cb_print_name.Visible = false;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_ws_svr.Text.Trim() == "")
                {
                    throw new Exception("服务器地址必填");
                }
                var is_mobile_pay = "0";
                if (chk_is_mobile_pay.Checked) is_mobile_pay = "1";

                if (is_mobile_pay == "1" && txt_svr.Text.Trim() == "")
                {
                    throw new Exception("移动支付接口必填");
                }
                if (is_mobile_pay == "1" && txt_mer_key.Text.Trim() == "")
                {
                    throw new Exception("商家密钥必填");
                }
                var print_name = "";
                
                IBLL.IPar par = new BLL.Par_aes();
                par.Write("app_set", "ws_svr", txt_ws_svr.Text.Trim());
                par.Write("app_set", "is_mobile_pay", is_mobile_pay);
                par.Write("app_set", "svr", txt_svr.Text.Trim());
                par.Write("app_set", "mer_key", txt_mer_key.Text.Trim());
                string is_print = "0";
                if (chk_is_print.Checked) is_print = "1";
                if (is_print == "1") 
                {
                    if (cb_print_name.SelectedValue != null) print_name = cb_print_name.SelectedValue.ToString();
                }
                par.Write("app_set", "print_name", print_name);
                par.Write("app_set", "is_print", is_print);
                par.Write("app_set", "is_bind_ip", chk_bind_ip.Checked ? "1" : "0");
                System.Windows.Forms.MessageBox.Show("保存成功!");

                this.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chk_is_mobile_pay_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_is_mobile_pay.Checked)
            {
                label10.Visible = true;
                label14.Visible = true;
                txt_svr.Visible = true;
                txt_mer_key.Visible = true;
            }
            else 
            {
                label10.Visible = false;
                label14.Visible = false;
                txt_svr.Visible = false;
                txt_mer_key.Visible = false;
            }
        }

        private void chk_is_print_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_is_print.Checked)
            {
                label9.Visible = true;
                cb_print_name.Visible = true;
            }
            else 
            {
                label9.Visible = false;
                cb_print_name.Visible = false;
            }
        }

    }
}
