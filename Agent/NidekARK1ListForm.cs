using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class NidekARK1ListForm : Form
    {
        DataSet DSet = new DataSet();

        public NidekARK1ListForm()
        {
            InitializeComponent();
        }

        private void NidekARK1ListForm_Load(object sender, EventArgs e)
        {
            DataTable table = DSet.Tables.Add("Files");
            table.Columns.Add("時刻");
            table.Columns.Add("ファイル名");
            table.Columns.Add("Obj", typeof(NidekARK1));

            this.ListShow();
        }

        private void ListShowButton_Click(object sender, EventArgs e)
        {
            this.ListShow();
        }

        void ListShow()
        {
            this.DataBox1.Clear();

            DataTable table = DSet.Tables["Files"];
            table.Rows.Clear();

            List<NidekARK1> list = NidekARK1.GetList();

            foreach (NidekARK1 obj in list)
            {
                if (!obj.SaveDate.Equals(this.Date1.Value.ToString("yyyyMMdd")))
                {
                    continue;
                }

                DataRow r = table.NewRow();

                r["時刻"] = DateTimeAgent.TimeFormat6(obj.SaveTime);
                r["ファイル名"] = Path.GetFileName(obj.SourceFile);
                r["Obj"] = obj;

                table.Rows.Add(r);
            }

            DataView view = new DataView(table);

            this.ListView1.DataSource = view;

            this.ListView1.Columns["時刻"].Width = 50;
            this.ListView1.Columns["時刻"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.ListView1.Columns["ファイル名"].Width = 200;
            this.ListView1.Columns["ファイル名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["Obj"].Visible = false;
        }

        private void Date1_ValueChanged(object sender, EventArgs e)
        {
            this.ListShow();
        }

        private void ListView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.ListView1.SelectedCells.Count <= 0)
            {
                return;
            }

            if (this.ListView1.Rows[this.ListView1.SelectedCells[0].RowIndex].Cells["Obj"].Value is NidekARK1)
            {
                this.DataBox1.Text = ((NidekARK1)this.ListView1.Rows[this.ListView1.SelectedCells[0].RowIndex].Cells["Obj"].Value).ConvertData;
            }
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            if (this.DataBox1.Text.Length == 0)
            {
                MessageBox.Show("対象データがありません。\r\n左のリストから該当のファイルを選んでください。");
                return;
            }

            Clipboard.SetText(this.DataBox1.Text);
        }
    }
}
