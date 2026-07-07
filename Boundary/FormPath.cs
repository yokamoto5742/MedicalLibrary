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
    public partial class FormPath : StdForm1
    {
        CalSize cs = new CalSize();

        Graphics gc;
        Graphics gd;

        Pen p1 = new Pen(Color.LightGray, 1);
        Pen p2 = new Pen(Color.Black, 2);

        Pen prd1 = new Pen(Color.Red, 1);

        Pen pr2 = new Pen(Color.Red, 2);
        Pen pg2 = new Pen(Color.Green, 2);
        Pen pb2 = new Pen(Color.Blue, 2);

        PathMaster Path1 = new PathMaster();

        public FormPath()
        {
            InitializeComponent();

            // サイズ設定
            this.PathDateBox1.Width = cs.W_Day * 18;
            this.PathBox1.Width = cs.W_Day * 18;

            this.PathCategoryBox1.Height = cs.H_Category * PathCategoryMaster.ListActive.Count;
            this.PathBox1.Height = cs.H_Category * PathCategoryMaster.ListActive.Count;


            // 日付描画
            Bitmap bd = new Bitmap(PathDateBox1.Width, PathDateBox1.Height);
            PathDateBox1.Image = bd;
            gd = Graphics.FromImage(bd);

            for (int i = -3; i <= 14; i++)
            {
                DateTime dt = PathDate1.Value.AddDays(i);

                if (dt.DayOfWeek == DayOfWeek.Sunday)
                {
                    gd.FillRectangle(Brushes.LightPink, cs.W_Day * (i + 3), 0, cs.W_Day, cs.H_Date);
                }
                else if (dt.DayOfWeek == DayOfWeek.Saturday)
                {
                    gd.FillRectangle(Brushes.LightBlue, cs.W_Day * (i + 3), 0, cs.W_Day, cs.H_Date);
                }
                else
                {
                    gd.FillRectangle(Brushes.LightYellow, cs.W_Day * (i + 3), 0, cs.W_Day, cs.H_Date);
                }

                gd.DrawRectangle(p1, cs.W_Day * (i + 3), 0, cs.W_Day, cs.H_Date);
                gd.DrawString(dt.ToString("M/d (ddd)"), AppFont.F9.Ft, Brushes.Black, cs.W_Day * (i + 3) + 5, 5);

                gd.FillRectangle(Brushes.LightGreen, cs.W_Day * (i + 3), cs.H_Date, cs.W_Day, cs.H_InOut);
                gd.DrawRectangle(p1, cs.W_Day * (i + 3), cs.H_Date, cs.W_Day, cs.H_InOut);
            }

            // カテゴリ描画
            Bitmap bc = new Bitmap(PathCategoryBox1.Width, PathCategoryBox1.Height);
            PathCategoryBox1.Image = bc;
            gc = Graphics.FromImage(bc);

            for (int i = 0; i < PathCategoryMaster.ListActive.Count; i++)
            {
                gc.FillRectangle(Brushes.LightYellow, 0, cs.H_Category * i, cs.W_Category, cs.H_Category);
                gc.DrawRectangle(p1, 0, cs.H_Category * i, cs.W_Category, cs.H_Category);
                gc.DrawString(PathCategoryMaster.ListActive[i].Name, AppFont.F9.Ft, Brushes.Black, 5, cs.H_Category * i + 5);
            }

            // パス描画
            for (int i = -3; i <= 14; i++)
            {
                DateTime dt = PathDate1.Value.AddDays(i);

                for (int j = 0; j < PathCategoryMaster.ListActive.Count; j++)
                {
                    PathCategoryMaster obj = PathCategoryMaster.ListActive[j];

                    // 既存の指示
                    Panel p1 = new Panel();
                    p1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
                    p1.Location = new Point(cs.W_Day * (i + 3), cs.H_Category * j);
                    p1.Size = new Size(cs.W_Day - 1, cs.H_Category / 2 - 1);
                    p1.BorderStyle = BorderStyle.None;
                    p1.BackColor = Color.White;
                    p1.AutoScroll = true;
                    p1.Name = obj.Code + "_1_" + i.ToString();

                    PathBox1.Controls.Add(p1);

                    // 適用パスの指示
                    Panel p2 = new Panel();
                    p2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
                    p2.Location = new Point(cs.W_Day * (i + 3), cs.H_Category * j + cs.H_Category / 2);
                    p2.Size = new Size(cs.W_Day - 1, cs.H_Category / 2 - 1);
                    p2.BorderStyle = BorderStyle.None;
                    p2.BackColor = Color.LightYellow;
                    p2.AutoScroll = true;
                    p2.Name = obj.Code + "_2_" + i.ToString();

                    PathBox1.Controls.Add(p2);
                }
            }

        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            this.PathShow();
        }

        /// <summary>
        /// 描画
        /// </summary>
        void PathShow()
        {
            string date0 = this.PathDate1.Value.AddDays(-3).ToString("yyyyMMdd");
            string date1 = this.PathDate1.Value.AddDays(-2).ToString("yyyyMMdd");
            string date2 = this.PathDate1.Value.AddDays(14).ToString("yyyyMMdd");
            string today = this.PathDate1.Value.ToString("yyyyMMdd");

            int d0 = int.Parse(date0);
            int d1 = int.Parse(date1);
            int d2 = int.Parse(date2);
            int td = int.Parse(today);

            Bitmap bd = new Bitmap(PathDateBox1.Width, PathDateBox1.Height);
            PathDateBox1.Image = bd;
            gd = Graphics.FromImage(bd);

            // 入退院履歴
            PatIn pat_in = new PatIn();

            List<PatIn> list = PatIn.GetHistory(this.Pat.Id, true);

            foreach (PatIn pin in list)
            {
                if (pin.InDate.CompareTo(d2.ToString()) <= 0 &&
                    (pin.OutDate.CompareTo(d0.ToString()) >= 0 || !AppString.IsDate(pin.OutDate)))
                {
                    pat_in = pin;
                    break;
                }
            }

//            PatIn pat_in = PatIn.GetByDates(this.Pat.Id, d0, d2);

            for (int i = -3; i <= 14; i++)
            {
                DateTime dt = PathDate1.Value.AddDays(i);

                if (dt.DayOfWeek == DayOfWeek.Sunday)
                {
                    gd.FillRectangle(Brushes.LightPink, cs.W_Day * (i + 3), 0, cs.W_Day, cs.H_Date);
                }
                else if (dt.DayOfWeek == DayOfWeek.Saturday)
                {
                    gd.FillRectangle(Brushes.LightBlue, cs.W_Day * (i + 3), 0, cs.W_Day, cs.H_Date);
                }
                else
                {
                    gd.FillRectangle(Brushes.LightYellow, cs.W_Day * (i + 3), 0, cs.W_Day, cs.H_Date);
                }

                gd.DrawRectangle(p1, cs.W_Day * (i + 3), 0, cs.W_Day, cs.H_Date);
                gd.DrawString(dt.ToString("M/d (ddd)"), AppFont.F9.Ft, Brushes.Black, cs.W_Day * (i + 3) + 5, 5);

                // 入院何日目か（初日 = 1日目とする）
                int dd = -1;

                if (pat_in.InDateInt > 0 && pat_in.InDateInt <= int.Parse(dt.ToString("yyyyMMdd")) &&
                    (pat_in.OutDateInt == 0 || pat_in.OutDateInt >= int.Parse(dt.ToString("yyyyMMdd"))))
                {
                    dd = DateTimeAgent.IntervalDays(pat_in.InDate, dt.ToString("yyyyMMdd"));
                }

                if (dd >= 0)
                {
                    gd.FillRectangle(Brushes.LightPink, cs.W_Day * (i + 3), cs.H_Date, cs.W_Day, cs.H_InOut);
                    gd.DrawRectangle(p1, cs.W_Day * (i + 3), cs.H_Date, cs.W_Day, cs.H_InOut);
                    gd.DrawString("入院" + (dd + 1) + "日目", AppFont.F9.Ft, Brushes.Black, cs.W_Day * (i + 3) + 5, cs.H_Date + 5);
                }
                else
                {
                    gd.FillRectangle(Brushes.LightGreen, cs.W_Day * (i + 3), cs.H_Date, cs.W_Day, cs.H_InOut);
                    gd.DrawRectangle(p1, cs.W_Day * (i + 3), cs.H_Date, cs.W_Day, cs.H_InOut);
                }

                // いったんクリア
                for (int j = 0; j < PathCategoryMaster.ListActive.Count; j++)
                {
                    PathCategoryMaster obj = PathCategoryMaster.ListActive[j];

                    string key = obj.Code + "_1_" + i.ToString();

                    if (PathBox1.Controls.ContainsKey(key))
                    {
                        PathBox1.Controls[key].Controls.Clear();
                    }

                    key = obj.Code + "_2_" + i.ToString();

                    if (PathBox1.Controls.ContainsKey(key))
                    {
                        PathBox1.Controls[key].Controls.Clear();
                    }
                }
            }


            List<string> shinku_list = new List<string>();
            List<string> dept_list = new List<string>();

            // 期間中のオーダーを取得
            List<PatOrder> order_list = PatOrder.GetListByPatDates(this.Pat.Id, date0, date2, "", shinku_list, dept_list, false);

            foreach (PatOrder obj in order_list)
            {
                string category_code = "";

                int shinku = 0;
                int.TryParse(obj.Shinku, out shinku);

                // カテゴリを調べる
                foreach (PathCategoryMaster c in PathCategoryMaster.ListActive)
                {
                    if (shinku >= c.KouiCode1 && shinku <= c.KouiCode2)
                    {
                        category_code = c.Code;
                        break;
                    }
                }

                for (int i = 0; ; i++)
                {
                    int d = DateTimeAgent.AddDays(int.Parse(obj.StartDate), i);

                    // 終了日を過ぎたら break
                    if (d > int.Parse(obj.EndDate))
                    {
                        break;
                    }

                    int dd = DateTimeAgent.IntervalDays(td, d);

                    string key = category_code + "_1_" + dd.ToString();

                    if (PathBox1.Controls.ContainsKey(key))
                    {
                        Panel p = (Panel)(PathBox1.Controls[key]);

                        Label lb = new Label();
                        lb.Location = new Point(1, 1 + p.Controls.Count * 20);
                        lb.Size = new System.Drawing.Size(cs.W_Day - 25, 18);
                        lb.AutoEllipsis = true;
                        lb.Text = obj.SOAP;
                        lb.TextAlign = ContentAlignment.MiddleLeft;

                        if (obj.KaikeiFlg.Equals("1"))
                        {
                            lb.BackColor = Color.Orange;
                        }
                        else if (obj.SekouFlg.Equals("1"))
                        {
                            lb.BackColor = Color.Yellow;
                        }
                        else
                        {
                            lb.BackColor = Color.White;
                        }

                        lb.BorderStyle = BorderStyle.FixedSingle;
                        lb.Tag = obj.OrderId;
                        lb.DoubleClick += new EventHandler(OrderLabel_DoubleClick);

                        p.Controls.Add(lb);
                    }
                }
            }


            // 初日までに有効な看護指示があれば取得
            int nursing_start = NursingOrderData.GetPrevDateByDate(this.Pat.Id, int.Parse(this.PathDate1.Value.AddDays(-3).ToString("yyyyMMdd")));

            if (nursing_start > 0)
            {
                // 看護指示 82
                string key = "82_1_-3";

                if (PathBox1.Controls.ContainsKey(key))
                {
                    Panel p = (Panel)(PathBox1.Controls[key]);

                    // 看護指示の場合は上書きなので、元の指示があれば削除する
                    p.Controls.Clear();

                    Label lb = new Label();
                    lb.Location = new Point(1, 1 + p.Controls.Count * 20);
                    lb.Size = new System.Drawing.Size(cs.W_Day - 25, 18);
                    lb.AutoEllipsis = true;
                    lb.Text = "看護指示 " + DateTimeAgent.DateFormat(nursing_start, DateTimeAgent.DateFormatKind.LONG);
                    lb.TextAlign = ContentAlignment.MiddleLeft;
                    lb.BackColor = Color.White;
                    lb.BorderStyle = BorderStyle.FixedSingle;

                    p.Controls.Add(lb);
                }
            }

            // 期間中に看護指示があれば取得
            List<int> nursing_list = NursingOrderData.GetDateListByDates(this.Pat.Id, int.Parse(this.PathDate1.Value.AddDays(-2).ToString("yyyyMMdd")), int.Parse(this.PathDate1.Value.AddDays(14).ToString("yyyyMMdd")));

            for (int i = -2; i <= 14; i++)
            {
                DateTime dt = PathDate1.Value.AddDays(i);

                if (!nursing_list.Contains(int.Parse(dt.ToString("yyyyMMdd"))))
                {
                    continue;
                }

                // 看護指示 82
                string key = "82_1_" + i.ToString();

                if (PathBox1.Controls.ContainsKey(key))
                {
                    Panel p = (Panel)(PathBox1.Controls[key]);

                    // 看護指示の場合は上書きなので、元の指示があれば削除する
                    p.Controls.Clear();

                    Label lb = new Label();
                    lb.Location = new Point(1, 1 + p.Controls.Count * 20);
                    lb.Size = new System.Drawing.Size(cs.W_Day - 25, 18);
                    lb.AutoEllipsis = true;
                    lb.Text = "看護指示";
                    lb.TextAlign = ContentAlignment.MiddleLeft;
                    lb.BackColor = Color.White;
                    lb.BorderStyle = BorderStyle.FixedSingle;

                    p.Controls.Add(lb);
                }
            }

            // 初日までに有効な基本指示があれば取得
            BaseOrderData base_order = BaseOrderData.LoadPrevByDate(this.Pat.Id, int.Parse(this.PathDate1.Value.AddDays(-3).ToString("yyyyMMdd")));

            if (base_order.OrderDate > 0)
            {
                // 基本指示 83
                string key = "83_1_-3";

                if (PathBox1.Controls.ContainsKey(key))
                {
                    Panel p = (Panel)(PathBox1.Controls[key]);

                    // 基本指示の場合は上書きなので、元の指示があれば削除する
                    p.Controls.Clear();

                    Label lb = new Label();
                    lb.Location = new Point(1, 1 + p.Controls.Count * 20);
                    lb.Size = new System.Drawing.Size(cs.W_Day - 25, 18);
                    lb.AutoEllipsis = true;
                    lb.Text = "基本指示 " + DateTimeAgent.DateFormat(base_order.OrderDate, DateTimeAgent.DateFormatKind.LONG);
                    lb.TextAlign = ContentAlignment.MiddleLeft;
                    lb.BackColor = Color.White;
                    lb.BorderStyle = BorderStyle.FixedSingle;

                    p.Controls.Add(lb);
                }
            }

            // 期間中に基本指示があれば取得
            Dictionary<int, BaseOrderData> base_dict = BaseOrderData.LoadByDates(this.Pat.Id, int.Parse(this.PathDate1.Value.AddDays(-2).ToString("yyyyMMdd")), int.Parse(this.PathDate1.Value.AddDays(14).ToString("yyyyMMdd")));

            for (int i = -2; i <= 14; i++)
            {
                DateTime dt = PathDate1.Value.AddDays(i);

                if (!base_dict.ContainsKey(int.Parse(dt.ToString("yyyyMMdd"))))
                {
                    continue;
                }

                // 基本指示 83
                string key = "83_1_" + i.ToString();

                if (PathBox1.Controls.ContainsKey(key))
                {
                    Panel p = (Panel)(PathBox1.Controls[key]);

                    // 看護指示の場合は上書きなので、元の指示があれば削除する
                    p.Controls.Clear();

                    Label lb = new Label();
                    lb.Location = new Point(1, 1 + p.Controls.Count * 20);
                    lb.Size = new System.Drawing.Size(cs.W_Day - 25, 18);
                    lb.AutoEllipsis = true;
                    lb.Text = "基本指示";
                    lb.TextAlign = ContentAlignment.MiddleLeft;
                    lb.BackColor = Color.White;
                    lb.BorderStyle = BorderStyle.FixedSingle;

                    p.Controls.Add(lb);
                }
            }
        }

        void OrderLabel_DoubleClick(object sender, EventArgs e)
        {
            if (this.Controls.ContainsKey("OrderPanel"))
            {
                this.Controls["OrderPanel"].Dispose();
            }

            string order_id = ((Label)sender).Tag.ToString();

            Panel p = PatOrder.GetPanel(order_id, 200, 200);
            p.Name = "OrderPanel";
            p.Location = new Point(1020, 80);
            p.BackColor = Color.LightYellow;

            this.Controls.Add(p);
        }

        void TaskLabel_DoubleClick(object sender, EventArgs e)
        {
            if (this.Controls.ContainsKey("OrderPanel"))
            {
                this.Controls["OrderPanel"].Dispose();
            }

            string task_code = ((Label)sender).Tag.ToString();

            Panel p = PathMasterTaskDetail.GetPanel(task_code, 200, 200);
            p.Name = "OrderPanel";
            p.Location = new Point(1020, 80);
            p.BackColor = Color.LightYellow;

            this.Controls.Add(p);
        }

        private void FormPathSelectorButton1_Click(object sender, EventArgs e)
        {
            FormPathTree f = new FormPathTree(this);
            f.ShowDialog();
        }

        public void PathMasterSet(PathMaster pm)
        {
            this.Path1 = pm;

            this.PathMasterBox1.Text = pm.Code;

            foreach (PathMasterTask obj in pm.TaskList)
            {
                string key = obj.CategoryCode + "_2_" + (obj.StartDay - 1).ToString();

                if (PathBox1.Controls.ContainsKey(key))
                {
                    Panel p = (Panel)(PathBox1.Controls[key]);

                    Label lb = new Label();
                    lb.Location = new Point(1, 1 + p.Controls.Count * 20);
                    lb.Size = new System.Drawing.Size(cs.W_Day - 25, 18);
                    lb.AutoEllipsis = true;
                    lb.Text = obj.TaskName;
                    lb.TextAlign = ContentAlignment.MiddleLeft;
                    lb.BackColor = Color.LightGreen;
                    lb.BorderStyle = BorderStyle.FixedSingle;
                    lb.Tag = obj.TaskCode;
                    lb.DoubleClick += new EventHandler(TaskLabel_DoubleClick);

                    p.Controls.Add(lb);
                }
            }

            foreach (int d in NursingOrderPathTemplate.GetDateList(pm.Code))
            {
                if (pm.NursingDict.ContainsKey(d))
                {
                    Dictionary<int, Dictionary<int, NursingOrderPathTemplate>> dict = pm.NursingDict[d];

                    // 看護指示 82
                    string key = "82_2_" + (d - 1).ToString();

                    if (PathBox1.Controls.ContainsKey(key))
                    {
                        Panel p = (Panel)(PathBox1.Controls[key]);

                        // 看護指示の場合は上書きなので、元の指示があれば削除する
                        p.Controls.Clear();

                        Label lb = new Label();
                        lb.Location = new Point(1, 1 + p.Controls.Count * 20);
                        lb.Size = new System.Drawing.Size(cs.W_Day - 25, 18);
                        lb.AutoEllipsis = true;
                        lb.Text = "看護指示";
                        lb.TextAlign = ContentAlignment.MiddleLeft;
                        lb.BackColor = Color.LightGreen;
                        lb.BorderStyle = BorderStyle.FixedSingle;

                        p.Controls.Add(lb);
                    }
                }
            }

            foreach (int d in OpeOrderPathTemplate.GetDateList(pm.Code))
            {
                if (pm.OpeDict.ContainsKey(d))
                {
                    Dictionary<int, Dictionary<int, OpeOrderPathTemplate>> dict = pm.OpeDict[d];

                    // 手術指示 84
                    string key = "84_2_" + (d - 1).ToString();

                    if (PathBox1.Controls.ContainsKey(key))
                    {
                        Panel p = (Panel)(PathBox1.Controls[key]);

                        // 手術指示の場合は上書きなので、元の指示があれば削除する
                        p.Controls.Clear();

                        Label lb = new Label();
                        lb.Location = new Point(1, 1 + p.Controls.Count * 20);
                        lb.Size = new System.Drawing.Size(cs.W_Day - 25, 18);
                        lb.AutoEllipsis = true;
                        lb.Text = "手術指示";
                        lb.TextAlign = ContentAlignment.MiddleLeft;
                        lb.BackColor = Color.LightGreen;
                        lb.BorderStyle = BorderStyle.FixedSingle;

                        p.Controls.Add(lb);
                    }
                }
            }

            foreach (BaseOrderPathTemplate obj in pm.BaseList)
            {
                // 基本指示 83
                string key = "83_2_" + (obj.OrderDate - 1).ToString();

                if (PathBox1.Controls.ContainsKey(key))
                {
                    Panel p = (Panel)(PathBox1.Controls[key]);

                    // 基本指示の場合は上書きなので、元の指示があれば削除する
                    p.Controls.Clear();

                    Label lb = new Label();
                    lb.Location = new Point(1, 1 + p.Controls.Count * 20);
                    lb.Size = new System.Drawing.Size(cs.W_Day - 25, 18);
                    lb.AutoEllipsis = true;
                    lb.Text = "基本指示";
                    lb.TextAlign = ContentAlignment.MiddleLeft;
                    lb.BackColor = Color.LightGreen;
                    lb.BorderStyle = BorderStyle.FixedSingle;

                    p.Controls.Add(lb);
                }
            }
        }


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
                    return 20;
                }
            }

            /// <summary>
            /// 入外欄の高さ
            /// </summary>
            public int H_InOut
            {
                get
                {
                    return 20;
                }
            }

            /// <summary>
            /// カテゴリあたりの高さ
            /// </summary>
            public int H_Category
            {
                get
                {
                    return 120;
                }
            }

            /// <summary>
            /// 1日あたりの幅
            /// </summary>
            public int W_Day
            {
                get
                {
                    return 150;
                }
            }

            /// <summary>
            /// カテゴリの幅
            /// </summary>
            public int W_Category
            {
                get
                {
                    return 100;
                }
            }
        }

        private void PathPanel1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                PathCategoryBox1.Location = new Point(3, 3 - e.NewValue);
            }
            else if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                PathDateBox1.Location = new Point(3 - e.NewValue, 3);
            }
        }

        private void PathDate1_ValueChanged(object sender, EventArgs e)
        {
            if (MessageBox.Show("いったんクリアされます。よろしいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.PathShow();
            }
        }
    }
}
