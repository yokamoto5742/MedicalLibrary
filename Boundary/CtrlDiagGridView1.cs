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
    public class CtrlDiagGridView1 : DataGridView
    {
        public DataSet DataSet1 = new DataSet();

        public CtrlDiagGridView1()
        {
            DataTable table = this.DataSet1.Tables.Add("病名");

            table.Columns.Add("患者コード");
            table.Columns.Add("主病名");
            table.Columns.Add("告知");
            table.Columns.Add("保険病名");
            table.Columns.Add("連番", typeof(int));
            table.Columns.Add("病名コード");
            table.Columns.Add("病名");
            table.Columns.Add("科コード");
            table.Columns.Add("科");
            table.Columns.Add("ＤＲコード");
            table.Columns.Add("ＤＲ");
            table.Columns.Add("入外区分コード");
            table.Columns.Add("入外区分");
            table.Columns.Add("保険パターン");
            table.Columns.Add("保険");
            table.Columns.Add("開始日");
            table.Columns.Add("確定日");
            table.Columns.Add("転帰日");
            table.Columns.Add("転帰区分");

            for (int i = 1; i <= 15; i++)
            {
                table.Columns.Add("接頭語" + AppString.HanToZen(i.ToString()).PadLeft(2, '０'));
            }

            for (int i = 1; i <= 5; i++)
            {
                table.Columns.Add("接尾語" + AppString.HanToZen(i.ToString()).PadLeft(2, '０'));
            }

            table.Columns.Add("ICDコード");
            table.Columns.Add("ICDコード2");
            table.Columns.Add("付加コード");
            table.Columns.Add("Obj", typeof(Diag));
        }

        public void ListShow(string pt_id, string sort = "開始日 desc, 連番 desc", string filter = "")
        {
            DataTable table = this.DataSet1.Tables["病名"];
            table.Rows.Clear();

            List<Diag> list = Diag.GetList(pt_id);

            foreach (Diag obj in list)
            {
                // 削除されたものは飛ばす
                if (obj.DeleteFlg)
                {
                    continue;
                }

                // 未確定のものは飛ばす
                if (!obj.FixFlg)
                {
                    continue;
                }

                DataRow r = table.NewRow();

                r["患者コード"] = obj.PtId;
                r["主病名"] = obj.MainFlg ? "●" : "";
                r["告知"] = obj.NoticeFlg ? "●" : "";
                r["保険病名"] = obj.InsFlg ? "●" : "";
                r["開始日"] = AppDateTime.DateStringFromString(obj.StartDate);
                r["連番"] = obj.SEQ;
                r["病名コード"] = obj.DiagCode;
                r["病名"] = obj.DiagName;
                r["科コード"] = obj.Dept;
                r["科"] = Dict.DeptDict[obj.Dept].ShortName;
                r["ＤＲコード"] = obj.Doctor;
                r["ＤＲ"] = Dict.DoctorDict.ContainsKey(obj.Doctor) ? Dict.DoctorDict[obj.Doctor].Name : "";
                r["確定日"] = AppDateTime.DateStringFromLong(obj.FixDate);
                r["保険パターン"] = obj.InsKind;
                r["保険"] = obj.InsKindName;
                r["転帰区分"] = obj.OutcomeString;
                r["転帰日"] = AppDateTime.DateStringFromString(obj.OutcomeDate);
                r["入外区分コード"] = obj.InOut;
                r["入外区分"] = obj.InOutString;

                for (int i = 1; i <= 15; i++)
                {
                    r["接頭語" + AppString.HanToZen(i.ToString()).PadLeft(2, '０')] = obj.PrefixList[i - 1];
                }

                for (int i = 1; i <= 5; i++)
                {
                    r["接尾語" + AppString.HanToZen(i.ToString()).PadLeft(2, '０')] = obj.SuffixList[i - 1];
                }

                r["ICDコード"] = obj.ICDCode1;
                r["ICDコード2"] = obj.ICDCode2;
                r["付加コード"] = obj.PlusCode;

                r["Obj"] = obj;

                table.Rows.Add(r);
            }

            this.ListFormat(sort, filter);
        }

        public void ListClear()
        {
            DataTable table = this.DataSet1.Tables["病名"];
            table.Rows.Clear();

            this.ListFormat();
        }

        public void ListFormat(string sort = "開始日 desc, 連番 desc", string filter = "")
        {
            DataView view = new DataView(this.DataSet1.Tables["病名"]);

            this.DataSource = view;

            view.Sort = sort;
            view.RowFilter = filter;

            this.Columns["患者コード"].Visible = false;

            this.Columns["主病名"].Width = 25;
            this.Columns["主病名"].HeaderText = "主";
            this.Columns["主病名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Columns["告知"].Width = 25;
            this.Columns["告知"].HeaderText = "告";
            this.Columns["告知"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Columns["保険病名"].Width = 25;
            this.Columns["保険病名"].HeaderText = "短";
            this.Columns["保険病名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Columns["開始日"].Width = 70;
            this.Columns["開始日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Columns["連番"].Visible = false;

            this.Columns["病名コード"].Visible = false;

            this.Columns["病名"].Width = 110;

            this.Columns["科コード"].Visible = false;

            this.Columns["科"].Width = 45;
            this.Columns["科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.Columns["ＤＲコード"].Visible = false;

            this.Columns["ＤＲ"].Width = 70;
            this.Columns["ＤＲ"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.Columns["確定日"].Visible = false;

            this.Columns["保険パターン"].Visible = false;

            this.Columns["保険"].Width = 40;
            this.Columns["保険"].HeaderText = "保険";
            this.Columns["保険"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Columns["転帰区分"].Width = 40;
            this.Columns["転帰区分"].HeaderText = "転帰";
            this.Columns["転帰区分"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Columns["転帰日"].Width = 70;
            this.Columns["転帰日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Columns["入外区分コード"].Visible = false;

            this.Columns["入外区分"].Width = 40;
            this.Columns["入外区分"].HeaderText = "入外";
            this.Columns["入外区分"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            for (int i = 1; i <= 15; i++)
            {
                this.Columns["接頭語" + AppString.HanToZen(i.ToString()).PadLeft(2, '０')].Visible = false;
            }

            for (int i = 1; i <= 5; i++)
            {
                this.Columns["接尾語" + AppString.HanToZen(i.ToString()).PadLeft(2, '０')].Visible = false;
            }

            this.Columns["ICDコード"].Width = 50;
            this.Columns["ICDコード"].HeaderText = "ICD";

            this.Columns["ICDコード2"].Width = 50;
            this.Columns["ICDコード2"].HeaderText = "ICD2";

            this.Columns["付加コード"].Width = 50;

            this.Columns["Obj"].Visible = false;

            foreach (DataGridViewRow r in this.Rows)
            {
                if (r.Cells["転帰区分"].Value.ToString().Length > 0)
                {
                    r.DefaultCellStyle.BackColor = Color.LightCyan;
                }
            }
        }

        public Diag GetDiag(int row)
        {
            Diag obj = new Diag();

            if (row < 0)
            {
                return obj;
            }

            if (row >= this.RowCount)
            {
                return obj;
            }

            obj = (Diag)this.Rows[row].Cells["Obj"].Value; 

            return obj;
        }
    }
}
