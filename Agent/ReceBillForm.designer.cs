namespace MedicalLibrary.Agent
{
    partial class ReceBillForm
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReceBillForm));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.ListView1 = new System.Windows.Forms.DataGridView();
			this.findButton = new System.Windows.Forms.Button();
			this.printButton = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.inOut2 = new System.Windows.Forms.RadioButton();
			this.inOut1 = new System.Windows.Forms.RadioButton();
			this.inOut0 = new System.Windows.Forms.RadioButton();
			this.countLabel = new System.Windows.Forms.Label();
			this.deptBox = new System.Windows.Forms.CheckedListBox();
			this.label4 = new System.Windows.Forms.Label();
			this.selectAllButton = new System.Windows.Forms.Button();
			this.selectNoButton = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.printDocument1 = new System.Drawing.Printing.PrintDocument();
			this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
			this.printDialog1 = new System.Windows.Forms.PrintDialog();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.touseki2 = new System.Windows.Forms.RadioButton();
			this.touseki1 = new System.Windows.Forms.RadioButton();
			this.touseki0 = new System.Windows.Forms.RadioButton();
			this.FilterBox = new System.Windows.Forms.TextBox();
			this.ExcludeBox = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.excelButton = new System.Windows.Forms.Button();
			this.ExcludeCodesLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.ListView1)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(97, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(17, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "自";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(97, 53);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(17, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "至";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 10);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 12);
			this.label3.TabIndex = 4;
			this.label3.Text = "対象期間";
			// 
			// ListView1
			// 
			this.ListView1.AllowUserToAddRows = false;
			this.ListView1.AllowUserToDeleteRows = false;
			this.ListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ListView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.ListView1.DefaultCellStyle = dataGridViewCellStyle1;
			this.ListView1.Location = new System.Drawing.Point(125, 28);
			this.ListView1.Name = "ListView1";
			this.ListView1.ReadOnly = true;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.ListView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.ListView1.RowHeadersVisible = false;
			this.ListView1.RowTemplate.Height = 21;
			this.ListView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.ListView1.Size = new System.Drawing.Size(815, 450);
			this.ListView1.TabIndex = 5;
			this.ListView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListView1_CellDoubleClick);
			// 
			// findButton
			// 
			this.findButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.findButton.Location = new System.Drawing.Point(5, 490);
			this.findButton.Name = "findButton";
			this.findButton.Size = new System.Drawing.Size(111, 24);
			this.findButton.TabIndex = 6;
			this.findButton.Text = "検索";
			this.findButton.UseVisualStyleBackColor = true;
			this.findButton.Click += new System.EventHandler(this.findButton_Click);
			// 
			// printButton
			// 
			this.printButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.printButton.Location = new System.Drawing.Point(860, 490);
			this.printButton.Name = "printButton";
			this.printButton.Size = new System.Drawing.Size(80, 24);
			this.printButton.TabIndex = 8;
			this.printButton.Text = "印刷";
			this.printButton.UseVisualStyleBackColor = true;
			this.printButton.Click += new System.EventHandler(this.printButton_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.inOut2);
			this.groupBox1.Controls.Add(this.inOut1);
			this.groupBox1.Controls.Add(this.inOut0);
			this.groupBox1.Location = new System.Drawing.Point(5, 95);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(111, 85);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "入外区分";
			// 
			// inOut2
			// 
			this.inOut2.AutoSize = true;
			this.inOut2.Location = new System.Drawing.Point(26, 63);
			this.inOut2.Name = "inOut2";
			this.inOut2.Size = new System.Drawing.Size(47, 16);
			this.inOut2.TabIndex = 11;
			this.inOut2.TabStop = true;
			this.inOut2.Text = "入院";
			this.inOut2.UseVisualStyleBackColor = true;
			// 
			// inOut1
			// 
			this.inOut1.AutoSize = true;
			this.inOut1.Location = new System.Drawing.Point(26, 41);
			this.inOut1.Name = "inOut1";
			this.inOut1.Size = new System.Drawing.Size(47, 16);
			this.inOut1.TabIndex = 10;
			this.inOut1.TabStop = true;
			this.inOut1.Text = "外来";
			this.inOut1.UseVisualStyleBackColor = true;
			// 
			// inOut0
			// 
			this.inOut0.AutoSize = true;
			this.inOut0.Location = new System.Drawing.Point(26, 19);
			this.inOut0.Name = "inOut0";
			this.inOut0.Size = new System.Drawing.Size(52, 16);
			this.inOut0.TabIndex = 0;
			this.inOut0.TabStop = true;
			this.inOut0.Text = "すべて";
			this.inOut0.UseVisualStyleBackColor = true;
			// 
			// countLabel
			// 
			this.countLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.countLabel.AutoSize = true;
			this.countLabel.Location = new System.Drawing.Point(840, 10);
			this.countLabel.Name = "countLabel";
			this.countLabel.Size = new System.Drawing.Size(53, 12);
			this.countLabel.TabIndex = 10;
			this.countLabel.Text = "検索件数";
			// 
			// deptBox
			// 
			this.deptBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.deptBox.CheckOnClick = true;
			this.deptBox.FormattingEnabled = true;
			this.deptBox.Location = new System.Drawing.Point(5, 200);
			this.deptBox.Name = "deptBox";
			this.deptBox.Size = new System.Drawing.Size(112, 256);
			this.deptBox.TabIndex = 11;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 185);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 12);
			this.label4.TabIndex = 12;
			this.label4.Text = "診療科";
			// 
			// selectAllButton
			// 
			this.selectAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.selectAllButton.Location = new System.Drawing.Point(5, 460);
			this.selectAllButton.Name = "selectAllButton";
			this.selectAllButton.Size = new System.Drawing.Size(53, 24);
			this.selectAllButton.TabIndex = 13;
			this.selectAllButton.Text = "全選択";
			this.selectAllButton.UseVisualStyleBackColor = true;
			this.selectAllButton.Click += new System.EventHandler(this.selectAllButton_Click);
			// 
			// selectNoButton
			// 
			this.selectNoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.selectNoButton.Location = new System.Drawing.Point(63, 460);
			this.selectNoButton.Name = "selectNoButton";
			this.selectNoButton.Size = new System.Drawing.Size(53, 24);
			this.selectNoButton.TabIndex = 14;
			this.selectNoButton.Text = "全解除";
			this.selectNoButton.UseVisualStyleBackColor = true;
			this.selectNoButton.Click += new System.EventHandler(this.selectNoButton_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(130, 10);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(112, 12);
			this.label5.TabIndex = 15;
			this.label5.Text = "絞込（ID, 氏名, 内容）";
			// 
			// printDocument1
			// 
			this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
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
			// printDialog1
			// 
			this.printDialog1.UseEXDialog = true;
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Location = new System.Drawing.Point(5, 28);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(110, 19);
			this.dateTimePicker1.TabIndex = 16;
			// 
			// dateTimePicker2
			// 
			this.dateTimePicker2.Location = new System.Drawing.Point(5, 68);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.Size = new System.Drawing.Size(110, 19);
			this.dateTimePicker2.TabIndex = 17;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox2.Controls.Add(this.touseki2);
			this.groupBox2.Controls.Add(this.touseki1);
			this.groupBox2.Controls.Add(this.touseki0);
			this.groupBox2.Location = new System.Drawing.Point(147, 484);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(255, 38);
			this.groupBox2.TabIndex = 18;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "透析・在宅・引落患者";
			// 
			// touseki2
			// 
			this.touseki2.AutoSize = true;
			this.touseki2.Location = new System.Drawing.Point(166, 17);
			this.touseki2.Name = "touseki2";
			this.touseki2.Size = new System.Drawing.Size(76, 16);
			this.touseki2.TabIndex = 11;
			this.touseki2.TabStop = true;
			this.touseki2.Text = "表示しない";
			this.touseki2.UseVisualStyleBackColor = true;
			this.touseki2.CheckedChanged += new System.EventHandler(this.touseki2_CheckedChanged);
			// 
			// touseki1
			// 
			this.touseki1.AutoSize = true;
			this.touseki1.Location = new System.Drawing.Point(81, 17);
			this.touseki1.Name = "touseki1";
			this.touseki1.Size = new System.Drawing.Size(66, 16);
			this.touseki1.TabIndex = 10;
			this.touseki1.TabStop = true;
			this.touseki1.Text = "表示する";
			this.touseki1.UseVisualStyleBackColor = true;
			this.touseki1.CheckedChanged += new System.EventHandler(this.touseki1_CheckedChanged);
			// 
			// touseki0
			// 
			this.touseki0.AutoSize = true;
			this.touseki0.Location = new System.Drawing.Point(12, 17);
			this.touseki0.Name = "touseki0";
			this.touseki0.Size = new System.Drawing.Size(52, 16);
			this.touseki0.TabIndex = 0;
			this.touseki0.TabStop = true;
			this.touseki0.Text = "すべて";
			this.touseki0.UseVisualStyleBackColor = true;
			this.touseki0.CheckedChanged += new System.EventHandler(this.touseki0_CheckedChanged);
			// 
			// FilterBox
			// 
			this.FilterBox.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
			this.FilterBox.Location = new System.Drawing.Point(245, 6);
			this.FilterBox.MaxLength = 20;
			this.FilterBox.Name = "FilterBox";
			this.FilterBox.Size = new System.Drawing.Size(120, 19);
			this.FilterBox.TabIndex = 19;
			this.FilterBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FilterBox_KeyDown);
			// 
			// ExcludeBox
			// 
			this.ExcludeBox.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
			this.ExcludeBox.Location = new System.Drawing.Point(445, 6);
			this.ExcludeBox.MaxLength = 20;
			this.ExcludeBox.Name = "ExcludeBox";
			this.ExcludeBox.Size = new System.Drawing.Size(220, 19);
			this.ExcludeBox.TabIndex = 21;
			this.ExcludeBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ExcludeBox_KeyDown);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(375, 10);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(65, 12);
			this.label6.TabIndex = 20;
			this.label6.Text = "除外（内容）";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(670, 10);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(133, 12);
			this.label7.TabIndex = 22;
			this.label7.Text = "※スペース区切りで複数可";
			// 
			// excelButton
			// 
			this.excelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.excelButton.Location = new System.Drawing.Point(770, 490);
			this.excelButton.Name = "excelButton";
			this.excelButton.Size = new System.Drawing.Size(80, 24);
			this.excelButton.TabIndex = 23;
			this.excelButton.Text = "Excel";
			this.excelButton.UseVisualStyleBackColor = true;
			this.excelButton.Click += new System.EventHandler(this.excelButton_Click);
			// 
			// ExcludeCodesLabel
			// 
			this.ExcludeCodesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ExcludeCodesLabel.AutoSize = true;
			this.ExcludeCodesLabel.Location = new System.Drawing.Point(435, 490);
			this.ExcludeCodesLabel.Name = "ExcludeCodesLabel";
			this.ExcludeCodesLabel.Size = new System.Drawing.Size(248, 24);
			this.ExcludeCodesLabel.TabIndex = 24;
			this.ExcludeCodesLabel.Text = "※除外指定されているオーダーコードを確認するには\r\n　　こちらをクリックしてください";
			this.ExcludeCodesLabel.Click += new System.EventHandler(this.ExcludeCodesLabel_Click);
			// 
			// ReceBillForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(944, 523);
			this.Controls.Add(this.ExcludeCodesLabel);
			this.Controls.Add(this.excelButton);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.ExcludeBox);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.FilterBox);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.dateTimePicker2);
			this.Controls.Add(this.dateTimePicker1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.selectNoButton);
			this.Controls.Add(this.selectAllButton);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.deptBox);
			this.Controls.Add(this.countLabel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.printButton);
			this.Controls.Add(this.findButton);
			this.Controls.Add(this.ListView1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ReceBillForm";
			this.Text = "会計未取込オーダー";
			this.Load += new System.EventHandler(this.ReceBillForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.ListView1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView ListView1;
        private System.Windows.Forms.Button findButton;
        private System.Windows.Forms.Button printButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton inOut2;
        private System.Windows.Forms.RadioButton inOut1;
        private System.Windows.Forms.RadioButton inOut0;
        private System.Windows.Forms.Label countLabel;
        private System.Windows.Forms.CheckedListBox deptBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button selectAllButton;
        private System.Windows.Forms.Button selectNoButton;
        private System.Windows.Forms.Label label5;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton touseki2;
        private System.Windows.Forms.RadioButton touseki1;
        private System.Windows.Forms.RadioButton touseki0;
        private System.Windows.Forms.TextBox FilterBox;
        private System.Windows.Forms.TextBox ExcludeBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button excelButton;
        private System.Windows.Forms.Label ExcludeCodesLabel;
    }
}

