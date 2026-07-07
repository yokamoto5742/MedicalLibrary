using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MedicalLibrary.Entity;

namespace MedicalLibrary.Boundary
{
    public partial class FormFindOpe : Form
    {
        DataSet DSet = new DataSet();

        DPCOpeMaster _OpeMaster = new DPCOpeMaster();

        public DPCOpeMaster OpeMaster
        {
            get
            {
                return this._OpeMaster;
            }
        }


        public FormFindOpe(string s = "")
        {
            InitializeComponent();

            this.OpeFindBox.Text = s;
        }

        private void FormFindOpe_Load(object sender, EventArgs e)
        {
            DataTable table = DSet.Tables.Add("List");

            table.Columns.Add("SEQ");
            table.Columns.Add("KCode");
            table.Columns.Add("術式");
            table.Columns.Add("STEM7");
            table.Columns.Add("コメント");
            table.Columns.Add("Obj", typeof(DPCOpeMaster));

            if (this.OpeFindBox.Text.Length > 0)
            {
                this.ListShow();
            }
        }

        void DataClear()
        {
            this._OpeMaster = new DPCOpeMaster();

            this.OpeNameBox.Clear();
            this.KCodeBox.Clear();
            this.STEMBox.Clear();
            this.ContBox.Clear();
        }

        private void OpeFindBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ListShow();
            }
        }

        void ListShow()
        {
            if (this.OpeFindBox.Text.Length == 0) return;

            if (!DSet.Tables.Contains("List")) return;

            this.DataClear();

            DataTable table = DSet.Tables["List"];
            table.Clear();

            List<DPCOpeMaster> list;

            if (this.OpeFindBox.Text.StartsWith("K"))
            {
                // Kコードで検索
                list = DPCOpeMaster.GetListByKCode(this.OpeFindBox.Text);
            }
            else if (Regex.IsMatch(this.OpeFindBox.Text, "^[a-zA-Z0-9 ]*$"))
            {
                // STEM7 で検索
                list = DPCOpeMaster.GetListBySTEM7(this.OpeFindBox.Text);
            }
            else
            {
                // 名称で検索
                list = DPCOpeMaster.GetListByName(this.OpeFindBox.Text);
            }

            foreach (DPCOpeMaster obj in list)
            {
                DataRow r = table.NewRow();

                r["SEQ"] = obj.SEQ;
                r["KCode"] = obj.KCode;
                r["術式"] = obj.Name;
                r["STEM7"] = obj.STEM7;
                r["コメント"] = obj.Cont;
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

            this.ListView.DataSource = view;

            this.ListView.Columns["SEQ"].Visible = false;

            this.ListView.Columns["KCode"].Width = 60;
            this.ListView.Columns["術式"].Width = 210;
            this.ListView.Columns["STEM7"].Width = 60;
            this.ListView.Columns["コメント"].Width = 120;

            this.ListView.Columns["Obj"].Visible = false;
        }

        private void ListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            DPCOpeMaster obj = (DPCOpeMaster)this.ListView.Rows[e.RowIndex].Cells["Obj"].Value;

            this._OpeMaster = obj;
            this.OpeNameBox.Text = obj.Name;
            this.KCodeBox.Text = obj.KCode;
            this.STEMBox.Text = obj.STEM7;
            this.ContBox.Text = obj.Cont;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
