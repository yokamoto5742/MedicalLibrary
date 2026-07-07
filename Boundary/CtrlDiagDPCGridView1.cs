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
    public class CtrlDiagDPCGridView1 : DataGridView
    {
        public DataSet DataSet1 = new DataSet();

        /// <summary>
        /// 留意病名にチェックがついたかどうか
        /// </summary>
        public bool ICDNotice = false;

        public CtrlDiagDPCGridView1()
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

            table.Columns.Add("主傷病", typeof(bool));
            table.Columns.Add("契機傷病", typeof(bool));
            table.Columns.Add("資源1", typeof(bool));
            table.Columns.Add("資源2", typeof(bool));
            table.Columns.Add("併存症", typeof(bool));
            table.Columns.Add("入院後発症", typeof(bool));

            table.Columns.Add("ICDコード");
            table.Columns.Add("ICDコード2");
            table.Columns.Add("付加コード");

            table.Columns.Add("留意病名1");
            table.Columns.Add("留意病名2");
            table.Columns.Add("Obj", typeof(Diag));

            this.CellContentClick += new DataGridViewCellEventHandler(CtrlDiagDPCGridView1_CellContentClick);
        }

        void CtrlDiagDPCGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            DataGridViewCell cell = this.Rows[e.RowIndex].Cells[e.ColumnIndex];
            DataGridViewColumn col = this.Columns[e.ColumnIndex];
            DataGridViewRow row = this.Rows[e.RowIndex];

            // 留意病名かどうか
            bool b = (row.Cells["留意病名1"].Value.ToString().Length > 0 || row.Cells["留意病名2"].Value.ToString().Length > 0);

            if (col.Name.Equals("主傷病"))
            {
                // 今は False（これから True になる）場合
                if (!cell.Value.ToString().Equals("True"))
                {
                    if (b) this.ICDNotice = true;

                    for (int i = 0; i < this.Rows.Count; i++)
                    {
                        if (i == e.RowIndex)
                        {
                            continue;
                        }
                        
                        this.Rows[i].Cells["主傷病"].Value = false;
                    }
                }
            }
            else if (col.Name.Equals("契機傷病"))
            {
                // 今は False（これから True になる）場合
                if (!cell.Value.ToString().Equals("True"))
                {
                    if (b) this.ICDNotice = true;

                    for (int i = 0; i < this.Rows.Count; i++)
                    {
                        if (i == e.RowIndex)
                        {
                            continue;
                        }

                        this.Rows[i].Cells["契機傷病"].Value = false;
                    }
                }
            }
            else if (col.Name.Equals("資源1"))
            {
                // 今は False（これから True になる）場合
                if (!cell.Value.ToString().Equals("True"))
                {
                    if (b) this.ICDNotice = true;

                    for (int i = 0; i < this.Rows.Count; i++)
                    {
                        if (i == e.RowIndex)
                        {
                            continue;
                        }

                        this.Rows[i].Cells["資源1"].Value = false;
                    }
                }

                // 資源2 にチェックが入っていれば外す
//                this.Rows[e.RowIndex].Cells["資源2"].Value = false;
            }
            else if (col.Name.Equals("資源2"))
            {
                // 今は False（これから True になる）場合
                if (!cell.Value.ToString().Equals("True"))
                {
                    if (b) this.ICDNotice = true;

                    for (int i = 0; i < this.Rows.Count; i++)
                    {
                        if (i == e.RowIndex)
                        {
                            continue;
                        }

                        this.Rows[i].Cells["資源2"].Value = false;
                    }
                }

                // 資源1 にチェックが入っていれば外す
//                this.Rows[e.RowIndex].Cells["資源1"].Value = false;
            }
            else if (col.Name.Equals("併存症") || col.Name.Equals("入院後発症"))
            {
                // 今は False（これから True になる）場合
                if (!cell.Value.ToString().Equals("True"))
                {
                    if (b) this.ICDNotice = true;
                }
            }
        }

        public void ListShow(string pt_id, int in_seq, string sort = "開始日 desc, 連番 desc", string filter = "")
        {
            // ICDNotice リセット
            this.ICDNotice = false;

            DataTable table = this.DataSet1.Tables["病名"];
            table.Rows.Clear();

            if (pt_id.Length == 0 || in_seq == 0)
            {
                return;
            }

            PatIn pin = PatIn.GetDataBySEQ(pt_id, in_seq);

            List<Diag> list = Diag.GetList(pt_id);

            // 留意病名を取得する
            List<string> code_list = new List<string>();

            foreach (Diag obj in list)
            {
                if (obj.ICDCode1.Length > 0 && !code_list.Contains(obj.ICDCode1)) code_list.Add(obj.ICDCode1);
                if (obj.ICDCode2.Length > 0 && !code_list.Contains(obj.ICDCode2)) code_list.Add(obj.ICDCode2);
            }

            List<DPCICDNotice> notice_list = DPCICDNotice.GetList(code_list);

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

                // 外来は飛ばす
                if (obj.InOut.Equals("1"))
                {
                    continue;
                }

                // 入院期間に該当しないものは飛ばす
                if ((AppString.IsDate(pin.OutDate) && obj.StartDate.CompareTo(pin.OutDate) > 0) ||
                    (AppString.IsDate(obj.OutcomeDate) && obj.OutcomeDate.CompareTo(pin.InDate) < 0))
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
                r["ICDコード"] = obj.ICDCode1;
                r["ICDコード2"] = obj.ICDCode2;
                r["付加コード"] = obj.PlusCode;
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

                foreach (DPCICDNotice obj2 in notice_list)
                {
                    if (obj2.IsMatch(obj.ICDCode1))
                    {
                        r["留意病名1"] = "1";
                        break;
                    }

                    if (obj2.IsMatch(obj.ICDCode2))
                    {
                        r["留意病名2"] = "1";
                        break;
                    }
                }

                r["Obj"] = obj;

                table.Rows.Add(r);
            }

            List<DiagDPC> list2 = DiagDPC.GetList(pt_id, in_seq);

            foreach (DiagDPC obj in list2)
            {
                foreach (DataRow r in table.Rows)
                {
                    if (r["連番"].ToString().Equals(obj.SEQ.ToString()))
                    {
                        r["主傷病"] = obj.MainFlg;
                        r["契機傷病"] = obj.TriggerFlg;
                        r["資源1"] = obj.ResourceFlg1;
                        r["資源2"] = obj.ResourceFlg2;
                        r["併存症"] = obj.SubFlg;
                        r["入院後発症"] = obj.AfterFlg;
                    }
                }
            }

            this.ListFormat(sort, filter);
        }

        public void ListClear()
        {
            // ICDNotice リセット
            this.ICDNotice = false;

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

            this.Columns["主傷病"].Width = 42;
            this.Columns["主傷病"].HeaderText = "DPC主";
            this.Columns["主傷病"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Columns["契機傷病"].Width = 42;
            this.Columns["契機傷病"].HeaderText = "契機";
            this.Columns["契機傷病"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Columns["資源1"].Width = 42;
            this.Columns["資源1"].HeaderText = "資1";
            this.Columns["資源1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Columns["資源2"].Width = 42;
            this.Columns["資源2"].HeaderText = "資2";
            this.Columns["資源2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Columns["併存症"].Width = 42;
            this.Columns["併存症"].HeaderText = "併存症";
            this.Columns["併存症"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.Columns["併存症"].ToolTipText = "併存症";

            this.Columns["入院後発症"].Width = 42;
            this.Columns["入院後発症"].HeaderText = "入院後発症";
            this.Columns["入院後発症"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.Columns["入院後発症"].ToolTipText = "入院後発症";

            this.Columns["ICDコード"].Width = 50;
            this.Columns["ICDコード"].HeaderText = "ICD";
            this.Columns["ICDコード"].ToolTipText = "赤字は留意病名";

            this.Columns["ICDコード2"].Width = 50;
            this.Columns["ICDコード2"].HeaderText = "ICD2";
            this.Columns["ICDコード2"].ToolTipText = "赤字は留意病名";

            this.Columns["付加コード"].Width = 50;

            this.Columns["留意病名1"].Visible = false;
            this.Columns["留意病名2"].Visible = false;

            this.Columns["Obj"].Visible = false;

            foreach (DataGridViewRow r in this.Rows)
            {
                if (r.Cells["留意病名1"].Value.ToString().Length > 0)
                {
                    r.Cells["ICDコード"].Style.ForeColor = Color.Red;
                }

                if (r.Cells["留意病名2"].Value.ToString().Length > 0)
                {
                    r.Cells["ICDコード2"].Style.ForeColor = Color.Red;
                }

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
