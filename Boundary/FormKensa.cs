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
    public partial class FormKensa : StdForm1
    {
        DataSet dSet = new DataSet();

        public FormKensa()
        {
            InitializeComponent();

            // 検査結果テーブル
            dSet.Tables.Add("KensaResult");

            // 異常結果テーブル
            dSet.Tables.Add("KensaMod");

            KensaDataView1.CellMouseEnter += new DataGridViewCellEventHandler(KensaDataView1_CellMouseEnter);
            KensaDataView1.CellMouseLeave += new DataGridViewCellEventHandler(KensaDataView1_CellMouseLeave);
        }

        private void FormKensa_Load(object sender, EventArgs e)
        {
            // 初期表示後、これをしないと異常値の色が変わらない
            this.DataFormat();
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            this.DataShow();
        }

        void DataShow()
        {
            if (!dSet.Tables.Contains("KensaResult") || !dSet.Tables.Contains("KensaMod"))
            {
                return;
            }

            string pt_id = this.Pat.Id;

            // 検査日一覧をクリアする
            KensaDatePanel.Controls.Clear();

            // 検査結果一覧をクリアする
            DataTable table1 = dSet.Tables["KensaResult"];
            table1.Columns.Clear();
            table1.Rows.Clear();

            // 異常結果一覧をクリアする
            DataTable table2 = dSet.Tables["KensaMod"];
            table2.Columns.Clear();
            table2.Rows.Clear();


            // 検査項目一覧を取得する
            Dictionary<string, KensaMaster> master_dict = KensaMaster.GetDict(pt_id);

            // 検査結果一覧を取得する
            Dictionary<string, Dictionary<string, KensaData>> data_dict = KensaData.GetDict(pt_id);


            table1.Columns.Add("項目");
            table1.Columns.Add("基準値");
            table1.Columns.Add("単位");

            table2.Columns.Add("項目");
            table2.Columns.Add("基準値");
            table2.Columns.Add("単位");


            int h = 0;
            string tmp_year = "";

            // 検査日一覧のチェックボックスと、検査結果の検査日カラムを生成する
            foreach (string d in data_dict.Keys)
            {
                // KensaDatePanel に検査日を生成

                // 年ラベルを作成する
                if (!tmp_year.Equals(d.Substring(0, 4)))
                {
                    h += 10;
                    tmp_year = d.Substring(0, 4);
                    Label lb = new Label();
                    lb.AutoSize = true;
                    lb.Text = tmp_year + " 年";
                    lb.TextAlign = ContentAlignment.MiddleLeft;
                    lb.Location = new Point(10, h);
                    KensaDatePanel.Controls.Add(lb);
                    h += 15;
                }

                // 検査日のチェックボックスを生成
                CheckBox c = new CheckBox();
                c.Name = AppDateTime.DateStringFromString(d);
                c.Text = AppDateTime.DateStringFromString(d);
                c.Location = new Point(20, h);

                // 初期状態で直近４回分まで表示されている
                if (h < 100)
                {
                    c.Checked = true;
                }

                c.CheckedChanged += new EventHandler(c_CheckedChanged);
                KensaDatePanel.Controls.Add(c);
                h += 20;


                // KensaDataView に検査日のカラムを生成
                table1.Columns.Add(AppDateTime.DateStringFromString(d));
                table2.Columns.Add(AppDateTime.DateStringFromString(d));
            }


            // KensaDataView に検査項目ごとの結果行を生成
            foreach (string m_key in master_dict.Keys)
            {
                DataRow r1 = table1.NewRow();
                DataRow r2 = table2.NewRow();

                // 先頭のカラム
                r1["項目"] = master_dict[m_key].Name;
//                r2["項目"] = master_dict[m_key].Name;

                bool normal_unit_flg = false;

                // 検査日ごとのデータをカラムにセットする
                foreach (string d in data_dict.Keys)
                {
                    Dictionary<string, KensaData> dd = data_dict[d];

                    if (dd.ContainsKey(m_key))
                    {
                        if (!normal_unit_flg)
                        {
                            r1["基準値"] = dd[m_key].Normal;
                            r1["単位"] = dd[m_key].Unit;
                            normal_unit_flg = true;
                        }

                        r1[AppDateTime.DateStringFromString(d)] = dd[m_key].Result;
                        r2[AppDateTime.DateStringFromString(d)] = dd[m_key].ModFlg.ToString();
                    }
                }

                table1.Rows.Add(r1);
                table2.Rows.Add(r2);
            }

            this.DataFormat();
        }

        void DataFormat()
        {
            DataTable table1 = dSet.Tables["KensaResult"];
            DataTable table2 = dSet.Tables["KensaMod"];

            KensaDataView1.DataSource = new DataView(table1);

            this.DateShow();

            KensaDataView1.Columns["項目"].Width = 120;
            KensaDataView1.Columns["項目"].DefaultCellStyle.BackColor = Color.LightYellow;

            KensaDataView1.Columns["基準値"].Width = 80;
            KensaDataView1.Columns["基準値"].DefaultCellStyle.BackColor = Color.LightYellow;

            KensaDataView1.Columns["単位"].Width = 60;
            KensaDataView1.Columns["単位"].DefaultCellStyle.BackColor = Color.LightYellow;
            KensaDataView1.Columns["単位"].Frozen = true;

            // 異常値ならば色を変更する
            for (int i = 0; i < table2.Rows.Count; i++)
            {
                for (int j = 0; j < table2.Rows[i].ItemArray.Length; j++)
                {
                    if (table2.Rows[i].ItemArray[j].ToString().Equals("1"))
                    {
                        KensaDataView1.Rows[i].Cells[j].Style.ForeColor = Color.Blue;
                    }
                    else if (table2.Rows[i].ItemArray[j].ToString().Equals("2"))
                    {
                        KensaDataView1.Rows[i].Cells[j].Style.ForeColor = Color.Red;
                    }
                }
            }
        }

        void KensaDataView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 3 && e.RowIndex >= 0)
            {
                KensaDataView1.Rows[e.RowIndex].Cells[0].Style.BackColor = Color.LightPink;
                KensaDataView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightPink;
            }
        }

        void KensaDataView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 3 && e.RowIndex >= 0)
            {
                KensaDataView1.Rows[e.RowIndex].Cells[0].Style.BackColor = Color.LightYellow;
                KensaDataView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
            }
        }

        void c_CheckedChanged(object sender, EventArgs e)
        {
            this.DateShow();
        }

        /// <summary>
        /// 選択されている日だけを表示する
        /// </summary>
        void DateShow()
        {
            foreach (DataGridViewColumn col in KensaDataView1.Columns)
            {
                if (col.Index > 2)
                {
                    if (KensaDatePanel.Controls.ContainsKey(col.HeaderText) &&
                        ((CheckBox)(KensaDatePanel.Controls[col.HeaderText])).Checked)
                    {
                        col.Visible = true;
                    }
                    else
                    {
                        col.Visible = false;
                    }
                }
            }
        }


        /// <summary>
        /// 選択されているデータをクリップボードにコピー
        /// </summary>
        void DataCopy()
        {
            if (KensaDataView1.Rows.Count == 0)
            {
                return;
            }

            // カラムごとの最長バイト値
            List<int> width_list = new List<int>();

            // 項目名の最長バイト値を仮に 10 としておく
            width_list.Add(10);

            // カラムごとの最長バイト値を調べる
            foreach (DataGridViewRow row in KensaDataView1.SelectedRows)
            {
                string tmp_str = row.Cells[0].Value.ToString();

                // 単位（存在する場合）
                if (row.Cells[2].Value.ToString().Length > 0)
                {
                    tmp_str += "(" + row.Cells[2].Value.ToString() + ")";
                }

                // 項目名が、これまでの最長バイト値より長ければ、値をセットする
                if (width_list[0] < AppString.LenB(tmp_str))
                {
                    width_list[0] = AppString.LenB(tmp_str);
                }


                // 各カラムが、これまでの最長バイト値より長ければ、値をセットする
                foreach (DataGridViewCell cell in row.Cells)
                {
                    // 項目・基準値・単位は飛ばす
                    if (cell.ColumnIndex <= 2) continue;

                    // カラムの最長バイト値を仮に 10 とする
                    if (width_list.Count < cell.ColumnIndex - 1)
                    {
                        width_list.Add(10);
                    }

                    // カラムの長さが、これまでの最長バイト値より長ければ、値をセットする
                    if (width_list[cell.ColumnIndex - 2] < AppString.LenB(cell.Value.ToString()))
                    {
                        width_list[cell.ColumnIndex - 2] = AppString.LenB(cell.Value.ToString());
                    }
                }
            }


            // 日付
            string s = "|".PadRight(width_list[0] + 1, ' ');

            foreach (DataGridViewColumn col in KensaDataView1.Columns)
            {
                if (col.Index <= 2) continue;

                if (col.Visible)
                {
                    s += "|" + col.HeaderText.PadRight(width_list[col.Index - 2], ' ');
                }
            }

            s += Environment.NewLine;


            // 日付の下の横線
            s += "|".PadRight(width_list[0] + 1, '-');

            foreach (DataGridViewColumn col in KensaDataView1.Columns)
            {
                if (col.Index <= 2) continue;

                if (col.Visible)
                {
                    s += "+".PadRight(width_list[col.Index - 2] + 1, '-');
                }
            }

            s += Environment.NewLine;


            // 検査項目と結果
            foreach (DataGridViewRow row in KensaDataView1.SelectedRows)
            {
                // 項目名
                string tmp_str = row.Cells[0].Value.ToString();

                // 単位
                if (row.Cells[2].Value.ToString().Length > 0)
                {
                    tmp_str += "(" + row.Cells[2].Value.ToString() + ")";
                }

                s += "|" + AppString.PadRightB(tmp_str, width_list[0], ' ');


                // 検査結果
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ColumnIndex <= 2) continue;

                    if (cell.Visible)
                    {
                        s += "|" + AppString.PadRightB(cell.Value.ToString(), width_list[cell.ColumnIndex - 2], ' ');
                    }
                }

                s += Environment.NewLine;
            }

            Clipboard.SetDataObject(s, true);
        }

/*
        private void ShowButton1_Click(object sender, EventArgs e)
        {
            this.DataShow();
        }
*/
        private void KensaDataView1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Modifiers & Keys.Control) == Keys.Control && e.KeyCode == Keys.C)
            {
                this.DataCopy();
            }
        }

        private void FormKensa_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Modifiers & Keys.Control) == Keys.Control && e.KeyCode == Keys.C)
            {
                this.DataCopy();
            }
        }

        private void CopyItem_Click(object sender, EventArgs e)
        {
            this.DataCopy();
        }
    }
}
