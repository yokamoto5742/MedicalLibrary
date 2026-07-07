using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using MedicalLibrary.Boundary;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class MWMForm : Form
    {
        /// <summary>
        /// 設定ファイル
        /// </summary>
        string SettingsFile = "";

        MWMSettings Settings = new MWMSettings();

        DataSet dSet = new DataSet();

        PatBase _Pat = new PatBase();

        public PatBase Pat
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

        Timer _Timer = new Timer();

        public MWMForm(string file = "")
        {
            InitializeComponent();

            this.SettingsFile = file;
        }

        private void MWMForm_Load(object sender, EventArgs e)
        {
            try
            {
                // プログラムで一度も実行されていなければ実行する
                LibSettings.Init();

                if (this.SettingsFile.Length > 0)
                {
                    string[] args = new string[3];

                    args[0] = "MWM.exe";
                    args[1] = "-f";
                    args[2] = this.SettingsFile;

                    this.InitShow(args);
                }
                else
                {
                    this.InitShow(Environment.GetCommandLineArgs());
                }

                if (!this.Settings.IsValid)
                {
                    throw new Exception("設定が初期化されていません");
                }

                DataTable tmpTable = dSet.Tables.Add("患者オーダー");
                tmpTable.Columns.Add("オーダー番号");
                tmpTable.Columns.Add("日付");
                tmpTable.Columns.Add("日付未定");
                tmpTable.Columns.Add("施行部署");
                tmpTable.Columns.Add("種別");
                tmpTable.Columns.Add("送信種別");
                tmpTable.Columns.Add("施行済");
                tmpTable.Columns.Add("入外");
                tmpTable.Columns.Add("診療科");
                tmpTable.Columns.Add("医師");
                tmpTable.Columns.Add("内容");
                tmpTable.Columns.Add("送信", typeof(bool));

                tmpTable = dSet.Tables.Add("オーダーリスト");
                tmpTable.Columns.Add("MWM_ID");
                tmpTable.Columns.Add("オーダー番号");
                tmpTable.Columns.Add("日付");
                tmpTable.Columns.Add("日付未定");
                tmpTable.Columns.Add("受付番号");
                tmpTable.Columns.Add("施行部署");
                tmpTable.Columns.Add("種別");
                tmpTable.Columns.Add("送信種別");
                tmpTable.Columns.Add("施行済");
                tmpTable.Columns.Add("時刻");
                tmpTable.Columns.Add("PT_ID");
                tmpTable.Columns.Add("カナ");
                tmpTable.Columns.Add("氏名");
                tmpTable.Columns.Add("性別");
                tmpTable.Columns.Add("生年月日");
                tmpTable.Columns.Add("年齢");
                tmpTable.Columns.Add("入外");
                tmpTable.Columns.Add("診療科");
                tmpTable.Columns.Add("医師");
                tmpTable.Columns.Add("指示日");
                tmpTable.Columns.Add("内容");
                tmpTable.Columns.Add("STATUS");
                tmpTable.Columns.Add("Obj", typeof(MWMOrder));

                this.PtListShow();
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex);
                this.Dispose();
            }
        }

        private void MWMForm_Shown(object sender, EventArgs e)
        {
            // MWM連携ファイルの出力先フォルダの確認
            List<string> errs = new List<string>();

            if (!Directory.Exists(this.Settings.Path))
            {
                errs.Add("MWM連携ファイルの出力先フォルダ " + this.Settings.Path + " が存在しません");
            }
            else
            {
                FileAttributes fas = File.GetAttributes(this.Settings.Path);

                if ((fas & FileAttributes.Directory) != FileAttributes.Directory)
                {
                    errs.Add(this.Settings.Path + " はフォルダではありません");
                }

                if ((fas & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    errs.Add(this.Settings.Path + " は読み取り専用です。書き込み権限を付与してください");
                }
            }

            if (errs.Count > 0)
            {
                MessageBox.Show(AppString.ConcatList(errs, Environment.NewLine));
            }

            // 60秒以上に設定されていれば有効とする
            if (Settings.Interval >= 60)
            {
                this._Timer.Interval = Settings.Interval * 1000;
                this._Timer.Tick += new EventHandler(_Timer_Tick);
            }
            else
            {
                this.AutoBox.Visible = false;
            }
        }

        void _Timer_Tick(object sender, EventArgs e)
        {
            // 未送信のものを送信する
            Console.WriteLine("ticked");

            // カレンダーを今日にする
            this.OrderDateTimePicker.Value = DateTime.Now;

            // 患者一覧を更新する
            this.PtListShow();

            List<MWMData> mwmList = new List<MWMData>();
            List<MWMSent> sentList = new List<MWMSent>();

            foreach (DataGridViewRow r in this.PtListView.Rows)
            {
                // 送信済または取消済のものは飛ばす
                if (r.Cells["STATUS"].Value.ToString().Length > 0) continue;

                MWMOrder obj = (MWMOrder)r.Cells["Obj"].Value;

                MWMData data = new MWMData();
                data.OrderId = obj.OrderId;
                data.PtId = obj.Pat.Id;
                data.Name = obj.Pat.Name;
                data.Kana = obj.Pat.Kana;
                data.Rome = "";
                data.Sex = obj.Pat.Sex;
                data.Birth = obj.Pat.Birth;

                data.KensaDate = DateTime.Now.ToString("yyyyMMdd");
                data.Modality = this.Settings.Name;
                data.MwmId = MWMSent.GetMwmId(obj.OrderId);
                data.Status = "0";

                if (HandleButton1.Checked)
                {
                    data.Handle = "1";
                }
                else
                {
                    data.Handle = "2";
                }

                mwmList.Add(data);

                MWMSent sent = new MWMSent();
                sent.MwmId = data.MwmId;
                sent.OrderId = obj.OrderId;
                sent.Status = "0";

                sentList.Add(sent);
            }

            this.Send(mwmList, sentList);
        }

        void InitShow(string[] args = null)
        {
			List<string> errs = new List<string>();

			for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Equals("-f", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (i < args.Length - 1)
                    {
                        this.SettingsFile = AppFile.FilePath(args[i + 1]);

						if (File.Exists(this.SettingsFile))
						{
							this.Settings.Init(this.SettingsFile);
						}
						else
						{
							errs.Add("設定ファイル " + this.SettingsFile + " が存在しません");
						}
                    }

					break;
                }
            }

            this.Text = "MWM " + this.Settings.Title;

            if (this.Settings.Handle.Equals("Start"))
            {
                HandleButton1.Checked = true;
            }
            else if (this.Settings.Handle.Equals("Reserve"))
            {
                HandleButton2.Checked = true;
            }

            if (this.Settings.CommentText.Length > 0)
            {
                CommentLabel.BackColor = Color.LightYellow;
                CommentPanel.BorderStyle = BorderStyle.FixedSingle;

                CommentLabel.Text = this.Settings.CommentText;

                foreach (MWMCommentItem obj in this.Settings.CommentItemList)
                {
                    RadioButton tmpBox = new RadioButton();
                    tmpBox.Text = obj.Text;
                    tmpBox.Tag = obj.Value;
                    tmpBox.AutoSize = true;

                    if (obj.Id.Equals(this.Settings.CommentId))
                    {
                        tmpBox.Checked = true;
                    }

                    CommentPanel.Controls.Add(tmpBox);
                }
            }

            if (this.Settings.Mode.Equals("1"))
            {
                ModeBox0.Checked = false;
                ModeBox1.Checked = true;
            }
            else
            {
                ModeBox0.Checked = true;
                ModeBox1.Checked = true;
            }

			if (errs.Count > 0)
			{
				throw new Exception(AppString.ConcatList(errs, Environment.NewLine));
			}
        }

        private void PtInfoShow()
        {
            int i = 0;

            if (PtIdBox.Text.Length == 0 || !int.TryParse(PtIdBox.Text, out i))
            {
                return;
            }

            this._Pat = PatBase.Load(PtIdBox.Text);

            this.PtInfoBox.Text = this.Pat.Info1;

            this.PtSeqBox.Clear();
            List<PatOut> tmpList = PatOut.GetOnedayLast(PtIdBox.Text, OrderDateTimePicker.Value.ToString("yyyyMMdd"));

            foreach (PatOut p in tmpList)
            {
                this.PtSeqBox.Text = p.Seq1;
                break;
            }
        }

        /// <summary>
        /// 患者ごとのオーダーを表示する。
        /// </summary>
        private void PtOrderShow()
        {
            DataTable tmpTable = dSet.Tables["患者オーダー"];
            tmpTable.Clear();

            List<PatOrder> order_list = new List<PatOrder>();

            if (this.Settings.Kind == MWMKind.SekouCode)
            {
                List<string> cond_list = new List<string>();
#if INNO
                cond_list.Add("SEKOU_CODE in (" + AppString.ConcatList(this.Settings.CodeList, ",") + ")");
                order_list = PatOrder.GetListByPatCond(this.Pat.Id, cond_list, "ORDER_DATE desc", true);
#else
                cond_list.Add("施行部署１ in (" + AppString.ConcatList(this.Settings.CodeList, ",") + ")");
                order_list = PatOrder.GetListByPatCond(this.Pat.Id, cond_list, "施行予定日 desc", true);
#endif
            }
            else if (this.Settings.Kind == MWMKind.OrderCode)
            {
                order_list = PatOrder.GetListByPatOrderCode(this.Pat.Id, this.Settings.CodeList, true);
            }

            foreach (PatOrder order in order_list)
            {
                DataRow r = tmpTable.NewRow();

                r["オーダー番号"] = order.OrderId;

                if (order.SekouDate.Equals("99999999"))
                {
                    r["日付未定"] = "●";
                }
                else
                {
                    r["日付"] = order.SekouDate.Insert(4, "/").Insert(7, "/");
                }

                r["施行部署"] = order.Sekou1;

                if (this.Settings.Kind == MWMKind.SekouCode)
                {
                    if (Dict.SekouDict.ContainsKey(order.Sekou1))
                    {
                        r["種別"] = Dict.SekouDict[order.Sekou1].ShortName;
                    }

                    if (this.Settings.ModalityDict.ContainsKey(order.Sekou1))
                    {
                        r["送信種別"] = this.Settings.ModalityDict[order.Sekou1];
                    }
                }
                else
                {
                    r["種別"] = this.Settings.Name;
                    r["送信種別"] = this.Settings.Name;
                }

                r["施行済"] = order.SekouFlg;
                r["入外"] = order.InOutNameShort;
                r["診療科"] = order.DeptName;
                r["医師"] = order.DoctorName;
                r["内容"] = order.SOAP;

                if (PtListView.SelectedCells.Count > 0)
                {
                    if (PtListView.Rows[PtListView.SelectedCells[0].RowIndex].Cells["オーダー番号"].Value.ToString().Equals(order.OrderId))
                    {
                        r["送信"] = true;
                    }
                }

                tmpTable.Rows.Add(r);
            }

            this.PtOrderFilter();
        }

        private void PtOrderFilter()
        {
            DataView tmpView = new DataView(dSet.Tables["患者オーダー"]);

            if (!PastBox.Checked)
            {
                tmpView.RowFilter = "送信 = true";
            }

            PtOrderView.DataSource = tmpView;

            PtOrderView.Columns["オーダー番号"].Visible = false;

            PtOrderView.Columns["日付"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PtOrderView.Columns["日付"].Width = 75;

            PtOrderView.Columns["日付未定"].HeaderText = "未定";
            PtOrderView.Columns["日付未定"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PtOrderView.Columns["日付未定"].Width = 35;

            PtOrderView.Columns["施行部署"].Visible = false;

            PtOrderView.Columns["種別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PtOrderView.Columns["種別"].Width = 80;

            PtOrderView.Columns["送信種別"].Visible = false;

            PtOrderView.Columns["施行済"].Visible = false;

            PtOrderView.Columns["入外"].Width = 35;
            PtOrderView.Columns["入外"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            PtOrderView.Columns["診療科"].Width = 70;
            PtOrderView.Columns["診療科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            PtOrderView.Columns["医師"].Width = 80;
            PtOrderView.Columns["医師"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            PtOrderView.Columns["内容"].Width = 560;
            PtOrderView.Columns["内容"].DefaultCellStyle.Font = new Font("ＭＳ Ｐゴシック", 8);

            PtOrderView.Columns["送信"].Width = 35;

            foreach (DataGridViewRow r in PtOrderView.Rows)
            {
                if (r.Cells["送信"].Value.ToString().Equals("true", StringComparison.CurrentCultureIgnoreCase))
                {
                    r.Selected = true;
                    break;
                }
            }
        }

        private void PtListShow()
        {
            DataTable tmpTable = dSet.Tables["オーダーリスト"];
            tmpTable.Clear();

            List<MWMOrder> tmpList = MWMOrder.Load(OrderDateTimePicker.Value.ToString("yyyyMMdd"), this.Settings.CodeList, this.Settings.Kind);

            List<string> pt_id_list = new List<string>();

            foreach (MWMOrder p in tmpList)
            {
                if (!pt_id_list.Contains(p.Pat.Id))
                {
                    pt_id_list.Add(p.Pat.Id);
                }
            }

            Dictionary<string, string> tmpDict = PatOut.GetOnedayLastSeq(pt_id_list, OrderDateTimePicker.Value.ToString("yyyyMMdd"));

            foreach (MWMOrder p in tmpList)
            {
                DataRow r = tmpTable.NewRow();

                r["MWM_ID"] = p.MwmId;
                r["オーダー番号"] = p.OrderId;
                r["日付"] = p.SekouDate;

                if (p.SekouDate.Equals("99999999"))
                {
                    r["日付未定"] = "●";
                }

                r["施行部署"] = p.Sekou1;

                if (tmpDict.ContainsKey(p.Pat.Id))
                {
                    r["受付番号"] = tmpDict[p.Pat.Id];
                }

                if (this.Settings.Kind == MWMKind.SekouCode)
                {
                    r["種別"] = p.SekouName1;

                    if (this.Settings.ModalityDict.ContainsKey(p.Sekou1))
                    {
                        r["送信種別"] = this.Settings.ModalityDict[p.Sekou1];
                    }
                }
                else
                {
                    r["種別"] = this.Settings.Name;
                    r["送信種別"] = this.Settings.Name;
                }

                if (p.SekouFlg.Equals("1"))
                {
                    r["施行済"] = "○";
                }

                r["時刻"] = p.SekouTimeString;
                r["PT_ID"] = p.Pat.Id;
                r["カナ"] = p.Pat.Kana;
                r["氏名"] = p.Pat.Name;
                r["性別"] = p.Pat.SexNameEng;
                r["生年月日"] = p.Pat.BirthString;
                r["年齢"] = p.Pat.Age;
                r["入外"] = p.InOutNameShort;
                r["診療科"] = p.DeptName;
                r["医師"] = p.DoctorName;
                r["指示日"] = p.OrderDateString;
                r["内容"] = p.SOAP;

                if (p.Status.Equals("1"))
                {
                    r["STATUS"] = "取消";
                }
                else if (p.Status.Equals("0"))
                {
                    r["STATUS"] = "○";
                }

                r["Obj"] = p;

                tmpTable.Rows.Add(r);
            }

            this.PtListFilter();
        }

        private void PtListFilter()
        {
            if (!dSet.Tables.Contains("オーダーリスト"))
            {
                return;
            }

            DataTable table = dSet.Tables["オーダーリスト"];
            DataView tmpView = new DataView(table);

            string filter = "";

            if (!ModeBox0.Checked && ModeBox1.Checked)
            {
                filter = "施行済 = '○'";
            }
            else if (ModeBox0.Checked && !ModeBox1.Checked)
            {
                filter = "施行済 is null";
            }
            else if (!ModeBox0.Checked && !ModeBox1.Checked)
            {
                filter = "施行済 = '2'";
            }

            if (!NextBox.Checked)
            {
                if (filter.Length > 0)
                {
                    filter += " and ";
                }

                filter += "日付未定 is null";
            }

            tmpView.RowFilter = filter;

            PtListView.DataSource = tmpView;

            PtListView.Columns["MWM_ID"].Visible = false;

            PtListView.Columns["オーダー番号"].Visible = false;

            PtListView.Columns["日付"].Visible = false;

            PtListView.Columns["日付未定"].HeaderText = "未定";
            PtListView.Columns["日付未定"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PtListView.Columns["日付未定"].Width = 35;

            PtListView.Columns["受付番号"].HeaderText = "受付";
            PtListView.Columns["受付番号"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PtListView.Columns["受付番号"].Width = 35;

            PtListView.Columns["施行部署"].Visible = false;

            PtListView.Columns["種別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PtListView.Columns["種別"].Width = 60;

            PtListView.Columns["送信種別"].Visible = false;

            PtListView.Columns["施行済"].HeaderText = "施済";
            PtListView.Columns["施行済"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PtListView.Columns["施行済"].Width = 40;

            PtListView.Columns["時刻"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PtListView.Columns["時刻"].Width = 45;

            PtListView.Columns["PT_ID"].HeaderText = "ID";
            PtListView.Columns["PT_ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            PtListView.Columns["PT_ID"].Width = 55;

            PtListView.Columns["カナ"].Width = 70;

            PtListView.Columns["氏名"].Width = 80;

            PtListView.Columns["性別"].Width = 35;
            PtListView.Columns["性別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PtListView.Columns["性別"].Visible = false;

            PtListView.Columns["生年月日"].Visible = false;

            PtListView.Columns["年齢"].Width = 35;
            PtListView.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            PtListView.Columns["入外"].Width = 35;
            PtListView.Columns["入外"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            PtListView.Columns["診療科"].Width = 60;
            PtListView.Columns["診療科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            PtListView.Columns["医師"].Width = 70;
            PtListView.Columns["医師"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            PtListView.Columns["指示日"].Width = 70;
            PtListView.Columns["指示日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            PtListView.Columns["内容"].Width = 240;

            PtListView.Columns["STATUS"].HeaderText = "送信";
            PtListView.Columns["STATUS"].Width = 40;
            PtListView.Columns["STATUS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            PtListView.Columns["Obj"].Visible = false;

            foreach (DataGridViewRow r in PtListView.Rows)
            {
                if (r.Cells["性別"].Value.ToString().Equals("F"))
                {
                    r.Cells["カナ"].Style.ForeColor = Color.Red;
                    r.Cells["氏名"].Style.ForeColor = Color.Red;
                }

                if (r.Cells["施行済"].Value.ToString().Equals("○"))
                {
                    r.DefaultCellStyle.BackColor = Color.LightCyan;
                }
            }
        }

        /// <summary>
        /// ステータスの変更。
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="status">0 ○, 1 取消</param>
        void PtListStatusChange(string order_id, string status)
        {
            foreach (DataRow r in dSet.Tables["オーダーリスト"].Rows)
            {
                if (r["オーダー番号"].ToString().Equals(order_id))
                {
                    if (status.Equals("1"))
                    {
                        r["STATUS"] = "取消";
                    }
                    else if (status.Equals("0"))
                    {
                        r["STATUS"] = "○";
                    }
                    else
                    {
                        r["STATUS"] = "";
                    }

                    break;
                }
            }

            PtListFilter();
        }

        private void FileExitMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void NextBox_CheckedChanged(object sender, EventArgs e)
        {
            this.PtListFilter();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            this.PtListShow();
        }

        private void PtIdBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.PastBox.Checked = true;
                this.PtInfoShow();
                this.PtOrderShow();
            }
        }

        private void PtListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.PtIdBox.Text = PtListView.Rows[e.RowIndex].Cells["PT_ID"].Value.ToString();
            this.PastBox.Checked = false;

            this.PtInfoShow();
            this.PtOrderShow();
        }

        private void PastBox_CheckedChanged(object sender, EventArgs e)
        {
            this.PtOrderFilter();
        }

        private void MakeCSVButton_Click(object sender, EventArgs e)
        {
            if (PtIdBox.Text.Length == 0)
            {
                MessageBox.Show("患者IDを入力してください");
                return;
            }

            List<MWMData> mwmList = new List<MWMData>();
            List<MWMSent> sentList = new List<MWMSent>();

            foreach (DataGridViewRow r in PtOrderView.Rows)
            {
                if (r.Cells["送信"].Value.ToString().Equals("true", StringComparison.CurrentCultureIgnoreCase))
                {
                    MWMData data = new MWMData();
                    data.OrderId = r.Cells["オーダー番号"].Value.ToString();
                    data.PtId = this.PtIdBox.Text.PadLeft(8, '0');
                    data.Name = Pat.Name;
                    data.Kana = Pat.Kana;
                    data.Rome = "";
                    data.Sex = Pat.Sex;
                    data.Birth = Pat.Birth;

                    foreach (Control c in CommentPanel.Controls)
                    {
                        if (c.GetType().Name.Equals("RadioButton"))
                        {
                            if (((RadioButton)c).Checked)
                            {
                                data.Comment = c.Tag.ToString();
                                break;
                            }
                        }
                    }

                    data.KensaDate = r.Cells["日付"].Value.ToString().Replace("/", "");
                    data.Modality = r.Cells["送信種別"].Value.ToString();
                    data.MwmId = MWMSent.GetMwmId(r.Cells["オーダー番号"].Value.ToString());
                    data.Status = "0";

                    if (HandleButton1.Checked)
                    {
                        data.Handle = "1";
                    }
                    else
                    {
                        data.Handle = "2";
                    }

                    mwmList.Add(data);

                    MWMSent sent = new MWMSent();
                    sent.MwmId = data.MwmId;
                    sent.OrderId = r.Cells["オーダー番号"].Value.ToString();
                    sent.Status = "0";

                    sentList.Add(sent);
                }
            }

            if (mwmList.Count == 0)
            {
                MessageBox.Show("送信するオーダーを選択してください");
                return;
            }

            // MWM連携ファイルの出力先フォルダの確認
            List<string> errs = new List<string>();

            if (!Directory.Exists(this.Settings.Path))
            {
                errs.Add("MWM連携ファイルの出力先フォルダ " + this.Settings.Path + " が存在しません");
            }
            else
            {
                FileAttributes fas = File.GetAttributes(this.Settings.Path);

                if ((fas & FileAttributes.Directory) != FileAttributes.Directory)
                {
                    errs.Add(this.Settings.Path + " はフォルダではありません");
                }

                if ((fas & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    errs.Add(this.Settings.Path + " は読み取り専用です。書き込み権限を付与してください");
                }
            }

            if (errs.Count > 0)
            {
                MessageBox.Show(AppString.ConcatList(errs, Environment.NewLine));
                return;
            }

            this.Send(mwmList, sentList);

            MessageBox.Show("送信しました");
        }

        void Send(List<MWMData> mwmList, List<MWMSent> sentList)
        {
            if (mwmList.Count == 0) return;

            try
            {
                // １つ１つ分けて送る
                List<MWMData> list = new List<MWMData>();

                foreach (MWMData data in mwmList)
                {
                    list.Clear();
                    list.Add(data);
                    MWMData.MakeCSV(list, this.Settings.Path);
                }

                foreach (MWMSent s in sentList)
                {
                    s.Save();
                    this.PtListStatusChange(s.OrderId, "0");
                }
            }
            finally
            {
            }
        }

        private void SentDelMenuItem_Click(object sender, EventArgs e)
        {
            if (PtListView.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("取消しますか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                {
                    DataGridViewRow r = PtListView.SelectedRows[0];

                    List<MWMData> tmpList = new List<MWMData>();

                    MWMData tmpData = new MWMData();
                    tmpData.OrderId = r.Cells["オーダー番号"].Value.ToString();
                    tmpData.PtId = r.Cells["PT_ID"].Value.ToString();
                    tmpData.Name = r.Cells["氏名"].Value.ToString();
                    tmpData.Kana = r.Cells["カナ"].Value.ToString();
                    tmpData.Rome = "";

                    if (r.Cells["性別"].Value.ToString().Equals("M"))
                    {
                        tmpData.Sex = "1";
                    }
                    else if (r.Cells["性別"].Value.ToString().Equals("F"))
                    {
                        tmpData.Sex = "2";
                    }

                    tmpData.Birth = r.Cells["生年月日"].Value.ToString();
                    tmpData.KensaDate = r.Cells["日付"].Value.ToString().Replace("/", "");
                    tmpData.Modality = r.Cells["送信種別"].Value.ToString();
                    tmpData.MwmId = r.Cells["MWM_ID"].Value.ToString();
                    tmpData.Status = "1";

                    tmpList.Add(tmpData);

                    MWMData.MakeCSV(tmpList, this.Settings.Path);

                    MWMSent tmpSent = new MWMSent();
                    tmpSent.MwmId = r.Cells["MWM_ID"].Value.ToString();
                    tmpSent.OrderId = r.Cells["オーダー番号"].Value.ToString();
                    tmpSent.Status = "1";

                    tmpSent.Save();

                    MessageBox.Show("取消しました");

                    this.PtListStatusChange(tmpSent.OrderId, "1");
                }
            }
        }

        private void SentListMenu_Opening(object sender, CancelEventArgs e)
        {
            if (PtListView.SelectedRows.Count > 0)
            {
                if (PtListView.SelectedRows[0].Cells["STATUS"].Value.ToString().Equals("○"))
                {
                    SentListMenu.Items[0].Enabled = true;
                }
                else
                {
                    SentListMenu.Items[0].Enabled = false;
                }
            }
            else
            {
                SentListMenu.Items[0].Enabled = false;
            }
        }

        private void MWMForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                PtListShow();
            }
        }

        private void ModeBox0_CheckedChanged(object sender, EventArgs e)
        {
            PtListFilter();
        }

        private void ModeBox1_CheckedChanged(object sender, EventArgs e)
        {
            PtListFilter();
        }

        private void AutoBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.AutoBox.Checked)
            {
                if (MessageBox.Show("自動送信モードにします。よろしいですか？", "確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this._Timer.Enabled = true;
                    this.AutoBox.ForeColor = Color.Red;
                    this.AutoBox.BackColor = Color.LightYellow;
                    Console.WriteLine("Start");
                }
                else
                {
                    this._Timer.Enabled = false;
                    this.AutoBox.Checked = false;
                    this.AutoBox.ForeColor = Color.Black;
                    this.AutoBox.BackColor = Color.Transparent;
                    Console.WriteLine("Stop");
                }
            }
            else
            {
                this._Timer.Enabled = false;
                this.AutoBox.ForeColor = Color.Black;
                this.AutoBox.BackColor = Color.Transparent;
                Console.WriteLine("Stop");
            }
        }
    }

    enum Mode : int
    {
        Yet = 0,
        Done = 1
    }
}