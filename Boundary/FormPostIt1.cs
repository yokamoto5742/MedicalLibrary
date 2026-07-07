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
    public partial class FormPostIt1 : StdForm1
    {
        bool _ReadOnly = false;

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
        /// 現在選択されている PostIt
        /// </summary>
        PostIt PostIt1 = new PostIt();

        /// <summary>
        /// デフォルトは this.Pat の当日の受付科。
        /// </summary>
        string DeptCode = "";


        public FormPostIt1(bool read_only = false, string dept_code = "")
        {
            InitializeComponent();

            this.CheckBox1.Checked = true;

            this.DeptCode = dept_code;

            this.DeptBox1.Init();
            this.DeptBox1.SetDept(this.DeptCode);

            this.ReadOnly = read_only;
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            this.ListShow();
        }

        public void DeptSet(string dept_code)
        {
            this.DeptCode = dept_code;

            this.DeptBox1.SetDept(this.DeptCode);
        }

        void DataClear()
        {
            this.PostIt1 = new PostIt();

            // 日付と診療科はキーのため、修正時は変更できない。新規登録の場合のみ変更できる。
            this.DoDatePicker1.Enabled = true;
            this.DeptBox1.Enabled = true;

            this.DoDatePicker1.Value = DateTime.Now;
            this.RegDateTimeLabel1.Text = "";
            this.RegStaffLabel1.Text = "";
            this.ContBox1.Clear();

            this.DeptBox1.SetDept(this.DeptCode);
        }

        void ListShow()
        {
            this.PostItView1.ListShow(this.Pat.Id);

            this.ListFormat();
            this.DataClear();
        }

        void ListFormat()
        {
            List<string> filters = new List<string>();

            if (this.CheckBox1.Checked)
            {
                filters.Add("対象日 = '" + DateTime.Now.ToString("yyyyMMdd") + "'");
            }

            if (!this.CheckBox2.Checked)
            {
                filters.Add("削除フラグ = False");
            }

            this.PostItView1.ListFormat("対象日 desc, 表示順", AppString.ConcatList(filters, " and "));

            if (this.PostItView1.Columns.Contains("内容"))
            {
                this.PostItView1.Columns["内容"].Width = 205;
            }
        }

        private void PostItView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.DataClear();

            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            this.PostIt1 = this.PostItView1.GetPostIt(e.RowIndex);

            this.DoDatePicker1.Value = this.PostIt1.DoDateValue;
            this.DeptBox1.SetDept(this.PostIt1.DeptCode);

            // 日付と診療科はキーのため変更できない
            this.DoDatePicker1.Enabled = false;
            this.DeptBox1.Enabled = false;

            if (this.PostIt1.UpStaffCode.Length > 0)
            {
                this.RegDateTimeLabel1.Text = this.PostIt1.UpDateTimeShort;
                this.RegStaffLabel1.Text = this.PostIt1.UpStaffName;
            }
            else
            {
                this.RegDateTimeLabel1.Text = this.PostIt1.RegDateTimeShort;
                this.RegStaffLabel1.Text = this.PostIt1.RegStaffName;
            }

            this.ContBox1.Text = this.PostIt1.Cont1;
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.ListFormat();
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
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

            if (this.DeptBox1.GetDept().Code == 0)
            {
                err += "診療科が選択されていません" + Environment.NewLine;
            }

            if (this.ContBox1.Text.Length == 0)
            {
                err += "付箋内容が入力されていません" + Environment.NewLine;
            }

            if (err.Length > 0)
            {
                MessageBox.Show(err);
                return;
            }

            if (MessageBox.Show("登録します。よろしいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (this.PostIt1.RegDate > 0)
                {
                    // 更新の場合

                    this.PostIt1.DeptCode = this.DeptBox1.GetDept().Code.ToString();
                    this.PostIt1.DoDate = int.Parse(this.DoDatePicker1.Value.ToString("yyyyMMdd"));
                    this.PostIt1.Cont1 = this.ContBox1.Text;

                    this.PostIt1.Update();
                }
                else
                {
                    // 新規の場合

                    PostIt obj = new PostIt();

                    obj.PtId = this.Pat.Id;
                    obj.InOut = "1";
                    obj.DeptCode = this.DeptBox1.GetDept().Code.ToString();
                    obj.DoDate = int.Parse(this.DoDatePicker1.Value.ToString("yyyyMMdd"));
                    obj.Cont1 = this.ContBox1.Text;

                    obj.Insert();
                }

                this.DataClear();
                this.ListShow();
            }
        }

        private void PostItMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (this.PostItView1.CurrentRow != null)
            {
                if (this.PostItView1.GetPostIt(this.PostItView1.CurrentRow.Index).DeleteFlg)
                {
                    this.DeleteMenuItem1.Enabled = false;
                }
                else
                {
                    this.DeleteMenuItem1.Enabled = true;
                }
            }
            else
            {
                this.DeleteMenuItem1.Enabled = false;
            }
        }

        private void DeleteMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.PostItView1.CurrentRow == null)
            {
                return;
            }

            this.DataDelete(this.PostItView1.GetPostIt(this.PostItView1.CurrentRow.Index));
        }

        private void DeleteButton1_Click(object sender, EventArgs e)
        {
            this.DataDelete(this.PostIt1);
        }

        void DataDelete(PostIt post_it)
        {
            if (post_it.DoDate == 0 || post_it.DeptCode.Length == 0)
            {
                return;
            }

            if (MessageBox.Show("削除します。よろしいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                post_it.Delete();

                this.DataClear();
                this.ListShow();
            }
        }
    }
}
