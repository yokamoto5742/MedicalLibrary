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
    public partial class ReceBillForm : Form
    {
        ReceBillSettings Settings = new ReceBillSettings();

        private int pageNumber = 1;      // 現在のページ番号
        private int pageCount = 1;      // ページ数

        DataSet oraSet = new DataSet();

        public ReceBillForm()
        {
            InitializeComponent();
        }

        private void ReceBillForm_Load(object sender, EventArgs e)
        {
            LibSettings.Init();
            this.Settings.Init();

            foreach (string k in Dict.DeptDict.Keys)
            {
                if (!k.Equals("0"))
                {
                    deptBox.Items.Add(k + " " + Dict.DeptDict[k].ShortName);
                }
            }

            inOut1.Checked = true;
            touseki2.Checked = true;
            excelButton.Enabled = false;
            printButton.Enabled = false;

            DataTable table = oraSet.Tables.Add("会計未取込");

            table.Columns.Add("日付");
            table.Columns.Add("ID");
            table.Columns.Add("氏名");
            table.Columns.Add("性別");
            table.Columns.Add("透在引");
            table.Columns.Add("診区");
            table.Columns.Add("入外");
            table.Columns.Add("診療科");
            table.Columns.Add("医師");
            table.Columns.Add("内容");
			table.Columns.Add("Obj", typeof(PatOrder));
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            this.findBill();
        }

        private void findBill()
        {
            if (deptBox.CheckedItems.Count == 0)
            {
                MessageBox.Show("診療科が一つも選択されていません");
                return;
            }

            string in_out = "";

            if (inOut1.Checked)
            {
                in_out = "1";
            }
            else if (inOut2.Checked)
            {
                in_out = "2";
            }

            List<string> dept_list = new List<string>();

            if (deptBox.CheckedItems.Count != deptBox.Items.Count)
            {
                for (int i = 0; i < deptBox.CheckedItems.Count; i++)
                {
                    dept_list.Add(deptBox.CheckedItems[i].ToString().Split(' ')[0]);
                }
            }
#if INNO
            List<PatOrder> list = PatOrder.GetListByDatesNotKaikei(dateTimePicker1.Value.ToString("yyyyMMdd"), dateTimePicker2.Value.ToString("yyyyMMdd"), in_out, dept_list, true);
#else
            List<PatOrder> list = PatOrder.GetListByDatesNotKaikei(dateTimePicker1.Value.ToString("yyyyMMdd"), dateTimePicker2.Value.ToString("yyyyMMdd"), in_out, dept_list, true);
#endif
            DataTable table = oraSet.Tables["会計未取込"];
            table.Clear();

            bool b = true;

            foreach (PatOrder obj in list)
            {
                b = true;

                // 除外リストに載っているかどうか
                foreach (PatOrderDetail detail in obj.DetailList)
                {
                    // Rp 先頭のオーダーコードが除外リストに載っていれば取り込まない
                    if (this.Settings.ExcludeCodeDict.ContainsKey(detail.Code))
                    {
                        b = false;
                    }

                    break;
                }

                // 除外リストに載っていれば取り込まない
                if (!b) continue;

                DataRow r = table.NewRow();

                r["日付"] = obj.SekouDateString;
                r["ID"] = obj.Pat.Id;
                r["氏名"] = obj.Pat.Name;
                r["性別"] = obj.Pat.Sex;
                r["透在引"] = obj.Pat.Note1.ReceBillMark;
                r["診区"] = obj.Shinku;
                r["入外"] = obj.InOut;
                r["診療科"] = obj.DeptName;
                r["医師"] = obj.DoctorName;
                r["内容"] = obj.SOAP;
				r["Obj"] = obj;

                table.Rows.Add(r);
            }

            filterBill();
        }

        void filterBill()
        {
            if (!oraSet.Tables.Contains("会計未取込"))
            {
                return;
            }

            DataView tmpView = new DataView(oraSet.Tables["会計未取込"]);

            List<string> filters = new List<string>();

            if (touseki1.Checked)
            {
                filters.Add("(透在引 = '透' or 透在引 = '在' or 透在引 = '引')");
            }
            else if (touseki2.Checked)
            {
                filters.Add("(透在引 is null or 透在引 = '')");
            }

            if (FilterBox.Text.Length > 0)
            {
                filters.Add("(ID = '" + this.FilterBox.Text + "' or 氏名 like '%" + this.FilterBox.Text + "%' or 内容 like '%" + this.FilterBox.Text + "%')");
            }

            if (ExcludeBox.Text.Length > 0)
            {
                foreach (string s in ExcludeBox.Text.Replace("　", " ").Split(' '))
                {
                    filters.Add("(内容 not like '%" + s + "%')");
                }
            }

            if (filters.Count > 0)
            {
                tmpView.RowFilter = AppString.ConcatList(filters, " and ");
            }

            ListView1.DataSource = tmpView;

            ListView1.Columns["日付"].DefaultCellStyle.Format = "00/00/00";
            ListView1.Columns["日付"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView1.Columns["日付"].HeaderText = "日付";
            ListView1.Columns["日付"].Width = 75;

            ListView1.Columns["ID"].HeaderText = "ID";
            ListView1.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            ListView1.Columns["ID"].Width = 60;

            ListView1.Columns["氏名"].HeaderText = "氏名";
            ListView1.Columns["氏名"].Width = 85;

            ListView1.Columns["性別"].Visible = false;

            ListView1.Columns["透在引"].HeaderText = "透在引";
            ListView1.Columns["透在引"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView1.Columns["透在引"].Width = 35;

            ListView1.Columns["診区"].HeaderText = "診区";
            ListView1.Columns["診区"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView1.Columns["診区"].Width = 40;

            ListView1.Columns["入外"].HeaderText = "入外";
            ListView1.Columns["入外"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView1.Columns["入外"].Width = 40;

            ListView1.Columns["診療科"].HeaderText = "診療科";
            ListView1.Columns["診療科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView1.Columns["診療科"].Width = 70;

            ListView1.Columns["医師"].HeaderText = "医師";
            ListView1.Columns["医師"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView1.Columns["医師"].Width = 75;

            ListView1.Columns["内容"].HeaderText = "内容";
            ListView1.Columns["内容"].Width = 315;

			ListView1.Columns["Obj"].Visible = false;

            this.countLabel.Text = "検索件数  " + ListView1.RowCount + " 件";

            AppDataGridView.SexColor(this.ListView1);

            if (ListView1.RowCount > 0)
            {
                excelButton.Enabled = true;
                printButton.Enabled = true;
            }
            else
            {
                excelButton.Enabled = false;
                printButton.Enabled = false;
            }
        }

        private void selectAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < deptBox.Items.Count; i++)
            {
                deptBox.SetItemChecked(i, true);
            }
        }

        private void selectNoButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < deptBox.Items.Count; i++)
            {
                deptBox.SetItemChecked(i, false);
            }
        }

        private void excelButton_Click(object sender, EventArgs e)
        {
            TableData data = new TableData(this.ListView1, false);

            if (data.ExcelOpen(true))
            {
                MessageBox.Show("Excel出力が完了しました");
            }
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            pageNumber = 1;
            pageCount = (int)(ListView1.RowCount / 50) + 1;

            printDialog1.PrinterSettings = new System.Drawing.Printing.PrinterSettings();

            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.PrinterSettings.PrinterName = printDialog1.PrinterSettings.PrinterName;
                printDocument1.Print();

                /*
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
                 */
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font f20 = new Font("", 20);
            Font f16 = new Font("", 16);
            Font f14 = new Font("", 14);
            Font f12 = new Font("", 12);
            Font f10 = new Font("", 10);
            Font f9 = new Font("", 9);

            Pen p1 = new Pen(Brushes.Black, 1);

            if (pageNumber <= pageCount)
            {
                e.Graphics.DrawString(pageNumber + " ページ", f10, Brushes.Black, 730, 30);

                e.Graphics.DrawString("会計未取込オーダー", f16, Brushes.Black, 50, 50);
                e.Graphics.DrawString("作成日 : " + DateTime.Now.ToString("yyyy年 M月 d日"), f10, Brushes.Black, 350, 55);

                e.Graphics.DrawString("日付", f10, Brushes.Black, 50, 100);
                e.Graphics.DrawString("ID", f10, Brushes.Black, 130, 100);
                e.Graphics.DrawString("氏名", f10, Brushes.Black, 200, 100);
                e.Graphics.DrawString("透在引", f9, Brushes.Black, 300, 100);
                e.Graphics.DrawString("診区", f9, Brushes.Black, 350, 100);
                e.Graphics.DrawString("入外", f9, Brushes.Black, 395, 100);
                e.Graphics.DrawString("診療科", f10, Brushes.Black, 430, 100);
                e.Graphics.DrawString("医師", f10, Brushes.Black, 510, 100);
                e.Graphics.DrawString("内容", f10, Brushes.Black, 600, 100);

                e.Graphics.DrawLine(p1, 50, 123, 750, 123);

                int h = 130;

                for (int i = (pageNumber - 1) * 50; i < pageNumber * 50 && i < ListView1.RowCount; i++)
                {
                    DataGridViewRow r = ListView1.Rows[i];

                    e.Graphics.DrawString(r.Cells["日付"].Value.ToString().Substring(2), f10, Brushes.Black, 50, h);
                    e.Graphics.DrawString(r.Cells["ID"].Value.ToString(), f10, Brushes.Black, 130, h);
                    e.Graphics.DrawString(r.Cells["氏名"].Value.ToString(), f10, Brushes.Black, 200, h);
                    e.Graphics.DrawString(r.Cells["透在引"].Value.ToString(), f10, Brushes.Black, 315, h);
                    e.Graphics.DrawString(r.Cells["診区"].Value.ToString(), f10, Brushes.Black, 355, h);
                    e.Graphics.DrawString(r.Cells["入外"].Value.ToString(), f10, Brushes.Black, 400, h);
                    e.Graphics.DrawString(r.Cells["診療科"].Value.ToString(), f10, Brushes.Black, 430, h);
                    e.Graphics.DrawString(r.Cells["医師"].Value.ToString(), f10, Brushes.Black, 510, h);

                    if (r.Cells[8].Value.ToString().Length > 10)
                    {
                        e.Graphics.DrawString(r.Cells["内容"].Value.ToString().Substring(0, 10) + " ...", f9, Brushes.Black, 600, h);
                    }
                    else
                    {
                        e.Graphics.DrawString(r.Cells["内容"].Value.ToString(), f9, Brushes.Black, 600, h);
                    }

                    h += 20;
                }

                if (pageNumber < pageCount)
                {
                    e.HasMorePages = true;
                }
                else
                {
                    e.HasMorePages = false;
                }
            }

            pageNumber++;
       }

        private void touseki0_CheckedChanged(object sender, EventArgs e)
        {
            filterBill();
        }

        private void touseki1_CheckedChanged(object sender, EventArgs e)
        {
            filterBill();
        }

        private void touseki2_CheckedChanged(object sender, EventArgs e)
        {
            filterBill();
        }

        private void FilterBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                filterBill();
            }
        }

        private void ExcludeBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                filterBill();
            }
        }

        private void ExcludeCodesLabel_Click(object sender, EventArgs e)
        {
            string s = "";

            foreach (string key in this.Settings.ExcludeCodeDict.Keys)
            {
                if (s.Length > 0)
                {
                    s += Environment.NewLine;
                }

                s += key.PadRight(12, ' ');

                if (this.Settings.ExcludeCodeDict[key].Length > 0)
                {
                    s += "  " + this.Settings.ExcludeCodeDict[key];
                }
            }

            if (s.Length == 0)
            {
                s += "除外指定されているオーダーコードはありません";
            }

            MessageBox.Show(s);
        }

		private void ListView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0) return;

			PatOrder obj = (PatOrder)ListView1.Rows[e.RowIndex].Cells["Obj"].Value;

			FormControl.FormOrderKaikei_Show(PatBase.Load(obj.Pat.Id)
				, obj.InOut.Equals("1") ? FormOrderKaikei.Mode.Out : FormOrderKaikei.Mode.In
				, obj.StartDate
				, obj.EndDate
				, 1
				, obj.Dept
				, obj.Doctor
				, obj.Pat.Ins);
		}
    }
}