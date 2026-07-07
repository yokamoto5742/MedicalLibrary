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
    public partial class FormDiagView : StdForm1
    {
        public FormDiagView()
        {
            InitializeComponent();
        }

        private void FormDiagView_Load(object sender, EventArgs e)
        {
            // 病名は「現病名」
            this.DiagCheckBox1.Checked = true;
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            this.ListShow();
        }

        void ListShow()
        {
            this.ListView1.ListShow(this.Pat.Id, "開始日 desc, 連番 desc", "");

            this.ListFormat();
        }

        void ListFormat()
        {
            List<string> filters = new List<string>();

            if (this.DiagCheckBox1.Checked)
            {
                filters.Add("転帰区分 = ''");
            }

            if (this.DiagCheckBox2.Checked)
            {
                filters.Add("転帰区分 <> ''");
            }

            if (filters.Count == 0)
            {
                filters.Add("転帰区分 = 'A'");
            }

            this.ListView1.ListFormat("開始日 desc, 連番 desc", AppString.ConcatList(filters, " or "));

            this.ListView1.Columns["病名"].Width = 140;
        }

        private void DiagCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.ListFormat();
        }

        private void DiagCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.ListFormat();
        }

        private void DPCButton1_Click(object sender, EventArgs e)
        {
            FormDiagDPC f = new FormDiagDPC();
            f.PatSet(this.Pat);
            f.ShowDialog(this);
        }
    }
}
