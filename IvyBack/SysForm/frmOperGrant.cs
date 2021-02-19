using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using IvyBack.Helper;

namespace IvyBack.SysForm
{
    public partial class frmOperGrant : Form
    {
        public frmOperGrant()
        {
            InitializeComponent();
        }
        Dictionary<string, sys_t_oper_type> types;
        Dictionary<string, TreeNode> tn_dic = new Dictionary<string, TreeNode>();

        private void LoadType()
        {
            this.tvType.Nodes.Clear();

            IBLL.IOper bll = new BLL.OperBLL();
            var list = bll.GetOperTypeList();

            foreach (var type in list)
            {
                TreeNode n = new TreeNode(type.type_name + "[" + type.oper_type + "]");
                n.Tag = type;
                this.tvType.Nodes.Add(n);
            }

        }

        private void LoadGrant()
        {
            this.tvGrant.Nodes.Clear();

            IBLL.IMyDestop bll = new BLL.MyDestop();
            types = bll.GetDic();

            //加报表
            TreeNode btn = new TreeNode("报表管理");

            //获取菜单列表 后四位0000
            var valueslis = types.Values.Where(t => t.type_id.Length == 6 && t.type_id.Substring(2).Equals("0000")).ToList();

            foreach (sys_t_oper_type t in valueslis)
            {
                {
                    TreeNode tn = new TreeNode(t.type_name);
                    tn.Tag = t;

                    //获取子菜单 前两位一样 后两位00
                    var lis = types.Values.Where(w => w.type_id.Length == 6 && w.type_id.Substring(0, 2).Equals(t.type_id.Substring(0, 2))
                        && w.type_id.Substring(4).Equals("00") && !string.IsNullOrEmpty(w.type_value)).ToList();
                    lis.ForEach((sub_t) =>
                    {
                        TreeNode sub_tn = new TreeNode(sub_t.type_name);
                        sub_tn.Tag = sub_t;

                        //添加权限
                        foreach (string key in sub_t.type_value.Split(';'))
                        {
                            if (string.IsNullOrEmpty(key)) continue;

                            TreeNode grand = new TreeNode(types[key].type_name);
                            grand.Tag = types[key];

                            sub_tn.Nodes.Add(grand);
                            tn_dic.Add(sub_t.type_id + "_" + key, grand);
                        }
                        tn.Nodes.Add(sub_tn);
                    });
                    this.tvGrant.Nodes.Add(tn);
                }
                {
                    TreeNode tn = new TreeNode(t.type_name);
                    tn.Tag = t;
                    //获取报表 前两位一样 中间两位00
                    var blis = types.Values.Where(w => w.type_id.Length == 6 && w.type_id.Substring(2, 2).Equals("00")
                        && w.type_id.Substring(0, 2).Equals(t.type_id.Substring(0, 2)) && !string.IsNullOrEmpty(w.type_value)).ToList();
                    blis.ForEach((sub_t) =>
                    {
                        TreeNode sub_tn = new TreeNode(sub_t.type_name);
                        sub_tn.Tag = sub_t;

                        //添加权限
                        foreach (string key in sub_t.type_value.Split(';'))
                        {
                            if (string.IsNullOrEmpty(key)) continue;

                            TreeNode grand = new TreeNode(types[key].type_name);
                            grand.Tag = types[key];

                            sub_tn.Nodes.Add(grand);
                            tn_dic.Add(sub_t.type_id + "_" + key, grand);
                        }
                        tn.Nodes.Add(sub_tn);
                    });

                    if (tn.Nodes.Count > 0)
                        btn.Nodes.Add(tn);
                }
            }

            this.tvGrant.Nodes.Add(btn);
        }

