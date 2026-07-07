using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MedicalLibrary.Agent
{
    public partial class MC500DataListForm : Form
    {
        List<MC500> DataList = new List<MC500>();

        public MC500DataListForm()
        {
            InitializeComponent();
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            PanelsShow();
        }

        private void PtIdBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PanelsShow();
            }
        }

        void PanelsShow()
        {
            if (PtIdBox.Text.Length > 0)
            {
                MC500Panels1.PanelsShow(PtIdBox.Text);
            }
        }
    }
}
