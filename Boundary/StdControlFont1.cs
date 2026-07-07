using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Boundary;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class StdControlFont1 : UserControl
    {
        public StdControlFont1()
        {
            InitializeComponent();
        }

        private void StdControlFont1_Load(object sender, EventArgs e)
        {
            this.FontBox1.Items.Add(9);
            this.FontBox1.Items.Add(10.5);
            this.FontBox1.Items.Add(11);
            this.FontBox1.Items.Add(12);
            this.FontBox1.Items.Add(14);

            this.FontBox1.Text = this.Parent.Font.Size.ToString();
        }

        public void FontSet(AppFont f)
        {
            this.Font = f.Ft;
            this.FontBox1.Text = f.Ft.Size.ToString();

            foreach (Control c in this.Controls)
            {
                /*
                if (c.GetType().Name.StartsWith("Label") ||
                    c.GetType().Name.StartsWith("TextBox") ||
                    c.GetType().Name.StartsWith("ComboBox") ||
                    c.GetType().Name.StartsWith("CheckBox") ||
                    c.GetType().Name.StartsWith("Button") ||
                    c.GetType().Name.StartsWith("DataGridView"))
                {
                    c.Font = f.Ft;
                }
                */

                if (c is Label ||
                    c is TextBox ||
                    c is ComboBox ||
                    c is CheckBox ||
                    c is Button ||
                    c is DataGridView)
                {
                    c.Font = f.Ft;
                }
            }
        }

        private void FontBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.FontSet(new AppFont(float.Parse(this.FontBox1.Text)));
/*
            if (this.Parent.GetType().BaseType.Name.Equals("StdForm1") ||
                this.Parent.GetType().BaseType.BaseType.Name.Equals("StdForm1") ||
                this.Parent.GetType().BaseType.BaseType.BaseType.Name.Equals("StdForm1"))
            {
                StdForm1 f = (StdForm1)(this.Parent);

                f.FontSet(new AppFont(float.Parse(this.FontBox1.Text)));
            }
*/
            if (this.Parent is StdForm1)
            {
                StdForm1 f = (StdForm1)(this.Parent);

                f.FontSet(new AppFont(float.Parse(this.FontBox1.Text)));
            }
        }
    }
}
