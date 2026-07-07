using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class FormSchema1 : Form
    {
        /// <summary>
        /// 呼び出し元の CtrlSoapPanel1
        /// </summary>
        public CtrlSoapPanel1 SoapPanel1;

        /// <summary>
        /// 呼び出し元の CtrlSoapImgBox1
        /// 画像修正の場合にセットされる
        /// </summary>
        public CtrlSoapImgBox1 SoapImgBox1;

        public string SoapKind = "";

        public Color Color1 = Color.Red;

        public enum Shape : int
        {
            None = 0,
            Line = 1,
            Free = 2,
            String = 3,
            Erase = 4
        }

        public Shape shape1 = Shape.None;

        public Shape Shape1
        {
            get
            {
                return this.shape1;
            }
            set
            {
                this.shape1 = value;

                if (this.shape1 == Shape.Line)
                {
                    this.ShapeButton1.Checked = true;
                }
                else if (this.shape1 == Shape.Free)
                {
                    this.ShapeButton2.Checked = true;
                }
                else if (this.shape1 == Shape.String)
                {
                    this.ShapeButton3.Checked = true;
                }
                else if (this.shape1 == Shape.Erase)
                {
                    this.ShapeButton4.Checked = true;
                }
                else
                {
                    this.ShapeButton1.Checked = false;
                    this.ShapeButton2.Checked = false;
                    this.ShapeButton3.Checked = false;
                    this.ShapeButton4.Checked = false;
                }
            }
        }

        // 0: 非描画, 1: 描画
        int mode = 0;

        int startX = -1;
        int startY = -1;

        int endX = -1;
        int endY = -1;

        Graphics g1;

        // 背景画像
        Image im1;

        Pen p1;


        public FormSchema1()
        {
            InitializeComponent();

            this.ShapeButton1.CheckedChanged += new EventHandler(ShapeButton_CheckedChanged);
            this.ShapeButton2.CheckedChanged += new EventHandler(ShapeButton_CheckedChanged);
            this.ShapeButton3.CheckedChanged += new EventHandler(ShapeButton_CheckedChanged);
            this.ShapeButton4.CheckedChanged += new EventHandler(ShapeButton_CheckedChanged);

            this.SchemaBox1.Image = new Bitmap(this.SchemaBox1.Width, this.SchemaBox1.Height);
            g1 = Graphics.FromImage(this.SchemaBox1.Image);

//            im1 = this.SchemaBox1.Image;

            this.p1 = new Pen(this.Color1, (float)this.PenWidthBox1.Value);
            this.ColorLabel1.BackColor = this.Color1;

            foreach (SchemaGroup group in SchemaGroup.List)
            {
                TabPage page = new TabPage();
                page.Name = group.Code;
                page.Text = group.Name;
                page.AutoScroll = true;

                int x = 5;
                int y = 5;

                foreach (SchemaImage image in group.ImageList)
                {
                    if (!File.Exists(image.FilePath))
                    {
                        continue;
                    }

                    PictureBox box = new PictureBox();
                    box.Size = new Size(150, 150);
                    box.Location = new Point(x, y);
                    box.BackgroundImage = Image.FromFile(image.FilePath);
                    box.BackgroundImageLayout = ImageLayout.Stretch;
                    box.DoubleClick += new EventHandler(Box_DoubleClick);

                    page.Controls.Add(box);

                    x += 155;

                    if (x > this.TabControl1.Width - 155)
                    {
                        x = 5;
                        y += 155;
                    }
                }

                this.TabControl1.TabPages.Add(page);
            }
        }

        void ShapeButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ShapeButton1.Checked)
            {
                this.Shape1 = Shape.Line;
            }
            else if (this.ShapeButton2.Checked)
            {
                this.Shape1 = Shape.Free;
            }
            else if (this.ShapeButton3.Checked)
            {
                this.Shape1 = Shape.String;
            }
            else if (this.ShapeButton4.Checked)
            {
                this.Shape1 = Shape.Erase;
            }
            else
            {
                this.Shape1 = Shape.None;
            }
        }

        void Box_DoubleClick(object sender, EventArgs e)
        {
            PictureBox box = (PictureBox)sender;

            this.Paste(box.BackgroundImage);
        }

        public void Init(CtrlSoapPanel1 panel1, string soap_kind)
        {
            this.SoapPanel1 = panel1;
            this.SoapKind = soap_kind;
        }

        public void Init(CtrlSoapPanel1 panel1, string soap_kind, CtrlSoapImgBox1 soap_box1)
        {
            this.SoapPanel1 = panel1;
            this.SoapKind = soap_kind;

            this.SoapImgBox1 = soap_box1;

            if (this.SoapImgBox1.SoapImg1.ImgTmpExist)
            {
                Image image = Image.FromFile(this.SoapImgBox1.SoapImg1.ImgTmpPath);

                this.SchemaBox1.Size = image.Size;
                this.SchemaBox1.Image = new Bitmap(image.Width, image.Height);

                g1 = Graphics.FromImage(this.SchemaBox1.Image);
                g1.DrawImage(image, 0, 0, image.Width, image.Height);
            }
            else if (this.SoapImgBox1.SoapImg1.ImgExist)
            {
                Image image = Image.FromFile(this.SoapImgBox1.SoapImg1.ImgPath);

                this.SchemaBox1.Size = image.Size;
                this.SchemaBox1.Image = new Bitmap(image.Width, image.Height);

                g1 = Graphics.FromImage(this.SchemaBox1.Image);
                g1.DrawImage(image, 0, 0, image.Width, image.Height);
            }

            if (this.SoapImgBox1.SoapImg1.OrgImage != null)
            {
                this.im1 = this.SoapImgBox1.SoapImg1.OrgImage;
            }
        }

        private void PasteButton1_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsImage())
            {
                this.Paste(Clipboard.GetImage());
            }
            else
            {
                MessageBox.Show("クリップボードに画像がありません");
            }
        }

        private void PasteButton2_Click(object sender, EventArgs e)
        {
            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog();

            //はじめのファイル名を指定する
            //はじめに「ファイル名」で表示される文字列を指定する
            ofd.FileName = "";

            //はじめに表示されるフォルダを指定する
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            ofd.InitialDirectory = Environment.CurrentDirectory;

            //[ファイルの種類]に表示される選択肢を指定する
            //指定しないとすべてのファイルが表示される
            ofd.Filter =
                "画像ファイル(*.bmp;*.gif;*.jpg;*.jpeg;*.png;*.tiff)|*.bmp;*.gif;*.jpg;*.jpeg;*.png;*.tiff|すべてのファイル(*.*)|*.*";

            //[ファイルの種類]ではじめに
            //「すべてのファイル」が選択されているようにする
            ofd.FilterIndex = 1;

            //タイトルを設定する
            ofd.Title = "開くファイルを選択してください";

            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            ofd.RestoreDirectory = true;

            //存在しないファイルの名前が指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            ofd.CheckFileExists = true;

            //存在しないパスが指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            ofd.CheckPathExists = true;

            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.Paste(Image.FromFile(ofd.FileName));
            }
        }

        void Paste(Image image)
        {
            if (this.im1 != null)
            {
                if (MessageBox.Show("画像を上書きしますか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
            }

            this.SchemaBox1.Size = image.Size;
            this.SchemaBox1.Image = new Bitmap(image.Width, image.Height);

            g1 = Graphics.FromImage(this.SchemaBox1.Image);
            g1.DrawImage(image, 0, 0, image.Width, image.Height);

            this.im1 = image;
        }

        private void ColorLabel1_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();

            cd.Color = this.Color1;

            cd.AllowFullOpen = true;

            cd.SolidColorOnly = false;

            if (cd.ShowDialog() == DialogResult.OK)
            {
                this.Color1 = cd.Color;
                this.ColorLabel1.BackColor = cd.Color;
                this.p1.Color = cd.Color;
            }
        }

        private void SchemaBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.Shape1 == Shape.String)
            {
                FormInputString1 f = new FormInputString1();

                if (f.ShowDialog() == DialogResult.OK)
                {
                    g1.DrawString(f.InputString1, AppFont.DefaultFont.Ft, new SolidBrush(this.Color1), e.X, e.Y);
                    this.SchemaBox1.Refresh();
                }
            }
            else
            {
                this.mode = 1;

                this.startX = e.X;
                this.startY = e.Y;
            }
        }

        private void SchemaBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // 描画中の場合
            if (this.mode == 1)
            {
                this.endX = e.X;
                this.endY = e.Y;

                if (this.Shape1 == Shape.Free)
                {
                    g1.DrawLine(p1, this.startX, this.startY, this.endX, this.endY);

                    this.startX = this.endX;
                    this.startY = this.endY;
                }
                else if (this.Shape1 == Shape.Erase)
                {
                    g1.DrawImage(im1, new Rectangle(startX, startY, 20, 20), new Rectangle(startX, startY, 20, 20), GraphicsUnit.Pixel);

                    this.startX = this.endX;
                    this.startY = this.endY;
                }

                this.SchemaBox1.Refresh();
            }
        }

        private void SchemaBox1_MouseUp(object sender, MouseEventArgs e)
        {
            this.mode = 0;

            this.endX = e.X;
            this.endY = e.Y;

            if (this.Shape1 == Shape.Line)
            {
                g1.DrawLine(p1, this.startX, this.startY, this.endX, this.endY);
                this.SchemaBox1.Refresh();
            }
        }

        private void SchemaBox1_Paint(object sender, PaintEventArgs e)
        {
            if (p1 != null)
            {
                e.Graphics.DrawLine(p1, this.startX, this.startY, this.endX, this.endY);
            }
        }

        private void SaveButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("保存します。よろしいですか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            // 画像をファイルとして一時フォルダに保存
            string s = DateTime.Now.ToString("yyyyMMddHHmmss") + LoginUser.Id.PadLeft(5, '0') + ".jpg";

            this.SchemaBox1.Image.Save(LibSettings.Current.SoapImageTemporaryFolder + "\\" + s, ImageFormat.Jpeg);

            SoapImg img = new SoapImg(s, int.Parse(DateTime.Now.ToString("yyyyMMdd")));
            img.OrgImage = this.im1;

            if (this.SoapImgBox1 == null)
            {
                // 新規
                this.SoapPanel1.ImgAdd(img);
            }
            else
            {
                // 修正
                this.SoapPanel1.ImgReplace(this.SoapImgBox1.SoapImg1.Code, img);
            }

            this.Dispose();
        }

        private void PenWidthBox1_ValueChanged(object sender, EventArgs e)
        {
            this.p1 = new Pen(this.Color1, (float)this.PenWidthBox1.Value);
        }
    }
}
