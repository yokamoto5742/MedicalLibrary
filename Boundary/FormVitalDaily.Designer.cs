namespace MedicalLibrary.Boundary
{
    partial class FormVitalDaily
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
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.DatePicker1 = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // TabControl1
            // 
            this.TabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl1.Location = new System.Drawing.Point(5, 35);
            this.TabControl1.Multiline = true;
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(475, 280);
            this.TabControl1.TabIndex = 0;
            this.TabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControl1_SelectedIndexChanged);
            // 
            // DatePicker1
            // 
            this.DatePicker1.CustomFormat = "yyyy年M月d日 (ddd)";
            this.DatePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatePicker1.Location = new System.Drawing.Point(10, 10);
            this.DatePicker1.Name = "DatePicker1";
            this.DatePicker1.Size = new System.Drawing.Size(150, 19);
            this.DatePicker1.TabIndex = 1;
            this.DatePicker1.ValueChanged += new System.EventHandler(this.DatePicker1_ValueChanged);
            // 
            // FormVitalDaily
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 322);
            this.Controls.Add(this.DatePicker1);
            this.Controls.Add(this.TabControl1);
            this.Name = "FormVitalDaily";
            this.Text = "バイタル";
            this.Load += new System.EventHandler(this.FormVitalDaily_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl1;
        private System.Windows.Forms.DateTimePicker DatePicker1;
    }
}