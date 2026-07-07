using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MedicalLibrary.Agent
{
    public partial class FormComeReportSchema : Form
    {
        FormComeReportPat fp;

        public struct PictureTag
        {
            public string Id;
            public string Tab;
            public string Path;
        }

        public FormComeReportSchema()
        {
            InitializeComponent();
        }

        public FormComeReportSchema(FormComeReportPat Fp)
        {
            InitializeComponent();

            this.fp = Fp;
        }

        private void FormComeReportSchema_Load(object sender, EventArgs e)
        {
            Font f = new Font("", 7);

            foreach (ComeReportSettings.SchemaBg bg in ComeReportSettings.Current.SchemaBgDict.Values)
            {
                if (!bg.Status.Equals("1"))
                {
                    continue;
                }

                if (!System.IO.File.Exists(bg.Path))
                {
                    continue;
                }

                PictureBox tmpBox = new PictureBox();
                PictureTag tmpTag = new PictureTag();

                tmpBox.Size = new Size(100, 100);

                tmpBox.BackColor = Color.White;
                tmpBox.BackgroundImage = Image.FromFile(bg.Path);
                tmpBox.BackgroundImageLayout = ImageLayout.Zoom;

                tmpBox.Image = new Bitmap(100, 100);
                Graphics g = Graphics.FromImage(tmpBox.Image);
                string[] s = bg.Path.Split('\\');
                g.DrawString(s[s.Length - 1].Split('.')[0], f, Brushes.DarkRed, 2, 2);

                tmpTag.Id = bg.Id;
                tmpTag.Tab = bg.Tab;
                tmpTag.Path = bg.Path;
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
            foreach (Control c in tabControl1.SelectedTab.Controls["panel1"].Controls)
            {
                PictureBox p = (PictureBox)(c);

                if (p.BorderStyle == BorderStyle.Fixed3D)
                {
                    PictureTag tmpTag = (PictureTag)(p.Tag);
                    fp.SetSchemaBg(tmpTag.Id, "");
                    break;
                }
            }

            this.Hide();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("背景をクリアしますか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                fp.SetSchemaBg("", "");
                this.Hide();
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}