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
    public partial class FormAddressGroup : Form
    {
        /// <summary>
        /// 現在開いているグループ
        /// </summary>
        AddressGroup Group1 = new AddressGroup();

        DataSet dSet = new DataSet();

        public FormAddressGroup()
        {
            InitializeComponent();

            DataTable table = dSet.Tables.Add("メンバー");
            table.Columns.Add("ID");
            table.Columns.Add("氏名");
            table.Columns.Add("所属");
            table.Columns.Add("科");

            this.ListShow();
        }

        void ListShow()
        {
            this.DataClear();
            this.ListBox1.Items.Clear();

            List<AddressGroup> list = AddressGroup.GetList(LoginUser.Id);

            foreach (AddressGroup obj in list)
            {
                this.ListBox1.Items.Add(obj);
            }
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DataShow();
        }

        void DataClear()
        {
            this.Group1 = new AddressGroup();

            this.NameBox1.Clear();

            DataTable table = dSet.Tables["メンバー"];
            table.Rows.Clear();
        }

        void DataShow()
        {
            this.DataClear();

            if (this.ListBox1.SelectedItem != null)
            {
                this.Group1 = (AddressGroup)(this.ListBox1.SelectedItem);

                this.NameBox1.Text = this.Group1.Name;

                DataTable table = dSet.Tables["メンバー"];

                foreach (Staff obj in AddressGroup.GetMembers(this.Group1.StaffCode, this.Group1.SEQ))
                {
                    DataRow r = table.NewRow();

                    r["ID"] = obj.Code.ToString();
                    r["氏名"] = obj.Name;
                    r["所属"] = obj.SectionShortName;
                    r["科"] = obj.DeptShortName;

                    table.Rows.Add(r);
                }

                DataView view = new DataView(table);

                this.ListView1.DataSource = view;

                this.ListView1.Columns["ID"].Width = 40;
                this.ListView1.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                this.ListView1.Columns["氏名"].Width = 80;
                this.ListView1.Columns["氏名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                this.ListView1.Columns["所属"].Width = 50;
                this.ListView1.Columns["所属"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                this.ListView1.Columns["科"].Width = 60;
                this.ListView1.Columns["科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private void SaveButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("登録しますか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            this.Group1.Name = this.NameBox1.Text;

            List<Staff> list = new List<Staff>();

            foreach (DataRow r in dSet.Tables["メンバー"].Rows)
            {
                Staff obj = new Staff();

                int.TryParse(r["ID"].ToString(), out obj.Code);

                if (obj.Code > 0)
                {
                    list.Add(obj);
                }
            }

            if (this.Group1.SEQ > 0)
            {
                this.Group1.Update(list);
            }
            else
            {
                this.Group1.Insert(list);
            }

            this.ListShow();
        }

        private void ClearButton1_Click(object sender, EventArgs e)
        {
            this.DataClear();
        }

        private void DeleteButton1_Click(object sender, EventArgs e)
        {
            if (this.Group1.SEQ == 0)
            {
                MessageBox.Show("削除対象のグループがありません");
                return;
            }

            if (MessageBox.Show("グループを削除します。よろしいですか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            this.Group1.Delete();
            this.ListShow();
        }

        private void AddMenuItem1_Click(object sender, EventArgs e)
        {
            List<Staff> list = FormFindStaff.FindStaff();

            DataTable table = dSet.Tables["メンバー"];

            foreach (Staff obj in list)
            {
                bool exist_flg = false;

                foreach (DataRow r in table.Rows)
                {
                    if (obj.Code.ToString().Equals(r["ID"].ToString()))
                    {
                        exist_flg = true;
                        break;
                    }
                }

                if (!exist_flg)
                {
                    DataRow r = table.NewRow();

                    r["ID"] = obj.Code.ToString();
                    r["氏名"] = obj.Name;
                    r["所属"] = obj.SectionShortName;
                    r["科"] = obj.DeptShortName;

                    table.Rows.Add(r);
                }
            }
        }

        private void DeleteMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.ListView1.CurrentRow == null)
            {
                return;
            }

            this.ListView1.Rows.RemoveAt(this.ListView1.CurrentRow.Index);
        }
    }
}
