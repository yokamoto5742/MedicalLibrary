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
    public partial class FormRsvDetail : StdForm1
    {
        public enum Mode : int
        {
            New = 1,
            Modify = 2
        }

        /// <summary>
        /// 予約種別
        /// </summary>
        public string Code1 = "";

        /// <summary>
        /// 予約詳細
        /// </summary>
        public string Code2 = "";

        /// <summary>
        /// 予約種別名称
        /// </summary>
        public string Name1 = "";

        /// <summary>
        /// 予約詳細名称
        /// </summary>
        public string Name2 = "";

        public int RsvDate = 0;

        public int Time1 = 0;

        public int Interval = 0;

        /// <summary>
        /// 予約枠が埋まっているか
        /// </summary>
        public bool IsFull = false;

        /// <summary>
        /// 連続して予約できる枠数
        /// </summary>
        public int NextWakus = 0;

        public string RsvDateTime
        {
            get
            {
                string s = DateTimeAgent.DateFormat(this.RsvDate, DateTimeAgent.DateFormatKind.WLONG) +
                    " " + DateTimeAgent.TimeFormat(this.Time1) +
                    "～";

                return s;
            }
        }

        public Mode Mode1 = Mode.New;


        public FormRsvDetail(string code1, string code2, int rsv_date, int time1, int interval, bool is_full, int next_wakus, string pt_id, Mode mode)
        {
            InitializeComponent();

            this.Code1 = code1;
            this.Code2 = code2;
            this.RsvDate = rsv_date;
            this.Time1 = time1;
            this.Interval = interval;
            this.IsFull = is_full;
            this.NextWakus = next_wakus;
            this.Mode1 = mode;

            RsvMaster master = RsvMaster.GetData(this.Code1, this.Code2, this.RsvDate.ToString());

            if (master.Name2.Length > 0)
            {
                this.NameLabel1.Text = master.Code2 + " " + master.Name2;
            }
            else
            {
                this.NameLabel1.Text = master.Code1 + " " + master.Name1;
            }
            
            this.DateTimeLabel1.Text = this.RsvDateTime;

            this.CommentBox1.Items.Add("");
            this.CommentBox2.Items.Add("");

            foreach (string s in master.Comments[1])
            {
                this.CommentBox1.Items.Add(s);
            }

            foreach (string s in master.Comments[2])
            {
                this.CommentBox2.Items.Add(s);
            }

            // 所用時間コメント
            if (master.Conts[1] != null && master.Conts[1].Length > 0)
            {
                this.ContLabel1.Text += master.Conts[1] + Environment.NewLine + Environment.NewLine;
            }

            if (master.Conts[2] != null && master.Conts[2].Length > 0)
            {
                this.ContLabel1.Text += master.Conts[2] + Environment.NewLine + Environment.NewLine;
            }

            if (master.Conts[3] != null && master.Conts[3].Length > 0)
            {
                this.ContLabel1.Text += master.Conts[3];
            }

            int h = 0;

            if (this.ContLabel1.Text.Length > 0)
            {
                h = this.ContLabel1.PreferredHeight - this.ContLabel1.Height + 30;
            }
            else
            {
                // コメントが無いときは非表示とする
                h = 0 - this.ContLabel1.Height;
            }

            this.Height += h;

            this.WakusButton1.Text = master.Interval.ToString() + " 分";
            this.WakusButton2.Text = (master.Interval * 2).ToString() + " 分";
            this.WakusButton3.Text = (master.Interval * 3).ToString() + " 分";

            this.WakusButton1.Enabled = false;
            this.WakusButton2.Enabled = false;
            this.WakusButton3.Enabled = false;

            if (this.Mode1 == Mode.Modify || !this.IsFull)
            {
                this.WakusButton1.Enabled = true;
                this.WakusButton1.Checked = true;

                if (this.NextWakus >= 1)
                {
                    this.WakusButton2.Enabled = true;
                }

                if (this.NextWakus >= 2)
                {
                    this.WakusButton3.Enabled = true;
                }
            }


            this.PatSet(PatBase.Load(pt_id));
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);
        }

        private void SaveButton1_Click(object sender, EventArgs e)
        {
            RsvData obj = new RsvData();

            obj.Code1 = this.Code1;
            obj.Code2 = this.Code2;
            obj.RsvDate = this.RsvDate;
            obj.Time1 = this.Time1;

            if (this.WakusButton1.Checked)
            {
                obj.Time2 = DateTimeAgent.AddTime(obj.Time1, this.Interval);
            }
            else if (this.WakusButton2.Checked)
            {
                obj.Time2 = DateTimeAgent.AddTime(obj.Time1, this.Interval * 2);
            }
            else if (this.WakusButton3.Checked)
            {
                obj.Time2 = DateTimeAgent.AddTime(obj.Time1, this.Interval * 3);
            }

            obj.Pat.Id = this.Pat.Id;
            obj.Cont1 = this.CommentBox1.Text;
            obj.Cont2 = this.CommentBox2.Text;

            obj.Insert();

            this.Dispose();
        }
    }
}
