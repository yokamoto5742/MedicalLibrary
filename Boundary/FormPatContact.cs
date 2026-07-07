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
    public partial class FormPatContact : StdForm1
    {
        PatContact PatContact1 = new PatContact();

        DataSet dSet = new DataSet();

        public FormPatContact()
        {
            InitializeComponent();

            DataTable table = dSet.Tables.Add("連絡先");
            table.Columns.Add("連番");
            table.Columns.Add("連絡優先順位");
            table.Columns.Add("カナ氏名");
            table.Columns.Add("漢字氏名");
            table.Columns.Add("生年月日");
            table.Columns.Add("年齢");
            table.Columns.Add("続柄区分");
            table.Columns.Add("続柄");
            table.Columns.Add("続柄コメント");
            table.Columns.Add("電話番号１");
            table.Columns.Add("連絡先区分１");
            table.Columns.Add("区分１");
            table.Columns.Add("電話番号２");
            table.Columns.Add("連絡先区分２");
            table.Columns.Add("区分２");
            table.Columns.Add("電話番号３");
            table.Columns.Add("連絡先区分３");
            table.Columns.Add("区分３");
            table.Columns.Add("健康状態");
            table.Columns.Add("同別居区分");
            table.Columns.Add("同別居");
            table.Columns.Add("介護役割");
            table.Columns.Add("備考");
            table.Columns.Add("登録日時");
            table.Columns.Add("登録者");

            foreach (PatContactRelation obj in PatContactRelation.Dict.Values)
            {
                this.RelationBox.Items.Add(obj);
            }

            foreach (PatContactKind obj in PatContactKind.Dict.Values)
            {
                this.KindBox1.Items.Add(obj);
                this.KindBox2.Items.Add(obj);
                this.KindBox3.Items.Add(obj);
            }

            foreach (PatContactResident obj in PatContactResident.Dict.Values)
            {
                this.ResidentBox.Items.Add(obj);
            }

            this.ShowSEQBox.Items.Add("");

            for (int i = 1; i <= 10; i++)
            {
                this.ShowSEQBox.Items.Add(i);
            }

            // 優先順位を上下するボタン。実際は使わない。
            this.UpButton1.Visible = false;
            this.DownButton1.Visible = false;
        }


        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            this.ListShow();
        }

        void DataClear()
        {
            this.ShowSEQBox.Text = "";

            this.NameBox.Clear();
            this.KanaBox.Clear();
            this.BirthBox.Clear();
            this.RelationBox.Text = "";
            this.RelationCommentBox.Clear();
            this.ResidentBox.Text = "";

            this.KindBox1.Text = "";
            this.TelBox1.Clear();
            this.KindBox2.Text = "";
            this.TelBox2.Clear();
            this.KindBox3.Text = "";
            this.TelBox3.Clear();

            this.HealthBox.Clear();
            this.CareBox.Clear();
            this.ContBox.Clear();

            this.StaffLabel.Text = "";
            this.DateTimeLabel.Text = "";
        }

        void DataShow()
        {
            this.DataClear();

            this.ShowSEQBox.Text = this.PatContact1.ShowSEQ.ToString();

            this.NameBox.Text = this.PatContact1.Name;
            this.KanaBox.Text = this.PatContact1.Kana;
            this.BirthBox.DateString = this.PatContact1.Birth;
            this.RelationBox.Text = this.PatContact1.RelationVal;
            this.RelationCommentBox.Text = this.PatContact1.RelationComment;
            this.ResidentBox.Text = this.PatContact1.ResidentVal;

            this.KindBox1.Text = this.PatContact1.KindVal1;
            this.TelBox1.Text = this.PatContact1.Tel1;

            this.KindBox2.Text = this.PatContact1.KindVal2;
            this.TelBox2.Text = this.PatContact1.Tel2;

            this.KindBox3.Text = this.PatContact1.KindVal3;
            this.TelBox3.Text = this.PatContact1.Tel3;

            this.HealthBox.Text = this.PatContact1.Health;
            this.CareBox.Text = this.PatContact1.Care;
            this.ContBox.Text = this.PatContact1.Cont;

            this.StaffLabel.Text = this.PatContact1.UpStaffName;
            this.DateTimeLabel.Text = this.PatContact1.UpDateTime;
        }

        void ListClear()
        {
            DataTable table = dSet.Tables["連絡先"];
            table.Rows.Clear();

            this.ListFormat();
        }

        void ListShow()
        {
            DataTable table = dSet.Tables["連絡先"];
            table.Rows.Clear();

            List<PatContact> list = PatContact.GetList(this.Pat.Id);

            foreach (PatContact obj in list)
            {
                DataRow r = table.NewRow();

                r["連番"] = obj.SEQ;
                r["連絡優先順位"] = obj.ShowSEQ;
                r["カナ氏名"] = obj.Kana;
                r["漢字氏名"] = obj.Name;
                r["生年月日"] = obj.BirthString;
                r["年齢"] = obj.Age;
                r["続柄区分"] = obj.RelationCode;
                r["続柄"] = obj.RelationVal;
                r["続柄コメント"] = obj.RelationComment;
                r["電話番号１"] = obj.Tel1;
                r["連絡先区分１"] = obj.KindCode1;
                r["区分１"] = obj.KindVal1;
                r["電話番号２"] = obj.Tel2;
                r["連絡先区分２"] = obj.KindCode2;
                r["区分２"] = obj.KindVal2;
                r["電話番号３"] = obj.Tel3;
                r["連絡先区分３"] = obj.KindCode3;
                r["区分３"] = obj.KindVal3;
                r["健康状態"] = obj.Health;
                r["同別居区分"] = obj.ResidentCode;
                r["同別居"] = obj.ResidentVal;
                r["介護役割"] = obj.Care;
                r["備考"] = obj.Cont;
                r["登録日時"] = obj.UpDateTime;
                r["登録者"] = obj.UpStaffName;

                table.Rows.Add(r);
            }

            this.ListFormat();

            this.UpDownButtonFormat();
        }

        void ListFormat(int i = 0)
        {
            DataView view = new DataView(dSet.Tables["連絡先"]);

            this.ListView1.DataSource = view;

            this.ListView1.Columns["連番"].Width = 50;
            this.ListView1.Columns["連番"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.ListView1.Columns["連番"].Visible = false;

            this.ListView1.Columns["連絡優先順位"].Width = 40;
            this.ListView1.Columns["連絡優先順位"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView1.Columns["連絡優先順位"].HeaderText = "優先";

            this.ListView1.Columns["カナ氏名"].Width = 70;
            this.ListView1.Columns["カナ氏名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["漢字氏名"].Width = 80;
            this.ListView1.Columns["漢字氏名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["生年月日"].Width = 70;
            this.ListView1.Columns["生年月日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.ListView1.Columns["年齢"].Width = 40;
            this.ListView1.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.ListView1.Columns["続柄区分"].Width = 50;
            this.ListView1.Columns["続柄区分"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.ListView1.Columns["続柄区分"].Visible = false;

            this.ListView1.Columns["続柄"].Width = 50;
            this.ListView1.Columns["続柄"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["続柄コメント"].Width = 60;
            this.ListView1.Columns["続柄コメント"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["電話番号１"].Width = 60;
            this.ListView1.Columns["電話番号１"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["連絡先区分１"].Width = 50;
            this.ListView1.Columns["連絡先区分１"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.ListView1.Columns["連絡先区分１"].Visible = false;

            this.ListView1.Columns["区分１"].Width = 50;
            this.ListView1.Columns["区分１"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["電話番号２"].Width = 60;
            this.ListView1.Columns["電話番号２"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["連絡先区分２"].Width = 50;
            this.ListView1.Columns["連絡先区分２"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.ListView1.Columns["連絡先区分２"].Visible = false;

            this.ListView1.Columns["区分２"].Width = 50;
            this.ListView1.Columns["区分２"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["電話番号３"].Width = 60;
            this.ListView1.Columns["電話番号３"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["連絡先区分３"].Width = 50;
            this.ListView1.Columns["連絡先区分３"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.ListView1.Columns["連絡先区分３"].Visible = false;

            this.ListView1.Columns["区分３"].Width = 50;
            this.ListView1.Columns["区分３"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["健康状態"].Width = 50;
            this.ListView1.Columns["健康状態"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["同別居区分"].Width = 50;
            this.ListView1.Columns["同別居区分"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.ListView1.Columns["同別居区分"].Visible = false;

            this.ListView1.Columns["同別居"].Width = 50;
            this.ListView1.Columns["同別居"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["介護役割"].Width = 50;
            this.ListView1.Columns["介護役割"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["備考"].Width = 50;
            this.ListView1.Columns["備考"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["登録日時"].Width = 80;
            this.ListView1.Columns["登録日時"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["登録者"].Width = 80;
            this.ListView1.Columns["登録者"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            if (this.ListView1.RowCount > i)
            {
                this.ListView1.Rows[i].Selected = true;
            }
        }

        private void ListView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.UpDownButtonFormat();

            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            DataGridViewRow r = ListView1.Rows[e.RowIndex];
            int i = 1;
            int.TryParse(r.Cells["連番"].Value.ToString(), out i);

            this.PatContact1 = PatContact.Load(this.Pat.Id, i);

            this.DataShow();
        }

        /// <summary>
        /// 優先順位を上下する。実際は使わない。
        /// </summary>
        void UpDownButtonFormat()
        {
            /*
            this.UpButton1.Enabled = false;
            this.DownButton1.Enabled = false;

            if (ListView1.SelectedRows.Count == 0)
            {
                return;
            }

            if (ListView1.RowCount <= 1)
            {
                return;
            }

            if (ListView1.SelectedRows[0].Index > 0)
            {
                this.UpButton1.Enabled = true;
            }

            if (ListView1.SelectedRows[0].Index < ListView1.RowCount - 1)
            {
                this.DownButton1.Enabled = true;
            }
             */
        }

        /// <summary>
        /// 1つ上の行と入れ替える
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpButton1_Click(object sender, EventArgs e)
        {
            if (ListView1.SelectedRows.Count == 0)
            {
                return;
            }

            if (ListView1.RowCount <= 1)
            {
                return;
            }

            if (ListView1.SelectedRows[0].Index <= 0)
            {
                return;
            }

            int i = ListView1.SelectedRows[0].Index;

            DataTable table = dSet.Tables["連絡先"];

            // コピー元の行
            DataRow r1 = table.Rows[i];

            // 新たな行を作ってコピーする
            DataRow r = table.NewRow();

            for (int j = 0; j < r1.ItemArray.Length; j++)
            {
                r[j] = r1[j];
            }

            table.Rows.InsertAt(r, i - 1);

            // コピー元の行は削除する
            table.Rows.RemoveAt(i + 1);

            this.ListFormat(i - 1);

            this.UpDownButtonFormat();
        }

        /// <summary>
        /// 1つ下の行と入れ替える
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownButton1_Click(object sender, EventArgs e)
        {
            if (ListView1.SelectedRows.Count == 0)
            {
                return;
            }

            if (ListView1.RowCount <= 1)
            {
                return;
            }

            if (ListView1.SelectedRows[0].Index >= ListView1.RowCount - 1)
            {
                return;
            }

            int i = ListView1.SelectedRows[0].Index;

            DataTable table = dSet.Tables["連絡先"];

            // コピー元の行
            DataRow r1 = table.Rows[i];

            // 新たな行を作ってコピーする
            DataRow r = table.NewRow();

            for (int j = 0; j < r1.ItemArray.Length; j++)
            {
                r[j] = r1[j];
            }

            table.Rows.InsertAt(r, i + 2);

            // コピー元の行は削除する
            table.Rows.RemoveAt(i);

            this.ListFormat(i + 1);

            this.UpDownButtonFormat();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            this.PatContact1 = new PatContact();
            this.DataClear();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (this.Pat.Id.Length == 0)
            {
                return;
            }

            if (this.ShowSEQBox.Text.Length == 0)
            {
                MessageBox.Show("優先順位が入力されていません");
                return;
            }

            if (this.NameBox.Text.Length == 0)
            {
                MessageBox.Show("氏名が入力されていません");
                return;
            }

            if (MessageBox.Show("登録しますか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            this.PatContact1.PtId = this.Pat.Id;
            int.TryParse(this.ShowSEQBox.Text, out this.PatContact1.ShowSEQ);
            this.PatContact1.Name = this.NameBox.Text;
            this.PatContact1.Kana = this.KanaBox.Text;
            this.PatContact1.Birth = this.BirthBox.DateInt.ToString();

            if (this.RelationBox.SelectedItem != null)
            {
                this.PatContact1.RelationCode = ((PatContactRelation)this.RelationBox.SelectedItem).Code;
            }

            this.PatContact1.RelationComment = this.RelationCommentBox.Text;

            if (this.ResidentBox.SelectedItem != null)
            {
                this.PatContact1.ResidentCode = ((PatContactResident)this.ResidentBox.SelectedItem).Code;
            }

            if (this.KindBox1.SelectedItem != null)
            {
                this.PatContact1.KindCode1 = ((PatContactKind)this.KindBox1.SelectedItem).Code;
            }

            this.PatContact1.Tel1 = this.TelBox1.Text;

            if (this.KindBox2.SelectedItem != null)
            {
                this.PatContact1.KindCode2 = ((PatContactKind)this.KindBox2.SelectedItem).Code;
            }

            this.PatContact1.Tel2 = this.TelBox2.Text;

            if (this.KindBox3.SelectedItem != null)
            {
                this.PatContact1.KindCode3 = ((PatContactKind)this.KindBox3.SelectedItem).Code;
            }

            this.PatContact1.Tel3 = this.TelBox3.Text;

            this.PatContact1.Health = this.HealthBox.Text;
            this.PatContact1.Care = this.CareBox.Text;
            this.PatContact1.Cont = this.ContBox.Text;

            if (this.PatContact1.SEQ > 0)
            {
                // 更新
                this.PatContact1.Update();
            }
            else
            {
                // 新規
                this.PatContact1.Insert();
            }

            this.ListShow();

            this.PatContact1 = new PatContact();
            this.DataClear();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (this.PatContact1.PtId.Length == 0 || this.PatContact1.SEQ == 0)
            {
                return;
            }

            if (MessageBox.Show("削除しますか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            this.PatContact1.Delete();

            this.ListShow();

            this.PatContact1 = new PatContact();
            this.DataClear();
        }
    }
}
