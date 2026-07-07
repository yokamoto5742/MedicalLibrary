namespace MedicalLibrary.Boundary
{
    partial class FormString1
    {
        /// <summary>
        /// 必要なデザイナ変数です。
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

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormString1));
            this.StringLabel = new System.Windows.Forms.Label();
            this.StringBox = new System.Windows.Forms.TextBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StringLabel
            // 
            this.StringLabel.AutoSize = true;
            this.StringLabel.Location = new System.Drawing.Point(12, 9);
            this.StringLabel.Name = "StringLabel";
            this.StringLabel.Size = new System.Drawing.Size(0, 12);
            this.StringLabel.TabIndex = 0;
            // 
            // StringBox
            // 
            this.StringBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.StringBox.Location = new System.Drawing.Point(12, 28);
            this.StringBox.Multiline = true;
            this.StringBox.Name = "StringBox";
            this.StringBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.StringBox.Size = new System.Drawing.Size(268, 68);
            this.StringBox.TabIndex = 1;
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OKButton.Location = new System.Drawing.Point(105, 101);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 20);
            this.OKButton.TabIndex = 2;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // FormString1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 123);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.StringBox);
            this.Controls.Add(this.StringLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormString1";
            this.Text = "FormString1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label StringLabel;
        private System.Windows.Forms.TextBox StringBox;
        private System.Windows.Forms.Button OKButton;
    }
}