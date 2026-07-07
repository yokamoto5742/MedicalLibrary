namespace MedicalLibrary.Boundary
{
    partial class FormDPCIdChange
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDPCIdChange));
            this.FileOpenButton1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.FileOpenBox1 = new System.Windows.Forms.TextBox();
            this.FileSaveBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.FileSaveButton1 = new System.Windows.Forms.Button();
            this.LogBox1 = new System.Windows.Forms.TextBox();
            this.ExecButton1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.CodeBox1 = new System.Windows.Forms.TextBox();
            this.CharBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FileOpenButton1
            // 
            this.FileOpenButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FileOpenButton1.Location = new System.Drawing.Point(330, 30);
            this.FileOpenButton1.Name = "FileOpenButton1";
            this.FileOpenButton1.Size = new System.Drawing.Size(50, 23);
            this.FileOpenButton1.TabIndex = 0;
            this.FileOpenButton1.Text = "選択";
            this.FileOpenButton1.UseVisualStyleBackColor = true;
            this.FileOpenButton1.Click += new System.EventHandler(this.FileOpenButton1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "元フォルダ";
            // 
            // FileOpenBox1
            // 
            this.FileOpenBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FileOpenBox1.Location = new System.Drawing.Point(75, 32);
            this.FileOpenBox1.Name = "FileOpenBox1";
            this.FileOpenBox1.ReadOnly = true;
            this.FileOpenBox1.Size = new System.Drawing.Size(250, 19);
            this.FileOpenBox1.TabIndex = 2;
            // 
            // FileSaveBox1
            // 
            this.FileSaveBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FileSaveBox1.Location = new System.Drawing.Point(75, 57);
            this.FileSaveBox1.Name = "FileSaveBox1";
            this.FileSaveBox1.ReadOnly = true;
            this.FileSaveBox1.Size = new System.Drawing.Size(250, 19);
            this.FileSaveBox1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "新フォルダ";
            // 
            // FileSaveButton1
            // 
            this.FileSaveButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FileSaveButton1.Location = new System.Drawing.Point(330, 55);
            this.FileSaveButton1.Name = "FileSaveButton1";
            this.FileSaveButton1.Size = new System.Drawing.Size(50, 23);
            this.FileSaveButton1.TabIndex = 3;
            this.FileSaveButton1.Text = "選択";
            this.FileSaveButton1.UseVisualStyleBackColor = true;
            this.FileSaveButton1.Click += new System.EventHandler(this.FileSaveButton1_Click);
            // 
            // LogBox1
            // 
            this.LogBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogBox1.Location = new System.Drawing.Point(12, 90);
            this.LogBox1.Multiline = true;
            this.LogBox1.Name = "LogBox1";
            this.LogBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogBox1.Size = new System.Drawing.Size(360, 160);
            this.LogBox1.TabIndex = 6;
            // 
            // ExecButton1
            // 
            this.ExecButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ExecButton1.Location = new System.Drawing.Point(160, 255);
            this.ExecButton1.Name = "ExecButton1";
            this.ExecButton1.Size = new System.Drawing.Size(75, 23);
            this.ExecButton1.TabIndex = 7;
            this.ExecButton1.Text = "実行";
            this.ExecButton1.UseVisualStyleBackColor = true;
            this.ExecButton1.Click += new System.EventHandler(this.ExecButton1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "病院コード";
            // 
            // CodeBox1
            // 
            this.CodeBox1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.CodeBox1.Location = new System.Drawing.Point(75, 7);
            this.CodeBox1.MaxLength = 9;
            this.CodeBox1.Name = "CodeBox1";
            this.CodeBox1.Size = new System.Drawing.Size(80, 19);
            this.CodeBox1.TabIndex = 9;
            this.CodeBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CharBox1
            // 
            this.CharBox1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.CharBox1.Location = new System.Drawing.Point(280, 7);
            this.CharBox1.MaxLength = 1;
            this.CharBox1.Name = "CharBox1";
            this.CharBox1.Size = new System.Drawing.Size(30, 19);
            this.CharBox1.TabIndex = 13;
            this.CharBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(175, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "識別番号先頭文字";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 282);
            this.Controls.Add(this.CharBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CodeBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ExecButton1);
            this.Controls.Add(this.LogBox1);
            this.Controls.Add(this.FileSaveBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FileSaveButton1);
            this.Controls.Add(this.FileOpenBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FileOpenButton1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "DPCファイル変換";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FileOpenButton1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FileOpenBox1;
        private System.Windows.Forms.TextBox FileSaveBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button FileSaveButton1;
        private System.Windows.Forms.TextBox LogBox1;
        private System.Windows.Forms.Button ExecButton1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox CodeBox1;
        private System.Windows.Forms.TextBox CharBox1;
        private System.Windows.Forms.Label label5;
    }
}

