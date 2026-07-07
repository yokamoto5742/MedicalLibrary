namespace MedicalLibrary.Boundary
{
    partial class FormInputString1
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
            this.InputBox1 = new System.Windows.Forms.TextBox();
            this.OKButton1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InputBox1
            // 
            this.InputBox1.Location = new System.Drawing.Point(5, 5);
            this.InputBox1.Multiline = true;
            this.InputBox1.Name = "InputBox1";
            this.InputBox1.Size = new System.Drawing.Size(180, 80);
            this.InputBox1.TabIndex = 0;
            // 
            // OKButton1
            // 
            this.OKButton1.Location = new System.Drawing.Point(60, 88);
            this.OKButton1.Name = "OKButton1";
            this.OKButton1.Size = new System.Drawing.Size(75, 20);
            this.OKButton1.TabIndex = 1;
            this.OKButton1.Text = "OK";
            this.OKButton1.UseVisualStyleBackColor = true;
            this.OKButton1.Click += new System.EventHandler(this.OKButton1_Click);
            // 
            // FormInputString1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(190, 112);
            this.Controls.Add(this.OKButton1);
            this.Controls.Add(this.InputBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormInputString1";
            this.Text = "FormInputString1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox InputBox1;
        private System.Windows.Forms.Button OKButton1;
    }
}