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
    public class CtrlPostItGridView1 : DataGridView
    {
        public DataSet DataSet1 = new DataSet();

        public CtrlPostItGridView1()
        {
            DataTable table = this.DataSet1.Tables.Add("付箋");

            table.Columns.Add("患者コード");
            table.Columns.Add("入外区分コード");
            table.Columns.Add("入外区分");
            table.Columns.Add("科コード");
            table.Columns.Add("科");
            table.Columns.Add("対象日");
            table.Columns.Add("日付");
            table.Columns.Add("連番", typeof(int));
            table.Columns.Add("内容");
            table.Columns.Add("表示順", typeof(int));
            table.Columns.Add("登録者コード");
            table.Columns.Add("登録者");
            table.Columns.Add("削除フラグ", typeof(bool));
        }

        public void ListShow(string pt_id, string sort = "対象日 desc, 表示順", string filter = "")
        {
            DataTable table = this.DataSet1.Tables["付箋"];
            table.Rows.Clear();

            List<PostIt> list = PostIt.GetList(pt_id);

            foreach (PostIt obj in list)
            {
                DataRow r = table.NewRow();

                r["患者コード"] = obj.PtId;
                r["入外区分コード"] = obj.InOut;
                r["入外区分"] = obj.InOutString;
                r["科コード"] = obj.DeptCode;
                r["科"] = obj.DeptName;
                r["対象日"] = obj.DoDate;
                r["日付"] = DateTimeAgent.DateFormat(obj.DoDate, DateTimeAgent.DateFormatKind.LONG);
                r["連番"] = obj.SEQ1;
                r["表示順"] = obj.SEQ2;
                r["内容"] = obj.Cont1;

                if (obj.UpStaffCode.Length > 0)
                {
                    r["登録者コード"] = obj.UpStaffCode;
                    r["登録者"] = obj.UpStaffName;
                }
                else
                {
                    r["登録者コード"] = obj.RegStaffCode;
                    r["登録者"] = obj.RegStaffName;
                }

                r["削除フラグ"] = obj.DeleteFlg;

                table.Rows.Add(r);
            }

            this.ListFormat(sort, filter);
        }

        public void ListClear()
        {
            DataTable table = this.DataSet1.Tables["付箋"];
            table.Rows.Clear();

            this.ListFormat();
        }

        public void ListFormat(string sort = "対象日 desc, 表示順", string filter = "")
        {
            DataView view = new DataView(this.DataSet1.Tables["付箋"]);

            this.DataSource = view;

            view.Sort = sort;
            view.RowFilter = filter;

            this.Columns["患者コード"].Visible = false;

            this.Columns["入外区分コード"].Visible = false;
            this.Columns["入外区分"].Visible = false;

            this.Columns["科コード"].Visible = false;

            this.Columns["科"].Width = 45;
            this.Columns["科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.Columns["対象日"].Visible = false;

            this.Columns["日付"].Width = 70;
            this.Columns["日付"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Columns["連番"].Visible = false;

            this.Columns["内容"].Width = 200;

            this.Columns["表示順"].Visible = false;
            this.Columns["登録者コード"].Visible = false;

            this.Columns["登録者"].Width = 70;
            this.Columns["登録者"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.Columns["削除フラグ"].Visible = false;

            foreach (DataGridViewRow r in this.Rows)
            {
                if (!r.Cells["対象日"].Value.ToString().Equals(DateTime.Now.ToString("yyyyMMdd")))
                {
                    r.DefaultCellStyle.BackColor = Color.LightGray;
                }

                if ((bool)r.Cells["削除フラグ"].Value)
                {
                    r.DefaultCellStyle.Font = AppFont.GetStrikeoutFont(this.Font);
                }
            }
        }

        public PostIt GetPostIt(int row)
        {
            PostIt obj = new PostIt();

            if (row < 0)
            {
                return obj;
            }

            if (row >= this.RowCount)
            {
                return obj;
            }

            DataGridViewRow r = this.Rows[row];

            obj = PostIt.Load(
                r.Cells["患者コード"].Value.ToString(),
                r.Cells["入外区分コード"].Value.ToString(),
                r.Cells["科コード"].Value.ToString(),
                r.Cells["対象日"].Value.ToString(),
                r.Cells["連番"].Value.ToString()
                );

            return obj;
        }
    }
}
