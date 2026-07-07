using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class FormDiagSearch : Form
    {
        string ListViewSort = "";
        SortOrder ListViewSortOrder = SortOrder.Ascending;

        DataSet DSet = new DataSet();

        ContextMenuStrip _MenuStrip = new ContextMenuStrip();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormDiagSearch()
        {
            InitializeComponent();

            DataTable table = DSet.Tables.Add("Data");
            table.Columns.Add("ID");
            table.Columns.Add("氏名");
            table.Columns.Add("性別");
            table.Columns.Add("生年月日");
            table.Columns.Add("年齢");
            table.Columns.Add("主");
            table.Columns.Add("コード");
            table.Columns.Add("病名");
            table.Columns.Add("入外");
            table.Columns.Add("登録日");
            table.Columns.Add("開始日");
            table.Columns.Add("転帰");
            table.Columns.Add("転帰日");
            table.Columns.Add("検査");
            table.Columns.Add("検査連番", typeof(int));
            table.Columns.Add("検査日");
            table.Columns.Add("検査結果");
            table.Columns.Add("検査基準値");
            table.Columns.Add("検査判定");
            table.Columns.Add("Obj1", typeof(Diag));
            table.Columns.Add("Obj2", typeof(KensaData));

            this.DiagDateButton1.Checked = true;

            foreach (Dept d in Dict.DeptDict.Values)
            {
                if (d.Code <= 0) continue;
                
                DeptListBox.Items.Add(d);
            }

            ToolStripMenuItem item = new ToolStripMenuItem("カルテ");
            item.Click += new EventHandler(item_Click);
            this._MenuStrip.Items.Add(item);

            this.ListView.ContextMenuStrip = this._MenuStrip;
        }

        void item_Click(object sender, EventArgs e)
        {
            if (this.ListView.SelectedCells.Count > 0)
            {
                DataGridViewRow r = this.ListView.Rows[this.ListView.SelectedCells[0].RowIndex];
                FormControl.FormPat_Show(PatBase.Load(r.Cells["ID"].Value.ToString()), true);
            }
        }

        private void FormDiagSearch_Load(object sender, EventArgs e)
        {
            DatePicker1.Value = DateTime.Now.AddYears(-1);

            // 科ごとに初期設定する検査項目
            switch (LoginUser.DeptId)
            {
                case "1":
                    DiagBox1.Items.Add("2500013: 糖尿病");
                    DiagBox2.Items.Add("2500013: 糖尿病");
                    DiagBox3.Items.Add("2500013: 糖尿病");

                    KensaBox1.Items.Add("HbA1c");
                    KensaBox2.Items.Add("HbA1c");
                    KensaBox3.Items.Add("HbA1c");

                    KensaBox1.Items.Add("11083: アルブミン/Cre補正");
                    KensaBox2.Items.Add("11083: アルブミン/Cre補正");
                    KensaBox3.Items.Add("11083: アルブミン/Cre補正");

                    KensaBox1.Items.Add("1450: TP/CRE");
                    KensaBox2.Items.Add("1450: TP/CRE");
                    KensaBox3.Items.Add("1450: TP/CRE");

                    KensaBox1.Items.Add("413: クレアチニン");
                    KensaBox2.Items.Add("413: クレアチニン");
                    KensaBox3.Items.Add("413: クレアチニン");

                    KensaBox1.Items.Add("538: 推定GRF");
                    KensaBox2.Items.Add("538: 推定GRF");
                    KensaBox3.Items.Add("538: 推定GRF");

                    break;

                case "2":
                    DiagBox1.Items.Add("大腸癌");
                    DiagBox2.Items.Add("大腸癌");
                    DiagBox3.Items.Add("大腸癌");

                    DiagBox1.Items.Add("直腸癌");
                    DiagBox2.Items.Add("直腸癌");
                    DiagBox3.Items.Add("直腸癌");

                    DiagBox1.Items.Add("胃癌");
                    DiagBox2.Items.Add("胃癌");
                    DiagBox3.Items.Add("胃癌");

                    KensaBox1.Items.Add("CEA");
                    KensaBox2.Items.Add("CEA");
                    KensaBox3.Items.Add("CEA");

                    break;

                case "7":
                    DiagBox1.Items.Add("網膜症");
                    DiagBox2.Items.Add("網膜症");
                    DiagBox3.Items.Add("網膜症");

                    DiagBox1.Items.Add("白内障");
                    DiagBox2.Items.Add("白内障");
                    DiagBox3.Items.Add("白内障");

                    DiagBox1.Items.Add("緑内障");
                    DiagBox2.Items.Add("緑内障");
                    DiagBox3.Items.Add("緑内障");

                    DiagBox1.Items.Add("糖尿病");
                    DiagBox2.Items.Add("糖尿病");
                    DiagBox3.Items.Add("糖尿病");

                    KensaBox1.Items.Add("HbA1c");
                    KensaBox2.Items.Add("HbA1c");
                    KensaBox3.Items.Add("HbA1c");

                    break;

                default:
                    DiagBox1.Items.Add("2500013: 糖尿病");
                    DiagBox2.Items.Add("2500013: 糖尿病");
                    DiagBox3.Items.Add("2500013: 糖尿病");

                    KensaBox1.Items.Add("HbA1c");
                    KensaBox2.Items.Add("HbA1c");
                    KensaBox3.Items.Add("HbA1c");

                    KensaBox1.Items.Add("11083: アルブミン/Cre補正");
                    KensaBox2.Items.Add("11083: アルブミン/Cre補正");
                    KensaBox3.Items.Add("11083: アルブミン/Cre補正");

                    KensaBox1.Items.Add("1450: TP/CRE");
                    KensaBox2.Items.Add("1450: TP/CRE");
                    KensaBox3.Items.Add("1450: TP/CRE");

                    KensaBox1.Items.Add("413: クレアチニン");
                    KensaBox2.Items.Add("413: クレアチニン");
                    KensaBox3.Items.Add("413: クレアチニン");

                    KensaBox1.Items.Add("538: 推定GRF");
                    KensaBox2.Items.Add("538: 推定GRF");
                    KensaBox3.Items.Add("538: 推定GRF");

                    break;
            }
        }

        void ListShow()
        {
            if (DatePicker1.Value > DatePicker2.Value)
            {
                MessageBox.Show("開始日が終了日より後になっています");
                return;
            }

            if (DatePicker1.Value.AddYears(1) < DatePicker2.Value)
            {
                MessageBox.Show("開始日と終了日の間は１年以内にしてください");
                return;
            }

            string diag1 = DiagBox1.Text.Contains(":") ? DiagBox1.Text.Split(':')[0].Trim() : DiagBox1.Text.Trim();
            string diag2 = DiagBox2.Text.Contains(":") ? DiagBox2.Text.Split(':')[0].Trim() : DiagBox2.Text.Trim();
            string diag3 = DiagBox3.Text.Contains(":") ? DiagBox3.Text.Split(':')[0].Trim() : DiagBox3.Text.Trim();

            // １文字でも検索できるようにしてほしい by 山本補佐, 関口さん 2019/12/23
/*
            if ((diag1.Length > 0 && diag1.Length < 2) ||
                (diag2.Length > 0 && diag2.Length < 2) ||
                (diag3.Length > 0 && diag3.Length < 2))
            {
                MessageBox.Show("病名は２文字以上入力してください");
                return;
            }
*/
            bool diag = diag1.Length + diag2.Length + diag3.Length > 0 ? true : false;

            string kensa1 = KensaBox1.Text.Contains(":") ? KensaBox1.Text.Split(':')[0].Trim() : KensaBox1.Text.Trim();
            string kensa2 = KensaBox2.Text.Contains(":") ? KensaBox2.Text.Split(':')[0].Trim() : KensaBox2.Text.Trim();
            string kensa3 = KensaBox3.Text.Contains(":") ? KensaBox3.Text.Split(':')[0].Trim() : KensaBox3.Text.Trim();

            if ((kensa1.Length > 0 && kensa1.Length < 2) ||
                (kensa2.Length > 0 && kensa2.Length < 2) ||
                (kensa3.Length > 0 && kensa3.Length < 2))
            {
                MessageBox.Show("検査は２文字以上入力してください");
                return;
            }

            bool kensa = kensa1.Length + kensa2.Length + kensa3.Length > 0 ? true : false;

            if (!diag && !kensa)
            {
                MessageBox.Show("病名または検査のいずれかを入力してください");
                return;
            }

            DataTable table = DSet.Tables["Data"];
            table.Clear();

            int d1 = int.Parse(DatePicker1.Value.ToString("yyyyMMdd"));
            int d2 = int.Parse(DatePicker2.Value.ToString("yyyyMMdd"));

            // 患者リスト
            List<string> pts = new List<string>();

            // 病名リスト
            List<string> diags = new List<string>();
            if (diag1.Length > 0) diags.Add(diag1);
            if (diag2.Length > 0 && !diags.Contains(diag2)) diags.Add(diag2);
            if (diag3.Length > 0 && !diags.Contains(diag3)) diags.Add(diag3);

            // 検査リスト
            List<string> kensas = new List<string>();
            if (kensa1.Length > 0) kensas.Add(kensa1);
            if (kensa2.Length > 0 && !kensas.Contains(kensa2)) kensas.Add(kensa2);
            if (kensa3.Length > 0 && !kensas.Contains(kensa3)) kensas.Add(kensa3);

            // １文字の病名があり、かつ、検査が入っている場合は
            // 病名のみ検索する（検査は検索しない）
            if ((diag1.Length == 1 || diag2.Length == 1 || diag3.Length == 1) && kensas.Count > 0)
            {
                MessageBox.Show("１文字の病名を検索する場合は、件数が多くなりすぎて高負荷がかかりますので、検査は検索対象から外します");
                kensas.Clear();
            }

            // 診療科リスト
            List<string> depts = new List<string>();

            // 最終受診日カラムがある場合はいったん削除する
            if (table.Columns.Count >= 22)
            {
                for (int c = 21; c < table.Columns.Count; c++)
                {
                    table.Columns.RemoveAt(c);
                }
            }

            foreach (Dept d in DeptListBox.CheckedItems)
            {
                // カラムがなければ追加する
                if (!table.Columns.Contains(d.ShortName))
                {
                    table.Columns.Add(d.ShortName);
                }

                depts.Add(d.Code.ToString());
            }

            List<Diag> diag_list = new List<Diag>();
            List<KensaData> kensa_list = new List<KensaData>();

            Dictionary<string, PatBase> pt_dict = new Dictionary<string, PatBase>();
            Dictionary<string, List<PatDept>> dept_dict = new Dictionary<string, List<PatDept>>();

            if (diag)
            {
                diag_list = Diag.GetListByDiagNamesDates(diags, d1, d2, DoubtBox.Checked, false, this.DiagDateButton3.Checked ? 3 : this.DiagDateButton2.Checked ? 2 : 1);

                foreach (Diag d in diag_list)
                {
                    // 削除されたものは飛ばす
                    if (d.DeleteFlg) continue;

                    // 未確定のものは飛ばす
                    if (!d.FixFlg) continue;

                    if (!pts.Contains(d.PtId))
                    {
                        pts.Add(d.PtId);
                    }
                }
            }

            if (kensa)
            {
                kensa_list = KensaData.GetListByKensaNamesDates(kensas, d1, d2);

                // 患者情報の検索は、病名検索しない場合のみ行う
                if (DiagBox1.Text.Trim().Length == 0)
                {
                    foreach (KensaData k in kensa_list)
                    {
                        if (!pts.Contains(k.PtId))
                        {
                            pts.Add(k.PtId);
                        }
                    }
                }
            }

            // 「検査していない人を除く」チェックボックス
            this.FilterBox1.Enabled = kensa;

            // 対象者が 1000 人を超えると検索できない（Oracleの制限）
            if (pts.Count > 1000)
            {
//                MessageBox.Show("対象者の上限は1000人ですが、この条件では " + pts.Count + " 人となるため検索できません。検索条件を変更して、対象者を減らして頂きますようお願い致します。");
//                return;
            }

            // 患者辞書をセット
            foreach (PatBase pt in PatBase.GetList(pts))
            {
                if (!pt_dict.ContainsKey(pt.Id))
                {
                    pt_dict.Add(pt.Id, pt);
                }
            }

            // 最終受診日をセット
            if (depts.Count > 0)
            {
                foreach (PatDept pd in PatDept.GetListByPatsDepts(pts, depts))
                {
                    if (!dept_dict.ContainsKey(pd.Id))
                    {
                        List<PatDept> dept_list = new List<PatDept>();
                        dept_list.Add(pd);
                        dept_dict.Add(pd.Id, dept_list);
                    }
                    else
                    {
                        dept_dict[pd.Id].Add(pd);
                    }
                }
            }

            // 検査の総数
            int ks = 0;

            // 同一患者の検査連番
            int i = 0;
            PatBase p = new PatBase();

            if (diag)
            {
                // 病名検索する場合

                foreach (Diag d in diag_list)
                {
                    // 削除されたものは飛ばす
                    if (d.DeleteFlg) continue;

                    // 未確定のものは飛ばす
                    if (!d.FixFlg) continue;

                    // 同一患者の検査連番
                    i = 0;

                    if (pt_dict.ContainsKey(d.PtId))
                    {
                        p = pt_dict[d.PtId];
                    }
                    else
                    {
                        p = new PatBase();
                        p.Id = d.PtId;
                    }

                    DataRow r = table.NewRow();

                    r["ID"] = p.Id;
                    r["氏名"] = p.Name;
                    r["性別"] = p.SexNameShort;
                    r["生年月日"] = p.BirthString;
                    r["年齢"] = p.AgeCalc(d.StartDate.ToString());
                    r["主"] = d.MainFlg ? "●" : "";
                    r["コード"] = d.DiagCode;
                    r["病名"] = d.DiagName;
                    r["入外"] = d.InOutStringShort;
                    r["登録日"] = DateTimeAgent.DateFormat(d.RegDate, DateTimeAgent.DateFormatKind.LONG);
                    r["開始日"] = DateTimeAgent.DateFormat(d.StartDate, DateTimeAgent.DateFormatKind.LONG);
                    r["転帰"] = d.OutcomeString;
                    r["転帰日"] = DateTimeAgent.DateFormat(d.OutcomeDate, DateTimeAgent.DateFormatKind.LONG);
                    r["Obj1"] = d;

                    // 最終受診日をセット
                    if (dept_dict.ContainsKey(p.Id))
                    {
                        foreach (PatDept pd in dept_dict[p.Id])
                        {
                            if (r[pd.DeptName] != null)
                            {
                                r[pd.DeptName] = DateTimeAgent.DateFormat(pd.LastDate, DateTimeAgent.DateFormatKind.LONG);
                            }
                        }
                    }

                    foreach (KensaData k in kensa_list)
                    {
                        if (!k.PtId.Equals(d.PtId)) continue;

                        // 検査件数インクリメント
                        ks++;
                        i++;

                        // 同一患者で２件目以降ならば行を追加する
                        if (i >= 2)
                        {
                            r = table.NewRow();

                            r["ID"] = p.Id;
                            r["氏名"] = p.Name;
                            r["性別"] = p.SexNameShort;
                            r["生年月日"] = p.BirthString;
                            r["主"] = d.MainFlg ? "●" : "";
                            r["コード"] = d.DiagCode;
                            r["病名"] = d.DiagName;
                            r["入外"] = d.InOutStringShort;
                            r["登録日"] = DateTimeAgent.DateFormat(d.RegDate, DateTimeAgent.DateFormatKind.LONG);
                            r["開始日"] = DateTimeAgent.DateFormat(d.StartDate, DateTimeAgent.DateFormatKind.LONG);
                            r["転帰"] = d.OutcomeString;
                            r["転帰日"] = DateTimeAgent.DateFormat(d.OutcomeDate, DateTimeAgent.DateFormatKind.LONG);
                            r["Obj1"] = d;

                            // 最終受診日をセット
                            if (dept_dict.ContainsKey(p.Id))
                            {
                                foreach (PatDept pd in dept_dict[p.Id])
                                {
                                    if (r[pd.DeptName] != null)
                                    {
                                        r[pd.DeptName] = DateTimeAgent.DateFormat(pd.LastDate, DateTimeAgent.DateFormatKind.LONG);
                                    }
                                }
                            }
                        }

                        // 検査日時点の年齢に置き換える
                        r["年齢"] = p.AgeCalc(k.KensaDate.ToString());

                        r["検査"] = k.KensaName;
                        r["検査連番"] = i;
                        r["検査日"] = DateTimeAgent.DateFormat(k.KensaDate, DateTimeAgent.DateFormatKind.LONG);
                        r["検査結果"] = k.Result;
                        r["検査基準値"] = k.Normal;

                        if (k.ModFlg.Equals(1))
                        {
                            r["検査判定"] = "L";
                        }
                        else if (k.ModFlg.Equals(2))
                        {
                            r["検査判定"] = "H";
                        }

                        r["Obj2"] = k;

                        table.Rows.Add(r);
                    }

                    // 検査がなければ行を追加
                    if (i == 0)
                    {
                        table.Rows.Add(r);
                    }
                }
            }
            else if (kensa)
            {
                // 検査検索のみの場合
                ks = kensa_list.Count;

                foreach (KensaData k in kensa_list)
                {
                    DataRow r = table.NewRow();

                    if (!p.Id.Equals(k.PtId))
                    {
                        if (pt_dict.ContainsKey(k.PtId))
                        {
                            p = pt_dict[k.PtId];
                        }
                        else
                        {
                            p = new PatBase();
                            p.Id = k.PtId;
                        }

                        i = 0;
                    }

                    // 検査件数インクリメント
                    i++;

                    r["ID"] = p.Id;
                    r["氏名"] = p.Name;
                    r["性別"] = p.SexNameShort;
                    r["生年月日"] = p.BirthString;
                    r["年齢"] = p.AgeCalc(k.KensaDate.ToString());
                    r["検査"] = k.KensaName;
                    r["検査連番"] = i;
                    r["検査日"] = DateTimeAgent.DateFormat(k.KensaDate, DateTimeAgent.DateFormatKind.LONG);
                    r["検査結果"] = k.Result;
                    r["検査基準値"] = k.Normal;

                    if (k.ModFlg.Equals(1))
                    {
                        r["検査判定"] = "L";
                    }
                    else if (k.ModFlg.Equals(2))
                    {
                        r["検査判定"] = "H";
                    }

                    r["Obj2"] = k;

                    // 最終受診日をセット
                    if (dept_dict.ContainsKey(p.Id))
                    {
                        foreach (PatDept pd in dept_dict[p.Id])
                        {
                            if (r[pd.DeptName] != null)
                            {
                                r[pd.DeptName] = DateTimeAgent.DateFormat(pd.LastDate, DateTimeAgent.DateFormatKind.LONG);
                            }
                        }
                    }

                    table.Rows.Add(r);
                }
            }

            this.ListFormat();
        }

        void ListFormat()
        {
            if (!DSet.Tables.Contains("Data"))
            {
                return;
            }

            DataTable table = DSet.Tables["Data"];
            DataView view = new DataView(table);

            ListView.DataSource = view;

            List<string> filters = new List<string>();

            // 検査していない人を除く
            if (this.FilterBox1.Checked)
            {
                filters.Add("(検査 is not null)");
            }

            if (filters.Count > 0)
            {
                view.RowFilter = AppString.ConcatList(filters, " and ");
            }

            if (this.ListViewSort.Length > 0)
            {
                view.Sort = this.ListViewSort;

                if (this.ListViewSortOrder == SortOrder.Descending)
                {
                    view.Sort += " desc";
                }
            }

            ListView.Columns["ID"].Width = 65;
            ListView.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            ListView.Columns["氏名"].Width = 80;
            ListView.Columns["氏名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            ListView.Columns["性別"].Width = 30;
            ListView.Columns["性別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView.Columns["性別"].Visible = false;

            ListView.Columns["生年月日"].Width = 75;
            ListView.Columns["生年月日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView.Columns["年齢"].Width = 30;
            ListView.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView.Columns["主"].Width = 30;
            ListView.Columns["主"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView.Columns["コード"].Width = 55;
            ListView.Columns["コード"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView.Columns["病名"].Width = 120;
            ListView.Columns["病名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            ListView.Columns["入外"].Width = 30;
            ListView.Columns["入外"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView.Columns["登録日"].Width = 75;
            ListView.Columns["登録日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView.Columns["開始日"].Width = 75;
            ListView.Columns["開始日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView.Columns["転帰"].Width = 40;
            ListView.Columns["転帰"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView.Columns["転帰日"].Width = 75;
            ListView.Columns["転帰日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView.Columns["検査"].Width = 90;
            ListView.Columns["検査"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            ListView.Columns["検査連番"].Width = 30;
            ListView.Columns["検査連番"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView.Columns["検査日"].Width = 75;
            ListView.Columns["検査日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView.Columns["検査結果"].Width = 70;
            ListView.Columns["検査結果"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView.Columns["検査基準値"].Width = 80;
            ListView.Columns["検査基準値"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView.Columns["検査判定"].Width = 30;
            ListView.Columns["検査判定"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView.Columns["Obj1"].Visible = false;
            ListView.Columns["Obj2"].Visible = false;

            // 最終受診日カラムがある場合はいったん削除する
            if (table.Columns.Count >= 22)
            {
                for (int c = 21; c < table.Columns.Count; c++)
                {
                    ListView.Columns[c].Width = 75;
                    ListView.Columns[c].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }

            List<string> pts = new List<string>();
            List<Diag> diag_list = new List<Diag>();
            List<KensaData> kensa_list = new List<KensaData>();

            foreach (DataGridViewRow r in ListView.Rows)
            {
                Diag d = (r.Cells["Obj1"].Value != null && r.Cells["Obj1"].Value is Diag) ? (Diag)r.Cells["Obj1"].Value : null;
                KensaData k = (r.Cells["Obj2"].Value != null && r.Cells["Obj2"].Value is KensaData) ? (KensaData)r.Cells["Obj2"].Value : null;

                if (!pts.Contains(r.Cells["ID"].Value.ToString())) pts.Add(r.Cells["ID"].Value.ToString());

                if (d != null && diag_list.FindAll((x) => { return x.PtId.Equals(d.PtId) && x.DiagName.Equals(d.DiagName) && x.RegDate.Equals(d.RegDate); }).Count == 0)
                {
                    diag_list.Add(d);
                }

                if (k != null && kensa_list.FindAll((x) => { return x.PtId.Equals(k.PtId) && x.KensaName.Equals(k.KensaName) && x.KensaDate.Equals(k.KensaDate); }).Count == 0)
                {
                    kensa_list.Add(k);
                }

                if (r.Cells["性別"].Value.ToString().Equals("女"))
                {
                    r.Cells["氏名"].Style.ForeColor = Color.Red;
                    r.Cells["性別"].Style.ForeColor = Color.Red;
                }

                if (r.Cells["検査判定"].Value.ToString().Equals("H"))
                {
                    r.Cells["検査結果"].Style.ForeColor = Color.Red;
                    r.Cells["検査結果"].Style.BackColor = Color.FromArgb(255, 224, 224);
                    r.Cells["検査判定"].Style.ForeColor = Color.Red;
                }
                else if (r.Cells["検査判定"].Value.ToString().Equals("L"))
                {
                    r.Cells["検査結果"].Style.ForeColor = Color.Blue;
                    r.Cells["検査結果"].Style.BackColor = Color.FromArgb(224, 224, 255);
                    r.Cells["検査判定"].Style.ForeColor = Color.Blue;
                }


            }

            string s = pts.Count + "名 / 全データ " + this.ListView.Rows.Count + "件";

            if (diag_list.Count > 0)
            {
                s += " / 病名 " + diag_list.Count + "件";
            }

            if (kensa_list.Count > 0)
            {
                s += " / 検査 " + kensa_list.Count + "件";
            }

            NumLabel.Text = s;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.ListShow();

            Cursor.Current = Cursors.Default;
        }

        private void excelButton_Click(object sender, EventArgs e)
        {
            DataGridView view = ListView;

            if (view.Rows.Count == 0)
            {
                MessageBox.Show("出力対象のデータがありません");
                return;
            }

            // ファイル名
            string file_prefix = "";

            if (DiagBox1.Text.Trim().Length > 0)
            {
                file_prefix += DiagBox1.Text.Contains(":") ? DiagBox1.Text.Split(':')[0].Trim() : DiagBox1.Text.Trim();
            }

            if (DiagBox2.Text.Trim().Length > 0)
            {
                if (file_prefix.Length > 0)
                {
                    file_prefix += "_";
                }

                file_prefix += DiagBox2.Text.Contains(":") ? DiagBox2.Text.Split(':')[0].Trim() : DiagBox2.Text.Trim();
            }

            if (DiagBox3.Text.Trim().Length > 0)
            {
                if (file_prefix.Length > 0)
                {
                    file_prefix += "_";
                }

                file_prefix += DiagBox3.Text.Contains(":") ? DiagBox3.Text.Split(':')[0].Trim() : DiagBox3.Text.Trim();
            }

            if (KensaBox1.Text.Trim().Length > 0)
            {
                if (file_prefix.Length > 0)
                {
                    file_prefix += "_";
                }

                file_prefix += KensaBox1.Text.Contains(":") ? KensaBox1.Text.Split(':')[0].Trim() : KensaBox1.Text.Trim();
            }

            if (KensaBox2.Text.Trim().Length > 0)
            {
                if (file_prefix.Length > 0)
                {
                    file_prefix += "_";
                }

                file_prefix += KensaBox2.Text.Contains(":") ? KensaBox2.Text.Split(':')[0].Trim() : KensaBox2.Text.Trim();
            }

            if (KensaBox3.Text.Trim().Length > 0)
            {
                if (file_prefix.Length > 0)
                {
                    file_prefix += "_";
                }

                file_prefix += KensaBox3.Text.Contains(":") ? KensaBox3.Text.Split(':')[0].Trim() : KensaBox3.Text.Trim();
            }

            //SaveFileDialogクラスのインスタンスを作成
            SaveFileDialog sfd = new SaveFileDialog();

            //はじめのファイル名を指定する
            sfd.FileName = file_prefix + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";

            //はじめに表示されるフォルダを指定する
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //[ファイルの種類]に表示される選択肢を指定する
            sfd.Filter =
                "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";

            //[ファイルの種類]ではじめに
            //「すべてのファイル」が選択されているようにする
            sfd.FilterIndex = 1;

            //タイトルを設定する
            sfd.Title = "保存先のファイルを選択してください";

            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            sfd.RestoreDirectory = true;

            //既に存在するファイル名を指定したとき警告する
            //デフォルトでTrueなので指定する必要はない
            sfd.OverwritePrompt = true;

            //存在しないパスが指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            sfd.CheckPathExists = true;

            //ダイアログを表示する
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                // CSVデータを生成する
                List<List<string>> list = new List<List<string>>();

                // 先頭行
                List<string> ss = new List<string>();

                foreach (DataGridViewColumn c in view.Columns)
                {
                    if (c.Visible)
                    {
                        ss.Add(c.HeaderText);
                    }
                }

                list.Add(ss);

                // レコード行
                foreach (DataGridViewRow r in view.Rows)
                {
                    ss = new List<string>();

                    foreach (DataGridViewCell c in r.Cells)
                    {
                        if (c.Visible)
                        {
                            string str = "";
                            if (c.Value != null)
                            {
                                str = c.Value.ToString();
                            }
                            ss.Add(str);
                        }
                    }

                    list.Add(ss);
                }

                //                MessageBox.Show(sfd.FileName);

                CsvWriter writer = new CsvWriter(sfd.FileName);
                writer.Write(list);

                writer.Dispose();
            }
        }

        private void ListView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow r = this.ListView.Rows[e.RowIndex];
                FormControl.FormPat_Show(PatBase.Load(r.Cells["ID"].Value.ToString()), true);
            }
        }

        private void ListView_Sorted(object sender, EventArgs e)
        {
            this.ListViewSort = this.ListView.SortedColumn.Name;
            this.ListViewSortOrder = this.ListView.SortOrder;

            this.ListFormat();
        }

        private void FilterBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.ListFormat();
        }
    }
}