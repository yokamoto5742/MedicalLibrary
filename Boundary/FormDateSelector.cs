using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class FormDateSelector : Form
    {
        public string Date = "";

        public FormDateSelector()
        {
            InitializeComponent();
        }

        public FormDateSelector(string date)
        {
            InitializeComponent();

            if (AppString.IsDate(date.Replace("/", "").Replace("-", "")))
            {
                this.Date = date.Replace("/", "").Replace("-", "");
                DateTime dt = DateTime.Now;

                if (DateTime.TryParse(DateTimeAgent.DateFormat(this.Date, DateTimeAgent.DateFormatKind.LONG), out dt))
                {
                    this.Calendar1.SelectionStart = dt;
                }
            }
        }

        private void SelectButton1_Click(object sender, EventArgs e)
        {
            this.Date = this.Calendar1.SelectionStart.ToString("yyyyMMdd");
            this.DialogResult = DialogResult.OK;
        }
    }
}
