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

namespace MedicalLibrary.Agent
{
    public partial class FormDrugAdvComment : Form
    {
        PatBase _Pat = new PatBase();

        public PatBase Pat
        {
            get
            {
                if (!this._Pat.Id.Equals(this.PtIdBox.Text) || this._Pat.Name.Length == 0)
                {
                    this._Pat = PatBase.Load(this.PtIdBox.Text);
                }

                return this._Pat;
            }
        }

        DataSet DSet = new DataSet();

        public FormDrugAdvComment()
        {
            InitializeComponent();
        }

        public FormDrugAdvComment(string pt_id)
        {
            InitializeComponent();

            this.PtIdBox.Text = pt_id;
            this.PtInfoBox.Text = this.Pat.Info1;
        }

        private void FormDrugAdvComment_Load(object sender, EventArgs e)
        {
            DataTable table = DSet.Tables.Add("Comment");
            table.Columns.Add("日付");
            table.Columns.Add("件名");
            table.Columns.Add("内容");
            table.Columns.Add("登録者");
            table.Columns.Add("Obj", typeof(DrugAdvComment));

            this.ListShow();
        }

        void ListShow()
        {
            DataTable table = DSet.Tables["Comment"];
            table.Clear();

            List<DrugAdvComment> list = DrugAdvComment.GetListById(this.Pat.Id, "SAVE_DATE desc, SAVE_TIME desc");

            foreach (DrugAdvComment obj in list)
            {
                if (!obj.Status.Equals(1)) continue;

                DataRow r = table.NewRow();

                r["日付"] = DateTimeAgent.DateFormat(obj.SaveDate, DateTimeAgent.DateFormatKind.LONG);
                r["件名"] = obj.Title;
                r["内容"] = obj.Cont;
                r["登録者"] = obj.StaffName;
                r["Obj"] = obj;

                table.Rows.Add(r);
            }

            DataView view = new DataView(table);
            this.CommentListView.DataSource = view;

            CommentListView.Columns["日付"].Width = 75;
            CommentListView.Columns["日付"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            CommentListView.Columns["件名"].Width = 65;
            CommentListView.Columns["件名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            CommentListView.Columns["内容"].Width = 100;
            CommentListView.Columns["内容"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            CommentListView.Columns["登録者"].Width = 65;
            CommentListView.Columns["登録者"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            CommentListView.Columns["Obj"].Visible = false;

            foreach (DataGridViewRow r in this.CommentListView.Rows)
            {
                DrugAdvComment obj = (DrugAdvComment)r.Cells["Obj"].Value;

                if (!obj.Status.Equals(1))
                {
                    r.DefaultCellStyle.BackColor = Color.LightGray;
                    r.DefaultCellStyle.Font = AppFont.GetStrikeoutFont(CommentListView.DefaultCellStyle.Font);
                }
            }
        }

        void DataClear()
        {
            this.TitleBox.Clear();
            this.ContBox.Clear();
            this.DateTimeLabel.Text = "";
        }

        private void PtIdBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.PtInfoBox.Text = this.Pat.Info1;
                this.ListShow();
                this.DataClear();
            }
        }

        private void CommentListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            this.DataClear();

            DrugAdvComment obj = (DrugAdvComment)CommentListView.Rows[e.RowIndex].Cells["Obj"].Value;
            this.TitleBox.Text = obj.Title;
            this.ContBox.Text = obj.Cont;
            this.DateTimeLabel.Text = DateTimeAgent.DateFormat(obj.SaveDate, DateTimeAgent.DateFormatKind.LONG) + " " + DateTimeAgent.TimeFormat6(obj.SaveTime, 4) + " " + obj.StaffName;
        }

        private void NewMenuItem_Click(object sender, EventArgs e)
        {
            this.DataClear();
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (CommentListView.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("削除しますか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }

                DrugAdvComment obj = (DrugAdvComment)CommentListView.SelectedRows[0].Cells["Obj"].Value;
                obj.StatusChange(0);

                ListShow();
                DataClear();
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            this.DataClear();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            List<string> msgs = new List<string>();

            if (this.Pat.Id.Length == 0)
            {
                msgs.Add("患者IDが入力されていません");
            }

            if (this.TitleBox.Text.Length == 0)
            {
                msgs.Add("件名が入力されていません");
            }

            // 内容は空でも登録できるように（内藤薬剤師より）
            if (this.ContBox.Text.Length == 0)
            {
//                msgs.Add("内容が入力されていません");
            }

            // エラーがある場合
            if (msgs.Count > 0)
            {
                MessageBox.Show(AppString.ConcatList(msgs, Environment.NewLine));
                return;
            }

            if (MessageBox.Show("登録しますか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            DrugAdvComment obj = new DrugAdvComment();
            obj.PtId = this.Pat.Id;
            obj.Title = this.TitleBox.Text;
            obj.Cont = this.ContBox.Text;
            obj.Save();

            ListShow();
            DataClear();
        }
    }
}
