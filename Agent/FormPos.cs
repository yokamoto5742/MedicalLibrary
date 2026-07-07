using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Boundary;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class FormPos : StdForm1
    {
        DataSet DSet = new DataSet();

        /// <summary>
        /// 患者の保険データの辞書
        /// </summary>
        Dictionary<int, PatIns> PatInsDict = new Dictionary<int, PatIns>();

        public FormPos()
        {
            InitializeComponent();
        }

        public FormPos(string pt_id)
        {
            InitializeComponent();

            this.Pat = PatBase.Load(pt_id);
        }

        private void PosForm_Load(object sender, EventArgs e)
        {
            DataTable table = DSet.Tables.Add("List1");
            table.Columns.Add("区分");
            table.Columns.Add("請求日");
            table.Columns.Add("請求書ID");
			table.Columns.Add("診療日");
			table.Columns.Add("請求額", typeof(int));
            table.Columns.Add("入金額", typeof(int));
            table.Columns.Add("支払");
            table.Columns.Add("OK");
            table.Columns.Add("支払日");
            table.Columns.Add("備考");
            table.Columns.Add("書損");
            table.Columns.Add("保険");
            table.Columns.Add("科");
            table.Columns.Add("医師");
            table.Columns.Add("Obj", typeof(PosDemand));

            table = DSet.Tables.Add("List2");
            table.Columns.Add("日付");
            table.Columns.Add("時刻");
            table.Columns.Add("請求書ID");
            table.Columns.Add("金額", typeof(int));
            table.Columns.Add("支払");
            table.Columns.Add("Obj", typeof(PosRegHistoryDetail));

            this.PatSet(this.Pat);

            this.PayYetBox.Checked = true;
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            this.PatInsDict = PatIns.GetDict(p.Id);

            this.ListShow();
        }

        void ListShow()
        {
            List<PosDemand> list1 = PosDemand.GetListByPat(this.Pat.Id);
            List<PosRegHistoryDetail> list2 = PosRegHistoryDetail.GetListByPat(this.Pat.Id);

            DataTable table = DSet.Tables["List1"];
            table.Clear();

            // 書損リスト
            List<string> list3 = new List<string>();

            // まず書損となっているものの請求書IDを取得
            foreach (PosDemand obj in list1)
            {
                if (obj.Kind.Equals("3") && !list3.Contains(obj.BillId))
                {
                    list3.Add(obj.BillId);
                }
            }

            // 未収総額
            int sum_yet = 0;

            foreach (PosDemand obj in list1)
            {
                DataRow r = table.NewRow();

                r["区分"] = obj.Kind.Equals("3") ? "削" : "";
                r["請求日"] = DateTimeAgent.DateFormat(obj.DemDate, DateTimeAgent.DateFormatKind.SHORT);
                r["請求書ID"] = obj.BillId;
				r["診療日"] = obj.DemTerm;
				r["請求額"] = obj.BillMoney;
                r["入金額"] = obj.PartMoney;
                r["支払"] = obj.PayKubunStringShort;
                r["OK"] = obj.OkFlgString;
                r["支払日"] = DateTimeAgent.DateFormat(obj.LastPayDate, DateTimeAgent.DateFormatKind.SHORT);
                r["備考"] = obj.Bikou;
                r["保険"] = this.PatInsDict.ContainsKey(obj.Ins) ? this.PatInsDict[obj.Ins].KindNameShort : "";
                r["科"] = obj.DeptName;
                r["医師"] = obj.DoctorName;
                r["Obj"] = obj;

                // 書損フラグ
                if (list3.Contains(obj.BillId))
                {
                    r["書損"] = "1";
                }

                sum_yet += obj.YetMoney;

                table.Rows.Add(r);
            }

            if (sum_yet > 0)
            {
                this.PayYetLabel.ForeColor = Color.Red;
                this.PayYetLabel.Text = "未収総額 \\" + String.Format("{0:#,0}", sum_yet);
            }
            else
            {
                this.PayYetLabel.ForeColor = Color.Black;
                this.PayYetLabel.Text = "未収なし";
            }

            table = DSet.Tables["List2"];
            table.Clear();

            foreach (PosRegHistoryDetail obj in list2)
            {
                DataRow r = table.NewRow();

                r["日付"] = DateTimeAgent.DateFormat(obj.ADate, DateTimeAgent.DateFormatKind.SHORT);
                r["時刻"] = DateTimeAgent.TimeFormat6(obj.ATime, 4, true);
                r["請求書ID"] = obj.BillId;
                r["金額"] = obj.PayMoney;
                r["支払"] = obj.PayKubunStringShort;
                r["Obj"] = obj;

                table.Rows.Add(r);
            }

            ListFormat1();
            ListFormat2();
        }

        void ListFormat1()
        {
            DataTable table = DSet.Tables["List1"];
            DataView view = new DataView(table);

            if (this.PayYetBox.Checked)
            {
                view.RowFilter = "OK = '未納' or OK = '一部'";
            }

            this.ListView1.DataSource = view;
            this.ListView1.Columns["区分"].Width = 30;
            this.ListView1.Columns["区分"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.ListView1.Columns["区分"].Visible = false;
			this.ListView1.Columns["請求日"].Width = 60;
            this.ListView1.Columns["請求日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView1.Columns["請求書ID"].Width = 80;
            this.ListView1.Columns["請求書ID"].Visible = true;
			this.ListView1.Columns["診療日"].Width = 90;
			this.ListView1.Columns["診療日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			this.ListView1.Columns["請求額"].Width = 55;
            this.ListView1.Columns["請求額"].DefaultCellStyle.Format = "#,0";
            this.ListView1.Columns["請求額"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.ListView1.Columns["入金額"].Width = 55;
            this.ListView1.Columns["入金額"].DefaultCellStyle.Format = "#,0";
            this.ListView1.Columns["入金額"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.ListView1.Columns["支払"].Width = 40;
            this.ListView1.Columns["支払"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView1.Columns["OK"].Width = 40;
            this.ListView1.Columns["OK"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView1.Columns["支払日"].Width = 60;
            this.ListView1.Columns["支払日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView1.Columns["備考"].Width = 60;
            this.ListView1.Columns["書損"].Visible = false;
            this.ListView1.Columns["保険"].Width = 40;
            this.ListView1.Columns["保険"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView1.Columns["科"].Width = 45;
            this.ListView1.Columns["医師"].Width = 70;
            this.ListView1.Columns["Obj"].Visible = false;

            string today = DateTime.Now.ToString("yyyy/MM/dd");

            foreach (DataGridViewRow r in this.ListView1.Rows)
            {
                if (r.Cells["書損"].Value.ToString().Equals("1"))
                {
                    r.DefaultCellStyle.Font = AppFont.FD9.Ft;
                    r.DefaultCellStyle.BackColor = Color.LightGray;
                }
                else
                {
                    switch (r.Cells["OK"].Value.ToString())
                    {
                        case "一部":
                            r.Cells["OK"].Style.BackColor = Color.Yellow;
                            break;
                        case "未納":
                            r.Cells["OK"].Style.BackColor = Color.LightPink;
                            break;
                        case "書損":
                            r.Cells["OK"].Style.BackColor = Color.LightGray;
                            break;
                        default:
                            break;
                    }

                    if (r.Cells["請求日"].Value.ToString().Equals(today))
                    {
                        r.Cells["請求日"].Style.BackColor = Color.Yellow;
                        r.Cells["請求書ID"].Style.BackColor = Color.Yellow;
                    }

                    if ((int)r.Cells["請求額"].Value < 0)
                    {
                        r.Cells["請求額"].Style.ForeColor = Color.Red;
                    }

                    if ((int)r.Cells["入金額"].Value < 0)
                    {
                        r.Cells["入金額"].Style.ForeColor = Color.Red;
                    }
                }
            }
        }

        void ListFormat2()
        {
            DataTable table = DSet.Tables["List2"];
            DataView view = new DataView(table);

            if (this.BillIdBox.Text.Length > 0)
            {
                view.RowFilter = "請求書ID like '%" + this.BillIdBox.Text + "%'";

                string file = LibSettings.Current.BillPdfFolder + "\\Bill_" + this.Pat.Id + "_" + this.BillIdBox.Text + ".pdf";

                if (File.Exists(file))
                {
                    this.BillPdfLabel.Text = Path.GetFileName(file);
                    this.BillPdfBrowser.Navigate(file);
                }
                else
                {
                    this.BillPdfLabel.Text = "請求書PDFファイルはありません";
                    this.BillPdfBrowser.Navigate("");
                }
            }
            else
            {
                this.BillPdfLabel.Text = "";
                this.BillPdfBrowser.Navigate("");
            }

            this.ListView2.DataSource = view;
            this.ListView2.Columns["日付"].Width = 60;
            this.ListView2.Columns["日付"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView2.Columns["時刻"].Width = 45;
            this.ListView2.Columns["時刻"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView2.Columns["請求書ID"].Width = 80;
            this.ListView2.Columns["請求書ID"].Visible = true;
            this.ListView2.Columns["金額"].Width = 55;
            this.ListView2.Columns["金額"].DefaultCellStyle.Format = "#,0";
            this.ListView2.Columns["金額"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.ListView2.Columns["支払"].Width = 40;
            this.ListView2.Columns["支払"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView2.Columns["Obj"].Visible = false;

            string today = DateTime.Now.ToString("yyyy/MM/dd");

            foreach (DataGridViewRow r in this.ListView2.Rows)
            {
                if (r.Cells["日付"].Value.ToString().Equals(today))
                {
                    r.Cells["日付"].Style.BackColor = Color.Yellow;
                    r.Cells["時刻"].Style.BackColor = Color.Yellow;
                    r.Cells["請求書ID"].Style.BackColor = Color.Yellow;
                }

                if ((int)r.Cells["金額"].Value < 0)
                {
                    r.Cells["金額"].Style.ForeColor = Color.Red;
                }
            }
        }

        private void ListView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (this.ListView1.Columns[e.ColumnIndex].Name.Equals("請求書ID"))
            {
                this.BillIdBox.Text = this.ListView1.Rows[e.RowIndex].Cells["請求書ID"].Value.ToString();
                this.ListFormat2();
            }
        }

        private void BillIdBoxClearButton_Click(object sender, EventArgs e)
        {
            this.BillIdBox.Clear();
            this.ListFormat2();
        }

        private void PayYetBox_CheckedChanged(object sender, EventArgs e)
        {
            this.ListFormat1();
        }
    }
}
