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
    public partial class FormFindPatIns : Form
    {
        string PtId = "";
        Dictionary<int, PatIns> Dict = new Dictionary<int, PatIns>();

        DataSet dSet = new DataSet();

        public FormFindPatIns(string pt_id)
        {
            InitializeComponent();

            this.PtId = pt_id;

            DataTable table = dSet.Tables.Add("保険");
            table.Columns.Add("連番");
            table.Columns.Add("有効");
            table.Columns.Add("保険番号");
            table.Columns.Add("被保険者記号");
            table.Columns.Add("被保険者番号");
            table.Columns.Add("種別");
            table.Columns.Add("外来負担率");
            table.Columns.Add("入院負担率");
            table.Columns.Add("開始日");
            table.Columns.Add("終了日");
        }

        private void FormFindPatIns_Load(object sender, EventArgs e)
        {
            this.ListShow();
        }

        public static string FindPatInsSEQ(string pt_id)
        {
            string s = "";

            FormFindPatIns f = new FormFindPatIns(pt_id);
            DialogResult r = f.ShowDialog();

            if (r == DialogResult.OK)
            {
                s = f.GetPatInsSEQ();
            }

            f.Dispose();

            return s;
        }

        public static PatIns FindPatIns(string pt_id)
        {
            PatIns p = new PatIns();

            FormFindPatIns f = new FormFindPatIns(pt_id);
            DialogResult r = f.ShowDialog();

            if (r == DialogResult.OK)
            {
                p = f.GetPatIns();
            }

            f.Dispose();

            return p;
        }

        string GetPatInsSEQ()
        {
            string s = "";

            if (this.ListView1.CurrentRow != null)
            {
                s = this.ListView1.CurrentRow.Cells["連番"].Value.ToString();
            }

            return s;
        }

        PatIns GetPatIns()
        {
            PatIns obj = new PatIns();

            if (this.ListView1.CurrentRow != null)
            {
                int seq = 0;

                if (int.TryParse(this.ListView1.CurrentRow.Cells["連番"].Value.ToString(), out seq) &&
                    this.Dict.ContainsKey(seq))
                {
                    obj = this.Dict[seq];
                }
            }

            return obj;
        }

        void ListClear()
        {
            DataTable table = dSet.Tables["保険"];
            table.Rows.Clear();

            this.ListFormat();
        }

        void ListShow()
        {
            DataTable table = dSet.Tables["保険"];
            table.Rows.Clear();

            this.Dict = PatIns.GetDict(this.PtId);

            foreach (PatIns obj in this.Dict.Values)
            {
                DataRow r = table.NewRow();

                r["連番"] = obj.SEQ;
                r["有効"] = obj.IsValid ? "" : "×";
                r["保険番号"] = obj.Code;
                r["被保険者記号"] = obj.Name1;
                r["被保険者番号"] = obj.Name2;
                r["種別"] = obj.KindNameShort;
                r["外来負担率"] = obj.Per1;
                r["入院負担率"] = obj.Per2;
                r["開始日"] = obj.StartDateString;
                r["終了日"] = obj.EndDateString;

                table.Rows.Add(r);
            }

            this.ListFormat();

            if (this.Dict.Count > 7)
            {
                this.Height = 250 + (this.Dict.Count - 7) * 22;
            }
        }

        void ListFormat()
        {
            DataView view = new DataView(this.dSet.Tables["保険"]);

            this.ListView1.DataSource = view;

            this.ListView1.Columns["連番"].Width = 35;
            this.ListView1.Columns["連番"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.ListView1.Columns["有効"].Width = 35;
            this.ListView1.Columns["有効"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.ListView1.Columns["保険番号"].Width = 70;
            this.ListView1.Columns["保険番号"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["被保険者記号"].Width = 100;
            this.ListView1.Columns["被保険者記号"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["被保険者番号"].Width = 80;
            this.ListView1.Columns["被保険者番号"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["種別"].Width = 50;
            this.ListView1.Columns["種別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["外来負担率"].Width = 40;
            this.ListView1.Columns["外来負担率"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.ListView1.Columns["入院負担率"].Width = 40;
            this.ListView1.Columns["入院負担率"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.ListView1.Columns["開始日"].Width = 80;
            this.ListView1.Columns["開始日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.ListView1.Columns["終了日"].Width = 80;
            this.ListView1.Columns["終了日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewRow r in this.ListView1.Rows)
            {
                if (r.Cells["有効"].Value.ToString().Equals("×"))
                {
                    r.DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }

        private void ListView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
