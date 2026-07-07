using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using MedicalLibrary.Boundary;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class ReceStatListForm : Form
    {
        DataSet dSet = new DataSet();

        string pcName = System.Environment.GetEnvironmentVariable("COMPUTERNAME");

        string dept
        {
            get
            {
                string s = "2";

                if (this.DeptBox1.Text.Contains(" "))
                {
                    s = this.DeptBox1.Text.Split(' ')[0];
                }

                return s;
            }
            set
            {
                if (Dict.DeptDict.ContainsKey(value))
                {
                    this.DeptBox1.Text = value + " " + Dict.DeptDict[value].ShortName;
                }
                else
                {
                    this.DeptBox1.Text = "2 外科";
                }
            }
        }

        string mode
        {
            get
            {
#if INNO
                string s = "0";
#else
                string s = "1";
#endif
                if (ModeBox1.Text.Contains(" "))
                {
                    s = this.ModeBox1.Text.Split(' ')[0];
                }

                return s;
            }
        }

        int count1 = 0;
        int count2 = 0;
        int count3 = 0;

        public ReceStatListForm()
        {
            InitializeComponent();
        }

        private void ReceStatListForm_Load(object sender, EventArgs e)
        {
            try
            {
                LibSettings.Init();

                foreach (string k in Dict.DeptDict.Keys)
                {
                    if (!k.Equals("0"))
                    {
                        DeptBox1.Items.Add(k + " " + Dict.DeptDict[k].ShortName);
                    }
                }

                this.DeptBox1.Text = "2 外科";

#if INNO
                this.ModeBox1.Items.Add("0 通常");
                this.ModeBox1.Items.Add("1 救急");
                this.ModeBox1.Items.Add("2 在宅");
                this.ModeBox1.Text = "0 通常";
#else
                this.ModeBox1.Items.Add("1 通常");
                this.ModeBox1.Items.Add("2 救急");
                this.ModeBox1.Text = "1 通常";
#endif

                DataTable tmpTable = dSet.Tables.Add("診察状況");
                tmpTable.Columns.Add("区分");
                tmpTable.Columns.Add("通番", typeof(int));
                tmpTable.Columns.Add("科番", typeof(int));
                tmpTable.Columns.Add("受付");
                tmpTable.Columns.Add("ID", typeof(int));
                tmpTable.Columns.Add("カナ");
                tmpTable.Columns.Add("氏名");
                tmpTable.Columns.Add("性別");
                tmpTable.Columns.Add("年齢", typeof(int));
                tmpTable.Columns.Add("種別");
                tmpTable.Columns.Add("科");
                tmpTable.Columns.Add("医師");
                tmpTable.Columns.Add("開始");
                tmpTable.Columns.Add("中断");
                tmpTable.Columns.Add("終了");
                tmpTable.Columns.Add("会計");
                tmpTable.Columns.Add("開始フラグ");
                tmpTable.Columns.Add("Obj", typeof(PatOut));

                tmpTable = dSet.Tables.Add("受付状況");
                tmpTable.Columns.Add("科コード", typeof(int));
                tmpTable.Columns.Add("診療科");
                tmpTable.Columns.Add("区分");
                tmpTable.Columns.Add("人数", typeof(int));

                tmpTable = dSet.Tables.Add("診察未終了状況");
                tmpTable.Columns.Add("科コード", typeof(int));
                tmpTable.Columns.Add("診療科");
                tmpTable.Columns.Add("区分");
                tmpTable.Columns.Add("人数", typeof(int));

                this.showAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.timer1.Interval = 180 * 1000;
            this.timer1.Enabled = true;
        }

        void showAll()
        {
            showList();
            showStat();
        }

        /// <summary>
        /// 診察状況リスト表示
        /// </summary>
        void showList()
        {
            if (!this.dSet.Tables.Contains("診察状況"))
            {
                return;
            }

            DataTable tmpTable = dSet.Tables["診察状況"];
            tmpTable.Clear();

            List<PatOut> tmpList = PatOut.GetList(this.dateTimePicker1.Value.ToString("yyyyMMdd"), this.dept, "");

            // 受付時間順に並べ替える
            tmpList.Sort((x, y) =>
                {
                    int x1 = 0;
                    int y1 = 0;

                    int.TryParse(x.Time1, out x1);
                    int.TryParse(y.Time1, out y1);

                    int i = x1 - y1;

                    if (i == 0)
                    {
                        int.TryParse(x.Seq1, out x1);
                        int.TryParse(y.Seq1, out y1);
                        i = x1 - y1;
                    }

                    return i;
                }
            );

            foreach (PatOut obj in tmpList)
            {
                // 区分が異なれば飛ばす
                if (!obj.Mode.Equals(this.mode))
                {
                    continue;
                }

                DataRow r = tmpTable.NewRow();

                r["区分"] = obj.Mode;
                r["通番"] = obj.Seq1;
                r["科番"] = obj.Seq3;
                r["受付"] = obj.TimeString1;

                r["ID"] = obj.Id;
                r["カナ"] = obj.Kana;
                r["氏名"] = obj.Name;
                r["性別"] = obj.SexNameShort;

                r["年齢"] = obj.Age;
                r["種別"] = obj.KindName;

                r["科"] = obj.DeptName;
                r["医師"] = obj.DoctorName;

                r["開始"] = obj.TimeString2;
                r["中断"] = obj.TimeString3;
                r["終了"] = obj.TimeString4;
                r["会計"] = obj.TimeString5;

                if (obj.TimeString2.Length > 0)
                {
                    r["開始フラグ"] = "0";
                }
                else
                {
                    r["開始フラグ"] = "1";
                }

                r["Obj"] = obj;

                tmpTable.Rows.Add(r);
            }

            this.filterStat1();
        }

        /// <summary>
        /// 集計リスト表示
        /// </summary>
        private void showStat()
        {
            if (!this.dSet.Tables.Contains("受付状況"))
            {
                return;
            }

            DataTable table = this.dSet.Tables["受付状況"];
            table.Rows.Clear();

            List<DeptOut> list1 = DeptOut.GetList(this.dateTimePicker1.Value.ToString("yyyyMMdd"));
            int d = 0;

            foreach (DeptOut obj in list1)
            {
                // 区分が異なれば飛ばす
                if (!obj.ModeCode.Equals(this.mode))
                {
                    continue;
                }

                DataRow r = table.NewRow();

                r["科コード"] = int.TryParse(obj.DeptCode, out d) ? d : 0;
                r["診療科"] = obj.DeptName;
                r["区分"] = obj.ModeCode;
                r["人数"] = obj.Count;

                table.Rows.Add(r);
            }

            DataView view = new DataView(table);

            dataGridView2.DataSource = view;
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[2].Visible = false;

            /* 20120918 matsui Oracle11gR2 では並び順が変わるので制御をする。 */
            dataGridView2.Sort(dataGridView2.Columns[0], ListSortDirection.Ascending);
            
            dataGridView2.Columns[1].Width = 80;
            dataGridView2.Columns[3].Width = 50;
            dataGridView2.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // 全体受付人数
            int all_count0 = 0;

            foreach (DataGridViewRow r in dataGridView2.Rows)
            {
                all_count0 += (int)r.Cells["人数"].Value;

                if (r.Cells["科コード"].Value.ToString().Equals(this.dept))
                {
                    r.Selected = true;
                }
            }

            label_all_count0.Text = "受付人数　" + all_count0 + " 人";


            table = this.dSet.Tables["診察未終了状況"];
            table.Rows.Clear();

            List<DeptOut> list2 = DeptOut.GetYetList(this.dateTimePicker1.Value.ToString("yyyyMMdd"));

            foreach (DeptOut obj in list2)
            {
                // 区分が異なれば飛ばす
                if (!obj.ModeCode.Equals(this.mode))
                {
                    continue;
                }

                DataRow r = table.NewRow();

                r["科コード"] = int.TryParse(obj.DeptCode, out d) ? d : 0;
                r["診療科"] = obj.DeptName;
                r["区分"] = obj.ModeCode;
                r["人数"] = obj.Count;

                table.Rows.Add(r);
            }

            view = new DataView(table);

            dataGridView3.DataSource = view;

            dataGridView3.Columns[0].Visible = false;
            dataGridView3.Columns[2].Visible = false;

            /* 20120918 matsui Oracle11gR2 では並び順が変わるので制御をする。 */
            dataGridView3.Sort(dataGridView3.Columns[0], ListSortDirection.Ascending);

            dataGridView3.Columns[1].Width = 80;
            dataGridView3.Columns[3].Width = 50;
            dataGridView3.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            foreach (DataGridViewRow r in dataGridView3.Rows)
            {
                if (r.Cells["科コード"].Value.ToString().Equals(this.dept))
                {
                    r.Selected = true;
                }
            }
        }

        /// <summary>
        /// 患者一覧のフィルタとソート
        /// </summary>
        private void filterStat1()
        {
            if (!this.dSet.Tables.Contains("診察状況"))
            {
                return;
            }

            DataView view = new DataView(dSet.Tables["診察状況"]);
            List<string> filters = new List<string>();

            if (!showEnd.Checked)
            {
                filters.Add("終了 = ''");
            }

            view.RowFilter = AppString.ConcatList(filters, " and ");

            // DataGridView コントロール上でソート順が指定されていればそれを保持する
            
            int tmpColumn = -1;              // 現在ソート対象になっているカラムの番号。デフォルトは 3（受付時間）。
            ListSortDirection tmpDirection = ListSortDirection.Ascending;       // ソートの方向。

            if (PatListView.SortOrder == SortOrder.Ascending)
            {
                tmpColumn = PatListView.SortedColumn.Index;
            }
            else if (PatListView.SortOrder == SortOrder.Descending)
            {
                tmpColumn = PatListView.SortedColumn.Index;
                tmpDirection = ListSortDirection.Descending;
            }

            PatListView.DataSource = view;

            PatListView.Columns["区分"].Visible = false;

            PatListView.Columns["通番"].HeaderText = "通番";
            PatListView.Columns["通番"].Width = 40;
            PatListView.Columns["通番"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            PatListView.Columns["科番"].HeaderText = "科番";
            PatListView.Columns["科番"].Width = 40;
            PatListView.Columns["科番"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            PatListView.Columns["受付"].HeaderText = "受付";
            PatListView.Columns["受付"].Width = 40;
            PatListView.Columns["受付"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            PatListView.Columns["ID"].HeaderText = "ID";
            PatListView.Columns["ID"].Width = 55;
            PatListView.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            PatListView.Columns["氏名"].Width = 90;

            PatListView.Columns["性別"].HeaderText = "性別";
            PatListView.Columns["性別"].Width = 35;
            PatListView.Columns["性別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            PatListView.Columns["年齢"].HeaderText = "年齢";
            PatListView.Columns["年齢"].Width = 35;
            PatListView.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            PatListView.Columns["種別"].HeaderText = "種別";
            PatListView.Columns["種別"].Width = 55;
            PatListView.Columns["種別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            PatListView.Columns["科"].HeaderText = "科";
            PatListView.Columns["科"].Width = 75;
            PatListView.Columns["科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            PatListView.Columns["医師"].HeaderText = "医師";
            PatListView.Columns["医師"].Width = 80;
            PatListView.Columns["医師"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            PatListView.Columns["開始"].HeaderText = "開始";
            PatListView.Columns["開始"].Width = 40;
            PatListView.Columns["開始"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            PatListView.Columns["中断"].HeaderText = "中断";
            PatListView.Columns["中断"].Width = 40;
            PatListView.Columns["中断"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            PatListView.Columns["終了"].HeaderText = "終了";
            PatListView.Columns["終了"].Width = 40;
            PatListView.Columns["終了"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            PatListView.Columns["会計"].HeaderText = "会計";
            PatListView.Columns["会計"].Width = 40;
            PatListView.Columns["会計"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            PatListView.Columns["開始フラグ"].Visible = false;

            PatListView.Columns["Obj"].Visible = false;

            if (tmpColumn >= 0)
            {
                PatListView.Sort(PatListView.Columns[tmpColumn], tmpDirection);
            }

            // 人数集計やグリッドの色変更を行う。ソートの後に行う必要がある
            count1 = 0;
            count2 = 0;
            count3 = 0;

            for (int i = 0; i < PatListView.Rows.Count; i++)
            {
                if (PatListView.Rows[i].Cells["終了"].Value.ToString().Length > 0)
                {
                    PatListView.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                    count3++;
                }
                else if (PatListView.Rows[i].Cells["中断"].Value.ToString().Length > 0)
                {
                    PatListView.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;
                    count2++;
                }
                else if (PatListView.Rows[i].Cells["開始"].Value.ToString().Length > 0)
                {
                    PatListView.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;
                    count2++;
                }
                else
                {
                    count1++;
                }

                if (PatListView.Rows[i].Cells["性別"].Value.ToString() == "女")
                {
                    PatListView.Rows[i].Cells["ID"].Style.ForeColor = Color.Red;
                    PatListView.Rows[i].Cells["カナ"].Style.ForeColor = Color.Red;
                    PatListView.Rows[i].Cells["氏名"].Style.ForeColor = Color.Red;
                    PatListView.Rows[i].Cells["性別"].Style.ForeColor = Color.Red;
                    PatListView.Rows[i].Cells["年齢"].Style.ForeColor = Color.Red;
                }
            }

            this.label_count1.Text = "診察開始前 " + count1 + " 人";
            this.label_count2.Text = "診察中 " + count2 + " 人";
            this.label_count3.Text = "診察終了 " + count3 + " 人";
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.showAll();
//            this.updateButton.Focus();
        }

        private void DeptBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.showAll();
            //            this.updateButton.Focus();
        }

        private void ModeBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.showAll();
            //            this.updateButton.Focus();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            this.showAll();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.showAll();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            for (int i = 0; i < PatListView.Rows.Count; i++)
            {
                if (PatListView.Rows[i].Cells["終了"].Value.ToString().Length > 0)
                {
                    PatListView.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                }
                else if (PatListView.Rows[i].Cells["中断"].Value.ToString().Length > 0)
                {
                    PatListView.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;
                }
                else if (PatListView.Rows[i].Cells["開始"].Value.ToString().Length > 0)
                {
                    PatListView.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;
                }

                if (PatListView.Rows[i].Cells["性別"].Value.ToString() == "女")
                {
                    PatListView.Rows[i].Cells["ID"].Style.ForeColor = Color.Red;
                    PatListView.Rows[i].Cells["カナ"].Style.ForeColor = Color.Red;
                    PatListView.Rows[i].Cells["氏名"].Style.ForeColor = Color.Red;
                    PatListView.Rows[i].Cells["性別"].Style.ForeColor = Color.Red;
                    PatListView.Rows[i].Cells["年齢"].Style.ForeColor = Color.Red;
                }
            }
        }

        private void showEnd_CheckedChanged(object sender, EventArgs e)
        {
            this.filterStat1();
        }

        private void KarteMenuItem_Click(object sender, EventArgs e)
        {
            if (PatListView.SelectedCells.Count > 0)
            {
                FormControl.FormPat_Show((PatOut)PatListView.Rows[PatListView.SelectedCells[0].RowIndex].Cells["Obj"].Value);
            }
        }

        private void PatListLabelPrintMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("医療安全管理上よくないとの理由で、この機能は使用不可となりました。\r\n申し訳ございません。（H21/3/31）");
        }

        private void ReceStatListForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                showAll();
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow r = dataGridView2.Rows[e.RowIndex];
                this.dept = r.Cells["科コード"].Value.ToString();
            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow r = dataGridView3.Rows[e.RowIndex];
                this.dept = r.Cells["科コード"].Value.ToString();
            }
        }
    }
}