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
    public partial class FormOpeNursingAs : Form
    {
        FormOpeNursingPat f1;

        DataSet mSet;
        DataSet aSet;
        DataSet rSet;

        Dictionary<string, string> asTitleDict;

        public FormOpeNursingAs()
        {
            InitializeComponent();
        }

        public FormOpeNursingAs(FormOpeNursingPat F1)
        {
            InitializeComponent();

            this.f1 = F1;
        }

        private void FormOpeNursingAs_Load(object sender, EventArgs e)
        {
            try
            {
                this.mSet = f1.mSet;
                this.asTitleDict = f1.asTitleDict;

                // ê›íËÉtÉ@ÉCÉã
                string file1 = AppFile.FilePath("OpeNursingAs.xml");

                aSet = new DataSet();
                aSet.ReadXml(new System.IO.StreamReader(file1, Encoding.GetEncoding("shift-jis")));

                rSet = new DataSet();

                // éËèpãLò^ÇÃì‡óeçÏê¨
                DataTable tmpTable = rSet.Tables.Add("RecordItem");
                tmpTable.Columns.Add("RecordId");
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
                tmpTable.Columns.Add("àÛéö");
                tmpTable.Columns.Add("ÉgÉäÉKÅ[");
                tmpTable.Columns.Add("ëOï∂éö");
                tmpTable.Columns.Add("å„ï∂éö");
                tmpTable.Columns.Add("ëOâ¸çs");
                tmpTable.Columns.Add("å„â¸çs");

                this.init();

//                this.OriginalText = "ä≈åÏêfífÅEãLò^çÏê¨";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Dispose();
            }
        }

        void init()
        {
            // ä≈åÏêfífÇÃéÌï çÏê¨
            DataTable tmpTable = mSet.Tables["AsTitle"];
            asTitleBox.Items.Clear();
            asTitleBox.Items.Add("");

            foreach (DataRow tmpRow in tmpTable.Rows)
            {
                if (!asTitleBox.Items.Contains(tmpRow.ItemArray[0].ToString() + " " + tmpRow.ItemArray[1].ToString()))
                {
                    asTitleBox.Items.Add(tmpRow.ItemArray[0].ToString() + " " + tmpRow.ItemArray[1].ToString());
                }
            }

            // éËèpãLò^ÇÃéÌï çÏê¨
            tmpTable = mSet.Tables["Record"];
            recTitleBox.Items.Clear();
            recTitleBox.Items.Add("");

            foreach (DataRow tmpRow in tmpTable.Rows)
            {
                if (!recTitleBox.Items.Contains(tmpRow.ItemArray[0].ToString().Trim() + " " + tmpRow.ItemArray[1].ToString().Trim()))
                {
                    recTitleBox.Items.Add(tmpRow.ItemArray[0].ToString().Trim() + " " + tmpRow.ItemArray[1].ToString().Trim());
                }
            }

            this.initRec();
        }

        void initRec()
        {
            DataTable tmpTable = rSet.Tables["RecordItem"];
            tmpTable.Clear();

            string file1 = AppFile.FilePath("OpeNursingRec.csv");
            System.IO.StreamReader reader = new System.IO.StreamReader(file1, Encoding.Default);

            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("'"))
                {
                    continue;
                }

                tmpTable.Rows.Add(line.Split(','));
            }

            reader.Close();
        }

        private void makeAsTabs()
        {
            if (asTitleBox.Text.Length > 0)
            {
                DataTable tmpTable = aSet.Tables["Assessment"];

                this.tabControl1.TabPages.Clear();

                // ä≈åÏêfífÇÃÉ^ÉuçÏê¨
                foreach (DataRow tmpRow in tmpTable.Rows)
                {
                    if (tmpRow.ItemArray[0].ToString().Trim().Equals(asTitleBox.Text.Split(' ')[0]))
                    {
                        string tmpText = tmpRow.ItemArray[1].ToString().Trim();
                        string tmpItem = tmpRow.ItemArray[2].ToString().Trim();
                        string tmpGoal = tmpRow.ItemArray[3].ToString().Trim();
                        string tmpPlan = tmpRow.ItemArray[4].ToString().Trim();
                        string tmpEval = tmpRow.ItemArray[5].ToString().Trim();

                        TabPage tmpPage = new TabPage(tmpText);

                        // ä≈åÏêfíf
                        Label itemLabel = new Label();
                        itemLabel.Name = "ä≈åÏêfífLabel";
                        itemLabel.Text = "ä≈åÏêfíf";
                        itemLabel.AutoSize = true;
                        itemLabel.Location = new Point(10, 10);
                        tmpPage.Controls.Add(itemLabel);

                        CheckedListBox itemBox = new CheckedListBox();
                        itemBox.Name = "ä≈åÏêfífBox";
                        itemBox.CheckOnClick = true;
                        itemBox.ScrollAlwaysVisible = true;
                        itemBox.ColumnWidth = 200;
                        itemBox.Location = new Point(10, 30);
                        itemBox.Size = new Size(250, 120);

                        foreach (string item in tmpItem.Split('\n'))
                        {
                            itemBox.Items.Add(item.Trim('\r'));
                        }

                        tmpPage.Controls.Add(itemBox);

                        TextBox itemText = new TextBox();
                        itemText.Name = "ä≈åÏêfífText";
                        itemText.Multiline = true;
                        itemText.Location = new Point(265, 30);
                        itemText.Size = new Size(145, 120);
                        tmpPage.Controls.Add(itemText);

                        // ñ⁄ïW
                        Label goalLabel = new Label();
                        goalLabel.Name = "ñ⁄ïWLabel";
                        goalLabel.Text = "ñ⁄ïW";
                        goalLabel.AutoSize = true;
                        goalLabel.Location = new Point(10, 155);
                        tmpPage.Controls.Add(goalLabel);

                        TextBox goalText = new TextBox();
                        goalText.Name = "ñ⁄ïWText";
                        goalText.Multiline = true;
                        goalText.ScrollBars = ScrollBars.None;
                        goalText.Location = new Point(10, 175);
                        goalText.Size = new Size(400, 30);
                        goalText.Text = tmpGoal;
                        tmpPage.Controls.Add(goalText);

                        // ä≈åÏåvâÊ
                        Label planLabel = new Label();
                        planLabel.Name = "ä≈åÏåvâÊLabel";
                        planLabel.Text = "ä≈åÏåvâÊ";
                        planLabel.AutoSize = true;
                        planLabel.Location = new Point(10, 210);
                        tmpPage.Controls.Add(planLabel);
                        
                        CheckBox planBox = new CheckBox();
                        planBox.Name = "ä≈åÏåvâÊBox";
                        planBox.Text = "ãLò^Ç…ä‹ÇﬂÇÈ";
                        planBox.AutoSize = true;
                        planBox.Location = new Point(150, 210);
                        planBox.Checked = true;
                        planBox.Visible = false;
                        tmpPage.Controls.Add(planBox);
                        
                        TextBox planText = new TextBox();
                        planText.Name = "ä≈åÏåvâÊText";
                        planText.Multiline = true;
                        planText.ScrollBars = ScrollBars.Both;
                        planText.Location = new Point(10, 230);
                        planText.Size = new Size(250, 200);
                        planText.Text = tmpPlan;
                        tmpPage.Controls.Add(planText);

                        TextBox planText2 = new TextBox();
                        planText2.Name = "ä≈åÏåvâÊText2";
                        planText2.Multiline = true;
                        planText2.Location = new Point(265, 230);
                        planText2.Size = new Size(145, 200);
                        tmpPage.Controls.Add(planText2);

                        // ï]âø
                        Label evalLabel = new Label();
                        evalLabel.Name = "ï]âøLabel";
                        evalLabel.Text = "ï]âø";
                        evalLabel.AutoSize = true;
                        evalLabel.Location = new Point(10, 435);
                        tmpPage.Controls.Add(evalLabel);

                        CheckedListBox evalBox = new CheckedListBox();
                        evalBox.Name = "ï]âøBox";
                        evalBox.CheckOnClick = true;
                        evalBox.ScrollAlwaysVisible = true;
                        evalBox.ColumnWidth = 200;
                        evalBox.Location = new Point(10, 455);
                        evalBox.Size = new Size(250, 120);

                        foreach (string eval in tmpEval.Split('\n'))
                        {
                            evalBox.Items.Add(eval.Trim('\r'));
                        }

                        tmpPage.Controls.Add(evalBox);

                        TextBox evalText = new TextBox();
                        evalText.Name = "ï]âøText";
                        evalText.Multiline = true;
                        evalText.Location = new Point(265, 455);
                        evalText.Size = new Size(145, 120);
                        tmpPage.Controls.Add(evalText);

                        tabControl1.TabPages.Add(tmpPage);
                    }
                }
            }
        }

        private void makeRecTable()
        {
            if (recTitleBox.Text.Contains(" "))
            {
                DataTable tmpTable = rSet.Tables["RecordItem"];

                this.recKindLabel.Text = "";
                this.recPanel.Controls.Clear();

                // éËèpãLò^ÇÃÉeÅ[ÉuÉãçÏê¨
                foreach (DataRow tmpRow in tmpTable.Select("RecordId like '" + recTitleBox.Text.Split(' ')[0] + "'"))
                {
                    string[] s = new string[20];

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

                        for (int i = 14; i < s.Length; i++)
                        {
                            tmpBox.Tag += s[i] + ",";
                        }

                        tmpBox.KeyDown += new KeyEventHandler(control_KeyDown);

                        this.recPanel.Controls.Add(tmpBox);
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

                        if (s[12].Length > 0)
                        {
                            tmpBox.Multiline = true;
                        }
                        else
                        {
                            tmpBox.Multiline = false;
                            tmpBox.KeyDown += new KeyEventHandler(control_KeyDown);
                        }

                        for (int i = 14; i < s.Length; i++)
                        {
                            tmpBox.Tag += s[i] + ",";
                        }

                        this.recPanel.Controls.Add(tmpBox);
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

                        for (int i = 14; i < s.Length; i++)
                        {
                            tmpLabel.Tag += s[i] + ",";
                        }

                        this.recPanel.Controls.Add(tmpLabel);
                    }
                    else
                    {
                        continue;
                    }
                }

                tmpTable = mSet.Tables["Record"];

                foreach (DataRow tmpRow in tmpTable.Select("Id = '" + recTitleBox.Text.Split(' ')[0] + "'"))
                {
                    if (recKindLabel.Text.Length > 0)
                    {
                        break;
                    }

                    if (tmpRow["Kind"].ToString().Equals("1"))
                    {
                        recKindLabel.Text = "1 èpëO";
                    }
                    else if (tmpRow["Kind"].ToString().Equals("2"))
                    {
                        recKindLabel.Text = "2 èpíÜ";
                    }
                    else if (tmpRow["Kind"].ToString().Equals("3"))
                    {
                        recKindLabel.Text = "3 èpå„";
                    }
                }
            }
        }

        private void control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ProcessTabKey(true);
            }
        }

        private void makeAsText()
        {
            if (f1 == null)
            {
                return;
            }

            if (f1.asTabControl.Controls.Count > 0)
            {
                if (MessageBox.Show("ä˘Ç…ä≈åÏêfífÇ™ãLç⁄Ç≥ÇÍÇƒÇ¢Ç‹Ç∑ÅBè„èëÇ´ÇµÇ‹Ç∑Ç©ÅH", "ämîF", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
            }

            f1.asTabControl.Controls.Clear();

            string tmp_as = "";

            foreach (TabPage tp in tabControl1.TabPages)
            {
                tmp_as = "";

                CheckedListBox tmpCheckedListBox;
                TextBox tmpTextBox;
                CheckBox tmpCheckBox;

                tmp_as += "Åyä≈åÏêfífÅz\r\n";

                tmpCheckedListBox = (CheckedListBox)(tp.Controls["ä≈åÏêfífBox"]);

                foreach (string s in tmpCheckedListBox.CheckedItems)
                {
                    tmp_as += s + "\r\n";
                }

                tmpTextBox = (TextBox)(tp.Controls["ä≈åÏêfífText"]);

                if (tmpTextBox.Text.Length > 0)
                {
                    tmp_as += tmpTextBox.Text + "\r\n";
                }

                tmp_as += "\r\n";

                tmpTextBox = (TextBox)(tp.Controls["ñ⁄ïWText"]);

                tmp_as += "Åyñ⁄ïWÅz\r\n";

                if (tmpTextBox.Text.Length > 0)
                {
                    tmp_as += tmpTextBox.Text + "\r\n";
                }

                tmp_as += "\r\n";

                tmpCheckBox = (CheckBox)(tp.Controls["ä≈åÏåvâÊBox"]);
                tmpTextBox = (TextBox)(tp.Controls["ä≈åÏåvâÊText"]);

                tmp_as += "Åyä≈åÏåvâÊÅz\r\n";

                if (tmpCheckBox.Checked && tmpTextBox.Text.Length > 0)
                {
                    tmp_as += tmpTextBox.Text + "\r\n";
                }

                tmpTextBox = (TextBox)(tp.Controls["ä≈åÏåvâÊText2"]);

                if (tmpTextBox.Text.Length > 0)
                {
                    tmp_as += tmpTextBox.Text + "\r\n";
                }

                tmp_as += "\r\n";

                /*
                tmp_as += "Åyï]âøÅz\r\n";

                tmpCheckedListBox = (CheckedListBox)(tp.Controls["ï]âøBox"]);

                foreach (string s in tmpCheckedListBox.CheckedItems)
                {
                    tmp_as += s + "\r\n";
                }

                tmpTextBox = (TextBox)(tp.Controls["ï]âøText"]);

                if (tmpTextBox.Text.Length > 0)
                {
                    tmp_as += tmpTextBox.Text + "\r\n";
                }

                tmp_as += "\r\n";
                */

                // FormPatÇÃä≈åÏêfífóìÇ…ì¸óÕÇ∑ÇÈ
                TextBox tmpAsTextBox = new TextBox();
                TabPage tmpAsTabPage = new TabPage();

                tmpAsTextBox.Multiline = true;
                tmpAsTextBox.Location = new Point(10, 10);
                tmpAsTextBox.Size = new Size(315, 450);
                tmpAsTextBox.ScrollBars = ScrollBars.Vertical;
                tmpAsTextBox.Text = tmp_as;

                tmpAsTabPage.Controls.Add(tmpAsTextBox);
                tmpAsTabPage.Text = tp.Text;

                f1.asTabControl.Controls.Add(tmpAsTabPage);
            }

            f1.cutOverData();

            // ä≈åÏêfífãLò^é“Çí«â¡
            f1.recHistBox.ListAdd(DateTime.Now.ToString("HH:mm").PadLeft(5, ' ') + " êfíf " + LoginUser.Name);
        }

        private void makeEvalText()
        {
            if (f1 == null)
            {
                return;
            }

            if (f1.asTabControl.TabPages.Count == 0)
            {
                MessageBox.Show("ä≈åÏêfífÇêÊÇ…çÏê¨ÇµÇ»ÇØÇÍÇŒìoò^Ç≈Ç´Ç‹ÇπÇÒÅB");
                return;
            }

            TabPage tmp_page;
            string tmp_as = "";

            foreach (TabPage tp in tabControl1.TabPages)
            {
                tmp_page = null;
                tmp_as = "";

                foreach (TabPage p in f1.asTabControl.TabPages)
                {
                    if (tp.Text == p.Text)
                    {
                        tmp_page = p;
                        break;
                    }
                }

                if (tmp_page == null)
                {
                    continue;
                }

                CheckedListBox tmpCheckedListBox;
                TextBox tmpTextBox;

                tmp_as += "Åyï]âøÅz\r\n";

                tmpCheckedListBox = (CheckedListBox)(tp.Controls["ï]âøBox"]);

                foreach (string s in tmpCheckedListBox.CheckedItems)
                {
                    tmp_as += s + "\r\n";
                }

                tmpTextBox = (TextBox)(tp.Controls["ï]âøText"]);

                if (tmpTextBox.Text.Length > 0)
                {
                    tmp_as += tmpTextBox.Text + "\r\n";
                }

                tmp_as += "\r\n";

                foreach (Control c in tmp_page.Controls)
                {
                    if (c.GetType().Name == "TextBox")
                    {
                        c.Text = c.Text.TrimEnd('\r', '\n') + "\r\n\r\n" + tmp_as;
                        break;
                    }
                }
            }

            f1.cutOverData();

            // ï]âøãLò^é“Çí«â¡
            f1.recHistBox.ListAdd(DateTime.Now.ToString("HH:mm").PadLeft(5, ' ') + " ï]âø " + LoginUser.Name);
        }

        private void makeRecText()
        {
            if (f1 == null)
            {
                return;
            }

            string tmp_rec = "";

            foreach (Control c in recPanel.Controls)
            {
                bool write_flg = true;
                string fix1 = "";
                string fix2 = "";
                string lf1 = "";
                string lf2 = "";

                // É^ÉOÇÃÉ`ÉFÉbÉN
                if (c.Tag != null && c.Tag.ToString().Length > 0)
                {
                    string[] s = c.Tag.ToString().Trim('\n').Trim('\r').Split(',');

                    // ëOå„ï∂éö
                    fix1 = s[2];
                    fix2 = s[3];

                    // ëOâ¸çs
                    if (s[4].Length > 0)
                    {
                        for (int i = 0; i < int.Parse(s[4]); i++)
                        {
                            lf1 += "\r\n";
                        }
                    }

                    // å„â¸çs
                    if (s[5].Length > 0)
                    {
                        for (int i = 0; i < int.Parse(s[5]); i++)
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
                        else if (c.GetType().Name == "Label")
                        {
                            Label tmpLabel = (Label)c;

                            write_flg = true;

                            /*
                            if (tmpLabel.Text.Length > 0)
                            {
                                write_flg = true;
                            }
                            else
                            {
                                continue;
                            }
                             */
                        }
                    }
                    else if (s[0] == "3")
                    {
                        continue;
                    }

                    if (s[0] == "4")
                    {
                        if (s[1].Length > 0 && recPanel.Controls.ContainsKey(s[1].Trim()))
                        {
                            Control trigCtrl = recPanel.Controls[s[1].Trim()];

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
                            }
                        }
                    }
                }

                if (write_flg)
                {
                    tmp_rec += lf1 + fix1 + c.Text + fix2 + lf2;
                }
            }

            // FormPatÇÃä≈åÏãLò^óìÇ…ì¸óÕÇ∑ÇÈ
            if (tmp_rec.Length > 0)
            {
                if (f1.recText.Text.Length > 0)
                {
                    DialogResult tmpResult = FormConfirm.Show("ä˘Ç…ä≈åÏãLò^Ç™ãLç⁄Ç≥ÇÍÇƒÇ¢Ç‹Ç∑ÅB\r\nè„èëÇ´ÇµÇ‹Ç∑Ç©ÅH", "ämîF", "è„èëÇ´Ç∑ÇÈ", "ÉLÉÉÉìÉZÉã", FormConfirm.FormButton.Button2);

                    if (tmpResult == DialogResult.Yes)
                    {
                        f1.recText.Text = tmp_rec;
                    }
                }
                else
                {
                    f1.recText.Text = tmp_rec;
                }

                f1.cutOverData();

                // ä≈åÏãLò^é“Çí«â¡
                f1.recHistBox.ListAdd(DateTime.Now.ToString("HH:mm").PadLeft(5, ' ') + " ãLò^ " + LoginUser.Name);
            }
        }

        private void asTitleBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.makeAsTabs();
        }

        private void makeAsButton_Click(object sender, EventArgs e)
        {
            this.makeAsText();
            f1.Activate();
        }

        private void makeEvalButton_Click(object sender, EventArgs e)
        {
            this.makeEvalText();
            f1.Activate();
        }

        private void recTitleBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.makeRecTable();
        }

        private void makeRecButton_Click(object sender, EventArgs e)
        {
            // éËèpä≈åÏãLò^ÇÃï∂èÕ
            this.makeRecText();

            // éÌï ÇÃëIë
            f1.recKindBox.Text = this.recKindLabel.Text;

            f1.Activate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void readRecFileButton_Click(object sender, EventArgs e)
        {
            this.initRec();
            this.makeRecTable();
        }
    }
}