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
    public partial class FormRsvPatList : Form
    {
        /// <summary>
        /// 予約種別のリスト
        /// </summary>
        List<RsvNameMaster> MasterList = new List<RsvNameMaster>();

        /// <summary>
        /// 選択されている予約種別のリスト
        /// </summary>
        List<RsvNameMaster> SelectList
        {
            get
            {
                List<RsvNameMaster> list = new List<RsvNameMaster>();

                foreach (Control c in this.MasterListPanel.Controls)
                {
                    if (c is CheckBox && ((CheckBox)c).Checked)
                    {
                        list.Add((RsvNameMaster)c.Tag);
                    }
                }

                return list;
            }
        }

        /// <summary>
        /// 対象患者リスト
        /// </summary>
        List<RsvPat> PatList = new List<RsvPat>();

        ContextMenuStrip MenuStrip = new ContextMenuStrip();

        DataSet DSet = new DataSet();

        public FormRsvPatList()
        {
            InitializeComponent();
        }

        private void FormRsvPatList_Load(object sender, EventArgs e)
        {
            this.MasterList = RsvNameMaster.GetList();

            foreach (RsvNameMaster m in this.MasterList)
            {
                CheckBox b = new CheckBox();
                b.Name = m.Code;
                b.Text = m.ShortName;
                b.Width = 120;
                b.Tag = m;

                this.MasterListPanel.Controls.Add(b);
            }

            DataTable table = DSet.Tables.Add("予約一覧");

            ToolStripMenuItem item;

            item = new ToolStripMenuItem("カルテ");
            item.Click += new EventHandler(item_Click);
            this.MenuStrip.Items.Add(item);

            item = new ToolStripMenuItem("眼科システム");
            item.Click += new EventHandler(item_Click);
            this.MenuStrip.Items.Add(item);

            this.ListView.ContextMenuStrip = this.MenuStrip;
        }

        void item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            if (this.ListView.SelectedRows.Count > 0)
            {
                RsvPat p = (RsvPat)this.ListView.SelectedRows[0].Cells["Obj"].Value;

                if (p.Pat.Id.Length > 0)
                {
                    if (item.Text.Equals("カルテ"))
                    {
                        FormSoap f = new FormSoap();
                        f.ReadOnly = true;
                        f.PatSet(p.Pat);
                        f.Show();
                    }
                    else if (item.Text.Equals("眼科システム"))
                    {
                        Launcher.EyeCenter(p.Pat.Id);
                    }
                }
            }
        }

        void ListShow()
        {
            if (!DSet.Tables.Contains("予約一覧"))
            {
                return;
            }

            DataTable table = DSet.Tables["予約一覧"];
            table.Rows.Clear();
            table.Columns.Clear();
            table.Columns.Add("ID");
            table.Columns.Add("氏名");
            table.Columns.Add("年齢");
            table.Columns.Add("性別");
            table.Columns.Add("FirstTime");

            List<string> code_list = new List<string>();

            foreach (RsvNameMaster m in this.SelectList)
            {
                table.Columns.Add(m.Code);
                code_list.Add(m.Code);
            }

            // 選択されていない種別も表示する場合
            if (this.OtherBox.Checked)
            {
                table.Columns.Add("他の検査");
            }

            // 眼科の最終受診日Ｐも表示する場合
            if (this.LastPBox7.Checked)
            {
                table.Columns.Add("眼科前回日");
                table.Columns.Add("眼科前回P");
                table.Columns.Add("眼科前回担当");
            }

            table.Columns.Add("Obj", typeof(RsvPat));

            // 対象患者リストを取得する
            this.PatList.Clear();

            List<RsvData> list = new List<RsvData>();

            if (code_list.Count > 0)
            {
                if (this.OtherBox.Checked)
                {
                    // すべての予約を取得する
                    list = RsvData.GetListByDate(this.DatePicker1.Value.ToString("yyyyMMdd"));
                }
                else
                {
                    // 選択された予約のみを取得する
                    list = RsvData.GetListByDateCodes(this.DatePicker1.Value.ToString("yyyyMMdd"), code_list);
                }

                List<string> pt_list7 = new List<string>();
                List<SoapDetail> soap_list7 = new List<SoapDetail>();

                // 対象患者リストを作る
                foreach (RsvData data in list)
                {
                    RsvPat p = null;

                    foreach (RsvPat pp in this.PatList)
                    {
                        if (pp.Pat.Id.Equals(data.Pat.Id))
                        {
                            p = pp;
                            break;
                        }
                    }

                    if (p == null)
                    {
                        p = new RsvPat();

                        p.Pat.Id = data.Pat.Id;
                        p.Pat.Kana = data.Pat.Kana;
                        p.Pat.Name = data.Pat.Name;
                        p.Pat.Sex = data.Pat.Sex;
                        p.Pat.Birth = data.Pat.Birth;

                        this.PatList.Add(p);
                        pt_list7.Add(data.Pat.Id);
                    }

                    p.RsvList.Add(data);
                }

                // 選択された予約のうち、最初の開始時間で並べ替える
                this.PatList.Sort((x, y) => {
                    return x.FirstTimeString(code_list).CompareTo(y.FirstTimeString(code_list));
                });

                // 眼科の最終受診日Ｐも表示する場合
                if (this.LastPBox7.Checked)
                {
                    soap_list7 = SoapDetail.GetLastList(pt_list7, DatePicker1.Value.ToString("yyyyMMdd"), "7", "4");
                }

                foreach (RsvPat p in this.PatList)
                {
                    // 選択された予約を含まなければ飛ばす
                    if (!p.Contains(code_list))
                    {
                        continue;
                    }

                    DataRow r = table.NewRow();

                    r["ID"] = p.Pat.Id;
                    r["氏名"] = p.Pat.Name;
                    r["年齢"] = p.Pat.AgeCalc(this.DatePicker1.Value.ToString("yyyyMMdd"));
                    r["性別"] = p.Pat.Sex;
                    r["FirstTime"] = p.FirstTimeString(code_list);

                    foreach (RsvData data in p.RsvList)
                    {
                        if (table.Columns.Contains(data.Code))
                        {
                            if (r[data.Code].ToString().Length > 0)
                            {
                                r[data.Code] += ", ";
                            }

                            r[data.Code] += data.TimeString();
                        }
                    }

                    // 選択されていない予約も表示する場合
                    if (this.OtherBox.Checked)
                    {
                        string other = "";

                        foreach (RsvData data in p.OtherList(code_list))
                        {
                            if (other.Length > 0)
                            {
                                other += ", ";
                            }

                            other += data.TimeString() + " ";

                            if (RsvNameMaster.Load(data.Code).ShortName.Length > 0)
                            {
                                other += RsvNameMaster.Load(data.Code).ShortName;
                            }
                        }

                        r["他の検査"] = other;
                    }

                    // 眼科の最終受診日Ｐも表示する場合
                    if (this.LastPBox7.Checked)
                    {
                        foreach (SoapDetail detail in soap_list7)
                        {
                            if (detail.PtId.Equals(p.Pat.Id))
                            {
                                r["眼科前回日"] = DateTimeAgent.DateFormat(detail.SoapDate, DateTimeAgent.DateFormatKind.LONG);
                                r["眼科前回P"] = detail.Cont.Trim();
                                r["眼科前回担当"] = detail.RegStaffName;
                                break;
                            }
                        }
                    }

                    r["Obj"] = p;

                    table.Rows.Add(r);
                }
            }

            DataView view = new DataView(table);

            this.ListView.DataSource = view;

            this.ListView.Columns["ID"].Width = 60;
            this.ListView.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.ListView.Columns["氏名"].Width = 80;
            this.ListView.Columns["氏名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView.Columns["年齢"].Width = 30;
            this.ListView.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.ListView.Columns["性別"].Visible = false;

            this.ListView.Columns["FirstTime"].HeaderText = "最初の予約";
            this.ListView.Columns["FirstTime"].Width = 80;
            this.ListView.Columns["FirstTime"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView.Columns["FirstTime"].ToolTipText = "選んだ予約の中で一番早い時間帯";

            // ２種類以上の検査を選んだ場合のみ表示
            if (code_list.Count > 1)
            {
                this.ListView.Columns["FirstTime"].Visible = true;
            }
            else
            {
                this.ListView.Columns["FirstTime"].Visible = false;
            }

            foreach (RsvNameMaster m in this.SelectList)
            {
                if (this.ListView.Columns.Contains(m.Code))
                {
                    this.ListView.Columns[m.Code].HeaderText = m.ShortName;
                    this.ListView.Columns[m.Code].Width = 80;
                    this.ListView.Columns[m.Code].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }

            if (this.ListView.Columns.Contains("他の検査"))
            {
                this.ListView.Columns["他の検査"].Width = 250;
                this.ListView.Columns["他の検査"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            if (this.ListView.Columns.Contains("眼科前回日"))
            {
                this.ListView.Columns["眼科前回日"].Width = 70;
                this.ListView.Columns["眼科前回日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                this.ListView.Columns["眼科前回P"].Width = 350;
                this.ListView.Columns["眼科前回P"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                this.ListView.Columns["眼科前回担当"].Width = 80;
                this.ListView.Columns["眼科前回担当"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            this.ListView.Columns["Obj"].Visible = false;

            AppDataGridView.SexColor(this.ListView);
        }

        private void MasterSelectShowButton_Click(object sender, EventArgs e)
        {
            this.MasterSelectPanel.Visible = true;
        }

        private void MasterSelectCloseButton_Click(object sender, EventArgs e)
        {
            this.MasterSelectPanel.Visible = false;

            this.ListShow();
        }

        private void DatePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.ListShow();
        }

        private void ExcelButton_Click(object sender, EventArgs e)
        {
            if (this.ListView.Rows.Count == 0)
            {
                MessageBox.Show("対象データがありません");
                return;
            }

            TableData data = new TableData(this.ListView);
            data.ExcelOpen(true);
        }
    }

    /// <summary>
    /// 患者ごとの予約一覧
    /// </summary>
    class RsvPat
    {
        PatBase _Pat = new PatBase();

        public PatBase Pat
        {
            get
            {
                return this._Pat;
            }
        }

        public List<RsvData> RsvList = new List<RsvData>();

        /// <summary>
        /// 指定された予約種別を含むかどうか
        /// </summary>
        /// <param name="code_list"></param>
        /// <returns></returns>
        public bool Contains(List<string> code_list)
        {
            bool b = false;

            foreach (RsvData data in this.RsvList)
            {
                if (code_list.Contains(data.Code))
                {
                    b = true;
                    break;
                }
            }

            return b;
        }

        /// <summary>
        /// 指定された種別以外の予約リスト
        /// </summary>
        /// <param name="code_list"></param>
        /// <returns></returns>
        public List<RsvData> OtherList(List<string> code_list)
        {
            List<RsvData> list = new List<RsvData>();

            foreach (RsvData data in this.RsvList)
            {
                if (code_list.Contains(data.Code))
                {
                    continue;
                }

                list.Add(data);
            }

            // 開始時刻で並べ替える
            list.Sort((x, y) =>
            {
                return x.Time1 - y.Time1;
            });

            return list;
        }

        /// <summary>
        /// 指定された種別のうち、最初の検査の開始時間
        /// </summary>
        /// <param name="code_list"></param>
        /// <returns></returns>
        public int FirstTime(List<string> code_list = null)
        {
            int time = 0;

            foreach (RsvData data in this.RsvList)
            {
                // code_list が指定されていて、かつ対象外の場合
                if (code_list != null && !code_list.Contains(data.Code))
                {
                    continue;
                }

                if (time == 0 || data.Time1 < time)
                {
                    time = data.Time1;
                }
            }

//            if (time == 0) time = 999999;

            return time;
        }

        /// <summary>
        /// 指定された種別のうち、最初の検査の時間帯
        /// </summary>
        /// <param name="code_list"></param>
        /// <returns></returns>
        public string FirstTimeString(List<string> code_list = null)
        {
            string time_string = "";

            foreach (RsvData data in this.RsvList)
            {
                // code_list が指定されていて、かつ対象外の場合
                if (code_list != null && !code_list.Contains(data.Code))
                {
                    continue;
                }

                if (time_string.Length == 0 || data.TimeString().CompareTo(time_string) < 0)
                {
                    time_string = data.TimeString();
                }
            }

            return time_string;
        }
    }
}
