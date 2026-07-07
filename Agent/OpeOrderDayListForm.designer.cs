namespace MedicalLibrary.Agent
{
    partial class OpeOrderDayListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpeOrderDayListForm));
            this.DatePicker1 = new System.Windows.Forms.DateTimePicker();
            this.ListPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.ShowButton = new System.Windows.Forms.Button();
            this.PlaceFilterBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.OpeRoomFilterBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.PrintBox1 = new System.Windows.Forms.CheckBox();
            this.SentBox1 = new System.Windows.Forms.CheckBox();
            this.ContBox = new System.Windows.Forms.TextBox();
            this.OpeDateLabel = new System.Windows.Forms.Label();
            this.PtIdLabel = new System.Windows.Forms.Label();
            this.PtNameLabel = new System.Windows.Forms.Label();
            this.OpeRoomBox = new System.Windows.Forms.ComboBox();
            this.MinutesBox1 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.RegButton = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.MinutesBox2 = new System.Windows.Forms.TextBox();
            this.ClearButton = new System.Windows.Forms.Button();
            this.OrderDataBox = new System.Windows.Forms.TextBox();
            this.OpeOrderGroupBox = new System.Windows.Forms.GroupBox();
            this.OpePartBox = new System.Windows.Forms.ComboBox();
            this.OpeNameBox = new System.Windows.Forms.ComboBox();
            this.OpeAnesBox = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.EndTimeBox = new MedicalLibrary.Boundary.CtrlTimeBox1();
            this.StartTimeBox = new MedicalLibrary.Boundary.CtrlTimeBox1();
            this.label21 = new System.Windows.Forms.Label();
            this.SaveLabel = new System.Windows.Forms.Label();
            this.CancelBox = new System.Windows.Forms.CheckBox();
            this.InfectionButton = new System.Windows.Forms.Button();
            this.InfectionBox = new System.Windows.Forms.TextBox();
            this.NsBox4 = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.NsBox2 = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.NsBox1 = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.DeptBox = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.TimeOutButton0 = new System.Windows.Forms.RadioButton();
            this.TimeOutButton1 = new System.Windows.Forms.RadioButton();
            this.TimeOutButton2 = new System.Windows.Forms.RadioButton();
            this.label16 = new System.Windows.Forms.Label();
            this.DoctorBox3 = new System.Windows.Forms.ComboBox();
            this.TFBox = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.PrintBox2 = new System.Windows.Forms.CheckBox();
            this.DaysLabel = new System.Windows.Forms.Label();
            this.AnesRecordButton = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.InOutFilterBox = new System.Windows.Forms.ComboBox();
            this.ExcelPlanButton = new System.Windows.Forms.Button();
            this.ShowCancelBox = new System.Windows.Forms.CheckBox();
            this.DatePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label25 = new System.Windows.Forms.Label();
            this.OpeOrderGroupBox.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DatePicker1
            // 
            this.DatePicker1.CustomFormat = "yyyy/MM/dd (ddd)";
            this.DatePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatePicker1.Location = new System.Drawing.Point(45, 5);
            this.DatePicker1.MaxDate = new System.DateTime(2999, 12, 31, 0, 0, 0, 0);
            this.DatePicker1.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.DatePicker1.Name = "DatePicker1";
            this.DatePicker1.Size = new System.Drawing.Size(125, 19);
            this.DatePicker1.TabIndex = 0;
            // 
            // ListPanel
            // 
            this.ListPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListPanel.AutoScroll = true;
            this.ListPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ListPanel.Location = new System.Drawing.Point(5, 30);
            this.ListPanel.Name = "ListPanel";
            this.ListPanel.Size = new System.Drawing.Size(918, 680);
            this.ListPanel.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "期間";
            // 
            // ShowButton
            // 
            this.ShowButton.Location = new System.Drawing.Point(325, 3);
            this.ShowButton.Name = "ShowButton";
            this.ShowButton.Size = new System.Drawing.Size(60, 23);
            this.ShowButton.TabIndex = 3;
            this.ShowButton.Text = "表示";
            this.ShowButton.UseVisualStyleBackColor = true;
            this.ShowButton.Click += new System.EventHandler(this.ShowButton_Click);
            // 
            // PlaceFilterBox
            // 
            this.PlaceFilterBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PlaceFilterBox.FormattingEnabled = true;
            this.PlaceFilterBox.Location = new System.Drawing.Point(435, 5);
            this.PlaceFilterBox.Name = "PlaceFilterBox";
            this.PlaceFilterBox.Size = new System.Drawing.Size(60, 20);
            this.PlaceFilterBox.TabIndex = 4;
            this.PlaceFilterBox.SelectedIndexChanged += new System.EventHandler(this.PlaceFilterBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(400, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "場所";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(620, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "手術室";
            // 
            // OpeRoomFilterBox
            // 
            this.OpeRoomFilterBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OpeRoomFilterBox.FormattingEnabled = true;
            this.OpeRoomFilterBox.Location = new System.Drawing.Point(665, 5);
            this.OpeRoomFilterBox.Name = "OpeRoomFilterBox";
            this.OpeRoomFilterBox.Size = new System.Drawing.Size(60, 20);
            this.OpeRoomFilterBox.TabIndex = 6;
            this.OpeRoomFilterBox.SelectedIndexChanged += new System.EventHandler(this.OpeRoomFilterBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(926, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "手術日";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(927, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "患者ID";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(926, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "手術指示";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "入退室";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "所要時間";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 114);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 13;
            this.label9.Text = "診療科";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 15;
            this.label11.Text = "手術室";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 271);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 12);
            this.label13.TabIndex = 17;
            this.label13.Text = "コメント";
            // 
            // PrintBox1
            // 
            this.PrintBox1.AutoSize = true;
            this.PrintBox1.Location = new System.Drawing.Point(11, 312);
            this.PrintBox1.Name = "PrintBox1";
            this.PrintBox1.Size = new System.Drawing.Size(60, 16);
            this.PrintBox1.TabIndex = 171;
            this.PrintBox1.Text = "印刷済";
            this.PrintBox1.UseVisualStyleBackColor = true;
            // 
            // SentBox1
            // 
            this.SentBox1.AutoSize = true;
            this.SentBox1.Location = new System.Drawing.Point(83, 312);
            this.SentBox1.Name = "SentBox1";
            this.SentBox1.Size = new System.Drawing.Size(84, 16);
            this.SentBox1.TabIndex = 173;
            this.SentBox1.Text = "麻酔送信済";
            this.SentBox1.UseVisualStyleBackColor = true;
            // 
            // ContBox
            // 
            this.ContBox.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.ContBox.Location = new System.Drawing.Point(55, 267);
            this.ContBox.MaxLength = 199;
            this.ContBox.Multiline = true;
            this.ContBox.Name = "ContBox";
            this.ContBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ContBox.Size = new System.Drawing.Size(190, 40);
            this.ContBox.TabIndex = 161;
            // 
            // OpeDateLabel
            // 
            this.OpeDateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OpeDateLabel.BackColor = System.Drawing.Color.White;
            this.OpeDateLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OpeDateLabel.Location = new System.Drawing.Point(971, 30);
            this.OpeDateLabel.Name = "OpeDateLabel";
            this.OpeDateLabel.Size = new System.Drawing.Size(70, 18);
            this.OpeDateLabel.TabIndex = 21;
            this.OpeDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PtIdLabel
            // 
            this.PtIdLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PtIdLabel.BackColor = System.Drawing.Color.White;
            this.PtIdLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PtIdLabel.Location = new System.Drawing.Point(971, 51);
            this.PtIdLabel.Name = "PtIdLabel";
            this.PtIdLabel.Size = new System.Drawing.Size(60, 18);
            this.PtIdLabel.TabIndex = 22;
            this.PtIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PtNameLabel
            // 
            this.PtNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PtNameLabel.BackColor = System.Drawing.Color.White;
            this.PtNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PtNameLabel.Location = new System.Drawing.Point(1032, 51);
            this.PtNameLabel.Name = "PtNameLabel";
            this.PtNameLabel.Size = new System.Drawing.Size(146, 18);
            this.PtNameLabel.TabIndex = 24;
            this.PtNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OpeRoomBox
            // 
            this.OpeRoomBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OpeRoomBox.FormattingEnabled = true;
            this.OpeRoomBox.Location = new System.Drawing.Point(70, 15);
            this.OpeRoomBox.Name = "OpeRoomBox";
            this.OpeRoomBox.Size = new System.Drawing.Size(80, 20);
            this.OpeRoomBox.TabIndex = 101;
            // 
            // MinutesBox1
            // 
            this.MinutesBox1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.MinutesBox1.Location = new System.Drawing.Point(70, 61);
            this.MinutesBox1.MaxLength = 2;
            this.MinutesBox1.Name = "MinutesBox1";
            this.MinutesBox1.Size = new System.Drawing.Size(30, 19);
            this.MinutesBox1.TabIndex = 107;
            this.MinutesBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(103, 64);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 12);
            this.label15.TabIndex = 31;
            this.label15.Text = "時間";
            // 
            // RegButton
            // 
            this.RegButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.RegButton.Location = new System.Drawing.Point(63, 370);
            this.RegButton.Name = "RegButton";
            this.RegButton.Size = new System.Drawing.Size(60, 22);
            this.RegButton.TabIndex = 201;
            this.RegButton.Text = "登録";
            this.RegButton.UseVisualStyleBackColor = false;
            this.RegButton.Click += new System.EventHandler(this.RegButton_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(168, 64);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 36;
            this.label10.Text = "分";
            // 
            // MinutesBox2
            // 
            this.MinutesBox2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.MinutesBox2.Location = new System.Drawing.Point(135, 61);
            this.MinutesBox2.MaxLength = 2;
            this.MinutesBox2.Name = "MinutesBox2";
            this.MinutesBox2.Size = new System.Drawing.Size(30, 19);
            this.MinutesBox2.TabIndex = 109;
            this.MinutesBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ClearButton
            // 
            this.ClearButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ClearButton.Location = new System.Drawing.Point(129, 370);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(60, 22);
            this.ClearButton.TabIndex = 203;
            this.ClearButton.Text = "クリア";
            this.ClearButton.UseVisualStyleBackColor = false;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // OrderDataBox
            // 
            this.OrderDataBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OrderDataBox.BackColor = System.Drawing.Color.White;
            this.OrderDataBox.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OrderDataBox.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.OrderDataBox.Location = new System.Drawing.Point(928, 91);
            this.OrderDataBox.MaxLength = 199;
            this.OrderDataBox.Multiline = true;
            this.OrderDataBox.Name = "OrderDataBox";
            this.OrderDataBox.ReadOnly = true;
            this.OrderDataBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.OrderDataBox.Size = new System.Drawing.Size(250, 220);
            this.OrderDataBox.TabIndex = 38;
            // 
            // OpeOrderGroupBox
            // 
            this.OpeOrderGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OpeOrderGroupBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.OpeOrderGroupBox.Controls.Add(this.OpePartBox);
            this.OpeOrderGroupBox.Controls.Add(this.OpeNameBox);
            this.OpeOrderGroupBox.Controls.Add(this.OpeAnesBox);
            this.OpeOrderGroupBox.Controls.Add(this.label24);
            this.OpeOrderGroupBox.Controls.Add(this.label23);
            this.OpeOrderGroupBox.Controls.Add(this.label22);
            this.OpeOrderGroupBox.Controls.Add(this.EndTimeBox);
            this.OpeOrderGroupBox.Controls.Add(this.StartTimeBox);
            this.OpeOrderGroupBox.Controls.Add(this.label21);
            this.OpeOrderGroupBox.Controls.Add(this.SaveLabel);
            this.OpeOrderGroupBox.Controls.Add(this.CancelBox);
            this.OpeOrderGroupBox.Controls.Add(this.InfectionButton);
            this.OpeOrderGroupBox.Controls.Add(this.InfectionBox);
            this.OpeOrderGroupBox.Controls.Add(this.NsBox4);
            this.OpeOrderGroupBox.Controls.Add(this.label20);
            this.OpeOrderGroupBox.Controls.Add(this.NsBox2);
            this.OpeOrderGroupBox.Controls.Add(this.label19);
            this.OpeOrderGroupBox.Controls.Add(this.NsBox1);
            this.OpeOrderGroupBox.Controls.Add(this.label18);
            this.OpeOrderGroupBox.Controls.Add(this.label17);
            this.OpeOrderGroupBox.Controls.Add(this.DeptBox);
            this.OpeOrderGroupBox.Controls.Add(this.flowLayoutPanel1);
            this.OpeOrderGroupBox.Controls.Add(this.label16);
            this.OpeOrderGroupBox.Controls.Add(this.DoctorBox3);
            this.OpeOrderGroupBox.Controls.Add(this.TFBox);
            this.OpeOrderGroupBox.Controls.Add(this.label14);
            this.OpeOrderGroupBox.Controls.Add(this.PrintBox2);
            this.OpeOrderGroupBox.Controls.Add(this.ClearButton);
            this.OpeOrderGroupBox.Controls.Add(this.OpeRoomBox);
            this.OpeOrderGroupBox.Controls.Add(this.label10);
            this.OpeOrderGroupBox.Controls.Add(this.PrintBox1);
            this.OpeOrderGroupBox.Controls.Add(this.MinutesBox2);
            this.OpeOrderGroupBox.Controls.Add(this.RegButton);
            this.OpeOrderGroupBox.Controls.Add(this.MinutesBox1);
            this.OpeOrderGroupBox.Controls.Add(this.ContBox);
            this.OpeOrderGroupBox.Controls.Add(this.label15);
            this.OpeOrderGroupBox.Controls.Add(this.SentBox1);
            this.OpeOrderGroupBox.Controls.Add(this.label11);
            this.OpeOrderGroupBox.Controls.Add(this.label9);
            this.OpeOrderGroupBox.Controls.Add(this.label13);
            this.OpeOrderGroupBox.Controls.Add(this.label8);
            this.OpeOrderGroupBox.Controls.Add(this.label7);
            this.OpeOrderGroupBox.Location = new System.Drawing.Point(928, 315);
            this.OpeOrderGroupBox.Name = "OpeOrderGroupBox";
            this.OpeOrderGroupBox.Size = new System.Drawing.Size(250, 395);
            this.OpeOrderGroupBox.TabIndex = 40;
            this.OpeOrderGroupBox.TabStop = false;
            this.OpeOrderGroupBox.Text = "記入欄";
            // 
            // OpePartBox
            // 
            this.OpePartBox.FormattingEnabled = true;
            this.OpePartBox.Location = new System.Drawing.Point(55, 157);
            this.OpePartBox.MaxLength = 30;
            this.OpePartBox.Name = "OpePartBox";
            this.OpePartBox.Size = new System.Drawing.Size(190, 20);
            this.OpePartBox.TabIndex = 137;
            // 
            // OpeNameBox
            // 
            this.OpeNameBox.FormattingEnabled = true;
            this.OpeNameBox.Location = new System.Drawing.Point(55, 135);
            this.OpeNameBox.MaxLength = 30;
            this.OpeNameBox.Name = "OpeNameBox";
            this.OpeNameBox.Size = new System.Drawing.Size(190, 20);
            this.OpeNameBox.TabIndex = 135;
            // 
            // OpeAnesBox
            // 
            this.OpeAnesBox.FormattingEnabled = true;
            this.OpeAnesBox.Location = new System.Drawing.Point(165, 201);
            this.OpeAnesBox.MaxLength = 30;
            this.OpeAnesBox.Name = "OpeAnesBox";
            this.OpeAnesBox.Size = new System.Drawing.Size(80, 20);
            this.OpeAnesBox.TabIndex = 145;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(130, 205);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(29, 12);
            this.label24.TabIndex = 150;
            this.label24.Text = "麻酔";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(7, 161);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(29, 12);
            this.label23.TabIndex = 148;
            this.label23.Text = "部位";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(7, 139);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(29, 12);
            this.label22.TabIndex = 146;
            this.label22.Text = "術式";
            // 
            // EndTimeBox
            // 
            this.EndTimeBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.EndTimeBox.Location = new System.Drawing.Point(135, 39);
            this.EndTimeBox.MaxLength = 5;
            this.EndTimeBox.Name = "EndTimeBox";
            this.EndTimeBox.Size = new System.Drawing.Size(40, 19);
            this.EndTimeBox.TabIndex = 105;
            this.EndTimeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // StartTimeBox
            // 
            this.StartTimeBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.StartTimeBox.Location = new System.Drawing.Point(70, 39);
            this.StartTimeBox.MaxLength = 5;
            this.StartTimeBox.Name = "StartTimeBox";
            this.StartTimeBox.Size = new System.Drawing.Size(40, 19);
            this.StartTimeBox.TabIndex = 103;
            this.StartTimeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(7, 352);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(29, 12);
            this.label21.TabIndex = 145;
            this.label21.Text = "更新";
            // 
            // SaveLabel
            // 
            this.SaveLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SaveLabel.Location = new System.Drawing.Point(42, 349);
            this.SaveLabel.Name = "SaveLabel";
            this.SaveLabel.Size = new System.Drawing.Size(200, 18);
            this.SaveLabel.TabIndex = 144;
            this.SaveLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CancelBox
            // 
            this.CancelBox.AutoSize = true;
            this.CancelBox.Location = new System.Drawing.Point(11, 331);
            this.CancelBox.Name = "CancelBox";
            this.CancelBox.Size = new System.Drawing.Size(71, 16);
            this.CancelBox.TabIndex = 177;
            this.CancelBox.Text = "キャンセル";
            this.CancelBox.UseVisualStyleBackColor = true;
            // 
            // InfectionButton
            // 
            this.InfectionButton.Location = new System.Drawing.Point(205, 177);
            this.InfectionButton.Name = "InfectionButton";
            this.InfectionButton.Size = new System.Drawing.Size(40, 23);
            this.InfectionButton.TabIndex = 142;
            this.InfectionButton.Text = "取込";
            this.InfectionButton.UseVisualStyleBackColor = true;
            this.InfectionButton.Click += new System.EventHandler(this.InfectionButton_Click);
            // 
            // InfectionBox
            // 
            this.InfectionBox.Location = new System.Drawing.Point(55, 179);
            this.InfectionBox.MaxLength = 100;
            this.InfectionBox.Name = "InfectionBox";
            this.InfectionBox.Size = new System.Drawing.Size(145, 19);
            this.InfectionBox.TabIndex = 141;
            // 
            // NsBox4
            // 
            this.NsBox4.FormattingEnabled = true;
            this.NsBox4.Location = new System.Drawing.Point(55, 245);
            this.NsBox4.Name = "NsBox4";
            this.NsBox4.Size = new System.Drawing.Size(70, 20);
            this.NsBox4.TabIndex = 155;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(7, 249);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(43, 12);
            this.label20.TabIndex = 139;
            this.label20.Text = "記録Ns";
            // 
            // NsBox2
            // 
            this.NsBox2.FormattingEnabled = true;
            this.NsBox2.Location = new System.Drawing.Point(175, 223);
            this.NsBox2.Name = "NsBox2";
            this.NsBox2.Size = new System.Drawing.Size(70, 20);
            this.NsBox2.TabIndex = 153;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(130, 227);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(43, 12);
            this.label19.TabIndex = 137;
            this.label19.Text = "外回Ns";
            // 
            // NsBox1
            // 
            this.NsBox1.FormattingEnabled = true;
            this.NsBox1.Location = new System.Drawing.Point(55, 223);
            this.NsBox1.Name = "NsBox1";
            this.NsBox1.Size = new System.Drawing.Size(70, 20);
            this.NsBox1.TabIndex = 151;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(7, 227);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(43, 12);
            this.label18.TabIndex = 135;
            this.label18.Text = "器械Ns";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(7, 183);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 12);
            this.label17.TabIndex = 134;
            this.label17.Text = "感染症";
            // 
            // DeptBox
            // 
            this.DeptBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeptBox.FormattingEnabled = true;
            this.DeptBox.Location = new System.Drawing.Point(55, 111);
            this.DeptBox.Name = "DeptBox";
            this.DeptBox.Size = new System.Drawing.Size(90, 20);
            this.DeptBox.TabIndex = 133;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.TimeOutButton0);
            this.flowLayoutPanel1.Controls.Add(this.TimeOutButton1);
            this.flowLayoutPanel1.Controls.Add(this.TimeOutButton2);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(7, 84);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(235, 23);
            this.flowLayoutPanel1.TabIndex = 132;
            // 
            // TimeOutButton0
            // 
            this.TimeOutButton0.AutoSize = true;
            this.TimeOutButton0.Checked = true;
            this.TimeOutButton0.Location = new System.Drawing.Point(3, 3);
            this.TimeOutButton0.Name = "TimeOutButton0";
            this.TimeOutButton0.Size = new System.Drawing.Size(47, 16);
            this.TimeOutButton0.TabIndex = 0;
            this.TimeOutButton0.TabStop = true;
            this.TimeOutButton0.Text = "通常";
            this.TimeOutButton0.UseVisualStyleBackColor = true;
            // 
            // TimeOutButton1
            // 
            this.TimeOutButton1.AutoSize = true;
            this.TimeOutButton1.Location = new System.Drawing.Point(56, 3);
            this.TimeOutButton1.Name = "TimeOutButton1";
            this.TimeOutButton1.Size = new System.Drawing.Size(59, 16);
            this.TimeOutButton1.TabIndex = 1;
            this.TimeOutButton1.Text = "予約外";
            this.TimeOutButton1.UseVisualStyleBackColor = true;
            // 
            // TimeOutButton2
            // 
            this.TimeOutButton2.AutoSize = true;
            this.TimeOutButton2.Location = new System.Drawing.Point(121, 3);
            this.TimeOutButton2.Name = "TimeOutButton2";
            this.TimeOutButton2.Size = new System.Drawing.Size(47, 16);
            this.TimeOutButton2.TabIndex = 2;
            this.TimeOutButton2.Text = "緊急";
            this.TimeOutButton2.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(115, 42);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(17, 12);
            this.label16.TabIndex = 131;
            this.label16.Text = "～";
            // 
            // DoctorBox3
            // 
            this.DoctorBox3.FormattingEnabled = true;
            this.DoctorBox3.Location = new System.Drawing.Point(55, 201);
            this.DoctorBox3.Name = "DoctorBox3";
            this.DoctorBox3.Size = new System.Drawing.Size(70, 20);
            this.DoctorBox3.TabIndex = 143;
            // 
            // TFBox
            // 
            this.TFBox.AutoSize = true;
            this.TFBox.Location = new System.Drawing.Point(195, 41);
            this.TFBox.Name = "TFBox";
            this.TFBox.Size = new System.Drawing.Size(38, 16);
            this.TFBox.TabIndex = 106;
            this.TFBox.Text = "TF";
            this.TFBox.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 205);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 125;
            this.label14.Text = "麻酔医";
            // 
            // PrintBox2
            // 
            this.PrintBox2.AutoSize = true;
            this.PrintBox2.Location = new System.Drawing.Point(177, 312);
            this.PrintBox2.Name = "PrintBox2";
            this.PrintBox2.Size = new System.Drawing.Size(64, 16);
            this.PrintBox2.TabIndex = 175;
            this.PrintBox2.Text = "ラベル済";
            this.PrintBox2.UseVisualStyleBackColor = true;
            // 
            // DaysLabel
            // 
            this.DaysLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DaysLabel.BackColor = System.Drawing.Color.White;
            this.DaysLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DaysLabel.Location = new System.Drawing.Point(1108, 30);
            this.DaysLabel.Name = "DaysLabel";
            this.DaysLabel.Size = new System.Drawing.Size(70, 18);
            this.DaysLabel.TabIndex = 41;
            this.DaysLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.DaysLabel.Visible = false;
            // 
            // AnesRecordButton
            // 
            this.AnesRecordButton.Location = new System.Drawing.Point(870, 5);
            this.AnesRecordButton.Name = "AnesRecordButton";
            this.AnesRecordButton.Size = new System.Drawing.Size(66, 22);
            this.AnesRecordButton.TabIndex = 42;
            this.AnesRecordButton.Text = "麻酔記録";
            this.AnesRecordButton.UseVisualStyleBackColor = true;
            this.AnesRecordButton.Click += new System.EventHandler(this.AnesRecordButton_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(510, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 12);
            this.label12.TabIndex = 44;
            this.label12.Text = "入外";
            // 
            // InOutFilterBox
            // 
            this.InOutFilterBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InOutFilterBox.FormattingEnabled = true;
            this.InOutFilterBox.Location = new System.Drawing.Point(545, 5);
            this.InOutFilterBox.Name = "InOutFilterBox";
            this.InOutFilterBox.Size = new System.Drawing.Size(60, 20);
            this.InOutFilterBox.TabIndex = 43;
            this.InOutFilterBox.SelectedIndexChanged += new System.EventHandler(this.InOutFilterBox_SelectedIndexChanged);
            // 
            // ExcelPlanButton
            // 
            this.ExcelPlanButton.Location = new System.Drawing.Point(940, 5);
            this.ExcelPlanButton.Name = "ExcelPlanButton";
            this.ExcelPlanButton.Size = new System.Drawing.Size(75, 22);
            this.ExcelPlanButton.TabIndex = 45;
            this.ExcelPlanButton.Text = "Excel出力";
            this.ExcelPlanButton.UseVisualStyleBackColor = true;
            this.ExcelPlanButton.Click += new System.EventHandler(this.ExcelPlanButton_Click);
            // 
            // ShowCancelBox
            // 
            this.ShowCancelBox.AutoSize = true;
            this.ShowCancelBox.Location = new System.Drawing.Point(750, 8);
            this.ShowCancelBox.Name = "ShowCancelBox";
            this.ShowCancelBox.Size = new System.Drawing.Size(95, 16);
            this.ShowCancelBox.TabIndex = 46;
            this.ShowCancelBox.Text = "キャンセル表示";
            this.ShowCancelBox.UseVisualStyleBackColor = true;
            this.ShowCancelBox.CheckedChanged += new System.EventHandler(this.ShowCancelBox_CheckedChanged);
            // 
            // DatePicker2
            // 
            this.DatePicker2.CustomFormat = "yyyy/MM/dd (ddd)";
            this.DatePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatePicker2.Location = new System.Drawing.Point(195, 5);
            this.DatePicker2.MaxDate = new System.DateTime(2999, 12, 31, 0, 0, 0, 0);
            this.DatePicker2.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.DatePicker2.Name = "DatePicker2";
            this.DatePicker2.Size = new System.Drawing.Size(125, 19);
            this.DatePicker2.TabIndex = 49;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(175, 9);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(17, 12);
            this.label25.TabIndex = 50;
            this.label25.Text = "～";
            // 
            // OpeOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 713);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.DatePicker2);
            this.Controls.Add(this.ShowCancelBox);
            this.Controls.Add(this.ExcelPlanButton);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.InOutFilterBox);
            this.Controls.Add(this.AnesRecordButton);
            this.Controls.Add(this.DaysLabel);
            this.Controls.Add(this.OpeOrderGroupBox);
            this.Controls.Add(this.OrderDataBox);
            this.Controls.Add(this.PtNameLabel);
            this.Controls.Add(this.PtIdLabel);
            this.Controls.Add(this.OpeDateLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.OpeRoomFilterBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PlaceFilterBox);
            this.Controls.Add(this.ShowButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListPanel);
            this.Controls.Add(this.DatePicker1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OpeOrderForm";
            this.Text = "手術指示一覧";
            this.Load += new System.EventHandler(this.OpeOrderDayListForm_Load);
            this.OpeOrderGroupBox.ResumeLayout(false);
            this.OpeOrderGroupBox.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker DatePicker1;
        private System.Windows.Forms.Panel ListPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ShowButton;
        private System.Windows.Forms.ComboBox PlaceFilterBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox OpeRoomFilterBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox PrintBox1;
        private System.Windows.Forms.CheckBox SentBox1;
        private System.Windows.Forms.TextBox ContBox;
        private System.Windows.Forms.Label OpeDateLabel;
        private System.Windows.Forms.Label PtIdLabel;
        private System.Windows.Forms.Label PtNameLabel;
        private System.Windows.Forms.ComboBox OpeRoomBox;
        private System.Windows.Forms.TextBox MinutesBox1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button RegButton;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox MinutesBox2;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.TextBox OrderDataBox;
        private System.Windows.Forms.GroupBox OpeOrderGroupBox;
        private System.Windows.Forms.Label DaysLabel;
        private System.Windows.Forms.Button AnesRecordButton;
        private System.Windows.Forms.CheckBox PrintBox2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox InOutFilterBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button ExcelPlanButton;
        private System.Windows.Forms.CheckBox TFBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox DoctorBox3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton TimeOutButton0;
        private System.Windows.Forms.RadioButton TimeOutButton1;
        private System.Windows.Forms.ComboBox DeptBox;
        private System.Windows.Forms.RadioButton TimeOutButton2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox NsBox4;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox NsBox2;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox NsBox1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox InfectionBox;
        private System.Windows.Forms.Button InfectionButton;
        private System.Windows.Forms.CheckBox CancelBox;
        private System.Windows.Forms.Label SaveLabel;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.CheckBox ShowCancelBox;
        private MedicalLibrary.Boundary.CtrlTimeBox1 StartTimeBox;
        private MedicalLibrary.Boundary.CtrlTimeBox1 EndTimeBox;
        private System.Windows.Forms.ComboBox OpePartBox;
        private System.Windows.Forms.ComboBox OpeNameBox;
        private System.Windows.Forms.ComboBox OpeAnesBox;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.DateTimePicker DatePicker2;
        private System.Windows.Forms.Label label25;
    }
}