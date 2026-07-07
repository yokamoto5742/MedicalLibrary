using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class FormDPCData2 : StdForm1
    {
        DataSet DSet = new DataSet();

        ContextMenuStrip _Menu1 = new ContextMenuStrip();
        ToolStripMenuItem _Item1 = new ToolStripMenuItem();

        /// <summary>
        /// 編集中の DPCHeader
        /// </summary>
        DPCHeader _Header = new DPCHeader();

        /// <summary>
        /// 登録されたかどうか
        /// </summary>
        bool _Saved = false;


        public FormDPCData2(DPCHeader header)
        {
            InitializeComponent();

            this._Header = header;

            // Detail が空の場合は取得する
            if (header.DetailList.Count == 0)
            {
                this._Header.DetailList = DPCDetail.GetList(header);
            }

            this.PatSet(PatBase.Load(header.Id));
        }

        private void FormDPCData2_Load(object sender, EventArgs e)
        {
            this.Location = new Point(this.Location.X, 20);

            // 診断
            this.DiagPanel.Visible = false;

            int h = 30;

            // 診断パネル作成
            foreach (DPCDiag diag in DPCDiag.List)
            {
                CheckBox cb = new CheckBox();
                cb.Name = "Diag_" + diag.Code;
                cb.Location = new Point(10, h);
                cb.AutoSize = true;
                cb.Text = diag.Name;

                this.DiagPanel.Controls.Add(cb);

                h += 20;
            }

            // 診療科
            this.DeptBox.Items.Add("");

            foreach (DPCDept dept in DPCDept.List)
            {
                this.DeptBox.Items.Add(dept);
            }

            // 医師
            this.DoctorBox1.Init();
            this.DoctorBox2.Init();


            this.DetailPanelInit();
        }

        private void FormDPCData2_Shown(object sender, EventArgs e)
        {
            // ログインユーザーによってボタンの有効・無効をセットする
            switch (LoginUser.QualId)
            {
                case "99":
                    // 情報室
                    this.HeaderSaveButton.Enabled = true;
                    break;

                case "21":
                    // 医事課・診療情報管理士
                    this.HeaderSaveButton.Enabled = true;
                    break;

                case "1":
                    // 医師
                    this.HeaderSaveButton.Enabled = false;

                    // 医師タブを開く
                    this.TabControl1.SelectedIndex = 2;

                    // フォントを大きくする
                    this.FontSet(AppFont.F10);

                    break;

                case "16":
                    // 看護師
                    this.HeaderSaveButton.Enabled = false;

                    // Ns入棟が完成していれば Ns退棟, 未完成なら Ns入棟
                    this.TabControl1.SelectedIndex = this.StatusBox3.Checked ? 4 : 3;

                    // フォントを大きくする
                    this.FontSet(AppFont.F10);

                    break;

                case "17":
                    // 看護助手
                    this.HeaderSaveButton.Enabled = false;

                    // Ns入棟が完成していれば Ns退棟, 未完成なら Ns入棟
                    this.TabControl1.SelectedIndex = this.StatusBox3.Checked ? 4 : 3;

                    // フォントを大きくする
                    this.FontSet(AppFont.F10);

                    break;

                default:
                    this.HeaderSaveButton.Enabled = false;
                    break;
            }

            this.DetailSaveButton1.Enabled = this.DetailPriv(1, LoginUser.QualId);
            this.DetailSaveButton2.Enabled = this.DetailPriv(2, LoginUser.QualId);
            this.DetailSaveButton3.Enabled = this.DetailPriv(3, LoginUser.QualId);
            this.DetailSaveButton4.Enabled = this.DetailPriv(4, LoginUser.QualId);
            this.DetailSaveButton5.Enabled = this.DetailPriv(5, LoginUser.QualId);

            this.StatusBox1.CheckedChanged += new EventHandler(StatusBox_CheckedChanged);
            this.StatusBox2.CheckedChanged += new EventHandler(StatusBox_CheckedChanged);
            this.StatusBox3.CheckedChanged += new EventHandler(StatusBox_CheckedChanged);
            this.StatusBox4.CheckedChanged += new EventHandler(StatusBox_CheckedChanged);
            this.StatusBox5.CheckedChanged += new EventHandler(StatusBox_CheckedChanged);

            if (this._Header.Id.Length > 0)
            {
                this.DataShow(false);
            }

            this.Height = Screen.PrimaryScreen.Bounds.Height - 60;
        }

        /// <summary>
        /// 完成チェックを外すと「登録」ボタンを押せるようにする
        /// （ただし「共通」が完成している場合を除く）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void StatusBox_CheckedChanged(object sender, EventArgs e)
        {
            // 「共通」の権限が無い職種の場合、「共通」が完成していたら終了する
            if (!this.DetailPriv(1, LoginUser.QualId) && this._Header.Status1.Equals("1"))
            {
                return;
            }

            CheckBox box = (CheckBox)sender;

            int i = 0;
            int.TryParse(box.Name.Substring(box.Name.Length - 1), out i);

            if (this.DetailPriv(i, LoginUser.QualId) && !box.Checked)
            {
                if (this.TabControl1.TabPages["TabPage" + i].Controls.ContainsKey("DetailSaveButton" + i))
                {
                    Button button = (Button)this.TabControl1.TabPages["TabPage" + i].Controls["DetailSaveButton" + i];
                    button.Enabled = true;
                }
            }
        }

        bool DetailPriv(int i, string qual)
        {
            bool b = false;

            // 資格によって変える
            switch (qual)
            {
                case "99":
                    // 情報室
                    b = true;
                    break;

                case "21":
                    // 医事課・診療情報管理士
                    b = true;
                    break;

                case "1":
                    // 医師
                    if (i == 2) b = true;
                    break;

                case "16":
                    // 看護師
                    if (i == 3 || i == 4) b = true;
                    break;

                case "17":
                    // 看護助手
                    if (i == 3 || i == 4) b = true;
                    break;

                default:
                    break;
            }

            return b;
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);
        }

        public override void FontSet(AppFont f)
        {
            base.FontSet(f);

            this.stdControlFont11.FontSet(f);
        }

        void DetailPanelInit()
        {
            this.DetailPanel1.Controls.Clear();
            this.DetailPanel2.Controls.Clear();
            this.DetailPanel3.Controls.Clear();
            this.DetailPanel4.Controls.Clear();
            this.DetailPanel5.Controls.Clear();

            this.DetailPanelInit(1);
            this.DetailPanelInit(2);
            this.DetailPanelInit(3);
            this.DetailPanelInit(4);
            this.DetailPanelInit(5);
        }

        void DetailPanelInit(int i)
        {
            Panel panel;
            int x = 10;
            int h = 10;

            // 入力欄の最小 x 位置
            int x1min = 150;
            int x1 = x1min;

            // 前要素の位置
            int h1 = 10;

            // 次要素の位置
            int h2 = 10;

            switch (i)
            {
                case 2:
                    panel = this.DetailPanel2;
                    break;

                case 3:
                    panel = this.DetailPanel3;
                    break;

                case 4:
                    panel = this.DetailPanel4;
                    break;

                case 5:
                    panel = this.DetailPanel5;
                    break;

                default:
                    panel = this.DetailPanel1;
                    break;
            }

            foreach (DPCItem2 obj in DPCItem2.List)
            {
                // タブが異なる場合は飛ばす
                if (!obj.Kind.Equals(i.ToString()))
                {
                    continue;
                }

                // 既に同名の要素がある場合は飛ばす
                if (panel.Controls.ContainsKey(obj.Name))
                {
                    continue;
                }

                // 診断コードの値が入っている場合
                if (obj.Diag.Length > 0)
                {
                    bool b = false;

                    foreach (string s in obj.DiagList)
                    {
                        // 患者の診断に該当するものが有る場合
                        if (this._Header.DiagList.Contains(s))
                        {
                            b = true;
                            break;
                        }
                    }

                    // 患者の診断に該当しない場合は飛ばす
                    if (!b)
                    {
                        continue;
                    }
                }

                if (obj.Location.Equals("2"))
                {
                    // 横に並べる場合は、前の縦位置を使う
                    h = h1;
                }
                else
                {
                    // 縦に並べる場合

                    // 新しい縦位置を使う
                    h = h2;

                    // 横位置はリセット
                    x = 10;
                }

                // マージンがある場合
                x += obj.MarginLeft;
                h += obj.MarginTop;

                // 先頭位置を h1 にセットしておく
                h1 = h;

                Label lb = new Label();
                lb.AutoEllipsis = true;
                lb.Location = new Point(x, h);
                lb.AutoSize = false;
                lb.Size = new Size(200, 20);
                lb.Text = obj.Name;
                lb.TextAlign = ContentAlignment.MiddleLeft;
                lb.Visible = obj.Visible;

                panel.Controls.Add(lb);

                lb.Width = lb.PreferredWidth + 10;
                lb.Height = lb.PreferredHeight + 8;

                // 入力欄の左位置
                x1 = obj.Visible ? (x + lb.Width + 5) : x + 5;

                int maxwidth = 0;

                if (obj.Box.Equals("Label", StringComparison.CurrentCultureIgnoreCase))
                {
                    lb.Name = obj.Code;

                    if (obj.Visible)
                    {
                        x = x1 + 10;
                        h += (lb.Height + 3 >= obj.Height + 4) ? (lb.Height + 3) : (obj.Height + 4);
                    }
                }
                else if (obj.Box.Equals("Button", StringComparison.CurrentCultureIgnoreCase))
                {
                    lb.Name = obj.Code;
                    lb.TextAlign = ContentAlignment.MiddleCenter;
                    lb.BorderStyle = BorderStyle.FixedSingle;
                    lb.FlatStyle = FlatStyle.Popup;
                    lb.BackColor = Color.LightYellow;
                    lb.MouseHover += new EventHandler(lb_MouseHover);
                    lb.MouseLeave += new EventHandler(lb_MouseLeave);

                    if (obj.Visible)
                    {
                        x = x1 + 10;
                        h += (lb.Height + 3 >= obj.Height + 4) ? (lb.Height + 3) : (obj.Height + 4);
                    }
                }
                else if (obj.Box.Equals("ComboBox", StringComparison.CurrentCultureIgnoreCase))
                {
                    // 入力欄の左位置。最低でも x1min 以上。
                    if (x1 < x1min) x1 = x1min;

                    DPCComboBox box = new DPCComboBox();

                    if (obj.DropDownStyle.Equals("Simple", StringComparison.CurrentCultureIgnoreCase))
                    {
                        box.DropDownStyle = ComboBoxStyle.Simple;
                    }
                    else if (obj.DropDownStyle.Equals("DropDown", StringComparison.CurrentCultureIgnoreCase))
                    {
                        box.DropDownStyle = ComboBoxStyle.DropDown;
                    }
                    else
                    {
                        box.DropDownStyle = ComboBoxStyle.DropDownList;
                    }

                    box.Location = new Point(x1, h);
                    box.Size = new Size(obj.Width, obj.Height);
                    box.Name = obj.Code;
                    box.Visible = obj.Visible;

                    box.Items.Add(new DPCSubItem2());

                    foreach (DPCSubItem2 sub in obj.SubItemList)
                    {
                        box.Items.Add(sub);

                        if (maxwidth < Encoding.GetEncoding("Shift_JIS").GetByteCount(sub.ToString()))
                        {
                            maxwidth = Encoding.GetEncoding("Shift_JIS").GetByteCount(sub.ToString());
                        }
                    }

                    box.Width = (30 + maxwidth * 7 > obj.Width) ? (30 + maxwidth * 7) : obj.Width;

                    panel.Controls.Add(box);

                    x = obj.Visible ? (box.Location.X + box.Width + 15) : (box.Location.X + 15);
                    h += obj.Visible ? (obj.Height + 4) : 0;
                }
                else if (obj.Box.Equals("RadioButton", StringComparison.CurrentCultureIgnoreCase))
                {
                    // 入力欄の左位置。最低でも x1min 以上。
                    if (x1 < x1min) x1 = x1min;

                    DPCRadioPanel p = new DPCRadioPanel();
                    p.Location = new Point(x1, h);
                    p.Size = new Size(panel.Width - x1 - 30, 100);
                    p.Name = obj.Code;
                    p.Visible = obj.Visible;
                    p.BorderStyle = BorderStyle.FixedSingle;

                    foreach (DPCSubItem2 sub in obj.SubItemList)
                    {
                        RadioButton button = new RadioButton();
                        button.AutoSize = true;
                        button.Text = sub.ToString();

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

                    int xx = 3;
                    int hh = 3;
                    int cc = 1;

                    foreach (DPCSubItem2 sub in obj.SubItemList)
                    {
                        RadioButton button = new RadioButton();
                        button.Location = new Point(xx, hh);
                        button.AutoSize = true;
                        button.Name = obj.Code + "_" + sub.Value;
                        button.Text = sub.ToString();

                        if (cc < c)
                        {
                            cc++;
                            xx += maxwidth;
                        }
                        else
                        {
                            cc = 1;
                            xx = 3;
                            hh += obj.Height + 1;
                        }

                        p.Controls.Add(button);
                    }

                    if (xx > 3)
                    {
                        hh += 22;
                    }

                    p.Height = hh;
                    p.Width = p.PreferredSize.Width + 30;

                    panel.Controls.Add(p);

                    h += obj.Visible ? (hh + 3) : 0;
                    x = obj.Visible ? (p.Location.X + p.Width + 15) : (p.Location.X + 15);
                }
                else if (obj.Box.Equals("CheckBox", StringComparison.CurrentCultureIgnoreCase))
                {
                    // 入力欄の左位置。最低でも x1min 以上。
                    if (x1 < x1min) x1 = x1min;

                    DPCCheckPanel p = new DPCCheckPanel();
                    p.Location = new Point(x1, h);
                    p.Size = new Size(panel.Width - x1 - 30, 100);
                    p.Name = obj.Code;
                    p.Visible = obj.Visible;
                    p.BorderStyle = BorderStyle.FixedSingle;

                    foreach (DPCSubItem2 sub in obj.SubItemList)
                    {
                        CheckBox button = new CheckBox();
                        button.AutoSize = true;
                        button.Text = sub.ToString();

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

                    int xx = 3;
                    int hh = 3;
                    int cc = 1;

                    foreach (DPCSubItem2 sub in obj.SubItemList)
                    {
                        CheckBox button = new CheckBox();
                        button.Location = new Point(xx, hh);
                        button.AutoSize = true;
                        button.Name = obj.Code + "_" + sub.Value;
                        button.Text = sub.ToString();

                        if (cc < c)
                        {
                            cc++;
                            xx += maxwidth;
                        }
                        else
                        {
                            cc = 1;
                            xx = 3;
                            hh += obj.Height + 1;
                        }

                        p.Controls.Add(button);
                    }

                    if (xx > 3)
                    {
                        hh += obj.Height + 1;
                    }

                    p.Height = hh;
                    p.Width = p.PreferredSize.Width + 30;

                    panel.Controls.Add(p);

                    h += obj.Visible ? (hh + 3) : 0;
                    x = obj.Visible ? (p.Location.X + p.Width + 15) : (p.Location.X + 15);
                }
                else if (obj.Box.Equals("AlphaBox", StringComparison.CurrentCultureIgnoreCase))
                {
                    // 入力欄の左位置。最低でも x1min 以上。
                    if (x1 < x1min) x1 = x1min;

                    CtrlAlphaBox1 box = new CtrlAlphaBox1();
                    box.Location = new Point(x1, h);
                    box.Size = new Size(obj.Width, obj.Height);
                    box.MaxLength = obj.MaxLength;
                    box.Name = obj.Code;
                    box.Visible = obj.Visible;
                    box.ReadOnly = obj.ReadOnly;
                    box.KeyDown += new KeyEventHandler(box_KeyDown);

                    box.Pattern = obj.Pattern;
                    box.Sample = obj.Sample;

                    panel.Controls.Add(box);

                    h += obj.Visible ? (obj.Height + 4) : 0;
                    x = obj.Visible ? (box.Location.X + box.Width + 15) : (box.Location.X + 15);
                }
                else if (obj.Box.Equals("NumBox", StringComparison.CurrentCultureIgnoreCase))
                {
                    // 入力欄の左位置。最低でも x1min 以上。
                    if (x1 < x1min) x1 = x1min;

                    CtrlNumBox1 box = new CtrlNumBox1();
                    box.Location = new Point(x1, h);
                    box.Size = new Size(obj.Width, obj.Height);
                    box.MaxLength = obj.MaxLength;
                    box.Name = obj.Code;
                    box.Visible = obj.Visible;
                    box.ReadOnly = obj.ReadOnly;
                    box.KeyDown += new KeyEventHandler(box_KeyDown);

                    box.Pattern = obj.Pattern;
                    box.Sample = obj.Sample;
                    box.MaxValue = obj.MaxValue;
                    box.MinValue = obj.MinValue;

                    panel.Controls.Add(box);

                    h += obj.Visible ? (obj.Height + 4) : 0;
                    x = obj.Visible ? (box.Location.X + box.Width + 15) : (box.Location.X + 15);
                }
                else if (obj.Box.Equals("DateBox", StringComparison.CurrentCultureIgnoreCase))
                {
                    // 入力欄の左位置。最低でも x1min 以上。
                    if (x1 < x1min) x1 = x1min;

                    CtrlDateBox2 box = new CtrlDateBox2();
                    box.Location = new Point(x1, h);
                    box.Size = new Size(obj.Width, obj.Height);
                    box.MaxLength = 8;
                    box.Name = obj.Code;
                    box.Visible = obj.Visible;
                    box.ReadOnly = obj.ReadOnly;
                    box.KeyDown += new KeyEventHandler(box_KeyDown);

                    panel.Controls.Add(box);

                    h += obj.Visible ? (obj.Height + 4) : 0;
                    x = obj.Visible ? (box.Location.X + box.Width + 15) : (box.Location.X + 15);
                }
                else if (obj.Box.Equals("TextBox", StringComparison.CurrentCultureIgnoreCase))
                {
                    // 入力欄の左位置。最低でも x1min 以上。
                    if (x1 < x1min) x1 = x1min;

                    CtrlTextBox1 box = new CtrlTextBox1();
                    box.Location = new Point(x1, h);
                    box.Size = new Size(obj.Width, obj.Height);
                    box.Multiline = obj.Multiline;
                    box.MaxLength = obj.MaxLength;
                    box.Name = obj.Code;

                    if (obj.Multiline) box.ScrollBars = ScrollBars.Vertical;

                    switch (obj.ImeMode)
                    {
                        case "Alpha":
                            box.ImeMode = ImeMode.Alpha;
                            break;

                        case "Disable":
                            box.ImeMode = ImeMode.Disable;
                            break;

                        case "NoControl":
                            box.ImeMode = ImeMode.NoControl;
                            break;

                        case "On":
                            box.ImeMode = ImeMode.On;
                            break;

                        case "Off":
                            box.ImeMode = ImeMode.Off;
                            break;

                        default:
                            box.ImeMode = ImeMode.Hiragana;
                            break;
                    }

                    box.Visible = obj.Visible;
                    box.ReadOnly = obj.ReadOnly;

                    if (!obj.Multiline) box.KeyDown += new KeyEventHandler(box_KeyDown);

                    box.Pattern = obj.Pattern;
                    box.Sample = obj.Sample;

                    panel.Controls.Add(box);

                    h += obj.Visible ? (obj.Height + 4) : 0;
                    x = obj.Visible ? (box.Location.X + box.Width + 15) : (box.Location.X + 15);
                }

                // 最下方の位置をセット
                if (h > h2) h2 = h;
            }

            // パネルの高さを調整するためダミーラベルをセット
            Label lbd = new Label();
            lbd.AutoSize = false;
            lbd.Location = new Point(10, h);
            lbd.Size = new Size(100, 20);
            lbd.Text = " ";

            panel.Controls.Add(lbd);


            // 特殊な計算があるもの

            foreach (Control c in panel.Controls)
            {
                // 難病
                if (c is TextBox && (c.Name.Equals("d295") || c.Name.Equals("d297")))
                {
                    c.KeyDown += new KeyEventHandler(FindDiagNan);
                }

                // 病名
                if (c is TextBox &&
                    (c.Name.Equals("d1") || c.Name.Equals("d8") || c.Name.Equals("d15") || c.Name.Equals("d22") ||
                    c.Name.Equals("d29") || c.Name.Equals("d36") || c.Name.Equals("d43") || c.Name.Equals("d50") ||
                    c.Name.Equals("d198") || c.Name.Equals("d206") || c.Name.Equals("d214") || c.Name.Equals("d222") ||
                    c.Name.Equals("d230") || c.Name.Equals("d238") ||
                    c.Name.Equals("d57") || c.Name.Equals("d64") || c.Name.Equals("d71") ||
                    c.Name.Equals("d111") || c.Name.Equals("d246") || c.Name.Equals("d254") || c.Name.Equals("d262") ||
                    c.Name.Equals("d270") || c.Name.Equals("d278") || c.Name.Equals("d286")))
                {
                    c.KeyDown += new KeyEventHandler(FindDiag);
                }

                // 手術
                if (c is TextBox &&
                    (c.Name.Equals("e1") || c.Name.Equals("e2") || c.Name.Equals("e4") ||
                    c.Name.Equals("e14") || c.Name.Equals("e15") || c.Name.Equals("e17") ||
                    c.Name.Equals("e27") || c.Name.Equals("e28") || c.Name.Equals("e30") ||
                    c.Name.Equals("e40") || c.Name.Equals("e41") || c.Name.Equals("e43") ||
                    c.Name.Equals("e53") || c.Name.Equals("e54") || c.Name.Equals("e56")))
                {
                    c.KeyDown += new KeyEventHandler(FindOpe);
                }

                // 再入院理由種別
                if (c.Name.Equals("c35") && c is DPCComboBox)
                {
                    ((ComboBox)c).SelectedIndexChanged += new EventHandler(RepeatReason1);
                }

                // 再転棟理由種別
                if (c.Name.Equals("c38") && c is DPCComboBox)
                {
                    ((ComboBox)c).SelectedIndexChanged += new EventHandler(RepeatReason2);
                }

                // がんの Stage 分類
                if (c.Name.Equals("d141") && c is DPCComboBox)
                {
                    ((ComboBox)c).SelectedIndexChanged += new EventHandler(CancerStage);
                }

                // がん取扱い規約
                if (c.Name.StartsWith("lb2_1") && Regex.IsMatch(c.Name, @"lb2_1[a-z]{1}"))
                {
                    if (c is Label)
                    {
                        c.Click += new EventHandler(CancerDoc);
                    }
                }

                // 肺炎重症度
                if (c.Name.StartsWith("d167") && Regex.IsMatch(c.Name, @"d167[a-z]{1}"))
                {
                    if (c is DPCRadioPanel)
                    {
                        foreach (Control cc in c.Controls)
                        {
                            if (cc is RadioButton)
                            {
                                ((RadioButton)cc).CheckedChanged += new EventHandler(CalcHaien);
                            }
                        }
                    }
                    else if (c is DPCComboBox)
                    {
                        ((ComboBox)c).SelectedIndexChanged += new EventHandler(CalcHaien);
                    }
                }

                // 肝硬変分類
                if (c.Name.StartsWith("d106") && Regex.IsMatch(c.Name, @"d106[a-z]{1}"))
                {
                    if (c is DPCRadioPanel)
                    {
                        foreach (Control cc in c.Controls)
                        {
                            if (cc is RadioButton)
                            {
                                ((RadioButton)cc).CheckedChanged += new EventHandler(CalcKankouhen);
                            }
                        }
                    }
                    else if (c is DPCComboBox)
                    {
                        ((ComboBox)c).SelectedIndexChanged += new EventHandler(CalcKankouhen);
                    }
                }

                // 膵炎重症度
                if (c.Name.StartsWith("d122") && Regex.IsMatch(c.Name, @"d122[a-z]{1}"))
                {
                    if (c is DPCCheckPanel)
                    {
                        foreach (Control cc in c.Controls)
                        {
                            if (cc is CheckBox)
                            {
                                ((CheckBox)cc).CheckedChanged += new EventHandler(CalcSuien);
                            }
                        }
                    }
                    else if (c is DPCComboBox)
                    {
                        ((ComboBox)c).SelectedIndexChanged += new EventHandler(CalcSuien);
                    }
                }

                // 敗血症１回目当日
                if (c.Name.StartsWith("d355") && Regex.IsMatch(c.Name, @"d355[a-z]{1}"))
                {
                    if (c is DPCRadioPanel)
                    {
                        foreach (Control cc in c.Controls)
                        {
                            if (cc is RadioButton)
                            {
                                ((RadioButton)cc).CheckedChanged += new EventHandler(CalcHaiketsu11);
                            }
                        }
                    }
                    else if (c is DPCComboBox)
                    {
                        ((ComboBox)c).SelectedIndexChanged += new EventHandler(CalcHaiketsu11);
                    }
                }

                // 敗血症１回目翌日
                if (c.Name.StartsWith("d356") && Regex.IsMatch(c.Name, @"d356[a-z]{1}"))
                {
                    if (c is DPCRadioPanel)
                    {
                        foreach (Control cc in c.Controls)
                        {
                            if (cc is RadioButton)
                            {
                                ((RadioButton)cc).CheckedChanged += new EventHandler(CalcHaiketsu12);
                            }
                        }
                    }
                    else if (c is DPCComboBox)
                    {
                        ((ComboBox)c).SelectedIndexChanged += new EventHandler(CalcHaiketsu12);
                    }
                }

                // 敗血症２回目当日
                if (c.Name.StartsWith("d359") && Regex.IsMatch(c.Name, @"d359[a-z]{1}"))
                {
                    if (c is DPCRadioPanel)
                    {
                        foreach (Control cc in c.Controls)
                        {
                            if (cc is RadioButton)
                            {
                                ((RadioButton)cc).CheckedChanged += new EventHandler(CalcHaiketsu21);
                            }
                        }
                    }
                    else if (c is DPCComboBox)
                    {
                        ((ComboBox)c).SelectedIndexChanged += new EventHandler(CalcHaiketsu21);
                    }
                }

                // 敗血症２回目翌日
                if (c.Name.StartsWith("d360") && Regex.IsMatch(c.Name, @"d360[a-z]{1}"))
                {
                    if (c is DPCRadioPanel)
                    {
                        foreach (Control cc in c.Controls)
                        {
                            if (cc is RadioButton)
                            {
                                ((RadioButton)cc).CheckedChanged += new EventHandler(CalcHaiketsu22);
                            }
                        }
                    }
                    else if (c is DPCComboBox)
                    {
                        ((ComboBox)c).SelectedIndexChanged += new EventHandler(CalcHaiketsu22);
                    }
                }

                // 入棟時ADL
                if (c.Name.StartsWith("d100") && Regex.IsMatch(c.Name, @"d100[a-z]{1}"))
                {
                    if (c is DPCRadioPanel)
                    {
                        foreach (Control cc in c.Controls)
                        {
                            if (cc is RadioButton)
                            {
                                ((RadioButton)cc).CheckedChanged += new EventHandler(CalcADL1);
                            }
                        }
                    }
                    else if (c is DPCComboBox)
                    {
                        ((ComboBox)c).SelectedIndexChanged += new EventHandler(CalcADL1);
                    }
                }

                // 入棟時ADLをすべて自立
                if (c.Name.Equals("lb3a_all"))
                {
                    if (c is Label)
                    {
                        c.Click += new EventHandler(SetADL1);
                    }
                }

                // 入棟時ADLをクリップボードにコピー
                if (c.Name.Equals("lb3a_clip"))
                {
                    if (c is Label)
                    {
                        c.Click += new EventHandler(ClipADL1);
                    }
                }

                // 退棟時ADL
                if (c.Name.StartsWith("d119") && Regex.IsMatch(c.Name, @"d119[a-z]{1}"))
                {
                    if (c is DPCRadioPanel)
                    {
                        foreach (Control cc in c.Controls)
                        {
                            if (cc is RadioButton)
                            {
                                ((RadioButton)cc).CheckedChanged += new EventHandler(CalcADL2);
                            }
                        }
                    }
                    else if (c is DPCComboBox)
                    {
                        ((ComboBox)c).SelectedIndexChanged += new EventHandler(CalcADL2);
                    }
                }

                // 退棟時JCSを入棟時コピー
                if (c.Name.Equals("lb4j_copy"))
                {
                    if (c is Label)
                    {
                        c.Click += new EventHandler(CopyJCS2);
                    }
                }

                // 退棟時ADLをすべて自立
                if (c.Name.Equals("lb4a_all"))
                {
                    if (c is Label)
                    {
                        c.Click += new EventHandler(SetADL2);
                    }
                }

                // 退棟時ADLをクリップボードにコピー
                if (c.Name.Equals("lb4a_clip"))
                {
                    if (c is Label)
                    {
                        c.Click += new EventHandler(ClipADL2);
                    }
                }

                // 退棟時ADLを入棟時コピー
                if (c.Name.Equals("lb4a_copy"))
                {
                    if (c is Label)
                    {
                        c.Click += new EventHandler(CopyADL2);
                    }
                }

                // 入棟時要介護情報
                if (c.Name.StartsWith("d391") && Regex.IsMatch(c.Name, @"d391[a-z]{1}"))
                {
                    if (c is DPCRadioPanel)
                    {
                        foreach (Control cc in c.Controls)
                        {
                            if (cc is RadioButton)
                            {
                                ((RadioButton)cc).CheckedChanged += new EventHandler(CalcKaigo1);
                            }
                        }
                    }
                    else if (c is DPCComboBox)
                    {
                        ((ComboBox)c).SelectedIndexChanged += new EventHandler(CalcKaigo1);
                    }
                }

                // 入棟時要介護情報をすべて無し
                if (c.Name.Equals("lb3k_all"))
                {
                    if (c is Label)
                    {
                        c.Click += new EventHandler(SetKaigo1);
                    }
                }

                // 退棟時要介護情報
                if (c.Name.StartsWith("d392") && Regex.IsMatch(c.Name, @"d392[a-z]{1}"))
                {
                    if (c is DPCRadioPanel)
                    {
                        foreach (Control cc in c.Controls)
                        {
                            if (cc is RadioButton)
                            {
                                ((RadioButton)cc).CheckedChanged += new EventHandler(CalcKaigo2);
                            }
                        }
                    }
                    else if (c is DPCComboBox)
                    {
                        ((ComboBox)c).SelectedIndexChanged += new EventHandler(CalcKaigo2);
                    }
                }

                // 退棟時要介護情報をすべて無し
                if (c.Name.Equals("lb4k_all"))
                {
                    if (c is Label)
                    {
                        c.Click += new EventHandler(SetKaigo2);
                    }
                }

                // 退棟時要介護情報を入棟時コピー
                if (c.Name.Equals("lb4k_copy"))
                {
                    if (c is Label)
                    {
                        c.Click += new EventHandler(CopyKaigo2);
                    }
                }

                // 身長・体重
                if (c.Name.Equals("d162") || c.Name.Equals("d163"))
                {
                    if (c is TextBox)
                    {
                        c.KeyDown += new KeyEventHandler(GetHeightWeight);
                    }
                }

                // 身長・体重測定不能
                if (c.Name.Equals("lb3hw_none"))
                {
                    if (c is Label)
                    {
                        c.Click += new EventHandler(SetHeightWeightNone);
                    }
                }

                // 喫煙指数
                if (c.Name.StartsWith("d118") && Regex.IsMatch(c.Name, @"d118[a-z]{1}"))
                {
                    if (c is TextBox)
                    {
                        ((TextBox)c).TextChanged += new EventHandler(CalcSmoking);
                    }
                }

                // 喫煙不明
                if (c.Name.Equals("lb3sm_none"))
                {
                    if (c is Label)
                    {
                        c.Click += new EventHandler(SetSmokingNone);
                    }
                }

                // 入院時JCS
                if (c.Name.StartsWith("f13") && Regex.IsMatch(c.Name, @"f13[a-z]{1}"))
                {
                    if (c is DPCComboBox)
                    {
                        ((ComboBox)c).SelectedIndexChanged += new EventHandler(CalcJCS1);
                    }
                }

                // 退院時JCS
                if (c.Name.StartsWith("f128") && Regex.IsMatch(c.Name, @"f128[a-z]{1}"))
                {
                    if (c is DPCComboBox)
                    {
                        ((ComboBox)c).SelectedIndexChanged += new EventHandler(CalcJCS2);
                    }
                }
            }
        }

        void lb_MouseHover(object sender, EventArgs e)
        {
            Label lb = (Label)sender;
            lb.BackColor = Color.LightCyan;
        }

        void lb_MouseLeave(object sender, EventArgs e)
        {
            Label lb = (Label)sender;
            lb.BackColor = Color.LightYellow;
        }

        void box_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        /// <summary>
        /// 指定した名前の要素を取得する
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Control GetElement(string name)
        {
            Control c = null;

            for (int i = 1; i <= 5; i++)
            {
                Panel panel;

                switch (i)
                {
                    case 2:
                        panel = this.DetailPanel2;
                        break;

                    case 3:
                        panel = this.DetailPanel3;
                        break;

                    case 4:
                        panel = this.DetailPanel4;
                        break;

                    case 5:
                        panel = this.DetailPanel5;
                        break;

                    default:
                        panel = this.DetailPanel1;
                        break;
                }

                foreach (Control cc in panel.Controls)
                {
                    if (cc.Name.Equals(name))
                    {
                        c = cc;
                        break;
                    }
                }

                if (c != null) break;
            }

            return c;
        }

        /// <summary>
        /// 指定した名前で始まる要素を取得する
        /// </summary>
        /// <param name="name"></param>
        /// <param name="self">true: name 自身を含む</param>
        /// <returns></returns>
        List<Control> GetElementsStartsWith(string name, bool self = false)
        {
            List<Control> list = new List<Control>();

            for (int i = 1; i <= 5; i++)
            {
                Panel panel;

                switch (i)
                {
                    case 2:
                        panel = this.DetailPanel2;
                        break;

                    case 3:
                        panel = this.DetailPanel3;
                        break;

                    case 4:
                        panel = this.DetailPanel4;
                        break;

                    case 5:
                        panel = this.DetailPanel5;
                        break;

                    default:
                        panel = this.DetailPanel1;
                        break;
                }

                foreach (Control cc in panel.Controls)
                {
                    // self == false で name 自身の場合は飛ばす
                    if (!self && cc.Name.Equals(name))
                    {
                        continue;
                    }

                    if (cc.Name.StartsWith(name))
                    {
                        list.Add(cc);
                    }
                }
            }

            return list;
        }

        #region 特殊計算

        /// <summary>
        /// 連結値を求める
        /// ADL, 肺炎重症度 など
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        string ConcatVals(string code)
        {
            string n = "";

            Control c = this.GetElement(code);

            if (c == null)
            {
                return n;
            }

            foreach (Control cc in this.GetElementsStartsWith(code, false))
            {
                if (!Regex.IsMatch(cc.Name, code + @"[a-z]{1}"))
                {
                    continue;
                }

                if (cc is DPCRadioPanel)
                {
                    if (((DPCRadioPanel)cc).Value.Length > 0)
                    {
                        n += ((DPCRadioPanel)cc).Value;
                    }
                    else
                    {
                        n += " ";
                    }
                }
                else if (cc is DPCComboBox)
                {
                    if (((DPCComboBox)cc).Value.Length > 0)
                    {
                        n += ((DPCComboBox)cc).Value;
                    }
                    else
                    {
                        n += " ";
                    }
                }
            }

            return n;
        }

        /// <summary>
        /// 病名グループ
        /// </summary>
        class DiagGroup
        {
            public string Name = "";

            public string Code1 = "";

            public string Code2 = "";

            public string Code3 = "";

            public string Code4 = "";

            public string Code5 = "";

            public DiagGroup(string name, string code1, string code2, string code3, string code4, string code5)
            {
                this.Name = name;
                this.Code1 = code1;
                this.Code2 = code2;
                this.Code3 = code3;
                this.Code4 = code4;
                this.Code5 = code5;
            }

            static List<DiagGroup> _List = new List<DiagGroup>();

            public static List<DiagGroup> List
            {
                get
                {
                    if (_List.Count == 0)
                    {
                        // 主傷病
                        _List.Add(new DiagGroup("主傷病名", "d1", "d2", "d4", "d171", "d172"));

                        // 入院契機
                        _List.Add(new DiagGroup("入院契機病名", "d8", "d9", "d11", "d173", "d174"));

                        // 医療資源1,2
                        _List.Add(new DiagGroup("医療資源1", "d15", "d16", "d18", "d175", "d176"));
                        _List.Add(new DiagGroup("医療資源2", "d22", "d23", "d25", "d177", "d178"));

                        // 入院時併存症
                        _List.Add(new DiagGroup("入院時併存症1", "d29", "d30", "d32", "d179", "d180"));
                        _List.Add(new DiagGroup("入院時併存症2", "d36", "d37", "d39", "d181", "d182"));
                        _List.Add(new DiagGroup("入院時併存症3", "d43", "d44", "d46", "d183", "d184"));
                        _List.Add(new DiagGroup("入院時併存症4", "d50", "d51", "d53", "d185", "d186"));
                        _List.Add(new DiagGroup("入院時併存症5", "d198", "d199", "d200", "d204", "d205"));
                        _List.Add(new DiagGroup("入院時併存症6", "d206", "d207", "d208", "d212", "d213"));
                        _List.Add(new DiagGroup("入院時併存症7", "d214", "d215", "d216", "d220", "d221"));
                        _List.Add(new DiagGroup("入院時併存症8", "d222", "d223", "d224", "d228", "d229"));
                        _List.Add(new DiagGroup("入院時併存症9", "d230", "d231", "d232", "d236", "d237"));
                        _List.Add(new DiagGroup("入院時併存症10", "d238", "d239", "d240", "d244", "d245"));

                        // 入院後発症
                        _List.Add(new DiagGroup("入院後発症1", "d57", "d58", "d60", "d187", "d188"));
                        _List.Add(new DiagGroup("入院後発症2", "d64", "d65", "d67", "d189", "d190"));
                        _List.Add(new DiagGroup("入院後発症3", "d71", "d72", "d74", "d191", "d192"));
                        _List.Add(new DiagGroup("入院後発症4", "d111", "d112", "d114", "d193", "d194"));
                        _List.Add(new DiagGroup("入院後発症5", "d246", "d247", "d248", "d252", "d253"));
                        _List.Add(new DiagGroup("入院後発症6", "d254", "d255", "d256", "d260", "d261"));
                        _List.Add(new DiagGroup("入院後発症7", "d262", "d263", "d264", "d268", "d269"));
                        _List.Add(new DiagGroup("入院後発症8", "d270", "d271", "d272", "d276", "d277"));
                        _List.Add(new DiagGroup("入院後発症9", "d278", "d279", "d280", "d284", "d285"));
                        _List.Add(new DiagGroup("入院後発症10", "d286", "d287", "d288", "d292", "d293"));
                    }

                    return _List;
                }
            }

            public static DiagGroup GetDataByCode1(string code1)
            {
                DiagGroup obj = new DiagGroup("", "", "", "", "", "");

                foreach (DiagGroup g in List)
                {
                    if (g.Code1.Equals(code1))
                    {
                        obj = g;
                        break;
                    }
                }

                return obj;
            }

            public static DiagGroup GetDataByName(string name)
            {
                DiagGroup obj = new DiagGroup("", "", "", "", "", "");

                foreach (DiagGroup g in List)
                {
                    if (g.Name.Equals(name))
                    {
                        obj = g;
                        break;
                    }
                }

                return obj;
            }
        }

        /// <summary>
        /// 手術グループ
        /// </summary>
        class OpeGroup
        {
            /// <summary>
            /// グループ名
            /// </summary>
            public string Name = "";

            /// <summary>
            /// 手術名
            /// </summary>
            public string Code1 = "";

            /// <summary>
            /// STEM7
            /// </summary>
            public string Code2 = "";

            /// <summary>
            /// Kコード
            /// </summary>
            public string Code3 = "";

            /// <summary>
            /// 開始日
            /// </summary>
            public string Code4 = "";

            /// <summary>
            /// 回数
            /// </summary>
            public string Code5 = "";

            /// <summary>
            /// 麻酔
            /// </summary>
            public string Code6 = "";

            /// <summary>
            /// 左右
            /// </summary>
            public string Code7 = "";

            public OpeGroup(string name, string code1, string code2, string code3, string code4 = "", string code5 = "", string code6 = "", string code7 = "")
            {
                this.Name = name;
                this.Code1 = code1;
                this.Code2 = code2;
                this.Code3 = code3;
                this.Code4 = code4;
                this.Code5 = code5;
                this.Code6 = code6;
                this.Code7 = code7;
            }

            static List<OpeGroup> _List = new List<OpeGroup>();

            public static List<OpeGroup> List
            {
                get
                {
                    if (_List.Count == 0)
                    {
                        _List.Add(new OpeGroup("手術1", "e1", "e2", "e4", "e5", "e7", "e10", "e66"));
                        _List.Add(new OpeGroup("手術2", "e14", "e15", "e17", "e18", "e20", "e23", "e67"));
                        _List.Add(new OpeGroup("手術3", "e27", "e28", "e30", "e31", "e33", "e36", "e68"));
                        _List.Add(new OpeGroup("手術4", "e40", "e41", "e43", "e44", "e46", "e49", "e69"));
                        _List.Add(new OpeGroup("手術5", "e53", "e54", "e56", "e57", "e59", "e62", "e70"));
                    }

                    return _List;
                }
            }

            public static OpeGroup GetDataByCode(string code)
            {
                OpeGroup obj = new OpeGroup("", "", "", "");

                foreach (OpeGroup g in List)
                {
                    if (g.Code1.Equals(code) || g.Code2.Equals(code) || g.Code3.Equals(code))
                    {
                        obj = g;
                        break;
                    }
                }

                return obj;
            }

            public static OpeGroup GetDataByName(string name)
            {
                OpeGroup obj = new OpeGroup("", "", "", "");

                foreach (OpeGroup g in List)
                {
                    if (g.Name.Equals(name))
                    {
                        obj = g;
                        break;
                    }
                }

                return obj;
            }
        }

        /// <summary>
        /// 難病検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FindDiagNan(object sender, KeyEventArgs e)
        {
            if (!(sender is TextBox)) return;

            TextBox box = (TextBox)sender;

            if (e.KeyCode == Keys.F3)
            {
                // 難病検索
                FormDiagNan f = new FormDiagNan(box.Text);

                if (f.ShowDialog() == DialogResult.OK)
                {
                    box.Text = f.SelectedDiagNan.Code;
                }

                f.Dispose();
            }
        }

        /// <summary>
        /// 病名検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FindDiag(object sender, KeyEventArgs e)
        {
            if (!(sender is TextBox)) return;

            TextBox box = (TextBox)sender;

            if (e.KeyCode == Keys.F3)
            {
                // 病名検索
                DiagGroup g = DiagGroup.GetDataByCode1(box.Name);

                FormFindDiag f = new FormFindDiag(box.Text);

                if (f.ShowDialog() == DialogResult.OK)
                {
                    if (this.GetElement(g.Code1) != null) this.GetElement(g.Code1).Text = f.Diag.DiagName;
                    if (this.GetElement(g.Code2) != null) this.GetElement(g.Code2).Text = f.Diag.ICDCode1;
                    if (this.GetElement(g.Code3) != null) this.GetElement(g.Code3).Text = f.Diag.DiagCode;
                    if (this.GetElement(g.Code4) != null) this.GetElement(g.Code4).Text = f.Diag.MainName;
                    if (this.GetElement(g.Code5) != null) this.GetElement(g.Code5).Text = f.Diag.PreSuffixString("");
                }
            }
            else if (e.KeyCode == Keys.F5)
            {
                // 病名取込み
                List<Diag> list1 = Diag.GetList(this.Pat.Id);

                if (list1.Count == 0)
                {
                    MessageBox.Show("病名が登録されていません");
                    return;
                }

                List<DiagDPC> list2 = DiagDPC.GetList(this.Pat.Id, this._Header.AdmDate);

                if (list2.Count == 0)
                {
                    MessageBox.Show("DPC病名がチェックされていません");
                    return;
                }

                // すでに入力されているデータがあるか
                bool b = false;

                foreach (DiagGroup g in DiagGroup.List)
                {
                    if (this.GetElement(g.Code1) != null && this.GetElement(g.Code1).Text.Length > 0) b = true;
                    if (this.GetElement(g.Code2) != null && this.GetElement(g.Code2).Text.Length > 0) b = true;
                    if (this.GetElement(g.Code3) != null && this.GetElement(g.Code3).Text.Length > 0) b = true;
                    if (this.GetElement(g.Code4) != null && this.GetElement(g.Code4).Text.Length > 0) b = true;
                    if (this.GetElement(g.Code5) != null && this.GetElement(g.Code5).Text.Length > 0) b = true;

                    if (b) break;
                }

                DialogResult dr = DialogResult.Cancel;

                string s = "すでに入力されている箇所があります。上書きしますか？" + Environment.NewLine + Environment.NewLine +
                    " Yes … 上書きする" + Environment.NewLine +
                    " No … 空欄のみ上書きする" + Environment.NewLine +
                    " Cancel … キャンセル";

                if (b)
                {
                    dr = MessageBox.Show(s, "確認", MessageBoxButtons.YesNoCancel);

                    if (dr == DialogResult.Cancel) return;
                }


                // 併存症の番号
                int i = 1;

                // 入院後発症の番号
                int j = 1;

                foreach (DiagDPC obj in list2)
                {
                    if (obj.DeleteFlg) continue;

                    Diag diag = null;

                    foreach (Diag d in list1)
                    {
                        if (d.DeleteFlg) continue;

                        if (d.SEQ.Equals(obj.SEQ))
                        {
                            diag = d;
                            break;
                        }
                    }

                    if (diag == null) continue;

                    // 主傷病名
                    if (obj.MainFlg)
                    {
                        DiagGroup g = DiagGroup.GetDataByName("主傷病名");

                        if (this.GetElement(g.Code1) != null && (dr == DialogResult.Yes || this.GetElement(g.Code1).Text.Length == 0)) this.GetElement(g.Code1).Text = diag.DiagName;
                        if (this.GetElement(g.Code2) != null && (dr == DialogResult.Yes || this.GetElement(g.Code2).Text.Length == 0)) this.GetElement(g.Code2).Text = diag.ICDCode1;
                        if (this.GetElement(g.Code3) != null && (dr == DialogResult.Yes || this.GetElement(g.Code3).Text.Length == 0)) this.GetElement(g.Code3).Text = diag.DiagCode;
                        if (this.GetElement(g.Code4) != null && (dr == DialogResult.Yes || this.GetElement(g.Code4).Text.Length == 0)) this.GetElement(g.Code4).Text = diag.MainName;
                        if (this.GetElement(g.Code5) != null && (dr == DialogResult.Yes || this.GetElement(g.Code5).Text.Length == 0)) this.GetElement(g.Code5).Text = diag.PreSuffixString("");
                    }

                    // 入院契機病名
                    if (obj.TriggerFlg)
                    {
                        DiagGroup g = DiagGroup.GetDataByName("入院契機病名");

                        if (this.GetElement(g.Code1) != null && (dr == DialogResult.Yes || this.GetElement(g.Code1).Text.Length == 0)) this.GetElement(g.Code1).Text = diag.DiagName;
                        if (this.GetElement(g.Code2) != null && (dr == DialogResult.Yes || this.GetElement(g.Code2).Text.Length == 0)) this.GetElement(g.Code2).Text = diag.ICDCode1;
                        if (this.GetElement(g.Code3) != null && (dr == DialogResult.Yes || this.GetElement(g.Code3).Text.Length == 0)) this.GetElement(g.Code3).Text = diag.DiagCode;
                        if (this.GetElement(g.Code4) != null && (dr == DialogResult.Yes || this.GetElement(g.Code4).Text.Length == 0)) this.GetElement(g.Code4).Text = diag.MainName;
                        if (this.GetElement(g.Code5) != null && (dr == DialogResult.Yes || this.GetElement(g.Code5).Text.Length == 0)) this.GetElement(g.Code5).Text = diag.PreSuffixString("");
                    }

                    // 医療資源1
                    if (obj.ResourceFlg1)
                    {
                        DiagGroup g = DiagGroup.GetDataByName("医療資源1");

                        if (this.GetElement(g.Code1) != null && (dr == DialogResult.Yes || this.GetElement(g.Code1).Text.Length == 0)) this.GetElement(g.Code1).Text = diag.DiagName;
                        if (this.GetElement(g.Code2) != null && (dr == DialogResult.Yes || this.GetElement(g.Code2).Text.Length == 0)) this.GetElement(g.Code2).Text = diag.ICDCode1;
                        if (this.GetElement(g.Code3) != null && (dr == DialogResult.Yes || this.GetElement(g.Code3).Text.Length == 0)) this.GetElement(g.Code3).Text = diag.DiagCode;
                        if (this.GetElement(g.Code4) != null && (dr == DialogResult.Yes || this.GetElement(g.Code4).Text.Length == 0)) this.GetElement(g.Code4).Text = diag.MainName;
                        if (this.GetElement(g.Code5) != null && (dr == DialogResult.Yes || this.GetElement(g.Code5).Text.Length == 0)) this.GetElement(g.Code5).Text = diag.PreSuffixString("");

                        // 付加コード
                        if (this.GetElement("d146") != null) this.GetElement("d146").Text = diag.PlusCode;
                    }

                    // 医療資源2
                    if (obj.ResourceFlg2)
                    {
                        DiagGroup g = DiagGroup.GetDataByName("医療資源2");

                        if (this.GetElement(g.Code1) != null && (dr == DialogResult.Yes || this.GetElement(g.Code1).Text.Length == 0)) this.GetElement(g.Code1).Text = diag.DiagName;
                        if (this.GetElement(g.Code2) != null && (dr == DialogResult.Yes || this.GetElement(g.Code2).Text.Length == 0)) this.GetElement(g.Code2).Text = diag.ICDCode1;
                        if (this.GetElement(g.Code3) != null && (dr == DialogResult.Yes || this.GetElement(g.Code3).Text.Length == 0)) this.GetElement(g.Code3).Text = diag.DiagCode;
                        if (this.GetElement(g.Code4) != null && (dr == DialogResult.Yes || this.GetElement(g.Code4).Text.Length == 0)) this.GetElement(g.Code4).Text = diag.MainName;
                        if (this.GetElement(g.Code5) != null && (dr == DialogResult.Yes || this.GetElement(g.Code5).Text.Length == 0)) this.GetElement(g.Code5).Text = diag.PreSuffixString("");
                    }

                    // 入院時併存症1～10
                    if (obj.SubFlg)
                    {
                        DiagGroup g = DiagGroup.GetDataByName("入院時併存症" + i);

                        if (this.GetElement(g.Code1) != null && (dr == DialogResult.Yes || this.GetElement(g.Code1).Text.Length == 0)) this.GetElement(g.Code1).Text = diag.DiagName;
                        if (this.GetElement(g.Code2) != null && (dr == DialogResult.Yes || this.GetElement(g.Code2).Text.Length == 0)) this.GetElement(g.Code2).Text = diag.ICDCode1;
                        if (this.GetElement(g.Code3) != null && (dr == DialogResult.Yes || this.GetElement(g.Code3).Text.Length == 0)) this.GetElement(g.Code3).Text = diag.DiagCode;
                        if (this.GetElement(g.Code4) != null && (dr == DialogResult.Yes || this.GetElement(g.Code4).Text.Length == 0)) this.GetElement(g.Code4).Text = diag.MainName;
                        if (this.GetElement(g.Code5) != null && (dr == DialogResult.Yes || this.GetElement(g.Code5).Text.Length == 0)) this.GetElement(g.Code5).Text = diag.PreSuffixString("");

                        i++;
                    }

                    // 入院後発症1～10
                    if (obj.AfterFlg)
                    {
                        DiagGroup g = DiagGroup.GetDataByName("入院後発症" + j);

                        if (this.GetElement(g.Code1) != null && (dr == DialogResult.Yes || this.GetElement(g.Code1).Text.Length == 0)) this.GetElement(g.Code1).Text = diag.DiagName;
                        if (this.GetElement(g.Code2) != null && (dr == DialogResult.Yes || this.GetElement(g.Code2).Text.Length == 0)) this.GetElement(g.Code2).Text = diag.ICDCode1;
                        if (this.GetElement(g.Code3) != null && (dr == DialogResult.Yes || this.GetElement(g.Code3).Text.Length == 0)) this.GetElement(g.Code3).Text = diag.DiagCode;
                        if (this.GetElement(g.Code4) != null && (dr == DialogResult.Yes || this.GetElement(g.Code4).Text.Length == 0)) this.GetElement(g.Code4).Text = diag.MainName;
                        if (this.GetElement(g.Code5) != null && (dr == DialogResult.Yes || this.GetElement(g.Code5).Text.Length == 0)) this.GetElement(g.Code5).Text = diag.PreSuffixString("");

                        j++;
                    }
                }
            }
        }

        /// <summary>
        /// 手術検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FindOpe(object sender, KeyEventArgs e)
        {
            if (!(sender is TextBox)) return;

            TextBox box = (TextBox)sender;

            if (e.KeyCode == Keys.F3)
            {
                OpeGroup g = OpeGroup.GetDataByCode(box.Name);

                FormFindOpe f = new FormFindOpe(box.Text);

                if (f.ShowDialog() == DialogResult.OK)
                {
                    if (this.GetElement(g.Code1) != null) this.GetElement(g.Code1).Text = f.OpeMaster.Name;
                    if (this.GetElement(g.Code2) != null) this.GetElement(g.Code2).Text = f.OpeMaster.STEM7.Replace(" ", "");
                    if (this.GetElement(g.Code3) != null) this.GetElement(g.Code3).Text = AppString.ZenToHan(f.OpeMaster.KCode.Replace(" ", ""));
                }
            }
            else if (e.KeyCode == Keys.F5)
            {
                PatIn pat_in = PatIn.GetDataByDate(this.Pat.Id, this._Header.AdmDate);

                if (!AppString.IsDate(pat_in.InDate)) return;

                bool b = false;

                // 手術取込み
                DischargeSummary obj = DischargeSummary.GetData(this.Pat.Id, pat_in.SEQ);

                for (int i = 1; i <= 5; i++)
                {
                    if (obj.OpeList[i].Name.Length > 0)
                    {
                        b = true;
                        break;
                    }
                }

                if (!b)
                {
                    MessageBox.Show("手術が登録されていません");
                    return;
                }

                b = false;

                foreach (OpeGroup g in OpeGroup.List)
                {
                    // 取り込むのは Code1: 手術名, Code4: 開始日, Code6: 麻酔 のみ
                    if (this.GetElement(g.Code1) != null && this.GetElement(g.Code1).Text.Length > 0) b = true;
                    if (this.GetElement(g.Code4) != null && this.GetElement(g.Code4).Text.Length > 0) b = true;
                    if (this.GetElement(g.Code6) != null && this.GetElement(g.Code6).Text.Length > 0) b = true;

                    if (b) break;
                }

                DialogResult dr = DialogResult.Cancel;

                string s = "すでに入力されている箇所があります。上書きしますか？" + Environment.NewLine + Environment.NewLine +
                    " Yes … 上書きする" + Environment.NewLine +
                    " No … 空欄のみ上書きする" + Environment.NewLine +
                    " Cancel … キャンセル";

                if (b)
                {
                    dr = MessageBox.Show(s, "確認", MessageBoxButtons.YesNoCancel);

                    if (dr == DialogResult.Cancel) return;
                }

                for (int i = 1; i <= 5; i++)
                {
                    OpeGroup g = OpeGroup.GetDataByName("手術" + i);

                    if (this.GetElement(g.Code1) != null && (dr == DialogResult.Yes || this.GetElement(g.Code1).Text.Length == 0)) this.GetElement(g.Code1).Text = obj.OpeList[i].Name;
                    if (this.GetElement(g.Code4) != null && (dr == DialogResult.Yes || this.GetElement(g.Code4).Text.Length == 0)) this.GetElement(g.Code4).Text = AppString.IsDate(obj.OpeList[i].Date) ? obj.OpeList[i].Date : "";

                    if (this.GetElement(g.Code6) != null && (dr == DialogResult.Yes || this.GetElement(g.Code6).Text.Length == 0))
                    {
                        if (obj.OpeList[i].Anes.Contains("全"))
                        {
                            if (obj.OpeList[i].Anes.Contains("硬"))
                            {
                                this.GetElement(g.Code6).Text = "6: 全麻＋硬膜外";
                            }
                            else
                            {
                                this.GetElement(g.Code6).Text = "1: 全麻";
                            }
                        }
                        else if (obj.OpeList[i].Anes.Contains("脊"))
                        {
                            if (obj.OpeList[i].Anes.Contains("硬"))
                            {
                                this.GetElement(g.Code6).Text = "7: 脊椎＋硬膜外";
                            }
                            else
                            {
                                this.GetElement(g.Code6).Text = "3: 脊椎";
                            }
                        }
                        else if (obj.OpeList[i].Anes.Contains("硬"))
                        {
                            this.GetElement(g.Code6).Text = "2: 硬膜外";
                        }
                        else if (obj.OpeList[i].Anes.Contains("静"))
                        {
                            this.GetElement(g.Code6).Text = "4: 静麻";
                        }
                        else if (obj.OpeList[i].Anes.Contains("局"))
                        {
                            this.GetElement(g.Code6).Text = "5: 局";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 再入院理由種別
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RepeatReason1(object sender, EventArgs e)
        {
            if (this.GetElement("c35") == null) return;
            if (this.GetElement("c36") == null) return;

            Control c1 = this.GetElement("c35");
            Control c2 = this.GetElement("c36");

            if (!(c1 is DPCComboBox)) return;
            if (!(c2 is DPCComboBox)) return;

            DPCComboBox box1 = (DPCComboBox)c1;
            DPCComboBox box2 = (DPCComboBox)c2;

            box2.Items.Clear();
            box2.Items.Add(new DPCSubItem2());

            if (box1.Value.Equals("1") && DPCItem2.Dict.ContainsKey("c36a"))
            {
                foreach (DPCSubItem2 sub in DPCItem2.Dict["c36a"].SubItemList)
                {
                    box2.Items.Add(sub);
                }
            }
            else if (box1.Value.Equals("2") && DPCItem2.Dict.ContainsKey("c36b"))
            {
                foreach (DPCSubItem2 sub in DPCItem2.Dict["c36b"].SubItemList)
                {
                    box2.Items.Add(sub);
                }
            }
        }

        /// <summary>
        /// 再転棟理由種別
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RepeatReason2(object sender, EventArgs e)
        {
            if (this.GetElement("c38") == null) return;
            if (this.GetElement("c39") == null) return;

            Control c1 = this.GetElement("c38");
            Control c2 = this.GetElement("c39");

            if (!(c1 is DPCComboBox)) return;
            if (!(c2 is DPCComboBox)) return;

            DPCComboBox box1 = (DPCComboBox)c1;
            DPCComboBox box2 = (DPCComboBox)c2;

            box2.Items.Clear();
            box2.Items.Add(new DPCSubItem2());

            if (box1.Value.Equals("1") && DPCItem2.Dict.ContainsKey("c39a"))
            {
                foreach (DPCSubItem2 sub in DPCItem2.Dict["c39a"].SubItemList)
                {
                    box2.Items.Add(sub);
                }
            }
            else if (box1.Value.Equals("2") && DPCItem2.Dict.ContainsKey("c39b"))
            {
                foreach (DPCSubItem2 sub in DPCItem2.Dict["c39b"].SubItemList)
                {
                    box2.Items.Add(sub);
                }
            }
        }

        /// <summary>
        /// がんの Stage 分類
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CancerStage(object sender, EventArgs e)
        {
            if (this.GetElement("d141") == null) return;
            if (this.GetElement("d90") == null) return;

            Control c1 = this.GetElement("d141");
            Control c2 = this.GetElement("d90");

            if (!(c1 is DPCComboBox)) return;
            if (!(c2 is DPCComboBox)) return;

            DPCComboBox box1 = (DPCComboBox)c1;
            DPCComboBox box2 = (DPCComboBox)c2;

            // d90 に入るべき値のリスト
            string[] ss = null;

            switch (box1.Value)
            {
                case "104":
                    ss = new string[] { "0", "1N", "1A", "1B", "2N", "2A", "2B", "3", "4", "9" };
                    break;

                case "105":
                    ss = new string[] { "0", "1", "2", "3N", "3A", "3B", "4N", "4A", "4B", "9" };
                    break;

                case "106":
                    ss = new string[] { "0", "1N", "1A", "1B", "2N", "2A", "2B", "3", "4", "9" };
                    break;

                case "107":
                    ss = new string[] { "0", "1", "2", "3N", "3A", "3B", "4N", "4A", "4B", "9" };
                    break;

                case "108":
                    ss = new string[] { "0", "1N", "1A", "1B", "2N", "2A", "2B", "3", "4", "9" };
                    break;

                case "109":
                    // 2020/04/01 から変更
                    // 2, 4 が無くなり、2N～2C, 3C, 4N～4C が追加
//                    ss = new string[] { "0", "1", "2", "3N", "3A", "3B", "4", "9" };
                    ss = new string[] { "0", "1", "2", "2N", "2A", "2B", "2C", "3N", "3A", "3B", "3C", "4", "4N", "4A", "4B", "4C", "9" };
//                    ss = new string[] { "0", "1", "2N", "2A", "2B", "2C", "3N", "3A", "3B", "3C", "4N", "4A", "4B", "4C", "9" };
                    break;

                case "110":
                    ss = new string[] { "1", "2", "3", "4N", "4A", "4B", "9" };
                    break;

                case "111":
                    ss = new string[] { "1", "2", "3", "4N", "4A", "4B", "9" };
                    break;

                case "112":
                    ss = new string[] { "1N", "1A", "1B", "2N", "2A", "2B", "2C", "3", "4", "9" };
                    break;

                case "113":
                    ss = new string[] { "1N", "1A", "1B", "2N", "2A", "2B", "3", "4N", "4A", "4B", "9" };
                    break;

                case "114":
                    ss = new string[] { "1", "2", "3", "4", "9" };
                    break;

                case "115":
                    ss = new string[] { "1", "2", "3", "4", "9" };
                    break;

                case "116":
                    ss = new string[] { "5", "6", "7", "8", "9" };
                    break;
            }

            box2.Items.Clear();
            box2.Items.Add(new DPCSubItem2());

            if (DPCItem2.Dict.ContainsKey("d90"))
            {
                foreach (DPCSubItem2 sub in DPCItem2.Dict["d90"].SubItemList)
                {
                    if (ss == null || ss.Contains(sub.Value))
                    {
                        box2.Items.Add(sub);
                    }
                }
            }
        }

        /// <summary>
        /// がん取扱い規約
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CancerDoc(object sender, EventArgs e)
        {
            Label lb = (Label)sender;
            DPCFile obj = DPCFile.GetData(lb.Name);

            if (!File.Exists(obj.File))
            {
                MessageBox.Show("該当のファイルがありません");
                return;
            }

            FormPDFViewer1 f = new FormPDFViewer1(FormPDFViewer1.PageOrientation.Portrait);
            f.Navigate(obj.File);
            f.Show();
        }

        /// <summary>
        /// 肺炎重症度
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        void CalcHaien(object sender, EventArgs e)
        {
            if (this.GetElement("d167") != null)
            {
                this.GetElement("d167").Text = this.ConcatVals("d167");
            }
        }

        /// <summary>
        /// 肝硬変分類
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        void CalcKankouhen(object sender, EventArgs e)
        {
            if (this.GetElement("d106") != null)
            {
                this.GetElement("d106").Text = this.ConcatVals("d106");
            }
        }

        /// <summary>
        /// 膵炎の重症度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CalcSuien(object sender, EventArgs e)
        {
            Control c = this.GetElement("d122");

            if (c == null)
            {
                return;
            }

            string s = "";

            Control ca = this.GetElement("d122a");

            if (ca != null && ca is DPCCheckPanel)
            {
                s += ((DPCCheckPanel)ca).Value.Length;
            }
            else
            {
                s += "0";
            }

            int n = 0;

            Control cb = this.GetElement("d122b");

            if (cb != null && cb is DPCComboBox)
            {
                int nn = 0;

                if (int.TryParse(((DPCComboBox)cb).Value, out nn))
                {
                    n += nn;
                }
            }

            Control cc = this.GetElement("d122c");

            if (cc != null && cc is DPCComboBox)
            {
                int nn = 0;

                if (int.TryParse(((DPCComboBox)cc).Value, out nn))
                {
                    n += nn;
                }
            }

            c.Text = s + n.ToString();
        }

        /// <summary>
        /// 敗血症１回目当日
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        void CalcHaiketsu11(object sender, EventArgs e)
        {
            if (this.GetElement("d355") != null)
            {
                this.GetElement("d355").Text = this.ConcatVals("d355");
            }
        }

        /// <summary>
        /// 敗血症１回目翌日
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        void CalcHaiketsu12(object sender, EventArgs e)
        {
            if (this.GetElement("d356") != null)
            {
                this.GetElement("d356").Text = this.ConcatVals("d356");
            }
        }

        /// <summary>
        /// 敗血症２回目当日
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        void CalcHaiketsu21(object sender, EventArgs e)
        {
            if (this.GetElement("d359") != null)
            {
                this.GetElement("d359").Text = this.ConcatVals("d359");
            }
        }

        /// <summary>
        /// 敗血症２回目翌日
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        void CalcHaiketsu22(object sender, EventArgs e)
        {
            if (this.GetElement("d360") != null)
            {
                this.GetElement("d360").Text = this.ConcatVals("d360");
            }
        }

        /// <summary>
        /// 身長・体重測定不能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SetHeightWeightNone(object sender, EventArgs e)
        {
            Control c1 = this.GetElement("d162");
            Control c2 = this.GetElement("d163");

            if (c1 != null) c1.Text = "000";
            if (c2 != null) c2.Text = "000";
        }

        /// <summary>
        /// 身長・体重を取り込む
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void GetHeightWeight(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5 || e.KeyCode == Keys.F6)
            {
                Control c1 = this.GetElement("d162");
                Control c2 = this.GetElement("d163");

                Dictionary<string, List<BaseInfo>> dict = BaseInfo.GetDict(this.Pat.Id);

                List<string> msgs = new List<string>();

                // F5 は栄養・排泄、F6 は短期入院
                string height = (e.KeyCode == Keys.F5) ? LibSettings.Current.BaseInfoCodes.Height2 : LibSettings.Current.BaseInfoCodes.Height;
                string weight = (e.KeyCode == Keys.F5) ? LibSettings.Current.BaseInfoCodes.Weight2 : LibSettings.Current.BaseInfoCodes.Weight;

                // 身長
                if (c1 != null)
                {
                    double d = 0.0;

                    if (dict.ContainsKey(height) &&
                        double.TryParse(AppString.ZenToHan(dict[height][0].Value), out d))
                    {
                        c1.Text = Math.Round(d, 0, MidpointRounding.AwayFromZero).ToString();
                    }
                    else
                    {
                        msgs.Add("身長が登録されていません");
                    }
                }

                // 体重
                if (c2 != null)
                {
                    double d = 0.0;

                    if (dict.ContainsKey(weight) &&
                        double.TryParse(AppString.ZenToHan(dict[weight][0].Value), out d))
                    {
                        c2.Text = Math.Round(d, 1, MidpointRounding.AwayFromZero).ToString();
                    }
                    else
                    {
                        msgs.Add("体重が登録されていません");
                    }
                }

                if (msgs.Count > 0)
                {
                    MessageBox.Show(AppString.ConcatList(msgs, Environment.NewLine));
                }
            }
        }

        /// <summary>
        /// 喫煙不明
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SetSmokingNone(object sender, EventArgs e)
        {
            Control c1 = this.GetElement("d118a");
            Control c2 = this.GetElement("d118b");

            if (c1 != null) c1.Text = "";
            if (c2 != null) c2.Text = "";

            Control c = this.GetElement("d118");

            if (c != null) c.Text = "9999";
        }

        /// <summary>
        /// 喫煙指数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CalcSmoking(object sender, EventArgs e)
        {
            Control c = this.GetElement("d118");

            if (c == null)
            {
                return;
            }

            // 喫煙指数の要素が入力されたかどうか
            bool b = true;

            float n = 1;

            foreach (Control cc in this.GetElementsStartsWith("d118", false))
            {
                if (!Regex.IsMatch(cc.Name, @"d118[a-z]{1}"))
                {
                    continue;
                }

                float nn = 0;

                if (float.TryParse(cc.Text, out nn))
                {
                    n *= nn;
                }
                else
                {
                    // 適切に入力されていない値がある場合
                    b = false;
                    break;
                }
            }

            if (b)
            {
                c.Text = n.ToString();
            }
            else
            {
                c.Text = "";
            }
        }

        /// <summary>
        /// 入棟時ADL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CalcADL1(object sender, EventArgs e)
        {
            if (this.GetElement("d100") != null)
            {
                this.GetElement("d100").Text = this.ConcatVals("d100");
            }

            // わかば病棟の場合は地域包括ケアADLもセットする
            if (this._Header.Ward.Equals("3"))
            {
                if (this.GetElement("d393") != null)
                {
                    this.GetElement("d393").Text = this.ConcatVals("d100");
                }
            }
        }

        /// <summary>
        /// 入棟時ADLをすべて自立にする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SetADL1(object sender, EventArgs e)
        {
            foreach (Control cc in this.GetElementsStartsWith("d100", false))
            {
                if (!Regex.IsMatch(cc.Name, @"^d100[a-z]{1}$"))
                {
                    continue;
                }

                if (cc is ComboBox)
                {
                    ComboBox box = (ComboBox)cc;

                    if (box.Items.Count >= 2)
                    {
                        box.SelectedIndex = 1;
                    }
                }
            }
        }

        /// <summary>
        /// 入棟時ADLをクリップボードにコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ClipADL1(object sender, EventArgs e)
        {
            string s = "【入棟時ADL】";

            foreach (Control cc in this.GetElementsStartsWith("d100", false))
            {
                if (!Regex.IsMatch(cc.Name, @"^d100[a-z]{1}$"))
                {
                    continue;
                }

                if (!DPCItem2.Dict.ContainsKey(cc.Name))
                {
                    continue;
                }

                DPCItem2 item = DPCItem2.Dict[cc.Name];

                if (s.Length > 0) s += Environment.NewLine;

                s += item.Name + "：";

                if (cc.Text.Contains(':') && cc.Text.IndexOf(':') < cc.Text.Length - 1)
                {
                    s += cc.Text.Substring(cc.Text.IndexOf(':') + 1).Trim();
                }
            }

            Clipboard.SetText(s);
        }

        /// <summary>
        /// 退棟時ADL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CalcADL2(object sender, EventArgs e)
        {
            if (this.GetElement("d119") != null)
            {
                this.GetElement("d119").Text = this.ConcatVals("d119");
            }

            // わかば病棟の場合は地域包括ケアADLもセットする
            if (this._Header.Ward.Equals("3"))
            {
                if (this.GetElement("d394") != null)
                {
                    this.GetElement("d394").Text = this.ConcatVals("d119");
                }
            }
        }

        /// <summary>
        /// 退棟時ADLをすべて自立にする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SetADL2(object sender, EventArgs e)
        {
            foreach (Control cc in this.GetElementsStartsWith("d119", false))
            {
                if (!Regex.IsMatch(cc.Name, @"^d119[a-z]{1}$"))
                {
                    continue;
                }

                if (cc is ComboBox)
                {
                    ComboBox box = (ComboBox)cc;

                    if (box.Items.Count >= 2)
                    {
                        box.SelectedIndex = 1;
                    }
                }
            }
        }

        /// <summary>
        /// 退棟時ADLをクリップボードにコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ClipADL2(object sender, EventArgs e)
        {
            string s = "【退棟時ADL】";

            foreach (Control cc in this.GetElementsStartsWith("d119", false))
            {
                if (!Regex.IsMatch(cc.Name, @"^d119[a-z]{1}$"))
                {
                    continue;
                }

                if (!DPCItem2.Dict.ContainsKey(cc.Name))
                {
                    continue;
                }

                DPCItem2 item = DPCItem2.Dict[cc.Name];

                if (s.Length > 0) s += Environment.NewLine;

                s += item.Name + "：";

                if (cc.Text.Contains(':') && cc.Text.IndexOf(':') < cc.Text.Length - 1)
                {
                    s += cc.Text.Substring(cc.Text.IndexOf(':') + 1).Trim();
                }
            }

            Clipboard.SetText(s);
        }

        /// <summary>
        /// 退棟時ADLを入棟時コピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CopyADL2(object sender, EventArgs e)
        {
            foreach (Control cc in this.GetElementsStartsWith("d119", false))
            {
                if (!Regex.IsMatch(cc.Name, @"^d119[a-z]{1}$"))
                {
                    continue;
                }

                // 最後の１文字
                string k = cc.Name.Substring(cc.Name.Length - 1, 1);

                // 入棟時の値を取得してセットする
                if (this.GetElement("d100" + k) != null)
                {
                    if (cc is ComboBox && this.GetElement("d100" + k) is ComboBox)
                    {
                        ComboBox box = (ComboBox)cc;
                        ComboBox box2 = (ComboBox)this.GetElement("d100" + k);

                        box.SelectedIndex = box2.SelectedIndex;
                    }
                }
            }
        }

        /// <summary>
        /// JCSを求める
        /// </summary>
        /// <param name="code"></param>
        string CalcJCS(string code)
        {
            string n = "";

            foreach (Control cc in this.GetElementsStartsWith(code, false))
            {
                if (!Regex.IsMatch(cc.Name, code + @"[a-z]{1}"))
                {
                    continue;
                }

                if (cc is DPCComboBox)
                {
                    n += ((DPCComboBox)cc).Value;
                }
            }

            return n;
        }

        /// <summary>
        /// 入棟時JCS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CalcJCS1(object sender, EventArgs e)
        {
            if (this.GetElement("f13") != null)
            {
                this.GetElement("f13").Text = this.CalcJCS("f13");
            }
        }

        /// <summary>
        /// 退棟時JCS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CalcJCS2(object sender, EventArgs e)
        {
            if (this.GetElement("f128") != null)
            {
                this.GetElement("f128").Text = this.CalcJCS("f128");
            }
        }

        /// <summary>
        /// 退棟時JCSを入棟時コピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CopyJCS2(object sender, EventArgs e)
        {
            foreach (Control cc in this.GetElementsStartsWith("f128", false))
            {
                if (!Regex.IsMatch(cc.Name, @"^f128[a-z]{1}$"))
                {
                    continue;
                }

                // 最後の１文字
                string k = cc.Name.Substring(cc.Name.Length - 1, 1);

                // 入棟時の値を取得してセットする
                if (this.GetElement("f13" + k) != null)
                {
                    if (cc is ComboBox && this.GetElement("f13" + k) is ComboBox)
                    {
                        ComboBox box = (ComboBox)cc;
                        ComboBox box2 = (ComboBox)this.GetElement("f13" + k);

                        box.SelectedIndex = box2.SelectedIndex;
                    }
                }
            }
        }

        /// <summary>
        /// 入棟時要介護情報
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CalcKaigo1(object sender, EventArgs e)
        {
            if (this.GetElement("d391") != null)
            {
                this.GetElement("d391").Text = this.ConcatVals("d391");
            }
        }

        /// <summary>
        /// 入棟時要介護情報をすべて無しにする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SetKaigo1(object sender, EventArgs e)
        {
            if (this.GetElement("d387") != null)
            {
                Control cc = this.GetElement("d387");

                if (cc is ComboBox)
                {
                    ComboBox box = (ComboBox)cc;

                    if (box.Items.Count >= 2)
                    {
                        box.SelectedIndex = 1;
                    }
                }
            }

            if (this.GetElement("d389") != null)
            {
                Control cc = this.GetElement("d389");

                if (cc is ComboBox)
                {
                    ComboBox box = (ComboBox)cc;

                    if (box.Items.Count >= 2)
                    {
                        box.SelectedIndex = 1;
                    }
                }
            }

            foreach (Control cc in this.GetElementsStartsWith("d391", false))
            {
                if (!Regex.IsMatch(cc.Name, @"^d391[a-z]{1}$"))
                {
                    continue;
                }

                if (cc is ComboBox)
                {
                    ComboBox box = (ComboBox)cc;

                    if (box.Items.Count >= 2)
                    {
                        box.SelectedIndex = 1;
                    }
                }
            }
        }

        /// <summary>
        /// 退棟時要介護情報
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CalcKaigo2(object sender, EventArgs e)
        {
            if (this.GetElement("d392") != null)
            {
                this.GetElement("d392").Text = this.ConcatVals("d392");
            }
        }

        /// <summary>
        /// 退棟時要介護情報をすべて無しにする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SetKaigo2(object sender, EventArgs e)
        {
            if (this.GetElement("d388") != null)
            {
                Control cc = this.GetElement("d388");

                if (cc is ComboBox)
                {
                    ComboBox box = (ComboBox)cc;

                    if (box.Items.Count >= 2)
                    {
                        box.SelectedIndex = 1;
                    }
                }
            }

            if (this.GetElement("d390") != null)
            {
                Control cc = this.GetElement("d390");

                if (cc is ComboBox)
                {
                    ComboBox box = (ComboBox)cc;

                    if (box.Items.Count >= 2)
                    {
                        box.SelectedIndex = 1;
                    }
                }
            }

            foreach (Control cc in this.GetElementsStartsWith("d392", false))
            {
                if (!Regex.IsMatch(cc.Name, @"^d392[a-z]{1}$"))
                {
                    continue;
                }

                if (cc is ComboBox)
                {
                    ComboBox box = (ComboBox)cc;

                    if (box.Items.Count >= 2)
                    {
                        box.SelectedIndex = 1;
                    }
                }
            }
        }

        /// <summary>
        /// 退棟時要介護情報を入棟時コピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CopyKaigo2(object sender, EventArgs e)
        {
            if (this.GetElement("d387") != null &&
                this.GetElement("d388") != null)
            {
                Control c1 = this.GetElement("d387");
                Control c2 = this.GetElement("d388");

                if (c1 is ComboBox && c2 is ComboBox)
                {
                    ComboBox cc1 = (ComboBox)c1;
                    ComboBox cc2 = (ComboBox)c2;

                    cc2.SelectedIndex = cc1.SelectedIndex;
                }
            }

            if (this.GetElement("d389") != null &&
                this.GetElement("d390") != null)
            {
                Control c1 = this.GetElement("d389");
                Control c2 = this.GetElement("d390");

                if (c1 is ComboBox && c2 is ComboBox)
                {
                    ComboBox cc1 = (ComboBox)c1;
                    ComboBox cc2 = (ComboBox)c2;

                    cc2.SelectedIndex = cc1.SelectedIndex;
                }
            }

            foreach (Control cc in this.GetElementsStartsWith("d392", false))
            {
                if (!Regex.IsMatch(cc.Name, @"^d392[a-z]{1}$"))
                {
                    continue;
                }

                // 最後の１文字
                string k = cc.Name.Substring(cc.Name.Length - 1, 1);

                // 入棟時の値を取得してセットする
                if (this.GetElement("d391" + k) != null)
                {
                    if (cc is ComboBox && this.GetElement("d391" + k) is ComboBox)
                    {
                        ComboBox box = (ComboBox)cc;
                        ComboBox box2 = (ComboBox)this.GetElement("d391" + k);

                        box.SelectedIndex = box2.SelectedIndex;
                    }
                }
            }
        }

        #endregion

        void DataClear()
        {
            // 診断を空にする
            this.DiagLabel.Text = "";

            // 診断パネルのチェックを外す
            foreach (Control c in this.DiagPanel.Controls)
            {
                if (c is CheckBox)
                {
                    ((CheckBox)c).Checked = false;
                }
            }

            this.AdmDateBox.Clear();
            this.StartDateBox.Clear();
            this.WardNameBox.Clear();

            this.DeptBox.Text = "";
            this.DoctorBox1.Text = "";
            this.DoctorBox2.Text = "";
            this.ContBox.Clear();

            this.StatusBox1.Checked = false;
            this.DateTimeLabel1.Text = "";
            this.StaffLabel1.Text = "";

            this.StatusBox2.Checked = false;
            this.DateTimeLabel2.Text = "";
            this.StaffLabel2.Text = "";

            this.StatusBox3.Checked = false;
            this.DateTimeLabel3.Text = "";
            this.StaffLabel3.Text = "";

            this.StatusBox4.Checked = false;
            this.DateTimeLabel4.Text = "";
            this.StaffLabel4.Text = "";

            this.StatusBox5.Checked = false;
            this.DateTimeLabel5.Text = "";
            this.StaffLabel5.Text = "";

            for (int i = 1; i <= 5; i++)
            {
                this.DetailClear(i);
            }
        }

        void DetailClear(int i)
        {
            Panel panel;

            switch (i)
            {
                case 2:
                    panel = this.DetailPanel2;
                    break;

                case 3:
                    panel = this.DetailPanel3;
                    break;

                case 4:
                    panel = this.DetailPanel4;
                    break;

                case 5:
                    panel = this.DetailPanel5;
                    break;

                default:
                    panel = this.DetailPanel1;
                    break;
            }

            foreach (Control c in panel.Controls)
            {
                if (c is DPCRadioPanel)
                {
                    ((DPCRadioPanel)c).Clear();
                }
                else if (c is DPCCheckPanel)
                {
                    ((DPCCheckPanel)c).Clear();
                }
                else if (c is DPCComboBox)
                {
                    ((ComboBox)c).Text = "";
                }
                else if (c is TextBox)
                {
                    ((TextBox)c).Text = "";
                }
            }
        }

        /// <summary>
        /// データを表示する
        /// </summary>
        /// <param name="detail_panel_init">true: DetailPanel を再描画する</param>
        void DataShow(bool detail_panel_init)
        {
            // DetailPanel を再描画する場合
            if (detail_panel_init)
            {
                this.DetailPanelInit();
            }

            this.DataClear();

            // 入院日
            this.AdmDateBox.Text = DateTimeAgent.DateFormat(this._Header.AdmDate, DateTimeAgent.DateFormatKind.LONG);

            // 入棟日
            this.StartDateBox.Text = DateTimeAgent.DateFormat(this._Header.StartDate, DateTimeAgent.DateFormatKind.LONG);

            // 病棟
            if (Dict.WardDict.ContainsKey(this._Header.Ward))
            {
                this.WardNameBox.Text = Dict.WardDict[this._Header.Ward].Name;
                this.WardNameBox.BackColor = Dict.WardDict[this._Header.Ward].BackColor;
            }


            // 診断
            this.DiagLabel.Tag = this._Header.Diag;

            foreach (string s in this._Header.DiagList)
            {
                // 診断名
                if (this.DiagLabel.Text.Length > 0)
                {
                    this.DiagLabel.Text += " ";
                }

                this.DiagLabel.Text += DPCDiag.GetData(s).Name;

                // 診断パネルにチェックを入れる
                if (this.DiagPanel.Controls.ContainsKey("Diag_" + s))
                {
                    CheckBox cb = (CheckBox)this.DiagPanel.Controls["Diag_" + s];
                    cb.Checked = true;
                }
            }

            // 入院歴
            List<PatIn> in_list = new List<PatIn>();

            // 退棟時間帯
            string end_time = "";

            // 診療科または医師1 が登録されていなければ入院歴・病棟移動歴から取得する
            if (this._Header.DeptName.Length == 0 || this._Header.DoctorName1.Length == 0)
            {
                in_list = PatIn.GetHistory(this.Pat.Id, true).FindAll((x) =>
                {
                    return x.InDate.Equals(this._Header.AdmDate);
                });

                // 病棟移動歴から退棟時間帯を取得する
                // 　→　退棟より先に科・医師を変更している場合があるので、退棟時間帯は取得しない 2019/08/13
                /*
                List<PatInDPCWard> ward_list = PatInDPCWard.GetList(in_list);

                foreach (PatInDPCWard _ward in ward_list)
                {
                    if (_ward.Id.Equals(this.Pat.Id) &&
                        _ward.DoDate.Equals(this._Header.StartDate) &&
                        _ward.Ward.Equals(this._Header.Ward))
                    {
                        end_time = _ward.EndTime;
                        break;
                    }
                }
                 */
            }

            // 診療科
            if (this._Header.DeptName.Length > 0)
            {
                // すでに登録されている場合
                this.DeptBox.Text = this._Header.Dept + " " + DPCDept.GetData(this._Header.Dept).Name;
            }
            else
            {
                // 未登録の場合は、科の履歴から取得する

                List<PatIn> dept_list = PatIn.GetDeptList(in_list, false);

                // 時系列の降順に並べ替える
                dept_list.Sort((x, y) =>
                {

                    int i = y.DoDate.CompareTo(x.DoDate);

                    if (i == 0)
                    {
                        i = y.DoTime.CompareTo(x.DoTime);
                    }

                    if (i == 0)
                    {
                        i = y.SEQ2 - x.SEQ2;
                    }

                    return i;
                });

                if (AppString.IsDate(this._Header.EndDate) && !AppString.IsDate(this._Header.DisDate))
                {
                    // 退棟日が入っている かつ 転棟の場合は、退棟直前の科まで遡る

                    foreach (PatIn pin in dept_list)
                    {
                        // 退棟日以降、または退棟した日の退棟時間以降ならば飛ばす
                        if (pin.DoDate.CompareTo(this._Header.EndDate) > 0 ||
                            (pin.DoDate.Equals(this._Header.EndDate) && pin.DoTime.CompareTo(end_time) >= 0)) continue;

                        DPCDept dept = DPCDept.GetDataByCode2(pin.Dept);
                        this.DeptBox.Text = dept.Code + " " + dept.Name;
                        break;
                    }
                }
                else
                {
                    // 退棟日が入っていない または 転棟でない場合は、最後の科を取得する

                    foreach (PatIn pin in dept_list)
                    {
                        DPCDept dept = DPCDept.GetDataByCode2(pin.Dept);
                        this.DeptBox.Text = dept.Code + " " + dept.Name;
                        break;
                    }
                }
            }

            // 医師1
            if (this._Header.DoctorName1.Length > 0)
            {
                // すでに登録されている場合
                this.DoctorBox1.SetDoctor(this._Header.Doctor1);
            }
            else
            {
                // 未登録の場合は、主治医の履歴から取得する

                // 入棟日時点の医師を取得する
                List<PatIn> doctor_list = PatIn.GetDoctorList(in_list, false);

                // 時系列の降順に並べ替える
                doctor_list.Sort((x, y) =>
                {

                    int i = y.DoDate.CompareTo(x.DoDate);

                    if (i == 0)
                    {
                        i = y.DoTime.CompareTo(x.DoTime);
                    }

                    if (i == 0)
                    {
                        i = y.SEQ2 - x.SEQ2;
                    }

                    return i;
                });

                if (AppString.IsDate(this._Header.EndDate) && !AppString.IsDate(this._Header.DisDate))
                {
                    // 退棟日が入っている かつ 転棟の場合は、退棟直前の医師まで遡る

                    foreach (PatIn pin in doctor_list)
                    {
                        // 退棟日以降、または退棟した日の退棟時間以降ならば飛ばす
                        if (pin.DoDate.CompareTo(this._Header.EndDate) > 0 ||
                            (pin.DoDate.Equals(this._Header.EndDate) && pin.DoTime.CompareTo(end_time) >= 0)) continue;

                        this.DoctorBox1.SetDoctor(pin.Doctor);
                        break;
                    }
                }
                else
                {
                    // 退棟日が入っていない または 転棟でない場合は、最後の医師を取得する

                    foreach (PatIn pin in doctor_list)
                    {
                        this.DoctorBox1.SetDoctor(pin.Doctor);
                        break;
                    }
                }
            }

            // 医師2
            this.DoctorBox2.SetDoctor(this._Header.Doctor2);

            // メモ
            this.ContBox.Text = this._Header.Cont;

            // 登録日時・スタッフ
            this.DateTimeLabel.Text = this._Header.SaveDateTime;
            this.StaffLabel.Text = this._Header.StaffName;


            this.StatusBox1.Checked = this._Header.Status1.Equals("1");
            this.DateTimeLabel1.Text = this._Header.SaveDateTime1;
            this.StaffLabel1.Text = this._Header.StaffName1;
            if (this._Header.Status1.Equals("1")) this.DetailSaveButton1.Enabled = false;
            this.DetailShow(1, this._Header.DetailList, this._Header.SaveDate1.Length == 0, false);

            this.StatusBox2.Checked = this._Header.Status2.Equals("1");
            this.DateTimeLabel2.Text = this._Header.SaveDateTime2;
            this.StaffLabel2.Text = this._Header.StaffName2;
            if (this._Header.Status2.Equals("1")) this.DetailSaveButton2.Enabled = false;
            this.DetailShow(2, this._Header.DetailList, this._Header.SaveDate2.Length == 0, false);

            this.StatusBox3.Checked = this._Header.Status3.Equals("1");
            this.DateTimeLabel3.Text = this._Header.SaveDateTime3;
            this.StaffLabel3.Text = this._Header.StaffName3;
            if (this._Header.Status3.Equals("1")) this.DetailSaveButton3.Enabled = false;
            this.DetailShow(3, this._Header.DetailList, this._Header.SaveDate3.Length == 0, false);

            this.StatusBox4.Checked = this._Header.Status4.Equals("1");
            this.DateTimeLabel4.Text = this._Header.SaveDateTime4;
            this.StaffLabel4.Text = this._Header.StaffName4;
            if (this._Header.Status4.Equals("1")) this.DetailSaveButton4.Enabled = false;
            this.DetailShow(4, this._Header.DetailList, this._Header.SaveDate4.Length == 0, false);

            this.StatusBox5.Checked = this._Header.Status5.Equals("1");
            this.DateTimeLabel5.Text = this._Header.SaveDateTime5;
            this.StaffLabel5.Text = this._Header.StaffName5;
            if (this._Header.Status5.Equals("1")) this.DetailSaveButton5.Enabled = false;
            this.DetailShow(5, this._Header.DetailList, this._Header.SaveDate5.Length == 0, false);
        }

        /// <summary>
        /// Detail データを表示する
        /// </summary>
        /// <param name="i"></param>
        /// <param name="detail_list"></param>
        /// <param name="default_val">true: デフォルト値を適用する（通常は新規作成時のみ）</param>
        /// <param name="copy"></param>
        void DetailShow(int i, List<DPCDetail> detail_list, bool default_val = true, bool copy = false)
        {
            Panel panel;

            switch (i)
            {
                case 2:
                    panel = this.DetailPanel2;
                    break;

                case 3:
                    panel = this.DetailPanel3;
                    break;

                case 4:
                    panel = this.DetailPanel4;
                    break;

                case 5:
                    panel = this.DetailPanel5;
                    break;

                default:
                    panel = this.DetailPanel1;
                    break;
            }

            foreach (Control c in panel.Controls)
            {
                // DPCItem2 のマスターに無ければ飛ばす
                if (!DPCItem2.Dict.ContainsKey(c.Name))
                {
                    continue;
                }

                DPCItem2 item = DPCItem2.Dict[c.Name];

                // 値をセットするコントロールでなければ飛ばす
                if (!(c is DPCRadioPanel) &&
                    !(c is DPCCheckPanel) &&
                    !(c is DPCComboBox) &&
                    !(c is TextBox))
                {
                    continue;
                }

                // コピーの場合
                if (copy)
                {
                    // すでに値が入っている場合は飛ばす
                    if (c is DPCRadioPanel && ((DPCRadioPanel)c).Value.Length > 0)
                    {
                        continue;
                    }
                    else if (c is DPCCheckPanel && ((DPCCheckPanel)c).Value.Length > 0)
                    {
                        continue;
                    }
                    else if (c is DPCComboBox && ((DPCComboBox)c).Value.Length > 0)
                    {
                        continue;
                    }
                    else if (c is TextBox && ((TextBox)c).Text.Length > 0)
                    {
                        continue;
                    }

                    // コピーしなくてよいものは飛ばす
                    string[] ss = new string[] {
                        "a1", "a6", "a7", "a9", "a10", "a16", "a17", "a20", "a21",
                        "b1", "b2", "b3", "b4", "b5",
                        "c1", "c2", "c4", "c29", "c30"
                    };

                    if (ss.Contains(c.Name))
                    {
                        continue;
                    }
                }

                DPCDetail detail = detail_list.Find((x) => { return x.Code.Equals(c.Name); });

                // 該当の detail がなく Default 値も無い場合は飛ばす
                if (detail == null && (!default_val || item.Default.Length == 0))
                {
                    continue;
                }

                // セットする値
                string val = (detail != null) ? detail.Cont : item.Default;

                if (item.Box.StartsWith("RadioButton"))
                {
                    if (c is DPCRadioPanel)
                    {
                        ((DPCRadioPanel)c).Value = val;
                    }
                }
                else if (item.Box.StartsWith("CheckBox"))
                {
                    if (c is DPCCheckPanel)
                    {
                        ((DPCCheckPanel)c).Value = val;
                    }
                }
                else if (item.Box.StartsWith("ComboBox"))
                {
                    if (c is DPCComboBox)
                    {
                        ((DPCComboBox)c).Value = val;
                    }
                }
                else
                {
                    c.Text = val;
                }
            }
        }

        void HeaderSave()
        {
            List<string> errs = new List<string>();

            if (!AppString.IsNumber(this._Header.Id)) errs.Add("患者IDがありません");
            if (!AppString.IsDate(this._Header.StartDate)) errs.Add("入棟日がありません");
            if (this._Header.Ward.Length == 0) errs.Add("病棟がありません");

            if (errs.Count > 0)
            {
                MessageBox.Show(AppString.ConcatList(errs, Environment.NewLine));
                return;
            }

            this._Header.Diag = this.DiagLabel.Tag.ToString();
            this._Header.Dept = this.DeptBox.Text.Contains(' ') ? this.DeptBox.Text.Split(' ')[0] : "";
            this._Header.Doctor1 = this.DoctorBox1.GetDoctor().Code.ToString();
            this._Header.Doctor2 = this.DoctorBox2.GetDoctor().Code.ToString();
            this._Header.Cont = this.ContBox.Text;

            this._Header.Status = "1";

            this._Header.Save(DPCHeader.SaveMode.HEADER);

            MessageBox.Show("登録しました");

            this._Saved = true;

            // データを再取得する
            this._Header = DPCHeader.GetData(this._Header.Id, this._Header.StartDate, this._Header.Ward, true);

            if (this.Owner is FormDPCList)
            {
                // FormDPCList を更新する
                ((FormDPCList)this.Owner).ListShow();
            }
            else if (this.Owner is FormDPCList2)
            {
                ((FormDPCList2)this.Owner).ListShow();
            }
        }

        void DetailSave(int i)
        {
            List<string> errs = new List<string>();

            if (!AppString.IsNumber(this._Header.Id)) errs.Add("患者IDがありません");
            if (!AppString.IsDate(this._Header.StartDate)) errs.Add("入棟日がありません");
            if (this._Header.Ward.Length == 0) errs.Add("病棟がありません");

            if (errs.Count > 0)
            {
                MessageBox.Show(AppString.ConcatList(errs, Environment.NewLine));
                return;
            }

            this._Header.DetailList.Clear();
            Panel panel;

            switch (i)
            {
                case 2:
                    panel = this.DetailPanel2;
                    this._Header.Status2 = this.StatusBox2.Checked ? "1" : "0";
                    break;

                case 3:
                    panel = this.DetailPanel3;
                    this._Header.Status3 = this.StatusBox3.Checked ? "1" : "0";
                    break;

                case 4:
                    panel = this.DetailPanel4;
                    this._Header.Status4 = this.StatusBox4.Checked ? "1" : "0";
                    break;

                case 5:
                    panel = this.DetailPanel5;
                    this._Header.Status5 = this.StatusBox5.Checked ? "1" : "0";
                    break;

                default:
                    panel = this.DetailPanel1;
                    this._Header.Status1 = this.StatusBox1.Checked ? "1" : "0";
                    break;
            }

            foreach (Control c in panel.Controls)
            {
                if (c is DPCRadioPanel)
                {
                    DPCRadioPanel p = (DPCRadioPanel)c;

                    DPCDetail detail = new DPCDetail();
                    detail.Code = c.Name;
                    detail.Cont = p.Value;

                    this._Header.DetailList.Add(detail);
                }
                else if (c is DPCCheckPanel)
                {
                    DPCCheckPanel p = (DPCCheckPanel)c;

                    DPCDetail detail = new DPCDetail();
                    detail.Code = c.Name;
                    detail.Cont = p.Value;

                    this._Header.DetailList.Add(detail);
                }
                else if (c is DPCComboBox)
                {
                    DPCComboBox b = (DPCComboBox)c;

                    DPCDetail detail = new DPCDetail();
                    detail.Code = c.Name;
                    detail.Cont = b.Value;

                    this._Header.DetailList.Add(detail);
                }
                else if (c is TextBox)
                {
                    TextBox b = (TextBox)c;

                    DPCDetail detail = new DPCDetail();
                    detail.Code = c.Name;
                    detail.Cont = c.Text;

                    this._Header.DetailList.Add(detail);
                }
            }

            this._Header.Save(DPCHeader.SaveMode.DETAIL, i);

            MessageBox.Show("登録しました");

            this._Saved = true;

            // データを再取得する
            this._Header = DPCHeader.GetData(this._Header.Id, this._Header.StartDate, this._Header.Ward, true);

            if (this.Owner is FormDPCList)
            {
                // FormDPCList を更新する
                ((FormDPCList)this.Owner).ListShow();
            }
            else if (this.Owner is FormDPCList2)
            {
                ((FormDPCList2)this.Owner).ListShow();
            }
        }

        private void DiagLabel_Click(object sender, EventArgs e)
        {
            this.DiagPanel.Visible = true;
        }

        private void DiagSetLabel_Click(object sender, EventArgs e)
        {
            this.DiagPanel.Visible = false;

            // 診断
            string diag = "";
            string diag_name = "";

            foreach (Control c in this.DiagPanel.Controls)
            {
                if (c is CheckBox && ((CheckBox)c).Checked)
                {
                    if (diag.Length > 0)
                    {
                        // スペース区切り
                        diag += " ";
                        diag_name += " ";
                    }

                    // 診断コードを追加
                    diag += c.Name.Split('_')[1];

                    // 診断名を追加
                    diag_name += c.Text;
                }
            }

            this.DiagLabel.Text = diag_name;
            this.DiagLabel.Tag = diag;
        }

        private void HeaderSaveButton_Click(object sender, EventArgs e)
        {
            this.HeaderSave();

            // データを再表示する
            this.DataShow(true);
        }

        private void DetailSaveButton1_Click(object sender, EventArgs e)
        {
            this.DetailSave(1);

            // データを再表示する
            this.DataShow(true);
        }

        private void DetailSaveButton2_Click(object sender, EventArgs e)
        {
            this.DetailSave(2);

            // データを再表示する
            this.DataShow(true);
        }

        private void DetailSaveButton3_Click(object sender, EventArgs e)
        {
            this.DetailSave(3);

            // データを再表示する
            this.DataShow(true);
        }

        private void DetailSaveButton4_Click(object sender, EventArgs e)
        {
            this.DetailSave(4);

            // データを再表示する
            this.DataShow(true);
        }

        private void DetailSaveButton5_Click(object sender, EventArgs e)
        {
            this.DetailSave(5);

            // データを再表示する
            this.DataShow(true);
        }

        private void HistoryButton_Click(object sender, EventArgs e)
        {
            FormDPCList2 f = new FormDPCList2(this._Header);

            if (f.ShowDialog(this) == DialogResult.OK)
            {
                DPCHeader _header = f.Header;

                // Detail が無い場合は取得する
                if (f.Header.DetailList.Count == 0)
                {
                    _header.DetailList = DPCDetail.GetList(f.Header);
                }

                TabPage page = this.TabControl1.SelectedTab;

                switch (page.Name)
                {
                    case "TabPage1":
                        this.DetailShow(1, _header.DetailList, false, true);
                        break;

                    case "TabPage2":
                        this.DetailShow(2, _header.DetailList, false, true);
                        break;

                    case "TabPage3":
                        this.DetailShow(3, _header.DetailList, false, true);
                        break;

                    case "TabPage4":
                        this.DetailShow(4, _header.DetailList, false, true);
                        break;

                    case "TabPage5":
                        this.DetailShow(5, _header.DetailList, false, true);
                        break;

                    default:
                        break;
                }
            }
        }

        private void DiagButton_Click(object sender, EventArgs e)
        {
            FormDiagDPC f = new FormDiagDPC();
            f.PatSet(this.Pat);
            f.Show(this);
        }

        private void InCalButton_Click(object sender, EventArgs e)
        {
            FormInCal2 f = new FormInCal2();
            f.PatSet(this.Pat);
            f.Show(this);
        }

        private void SummaryButton_Click(object sender, EventArgs e)
        {
            PatIn pin = PatIn.GetDataByDate(this.Pat.Id, this._Header.AdmDate);

            if (pin.SEQ > 0)
            {
                FormDischargeSummary f = new FormDischargeSummary(this.Pat.Id, pin.SEQ);
                f.Show();
            }
        }

        private void KarteMessageButton_Click(object sender, EventArgs e)
        {
            List<string> send_to_list = new List<string>();

            if (this.DoctorBox1.Text.Length > 0) send_to_list.Add(this.DoctorBox1.GetDoctor().StaffCode.ToString());
            if (this.DoctorBox2.Text.Length > 0) send_to_list.Add(this.DoctorBox2.GetDoctor().StaffCode.ToString());

            FormKarteMessage2 f = new FormKarteMessage2(this.Pat.Id, send_to_list);
            f.Show(this);
        }

        private void FormDPCData2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = this._Saved ? DialogResult.OK : DialogResult.Cancel;
        }
    }

    class DPCRadioPanel : Panel
    {
        bool _ReadOnly = false;

        public bool ReadOnly
        {
            get
            {
                return this._ReadOnly;
            }
            set
            {
                this._ReadOnly = value;

                foreach (Control c in this.Controls)
                {
                    if (c is RadioButton)
                    {
                        ((RadioButton)c).Enabled = !value;
                    }
                }
            }
        }

        public string Value
        {
            get
            {
                string s = "";

                foreach (Control c in this.Controls)
                {
                    if (c is RadioButton && ((RadioButton)c).Checked)
                    {
                        if (c.Text.Contains(':'))
                        {
                            s = c.Text.Split(':')[0];
                            break;
                        }
                    }
                }

                return s;
            }
            set
            {
                foreach (Control c in this.Controls)
                {
                    if (c is RadioButton)
                    {
                        ((RadioButton)c).Checked = false;

                        if (c.Text.Contains(':') && c.Text.Split(':')[0].Equals(value))
                        {
                            ((RadioButton)c).Checked = true;
                            break;
                        }
                    }
                }
            }
        }

        public void Clear()
        {
            foreach (Control c in this.Controls)
            {
                if (c is RadioButton)
                {
                    ((RadioButton)c).Checked = false;
                }
            }
        }
    }

    class DPCCheckPanel : Panel
    {
        bool _ReadOnly = false;

        public bool ReadOnly
        {
            get
            {
                return this._ReadOnly;
            }
            set
            {
                this._ReadOnly = value;

                foreach (Control c in this.Controls)
                {
                    if (c is CheckBox)
                    {
                        ((CheckBox)c).Enabled = !value;
                    }
                }
            }
        }

        public string Value
        {
            get
            {
                // チェックされている値を連結する
                string s = "";

                foreach (Control c in this.Controls)
                {
                    if (c is CheckBox && ((CheckBox)c).Checked)
                    {
                        if (c.Text.Contains(':'))
                        {
                            s += c.Text.Split(':')[0];
                        }
                    }
                }

                return s;
            }
            set
            {
                // 値が value に含まれる場合は ON にする
                foreach (Control c in this.Controls)
                {
                    if (c is CheckBox)
                    {
                        ((CheckBox)c).Checked = false;

                        if (c.Text.Contains(':') && value.Contains(c.Text.Split(':')[0]))
                        {
                            ((CheckBox)c).Checked = true;
                        }
                    }
                }
            }
        }

        public void Clear()
        {
            foreach (Control c in this.Controls)
            {
                if (c is CheckBox)
                {
                    ((CheckBox)c).Checked = false;
                }
            }
        }
    }

    class DPCComboBox : ComboBox
    {
        public string Value
        {
            get
            {
                string s = "";

                if (this.SelectedItem != null && this.SelectedItem is DPCSubItem2)
                {
                    s = ((DPCSubItem2)this.SelectedItem).Value;
                }
                else if (this.Text.Length > 0)
                {
                    // DropDown で自由入力の場合
                    s = this.Text;
                }

                return s;
            }
            set
            {
                this.Text = "";

                foreach (DPCSubItem2 s in this.Items)
                {
                    if (s.Value.Equals(value))
                    {
                        this.SelectedItem = s;
                        break;
                    }
                }

                if (this.Text.Length == 0 && value.Length > 0)
                {
                    DPCSubItem2 item = new DPCSubItem2();
                    item.Text = value;

                    this.Items.Add(item);
                    this.SelectedItem = item;
                }
            }
        }
    }
}
