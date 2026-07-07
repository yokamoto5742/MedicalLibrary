using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Boundary;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class FormComeReportPat : StdForm1
    {
        static List<FormComeReportPat> FormPatList = new List<FormComeReportPat>();

        public static FormComeReportPat FormShow(string pt_id = "")
        {
            FormComeReportPat f = null;

            foreach (FormComeReportPat ff in FormPatList)
            {
                if (ff.Pat.Id.Equals(pt_id))
                {
                    f = ff;
                    break;
                }
            }

            if (f == null || !f.Created)
            {
                f = new FormComeReportPat(pt_id);
                FormPatList.Add(f);
            }

            f.Show();
            f.BringToFront();
            f.WindowState = FormWindowState.Normal;

            return f;
        }

        public static FormComeReportPat FormShow(string pt_id = "", string order_id = "")
        {
            FormComeReportPat f = null;

            foreach (FormComeReportPat ff in FormPatList)
            {
                if (ff.Pat.Id.Equals(pt_id))
                {
                    f = ff;
                    f.OrderId = order_id;
                    f.ShowPtData();
                    break;
                }
            }

            if (f == null || !f.Created)
            {
                f = new FormComeReportPat(pt_id, order_id);
                FormPatList.Add(f);
            }

            f.Show();
            f.BringToFront();
            f.WindowState = FormWindowState.Normal;

            return f;
        }


        DataSet DSet = new DataSet();

        /// <summary>
        /// 所見の内容を保持する Report オブジェクトのリスト
        /// </summary>
        List<ComeReportData> ReportList = new List<ComeReportData>();

        /// <summary>
        /// あるオーダーにおける部位のリスト
        /// </summary>
        List<string> PartList = new List<string>();


        FormComeReportTemplate Fr;

        string OrderId = "";

        /// <summary>
        /// シェーマ選択ダイアログ
        /// </summary>
        FormComeReportSchema Fs;

        /// <summary>
        /// メイン SchemaBox のシェーマ
        /// </summary>
        public ComeReportSchema GSchema = new ComeReportSchema();

        /// <summary>
        /// 0 選択状態
        /// 1 直線描画, 2 楕円描画, 3 四角描画, 4 文字描画, 5 フリー描画
        /// 11 直線移動（始点確定）, 21 直線移動（終点確定）
        /// 12 楕円移動（始点確定）, 22 楕円移動（終点確定）
        /// 13 四角移動（始点確定）, 23 四角移動（終点確定）
        /// </summary>
        int GMode = 0;

        /// <summary>
        /// メイン SchemaBox 上で描画する際の始点。
        /// </summary>
        Point GStart = new Point();

        /// <summary>
        /// メイン SchemaBox 上で描画する際の終点。
        /// </summary>
        Point GEnd = new Point();

        /// <summary>
        /// メイン SchemaBox に描画されたシェーマアイテム上の点。
        /// </summary>
        List<Point> GPointList = new List<Point>();

        /// <summary>
        /// シェーマアイテムのサイズ変更時、クリックポイントと始点・終点との間隔
        /// </summary>
        int GMarginX = 0;
        int GMarginY = 0;

        /// <summary>
        /// メイン SchemaBox で現在選択されているシェーマアイテム
        /// </summary>
        ComeReportSchemaItem TmpItem;

        /// <summary>
        /// メイン SchemaBox で現在選択されているシェーマ文字ラベル
        /// </summary>
        Label TmpLabel;

        /// <summary>
        /// 描画色
        /// </summary>
        Color GColor = Color.Magenta;

        /// <summary>
        /// 描画ペン
        /// </summary>
        Pen GPen = new Pen(Brushes.Magenta, 2);

        /// <summary>
        /// 選択ペン
        /// </summary>
        Pen SPen = new Pen(Brushes.Red, 3);

        public FormComeReportPat(string ptId)
        {
            InitializeComponent();

            this.PatSet(PatBase.Load(ptId));
        }

        public FormComeReportPat(string ptId, string orderId)
        {
            InitializeComponent();

            this.OrderId = orderId;
            this.PatSet(PatBase.Load(ptId));
        }

        private void FormComeReportPat_Load(object sender, EventArgs e)
        {
            DataTable tmpTable = this.DSet.Tables.Add("オーダー");
            tmpTable.Columns.Add("オーダー番号");
            tmpTable.Columns.Add("指示日");
            tmpTable.Columns.Add("施行日");
            tmpTable.Columns.Add("施行部署１");
            tmpTable.Columns.Add("種別");
            tmpTable.Columns.Add("科コード");
            tmpTable.Columns.Add("科");
            tmpTable.Columns.Add("指示医コード");
            tmpTable.Columns.Add("指示医");
            tmpTable.Columns.Add("入外区分");
            tmpTable.Columns.Add("入外");
            tmpTable.Columns.Add("保険ビット");
            tmpTable.Columns.Add("保険");
            tmpTable.Columns.Add("内容");
            tmpTable.Columns.Add("施行");
            tmpTable.Columns.Add("院外");
            tmpTable.Columns.Add("記録");
            tmpTable.Columns.Add("完成");
            tmpTable.Columns.Add("Obj", typeof(ComeReportOrder));

            this.GSchema.SchemaBox = GSchemaBox;
            this.GSchema_Reset();

            this.GSchemaIdBox.Text = "";

            ColorBox1.BorderStyle = BorderStyle.Fixed3D;
            GColor = Color.Magenta;
            GPen = new Pen(GColor, 2);

            GSchemaBox.AllowDrop = true;
            GSchemaBox.DragEnter += new DragEventHandler(GSchemaBox_DragEnter);
            GSchemaBox.DragDrop += new DragEventHandler(GSchemaBox_DragDrop);

            if (ComeReportSettings.Current.ImgHostVisible.Equals("1"))
            {
                this.OpenSchemaFileButton.Visible = true;
            }
            else
            {
                this.OpenSchemaFileButton.Visible = false;
            }

            if (this.Pat.Id.Length > 0)
            {
                this.ShowPtData();
            }
            else
            {
                this.stdControlPat11.Select();
            }
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            this.ShowPtData();
        }

        public void MakeOrderHistory(string selectedOrderId)
        {
            this.MakeOrderHistory();

            if (selectedOrderId.Length > 0)
            {
                foreach (DataGridViewRow r in this.HistoryView.Rows)
                {
                    if (r.Cells["オーダー番号"].Value.ToString().Equals(selectedOrderId))
                    {
                        r.Selected = true;
                        break;
                    }
                }
            }
        }

        public void MakeOrderHistory()
        {
            if (this.Pat.Id.Length > 0 &&  DSet.Tables.Contains("オーダー"))
            {
                // ＮＴオーダーヘッダーからオーダーデータを取得する
                DataTable tmpTable = DSet.Tables["オーダー"];
                tmpTable.Clear();

                // 該当患者のオーダーデータを取得する
                List<ComeReportOrder> list = ComeReportOrder.GetList1(this.Pat.Id, ComeReportSettings.Current.GroupDict["0"].Kensa.Split(','), true);

                foreach (ComeReportOrder obj in list)
                {
                    DataRow r = tmpTable.NewRow();

                    r["オーダー番号"] = obj.OrderId;
                    r["指示日"] = obj.OrderDate;
                    r["施行日"] = DateTimeAgent.DateFormat(obj.SekouDate, DateTimeAgent.DateFormatKind.LONG);
                    r["施行部署１"] = obj.Sekou1;
                    r["種別"] = obj.SekouName1;
                    r["科コード"] = obj.Dept;
                    r["科"] = obj.DeptName;
                    r["指示医コード"] = obj.Doctor;
                    r["指示医"] = obj.DoctorName;
                    r["入外区分"] = obj.InOut;
                    r["入外"] = obj.InOutName;
                    r["保険ビット"] = obj.Pat.Ins;
                    r["保険"] = obj.Pat.InsKindName;
                    r["内容"] = obj.SOAP;
                    r["施行"] = obj.SekouFlg.Equals("1") ? "○" : "";
                    r["院外"] = obj.OutSideFlg ? "○" : "";
                    r["記録"] = obj.RecordFlg ? "○" : "";
                    r["完成"] = obj.ReportListString;
                    r["Obj"] = obj;

                    tmpTable.Rows.Add(r);
                }

                HistoryView.DataSource = new DataView(tmpTable);

                HistoryView.Columns["オーダー番号"].Visible = false;

                HistoryView.Columns["指示日"].Visible = false;

                HistoryView.Columns["施行日"].Width = 65;
                HistoryView.Columns["施行日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                HistoryView.Columns["施行部署１"].Visible = false;
                HistoryView.Columns["種別"].Width = 40;

                HistoryView.Columns["科コード"].Visible = false;
                HistoryView.Columns["科"].Width = 60;

                HistoryView.Columns["指示医コード"].Visible = false;
                HistoryView.Columns["指示医"].Width = 60;

                HistoryView.Columns["入外区分"].Visible = false;
                HistoryView.Columns["入外"].Width = 35;
                HistoryView.Columns["入外"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                HistoryView.Columns["保険ビット"].Visible = false;
                HistoryView.Columns["保険"].Width = 35;
                HistoryView.Columns["保険"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                HistoryView.Columns["内容"].Width = 270;
                HistoryView.Columns["内容"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                HistoryView.Columns["施行"].Width = 35;
                HistoryView.Columns["施行"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                HistoryView.Columns["院外"].Width = 35;
                HistoryView.Columns["院外"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                HistoryView.Columns["記録"].Width = 35;
                HistoryView.Columns["記録"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                HistoryView.Columns["完成"].Width = 100;
                HistoryView.Columns["完成"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                HistoryView.Columns["Obj"].Visible = false;
            }
        }

        /// <summary>
        /// オーダーディテールから FormPat に診断・目的・部位・コメントをセットする。
        /// </summary>
        /// <param name="orderId"></param>
        public void MakeOrderDetail(string kensaCode, string orderId)
        {
            if (orderId.Length > 0)
            {
                // オーダーディテールから取得したデータを OrderDetail オブジェクト化してリストに登録する。
                List<PatOrderDetail> tmpList = PatOrderDetail.Load(orderId);

                // オリジナルオーダーの印字
                string orderOrg = "";

                foreach (PatOrderDetail detail in tmpList)
                {
                    if (orderOrg.Length > 0)
                    {
                        orderOrg += Environment.NewLine;
                    }

                    orderOrg += detail.Name;

                    if (detail.Unit.Length > 0 || !detail.Qty.Equals(1))
                    {
                        orderOrg += " " + detail.Qty + detail.Unit;
                    }

                    if (!detail.Times.Equals(1) && !detail.Times.Equals(0))
                    {
                        orderOrg += " ×" + detail.Times;
                    }
                }

                // オーダーディテールから診断・目的・部位・コメント等を取得する。
                List<ComeReportSettings.PrintSet> tmpPrintSetList = ComeReportSettings.Current.KensaDict[kensaCode].PrintSetList;

                string order = "";
                string orderCont = "";

                this.PartList.Clear();

                for (int i = 0; i < tmpPrintSetList.Count; i++)
                {
                    ComeReportSettings.PrintSet tmpPrintSet = tmpPrintSetList[i];

                    if (order.Length > 0)
                    {
                        order += "\r\n";
                    }

                    order += "【" + tmpPrintSet.Word + "】\r\n";

                    orderCont = "";

                    for (int j = 0; j < tmpPrintSet.ElemList.Count; j++)
                    {
                        ComeReportSettings.PrintElement tmpElement = tmpPrintSet.ElemList[j];

                        for (int k = 0; k < tmpList.Count; k++)
                        {
                            if (tmpList[k].Code.Equals(tmpElement.Code))
                            {
                                if (tmpElement.Kind.Equals("0"))
                                {
                                    if (orderCont.Length > 0)
                                    {
                                        orderCont += " ";
                                    }

                                    orderCont += tmpList[k].Name;

                                    // 部位ならばリストに追加する
                                    if (tmpPrintSet.Word.Equals("部位"))
                                    {
                                        PartList.Add(tmpList[k].Name);
                                    }
                                }
                                else if (tmpElement.Kind.Equals("1"))
                                {
                                    if (k < tmpList.Count - 1 && (tmpList[k + 1].Code.Equals("88888888") || tmpList[k + 1].Code.Equals("88888889")))
                                    {
                                        if (orderCont.Length > 0)
                                        {
                                            orderCont += " ";
                                        }

                                        orderCont += tmpList[k + 1].Name;

                                        // 部位ならばリストに追加する
                                        if (tmpPrintSet.Word.Equals("部位"))
                                        {
                                            PartList.Add(tmpList[k + 1].Name);
                                        }
                                    }
                                }
                                else if (tmpElement.Kind.Equals("3"))
                                {
                                    if (orderCont.Length > 0)
                                    {
                                        orderCont += " ";
                                    }

                                    orderCont += tmpList[k].QtyString;
                                }
                                else if (tmpElement.Kind.Equals("4"))
                                {
                                    if (orderCont.Length > 0)
                                    {
                                        orderCont += " ";
                                    }

                                    orderCont += tmpList[k].Times.ToString();
                                }
                            }
                        }
                    }

                    // 該当項目の単位が空でない場合には単位をつける。
                    if (tmpPrintSet.Unit.Length > 0)
                    {
                        orderCont += " " + tmpPrintSet.Unit;
                    }

                    // 該当項目の内容が空でない場合には印字する。
                    if (orderCont.Length > 0)
                    {
                        order += orderCont;
                    }

                    order += "\r\n";
                }

                this.OrderBox.Text = order;
            }
        }

        private void MakeTab(ComeReportData report)
        {
            TabPage tmpPage = new TabPage();
            tmpPage.Name = report.Tab;
            tmpPage.Text = report.Tab;
            tmpPage.ContextMenuStrip = this.TabPageMenuStrip;

            TextBox reportIdBox = new TextBox();
            reportIdBox.Size = new Size(15, 20);
            reportIdBox.Location = new Point(330, 7);
            reportIdBox.Multiline = false;
            reportIdBox.Visible = false;
            reportIdBox.Name = "ReportIdBox";
            reportIdBox.Text = report.ReportId;
            tmpPage.Controls.Add(reportIdBox);

            Label staffLabel = new Label();
            staffLabel.AutoSize = true;
            staffLabel.Location = new Point(5, 10);
            staffLabel.Name = "StaffLabel";
            staffLabel.Text = "作成者";
            tmpPage.Controls.Add(staffLabel);

            //            TextBox staffNameBox = new TextBox();
            Label staffNameBox = new Label();
            staffNameBox.Size = new Size(80, 20);
            staffNameBox.Location = new Point(50, 6);
            staffNameBox.Name = "StaffNameBox";
            staffNameBox.Text = report.StaffName;
            staffNameBox.TextAlign = ContentAlignment.MiddleCenter;
            staffNameBox.BackColor = Color.White;
            tmpPage.Controls.Add(staffNameBox);

            TextBox staffCodeBox = new TextBox();
            staffCodeBox.Size = new Size(20, 20);
            staffCodeBox.Multiline = false;
            staffCodeBox.Location = new Point(133, 7);
            staffCodeBox.Name = "StaffCodeBox";
            staffCodeBox.Visible = false;
            staffCodeBox.Text = report.StaffCode;
            tmpPage.Controls.Add(staffCodeBox);

            Label saveDateLabel = new Label();
            saveDateLabel.AutoSize = true;
            saveDateLabel.Location = new Point(160, 10);
            saveDateLabel.Name = "SaveDateLabel";
            saveDateLabel.Text = "作成日時";
            tmpPage.Controls.Add(saveDateLabel);

            //            TextBox saveDateBox = new TextBox();
            Label saveDateBox = new Label();
            saveDateBox.Size = new Size(110, 20);
            saveDateBox.Location = new Point(215, 6);
            saveDateBox.Name = "SaveDateBox";
            saveDateBox.Text = report.SaveDateTime;
            saveDateBox.TextAlign = ContentAlignment.MiddleCenter;
            saveDateBox.BackColor = Color.White;
            tmpPage.Controls.Add(saveDateBox);

            Button templateButton = new Button();
            templateButton.Size = new Size(75, 22);
            templateButton.Location = new Point(435, 5);
            templateButton.Name = "TemplateButton";
            templateButton.Text = "テンプレート";
            templateButton.Click += new EventHandler(TemplateButton_Click);
            tmpPage.Controls.Add(templateButton);

            Button orderButton = new Button();
            orderButton.Size = new Size(55, 22);
            orderButton.Location = new Point(515, 5);
            orderButton.Name = "OrderButton";
            orderButton.Text = "オーダ";
            orderButton.Click += new EventHandler(OrderButton_Click);
            tmpPage.Controls.Add(orderButton);

            Label outSideLabel = new Label();
            outSideLabel.AutoSize = false;
            outSideLabel.Size = new Size(40, 20);
            outSideLabel.Location = new Point(350, 6);
            outSideLabel.Name = "OutSideLabel";
            outSideLabel.TextAlign = ContentAlignment.MiddleCenter;
            outSideLabel.DoubleClick += new EventHandler(OutSideLabel_DoubleClick);

            if (report.OutSide.Equals("1"))
            {
                outSideLabel.BackColor = Color.LightPink;
                outSideLabel.Text = "院外";
            }
            else
            {
                outSideLabel.BackColor = Color.White;
                outSideLabel.Text = "院内";
            }

            tmpPage.Controls.Add(outSideLabel);

            FlowLayoutPanel schemaPanel = new FlowLayoutPanel();
            schemaPanel.Size = new Size(135, 380);
            schemaPanel.Location = new Point(435, 30);
            schemaPanel.Name = "SchemaPanel";
            schemaPanel.BackColor = Color.DarkGray;
            schemaPanel.FlowDirection = FlowDirection.TopDown;
            schemaPanel.AutoScroll = true;

            this.RedrawSchemas(schemaPanel, report.SchemaList);

            tmpPage.Controls.Add(schemaPanel);

            TextBox contBox = new TextBox();
            contBox.Size = new Size(425, 410);
            contBox.Location = new Point(6, 30);
            contBox.Multiline = true;
            contBox.BackColor = Color.FromArgb(255, 255, 224);
            contBox.ScrollBars = ScrollBars.Vertical;
            contBox.ImeMode = ImeMode.Hiragana;
            contBox.Name = "ContBox";
            contBox.Text = report.Cont;
            contBox.TabIndex = 0;

            Button regButton = new Button();
            regButton.Size = new Size(60, 24);
            regButton.Location = new Point(510, 418);
            regButton.Name = "RegButton";
            regButton.Text = "登録";
            regButton.Click += new EventHandler(RegButton_Click);

            CheckBox statusBox = new CheckBox();
            statusBox.AutoSize = true;
            statusBox.Location = new Point(440, 425);
            statusBox.BackColor = Color.FromArgb(255, 255, 224);
            statusBox.Name = "StatusBox";
            statusBox.Text = "完成";

            if (report.Status.Equals("1"))
            {
                statusBox.Checked = true;
                contBox.ReadOnly = true;
                regButton.Enabled = false;
            }
            else if (report.OutSide.Equals("1"))
            {
                statusBox.Checked = false;
                contBox.ReadOnly = true;
                regButton.Enabled = false;
            }
            else
            {
                statusBox.Checked = false;
                contBox.ReadOnly = false;
                regButton.Enabled = true;
            }

            statusBox.Click += new EventHandler(StatusBox_Click);
            tmpPage.Controls.Add(statusBox);
            tmpPage.Controls.Add(regButton);
            tmpPage.Controls.Add(contBox);

            this.ReportTabControl.TabPages.Add(tmpPage);
        }

        void OrderButton_Click(object sender, EventArgs e)
        {
            if (this.OrderIdBox.Text.Length > 0)
            {
                foreach (DataGridViewRow d in this.HistoryView.Rows)
                {
                    if (d.Cells["オーダー番号"].Value.ToString().Equals(this.OrderIdBox.Text))
                    {
                        d.Selected = true;
                        break;
                    }
                }
            }
        }

        void OutSideLabel_DoubleClick(object sender, EventArgs e)
        {
            Label outSideLabel = (Label)(sender);
            CheckBox statusBox = (CheckBox)(this.ReportTabControl.SelectedTab.Controls["StatusBox"]);
            TextBox contBox = (TextBox)(this.ReportTabControl.SelectedTab.Controls["ContBox"]);
            Button regButton = (Button)(this.ReportTabControl.SelectedTab.Controls["RegButton"]);

            if (outSideLabel.Text.Equals("院内"))
            {
                outSideLabel.BackColor = Color.LightPink;
                outSideLabel.Text = "院外";

                contBox.ReadOnly = true;
            }
            else
            {
                outSideLabel.BackColor = Color.White;
                outSideLabel.Text = "院内";

                if (!statusBox.Checked)
                {
                    contBox.ReadOnly = false;
                    regButton.Enabled = true;
                }
            }
        }

        void StatusBox_Click(object sender, EventArgs e)
        {
            CheckBox statusBox = (CheckBox)(sender);
            TextBox contBox = (TextBox)(this.ReportTabControl.SelectedTab.Controls["ContBox"]);
            Button regButton = (Button)(this.ReportTabControl.SelectedTab.Controls["RegButton"]);
            Label outSideLabel = (Label)(this.ReportTabControl.SelectedTab.Controls["OutSideLabel"]);

            if (statusBox.Checked)
            {
                contBox.ReadOnly = true;
            }
            else
            {
                if (outSideLabel.Text.Equals("院内"))
                {
                    contBox.ReadOnly = false;
                    regButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// schemaList の内容に基づいてタブ上のシェーマを再描画する。
        /// </summary>
        private void RedrawSchemas(FlowLayoutPanel panel, List<ComeReportSchema> schemaList)
        {
            panel.Controls.Clear();

            // schemaList の内容に基づいて schemaPanel 中にシェーマを描画する。
            for (int i = 0; i < schemaList.Count; i++)
            {
                ComeReportSchema s = schemaList[i];

                PictureBox tmpBox = new PictureBox();
                tmpBox.Size = new Size(100, 100);
                tmpBox.BackColor = Color.White;
                tmpBox.Name = i.ToString();
                tmpBox.Tag = s;
                tmpBox.Click += new EventHandler(SmallSchemaBox_Click);

                s.SchemaBox = tmpBox;
                s.Zoom = 0.25;
                s.Draw();

                panel.Controls.Add(tmpBox);
            }
        }

        /// <summary>
        /// schemaPanel 中のシェーマがクリックされた時の動作。
        /// クリックされたシェーマを SchemaBox に描画する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SmallSchemaBox_Click(object sender, EventArgs e)
        {
            PictureBox tmpBox = (PictureBox)(sender);

            ComeReportSchema tmpSchema = (ComeReportSchema)(tmpBox.Tag);

            this.GSchema = tmpSchema.Clone();
            this.GSchema.SchemaBox = this.GSchemaBox;
            this.GSchema.Zoom = 1.0;

            this.GSchema.Draw();
            this.GSchema_AddLabelEvent();

            this.GReportIdBox.Text = this.ReportTabControl.SelectedTab.Controls["ReportIdBox"].Text;
            this.GSchemaIdBox.Text = tmpBox.Name;

            // シェーマ削除ボタンを true にする。
            this.DeleteSchemaButton.Enabled = true;
        }

        /// <summary>
        /// シェーマを登録する。
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="reportId"></param>
        /// <param name="schemaId"></param>
        /// <param name="schema"></param>
        public void RegSchema(string orderId, string reportId, string schemaId, ComeReportSchema schema)
        {
            string tmpReportId = reportId;

            if (reportId.Length == 0)
            {
                tmpReportId = this.ReportTabControl.SelectedTab.Controls["ReportIdBox"].Text;
            }

            ComeReportData tmpReport = new ComeReportData();

            foreach (ComeReportData r in ReportList)
            {
                if (r.OrderId.Equals(orderId) && r.ReportId.Equals(tmpReportId))
                {
                    tmpReport = r;
                    break;
                }
            }

            if (tmpReport.OrderId.Length > 5)
            {
                if (schemaId.Length > 0 && int.Parse(schemaId) >= 0 && int.Parse(schemaId) < tmpReport.SchemaList.Count)
                {
                    tmpReport.SchemaList[int.Parse(schemaId)] = schema;
                }
                else
                {
                    tmpReport.SchemaList.Add(schema);
                }

                foreach (TabPage tp in this.ReportTabControl.TabPages)
                {
                    if (tp.Controls["ReportIdBox"].Text.Equals(tmpReportId))
                    {
                        this.RedrawSchemas((FlowLayoutPanel)(tp.Controls["SchemaPanel"]), tmpReport.SchemaList);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// シェーマを削除する。
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="reportId"></param>
        /// <param name="schemaId"></param>
        public void DeleteSchema(string orderId, string reportId, string schemaId)
        {
            ComeReportData tmpReport = new ComeReportData();

            foreach (ComeReportData r in ReportList)
            {
                if (r.OrderId.Equals(orderId) && r.ReportId.Equals(reportId))
                {
                    tmpReport = r;
                    break;
                }
            }

            if (tmpReport.OrderId.Length > 5)
            {
                if (int.Parse(schemaId) >= 0 && int.Parse(schemaId) < tmpReport.SchemaList.Count)
                {
                    tmpReport.SchemaList.RemoveAt(int.Parse(schemaId));
                }

                TabPage tmpPage = new TabPage();

                foreach (TabPage tp in this.ReportTabControl.TabPages)
                {
                    if (tp.Controls["ReportIdBox"].Text.Equals(reportId))
                    {
                        tmpPage = tp;
                        break;
                    }
                }

                if (tmpPage != null)
                {
                    this.RedrawSchemas((FlowLayoutPanel)(tmpPage.Controls["SchemaPanel"]), tmpReport.SchemaList);
                }
            }
        }

        private void TemplateButton_Click(object sender, EventArgs e)
        {
            if (this.Fr == null || !this.Fr.Created)
            {
                Fr = new FormComeReportTemplate(this);
            }

            Fr.Show();
            Fr.Activate();

            if (Fr.WindowState == FormWindowState.Minimized)
            {
                Fr.WindowState = FormWindowState.Normal;
            }
        }

        private void RegButton_Click(object sender, EventArgs e)
        {
            ComeReportData tmpReport = new ComeReportData();

            tmpReport.OrderId = this.OrderIdBox.Text;
            tmpReport.ReportId = this.ReportTabControl.SelectedTab.Controls["ReportIdBox"].Text;
            tmpReport.Cont = this.ReportTabControl.SelectedTab.Controls["ContBox"].Text;
            tmpReport.Tab = this.ReportTabControl.SelectedTab.Text;

            Label tmpLabel = (Label)(this.ReportTabControl.SelectedTab.Controls["OutSideLabel"]);

            if (tmpLabel.Text.Equals("院外"))
            {
                tmpReport.OutSide = "1";
            }
            else
            {
                tmpReport.OutSide = "0";
            }

            CheckBox tmpBox = (CheckBox)(this.ReportTabControl.SelectedTab.Controls["StatusBox"]);

            if (tmpBox.Checked)
            {
                tmpReport.Status = "1";
            }
            else
            {
                tmpReport.Status = "2";
            }

            foreach (ComeReportData r in ReportList)
            {
                if (r.OrderId.Equals(tmpReport.OrderId) && r.ReportId.Equals(tmpReport.ReportId))
                {
                    tmpReport.SchemaList = r.SchemaList;
                    break;
                }
            }

            this.RegReport(tmpReport);

            Label tmpBox2 = (Label)(this.ReportTabControl.SelectedTab.Controls["StaffNameBox"]);
            tmpBox2.Text = LoginUser.Name;

            tmpBox2 = (Label)(this.ReportTabControl.SelectedTab.Controls["SaveDateBox"]);
            tmpBox2.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");

            MessageBox.Show("登録しました");
        }

        /// <summary>
        /// オーダーに対して新たに所見を作成する。
        /// </summary>
        /// <param name="kensaCode"></param>
        /// <param name="orderId"></param>
        public void NewReport(string kensaCode, string orderId)
        {
            this.ClearReport();

            int reportId = ComeReportData.GetMaxByOrderId(orderId);
            reportId++;

            string record = "";
            string status = "";

            foreach (string tab in ComeReportSettings.Current.KensaDict[kensaCode].TabList)
            {
                if (tab.Equals("[部位]"))
                {
                    for (int i = 0; i < PartList.Count; i++)
                    {
                        ComeReportData tmpReport = new ComeReportData();
                        tmpReport.OrderId = orderId;
                        tmpReport.ReportId = reportId.ToString();
                        tmpReport.Tab = PartList[i];

                        ComeReportData.TabInsert(tmpReport.OrderId, tmpReport.ReportId, tmpReport.Tab);

                        this.ReportList.Add(tmpReport);
                        this.MakeTab(tmpReport);

                        reportId++;

                        record = "○";

                        if (status.Length > 0)
                        {
                            status += " △" + PartList[i];
                        }
                        else
                        {
                            status = "△" + PartList[i];
                        }
                    }
                }
                else
                {
                    ComeReportData tmpReport = new ComeReportData();
                    tmpReport.OrderId = orderId;
                    tmpReport.ReportId = reportId.ToString();
                    tmpReport.Tab = tab;

                    ComeReportData.TabInsert(tmpReport.OrderId, tmpReport.ReportId, tmpReport.Tab);

                    this.ReportList.Add(tmpReport);
                    this.MakeTab(tmpReport);

                    reportId++;

                    record = "○";

                    if (status.Length > 0)
                    {
                        status += " △" + tab;
                    }
                    else
                    {
                        status = "△" + tab;
                    }
                }
            }

            if (this.ReportTabControl.TabCount == 0)
            {
                if (MessageBox.Show("タブが作成されていません。手動で作成しますか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                {
                    FormComeReportTab fat = new FormComeReportTab(orderId);
                    fat.ShowDialog();
                }
            }

            this.SortTab();

            foreach (DataGridViewRow r in this.HistoryView.Rows)
            {
                if (r.Cells["オーダー番号"].Value.ToString().Equals(orderId))
                {
                    r.Selected = true;
                    r.Cells["記録"].Value = record;
                    r.Cells["完成"].Value = status;
                    break;
                }
            }

            //            this.MakeOrderHistory();
        }

        /// <summary>
        /// オーダーに対して既に作成されている所見を表示する。
        /// </summary>
        /// <param name="orderId"></param>
        public void ShowReport(string order_id)
        {
            List<string> order_list = new List<string>();
            order_list.Add(order_id);

            this.ReportList = ComeReportData.GetListByOrderIds(order_list);

            this.ClearReport();

            foreach (ComeReportData report in ReportList)
            {
                if (!report.Status.Equals("0"))
                {
                    this.MakeTab(report);
                }
            }

            this.SortTab();
        }

        /// <summary>
        /// オーダーに対して所見を追加する。
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="tabName"></param>
        public void AddReport(string orderId, string tabName)
        {
            int reportId = ComeReportData.GetMaxByOrderId(orderId);
            reportId++;

            ComeReportData tmpReport = new ComeReportData();
            tmpReport.OrderId = orderId;
            tmpReport.ReportId = reportId.ToString();
            tmpReport.Tab = tabName;

            ComeReportData.TabInsert(tmpReport.OrderId, tmpReport.ReportId, tmpReport.Tab);

            this.ReportList.Add(tmpReport);
            this.MakeTab(tmpReport);

            this.SortTab();
            this.MakeOrderHistory(orderId);
        }

        /// <summary>
        /// 所見名を変更する。
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="reportId"></param>
        /// <param name="tabName"></param>
        public void RenameReport(string orderId, string reportId, string tabName)
        {
            if (tabName.Length == 0)
            {
                return;
            }

            ComeReportData.TabRename(orderId, reportId, tabName);

            foreach (ComeReportData r in this.ReportList)
            {
                if (r.OrderId.Equals(orderId) && r.ReportId.Equals(reportId))
                {
                    r.Tab = tabName;
                    break;
                }
            }

            foreach (TabPage tp in this.ReportTabControl.TabPages)
            {
                if (tp.Controls["ReportIdBox"].Text.Equals(reportId))
                {
                    tp.Name = tabName;
                    tp.Text = tabName;
                    break;
                }
            }

            this.SortTab();
            this.MakeOrderHistory(orderId);
        }

        /// <summary>
        /// オーダーに対して既に作成されている所見を削除する。
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="reportId"></param>
        public void DeleteReport(string orderId, string reportId)
        {
            ComeReportData.TabDelete(orderId, reportId);

            foreach (ComeReportData r in this.ReportList)
            {
                if (r.OrderId.Equals(orderId) && r.ReportId.Equals(reportId))
                {
                    this.ReportList.Remove(r);
                    break;
                }
            }

            this.ReportTabControl.TabPages.Remove(this.ReportTabControl.SelectedTab);

            this.SortTab();
            this.MakeOrderHistory(orderId);
        }

        /// <summary>
        /// 所見を登録する。
        /// </summary>
        /// <param name="report"></param>
        public void RegReport(ComeReportData report)
        {
            ComeReportData.TabSave(report);

            this.MakeOrderHistory(report.OrderId);
        }

        /// <summary>
        /// タブの選択
        /// </summary>
        private void SortTab()
        {
            bool loop = true;

            for (int i = 0; i < this.ReportTabControl.TabPages.Count && loop; i++)
            {
                foreach (string s in ComeReportSettings.Current.MyTabList)
                {
                    if (this.ReportTabControl.TabPages[i].Text.Contains(s))
                    {
                        this.ReportTabControl.SelectedIndex = i;
                        loop = false;
                        break;
                    }
                }
            }
        }

        void GSchemaBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] s = (string[])(e.Data.GetData(DataFormats.FileDrop));
                string srcFileName = s[0];

                if (srcFileName.EndsWith(".jpg", StringComparison.CurrentCultureIgnoreCase) || srcFileName.EndsWith(".jpeg", StringComparison.CurrentCultureIgnoreCase))
                {
                    e.Effect = DragDropEffects.All;
                }
            }
        }

        void GSchemaBox_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] s = (string[])(e.Data.GetData(DataFormats.FileDrop));
                string srcFileName = s[0];
                string tarFileName = System.Environment.GetEnvironmentVariable("TEMP") + "\\" + this.Pat.Id.PadLeft(9, '0') + LoginUser.Id.PadLeft(5, '0') + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                System.IO.File.Copy(srcFileName, tarFileName, true);

                this.SetSchemaBg("", tarFileName);
            }
        }

        private void BgPasteMenuItem_Click(object sender, EventArgs e)
        {
            Image tmpImg = (Image)(Clipboard.GetData(DataFormats.Bitmap));

            if (tmpImg != null)
            {
                string tarFileName = System.Environment.GetEnvironmentVariable("TEMP") + "\\" + this.Pat.Id.PadLeft(9, '0') + LoginUser.Id.PadLeft(5, '0') + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                tmpImg.Save(tarFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                this.SetSchemaBg("", tarFileName);
            }
        }

        private void GSchema_Reset()
        {
            this.GMode = 0;
            this.DrawButton0.Checked = true;
            this.GStart = new Point();
            this.GEnd = new Point();
            this.GPointList = new List<Point>();

            this.GMarginX = 0;
            this.GMarginY = 0;

            this.TmpItem = null;
            this.TmpLabel = null;
        }

        public void GSchema_AddLabelEvent()
        {
            foreach (Control c in GSchemaBox.Controls)
            {
                if (c.GetType().Name.Equals("Label"))
                {
                    c.ContextMenuStrip = LabelMenuStrip;
                    c.MouseDown += new MouseEventHandler(Label_MouseDown);
                    c.MouseMove += new MouseEventHandler(Label_MouseMove);
                    c.MouseUp += new MouseEventHandler(Label_MouseUp);
                }
            }
        }

        private void Label_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && GMode.Equals(14))
            {
                GStart = new Point(e.X, e.Y);
            }
            else if (e.Button == MouseButtons.Right)
            {
                LabelMenuStrip.Enabled = true;
            }
        }

        private void Label_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && GMode.Equals(14))
            {
                GEnd = new Point(e.X, e.Y);
            }
        }

        private void Label_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && GMode.Equals(14) && TmpLabel != null)
            {
                ComeReportSchemaItem tmpItem = new ComeReportSchemaItem("文字", TmpLabel.ForeColor, TmpLabel.Location.X, TmpLabel.Location.Y, TmpLabel.Text);

                int index = GSchema.FindSchemaItemAt(tmpItem);

                int movex = GEnd.X - GStart.X;
                int movey = GEnd.Y - GStart.Y;

                TmpLabel.Location = new Point(TmpLabel.Location.X + movex, TmpLabel.Location.Y + movey);
                tmpItem.SetStart(TmpLabel.Location);
                GSchema.SetSchemaItemAt(index, tmpItem);

                this.GSchema_Reset();
            }
        }

        private void FileExitMenu_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void OpenSchemaBgButton_Click(object sender, EventArgs e)
        {
            if (Fs == null)
            {
                Fs = new FormComeReportSchema(this);
            }

            Fs.Show();
            Fs.Activate();

            if (Fs.WindowState == FormWindowState.Minimized)
            {
                Fs.WindowState = FormWindowState.Normal;
            }
        }
        
        public void SetSchemaBg(string bgId, string bgFile)
        {
            if (ComeReportSettings.Current.SchemaBgDict.ContainsKey(bgId) && System.IO.File.Exists(ComeReportSettings.Current.SchemaBgDict[bgId].Path))
            {
                GSchema.BgId = bgId;
                GSchema.Draw();
            }
            else
            {
                GSchema.BgImagePath = bgFile;
                GSchema.Draw();
            }
        }
        
        private void DrawButton0_CheckedChanged(object sender, EventArgs e)
        {
            if (DrawButton0.Checked)
            {
                GMode = 0;
            }
        }

        private void DrawButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (DrawButton1.Checked)
            {
                GMode = 1;
            }
        }

        private void DrawButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (DrawButton2.Checked)
            {
                GMode = 2;
            }
        }

        private void DrawButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (DrawButton3.Checked)
            {
                GMode = 3;
            }
        }

        private void DrawButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (DrawButton4.Checked)
            {
                GMode = 4;
            }
        }

        private void DrawButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (DrawButton5.Checked)
            {
                GMode = 5;
            }
        }

        private void ClearSchema()
        {
            this.GReportIdBox.Text = "";
            this.GSchemaIdBox.Text = "";
            this.GSchema = new ComeReportSchema(GSchemaBox, 1.0);
            this.GSchema.Draw();

            GSchemaBox.Controls.Clear();

            // シェーマ削除ボタンを false にする。
            this.DeleteSchemaButton.Enabled = false;
        }

        private void RegSchemaButton_Click(object sender, EventArgs e)
        {
            if (this.GReportIdBox.Text.Length > 0)
            {
                foreach (TabPage tp in this.ReportTabControl.TabPages)
                {
                    if (tp.Controls["ReportIdBox"].Text.Equals(this.GReportIdBox.Text))
                    {
                        CheckBox tmpBox = (CheckBox)(tp.Controls["StatusBox"]);

                        if (tmpBox.Checked)
                        {
                            MessageBox.Show("完成済みの所見にはシェーマを登録できません。\r\n完成チェックボックスを外してから登録してください。");
                            break;
                        }
                        else
                        {
                            this.RegSchema(this.OrderIdBox.Text, this.GReportIdBox.Text, this.GSchemaIdBox.Text, this.GSchema);
                            this.ClearSchema();
                            break;
                        }
                    }
                }
            }
            else if (this.ReportTabControl.TabPages.Count > 0)
            {
                // 新規シェーマであった場合
                this.RegSchema(this.OrderIdBox.Text, this.ReportTabControl.SelectedTab.Controls["ReportIdBox"].Text, "", this.GSchema);
                this.ClearSchema();
            }
        }

        private void DeleteSchemaButton_Click(object sender, EventArgs e)
        {
            foreach (TabPage tp in this.ReportTabControl.TabPages)
            {
                if (tp.Controls["ReportIdBox"].Text.Equals(this.GReportIdBox.Text))
                {
                    CheckBox tmpBox = (CheckBox)(tp.Controls["StatusBox"]);

                    if (tmpBox.Checked)
                    {
                        MessageBox.Show("完成済みの所見からはシェーマを削除できません。\r\n完成チェックボックスを外してから削除してください。");
                    }
                    else
                    {
                        if (MessageBox.Show("このシェーマをリストから削除しますか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            this.DeleteSchema(this.OrderIdBox.Text, this.GReportIdBox.Text, this.GSchemaIdBox.Text);
                            this.ClearSchema();
                        }
                    }
                }
            }
        }

        private void ClearSchemaButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("シェーマ画像をクリアしますか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.ClearSchema();
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox tmpBox = (TextBox)(sender);

            if (tmpBox.Text != null && tmpBox.Text.Length > 0)
            {
                ComeReportSchemaItem tmpItem = new ComeReportSchemaItem("文字", GColor, tmpBox.Location.X, tmpBox.Location.Y, tmpBox.Text);

                int itemNum = this.GSchema.FindSchemaItemAt(tmpItem);

                if (itemNum >= 0)
                {
                    this.GSchema.SetSchemaItemAt(itemNum, tmpItem);
                }
                else
                {
                    this.GSchema.AddSchemaItem(tmpItem);
                }

                tmpBox.Dispose();

                this.GSchema.Draw();
                this.GSchema_AddLabelEvent();
            }
        }

        private void LabelMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            ContextMenuStrip tmpStrip = (ContextMenuStrip)(sender);
            TmpLabel = (Label)(tmpStrip.SourceControl);
        }

        private void LabelMoveMenuItem_Click(object sender, EventArgs e)
        {
            if (TmpLabel != null)
            {
                this.GMode = 14;
            }
        }

        private void LabelModifyMenuItem_Click(object sender, EventArgs e)
        {
            if (TmpLabel != null)
            {
                TextBox tmpBox = new TextBox();
                tmpBox.Location = TmpLabel.Location;
                tmpBox.Text = TmpLabel.Text;
                tmpBox.Size = new Size(TmpLabel.Size.Width + 20, TmpLabel.Size.Height + 20);
                tmpBox.Multiline = true;
                tmpBox.ImeMode = ImeMode.Hiragana;
                tmpBox.Leave += new EventHandler(TextBox_Leave);

                ComeReportSchemaItem tmpItem = new ComeReportSchemaItem("文字", TmpLabel.ForeColor, TmpLabel.Location.X, TmpLabel.Location.Y, TmpLabel.Text);
                GSchema.RemoveSchemaItem(tmpItem);
                TmpLabel.Dispose();

                GSchemaBox.Controls.Add(tmpBox);
                tmpBox.Focus();
            }
        }

        private void LabelDeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (TmpLabel != null)
            {
                if (MessageBox.Show("削除しますか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                {
                    ComeReportSchemaItem tmpItem = new ComeReportSchemaItem("文字", TmpLabel.ForeColor, TmpLabel.Location.X, TmpLabel.Location.Y, TmpLabel.Text);
                    GSchema.RemoveSchemaItem(tmpItem);
                    TmpLabel.Dispose();
                }
            }

            this.GSchema_Reset();
        }

        private void ItemDeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (TmpItem != null)
            {
                if (MessageBox.Show("削除しますか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                {
                    GSchema.RemoveSchemaItem(TmpItem);
                    GSchema.Draw();
                    GSchema_AddLabelEvent();
                }
            }

            this.GSchema_Reset();
        }

        private void GSchemaBox_Click(object sender, EventArgs e)
        {
            GSchemaBox.Focus();
        }

        private void GSchemaBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // 描画モードの時
                if (this.GMode >= 1 && this.GMode <= 5)
                {
                    this.GStart = new Point(e.X, e.Y);

                    if (this.GMode == 5)
                    {
                        this.GPointList.Add(this.GStart);
                    }
                }
                // 選択モードの時
                else if (this.GMode == 0)
                {
                    TmpItem = GSchema.PointSchemaItem(new Point(e.X, e.Y));

                    // 同一職種スタッフが書いたものであれば選択変更可能。それ以外なら不可。
                    if (TmpItem != null && TmpItem.GetColor().ToArgb() == GPen.Color.ToArgb())
                    {
                        TmpItem.Draw(GSchemaBox, SPen, 1.0);

                        if (TmpItem.GetKind().Equals("直線"))
                        {
                            if (TmpItem.GetPointMode().Equals(1))
                            {
                                GStart = TmpItem.GetStart();
                                GEnd = TmpItem.GetEnd();
                                GMode = 21;
                            }
                            else if (TmpItem.GetPointMode().Equals(2))
                            {
                                GStart = TmpItem.GetStart();
                                GEnd = TmpItem.GetEnd();
                                GMode = 11;
                            }
                        }
                        else if (TmpItem.GetKind().Equals("楕円"))
                        {
                            if (TmpItem.GetPointMode().Equals(1))
                            {
                                GStart = TmpItem.GetStart();
                                GEnd = TmpItem.GetEnd();
                                GMarginX = TmpItem.GetStart().X - e.X;
                                GMarginY = TmpItem.GetStart().Y - e.Y;
                                GMode = 22;
                            }
                            else if (TmpItem.GetPointMode().Equals(2))
                            {
                                GStart = TmpItem.GetStart();
                                GEnd = TmpItem.GetEnd();
                                GMarginX = TmpItem.GetEnd().X - e.X;
                                GMarginY = TmpItem.GetEnd().Y - e.Y;
                                GMode = 12;
                            }
                        }
                        else if (TmpItem.GetKind().Equals("四角"))
                        {
                            if (TmpItem.GetPointMode().Equals(1))
                            {
                                GStart = TmpItem.GetStart();
                                GEnd = TmpItem.GetEnd();
                                GMarginX = TmpItem.GetStart().X - e.X;
                                GMarginY = TmpItem.GetStart().Y - e.Y;
                                GMode = 23;
                            }
                            else if (TmpItem.GetPointMode().Equals(2))
                            {
                                GStart = TmpItem.GetStart();
                                GEnd = TmpItem.GetEnd();
                                GMarginX = TmpItem.GetEnd().X - e.X;
                                GMarginY = TmpItem.GetEnd().Y - e.Y;
                                GMode = 13;
                            }
                        }
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                // 選択モードの時
                if (this.GMode == 0)
                {
                    this.GSchema.Draw();
                    this.GSchema_AddLabelEvent();

                    this.ItemMenuStrip.Items[2].Enabled = false;

                    TmpItem = GSchema.PointSchemaItem(new Point(e.X, e.Y));

                    if (TmpItem != null)
                    {
                        TmpItem.Draw(GSchemaBox, SPen, 1.0);
                        this.ItemMenuStrip.Items[2].Enabled = true;
                    }
                }

                // クリップボードに画像が貼り付けられていれば「背景貼り付け」メニューが有効になる
                if ((Image)(Clipboard.GetData(DataFormats.Bitmap)) != null)
                {
                    this.ItemMenuStrip.Items[0].Enabled = true;
                }
                else
                {
                    this.ItemMenuStrip.Items[0].Enabled = false;
                }
            }
        }

        private void GSchemaBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.GMode >= 1)
                {
                    if (GMode >= 1 && GMode <= 5)
                    {
                        this.GEnd = new Point(e.X, e.Y);

                        if (this.GMode == 5)
                        {
                            this.GPointList.Add(this.GEnd);

                            if (GPointList.Count >= 2)
                            {
                                Graphics g = Graphics.FromImage(GSchemaBox.Image);
                                g.DrawLine(GPen, GPointList[GPointList.Count - 2], GPointList[GPointList.Count - 1]);
                            }
                        }
                    }
                    else if (GMode == 11 || GMode == 12 || GMode == 13)
                    {
                        this.GEnd = new Point(e.X + GMarginX, e.Y + GMarginY);
                    }
                    else if (GMode == 21 || GMode == 22 || GMode == 23)
                    {
                        this.GStart = new Point(e.X + GMarginX, e.Y + GMarginY);
                    }

                    this.GSchemaBox.Refresh();
                }
            }
        }

        private void GSchemaBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.GMode == 1)
                {
                    ComeReportSchemaItem tmpItem = new ComeReportSchemaItem("直線", GColor, GStart.X, GStart.Y, GEnd.X, GEnd.Y);
                    this.GSchema.AddSchemaItem(tmpItem);
                    this.GSchema.Draw();
                    this.GSchema_AddLabelEvent();
                }
                else if (this.GMode == 11 || this.GMode == 21)
                {
                    if (TmpItem != null)
                    {
                        GSchema.RemoveSchemaItem(TmpItem);
                    }

                    ComeReportSchemaItem tmpItem = new ComeReportSchemaItem("直線", GColor, GStart.X, GStart.Y, GEnd.X, GEnd.Y);
                    this.GSchema.AddSchemaItem(tmpItem);
                    this.GSchema.Draw();
                    this.GSchema_AddLabelEvent();
                }
                else if (this.GMode == 2)
                {
                    ComeReportSchemaItem tmpItem = new ComeReportSchemaItem("楕円", GColor, GStart.X, GStart.Y, GEnd.X, GEnd.Y);
                    this.GSchema.AddSchemaItem(tmpItem);
                    this.GSchema.Draw();
                    this.GSchema_AddLabelEvent();
                }
                else if (this.GMode == 12 || this.GMode == 22)
                {
                    if (TmpItem != null)
                    {
                        GSchema.RemoveSchemaItem(TmpItem);
                    }

                    ComeReportSchemaItem tmpItem = new ComeReportSchemaItem("楕円", GColor, GStart.X, GStart.Y, GEnd.X, GEnd.Y);
                    this.GSchema.AddSchemaItem(tmpItem);
                    this.GSchema.Draw();
                    this.GSchema_AddLabelEvent();
                }
                else if (this.GMode == 3)
                {
                    if (GStart.X < GEnd.X && GStart.Y < GEnd.Y)
                    {
                        ComeReportSchemaItem tmpItem = new ComeReportSchemaItem("四角", GColor, GStart.X, GStart.Y, GEnd.X, GEnd.Y);
                        this.GSchema.AddSchemaItem(tmpItem);
                        this.GSchema.Draw();
                        this.GSchema_AddLabelEvent();
                    }
                }
                else if (this.GMode == 13 || this.GMode == 23)
                {
                    if (GStart.X < GEnd.X && GStart.Y < GEnd.Y)
                    {
                        if (TmpItem != null)
                        {
                            GSchema.RemoveSchemaItem(TmpItem);
                        }

                        ComeReportSchemaItem tmpItem = new ComeReportSchemaItem("四角", GColor, GStart.X, GStart.Y, GEnd.X, GEnd.Y);
                        this.GSchema.AddSchemaItem(tmpItem);
                        this.GSchema.Draw();
                        this.GSchema_AddLabelEvent();
                    }
                }
                else if (this.GMode == 4)
                {
                    TextBox tmpBox = new TextBox();
                    tmpBox.Location = GStart;
                    tmpBox.Size = new Size(GEnd.X - GStart.X, GEnd.Y - GStart.Y);
                    tmpBox.Multiline = true;
                    tmpBox.ImeMode = ImeMode.Hiragana;
                    tmpBox.ForeColor = GColor;
                    tmpBox.Leave += new EventHandler(TextBox_Leave);

                    GSchemaBox.Controls.Add(tmpBox);
                    tmpBox.Focus();
                }
                else if (this.GMode == 5)
                {
                    ComeReportSchemaItem tmpItem = new ComeReportSchemaItem("フリー", GColor, GPointList);
                    this.GSchema.AddSchemaItem(tmpItem);
                    this.GSchema.Draw();
                    this.GSchema_AddLabelEvent();
                }

                this.GSchema_Reset();
            }
        }

        private void GSchemaBox_Paint(object sender, PaintEventArgs e)
        {
            if (this.GMode == 1 || this.GMode == 11 || this.GMode == 21)
            {
                e.Graphics.DrawLine(GPen, GStart, GEnd);
            }
            else if (this.GMode == 2 || this.GMode == 12 || this.GMode == 22)
            {
                e.Graphics.DrawEllipse(GPen, GStart.X, GStart.Y, GEnd.X - GStart.X, GEnd.Y - GStart.Y);
            }
            else if (this.GMode == 3 || this.GMode == 13 || this.GMode == 23)
            {
                e.Graphics.DrawRectangle(GPen, GStart.X, GStart.Y, GEnd.X - GStart.X, GEnd.Y - GStart.Y);
            }
            else if (this.GMode == 4)
            {
                e.Graphics.DrawRectangle(GPen, GStart.X, GStart.Y, GEnd.X - GStart.X, GEnd.Y - GStart.Y);
            }
        }

        private void ShowPtData()
        {
            this.ClearReport();

            int pt_id = 0;

            if (this.Pat.Id.Length == 0 || !int.TryParse(this.Pat.Id, out pt_id))
            {
                return;
            }

            this.Text = this.Pat.Name;
            this.MakeOrderHistory();

            if (this.OrderId.Length > 0)
            {
                foreach (DataGridViewRow r in this.HistoryView.Rows)
                {
                    if (r.Cells["オーダー番号"].Value.ToString().Equals(this.OrderId))
                    {
                        r.Selected = true;
                        this.ShowReportOnSelectedRow("参照");
                        break;
                    }
                }
            }

            // ログ
            LibUtility.Log("検査所見システム参照", this.Pat.Id);
        }

        private void HistoryMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (HistoryView.SelectedRows.Count > 0)
            {
                DataGridViewRow tmpRow = HistoryView.SelectedRows[0];

                if (tmpRow.Cells["記録"].Value.ToString().Length > 0)
                {
                    this.HistoryMenuStrip.Items[0].Enabled = false;
                    this.HistoryMenuStrip.Items[1].Enabled = true;

                    if (ComeReportSettings.Current.OutSideList.Contains(tmpRow.Cells["施行部署１"].Value.ToString()))
                    {
                        this.HistoryMenuStrip.Items[3].Enabled = true;
                    }
                    else
                    {
                        this.HistoryMenuStrip.Items[3].Enabled = false;
                    }
                }
                else
                {
                    this.HistoryMenuStrip.Items[0].Enabled = true;
                    this.HistoryMenuStrip.Items[1].Enabled = false;
                    this.HistoryMenuStrip.Items[3].Enabled = false;
                }
            }
        }

        private void HistoryNewMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowReportOnSelectedRow("新規");
            this.RegSchemaButton.Enabled = true;
        }

        private void HistoryShowMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowReportOnSelectedRow("参照");
            this.RegSchemaButton.Enabled = true;
        }

        /// <summary>
        /// 所見作成フォームをクリアする。
        /// </summary>
        public void ClearReport()
        {
            this.OrderIdBox.Text = "";
            this.OrderBox.Text = "";

            this.ReportTabControl.TabPages.Clear();

            this.ClearSchema();
        }

        /// <summary>
        /// 選択行の所見を開く。
        /// </summary>
        /// <param name="mode">1 新規, 2 参照</param>
        private void ShowReportOnSelectedRow(string mode)
        {
            if (mode.Equals("新規"))
            {
                if (HistoryView.SelectedRows.Count > 0)
                {
                    DataGridViewRow tmpRow = HistoryView.SelectedRows[0];

                    this.ClearReport();

                    string tmpOrderId = tmpRow.Cells["オーダー番号"].Value.ToString();
                    string tmpSekouCode = tmpRow.Cells["施行部署１"].Value.ToString();

                    this.MakeOrderDetail(tmpSekouCode, tmpOrderId);
                    string tmpOrderDetail = this.OrderBox.Text;

                    this.NewReport(tmpSekouCode, tmpOrderId);

                    this.OrderIdBox.Text = tmpOrderId;
                    this.OrderBox.Text = tmpOrderDetail;
                }
            }
            else if (mode.Equals("参照"))
            {
                if (HistoryView.SelectedRows.Count > 0)
                {
                    DataGridViewRow tmpRow = HistoryView.SelectedRows[0];

                    this.ClearReport();

                    string tmpOrderId = tmpRow.Cells["オーダー番号"].Value.ToString();
                    string tmpSekouCode = tmpRow.Cells["施行部署１"].Value.ToString();

                    // 記録カラムが空でなければ、該当の所見を表示する
                    if (tmpRow.Cells["記録"].Value.ToString().Length > 0)
                    {
                        this.ShowReport(tmpOrderId);
                        this.RegSchemaButton.Enabled = true;
                    }

                    this.OrderIdBox.Text = tmpOrderId;
                    this.MakeOrderDetail(tmpSekouCode, tmpOrderId);
                }
            }
        }

        private void DicomButton_Click(object sender, EventArgs e)
        {
            Launcher.Dicom(this.Pat.Id, Launcher.DicomKind.EV);
        }

        private void KarteButton_Click(object sender, EventArgs e)
        {
            if (!InnoProgram.KarteShow(this.Pat.Id))
            {
                MessageBox.Show("カルテが起動していません");
            }
        }

        private void TabPageMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            TabPage tp = ((TabControl)TabPageMenuStrip.SourceControl).SelectedTab;

            if (tp.Controls.ContainsKey("RegButton"))
            {
                Button b = (Button)tp.Controls["RegButton"];

                if (b.Enabled)
                {
                    // 登録ボタンが押せる状態
                    this.TabNewMenuItem.Enabled = true;
                    this.TabRenameMenuItem.Enabled = true;
                    this.TabDeleteMenuItem.Enabled = true;
                }
                else
                {
                    // 登録ボタンが押せない状態
                    this.TabNewMenuItem.Enabled = false;
                    this.TabRenameMenuItem.Enabled = false;
                    this.TabDeleteMenuItem.Enabled = false;
                }
            }
        }

        private void TabNewMenuItem_Click(object sender, EventArgs e)
        {
            FormComeReportTab fat = new FormComeReportTab(this.OrderIdBox.Text);
            fat.ShowDialog(this);
        }

        private void TabRenameMenuItem_Click(object sender, EventArgs e)
        {
            FormComeReportTab fat = new FormComeReportTab(this.OrderIdBox.Text, this.ReportTabControl.SelectedTab.Controls["ReportIdBox"].Text, this.ReportTabControl.SelectedTab.Text);
            fat.ShowDialog(this);
        }

        private void TabDeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("削除しますか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.DeleteReport(this.OrderIdBox.Text, ReportTabControl.SelectedTab.Controls["ReportIdBox"].Text);
            }
        }

        private void OpenSchemaFileButton_Click(object sender, EventArgs e)
        {
            if (SchemaFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 背景画像ファイルをTEMPにコピーする。ファイル名は28文字（患者ID 9桁 + ユーザーID 5桁 + 年月日時刻 14桁）とする。
                string fileName = System.Environment.GetEnvironmentVariable("TEMP") + "\\" + this.Pat.Id.PadLeft(9, '0') + LoginUser.Id.PadLeft(5, '0') + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                System.IO.File.Copy(SchemaFileDialog.FileName, fileName, true);

                this.SetSchemaBg("", fileName);
            }
        }

        private void ColorBox1_Click(object sender, EventArgs e)
        {
            ColorBox1.BorderStyle = BorderStyle.Fixed3D;
            ColorBox2.BorderStyle = BorderStyle.None;
            ColorBox3.BorderStyle = BorderStyle.None;

            GColor = ColorBox1.BackColor;
            GPen = new Pen(GColor, 2);
        }

        private void ColorBox2_Click(object sender, EventArgs e)
        {
            ColorBox1.BorderStyle = BorderStyle.None;
            ColorBox2.BorderStyle = BorderStyle.Fixed3D;
            ColorBox3.BorderStyle = BorderStyle.None;

            GColor = ColorBox2.BackColor;
            GPen = new Pen(GColor, 2);
        }

        private void ColorBox3_Click(object sender, EventArgs e)
        {
            ColorBox1.BorderStyle = BorderStyle.None;
            ColorBox2.BorderStyle = BorderStyle.None;
            ColorBox3.BorderStyle = BorderStyle.Fixed3D;

            GColor = ColorBox3.BackColor;
            GPen = new Pen(GColor, 2);
        }

        private void MessageButton_Click(object sender, EventArgs e)
        {
            List<string> send_to_list = new List<string>();

            if (HistoryView.SelectedRows.Count > 0)
            {
                string doctor_code = HistoryView.SelectedRows[0].Cells["指示医コード"].Value.ToString();

                if (Dict.DoctorDict.ContainsKey(doctor_code))
                {
                    send_to_list.Add(Dict.DoctorDict[doctor_code].StaffCode.ToString());
                }
            }

            foreach (string s in ComeReportSettings.Current.MessageStaffList)
            {
                send_to_list.Add(s);
            }

            FormKarteMessage2 fm = new FormKarteMessage2(this.Pat.Id
                , send_to_list
                , ComeReportSettings.Current.MessageSubject
                , ComeReportSettings.Current.MessageBody);

            fm.ShowDialog();
        }

        private void HistoryOutSideMenuItem_Click(object sender, EventArgs e)
        {
            if (HistoryView.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("院外読影依頼しますか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    string order_id = HistoryView.SelectedRows[0].Cells["オーダー番号"].Value.ToString();

                    ComeReportOrder.ReportOutSide(order_id, LoginUser.Id);
                    this.MakeOrderHistory(order_id);

                    this.ReportTabControl.TabPages.Clear();
                    MessageBox.Show("院外読影依頼にしました");
                }
            }
        }

        class OrderResult
        {
            public string OrderId = "";
            public string OutSide = "";
            public string Record = "";
            public string Status = "";
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("終了しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void FormComeReportPat_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormPatList.Remove(this);
        }
    }
}