namespace MedicalLibrary.Agent
{
    partial class FormBillPayList
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBillPayList));
			this.ListView2 = new System.Windows.Forms.DataGridView();
			this.DatePicker1 = new System.Windows.Forms.DateTimePicker();
			this.ListLabel2 = new System.Windows.Forms.Label();
			this.ListView1 = new System.Windows.Forms.DataGridView();
			this.ListLabel1 = new System.Windows.Forms.Label();
			this.ShowButton = new System.Windows.Forms.Button();
			this.PtIdBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.RegButton = new System.Windows.Forms.Button();
			this.ListLabel3 = new System.Windows.Forms.Label();
			this.ListView3 = new System.Windows.Forms.DataGridView();
			this.RunBox = new System.Windows.Forms.CheckBox();
			this.IntervalBox = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.LiveTimeBox = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.BillPrinterLabel = new System.Windows.Forms.Label();
			this.InvoicePrinterLabel = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.BillPrintAutoBox = new System.Windows.Forms.CheckBox();
			this.PlaceBox = new System.Windows.Forms.ComboBox();
			this.PlaceLabel = new System.Windows.Forms.Label();
			this.FilterBox = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.NumLabel2 = new System.Windows.Forms.Label();
			this.NumLabel1 = new System.Windows.Forms.Label();
			this.NumLabel3 = new System.Windows.Forms.Label();
			this.PrintIntervalBox1 = new System.Windows.Forms.ComboBox();
			this.PrintIntervalBox2 = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.SwLabel = new System.Windows.Forms.Label();
			this.ManualButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.ListView2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ListView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ListView3)).BeginInit();
			this.SuspendLayout();
			// 
			// ListView2
			// 
			this.ListView2.AllowUserToAddRows = false;
			this.ListView2.AllowUserToDeleteRows = false;
			this.ListView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.ListView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.ListView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.ListView2.Location = new System.Drawing.Point(10, 60);
			this.ListView2.Margin = new System.Windows.Forms.Padding(4);
			this.ListView2.MultiSelect = false;
			this.ListView2.Name = "ListView2";
			this.ListView2.ReadOnly = true;
			this.ListView2.RowHeadersVisible = false;
			this.ListView2.RowTemplate.Height = 21;
			this.ListView2.Size = new System.Drawing.Size(1045, 140);
			this.ListView2.TabIndex = 0;
			// 
			// DatePicker1
			// 
			this.DatePicker1.CustomFormat = "yyyy年MM月dd日 (ddd)";
			this.DatePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.DatePicker1.Location = new System.Drawing.Point(10, 7);
			this.DatePicker1.Margin = new System.Windows.Forms.Padding(4);
			this.DatePicker1.MaxDate = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
			this.DatePicker1.MinDate = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
			this.DatePicker1.Name = "DatePicker1";
			this.DatePicker1.Size = new System.Drawing.Size(180, 22);
			this.DatePicker1.TabIndex = 1;
			// 
			// ListLabel2
			// 
			this.ListLabel2.AutoSize = true;
			this.ListLabel2.Location = new System.Drawing.Point(10, 40);
			this.ListLabel2.Name = "ListLabel2";
			this.ListLabel2.Size = new System.Drawing.Size(37, 15);
			this.ListLabel2.TabIndex = 2;
			this.ListLabel2.Text = "保留";
			// 
			// ListView1
			// 
			this.ListView1.AllowUserToAddRows = false;
			this.ListView1.AllowUserToDeleteRows = false;
			this.ListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.ListView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.ListView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.ListView1.Location = new System.Drawing.Point(10, 225);
			this.ListView1.Margin = new System.Windows.Forms.Padding(4);
			this.ListView1.MultiSelect = false;
			this.ListView1.Name = "ListView1";
			this.ListView1.ReadOnly = true;
			this.ListView1.RowHeadersVisible = false;
			this.ListView1.RowTemplate.Height = 21;
			this.ListView1.Size = new System.Drawing.Size(1045, 310);
			this.ListView1.TabIndex = 3;
			// 
			// ListLabel1
			// 
			this.ListLabel1.AutoSize = true;
			this.ListLabel1.Location = new System.Drawing.Point(10, 205);
			this.ListLabel1.Name = "ListLabel1";
			this.ListLabel1.Size = new System.Drawing.Size(37, 15);
			this.ListLabel1.TabIndex = 4;
			this.ListLabel1.Text = "通常";
			// 
			// ShowButton
			// 
			this.ShowButton.Location = new System.Drawing.Point(200, 5);
			this.ShowButton.Name = "ShowButton";
			this.ShowButton.Size = new System.Drawing.Size(80, 26);
			this.ShowButton.TabIndex = 5;
			this.ShowButton.Text = "更新（F5）";
			this.ShowButton.UseVisualStyleBackColor = true;
			this.ShowButton.Click += new System.EventHandler(this.ShowButton_Click);
			// 
			// PtIdBox
			// 
			this.PtIdBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.PtIdBox.Location = new System.Drawing.Point(325, 7);
			this.PtIdBox.MaxLength = 9;
			this.PtIdBox.Name = "PtIdBox";
			this.PtIdBox.Size = new System.Drawing.Size(90, 22);
			this.PtIdBox.TabIndex = 6;
			this.PtIdBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.PtIdBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PtIdBox_KeyDown);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(300, 10);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(21, 15);
			this.label3.TabIndex = 7;
			this.label3.Text = "ID";
			// 
			// RegButton
			// 
			this.RegButton.Location = new System.Drawing.Point(420, 5);
			this.RegButton.Name = "RegButton";
			this.RegButton.Size = new System.Drawing.Size(60, 26);
			this.RegButton.TabIndex = 8;
			this.RegButton.Text = "登録";
			this.RegButton.UseVisualStyleBackColor = true;
			this.RegButton.Click += new System.EventHandler(this.RegButton_Click);
			// 
			// ListLabel3
			// 
			this.ListLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ListLabel3.AutoSize = true;
			this.ListLabel3.Location = new System.Drawing.Point(10, 540);
			this.ListLabel3.Name = "ListLabel3";
			this.ListLabel3.Size = new System.Drawing.Size(37, 15);
			this.ListLabel3.TabIndex = 10;
			this.ListLabel3.Text = "終了";
			// 
			// ListView3
			// 
			this.ListView3.AllowUserToAddRows = false;
			this.ListView3.AllowUserToDeleteRows = false;
			this.ListView3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.ListView3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.ListView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.ListView3.Location = new System.Drawing.Point(10, 560);
			this.ListView3.Margin = new System.Windows.Forms.Padding(4);
			this.ListView3.MultiSelect = false;
			this.ListView3.Name = "ListView3";
			this.ListView3.ReadOnly = true;
			this.ListView3.RowHeadersVisible = false;
			this.ListView3.RowTemplate.Height = 21;
			this.ListView3.Size = new System.Drawing.Size(1045, 160);
			this.ListView3.TabIndex = 9;
			// 
			// RunBox
			// 
			this.RunBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.RunBox.AutoSize = true;
			this.RunBox.Location = new System.Drawing.Point(800, 10);
			this.RunBox.Name = "RunBox";
			this.RunBox.Size = new System.Drawing.Size(86, 19);
			this.RunBox.TabIndex = 12;
			this.RunBox.Text = "自動更新";
			this.RunBox.UseVisualStyleBackColor = true;
			this.RunBox.CheckedChanged += new System.EventHandler(this.RunBox_CheckedChanged);
			// 
			// IntervalBox
			// 
			this.IntervalBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.IntervalBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.IntervalBox.FormattingEnabled = true;
			this.IntervalBox.Location = new System.Drawing.Point(890, 7);
			this.IntervalBox.Name = "IntervalBox";
			this.IntervalBox.Size = new System.Drawing.Size(50, 23);
			this.IntervalBox.TabIndex = 13;
			this.IntervalBox.TextChanged += new System.EventHandler(this.IntervalBox_TextChanged);
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(940, 10);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(45, 15);
			this.label5.TabIndex = 14;
			this.label5.Text = "秒おき";
			// 
			// LiveTimeBox
			// 
			this.LiveTimeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.LiveTimeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.LiveTimeBox.FormattingEnabled = true;
			this.LiveTimeBox.Location = new System.Drawing.Point(100, 536);
			this.LiveTimeBox.Name = "LiveTimeBox";
			this.LiveTimeBox.Size = new System.Drawing.Size(121, 23);
			this.LiveTimeBox.TabIndex = 15;
			this.LiveTimeBox.TextChanged += new System.EventHandler(this.LiveTimeBox_TextChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(490, 10);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(52, 15);
			this.label6.TabIndex = 16;
			this.label6.Text = "請求書";
			// 
			// BillPrinterLabel
			// 
			this.BillPrinterLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.BillPrinterLabel.BackColor = System.Drawing.Color.White;
			this.BillPrinterLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.BillPrinterLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BillPrinterLabel.Location = new System.Drawing.Point(550, 6);
			this.BillPrinterLabel.Name = "BillPrinterLabel";
			this.BillPrinterLabel.Size = new System.Drawing.Size(240, 22);
			this.BillPrinterLabel.TabIndex = 17;
			this.BillPrinterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// InvoicePrinterLabel
			// 
			this.InvoicePrinterLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.InvoicePrinterLabel.BackColor = System.Drawing.Color.White;
			this.InvoicePrinterLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.InvoicePrinterLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.InvoicePrinterLabel.Location = new System.Drawing.Point(550, 32);
			this.InvoicePrinterLabel.Name = "InvoicePrinterLabel";
			this.InvoicePrinterLabel.Size = new System.Drawing.Size(240, 22);
			this.InvoicePrinterLabel.TabIndex = 19;
			this.InvoicePrinterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(490, 36);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(52, 15);
			this.label8.TabIndex = 18;
			this.label8.Text = "明細書";
			// 
			// BillPrintAutoBox
			// 
			this.BillPrintAutoBox.AutoSize = true;
			this.BillPrintAutoBox.Location = new System.Drawing.Point(100, 205);
			this.BillPrintAutoBox.Name = "BillPrintAutoBox";
			this.BillPrintAutoBox.Size = new System.Drawing.Size(131, 19);
			this.BillPrintAutoBox.TabIndex = 20;
			this.BillPrintAutoBox.Text = "請求書自動発行";
			this.BillPrintAutoBox.UseVisualStyleBackColor = true;
			// 
			// PlaceBox
			// 
			this.PlaceBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.PlaceBox.Enabled = false;
			this.PlaceBox.FormattingEnabled = true;
			this.PlaceBox.Location = new System.Drawing.Point(325, 32);
			this.PlaceBox.Name = "PlaceBox";
			this.PlaceBox.Size = new System.Drawing.Size(121, 23);
			this.PlaceBox.TabIndex = 21;
			this.PlaceBox.TextChanged += new System.EventHandler(this.PlaceBox_TextChanged);
			// 
			// PlaceLabel
			// 
			this.PlaceLabel.AutoSize = true;
			this.PlaceLabel.Location = new System.Drawing.Point(285, 35);
			this.PlaceLabel.Name = "PlaceLabel";
			this.PlaceLabel.Size = new System.Drawing.Size(37, 15);
			this.PlaceLabel.TabIndex = 22;
			this.PlaceLabel.Text = "場所";
			this.PlaceLabel.DoubleClick += new System.EventHandler(this.PlaceLabel_DoubleClick);
			// 
			// FilterBox
			// 
			this.FilterBox.Location = new System.Drawing.Point(140, 36);
			this.FilterBox.Name = "FilterBox";
			this.FilterBox.Size = new System.Drawing.Size(140, 22);
			this.FilterBox.TabIndex = 23;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(100, 40);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(37, 15);
			this.label7.TabIndex = 24;
			this.label7.Text = "検索";
			// 
			// NumLabel2
			// 
			this.NumLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.NumLabel2.AutoSize = true;
			this.NumLabel2.Location = new System.Drawing.Point(1010, 40);
			this.NumLabel2.Name = "NumLabel2";
			this.NumLabel2.Size = new System.Drawing.Size(30, 15);
			this.NumLabel2.TabIndex = 25;
			this.NumLabel2.Text = "0名";
			// 
			// NumLabel1
			// 
			this.NumLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.NumLabel1.AutoSize = true;
			this.NumLabel1.Location = new System.Drawing.Point(1010, 205);
			this.NumLabel1.Name = "NumLabel1";
			this.NumLabel1.Size = new System.Drawing.Size(30, 15);
			this.NumLabel1.TabIndex = 26;
			this.NumLabel1.Text = "0名";
			// 
			// NumLabel3
			// 
			this.NumLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.NumLabel3.AutoSize = true;
			this.NumLabel3.Location = new System.Drawing.Point(1010, 540);
			this.NumLabel3.Name = "NumLabel3";
			this.NumLabel3.Size = new System.Drawing.Size(30, 15);
			this.NumLabel3.TabIndex = 27;
			this.NumLabel3.Text = "0名";
			// 
			// PrintIntervalBox1
			// 
			this.PrintIntervalBox1.FormattingEnabled = true;
			this.PrintIntervalBox1.Location = new System.Drawing.Point(450, 201);
			this.PrintIntervalBox1.Name = "PrintIntervalBox1";
			this.PrintIntervalBox1.Size = new System.Drawing.Size(40, 23);
			this.PrintIntervalBox1.TabIndex = 28;
			this.PrintIntervalBox1.TextChanged += new System.EventHandler(this.PrintIntervalBox1_TextChanged);
			// 
			// PrintIntervalBox2
			// 
			this.PrintIntervalBox2.FormattingEnabled = true;
			this.PrintIntervalBox2.Location = new System.Drawing.Point(745, 201);
			this.PrintIntervalBox2.Name = "PrintIntervalBox2";
			this.PrintIntervalBox2.Size = new System.Drawing.Size(40, 23);
			this.PrintIntervalBox2.TabIndex = 29;
			this.PrintIntervalBox2.TextChanged += new System.EventHandler(this.PrintIntervalBox2_TextChanged);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(270, 206);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(176, 15);
			this.label9.TabIndex = 30;
			this.label9.Text = "請求書ごとの印刷間隔（秒）";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(530, 206);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(209, 15);
			this.label10.TabIndex = 31;
			this.label10.Text = "請求書と明細書の印刷間隔（秒）";
			// 
			// SwLabel
			// 
			this.SwLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SwLabel.Location = new System.Drawing.Point(810, 40);
			this.SwLabel.Name = "SwLabel";
			this.SwLabel.Size = new System.Drawing.Size(80, 15);
			this.SwLabel.TabIndex = 32;
			this.SwLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ManualButton
			// 
			this.ManualButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ManualButton.Location = new System.Drawing.Point(990, 5);
			this.ManualButton.Name = "ManualButton";
			this.ManualButton.Size = new System.Drawing.Size(70, 26);
			this.ManualButton.TabIndex = 33;
			this.ManualButton.Text = "使い方";
			this.ManualButton.UseVisualStyleBackColor = true;
			this.ManualButton.Click += new System.EventHandler(this.ManualButton_Click);
			// 
			// FormBillPayList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1064, 722);
			this.Controls.Add(this.ManualButton);
			this.Controls.Add(this.SwLabel);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.PrintIntervalBox2);
			this.Controls.Add(this.PrintIntervalBox1);
			this.Controls.Add(this.NumLabel3);
			this.Controls.Add(this.NumLabel1);
			this.Controls.Add(this.NumLabel2);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.FilterBox);
			this.Controls.Add(this.PlaceLabel);
			this.Controls.Add(this.PlaceBox);
			this.Controls.Add(this.BillPrintAutoBox);
			this.Controls.Add(this.InvoicePrinterLabel);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.BillPrinterLabel);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.LiveTimeBox);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.IntervalBox);
			this.Controls.Add(this.RunBox);
			this.Controls.Add(this.ListLabel3);
			this.Controls.Add(this.ListView3);
			this.Controls.Add(this.RegButton);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.PtIdBox);
			this.Controls.Add(this.ShowButton);
			this.Controls.Add(this.ListLabel1);
			this.Controls.Add(this.ListView1);
			this.Controls.Add(this.ListLabel2);
			this.Controls.Add(this.DatePicker1);
			this.Controls.Add(this.ListView2);
			this.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "FormBillPayList";
			this.Text = "会計患者一覧";
			this.Load += new System.EventHandler(this.FormBillPayList_Load);
			this.Shown += new System.EventHandler(this.FormBillPayList_Shown);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormBillPayList_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.ListView2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ListView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ListView3)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ListView2;
        private System.Windows.Forms.DateTimePicker DatePicker1;
        private System.Windows.Forms.Label ListLabel2;
        private System.Windows.Forms.DataGridView ListView1;
        private System.Windows.Forms.Label ListLabel1;
        private System.Windows.Forms.Button ShowButton;
        private System.Windows.Forms.TextBox PtIdBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button RegButton;
        private System.Windows.Forms.Label ListLabel3;
        private System.Windows.Forms.DataGridView ListView3;
        private System.Windows.Forms.CheckBox RunBox;
        private System.Windows.Forms.ComboBox IntervalBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox LiveTimeBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label BillPrinterLabel;
        private System.Windows.Forms.Label InvoicePrinterLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox BillPrintAutoBox;
        private System.Windows.Forms.ComboBox PlaceBox;
        private System.Windows.Forms.Label PlaceLabel;
        private System.Windows.Forms.TextBox FilterBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label NumLabel2;
        private System.Windows.Forms.Label NumLabel1;
        private System.Windows.Forms.Label NumLabel3;
        private System.Windows.Forms.ComboBox PrintIntervalBox1;
        private System.Windows.Forms.ComboBox PrintIntervalBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label SwLabel;
		private System.Windows.Forms.Button ManualButton;
    }
}

