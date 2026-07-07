using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Boundary;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class CtrlOrderView1 : UserControl
    {
        public enum Mode : int
        {
            DoBoxNotShow = 0,
            DoBoxShow = 1
        }

        public Mode Mode1 = Mode.DoBoxShow;

        public PatOrder PatOrder1 = new PatOrder();

        public CtrlOrderView1()
        {
            InitializeComponent();

            this.Click += new EventHandler(CheckedChange);
            this.DateLabel1.Click += new EventHandler(CheckedChange);
            this.DeptLabel1.Click += new EventHandler(CheckedChange);
            this.DoctorLabel1.Click += new EventHandler(CheckedChange);
            this.TimesLabel0.Click += new EventHandler(CheckedChange);
            this.TimesLabel1.Click += new EventHandler(CheckedChange);
            this.TermLabel1.Click += new EventHandler(CheckedChange);
            this.KouiLabel1.Click += new EventHandler(CheckedChange);
            this.ContLabel1.Click += new EventHandler(CheckedChange);
        }

        public CtrlOrderView1(string order_id, Mode mode)
        {
            InitializeComponent();

            this.Click += new EventHandler(CheckedChange);
            this.DateLabel1.Click += new EventHandler(CheckedChange);
            this.DeptLabel1.Click += new EventHandler(CheckedChange);
            this.DoctorLabel1.Click += new EventHandler(CheckedChange);
            this.TimesLabel0.Click += new EventHandler(CheckedChange);
            this.TimesLabel1.Click += new EventHandler(CheckedChange);
            this.TermLabel1.Click += new EventHandler(CheckedChange);
            this.KouiLabel1.Click += new EventHandler(CheckedChange);
            this.ContLabel1.Click += new EventHandler(CheckedChange);

            this.PatOrder1 = PatOrder.Load(order_id);

            this.PatOrder1.DetailList = PatOrderDetail.Load(this.PatOrder1.OrderId);

            this.ModeChange(mode);

            this.OrderShow();
        }

        public CtrlOrderView1(PatOrder order, Mode mode)
        {
            InitializeComponent();

            this.Click += new EventHandler(CheckedChange);
            this.DateLabel1.Click += new EventHandler(CheckedChange);
            this.DeptLabel1.Click += new EventHandler(CheckedChange);
            this.DoctorLabel1.Click += new EventHandler(CheckedChange);
            this.TimesLabel0.Click += new EventHandler(CheckedChange);
            this.TimesLabel1.Click += new EventHandler(CheckedChange);
            this.TermLabel1.Click += new EventHandler(CheckedChange);
            this.KouiLabel1.Click += new EventHandler(CheckedChange);
            this.ContLabel1.Click += new EventHandler(CheckedChange);

            this.PatOrder1 = order;

            this.ModeChange(mode);

            this.OrderShow();
        }

        void OrderShow()
        {
            if (this.PatOrder1.SekouDate.Equals("99999999"))
            {
                this.DateLabel1.Text = "日付未定";
            }
            else
            {
                this.DateLabel1.Text = this.PatOrder1.SekouDateString;
            }

            if (this.PatOrder1.InOut.Equals("1"))
            {
                this.DateLabel1.BackColor = AppColor.Gairai;
            }
            else if (this.PatOrder1.InOut.Equals("2"))
            {
                this.DateLabel1.BackColor = AppColor.Nyuin;
            }

            this.DeptLabel1.Text = this.PatOrder1.DeptName;
            this.DoctorLabel1.Text = this.PatOrder1.DoctorName;

            string cont1 = "";

            foreach (PatOrderDetail detail in this.PatOrder1.DetailList)
            {
                cont1 += detail.Name;

                if (detail.QtyString.Length > 0)
                {
                    cont1 += "  x " + detail.QtyString + detail.Unit;
                }

                cont1 += Environment.NewLine;
            }

            this.ContLabel1.Text = cont1;

            this.TimesLabel1.Text = this.PatOrder1.Times.ToString();

            if (!this.PatOrder1.StartDate.Equals(this.PatOrder1.EndDate))
            {
                this.TermLabel1.Text = this.PatOrder1.StartDateStringShort + "～" + this.PatOrder1.EndDateStringShort;
            }

            this.KouiLabel1.Text = this.PatOrder1.ShinkuString;

            if (this.PatOrder1.InnaiFlg.Equals("0"))
            {
                this.KouiLabel1.ForeColor = AppColor.InNai;
            }
            else
            {
                this.KouiLabel1.ForeColor = AppColor.InGai;
            }

            if (this.PatOrder1.KaikeiFlg.Equals("1"))
            {
                this.ContLabel1.BackColor = AppColor.Kaikei;
            }
            else if (this.PatOrder1.SekouFlg.Equals("1"))
            {
                this.ContLabel1.BackColor = AppColor.Sekou;
            }
            else
            {
                this.ContLabel1.BackColor = Color.White;
            }

            if (this.ContLabel1.PreferredHeight > this.ContLabel1.Height)
            {
                this.ContLabel1.Height = this.ContLabel1.PreferredHeight;
            }

            this.Height = this.ContLabel1.Height + 30;

            // オーダーが存在すれば右クリックメニューを Enabled にする
            if (this.PatOrder1.DetailList.Count > 0)
            {
                this.CtrlOrderMenuStrip1.Enabled = true;
            }
            else
            {
                this.CtrlOrderMenuStrip1.Enabled = false;
            }
        }

        void ModeChange(Mode mode)
        {
            this.Mode1 = mode;

            if (mode == Mode.DoBoxShow)
            {
                this.DoBox1.Visible = true;

                this.DateLabel1.Location = new Point(25, 3);
                this.DeptLabel1.Location = new Point(100, 3);
                this.DoctorLabel1.Location = new Point(180, 3);
                this.KouiLabel1.Location = new Point(25, 25);
                this.ContLabel1.Location = new Point(80, 25);
                this.ContLabel1.Size = new Size(395, 18);
            }
            else if (mode == Mode.DoBoxNotShow)
            {
                this.DoBox1.Visible = false;

                this.DateLabel1.Location = new Point(5, 3);
                this.DeptLabel1.Location = new Point(80, 3);
                this.DoctorLabel1.Location = new Point(160, 3);
                this.KouiLabel1.Location = new Point(5, 25);
                this.ContLabel1.Location = new Point(60, 25);
                this.ContLabel1.Size = new Size(415, 18);
            }
        }

        public bool Checked
        {
            set
            {
                this.DoBox1.Checked = value;
            }
            get
            {
                return this.DoBox1.Checked;
            }
        }

        public string SekouFlg
        {
            get
            {
                return this.PatOrder1.SekouFlg;
            }
        }

        public string KaikeiFlg
        {
            get
            {
                return this.PatOrder1.KaikeiFlg;
            }
        }

        private void DoBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.DoBox1.Checked)
            {
                this.BackColor = Color.FromArgb(160, 255, 255);
            }
            else
            {
                this.BackColor = Color.FromArgb(248, 248, 248);
            }
        }

        void CheckedChange(object sender, EventArgs e)
        {
            if (this.Checked)
            {
                this.Checked = false;
            }
            else
            {
                this.Checked = true;
            }
        }

        public void SetKaikeiFlg(int val)
        {
            OrderHeader.SetKaikeiFlg(this.PatOrder1.OrderId, val);
            this.PatOrder1.KaikeiFlg = val.ToString();
            this.OrderShow();
        }

        private void KaikeiFlg1MenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("会計フラグを「取込済」にします。よろしいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.SetKaikeiFlg(1);
            }
        }

        private void KaikeiFlg0MenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("会計フラグを「未取込」にします。よろしいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.SetKaikeiFlg(0);
            }
        }

        public void SetSekouFlg(int val)
        {
            OrderHeader.SetSekouFlg(this.PatOrder1.OrderId, val);
            this.PatOrder1.SekouFlg = val.ToString();
            this.OrderShow();
        }

        private void SekouFlg1MenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("施行フラグを「施行済」にします。よろしいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.SetSekouFlg(1);
            }
        }

        private void SekouFlg0MenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("施行フラグを「未施行」にします。よろしいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.SetSekouFlg(0);
            }
        }

        public void SetPaperFlg(int val)
        {
            OrderHeader.SetPaperFlg(this.PatOrder1.OrderId, val);
            this.PatOrder1.PaperFlg = val.ToString();
            this.OrderShow();
        }

        private void PaperFlg1MenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("指示箋フラグを「発行済」にします。よろしいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.SetPaperFlg(1);
            }
        }

        private void PaperFlg0MenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("指示箋フラグを「未発行」にします。よろしいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.SetPaperFlg(0);
            }
        }
    }
}
