using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class FormPatRsv : StdForm1
    {
        DataSet dSet = new DataSet();

        public FormPatRsv()
        {
            InitializeComponent();

            DataTable table = dSet.Tables.Add("Rsv");
            table.Columns.Add("予約日");
            table.Columns.Add("時間");
            table.Columns.Add("予約種別コード");
            table.Columns.Add("予約種別");
            table.Columns.Add("予約詳細コード");
            table.Columns.Add("予約詳細");
            table.Columns.Add("備考１");
            table.Columns.Add("備考２");
            table.Columns.Add("連番");
			table.Columns.Add("Date");
			table.Columns.Add("Obj", typeof(RsvData));
        }

        private void FormPatRsv_Load(object sender, EventArgs e)
        {
            this.ListShow();
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.ListShow();
        }

        void ListShow()
        {
            DataTable table = dSet.Tables["Rsv"];
            table.Clear();

            List<RsvData> list = RsvData.GetListByPat(this.Pat.Id);
			int today = int.Parse(DateTime.Now.ToString("yyyyMMdd"));

            foreach (RsvData obj in list)
            {
                DataRow r = table.NewRow();

                r["予約日"] = DateTimeAgent.DateFormat(obj.RsvDate, DateTimeAgent.DateFormatKind.WLONG);
                r["時間"] = obj.TimeString("-");
                r["予約種別コード"] = obj.Code1;
                r["予約種別"] = obj.Name1;
                r["予約詳細コード"] = obj.Code2;
                r["予約詳細"] = obj.Name2;
                r["備考１"] = obj.Cont1;
                r["備考２"] = obj.Cont2;
                r["連番"] = obj.SEQ;
				r["Obj"] = obj;

				// 本日以降かどうか
				if (obj.RsvDate > today)
				{
					r["Date"] = "1";
				}
				else if (obj.RsvDate == today)
				{
					r["Date"] = "0";
				}
				else
				{
					r["Date"] = "-1";
				}

                table.Rows.Add(r);
            }

            this.ListFormat();
        }

        void ListFormat()
        {
            DataView view = new DataView(dSet.Tables["Rsv"]);

			if (!this.PastBox.Checked)
			{
				view.RowFilter = "Date <> '-1'";
			}

            ListView1.DataSource = view;

            ListView1.Columns["予約種別コード"].Visible = false;
            ListView1.Columns["予約種別"].Width = 80;

            ListView1.Columns["予約詳細コード"].Visible = false;
            ListView1.Columns["予約詳細"].Width = 80;

            ListView1.Columns["予約日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView1.Columns["予約日"].Width = 90;

            ListView1.Columns["時間"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView1.Columns["時間"].Width = 80;

            ListView1.Columns["備考１"].Width = 100;
            ListView1.Columns["備考２"].Width = 100;

            ListView1.Columns["連番"].Visible = false;
			ListView1.Columns["Date"].Visible = false;
			ListView1.Columns["Obj"].Visible = false;

            foreach (DataGridViewRow r in this.ListView1.Rows)
            {
				/*
                int dt = int.Parse(r.Cells["予約日"].Value.ToString().Substring(0, 10).Replace("/", ""));
                int today = int.Parse(DateTime.Now.ToString("yyyyMMdd"));

                if (dt > today)
                {
                    // 未来の場合
//                    r.DefaultCellStyle.BackColor = Color.LightYellow;
                }
				else if (dt == today)
				{
					// 今日の場合
					r.DefaultCellStyle.ForeColor = Color.Red;
				}
				else
				{
					// 過去の場合
					r.DefaultCellStyle.BackColor = Color.LightGray;
				}
				 */

				string dt = r.Cells["Date"].Value.ToString();

				if (dt.Equals("0"))
				{
					// 今日の場合
					r.DefaultCellStyle.ForeColor = Color.Red;
				}
				else if (dt.Equals("-1"))
				{
					// 過去の場合
					r.DefaultCellStyle.BackColor = Color.LightGray;
				}
            }
        }

        private void ListView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int date1 = int.Parse(ListView1.CurrentRow.Cells["予約日"].Value.ToString().Substring(0, 10).Replace("/", ""));
            string code1 = ListView1.CurrentRow.Cells["予約種別コード"].Value.ToString();
            string name1 = ListView1.CurrentRow.Cells["予約種別"].Value.ToString();
            string code2 = ListView1.CurrentRow.Cells["予約詳細コード"].Value.ToString();
            string name2 = ListView1.CurrentRow.Cells["予約詳細"].Value.ToString();

            FormControl.FormRsvs_Show(date1, code1, name1, code2, name2);
        }

		private void PastBox_CheckStateChanged(object sender, EventArgs e)
		{
			this.ListFormat();
		}
    }
}
