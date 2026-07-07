namespace MedicalLibrary.Boundary
{
    partial class StdControlPat1
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
            this.PatNameLabel1 = new System.Windows.Forms.Label();
            this.PatIdBox1 = new System.Windows.Forms.TextBox();
            this.PatSexLabel1 = new System.Windows.Forms.Label();
            this.PatBirthLabel1 = new System.Windows.Forms.Label();
            this.PatAgeLabel1 = new System.Windows.Forms.Label();
            this.PatKanaLabel1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PatNameLabel1
            // 
            this.PatNameLabel1.AutoEllipsis = true;
            this.PatNameLabel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PatNameLabel1.Location = new System.Drawing.Point(75, 5);
            this.PatNameLabel1.Name = "PatNameLabel1";
            this.PatNameLabel1.Size = new System.Drawing.Size(104, 23);
            this.PatNameLabel1.TabIndex = 1;
            this.PatNameLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PatIdBox1
            // 
            this.PatIdBox1.BackColor = System.Drawing.Color.White;
            this.PatIdBox1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.PatIdBox1.Location = new System.Drawing.Point(2, 7);
            this.PatIdBox1.MaxLength = 9;
            this.PatIdBox1.Name = "PatIdBox1";
            this.PatIdBox1.Size = new System.Drawing.Size(70, 19);
            this.PatIdBox1.TabIndex = 2;
            this.PatIdBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PatIdBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PatIdBox1_KeyDown);
            // 
            // PatSexLabel1
            // 
            this.PatSexLabel1.AutoEllipsis = true;
            this.PatSexLabel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PatSexLabel1.Location = new System.Drawing.Point(261, 5);
            this.PatSexLabel1.Name = "PatSexLabel1";
            this.PatSexLabel1.Size = new System.Drawing.Size(23, 23);
            this.PatSexLabel1.TabIndex = 3;
            this.PatSexLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PatBirthLabel1
            // 
            this.PatBirthLabel1.AutoEllipsis = true;
            this.PatBirthLabel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PatBirthLabel1.Location = new System.Drawing.Point(285, 5);
            this.PatBirthLabel1.Name = "PatBirthLabel1";
            this.PatBirthLabel1.Size = new System.Drawing.Size(120, 23);
            this.PatBirthLabel1.TabIndex = 4;
            this.PatBirthLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PatAgeLabel1
            // 
            this.PatAgeLabel1.AutoEllipsis = true;
            this.PatAgeLabel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PatAgeLabel1.Location = new System.Drawing.Point(406, 5);
            this.PatAgeLabel1.Name = "PatAgeLabel1";
            this.PatAgeLabel1.Size = new System.Drawing.Size(40, 23);
            this.PatAgeLabel1.TabIndex = 5;
            this.PatAgeLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PatKanaLabel1
            // 
            this.PatKanaLabel1.AutoEllipsis = true;
            this.PatKanaLabel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PatKanaLabel1.Location = new System.Drawing.Point(180, 5);
            this.PatKanaLabel1.Name = "PatKanaLabel1";
            this.PatKanaLabel1.Size = new System.Drawing.Size(80, 23);
            this.PatKanaLabel1.TabIndex = 6;
            this.PatKanaLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StdControlPat1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PatKanaLabel1);
            this.Controls.Add(this.PatAgeLabel1);
            this.Controls.Add(this.PatBirthLabel1);
            this.Controls.Add(this.PatSexLabel1);
            this.Controls.Add(this.PatIdBox1);
            this.Controls.Add(this.PatNameLabel1);
            this.Name = "StdControlPat1";
            this.Size = new System.Drawing.Size(450, 30);
            this.Load += new System.EventHandler(this.StdControlPat1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PatNameLabel1;
        private System.Windows.Forms.TextBox PatIdBox1;
        private System.Windows.Forms.Label PatSexLabel1;
        private System.Windows.Forms.Label PatBirthLabel1;
        private System.Windows.Forms.Label PatAgeLabel1;
        private System.Windows.Forms.Label PatKanaLabel1;

    }
}
