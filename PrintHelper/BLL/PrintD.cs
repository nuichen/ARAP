using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace PrintHelper.BLL
{
    public class PrintD : Form, IBLL.IPrint, cons.IDesign
    {
        private System.Data.DataTable tbmain;
        private Panel panel2;
        private ContextMenuStrip contextMenuStrip1;
        private System.ComponentModel.IContainer components;
        private cons.DesignPanel pnl;
        private System.Data.DataTable tbdetail;

        private Helper.OperRecord operRecord;
        private MenuStrip menuStrip1;
        private ToolStrip toolStrip1;
        private ToolStrip toolStrip2;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripButton3;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripButton4;
        private ToolStripButton toolStripButton6;
        private ToolStripButton toolStripButton7;
        private ToolStripButton toolStripButton8;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton toolStripButton10;
        private ToolStripButton toolStripButton14;
        private ToolStripButton toolStripButton15;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripButton toolStripButton16;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripSplitButton toolStripSplitButton1;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripMenuItem 下框线ToolStripMenuItem;
        private ToolStripMenuItem 上框线ToolStripMenuItem;
        private ToolStripMenuItem 左框线ToolStripMenuItem;
        private ToolStripMenuItem 右框线ToolStripMenuItem;
        private ToolStripMenuItem 无框线ToolStripMenuItem;
        private ToolStripMenuItem 外侧框线ToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSplitButton toolStripSplitButton3;
        private ToolStripMenuItem 黑色ToolStripMenuItem;
        private ToolStripMenuItem 蓝色ToolStripMenuItem;
        private ToolStripMenuItem 红色ToolStripMenuItem;
        private ToolStripMenuItem 更多ToolStripMenuItem;
        private ToolStripMenuItem 更多ToolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripButton toolStripButton9;
        private ToolStripButton toolStripButton11;
        private ToolStripButton toolStripButton12;
        private ToolStripButton toolStripButton17;
        private ToolStripSeparator toolStripSeparator9;
        private ToolStripButton toolStripButton18;
        internal ToolStripButton TbtnPointer;
        internal ToolStripButton TbtnTextBox;
        internal ToolStripButton TbtnPicBox;
        internal ToolStripButton TbtnLineH;
        internal ToolStripButton TbtnLineV;
        internal ToolStripButton TbtnSum;
        private ToolStripButton TbtnPageIndex;
        private ToolStripButton TbtnPrintTime;
        internal ToolStripButton TbtnPrintPage;
        private ToolStripMenuItem 文件ToolStripMenuItem;
        private ToolStripMenuItem 重置上次保存格式ToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem 保存ToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem 页面设置ToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripMenuItem 预览ToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem4;
        private ToolStripMenuItem 退出ToolStripMenuItem;
        private ToolStripMenuItem 编辑ToolStripMenuItem;
        private ToolStripMenuItem 撤销ToolStripMenuItem;
        private ToolStripMenuItem 恢复ToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem5;
        private ToolStripMenuItem 剪切ToolStripMenuItem;
        private ToolStripMenuItem 复制ToolStripMenuItem;
        private ToolStripMenuItem 粘贴ToolStripMenuItem;
        private ToolStripMenuItem 删除ToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem6;
        private ToolStripMenuItem 全选ToolStripMenuItem;
        private ToolStripMenuItem 插入ToolStripMenuItem;
        private ToolStripMenuItem 文本标签ToolStripMenuItem;
        private ToolStripMenuItem 图片ToolStripMenuItem;
        private ToolStripMenuItem 横线ToolStripMenuItem;
        private ToolStripMenuItem 竖线ToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem7;
        private ToolStripMenuItem 合计ToolStripMenuItem;
        private ToolStripMenuItem 页码ToolStripMenuItem;
        private ToolStripMenuItem 打印时间ToolStripMenuItem;
        private ToolStripMenuItem 格式ToolStripMenuItem;
        private ToolStripMenuItem 控件对齐ToolStripMenuItem;
        private ToolStripMenuItem 文本对齐ToolStripMenuItem;
        private ToolStripMenuItem 边框ToolStripMenuItem;
        private ToolStripMenuItem 字体颜色ToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem8;
        private ToolStripMenuItem 属性ToolStripMenuItem;
        private ToolStripMenuItem 帮助ToolStripMenuItem;
        private ToolStripMenuItem 左对齐ToolStripMenuItem;
        private ToolStripMenuItem 右对齐ToolStripMenuItem;
        private ToolStripMenuItem 上对齐ToolStripMenuItem;
        private ToolStripMenuItem 下对齐ToolStripMenuItem;
        private ToolStripMenuItem 下框线ToolStripMenuItem1;
        private ToolStripMenuItem 上框线ToolStripMenuItem1;
        private ToolStripMenuItem 左框线ToolStripMenuItem1;
        private ToolStripMenuItem 右框线ToolStripMenuItem1;
        private ToolStripMenuItem 无框线ToolStripMenuItem1;
        private ToolStripMenuItem 外侧框线ToolStripMenuItem1;
        private ToolStripMenuItem 更多ToolStripMenuItem2;
        private ToolStripMenuItem 黑色ToolStripMenuItem1;
        private ToolStripMenuItem 蓝色ToolStripMenuItem1;
        private ToolStripMenuItem 红色ToolStripMenuItem1;
        private ToolStripMenuItem 更多ToolStripMenuItem3;
        private cons.FieldList fieldList1;
        private Timer timer1;
        private ToolStripButton toolStripButton5;
        private ToolStripButton toolStripButton13;
        private ToolStripComboBox toolStripComboBox1;
        private ToolStripLabel toolStripLabel1;
        private ToolStripSeparator toolStripSeparator10;
        private ToolStripButton toolStripButton19;
        private ToolStripButton toolStripButton20;
        private ToolStripButton toolStripButton21;
        private ToolStripMenuItem 居左ToolStripMenuItem;
        private ToolStripMenuItem 居中ToolStripMenuItem;
        private ToolStripMenuItem 居右ToolStripMenuItem;
        private ToolStripMenuItem 剪切ToolStripMenuItem1;
        private ToolStripMenuItem 复制ToolStripMenuItem1;
        private ToolStripMenuItem 粘贴ToolStripMenuItem1;
        private ToolStripMenuItem 删除ToolStripMenuItem1;
        private ToolStripSeparator 分隔toolStripMenuItem;
        private ToolStripMenuItem tsmiExportStyle;
        private ToolStripSeparator toolStripSeparator11;
        private ToolStripMenuItem tsmiImportStyle;
        private ToolStripButton tsbImport;
        private ToolStripButton tsbExport;
        private cons.IDesign des;
        public PrintD()
        {
            this.InitializeComponent();
            des = this;
            //
            for (int i = 7; i <= 60; i++)
            {
                this.toolStripComboBox1.Items.Add(i.ToString());
            }

        }

        public delegate void SaveStyleHandler(string style_id, string xml);
        public event SaveStyleHandler _SaveStyle;
        public event SaveStyleHandler SaveStyle
        {
            add
            {
                _SaveStyle += value;
            }
            remove
            {
                _SaveStyle -= value;
            }
        }

        private string style_id = "";
        private string xml = "";
        void IBLL.IPrint.Print(string style_id, string xml, System.Data.DataTable tbmain, System.Data.DataTable tbdetail)
        {
            try
            {
                this.style_id = style_id;
                this.xml = xml;
                this.tbmain = tbmain;
                this.tbdetail = tbdetail;

                if (this.tbdetail.Columns.Contains("行号") == true)
                {
                    this.tbdetail.Columns.Remove("行号");
                    this.tbdetail.Columns.Add("行号");
                    this.tbdetail.Columns["行号"].SetOrdinal(0);
                }
                else
                {
                    this.tbdetail.Columns.Add("行号");
                    this.tbdetail.Columns["行号"].SetOrdinal(0);
                }
                pnl.DataSource = tbdetail;
                //
                this.fieldList1.DataSource = this.tbmain;
                //

                if (xml != "")
                {
                    cons.IDesign des = this;
                    des.xml = xml;
                }
                else
                {
                    pnl.PageWidth = (int)(21 * Helper.Conv.getAnCMInterval());
                    pnl.PageHeight = (int)(29.7F * Helper.Conv.getAnCMInterval());
                }
                if (1 == 1)
                {
                    cons.IDesign des = this;
                    operRecord = new Helper.OperRecord(des.xml);
                }
                //
                this.ShowDialog();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private bool is_edit = false;
        public void Record()
        {
            is_edit = true;
            Helper.OperRecord oper = new Helper.OperRecord(des.xml);
            operRecord.Next = oper;
            oper.Pre = operRecord;
            operRecord = oper;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintD));
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnl = new PrintHelper.cons.DesignPanel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.剪切ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.复制ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.粘贴ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.分隔toolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重置上次保存格式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.页面设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiImportStyle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportStyle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.预览ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.撤销ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.恢复ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.剪切ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.全选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.插入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文本标签ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图片ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.横线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.竖线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.合计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.页码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打印时间ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.格式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.控件对齐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.左对齐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.右对齐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上对齐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下对齐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文本对齐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.居左ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.居中ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.居右ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.边框ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下框线ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.上框线ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.左框线ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.右框线ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.无框线ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.外侧框线ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.更多ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.字体颜色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.黑色ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.蓝色ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.红色ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.更多ToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.属性ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.TbtnPrintPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton19 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton20 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton21 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton13 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton14 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton15 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton16 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.下框线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上框线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.左框线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.右框线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.无框线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.外侧框线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更多ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton3 = new System.Windows.Forms.ToolStripSplitButton();
            this.黑色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.蓝色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.红色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更多ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton11 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton12 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton17 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton18 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.TbtnPointer = new System.Windows.Forms.ToolStripButton();
            this.TbtnTextBox = new System.Windows.Forms.ToolStripButton();
            this.TbtnPicBox = new System.Windows.Forms.ToolStripButton();
            this.TbtnLineH = new System.Windows.Forms.ToolStripButton();
            this.TbtnLineV = new System.Windows.Forms.ToolStripButton();
            this.TbtnSum = new System.Windows.Forms.ToolStripButton();
            this.TbtnPageIndex = new System.Windows.Forms.ToolStripButton();
            this.TbtnPrintTime = new System.Windows.Forms.ToolStripButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.fieldList1 = new PrintHelper.cons.FieldList();
            this.tsbExport = new System.Windows.Forms.ToolStripButton();
            this.tsbImport = new System.Windows.Forms.ToolStripButton();
            this.panel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel2.Controls.Add(this.pnl);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(75, 75);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(933, 435);
            this.panel2.TabIndex = 1;
            // 
            // pnl
            // 
            this.pnl.AHeight = 80;
            this.pnl.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pnl.BHeight = 80;
            this.pnl.CHeight = 30;
            this.pnl.DataSource = null;
            this.pnl.DHeight = 80;
            this.pnl.EHeight = 80;
            this.pnl.Location = new System.Drawing.Point(0, 0);
            this.pnl.Name = "pnl";
            this.pnl.PageHeight = 3000;
            this.pnl.PageWidth = 1000;
            this.pnl.Size = new System.Drawing.Size(545, 431);
            this.pnl.TabIndex = 0;
            this.pnl.DataChange += new System.EventHandler(this.pnl_DataSize);
            this.pnl.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_Paint);
            this.pnl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnl_MouseClick);
            this.pnl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnl_MouseDown);
            this.pnl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnl_MouseMove);
            this.pnl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnl_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.剪切ToolStripMenuItem1,
            this.复制ToolStripMenuItem1,
            this.粘贴ToolStripMenuItem1,
            this.删除ToolStripMenuItem1,
            this.分隔toolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 98);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // 剪切ToolStripMenuItem1
            // 
            this.剪切ToolStripMenuItem1.Name = "剪切ToolStripMenuItem1";
            this.剪切ToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.剪切ToolStripMenuItem1.Text = "剪切";
            // 
            // 复制ToolStripMenuItem1
            // 
            this.复制ToolStripMenuItem1.Name = "复制ToolStripMenuItem1";
            this.复制ToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.复制ToolStripMenuItem1.Text = "复制";
            // 
            // 粘贴ToolStripMenuItem1
            // 
            this.粘贴ToolStripMenuItem1.Name = "粘贴ToolStripMenuItem1";
            this.粘贴ToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.粘贴ToolStripMenuItem1.Text = "粘贴";
            // 
            // 删除ToolStripMenuItem1
            // 
            this.删除ToolStripMenuItem1.Name = "删除ToolStripMenuItem1";
            this.删除ToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.删除ToolStripMenuItem1.Text = "删除";
            // 
            // 分隔toolStripMenuItem
            // 
            this.分隔toolStripMenuItem.Name = "分隔toolStripMenuItem";
            this.分隔toolStripMenuItem.Size = new System.Drawing.Size(97, 6);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.插入ToolStripMenuItem,
            this.格式ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            this.menuStrip1.Click += new System.EventHandler(this.menuStrip1_Click);
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.重置上次保存格式ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.保存ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.页面设置ToolStripMenuItem,
            this.toolStripSeparator11,
            this.tsmiImportStyle,
            this.tsmiExportStyle,
            this.toolStripMenuItem3,
            this.预览ToolStripMenuItem,
            this.toolStripMenuItem4,
            this.退出ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            this.文件ToolStripMenuItem.DropDownOpened += new System.EventHandler(this.文件ToolStripMenuItem_DropDownOpened);
            // 
            // 重置上次保存格式ToolStripMenuItem
            // 
            this.重置上次保存格式ToolStripMenuItem.Name = "重置上次保存格式ToolStripMenuItem";
            this.重置上次保存格式ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.重置上次保存格式ToolStripMenuItem.Text = "重置上次保存格式";
            this.重置上次保存格式ToolStripMenuItem.Click += new System.EventHandler(this.重置上次保存格式ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.保存ToolStripMenuItem.Text = "保存";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
            // 
            // 页面设置ToolStripMenuItem
            // 
            this.页面设置ToolStripMenuItem.Name = "页面设置ToolStripMenuItem";
            this.页面设置ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.页面设置ToolStripMenuItem.Text = "页面设置...";
            this.页面设置ToolStripMenuItem.Click += new System.EventHandler(this.页面设置ToolStripMenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(177, 6);
            // 
            // tsmiImportStyle
            // 
            this.tsmiImportStyle.Name = "tsmiImportStyle";
            this.tsmiImportStyle.Size = new System.Drawing.Size(180, 22);
            this.tsmiImportStyle.Text = "导入...";
            this.tsmiImportStyle.Click += new System.EventHandler(this.tsmiImportStyle_Click);
            // 
            // tsmiExportStyle
            // 
            this.tsmiExportStyle.Name = "tsmiExportStyle";
            this.tsmiExportStyle.Size = new System.Drawing.Size(180, 22);
            this.tsmiExportStyle.Text = "导出...";
            this.tsmiExportStyle.Click += new System.EventHandler(this.tsmiExportStyle_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(177, 6);
            // 
            // 预览ToolStripMenuItem
            // 
            this.预览ToolStripMenuItem.Name = "预览ToolStripMenuItem";
            this.预览ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.预览ToolStripMenuItem.Text = "预览...";
            this.预览ToolStripMenuItem.Click += new System.EventHandler(this.预览ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(177, 6);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.撤销ToolStripMenuItem,
            this.恢复ToolStripMenuItem,
            this.toolStripMenuItem5,
            this.剪切ToolStripMenuItem,
            this.复制ToolStripMenuItem,
            this.粘贴ToolStripMenuItem,
            this.删除ToolStripMenuItem,
            this.toolStripMenuItem6,
            this.全选ToolStripMenuItem});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.编辑ToolStripMenuItem.Text = "编辑";
            this.编辑ToolStripMenuItem.DropDownOpened += new System.EventHandler(this.编辑ToolStripMenuItem_DropDownOpened);
            // 
            // 撤销ToolStripMenuItem
            // 
            this.撤销ToolStripMenuItem.Name = "撤销ToolStripMenuItem";
            this.撤销ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.撤销ToolStripMenuItem.Text = "撤销";
            this.撤销ToolStripMenuItem.Click += new System.EventHandler(this.撤销ToolStripMenuItem_Click);
            // 
            // 恢复ToolStripMenuItem
            // 
            this.恢复ToolStripMenuItem.Name = "恢复ToolStripMenuItem";
            this.恢复ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.恢复ToolStripMenuItem.Text = "恢复";
            this.恢复ToolStripMenuItem.Click += new System.EventHandler(this.恢复ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(97, 6);
            // 
            // 剪切ToolStripMenuItem
            // 
            this.剪切ToolStripMenuItem.Name = "剪切ToolStripMenuItem";
            this.剪切ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.剪切ToolStripMenuItem.Text = "剪切";
            this.剪切ToolStripMenuItem.Click += new System.EventHandler(this.剪切ToolStripMenuItem_Click);
            // 
            // 复制ToolStripMenuItem
            // 
            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            this.复制ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.复制ToolStripMenuItem.Text = "复制";
            this.复制ToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // 粘贴ToolStripMenuItem
            // 
            this.粘贴ToolStripMenuItem.Name = "粘贴ToolStripMenuItem";
            this.粘贴ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.粘贴ToolStripMenuItem.Text = "粘贴";
            this.粘贴ToolStripMenuItem.Click += new System.EventHandler(this.粘贴ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(97, 6);
            // 
            // 全选ToolStripMenuItem
            // 
            this.全选ToolStripMenuItem.Name = "全选ToolStripMenuItem";
            this.全选ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.全选ToolStripMenuItem.Text = "全选";
            this.全选ToolStripMenuItem.Click += new System.EventHandler(this.全选ToolStripMenuItem_Click);
            // 
            // 插入ToolStripMenuItem
            // 
            this.插入ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文本标签ToolStripMenuItem,
            this.图片ToolStripMenuItem,
            this.横线ToolStripMenuItem,
            this.竖线ToolStripMenuItem,
            this.toolStripMenuItem7,
            this.合计ToolStripMenuItem,
            this.页码ToolStripMenuItem,
            this.打印时间ToolStripMenuItem});
            this.插入ToolStripMenuItem.Name = "插入ToolStripMenuItem";
            this.插入ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.插入ToolStripMenuItem.Text = "插入";
            this.插入ToolStripMenuItem.DropDownOpened += new System.EventHandler(this.插入ToolStripMenuItem_DropDownOpened);
            // 
            // 文本标签ToolStripMenuItem
            // 
            this.文本标签ToolStripMenuItem.Name = "文本标签ToolStripMenuItem";
            this.文本标签ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.文本标签ToolStripMenuItem.Text = "文本标签";
            this.文本标签ToolStripMenuItem.Click += new System.EventHandler(this.文本标签ToolStripMenuItem_Click);
            // 
            // 图片ToolStripMenuItem
            // 
            this.图片ToolStripMenuItem.Name = "图片ToolStripMenuItem";
            this.图片ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.图片ToolStripMenuItem.Text = "图片";
            this.图片ToolStripMenuItem.Click += new System.EventHandler(this.图片ToolStripMenuItem_Click);
            // 
            // 横线ToolStripMenuItem
            // 
            this.横线ToolStripMenuItem.Name = "横线ToolStripMenuItem";
            this.横线ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.横线ToolStripMenuItem.Text = "横线";
            this.横线ToolStripMenuItem.Click += new System.EventHandler(this.横线ToolStripMenuItem_Click);
            // 
            // 竖线ToolStripMenuItem
            // 
            this.竖线ToolStripMenuItem.Name = "竖线ToolStripMenuItem";
            this.竖线ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.竖线ToolStripMenuItem.Text = "竖线";
            this.竖线ToolStripMenuItem.Click += new System.EventHandler(this.竖线ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(121, 6);
            // 
            // 合计ToolStripMenuItem
            // 
            this.合计ToolStripMenuItem.Name = "合计ToolStripMenuItem";
            this.合计ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.合计ToolStripMenuItem.Text = "合计";
            this.合计ToolStripMenuItem.Click += new System.EventHandler(this.合计ToolStripMenuItem_Click);
            // 
            // 页码ToolStripMenuItem
            // 
            this.页码ToolStripMenuItem.Name = "页码ToolStripMenuItem";
            this.页码ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.页码ToolStripMenuItem.Text = "页码";
            this.页码ToolStripMenuItem.Click += new System.EventHandler(this.页码ToolStripMenuItem_Click);
            // 
            // 打印时间ToolStripMenuItem
            // 
            this.打印时间ToolStripMenuItem.Name = "打印时间ToolStripMenuItem";
            this.打印时间ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.打印时间ToolStripMenuItem.Text = "打印时间";
            this.打印时间ToolStripMenuItem.Click += new System.EventHandler(this.打印时间ToolStripMenuItem_Click);
            // 
            // 格式ToolStripMenuItem
            // 
            this.格式ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.控件对齐ToolStripMenuItem,
            this.文本对齐ToolStripMenuItem,
            this.边框ToolStripMenuItem,
            this.字体颜色ToolStripMenuItem,
            this.toolStripMenuItem8,
            this.属性ToolStripMenuItem});
            this.格式ToolStripMenuItem.Name = "格式ToolStripMenuItem";
            this.格式ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.格式ToolStripMenuItem.Text = "格式";
            this.格式ToolStripMenuItem.DropDownOpened += new System.EventHandler(this.格式ToolStripMenuItem_DropDownOpened);
            // 
            // 控件对齐ToolStripMenuItem
            // 
            this.控件对齐ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.左对齐ToolStripMenuItem,
            this.右对齐ToolStripMenuItem,
            this.上对齐ToolStripMenuItem,
            this.下对齐ToolStripMenuItem});
            this.控件对齐ToolStripMenuItem.Name = "控件对齐ToolStripMenuItem";
            this.控件对齐ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.控件对齐ToolStripMenuItem.Text = "控件对齐";
            // 
            // 左对齐ToolStripMenuItem
            // 
            this.左对齐ToolStripMenuItem.Name = "左对齐ToolStripMenuItem";
            this.左对齐ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.左对齐ToolStripMenuItem.Text = "左对齐";
            this.左对齐ToolStripMenuItem.Click += new System.EventHandler(this.左对齐ToolStripMenuItem_Click);
            // 
            // 右对齐ToolStripMenuItem
            // 
            this.右对齐ToolStripMenuItem.Name = "右对齐ToolStripMenuItem";
            this.右对齐ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.右对齐ToolStripMenuItem.Text = "右对齐";
            this.右对齐ToolStripMenuItem.Click += new System.EventHandler(this.右对齐ToolStripMenuItem_Click);
            // 
            // 上对齐ToolStripMenuItem
            // 
            this.上对齐ToolStripMenuItem.Name = "上对齐ToolStripMenuItem";
            this.上对齐ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.上对齐ToolStripMenuItem.Text = "上对齐";
            this.上对齐ToolStripMenuItem.Click += new System.EventHandler(this.上对齐ToolStripMenuItem_Click);
            // 
            // 下对齐ToolStripMenuItem
            // 
            this.下对齐ToolStripMenuItem.Name = "下对齐ToolStripMenuItem";
            this.下对齐ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.下对齐ToolStripMenuItem.Text = "下对齐";
            this.下对齐ToolStripMenuItem.Click += new System.EventHandler(this.下对齐ToolStripMenuItem_Click);
            // 
            // 文本对齐ToolStripMenuItem
            // 
            this.文本对齐ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.居左ToolStripMenuItem,
            this.居中ToolStripMenuItem,
            this.居右ToolStripMenuItem});
            this.文本对齐ToolStripMenuItem.Name = "文本对齐ToolStripMenuItem";
            this.文本对齐ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.文本对齐ToolStripMenuItem.Text = "文本对齐";
            // 
            // 居左ToolStripMenuItem
            // 
            this.居左ToolStripMenuItem.Name = "居左ToolStripMenuItem";
            this.居左ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.居左ToolStripMenuItem.Text = "居左";
            this.居左ToolStripMenuItem.Click += new System.EventHandler(this.居左ToolStripMenuItem_Click);
            // 
            // 居中ToolStripMenuItem
            // 
            this.居中ToolStripMenuItem.Name = "居中ToolStripMenuItem";
            this.居中ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.居中ToolStripMenuItem.Text = "居中";
            this.居中ToolStripMenuItem.Click += new System.EventHandler(this.居中ToolStripMenuItem_Click);
            // 
            // 居右ToolStripMenuItem
            // 
            this.居右ToolStripMenuItem.Name = "居右ToolStripMenuItem";
            this.居右ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.居右ToolStripMenuItem.Text = "居右";
            this.居右ToolStripMenuItem.Click += new System.EventHandler(this.居右ToolStripMenuItem_Click);
            // 
            // 边框ToolStripMenuItem
            // 
            this.边框ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.下框线ToolStripMenuItem1,
            this.上框线ToolStripMenuItem1,
            this.左框线ToolStripMenuItem1,
            this.右框线ToolStripMenuItem1,
            this.无框线ToolStripMenuItem1,
            this.外侧框线ToolStripMenuItem1,
            this.更多ToolStripMenuItem2});
            this.边框ToolStripMenuItem.Name = "边框ToolStripMenuItem";
            this.边框ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.边框ToolStripMenuItem.Text = "边框";
            // 
            // 下框线ToolStripMenuItem1
            // 
            this.下框线ToolStripMenuItem1.Name = "下框线ToolStripMenuItem1";
            this.下框线ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.下框线ToolStripMenuItem1.Text = "下框线";
            this.下框线ToolStripMenuItem1.Click += new System.EventHandler(this.下框线ToolStripMenuItem1_Click);
            // 
            // 上框线ToolStripMenuItem1
            // 
            this.上框线ToolStripMenuItem1.Name = "上框线ToolStripMenuItem1";
            this.上框线ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.上框线ToolStripMenuItem1.Text = "上框线";
            this.上框线ToolStripMenuItem1.Click += new System.EventHandler(this.上框线ToolStripMenuItem1_Click);
            // 
            // 左框线ToolStripMenuItem1
            // 
            this.左框线ToolStripMenuItem1.Name = "左框线ToolStripMenuItem1";
            this.左框线ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.左框线ToolStripMenuItem1.Text = "左框线";
            this.左框线ToolStripMenuItem1.Click += new System.EventHandler(this.左框线ToolStripMenuItem1_Click);
            // 
            // 右框线ToolStripMenuItem1
            // 
            this.右框线ToolStripMenuItem1.Name = "右框线ToolStripMenuItem1";
            this.右框线ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.右框线ToolStripMenuItem1.Text = "右框线";
            this.右框线ToolStripMenuItem1.Click += new System.EventHandler(this.右框线ToolStripMenuItem1_Click);
            // 
            // 无框线ToolStripMenuItem1
            // 
            this.无框线ToolStripMenuItem1.Name = "无框线ToolStripMenuItem1";
            this.无框线ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.无框线ToolStripMenuItem1.Text = "无框线";
            this.无框线ToolStripMenuItem1.Click += new System.EventHandler(this.无框线ToolStripMenuItem1_Click);
            // 
            // 外侧框线ToolStripMenuItem1
            // 
            this.外侧框线ToolStripMenuItem1.Name = "外侧框线ToolStripMenuItem1";
            this.外侧框线ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.外侧框线ToolStripMenuItem1.Text = "外侧框线";
            this.外侧框线ToolStripMenuItem1.Click += new System.EventHandler(this.外侧框线ToolStripMenuItem1_Click);
            // 
            // 更多ToolStripMenuItem2
            // 
            this.更多ToolStripMenuItem2.Name = "更多ToolStripMenuItem2";
            this.更多ToolStripMenuItem2.Size = new System.Drawing.Size(124, 22);
            this.更多ToolStripMenuItem2.Text = "更多...";
            this.更多ToolStripMenuItem2.Click += new System.EventHandler(this.更多ToolStripMenuItem2_Click);
            // 
            // 字体颜色ToolStripMenuItem
            // 
            this.字体颜色ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.黑色ToolStripMenuItem1,
            this.蓝色ToolStripMenuItem1,
            this.红色ToolStripMenuItem1,
            this.更多ToolStripMenuItem3});
            this.字体颜色ToolStripMenuItem.Name = "字体颜色ToolStripMenuItem";
            this.字体颜色ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.字体颜色ToolStripMenuItem.Text = "字体颜色";
            // 
            // 黑色ToolStripMenuItem1
            // 
            this.黑色ToolStripMenuItem1.Name = "黑色ToolStripMenuItem1";
            this.黑色ToolStripMenuItem1.Size = new System.Drawing.Size(109, 22);
            this.黑色ToolStripMenuItem1.Text = "黑色";
            this.黑色ToolStripMenuItem1.Click += new System.EventHandler(this.黑色ToolStripMenuItem1_Click);
            // 
            // 蓝色ToolStripMenuItem1
            // 
            this.蓝色ToolStripMenuItem1.ForeColor = System.Drawing.Color.Blue;
            this.蓝色ToolStripMenuItem1.Name = "蓝色ToolStripMenuItem1";
            this.蓝色ToolStripMenuItem1.Size = new System.Drawing.Size(109, 22);
            this.蓝色ToolStripMenuItem1.Text = "蓝色";
            this.蓝色ToolStripMenuItem1.Click += new System.EventHandler(this.蓝色ToolStripMenuItem1_Click);
            // 
            // 红色ToolStripMenuItem1
            // 
            this.红色ToolStripMenuItem1.ForeColor = System.Drawing.Color.Red;
            this.红色ToolStripMenuItem1.Name = "红色ToolStripMenuItem1";
            this.红色ToolStripMenuItem1.Size = new System.Drawing.Size(109, 22);
            this.红色ToolStripMenuItem1.Text = "红色";
            this.红色ToolStripMenuItem1.Click += new System.EventHandler(this.红色ToolStripMenuItem1_Click);
            // 
            // 更多ToolStripMenuItem3
            // 
            this.更多ToolStripMenuItem3.Name = "更多ToolStripMenuItem3";
            this.更多ToolStripMenuItem3.Size = new System.Drawing.Size(109, 22);
            this.更多ToolStripMenuItem3.Text = "更多...";
            this.更多ToolStripMenuItem3.Click += new System.EventHandler(this.更多ToolStripMenuItem3_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(121, 6);
            // 
            // 属性ToolStripMenuItem
            // 
            this.属性ToolStripMenuItem.Name = "属性ToolStripMenuItem";
            this.属性ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.属性ToolStripMenuItem.Text = "属性";
            this.属性ToolStripMenuItem.Click += new System.EventHandler(this.属性ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            this.帮助ToolStripMenuItem.DropDownOpened += new System.EventHandler(this.帮助ToolStripMenuItem_DropDownOpened);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.tsbImport,
            this.tsbExport,
            this.toolStripButton2,
            this.TbtnPrintPage,
            this.toolStripSeparator1,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripSeparator2,
            this.toolStripButton5,
            this.toolStripButton6,
            this.toolStripButton7,
            this.toolStripButton8,
            this.toolStripSeparator3,
            this.toolStripButton10,
            this.toolStripSeparator10,
            this.toolStripButton19,
            this.toolStripButton20,
            this.toolStripButton21,
            this.toolStripSeparator8,
            this.toolStripLabel1,
            this.toolStripComboBox1,
            this.toolStripButton13,
            this.toolStripButton14,
            this.toolStripButton15,
            this.toolStripSeparator6,
            this.toolStripButton16,
            this.toolStripSeparator7,
            this.toolStripSplitButton1,
            this.toolStripSeparator4,
            this.toolStripSplitButton3,
            this.toolStripSeparator5,
            this.toolStripButton9,
            this.toolStripButton11,
            this.toolStripButton12,
            this.toolStripButton17,
            this.toolStripSeparator9,
            this.toolStripButton18});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(1008, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStrip1_KeyDown);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Silver;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "保存";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Silver;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "预览";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // TbtnPrintPage
            // 
            this.TbtnPrintPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TbtnPrintPage.Image = ((System.Drawing.Image)(resources.GetObject("TbtnPrintPage.Image")));
            this.TbtnPrintPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TbtnPrintPage.Name = "TbtnPrintPage";
            this.TbtnPrintPage.Size = new System.Drawing.Size(23, 22);
            this.TbtnPrintPage.Text = "页面设置";
            this.TbtnPrintPage.ToolTipText = "设置打印页面";
            this.TbtnPrintPage.Click += new System.EventHandler(this.TbtnPrintPage_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Silver;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "撤销";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Silver;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "恢复";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "剪切";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton6.Text = "复制";
            this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Silver;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton7.Text = "粘贴";
            this.toolStripButton7.Click += new System.EventHandler(this.toolStripButton7_Click);
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton8.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton8.Image")));
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Silver;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton8.Text = "删除";
            this.toolStripButton8.Click += new System.EventHandler(this.toolStripButton8_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton10
            // 
            this.toolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton10.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton10.Image")));
            this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Silver;
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton10.Text = "全选";
            this.toolStripButton10.Click += new System.EventHandler(this.toolStripButton10_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton19
            // 
            this.toolStripButton19.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton19.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton19.Image")));
            this.toolStripButton19.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton19.Name = "toolStripButton19";
            this.toolStripButton19.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton19.Text = "居左";
            this.toolStripButton19.Click += new System.EventHandler(this.toolStripButton19_Click);
            // 
            // toolStripButton20
            // 
            this.toolStripButton20.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton20.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton20.Image")));
            this.toolStripButton20.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton20.Name = "toolStripButton20";
            this.toolStripButton20.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton20.Text = "居右";
            this.toolStripButton20.Click += new System.EventHandler(this.toolStripButton20_Click);
            // 
            // toolStripButton21
            // 
            this.toolStripButton21.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton21.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton21.Image")));
            this.toolStripButton21.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton21.Name = "toolStripButton21";
            this.toolStripButton21.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton21.Text = "居中";
            this.toolStripButton21.Click += new System.EventHandler(this.toolStripButton21_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(32, 22);
            this.toolStripLabel1.Text = "字号";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(75, 25);
            this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            this.toolStripComboBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripComboBox1_KeyDown);
            // 
            // toolStripButton13
            // 
            this.toolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton13.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton13.Image")));
            this.toolStripButton13.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton13.Name = "toolStripButton13";
            this.toolStripButton13.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton13.Text = "粗体";
            this.toolStripButton13.Click += new System.EventHandler(this.toolStripButton13_Click);
            // 
            // toolStripButton14
            // 
            this.toolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton14.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton14.Image")));
            this.toolStripButton14.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton14.Name = "toolStripButton14";
            this.toolStripButton14.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton14.Text = "斜体";
            this.toolStripButton14.Click += new System.EventHandler(this.toolStripButton14_Click);
            // 
            // toolStripButton15
            // 
            this.toolStripButton15.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton15.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton15.Image")));
            this.toolStripButton15.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton15.Name = "toolStripButton15";
            this.toolStripButton15.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton15.Text = "下划线";
            this.toolStripButton15.Click += new System.EventHandler(this.toolStripButton15_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton16
            // 
            this.toolStripButton16.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton16.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton16.Image")));
            this.toolStripButton16.ImageTransparentColor = System.Drawing.Color.Silver;
            this.toolStripButton16.Name = "toolStripButton16";
            this.toolStripButton16.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton16.Text = "属性";
            this.toolStripButton16.Click += new System.EventHandler(this.toolStripButton16_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.下框线ToolStripMenuItem,
            this.上框线ToolStripMenuItem,
            this.左框线ToolStripMenuItem,
            this.右框线ToolStripMenuItem,
            this.无框线ToolStripMenuItem,
            this.外侧框线ToolStripMenuItem,
            this.更多ToolStripMenuItem});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(60, 22);
            this.toolStripSplitButton1.Text = "无框线";
            this.toolStripSplitButton1.ButtonClick += new System.EventHandler(this.toolStripSplitButton1_ButtonClick);
            // 
            // 下框线ToolStripMenuItem
            // 
            this.下框线ToolStripMenuItem.Name = "下框线ToolStripMenuItem";
            this.下框线ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.下框线ToolStripMenuItem.Text = "下框线";
            this.下框线ToolStripMenuItem.Click += new System.EventHandler(this.下框线ToolStripMenuItem_Click);
            // 
            // 上框线ToolStripMenuItem
            // 
            this.上框线ToolStripMenuItem.Name = "上框线ToolStripMenuItem";
            this.上框线ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.上框线ToolStripMenuItem.Text = "上框线";
            this.上框线ToolStripMenuItem.Click += new System.EventHandler(this.上框线ToolStripMenuItem_Click);
            // 
            // 左框线ToolStripMenuItem
            // 
            this.左框线ToolStripMenuItem.Name = "左框线ToolStripMenuItem";
            this.左框线ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.左框线ToolStripMenuItem.Text = "左框线";
            this.左框线ToolStripMenuItem.Click += new System.EventHandler(this.左框线ToolStripMenuItem_Click);
            // 
            // 右框线ToolStripMenuItem
            // 
            this.右框线ToolStripMenuItem.Name = "右框线ToolStripMenuItem";
            this.右框线ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.右框线ToolStripMenuItem.Text = "右框线";
            this.右框线ToolStripMenuItem.Click += new System.EventHandler(this.右框线ToolStripMenuItem_Click);
            // 
            // 无框线ToolStripMenuItem
            // 
            this.无框线ToolStripMenuItem.Name = "无框线ToolStripMenuItem";
            this.无框线ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.无框线ToolStripMenuItem.Text = "无框线";
            this.无框线ToolStripMenuItem.Click += new System.EventHandler(this.无框线ToolStripMenuItem_Click);
            // 
            // 外侧框线ToolStripMenuItem
            // 
            this.外侧框线ToolStripMenuItem.Name = "外侧框线ToolStripMenuItem";
            this.外侧框线ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.外侧框线ToolStripMenuItem.Text = "外侧框线";
            this.外侧框线ToolStripMenuItem.Click += new System.EventHandler(this.外侧框线ToolStripMenuItem_Click);
            // 
            // 更多ToolStripMenuItem
            // 
            this.更多ToolStripMenuItem.Name = "更多ToolStripMenuItem";
            this.更多ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.更多ToolStripMenuItem.Text = "更多...";
            this.更多ToolStripMenuItem.Click += new System.EventHandler(this.更多ToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSplitButton3
            // 
            this.toolStripSplitButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.黑色ToolStripMenuItem,
            this.蓝色ToolStripMenuItem,
            this.红色ToolStripMenuItem,
            this.更多ToolStripMenuItem1});
            this.toolStripSplitButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton3.Image")));
            this.toolStripSplitButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton3.Name = "toolStripSplitButton3";
            this.toolStripSplitButton3.Size = new System.Drawing.Size(72, 22);
            this.toolStripSplitButton3.Text = "字体颜色";
            this.toolStripSplitButton3.ButtonClick += new System.EventHandler(this.toolStripSplitButton3_ButtonClick);
            // 
            // 黑色ToolStripMenuItem
            // 
            this.黑色ToolStripMenuItem.Name = "黑色ToolStripMenuItem";
            this.黑色ToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.黑色ToolStripMenuItem.Text = "黑色";
            this.黑色ToolStripMenuItem.Click += new System.EventHandler(this.黑色ToolStripMenuItem_Click);
            // 
            // 蓝色ToolStripMenuItem
            // 
            this.蓝色ToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.蓝色ToolStripMenuItem.Name = "蓝色ToolStripMenuItem";
            this.蓝色ToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.蓝色ToolStripMenuItem.Text = "蓝色";
            this.蓝色ToolStripMenuItem.Click += new System.EventHandler(this.蓝色ToolStripMenuItem_Click);
            // 
            // 红色ToolStripMenuItem
            // 
            this.红色ToolStripMenuItem.ForeColor = System.Drawing.Color.Red;
            this.红色ToolStripMenuItem.Name = "红色ToolStripMenuItem";
            this.红色ToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.红色ToolStripMenuItem.Text = "红色";
            this.红色ToolStripMenuItem.Click += new System.EventHandler(this.红色ToolStripMenuItem_Click);
            // 
            // 更多ToolStripMenuItem1
            // 
            this.更多ToolStripMenuItem1.Name = "更多ToolStripMenuItem1";
            this.更多ToolStripMenuItem1.Size = new System.Drawing.Size(109, 22);
            this.更多ToolStripMenuItem1.Text = "更多...";
            this.更多ToolStripMenuItem1.Click += new System.EventHandler(this.更多ToolStripMenuItem1_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton9
            // 
            this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton9.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton9.Image")));
            this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton9.Name = "toolStripButton9";
            this.toolStripButton9.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton9.Text = "左对齐";
            this.toolStripButton9.Click += new System.EventHandler(this.toolStripButton9_Click);
            // 
            // toolStripButton11
            // 
            this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton11.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton11.Image")));
            this.toolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton11.Name = "toolStripButton11";
            this.toolStripButton11.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton11.Text = "右对齐";
            this.toolStripButton11.Click += new System.EventHandler(this.toolStripButton11_Click);
            // 
            // toolStripButton12
            // 
            this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton12.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton12.Image")));
            this.toolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton12.Name = "toolStripButton12";
            this.toolStripButton12.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton12.Text = "上对齐";
            this.toolStripButton12.Click += new System.EventHandler(this.toolStripButton12_Click);
            // 
            // toolStripButton17
            // 
            this.toolStripButton17.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton17.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton17.Image")));
            this.toolStripButton17.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton17.Name = "toolStripButton17";
            this.toolStripButton17.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton17.Text = "下对齐";
            this.toolStripButton17.Click += new System.EventHandler(this.toolStripButton17_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton18
            // 
            this.toolStripButton18.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton18.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton18.Image")));
            this.toolStripButton18.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton18.Name = "toolStripButton18";
            this.toolStripButton18.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton18.Text = "退出";
            this.toolStripButton18.Click += new System.EventHandler(this.toolStripButton18_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TbtnPointer,
            this.TbtnTextBox,
            this.TbtnPicBox,
            this.TbtnLineH,
            this.TbtnLineV,
            this.TbtnSum,
            this.TbtnPageIndex,
            this.TbtnPrintTime});
            this.toolStrip2.Location = new System.Drawing.Point(0, 50);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(1008, 25);
            this.toolStrip2.TabIndex = 4;
            this.toolStrip2.Text = "toolStrip2";
            this.toolStrip2.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip2_ItemClicked);
            // 
            // TbtnPointer
            // 
            this.TbtnPointer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TbtnPointer.Image = ((System.Drawing.Image)(resources.GetObject("TbtnPointer.Image")));
            this.TbtnPointer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TbtnPointer.Name = "TbtnPointer";
            this.TbtnPointer.Size = new System.Drawing.Size(23, 22);
            this.TbtnPointer.Text = "指针";
            this.TbtnPointer.ToolTipText = "指针";
            // 
            // TbtnTextBox
            // 
            this.TbtnTextBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TbtnTextBox.Image = ((System.Drawing.Image)(resources.GetObject("TbtnTextBox.Image")));
            this.TbtnTextBox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TbtnTextBox.Name = "TbtnTextBox";
            this.TbtnTextBox.Size = new System.Drawing.Size(23, 22);
            this.TbtnTextBox.Text = "文本框";
            this.TbtnTextBox.ToolTipText = "文本框";
            this.TbtnTextBox.Click += new System.EventHandler(this.TbtnTextBox_Click);
            // 
            // TbtnPicBox
            // 
            this.TbtnPicBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TbtnPicBox.Image = ((System.Drawing.Image)(resources.GetObject("TbtnPicBox.Image")));
            this.TbtnPicBox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TbtnPicBox.Name = "TbtnPicBox";
            this.TbtnPicBox.Size = new System.Drawing.Size(23, 22);
            this.TbtnPicBox.Text = "图片框";
            this.TbtnPicBox.ToolTipText = "图片框";
            this.TbtnPicBox.Click += new System.EventHandler(this.TbtnPicBox_Click);
            // 
            // TbtnLineH
            // 
            this.TbtnLineH.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TbtnLineH.Image = ((System.Drawing.Image)(resources.GetObject("TbtnLineH.Image")));
            this.TbtnLineH.ImageTransparentColor = System.Drawing.Color.White;
            this.TbtnLineH.Name = "TbtnLineH";
            this.TbtnLineH.Size = new System.Drawing.Size(23, 22);
            this.TbtnLineH.Text = "水平线";
            this.TbtnLineH.ToolTipText = "水平线";
            // 
            // TbtnLineV
            // 
            this.TbtnLineV.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TbtnLineV.Image = ((System.Drawing.Image)(resources.GetObject("TbtnLineV.Image")));
            this.TbtnLineV.ImageTransparentColor = System.Drawing.Color.White;
            this.TbtnLineV.Name = "TbtnLineV";
            this.TbtnLineV.Size = new System.Drawing.Size(23, 22);
            this.TbtnLineV.Text = "垂直线";
            this.TbtnLineV.ToolTipText = "垂直线";
            // 
            // TbtnSum
            // 
            this.TbtnSum.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TbtnSum.Image = ((System.Drawing.Image)(resources.GetObject("TbtnSum.Image")));
            this.TbtnSum.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TbtnSum.Name = "TbtnSum";
            this.TbtnSum.Size = new System.Drawing.Size(36, 22);
            this.TbtnSum.Text = "合计";
            this.TbtnSum.ToolTipText = "合计";
            // 
            // TbtnPageIndex
            // 
            this.TbtnPageIndex.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TbtnPageIndex.Image = ((System.Drawing.Image)(resources.GetObject("TbtnPageIndex.Image")));
            this.TbtnPageIndex.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TbtnPageIndex.Name = "TbtnPageIndex";
            this.TbtnPageIndex.Size = new System.Drawing.Size(36, 22);
            this.TbtnPageIndex.Text = "页码";
            // 
            // TbtnPrintTime
            // 
            this.TbtnPrintTime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TbtnPrintTime.Image = ((System.Drawing.Image)(resources.GetObject("TbtnPrintTime.Image")));
            this.TbtnPrintTime.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TbtnPrintTime.Name = "TbtnPrintTime";
            this.TbtnPrintTime.Size = new System.Drawing.Size(60, 22);
            this.TbtnPrintTime.Text = "打印时间";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // fieldList1
            // 
            this.fieldList1.cur_obj = null;
            this.fieldList1.Dock = System.Windows.Forms.DockStyle.Left;
            this.fieldList1.Location = new System.Drawing.Point(0, 75);
            this.fieldList1.Name = "fieldList1";
            this.fieldList1.Size = new System.Drawing.Size(75, 435);
            this.fieldList1.TabIndex = 5;
            this.fieldList1.Text = "fieldList1";
            this.fieldList1.ItemClick += new System.EventHandler(this.fieldList1_ItemClick);
            // 
            // tsbExport
            // 
            this.tsbExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExport.Image = ((System.Drawing.Image)(resources.GetObject("tsbExport.Image")));
            this.tsbExport.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.Size = new System.Drawing.Size(23, 22);
            this.tsbExport.Text = "保存";
            this.tsbExport.Click += new System.EventHandler(this.tsmiExportStyle_Click);
            // 
            // tsbImport
            // 
            this.tsbImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbImport.Image = ((System.Drawing.Image)(resources.GetObject("tsbImport.Image")));
            this.tsbImport.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tsbImport.Name = "tsbImport";
            this.tsbImport.Size = new System.Drawing.Size(23, 22);
            this.tsbImport.Text = "保存";
            this.tsbImport.Click += new System.EventHandler(this.tsmiImportStyle_Click);
            // 
            // PrintD
            // 
            this.ClientSize = new System.Drawing.Size(1008, 510);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.fieldList1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PrintD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打印格式设计";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PrintD_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PrintD_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PrintD_KeyPress);
            this.panel2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private System.Drawing.Point p = new System.Drawing.Point(0, 0);
        private System.Drawing.Point p2 = new System.Drawing.Point(0, 0);
        private void pnl_MouseClick(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            if (e.Button == MouseButtons.Left)
            {
                if (TbtnPointer.Checked == true)
                {

                }
                else if (TbtnTextBox.Checked == true)
                {
                    cons.IPrintObject ins = new cons.PrintObject1();
                    ins.SetSelectControl(this);
                    cons.ISizeable sizeable = (cons.ISizeable)ins;
                    cons.IContextable contextable = (cons.IContextable)ins;
                    sizeable.Location = p;
                    contextable.Context = "普通文本";
                    ins.Show(pnl);
                    Record();
                }
                else if (TbtnPicBox.Checked == true)
                {
                    cons.IPrintObject ins = new cons.PrintObject6();
                    ins.SetSelectControl(this);
                    cons.ISizeable sizeable = (cons.ISizeable)ins;
                    sizeable.Location = p;
                    ins.Show(pnl);
                    Record();
                }
                else if (TbtnLineH.Checked == true)
                {
                    cons.IPrintObject ins = new cons.PrintObject5();
                    ins.SetSelectControl(this);
                    cons.ISizeable sizeable = (cons.ISizeable)ins;
                    sizeable.Location = p;
                    ins.Show(pnl);
                    Record();
                }
                else if (TbtnLineV.Checked == true)
                {
                    cons.IPrintObject ins = new cons.PrintObject4();
                    ins.SetSelectControl(this);
                    cons.ISizeable sizeable = (cons.ISizeable)ins;
                    sizeable.Location = p;
                    ins.Show(pnl);
                    Record();
                }
                else if (TbtnSum.Checked == true)
                {
                    cons.IChangeField chg = new cons.ChangeField();
                    List<string> lst = new List<string>();
                    if (tbdetail != null)
                    {
                        foreach (System.Data.DataColumn col in tbdetail.Columns)
                        {
                            lst.Add(col.ColumnName);
                        }
                        string def = "";
                        foreach (ToolStripButton item in toolStrip2.Items)
                        {
                            item.Checked = false;
                        }
                        fieldList1.cur_obj = null;
                        TbtnPointer.Checked = true;
                        if (chg.Change(lst.ToArray(), def, out def) == true)
                        {
                            var con = new cons.PrintObject3();
                            cons.IPrintObject ins = con;
                            ins.SetSelectControl(this);
                            cons.ISizeable sizeable = (cons.ISizeable)ins;
                            sizeable.Location = p;
                            con.Field = def;

                            ins.Show(pnl);
                            Record();
                        }
                    }
                    else
                    {

                    }

                }
                else if (TbtnPageIndex.Checked == true)
                {
                    cons.IPrintObject ins = new cons.PrintObject7();
                    ins.SetSelectControl(this);
                    cons.ISizeable sizeable = (cons.ISizeable)ins;
                    sizeable.Location = p;
                    ins.Show(pnl);
                    Record();
                }
                else if (TbtnPrintTime.Checked == true)
                {
                    cons.IPrintObject ins = new cons.PrintObject8();
                    ins.SetSelectControl(this);
                    cons.ISizeable sizeable = (cons.ISizeable)ins;
                    sizeable.Location = p;
                    ins.Show(pnl);
                    Record();
                }
                else
                {
                    if (fieldList1.cur_obj != null)
                    {

                        string field = fieldList1.cur_obj.column_name;
                        cons.IPrintObject ins = new cons.PrintObject2();
                        ins.SetSelectControl(this);
                        cons.ISizeable sizeable = (cons.ISizeable)ins;
                        sizeable.Location = this.p;
                        cons.IFieldAble fieldable = (cons.IFieldAble)ins;
                        fieldable.Field = field;
                        ins.Show(pnl);

                    }
                }

            }
            //
            foreach (ToolStripButton item in toolStrip2.Items)
            {
                item.Checked = false;
            }
            fieldList1.cur_obj = null;
            TbtnPointer.Checked = true;
        }



        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            this.contextMenuStrip1.Visible = false;

            try
            {
                if (e.ClickedItem.Text == "剪切")
                {
                    int flag = 0;
                    lstcopy.Clear();

                    List<cons.IPrintObject> lst = new List<cons.IPrintObject>();
                    foreach (cons.IPrintObject ins in SelectObjects)
                    {
                        lst.Add(ins);
                        lstcopy.Add(ins.Copy());
                    }
                    foreach (cons.IPrintObject ins in lst)
                    {
                        if (ins.GetType().GetInterface(typeof(cons.IDeleteable).ToString()) != null)
                        {
                            cons.IDeleteable del = (cons.IDeleteable)ins;
                            del.Delete(pnl);
                            flag = 1;
                        }
                    }
                    if (flag == 1)
                    {
                        Record();
                    }
                }
                else if (e.ClickedItem.Text == "复制")
                {

                    lstcopy.Clear();

                    foreach (cons.IPrintObject ins in SelectObjects)
                    {

                        lstcopy.Add(ins.Copy());
                    }
                }
                else if (e.ClickedItem.Text == "粘贴")
                {

                    Point p = (Point)this.contextMenuStrip1.Tag;

                    int x1 = 9999;
                    int y1 = 9999;
                    int x2 = -9999;
                    int y2 = -9999;
                    foreach (cons.IPrintObject ins in lstcopy)
                    {

                        Control con = (Control)ins;
                        if (x1 > con.Left)
                        {
                            x1 = con.Left;
                        }
                        if (y1 > con.Top)
                        {
                            y1 = con.Top;
                        }
                        if (x2 < con.Left + con.Width)
                        {
                            x2 = con.Left + con.Width;
                        }
                        if (y2 < con.Top + con.Height)
                        {
                            y2 = con.Top + con.Height;
                        }

                    }
                    int w = x2 - x1;
                    int h = y2 - y1;
                    int x = p.X;
                    int y = p.Y;
                    int offset_x = x - x1;
                    int offset_y = y - y1;
                    //
                    int flag = 0;
                    List<cons.IPrintObject> lst = new List<cons.IPrintObject>();
                    foreach (cons.IPrintObject ins in lstcopy)
                    {
                        cons.IPrintObject item = ins.Copy();
                        item.SetSelectControl(this);
                        Control con = (Control)item;
                        con.Left += offset_x;
                        con.Top += offset_y;
                        item.Show(pnl);
                        con.BringToFront();
                        lst.Add(item);
                        flag = 1;
                    }
                    if (flag == 1)
                    {
                        List<cons.IPrintObject> lsttemp = new List<cons.IPrintObject>();
                        foreach (cons.IPrintObject ins in SelectObjects)
                        {
                            lsttemp.Add(ins);
                        }
                        foreach (cons.IPrintObject ins in lsttemp)
                        {
                            ins.Selected = false;
                        }
                        SelectObjects.Clear();
                        foreach (cons.IPrintObject ins in lst)
                        {
                            ins.Selected = true;
                        }

                        Record();
                    }
                }
                else if (e.ClickedItem.Text == "删除")
                {
                    int flag = 0;
                    List<cons.IPrintObject> lst = new List<cons.IPrintObject>();
                    foreach (cons.IPrintObject ins in SelectObjects)
                    {
                        lst.Add(ins);
                    }
                    foreach (cons.IPrintObject ins in lst)
                    {
                        if (ins.GetType().GetInterface(typeof(cons.IDeleteable).ToString()) != null)
                        {
                            cons.IDeleteable del = (cons.IDeleteable)ins;
                            del.Delete(pnl);
                            flag = 1;
                        }
                    }
                    if (flag == 1)
                    {
                        Record();
                    }
                }
                else if (e.ClickedItem.Text == "文本内容")
                {
                    cons.IInput input = new cons.InputString();
                    string def = "";
                    if (SelectObjects.Count == 1)
                    {
                        cons.IContextable contextable = (cons.IContextable)SelectObjects[0];
                        def = contextable.Context;
                    }
                    if (input.Input(def, out def) == true)
                    {
                        foreach (cons.IPrintObject ins in SelectObjects)
                        {
                            cons.IContextable contextable = (cons.IContextable)ins;
                            contextable.Context = def;
                        }
                        Record();
                    }

                }
                else if (e.ClickedItem.Text == "文本对齐")
                {
                    cons.ISelectContextAlign align = new cons.SelectContextAlign();
                    int def = 0;
                    if (SelectObjects.Count == 1)
                    {
                        cons.IContextAlignAble contextalignable = (cons.IContextAlignAble)SelectObjects[0];
                        def = contextalignable.Align;
                    }
                    if (align.Select(def, out def) == true)
                    {
                        foreach (cons.IPrintObject ins in SelectObjects)
                        {
                            cons.IContextAlignAble contextalignable = (cons.IContextAlignAble)ins;
                            contextalignable.Align = def;
                        }
                        Record();
                    }
                }
                else if (e.ClickedItem.Text == "字体")
                {
                    FontDialog f = new FontDialog();

                    if (SelectObjects.Count == 1)
                    {
                        cons.IFontable fontable = (cons.IFontable)SelectObjects[0];
                        f.Font = fontable.Font;
                    }
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        foreach (cons.IPrintObject ins in SelectObjects)
                        {
                            cons.IFontable fontable = (cons.IFontable)ins;
                            fontable.Font = f.Font;
                        }
                        Record();
                    }
                }
                else if (e.ClickedItem.Text == "边框")
                {
                    cons.IEditBorder border = new cons.EditBorder();
                    int borderLeft = 0;
                    int borderRight = 0;
                    int borderTop = 0;
                    int borderBottom = 0;
                    if (SelectObjects.Count == 1)
                    {
                        cons.IBorderable borderable = (cons.IBorderable)SelectObjects[0];
                        borderLeft = borderable.BorderLeft;
                        borderRight = borderable.BorderRight;
                        borderTop = borderable.BorderTop;
                        borderBottom = borderable.BorderBottom;
                    }
                    if (border.EditBorder(borderLeft, borderRight, borderTop, borderBottom,
                        out borderLeft, out borderRight, out borderTop, out borderBottom) == true)
                    {
                        foreach (cons.IPrintObject ins in SelectObjects)
                        {
                            cons.IBorderable borderable = (cons.IBorderable)ins;
                            borderable.BorderLeft = borderLeft;
                            borderable.BorderRight = borderRight;
                            borderable.BorderTop = borderTop;
                            borderable.BorderBottom = borderBottom;
                        }
                        Record();
                    }
                }
                else if (e.ClickedItem.Text == "颜色")
                {
                    ColorDialog f = new ColorDialog();
                    Color def = Color.Black;
                    if (SelectObjects.Count == 1)
                    {
                        cons.IColorable colorable = (cons.IColorable)SelectObjects[0];
                        def = colorable.Color;
                    }
                    f.Color = def;
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        foreach (cons.IPrintObject ins in SelectObjects)
                        {
                            cons.IColorable colorable = (cons.IColorable)ins;
                            colorable.Color = f.Color;
                        }
                        Record();
                    }

                }
                else if (e.ClickedItem.Text == "左对齐")
                {
                    int flag = 0;
                    cons.ISizeable sizeable2 = (cons.ISizeable)_fistSelectObject;
                    foreach (cons.IPrintObject ins in SelectObjects)
                    {
                        cons.ISizeable sizeable = (cons.ISizeable)ins;
                        if (sizeable.Location != new Point(sizeable2.Location.X, sizeable.Location.Y))
                        {
                            flag = 1;
                        }
                        sizeable.Location = new Point(sizeable2.Location.X, sizeable.Location.Y);
                    }
                    if (flag == 1)
                    {
                        Record();
                    }

                }
                else if (e.ClickedItem.Text == "右对齐")
                {
                    int flag = 0;
                    cons.ISizeable sizeable2 = (cons.ISizeable)_fistSelectObject;
                    foreach (cons.IPrintObject ins in SelectObjects)
                    {
                        cons.ISizeable sizeable = (cons.ISizeable)ins;
                        if (sizeable.Location != new Point((sizeable2.Location.X + sizeable2.Size.Width) - sizeable.Size.Width, sizeable.Location.Y))
                        {
                            flag = 1;
                        }
                        sizeable.Location = new Point((sizeable2.Location.X + sizeable2.Size.Width) - sizeable.Size.Width, sizeable.Location.Y);
                    }
                    if (flag == 1)
                    {
                        Record();
                    }

                }
                else if (e.ClickedItem.Text == "上对齐")
                {
                    int flag = 0;
                    cons.ISizeable sizeable2 = (cons.ISizeable)_fistSelectObject;
                    foreach (cons.IPrintObject ins in SelectObjects)
                    {
                        cons.ISizeable sizeable = (cons.ISizeable)ins;
                        if (sizeable.Location != new Point(sizeable.Location.X, sizeable2.Location.Y))
                        {
                            flag = 1;
                        }
                        sizeable.Location = new Point(sizeable.Location.X, sizeable2.Location.Y);
                    }
                    if (flag == 1)
                    {
                        Record();
                    }

                }
                else if (e.ClickedItem.Text == "下对齐")
                {
                    int flag = 0;
                    cons.ISizeable sizeable2 = (cons.ISizeable)_fistSelectObject;
                    foreach (cons.IPrintObject ins in SelectObjects)
                    {
                        cons.ISizeable sizeable = (cons.ISizeable)ins;
                        if (sizeable.Location != new Point(sizeable.Location.X, (sizeable2.Location.Y + sizeable2.Size.Height) - sizeable.Size.Height))
                        {
                            flag = 1;
                        }
                        sizeable.Location = new Point(sizeable.Location.X, (sizeable2.Location.Y + sizeable2.Size.Height) - sizeable.Size.Height);
                    }
                    if (flag == 1)
                    {
                        Record();
                    }

                }
                else if (e.ClickedItem.Text == "导入图片")
                {
                    OpenFileDialog f = new OpenFileDialog();
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        var fileInfo = new System.IO.FileInfo(f.FileName);
                        decimal len = Helper.Conv.ToDecimal(fileInfo.Length);
                        len = len / 1024;
                        if (len > 500)
                        {
                            throw new Exception("图片文件大于500K");
                        }
                        var img = Image.FromFile(f.FileName);
                        cons.IImageAble imageable = (cons.IImageAble)_fistSelectObject;
                        imageable.Image = img;
                        Record();
                    }
                }
                else if (e.ClickedItem.Text == "导出图片")
                {
                    cons.IImageAble imageable = (cons.IImageAble)_fistSelectObject;
                    if (imageable.Image == null)
                    {
                        throw new Exception("无可导出的图片");
                    }
                    else
                    {
                        SaveFileDialog f = new SaveFileDialog();
                        f.Filter = "*.jpg|*.jpg";
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            imageable.Image.Save(f.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                    }

                }
                else if (e.ClickedItem.Text == "属性")
                {
                    System.Windows.Forms.MessageBox.Show(_fistSelectObject.propertyInfo, "属性");
                }
                else if (e.ClickedItem.Text == "格式化")
                {
                    cons.IInput input = new cons.InputFormatString();
                    string def = "";
                    if (SelectObjects.Count == 1)
                    {
                        cons.IFormatable formatable = (cons.IFormatable)SelectObjects[0];
                        def = formatable.Format;
                    }
                    if (input.Input(def, out def) == true)
                    {
                        foreach (cons.IPrintObject ins in SelectObjects)
                        {
                            cons.IFormatable formatable = (cons.IFormatable)ins;
                            formatable.Format = def;
                        }
                        Record();
                    }
                }
                else if (e.ClickedItem.Text == "改字段")
                {

                    string def = "";
                    if (SelectObjects.Count == 1)
                    {
                        cons.IFieldAble fieldable = (cons.IFieldAble)SelectObjects[0];
                        def = fieldable.Field;
                    }
                    cons.IChangeField chg = new cons.ChangeField();
                    List<string> lst = new List<string>();
                    if (tbmain != null)
                    {
                        foreach (System.Data.DataColumn col in tbmain.Columns)
                        {
                            lst.Add(col.ColumnName);
                        }
                    }
                    if (chg.Change(lst.ToArray(), def, out def) == true)
                    {
                        foreach (cons.IPrintObject ins in SelectObjects)
                        {
                            cons.IFieldAble fieldable = (cons.IFieldAble)ins;
                            fieldable.Field = def;
                        }
                        Record();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }



        }


        private void pnl_MouseDown(object sender, MouseEventArgs e)
        {
            pnl.Focus();

            this.p = new System.Drawing.Point(e.X, e.Y);
            this.p2 = new System.Drawing.Point(e.X, e.Y);
        }


        private Rectangle rec = new Rectangle(0, 0, 0, 0);

        private void pnl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (pnl.reject_select == 0)
                {
                    this.p2 = new System.Drawing.Point(e.X, e.Y);
                    var rec2 = Helper.PrintObjectHelper.CreateRectangle(p, p2);
                    ControlPaint.DrawReversibleFrame(rec, Color.White, FrameStyle.Dashed);
                    rec = pnl.RectangleToScreen(rec2);
                    ControlPaint.DrawReversibleFrame(rec, Color.White, FrameStyle.Dashed);
                }

            }
        }



        List<cons.IPrintObject> cons.IDesign.GetSelectObjects()
        {
            return SelectObjects;
        }

        private List<cons.IPrintObject> SelectObjects = new List<cons.IPrintObject>();
        private void pnl_MouseUp(object sender, MouseEventArgs e)
        {


            //
            if (e.Button == MouseButtons.Right)
            {


                foreach (Control con in pnl.Controls)
                {
                    cons.IPrintObject ins = (cons.IPrintObject)con;
                    ins.Selected = false;
                }
                SelectObjects = new List<cons.IPrintObject>();
                if (SelectObjects.Count == 0)
                {
                    _fistSelectObject = null;
                }
                pnl.Refresh();

                IniMenu();
                this.contextMenuStrip1.Tag = new Point(e.X, e.Y);
                this.contextMenuStrip1.Show(pnl, e.X, e.Y);
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (1 == 1)
                {
                    ControlPaint.DrawReversibleFrame(rec, Color.White, FrameStyle.Dashed);
                    rec = new Rectangle(0, 0, 0, 0);
                    //
                    if (System.Windows.Forms.Control.ModifierKeys != Keys.Control)
                    {
                        foreach (Control con in pnl.Controls)
                        {
                            cons.IPrintObject ins = (cons.IPrintObject)con;
                            ins.Selected = false;
                        }
                    }

                    var rec2 = Helper.PrintObjectHelper.CreateRectangle(p, p2);
                    foreach (Control con in pnl.Controls)
                    {
                        var rec3 = new Rectangle(con.Left, con.Top, con.Width, con.Height);
                        if (Helper.PrintObjectHelper.RectangleInRectangle(rec3, rec2) == true)
                        {
                            cons.IPrintObject ins = (cons.IPrintObject)con;
                            ins.Selected = true;

                        }
                    }
                    if (SelectObjects.Count == 0)
                    {
                        _fistSelectObject = null;
                    }
                    pnl.Refresh();
                }

            }
        }



        void cons.IDesign.Add(cons.IPrintObject ins)
        {
            if (SelectObjects.Contains(ins) == false)
            {
                SelectObjects.Add(ins);
            }
        }

        void cons.IDesign.Remove(cons.IPrintObject ins)
        {
            if (SelectObjects.Contains(ins) == true)
            {
                SelectObjects.Remove(ins);
            }
            if (SelectObjects.Count == 0)
            {
                _fistSelectObject = null;
            }
        }



        void cons.IDesign.RemoveAll()
        {
            foreach (Control con in pnl.Controls)
            {
                cons.IPrintObject ins2 = (cons.IPrintObject)con;
                ins2.Selected = false;
            }
            _fistSelectObject = null;
        }



        int cons.IDesign.Count
        {
            get { return SelectObjects.Count; }
        }

        cons.IPrintObject _fistSelectObject = null;
        cons.IPrintObject cons.IDesign.FirstSelectObject
        {
            get
            {
                return _fistSelectObject;
            }
            set
            {
                if (_fistSelectObject != null)
                {
                    _fistSelectObject.FirstSelected = false;
                }
                _fistSelectObject = value;
                if (_fistSelectObject != null)
                {
                    _fistSelectObject.FirstSelected = true;
                    var con = (Control)_fistSelectObject;
                    con.BringToFront();
                }
            }
        }


        void cons.IDesign.OffSetX(int inte)
        {

            foreach (cons.IPrintObject ins in SelectObjects)
            {
                cons.ISizeable sizeable = (cons.ISizeable)ins;
                sizeable.Location = new Point(sizeable.Location.X + inte, sizeable.Location.Y);
            }

        }

        void cons.IDesign.OffSetY(int inte)
        {

            foreach (cons.IPrintObject ins in SelectObjects)
            {
                cons.ISizeable sizeable = (cons.ISizeable)ins;
                sizeable.Location = new Point(sizeable.Location.X, sizeable.Location.Y + inte);

            }

        }

        void cons.IDesign.OffSetWidth(int inte)
        {

            foreach (cons.IPrintObject ins in SelectObjects)
            {
                cons.ISizeable sizeable = (cons.ISizeable)ins;
                sizeable.Size = new Size(sizeable.Size.Width + inte, sizeable.Size.Height);

            }

        }

        void cons.IDesign.OffSetHeight(int inte)
        {

            foreach (cons.IPrintObject ins in SelectObjects)
            {
                cons.ISizeable sizeable = (cons.ISizeable)ins;
                sizeable.Size = new Size(sizeable.Size.Width, sizeable.Size.Height + inte);

            }

        }

        List<cons.IPrintObject> lstcopy = new List<cons.IPrintObject>();

        private void PrintD_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Shift == true)
            {
                if (e.KeyCode == Keys.Up)
                {
                    int flag = 0;
                    foreach (cons.IPrintObject ins in SelectObjects)
                    {
                        cons.ISizeable sizeable = (cons.ISizeable)ins;
                        sizeable.Size = new Size(sizeable.Size.Width, sizeable.Size.Height - 1);
                        flag = 1;
                    }
                    if (flag == 1)
                    {
                        Record();
                    }
                }
                else if (e.KeyCode == Keys.Down)
                {
                    int flag = 0;
                    foreach (cons.IPrintObject ins in SelectObjects)
                    {
                        cons.ISizeable sizeable = (cons.ISizeable)ins;
                        sizeable.Size = new Size(sizeable.Size.Width, sizeable.Size.Height + 1);
                        flag = 1;
                    }
                    if (flag == 1)
                    {
                        Record();
                    }
                }
                else if (e.KeyCode == Keys.Left)
                {
                    int flag = 0;
                    foreach (cons.IPrintObject ins in SelectObjects)
                    {
                        cons.ISizeable sizeable = (cons.ISizeable)ins;
                        sizeable.Size = new Size(sizeable.Size.Width - 1, sizeable.Size.Height);
                        flag = 1;
                    }
                    if (flag == 1)
                    {
                        Record();
                    }
                }
                else if (e.KeyCode == Keys.Right)
                {
                    int flag = 0;
                    foreach (cons.IPrintObject ins in SelectObjects)
                    {
                        cons.ISizeable sizeable = (cons.ISizeable)ins;
                        sizeable.Size = new Size(sizeable.Size.Width + 1, sizeable.Size.Height);
                        flag = 1;
                    }
                    if (flag == 1)
                    {
                        Record();
                    }
                }
                if (e.Control == true && e.KeyCode == Keys.Z)
                {
                    if (operRecord.Next == null)
                    {

                    }
                    else
                    {
                        operRecord.Next.Undo(des);
                        operRecord = operRecord.Next;
                    }
                }
            }
            else if (e.Control == true)
            {
                if (e.KeyCode == Keys.A)
                {
                    foreach (cons.IPrintObject ins in pnl.Controls)
                    {
                        ins.Selected = true;
                    }
                }
                if (e.KeyCode == Keys.X)
                {
                    int flag = 0;
                    lstcopy.Clear();

                    List<cons.IPrintObject> lst = new List<cons.IPrintObject>();
                    foreach (cons.IPrintObject ins in SelectObjects)
                    {
                        lst.Add(ins);
                        lstcopy.Add(ins.Copy());
                    }
                    foreach (cons.IPrintObject ins in lst)
                    {
                        if (ins.GetType().GetInterface(typeof(cons.IDeleteable).ToString()) != null)
                        {
                            cons.IDeleteable del = (cons.IDeleteable)ins;
                            del.Delete(pnl);
                            flag = 1;
                        }
                    }
                    if (flag == 1)
                    {
                        Record();
                    }
                }
                if (e.KeyCode == Keys.C)
                {

                    lstcopy.Clear();

                    foreach (cons.IPrintObject ins in SelectObjects)
                    {

                        lstcopy.Add(ins.Copy());
                    }

                }
                if (e.KeyCode == Keys.V)
                {
                    KeysV();

                }
                if (e.KeyCode == Keys.S)
                {
                    try
                    {
                        cons.IDesign des = this;
                        string xml = des.xml;
                        //
                        if (_SaveStyle != null)
                        {
                            _SaveStyle.Invoke(style_id, xml);
                        }
                        this.xml = xml;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                if (e.KeyCode == Keys.Z)
                {

                    if (operRecord.Pre == null)
                    {

                    }
                    else
                    {

                        operRecord.Pre.Undo(des);
                        operRecord = operRecord.Pre;
                    }
                }
            }
            else
            {

                if (e.KeyCode == Keys.Up)
                {
                    int flag = 0;
                    foreach (cons.IPrintObject ins in SelectObjects)
                    {
                        cons.ISizeable sizeable = (cons.ISizeable)ins;
                        sizeable.Location = new Point(sizeable.Location.X, sizeable.Location.Y - 1);
                        flag = 1;
                    }
                    if (flag == 1)
                    {
                        Record();
                    }
                }
                else if (e.KeyCode == Keys.Down)
                {
                    int flag = 0;
                    foreach (cons.IPrintObject ins in SelectObjects)
                    {
                        cons.ISizeable sizeable = (cons.ISizeable)ins;
                        sizeable.Location = new Point(sizeable.Location.X, sizeable.Location.Y + 1);
                        flag = 1;
                    }
                    if (flag == 1)
                    {
                        Record();
                    }
                }
                else if (e.KeyCode == Keys.Left)
                {
                    int flag = 0;
                    foreach (cons.IPrintObject ins in SelectObjects)
                    {
                        cons.ISizeable sizeable = (cons.ISizeable)ins;
                        sizeable.Location = new Point(sizeable.Location.X - 1, sizeable.Location.Y);
                        flag = 1;
                    }
                    if (flag == 1)
                    {
                        Record();
                    }
                }
                else if (e.KeyCode == Keys.Right)
                {
                    int flag = 0;
                    foreach (cons.IPrintObject ins in SelectObjects)
                    {
                        cons.ISizeable sizeable = (cons.ISizeable)ins;
                        sizeable.Location = new Point(sizeable.Location.X + 1, sizeable.Location.Y);
                        flag = 1;
                    }
                    if (flag == 1)
                    {
                        Record();
                    }
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    int flag = 0;
                    List<cons.IPrintObject> lst = new List<cons.IPrintObject>();
                    foreach (cons.IPrintObject ins in SelectObjects)
                    {
                        lst.Add(ins);
                    }
                    foreach (cons.IPrintObject ins in lst)
                    {
                        if (ins.GetType().GetInterface(typeof(cons.IDeleteable).ToString()) != null)
                        {
                            cons.IDeleteable del = (cons.IDeleteable)ins;
                            del.Delete(pnl);
                            flag = 1;
                        }
                    }
                    if (flag == 1)
                    {
                        Record();
                    }
                }
            }
        }





        void cons.IDesign.ShowMenu(Point scrPoint)
        {
            IniMenu();
            //
            var p = pnl.PointToClient(scrPoint);
            this.contextMenuStrip1.Tag = new Point(p.X, p.Y);
            this.contextMenuStrip1.Show(pnl, p.X, p.Y);
        }

        private void IniMenu()
        {
            var ts = Helper.PrintObjectHelper.GetTypes(SelectObjects.ToArray());
            List<object> lst = new List<object>();
            lst.Add(this.剪切ToolStripMenuItem1);
            lst.Add(this.复制ToolStripMenuItem1);
            lst.Add(this.粘贴ToolStripMenuItem1);
            lst.Add(this.删除ToolStripMenuItem1);
            lst.Add(this.分隔toolStripMenuItem);
            List<object> lstdel = new List<object>();
            foreach (ToolStripItem item in this.contextMenuStrip1.Items)
            {
                if (lst.Contains(item) == false)
                {
                    lstdel.Add(item);
                }
            }
            foreach (object obj in lstdel)
            {
                this.contextMenuStrip1.Items.Remove((ToolStripItem)obj);
            }

            foreach (Type t in ts)
            {
                if (t == typeof(cons.IColorable))
                {
                    this.contextMenuStrip1.Items.Add("颜色");
                }
                else if (t == typeof(cons.IContextable))
                {
                    this.contextMenuStrip1.Items.Add("文本内容");
                }
                else if (t == typeof(cons.IContextAlignAble))
                {
                    this.contextMenuStrip1.Items.Add("文本对齐");
                }
                else if (t == typeof(cons.IFontable))
                {
                    this.contextMenuStrip1.Items.Add("字体");
                }
                else if (t == typeof(cons.IBorderable))
                {
                    this.contextMenuStrip1.Items.Add("边框");
                }
                else if (t == typeof(cons.IFormatable))
                {
                    this.contextMenuStrip1.Items.Add("格式化");
                }
                else if (t == typeof(cons.IImageAble))
                {
                    this.contextMenuStrip1.Items.Add("导入图片");
                    this.contextMenuStrip1.Items.Add("导出图片");
                }
                else if (t == typeof(cons.IFieldAble))
                {
                    this.contextMenuStrip1.Items.Add("改字段");
                }
                else if (t == typeof(cons.ISizeable))
                {
                    this.contextMenuStrip1.Items.Add("左对齐");
                    this.contextMenuStrip1.Items.Add("右对齐");
                    this.contextMenuStrip1.Items.Add("上对齐");
                    this.contextMenuStrip1.Items.Add("下对齐");
                }
                else if (t == typeof(cons.IPrintObject))
                {
                    this.contextMenuStrip1.Items.Add("属性");
                }
            }
        }

        private void PrintD_Load(object sender, EventArgs e)
        {
            pnl.Size = new Size(3000, 3000);
        }

        private void PrintD_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            return false;

        }

        private void pnl_Paint(object sender, PaintEventArgs e)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            //e.Graphics.DrawString("右键添加项", pnl.Font, new SolidBrush(Color.Gray), new Rectangle(0, 0, pnl.Width, pnl.Height), sf);
        }



        string cons.IDesign.xml
        {
            get
            {
                Helper.StringBuilderForXML sb = new Helper.StringBuilderForXML();
                string str = "";

                sb.Append(pnl.GetXML());
                //
                foreach (cons.IPrintObject ins in pnl.Controls)
                {
                    cons.IChangeAreaAble area = (cons.IChangeAreaAble)ins;
                    System.Windows.Forms.Control con = (System.Windows.Forms.Control)ins;
                    area.Area = pnl.Area(con);
                    sb.Append(ins.xml);
                }

                //
                str = sb.ToString();
                sb.Clear();
                sb.Append("xml", str);
                return sb.ToString();
            }
            set
            {

                List<cons.IPrintObject> lst = new List<cons.IPrintObject>();
                foreach (cons.IPrintObject ins in pnl.Controls)
                {
                    lst.Add(ins);
                }
                foreach (cons.IPrintObject ins in lst)
                {
                    ins.Selected = false;
                    if (ins.GetType().GetInterface(typeof(cons.IDeleteable).ToString()) != null)
                    {
                        cons.IDeleteable del = (cons.IDeleteable)ins;
                        del.Delete(pnl);
                    }
                }

                //
                Helper.ReadXml r = new Helper.ReadXml(value);
                pnl.Controls.Clear();
                pnl.SetXML(r.Read("Page"));

                pnl.Refresh();
                //
                foreach (Helper.ReadXml r2 in r.ReadList("PrintObject1"))
                {
                    cons.IPrintObject ins = new cons.PrintObject1();
                    ins.SetSelectControl(this);
                    ins.xml = r2.Context;
                    ins.Show(pnl);
                }
                //
                foreach (Helper.ReadXml r2 in r.ReadList("PrintObject2"))
                {
                    cons.IPrintObject ins = new cons.PrintObject2();
                    ins.SetSelectControl(this);
                    ins.xml = r2.Context;
                    ins.Show(pnl);
                }
                //
                foreach (Helper.ReadXml r2 in r.ReadList("PrintObject3"))
                {
                    cons.IPrintObject ins = new cons.PrintObject3();
                    ins.SetSelectControl(this);
                    ins.xml = r2.Context;
                    ins.Show(pnl);
                }
                //
                foreach (Helper.ReadXml r2 in r.ReadList("PrintObject4"))
                {
                    cons.IPrintObject ins = new cons.PrintObject4();
                    ins.SetSelectControl(this);
                    ins.xml = r2.Context;
                    ins.Show(pnl);
                }
                //
                foreach (Helper.ReadXml r2 in r.ReadList("PrintObject5"))
                {
                    cons.IPrintObject ins = new cons.PrintObject5();
                    ins.SetSelectControl(this);
                    ins.xml = r2.Context;
                    ins.Show(pnl);
                }
                //
                foreach (Helper.ReadXml r2 in r.ReadList("PrintObject6"))
                {
                    cons.IPrintObject ins = new cons.PrintObject6();
                    ins.SetSelectControl(this);
                    ins.xml = r2.Context;
                    ins.Show(pnl);
                }
                //
                foreach (Helper.ReadXml r2 in r.ReadList("PrintObject7"))
                {
                    cons.IPrintObject ins = new cons.PrintObject7();
                    ins.SetSelectControl(this);
                    ins.xml = r2.Context;
                    ins.Show(pnl);
                }
                //
                foreach (Helper.ReadXml r2 in r.ReadList("PrintObject8"))
                {
                    cons.IPrintObject ins = new cons.PrintObject8();
                    ins.SetSelectControl(this);
                    ins.xml = r2.Context;
                    ins.Show(pnl);
                }
                //
            }
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            foreach (ToolStripButton item in toolStrip2.Items)
            {
                item.Checked = false;
            }
            ToolStripButton it = (ToolStripButton)e.ClickedItem;
            it.Checked = true;
            //
            fieldList1.cur_obj = null;
        }

        private void fieldList1_ItemClick(object sender, EventArgs e)
        {
            foreach (ToolStripButton item in toolStrip2.Items)
            {
                item.Checked = false;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                //
                if (pnl.PageHeight < pnl.AHeight + pnl.BHeight + pnl.CHeight * 3 + pnl.DHeight + pnl.EHeight)
                {
                    throw new Exception("纸张容纳不下当前内容!");
                }
                //
                cons.IDesign des = this;
                string xml = des.xml;
                //

                if (_SaveStyle != null)
                {
                    _SaveStyle.Invoke(style_id, xml);
                }
                this.xml = xml;
                //
                is_edit = false;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                //
                if (pnl.PageHeight < pnl.AHeight + pnl.BHeight + pnl.CHeight * 3 + pnl.DHeight + pnl.EHeight)
                {

                    throw new Exception("纸张容纳不下当前内容!");
                }
                //
                cons.IDesign des = this;
                string xml = des.xml;
                //

                if (_SaveStyle != null)
                {
                    _SaveStyle.Invoke(style_id, xml);
                }

                this.xml = xml;
                //
                is_edit = false;
                //
                IBLL.IPrint ins = new BLL.PrintV();
                ins.Print(style_id, xml, this.tbmain, this.tbdetail);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }


        }

        private void TbtnPrintPage_Click(object sender, EventArgs e)
        {
            cons.IInputSize ins = new cons.InputSizeForPage();
            System.Drawing.Size size = new Size(pnl.PageWidth, pnl.PageHeight);
            if (ins.Input(size, out size) == true)
            {
                pnl.PageWidth = size.Width;
                pnl.PageHeight = size.Height;
                pnl.Refresh();
                Record();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (operRecord.Pre == null)
            {

            }
            else
            {
                operRecord.Pre.Undo(des);
                operRecord = operRecord.Pre;
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (operRecord.Next == null)
            {

            }
            else
            {
                operRecord.Next.Undo(des);
                operRecord = operRecord.Next;
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            int flag = 0;
            lstcopy.Clear();

            List<cons.IPrintObject> lst = new List<cons.IPrintObject>();
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                lst.Add(ins);
                lstcopy.Add(ins.Copy());
            }
            foreach (cons.IPrintObject ins in lst)
            {
                if (ins.GetType().GetInterface(typeof(cons.IDeleteable).ToString()) != null)
                {
                    cons.IDeleteable del = (cons.IDeleteable)ins;
                    del.Delete(pnl);
                    flag = 1;
                }
            }
            if (flag == 1)
            {
                Record();
            }
            //

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //鼠标样式
            if (1 == 1)
            {
                int flag = 0;
                foreach (ToolStripButton item in toolStrip2.Items)
                {
                    if (item.Checked == true && item != TbtnPointer)
                    {
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0)
                {
                    if (fieldList1.cur_obj != null)
                    {
                        flag = 1;
                    }
                }
                if (flag == 0)
                {
                    if (pnl.Cursor == System.Windows.Forms.Cursors.Cross)
                    {
                        pnl.Cursor = System.Windows.Forms.Cursors.Default;
                    }

                }
                else
                {
                    pnl.Cursor = System.Windows.Forms.Cursors.Cross;
                }
            }
            //撤销，恢复
            if (1 == 1)
            {
                if (operRecord == null)
                {
                    toolStripButton3.Enabled = false;
                    toolStripButton4.Enabled = false;
                    撤销ToolStripMenuItem.Enabled = false;
                    恢复ToolStripMenuItem.Enabled = false;
                }
                else
                {
                    if (operRecord.Pre == null)
                    {
                        toolStripButton3.Enabled = false;
                        撤销ToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        toolStripButton3.Enabled = true;
                        撤销ToolStripMenuItem.Enabled = true;
                    }
                    if (operRecord.Next == null)
                    {
                        toolStripButton4.Enabled = false;
                        恢复ToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        toolStripButton4.Enabled = true;
                        恢复ToolStripMenuItem.Enabled = true;
                    }
                }
            }

            if (1 == 1) //剪切，复制，粘贴，删除
            {
                if (SelectObjects.Count == 0)
                {
                    toolStripButton5.Enabled = false;
                    toolStripButton6.Enabled = false;
                    toolStripButton8.Enabled = false;
                    this.剪切ToolStripMenuItem.Enabled = false;
                    this.复制ToolStripMenuItem.Enabled = false;
                    this.删除ToolStripMenuItem.Enabled = false;
                    this.剪切ToolStripMenuItem1.Enabled = false;
                    this.复制ToolStripMenuItem1.Enabled = false;
                    this.删除ToolStripMenuItem1.Enabled = false;

                }
                else
                {
                    toolStripButton5.Enabled = true;
                    toolStripButton6.Enabled = true;
                    toolStripButton8.Enabled = true;
                    this.剪切ToolStripMenuItem.Enabled = true;
                    this.复制ToolStripMenuItem.Enabled = true;
                    this.删除ToolStripMenuItem.Enabled = true;
                    this.剪切ToolStripMenuItem1.Enabled = true;
                    this.复制ToolStripMenuItem1.Enabled = true;
                    this.删除ToolStripMenuItem1.Enabled = true;
                }
                if (lstcopy.Count == 0)
                {
                    toolStripButton7.Enabled = false;
                    this.粘贴ToolStripMenuItem.Enabled = false;
                    this.粘贴ToolStripMenuItem1.Enabled = false;
                }
                else
                {
                    toolStripButton7.Enabled = true;
                    this.粘贴ToolStripMenuItem.Enabled = true;
                    this.粘贴ToolStripMenuItem1.Enabled = true;
                }
            }

            if (toolStripComboBox1.Focused == false)//字号
            {
                int flag = 0;
                int size = 0;
                foreach (cons.IPrintObject ins in SelectObjects)
                {
                    if (ins.GetType().GetInterface(typeof(cons.IFontable).ToString()) != null)
                    {
                        cons.IFontable fontable = (cons.IFontable)ins;
                        if (size == 0)
                        {
                            size = (int)fontable.Font.Size;
                        }
                        else
                        {
                            if (size == (int)fontable.Font.Size)
                            {

                            }
                            else
                            {

                                flag = 1;
                            }
                        }

                    }
                }
                if (flag == 1)
                {
                    this.toolStripComboBox1.Text = "";
                }
                else
                {
                    if (size == 0)
                    {
                        this.toolStripComboBox1.Text = "";
                    }
                    else
                    {
                        this.toolStripComboBox1.Text = size.ToString();
                    }

                }

            }

            if (1 == 1)//粗体
            {
                int flag2 = 0;
                foreach (cons.IPrintObject ins in SelectObjects)
                {
                    if (ins.GetType().GetInterface(typeof(cons.IFontable).ToString()) != null)
                    {
                        cons.IFontable fontable = (cons.IFontable)ins;
                        if (fontable.Font.Bold == true)
                        {
                            if (flag2 == 0)
                            {
                                flag2 = 1;
                            }
                            else if (flag2 == 1)
                            {
                                flag2 = 1;
                            }
                            else
                            {
                                flag2 = 3;
                            }
                        }
                        else
                        {
                            if (flag2 == 0)
                            {
                                flag2 = 2;
                            }
                            else if (flag2 == 1)
                            {
                                flag2 = 3;
                            }
                            else if (flag2 == 2)
                            {
                                flag2 = 2;
                            }
                            else
                            {
                                flag2 = 3;
                            }
                        }

                    }
                }
                if (flag2 == 0)
                {
                    toolStripButton13.Checked = false;
                }
                else if (flag2 == 1)
                {
                    toolStripButton13.Checked = true;
                }
                else if (flag2 == 2)
                {
                    toolStripButton13.Checked = false;
                }
                else
                {
                    toolStripButton13.Checked = false;
                }
            }

            if (1 == 1)//斜体
            {
                int flag2 = 0;
                foreach (cons.IPrintObject ins in SelectObjects)
                {
                    if (ins.GetType().GetInterface(typeof(cons.IFontable).ToString()) != null)
                    {
                        cons.IFontable fontable = (cons.IFontable)ins;
                        if (fontable.Font.Italic == true)
                        {
                            if (flag2 == 0)
                            {
                                flag2 = 1;
                            }
                            else if (flag2 == 1)
                            {
                                flag2 = 1;
                            }
                            else
                            {
                                flag2 = 3;
                            }
                        }
                        else
                        {
                            if (flag2 == 0)
                            {
                                flag2 = 2;
                            }
                            else if (flag2 == 1)
                            {
                                flag2 = 3;
                            }
                            else if (flag2 == 2)
                            {
                                flag2 = 2;
                            }
                            else
                            {
                                flag2 = 3;
                            }
                        }

                    }
                }
                if (flag2 == 0)
                {
                    toolStripButton14.Checked = false;
                }
                else if (flag2 == 1)
                {
                    toolStripButton14.Checked = true;
                }
                else if (flag2 == 2)
                {
                    toolStripButton14.Checked = false;
                }
                else
                {
                    toolStripButton14.Checked = false;
                }
            }

            if (1 == 1)//字体下划线
            {
                int flag2 = 0;
                foreach (cons.IPrintObject ins in SelectObjects)
                {
                    if (ins.GetType().GetInterface(typeof(cons.IFontable).ToString()) != null)
                    {
                        cons.IFontable fontable = (cons.IFontable)ins;
                        if (fontable.Font.Underline == true)
                        {
                            if (flag2 == 0)
                            {
                                flag2 = 1;
                            }
                            else if (flag2 == 1)
                            {
                                flag2 = 1;
                            }
                            else
                            {
                                flag2 = 3;
                            }
                        }
                        else
                        {
                            if (flag2 == 0)
                            {
                                flag2 = 2;
                            }
                            else if (flag2 == 1)
                            {
                                flag2 = 3;
                            }
                            else if (flag2 == 2)
                            {
                                flag2 = 2;
                            }
                            else
                            {
                                flag2 = 3;
                            }
                        }

                    }
                }
                if (flag2 == 0)
                {
                    toolStripButton15.Checked = false;
                }
                else if (flag2 == 1)
                {
                    toolStripButton15.Checked = true;
                }
                else if (flag2 == 2)
                {
                    toolStripButton15.Checked = false;
                }
                else
                {
                    toolStripButton15.Checked = false;
                }
            }

            //文本对齐
            if (1 == 1)
            {
                int align = 0;
                int flag2 = 0;
                foreach (cons.IPrintObject ins in SelectObjects)
                {
                    if (ins.GetType().GetInterface(typeof(cons.IContextAlignAble).ToString()) != null)
                    {
                        cons.IContextAlignAble item = (cons.IContextAlignAble)ins;
                        if (align == 0)
                        {
                            align = item.Align;
                        }
                        else
                        {
                            if (align == item.Align)
                            {

                            }
                            else
                            {

                                flag2 = 1;
                            }
                        }

                    }
                }

                toolStripButton19.Checked = false;
                toolStripButton20.Checked = false;
                toolStripButton21.Checked = false;
                if (flag2 == 0)
                {
                    if (align == 1)
                    {
                        toolStripButton19.Checked = true;
                    }
                    else if (align == 2)
                    {
                        toolStripButton20.Checked = true;
                    }
                    else if (align == 3)
                    {
                        toolStripButton21.Checked = true;
                    }


                }
                else if (flag2 == 1)
                {

                }

            }



        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

            lstcopy.Clear();

            foreach (cons.IPrintObject ins in SelectObjects)
            {

                lstcopy.Add(ins.Copy());
            }

        }

        private void KeysV()
        {
            int x1 = 9999;
            int y1 = 9999;
            int x2 = -9999;
            int y2 = -9999;
            foreach (cons.IPrintObject ins in lstcopy)
            {

                Control con = (Control)ins;
                if (x1 > con.Left)
                {
                    x1 = con.Left;
                }
                if (y1 > con.Top)
                {
                    y1 = con.Top;
                }
                if (x2 < con.Left + con.Width)
                {
                    x2 = con.Left + con.Width;
                }
                if (y2 < con.Top + con.Height)
                {
                    y2 = con.Top + con.Height;
                }

            }
            int w = x2 - x1;
            int h = y2 - y1;
            int x = (pnl.PageWidth - w) / 2;
            int y = (pnl.DesignHeight() - h) / 2;
            int offset_x = x - x1;
            int offset_y = y - y1;
            //
            int flag = 0;
            List<cons.IPrintObject> lst = new List<cons.IPrintObject>();
            foreach (cons.IPrintObject ins in lstcopy)
            {
                cons.IPrintObject item = ins.Copy();
                item.SetSelectControl(this);
                Control con = (Control)item;
                con.Left += offset_x;
                con.Top += offset_y;
                item.Show(pnl);
                con.BringToFront();
                lst.Add(item);
                flag = 1;
            }
            if (flag == 1)
            {
                List<cons.IPrintObject> lsttemp = new List<cons.IPrintObject>();
                foreach (cons.IPrintObject ins in SelectObjects)
                {
                    lsttemp.Add(ins);
                }
                foreach (cons.IPrintObject ins in lsttemp)
                {
                    ins.Selected = false;
                }
                SelectObjects.Clear();
                foreach (cons.IPrintObject ins in lst)
                {
                    ins.Selected = true;
                }

                Record();
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            KeysV();

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            int flag = 0;
            List<cons.IPrintObject> lst = new List<cons.IPrintObject>();
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                lst.Add(ins);
            }
            foreach (cons.IPrintObject ins in lst)
            {
                if (ins.GetType().GetInterface(typeof(cons.IDeleteable).ToString()) != null)
                {
                    cons.IDeleteable del = (cons.IDeleteable)ins;
                    del.Delete(pnl);
                    flag = 1;
                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            foreach (cons.IPrintObject ins in pnl.Controls)
            {
                ins.Selected = true;
            }
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            int flag = 0;
            int flag2 = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IFontable).ToString()) != null)
                {
                    cons.IFontable fontable = (cons.IFontable)ins;
                    if (fontable.Font.Bold == true)
                    {
                        if (flag2 == 0)
                        {
                            flag2 = 1;
                        }
                        else if (flag2 == 1)
                        {
                            flag2 = 1;
                        }
                        else
                        {
                            flag2 = 3;
                        }
                    }
                    else
                    {
                        if (flag2 == 0)
                        {
                            flag2 = 2;
                        }
                        else if (flag2 == 2)
                        {
                            flag2 = 2;
                        }
                        else
                        {
                            flag2 = 3;
                        }
                    }

                }
            }

            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IFontable).ToString()) != null)
                {
                    cons.IFontable fontable = (cons.IFontable)ins;
                    if (flag2 == 1)
                    {
                        int style = 0;
                        if (fontable.Font.Italic == true)
                        {
                            style += (int)FontStyle.Italic;
                        }
                        if (fontable.Font.Underline == true)
                        {
                            style += (int)FontStyle.Underline;
                        }
                        FontStyle s = (FontStyle)style;
                        fontable.Font = new Font(fontable.Font.FontFamily, fontable.Font.Size, s);
                    }
                    else
                    {
                        int style = 0;
                        if (fontable.Font.Italic == true)
                        {
                            style += (int)FontStyle.Italic;
                        }
                        if (fontable.Font.Underline == true)
                        {
                            style += (int)FontStyle.Underline;
                        }
                        style += (int)FontStyle.Bold;
                        FontStyle s = (FontStyle)style;
                        fontable.Font = new Font(fontable.Font.FontFamily, fontable.Font.Size, s);
                    }
                    flag = 1;
                }
            }

            if (flag == 1)
            {
                Record();
            }


        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int flag = 0;

            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IFontable).ToString()) != null)
                {
                    cons.IFontable fontable = (cons.IFontable)ins;
                    int style = 0;
                    if (fontable.Font.Italic == true)
                    {
                        style += (int)FontStyle.Italic;
                    }
                    if (fontable.Font.Underline == true)
                    {
                        style += (int)FontStyle.Underline;
                    }
                    if (fontable.Font.Bold == true)
                    {
                        style += (int)FontStyle.Bold;
                    }

                    FontStyle s = (FontStyle)style;
                    fontable.Font = new Font(fontable.Font.FontFamily, Helper.Conv.ToInt16(toolStripComboBox1.Text), s);

                    flag = 1;
                }
            }

            if (flag == 1)
            {

                Record();
            }

        }

        private void toolStrip1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void toolStripComboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (toolStripComboBox1.Text.Trim() == "")
                {

                }
                else
                {
                    int size = Helper.Conv.ToInt16(toolStripComboBox1.Text);
                    if (size >= 7 && size <= 60)
                    {
                        toolStripComboBox1.Text = size.ToString();
                        toolStripComboBox1.SelectionStart = 0;
                        toolStripComboBox1.SelectionLength = 0;
                    }
                    else
                    {
                        size = 9;
                        toolStripComboBox1.Text = size.ToString();
                        toolStripComboBox1.SelectionStart = 0;
                        toolStripComboBox1.SelectionLength = 0;
                    }

                }

            }
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            int flag = 0;
            int flag2 = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IFontable).ToString()) != null)
                {
                    cons.IFontable fontable = (cons.IFontable)ins;
                    if (fontable.Font.Italic == true)
                    {
                        if (flag2 == 0)
                        {
                            flag2 = 1;
                        }
                        else if (flag2 == 1)
                        {
                            flag2 = 1;
                        }
                        else
                        {
                            flag2 = 3;
                        }
                    }
                    else
                    {
                        if (flag2 == 0)
                        {
                            flag2 = 2;
                        }
                        else if (flag2 == 2)
                        {
                            flag2 = 2;
                        }
                        else
                        {
                            flag2 = 3;
                        }
                    }

                }
            }

            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IFontable).ToString()) != null)
                {
                    cons.IFontable fontable = (cons.IFontable)ins;
                    if (flag2 == 1)
                    {
                        int style = 0;
                        if (fontable.Font.Bold == true)
                        {
                            style += (int)FontStyle.Bold;
                        }
                        if (fontable.Font.Underline == true)
                        {
                            style += (int)FontStyle.Underline;
                        }
                        FontStyle s = (FontStyle)style;
                        fontable.Font = new Font(fontable.Font.FontFamily, fontable.Font.Size, s);
                    }
                    else
                    {
                        int style = 0;
                        if (fontable.Font.Bold == true)
                        {
                            style += (int)FontStyle.Bold;
                        }
                        if (fontable.Font.Underline == true)
                        {
                            style += (int)FontStyle.Underline;
                        }
                        style += (int)FontStyle.Italic;
                        FontStyle s = (FontStyle)style;
                        fontable.Font = new Font(fontable.Font.FontFamily, fontable.Font.Size, s);
                    }
                    flag = 1;
                }
            }

            if (flag == 1)
            {
                Record();
            }
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            int flag = 0;
            int flag2 = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IFontable).ToString()) != null)
                {
                    cons.IFontable fontable = (cons.IFontable)ins;
                    if (fontable.Font.Underline == true)
                    {
                        if (flag2 == 0)
                        {
                            flag2 = 1;
                        }
                        else if (flag2 == 1)
                        {
                            flag2 = 1;
                        }
                        else
                        {
                            flag2 = 3;
                        }
                    }
                    else
                    {
                        if (flag2 == 0)
                        {
                            flag2 = 2;
                        }
                        else if (flag2 == 2)
                        {
                            flag2 = 2;
                        }
                        else
                        {
                            flag2 = 3;
                        }
                    }

                }
            }

            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IFontable).ToString()) != null)
                {
                    cons.IFontable fontable = (cons.IFontable)ins;
                    if (flag2 == 1)
                    {
                        int style = 0;
                        if (fontable.Font.Bold == true)
                        {
                            style += (int)FontStyle.Bold;
                        }
                        if (fontable.Font.Italic == true)
                        {
                            style += (int)FontStyle.Italic;
                        }
                        FontStyle s = (FontStyle)style;
                        fontable.Font = new Font(fontable.Font.FontFamily, fontable.Font.Size, s);
                    }
                    else
                    {
                        int style = 0;
                        if (fontable.Font.Bold == true)
                        {
                            style += (int)FontStyle.Bold;
                        }
                        if (fontable.Font.Italic == true)
                        {
                            style += (int)FontStyle.Italic;
                        }
                        style += (int)FontStyle.Underline;
                        FontStyle s = (FontStyle)style;
                        fontable.Font = new Font(fontable.Font.FontFamily, fontable.Font.Size, s);
                    }
                    flag = 1;
                }
            }

            if (flag == 1)
            {
                Record();
            }
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show(_fistSelectObject.propertyInfo, "属性");
        }



        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            int flag = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IBorderable).ToString()) != null)
                {
                    flag = 1;
                    cons.IBorderable item = (cons.IBorderable)ins;
                    string text = toolStripSplitButton1.Text.Trim();
                    if (text == "下框线")
                    {
                        item.BorderBottom = 1;
                    }
                    else if (text == "上框线")
                    {
                        item.BorderTop = 1;
                    }
                    else if (text == "左框线")
                    {
                        item.BorderLeft = 1;
                    }
                    else if (text == "右框线")
                    {
                        item.BorderRight = 1;
                    }
                    else if (text == "无框线")
                    {
                        item.BorderTop = 0;
                        item.BorderBottom = 0;
                        item.BorderLeft = 0;
                        item.BorderRight = 0;
                    }
                    else if (text == "外侧框线")
                    {
                        item.BorderTop = 1;
                        item.BorderBottom = 1;
                        item.BorderLeft = 1;
                        item.BorderRight = 1;
                    }

                }
            }

            if (flag == 1)
            {
                Record();
            }
        }

        private void 下框线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripSplitButton1.Text = "下框线";
            int flag = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IBorderable).ToString()) != null)
                {
                    cons.IBorderable item = (cons.IBorderable)ins;
                    if (item.BorderBottom == 0)
                    {
                        flag = 1;
                    }
                    item.BorderBottom = 1;
                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void 上框线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripSplitButton1.Text = "上框线";
            int flag = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IBorderable).ToString()) != null)
                {
                    cons.IBorderable item = (cons.IBorderable)ins;
                    if (item.BorderTop == 0)
                    {
                        flag = 1;
                    }
                    item.BorderTop = 1;
                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void 左框线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripSplitButton1.Text = "左框线";
            int flag = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IBorderable).ToString()) != null)
                {
                    cons.IBorderable item = (cons.IBorderable)ins;
                    if (item.BorderLeft != 1)
                    {
                        flag = 1;
                    }
                    item.BorderLeft = 1;

                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void 右框线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripSplitButton1.Text = "右框线";
            int flag = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IBorderable).ToString()) != null)
                {
                    cons.IBorderable item = (cons.IBorderable)ins;
                    if (item.BorderRight == 0)
                    {
                        flag = 1;
                    }
                    item.BorderRight = 1;

                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void 无框线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripSplitButton1.Text = "无框线";
            int flag = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IBorderable).ToString()) != null)
                {
                    cons.IBorderable item = (cons.IBorderable)ins;
                    item.BorderTop = 0;
                    item.BorderBottom = 0;
                    item.BorderLeft = 0;
                    item.BorderRight = 0;
                    flag = 1;
                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void 外侧框线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripSplitButton1.Text = "外侧框线";
            int flag = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IBorderable).ToString()) != null)
                {
                    cons.IBorderable item = (cons.IBorderable)ins;
                    item.BorderTop = 1;
                    item.BorderBottom = 1;
                    item.BorderLeft = 1;
                    item.BorderRight = 1;
                    flag = 1;
                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void 更多ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cons.IEditBorder border = new cons.EditBorder();
            int borderLeft = 0;
            int borderRight = 0;
            int borderTop = 0;
            int borderBottom = 0;
            if (SelectObjects.Count == 1)
            {
                cons.IBorderable borderable = (cons.IBorderable)SelectObjects[0];
                borderLeft = borderable.BorderLeft;
                borderRight = borderable.BorderRight;
                borderTop = borderable.BorderTop;
                borderBottom = borderable.BorderBottom;
            }
            if (border.EditBorder(borderLeft, borderRight, borderTop, borderBottom,
                out borderLeft, out borderRight, out borderTop, out borderBottom) == true)
            {
                foreach (cons.IPrintObject ins in SelectObjects)
                {
                    cons.IBorderable borderable = (cons.IBorderable)ins;
                    borderable.BorderLeft = borderLeft;
                    borderable.BorderRight = borderRight;
                    borderable.BorderTop = borderTop;
                    borderable.BorderBottom = borderBottom;
                }
                Record();
            }
        }

        private void toolStripSplitButton3_ButtonClick(object sender, EventArgs e)
        {
            int flag = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IColorable).ToString()) != null)
                {
                    cons.IColorable item = (cons.IColorable)ins;
                    if (item.Color != toolStripSplitButton3.ForeColor)
                    {
                        flag = 1;
                    }
                    item.Color = toolStripSplitButton3.ForeColor;

                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void 黑色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripSplitButton3.ForeColor = Color.Black;
            int flag = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IColorable).ToString()) != null)
                {
                    cons.IColorable item = (cons.IColorable)ins;
                    if (item.Color != toolStripSplitButton3.ForeColor)
                    {
                        flag = 1;
                    }
                    item.Color = toolStripSplitButton3.ForeColor;

                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void 蓝色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripSplitButton3.ForeColor = Color.Blue;
            int flag = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IColorable).ToString()) != null)
                {
                    cons.IColorable item = (cons.IColorable)ins;
                    if (item.Color != toolStripSplitButton3.ForeColor)
                    {
                        flag = 1;
                    }
                    item.Color = toolStripSplitButton3.ForeColor;

                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void 红色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripSplitButton3.ForeColor = Color.Red;
            int flag = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IColorable).ToString()) != null)
                {
                    cons.IColorable item = (cons.IColorable)ins;
                    if (item.Color != toolStripSplitButton3.ForeColor)
                    {
                        flag = 1;
                    }
                    item.Color = toolStripSplitButton3.ForeColor;

                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void 更多ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ColorDialog f = new ColorDialog();
            Color def = Color.Black;
            if (SelectObjects.Count == 1)
            {
                cons.IColorable colorable = (cons.IColorable)SelectObjects[0];
                def = colorable.Color;
            }
            f.Color = def;
            if (f.ShowDialog() == DialogResult.OK)
            {
                foreach (cons.IPrintObject ins in SelectObjects)
                {
                    cons.IColorable colorable = (cons.IColorable)ins;
                    colorable.Color = f.Color;
                }
                Record();
            }
        }



        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            int flag = 0;
            cons.ISizeable sizeable2 = (cons.ISizeable)_fistSelectObject;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                cons.ISizeable sizeable = (cons.ISizeable)ins;
                if (sizeable.Location != new Point(sizeable2.Location.X, sizeable.Location.Y))
                {
                    flag = 1;
                }
                sizeable.Location = new Point(sizeable2.Location.X, sizeable.Location.Y);
            }
            if (flag == 1)
            {
                Record();
            }

        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            int flag = 0;
            cons.ISizeable sizeable2 = (cons.ISizeable)_fistSelectObject;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                cons.ISizeable sizeable = (cons.ISizeable)ins;
                if (sizeable.Location != new Point((sizeable2.Location.X + sizeable2.Size.Width) - sizeable.Size.Width, sizeable.Location.Y))
                {
                    flag = 1;
                }
                sizeable.Location = new Point((sizeable2.Location.X + sizeable2.Size.Width) - sizeable.Size.Width, sizeable.Location.Y);
            }
            if (flag == 1)
            {
                Record();
            }

        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            int flag = 1;
            cons.ISizeable sizeable2 = (cons.ISizeable)_fistSelectObject;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                cons.ISizeable sizeable = (cons.ISizeable)ins;
                if (sizeable.Location != new Point(sizeable.Location.X, sizeable2.Location.Y))
                {
                    flag = 1;
                }
                sizeable.Location = new Point(sizeable.Location.X, sizeable2.Location.Y);
            }
            if (flag == 1)
            {
                Record();
            }

        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            int flag = 0;
            cons.ISizeable sizeable2 = (cons.ISizeable)_fistSelectObject;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                cons.ISizeable sizeable = (cons.ISizeable)ins;
                if (sizeable.Location != new Point(sizeable.Location.X, (sizeable2.Location.Y + sizeable2.Size.Height) - sizeable.Size.Height))
                {
                    flag = 1;
                }
                sizeable.Location = new Point(sizeable.Location.X, (sizeable2.Location.Y + sizeable2.Size.Height) - sizeable.Size.Height);
            }
            if (flag == 1)
            {
                Record();
            }

        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (is_edit == true)
                {
                    var res = System.Windows.Forms.MessageBox.Show("打印格式已修改，是否保存？", "提示", MessageBoxButtons.YesNoCancel);
                    if (res == DialogResult.Yes)
                    {
                        cons.IDesign des = this;
                        string xml = des.xml;
                        //
                        if (_SaveStyle != null)
                        {
                            _SaveStyle.Invoke(style_id, xml);
                        }
                        this.xml = xml;
                        //
                        is_edit = false;
                    }
                    else if (res == DialogResult.No)
                    {

                    }
                    else if (res == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            base.OnClosing(e);
        }

        private void 重置上次保存格式ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (this.xml != "")
            {
                cons.IDesign des = this;
                des.xml = xml;
            }
        }



        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                cons.IDesign des = this;
                string xml = des.xml;
                //

                if (_SaveStyle != null)
                {
                    _SaveStyle.Invoke(style_id, xml);
                }
                this.xml = xml;
                //
                is_edit = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void 页面设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TbtnPrintPage_Click(sender, e);
        }

        private void 预览ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                cons.IDesign des = this;
                string xml = des.xml;
                //

                if (_SaveStyle != null)
                {
                    _SaveStyle.Invoke(style_id, xml);
                }
                this.xml = xml;
                //
                is_edit = false;
                //
                IBLL.IPrint ins = new BLL.PrintV();
                ins.Print(style_id, xml, this.tbmain, this.tbdetail);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (operRecord.Pre == null)
            {

            }
            else
            {
                operRecord.Pre.Undo(des);
                operRecord = operRecord.Pre;
            }
        }

        private void 恢复ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (operRecord.Next == null)
            {

            }
            else
            {
                operRecord.Next.Undo(des);
                operRecord = operRecord.Next;
            }
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int flag = 0;
            lstcopy.Clear();

            List<cons.IPrintObject> lst = new List<cons.IPrintObject>();
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                lst.Add(ins);
                lstcopy.Add(ins.Copy());
            }
            foreach (cons.IPrintObject ins in lst)
            {
                if (ins.GetType().GetInterface(typeof(cons.IDeleteable).ToString()) != null)
                {
                    cons.IDeleteable del = (cons.IDeleteable)ins;
                    del.Delete(pnl);
                    flag = 1;
                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            lstcopy.Clear();

            foreach (cons.IPrintObject ins in SelectObjects)
            {

                lstcopy.Add(ins.Copy());
            }
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KeysV();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int flag = 0;
            List<cons.IPrintObject> lst = new List<cons.IPrintObject>();
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                lst.Add(ins);
            }
            foreach (cons.IPrintObject ins in lst)
            {
                if (ins.GetType().GetInterface(typeof(cons.IDeleteable).ToString()) != null)
                {
                    cons.IDeleteable del = (cons.IDeleteable)ins;
                    del.Delete(pnl);
                    flag = 1;
                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (cons.IPrintObject ins in pnl.Controls)
            {
                ins.Selected = true;
            }
        }

        private void 左对齐ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int flag = 0;
            cons.ISizeable sizeable2 = (cons.ISizeable)_fistSelectObject;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                cons.ISizeable sizeable = (cons.ISizeable)ins;
                if (sizeable.Location != new Point(sizeable2.Location.X, sizeable.Location.Y))
                {
                    flag = 1;
                }
                sizeable.Location = new Point(sizeable2.Location.X, sizeable.Location.Y);
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void 右对齐ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int flag = 0;
            cons.ISizeable sizeable2 = (cons.ISizeable)_fistSelectObject;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                cons.ISizeable sizeable = (cons.ISizeable)ins;
                if (sizeable.Location != new Point((sizeable2.Location.X + sizeable2.Size.Width) - sizeable.Size.Width, sizeable.Location.Y))
                {
                    flag = 1;
                }
                sizeable.Location = new Point((sizeable2.Location.X + sizeable2.Size.Width) - sizeable.Size.Width, sizeable.Location.Y);
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void 上对齐ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int flag = 1;
            cons.ISizeable sizeable2 = (cons.ISizeable)_fistSelectObject;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                cons.ISizeable sizeable = (cons.ISizeable)ins;
                if (sizeable.Location != new Point(sizeable.Location.X, sizeable2.Location.Y))
                {
                    flag = 1;
                }
                sizeable.Location = new Point(sizeable.Location.X, sizeable2.Location.Y);
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void 下对齐ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int flag = 0;
            cons.ISizeable sizeable2 = (cons.ISizeable)_fistSelectObject;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                cons.ISizeable sizeable = (cons.ISizeable)ins;
                if (sizeable.Location != new Point(sizeable.Location.X, (sizeable2.Location.Y + sizeable2.Size.Height) - sizeable.Size.Height))
                {
                    flag = 1;
                }
                sizeable.Location = new Point(sizeable.Location.X, (sizeable2.Location.Y + sizeable2.Size.Height) - sizeable.Size.Height);
            }
            if (flag == 1)
            {
                Record();
            }
        }


        private void 下框线ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripSplitButton1.Text = "下框线";
            int flag = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IBorderable).ToString()) != null)
                {
                    cons.IBorderable item = (cons.IBorderable)ins;
                    if (item.BorderBottom == 0)
                    {
                        flag = 1;
                    }
                    item.BorderBottom = 1;
                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void 上框线ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripSplitButton1.Text = "上框线";
            int flag = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IBorderable).ToString()) != null)
                {
                    cons.IBorderable item = (cons.IBorderable)ins;
                    if (item.BorderTop == 0)
                    {
                        flag = 1;
                    }
                    item.BorderTop = 1;
                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void 左框线ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripSplitButton1.Text = "左框线";
            int flag = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IBorderable).ToString()) != null)
                {
                    cons.IBorderable item = (cons.IBorderable)ins;
                    if (item.BorderLeft != 1)
                    {
                        flag = 1;
                    }
                    item.BorderLeft = 1;

                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void 右框线ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripSplitButton1.Text = "右框线";
            int flag = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IBorderable).ToString()) != null)
                {
                    cons.IBorderable item = (cons.IBorderable)ins;
                    if (item.BorderRight == 0)
                    {
                        flag = 1;
                    }
                    item.BorderRight = 1;

                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void 无框线ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripSplitButton1.Text = "无框线";
            int flag = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IBorderable).ToString()) != null)
                {
                    cons.IBorderable item = (cons.IBorderable)ins;
                    item.BorderTop = 0;
                    item.BorderBottom = 0;
                    item.BorderLeft = 0;
                    item.BorderRight = 0;
                    flag = 1;
                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void 外侧框线ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripSplitButton1.Text = "外侧框线";
            int flag = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IBorderable).ToString()) != null)
                {
                    cons.IBorderable item = (cons.IBorderable)ins;
                    item.BorderTop = 1;
                    item.BorderBottom = 1;
                    item.BorderLeft = 1;
                    item.BorderRight = 1;
                    flag = 1;
                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void 更多ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            cons.IEditBorder border = new cons.EditBorder();
            int borderLeft = 0;
            int borderRight = 0;
            int borderTop = 0;
            int borderBottom = 0;
            if (SelectObjects.Count == 1)
            {
                cons.IBorderable borderable = (cons.IBorderable)SelectObjects[0];
                borderLeft = borderable.BorderLeft;
                borderRight = borderable.BorderRight;
                borderTop = borderable.BorderTop;
                borderBottom = borderable.BorderBottom;
            }
            if (border.EditBorder(borderLeft, borderRight, borderTop, borderBottom,
                out borderLeft, out borderRight, out borderTop, out borderBottom) == true)
            {
                foreach (cons.IPrintObject ins in SelectObjects)
                {
                    cons.IBorderable borderable = (cons.IBorderable)ins;
                    borderable.BorderLeft = borderLeft;
                    borderable.BorderRight = borderRight;
                    borderable.BorderTop = borderTop;
                    borderable.BorderBottom = borderBottom;
                }
                Record();
            }
        }

        private void 黑色ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripSplitButton3.ForeColor = Color.Black;
            int flag = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IColorable).ToString()) != null)
                {
                    cons.IColorable item = (cons.IColorable)ins;
                    if (item.Color != toolStripSplitButton3.ForeColor)
                    {
                        flag = 1;
                    }
                    item.Color = toolStripSplitButton3.ForeColor;

                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void 蓝色ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripSplitButton3.ForeColor = Color.Blue;
            int flag = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IColorable).ToString()) != null)
                {
                    cons.IColorable item = (cons.IColorable)ins;
                    if (item.Color != toolStripSplitButton3.ForeColor)
                    {
                        flag = 1;
                    }
                    item.Color = toolStripSplitButton3.ForeColor;

                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void 红色ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripSplitButton3.ForeColor = Color.Red;
            int flag = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IColorable).ToString()) != null)
                {
                    cons.IColorable item = (cons.IColorable)ins;
                    if (item.Color != toolStripSplitButton3.ForeColor)
                    {
                        flag = 1;
                    }
                    item.Color = toolStripSplitButton3.ForeColor;

                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void 更多ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ColorDialog f = new ColorDialog();
            Color def = Color.Black;
            if (SelectObjects.Count == 1)
            {
                cons.IColorable colorable = (cons.IColorable)SelectObjects[0];
                def = colorable.Color;
            }
            f.Color = def;
            if (f.ShowDialog() == DialogResult.OK)
            {
                foreach (cons.IPrintObject ins in SelectObjects)
                {
                    cons.IColorable colorable = (cons.IColorable)ins;
                    colorable.Color = f.Color;
                }
                Record();
            }
        }

        private void 属性ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show(_fistSelectObject.propertyInfo, "属性");
        }

        private void TbtnTextBox_Click(object sender, EventArgs e)
        {

        }

        private void 文本标签ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip2_ItemClicked(toolStrip2, new ToolStripItemClickedEventArgs(TbtnTextBox));
        }

        private void 图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip2_ItemClicked(toolStrip2, new ToolStripItemClickedEventArgs(TbtnPicBox));
        }

        private void 横线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip2_ItemClicked(toolStrip2, new ToolStripItemClickedEventArgs(TbtnLineH));
        }

        private void 竖线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip2_ItemClicked(toolStrip2, new ToolStripItemClickedEventArgs(TbtnLineV));
        }

        private void 数据表格ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



        private void 合计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip2_ItemClicked(toolStrip2, new ToolStripItemClickedEventArgs(TbtnSum));
        }

        private void 页码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip2_ItemClicked(toolStrip2, new ToolStripItemClickedEventArgs(TbtnPageIndex));
        }

        private void 打印时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip2_ItemClicked(toolStrip2, new ToolStripItemClickedEventArgs(TbtnPrintTime));
        }

        private void menuStrip1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void 文件ToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            foreach (ToolStripButton item in toolStrip2.Items)
            {
                item.Checked = false;
            }
            fieldList1.cur_obj = null;
            TbtnPointer.Checked = true;
        }

        private void 编辑ToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            foreach (ToolStripButton item in toolStrip2.Items)
            {
                item.Checked = false;
            }
            fieldList1.cur_obj = null;
            TbtnPointer.Checked = true;
        }

        private void 插入ToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            foreach (ToolStripButton item in toolStrip2.Items)
            {
                item.Checked = false;
            }
            fieldList1.cur_obj = null;
            TbtnPointer.Checked = true;
        }

        private void 格式ToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            foreach (ToolStripButton item in toolStrip2.Items)
            {
                item.Checked = false;
            }
            fieldList1.cur_obj = null;
            TbtnPointer.Checked = true;
        }

        private void 帮助ToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            foreach (ToolStripButton item in toolStrip2.Items)
            {
                item.Checked = false;
            }
            fieldList1.cur_obj = null;
            TbtnPointer.Checked = true;
        }

        private void pnl_DataSize(object sender, EventArgs e)
        {
            Record();
        }

        private void TbtnPicBox_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            int flag = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IContextAlignAble).ToString()) != null)
                {
                    cons.IContextAlignAble item = (cons.IContextAlignAble)ins;
                    if (item.Align != 1)
                    {
                        flag = 1;
                    }
                    item.Align = 1;
                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            int flag = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IContextAlignAble).ToString()) != null)
                {
                    cons.IContextAlignAble item = (cons.IContextAlignAble)ins;
                    if (item.Align != 2)
                    {
                        flag = 1;
                    }
                    item.Align = 2;
                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            int flag = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IContextAlignAble).ToString()) != null)
                {
                    cons.IContextAlignAble item = (cons.IContextAlignAble)ins;
                    if (item.Align != 3)
                    {
                        flag = 1;
                    }
                    item.Align = 3;
                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void 居左ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int flag = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IContextAlignAble).ToString()) != null)
                {
                    cons.IContextAlignAble item = (cons.IContextAlignAble)ins;
                    if (item.Align != 1)
                    {
                        flag = 1;
                    }
                    item.Align = 1;
                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void 居中ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int flag = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IContextAlignAble).ToString()) != null)
                {
                    cons.IContextAlignAble item = (cons.IContextAlignAble)ins;
                    if (item.Align != 2)
                    {
                        flag = 1;
                    }
                    item.Align = 2;
                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void 居右ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int flag = 0;
            foreach (cons.IPrintObject ins in SelectObjects)
            {
                if (ins.GetType().GetInterface(typeof(cons.IContextAlignAble).ToString()) != null)
                {
                    cons.IContextAlignAble item = (cons.IContextAlignAble)ins;
                    if (item.Align != 3)
                    {
                        flag = 1;
                    }
                    item.Align = 3;
                }
            }
            if (flag == 1)
            {
                Record();
            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void tsmiImportStyle_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog of = new OpenFileDialog();
                of.Filter = "打印样式文件|*.ivystyle";
                of.Title = "选择打印样式文件";
                of.Multiselect = false;
                if (of.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream fs = new FileStream(of.FileName, FileMode.Open))
                    {
                        using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                        {
                            string style_xml = sr.ReadToEnd();
                            cons.IDesign des = this;
                            if (!string.IsNullOrEmpty(style_xml))
                            {
                                des.xml = style_xml;
                                pnl.Refresh();
                            }
                            else
                            {
                                MessageBox.Show("导入样式不可为空!");
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导入失败:" + ex.Message);
            }
        }

        private void tsmiExportStyle_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog sf = new SaveFileDialog();
                sf.Filter = "打印样式文件|*.ivystyle";
                sf.Title = "导出打印样式文件";
                if (sf.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream fs = new FileStream(sf.FileName, FileMode.Create))
                    {
                        byte[] data = System.Text.Encoding.UTF8.GetBytes(xml);
                        fs.Write(data, 0, data.Length);
                        fs.Flush();
                    }
                    MessageBox.Show("导出成功");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出失败:" + ex.Message);
            }
        }

    }
}
