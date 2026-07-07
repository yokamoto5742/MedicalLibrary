using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class FormDeptStat : StdForm1
    {
        DataSet dSet = new DataSet();

        public FormDeptStat()
        {
            InitializeComponent();

            DataTable table = dSet.Tables.Add("受付状況");
            table.Columns.Add("科コード", typeof(int));
            table.Columns.Add("診療科");
            table.Columns.Add("区分");
            table.Columns.Add("受付人数", typeof(int));
            table.Columns.Add("診察未終了", typeof(int));

            this.DatePicker1.MaxDate = DateTime.Now.AddDays(3);
            this.DatePicker1.Value = DateTime.Now;

            // デフォルトで「通常」にする。
            KindButton1.Checked = true;

            this.TickBox1.Items.Add(30);
            this.TickBox1.Items.Add(60);
            this.TickBox1.Items.Add(120);
            this.TickBox1.Text = "60";

            this.ListShow();

            this.Timer1.Interval = int.Parse(this.TickBox1.Text) * 1000;
            this.Timer1.Enabled = true;
        }

        /// <summary>
        /// 集計リスト表示
        /// </summary>
        private void ListShow()
        {
            string come_date = this.DatePicker1.Value.ToString("yyyyMMdd");

            DataTable table = dSet.Tables["受付状況"];
            table.Rows.Clear();

            List<DeptOut> list1 = DeptOut.GetList(come_date);
            List<DeptOut> list2 = DeptOut.GetYetList(come_date);

            foreach (DeptOut obj in list1)
            {
                DataRow r = table.NewRow();

                r["科コード"] = obj.DeptCode;
                r["診療科"] = obj.DeptName;
                r["区分"] = obj.ModeCode;
                r["受付人数"] = obj.Count;

                table.Rows.Add(r);
            }

            foreach (DeptOut obj in list2)
            {
                foreach (DataRow r in table.Rows)
                {
                    if (r["科コード"].ToString().Equals(obj.DeptCode) &&
                        r["区分"].ToString().Equals(obj.ModeCode))
                    {
                        r["診察未終了"] = obj.Count;
                        break;
                    }
                }
            }

            this.ListFormat();
        }

        void ListFormat()
        {
            List<string> filters = new List<string>();

            if (KindButton1.Checked)
            {
                filters.Add("区分 = 1");
            }
            else
            {
                filters.Add("区分 = 2");
            }

            DataView view = new DataView(dSet.Tables["受付状況"]);
            view.RowFilter = AppString.ConcatList(filters, " and ");

            view.Sort = "科コード";

            DeptListView1.DataSource = view;

            DeptListView1.Columns["科コード"].Width = 40;
            DeptListView1.Columns["科コード"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DeptListView1.Columns["診療科"].Width = 80;
            DeptListView1.Columns["区分"].Visible = false;

            DeptListView1.Columns["受付人数"].Width = 60;
            DeptListView1.Columns["受付人数"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            DeptListView1.Columns["診察未終了"].Width = 60;
            DeptListView1.Columns["診察未終了"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


            // 全体受付人数
            int all_count0 = 0;

            for (int i = 0; i < DeptListView1.RowCount; i++)
            {
                DataGridViewRow r = DeptListView1.Rows[i];

                all_count0 += Int16.Parse(r.Cells["受付人数"].Value.ToString());
            }

            label_all_count0.Text = "受付人数　" + all_count0 + " 人";
        }

        private void KindButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.ListFormat();
        }

        private void KindButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.ListFormat();
        }

        private void DatePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.ListShow();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            this.ListShow();
        }

        private void TickBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Timer1.Interval = int.Parse(this.TickBox1.Text) * 1000;
        }
    }
}
