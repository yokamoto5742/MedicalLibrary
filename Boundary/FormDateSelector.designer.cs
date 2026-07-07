namespace MedicalLibrary.Boundary
{
    partial class FormDateSelector
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
            this.Calendar1 = new System.Windows.Forms.MonthCalendar();
            this.SelectButton1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Calendar1
            // 
            this.Calendar1.Location = new System.Drawing.Point(6, 7);
            this.Calendar1.MaxSelectionCount = 1;
            this.Calendar1.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.Calendar1.Name = "Calendar1";
            this.Calendar1.TabIndex = 0;
            // 
            // SelectButton1
            // 
            this.SelectButton1.Location = new System.Drawing.Point(15, 200);
            this.SelectButton1.Name = "SelectButton1";
            this.SelectButton1.Size = new System.Drawing.Size(200, 23);
            this.SelectButton1.TabIndex = 1;
            this.SelectButton1.Text = "選択";
            this.SelectButton1.UseVisualStyleBackColor = true;
            this.SelectButton1.Click += new System.EventHandler(this.SelectButton1_Click);
            // 
            // FormDateSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 232);
            this.Controls.Add(this.SelectButton1);
            this.Controls.Add(this.Calendar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormDateSelector";
            this.Text = "日付選択";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MonthCalendar Calendar1;
        private System.Windows.Forms.Button SelectButton1;
    }
}