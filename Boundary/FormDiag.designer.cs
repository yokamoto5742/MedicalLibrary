namespace MedicalLibrary.Boundary
{
    partial class FormDiag
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDiag));
            this.ICDBox2 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.ICDBox1 = new System.Windows.Forms.TextBox();
            this.SEQLabel = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.OutcomeBox1 = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.DoctorBox1 = new MedicalLibrary.Boundary.CtrlDoctorBox1();
            this.label10 = new System.Windows.Forms.Label();
            this.InsBox1 = new System.Windows.Forms.ComboBox();
            this.DateBox2 = new MedicalLibrary.Boundary.CtrlDateBox1();
            this.label9 = new System.Windows.Forms.Label();
            this.DeleteButton1 = new System.Windows.Forms.Button();
            this.ClearButton1 = new System.Windows.Forms.Button();
            this.SaveButton1 = new System.Windows.Forms.Button();
            this.ModeLabel = new System.Windows.Forms.Label();
            this.DPCButton1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.DatePicker1 = new System.Windows.Forms.DateTimePicker();
            this.InOutBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.NoticeFlgBox1 = new System.Windows.Forms.CheckBox();
            this.MainFlgBox1 = new System.Windows.Forms.CheckBox();
            this.DeptBox1 = new MedicalLibrary.Boundary.CtrlDeptBox1();
            this.DiagCheckBox2 = new System.Windows.Forms.CheckBox();
            this.DiagCheckBox1 = new System.Windows.Forms.CheckBox();
            this.ListView1 = new MedicalLibrary.Boundary.CtrlDiagGridView1();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuffixCodeBox1 = new System.Windows.Forms.TextBox();
            this.PrefixCodeBox1 = new System.Windows.Forms.TextBox();
            this.DiagCodeBox1 = new System.Windows.Forms.TextBox();
            this.DiagNameBox1 = new System.Windows.Forms.TextBox();
            this.DoubtFlgBox1 = new System.Windows.Forms.CheckBox();
            this.InsFlgBox1 = new System.Windows.Forms.CheckBox();
            this.DiagFindPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.DiagFindBox1 = new System.Windows.Forms.TextBox();
            this.stdControlPat11 = new MedicalLibrary.Boundary.StdControlPat1();
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ICDBox2
            // 
            this.ICDBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ICDBox2.Location = new System.Drawing.Point(330, 85);
            this.ICDBox2.Name = "ICDBox2";
            this.ICDBox2.ReadOnly = true;
            this.ICDBox2.Size = new System.Drawing.Size(45, 19);
            this.ICDBox2.TabIndex = 62;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(280, 70);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(36, 12);
            this.label13.TabIndex = 61;
            this.label13.Text = "ICD10";
            // 
            // ICDBox1
            // 
            this.ICDBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ICDBox1.Location = new System.Drawing.Point(280, 85);
            this.ICDBox1.Name = "ICDBox1";
            this.ICDBox1.ReadOnly = true;
            this.ICDBox1.Size = new System.Drawing.Size(45, 19);
            this.ICDBox1.TabIndex = 60;
            // 
            // SEQLabel
            // 
            this.SEQLabel.BackColor = System.Drawing.Color.White;
            this.SEQLabel.Location = new System.Drawing.Point(475, 14);
            this.SEQLabel.Name = "SEQLabel";
            this.SEQLabel.Size = new System.Drawing.Size(30, 16);
            this.SEQLabel.TabIndex = 59;
            this.SEQLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(220, 199);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 12);
            this.label12.TabIndex = 58;
            this.label12.Text = "転帰";
            // 
            // OutcomeBox1
            // 
            this.OutcomeBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OutcomeBox1.FormattingEnabled = true;
            this.OutcomeBox1.Location = new System.Drawing.Point(255, 195);
            this.OutcomeBox1.Name = "OutcomeBox1";
            this.OutcomeBox1.Size = new System.Drawing.Size(60, 20);
            this.OutcomeBox1.TabIndex = 57;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(230, 144);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 56;
            this.label11.Text = "医師";
            // 
            // DoctorBox1
            // 
            this.DoctorBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DoctorBox1.FormattingEnabled = true;
            this.DoctorBox1.Location = new System.Drawing.Point(261, 140);
            this.DoctorBox1.Name = "DoctorBox1";
            this.DoctorBox1.Size = new System.Drawing.Size(115, 20);
            this.DoctorBox1.TabIndex = 55;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(220, 174);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 54;
            this.label10.Text = "保険";
            // 
            // InsBox1
            // 
            this.InsBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InsBox1.FormattingEnabled = true;
            this.InsBox1.Location = new System.Drawing.Point(255, 170);
            this.InsBox1.Name = "InsBox1";
            this.InsBox1.Size = new System.Drawing.Size(60, 20);
            this.InsBox1.TabIndex = 53;
            // 
            // DateBox2
            // 
            this.DateBox2.DateInt = 0;
            this.DateBox2.DateString = "";
            this.DateBox2.Location = new System.Drawing.Point(52, 193);
            this.DateBox2.Name = "DateBox2";
            this.DateBox2.Size = new System.Drawing.Size(150, 26);
            this.DateBox2.TabIndex = 52;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 199);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 51;
            this.label9.Text = "転帰日";
            // 
            // DeleteButton1
            // 
            this.DeleteButton1.Location = new System.Drawing.Point(715, 10);
            this.DeleteButton1.Name = "DeleteButton1";
            this.DeleteButton1.Size = new System.Drawing.Size(50, 23);
            this.DeleteButton1.TabIndex = 50;
            this.DeleteButton1.Text = "削除";
            this.DeleteButton1.UseVisualStyleBackColor = true;
            this.DeleteButton1.Click += new System.EventHandler(this.DeleteButton1_Click);
            // 
            // ClearButton1
            // 
            this.ClearButton1.Location = new System.Drawing.Point(660, 10);
            this.ClearButton1.Name = "ClearButton1";
            this.ClearButton1.Size = new System.Drawing.Size(50, 23);
            this.ClearButton1.TabIndex = 49;
            this.ClearButton1.Text = "クリア";
            this.ClearButton1.UseVisualStyleBackColor = true;
            this.ClearButton1.Click += new System.EventHandler(this.ClearButton1_Click);
            // 
            // SaveButton1
            // 
            this.SaveButton1.Location = new System.Drawing.Point(580, 10);
            this.SaveButton1.Name = "SaveButton1";
            this.SaveButton1.Size = new System.Drawing.Size(75, 23);
            this.SaveButton1.TabIndex = 48;
            this.SaveButton1.Text = "登録";
            this.SaveButton1.UseVisualStyleBackColor = true;
            this.SaveButton1.Click += new System.EventHandler(this.SaveButton1_Click);
            // 
            // ModeLabel
            // 
            this.ModeLabel.BackColor = System.Drawing.Color.White;
            this.ModeLabel.Location = new System.Drawing.Point(510, 14);
            this.ModeLabel.Name = "ModeLabel";
            this.ModeLabel.Size = new System.Drawing.Size(50, 16);
            this.ModeLabel.TabIndex = 47;
            this.ModeLabel.Text = "新規";
            this.ModeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DPCButton1
            // 
            this.DPCButton1.Location = new System.Drawing.Point(220, 225);
            this.DPCButton1.Name = "DPCButton1";
            this.DPCButton1.Size = new System.Drawing.Size(75, 23);
            this.DPCButton1.TabIndex = 46;
            this.DPCButton1.Text = "DPC";
            this.DPCButton1.UseVisualStyleBackColor = true;
            this.DPCButton1.Click += new System.EventHandler(this.DPCButton1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(110, 144);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 45;
            this.label8.Text = "科";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 144);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 44;
            this.label7.Text = "入外";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 173);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 43;
            this.label6.Text = "開始日";
            // 
            // DatePicker1
            // 
            this.DatePicker1.CustomFormat = "yyyy/MM/dd (ddd)";
            this.DatePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatePicker1.Location = new System.Drawing.Point(55, 170);
            this.DatePicker1.Name = "DatePicker1";
            this.DatePicker1.Size = new System.Drawing.Size(125, 19);
            this.DatePicker1.TabIndex = 42;
            // 
            // InOutBox1
            // 
            this.InOutBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InOutBox1.FormattingEnabled = true;
            this.InOutBox1.Location = new System.Drawing.Point(45, 140);
            this.InOutBox1.Name = "InOutBox1";
            this.InOutBox1.Size = new System.Drawing.Size(55, 20);
            this.InOutBox1.TabIndex = 41;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(390, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 40;
            this.label5.Text = "病名検索";
            // 
            // NoticeFlgBox1
            // 
            this.NoticeFlgBox1.AutoSize = true;
            this.NoticeFlgBox1.Location = new System.Drawing.Point(90, 115);
            this.NoticeFlgBox1.Name = "NoticeFlgBox1";
            this.NoticeFlgBox1.Size = new System.Drawing.Size(60, 16);
            this.NoticeFlgBox1.TabIndex = 39;
            this.NoticeFlgBox1.Text = "告知無";
            this.NoticeFlgBox1.UseVisualStyleBackColor = true;
            // 
            // MainFlgBox1
            // 
            this.MainFlgBox1.AutoSize = true;
            this.MainFlgBox1.Location = new System.Drawing.Point(15, 115);
            this.MainFlgBox1.Name = "MainFlgBox1";
            this.MainFlgBox1.Size = new System.Drawing.Size(60, 16);
            this.MainFlgBox1.TabIndex = 38;
            this.MainFlgBox1.Text = "主病名";
            this.MainFlgBox1.UseVisualStyleBackColor = true;
            // 
            // DeptBox1
            // 
            this.DeptBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeptBox1.FormattingEnabled = true;
            this.DeptBox1.Location = new System.Drawing.Point(130, 140);
            this.DeptBox1.Name = "DeptBox1";
            this.DeptBox1.Size = new System.Drawing.Size(90, 20);
            this.DeptBox1.TabIndex = 37;
            // 
            // DiagCheckBox2
            // 
            this.DiagCheckBox2.AutoSize = true;
            this.DiagCheckBox2.Location = new System.Drawing.Point(105, 230);
            this.DiagCheckBox2.Name = "DiagCheckBox2";
            this.DiagCheckBox2.Size = new System.Drawing.Size(72, 16);
            this.DiagCheckBox2.TabIndex = 36;
            this.DiagCheckBox2.Text = "転帰病名";
            this.DiagCheckBox2.UseVisualStyleBackColor = true;
            this.DiagCheckBox2.CheckedChanged += new System.EventHandler(this.DiagCheckBox2_CheckedChanged);
            // 
            // DiagCheckBox1
            // 
            this.DiagCheckBox1.AutoSize = true;
            this.DiagCheckBox1.Location = new System.Drawing.Point(30, 230);
            this.DiagCheckBox1.Name = "DiagCheckBox1";
            this.DiagCheckBox1.Size = new System.Drawing.Size(60, 16);
            this.DiagCheckBox1.TabIndex = 35;
            this.DiagCheckBox1.Text = "現病名";
            this.DiagCheckBox1.UseVisualStyleBackColor = true;
            this.DiagCheckBox1.CheckedChanged += new System.EventHandler(this.DiagCheckBox1_CheckedChanged);
            // 
            // ListView1
            // 
            this.ListView1.AllowUserToAddRows = false;
            this.ListView1.AllowUserToDeleteRows = false;
            this.ListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
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
            this.ListView1.Location = new System.Drawing.Point(10, 255);
            this.ListView1.Name = "ListView1";
            this.ListView1.ReadOnly = true;
            this.ListView1.RowHeadersVisible = false;
            this.ListView1.RowHeadersWidth = 20;
            this.ListView1.RowTemplate.Height = 21;
            this.ListView1.Size = new System.Drawing.Size(770, 185);
            this.ListView1.TabIndex = 34;
            this.ListView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListView1_CellClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(190, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 32;
            this.label4.Text = "接尾語コード";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(75, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 31;
            this.label3.Text = "接頭語コード";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 30;
            this.label2.Text = "病名コード";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 29;
            this.label1.Text = "病名";
            // 
            // SuffixCodeBox1
            // 
            this.SuffixCodeBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.SuffixCodeBox1.Location = new System.Drawing.Point(190, 85);
            this.SuffixCodeBox1.Name = "SuffixCodeBox1";
            this.SuffixCodeBox1.ReadOnly = true;
            this.SuffixCodeBox1.Size = new System.Drawing.Size(85, 19);
            this.SuffixCodeBox1.TabIndex = 28;
            // 
            // PrefixCodeBox1
            // 
            this.PrefixCodeBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.PrefixCodeBox1.Location = new System.Drawing.Point(75, 85);
            this.PrefixCodeBox1.Name = "PrefixCodeBox1";
            this.PrefixCodeBox1.ReadOnly = true;
            this.PrefixCodeBox1.Size = new System.Drawing.Size(110, 19);
            this.PrefixCodeBox1.TabIndex = 27;
            // 
            // DiagCodeBox1
            // 
            this.DiagCodeBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.DiagCodeBox1.Location = new System.Drawing.Point(10, 85);
            this.DiagCodeBox1.Name = "DiagCodeBox1";
            this.DiagCodeBox1.ReadOnly = true;
            this.DiagCodeBox1.Size = new System.Drawing.Size(60, 19);
            this.DiagCodeBox1.TabIndex = 26;
            // 
            // DiagNameBox1
            // 
            this.DiagNameBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.DiagNameBox1.Location = new System.Drawing.Point(45, 42);
            this.DiagNameBox1.Name = "DiagNameBox1";
            this.DiagNameBox1.ReadOnly = true;
            this.DiagNameBox1.Size = new System.Drawing.Size(331, 19);
            this.DiagNameBox1.TabIndex = 25;
            // 
            // DoubtFlgBox1
            // 
            this.DoubtFlgBox1.AutoSize = true;
            this.DoubtFlgBox1.Location = new System.Drawing.Point(255, 115);
            this.DoubtFlgBox1.Name = "DoubtFlgBox1";
            this.DoubtFlgBox1.Size = new System.Drawing.Size(71, 16);
            this.DoubtFlgBox1.TabIndex = 24;
            this.DoubtFlgBox1.Text = "疑い病名";
            this.DoubtFlgBox1.UseVisualStyleBackColor = true;
            this.DoubtFlgBox1.CheckedChanged += new System.EventHandler(this.DoubtFlgBox1_CheckedChanged);
            // 
            // InsFlgBox1
            // 
            this.InsFlgBox1.AutoSize = true;
            this.InsFlgBox1.Location = new System.Drawing.Point(165, 115);
            this.InsFlgBox1.Name = "InsFlgBox1";
            this.InsFlgBox1.Size = new System.Drawing.Size(72, 16);
            this.InsFlgBox1.TabIndex = 23;
            this.InsFlgBox1.Text = "短期病名";
            this.InsFlgBox1.UseVisualStyleBackColor = true;
            // 
            // DiagFindPanel1
            // 
            this.DiagFindPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DiagFindPanel1.AutoScroll = true;
            this.DiagFindPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DiagFindPanel1.Location = new System.Drawing.Point(390, 65);
            this.DiagFindPanel1.Name = "DiagFindPanel1";
            this.DiagFindPanel1.Size = new System.Drawing.Size(390, 180);
            this.DiagFindPanel1.TabIndex = 22;
            this.DiagFindPanel1.WrapContents = false;
            // 
            // DiagFindBox1
            // 
            this.DiagFindBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DiagFindBox1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.DiagFindBox1.Location = new System.Drawing.Point(450, 42);
            this.DiagFindBox1.Name = "DiagFindBox1";
            this.DiagFindBox1.Size = new System.Drawing.Size(330, 19);
            this.DiagFindBox1.TabIndex = 21;
            this.DiagFindBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DiagFindBox1_KeyDown);
            // 
            // stdControlPat11
            // 
            this.stdControlPat11.Location = new System.Drawing.Point(10, 5);
            this.stdControlPat11.Mode1 = MedicalLibrary.Boundary.StdControlPat1.Mode.Normal;
            this.stdControlPat11.Name = "stdControlPat11";
            this.stdControlPat11.ReadOnly = false;
            this.stdControlPat11.Size = new System.Drawing.Size(500, 30);
            this.stdControlPat11.TabIndex = 33;
            // 
            // FormDiag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 442);
            this.Controls.Add(this.ICDBox2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.ICDBox1);
            this.Controls.Add(this.SEQLabel);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.OutcomeBox1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.DoctorBox1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.InsBox1);
            this.Controls.Add(this.DateBox2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.DeleteButton1);
            this.Controls.Add(this.ClearButton1);
            this.Controls.Add(this.SaveButton1);
            this.Controls.Add(this.ModeLabel);
            this.Controls.Add(this.DPCButton1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.DatePicker1);
            this.Controls.Add(this.InOutBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.NoticeFlgBox1);
            this.Controls.Add(this.MainFlgBox1);
            this.Controls.Add(this.DeptBox1);
            this.Controls.Add(this.DiagCheckBox2);
            this.Controls.Add(this.DiagCheckBox1);
            this.Controls.Add(this.ListView1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SuffixCodeBox1);
            this.Controls.Add(this.PrefixCodeBox1);
            this.Controls.Add(this.DiagCodeBox1);
            this.Controls.Add(this.DiagNameBox1);
            this.Controls.Add(this.DoubtFlgBox1);
            this.Controls.Add(this.InsFlgBox1);
            this.Controls.Add(this.DiagFindPanel1);
            this.Controls.Add(this.DiagFindBox1);
            this.Controls.Add(this.stdControlPat11);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDiag";
            this.Text = "病名";
            this.Load += new System.EventHandler(this.FormDiag_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox DiagFindBox1;
        private System.Windows.Forms.FlowLayoutPanel DiagFindPanel1;
        private System.Windows.Forms.CheckBox InsFlgBox1;
        private System.Windows.Forms.CheckBox DoubtFlgBox1;
        private System.Windows.Forms.TextBox DiagNameBox1;
        private System.Windows.Forms.TextBox DiagCodeBox1;
        private System.Windows.Forms.TextBox PrefixCodeBox1;
        private System.Windows.Forms.TextBox SuffixCodeBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private MedicalLibrary.Boundary.StdControlPat1 stdControlPat11;
        private MedicalLibrary.Boundary.CtrlDiagGridView1 ListView1;
        private System.Windows.Forms.CheckBox DiagCheckBox2;
        private System.Windows.Forms.CheckBox DiagCheckBox1;
        private MedicalLibrary.Boundary.CtrlDeptBox1 DeptBox1;
        private System.Windows.Forms.CheckBox MainFlgBox1;
        private System.Windows.Forms.CheckBox NoticeFlgBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox InOutBox1;
        private System.Windows.Forms.DateTimePicker DatePicker1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button DPCButton1;
        private System.Windows.Forms.Label ModeLabel;
        private System.Windows.Forms.Button SaveButton1;
        private System.Windows.Forms.Button ClearButton1;
        private System.Windows.Forms.Button DeleteButton1;
        private System.Windows.Forms.Label label9;
        private MedicalLibrary.Boundary.CtrlDateBox1 DateBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox InsBox1;
        private MedicalLibrary.Boundary.CtrlDoctorBox1 DoctorBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox OutcomeBox1;
        private System.Windows.Forms.Label SEQLabel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox ICDBox1;
        private System.Windows.Forms.TextBox ICDBox2;
    }
}