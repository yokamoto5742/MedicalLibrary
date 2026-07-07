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
    public class CtrlOrderPanel1 : Panel
    {
        public CtrlOrderPanel1()
        {
            
        }

        public void Add(CtrlOrder1 order)
        {
            this.Controls.Add(order);
            this.Sort();
        }

        public void Remove(CtrlOrder1 order)
        {
            if (this.Controls.Contains(order))
            {
                this.Controls.Remove(order);
                this.Sort();
            }
        }

        public void Sort()
        {
            int h = 5;

            foreach (Control c in this.Controls)
            {
                if (c is CtrlOrder1)
                {
                    c.Location = new Point(10, h - this.VerticalScroll.Value);

                    h += c.Height + 5;
                }
            }
        }
    }
}
