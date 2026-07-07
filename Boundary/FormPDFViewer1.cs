using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace MedicalLibrary.Boundary
{
    public partial class FormPDFViewer1 : Form
    {
        public enum PageOrientation : int
        {
            Portrait = 1,
            Landscape = 2
        }

        PageOrientation _Orientation = PageOrientation.Portrait;

        public PageOrientation Orientation
        {
            get
            {
                return this._Orientation;
            }
            set
            {
                this._Orientation = value;

                if (value == PageOrientation.Portrait)
                {
                    this.Width = 540;
                    this.Height = 760;
                }
                else if (value == PageOrientation.Landscape)
                {
                    this.Width = 760;
                    this.Height = 540;
                }
            }
        }


        public FormPDFViewer1()
        {
            InitializeComponent();
        }

        public FormPDFViewer1(PageOrientation direction)
        {
            InitializeComponent();

            this.Orientation = direction;
        }


        public void Navigate(string file)
        {
            if (File.Exists(file))
            {
                this.PdfBrowser1.Navigate(file);
            }
        }
    }
}
