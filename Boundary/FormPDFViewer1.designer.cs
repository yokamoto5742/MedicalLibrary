namespace MedicalLibrary.Boundary
{
    partial class FormPDFViewer1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPDFViewer1));
            this.PdfBrowser1 = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // PdfBrowser1
            // 
            this.PdfBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PdfBrowser1.Location = new System.Drawing.Point(5, 5);
            this.PdfBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.PdfBrowser1.Name = "PdfBrowser1";
            this.PdfBrowser1.Size = new System.Drawing.Size(775, 480);
            this.PdfBrowser1.TabIndex = 5;
            // 
            // FormPDFViewer1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 487);
            this.Controls.Add(this.PdfBrowser1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormPDFViewer1";
            this.Text = "PDFビューア";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser PdfBrowser1;
    }
}