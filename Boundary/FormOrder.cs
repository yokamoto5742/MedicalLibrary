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
    public partial class FormOrder : StdForm1
    {
        bool _ReadOnly = true;

        public bool ReadOnly
        {
            get
            {
                return this._ReadOnly;
            }
            set
            {
                this._ReadOnly = value;

                if (value)
                {
                    this.Width = 640;
                    this.SaveButton1.Visible = false;

                    this.Panel1.Visible = false;
                    this.TabControl1.Location = new Point(5, 45);
                    this.TabControl1.Size = new Size(620, 668);

                    this.DoHistoryButton1.Visible = false;
                    this.DoCalButton1.Visible = false;
                }
                else
                {
                    this.Width = 1200;
                    this.SaveButton1.Visible = true;

                    this.Panel1.Visible = true;
                    this.TabControl1.Location = new Point(560, 42);
                    this.TabControl1.Size = new Size(620, 668);

                    this.DoHistoryButton1.Visible = true;
                    this.DoCalButton1.Visible = true;
                }

                this.MakeTabs(DeptBox1.Text, DoctorBox1.Text);
            }
        }

        DataSet dSet = new DataSet();

        enum CalMode : int
        {
            None = 0,
            On = 1
        }

        CalMode CalMode1 = CalMode.None;

        /// <summary>
        /// カレンダーに表示する日数
        /// </summary>
        const int CAL_DAYS = 90;

        string in_out = "1";

        public string InOut
        {
            get
            {
                return this.in_out;
            }
            set
            {
                this.in_out = value;

                if (this.in_out.Equals("1"))
                {
                    this.InOutLabel1.Text = "外来";
                    this.Panel1.BackColor = Color.LightGreen;
                }
                else if (this.in_out.Equals("2"))
                {
                    this.InOutLabel1.Text = "入院";
                    this.Panel1.BackColor = Color.LightPink;
                }
            }
        }

        public FormOrder(bool read_only = true)
        {
            InitializeComponent();

            OrderHistoryView1.Columns.Add("入外", "入外");
            OrderHistoryView1.Columns.Add("実施日", "実施日");
            OrderHistoryView1.Columns.Add("受付番号", "受付番号");
            OrderHistoryView1.Columns.Add("連番", "連番");
            OrderHistoryView1.Columns.Add("SDCD", "SDCD");
            OrderHistoryView1.Columns.Add("診", "診");
            OrderHistoryView1.Columns.Add("科コード", "科コード");
            OrderHistoryView1.Columns.Add("科", "科");
            OrderHistoryView1.Columns.Add("医師コード", "医師コード");
            OrderHistoryView1.Columns.Add("医師", "医師");
            OrderHistoryView1.Columns.Add("オーダーコード", "オーダーコード");
            OrderHistoryView1.Columns.Add("オーダー項目", "オーダー項目");
            OrderHistoryView1.Columns.Add("オーダー番号", "オーダー番号");
            OrderHistoryView1.Columns.Add("施行フラグ", "施行フラグ");
            OrderHistoryView1.Columns.Add("会計フラグ", "会計フラグ");
            OrderHistoryView1.Columns.Add("明細連番", "明細連番");
            OrderHistoryView1.Columns.Add("数量", "数量");
            OrderHistoryView1.Columns.Add("単位", "単位");
            OrderHistoryView1.Columns.Add("日/回数", "日/回数");

            OrderHistoryView1.Columns["入外"].Width = 25;
            OrderHistoryView1.Columns["入外"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            OrderHistoryView1.Columns["実施日"].Width = 70;
            OrderHistoryView1.Columns["実施日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            OrderHistoryView1.Columns["受付番号"].Visible = false;
            OrderHistoryView1.Columns["連番"].Visible = false;
            OrderHistoryView1.Columns["SDCD"].Visible = false;

            OrderHistoryView1.Columns["診"].Width = 35;
            OrderHistoryView1.Columns["診"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            OrderHistoryView1.Columns["科コード"].Visible = false;

            OrderHistoryView1.Columns["科"].Width = 25;
            OrderHistoryView1.Columns["科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            OrderHistoryView1.Columns["医師コード"].Visible = false;

            OrderHistoryView1.Columns["医師"].Width = 70;
            OrderHistoryView1.Columns["医師"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            OrderHistoryView1.Columns["オーダーコード"].Visible = false;

            OrderHistoryView1.Columns["オーダー項目"].Width = 225;
            OrderHistoryView1.Columns["オーダー項目"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            OrderHistoryView1.Columns["オーダー番号"].Visible = false;

            OrderHistoryView1.Columns["施行フラグ"].Visible = false;
            OrderHistoryView1.Columns["会計フラグ"].Visible = false;

            OrderHistoryView1.Columns["明細連番"].Visible = false;

            OrderHistoryView1.Columns["数量"].Width = 30;
            OrderHistoryView1.Columns["数量"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            OrderHistoryView1.Columns["単位"].Width = 30;
            OrderHistoryView1.Columns["単位"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            OrderHistoryView1.Columns["日/回数"].Width = 30;
            OrderHistoryView1.Columns["日/回数"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


            OrderCalView1.Columns.Add("入外", "入外");
            OrderCalView1.Columns.Add("実施日", "実施日");
            OrderCalView1.Columns.Add("受付番号", "受付番号");
            OrderCalView1.Columns.Add("連番", "連番");
            OrderCalView1.Columns.Add("SDCD", "SDCD");
            OrderCalView1.Columns.Add("診", "診");
            OrderCalView1.Columns.Add("科コード", "科コード");
            OrderCalView1.Columns.Add("科", "科");
            OrderCalView1.Columns.Add("医師コード", "医師コード");
            OrderCalView1.Columns.Add("医師", "医師");
            OrderCalView1.Columns.Add("オーダーコード", "オーダーコード");
            OrderCalView1.Columns.Add("オーダー項目", "オーダー項目");
            OrderCalView1.Columns.Add("オーダー番号", "オーダー番号");
            OrderCalView1.Columns.Add("施行フラグ", "施行フラグ");
            OrderCalView1.Columns.Add("会計フラグ", "会計フラグ");
            OrderCalView1.Columns.Add("明細連番", "明細連番");
            OrderCalView1.Columns.Add("数量", "数量");
            OrderCalView1.Columns.Add("単位", "単位");
            OrderCalView1.Columns.Add("日/回数", "日/回数");

            OrderCalView1.Columns["入外"].Width = 25;
            OrderCalView1.Columns["入外"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OrderCalView1.Columns["入外"].ReadOnly = true;

            OrderCalView1.Columns["実施日"].Width = 70;
            OrderCalView1.Columns["実施日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OrderCalView1.Columns["実施日"].ReadOnly = true;

            OrderCalView1.Columns["受付番号"].Visible = false;
            OrderCalView1.Columns["連番"].Visible = false;
            OrderCalView1.Columns["SDCD"].Visible = false;

            OrderCalView1.Columns["診"].Width = 35;
            OrderCalView1.Columns["診"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OrderCalView1.Columns["診"].ReadOnly = true;

            OrderCalView1.Columns["科コード"].Visible = false;

            OrderCalView1.Columns["科"].Width = 25;
            OrderCalView1.Columns["科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OrderCalView1.Columns["科"].ReadOnly = true;

            OrderCalView1.Columns["医師コード"].Visible = false;

            OrderCalView1.Columns["医師"].Width = 70;
            OrderCalView1.Columns["医師"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            OrderCalView1.Columns["医師"].ReadOnly = true;

            OrderCalView1.Columns["オーダーコード"].Visible = false;

            OrderCalView1.Columns["オーダー項目"].Width = 225;
            OrderCalView1.Columns["オーダー項目"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            OrderCalView1.Columns["オーダー項目"].ReadOnly = true;

            OrderCalView1.Columns["オーダー番号"].Visible = false;

            OrderCalView1.Columns["施行フラグ"].Visible = false;
            OrderCalView1.Columns["会計フラグ"].Visible = false;

            OrderCalView1.Columns["明細連番"].Visible = false;

            OrderCalView1.Columns["数量"].Width = 30;
            OrderCalView1.Columns["数量"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            OrderCalView1.Columns["数量"].ReadOnly = true;

            OrderCalView1.Columns["単位"].Width = 30;
            OrderCalView1.Columns["単位"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            OrderCalView1.Columns["単位"].ReadOnly = true;

            OrderCalView1.Columns["日/回数"].Width = 30;
            OrderCalView1.Columns["日/回数"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            OrderCalView1.Columns["日/回数"].ReadOnly = true;

            OrderCalView1.Columns["日/回数"].Frozen = true;
            OrderCalView1.Columns["日/回数"].DividerWidth = 1;

/*
            OrderGridView1.Columns.Add("Kind", "種別");
            OrderGridView1.Columns.Add("SekouDate", "実施日");
            OrderGridView1.Columns.Add("SEQ2", "明細連番");
            OrderGridView1.Columns.Add("SDCD", "SD");
            OrderGridView1.Columns.Add("KouiName", "診");
            OrderGridView1.Columns.Add("OrderCode", "オーダーコード");
            OrderGridView1.Columns.Add("OrderName", "オーダー項目");
            OrderGridView1.Columns.Add("Qty", "数量");
            OrderGridView1.Columns.Add("Unit", "単位");
            OrderGridView1.Columns.Add("Times", "日/回数");
            OrderGridView1.Columns.Add("RsvCode1", "予約種別");
            OrderGridView1.Columns.Add("RsvCode2", "予約詳細");

            OrderGridView1.Columns["Kind"].Width = 20;
            OrderGridView1.Columns["Kind"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            OrderGridView1.Columns["SekouDate"].Width = 80;
            OrderGridView1.Columns["SekouDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OrderGridView1.Columns["SekouDate"].ReadOnly = true;

            OrderGridView1.Columns["SEQ2"].Width = 20;
            OrderGridView1.Columns["SEQ2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            OrderGridView1.Columns["SDCD"].Width = 30;
            OrderGridView1.Columns["SDCD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            OrderGridView1.Columns["KouiName"].Width = 40;
            OrderGridView1.Columns["KouiName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            OrderGridView1.Columns["OrderCode"].Width = 70;
            OrderGridView1.Columns["OrderName"].Width = 140;

            OrderGridView1.Columns["Qty"].Width = 30;
            OrderGridView1.Columns["Qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            OrderGridView1.Columns["Unit"].Width = 30;
            OrderGridView1.Columns["Unit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            OrderGridView1.Columns["Times"].Width = 30;
            OrderGridView1.Columns["Times"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            OrderGridView1.Columns["RsvCode1"].Width = 30;
            OrderGridView1.Columns["RsvCode1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            OrderGridView1.Columns["RsvCode2"].Width = 30;
            OrderGridView1.Columns["RsvCode2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
*/
            this.OrderSheetView1.Init();

            DeptBox1.Text = "2";
            DoctorBox1.Text = "120";

            MakeTabs(DeptBox1.Text, DoctorBox1.Text);

            DatePicker1.Value = DateTime.Now.AddDays(-30);
            DatePicker1.MinDate = DateTime.Parse("2007/01/01");

            DatePicker2.Value = DateTime.Now.AddDays(-30);
            DatePicker2.MinDate = DateTime.Parse("2007/01/01");

            CalModeChange(this.CalMode1);

            this.CalMake();

            this.InOut = "1";

            this.ReadOnly = read_only;
        }

        void CalMake()
        {
            // カラム 0 ～ 18 を残して削除する
            for (int i = OrderCalView1.Columns.Count - 1; i >= 19; i--)
            {
                OrderCalView1.Columns.RemoveAt(i);
            }

            for (int i = 0; i < CAL_DAYS; i++)
            {
                DateTime dt = DatePicker1.Value.AddDays(i);

                OrderCalView1.Columns.Add(dt.ToString("yyyyMMdd"), dt.ToString("yyyyMMdd"));

                OrderCalView1.Columns[dt.ToString("yyyyMMdd")].HeaderText = dt.ToString("M/d ddd");
                OrderCalView1.Columns[dt.ToString("yyyyMMdd")].Width = 35;
                OrderCalView1.Columns[dt.ToString("yyyyMMdd")].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                OrderCalView1.Columns[dt.ToString("yyyyMMdd")].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                if (dt.DayOfWeek == DayOfWeek.Saturday)
                {
                    OrderCalView1.Columns[dt.ToString("yyyyMMdd")].HeaderCell.Style.ForeColor = Color.Blue;
                }
                else if (dt.DayOfWeek == DayOfWeek.Sunday)
                {
                    OrderCalView1.Columns[dt.ToString("yyyyMMdd")].HeaderCell.Style.ForeColor = Color.Red;
                }
            }

            // 最初のスクロール位置
            OrderCalView1.FirstDisplayedScrollingColumnIndex = 19;
        }

        public override void FontSet(AppFont f)
        {
            base.FontSet(f);
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            this.PatShow();
        }

        void MakeTabs(string dept_code, string doctor_code)
        {
            foreach (TabPage pg in TabControl1.TabPages)
            {
                // 履歴とカレンダー以外は消去する

                if (pg.Text.Equals("履歴") || pg.Text.Equals("カレンダー"))
                {
                    continue;
                }

                pg.Dispose();
            }

            // ReadOnly の場合はタブを作らず終了
            if (this.ReadOnly)
            {
                return;
            }

            Cust1 cust1 = Cust1.Get(dept_code, doctor_code);

            Dictionary<Tuple<string, int, int>, Cust2> dict2 = Cust2.GetDict(DeptBox1.Text, DoctorBox1.Text);
            Dictionary<Tuple<string, int, int>, List<Cust3>> dict3 = Cust3.GetDict(DeptBox1.Text, DoctorBox1.Text);

            for (int i = 1; i <= 10; i++)
            {
                if (i == 1)
                {
                    // i = 1 は「履歴」なので飛ばす
                    continue;
                }

                if (i == 7)
                {
                    // i = 7 は「カレンダー」なので飛ばす
                    continue;
                }

                TabPage page = new TabPage();

                page.Name = "TabNames" + i;
                page.Text = cust1.TabNames[i];
                page.AutoScroll = true;

                // i = 1 の場合、履歴を表示　→　最初から固定なので飛ばす
                if (i == 1)
                {
                    DataGridView view1 = new DataGridView();
                    view1.AllowUserToAddRows = false;
                    view1.AllowUserToDeleteRows = false;
                    view1.ReadOnly = true;

                    view1.Size = new Size(TabControl1.Width - 60, TabControl1.Height - 60);
                    view1.Location = new Point(0, 0);

                    DataTable table = dSet.Tables["OrderHistory"];
                    view1.DataSource = new DataView(table);

                    page.Controls.Add(view1);
                    view1.Anchor = AnchorStyles.Bottom | AnchorStyles.Top;
                }


                // i = 6 の場合、伝票を追加
                if (i == 6)
                {
                    Button bt = new Button();
                    bt.Size = new Size(80, 20);
                    bt.Location = new Point(10, 5);
                    bt.Text = "確認";
                    bt.Click += new EventHandler(VoucherButton_Click);

                    page.Controls.Add(bt);


                    TabControl tc = new TabControl();

                    tc.Name = "VoucherTc";
                    tc.Multiline = true;
                    tc.Size = new Size(TabControl1.Width - 60, TabControl1.Height - 50);
                    tc.Location = new Point(0, 30);

                    List<Voucher> list = Voucher.GetList(DeptBox1.Text);

                    foreach (Voucher v in list)
                    {
                        // デバッグのため D02001 外科伝票, DX002 放射線科伝票 のみ

                        if (!v.Code.Equals("D02001") && !v.Code.Equals("DX002"))
                        {
//                            continue;
                        }

                        TabPage tp = new TabPage();
                        tp.Name = v.Code;
                        tp.Text = v.Name;
                        tp.Padding = new Padding(10, 10, 10, 10);
                        tp.AutoScroll = true;

                        List<VoucherComp> vlist = VoucherComp.GetList(v.Code);

                        tp.Tag = vlist;

                        tc.TabPages.Add(tp);

                        CtrlVoucherPanel1 vp1 = new CtrlVoucherPanel1(v.Code);
                        vp1.Location = new Point(10, 10);
                        vp1.MakePanel();

                        tp.Controls.Add(vp1);
                    }

                    page.Controls.Add(tc);
                    tc.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                }

                // i = 3（処方用法）の場合、用法（Cust2）ボタンを追加
                // i = 4（注射用法）の場合、用法（Cust2）ボタンを追加
                // i = 8,9,10（カスタマイズ）の場合、処方（Cust3）・用法（Cust2）ボタンを追加

                if (i == 3 || i == 4 || i >= 8)
                {
                    // 用法（Cust2）ボタンを追加
                    Panel panel2 = new Panel();

                    panel2.AutoScroll = true;
                    panel2.BorderStyle = BorderStyle.Fixed3D;

                    if (i == 3)
                    {
                        panel2.Size = new Size(420, 410);
                        panel2.Location = new Point(20, 310);

                        // i = 3 の場合、処方用法パネルを追加
                        CtrlDrugYoho1 p = new CtrlDrugYoho1();
                        p.Size = new Size(420, 300);
                        p.Location = new Point(20, 5);
                        p.Init(this.OrderSheetView1);

                        page.Controls.Add(p);
                    }
                    else if (i == 4)
                    {
                        panel2.Size = new Size(420, 410);
                        panel2.Location = new Point(20, 310);

                        // i = 4 の場合、注射用法パネルを追加
                        CtrlInjectionYoho1 p = new CtrlInjectionYoho1();
                        p.Size = new Size(420, 300);
                        p.Location = new Point(20, 5);
                        p.Init(this.OrderSheetView1);

                        page.Controls.Add(p);
                    }
                    else if (i >= 8)
                    {
                        panel2.Size = new Size(420, 270);
                        panel2.Location = new Point(20, 450);
                    }

                    foreach (Tuple<string, int, int> key in dict2.Keys)
                    {
                        if (i == 3)
                        {
                            // 処方用法でない場合は飛ばす
                            if (key.Item2 != 1)
                            {
                                continue;
                            }
                        }
                        else if (i == 4)
                        {
                            // 注射用法でない場合は飛ばす
                            if (key.Item2 != 3)
                            {
                                continue;
                            }
                        }
                        else if (i >= 8)
                        {
                            // ページが異なる場合は飛ばす
                            if (key.Item2 != i - 3)
                            {
                                continue;
                            }
                        }

                        // ＤＲコード = 0 の場合、辞書にＤＲ固有のデータが存在すれば飛ばす
                        if (key.Item1.Equals("0"))
                        {
                            Tuple<string, int, int> k2 = new Tuple<string, int, int>(DoctorBox1.Text, key.Item2, key.Item3);

                            if (dict2.ContainsKey(k2))
                            {
                                continue;
                            }
                        }

                        Cust2 cust2 = dict2[key];

                        Button button = new Button();
                        button.Tag = cust2;
                        button.BackColor = Color.LightGoldenrodYellow;

                        button.Text = cust2.Name;

                        if (i == 3)
                        {
                            // i = 3（処方用法）の場合、用法（Cust2）ボタンは 10 * 3 = 30個
                            button.Size = new Size(100, 50);
                            button.Location = new Point(((key.Item3 - 100) % 3) * 100, ((key.Item3 - 100) / 3) * 50);
                        }
                        else if (i == 4)
                        {
                            // i = 4（注射用法）の場合、用法（Cust2）ボタンは 8 * 4 = 32個
                            button.Size = new Size(100, 50);
                            button.Location = new Point(((key.Item3 - 100) % 4) * 100, ((key.Item3 - 100) / 4) * 50);
                        }
                        else if (i >= 8)
                        {
                            // i = 8,9,10（カスタマイズ）の場合、用法（Cust2）ボタンは 5 * 4 = 20個
                            // 連番は、左上から 0、右に向かって増えていく。
                            button.Size = new Size(100, 50);
                            button.Location = new Point((key.Item3 % 4) * 100, (key.Item3 / 4) * 50);
                        }

                        button.Click += new EventHandler(Cust2Button_Click);

                        panel2.Controls.Add(button);
                    }

                    page.Controls.Add(panel2);


                    // i = 8,9,10（カスタマイズ）の場合、処方（Cust3）ボタンを追加
                    if (i >= 8)
                    {
                        Panel panel3 = new Panel();

                        panel3.AutoScroll = true;
                        panel3.BorderStyle = BorderStyle.Fixed3D;

                        if (i == 8 || i == 9)
                        {
                            // i = 8,9 の場合、処方ボタンは 4 * 4 = 16個
                            panel3.Size = new Size(420, 420);
                            panel3.Location = new Point(20, 20);
                        }
                        else if (i == 10)
                        {
                            // i = 8,9 の場合、処方ボタンは 8 * 4 = 32個
                            panel3.Size = new Size(420, 420);
                            panel3.Location = new Point(20, 20);
                        }

                        foreach (Tuple<string, int, int> key in dict3.Keys)
                        {
                            // ページが異なる場合は飛ばす
                            if (key.Item2 != i - 3)
                            {
                                continue;
                            }

                            // ＤＲコード = 0 の場合、辞書にＤＲ固有のデータが存在すれば飛ばす
                            if (key.Item1.Equals("0"))
                            {
                                Tuple<string, int, int> k2 = new Tuple<string, int, int>(DoctorBox1.Text, key.Item2, key.Item3);

                                if (dict3.ContainsKey(k2))
                                {
                                    continue;
                                }
                            }

                            List<Cust3> list = dict3[key];

                            Button button = new Button();
                            button.Tag = list;

                            // ボタンの作成
                            foreach (Cust3 cust3 in list)
                            {
                                // 明細連番 = 0 の場合はボタン名
                                if (cust3.SEQ2 == 0)
                                {
                                    button.Text = cust3.Name;

                                    if (i == 8 || i == 9)
                                    {
                                        // i = 8,9 の場合、処方ボタンは 4 * 4 = 16個
                                        button.Size = new Size(100, 100);
                                        button.Location = new Point((key.Item3 / 4) * 100, (key.Item3 % 4) * 100);
                                    }
                                    else if (i == 10)
                                    {
                                        // i = 10 の場合、処方ボタンは 8 * 4 = 32個
                                        button.Size = new Size(100, 50);
                                        button.Location = new Point((key.Item3 / 8) * 100, (key.Item3 % 8) * 50);
                                    }

                                    break;
                                }
                            }

                            button.Click += new EventHandler(Cust3Button_Click);

                            panel3.Controls.Add(button);
                        }

                        page.Controls.Add(panel3);
                    }
                }

                TabControl1.TabPages.Add(page);
            }

            for (int i = 1; i <= 5; i++)
            {
                TabPage page = new TabPage();

                page.Name = "FieldNames" + i;
                page.Text = cust1.FieldNames[i];
                page.AutoScroll = true;


                // 用法（Cust2）ボタンを追加
                Panel panel2 = new Panel();

                panel2.AutoScroll = true;
                panel2.BorderStyle = BorderStyle.Fixed3D;

                panel2.Size = new Size(420, 270);
                panel2.Location = new Point(20, 450);

                foreach (Tuple<string, int, int> key in dict2.Keys)
                {
                    // ページが異なる場合は飛ばす
                    if (key.Item2 != i + 7)
                    {
                        continue;
                    }

                    // ＤＲコード = 0 の場合、辞書にＤＲ固有のデータが存在すれば飛ばす
                    if (key.Item1.Equals("0"))
                    {
                        Tuple<string, int, int> k2 = new Tuple<string, int, int>(DoctorBox1.Text, key.Item2, key.Item3);

                        if (dict2.ContainsKey(k2))
                        {
                            continue;
                        }
                    }

                    Cust2 cust2 = dict2[key];

                    Button button = new Button();
                    button.Tag = cust2;
                    button.BackColor = Color.LightGoldenrodYellow;

                    button.Text = cust2.Name;

                    // 用法（Cust2）ボタンは 5 * 4 = 20個
                    // 連番は、左上から 0、右に向かって増えていく。
                    button.Size = new Size(100, 50);
                    button.Location = new Point((key.Item3 % 4) * 100, (key.Item3 / 4) * 50);

                    button.Click += new EventHandler(Cust2Button_Click);

                    panel2.Controls.Add(button);
                }

                page.Controls.Add(panel2);


                // 処方（Cust3）ボタンを追加
                Panel panel3 = new Panel();

                panel3.AutoScroll = true;
                panel3.BorderStyle = BorderStyle.Fixed3D;

                panel3.Size = new Size(420, 420);
                panel3.Location = new Point(20, 20);

                foreach (Tuple<string, int, int> key in dict3.Keys)
                {
                    // ページが異なる場合は飛ばす
                    if (key.Item2 != i + 7)
                    {
                        continue;
                    }

                    // ＤＲコード = 0 の場合、辞書にＤＲ固有のデータが存在すれば飛ばす
                    if (key.Item1.Equals("0"))
                    {
                        Tuple<string, int, int> k2 = new Tuple<string, int, int>(DoctorBox1.Text, key.Item2, key.Item3);

                        if (dict3.ContainsKey(k2))
                        {
                            continue;
                        }
                    }

                    List<Cust3> list = dict3[key];

                    Button button = new Button();
                    button.Tag = list;

                    // ボタンの作成
                    foreach (Cust3 cust3 in list)
                    {
                        // 明細連番 = 0 の場合はボタン名
                        if (cust3.SEQ2 == 0)
                        {
                            button.Text = cust3.Name;

                            button.Size = new Size(100, 50);
                            button.Location = new Point((key.Item3 / 8) * 100, (key.Item3 % 8) * 50);

                            break;
                        }
                    }

                    button.Click += new EventHandler(Cust3Button_Click);

                    panel3.Controls.Add(button);
                }

                page.Controls.Add(panel3);

                TabControl1.TabPages.Add(page);
            }
        }

        void VoucherButton_Click(object sender, EventArgs e)
        {
            /*
            int i = OrderGridView1.Rows.Count - 1;
            */

            /*
            if (OrderGridView1.CurrentRow != null)
            {
                i = OrderGridView1.CurrentRow.Index;
            }
            else
            {
                i = OrderGridView1.Rows.Count - 1;
            }
             */

            TabControl tc = (TabControl)(TabControl1.TabPages["TabNames6"].Controls["VoucherTc"]);

            TabPage page = tc.SelectedTab;


            List<OrderHeader> order_list = new List<OrderHeader>();

            foreach (Control c in page.Controls)
            {
                if (c is CtrlVoucherPanel1)
                {
                    CtrlVoucherPanel1 vp = (CtrlVoucherPanel1)c;
                    order_list = vp.GetOrderHeaderList();
                    break;
                }
            }


            foreach (OrderHeader order in order_list)
            {
                this.OrderPanel1.Add(new CtrlOrder1(order));

                this.OrderSheetView1.OrderHeaderInsert(order);
/*
                int j = 0;

                foreach (OrderDetail detail in order.DetailList)
                {
                    OrderGridView1.Rows.Insert(i, 1);

                    DataGridViewRow r = this.OrderGridView1.Rows[i];

                    if (detail.Kind.Equals(1))
                    {
                        r.Cells["SekouDate"].Value = detail.SekouDate;
                        r.Cells["KouiName"].Value = detail.KouiName;
                    }

                    r.Cells["Kind"].Value = detail.Kind;
                    r.Cells["SEQ2"].Value = detail.SEQ2;
                    r.Cells["SDCD"].Value = detail.SDCD;
                    r.Cells["OrderCode"].Value = detail.OrderCode;
                    r.Cells["OrderName"].Value = detail.OrderName;
                    r.Cells["Qty"].Value = detail.Qty;
                    r.Cells["Unit"].Value = detail.Unit;

                    if (detail.Times > 0)
                    {
                        // 回数が入っている場合は、必ず区切り線を入れる。
                        r.Cells["Times"].Value = detail.Times;
                        r.DividerHeight = 1;
                    }
                    else if (j >= order.DetailList.Count - 1)
                    {
                        // 回数が入っていない場合は、DetailList の最後の行に区切り線を入れる。
                        r.Cells["Times"].Value = "1";
                        r.DividerHeight = 1;
                    }

                    r.Cells["RsvCode1"].Value = detail.EtcFlgs[7];
                    r.Cells["RsvCode2"].Value = detail.EtcFlgs[17];

                    i++;
                    j++;
                }
 */
            }
        }

        /// <summary>
        /// 用法ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Cust2Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Cust2 cust2 = (Cust2)(button.Tag);

            List<string> msgs = new List<string>();
/*
            int i = OrderGridView1.Rows.Count - 1;

            if (i <= 0 || OrderGridView1.Rows[i - 1].DividerHeight >= 1)
            {
                msgs.Add("OrderGridView1 : 用法単独では指定できません");
            }
*/

            int i = OrderSheetView1.Rows.Count - 1;

            if (i <= 0 || OrderSheetView1.Rows[i - 1].DividerHeight >= 1)
            {
                msgs.Add("OrderSheetView1 : 用法単独では指定できません");
            }


            // いま入力しようとしている用法が、内服・頓用・外用の Rp に合致していること

            /*
            // いま入力しようとしている Rp の診療区分を調べる
            string koui_code = "";

            for (int j = i - 1; j >= 0; j--)
            {
                if (j == 0 || OrderGridView1.Rows[j - 1].DividerHeight >= 1)
                {
                    koui_code = OrderGridView1.Rows[j].Cells["SDCD"].Value.ToString();
                    break;
                }
            }

            MessageBox.Show("KouiCode = " + koui_code);
            
             
            if (OrderGridView1.CurrentRow != null)
            {
                i = OrderGridView1.CurrentRow.Index;
            }
            else
            {
                i = OrderGridView1.Rows.Count - 1;
            }
             */


            if (msgs.Count > 0)
            {
                MessageBox.Show(AppString.ConcatList(msgs, Environment.NewLine));
                return;
            }
/*
            OrderGridView1.Rows.Insert(i, 1);

            DataGridViewRow r = OrderGridView1.Rows[i];

            r.Cells["Kind"].Value = cust2.Kind;
            r.Cells["OrderCode"].Value = cust2.Code;
            r.Cells["OrderName"].Value = cust2.Name;
            r.Cells["Times"].Value = cust2.Times;

            if (cust2.Times > 0)
            {
                r.DividerHeight = 1;
            }

            OrderGridView1.CurrentCell = OrderGridView1.Rows[i + 1].Cells[0];
*/

            List<OrderDetail> detail_list = new List<OrderDetail>();
            OrderDetail detail = new OrderDetail();

            detail.Kind = cust2.Kind;
            detail.OrderCode = cust2.Code;
            detail.OrderName = cust2.Name;
            detail.Times = cust2.Times;

            detail_list.Add(detail);

            this.OrderSheetView1.OrderDetailInsert(detail_list, false, false);
        }

        /// <summary>
        /// 処方ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Cust3Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            List<Cust3> list = (List<Cust3>)(button.Tag);

            /*
            int i = OrderGridView1.Rows.Count - 1;
            */

            /*
            int j = 0;

            if (OrderGridView1.CurrentRow != null)
            {
                i = OrderGridView1.CurrentRow.Index;
            }
            else
            {
                i = OrderGridView1.Rows.Count - 1;
            }

            // 新規行または区切りの先頭行でない場合はエラーを出す
            DataGridViewRow r = OrderGridView1.Rows[i];

            if (!r.IsNewRow && r.Cells["Kind"].Value != null && !r.Cells["Kind"].Value.ToString().Equals("1"))
            {
                MessageBox.Show("ここにオーダーを入れることは出来ません");
                return;
            }

            OrderGridView1.Rows.Insert(i, list.Count - 1);

            foreach (Cust3 cust3 in list)
            {
                if (cust3.SEQ2 == 0)
                {
                    continue;
                }

                r = OrderGridView1.Rows[i + j];

                r.Cells["Kind"].Value = cust3.Kind;
                r.Cells["SEQ2"].Value = cust3.SEQ2;
                r.Cells["SDCD"].Value = cust3.SDCD;
                r.Cells["KouiName"].Value = cust3.KouiName;
                r.Cells["OrderCode"].Value = cust3.Code;
                r.Cells["OrderName"].Value = cust3.Name;
                r.Cells["Qty"].Value = cust3.Qty;
                r.Cells["Times"].Value = cust3.Times;

                if (cust3.Kind == 1)
                {
                    r.Cells["SekouDate"].Value = DateTime.Now.ToString("yyyy/MM/dd");
                }

                if (cust3.Times > 0)
                {
                    r.DividerHeight = 1;
                }

                j++;
            }

            OrderGridView1.CurrentCell = OrderGridView1.Rows[i + list.Count - 1].Cells[0];
             */

            List<OrderHeader> order_list = new List<OrderHeader>();
            List<OrderDetail> detail_list = new List<OrderDetail>();

            foreach (Cust3 cust3 in list)
            {
                if (cust3.SEQ2 == 0)
                {
                    continue;
                }

                OrderDetail tmp = new OrderDetail();
                tmp.Kind = cust3.Kind;
                tmp.SEQ2 = cust3.SEQ2;
                tmp.SDCD = cust3.SDCD;
                tmp.KouiName = cust3.KouiName;
                tmp.OrderCode = cust3.Code;
                tmp.OrderName = cust3.Name;
                tmp.Qty = cust3.Qty;
                tmp.Times = cust3.Times;

                detail_list.Add(tmp);

                if (tmp.Times > 0)
                {
                    OrderHeader tmp_order = new OrderHeader();
                    tmp_order.SekouDate = DateTime.Now.ToString("yyyyMMdd");
                    tmp_order.KouiCode = detail_list[0].SDCD;
                    tmp_order.Times = detail_list[detail_list.Count - 1].Times;

                    foreach (OrderDetail tmp2 in detail_list)
                    {
                        tmp_order.DetailList.Add(tmp2);
                    }

                    order_list.Add(tmp_order);

                    detail_list.Clear();
                }
            }

            if (detail_list.Count > 0)
            {
                OrderHeader tmp_order = new OrderHeader();
                tmp_order.SekouDate = DateTime.Now.ToString("yyyyMMdd");
                tmp_order.KouiCode = detail_list[0].SDCD;
                tmp_order.Times = detail_list[detail_list.Count - 1].Times;

                foreach (OrderDetail tmp2 in detail_list)
                {
                    tmp_order.DetailList.Add(tmp2);
                }

                order_list.Add(tmp_order);

                detail_list.Clear();
            }

            foreach (OrderHeader order in order_list)
            {
                this.OrderPanel1.Add(new CtrlOrder1(order));

                this.OrderSheetView1.OrderHeaderInsert(order);

                /*
                int j = 0;

                foreach (OrderDetail detail in order.DetailList)
                {
                    OrderGridView1.Rows.Insert(i, 1);

                    DataGridViewRow r = this.OrderGridView1.Rows[i];

                    if (detail.Kind.Equals(1))
                    {
                        r.Cells["SekouDate"].Value = detail.SekouDate;
                        r.Cells["KouiName"].Value = detail.KouiName;
                    }

                    r.Cells["Kind"].Value = detail.Kind;
                    r.Cells["SEQ2"].Value = detail.SEQ2;
                    r.Cells["SDCD"].Value = detail.SDCD;
                    r.Cells["OrderCode"].Value = detail.OrderCode;
                    r.Cells["OrderName"].Value = detail.OrderName;
                    r.Cells["Qty"].Value = detail.Qty;
                    r.Cells["Unit"].Value = detail.Unit;

                    if (detail.Times > 0)
                    {
                        // 回数が入っている場合は、必ず区切り線を入れる。
                        r.Cells["Times"].Value = detail.Times;
                        r.DividerHeight = 1;
                    }
                    else if (j >= order.DetailList.Count - 1)
                    {
                        // 回数が入っていない場合は、DetailList の最後の行に区切り線を入れる。
                        // →　やはり入れなくてよい
//                        r.Cells["Times"].Value = "1";
//                        r.DividerHeight = 1;
                    }

                    r.Cells["RsvCode1"].Value = detail.EtcFlgs[7];
                    r.Cells["RsvCode2"].Value = detail.EtcFlgs[17];

                    i++;
                    j++;
                }
                 */
            }
        }

        /*
        void OrderDetailInsert(List<OrderDetail> detail_list, DataGridView view, int i)
        {
            int j = 0;

            foreach (OrderDetail detail in detail_list)
            {
                view.Rows.Insert(i, 1);

                DataGridViewRow r = view.Rows[i];

                // オーダーコードが存在し、かつ 8888888% ではなく
                // SDCD・診療区分・名称・単位が無い場合はオーダーマスターから取得
                if (detail.OrderCode.Length > 0 && !detail.OrderCode.StartsWith("8888888"))
                {
                    detail.GetFromOrderMaster();
                }

                if (detail.Kind.Equals(1))
                {
                    r.Cells["SekouDate"].Value = detail.SekouDate;
                    r.Cells["KouiName"].Value = detail.KouiName;
                }

                r.Cells["Kind"].Value = detail.Kind;
                r.Cells["SEQ2"].Value = detail.SEQ2;
                r.Cells["SDCD"].Value = detail.SDCD;
                r.Cells["OrderCode"].Value = detail.OrderCode;
                r.Cells["OrderName"].Value = detail.OrderName;
                r.Cells["Qty"].Value = detail.Qty;
                r.Cells["Unit"].Value = detail.Unit;

                if (detail.Times > 0)
                {
                    r.Cells["Times"].Value = detail.Times;
                    r.DividerHeight = 1;
                }
                else if (j >= detail_list.Count - 1)
                {
                    // 回数が入っていない場合は、DetailList の最後の行に区切り線を入れる。
                    // →　やはり入れなくてよい
//                        r.Cells["Times"].Value = "1";
//                        r.DividerHeight = 1;
                }

                r.Cells["RsvCode1"].Value = detail.EtcFlgs[7];
                r.Cells["RsvCode2"].Value = detail.EtcFlgs[17];

                i++;
                j++;
            }
        }
        */

        private void ShowButton_Click(object sender, EventArgs e)
        {
            MakeTabs(DeptBox1.Text, DoctorBox1.Text);
        }

        void PatShow()
        {
            // 保険をクリア
            this.InsBox1.Clear();
            this.InsPerLabel1.Text = "";
            this.InsPerLabel2.Text = "";

            // OrderGridView をクリア
            /*
            this.OrderGridView1.Rows.Clear();
            */

            // OrderPanel をクリア
            this.OrderPanel1.Controls.Clear();


            List<string> empty_list = new List<string>();

            // 開始日がカレンダーの表示日数以上前の場合は、最終日を「日付未定」ではなく「開始日から表示日数の間」となるようにオーダーを絞る

            string date2 = "99999999";

            if (DateTime.Now.Subtract(DatePicker1.Value).Days >= CAL_DAYS)
            {
                date2 = DatePicker1.Value.AddDays(CAL_DAYS - 1).ToString("yyyyMMdd");
            }

            List<PatOrder> list = PatOrder.GetListByPatDates(this.Pat.Id, DatePicker1.Value.ToString("yyyyMMdd"), date2, "", empty_list, empty_list, true);

            OrderHistoryView1.Rows.Clear();
            OrderCalView1.Rows.Clear();

            // カレンダーの日付欄を再作成
            this.CalMake();

            int j = 0;
            string uke_id = "";
            bool uke_first = true;

            foreach (PatOrder obj in list)
            {
                if (j > 0 && !obj.UkeId.Equals(uke_id))
                {
                    OrderHistoryView1.Rows[j - 1].DividerHeight = 1;
                    uke_first = true;
                }

                uke_id = obj.UkeId;

                foreach (PatOrderDetail obj2 in obj.DetailList)
                {
                    OrderHistoryView1.Rows.Insert(j, 1);
                    DataGridViewRow r = OrderHistoryView1.Rows[j];

                    if (uke_first)
                    {
                        r.Cells["入外"].Value = obj.InOutNameShort;
                        r.Cells["実施日"].Value = obj.SekouDateString;
                        r.Cells["受付番号"].Value = obj.UkeId;
                        r.Cells["連番"].Value = obj.UkeSEQ;
                        r.Cells["SDCD"].Value = obj.Shinku;
                        r.Cells["診"].Value = obj.ShinkuString;
                        r.Cells["科コード"].Value = obj.Dept;
                        r.Cells["科"].Value = obj.DeptName.Substring(0, 1);
                        r.Cells["医師コード"].Value = obj.Doctor;
                        r.Cells["医師"].Value = obj.DoctorName;

                        if (obj.InOut.Equals("1"))
                        {
                            r.Cells["入外"].Style.BackColor = Color.LightGreen;
                            r.Cells["実施日"].Style.BackColor = Color.LightGreen;
                        }
                        else if (obj.InOut.Equals("2"))
                        {
                            r.Cells["入外"].Style.BackColor = Color.Yellow;
                            r.Cells["実施日"].Style.BackColor = Color.Yellow;
                        }
                    }
                    else
                    {
                        r.Cells["入外"].Value = "";
                        r.Cells["実施日"].Value = "";
                        r.Cells["受付番号"].Value = "";
                        r.Cells["連番"].Value = "";
                        r.Cells["SDCD"].Value = "";
                        r.Cells["診"].Value = "";
                        r.Cells["科コード"].Value = "";
                        r.Cells["科"].Value = "";
                        r.Cells["医師コード"].Value = "";
                        r.Cells["医師"].Value = "";
                    }

                    r.Cells["オーダーコード"].Value = obj2.Code;
                    r.Cells["オーダー項目"].Value = obj2.Name;
                    r.Cells["オーダー番号"].Value = obj2.OrderId;
                    r.Cells["施行フラグ"].Value = obj.SekouFlg;
                    r.Cells["会計フラグ"].Value = obj.KaikeiFlg;
                    r.Cells["明細連番"].Value = obj2.DetailId;
                    r.Cells["数量"].Value = obj2.QtyString;
                    r.Cells["単位"].Value = obj2.Unit;

                    if (obj2.Times > 0)
                    {
                        r.Cells["日/回数"].Value = obj2.Times;
                    }
                    else
                    {
                        r.Cells["日/回数"].Value = "";
                    }

                    if (obj.KaikeiFlg.Equals("1"))
                    {
                        r.Cells["オーダーコード"].Style.BackColor = Color.Orange;
                        r.Cells["オーダー項目"].Style.BackColor = Color.Orange;
                        r.Cells["オーダー番号"].Style.BackColor = Color.Orange;
                        r.Cells["明細連番"].Style.BackColor = Color.Orange;
                        r.Cells["数量"].Style.BackColor = Color.Orange;
                        r.Cells["単位"].Style.BackColor = Color.Orange;
                        r.Cells["日/回数"].Style.BackColor = Color.Orange;
                    }
                    else if (obj.SekouFlg.Equals("1"))
                    {
                        r.Cells["オーダーコード"].Style.BackColor = Color.Yellow;
                        r.Cells["オーダー項目"].Style.BackColor = Color.Yellow;
                        r.Cells["オーダー番号"].Style.BackColor = Color.Yellow;
                        r.Cells["明細連番"].Style.BackColor = Color.Yellow;
                        r.Cells["数量"].Style.BackColor = Color.Yellow;
                        r.Cells["単位"].Style.BackColor = Color.Yellow;
                        r.Cells["日/回数"].Style.BackColor = Color.Yellow;
                    }


                    uke_first = false;

                    j++;
                }
            }

            OrderHistoryView1.ClearSelection();

            // カレンダー
            j = 0;
            uke_id = "";
            uke_first = true;

            foreach (PatOrder obj in list)
            {
                if (j > 0 && !obj.UkeId.Equals(uke_id))
                {
                    OrderCalView1.Rows[j - 1].DividerHeight = 1;
                    uke_first = true;
                }

                uke_id = obj.UkeId;

                foreach (PatOrderDetail obj2 in obj.DetailList)
                {
                    OrderCalView1.Rows.Insert(j, 1);
                    DataGridViewRow r = OrderCalView1.Rows[j];

                    if (uke_first)
                    {
                        r.Cells["入外"].Value = obj.InOutNameShort;
                        r.Cells["実施日"].Value = obj.SekouDateString;
                        r.Cells["受付番号"].Value = obj.UkeId;
                        r.Cells["連番"].Value = obj.UkeSEQ;
                        r.Cells["SDCD"].Value = obj.Shinku;
                        r.Cells["診"].Value = obj.ShinkuString;
                        r.Cells["科コード"].Value = obj.Dept;
                        r.Cells["科"].Value = obj.DeptName.Substring(0, 1);
                        r.Cells["医師コード"].Value = obj.Doctor;
                        r.Cells["医師"].Value = obj.DoctorName;

                        if (obj.InOut.Equals("1"))
                        {
                            r.Cells["入外"].Style.BackColor = Color.LightGreen;
                            r.Cells["実施日"].Style.BackColor = Color.LightGreen;
                        }
                        else if (obj.InOut.Equals("2"))
                        {
                            r.Cells["入外"].Style.BackColor = Color.Yellow;
                            r.Cells["実施日"].Style.BackColor = Color.Yellow;
                        }

                        if (obj.InnaiFlg.Equals("0"))
                        {
                            r.Cells["診"].Style.ForeColor = Color.Blue;
                        }
                        else
                        {
                            r.Cells["診"].Style.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        r.Cells["入外"].Value = "";
                        r.Cells["実施日"].Value = "";
                        r.Cells["受付番号"].Value = "";
                        r.Cells["連番"].Value = "";
                        r.Cells["SDCD"].Value = "";
                        r.Cells["診"].Value = "";
                        r.Cells["科コード"].Value = "";
                        r.Cells["科"].Value = "";
                        r.Cells["医師コード"].Value = "";
                        r.Cells["医師"].Value = "";
                    }

                    r.Cells["オーダーコード"].Value = obj2.Code;
                    r.Cells["オーダー項目"].Value = obj2.Name;
                    r.Cells["オーダー番号"].Value = obj2.OrderId;
                    r.Cells["施行フラグ"].Value = obj.SekouFlg;
                    r.Cells["会計フラグ"].Value = obj.KaikeiFlg;
                    r.Cells["明細連番"].Value = obj2.DetailId;
                    r.Cells["数量"].Value = obj2.QtyString;
                    r.Cells["単位"].Value = obj2.Unit;

                    if (obj2.Times > 0)
                    {
                        r.Cells["日/回数"].Value = obj2.Times;

                        // 開始日と終了日の間かどうかをチェック
                        int d1 = int.Parse(obj.StartDate);
                        int d2 = int.Parse(obj.EndDate);

                        for (int i = 0; i < CAL_DAYS; i++)
                        {
                            DateTime dt = DatePicker1.Value.AddDays(i);
                            int d = int.Parse(dt.ToString("yyyyMMdd"));

                            if (d >= d1 && d <= d2)
                            {
                                if (d1 != d2)
                                {
                                    // 開始日 != 終了日の場合は「日数」を表すので、毎日 1 をセット。
                                    // 基本的には内服のみ。
                                    r.Cells[dt.ToString("yyyyMMdd")].Value = "1";
                                }
                                else
                                {
                                    // 開始日 = 終了日のときは回数を表す。
                                    r.Cells[dt.ToString("yyyyMMdd")].Value = obj2.Times;
                                }
                            }
                            else
                            {
                                r.Cells[dt.ToString("yyyyMMdd")].Value = "";
                            }
                        }
                    }
                    else
                    {
                        r.Cells["日/回数"].Value = "";
                    }

                    if (obj.KaikeiFlg.Equals("1"))
                    {
                        r.Cells["オーダーコード"].Style.BackColor = Color.Orange;
                        r.Cells["オーダー項目"].Style.BackColor = Color.Orange;
                        r.Cells["オーダー番号"].Style.BackColor = Color.Orange;
                        r.Cells["明細連番"].Style.BackColor = Color.Orange;
                        r.Cells["数量"].Style.BackColor = Color.Orange;
                        r.Cells["単位"].Style.BackColor = Color.Orange;
                        r.Cells["日/回数"].Style.BackColor = Color.Orange;
                    }
                    else if (obj.SekouFlg.Equals("1"))
                    {
                        r.Cells["オーダーコード"].Style.BackColor = Color.Yellow;
                        r.Cells["オーダー項目"].Style.BackColor = Color.Yellow;
                        r.Cells["オーダー番号"].Style.BackColor = Color.Yellow;
                        r.Cells["明細連番"].Style.BackColor = Color.Yellow;
                        r.Cells["数量"].Style.BackColor = Color.Yellow;
                        r.Cells["単位"].Style.BackColor = Color.Yellow;
                        r.Cells["日/回数"].Style.BackColor = Color.Yellow;
                    }

                    uke_first = false;

                    j++;
                }
            }

            // 最初のスクロール位置
//            OrderCalView1.FirstDisplayedScrollingColumnIndex = 100;
        }

        private void OrderHistoryView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            DataGridViewRow r = OrderHistoryView1.Rows[e.RowIndex];

            // 同じオーダー番号のものは選択・非選択される
            foreach (DataGridViewRow rr in OrderHistoryView1.Rows)
            {
                if (rr.Index == e.RowIndex)
                {
                    continue;
                }

                if (rr.Cells["オーダー番号"].Value.ToString().Equals(r.Cells["オーダー番号"].Value.ToString()))
                {
                    if (r.Selected)
                    {
                        rr.Selected = true;
                    }
                    else
                    {
                        rr.Selected = false;
                    }
                }
            }
             */
        }

        private void DoHistoryButton1_Click(object sender, EventArgs e)
        {
            /*
            int i = 0;

            if (OrderGridView1.CurrentRow != null)
            {
                i = OrderGridView1.CurrentRow.Index;
            }
            else
            {
                i = OrderGridView1.Rows.Count - 1;
            }

            // 新規行または区切りの先頭行でない場合はエラーを出す
            DataGridViewRow rr = OrderGridView1.Rows[i];

            if (!rr.IsNewRow && rr.Cells["Kind"].Value != null && !rr.Cells["Kind"].Value.ToString().Equals("1"))
            {
                MessageBox.Show("ここにオーダーを入れることは出来ません");
                return;
            }
             */

            List<OrderDetail> list = new List<OrderDetail>();

            // オーダー番号のリスト
            List<string> list2 = new List<string>();

            foreach (DataGridViewRow r in OrderHistoryView1.Rows)
            {
                if (r.Selected)
                {
                    OrderDetail obj = new OrderDetail();

                    int.TryParse(r.Cells["明細連番"].Value.ToString(), out obj.Kind);
                    int.TryParse(r.Cells["明細連番"].Value.ToString(), out obj.SEQ2);
                    obj.SekouDate = DateTime.Now.ToString("yyyy/MM/dd");
                    obj.SDCD = r.Cells["SDCD"].Value.ToString();
                    obj.KouiName = r.Cells["診"].Value.ToString();
                    obj.OrderCode = r.Cells["オーダーコード"].Value.ToString();
                    obj.OrderName = r.Cells["オーダー項目"].Value.ToString();
                    float.TryParse(r.Cells["数量"].Value.ToString(), out obj.Qty);
                    obj.Unit = r.Cells["単位"].Value.ToString();
                    float.TryParse(r.Cells["日/回数"].Value.ToString(), out obj.Times);

                    list.Add(obj);

                    // オーダー番号をリストに追加する
                    string order_id = r.Cells["オーダー番号"].Value.ToString();

                    if (!list2.Contains(order_id))
                    {
                        list2.Add(order_id);
                    }
                }
            }

            /*
            // OrderGridView
            this.OrderDetailInsert(list, OrderGridView1, i);

            OrderGridView1.CurrentCell = OrderGridView1.Rows[i + list.Count].Cells[0];
            */

            this.OrderSheetView1.OrderDetailInsert(list);


            // OrderPanel
            foreach (string order_id in list2)
            {
                this.OrderPanel1.Add(new CtrlOrder1(order_id));
            }
        }

        private void DoCalButton1_Click(object sender, EventArgs e)
        {
            /*
            int i = 0;

            if (OrderGridView1.CurrentRow != null)
            {
                i = OrderGridView1.CurrentRow.Index;
            }
            else
            {
                i = OrderGridView1.Rows.Count - 1;
            }

            // 新規行または区切りの先頭行でない場合はエラーを出す
            DataGridViewRow rr = OrderGridView1.Rows[i];

            if (!rr.IsNewRow && rr.Cells["Kind"].Value != null && !rr.Cells["Kind"].Value.ToString().Equals("1"))
            {
                MessageBox.Show("ここにオーダーを入れることは出来ません");
                return;
            }
            */

            List<OrderDetail> list = new List<OrderDetail>();

            foreach (DataGridViewRow r in OrderCalView1.Rows)
            {
                if (r.Cells["オーダー項目"].Style.BackColor == Color.LightCyan)
                {
                    OrderDetail obj = new OrderDetail();

                    int.TryParse(r.Cells["明細連番"].Value.ToString(), out obj.Kind);
                    int.TryParse(r.Cells["明細連番"].Value.ToString(), out obj.SEQ2);
                    obj.SekouDate = DateTime.Now.ToString("yyyy/MM/dd");
                    obj.SDCD = r.Cells["SDCD"].Value.ToString();
                    obj.KouiName = r.Cells["診"].Value.ToString();
                    obj.OrderCode = r.Cells["オーダーコード"].Value.ToString();
                    obj.OrderName = r.Cells["オーダー項目"].Value.ToString();
                    float.TryParse(r.Cells["数量"].Value.ToString(), out obj.Qty);
                    obj.Unit = r.Cells["単位"].Value.ToString();
                    float.TryParse(r.Cells["日/回数"].Value.ToString(), out obj.Times);

                    list.Add(obj);

                    if (r.Cells["会計フラグ"].Value.ToString().Equals("1"))
                    {
                        r.Cells["オーダーコード"].Style.BackColor = Color.Orange;
                        r.Cells["オーダー項目"].Style.BackColor = Color.Orange;
                        r.Cells["オーダー番号"].Style.BackColor = Color.Orange;
                        r.Cells["明細連番"].Style.BackColor = Color.Orange;
                        r.Cells["数量"].Style.BackColor = Color.Orange;
                        r.Cells["単位"].Style.BackColor = Color.Orange;
                        r.Cells["日/回数"].Style.BackColor = Color.Orange;
                    }
                    else if (r.Cells["施行フラグ"].Value.ToString().Equals("1"))
                    {
                        r.Cells["オーダーコード"].Style.BackColor = Color.Yellow;
                        r.Cells["オーダー項目"].Style.BackColor = Color.Yellow;
                        r.Cells["オーダー番号"].Style.BackColor = Color.Yellow;
                        r.Cells["明細連番"].Style.BackColor = Color.Yellow;
                        r.Cells["数量"].Style.BackColor = Color.Yellow;
                        r.Cells["単位"].Style.BackColor = Color.Yellow;
                        r.Cells["日/回数"].Style.BackColor = Color.Yellow;
                    }
                    else
                    {
                        r.Cells["オーダーコード"].Style.BackColor = Color.White;
                        r.Cells["オーダー項目"].Style.BackColor = Color.White;
                        r.Cells["オーダー番号"].Style.BackColor = Color.White;
                        r.Cells["明細連番"].Style.BackColor = Color.White;
                        r.Cells["数量"].Style.BackColor = Color.White;
                        r.Cells["単位"].Style.BackColor = Color.White;
                        r.Cells["日/回数"].Style.BackColor = Color.White;
                    }
                }
            }

            /*
            OrderDetailInsert(list, OrderGridView1, i);

            OrderGridView1.CurrentCell = OrderGridView1.Rows[i + list.Count].Cells[0];
             */

            this.OrderSheetView1.OrderDetailInsert(list);
        }

        void CalModeChange(CalMode mode)
        {
            this.CalMode1 = mode;

            if (mode == CalMode.On)
            {
                this.CalModeLabel1.Text = "On";
            }
            else
            {
                this.CalModeLabel1.Text = "None";
            }
        }

        private void OrderCalView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex <= 9)
            {
                return;
            }
            else if (e.ColumnIndex <= 18)
            {
                DataGridViewCell cell = OrderCalView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string order_id = OrderCalView1.Rows[e.RowIndex].Cells["オーダー番号"].Value.ToString();

                if (cell.Style.BackColor == Color.LightCyan)
                {
                    foreach (DataGridViewRow r in OrderCalView1.Rows)
                    {
                        if (r.Cells["オーダー番号"].Value.ToString().Equals(order_id))
                        {
                            if (r.Cells["会計フラグ"].Value.ToString().Equals("1"))
                            {
                                r.Cells["オーダーコード"].Style.BackColor = Color.Orange;
                                r.Cells["オーダー項目"].Style.BackColor = Color.Orange;
                                r.Cells["オーダー番号"].Style.BackColor = Color.Orange;
                                r.Cells["明細連番"].Style.BackColor = Color.Orange;
                                r.Cells["数量"].Style.BackColor = Color.Orange;
                                r.Cells["単位"].Style.BackColor = Color.Orange;
                                r.Cells["日/回数"].Style.BackColor = Color.Orange;
                            }
                            else if (r.Cells["施行フラグ"].Value.ToString().Equals("1"))
                            {
                                r.Cells["オーダーコード"].Style.BackColor = Color.Yellow;
                                r.Cells["オーダー項目"].Style.BackColor = Color.Yellow;
                                r.Cells["オーダー番号"].Style.BackColor = Color.Yellow;
                                r.Cells["明細連番"].Style.BackColor = Color.Yellow;
                                r.Cells["数量"].Style.BackColor = Color.Yellow;
                                r.Cells["単位"].Style.BackColor = Color.Yellow;
                                r.Cells["日/回数"].Style.BackColor = Color.Yellow;
                            }
                            else
                            {
                                r.Cells["オーダーコード"].Style.BackColor = Color.White;
                                r.Cells["オーダー項目"].Style.BackColor = Color.White;
                                r.Cells["オーダー番号"].Style.BackColor = Color.White;
                                r.Cells["明細連番"].Style.BackColor = Color.White;
                                r.Cells["数量"].Style.BackColor = Color.White;
                                r.Cells["単位"].Style.BackColor = Color.White;
                                r.Cells["日/回数"].Style.BackColor = Color.White;
                            }
                        }
                    }
                }
                else
                {
                    foreach (DataGridViewRow r in OrderCalView1.Rows)
                    {
                        if (r.Cells["オーダー番号"].Value.ToString().Equals(order_id))
                        {
                            r.Cells["オーダーコード"].Style.BackColor = Color.LightCyan;
                            r.Cells["オーダー項目"].Style.BackColor = Color.LightCyan;
                            r.Cells["オーダー番号"].Style.BackColor = Color.LightCyan;
                            r.Cells["明細連番"].Style.BackColor = Color.LightCyan;
                            r.Cells["数量"].Style.BackColor = Color.LightCyan;
                            r.Cells["単位"].Style.BackColor = Color.LightCyan;
                            r.Cells["日/回数"].Style.BackColor = Color.LightCyan;
                        }
                    }
                }
            }
            else
            {
                CalModeChange(CalMode.On);

                this.CalStartColumnLabel1.Text = e.ColumnIndex.ToString();
                this.CalStartRowLabel1.Text = e.RowIndex.ToString();
            }
        }

        private void OrderCalView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex <= 18)
            {
                return;
            }

            if (this.CalStartColumnLabel1.Text.Length > 0 && this.CalStartRowLabel1.Text.Length > 0)
            {
                int col = int.Parse(this.CalStartColumnLabel1.Text);
                int row = int.Parse(this.CalStartRowLabel1.Text);

                if (OrderCalView1.Rows[row].Cells[col].Value == null)
                {
                    return;
                }

                string val = OrderCalView1.Rows[row].Cells[col].Value.ToString();

                if (col < e.ColumnIndex)
                {
                    if (row < e.RowIndex)
                    {
                        for (int c = col; c <= e.ColumnIndex; c++)
                        {
                            for (int r = row; r <= e.RowIndex; r++)
                            {
                                OrderCalView1.Rows[r].Cells[c].Value = val;
                                OrderCalView1.Rows[r].Cells[c].Tag = "1";
                            }
                        }
                    }
                    else
                    {
                        for (int c = col; c <= e.ColumnIndex; c++)
                        {
                            for (int r = row; r >= e.RowIndex; r--)
                            {
                                OrderCalView1.Rows[r].Cells[c].Value = val;
                                OrderCalView1.Rows[r].Cells[c].Style.BackColor = Color.Cyan;
                            }
                        }
                    }
                }
                else
                {
                    if (row < e.RowIndex)
                    {
                        for (int c = col; c >= e.ColumnIndex; c--)
                        {
                            for (int r = row; r <= e.RowIndex; r++)
                            {
                                OrderCalView1.Rows[r].Cells[c].Value = val;
                                OrderCalView1.Rows[r].Cells[c].Style.BackColor = Color.Cyan;
                            }
                        }
                    }
                    else
                    {
                        for (int c = col; c >= e.ColumnIndex; c--)
                        {
                            for (int r = row; r >= e.RowIndex; r--)
                            {
                                OrderCalView1.Rows[r].Cells[c].Value = val;
                                OrderCalView1.Rows[r].Cells[c].Style.BackColor = Color.Cyan;
                            }
                        }
                    }
                }

                OrderCalView1.Rows[row].Cells[col].Style.BackColor = Color.White;
            }

            CalModeChange(CalMode.None);

            this.CalStartColumnLabel1.Text = "";
            this.CalStartRowLabel1.Text = "";
        }

        void tb_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void DatePicker1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void DatePicker2_ValueChanged(object sender, EventArgs e)
        {
        }

        private void DatePicker1_Validated(object sender, EventArgs e)
        {
            this.PatShow();
        }

        private void DatePicker2_Validated(object sender, EventArgs e)
        {
            this.PatShow();
        }

        private void SaveButton1_Click(object sender, EventArgs e)
        {
            List<string> msgs = new List<string>();

            if (this.Pat.Id.Length == 0)
            {
                msgs.Add("患者IDが入力されていません");
            }

            if (this.InsBox1.Text.Length == 0)
            {
                msgs.Add("保険が選択されていません");
            }

            if (this.DeptBox1.Text.Length == 0)
            {
                msgs.Add("科が選択されていません");
            }

            if (this.DoctorBox1.Text.Length == 0)
            {
                msgs.Add("医師が選択されていません");
            }

            if (msgs.Count > 0)
            {
                MessageBox.Show(AppString.ConcatList(msgs, Environment.NewLine));
                return;
            }

            List<OrderHeader> list = this.OrderSheetView1.GetOrderList();

            StdReturn sr = OrderHeader.Insert(this.Pat.Id, this.InsBox1.Text, this.InOut, this.DeptBox1.Text, this.DoctorBox1.Text, 0, false, list);

            LibUtility.Log(sr.Msg);
        }

        private void InsBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                if (this.Pat.Id.Length == 0)
                {
                    return;
                }

                PatIns obj = FormFindPatIns.FindPatIns(this.Pat.Id);

                this.InsBox1.Text = obj.SEQ.ToString();
                this.InsPerLabel1.Text = "外 " + obj.Per1 + "%";
                this.InsPerLabel2.Text = "入 " + obj.Per2 + "%";
            }
        }

        /// <summary>
        /// 外来・入院モード切り替え
        /// </summary>
        void InOutChange()
        {
            if (this.InOut.Equals("1"))
            {
                this.InOut = "2";
            }
            else if (this.InOut.Equals("2"))
            {
                this.InOut = "1";
            }
        }

        private void Panel1_DoubleClick(object sender, EventArgs e)
        {
            this.InOutChange();
        }

        private void OrderInsertMenuItem_Click(object sender, EventArgs e)
        {
            if (this.OrderMenuStrip1.SourceControl is CtrlOrderSheetGridView1)
            {
                this.OrderSheetView1.InsertCurrentRow();
            }
        }

        private void OrderDeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (this.OrderMenuStrip1.SourceControl is CtrlOrderSheetGridView1)
            {
                this.OrderSheetView1.RemoveCurrentRow();
            }
        }
    }
}
