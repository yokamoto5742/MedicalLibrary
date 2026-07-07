using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class FormKarteMessage1 : Form
    {
        public enum MailKind : int
        {
            Rcv = 1,
            Send = 2
        }

        /// <summary>
        /// 受信者リスト
        /// </summary>
        public List<TextBox> SendToList1 = new List<TextBox>();
        public List<TextBox> SendToNameList1 = new List<TextBox>();

        /// <summary>
        /// 未開封メールは太字
        /// </summary>
        Font mailFont0 = new Font("", 9, FontStyle.Bold);

        /// <summary>
        /// 開封済メールは細字
        /// </summary>
        Font mailFont1 = new Font("", 9);

        DataSet dSet = new DataSet();

        public FormKarteMessage1()
        {
            InitializeComponent();
        }

        public FormKarteMessage1(string pt_id, List<string> send_to_list = null, string title = "", string msg = "")
        {
            InitializeComponent();

            FormKarteMessage2 f = new FormKarteMessage2(pt_id, send_to_list, title, msg);
            f.Show(this);
        }

        private void FormKarteMessage1_Load(object sender, EventArgs e)
        {
            DataTable table = dSet.Tables.Add("受信メール");
            table.Columns.Add("ID");
            table.Columns.Add("氏名");
            table.Columns.Add("送信日");
            table.Columns.Add("送信時刻");
            table.Columns.Add("送信日時");
            table.Columns.Add("件名");
            table.Columns.Add("本文");
            table.Columns.Add("患者ID");
            table.Columns.Add("患者名");
            table.Columns.Add("重");
            table.Columns.Add("開封フラグ");
            table.Columns.Add("開封日");
            table.Columns.Add("開封時刻");
            table.Columns.Add("開封日時");
            table.Columns.Add("Obj", typeof(KarteMessage));

            table = dSet.Tables.Add("送信メール");
            table.Columns.Add("ID");
            table.Columns.Add("氏名");
            table.Columns.Add("送信日");
            table.Columns.Add("送信時刻");
            table.Columns.Add("送信日時");
            table.Columns.Add("件名");
            table.Columns.Add("本文");
            table.Columns.Add("患者ID");
            table.Columns.Add("患者名");
            table.Columns.Add("重");
            table.Columns.Add("開封フラグ");
            table.Columns.Add("開封日");
            table.Columns.Add("開封時刻");
            table.Columns.Add("開封日時");
            table.Columns.Add("Obj", typeof(KarteMessage));

            // Load メソッドで処理しなければ、最初の未読太字の表示がうまくいかない。

            this.KindButton1.Checked = true;
        }

        void SendToClear1()
        {
            this.Panel1.Controls.Clear();
            this.SendToList1.Clear();
            this.SendToNameList1.Clear();
        }

        void SendToMake1(int num)
        {
            this.SendToClear1();

            for (int i = 1; i <= num; i++)
            {
                TextBox box1 = new TextBox();
                box1.Name = "SendTo1" + i.ToString();
                box1.Size = new Size(60, 19);
                box1.Location = new Point(2, 2 + (i - 1) * 23);
                box1.TextAlign = HorizontalAlignment.Center;
                box1.BackColor = Color.LightYellow;
                box1.ReadOnly = true;

                this.Panel1.Controls.Add(box1);
                this.SendToList1.Add(box1);

                TextBox box2 = new TextBox();
                box2.Name = "SendToName1" + i.ToString();
                box2.Size = new Size(80, 19);
                box2.Location = new Point(65, 2 + (i - 1) * 23);
                box2.TextAlign = HorizontalAlignment.Center;
                box2.BackColor = Color.White;
                box2.ReadOnly = true;

                this.Panel1.Controls.Add(box2);
                this.SendToNameList1.Add(box2);
            }
        }

        void SendToAdd1(int num)
        {
            int c = 1;

            while (true)
            {
                if (!this.Panel1.Controls.ContainsKey("SendTo1" + c.ToString()))
                {
                    break;
                }

                c++;
            }

            for (int i = c; i < c + num; i++)
            {
                TextBox box1 = new TextBox();
                box1.Name = "SendTo1" + i.ToString();
                box1.Size = new Size(60, 19);
                box1.Location = new Point(2, 2 + (i - 1) * 23);
                box1.TextAlign = HorizontalAlignment.Center;
                box1.BackColor = Color.LightYellow;
                box1.ReadOnly = true;

                this.Panel1.Controls.Add(box1);
                this.SendToList1.Add(box1);

                TextBox box2 = new TextBox();
                box2.Name = "SendToName1" + i.ToString();
                box2.Size = new Size(80, 19);
                box2.Location = new Point(65, 2 + (i - 1) * 23);
                box2.TextAlign = HorizontalAlignment.Center;
                box2.BackColor = Color.White;
                box2.ReadOnly = true;

                this.Panel1.Controls.Add(box2);
                this.SendToNameList1.Add(box2);
            }
        }

        /// <summary>
        /// 「受信メール」ラジオボタンを選択した時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KindButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.ListShow1();
        }

        /// <summary>
        /// 「送信済みメール」ラジオボタンを選択した時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KindButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.ListShow2();
        }

        /// <summary>
        /// 受信メールを表示する
        /// </summary>
        private void ListShow1()
        {
            try
            {
                this.MailClear1();

                if (LoginUser.Id.Length > 0)
                {
                    this.ReadButton.Visible = true;
                    this.UnReadButton.Visible = true;
                    this.ReplyButton.Visible = true;
                    this.ReplyAllButton.Visible = true;

                    DataTable table = dSet.Tables["受信メール"];
                    table.Rows.Clear();

                    List<KarteMessage> list = KarteMessage.GetRcvList(LoginUser.Id);

                    foreach (KarteMessage obj in list)
                    {
                        DataRow r = table.NewRow();

                        r["ID"] = obj.FromCode;
                        r["氏名"] = obj.FromName;
                        r["送信日"] = obj.SendDate;
                        r["送信時刻"] = obj.SendTime;
                        r["送信日時"] = obj.SendDateTime;
                        r["件名"] = obj.Title;
                        r["本文"] = obj.Msg;
                        r["患者ID"] = obj.PtId;
                        r["患者名"] = obj.PtName;
                        r["重"] = obj.PriorityValue;
                        r["開封フラグ"] = (obj.ReadFlg == true) ? 1 : 0;
                        r["開封日"] = obj.ReadDate;
                        r["開封時刻"] = obj.ReadTime;
                        r["開封日時"] = obj.ReadDateTime;
                        r["Obj"] = obj;

                        table.Rows.Add(r);
                    }

                    DataView view = new DataView(table);

                    ListView1.DataSource = view;

                    this.ListFormat();

                    if (this.ListView1.RowCount > 0)
                    {
                        this.MailShow(0);
                    }
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                MessageBox.Show(err);
            }
        }

        /// <summary>
        /// 送信済みメールを表示する
        /// </summary>
        private void ListShow2()
        {
            try
            {
                if (LoginUser.Id.Length > 0)
                {
                    this.ReadButton.Visible = false;
                    this.UnReadButton.Visible = false;
                    this.ReplyButton.Visible = false;
                    this.ReplyAllButton.Visible = false;

                    DataTable table = dSet.Tables["送信メール"];
                    table.Rows.Clear();

                    List<KarteMessage> list = KarteMessage.GetSendList(LoginUser.Id);

                    foreach (KarteMessage obj in list)
                    {
                        DataRow r = table.NewRow();

                        r["ID"] = obj.ToCode;
                        r["氏名"] = obj.ToName;
                        r["送信日"] = obj.SendDate;
                        r["送信時刻"] = obj.SendTime;
                        r["送信日時"] = obj.SendDateTime;
                        r["件名"] = obj.Title;
                        r["本文"] = obj.Msg;
                        r["患者ID"] = obj.PtId;
                        r["患者名"] = obj.PtName;
                        r["重"] = obj.PriorityValue;
                        r["開封フラグ"] = (obj.ReadFlg == true) ? 1 : 0;
                        r["開封日"] = obj.ReadDate;
                        r["開封時刻"] = obj.ReadTime;
                        r["開封日時"] = obj.ReadDateTime;
                        r["Obj"] = obj;

                        table.Rows.Add(r);
                    }

                    DataView view = new DataView(table);

                    ListView1.DataSource = view;

                    this.ListFormat();

                    if (this.ListView1.RowCount > 0)
                    {
                        this.MailShow(0);
                    }
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                MessageBox.Show(err);
            }
        }

        private void ListFormat()
        {
            ListView1.Columns["ID"].HeaderText = "ID";
            ListView1.Columns["ID"].Width = 55;
            ListView1.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            ListView1.Columns["氏名"].HeaderText = "氏名";
            ListView1.Columns["氏名"].Width = 80;

            ListView1.Columns["送信日"].Visible = false;
            ListView1.Columns["送信時刻"].Visible = false;

            ListView1.Columns["送信日時"].HeaderText = "送信日時";
            ListView1.Columns["送信日時"].Width = 120;
            ListView1.Columns["送信日時"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            ListView1.Columns["件名"].HeaderText = "件名";
            ListView1.Columns["件名"].Width = 160;

            ListView1.Columns["本文"].Visible = false;

            ListView1.Columns["患者ID"].HeaderText = "患者ID";
            ListView1.Columns["患者ID"].Width = 70;
            ListView1.Columns["患者ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            ListView1.Columns["患者名"].HeaderText = "患者名";
            ListView1.Columns["患者名"].Width = 90;

            ListView1.Columns["重"].HeaderText = "重";
            ListView1.Columns["重"].Width = 25;
            ListView1.Columns["重"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView1.Columns["開封フラグ"].Visible = false;

            ListView1.Columns["開封日"].Visible = false;
            ListView1.Columns["開封時刻"].Visible = false;

            ListView1.Columns["開封日時"].HeaderText = "開封日時";
            ListView1.Columns["開封日時"].Width = 120;
            ListView1.Columns["開封日時"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            ListView1.Columns["Obj"].Visible = false;

            for (int i = 0; i < ListView1.RowCount; i++)
            {
                DataGridViewRow r = ListView1.Rows[i];

                if (r.Cells["開封フラグ"].Value.ToString().Equals("0"))
                {
                    r.DefaultCellStyle.Font = mailFont0;
                }
                else
                {
                    r.DefaultCellStyle.Font = mailFont1;
                }

                if (r.Cells["重"].Value.ToString().Equals("高"))
                {
                    r.Cells["重"].Style.ForeColor = Color.Red;
                }
            }
        }

        /// <summary>
        /// ListView の行にフォーカスが入った時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.MailShow(e.RowIndex);

            if (KindButton1.Checked == true)
            {
                this.timer1.Interval = 5000;
                this.timer1.Enabled = true;
            }
            else
            {
                this.timer1.Enabled = false;
            }
        }

        /// <summary>
        /// ListView の行からフォーカスが出た時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            this.timer1.Enabled = false;
        }

        /// <summary>
        /// メールの内容を表示する
        /// </summary>
        /// <param name="rowIndex"></param>
        private void MailShow(int rowIndex)
        {
            try
            {
                this.MailClear1();

                if (rowIndex >= 0)
                {
                    DataGridViewRow r = ListView1.Rows[rowIndex];

                    string from = r.Cells["ID"].Value.ToString();
                    string from_name = r.Cells["氏名"].Value.ToString();

                    if (this.KindButton2.Checked)
                    {
                        from = LoginUser.Id;
                        from_name = LoginUser.Name;
                    }

                    string send_date = r.Cells["送信日"].Value.ToString();
                    string send_time = r.Cells["送信時刻"].Value.ToString();

                    List<KarteMessage> list = KarteMessage.GetSameList(from, send_date, send_time, true);

                    this.SendToClear1();

                    int i = 1;

                    foreach (KarteMessage obj in list)
                    {
                        this.SendToAdd1(1);

                        if (this.Panel1.Controls.ContainsKey("SendTo1" + i.ToString()))
                        {
                            this.Panel1.Controls["SendTo1" + i.ToString()].Text = obj.ToCode;
                            this.Panel1.Controls["SendToName1" + i.ToString()].Text = obj.ToName;
                        }

                        i++;
                    }


                    FromNameBox1.Text = from_name;
                    PriorityBox1.Text = r.Cells["重"].Value.ToString();
                    PtBox1.Text = r.Cells["患者ID"].Value.ToString().Trim();
                    PtNameBox1.Text = r.Cells["患者名"].Value.ToString().Trim();
                    TitleBox1.Text = r.Cells["件名"].Value.ToString().Trim();
                    ContBox1.Text = r.Cells["本文"].Value.ToString().Trim();

                    if (this.PriorityBox1.Text.Equals("高"))
                    {
                        PriorityBox1.ForeColor = Color.Red;
                    }
                    else
                    {
                        PriorityBox1.ForeColor = Color.Black;
                    }
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                MessageBox.Show(err);
            }
        }

        /// <summary>
        /// 受信メールを開封済みまたは未開封にする
        /// </summary>
        /// <param name="read"></param>
        private void MailRead(bool read)
        {
            try
            {
                if (ListView1.CurrentRow != null && KindButton1.Checked == true)
                {
                    DataGridViewRow r = ListView1.CurrentRow;

                    string from = r.Cells["ID"].Value.ToString().Trim();
                    string send_date = r.Cells["送信日"].Value.ToString().Trim();
                    string send_time = r.Cells["送信時刻"].Value.ToString().Trim();

                    if (read)
                    {
                        if (r.Cells["開封フラグ"].Value.ToString().Equals("0"))
                        {
                            KarteMessage.Read(LoginUser.Id, from, send_date, send_time, true);

                            r.Cells["開封フラグ"].Value = "1";
                            r.Cells["開封日"].Value = DateTime.Now.ToString("yyyyMMdd");
                            r.Cells["開封時刻"].Value = DateTime.Now.ToString("HHmmss");
                            r.Cells["開封日時"].Value = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                        }

                        r.DefaultCellStyle.Font = mailFont1;
                    }
                    else
                    {
                        KarteMessage.Read(LoginUser.Id, from, send_date, send_time, false);

                        r.Cells["開封フラグ"].Value = "0";
                        r.Cells["開封日"].Value = "0";
                        r.Cells["開封時刻"].Value = "0";
                        r.Cells["開封日時"].Value = "";

                        r.DefaultCellStyle.Font = mailFont0;
                    }

                    ListView1.Refresh();
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                MessageBox.Show(err);
            }
        }

        /// <summary>
        /// メール表示欄をクリアする
        /// </summary>
        private void MailClear1()
        {
            this.SendToClear1();

            this.PtBox1.Text = "";
            this.PtNameBox1.Text = "";
            this.TitleBox1.Text = "";
            this.ContBox1.Text = "";
            this.PriorityBox1.Text = "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.MailRead(true);
            this.timer1.Enabled = false;
        }

        private void ReadButton_Click(object sender, EventArgs e)
        {
            this.MailRead(true);
        }

        private void UnReadButton_Click(object sender, EventArgs e)
        {
            this.MailRead(false);
        }

        private void ReadMenuItem1_Click(object sender, EventArgs e)
        {
            this.MailRead(true);
        }

        private void UnReadMenuItem1_Click(object sender, EventArgs e)
        {
            this.MailRead(false);
        }

        /// <summary>
        /// 返信メールを作成する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReplyButton_Click(object sender, EventArgs e)
        {
            if (ListView1.CurrentRow == null)
            {
                return;
            }

            DataGridViewRow r = ListView1.CurrentRow;
            KarteMessage km = (KarteMessage)r.Cells["Obj"].Value;

            string msg = "";

            if (MessageBox.Show("元のメッセージを引用しますか？", "確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (string s in km.Msg.Split('\n'))
                {
                    msg += "> " + s.Trim('\r') + Environment.NewLine;
                }
            }

            List<string> send_to_list = new List<string>();
            send_to_list.Add(km.FromCode);

            FormKarteMessage2 f = new FormKarteMessage2(km.PtId, send_to_list, "Re: " + km.Title, msg);
            f.Show(this);
        }

        /// <summary>
        /// 全員に返信する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReplyAllButton_Click(object sender, EventArgs e)
        {
            if (ListView1.CurrentRow == null)
            {
                return;
            }

            DataGridViewRow r = ListView1.CurrentRow;
            KarteMessage km = (KarteMessage)r.Cells["Obj"].Value;
/*
            string from = r.Cells["ID"].Value.ToString().Trim();
            string from_name = r.Cells["氏名"].Value.ToString().Trim();
            string send_date = r.Cells["送信日"].Value.ToString();
            string send_time = r.Cells["送信時刻"].Value.ToString();
*/
            List<KarteMessage> list = KarteMessage.GetSameList(km.FromCode, km.SendDate, km.SendTime, true);

            List<string> send_to_list = new List<string>();
            send_to_list.Add(km.FromCode);

            int i = 2;

            foreach (KarteMessage obj in list)
            {
                // 自分自身は含めない
                if (obj.ToCode.Equals(LoginUser.Id))
                {
                    continue;
                }

                // 送信者は最初に入れてあるので含めない
                if (obj.ToCode.Equals(km.FromCode))
                {
                    continue;
                }

                send_to_list.Add(obj.ToCode);
                i++;
            }

            string msg = "";

            if (MessageBox.Show("元のメッセージを引用しますか？", "確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (string s in km.Msg.Split('\n'))
                {
                    msg += "> " + s.Trim('\r') + Environment.NewLine;
                }
            }

            FormKarteMessage2 f = new FormKarteMessage2(km.PtId, send_to_list, "Re: " + km.Title, msg);
            f.Show(this);
        }

        /// <summary>
        /// メールを削除する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton1_Click(object sender, EventArgs e)
        {
            if (ListView1.CurrentRow == null)
            {
                return;
            }

            if (MessageBox.Show("削除しますか？", "削除", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (KindButton1.Checked == true)
                {
                    this.MailDelete(MailKind.Rcv);
                }
                else
                {
                    this.MailDelete(MailKind.Send);
                }

                this.MailClear1();
            }
        }

        /// <summary>
        /// メールを削除する
        /// </summary>
        /// <param name="kind"></param>
        private void MailDelete(MailKind kind = MailKind.Rcv)
        {
            try
            {
                if (ListView1.CurrentRow != null)
                {
                    DataGridViewRow r = ListView1.CurrentRow;

                    string id = r.Cells["ID"].Value.ToString().Trim();
                    string send_date = r.Cells["送信日"].Value.ToString().Trim();
                    string send_time = r.Cells["送信時刻"].Value.ToString().Trim();

                    if (kind == MailKind.Rcv)
                    {
                        KarteMessage.RsvDelete(LoginUser.Id, id, send_date, send_time, true);
                    }
                    else if (kind == MailKind.Send)
                    {
                        KarteMessage.SendDelete(id, LoginUser.Id, send_date, send_time, true);
                    }

                    this.ListShow();
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                MessageBox.Show(err);
            }
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            FormKarteMessage2 f = new FormKarteMessage2();
            f.Show(this);
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            this.ListShow();
        }

        public void ListShow()
        {
            if (KindButton1.Checked == true)
            {
                this.ListShow1();
            }
            else if (KindButton2.Checked == true)
            {
                this.ListShow2();
            }
        }

        private void KarteButton1_Click(object sender, EventArgs e)
        {
            if (this.PtBox1.Text.Length == 0)
            {
                return;
            }

            FormControl.FormPat_Show(PatBase.Load(this.PtBox1.Text));
        }
    }
}