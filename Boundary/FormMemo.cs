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
    public partial class FormMemo : StdForm1
    {
        bool _ReadOnly = false;

        public bool ReadOnly
        {
            get
            {
                return this._ReadOnly;
            }
            set
            {
                this._ReadOnly = value;

                if (value)
                {
                    this.RegButton1.Visible = false;
                }
                else
                {
                    this.RegButton1.Visible = true;
                }
            }
        }

        public FormMemo(bool read_only = false)
        {
            InitializeComponent();

            this.ReadOnly = read_only;
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            this.DataShow();
        }

        void DataClear()
        {
            this.MemoBox.Clear();
            this.RegDateTimeLabel1.Text = "";
            this.RegStaffLabel1.Text = "";
        }

        void DataShow()
        {
            this.DataClear();

            Memo obj = Memo.Load(this.Pat.Id, "0");

            this.MemoBox.Text = obj.Cont;
            this.RegDateTimeLabel1.Text = obj.UpDateTime;
            this.RegStaffLabel1.Text = obj.UpStaffName;

            // 末尾にカーソルを持って来る
            this.MemoBox.Select(this.MemoBox.Text.Length, 0);
        }

        private void RegButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("登録します。よろしいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Memo obj = new Memo();

                obj.PtId = this.Pat.Id;
                obj.InOut = "0";
                obj.Cont = this.MemoBox.Text;

                this.Msg1(obj.Save());
                this.DataShow();
            }
        }
    }
}
