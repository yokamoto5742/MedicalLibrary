using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Boundary;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class StdControlPat1 : UserControl
    {
        public enum Mode : int
        {
            Normal = 1,
            Short = 2,
            ShortWide = 3
        }

        public PatBase Pat = new PatBase();

        Mode mode = Mode.Normal;

        public Mode Mode1
        {
            set
            {
                this.mode = value;

                if (this.mode == Mode.Normal)
                {
                    this.Height = 30;
                    this.Width = 450;

                    this.PatNameLabel1.Size = new Size(104, 23);
                    this.PatKanaLabel1.Size = new Size(80, 23);
                    this.PatKanaLabel1.Location = new Point(180, 5);

                    this.PatSexLabel1.Visible = true;
                    this.PatBirthLabel1.Visible = true;
                    this.PatAgeLabel1.Visible = true;

                    this.PatSexLabel1.Location = new Point(261, 5);
                    this.PatBirthLabel1.Location = new Point(285, 5);
                    this.PatAgeLabel1.Location = new Point(406, 5);
                }
                else if (this.mode == Mode.Short)
                {
                    this.Height = 30;
                    this.Width = 450;

                    this.PatNameLabel1.Size = new Size(104, 23);
                    this.PatKanaLabel1.Size = new Size(80, 23);
                    this.PatKanaLabel1.Location = new Point(180, 5);

                    this.PatSexLabel1.Visible = false;
                    this.PatBirthLabel1.Visible = false;
                    this.PatAgeLabel1.Visible = false;

                    this.PatSexLabel1.Location = new Point(0, 0);
                    this.PatBirthLabel1.Location = new Point(0, 0);
                    this.PatAgeLabel1.Location = new Point(0, 0);
                }
                else if (this.mode == Mode.ShortWide)
                {
                    this.Height = 60;
                    this.Width = 265;

                    this.PatNameLabel1.Size = new Size(104, 23);
                    this.PatKanaLabel1.Size = new Size(80, 23);
                    this.PatKanaLabel1.Location = new Point(180, 5);

                    this.PatSexLabel1.Visible = true;
                    this.PatBirthLabel1.Visible = true;
                    this.PatAgeLabel1.Visible = true;

                    this.PatSexLabel1.Location = new Point(75, 30);
                    this.PatBirthLabel1.Location = new Point(99, 30);
                    this.PatAgeLabel1.Location = new Point(220, 30);
                }
            }
            get
            {
                return this.mode;
            }
        }

        bool read_only = true;

        public bool ReadOnly
        {
            set
            {
                this.read_only = value;

                this.PatIdBox1.ReadOnly = this.read_only;
            }
            get
            {
                return this.read_only;
            }
        }


        public StdControlPat1()
        {
            InitializeComponent();
        }

        private void StdControlPat1_Load(object sender, EventArgs e)
        {
            if (this.Pat.Id.Length == 0 && AppStat.CurrentPat.Id.Length > 0)
            {
                this.PatSet(AppStat.CurrentPat);
            }

            this.PatIdBox1.Focus();
        }

        public new bool Focus()
        {
            return this.PatIdBox1.Focus();
        }

        void PatFormat()
        {
            if (this.PatSexLabel1.Text.Equals("男"))
            {
                this.PatSexLabel1.ForeColor = Color.Black;
            }
            else if (this.PatSexLabel1.Text.Equals("女"))
            {
                this.PatSexLabel1.ForeColor = Color.Red;
            }
        }

        public void PatSet(PatBase p)
        {
            this.Pat = p;

            this.PatIdBox1.Clear();
            this.PatNameLabel1.Text = "";
            this.PatKanaLabel1.Text = "";
            this.PatSexLabel1.Text = "";
            this.PatBirthLabel1.Text = "";
            this.PatAgeLabel1.Text = "";

            this.PatIdBox1.Text = p.Id;
            this.PatNameLabel1.Text = p.Name;
            this.PatKanaLabel1.Text = p.Kana;
            this.PatSexLabel1.Text = p.SexNameShort;

            if (p.BirthStringJ.Length > 0)
            {
                this.PatBirthLabel1.Text = p.BirthStringJ + "生";
            }

            if (p.Age.Length > 0)
            {
                this.PatAgeLabel1.Text = p.Age + "歳";
            }

            this.PatFormat();
        }

        public void FontSet(AppFont f)
        {
            this.Font = f.Ft;

            foreach (Control c in this.Controls)
            {
                if (c.GetType().Name.StartsWith("Label") ||
                    c.GetType().Name.StartsWith("TextBox") ||
                    c.GetType().Name.StartsWith("ComboBox") ||
                    c.GetType().Name.StartsWith("CheckBox") ||
                    c.GetType().Name.StartsWith("Button") ||
                    c.GetType().Name.StartsWith("DataGridView"))
                {
                    c.Font = f.Ft;
                }
            }
        }

        private void PatIdBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.PatIdBox1.Text.Length > 0)
                {
                    double pid = 0;

                    if (!double.TryParse(this.PatIdBox1.Text, out pid))
                    {
                        MessageBox.Show("患者コードは数字のみで入力してください");
                        this.PatIdBox1.Clear();
                        return;
                    }

                    PatBase p = PatBase.Load(this.PatIdBox1.Text);

                    this.PatSet(p);

                    if (this.Parent is StdForm1)
                    {
                        StdForm1 f = (StdForm1)(this.Parent);
                        f.PatSet(p);
                    }
                }
            }
            else if (e.KeyCode == Keys.F3)
            {
                PatBase p = FormFindPat.FindPat();

                if (p.Id.Length > 0)
                {
                    this.PatSet(p);

                    if (this.Parent is StdForm1)
                    {
                        StdForm1 f = (StdForm1)(this.Parent);
                        f.PatSet(p);
                    }
                }
            }
        }
    }
}
