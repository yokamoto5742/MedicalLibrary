using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;
using iTextSharp.text.pdf;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class OpeOrderDayListForm : Form
    {
        OpeOrderSettings Settings = new OpeOrderSettings();

        DataSet DSet = new DataSet();

        PrintDialog printDialog1 = new PrintDialog();
        PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
        PrintDocument printDocument1 = new PrintDocument();
        ContextMenuStrip contextMenuStrip1 = new ContextMenuStrip();

        Font f14 = new Font("", 14);
        Font f12 = new Font("", 12);
        Font f11 = new Font("", 11);
        Font f10 = new Font("", 10);
        Font f9 = new Font("", 9);
        Font f8 = new Font("", 8);

        int StartX4 = 30;
        int StartY4 = 20;
        int Height4 = 25;
        int StartX41 = 30;
        int StartX42 = 280;
        int StartY42 = 220;
        int printCounter = 0;

        List<PatOpeOrder2> printList;
        List<string> printCheckedList;

        /// <summary>
        /// 現在開いている手術指示
        /// </summary>
        PatOpeOrder2 opeOrder = new PatOpeOrder2();

        const int SHOW_DAYS = 2;

        public OpeOrderDayListForm()
        {
            InitializeComponent();

            // 時間計算
            this.StartTimeBox.Leave += new EventHandler(CalcTime);
            this.EndTimeBox.Leave += new EventHandler(CalcTime);
            this.MinutesBox2.Leave += new EventHandler(CalcTime);
        }

        private void OpeOrderDayListForm_Load(object sender, EventArgs e)
        {
            // プログラムで一度も実行されていなければ実行する
            LibSettings.Init();

            this.Settings.Init();

            // ログインしているかチェック
            LoginUser.Init();

            if (LoginUser.Id.Length == 0)
            {
                MessageBox.Show("ログインされていません");
                this.Dispose();
            }

            this.Text += "（" + LoginUser.Name + "　ログイン中）";

            this.Width = Screen.PrimaryScreen.WorkingArea.Width - 200;

            if (this.Width > 1700)
            {
                this.Width = 1700;
            }

            this.Location = new Point(50, this.Location.Y);

            this.DatePicker2.Value = this.DatePicker1.Value.AddDays(SHOW_DAYS - 1);

            ListShow();

            PlaceFilterBox.Items.Add("");
            PlaceFilterBox.Items.Add("中央");
            PlaceFilterBox.Items.Add("南館");

            if (this.Settings.PCPlace == OpeOrderSettings.Place.Hon)
            {
                PlaceFilterBox.Text = "中央";
            }
            else if (this.Settings.PCPlace == OpeOrderSettings.Place.Minami)
            {
                PlaceFilterBox.Text = "南館";
            }

            InOutFilterBox.Items.Add("");
            InOutFilterBox.Items.Add("外来");
            InOutFilterBox.Items.Add("入院");

            OpeRoomBox.Items.Add("");
            OpeRoomFilterBox.Items.Add("");

            foreach (string k in PatOpeOrder2.OpeRoomDict.Keys)
            {
                OpeRoomBox.Items.Add(k + " " + PatOpeOrder2.OpeRoomDict[k]);
                OpeRoomFilterBox.Items.Add(k + " " + PatOpeOrder2.OpeRoomDict[k]);
            }

            DeptBox.Items.Add("");

            foreach (string k in Dict.DeptDict.Keys)
            {
                if (!k.Equals("0"))
                {
                    DeptBox.Items.Add(k + " " + Dict.DeptDict[k].ShortName);
                }
            }

            foreach (string s in this.Settings.DoctorList3)
            {
                DoctorBox3.Items.Add(s);
            }

            foreach (string s in this.Settings.NsList1)
            {
                NsBox1.Items.Add(s);
            }

            foreach (string s in this.Settings.NsList2)
            {
                NsBox2.Items.Add(s);
            }

            foreach (string s in this.Settings.NsList4)
            {
                NsBox4.Items.Add(s);
            }

            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

            ToolStripMenuItem item1 = new ToolStripMenuItem();
            item1.Text = "ラベル発行画面";
            item1.Click += new EventHandler(ItemLabelPrint_Click);
            contextMenuStrip1.Items.Add(item1);

            if (!this.Settings.StaffCodeList.Contains(LoginUser.Id) && !LoginUser.Id.Equals("519"))
            {
                RegButton.Enabled = false;
            }
        }

        DataTable TableInit(int i)
        {
            DataTable table;

            if (DSet.Tables.Contains("T" + i))
            {
                table = DSet.Tables["T" + i];
                table.Clear();
            }
            else
            {
                table = DSet.Tables.Add("T" + i);

                if (this.Settings.PCPlace == OpeOrderSettings.Place.Minami)
                {
                    table.Columns.Add("ID", typeof(int));
                    table.Columns.Add("氏名");
                    table.Columns.Add("年齢");
                    table.Columns.Add("性別");
                    table.Columns.Add("病棟");
                    table.Columns.Add("病室");
                    table.Columns.Add("科コード");
                    table.Columns.Add("科");
                    table.Columns.Add("主治医");
                    table.Columns.Add("場所");
                    table.Columns.Add("入外");
                    table.Columns.Add("術者");
                    table.Columns.Add("内容");
                    table.Columns.Add("室コード");
                    table.Columns.Add("室");
                    table.Columns.Add("入室");
                    table.Columns.Add("退室");
                    table.Columns.Add("所要分");
                    table.Columns.Add("TF");
                    table.Columns.Add("緊急");
                    table.Columns.Add("感染");
                    table.Columns.Add("感染フラグ");
                    table.Columns.Add("術式");
                    table.Columns.Add("部位");
                    table.Columns.Add("麻酔");
                    table.Columns.Add("印刷済コード");
                    table.Columns.Add("印刷済");
                    table.Columns.Add("印刷", typeof(bool));
                    table.Columns.Add("送信済コード");
                    table.Columns.Add("送信済");
                    table.Columns.Add("送信", typeof(bool));
                    table.Columns.Add("ラベル済コード");
                    table.Columns.Add("ラベル済");
                    table.Columns.Add("ラベル", typeof(bool));
                    table.Columns.Add("麻酔医");
                    table.Columns.Add("器械Ns");
                    table.Columns.Add("外回Ns");
                    table.Columns.Add("記録Ns");
                    table.Columns.Add("コメント");
                    table.Columns.Add("ステータス");
                    table.Columns.Add("更新日時");
                    table.Columns.Add("更新者");
                    table.Columns.Add("Obj", typeof(PatOpeOrder2));
                }
                else
                {
                    table.Columns.Add("室コード");
                    table.Columns.Add("室");
                    table.Columns.Add("TF");
                    table.Columns.Add("入室");
                    table.Columns.Add("緊急");
                    table.Columns.Add("入外");
                    table.Columns.Add("科コード");
                    table.Columns.Add("科");
                    table.Columns.Add("所要分");
                    table.Columns.Add("退室");
                    table.Columns.Add("ID", typeof(int));
                    table.Columns.Add("氏名");
                    table.Columns.Add("年齢");
                    table.Columns.Add("性別");
                    table.Columns.Add("病棟");
                    table.Columns.Add("病室");
                    table.Columns.Add("主治医");
                    table.Columns.Add("場所");
                    table.Columns.Add("内容");
                    table.Columns.Add("感染");
                    table.Columns.Add("感染フラグ");
                    table.Columns.Add("術式");
                    table.Columns.Add("部位");
                    table.Columns.Add("麻酔");
                    table.Columns.Add("術者");
                    table.Columns.Add("麻酔医");
                    table.Columns.Add("送信済コード");
                    table.Columns.Add("送信済");
                    table.Columns.Add("送信", typeof(bool));
                    table.Columns.Add("器械Ns");
                    table.Columns.Add("外回Ns");
                    table.Columns.Add("記録Ns");
                    table.Columns.Add("コメント");
                    table.Columns.Add("印刷済コード");
                    table.Columns.Add("印刷済");
                    table.Columns.Add("印刷", typeof(bool));
                    table.Columns.Add("ラベル済コード");
                    table.Columns.Add("ラベル済");
                    table.Columns.Add("ラベル", typeof(bool));
                    table.Columns.Add("ステータス");
                    table.Columns.Add("更新日時");
                    table.Columns.Add("更新者");
                    table.Columns.Add("Obj", typeof(PatOpeOrder2));
                }
            }

            return table;
        }

        void ItemLabelPrint_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            ContextMenuStrip strip = (ContextMenuStrip)(item.Owner);
            DataGridView grid = (DataGridView)(strip.SourceControl);

            if (grid.SelectedCells.Count > 0)
            {
                string pt_id = grid.Rows[grid.SelectedCells[0].RowIndex].Cells["ID"].Value.ToString();
                Launcher.PatLabel(pt_id, grid.Tag.ToString());
            }
        }

        void OpeOrderClear()
        {
            this.opeOrder = new PatOpeOrder2();

            DaysLabel.Text = "";
            OpeDateLabel.Text = "";
            PtIdLabel.Text = "";
            PtNameLabel.Text = "";
            OrderDataBox.Clear();

            OpeRoomBox.Text = "";
            StartTimeBox.Clear();
            EndTimeBox.Clear();
            MinutesBox1.Clear();
            MinutesBox2.Clear();
            PrintBox1.Checked = false;
            PrintBox2.Checked = false;
            SentBox1.Checked = false;
            ContBox.Clear();

            TFBox.Checked = false;
            TimeOutButton0.Checked = true;
            TimeOutButton1.Checked = false;
            TimeOutButton2.Checked = false;
            DeptBox.Text = "";
            InfectionBox.Clear();
            DoctorBox3.Text = "";
            NsBox1.Text = "";
            NsBox2.Text = "";
            NsBox4.Text = "";

            OpeNameBox.Text = "";
            OpePartBox.Text = "";
            OpeAnesBox.Text = "";

            CancelBox.Checked = false;
            SaveLabel.Text = "";
        }

        void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            PatOpeOrder2 order = printList[printCounter];

            e.Graphics.DrawString("印刷日時　：　" + DateTime.Now.ToString("yy/MM/dd HH:mm"), f8, Brushes.Black, 550, 12); ;
            e.Graphics.DrawString("手術指示票", f14, Brushes.Black, 260, 20);

            Barcode128 barcode = new Barcode128();

            barcode.Code = order.Pat.Id;
            barcode.BarHeight = Height4;

            Image img128 = barcode.CreateDrawingImage(Color.Black, Color.White);
            e.Graphics.DrawImage(img128, StartX4 + 30, StartY4);

            e.Graphics.DrawString("ID　" + order.Pat.Id + "　　" + order.Pat.Name + "　様　　" + order.Pat.SexName + "　　" + order.Pat.BirthStringJ + "生　" + order.Pat.AgeCalc(order.Date) + "歳", f12, Brushes.Black, StartX4 + 50, 60);
            e.Graphics.DrawString("手術日： " + DateTimeAgent.DateFormat(order.Date, DateTimeAgent.DateFormatKind.SHORT) + "　　　病棟： " + order.Pat.WardName + "　" + order.Pat.Room + "　　　診療科： " + order.DeptName + "　　　医師： " + order.Pat.DoctorName, f10, Brushes.Black, StartX4 + 50, 90);

            e.Graphics.DrawString("手術室： " + order.OpeRoomName + "　　　入室時刻： " + DateTimeAgent.TimeFormat(order.StartTime, false) + "　　　所要： " + order.Minutes + " 分　　　退室時刻： " + DateTimeAgent.TimeFormat(order.EndTime), f10, Brushes.Black, StartX4 + 50, 120);
            e.Graphics.DrawString("コメント： ", f10, Brushes.Black, StartX4 + 50, 150);
            e.Graphics.DrawString(order.Cont.Replace("\r\n", "　"), f10, Brushes.Black, new Rectangle(StartX4 + 120, 150, 600, 120));

            string code = "";
            string title = "";
            string text = "";
            int tmp_y42 = StartY42;

            foreach (PatOpeOrderData data in order.DictData.Values)
            {
                if (code.Length == 0)
                {
                    code = data.KeyCode.Split('-')[1];
                    title = data.Title;
                    text = data.Text;
                }
                else if (!code.Equals(data.KeyCode.Split('-')[1]))
                {
                    e.Graphics.DrawString(title, f9, Brushes.Black, StartX41, tmp_y42);
                    e.Graphics.DrawString(text, f9, Brushes.Black, StartX42, tmp_y42);

                    tmp_y42 += 22;

                    code = data.KeyCode.Split('-')[1];
                    title = data.Title;
                    text = data.Text;
                }
                else
                {
                    text += "　" + data.Text;
                }
            }

            if (text.Length > 0)
            {
                e.Graphics.DrawString(title, f9, Brushes.Black, StartX41, tmp_y42);
                e.Graphics.DrawString(text, f9, Brushes.Black, StartX42, tmp_y42);
            }

            if (printCounter < printList.Count - 1)
            {
                e.HasMorePages = true;
                printCounter++;
            }
            else
            {
                e.HasMorePages = false;
            }
        }

        void ListShow()
        {
            OpeOrderClear();
            ListPanel.Controls.Clear();

            int d1 = int.Parse(DatePicker1.Value.ToString("yyyyMMdd"));
            int d2 = int.Parse(DatePicker2.Value.ToString("yyyyMMdd"));

            List<PatOpeOrder2> list = PatOpeOrder2.Load(d1, d2);

            int tmp_x = 2;
            int tmp_y = 2;
            int tmp_date = int.Parse(DatePicker1.Value.ToString("yyyyMMdd"));
            Size panel_size = new Size(ListPanel.Width - 25, 320);
            Size button_size = new Size(60, 20);
            Size grid_size = new Size(panel_size.Width - 10, 290);
            Font grid_font = new Font("ＭＳ Ｐゴシック", 8);

            DataTable table;

            for (int i = 0; ; i++)
            {
                // 31日を超えたら終了
                if (i > 31)
                {
                    break;
                }

                tmp_date = int.Parse(DatePicker1.Value.AddDays(i).ToString("yyyyMMdd"));

                // 対象期間を過ぎたら終了
                if (tmp_date > d2)
                {
                    break;
                }

                table = TableInit(i);

                Panel panel = new Panel();
                panel.Name = "P" + i;
                panel.Tag = i;
                panel.Location = new Point(tmp_x, tmp_y);
                panel.Size = panel_size;
                panel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

                Label label = new Label();
                label.Text = DatePicker1.Value.AddDays(i).ToString("M/d (ddd)");
                label.AutoSize = true;
                label.Location = new Point(5, 5);

                panel.Controls.Add(label);

                Button button1 = new Button();
                button1.BackColor = Color.Beige;
                button1.Tag = tmp_date;
                button1.Text = "指示票";
                button1.Size = button_size;
                button1.Location = new Point(70, 2);
                button1.Click += new EventHandler(PrintButton1_Click);

                panel.Controls.Add(button1);

                Button button2 = new Button();
                button2.BackColor = Color.Bisque;
                button2.Tag = tmp_date;
                button2.Text = "送信";
                button2.Size = button_size;
                button2.Location = new Point(150, 2);
                button2.Click += new EventHandler(SendButton1_Click);

                panel.Controls.Add(button2);

                Button button3 = new Button();
                button3.BackColor = Color.AliceBlue;
                button3.Tag = tmp_date;
                button3.Text = "ラベル";
                button3.Size = button_size;
                button3.Location = new Point(230, 2);
                button3.Click += new EventHandler(PrintButton2_Click);

                panel.Controls.Add(button3);

                DataGridView grid = new DataGridView();
                grid.Name = "G" + i;
                grid.Tag = tmp_date;
                grid.Location = new Point(2, 25);
                grid.Size = grid_size;
                grid.AllowUserToAddRows = false;
                grid.AllowUserToDeleteRows = false;
                grid.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                grid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                grid.DefaultCellStyle.Font = grid_font;
                grid.DefaultCellStyle.SelectionBackColor = Color.LightPink;
                grid.DefaultCellStyle.SelectionForeColor = Color.Black;
//                grid.ReadOnly = true;
                grid.MultiSelect = false;
                grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                grid.RowHeadersVisible = false;
                grid.ContextMenuStrip = contextMenuStrip1;
                grid.CellClick += new DataGridViewCellEventHandler(Grid_CellClick);

                foreach (PatOpeOrder2 order in list)
                {
                    if (order.Date < tmp_date)
                    {
                        continue;
                    }
                    else if (order.Date == tmp_date)
                    {
                        DataRow r = table.NewRow();

                        DataRowSet(r, order);

                        table.Rows.Add(r);
                    }
                    else
                    {
                        break;
                    }
                }

                panel.Controls.Add(grid);
                ListPanel.Controls.Add(panel);

                tmp_y += 325;
            }

            ListFormat();
        }

        void ShowCancelBox_Click(object sender, EventArgs e)
        {
            ListFormat();
        }

        /// <summary>
        /// １日のListを表示する。
        /// </summary>
        /// <param name="date">基準日</param>
        /// <param name="days">基準日からの日数</param>
        void ListUpdate(int date, int days)
        {
            int tmp_date = DateTimeAgent.AddDays(date, days);
            List<PatOpeOrder2> list = PatOpeOrder2.Load(tmp_date);

            // 該当テーブルがなければ終了
            if (!DSet.Tables.Contains("T" + days))
            {
                return;
            }

            // 該当パネルとグリッドがなければ終了
            if (!ListPanel.Controls.ContainsKey("P" + days) ||
                !ListPanel.Controls["P" + days].Controls.ContainsKey("G" + days))
            {
                return;
            }

            DataTable table = DSet.Tables["T" + days];
            table.Clear();

            DataGridView grid = (DataGridView)(ListPanel.Controls["P" + days].Controls["G" + days]);

            foreach (PatOpeOrder2 order in list)
            {
                if (order.Date < tmp_date)
                {
                    continue;
                }
                else if (order.Date == tmp_date)
                {
                    DataRow r = table.NewRow();

                    DataRowSet(r, order);

                    table.Rows.Add(r);
                }
                else
                {
                    break;
                }
            }

            ListFormat();
        }

        /// <summary>
        /// 手術指示一覧の表示をフォーマットする。
        /// </summary>
        void ListFormat()
        {
            string filter = "";

            if (PlaceFilterBox.Text.Length > 0)
            {
                filter = "場所 = '" + PlaceFilterBox.Text + "'";
            }

            if (InOutFilterBox.Text.Length > 0)
            {
                if (filter.Length > 0)
                {
                    filter += " and ";
                }

                filter += "入外 = '" + InOutFilterBox.Text + "'";
            }

            if (OpeRoomFilterBox.Text.Contains(" "))
            {
                if (filter.Length > 0)
                {
                    filter += " and ";
                }

                filter += "室コード = '" + OpeRoomFilterBox.Text.Split(' ')[0] + "'";
            }

            if (!ShowCancelBox.Checked)
            {
                if (filter.Length > 0)
                {
                    filter += " and ";
                }

                filter += "ステータス <> '9'";
            }

            int d1 = int.Parse(DatePicker1.Value.ToString("yyyyMMdd"));
            int d2 = int.Parse(DatePicker2.Value.ToString("yyyyMMdd"));

            for (int i = 0; ; i++)
            {
                // 31日を過ぎたら終了
                if (i > 31)
                {
                    break;
                }

                // 対象期間を過ぎたら終了
                if (int.Parse(DatePicker1.Value.AddDays(i).ToString("yyyyMMdd")) > d2)
                {
                    break;
                }

                // 該当テーブルがなければ終了
                if (!DSet.Tables.Contains("T" + i))
                {
                    break;
                }

                // 該当パネルとグリッドがなければ終了
                if (!ListPanel.Controls.ContainsKey("P" + i) ||
                    !ListPanel.Controls["P" + i].Controls.ContainsKey("G" + i))
                {
                    return;
                }

                DataView view = new DataView(DSet.Tables["T" + i]);
                view.RowFilter = filter;
                view.Sort = "室コード, TF, 入室, 科コード, ID";

                DataGridView grid = (DataGridView)(ListPanel.Controls["P" + i].Controls["G" + i]);
                grid.DataSource = view;

                grid.Columns["ID"].Width = 50;
                grid.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                grid.Columns["ID"].ReadOnly = true;
                grid.Columns["氏名"].Width = 70;
                grid.Columns["氏名"].ReadOnly = true;
                grid.Columns["年齢"].Width = 35;
                grid.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grid.Columns["年齢"].ReadOnly = true;
                grid.Columns["性別"].Width = 35;
                grid.Columns["性別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grid.Columns["性別"].ReadOnly = true;
                grid.Columns["病棟"].Width = 45;
                grid.Columns["病棟"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grid.Columns["病棟"].ReadOnly = true;
                grid.Columns["病室"].Width = 35;
                grid.Columns["病室"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grid.Columns["病室"].ReadOnly = true;
                grid.Columns["科コード"].Visible = false;
                grid.Columns["科"].ReadOnly = true;
                grid.Columns["科"].Width = 40;
                grid.Columns["科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grid.Columns["主治医"].Visible = false;
                grid.Columns["主治医"].ReadOnly = true;
                grid.Columns["場所"].Width = 40;
                grid.Columns["場所"].ReadOnly = true;
                grid.Columns["入外"].Width = 40;
                grid.Columns["入外"].ReadOnly = true;
                grid.Columns["術者"].Width = 75;
                grid.Columns["術者"].ReadOnly = true;
                grid.Columns["内容"].Visible = false;
                grid.Columns["内容"].ReadOnly = true;

                grid.Columns["室コード"].Visible = false;
                grid.Columns["室"].Width = 35;
                grid.Columns["室"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grid.Columns["室"].ReadOnly = true;
                grid.Columns["入室"].Width = 40;
                grid.Columns["入室"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grid.Columns["入室"].ReadOnly = true;
                grid.Columns["退室"].Width = 40;
                grid.Columns["退室"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grid.Columns["退室"].ReadOnly = true;
                grid.Columns["所要分"].HeaderText = "分";
                grid.Columns["所要分"].Width = 35;
                grid.Columns["所要分"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grid.Columns["所要分"].ReadOnly = true;
                grid.Columns["TF"].Width = 30;
                grid.Columns["TF"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grid.Columns["TF"].ReadOnly = true;
                grid.Columns["緊急"].Width = 35;
                grid.Columns["緊急"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grid.Columns["緊急"].ReadOnly = true;
                grid.Columns["感染"].Visible = false;
                grid.Columns["感染フラグ"].HeaderText = "感染";
                grid.Columns["感染フラグ"].Width = 35;
                grid.Columns["感染フラグ"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grid.Columns["感染フラグ"].ReadOnly = true;

                grid.Columns["術式"].Width = 100;
                grid.Columns["術式"].ReadOnly = true;

                grid.Columns["部位"].ReadOnly = true;
                grid.Columns["部位"].Width = 50;

                grid.Columns["麻酔"].ReadOnly = true;
                grid.Columns["麻酔"].Width = 50;

                grid.Columns["印刷済コード"].Visible = false;
                grid.Columns["印刷済"].Width = 35;
                grid.Columns["印刷済"].HeaderText = "印済";
                grid.Columns["印刷済"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grid.Columns["印刷済"].DefaultCellStyle.BackColor = Color.Beige;
                grid.Columns["印刷済"].ReadOnly = true;
                grid.Columns["印刷"].Width = 35;
                grid.Columns["印刷"].DefaultCellStyle.BackColor = Color.Beige;
                grid.Columns["印刷"].ReadOnly = false;

                grid.Columns["送信済コード"].Visible = false;
                grid.Columns["送信済"].Width = 35;
                grid.Columns["送信済"].HeaderText = "送済";
                grid.Columns["送信済"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grid.Columns["送信済"].DefaultCellStyle.BackColor = Color.Bisque;
                grid.Columns["送信済"].ReadOnly = true;
                grid.Columns["送信"].Width = 35;
                grid.Columns["送信"].DefaultCellStyle.BackColor = Color.Bisque;
                grid.Columns["送信"].ReadOnly = false;

                grid.Columns["ラベル済コード"].Visible = false;
                grid.Columns["ラベル済"].Width = 35;
                grid.Columns["ラベル済"].HeaderText = "ラ済";
                grid.Columns["ラベル済"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grid.Columns["ラベル済"].DefaultCellStyle.BackColor = Color.AliceBlue;
                grid.Columns["ラベル済"].ReadOnly = true;
                grid.Columns["ラベル"].Width = 45;
                grid.Columns["ラベル"].DefaultCellStyle.BackColor = Color.AliceBlue;
                grid.Columns["ラベル"].ReadOnly = false;

                grid.Columns["麻酔医"].Width = 60;
                grid.Columns["麻酔医"].ReadOnly = true;
                grid.Columns["器械Ns"].Width = 40;
                grid.Columns["器械Ns"].ReadOnly = true;
                grid.Columns["外回Ns"].Width = 40;
                grid.Columns["外回Ns"].ReadOnly = true;
                grid.Columns["記録Ns"].Width = 40;
                grid.Columns["記録Ns"].ReadOnly = true;

                grid.Columns["コメント"].Width = 40;
                grid.Columns["コメント"].ReadOnly = true;
                grid.Columns["コメント"].Visible = true;

                grid.Columns["ステータス"].Visible = false;
                grid.Columns["更新日時"].Visible = false;

                grid.Columns["更新者"].Width = 50;
                grid.Columns["更新者"].ReadOnly = true;
                grid.Columns["更新者"].Visible = true;

                grid.Columns["Obj"].Visible = false;

                if (this.Settings.PCPlace == OpeOrderSettings.Place.Minami)
                {
                    grid.Columns["病棟"].Visible = true;
                    grid.Columns["病室"].Visible = true;
                    grid.Columns["年齢"].Visible = false;
                    grid.Columns["性別"].Visible = false;
                    grid.Columns["術式"].Visible = false;
                    grid.Columns["麻酔"].Visible = false;
                }
                else
                {
                    grid.Columns["病棟"].Visible = false;
                    grid.Columns["病室"].Visible = false;
                    grid.Columns["年齢"].Visible = true;
                    grid.Columns["性別"].Visible = true;
                    grid.Columns["術式"].Visible = true;
                    grid.Columns["麻酔"].Visible = true;
                }

                Font cancel_font = new Font("ＭＳ Ｐゴシック", 8, FontStyle.Strikeout);

                foreach (DataGridViewRow r in grid.Rows)
                {
                    if (r.Cells["ステータス"].Value.ToString().Equals("9"))
                    {
                        r.DefaultCellStyle.BackColor = Color.Gray;
                        r.DefaultCellStyle.Font = cancel_font;
                    }
                }
            }
        }

        void Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                OpeOrderClear();

                DataGridView v = (DataGridView)sender;
                DataGridViewRow r = v.Rows[e.RowIndex];

                this.opeOrder = (PatOpeOrder2)r.Cells["Obj"].Value;

                DaysLabel.Text = v.Name.Substring(1);
                OpeDateLabel.Text = DateTimeAgent.DateFormat(opeOrder.Date, DateTimeAgent.DateFormatKind.LONG);
                PtIdLabel.Text = opeOrder.Pat.Id;
                PtNameLabel.Text = opeOrder.Pat.Name + " 様 " + opeOrder.Pat.AgeCalc(opeOrder.Date) + " 歳";
                OrderDataBox.Text = opeOrder.DictDataString;

                if (opeOrder.OpeRoom.Length > 0)
                {
                    OpeRoomBox.Text = opeOrder.OpeRoom + " " + opeOrder.OpeRoomName;
                }

                StartTimeBox.Text = opeOrder.StartTimeString;
                EndTimeBox.Text = opeOrder.EndTimeString;

                if (opeOrder.Minutes >= 60)
                {
                    MinutesBox1.Text = (opeOrder.Minutes / 60).ToString();
                    MinutesBox2.Text = (opeOrder.Minutes % 60).ToString();
                }
                else if (opeOrder.Minutes > 0)
                {
                    MinutesBox1.Text = "0";
                    MinutesBox2.Text = opeOrder.Minutes.ToString();
                }

                DeptBox.Text = opeOrder.Dept + " " + opeOrder.DeptName;

                if (opeOrder.TimeFree)
                {
                    TFBox.Checked = true;
                }

                if (opeOrder.TimeOutStringShort.Equals("予約外"))
                {
                    TimeOutButton1.Checked = true;
                }
                else if (opeOrder.TimeOutStringShort.Equals("緊急"))
                {
                    TimeOutButton2.Checked = true;
                }
                else
                {
                    TimeOutButton0.Checked = true;
                }

                InfectionBox.Text = opeOrder.Infection;

                if (opeOrder.Print1.Equals(1))
                {
                    PrintBox1.Checked = true;
                }

                if (opeOrder.Sent1.Equals(1))
                {
                    SentBox1.Checked = true;
                }

                if (opeOrder.Print2.Equals(1))
                {
                    PrintBox2.Checked = true;
                }

                DoctorBox3.Text = opeOrder.Doctor3;
                NsBox1.Text = opeOrder.Ns1;
                NsBox2.Text = opeOrder.Ns2;
                NsBox4.Text = opeOrder.Ns4;
                ContBox.Text = opeOrder.Cont;

                OpeNameBox.Text = opeOrder.OpeName;
                OpePartBox.Text = opeOrder.OpePart;
                OpeAnesBox.Text = opeOrder.OpeAnes;

                if (opeOrder.Status.Equals("9"))
                {
                    CancelBox.Checked = true;
                }
                else
                {
                    CancelBox.Checked = false;
                }

                SaveLabel.Text = opeOrder.SaveDateString + " " + opeOrder.SaveTimeString + " " + opeOrder.StaffName;
            }
        }

        /// <summary>
        /// 指示票印刷ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PrintButton1_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Panel panel = (Panel)(button.Parent);

            printCheckedList = new List<string>();
            DataGridView grid = (DataGridView)(panel.Controls["G" + panel.Tag.ToString()]);

            foreach (DataGridViewRow r in grid.Rows)
            {
                if (r.Cells["印刷"].Value.Equals(true))
                {
                    printCheckedList.Add(r.Cells["ID"].Value.ToString());
                }
            }

            if (printCheckedList.Count == 0)
            {
                MessageBox.Show("印刷対象が一つもありません");
                return;
            }

            printList = PatOpeOrder2.Load((int)(button.Tag), printCheckedList);
            printCounter = 0;

            printDialog1.PrinterSettings = new PrinterSettings();

            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.PrinterSettings = printDialog1.PrinterSettings;

                if (MessageBox.Show("印刷しますか？プレビューしますか？\r\nYes … 印刷する\r\nNo … プレビューする", "確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    printDocument1.Print();

                    // 指示票印刷後にフラグを立てる。
                    PatOpeOrder2.SetFlg(PatOpeOrder2.Flg.Print1, (int)(button.Tag), printCheckedList);
                    ListUpdate(int.Parse(DatePicker1.Value.ToString("yyyyMMdd")), int.Parse(panel.Tag.ToString()));
                }
                else
                {
                    printPreviewDialog1.Document = printDocument1;
                    printPreviewDialog1.ShowDialog();
                }
            }
        }

        /// <summary>
        /// ラベル印刷ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PrintButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ラベルプリンタから患者ラベルを印刷します。よろしいですか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            Button button = (Button)sender;
            Panel panel = (Panel)(button.Parent);

            printCheckedList = new List<string>();
            DataGridView grid = (DataGridView)(panel.Controls["G" + panel.Tag.ToString()]);

            foreach (DataGridViewRow r in grid.Rows)
            {
                if (r.Cells["ラベル"].Value.Equals(true) && File.Exists(AppFile.FilePath("PatLabelLight.exe")))
                {
                    Launcher.PatLabelLight(r.Cells["ID"].Value.ToString(), button.Tag.ToString());
                    printCheckedList.Add(r.Cells["ID"].Value.ToString());
                }
            }

            if (printCheckedList.Count == 0)
            {
                MessageBox.Show("印刷対象が一つもありません");
                return;
            }

            // ラベル印刷後にフラグを立てる。
            PatOpeOrder2.SetFlg(PatOpeOrder2.Flg.Print2, (int)(button.Tag), printCheckedList);
            ListUpdate(int.Parse(DatePicker1.Value.ToString("yyyyMMdd")), int.Parse(panel.Tag.ToString()));
        }

        /// <summary>
        /// 麻酔装置送信ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SendButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("麻酔装置に患者情報を送信します。よろしいですか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            Button button = (Button)sender;
            Panel panel = (Panel)(button.Parent);

            List<string> sendCheckedList = new List<string>();
            DataGridView grid = (DataGridView)(panel.Controls["G" + panel.Tag.ToString()]);

            foreach (DataGridViewRow r in grid.Rows)
            {
                if (r.Cells["送信"].Value.Equals(true))
                {
                    sendCheckedList.Add(r.Cells["ID"].Value.ToString());
                }
            }

            if (sendCheckedList.Count == 0)
            {
                MessageBox.Show("送信対象が一つもありません");
                return;
            }

            List<PatOpeOrder2> sendList = PatOpeOrder2.Load((int)(button.Tag), sendCheckedList);

            Dictionary<string, Dictionary<string, List<BaseInfo>>> dict1 = BaseInfo.GetDict(sendCheckedList);
            Dictionary<string, PatIn> dict2 = PatIn.GetDict(sendCheckedList);
            Dictionary<string, InfectionData> dict3 = InfectionData.GetDict(sendCheckedList);

            string file = LibSettings.Current.AnesDataFolder + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".dat";
//            StreamWriter writer = new StreamWriter(new FileStream(file, FileMode.Create), Encoding.Default);

            TableData data = new TableData();

            foreach (PatOpeOrder2 order in sendList)
            {
                TableDataRecord record = new TableDataRecord();

                record.DataList.Add("");
                record.DataList.Add(order.DeptName);
                record.DataList.Add(order.Pat.Id);
                record.DataList.Add(order.Pat.Name);
                record.DataList.Add(order.Pat.SexName);
                record.DataList.Add(order.Pat.Birth);
                record.DataList.Add("");
                record.DataList.Add("");

                record.DataList.Add(order.OpeName);
                record.DataList.Add("");
                record.DataList.Add("");
                record.DataList.Add("");
                record.DataList.Add("");
                record.DataList.Add("");
                record.DataList.Add("");
                record.DataList.Add("");
                record.DataList.Add("");
                record.DataList.Add("");
                record.DataList.Add("");
                record.DataList.Add("");

                if (dict1.ContainsKey(order.Pat.Id) && dict1[order.Pat.Id].ContainsKey(LibSettings.Current.BaseInfoCodes.Height2))
                {
                    record.DataList.Add(AppString.ZenToHan(dict1[order.Pat.Id][LibSettings.Current.BaseInfoCodes.Height2][0].Value));
                }
                else
                {
                    record.DataList.Add("");
                }

                if (dict1.ContainsKey(order.Pat.Id) && dict1[order.Pat.Id].ContainsKey(LibSettings.Current.BaseInfoCodes.Weight2))
                {
                    record.DataList.Add(AppString.ZenToHan(dict1[order.Pat.Id][LibSettings.Current.BaseInfoCodes.Weight2][0].Value));
                }
                else
                {
                    record.DataList.Add("");
                }

                if (dict2.ContainsKey(order.Pat.Id))
                {
                    record.DataList.Add(dict2[order.Pat.Id].WardName);
                }
                else
                {
                    record.DataList.Add("");
                }

                if (dict3.ContainsKey(order.Pat.Id))
                {
                    string[] inf_data = { "", "", "" };
                    int i = 0;

                    foreach (InfectionDetail infection in dict3[order.Pat.Id].DictData.Values)
                    {
                        inf_data[i] = infection.Name + " " + infection.Data;
                        i++;

                        if (i > 2)
                        {
                            break;
                        }
                    }

                    record.DataList.Add(inf_data[0]);
                    record.DataList.Add(inf_data[1]);
                    record.DataList.Add(inf_data[2]);
                }
                else
                {
                    record.DataList.Add("");
                    record.DataList.Add("");
                    record.DataList.Add("");
                }

                record.DataList.Add("");
                record.DataList.Add("");
                record.DataList.Add("");

                record.DataList.Add(order.OpeDoctor);
                record.DataList.Add("");
                record.DataList.Add("");
                record.DataList.Add("");
                record.DataList.Add("");

                data.RecordList.Add(record);
            }

            if (data.CSVSave(file, false, false, false))
            {
                MessageBox.Show("送信が完了しました");

                // 送信後にフラグを立てる。
                PatOpeOrder2.SetFlg(PatOpeOrder2.Flg.Sent1, (int)(button.Tag), sendCheckedList);
                ListUpdate(int.Parse(DatePicker1.Value.ToString("yyyyMMdd")), int.Parse(panel.Tag.ToString()));
            }
            else
            {
                MessageBox.Show("送信に失敗しました");
            }
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            if (DatePicker1.Value > DatePicker2.Value)
            {
                MessageBox.Show("開始日が終了日より後になっています");
                return;
            }

            if (DatePicker1.Value.AddDays(31) < DatePicker2.Value)
            {
                MessageBox.Show("対象期間が31日を超えることは出来ません");
                return;
            }

            ListShow();
        }

        private void RegButton_Click(object sender, EventArgs e)
        {
            if (opeOrder.Pat.Id.Length == 0 || opeOrder.Date <= 0)
            {
                return;
            }

            if (OpeRoomBox.Text.Contains(" "))
            {
                opeOrder.OpeRoom = OpeRoomBox.Text.Split(' ')[0];
            }

            opeOrder.StartTime = StartTimeBox.ToInt();
            opeOrder.EndTime = EndTimeBox.ToInt();

            bool d_book = false;
            int s_time = 0;
            int e_time = 0;

            // ダブルブッキングのチェック
            foreach (DataRow r1 in DSet.Tables["T" + DaysLabel.Text].Rows)
            {
                // それ自身の場合はチェックから外す
                if (r1["ID"].ToString().Equals(opeOrder.Pat.Id))
                {
                    continue;
                }

                if (!r1["室コード"].ToString().Equals(opeOrder.OpeRoom))
                {
                    continue;
                }

                if (int.TryParse(r1["入室"].ToString().Replace(":", ""), out s_time) && int.TryParse(r1["退室"].ToString().Replace(":", ""), out e_time))
                {
                    if (opeOrder.StartTime >= s_time && opeOrder.StartTime <= e_time)
                    {
                        d_book = true;
                        break;
                    }
                    else if (opeOrder.EndTime >= s_time && opeOrder.EndTime <= e_time)
                    {
                        d_book = true;
                        break;
                    }
                }
            }

            if (d_book)
            {
                MessageBox.Show("時間帯が重なっています。確認してください");
                StartTimeBox.Select();
                return;
            }

            int minutes = 0;

            if (MinutesBox1.Text.Length > 0)
            {
                minutes += int.Parse(MinutesBox1.Text) * 60;
            }

            if (MinutesBox2.Text.Length > 0)
            {
                minutes += int.Parse(MinutesBox2.Text);
            }

            opeOrder.Minutes = minutes;

            if (DeptBox.Text.Contains(" "))
            {
                opeOrder.Dept = DeptBox.Text.Split(' ')[0];
            }
            else
            {
                opeOrder.Dept = "0";
            }

            opeOrder.TimeFree = TFBox.Checked;

            if (TimeOutButton1.Checked)
            {
                opeOrder.TimeOut = 1;
            }
            else if (TimeOutButton2.Checked)
            {
                opeOrder.TimeOut = 2;
            }
            else
            {
                opeOrder.TimeOut = 0;
            }

            opeOrder.Infection = InfectionBox.Text;

            opeOrder.Sent1 = SentBox1.Checked ? 1 : 0;
            opeOrder.Print1 = PrintBox1.Checked ? 1 : 0;
            opeOrder.Print2 = PrintBox2.Checked ? 1 : 0;

            opeOrder.Doctor3 = DoctorBox3.Text;
            opeOrder.Ns1 = NsBox1.Text;
            opeOrder.Ns2 = NsBox2.Text;
            opeOrder.Ns4 = NsBox4.Text;
            opeOrder.Cont = ContBox.Text;

            opeOrder.OpeName = OpeNameBox.Text;
            opeOrder.OpePart = OpePartBox.Text;
            opeOrder.OpeAnes = OpeAnesBox.Text;

            opeOrder.Status = CancelBox.Checked ? "9" : "0";

            opeOrder.SaveDate = DateTime.Now.ToString("yyyyMMdd");
            opeOrder.SaveTime = DateTime.Now.ToString("HHmmss");
            opeOrder.Staff = LoginUser.Id;

            if (MessageBox.Show("登録します。よろしいですか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            StdReturn sr = opeOrder.Save();

            if (sr.ErrExist)
            {
                MessageBox.Show(sr.Err);
            }
            else
            {
                DataRow[] r = DSet.Tables["T" + DaysLabel.Text].Select("ID = '" + opeOrder.Pat.Id + "'");

                DataRowSet(r[0], opeOrder);

                ListFormat();
                OpeOrderClear();
            }
        }

        DataRow DataRowSet(DataRow r, PatOpeOrder2 order)
        {
            r["ID"] = order.Pat.Id;
            r["氏名"] = order.Pat.Name;
            r["年齢"] = order.Pat.AgeCalc(order.Date);
            r["性別"] = order.Pat.SexName;
            r["病棟"] = order.Pat.WardName;
            r["病室"] = order.Pat.Room;
            r["科コード"] = order.Dept;
            r["科"] = order.DeptName;
            r["主治医"] = order.Pat.DoctorName;

            r["場所"] = order.OpePlace;
            r["入外"] = order.InOutString;
            r["術者"] = order.OpeDoctor;

            r["内容"] = order.DictDataString;

            r["室コード"] = order.OpeRoom;
            r["室"] = order.OpeRoomName;
            r["入室"] = order.StartTimeString;
            r["退室"] = order.EndTimeString;

            if (order.Minutes > 0)
            {
                r["所要分"] = order.Minutes;
            }

            r["TF"] = order.TimeFreeMark;
            r["緊急"] = order.TimeOutStringShort;
            r["感染"] = order.Infection;
            r["感染フラグ"] = order.InfectionFlg;

            r["術式"] = order.OpeName;
            r["部位"] = order.OpePart;
            r["麻酔"] = order.OpeAnes;

            r["印刷済コード"] = order.Print1;
            r["印刷済"] = order.PrintMark1;

            // 南館のみ
            if (!order.IsCanceled && !order.Print1.Equals(1) && order.OpePlace.Equals("南館"))
            {
                r["印刷"] = true;
            }
            else
            {
                r["印刷"] = false;
            }

            r["送信済コード"] = order.Sent1;
            r["送信済"] = order.SentMark1;

            // 中央のみ
            if (!order.IsCanceled && !order.Sent1.Equals(1) && order.OpePlace.Equals("中央"))
            {
                r["送信"] = true;
            }
            else
            {
                r["送信"] = false;
            }

            r["ラベル済コード"] = order.Print2;
            r["ラベル済"] = order.PrintMark2;

            // 外来のみ
            if (!order.IsCanceled && !order.Print2.Equals(1) && order.InOut.Equals("1"))
            {
                r["ラベル"] = true;
            }
            else
            {
                r["ラベル"] = false;
            }

            r["麻酔医"] = order.Doctor3;
            r["器械Ns"] = order.Ns1;
            r["外回Ns"] = order.Ns2;
            r["記録Ns"] = order.Ns4;
            r["コメント"] = order.Cont;

            r["ステータス"] = order.Status;
            r["更新日時"] = order.SaveDateString + " " + order.SaveTimeString;
            r["更新者"] = order.StaffName;

            r["Obj"] = order;

            return r;
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("クリアしますか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                OpeOrderClear();
            }
        }

        void CalcTime(object sender, EventArgs e)
        {
            if (StartTimeBox.Text.Length > 0 && EndTimeBox.Text.Length > 0)
            {
                int minutes = DateTimeAgent.IntervalMinutes(StartTimeBox.ToInt(), EndTimeBox.ToInt());

                MinutesBox1.Text = (minutes / 60).ToString();
                MinutesBox2.Text = (minutes % 60).ToString();
            }
            else if (StartTimeBox.Text.Length > 0 && MinutesBox2.Text.Length > 0)
            {
                /*
                if (EndTimeBox.Text.Length > 0 && MessageBox.Show("既に退室時間が入力されています。再計算しますか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }
                 */

                if (MinutesBox1.Text.Length == 0)
                {
                    MinutesBox1.Text = "0";
                }

                int start_time = StartTimeBox.ToInt();
                int minutes = int.Parse(MinutesBox1.Text) * 60 + int.Parse(MinutesBox2.Text);

                EndTimeBox.FromInt(DateTimeAgent.AddTime(start_time, minutes));
            }
            else
            {
                if (MinutesBox1.Text.Length == 0)
                {
                    MinutesBox1.Text = "0";
                }

                if (MinutesBox2.Text.Length == 0)
                {
                    MinutesBox2.Text = "0";
                }
            }
        }

        private void PlaceFilterBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListFormat();
        }

        private void InOutFilterBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListFormat();
        }

        private void OpeRoomFilterBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListFormat();
        }

        private void ShowCancelBox_CheckedChanged(object sender, EventArgs e)
        {
            ListFormat();
        }

        private void AnesRecordButton_Click(object sender, EventArgs e)
        {
            Launcher.MedicalAgent("-anes");
        }

        private void ExcelPlanButton_Click(object sender, EventArgs e)
        {
            if (DatePicker1.Value > DatePicker2.Value)
            {
                MessageBox.Show("開始日が終了日より後になっています");
                return;
            }

            if (DatePicker1.Value.AddDays(31) < DatePicker2.Value)
            {
                MessageBox.Show("対象期間が31日を超えることは出来ません");
                return;
            }

            string file = AppFile.FilePath(Settings.ExcelTerm);

            if (!File.Exists(file))
            {
                MessageBox.Show(Settings.ExcelTerm + " が存在しません");
                return;
            }

            OpeOrderExcelPlan.ExcelOpen(file, PatOpeOrder2.Load(int.Parse(DatePicker1.Value.ToString("yyyyMMdd")), int.Parse(DatePicker2.Value.ToString("yyyyMMdd"))));
        }

        private void InfectionButton_Click(object sender, EventArgs e)
        {
            if (PtIdLabel.Text.Length > 0 && MessageBox.Show("電子カルテから感染症データを取り込みますか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                InfectionBox.Text = InfectionData.GetInfectionData(PtIdLabel.Text).ResultString;
            }
        }
    }
}