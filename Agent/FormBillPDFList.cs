using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using MedicalLibrary.Boundary;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class FormBillPDFList : StdForm1
    {
        string ListViewSort = "";
        SortOrder ListViewSortOrder = SortOrder.Ascending;

        bool tick_mode = true;

        public bool TickMode
        {
            set
            {
                this.tick_mode = value;

                if (this.tick_mode)
                {
                    this.TickModeButton1.Text = "自動更新";
                    this.TickModeButton1.BackColor = Color.LightYellow;
                    this.TickModeButton1.ForeColor = Color.Red;

                    this.TickIntervalBox1.Enabled = true;

                    this.Timer1.Start();
                }
                else
                {
                    this.TickModeButton1.Text = "手動更新";
                    this.TickModeButton1.BackColor = Color.White;
                    this.TickModeButton1.ForeColor = Color.Black;

                    this.TickIntervalBox1.Enabled = false;

                    this.Timer1.Stop();
                }
            }
            get
            {
                return this.tick_mode;
            }
        }

        DataSet dSet = new DataSet();

        List<BillFile> FileList = new List<BillFile>();

        public FormBillPDFList()
        {
            InitializeComponent();

            DataTable table = dSet.Tables.Add("PDF");
            table.Columns.Add("日付");
            table.Columns.Add("種別");
            table.Columns.Add("控え");
            table.Columns.Add("フルパス");
            table.Columns.Add("場所");
            table.Columns.Add("ファイル名");
            table.Columns.Add("患者ID");
            table.Columns.Add("氏名");
            table.Columns.Add("性別");
            table.Columns.Add("年齢");
            table.Columns.Add("請求書番号");
            table.Columns.Add("番号");
            table.Columns.Add("作成日時");
        }

        private void FormBillPDFList_Load(object sender, EventArgs e)
        {
            // 更新間隔（秒）
            this.TickIntervalBox1.Items.Add(30);
            this.TickIntervalBox1.Items.Add(60);
            this.TickIntervalBox1.Items.Add(180);
            this.TickIntervalBox1.Items.Add(600);
            this.TickIntervalBox1.Text = "60";

            this.ListShow();

            this.Timer1.Interval = 60 * 1000;
            this.TickMode = true;
        }

        void ListShow()
        {
            // 元の請求書・明細書PDF・スクロール位置をセットする
            string file = "";
            int y = this.ListView1.FirstDisplayedScrollingRowIndex;

            if (this.ListView1.SelectedRows.Count > 0)
            {
                file = this.ListView1.SelectedRows[0].Cells["ファイル名"].Value.ToString();
            }

            this.FileList.Clear();

            // 各フォルダにあるPDFファイルを取ってくる

            // BillPdfSrc
            foreach (string s in Directory.GetFiles(BillMakerSettings.Current.BillPdfSrc, "*.pdf"))
            {
                BillFile obj = new BillFile();

                obj.Kind = "BillPdfSrc";
                obj.FullFileName = s;

                this.FileList.Add(obj);
            }

            // BillPdfDst
            foreach (string s in Directory.GetFiles(BillMakerSettings.Current.BillPdfDst, "*.pdf"))
            {
                BillFile obj = new BillFile();

                obj.Kind = "BillPdfDst";
                obj.FullFileName = s;

                this.FileList.Add(obj);
            }

            // BillPdfMid
            foreach (string s in Directory.GetFiles(BillMakerSettings.Current.BillPdfMid, "*.pdf"))
            {
                BillFile obj = new BillFile();

                obj.Kind = "BillPdfMid";
                obj.FullFileName = s;

                this.FileList.Add(obj);
            }

            // BillErr
            foreach (string s in Directory.GetFiles(BillMakerSettings.Current.BillErr, "*.pdf"))
            {
                BillFile obj = new BillFile();

                obj.Kind = "BillErr";
                obj.FullFileName = s;

                this.FileList.Add(obj);
            }


            // InvoicePdfSrc
            foreach (string s in Directory.GetFiles(BillMakerSettings.Current.InvoicePdfSrc, "*.pdf"))
            {
                BillFile obj = new BillFile();

                obj.Kind = "InvoicePdfSrc";
                obj.FullFileName = s;

                this.FileList.Add(obj);
            }

            // InvoicePdfDst
            foreach (string s in Directory.GetFiles(BillMakerSettings.Current.InvoicePdfDst, "*.pdf"))
            {
                BillFile obj = new BillFile();

                obj.Kind = "InvoicePdfDst";
                obj.FullFileName = s;

                this.FileList.Add(obj);
            }

            // InvoicePdfMid
            foreach (string s in Directory.GetFiles(BillMakerSettings.Current.InvoicePdfMid, "*.pdf"))
            {
                BillFile obj = new BillFile();

                obj.Kind = "InvoicePdfMid";
                obj.FullFileName = s;

                this.FileList.Add(obj);
            }

            // InvoiceErr
            foreach (string s in Directory.GetFiles(BillMakerSettings.Current.InvoiceErr, "*.pdf"))
            {
                BillFile obj = new BillFile();

                obj.Kind = "InvoiceErr";
                obj.FullFileName = s;

                this.FileList.Add(obj);
            }


            DataTable table = dSet.Tables["PDF"];
            table.Rows.Clear();

            List<string> pt_list = new List<string>();
            string today = DateTime.Now.ToString("yyyy/MM/dd");

            foreach (BillFile obj in this.FileList)
            {
                if (!this.ShowPastBox1.Checked)
                {
                    if (!obj.SaveDate.Equals(today))
                    {
                        continue;
                    }
                }

                DataRow r = table.NewRow();

                r["日付"] = obj.SaveDate;
                r["種別"] = obj.KindString;
                r["控え"] = obj.Sub ? "○" : "";
                r["フルパス"] = obj.FullFileName;
                r["場所"] = obj.PathName;
                r["ファイル名"] = obj.FileName;
                r["患者ID"] = obj.PtId;
                r["請求書番号"] = obj.BillId;
                r["番号"] = obj.PageNum;
                r["作成日時"] = obj.SaveDateTime;

                if (obj.PtId.Length > 0 && !pt_list.Contains(obj.PtId))
                {
                    pt_list.Add(obj.PtId);
                }

                table.Rows.Add(r);
            }

            List<PatBase> pat_list = PatBase.GetList(pt_list);

            foreach (DataRow r in table.Rows)
            {
                foreach (PatBase p in pat_list)
                {
                    if (r["患者ID"].ToString().Equals(p.Id))
                    {
                        r["氏名"] = p.Name;
                        r["性別"] = p.Sex;
                        r["年齢"] = p.Age;
                        break;
                    }
                }
            }

            this.ListFormat();

            // 元のファイル名を選択する
            if (file.Length > 0)
            {
                foreach (DataGridViewRow r in this.ListView1.Rows)
                {
                    if (r.Cells["ファイル名"].Value.ToString().Equals(file))
                    {
                        r.Selected = true;
                        break;
                    }
                }
            }

            if (y >= 0 && y < this.ListView1.Rows.Count)
            {
                this.ListView1.FirstDisplayedScrollingRowIndex = y;
            }
        }

        void ListFormat()
        {
            DataTable table = dSet.Tables["PDF"];

            DataView view = new DataView(table);

            view.Sort = "作成日時 desc";

            string filter = "";
            List<string> filters = new List<string>();

            if (this.FilterBox1.Text.Length > 0)
            {
                filters.Add("患者ID = '" + this.FilterBox1.Text.TrimStart('0') + "'");
            }

            if (this.FilterBox2.Text.Length > 0)
            {
                filters.Add("氏名 like '%" + this.FilterBox2.Text + "%'");
            }

            if (this.FilterBox3.Text.Length > 0)
            {
                filters.Add("請求書番号 = '" + this.FilterBox3.Text + "'");
            }

            if (filters.Count > 0)
            {
                filter = AppString.ConcatList(filters, " or ");
            }

            if (!this.ShowSubBox1.Checked)
            {
                if (filter.Length > 0)
                {
                    filter = "(" + filter + ") and ";
                }

                filter += "控え = ''";
            }

            view.RowFilter = filter;

            ListView1.DataSource = view;

            ListView1.Columns["日付"].Width = 75;
            ListView1.Columns["日付"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView1.Columns["種別"].Width = 35;
            ListView1.Columns["種別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView1.Columns["控え"].Width = 35;
            ListView1.Columns["控え"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView1.Columns["控え"].Visible = false;

            ListView1.Columns["フルパス"].Visible = false;

            ListView1.Columns["場所"].Width = 140;
            ListView1.Columns["場所"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            ListView1.Columns["ファイル名"].Width = 100;
            ListView1.Columns["ファイル名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            ListView1.Columns["患者ID"].Width = 60;
            ListView1.Columns["患者ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            ListView1.Columns["氏名"].Width = 80;
            ListView1.Columns["氏名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            ListView1.Columns["性別"].Visible = false;
            ListView1.Columns["性別"].Width = 35;
            ListView1.Columns["性別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView1.Columns["年齢"].Width = 35;
            ListView1.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView1.Columns["請求書番号"].Width = 80;
            ListView1.Columns["請求書番号"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            ListView1.Columns["番号"].Width = 35;
            ListView1.Columns["番号"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView1.Columns["作成日時"].Width = 110;
            ListView1.Columns["作成日時"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewRow r in this.ListView1.Rows)
            {
                if (r.Cells["種別"].Value.ToString().StartsWith("請"))
                {
                    r.Cells["種別"].Style.ForeColor = Color.Red;
                }
                else if (r.Cells["種別"].Value.ToString().StartsWith("明"))
                {
                    r.Cells["種別"].Style.ForeColor = Color.Blue;
                }

                if (r.Cells["性別"].Value.ToString().Equals("2"))
                {
                    r.Cells["氏名"].Style.ForeColor = Color.Red;
                }
            }
        }

        private void ShowSubBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.ListFormat();
        }

        private void ListView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }

            string s = ListView1.Rows[e.RowIndex].Cells["フルパス"].Value.ToString();
            string k = ListView1.Rows[e.RowIndex].Cells["種別"].Value.ToString();

            if (File.Exists(s))
            {
                if (k.Equals("請"))
                {
                    FormPDFViewer1 f = new FormPDFViewer1(FormPDFViewer1.PageOrientation.Landscape);
                    f.Navigate(s);
                    f.Show();
                }
                else if (k.Equals("明"))
                {
                    FormPDFViewer1 f = new FormPDFViewer1(FormPDFViewer1.PageOrientation.Portrait);
                    f.Navigate(s);
                    f.Show();
                }
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            this.ListShow();
        }

        private void ShowButton1_Click(object sender, EventArgs e)
        {
            this.ListShow();
        }

        private void PrintButton1_Click(object sender, EventArgs e)
        {
            this.Print();
        }

        private void ShowPastBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ShowPastBox1.Checked)
            {
                MessageBox.Show("過去データも表示するには「更新」ボタンをクリックしてください");
            }
        }

        private void TickIntervalBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Timer1.Interval = int.Parse(this.TickIntervalBox1.Text) * 1000;
        }

        private void TickModeButton1_Click(object sender, EventArgs e)
        {
            this.TickMode = !this.TickMode;
        }

        private void FormPDFView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                this.FilterBox1.Clear();
                this.FilterBox1.Focus();
            }
            else if (e.KeyCode == Keys.F5)
            {
                this.ListShow();
            }
            else if (e.KeyCode == Keys.F8)
            {
                this.Print();
            }
        }

        private void ListView1_Sorted(object sender, EventArgs e)
        {
            this.ListViewSort = this.ListView1.SortedColumn.Name;
            this.ListViewSortOrder = this.ListView1.SortOrder;

            this.ListFormat();
        }

        void Print()
        {
            try
            {
                if (ListView1.SelectedRows.Count > 0)
                {
                    string s = ListView1.SelectedRows[0].Cells["フルパス"].Value.ToString();
                    string n = ListView1.SelectedRows[0].Cells["請求書番号"].Value.ToString();
                    string k1 = ListView1.SelectedRows[0].Cells["種別"].Value.ToString();
                    string k2 = "";

                    string msg = "";

                    if (File.Exists(s))
                    {
                        if (k1.StartsWith("請"))
                        {
                            msg = "明細書も印刷しますか？";
                            k2 = "明";
                            Launcher.PDFPrint(s, LibSettings.Current.PC.BillPrinter);
                        }
                        else if (k1.StartsWith("明"))
                        {
                            msg = "請求書も印刷しますか？";
                            k2 = "請";
                            Launcher.PDFPrint(s, LibSettings.Current.PC.InvoicePrinter);
                        }
                    }

                    if (msg.Length > 0 && MessageBox.Show(msg, "確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow r in ListView1.Rows)
                        {
                            if (r.Cells["請求書番号"].Value.ToString().Equals(n) && r.Cells["種別"].Value.ToString().Equals(k2))
                            {
                                s = r.Cells["フルパス"].Value.ToString();

                                if (File.Exists(s))
                                {
                                    if (k2.StartsWith("請"))
                                    {
                                        Launcher.PDFPrint(s, LibSettings.Current.PC.BillPrinter);
                                    }
                                    else if (k2.StartsWith("明"))
                                    {
                                        Launcher.PDFPrint(s, LibSettings.Current.PC.InvoicePrinter);
                                    }
                                }

                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex);
            }
        }

        private void FilterBox1_TextChanged(object sender, EventArgs e)
        {
            this.ListFormat();
        }

        private void FilterBox2_TextChanged(object sender, EventArgs e)
        {
            this.ListFormat();
        }

        private void FilterBox3_TextChanged(object sender, EventArgs e)
        {
            this.ListFormat();
        }
    }

    class BillFile
    {
        public string Kind = "";

        public string KindString
        {
            get
            {
                string s = "";

                if (Kind.StartsWith("Bill"))
                {
                    s = "請";
                }
                else if (Kind.StartsWith("Invoice"))
                {
                    s = "明";
                }

                return s;
            }
        }

        public string FullFileName = "";

        public string PathName
        {
            get
            {
                string s = "";

                if (this.FullFileName.LastIndexOf('\\') > 0)
                {
                    s = this.FullFileName.Substring(0, this.FullFileName.LastIndexOf('\\'));
                }

                return s;
            }
        }

        public string FileName
        {
            get
            {
                string s = "";

                if (this.FullFileName.Contains("\\"))
                {
                    s = this.FullFileName.Substring(this.FullFileName.LastIndexOf('\\') + 1);
                }

                return s;
            }
        }

        public bool Sub
        {
            get
            {
                bool b = false;

                if (this.FileName.EndsWith("_sub.pdf"))
                {
                    b = true;
                }

                return b;
            }
        }

        public string PageNum
        {
            get
            {
                string s = "";

                if (this.FileName.StartsWith("Invoice_") &&
                    this.FileName.Split('_').Length >= 4)
                {
                    s = this.FileName.Split('_')[3];
                    s = s.Substring(0, s.LastIndexOf('.'));
                }

                return s;
            }
        }

        public string PtId
        {
            get
            {
                string s = "";

                if (this.FileName.Split('_').Length > 2)
                {
                    if (this.FileName.StartsWith("Bill_") ||
                        this.FileName.StartsWith("Invoice_"))
                    {
                        s = this.FileName.Split('_')[1];
                    }
                }

                return s;
            }
        }

        public string BillId
        {
            get
            {
                string s = "";

                if (this.FileName.Split('_').Length > 2)
                {
                    if (this.FileName.StartsWith("Bill_") ||
                        this.FileName.StartsWith("Invoice_"))
                    {
                        s = this.FileName.Split('_')[2];
                    }
                }

                if (s.IndexOf('.') > 0)
                {
                    s = s.Substring(0, s.IndexOf('.'));
                }

                return s;
            }
        }

        public string SaveDate
        {
            get
            {
                string s = "";

                if (File.Exists(this.FullFileName))
                {
                    FileInfo fi = new FileInfo(this.FullFileName);
                    s = fi.CreationTime.ToString("yyyy/MM/dd");
                }

                return s;
            }
        }

        public string SaveDateTime
        {
            get
            {
                string s = "";

                if (File.Exists(this.FullFileName))
                {
                    FileInfo fi = new FileInfo(this.FullFileName);
                    s = fi.CreationTime.ToString("yyyy/MM/dd HH:mm:ss");
                }

                return s;
            }
        }
    }
}
