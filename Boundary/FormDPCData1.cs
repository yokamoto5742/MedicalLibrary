using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class FormDPCData1 : StdForm1
    {
        public FormDPCData1()
        {
            InitializeComponent();

            this.ContBox8.Items.Add("");
            this.ContBox8.Items.Add("非表示");
            this.ContBox8.Items.Add("面会謝絶");

            int h = 10;
            string kind = "";

            List<Panel> panels = new List<Panel>();
            panels.Add(this.Panel1);
            panels.Add(this.Panel2);
            panels.Add(this.Panel3);

            // 様式1 のフォーム作成
            foreach (Panel panel in panels)
            {
                h = 10;
                kind = panel.Tag.ToString();

                foreach (DPCItem1 obj in DPCItem1.List)
                {
                    if (!kind.Equals(obj.Kind))
                    {
                        continue;
                    }

                    Label lb = new Label();
                    lb.AutoEllipsis = true;
                    lb.Location = new Point(10, h);
                    lb.AutoSize = false;
                    lb.Size = new Size(200, 20);
                    lb.Text = obj.Name;
                    lb.TextAlign = ContentAlignment.MiddleLeft;

                    panel.Controls.Add(lb);

                    int maxwidth = 0;

                    if (obj.Box.StartsWith("ComboBox"))
                    {
                        ComboBox box = new ComboBox();
                        box.DropDownStyle = ComboBoxStyle.DropDownList;
                        box.Location = new Point(220, h);
                        box.Size = new Size(150, 21);
                        box.Name = obj.Code;

                        box.Items.Add("");

                        foreach (DPCSubItem1 sub in obj.SubItemList)
                        {
                            box.Items.Add(sub.Code + ": " + sub.Text);

                            if (maxwidth < sub.Text.Length)
                            {
                                maxwidth = sub.Text.Length;
                            }
                        }

                        box.Width = 30 + maxwidth * 12;

                        panel.Controls.Add(box);

                        h += 25;
                    }
                    else if (obj.Box.StartsWith("RadioButton"))
                    {
                        Panel p = new Panel();
                        p.Location = new Point(220, h);
                        p.Size = new Size(panel.Width - 220 - 30, 100);
                        p.Name = obj.Code;
                        p.BorderStyle = BorderStyle.FixedSingle;

                        foreach (DPCSubItem1 sub in obj.SubItemList)
                        {
                            RadioButton button = new RadioButton();
                            button.AutoSize = true;
                            button.Text = sub.Code + ": " + sub.Text;

                            if (button.PreferredSize.Width > maxwidth)
                            {
                                maxwidth = button.PreferredSize.Width;
                            }
                        }

                        maxwidth += 20;

                        int c = 1;

                        if (p.Width / maxwidth >= 2)
                        {
                            c = p.Width / maxwidth;
                        }

                        int x = 3;
                        int hh = 3;
                        int cc = 1;

                        foreach (DPCSubItem1 sub in obj.SubItemList)
                        {
                            RadioButton button = new RadioButton();
                            button.Location = new Point(x, hh);
                            button.AutoSize = true;
                            button.Name = obj.Code + "_" + sub.Code;
                            button.Text = sub.Code + ": " + sub.Text;

                            if (cc < c)
                            {
                                cc++;
                                x += maxwidth;
                            }
                            else
                            {
                                cc = 1;
                                x = 3;
                                hh += 22;
                            }

                            p.Controls.Add(button);
                        }

                        if (x > 3)
                        {
                            hh += 22;
                        }

                        p.Height = hh;
                        h += hh + 3;

                        panel.Controls.Add(p);
                    }
                    else if (obj.Box.StartsWith("TextBox"))
                    {
                        TextBox box = new TextBox();
                        box.Location = new Point(220, h);
                        box.Size = new Size(160, 21);
                        box.MaxLength = 100;
                        box.Name = obj.Code;

                        panel.Controls.Add(box);

                        h += 25;
                    }
                }
            }

            this.InHistoryBox2.ValueChanged += new EventHandler<EventArgs>(InHistoryBox2_ValueChanged);

            this.SaveButton8.Click += new EventHandler(SaveButton_Click);

            this.SaveButton11.Click += new EventHandler(SaveButton_Click);
            this.SaveButton13.Click += new EventHandler(SaveButton_Click);
            this.SaveButton14.Click += new EventHandler(SaveButton_Click);
            this.SaveButton15.Click += new EventHandler(SaveButton_Click);
            this.SaveButton16.Click += new EventHandler(SaveButton_Click);
            this.SaveButton17.Click += new EventHandler(SaveButton_Click);

            this.SaveButton21.Click += new EventHandler(SaveButton_Click);
            this.SaveButton23.Click += new EventHandler(SaveButton_Click);
            this.SaveButton24.Click += new EventHandler(SaveButton_Click);
            this.SaveButton25.Click += new EventHandler(SaveButton_Click);
            this.SaveButton26.Click += new EventHandler(SaveButton_Click);
            this.SaveButton27.Click += new EventHandler(SaveButton_Click);

            this.SaveButton31.Click += new EventHandler(SaveButton_Click);
            this.SaveButton33.Click += new EventHandler(SaveButton_Click);
            this.SaveButton34.Click += new EventHandler(SaveButton_Click);
            this.SaveButton35.Click += new EventHandler(SaveButton_Click);
            this.SaveButton36.Click += new EventHandler(SaveButton_Click);
            this.SaveButton37.Click += new EventHandler(SaveButton_Click);

            if (LoginUser.IsDPC)
            {
                this.SaveButton17.Enabled = true;
                this.SaveButton27.Enabled = true;
                this.SaveButton37.Enabled = true;
            }
            else
            {
                this.SaveButton17.Enabled = false;
                this.SaveButton27.Enabled = false;
                this.SaveButton37.Enabled = false;
            }
        }

        private void FormDPCData1_Load(object sender, EventArgs e)
        {
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            this.PatShow(p);
        }

        public override void FontSet(AppFont f)
        {
            base.FontSet(f);

            this.stdControlFont11.FontSet(f);
        }

        public void PatInDateSet(PatBase p, string in_date)
        {
            if (this.stdControlPat11.Pat.Id.Length > 0 && !this.stdControlPat11.Pat.Id.Equals(p.Id))
            {
                if (MessageBox.Show("入院・DPC画面に表示されている患者IDを変更します。" + Environment.NewLine + "よろしいですか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                {
                    return;
                }
            }

            this.PatSet(p);
            this.InDateSet(in_date);
        }

        void PatShow(PatBase p)
        {
            this.DataClear();

            // 入院履歴を取得
            this.InHistoryBox2.Init(p.Id);

            if (this.InHistoryBox2.InDateString.Length > 0)
            {
                this.DataShow();
            }
        }

        /// <summary>
        /// 入院日を指定
        /// </summary>
        /// <param name="in_date">8桁の日付</param>
        public void InDateSet(string in_date)
        {
            if (this.Pat.Id.Length == 0 || in_date.Length == 0)
            {
                return;
            }

            this.InHistoryBox2.Init(this.Pat.Id, in_date);
//            this.InHistoryBox2.PatIn1 = PatIn.GetByDates(this.Pat.Id, int.Parse(in_date), int.Parse(in_date));
        }

        private void InHistoryBox2_ValueChanged(object sender, EventArgs e)
        {
            this.DataShow();
        }

        void DataClear()
        {
            // 様式1 のフォーム
            foreach (DPCItem1 obj in DPCItem1.List)
            {
                if (!obj.Kind.Equals("1"))
                {
                    continue;
                }

                if (this.Panel1.Controls.ContainsKey(obj.Code))
                {
                    Control c = this.Panel1.Controls[obj.Code];

                    if (obj.Box.StartsWith("RadioButton"))
                    {
                        foreach (DPCSubItem1 sub in obj.SubItemList)
                        {
                            if (c.Controls.ContainsKey(obj.Code + "_" + sub.Code))
                            {
                                Control cc = c.Controls[obj.Code + "_" + sub.Code];

                                if (cc is RadioButton)
                                {
                                    ((RadioButton)cc).Checked = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        c.Text = "";
                    }
                }
            }

            this.CompleteBox11.Checked = false;
            this.SaveStaffLabel11.Text = "";

            // 医師完了
            this.CompleteBox13.Checked = false;
            this.SaveStaffLabel13.Text = "";

            // 看護師完了
            this.CompleteBox14.Checked = false;
            this.SaveStaffLabel14.Text = "";

            // レセ完了
            this.CompleteBox15.Checked = false;
            this.SaveStaffLabel15.Text = "";

            // DPC完了
            this.CompleteBox16.Checked = false;
            this.SaveStaffLabel16.Text = "";

            // コメント
            this.ContBox17.Clear();
            this.SaveStaffLabel17.Text = "";


            // 様式1 のフォーム
            foreach (DPCItem1 obj in DPCItem1.List)
            {
                if (!obj.Kind.Equals("2"))
                {
                    continue;
                }

                if (this.Panel2.Controls.ContainsKey(obj.Code))
                {
                    Control c = this.Panel2.Controls[obj.Code];

                    if (obj.Box.StartsWith("RadioButton"))
                    {
                        foreach (DPCSubItem1 sub in obj.SubItemList)
                        {
                            if (c.Controls.ContainsKey(obj.Code + "_" + sub.Code))
                            {
                                Control cc = c.Controls[obj.Code + "_" + sub.Code];

                                if (cc is RadioButton)
                                {
                                    ((RadioButton)cc).Checked = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        c.Text = "";
                    }
                }
            }

            this.CompleteBox21.Checked = false;
            this.SaveStaffLabel21.Text = "";

            // 医師完了
            this.CompleteBox23.Checked = false;
            this.SaveStaffLabel23.Text = "";

            // 看護師完了
            this.CompleteBox24.Checked = false;
            this.SaveStaffLabel24.Text = "";

            // レセ完了
            this.CompleteBox25.Checked = false;
            this.SaveStaffLabel25.Text = "";

            // DPC完了
            this.CompleteBox26.Checked = false;
            this.SaveStaffLabel26.Text = "";

            // コメント
            this.ContBox27.Clear();
            this.SaveStaffLabel27.Text = "";


            // 様式1 のフォーム
            foreach (DPCItem1 obj in DPCItem1.List)
            {
                if (!obj.Kind.Equals("3"))
                {
                    continue;
                }

                if (this.Panel3.Controls.ContainsKey(obj.Code))
                {
                    Control c = this.Panel3.Controls[obj.Code];

                    if (obj.Box.StartsWith("RadioButton"))
                    {
                        foreach (DPCSubItem1 sub in obj.SubItemList)
                        {
                            if (c.Controls.ContainsKey(obj.Code + "_" + sub.Code))
                            {
                                Control cc = c.Controls[obj.Code + "_" + sub.Code];

                                if (cc is RadioButton)
                                {
                                    ((RadioButton)cc).Checked = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        c.Text = "";
                    }
                }
            }

            this.CompleteBox31.Checked = false;
            this.SaveStaffLabel31.Text = "";

            // 医師完了
            this.CompleteBox33.Checked = false;
            this.SaveStaffLabel33.Text = "";

            // 看護師完了
            this.CompleteBox34.Checked = false;
            this.SaveStaffLabel34.Text = "";

            // レセ完了
            this.CompleteBox35.Checked = false;
            this.SaveStaffLabel35.Text = "";

            // DPC完了
            this.CompleteBox36.Checked = false;
            this.SaveStaffLabel36.Text = "";

            // コメント
            this.ContBox37.Clear();
            this.SaveStaffLabel37.Text = "";


            // 氏名表示
            this.ContBox8.Text = "";
        }

        void DataShow()
        {
            this.DataClear();

//            PatIn pin = (PatIn)this.InHistoryBox1.SelectedItem;
            PatIn pin = this.InHistoryBox2.PatIn1;

            if (pin.InDate.Length != 8)
            {
                return;
            }

            Dictionary<string, DPCData> dict = DPCData.GetDict(this.Pat.Id, pin.InDate);

            DPCData data;

            if (dict.ContainsKey("11"))
            {
                data = dict["11"];

                // 様式1 のフォーム
                foreach (DPCItem1 obj in DPCItem1.List)
                {
                    if (!data.ContDict.ContainsKey(obj.Code))
                    {
                        continue;
                    }

                    string s = data.ContDict[obj.Code].Text;

                    if (this.Panel1.Controls.ContainsKey(obj.Code))
                    {
                        Control c = this.Panel1.Controls[obj.Code];

                        if (obj.Box.StartsWith("RadioButton"))
                        {
                            foreach (DPCSubItem1 sub in obj.SubItemList)
                            {
                                if (c.Controls.ContainsKey(obj.Code + "_" + sub.Code))
                                {
                                    Control cc = c.Controls[obj.Code + "_" + sub.Code];

                                    if (cc is RadioButton && cc.Text.Equals(s))
                                    {
                                        ((RadioButton)cc).Checked = true;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            c.Text = s;
                        }
                    }
                }

                if (data.Status.Equals("1"))
                {
                    this.CompleteBox11.Checked = true;
                }

                this.SaveStaffLabel11.Text = data.SaveDateTime + " " + data.StaffName;
            }

            // 医師完了
            if (dict.ContainsKey("13"))
            {
                data = dict["13"];

                if (data.Status.Equals("1"))
                {
                    this.CompleteBox13.Checked = true;
                }

                this.SaveStaffLabel13.Text = data.SaveDateTime + " " + data.StaffName;
            }

            // 看護師完了
            if (dict.ContainsKey("14"))
            {
                data = dict["14"];

                if (data.Status.Equals("1"))
                {
                    this.CompleteBox14.Checked = true;
                }

                this.SaveStaffLabel14.Text = data.SaveDateTime + " " + data.StaffName;
            }

            // レセ完了
            if (dict.ContainsKey("15"))
            {
                data = dict["15"];

                if (data.Status.Equals("1"))
                {
                    this.CompleteBox15.Checked = true;
                }

                this.SaveStaffLabel15.Text = data.SaveDateTime + " " + data.StaffName;
            }

            // DPC完了
            if (dict.ContainsKey("16"))
            {
                data = dict["16"];

                if (data.Status.Equals("1"))
                {
                    this.CompleteBox16.Checked = true;
                }

                this.SaveStaffLabel16.Text = data.SaveDateTime + " " + data.StaffName;
            }

            // DPCコメント
            if (dict.ContainsKey("17"))
            {
                data = dict["17"];

                this.ContBox17.Text = data.Cont;
                this.SaveStaffLabel17.Text = data.SaveDateTime + " " + data.StaffName;
            }


            if (dict.ContainsKey("21"))
            {
                data = dict["21"];

                // 様式1 のフォーム
                foreach (DPCItem1 obj in DPCItem1.List)
                {
                    if (!data.ContDict.ContainsKey(obj.Code))
                    {
                        continue;
                    }

                    string s = data.ContDict[obj.Code].Text;

                    if (this.Panel2.Controls.ContainsKey(obj.Code))
                    {
                        Control c = this.Panel2.Controls[obj.Code];

                        if (obj.Box.StartsWith("RadioButton"))
                        {
                            foreach (DPCSubItem1 sub in obj.SubItemList)
                            {
                                if (c.Controls.ContainsKey(obj.Code + "_" + sub.Code))
                                {
                                    Control cc = c.Controls[obj.Code + "_" + sub.Code];

                                    if (cc is RadioButton && cc.Text.Equals(s))
                                    {
                                        ((RadioButton)cc).Checked = true;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            c.Text = s;
                        }
                    }
                }

                if (data.Status.Equals("1"))
                {
                    this.CompleteBox21.Checked = true;
                }

                this.SaveStaffLabel21.Text = data.SaveDateTime + " " + data.StaffName;
            }

            // 医師完了
            if (dict.ContainsKey("23"))
            {
                data = dict["23"];

                if (data.Status.Equals("1"))
                {
                    this.CompleteBox23.Checked = true;
                }

                this.SaveStaffLabel23.Text = data.SaveDateTime + " " + data.StaffName;
            }

            // 看護師完了
            if (dict.ContainsKey("24"))
            {
                data = dict["24"];

                if (data.Status.Equals("1"))
                {
                    this.CompleteBox24.Checked = true;
                }

                this.SaveStaffLabel24.Text = data.SaveDateTime + " " + data.StaffName;
            }

            // レセ完了
            if (dict.ContainsKey("25"))
            {
                data = dict["25"];

                if (data.Status.Equals("1"))
                {
                    this.CompleteBox25.Checked = true;
                }

                this.SaveStaffLabel25.Text = data.SaveDateTime + " " + data.StaffName;
            }

            // DPC完了
            if (dict.ContainsKey("26"))
            {
                data = dict["26"];

                if (data.Status.Equals("1"))
                {
                    this.CompleteBox26.Checked = true;
                }

                this.SaveStaffLabel26.Text = data.SaveDateTime + " " + data.StaffName;
            }

            // DPCコメント
            if (dict.ContainsKey("27"))
            {
                data = dict["27"];

                this.ContBox27.Text = data.Cont;
                this.SaveStaffLabel27.Text = data.SaveDateTime + " " + data.StaffName;
            }


            if (dict.ContainsKey("31"))
            {
                data = dict["31"];

                // 様式1 のフォーム
                foreach (DPCItem1 obj in DPCItem1.List)
                {
                    if (!data.ContDict.ContainsKey(obj.Code))
                    {
                        continue;
                    }

                    string s = data.ContDict[obj.Code].Text;

                    if (this.Panel3.Controls.ContainsKey(obj.Code))
                    {
                        Control c = this.Panel3.Controls[obj.Code];

                        if (obj.Box.StartsWith("RadioButton"))
                        {
                            foreach (DPCSubItem1 sub in obj.SubItemList)
                            {
                                if (c.Controls.ContainsKey(obj.Code + "_" + sub.Code))
                                {
                                    Control cc = c.Controls[obj.Code + "_" + sub.Code];

                                    if (cc is RadioButton && cc.Text.Equals(s))
                                    {
                                        ((RadioButton)cc).Checked = true;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            c.Text = s;
                        }
                    }
                }

                if (data.Status.Equals("1"))
                {
                    this.CompleteBox31.Checked = true;
                }

                this.SaveStaffLabel31.Text = data.SaveDateTime + " " + data.StaffName;
            }

            // 医師完了
            if (dict.ContainsKey("33"))
            {
                data = dict["33"];

                if (data.Status.Equals("1"))
                {
                    this.CompleteBox33.Checked = true;
                }

                this.SaveStaffLabel33.Text = data.SaveDateTime + " " + data.StaffName;
            }

            // 看護師完了
            if (dict.ContainsKey("34"))
            {
                data = dict["34"];

                if (data.Status.Equals("1"))
                {
                    this.CompleteBox34.Checked = true;
                }

                this.SaveStaffLabel34.Text = data.SaveDateTime + " " + data.StaffName;
            }

            // レセ完了
            if (dict.ContainsKey("35"))
            {
                data = dict["35"];

                if (data.Status.Equals("1"))
                {
                    this.CompleteBox35.Checked = true;
                }

                this.SaveStaffLabel35.Text = data.SaveDateTime + " " + data.StaffName;
            }

            // DPC完了
            if (dict.ContainsKey("36"))
            {
                data = dict["36"];

                if (data.Status.Equals("1"))
                {
                    this.CompleteBox36.Checked = true;
                }

                this.SaveStaffLabel36.Text = data.SaveDateTime + " " + data.StaffName;
            }

            // DPCコメント
            if (dict.ContainsKey("37"))
            {
                data = dict["37"];

                this.ContBox37.Text = data.Cont;
                this.SaveStaffLabel37.Text = data.SaveDateTime + " " + data.StaffName;
            }

            // 氏名表示
            if (dict.ContainsKey("8"))
            {
                data = dict["8"];

                this.ContBox8.Text = data.Cont;
            }
        }

        void SaveButton_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            DPCData data = new DPCData();

            if (this.Pat.Id.Length == 0)
            {
                MessageBox.Show("患者が指定されていません");
                return;
            }

            PatIn pin = this.InHistoryBox2.PatIn1;

            if (pin == null || pin.InDate.Length != 8)
            {
                MessageBox.Show("入院期間が指定されていません");
                return;
            }

            if (MessageBox.Show("登録します。よろしいですか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            data.Id = this.Pat.Id;
            data.AdmDate = pin.InDate;
            data.Kind = b.Tag.ToString();

            if (data.Kind.Equals("11"))
            {
                // 様式1 のフォーム
                foreach (DPCItem1 obj in DPCItem1.List)
                {
                    if (!obj.Kind.Equals("1"))
                    {
                        continue;
                    }

                    if (this.Panel1.Controls.ContainsKey(obj.Code))
                    {
                        Control c = this.Panel1.Controls[obj.Code];

                        if (obj.Box.StartsWith("RadioButton"))
                        {
                            foreach (DPCSubItem1 sub in obj.SubItemList)
                            {
                                if (c.Controls.ContainsKey(obj.Code + "_" + sub.Code))
                                {
                                    Control cc = c.Controls[obj.Code + "_" + sub.Code];

                                    if (cc is RadioButton && ((RadioButton)cc).Checked)
                                    {
                                        data.Cont += obj.Code + "," + cc.Text + Environment.NewLine;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            data.Cont += obj.Code + "," + c.Text + Environment.NewLine;
                        }
                    }
                }

                if (this.CompleteBox11.Checked)
                {
                    data.Status = "1";
                }
                else
                {
                    data.Status = "2";
                }
            }
            else if (data.Kind.Equals("21"))
            {
                // 様式1 のフォーム
                foreach (DPCItem1 obj in DPCItem1.List)
                {
                    if (!obj.Kind.Equals("2"))
                    {
                        continue;
                    }

                    if (this.Panel2.Controls.ContainsKey(obj.Code))
                    {
                        Control c = this.Panel2.Controls[obj.Code];

                        if (obj.Box.StartsWith("RadioButton"))
                        {
                            foreach (DPCSubItem1 sub in obj.SubItemList)
                            {
                                if (c.Controls.ContainsKey(obj.Code + "_" + sub.Code))
                                {
                                    Control cc = c.Controls[obj.Code + "_" + sub.Code];

                                    if (cc is RadioButton && ((RadioButton)cc).Checked)
                                    {
                                        data.Cont += obj.Code + "," + cc.Text + Environment.NewLine;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            data.Cont += obj.Code + "," + c.Text + Environment.NewLine;
                        }
                    }
                }

                if (this.CompleteBox21.Checked)
                {
                    data.Status = "1";
                }
                else
                {
                    data.Status = "2";
                }
            }
            else if (data.Kind.Equals("31"))
            {
                // 様式1 のフォーム
                foreach (DPCItem1 obj in DPCItem1.List)
                {
                    if (!obj.Kind.Equals("3"))
                    {
                        continue;
                    }

                    if (this.Panel3.Controls.ContainsKey(obj.Code))
                    {
                        Control c = this.Panel3.Controls[obj.Code];

                        if (obj.Box.StartsWith("RadioButton"))
                        {
                            foreach (DPCSubItem1 sub in obj.SubItemList)
                            {
                                if (c.Controls.ContainsKey(obj.Code + "_" + sub.Code))
                                {
                                    Control cc = c.Controls[obj.Code + "_" + sub.Code];

                                    if (cc is RadioButton && ((RadioButton)cc).Checked)
                                    {
                                        data.Cont += obj.Code + "," + cc.Text + Environment.NewLine;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            data.Cont += obj.Code + "," + c.Text + Environment.NewLine;
                        }
                    }
                }

                if (this.CompleteBox31.Checked)
                {
                    data.Status = "1";
                }
                else
                {
                    data.Status = "2";
                }
            }
            else if (data.Kind.Equals("13") || data.Kind.Equals("14") || data.Kind.Equals("15") || data.Kind.Equals("16"))
            {
                if (this.TabPage1.Controls.ContainsKey("CompleteBox" + data.Kind))
                {
                    CheckBox cb = (CheckBox)(this.TabPage1.Controls["CompleteBox" + data.Kind]);

                    if (cb.Checked)
                    {
                        data.Status = "1";
                    }
                }
            }
            else if (data.Kind.Equals("23") || data.Kind.Equals("24") || data.Kind.Equals("25") || data.Kind.Equals("26"))
            {
                if (this.TabPage2.Controls.ContainsKey("CompleteBox" + data.Kind))
                {
                    CheckBox cb = (CheckBox)(this.TabPage2.Controls["CompleteBox" + data.Kind]);

                    if (cb.Checked)
                    {
                        data.Status = "1";
                    }
                }
            }
            else if (data.Kind.Equals("33") || data.Kind.Equals("34") || data.Kind.Equals("35") || data.Kind.Equals("36"))
            {
                if (this.TabPage3.Controls.ContainsKey("CompleteBox" + data.Kind))
                {
                    CheckBox cb = (CheckBox)(this.TabPage3.Controls["CompleteBox" + data.Kind]);

                    if (cb.Checked)
                    {
                        data.Status = "1";
                    }
                }
            }
            else if (data.Kind.Equals("17"))
            {
                if (this.TabPage1.Controls.ContainsKey("ContBox" + data.Kind))
                {
                    data.Cont = this.TabPage1.Controls["ContBox" + data.Kind].Text;
                }
            }
            else if (data.Kind.Equals("27"))
            {
                if (this.TabPage2.Controls.ContainsKey("ContBox" + data.Kind))
                {
                    data.Cont = this.TabPage2.Controls["ContBox" + data.Kind].Text;
                }
            }
            else if (data.Kind.Equals("37"))
            {
                if (this.TabPage3.Controls.ContainsKey("ContBox" + data.Kind))
                {
                    data.Cont = this.TabPage3.Controls["ContBox" + data.Kind].Text;
                }
            }
            else if (data.Kind.Equals("8"))
            {
                if (this.Controls.ContainsKey("ContBox" + data.Kind))
                {
                    data.Cont = this.Controls["ContBox" + data.Kind].Text;
                }
            }

            data.Save();
            this.DataShow();

            FormControl.FormByotoList_ListShow();
        }
    }
}
