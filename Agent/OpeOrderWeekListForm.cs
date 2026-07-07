using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class OpeOrderWeekListForm : Form
    {
        OpeOrderSettings Settings = new OpeOrderSettings();

        DataSet DSet = new DataSet();

        int crit_date
        {
            get
            {
                return int.Parse(CritDate.Value.ToString("yyyyMMdd"));
            }
        }

        public OpeOrderWeekListForm()
        {
            InitializeComponent();
        }

        private void OpeOrderWeekListForm_Load(object sender, EventArgs e)
        {
            // プログラムで一度も実行されていなければ実行する
            LibSettings.Init();

            this.Settings.Init();

            this.Location = new Point(this.Location.X, 0);
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            DataTable table = DSet.Tables.Add("OpeList");

            table.Columns.Add("館");
            table.Columns.Add("室");
            table.Columns.Add("0");
            table.Columns.Add("1");
            table.Columns.Add("2");
            table.Columns.Add("3");
            table.Columns.Add("4");
            table.Columns.Add("5");
            table.Columns.Add("6");

            ListShow();

            PlaceFilterBox.Items.Add("");
            PlaceFilterBox.Items.Add("中央");
            PlaceFilterBox.Items.Add("南館");

            if (this.Settings.PCPlace == OpeOrderSettings.Place.Hon)
            {
                PlaceFilterBox.Text = "中央";
            }
            else if (this.Settings.PCPlace == OpeOrderSettings.Place.Minami)
            {
                PlaceFilterBox.Text = "南館";
            }
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            this.ListShow();
        }

        void ListShow()
        {
            DataTable table = DSet.Tables["OpeList"];
            table.Clear();

            List<PatOpeOrder2> list = PatOpeOrder2.Load(int.Parse(CritDate.Value.ToString("yyyyMMdd")), int.Parse(CritDate.Value.AddDays(6).ToString("yyyyMMdd")));

            int days = 0;

            // 手術室が決まっているもの
            string[] opes = new string[7];

            foreach (string k in PatOpeOrder2.OpeRoomDict.Keys)
            {
                DataRow r = table.NewRow();

                if (k.Equals("11") || k.Equals("12") || k.Equals("13"))
                {
                    r["館"] = "南館";
                }
                else
                {
                    r["館"] = "中央";
                }

                r["室"] = PatOpeOrder2.OpeRoomDict[k];

                opes = new string[] { "", "", "", "", "", "", "" };

                foreach (PatOpeOrder2 p in list)
                {
                    if (p.IsCanceled)
                    {
                        continue;
                    }

                    days = DateTimeAgent.IntervalDays(crit_date, p.Date);

                    if (days < 0 || days > 6)
                    {
                        continue;
                    }

                    if (p.OpeRoom.Equals(k))
                    {
                        /*
                        if (opes[days].Length > 0)
                        {
                            opes[days] += "\r\n\r\n";
                        }
                         */

                        opes[days] += p.StartEndTimeString + " " + p.TimeOutMarkShort + p.InOutMark + p.TimeFreeMark + p.DeptName +
                            "(" + p.Pat.Name + "様 ID " + p.Pat.Id + ")" + p.OpeName + "(" + p.OpeDoctor + ")" +
                            p.OpeAnes + p.Doctor3WithBrackets + p.StaffNameWithBrackets + "\r\n\r\n";
                    }
                }

                for (int i = 0; i < 7; i++)
                {
                    r[i.ToString()] = opes[i];
                }

                table.Rows.Add(r);
            }

            // 手術室未定のもの
            string[] opes1 = new string[] { "", "", "", "", "", "", "" };
            string[] opes2 = new string[] { "", "", "", "", "", "", "" };

            DataRow r1 = table.NewRow();
            r1["館"] = "中央";
            r1["室"] = "未定";

            DataRow r2 = table.NewRow();
            r2["館"] = "南館";
            r2["室"] = "未定";

            foreach (PatOpeOrder2 p in list)
            {
                if (p.IsCanceled)
                {
                    continue;
                }

                if (p.OpeRoom.Equals("0") || p.OpeRoom.Length == 0)
                {
                    days = DateTimeAgent.IntervalDays(crit_date, p.Date);

                    if (days < 0 || days > 6)
                    {
                        continue;
                    }

                    if (p.OpePlace.Equals("南館"))
                    {
                        opes2[days] += p.StartEndTimeString + " " + p.TimeOutMarkShort + p.InOutMark + p.TimeFreeMark + p.DeptName +
                            "(" + p.Pat.Name + "様 ID " + p.Pat.Id + ")" + p.OpeName + "(" + p.OpeDoctor + ")" +
                            p.OpeAnes + p.Doctor3WithBrackets + p.StaffNameWithBrackets + "\r\n\r\n";
                    }
                    else
                    {
                        opes1[days] += p.StartEndTimeString + " " + p.TimeOutMarkShort + p.InOutMark + p.TimeFreeMark + p.DeptName +
                            "(" + p.Pat.Name + "様 ID " + p.Pat.Id + ")" + p.OpeName + "(" + p.OpeDoctor + ")" +
                            p.OpeAnes + p.Doctor3WithBrackets + p.StaffNameWithBrackets + "\r\n\r\n";
                    }
                }
            }

            for (int i = 0; i < 7; i++)
            {
                r1[i.ToString()] = opes1[i];
                r2[i.ToString()] = opes2[i];
            }

            table.Rows.Add(r1);
            table.Rows.Add(r2);

            // 南館３・全身麻酔を本館に表示する
            string[] anes1 = new string[] { "", "", "", "", "", "", "" };

            DataRow ra1 = table.NewRow();
            ra1["館"] = "中央";
            ra1["室"] = "南全麻";

            foreach (PatOpeOrder2 p in list)
            {
                if (p.IsCanceled)
                {
                    continue;
                }

                if (p.OpePlace.Equals("南館") && (p.OpeAnes.Contains("全麻") || p.OpeAnes.Contains("全身")))
                {
                    days = DateTimeAgent.IntervalDays(crit_date, p.Date);

                    if (days < 0 || days > 6)
                    {
                        continue;
                    }

                    anes1[days] += p.StartEndTimeString + " " + p.TimeOutMarkShort + p.InOutMark + p.TimeFreeMark + p.DeptName +
                        "(" + p.Pat.Name + "様 ID " + p.Pat.Id + ")" + p.OpeName + "(" + p.OpeDoctor + ")" +
                        p.OpeAnes + p.Doctor3WithBrackets + p.StaffNameWithBrackets + "\r\n\r\n";
                }
            }

            for (int i = 0; i < 7; i++)
            {
                ra1[i.ToString()] = anes1[i];
            }

            table.Rows.Add(ra1);

            // 麻酔科予約を本館に表示する
            string[] anes2 = new string[] { "", "", "", "", "", "", "" };

            DataRow ra2 = table.NewRow();
            ra2["館"] = "本館";
            ra2["室"] = "麻酔科";

            // 麻酔科 濱田DR 予約コード 1144
            List<RsvData> rsv_list = RsvData.GetListByDates("1144", "", int.Parse(CritDate.Value.ToString("yyyyMMdd")), int.Parse(CritDate.Value.AddDays(6).ToString("yyyyMMdd")));

            foreach (RsvData p in rsv_list)
            {
                days = DateTimeAgent.IntervalDays(crit_date, p.RsvDate);

                if (days < 0 || days > 6)
                {
                    continue;
                }

                anes2[days] += p.TimeString("-") + " " + p.Cont1 + "(" + p.Pat.Name + "様 ID " + p.Pat.Id + ")" + p.Cont2;

                if (p.StaffName.Length > 0)
                {
                    anes2[days] += " (" + p.StaffName + ")";
                }

                anes2[days] += "\r\n\r\n";
            }

            for (int i = 0; i < 7; i++)
            {
                ra2[i.ToString()] = anes2[i];
            }

            table.Rows.Add(ra2);

            ListFormat();
        }

        void ListFormat()
        {
            DataView view = new DataView(DSet.Tables["OpeList"]);

            string filter = "";

            if (PlaceFilterBox.Text.Length > 0)
            {
                if (PlaceFilterBox.Text.Equals("中央"))
                {
                    // 「中央」のほか「南館３・全身麻酔」と「麻酔科診察」も表示する。
                    filter = "館 = '中央' or 室 = '麻酔科' or 室 = '南3全麻'";
                }
                else
                {
                    filter = "館 = '" + PlaceFilterBox.Text + "'";
                }
            }

            view.RowFilter = filter;

            OpeListView.DataSource = view;

            OpeListView.Columns["館"].Width = 35;

            OpeListView.Columns["室"].Width = 35;

            for (int i = 0; i < 7; i++)
            {
                OpeListView.Columns[i.ToString()].HeaderText = CritDate.Value.AddDays(i).ToString("M/d(ddd)");
                OpeListView.Columns[i.ToString()].Width = 130;
            }
        }

        private void PlaceFilterBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListFormat();
        }

        private void ExcelPlanButton_Click(object sender, EventArgs e)
        {
            string file = AppFile.FilePath(Settings.ExcelWeek);

            if (!File.Exists(file))
            {
                MessageBox.Show(Settings.ExcelWeek + " が存在しません");
                return;
            }

            OpeOrderExcelWeekPlan.ExcelOpen(file, CritDate.Value.ToString("yyyyMMdd_HHmmss"), OpeListView);
        }
    }
}
