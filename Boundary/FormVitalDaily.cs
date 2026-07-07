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
    public partial class FormVitalDaily : StdForm1
    {
        string Code = "";

        DataSet dSet = new DataSet();

        public FormVitalDaily(string pt_id, string date_string, string code = "1")
        {
            InitializeComponent();

            this.Pat.Id = pt_id;
            this.Code = code;

            DateTime dt = DateTime.Now;

            if (DateTime.TryParse(date_string, out dt))
            {
                this.DatePicker1.Value = dt;
            }
        }

        private void FormVitalDaily_Load(object sender, EventArgs e)
        {
            this.TabControl1.TabPages.Clear();

            foreach (string k in VitalMaster.Dict.Keys)
            {
                VitalMaster m = VitalMaster.Dict[k];


                TabPage page = new TabPage();
                page.Name = "TabPage" + m.Code;
                page.Text = m.Name;

                DataGridView view = new DataGridView();
                view.Tag = m.Code;
                view.Name = "ListView";
                view.AllowUserToAddRows = false;
                view.AllowUserToDeleteRows = false;
                view.AllowUserToOrderColumns = false;
                view.Location = new Point(5, 5);
                view.Size = new Size(this.TabControl1.Width - 20, 110);
                view.RowHeadersVisible = false;
                view.CellClick += new DataGridViewCellEventHandler(ListView_CellClick);

                page.Controls.Add(view);


                DataTable table = dSet.Tables.Add("Table" + m.Code);
                table.Columns.Add("連番");
                table.Columns.Add("時刻");
                table.Columns.Add("入力者");
                table.Columns.Add("測定値１");
                table.Columns.Add("単位１");
                table.Columns.Add("測定値２");
                table.Columns.Add("単位２");


                Label lbn = new Label();
                lbn.Name = "SEQLabel";
                lbn.Location = new Point(160, 122);
                lbn.Size = new Size(40, 16);

                page.Controls.Add(lbn);


                Label lp = new Label();
                lp.Location = new Point(5, 122);
                lp.AutoSize = true;
                lp.Text = "時刻";

                page.Controls.Add(lp);


                DateTimePicker tp = new DateTimePicker();
                tp.Name = "TimePicker";
                tp.Location = new Point(80, 120);
                tp.Size = new Size(60, 20);
                tp.ShowUpDown = true;
                tp.Format = DateTimePickerFormat.Custom;
                tp.CustomFormat = "HH:mm";
                tp.Value = DateTime.Now;

                page.Controls.Add(tp);


                Label lp1 = new Label();
                lp1.Location = new Point(5, 147);
                lp1.AutoSize = true;
                lp1.Text = m.Name1;

                page.Controls.Add(lp1);


                TextBox tb1 = new TextBox();
                tb1.Tag = m;
                tb1.Name = "TextBox_" + m.Code + "_1";
                tb1.Location = new Point(80, 145);
                tb1.Size = new Size(80, 20);
                tb1.MaxLength = 15;

                if (m.DataType1 == VitalMaster.DataType.Int)
                {
                    tb1.ImeMode = ImeMode.Disable;
                    tb1.Leave += new EventHandler(TextBox1_ValueCheck);
                }
                else if (m.DataType1 == VitalMaster.DataType.Float)
                {
                    tb1.ImeMode = ImeMode.Disable;
                    tb1.Leave += new EventHandler(TextBox1_ValueCheck);
                }
                else if (m.DataType1 == VitalMaster.DataType.Alpha)
                {
                    tb1.ImeMode = ImeMode.Disable;
                }
                else if (m.DataType1 == VitalMaster.DataType.Hiragana)
                {
                    tb1.ImeMode = ImeMode.Hiragana;
                }

                page.Controls.Add(tb1);


                if (m.Unit1.Length > 0)
                {
                    Label lpu1 = new Label();
                    lpu1.Location = new Point(165, 147);
                    lpu1.AutoSize = true;

                    lpu1.Text = m.Unit1;

                    page.Controls.Add(lpu1);
                }


                if (m.Name2.Length > 0)
                {
                    Label lp2 = new Label();
                    lp2.Location = new Point(5, 172);
                    lp2.AutoSize = true;
                    lp2.Text = m.Name2;

                    page.Controls.Add(lp2);


                    TextBox tb2 = new TextBox();
                    tb2.Tag = m;
                    tb2.Name = "TextBox_" + m.Code + "_2";
                    tb2.Location = new Point(80, 170);
                    tb2.Size = new Size(80, 20);
                    tb2.MaxLength = 15;

                    if (m.DataType1 == VitalMaster.DataType.Int)
                    {
                        tb2.ImeMode = ImeMode.Disable;
                        tb2.Leave += new EventHandler(TextBox2_ValueCheck);
                    }
                    else if (m.DataType1 == VitalMaster.DataType.Float)
                    {
                        tb2.ImeMode = ImeMode.Disable;
                        tb2.Leave += new EventHandler(TextBox2_ValueCheck);
                    }
                    else if (m.DataType1 == VitalMaster.DataType.Alpha)
                    {
                        tb2.ImeMode = ImeMode.Disable;
                    }
                    else if (m.DataType1 == VitalMaster.DataType.Hiragana)
                    {
                        tb2.ImeMode = ImeMode.Hiragana;
                    }

                    page.Controls.Add(tb2);


                    if (m.Unit2.Length > 0)
                    {
                        Label lpu2 = new Label();
                        lpu2.Location = new Point(165, 172);
                        lpu2.AutoSize = true;

                        lpu2.Text = m.Unit2;

                        page.Controls.Add(lpu2);
                    }
                }


                Label lbs = new Label();
                lbs.Name = "StaffLabel";
                lbs.AutoSize = false;
                lbs.Location = new Point(240, 122);
                lbs.Size = new Size(100, 16);
                lbs.BackColor = Color.LightYellow;
                lbs.TextAlign = ContentAlignment.MiddleCenter;

                page.Controls.Add(lbs);
                

                Button b1 = new Button();
                b1.Tag = m.Code;
                b1.Text = "登録";
                b1.Location = new Point(240, 145);
                b1.Size = new Size(75, 22);
                b1.Click += new EventHandler(SaveButton_Click);

                page.Controls.Add(b1);


                Button b2 = new Button();
                b2.Tag = m.Code;
                b2.Text = "クリア";
                b2.Location = new Point(320, 145);
                b2.Size = new Size(50, 22);
                b2.Click += new EventHandler(ClearButton_Click);

                page.Controls.Add(b2);


                Button b3 = new Button();
                b3.Tag = m.Code;
                b3.Text = "削除";
                b3.Location = new Point(375, 145);
                b3.Size = new Size(50, 22);
                b3.Click += new EventHandler(DeleteButton_Click);

                page.Controls.Add(b3);


                this.TabControl1.TabPages.Add(page);

                if (this.Code.Equals(m.Code))
                {
                    this.TabControl1.SelectedTab = page;
                }
            }

            this.ListShowAll();
        }

        void TextBox1_ValueCheck(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (tb.Tag == null)
            {
                return;
            }

            if (tb.Text.Length == 0)
            {
                return;
            }

            VitalMaster m = (VitalMaster)tb.Tag;

            if (m.DataType1 == VitalMaster.DataType.Int)
            {
                int i = 0;

                if (int.TryParse(tb.Text, out i))
                {
                    if (i < m.Limit11 || i > m.Limit12)
                    {
                        MessageBox.Show("入力できる範囲は " + m.Limit11 + " ～ " + m.Limit12 + " です");
                        tb.Clear();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("整数値を入力してください");
                    tb.Clear();
                    return;
                }
            }
            else if (m.DataType1 == VitalMaster.DataType.Float)
            {
                float f = 0;

                if (float.TryParse(tb.Text, out f))
                {
                    if (f < m.Limit11 || f > m.Limit12)
                    {
                        MessageBox.Show("入力できる範囲は " + m.Limit11 + " ～ " + m.Limit12 + " です");
                        tb.Clear();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("数値を入力してください");
                    tb.Clear();
                    return;
                }
            }
        }

        void TextBox2_ValueCheck(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (tb.Tag == null)
            {
                return;
            }

            if (tb.Text.Length == 0)
            {
                return;
            }

            VitalMaster m = (VitalMaster)tb.Tag;

            if (m.DataType2 == VitalMaster.DataType.Int)
            {
                int i = 0;

                if (int.TryParse(tb.Text, out i))
                {
                    if (i < m.Limit21 || i > m.Limit22)
                    {
                        MessageBox.Show("入力できる範囲は " + m.Limit21 + " ～ " + m.Limit22 + " です");
                        tb.Clear();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("整数値を入力してください");
                    tb.Clear();
                    return;
                }
            }
            else if (m.DataType2 == VitalMaster.DataType.Float)
            {
                float f = 0;

                if (float.TryParse(tb.Text, out f))
                {
                    if (f < m.Limit21 || f > m.Limit22)
                    {
                        MessageBox.Show("入力できる範囲は " + m.Limit21 + " ～ " + m.Limit22 + " です");
                        tb.Clear();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("数値を入力してください");
                    tb.Clear();
                    return;
                }
            }
        }

        void SaveButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("登録しますか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            string code = ((Button)sender).Tag.ToString();

            Vital obj = new Vital();
            obj.PtId = this.Pat.Id;
            obj.Code = code;
            obj.VitalDate = DatePicker1.Value.ToString("yyyyMMdd");

            TabPage page = this.TabControl1.TabPages["TabPage" + code];

            if (page.Controls.ContainsKey("SEQLabel"))
            {
                int.TryParse(page.Controls["SEQLabel"].Text, out obj.SEQ);
            }

            if (page.Controls.ContainsKey("TimePicker"))
            {
                obj.VitalTime = ((DateTimePicker)page.Controls["TimePicker"]).Value.ToString("HHmm");
            }

            if (page.Controls.ContainsKey("TextBox_" + code + "_1"))
            {
                obj.Data1 = page.Controls["TextBox_" + code + "_1"].Text;
            }

            if (page.Controls.ContainsKey("TextBox_" + code + "_2"))
            {
                obj.Data2 = page.Controls["TextBox_" + code + "_2"].Text;
            }

            if (obj.SEQ > 0)
            {
                obj.Update();
            }
            else
            {
                obj.Insert();
            }

            this.ListShow(code);
        }

        void ClearButton_Click(object sender, EventArgs e)
        {
            string code = ((Button)sender).Tag.ToString();

            this.DataClear(code);
        }

        void DeleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("削除しますか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            string code = ((Button)sender).Tag.ToString();

            Vital obj = new Vital();
            obj.PtId = this.Pat.Id;
            obj.Code = code;
            obj.VitalDate = DatePicker1.Value.ToString("yyyyMMdd");

            TabPage page = this.TabControl1.TabPages["TabPage" + code];

            if (page.Controls.ContainsKey("SEQLabel"))
            {
                int.TryParse(page.Controls["SEQLabel"].Text, out obj.SEQ);
            }

            if (obj.SEQ > 0)
            {
                obj.Delete();
            }

            this.ListShow(code);
        }

        void ListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            DataGridView view = (DataGridView)sender;

            string code = view.Tag.ToString();
            TabPage page = this.TabControl1.TabPages["TabPage" + code];

            if (view.CurrentRow == null)
            {
                return;
            }

            DataGridViewRow r = view.CurrentRow;


            if (page.Controls.ContainsKey("SEQLabel"))
            {
                page.Controls["SEQLabel"].Text = r.Cells["連番"].Value.ToString();
            }

            if (page.Controls.ContainsKey("TimePicker"))
            {
                page.Controls["TimePicker"].Text = r.Cells["時刻"].Value.ToString();
            }

            if (page.Controls.ContainsKey("TextBox_" + code + "_1"))
            {
                page.Controls["TextBox_" + code + "_1"].Text = r.Cells["測定値１"].Value.ToString();
            }

            if (page.Controls.ContainsKey("TextBox_" + code + "_2"))
            {
                page.Controls["TextBox_" + code + "_2"].Text = r.Cells["測定値２"].Value.ToString();
            }

            if (page.Controls.ContainsKey("StaffLabel"))
            {
                page.Controls["StaffLabel"].Text = r.Cells["入力者"].Value.ToString();
            }
        }

        void DataClear(string code)
        {
            TabPage page = this.TabControl1.TabPages["TabPage" + code];

            if (page.Controls.ContainsKey("SEQLabel"))
            {
                page.Controls["SEQLabel"].Text = "";
            }

            if (page.Controls.ContainsKey("TimePicker"))
            {
                page.Controls["TimePicker"].Text = DateTime.Now.ToString("HH:mm");
            }

            if (page.Controls.ContainsKey("TextBox_" + code + "_1"))
            {
                page.Controls["TextBox_" + code + "_1"].Text = "";
            }

            if (page.Controls.ContainsKey("TextBox_" + code + "_2"))
            {
                page.Controls["TextBox_" + code + "_2"].Text = "";
            }

            if (page.Controls.ContainsKey("StaffLabel"))
            {
                page.Controls["StaffLabel"].Text = "";
            }
        }

        void ListClear(string code)
        {
            if (!dSet.Tables.Contains("Table" + code))
            {
                return;
            }

            DataTable table = dSet.Tables["Table" + code];
            table.Rows.Clear();

            this.ListFormat(code);
        }

        void ListShowAll()
        {
            List<Vital> list = Vital.GetListByDate(this.Pat.Id, this.DatePicker1.Value.ToString("yyyyMMdd"));

            foreach (string k in VitalMaster.Dict.Keys)
            {
                VitalMaster m = VitalMaster.Dict[k];

                List<Vital> tmp_list = new List<Vital>();

                foreach (Vital obj in list)
                {
                    if (obj.Code.Equals(m.Code))
                    {
                        tmp_list.Add(obj);
                    }
                }

                this.ListShow(m.Code, tmp_list);
            }
        }

        void ListShow(string code, List<Vital> list)
        {
            if (!VitalMaster.Dict.ContainsKey(code))
            {
                return;
            }

            if (!dSet.Tables.Contains("Table" + code))
            {
                return;
            }

            DataTable table = dSet.Tables["Table" + code];
            table.Rows.Clear();

            VitalMaster m = VitalMaster.Dict[code];

            foreach (Vital obj in list)
            {
                DataRow r = table.NewRow();

                r["連番"] = obj.SEQ;
                r["時刻"] = obj.VitalTimeString;
                r["入力者"] = obj.UpStaffName;
                r["測定値１"] = obj.Data1;
                r["単位１"] = m.Unit1;
                r["測定値２"] = obj.Data2;
                r["単位２"] = m.Unit2;

                table.Rows.Add(r);
            }

            this.ListFormat(code);
        }

        void ListShow(string code)
        {
            List<Vital> list = Vital.GetListByDateCode(this.Pat.Id, this.DatePicker1.Value.ToString("yyyyMMdd"), code);
            this.ListShow(code, list);
        }

        void ListFormat(string code)
        {
            if (!dSet.Tables.Contains("Table" + code))
            {
                return;
            }

            if (!this.TabControl1.TabPages.ContainsKey("TabPage" + code))
            {
                return;
            }

            if (!this.TabControl1.TabPages["TabPage" + code].Controls.ContainsKey("ListView"))
            {
                return;
            }

            if (!VitalMaster.Dict.ContainsKey(code))
            {
                return;
            }

            VitalMaster m = VitalMaster.Dict[code];

            DataGridView view = (DataGridView)this.TabControl1.TabPages["TabPage" + code].Controls["ListView"];

            DataView vw = new DataView(dSet.Tables["Table" + code]);

            view.DataSource = vw;

            view.Columns["連番"].Visible = false;

            view.Columns["時刻"].Width = 50;
            view.Columns["時刻"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["入力者"].Width = 75;

            view.Columns["測定値１"].HeaderText = m.Name1;

            if (m.Unit1.Length > 0)
            {
                view.Columns["単位１"].HeaderText = "単位";
                view.Columns["単位１"].Width = 50;
            }
            else
            {
                view.Columns["単位１"].Visible = false;
            }

            if (m.Name2.Length > 0)
            {
                view.Columns["測定値２"].HeaderText = m.Name2;

                if (m.Unit2.Length > 0)
                {
                    view.Columns["単位２"].HeaderText = "単位";
                    view.Columns["単位２"].Width = 50;
                }
                else
                {
                    view.Columns["単位２"].Visible = false;
                }
            }
            else
            {
                view.Columns["測定値２"].Visible = false;
                view.Columns["単位２"].Visible = false;
            }
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Code = this.TabControl1.SelectedTab.Name.Substring(7);
        }

        private void DatePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.ListShowAll();
        }
    }
}
