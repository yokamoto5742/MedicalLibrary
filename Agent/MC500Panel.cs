using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class MC500Panel : UserControl
    {
        MC500Panels ParentPanel;

        MC500 Mc = new MC500();

        public MC500Panel(MC500Panels p_panel)
        {
            InitializeComponent();

            EyeBox.Items.Add("");
            EyeBox.Items.Add("右");
            EyeBox.Items.Add("左");
            EyeBox.Items.Add("両");

            ParentPanel = p_panel;
        }

        void DataClear()
        {
            LaserDateBox.Clear();
            EyeBox.Text = "";
            StaffBox.Clear();
        }

        public void DataShow(MC500 mc)
        {
            DataClear();

            Mc = mc;

            LaserDateBox.Text = DateTimeAgent.DateFormat(Mc.LaserDate, DateTimeAgent.DateFormatKind.LONG);
            EyeBox.Text = Mc.EyeJ;

            if (Dict.StaffDict.ContainsKey(Mc.Staff))
            {
                StaffBox.Text = Dict.StaffDict[Mc.Staff].Name;
            }

            ListView.Columns.Add("LaserShot", "LaserShot");
            ListView.Columns.Add("SpotSize", "SpotSize");
            ListView.Columns.Add("Time", "Time");
            ListView.Columns.Add("Power", "Power");
            ListView.Columns.Add("ShotNo", "ShotNo");
            ListView.Columns.Add("SP", "SP");

            for (int i = 0; i < 4; i++)
            {
                MC500Element m = Mc.DataList[i];

                ListView.Rows.Add(m.LaserShot, m.SpotSizeAve, m.TimeAve, m.PowerAve, m.ShotNo);

                if (i == 3)
                {
                    DataGridViewComboBoxCell cell1 = new DataGridViewComboBoxCell();
                    cell1.Items.Add("");
                    cell1.Items.Add("Total");
                    cell1.Items.Add("Green-Red");
                    cell1.Items.Add("Green-Yellow");
                    cell1.Items.Add("Red-Yellow");
                    cell1.Value = m.LaserShot;
                    cell1.AutoComplete = true;
                    cell1.ReadOnly = false;

                    ListView[0, i] = cell1;
                }

                DataGridViewComboBoxCell cell2 = new DataGridViewComboBoxCell();
                cell2.Items.Add("");
                cell2.Items.Add("シングル");
                cell2.Items.Add("0.5");
                cell2.Items.Add("0.75");
                cell2.Items.Add("1.0");
                cell2.Value = m.SP;
                cell2.AutoComplete = true;
                cell2.ReadOnly = false;

                ListView[5, i] = cell2;
            }

            ListFormat();

            ContBox.Text = Mc.Cont;
        }

        void ListFormat()
        {
            ListView.Columns["LaserShot"].HeaderText = "設定波長";
            ListView.Columns["LaserShot"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView.Columns["LaserShot"].Width = 100;

            ListView.Columns["SpotSize"].HeaderText = "スポットサイズ";
            ListView.Columns["SpotSize"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView.Columns["SpotSize"].Width = 90;

            ListView.Columns["Time"].HeaderText = "設定時間";
            ListView.Columns["Time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView.Columns["Time"].Width = 80;

            ListView.Columns["Power"].HeaderText = "凝固出力";
            ListView.Columns["Power"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView.Columns["Power"].Width = 80;

            ListView.Columns["ShotNo"].HeaderText = "ショット数";
            ListView.Columns["ShotNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView.Columns["ShotNo"].Width = 90;

            ListView.Columns["SP"].HeaderText = "SP";
            ListView.Columns["SP"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView.Columns["SP"].Width = 85;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                StaffBox.Text = LoginUser.Name;

                MessageBox.Show("登録しました");
            }
        }

        bool Save()
        {
            if (EyeBox.Text.Length == 0)
            {
                MessageBox.Show("術眼が選択されていません");
                return false;
            }

            for (int i = 0; i < 4; i++)
            {
                DataGridViewRow r = ListView.Rows[i];

                if (r.Cells["LaserShot"].Value != null)
                {
                    Mc.DataList[i].LaserShot = r.Cells["LaserShot"].Value.ToString();
                }
                else
                {
                    Mc.DataList[i].LaserShot = "";
                }

                if (r.Cells["SpotSize"].Value != null)
                {
                    Mc.DataList[i].SpotSizeAve = r.Cells["SpotSize"].Value.ToString();
                }
                else
                {
                    Mc.DataList[i].SpotSizeAve = "";
                }

                if (r.Cells["Time"].Value != null)
                {
                    Mc.DataList[i].TimeAve = r.Cells["Time"].Value.ToString();
                }
                else
                {
                    Mc.DataList[i].TimeAve = "";
                }

                if (r.Cells["Power"].Value != null)
                {
                    Mc.DataList[i].PowerAve = r.Cells["Power"].Value.ToString();
                }
                else
                {
                    Mc.DataList[i].PowerAve = "";
                }

                if (r.Cells["ShotNo"].Value != null)
                {
                    Mc.DataList[i].ShotNo = r.Cells["ShotNo"].Value.ToString();
                }
                else
                {
                    Mc.DataList[i].ShotNo = "";
                }

                if (r.Cells["SP"].Value != null)
                {
                    Mc.DataList[i].SP = r.Cells["SP"].Value.ToString();
                }
                else
                {
                    Mc.DataList[i].SP = "";
                }
            }

            switch (EyeBox.Text)
            {
                case "右":
                    Mc.Eye = "R";
                    break;
                case "左":
                    Mc.Eye = "L";
                    break;
                case "両":
                    Mc.Eye = "B";
                    break;
                default:
                    Mc.Eye = "";
                    break;
            }

            Mc.Cont = ContBox.Text;

            return !Mc.Save(LoginUser.Id).ErrExist;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("削除します。よろしいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (Mc.Delete())
                {
                    MessageBox.Show("削除しました");
                    ParentPanel.PanelsShow(Mc.PtId);
                }
            }
        }

        private void ListMenuItem1_Click(object sender, EventArgs e)
        {
            switch (EyeBox.Text)
            {
                case "右":
                    Mc.Eye = "R";
                    break;
                case "左":
                    Mc.Eye = "L";
                    break;
                case "両":
                    Mc.Eye = "B";
                    break;
                default:
                    Mc.Eye = "";
                    break;
            }

            string str = Mc.Eye + ",";

            DataGridViewRow r = ListView.CurrentRow;

            str += r.Cells["LaserShot"].Value.ToString() + ",";
            str += r.Cells["SpotSize"].Value.ToString() + ",";
            str += r.Cells["Time"].Value.ToString() + ",";
            str += r.Cells["Power"].Value.ToString() + ",";
            str += r.Cells["ShotNo"].Value.ToString() + ",";
            str += r.Cells["SP"].Value.ToString() + "\r\n";

            str += ContBox.Text;

            Clipboard.SetText(str);
        }
    }
}
