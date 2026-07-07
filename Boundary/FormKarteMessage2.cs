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
    public partial class FormKarteMessage2 : StdForm1
    {
        /// <summary>
        /// 送信先リスト
        /// </summary>
        public List<TextBox> SendToList2 = new List<TextBox>();
        public List<TextBox> SendToNameList2 = new List<TextBox>();

        public FormKarteMessage2()
        {
            InitializeComponent();

            this.SendToMake2(5);
        }

        public FormKarteMessage2(string pt_id, List<string> send_to_list = null, string title = "", string msg = "")
        {
            InitializeComponent();

            this.StaffNameBox.Text = LoginUser.Name;

            // 送信先リスト
            List<Staff> staff_list = new List<Staff>();

            if (send_to_list != null)
            {
                foreach (string s in send_to_list)
                {
                    // 送信先コードが 0 以下なら飛ばす
                    if (s.CompareTo("0") <= 0) continue;

                    Staff staff = Staff.Load(s);

                    // 該当 Staff が無ければ飛ばす
                    if (staff.Name.Length == 0) continue;

                    staff_list.Add(staff);
                }
            }

            // 送信先を５個以上作る
            this.SendToMake2(staff_list.Count > 5 ? staff_list.Count : 5);

            for (int i = 1; i <= staff_list.Count; i++)
            {
                this.SendToList2[i - 1].Text = staff_list[i - 1].Code.ToString();
                this.SendToNameList2[i - 1].Text = staff_list[i - 1].Name;
            }

            this.PatSet(PatBase.Load(pt_id));

            this.TitleBox2.Text = title;
            this.ContBox2.Text = msg;
        }

        private void FormKarteMessage2_Load(object sender, EventArgs e)
        {
            List<AddressGroup> list = AddressGroup.GetList(LoginUser.Id);

            foreach (AddressGroup obj in list)
            {
                this.AddressGroupBox2.Items.Add(obj);
            }
        }

        private void FormKarteMessage2_Shown(object sender, EventArgs e)
        {
            this.stdControlPat11.ReadOnly = false;

            if (this.Pat.Id.Length == 0)
            {
                this.stdControlPat11.Focus();
            }
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);
        }

        /// <summary>
        /// メール作成欄をクリアする
        /// </summary>
        private void MailClear2()
        {
            this.SendToMake2(5);

            this.PatSet(new PatBase());

            this.TitleBox2.Text = "";
            this.ContBox2.Text = "";
            this.PriorityBox2.Text = "";

            this.AddressGroupBox2.ClearSelected();

            for (int i = 0; i < this.AddressGroupBox2.Items.Count; i++)
            {
                this.AddressGroupBox2.SetItemChecked(i, false);
            }
        }

        private void PtBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                this.PatSet(FormFindPat.FindPat());
            }
            else if (e.KeyCode == Keys.Enter)
            {
                this.TitleBox2.Focus();
            }
        }

        private void KarteButton2_Click(object sender, EventArgs e)
        {
            if (this.Pat.Id.Length > 0)
            {
                FormControl.FormPat_Show(this.Pat);
            }
        }

        private void TitleBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.PriorityBox2.Focus();
            }
        }

        void SendToClear2()
        {
            this.Panel2.Controls.Clear();
            this.SendToList2.Clear();
            this.SendToNameList2.Clear();
        }

        void SendToMake2(int num)
        {
            this.SendToClear2();

            for (int i = 1; i <= num; i++)
            {
                TextBox box1 = new TextBox();
                box1.Name = "SendTo2" + i.ToString();
                box1.Size = new Size(60, 19);
                box1.Location = new Point(2, 2 + (i - 1) * 23);
                box1.TextAlign = HorizontalAlignment.Center;
                box1.BackColor = Color.LightYellow;
                box1.ReadOnly = false;
                box1.KeyDown += new KeyEventHandler(SendTo2_KeyDown);
                box1.Leave += new EventHandler(SendTo2_Leave);

                this.Panel2.Controls.Add(box1);
                this.SendToList2.Add(box1);

                TextBox box2 = new TextBox();
                box2.Name = "SendToName2" + i.ToString();
                box2.Size = new Size(80, 19);
                box2.Location = new Point(65, 2 + (i - 1) * 23);
                box2.TextAlign = HorizontalAlignment.Center;
                box2.BackColor = Color.White;
                box2.ReadOnly = true;

                this.Panel2.Controls.Add(box2);
                this.SendToNameList2.Add(box2);
            }
        }

        void SendTo2_Leave(object sender, EventArgs e)
        {
            TextBox c = (TextBox)sender;

            if (this.Panel2.Controls.ContainsKey("SendToName2" + c.Name.Substring(7)))
            {
                this.FindSendToName(c, (TextBox)this.Panel2.Controls["SendToName2" + c.Name.Substring(7)]);
            }
        }

        void SendTo2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                this.FindSendTo2();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                TextBox c = (TextBox)sender;

                int n = 1;

                if (int.TryParse(c.Name.Substring(7), out n) &&
                    this.Panel2.Controls.ContainsKey("SendToName2" + n.ToString()))
                {
                    this.FindSendToName(c, (TextBox)this.Panel2.Controls["SendToName2" + n.ToString()]);

                    if (this.Panel2.Controls.ContainsKey("SendTo2" + (n + 1).ToString()))
                    {
                        this.Panel2.Controls["SendTo2" + (n + 1).ToString()].Focus();
                    }
                }
            }
        }

        void SendToAdd2(int num)
        {
            int c = 1;

            while (true)
            {
                if (!this.Panel2.Controls.ContainsKey("SendTo2" + c.ToString()))
                {
                    break;
                }

                c++;
            }

            for (int i = c; i < c + num; i++)
            {
                TextBox box1 = new TextBox();
                box1.Name = "SendTo2" + i.ToString();
                box1.Size = new Size(60, 19);
                box1.Location = new Point(2, 2 + (i - 1) * 23 - this.Panel2.VerticalScroll.Value);
                box1.TextAlign = HorizontalAlignment.Center;
                box1.BackColor = Color.LightYellow;
                box1.ReadOnly = false;
                box1.KeyDown += new KeyEventHandler(SendTo2_KeyDown);
                box1.Leave += new EventHandler(SendTo2_Leave);

                this.Panel2.Controls.Add(box1);
                this.SendToList2.Add(box1);

                TextBox box2 = new TextBox();
                box2.Name = "SendToName2" + i.ToString();
                box2.Size = new Size(80, 19);
                box2.Location = new Point(65, 2 + (i - 1) * 23 - this.Panel2.VerticalScroll.Value);
                box2.TextAlign = HorizontalAlignment.Center;
                box2.BackColor = Color.White;
                box2.ReadOnly = true;

                this.Panel2.Controls.Add(box2);
                this.SendToNameList2.Add(box2);
            }
        }

        private void FindSendTo2()
        {
            List<Staff> list = FormFindStaff.FindStaff();

            // 空いている送信先の数を調べる
            int empty = 0;

            for (int i = 0; i < this.SendToList2.Count; i++)
            {
                if (this.SendToList2[i].Text.Length == 0)
                {
                    empty++;
                }
            }

            // 空きが足りなければ追加する
            if (empty < list.Count)
            {
                this.SendToAdd2(list.Count - empty);
            }

            int c = 0;

            foreach (Staff staff in list)
            {
                while (c < this.SendToList2.Count)
                {
                    if (this.SendToList2[c].Text.Length == 0)
                    {
                        this.SendToList2[c].Text = staff.Code.ToString();
                        this.SendToNameList2[c].Text = staff.Name;
                        c++;

                        break;
                    }

                    c++;
                }
            }
        }

        /// <summary>
        /// 職員番号から職員名を検索
        /// </summary>
        /// <param name="sendMailTo"></param>
        /// <param name="sendMailToName"></param>
        private void FindSendToName(TextBox sendMailTo, TextBox sendMailToName)
        {
            try
            {
                sendMailToName.Text = "";

                if (sendMailTo.Text.Length > 0)
                {
                    Staff obj = Staff.Load(sendMailTo.Text);

                    if (obj.Code > 0)
                    {
                        sendMailToName.Text = obj.Name;
                    }
                    else
                    {
                        MessageBox.Show("該当する職員はありません");
                        sendMailTo.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                MessageBox.Show(err);
            }
        }

        private void SendToAddButton2_Click(object sender, EventArgs e)
        {
            this.SendToAdd2(1);
        }

        private void FormAddressGroupButton1_Click(object sender, EventArgs e)
        {
            FormAddressGroup f = new FormAddressGroup();
            f.ShowDialog();
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            try
            {
                string err = "";

                if (TitleBox2.Text.Length == 0)
                {
                    err += "タイトルがありません。" + Environment.NewLine;
                }

                if (ContBox2.Text.Length == 0)
                {
                    err += "中身がありません。" + Environment.NewLine;
                }

                // 送信先を取得する
                List<string> code_list = new List<string>();

                foreach (TextBox t in SendToList2)
                {
                    if (t.Text.Length > 0 && !code_list.Contains(t.Text))
                    {
                        code_list.Add(t.Text);
                    }
                }

                foreach (Object item in this.AddressGroupBox2.CheckedItems)
                {
                    List<Staff> list = AddressGroup.GetMembers(LoginUser.Id, ((AddressGroup)item).SEQ);

                    foreach (Staff staff in list)
                    {
                        if (!code_list.Contains(staff.Code.ToString()))
                        {
                            code_list.Add(staff.Code.ToString());
                        }
                    }
                }

                if (code_list.Count == 0)
                {
                    err += "送信先がありません" + Environment.NewLine;
                }

                if (err.Length > 0)
                {
                    throw new Exception(err);
                }
                else
                {
                    // 医師以外ならば確認する
                    if (!LoginUser.IsDoctor)
                    {
                        if (MessageBox.Show("送信します。よろしいですか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
                        {
                            return;
                        }
                    }
                }

                KarteMessage obj = new KarteMessage();

                obj.FromCode = LoginUser.Id;
                obj.SendDate = DateTime.Now.ToString("yyyyMMdd");
                obj.SendTime = DateTime.Now.ToString("HHmmss");

                obj.Title = TitleBox2.Text;
                obj.Msg = ContBox2.Text;
                obj.PtId = this.Pat.Id;

                if (PriorityBox2.SelectedIndex > 0)
                {
                    obj.Priority = PriorityBox2.SelectedIndex.ToString();
                }

                foreach (string s in code_list)
                {
                    if (s.Length > 0)
                    {
                        obj.ToCode = s;

                        obj.Send();
                    }
                }

                this.MailClear2();

                if (this.Owner != null && this.Owner is FormKarteMessage1)
                {
                    ((FormKarteMessage1)this.Owner).ListShow();
                }

                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("クリアしますか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            this.MailClear2();
        }
    }
}
