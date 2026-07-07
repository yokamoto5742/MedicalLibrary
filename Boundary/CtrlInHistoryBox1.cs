using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public class CtrlInHistoryBox1 : ComboBox
    {
        public CtrlInHistoryBox1()
        {
            this.Width = 180;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void Init(string pt_id)
        {
            this.Items.Clear();

            List<PatIn> list = PatIn.GetHistory(pt_id);

            foreach (PatIn obj in list)
            {
                this.Items.Add(obj);
            }
        }
    }
}
