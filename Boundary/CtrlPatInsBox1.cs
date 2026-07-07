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
    public class CtrlPatInsBox1 : ComboBox
    {
        public CtrlPatInsBox1()
        {
            this.Width = 100;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void Init(string pt_id)
        {
            this.Items.Clear();
            this.Items.Add(new PatIns());

            Dictionary<int, PatIns> dict = PatIns.GetDict(pt_id);

            foreach (PatIns obj in dict.Values)
            {
                if (!obj.IsValid)
                {
                    continue;
                }

                this.Items.Add(obj);
            }
        }
    }
}
