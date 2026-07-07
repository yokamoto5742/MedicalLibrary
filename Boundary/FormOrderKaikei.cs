using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using MedicalLibrary.Agent;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class FormOrderKaikei : StdForm1
    {
        DataSet dSet = new DataSet();

        public enum Mode : int
        {
            Out = 1,
            In = 2
        }

        Mode mode1 = Mode.Out;

        public Mode Mode1
        {
            set
            {
                this.mode1 = value;

                if (this.mode1 == Mode.Out)
                {
                    this.InOutButton1.Checked = true;

                    this.DatePicker1.Value = DateTime.Now;
                    this.DatePicker2.Value = DateTime.Now;

                    this.SEQBox1.Visible = true;
                    this.SEQLabel1.Visible = true;

                    this.DateLabel2.Visible = false;
                    this.DatePicker2.Visible = false;

                    this.InsLabel1.Visible = true;
                    this.InsBox1.Visible = true;
                    this.InsNameLabel1.Visible = true;
                }
                else if (this.mode1 == Mode.In)
                {
                    this.InOutButton2.Checked = true;

                    this.DatePicker1.Value = DateTime.Now;
                    this.DatePicker2.Value = DateTime.Now;

                    this.SEQBox1.Visible = false;
                    this.SEQLabel1.Visible = false;

                    this.DateLabel2.Visible = true;
                    this.DatePicker2.Visible = true;

                    this.InsLabel1.Visible = false;
                    this.InsBox1.Visible = false;
                    this.InsNameLabel1.Visible = false;
                }
            }
            get
            {
                return this.mode1;
            }
        }

		/// <summary>
		/// 日付（８桁数字）
		/// </summary>
		public string Date1
		{
			get
			{
				return this.DatePicker1.Value.ToString("yyyyMMdd");
			}
			set
			{
				if (DateTimeAgent.IsDate(value))
				{
					DateTime dt = DateTime.Now;

					if (DateTime.TryParse(DateTimeAgent.DateFormat(value, DateTimeAgent.DateFormatKind.LONG), out dt))
					{
						this.DatePicker1.Value = dt;
					}
				}
			}
		}

		/// <summary>
		/// 日付（８桁数字）
		/// </summary>
		public string Date2
		{
			get
			{
				return this.DatePicker2.Value.ToString("yyyyMMdd");
			}
			set
			{
				if (DateTimeAgent.IsDate(value))
				{
					DateTime dt = DateTime.Now;

					if (DateTime.TryParse(DateTimeAgent.DateFormat(value, DateTimeAgent.DateFormatKind.LONG), out dt))
					{
						this.DatePicker2.Value = dt;
					}
				}
			}
		}

        /// <summary>
        /// 当日の何回目の受診か（Q26とプロアスに渡す引数として必要。外来のみ）
        /// </summary>
        public int SEQ
        {
            set
            {
                this.SEQBox1.Text = value.ToString();
            }
            get
            {
                int i = 1;

                int.TryParse(this.SEQBox1.Text, out i);

                return i;
            }
        }


        public FormOrderKaikei()
        {
            InitializeComponent();

            this.SEQBox1.Items.Add("1");
            this.SEQBox1.Items.Add("2");
            this.SEQBox1.Items.Add("3");
            this.SEQBox1.Items.Add("4");
        }

        public FormOrderKaikei(Mode mode, int seq = 1)
        {
            InitializeComponent();

            this.StatusLabel1.BackColor = AppColor.MiSekou;
            this.StatusLabel2.BackColor = AppColor.Sekou;
            this.StatusLabel3.BackColor = AppColor.Kaikei;

            this.SEQBox1.Items.Add("1");
            this.SEQBox1.Items.Add("2");
            this.SEQBox1.Items.Add("3");
            this.SEQBox1.Items.Add("4");

            this.Mode1 = mode;

            if (seq >= 1)
            {
                this.SEQ = seq;
            }
            else
            {
                this.SEQ = 1;
            }
        }

        private void FormOrderKaikei_Load(object sender, EventArgs e)
        {
            this.ctrlDeptBox11.Init();
            this.ctrlDoctorBox11.Init();
            if (!File.Exists(LibSettings.Current.ReceApiExe) ||
                !File.Exists(LibSettings.Current.ReceExe))
            {
                this.KaikeiButton1.Enabled = false;
            }
            ToolTip t = new ToolTip();

            t.SetToolTip(this.InsBox1, "F3キーで保険一覧が表示されます");
        }

        public override void FontSet(AppFont f)
        {
            base.FontSet(f);

            this.stdControlFont11.FontSet(f);
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            // 保険をクリア
            this.InsClear();

            this.PatShow();
        }

        public void DeptDoctorSet(string dept, string doctor)
        {
            this.ctrlDeptBox11.SetDept(dept);
            this.ctrlDoctorBox11.SetDoctor(doctor);
        }

        public void InsSet(string ins_seq)
        {
            this.InsBox1.Text = ins_seq;

            if (this.Pat.Id.Length == 0)
            {
                this.InsClear();
                return;
            }

            if (this.InsBox1.Text.Length == 0)
            {
                this.InsClear();
                return;
            }

            this.InsNameLabel1.Text = "";

            Dictionary<int, PatIns> dict = PatIns.GetDict(this.Pat.Id);

            bool exist = false;

            foreach (PatIns obj in dict.Values)
            {
                if (obj.SEQ.ToString().Equals(this.InsBox1.Text))
                {
                    this.InsNameLabel1.Text = obj.KindNameShort;
                    exist = true;
                    break;
                }
            }

            // 存在しない保険パターンの場合
            if (!exist)
            {
                this.InsBox1.Clear();
            }
        }

        private void InOutButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.InOutButton1.Checked)
            {
                this.Mode1 = Mode.Out;
            }
            else
            {
                this.Mode1 = Mode.In;
            }
        }

        private void InOutButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.InOutButton2.Checked)
            {
                this.Mode1 = Mode.In;
            }
            else
            {
                this.Mode1 = Mode.Out;
            }
        }

        void InsClear()
        {
            this.InsBox1.Clear();
            this.InsNameLabel1.Text = "";
        }

        public void PatShow()
        {
            this.HistoryPanel1.Controls.Clear();

            List<string> empty_list = new List<string>();

            List<PatOrder> list = new List<PatOrder>();

            if (this.mode1 == Mode.Out)
            {
                list = PatOrder.GetListByPatSekouDates(this.Pat.Id, DatePicker1.Value.ToString("yyyyMMdd"), DatePicker1.Value.ToString("yyyyMMdd"), "1", empty_list, empty_list, empty_list, false, true);
            }
            else if (this.mode1 == Mode.In)
            {
                list = PatOrder.GetListByPatSekouDates(this.Pat.Id, DatePicker1.Value.ToString("yyyyMMdd"), DatePicker2.Value.ToString("yyyyMMdd"), "2", empty_list, empty_list, empty_list, false, true);
            }

            int h = 5;

            foreach (PatOrder obj in list)
            {
                CtrlOrderView1 order = new CtrlOrderView1(obj.OrderId, CtrlOrderView1.Mode.DoBoxShow);

                order.Location = new Point(10, h);

                // デフォルトでは施行済・未会計の場合は選択する
                if (order.SekouFlg.Equals("1") && !order.KaikeiFlg.Equals("1"))
                {
                    order.Checked = true;
                }

                this.HistoryPanel1.Controls.Add(order);

                h += order.Height + 5;
            }
        }

        private void ShowButton1_Click(object sender, EventArgs e)
        {
            this.PatShow();
        }

        private void SelectAllButton1_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.HistoryPanel1.Controls)
            {
                if (c is CtrlOrderView1)
                {
                    ((CtrlOrderView1)c).Checked = true;
                }
            }
        }

        private void SelectReceYetButton1_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.HistoryPanel1.Controls)
            {
                if (c is CtrlOrderView1)
                {
                    CtrlOrderView1 cc = (CtrlOrderView1)c;

                    if (cc.SekouFlg.Equals("1") && !cc.KaikeiFlg.Equals("1"))
                    {
                        cc.Checked = true;
                    }
                    else
                    {
                        cc.Checked = false;
                    }
                }
            }
        }

        private void SelectNoneButton1_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.HistoryPanel1.Controls)
            {
                if (c is CtrlOrderView1)
                {
                    ((CtrlOrderView1)c).Checked = false;
                }
            }
        }

        private void KaikeiButton1_Click(object sender, EventArgs e)
        {
            if (!File.Exists(LibSettings.Current.ReceApiExe) ||
                !File.Exists(LibSettings.Current.ReceExe))
            {
                return;
            }
            // チェックの入ったオーダー
            List<PatOrder> list = new List<PatOrder>();

            // チェックの入ったオーダーの番号
            string s = "";

            foreach (Control c in this.HistoryPanel1.Controls)
            {
                if (c is CtrlOrderView1)
                {
                    CtrlOrderView1 cc = (CtrlOrderView1)c;

                    // チェックが入っていなければ飛ばす
                    if (!cc.Checked)
                    {
                        continue;
                    }

                    if (s.Length > 0)
                    {
                        s += ",";
                    }

                    s += cc.PatOrder1.OrderId;
                    list.Add(cc.PatOrder1);
                }
            }

            if (s.Length == 0)
            {
                MessageBox.Show("会計に取り込むオーダーが選択されていません");
                return;
            }

            string dept_code = this.ctrlDeptBox11.GetDept().Code.ToString();
            string doctor_code = this.ctrlDoctorBox11.GetDoctor().Code.ToString();
            string ins_seq = this.InsBox1.Text;

            if (ins_seq.Length == 0)
            {
                ins_seq = "0";
            }

            if (this.mode1 == Mode.Out)
            {
                // ID701RC.F20 に会計入力者コードをセット
                PatOut.SetKaikeiStaffByPtId(LoginUser.Id, this.DatePicker1.Value.ToString("yyyyMMdd"), this.Pat.Id);
                if (list.Count > 0)
                {
                    Q26.Execute(list, DateTime.Now, 1, this.SEQ, true, LibSettings.Current.Proas.OrderXmlTmpFolder, LibSettings.Current.Proas.OrderXmlDstFolder, true, LoginUser.Id);
                    Thread.Sleep(LibSettings.Current.OrderReceApiIntervalInt * 1000);
                }

                Process.Start(LibSettings.Current.ReceApiExe, "1 " + this.Pat.Id + " " + this.SEQ.ToString() + " " + dept_code + " " + doctor_code + " " + ins_seq);
            }
            else
            {
                if (list.Count > 0)
                {
                    Q26.Execute(list, DateTime.Now, 1, 0, true, LibSettings.Current.Proas.OrderXmlTmpFolder, LibSettings.Current.Proas.OrderXmlDstFolder, true, LoginUser.Id);
                    Thread.Sleep(LibSettings.Current.OrderReceApiIntervalInt * 1000);
                }

                Process.Start(LibSettings.Current.ReceApiExe, "2 " + this.Pat.Id + " 0 " + dept_code + " " + doctor_code + " " + ins_seq);
            }
        }

        private void InsBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                if (this.Pat.Id.Length == 0)
                {
                    return;
                }

                PatIns obj = FormFindPatIns.FindPatIns(this.Pat.Id);

                if (obj.SEQ > 0)
                {
                    this.InsBox1.Text = obj.SEQ.ToString();
                    this.InsNameLabel1.Text = obj.KindNameShort;
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                this.InsSet(this.InsBox1.Text);
            }
        }

        private void KaikeiFlg0MenuItem_Click(object sender, EventArgs e)
        {
            int i = 0;

            foreach (Control c in this.HistoryPanel1.Controls)
            {
                if (c is CtrlOrderView1)
                {
                    CtrlOrderView1 cc = (CtrlOrderView1)c;

                    // チェックが入っていなければ飛ばす
                    if (!cc.Checked)
                    {
                        continue;
                    }

                    i++;
                }
            }

            if (this.HistoryPanel1.Controls.Count == 0)
            {
                MessageBox.Show("対象のオーダーがありません");
            }
            else if (i == 0)
            {
                MessageBox.Show("オーダーが選択されていません");
            }
            else if (MessageBox.Show("選択された全オーダーの会計フラグを「未取込」にします。よろしいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                foreach (Control c in this.HistoryPanel1.Controls)
                {
                    if (c is CtrlOrderView1)
                    {
                        CtrlOrderView1 cc = (CtrlOrderView1)c;

                        // チェックが入っていなければ飛ばす
                        if (!cc.Checked)
                        {
                            continue;
                        }

                        cc.SetKaikeiFlg(0);
                    }
                }
            }
        }

        private void KaikeiFlg1MenuItem_Click(object sender, EventArgs e)
        {
            int i = 0;

            foreach (Control c in this.HistoryPanel1.Controls)
            {
                if (c is CtrlOrderView1)
                {
                    CtrlOrderView1 cc = (CtrlOrderView1)c;

                    // チェックが入っていなければ飛ばす
                    if (!cc.Checked)
                    {
                        continue;
                    }

                    i++;
                }
            }

            if (this.HistoryPanel1.Controls.Count == 0)
            {
                MessageBox.Show("対象のオーダーがありません");
            }
            else if (i == 0)
            {
                MessageBox.Show("オーダーが選択されていません");
            }
            else if (MessageBox.Show("選択された全オーダーの会計フラグを「取込済」にします。よろしいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                foreach (Control c in this.HistoryPanel1.Controls)
                {
                    if (c is CtrlOrderView1)
                    {
                        CtrlOrderView1 cc = (CtrlOrderView1)c;

                        // チェックが入っていなければ飛ばす
                        if (!cc.Checked)
                        {
                            continue;
                        }

                        cc.SetKaikeiFlg(1);
                    }
                }
            }
        }
    }
}
