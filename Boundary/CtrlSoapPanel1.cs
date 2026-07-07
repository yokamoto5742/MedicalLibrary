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
    public partial class CtrlSoapPanel1 : UserControl
    {
        string kind = "1";

        public string Kind
        {
            set
            {
                this.kind = value;

                if (Dict.SoapShortDict.ContainsKey(this.kind))
                {
                    this.KindLabel.Text = Dict.SoapShortDict[this.kind];
                }
                else
                {
                    this.KindLabel.Text = "";
                }
            }
            get
            {
                return this.kind;
            }
        }

        public string Cont
        {
            get
            {
                return this.ContBox.Text;
            }
            set
            {
                this.ContBox.Text = value;
            }
        }

        public Dictionary<string, SoapImg> ImgDict = new Dictionary<string, SoapImg>();


        public CtrlSoapPanel1()
        {
            InitializeComponent();

            this.SchemaLabel.Click += new EventHandler(SchemaLabel_Click);
        }

        public CtrlSoapPanel1(string _kind)
        {
            InitializeComponent();

            this.SchemaLabel.Click += new EventHandler(SchemaLabel_Click);
            this.Kind = _kind;
        }

        void SchemaLabel_Click(object sender, EventArgs e)
        {
            if (this.ImgDict.Count >= 5)
            {
                MessageBox.Show("画像をこれ以上追加できません");
                return;
            }

            FormSchema1 f = new FormSchema1();
            f.Init(this, this.Kind);
            f.ShowDialog();
        }

        public void Clear()
        {
            this.ContBox.Clear();
            this.ContBox.Width = this.Width - 30;

            this.ImgDict.Clear();

            foreach (Control c in this.Controls)
            {
                if (c is CtrlSoapImgBox1)
                {
                    this.Controls.Remove(c);
                }
            }
        }

        public void DataShow(SoapDetail data, bool append = true)
        {
            if (append)
            {
                this.ContBox.Text += data.Cont;
            }
            else
            {
                this.ContBox.Text = data.Cont;
                this.ImgDict.Clear();
            }

            int i = 1;

            while (i <= 5)
            {
                if (!this.ImgDict.ContainsKey(i.ToString()))
                {
                    break;
                }

                i++;
            }

            foreach (SoapImg img in data.ImgDict.Values)
            {
                img.Code = i.ToString();
                this.ImgDict.Add(img.Code, img);
                i++;
            }

            this.ImgsShow();
        }

        public void Write(string s, bool append = true)
        {
            if (append)
            {
                this.ContBox.Text += s;
            }
            else
            {
                this.ContBox.Text = s;
            }
        }

        public void WriteLine(string s, bool append = true)
        {
            this.Write(s, append);
            this.ContBox.Text += Environment.NewLine;
        }

        public void ImgsShow()
        {
            int x = this.Width - this.ImgDict.Count * 50 - 5;
            int y = 2;

            this.ContBox.Width = x - 25;

            // いったんすべての画像を削除
            foreach (Control c in this.Controls)
            {
                if (c is CtrlSoapImgBox1)
                {
                    this.Controls.Remove(c);
                }
            }

            foreach (string key in this.ImgDict.Keys)
            {
                SoapImg img = this.ImgDict[key];

                CtrlSoapImgBox1 box = new CtrlSoapImgBox1(img);
                box.Name = "img" + key;
                box.Tag = img;
                box.Location = new Point(x + 2, y);
                box.Width = 48;
                box.DoubleClick += new EventHandler(Box_DoubleClick);

                this.Controls.Add(box);

                x += 50;
            }
        }

        /// <summary>
        /// 画像を追加する
        /// </summary>
        /// <param name="img"></param>
        public void ImgAdd(SoapImg img)
        {
            if (this.ImgDict.Count >= 5)
            {
                MessageBox.Show("画像をこれ以上追加できません");
                return;
            }

            int i = 1;

            while (i <= 5)
            {
                if (!this.ImgDict.ContainsKey(i.ToString()))
                {
                    break;
                }

                i++;
            }

            img.Code = i.ToString();
            this.ImgDict.Add(i.ToString(), img);

            this.ImgsShow();
        }

        /// <summary>
        /// 画像を置き換える
        /// </summary>
        /// <param name="key">1～5の数字</param>
        /// <param name="img"></param>
        public void ImgReplace(string key, SoapImg img)
        {
            if (this.ImgDict.ContainsKey(key))
            {
                this.ImgDict[key] = img;
                this.ImgsShow();
            }
            else
            {
                this.ImgAdd(img);
            }
        }

        void Box_DoubleClick(object sender, EventArgs e)
        {
            CtrlSoapImgBox1 box = (CtrlSoapImgBox1)sender;

            FormSchema1 f = new FormSchema1();
            f.Init(this, this.Kind, box);
            f.ShowDialog();
        }
    }
}
