using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using MedicalLibrary.Boundary;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class PatOrderSheetForm : Form
    {
        PatOrderSettings Settings = new PatOrderSettings();

        PatBase _Pat = new PatBase();

        PatBase Pat
        {
            get
            {
                if (!this._Pat.Id.Equals(this.PtIdBox.Text))
                {
                    this._Pat = PatBase.Load(this.PtIdBox.Text);
                }

                return this._Pat;
            }
        }

        /// <summary>
        /// 印刷するデータ
        /// </summary>
        OrderPrint Order1 = new OrderPrint();

        /// <summary>
        /// 現在、印刷中のページ番号
        /// </summary>
        int PageNum = 1;

        DataSet DSet = new DataSet();

        Font f30 = new Font("", 30);
        Font f20 = new Font("", 20);
        Font f16 = new Font("", 16);
        Font f14 = new Font("", 14);
        Font f12 = new Font("", 12);
        Font f10 = new Font("", 10);
        Font f9 = new Font("", 9);
        Font ft9 = new Font("ＭＳ ゴシック", 9);
        Font f8 = new Font("", 8);

        enum LIST_SHOW : int
        {
            NO = 0,
            YES = 1
        }

        public PatOrderSheetForm()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WinAPI.WM_COPYDATA)
            {
                // 文字列が送信されて来た
                WinAPI.COPYDATASTRUCT mystr = new WinAPI.COPYDATASTRUCT();
                Type mytype = mystr.GetType();
                mystr = (WinAPI.COPYDATASTRUCT)m.GetLParam(mytype);

                if (mystr.lpData.Split(' ').Length > 0)
                {
                    this.InitShow(mystr.lpData.Split(' '));
                }
            }

            base.WndProc(ref m);
        }

        private void PatOrderSheetForm_Load(object sender, EventArgs e)
        {
            // プログラムで一度も実行されていなければ実行する
            LibSettings.Init();

            this.Settings.Init();

            DataTable tmpTable = DSet.Tables.Add("Order");
            tmpTable.Columns.Add("印刷", typeof(bool));
            tmpTable.Columns.Add("日付");
            tmpTable.Columns.Add("診療区分");
            tmpTable.Columns.Add("診療");
            tmpTable.Columns.Add("施行部署コード");
            tmpTable.Columns.Add("施行部署");
            tmpTable.Columns.Add("施行済");
            tmpTable.Columns.Add("入外区分");
            tmpTable.Columns.Add("入外");
            tmpTable.Columns.Add("科コード");
            tmpTable.Columns.Add("科");
            tmpTable.Columns.Add("指示医コード");
            tmpTable.Columns.Add("医師");
            tmpTable.Columns.Add("指示日");
            tmpTable.Columns.Add("予約種別");
            tmpTable.Columns.Add("予約日");
            tmpTable.Columns.Add("予約時間");
            tmpTable.Columns.Add("内容");

            OrderView.DataSource = new DataView(tmpTable);

            tmpTable = DSet.Tables.Add("List");
            tmpTable.Columns.Add("受付");
            tmpTable.Columns.Add("施行部署コード");
            tmpTable.Columns.Add("施行部署");
            tmpTable.Columns.Add("ID");
            tmpTable.Columns.Add("カナ");
            tmpTable.Columns.Add("氏名");
            tmpTable.Columns.Add("施行済");
            tmpTable.Columns.Add("入外区分");
            tmpTable.Columns.Add("入外");
            tmpTable.Columns.Add("科コード");
            tmpTable.Columns.Add("科");
            tmpTable.Columns.Add("指示医コード");
            tmpTable.Columns.Add("医師");

            ListView.DataSource = new DataView(tmpTable);

            foreach (string key in this.Settings.ShinkuDict.Keys)
            {
                string value = this.Settings.ShinkuDict[key];

                CheckBox tmpBox = new CheckBox();
                tmpBox.AutoSize = true;
                tmpBox.Name = value;
                tmpBox.Text = value;
                tmpBox.Tag = key;
                tmpBox.CheckStateChanged += new EventHandler(DeptShinkuBox_CheckStateChanged);

                // デフォルトで全てチェック 2018/10/02
                tmpBox.Checked = true;

                /*
                if (PODict.PcShinkuList.Contains(key))
                {
                    tmpBox.Checked = true;
                }
                 */

                ShinkuPanel.Controls.Add(tmpBox);
            }

            foreach (string key in this.Settings.DeptDict.Keys)
            {
                string value = this.Settings.DeptDict[key];

                CheckBox tmpBox = new CheckBox();
                tmpBox.AutoSize = true;
                tmpBox.Name = value;
                tmpBox.Text = value;
                tmpBox.Tag = key;
                tmpBox.CheckStateChanged += new EventHandler(DeptShinkuBox_CheckStateChanged);

                // デフォルトで全てチェック 2018/10/02
                tmpBox.Checked = true;

                /*
                if (PODict.PcDeptList.Contains(key))
                {
                    tmpBox.Checked = true;
                }
                 */

                DeptPanel.Controls.Add(tmpBox);
            }

            foreach (string key in this.Settings.SekouDict.Keys)
            {
                string value = this.Settings.SekouDict[key];

                CheckBox tmpBox = new CheckBox();
                tmpBox.AutoSize = true;
                tmpBox.Name = value;
                tmpBox.Text = value;
                tmpBox.Tag = key;
                tmpBox.CheckStateChanged += new EventHandler(SekouBox_CheckStateChanged);

                if (this.Settings.PcSekouList.Contains(key))
                {
                    tmpBox.Checked = true;
                }

                SekouPanel.Controls.Add(tmpBox);
            }

            // デフォルトで入外ともにチェック 2018/10/02
            InOutBox1.Checked = true;
            InOutBox2.Checked = true;

            /*
            if (PODict.PcInOutList.Contains("1"))
            {
                InOutBox1.Checked = true;
            }

            if (PODict.PcInOutList.Contains("2"))
            {
                InOutBox2.Checked = true;
            }
             */

            // デフォルトで日付未定をチェック 2018/10/02
            MiteiBox.Checked = true;

            this.ModeChange(LIST_SHOW.NO);
        }

        private void PatOrderSheetForm_Shown(object sender, EventArgs e)
        {
            PtIdBox.Focus();
        }

        void InitShow(string[] args = null)
        {
            int pat_id = 0;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Equals("-p", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (i < args.Length - 1 && args[i].Length < 9 && int.TryParse(args[i + 1], out pat_id))
                    {
                        this.PtIdBox.Text = args[i + 1];
                        i++;
                    }
                }
            }

            if (this.Pat.Id.Length > 0)
            {
                this.OrderShow();
            }
        }

        private void SekouDate_ValueChanged(object sender, EventArgs e)
        {
            this.OrderShow();
        }

        private void MiteiBox_CheckedChanged(object sender, EventArgs e)
        {
            this.OrderShow();
        }

        private void InOutBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.OrderShow();
        }

        private void InOutBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.OrderShow();
        }

        void DeptShinkuBox_CheckStateChanged(object sender, EventArgs e)
        {
            this.OrderFormat();
        }

        void SekouBox_CheckStateChanged(object sender, EventArgs e)
        {
//            this.ListViewShow();
        }

        /// <summary>
        /// すべてクリアする。
        /// </summary>
        void AllClear()
        {
            PtIdBox.Clear();
            Clear();

            PtIdBox.Focus();
        }

        /// <summary>
        /// 患者ID以外をクリアする。
        /// </summary>
        void Clear()
        {
            PtInfoLabel.Text = "";
            SEQLabel.Text = "";

            DataTable tmpTable = DSet.Tables["Order"];
            tmpTable.Clear();

            OrderView.DataSource = new DataView(tmpTable);
            OrderFormat();
        }

        /// <summary>
        /// オーダー内容を表示する。
        /// </summary>
        void OrderShow()
        {
            this.Clear();

            int p = 0;

            if (PtIdBox.Text.Length == 0 || !int.TryParse(PtIdBox.Text, out p))
            {
                this.PtIdBox.Clear();
                this.PtIdBox.Focus();
                return;
            }

//            PtInfoLabel.Text = Pat.Name + "（" + Pat.Kana + "）様　" + Pat.SexNameShort + "　" + this.Pat.BirthString + "生　" + DateTimeAgent.AgeCalc(Pat.Birth, DateTime.Now.ToString("yyyyMMdd")) + "歳";
            PtInfoLabel.Text = Pat.GetInfo1(SekouDate.Value.ToString("yyyyMMdd"));

            List<PatOut> tmpOutList = PatOut.GetOnedayLast(Pat.Id, SekouDate.Value.ToString("yyyyMMdd"));
            string seq1 = "";

            foreach (PatOut tmpPat in tmpOutList)
            {
                if (seq1.Length == 0)
                {
                    seq1 = tmpPat.Seq1 + "   " + tmpPat.TimeString1 + "   " + Dict.DeptDict[tmpPat.Dept].ShortName;
                }
                else
                {
                    seq1 += ", " + Dict.DeptDict[tmpPat.Dept].ShortName;
                }
            }

            SEQLabel.Text = seq1;

            List<string> shinku_list = new List<string>();

            foreach (string key in this.Settings.ShinkuDict.Keys)
            {
                shinku_list.Add(key);
            }

            if (shinku_list.Count == 0)
            {
                MessageBox.Show("XMLファイルに診療区分が登録されていないため検索できません");
                return;
            }

            string in_out = "";

            if (InOutBox1.Checked && !InOutBox2.Checked)
            {
                in_out = "1";
            }
            else if (!InOutBox1.Checked && InOutBox2.Checked)
            {
                in_out = "2";
            }
            else if (!InOutBox1.Checked && !InOutBox2.Checked)
            {
                MessageBox.Show("入外区分を選択してください");
                return;
            }

            List<PatOrderSheet> list = new List<PatOrderSheet>();

            List<string> dept_list = new List<string>();
            List<string> sekou1_list = new List<string>();

            // オーダー情報を取得する
#if INNO
            List<PatOrder> order_list = PatOrder.GetListByPatSekouDates(PtIdBox.Text, SekouDate.Value.ToString("yyyyMMdd"), SekouDate.Value.ToString("yyyyMMdd"), in_out, shinku_list, dept_list, sekou1_list, this.MiteiBox.Checked, true);
#else
            List<PatOrder> order_list = PatOrder.GetListByPatSekouDates(PtIdBox.Text, SekouDate.Value.ToString("yyyyMMdd"), SekouDate.Value.ToString("yyyyMMdd"), in_out, shinku_list, dept_list, sekou1_list, this.MiteiBox.Checked, false);
#endif

            foreach (PatOrder order in order_list)
            {
                PatOrderSheet os = new PatOrderSheet();
                os.Order = order;

                list.Add(os);
            }

            // 患者ID, 日付、予約時刻, 診療区分, 施行部署, オーダー番号 の順に並び替える
            list.Sort((x, y) =>
            {
                int i = 0;

                try
                {
                    int x1 = 0;
                    int y1 = 0;

                    int.TryParse(x.Order.Pat.Id, out x1);
                    int.TryParse(y.Order.Pat.Id, out y1);
                    i = x1 - y1;

                    // 日付
                    if (i.Equals(0))
                    {
                        int.TryParse(x.Order.SekouDate, out x1);
                        int.TryParse(y.Order.SekouDate, out y1);
                        i = x1 - y1;
                    }

                    // 診療区分
                    if (i.Equals(0))
                    {
                        int.TryParse(x.Order.Shinku, out x1);
                        int.TryParse(y.Order.Shinku, out y1);
                        i = x1 - y1;
                    }

                    // 施行部署
                    // 空の場合は後になる
                    if (i.Equals(0))
                    {
                        if (!int.TryParse(x.Order.Sekou1, out x1)) x1 = 0;
                        if (!int.TryParse(y.Order.Sekou1, out y1)) y1 = 0;
                        if (x1.Equals(0)) x1 = 9999;
                        if (y1.Equals(0)) y1 = 9999;
                        i = x1 - y1;
                    }

                    // オーダー番号
                    if (i.Equals(0))
                    {
                        int.TryParse(x.Order.OrderId, out x1);
                        int.TryParse(y.Order.OrderId, out y1);
                        i = x1 - y1;
                    }
                }
                catch (Exception ex)
                {
                    LibUtility.Except(ex, false);
                }

                return i;
            });

            DataTable tmpTable = DSet.Tables["Order"];

            foreach (PatOrderSheet os in list)
            {
                DataRow r = tmpTable.NewRow();

                r["印刷"] = true;
                r["日付"] = os.Order.SekouDate.Equals("99999999") ? "未定" :  os.Order.SekouDateStringShort;
                r["診療区分"] = os.Order.Shinku;
                r["診療"] = this.Settings.ShinkuDict.ContainsKey(os.Order.Shinku) ? this.Settings.ShinkuDict[os.Order.Shinku] : "";
                r["施行部署コード"] = os.Order.Sekou1;
                r["施行部署"] = Dict.SekouDict.ContainsKey(os.Order.Sekou1) ? Dict.SekouDict[os.Order.Sekou1].ShortName : "";
                r["施行済"] = os.Order.SekouFlg.Equals("1") ? "済" : "";
                r["入外区分"] = os.Order.InOut;
                r["入外"] = os.Order.InOutNameShort;
                r["科コード"] = os.Order.Dept;
                r["科"] = this.Settings.DeptDict.ContainsKey(os.Order.Dept) ? this.Settings.DeptDict[os.Order.Dept] : "";
                r["指示医コード"] = os.Order.Doctor;
                r["医師"] = Dict.DoctorDict.ContainsKey(os.Order.Doctor) ? Dict.DoctorDict[os.Order.Doctor].Name : "";
                r["指示日"] = os.Order.OrderDateStringShort;
                r["予約種別"] = os.RsvName;
                r["予約日"] = os.Order.RsvDateStringShort;
                r["予約時間"] = os.Order.RsvTimeString;
                r["内容"] = os.Order.SOAP;

                tmpTable.Rows.Add(r);
            }

            OrderView.DataSource = new DataView(tmpTable);
            OrderFormat();
        }

        /// <summary>
        /// OrderView のフィルタや幅を整える。
        /// </summary>
        void OrderFormat()
        {
            string dept_filter = "";
            string shinku_filter = "";

            foreach (Control c in DeptPanel.Controls)
            {
                if (c.GetType().Name.Equals("CheckBox") && ((CheckBox)c).Checked)
                {
                    if (dept_filter.Length > 0)
                    {
                        dept_filter += " or ";
                    }

                    dept_filter += "科コード = '" + c.Tag.ToString() + "'";
                }
            }

            if (dept_filter.Length == 0)
            {
                dept_filter = "科コード = 'a'";
            }

            foreach (Control c in ShinkuPanel.Controls)
            {
                if (c.GetType().Name.Equals("CheckBox") && ((CheckBox)c).Checked)
                {
                    if (shinku_filter.Length > 0)
                    {
                        shinku_filter += " or ";
                    }

                    shinku_filter += "診療区分 = '" + c.Tag.ToString() + "'";
                }
            }

            if (shinku_filter.Length == 0)
            {
                shinku_filter = "診療区分 = 'a'";
            }

            DataView tmpView = new DataView(DSet.Tables["Order"]);
            tmpView.RowFilter = "(" + dept_filter + ") and (" + shinku_filter + ")";

            OrderView.DataSource = tmpView;

            OrderView.Columns["印刷"].Width = 30;

            OrderView.Columns["日付"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OrderView.Columns["日付"].Width = 55;

            OrderView.Columns["診療区分"].Visible = false;

            OrderView.Columns["診療"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OrderView.Columns["診療"].Width = 35;

            OrderView.Columns["施行部署コード"].Visible = false;

            OrderView.Columns["施行部署"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OrderView.Columns["施行部署"].Width = 50;
            OrderView.Columns["施行部署"].HeaderText = "種別";

            OrderView.Columns["施行済"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OrderView.Columns["施行済"].Width = 25;
            OrderView.Columns["施行済"].HeaderText = "済";

            OrderView.Columns["入外区分"].Visible = false;

            OrderView.Columns["入外"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OrderView.Columns["入外"].Width = 35;

            OrderView.Columns["科コード"].Visible = false;

            OrderView.Columns["科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OrderView.Columns["科"].Width = 55;

            OrderView.Columns["指示医コード"].Visible = false;

            OrderView.Columns["医師"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OrderView.Columns["医師"].Width = 65;

            OrderView.Columns["指示日"].Visible = false;

            OrderView.Columns["予約種別"].Visible = false;
            OrderView.Columns["予約日"].Visible = false;
            OrderView.Columns["予約時間"].Visible = false;

            OrderView.Columns["内容"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            OrderView.Columns["内容"].Width = 145;
        }

        private void PtIdBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.OrderShow();
            }
            else if (e.KeyCode == Keys.F3)
            {
                this._Pat = FormFindPat.FindPat();
                this.PtIdBox.Text = this._Pat.Id;

                this.OrderShow();
            }
        }

        private void PtIdBox_Click(object sender, EventArgs e)
        {
            this.AllClear();
        }

        private void PatOrderSheetForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                this.AllClear();
                this.PtIdBox.Focus();
            }
            else if (e.KeyCode == Keys.F8)
            {
                this.LabelPrint();
            }
            else if (e.KeyCode == Keys.F9)
            {
                this.Dispose();
            }
        }

        /// <summary>
        /// ラベルを印刷する。
        /// </summary>
        void LabelPrint()
        {
            if (PtIdBox.Text.Length == 0)
            {
                MessageBox.Show("患者IDを入力してください");
                return;
            }

            Order1 = OrderPrint.Make(OrderView, this.Pat.Id, this.SekouDate.Value.ToString("yyyyMMdd"));

            // 選択されたオーダーがない場合
            if (Order1.OrderStrings.Count <= 1)
            {
                if (MessageBox.Show("該当のオーダーがありません。印刷しますか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                {
                    return;
                }
            }

            // XML内に該当PCのプリンタ情報があればそれを使用する。
            // →　通常使うプリンタから印刷 2015/11/06
            /*
            if (PatOrderSettings.Printer.Length > 0)
            {
                printDocument1.PrinterSettings.PrinterName = PatOrderSettings.Printer;
            }
            else
            {
                printDialog1.PrinterSettings = new System.Drawing.Printing.PrinterSettings();

                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                }
            }
            */

            if (this.Settings.Preview)
            {
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
            }
            else
            {
                try
                {
                    printDocument1.DocumentName = PtIdBox.Text + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    printDocument1.Print();
                }
                catch (System.Drawing.Printing.InvalidPrinterException ex)
                {
                    MessageBox.Show(ex.GetBaseException().Message, "印刷エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            this.LabelPrint();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(DateTimeAgent.DateFormat(SekouDate.Value.ToString("yyyyMMdd"), DateTimeAgent.DateFormatKind.J1), f12, Brushes.Black, 30, 25);
            e.Graphics.DrawString("受付番号", f16, Brushes.Black, 30, 60);

            if (SEQLabel.Text.Length > 0)
            {
                e.Graphics.DrawString(SEQLabel.Text.Split(' ')[0], f30, Brushes.Black, 180, 45);
                e.Graphics.DrawString(SEQLabel.Text.Substring(SEQLabel.Text.IndexOf(' ')), f16, Brushes.Black, 280, 60);
            }

            e.Graphics.DrawString("ID　" + PtIdBox.Text, f20, Brushes.Black, 30, 105);

            Barcode128 bar128 = new Barcode128();
            bar128.CodeType = Barcode.CODE128;
            bar128.CodeSet = Barcode128.Barcode128CodeSet.B;
            bar128.Code = this.Pat.Id;
//            bar128.ChecksumText = true;
//            bar128.GenerateChecksum = true;
//            bar128.StartStopText = true;
//            bar128.BarHeight = 35.0F;
//            bar128.Size = 12.0F;

            float bar_width = this.Pat.Id.Length * 35;

            if (bar_width < 100)
            {
                bar_width = 100;
            }
            else if (bar_width > 150)
            {
                bar_width = 150;
            }

            System.Drawing.Image img128 = bar128.CreateDrawingImage(Color.Black, Color.White);
            e.Graphics.DrawImage(img128, 240, 100, bar_width, 35);
/*
            Barcode128 tmpBarcode = new Barcode128();
            tmpBarcode.Draw(Barcode128.CODE.B, PtIdBox.Text, e.Graphics, 240, 100, 35, 1.25F);
*/
			e.Graphics.DrawString(this.Pat.BirthStringJ + "生　" + this.Pat.AgeCalc(this.SekouDate.Value.ToString("yyyyMMdd")) + "歳", f14, Brushes.Black, 520, 110);
			e.Graphics.DrawString(Pat.Name + "（" + Pat.Kana + "）様　　" + Pat.SexNameShort, f20, Brushes.Black, 25, 145);

            if (Order1.StringPages.Count > 1 && PageNum < Order1.StringPages.Count)
            {
                // 複数ページ存在し、かつ、最終ページで無い場合
                e.Graphics.DrawString(Order1.StringPages[PageNum - 1], ft9, Brushes.Black, new RectangleF(20, 180, 750, 955));
                e.Graphics.DrawString(PageNum.ToString() + " / " + Order1.StringPages.Count.ToString() + " ページ", f12, Brushes.Black, 700, 25);

                e.HasMorePages = true;
                PageNum++;
            }
            else
            {
                e.Graphics.DrawString(Order1.StringPages[PageNum - 1], ft9, Brushes.Black, new RectangleF(20, 180, 750, 955));
                e.Graphics.DrawString(PageNum.ToString() + " / " + Order1.StringPages.Count.ToString() + " ページ", f12, Brushes.Black, 700, 25);

                e.HasMorePages = false;
                PageNum = 1;
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void OrderView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                PrintButton.Focus();
            }
        }

        private void ListShowBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ListShowBox.Checked)
            {
                ModeChange(LIST_SHOW.YES);
            }
            else
            {
                ModeChange(LIST_SHOW.NO);
            }
        }

        void ModeChange(LIST_SHOW list_show)
        {
            if (list_show == LIST_SHOW.YES)
            {
                this.Width = 1024;
                this.ListViewShow();
                timer1.Start();
            }
            else
            {
                this.Width = 550;
                timer1.Stop();
            }
        }

        void ListViewShow()
        {
            DataTable tmpTable = DSet.Tables["List"];
            tmpTable.Clear();

            List<string> empty_list = new List<string>();
            List<string> sekou_list = new List<string>();

            foreach (Control c in SekouPanel.Controls)
            {
                if (((CheckBox)c).Checked)
                {
                    sekou_list.Add(c.Tag.ToString());
                }
            }

#if INNO
            List<PatOrder> tmpList = PatOrder.GetListByDate(SekouDate2.Value.ToString("yyyyMMdd"), "", empty_list, empty_list, sekou_list, false, true);
#else
            List<PatOrder> tmpList = PatOrder.GetListByDate(SekouDate2.Value.ToString("yyyyMMdd"), "", empty_list, empty_list, sekou_list, false, false);
#endif

            // 患者ID, 施行済（未施行→施行済）, 施行部署（降順→無）の順に並び替える
            tmpList.Sort((x, y) =>
            {
                int i = 0;

                try
                {
                    int x1 = 0;
                    int y1 = 0;

                    int.TryParse(x.Pat.Id, out x1);
                    int.TryParse(y.Pat.Id, out y1);
                    i = x1 - y1;

                    // 施行済
                    if (i.Equals(0))
                    {
                        int.TryParse(x.SekouFlg, out x1);
                        int.TryParse(y.SekouFlg, out y1);
                        i = x1 - y1;
                    }

                    // 施行部署
                    if (i.Equals(0))
                    {
                        int.TryParse(x.Sekou1, out x1);
                        int.TryParse(y.Sekou1, out y1);

                        if (x1 > 0 && y1 > 0)
                        {
                            // 両方ある場合は降順
                            i = y1 - x1;
                        }
                        else if (x1 > 0)
                        {
                            // y1 = 0 の場合は x が優先
                            i = -1;
                        }
                        else
                        {
                            // x1 = 0 の場合は y が優先
                            i = 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LibUtility.Except(ex, false);
                }

                return i;
            });

            List<string> pt_id_list = new List<string>();

            foreach (PatOrder p in tmpList)
            {
                if (!pt_id_list.Contains(p.Pat.Id))
                {
                    pt_id_list.Add(p.Pat.Id);
                }
            }

            Dictionary<string, string> tmpOutDict = PatOut.GetOnedayLastSeq(pt_id_list, SekouDate.Value.ToString("yyyyMMdd"));

            bool b = false;

            foreach (PatOrder p in tmpList)
            {
                // 施行部署が対象外ならば飛ばす
                if (!this.Settings.SekouDict.ContainsKey(p.Sekou1))
                {
                    continue;
                }

                b = true;

                // 患者IDと施行部署コードが重複するものは飛ばす
                foreach (DataRow rr in tmpTable.Rows)
                {
                    if (rr["ID"].ToString().Equals(p.Pat.Id) &&
                        rr["施行部署コード"].ToString().Equals(p.Sekou1))
                    {
                        b = false;
                        break;
                    }
                }

                if (!b) continue;

                DataRow r = tmpTable.NewRow();

                r["受付"] = tmpOutDict.ContainsKey(p.Pat.Id) ? tmpOutDict[p.Pat.Id] : "";
                r["施行部署コード"] = p.Sekou1;
                r["施行部署"] = this.Settings.SekouDict.ContainsKey(p.Sekou1) ? this.Settings.SekouDict[p.Sekou1] : "";
                r["ID"] = p.Pat.Id;
                r["カナ"] = p.Pat.Kana;
                r["氏名"] = p.Pat.Name;
                r["施行済"] = p.SekouFlg.Equals("1") ? "済" : "";
                r["入外区分"] = p.InOut;
                r["入外"] = p.InOutNameShort;
                r["科コード"] = p.Dept;
                r["科"] = Dict.DeptDict.ContainsKey(p.Dept) ? Dict.DeptDict[p.Dept].ShortName : "";
                r["指示医コード"] = p.Doctor;
                r["医師"] = Dict.DoctorDict.ContainsKey(p.Doctor) ? Dict.DoctorDict[p.Doctor].Name : "";

                tmpTable.Rows.Add(r);
            }

            this.ListViewFormat();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.ListViewShow();
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            this.ListViewShow();
        }

        private void SekouBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SekouBox.Checked)
            {
                SekouBox.Text = "施行済";
                SekouBox.BackColor = Color.AliceBlue;
            }
            else
            {
                SekouBox.Text = "未施行";
                SekouBox.BackColor = Color.White;
            }

            this.ListViewFormat();
        }

        void ListViewFormat()
        {
            string filter = "";

            if (SekouBox.Checked)
            {
                filter = "施行済 = '済'";
                ListView.DefaultCellStyle.BackColor = Color.AliceBlue;
            }
            else
            {
                filter = "施行済 = ''";
                ListView.DefaultCellStyle.BackColor = Color.White;
            }

            DataView tmpView = new DataView(DSet.Tables["List"]);
            tmpView.RowFilter = filter;

            ListView.DataSource = tmpView;

            ListView.Columns["受付"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView.Columns["受付"].Width = 35;

            ListView.Columns["施行部署コード"].Visible = false;
            ListView.Columns["施行部署コード"].Width = 40;

            ListView.Columns["施行部署"].HeaderText = "種別";
            ListView.Columns["施行部署"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView.Columns["施行部署"].Width = 50;

            ListView.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            ListView.Columns["ID"].Width = 50;

            ListView.Columns["カナ"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView.Columns["カナ"].Width = 65;

            ListView.Columns["氏名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView.Columns["氏名"].Width = 75;

            ListView.Columns["施行済"].HeaderText = "済";
            ListView.Columns["施行済"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView.Columns["施行済"].Width = 30;

            ListView.Columns["入外区分"].Visible = false;

            ListView.Columns["入外"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView.Columns["入外"].Width = 30;

            ListView.Columns["科コード"].Visible = false;

            ListView.Columns["科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView.Columns["科"].Width = 50;

            ListView.Columns["指示医コード"].Visible = false;

            ListView.Columns["医師"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView.Columns["医師"].Width = 55;
        }

        private void ListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow r = ListView.Rows[e.RowIndex];

                this.PtIdBox.Text = r.Cells["ID"].Value.ToString();
                this.OrderShow();
            }
        }

        private void ListView_SelectionChanged(object sender, EventArgs e)
        {
            /* 処理が重くなるため除外 09/04/01
            if (ListView.SelectedRows.Count > 0)
            {
                this.PtIdBox.Text = ListView.SelectedRows[0].Cells["ID"].Value.ToString();
                this.OrderShow();
            }
             */
        }
    }

    /// <summary>
    /// 印刷するオーダー文字列に関するクラス
    /// </summary>
    class OrderPrint
    {
        /// <summary>
        /// オーダー単位で区切られた文字列
        /// </summary>
        public List<OrderString> OrderStrings = new List<OrderString>();

        public int LineLength = 57;
        public int PageLines = 75;

        /// <summary>
        /// 文字列をページ単位でリスト化
        /// </summary>
        public List<string> StringPages
        {
            get
            {
                List<string> list = new List<string>();

                // 行番号
                int i = 0;

                // 現在のページの文字列
                string s = "";

                foreach (OrderString os in OrderStrings)
                {
                    // 次のオーダーを印字するとページオーバーする場合
                    if (i + os.StringLines.Count > PageLines)
                    {
                        list.Add((string)s.Clone());
                        s = "";
                        i = 0;
                    }

                    s += os.PrintString;
                    i += os.StringLines.Count;
                }

                if (s.Length > 0)
                {
                    list.Add((string)s.Clone());
                }

                return list;
            }
        }

        public static OrderPrint Make(DataGridView view, string pt_id, string sekou_date)
        {
            OrderPrint obj = new OrderPrint();

            for (int i = 0; i < view.RowCount; i++)
            {
                DataGridViewRow r = view.Rows[i];

                if (r.Cells["印刷"].Value.Equals(true))
                {
                    string s = "";

                    s = "──────────────────────────────\r\n";
                    s += "┌──┐\r\n";
                    s += "│　　│　";

                    if (r.Cells["施行済"].Value.ToString().Equals("済"))
                    {
                        s += "施行済　";
                    }

                    if (r.Cells["施行部署"].Value.ToString().Length > 0)
                    {
                        s += "【" + r.Cells["施行部署"].Value.ToString() + "】　";
                    }

                    s += r.Cells["日付"].Value.ToString() + "　[" + r.Cells["入外"].Value.ToString() + "]　" + r.Cells["科"].Value.ToString() + "　" + r.Cells["医師"].Value.ToString();

                    // 2018/11/28 by sakane
                    // 健診から要望されたが、必要なくなった
                    /*
                    if (r.Cells["予約時間"].Value.ToString().Length > 0)
                    {
                        s += "　[予約] " + r.Cells["予約種別"].Value.ToString() + " " + r.Cells["予約時間"].Value.ToString();
                    }
                    */

                    s += "\r\n";
                    s += "│　　│\r\n";
                    s += "└──┘\r\n";
                    s += r.Cells["内容"].Value.ToString() + "\r\n";

                    obj.OrderStrings.Add(new OrderString((string)(s.Clone())));
                }
            }

            if (obj.OrderStrings.Count == 0)
            {
                // オーダーが無い場合
                obj.OrderStrings.Add(new OrderString("──────────────────────────────\r\n\r\n"));
                obj.OrderStrings.Add(new OrderString("該当のオーダーはありません。\r\n\r\n"));
                obj.OrderStrings.Add(new OrderString("──────────────────────────────\r\n\r\n"));
            }
            else
            {
                // オーダーがある場合
                obj.OrderStrings.Add(new OrderString("──────────────────────────────\r\n\r\n"));
            }

            // 予約を一番下に印字する場合
            string rsv_string = "";

            // 予約情報を取得する
            List<RsvData> rsv_list = RsvData.GetListByPatDates(pt_id, sekou_date, sekou_date);

            // 開始時間順・予約種別順にソート
            rsv_list.Sort((x, y) =>
            {
                int i = x.Time1 - y.Time1;

                if (i == 0)
                {
                    i = x.Code1.CompareTo(y.Code1);
                }

                if (i == 0)
                {
                    i = x.Code2.CompareTo(y.Code2);
                }

                return i;
            });

            foreach (RsvData rsv in rsv_list)
            {
                rsv_string += "　" + rsv.TimeString() + "　" + rsv.Name + "　" + rsv.Cont1 + "　" + rsv.Cont2 + "\r\n";
            }

            // 予約がある場合
            if (rsv_string.Length > 0)
            {
                obj.OrderStrings.Add(new OrderString("【予　約】\r\n" + rsv_string + "\r\n"));
                obj.OrderStrings.Add(new OrderString("──────────────────────────────\r\n\r\n"));
            }

            obj.OrderStrings.Add(new OrderString("以　上"));

            return obj;
        }
    }

    /// <summary>
    /// １オーダーの内容を示す文字列
    /// </summary>
    class OrderString
    {
        public string PrintString = "";

        public int LineLength = 57;

        /// <summary>
        /// 文字列を行単位でリスト化
        /// </summary>
        public List<string> StringLines
        {
            get
            {
                List<string> list = new List<string>();

                // 現在の行
                string s = "";

                for (int i = 0; i < PrintString.Length; i++)
                {
                    if (s.Length >= LineLength || PrintString[i] == '\r')
                    {
                        list.Add((string)s.Clone());
                        s = "";
                    }

                    if (PrintString[i] != '\r' && PrintString[i] != '\n')
                    {
                        s += PrintString[i];
                    }
                }

                if (s.Length > 0)
                {
                    list.Add((string)s.Clone());
                }

                return list;
            }
        }

        public OrderString(string s)
        {
            PrintString = s;
        }
    }

    class PatOrderSheet
    {
        /// <summary>
        /// 予約種別コード
        /// </summary>
        public string RsvCode = "";

        /// <summary>
        /// 予約種別
        /// </summary>
        public string RsvName = "";

        /// <summary>
        /// 患者オーダー
        /// </summary>
        public PatOrder Order = new PatOrder();
    }
}