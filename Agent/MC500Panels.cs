using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MedicalLibrary.Agent
{
    public partial class MC500Panels : UserControl
    {
        public MC500Panels()
        {
            InitializeComponent();
        }

        void PanelsClear()
        {
            this.Controls.Clear();
        }

        void NoRecord()
        {
            PanelsClear();

            Label lb = new Label();
            lb.Location = new Point(30, 30);
            lb.Text = "記録はありません";

            this.Controls.Add(lb);
        }

        public void PanelsShow(string pt_id)
        {
            if (pt_id.Length == 0)
            {
                NoRecord();
                return;
            }

            PanelsClear();

            int py = 0;
            int c = 0;

            List<MC500> list = MC500.LoadByPt(pt_id);

            if (list.Count == 0)
            {
                NoRecord();
            }
            else
            {
                foreach (MC500 mc in list)
                {
                    MC500Panel panel = new MC500Panel(this);

                    panel.DataShow(mc);
                    panel.Location = new Point(10, py);

                    if (c % 2 != 0)
                    {
                        panel.BackColor = Color.Azure;
                    }

                    this.Controls.Add(panel);

                    py += panel.Height + 5;
                    c++;
                }
            }
        }
    }
}
