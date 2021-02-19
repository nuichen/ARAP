using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IvyBack.Helper;
using Model;
using System.Threading;

namespace IvyBack.SysForm
{
    public partial class SysSetting : Form
    {
        public SysSetting()
        {
            InitializeComponent();

        }
        Dictionary<string, sys_t_system> setting_dic = new Dictionary<string, sys_t_system>();
        Dictionary<string, sys_t_system> update_dic = new Dictionary<string, sys_t_system>();

        private void LoadSetting()
        {
            IBLL.ISys bll = new BLL.SysBLL();
            setting_dic = bll.GetAllDic();
        }
        private void LoadCb()
        {
            Thread th = new Thread(() =>
            {
                Helper.GlobalData.windows.ShowLoad(this);
                try
                {
                    LoadSetting();
                    Cursor.Current = Cursors.WaitCursor;
                    //日期格式
                    Dictionary<string, string> DateMaskdic = new Dictionary<string, string>()
                   {
                    {"yyyy-MM-dd",DateTime.Now.ToString("yyyy-MM-dd")},
                    {"yyyy-MM-dd HH:MM:SS",DateTime.Now.ToString("yyyy-MM-dd HH:MM:SS")},
                    {"yyyy/MM/dd",DateTime.Now.ToString("yyyy/MM/dd")},
                    {"yyyy年MM月dd日",DateTime.Now.ToString("yyyy年MM月dd日")},
                    {"yy年MM月dd日",DateTime.Now.ToString("yy年MM月dd日")},
                    {"yy-MM-dd",DateTime.Now.ToString("yy-MM-dd")},
                    {"yy-MM-dd HH:MM:SS",DateTime.Now.ToString("yy-MM-dd HH:MM:SS")},
                    };
                    this.cbDateMask.Invoke((MethodInvoker)delegate
                    {
                        this.cbDateMask.DisplayMember = "Value";
                        this.cbDateMask.ValueMember = "Key";
                        this.cbDateMask.DataSource = new BindingSource(DateMaskdic, null);
                    });


                    //数量格式
                    Dictionary<string, string> NumberMaskdic = new Dictionary<string, string>()
                    {
                    {"#,###,###,##0.00","#,###,###,##0.00"},
                    {"#,###,###,##0.0000","#,###,###,##0.0000"},
                    {"#########0.00","#########0.00"},
                    {"#########0.0000","#########0.0000"},
                    {"#,###,###,###.00","#,###,###,###.00"},
                    {"#,###,###,###.0000","#,###,###,###.0000"},
                    {"##########.00","##########.00"},
                    {"##########.##","##########.##"},
                    {"#,###,###,###.##","#,###,###,###.##"},
                    {"#,###,###,##0","#,###,###,##0"},
                    {"##########0","##########0"},
                    {"#,###,###,###","#,###,###,###"},
                    {"##########","##########"},
                    };
                    this.cbNumberMask.Invoke((MethodInvoker)delegate
                    {
                        this.cbNumberMask.DisplayMember = "Value";
                        this.cbNumberMask.ValueMember = "Key";
                        this.cbNumberMask.DataSource = new BindingSource(NumberMaskdic, null);
                    });


                    //价格格式
                    Dictionary<string, string> PriceMaskdic = new Dictionary<string, string>()
                   {
                    {"￥#,###,###,##0.00","￥#,###,###,##0.00"},
                    {"$ #,###,###,##0.00","$ #,###,###,##0.00"},
                    {"#,###,###,##0.00","#,###,###,##0.00"},
                    {"#########0.00","#########0.00"},
                    {"#########0.0000","#########0.0000"},
                    {"#,###,###,##0.0000","#,###,###,##0.0000"},
                    {"#,###,###,###.00","#,###,###,###.00"},
                    {"#,###,###,###.0000","#,###,###,###.0000"},
                    {"#,###,###,###.##","#,###,###,###.##"},
                    {"##########.##","##########.##"},
                    {"#,###,###,##0","#,###,###,##0"},
                    {"#,###,###,###","#,###,###,###"},
                    {"#########0","#########0"},
                    {"##########","##########"},
                   };

                    this.cbPriceMask.Invoke((MethodInvoker)delegate
                    {
                        this.cbPriceMask.DisplayMember = "Value";
                        this.cbPriceMask.ValueMember = "Key";
                        this.cbPriceMask.DataSource = new BindingSource(PriceMaskdic, null);
                    });


                    //金额显示
                    this.cbMoneyMask.Invoke((MethodInvoker)delegate
                    {
                        this.cbMoneyMask.DisplayMember = "Value";
                        this.cbMoneyMask.ValueMember = "Key";
                        this.cbMoneyMask.DataSource = new BindingSource(PriceMaskdic, null);
                    });



                    //仓库
                    IBLL.IBranch bll = new BLL.BranchBLL();
                    var tb = bll.GetAllList("");

                    this.cbgs_defa_branch.Invoke((MethodInvoker)delegate
                    {
                        this.cbgs_defa_branch.DisplayMember = "branch_name";
                        this.cbgs_defa_branch.ValueMember = "branch_no";
                        this.cbgs_defa_branch.DataSource = tb;
                    });


                    //特殊显示下拉框
                    Dictionary<string, string> cbDic = new Dictionary<string, string>()
                   {
                    {">","大于"},
                    {"<","小于"},
                    {"=","标准"},
                    };

                    this.Invoke((MethodInvoker)delegate
                    {
                        this.cbNum.DisplayMember = "Value";
                        this.cbNum.ValueMember = "Key";
                        this.cbNum.DataSource = new BindingSource(cbDic, null);
                        this.cbPrice.DisplayMember = "Value";
                        this.cbPrice.ValueMember = "Key";
                        this.cbPrice.DataSource = new BindingSource(cbDic, null);
                    });


                    //供应商产生规则下拉框
                    Dictionary<string, string> hsDic = new Dictionary<string, string>()    
                   {
                     {"1.商品类别 + 流水号","1.商品类别 + 流水号"},
                     {"2.供应商编码 + 流水号","2.供应商编码 + 流水号"},
                     {"3.自动递增","3.自动递增"},
                   };

                    this.cbItemNoAuto.Invoke((MethodInvoker)delegate
                    {
                        this.cbItemNoAuto.DisplayMember = "Value";
                        this.cbItemNoAuto.ValueMember = "Key";
                        this.cbItemNoAuto.DataSource = new BindingSource(hsDic, null);
                    });


                    //批发级别
                    Dictionary<string, string> pricedic = new Dictionary<string, string>()
                    {
                         {"1","一级"},
                         {"2","二级"},
                         {"3","三级"},
                     };

                    this.cblevel_price_pf.Invoke((MethodInvoker)delegate
                    {
                        this.cblevel_price_pf.DisplayMember = "Value";
                        this.cblevel_price_pf.ValueMember = "Key";
                        this.cblevel_price_pf.DataSource = new BindingSource(pricedic, null);
                    });


                    //超过客户信誉额度销售：cust_credit
                    Dictionary<string, string> cust_creditdic = new Dictionary<string, string>()
                   {
                        {"0","允许"},
                        {"1","不允许"},
                        {"2","允许但提示"},
                    };

                    this.cbcust_credit.Invoke((MethodInvoker)delegate
                    {
                        this.cbcust_credit.DisplayMember = "Value";
                        this.cbcust_credit.ValueMember = "Key";
                        this.cbcust_credit.DataSource = new BindingSource(cust_creditdic, null);
                    });


                    //低于系统库存时出库或高于库存上限进货：stock_check
                    Dictionary<string, string> Stock_checkdic = new Dictionary<string, string>()
                    {
                        {"0","不允许"},
                        {"1","允许，但提示"},
                        {"2","允许，不提示"},
                    };

                    this.cbStock_check.Invoke((MethodInvoker)delegate
                    {
                        this.cbStock_check.DisplayMember = "Value";
                        this.cbStock_check.ValueMember = "Key";
                        this.cbStock_check.DataSource = new BindingSource(Stock_checkdic, null);
                    });


                    //低于进价销售：pos_check_price
                    this.cbpos_check_price.Invoke((MethodInvoker)delegate
                    {
                        this.cbpos_check_price.DisplayMember = "Value";
                        this.cbpos_check_price.ValueMember = "Key";
                        this.cbpos_check_price.DataSource = new BindingSource(Stock_checkdic, null);
                    });


                    //非主供应商商品第一次进货(退货,赠送)：check_sup_item
                    this.cbcheck_sup_item.Invoke((MethodInvoker)delegate
                    {
                        this.cbcheck_sup_item.DisplayMember = "Value";
                        this.cbcheck_sup_item.ValueMember = "Key";
                        this.cbcheck_sup_item.DataSource = new BindingSource(Stock_checkdic, null);
                    });

                    //供应商进货价采用：sup_pricetype
                    Dictionary<string, string> sup_pricetypedic = new Dictionary<string, string>()
                    {
                        {"0","最后进价"},
                        {"1","约定进价"},
                        {"2","最低进价"},
                        {"3","商品档案中的进价"},
                    };
                    this.cbsup_pricetype.Invoke((MethodInvoker)delegate
                    {
                        this.cbsup_pricetype.DisplayMember = "Value";
                        this.cbsup_pricetype.ValueMember = "Key";
                        this.cbsup_pricetype.DataSource = new BindingSource(sup_pricetypedic, null);
                    });


                    //供应商退货价采用：sup_ro_pricetype
                    Dictionary<string, string> sup_ro_pricetypedic = new Dictionary<string, string>()
                    {
                        {"0","最后进价"},
                        {"1","约定进价"},
                        {"2","最低进价"},
                        {"3","商品档案中的进价"},
                        {"4","当前库存平均价"},
                        {"5","指定批次进价"},
                    };

                    this.cbsup_ro_pricetype.Invoke((MethodInvoker)delegate
                    {
                        this.cbsup_ro_pricetype.DisplayMember = "Value";
                        this.cbsup_ro_pricetype.ValueMember = "Key";
                        this.cbsup_ro_pricetype.DataSource = new BindingSource(sup_ro_pricetypedic, null);
                    });


                    //客户批发价采用：cust_pricetype
                    Dictionary<string, string> cust_pricetypedic = new Dictionary<string, string>()
                   {
                    {"0","最后批发价"},
                    {"1","约定批发价"},
                    {"2","最低批发价"},
                    {"3","商品档案中的批发价"},
                   };
                    this.cbcust_pricetype.Invoke((MethodInvoker)delegate
                    {
                        this.cbcust_pricetype.DisplayMember = "Value";
                        this.cbcust_pricetype.ValueMember = "Key";
                        this.cbcust_pricetype.DataSource = new BindingSource(cust_pricetypedic, null);
                    });


                    //4.4客户退货价采用： cust_ri_pricetype
                    Dictionary<string, string> cust_ri_pricetypedic = new Dictionary<string, string>()
                    {    
                    {"0","最后批发价"},
                    {"1","约定批发价"},   
                    {"2","最低批发价"},
                    {"3","商品档案中的批发价"},
                    {"4","指定批次销售价"},
                    };
                    this.cbcust_ri_pricetype.Invoke((MethodInvoker)delegate
                    {
                        this.cbcust_ri_pricetype.DisplayMember = "Value";
                        this.cbcust_ri_pricetype.ValueMember = "Key";
                        this.cbcust_ri_pricetype.DataSource = new BindingSource(cust_ri_pricetypedic, null);
                    });

                    this.Invoke((MethodInvoker)delegate
                    {
                        LoadMain(this);
                        this.Opacity = 100;
                    });


                    Cursor.Current = Cursors.Default;
                }
                catch (Exception ex)
                {
                    LogHelper.writeLog("LoadOper", ex.ToString());
                    MsgForm.ShowFrom(ex);
                }
                GlobalData.windows.CloseLoad(this);
            });
            th.Start();
        }
        private void LoadMain(Control mainControl)
        {
            foreach (Control c in mainControl.Controls)
            {
                if (c.Tag != null && !string.IsNullOrEmpty(c.Tag.ToString()))
                {
                    var tags = c.Tag.ToString().Split(':');

                    sys_t_system value;
                    if (setting_dic.TryGetValue(tags[0], out value))
                    {
                        if (tags.Length == 1)
                        {
                            LoadControls(c, value.sys_var_value);
                        }
                        else if (tags.Length == 2)
                        {
                            if (tags[1].Contains("["))
                            {
                                var str = tags[1].Replace("[", "").Replace("]", "");
                                var objStr = str.Split(',');
                                var values = value.sys_var_value.Split('/');
                                var i = 0;
                                foreach (var obj in objStr)
                                {
                                    if (obj.Equals("this"))
                                    {
                                        LoadControls(c, values[i]);
                                    }
                                    else
                                    {
                                        var con = c.Parent.Controls.Find(obj, true).SingleOrDefault();
                                        if (con != null)
                                            LoadControls(con, values[i]);
                                    }
                                    i++;
                                }
                            }
                            else if (value.sys_var_value.Equals(tags[1]))
                            {
                                LoadControls(c, "");
                            }
                        }
                        else if (tags.Length == 3)
                        {
                            var str = tags[2].Replace("[", "").Replace("]", "");
                            var obj_str = str.Split(',');
                            var values = value.sys_var_value.Split('/');

                            if (values[0].Equals(tags[1]))
                            {
                                LoadControls(c, "");
                            }

                            var i = 1;
                            foreach (var obj in obj_str)
                            {
                                var con = c.Parent.Controls.Find(obj, true).SingleOrDefault();
                                if (con != null)
                                    LoadControls(con, values[i]);
                                i++;
                            }


                        }


                    }
                }
                if (c.Controls != null)
                {
                    LoadMain(c);
                }
            }
        }
        private void LoadControls(Control c, object obj)
        {
            if (c is TextBox)
            {
                TextBox txt = c as TextBox;
                txt.Text = obj.ToString();
            }
            else if (c is RadioButton)
            {
                RadioButton rb = c as RadioButton;
                rb.Checked = true;
            }
            else if (c is NumericUpDown)
            {
                NumericUpDown num = c as NumericUpDown;
                num.Value = Conv.ToInt(obj);
            }
            else if (c is ComboBox)
            {
                ComboBox cb = c as ComboBox;
                if (cb.DataSource != null)
                {
                    cb.SelectedValue = obj;
                }
                else
                {
                    cb.SelectedItem = obj;
                }
            }
            else if (c is CheckBox)
            {
                bool isCheched = false;
                if (obj.Equals("1") || obj.Equals("是") || Boolean.TryParse(obj.ToString(), out isCheched))
                {
                    isCheched = true;
                }
                CheckBox cb = c as CheckBox;
                cb.Checked = isCheched;

            }
            else if (c is Button)
            {
                Button btn = c as Button;
                btn.Text = obj.ToString();
            }
        }

