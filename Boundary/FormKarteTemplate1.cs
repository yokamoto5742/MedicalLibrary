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
    public partial class FormKarteTemplate1 : Form
    {
        public FormKarteTemplate1()
        {
            InitializeComponent();
        }

        public void Init(CtrlSoapWrite1 soap_write)
        {
            this.KarteTemplate1.Init(soap_write);
        }
    }
}
