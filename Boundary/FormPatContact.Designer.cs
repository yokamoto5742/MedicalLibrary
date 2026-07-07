namespace MedicalLibrary.Boundary
{
    partial class FormPatContact
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.stdControlPat11 = new MedicalLibrary.Boundary.StdControlPat1();
            this.ListView1 = new System.Windows.Forms.DataGridView();
            this.UpButton1 = new System.Windows.Forms.Button();
            this.DownButton1 = new System.Windows.Forms.Button();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.KanaBox = new System.Windows.Forms.TextBox();
            this.BirthBox = new MedicalLibrary.Boundary.CtrlDateBox1();
            this.label4 = new System.Windows.Forms.Label();
            this.RelationBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.RelationCommentBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.KindBox1 = new System.Windows.Forms.ComboBox();
            this.TelBox1 = new System.Windows.Forms.TextBox();
            this.TelBox2 = new System.Windows.Forms.TextBox();
            this.KindBox2 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TelBox3 = new System.Windows.Forms.TextBox();
            this.KindBox3 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.HealthBox = new System.Windows.Forms.TextBox();
            this.ResidentBox = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.CareBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.ContBox = new System.Windows.Forms.TextBox();
            this.ShowSEQBox = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.StaffLabel = new System.Windows.Forms.Label();
            this.DateTimeLabel = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // stdControlPat11
            // 
            this.stdControlPat11.Location = new System.Drawing.Point(2, 2);
            this.stdControlPat11.Mode1 = MedicalLibrary.Boundary.StdControlPat1.Mode.Normal;
            this.stdControlPat11.Name = "stdControlPat11";
            this.stdControlPat11.ReadOnly = false;
            this.stdControlPat11.Size = new System.Drawing.Size(450, 30);
            this.stdControlPat11.TabIndex = 0;
            // 
            // ListView1
            // 
            this.ListView1.AllowUserToAddRows = false;
            this.ListView1.AllowUserToDeleteRows = false;
            this.ListView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ListView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ListView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListView1.Location = new System.Drawing.Point(5, 40);
            this.ListView1.MultiSelect = false;
            this.ListView1.Name = "ListView1";
            this.ListView1.ReadOnly = true;
            this.ListView1.RowHeadersVisible = false;
            this.ListView1.RowTemplate.Height = 21;
            this.ListView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ListView1.Size = new System.Drawing.Size(535, 150);
            this.ListView1.TabIndex = 1;
            this.ListView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListView1_CellClick);
            // 
            // UpButton1
            // 
            this.UpButton1.Location = new System.Drawing.Point(460, 10);
            this.UpButton1.Name = "UpButton1";
            this.UpButton1.Size = new System.Drawing.Size(30, 23);
            this.UpButton1.TabIndex = 2;
            this.UpButton1.Text = "▲";
            this.UpButton1.UseVisualStyleBackColor = true;
            this.UpButton1.Click += new System.EventHandler(this.UpButton1_Click);
            // 
            // DownButton1
            // 
            this.DownButton1.Location = new System.Drawing.Point(495, 10);
            this.DownButton1.Name = "DownButton1";
            this.DownButton1.Size = new System.Drawing.Size(30, 23);
            this.DownButton1.TabIndex = 4;
            this.DownButton1.Text = "▼";
            this.DownButton1.UseVisualStyleBackColor = true;
            this.DownButton1.Click += new System.EventHandler(this.DownButton1_Click);
            // 
            // NameBox
            // 
            this.NameBox.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.NameBox.Location = new System.Drawing.Point(80, 225);
            this.NameBox.MaxLength = 20;
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(100, 19);
            this.NameBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "氏名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 254);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "カナ";
            // 
            // KanaBox
            // 
            this.KanaBox.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.KanaBox.Location = new System.Drawing.Point(80, 250);
            this.KanaBox.MaxLength = 20;
            this.KanaBox.Name = "KanaBox";
            this.KanaBox.Size = new System.Drawing.Size(100, 19);
            this.KanaBox.TabIndex = 7;
            // 
            // BirthBox
            // 
            this.BirthBox.DateInt = 0;
            this.BirthBox.DateString = "";
            this.BirthBox.Location = new System.Drawing.Point(78, 273);
            this.BirthBox.Name = "BirthBox";
            this.BirthBox.Size = new System.Drawing.Size(150, 26);
            this.BirthBox.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 279);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "生年月日";
            // 
            // RelationBox
            // 
            this.RelationBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RelationBox.FormattingEnabled = true;
            this.RelationBox.Location = new System.Drawing.Point(80, 300);
            this.RelationBox.Name = "RelationBox";
            this.RelationBox.Size = new System.Drawing.Size(100, 20);
            this.RelationBox.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 304);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "続柄";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 329);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "続柄コメント";
            // 
            // RelationCommentBox
            // 
            this.RelationCommentBox.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.RelationCommentBox.Location = new System.Drawing.Point(80, 325);
            this.RelationCommentBox.MaxLength = 10;
            this.RelationCommentBox.Name = "RelationCommentBox";
            this.RelationCommentBox.Size = new System.Drawing.Size(120, 19);
            this.RelationCommentBox.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(260, 204);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "連絡先電話1";
            // 
            // KindBox1
            // 
            this.KindBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.KindBox1.FormattingEnabled = true;
            this.KindBox1.Location = new System.Drawing.Point(450, 200);
            this.KindBox1.Name = "KindBox1";
            this.KindBox1.Size = new System.Drawing.Size(80, 20);
            this.KindBox1.TabIndex = 16;
            // 
            // TelBox1
            // 
            this.TelBox1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TelBox1.Location = new System.Drawing.Point(340, 200);
            this.TelBox1.MaxLength = 13;
            this.TelBox1.Name = "TelBox1";
            this.TelBox1.Size = new System.Drawing.Size(100, 19);
            this.TelBox1.TabIndex = 17;
            // 
            // TelBox2
            // 
            this.TelBox2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TelBox2.Location = new System.Drawing.Point(340, 225);
            this.TelBox2.MaxLength = 13;
            this.TelBox2.Name = "TelBox2";
            this.TelBox2.Size = new System.Drawing.Size(100, 19);
            this.TelBox2.TabIndex = 20;
            // 
            // KindBox2
            // 
            this.KindBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.KindBox2.FormattingEnabled = true;
            this.KindBox2.Location = new System.Drawing.Point(450, 225);
            this.KindBox2.Name = "KindBox2";
            this.KindBox2.Size = new System.Drawing.Size(80, 20);
            this.KindBox2.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(260, 229);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 12);
            this.label8.TabIndex = 18;
            this.label8.Text = "連絡先電話2";
            // 
            // TelBox3
            // 
            this.TelBox3.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TelBox3.Location = new System.Drawing.Point(340, 250);
            this.TelBox3.MaxLength = 13;
            this.TelBox3.Name = "TelBox3";
            this.TelBox3.Size = new System.Drawing.Size(100, 19);
            this.TelBox3.TabIndex = 23;
            // 
            // KindBox3
            // 
            this.KindBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.KindBox3.FormattingEnabled = true;
            this.KindBox3.Location = new System.Drawing.Point(450, 250);
            this.KindBox3.Name = "KindBox3";
            this.KindBox3.Size = new System.Drawing.Size(80, 20);
            this.KindBox3.TabIndex = 22;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(260, 254);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 12);
            this.label9.TabIndex = 21;
            this.label9.Text = "連絡先電話3";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(260, 279);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 25;
            this.label10.Text = "健康状態";
            // 
            // HealthBox
            // 
            this.HealthBox.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.HealthBox.Location = new System.Drawing.Point(340, 275);
            this.HealthBox.MaxLength = 60;
            this.HealthBox.Name = "HealthBox";
            this.HealthBox.Size = new System.Drawing.Size(190, 19);
            this.HealthBox.TabIndex = 24;
            // 
            // ResidentBox
            // 
            this.ResidentBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ResidentBox.FormattingEnabled = true;
            this.ResidentBox.Location = new System.Drawing.Point(80, 350);
            this.ResidentBox.Name = "ResidentBox";
            this.ResidentBox.Size = new System.Drawing.Size(80, 20);
            this.ResidentBox.TabIndex = 27;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 354);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 26;
            this.label11.Text = "同別居";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(260, 304);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 29;
            this.label12.Text = "介護役割";
            // 
            // CareBox
            // 
            this.CareBox.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.CareBox.Location = new System.Drawing.Point(340, 300);
            this.CareBox.MaxLength = 60;
            this.CareBox.Name = "CareBox";
            this.CareBox.Size = new System.Drawing.Size(190, 19);
            this.CareBox.TabIndex = 28;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(260, 329);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 12);
            this.label13.TabIndex = 31;
            this.label13.Text = "備考";
            // 
            // ContBox
            // 
            this.ContBox.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.ContBox.Location = new System.Drawing.Point(340, 325);
            this.ContBox.MaxLength = 60;
            this.ContBox.Multiline = true;
            this.ContBox.Name = "ContBox";
            this.ContBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ContBox.Size = new System.Drawing.Size(190, 45);
            this.ContBox.TabIndex = 30;
            // 
            // ShowSEQBox
            // 
            this.ShowSEQBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ShowSEQBox.FormattingEnabled = true;
            this.ShowSEQBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ShowSEQBox.Location = new System.Drawing.Point(80, 200);
            this.ShowSEQBox.Name = "ShowSEQBox";
            this.ShowSEQBox.Size = new System.Drawing.Size(50, 20);
            this.ShowSEQBox.TabIndex = 32;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 204);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 33;
            this.label14.Text = "優先順位";
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(340, 385);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 34;
            this.SaveButton.Text = "登録";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // StaffLabel
            // 
            this.StaffLabel.BackColor = System.Drawing.SystemColors.Info;
            this.StaffLabel.Location = new System.Drawing.Point(55, 390);
            this.StaffLabel.Name = "StaffLabel";
            this.StaffLabel.Size = new System.Drawing.Size(100, 16);
            this.StaffLabel.TabIndex = 35;
            this.StaffLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DateTimeLabel
            // 
            this.DateTimeLabel.BackColor = System.Drawing.SystemColors.Info;
            this.DateTimeLabel.Location = new System.Drawing.Point(230, 390);
            this.DateTimeLabel.Name = "DateTimeLabel";
            this.DateTimeLabel.Size = new System.Drawing.Size(100, 16);
            this.DateTimeLabel.TabIndex = 36;
            this.DateTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(10, 392);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 12);
            this.label15.TabIndex = 37;
            this.label15.Text = "登録者";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(170, 392);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.TabIndex = 38;
            this.label16.Text = "登録日時";
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(475, 385);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(50, 23);
            this.DeleteButton.TabIndex = 39;
            this.DeleteButton.Text = "削除";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(420, 385);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(50, 23);
            this.ClearButton.TabIndex = 40;
            this.ClearButton.Text = "クリア";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // FormPatContact
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 412);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.DateTimeLabel);
            this.Controls.Add(this.StaffLabel);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.ShowSEQBox);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.ContBox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.CareBox);
            this.Controls.Add(this.ResidentBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.HealthBox);
            this.Controls.Add(this.TelBox3);
            this.Controls.Add(this.KindBox3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.TelBox2);
            this.Controls.Add(this.KindBox2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.TelBox1);
            this.Controls.Add(this.KindBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.RelationCommentBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.RelationBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.BirthBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.KanaBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NameBox);
            this.Controls.Add(this.DownButton1);
            this.Controls.Add(this.UpButton1);
            this.Controls.Add(this.ListView1);
            this.Controls.Add(this.stdControlPat11);
            this.Name = "FormPatContact";
            this.Text = "連絡先";
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StdControlPat1 stdControlPat11;
        private System.Windows.Forms.DataGridView ListView1;
        private System.Windows.Forms.Button UpButton1;
        private System.Windows.Forms.Button DownButton1;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox KanaBox;
        private CtrlDateBox1 BirthBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox RelationBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox RelationCommentBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox KindBox1;
        private System.Windows.Forms.TextBox TelBox1;
        private System.Windows.Forms.TextBox TelBox2;
        private System.Windows.Forms.ComboBox KindBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TelBox3;
        private System.Windows.Forms.ComboBox KindBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox HealthBox;
        private System.Windows.Forms.ComboBox ResidentBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox CareBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox ContBox;
        private System.Windows.Forms.ComboBox ShowSEQBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Label StaffLabel;
        private System.Windows.Forms.Label DateTimeLabel;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button ClearButton;
    }
}