namespace MedicalLibrary.Boundary
{
    partial class FormExec
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExec));
            this.PatLabelBox1 = new System.Windows.Forms.PictureBox();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.PrintButton1 = new System.Windows.Forms.Button();
            this.OrderListView1 = new System.Windows.Forms.DataGridView();
            this.DatePicker1 = new System.Windows.Forms.DateTimePicker();
            this.ShowButton1 = new System.Windows.Forms.Button();
            this.WardBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.StaffCodeLabel1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PatCodeLabel1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SekouBox1 = new System.Windows.Forms.CheckBox();
            this.SekouBox2 = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.PreBarcodePanel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.ExecListView1 = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.OrderExecPanel1 = new System.Windows.Forms.Panel();
            this.ErrLabel1 = new System.Windows.Forms.Label();
            this.StaffNameLabel1 = new System.Windows.Forms.Label();
            this.PatNameLabel1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PatLabelBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderListView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExecListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // PatLabelBox1
            // 
            this.PatLabelBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PatLabelBox1.BackColor = System.Drawing.Color.White;
            this.PatLabelBox1.Location = new System.Drawing.Point(730, 10);
            this.PatLabelBox1.Name = "PatLabelBox1";
            this.PatLabelBox1.Size = new System.Drawing.Size(240, 240);
            this.PatLabelBox1.TabIndex = 0;
            this.PatLabelBox1.TabStop = false;
            this.PatLabelBox1.DoubleClick += new System.EventHandler(this.PatLabelBox1_DoubleClick);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // PrintButton1
            // 
            this.PrintButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PrintButton1.Enabled = false;
            this.PrintButton1.Location = new System.Drawing.Point(815, 255);
            this.PrintButton1.Name = "PrintButton1";
            this.PrintButton1.Size = new System.Drawing.Size(75, 23);
            this.PrintButton1.TabIndex = 10;
            this.PrintButton1.Text = "印刷";
            this.PrintButton1.UseVisualStyleBackColor = true;
            this.PrintButton1.Click += new System.EventHandler(this.PrintButton1_Click);
            // 
            // OrderListView1
            // 
            this.OrderListView1.AllowUserToAddRows = false;
            this.OrderListView1.AllowUserToDeleteRows = false;
            this.OrderListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OrderListView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OrderListView1.Location = new System.Drawing.Point(12, 45);
            this.OrderListView1.Name = "OrderListView1";
            this.OrderListView1.ReadOnly = true;
            this.OrderListView1.RowHeadersVisible = false;
            this.OrderListView1.RowTemplate.Height = 21;
            this.OrderListView1.Size = new System.Drawing.Size(680, 350);
            this.OrderListView1.TabIndex = 2;
            this.OrderListView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OrderListView1_CellClick);
            // 
            // DatePicker1
            // 
            this.DatePicker1.CustomFormat = "yyyy/MM/dd (ddd)";
            this.DatePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatePicker1.Location = new System.Drawing.Point(12, 6);
            this.DatePicker1.MinDate = new System.DateTime(2007, 1, 1, 0, 0, 0, 0);
            this.DatePicker1.Name = "DatePicker1";
            this.DatePicker1.Size = new System.Drawing.Size(130, 19);
            this.DatePicker1.TabIndex = 3;
            this.DatePicker1.ValueChanged += new System.EventHandler(this.DatePicker1_ValueChanged);
            // 
            // ShowButton1
            // 
            this.ShowButton1.Location = new System.Drawing.Point(155, 5);
            this.ShowButton1.Name = "ShowButton1";
            this.ShowButton1.Size = new System.Drawing.Size(75, 23);
            this.ShowButton1.TabIndex = 1;
            this.ShowButton1.Text = "表示";
            this.ShowButton1.UseVisualStyleBackColor = true;
            this.ShowButton1.Click += new System.EventHandler(this.ShowButton1_Click);
            // 
            // WardBox1
            // 
            this.WardBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WardBox1.FormattingEnabled = true;
            this.WardBox1.Location = new System.Drawing.Point(250, 6);
            this.WardBox1.Name = "WardBox1";
            this.WardBox1.Size = new System.Drawing.Size(80, 20);
            this.WardBox1.TabIndex = 5;
            this.WardBox1.SelectedIndexChanged += new System.EventHandler(this.WardBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(705, 348);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "オーダー";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(705, 298);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "スタッフ";
            // 
            // StaffCodeLabel1
            // 
            this.StaffCodeLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StaffCodeLabel1.AutoEllipsis = true;
            this.StaffCodeLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.StaffCodeLabel1.Location = new System.Drawing.Point(760, 295);
            this.StaffCodeLabel1.Name = "StaffCodeLabel1";
            this.StaffCodeLabel1.Size = new System.Drawing.Size(80, 18);
            this.StaffCodeLabel1.TabIndex = 10;
            this.StaffCodeLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(705, 323);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "患者";
            // 
            // PatCodeLabel1
            // 
            this.PatCodeLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PatCodeLabel1.AutoEllipsis = true;
            this.PatCodeLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.PatCodeLabel1.Location = new System.Drawing.Point(760, 320);
            this.PatCodeLabel1.Name = "PatCodeLabel1";
            this.PatCodeLabel1.Size = new System.Drawing.Size(80, 18);
            this.PatCodeLabel1.TabIndex = 12;
            this.PatCodeLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(705, 630);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(175, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "※ F5キーを押すとクリアされます";
            // 
            // SekouBox1
            // 
            this.SekouBox1.AutoSize = true;
            this.SekouBox1.Location = new System.Drawing.Point(380, 28);
            this.SekouBox1.Name = "SekouBox1";
            this.SekouBox1.Size = new System.Drawing.Size(60, 16);
            this.SekouBox1.TabIndex = 15;
            this.SekouBox1.Text = "未施行";
            this.SekouBox1.UseVisualStyleBackColor = true;
            this.SekouBox1.CheckedChanged += new System.EventHandler(this.SekouBox1_CheckedChanged);
            // 
            // SekouBox2
            // 
            this.SekouBox2.AutoSize = true;
            this.SekouBox2.Location = new System.Drawing.Point(450, 28);
            this.SekouBox2.Name = "SekouBox2";
            this.SekouBox2.Size = new System.Drawing.Size(60, 16);
            this.SekouBox2.TabIndex = 16;
            this.SekouBox2.Text = "施行済";
            this.SekouBox2.UseVisualStyleBackColor = true;
            this.SekouBox2.CheckedChanged += new System.EventHandler(this.SekouBox2_CheckedChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(705, 645);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(2);
            this.label5.Size = new System.Drawing.Size(290, 30);
            this.label5.TabIndex = 17;
            this.label5.Text = "複数回の点滴オーダーが出ている場合にどうするか、検討が必要です。";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(705, 470);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 19;
            this.label6.Text = "先行実施";
            // 
            // PreBarcodePanel1
            // 
            this.PreBarcodePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PreBarcodePanel1.AutoScroll = true;
            this.PreBarcodePanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PreBarcodePanel1.Location = new System.Drawing.Point(705, 487);
            this.PreBarcodePanel1.Name = "PreBarcodePanel1";
            this.PreBarcodePanel1.Size = new System.Drawing.Size(290, 100);
            this.PreBarcodePanel1.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 12);
            this.label7.TabIndex = 26;
            this.label7.Text = "オーダー";
            // 
            // ExecListView1
            // 
            this.ExecListView1.AllowUserToAddRows = false;
            this.ExecListView1.AllowUserToDeleteRows = false;
            this.ExecListView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ExecListView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ExecListView1.Location = new System.Drawing.Point(12, 415);
            this.ExecListView1.Name = "ExecListView1";
            this.ExecListView1.ReadOnly = true;
            this.ExecListView1.RowHeadersVisible = false;
            this.ExecListView1.RowTemplate.Height = 21;
            this.ExecListView1.Size = new System.Drawing.Size(680, 260);
            this.ExecListView1.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 400);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 28;
            this.label8.Text = "施行";
            // 
            // OrderExecPanel1
            // 
            this.OrderExecPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OrderExecPanel1.AutoScroll = true;
            this.OrderExecPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.OrderExecPanel1.Location = new System.Drawing.Point(705, 365);
            this.OrderExecPanel1.Name = "OrderExecPanel1";
            this.OrderExecPanel1.Size = new System.Drawing.Size(290, 100);
            this.OrderExecPanel1.TabIndex = 26;
            // 
            // ErrLabel1
            // 
            this.ErrLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ErrLabel1.Location = new System.Drawing.Point(705, 595);
            this.ErrLabel1.Name = "ErrLabel1";
            this.ErrLabel1.Padding = new System.Windows.Forms.Padding(2);
            this.ErrLabel1.Size = new System.Drawing.Size(290, 30);
            this.ErrLabel1.TabIndex = 29;
            // 
            // StaffNameLabel1
            // 
            this.StaffNameLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StaffNameLabel1.AutoEllipsis = true;
            this.StaffNameLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.StaffNameLabel1.Location = new System.Drawing.Point(860, 295);
            this.StaffNameLabel1.Name = "StaffNameLabel1";
            this.StaffNameLabel1.Size = new System.Drawing.Size(100, 18);
            this.StaffNameLabel1.TabIndex = 30;
            this.StaffNameLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PatNameLabel1
            // 
            this.PatNameLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PatNameLabel1.AutoEllipsis = true;
            this.PatNameLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.PatNameLabel1.Location = new System.Drawing.Point(860, 320);
            this.PatNameLabel1.Name = "PatNameLabel1";
            this.PatNameLabel1.Size = new System.Drawing.Size(100, 18);
            this.PatNameLabel1.TabIndex = 31;
            this.PatNameLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormExec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 682);
            this.Controls.Add(this.PatNameLabel1);
            this.Controls.Add(this.StaffNameLabel1);
            this.Controls.Add(this.ErrLabel1);
            this.Controls.Add(this.OrderExecPanel1);
            this.Controls.Add(this.ShowButton1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ExecListView1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.PreBarcodePanel1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.SekouBox2);
            this.Controls.Add(this.SekouBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PatCodeLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StaffCodeLabel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.WardBox1);
            this.Controls.Add(this.DatePicker1);
            this.Controls.Add(this.OrderListView1);
            this.Controls.Add(this.PrintButton1);
            this.Controls.Add(this.PatLabelBox1);
            this.KeyPreview = true;
            this.Name = "FormExec";
            this.Text = "実施記録";
            this.Load += new System.EventHandler(this.FormExec_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormExec_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormExec_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.PatLabelBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderListView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExecListView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PatLabelBox1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Button PrintButton1;
        private System.Windows.Forms.DataGridView OrderListView1;
        private System.Windows.Forms.DateTimePicker DatePicker1;
        private System.Windows.Forms.Button ShowButton1;
        private System.Windows.Forms.ComboBox WardBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label StaffCodeLabel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label PatCodeLabel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox SekouBox1;
        private System.Windows.Forms.CheckBox SekouBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel PreBarcodePanel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView ExecListView1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel OrderExecPanel1;
        private System.Windows.Forms.Label ErrLabel1;
        private System.Windows.Forms.Label StaffNameLabel1;
        private System.Windows.Forms.Label PatNameLabel1;
    }
}