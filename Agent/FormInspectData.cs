using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Boundary;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class FormInspectData : StdForm1
    {
        bool _ReadOnly = false;

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
                }
                else
                {
                }
            }
        }

        DataSet DSet = new DataSet();

        /// <summary>
        /// 現在表示しているデータ
        /// </summary>
        InspectDataGroup _InspectDataGroup = new InspectDataGroup();

        ContextMenuStrip _ContextMenuStrip = new ContextMenuStrip();


        public FormInspectData(bool read_only = true)
        {
            InitializeComponent();

            this.ReadOnly = read_only;
        }

        private void FormInspectData_Load(object sender, EventArgs e)
        {
            InspectSettings.Init();

            LibSettings.Init();

            ToolStripItem Item1 = new ToolStripMenuItem();
            Item1.Text = "削除";
            Item1.Click += new EventHandler(Item1_Click);
            this._ContextMenuStrip.Items.Add(Item1);

            this.ListView.ContextMenuStrip = this._ContextMenuStrip;
            this._ContextMenuStrip.Opening += new CancelEventHandler(_ContextMenuStrip_Opening);

            DataTable table = DSet.Tables.Add("InspectData");

            table.Columns.Add("検査日");
            table.Columns.Add("内容");
            table.Columns.Add("登録者");
            table.Columns.Add("Obj", typeof(InspectDataGroup));

            int h = 10;

            foreach (InspectKind k in InspectSettings.Current.InspectKindList)
            {
                /*
                int kid = int.Parse(k.Id);

                // 1～10, 16 のみを対象とする
                if (kid > 10 && !kid.Equals(16))
                {
                    continue;
                }
                */
                if (!k.OutFlg.Equals("1")) continue;

                Label lb = new Label();
                lb.Text = k.Name;
                lb.AutoSize = true;
                lb.Location = new Point(10, h);

                this.InspectPanel.Controls.Add(lb);

                ComboBox cb = new ComboBox();
                cb.Name = "InspectKind_" + k.Id;
                cb.Location = new Point(120, h - 4);
                cb.Width = 300;

                foreach (InspectOrder io in k.OrderList)
                {
                    if (io.Name.Length > 0 && !cb.Items.Contains(io.Name))
                    {
                        cb.Items.Add(io.Name);
                    }
                }

                this.InspectPanel.Controls.Add(cb);

                h += 25;
            }

            this.ListShow();
        }

        void _ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (this.ListView.CurrentRow == null)
            {
                this._ContextMenuStrip.Items[0].Enabled = false;
            }
        }

        void Item1_Click(object sender, EventArgs e)
        {
            if (this.ListView.CurrentRow == null)
            {
                return;
            }

            InspectDataGroup g = (InspectDataGroup)this.ListView.Rows[this.ListView.CurrentRow.Index].Cells["Obj"].Value;

            if (MessageBox.Show(DateTimeAgent.DateFormat(g.InspectDate, DateTimeAgent.DateFormatKind.LONG) + " のデータを削除しますか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != DialogResult.OK)
            {
                return;
            }

            InspectDataGroup.Delete(g.PtId, g.InspectDate);

            this.ListShow();
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            this.ListShow();
        }

        void ListShow()
        {
            if (!DSet.Tables.Contains("InspectData"))
            {
                return;
            }

            DataTable table = DSet.Tables["InspectData"];
            table.Rows.Clear();

            string date1 = "20010101";
            string date2 = "20991231";

            List<InspectDataGroup> list = InspectDataGroup.GetList(InspectData.GetOutList(this.Pat.Id, date1, date2));

            foreach (InspectDataGroup obj in list)
            {
                DataRow r = table.NewRow();

                r["検査日"] = DateTimeAgent.DateFormat(obj.InspectDate, DateTimeAgent.DateFormatKind.LONG);
                r["内容"] = obj.Cont;
                r["登録者"] = obj.StaffName;
                r["Obj"] = obj;

                table.Rows.Add(r);
            }

            this.ListFormat();

            if (list.Count > 0)
            {
                this.HelpPanel.Visible = true;
            }
        }

        void ListFormat()
        {
            if (!DSet.Tables.Contains("InspectData"))
            {
                return;
            }

            DataTable table = DSet.Tables["InspectData"];
            DataView view = new DataView(table);
            this.ListView.DataSource = view;

            this.ListView.Columns["検査日"].Width = 80;
            this.ListView.Columns["検査日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ListView.Columns["検査日"].DefaultCellStyle.Font = AppFont.F9.Ft;

            this.ListView.Columns["内容"].Width = 270;
            this.ListView.Columns["内容"].DefaultCellStyle.Font = AppFont.F9.Ft;

            this.ListView.Columns["登録者"].Width = 90;
            this.ListView.Columns["登録者"].DefaultCellStyle.Font = AppFont.F9.Ft;

            this.ListView.Columns["Obj"].Visible = false;

            // 非選択状態にしておく
            ListView.ClearSelection();
        }

        void DataShow(InspectDataGroup obj)
        {
            this.DataClear();

            this._InspectDataGroup = obj;

            this.DatePicker.Value = DateTime.Parse(DateTimeAgent.DateFormat(obj.InspectDate, DateTimeAgent.DateFormatKind.LONG));

            foreach (InspectData io in obj.InspectDataList)
            {
                if (this.InspectPanel.Controls.ContainsKey("InspectKind_" + io.Kind))
                {
                    Control c = this.InspectPanel.Controls["InspectKind_" + io.Kind];
                    c.Text = io.Cont;
                }
            }
        }

        void DataClear()
        {
            this._InspectDataGroup = new InspectDataGroup();

            this.DatePicker.Value = DateTime.Now;

            foreach (Control c in this.InspectPanel.Controls)
            {
                if (c is ComboBox)
                {
                    c.Text = "";
                }
            }
        }

        private void ListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            this.DataShow((InspectDataGroup)this.ListView.Rows[e.RowIndex].Cells["Obj"].Value);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("クリアしますか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            this.DataClear();
        }

        private void RegButton_Click(object sender, EventArgs e)
        {
            List<InspectData> list = new List<InspectData>();

            // InspectData データを取得する
            foreach (Control c in this.InspectPanel.Controls)
            {
                if (!(c is ComboBox))
                {
                    continue;
                }

                // 入力されていなければ飛ばす
                if (c.Text.Length == 0)
                {
                    continue;
                }

                // 名称が不適切なら飛ばす
                if (!c.Name.Contains('_'))
                {
                    continue;
                }

                InspectData obj = new InspectData();
                obj.PtId = this.Pat.Id;
                obj.InspectDate = DatePicker.Value.ToString("yyyyMMdd");
                obj.Kind = c.Name.Split('_')[1];
                obj.Cont = c.Text;

                list.Add(obj);
            }

            if (list.Count == 0)
            {
                MessageBox.Show("登録するデータがありません");
                return;
            }

            if (DatePicker.Value.ToString("yyyyMMdd").CompareTo(DateTime.Now.ToString("yyyyMMdd")) > 0)
            {
                if (MessageBox.Show("検査日が未来の日付です。登録しますか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                {
                    return;
                }
            }
            else
            {
                if (MessageBox.Show("登録しますか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
            }

            // 既存のデータを削除する
            InspectDataGroup.Delete(this.Pat.Id, this.DatePicker.Value.ToString("yyyyMMdd"));

            // 個々の InspectData データを登録する
            foreach (InspectData obj in list)
            {
                obj.Save();
            }

            this.DataClear();
            this.ListShow();
        }

        private void HelpCloseLabel_Click(object sender, EventArgs e)
        {
            this.HelpPanel.Visible = false;
        }
    }
}
