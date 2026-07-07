namespace MedicalLibrary.Boundary
{
    partial class FormRsvDetail
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
            this.stdControlPat11 = new MedicalLibrary.Boundary.StdControlPat1();
            this.CommentBox1 = new System.Windows.Forms.ComboBox();
            this.CommentBox2 = new System.Windows.Forms.ComboBox();
            this.NameLabel1 = new System.Windows.Forms.Label();
            this.DateTimeLabel1 = new System.Windows.Forms.Label();
            this.SaveButton1 = new System.Windows.Forms.Button();
            this.WakusPanel1 = new System.Windows.Forms.Panel();
            this.WakusButton3 = new System.Windows.Forms.RadioButton();
            this.WakusButton2 = new System.Windows.Forms.RadioButton();
            this.WakusButton1 = new System.Windows.Forms.RadioButton();
            this.ContLabel1 = new System.Windows.Forms.Label();
            this.WakusPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // stdControlPat11
            // 
            this.stdControlPat11.Location = new System.Drawing.Point(10, 55);
            this.stdControlPat11.Mode1 = MedicalLibrary.Boundary.StdControlPat1.Mode.ShortWide;
            this.stdControlPat11.Name = "stdControlPat11";
            this.stdControlPat11.ReadOnly = true;
            this.stdControlPat11.Size = new System.Drawing.Size(265, 60);
            this.stdControlPat11.TabIndex = 0;
            // 
            // CommentBox1
            // 
            this.CommentBox1.FormattingEnabled = true;
            this.CommentBox1.Location = new System.Drawing.Point(75, 120);
            this.CommentBox1.Name = "CommentBox1";
            this.CommentBox1.Size = new System.Drawing.Size(200, 20);
            this.CommentBox1.TabIndex = 1;
            // 
            // CommentBox2
            // 
            this.CommentBox2.FormattingEnabled = true;
            this.CommentBox2.Location = new System.Drawing.Point(75, 145);
            this.CommentBox2.Name = "CommentBox2";
            this.CommentBox2.Size = new System.Drawing.Size(200, 20);
            this.CommentBox2.TabIndex = 2;
            // 
            // NameLabel1
            // 
            this.NameLabel1.BackColor = System.Drawing.SystemColors.Info;
            this.NameLabel1.Location = new System.Drawing.Point(5, 5);
            this.NameLabel1.Name = "NameLabel1";
            this.NameLabel1.Size = new System.Drawing.Size(275, 20);
            this.NameLabel1.TabIndex = 3;
            this.NameLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DateTimeLabel1
            // 
            this.DateTimeLabel1.BackColor = System.Drawing.SystemColors.Info;
            this.DateTimeLabel1.Location = new System.Drawing.Point(5, 30);
            this.DateTimeLabel1.Name = "DateTimeLabel1";
            this.DateTimeLabel1.Size = new System.Drawing.Size(275, 20);
            this.DateTimeLabel1.TabIndex = 4;
            this.DateTimeLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SaveButton1
            // 
            this.SaveButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SaveButton1.Location = new System.Drawing.Point(105, 295);
            this.SaveButton1.Name = "SaveButton1";
            this.SaveButton1.Size = new System.Drawing.Size(75, 23);
            this.SaveButton1.TabIndex = 5;
            this.SaveButton1.Text = "登録";
            this.SaveButton1.UseVisualStyleBackColor = true;
            this.SaveButton1.Click += new System.EventHandler(this.SaveButton1_Click);
            // 
            // WakusPanel1
            // 
            this.WakusPanel1.Controls.Add(this.WakusButton3);
            this.WakusPanel1.Controls.Add(this.WakusButton2);
            this.WakusPanel1.Controls.Add(this.WakusButton1);
            this.WakusPanel1.Location = new System.Drawing.Point(12, 175);
            this.WakusPanel1.Name = "WakusPanel1";
            this.WakusPanel1.Size = new System.Drawing.Size(265, 30);
            this.WakusPanel1.TabIndex = 8;
            // 
            // WakusButton3
            // 
            this.WakusButton3.Appearance = System.Windows.Forms.Appearance.Button;
            this.WakusButton3.Location = new System.Drawing.Point(175, 3);
            this.WakusButton3.Name = "WakusButton3";
            this.WakusButton3.Size = new System.Drawing.Size(80, 24);
            this.WakusButton3.TabIndex = 2;
            this.WakusButton3.TabStop = true;
            this.WakusButton3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.WakusButton3.UseVisualStyleBackColor = true;
            // 
            // WakusButton2
            // 
            this.WakusButton2.Appearance = System.Windows.Forms.Appearance.Button;
            this.WakusButton2.Location = new System.Drawing.Point(90, 3);
            this.WakusButton2.Name = "WakusButton2";
            this.WakusButton2.Size = new System.Drawing.Size(80, 24);
            this.WakusButton2.TabIndex = 1;
            this.WakusButton2.TabStop = true;
            this.WakusButton2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.WakusButton2.UseVisualStyleBackColor = true;
            // 
            // WakusButton1
            // 
            this.WakusButton1.Appearance = System.Windows.Forms.Appearance.Button;
            this.WakusButton1.Location = new System.Drawing.Point(5, 3);
            this.WakusButton1.Name = "WakusButton1";
            this.WakusButton1.Size = new System.Drawing.Size(80, 24);
            this.WakusButton1.TabIndex = 0;
            this.WakusButton1.TabStop = true;
            this.WakusButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.WakusButton1.UseVisualStyleBackColor = true;
            // 
            // ContLabel1
            // 
            this.ContLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContLabel1.AutoEllipsis = true;
            this.ContLabel1.BackColor = System.Drawing.SystemColors.Info;
            this.ContLabel1.ForeColor = System.Drawing.Color.Red;
            this.ContLabel1.Location = new System.Drawing.Point(10, 215);
            this.ContLabel1.Name = "ContLabel1";
            this.ContLabel1.Size = new System.Drawing.Size(265, 70);
            this.ContLabel1.TabIndex = 10;
            this.ContLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormRsvDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 322);
            this.Controls.Add(this.ContLabel1);
            this.Controls.Add(this.WakusPanel1);
            this.Controls.Add(this.SaveButton1);
            this.Controls.Add(this.DateTimeLabel1);
            this.Controls.Add(this.NameLabel1);
            this.Controls.Add(this.CommentBox2);
            this.Controls.Add(this.CommentBox1);
            this.Controls.Add(this.stdControlPat11);
            this.Name = "FormRsvDetail";
            this.Text = "FormRsvDetail";
            this.WakusPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private StdControlPat1 stdControlPat11;
        private System.Windows.Forms.ComboBox CommentBox1;
        private System.Windows.Forms.ComboBox CommentBox2;
        private System.Windows.Forms.Label NameLabel1;
        private System.Windows.Forms.Label DateTimeLabel1;
        private System.Windows.Forms.Button SaveButton1;
        private System.Windows.Forms.Panel WakusPanel1;
        private System.Windows.Forms.RadioButton WakusButton3;
        private System.Windows.Forms.RadioButton WakusButton2;
        private System.Windows.Forms.RadioButton WakusButton1;
        private System.Windows.Forms.Label ContLabel1;
    }
}