using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MedicalLibrary.Boundary
{
    public partial class FormInputString1 : Form
    {
        public string InputString1
        {
            get
            {
                return this.InputBox1.Text;
            }
        }

        public FormInputString1()
        {
            InitializeComponent();
        }

        private void OKButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
//            this.Dispose();
        }
    }
}
