using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Boundary;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class OushinListForm : Form
    {
        DataSet DSet = new DataSet();

        public OushinListForm()
        {
            InitializeComponent();
        }

        private void OushinListForm_Load(object sender, EventArgs e)
        {
            LibSettings.Init();

            if (LoginUser.Init() && LoginUser.Id.Length > 0)
            {
                this.ShowBikouBox2.Checked = true;
                this.ShowBikouBox5.Checked = true;
                this.ShowBikouBox0.Checked = false;

                this.ShowDeadBox.Checked = false;
                this.ShowDeleteBox.Checked = false;

                DataTable table = DSet.Tables.Add("往診患者一覧");
                table.Columns.Add("ID");
                table.Columns.Add("氏名");
                table.Columns.Add("性別");
                table.Columns.Add("生年月日");
                table.Columns.Add("年齢");
                table.Columns.Add("コメント");
                table.Columns.Add("備考");
                table.Columns.Add("逝去");
                table.Columns.Add("状態");
                table.Columns.Add("登録日");
                table.Columns.Add("登録者");

                ListShow();
            }
            else
            {
                this.Dispose();
            }
        }

        /// <summary>
        /// 往診患者一覧を表示する。
        /// </summary>
        void ListShow()
        {
            DataTable table = DSet.Tables["往診患者一覧"];
            table.Clear();

            List<OushinPat> list = OushinPat.LoadAll();

            foreach (OushinPat pat in list)
            {
                DataRow r = table.NewRow();

                r["ID"] = pat.PtId;
                r["氏名"] = pat.Pat.Name;
                r["性別"] = pat.Pat.SexNameShort;
                r["生年月日"] = pat.Pat.BirthString;
                r["年齢"] = pat.Pat.Age;
                r["コメント"] = pat.Cont;

                if (pat.Pat.NoteCode.Equals("2") || pat.Pat.NoteCode.Equals("5"))
                {
                    // 2: 口座引落 or 5: 往診・在宅 の方のみ表示
                    r["備考"] = pat.Pat.Note1.OushinMark;
                }

                r["逝去"] = pat.Pat.Dead.Equals("1") ? "逝" : "";

                if (pat.Status.Equals("0"))
                {
                    r["状態"] = "削";
                }
                else if (pat.Status.Length == 0)
                {
                    r["状態"] = "未";
                }

                r["登録日"] = DateTimeAgent.DateFormat(pat.SaveDate, DateTimeAgent.DateFormatKind.LONG);
                r["登録者"] = pat.StaffName;

                table.Rows.Add(r);
            }

            ListFormat();
        }

        /// <summary>
        /// 往診患者一覧のフォーマット。
        /// </summary>
        void ListFormat()
        {
            if (!DSet.Tables.Contains("往診患者一覧"))
            {
                return;
            }

            DataView view = new DataView(DSet.Tables["往診患者一覧"]);

            ListView.DataSource = view;

            List<string> filters = new List<string>();

            if (!ShowBikouBox2.Checked)
            {
                filters.Add("(備考 is null or 備考 <> '☆')");
            }

            if (!ShowBikouBox5.Checked)
            {
                filters.Add("(備考 is null or 備考 <> '□')");
            }

            if (!ShowBikouBox0.Checked)
            {
                filters.Add("(備考 is not null and 備考 <> '')");
            }

            if (!ShowDeadBox.Checked)
            {
                filters.Add("(逝去 is null or 逝去 = '')");
            }

            if (!ShowDeleteBox.Checked)
            {
                filters.Add("(状態 is null or 状態 = '')");
            }

            if (FilterBox.Text.Length > 0)
            {
                filters.Add("(ID = '" + this.FilterBox.Text + "' or 氏名 like '%" + this.FilterBox.Text + "%' or コメント like '%" + this.FilterBox.Text + "%')");
            }

            if (filters.Count > 0)
            {
                view.RowFilter = AppString.ConcatList(filters, " and ");
            }

            ListView.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            ListView.Columns["ID"].Width = 60;

            ListView.Columns["氏名"].Width = 100;

            ListView.Columns["性別"].Width = 30;
            ListView.Columns["性別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView.Columns["生年月日"].Width = 85;
            ListView.Columns["生年月日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView.Columns["年齢"].Width = 35;
            ListView.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView.Columns["コメント"].Width = 330;

//            ListView.Columns["備考"].HeaderText = "引落";
            ListView.Columns["備考"].Width = 30;
            ListView.Columns["備考"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView.Columns["備考"].ToolTipText = "☆口座引落　□往診・在宅";

            ListView.Columns["逝去"].Width = 30;
            ListView.Columns["逝去"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView.Columns["状態"].Width = 30;
            ListView.Columns["状態"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView.Columns["登録日"].Width = 85;
            ListView.Columns["登録日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView.Columns["登録者"].Width = 100;
            ListView.Columns["登録者"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            AppDataGridView.SexColor(ListView);

            int c = 0;

            foreach (DataGridViewRow r in ListView.Rows)
            {
                c++;

                if (r.Cells["逝去"].Value.ToString().Equals("逝"))
                {
                    r.DefaultCellStyle.BackColor = Color.DarkGray;
                    r.DefaultCellStyle.ForeColor = Color.White;
                }
                else if (r.Cells["状態"].Value.ToString().Equals("削") ||
                    r.Cells["状態"].Value.ToString().Equals("未"))
                {
                    r.DefaultCellStyle.BackColor = Color.LightGray;
                }
            }

            /*
            if (ShowDeadBox.Checked)
            {
                Font fn = new Font(new Font("MS UI Gothic", 8), FontStyle.Strikeout);

                foreach (DataGridViewRow r in ListView.Rows)
                {
                    if (r.Cells["逝去"].Value.ToString().Equals("1"))
                    {
                        r.DefaultCellStyle.BackColor = Color.LightGray;
                        r.DefaultCellStyle.Font = fn;
                    }
                }
            }
             */

            this.CountLabel.Text = c + " 名";
        }

        void PtClear()
        {
            PtIdBox.Clear();
            PtNameBox.Clear();
            PtMarkBox.Clear();
            PtAddrBox.Clear();
            PtContBox.Clear();
        }

        private void PtIdBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.PtShow();
            }
            else if (e.KeyCode == Keys.F3)
            {
                PatBase p = FormFindPat.FindPat();
                this.PtIdBox.Text = p.Id;
                PtShow();
            }
        }

        /// <summary>
        /// 患者ID欄に入力された患者情報を表示する。
        /// </summary>
        void PtShow()
        {
            if (PtIdBox.Text.Length == 0)
            {
                return;
            }

            OushinPat pat = OushinPat.Load(PtIdBox.Text);

            PtNameBox.Text = pat.Pat.Info1;
            PtMarkBox.Text = pat.Pat.Note1.OushinMark + pat.Pat.Note1.Name;

            string addr = "";

            if (pat.Pat.Post.Length > 0)
            {
                addr += "〒" + pat.Pat.Post + "　";
            }

            if (pat.Pat.Addr.Length > 0)
            {
                addr += pat.Pat.Addr;
            }

            // いったん改行する
            if (addr.Length > 0)
            {
                addr += Environment.NewLine;
            }

            if (pat.Pat.Tel.Length > 0)
            {
                addr += pat.Pat.Tel;
            }

            PtAddrBox.Text = addr;

            PtContBox.Text = pat.Cont;

        }

        private void ShowBikouBox2_CheckedChanged(object sender, EventArgs e)
        {
            ListFormat();
        }

        private void ShowBikouBox5_CheckedChanged(object sender, EventArgs e)
        {
            ListFormat();
        }

        private void ShowBikouBox0_CheckedChanged(object sender, EventArgs e)
        {
            ListFormat();
        }

        private void ShowDeadBox_CheckedChanged(object sender, EventArgs e)
        {
            ListFormat();
        }

        private void ShowDeleteBox_CheckedChanged(object sender, EventArgs e)
        {
            ListFormat();
        }

        private void FilterBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ListFormat();
            }
        }

        private void ListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                PtIdBox.Text = ListView.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                PtShow();
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            PtClear();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            

            if (PtIdBox.Text.Length == 0)
            {
                return;
            }

            if (MessageBox.Show("削除しますか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                OushinPat.Delete(PtIdBox.Text);
                PtClear();
                ListShow();
            }
        }

        private void RegButton_Click(object sender, EventArgs e)
        {
            if (PtIdBox.Text.Length == 0)
            {
                return;
            }

            OushinPat pat = new OushinPat();

            pat.PtId = PtIdBox.Text;
            pat.Cont = PtContBox.Text;
            pat.Status = "1";

            pat.Save();
            MessageBox.Show("登録しました");

            PtClear();
            ListShow();
        }

        private void ExcelButton_Click(object sender, EventArgs e)
        {
            TableData data = new TableData();
            int i = 0;

            foreach (DataGridViewColumn c in ListView.Columns)
            {
                // 出力するのは逝去カラムまで
                if (i > 7) break;

                data.Title.Add(c.HeaderText);
                i++;
            }

            foreach (DataGridViewRow r in ListView.Rows)
            {
                TableDataRecord obj = new TableDataRecord();
                i = 0;

                foreach (DataGridViewColumn c in ListView.Columns)
                {
                    // 出力するのは逝去カラムまで
                    if (i > 7) break;

                    obj.DataList.Add(r.Cells[c.Name].Value.ToString());
                    i++;
                }

                data.RecordList.Add(obj);
            }

            if (data.ExcelOpen(true))
            {
                MessageBox.Show("Excel 出力が完了しました");
            }
        }
    }
}