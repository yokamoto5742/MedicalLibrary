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
    public partial class FormDischargeSummary : StdForm1
    {
        string PtId = "";

        int InSEQ = 0;

        public FormDischargeSummary(string pt_id, int in_seq)
        {
            InitializeComponent();

            this.PtId = pt_id;
            this.InSEQ = in_seq;
        }

        private void FormDischargeSummary_Load(object sender, EventArgs e)
        {
            this.PatSet(PatBase.Load(this.PtId));
            this.ctrlInHistoryBox21.Init(this.PtId, this.InSEQ);
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);
            this.stdControlPat11.PatSet(p);
        }

        void DataClear()
        {
            this.MainDiagBox.Clear();

            this.ContBox1.Clear();
            this.ContBox2.Clear();
            this.ContBox3.Clear();
            this.ContBox4.Clear();
            this.ContBox5.Clear();
            this.ContBox6.Clear();

            this.StatusDateBox.Clear();

            this.CheckBox1.Checked = false;
            this.CheckBox2.Checked = false;

            this.DateTimeLabel.Text = "";

            for (int i = 1; i <= 5; i++)
            {
                if (this.Controls.ContainsKey("OpePanel" + i))
                {
                    this.Controls.RemoveByKey("OpePanel" + i);
                }
            }
        }

        void DataShow()
        {
            this.DataClear();

            DischargeSummary summary = DischargeSummary.GetData(this.PtId, this.InSEQ);

            this.MainDiagBox.Text = summary.MainDiag;

            this.ContBox1.Text = summary.Cont1;
            this.ContBox2.Text = summary.Cont2;
            this.ContBox3.Text = summary.Cont3;
            this.ContBox4.Text = summary.Cont4;
            this.ContBox5.Text = summary.Cont5;
            this.ContBox6.Text = summary.Cont6;

            this.StatusDateBox.Text = AppString.IsDate(summary.StatusDate) ? summary.StatusDate : "";

            this.CheckBox1.Checked = summary.InputCheck > 0;
            this.CheckBox2.Checked = summary.InputCheck.Equals(2);

            if (summary.RegDate > 0 || summary.UpDate > 0)
            {
                this.DateTimeLabel.Text = summary.UpDate > 0 ? summary.UpDateTime + "　" + summary.UpStaffName : summary.RegDateTime + "　" + summary.RegStaffName;
            }

            int h = 40;

            for (int i = 1; i <= 5; i++)
            {
                DischargeSummaryOpe ope = summary.OpeList[i];

                if (ope.Name.Length == 0) continue;

                OpePanel panel = new OpePanel(i, ope);
                panel.Name = "OpePanel" + i;
                panel.Location = new Point(660, h);

                this.Controls.Add(panel);

                h += panel.Height + 5;
            }

            if (h > 40)
            {
                // 手術がある場合
                this.Width = 1280;
            }
            else
            {
                // 手術が無い場合
                this.Width = 680;
            }
        }

        private void ctrlInHistoryBox21_ValueChanged(object sender, EventArgs e)
        {
            this.InSEQ = this.ctrlInHistoryBox21.PatIn1.SEQ;

            this.DataShow();
        }
    }

    class OpePanel : Panel
    {
        /// <summary>
        /// 番号 1～5
        /// </summary>
        int SEQ = 0;

        public OpePanel(int seq, DischargeSummaryOpe ope)
        {
            this.SEQ = seq;

            int h = 10;
            int x = 45;

            Label lb_name = new Label();
            lb_name.AutoSize = true;
            lb_name.Location = new Point(5, h);
            lb_name.Text = "術式";
            this.Controls.Add(lb_name);

            TextBox box_name = new TextBox();
            box_name.Size = new Size(530, 20);
            box_name.Location = new Point(x, h - 5);
            box_name.Text = ope.Name;
            this.Controls.Add(box_name);

            h += 22;

            Label lb_doctor = new Label();
            lb_doctor.AutoSize = true;
            lb_doctor.Location = new Point(5, h);
            lb_doctor.Text = "術者";
            this.Controls.Add(lb_doctor);

            TextBox box_doctor = new TextBox();
            box_doctor.Size = new Size(240, 20);
            box_doctor.Location = new Point(x, h - 5);
            box_doctor.Text = ope.Doctor;
            this.Controls.Add(box_doctor);

            Label lb_date = new Label();
            lb_date.AutoSize = true;
            lb_date.Location = new Point(300, h);
            lb_date.Text = "日時";
            this.Controls.Add(lb_date);

            CtrlDateBox2 box_date = new CtrlDateBox2();
            box_date.Size = new Size(80, 20);
            box_date.Location = new Point(335, h - 5);
            box_date.Text = AppString.IsDate(ope.Date) ? ope.Date : "";
            this.Controls.Add(box_date);

            TextBox box_time = new TextBox();
            box_time.Size = new Size(155, 20);
            box_time.Location = new Point(420, h - 5);
            box_time.Text = ope.Time;
            this.Controls.Add(box_time);

            h += 22;

            Label lb_anes = new Label();
            lb_anes.AutoSize = true;
            lb_anes.Location = new Point(5, h);
            lb_anes.Text = "麻酔";
            this.Controls.Add(lb_anes);

            TextBox box_anes = new TextBox();
            box_anes.Size = new Size(530, 20);
            box_anes.Location = new Point(x, h - 5);
            box_anes.Text = ope.Anes;
            this.Controls.Add(box_anes);

            h += 22;

            Label lb_cont = new Label();
            lb_cont.AutoSize = true;
            lb_cont.Location = new Point(5, h);
            lb_cont.Text = "所見";
            this.Controls.Add(lb_cont);

            TextBox box_cont = new TextBox();
            box_cont.Size = new Size(530, 250);
            box_cont.Location = new Point(x, h - 5);
            box_cont.Multiline = true;
            box_cont.ScrollBars = ScrollBars.Vertical;
            box_cont.Text = ope.Cont;
            this.Controls.Add(box_cont);

            h += 250;

            this.Size = new Size(585, h);
            this.BorderStyle = BorderStyle.FixedSingle;
        }
    }
}
