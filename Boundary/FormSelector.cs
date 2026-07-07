using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MedicalLibrary.Boundary
{
    public partial class FormSelector : Form
    {
        DataSet dSet = new DataSet();

        Dictionary<string, string> dataDict;


        public FormSelector(Dictionary<string, string> data_dict)
        {
            InitializeComponent();

            dSet.Tables.Add("Item");

            this.dataDict = data_dict;
        }

        public void Set(List<FormSelectorColumn> col_list, List<Dictionary<string, string>> data_list)
        {
            DataTable table = dSet.Tables["Item"];
            table.Rows.Clear();
            table.Columns.Clear();

            foreach (FormSelectorColumn col in col_list)
            {
                table.Columns.Add(col.ColumnName);
            }

            foreach (Dictionary<string, string> data in data_list)
            {
                DataRow r = table.NewRow();

                foreach (string s in data.Keys)
                {
                    if (table.Columns.Contains(s))
                    {
                        r[s] = data[s];
                    }
                }

                table.Rows.Add(r);
            }

            ListView1.DataSource = new DataView(table);
            int width = 0;

            foreach (FormSelectorColumn col in col_list)
            {
                ListView1.Columns[col.ColumnName].DefaultCellStyle.Alignment = col.Alignment;
                ListView1.Columns[col.ColumnName].HeaderText = col.HeaderText;
                ListView1.Columns[col.ColumnName].Width = col.Width;
                ListView1.Columns[col.ColumnName].Visible = col.Visible;

                if (col.Visible)
                {
                    width += col.Width;
                }
            }

            this.Width = width + 60;
        }

        private void KeywordBox1_TextChanged(object sender, EventArgs e)
        {
            DataTable table = dSet.Tables["Item"];
            string filter = "";

            if (this.KeywordBox1.Text.Length > 0)
            {
                foreach (DataColumn col in table.Columns)
                {
                    if (filter.Length > 0)
                    {
                        filter += " or ";
                    }

                    filter += col.ColumnName + " like '%" + KeywordBox1.Text + "%'";
                }
            }

            DataView view = new DataView(table);
            view.RowFilter = filter;

            ListView1.DataSource = view;
        }

        private void ListView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.DataSelect();
        }

        private void ListView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DataSelect();
            }
        }

        void DataSelect()
        {
            this.dataDict.Clear();

            if (ListView1.CurrentRow != null)
            {
                DataGridViewRow r = ListView1.CurrentRow;

                foreach (DataGridViewCell cell in r.Cells)
                {
                    this.dataDict.Add(cell.OwningColumn.Name, cell.Value.ToString());
                }
            }

            this.Dispose();
        }
    }

    public class FormSelectorColumn
    {
        public string ColumnName = "";

        string headerText = "";

        public string HeaderText
        {
            set
            {
                this.headerText = value;
            }
            get
            {
                if (this.headerText.Length > 0)
                {
                    return this.headerText;
                }
                else
                {
                    return this.ColumnName;
                }
            }
        }

        public DataGridViewContentAlignment Alignment = DataGridViewContentAlignment.MiddleLeft;

        public int Width = 70;

        public bool Visible = true;
    }
}
