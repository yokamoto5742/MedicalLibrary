using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class FormPathTree : Form
    {
        FormPath Fp;

        DataSet dSet = new DataSet();

        public FormPathTree(FormPath fp)
        {
            InitializeComponent();

            this.Fp = fp;

            DataTable table = dSet.Tables.Add("Path");
            table.Columns.Add("コード");
            table.Columns.Add("名称");
        }

        private void FormPathTree_Load(object sender, EventArgs e)
        {
            foreach (PathMaster p in PathMaster.ListActive)
            {
                // トップレベルから探索する
                TreeNodeCollection nodes = TreeView1.Nodes;

                // その階層までのタグ名称
                string s = "";

                for (int i = 0; i < p.TagNames.Length; i++)
                {
                    if (s.Length > 0)
                    {
                        s += "\\";
                    }

                    s += p.TagNames[i];

                    bool exist_flg = false;

                    foreach (TreeNode tmp in nodes)
                    {
                        // 同じ名前のノードがすでに存在するかどうか
                        if (tmp.Text.Equals(p.TagNames[i]))
                        {
                            // 存在すれば、それが親ノードとなり、次の子ノードを探索していく
                            nodes = tmp.Nodes;
                            exist_flg = true;
                        }
                    }

                    if (!exist_flg)
                    {
                        // 存在しなければ、新たなノードを追加して、それが親ノードとなり、次の子ノードを探索していく
                        TreeNode n = new TreeNode();
                        n.Text = p.TagNames[i];
                        n.Tag = s;
                        nodes.Add(n);

                        nodes = n.Nodes;
                    }
                }
            }

            // タグ無しパスのノードを作成
            TreeNode nn = new TreeNode();
            nn.Text = "その他";
            nn.Tag = "";

            TreeView1.Nodes.Add(nn);
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string s = e.Node.Tag.ToString();

            DataTable table = dSet.Tables["Path"];
            table.Rows.Clear();

            if (s.Length > 0)
            {
                foreach (PathMaster obj in PathMaster.ListActive)
                {
                    if (obj.TagName.Equals(s))
                    {
                        DataRow r = table.NewRow();

                        r["コード"] = obj.Code;
                        r["名称"] = obj.Name;

                        table.Rows.Add(r);
                    }
                }
            }
            else
            {
                foreach (PathMaster obj in PathMaster.ListInactive)
                {
                    if (obj.TagName.Equals(s))
                    {
                        DataRow r = table.NewRow();

                        r["コード"] = obj.Code;
                        r["名称"] = obj.Name;

                        table.Rows.Add(r);
                    }
                }
            }

            DataView view = new DataView(table);

            ListView1.DataSource = view;
        }

        private void ListView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                Fp.PathMasterSet(PathMaster.Load(ListView1.Rows[e.RowIndex].Cells["コード"].Value.ToString()));
                this.Dispose();
            }
        }
    }
}
