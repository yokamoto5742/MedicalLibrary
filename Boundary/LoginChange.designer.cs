namespace MedicalLibrary.Boundary
{
    partial class LoginChange
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginChange));
            this.label1 = new System.Windows.Forms.Label();
            this.UserLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "ログインユーザーを変更するには\r\n名札のバーコードを読んでください。";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UserLabel
            // 
            this.UserLabel.BackColor = System.Drawing.Color.LightYellow;
            this.UserLabel.Location = new System.Drawing.Point(21, 17);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(223, 17);
            this.UserLabel.TabIndex = 1;
            this.UserLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoginChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 78);
            this.Controls.Add(this.UserLabel);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginChange";
            this.Text = "ログインユーザー";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LoginChange_KeyPress);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LoginChange_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label UserLabel;
    }
}