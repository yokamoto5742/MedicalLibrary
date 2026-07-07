using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using MedicalLibrary.Agent;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class FormPatList : StdForm1
    {
        string PatListViewSort = "通番";
        SortOrder PatListViewSortOrder = SortOrder.Ascending;

#if INNO
        /// <summary>
        /// 通常 or 救急 or 在宅
        /// </summary>
        public enum TimeMode : int
        {
            Normal = 1,
            Home = 2,
            QQ = 99
        }
#else
        /// <summary>
        /// 通常 or 救急
        /// </summary>
        public enum TimeMode : int
        {
            Normal = 1,
            QQ = 99
        }
#endif

        TimeMode time_mode = TimeMode.Normal;

        public TimeMode TimeMode1
        {
            set
            {
                this.time_mode = value;
            }
            get
            {
                return this.time_mode;
            }
        }

        /// <summary>
        /// ユーザーモード
        /// ダブルクリックした時の動作が決まる
        /// </summary>
        public enum UserMode : int
        {
            Doctor = 1,
            Nurse = 11,

            /// <summary>
            /// 中央医事
            /// </summary>
            Iji = 20,
            
            /// <summary>
            /// 各科医事
            /// </summary>
            Iji1 = 21,

            /// <summary>
            /// 病棟医事
            /// </summary>
            Iji2 = 22,

            Joho = 99
        }

        UserMode user_mode = UserMode.Iji;

        public UserMode UserMode1
        {
            set
            {
                this.user_mode = value;

                if (this.user_mode == UserMode.Iji)
                {
                    this.ShowKarteYet.Checked = false;
                    this.ShowKarteStart.Checked = false;
                    this.ShowKarteAllEnd.Checked = true;
                }
                else
                {
                    this.ShowKarteYet.Checked = true;
                    this.ShowKarteStart.Checked = true;
                    this.ShowKarteAllEnd.Checked = false;
                }
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

                    // 付箋
                    this.PostItButton1.Visible = true;
                    this.PostItView1.Visible = true;
                    this.PostItCheckBox1.Visible = true;
                    this.PostItCheckBox2.Visible = true;

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
                    this.PatListView.Height = this.Height - 360;
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

                    // 付箋
                    this.PostItButton1.Visible = false;
                    this.PostItView1.Visible = false;
                    this.PostItCheckBox1.Visible = false;
                    this.PostItCheckBox2.Visible = false;

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
                    this.PatListView.Height = this.Height - 98;
                }
            }
            get
            {
                return detail_mode;
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

        /// <summary>
        /// this.Pat の現在選択されている受診科コード。
        /// 付箋フォームを開くときに必要。
        /// </summary>
        string DeptCode = "";


        DataSet dSet = new DataSet();

        string pcName = System.Environment.GetEnvironmentVariable("COMPUTERNAME");

        public FormPatList()
        {
            InitializeComponent();
        }

        private void FormPatList_Load(object sender, EventArgs e)
        {
            try
            {
                this.DatePicker1.MaxDate = DateTime.Now.AddDays(3);
                this.DatePicker1.Value = DateTime.Now;

                DataTable table = dSet.Tables.Add("診察状況");
                table.Columns.Add("通番", typeof(int));
                table.Columns.Add("連番", typeof(int));
                table.Columns.Add("科番", typeof(int));
                table.Columns.Add("受付日時");
                table.Columns.Add("受付");
                table.Columns.Add("予約");
                table.Columns.Add("ID");
                table.Columns.Add("カナ");
                table.Columns.Add("氏名");
                table.Columns.Add("性別");
                table.Columns.Add("年齢");
                table.Columns.Add("属性コード");
                table.Columns.Add("属性");
                table.Columns.Add("種別");
                table.Columns.Add("区分");
                table.Columns.Add("保険パターン番号");
                table.Columns.Add("保険種別コード");
                table.Columns.Add("保険");
                table.Columns.Add("科コード");
                table.Columns.Add("科");
                table.Columns.Add("医師コード");
                table.Columns.Add("医師");
                table.Columns.Add("開始");
                table.Columns.Add("中断");
                table.Columns.Add("終了");
                table.Columns.Add("総合");
                table.Columns.Add("会計");
                table.Columns.Add("会計入力者コード");
                table.Columns.Add("会計入力者");
                table.Columns.Add("開始フラグ");

                table.Columns.Add("受付科", typeof(int));
                table.Columns.Add("終了科", typeof(int));

                table.Columns.Add("入院日");

                table.Columns.Add("★");
                table.Columns.Add("障害");
                table.Columns.Add("病名");
                table.Columns.Add("付箋");

                table.Columns.Add("無オーダー");
                table.Columns.Add("未施行");
                table.Columns.Add("初再");
                table.Columns.Add("指導");
                table.Columns.Add("在宅");
                table.Columns.Add("処方");
                table.Columns.Add("注射");
                table.Columns.Add("処置");
                table.Columns.Add("手術");
                table.Columns.Add("検査");
                table.Columns.Add("画像");
                table.Columns.Add("リハ");
                table.Columns.Add("自費");
                table.Columns.Add("他");
                table.Columns.Add("Obj", typeof(PatOut));

                // デフォルトで「中央医事」モードにする
                UserModeBox1.Items.Add("診察");
                UserModeBox1.Items.Add("中央医事");
                UserModeBox1.Items.Add("各科医事");

                this.UserMode1 = UserMode.Iji;
                this.UserModeBox1.Text = "中央医事";

                this.ShowReceYet.Checked = true;
                this.ShowReceEnd.Checked = false;

                this.DeptBox1.Init(true, false, true);

                // 中央医事の場合は全科表示。それ以外は各科表示。
                if (this.UserMode1 == UserMode.Iji)
                {
                    this.DetailBox1.Checked = false;
                    this.DetailMode = false;
                }
                else
                {
                    string dept = LibSettings.Current.PC.Dept;

                    if (dept.Length > 0 && !dept.Equals("0") && Dict.DeptDict.ContainsKey(dept))
                    {
                        this.DeptBox1.SetDept(dept);
                    }

                    this.DetailBox1.Checked = true;
                    this.DetailMode = true;
                }

                this.DoctorBox1.Init(true, true);

                if (LoginUser.DoctorId.Length > 0)
                {
                    this.DoctorBox1.SetDoctor(LoginUser.DoctorId);
                }

                this.ProblemSectionBox1.Init(true, false, true, false);

                // 更新間隔（秒）
                this.TickIntervalBox1.Items.Add(30);
                this.TickIntervalBox1.Items.Add(60);
                this.TickIntervalBox1.Items.Add(120);
                this.TickIntervalBox1.Items.Add(300);
                this.TickIntervalBox1.Text = "30";

                // 病名は「現病名」
                this.DiagCheckBox1.Checked = true;

                // 付箋は「当日分のみ」
                this.PostItCheckBox1.Checked = true;

                // プロブレムは外来・未解決のみ（チェックボックスは OFF）

                // デフォルトで「通常」モードにする。
                this.TimeModeBox1.Items.Add("通常");
                this.TimeModeBox1.Items.Add("救急");
#if INNO
                this.TimeModeBox1.Items.Add("在宅");
#endif
                this.TimeModeBox1.Text = "通常";


                // 診察開始・診察終了時刻のセット機能は非公開
                this.KarteStartTimeMenuItem.Visible = false;
                this.KarteEndTimeMenuItem.Visible = false;

//                this.ListShow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.Timer1.Interval = int.Parse(this.TickIntervalBox1.Text) * 1000;
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

            this.ListFormat1();
        }

        /// <summary>
        /// 診察状況リスト表示
        /// </summary>
        void ListShow()
        {
            // 元の通番・連番・スクロール位置を取得
            string seq1 = "";
            string seq2 = "";
            int y = this.PatListView.FirstDisplayedScrollingRowIndex;

            if (this.PatListView.SelectedRows.Count > 0)
            {
                seq1 = this.PatListView.SelectedRows[0].Cells["通番"].Value.ToString();
                seq2 = this.PatListView.SelectedRows[0].Cells["連番"].Value.ToString();
            }

            DataTable table = dSet.Tables["診察状況"];
            table.Clear();

            string dept = "";

            if (DeptBox1.Text.Length > 0 && DeptBox1.Text.Contains(" "))
            {
                if (!DeptBox1.Text.Split(' ')[0].Equals("0"))
                {
                    dept = DeptBox1.Text.Split(' ')[0];
                }
            }

            string doctor = "";

            if (DoctorBox1.Text.Length > 0 && DoctorBox1.Text.Contains(" "))
            {
                if (!DoctorBox1.Text.Split(' ')[0].Equals("0"))
                {
                    doctor = DoctorBox1.Text.Split(' ')[0];
                }
            }

            string come_date = this.DatePicker1.Value.ToString("yyyyMMdd");

            List<PatOut> tmpList = PatOut.GetList(come_date, dept, doctor);
            List<PatOutKoui> kouiList = PatOutKoui.GetList(come_date, dept);
            List<PatOutKarteStatus> karteStatusList = PatOutKarteStatus.GetList(come_date);

            // 予約データ
            List<RsvMaster> rsvMasterList = new List<RsvMaster>();
            List<RsvData> rsvDataList = new List<RsvData>();

            // 付箋データ
            List<PostIt> postItList = new List<PostIt>();

            // 未転帰病名データ
            List<Diag> diagList = new List<Diag>();

            // 患者固定情報データ
            List<StdClass> baseInfoList = new List<StdClass>();

            // 会計到着データ
            List<BillPay> billPayList = new List<BillPay>();

            if (UserMode1 == UserMode.Iji)
            {
                // 付箋データ
                postItList = PostIt.GetListByDate(come_date, "1");

                // 患者固定情報データ
//                baseInfoList = BaseInfoFixed.GetListByOutDate(come_date);

                // 会計到着データ
                billPayList = BillPay.GetLiveListByDate(come_date, "");

                // 時間降順に並べ替える
                billPayList.Sort((b1, b2) =>
                    {
                        return b2.ArTime.CompareTo(b1.ArTime);
                    });
            }
            else
            {
                // 予約データ
                rsvMasterList = RsvMaster.GetListByDate2(come_date);
                rsvDataList = RsvData.GetListByDate(come_date);

                // 付箋データ
                postItList = PostIt.GetListByDate(come_date, "1");

                // 未転帰病名データ
                diagList = Diag.GetYetListByOutDate(come_date, dept);

                // 患者固定情報データ
                baseInfoList = BaseInfoFixed.GetListByOutDate(come_date);
            }

            // 入院患者データ
            // 会計時、入院中の人はリストに出ないようにするため
            List<PatIn> patInList = PatIn.GetList();


            foreach (PatOut obj in tmpList)
            {
                DataRow r = table.NewRow();

                r["通番"] = obj.Seq1;
                r["連番"] = obj.Seq2;
                r["科番"] = obj.Seq3;
                r["受付日時"] = obj.DateTime1;

                r["受付"] = obj.TimeString1;

                foreach (RsvData rd in rsvDataList)
                {
                    // 患者コードが異なれば飛ばす
                    if (!rd.Pat.Id.Equals(obj.Id))
                    {
                        continue;
                    }

                    foreach (RsvMaster rm in rsvMasterList)
                    {
                        // 予約種別または詳細が異なれば飛ばす
                        if (!rd.Code1.Equals(rm.Code1) || !rd.Code2.Equals(rm.Code2))
                        {
                            continue;
                        }

                        // 診察予約でなければ飛ばす
                        if (rm.Kind1 != RsvKind.Doctor)
                        {
                            continue;
                        }

                        // 科が異なれば飛ばす
                        if (!rm.DeptCode.Equals(obj.Dept))
                        {
                            continue;
                        }

                        r["予約"] = DateTimeAgent.TimeFormat(rd.Time1);
                    }
                }

                r["ID"] = obj.Id;
                r["カナ"] = obj.Kana;
                r["氏名"] = obj.Name;

                if (UserMode1 == UserMode.Iji || UserMode1 == UserMode.Iji1 || UserMode1 == UserMode.Iji2)
                {
                    r["氏名"] = obj.Note1.PatListMark + r["氏名"].ToString();
                }

                if (obj.Sex.Equals("1"))
                {
                    r["性別"] = "男";
                }
                else if (obj.Sex.Equals("2"))
                {
                    r["性別"] = "女";
                }

                r["年齢"] = obj.Age;
                r["属性コード"] = obj.Note1.Code;
                r["属性"] = obj.Note1.Short;

                r["種別"] = obj.KindName;
                r["区分"] = obj.Mode;

                r["保険パターン番号"] = obj.Ins;
                r["保険種別コード"] = obj.InsKind;
                r["保険"] = obj.InsKindName;

                r["科コード"] = obj.Dept;
                r["科"] = obj.DeptName;
                r["医師コード"] = obj.Doctor;
                r["医師"] = obj.DoctorName;

                r["開始"] = obj.TimeString2;
                r["中断"] = obj.TimeString3;
                r["終了"] = obj.TimeString4;

                foreach (BillPay p in billPayList)
                {
                    // 患者コードが異なれば飛ばす
                    if (!p.PtId.Equals(obj.Id))
                    {
                        continue;
                    }

                    r["総合"] = DateTimeAgent.TimeFormat6(p.ArTime, 4, true);
                    break;
                }

                r["会計"] = obj.TimeString5;

                if (obj.Kaikei.Length > 0 && !obj.Kaikei.Equals("0"))
                {
                    r["会計入力者コード"] = obj.Kaikei;
                    r["会計入力者"] = obj.KaikeiName;
                }

                if (obj.Time2.Length > 0 && !obj.Time2.Equals("0"))
                {
                    r["開始フラグ"] = "0";
                }
                else
                {
                    r["開始フラグ"] = "1";
                }

                // 受診科数と終了科数
                foreach (PatOutKarteStatus k in karteStatusList)
                {
                    // 患者コードが異なれば飛ばす
                    if (!k.Id.Equals(obj.Id))
                    {
                        continue;
                    }

                    // 受付ＮＯが異なれば飛ばす
                    if (!k.Seq1.Equals(obj.Seq1))
                    {
                        continue;
                    }

                    r["受付科"] = k.Amount;
                    r["終了科"] = k.End;

                    // 1件あれば終了
                    break;
                }

                // 入院日
                foreach (PatIn p in patInList)
                {
                    // 患者コードが異なれば飛ばす
                    if (!p.Id.Equals(obj.Id))
                    {
                        continue;
                    }

//                    r["入院日"] = p.InDateStringShort;
                    r["入院日"] = DateTimeAgent.DateFormat(p.InDate, DateTimeAgent.DateFormatKind.MD);

                    break;
                }

                // 患者固定情報
                foreach (StdClass tmp in baseInfoList)
                {
                    // 患者コードが異なれば飛ばす
                    if (!tmp.DataDict["患者コード"].ToString().Equals(obj.Id))
                    {
                        continue;
                    }

                    if (tmp.DataDict["トラフラグ"].ToString().Equals("1"))
                    {
                        r["★"] = "★";
                    }

                    if (tmp.DataDict["障害１フラグ"].ToString().Equals("1"))
                    {
                        r["障害"] += "視力 ";
                    }

                    if (tmp.DataDict["障害２フラグ"].ToString().Equals("1"))
                    {
                        r["障害"] += "聴力 ";
                    }

                    if (tmp.DataDict["障害３フラグ"].ToString().Equals("1"))
                    {
                        r["障害"] += "言語 ";
                    }

                    if (tmp.DataDict["障害４フラグ"].ToString().Equals("1"))
                    {
                        r["障害"] += "運動 ";
                    }

                    // 1件あれば終了
                    break;
                }

                // 未転帰病名
                foreach (Diag k in diagList)
                {
                    // 削除されていれば飛ばす
                    if (k.DeleteFlg)
                    {
                        continue;
                    }

                    // 未確定のものは飛ばす
                    if (!k.FixFlg)
                    {
                        continue;
                    }

                    // 患者コードが異なれば飛ばす
                    if (!k.PtId.Equals(obj.Id))
                    {
                        continue;
                    }

                    // 入院ならば飛ばす
                    if (k.InOut.Equals("2"))
                    {
                        continue;
                    }

                    // 診療科が異なれば飛ばす
                    if (!k.Dept.Equals(obj.Dept))
                    {
                        continue;
                    }

                    r["病名"] = "○";

                    // 1件でもあれば終了
                    break;
                }


                // 付箋
                foreach (PostIt k in postItList)
                {
                    // 削除されていれば飛ばす
                    if (k.DeleteFlg)
                    {
                        continue;
                    }

                    // 患者コードが異なれば飛ばす
                    if (!k.PtId.Equals(obj.Id))
                    {
                        continue;
                    }

                    // 科が異なれば飛ばす
                    if (!k.DeptCode.Equals(obj.Dept))
                    {
                        continue;
                    }

                    // 入院ならば飛ばす
                    if (k.InOut.Equals("2"))
                    {
                        continue;
                    }

                    // 中央医事モードの場合は、先頭に「中）」と入っているものだけを抽出
                    // 2018/11/21 深田課長の要望
                    if (UserMode1 == UserMode.Iji)
                    {
                        if (!k.Cont1.StartsWith("中）") && !k.Cont1.StartsWith("中)"))
                        {
                            continue;
                        }
                    }

                    if (r["付箋"].ToString().Length > 0)
                    {
                        r["付箋"] += " ";
                    }

                    r["付箋"] += k.Cont1;
                }

                // オーダーが存在するかどうか
                bool koui_exist = false;

                // 全オーダーが施行済みかどうか
                bool koui_end = true;

                // オーダー
                foreach (PatOutKoui k in kouiList)
                {
                    // 患者コードが異なれば飛ばす
                    if (!k.Id.Equals(obj.Id))
                    {
                        continue;
                    }

                    // 診療科が異なれば飛ばす
                    if (!k.Dept.Equals(obj.Dept))
                    {
                        continue;
                    }

                    // オーダーは存在する
                    koui_exist = true;

                    if (k.Koui.Equals(0))
                    {
                        r["自費"] = "自";
                    }
                    else if (k.Koui.Equals(11))
                    {
                        r["初再"] += "初";
                    }
                    else if (k.Koui.Equals(12))
                    {
                        r["初再"] += "再";
                    }
                    else if (k.Koui.Equals(13))
                    {
                        // 外来栄養指導は施行入力を要する
//                        r["指導"] = "指";

                        if (k.Sekou.Equals(1))
                        {
                            r["指導"] = "◆";
                        }
                        else
                        {
                            r["指導"] = "◇";
                            koui_end = false;
                        }
                    }
                    else if (k.Koui.Equals(14))
                    {
                        r["在宅"] = "在";
                    }
                    else if (k.Koui >= 20 && k.Koui <= 29)
                    {
                        if (k.PharmacyInOut.Equals("0"))
                        {
                            if (!r["処方"].ToString().Contains("内"))
                            {
                                r["処方"] += "内";
                            }
                        }
                        else if (k.PharmacyInOut.Equals("1"))
                        {
                            if (!r["処方"].ToString().Contains("外"))
                            {
                                r["処方"] += "外";
                            }
                        }
                    }
                    else if (k.Koui >= 30 && k.Koui <= 39)
                    {
                        r["注射"] = "注";
                    }
                    else if (k.Koui >= 40 && k.Koui <= 49)
                    {
                        r["処置"] = "処";
                    }
                    else if (k.Koui >= 50 && k.Koui <= 59)
                    {
                        r["手術"] = "手";
                    }
                    else if (k.Koui >= 60 && k.Koui <= 69)
                    {
                        if (k.Sekou.Equals(1))
                        {
                            r["検査"] = "●";
                        }
                        else
                        {
                            r["検査"] = "○";
                            koui_end = false;
                        }
                    }
                    else if (k.Koui >= 70 && k.Koui <= 79)
                    {
                        if (k.Sekou.Equals(1))
                        {
                            r["画像"] = "■";
                        }
                        else
                        {
                            r["画像"] = "□";
                            koui_end = false;
                        }
                    }
                    else if (k.Koui >= 80 && k.Koui <= 89)
                    {
                        r["リハ"] = "リ";
                    }
                    else
                    {
                        r["他"] = "他";
                    }
                }

                if (!koui_exist)
                {
                    r["無オーダー"] = "■";
                }

                if (!koui_end)
                {
                    r["未施行"] = "●";
                }

                r["Obj"] = obj;

                table.Rows.Add(r);
            }

            foreach (DataRow r in table.Rows)
            {
                // 同一患者で、ある科に■（無オーダー）がある場合、他の科が有オーダーであれば空白にする。
                // →　高見さんの希望により、この処理は外す 2015/09/26
                /*
                if (r["無オーダー"].ToString().Equals("■"))
                {
                    foreach (DataRow r2 in table.Rows)
                    {
                        if (r2["ID"].ToString().Equals(r["ID"].ToString()) && !r2["無オーダー"].ToString().Equals("■"))
                        {
                            r["無オーダー"] = "";
                            break;
                        }
                    }
                }
                */

                // 同一患者で、ある科に●（未施行オーダー）があれば、他の科も○にする。
                if (r["未施行"].ToString().Equals("●"))
                {
                    foreach (DataRow r2 in table.Rows)
                    {
                        if (r2["ID"].ToString().Equals(r["ID"].ToString()) && r2["未施行"].ToString().Length == 0)
                        {
                            r2["未施行"] = "○";
                        }
                    }
                }
            }

            this.ListFormat1();

            // 患者の病名・付箋欄をクリア
//            this.PtDataClear();


            if (seq1.Length > 0 && seq2.Length > 0)
            {
                foreach (DataGridViewRow r in this.PatListView.Rows)
                {
                    if (r.Cells["通番"].Value.ToString().Equals(seq1) &&
                        r.Cells["連番"].Value.ToString().Equals(seq2))
                    {
                        r.Selected = true;
                        break;
                    }
                }
            }

            if (y >= 0 && y < this.PatListView.RowCount)
            {
                this.PatListView.FirstDisplayedScrollingRowIndex = y;
            }
        }

        /// <summary>
        /// 患者一覧のフィルタとソート
        /// </summary>
        private void ListFormat1()
        {
            if (!dSet.Tables.Contains("診察状況")) return;

            DataView view = new DataView(dSet.Tables["診察状況"]);

            List<string> filters = new List<string>();

#if INNO
            // 通常 or 救急 or 在宅 の表示条件
            if (this.TimeMode1 == TimeMode.Normal)
            {
                filters.Add("(区分 = 0)");
            }
            else if (this.TimeMode1 == TimeMode.QQ)
            {
                filters.Add("(区分 = 1)");
            }
            else if (this.TimeMode1 == TimeMode.Home)
            {
                filters.Add("(区分 = 2)");
            }
#else
            // 通常 or 救急の表示条件
            if (this.TimeMode1 == TimeMode.Normal)
            {
                filters.Add("(区分 = 1)");
            }
            else if (this.TimeMode1 == TimeMode.QQ)
            {
                filters.Add("(区分 = 2)");
            }
#endif

            // 入院の表示条件
            if (!ShowInBox1.Checked)
            {
                filters.Add("(入院日 is null)");
            }

            // 診察状況の表示条件
            List<string> filters_karte = new List<string>();

            if (this.ShowKarteAllEnd.Checked)
            {
                // 全科診察終了のみの場合（医事課向け）
                filters_karte.Add("(受付科 = 終了科) or (総合 is not null and 総合 <> '')");
            }
            else
            {
                // 全科診察終了以外の場合

                // 診察未開始
                if (ShowKarteYet.Checked)
                {
                    filters_karte.Add("(開始 is null or 開始 = '')");
                }

                // 診察開始 or 中断
                if (ShowKarteStart.Checked)
                {
                    filters_karte.Add("(開始 is not null and 開始 <> '' and (終了 is null or 終了 = ''))");
                }

                // 診察終了
                if (ShowKarteEnd.Checked)
                {
                    filters_karte.Add("(終了 is not null and 終了 <> '')");
                }
            }

            // 診察状況の条件の有無をチェック
            if (filters_karte.Count > 0)
            {
                filters.Add("(" + AppString.ConcatList(filters_karte, " or ") + ")");
            }
            else
            {
                // 診察の条件が指定されていなければ表示しない
                filters.Add("(開始 = '9999')");
            }


            // 会計入力状況の表示条件
            List<string> filters_rece = new List<string>();

            // 会計入力未開始
            if (ShowReceYet.Checked)
            {
                filters_rece.Add("(会計 is null or 会計 = '')");
            }

            // 会計入力済み
            if (ShowReceEnd.Checked)
            {
                filters_rece.Add("(会計 is not null and 会計 <> '')");
            }

            // 会計入力状況の条件の有無をチェック
            if (filters_rece.Count > 0)
            {
                filters.Add("(" + AppString.ConcatList(filters_rece, " or ") + ")");
            }
            else
            {
                // 会計の条件が指定されていなければ表示しない
                filters.Add("(会計 = '9999')");
            }


            // キーワード
            if (KeywordBox1.Text.Length > 0)
            {
                string keyword = KeywordBox1.Text;

                filters.Add("(ID like '%" + keyword + "%' or 氏名 like '%" + keyword + "%' or カナ like '%" + keyword + "%' or 付箋 like '%" + keyword + "%')");
            }

            string filter = AppString.ConcatList(filters, " and ");

            view.RowFilter = filter;

            if (this.PatListViewSort.Length > 0)
            {
                view.Sort = this.PatListViewSort;
            }
            else
            {
                view.Sort = "通番, 連番";
            }

            if (this.PatListViewSortOrder == SortOrder.Descending)
            {
                view.Sort += " desc";
            }
/*
            // DataGridView コントロール上でソート順が指定されていればそれを保持する
            int tmpColumn = 0;
            string tmpColumnName = "通番";

            if (this.UserMode1 == UserMode.Iji)
            {
//                tmpColumnName = "終了";
                tmpColumnName = "通番";
            }

            // ソートの方向
            ListSortDirection tmpDirection = ListSortDirection.Ascending;

            if (PatListView.SortOrder == SortOrder.Ascending)
            {
                tmpColumn = PatListView.SortedColumn.Index;
                tmpColumnName = PatListView.SortedColumn.Name;
            }
            else if (PatListView.SortOrder == SortOrder.Descending)
            {
                tmpColumn = PatListView.SortedColumn.Index;
                tmpColumnName = PatListView.SortedColumn.Name;
                tmpDirection = ListSortDirection.Descending;
            }
 */

            PatListView.DataSource = view;

            if (this.UserMode1 == UserMode.Iji)
            {
                PatListView.Columns["通番"].HeaderText = "通番";
                PatListView.Columns["通番"].Width = 30;
                PatListView.Columns["通番"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["連番"].Visible = false;

                PatListView.Columns["科番"].HeaderText = "科番";
                PatListView.Columns["科番"].Width = 30;
                PatListView.Columns["科番"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PatListView.Columns["科番"].Visible = false;

                PatListView.Columns["受付日時"].Visible = false;

                PatListView.Columns["受付"].HeaderText = "受付";
                PatListView.Columns["受付"].Width = 38;
                PatListView.Columns["受付"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["予約"].HeaderText = "予約";
                PatListView.Columns["予約"].Width = 38;
                PatListView.Columns["予約"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PatListView.Columns["予約"].Visible = false;

                PatListView.Columns["ID"].HeaderText = "ID";
                PatListView.Columns["ID"].Width = 50;
                PatListView.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                PatListView.Columns["カナ"].Width = 60;

                PatListView.Columns["氏名"].Width = 78;
                PatListView.Columns["氏名"].ToolTipText = "◎分割　☆口座引落　◇透析　○地域包括";

                PatListView.Columns["性別"].HeaderText = "性別";
                PatListView.Columns["性別"].Width = 28;
                PatListView.Columns["性別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PatListView.Columns["性別"].Visible = false;

                PatListView.Columns["年齢"].HeaderText = "年齢";
                PatListView.Columns["年齢"].Width = 28;
                PatListView.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["属性コード"].Visible = false;
                PatListView.Columns["属性"].Visible = false;
                PatListView.Columns["区分"].Visible = false;

                PatListView.Columns["種別"].HeaderText = "種別";
                PatListView.Columns["種別"].Width = 38;
                PatListView.Columns["種別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PatListView.Columns["種別"].Visible = false;

                PatListView.Columns["保険パターン番号"].Visible = false;
                PatListView.Columns["保険種別コード"].Visible = false;

                PatListView.Columns["保険"].HeaderText = "保険";
                PatListView.Columns["保険"].Width = 33;
                PatListView.Columns["保険"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PatListView.Columns["保険"].Visible = true;

                PatListView.Columns["科コード"].Visible = false;

                PatListView.Columns["科"].HeaderText = "科";
                PatListView.Columns["科"].Width = 40;
                PatListView.Columns["科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                PatListView.Columns["医師コード"].Visible = false;

                PatListView.Columns["医師"].HeaderText = "医師";
                PatListView.Columns["医師"].Width = 58;
                PatListView.Columns["医師"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                PatListView.Columns["開始"].HeaderText = "開始";
                PatListView.Columns["開始"].Width = 38;
                PatListView.Columns["開始"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["中断"].HeaderText = "中断";
                PatListView.Columns["中断"].Width = 38;
                PatListView.Columns["中断"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["終了"].HeaderText = "終了";
                PatListView.Columns["終了"].Width = 38;
                PatListView.Columns["終了"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["総合"].HeaderText = "総合";
                PatListView.Columns["総合"].Width = 38;
                PatListView.Columns["総合"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["会計"].HeaderText = "会計";
                PatListView.Columns["会計"].Width = 38;
                PatListView.Columns["会計"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["会計入力者コード"].Visible = false;

                PatListView.Columns["会計入力者"].HeaderText = "会計入力";
                PatListView.Columns["会計入力者"].Width = 58;
                PatListView.Columns["会計入力者"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                PatListView.Columns["開始フラグ"].Visible = false;

                PatListView.Columns["受付科"].Width = 20;
                PatListView.Columns["受付科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["終了科"].Width = 20;
                PatListView.Columns["終了科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["入院日"].HeaderText = "入院日";
                PatListView.Columns["入院日"].Width = 38;
                PatListView.Columns["入院日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["★"].HeaderText = "★";
                PatListView.Columns["★"].Width = 28;
                PatListView.Columns["★"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PatListView.Columns["★"].Visible = false;

                PatListView.Columns["障害"].HeaderText = "障害";
                PatListView.Columns["障害"].Width = 50;
                PatListView.Columns["障害"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                PatListView.Columns["障害"].Visible = false;

                PatListView.Columns["病名"].HeaderText = "病名";
                PatListView.Columns["病名"].Width = 35;
                PatListView.Columns["病名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PatListView.Columns["病名"].Visible = false;

                // 2018/11/21 深田課長の要望で表示することにした
                PatListView.Columns["付箋"].HeaderText = "付箋";
                PatListView.Columns["付箋"].Width = 80;
                PatListView.Columns["付箋"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                PatListView.Columns["付箋"].Visible = true;

                PatListView.Columns["無オーダー"].Width = 28;
                PatListView.Columns["無オーダー"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PatListView.Columns["無オーダー"].DefaultCellStyle.ForeColor = Color.Blue;
                PatListView.Columns["無オーダー"].ToolTipText = "■当科オーダー無し";

                PatListView.Columns["未施行"].Width = 28;
                PatListView.Columns["未施行"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PatListView.Columns["未施行"].DefaultCellStyle.ForeColor = Color.Red;
                PatListView.Columns["未施行"].ToolTipText = "●当科未施行あり" + Environment.NewLine + "○他科未施行あり";

                PatListView.Columns["自費"].Width = 28;
                PatListView.Columns["自費"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["初再"].Width = 28;
                PatListView.Columns["初再"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PatListView.Columns["初再"].Visible = false;

                PatListView.Columns["指導"].Width = 28;
                PatListView.Columns["指導"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["在宅"].Width = 28;
                PatListView.Columns["在宅"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["処方"].Width = 28;
                PatListView.Columns["処方"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["注射"].Width = 28;
                PatListView.Columns["注射"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["処置"].Width = 28;
                PatListView.Columns["処置"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["手術"].Width = 28;
                PatListView.Columns["手術"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["検査"].Width = 28;
                PatListView.Columns["検査"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PatListView.Columns["検査"].ToolTipText = "○未施行" + Environment.NewLine + "●施行済";

                PatListView.Columns["画像"].Width = 28;
                PatListView.Columns["画像"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PatListView.Columns["画像"].ToolTipText = "□未施行" + Environment.NewLine + "■施行済";

                PatListView.Columns["リハ"].Width = 28;
                PatListView.Columns["リハ"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["他"].Width = 28;
                PatListView.Columns["他"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            else
            {
                PatListView.Columns["通番"].HeaderText = "通番";
                PatListView.Columns["通番"].Width = 30;
                PatListView.Columns["通番"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["連番"].Visible = false;

                PatListView.Columns["科番"].HeaderText = "科番";
                PatListView.Columns["科番"].Width = 30;
                PatListView.Columns["科番"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PatListView.Columns["科番"].Visible = false;

                PatListView.Columns["受付日時"].Visible = false;

                PatListView.Columns["受付"].HeaderText = "受付";
                PatListView.Columns["受付"].Width = 38;
                PatListView.Columns["受付"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["予約"].HeaderText = "予約";
                PatListView.Columns["予約"].Width = 38;
                PatListView.Columns["予約"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PatListView.Columns["予約"].Visible = true;

                PatListView.Columns["ID"].HeaderText = "ID";
                PatListView.Columns["ID"].Width = 50;
                PatListView.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                PatListView.Columns["カナ"].Width = 60;

                PatListView.Columns["氏名"].Width = 78;
                PatListView.Columns["氏名"].ToolTipText = "◎分割　☆口座引落　◇透析　○地域包括";

                PatListView.Columns["性別"].HeaderText = "性別";
                PatListView.Columns["性別"].Width = 28;
                PatListView.Columns["性別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PatListView.Columns["性別"].Visible = false;

                PatListView.Columns["年齢"].HeaderText = "年齢";
                PatListView.Columns["年齢"].Width = 28;
                PatListView.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["属性コード"].Visible = false;
                PatListView.Columns["属性"].Visible = false;
                PatListView.Columns["区分"].Visible = false;

                PatListView.Columns["種別"].HeaderText = "種別";
                PatListView.Columns["種別"].Width = 38;
                PatListView.Columns["種別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PatListView.Columns["種別"].Visible = true;

                PatListView.Columns["保険パターン番号"].Visible = false;
                PatListView.Columns["保険種別コード"].Visible = false;

                PatListView.Columns["保険"].HeaderText = "保険";
                PatListView.Columns["保険"].Width = 33;
                PatListView.Columns["保険"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PatListView.Columns["保険"].Visible = true;

                PatListView.Columns["科コード"].Visible = false;

                PatListView.Columns["科"].HeaderText = "科";
                PatListView.Columns["科"].Width = 40;
                PatListView.Columns["科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                PatListView.Columns["医師コード"].Visible = false;

                PatListView.Columns["医師"].HeaderText = "医師";
                PatListView.Columns["医師"].Width = 58;
                PatListView.Columns["医師"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                PatListView.Columns["開始"].HeaderText = "開始";
                PatListView.Columns["開始"].Width = 38;
                PatListView.Columns["開始"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["中断"].HeaderText = "中断";
                PatListView.Columns["中断"].Width = 38;
                PatListView.Columns["中断"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["終了"].HeaderText = "終了";
                PatListView.Columns["終了"].Width = 38;
                PatListView.Columns["終了"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["総合"].HeaderText = "総合";
                PatListView.Columns["総合"].Width = 38;
                PatListView.Columns["総合"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["会計"].HeaderText = "会計";
                PatListView.Columns["会計"].Width = 38;
                PatListView.Columns["会計"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["会計入力者コード"].Visible = false;

                PatListView.Columns["会計入力者"].HeaderText = "会計入力";
                PatListView.Columns["会計入力者"].Width = 58;
                PatListView.Columns["会計入力者"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                PatListView.Columns["開始フラグ"].Visible = false;

                PatListView.Columns["受付科"].Width = 20;
                PatListView.Columns["受付科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["終了科"].Width = 20;
                PatListView.Columns["終了科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["入院日"].HeaderText = "入院日";
                PatListView.Columns["入院日"].Width = 38;
                PatListView.Columns["入院日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["★"].HeaderText = "★";
                PatListView.Columns["★"].Width = 30;
                PatListView.Columns["★"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PatListView.Columns["★"].Visible = true;

                PatListView.Columns["障害"].HeaderText = "障害";
                PatListView.Columns["障害"].Width = 50;
                PatListView.Columns["障害"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                PatListView.Columns["障害"].Visible = true;

                PatListView.Columns["病名"].HeaderText = "病名";
                PatListView.Columns["病名"].Width = 35;
                PatListView.Columns["病名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PatListView.Columns["病名"].Visible = true;

                PatListView.Columns["付箋"].HeaderText = "付箋";
                PatListView.Columns["付箋"].Width = 85;
                PatListView.Columns["付箋"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                PatListView.Columns["付箋"].Visible = true;

                PatListView.Columns["無オーダー"].Width = 28;
                PatListView.Columns["無オーダー"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PatListView.Columns["無オーダー"].DefaultCellStyle.ForeColor = Color.Blue;
                PatListView.Columns["無オーダー"].ToolTipText = "■当科オーダー無し";

                PatListView.Columns["未施行"].Width = 28;
                PatListView.Columns["未施行"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PatListView.Columns["未施行"].DefaultCellStyle.ForeColor = Color.Red;
                PatListView.Columns["未施行"].ToolTipText = "●当科未施行あり" + Environment.NewLine + "○他科未施行あり";

                PatListView.Columns["自費"].Width = 28;
                PatListView.Columns["自費"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["初再"].Width = 28;
                PatListView.Columns["初再"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PatListView.Columns["初再"].Visible = false;

                PatListView.Columns["指導"].Width = 28;
                PatListView.Columns["指導"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["在宅"].Width = 28;
                PatListView.Columns["在宅"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["処方"].Width = 28;
                PatListView.Columns["処方"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["注射"].Width = 28;
                PatListView.Columns["注射"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["処置"].Width = 28;
                PatListView.Columns["処置"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["手術"].Width = 28;
                PatListView.Columns["手術"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["検査"].Width = 28;
                PatListView.Columns["検査"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PatListView.Columns["検査"].ToolTipText = "○未施行" + Environment.NewLine + "●施行済";

                PatListView.Columns["画像"].Width = 28;
                PatListView.Columns["画像"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PatListView.Columns["画像"].ToolTipText = "□未施行" + Environment.NewLine + "■施行済";

                PatListView.Columns["リハ"].Width = 28;
                PatListView.Columns["リハ"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                PatListView.Columns["他"].Width = 28;
                PatListView.Columns["他"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            PatListView.Columns["Obj"].Visible = false;

//            PatListView.Sort(PatListView.Columns[tmpColumn], tmpDirection);
//            PatListView.Sort(PatListView.Columns[tmpColumnName], tmpDirection);

            if (this.Font.Size > 9)
            {
                foreach (DataGridViewColumn c in PatListView.Columns)
                {
                    c.Width = (int)(c.Width * (float)(this.Font.Size / 9));
                }
            }

            foreach (DataGridViewRow r in this.PatListView.Rows)
            {
                if (UserMode1 == UserMode.Iji || UserMode1 == UserMode.Iji1 || UserMode1 == UserMode.Iji2)
                {
                    if (r.Cells["氏名"].Value.ToString().StartsWith("◎") ||
                        r.Cells["氏名"].Value.ToString().StartsWith("☆") ||
                        r.Cells["氏名"].Value.ToString().StartsWith("◇") ||
                        r.Cells["氏名"].Value.ToString().StartsWith("○"))
                    {
                        r.Cells["氏名"].ToolTipText = "◎分割　☆口座引落　◇透析　○地域包括" + Environment.NewLine + r.Cells["氏名"].Value.ToString();
                    }
                }

                if (r.Cells["無オーダー"].Value.ToString().Equals("■"))
                {
                    r.Cells["無オーダー"].ToolTipText = "■当科オーダー無し";
                }

                if (r.Cells["未施行"].Value.ToString().Equals("●"))
                {
                    r.Cells["未施行"].ToolTipText = "●当科未施行あり";
                }
                else if (r.Cells["未施行"].Value.ToString().Equals("○"))
                {
                    r.Cells["未施行"].ToolTipText = "○他科未施行あり";
                }

                if (r.Cells["処方"].Value.ToString().Equals("内"))
                {
                    r.Cells["処方"].Style.ForeColor = Color.Blue;
                }
                else if (r.Cells["処方"].Value.ToString().Equals("外"))
                {
                    r.Cells["処方"].Style.ForeColor = Color.Red;
                }

                if (r.Cells["検査"].Value.ToString().Equals("○"))
                {
                    r.Cells["検査"].ToolTipText = "未施行";
                }
                else if (r.Cells["検査"].Value.ToString().Equals("●"))
                {
                    r.Cells["検査"].ToolTipText = "施行済";
                }

                if (r.Cells["画像"].Value.ToString().Equals("□"))
                {
                    r.Cells["画像"].ToolTipText = "未施行";
                }
                else if (r.Cells["画像"].Value.ToString().Equals("■"))
                {
                    r.Cells["画像"].ToolTipText = "施行済";
                }
            }

            this.ListFormat2();
        }

        /// <summary>
        /// 人数集計やグリッドの色変更を行う。ソートの後に行う必要がある。
        /// </summary>
        void ListFormat2()
        {
            int count1 = 0;
            int count2 = 0;
            int count3 = 0;

            for (int i = 0; i < PatListView.Rows.Count; i++)
            {
                DataGridViewRow r = PatListView.Rows[i];

                // カルテモードが「会計」の場合は、会計状況に応じて背景色を変える
                if (this.ShowKarteAllEnd.Checked)
                {
                    if (r.Cells["会計"].Value.ToString().Length > 0)
                    {
                        r.DefaultCellStyle.BackColor = Color.LightGray;
                        count3++;
                    }
                    else
                    {
                        r.DefaultCellStyle.BackColor = Color.White;
                        count1++;
                    }
                }
                else
                {
                    if (r.Cells["終了"].Value.ToString().Length > 0)
                    {
                        r.DefaultCellStyle.BackColor = Color.LightGray;
                        count3++;
                    }
                    else if (r.Cells["中断"].Value.ToString().Length > 0)
                    {
                        r.DefaultCellStyle.BackColor = Color.LightYellow;
                        count2++;
                    }
                    else if (r.Cells["開始"].Value.ToString().Length > 0)
                    {
                        r.DefaultCellStyle.BackColor = Color.LightYellow;
                        count2++;
                    }
                    else
                    {
                        count1++;
                    }

                }

                if (r.Cells["性別"].Value.ToString() == "女")
                {
                    r.Cells["ID"].Style.ForeColor = Color.Red;
                    r.Cells["カナ"].Style.ForeColor = Color.Red;
                    r.Cells["氏名"].Style.ForeColor = Color.Red;
                    r.Cells["性別"].Style.ForeColor = Color.Red;
                    r.Cells["年齢"].Style.ForeColor = Color.Red;
                }
            }

            if (this.ShowKarteAllEnd.Checked)
            {
                this.CountLabel1.Text = "未会計 " + count1 + " 人";
                this.CountLabel2.Visible = false;
                this.CountLabel3.Text = "会計済 " + count3 + " 人";
            }
            else
            {
                this.CountLabel1.Text = "診察前 " + count1 + " 人";
                this.CountLabel2.Visible = true;
                this.CountLabel2.Text = "診察中 " + count2 + " 人";
                this.CountLabel3.Text = "診察終了 " + count3 + " 人";
            }
        }

        private void DeptBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ListShow();
        }

        private void DoctorBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ListShow();
        }

        private void PatListView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.ListFormat2();
        }
        
        private void ShowButton_Click(object sender, EventArgs e)
        {
            this.ListShow();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            this.ListShow();
        }

        private void ShowKarteYet_CheckedChanged(object sender, EventArgs e)
        {
            this.ListFormat1();
        }

        private void ShowKarteStart_CheckedChanged(object sender, EventArgs e)
        {
            this.ListFormat1();
        }

        private void ShowKarteEnd_CheckedChanged(object sender, EventArgs e)
        {
            this.ListFormat1();
        }

        private void ShowInBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.ListFormat1();
        }

        private void KeywordBox1_TextChanged(object sender, EventArgs e)
        {
            this.ListFormat1();
        }

        private void ShowKarteAllEnd_CheckedChanged(object sender, EventArgs e)
        {
            // 全科終了が on の場合は、診察未開始・開始・終了は無関係となる。
            if (ShowKarteAllEnd.Checked)
            {
                this.ShowKarteYet.Enabled = false;
                this.ShowKarteStart.Enabled = false;
                this.ShowKarteEnd.Enabled = false;
            }
            else
            {
                this.ShowKarteYet.Enabled = true;
                this.ShowKarteStart.Enabled = true;
                this.ShowKarteEnd.Enabled = true;

                if (UserMode1 == UserMode.Iji)
                {
                    this.ShowKarteYet.Checked = true;
                    this.ShowKarteStart.Checked = true;
                    this.ShowKarteEnd.Checked = true;
                }
            }

            this.ListFormat1();
        }

        private void ShowReceYet_CheckedChanged(object sender, EventArgs e)
        {
            this.ListFormat1();
        }

        private void ShowReceEnd_CheckedChanged(object sender, EventArgs e)
        {
            this.ListFormat1();
        }

        private void LabelPrintMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("医療安全管理上よくないとの理由で、この機能は使用不可となりました。\r\n申し訳ございません。（H21/3/31）");
        }

        private void KarteMenuItem_Click(object sender, EventArgs e)
        {
            if (PatListView.SelectedRows.Count > 0)
            {
                FormControl.FormPat_Show((PatOut)PatListView.SelectedRows[0].Cells["Obj"].Value);
            }
        }

        private void KaikeiTimeNowMenuItem_Click(object sender, EventArgs e)
        {
            if (PatListView.SelectedRows.Count > 0)
            {
#if INNO
                int time = int.Parse(DateTime.Now.ToString("HHmmss"));
#else
                int time = int.Parse(DateTime.Now.ToString("HHmm"));
#endif

                string come_date = this.DatePicker1.Value.ToString("yyyyMMdd");
                string seq1 = PatListView.SelectedRows[0].Cells["通番"].Value.ToString();
                string seq2 = PatListView.SelectedRows[0].Cells["連番"].Value.ToString();

                string msg = "受付番号 " + seq1 +
                    "  患者ID " + PatListView.SelectedRows[0].Cells["ID"].Value.ToString() +
                    " " + PatListView.SelectedRows[0].Cells["氏名"].Value.ToString() + " 様" + Environment.NewLine +
                    "の会計時間を入力します。よろしいですか？";

                if (MessageBox.Show(msg, "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    PatOut.SetKaikeiTimeBySEQ(time, come_date, seq1, seq2);
                    this.ListShow();
                }
            }
        }

        private void KaikeiTime0MenuItem_Click(object sender, EventArgs e)
        {
            if (PatListView.SelectedRows.Count > 0)
            {
                string come_date = this.DatePicker1.Value.ToString("yyyyMMdd");
                string seq1 = PatListView.SelectedRows[0].Cells["通番"].Value.ToString();
                string seq2 = PatListView.SelectedRows[0].Cells["連番"].Value.ToString();

                PatOut.SetKaikeiTimeBySEQ(0, come_date, seq1, seq2);
                this.ListShow();
            }
        }

        private void KarteStartTimeNowMenuItem_Click(object sender, EventArgs e)
        {
            if (PatListView.SelectedRows.Count > 0)
            {
#if INNO
                int time = int.Parse(DateTime.Now.ToString("HHmmss"));
#else
                int time = int.Parse(DateTime.Now.ToString("HHmm"));
#endif

                string come_date = this.DatePicker1.Value.ToString("yyyyMMdd");
                string seq1 = PatListView.SelectedRows[0].Cells["通番"].Value.ToString();
                string seq2 = PatListView.SelectedRows[0].Cells["連番"].Value.ToString();

                string msg = "受付番号 " + seq1 +
                    "  患者ID " + PatListView.SelectedRows[0].Cells["ID"].Value.ToString() +
                    " " + PatListView.SelectedRows[0].Cells["氏名"].Value.ToString() + " 様" + Environment.NewLine +
                    "の診察開始時間を入力します。よろしいですか？";

                if (MessageBox.Show(msg, "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    PatOut.SetKarteStartTimeBySEQ(time, come_date, seq1, seq2, LoginUser.DeptId, LoginUser.DoctorId);
                    this.ListShow();
                }
            }
        }

        private void KarteStartTime0MenuItem_Click(object sender, EventArgs e)
        {
            if (PatListView.SelectedRows.Count > 0)
            {
                string come_date = this.DatePicker1.Value.ToString("yyyyMMdd");
                string seq1 = PatListView.SelectedRows[0].Cells["通番"].Value.ToString();
                string seq2 = PatListView.SelectedRows[0].Cells["連番"].Value.ToString();

                PatOut.SetKarteStartTimeBySEQ(0, come_date, seq1, seq2, "0", "0");
                this.ListShow();
            }
        }

        private void KarteEndTimeNowMenuItem_Click(object sender, EventArgs e)
        {
            if (PatListView.SelectedRows.Count > 0)
            {
#if INNO
                int time = int.Parse(DateTime.Now.ToString("HHmmss"));
#else
                int time = int.Parse(DateTime.Now.ToString("HHmm"));
#endif

                string come_date = this.DatePicker1.Value.ToString("yyyyMMdd");
                string seq1 = PatListView.SelectedRows[0].Cells["通番"].Value.ToString();
                string seq2 = PatListView.SelectedRows[0].Cells["連番"].Value.ToString();

                string msg = "受付番号 " + seq1 +
                    "  患者ID " + PatListView.SelectedRows[0].Cells["ID"].Value.ToString() +
                    " " + PatListView.SelectedRows[0].Cells["氏名"].Value.ToString() + " 様" + Environment.NewLine +
                    "の診察終了時間を入力します。よろしいですか？";

                if (MessageBox.Show(msg, "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    PatOut.SetKarteEndTimeBySEQ(time, come_date, seq1, seq2);
                    this.ListShow();
                }
            }
        }

        private void KarteEndTime0MenuItem_Click(object sender, EventArgs e)
        {
            if (PatListView.SelectedRows.Count > 0)
            {
                string come_date = this.DatePicker1.Value.ToString("yyyyMMdd");
                string seq1 = PatListView.SelectedRows[0].Cells["通番"].Value.ToString();
                string seq2 = PatListView.SelectedRows[0].Cells["連番"].Value.ToString();

                PatOut.SetKarteEndTimeBySEQ(0, come_date, seq1, seq2);
                this.ListShow();
            }
        }

        /// <summary>
        /// その日の何回目の会計か
        /// </summary>
        /// <param name="list">その日の診察状況データを会計時間順（昇順）にソートされたリスト。</param>
        /// <param name="time5">会計時間</param>
        /// <returns></returns>
        private int KaikeiCount(List<PatOut> list, string time5)
        {
            // 会計時間が入っている場合は、修正なので「その会計時間は、その日の何回目の会計か」とする。
            // 会計時間が入っていない場合は、新規なので「その日に会計を行った回数 + 1」とする。
            int i = 1;

            string tmp_time5 = "0";

            foreach (PatOut obj in list)
            {
                if (tmp_time5.Equals(obj.TimeString5))
                {
                    // 前と同じ会計時間ならば飛ばす
                    continue;
                }

                if (obj.Time5.Equals("0"))
                {
                    // 会計時間が入っていなければ飛ばす
                    continue;
                }

                tmp_time5 = obj.TimeString5;

                if (tmp_time5.Equals(time5))
                {
                    break;
                }

                i++;
            }

            return i;
        }

        void OrderKaikeiShow()
        {
            if (PatListView.SelectedRows.Count > 0)
            {
                DataGridViewRow r = PatListView.SelectedRows[0];

                string pt_id = r.Cells["ID"].Value.ToString();
                string seq1 = r.Cells["通番"].Value.ToString();
                string come_date = DatePicker1.Value.ToString("yyyyMMdd");
                string dept_code = r.Cells["科コード"].Value.ToString();
                string doctor_code = r.Cells["医師コード"].Value.ToString();
                string ins_seq = r.Cells["保険パターン番号"].Value.ToString();

                if (dept_code.Length == 0)
                {
                    dept_code = "0";
                }

                if (doctor_code.Length == 0)
                {
                    doctor_code = "0";
                }

                if (ins_seq.Length == 0)
                {
                    ins_seq = "0";
                }

                // 会計時間
                string time5 = r.Cells["会計"].Value.ToString();

                // その日の診察状況データを会計時間順（昇順）にソートしたリスト
#if INNO
                List<PatOut> list = PatOut.GetOneday(pt_id, come_date, "BILL_TIME");
#else
                List<PatOut> list = PatOut.GetOneday(pt_id, come_date, "会計時間");
#endif

                // その日の何回目の会計か。
                // 会計時間が入っている場合は、修正なので「その会計時間は、その日の何回目の会計か」とする。
                // 会計時間が入っていない場合は、新規なので「その日に会計を行った回数 + 1」とする。
                int i = this.KaikeiCount(list, time5);

                FormControl.FormOrderKaikei_Show(PatBase.Load(pt_id), FormOrderKaikei.Mode.Out, come_date, come_date, i, dept_code, doctor_code, ins_seq);
            }
        }

        private void OrderKaikeiMenuItem_Click(object sender, EventArgs e)
        {
            this.OrderKaikeiShow();
        }

        private void FormPatList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                ListShow();
            }
        }

        private void PatListView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            DataGridViewRow r = PatListView.Rows[e.RowIndex];

            PatOut obj = (PatOut)r.Cells["Obj"].Value;
            string pt_id = r.Cells["ID"].Value.ToString();
            string seq1 = r.Cells["通番"].Value.ToString();
            string come_date = DatePicker1.Value.ToString("yyyyMMdd");
            string dept_code = r.Cells["科コード"].Value.ToString();
            string doctor_code = r.Cells["医師コード"].Value.ToString();
            string ins_seq = r.Cells["保険パターン番号"].Value.ToString();

            if (dept_code.Length == 0)
            {
                dept_code = "0";
            }

            if (doctor_code.Length == 0)
            {
                doctor_code = "0";
            }

            if (ins_seq.Length == 0)
            {
                ins_seq = "0";
            }

            // 患者IDをクリップボードに貼り付け
            Clipboard.SetText(pt_id);

            if (UserMode1 == UserMode.Iji)
            {
                List<string> msgs = new List<string>();

                if (r.Cells["属性コード"].Value.ToString().Equals("3"))
                {
                    msgs.Add("透析の患者です。");
                }

                if (r.Cells["終了"].Value.ToString().Length == 0)
                {
                    msgs.Add(r.Cells["科"].Value.ToString() + " 診察が終了していません。");
                }

                if (msgs.Count > 0)
                {
                    string msg = AppString.ConcatList(msgs, Environment.NewLine) + Environment.NewLine + "オーダーを取込みますか？";

                    if (MessageBox.Show(msg, "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    {
                        return;
                    }
                }

                msgs.Clear();

#if INNO
#else
                if (!File.Exists(LibSettings.Current.OrderXmlExe))
                {
                    msgs.Add("オーダー転送アプリ " + LibSettings.Current.OrderXmlExe + " が存在しません。");
                }
#endif

                if (!File.Exists(LibSettings.Current.ReceApiExe))
                {
                    msgs.Add("医事会計APIアプリ " + LibSettings.Current.ReceApiExe + " が存在しません。");
                }

                if (!File.Exists(LibSettings.Current.ReceExe))
                {
                    msgs.Add("医事会計アプリ " + LibSettings.Current.ReceExe + " が存在しません。");
                }

                if (msgs.Count > 0)
                {
                    MessageBox.Show(AppString.ConcatList(msgs, Environment.NewLine));
                    return;
                }

                // 会計時間
                string time5 = r.Cells["会計"].Value.ToString();

                // その日の診察状況データを会計時間順（昇順）にソートしたリスト
#if INNO
                List<PatOut> list = PatOut.GetOneday(pt_id, come_date, "BILL_TIME");
#else
                List<PatOut> list = PatOut.GetOneday(pt_id, come_date, "会計時間");
#endif

                // その日の何回目の会計か。
                // 会計時間が入っている場合は、修正なので「その会計時間は、その日の何回目の会計か」とする。
                // 会計時間が入っていない場合は、新規なので「その日に会計を行った回数 + 1」とする。
                int i = this.KaikeiCount(list, time5);

                msgs.Clear();

                /*
                 * 入力練習中は、会計入力済みの場合は、直接オーダーが飛ぶようにする。
                 * 本番稼働時にはコメントアウトを外す。2015/07/06
                 * →　やはり本番もこのままで行くことになった 2015/09/29
                if (time5.Length > 0)
                {
                    msg += "・すでに会計入力済みです。" + Environment.NewLine;
                }
                */

                if (r.Cells["未施行"].Value.ToString().Equals("●") || r.Cells["未施行"].Value.ToString().Equals("○"))
                {
                    // 未施行がある場合
                    msgs.Add("・未施行のオーダーがあります。");
                }

                if (i > 1)
                {
                    // ２回目以降の会計の場合
                    msgs.Add("・本日２回目以降の会計です。");
                }


                if (msgs.Count > 0)
                {
                    MessageBox.Show("以下の理由により、オーダー会計取込み画面を表示します。" + Environment.NewLine + Environment.NewLine + AppString.ConcatList(msgs, Environment.NewLine));
                    FormControl.FormOrderKaikei_Show(PatBase.Load(pt_id), FormOrderKaikei.Mode.Out, come_date, come_date, i, dept_code, doctor_code, ins_seq);
                }
                else
                {
                    // ID701RC.F20 に会計入力者コードをセット
                    PatOut.SetKaikeiStaffBySEQ(LoginUser.Id, come_date, seq1);
#if INNO
                    // 施行済・未会計のものを取得する
                    List<PatOrder> order_list = PatOrder.GetListByPatDates(pt_id, come_date, come_date, "1", null, null, true).FindAll((x) =>
                        {
                            return x.SekouFlg.Equals("1") && !x.KaikeiFlg.Equals("1");
                        });

                    if (order_list.Count > 0)
                    {
                        Q26.Execute(order_list, DateTime.Now, 1, i, true, LibSettings.Current.Proas.OrderXmlTmpFolder, LibSettings.Current.Proas.OrderXmlDstFolder, true, LoginUser.Id);
                        Thread.Sleep(LibSettings.Current.OrderReceApiIntervalInt * 1000);
                    }

                    Process.Start(LibSettings.Current.ReceApiExe, "1 " + pt_id + " " + i.ToString() + " " + dept_code + " " + doctor_code + " " + ins_seq);
#else
                    // 会計入力の場合は Q26_OrderXml.exe を起動
                    Process p = Process.Start(LibSettings.Current.OrderXmlExe, "-m1 1 " + pt_id + " " + come_date + " " + i.ToString() + " " + LoginUser.Id);

                    if (p.WaitForExit(20 * 1000))
                    {
                        Thread.Sleep(LibSettings.Current.OrderReceApiIntervalInt * 1000);

                        // レセを直接起動することはしない 2015/06/26
//                        p = Process.Start(LibSettings.Current.ReceExe, "F03GS^^^^1^" + pt_id + "^" + come_date + "^" + i.ToString() + "^ADMIN0");
                        p = Process.Start(LibSettings.Current.ReceApiExe, "1 " + pt_id + " " + i.ToString() + " " + dept_code + " " + doctor_code + " " + ins_seq);
                    }
#endif
                }
            }
            else
            {
                FormControl.FormPat_Show(obj);
            }
        }

        private void PatListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow r = PatListView.Rows[e.RowIndex];

            // ダブルクリックした時は、AppStat.CurrentPat を変更するが
            // シングルクリックの時は、このアプリ内の Pat.Id のみ変更する。
            this.PatSet(PatBase.Load(r.Cells["ID"].Value.ToString()));

            this.DeptCode = r.Cells["科コード"].Value.ToString();
        }

        /// <summary>
        /// 患者データをクリアする（病名、付箋）
        /// </summary>
        void PtDataClear()
        {
            this.Pat.Id = "";

            this.DiagView1.ListClear();
            this.ProblemView1.ListClear();
            this.PostItView1.ListClear();

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
                this.PostItView1.ListShow(pt_id);

                Memo memo = Memo.Load(pt_id, "0");
                this.MemoBox1.Text = memo.Cont;
                this.MemoDateTimeLabel1.Text = memo.UpDateTime;
                this.MemoStaffLabel1.Text = memo.UpStaffName;
            }

            if (FormControl.IsFormOrderKaikeiOpen)
            {
                this.OrderKaikeiShow();
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

            if (this.DiagView1.Columns.Contains("ICDコード"))
            {
                DiagView1.Columns["ICDコード"].Visible = false;
            }

            DiagView1.ListFormat("開始日 desc, 連番 desc", AppString.ConcatList(filters, " or "));


            filters.Clear();

            if (this.ProblemSectionBox1.GetSection().Code > 0)
            {
                filters.Add("資格コード = " + this.ProblemSectionBox1.GetSection().Code);
            }

            // 外来のみ表示する
            if (!this.ProblemCheckBox1.Checked)
            {
                filters.Add("入外区分 = '外来'");
            }

            // 未解決のみ表示する
            if (!this.ProblemCheckBox2.Checked)
            {
                filters.Add("解決日 = ''");
            }

            ProblemView1.ListFormat("プロブレムＮＯ desc", AppString.ConcatList(filters, " and "));

            filters.Clear();

            if (this.PostItCheckBox1.Checked)
            {
                filters.Add("対象日 = '" + DateTime.Now.ToString("yyyyMMdd") + "'");
            }

            if (!this.PostItCheckBox2.Checked)
            {
                filters.Add("削除フラグ = False");
            }

            PostItView1.ListFormat("対象日 desc, 表示順", AppString.ConcatList(filters, " and "));
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
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

        private void PostItCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.PtDataFormat();
        }

        private void PostItCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.PtDataFormat();
        }

        private void TickBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Timer1.Interval = int.Parse(this.TickIntervalBox1.Text) * 1000;
        }

        private void TimeModeBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
#if INNO
            if (this.TimeModeBox1.Text.Equals("救急"))
            {
                this.TimeMode1 = TimeMode.QQ;
            }
            else if (this.TimeModeBox1.Text.Equals("在宅"))
            {
                this.TimeMode1 = TimeMode.Home;
            }
            else
            {
                this.TimeMode1 = TimeMode.Normal;
            }
#else
            if (this.TimeModeBox1.Text.Equals("救急"))
            {
                this.TimeMode1 = TimeMode.QQ;
            }
            else
            {
                this.TimeMode1 = TimeMode.Normal;
            }
#endif

            this.ListShow();
        }

        private void UserModeBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.UserModeBox1.Text.Equals("診察"))
            {
                this.UserMode1 = UserMode.Doctor;
                this.TickIntervalBox1.Text = "30";
            }
            else if (this.UserModeBox1.Text.Equals("中央医事"))
            {
                this.UserMode1 = UserMode.Iji;
                this.TickIntervalBox1.Text = "30";
            }
            else if (this.UserModeBox1.Text.Equals("各科医事"))
            {
                this.UserMode1 = UserMode.Iji1;
                this.TickIntervalBox1.Text = "60";
            }

            this.ListShow();
        }

        private void FormDeptStatButton1_Click(object sender, EventArgs e)
        {
            FormControl.FormDeptStat_Show();
        }

        private void MemoButton1_Click(object sender, EventArgs e)
        {
            if (this.Pat.Id.Length == 0)
            {
                return;
            }

            FormMemo f = new FormMemo();
            f.PatSet(PatBase.Load(this.Pat.Id));
            f.ShowDialog();
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
            f.PatSet(PatBase.Load(this.Pat.Id));
            f.ShowDialog();
        }

        private void PostItButton1_Click(object sender, EventArgs e)
        {
            if (this.Pat.Id.Length == 0)
            {
                return;
            }

            FormPostIt1 f = new FormPostIt1(false, this.DeptCode);
            f.PatSet(PatBase.Load(this.Pat.Id));
            f.ShowDialog();
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

            FormProblem1 f = new FormProblem1(true, "1", this.DeptCode);
            f.PatSet(PatBase.Load(this.Pat.Id));
            f.ShowDialog();
        }

        private void TickModeButton1_Click(object sender, EventArgs e)
        {
            this.TickMode = !this.TickMode;
        }

        private void FormOrderKaikeiButton1_Click(object sender, EventArgs e)
        {
            FormControl.FormOrderKaikei_Show(FormOrderKaikei.Mode.Out);
        }

        private void DetailBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.DetailMode = this.DetailBox1.Checked;
        }

        private void PatListView_Sorted(object sender, EventArgs e)
        {
            this.PatListViewSort = this.PatListView.SortedColumn.Name;
            this.PatListViewSortOrder = this.PatListView.SortOrder;

            this.ListFormat1();
        }

        private void ManualButton1_Click(object sender, EventArgs e)
        {
            string file = AppFile.FilePath(@"doc\FormPatList1.pdf");

            if (File.Exists(file))
            {
                System.Diagnostics.Process.Start(file);
            }
            else
            {
                MessageBox.Show("ファイルが存在しません");
            }
        }
    }
}