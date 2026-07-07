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
    public partial class FormProblem1 : StdForm1
    {
        bool _ReadOnly = true;

        public bool ReadOnly
        {
            get
            {
                return this._ReadOnly;
            }
            set
            {
                this._ReadOnly = value;

                if (value)
                {
                    this.RegButton1.Visible = false;
                    this.ClearButton1.Visible = false;
                    this.DeleteButton1.Visible = false;
                }
                else
                {
                    this.RegButton1.Visible = true;
                    this.ClearButton1.Visible = true;
                    this.DeleteButton1.Visible = true;
                }
            }
        }

        /// <summary>
        /// 現在表示されている ProblemData
        /// </summary>
        ProblemData ProblemData1 = new ProblemData();

        string InOut = "";

        string InOutString
        {
            get
            {
                string s = "";

                if (this.InOut.Equals("1"))
                {
                    s = "外来";
                }
                else if (this.InOut.Equals("2"))
                {
                    s = "入院";
                }

                return s;
            }
        }

        string DeptCode = "";

        public FormProblem1(bool read_only = true, string in_out_code = "", string dept_code = "")
        {
            InitializeComponent();

            this.InOut = in_out_code;

            this.InOutBox1.Items.Add("");
            this.InOutBox1.Items.Add("外来");
            this.InOutBox1.Items.Add("入院");
            this.InOutBox1.Text = this.InOutString;

            this.InOutShowBox1.Items.Add("");
            this.InOutShowBox1.Items.Add("外来");
            this.InOutShowBox1.Items.Add("入院");
            this.InOutShowBox1.Text = this.InOutString;

            this.SectionBox1.Init();

            this.DeptCode = dept_code;

            this.DeptBox1.Init(true, false, false);
            this.DeptBox1.SetDept(this.DeptCode);

            this.DeptBox2.Init(true, false, false);
            this.DeptBox2.SetDept(this.DeptCode);

            Dictionary<int, ProblemTopMaster> top_dict = ProblemTopMaster.Dict;

            foreach (int key in top_dict.Keys)
            {
                TreeNode node = new TreeNode();
                node.Text = top_dict[key].Name;
                node.Name = top_dict[key].SEQ1.ToString();

                ProblemMasterTreeView1.Nodes.Add(node);
            }

            List<ProblemMaster> list = ProblemMaster.AllList;

            foreach (ProblemMaster obj in list)
            {
                if (ProblemMasterTreeView1.Nodes.ContainsKey(obj.SEQ1.ToString()))
                {
                    TreeNode pnode = ProblemMasterTreeView1.Nodes[obj.SEQ1.ToString()];

                    TreeNode node = new TreeNode();
                    node.Name = obj.SEQ2.ToString();
                    node.Text = obj.Cont;

                    pnode.Nodes.Add(node);
                }
            }

            this.ProblemMasterTreeView1.ExpandAll();
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            this.ListShow();
        }

        void DataClear()
        {
            this.ProblemData1 = new ProblemData();

            this.InOutBox1.Enabled = true;
            this.DatePicker1.Enabled = true;
            this.DeptBox1.Enabled = true;

            this.InOutBox1.Text = this.InOutString;

            this.DatePicker1.Value = DateTime.Now;
            this.DeptBox1.SetDept(this.DeptCode);
            this.StaffLabel1.Text = "";

            this.ContBox1.Clear();

            this.ResolveBox1.Checked = false;

            this.DatePicker2.Value = DateTime.Now;
            this.DeptBox2.SetDept(this.DeptCode);
            this.StaffLabel2.Text = "";
        }

        void ListShow()
        {
            this.ProblemGridView1.ListShow(this.Pat.Id);

            this.ListFormat();
            this.DataClear();
        }

        void ListFormat()
        {
            List<string> filters = new List<string>();

            /*
            if (this.SectionBox1.Text.Length > 0 && this.SectionBox1.Text.Contains(" "))
            {
                if (!this.SectionBox1.Text.Split(' ')[0].Equals("0"))
                {
                    filters.Add("資格コード = " + this.SectionBox1.GetSection().Code);
                }
            }
            */

            if (!this.SectionBox1.GetSection().Code.ToString().Equals("0"))
            {
                filters.Add("資格コード = " + this.SectionBox1.GetSection().Code);
            }

            if (this.InOutShowBox1.Text.Length > 0)
            {
                filters.Add("入外区分 = '" + this.InOutShowBox1.Text + "'");
            }

            if (!this.ResolveShowBox1.Checked)
            {
                filters.Add("解決日 = ''");
            }

            this.ProblemGridView1.ListFormat("プロブレムＮＯ desc", AppString.ConcatList(filters, " and "));

            this.ProblemGridView1.Columns["問題内容"].Width = 160;
        }

        private void ProblemMasterTreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.ContBox1.Text += e.Node.Text + Environment.NewLine;
        }

        private void ProblemGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.DataClear();

            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            this.ProblemData1 = this.ProblemGridView1.GetProblemData(e.RowIndex);

            this.DatePicker1.Value = this.ProblemData1.DateValue1;
            this.InOutBox1.Text = this.ProblemData1.InOutString;
            this.DeptBox1.SetDept(this.ProblemData1.DeptCode1);
            this.StaffLabel1.Text = this.ProblemData1.StaffName1;

            this.ContBox1.Text = this.ProblemData1.Cont;

            if (this.ProblemData1.Date2.Length == 8)
            {
                this.ResolveBox1.Checked = true;
                this.DatePicker2.Value = this.ProblemData1.DateValue2;
                this.DeptBox2.SetDept(this.ProblemData1.DeptCode2);
                this.StaffLabel2.Text = this.ProblemData1.StaffName2;
            }

            // 入外区分・発生日・発生科は変更できない
            this.InOutBox1.Enabled = false;
            this.DatePicker1.Enabled = false;
            this.DeptBox1.Enabled = false;
        }

        private void SectionBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ListFormat();
        }

        private void ResolveShowBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.ListFormat();
        }

        private void InOutShowBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ListFormat();
        }

        private void ClearButton1_Click(object sender, EventArgs e)
        {
            this.DataClear();
        }

        private void RegButton1_Click(object sender, EventArgs e)
        {
            string err = "";

            if (this.InOutBox1.Text.Length == 0)
            {
                err += "入外区分が選択されていません。" + Environment.NewLine;
            }

            /*
            if (this.DeptBox1.GetDept().Code == 0)
            {
                err += "発生科が選択されていません" + Environment.NewLine;
            }
            */

            if (this.ContBox1.Text.Length == 0)
            {
                err += "プロブレムが入力されていません" + Environment.NewLine;
            }

            if (err.Length > 0)
            {
                MessageBox.Show(err);
                return;
            }

            if (MessageBox.Show("登録します。よろしいですか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            if (this.ProblemData1.RegDate > 0)
            {
                // 更新の場合
                // 発生登録者と資格コードは変更しない

                if (this.InOutBox1.Text.Equals("外来"))
                {
                    this.ProblemData1.InOut = "1";
                }
                else if (this.InOutBox1.Text.Equals("入院"))
                {
                    this.ProblemData1.InOut = "2";
                }

                this.ProblemData1.Date1 = this.DatePicker1.Value.ToString("yyyyMMdd");
                this.ProblemData1.DeptCode1 = this.DeptBox1.GetDept().Code.ToString();
                this.ProblemData1.Cont = this.ContBox1.Text;

                if (this.ResolveBox1.Checked)
                {
                    this.ProblemData1.Date2 = this.DatePicker2.Value.ToString("yyyyMMdd");
                    this.ProblemData1.DeptCode2 = this.DeptBox2.GetDept().Code.ToString();
                    this.ProblemData1.StaffCode2 = LoginUser.Id;
                }
                else
                {
                    this.ProblemData1.Date2 = "";
                    this.ProblemData1.DeptCode2 = "";
                    this.ProblemData1.StaffCode2 = "";
                }

                this.ProblemData1.Update();
            }
            else
            {
                // 新規の場合

                ProblemData obj = new ProblemData();

                obj.PtId = this.Pat.Id;

                if (this.InOutBox1.Text.Equals("外来"))
                {
                    obj.InOut = "1";
                }
                else if (this.InOutBox1.Text.Equals("入院"))
                {
                    obj.InOut = "2";
                }

                obj.Date1 = this.DatePicker1.Value.ToString("yyyyMMdd");
                obj.DeptCode1 = this.DeptBox1.GetDept().Code.ToString();
                obj.Cont = this.ContBox1.Text;

                if (this.ResolveBox1.Checked)
                {
                    obj.Date2 = this.DatePicker2.Value.ToString("yyyyMMdd");
                    obj.DeptCode2 = this.DeptBox2.GetDept().Code.ToString();
                    obj.StaffCode2 = LoginUser.Id;
                }
                else
                {
                    obj.Date2 = "";
                    obj.DeptCode2 = "";
                    obj.StaffCode2 = "";
                }

                obj.Insert();
            }

            this.DataClear();
            this.ListShow();
        }

        private void DeleteButton1_Click(object sender, EventArgs e)
        {
            if (this.ProblemData1.SEQ == 0)
            {
                return;
            }

            if (MessageBox.Show("削除します。よろしいですか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            this.ProblemData1.Delete();

            this.DataClear();
            this.ListShow();
        }

        private void ResolveMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.ProblemGridView1.CurrentRow == null)
            {
                return;
            }

            if (MessageBox.Show("解決します。よろしいですか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }            

            ProblemData obj = this.ProblemGridView1.GetProblemData(this.ProblemGridView1.CurrentRow.Index);

            // 解決日は本日とする
            obj.Date2 = DateTime.Now.ToString("yyyyMMdd");

            // 解決科は発生科と同じとする
            obj.DeptCode2 = this.DeptBox1.GetDept().Code.ToString();

            obj.StaffCode2 = LoginUser.Id;

            obj.Update();

            this.ListShow();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (this.ProblemGridView1.CurrentRow != null)
            {
                ProblemData obj = this.ProblemGridView1.GetProblemData(this.ProblemGridView1.CurrentRow.Index);

                // 解決済みの場合
                if (obj.Date2.Length == 8)
                {
                    this.ResolveMenuItem1.Enabled = false;
                }
                else
                {
                    this.ResolveMenuItem1.Enabled = true;
                }
            }
            else
            {
                this.ResolveMenuItem1.Enabled = false;
            }
        }
    }
}
