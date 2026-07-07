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
    public partial class FormDiagNan : Form
    {
        DataSet DSet = new DataSet();

        public DiagNan SelectedDiagNan
        {
            get
            {
                DiagNan obj = new DiagNan();

                if (this.ListView.SelectedRows.Count > 0)
                {
                    DataGridViewRow r = this.ListView.SelectedRows[0];
                    obj = (DiagNan)r.Cells["Obj"].Value;
                }

                return obj;
            }
        }

        public FormDiagNan(string code = "")
        {
            InitializeComponent();

            this.FilterBox.Text = DiagNan.GetData(code).Name;
        }

        private void FormDiagNan_Load(object sender, EventArgs e)
        {
            DataTable table = DSet.Tables.Add("List");

            table.Columns.Add("Code");
            table.Columns.Add("Name");
            table.Columns.Add("Cont1");
            table.Columns.Add("Status");
            table.Columns.Add("Obj", typeof(DiagNan));

            this.ListShow();
        }

        void ListShow()
        {
            DataTable table = DSet.Tables["List"];
            table.Clear();

            List<DiagNan> list = DiagNan.List;

            foreach (DiagNan obj in list)
            {
                DataRow r = table.NewRow();

                r["Code"] = obj.Code;
                r["Name"] = obj.Name;
                r["Cont1"] = obj.Cont1;
                r["Status"] = obj.Status;
                r["Obj"] = obj;

                table.Rows.Add(r);
            }

            this.ListFormat();
        }

        void ListFormat()
        {
            if (!DSet.Tables.Contains("List")) return;

            DataTable table = DSet.Tables["List"];

            DataView view = new DataView(table);

            List<string> filters = new List<string>();

            if (this.FilterBox.Text.Length > 0)
            {
                filters.Add("(Name like '%" + this.FilterBox.Text + "%')");
            }

            view.RowFilter = AppString.ConcatList(filters, " and ");

            this.ListView.DataSource = view;

            this.ListView.Columns["Code"].Width = 40;
            this.ListView.Columns["Code"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.ListView.Columns["Code"].HeaderText = "番号";

            this.ListView.Columns["Name"].Width = 300;
            this.ListView.Columns["Name"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.ListView.Columns["Name"].HeaderText = "病名";

            this.ListView.Columns["Cont1"].Width = 80;
            this.ListView.Columns["Cont1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.ListView.Columns["Cont1"].HeaderText = "指定開始";

            this.ListView.Columns["Status"].Visible = false;

            this.ListView.Columns["Obj"].Visible = false;
        }

        private void ListView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void FilterBox_TextChanged(object sender, EventArgs e)
        {
            this.ListFormat();
        }
    }
}
