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
    public partial class FormBaseOrder : StdForm1
    {
        public FormBaseOrder()
        {
            InitializeComponent();
        }

        private void FormBaseOrder_Load(object sender, EventArgs e)
        {
            this.Pat = AppStat.CurrentPat;

            Dictionary<string, BaseOrderMaster> master_dict = BaseOrderMaster.GetDict();

            int h = 0;

            Label lb_name = new Label();
            lb_name.Name = "lb_name";
            lb_name.AutoEllipsis = true;
            lb_name.AutoSize = false;
            lb_name.Width = 180;
            lb_name.Location = new Point(10, h);
            lb_name.TextAlign = ContentAlignment.MiddleLeft;
            lb_name.Text = "名称";
            Panel1.Controls.Add(lb_name);

            Label lb_value1 = new Label();
            lb_value1.Name = "lb_value1";
            lb_value1.AutoEllipsis = true;
            lb_value1.AutoSize = false;
            lb_value1.Width = 190;
            lb_value1.Location = new Point(200, h);
            lb_value1.TextAlign = ContentAlignment.MiddleLeft;
            lb_value1.Text = "条件";
            Panel1.Controls.Add(lb_value1);

            Label lb_value2 = new Label();
            lb_value2.Name = "lb_value1";
            lb_value2.AutoEllipsis = true;
            lb_value2.AutoSize = false;
            lb_value2.Width = 190;
            lb_value2.Location = new Point(400, h);
            lb_value2.TextAlign = ContentAlignment.MiddleLeft;
            lb_value2.Text = "指示";
            Panel1.Controls.Add(lb_value2);

            h += 24;

            foreach (string key in master_dict.Keys)
            {
                BaseOrderMaster obj = master_dict[key];

                Label lb = new Label();
                lb.Name = "lb_" + obj.SEQ.ToString();
                lb.AutoEllipsis = true;
                lb.AutoSize = false;
                lb.Width = 180;
                lb.BorderStyle = BorderStyle.FixedSingle;
                lb.Location = new Point(10, h);
                lb.TextAlign = ContentAlignment.MiddleLeft;
                lb.Text = obj.Name1;

                Panel1.Controls.Add(lb);

                ComboBox cb1 = new ComboBox();
                cb1.Name = "cb1_" + obj.SEQ.ToString();
                cb1.Width = 190;
                cb1.Location = new Point(200, h);

                foreach (string s1 in obj.List1)
                {
                    cb1.Items.Add(s1);
                }

                Panel1.Controls.Add(cb1);

                ComboBox cb2 = new ComboBox();
                cb2.Name = "cb2_" + obj.SEQ.ToString();
                cb2.Width = 190;
                cb2.Location = new Point(400, h);

                foreach (string s2 in obj.List2)
                {
                    cb2.Items.Add(s2);
                }

                Panel1.Controls.Add(cb2);

                h += 24;
            }

            h += 2;

            TextBox tb = new TextBox();
            tb.Name = "tb";
            tb.Multiline = true;
            tb.ScrollBars = ScrollBars.Vertical;
            tb.Size = new Size(600, 60);
            tb.Location = new Point(10, h);

            Panel1.Controls.Add(tb);
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

            List<BaseOrderData> list = BaseOrderData.GetList(this.Pat.Id);

            foreach (BaseOrderData obj in list)
            {
                this.DateBox1.Items.Add(DateTimeAgent.DateFormat(obj.OrderDate.ToString(), DateTimeAgent.DateFormatKind.WLONG));
            }

            if (list.Count > 0)
            {
                int i = 0;

                // 現時点のデータまでさかのぼる
                for (i = 0; i < this.DateBox1.Items.Count; i++)
                {
                    string s = this.DateBox1.Items[i].ToString();

                    if (DateTime.Parse(s) < DateTime.Now)
                    {
                        break;
                    }
                }

                // 過去に登録されたデータが無かった場合は、最も近い未来のデータを表示する
                if (i >= this.DateBox1.Items.Count)
                {
                    i--;
                }

                this.DateBox1.SelectedIndex = i;
            }
        }

        void DataClear()
        {
            foreach (Control c in Panel1.Controls)
            {
                if (c is ComboBox || c is TextBox)
                {
                    c.Text = "";
                }
            }
        }

        void DataShow()
        {
            this.DataClear();

            if (this.Pat.Id.Length == 0 || this.DateBox1.Text.Length == 0)
            {
                return;
            }

            int crit_date = 0;
            int.TryParse(this.DateBox1.Text.Substring(0, 10).Replace("/", ""), out crit_date);

            BaseOrderData obj = BaseOrderData.LoadPrevByDate(this.Pat.Id, crit_date);

            foreach (int i in obj.ItemDict.Keys)
            {
                BaseOrderDataItem item = obj.ItemDict[i];

                if (Panel1.Controls.ContainsKey("lb_" + i))
                {
                    Control lb = Panel1.Controls["lb_" + i];
                    lb.Text = item.Name;
                }

                if (Panel1.Controls.ContainsKey("cb1_" + i))
                {
                    Control cb1 = Panel1.Controls["cb1_" + i];
                    cb1.Text = item.Value1;
                }

                if (Panel1.Controls.ContainsKey("cb2_" + i))
                {
                    Control cb2 = Panel1.Controls["cb2_" + i];
                    cb2.Text = item.Value2;
                }
            }

            if (Panel1.Controls.ContainsKey("tb"))
            {
                Panel1.Controls["tb"].Text = obj.Cont1;
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
            BaseOrderData obj = new BaseOrderData();
            obj.PtId = this.Pat.Id;
            obj.OrderDate = int.Parse(this.DatePicker1.Value.ToString("yyyyMMdd"));

            for (int i = 1; i <= 20; i++)
            {
                BaseOrderDataItem item = new BaseOrderDataItem();
                item.SEQ = i;

                if (Panel1.Controls.ContainsKey("lb_" + i.ToString()))
                {
                    Control c = Panel1.Controls["lb_" + i.ToString()];
                    item.Name = c.Text;
                }

                if (Panel1.Controls.ContainsKey("cb1_" + i.ToString()))
                {
                    Control c = Panel1.Controls["cb1_" + i.ToString()];
                    item.Value1 = c.Text;
                }

                if (Panel1.Controls.ContainsKey("cb2_" + i.ToString()))
                {
                    Control c = Panel1.Controls["cb2_" + i.ToString()];
                    item.Value2 = c.Text;
                }

                if (!obj.ItemDict.ContainsKey(i))
                {
                    obj.ItemDict.Add(i, item);
                }
            }

            if (Panel1.Controls.ContainsKey("tb"))
            {
                obj.Cont1 = Panel1.Controls["tb"].Text;
            }

            // 適用開始日の直前のデータを取得する
            BaseOrderData obj1 = BaseOrderData.LoadPrevByDate(this.Pat.Id, obj.OrderDate);

            // 適用開始日の直前のデータとの変更点をチェックする
            string msg = "";

            if (obj1.OrderDate > 0 && obj1.OrderDate <= obj.OrderDate)
            {
                // 過去のデータがある場合

                string tmp_msg = "";

                // 直前のデータと比較する
                for (int i = 1; i <= 20; i++)
                {
                    string tmp_msg1 = "";
                    string tmp_msg2 = "";

                    // 約束条件に変更がある場合
                    if (!obj1.ItemDict[i].Value1.Trim().Equals(obj.ItemDict[i].Value1.Trim()))
                    {
                        if (obj1.ItemDict[i].Value1.Trim().Length > 0)
                        {
                            tmp_msg1 += " 条件「" + obj1.ItemDict[i].Value1.Trim() + "」→「" + obj.ItemDict[i].Value1.Trim() + "」";
                        }
                        else
                        {
                            tmp_msg1 += " 条件「" + obj.ItemDict[i].Value1.Trim() + "」追加";
                        }
                    }

                    // 約束指示に変更がある場合
                    if (!obj1.ItemDict[i].Value2.Trim().Equals(obj.ItemDict[i].Value2.Trim()))
                    {
                        if (obj1.ItemDict[i].Value2.Trim().Length > 0)
                        {
                            tmp_msg2 += " 指示「" + obj1.ItemDict[i].Value2.Trim() + "」→「" + obj.ItemDict[i].Value2.Trim() + "」";
                        }
                        else
                        {
                            tmp_msg2 += " 指示「" + obj.ItemDict[i].Value2.Trim() + "」追加";
                        }
                    }

                    // 約束条件・約束指示のいずれかに変更がある場合
                    if (tmp_msg1.Length > 0 || tmp_msg2.Length > 0)
                    {
                        tmp_msg += "【" + obj.ItemDict[i].Name + "】" + tmp_msg1;

                        if (tmp_msg1.Length > 0)
                        {
                            tmp_msg += ", ";
                        }
                        
                        tmp_msg += tmp_msg2 + Environment.NewLine;
                    }
                }

                if (!obj1.Cont1.Trim().Equals(obj.Cont1.Trim()))
                {
                    tmp_msg += "【フリーコメント】 画面を参照のこと" + Environment.NewLine;
                }

                if (tmp_msg.Length > 0)
                {
                    if (MessageBox.Show("登録します。よろしいですか？", "確認", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                    {
                        // 登録処理を行う。
                        sr = obj.Save();
                    }

                    msg = "基本指示を次の通り変更しました。" + Environment.NewLine;
                    msg += "【適用開始日】 " + this.DatePicker1.Value.ToString("yyyy/MM/dd(ddd)") + Environment.NewLine;
                    msg += tmp_msg;
                }
                else
                {
                    if (MessageBox.Show("変更箇所がありません。本当に登録しますか？", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        // 登録処理を行う。
                        sr = obj.Save();
                    }
                }
            }
            else
            {
                // 過去のデータが無い場合

                if (MessageBox.Show("登録します。よろしいですか？", "確認", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                {
                    // 登録処理を行う。
                    sr = obj.Save();
                }

                msg = "基本指示を新規登録しました。" + Environment.NewLine;
                msg += "【適用開始日】 " + this.DatePicker1.Value.ToString("yyyy/MM/dd(ddd)") + Environment.NewLine;
            }

            ChangeBox1.Text = msg;
            this.PtShow();

            return sr;
        }

        /// <summary>
        /// 指定された基本指示を削除する。
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

            BaseOrderData obj = new BaseOrderData();
            obj.PtId = pt_id;
            int.TryParse(order_date.Substring(0, 10).Replace("/", ""), out obj.OrderDate);

            if (MessageBox.Show("削除します。よろしいですか？", "確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // 登録処理を行う。
                sr = obj.Delete();
            }

            string msg = "";

            msg = "基本指示を削除しました。" + Environment.NewLine;
            msg += "【適用開始日】 " + order_date + Environment.NewLine;

            ChangeBox1.Text = msg;
            this.PtShow();

            return sr;
        }

        private void DateBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DataShow();
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