        private void frmOperGrant_Load(object sender, EventArgs e)
        {
            try
            {
                LoadType();
                LoadGrant();
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }

        private void tvGrant_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                //只处理鼠标点击引起的状态变化
                if (e.Action == TreeViewAction.ByMouse)
                {
                    //更新子节点状态
                    UpdateChildNodes(e.Node);
                    //更新父节点
                    if (e.Node.Parent != null)
                        UpdateParentNodes(e.Node.Parent);
                }
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }

        //选中状态
        private void UpdateChildNodes(TreeNode node)
        {
            foreach (TreeNode child in node.Nodes)
            {
                child.Checked = node.Checked;
                UpdateChildNodes(child);
            }
        }
        //复选框
        private void UpdateParentNodes(TreeNode node)
        {
            foreach (TreeNode n in node.Nodes)
            {
                if (n.Checked)
                {
                    node.Checked = true;
                    if (node.Parent != null)
                    {
                        UpdateParentNodes(node.Parent);
                    }
                    return;
                }
            }
            node.Checked = false;
            if (node.Parent != null)
            {
                UpdateParentNodes(node.Parent);
            }
        }

        private void UpdateView(TreeNode node, bool check)
        {
            node.Checked = check;
            foreach (TreeNode tn in node.Nodes)
            {
                tn.Checked = check;
                if (tn.Nodes.Count > 0)
                {
                    UpdateView(tn, check);
                }
            }

        }

        private void tsbAllCheck_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TreeNode t in this.tvGrant.Nodes)
                {
                    UpdateView(t, true);
                }

            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }

        private void tsbNoCheck_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TreeNode t in this.tvGrant.Nodes)
                {
                    UpdateView(t, false);
                }
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MyLove.PermissionsBalidation(this.Text, "04"))
                {

                    return;
                }
                if (this.tvType.SelectedNode == null)
                {
                    MsgForm.ShowFrom("请先选择操作员组");
                    return;
                }

                sa_t_oper_type oper_type = this.tvType.SelectedNode.Tag as sa_t_oper_type;

                if (oper_type.oper_type.Equals("1000"))
                {
                    MsgForm.ShowFrom("系统管理员权限不可修改");
                    return;
                }

                List<sa_t_oper_grant> grantlis = new List<sa_t_oper_grant>();
                var list = tn_dic.Where(d => d.Value.Checked).Select(d => d.Key).ToList();

                list.ForEach(t =>
                {
                    string[] strs = t.Split('_');

                    var item = grantlis.SingleOrDefault(g => g.func_id.Equals(strs[0]));

                    if (item == null)
                    {
                        grantlis.Add(new sa_t_oper_grant()
                        {
                            func_id = strs[0],
                            grant_string = strs[1] + ";",
                            oper_id = oper_type.oper_type,
                            update_time = DateTime.Now,
                        });
                    }
                    else
                    {
                        item.grant_string += strs[1] + ";";
                    }
                });

                IBLL.ISys bll = new BLL.SysBLL();
                bll.SaveGrant(grantlis);

                MsgForm.ShowFrom("保存成功");
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tvType_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (this.tvType.SelectedNode == null) return;

                sa_t_oper_type oper_type = this.tvType.SelectedNode.Tag as sa_t_oper_type;
                if (oper_type.oper_type.Equals("1000"))
                {
                    tsbAllCheck_Click(sender, e);
                }
                else
                {
                    IBLL.ISys bll = new BLL.SysBLL();
                    List<sa_t_oper_grant> list = bll.GetAllGrant(new sa_t_oper_grant()
                    {
                        oper_id = oper_type.oper_type
                    });

                    //清空选项
                    tsbNoCheck_Click(sender, e);

                    list.ForEach((grant) =>
                    {
                        string[] grants = grant.grant_string.Split(';');
                        foreach (string str in grants)
                        {
                            if (string.IsNullOrEmpty(str)) continue;

                            TreeNode tn = tn_dic[grant.func_id + "_" + str];
                            tn.Checked = true;
                            if (tn.Parent != null)
                                UpdateParentNodes(tn.Parent);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }



    }
}
