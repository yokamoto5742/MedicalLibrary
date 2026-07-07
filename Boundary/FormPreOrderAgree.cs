using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Entity;

namespace MedicalLibrary.Boundary
{
    public partial class FormPreOrderAgree : StdForm1
    {
        DataSet dSet = new DataSet();

        public FormPreOrderAgree()
        {
            InitializeComponent();

            DataTable table = dSet.Tables.Add("Agree");
            table.Columns.Add("オーダー番号");
            table.Columns.Add("病棟");
            table.Columns.Add("病室");
            table.Columns.Add("患者コード");
            table.Columns.Add("氏名");
            table.Columns.Add("性別");
            table.Columns.Add("年齢");
            table.Columns.Add("科");
            table.Columns.Add("主治医");
            table.Columns.Add("内容");
            table.Columns.Add("実施日時");
            table.Columns.Add("実施者");
            table.Columns.Add("承認", typeof(bool));

            this.DatePicker1.MinDate = DateTime.Now.AddMonths(-1);
        }

        private void FormPreOrderAgree_Load(object sender, EventArgs e)
        {
            this.DatePicker1.Value = DateTime.Now.AddDays(-3);

            this.ListShow();
        }

        void ListShow()
        {
            DataTable table = dSet.Tables["Agree"];
            table.Clear();

            List<PreOrderAgree> list = PreOrderAgree.GetYetList(this.DatePicker1.Value.ToString("yyyyMMdd"));

            foreach (PreOrderAgree obj in list)
            {
                DataRow r = table.NewRow();

                r["オーダー番号"] = obj.OrderId;
                r["病棟"] = obj.WardName;
                r["病室"] = obj.Room;
                r["患者コード"] = obj.Pat.Id;
                r["氏名"] = obj.Pat.Name;
                r["性別"] = obj.Pat.SexNameShort;
                r["年齢"] = obj.Pat.AgeCalc(this.DatePicker1.Value.ToString("yyyyMMdd"));
                r["科"] = obj.DeptName;
                r["主治医"] = obj.DoctorName;
                r["内容"] = obj.SOAP;
                r["実施日時"] = obj.ExecDateTime;
                r["実施者"] = obj.StaffName;

                table.Rows.Add(r);
            }

            this.ListFormat();
        }

        void ListFormat()
        {
            DataView view = new DataView(dSet.Tables["Agree"]);

            AgreeListView1.DataSource = view;

            AgreeListView1.Columns["オーダー番号"].Visible = false;

            AgreeListView1.Columns["病棟"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            AgreeListView1.Columns["病棟"].Width = 50;

            AgreeListView1.Columns["病室"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            AgreeListView1.Columns["病室"].Width = 40;

            AgreeListView1.Columns["患者コード"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            AgreeListView1.Columns["患者コード"].Width = 50;

            AgreeListView1.Columns["氏名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            AgreeListView1.Columns["氏名"].Width = 90;

            AgreeListView1.Columns["性別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            AgreeListView1.Columns["性別"].Width = 30;

            AgreeListView1.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            AgreeListView1.Columns["年齢"].Width = 30;

            AgreeListView1.Columns["科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            AgreeListView1.Columns["科"].Width = 50;

            AgreeListView1.Columns["主治医"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            AgreeListView1.Columns["主治医"].Width = 75;

            AgreeListView1.Columns["内容"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            AgreeListView1.Columns["内容"].Width = 300;

            AgreeListView1.Columns["実施日時"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            AgreeListView1.Columns["実施日時"].Width = 100;

            AgreeListView1.Columns["実施者"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            AgreeListView1.Columns["実施者"].Width = 75;

            AgreeListView1.Columns["承認"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            AgreeListView1.Columns["承認"].Width = 30;

            foreach (DataGridViewRow r in AgreeListView1.Rows)
            {
                if (r.Cells["性別"].Value.ToString().Equals("女"))
                {
                    r.Cells["患者コード"].Style.ForeColor = Color.Red;
                    r.Cells["氏名"].Style.ForeColor = Color.Red;
                    r.Cells["性別"].Style.ForeColor = Color.Red;
                    r.Cells["年齢"].Style.ForeColor = Color.Red;
                }
            }
        }

        private void DatePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.ListShow();
        }
    }
}
