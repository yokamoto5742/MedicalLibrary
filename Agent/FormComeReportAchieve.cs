using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;
using MedicalLibrary.Agent;

namespace MedicalLibrary.Agent
{
    public partial class FormComeReportAchieve : Form
    {
        DataSet DSet = new DataSet();

        public FormComeReportAchieve()
        {
            InitializeComponent();
        }

        private void FormComeReportAchieve_Load(object sender, EventArgs e)
        {
            for (int i = 2008; i <= DateTime.Now.Year; i++)
            {
                YearBox.Items.Add(i);
            }

            YearBox.Text = DateTime.Now.Year.ToString();

            for (int i = 1; i <= 12; i++)
            {
                MonthBox.Items.Add(i);
            }

            MonthBox.Text = DateTime.Now.Month.ToString();

            DataTable tmpTable = DSet.Tables.Add("Achieve");
            tmpTable.Columns.Add("STAFF");
            tmpTable.Columns.Add("à„ét");
            tmpTable.Columns.Add("SEKOU");
            tmpTable.Columns.Add("éÌï ");
            tmpTable.Columns.Add("åèêî");

            tmpTable = DSet.Tables.Add("Achieve2");
            tmpTable.Columns.Add("à„ét");
            tmpTable.Columns.Add("éÌï ");
            tmpTable.Columns.Add("ì‡óe");
            tmpTable.Columns.Add("åèêî");

            this.ShowList();
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            this.ShowList();
        }

        private void ShowList()
        {
            if (YearBox.Text.Length == 0 || MonthBox.Text.Length == 0)
            {
                return;
            }

            string startDate = YearBox.Text + MonthBox.Text.PadLeft(2, '0') + "01";
            string endDate = YearBox.Text + MonthBox.Text.PadLeft(2, '0') + "99";

            List<ReportAchieve> list = ReportAchieve.GetList1(startDate, endDate, ComeReportSettings.Current.AchieveKensaList.ToArray());

            list.Sort((x, y) =>
            {
                int i = x.StaffCode.CompareTo(y.StaffCode);

                if (i == 0)
                {
                    i = x.SekouCode.CompareTo(y.SekouCode);
                }

                return i;
            });

            DataTable tmpTable = DSet.Tables["Achieve"];
            tmpTable.Clear();

            foreach (ReportAchieve obj in list)
            {
                // à„étÇÃèÍçáÇÃÇ›
                if (obj.QualCode.Equals(1))
                {
                    DataRow r = tmpTable.NewRow();

                    r["STAFF"] = obj.StaffCode;
                    r["à„ét"] = obj.StaffName;
                    r["SEKOU"] = obj.SekouCode;
                    r["éÌï "] = obj.SekouName;
                    r["åèêî"] = obj.Count;

                    tmpTable.Rows.Add(r);
                }
            }

            GridView.DataSource = new DataView(tmpTable);

            GridView.Columns["STAFF"].Visible = false;
            GridView.Columns["à„ét"].Width = 90;
            GridView.Columns["SEKOU"].Visible = false;
            GridView.Columns["éÌï "].Width = 80;
            GridView.Columns["åèêî"].Width = 70;
            GridView.Columns["åèêî"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void ShowList2()
        {
            if (YearBox.Text.Length == 0 || MonthBox.Text.Length == 0)
            {
                return;
            }

            if (this.GridView.SelectedRows.Count == 0)
            {
                return;
            }

            string staff_code = this.GridView.SelectedRows[0].Cells["STAFF"].Value.ToString();
            string staff_name = this.GridView.SelectedRows[0].Cells["à„ét"].Value.ToString();

            string[] sekous = new string[1];
            sekous[0] = this.GridView.SelectedRows[0].Cells["SEKOU"].Value.ToString();

            string startDate = YearBox.Text + MonthBox.Text.PadLeft(2, '0') + "01";
            string endDate = YearBox.Text + MonthBox.Text.PadLeft(2, '0') + "99";

            List<ReportAchieve> list = ReportAchieve.GetList2(startDate, endDate, sekous, staff_code);

            list.Sort((x, y) =>
            {
                int i = x.SekouCode.CompareTo(y.SekouCode);

                if (i == 0)
                {
                    i = x.Tab.CompareTo(y.Tab);
                }

                return i;
            });

            DataTable tmpTable = DSet.Tables["Achieve2"];
            tmpTable.Clear();

            foreach (ReportAchieve obj in list)
            {
                DataRow r = tmpTable.NewRow();

                r["à„ét"] = staff_name;
                r["éÌï "] = obj.SekouName;
                r["ì‡óe"] = obj.Tab;
                r["åèêî"] = obj.Count;

                tmpTable.Rows.Add(r);
            }

            GridView2.DataSource = new DataView(tmpTable);

            GridView2.Columns["à„ét"].Width = 80;
            GridView2.Columns["éÌï "].Width = 40;
            GridView2.Columns["ì‡óe"].Width = 140;
            GridView2.Columns["åèêî"].Width = 40;
            GridView2.Columns["åèêî"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void GridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.ShowList2();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}