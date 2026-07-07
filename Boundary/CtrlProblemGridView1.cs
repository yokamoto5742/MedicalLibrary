using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public class CtrlProblemGridView1 : DataGridView
    {
        public DataSet DataSet1 = new DataSet();

        public CtrlProblemGridView1()
        {
            DataTable table = this.DataSet1.Tables.Add("プロブレム");

            table.Columns.Add("患者コード");
            table.Columns.Add("入外区分");
            table.Columns.Add("プロブレムＮＯ", typeof(int));
            table.Columns.Add("問題内容");
            table.Columns.Add("発生日");
            table.Columns.Add("発生科");
            table.Columns.Add("発生登録者");
            table.Columns.Add("資格コード");
            table.Columns.Add("資格");
            table.Columns.Add("解決日");
            table.Columns.Add("解決科");
            table.Columns.Add("解決登録者");

            this.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }


        public void ListShow(string pt_id, string sort = "", string filter = "")
        {
            DataTable table = this.DataSet1.Tables["プロブレム"];
            table.Rows.Clear();

            List<ProblemData> list = ProblemData.GetList(pt_id);

            foreach (ProblemData obj in list)
            {
                // 削除されたものは表示しない
                if (obj.DeleteFlg)
                {
                    continue;
                }

                DataRow r = table.NewRow();

                r["患者コード"] = obj.PtId;
                r["入外区分"] = obj.InOutString;
                r["プロブレムＮＯ"] = obj.SEQ;
                r["問題内容"] = obj.Cont;
                r["発生日"] = obj.DateString1;
                r["発生科"] = obj.DeptName1;
                r["発生登録者"] = obj.StaffName1;
                r["資格コード"] = obj.SectionCode1;
                r["資格"] = obj.SectionName1;
                r["解決日"] = obj.DateString2;
                r["解決科"] = obj.DeptName2;
                r["解決登録者"] = obj.StaffName2;

                table.Rows.Add(r);
            }

            this.ListFormat(sort, filter);
        }

        public void ListClear()
        {
            DataTable table = this.DataSet1.Tables["プロブレム"];
            table.Rows.Clear();

            this.ListFormat();
        }

        public void ListFormat(string sort = "", string filter = "")
        {
            DataView view = new DataView(this.DataSet1.Tables["プロブレム"]);

            this.DataSource = view;

            view.Sort = sort;
            view.RowFilter = filter;

            this.Columns["患者コード"].Visible = false;

            this.Columns["入外区分"].Width = 40;
            this.Columns["入外区分"].HeaderText = "入外";
            this.Columns["入外区分"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Columns["プロブレムＮＯ"].Width = 25;
            this.Columns["プロブレムＮＯ"].HeaderText = "No";
            this.Columns["プロブレムＮＯ"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Columns["問題内容"].Width = 145;
            this.Columns["問題内容"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.Columns["問題内容"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            this.Columns["発生日"].Width = 70;
            this.Columns["発生日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Columns["発生科"].Width = 45;
            this.Columns["発生科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.Columns["発生登録者"].Width = 70;
            this.Columns["発生登録者"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.Columns["資格コード"].Visible = false;

            this.Columns["資格"].Width = 40;
            this.Columns["資格"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Columns["解決日"].Width = 70;
            this.Columns["解決日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Columns["解決科"].Width = 45;
            this.Columns["解決科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.Columns["解決登録者"].Width = 70;
            this.Columns["解決登録者"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            foreach (DataGridViewRow r in this.Rows)
            {
                if (r.Cells["解決日"].Value.ToString().Length > 0)
                {
                    r.DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }

        public ProblemData GetProblemData(int row)
        {
            ProblemData obj = new ProblemData();

            if (row < 0)
            {
                return obj;
            }

            if (row >= this.RowCount)
            {
                return obj;
            }

            DataGridViewRow r = this.Rows[row];

            obj = ProblemData.Load(
                r.Cells["患者コード"].Value.ToString(),
                r.Cells["プロブレムＮＯ"].Value.ToString()
                );

            return obj;
        }
    }
}
