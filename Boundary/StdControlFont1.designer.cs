namespace MedicalLibrary.Boundary
{
    partial class StdControlFont1
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
            this.FontBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // FontBox1
            // 
            this.FontBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FontBox1.FormattingEnabled = true;
            this.FontBox1.Location = new System.Drawing.Point(5, 5);
            this.FontBox1.Name = "FontBox1";
            this.FontBox1.Size = new System.Drawing.Size(50, 20);
            this.FontBox1.TabIndex = 3;
            this.FontBox1.SelectedIndexChanged += new System.EventHandler(this.FontBox1_SelectedIndexChanged);
            // 
            // FontControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FontBox1);
            this.Name = "FontControl1";
            this.Size = new System.Drawing.Size(60, 30);
            this.Load += new System.EventHandler(this.StdControlFont1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox FontBox1;
    }
}
