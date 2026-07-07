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
    public partial class FormRsvs : Form
    {
        DataSet dSet = new DataSet();

        List<RsvMaster> masterList = new List<RsvMaster>();

        Dictionary<Tuple<string, string>, FormRsv> formDict = new Dictionary<Tuple<string, string>, FormRsv>();

        public FormRsvs()
        {
            InitializeComponent();

            DataTable table = dSet.Tables.Add("RsvMaster");
            table.Columns.Add("予約種別コード");
            table.Columns.Add("予約種別");
            table.Columns.Add("科");
            table.Columns.Add("種別");

            table = dSet.Tables.Add("RsvDetail");
            table.Columns.Add("予約種別コード");
            table.Columns.Add("予約種別");
            table.Columns.Add("予約詳細コード");
            table.Columns.Add("予約詳細");

            KindButton1.Checked = true;
        }

        private void FormRsvs_Load(object sender, EventArgs e)
        {
            this.ListShow1();
        }

        void ListShow1()
        {
            DataTable table = dSet.Tables["RsvMaster"];
            table.Clear();

            // デフォルトで２８日先を基準にして予約マスターを取得する
            this.masterList = RsvMaster.GetListByDate1(DateTime.Now.AddDays(28).ToString("yyyyMMdd"));

            foreach (RsvMaster obj in this.masterList)
            {
                DataRow r = table.NewRow();

                r["予約種別コード"] = obj.Code1;
                r["予約種別"] = obj.Name1;
                r["科"] = obj.DeptName;
                r["種別"] = obj.Kind1;

                table.Rows.Add(r);
            }

            this.ListFormat1();
        }

        void ListFormat1()
        {
            DataView view = new DataView(dSet.Tables["RsvMaster"]);

            string filter = "";

            if (KindButton1.Checked)
            {
                filter = "種別 = 'Doctor'";
            }
            else
            {
                filter = "種別 = 'Kensa'";
            }

            if (this.KeywordBox1.Text.Length > 0)
            {
                filter += " and (予約種別コード like '%" + this.KeywordBox1.Text + "%' or 予約種別 like '%" + this.KeywordBox1.Text + "%' or 科 like '%" + this.KeywordBox1.Text + "%')";
            }

            view.RowFilter = filter;

            ListView1.DataSource = view;

            ListView1.Columns["予約種別コード"].HeaderText = "コード";
            ListView1.Columns["予約種別コード"].Width = 40;
            ListView1.Columns["予約種別"].Width = 110;
            ListView1.Columns["科"].Width = 60;
            ListView1.Columns["種別"].Visible = false;
        }

        void ListShow2(string code1, string name1)
        {
            List<RsvMaster> detail_list = RsvMaster.GetDetailList(code1);

            DataTable table = dSet.Tables["RsvDetail"];
            table.Clear();

            foreach (RsvMaster obj in detail_list)
            {
                DataRow r = table.NewRow();

                r["予約種別コード"] = obj.Code1;
                r["予約種別"] = name1;
                r["予約詳細コード"] = obj.Code2;
                r["予約詳細"] = obj.Name2;

                table.Rows.Add(r);
            }

            DataView view = new DataView(table);
            ListView2.DataSource = view;

            ListView2.Columns["予約種別コード"].Visible = false;
            ListView2.Columns["予約種別"].Visible = false;
            ListView2.Columns["予約詳細コード"].HeaderText = "コード";
            ListView2.Columns["予約詳細コード"].Width = 40;
            ListView2.Columns["予約詳細"].Width = 170;
        }

        private void KindButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.ListFormat1();
        }

        private void KindButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.ListFormat1();
        }

        private void KeywordBox1_TextChanged(object sender, EventArgs e)
        {
            this.ListFormat1();
        }

        private void ListView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string code1 = ListView1.CurrentRow.Cells["予約種別コード"].Value.ToString();
            string name1 = ListView1.CurrentRow.Cells["予約種別"].Value.ToString();

            this.ListShow2(code1, name1);
        }

        private void ListView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string code1 = ListView1.CurrentRow.Cells["予約種別コード"].Value.ToString();
            string name1 = ListView1.CurrentRow.Cells["予約種別"].Value.ToString();

            List<RsvMaster> detail_list = RsvMaster.GetDetailList(code1);

            // 詳細が存在する場合は終了
            if (detail_list.Count > 0)
            {
                return;
            }

            this.RsvShow(int.Parse(DateTime.Now.ToString("yyyyMMdd")), code1, name1);
        }

        private void ListView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string code1 = ListView2.CurrentRow.Cells["予約種別コード"].Value.ToString();
            string name1 = ListView2.CurrentRow.Cells["予約種別"].Value.ToString();
            string code2 = ListView2.CurrentRow.Cells["予約詳細コード"].Value.ToString();
            string name2 = ListView2.CurrentRow.Cells["予約詳細"].Value.ToString();

            this.RsvShow(int.Parse(DateTime.Now.ToString("yyyyMMdd")), code1, name1, code2, name2);
        }


        public void RsvShow(int crit_date, string code1, string name1, string code2 = "", string name2 = "")
        {
            // 予約詳細が 0 の場合も空白の場合も同じとみなす
            Tuple<string, string> key = new Tuple<string, string>(code1, code2.TrimStart('0'));

            if (!formDict.ContainsKey(key))
            {
                formDict.Add(key, new FormRsv());
            }

            FormRsv f = formDict[key];

            f.CodeSet(crit_date, code1, name1, code2, name2);
            f.PatSet(AppStat.CurrentPat);
            f.MdiParent = this;
            f.Show();
            f.Activate();

            if (f.WindowState == FormWindowState.Minimized)
            {
                f.WindowState = FormWindowState.Normal;
            }
        }
    }
}
