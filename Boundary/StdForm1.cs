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
    public class StdForm1 : Form
    {
        public AppFont Fnt = AppStat.CurrentFont;

        PatBase _Pat = new PatBase();

        public PatBase Pat
        {
            get
            {
                return this._Pat;
            }
            set
            {
                this._Pat = value;
            }
        }

        public StdForm1()
        {
            InitializeComponent();
        }

        private void StdForm1_Load(object sender, EventArgs e)
        {
            this.Font = this.Fnt.Ft;
        }

        public virtual void FontSet(AppFont f)
        {
            this.Fnt = f;
            this.Font = f.Ft;

            foreach (Control c in this.Controls)
            {
                c.Font = f.Ft;
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
            }
        }

        public virtual void PatSet(PatBase p)
        {
            this.Pat = p;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // StdForm1
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "StdForm1";
            this.Load += new System.EventHandler(this.StdForm1_Load);
            this.ResumeLayout(false);

        }
    }
}
