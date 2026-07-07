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
    public partial class FormVital : StdForm1
    {
        ChartSize cs = new ChartSize();

        Graphics gc;
        Graphics gd;

        Pen p1 = new Pen(Color.LightGray, 1);
        Pen p2 = new Pen(Color.Black, 2);

        Pen prd1 = new Pen(Color.Red, 1);

        Pen pr2 = new Pen(Color.Red, 2);
        Pen pg2 = new Pen(Color.Green, 2);
        Pen pb2 = new Pen(Color.Blue, 2);

        public FormVital()
        {
            InitializeComponent();

            // 位置とサイズ調整
            ChartBox1.Width = cs.W1 + cs.W_Day * 7;
            ChartBox1.Height = cs.H_Day + cs.H_Grid * 5;

            DataPanel1.Location = new Point(ChartBox1.Location.X, ChartBox1.Location.Y + ChartBox1.Height + 2);
            DataPanel1.Width = cs.W1 + cs.W_Day * 7 + 25;
            DataPanel1.Height = this.Height - DataPanel1.Location.Y - 50;

            DataBox1.Location = new Point(0, 0);
            DataBox1.Width = cs.W1 + cs.W_Day * 7;
            DataBox1.Height = cs.H1 * VitalMaster.Dict.Count;

            // 破線
            prd1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
        }

        private void FormVital_Load(object sender, EventArgs e)
        {
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            this.DataDraw();
        }

        /// <summary>
        /// ChartBox, DataBox 描画
        /// </summary>
        void DataDraw()
        {
            // ChartBox1 描画
            Bitmap bc = new Bitmap(ChartBox1.Width, ChartBox1.Height);
            ChartBox1.Image = bc;
            gc = Graphics.FromImage(bc);

            // DataBox1 描画
            Bitmap bd = new Bitmap(DataBox1.Width, DataBox1.Height);
            DataBox1.Image = bd;
            gd = Graphics.FromImage(bd);

            int wc = ChartBox1.Width;
            int hc = ChartBox1.Height;

            int wd = DataBox1.Width;
            int hd = DataBox1.Height;


            // 項目名の背景色
            gc.FillRectangle(Brushes.LightYellow, 0, cs.H_Day, cs.W1, hc);
            gd.FillRectangle(Brushes.LightYellow, 0, 0, cs.W1, hd);

            // 左の線
            gc.DrawLine(p1, cs.W1, 0, cs.W1, hc);
            gd.DrawLine(p1, cs.W1, 0, cs.W1, hd);

            // 日付と区切り線
            for (int i = 0; i < 7; i++)
            {
                gc.DrawString(ChartDate1.Value.AddDays(i - 6).ToString("M/dd(ddd)"), AppFont.F10.Ft, Brushes.Black, cs.W1 + cs.W_Day * i, 0);
                gc.DrawString("0", AppFont.F10.Ft, Brushes.Black, cs.W1 + cs.W_Day * i, cs.H_Day / 2);
                gc.DrawString("12", AppFont.F10.Ft, Brushes.Black, cs.W1 + cs.W_Day * i + cs.W_Day / 2, cs.H_Day / 2);

                gc.DrawLine(p1, cs.W1 + cs.W_Day * i, 0, cs.W1 + cs.W_Day * i, hc);
                gd.DrawLine(p1, cs.W1 + cs.W_Day * i, 0, cs.W1 + cs.W_Day * i, hd);
            }

            // 日付と時刻の間の横線
            gc.DrawLine(p1, cs.W1, cs.H_Day / 2, wc, cs.H_Day / 2);


            // BP・P・T 目盛線と数値
            gc.DrawString("BP", AppFont.F10.Ft, Brushes.Green, 30, cs.H_Day / 2);
            gc.DrawString("P", AppFont.F10.Ft, Brushes.Red, 60, cs.H_Day / 2);
            gc.DrawString("T", AppFont.F10.Ft, Brushes.Blue, 90, cs.H_Day / 2);

            gc.DrawLine(p1, 30, cs.H_Day, 30, hc);
            gc.DrawLine(p1, 60, cs.H_Day, 60, hc);
            gc.DrawLine(p1, 90, cs.H_Day, 90, hc);

            for (int i = 1; i <= 9; i++)
            {
                gc.DrawString((220 - i * 20).ToString(), AppFont.F10.Ft, Brushes.Green, 30, cs.H_Day + i * (cs.H_Grid * 5 / 10) - 12);
                gc.DrawString((200 - i * 20).ToString(), AppFont.F10.Ft, Brushes.Red, 60, cs.H_Day + i * (cs.H_Grid * 5 / 10) - 12);
            }

            for (int i = 1; i <= 6; i++)
            {
                gc.DrawString((41 - i).ToString(), AppFont.F10.Ft, Brushes.Blue, 90, cs.H_Day + i * (cs.H_Grid * 5 / 7) - 12);
            }


            // 37度線
            gc.DrawLine(prd1, cs.W1, cs.H_Day + (cs.H_Grid * 5 / 7) * 4, wc, cs.H_Day + (cs.H_Grid * 5 / 7) * (41 - 37));


            // 横線
            gc.DrawLine(p1, 0, cs.H_Day, wc, cs.H_Day);

            for (int i = 1; i <= 5; i++)
            {
                gc.DrawLine(p1, 0, cs.H_Day + cs.H_Grid * i, wc, cs.H_Day + cs.H_Grid * i);
            }

            for (int i = 1; i <= VitalMaster.Dict.Count; i++)
            {
                gd.DrawLine(p1, 0, cs.H1 * i, wd, cs.H1 * i);
            }


            // 項目名
            int c = 0;

            foreach (VitalMaster m in VitalMaster.Dict.Values)
            {
                gd.DrawString(m.Name, AppFont.F10.Ft, m.ChartBrush, 5, cs.H1 * c + 10);
                c++;
            }

            if (this.Pat.Id.Length > 0)
            {
                DataBox1.Controls.Clear();

                List<Vital> DataList = Vital.GetListByDates(this.Pat.Id, ChartDate1.Value.AddDays(-6).ToString("yyyyMMdd"), ChartDate1.Value.ToString("yyyyMMdd"));

                Label[,] labels = new Label[7, VitalMaster.Dict.Count];
                ToolTip[,] tips = new ToolTip[7, VitalMaster.Dict.Count];

                for (int i = 0; i <= 6; i++)
                {
                    for (int j = 0; j < VitalMaster.Dict.Count; j++)
                    {
                        Label d1 = new Label();
                        labels[i, j] = d1;

                        LabelInfo info1 = new LabelInfo();
                        info1.DateString = ChartDate1.Value.AddDays(i - 6).ToString("yyyy/MM/dd");

                        int k = 0;

                        foreach (string key in VitalMaster.Dict.Keys)
                        {
                            if (k < j)
                            {
                                k++;
                            }
                            else
                            {
                                info1.Code = key;
                                break;
                            }
                        }

                        d1.Tag = info1;
                        d1.Font = AppFont.F9.Ft;
                        d1.Location = new Point(cs.W1 + cs.W_Day * i + 1, cs.H1 * j + 1);
                        d1.AutoSize = false;
                        d1.Size = new Size(cs.W_Day - 2, cs.H1 - 2);
                        d1.MouseClick += new MouseEventHandler(d1_MouseClick);

                        ToolTip t1 = new ToolTip();
                        tips[i, j] = t1;
                    }
                }

                foreach (Vital data in DataList)
                {
                    // データが無ければ飛ばす
                    if (data.Data1.Length == 0 && data.Data2.Length == 0)
                    {
                        continue;
                    }

                    string date = AppDateTime.DateStringFromString(data.VitalDate);
                    string time = AppDateTime.TimeStringFromString4(data.VitalTime);

                    DateTime dt1 = DateTime.Parse(date + " " + time);
                    DateTime dt0 = DateTime.Parse(ChartDate1.Value.ToString("yyyy/MM/dd 23:59:59"));

                    int i = 6 - dt0.Subtract(dt1).Days;

                    Label d1 = labels[i, data.Num - 1];

                    if (d1.Text.Length > 0)
                    {
                        d1.Text += Environment.NewLine;
                    }

                    d1.Text += time + "  ";

                    if (data.Data1.Length > 0)
                    {
                        d1.Text += data.Data1.ToString();
                    }

                    if (data.Data2.Length > 0)
                    {
                        d1.Text += " / " + data.Data2.ToString();
                    }

                    ToolTip t1 = tips[i, data.Num - 1];

                    t1.SetToolTip(d1, d1.Text);
                }

                for (int i = 0; i <= 6; i++)
                {
                    for (int j = 0; j < VitalMaster.Dict.Count; j++)
                    {
                        Label d1 = labels[i, j];

                        DataBox1.Controls.Add(d1);
                    }
                }


                // チャート描画

                Vital v1 = new Vital();
                Vital v2 = new Vital();

                float x1 = 0.0F;
                float y1 = 0.0F;
                float x2 = 0.0F;
                float y2 = 0.0F;

                // 体温チャート描画
                foreach (Vital data in DataList)
                {
                    // 体温でなければ飛ばす
                    if (!data.Code.Equals("1"))
                    {
                        continue;
                    }

                    // データが無ければ飛ばす
                    if (data.Data1.Length == 0)
                    {
                        continue;
                    }

                    // 点を描画
                    string date = AppDateTime.DateStringFromString(data.VitalDate);
                    string time = AppDateTime.TimeStringFromString4(data.VitalTime);

                    DateTime dt1 = DateTime.Parse(date + " " + time);
                    DateTime dt0 = DateTime.Parse(ChartDate1.Value.ToString("yyyy/MM/dd 23:59:59"));

                    int i = 6 - dt0.Subtract(dt1).Days;

                    int th = AppDateTime.HourIntFromString4(data.VitalTime);
                    int tm = AppDateTime.MinuteIntFromString4(data.VitalTime);

                    x2 = cs.W1 + cs.W_Day * i + cs.W_Hour * th + cs.W_Min * tm;
                    y2 = cs.H_Day + (cs.H_Grid * 5 / 7) * (41 - float.Parse(data.Data1));

                    gc.FillEllipse(Brushes.Blue, x2 - 1.5F, y2 - 1.5F, 3.0F, 3.0F);

                    if (v1.Code.Length > 0)
                    {
                        // 2番目以降のデータは点と線を描く
                        v2 = data;

                        gc.DrawLine(pb2, x1, y1, x2, y2);
                    }

                    v1 = data;
                    x1 = x2;
                    y1 = y2;
                }

                v1 = new Vital();
                v2 = new Vital();

                x1 = 0.0F;
                y1 = 0.0F;
                x2 = 0.0F;
                y2 = 0.0F;

                // 血圧（高）チャート描画
                foreach (Vital data in DataList)
                {
                    // 血圧でなければ飛ばす
                    if (!data.Code.Equals("2"))
                    {
                        continue;
                    }

                    // データが無ければ飛ばす
                    if (data.Data1.Length == 0)
                    {
                        continue;
                    }

                    // 点を描画
                    string date = AppDateTime.DateStringFromString(data.VitalDate);
                    string time = AppDateTime.TimeStringFromString4(data.VitalTime);

                    DateTime dt1 = DateTime.Parse(date + " " + time);
                    DateTime dt0 = DateTime.Parse(ChartDate1.Value.ToString("yyyy/MM/dd 23:59:59"));

                    int i = 6 - dt0.Subtract(dt1).Days;

                    int th = AppDateTime.HourIntFromString4(data.VitalTime);
                    int tm = AppDateTime.MinuteIntFromString4(data.VitalTime);

                    x2 = cs.W1 + cs.W_Day * i + cs.W_Hour * th + cs.W_Min * tm;
                    y2 = cs.H_Day + (cs.H_Grid * 5 / 200) * (220 - int.Parse(data.Data1));

                    gc.FillEllipse(Brushes.Green, x2 - 1.5F, y2 - 1.5F, 3.0F, 3.0F);

                    if (v1.Code.Length > 0)
                    {
                        // 2番目以降のデータは点と線を描く
                        v2 = data;

                        gc.DrawLine(pg2, x1, y1, x2, y2);
                    }

                    v1 = data;
                    x1 = x2;
                    y1 = y2;
                }

                v1 = new Vital();
                v2 = new Vital();

                x1 = 0.0F;
                y1 = 0.0F;
                x2 = 0.0F;
                y2 = 0.0F;

                // 血圧（低）チャート描画
                foreach (Vital data in DataList)
                {
                    // 血圧でなければ飛ばす
                    if (!data.Code.Equals("2"))
                    {
                        continue;
                    }

                    // データが無ければ飛ばす
                    if (data.Data2.Length == 0)
                    {
                        continue;
                    }

                    // 点を描画
                    string date = AppDateTime.DateStringFromString(data.VitalDate);
                    string time = AppDateTime.TimeStringFromString4(data.VitalTime);

                    DateTime dt1 = DateTime.Parse(date + " " + time);
                    DateTime dt0 = DateTime.Parse(ChartDate1.Value.ToString("yyyy/MM/dd 23:59:59"));

                    int i = 6 - dt0.Subtract(dt1).Days;

                    int th = AppDateTime.HourIntFromString4(data.VitalTime);
                    int tm = AppDateTime.MinuteIntFromString4(data.VitalTime);

                    x2 = cs.W1 + cs.W_Day * i + cs.W_Hour * th + cs.W_Min * tm;
                    y2 = cs.H_Day + (cs.H_Grid * 5 / 200) * (220 - int.Parse(data.Data2));

                    gc.FillEllipse(Brushes.Green, x2 - 1.5F, y2 - 1.5F, 3.0F, 3.0F);

                    if (v1.Code.Length > 0)
                    {
                        // 2番目以降のデータは点と線を描く
                        v2 = data;

                        gc.DrawLine(pg2, x1, y1, x2, y2);
                    }

                    v1 = data;
                    x1 = x2;
                    y1 = y2;
                }

                v1 = new Vital();
                v2 = new Vital();

                x1 = 0.0F;
                y1 = 0.0F;
                x2 = 0.0F;
                y2 = 0.0F;

                // 脈拍チャート描画
                foreach (Vital data in DataList)
                {
                    // 脈拍でなければ飛ばす
                    if (!data.Code.Equals("3"))
                    {
                        continue;
                    }

                    // データが無ければ飛ばす
                    if (data.Data1.Length == 0)
                    {
                        continue;
                    }

                    // 点を描画
                    string date = AppDateTime.DateStringFromString(data.VitalDate);
                    string time = AppDateTime.TimeStringFromString4(data.VitalTime);

                    DateTime dt1 = DateTime.Parse(date + " " + time);
                    DateTime dt0 = DateTime.Parse(ChartDate1.Value.ToString("yyyy/MM/dd 23:59:59"));

                    int i = 6 - dt0.Subtract(dt1).Days;

                    int th = AppDateTime.HourIntFromString4(data.VitalTime);
                    int tm = AppDateTime.MinuteIntFromString4(data.VitalTime);

                    x2 = cs.W1 + cs.W_Day * i + cs.W_Hour * th + cs.W_Min * tm;
                    y2 = cs.H_Day + (cs.H_Grid * 5 / 200) * (200 - float.Parse(data.Data1));

                    gc.FillEllipse(Brushes.Red, x2 - 1.5F, y2 - 1.5F, 3.0F, 3.0F);

                    if (v1.Code.Length > 0)
                    {
                        // 2番目以降のデータは点と線を描く
                        v2 = data;

                        gc.DrawLine(pr2, x1, y1, x2, y2);
                    }

                    v1 = data;
                    x1 = x2;
                    y1 = y2;
                }
            }
        }

        void d1_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.Pat.Id.Length == 0)
            {
                MessageBox.Show("患者IDが入力されていません");
                return;
            }

            LabelInfo info1 = (LabelInfo)((Label)sender).Tag;

            FormVitalDaily f = new FormVitalDaily(this.Pat.Id, info1.DateString, info1.Code);
            f.ShowDialog();

            this.DataDraw();
        }

        private void FormVital_Resize(object sender, EventArgs e)
        {
            DataPanel1.Height = this.Height - DataPanel1.Location.Y - 50;
        }

        private void ChartDate1_ValueChanged(object sender, EventArgs e)
        {
            this.DataDraw();
        }

        private void ShowButton1_Click(object sender, EventArgs e)
        {
            this.DataDraw();
        }


        class ChartSize
        {
            public enum ChartMode : int
            {
                Normal = 1,
                Middle = 2,
                Heavy = 3
            }

            public ChartMode Mode = ChartMode.Normal;

            /// <summary>
            /// 項目名の高さ
            /// </summary>
            public int H1
            {
                get
                {
                    if (this.Mode == ChartMode.Normal)
                    {
                        return 40;
                    }
                    else if (this.Mode == ChartMode.Middle)
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
            public int H_Day
            {
                get
                {
                    return 30;
                }
            }

            /// <summary>
            /// グラフの目盛り線の高さ
            /// </summary>
            public int H_Grid
            {
                get
                {
                    return 80;
                }
            }

            /// <summary>
            /// 項目名の幅
            /// </summary>
            public int W1
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
                    return 144;
                }
            }

            /// <summary>
            /// 1時間あたりの幅
            /// </summary>
            public int W_Hour
            {
                get
                {
                    return 6;
                }
            }

            /// <summary>
            /// 10分あたりの幅
            /// </summary>
            public int W_Min10
            {
                get
                {
                    return 1;
                }
            }

            /// <summary>
            /// 1分あたりの幅
            /// </summary>
            public float W_Min
            {
                get
                {
                    return 0.1F;
                }
            }
        }

        /// <summary>
        /// 日付・項目のラベルの属性
        /// </summary>
        class LabelInfo
        {
            public string Code = "";
            public string DateString = "";
        }
    }
}
