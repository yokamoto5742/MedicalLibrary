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
    public partial class FormOpeRecord : StdForm1
    {
        DataSet DSet = new DataSet();

        public FormOpeRecord()
        {
            InitializeComponent();
        }

        private void FormOpeRecord_Load(object sender, EventArgs e)
        {
            // デフォルトで３か月前から今日まで
            this.DatePicker1.Value = DateTime.Now.AddMonths(-3);

            DataTable table = DSet.Tables.Add("OpeRecord");

            table.Columns.Add("手術日");
            table.Columns.Add("ID");
            table.Columns.Add("氏名");
            table.Columns.Add("性別");
            table.Columns.Add("生年月日");
            table.Columns.Add("年齢");
            table.Columns.Add("科");
            table.Columns.Add("医師");
            table.Columns.Add("診断");
            table.Columns.Add("術式");
            table.Columns.Add("Obj", typeof(OpeRecord));

            this.MainDeptBox.Init();
            this.MainDoctorBox1.Init();
            this.MainDoctorBox2.Init();

            this.SubDeptBox.Init();
            this.SubDoctorBox1.Init();

            this.OpeDoctorBox1.Init();
            this.OpeDoctorBox2.Init();
            this.OpeDoctorBox3.Init();
        }

        private void FormOpeRecord_Shown(object sender, EventArgs e)
        {
            if (this.Pat.Id.Length > 0)
            {
                this.RadioButton2.Checked = true;
            }
            else
            {
                this.RadioButton1.Checked = true;
            }

            this.ListShow();
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);
            this.RadioButton2.Checked = true;

            this.ListShow();
        }

        void ListShow()
        {
            if (!RadioButton1.Checked && !RadioButton2.Checked)
            {
                MessageBox.Show("検索条件を選んでください");
                return;
            }

            if (!DSet.Tables.Contains("OpeRecord"))
            {
                return;
            }

            List<OpeRecord> list;

            if (RadioButton1.Checked)
            {
                list = OpeRecord.GetListByDates(DatePicker1.Value.ToString("yyyyMMdd"), DatePicker2.Value.ToString("yyyyMMdd"));
            }
            else
            {
                if (this.Pat.Id.Length == 0)
                {
                    MessageBox.Show("患者を指定してください");
                    return;
                }

                list = OpeRecord.GetListByPat(this.Pat.Id);
            }

            DataTable table = DSet.Tables["OpeRecord"];
            table.Rows.Clear();

            foreach (OpeRecord obj in list)
            {
                // 削除されたものは飛ばす
                if (obj.DeleteFlg.Equals("1"))
                {
                    continue;
                }

                DataRow r = table.NewRow();

                r["手術日"] = DateTimeAgent.DateFormat(obj.OpeDate, DateTimeAgent.DateFormatKind.LONG);
                r["ID"] = obj.PtId;
                r["氏名"] = obj.Pat.Name;
                r["性別"] = obj.Pat.Sex;
                r["生年月日"] = obj.Pat.BirthString;
                r["年齢"] = obj.Pat.AgeCalc(obj.OpeDate);
                r["科"] = obj.MainDeptName;
                r["医師"] = obj.OpeDoctor;
                r["診断"] = obj.DiagName;
                r["術式"] = obj.OpeName;
                r["Obj"] = obj;

                table.Rows.Add(r);
            }

            this.ListFormat();
        }

        void ListFormat()
        {
            if (!DSet.Tables.Contains("OpeRecord"))
            {
                return;
            }

            DataView view = new DataView(DSet.Tables["OpeRecord"]);

            this.ListView.DataSource = view;

            this.ListView.Columns["手術日"].Width = 75;
            this.ListView.Columns["手術日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.ListView.Columns["ID"].Width = 60;
            this.ListView.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.ListView.Columns["氏名"].Width = 75;
            this.ListView.Columns["氏名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView.Columns["性別"].Visible = false;

            this.ListView.Columns["生年月日"].Visible = false;
            this.ListView.Columns["生年月日"].Width = 75;
            this.ListView.Columns["生年月日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.ListView.Columns["年齢"].Width = 30;
            this.ListView.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.ListView.Columns["科"].Width = 65;
            this.ListView.Columns["科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            if (this.RadioButton1.Checked)
            {
                this.ListView.Columns["ID"].Visible = true;
                this.ListView.Columns["氏名"].Visible = true;

                this.ListView.Columns["医師"].Width = 85;
                this.ListView.Columns["医師"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                this.ListView.Columns["診断"].Width = 140;
                this.ListView.Columns["診断"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                this.ListView.Columns["術式"].Width = 140;
                this.ListView.Columns["術式"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
            else if (this.RadioButton2.Checked)
            {
                this.ListView.Columns["ID"].Visible = false;
                this.ListView.Columns["氏名"].Visible = false;

                this.ListView.Columns["医師"].Width = 100;
                this.ListView.Columns["医師"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                this.ListView.Columns["診断"].Width = 200;
                this.ListView.Columns["診断"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                this.ListView.Columns["術式"].Width = 200;
                this.ListView.Columns["術式"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            this.ListView.Columns["Obj"].Visible = false;

            AppDataGridView.SexColor(this.ListView);
        }

        private void ShowListButton_Click(object sender, EventArgs e)
        {
            this.ListShow();
        }

        void DataClear()
        {
            this.OpeDatePicker.Value = DateTime.Now;

            this.MainDeptBox.SetDept("");
            this.MainDoctorBox1.SetDoctor("");
            this.MainDoctorBox2.SetDoctor("");

            this.SubDeptBox.SetDept("");
            this.SubDoctorBox1.SetDoctor("");

            this.OpeDoctorBox1.SetDoctor("");
            this.OpeDoctorBox2.SetDoctor("");

            this.HeightBox.Clear();
            this.WeightBox.Clear();
            this.BloodTypeBox.Clear();

            this.AnesBox.Clear();

            this.OpeTimeBox.Clear();
            this.AnesTimeBox.Clear();

            this.DiagNameBox.Clear();
            this.OpeNameBox.Clear();

            this.BloodOutBox.Clear();
            this.BloodInBox.Clear();

            this.BeforeCommentBox.Clear();
            this.OpeCommentBox.Clear();
            this.PathologyBox.Clear();
            this.NoteBox.Clear();
        }

        void DataShow(OpeRecord obj)
        {
            this.DataClear();

            try
            {
                this.OpeDatePicker.Value = DateTime.Parse(DateTimeAgent.DateFormat(obj.OpeDate, DateTimeAgent.DateFormatKind.LONG));

                this.MainDeptBox.SetDept(obj.MainDept);
                this.MainDoctorBox1.SetDoctor(obj.MainDoctor1);
                this.MainDoctorBox2.SetDoctor(obj.MainDoctor2);

                this.SubDeptBox.SetDept(obj.SubDept);
                this.SubDoctorBox1.SetDoctor(obj.SubDoctor1);

                this.OpeDoctorBox1.SetDoctor(obj.OpeDoctor1);
                this.OpeDoctorBox2.SetDoctor(obj.OpeDoctor2);
                this.OpeDoctorBox3.SetDoctor(obj.OpeDoctor3);

                this.HeightBox.Text = obj.Height.ToString();
                this.WeightBox.Text = obj.Weight.ToString();
                this.BloodTypeBox.Text = obj.BloodTypeName;

                this.AnesBox.Text = obj.Anes;

                this.OpeTimeBox.Text = obj.OpeTime.ToString();
                this.AnesTimeBox.Text = obj.AnesTime.ToString();

                this.DiagNameBox.Text = obj.DiagName;
                this.OpeNameBox.Text = obj.OpeName;

                this.BloodOutBox.Text = obj.BloodOut.ToString();
                this.BloodInBox.Text = obj.BloodIn.ToString();

                this.BeforeCommentBox.Text = obj.BeforeComment;
                this.OpeCommentBox.Text = obj.OpeComment;
                this.PathologyBox.Text = obj.Pathology;
                this.NoteBox.Text = obj.Note;
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex, true);
                this.DataClear();
            }
        }

        private void ListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            OpeRecord obj = (OpeRecord)this.ListView.Rows[e.RowIndex].Cells["Obj"].Value;
            this.DataShow(obj);
        }
    }
}
