namespace MedicalLibrary.Boundary
{
    partial class CtrlDateBox1
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
            this.DateBox1 = new System.Windows.Forms.TextBox();
            this.SelectButton1 = new System.Windows.Forms.Button();
            this.ClearButton1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DateBox1
            // 
            this.DateBox1.BackColor = System.Drawing.Color.White;
            this.DateBox1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.DateBox1.Location = new System.Drawing.Point(3, 3);
            this.DateBox1.MaxLength = 10;
            this.DateBox1.Name = "DateBox1";
            this.DateBox1.Size = new System.Drawing.Size(75, 19);
            this.DateBox1.TabIndex = 0;
            this.DateBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DateBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DateBox1_KeyDown);
            this.DateBox1.Leave += new System.EventHandler(this.DateBox1_Leave);
            // 
            // SelectButton1
            // 
            this.SelectButton1.Location = new System.Drawing.Point(80, 2);
            this.SelectButton1.Name = "SelectButton1";
            this.SelectButton1.Size = new System.Drawing.Size(38, 22);
            this.SelectButton1.TabIndex = 1;
            this.SelectButton1.Text = "選択";
            this.SelectButton1.UseVisualStyleBackColor = true;
            this.SelectButton1.Click += new System.EventHandler(this.SelectButton1_Click);
            // 
            // ClearButton1
            // 
            this.ClearButton1.Location = new System.Drawing.Point(118, 2);
            this.ClearButton1.Name = "ClearButton1";
            this.ClearButton1.Size = new System.Drawing.Size(32, 22);
            this.ClearButton1.TabIndex = 2;
            this.ClearButton1.Text = "ｸﾘｱ";
            this.ClearButton1.UseVisualStyleBackColor = true;
            this.ClearButton1.Click += new System.EventHandler(this.ClearButton1_Click);
            // 
            // CtrlDateBox1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ClearButton1);
            this.Controls.Add(this.SelectButton1);
            this.Controls.Add(this.DateBox1);
            this.Name = "CtrlDateBox1";
            this.Size = new System.Drawing.Size(150, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox DateBox1;
        private System.Windows.Forms.Button SelectButton1;
        private System.Windows.Forms.Button ClearButton1;
    }
}
