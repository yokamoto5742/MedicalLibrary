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
    public partial class FormNursingOrder : StdForm1
    {
        public FormNursingOrder()
        {
            InitializeComponent();
        }

        private void FormNursingOrder_Load(object sender, EventArgs e)
        {
            this.Pat = AppStat.CurrentPat;

            Dictionary<int, Dictionary<int, NursingOrderMaster>> dict = NursingOrderMaster.Dict;

            int h = 0;

            // FlowLayoutPanel の最大必要幅
            int w = 0;

            foreach (int code1 in dict.Keys)
            {
                Dictionary<int, NursingOrderMaster> tmp_dict = dict[code1];

                string title = "";

                FlowLayoutPanel fp = new FlowLayoutPanel();
                fp.Name = "fp_" + code1.ToString();
                fp.Location = new Point(200, h);
                fp.Anchor = AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Left;
                fp.Size = new Size(Panel1.Width - this.Location.X - 20, 23);
                fp.FlowDirection = FlowDirection.LeftToRight;
                fp.AutoScroll = false;

                // 1: RadioButton, 2: CheckBox, 3: TextBox
                int kind = 1;

                foreach (int code2 in tmp_dict.Keys)
                {
                    NursingOrderMaster obj = tmp_dict[code2];

                    // 先頭のタイトル名を採用する
                    if (title.Length == 0)
                    {
                        title = obj.Title;
                    }

                    kind = obj.Kind;

                    if (kind.Equals(1))
                    {
                        // RadioButton
                        RadioButton b = new RadioButton();
                        b.Width = b.PreferredSize.Width;
                        b.AutoSize = true;
                        b.Name = "b_" + code1.ToString() + "_" + code2.ToString();
                        b.Text = obj.Text1;
                        b.Tag = obj;

                        fp.Controls.Add(b);
                    }
                    else if (kind.Equals(2))
                    {
                        // CheckBox
                        CheckBox b = new CheckBox();
                        b.AutoSize = true;
                        b.Width = b.PreferredSize.Width;
                        b.Name = "b_" + code1.ToString() + "_" + code2.ToString();
                        b.Text = obj.Text1;
                        b.Tag = obj;

                        fp.Controls.Add(b);
                    }
                    else
                    {
                        // TextBox
                        TextBox b = new TextBox();

                        if (obj.Limit > 5)
                        {
                            b.Width = obj.Limit * 10;
                        }
                        else
                        {
                            b.Width = 60;
                        }

                        b.Name = "b_" + code1.ToString() + "_" + code2.ToString();
                        b.Tag = obj;

                        fp.Controls.Add(b);
                    }

                    // 選択肢名称２がある場合
                    if (obj.Text2.Length > 0)
                    {
                        Label b = new Label();
                        b.AutoSize = true;
                        b.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
                        b.Name = "b2_" + code1.ToString() + "_" + code2.ToString();
                        b.TextAlign = ContentAlignment.BottomLeft;
                        b.Text = obj.Text2;

                        fp.Controls.Add(b);
                    }

                    // テキスト有無（文字数制限）がある場合
                    if (obj.Limit > 0)
                    {
                        Label b = new Label();
                        b.AutoSize = true;
                        b.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
                        b.Name = "b3_" + code1.ToString() + "_" + code2.ToString();
                        b.TextAlign = ContentAlignment.BottomLeft;
                        b.Text = "(" + obj.Limit.ToString() + "文字まで)";

                        fp.Controls.Add(b);
                    }
                }

                // FlowLayoutPanel の最適幅
                int tmp_w = fp.PreferredSize.Width;

                // Panel1 内の FlowLayoutPanel の最大値を取得する
                if (tmp_w > w)
                {
                    w = tmp_w;
                }

                Panel1.Controls.Add(fp);

                Label lb = new Label();
                lb.Name = "lb_" + code1.ToString();
                lb.AutoEllipsis = true;
                lb.AutoSize = false;
                lb.Width = 180;
                lb.BorderStyle = BorderStyle.FixedSingle;
                lb.Location = new Point(10, h);
                lb.TextAlign = ContentAlignment.MiddleLeft;
                lb.Text = title;
                Panel1.Controls.Add(lb);

                h += 24;
            }

            // FlowLayoutPanel の最大幅に応じて、フォームの幅を調整する
            this.Width = w + 180 + 180 + 140;
        }

        public override void FontSet(AppFont f)
        {
            base.FontSet(f);
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            this.PtShow();
            //            this.ListShow();
        }

        void PtClear()
        {
            this.DateBox1.Items.Clear();
            this.DataClear();
        }

        void PtShow()
        {
            this.PtClear();

            List<int> list = NursingOrderData.GetDateList(this.Pat.Id);

            foreach (int order_date in list)
            {
                this.DateBox1.Items.Add(DateTimeAgent.DateFormat(order_date, DateTimeAgent.DateFormatKind.WLONG));
            }
        }

        private void DateBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DataShow();
        }

        void DataClear()
        {
            foreach (Control c in Panel1.Controls)
            {
                if (c is FlowLayoutPanel)
                {
                    FlowLayoutPanel fp = (FlowLayoutPanel)c;

                    foreach (Control cc in fp.Controls)
                    {
                        if (cc is RadioButton)
                        {
                            ((RadioButton)cc).Checked = false;
                        }
                        else if (cc is CheckBox)
                        {
                            ((CheckBox)cc).Checked = false;
                        }
                        else if (cc is TextBox)
                        {
                            cc.Text = "";
                        }
                    }
                }
            }
        }

        void DataShow()
        {
            this.DataClear();

            int crit_date = 0;
            int.TryParse(this.DateBox1.Text.Substring(0, 10).Replace("/", ""), out crit_date);

            Dictionary<int, Dictionary<int, NursingOrderData>> data_dict = NursingOrderData.LoadByDate(this.Pat.Id, crit_date);

            Dictionary<int, Dictionary<int, NursingOrderMaster>> master_dict = NursingOrderMaster.Dict;

            foreach (int code1 in master_dict.Keys)
            {
                if (data_dict.ContainsKey(code1))
                {
                    // その指示コードにデータがあることを示す

                    foreach (int code2 in master_dict[code1].Keys)
                    {
                        if (data_dict[code1].ContainsKey(code2))
                        {
                            // その連番にデータがあることを示す

                            NursingOrderMaster master = master_dict[code1][code2];

                            if (Panel1.Controls.ContainsKey("fp_" + master.Code1))
                            {
                                FlowLayoutPanel fp = (FlowLayoutPanel)(Panel1.Controls["fp_" + master.Code1]);

                                if (fp.Controls.ContainsKey("b_" + master.Code1 + "_" + master.Code2))
                                {
                                    Control c = fp.Controls["b_" + master.Code1 + "_" + master.Code2];

                                    if (master.Kind.Equals(1))
                                    {
                                        ((RadioButton)c).Checked = true;
                                    }
                                    else if (master.Kind.Equals(2))
                                    {
                                        ((CheckBox)c).Checked = true;
                                    }
                                    else if (master.Kind.Equals(3))
                                    {
                                        c.Text = data_dict[code1][code2].Cont1;
                                    }
                                }
                            }
                        }

                    }
                }
            }

        }

        /// <summary>
        /// 指定された適用開始日で登録する。
        /// </summary>
        StdReturn DataSave()
        {
            StdReturn sr = new StdReturn();

            ChangeBox1.Clear();

            // 入力されたデータを取得する
            string pt_id = this.Pat.Id;
            int order_date = int.Parse(this.DatePicker1.Value.ToString("yyyyMMdd"));

            // 既存のデータがあれば削除する
            sr.Msgs.Add(NursingOrderData.Delete(pt_id, order_date).Msg);

            // パネルからデータを取得して登録する
            foreach (Control pc in Panel1.Controls)
            {
                if (!pc.GetType().Name.StartsWith("FlowLayoutPanel"))
                {
                    continue;
                }

                FlowLayoutPanel fp = (FlowLayoutPanel)pc;

                foreach (Control c in fp.Controls)
                {
                    // 登録するデータでない場合は飛ばす
                    if (!c.Name.StartsWith("b_"))
                    {
                        continue;
                    }

                    NursingOrderMaster m = (NursingOrderMaster)(c.Tag);

                    NursingOrderData obj = new NursingOrderData();
                    obj.PtId = pt_id;
                    obj.OrderDate = order_date;
                    obj.Code1 = m.Code1;
                    obj.Code2 = m.Code2;
                    obj.Value1 = 1;

                    if (c.GetType().Name.StartsWith("RadioButton"))
                    {
                        RadioButton b = (RadioButton)c;

                        if (b.Checked)
                        {
                            sr.Msgs.Add(obj.Save().Msg);
                        }
                    }
                    else if (c.GetType().Name.StartsWith("CheckBox"))
                    {
                        CheckBox b = (CheckBox)c;

                        if (b.Checked)
                        {
                            sr.Msgs.Add(obj.Save().Msg);
                        }
                    }
                    else if (c.GetType().Name.StartsWith("TextBox"))
                    {
                        TextBox b = (TextBox)c;

                        if (b.Text.Length > 0)
                        {
                            obj.Cont1 = b.Text;
                            sr.Msgs.Add(obj.Save().Msg);
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            ChangeBox1.Text = sr.Msg;
            this.PtShow();

            return sr;
        }

        /// <summary>
        /// 指定された指示を削除する。
        /// </summary>
        StdReturn DataDelete()
        {
            StdReturn sr = new StdReturn();

            ChangeBox1.Clear();

            // 削除するデータを取得する
            string pt_id = this.Pat.Id;
            string order_date = this.DateBox1.Text;

            if (pt_id.Length == 0 || order_date.Length == 0)
            {
                return sr;
            }

            int crit_date = 0;
            int.TryParse(order_date.Substring(0, 10).Replace("/", ""), out crit_date);

            if (MessageBox.Show("削除します。よろしいですか？", "確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // 登録処理を行う。
                sr = NursingOrderData.Delete(pt_id, crit_date);
            }

            string msg = "";

            msg = "看護指示を削除しました。" + Environment.NewLine;
            msg += "【適用開始日】 " + order_date + Environment.NewLine;

            ChangeBox1.Text = msg;
            this.PtShow();

            return sr;
        }

        private void SaveButton1_Click(object sender, EventArgs e)
        {
            StdReturn sr = this.DataSave();

            if (sr.MsgExist)
            {
                MessageBox.Show(sr.Msg);
            }
        }

        private void DeleteButton1_Click(object sender, EventArgs e)
        {
            StdReturn sr = this.DataDelete();

            if (sr.MsgExist)
            {
                MessageBox.Show(sr.Msg);
            }
        }
    }
}
