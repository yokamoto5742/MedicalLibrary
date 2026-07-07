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
    public partial class FormRsv : StdForm1
    {
        CalSize cs = new CalSize();

        Graphics g1;
        Graphics gt1;
        Graphics gd1;

        Pen p1 = new Pen(Color.LightGray, 1);
        Pen p2 = new Pen(Color.Black, 2);

        Pen prd1 = new Pen(Color.Red, 1);

        Pen pr2 = new Pen(Color.Red, 2);
        Pen pg2 = new Pen(Color.Green, 2);
        Pen pb2 = new Pen(Color.Blue, 2);

        string Code1
        {
            get
            {
                return this.CodeBox1.Text;
            }
        }

        string Code2
        {
            get
            {
                string s = "";

                if (this.Code1.Contains(" "))
                {
                    s = this.Code1.Split(' ')[0];
                }

                return s;
            }
        }

        /// <summary>
        /// 時間枠のリスト
        /// </summary>
        List<WakuBox> waku_list = new List<WakuBox>();

        /// <summary>
        /// 予約のリスト
        /// </summary>
        List<WakuBox> rsv_list = new List<WakuBox>();

        /// <summary>
        /// 現在開いている予約枠
        /// </summary>
        WakuBox Waku1 = new WakuBox();

        bool IsFull
        {
            get
            {
                bool is_full = false;

                if (this.RsvLabel1.BackColor == Color.LightPink)
                {
                    is_full = true;
                }

                return is_full;
            }
            set
            {
                if (value)
                {
                    this.RsvLabel1.BackColor = Color.LightPink;
                }
                else
                {
                    this.RsvLabel1.BackColor = Color.LightYellow;
                }
            }
        }

        /// <summary>
        /// 連続予約できる枠数
        /// </summary>
        int NextWakus = 0;


        DataSet dSet = new DataSet();


        public FormRsv()
        {
            InitializeComponent();

            DataTable table = dSet.Tables.Add("Rsv");

            table.Columns.Add("ID");
            table.Columns.Add("氏名");
            table.Columns.Add("性別");
            table.Columns.Add("年齢");
            table.Columns.Add("開始");
            table.Columns.Add("終了");
            table.Columns.Add("備考１");
            table.Columns.Add("備考２");
        }

        private void FormRsv_Load(object sender, EventArgs e)
        {
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            this.RsvShow();
        }

        private void CodeBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.CodeBox1.Text.Length == 0)
                {
                    return;
                }

                this.CodeSet(int.Parse(DateBox1.Value.ToString("yyyyMMdd")), this.CodeBox1.Text, "");
            }
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            if (this.CodeBox1.Text.Length == 0)
            {
                return;
            }

            this.RsvShow();
        }

        public void CodeSet(int crit_date, string code1, string name1, string code2 = "", string name2 = "")
        {
            this.Text = name1;

            if (name2.Length > 0)
            {
                this.Text += " " + name2;
            }

            this.CodeBox1.Text = code1;
            this.DateBox1.Value = DateTime.Parse(DateTimeAgent.DateFormat(crit_date, DateTimeAgent.DateFormatKind.LONG));

            List<RsvMaster> list = RsvMaster.GetDetailList(code1);

            CodeBox2.Items.Clear();

            foreach (RsvMaster obj in list)
            {
                CodeBox2.Items.Add(obj.Code2 + " " + obj.Name2);

                if (code2.Equals(obj.Code2))
                {
                    CodeBox2.Text = obj.Code2 + " " + obj.Name2;
                }
            }
        }

        /// <summary>
        /// カレンダーを表示する
        /// </summary>
        /// <param name="code1">種別コード</param>
        /// <param name="code2">詳細コード</param>
        /// <param name="date1">開始日。デフォルトは今日。</param>
        /// <param name="date2">終了日。デフォルトは４週間後。</param>
        void RsvShow()
        {
            string code1 = this.Code1;
            string code2 = this.Code2;
            int date1 = int.Parse(DateBox1.Value.ToString("yyyyMMdd"));
            int date2 = int.Parse(DateBox1.Value.AddDays(AppStat.RsvDays).ToString("yyyyMMdd"));

            this.RsvDetailClear();

            waku_list.Clear();
            rsv_list.Clear();

            List<RsvMaster> list = RsvMaster.GetDataList(code1, code2);

            // 予約枠の開始時刻・終了時刻
            int time1 = 0;
            int time2 = 0;

            foreach (RsvMaster obj in list)
            {
                if (obj.StartDate > date2 || (obj.EndDate < date1 && obj.EndDate > 0))
                {
                    continue;
                }

                if (time1 == 0 || obj.Time1 < time1)
                {
                    time1 = obj.Time1;
                }

                if (time2 == 0 || obj.Time2 > time2)
                {
                    time2 = obj.Time2;
                }
            }

            // time1, time2 をもとに、表示する予約枠の開始時刻・終了時刻を計算する
            int start_time = time1;
            int end_time = time2;

            if (time1 % 100 > 0)
            {
                start_time = (start_time / 100) * 100;
            }

            if (time2 % 100 > 0)
            {
                end_time = (end_time / 100 + 1) * 100;
            }

            int box_width = (DateTimeAgent.IntervalDays(date1, date2) + 1) * cs.W_Day + 10;
            int box_height = DateTimeAgent.IntervalMinutes(start_time, end_time) + 10;

            RsvBox1.Width = box_width;
            RsvBox1.Height = box_height;

            RsvDateBox1.Width = box_width;
            RsvTimeBox1.Height = box_height;

            Bitmap b1 = new Bitmap(box_width, box_height);
            RsvBox1.Image = b1;
            g1 = Graphics.FromImage(b1);

            Bitmap bd1 = new Bitmap(box_width, cs.H_Date);
            RsvDateBox1.Image = bd1;
            gd1 = Graphics.FromImage(bd1);

            Bitmap bt1 = new Bitmap(cs.W_Time + 10, box_height);
            RsvTimeBox1.Image = bt1;
            gt1 = Graphics.FromImage(bt1);

            RsvPanel1.AutoScrollPosition = new Point(0, 0);

            RsvDateBox1.Location = new Point(3, 3);
            RsvTimeBox1.Location = new Point(3, 3);

            //            MessageBox.Show("ScrollX = " + RsvBox1.AutoScrollOffset.X + ", ScrollY = " + RsvBox1.AutoScrollOffset.Y + ", LocX = " + RsvBox1.Location.X + ", LocY = " + RsvBox1.Location.Y);
            RsvBox1.Location = new Point(3, 3);
            //            RsvBox1.AutoScrollOffset = new Point(0, 0);

            // まずは時間枠を作る
            int tmp_time = start_time;

            for (int i = 0; true; i++)
            {
                if (tmp_time >= end_time)
                {
                    break;
                }

                gt1.FillRectangle(Brushes.LightYellow, 0, cs.H_Hour * i, cs.W_Time, cs.H_Hour);
                gt1.DrawRectangle(p1, 0, cs.H_Hour * i, cs.W_Time, cs.H_Hour);
                gt1.DrawString(tmp_time.ToString(), AppFont.F9.Ft, Brushes.Black, 5, cs.H_Hour * i + 5);
                /*
                                Waku waku = new Waku();
                                waku.X1 = 0;
                                waku.X2 = cs.W_Time - 1;
                                waku.Y1 = cs.H_Hour * i;
                                waku.Y2 = cs.H_Hour * (i + 1) - 1;
                                waku.Cont = tmp_time.ToString();

                                waku_list.Add(waku);
                */
                tmp_time = DateTimeAgent.AddTime(tmp_time, 60);
            }


            DateTime dt1 = DateTime.Parse(date1.ToString().Insert(4, "/").Insert(7, "/") + " 00:00:00");
            DateTime dt2 = DateTime.Parse(date2.ToString().Insert(4, "/").Insert(7, "/") + " 00:00:00");

            // 予約データの取得
            Dictionary<int, List<RsvData>> rsv_dict = RsvData.GetDict(code1, code2, date1, date2);


            for (int d = 0; true; d++)
            {
                DateTime dt = dt1.AddDays(d);

                if (dt > dt2)
                {
                    break;
                }

                // 土日の場合は色を変える
                if (dt.DayOfWeek == DayOfWeek.Saturday)
                {
                    gd1.FillRectangle(Brushes.SkyBlue, d * cs.W_Day, 0, cs.W_Day, cs.H_Date);
                }
                else if (dt.DayOfWeek == DayOfWeek.Sunday)
                {
                    gd1.FillRectangle(Brushes.LightPink, d * cs.W_Day, 0, cs.W_Day, cs.H_Date);
                }
                else
                {
                    gd1.FillRectangle(Brushes.LightYellow, d * cs.W_Day, 0, cs.W_Day, cs.H_Date);
                }

                gd1.DrawRectangle(p1, d * cs.W_Day, 0, cs.W_Day, cs.H_Date);
                gd1.DrawString(dt.ToString("M/d\n(ddd)"), AppFont.F9.Ft, Brushes.Black, d * cs.W_Day + 5, 5);

                int dt_int = int.Parse(dt.ToString("yyyyMMdd"));
                int dt_wday = (int)(dt.DayOfWeek);

                // その日の予約マスター
                RsvMaster master = new RsvMaster();

                foreach (RsvMaster obj in list)
                {
                    if (!obj.IsOn(dt_int))
                    {
                        continue;
                    }

                    master = obj;
                    break;
                }

                // 午前・午後・夕方の予約枠を作る
                for (int i = 1; i <= 3; i++)
                {
                    RsvWaku rsv_waku = master.Wakus[i, dt_wday + 1];

                    tmp_time = rsv_waku.Time1;

                    for (int j = 0; true; j++)
                    {
                        if (tmp_time >= rsv_waku.Time2)
                        {
                            break;
                        }

                        WakuBox waku = new WakuBox();

                        waku.X1 = d * cs.W_Day;
                        waku.X2 = waku.X1 + cs.W_Day - 1;
                        waku.Y1 = DateTimeAgent.IntervalMinutes(start_time, tmp_time);
                        waku.Y2 = waku.Y1 + master.Interval - 1;
                        waku.RsvDate = dt_int;
                        waku.Time1 = tmp_time;
                        waku.Time2 = DateTimeAgent.AddTime(tmp_time, master.Interval);
                        waku.RsvMax = rsv_waku.RsvMax;
                        waku.Cont = rsv_waku.RsvMax.ToString();

                        waku_list.Add(waku);

                        g1.DrawRectangle(p1, waku.X1, waku.Y1, cs.W_Day, master.Interval);
                        g1.DrawString(rsv_waku.RsvMax.ToString(), AppFont.F9.Ft, Brushes.Gray, waku.X1 + 5, waku.Y1 + 5);

                        tmp_time = waku.Time2;
                    }
                }

                // その日の予約データ
                List<RsvData> today_list = new List<RsvData>();

                if (rsv_dict.ContainsKey(dt_int))
                {
                    today_list = rsv_dict[dt_int];
                }

                foreach (WakuBox waku in waku_list)
                {
                    // 日付が異なれば飛ばす
                    if (!waku.RsvDate.Equals(dt_int))
                    {
                        continue;
                    }

                    // その枠に被る予約データのリスト
                    List<RsvData> tmp_list = new List<RsvData>();

                    foreach (RsvData data in today_list)
                    {
                        // 枠に被っていれば含める
                        if (data.Time1 < waku.Time2 && data.Time2 > waku.Time1)
                        {
                            tmp_list.Add(data);
                        }
                    }

                    // 予約が１件もなければ飛ばす
                    if (tmp_list.Count == 0)
                    {
                        continue;
                    }


                    WakuBox rsv_waku = waku.Clone();

                    rsv_waku.Cont = tmp_list.Count.ToString() + "/" + rsv_waku.RsvMax.ToString();
                    rsv_waku.RsvDataList = tmp_list;

                    // 現在のカルテの患者
                    bool pt_flg = false;

                    if (this.Pat.Id.Length > 0)
                    {
                        foreach (RsvData tmp in tmp_list)
                        {
                            if (tmp.Pat.Id.Equals(this.Pat.Id))
                            {
                                pt_flg = true;
                            }
                        }
                    }

                    if (pt_flg)
                    {
                        // 現在のカルテの患者が含まれる場合
                        g1.FillRectangle(Brushes.Purple, rsv_waku.X1, rsv_waku.Y1, cs.W_Day, rsv_waku.Y2 - rsv_waku.Y1);
                    }
                    else
                    {
                        // 現在のカルテの患者が含まれない場合
                        g1.FillRectangle(Brushes.LightPink, rsv_waku.X1, rsv_waku.Y1, cs.W_Day, rsv_waku.Y2 - rsv_waku.Y1);
                    }

                    g1.DrawRectangle(p1, rsv_waku.X1, rsv_waku.Y1, cs.W_Day, rsv_waku.Y2 - rsv_waku.Y1);
                    g1.DrawString(rsv_waku.Cont, AppFont.F9.Ft, Brushes.Black, rsv_waku.X1 + 5, rsv_waku.Y1 + 5);

                    rsv_list.Add(rsv_waku);
                }
/*
                foreach (Tuple<int, int> key in rsv_dict.Keys)
                {
                    // 日付が異なる場合は飛ばす
                    if (key.Item1 != dt_int)
                    {
                        continue;
                    }

                    List<RsvData> tmp_list = rsv_dict[key];

                    RsvBox rsv_waku = new RsvBox();

                    rsv_waku.RsvDate = key.Item1;
                    rsv_waku.Time1 = key.Item2;

                    // 予約枠の最大数と終了時間を取得
                    foreach (WakuBox waku in waku_list)
                    {
                        if (waku.RsvDate.Equals(rsv_waku.RsvDate) && waku.Time1.Equals(rsv_waku.Time1))
                        {
                            rsv_waku.Time2 = waku.Time2;
                            rsv_waku.RsvMax = waku.RsvMax;
                            break;
                        }
                    }

                    rsv_waku.X1 = d * cs.W_Day;
                    rsv_waku.X2 = rsv_waku.X1 + cs.W_Day - 1;
                    rsv_waku.Y1 = DateTimeAgent.IntervalMinutes(start_time, key.Item2);
                    rsv_waku.Y2 = DateTimeAgent.IntervalMinutes(start_time, rsv_waku.Time2);

                    rsv_waku.Cont = tmp_list.Count.ToString() + "/" + rsv_waku.RsvMax.ToString();
                    rsv_waku.RsvDataList = tmp_list;

                    // 現在のカルテの患者
                    bool pt_flg = false;

                    if (this.Pat.Id.Length > 0)
                    {
                        foreach (RsvData tmp in tmp_list)
                        {
                            if (tmp.PtId.Equals(this.Pat.Id))
                            {
                                pt_flg = true;
                            }
                        }
                    }

                    if (pt_flg)
                    {
                        // 現在のカルテの患者が含まれる場合
                        g1.FillRectangle(Brushes.Purple, rsv_waku.X1, rsv_waku.Y1, cs.W_Day, rsv_waku.Y2 - rsv_waku.Y1);
                    }
                    else
                    {
                        // 現在のカルテの患者が含まれない場合
                        g1.FillRectangle(Brushes.LightPink, rsv_waku.X1, rsv_waku.Y1, cs.W_Day, rsv_waku.Y2 - rsv_waku.Y1);
                    }

                    g1.DrawRectangle(p1, rsv_waku.X1, rsv_waku.Y1, cs.W_Day, rsv_waku.Y2 - rsv_waku.Y1);
                    g1.DrawString(rsv_waku.Cont, AppFont.F9.Ft, Brushes.Black, rsv_waku.X1 + 5, rsv_waku.Y1 + 5);

                    rsv_list.Add(rsv_waku);
                }
 */
            }
        }

        void RsvDetailClear()
        {
            this.Waku1 = new WakuBox();
            this.NextWakus = 0;

            this.RsvLabel1.Text = "";
            this.RsvLabel1.BackColor = Color.LightYellow;

            DataTable table = dSet.Tables["Rsv"];
            table.Rows.Clear();

            this.RsvDetailFormat();

            this.NewRsvButton1.Enabled = false;
        }

        void RsvDetailShow()
        {
            DataTable table = dSet.Tables["Rsv"];
            table.Rows.Clear();

            this.RsvLabel1.Text = this.Waku1.RsvDateTime;

            List<RsvData> list = RsvData.GetListByDateTime(this.Code1, this.Code2, this.Waku1.RsvDate, this.Waku1.Time1, this.Waku1.Time2);

            this.NewRsvButton1.Enabled = true;

            if (list.Count >= this.Waku1.RsvMax)
            {
                this.IsFull = true;
                this.NewRsvButton1.Enabled = false;
            }
            else
            {
                this.IsFull = false;
            }

            foreach (RsvData rsv in list)
            {
                if (rsv.Pat.Id.Equals(this.Pat.Id))
                {
                    this.NewRsvButton1.Enabled = false;
                    break;
                }
            }

            foreach (RsvData data in list)
            {
                DataRow r = table.NewRow();

                r["ID"] = data.Pat.Id;
                r["氏名"] = data.Pat.Name;
                r["性別"] = data.Pat.SexNameShort;

                if (data.Pat.Birth.Length == 8)
                {
                    r["年齢"] = DateTimeAgent.AgeCalc(data.Pat.Birth, data.RsvDate.ToString());
                }

                r["開始"] = DateTimeAgent.TimeFormat(data.Time1);
                r["終了"] = DateTimeAgent.TimeFormat(data.Time2);
                r["備考１"] = data.Cont1;
                r["備考２"] = data.Cont2;

                table.Rows.Add(r);
            }

            this.RsvDetailFormat();
        }

        void RsvDetailFormat()
        {
            DataView view = new DataView(dSet.Tables["Rsv"]);
            RsvView1.DataSource = view;

            RsvView1.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            RsvView1.Columns["ID"].Width = 50;

            RsvView1.Columns["氏名"].Width = 65;

            RsvView1.Columns["性別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            RsvView1.Columns["性別"].Width = 35;

            RsvView1.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            RsvView1.Columns["年齢"].Width = 35;

            RsvView1.Columns["開始"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            RsvView1.Columns["開始"].Width = 35;

            RsvView1.Columns["終了"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            RsvView1.Columns["終了"].Width = 35;

            RsvView1.Columns["備考１"].Width = 60;
            RsvView1.Columns["備考２"].Width = 60;

            foreach (DataGridViewRow r in RsvView1.Rows)
            {
                if (r.Cells["性別"].Value.ToString().Equals("女"))
                {
                    r.Cells["ID"].Style.ForeColor = Color.Red;
                    r.Cells["氏名"].Style.ForeColor = Color.Red;
                    r.Cells["性別"].Style.ForeColor = Color.Red;
                    r.Cells["年齢"].Style.ForeColor = Color.Red;
                }
            }
        }

        private void RsvBox1_MouseClick(object sender, MouseEventArgs e)
        {
            this.RsvDetailClear();

            bool rsv_exist = false;

            float x = e.X;
            float y = e.Y;

            foreach (WakuBox waku in rsv_list)
            {
                if (waku.IsOn(x, y))
                {
                    this.Waku1 = waku;
                    y = waku.Y2 + 1;
                    
                    rsv_exist = true;
                    break;
                }
            }

            if (!rsv_exist)
            {
                foreach (WakuBox waku in waku_list)
                {
                    if (waku.IsOn(x, y))
                    {
                        this.Waku1 = waku;
                        y = waku.Y2 + 1;

                        break;
                    }
                }
            }

            this.RsvDetailShow();


            // 連続予約を取れる枠数
            this.NextWakus = 0;

            // この枠で予約が取れる場合、次の枠が空いているか確認する

            rsv_exist = false;

            foreach (WakuBox waku in rsv_list)
            {
                if (waku.IsOn(x, y))
                {
                    rsv_exist = true;

                    if (waku.RsvDataList.Count < waku.RsvMax)
                    {
                        y = waku.Y2 + 1;
                        this.NextWakus++;
                    }

                    break;
                }
            }

            if (!rsv_exist)
            {
                foreach (WakuBox waku in waku_list)
                {
                    if (waku.IsOn(x, y))
                    {
                        y = waku.Y2 + 1;
                        this.NextWakus++;

                        break;
                    }
                }
            }


            // この枠で予約が取れなければ、ここで終了する
            if (this.NextWakus < 1)
            {
                return;
            }


            // この枠で予約が取れる場合、次の枠が空いているか確認する

            rsv_exist = false;

            foreach (WakuBox waku in rsv_list)
            {
                if (waku.IsOn(x, y))
                {
                    rsv_exist = true;

                    if (waku.RsvDataList.Count < waku.RsvMax)
                    {
                        this.NextWakus++;
                    }

                    break;
                }
            }

            if (!rsv_exist)
            {
                foreach (WakuBox waku in waku_list)
                {
                    if (waku.IsOn(x, y))
                    {
                        this.NextWakus++;
                        break;
                    }
                }
            }
        }

        private void RsvPanel1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                RsvTimeBox1.Location = new Point(3, 3 - e.NewValue);
            }
            else if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                RsvDateBox1.Location = new Point(3 - e.NewValue, 3);
            }
        }

        private void RsvView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string pt_id = RsvView1.CurrentRow.Cells["ID"].Value.ToString();

            FormControl.FormPat_Show(PatBase.Load(pt_id));
        }

        class WakuBox
        {
            public float X1 = 0;

            public float X2 = 0;

            public float Y1 = 0;

            public float Y2 = 0;

            public int RsvDate = 0;

            public int Time1 = 0;

            public int Time2 = 0;

            public int Interval
            {
                get
                {
                    int i = 0;

                    if (this.Time1 < this.Time2)
                    {
                        i = DateTimeAgent.IntervalMinutes(this.Time1, this.Time2);
                    }

                    return i;
                }
            }

            public string RsvDateTime
            {
                get
                {
                    string s = DateTimeAgent.DateFormat(this.RsvDate, DateTimeAgent.DateFormatKind.WLONG) +
                        " " + DateTimeAgent.TimeFormat(this.Time1) +
                        "～" + DateTimeAgent.TimeFormat(this.Time2);

                    return s;
                }
            }

            public int RsvMax = 0;

            public string Cont = "";

            public bool IsOn(float x, float y)
            {
                if (this.X1 <= x && this.X2 >= x &&
                    this.Y1 <= y && this.Y2 >= y)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public WakuBox Clone()
            {
                WakuBox obj = new WakuBox();

                obj.RsvDate = this.RsvDate;
                obj.Time1 = this.Time1;
                obj.Time2 = this.Time2;
                obj.Cont = this.Cont;
                obj.RsvMax = this.RsvMax;
                obj.X1 = this.X1;
                obj.X2 = this.X2;
                obj.Y1 = this.Y1;
                obj.Y2 = this.Y2;

                return obj;
            }

            public List<RsvData> RsvDataList = new List<RsvData>();
        }
/*
        class RsvBox : WakuBox
        {
            public List<RsvData> RsvDataList = new List<RsvData>();

            public RsvBox(WakuBox waku)
            {
                this.RsvDate = waku.RsvDate;
                this.Time1 = waku.Time1;
                this.Time2 = waku.Time2;
                this.Cont = waku.Cont;
                this.RsvMax = waku.RsvMax;
                this.X1 = waku.X1;
                this.X2 = waku.X2;
                this.Y1 = waku.Y1;
                this.Y2 = waku.Y2;
            }
        }
*/
        class CalSize
        {
            public enum CalMode : int
            {
                Normal = 1,
                Middle = 2,
                Heavy = 3
            }

            public CalMode Mode = CalMode.Normal;

            /// <summary>
            /// 項目名の高さ
            /// </summary>
            public int H1
            {
                get
                {
                    if (this.Mode == CalMode.Normal)
                    {
                        return 40;
                    }
                    else if (this.Mode == CalMode.Middle)
                    {
                        return 40;
                    }
                    else
                    {
                        return 40;
                    }
                }
            }

            /// <summary>
            /// 日付欄の高さ
            /// </summary>
            public int H_Date
            {
                get
                {
                    return 60;
                }
            }

            /// <summary>
            /// １時間あたりの高さ
            /// </summary>
            public int H_Hour
            {
                get
                {
                    return 60;
                }
            }

            /// <summary>
            /// 時間帯の幅
            /// </summary>
            public int W_Time
            {
                get
                {
                    return 50;
                }
            }

            /// <summary>
            /// 1日あたりの幅
            /// </summary>
            public int W_Day
            {
                get
                {
                    return 50;
                }
            }
        }

        private void RsvView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            // 予約種別がなければ削除できない
            if (this.Code1.Length == 0)
            {
                return;
            }

            if (MessageBox.Show("削除します。よろしいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                DataGridViewRow r = this.RsvView1.Rows[e.RowIndex];

                RsvData obj = new RsvData();

                obj.Code1 = this.Code1;
                obj.Code2 = this.Code2;
                obj.RsvDate = this.Waku1.RsvDate;

                // 開始時刻とIDは DataGridView から取得する
                int.TryParse(r.Cells["開始"].Value.ToString().Replace(":", ""), out obj.Time1);
                obj.Pat.Id = r.Cells["ID"].Value.ToString();

                obj.Delete();

                this.RsvShow();
            }
        }

        private void NewRsvButton1_Click(object sender, EventArgs e)
        {
            // 該当患者がなければ新規予約はできない
            if (this.Pat.Id.Length == 0)
            {
                return;
            }

            // 予約種別がなければ新規予約はできない
            if (this.Code1.Length == 0)
            {
                return;
            }

            FormRsvDetail f = new FormRsvDetail(this.Code1, this.Code2, this.Waku1.RsvDate, this.Waku1.Time1, this.Waku1.Interval, this.IsFull, this.NextWakus, this.Pat.Id, FormRsvDetail.Mode.New);
            f.ShowDialog();

            this.RsvShow();
        }
    }
}
