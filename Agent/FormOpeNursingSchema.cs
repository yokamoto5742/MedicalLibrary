using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class FormOpeNursingSchema : Form
    {
        public PictureBox SelectedPictureBox
        {
            get
            {
                PictureBox box = new PictureBox();
                TabPage page = tabControl1.SelectedTab;

                if (page.Controls.ContainsKey("panel1"))
                {
                    foreach (Control c in page.Controls["panel1"].Controls)
                    {
                        PictureBox p = (PictureBox)(c);

                        if (p.BorderStyle == BorderStyle.Fixed3D)
                        {
                            box = p;
                            break;
                        }
                    }
                }

                return box;
            }
        }

        public FormOpeNursingSchema()
        {
            InitializeComponent();
        }

        private void FormOpeNursingSchema_Load(object sender, EventArgs e)
        {
            OpeNursingSettings.Init();

            foreach (OpeNursingSettings.SchemaBg bg in OpeNursingSettings.Current.SchemaBgDict.Values)
            {
                if (!bg.Status.Equals("1")) continue;

                string filePath = AppFile.FilePath(bg.Path);

                if (filePath.Length > 0)
                {
                    PictureBox tmpBox = new PictureBox();
                    OpeNursingSettings.PictureTag tmpTag = new OpeNursingSettings.PictureTag();

                    tmpBox.Size = new Size(100, 100);

                    tmpBox.BackgroundImage = Image.FromFile(filePath);
                    tmpBox.BackgroundImageLayout = ImageLayout.Zoom;

                    tmpTag.Id = bg.Id;
                    tmpTag.Tab = bg.Tab;
                    tmpTag.Path = filePath;
                    tmpBox.Tag = tmpTag;

                    tmpBox.Click += new EventHandler(pictureBox_Click);


                    if (tabControl1.TabPages.ContainsKey(tmpTag.Tab))
                    {
                        tabControl1.TabPages[tmpTag.Tab].Controls["panel1"].Controls.Add(tmpBox);
                    }
                    else
                    {
                        TabPage tmpPage = new TabPage(tmpTag.Tab);

                        FlowLayoutPanel tmpPanel = new FlowLayoutPanel();
                        tmpPanel.Name = "panel1";
                        tmpPanel.AutoScroll = true;
                        tmpPanel.Size = new Size(350, 300);
                        tmpPanel.Controls.Add(tmpBox);

                        tmpPage.Controls.Add(tmpPanel);
                        tmpPage.Name = tmpTag.Tab;

                        tabControl1.TabPages.Add(tmpPage);

                        foreach (Control c in tmpPage.Controls)
                        {
                            Console.WriteLine(tmpPage.Name + " " + c.Name);
                        }
                    }
                }
            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox tmpBox = (PictureBox)(sender);

            if (tmpBox.BorderStyle == BorderStyle.Fixed3D)
            {
                tmpBox.BorderStyle = BorderStyle.None;
            }
            else
            {
                foreach (Control c in tabControl1.SelectedTab.Controls["panel1"].Controls)
                {
                    PictureBox p = (PictureBox)(c);

                    if (p.Equals(tmpBox))
                    {
                        p.BorderStyle = BorderStyle.Fixed3D;
                    }
                    else
                    {
                        p.BorderStyle = BorderStyle.None;
                    }
                }
            }
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}