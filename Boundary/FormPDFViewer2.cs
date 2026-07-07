using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MedicalLibrary.Boundary
{
    public partial class FormPDFViewer2 : Form
    {
        public FormPDFViewer2()
        {
            InitializeComponent();
        }

        public void Navigate(List<string> file_list)
        {
            int x = 0;

            foreach (string file in file_list)
            {
                if (File.Exists(file))
                {
                    WebBrowser wb = new WebBrowser();
                    wb.Size = new Size(200, 150);
                    wb.Location = new Point(x, 5);
                    wb.Navigate(file);
                    this.PdfPanel1.Controls.Add(wb);

                    Button bb = new Button();
                    bb.Size = new Size(200, 25);
                    bb.Location = new Point(x, 155);
                    bb.Text = Path.GetFileName(file);
                    bb.TextAlign = ContentAlignment.MiddleCenter;
                    bb.Tag = file;
                    bb.Click += new EventHandler(bb_Click);
                    this.PdfPanel1.Controls.Add(bb);

                    x += 210;
                }
            }
        }

        void bb_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            if (File.Exists(b.Tag.ToString()))
            {
                this.PdfBrowser1.Navigate(b.Tag.ToString());
            }
        }
    }
}
