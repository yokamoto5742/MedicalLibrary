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

namespace MedicalLibrary.Boundary
{
    public partial class FormDPCICD : StdForm1
    {
        FormDPCManager F;

        DataSet dSet = new DataSet();

        public FormDPCICD(FormDPCManager f)
        {
            InitializeComponent();

            this.F = f;

            DataTable table = dSet.Tables.Add("ICD");
            table.Columns.Add("病名");
            table.Columns.Add("ICD名称");
            table.Columns.Add("ICD");
            table.Columns.Add("MDC");
        }

        void ListShow()
        {
            if (this.DiagNameBox1.Text.Length < 2)
            {
                return;
            }

            List<DPCICD> list = new List<DPCICD>();
//            List<DPCICD> list = DPCICD.GetListByDiagName(this.DiagNameBox1.Text);

            DataTable table = dSet.Tables["ICD"];
            table.Rows.Clear();

            foreach (DPCICD obj in list)
            {
                if (obj.MDC.Length == 0)
                {
//                    continue;
                }

                DataRow r = table.NewRow();

                r["病名"] = obj.DiagName;
                r["ICD名称"] = obj.ICDName;
                r["ICD"] = obj.ICDCode;
                r["MDC"] = obj.MDC + obj.Group;

                table.Rows.Add(r);
            }

            this.ListFormat();
        }

        void ListFormat()
        {
            DataView view = new DataView(dSet.Tables["ICD"]);

            ListView1.DataSource = view;

            ListView1.Columns["病名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView1.Columns["病名"].Width = 200;

            ListView1.Columns["ICD名称"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView1.Columns["ICD名称"].Width = 250;

            ListView1.Columns["ICD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView1.Columns["ICD"].Width = 50;

            ListView1.Columns["MDC"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView1.Columns["MDC"].Width = 50;
        }

        private void ListView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            this.F.MDCSet(ListView1.Rows[e.RowIndex].Cells["MDC"].Value.ToString());
            this.Dispose();
        }

        private void DiagNameBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ListShow();
            }
        }
    }
}
