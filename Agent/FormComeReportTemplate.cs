using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class FormComeReportTemplate : Form
    {
        FormComeReportPat Fp;

        DataSet DSet = new DataSet();

        public FormComeReportTemplate()
        {
            InitializeComponent();
        }

        public FormComeReportTemplate(FormComeReportPat F1)
        {
            InitializeComponent();

            this.Fp = F1;
        }

        private void FormComeReportTemplate_Load(object sender, EventArgs e)
        {
            // 所見テンプレートの初期設定
            DataTable tmpTable = DSet.Tables.Add("Report");
            tmpTable.Columns.Add("KensaCode");
            tmpTable.Columns.Add("Kind");
            tmpTable.Columns.Add("Name");
            tmpTable.Columns.Add("Text");
            tmpTable.Columns.Add("X");
            tmpTable.Columns.Add("Y");
            tmpTable.Columns.Add("Width");
            tmpTable.Columns.Add("Height");
            tmpTable.Columns.Add("AutoSize");
            tmpTable.Columns.Add("Visible");
            tmpTable.Columns.Add("Option1");
            tmpTable.Columns.Add("Option2");
            tmpTable.Columns.Add("Option3");
            tmpTable.Columns.Add("Option4");
            tmpTable.Columns.Add("Option5");
            tmpTable.Columns.Add("印字");
            tmpTable.Columns.Add("トリガー");
            tmpTable.Columns.Add("PadType");
            tmpTable.Columns.Add("PadLength");
            tmpTable.Columns.Add("前文字");
            tmpTable.Columns.Add("後文字");
            tmpTable.Columns.Add("前改行");
            tmpTable.Columns.Add("後改行");

            this.Init();
        }

        private void Init()
        {
            // 所見の内容作成
            DataTable tmpTable = DSet.Tables["Report"];
            tmpTable.Clear();

            string tmp_file = "";

            if (LoginUser.QualId.Equals("1"))
            {
                tmp_file = AppFile.FilePath("ComeReportRec1.csv");
            }
            else
            {
                tmp_file = AppFile.FilePath("ComeReportRec2.csv");
            }

            System.IO.StreamReader reader = new System.IO.StreamReader(tmp_file, Encoding.Default);

            string line;

            KensaBox.Items.Clear();
            KensaBox.Items.Add("");

            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("'"))
                {
                    continue;
                }

                if (line.Contains(","))
                {
                    tmpTable.Rows.Add(line.Split(','));

                    if (ComeReportSettings.Current.KensaDict.ContainsKey(line.Split(',')[0]))
                    {
                        if (!KensaBox.Items.Contains(line.Split(',')[0] + " " + ComeReportSettings.Current.KensaDict[line.Split(',')[0]].Name))
                        {
                            KensaBox.Items.Add(line.Split(',')[0] + " " + ComeReportSettings.Current.KensaDict[line.Split(',')[0]].Name);
                        }
                    }
                }
            }

            reader.Close();
        }

        private void MakeReportTable()
        {
            if (KensaBox.Text.Contains(" "))
            {
                DataTable tmpTable = DSet.Tables["Report"];

                this.ReportPanel.Controls.Clear();

                // 所見のテーブル作成
                foreach (DataRow tmpRow in tmpTable.Select("KensaCode like '" + KensaBox.Text.Split(' ')[0] + "'"))
                {
                    string[] s = new string[22];

                    for (int i = 0; i < s.Length; i++)
                    {
                        s[i] = tmpRow.ItemArray[i + 1].ToString();
                    }

                    if (s[0] == "CheckBox")
                    {
                        CheckBox tmpBox = new CheckBox();
                        tmpBox.Name = s[1];
                        tmpBox.Text = s[2];

                        if (s[3].Length > 0 && s[4].Length > 0)
                        {
                            tmpBox.Location = new Point(int.Parse(s[3]), int.Parse(s[4]));
                        }

                        if (s[5].Length > 0 || s[6].Length > 0)
                        {
                            if (s[5].Length > 0)
                            {
                                tmpBox.Width = int.Parse(s[5]);
                            }

                            if (s[6].Length > 0)
                            {
                                tmpBox.Height = int.Parse(s[6]);
                            }
                        }

                        if (s[7] == "1")
                        {
                            tmpBox.AutoSize = true;
                        }
                        else
                        {
                            tmpBox.AutoSize = false;
                        }

                        if (s[8] == "1")
                        {
                            tmpBox.Visible = true;
                        }
                        else
                        {
                            tmpBox.Visible = false;
                        }

                        if (s[9].Length > 0)
                        {
                            if (s[9].Contains("Button"))
                            {
                                tmpBox.Appearance = Appearance.Button;
                            }
                            else
                            {
                                tmpBox.Appearance = Appearance.Normal;
                            }
                        }

                        if (s[10].Length > 0)
                        {
                            if (s[10].Contains("Flat"))
                            {
                                tmpBox.FlatStyle = FlatStyle.Flat;
                            }
                        }

                        if (s[11].Length > 0)
                        {
                            if (s[11].Contains("Checked"))
                            {
                                tmpBox.Checked = true;
                            }
                            else
                            {
                                tmpBox.Checked = false;
                            }
                        }

                        tmpBox.Tag = "";

                        for (int i = 14; i < s.Length; i++)
                        {
                            tmpBox.Tag += s[i] + ",";
                        }

                        tmpBox.KeyDown += new KeyEventHandler(Control_KeyDown);

                        this.ReportPanel.Controls.Add(tmpBox);
                    }
                    else if (s[0] == "TextBox")
                    {
                        TextBox tmpBox = new TextBox();
                        tmpBox.Name = s[1];
                        tmpBox.Text = s[2];

                        if (s[3].Length > 0 && s[4].Length > 0)
                        {
                            tmpBox.Location = new Point(int.Parse(s[3]), int.Parse(s[4]));
                        }

                        if (s[5].Length > 0)
                        {
                            tmpBox.Width = int.Parse(s[5]);
                        }

                        if (s[6].Length > 0)
                        {
                            tmpBox.Height = int.Parse(s[6]);
                        }

                        if (s[8] == "1")
                        {
                            tmpBox.Visible = true;
                        }
                        else
                        {
                            tmpBox.Visible = false;
                        }

                        if (s[9].Length > 0)
                        {
                            if (s[9].Contains("Center"))
                            {
                                tmpBox.TextAlign = HorizontalAlignment.Center;
                            }
                            else if (s[9].Contains("Right"))
                            {
                                tmpBox.TextAlign = HorizontalAlignment.Right;
                            }
                        }

                        if (s[10].Length > 0)
                        {
                            if (s[10].Contains("Hiragana"))
                            {
                                tmpBox.ImeMode = ImeMode.Hiragana;
                            }
                            else if (s[10].Contains("Disable"))
                            {
                                tmpBox.ImeMode = ImeMode.Disable;
                            }
                            else
                            {
                                tmpBox.ImeMode = ImeMode.Off;
                            }
                        }

                        if (s[11].Length > 0)
                        {
                            tmpBox.MaxLength = int.Parse(s[11].Trim());
                        }

                        if (s[12].Equals("1"))
                        {
                            tmpBox.Multiline = true;
                        }
                        else
                        {
                            tmpBox.Multiline = false;
                            tmpBox.KeyDown += new KeyEventHandler(Control_KeyDown);
                        }

                        tmpBox.Tag = "";

                        for (int i = 14; i < s.Length; i++)
                        {
                            tmpBox.Tag += s[i] + ",";
                        }

                        this.ReportPanel.Controls.Add(tmpBox);
                    }
                    else if (s[0] == "ComboBox")
                    {
                        // ComboBox が既存かどうか
                        bool exist_flg = false;

                        ComboBox tmpBox = new ComboBox();
                        tmpBox.Name = s[1];

                        for (int i = 0; i < this.ReportPanel.Controls.Count; i++)
                        {
                            Control c = this.ReportPanel.Controls[i];

                            if (c.GetType().Name.Equals("ComboBox") && c.Name.Equals(tmpBox.Name))
                            {
                                tmpBox = (ComboBox)c;
                                exist_flg = true;
                                break;
                            }
                        }

                        tmpBox.Items.Add(s[2]);

                        if (s[3].Length > 0 && s[4].Length > 0)
                        {
                            tmpBox.Location = new Point(int.Parse(s[3]), int.Parse(s[4]));
                        }

                        if (s[5].Length > 0)
                        {
                            tmpBox.Width = int.Parse(s[5]);
                        }

                        if (s[6].Length > 0)
                        {
                            tmpBox.Height = int.Parse(s[6]);
                        }

                        if (s[8] == "1")
                        {
                            tmpBox.Visible = true;
                        }
                        else
                        {
                            tmpBox.Visible = false;
                        }

                        if (s[9].Length > 0)
                        {
                            if (s[9].Contains("DropDownList"))
                            {
                                tmpBox.DropDownStyle = ComboBoxStyle.DropDownList;
                            }
                        }

                        if (s[10].Length > 0)
                        {
                            if (s[10].Contains("Hiragana"))
                            {
                                tmpBox.ImeMode = ImeMode.Hiragana;
                            }
                            else if (s[10].Contains("Disable"))
                            {
                                tmpBox.ImeMode = ImeMode.Disable;
                            }
                            else
                            {
                                tmpBox.ImeMode = ImeMode.Off;
                            }
                        }

                        tmpBox.Tag = "";

                        for (int i = 14; i < s.Length; i++)
                        {
                            tmpBox.Tag += s[i] + ",";
                        }

                        if (!exist_flg)
                        {
                            this.ReportPanel.Controls.Add(tmpBox);
                        }
                    }
                    else if (s[0] == "Label")
                    {
                        Label tmpLabel = new Label();
                        tmpLabel.Name = s[1];
                        tmpLabel.Text = s[2];

                        if (s[3].Length > 0 && s[4].Length > 0)
                        {
                            tmpLabel.Location = new Point(int.Parse(s[3]), int.Parse(s[4]));
                        }

                        if (s[5].Length > 0)
                        {
                            tmpLabel.Width = int.Parse(s[5]);
                        }

                        if (s[6].Length > 0)
                        {
                            tmpLabel.Height = int.Parse(s[6]);
                        }

                        if (s[7] == "1")
                        {
                            tmpLabel.AutoSize = true;
                        }
                        else
                        {
                            tmpLabel.AutoSize = false;
                        }

                        if (s[8] == "1")
                        {
                            tmpLabel.Visible = true;
                        }
                        else
                        {
                            tmpLabel.Visible = false;
                        }

                        tmpLabel.Tag = "";

                        for (int i = 14; i < s.Length; i++)
                        {
                            tmpLabel.Tag += s[i] + ",";
                        }

                        this.ReportPanel.Controls.Add(tmpLabel);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ProcessTabKey(true);
            }
        }

        private void MakeReportText()
        {
            if (Fp == null)
            {
                return;
            }

            string tmp_rec = "";

            foreach (Control c in ReportPanel.Controls)
            {
                bool write_flg = true;
                string padType = "";
                int padLength = 0;
                string fix1 = "";
                string fix2 = "";
                string lf1 = "";
                string lf2 = "";

                // タグのチェック
                if (c.Tag != null && c.Tag.ToString().Length > 0)
                {
                    string[] s = c.Tag.ToString().Trim('\n').Trim('\r').Split(',');

                    // Pad
                    padType = s[2];

                    if (s[3].Length > 0 && int.TryParse(s[3], out padLength))
                    {
                        padLength = int.Parse(s[3]);
                    }
                    else
                    {
                        padLength = 0;
                    }

                    // 前後文字
                    fix1 = s[4];
                    fix2 = s[5];

                    // 前改行
                    if (s[6].Length > 0)
                    {
                        for (int i = 0; i < int.Parse(s[6]); i++)
                        {
                            lf1 += "\r\n";
                        }
                    }

                    // 後改行
                    if (s[7].Length > 0)
                    {
                        for (int i = 0; i < int.Parse(s[7]); i++)
                        {
                            lf2 += "\r\n";
                        }
                    }

                    if (s[0] == "1")
                    {
                        write_flg = true;
                    }
                    else if (s[0] == "2" || s[0] == "4")
                    {
                        if (c.GetType().Name == "CheckBox")
                        {
                            CheckBox tmpBox = (CheckBox)c;

                            if (tmpBox.Checked)
                            {
                                write_flg = true;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else if (c.GetType().Name == "TextBox")
                        {
                            TextBox tmpBox = (TextBox)c;

                            if (tmpBox.Text.Length > 0)
                            {
                                write_flg = true;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else if (c.GetType().Name == "ComboBox")
                        {
                            ComboBox tmpBox = (ComboBox)c;

                            if (tmpBox.Text.Length > 0)
                            {
                                write_flg = true;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else if (c.GetType().Name == "Label")
                        {
                            Label tmpLabel = (Label)c;

                            if (tmpLabel.Text.Length > 0)
                            {
                                write_flg = true;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    else if (s[0] == "3")
                    {
                        continue;
                    }

                    if (s[0] == "4")
                    {
                        if (s[1].Length > 0 && ReportPanel.Controls.ContainsKey(s[1].Trim()))
                        {
                            Control trigCtrl = ReportPanel.Controls[s[1].Trim()];

                            if (trigCtrl != null)
                            {
                                if (trigCtrl.GetType().Name == "CheckBox")
                                {
                                    CheckBox trigBox = (CheckBox)trigCtrl;

                                    if (!trigBox.Checked)
                                    {
                                        write_flg = false;
                                    }
                                }
                                else if (trigCtrl.GetType().Name == "TextBox")
                                {
                                    TextBox trigBox = (TextBox)trigCtrl;

                                    if (trigBox.Text.Length == 0)
                                    {
                                        write_flg = false;
                                    }
                                }
                                else if (trigCtrl.GetType().Name == "ComboBox")
                                {
                                    ComboBox trigBox = (ComboBox)trigCtrl;

                                    if (trigBox.Text.Length == 0)
                                    {
                                        write_flg = false;
                                    }
                                }
                            }
                        }
                    }
                }

                if (write_flg)
                {
                    string printText = c.Text;

                    // Pad を入れる処理
                    int len = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(printText);

                    if (padType.Equals("L") && len < padLength)
                    {
                        for (; len < padLength; len++)
                        {
                            printText = " " + printText;
                        }
                    }
                    else if (padType.Equals("R") && len < padLength)
                    {
                        for (; len < padLength; len++)
                        {
                            printText += " ";
                        }
                    }

                    tmp_rec += lf1 + fix1 + printText + fix2 + lf2;
                }
            }

            // FormPatの所見欄に入力する
            if (tmp_rec.Length > 0)
            {
                TabPage tmpPage = Fp.ReportTabControl.SelectedTab;

                TextBox tmpBox = (TextBox)(tmpPage.Controls["ContBox"]);

                if (tmpBox.Text.Length > 0)
                {
                    if (MessageBox.Show("既に所見が記載されています。上書きしますか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        tmpBox.Text = tmp_rec;
                    }
                }
                else
                {
                    tmpBox.Text = tmp_rec;
                }
            }
        }

        private void KensaBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.MakeReportTable();
        }

        private void MakeReportButton_Click(object sender, EventArgs e)
        {
            this.MakeReportText();
            Fp.Activate();
        }

        private void FileExitMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}