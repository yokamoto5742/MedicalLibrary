using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Boundary;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class FormAlert : StdForm1
    {
        /// <summary>
        /// 0: 編集, 1: 表示
        /// </summary>
        int _Mode = 1;

        /// <summary>
        /// 0: 編集, 1: 表示
        /// </summary>
        public int Mode
        {
            get
            {
                return this._Mode;
            }
            set
            {
                switch (value)
                {
                    case 0:
                        this._Mode = value;

                        this.stdControlPat11.Visible = true;
                        this.ContBox.Visible = true;
                        this.StatusBox.Visible = true;
                        this.SaveButton.Visible = true;

                        this.FormBorderStyle = FormBorderStyle.Sizable;
                        this.Width = 600;
                        this.Height = 300;
                        this.TopMost = false;

                        break;

                    case 1:
                        this._Mode = value;

                        this.stdControlPat11.Visible = false;
                        this.ContBox.Visible = false;
                        this.StatusBox.Visible = false;
                        this.SaveButton.Visible = false;

                        this.FormBorderStyle = FormBorderStyle.None;
                        this.Width = 580;
                        this.Height = 100;
                        this.TopMost = true;

                        break;
                }
            }
        }

        public FormAlert()
        {
            InitializeComponent();
        }

        private void FormAlert_Load(object sender, EventArgs e)
        {
            if (this.Owner is FormMedicalSupport)
            {
                this.Location = new Point(this.Owner.Location.X + 10, this.Owner.Location.Y + 100);
                this.Size = new Size(this.Owner.Width - 20, 100);
            }
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            this.DataShow();
        }

        void DataShow()
        {
            Alert obj = Alert.GetData(this.Pat.Id);

            this.AlertLabel.Text = obj.Cont;
            this.ContBox.Text = obj.Cont;
            this.InfoLabel.Text = DateTimeAgent.DateFormat(obj.SaveDate, DateTimeAgent.DateFormatKind.LONG) + " " + obj.StaffName.Replace("　", "").Replace(" ", "");

            // 長さに応じて場所を変える
            if (this.Mode == 0)
            {
                this.InfoLabel.Location = new Point(this.Width - 25 - this.InfoLabel.PreferredWidth, 75);
            }
            else if (this.Mode == 1)
            {
                this.InfoLabel.Location = new Point(this.Width - 10 - this.InfoLabel.PreferredWidth, 75);
            }

            this.InfoLabel.Width = this.InfoLabel.PreferredWidth;

            if (obj.Cont.Length > 0)
            {
                // 内容が存在する場合は Status に従う
                this.StatusBox.Checked = !obj.Status.Equals("1");

                this.InfoLabel.Visible = true;
            }
            else
            {
                // 空の場合は false
                this.StatusBox.Checked = false;

                this.InfoLabel.Visible = false;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Alert obj = new Alert();

            obj.PtId = this.Pat.Id;
            obj.Cont = this.ContBox.Text;
            obj.Status = this.StatusBox.Checked ? "0" : "1";

            obj.Save();

            if (obj.Cont.Length > 0 && obj.Status.Equals("1"))
            {
                // 内容がある and 非表示でない場合
                this.Mode = 1;
                this.DataShow();
            }
            else
            {
                this.Dispose();
            }
        }

        private void AlertLabel_DoubleClick(object sender, EventArgs e)
        {
            this.Mode = this.Mode.Equals(1) ? 0 : 1;
        }
    }
}
