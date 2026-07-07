using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MedicalLibrary.Agent
{
    public partial class FormComeReportTab : Form
    {
        string OrderId;
        string ReportId;

        public FormComeReportTab(string orderId)
        {
            InitializeComponent();

            this.OrderId = orderId;
            this.ReportId = "";
        }

        public FormComeReportTab(string orderId, string reportId, string initName)
        {
            InitializeComponent();

            this.OrderId = orderId;
            this.ReportId = reportId;
            this.TabNameBox.Text = initName;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (this.Owner is FormComeReportPat)
            {
                if (this.TabNameBox.Text.Length > 0)
                {
                    if (this.ReportId.Length > 0)
                    {
                        ((FormComeReportPat)this.Owner).RenameReport(OrderId, ReportId, this.TabNameBox.Text);
                    }
                    else
                    {
                        ((FormComeReportPat)this.Owner).AddReport(OrderId, this.TabNameBox.Text);
                    }
                }
            }

            this.Dispose();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}