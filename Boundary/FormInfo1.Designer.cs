namespace MedicalLibrary.Boundary
{
    partial class FormInfo1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInfo1));
            this.ContBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ContBox1
            // 
            this.ContBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContBox1.BackColor = System.Drawing.Color.White;
            this.ContBox1.Location = new System.Drawing.Point(5, 5);
            this.ContBox1.Multiline = true;
            this.ContBox1.Name = "ContBox1";
            this.ContBox1.ReadOnly = true;
            this.ContBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ContBox1.Size = new System.Drawing.Size(455, 250);
            this.ContBox1.TabIndex = 0;
            // 
            // FormInfo1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 262);
            this.Controls.Add(this.ContBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormInfo1";
            this.Text = "情報";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ContBox1;
    }
}