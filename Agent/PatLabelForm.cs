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
    public partial class PatLabelForm : Form
    {
        PatLabelSettings Settings = new PatLabelSettings();

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

        DataSet DSet = new DataSet();

        Font f10 = new Font("", 10);
        Font f9 = new Font("", 9);
        Font f8 = new Font("", 8);

        int print_total = 0;
        int print_counter = 0;
        int order_counter = 0;

        public PatLabelForm()
        {
            InitializeComponent();
        }

        private void PatLabelForm_Load(object sender, EventArgs e)
        {
            // プログラムで一度も実行されていなければ実行する
            LibSettings.Init();

            this.Settings.Init();

            DataTable tmpTable = DSet.Tables.Add("Order");
            tmpTable.Columns.Add("印刷", typeof(bool));
            tmpTable.Columns.Add("日付");
            tmpTable.Columns.Add("診療区分");
            tmpTable.Columns.Add("診療");
            tmpTable.Columns.Add("入外区分");
            tmpTable.Columns.Add("入外");
            tmpTable.Columns.Add("科コード");
            tmpTable.Columns.Add("科");
            tmpTable.Columns.Add("指示医コード");
            tmpTable.Columns.Add("医師");
            tmpTable.Columns.Add("指示日");
            tmpTable.Columns.Add("指示時間");
            tmpTable.Columns.Add("内容");

            OrderView.DataSource = new DataView(tmpTable);

            foreach (string key in this.Settings.ShinkuDict.Keys)
            {
                string value = this.Settings.ShinkuDict[key];

                CheckBox tmpBox = new CheckBox();
                tmpBox.AutoSize = true;
                tmpBox.Name = value;
                tmpBox.Text = value;
                tmpBox.Tag = key;
                tmpBox.CheckStateChanged += new EventHandler(DeptShinkuBox_CheckStateChanged);

                if (this.Settings.PcShinkuList.Contains(key))
                {
                    tmpBox.Checked = true;
                }

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

                if (this.Settings.PcDeptList.Contains(key))
                {
                    tmpBox.Checked = true;
                }

                DeptPanel.Controls.Add(tmpBox);
            }

            if (this.Settings.PcInOutList.Contains("1"))
            {
                InOutBox1.Checked = true;
            }

            if (this.Settings.PcInOutList.Contains("2"))
            {
                InOutBox2.Checked = true;
            }

            this.InitShow(Environment.GetCommandLineArgs());
        }

        private void PatLabelForm_Shown(object sender, EventArgs e)
        {
            PtIdBox.Focus();
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

        void InitShow(string[] args = null)
        {
            int pat_id = 0;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Equals("-p", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (i < args.Length - 1 && int.TryParse(args[i + 1], out pat_id))
                    {
                        // 次のパラメータが数字ならば患者IDとみなす
                        this._Pat = PatBase.Load(pat_id.ToString());
                        this.PtIdBox.Text = this._Pat.Id;
                        i++;
                    }
                }

                if (args[i].Equals("-d", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (i < args.Length - 1 && DateTimeAgent.IsDate(args[i + 1]))
                    {
                        this.SekouDate.Value = DateTime.Parse(DateTimeAgent.DateFormat(args[i + 1], DateTimeAgent.DateFormatKind.LONG));
                        i++;
                    }
                }
            }

            if (PtIdBox.Text.Length > 0)
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

            PtInfoLabel.Text = Pat.Name + " 様　" + Pat.SexNameShort + "　" + Pat.BirthStringJ + "生　" + Pat.Age + "歳";

            List<string> empty_list = new List<string>();
            List<string> shinku_list = new List<string>();

            foreach (string key in this.Settings.ShinkuDict.Keys)
            {
                if (!shinku_list.Contains(key))
                {
                    shinku_list.Add(key);
                }
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
#if INNO
			List<PatOrder> tmpList = PatOrder.GetListByPatSekouDates(this.Pat.Id, SekouDate.Value.ToString("yyyyMMdd"), SekouDate.Value.ToString("yyyyMMdd"), in_out, shinku_list, empty_list, empty_list, MiteiBox.Checked, true);
#else
            List<PatOrder> tmpList = PatOrder.GetListByPatSekouDates(this.Pat.Id, SekouDate.Value.ToString("yyyyMMdd"), SekouDate.Value.ToString("yyyyMMdd"), in_out, shinku_list, empty_list, empty_list, MiteiBox.Checked, false);
#endif
            DataTable tmpTable = DSet.Tables["Order"];

            foreach (PatOrder order in tmpList)
            {
                DataRow r = tmpTable.NewRow();

                r["印刷"] = true;
                r["日付"] = order.SekouDate.Equals("99999999") ? "未定" : order.SekouDateStringShort;
                r["診療区分"] = order.Shinku;
                r["診療"] = this.Settings.ShinkuDict.ContainsKey(order.Shinku) ? this.Settings.ShinkuDict[order.Shinku] : "";
                r["入外区分"] = order.InOut;
                r["入外"] = order.InOutName;
                r["科コード"] = order.Dept;
                r["科"] = this.Settings.DeptDict.ContainsKey(order.Dept) ? this.Settings.DeptDict[order.Dept] : "";
                r["指示医コード"] = order.Doctor;
                r["医師"] = Dict.DoctorDict.ContainsKey(order.Doctor) ? Dict.DoctorDict[order.Doctor].Name : "";
                r["指示日"] = order.OrderDate;
                r["指示時間"] = order.OrderTime;
                r["内容"] = order.SOAP;

                tmpTable.Rows.Add(r);
            }

            OrderView.DataSource = new DataView(tmpTable);
            OrderFormat();

            PrintButton.Enabled = true;
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
            OrderView.Columns["指示時間"].Visible = false;

            OrderView.Columns["内容"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            OrderView.Columns["内容"].Width = 215;
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
            PrintButton.Enabled = false;
        }

        private void PtIdBox_TextChanged(object sender, EventArgs e)
        {
            PrintButton.Enabled = false;
        }

        private void PatLabelForm_KeyDown(object sender, KeyEventArgs e)
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

            if (!PrintButton.Enabled)
            {
                MessageBox.Show("患者IDを入力して Enter を押してください");
                return;
            }

            print_total = 0;
            print_counter = 0;
            order_counter = 0;

            foreach (DataGridViewRow r in OrderView.Rows)
            {
                if (r.Cells["印刷"].Value.Equals(true))
                {
                    print_total++;
                }
            }

            // XML内に該当PCのプリンタ情報があればそれを使用する。
            if (print_total > 0 && this.Settings.Printer.Length > 0)
            {
                printDocument1.PrinterSettings.PrinterName = this.Settings.Printer;
            }
            else if (print_total == 0 && this.Settings.PrinterSmall.Length > 0)
            {
                printDocument1.PrinterSettings.PrinterName = this.Settings.PrinterSmall;
            }
            else
            {
                printDialog1.PrinterSettings = new System.Drawing.Printing.PrinterSettings();

                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                }
            }

            if (this.Settings.PrintView)
            {
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
            }
            else
            {
                try
                {
                    printDocument1.DocumentName = this.Pat.Id + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
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
            for (int i = order_counter; i < OrderView.RowCount; i++)
            {
                DataGridViewRow r = OrderView.Rows[i];

                if (r.Cells["印刷"].Value.Equals(true))
                {
                    order_counter = i;
                    break;
                }
            }

            e.Graphics.DrawString(Pat.Id, f10, Brushes.Black, 10, 10);

            Barcode128 bar128 = new Barcode128();
            bar128.CodeType = Barcode.CODE128;
            bar128.CodeSet = Barcode128.Barcode128CodeSet.B;
            bar128.Code = this.Pat.Id;

            float bar_width = this.Pat.Id.Length * 35;

            if (bar_width < 100)
            {
                bar_width = 100;
            }
            else if (bar_width > 130)
            {
                bar_width = 130;
            }

            System.Drawing.Image img128 = bar128.CreateDrawingImage(Color.Black, Color.White);
            e.Graphics.DrawImage(img128, 85, 5, bar_width, 25);

            e.Graphics.DrawString(Pat.Name + "　様　　" + Pat.SexNameShort, f10, Brushes.Black, 10, 35);
            e.Graphics.DrawString(Pat.BirthStringJ + "生　" + Pat.Age + "歳", f8, Brushes.Black, 50, 55);

            if (order_counter < OrderView.RowCount)
            {
                DataGridViewRow r = OrderView.Rows[order_counter];

                e.Graphics.DrawString(r.Cells["日付"].Value.ToString() + "　[" + r.Cells["入外"].Value.ToString() + "]　" + r.Cells["科"].Value.ToString() + "　" + r.Cells["医師"].Value.ToString(), f8, Brushes.Black, new RectangleF(5, 80, 225, 33));
                e.Graphics.DrawString(r.Cells["内容"].Value.ToString(), f8, Brushes.Black, new RectangleF(5, 115, 225, 115));
            }
            else
            {
                if (OrderView.RowCount == 0)
                {
                    e.Graphics.DrawString("※該当オーダーはありません", f9, Brushes.Black, new RectangleF(25, 80, 205, 33));
                }
                else
                {
                    e.Graphics.DrawString("※オーダー印刷なし", f9, Brushes.Black, new RectangleF(25, 80, 205, 33));
                }
            }

            print_counter++;
            order_counter++;

            if (print_counter < print_total)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
                print_counter = 0;
                order_counter = 0;
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
    }
}