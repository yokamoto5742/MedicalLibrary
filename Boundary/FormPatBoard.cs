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
    public partial class FormPatBoard : StdForm1
    {
        enum Mode : int
        {
            None = 0,
            Order = 1,
            Reply = 2
        }

        Mode Mode1 = Mode.None;

        DataSet dSet = new DataSet();

        public FormPatBoard()
        {
            InitializeComponent();

            DataTable table = dSet.Tables.Add("患者掲示板");

            table.Columns.Add("SEQ1", typeof(int));
            table.Columns.Add("SEQ2", typeof(int));
            table.Columns.Add("記事数", typeof(int));
            table.Columns.Add("状態コード");
            table.Columns.Add("状態");
            table.Columns.Add("タイトル");
            table.Columns.Add("種別");
            table.Columns.Add("記事");
            table.Columns.Add("登録日時");
            table.Columns.Add("登録者コード");
            table.Columns.Add("登録者");
            table.Columns.Add("代行登録者コード");
            table.Columns.Add("所属");
            table.Columns.Add("削除フラグ", typeof(int));
            table.Columns.Add("削除", typeof(int));

            KindBox1.Items.Add("");
            KindBox1.Items.Add("オーダー");
            KindBox1.Items.Add("基本指示");
            KindBox1.Items.Add("看護指示");
            KindBox1.Items.Add("手術指示");

            this.ListFormat();

            this.ModeChange(Mode.None);
        }

        private void FormPatBoard_Load(object sender, EventArgs e)
        {
//            this.Pat = AppStat.CurrentPat;
//            this.PtIdBox1.Text = AppDict.CurrentPat.Id;
        }

        public override void FontSet(AppFont f)
        {
            base.FontSet(f);

            this.stdControlFont11.FontSet(f);

            // ListView 内のフォントを変更する
            if (this.ListView1.Columns.Count > 0)
            {
                this.ListFormat();
            }
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            this.ListShow();
        }

        void ListShow()
        {
            DataTable table = dSet.Tables["患者掲示板"];
            table.Rows.Clear();

            this.DataClear();

            Dictionary<string, List<PatBoard>> dict = PatBoard.GetDict(this.Pat.Id);

            foreach (string key in dict.Keys)
            {
                List<PatBoard> list = dict[key];

                foreach (PatBoard obj in list)
                {
                    DataRow r = table.NewRow();

                    r["SEQ1"] = obj.SEQ1;
                    r["SEQ2"] = obj.SEQ2;
                    r["記事数"] = list.Count;
                    r["状態コード"] = obj.StatusCode;

                    if (obj.SEQ2.Equals(1))
                    {
                        r["状態"] = obj.StatusName;
                        r["タイトル"] = obj.KindName1;
                        r["種別"] = "●";
                    }
                    else
                    {
                        r["種別"] = ">";
                    }

                    r["記事"] = obj.Cont1;
                    r["登録日時"] = obj.UpDateTime;
                    r["登録者コード"] = obj.UpStaffCode;
                    r["登録者"] = obj.UpStaffName;
                    r["代行登録者コード"] = obj.UpStaffCode2;
                    r["所属"] = obj.UpStaffSectionName;
                    r["削除フラグ"] = obj.DeleteFlg;
                    r["削除"] = obj.DeleteFlg;

                    table.Rows.Add(r);
                }
            }

            // 親が削除ならば子も削除
            foreach (DataRow r in table.Rows)
            {
                if (r["種別"].ToString().Equals("●") && r["削除"].ToString().Equals("1"))
                {
                    string seq1 = r["SEQ1"].ToString();

                    foreach (DataRow rr in table.Rows)
                    {
                        if (rr["種別"].ToString().Equals("●"))
                        {
                            continue;
                        }

                        if (rr["SEQ1"].ToString().Equals(seq1))
                        {
                            rr["削除"] = 1;
                        }
                    }
                }
            }

            this.ListFormat();
        }

        void ListFormat()
        {
            DataTable table = dSet.Tables["患者掲示板"];

            DataView view = new DataView(table);
            ListView1.DataSource = view;

            ListView1.Columns["SEQ1"].Visible = false;
            ListView1.Columns["SEQ2"].Visible = false;
            ListView1.Columns["記事数"].Visible = false;
            ListView1.Columns["状態コード"].Visible = false;
            ListView1.Columns["状態"].Width = 50;
            ListView1.Columns["タイトル"].Width = 65;
            ListView1.Columns["種別"].Width = 25;
            ListView1.Columns["種別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView1.Columns["記事"].Width = 360;
            ListView1.Columns["登録日時"].Width = 120;
            ListView1.Columns["登録日時"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView1.Columns["登録者コード"].Visible = false;
            ListView1.Columns["登録者"].Width = 75;
            ListView1.Columns["登録者"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView1.Columns["代行登録者コード"].Visible = false;
            ListView1.Columns["所属"].Width = 65;
            ListView1.Columns["削除フラグ"].Visible = false;

            // ソートできないようにする
            foreach (DataGridViewColumn c in ListView1.Columns)
            {
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            bool inactive_flg = this.InactiveBox1.Checked;
            bool done_flg = this.DoneBox1.Checked;

            List<string> filters = new List<string>();

            if (!inactive_flg)
            {
                filters.Add("削除 <> 1");
            }

            if (!done_flg)
            {
                filters.Add("状態コード <> 2");
            }

            string filter = "";

            foreach (string s in filters)
            {
                if (filter.Length > 0)
                {
                    filter += " and ";
                }

                filter += s;
            }

            view.RowFilter = filter;

            // 返信は非表示にし、背景色を変える
            foreach (DataGridViewRow r in ListView1.Rows)
            {
                if (r.Cells["種別"].Value.ToString().Equals("●"))
                {
                    r.DefaultCellStyle.Font = new AppFont(this.Font, FontStyle.Bold).Ft;
                }
                else
                {
                    /*
                    r.Visible = false;
                     */
                }

                if (r.Cells["削除フラグ"].Value.ToString().Equals("1"))
                {
                    r.DefaultCellStyle.Font = new AppFont(this.Font, FontStyle.Strikeout).Ft;
/*
                    if (!inactive_flg)
                    {
                        r.Visible = false;

                        if (r.Cells["種別"].Value.ToString().Equals("●"))
                        {
                            foreach (DataGridViewRow rr in ListView1.Rows)
                            {
                                if (r.Cells["SEQ1"].Value.ToString().Equals(rr.Cells["SEQ1"].Value.ToString()))
                                {
                                    rr.Visible = false;
                                }
                            }
                        }
                    }
 */
                }

                if (r.Cells["状態コード"].Value.ToString().Equals("0"))
                {
                    // 未確認
                    r.DefaultCellStyle.BackColor = Color.LightPink;
                }
                else if (r.Cells["状態コード"].Value.ToString().Equals("1"))
                {
                    // 確認
                    r.DefaultCellStyle.BackColor = Color.LightYellow;
                }
                else if (r.Cells["状態コード"].Value.ToString().Equals("2"))
                {
                    // 完了
                    r.DefaultCellStyle.BackColor = Color.LightGray;

                    if (!done_flg)
                    {
//                        r.Visible = false;
                    }
                }
            }
        }

        void ListClear()
        {
            DataTable table = dSet.Tables["患者掲示板"];
            table.Rows.Clear();

            DataView view = new DataView(table);
            ListView1.DataSource = view;
        }
/*
        private void ShowButton1_Click(object sender, EventArgs e)
        {
            this.PtClear();

//            this.stdControl11.PatInfoSet(this.PtIdBox1.Text);
            this.ListShow();
        }
*/
        private void ListView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.DataClear();

            DataGridViewRow r = ListView1.Rows[e.RowIndex];

            this.SEQLabel1.Text = r.Cells["SEQ1"].Value.ToString();
            this.SEQLabel2.Text = r.Cells["SEQ2"].Value.ToString();
            this.CountLabel1.Text = r.Cells["記事数"].Value.ToString();

            if (r.Cells["状態コード"].Value.ToString().Equals("2"))
            {
                this.StatusBox1.Checked = true;
            }

            string SEQ1 = r.Cells["SEQ1"].Value.ToString();

            bool inactive_flg = this.InactiveBox1.Checked;

            if (r.Cells["種別"].Value.ToString().Equals("●"))
            {
                // 指示を表示
                this.KindBox1.Text = r.Cells["タイトル"].Value.ToString();
                this.OrderBox1.Text = r.Cells["記事"].Value.ToString();
                this.OrderDateTimeLabel1.Text = r.Cells["登録日時"].Value.ToString();
                this.OrderStaffCodeLabel1.Text = r.Cells["登録者コード"].Value.ToString();
                this.OrderStaffNameLabel1.Text = r.Cells["登録者"].Value.ToString();
                this.OrderStaffCodeLabel2.Text = r.Cells["代行登録者コード"].Value.ToString();

                // 一連のスレッドの表示・非表示をスイッチする。
                /*
                foreach (DataGridViewRow rr in ListView1.Rows)
                {
                    if (rr.Cells["SEQ1"].Value.ToString().Equals(SEQ1) &&
                        rr.Cells["種別"].Value.ToString().Equals(">"))
                    {
                        if (rr.Visible)
                        {
                            // もともと「表示」の場合は非表示にする。
                            rr.Visible = false;
                        }
                        else
                        {
                            // もともと「非表示」の場合
                            // 「削除フラグ != 1」もしくは「削除表示 = オン」の場合は表示する。
                            if (!rr.Cells["削除フラグ"].Value.ToString().Equals("1") || inactive_flg)
                            {
                                rr.Visible = true;
                            }
                        }
                    }
                }
                */

                // OrderBox1 の中身を確認するため、モード変更は最後に行う必要がある。
                this.ModeChange(Mode.Order);
            }
            else
            {
                // 返信を表示
                this.ReplyBox1.Text = r.Cells["記事"].Value.ToString();
                this.ReplyDateTimeLabel1.Text = r.Cells["登録日時"].Value.ToString();
                this.ReplyStaffCodeLabel1.Text = r.Cells["登録者コード"].Value.ToString();
                this.ReplyStaffNameLabel1.Text = r.Cells["登録者"].Value.ToString();
                this.ReplyStaffCodeLabel2.Text = r.Cells["代行登録者コード"].Value.ToString();

                // 元の指示の表示
                foreach (DataGridViewRow rr in ListView1.Rows)
                {
                    if (rr.Cells["SEQ1"].Value.ToString().Equals(SEQ1) &&
                        rr.Cells["種別"].Value.ToString().Equals("●"))
                    {
                        this.KindBox1.Text = rr.Cells["タイトル"].Value.ToString();
                        this.OrderBox1.Text = rr.Cells["記事"].Value.ToString();
                        this.OrderDateTimeLabel1.Text = rr.Cells["登録日時"].Value.ToString();
                        this.OrderStaffCodeLabel1.Text = rr.Cells["登録者コード"].Value.ToString();
                        this.OrderStaffNameLabel1.Text = rr.Cells["登録者"].Value.ToString();
                        this.OrderStaffCodeLabel2.Text = rr.Cells["代行登録者コード"].Value.ToString();
                        break;
                    }
                }

                // OrderBox1 の中身を確認するため、モード変更は最後に行う必要がある。
                this.ModeChange(Mode.Reply);
            }
        }

        private void InactiveBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.ListFormat();
        }

        private void DoneBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.ListFormat();
        }

        void PtClear()
        {
            this.ListClear();
            this.DataClear();
        }

        void DataClear()
        {
            this.SEQLabel1.Text = "";
            this.SEQLabel2.Text = "";
            this.KindBox1.Text = "";

            this.OrderBox1.Clear();
            this.OrderDateTimeLabel1.Text = "";
            this.OrderStaffCodeLabel1.Text = "";
            this.OrderStaffNameLabel1.Text = "";
            this.OrderStaffCodeLabel2.Text = "";

            this.ReplyBox1.Clear();
            this.ReplyDateTimeLabel1.Text = "";
            this.ReplyStaffCodeLabel1.Text = "";
            this.ReplyStaffNameLabel1.Text = "";
            this.ReplyStaffCodeLabel2.Text = "";

            this.CountLabel1.Text = "";
            this.StatusBox1.Checked = false;
        }

        void ModeChange(Mode mode)
        {
            this.Mode1 = mode;

            if (mode == Mode.None)
            {
                this.OrderBox1.ReadOnly = true;
                this.ReplyBox1.ReadOnly = true;

                this.KindBox1.Enabled = false;
                this.StatusBox1.Enabled = false;

                this.OrderNewButton1.Enabled = true;
                this.OrderSaveButton1.Enabled = false;
                this.OrderDeleteButton1.Enabled = false;

                this.ReplyNewButton1.Enabled = false;
                this.ReplySaveButton1.Enabled = false;
                this.ReplyDeleteButton1.Enabled = false;
            }
            else if (mode == Mode.Order)
            {
                this.OrderBox1.ReadOnly = false;
                this.ReplyBox1.ReadOnly = true;

                this.KindBox1.Enabled = true;
                this.StatusBox1.Enabled = false;

                this.OrderNewButton1.Enabled = true;
                this.OrderSaveButton1.Enabled = true;
                this.OrderDeleteButton1.Enabled = true;

                this.ReplyNewButton1.Enabled = true;
                this.ReplySaveButton1.Enabled = false;
                this.ReplyDeleteButton1.Enabled = false;
            }
            else if (mode == Mode.Reply)
            {
                this.OrderBox1.ReadOnly = true;
                this.ReplyBox1.ReadOnly = false;

                this.KindBox1.Enabled = false;
                this.StatusBox1.Enabled = true;

                this.OrderNewButton1.Enabled = true;
                this.OrderSaveButton1.Enabled = false;
                this.OrderDeleteButton1.Enabled = false;

                this.ReplyNewButton1.Enabled = true;
                this.ReplySaveButton1.Enabled = true;
                this.ReplyDeleteButton1.Enabled = true;
            }

            // 指示が入っていない時は、返信を入力できないようにする
            if (this.OrderBox1.Text.Length == 0)
            {
                this.ReplyNewButton1.Enabled = false;
                this.ReplySaveButton1.Enabled = false;
                this.ReplyDeleteButton1.Enabled = false;
            }
        }

        private void OrderNewButton1_Click(object sender, EventArgs e)
        {
            // 全データをクリア
            this.DataClear();

            this.ModeChange(Mode.Order);
        }

        private void ReplyNewButton1_Click(object sender, EventArgs e)
        {
            // 種別・掲示連番・元の指示は残して
            // 連番・返信記事をクリア
            this.SEQLabel2.Text = "";
            this.ReplyBox1.Clear();
            this.ReplyDateTimeLabel1.Text = "";
            this.ReplyStaffNameLabel1.Text = "";

            this.ModeChange(Mode.Reply);
        }

        private void StatusBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.StatusBox1.Checked)
            {
//                this.ReplyBox1.Text += "完了しました";
            }
        }

        /// <summary>
        /// 指示を登録する
        /// </summary>
        StdReturn OrderSave()
        {
            StdReturn sr = new StdReturn();

            if (KindBox1.Text.Length == 0)
            {
                sr.Errs.Add("区分が選択されていません");
            }

            if (OrderBox1.Text.Length == 0)
            {
                sr.Errs.Add("指示が入力されていません");
            }

            if (sr.ErrExist)
            {
                return sr;
            }

            if (MessageBox.Show("登録します。よろしいですか？", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                // 登録する

                PatBoard obj = new PatBoard();
                obj.PtId = this.Pat.Id;
                int.TryParse(this.SEQLabel1.Text, out obj.SEQ1);
                obj.SEQ2 = 1;
                obj.KindCode1 = PatBoard.GetKindCodeFromName(this.KindBox1.Text);
                obj.Cont1 = this.OrderBox1.Text;
                obj.DeleteFlg = 0;

                if (this.StatusBox1.Checked)
                {
                    obj.StatusCode = "2";
                }
                else
                {
                    // 完了でない場合は記事数によって判断する（PatBoard クラスで処理）
                }

                sr = obj.OrderSave();

                this.ListShow();
            }

            return sr;
        }

        /// <summary>
        /// 指示を削除する
        /// </summary>
        StdReturn OrderDelete()
        {
            StdReturn sr = new StdReturn();

            if (this.SEQLabel1.Text.Length == 0)
            {
                sr.Errs.Add("指示が選択されていません");
            }

            if (sr.ErrExist)
            {
                return sr;
            }

            int count = 0;
            int.TryParse(this.CountLabel1.Text, out count);

            if (count > 0)
            {
                // 返信がある場合
                if (MessageBox.Show("返信が登録されています。本当に削除しますか？", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    PatBoard obj = new PatBoard();
                    obj.PtId = this.Pat.Id;
                    int.TryParse(this.SEQLabel1.Text, out obj.SEQ1);
                    obj.SEQ2 = 1;
                    obj.DeleteFlg = 1;

                    sr = obj.OrderDelete();

                    this.ListShow();
                }
            }
            else
            {
                // 返信が無い場合
                if (MessageBox.Show("削除します。よろしいですか？", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    PatBoard obj = new PatBoard();
                    obj.PtId = this.Pat.Id;
                    int.TryParse(this.SEQLabel1.Text, out obj.SEQ1);
                    obj.SEQ2 = 1;
                    obj.DeleteFlg = 1;

                    sr = obj.OrderDelete();

                    this.ListShow();
                }
            }

            return sr;
        }

        /// <summary>
        /// 返信を登録する
        /// </summary>
        StdReturn ReplySave()
        {
            StdReturn sr = new StdReturn();

            if (this.SEQLabel1.Text.Length == 0)
            {
                sr.Errs.Add("指示が選択されていません");
            }

            if (this.ReplyBox1.Text.Length == 0)
            {
                sr.Errs.Add("返信が入力されていません");
            }

            if (sr.ErrExist)
            {
                return sr;
            }

            if (MessageBox.Show("登録します。よろしいですか？", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                // 登録する

                PatBoard obj = new PatBoard();
                obj.PtId = this.Pat.Id;
                int.TryParse(this.SEQLabel1.Text, out obj.SEQ1);
                int.TryParse(this.SEQLabel2.Text, out obj.SEQ2);
                obj.KindCode1 = PatBoard.GetKindCodeFromName(this.KindBox1.Text);
                obj.Cont1 = this.ReplyBox1.Text;
                obj.DeleteFlg = 0;

                if (this.StatusBox1.Checked)
                {
                    obj.StatusCode = "2";
                }
                else
                {
                    // 完了でない場合は記事数によって判断する（PatBoard クラスで処理）
                }

                sr = obj.ReplySave();

                this.ListShow();
            }

            return sr;
        }

        /// <summary>
        /// 該当の返信を削除する
        /// </summary>
        StdReturn ReplyDelete()
        {
            StdReturn sr = new StdReturn();

            if (this.SEQLabel1.Text.Length == 0)
            {
                sr.Errs.Add("指示が選択されていません");
            }

            if (this.SEQLabel2.Text.Length == 0)
            {
                sr.Errs.Add("返信が選択されていません");
            }

            if (sr.ErrExist)
            {
                return sr;
            }

            if (MessageBox.Show("削除します。よろしいですか？", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                // 返信を削除する

                PatBoard obj = new PatBoard();
                obj.PtId = this.Pat.Id;
                int.TryParse(this.SEQLabel1.Text, out obj.SEQ1);
                int.TryParse(this.SEQLabel2.Text, out obj.SEQ2);
                obj.DeleteFlg = 1;

                sr = obj.ReplyDelete();

                this.ListShow();
            }

            return sr;
        }

        private void BaseOrderButton1_Click(object sender, EventArgs e)
        {
            if (this.MdiParent is FormPat)
            {
                ((FormPat)(this.MdiParent)).FormBaseOrder_Show();
            }
        }

        private void OpeOrderButton1_Click(object sender, EventArgs e)
        {
            if (this.MdiParent is FormPat)
            {
                ((FormPat)(this.MdiParent)).FormOpeOrder_Show();
            }
        }

        private void NursingOrderButton1_Click(object sender, EventArgs e)
        {
            if (this.MdiParent is FormPat)
            {
                ((FormPat)(this.MdiParent)).FormNursingOrder_Show();
            }
        }

        private void OrderSaveButton1_Click(object sender, EventArgs e)
        {
            this.Msg1(this.OrderSave());
        }

        private void OrderDeleteButton1_Click(object sender, EventArgs e)
        {
            this.Msg1(this.OrderDelete());
        }

        private void ReplySaveButton1_Click(object sender, EventArgs e)
        {
            this.Msg1(this.ReplySave());
        }

        private void ReplyDeleteButton1_Click(object sender, EventArgs e)
        {
            this.Msg1(this.ReplyDelete());
        }
    }
}
