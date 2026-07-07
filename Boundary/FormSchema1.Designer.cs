namespace MedicalLibrary.Boundary
{
    partial class FormSchema1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.SchemaBox1 = new System.Windows.Forms.PictureBox();
            this.PasteButton1 = new System.Windows.Forms.Button();
            this.SchemaPanel1 = new System.Windows.Forms.Panel();
            this.ColorLabel1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ShapeButton4 = new System.Windows.Forms.RadioButton();
            this.ShapeButton3 = new System.Windows.Forms.RadioButton();
            this.ShapeButton2 = new System.Windows.Forms.RadioButton();
            this.ShapeButton1 = new System.Windows.Forms.RadioButton();
            this.SaveButton1 = new System.Windows.Forms.Button();
            this.PenWidthBox1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PasteButton2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SchemaBox1)).BeginInit();
            this.SchemaPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PenWidthBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // TabControl1
            // 
            this.TabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl1.Location = new System.Drawing.Point(475, 35);
            this.TabControl1.Multiline = true;
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(500, 520);
            this.TabControl1.TabIndex = 0;
            // 
            // SchemaBox1
            // 
            this.SchemaBox1.BackColor = System.Drawing.Color.White;
            this.SchemaBox1.Location = new System.Drawing.Point(0, 0);
            this.SchemaBox1.Name = "SchemaBox1";
            this.SchemaBox1.Size = new System.Drawing.Size(455, 515);
            this.SchemaBox1.TabIndex = 1;
            this.SchemaBox1.TabStop = false;
            this.SchemaBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.SchemaBox1_Paint);
            this.SchemaBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SchemaBox1_MouseDown);
            this.SchemaBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SchemaBox1_MouseMove);
            this.SchemaBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SchemaBox1_MouseUp);
            // 
            // PasteButton1
            // 
            this.PasteButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PasteButton1.Location = new System.Drawing.Point(650, 8);
            this.PasteButton1.Name = "PasteButton1";
            this.PasteButton1.Size = new System.Drawing.Size(100, 23);
            this.PasteButton1.TabIndex = 2;
            this.PasteButton1.Text = "ｸﾘｯﾌﾟﾎﾞｰﾄﾞ貼付";
            this.PasteButton1.UseVisualStyleBackColor = true;
            this.PasteButton1.Click += new System.EventHandler(this.PasteButton1_Click);
            // 
            // SchemaPanel1
            // 
            this.SchemaPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SchemaPanel1.AutoScroll = true;
            this.SchemaPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SchemaPanel1.Controls.Add(this.SchemaBox1);
            this.SchemaPanel1.Location = new System.Drawing.Point(5, 35);
            this.SchemaPanel1.Name = "SchemaPanel1";
            this.SchemaPanel1.Size = new System.Drawing.Size(460, 520);
            this.SchemaPanel1.TabIndex = 3;
            // 
            // ColorLabel1
            // 
            this.ColorLabel1.BackColor = System.Drawing.Color.Red;
            this.ColorLabel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ColorLabel1.Location = new System.Drawing.Point(10, 10);
            this.ColorLabel1.Name = "ColorLabel1";
            this.ColorLabel1.Size = new System.Drawing.Size(40, 20);
            this.ColorLabel1.TabIndex = 5;
            this.ColorLabel1.Click += new System.EventHandler(this.ColorLabel1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ShapeButton4);
            this.panel1.Controls.Add(this.ShapeButton3);
            this.panel1.Controls.Add(this.ShapeButton2);
            this.panel1.Controls.Add(this.ShapeButton1);
            this.panel1.Location = new System.Drawing.Point(130, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(170, 28);
            this.panel1.TabIndex = 6;
            // 
            // ShapeButton4
            // 
            this.ShapeButton4.Appearance = System.Windows.Forms.Appearance.Button;
            this.ShapeButton4.AutoSize = true;
            this.ShapeButton4.Location = new System.Drawing.Point(126, 3);
            this.ShapeButton4.Name = "ShapeButton4";
            this.ShapeButton4.Size = new System.Drawing.Size(39, 22);
            this.ShapeButton4.TabIndex = 3;
            this.ShapeButton4.TabStop = true;
            this.ShapeButton4.Text = "消去";
            this.ShapeButton4.UseVisualStyleBackColor = true;
            // 
            // ShapeButton3
            // 
            this.ShapeButton3.Appearance = System.Windows.Forms.Appearance.Button;
            this.ShapeButton3.AutoSize = true;
            this.ShapeButton3.Location = new System.Drawing.Point(84, 3);
            this.ShapeButton3.Name = "ShapeButton3";
            this.ShapeButton3.Size = new System.Drawing.Size(39, 22);
            this.ShapeButton3.TabIndex = 2;
            this.ShapeButton3.TabStop = true;
            this.ShapeButton3.Text = "文字";
            this.ShapeButton3.UseVisualStyleBackColor = true;
            // 
            // ShapeButton2
            // 
            this.ShapeButton2.Appearance = System.Windows.Forms.Appearance.Button;
            this.ShapeButton2.AutoSize = true;
            this.ShapeButton2.Location = new System.Drawing.Point(45, 3);
            this.ShapeButton2.Name = "ShapeButton2";
            this.ShapeButton2.Size = new System.Drawing.Size(36, 22);
            this.ShapeButton2.TabIndex = 1;
            this.ShapeButton2.TabStop = true;
            this.ShapeButton2.Text = "ﾌﾘｰ";
            this.ShapeButton2.UseVisualStyleBackColor = true;
            // 
            // ShapeButton1
            // 
            this.ShapeButton1.Appearance = System.Windows.Forms.Appearance.Button;
            this.ShapeButton1.AutoSize = true;
            this.ShapeButton1.Location = new System.Drawing.Point(3, 3);
            this.ShapeButton1.Name = "ShapeButton1";
            this.ShapeButton1.Size = new System.Drawing.Size(39, 22);
            this.ShapeButton1.TabIndex = 0;
            this.ShapeButton1.TabStop = true;
            this.ShapeButton1.Text = "直線";
            this.ShapeButton1.UseVisualStyleBackColor = true;
            // 
            // SaveButton1
            // 
            this.SaveButton1.Location = new System.Drawing.Point(400, 8);
            this.SaveButton1.Name = "SaveButton1";
            this.SaveButton1.Size = new System.Drawing.Size(60, 23);
            this.SaveButton1.TabIndex = 7;
            this.SaveButton1.Text = "確定";
            this.SaveButton1.UseVisualStyleBackColor = true;
            this.SaveButton1.Click += new System.EventHandler(this.SaveButton1_Click);
            // 
            // PenWidthBox1
            // 
            this.PenWidthBox1.DecimalPlaces = 1;
            this.PenWidthBox1.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.PenWidthBox1.Location = new System.Drawing.Point(82, 10);
            this.PenWidthBox1.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.PenWidthBox1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PenWidthBox1.Name = "PenWidthBox1";
            this.PenWidthBox1.Size = new System.Drawing.Size(40, 19);
            this.PenWidthBox1.TabIndex = 8;
            this.PenWidthBox1.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.PenWidthBox1.ValueChanged += new System.EventHandler(this.PenWidthBox1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "太さ";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(480, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "背景画像";
            // 
            // PasteButton2
            // 
            this.PasteButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PasteButton2.Location = new System.Drawing.Point(760, 8);
            this.PasteButton2.Name = "PasteButton2";
            this.PasteButton2.Size = new System.Drawing.Size(100, 23);
            this.PasteButton2.TabIndex = 11;
            this.PasteButton2.Text = "画像ファイル貼付";
            this.PasteButton2.UseVisualStyleBackColor = true;
            this.PasteButton2.Click += new System.EventHandler(this.PasteButton2_Click);
            // 
            // FormSchema1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 562);
            this.Controls.Add(this.PasteButton2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PenWidthBox1);
            this.Controls.Add(this.SaveButton1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ColorLabel1);
            this.Controls.Add(this.PasteButton1);
            this.Controls.Add(this.TabControl1);
            this.Controls.Add(this.SchemaPanel1);
            this.Name = "FormSchema1";
            this.Text = "シェーマ";
            ((System.ComponentModel.ISupportInitialize)(this.SchemaBox1)).EndInit();
            this.SchemaPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PenWidthBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl1;
        private System.Windows.Forms.PictureBox SchemaBox1;
        private System.Windows.Forms.Button PasteButton1;
        private System.Windows.Forms.Panel SchemaPanel1;
        private System.Windows.Forms.Label ColorLabel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton ShapeButton2;
        private System.Windows.Forms.RadioButton ShapeButton1;
        private System.Windows.Forms.RadioButton ShapeButton3;
        private System.Windows.Forms.RadioButton ShapeButton4;
        private System.Windows.Forms.Button SaveButton1;
        private System.Windows.Forms.NumericUpDown PenWidthBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button PasteButton2;
    }
}