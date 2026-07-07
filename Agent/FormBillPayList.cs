using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using MedicalLibrary.Boundary;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class FormBillPayList : Form
    {
        class LiveTimeItem
        {
            public int Time = 0;

            public string Name = "";

            public override string ToString()
            {
                return this.Name;
            }

            public LiveTimeItem(int time, string name)
            {
                this.Time = time;
                this.Name = name;
            }
        }

        enum Mode : int
        {
            Stop = 0,
            Run = 1
        }

        Mode _mode = Mode.Stop;

        Mode _Mode
        {
            get
            {
                return this._mode;
            }
            set
            {
                this._mode = value;

                if (this._mode == Mode.Stop)
                {
                    this._Timer.Stop();
                }
                else if (this._mode == Mode.Run)
                {
                    this._Timer.Start();
                }
            }
        }

        DataSet DSet = new DataSet();

        ContextMenuStrip Menu1 = new ContextMenuStrip();

        Timer _Timer = new Timer();

        /// <summary>
        /// 時間計測
        /// </summary>
        Stopwatch _Sw1 = new Stopwatch();
        Stopwatch _Sw2 = new Stopwatch();

        public FormBillPayList()
        {
            InitializeComponent();
        }

        void _Timer_Tick(object sender, EventArgs e)
        {
            this.ListShow();
        }

        private void FormBillPayList_Load(object sender, EventArgs e)
        {
            DataTable table = DSet.Tables.Add("List1");
            table.Columns.Add("ID");
            table.Columns.Add("氏名");
            table.Columns.Add("性別");
            table.Columns.Add("年齢");
            table.Columns.Add("カナ");
            table.Columns.Add("受番");
            table.Columns.Add("到着");
            table.Columns.Add("待ち", typeof(int));
            table.Columns.Add("科");
            table.Columns.Add("保険");
            table.Columns.Add("要注意保険");
            table.Columns.Add("会計未入力");
            table.Columns.Add("請求書");
            table.Columns.Add("請求書未印刷", typeof(int));
            table.Columns.Add("備考");
            table.Columns.Add("本日請求", typeof(int));
            table.Columns.Add("過去未収", typeof(int));
            table.Columns.Add("SaveDateTime");
            table.Columns.Add("Obj", typeof(BillPay));

            table = DSet.Tables.Add("List2");
            table.Columns.Add("ID");
            table.Columns.Add("氏名");
            table.Columns.Add("性別");
            table.Columns.Add("年齢");
            table.Columns.Add("カナ");
            table.Columns.Add("受番");
            table.Columns.Add("到着");
            table.Columns.Add("待ち", typeof(int));
            table.Columns.Add("科");
            table.Columns.Add("保険");
            table.Columns.Add("要注意保険");
            table.Columns.Add("会計未入力");
            table.Columns.Add("請求書");
            table.Columns.Add("請求書未印刷", typeof(int));
            table.Columns.Add("備考");
            table.Columns.Add("本日請求", typeof(int));
            table.Columns.Add("過去未収", typeof(int));
            table.Columns.Add("SaveDateTime");
            table.Columns.Add("Obj", typeof(BillPay));

            table = DSet.Tables.Add("List3");
            table.Columns.Add("ID");
            table.Columns.Add("氏名");
            table.Columns.Add("性別");
            table.Columns.Add("年齢");
            table.Columns.Add("カナ");
            table.Columns.Add("受番");
            table.Columns.Add("到着");
            table.Columns.Add("待ち", typeof(int));
            table.Columns.Add("科");
            table.Columns.Add("保険");
            table.Columns.Add("要注意保険");
            table.Columns.Add("会計未入力");
            table.Columns.Add("請求書");
            table.Columns.Add("請求書未印刷", typeof(int));
            table.Columns.Add("備考");
            table.Columns.Add("本日請求", typeof(int));
            table.Columns.Add("過去未収", typeof(int));
            table.Columns.Add("SaveDateTime");
            table.Columns.Add("Obj", typeof(BillPay));

            // 場所選択ボックス
            foreach (BillPayPlace obj in BillPayPlace.Dict.Values)
            {
                this.PlaceBox.Items.Add(obj);
            }

            if (BillPayPlace.Dict.ContainsKey(LibSettings.Current.PC.BillPayPlace))
            {
                this.PlaceBox.SelectedItem = BillPayPlace.Dict[LibSettings.Current.PC.BillPayPlace];
            }
            else
            {
                this.PlaceBox.SelectedItem = BillPayPlace.Dict["1"];
            }

            // 請求書・明細書の印刷プリンター
            this.BillPrinterLabel.Text = LibSettings.Current.PC.BillPrinter;
            this.BillPrinterLabel.Click += new EventHandler(BillPrinterLabel_Click);

            this.InvoicePrinterLabel.Text = LibSettings.Current.PC.InvoicePrinter;
            this.InvoicePrinterLabel.Click += new EventHandler(InvoicePrinterLabel_Click);

            // 終了後の表示時間
            this.LiveTimeBox.Items.Add(new LiveTimeItem(0, "表示しない"));
            this.LiveTimeBox.Items.Add(new LiveTimeItem(10, "10分以内"));
            this.LiveTimeBox.Items.Add(new LiveTimeItem(30, "30分以内"));
            this.LiveTimeBox.Items.Add(new LiveTimeItem(-1, "すべて"));
            this.LiveTimeBox.SelectedIndex = 0;

            // DataGridView コンテキストメニュー
            ToolStripItem Item1 = Menu1.Items.Add("請求書プレビュー");
            Item1.Click += new EventHandler(Item1_Click);

            ToolStripItem Item2 = Menu1.Items.Add("POSデータ");
            Item2.Click += new EventHandler(Item2_Click);
            
            ToolStripItem Item9 = Menu1.Items.Add("終了");
            Item9.Click += new EventHandler(Item9_Click);

            this.Menu1.Opening += new CancelEventHandler(Menu1_Opening);

            this.ListView1.ContextMenuStrip = this.Menu1;
            this.ListView2.ContextMenuStrip = this.Menu1;
            this.ListView3.ContextMenuStrip = this.Menu1;

            // DataGridView クリック時の動作
            this.ListView1.CellClick += new DataGridViewCellEventHandler(ListView_CellClick);
            this.ListView2.CellClick += new DataGridViewCellEventHandler(ListView_CellClick);
            this.ListView3.CellClick += new DataGridViewCellEventHandler(ListView_CellClick);

            // 自動更新の設定
            this.IntervalBox.Items.Add("30");
            this.IntervalBox.Items.Add("60");
            this.IntervalBox.Items.Add("90");
            this.IntervalBox.Text = "30";

            this._Timer.Interval = 30 * 1000;
            this._Timer.Tick += new EventHandler(_Timer_Tick);

            // 印刷間隔
            for (int i = 0; i <= 10; i++)
            {
                this.PrintIntervalBox1.Items.Add(i);
                this.PrintIntervalBox2.Items.Add(i);
            }

            this.PrintIntervalBox1.Text = "1";
            this.PrintIntervalBox2.Text = "4";
        }

        private void FormBillPayList_Shown(object sender, EventArgs e)
        {
			// 初期設定が 請求書自動発行ON の端末
			if (LibSettings.Current.PC.BillPrintAuto.Equals("1"))
            {
				// 自動更新ON
				this.RunBox.Checked = true;

				// 請求書自動発行ON
				if (MessageBox.Show("請求書を自動発行しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    this.BillPrintAutoBox.Checked = true;
                }
            }

            this.ListShow();
        }

        void Menu1_Opening(object sender, CancelEventArgs e)
        {
            DataGridView view = (DataGridView)this.Menu1.SourceControl;

            // 請求書がなければ 請求書プレビュー = disable
            if (view.SelectedCells.Count > 0)
            {
                BillPay obj = (BillPay)view.Rows[view.SelectedCells[0].RowIndex].Cells["Obj"].Value;

                if (obj.BillList.Count > 0)
                {
                    this.Menu1.Items[0].Enabled = true;
                }
                else
                {
                    this.Menu1.Items[0].Enabled = false;
                }
            }

            // ListView3 であれば 終了 = disable
            this.Menu1.Items[2].Enabled = true;

            if (view.Name.Equals("ListView3"))
            {
                this.Menu1.Items[2].Enabled = false;
            }
            else if (view.SelectedCells.Count == 0)
            {
                this.Menu1.Items[2].Enabled = false;
            }
        }

        void Item1_Click(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)this.Menu1.SourceControl;

            if (view.SelectedCells.Count > 0)
            {
                BillPay obj = (BillPay)view.Rows[view.SelectedCells[0].RowIndex].Cells["Obj"].Value;
                List<string> file_list = new List<string>();

                foreach (Bill bill in obj.BillList)
                {
                    // 請求書を探す
                    foreach (string s in Directory.GetFiles(LibSettings.Current.BillPdfFolder, "Bill_" + bill.PtId + "_" + bill.BillId + ".pdf"))
                    {
                        file_list.Add(s);
                    }

                    // 明細書を探す
                    foreach (string s in Directory.GetFiles(LibSettings.Current.InvoicePdfFolder, "Invoice_" + bill.PtId + "_" + bill.BillId + "*.pdf"))
                    {
                        file_list.Add(s);
                    }
                }

                FormPDFViewer2 f = new FormPDFViewer2();
                f.Navigate(file_list);
                f.ShowDialog();
            }
        }

        void Item2_Click(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)this.Menu1.SourceControl;

            if (view.SelectedCells.Count > 0)
            {
                DataGridViewRow r = view.Rows[view.SelectedCells[0].RowIndex];

                FormPos f = new FormPos(r.Cells["ID"].Value.ToString());
                f.ShowDialog(this);
            }
        }

        void Item9_Click(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)this.Menu1.SourceControl;

            if (view.SelectedCells.Count > 0)
            {
                DataGridViewRow r = view.Rows[view.SelectedCells[0].RowIndex];

                if (MessageBox.Show(r.Cells["ID"].Value.ToString() + " " + r.Cells["氏名"].Value.ToString() + " 様\r\n終了しますか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    BillPay.StatusChange(this.DatePicker1.Value.ToString("yyyyMMdd"), ((BillPayPlace)this.PlaceBox.SelectedItem).Code, r.Cells["ID"].Value.ToString(), 8);
                    this.ListShow();
                }
            }
        }

        void BillPrinterLabel_Click(object sender, EventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            pd.PrinterSettings.PrinterName = this.BillPrinterLabel.Text;

            if (pd.ShowDialog() == DialogResult.OK)
            {
                LibSettings.Current.PC.BillPrinter = pd.PrinterSettings.PrinterName;
                this.BillPrinterLabel.Text = LibSettings.Current.PC.BillPrinter;
            }
        }

        void InvoicePrinterLabel_Click(object sender, EventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            pd.PrinterSettings.PrinterName = this.InvoicePrinterLabel.Text;

            if (pd.ShowDialog() == DialogResult.OK)
            {
                LibSettings.Current.PC.InvoicePrinter = pd.PrinterSettings.PrinterName;
                this.InvoicePrinterLabel.Text = LibSettings.Current.PC.InvoicePrinter;
            }
        }

        void ListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridView view = (DataGridView)sender;
            DataGridViewRow r = view.Rows[e.RowIndex];
            DataGridViewColumn col = view.Columns[e.ColumnIndex];

            BillPay obj = (BillPay)view.Rows[e.RowIndex].Cells["Obj"].Value;

            // 患者IDをクリップボードにコピー 2019/01/18 総合医事の要望
            Clipboard.SetText(obj.PtId);

            if (col.Name.Equals("請求書"))
            {
                // 請求書カラムの場合は、請求書・明細書を印刷する

                if (obj.BillList.Count == 0)
                {
                    return;
                }

                if (obj.BillPrintDoneList.Count == 0)
                {
                    // すべて未印刷の場合
                    this.Print(obj, false);

                    // 印刷ステータスが変わるため更新する
                    this.ListShow();
                }
                else if (obj.BillPrintYetList.Count == 0)
                {
                    // すべて印刷済の場合
                    if (MessageBox.Show(r.Cells["ID"].Value.ToString() + " " + r.Cells["氏名"].Value.ToString() + " 様\r\n\r\n未印刷の請求書はありません。\r\n再度印刷しますか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        this.Print(obj, true);
                    }
                }
                else
                {
                    // 印刷済と未印刷が混在する場合
                    List<StdMsgButton> mb_list = new List<StdMsgButton>();
                    mb_list.Add(new StdMsgButton("未印刷のみ印刷", DialogResult.Yes));
                    mb_list.Add(new StdMsgButton("印刷済もすべて印刷", DialogResult.No));
                    mb_list.Add(new StdMsgButton("印刷しない", DialogResult.Cancel));

                    DialogResult dr = StdMsgBox.Show(this, r.Cells["ID"].Value.ToString() + " " + r.Cells["氏名"].Value.ToString() + " 様\r\n\r\n未印刷の請求書を印刷しますか？", "確認", mb_list);

                    if (dr == DialogResult.Yes)
                    {
                        this.Print(obj, false);

                        // 印刷ステータスが変わるため更新する
                        this.ListShow();
                    }
                    else if (dr == DialogResult.No)
                    {
                        this.Print(obj, true);
                    }
                }
            }
        }

        void ListShow()
        {
            this._Sw1.Restart();

            // 元のスクロール位置をセットする
            int y1 = this.ListView1.FirstDisplayedScrollingRowIndex;
            int y2 = this.ListView2.FirstDisplayedScrollingRowIndex;
            int y3 = this.ListView3.FirstDisplayedScrollingRowIndex;

            BillPay p1 = new BillPay();
            BillPay p2 = new BillPay();
            BillPay p3 = new BillPay();
            int c1 = 0, c2 = 0, c3 = 0;

            if (this.ListView1.SelectedCells.Count > 0)
            {
                p1 = (BillPay)this.ListView1.Rows[this.ListView1.SelectedCells[0].RowIndex].Cells["Obj"].Value;
                c1 = this.ListView1.SelectedCells[0].ColumnIndex;
            }

            if (this.ListView2.SelectedCells.Count > 0)
            {
                p2 = (BillPay)this.ListView2.Rows[this.ListView2.SelectedCells[0].RowIndex].Cells["Obj"].Value;
                c2 = this.ListView2.SelectedCells[0].ColumnIndex;
            }

            if (this.ListView3.SelectedCells.Count > 0)
            {
                p3 = (BillPay)this.ListView3.Rows[this.ListView3.SelectedCells[0].RowIndex].Cells["Obj"].Value;
                c3 = this.ListView3.SelectedCells[0].ColumnIndex;
            }

            DataTable table1 = DSet.Tables["List1"];
            table1.Clear();

            DataTable table2 = DSet.Tables["List2"];
            table2.Clear();

            DataTable table3 = DSet.Tables["List3"];
            table3.Clear();

            if (this.PlaceBox.SelectedItem == null || this.LiveTimeBox.SelectedItem == null)
            {
                return;
            }

            List<BillPay> list = BillPay.GetLiveListByDate(this.DatePicker1.Value.ToString("yyyyMMdd"), ((BillPayPlace)this.PlaceBox.SelectedItem).Code, ((LiveTimeItem)LiveTimeBox.SelectedItem).Time);

            // 未収チェック
            // Status = 0 ← とりあえず 0, 1, 2, 8, 9 にしておく（負荷次第）
            List<string> pt_id_list1 = new List<string>();

            // 外来受付チェック
            // Status = 0, 1, 2, 8, 9
            List<string> pt_id_list2 = new List<string>();

            // 請求書チェック
            // Status = 0, 1, 2, 8, 9
            List<string> pt_id_list3 = new List<string>();

            // 入金チェック
            // Status = 0, 1, 2
            List<string> pt_id_list4 = new List<string>();

            // まず対象者の患者ＩＤリストを取得
            foreach (BillPay obj in list)
            {
                if (obj.PtId.Length == 0)
                {
                    continue;
                }

                if (obj.Status.Equals(0))
                {
                    if (!pt_id_list1.Contains(obj.PtId))
                    {
                        pt_id_list1.Add(obj.PtId);
                    }

                    if (!pt_id_list2.Contains(obj.PtId))
                    {
                        pt_id_list2.Add(obj.PtId);
                    }

                    if (!pt_id_list3.Contains(obj.PtId))
                    {
                        pt_id_list3.Add(obj.PtId);
                    }

                    if (!pt_id_list4.Contains(obj.PtId))
                    {
                        pt_id_list4.Add(obj.PtId);
                    }
                }
                else if (obj.Status.Equals(1) || obj.Status.Equals(2))
                {
                    if (!pt_id_list1.Contains(obj.PtId))
                    {
                        pt_id_list1.Add(obj.PtId);
                    }

                    if (!pt_id_list2.Contains(obj.PtId))
                    {
                        pt_id_list2.Add(obj.PtId);
                    }

                    if (!pt_id_list3.Contains(obj.PtId))
                    {
                        pt_id_list3.Add(obj.PtId);
                    }

                    if (!pt_id_list4.Contains(obj.PtId))
                    {
                        pt_id_list4.Add(obj.PtId);
                    }
                }
                else if (obj.Status.Equals(8) || obj.Status.Equals(9))
                {
                    // 処理が重い場合は外す
                    if (!pt_id_list1.Contains(obj.PtId))
                    {
                        pt_id_list1.Add(obj.PtId);
                    }

                    if (!pt_id_list2.Contains(obj.PtId))
                    {
                        pt_id_list2.Add(obj.PtId);
                    }

                    if (!pt_id_list3.Contains(obj.PtId))
                    {
                        pt_id_list3.Add(obj.PtId);
                    }
                }
            }

            // 請求データ
            List<PosDemand> list1 = PosDemand.GetListForBillPay(pt_id_list1, this.DatePicker1.Value.ToString("yyyyMMdd"));

            // 外来受付データ
            List<PatOut> list2 = PatOut.GetList(this.DatePicker1.Value.ToString("yyyyMMdd"), "", "").FindAll((x) =>
            {
                return pt_id_list2.Contains(x.Id);
            });

            // 請求書データ
            List<Bill> list3 = Bill.GetListByDate(this.DatePicker1.Value.ToString("yyyyMMdd")).FindAll((x) =>
            {
                return pt_id_list3.Contains(x.PtId);
            });

            // 入金データ
            List<PosRegHistoryDetail> list4 = PosRegHistoryDetail.GetListByPatDate(pt_id_list4, this.DatePicker1.Value.ToString("yyyyMMdd"));


            foreach (BillPay obj in list)
            {
                foreach (PosDemand obj1 in list1)
                {
                    if (!obj.PtId.Equals(obj1.PtId))
                    {
                        continue;
                    }

                    obj.PosDemandList.Add(obj1);
                }

                foreach (PatOut obj2 in list2)
                {
                    if (!obj.PtId.Equals(obj2.Id))
                    {
                        continue;
                    }

                    obj.PatOutList.Add(obj2);

					// 当日救急を受診された場合
					if (obj2.ModeName.Contains("救急") && !obj.CommentList.Contains("救急"))
					{
						obj.CommentList.Add("救急");
					}
                }

                foreach (Bill obj3 in list3)
                {
                    if (!obj.PtId.Equals(obj3.PtId))
                    {
                        continue;
                    }

                    bool b = true;

                    // この請求書IDの書損データがあれば飛ばす
                    foreach (PosDemand pd in obj.PosDemandList)
                    {
                        // 請求書IDが異なれば飛ばす
                        if (!pd.BillId.Equals(obj3.BillId))
                        {
                            continue;
                        }

                        // 書損でなければ飛ばす
                        if (!pd.Kind.Equals("3"))
                        {
                            continue;
                        }

                        b = false;
                        break;
                    }

                    if (b)
                    {
                        obj.BillList.Add(obj3);
                    }
                    else
                    {
                        if (!obj.CommentList.Contains("書損あり"))
                        {
                            obj.CommentList.Add("書損あり");
                        }
                    }
                }

                foreach (PosRegHistoryDetail obj4 in list4)
                {
                    if (!obj.PtId.Equals(obj4.PtId))
                    {
                        continue;
                    }

                    obj.PosRegHistoryList.Add(obj4);
                }

                // Status が変わる場合
                if (!obj.Status.Equals(obj.Status2))
                {
                    obj.Status = obj.Status2;

					// 自動更新の場合はステータスを変更する
					if (this.RunBox.Checked)
					{
						BillPay.StatusChange(this.DatePicker1.Value.ToString("yyyyMMdd"), ((BillPayPlace)this.PlaceBox.SelectedItem).Code, obj.PtId, obj.Status2);
					}
                }

                // Status = 1 and 請求書自動発行の場合
                if (obj.Status.Equals(1) && this.BillPrintAutoBox.Checked)
                {
                    // 未印刷のものがあれば印刷する
                    if (obj.BillPrintYetList.Count > 0)
                    {
                        this.Print(obj, false);
                    }
                }

                DataRow r;

                if (obj.Status2.Equals(1))
                {
                    r = table1.NewRow();

                    r["ID"] = obj.Pat.Id;
                    r["氏名"] = obj.Pat.Name;
                    r["性別"] = obj.Pat.Sex;
                    r["年齢"] = obj.Pat.AgeCalc(this.DatePicker1.Value.ToString("yyyyMMdd"));
                    r["カナ"] = obj.Pat.Kana;
                    r["受番"] = obj.Seq1;
                    r["到着"] = DateTimeAgent.TimeFormat6(obj.ArTime, 4);
                    r["待ち"] = obj.WaitMinutes;
                    r["科"] = obj.DeptNames;
                    r["保険"] = obj.InsNames;
                    r["要注意保険"] = obj.MarkInsNames;
                    r["会計未入力"] = obj.KaikeiYetDeptNames;
                    r["請求書"] = obj.BillStatus;
                    r["請求書未印刷"] = obj.BillPrintYetList.Count;
                    r["備考"] = obj.Comment;
                    r["本日請求"] = obj.BillMoneyToday;
                    r["過去未収"] = obj.PayYetMoneyYesterday;
                    r["SaveDateTime"] = obj.SaveDateTime;
                    r["Obj"] = obj;

                    table1.Rows.Add(r);
                }
                else if (obj.Status2.Equals(2))
                {
                    r = table2.NewRow();

                    r["ID"] = obj.Pat.Id;
                    r["氏名"] = obj.Pat.Name;
                    r["性別"] = obj.Pat.Sex;
                    r["年齢"] = obj.Pat.AgeCalc(this.DatePicker1.Value.ToString("yyyyMMdd"));
                    r["カナ"] = obj.Pat.Kana;
                    r["受番"] = obj.Seq1;
                    r["到着"] = DateTimeAgent.TimeFormat6(obj.ArTime, 4);
                    r["待ち"] = obj.WaitMinutes;
                    r["科"] = obj.DeptNames;
                    r["保険"] = obj.InsNames;
                    r["要注意保険"] = obj.MarkInsNames;
                    r["会計未入力"] = obj.KaikeiYetDeptNames;
                    r["請求書"] = obj.BillStatus;
                    r["請求書未印刷"] = obj.BillPrintYetList.Count;
                    r["備考"] = obj.Comment;
                    r["本日請求"] = obj.BillMoneyToday;
                    r["過去未収"] = obj.PayYetMoneyYesterday;
                    r["SaveDateTime"] = obj.SaveDateTime;
                    r["Obj"] = obj;

                    table2.Rows.Add(r);
                }
                else if (obj.Status2.Equals(8) || obj.Status2.Equals(9))
                {
                    // 「表示しない」以外の場合
                    if (!((LiveTimeItem)LiveTimeBox.SelectedItem).Time.Equals(0))
                    {
                        r = table3.NewRow();

                        r["ID"] = obj.Pat.Id;
                        r["氏名"] = obj.Pat.Name;
                        r["性別"] = obj.Pat.Sex;
                        r["年齢"] = obj.Pat.AgeCalc(this.DatePicker1.Value.ToString("yyyyMMdd"));
                        r["カナ"] = obj.Pat.Kana;
                        r["受番"] = obj.Seq1;
                        r["到着"] = DateTimeAgent.TimeFormat6(obj.ArTime, 4);
                        r["待ち"] = obj.WaitMinutes;
                        r["科"] = obj.DeptNames;
                        r["保険"] = obj.InsNames;
                        r["要注意保険"] = obj.MarkInsNames;
                        r["会計未入力"] = obj.KaikeiYetDeptNames;
                        r["請求書"] = obj.BillStatus;
                        r["請求書未印刷"] = obj.BillPrintYetList.Count;
                        r["備考"] = obj.Comment;
                        r["本日請求"] = obj.BillMoneyToday;
                        r["過去未収"] = obj.PayYetMoneyYesterday;
                        r["SaveDateTime"] = obj.SaveDateTime;
                        r["Obj"] = obj;

                        table3.Rows.Add(r);
                    }
                }
            }

            DataView view1 = new DataView(table1);

            if (this.FilterBox.Text.Length > 0)
            {
                view1.RowFilter = "ID = '" + this.FilterBox.Text + "' or 氏名 like '%" + this.FilterBox.Text + "%' or カナ like '%" + this.FilterBox.Text + "%'";
            }

            view1.Sort = "到着 asc";
            this.ListView1.DataSource = view1;
            this.ListView1.Columns["ID"].Width = 70;
            this.ListView1.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.ListView1.Columns["性別"].Width = 35;
            this.ListView1.Columns["性別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView1.Columns["性別"].Visible = false;
            this.ListView1.Columns["年齢"].Width = 35;
            this.ListView1.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView1.Columns["カナ"].Width = 80;
            this.ListView1.Columns["受番"].Width = 55;
            this.ListView1.Columns["受番"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.ListView1.Columns["到着"].Width = 55;
            this.ListView1.Columns["到着"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView1.Columns["待ち"].Width = 40;
            this.ListView1.Columns["待ち"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.ListView1.Columns["保険"].Width = 75;
            this.ListView1.Columns["要注意保険"].Visible = false;
            this.ListView1.Columns["請求書"].Width = 75;
            this.ListView1.Columns["請求書未印刷"].Visible = false;
            this.ListView1.Columns["本日請求"].Width = 65;
            this.ListView1.Columns["本日請求"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.ListView1.Columns["本日請求"].DefaultCellStyle.Format = "#,0";
            this.ListView1.Columns["本日請求"].Visible = true;
            this.ListView1.Columns["過去未収"].Width = 70;
            this.ListView1.Columns["過去未収"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.ListView1.Columns["過去未収"].DefaultCellStyle.Format = "#,0";
            this.ListView1.Columns["過去未収"].Visible = true;
            this.ListView1.Columns["SaveDateTime"].Visible = false;
            this.ListView1.Columns["Obj"].Visible = false;
            AppDataGridView.SexColor(this.ListView1, "性別");

            foreach (DataGridViewRow r in this.ListView1.Rows)
            {
                if ((int)r.Cells["待ち"].Value >= 20)
                {
                    r.Cells["待ち"].Style.BackColor = Color.LightPink;
                }

                if (r.Cells["要注意保険"].Value.ToString().Length > 0)
                {
                    r.Cells["保険"].Style.BackColor = Color.LightPink;
                }

                if (!r.Cells["請求書未印刷"].Value.ToString().Equals("0"))
                {
                    r.Cells["請求書"].Style.BackColor = Color.LightPink;
                }

                if ((int)r.Cells["本日請求"].Value < 0)
                {
                    r.Cells["本日請求"].Style.ForeColor = Color.Red;
                }

                if ((int)r.Cells["過去未収"].Value < 0)
                {
                    r.Cells["過去未収"].Style.ForeColor = Color.Red;
                }

                if (r.Cells["ID"].Value.ToString().Equals(p1.PtId))
                {
                    r.Cells[c1].Selected = true;
                }
            }

            if (y1 >= 0 && y1 < this.ListView1.Rows.Count)
            {
                this.ListView1.FirstDisplayedScrollingRowIndex = y1;
            }

            DataView view2 = new DataView(table2);

            if (this.FilterBox.Text.Length > 0)
            {
                view2.RowFilter = "ID = '" + this.FilterBox.Text + "' or 氏名 like '%" + this.FilterBox.Text + "%' or カナ like '%" + this.FilterBox.Text + "%'";
            }

            view2.Sort = "到着 asc";
            this.ListView2.DataSource = view2;
            this.ListView2.Columns["ID"].Width = 70;
            this.ListView2.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.ListView2.Columns["性別"].Width = 35;
            this.ListView2.Columns["性別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView2.Columns["性別"].Visible = false;
            this.ListView2.Columns["年齢"].Width = 35;
            this.ListView2.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView2.Columns["カナ"].Width = 80;
            this.ListView2.Columns["受番"].Width = 55;
            this.ListView2.Columns["受番"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.ListView2.Columns["到着"].Width = 55;
            this.ListView2.Columns["到着"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView2.Columns["待ち"].Width = 40;
            this.ListView2.Columns["待ち"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.ListView2.Columns["保険"].Width = 75;
            this.ListView2.Columns["要注意保険"].Visible = false;
            this.ListView2.Columns["請求書"].Width = 75;
            this.ListView2.Columns["請求書未印刷"].Visible = false;
            this.ListView2.Columns["本日請求"].Width = 65;
            this.ListView2.Columns["本日請求"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.ListView2.Columns["本日請求"].DefaultCellStyle.Format = "#,0";
            this.ListView2.Columns["本日請求"].Visible = true;
            this.ListView2.Columns["過去未収"].Width = 70;
            this.ListView2.Columns["過去未収"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.ListView2.Columns["過去未収"].DefaultCellStyle.Format = "#,0";
            this.ListView2.Columns["過去未収"].Visible = true;
            this.ListView2.Columns["SaveDateTime"].Visible = false;
            this.ListView2.Columns["Obj"].Visible = false;
            AppDataGridView.SexColor(this.ListView2, "性別");

            foreach (DataGridViewRow r in this.ListView2.Rows)
            {
                if ((int)r.Cells["待ち"].Value >= 20)
                {
                    r.Cells["待ち"].Style.BackColor = Color.LightPink;
                }

                if (r.Cells["要注意保険"].Value.ToString().Length > 0)
                {
                    r.Cells["保険"].Style.BackColor = Color.LightPink;
                }

                if (!r.Cells["請求書未印刷"].Value.ToString().Equals("0"))
                {
                    r.Cells["請求書"].Style.BackColor = Color.LightPink;
                }

                if ((int)r.Cells["本日請求"].Value < 0)
                {
                    r.Cells["本日請求"].Style.ForeColor = Color.Red;
                }

                if ((int)r.Cells["過去未収"].Value < 0)
                {
                    r.Cells["過去未収"].Style.ForeColor = Color.Red;
                }

                if (r.Cells["ID"].Value.ToString().Equals(p2.PtId))
                {
                    r.Cells[c2].Selected = true;
                }
            }

            if (y2 >= 0 && y2 < this.ListView2.Rows.Count)
            {
                this.ListView2.FirstDisplayedScrollingRowIndex = y2;
            }

            DataView view3 = new DataView(table3);

            if (this.FilterBox.Text.Length > 0)
            {
                view3.RowFilter = "ID = '" + this.FilterBox.Text + "' or 氏名 like '%" + this.FilterBox.Text + "%' or カナ like '%" + this.FilterBox.Text + "%'";
            }

            view3.Sort = "SaveDateTime desc";
            this.ListView3.DataSource = view3;
            this.ListView3.Columns["ID"].Width = 70;
            this.ListView3.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.ListView3.Columns["性別"].Width = 35;
            this.ListView3.Columns["性別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView3.Columns["性別"].Visible = false;
            this.ListView3.Columns["年齢"].Width = 35;
            this.ListView3.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView3.Columns["カナ"].Width = 80;
            this.ListView3.Columns["受番"].Width = 55;
            this.ListView3.Columns["受番"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.ListView3.Columns["到着"].Width = 55;
            this.ListView3.Columns["到着"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView3.Columns["待ち"].Width = 40;
            this.ListView3.Columns["待ち"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.ListView3.Columns["保険"].Width = 75;
            this.ListView3.Columns["要注意保険"].Visible = false;
            this.ListView3.Columns["請求書"].Width = 75;
            this.ListView3.Columns["請求書未印刷"].Visible = false;
            this.ListView3.Columns["本日請求"].Width = 65;
            this.ListView3.Columns["本日請求"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.ListView3.Columns["本日請求"].DefaultCellStyle.Format = "#,0";
            this.ListView3.Columns["本日請求"].Visible = false;
            this.ListView3.Columns["過去未収"].Width = 70;
            this.ListView3.Columns["過去未収"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.ListView3.Columns["過去未収"].DefaultCellStyle.Format = "#,0";
            this.ListView3.Columns["過去未収"].Visible = false;
            this.ListView3.Columns["備考"].Width = 230;
            this.ListView3.Columns["SaveDateTime"].Visible = false;
            this.ListView3.Columns["Obj"].Visible = false;
            AppDataGridView.SexColor(this.ListView3, "性別");

            foreach (DataGridViewRow r in this.ListView3.Rows)
            {
                if ((int)r.Cells["待ち"].Value >= 20)
                {
                    r.Cells["待ち"].Style.BackColor = Color.LightPink;
                }

                if (r.Cells["要注意保険"].Value.ToString().Length > 0)
                {
                    r.Cells["保険"].Style.BackColor = Color.LightPink;
                }

                if (!r.Cells["請求書未印刷"].Value.ToString().Equals("0"))
                {
                    r.Cells["請求書"].Style.BackColor = Color.LightPink;
                }

                if (r.Cells["ID"].Value.ToString().Equals(p3.PtId))
                {
                    r.Cells[c3].Selected = true;
                }
            }

            if (y3 >= 0 && y3 < this.ListView3.Rows.Count)
            {
                this.ListView3.FirstDisplayedScrollingRowIndex = y3;
            }

            NumLabel1.Text = ListView1.RowCount + "名";
            NumLabel2.Text = ListView2.RowCount + "名";
            NumLabel3.Text = ListView3.RowCount + "名";

            this._Sw1.Stop();
            this.SwLabel.Text = Math.Round((decimal)this._Sw1.ElapsedMilliseconds / 1000, 2).ToString() + "秒";

            this.PtIdBox.Focus();
        }

        private void RegButton_Click(object sender, EventArgs e)
        {
            this.Reg();
        }

        void Reg()
        {
            if (this.PtIdBox.Text.Length == 0)
            {
                return;
            }

            int pt_id = 0;

            if (!int.TryParse(this.PtIdBox.Text, out pt_id))
            {
                MessageBox.Show("患者IDが不適切です。入力しなおしてください。");
                this.PtIdBox.Clear();
                return;
            }

            // 今日でない場合は確認する
            if (!this.DatePicker1.Value.ToString("yyyyMMdd").Equals(DateTime.Now.ToString("yyyyMMdd")))
            {
                if (MessageBox.Show("本日ではありません。登録しますか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
            }

            BillPay obj = new BillPay();
            obj.PtId = pt_id.ToString();
            obj.ArDate = this.DatePicker1.Value.ToString("yyyyMMdd");
            obj.ArTime = DateTime.Now.ToString("HHmmss");
            obj.ArPlace = ((BillPayPlace)this.PlaceBox.SelectedItem).Code;
            obj.Status = 0;

            obj.Save();

            this.PtIdBox.Clear();
            this.ListShow();
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            this.ListShow();
        }

        private void RunBox_CheckedChanged(object sender, EventArgs e)
        {
            this._Mode = this.RunBox.Checked ? Mode.Run : Mode.Stop;
        }

        private void PtIdBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Reg();
            }
            else if (e.KeyCode == Keys.F3)
            {
                this.PtIdBox.Text = FormFindPat.FindPat().Id;
            }
        }

        private void FormBillPayList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                this.ListShow();
            }
        }

        /// <summary>
        /// 請求書・明細書の印刷
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="print_done">印刷済のものも印刷する</param>
        void Print(BillPay obj, bool print_done = false)
        {
            // 請求書ごとの印刷間隔
            float f1 = 2;
            float.TryParse(this.PrintIntervalBox1.Text, out f1);

            // 請求書と明細書の印刷間隔
            float f2 = 1;
            float.TryParse(this.PrintIntervalBox2.Text, out f2);

            // 印刷プロセスの同期を取る
            Process p;
            bool b = false;

            foreach (Bill bill in obj.BillList)
            {
                if (print_done || bill.Status.Equals(1))
                {
                    // 請求書を探す
                    foreach (string s in Directory.GetFiles(LibSettings.Current.BillPdfFolder, "Bill_" + bill.PtId + "_" + bill.BillId + ".pdf"))
                    {
                        p = Launcher.PDFPrint(s, LibSettings.Current.PC.BillPrinter);
                        p.WaitForExit(10 * 1000);
                        b = p.HasExited;
                    }

                    // 明細が必要な人のみ印刷する
                    if (obj.InvoiceFlg)
                    {
                        if (f2 > 0)
                        {
                            if (b)
                            {
                                // 請求書発行が終了した場合
                                System.Threading.Thread.Sleep((int)(f2 * 1000));
                            }
                            else
                            {
                                // 請求書発行が終了していなければ更に待つ
                                System.Threading.Thread.Sleep((int)(f2 * 2 * 1000));
                            }
                        }

                        // 明細書を探す
                        foreach (string s in Directory.GetFiles(LibSettings.Current.InvoicePdfFolder, "Invoice_" + bill.PtId + "_" + bill.BillId + "*.pdf"))
                        {
                            p = Launcher.PDFPrint(s, LibSettings.Current.PC.InvoicePrinter);
                            p.WaitForExit(10 * 1000);
                            b = p.HasExited;
                        }
                    }

                    // 印刷済にする
                    bill.Status = 2;
                    Bill.StatusChange(bill.BillId, 2);

                    if (f1 > 0)
                    {
                        if (b)
                        {
                            // 発行が終了した場合
                            System.Threading.Thread.Sleep((int)(f1 * 1000));
                        }
                        else
                        {
                            // 発行が終了していなければ更に待つ
                            System.Threading.Thread.Sleep((int)(f1 * 2 * 1000));
                        }
                    }
                }
            }
        }

        private void IntervalBox_TextChanged(object sender, EventArgs e)
        {
            int i = 30;

            if (int.TryParse(this.IntervalBox.Text, out i))
            {
                this._Timer.Interval = i * 1000;
            }
        }

        private void PlaceBox_TextChanged(object sender, EventArgs e)
        {
            this.ListShow();
        }

        private void PlaceLabel_DoubleClick(object sender, EventArgs e)
        {
            // PlaceBox の選択可否モードを変更する
            this.PlaceBox.Enabled = !this.PlaceBox.Enabled;
        }

        private void LiveTimeBox_TextChanged(object sender, EventArgs e)
        {
            if (((LiveTimeItem)LiveTimeBox.SelectedItem).Time.Equals(0))
            {
                // 終了者を「表示しない」場合
                this.ListView3.Visible = false;
                this.ListLabel3.Location = new Point(10, this.Height - 60);
                this.LiveTimeBox.Location = new Point(100, this.Height - 64);
                this.NumLabel3.Location = new Point(this.Width - 70, this.Height - 60);
                this.NumLabel3.Visible = false;
                this.ListView1.Height = this.Height - 290;
            }
            else
            {
                // 終了者も表示する場合
                this.ListView3.Visible = true;
                this.ListLabel3.Location = new Point(10, this.Height - 220);
                this.LiveTimeBox.Location = new Point(100, this.Height - 224);
                this.NumLabel3.Location = new Point(this.Width - 70, this.Height - 220);
                this.NumLabel3.Visible = true;
                this.ListView1.Height = this.Height - 450;
            }

            this.ListShow();
        }

        private void PrintIntervalBox1_TextChanged(object sender, EventArgs e)
        {
            // 請求書ごとの印刷間隔
            // デフォルト 1秒
            float f = 0;

            if (float.TryParse(this.PrintIntervalBox1.Text, out f))
            {
                if (f < 0 || f > 10)
                {
                    this.PrintIntervalBox1.Text = "1";
                }
            }
            else
            {
                this.PrintIntervalBox1.Text = "1";
            }
        }

        private void PrintIntervalBox2_TextChanged(object sender, EventArgs e)
        {
            // 請求書と明細書の印刷間隔
            // デフォルト 4秒
            float f = 0;

            if (float.TryParse(this.PrintIntervalBox2.Text, out f))
            {
                if (f < 0 || f > 10)
                {
                    this.PrintIntervalBox2.Text = "4";
                }
            }
            else
            {
                this.PrintIntervalBox2.Text = "4";
            }
        }

        private void ManualButton_Click(object sender, EventArgs e)
        {
            string file = AppFile.FilePath(@"doc\BillPay1.pdf");

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
