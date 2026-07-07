namespace MedicalLibrary.Boundary
{
    partial class CtrlSoapPanel1
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.SchemaLabel = new System.Windows.Forms.Label();
            this.KindLabel = new System.Windows.Forms.Label();
            this.ContBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // SchemaLabel
            // 
            this.SchemaLabel.BackColor = System.Drawing.Color.White;
            this.SchemaLabel.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SchemaLabel.Location = new System.Drawing.Point(3, 30);
            this.SchemaLabel.Name = "SchemaLabel";
            this.SchemaLabel.Size = new System.Drawing.Size(20, 20);
            this.SchemaLabel.TabIndex = 21;
            this.SchemaLabel.Tag = "1";
            this.SchemaLabel.Text = "図";
            this.SchemaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KindLabel
            // 
            this.KindLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KindLabel.Location = new System.Drawing.Point(3, 5);
            this.KindLabel.Name = "KindLabel";
            this.KindLabel.Size = new System.Drawing.Size(22, 20);
            this.KindLabel.TabIndex = 20;
            this.KindLabel.Text = "S";
            this.KindLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ContBox
            // 
            this.ContBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContBox.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ContBox.Location = new System.Drawing.Point(25, 2);
            this.ContBox.MaxLength = 1000;
            this.ContBox.Multiline = true;
            this.ContBox.Name = "ContBox";
            this.ContBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ContBox.Size = new System.Drawing.Size(450, 96);
            this.ContBox.TabIndex = 19;
            // 
            // CtrlSoapPanel1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SchemaLabel);
            this.Controls.Add(this.KindLabel);
            this.Controls.Add(this.ContBox);
            this.Name = "CtrlSoapPanel1";
            this.Size = new System.Drawing.Size(480, 100);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SchemaLabel;
        private System.Windows.Forms.Label KindLabel;
        private System.Windows.Forms.TextBox ContBox;
    }
}
