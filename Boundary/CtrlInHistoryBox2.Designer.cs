namespace MedicalLibrary.Boundary
{
    partial class CtrlInHistoryBox2
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
            this.SelectButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.InTermBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // SelectButton
            // 
            this.SelectButton.Location = new System.Drawing.Point(155, 1);
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(40, 23);
            this.SelectButton.TabIndex = 34;
            this.SelectButton.Text = "選択";
            this.SelectButton.UseVisualStyleBackColor = true;
            this.SelectButton.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(195, 1);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(35, 23);
            this.ClearButton.TabIndex = 35;
            this.ClearButton.Text = "ｸﾘｱ";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // InTermBox
            // 
            this.InTermBox.BackColor = System.Drawing.Color.White;
            this.InTermBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.InTermBox.Location = new System.Drawing.Point(3, 3);
            this.InTermBox.MaxLength = 25;
            this.InTermBox.Name = "InTermBox";
            this.InTermBox.ReadOnly = true;
            this.InTermBox.Size = new System.Drawing.Size(150, 19);
            this.InTermBox.TabIndex = 36;
            this.InTermBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InTermBox_KeyDown);
            // 
            // CtrlInHistoryBox2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.InTermBox);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.SelectButton);
            this.Name = "CtrlInHistoryBox2";
            this.Size = new System.Drawing.Size(235, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SelectButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.TextBox InTermBox;
    }
}
