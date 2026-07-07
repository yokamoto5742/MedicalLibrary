namespace MedicalLibrary.Agent
{
    partial class FormBillMaker1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBillMaker1));
            this.LogBox1 = new System.Windows.Forms.TextBox();
            this.StartButton1 = new System.Windows.Forms.Button();
            this.StopButton1 = new System.Windows.Forms.Button();
            this.InfoLabel1 = new System.Windows.Forms.Label();
            this.FileClearButton1 = new System.Windows.Forms.Button();
            this.ExitButton1 = new System.Windows.Forms.Button();
            this.MinimizeButton1 = new System.Windows.Forms.Button();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PdfViewerButton1 = new System.Windows.Forms.Button();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LogBox1
            // 
            this.LogBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogBox1.BackColor = System.Drawing.Color.White;
            this.LogBox1.Location = new System.Drawing.Point(5, 55);
            this.LogBox1.Multiline = true;
            this.LogBox1.Name = "LogBox1";
            this.LogBox1.ReadOnly = true;
            this.LogBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogBox1.Size = new System.Drawing.Size(470, 215);
            this.LogBox1.TabIndex = 1;
            // 
            // StartButton1
            // 
            this.StartButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StartButton1.Location = new System.Drawing.Point(151, 273);
            this.StartButton1.Name = "StartButton1";
            this.StartButton1.Size = new System.Drawing.Size(75, 23);
            this.StartButton1.TabIndex = 12;
            this.StartButton1.Text = "開始";
            this.StartButton1.UseVisualStyleBackColor = true;
            this.StartButton1.Visible = false;
            this.StartButton1.Click += new System.EventHandler(this.StartButton1_Click);
            // 
            // StopButton1
            // 
            this.StopButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StopButton1.Enabled = false;
            this.StopButton1.Location = new System.Drawing.Point(232, 273);
            this.StopButton1.Name = "StopButton1";
            this.StopButton1.Size = new System.Drawing.Size(75, 23);
            this.StopButton1.TabIndex = 13;
            this.StopButton1.Text = "停止";
            this.StopButton1.UseVisualStyleBackColor = true;
            this.StopButton1.Visible = false;
            this.StopButton1.Click += new System.EventHandler(this.StopButton1_Click);
            // 
            // InfoLabel1
            // 
            this.InfoLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InfoLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.InfoLabel1.ForeColor = System.Drawing.Color.Red;
            this.InfoLabel1.Location = new System.Drawing.Point(10, 275);
            this.InfoLabel1.Name = "InfoLabel1";
            this.InfoLabel1.Size = new System.Drawing.Size(116, 18);
            this.InfoLabel1.TabIndex = 14;
            this.InfoLabel1.Text = "停止中";
            this.InfoLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.InfoLabel1.DoubleClick += new System.EventHandler(this.InfoLabel1_DoubleClick);
            // 
            // FileClearButton1
            // 
            this.FileClearButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.FileClearButton1.Location = new System.Drawing.Point(311, 273);
            this.FileClearButton1.Name = "FileClearButton1";
            this.FileClearButton1.Size = new System.Drawing.Size(75, 23);
            this.FileClearButton1.TabIndex = 15;
            this.FileClearButton1.Text = "ファイルクリア";
            this.FileClearButton1.UseVisualStyleBackColor = true;
            this.FileClearButton1.Visible = false;
            this.FileClearButton1.Click += new System.EventHandler(this.FileClearButton1_Click);
            // 
            // ExitButton1
            // 
            this.ExitButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ExitButton1.Location = new System.Drawing.Point(391, 273);
            this.ExitButton1.Name = "ExitButton1";
            this.ExitButton1.Size = new System.Drawing.Size(75, 23);
            this.ExitButton1.TabIndex = 16;
            this.ExitButton1.Text = "終了";
            this.ExitButton1.UseVisualStyleBackColor = true;
            this.ExitButton1.Visible = false;
            this.ExitButton1.Click += new System.EventHandler(this.ExitButton1_Click);
            // 
            // MinimizeButton1
            // 
            this.MinimizeButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MinimizeButton1.Location = new System.Drawing.Point(415, 32);
            this.MinimizeButton1.Name = "MinimizeButton1";
            this.MinimizeButton1.Size = new System.Drawing.Size(60, 22);
            this.MinimizeButton1.TabIndex = 17;
            this.MinimizeButton1.Text = "最小化";
            this.MinimizeButton1.UseVisualStyleBackColor = true;
            this.MinimizeButton1.Click += new System.EventHandler(this.MinimizeButton1_Click);
            // 
            // TitleLabel
            // 
            this.TitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TitleLabel.BackColor = System.Drawing.Color.Red;
            this.TitleLabel.ForeColor = System.Drawing.Color.White;
            this.TitleLabel.Location = new System.Drawing.Point(5, 5);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(425, 25);
            this.TitleLabel.TabIndex = 18;
            this.TitleLabel.Text = "外来請求書を発行する医事端末では、このプログラムは常時動かしておいてください。";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TitleLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleLabel_MouseDown);
            this.TitleLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TitleLabel_MouseMove);
            this.TitleLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TitleLabel_MouseUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(100, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(310, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = "表示が邪魔な場合は、右の「最小化」ボタンをクリックしてください。";
            // 
            // PdfViewerButton1
            // 
            this.PdfViewerButton1.Location = new System.Drawing.Point(5, 32);
            this.PdfViewerButton1.Name = "PdfViewerButton1";
            this.PdfViewerButton1.Size = new System.Drawing.Size(75, 22);
            this.PdfViewerButton1.TabIndex = 20;
            this.PdfViewerButton1.Text = "PDF一覧";
            this.PdfViewerButton1.UseVisualStyleBackColor = true;
            this.PdfViewerButton1.Click += new System.EventHandler(this.PdfViewerButton1_Click);
            // 
            // VersionLabel
            // 
            this.VersionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.VersionLabel.BackColor = System.Drawing.Color.LightYellow;
            this.VersionLabel.Location = new System.Drawing.Point(435, 8);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(40, 20);
            this.VersionLabel.TabIndex = 21;
            this.VersionLabel.Text = "1.0.5";
            this.VersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VersionLabel.Click += new System.EventHandler(this.VersionLabel_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 300);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.PdfViewerButton1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.MinimizeButton1);
            this.Controls.Add(this.ExitButton1);
            this.Controls.Add(this.FileClearButton1);
            this.Controls.Add(this.InfoLabel1);
            this.Controls.Add(this.StopButton1);
            this.Controls.Add(this.StartButton1);
            this.Controls.Add(this.LogBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "請求書";
            this.Load += new System.EventHandler(this.FormBillMaker1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LogBox1;
        private System.Windows.Forms.Button StartButton1;
        private System.Windows.Forms.Button StopButton1;
        private System.Windows.Forms.Label InfoLabel1;
        private System.Windows.Forms.Button FileClearButton1;
        private System.Windows.Forms.Button ExitButton1;
        private System.Windows.Forms.Button MinimizeButton1;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button PdfViewerButton1;
        private System.Windows.Forms.Label VersionLabel;
    }
}

