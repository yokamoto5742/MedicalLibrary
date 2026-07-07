using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class FormDrugAdvTemplate : Form
    {
        Form fp;
        int mode;

        string nodeEditMode = "";
        string nodeBeforeEdit = "";
        TreeNode nodeClicked;

        Dictionary<string, string> nodeDict = new Dictionary<string, string>();

        public struct AdvTemp
        {
            public string temp_id;
            public string temp_node;
            public string temp_name;
        }

        List<AdvTemp> advTempList = new List<AdvTemp>();

        public FormDrugAdvTemplate()
        {
            InitializeComponent();
        }

        public FormDrugAdvTemplate(Form F, int Mode)
        {
            InitializeComponent();

            this.fp = F;
            this.mode = Mode;
        }

        private void FormDrugAdvTemplate_Load(object sender, EventArgs e)
        {
            this.makeTree();
            this.clearTemp();

            this.applyButton.Enabled = true;

            if (mode == 2)
            {
                this.applyBox.Items.Add("指導内容");
                this.applyBox.Items.Add("特記事項");
                this.applyBox.Items.Add("退院後の注意点");
                this.applyBox.Items.Add("調剤上の工夫");
                this.applyBox.Items.Add("その他");
            }
            else
            {
                this.applyBox.Items.Add("指導内容");
            }

            if (!LoginUser.IsDrug)
            {
                this.regButton.Enabled = false;
            }
        }

        private void clearTemp()
        {
            this.nameBox.Text = "";
            this.nodeBox.Text = "";
            this.advBox.Text = "";
            this.modeBox.Text = "新規";
        }

        private void makeTree()
        {
            this.advTreeView.Nodes.Clear();
            this.nodeBox.Items.Clear();
            this.nodeDict.Clear();
            this.advTempList.Clear();

            List<DrugAdvNode> list1 = DrugAdvNode.GetList();

            foreach (DrugAdvNode n in list1)
            {
                if (!n.Status.Equals(1)) continue;

                TreeNode addNode = this.advTreeView.Nodes.Add(n.SEQ.ToString(), n.Name);
                addNode.ContextMenuStrip = nodeContextMenuStrip;
                addNode.Tag = n;

                this.nodeBox.Items.Add(n.Name);
                this.nodeDict.Add(n.Name, n.SEQ.ToString());
            }

            List<DrugAdvTemplate> list2 = DrugAdvTemplate.GetList();

            foreach (DrugAdvTemplate t in list2)
            {
                if (!t.Status.Equals(1)) continue;

                if (this.advTreeView.Nodes.ContainsKey(t.NodeSEQ.ToString()))
                {
                    TreeNode addNode = this.advTreeView.Nodes[t.NodeSEQ.ToString()].Nodes.Add(t.SEQ.ToString(), t.Name);
                    addNode.ContextMenuStrip = tempContextMenuStrip;
                    addNode.Tag = t;

                    AdvTemp tmpTemp = new AdvTemp();
                    tmpTemp.temp_id = t.SEQ.ToString();
                    tmpTemp.temp_node = t.NodeSEQ.ToString();
                    tmpTemp.temp_name = t.Name;
                    this.advTempList.Add(tmpTemp);
                }
            }
        }

        private void newNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.clearTemp();

            this.nodeBox.Text = this.advTreeView.SelectedNode.Text;
        }

        private void renameNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.advTreeView.LabelEdit = true;

            TreeNode tmpNode = this.advTreeView.SelectedNode;
            tmpNode.BeginEdit();

            this.nodeEditMode = "修正";
            this.nodeBeforeEdit = tmpNode.Text;
        }

        private void delNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("分類の下にあるテンプレートもすべて削除されます。\r\n本当に削除しますか？", "削除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                int seq = 0;

                if (int.TryParse(this.nodeDict[this.advTreeView.SelectedNode.Text], out seq))
                {
                    DrugAdvNode.Delete(seq);
                    MessageBox.Show("削除しました");
                }

                this.makeTree();
            }
        }

        private void newTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.advTreeView.LabelEdit = true;

            TreeNode tmpNode = this.advTreeView.Nodes.Add("");
            tmpNode.BeginEdit();

            this.nodeEditMode = "新規";
        }

        private void advTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            try
            {
                string nodeName = e.Label;

                if (this.nodeDict.ContainsKey(nodeName))
                {
                    nodeName += " (2)";
                }

                DrugAdvNode obj = new DrugAdvNode();

                obj.Name = nodeName;
                obj.Status = 1;

                if (this.nodeEditMode == "修正")
                {
                    int.TryParse(this.nodeDict[this.nodeBeforeEdit], out obj.SEQ);
                }

                obj.Save();
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex);
            }

            this.makeTree();

            this.advTreeView.LabelEdit = false;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            this.clearTemp();
        }

        private void showTempToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.showTemp();
        }

        private void advTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.nodeClicked = e.Node;

            if (e.Node.Level > 0)
            {
                this.showTemp();
            }
        }

        private void showTemp()
        {
            this.clearTemp();

            if (this.nodeClicked.Tag is DrugAdvTemplate)
            {
                DrugAdvTemplate t = (DrugAdvTemplate)this.nodeClicked.Tag;

                this.tempIdBox.Text = t.SEQ.ToString();
                this.nameBox.Text = t.Name;
                this.nodeBox.Text = this.advTreeView.Nodes[t.NodeSEQ.ToString()].Text;
                this.advBox.Text = t.Adv;

                this.modeBox.Text = "参照";
            }
        }

        private void delTempToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("削除しますか？", "削除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if (this.advTreeView.SelectedNode.Tag is DrugAdvTemplate)
                {
                    DrugAdvTemplate t = (DrugAdvTemplate)this.advTreeView.SelectedNode.Tag;

                    StdReturn sr = DrugAdvTemplate.Delete(t.SEQ);

                    if (!sr.ErrExist && sr.IntValue > 0)
                    {
                        MessageBox.Show("削除しました");
                    }
                }

                this.makeTree();

                this.clearTemp();
            }
        }

        private void regButton_Click(object sender, EventArgs e)
        {
            DrugAdvTemplate t = new DrugAdvTemplate();

            int.TryParse(this.nodeDict[this.nodeBox.Text], out t.NodeSEQ);
            t.Name = this.nameBox.Text;
            t.Adv = this.advBox.Text;

            if (this.modeBox.Text == "参照")
            {
                int.TryParse(this.tempIdBox.Text, out t.SEQ);
            }

            if (!t.Save().ErrExist)
            {
                MessageBox.Show("登録しました");
            }

            this.makeTree();

            this.clearTemp();
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            if (this.fp != null && this.fp.GetType().Name == "FormDrugAdv")
            {
                FormDrugAdv tmpFp = (FormDrugAdv)fp;

                if (this.mode == 1)
                {
                    tmpFp.advAdvBox.Text += this.advBox.Text + "\r\n";
                }
                else if (this.mode == 2)
                {
                    if (this.applyBox.Text == "特記事項")
                    {
                        tmpFp.disStNoteBox.Text += this.advBox.Text + "\r\n";
                    }
                    else if (this.applyBox.Text == "退院後の注意点")
                    {
                        tmpFp.disPtNoteBox1.Text += this.advBox.Text + "\r\n";
                    }
                    else if (this.applyBox.Text == "調剤上の工夫")
                    {
                        tmpFp.disPtNoteBox2.Text += this.advBox.Text + "\r\n";
                    }
                    else if (this.applyBox.Text == "その他")
                    {
                        tmpFp.disPtNoteBox3.Text += this.advBox.Text + "\r\n";
                    }
                    else
                    {
                        tmpFp.disAdvBox.Text += this.advBox.Text + "\r\n";
                    }
                }
            }
        }

        private void makeTreeButton_Click(object sender, EventArgs e)
        {
            this.makeTree();
        }
    }
}