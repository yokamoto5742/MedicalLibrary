using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class FormExec : StdForm1
    {
        DataSet dSet = new DataSet();

        /// <summary>
        /// 現在開いているオーダーリスト
        /// </summary>
        List<PatOrder> orderList = new List<PatOrder>();

        /// <summary>
        /// 指定日の入院患者リスト
        /// </summary>
        List<PatIn> patInList = new List<PatIn>();

        /// <summary>
        /// 現在選択されているオーダー
        /// </summary>
        PatOrder patOrder = new PatOrder();

        /// <summary>
        /// 現在選択されている入院患者
        /// </summary>
        PatIn patIn = new PatIn();

        /// <summary>
        /// 現在選択されているオーダーのディティールリスト
        /// （印刷時は、未印刷のリストとして使われる）
        /// </summary>
        List<PatOrderDetail> detailList = new List<PatOrderDetail>();

        /// <summary>
        /// 印刷するオーダーのディティールリスト
        /// </summary>
        List<PatOrderDetail> printList = new List<PatOrderDetail>();

        /// <summary>
        /// 印刷ページ番号
        /// </summary>
        int pageNum = 1;

        /// <summary>
        /// 印刷するディティールの番号
        /// （通常は 1 だが、複数回のオーダーの場合は、その枚数が印刷される）
        /// </summary>
        int detailCounter = 1;


        /// <summary>
        /// バーコード入力された文字列
        /// </summary>
        string inputString = "";


        /// <summary>
        /// 実施するオーダーのリスト
        /// </summary>
        List<PatOrder> orderExecList = new List<PatOrder>();

        /// <summary>
        /// 先行実施で読み込んだバーコードのリスト
        /// </summary>
        List<PreBarcode> preBarcodeList = new List<PreBarcode>();


        Pen p1 = new Pen(Color.Black, 1);
        Pen p2 = new Pen(Color.Black, 2);

        public FormExec()
        {
            InitializeComponent();

            DataTable table = dSet.Tables.Add("Pat");

            table.Columns.Add("病棟コード");
            table.Columns.Add("病棟");
            table.Columns.Add("病室");
            table.Columns.Add("患者コード");
            table.Columns.Add("氏名");
            table.Columns.Add("性別");
            table.Columns.Add("年齢");
            table.Columns.Add("科コード");
            table.Columns.Add("科");

            table = dSet.Tables.Add("Order");

            table.Columns.Add("病棟コード");
            table.Columns.Add("病棟");
            table.Columns.Add("病室");
            table.Columns.Add("施行予定日");
            table.Columns.Add("患者コード");
            table.Columns.Add("氏名");
            table.Columns.Add("性別");
            table.Columns.Add("年齢");
            table.Columns.Add("保険ビット");
            table.Columns.Add("診療区分コード");
            table.Columns.Add("診区");
            table.Columns.Add("連番");
            table.Columns.Add("入外区分コード");
            table.Columns.Add("入外");
            table.Columns.Add("科コード");
            table.Columns.Add("科");
            table.Columns.Add("指示医コード");
            table.Columns.Add("医師");
            table.Columns.Add("オーダー番号");
            table.Columns.Add("オーダー");

            // 基本的にはオーダーの「施行フラグ」なので 0 or 1 だが
            // この画面で実施した場合は「2」となる。
            // 先行実施の場合は「3」となる。
            table.Columns.Add("施行フラグ");

            // 先行実施
            table = dSet.Tables.Add("Exec");

            table.Columns.Add("病棟コード");
            table.Columns.Add("病棟");
            table.Columns.Add("病室");
            table.Columns.Add("施行日");
            table.Columns.Add("患者コード");
            table.Columns.Add("氏名");
            table.Columns.Add("性別");
            table.Columns.Add("年齢");
            table.Columns.Add("診療区分コード");
            table.Columns.Add("診区");
            table.Columns.Add("入外区分コード");
            table.Columns.Add("入外");
            table.Columns.Add("科コード");
            table.Columns.Add("科");
            table.Columns.Add("指示医コード");
            table.Columns.Add("医師");
            table.Columns.Add("オーダー番号");
            table.Columns.Add("オーダーコード");
            table.Columns.Add("オーダー");
            table.Columns.Add("数量");
            table.Columns.Add("施行者コード");
            table.Columns.Add("施行者");
            table.Columns.Add("施行日時");

            this.WardBox1.Items.Add("03 わかば");
            this.WardBox1.Items.Add("04 さくら");
            this.WardBox1.Items.Add("05 あやめ");
            this.WardBox1.Text = "04 さくら";

            this.SekouBox1.Checked = true;
        }

        private void FormExec_Load(object sender, EventArgs e)
        {
            PreBarcode.Init();

            this.DatePicker1.MinDate = DateTime.Now.AddDays(-7);

            this.OrderListShow();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            this.printList = this.BarcodeDraw(g, patOrder, this.printList);

            if (this.printList.Count > 0)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }

            this.pageNum++;
        }

        List<PatOrderDetail> BarcodeDraw(Graphics g, PatOrder order, List<PatOrderDetail> detail_list)
        {
            // まだ印刷されていないディティールのリスト（戻り値）
            List<PatOrderDetail> yet_list = new List<PatOrderDetail>();

            System.Drawing.Font f7 = new System.Drawing.Font("ＭＳ ゴシック", 7);
            System.Drawing.Font f8 = new System.Drawing.Font("ＭＳ ゴシック", 8);
            System.Drawing.Font f10 = new System.Drawing.Font("ＭＳ ゴシック", 10);
            System.Drawing.Font f11 = new System.Drawing.Font("ＭＳ ゴシック", 11);
            System.Drawing.Font f12 = new System.Drawing.Font("ＭＳ ゴシック", 12);
            System.Drawing.Font f14 = new System.Drawing.Font("ＭＳ ゴシック", 14);

            Barcode128 bar128 = new Barcode128();
            bar128.Code = "9" + order.OrderId.PadLeft(14, '0').Substring(1);
            bar128.BarHeight = 20;

            System.Drawing.Image img128 = bar128.CreateDrawingImage(Color.Black, Color.White);

            // 上のバーコードは、最初のページのみ印刷
            if (this.pageNum == 1)
            {
                g.DrawImage(img128, 50, 5);
            }
            else
            {
                g.DrawString("★前ページの続き", f10, Brushes.Black, 0, 5);
                g.DrawString(this.pageNum.ToString() + "枚目", f8, Brushes.Black, 180, 5);
            }

            g.DrawString("実施日：" + DatePicker1.Value.ToString("yyyy/MM/dd(ddd)"), f8, Brushes.Black, 0, 30);
            g.DrawString(order.Pat.Id.PadLeft(9, ' '), f8, Brushes.Black, 160, 30);
            g.DrawString("病室：" + this.patIn.WardName + "（" + this.patIn.Room + "号室）", f8, Brushes.Black, 0, 42);
            g.DrawString(order.Pat.Name + " 様", f14, Brushes.Black, 0, 56);

            // テスト用に患者バーコードも印字
            bar128.Code = order.Pat.Id.PadLeft(9, '0');
            bar128.BarHeight = 20;

            img128 = bar128.CreateDrawingImage(Color.Black, Color.White);
            g.DrawImage(img128, 40, 182, 120, 20);

            g.DrawLine(p1, 0, 78, 220, 78);

//            g.DrawString(order.ShinkuString, f8, Brushes.Black, 0, 84);

            int h = 84;
            bool print = true;

            foreach (PatOrderDetail detail in detail_list)
            {
                // 指定の位置を超えたら次のページへ
                if (h > 156)
//              if (h > 196)
                {
                    print = false;
                }

                if (print)
                {
                    bool qty = true;
                    int h1 = 12;
                    int len1 = 28;
                    System.Drawing.Font ff = f8;

                    // コメントの場合
                    if (detail.Code.StartsWith("8888888"))
                    {
                        // 数量は印字しない
                        qty = false;

                        // 文字は大きくする
                        h1 = 16;
                        ff = f11;

                        // 数量を印字しないので、字数は増やせる
                        //                    len1 = 20;
                        len1 = 24;
                    }

                    string s = "";

                    for (int i = 0; i < detail.Name.Length; i++)
                    {
                        s += detail.Name[i];

                        if (AppString.LenB(s) > len1)
                        {
                            g.DrawString(s, ff, Brushes.Black, 0, h);

                            // まだ続く文字がある場合
                            if (i < detail.Name.Length - 1)
                            {
                                h += h1;
                            }

                            s = "";
                        }
                    }

                    g.DrawString(s, ff, Brushes.Black, 0, h);

                    // コメントの場合、数量は印字しない
                    if (qty)
                    {
                        g.DrawString(detail.QtyString + detail.Unit, ff, Brushes.Black, 172, h);
                    }

                    h += h1;
                }
                else
                {
                    yet_list.Add(detail);
                }
            }

            if (yet_list.Count > 0)
            {
                g.DrawString("★次ページへ続く", f10, Brushes.Black, 100, 212);
            }
            else
            {
                // 下のバーコードは、最後のページのみ印刷
                bar128 = new Barcode128();
                bar128.Code = "0" + this.detailCounter.ToString().PadLeft(2, '0') + "00" + order.OrderId.PadLeft(14, '0').Substring(0, 6) + order.OrderId.PadLeft(14, '0').Substring(11, 3);
                bar128.BarHeight = 20;

                img128 = bar128.CreateDrawingImage(Color.Black, Color.White);

                g.DrawImage(img128, 50, 212);
            }

            return yet_list;
        }

        private void PrintButton1_Click(object sender, EventArgs e)
        {
            printDocument1.PrinterSettings.PrinterName = "Brother QL-580N";

            // ディティールの回数を表す数値を 1 にリセット
            this.detailCounter = 1;

            // ディティールの最大回数を取得する
            float f = 1;

            foreach (PatOrderDetail detail in this.detailList)
            {
                if (detail.Times > f)
                {
                    f = detail.Times;
                }
            }


            // ディティールの最大回数分だけ印刷を繰り返す
            while (this.detailCounter <= f)
            {
                // 印刷するディティールリストをセット
                this.printList = this.detailList;

                // ページ番号を 1 にリセット
                this.pageNum = 1;

                printDocument1.Print();
/*
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
*/
                this.detailCounter++;
            }
        }

        private void DatePicker1_ValueChanged(object sender, EventArgs e)
        {
//            this.OrderListShow();
        }

        void OrderListShow()
        {
            // 前日と当日のオーダーを取得する
            string date1 = DatePicker1.Value.AddDays(-1).ToString("yyyyMMdd");
            string date2 = DatePicker1.Value.ToString("yyyyMMdd");

            // 入院患者一覧を取得
            this.patInList = PatIn.GetList();

            DataTable table = dSet.Tables["Pat"];
            table.Clear();

            foreach (PatIn obj in this.patInList)
            {
                DataRow r = table.NewRow();

                r["病棟コード"] = obj.Ward;
                r["病棟"] = obj.WardName;
                r["病室"] = obj.Room;
                r["患者コード"] = obj.Id;
                r["氏名"] = obj.Name;
                r["性別"] = obj.SexName;
                r["年齢"] = obj.Age;
                r["科コード"] = obj.Dept;
                r["科"] = obj.DeptName;

                table.Rows.Add(r);
            }

            List<string> shinku_list = new List<string>();
            shinku_list.Add("30");
            shinku_list.Add("31");
            shinku_list.Add("32");
            shinku_list.Add("33");

            List<string> empty_list = new List<string>();

            // オーダー一覧を取得
            orderList = PatOrder.GetListBySekouDates(date1, date2, "2", shinku_list, empty_list, empty_list, false, true);

            table = dSet.Tables["Order"];
            table.Clear();

            foreach (PatOrder obj in orderList)
            {
                DataRow r = table.NewRow();

                foreach (PatIn patIn in this.patInList)
                {
                    if (patIn.Id.Equals(obj.Pat.Id))
                    {
                        r["病棟コード"] = patIn.Ward;
                        r["病棟"] = patIn.WardName;
                        r["病室"] = patIn.Room;
                        break;
                    }
                }

                r["施行予定日"] = obj.SekouDateString;
                r["患者コード"] = obj.Pat.Id;
                r["氏名"] = obj.Pat.Name;
                r["性別"] = obj.Pat.SexNameShort;
                r["年齢"] = obj.Pat.AgeCalc(obj.SekouDate);
                r["診療区分コード"] = obj.Shinku;
                r["診区"] = obj.ShinkuString;
                r["入外区分コード"] = obj.InOut;
                r["入外"] = obj.InOutName;
                r["科コード"] = obj.Dept;
                r["科"] = obj.DeptName;
                r["指示医コード"] = obj.Doctor;
                r["医師"] = obj.DoctorName;
                r["オーダー番号"] = obj.OrderId;
                r["オーダー"] = obj.SOAP;
                r["施行フラグ"] = obj.SekouFlg;

                table.Rows.Add(r);
            }

            this.OrderListFormat();
        }

        void OrderListFormat()
        {
            DataView view = new DataView(dSet.Tables["Order"]);
            OrderListView1.DataSource = view;

            string filter = "病室 is not null";

            List<string> filters = new List<string>();

            if (this.SekouBox1.Checked)
            {
                filters.Add("施行フラグ = 0");
            }

            if (this.SekouBox2.Checked)
            {
                filters.Add("施行フラグ = 1 or 施行フラグ = 2");
            }

            if (filters.Count > 0)
            {
                filter += " and (" + AppString.ConcatList(filters, " or ") + ")";
            }
            else
            {
                filter += " and 施行フラグ <> 0 and 施行フラグ <> 1 and 施行フラグ <> 2";
            }

            if (this.WardBox1.Text.Contains(' '))
            {
                filter += " and 病棟コード = " + this.WardBox1.Text.Split(' ')[0];
            }

            view.RowFilter = filter;
            view.Sort = "病室, 患者コード";

            OrderListView1.Columns["病棟コード"].Visible = false;
            OrderListView1.Columns["病棟"].Visible = false;

            OrderListView1.Columns["病室"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OrderListView1.Columns["病室"].Width = 40;

            OrderListView1.Columns["施行予定日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OrderListView1.Columns["施行予定日"].Width = 70;
            OrderListView1.Columns["施行予定日"].HeaderText = "施行日";

            OrderListView1.Columns["患者コード"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            OrderListView1.Columns["患者コード"].Width = 50;
            OrderListView1.Columns["患者コード"].HeaderText = "コード";

            OrderListView1.Columns["氏名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            OrderListView1.Columns["氏名"].Width = 85;

            OrderListView1.Columns["性別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OrderListView1.Columns["性別"].Width = 35;

            OrderListView1.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OrderListView1.Columns["年齢"].Width = 35;

            OrderListView1.Columns["保険ビット"].Visible = false;

            OrderListView1.Columns["診療区分コード"].Visible = false;

            OrderListView1.Columns["診区"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OrderListView1.Columns["診区"].Width = 40;

            OrderListView1.Columns["連番"].Visible = false;
            OrderListView1.Columns["入外区分コード"].Visible = false;
            OrderListView1.Columns["入外"].Visible = false;

            OrderListView1.Columns["科コード"].Visible = false;

            OrderListView1.Columns["科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            OrderListView1.Columns["科"].Width = 50;

            OrderListView1.Columns["指示医コード"].Visible = false;

            OrderListView1.Columns["医師"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            OrderListView1.Columns["医師"].Width = 75;

            OrderListView1.Columns["オーダー番号"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            OrderListView1.Columns["オーダー番号"].Width = 20;
            OrderListView1.Columns["オーダー番号"].Visible = false;

            OrderListView1.Columns["オーダー"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            OrderListView1.Columns["オーダー"].Width = 180;

            OrderListView1.Columns["施行フラグ"].Visible = false;

            foreach (DataGridViewRow r in OrderListView1.Rows)
            {
                if (r.Cells["性別"].Value.ToString().Equals("女"))
                {
                    r.Cells["患者コード"].Style.ForeColor = Color.Red;
                    r.Cells["氏名"].Style.ForeColor = Color.Red;
                    r.Cells["性別"].Style.ForeColor = Color.Red;
                    r.Cells["年齢"].Style.ForeColor = Color.Red;
                }

                if (r.Cells["施行フラグ"].Value.ToString().Equals("1"))
                {
                    r.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192);
                }
                else if (r.Cells["施行フラグ"].Value.ToString().Equals("2"))
                {
                    r.DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
        }

        private void OrderListView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow r = OrderListView1.Rows[e.RowIndex];

            string pt_id = r.Cells["患者コード"].Value.ToString();
            string order_id = r.Cells["オーダー番号"].Value.ToString();

            this.patIn = new PatIn();

            foreach (PatIn tmp_pat in this.patInList)
            {
                if (!tmp_pat.Id.Equals(pt_id))
                {
                    continue;
                }

                this.patIn = tmp_pat;
                break;
            }

            this.patOrder = new PatOrder();
            this.detailList = new List<PatOrderDetail>();

            foreach (PatOrder tmp_order in this.orderList)
            {
                if (!tmp_order.OrderId.Equals(order_id))
                {
                    continue;
                }

                this.patOrder = tmp_order;
                this.detailList = tmp_order.DetailList;
                break;
            }

            PatLabelBox1.Image = new Bitmap(PatLabelBox1.Width, PatLabelBox1.Height);
            Graphics g = Graphics.FromImage(PatLabelBox1.Image);

            this.BarcodeDraw(g, patOrder, detailList);
        }

        private void ShowButton1_Click(object sender, EventArgs e)
        {
            this.OrderListShow();
        }

        void ExecListFormat()
        {
            DataView view = new DataView(dSet.Tables["Exec"]);

            view.Sort = "病室, 患者コード";

            ExecListView1.DataSource = view;

            ExecListView1.Columns["病棟コード"].Visible = false;
            ExecListView1.Columns["病棟"].Visible = false;

            ExecListView1.Columns["病室"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ExecListView1.Columns["病室"].Width = 40;

            ExecListView1.Columns["施行日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ExecListView1.Columns["施行日"].Width = 70;

            ExecListView1.Columns["患者コード"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            ExecListView1.Columns["患者コード"].Width = 50;
            ExecListView1.Columns["患者コード"].HeaderText = "コード";

            ExecListView1.Columns["氏名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ExecListView1.Columns["氏名"].Width = 85;

            ExecListView1.Columns["性別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ExecListView1.Columns["性別"].Width = 35;

            ExecListView1.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ExecListView1.Columns["年齢"].Width = 35;

            ExecListView1.Columns["診療区分コード"].Visible = false;

            ExecListView1.Columns["診区"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ExecListView1.Columns["診区"].Width = 40;
            ExecListView1.Columns["診区"].HeaderText = "診区";

            ExecListView1.Columns["入外区分コード"].Visible = false;
            ExecListView1.Columns["入外"].Visible = false;

            ExecListView1.Columns["科コード"].Visible = false;

            ExecListView1.Columns["科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ExecListView1.Columns["科"].Width = 50;

            ExecListView1.Columns["指示医コード"].Visible = false;

            ExecListView1.Columns["医師"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ExecListView1.Columns["医師"].Width = 75;

            ExecListView1.Columns["オーダー番号"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ExecListView1.Columns["オーダー番号"].Width = 20;
            ExecListView1.Columns["オーダー番号"].Visible = false;

            ExecListView1.Columns["オーダーコード"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ExecListView1.Columns["オーダーコード"].Width = 20;
            ExecListView1.Columns["オーダーコード"].Visible = false;

            ExecListView1.Columns["オーダー"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ExecListView1.Columns["オーダー"].Width = 120;

            ExecListView1.Columns["数量"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ExecListView1.Columns["数量"].Width = 35;

            ExecListView1.Columns["施行者コード"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ExecListView1.Columns["施行者コード"].Width = 35;
            ExecListView1.Columns["施行者コード"].Visible = false;

            ExecListView1.Columns["施行者"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ExecListView1.Columns["施行者"].Width = 65;

            ExecListView1.Columns["施行日時"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ExecListView1.Columns["施行日時"].Width = 100;

            foreach (DataGridViewRow r in ExecListView1.Rows)
            {
                if (r.Cells["性別"].Value.ToString().Equals("女"))
                {
                    r.Cells["患者コード"].Style.ForeColor = Color.Red;
                    r.Cells["氏名"].Style.ForeColor = Color.Red;
                    r.Cells["性別"].Style.ForeColor = Color.Red;
                    r.Cells["年齢"].Style.ForeColor = Color.Red;
                }
            }
        }


        private void FormExec_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.inputString += e.KeyChar;
        }

        private void FormExec_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ErrClear();

                this.inputString = this.inputString.TrimEnd('\r').Trim('\n');

                if (this.inputString.StartsWith("9") && this.inputString.Length == 14)
                {
                    // 9 で始まる 14 桁ならばオーダー

                    string order = "1" + this.inputString.Substring(1);
//                    this.InputOrderLabel1.Text = "1" + this.inputString.Substring(1);

                    foreach (PatOrder obj in this.orderExecList)
                    {
                        if (obj.OrderId.Equals(order))
                        {
                            this.ErrShow("このオーダーは既に読み込まれています。");
                            return;
                        }
                    }

                    bool exist_flg = false;

                    // 該当オーダーを確認する
                    foreach (DataGridViewRow r in this.OrderListView1.Rows)
                    {
                        if (!r.Cells["オーダー番号"].Value.ToString().Equals(order))
                        {
                            continue;
                        }

                        if (r.Cells["施行フラグ"].Value.ToString().Equals("1") ||
                            r.Cells["施行フラグ"].Value.ToString().Equals("2"))
                        {
                            this.ErrShow("このオーダーは既に施行されています。");
                            return;
                        }

                        exist_flg = true;
                    }

                    if (!exist_flg)
                    {
                        this.ErrShow("該当のオーダーがありません。古いオーダーの可能性があります。");
                        return;
                    }

                    // オーダーと患者の照合
                    if (this.OrderPatCheck(order).Length > 0)
                    {
                        this.ErrShow("オーダーと患者が一致しません。");
                        return;
                    }

                    // 該当オーダーの色を変える
                    foreach (DataGridViewRow r in this.OrderListView1.Rows)
                    {
                        if (!r.Cells["オーダー番号"].Value.ToString().Equals(order))
                        {
                            continue;
                        }

                        r.DefaultCellStyle.BackColor = Color.Orange;
                        break;
                    }

                    foreach (PatOrder obj in this.orderList)
                    {
                        if (obj.OrderId.Equals(order))
                        {
                            this.orderExecList.Add(obj);
                            break;
                        }
                    }

                    this.OrderExecPanelShow();
                }
                else if (this.inputString.StartsWith("MA") && this.inputString.Length == 9)
                {
                    // MA で始まる 9 桁ならば職員

                    this.StaffCodeLabel1.Text = this.inputString.Substring(2).TrimStart('0');
                    this.StaffNameLabel1.Text = "";

                    if (Dict.StaffDict.ContainsKey(this.StaffCodeLabel1.Text))
                    {
                        this.StaffNameLabel1.Text = Dict.StaffDict[this.StaffCodeLabel1.Text].Name;
                    }
                }
                else if (this.inputString.StartsWith("ME") && this.inputString.Length == 21)
                {
                    // ME で始まるならばおでんシート

                    string barcode = this.inputString.Substring(2);

                    foreach (PreBarcode obj in PreBarcode.MasterList)
                    {
                        if (obj.Barcode.Equals(barcode))
                        {
                            this.preBarcodeList.Add(obj);
                            break;
                        }
                    }

                    this.PreBarcodePanelShow();
                }
                else if (this.inputString.Length == 9)
                {
                    // 上記以外で 9 桁ならば患者

                    this.PatCodeLabel1.Text = this.inputString.TrimStart('0');
                    this.PatNameLabel1.Text = "";

                    foreach (PatIn obj in this.patInList)
                    {
                        if (obj.Id.Equals(this.PatCodeLabel1.Text))
                        {
                            this.PatNameLabel1.Text = obj.Name;
                            break;
                        }
                    }

                    // オーダーと患者の照合
                    foreach (PatOrder obj in this.orderExecList)
                    {
                        if (this.OrderPatCheck(obj.OrderId).Length > 0)
                        {
                            this.ErrShow("オーダーと患者が一致しません。");
                            this.PatCodeLabel1.Text = "";
                            this.PatNameLabel1.Text = "";
                            return;
                        }
                    }

                    // 該当患者の色を変える
                    foreach (DataGridViewRow r in this.OrderListView1.Rows)
                    {
                        if (r.Cells["患者コード"].Value.ToString().Equals(this.PatCodeLabel1.Text))
                        {
                            r.Cells["患者コード"].Style.BackColor = Color.Orange;
                            r.Cells["氏名"].Style.BackColor = Color.Orange;
                        }
                    }
                }
                else if (this.inputString.Equals("MZOK"))
                {
                    // 確定バーコード

                    if (this.StaffCodeLabel1.Text.Length > 0 && this.StaffCodeLabel1.Text.Length <= 7 &&
                        this.PatCodeLabel1.Text.Length > 0 && this.PatCodeLabel1.Text.Length <= 9 &&
                        (this.orderExecList.Count > 14 || this.preBarcodeList.Count > 0))
                    {
                        if (MessageBox.Show("施行します。よろしいですか？", "確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // オーダーを施行した場合

                            if (this.orderExecList.Count > 0)
                            {
                                DataTable table = dSet.Tables["Order"];
                                DataTable table2 = dSet.Tables["Exec"];

                                foreach (PatOrder obj in this.orderExecList)
                                {
                                    foreach (DataRow r in table.Rows)
                                    {
                                        if (!r["オーダー番号"].ToString().Equals(obj.OrderId))
                                        {
                                            continue;
                                        }

                                        r["施行フラグ"] = "2";

                                        DataRow r2 = table2.NewRow();

                                        r2["病棟コード"] = r["病棟コード"];
                                        r2["病棟"] = r["病棟"];
                                        r2["病室"] = r["病室"];
                                        r2["施行日"] = r["施行予定日"];
                                        r2["患者コード"] = r["患者コード"];
                                        r2["氏名"] = r["氏名"];
                                        r2["性別"] = r["性別"];
                                        r2["年齢"] = r["年齢"];

                                        r2["診療区分コード"] = r["診療区分コード"];
                                        r2["診区"] = r["診区"];

                                        r2["入外区分コード"] = r["入外区分コード"];
                                        r2["入外"] = r["入外"];

                                        r2["科コード"] = r["科コード"];
                                        r2["科"] = r["科"];

                                        r2["指示医コード"] = r["指示医コード"];
                                        r2["医師"] = r["医師"];

                                        r2["オーダー番号"] = r["オーダー番号"];
                                        r2["オーダー"] = r["オーダー"];

                                        r2["施行者コード"] = this.StaffCodeLabel1.Text;

                                        if (Dict.StaffDict.ContainsKey(this.StaffCodeLabel1.Text))
                                        {
                                            r2["施行者"] = Dict.StaffDict[this.StaffCodeLabel1.Text].Name;
                                        }

                                        r2["施行日時"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                                        table2.Rows.Add(r2);

                                        // ローカルPCのログに記録する。
                                        this.SekouLog(this.StaffCodeLabel1.Text + " " + r["患者コード"].ToString() + " " + r["オーダー番号"].ToString());
                                        break;
                                    }
                                }
                            }


                            // 先行実施の場合

                            if (this.preBarcodeList.Count > 0)
                            {
                                string pt_ward = "";
                                string pt_wardname = "";
                                string pt_room = "";
                                string pt_name = "";
                                string pt_sex = "";
                                string pt_age = "";
                                string pt_dept = "";
                                string pt_deptname = "";

                                foreach (DataRow r in dSet.Tables["Pat"].Rows)
                                {
                                    if (r["患者コード"].ToString().Equals(this.PatCodeLabel1.Text))
                                    {
                                        pt_ward = r["病棟コード"].ToString();
                                        pt_wardname = r["病棟"].ToString();
                                        pt_room = r["病室"].ToString();
                                        pt_name = r["氏名"].ToString();
                                        pt_sex = r["性別"].ToString();
                                        pt_age = r["年齢"].ToString();
                                        pt_dept = r["科コード"].ToString();
                                        pt_deptname = r["科"].ToString();
                                        break;
                                    }
                                }

                                DataTable table = dSet.Tables["Exec"];

                                foreach (PreBarcode obj in this.preBarcodeList)
                                {
                                    DataRow r = table.NewRow();

                                    r["病棟コード"] = pt_ward;
                                    r["病棟"] = pt_wardname;
                                    r["病室"] = pt_room;
                                    r["施行日"] = DateTime.Now.ToString("yyyy/MM/dd");
                                    r["患者コード"] = this.PatCodeLabel1.Text;
                                    r["氏名"] = pt_name;
                                    r["性別"] = pt_sex;
                                    r["年齢"] = pt_age;

                                    r["診療区分コード"] = obj.KouiCode;
                                    r["診区"] = obj.KouiName;

                                    r["入外区分コード"] = "2";
                                    r["入外"] = "入院";

                                    r["科コード"] = pt_dept;
                                    r["科"] = pt_deptname;

                                    r["オーダーコード"] = obj.OrderCode;
                                    r["オーダー"] = obj.OrderName;
                                    r["数量"] = obj.Qty;

                                    r["施行者コード"] = this.StaffCodeLabel1.Text;

                                    if (Dict.StaffDict.ContainsKey(this.StaffCodeLabel1.Text))
                                    {
                                        r["施行者"] = Dict.StaffDict[this.StaffCodeLabel1.Text].Name;
                                    }

                                    r["施行日時"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                                    table.Rows.Add(r);

                                    // ローカルPCのログに記録する。
                                    this.SekouLog(this.StaffCodeLabel1.Text + " " + this.PatCodeLabel1.Text + " " + obj.LogString);
                                }
                            }

                            this.ExecListFormat();

//                            MessageBox.Show("施行しました");
                            this.InputClear();
                        }
                    }
                }
                else if (this.inputString.Equals("MZCLEAR"))
                {
                    // 取消バーコード
                    this.InputClear();
                }

                this.inputString = "";
            }
            else if (e.KeyCode == Keys.F5)
            {
                this.InputClear();
            }
        }

        void pb4_DoubleClick(object sender, EventArgs e)
        {
            TextBox c = (TextBox)sender;

            if (c.ReadOnly)
            {
                c.ReadOnly = false;
            }
            else
            {
                c.ReadOnly = true;
            }
        }

        void pb4_TextChanged(object sender, EventArgs e)
        {
            TextBox c = (TextBox)sender;
            float.TryParse(c.Text, out this.preBarcodeList[int.Parse(c.Tag.ToString())].Qty);
        }

        void po9_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("削除します。よろしいですか？", "確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Label lb = (Label)sender;
                this.orderExecList.RemoveAt(int.Parse(lb.Tag.ToString()));

                this.OrderExecPanelShow();
            }
        }

        void pb9_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("削除します。よろしいですか？", "確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Label lb = (Label)sender;
                this.preBarcodeList.RemoveAt(int.Parse(lb.Tag.ToString()));

                this.PreBarcodePanelShow();
            }
        }

        /// <summary>
        /// オーダー実施パネルの表示
        /// </summary>
        void OrderExecPanelShow()
        {
            this.OrderExecPanel1.Controls.Clear();

            int h = 2;

            for (int i = 0; i < this.orderExecList.Count; i++)
            {
                PatOrder obj = this.orderExecList[i];

                Label p1 = new Label();
                p1.Size = new System.Drawing.Size(40, 18);
                p1.TextAlign = ContentAlignment.MiddleCenter;
                p1.Location = new Point(2, h);
                p1.BackColor = Color.LightYellow;
                p1.Text = obj.ShinkuString;
                p1.BorderStyle = BorderStyle.FixedSingle;

                OrderExecPanel1.Controls.Add(p1);

                Label p2 = new Label();
                p2.Size = new System.Drawing.Size(60, 18);
                p2.TextAlign = ContentAlignment.MiddleLeft;
                p2.Location = new Point(44, h);
                p2.BackColor = Color.LightYellow;
                p2.Text = obj.OrderId;
                p2.AutoEllipsis = true;
                p2.Visible = false;
                p2.BorderStyle = BorderStyle.FixedSingle;

                OrderExecPanel1.Controls.Add(p2);

                Label p3 = new Label();
                p3.Size = new System.Drawing.Size(190, 18);
                p3.TextAlign = ContentAlignment.MiddleLeft;
                p3.Location = new Point(46, h);
                p3.BackColor = Color.LightYellow;
                p3.Text = obj.SOAP;
                p3.AutoEllipsis = true;
                p3.BorderStyle = BorderStyle.FixedSingle;

                OrderExecPanel1.Controls.Add(p3);

                Label p9 = new Label();
                p9.Size = new System.Drawing.Size(40, 18);
                p9.TextAlign = ContentAlignment.MiddleCenter;
                p9.Location = new Point(240, h);
                p9.BackColor = Color.LightPink;
                p9.Text = "削除";
                p9.Tag = i;
                p9.Click += new EventHandler(po9_Click);

                OrderExecPanel1.Controls.Add(p9);

                h += 20;
            }
        }

        /// <summary>
        /// 先行実施パネルの表示
        /// </summary>
        void PreBarcodePanelShow()
        {
            this.PreBarcodePanel1.Controls.Clear();

            int h = 2;

            for (int i = 0; i < this.preBarcodeList.Count; i++)
            {
                PreBarcode obj = this.preBarcodeList[i];

                Label p1 = new Label();
                p1.Size = new System.Drawing.Size(40, 18);
                p1.TextAlign = ContentAlignment.MiddleCenter;
                p1.Location = new Point(2, h);
                p1.BackColor = Color.LightYellow;
                p1.Text = obj.KouiName;
                p1.BorderStyle = BorderStyle.FixedSingle;

                PreBarcodePanel1.Controls.Add(p1);

                Label p2 = new Label();
                p2.Size = new System.Drawing.Size(60, 18);
                p2.TextAlign = ContentAlignment.MiddleLeft;
                p2.Location = new Point(44, h);
                p2.BackColor = Color.LightYellow;
                p2.Text = obj.OrderCode;
                p2.AutoEllipsis = true;
                p2.Visible = false;
                p2.BorderStyle = BorderStyle.FixedSingle;

                PreBarcodePanel1.Controls.Add(p2);

                Label p3 = new Label();
                p3.Size = new System.Drawing.Size(160, 18);
                p3.TextAlign = ContentAlignment.MiddleLeft;
                p3.Location = new Point(46, h);
                p3.BackColor = Color.LightYellow;
                p3.Text = obj.OrderName;
                p3.AutoEllipsis = true;
                p3.BorderStyle = BorderStyle.FixedSingle;

                PreBarcodePanel1.Controls.Add(p3);

                TextBox p4 = new TextBox();
                p4.Size = new Size(30, 18);
                p4.TextAlign = HorizontalAlignment.Center;
                p4.Location = new Point(208, h);
                p4.BackColor = Color.LightYellow;
                p4.Text = obj.Qty.ToString();
                p4.ReadOnly = true;
                p4.MaxLength = 5;
                p4.BorderStyle = BorderStyle.FixedSingle;
                p4.Tag = i;
                p4.DoubleClick += new EventHandler(pb4_DoubleClick);
                p4.TextChanged += new EventHandler(pb4_TextChanged);

                PreBarcodePanel1.Controls.Add(p4);

                Label p9 = new Label();
                p9.Size = new System.Drawing.Size(40, 18);
                p9.TextAlign = ContentAlignment.MiddleCenter;
                p9.Location = new Point(240, h);
                p9.BackColor = Color.LightPink;
                p9.Text = "削除";
                p9.Tag = i;
                p9.Click += new EventHandler(pb9_Click);

                PreBarcodePanel1.Controls.Add(p9);

                h += 20;
            }
        }

        /// <summary>
        /// ローカルＰＣの施行ログファイルに記録する
        /// </summary>
        /// <param name="msg"></param>
        void SekouLog(string msg)
        {
            string file = Environment.CurrentDirectory + "\\exec.log";

            using (StreamWriter sw1 = new StreamWriter(file, true, Encoding.Default))
            {
                sw1.Write(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " " + msg + Environment.NewLine);
            }
        }

        /// <summary>
        /// 読み込んだバーコード情報をいったんクリアする
        /// </summary>
        void InputClear()
        {
            this.StaffCodeLabel1.Text = "";
            this.StaffNameLabel1.Text = "";

            this.PatCodeLabel1.Text = "";
            this.PatNameLabel1.Text = "";

            this.orderExecList.Clear();
            this.OrderExecPanelShow();

            this.preBarcodeList.Clear();
            this.PreBarcodePanelShow();

            this.ErrClear();

            this.inputString = "";

            this.OrderListFormat();
        }

        string OrderPatCheck(string order_id)
        {
            string err = "";

            string pt_label = this.PatCodeLabel1.Text;

            if (pt_label.Length == 0)
            {
                return err;
            }

            DataTable table = dSet.Tables["Order"];

            foreach (DataRow r in table.Rows)
            {
                if (!r["オーダー番号"].ToString().Equals(order_id))
                {
                    continue;
                }

                if (pt_label.Length > 0)
                {
                    if (!r["患者コード"].ToString().Equals(pt_label))
                    {
                        err += "オーダーと患者が一致しません" + Environment.NewLine;
                    }
                }

                break;
            }

            return err;
        }

        private void PatLabelBox1_DoubleClick(object sender, EventArgs e)
        {
            if (this.PrintButton1.Enabled)
            {
                this.PrintButton1.Enabled = false;
            }
            else
            {
                this.PrintButton1.Enabled = true;
            }
        }

        private void SekouBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.OrderListFormat();
        }

        private void SekouBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.OrderListFormat();
        }

        void ErrShow(string err)
        {
            this.ErrLabel1.Text = err;
            this.ErrLabel1.BackColor = Color.LightPink;

            this.inputString = "";
        }

        void ErrClear()
        {
            this.ErrLabel1.Text = "";
            this.ErrLabel1.BackColor = Color.Transparent;
        }

        private void WardBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.OrderListFormat();
        }
    }
}
