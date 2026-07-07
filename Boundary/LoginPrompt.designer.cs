namespace MedicalLibrary.Boundary
{
    partial class LoginPrompt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginPrompt));
            this.idBox1 = new System.Windows.Forms.TextBox();
            this.IDLabel1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pwdBox1 = new System.Windows.Forms.TextBox();
            this.loginButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.errLabel = new System.Windows.Forms.Label();
            this.pwdBox2 = new System.Windows.Forms.TextBox();
            this.idBox2 = new System.Windows.Forms.TextBox();
            this.SecondLabel1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // idBox1
            // 
            this.idBox1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.idBox1.Location = new System.Drawing.Point(100, 30);
            this.idBox1.MaxLength = 50;
            this.idBox1.Name = "idBox1";
            this.idBox1.Size = new System.Drawing.Size(92, 19);
            this.idBox1.TabIndex = 0;
            this.idBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.idBox_KeyDown);
            // 
            // IDLabel1
            // 
            this.IDLabel1.Location = new System.Drawing.Point(32, 33);
            this.IDLabel1.Name = "IDLabel1";
            this.IDLabel1.Size = new System.Drawing.Size(50, 12);
            this.IDLabel1.TabIndex = 1;
            this.IDLabel1.Text = "ID";
            this.IDLabel1.DoubleClick += new System.EventHandler(this.IDLabel1_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "パスワード";
            // 
            // pwdBox1
            // 
            this.pwdBox1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.pwdBox1.Location = new System.Drawing.Point(100, 55);
            this.pwdBox1.MaxLength = 50;
            this.pwdBox1.Name = "pwdBox1";
            this.pwdBox1.PasswordChar = '*';
            this.pwdBox1.Size = new System.Drawing.Size(92, 19);
            this.pwdBox1.TabIndex = 1;
            this.pwdBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pwdBox_KeyDown);
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(28, 89);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(74, 23);
            this.loginButton.TabIndex = 4;
            this.loginButton.Text = "ログイン";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(123, 89);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(74, 23);
            this.closeButton.TabIndex = 5;
            this.closeButton.Text = "閉じる";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // errLabel
            // 
            this.errLabel.AutoSize = true;
            this.errLabel.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.errLabel.ForeColor = System.Drawing.Color.Red;
            this.errLabel.Location = new System.Drawing.Point(45, 9);
            this.errLabel.Name = "errLabel";
            this.errLabel.Size = new System.Drawing.Size(0, 12);
            this.errLabel.TabIndex = 6;
            // 
            // pwdBox2
            // 
            this.pwdBox2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.pwdBox2.Location = new System.Drawing.Point(200, 55);
            this.pwdBox2.MaxLength = 50;
            this.pwdBox2.Name = "pwdBox2";
            this.pwdBox2.PasswordChar = '*';
            this.pwdBox2.Size = new System.Drawing.Size(92, 19);
            this.pwdBox2.TabIndex = 3;
            this.pwdBox2.Visible = false;
            // 
            // idBox2
            // 
            this.idBox2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.idBox2.Location = new System.Drawing.Point(200, 30);
            this.idBox2.MaxLength = 50;
            this.idBox2.Name = "idBox2";
            this.idBox2.Size = new System.Drawing.Size(92, 19);
            this.idBox2.TabIndex = 2;
            this.idBox2.Visible = false;
            // 
            // SecondLabel1
            // 
            this.SecondLabel1.AutoSize = true;
            this.SecondLabel1.Location = new System.Drawing.Point(200, 12);
            this.SecondLabel1.Name = "SecondLabel1";
            this.SecondLabel1.Size = new System.Drawing.Size(41, 12);
            this.SecondLabel1.TabIndex = 9;
            this.SecondLabel1.Text = "代行者";
            this.SecondLabel1.Visible = false;
            // 
            // LoginPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 142);
            this.Controls.Add(this.SecondLabel1);
            this.Controls.Add(this.pwdBox2);
            this.Controls.Add(this.idBox2);
            this.Controls.Add(this.errLabel);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pwdBox1);
            this.Controls.Add(this.IDLabel1);
            this.Controls.Add(this.idBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginPrompt";
            this.Text = "MedicalAgent";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox idBox1;
        private System.Windows.Forms.Label IDLabel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pwdBox1;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label errLabel;
        private System.Windows.Forms.TextBox pwdBox2;
        private System.Windows.Forms.TextBox idBox2;
        private System.Windows.Forms.Label SecondLabel1;
    }
}