namespace MedicalLibrary.Boundary
{
    partial class FormKarteMessage2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormKarteMessage2));
            this.FormAddressGroupButton1 = new System.Windows.Forms.Button();
            this.SendToAddButton2 = new System.Windows.Forms.Button();
            this.AddressGroupBox2 = new System.Windows.Forms.CheckedListBox();
            this.KarteButton2 = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PriorityBox2 = new System.Windows.Forms.ComboBox();
            this.TitleBox2 = new System.Windows.Forms.TextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.ContBox2 = new System.Windows.Forms.TextBox();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.StaffNameBox = new System.Windows.Forms.TextBox();
            this.stdControlPat11 = new MedicalLibrary.Boundary.StdControlPat1();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FormAddressGroupButton1
            // 
            this.FormAddressGroupButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FormAddressGroupButton1.Location = new System.Drawing.Point(5, 185);
            this.FormAddressGroupButton1.Name = "FormAddressGroupButton1";
            this.FormAddressGroupButton1.Size = new System.Drawing.Size(50, 35);
            this.FormAddressGroupButton1.TabIndex = 91;
            this.FormAddressGroupButton1.Text = "アドレス帳";
            this.FormAddressGroupButton1.UseVisualStyleBackColor = true;
            this.FormAddressGroupButton1.Click += new System.EventHandler(this.FormAddressGroupButton1_Click);
            // 
            // SendToAddButton2
            // 
            this.SendToAddButton2.Location = new System.Drawing.Point(10, 65);
            this.SendToAddButton2.Name = "SendToAddButton2";
            this.SendToAddButton2.Size = new System.Drawing.Size(40, 22);
            this.SendToAddButton2.TabIndex = 90;
            this.SendToAddButton2.Text = "追加";
            this.SendToAddButton2.UseVisualStyleBackColor = true;
            this.SendToAddButton2.Click += new System.EventHandler(this.SendToAddButton2_Click);
            // 
            // AddressGroupBox2
            // 
            this.AddressGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddressGroupBox2.CheckOnClick = true;
            this.AddressGroupBox2.FormattingEnabled = true;
            this.AddressGroupBox2.Location = new System.Drawing.Point(60, 180);
            this.AddressGroupBox2.Name = "AddressGroupBox2";
            this.AddressGroupBox2.Size = new System.Drawing.Size(165, 74);
            this.AddressGroupBox2.TabIndex = 89;
            // 
            // KarteButton2
            // 
            this.KarteButton2.Location = new System.Drawing.Point(500, 7);
            this.KarteButton2.Name = "KarteButton2";
            this.KarteButton2.Size = new System.Drawing.Size(60, 24);
            this.KarteButton2.TabIndex = 87;
            this.KarteButton2.Text = "カルテ";
            this.KarteButton2.UseVisualStyleBackColor = true;
            this.KarteButton2.Click += new System.EventHandler(this.KarteButton2_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearButton.Location = new System.Drawing.Point(715, 235);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(60, 24);
            this.ClearButton.TabIndex = 86;
            this.ClearButton.Text = "クリア";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(230, 242);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(323, 12);
            this.label9.TabIndex = 85;
            this.label9.Text = "※患者IDや送信先IDを検索するには F3 キーを押してください。";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 82;
            this.label5.Text = "送信先";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(690, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 83;
            this.label8.Text = "重要度";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(230, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 80;
            this.label3.Text = "件名";
            // 
            // PriorityBox2
            // 
            this.PriorityBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PriorityBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PriorityBox2.FormattingEnabled = true;
            this.PriorityBox2.Items.AddRange(new object[] {
            "中",
            "高",
            "低"});
            this.PriorityBox2.Location = new System.Drawing.Point(735, 36);
            this.PriorityBox2.Name = "PriorityBox2";
            this.PriorityBox2.Size = new System.Drawing.Size(40, 20);
            this.PriorityBox2.TabIndex = 77;
            // 
            // TitleBox2
            // 
            this.TitleBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TitleBox2.BackColor = System.Drawing.SystemColors.Window;
            this.TitleBox2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TitleBox2.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TitleBox2.Location = new System.Drawing.Point(265, 37);
            this.TitleBox2.MaxLength = 32;
            this.TitleBox2.Name = "TitleBox2";
            this.TitleBox2.Size = new System.Drawing.Size(400, 19);
            this.TitleBox2.TabIndex = 75;
            this.TitleBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TitleBox2_KeyDown);
            // 
            // SendButton
            // 
            this.SendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SendButton.Location = new System.Drawing.Point(610, 235);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(80, 24);
            this.SendButton.TabIndex = 79;
            this.SendButton.Text = "送信";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // ContBox2
            // 
            this.ContBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContBox2.BackColor = System.Drawing.SystemColors.Window;
            this.ContBox2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ContBox2.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.ContBox2.Location = new System.Drawing.Point(230, 60);
            this.ContBox2.MaxLength = 1000;
            this.ContBox2.Multiline = true;
            this.ContBox2.Name = "ContBox2";
            this.ContBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ContBox2.Size = new System.Drawing.Size(550, 170);
            this.ContBox2.TabIndex = 78;
            // 
            // Panel2
            // 
            this.Panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Panel2.AutoScroll = true;
            this.Panel2.Location = new System.Drawing.Point(55, 40);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(170, 135);
            this.Panel2.TabIndex = 88;
            // 
            // StaffNameBox
            // 
            this.StaffNameBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StaffNameBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.StaffNameBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.StaffNameBox.Location = new System.Drawing.Point(680, 10);
            this.StaffNameBox.Name = "StaffNameBox";
            this.StaffNameBox.ReadOnly = true;
            this.StaffNameBox.Size = new System.Drawing.Size(100, 19);
            this.StaffNameBox.TabIndex = 92;
            this.StaffNameBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // stdControlPat11
            // 
            this.stdControlPat11.Location = new System.Drawing.Point(5, 5);
            this.stdControlPat11.Mode1 = MedicalLibrary.Boundary.StdControlPat1.Mode.Normal;
            this.stdControlPat11.Name = "stdControlPat11";
            this.stdControlPat11.ReadOnly = true;
            this.stdControlPat11.Size = new System.Drawing.Size(450, 30);
            this.stdControlPat11.TabIndex = 93;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(630, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 94;
            this.label1.Text = "ユーザー";
            // 
            // FormKarteMessage2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 262);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stdControlPat11);
            this.Controls.Add(this.StaffNameBox);
            this.Controls.Add(this.FormAddressGroupButton1);
            this.Controls.Add(this.SendToAddButton2);
            this.Controls.Add(this.AddressGroupBox2);
            this.Controls.Add(this.KarteButton2);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PriorityBox2);
            this.Controls.Add(this.TitleBox2);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.ContBox2);
            this.Controls.Add(this.Panel2);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormKarteMessage2";
            this.Text = "カルテメッセージ作成";
            this.Load += new System.EventHandler(this.FormKarteMessage2_Load);
            this.Shown += new System.EventHandler(this.FormKarteMessage2_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FormAddressGroupButton1;
        private System.Windows.Forms.Button SendToAddButton2;
        private System.Windows.Forms.CheckedListBox AddressGroupBox2;
        private System.Windows.Forms.Button KarteButton2;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox PriorityBox2;
        private System.Windows.Forms.TextBox TitleBox2;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.TextBox ContBox2;
        private System.Windows.Forms.Panel Panel2;
        private System.Windows.Forms.TextBox StaffNameBox;
        private StdControlPat1 stdControlPat11;
        private System.Windows.Forms.Label label1;
    }
}