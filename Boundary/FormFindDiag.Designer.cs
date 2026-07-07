namespace MedicalLibrary.Boundary
{
    partial class FormFindDiag
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFindDiag));
            this.label5 = new System.Windows.Forms.Label();
            this.DiagFindPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.DiagFindBox1 = new System.Windows.Forms.TextBox();
            this.ICDBox2 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.ICDBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuffixCodeBox1 = new System.Windows.Forms.TextBox();
            this.PrefixCodeBox1 = new System.Windows.Forms.TextBox();
            this.DiagCodeBox1 = new System.Windows.Forms.TextBox();
            this.DiagNameBox1 = new System.Windows.Forms.TextBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.MainNameBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 43;
            this.label5.Text = "病名検索";
            // 
            // DiagFindPanel1
            // 
            this.DiagFindPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DiagFindPanel1.AutoScroll = true;
            this.DiagFindPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DiagFindPanel1.Location = new System.Drawing.Point(5, 28);
            this.DiagFindPanel1.Name = "DiagFindPanel1";
            this.DiagFindPanel1.Size = new System.Drawing.Size(475, 160);
            this.DiagFindPanel1.TabIndex = 42;
            this.DiagFindPanel1.WrapContents = false;
            // 
            // DiagFindBox1
            // 
            this.DiagFindBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DiagFindBox1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.DiagFindBox1.Location = new System.Drawing.Point(65, 5);
            this.DiagFindBox1.Name = "DiagFindBox1";
            this.DiagFindBox1.Size = new System.Drawing.Size(415, 19);
            this.DiagFindBox1.TabIndex = 41;
            this.DiagFindBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DiagFindBox1_KeyDown);
            // 
            // ICDBox2
            // 
            this.ICDBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ICDBox2.Location = new System.Drawing.Point(325, 237);
            this.ICDBox2.Name = "ICDBox2";
            this.ICDBox2.ReadOnly = true;
            this.ICDBox2.Size = new System.Drawing.Size(45, 19);
            this.ICDBox2.TabIndex = 73;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(275, 222);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(36, 12);
            this.label13.TabIndex = 72;
            this.label13.Text = "ICD10";
            // 
            // ICDBox1
            // 
            this.ICDBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ICDBox1.Location = new System.Drawing.Point(275, 237);
            this.ICDBox1.Name = "ICDBox1";
            this.ICDBox1.ReadOnly = true;
            this.ICDBox1.Size = new System.Drawing.Size(45, 19);
            this.ICDBox1.TabIndex = 71;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(185, 222);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 12);
            this.label4.TabIndex = 70;
            this.label4.Text = "接尾語コード";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(70, 222);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 12);
            this.label3.TabIndex = 69;
            this.label3.Text = "接頭語コード";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 222);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 12);
            this.label2.TabIndex = 68;
            this.label2.Text = "病名コード";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 197);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 67;
            this.label1.Text = "病名";
            // 
            // SuffixCodeBox1
            // 
            this.SuffixCodeBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.SuffixCodeBox1.Location = new System.Drawing.Point(185, 237);
            this.SuffixCodeBox1.Name = "SuffixCodeBox1";
            this.SuffixCodeBox1.ReadOnly = true;
            this.SuffixCodeBox1.Size = new System.Drawing.Size(85, 19);
            this.SuffixCodeBox1.TabIndex = 66;
            // 
            // PrefixCodeBox1
            // 
            this.PrefixCodeBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.PrefixCodeBox1.Location = new System.Drawing.Point(70, 237);
            this.PrefixCodeBox1.Name = "PrefixCodeBox1";
            this.PrefixCodeBox1.ReadOnly = true;
            this.PrefixCodeBox1.Size = new System.Drawing.Size(110, 19);
            this.PrefixCodeBox1.TabIndex = 65;
            // 
            // DiagCodeBox1
            // 
            this.DiagCodeBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.DiagCodeBox1.Location = new System.Drawing.Point(5, 237);
            this.DiagCodeBox1.Name = "DiagCodeBox1";
            this.DiagCodeBox1.ReadOnly = true;
            this.DiagCodeBox1.Size = new System.Drawing.Size(60, 19);
            this.DiagCodeBox1.TabIndex = 64;
            // 
            // DiagNameBox1
            // 
            this.DiagNameBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.DiagNameBox1.Location = new System.Drawing.Point(40, 194);
            this.DiagNameBox1.Name = "DiagNameBox1";
            this.DiagNameBox1.ReadOnly = true;
            this.DiagNameBox1.Size = new System.Drawing.Size(220, 19);
            this.DiagNameBox1.TabIndex = 63;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(400, 235);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 74;
            this.OKButton.Text = "確定";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(270, 197);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 76;
            this.label6.Text = "本体病名";
            // 
            // MainNameBox1
            // 
            this.MainNameBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.MainNameBox1.Location = new System.Drawing.Point(330, 194);
            this.MainNameBox1.Name = "MainNameBox1";
            this.MainNameBox1.ReadOnly = true;
            this.MainNameBox1.Size = new System.Drawing.Size(150, 19);
            this.MainNameBox1.TabIndex = 75;
            // 
            // FormFindDiag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 262);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.MainNameBox1);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.ICDBox2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.ICDBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SuffixCodeBox1);
            this.Controls.Add(this.PrefixCodeBox1);
            this.Controls.Add(this.DiagCodeBox1);
            this.Controls.Add(this.DiagNameBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.DiagFindPanel1);
            this.Controls.Add(this.DiagFindBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormFindDiag";
            this.Text = "病名検索";
            this.Load += new System.EventHandler(this.FormFindDiag_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.FlowLayoutPanel DiagFindPanel1;
        private System.Windows.Forms.TextBox DiagFindBox1;
        private System.Windows.Forms.TextBox ICDBox2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox ICDBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SuffixCodeBox1;
        private System.Windows.Forms.TextBox PrefixCodeBox1;
        private System.Windows.Forms.TextBox DiagCodeBox1;
        private System.Windows.Forms.TextBox DiagNameBox1;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox MainNameBox1;
    }
}