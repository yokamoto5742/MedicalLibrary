namespace MedicalLibrary.Agent
{
    partial class FormComeReportSchema
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormComeReportSchema));
            this.applyButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(50, 346);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 11;
            this.applyButton.Text = "適用";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(253, 346);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 12;
            this.closeButton.Text = "閉じる";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(152, 346);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 14;
            this.clearButton.Text = "クリア";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(358, 328);
            this.tabControl1.TabIndex = 15;
            // 
            // FormSchema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 378);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.applyButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSchema";
            this.Text = "シェーマ";
            this.Load += new System.EventHandler(this.FormComeReportSchema_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.TabControl tabControl1;
    }
}