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
    public partial class FormBaseInfo : StdForm1
    {
        bool _ReadOnly = true;

        public bool ReadOnly
        {
            get
            {
                return this._ReadOnly;
            }
            set
            {
                this._ReadOnly = value;

                if (value)
                {
                    this.SaveFixedButton.Visible = false;
                    this.ctrlAllergy11.ReadOnly = true;

                    foreach (TabPage page in this.TabControl1.TabPages)
                    {
                        if (page.Controls.ContainsKey("SaveButton"))
                        {
                            page.Controls["SaveButton"].Visible = false;
                        }
                    }
                }
                else
                {
                    this.SaveFixedButton.Visible = true;
                    this.ctrlAllergy11.ReadOnly = false;

                    foreach (TabPage page in this.TabControl1.TabPages)
                    {
                        if (page.Controls.ContainsKey("SaveButton"))
                        {
                            page.Controls["SaveButton"].Visible = true;
                        }
                    }
                }
            }
        }

        public FormBaseInfo(bool read_only = true)
        {
            InitializeComponent();
            
            int h = 30;

            // 固定情報タブ
            foreach (BaseInfoFixedMaster item in BaseInfoFixedMaster.List)
            {
                Label lb1 = new Label();
                lb1.Name = item.Code + "_Label";
                lb1.AutoEllipsis = true;
                lb1.Width = 120;
                lb1.Height = 20;
                lb1.BorderStyle = BorderStyle.FixedSingle;
                lb1.Location = new Point(10, h);
                lb1.Text = item.Name;
                lb1.TextAlign = ContentAlignment.MiddleLeft;

                TabPageFixed.Controls.Add(lb1);

                // １番目の列の入力部分
                if (item.Kind1 == BaseInfoFixedMaster.InputKind.TextBox)
                {
                    // テキストボックスの場合
                    TextBox c = new TextBox();
                    c.Name = item.Code + "_TextBox1";
                    c.Width = 120;
                    c.Height = 20;
                    c.Location = new Point(130, h);

                    TabPageFixed.Controls.Add(c);
                }
                else if (item.Kind1 == BaseInfoFixedMaster.InputKind.CheckBox)
                {
                    // チェックボックスの場合
                    CheckBox c = new CheckBox();
                    c.Name = item.Code + "_CheckBox1";
                    c.Width = 120;
                    c.Height = 20;
                    c.Location = new Point(130, h);
                    c.Text = item.KindText1;

                    TabPageFixed.Controls.Add(c);
                }
                else if (item.Kind1 == BaseInfoFixedMaster.InputKind.ComboBox)
                {
                    // コンボボックスの場合
                    ComboBox c = new ComboBox();
                    c.Name = item.Code + "_ComboBox1";
                    c.Width = 120;
                    c.Height = 20;
                    c.Location = new Point(130, h);
                    c.DropDownStyle = ComboBoxStyle.DropDownList;

                    c.Items.Add("");

                    // アイテムのセット
                    foreach (string s in item.KindItemList1)
                    {
                        c.Items.Add(s);
                    }

                    TabPageFixed.Controls.Add(c);
                }
                else if (item.Kind1 == BaseInfoFixedMaster.InputKind.DateTime)
                {
                    // 日付の場合
                }

                // ２番目の列の入力部分（テキストボックスのみ）
                if (item.DbColumnName2.Length > 0 && item.Kind2 == BaseInfoFixedMaster.InputKind.TextBox)
                {
                    TextBox c = new TextBox();
                    c.Name = item.Code + "_TextBox2";
                    c.Width = 360;
                    c.Height = 20;
                    c.Location = new Point(250, h);

                    TabPageFixed.Controls.Add(c);
                }

                h += 20;
            }

            // その他のタブを作成する
            foreach (BaseInfoTabMaster tab in BaseInfoTabMaster.List)
            {
                TabPage page = new TabPage();

                page.Name = "TabPage" + tab.Code;
                page.Text = tab.Name;
                page.AutoScroll = true;


                Label StaffLabel = new Label();
                StaffLabel.Name = "StaffLabel";
                StaffLabel.BackColor = Color.LightYellow;
                StaffLabel.Size = new Size(100, 16);
                StaffLabel.Location = new Point(60, 10);
                StaffLabel.TextAlign = ContentAlignment.MiddleCenter;

                page.Controls.Add(StaffLabel);


                Label DateTimeLabel = new Label();
                DateTimeLabel.Name = "DateTimeLabel";
                DateTimeLabel.BackColor = Color.LightYellow;
                DateTimeLabel.Size = new Size(120, 16);
                DateTimeLabel.Location = new Point(165, 10);
                DateTimeLabel.TextAlign = ContentAlignment.MiddleCenter;

                page.Controls.Add(DateTimeLabel);


                Button SaveButton = new Button();
                SaveButton.Name = "SaveButton";
                SaveButton.Tag = tab.Code;
                SaveButton.Size = new Size(75, 23);
                SaveButton.Location = new Point(300, 5);
                SaveButton.Text = "登録";
                SaveButton.Click += new EventHandler(SaveButton_Click);

                page.Controls.Add(SaveButton);



                // 入力欄を作成する
                if (BaseInfoItemMaster.Dict.ContainsKey(tab.Code))
                {
                    h = 30;

                    foreach (BaseInfoItemMaster item in BaseInfoItemMaster.Dict[tab.Code])
                    {
                        Label lb1 = new Label();
                        lb1.Name = item.ItemKey + "_Label1";
                        lb1.AutoEllipsis = true;
                        lb1.Width = 120;
                        lb1.Height = 20 * item.InputHeight;
                        lb1.BorderStyle = BorderStyle.FixedSingle;
                        lb1.Location = new Point(10, h);
                        lb1.Text = item.Name1;
                        lb1.TextAlign = ContentAlignment.MiddleLeft;

                        page.Controls.Add(lb1);

                        Label lb2 = new Label();
                        lb2.Name = item.ItemKey + "_Label2";
                        lb2.AutoEllipsis = true;
                        lb2.Width = 120;
                        lb2.Height = 20 * item.InputHeight;
                        lb2.BorderStyle = BorderStyle.FixedSingle;
                        lb2.Location = new Point(10 + lb1.Width, h);
                        lb2.Text = item.Name2;
                        lb2.TextAlign = ContentAlignment.MiddleLeft;

                        page.Controls.Add(lb2);

                        if (item.InputType.Equals(1))
                        {
                            // テキストボックス
                            TextBox c = new TextBox();
                            c.Name = item.ItemKey + "_TextBox";
                            c.Width = 120 * item.InputWidth;
                            c.Location = new Point(10 + lb1.Width + lb2.Width, h);
                            c.Tag = item.ItemKey;

                            if (item.InputHeight > 1)
                            {
                                c.Height = 20 * item.InputHeight;
                                c.Multiline = true;
                                c.ScrollBars = ScrollBars.Both;
                            }
                            else
                            {
                                c.Height = 20 * item.InputHeight;
                                c.Multiline = false;
                            }

                            page.Controls.Add(c);

                            // 単位
                            if (item.Unit.Length > 0)
                            {
                                Label lb3 = new Label();
                                lb3.Name = item.ItemKey + "_Label3";
                                lb3.AutoEllipsis = true;
                                lb3.Width = 120;
                                lb3.Height = 20 * item.InputHeight;
                                lb3.BorderStyle = BorderStyle.None;
                                lb3.Location = new Point(10 + lb1.Width + lb2.Width + c.Width, h);
                                lb3.Text = item.Unit;
                                lb3.TextAlign = ContentAlignment.MiddleLeft;

                                page.Controls.Add(lb3);
                            }
                        }
                        else if (item.InputType.Equals(3))
                        {
                            // コンボボックス
                            ComboBox c = new ComboBox();
                            c.Name = item.ItemKey + "_ComboBox";
                            c.Width = 120 * item.InputWidth;
                            c.Height = 20 * item.InputHeight;
                            c.Location = new Point(10 + lb1.Width + lb2.Width, h);
                            c.DropDownStyle = ComboBoxStyle.DropDownList;
                            c.Tag = item.ItemKey;

                            c.Items.Add("");

                            // アイテムを追加する
                            if (BaseInfoSelectMaster.Dict.ContainsKey(item.SelectMasterCode))
                            {
                                foreach (BaseInfoSelectMaster sm in BaseInfoSelectMaster.Dict[item.SelectMasterCode])
                                {
                                    c.Items.Add(sm);
                                }
                            }

                            page.Controls.Add(c);
                        }
                        else if (item.InputType.Equals(4))
                        {
                            // チェックボックス

                            int x = 10 + lb1.Width + lb2.Width;
                            int i = 1;

                            if (BaseInfoSelectMaster.Dict.ContainsKey(item.SelectMasterCode))
                            {
                                foreach (BaseInfoSelectMaster sm in BaseInfoSelectMaster.Dict[item.SelectMasterCode])
                                {
                                    CheckBox c = new CheckBox();
                                    c.Name = item.ItemKey + "_CheckBox" + sm.IntValue;
                                    c.Width = 120 * item.InputWidth;
                                    c.Height = 20 * item.InputHeight;
                                    c.Location = new Point(x, h);
                                    c.Text = sm.StringValue;
                                    c.Tag = item.ItemKey;

                                    page.Controls.Add(c);

                                    x += 120;
                                    i++;
                                }
                            }
                        }

                        h += 20 * item.InputHeight;
                    }
                }

                TabControl1.TabPages.Add(page);
            }

            this.ReadOnly = read_only;
        }

        /// <summary>
        /// 固定タブのデータを保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFixedButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("登録しますか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            // コードと値の組み合わせ
            Dictionary<string, string> dict = new Dictionary<string, string>();

            foreach (BaseInfoFixedMaster fm in BaseInfoFixedMaster.List)
            {
                // １番目の入力欄
                string key = fm.Code + "_1";
                string val = "";

                if (fm.Kind1 == BaseInfoFixedMaster.InputKind.CheckBox)
                {
                    // チェックボックスの場合
                    if (TabPageFixed.Controls.ContainsKey(fm.Code + "_CheckBox1"))
                    {
                        CheckBox cb = (CheckBox)(TabPageFixed.Controls[fm.Code + "_CheckBox1"]);

                        if (cb.Checked)
                        {
                            val = "1";
                        }
                        else
                        {
                            val = "0";
                        }
                    }
                }
                else if (fm.Kind1 == BaseInfoFixedMaster.InputKind.ComboBox)
                {
                    // コンボボックスの場合
                    if (TabPageFixed.Controls.ContainsKey(fm.Code + "_ComboBox1"))
                    {
                        val = ((ComboBox)(TabPageFixed.Controls[fm.Code + "_ComboBox1"])).Text;
                    }
                }
                else if (fm.Kind1 == BaseInfoFixedMaster.InputKind.TextBox)
                {
                    // テキストボックスの場合
                    if (TabPageFixed.Controls.ContainsKey(fm.Code + "_TextBox1"))
                    {
                        val = ((TextBox)(TabPageFixed.Controls[fm.Code + "_TextBox1"])).Text;
                    }
                }

                dict.Add(key, val);

                // ２番目の入力欄
                key = fm.Code + "_2";
                val = "";

                if (fm.DbColumnName2.Length > 0 && fm.Kind2 == BaseInfoFixedMaster.InputKind.TextBox)
                {
                    val = ((TextBox)(TabPageFixed.Controls[fm.Code + "_TextBox2"])).Text;
                    dict.Add(key, val);
                }
            }

            BaseInfoFixed.Save(this.Pat.Id, dict);

            this.DataShow();
        }

        /// <summary>
        /// 固定・感染禁忌以外タブのデータを保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SaveButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("登録しますか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            Button button = (Button)sender;

            string code = button.Tag.ToString();

            if (!this.TabControl1.TabPages.ContainsKey("TabPage" + code))
            {
                return;
            }

            TabPage page = this.TabControl1.TabPages["TabPage" + code];

            // 入力された結果値のリスト
            List<BaseInfo> list = new List<BaseInfo>();

            // 入力欄を確認する
            if (BaseInfoItemMaster.Dict.ContainsKey(code))
            {
                foreach (BaseInfoItemMaster item in BaseInfoItemMaster.Dict[code])
                {
                    if (item.InputType.Equals(1))
                    {
                        // テキストボックス
                        if (page.Controls.ContainsKey(item.ItemKey + "_TextBox"))
                        {
                            Control c = page.Controls[item.ItemKey + "_TextBox"];

                            if (c.Text.Length > 0)
                            {
                                // 入力されたテキストは「データ値 = c.Text」となる
                                BaseInfo data = new BaseInfo();
                                data.ItemKey = item.ItemKey;
                                data.SEQ = 1;
                                data.Name1 = item.Name1;
                                data.Name2 = item.Name2;
                                data.Value = c.Text;

                                list.Add(data);
                            }
                        }
                    }
                    else if (item.InputType.Equals(3))
                    {
                        // コンボボックス
                        if (page.Controls.ContainsKey(item.ItemKey + "_ComboBox"))
                        {
                            Control c = page.Controls[item.ItemKey + "_ComboBox"];

                            if (c.Text.Length > 0)
                            {
                                // データ値は数値なので、SelectMaster から該当するテキストを探す

                                if (BaseInfoSelectMaster.Dict.ContainsKey(item.SelectMasterCode))
                                {
                                    List<BaseInfoSelectMaster> im_list = BaseInfoSelectMaster.Dict[item.SelectMasterCode];

                                    foreach (BaseInfoSelectMaster sm in im_list)
                                    {
                                        // 入力された「データ値」と、マスターの「選択値」が一致すれば
                                        // その「選択名」をセット
                                        if (sm.StringValue.ToString().Equals(c.Text))
                                        {
                                            // 選択されたコンボボックスの値は「データ値 = sm.IntValue」となる。

                                            BaseInfo data = new BaseInfo();
                                            data.ItemKey = item.ItemKey;
                                            data.SEQ = 1;
                                            data.Name1 = item.Name1;
                                            data.Name2 = item.Name2;
                                            data.Value = sm.IntValue.ToString();

                                            list.Add(data);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (item.InputType.Equals(4))
                    {
                        if (BaseInfoSelectMaster.Dict.ContainsKey(item.SelectMasterCode))
                        {
                            List<BaseInfoSelectMaster> im_list = BaseInfoSelectMaster.Dict[item.SelectMasterCode];

                            foreach (BaseInfoSelectMaster sm in im_list)
                            {
                                if (page.Controls.ContainsKey(item.ItemKey + "_CheckBox" + sm.IntValue))
                                {
                                    CheckBox c = (CheckBox)page.Controls[item.ItemKey + "_CheckBox" + sm.IntValue];

                                    if (c.Checked)
                                    {
                                        // 選択されたチェックボックスの値は「連番 = sm.IntValue」となり、常に「データ値 = 1」となる。

                                        BaseInfo data = new BaseInfo();
                                        data.ItemKey = item.ItemKey;
                                        data.SEQ = sm.IntValue;
                                        data.Name1 = item.Name1;
                                        data.Name2 = item.Name2;
                                        data.Value = "1";

                                        list.Add(data);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            BaseInfo.Save(this.Pat.Id, code, list);

            this.DataShow();
        }

        private void FormBaseInfo_Load(object sender, EventArgs e)
        {
            this.ctrlAllergy11.Init();
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            this.DataShow();
        }

        private void ShowButton1_Click(object sender, EventArgs e)
        {
            this.DataShow();
        }

        /// <summary>
        /// 全データをクリア
        /// </summary>
        void DataClear()
        {
            foreach (TabPage page in TabControl1.TabPages)
            {
                if (page.Controls.ContainsKey("StaffLabel"))
                {
                    page.Controls["StaffLabel"].Text = "";
                }

                if (page.Controls.ContainsKey("DateTimeLabel"))
                {
                    page.Controls["DateTimeLabel"].Text = "";
                }

                foreach (Control c in page.Controls)
                {
                    if (c is TextBox)
                    {
                        ((TextBox)c).Clear();
                    }
                    else if (c is ComboBox)
                    {
                        ((ComboBox)c).SelectedItem = null;
                    }
                    else if (c is CheckBox)
                    {
                        ((CheckBox)c).Checked = false;
                    }
                }
            }
        }

        void DataShow()
        {
            // 全データをクリア
            this.DataClear();

            // 患者固定情報データ
            StdClass f = BaseInfoFixed.GetData(this.Pat.Id);

            if (f.DataDict.Count > 0)
            {
                if (Dict.StaffDict.ContainsKey(f.GetDataString("更新者")))
                {
                    TabPageFixed.Controls["StaffLabel"].Text = Dict.StaffDict[f.GetDataString("更新者")].Name;
                }

                TabPageFixed.Controls["DateTimeLabel"].Text =
                    DateTimeAgent.DateFormat(f.GetDataString("更新日"), DateTimeAgent.DateFormatKind.LONG) +
                    " " + DateTimeAgent.TimeFormat6(f.GetDataString("更新時間"), 4);

                foreach (BaseInfoFixedMaster fm in BaseInfoFixedMaster.List)
                {
                    // １番目の入力欄
                    if (f.DataDict.ContainsKey(fm.DbColumnName1))
                    {
                        string fval = f.DataDict[fm.DbColumnName1].ToString();

                        if (fm.Kind1 == BaseInfoFixedMaster.InputKind.CheckBox)
                        {
                            // チェックボックスの場合
                            if (TabPageFixed.Controls.ContainsKey(fm.Code + "_CheckBox1") &&
                                fval.Equals("1"))
                            {
                                ((CheckBox)(TabPageFixed.Controls[fm.Code + "_CheckBox1"])).Checked = true;
                            }
                        }
                        else if (fm.Kind1 == BaseInfoFixedMaster.InputKind.ComboBox)
                        {
                            // コンボボックスの場合
                            if (TabPageFixed.Controls.ContainsKey(fm.Code + "_ComboBox1") &&
                                fval.Length > 0)
                            {
                                ((ComboBox)(TabPageFixed.Controls[fm.Code + "_ComboBox1"])).Text = fval;
                            }
                        }
                        else if (fm.Kind1 == BaseInfoFixedMaster.InputKind.TextBox)
                        {
                            // テキストボックスの場合
                            if (TabPageFixed.Controls.ContainsKey(fm.Code + "_TextBox1") &&
                                fval.Length > 0)
                            {
                                ((TextBox)(TabPageFixed.Controls[fm.Code + "_TextBox1"])).Text = fval;
                            }
                        }
                    }

                    // ２番目の入力欄
                    if (fm.DbColumnName2.Length > 0 && fm.Kind2 == BaseInfoFixedMaster.InputKind.TextBox)
                    {
                        ((TextBox)(TabPageFixed.Controls[fm.Code + "_TextBox2"])).Text = f.DataDict[fm.DbColumnName2].ToString();
                    }
                }
            }

            // 感染・禁忌データ
            this.ctrlInfection11.PatSet(this.Pat);
            this.ctrlAllergy11.PatSet(this.Pat);

            // 患者基本情報データ
            Dictionary<string, List<BaseInfo>> dict = BaseInfo.GetDict(this.Pat.Id);

            foreach (List<BaseInfo> list in dict.Values)
            {
                foreach (BaseInfo data in list)
                {
                    // データに相当するマスターを取得する
                    if (BaseInfoItemMaster.GetByItemKey(data.ItemKey).ItemKey.Length > 0)
                    {
                        BaseInfoItemMaster im = BaseInfoItemMaster.GetByItemKey(data.ItemKey);

                        // 該当のタブコードを見つける
                        if (TabControl1.TabPages.ContainsKey("TabPage" + im.TabCode))
                        {
                            TabPage page = TabControl1.TabPages["TabPage" + im.TabCode];

                            if (page.Controls.ContainsKey("StaffLabel") && page.Controls["StaffLabel"].Text.Length == 0)
                            {
                                page.Controls["StaffLabel"].Text = data.UpStaffName;
                            }

                            if (page.Controls.ContainsKey("DateTimeLabel") && page.Controls["DateTimeLabel"].Text.Length == 0)
                            {
                                page.Controls["DateTimeLabel"].Text = data.UpDateTime;
                            }
                            
                            if (im.InputType.Equals(1))
                            {
                                // テキストボックスの場合
                                // 該当のテキストボックスを探して、値そのままをセット

                                if (page.Controls.ContainsKey(im.ItemKey + "_TextBox"))
                                {
                                    page.Controls[im.ItemKey + "_TextBox"].Text = data.Value;
                                }
                            }
                            else if (im.InputType.Equals(3))
                            {
                                // コンボボックスの場合
                                // 該当のコンボボックスを探す

                                if (page.Controls.ContainsKey(im.ItemKey + "_ComboBox"))
                                {
                                    // データ値は数値なので、SelectMaster から該当するテキストを探す

                                    if (BaseInfoSelectMaster.Dict.ContainsKey(im.SelectMasterCode))
                                    {
                                        List<BaseInfoSelectMaster> im_list = BaseInfoSelectMaster.Dict[im.SelectMasterCode];

                                        foreach (BaseInfoSelectMaster sm in im_list)
                                        {
                                            // 入力された「データ値」と、マスターの「選択値」が一致すれば
                                            // その「選択名」をセット
                                            if (sm.IntValue.ToString().Equals(data.Value))
                                            {
                                                page.Controls[im.ItemKey + "_ComboBox"].Text = sm.StringValue;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (im.InputType.Equals(4))
                            {
                                // チェックボックスの場合
                                // 該当のチェックボックスを探して、あればチェックをオンにする

                                if (page.Controls.ContainsKey(im.ItemKey + "_CheckBox" + data.SEQ))
                                {
                                    ((CheckBox)(page.Controls[im.ItemKey + "_CheckBox" + data.SEQ])).Checked = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // これを実施しないと感染症の異常値の色が変わらない
            if (this.TabControl1.SelectedTab == this.TabPageInfection)
            {
                this.ctrlInfection11.DataFormat();
            }
        }
    }
}
