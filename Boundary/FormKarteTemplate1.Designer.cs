namespace MedicalLibrary.Boundary
{
    partial class FormKarteTemplate1
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
            this.KarteTemplate1 = new MedicalLibrary.Boundary.CtrlKarteTemplate1();
            this.SuspendLayout();
            // 
            // KarteTemplate1
            // 
            this.KarteTemplate1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.KarteTemplate1.Location = new System.Drawing.Point(0, 0);
            this.KarteTemplate1.Name = "KarteTemplate1";
            this.KarteTemplate1.Size = new System.Drawing.Size(600, 600);
            this.KarteTemplate1.TabIndex = 0;
            // 
            // FormKarteTemplate1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 602);
            this.Controls.Add(this.KarteTemplate1);
            this.Name = "FormKarteTemplate1";
            this.Text = "カルテテンプレート";
            this.ResumeLayout(false);

        }

        #endregion

        private CtrlKarteTemplate1 KarteTemplate1;
    }
}