namespace MedicalLibrary.Agent
{
    partial class CanonRKF1Form
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CanonRKF1Form));
            this.PortBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.FileButton = new System.Windows.Forms.Button();
            this.RsvBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // PortBox
            // 
            this.PortBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PortBox.FormattingEnabled = true;
            this.PortBox.Location = new System.Drawing.Point(68, 35);
            this.PortBox.Name = "PortBox";
            this.PortBox.Size = new System.Drawing.Size(75, 20);
            this.PortBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "COMポート";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(147, 34);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(75, 23);
            this.ConnectButton.TabIndex = 2;
            this.ConnectButton.Text = "通信接続";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CloseButton.Location = new System.Drawing.Point(200, 548);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(60, 23);
            this.CloseButton.TabIndex = 3;
            this.CloseButton.Text = "終了";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // FileButton
            // 
            this.FileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FileButton.Location = new System.Drawing.Point(85, 548);
            this.FileButton.Name = "FileButton";
            this.FileButton.Size = new System.Drawing.Size(85, 23);
            this.FileButton.TabIndex = 4;
            this.FileButton.Text = "ファイル出力";
            this.FileButton.UseVisualStyleBackColor = true;
            this.FileButton.Click += new System.EventHandler(this.FileButton_Click);
            // 
            // RsvBox
            // 
            this.RsvBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RsvBox.BackColor = System.Drawing.Color.White;
            this.RsvBox.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RsvBox.Location = new System.Drawing.Point(11, 61);
            this.RsvBox.Multiline = true;
            this.RsvBox.Name = "RsvBox";
            this.RsvBox.ReadOnly = true;
            this.RsvBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.RsvBox.Size = new System.Drawing.Size(270, 347);
            this.RsvBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(22, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(248, 22);
            this.label2.TabIndex = 6;
            this.label2.Text = "起動直後に「通信接続」ボタンを押してください。";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.Location = new System.Drawing.Point(12, 412);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(268, 133);
            this.label3.TabIndex = 7;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // StatusLabel
            // 
            this.StatusLabel.BackColor = System.Drawing.Color.White;
            this.StatusLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.StatusLabel.Location = new System.Drawing.Point(225, 34);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(55, 22);
            this.StatusLabel.TabIndex = 8;
            this.StatusLabel.Text = "切断";
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CanonRKF1Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 573);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RsvBox);
            this.Controls.Add(this.FileButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PortBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CanonRKF1Form";
            this.Text = "Canon RF-K1 通信プログラム";
            this.Load += new System.EventHandler(this.CanonRFK1Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox PortBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button FileButton;
        private System.Windows.Forms.TextBox RsvBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Timer timer1;
    }
}

