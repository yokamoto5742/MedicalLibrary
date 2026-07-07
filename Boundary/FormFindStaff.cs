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
    public partial class FormFindStaff : Form
    {
        /*
        List<TextBox> IdBoxList;
        List<TextBox> NameBoxList;
        */

        DataSet dSet = new DataSet();

        public FormFindStaff()
        {
            InitializeComponent();

            this.SelectButton1.Enabled = false;

            DataTable table = dSet.Tables.Add("Eˆõ");
            table.Columns.Add("ID");
            table.Columns.Add("–¼");
            table.Columns.Add("ƒJƒi");
            table.Columns.Add("Š‘®");
            table.Columns.Add("‘Ši");
            table.Columns.Add("í•Ê");
        }
/*
        public FormFindStaff(List<TextBox> id_box_list)
        {
            InitializeComponent();

            this.IdBoxList = id_box_list;

            this.SelectButton1.Enabled = false;

            DataTable table = dSet.Tables.Add("Eˆõ");
            table.Columns.Add("ID");
            table.Columns.Add("–¼");
            table.Columns.Add("ƒJƒi");
            table.Columns.Add("Š‘®");
            table.Columns.Add("‘Ši");
            table.Columns.Add("í•Ê");
        }

        public FormFindStaff(List<TextBox> id_box_list, List<TextBox> name_box_list)
        {
            InitializeComponent();

            this.IdBoxList = id_box_list;
            this.NameBoxList = name_box_list;

            this.SelectButton1.Enabled = false;

            DataTable table = dSet.Tables.Add("Eˆõ");
            table.Columns.Add("ID");
            table.Columns.Add("–¼");
            table.Columns.Add("ƒJƒi");
            table.Columns.Add("Š‘®");
            table.Columns.Add("‘Ši");
            table.Columns.Add("í•Ê");
        }
*/
        public static List<Staff> FindStaff()
        {
            FormFindStaff f = new FormFindStaff();
            f.ShowDialog();
            List<Staff> list = f.GetList();
            f.Dispose();

            return list;
        }

        void ListClear()
        {
            DataTable table = dSet.Tables["Eˆõ"];
            table.Rows.Clear();

            this.ListFormat();
        }

        private void ListShow()
        {
            DataTable table = dSet.Tables["Eˆõ"];
            table.Rows.Clear();

            if (NameBox1.Text.Length > 0 || KanaBox1.Text.Length > 0)
            {
                List<Staff> list = Staff.GetListByNameKana(NameBox1.Text, KanaBox1.Text);

                foreach (Staff obj in list)
                {
                    DataRow r = table.NewRow();

                    r["ID"] = obj.Code;
                    r["–¼"] = obj.Name;
                    r["ƒJƒi"] = obj.Kana;
                    r["Š‘®"] = obj.SectionShortName;
                    r["‘Ši"] = obj.QualShortName;
                    r["í•Ê"] = obj.StatusName;

                    table.Rows.Add(r);
                }
            }

            this.ListFormat();
        }

        private void ListDoctorShow()
        {
            DataTable table = dSet.Tables["Eˆõ"];
            table.Rows.Clear();

            List<Staff> list = Staff.GetListBySectionStatus("1", "1");

            foreach (Staff obj in list)
            {
                DataRow r = table.NewRow();

                r["ID"] = obj.Code;
                r["–¼"] = obj.Name;
                r["ƒJƒi"] = obj.Kana;
                r["Š‘®"] = obj.SectionShortName;
                r["‘Ši"] = obj.QualShortName;
                r["í•Ê"] = obj.StatusName;

                table.Rows.Add(r);
            }

            this.ListFormat();
        }

        void ListFormat()
        {
            DataView view = new DataView(dSet.Tables["Eˆõ"]);

            this.ListView1.DataSource = view;

            this.ListView1.Columns["ID"].Width = 55;
            this.ListView1.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.ListView1.Columns["–¼"].Width = 80;
            this.ListView1.Columns["–¼"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["ƒJƒi"].Width = 80;
            this.ListView1.Columns["ƒJƒi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

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

        public List<Staff> GetList()
        {
            /*
            int count = 0;

            for (int i = 0; i < this.ListView1.SelectedRows.Count; i++)
            {
                while (count < this.IdBoxList.Count && count < this.NameBoxList.Count)
                {
                    if (this.IdBoxList[count].Text.Length == 0)
                    {
                        this.IdBoxList[count].Text = this.ListView1.SelectedRows[i].Cells["ID"].Value.ToString().Trim();
                        this.NameBoxList[count].Text = this.ListView1.SelectedRows[i].Cells["–¼"].Value.ToString().Trim();
                        break;
                    }

                    count++;
                }
            }
             */

            List<Staff> list = new List<Staff>();

            for (int i = 0; i < this.ListView1.SelectedRows.Count; i++)
            {
                if (Dict.StaffDict.ContainsKey(this.ListView1.SelectedRows[i].Cells["ID"].Value.ToString()))
                {
                    list.Add(Dict.StaffDict[this.ListView1.SelectedRows[i].Cells["ID"].Value.ToString()]);
                }
            }

            return list;
        }

        private void FindButton1_Click(object sender, EventArgs e)
        {
            this.ListShow();
        }

        private void SelectButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DoctorButton1_Click(object sender, EventArgs e)
        {
            this.ListDoctorShow();
        }

        private void ClearButton1_Click(object sender, EventArgs e)
        {
            this.NameBox1.Clear();
            this.KanaBox1.Clear();

            this.ListClear();

            this.NameBox1.Select();
        }

        private void FormFindStaff_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                this.ListShow();
            }
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
                this.FindButton1.Select();
            }
        }
    }
}