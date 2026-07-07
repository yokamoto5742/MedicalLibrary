using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class FormQ26 : Form
    {
        DataSet DSet = new DataSet();

        ContextMenuStrip Menu1 = new ContextMenuStrip();

        Timer Timer1 = new Timer();

        enum LogKind : int
        {
            Order = 1,
            OrderDel = 2
        }

        enum Mode : int
        {
            Run = 1,
            Stop = 0
        }

        Mode _Mode1 = Mode.Stop;

        Mode Mode1
        {
            get
            {
                return this._Mode1;
            }
            set
            {
                this._Mode1 = value;

                if (this._Mode1 == Mode.Run)
                {
                    // 自動送信の場合は会計フラグをセットする
                    this.SetKaikeiFlgBox.Checked = true;

                    this.Timer1.Start();
                }
                else
                {
                    this.Timer1.Stop();
                }
            }
        }

        /// <summary>
        /// リスト表示・非表示
        /// </summary>
        bool Wide = true;

        public FormQ26()
        {
            InitializeComponent();

            DataTable table = DSet.Tables.Add("List1");
            table.Columns.Add("日付");
            table.Columns.Add("RP");
            table.Columns.Add("連番");
            table.Columns.Add("オーダー番号");
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("氏名");
            table.Columns.Add("性別");
            table.Columns.Add("年齢");
            table.Columns.Add("保険パターン");
            table.Columns.Add("保険");
            table.Columns.Add("科");
            table.Columns.Add("医師");
            table.Columns.Add("入外コード");
            table.Columns.Add("入外");
            table.Columns.Add("診区コード");
            table.Columns.Add("診区");
            table.Columns.Add("内容");
            table.Columns.Add("施行");
            table.Columns.Add("会計");
            table.Columns.Add("送信", typeof(bool));
            table.Columns.Add("Obj", typeof(PatOrder));

            table = DSet.Tables.Add("List2");
            table.Columns.Add("日付");
            table.Columns.Add("RP");
            table.Columns.Add("連番");
            table.Columns.Add("オーダー番号");
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("氏名");
            table.Columns.Add("性別");
            table.Columns.Add("年齢");
            table.Columns.Add("保険パターン");
            table.Columns.Add("保険");
            table.Columns.Add("科");
            table.Columns.Add("医師");
            table.Columns.Add("入外コード");
            table.Columns.Add("入外");
            table.Columns.Add("診区コード");
            table.Columns.Add("診区");
            table.Columns.Add("内容");
            table.Columns.Add("施行");
            table.Columns.Add("会計");
            table.Columns.Add("送信", typeof(bool));
            table.Columns.Add("Obj", typeof(PatOrder));
        }

        private void FormQ26_Load(object sender, EventArgs e)
        {
            // 出力先フォルダ
            if (!Directory.Exists(LibSettings.Current.Proas.OrderXmlTmpFolder))
            {
                Directory.CreateDirectory(LibSettings.Current.Proas.OrderXmlTmpFolder);
            }

            this.TmpPathLabel.Text = LibSettings.Current.Proas.OrderXmlTmpFolder;

            // 移動先フォルダ
            if (Directory.Exists(LibSettings.Current.Proas.OrderXmlDstFolder))
            {
                this.DstPathLabel.Text = LibSettings.Current.Proas.OrderXmlDstFolder;
            }
            else
            {
                // なければ一時フォルダと同じにする
                this.DstPathLabel.Text = this.TmpPathLabel.Text;
            }

			// ログフォルダ
			if (!Directory.Exists(LibSettings.Current.Proas.OrderXmlLogFolder))
			{
				Directory.CreateDirectory(LibSettings.Current.Proas.OrderXmlLogFolder);
			}

			// 送信後に会計フラグをセット
            this.SetKaikeiFlgBox.Checked = true;

			this.DoneBox.Items.Add("すべて");
			this.DoneBox.Items.Add("未処理");
			this.DoneBox.Text = "未処理";

			this.InOutBox.Items.Add("すべて");
			this.InOutBox.Items.Add("外来");
			this.InOutBox.Items.Add("入院");
			this.InOutBox.Text = "入院";

			this.ShinkuBox.Items.Add("");

			foreach (string key in Dict.KouiDict.Keys)
			{
				this.ShinkuBox.Items.Add(key + " " + Dict.KouiDict[key]);
			}
			
			this.DoujituRaiinsuBox.Items.Add("0");
            this.DoujituRaiinsuBox.Items.Add("1");
            this.DoujituRaiinsuBox.Items.Add("2");
            this.DoujituRaiinsuBox.Items.Add("3");
            this.DoujituRaiinsuBox.Items.Add("4");
            this.DoujituRaiinsuBox.Items.Add("5");
            this.DoujituRaiinsuBox.Text = "1";

            ToolStripMenuItem item1 = new ToolStripMenuItem("送信");
            item1.Click += new EventHandler(Item1_Click);
            this.Menu1.Items.Add(item1);

            ToolStripMenuItem item2 = new ToolStripMenuItem("会計フラグリセット");
            item2.Click += new EventHandler(Item2_Click);
            this.Menu1.Items.Add(item2);

            this.ListView1.ContextMenuStrip = this.Menu1;
            this.ListView2.ContextMenuStrip = this.Menu1;

            this.Menu1.Opening += new CancelEventHandler(Menu1_Opening);

            this.IntervalBox.Items.Add("60");
            this.IntervalBox.Items.Add("120");
            this.IntervalBox.Items.Add("180");
            this.IntervalBox.Text = "60";

            this.Timer1.Interval = 60 * 1000;
            this.Timer1.Tick += new EventHandler(Timer1_Tick);

            // 最初はリスト非表示
            this.ListShowSwitch();
		}

		private void FormQ26_Shown(object sender, EventArgs e)
		{
			if (MessageBox.Show("自動送信モードにしますか？", "確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				this.ModeBox.Checked = true;
			}
		}

        private void ModeBox_CheckedChanged(object sender, EventArgs e)
        {
            this.Mode1 = this.ModeBox.Checked ? Mode.Run : Mode.Stop;
        }

        void Timer1_Tick(object sender, EventArgs e)
        {
            // 自動更新中は、自動的に　未処理・入院・本日の日付　にする
            this.DoneBox.Text = "未処理";
            this.InOutBox.Text = "入院";

            if (!this.DatePicker1.Value.ToString("yyyyMMdd").Equals(DateTime.Now.ToString("yyyyMMdd")))
            {
                this.DatePicker1.Value = DateTime.Now;
            }

            this.ListShow();
            this.Send();
        }

        void Menu1_Opening(object sender, CancelEventArgs e)
        {
            DataGridView view = (DataGridView)this.Menu1.SourceControl;

            if (view.SelectedCells.Count > 0)
            {
                this.Menu1.Items[0].Enabled = true;
                this.Menu1.Items[1].Enabled = true;
            }
            else
            {
                this.Menu1.Items[0].Enabled = false;
                this.Menu1.Items[1].Enabled = false;
            }
        }

        void Item1_Click(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)this.Menu1.SourceControl;

            if (view.SelectedCells.Count == 0)
            {
                return;
            }

            StdReturn sr = new StdReturn();

            PatOrder order = (PatOrder)view.Rows[view.SelectedCells[0].RowIndex].Cells["Obj"].Value;
            List<PatOrder> list = new List<PatOrder>();
            list.Add(order);

            if (view.Name.Equals("ListView1"))
            {
                sr = Q26.Execute(list, DateTime.Now, 1, int.Parse(this.DoujituRaiinsuBox.Text), true, this.TmpPathLabel.Text, this.DstPathLabel.Text, this.SetKaikeiFlgBox.Checked, LoginUser.Id);

                if (sr.ErrExist)
                {
                    this.Log(LogKind.Order, "★" + sr.Err);
                    MessageBox.Show(sr.Err);
                }

                if (sr.MsgExist)
                {
                    this.Log(LogKind.Order, sr.Msg);
                    MessageBox.Show("送信しました");
                    this.ListShow();
                }
            }
            else if (view.Name.Equals("ListView2"))
            {
				sr = Q26.Execute(list, DateTime.Now, 1, 1, false, this.TmpPathLabel.Text, this.DstPathLabel.Text, this.SetKaikeiFlgBox.Checked, LoginUser.Id);

                if (sr.ErrExist)
                {
                    this.Log(LogKind.OrderDel, "★" + sr.Err);
                    MessageBox.Show(sr.Err);
                }

                if (sr.MsgExist)
                {
                    this.Log(LogKind.OrderDel, sr.Msg);
                    MessageBox.Show("送信しました");
                    this.ListShow();
                }
            }
        }

        void Item2_Click(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)this.Menu1.SourceControl;

            if (view.SelectedCells.Count == 0)
            {
                return;
            }

            StdReturn sr = new StdReturn();

            PatOrder order = (PatOrder)view.Rows[view.SelectedCells[0].RowIndex].Cells["Obj"].Value;
            PatOrder.SetKaikeiFlg(order.OrderId, 0);

            MessageBox.Show("会計フラグをリセットしました");
            this.ListShow();
        }

        void ListShow()
        {
            DataTable table = DSet.Tables["List1"];
            table.Clear();

            List<PatOrder> list = new List<PatOrder>();

            string in_out = "";

            if (this.InOutBox.Text.Equals("外来"))
            {
                in_out = "1";
            }
            else if (this.InOutBox.Text.Equals("入院"))
            {
                in_out = "2";
            }

            List<string> shinku_list = new List<string>();
            List<string> dept_list = new List<string>();
            List<string> sekou1_list = new List<string>();

            string order_label = "";

            if (this.DoneBox.Text.Equals("すべて"))
            {
                // 全オーダーを取得
                list = PatOrder.GetListByDate(
                    this.DatePicker1.Value.ToString("yyyyMMdd"),
                    in_out,
                    shinku_list,
                    dept_list,
                    sekou1_list,
                    false,
                    true);

                order_label = "【取込】" + this.DatePicker1.Value.ToString("yy/MM/dd");
            }
            else
            {
                // 施行済・未取込オーダーのみ取得

                if (in_out.Equals("2"))
                {
                    // 入院の場合は、過去１４日分を取得
                    list = PatOrder.GetListByDatesNotKaikei(
                        this.DatePicker1.Value.AddDays(-14).ToString("yyyyMMdd"),
                        this.DatePicker1.Value.ToString("yyyyMMdd"),
                        in_out,
                        dept_list,
                        true);

                    order_label = "【取込】" + this.DatePicker1.Value.AddDays(-14).ToString("yy/MM/dd") + "～" + this.DatePicker1.Value.ToString("yy/MM/dd");
                }
                else if (in_out.Equals("1"))
                {
                    // 外来の場合は、当日分のみ取得
                    list = PatOrder.GetListByDatesNotKaikei(
                        this.DatePicker1.Value.ToString("yyyyMMdd"),
                        this.DatePicker1.Value.ToString("yyyyMMdd"),
                        in_out,
                        dept_list,
                        true);

                    order_label = "【取込】" + this.DatePicker1.Value.ToString("yy/MM/dd");
                }
                else
                {
                    // すべての場合は、当日分のみ取得
                    list = PatOrder.GetListByDatesNotKaikei(
                        this.DatePicker1.Value.ToString("yyyyMMdd"),
                        this.DatePicker1.Value.ToString("yyyyMMdd"),
                        in_out,
                        dept_list,
                        true);

                    order_label = "【取込】" + this.DatePicker1.Value.ToString("yy/MM/dd");
                }
            }

            foreach (PatOrder order in list)
            {
                DataRow r = table.NewRow();

                r["日付"] = order.SekouDateStringShort;
                r["オーダー番号"] = order.OrderId;
                r["RP"] = order.UkeId;
                r["連番"] = order.UkeSEQ;
                r["ID"] = order.Pat.Id;
                r["氏名"] = order.Pat.Name;
                r["性別"] = order.Pat.SexNameShort;
                r["年齢"] = order.Pat.AgeCalc(order.SekouDate);
                r["保険パターン"] = order.Pat.Ins;
                r["保険"] = order.Pat.InsKindName;
                r["科"] = order.DeptName;
                r["医師"] = order.DoctorName;
                r["入外コード"] = order.InOut;
                r["入外"] = order.InOutNameShort;
                r["診区コード"] = order.Shinku;
                r["診区"] = order.ShinkuString;
                r["内容"] = order.SOAP;
                r["施行"] = order.SekouFlg;
                r["会計"] = order.KaikeiFlg;
				r["送信"] = order.InOut.Equals("2") && order.SekouFlg.Equals("1") && !order.KaikeiFlg.Equals("1");

                r["Obj"] = order;

                table.Rows.Add(r);
            }

            table = DSet.Tables["List2"];
            table.Clear();

            // 削除の場合は、未処理（BILL_FLG = 0）のみ、過去１か月日分
            list = PatOrder.GetDelListByDates(
                this.DatePicker1.Value.AddMonths(-1).ToString("yyyyMMdd"),
                this.DatePicker1.Value.ToString("yyyyMMdd"),
                "",
                dept_list,
                false,
                false);

            order_label += Environment.NewLine;
            order_label += "【削除】" + this.DatePicker1.Value.AddMonths(-1).ToString("yy/MM/dd") + "～" + this.DatePicker1.Value.ToString("yy/MM/dd");

            foreach (PatOrder order in list)
            {
				DataRow r = table.NewRow();

                r["日付"] = order.SekouDateStringShort;
                r["オーダー番号"] = order.OrderId;
                r["RP"] = order.UkeId;
                r["連番"] = order.UkeSEQ;
                r["ID"] = order.Pat.Id;
                r["氏名"] = order.Pat.Name;
                r["性別"] = order.Pat.SexNameShort;
                r["年齢"] = order.Pat.AgeCalc(order.SekouDate);
                r["保険パターン"] = order.Pat.Ins;
                r["保険"] = order.Pat.InsKindName;
                r["科"] = order.DeptName;
                r["医師"] = order.DoctorName;
                r["入外コード"] = order.InOut;
                r["入外"] = order.InOutNameShort;
                r["診区コード"] = order.Shinku;
                r["診区"] = order.ShinkuString;
//                r["内容"] = order.SOAP;
                r["施行"] = order.SekouFlg;
                r["会計"] = order.KaikeiFlg;
                r["送信"] = !order.KaikeiFlg.Equals("1");
                r["Obj"] = order;

                table.Rows.Add(r);
            }

            this.OrderLabel.Text = order_label;

            this.ListFormat();
        }

        void ListFormat()
        {
            DataTable table = DSet.Tables["List1"];
            DataView view = new DataView(table);

            List<string> filters = new List<string>();

            if (this.InOutBox.Text.Equals("外来"))
            {
                filters.Add("(入外コード = '1')");
            }
            else if (this.InOutBox.Text.Equals("入院"))
            {
                filters.Add("(入外コード = '2')");
            }

            if (this.ShinkuBox.Text.Length > 0 && this.ShinkuBox.Text.Contains(" "))
            {
                filters.Add("(診区コード = '" + this.ShinkuBox.Text.Split(' ')[0] + "')");
            }

            if (this.FilterBox.Text.Length > 0)
            {
                int id = 0;

                if (int.TryParse(this.FilterBox.Text, out id))
                {
                    filters.Add("(氏名 like '%" + this.FilterBox.Text + "%' or 内容 like '%" + this.FilterBox.Text + "%' or ID = '" + id + "')");
                }
                else
                {
                    filters.Add("(氏名 like '%" + this.FilterBox.Text + "%' or 内容 like '%" + this.FilterBox.Text + "%')");
                }
            }

            if (filters.Count > 0)
            {
                view.RowFilter = AppString.ConcatList(filters, " and ");
            }

            this.ListView1.DataSource = view;

            this.ListView1.Columns["日付"].Width = 55;
            this.ListView1.Columns["日付"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView1.Columns["RP"].Width = 80;
            this.ListView1.Columns["連番"].Width = 28;
            this.ListView1.Columns["連番"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView1.Columns["オーダー番号"].Width = 90;
            this.ListView1.Columns["ID"].Width = 60;
            this.ListView1.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.ListView1.Columns["氏名"].Width = 80;
            this.ListView1.Columns["性別"].Width = 30;
            this.ListView1.Columns["性別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView1.Columns["年齢"].Width = 30;
            this.ListView1.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView1.Columns["保険パターン"].Visible = false;
            this.ListView1.Columns["保険"].Width = 35;
            this.ListView1.Columns["保険"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView1.Columns["科"].Width = 45;
            this.ListView1.Columns["医師"].Width = 65;
            this.ListView1.Columns["入外コード"].Visible = false;
            this.ListView1.Columns["入外"].Width = 28;
            this.ListView1.Columns["入外"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView1.Columns["診区コード"].Width = 30;
            this.ListView1.Columns["診区コード"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView1.Columns["診区"].Width = 35;
            this.ListView1.Columns["診区"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView1.Columns["内容"].Width = 255;
            this.ListView1.Columns["施行"].Visible = false;
            this.ListView1.Columns["会計"].Visible = false;
            this.ListView1.Columns["送信"].Width = 28;
            this.ListView1.Columns["Obj"].Visible = false;

            AppDataGridView.SexColor(this.ListView1, "性別");

            foreach (DataGridViewRow r in this.ListView1.Rows)
            {
                PatOrder order = (PatOrder)r.Cells["Obj"].Value;

                if (order.KaikeiFlg.Equals("1"))
                {
                    r.Cells["オーダー番号"].Style.BackColor = Color.Gold;
                    r.Cells["内容"].Style.BackColor = Color.Gold;
                }
                else if (order.SekouFlg.Equals("1"))
                {
                    r.Cells["オーダー番号"].Style.BackColor = Color.Yellow;
                    r.Cells["内容"].Style.BackColor = Color.Yellow;
                }

                if (order.InOut.Equals("1"))
                {
                    r.Cells["入外"].Style.BackColor = Color.LightGreen;
                }
                else if (order.InOut.Equals("2"))
                {
                    r.Cells["入外"].Style.BackColor = Color.LightPink;
                }
            }


            table = DSet.Tables["List2"];
            view = new DataView(table);

            if (filters.Count > 0)
            {
                // 削除はフィルタリングしない
//                view.RowFilter = AppString.ConcatList(filters, " and ");
            }

            this.ListView2.DataSource = view;

            this.ListView2.Columns["日付"].Width = 55;
            this.ListView2.Columns["日付"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView2.Columns["RP"].Width = 80;
            this.ListView2.Columns["連番"].Width = 28;
            this.ListView2.Columns["連番"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView2.Columns["オーダー番号"].Width = 90;
            this.ListView2.Columns["ID"].Width = 60;
            this.ListView2.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.ListView2.Columns["氏名"].Width = 80;
            this.ListView2.Columns["性別"].Width = 30;
            this.ListView2.Columns["性別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView2.Columns["年齢"].Width = 30;
            this.ListView2.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView2.Columns["保険パターン"].Visible = false;
            this.ListView2.Columns["保険"].Width = 35;
            this.ListView2.Columns["保険"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView2.Columns["科"].Width = 45;
            this.ListView2.Columns["医師"].Width = 65;
            this.ListView2.Columns["入外コード"].Visible = false;
            this.ListView2.Columns["入外"].Width = 28;
            this.ListView2.Columns["入外"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView2.Columns["診区コード"].Width = 30;
            this.ListView2.Columns["診区コード"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView2.Columns["診区"].Width = 35;
            this.ListView2.Columns["診区"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView2.Columns["内容"].Width = 255;
            this.ListView2.Columns["施行"].Visible = false;
            this.ListView2.Columns["会計"].Visible = false;
            this.ListView2.Columns["送信"].Width = 28;
            this.ListView2.Columns["Obj"].Visible = false;

            AppDataGridView.SexColor(this.ListView2, "性別");

            foreach (DataGridViewRow r in this.ListView2.Rows)
            {
                PatOrder order = (PatOrder)r.Cells["Obj"].Value;

                if (order.KaikeiFlg.Equals("1"))
                {
                    r.Cells["オーダー番号"].Style.BackColor = Color.Gold;
                    r.Cells["内容"].Style.BackColor = Color.Gold;
                }
                else if (order.SekouFlg.Equals("1"))
                {
                    r.Cells["オーダー番号"].Style.BackColor = Color.Yellow;
                    r.Cells["内容"].Style.BackColor = Color.Yellow;
                }

                if (order.InOut.Equals("1"))
                {
                    r.Cells["入外"].Style.BackColor = Color.LightGreen;
                }
                else if (order.InOut.Equals("2"))
                {
                    r.Cells["入外"].Style.BackColor = Color.LightPink;
                }
            }
        }

        private void IntervalBox_TextChanged(object sender, EventArgs e)
        {
            int i = 30;

            if (int.TryParse(this.IntervalBox.Text, out i))
            {
                this.Timer1.Interval = i * 1000;
            }
        }

        private void DatePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.ListShow();
        }

        private void DoneBox_TextChanged(object sender, EventArgs e)
        {
            this.ListShow();
        }

        private void InOutBox_TextChanged(object sender, EventArgs e)
        {
            this.ListShow();
        }

        private void ShinkuBox_TextChanged(object sender, EventArgs e)
        {
            this.ListFormat();
        }

        private void FilterBox_TextChanged(object sender, EventArgs e)
        {
            this.ListFormat();
        }

        private void TmpPathLabel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();

            fd.Description = "連携ファイルの出力先フォルダを選択してください";

            if (Directory.Exists(this.TmpPathLabel.Text))
            {
                fd.SelectedPath = this.TmpPathLabel.Text;
            }

            fd.ShowNewFolderButton = false;

            if (fd.ShowDialog(this) == DialogResult.OK)
            {
                this.TmpPathLabel.Text = fd.SelectedPath;
            }
        }

        private void DstPathLabel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();

            fd.Description = "連携ファイルの移動先フォルダを選択してください";

            if (Directory.Exists(this.DstPathLabel.Text))
            {
                fd.SelectedPath = this.DstPathLabel.Text;
            }

            fd.ShowNewFolderButton = false;

            if (fd.ShowDialog(this) == DialogResult.OK)
            {
                this.DstPathLabel.Text = fd.SelectedPath;
            }
        }

        StdReturn Send()
        {
            StdReturn sr = new StdReturn();

            try
            {
                if (this.TmpPathLabel.Text.Length == 0 || !Directory.Exists(this.TmpPathLabel.Text))
                {
                    sr.Errs.Add("出力先パスを指定してください");
                }

                if (this.DstPathLabel.Text.Length == 0 || !Directory.Exists(this.DstPathLabel.Text))
                {
                    sr.Errs.Add("移動先パスを指定してください");
                }

                // ファイル生成日時
                DateTime dt = DateTime.Now;

                List<PatOrder> list1 = new List<PatOrder>();
                List<PatOrder> list2 = new List<PatOrder>();

                foreach (DataGridViewRow r in this.ListView1.Rows)
                {
                    if (!(bool)r.Cells["送信"].Value)
                    {
                        continue;
                    }

                    PatOrder order = (PatOrder)r.Cells["Obj"].Value;
                    list1.Add(order);
                }

                foreach (DataGridViewRow r in this.ListView2.Rows)
                {
                    if (!(bool)r.Cells["送信"].Value)
                    {
                        continue;
                    }

                    PatOrder order = (PatOrder)r.Cells["Obj"].Value;
                    list2.Add(order);
                }

                // この時点で例外があれば終了
                if (sr.ErrExist)
                {
                    throw new Exception(sr.Err);
                }

                sr = new StdReturn();

                // 先に削除を生成する

				StdReturn srr = Q26.Execute(list2, dt, 1, 1, false, this.TmpPathLabel.Text, this.DstPathLabel.Text, this.SetKaikeiFlgBox.Checked, LoginUser.Id);

                if (srr.ErrExist)
                {
                    sr.Errs.AddRange(srr.Errs);
                    this.Log(LogKind.OrderDel, "★" + srr.Err);
                }

                if (srr.MsgExist)
                {
                    sr.Msgs.AddRange(srr.Msgs);
                    this.Log(LogKind.OrderDel, srr.Msg);
                }

                int i = srr.Msgs.Count + 1;


                // 次に登録・修正を生成する

				srr = Q26.Execute(list1, dt, i, int.Parse(this.DoujituRaiinsuBox.Text), true, this.TmpPathLabel.Text, this.DstPathLabel.Text, this.SetKaikeiFlgBox.Checked, LoginUser.Id);

                if (srr.MsgExist)
                {
                    sr.Msgs.AddRange(srr.Msgs);
                    this.Log(LogKind.Order, srr.Msg);
                }

                if (srr.ErrExist)
                {
                    sr.Errs.AddRange(srr.Errs);
                    this.Log(LogKind.Order, "★" + srr.Err);
                }

                this.ListShow();

                return sr;
            }
            catch (Exception ex)
            {
                sr.Errs.Clear();
                sr.Errs.Add(ex.Message);
                return sr;
            }
            finally
            {
            }
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            this.Send();
        }

        /// <summary>
        /// ログ
        /// </summary>
        /// <param name="kind">Order: オーダー, OrderDel: オーダー削除</param>
        /// <param name="msg">メッセージ</param>
        void Log(LogKind kind, string msg)
        {
            if (msg.Length == 0)
            {
                return;
            }

            string s = "";

            s += DateTime.Now.ToString("yy/MM/dd HH:mm:ss") + " [" + kind + "]" + Environment.NewLine;
            s += msg + Environment.NewLine;

            // 字数制限を超える場合
            if (this.LogBox.Text.Length + s.Length >= this.LogBox.MaxLength)
            {
                this.LogBox.Clear();
            }

            this.LogBox.Text += s;

            // 一番下までスクロール
            this.LogBox.Select(this.LogBox.Text.Length, 0);
            this.LogBox.ScrollToCaret();

			using (StreamWriter sw1 = new StreamWriter(LibSettings.Current.Proas.OrderXmlLogFolder + "\\Q26_" + DateTime.Now.ToString("yyMMdd") + ".log", true, Encoding.Default))
			{
				sw1.WriteLine(s);
			}
		}

        private void ListShowButton_Click(object sender, EventArgs e)
        {
            this.ListShowSwitch();
        }

        void ListShowSwitch()
        {
            this.Wide = !this.Wide;

            if (this.Wide)
            {
                this.Width = 1280;
                this.Height = 680;
                this.ListShowButton.Text = "リスト非表示";
            }
            else
            {
                this.Width = 270;
                this.Height = 400;
                this.ListShowButton.Text = "リスト表示";
            }
        }
    }
}
