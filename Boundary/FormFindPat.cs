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
    public partial class FormFindPat : Form
    {
        DataSet dSet = new DataSet();

        public FormFindPat()
        {
            InitializeComponent();

            this.SelectButton1.Enabled = false;

            DataTable table = dSet.Tables.Add("患者");
            table.Columns.Add("ID");
            table.Columns.Add("氏名");
            table.Columns.Add("カナ");
            table.Columns.Add("性別");
            table.Columns.Add("生年月日");
            table.Columns.Add("年齢");
            table.Columns.Add("郵便");
            table.Columns.Add("住所");
            table.Columns.Add("電話");
        }

        public static PatBase FindPat()
        {
            FormFindPat f = new FormFindPat();
            f.ShowDialog();
            PatBase p = f.GetPat();
            f.Dispose();

            return p;
        }

        private void FindButton1_Click(object sender, EventArgs e)
        {
            this.ListShow();
        }

        private void ListClear()
        {
            DataTable table = dSet.Tables["患者"];
            table.Rows.Clear();

            this.ListFormat();
        }

        private void ListShow()
        {
            DataTable table = dSet.Tables["患者"];
            table.Rows.Clear();

            if (NameBox1.Text.Length > 0 || KanaBox1.Text.Length > 0 || BirthBox1.Text.Length >= 5)
            {
                string birth = "";

                if (BirthBox1.Text.Length == 8)
                {
                    birth = this.BirthBox1.Text;
                }
                else if (GenBox1.Text.Length > 0)
                {
                    int gen = 0;
                    int.TryParse(GenBox1.Text, out gen);

                    int gyymmdd = 0;
                    int.TryParse(BirthBox1.Text, out gyymmdd);

                    birth = DateTimeAgent.JtoW(gen, gyymmdd).ToString();
                }

                List<PatBase> list = PatBase.GetListByNameKanaBirth(NameBox1.Text, KanaBox1.Text, birth);

                foreach (PatBase obj in list)
                {
                    DataRow r = table.NewRow();

                    r["ID"] = obj.Id;
                    r["氏名"] = obj.Name;
                    r["カナ"] = obj.Kana;
                    r["性別"] = obj.SexNameShort;
                    r["生年月日"] = obj.BirthStringJ;
                    r["年齢"] = obj.Age;
                    r["郵便"] = obj.Post;
                    r["住所"] = obj.Addr;
                    r["電話"] = obj.Tel;

                    table.Rows.Add(r);
                }
            }

            this.ListFormat();
        }

        void ListFormat()
        {
            DataView view = new DataView(this.dSet.Tables["患者"]);

            this.ListView1.DataSource = view;

            this.ListView1.Columns["ID"].Width = 55;
            this.ListView1.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.ListView1.Columns["氏名"].Width = 80;
            this.ListView1.Columns["氏名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["カナ"].Width = 80;
            this.ListView1.Columns["カナ"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["性別"].Width = 40;
            this.ListView1.Columns["性別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.ListView1.Columns["生年月日"].Width = 105;
            this.ListView1.Columns["生年月日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["年齢"].Width = 40;
            this.ListView1.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.ListView1.Columns["郵便"].Width = 60;
            this.ListView1.Columns["郵便"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["住所"].Width = 160;
            this.ListView1.Columns["住所"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["電話"].Width = 85;
            this.ListView1.Columns["電話"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;


            foreach (DataGridViewRow r in this.ListView1.Rows)
            {
                if (r.Cells["性別"].Value.ToString().Equals("女"))
                {
                    r.Cells["氏名"].Style.ForeColor = Color.Red;
                    r.Cells["カナ"].Style.ForeColor = Color.Red;
                    r.Cells["性別"].Style.ForeColor = Color.Red;
                }
            }

            if (this.ListView1.RowCount > 0)
            {
                this.ListView1.Focus();
                this.SelectButton1.Enabled = true;
            }
            else
            {
                this.SelectButton1.Enabled = false;
            }
        }

        private void SelectButton1_Click(object sender, EventArgs e)
        {
            if (this.ListView1.CurrentRow != null)
            {
                this.Close();
            }
        }

        private void ListView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            // クリップボードに貼り付ける
            Clipboard.SetText(ListView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
        }

        private void ListView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            // クリップボードに貼り付ける
            Clipboard.SetText(ListView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());

            this.Close();
        }

        public PatBase GetPat()
        {
            PatBase p = new PatBase();

            if (this.ListView1.CurrentRow != null)
            {
                p = PatBase.Load(this.ListView1.CurrentRow.Cells["ID"].Value.ToString());
            }

            return p;
        }

        private void NameBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.KanaBox1.Select();
            }
        }

        private void KanaBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.GenBox1.Select();
            }
        }

        private void GenBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.BirthBox1.Select();
            }
        }

        private void BirthBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.FindButton1.Select();
            }
        }

        private void ClearButton1_Click(object sender, EventArgs e)
        {
            this.NameBox1.Clear();
            this.KanaBox1.Clear();
            this.GenBox1.Clear();
            this.BirthBox1.Clear();

            this.ListClear();

            this.NameBox1.Select();
        }

        private void FormFindPat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                this.ListShow();
            }
        }
    }
}