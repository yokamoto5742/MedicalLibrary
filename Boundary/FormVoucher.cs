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
    public partial class FormVoucher : Form
    {
        DataSet dSet = new DataSet();
        List<VoucherComp> list = new List<VoucherComp>();

        public FormVoucher()
        {
            InitializeComponent();
        }

        private void FormVoucher_Load(object sender, EventArgs e)
        {
            DataTable table = dSet.Tables.Add("Voucher");
        }

        private void ShowButton1_Click(object sender, EventArgs e)
        {
            if (CodeBox1.Text.Length == 0)
            {
                MessageBox.Show("伝票番号を入力してください");
                return;
            }

            DataTable table = dSet.Tables["Voucher"];
            table.Columns.Clear();
            table.Rows.Clear();

            list = VoucherComp.GetList(CodeBox1.Text);

            int Rows = 0;
            int Cols = 0;

            foreach (VoucherComp obj in list)
            {
                if (obj.Col > Cols)
                {
                    Cols = obj.Col;
                }
            }

            for (int i = 1; i <= Cols; i++)
            {
                table.Columns.Add(i.ToString());
            }

            foreach (VoucherComp obj in list)
            {
                if (obj.Row > Rows)
                {
                    Rows = obj.Row;
                }
            }

            for (int i = 1; i <= Rows; i++)
            {
                DataRow r = table.NewRow();
                table.Rows.Add(r);
            }

            foreach (VoucherComp obj in list)
            {
                table.Rows[obj.Row - 1][obj.Col - 1] = obj.Cont1.Name;
            }

            DataView view = new DataView(table);

            VoucherGridView1.DataSource = view;

            for (int i = 1; i <= VoucherGridView1.Rows.Count; i++)
            {
                VoucherGridView1.Rows[i - 1].HeaderCell.Value = i.ToString();
            }

            foreach (VoucherComp obj in list)
            {
                if (obj.Attr1.Kind.Equals("3"))
                {
                    // チェックボックス
                    DataGridViewCheckBoxLabelCell cb = new DataGridViewCheckBoxLabelCell();
                    cb.LabelText = obj.Cont1.Name;

                    VoucherGridView1.Rows[obj.Row - 1].Cells[obj.Col - 1] = cb;
                    VoucherGridView1.Rows[obj.Row - 1].Cells[obj.Col - 1].Value = true;

                }
                else if (obj.Attr1.Kind.Equals("4"))
                {
                    // コンボボックス
                    DataGridViewComboBoxCell cell = new DataGridViewComboBoxCell();

                    foreach (VoucherCont c in obj.ContList1)
                    {
                        cell.Items.Add(c.Name);
                    }

                    VoucherGridView1.Rows[obj.Row - 1].Cells[obj.Col - 1] = cell;
                }
                else if (obj.Attr1.Kind.Equals("5"))
                {
                    // トグルボタン
                    DataGridViewButtonCell button = new DataGridViewButtonCell();
                    button.Value = obj.Cont1.Name;

                    VoucherGridView1.Rows[obj.Row - 1].Cells[obj.Col - 1] = button;
                }
            }

            this.MakePanel();
        }

        void MakePanel()
        {
            VoucherCell[,] cells = VoucherCell.Get(list);

            VoucherPanel1.Controls.Clear();

            foreach (VoucherComp obj in list)
            {
                if (obj.Attr1.Kind.Equals("1"))
                {
                    // 数値
                    TextBox cb = new TextBox();
                    cb.ImeMode = System.Windows.Forms.ImeMode.Disable;
                    cb.Width = (int)obj.Width;
                    cb.Height = (int)obj.Height;
                    cb.Tag = obj;
                    cb.Location = new Point((int)(cells[obj.Row - 1, obj.Col - 1].X), (int)(cells[obj.Row - 1, obj.Col - 1].Y));

                    VoucherPanel1.Controls.Add(cb);
                }
                else if (obj.Attr1.Kind.Equals("2"))
                {
                    if (obj.Attr1.Embed.Equals("1"))
                    {
                        // ラベル
                        Label cb = new Label();
                        cb.AutoEllipsis = true;
                        cb.Text = obj.Cont1.Name;
                        cb.Width = (int)obj.Width;
                        cb.Height = (int)obj.Height;
                        cb.Tag = obj;
                        cb.Location = new Point((int)(cells[obj.Row - 1, obj.Col - 1].X), (int)(cells[obj.Row - 1, obj.Col - 1].Y));

                        VoucherPanel1.Controls.Add(cb);
                    }
                    else
                    {
                        // テキストボックス
                        TextBox cb = new TextBox();
                        cb.Text = obj.Cont1.Name;
                        cb.Width = (int)obj.Width;
                        cb.Height = (int)obj.Height;
                        cb.Tag = obj;
                        cb.Location = new Point((int)(cells[obj.Row - 1, obj.Col - 1].X), (int)(cells[obj.Row - 1, obj.Col - 1].Y));

                        VoucherPanel1.Controls.Add(cb);
                    }
                }
                else if (obj.Attr1.Kind.Equals("3"))
                {
                    // チェックボックス
                    CheckBox cb = new CheckBox();
                    cb.AutoEllipsis = true;
                    cb.Text = obj.Cont1.Name;
                    cb.Width = (int)obj.Width;
                    cb.Height = (int)obj.Height;
                    cb.Tag = obj;
                    cb.Location = new Point((int)(cells[obj.Row - 1, obj.Col - 1].X), (int)(cells[obj.Row - 1, obj.Col - 1].Y));

                    VoucherPanel1.Controls.Add(cb);
                }
                else if (obj.Attr1.Kind.Equals("4"))
                {
                    // コンボボックス
                    ComboBox cb = new ComboBox();
                    cb.Width = (int)obj.Width;
                    cb.Height = (int)obj.Height;
                    cb.Tag = obj;
                    cb.Location = new Point((int)(cells[obj.Row - 1, obj.Col - 1].X), (int)(cells[obj.Row - 1, obj.Col - 1].Y));

                    foreach (VoucherCont c in obj.ContList1)
                    {
                        cb.Items.Add(c.Name);
                    }

                    VoucherPanel1.Controls.Add(cb);
                }
                else if (obj.Attr1.Kind.Equals("5"))
                {
                    // トグルボタン
                    CheckBox cb = new CheckBox();
                    cb.Text = obj.Cont1.Name;
                    cb.Appearance = Appearance.Button;
                    cb.Width = (int)obj.Width;
                    cb.Height = (int)obj.Height;
                    cb.Tag = obj;
                    cb.Location = new Point((int)(cells[obj.Row - 1, obj.Col - 1].X), (int)(cells[obj.Row - 1, obj.Col - 1].Y));

                    cb.Checked = obj.Checked;

                    cb.CheckedChanged += new EventHandler(cb_CheckedChanged);

                    VoucherPanel1.Controls.Add(cb);
                }
            }

            this.MovePanel();
        }

        void MovePanel()
        {
            VoucherCell[,] cells = VoucherCell.Get(list);

            int x = VoucherPanel1.HorizontalScroll.Value;
            int y = VoucherPanel1.VerticalScroll.Value;

            foreach (VoucherComp obj in list)
            {
                foreach (Control c in VoucherPanel1.Controls)
                {
                    VoucherComp v = (VoucherComp)(c.Tag);

                    if (obj == v)
                    {
                        c.Location = new Point((int)(cells[obj.Row - 1, obj.Col - 1].X) - x, (int)(cells[obj.Row - 1, obj.Col - 1].Y) - y);

                        if (!cells[obj.Row - 1, obj.Col - 1].Visible)
                        {
                            c.Visible = false;
                        }
                        else
                        {
                            c.Visible = true;
                        }

                        break;
                    }
                }
            }

            VoucherPanel1.Refresh();
        }

        void cb_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            VoucherComp v = (VoucherComp)(c.Tag);

            v.Checked = c.Checked;

            MovePanel();
        }
    }
}