        private void Save()
        {
            CompareCon(this);

            IBLL.ISys bll = new BLL.SysBLL();
            bll.updateDic(update_dic);
        }

        private void CompareCon(Control mainControl)
        {
            foreach (Control c in mainControl.Controls)
            {
                if (c.Tag != null && !string.IsNullOrEmpty(c.Tag.ToString()))
                {
                    var tags = c.Tag.ToString().Split(':');

                    sys_t_system value;
                    if (setting_dic.TryGetValue(tags[0], out value))
                    {
                        if (tags.Length == 1)
                        {
                            string updateValue;
                            if (!Compare(c, value.sys_var_value, out  updateValue))
                            {
                                sys_t_system system;
                                if (update_dic.TryGetValue(value.sys_var_id, out system))
                                {
                                    system.sys_var_value = updateValue;
                                    value.update_time = DateTime.Now;
                                }
                                else
                                {
                                    value.sys_var_value = updateValue;
                                    value.update_time = DateTime.Now;
                                    this.update_dic.Add(value.sys_var_id, value);
                                }
                            }
                        }
                        else if (tags.Length == 2)
                        {
                            string updateValue = "";

                            if (tags[1].Contains("["))
                            {
                                string str = tags[1].Replace("[", "").Replace("]", "");
                                var obj_str = str.Split(',');

                                int i = 0;
                                foreach (string obj in obj_str)
                                {
                                    if (obj.Equals("this"))
                                    {
                                        string s;
                                        Compare(c, "", out s);
                                        updateValue += s + "/";

                                    }
                                    else
                                    {
                                        Control con = c.Parent.Controls.Find(obj, true).SingleOrDefault();
                                        if (con != null)
                                        {
                                            string s;
                                            Compare(con, "", out s);
                                            updateValue += s;
                                            if (obj_str[obj_str.Length - 1] != con.Name)
                                                updateValue += "/";
                                        }
                                    }
                                    i++;
                                }

                                if (updateValue != value.sys_var_value)
                                {
                                    sys_t_system system;
                                    if (update_dic.TryGetValue(value.sys_var_id, out system))
                                    {
                                        system.sys_var_value = updateValue;
                                        value.update_time = DateTime.Now;
                                    }
                                    else
                                    {
                                        value.sys_var_value = updateValue;
                                        value.update_time = DateTime.Now;
                                        this.update_dic.Add(value.sys_var_id, value);
                                    }
                                }
                            }
                            else if (value.sys_var_value.Equals(tags[1]))
                            {
                                if (!Compare(c, "1", out  updateValue))
                                {
                                    sys_t_system system;
                                    if (update_dic.TryGetValue(value.sys_var_id, out system))
                                    {
                                        system.sys_var_value = tags[1];
                                        value.update_time = DateTime.Now;
                                    }
                                    else
                                    {
                                        value.sys_var_value = tags[1];
                                        value.update_time = DateTime.Now;
                                        this.update_dic.Add(value.sys_var_id, value);
                                    }
                                }
                            }
                            else
                            {
                                if (!Compare(c, "0", out  updateValue))
                                {
                                    sys_t_system system;
                                    if (update_dic.TryGetValue(value.sys_var_id, out system))
                                    {
                                        system.sys_var_value = tags[1];
                                        value.update_time = DateTime.Now;
                                    }
                                    else
                                    {
                                        value.sys_var_value = tags[1];
                                        value.update_time = DateTime.Now;
                                        this.update_dic.Add(value.sys_var_id, value);
                                    }
                                }
                            }

                        }
                        else if (tags.Length == 3)
                        {
                            string str = tags[2].Replace("[", "").Replace("]", "");
                            var obj_str = str.Split(',');
                            var values = value.sys_var_value.Split('/');
                            string update_value = "";

                            if (values[0].Equals(tags[1]))
                            {
                                Compare(c, "1", out  update_value);
                            }
                            else
                            {
                                Compare(c, "0", out  update_value);
                            }
                            update_value += "/";
                            foreach (string obj in obj_str)
                            {
                                Control con = c.Parent.Controls.Find(obj, true).SingleOrDefault();
                                if (con != null)
                                {
                                    string s;
                                    Compare(con, "", out s);
                                    update_value += s;
                                }

                                if (obj_str[obj_str.Length - 1] != obj)
                                    update_value += "/";
                            }

                            if (update_value != value.sys_var_value)
                            {
                                sys_t_system system;
                                if (update_dic.TryGetValue(value.sys_var_id, out system))
                                {
                                    system.sys_var_value = update_value;
                                    value.update_time = DateTime.Now;
                                }
                                else
                                {
                                    value.sys_var_value = update_value;
                                    value.update_time = DateTime.Now;
                                    this.update_dic.Add(value.sys_var_id, value);
                                }
                            }
                        }
                    }
                }

                if (c.Controls != null)
                {
                    CompareCon(c);
                }
            }

        }
        private bool Compare(Control c, object obj, out string update_value)
        {
            if (c is TextBox)
            {
                TextBox txt = c as TextBox;
                update_value = txt.Text;
                return txt.Text.Equals(obj.ToString());
            }
            else if (c is RadioButton)
            {
                RadioButton rb = c as RadioButton;
                update_value = rb.Checked ? "1" : "0";
                return update_value.Equals(obj);
            }
            else if (c is NumericUpDown)
            {
                NumericUpDown num = c as NumericUpDown;
                update_value = num.Value.ToString(); ;
                return num.Value == Conv.ToInt(obj);
            }
            else if (c is ComboBox)
            {
                ComboBox cb = c as ComboBox;
                update_value = cb.SelectedValue.ToString();
                return cb.SelectedValue.ToString().Equals(obj);
            }
            else if (c is CheckBox)
            {
                CheckBox cb = c as CheckBox;
                update_value = cb.Checked ? "1" : "0";
                return update_value.Equals(obj);
            }
            else if (c is Button)
            {
                Button btn = c as Button;
                update_value = btn.Text;
                return btn.Text.Equals(obj.ToString());
            }
            update_value = "";
            return false;
        }

