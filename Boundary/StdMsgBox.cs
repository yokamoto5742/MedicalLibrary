using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class StdMsgBox : Form
    {
        string Msg = "";

        List<StdMsgButton> MsgButtonList = new List<StdMsgButton>();

        public StdMsgBox(string msg, string caption = "", List<StdMsgButton> list = null)
        {
            InitializeComponent();

            this.Msg = msg;

            if (caption.Length > 0)
            {
                this.Text = caption;
            }
            else
            {
                this.Text = "確認";
            }

            if (list != null)
            {
                this.MsgButtonList = list;
            }
            else
            {
                this.MsgButtonList.Add(new StdMsgButton("OK", DialogResult.OK));
                this.MsgButtonList.Add(new StdMsgButton("キャンセル", DialogResult.Cancel));
            }
        }

        private void StdMsgBox_Load(object sender, EventArgs e)
        {
            // 呼び出し元のフォントに合わせる
            if (this.Owner != null)
            {
                this.Font = this.Owner.Font;
            }

            Label MsgLabel = new Label();
            MsgLabel.Text = this.Msg;
            MsgLabel.AutoSize = true;
            MsgLabel.Location = new Point(20, 20);

            this.Controls.Add(MsgLabel);

            int x = 20;
            int y = MsgLabel.Height + 40;

            foreach (StdMsgButton mb in this.MsgButtonList)
            {
                Button b = new Button();
                b.AutoSize = true;
                b.Location = new Point(x, y);
                b.Text = mb.Text;
                b.TextAlign = ContentAlignment.MiddleCenter;
                b.Tag = mb;
                b.Click += new EventHandler(b_Click);

                this.Controls.Add(b);

                x += b.Width + 20;
            }

            // MsgLabl の右端か、一番右のボタンのうち、右側にある方に合わせる
            this.Width = MsgLabel.Width + 40 > x + 20 ? MsgLabel.Width + 40 : x + 20;

            // 高さを合わせる
            this.Height = y + 75;

            // 表示位置の調整
            if (this.Owner != null)
            {
                Point p = new Point();
                p.X = this.Owner.Location.X + (this.Owner.Width / 2) - this.Width / 2;
                p.Y = this.Owner.Location.Y + (this.Owner.Height / 2) - this.Height / 2;
                this.Location = p;
           }
        }

        void b_Click(object sender, EventArgs e)
        {
            StdMsgButton mb = (StdMsgButton)((Button)sender).Tag;
            this.DialogResult = mb.Result;
            this.Dispose();
        }

        public static DialogResult Show(Form owner, string msg, string caption = "", List<StdMsgButton> list = null)
        {
            StdMsgBox mb = new StdMsgBox(msg, caption, list);
            return mb.ShowDialog(owner);
        }
    }

    public class StdMsgButton
    {
        public string Text = "";

        public DialogResult Result = DialogResult.OK;

        public StdMsgButton(string text, DialogResult result)
        {
            this.Text = text;
            this.Result = result;
        }
    }
}
