using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class CtrlKarteTemplate1 : UserControl
    {
        CtrlSoapWrite1 SoapWrite1;

        public KarteTemplate KarteTemplate1
        {
            get
            {
                return (KarteTemplate)this.TemplateBox1.SelectedItem;
            }
        }

        List<KarteTemplateComp> CompList = new List<KarteTemplateComp>();

        public CtrlKarteTemplate1()
        {
            InitializeComponent();
        }

        public void Init(CtrlSoapWrite1 soap_write)
        {
            this.SoapWrite1 = soap_write;

            foreach (Section obj in Dict.SectionDict.Values)
            {
                if (obj.Kind1.Equals(9))
                {
                    continue;
                }

                this.SectionBox1.Items.Add(obj);
            }

            foreach (Dept obj in Dict.DeptDict.Values)
            {
                this.DeptBox1.Items.Add(obj);
            }

            this.SectionBox1.SelectedIndex = 0;
            this.DeptBox1.SelectedIndex = 0;

            this.ListShow();
        }

        void ListShow()
        {
            if (this.SectionBox1.SelectedItem == null || this.DeptBox1.SelectedItem == null)
            {
                return;
            }

            this.TemplateBox1.Items.Clear();


            List<KarteTemplate> list = KarteTemplate.GetList(((Section)this.SectionBox1.SelectedItem).Code.ToString(), ((Dept)this.DeptBox1.SelectedItem).Code.ToString());

            foreach (KarteTemplate obj in list)
            {
                // 階層キー１・２ともに 0 のものは飛ばす
                if (obj.Key1.Equals("0") && obj.Key2.Equals("0"))
                {
                    continue;
                }

                // 種別が 1 でも 2 でも無いものは飛ばす
                if (!obj.Kind.Equals("1") && !obj.Kind.Equals("2"))
                {
                    continue;
                }

                this.TemplateBox1.Items.Add(obj);
            }
        }

        void PanelShow()
        {
            if (this.TemplateBox1.SelectedItem == null)
            {
                return;
            }

            KarteTemplate t = (KarteTemplate)this.TemplateBox1.SelectedItem;

            this.CompList = KarteTemplateComp.GetList(t.Code, t.Kind);

            this.MakePanel();
        }

        private void SectionBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SectionBox1.SelectedItem != null)
            {
                this.ListShow();
            }
        }

        private void DeptBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.DeptBox1.SelectedItem != null)
            {
                this.ListShow();
            }
        }

        private void TemplateBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TemplateBox1.SelectedItem != null)
            {
                this.PanelShow();
            }
        }

        void MakePanel()
        {
            KarteTemplateCell[,] cells = KarteTemplateCell.Get(this.CompList);

            this.KarteTemplatePanel1.Controls.Clear();

            foreach (KarteTemplateComp obj in this.CompList)
            {
                if (obj.Attr1.Kind.Equals("1"))
                {
                    // 数値
                    TextBox cb = new TextBox();
                    cb.Name = obj.Pos;
                    cb.ImeMode = System.Windows.Forms.ImeMode.Disable;
                    cb.TextAlign = HorizontalAlignment.Right;
                    cb.Width = (int)obj.Width;
                    cb.Height = (int)obj.Height;
                    cb.ForeColor = obj.Attr1.ForeColorValue;
                    cb.BackColor = obj.Attr1.BackColorValue;
                    //                    cb.Font = obj.Attr1.FontValue;
                    cb.Tag = obj;
                    cb.Location = new Point((int)(cells[obj.Row - 1, obj.Col - 1].X), (int)(cells[obj.Row - 1, obj.Col - 1].Y));

                    this.KarteTemplatePanel1.Controls.Add(cb);
                }
                else if (obj.Attr1.Kind.Equals("2"))
                {
                    if (obj.Attr1.Embed.Equals("1") || obj.Cont1.Name.Length > 0)
                    {
                        // 「埋め込み」または最初から文字列が入っている場合

                        // ラベル
                        Label cb = new Label();
                        cb.Name = obj.Pos;
                        cb.AutoEllipsis = true;
                        cb.Padding = new Padding(3, 3, 3, 3);
                        cb.TextAlign = ContentAlignment.MiddleLeft;

                        if (obj.Cont1.Name.Length > 0)
                        {
                            cb.Text = obj.Cont1.Name;
//                            cb.Text = obj.Cont1.Name + " [" + obj.Row + "," + obj.Col + "]";
                        }

                        cb.Width = (int)obj.Width;
                        cb.Height = (int)obj.Height;
                        cb.ForeColor = obj.Attr1.ForeColorValue;
                        cb.BackColor = obj.Attr1.BackColorValue;
//                        cb.Font = obj.Attr1.FontValue;
                        cb.Tag = obj;
                        cb.Location = new Point((int)(cells[obj.Row - 1, obj.Col - 1].X), (int)(cells[obj.Row - 1, obj.Col - 1].Y));

                        // セルオーバーが 1 の場合
                        if (this.KarteTemplate1.CellOver && cb.PreferredSize.Width > cb.Width)
                        {
                            cb.Width = cb.PreferredSize.Width;
                        }

                        this.KarteTemplatePanel1.Controls.Add(cb);
                    }
                    else
                    {
                        // テキストボックス
                        TextBox cb = new TextBox();
                        cb.Name = obj.Pos;
//                        cb.Text = obj.Cont1.Name + " [" + obj.Row + "," + obj.Col + "]";
                        cb.Text = obj.Cont1.Name;
                        cb.Width = (int)obj.Width;
                        cb.Height = (int)obj.Height;
                        cb.ForeColor = obj.Attr1.ForeColorValue;
                        cb.BackColor = obj.Attr1.BackColorValue;
//                        cb.Font = obj.Attr1.FontValue;
                        cb.Tag = obj;
                        cb.Location = new Point((int)(cells[obj.Row - 1, obj.Col - 1].X), (int)(cells[obj.Row - 1, obj.Col - 1].Y));

                        // セルオーバーが 1 の場合
                        if (this.KarteTemplate1.CellOver && cb.PreferredSize.Width > cb.Width)
                        {
                            cb.Width = cb.PreferredSize.Width;
                        }

                        this.KarteTemplatePanel1.Controls.Add(cb);
                    }
                }
                else if (obj.Attr1.Kind.Equals("3"))
                {
                    // チェックボックス
                    CheckBox cb = new CheckBox();
                    cb.Name = obj.Pos;
                    cb.AutoEllipsis = true;
                    cb.Padding = new Padding(3, 3, 3, 3);
                    cb.Text = obj.Cont1.Name;
//                    cb.Text = obj.Cont1.Name + " [" + obj.Row + "," + obj.Col + "]";
                    cb.Width = (int)obj.Width;
                    cb.Height = (int)obj.Height;
                    cb.ForeColor = obj.Attr1.ForeColorValue;
                    cb.BackColor = obj.Attr1.BackColorValue;
//                    cb.Font = obj.Attr1.FontValue;
                    cb.Tag = obj;
                    cb.Location = new Point((int)(cells[obj.Row - 1, obj.Col - 1].X), (int)(cells[obj.Row - 1, obj.Col - 1].Y));

                    cb.CheckedChanged += new EventHandler(CheckBox_CheckedChanged);

                    this.KarteTemplatePanel1.Controls.Add(cb);
                }
                else if (obj.Attr1.Kind.Equals("4"))
                {
                    // コンボボックス
                    ComboBox cb = new ComboBox();
                    cb.Name = obj.Pos;
                    cb.Width = (int)obj.Width;
                    cb.Height = (int)obj.Height;
                    cb.ForeColor = obj.Attr1.ForeColorValue;
                    cb.BackColor = obj.Attr1.BackColorValue;
//                    cb.Font = obj.Attr1.FontValue;
                    cb.Tag = obj;
                    cb.Location = new Point((int)(cells[obj.Row - 1, obj.Col - 1].X), (int)(cells[obj.Row - 1, obj.Col - 1].Y));

                    foreach (KarteTemplateCont c in obj.ContList1)
                    {
                        if (c.Name.Length > 0)
                        {
                            cb.Items.Add(c.Name);
                        }
                    }

                    this.KarteTemplatePanel1.Controls.Add(cb);
                }
                else if (obj.Attr1.Kind.Equals("5"))
                {
                    // トグルボタン
                    CheckBox cb = new CheckBox();
                    cb.Name = obj.Pos;
                    cb.Text = obj.Cont1.Name + " [" + obj.Row + "," + obj.Col + "]";
                    cb.Appearance = Appearance.Button;
                    cb.Width = (int)obj.Width;
                    cb.Height = (int)obj.Height;
                    cb.ForeColor = obj.Attr1.ForeColorValue;
                    cb.BackColor = Color.LightGray;
//                    cb.Font = obj.Attr1.FontValue;
                    cb.Tag = obj;
                    cb.Location = new Point((int)(cells[obj.Row - 1, obj.Col - 1].X), (int)(cells[obj.Row - 1, obj.Col - 1].Y));

                    cb.Checked = obj.Checked;

                    cb.CheckedChanged += new EventHandler(Toggle_CheckedChanged);

                    this.KarteTemplatePanel1.Controls.Add(cb);
                }
            }

            this.MovePanel();
        }

        void MovePanel()
        {
            KarteTemplateCell[,] cells = KarteTemplateCell.Get(this.CompList);

            int x = this.KarteTemplatePanel1.HorizontalScroll.Value;
            int y = this.KarteTemplatePanel1.VerticalScroll.Value;

            foreach (KarteTemplateComp obj in this.CompList)
            {
                foreach (Control c in this.KarteTemplatePanel1.Controls)
                {
                    KarteTemplateComp v = (KarteTemplateComp)(c.Tag);

                    if (obj == v)
                    {
                        c.Location = new Point((int)(cells[obj.Row - 1, obj.Col - 1].X) - x, (int)(cells[obj.Row - 1, obj.Col - 1].Y) - y);

                        if (!cells[obj.Row - 1, obj.Col - 1].Visible)
                        {
                            c.Visible = false;
                        }
                        else
                        {
                            c.Visible = true;
                        }

                        break;
                    }
                }
            }

            this.KarteTemplatePanel1.Refresh();
        }

        void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // グループ化設定されている場合、これが ON になれば、同一グループのチェックボックス・トグルボタンはすべて OFF になる。
            CheckBox cb = (CheckBox)sender;
            KarteTemplateComp v = (KarteTemplateComp)(cb.Tag);
            Panel panel = (Panel)(cb.Parent);

            v.Checked = cb.Checked;

            if (cb.Checked)
            {
                // 同一グループは OFF にする
                foreach (List<KarteTemplateCompGroup> group_list in v.GroupList)
                {
                    foreach (KarteTemplateCompGroup obj in group_list)
                    {
                        if (obj.Pos.Equals(v.Pos))
                        {
                            // 自分自身の場合は飛ばす
                            continue;
                        }

                        if (panel.Controls.ContainsKey(obj.Pos))
                        {
                            if (panel.Controls[obj.Pos] is CheckBox)
                            {
                                ((CheckBox)(panel.Controls[obj.Pos])).Checked = false;
                            }
                        }
                    }
                }
            }
        }

        void Toggle_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox tg = (CheckBox)sender;
            KarteTemplateComp v = (KarteTemplateComp)(tg.Tag);
            Panel panel = (Panel)(tg.Parent);

            v.Checked = tg.Checked;

            if (tg.Checked)
            {
                tg.ForeColor = v.Attr1.BackColorValue;

                // 連動対象も ON にする
                foreach (KarteTemplateLink obj in v.ChildList)
                {
                    if (panel.Controls.ContainsKey(obj.ChildPos))
                    {
                        if (panel.Controls[obj.ChildPos].GetType().Name.Equals("CheckBox"))
                        {
                            CheckBox cb = (CheckBox)(panel.Controls[obj.ChildPos]);
                            cb.Checked = true;
                        }
                    }
                }

                // グループ化設定されている場合、これが ON になれば、同一グループのチェックボックス・トグルボタンはすべて OFF になる。
                foreach (List<KarteTemplateCompGroup> group_list in v.GroupList)
                {
                    foreach (KarteTemplateCompGroup obj in group_list)
                    {
                        if (obj.Pos.Equals(v.Pos))
                        {
                            // 自分自身の場合は飛ばす
                            continue;
                        }

                        if (panel.Controls.ContainsKey(obj.Pos))
                        {
                            if (panel.Controls[obj.Pos] is CheckBox)
                            {
                                ((CheckBox)(panel.Controls[obj.Pos])).Checked = false;
                            }
                        }
                    }
                }
            }
            else
            {
                tg.ForeColor = v.Attr1.ForeColorValue;

                // 連動対象のうち、他の連動元がすべて OFF ならばチェックを外す
                foreach (KarteTemplateLink obj in v.ChildList)
                {
                    if (panel.Controls.ContainsKey(obj.ChildPos))
                    {
                        if (panel.Controls[obj.ChildPos].GetType().Name.Equals("CheckBox"))
                        {
                            CheckBox cb = (CheckBox)(panel.Controls[obj.ChildPos]);
                            KarteTemplateComp vc = (KarteTemplateComp)(cb.Tag);

                            bool other_on = false;

                            foreach (KarteTemplateLink vp in vc.ParentList)
                            {
                                if (panel.Controls.ContainsKey(vp.Pos))
                                {
                                    if (panel.Controls[vp.Pos].GetType().Name.Equals("CheckBox"))
                                    {
                                        CheckBox cp = (CheckBox)(panel.Controls[vp.Pos]);

                                        if (cp.Checked)
                                        {
                                            other_on = true;
                                            break;
                                        }
                                    }
                                }
                            }

                            if (!other_on)
                            {
                                cb.Checked = false;
                            }
                        }
                    }
                }
            }

            List<KarteTemplateComp> list = (List<KarteTemplateComp>)(panel.Tag);

            this.MovePanel();
        }

        public Dictionary<string, string> GetSoapDict()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            List<KarteTemplateWrite> iji_list = KarteTemplateWrite.GetList(this.KarteTemplate1.Code, this.KarteTemplate1.Kind);

            // 区切り連番
            int rp_num = 0;

            foreach (KarteTemplateWrite iji in iji_list)
            {
                if (this.KarteTemplatePanel1.Controls.ContainsKey(iji.Pos))
                {
                    // トリガーがあれば、それが ON かどうか確認する。OFF なら continue。
                    if (iji.TriggerList.Count > 0)
                    {
                        bool trigger_on = false;

                        foreach (KarteTemplateWrite tr_iji in iji.TriggerList)
                        {
                            Control tr_c = this.KarteTemplatePanel1.Controls[tr_iji.Pos];

                            if (tr_c is Label || tr_c is TextBox || tr_c is ComboBox)
                            {
                                if (tr_c.Text.Length > 0)
                                {
                                    trigger_on = true;
                                    break;
                                }
                            }
                            else if (tr_c is CheckBox)
                            {
                                if (((CheckBox)tr_c).Checked)
                                {
                                    trigger_on = true;
                                    break;
                                }
                            }
                        }

                        if (!trigger_on)
                        {
                            continue;
                        }
                    }

                    Control c = this.KarteTemplatePanel1.Controls[iji.Pos];

                    string s = "";

                    if (dict.ContainsKey(iji.SoapKind))
                    {
                        s = dict[iji.SoapKind];
                    }

                    if (c is Label || c is TextBox || c is ComboBox)
                    {
                        if (c.Text.Length > 0)
                        {
                            KarteTemplateComp comp = (KarteTemplateComp)(c.Tag);

                            if (!rp_num.Equals(iji.SEQ1))
                            {
                                rp_num = iji.SEQ1;
                            }

                            s += c.Text;

                            if (iji.Newline)
                            {
                                s += Environment.NewLine;
                            }
                        }
                    }
                    else if (c is CheckBox)
                    {
                        CheckBox cb = (CheckBox)c;

                        if (cb.Checked)
                        {
                            KarteTemplateComp comp = (KarteTemplateComp)(c.Tag);

                            if (!rp_num.Equals(iji.SEQ1))
                            {
                                rp_num = iji.SEQ1;
                            }

                            s += c.Text;

                            if (iji.Newline)
                            {
                                s += Environment.NewLine;
                            }
                        }
                    }

                    if (dict.ContainsKey(iji.SoapKind))
                    {
                        dict[iji.SoapKind] = s;
                    }
                    else
                    {
                        dict.Add(iji.SoapKind, s);
                    }
                }
            }

            // 内容クリア

            foreach (KarteTemplateWrite iji in iji_list)
            {
                if (this.KarteTemplatePanel1.Controls.ContainsKey(iji.Pos))
                {
                    Control c = this.KarteTemplatePanel1.Controls[iji.Pos];

                    if (c is TextBox || c is ComboBox)
                    {
                        c.Text = "";
                    }
                    else if (c is CheckBox)
                    {
                        ((CheckBox)c).Checked = false;
                    }

                    // 数量
                    if (iji.QtyRow > 0 || iji.QtyCol > 0)
                    {
                        Control cc = this.KarteTemplatePanel1.Controls[iji.QtyPos];

                        if (cc is Label || cc is TextBox || cc is ComboBox)
                        {
                            cc.Text = "";
                        }
                    }

                    // 日/回数
                    if (iji.TimesRow > 0 || iji.TimesCol > 0)
                    {
                        Control cc = this.KarteTemplatePanel1.Controls[iji.TimesPos];

                        if (cc is Label || cc is TextBox || cc is ComboBox)
                        {
                            cc.Text = "";
                        }
                    }
                }
            }


            return dict;
        }

        private void MakeButton1_Click(object sender, EventArgs e)
        {
            this.SoapWrite1.SoapWrite(this.GetSoapDict());
        }
    }
}