        private void SysSetting_Load(object sender, EventArgs e)
        {
            try
            {
                LoadCb();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("SysSetting_Load", ex.ToString());
                MsgForm.ShowFrom(ex);
            }
        }

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MyLove.PermissionsBalidation(this.Text, "14"))
                {
                    
                    return;
                }
                Save();
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }

        private void btnSaveNoClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MyLove.PermissionsBalidation(this.Text, "14"))
                {
                    
                    return;
                }
                Save();
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSpNumber_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                this.btnSpNumber.Text = System.Drawing.ColorTranslator.ToHtml(cd.Color);
            }
        }

        private void btnSpMoney_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                this.btnSpMoney.Text = System.Drawing.ColorTranslator.ToHtml(cd.Color);
            }
        }

        private void cbSpDisplay_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control c in gbSpDisplay.Controls)
            {
                if (c == sender) continue;
                c.Enabled = this.cbSpDisplay.Checked;
            }
        }

        private void btnSpMoney_TextChanged(object sender, EventArgs e)
        {
            this.txtSpMoney.ForeColor = System.Drawing.ColorTranslator.FromHtml(btnSpMoney.Text);
        }

        private void btnSpNumber_TextChanged(object sender, EventArgs e)
        {
            this.txtSpNumber.ForeColor = System.Drawing.ColorTranslator.FromHtml(btnSpNumber.Text);
        }

        private void rbitem_rule_NoAuto_CheckedChanged(object sender, EventArgs e)
        {
            this.cbItemNoAuto.Enabled = !this.rbitem_rule_NoAuto.Checked;
            this.numItemNoAuto.Enabled = !this.rbitem_rule_NoAuto.Checked;
        }


    }
}
