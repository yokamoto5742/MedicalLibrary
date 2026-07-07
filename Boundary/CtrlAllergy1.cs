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
    public partial class CtrlAllergy1 : UserControl
    {
        bool _ReadOnly = true;

        public bool ReadOnly
        {
            get
            {
                return this._ReadOnly;
            }
            set
            {
                this._ReadOnly = value;

                if (value)
                {
                    this.SaveButton1.Visible = false;
                    this.ClearButton1.Visible = false;
                    this.DeleteButton1.Visible = false;
                }
                else
                {
                    this.SaveButton1.Visible = true;
                    this.ClearButton1.Visible = true;
                    this.DeleteButton1.Visible = true;
                }
            }
        }

        PatBase Pat = new PatBase();

        DataSet dSet = new DataSet();

        public CtrlAllergy1()
        {
            InitializeComponent();

            DataTable table = dSet.Tables.Add("禁忌");
            table.Columns.Add("分類コード");
            table.Columns.Add("分類");
            table.Columns.Add("連番");
            table.Columns.Add("項目");
            table.Columns.Add("コメント");
            table.Columns.Add("登録者コード");
            table.Columns.Add("登録者");
            table.Columns.Add("登録日時");
        }

        private void CtrlAllergy1_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 最初に実行すべきメソッド
        /// </summary>
        public void Init()
        {
            this.GroupBox1.Items.Clear();
            this.GroupBox1.Items.Add("");

            foreach (AllergyGroupMaster am in AllergyGroupMaster.ListAll)
            {
                this.GroupBox1.Items.Add(am);
            }
        }

        public void PatSet(PatBase p)
        {
            this.Pat = p;

            this.ListShow();
        }

        void ListClear()
        {
            DataTable table = dSet.Tables["禁忌"];
            table.Rows.Clear();

            this.DataClear();

            this.ListFormat();
        }

        void ListShow()
        {
            if (this.Pat.Id.Length == 0)
            {
                return;
            }

            DataTable table = dSet.Tables["禁忌"];
            table.Rows.Clear();

            this.DataClear();

            List<AllergyData> list = AllergyData.GetList(this.Pat.Id);

            foreach (AllergyData obj in list)
            {
                DataRow r = table.NewRow();

                r["分類コード"] = obj.GroupCode;
                r["分類"] = obj.GroupName;
                r["連番"] = obj.SEQ;
                r["項目"] = obj.Name;
                r["コメント"] = obj.Cont;
                r["登録者コード"] = obj.UpStaffCode;
                r["登録者"] = obj.UpStaffName;
                r["登録日時"] = obj.UpDateTime;

                table.Rows.Add(r);
            }

            this.ListFormat();
        }

        void ListFormat()
        {
            DataView view = new DataView(dSet.Tables["禁忌"]);

            this.ListView1.DataSource = view;

            this.ListView1.Columns["分類コード"].Visible = false;

            this.ListView1.Columns["分類"].Width = 70;
            this.ListView1.Columns["分類"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["連番"].Visible = false;

            this.ListView1.Columns["項目"].Width = 80;
            this.ListView1.Columns["項目"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["コメント"].Width = 200;
            this.ListView1.Columns["コメント"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["登録者コード"].Visible = false;

            this.ListView1.Columns["登録者"].Width = 80;
            this.ListView1.Columns["登録者"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.ListView1.Columns["登録日時"].Width = 100;
            this.ListView1.Columns["登録日時"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        void DataClear()
        {
            this.GroupBox1.Text = "";
            this.MasterBox1.Items.Clear();
            this.ContBox1.Clear();
            this.StaffLabel1.Text = "";
            this.DateTimeLabel1.Text = "";
        }

        private void ListView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            this.DataClear();

            DataGridViewRow r = ListView1.Rows[e.RowIndex];

            this.GroupBox1.Text = r.Cells["分類"].Value.ToString();
            this.MasterBox1.Text = r.Cells["項目"].Value.ToString();
            this.ContBox1.Text = r.Cells["コメント"].Value.ToString();
            this.StaffLabel1.Text = r.Cells["登録者"].Value.ToString();
            this.DateTimeLabel1.Text = r.Cells["登録日時"].Value.ToString();
        }

        private void GroupBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.MasterBox1.Items.Clear();
            this.MasterBox1.Items.Add("");

            if (this.GroupBox1.Text.Length == 0)
            {
                return;
            }

            AllergyGroupMaster am = (AllergyGroupMaster)this.GroupBox1.SelectedItem;

            foreach (AllergyMaster m in am.MasterList)
            {
                this.MasterBox1.Items.Add(m);
            }
        }

        private void SaveButton1_Click(object sender, EventArgs e)
        {
            if (this.Pat.Id.Length == 0)
            {
                MessageBox.Show("患者コードが指定されていません");
                return;
            }

            if (this.GroupBox1.SelectedItem == null || this.GroupBox1.Text.Length == 0)
            {
                MessageBox.Show("分類が選択されていません");
                return;
            }

            if (this.MasterBox1.SelectedItem == null || this.MasterBox1.Text.Length == 0)
            {
                MessageBox.Show("項目が選択されていません");
                return;
            }

            if (MessageBox.Show("登録しますか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            AllergyData obj = new AllergyData();

            obj.PtId = this.Pat.Id;
            obj.GroupCode = ((AllergyGroupMaster)this.GroupBox1.SelectedItem).Code;
            obj.SEQ = ((AllergyMaster)this.MasterBox1.SelectedItem).SEQ;
            obj.Name = ((AllergyMaster)this.MasterBox1.SelectedItem).Name;
            obj.Cont = this.ContBox1.Text;

            obj.Save();

            this.ListShow();
        }

        private void ClearButton1_Click(object sender, EventArgs e)
        {
            this.DataClear();
        }

        private void DeleteButton1_Click(object sender, EventArgs e)
        {
            if (this.Pat.Id.Length == 0)
            {
                MessageBox.Show("患者コードが指定されていません");
                return;
            }

            if (this.GroupBox1.SelectedItem == null || this.GroupBox1.Text.Length == 0)
            {
                MessageBox.Show("分類が選択されていません");
                return;
            }

            if (this.MasterBox1.SelectedItem == null || this.MasterBox1.Text.Length == 0)
            {
                MessageBox.Show("項目が選択されていません");
                return;
            }

            if (MessageBox.Show("削除しますか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            AllergyData obj = new AllergyData();

            obj.PtId = this.Pat.Id;
            obj.GroupCode = ((AllergyGroupMaster)this.GroupBox1.SelectedItem).Code;
            obj.SEQ = ((AllergyMaster)this.MasterBox1.SelectedItem).SEQ;

            obj.Delete();

            this.ListShow();
        }
    }
}
