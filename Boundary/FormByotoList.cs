using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class FormByotoList : StdForm1
    {
        DataSet dSet = new DataSet();

        string ListViewSort1 = "病棟コード, 病室";
        SortOrder ListViewSortOrder1 = SortOrder.Ascending;

        string ListViewSort2 = "退院日";
        SortOrder ListViewSortOrder2 = SortOrder.Descending;

        string ListViewSort3 = "入院予定日";
        SortOrder ListViewSortOrder3 = SortOrder.Ascending;

        /// <summary>
        /// 現在選択されている病棟の科コード。
        /// プロブレムフォームを開くときに必要。
        /// </summary>
        string DeptCode = "";

        /// <summary>
        /// ユーザーモード
        /// ダブルクリックした時の動作が決まる
        /// </summary>
        public enum UserMode : int
        {
            Doctor = 1,
            Nurse = 11,

            /// <summary>
            /// 医事
            /// </summary>
            Iji = 20,

            /// <summary>
            /// 病棟医事
            /// </summary>
            Iji2 = 22,

            /// <summary>
            /// DPC
            /// </summary>
            Dpc = 25,

            Joho = 99
        }

        UserMode user_mode = UserMode.Iji;

        public UserMode UserMode1
        {
            set
            {
                this.user_mode = value;

                this.ListFormat1();
            }
            get
            {
                return this.user_mode;
            }
        }

        /// <summary>
        /// 詳細表示
        /// </summary>
        bool detail_mode = true;

        /// <summary>
        /// 詳細表示
        /// </summary>
        public bool DetailMode
        {
            set
            {
                detail_mode = value;

                if (this.detail_mode)
                {
                    // 患者情報
                    this.stdControlPat11.Visible = true;

                    // 病名
                    this.DiagLabel1.Visible = true;
                    this.DiagButton1.Visible = false;
                    this.DiagView1.Visible = true;
                    this.DiagCheckBox1.Visible = true;
                    this.DiagCheckBox2.Visible = true;
                    this.DPCButton1.Visible = true;

                    // 入退院
                    this.InHistoryButton1.Visible = true;
                    this.InHistoryGridView1.Visible = true;

                    // プロブレム
                    this.ProblemLabel1.Visible = true;
                    this.ProblemButton1.Visible = false;
                    this.ProblemView1.Visible = true;
                    this.ProblemSectionBox1.Visible = true;
                    this.ProblemCheckBox1.Visible = true;
                    this.ProblemCheckBox2.Visible = true;

                    // 伝達情報
                    this.MemoButton1.Visible = true;
                    this.MemoBox1.Visible = true;
                    this.MemoDateTimeLabel1.Visible = true;
                    this.MemoStaffLabel1.Visible = true;

                    // 患者一覧
                    this.TabControl1.Height = this.Height - 330;
                }
                else
                {
                    // 患者情報
                    this.stdControlPat11.Visible = false;

                    // 病名
                    this.DiagLabel1.Visible = false;
                    this.DiagButton1.Visible = false;
                    this.DiagView1.Visible = false;
                    this.DiagCheckBox1.Visible = false;
                    this.DiagCheckBox2.Visible = false;
                    this.DPCButton1.Visible = false;

                    // 入退院
                    this.InHistoryButton1.Visible = false;
                    this.InHistoryGridView1.Visible = false;

                    // プロブレム
                    this.ProblemLabel1.Visible = false;
                    this.ProblemButton1.Visible = false;
                    this.ProblemView1.Visible = false;
                    this.ProblemSectionBox1.Visible = false;
                    this.ProblemCheckBox1.Visible = false;
                    this.ProblemCheckBox2.Visible = false;

                    // 伝達情報
                    this.MemoButton1.Visible = false;
                    this.MemoBox1.Visible = false;
                    this.MemoDateTimeLabel1.Visible = false;
                    this.MemoStaffLabel1.Visible = false;

                    // 患者一覧
                    this.TabControl1.Height = this.Height - 70;
                }
            }
            get
            {
                return detail_mode;
            }
        }

        bool hide_mode = true;

        public bool HideMode
        {
            set
            {
                this.hide_mode = value;

                foreach (DataGridViewRow r in this.ListView1.Rows)
                {
                    if (r.Cells["性別"].Value.ToString().Equals("女"))
                    {
                        r.Cells["ID"].Style.ForeColor = Color.Red;
                        r.Cells["カナ"].Style.ForeColor = Color.Red;
                        r.Cells["氏名"].Style.ForeColor = Color.Red;
                        r.Cells["性別"].Style.ForeColor = Color.Red;
                        r.Cells["年齢"].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        r.Cells["ID"].Style.ForeColor = Color.Black;
                        r.Cells["カナ"].Style.ForeColor = Color.Black;
                        r.Cells["氏名"].Style.ForeColor = Color.Black;
                        r.Cells["性別"].Style.ForeColor = Color.Black;
                        r.Cells["年齢"].Style.ForeColor = Color.Black;
                    }

                    if (r.Cells["面会状況"].Value.ToString().Length > 0)
                    {
                        r.Cells["ID"].Style.BackColor = Color.SkyBlue;
                        r.Cells["カナ"].Style.BackColor = Color.SkyBlue;
                        r.Cells["氏名"].Style.BackColor = Color.SkyBlue;
                        r.Cells["性別"].Style.BackColor = Color.SkyBlue;
                        r.Cells["年齢"].Style.BackColor = Color.SkyBlue;
                    }

                    if (this.hide_mode && r.Cells["面会状況"].Value.ToString().Equals("非表示"))
                    {
                        r.Cells["ID"].Style.ForeColor = Color.SkyBlue;
                        r.Cells["カナ"].Style.ForeColor = Color.SkyBlue;
                        r.Cells["氏名"].Style.ForeColor = Color.SkyBlue;
                        r.Cells["性別"].Style.ForeColor = Color.SkyBlue;
                        r.Cells["年齢"].Style.ForeColor = Color.SkyBlue;
                    }
                }

                foreach (DataGridViewRow r in this.ListView2.Rows)
                {
                    if (r.Cells["性別"].Value.ToString().Equals("女"))
                    {
                        r.Cells["ID"].Style.ForeColor = Color.Red;
                        r.Cells["カナ"].Style.ForeColor = Color.Red;
                        r.Cells["氏名"].Style.ForeColor = Color.Red;
                        r.Cells["性別"].Style.ForeColor = Color.Red;
                        r.Cells["年齢"].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        r.Cells["ID"].Style.ForeColor = Color.Black;
                        r.Cells["カナ"].Style.ForeColor = Color.Black;
                        r.Cells["氏名"].Style.ForeColor = Color.Black;
                        r.Cells["性別"].Style.ForeColor = Color.Black;
                        r.Cells["年齢"].Style.ForeColor = Color.Black;
                    }

                    if (r.Cells["面会状況"].Value.ToString().Length > 0)
                    {
                        r.Cells["ID"].Style.BackColor = Color.SkyBlue;
                        r.Cells["カナ"].Style.BackColor = Color.SkyBlue;
                        r.Cells["氏名"].Style.BackColor = Color.SkyBlue;
                        r.Cells["性別"].Style.BackColor = Color.SkyBlue;
                        r.Cells["年齢"].Style.BackColor = Color.SkyBlue;
                    }
                }

                foreach (DataGridViewRow r in this.ListView3.Rows)
                {
                    if (r.Cells["性別"].Value.ToString().Equals("女"))
                    {
                        r.Cells["ID"].Style.ForeColor = Color.Red;
                        r.Cells["カナ"].Style.ForeColor = Color.Red;
                        r.Cells["氏名"].Style.ForeColor = Color.Red;
                        r.Cells["性別"].Style.ForeColor = Color.Red;
                        r.Cells["年齢"].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        r.Cells["ID"].Style.ForeColor = Color.Black;
                        r.Cells["カナ"].Style.ForeColor = Color.Black;
                        r.Cells["氏名"].Style.ForeColor = Color.Black;
                        r.Cells["性別"].Style.ForeColor = Color.Black;
                        r.Cells["年齢"].Style.ForeColor = Color.Black;
                    }

                    if (r.Cells["面会状況"].Value.ToString().Length > 0)
                    {
                        r.Cells["ID"].Style.BackColor = Color.SkyBlue;
                        r.Cells["カナ"].Style.BackColor = Color.SkyBlue;
                        r.Cells["氏名"].Style.BackColor = Color.SkyBlue;
                        r.Cells["性別"].Style.BackColor = Color.SkyBlue;
                        r.Cells["年齢"].Style.BackColor = Color.SkyBlue;
                    }
                }
            }
            get
            {
                return this.hide_mode;
            }
        }


        bool tick_mode = true;

        public bool TickMode
        {
            set
            {
                this.tick_mode = value;

                if (this.tick_mode)
                {
                    this.TickModeButton1.Text = "自動更新";
                    this.TickModeButton1.BackColor = Color.LightYellow;
                    this.TickModeButton1.ForeColor = Color.Red;

                    this.TickIntervalBox1.Enabled = true;

                    this.Timer1.Start();
                }
                else
                {
                    this.TickModeButton1.Text = "手動更新";
                    this.TickModeButton1.BackColor = Color.White;
                    this.TickModeButton1.ForeColor = Color.Black;

                    this.TickIntervalBox1.Enabled = false;

                    this.Timer1.Stop();
                }
            }
            get
            {
                return this.tick_mode;
            }
        }


        public FormByotoList()
        {
            InitializeComponent();
        }

        private void FormByotoList_Load(object sender, EventArgs e)
        {
            DataTable table = dSet.Tables.Add("入院");
            table.Columns.Add("病棟コード");
            table.Columns.Add("病棟");
            table.Columns.Add("病室");
            table.Columns.Add("ID");
            table.Columns.Add("カナ");
            table.Columns.Add("氏名");
            table.Columns.Add("性別");
            table.Columns.Add("年齢", typeof(int));
            table.Columns.Add("科コード");
            table.Columns.Add("科");
            table.Columns.Add("医師コード");
            table.Columns.Add("医師");
            table.Columns.Add("入院日");
            table.Columns.Add("入院日8");
            table.Columns.Add("入院区分");
            table.Columns.Add("転棟日");
            table.Columns.Add("転棟日8");
            table.Columns.Add("退院予定日");
            table.Columns.Add("退院予定時間");
            table.Columns.Add("入院日数");
            table.Columns.Add("保険パターン番号");
            table.Columns.Add("保険種別コード");
            table.Columns.Add("保険");
            table.Columns.Add("サマリ");
            table.Columns.Add("DPC病名");
            table.Columns.Add("面会状況");
            table.Columns.Add("DR1");
            table.Columns.Add("NS1");
            table.Columns.Add("様式1");
            table.Columns.Add("レセ1");
            table.Columns.Add("DPC1");
            table.Columns.Add("コメント1");
            table.Columns.Add("DR2");
            table.Columns.Add("NS2");
            table.Columns.Add("様式2");
            table.Columns.Add("レセ2");
            table.Columns.Add("DPC2");
            table.Columns.Add("コメント2");
            table.Columns.Add("DR3");
            table.Columns.Add("NS3");
            table.Columns.Add("様式3");
            table.Columns.Add("レセ3");
            table.Columns.Add("DPC3");
            table.Columns.Add("コメント3");
            table.Columns.Add("Obj", typeof(PatIn));

            table = dSet.Tables.Add("退院");
            table.Columns.Add("病棟コード");
            table.Columns.Add("病棟");
            table.Columns.Add("病室");
            table.Columns.Add("ID");
            table.Columns.Add("カナ");
            table.Columns.Add("氏名");
            table.Columns.Add("性別");
            table.Columns.Add("年齢", typeof(int));
            table.Columns.Add("科コード");
            table.Columns.Add("科");
            table.Columns.Add("医師コード");
            table.Columns.Add("医師");
            table.Columns.Add("入院日");
            table.Columns.Add("入院日8");
            table.Columns.Add("入院区分");
            table.Columns.Add("退院日");
            table.Columns.Add("退院区分");
            table.Columns.Add("入院日数");
            table.Columns.Add("保険パターン番号");
            table.Columns.Add("保険種別コード");
            table.Columns.Add("保険");
            table.Columns.Add("サマリ");
            table.Columns.Add("DPC病名");
            table.Columns.Add("面会状況");
            table.Columns.Add("DR1");
            table.Columns.Add("NS1");
            table.Columns.Add("様式1");
            table.Columns.Add("レセ1");
            table.Columns.Add("DPC1");
            table.Columns.Add("コメント1");
            table.Columns.Add("DR2");
            table.Columns.Add("NS2");
            table.Columns.Add("様式2");
            table.Columns.Add("レセ2");
            table.Columns.Add("DPC2");
            table.Columns.Add("コメント2");
            table.Columns.Add("DR3");
            table.Columns.Add("NS3");
            table.Columns.Add("様式3");
            table.Columns.Add("レセ3");
            table.Columns.Add("DPC3");
            table.Columns.Add("コメント3");
            table.Columns.Add("Obj", typeof(PatIn));

            table = dSet.Tables.Add("入院予定");
            table.Columns.Add("病棟コード");
            table.Columns.Add("病棟");
            table.Columns.Add("病室");
            table.Columns.Add("ID");
            table.Columns.Add("カナ");
            table.Columns.Add("氏名");
            table.Columns.Add("性別");
            table.Columns.Add("年齢", typeof(int));
            table.Columns.Add("科コード");
            table.Columns.Add("科");
            table.Columns.Add("医師コード");
            table.Columns.Add("医師");
            table.Columns.Add("入院予定日");
            table.Columns.Add("入院日8");
            table.Columns.Add("入院予定時間");
            table.Columns.Add("入院区分");
            table.Columns.Add("退院予定日");
            table.Columns.Add("退院予定時間");
            table.Columns.Add("入院日数");
            table.Columns.Add("保険パターン番号");
            table.Columns.Add("保険種別コード");
            table.Columns.Add("保険");
            table.Columns.Add("DPC病名");
            table.Columns.Add("面会状況");
            table.Columns.Add("DR1");
            table.Columns.Add("NS1");
            table.Columns.Add("様式1");
            table.Columns.Add("レセ1");
            table.Columns.Add("DPC1");
            table.Columns.Add("コメント1");
            table.Columns.Add("DR2");
            table.Columns.Add("NS2");
            table.Columns.Add("様式2");
            table.Columns.Add("レセ2");
            table.Columns.Add("DPC2");
            table.Columns.Add("コメント2");
            table.Columns.Add("DR3");
            table.Columns.Add("NS3");
            table.Columns.Add("様式3");
            table.Columns.Add("レセ3");
            table.Columns.Add("DPC3");
            table.Columns.Add("コメント3");
            table.Columns.Add("Obj", typeof(PatIn));

            this.WardBox1.Items.Add("");
            this.WardBox1.Items.Add("3 わかば");
            this.WardBox1.Items.Add("4 さくら");
            this.WardBox1.Items.Add("5 あやめ");

            this.DatePicker21.Value = DateTime.Now.AddDays(-14);
            this.DatePicker22.Value = DateTime.Now.AddDays(7);

            this.ProblemSectionBox1.Init(true, false, true, false);

            // 更新間隔（分）
            this.TickIntervalBox1.Items.Add(5);
            this.TickIntervalBox1.Items.Add(10);
            this.TickIntervalBox1.Items.Add(30);
            this.TickIntervalBox1.Text = "10";


            // 病名は「現病名」
            this.DiagCheckBox1.Checked = true;

            // プロブレムは外来・未解決のみ（チェックボックスは OFF）


            // リストの説明書き
            if (this.UserMode1 == UserMode.Dpc)
            {
                this.TabLabel1.Text = "DR/NS/様1/レセ/DPC　　●完了, △未完了";
                this.TabLabel2.Text = "DR/NS/様1/レセ/DPC　　●完了, △未完了";
                this.TabLabel3.Text = "DR/NS/様1/レセ/DPC　　●完了, △未完了";
            }
            else
            {
                this.TabLabel1.Text = "";
                this.TabLabel2.Text = "";
                this.TabLabel3.Text = "";

                this.TabLabel1.Visible = false;
                this.TabLabel2.Visible = false;
                this.TabLabel3.Visible = false;
            }

            this.DetailBox1.Checked = false;
            this.DetailMode = false;

            // デフォルトで「病棟医事」モードにする
            UserModeBox1.Items.Add("医師");
            UserModeBox1.Items.Add("看護部");
            UserModeBox1.Items.Add("病棟医事");
            UserModeBox1.Items.Add("DPC");

            if (LoginUser.IsDPC)
            {
                this.UserModeBox1.Text = "DPC";
                this.UserMode1 = UserMode.Dpc;
            }
            else
            {
                this.UserModeBox1.Text = "病棟医事";
                this.UserMode1 = UserMode.Iji2;
            }

            this.CSVButton1.Click += new EventHandler(CSVButton_Click);
            this.CSVButton2.Click += new EventHandler(CSVButton_Click);
            this.CSVButton3.Click += new EventHandler(CSVButton_Click);

            this.ListShow();

            this.TickMode = true;
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            this.Pat.Id = p.Id;
            this.PtDataShow();
        }

        public override void FontSet(AppFont f)
        {
            base.FontSet(f);

            this.stdControlFont11.FontSet(f);

            this.ListFormat();
        }

        private void ShowButton2_Click(object sender, EventArgs e)
        {
            this.ListShow2();
        }

        private void DateBox11_ValueChanged(object sender, EventArgs e)
        {
            this.ListFormat1();
        }

        private void DateBox31_ValueChanged(object sender, EventArgs e)
        {
            this.ListFormat3();
        }

        private void FormByotoList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                this.ListShow();
            }
        }

        public void ListShow()
        {
            if (this.TabControl1.SelectedTab.Name.Equals("TabPage1"))
            {
                this.ListShow1();
            }
            else if (this.TabControl1.SelectedTab.Name.Equals("TabPage2"))
            {
                this.ListShow2();
            }
            else if (this.TabControl1.SelectedTab.Name.Equals("TabPage3"))
            {
                this.ListShow3();
            }
        }

        void ListShow1()
        {
            // 元の選択患者・入院日・スクロール位置を取得
            string pt_id = "";
            string in_date = "";
            int y = this.ListView1.FirstDisplayedScrollingRowIndex;

            if (this.ListView1.SelectedRows.Count > 0)
            {
                pt_id = this.ListView1.SelectedRows[0].Cells["ID"].Value.ToString();
                in_date = this.ListView1.SelectedRows[0].Cells["入院日8"].Value.ToString();
            }

            // 入院患者リスト
            List<PatIn> list = PatIn.GetList();

            DataTable table = dSet.Tables["入院"];
            table.Rows.Clear();

            if (table.Columns.Count > 45)
            {
                for (int i = table.Columns.Count - 1; i >= 45; i--)
                {
                    table.Columns.RemoveAt(i);
                }
            }

            for (int i = 0; i < 14; i++)
            {
                DateTime dt = DateTime.Now.AddDays(i - 10);
                table.Columns.Add(dt.ToString("yyyyMMdd"));
            }

            List<DiagDPC> diag_list = DiagDPC.GetList(list);
            List<DischargeSummary> discharge_list = DischargeSummary.GetList(list);

            List<string> pt_list = new List<string>();

            // 前日
            int yesterday = int.Parse(DateTime.Now.AddDays(-1).ToString("yyyyMMdd"));

            foreach (PatIn obj in list)
            {
                DataRow r = table.NewRow();

                r["病棟コード"] = obj.Ward;
                r["病棟"] = obj.WardName;
                r["病室"] = obj.Room;
                r["ID"] = obj.Id;
                r["カナ"] = obj.Kana;
                r["氏名"] = obj.Name;
                r["性別"] = obj.SexNameShort;
                r["年齢"] = obj.Age;
                r["科コード"] = obj.Dept;
                r["科"] = obj.DeptName;
                r["医師コード"] = obj.Doctor;
                r["医師"] = obj.DoctorName;
                r["入院日"] = obj.InDateString;
                r["入院日8"] = obj.InDate;
                r["入院区分"] = obj.InKindString;
                r["退院予定日"] = obj.OutDateString;
                r["退院予定時間"] = obj.OutTimeString;
                r["入院日数"] = obj.Days;
                r["保険パターン番号"] = obj.Ins;
                r["保険種別コード"] = obj.InsKind;
                r["保険"] = obj.InsKindName;

                foreach (DischargeSummary d in discharge_list)
                {
                    if (d.PtId.Equals(obj.Id))
                    {
                        if (d.InputCheck.Equals(1))
                        {
                            r["サマリ"] = "▲ " + DateTime.Parse(DateTimeAgent.DateFormat((d.UpDate > d.RegDate ? d.UpDate : d.RegDate), DateTimeAgent.DateFormatKind.LONG)).ToString("M/dd").PadLeft(5, ' ');
                        }
                        else if (d.InputCheck.Equals(2))
                        {
                            r["サマリ"] = "● " + DateTime.Parse(DateTimeAgent.DateFormat((d.UpDate > d.RegDate ? d.UpDate : d.RegDate), DateTimeAgent.DateFormatKind.LONG)).ToString("M/dd").PadLeft(5, ' ');
                        }
                        else
                        {
                            r["サマリ"] = "△ " + DateTime.Parse(DateTimeAgent.DateFormat((d.UpDate > d.RegDate ? d.UpDate : d.RegDate), DateTimeAgent.DateFormatKind.LONG)).ToString("M/dd").PadLeft(5, ' ');
                        }

                        break;
                    }
                }

                // DPC主病名がチェックされているか
                bool diag_flg = false;

                // 最終登録日時
                int diag_date = 0;

                foreach (DiagDPC d in diag_list)
                {
                    // 削除されていれば飛ばす
                    if (d.DeleteFlg) continue;

                    if (d.PtId.Equals(obj.Id))
                    {
                        // DPC主病名がチェックされている場合
                        if (d.MainFlg)
                        {
                            diag_flg = true;
                        }

                        // 最終登録日時
                        if (d.RegDate >= diag_date || d.UpDate >= diag_date)
                        {
                            diag_date = d.UpDate > d.RegDate ? d.UpDate : d.RegDate;
                        }
                    }
                }

                // DPC主病名がチェックされている
                if (diag_flg)
                {
                    r["DPC病名"] = "○ " + DateTime.Parse(DateTimeAgent.DateFormat(diag_date, DateTimeAgent.DateFormatKind.LONG)).ToString("M/dd").PadLeft(5, ' ');
                }

                r["Obj"] = obj;

                table.Rows.Add(r);

                pt_list.Add(obj.Id);
            }


            List<DPCData> list2 = DPCData.GetList(pt_list);
            Dictionary<string, List<PatIn>> dict = PatIn.GetRoomDict();

            foreach (DataRow r in table.Rows)
            {
                foreach (DPCData data in list2)
                {
                    if (r["ID"].ToString().Equals(data.Id) && r["入院日8"].ToString().Equals(data.AdmDate))
                    {
                        if (data.Kind.Equals("11"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["様式1"] = "●";
                            }
                            else if (data.Status.Equals("2"))
                            {
                                r["様式1"] = "△";
                            }
                        }
                        else if (data.Kind.Equals("13"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["DR1"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("14"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["NS1"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("15"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["レセ1"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("16"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["DPC1"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("17"))
                        {
                            r["コメント1"] = data.Cont;
                        }
                        else if (data.Kind.Equals("21"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["様式2"] = "●";
                            }
                            else if (data.Status.Equals("2"))
                            {
                                r["様式2"] = "△";
                            }
                        }
                        else if (data.Kind.Equals("23"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["DR2"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("24"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["NS2"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("25"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["レセ2"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("26"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["DPC2"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("27"))
                        {
                            r["コメント2"] = data.Cont;
                        }
                        else if (data.Kind.Equals("31"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["様式3"] = "●";
                            }
                            else if (data.Status.Equals("3"))
                            {
                                r["様式3"] = "△";
                            }
                        }
                        else if (data.Kind.Equals("33"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["DR3"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("34"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["NS3"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("35"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["レセ3"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("36"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["DPC3"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("37"))
                        {
                            r["コメント3"] = data.Cont;
                        }
                        else if (data.Kind.Equals("8"))
                        {
                            r["面会状況"] = data.Cont;
                        }
                    }
                }

                if (dict.ContainsKey(r["ID"].ToString()))
                {
                    List<PatIn> list3 = dict[r["ID"].ToString()];

                    int date1 = 0;
                    int date2 = 0;

                    if (r["入院日8"].ToString().Length > 0)
                    {
                        date1 = int.Parse(r["入院日8"].ToString());
                    }

                    if (r["退院予定日"].ToString().Length > 0)
                    {
                        date2 = DateTimeAgent.DateToInt(r["退院予定日"].ToString());
                    }

                    PatIn tmp0 = new PatIn();

                    foreach (PatIn tmp in list3)
                    {
                        for (int i = 0; i < 14; i++)
                        {
                            int d = int.Parse(DateTime.Now.AddDays(i - 10).ToString("yyyyMMdd"));

                            // 退院後なら終了
                            if (date2 > 0 && d > date2)
                            {
                                break;
                            }

                            // 入院前なら飛ばす
                            if (d < date1)
                            {
                                continue;
                            }

                            // 移動日の翌日以降であればいったん消す。
                            // 移動日当日ならば消さない。
                            if (d > tmp.DoDateInt)
                            {
                                r[d.ToString()] = "";
                            }

                            if (d >= tmp.DoDateInt)
                            {
                                if (r[d.ToString()].ToString().Length > 0)
                                {
                                    r[d.ToString()] += "-";
                                }

                                r[d.ToString()] += tmp.Room;
                            }
                        }

                        // 転棟した場合は最終転棟日をセット
                        if (tmp0.Ward.Length > 0 && !tmp.Ward.Equals(tmp0.Ward))
                        {
                            r["転棟日"] = tmp.DoDateString;
                            r["転棟日8"] = tmp.DoDateInt;
                        }

                        tmp0 = tmp;
                    }
                }
            }

            this.ListFormat1();

            // 元の選択患者・入院日・スクロール位置をセット
            if (pt_id.Length > 0 && in_date.Length > 0)
            {
                foreach (DataGridViewRow r in this.ListView1.Rows)
                {
                    if (r.Cells["ID"].Value.ToString().Equals(pt_id) &&
                        r.Cells["入院日8"].Value.ToString().Equals(in_date))
                    {
                        r.Selected = true;
                        break;
                    }
                }
            }

            if (y >= 0 && y < this.ListView1.RowCount)
            {
                this.ListView1.FirstDisplayedScrollingRowIndex = y;
            }
        }

        void ListFormat1()
        {
            DataView view = new DataView(dSet.Tables["入院"]);

            if (view.Count == 0)
            {
                return;
            }

            List<string> filters = new List<string>();

            if (this.WardBox1.Text.Contains(" "))
            {
                filters.Add("病棟コード = '" + this.WardBox1.Text.Split(' ')[0] + "'");
            }

            // キーワード
            if (this.KeywordBox1.Text.Length > 0)
            {
                string keyword = KeywordBox1.Text;

                filters.Add("(ID like '%" + keyword + "%' or 氏名 like '%" + keyword + "%' or カナ like '%" + keyword + "%' or 病室 like '%" + keyword + "%')");
            }

            if (this.DateBox11.DateInt > 0)
            {
                filters.Add("入院日8 = " + this.DateBox11.DateInt);
            }

            string filter = AppString.ConcatList(filters, " and ");

            view.RowFilter = filter;

            if (this.ListViewSort1.Length > 0)
            {
                view.Sort = this.ListViewSort1;
            }
            else
            {
                view.Sort = "病棟コード, 病室";
            }

            if (this.ListViewSortOrder1 == SortOrder.Descending)
            {
                view.Sort += " desc";
            }

            this.CountLabel1.Text = view.Count + " 名";

            try
            {
                ListView1.Columns.Clear();
                ListView1.DataSource = view;

                ListView1.Columns["病棟コード"].Visible = false;

                ListView1.Columns["病棟"].HeaderText = "病棟";
                ListView1.Columns["病棟"].Width = 45;
                ListView1.Columns["病棟"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView1.Columns["病室"].HeaderText = "病室";
                ListView1.Columns["病室"].Width = 35;
                ListView1.Columns["病室"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView1.Columns["ID"].HeaderText = "ID";
                ListView1.Columns["ID"].Width = 55;
                ListView1.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                ListView1.Columns["カナ"].Width = 70;

                ListView1.Columns["氏名"].Width = 90;

                ListView1.Columns["性別"].HeaderText = "性別";
                ListView1.Columns["性別"].Width = 35;
                ListView1.Columns["性別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView1.Columns["年齢"].HeaderText = "年齢";
                ListView1.Columns["年齢"].Width = 35;
                ListView1.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView1.Columns["科コード"].Visible = false;

                ListView1.Columns["科"].HeaderText = "科";
                ListView1.Columns["科"].Width = 50;
                ListView1.Columns["科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                ListView1.Columns["医師コード"].Visible = false;

                ListView1.Columns["医師"].HeaderText = "医師";
                ListView1.Columns["医師"].Width = 70;
                ListView1.Columns["医師"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                ListView1.Columns["入院日"].HeaderText = "入院日";
                ListView1.Columns["入院日"].Width = 70;
                ListView1.Columns["入院日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView1.Columns["入院日8"].Visible = false;

                ListView1.Columns["入院区分"].HeaderText = "区分";
                ListView1.Columns["入院区分"].Width = 35;
                ListView1.Columns["入院区分"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView1.Columns["転棟日"].HeaderText = "転棟日";
                ListView1.Columns["転棟日"].Width = 70;
                ListView1.Columns["転棟日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView1.Columns["転棟日"].ToolTipText = "最終転棟日";

                ListView1.Columns["転棟日8"].Visible = false;

                ListView1.Columns["退院予定日"].HeaderText = "退院日";
                ListView1.Columns["退院予定日"].Width = 70;
                ListView1.Columns["退院予定日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView1.Columns["退院予定時間"].HeaderText = "時間";
                ListView1.Columns["退院予定時間"].Width = 35;
                ListView1.Columns["退院予定時間"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView1.Columns["入院日数"].HeaderText = "日数";
                ListView1.Columns["入院日数"].Width = 25;
                ListView1.Columns["入院日数"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView1.Columns["保険パターン番号"].Visible = false;
                ListView1.Columns["保険種別コード"].Visible = false;

                ListView1.Columns["保険"].HeaderText = "保険";
                ListView1.Columns["保険"].Width = 35;
                ListView1.Columns["保険"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView1.Columns["保険"].Visible = false;

                ListView1.Columns["サマリ"].Width = 55;
                ListView1.Columns["サマリ"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView1.Columns["サマリ"].ToolTipText = "▲ 医師, ● 管理士, △ 未チェック";

                ListView1.Columns["DPC病名"].Width = 55;
                ListView1.Columns["DPC病名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView1.Columns["DPC病名"].ToolTipText = "○ DPC主病名あり";

                ListView1.Columns["面会状況"].HeaderText = "面会";
                ListView1.Columns["面会状況"].Width = 45;
                ListView1.Columns["面会状況"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView1.Columns["面会状況"].Frozen = true;
                ListView1.Columns["面会状況"].DividerWidth = 1;

                ListView1.Columns["DR1"].HeaderText = "DR";
                ListView1.Columns["DR1"].Width = 35;
                ListView1.Columns["DR1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView1.Columns["NS1"].HeaderText = "NS";
                ListView1.Columns["NS1"].Width = 35;
                ListView1.Columns["NS1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView1.Columns["様式1"].HeaderText = "様1";
                ListView1.Columns["様式1"].Width = 35;
                ListView1.Columns["様式1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView1.Columns["レセ1"].HeaderText = "レセ";
                ListView1.Columns["レセ1"].Width = 35;
                ListView1.Columns["レセ1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView1.Columns["DPC1"].HeaderText = "DPC";
                ListView1.Columns["DPC1"].Width = 35;
                ListView1.Columns["DPC1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView1.Columns["コメント1"].HeaderText = "DPCコメント";
                ListView1.Columns["コメント1"].Width = 100;
                ListView1.Columns["コメント1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                ListView1.Columns["DR2"].HeaderText = "DR";
                ListView1.Columns["DR2"].Width = 35;
                ListView1.Columns["DR2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView1.Columns["DR2"].DefaultCellStyle.BackColor = Color.LightYellow;

                ListView1.Columns["NS2"].HeaderText = "NS";
                ListView1.Columns["NS2"].Width = 35;
                ListView1.Columns["NS2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView1.Columns["NS2"].DefaultCellStyle.BackColor = Color.LightYellow;

                ListView1.Columns["様式2"].HeaderText = "様1";
                ListView1.Columns["様式2"].Width = 35;
                ListView1.Columns["様式2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView1.Columns["様式2"].DefaultCellStyle.BackColor = Color.LightYellow;

                ListView1.Columns["レセ2"].HeaderText = "レセ";
                ListView1.Columns["レセ2"].Width = 35;
                ListView1.Columns["レセ2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView1.Columns["レセ2"].DefaultCellStyle.BackColor = Color.LightYellow;

                ListView1.Columns["DPC2"].HeaderText = "DPC";
                ListView1.Columns["DPC2"].Width = 35;
                ListView1.Columns["DPC2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView1.Columns["DPC2"].DefaultCellStyle.BackColor = Color.LightYellow;

                ListView1.Columns["コメント2"].HeaderText = "DPCコメント";
                ListView1.Columns["コメント2"].Width = 100;
                ListView1.Columns["コメント2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                ListView1.Columns["コメント2"].DefaultCellStyle.BackColor = Color.LightYellow;

                ListView1.Columns["DR3"].HeaderText = "DR";
                ListView1.Columns["DR3"].Width = 35;
                ListView1.Columns["DR3"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView1.Columns["DR3"].DefaultCellStyle.BackColor = Color.LightCyan;

                ListView1.Columns["NS3"].HeaderText = "NS";
                ListView1.Columns["NS3"].Width = 35;
                ListView1.Columns["NS3"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView1.Columns["NS3"].DefaultCellStyle.BackColor = Color.LightCyan;

                ListView1.Columns["様式3"].HeaderText = "様1";
                ListView1.Columns["様式3"].Width = 35;
                ListView1.Columns["様式3"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView1.Columns["様式3"].DefaultCellStyle.BackColor = Color.LightCyan;

                ListView1.Columns["レセ3"].HeaderText = "レセ";
                ListView1.Columns["レセ3"].Width = 35;
                ListView1.Columns["レセ3"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView1.Columns["レセ3"].DefaultCellStyle.BackColor = Color.LightCyan;

                ListView1.Columns["DPC3"].HeaderText = "DPC";
                ListView1.Columns["DPC3"].Width = 35;
                ListView1.Columns["DPC3"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView1.Columns["DPC3"].DefaultCellStyle.BackColor = Color.LightCyan;

                ListView1.Columns["コメント3"].HeaderText = "DPCコメント";
                ListView1.Columns["コメント3"].Width = 100;
                ListView1.Columns["コメント3"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                ListView1.Columns["コメント3"].DefaultCellStyle.BackColor = Color.LightCyan;

                ListView1.Columns["Obj"].Visible = false;


                if (this.UserMode1 == UserMode.Dpc)
                {
                    ListView1.Columns["DR1"].Visible = true;
                    ListView1.Columns["NS1"].Visible = true;
                    ListView1.Columns["様式1"].Visible = true;
                    ListView1.Columns["レセ1"].Visible = true;
                    ListView1.Columns["DPC1"].Visible = true;
                    ListView1.Columns["コメント1"].Visible = true;
                    ListView1.Columns["DR2"].Visible = true;
                    ListView1.Columns["NS2"].Visible = true;
                    ListView1.Columns["様式2"].Visible = true;
                    ListView1.Columns["レセ2"].Visible = true;
                    ListView1.Columns["DPC2"].Visible = true;
                    ListView1.Columns["コメント2"].Visible = true;
                    ListView1.Columns["DR3"].Visible = true;
                    ListView1.Columns["NS3"].Visible = true;
                    ListView1.Columns["様式3"].Visible = true;
                    ListView1.Columns["レセ3"].Visible = true;
                    ListView1.Columns["DPC3"].Visible = true;
                    ListView1.Columns["コメント3"].Visible = true;
                }
                else
                {
                    ListView1.Columns["DR1"].Visible = false;
                    ListView1.Columns["NS1"].Visible = false;
                    ListView1.Columns["様式1"].Visible = false;
                    ListView1.Columns["レセ1"].Visible = false;
                    ListView1.Columns["DPC1"].Visible = false;
                    ListView1.Columns["コメント1"].Visible = false;
                    ListView1.Columns["DR2"].Visible = false;
                    ListView1.Columns["NS2"].Visible = false;
                    ListView1.Columns["様式2"].Visible = false;
                    ListView1.Columns["レセ2"].Visible = false;
                    ListView1.Columns["DPC2"].Visible = false;
                    ListView1.Columns["コメント2"].Visible = false;
                    ListView1.Columns["DR3"].Visible = false;
                    ListView1.Columns["NS3"].Visible = false;
                    ListView1.Columns["様式3"].Visible = false;
                    ListView1.Columns["レセ3"].Visible = false;
                    ListView1.Columns["DPC3"].Visible = false;
                    ListView1.Columns["コメント3"].Visible = false;
                }

                for (int i = 0; i < 14; i++)
                {
                    DateTime dt = DateTime.Now.AddDays(i - 10);

                    if (ListView1.Columns.Contains(dt.ToString("yyyyMMdd")))
                    {
                        ListView1.Columns[dt.ToString("yyyyMMdd")].HeaderText = dt.ToString("M/d(ddd)");
                        ListView1.Columns[dt.ToString("yyyyMMdd")].Width = 65;
                    }
                }

                foreach (DataGridViewRow r in this.ListView1.Rows)
                {
                    string code = r.Cells["病棟コード"].Value.ToString();

                    if (Dict.WardDict.ContainsKey(code))
                    {
                        r.Cells["病棟"].Style.BackColor = Dict.WardDict[code].BackColor;
                    }

                    r.Cells["ID"].ToolTipText = r.Cells["ID"].Value.ToString();
                    r.Cells["カナ"].ToolTipText = r.Cells["カナ"].Value.ToString();
                    r.Cells["氏名"].ToolTipText = r.Cells["氏名"].Value.ToString();
                    r.Cells["性別"].ToolTipText = r.Cells["性別"].Value.ToString();
                    r.Cells["年齢"].ToolTipText = r.Cells["年齢"].Value.ToString();

                    for (int i = 0; i < 14; i++)
                    {
                        DateTime dt = DateTime.Now.AddDays(i - 10);

                        if (ListView1.Columns.Contains(dt.ToString("yyyyMMdd")) &&
                            r.Cells[dt.ToString("yyyyMMdd")].Value.ToString().Contains("-"))
                        {
                            r.Cells[dt.ToString("yyyyMMdd")].Style.ForeColor = Color.Red;
                        }
                    }
                }

                // 非表示の設定
                this.HideMode = !this.HideBox1.Checked;

                if (this.Font.Size > 9)
                {
                    foreach (DataGridViewColumn c in ListView1.Columns)
                    {
                        c.Width = (int)(c.Width * (float)(this.Font.Size / 9));
                    }
                }
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex);
            }
        }

        void ListShow2()
        {
            // 元の選択患者・入院日・スクロール位置を取得
            string pt_id = "";
            string in_date = "";
            int y = this.ListView2.FirstDisplayedScrollingRowIndex;

            if (this.ListView2.SelectedRows.Count > 0)
            {
                pt_id = this.ListView2.SelectedRows[0].Cells["ID"].Value.ToString();
                in_date = this.ListView2.SelectedRows[0].Cells["入院日8"].Value.ToString();
            }

            int date21 = int.Parse(DatePicker21.Value.ToString("yyyyMMdd"));
            int date22 = int.Parse(DatePicker22.Value.ToString("yyyyMMdd"));

            if (date21 > date22)
            {
                MessageBox.Show("開始日が終了日より後になっています。日付を選択しなおしてください。");
                return;
            }
            else if (DateTimeAgent.AddMonths(date21, 3) < date22)
            {
                MessageBox.Show("対象期間（開始日と終了日の間）が３か月を超えています。負荷が重くなりますので、３か月以内に変更をお願いします。");
                return;
            }

            // 退院患者リスト
            List<PatIn> list = PatIn.GetOutList(DatePicker21.Value.ToString("yyyyMMdd"), DatePicker22.Value.ToString("yyyyMMdd"), "", "");

            DataTable table = dSet.Tables["退院"];
            table.Rows.Clear();

            List<DiagDPC> diag_list = DiagDPC.GetList(list);
            List<DischargeSummary> discharge_list = DischargeSummary.GetList(list);

            List<string> pt_list = new List<string>();

            // 前日
            int yesterday = int.Parse(DateTime.Now.AddDays(-1).ToString("yyyyMMdd"));

            foreach (PatIn obj in list)
            {
                DataRow r = table.NewRow();

                r["病棟コード"] = obj.Ward;
                r["病棟"] = obj.WardName;
                r["病室"] = obj.Room;
                r["ID"] = obj.Id;
                r["カナ"] = obj.Kana;
                r["氏名"] = obj.Name;
                r["性別"] = obj.SexNameShort;
                r["年齢"] = obj.Age;
                r["科コード"] = obj.Dept;
                r["科"] = obj.DeptName;
                r["医師コード"] = obj.Doctor;
                r["医師"] = obj.DoctorName;
                r["入院日"] = obj.InDateString;
                r["入院日8"] = obj.InDate;
                r["入院区分"] = obj.InKindString;
                r["退院日"] = obj.OutDateString;
                r["退院区分"] = obj.OutKindString;
                r["入院日数"] = obj.Days;
                r["Obj"] = obj;

                foreach (DischargeSummary d in discharge_list)
                {
                    if (d.PtId.Equals(obj.Id))
                    {
                        if (d.InputCheck.Equals(1))
                        {
                            r["サマリ"] = "▲ " + DateTime.Parse(DateTimeAgent.DateFormat((d.UpDate > d.RegDate ? d.UpDate : d.RegDate), DateTimeAgent.DateFormatKind.LONG)).ToString("M/dd").PadLeft(5, ' ');
                        }
                        else if (d.InputCheck.Equals(2))
                        {
                            r["サマリ"] = "● " + DateTime.Parse(DateTimeAgent.DateFormat((d.UpDate > d.RegDate ? d.UpDate : d.RegDate), DateTimeAgent.DateFormatKind.LONG)).ToString("M/dd").PadLeft(5, ' ');
                        }
                        else
                        {
                            r["サマリ"] = "△ " + DateTime.Parse(DateTimeAgent.DateFormat((d.UpDate > d.RegDate ? d.UpDate : d.RegDate), DateTimeAgent.DateFormatKind.LONG)).ToString("M/dd").PadLeft(5, ' ');
                        }

                        break;
                    }
                }

                // DPC主病名がチェックされているか
                bool diag_flg = false;

                // 最終登録日時
                int diag_date = 0;

                foreach (DiagDPC d in diag_list)
                {
                    // 削除されていれば飛ばす
                    if (d.DeleteFlg) continue;

                    if (d.PtId.Equals(obj.Id))
                    {
                        // DPC主病名がチェックされている場合
                        if (d.MainFlg)
                        {
                            diag_flg = true;
                        }

                        // 最終登録日時
                        if (d.RegDate >= diag_date || d.UpDate >= diag_date)
                        {
                            diag_date = d.UpDate > d.RegDate ? d.UpDate : d.RegDate;
                        }
                    }
                }

                // DPC主病名がチェックされている
                if (diag_flg)
                {
                    r["DPC病名"] = "○ " + DateTime.Parse(DateTimeAgent.DateFormat(diag_date, DateTimeAgent.DateFormatKind.LONG)).ToString("M/dd").PadLeft(5, ' ');
                }

                table.Rows.Add(r);

                pt_list.Add(obj.Id);
            }


            List<DPCData> list2 = DPCData.GetList(pt_list);

            foreach (DataRow r in table.Rows)
            {
                foreach (DPCData data in list2)
                {
                    if (r["ID"].ToString().Equals(data.Id) && r["入院日8"].ToString().Equals(data.AdmDate))
                    {
                        if (data.Kind.Equals("11"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["様式1"] = "●";
                            }
                            else if (data.Status.Equals("2"))
                            {
                                r["様式1"] = "△";
                            }
                        }
                        else if (data.Kind.Equals("13"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["DR1"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("14"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["NS1"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("15"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["レセ1"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("16"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["DPC1"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("17"))
                        {
                            r["コメント1"] = data.Cont;
                        }
                        else if (data.Kind.Equals("21"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["様式2"] = "●";
                            }
                            else if (data.Status.Equals("2"))
                            {
                                r["様式2"] = "△";
                            }
                        }
                        else if (data.Kind.Equals("23"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["DR2"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("24"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["NS2"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("25"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["レセ2"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("26"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["DPC2"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("27"))
                        {
                            r["コメント2"] = data.Cont;
                        }
                        else if (data.Kind.Equals("31"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["様式3"] = "●";
                            }
                            else if (data.Status.Equals("2"))
                            {
                                r["様式3"] = "△";
                            }
                        }
                        else if (data.Kind.Equals("33"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["DR3"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("34"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["NS3"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("35"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["レセ3"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("36"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["DPC3"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("37"))
                        {
                            r["コメント3"] = data.Cont;
                        }
                        else if (data.Kind.Equals("8"))
                        {
                            r["面会状況"] = data.Cont;
                        }
                    }
                }
            }

            this.ListFormat2();

            // 元の選択患者・入院日・スクロール位置をセット
            if (pt_id.Length > 0 && in_date.Length > 0)
            {
                foreach (DataGridViewRow r in this.ListView2.Rows)
                {
                    if (r.Cells["ID"].Value.ToString().Equals(pt_id) &&
                        r.Cells["入院日8"].Value.ToString().Equals(in_date))
                    {
                        r.Selected = true;
                        break;
                    }
                }
            }

            if (y >= 0 && y < this.ListView2.RowCount)
            {
                this.ListView2.FirstDisplayedScrollingRowIndex = y;
            }
        }

        void ListFormat2()
        {
            DataView view = new DataView(dSet.Tables["退院"]);

            if (view.Count == 0)
            {
                return;
            }

            List<string> filters = new List<string>();

            if (this.WardBox1.Text.Contains(" "))
            {
                filters.Add("病棟コード = '" + this.WardBox1.Text.Split(' ')[0] + "'");
            }

            // キーワード
            if (this.KeywordBox1.Text.Length > 0)
            {
                string keyword = KeywordBox1.Text;

                filters.Add("(ID like '%" + keyword + "%' or 氏名 like '%" + keyword + "%' or カナ like '%" + keyword + "%' or 病室 like '%" + keyword + "%')");
            }

            string filter = AppString.ConcatList(filters, " and ");

            view.RowFilter = filter;

            if (this.ListViewSort2.Length > 0)
            {
                view.Sort = this.ListViewSort2;
            }
            else
            {
                view.Sort = "退院日";
            }

            if (this.ListViewSortOrder2 == SortOrder.Descending)
            {
                view.Sort += " desc";
            }

            this.CountLabel2.Text = view.Count + " 名";

            try
            {
                ListView2.DataSource = view;

                ListView2.Columns["病棟コード"].Visible = false;

                ListView2.Columns["病棟"].HeaderText = "病棟";
                ListView2.Columns["病棟"].Width = 45;
                ListView2.Columns["病棟"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView2.Columns["病室"].HeaderText = "病室";
                ListView2.Columns["病室"].Width = 35;
                ListView2.Columns["病室"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView2.Columns["ID"].HeaderText = "ID";
                ListView2.Columns["ID"].Width = 55;
                ListView2.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                ListView2.Columns["カナ"].Width = 70;

                ListView2.Columns["氏名"].Width = 90;

                ListView2.Columns["性別"].HeaderText = "性別";
                ListView2.Columns["性別"].Width = 35;
                ListView2.Columns["性別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView2.Columns["年齢"].HeaderText = "年齢";
                ListView2.Columns["年齢"].Width = 35;
                ListView2.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView2.Columns["科コード"].Visible = false;

                ListView2.Columns["科"].HeaderText = "科";
                ListView2.Columns["科"].Width = 50;
                ListView2.Columns["科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                ListView2.Columns["医師コード"].Visible = false;

                ListView2.Columns["医師"].HeaderText = "医師";
                ListView2.Columns["医師"].Width = 70;
                ListView2.Columns["医師"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                ListView2.Columns["入院日"].HeaderText = "入院日";
                ListView2.Columns["入院日"].Width = 70;
                ListView2.Columns["入院日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView2.Columns["入院日8"].Visible = false;

                ListView2.Columns["入院区分"].HeaderText = "区分";
                ListView2.Columns["入院区分"].Width = 35;
                ListView2.Columns["入院区分"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView2.Columns["退院日"].HeaderText = "退院日";
                ListView2.Columns["退院日"].Width = 70;
                ListView2.Columns["退院日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView2.Columns["退院区分"].HeaderText = "区分";
                ListView2.Columns["退院区分"].Width = 35;
                ListView2.Columns["退院区分"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView2.Columns["入院日数"].HeaderText = "日数";
                ListView2.Columns["入院日数"].Width = 25;
                ListView2.Columns["入院日数"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView2.Columns["保険パターン番号"].Visible = false;
                ListView2.Columns["保険種別コード"].Visible = false;
                ListView2.Columns["保険"].Visible = false;

                ListView2.Columns["サマリ"].Width = 55;
                ListView2.Columns["サマリ"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView2.Columns["サマリ"].ToolTipText = "▲ 医師, ● 管理士, △ 未チェック";

                ListView2.Columns["DPC病名"].Width = 55;
                ListView2.Columns["DPC病名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView2.Columns["DPC病名"].ToolTipText = "○ DPC主病名あり";

                ListView2.Columns["面会状況"].HeaderText = "面会";
                ListView2.Columns["面会状況"].Width = 45;
                ListView2.Columns["面会状況"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView2.Columns["DR1"].HeaderText = "DR";
                ListView2.Columns["DR1"].Width = 35;
                ListView2.Columns["DR1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView2.Columns["NS1"].HeaderText = "NS";
                ListView2.Columns["NS1"].Width = 35;
                ListView2.Columns["NS1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView2.Columns["様式1"].HeaderText = "様1";
                ListView2.Columns["様式1"].Width = 35;
                ListView2.Columns["様式1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView2.Columns["レセ1"].HeaderText = "レセ";
                ListView2.Columns["レセ1"].Width = 35;
                ListView2.Columns["レセ1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView2.Columns["DPC1"].HeaderText = "DPC";
                ListView2.Columns["DPC1"].Width = 35;
                ListView2.Columns["DPC1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView2.Columns["コメント1"].HeaderText = "DPCコメント";
                ListView2.Columns["コメント1"].Width = 100;
                ListView2.Columns["コメント1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                ListView2.Columns["DR2"].HeaderText = "DR";
                ListView2.Columns["DR2"].Width = 35;
                ListView2.Columns["DR2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView2.Columns["DR2"].DefaultCellStyle.BackColor = Color.LightYellow;

                ListView2.Columns["NS2"].HeaderText = "NS";
                ListView2.Columns["NS2"].Width = 35;
                ListView2.Columns["NS2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView2.Columns["NS2"].DefaultCellStyle.BackColor = Color.LightYellow;

                ListView2.Columns["様式2"].HeaderText = "様1";
                ListView2.Columns["様式2"].Width = 35;
                ListView2.Columns["様式2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView2.Columns["様式2"].DefaultCellStyle.BackColor = Color.LightYellow;

                ListView2.Columns["レセ2"].HeaderText = "レセ";
                ListView2.Columns["レセ2"].Width = 35;
                ListView2.Columns["レセ2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView2.Columns["レセ2"].DefaultCellStyle.BackColor = Color.LightYellow;

                ListView2.Columns["DPC2"].HeaderText = "DPC";
                ListView2.Columns["DPC2"].Width = 35;
                ListView2.Columns["DPC2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView2.Columns["DPC2"].DefaultCellStyle.BackColor = Color.LightYellow;

                ListView2.Columns["コメント2"].HeaderText = "DPCコメント";
                ListView2.Columns["コメント2"].Width = 100;
                ListView2.Columns["コメント2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                ListView2.Columns["コメント2"].DefaultCellStyle.BackColor = Color.LightYellow;

                ListView2.Columns["DR3"].HeaderText = "DR";
                ListView2.Columns["DR3"].Width = 35;
                ListView2.Columns["DR3"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView2.Columns["DR3"].DefaultCellStyle.BackColor = Color.LightCyan;

                ListView2.Columns["NS3"].HeaderText = "NS";
                ListView2.Columns["NS3"].Width = 35;
                ListView2.Columns["NS3"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView2.Columns["NS3"].DefaultCellStyle.BackColor = Color.LightCyan;

                ListView2.Columns["様式3"].HeaderText = "様1";
                ListView2.Columns["様式3"].Width = 35;
                ListView2.Columns["様式3"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView2.Columns["様式3"].DefaultCellStyle.BackColor = Color.LightCyan;

                ListView2.Columns["レセ3"].HeaderText = "レセ";
                ListView2.Columns["レセ3"].Width = 35;
                ListView2.Columns["レセ3"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView2.Columns["レセ3"].DefaultCellStyle.BackColor = Color.LightCyan;

                ListView2.Columns["DPC3"].HeaderText = "DPC";
                ListView2.Columns["DPC3"].Width = 35;
                ListView2.Columns["DPC3"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView2.Columns["DPC3"].DefaultCellStyle.BackColor = Color.LightCyan;

                ListView2.Columns["コメント3"].HeaderText = "DPCコメント";
                ListView2.Columns["コメント3"].Width = 100;
                ListView2.Columns["コメント3"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                ListView2.Columns["コメント3"].DefaultCellStyle.BackColor = Color.LightCyan;

                ListView2.Columns["Obj"].Visible = false;

                if (this.UserMode1 == UserMode.Dpc)
                {
                    ListView2.Columns["DR1"].Visible = true;
                    ListView2.Columns["NS1"].Visible = true;
                    ListView2.Columns["様式1"].Visible = true;
                    ListView2.Columns["レセ1"].Visible = true;
                    ListView2.Columns["DPC1"].Visible = true;
                    ListView2.Columns["コメント1"].Visible = true;
                    ListView2.Columns["DR2"].Visible = true;
                    ListView2.Columns["NS2"].Visible = true;
                    ListView2.Columns["様式2"].Visible = true;
                    ListView2.Columns["レセ2"].Visible = true;
                    ListView2.Columns["DPC2"].Visible = true;
                    ListView2.Columns["コメント2"].Visible = true;
                    ListView2.Columns["DR3"].Visible = true;
                    ListView2.Columns["NS3"].Visible = true;
                    ListView2.Columns["様式3"].Visible = true;
                    ListView2.Columns["レセ3"].Visible = true;
                    ListView2.Columns["DPC3"].Visible = true;
                    ListView2.Columns["コメント3"].Visible = true;
                }
                else
                {
                    ListView2.Columns["DR1"].Visible = false;
                    ListView2.Columns["NS1"].Visible = false;
                    ListView2.Columns["様式1"].Visible = false;
                    ListView2.Columns["レセ1"].Visible = false;
                    ListView2.Columns["DPC1"].Visible = false;
                    ListView2.Columns["コメント1"].Visible = false;
                    ListView2.Columns["DR2"].Visible = false;
                    ListView2.Columns["NS2"].Visible = false;
                    ListView2.Columns["様式2"].Visible = false;
                    ListView2.Columns["レセ2"].Visible = false;
                    ListView2.Columns["DPC2"].Visible = false;
                    ListView2.Columns["コメント2"].Visible = false;
                    ListView2.Columns["DR3"].Visible = false;
                    ListView2.Columns["NS3"].Visible = false;
                    ListView2.Columns["様式3"].Visible = false;
                    ListView2.Columns["レセ3"].Visible = false;
                    ListView2.Columns["DPC3"].Visible = false;
                    ListView2.Columns["コメント3"].Visible = false;
                }

                foreach (DataGridViewRow r in this.ListView2.Rows)
                {
                    string code = r.Cells["病棟コード"].Value.ToString();

                    if (Dict.WardDict.ContainsKey(code))
                    {
                        r.Cells["病棟"].Style.BackColor = Dict.WardDict[code].BackColor;
                    }

                    r.Cells["ID"].ToolTipText = r.Cells["ID"].Value.ToString();
                    r.Cells["カナ"].ToolTipText = r.Cells["カナ"].Value.ToString();
                    r.Cells["氏名"].ToolTipText = r.Cells["氏名"].Value.ToString();
                    r.Cells["性別"].ToolTipText = r.Cells["性別"].Value.ToString();
                    r.Cells["年齢"].ToolTipText = r.Cells["年齢"].Value.ToString();
                }

                // 非表示の設定
                this.HideMode = !this.HideBox1.Checked;

                if (this.Font.Size > 9)
                {
                    foreach (DataGridViewColumn c in ListView2.Columns)
                    {
                        c.Width = (int)(c.Width * (float)(this.Font.Size / 9));
                    }
                }
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex);
            }
        }

        void ListShow3()
        {
            // 元の選択患者・入院日・スクロール位置を取得
            string pt_id = "";
            string in_date = "";
            int y = this.ListView3.FirstDisplayedScrollingRowIndex;

            if (this.ListView3.SelectedRows.Count > 0)
            {
                pt_id = this.ListView3.SelectedRows[0].Cells["ID"].Value.ToString();
                in_date = this.ListView3.SelectedRows[0].Cells["入院日8"].Value.ToString();
            }

            // 入院予定患者リスト
            List<PatIn> list = PatIn.GetYoteiList("", "", "");

            DataTable table = dSet.Tables["入院予定"];
            table.Rows.Clear();

            List<DiagDPC> diag_list = DiagDPC.GetList(list);

            List<string> pt_list = new List<string>();

            // 前日
            int yesterday = int.Parse(DateTime.Now.AddDays(-1).ToString("yyyyMMdd"));

            foreach (PatIn obj in list)
            {
                DataRow r = table.NewRow();

                r["病棟コード"] = obj.Ward;
                r["病棟"] = obj.WardName;
                r["病室"] = obj.Room;
                r["ID"] = obj.Id;
                r["カナ"] = obj.Kana;
                r["氏名"] = obj.Name;
                r["性別"] = obj.SexNameShort;
                r["年齢"] = obj.Age;
                r["科コード"] = obj.Dept;
                r["科"] = obj.DeptName;
                r["医師コード"] = obj.Doctor;
                r["医師"] = obj.DoctorName;
                r["入院予定日"] = obj.InDateString;
                r["入院日8"] = obj.InDate;
                r["入院予定時間"] = obj.InTimeString;
                r["入院区分"] = obj.InKindString;
                r["退院予定日"] = obj.OutDateString;
                r["退院予定時間"] = obj.OutTimeString;
                r["入院日数"] = obj.Days;
                r["Obj"] = obj;

                // DPC主病名がチェックされているか
                bool diag_flg = false;

                // 最終登録日時
                int diag_date = 0;

                foreach (DiagDPC d in diag_list)
                {
                    // 削除されていれば飛ばす
                    if (d.DeleteFlg) continue;

                    if (d.PtId.Equals(obj.Id))
                    {
                        // DPC主病名がチェックされている場合
                        if (d.MainFlg)
                        {
                            diag_flg = true;
                        }

                        // 最終登録日時
                        if (d.RegDate >= diag_date || d.UpDate >= diag_date)
                        {
                            diag_date = d.UpDate > d.RegDate ? d.UpDate : d.RegDate;
                        }
                    }
                }

                // DPC主病名がチェックされている
                if (diag_flg)
                {
                    r["DPC病名"] = "○ " + DateTime.Parse(DateTimeAgent.DateFormat(diag_date, DateTimeAgent.DateFormatKind.LONG)).ToString("M/dd").PadLeft(5, ' ');
                }

                table.Rows.Add(r);

                pt_list.Add(obj.Id);
            }


            List<DPCData> list2 = DPCData.GetList(pt_list);

            foreach (DataRow r in table.Rows)
            {
                foreach (DPCData data in list2)
                {
                    if (r["ID"].ToString().Equals(data.Id) && r["入院日8"].ToString().Equals(data.AdmDate))
                    {
                        if (data.Kind.Equals("11"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["様式1"] = "●";
                            }
                            else if (data.Status.Equals("2"))
                            {
                                r["様式1"] = "△";
                            }
                        }
                        else if (data.Kind.Equals("13"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["DR1"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("14"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["NS1"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("15"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["レセ1"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("16"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["DPC1"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("17"))
                        {
                            r["コメント1"] = data.Cont;
                        }
                        else if (data.Kind.Equals("21"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["様式2"] = "●";
                            }
                            else if (data.Status.Equals("2"))
                            {
                                r["様式2"] = "△";
                            }
                        }
                        else if (data.Kind.Equals("23"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["DR2"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("24"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["NS2"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("25"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["レセ2"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("26"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["DPC2"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("27"))
                        {
                            r["コメント2"] = data.Cont;
                        }
                        else if (data.Kind.Equals("31"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["様式3"] = "●";
                            }
                            else if (data.Status.Equals("2"))
                            {
                                r["様式3"] = "△";
                            }
                        }
                        else if (data.Kind.Equals("33"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["DR3"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("34"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["NS3"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("35"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["レセ3"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("36"))
                        {
                            if (data.Status.Equals("1"))
                            {
                                r["DPC3"] = "●";
                            }
                        }
                        else if (data.Kind.Equals("37"))
                        {
                            r["コメント3"] = data.Cont;
                        }
                        else if (data.Kind.Equals("8"))
                        {
                            r["面会状況"] = data.Cont;
                        }
                    }
                }
            }

            this.ListFormat3();

            // 元の選択患者・入院日・スクロール位置をセット
            if (pt_id.Length > 0 && in_date.Length > 0)
            {
                foreach (DataGridViewRow r in this.ListView3.Rows)
                {
                    if (r.Cells["ID"].Value.ToString().Equals(pt_id) &&
                        r.Cells["入院日8"].Value.ToString().Equals(in_date))
                    {
                        r.Selected = true;
                        break;
                    }
                }
            }

            if (y >= 0 && y < this.ListView3.RowCount)
            {
                this.ListView3.FirstDisplayedScrollingRowIndex = y;
            }
        }

        void ListFormat3()
        {
            DataView view = new DataView(dSet.Tables["入院予定"]);

            if (view.Count == 0)
            {
                return;
            }

            List<string> filters = new List<string>();

            if (this.WardBox1.Text.Contains(" "))
            {
                filters.Add("病棟コード = '" + this.WardBox1.Text.Split(' ')[0] + "'");
            }

            // キーワード
            if (this.KeywordBox1.Text.Length > 0)
            {
                string keyword = KeywordBox1.Text;

                filters.Add("(ID like '%" + keyword + "%' or 氏名 like '%" + keyword + "%' or カナ like '%" + keyword + "%' or 病室 like '%" + keyword + "%')");
            }

            if (this.DateBox31.DateInt > 0)
            {
                filters.Add("入院日8 = " + this.DateBox31.DateInt);
            }

            string filter = AppString.ConcatList(filters, " and ");

            view.RowFilter = filter;

            if (this.ListViewSort3.Length > 0)
            {
                view.Sort = this.ListViewSort3;
            }
            else
            {
                view.Sort = "入院予定日";
            }

            if (this.ListViewSortOrder3 == SortOrder.Descending)
            {
                view.Sort += " desc";
            }

            this.CountLabel3.Text = view.Count + " 名";

            try
            {
                ListView3.DataSource = view;

                ListView3.Columns["病棟コード"].Visible = false;

                ListView3.Columns["病棟"].HeaderText = "病棟";
                ListView3.Columns["病棟"].Width = 45;
                ListView3.Columns["病棟"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView3.Columns["病室"].HeaderText = "病室";
                ListView3.Columns["病室"].Width = 35;
                ListView3.Columns["病室"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView3.Columns["ID"].HeaderText = "ID";
                ListView3.Columns["ID"].Width = 55;
                ListView3.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                ListView3.Columns["カナ"].Width = 70;

                ListView3.Columns["氏名"].Width = 90;

                ListView3.Columns["性別"].HeaderText = "性別";
                ListView3.Columns["性別"].Width = 35;
                ListView3.Columns["性別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView3.Columns["年齢"].HeaderText = "年齢";
                ListView3.Columns["年齢"].Width = 35;
                ListView3.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView3.Columns["科コード"].Visible = false;

                ListView3.Columns["科"].HeaderText = "科";
                ListView3.Columns["科"].Width = 50;
                ListView3.Columns["科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                ListView3.Columns["医師コード"].Visible = false;

                ListView3.Columns["医師"].HeaderText = "医師";
                ListView3.Columns["医師"].Width = 70;
                ListView3.Columns["医師"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                ListView3.Columns["入院予定日"].HeaderText = "入院日";
                ListView3.Columns["入院予定日"].Width = 70;
                ListView3.Columns["入院予定日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView3.Columns["入院日8"].Visible = false;

                ListView3.Columns["入院予定時間"].HeaderText = "時間";
                ListView3.Columns["入院予定時間"].Width = 35;
                ListView3.Columns["入院予定時間"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView3.Columns["入院区分"].HeaderText = "区分";
                ListView3.Columns["入院区分"].Width = 35;
                ListView3.Columns["入院区分"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView3.Columns["退院予定日"].HeaderText = "退院日";
                ListView3.Columns["退院予定日"].Width = 70;
                ListView3.Columns["退院予定日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView3.Columns["退院予定時間"].HeaderText = "時間";
                ListView3.Columns["退院予定時間"].Width = 35;
                ListView3.Columns["退院予定時間"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView3.Columns["入院日数"].HeaderText = "日数";
                ListView3.Columns["入院日数"].Width = 25;
                ListView3.Columns["入院日数"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView3.Columns["保険パターン番号"].Visible = false;
                ListView3.Columns["保険種別コード"].Visible = false;
                ListView3.Columns["保険"].Visible = false;

                ListView3.Columns["DPC病名"].Width = 55;
                ListView3.Columns["DPC病名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView3.Columns["DPC病名"].ToolTipText = "○ DPC主病名あり";

                ListView3.Columns["面会状況"].HeaderText = "面会";
                ListView3.Columns["面会状況"].Width = 45;
                ListView3.Columns["面会状況"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView3.Columns["DR1"].HeaderText = "DR";
                ListView3.Columns["DR1"].Width = 35;
                ListView3.Columns["DR1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView3.Columns["NS1"].HeaderText = "NS";
                ListView3.Columns["NS1"].Width = 35;
                ListView3.Columns["NS1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView3.Columns["様式1"].HeaderText = "様1";
                ListView3.Columns["様式1"].Width = 35;
                ListView3.Columns["様式1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView3.Columns["レセ1"].HeaderText = "レセ";
                ListView3.Columns["レセ1"].Width = 35;
                ListView3.Columns["レセ1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView3.Columns["DPC1"].HeaderText = "DPC";
                ListView3.Columns["DPC1"].Width = 35;
                ListView3.Columns["DPC1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ListView3.Columns["コメント1"].HeaderText = "DPCコメント";
                ListView3.Columns["コメント1"].Width = 100;
                ListView3.Columns["コメント1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                ListView3.Columns["DR2"].HeaderText = "DR";
                ListView3.Columns["DR2"].Width = 35;
                ListView3.Columns["DR2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView3.Columns["DR2"].DefaultCellStyle.BackColor = Color.LightYellow;

                ListView3.Columns["NS2"].HeaderText = "NS";
                ListView3.Columns["NS2"].Width = 35;
                ListView3.Columns["NS2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView3.Columns["NS2"].DefaultCellStyle.BackColor = Color.LightYellow;

                ListView3.Columns["様式2"].HeaderText = "様1";
                ListView3.Columns["様式2"].Width = 35;
                ListView3.Columns["様式2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView3.Columns["様式2"].DefaultCellStyle.BackColor = Color.LightYellow;

                ListView3.Columns["レセ2"].HeaderText = "レセ";
                ListView3.Columns["レセ2"].Width = 35;
                ListView3.Columns["レセ2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView3.Columns["レセ2"].DefaultCellStyle.BackColor = Color.LightYellow;

                ListView3.Columns["DPC2"].HeaderText = "DPC";
                ListView3.Columns["DPC2"].Width = 35;
                ListView3.Columns["DPC2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView3.Columns["DPC2"].DefaultCellStyle.BackColor = Color.LightYellow;

                ListView3.Columns["コメント2"].HeaderText = "DPCコメント";
                ListView3.Columns["コメント2"].Width = 100;
                ListView3.Columns["コメント2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                ListView3.Columns["コメント2"].DefaultCellStyle.BackColor = Color.LightYellow;

                ListView3.Columns["DR3"].HeaderText = "DR";
                ListView3.Columns["DR3"].Width = 35;
                ListView3.Columns["DR3"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView3.Columns["DR3"].DefaultCellStyle.BackColor = Color.LightCyan;

                ListView3.Columns["NS3"].HeaderText = "NS";
                ListView3.Columns["NS3"].Width = 35;
                ListView3.Columns["NS3"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView3.Columns["NS3"].DefaultCellStyle.BackColor = Color.LightCyan;

                ListView3.Columns["様式3"].HeaderText = "様1";
                ListView3.Columns["様式3"].Width = 35;
                ListView3.Columns["様式3"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView3.Columns["様式3"].DefaultCellStyle.BackColor = Color.LightCyan;

                ListView3.Columns["レセ3"].HeaderText = "レセ";
                ListView3.Columns["レセ3"].Width = 35;
                ListView3.Columns["レセ3"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView3.Columns["レセ3"].DefaultCellStyle.BackColor = Color.LightCyan;

                ListView3.Columns["DPC3"].HeaderText = "DPC";
                ListView3.Columns["DPC3"].Width = 35;
                ListView3.Columns["DPC3"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ListView3.Columns["DPC3"].DefaultCellStyle.BackColor = Color.LightCyan;

                ListView3.Columns["コメント3"].HeaderText = "DPCコメント";
                ListView3.Columns["コメント3"].Width = 100;
                ListView3.Columns["コメント3"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                ListView3.Columns["コメント3"].DefaultCellStyle.BackColor = Color.LightCyan;

                ListView3.Columns["Obj"].Visible = false;

                if (this.UserMode1 == UserMode.Dpc)
                {
                    ListView3.Columns["DR1"].Visible = true;
                    ListView3.Columns["NS1"].Visible = true;
                    ListView3.Columns["様式1"].Visible = true;
                    ListView3.Columns["レセ1"].Visible = true;
                    ListView3.Columns["DPC1"].Visible = true;
                    ListView3.Columns["コメント1"].Visible = true;
                    ListView3.Columns["DR2"].Visible = true;
                    ListView3.Columns["NS2"].Visible = true;
                    ListView3.Columns["様式2"].Visible = true;
                    ListView3.Columns["レセ2"].Visible = true;
                    ListView3.Columns["DPC2"].Visible = true;
                    ListView3.Columns["コメント2"].Visible = true;
                    ListView3.Columns["DR3"].Visible = true;
                    ListView3.Columns["NS3"].Visible = true;
                    ListView3.Columns["様式3"].Visible = true;
                    ListView3.Columns["レセ3"].Visible = true;
                    ListView3.Columns["DPC3"].Visible = true;
                    ListView3.Columns["コメント3"].Visible = true;
                }
                else
                {
                    ListView3.Columns["DR1"].Visible = false;
                    ListView3.Columns["NS1"].Visible = false;
                    ListView3.Columns["様式1"].Visible = false;
                    ListView3.Columns["レセ1"].Visible = false;
                    ListView3.Columns["DPC1"].Visible = false;
                    ListView3.Columns["コメント1"].Visible = false;
                    ListView3.Columns["DR2"].Visible = false;
                    ListView3.Columns["NS2"].Visible = false;
                    ListView3.Columns["様式2"].Visible = false;
                    ListView3.Columns["レセ2"].Visible = false;
                    ListView3.Columns["DPC2"].Visible = false;
                    ListView3.Columns["コメント2"].Visible = false;
                    ListView3.Columns["DR3"].Visible = false;
                    ListView3.Columns["NS3"].Visible = false;
                    ListView3.Columns["様式3"].Visible = false;
                    ListView3.Columns["レセ3"].Visible = false;
                    ListView3.Columns["DPC3"].Visible = false;
                    ListView3.Columns["コメント3"].Visible = false;
                }

                foreach (DataGridViewRow r in this.ListView3.Rows)
                {
                    string code = r.Cells["病棟コード"].Value.ToString();

                    if (Dict.WardDict.ContainsKey(code))
                    {
                        r.Cells["病棟"].Style.BackColor = Dict.WardDict[code].BackColor;
                    }

                    r.Cells["ID"].ToolTipText = r.Cells["ID"].Value.ToString();
                    r.Cells["カナ"].ToolTipText = r.Cells["カナ"].Value.ToString();
                    r.Cells["氏名"].ToolTipText = r.Cells["氏名"].Value.ToString();
                    r.Cells["性別"].ToolTipText = r.Cells["性別"].Value.ToString();
                    r.Cells["年齢"].ToolTipText = r.Cells["年齢"].Value.ToString();
                }

                // 非表示の設定
                this.HideMode = !this.HideBox1.Checked;

                if (this.Font.Size > 9)
                {
                    foreach (DataGridViewColumn c in ListView3.Columns)
                    {
                        c.Width = (int)(c.Width * (float)(this.Font.Size / 9));
                    }
                }
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex);
            }
        }

        void ListFormat()
        {
            if (this.TabControl1.SelectedTab.Name.Equals("TabPage1"))
            {
                this.ListFormat1();
            }
            else if (this.TabControl1.SelectedTab.Name.Equals("TabPage2"))
            {
                this.ListFormat2();
            }
            else if (this.TabControl1.SelectedTab.Name.Equals("TabPage3"))
            {
                this.ListFormat3();
            }
        }


        private void WardBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.WardBox1.Text.StartsWith("3 "))
            {
                this.DeptCode = "31";
            }
            else if (this.WardBox1.Text.StartsWith("4 "))
            {
                this.DeptCode = "41";
            }
            else if (this.WardBox1.Text.StartsWith("5 "))
            {
                this.DeptCode = "33";
            }
            else
            {
                this.DeptCode = "";
            }
            this.ListFormat1();
            this.ListFormat2();
            this.ListFormat3();
        }

        private void ListView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
/*
            string pt_id = ListView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
            string dept_code = ListView1.Rows[e.RowIndex].Cells["科コード"].Value.ToString();
            string doctor_code = ListView1.Rows[e.RowIndex].Cells["医師コード"].Value.ToString();
*/
            PatIn pin = (PatIn)ListView1.Rows[e.RowIndex].Cells["Obj"].Value;
            FormControl.FormOrderKaikei_Show(pin, FormOrderKaikei.Mode.In, "", "", 1, pin.Dept, pin.Doctor);
        }

        private void ListView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
/*
            string pt_id = ListView2.Rows[e.RowIndex].Cells["ID"].Value.ToString();
            string dept_code = ListView2.Rows[e.RowIndex].Cells["科コード"].Value.ToString();
            string doctor_code = ListView2.Rows[e.RowIndex].Cells["医師コード"].Value.ToString();
*/
            PatIn pin = (PatIn)ListView2.Rows[e.RowIndex].Cells["Obj"].Value;
            FormControl.FormOrderKaikei_Show(pin, FormOrderKaikei.Mode.In, "", "", 1, pin.Dept, pin.Doctor);
        }

        private void ListView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            PatIn pin = (PatIn)ListView3.Rows[e.RowIndex].Cells["Obj"].Value;
            FormControl.FormOrderKaikei_Show(pin, FormOrderKaikei.Mode.In, "", "", 1, pin.Dept, pin.Doctor);
        }

        /// <summary>
        /// 患者データをクリアする
        /// </summary>
        void PtDataClear()
        {
            this.Pat.Id = "";

            this.DiagView1.ListClear();
            this.ProblemView1.ListClear();
            this.InHistoryGridView1.ListClear();

            this.MemoBox1.Clear();
            this.MemoDateTimeLabel1.Text = "";
            this.MemoStaffLabel1.Text = "";
        }

        /// <summary>
        /// 患者データを表示する
        /// </summary>
        void PtDataShow()
        {
            string pt_id = this.Pat.Id;

            if (this.DetailMode)
            {
                this.DiagView1.ListShow(pt_id);
                this.ProblemView1.ListShow(pt_id);
                this.InHistoryGridView1.ListShow(pt_id);

                Memo memo = Memo.Load(pt_id, "0");
                this.MemoBox1.Text = memo.Cont;
                this.MemoDateTimeLabel1.Text = memo.UpDateTime;
                this.MemoStaffLabel1.Text = memo.UpStaffName;
            }

            if (FormControl.IsFormOrderKaikeiOpen)
            {
                this.OrderKaikeiShow();
            }

            if (FormControl.IsFormDPCData1Open)
            {
                this.DPCDataShow();
            }

            this.PtDataFormat();
        }

        void PtDataFormat()
        {
            List<string> filters = new List<string>();

            if (this.DiagCheckBox1.Checked)
            {
                filters.Add("転帰区分 = ''");
            }

            if (this.DiagCheckBox2.Checked)
            {
                filters.Add("転帰区分 <> ''");
            }

            if (filters.Count == 0)
            {
                filters.Add("転帰区分 = 'A'");
            }

            DiagView1.ListFormat("開始日 desc, 連番 desc", AppString.ConcatList(filters, " or "));


            filters.Clear();

            if (this.ProblemSectionBox1.GetSection().Code > 0)
            {
                filters.Add("資格コード = " + this.ProblemSectionBox1.GetSection().Code);
            }

            // 入院のみ表示する
            if (!this.ProblemCheckBox1.Checked)
            {
                filters.Add("入外区分 = '入院'");
            }

            // 未解決のみ表示する
            if (!this.ProblemCheckBox2.Checked)
            {
                filters.Add("解決日 = ''");
            }

            ProblemView1.ListFormat("プロブレムＮＯ desc", AppString.ConcatList(filters, " and "));
        }

        private void ListView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow r = ListView1.Rows[e.RowIndex];

            // ダブルクリックした時は、AppStat.CurrentPat を変更するが
            // シングルクリックの時は、このアプリ内の Pat.Id のみ変更する。
            this.PatSet(PatBase.Load(r.Cells["ID"].Value.ToString()));
        }

        private void ListView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow r = ListView2.Rows[e.RowIndex];

            // ダブルクリックした時は、AppStat.CurrentPat を変更するが
            // シングルクリックの時は、このアプリ内の Pat.Id のみ変更する。
            this.PatSet(PatBase.Load(r.Cells["ID"].Value.ToString()));
        }

        private void ListView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow r = ListView3.Rows[e.RowIndex];

            // ダブルクリックした時は、AppStat.CurrentPat を変更するが
            // シングルクリックの時は、このアプリ内の Pat.Id のみ変更する。
            this.PatSet(PatBase.Load(r.Cells["ID"].Value.ToString()));
        }

        private void DiagButton1_Click(object sender, EventArgs e)
        {
            // 管理者でなければ終了
            if (!LoginUser.IsAdmin)
            {
                return;
            }

            if (this.Pat.Id.Length == 0)
            {
                return;
            }

            FormDiag f = new FormDiag();
            f.PatSet(this.Pat);
            f.ShowDialog(this);
        }

        private void DPCButton1_Click(object sender, EventArgs e)
        {
            FormDiagDPC f = new FormDiagDPC();
            f.PatSet(this.Pat);
            f.ShowDialog(this);
        }

        private void InHistoryButton1_Click(object sender, EventArgs e)
        {
            FormInCal2 f = new FormInCal2();
            f.PatSet(this.Pat);
            f.ShowDialog(this);
        }

        private void ProblemButton1_Click(object sender, EventArgs e)
        {
            // 管理者でなければ終了
            if (!LoginUser.IsAdmin)
            {
                return;
            }

            if (this.Pat.Id.Length == 0)
            {
                return;
            }

            FormProblem1 f = new FormProblem1(true, "2", this.DeptCode);
            f.PatSet(this.Pat);
            f.ShowDialog(this);
        }

        private void MemoButton1_Click(object sender, EventArgs e)
        {
            if (this.Pat.Id.Length == 0)
            {
                return;
            }

            FormMemo f = new FormMemo();
            f.PatSet(this.Pat);
            f.ShowDialog(this);
        }

        private void DiagCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.PtDataFormat();
        }

        private void DiagCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.PtDataFormat();
        }

        private void ProblemSectionBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PtDataFormat();
        }

        private void ProblemCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.PtDataFormat();
        }

        private void ProblemCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.PtDataFormat();
        }

        private void ByotoListMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            /*
            TabPage page = this.TabControl1.SelectedTab;

            if (page.Controls.ContainsKey("ListView3"))
            {
                DPCMenuItem.Enabled = false;
                SeeMenuItem.Enabled = false;
            }
            else
            {
                DPCMenuItem.Enabled = true;
                SeeMenuItem.Enabled = true;
            }
             */
        }

        private void KarteMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)this.ByotoListMenuStrip1.SourceControl;

            if (view.SelectedRows.Count > 0)
            {
                FormControl.FormPat_Show((PatIn)view.SelectedRows[0].Cells["Obj"].Value);
            }
        }

        void OrderKaikeiShow()
        {
            TabPage page = this.TabControl1.SelectedTab;
            DataGridView view = new DataGridView();

            if (page.Name.Equals("TabPage1"))
            {
                view = this.ListView1;
            }
            else if (page.Name.Equals("TabPage2"))
            {
                view = this.ListView2;
            }
            else if (page.Name.Equals("TabPage3"))
            {
                view = this.ListView3;
            }

            if (view.SelectedRows.Count > 0)
            {
                PatIn pin = (PatIn)view.SelectedRows[0].Cells["Obj"].Value;
                FormControl.FormOrderKaikei_Show(pin, FormOrderKaikei.Mode.In, "", "", 1, pin.Dept, pin.Doctor, pin.Ins);
            }
        }

        private void OrderKaikeiMenuItem_Click(object sender, EventArgs e)
        {
            this.OrderKaikeiShow();
        }

        private void InCalMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)this.ByotoListMenuStrip1.SourceControl;

            if (view.CurrentRow != null)
            {
                string pt_id = view.CurrentRow.Cells["ID"].Value.ToString();

                FormInCal2 f = new FormInCal2();
                f.PatSet(PatBase.Load(pt_id));
                f.Show();
            }
        }

        void DPCDataShow()
        {
            TabPage page = this.TabControl1.SelectedTab;
            DataGridView view = new DataGridView();

            if (page.Name.Equals("TabPage1"))
            {
                view = this.ListView1;
            }
            else if (page.Name.Equals("TabPage2"))
            {
                view = this.ListView2;
            }
            else if (page.Name.Equals("TabPage3"))
            {
                view = this.ListView3;
            }

            string pt_id = "";
            string in_date = "";

            if (view.SelectedRows.Count > 0)
            {
                pt_id = view.SelectedRows[0].Cells["ID"].Value.ToString();
                in_date = view.SelectedRows[0].Cells["入院日8"].Value.ToString();
            }

            FormControl.FormDPCData1_Show(PatBase.Load(pt_id), in_date);
        }

        private void DPCMenuItem_Click(object sender, EventArgs e)
        {
            this.DPCDataShow();
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ListShow();
        }

        private void KeywordBox1_TextChanged(object sender, EventArgs e)
        {
            this.ListFormat();
        }

        private void FormOrderKaikeiButton1_Click(object sender, EventArgs e)
        {
            this.OrderKaikeiShow();
        }

        private void KarteMessageMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)this.ByotoListMenuStrip1.SourceControl;

            if (view.CurrentRow != null)
            {
                PatIn obj = (PatIn)view.CurrentRow.Cells["Obj"].Value;

                // 主治医を取得する
                Doctor doctor = Doctor.Load(obj.Doctor);

                FormKarteMessage2 f = new FormKarteMessage2(obj.Id, new List<string> { doctor.StaffCode.ToString() });
                f.Show();
            }
        }

        private void SeeMenuItem0_Click(object sender, EventArgs e)
        {
            TabPage page = this.TabControl1.SelectedTab;

            if (page.Controls.ContainsKey("ListView1") && ListView1.CurrentRow != null)
            {
                DPCData data = new DPCData();
                data.Id = ListView1.CurrentRow.Cells["ID"].Value.ToString();
                data.AdmDate = ListView1.CurrentRow.Cells["入院日8"].Value.ToString();
                data.Kind = "8";
                data.Cont = "";

                data.Save();
                this.ListShow1();
            }
            else if (page.Controls.ContainsKey("ListView2") && ListView2.CurrentRow != null)
            {
                DPCData data = new DPCData();
                data.Id = ListView2.CurrentRow.Cells["ID"].Value.ToString();
                data.AdmDate = ListView2.CurrentRow.Cells["入院日8"].Value.ToString();
                data.Kind = "8";
                data.Cont = "";

                data.Save();
                this.ListShow2();
            }
            else if (page.Controls.ContainsKey("ListView3") && ListView3.CurrentRow != null)
            {
                DPCData data = new DPCData();
                data.Id = ListView3.CurrentRow.Cells["ID"].Value.ToString();
                data.AdmDate = ListView3.CurrentRow.Cells["入院日8"].Value.ToString();
                data.Kind = "8";
                data.Cont = "";

                data.Save();
                this.ListShow3();
            }
        }

        private void SeeMenuItem1_Click(object sender, EventArgs e)
        {
            TabPage page = this.TabControl1.SelectedTab;

            if (page.Controls.ContainsKey("ListView1") && ListView1.CurrentRow != null)
            {
                DPCData data = new DPCData();
                data.Id = ListView1.CurrentRow.Cells["ID"].Value.ToString();
                data.AdmDate = ListView1.CurrentRow.Cells["入院日8"].Value.ToString();
                data.Kind = "8";
                data.Cont = "非表示";

                data.Save();
                this.ListShow1();
            }
            else if (page.Controls.ContainsKey("ListView2") && ListView2.CurrentRow != null)
            {
                DPCData data = new DPCData();
                data.Id = ListView2.CurrentRow.Cells["ID"].Value.ToString();
                data.AdmDate = ListView2.CurrentRow.Cells["入院日8"].Value.ToString();
                data.Kind = "8";
                data.Cont = "非表示";

                data.Save();
                this.ListShow2();
            }
            else if (page.Controls.ContainsKey("ListView3") && ListView3.CurrentRow != null)
            {
                DPCData data = new DPCData();
                data.Id = ListView3.CurrentRow.Cells["ID"].Value.ToString();
                data.AdmDate = ListView3.CurrentRow.Cells["入院日8"].Value.ToString();
                data.Kind = "8";
                data.Cont = "非表示";

                data.Save();
                this.ListShow3();
            }
        }

        private void SeeMenuItem2_Click(object sender, EventArgs e)
        {
            TabPage page = this.TabControl1.SelectedTab;

            if (page.Controls.ContainsKey("ListView1") && ListView1.CurrentRow != null)
            {
                DPCData data = new DPCData();
                data.Id = ListView1.CurrentRow.Cells["ID"].Value.ToString();
                data.AdmDate = ListView1.CurrentRow.Cells["入院日8"].Value.ToString();
                data.Kind = "8";
                data.Cont = "面会謝絶";

                data.Save();
                this.ListShow1();
            }
            else if (page.Controls.ContainsKey("ListView2") && ListView2.CurrentRow != null)
            {
                DPCData data = new DPCData();
                data.Id = ListView2.CurrentRow.Cells["ID"].Value.ToString();
                data.AdmDate = ListView2.CurrentRow.Cells["入院日8"].Value.ToString();
                data.Kind = "8";
                data.Cont = "面会謝絶";

                data.Save();
                this.ListShow2();
            }
            else if (page.Controls.ContainsKey("ListView3") && ListView3.CurrentRow != null)
            {
                DPCData data = new DPCData();
                data.Id = ListView3.CurrentRow.Cells["ID"].Value.ToString();
                data.AdmDate = ListView3.CurrentRow.Cells["入院日8"].Value.ToString();
                data.Kind = "8";
                data.Cont = "面会謝絶";

                data.Save();
                this.ListShow3();
            }
        }

        private void DPCDataMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)this.ByotoListMenuStrip1.SourceControl;

            if (view.CurrentRow != null)
            {
                PatIn pin = (PatIn)view.CurrentRow.Cells["Obj"].Value;

                List<PatIn> in_list = new List<PatIn>();
                in_list.Add(pin);

                // 入棟データ
                PatInDPCWard ward = new PatInDPCWard();

                // 対象患者の病棟移動歴を取得する
                List<PatInDPCWard> ward_list = PatInDPCWard.GetList(in_list, true);

                foreach (PatInDPCWard obj in ward_list)
                {
                    if (obj.Ward.Equals(pin.Ward))
                    {
                        ward = obj;
                        break;
                    }
                }

                List<DPCHeader> header_list = DPCHeader.GetList(pin.Id, true);
                bool b = false;

                foreach (DPCHeader header in header_list)
                {
                    if (header.AdmDate.Equals(pin.InDate) && header.Ward.Equals(pin.Ward))
                    {
                        FormDPCData2 f = new FormDPCData2(header);
                        f.Show(this);

                        b = true;
                        break;
                    }
                }

                // 既存DPCデータが無く、入棟データがある場合
                if (!b && AppString.IsDate(ward.DoDate))
                {
                    DPCHeader header = new DPCHeader();
                    header.Id = ward.Id;
                    header.AdmDate = ward.InDate;
                    header.DisDate = ward.OutDate;
                    header.StartDate = ward.DoDate;
                    header.EndDate = ward.EndDate;
                    header.Ward = ward.Ward;

                    // 新規登録の場合は 0 とする
                    header.SEQ = 0;

                    FormDPCData2 f = new FormDPCData2(header);
                    f.Show(this);
                }
            }
        }

        private void DPCListMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)this.ByotoListMenuStrip1.SourceControl;

            if (view.CurrentRow != null)
            {
                FormDPCList2 f = new FormDPCList2();
                f.PatSet(this.Pat);
                f.Show(this);
            }
        }

        private void DPCDiagMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)this.ByotoListMenuStrip1.SourceControl;

            if (view.CurrentRow != null)
            {
                FormDiagDPC f = new FormDiagDPC();
                f.PatSet(this.Pat);
                f.Show();
            }
        }

        private void DischargeSummaryMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)this.ByotoListMenuStrip1.SourceControl;

            if (view.CurrentRow != null)
            {
                PatIn obj = (PatIn)view.CurrentRow.Cells["Obj"].Value;

                FormDischargeSummary f = new FormDischargeSummary(this.Pat.Id, obj.SEQ);
                f.Show();
            }
        }

        private void DetailBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.DetailMode = this.DetailBox1.Checked;
        }

        private void HideBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.HideMode = !this.HideBox1.Checked;
        }

        private void UserModeBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.UserModeBox1.Text.Equals("医師"))
            {
                this.UserMode1 = UserMode.Doctor;
            }
            else if (this.UserModeBox1.Text.Equals("看護部"))
            {
                this.UserMode1 = UserMode.Nurse;
            }
            else if (this.UserModeBox1.Text.Equals("病棟医事"))
            {
                this.UserMode1 = UserMode.Iji2;
            }
            else if (this.UserModeBox1.Text.Equals("DPC"))
            {
                this.UserMode1 = UserMode.Dpc;
            }
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            this.ListShow();
        }

        private void TickIntervalBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Timer1.Interval = int.Parse(this.TickIntervalBox1.Text) * 60 * 1000;
        }

        private void TickModeButton1_Click(object sender, EventArgs e)
        {
            this.TickMode = !this.TickMode;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            this.ListShow();
        }

        private void ListView1_Sorted(object sender, EventArgs e)
        {
            this.ListViewSort1 = this.ListView1.SortedColumn.Name;
            this.ListViewSortOrder1 = this.ListView1.SortOrder;

            this.ListFormat1();
        }

        private void ListView2_Sorted(object sender, EventArgs e)
        {
            this.ListViewSort2 = this.ListView2.SortedColumn.Name;
            this.ListViewSortOrder2 = this.ListView2.SortOrder;

            this.ListFormat2();
        }

        private void ListView3_Sorted(object sender, EventArgs e)
        {
            this.ListViewSort3 = this.ListView3.SortedColumn.Name;
            this.ListViewSortOrder3 = this.ListView3.SortOrder;

            this.ListFormat3();
        }

        private void CSVButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            DataGridView view = new DataGridView();
            string file_prefix = "入院一覧";

            if (button.Name.Equals("CSVButton1"))
            {
                view = this.ListView1;
                file_prefix = "入院一覧";
            }
            else if (button.Name.Equals("CSVButton2"))
            {
                view = this.ListView2;
                file_prefix = "退院済み";
            }
            else if (button.Name.Equals("CSVButton3"))
            {
                view = this.ListView3;
                file_prefix = "入院予定";
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
                        ss.Add(c.Name);
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
                            ss.Add(c.Value.ToString());
                        }
                    }

                    list.Add(ss);
                }

                //                MessageBox.Show(sfd.FileName);

                CsvWriter writer = new CsvWriter(sfd.FileName);
                writer.Write(list);

                writer.Dispose();

                if (MessageBox.Show("出力が完了しました。出力先のフォルダを開きますか？", "確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    FileInfo fi = new FileInfo(sfd.FileName);
                    Process.Start(fi.Directory.FullName);
                }
            }
        }
    }
}
