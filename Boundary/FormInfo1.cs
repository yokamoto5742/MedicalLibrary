using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace MedicalLibrary.Boundary
{
    public partial class FormInfo1 : Form
    {
        public FormInfo1()
        {
            InitializeComponent();
        }

        public FormInfo1(string caption, string cont = "", int width = 480, int height = 300)
        {
            InitializeComponent();

            this.Text = caption;
            this.ContBox1.Text = cont;
            this.Width = width;
            this.Height = height;
        }

        public void FileRead(string file, bool append = false)
        {
            if (!File.Exists(file))
            {
                return;
            }

            StreamReader reader = new StreamReader(file, Encoding.Default);

            string cont = "";
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                cont += line + Environment.NewLine;
            }

            if (!append)
            {
                this.ContBox1.Clear();
            }

            if (cont.Length > this.ContBox1.MaxLength)
            {
                this.ContBox1.Text += cont.Substring(0, this.ContBox1.MaxLength);
            }
            else
            {
                this.ContBox1.Text += cont;
            }

            this.ContBox1.SelectionStart = this.ContBox1.Text.Length - 1;
        }
    }
}
