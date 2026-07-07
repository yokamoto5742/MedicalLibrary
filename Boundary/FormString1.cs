using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MedicalLibrary.Boundary
{
    public partial class FormString1 : Form
    {
        public FormString1(string title, string desc_label, string desc_box)
        {
            InitializeComponent();

            this.Text = title;
            this.StringLabel.Text = desc_label;
            this.StringBox.Text = desc_box;

            this.OKButton.Select();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}