using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;
using MedicalLibrary.Boundary;

namespace MedicalLibrary.Agent
{
    public partial class PatLabelSeqForm : Form
    {
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

        /// <summary>
        /// 印字する受付番号
        /// </summary>
        string SEQ = "";

        public PatLabelSeqForm()
        {
            InitializeComponent();
        }

        private void PatLabelSeqForm_Load(object sender, EventArgs e)
        {
            LibSettings.Init();

            DataTable tmpTable = DSet.Tables.Add("受付");
            tmpTable.Columns.Add("印刷", typeof(bool));
            tmpTable.Columns.Add("番号");
            tmpTable.Columns.Add("受付");
            tmpTable.Columns.Add("科");
        }

        void AllClear()
        {
            this.SEQ = "";

            this.PtIdBox.Clear();
            this.PtInfoLabel.Text = "";

            this.ListViewClear();

            PtIdBox.Focus();
        }

        void ListViewClear()
        {
            this.SEQ = "";

            DataTable tmpTable = DSet.Tables["受付"];
            tmpTable.Clear();

            ListView.DataSource = new DataView(tmpTable);
        }

        void PtShow()
        {
            int i = 0;

            if (this.PtIdBox.Text.Length == 0 || !int.TryParse(this.PtIdBox.Text, out i))
            {
                AllClear();
                return;
            }

            this.PtInfoLabel.Text = Pat.Name + "（" + Pat.Kana + "）様";
            this.PtNameLabel.Text = Pat.Kana;

            List<PatOut> tmpList = PatOut.GetOneday(this.Pat.Id, this.ComeDate.Value.ToString("yyyyMMdd"));

            DataTable tmpTable = DSet.Tables["受付"];
            tmpTable.Clear();

            foreach (PatOut p in tmpList)
            {
                DataRow r = tmpTable.NewRow();

                r["印刷"] = true;
                r["番号"] = p.Seq1;
                r["受付"] = p.Time1.PadLeft(4, '0').Insert(2, ":");

                if (Dict.DeptDict.ContainsKey(p.Dept))
                {
                    r["科"] = Dict.DeptDict[p.Dept].ShortName;
                }

                tmpTable.Rows.Add(r);
            }

            ListView.DataSource = new DataView(tmpTable);

            ListView.Columns["印刷"].Width = 55;

            ListView.Columns["番号"].Width = 55;
            ListView.Columns["番号"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView.Columns["受付"].Width = 55;
            ListView.Columns["受付"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView.Columns["科"].Width = 80;
            ListView.Columns["科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            PrintButton.Focus();
        }

        void LabelPrint()
        {
            List<string> tmpList = new List<string>();

            foreach (DataGridViewRow r in ListView.Rows)
            {
                if (r.Cells["印刷"].Value.Equals(true) && !tmpList.Contains(r.Cells["番号"].Value.ToString()))
                {
                    tmpList.Add(r.Cells["番号"].Value.ToString());
                }
            }

            printDocument1.PrinterSettings.PrinterName = "Brother QL-580N";

            foreach (string s in tmpList)
            {
                this.SEQ = s;
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
            }
        }

        private void PatLabelSeqForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                this.AllClear();
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

        private void PtIdBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PtShow();
            }
        }

        private void ListView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                PrintButton.Focus();
            }
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            this.LabelPrint();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font f60 = new Font("", 60);
            Font f18 = new Font("", 18);
            Font f12 = new Font("", 12);
            Font f9 = new Font("", 9);

            e.Graphics.DrawString(PtNameLabel.Text + " 様", f18, Brushes.Black, 20, 20);
            e.Graphics.DrawString("下記番号にてお呼びします", f12, Brushes.Black, 20, 60);
            e.Graphics.DrawString(this.SEQ.PadLeft(4, ' '), f60, Brushes.Black, 20, 100);
            e.Graphics.DrawString("真生会富山病院　" + DateTimeAgent.DateFormat(DateTime.Now.ToString("yyyyMMdd"), DateTimeAgent.DateFormatKind.LONG), f9, Brushes.Black, 40, 220);

            e.HasMorePages = false;
        }
    }
}