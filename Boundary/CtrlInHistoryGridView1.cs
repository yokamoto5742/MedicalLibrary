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
    public class CtrlInHistoryGridView1 : DataGridView
    {
        public DataSet DataSet1 = new DataSet();

        public CtrlInHistoryGridView1()
        {
            DataTable table = this.DataSet1.Tables.Add("入院歴");

            table.Columns.Add("患者コード");
            table.Columns.Add("入院日8", typeof(int));
            table.Columns.Add("入院日");
            table.Columns.Add("入院区分コード");
            table.Columns.Add("入院区分");
            table.Columns.Add("退院日8", typeof(int));
            table.Columns.Add("退院日");
            table.Columns.Add("退院区分コード");
            table.Columns.Add("退院区分");
        }

        public void ListShow(string pt_id, string sort = "入院日8 desc", string filter = "")
        {
            DataTable table = this.DataSet1.Tables["入院歴"];
            table.Rows.Clear();

            List<PatIn> list = PatIn.GetHistory(pt_id);

            foreach (PatIn obj in list)
            {
                DataRow r = table.NewRow();

                r["患者コード"] = pt_id;
                r["入院日8"] = obj.InDate;
                r["入院日"] = obj.InDateString;
                r["入院区分コード"] = obj.InKind;
                r["入院区分"] = obj.InKindString;
                r["退院日8"] = obj.OutDate;
                r["退院日"] = obj.OutDateString;
                r["退院区分コード"] = obj.OutKind;
                r["退院区分"] = obj.OutKindString;

                table.Rows.Add(r);
            }

            this.ListFormat(sort, filter);
        }

        public void ListClear()
        {
            DataTable table = this.DataSet1.Tables["入院歴"];
            table.Rows.Clear();

            this.ListFormat();
        }

        public void ListFormat(string sort = "入院日8 desc", string filter = "")
        {
            DataView view = new DataView(this.DataSet1.Tables["入院歴"]);

            this.DataSource = view;

            view.Sort = sort;
            view.RowFilter = filter;

            this.Columns["患者コード"].Visible = false;

            this.Columns["入院日8"].Visible = false;

            this.Columns["入院日"].Width = 70;
            this.Columns["入院日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Columns["入院区分コード"].Visible = false;

            this.Columns["入院区分"].Width = 50;
            this.Columns["入院区分"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Columns["退院日8"].Visible = false;

            this.Columns["退院日"].Width = 70;
            this.Columns["退院日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Columns["退院区分コード"].Visible = false;

            this.Columns["退院区分"].Width = 50;
            this.Columns["退院区分"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public PatIn GetPatIn(int row)
        {
            PatIn obj = new PatIn();

            if (row < 0)
            {
                return obj;
            }

            if (row >= this.RowCount)
            {
                return obj;
            }

            DataGridViewRow r = this.Rows[row];

            obj.Id = r.Cells["患者コード"].Value.ToString();
            obj.InDate = r.Cells["入院日8"].Value.ToString();
            obj.InKind = r.Cells["入院区分コード"].Value.ToString();
            obj.OutDate = r.Cells["退院日8"].Value.ToString();
            obj.OutKind = r.Cells["退院区分コード"].Value.ToString();

            return obj;
        }
    }
}
