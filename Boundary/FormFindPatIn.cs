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
    public partial class FormFindPatIn : Form
    {
        string PtId = "";
        List<PatIn> List = new List<PatIn>();

        DataSet dSet = new DataSet();

        public FormFindPatIn(string pt_id)
        {
            InitializeComponent();

            this.PtId = pt_id;

            DataTable table = dSet.Tables.Add("入院歴");
            table.Columns.Add("予定");
            table.Columns.Add("患者コード");
            table.Columns.Add("入院日");
            table.Columns.Add("入院区分コード");
            table.Columns.Add("入院区分");
            table.Columns.Add("退院日");
            table.Columns.Add("退院区分コード");
            table.Columns.Add("退院区分");
            table.Columns.Add("病棟コード");
            table.Columns.Add("病棟");
            table.Columns.Add("病室");
            table.Columns.Add("科コード");
            table.Columns.Add("科");
            table.Columns.Add("医師コード");
            table.Columns.Add("医師");
            table.Columns.Add("日数");
            table.Columns.Add("前回退院から");
            table.Columns.Add("DPCコメント");
            table.Columns.Add("Obj", typeof(PatIn));
        }

        private void FormFindPatIn_Load(object sender, EventArgs e)
        {
            this.ListShow();
        }

        public static string FindPatInString(string pt_id)
        {
            string s = "";

            FormFindPatIn f = new FormFindPatIn(pt_id);
            DialogResult r = f.ShowDialog();

            if (r == DialogResult.OK)
            {
                s = f.GetPatInString();
            }

            f.Dispose();

            return s;
        }

        public static PatIn FindPatIn(string pt_id)
        {
            PatIn p = new PatIn();

            FormFindPatIn f = new FormFindPatIn(pt_id);
            DialogResult r = f.ShowDialog();

            if (r == DialogResult.OK)
            {
                p = f.GetPatIn();
            }

            f.Dispose();

            return p;
        }

        string GetPatInString()
        {
            string s = "";

            if (this.ListView1.CurrentRow != null)
            {
                s = this.ListView1.CurrentRow.Cells["入院日"].Value.ToString() + " ～ " +
                    this.ListView1.CurrentRow.Cells["退院日"].Value.ToString();
            }

            return s;
        }

        PatIn GetPatIn()
        {
            PatIn p = new PatIn();

            if (this.ListView1.CurrentRow != null)
            {
                p = (PatIn)this.ListView1.CurrentRow.Cells["Obj"].Value;
            }

            return p;
        }

        void ListClear()
        {
            DataTable table = dSet.Tables["入院歴"];
            table.Rows.Clear();

            this.ListFormat();
        }

        void ListShow()
        {
            DataTable table = dSet.Tables["入院歴"];
            table.Rows.Clear();

            this.List = PatIn.GetHistory(this.PtId);

            // DPCデータ
            List<DPCHeader> dpc_list1 = DPCHeader.GetList(this.PtId);

            // 旧DPCデータ
            List<DPCData> dpc_list2 = DPCData.GetList(this.List.ConvertAll((x) => { return x.Id; }));

            foreach (PatIn obj in List)
            {
                DataRow r = table.NewRow();

                if (obj.Status == PatInStatus.Yet)
                {
                    r["予定"] = "●";
                }

                r["患者コード"] = obj.Id;
                r["入院日"] = obj.InDateStringShort;
                r["入院区分コード"] = obj.InKind;
                r["入院区分"] = obj.InKindString;
                r["退院日"] = obj.OutDateStringShort;
                r["退院区分コード"] = obj.OutKind;
                r["退院区分"] = obj.OutKindString;
                r["病棟コード"] = obj.Ward;
                r["病棟"] = obj.WardName;
                r["病室"] = obj.Room;
                r["科コード"] = obj.Dept;
                r["科"] = obj.DeptName;
                r["医師コード"] = obj.Doctor;
                r["医師"] = Dict.DoctorDict.ContainsKey(obj.Doctor) ? Dict.DoctorDict[obj.Doctor].ShortName.Replace("　", " ").Replace(" ", "") : "";
                r["日数"] = obj.Days;

                if (obj.IntervalDays > 0)
                {
                    if (obj.IntervalDays >= 7)
                    {
                        r["前回退院から"] = obj.IntervalDays / 7 + "週";
                    }

                    r["前回退院から"] += obj.IntervalDays % 7 + "日";
                }

                string dpc = "";

                foreach (DPCHeader header in dpc_list1)
                {
                    // 入院日が異なれば飛ばす
                    if (!header.AdmDate.Equals(obj.InDate)) continue;

                    // コメントが空なら飛ばす
                    if (header.Cont.Length == 0) continue;

                    if (dpc.Length > 0) dpc += Environment.NewLine;

                    dpc += "[" + header.WardName + "] " + header.Cont;
                }

                foreach (DPCData data in dpc_list2)
                {
                    // 入院日が異なれば飛ばす
                    if (!data.AdmDate.Equals(obj.InDate)) continue;

                    // コメントでなければ飛ばす
                    if (!data.Kind.Equals("17") && !data.Kind.Equals("27") && !data.Kind.Equals("37"))
                    {
                        continue;
                    }

                    // コメントが空なら飛ばす
                    if (data.Cont.Length == 0) continue;

                    if (dpc.Length > 0) dpc += Environment.NewLine;

                    switch (data.Kind)
                    {
                        case "17":
                            dpc += "[一般] ";
                            break;

                        case "27":
                            dpc += "[地域] ";
                            break;

                        case "37":
                            dpc += "[一般(2)] ";
                            break;

                        default:
                            break;
                    }

                    dpc += data.Cont;
                }

                r["DPCコメント"] = dpc;

                r["Obj"] = obj;

                table.Rows.Add(r);
            }

            // 入院日の降順でソート
            this.List.Sort((x, y) =>
            {
                return y.InDateInt - x.InDateInt;
            });

            this.ListFormat();
        }

        void ListFormat()
        {
            DataView view = new DataView(this.dSet.Tables["入院歴"]);

            this.ListView1.DataSource = view;

            this.ListView1.Columns["予定"].Width = 35;
            this.ListView1.Columns["予定"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView1.Columns["予定"].DefaultCellStyle.ForeColor = Color.Red;

            this.ListView1.Columns["患者コード"].Visible = false;

            this.ListView1.Columns["入院日"].Width = 55;
            this.ListView1.Columns["入院日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.ListView1.Columns["入院区分コード"].Visible = false;

            this.ListView1.Columns["入院区分"].Width = 35;
            this.ListView1.Columns["入院区分"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.ListView1.Columns["退院日"].Width = 55;
            this.ListView1.Columns["退院日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.ListView1.Columns["退院区分コード"].Visible = false;

            this.ListView1.Columns["退院区分"].Width = 35;
            this.ListView1.Columns["退院区分"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.ListView1.Columns["病棟コード"].Visible = false;

            this.ListView1.Columns["病棟"].Width = 45;
            this.ListView1.Columns["病棟"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.ListView1.Columns["病室"].Width = 30;
            this.ListView1.Columns["病室"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.ListView1.Columns["科コード"].Visible = false;

            this.ListView1.Columns["科"].Width = 60;
            this.ListView1.Columns["科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["医師コード"].Visible = false;

            this.ListView1.Columns["医師"].Width = 60;
            this.ListView1.Columns["医師"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["日数"].Width = 35;
            this.ListView1.Columns["日数"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.ListView1.Columns["前回退院から"].Width = 65;
            this.ListView1.Columns["前回退院から"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["DPCコメント"].Width = 280;
            this.ListView1.Columns["DPCコメント"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["Obj"].Visible = false;

            foreach (DataGridViewRow r in this.ListView1.Rows)
            {
                string code = r.Cells["病棟コード"].Value.ToString();

                if (Dict.WardDict.ContainsKey(code))
                {
                    r.Cells["病棟"].Style.BackColor = Dict.WardDict[code].BackColor;
                }
            }
        }

        private void ListView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
