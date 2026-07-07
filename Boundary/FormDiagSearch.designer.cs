namespace MedicalLibrary.Boundary
{
    partial class FormDiagSearch
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDiagSearch));
            this.DatePicker1 = new System.Windows.Forms.DateTimePicker();
            this.DatePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.searchButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ListView = new System.Windows.Forms.DataGridView();
            this.DiagBox1 = new System.Windows.Forms.ComboBox();
            this.excelButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.KensaBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DeptListBox = new System.Windows.Forms.CheckedListBox();
            this.NumLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.DiagBox2 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.KensaBox2 = new System.Windows.Forms.ComboBox();
            this.KensaBox3 = new System.Windows.Forms.ComboBox();
            this.DiagBox3 = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.DoubtBox = new System.Windows.Forms.CheckBox();
            this.DiagDataPanel = new System.Windows.Forms.Panel();
            this.DiagDateButton3 = new System.Windows.Forms.RadioButton();
            this.DiagDateButton2 = new System.Windows.Forms.RadioButton();
            this.DiagDateButton1 = new System.Windows.Forms.RadioButton();
            this.FilterBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ListView)).BeginInit();
            this.DiagDataPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // DatePicker1
            // 
            this.DatePicker1.Location = new System.Drawing.Point(145, 56);
            this.DatePicker1.MinDate = new System.DateTime(1975, 1, 1, 0, 0, 0, 0);
            this.DatePicker1.Name = "DatePicker1";
            this.DatePicker1.Size = new System.Drawing.Size(122, 19);
            this.DatePicker1.TabIndex = 12;
            // 
            // DatePicker2
            // 
            this.DatePicker2.Location = new System.Drawing.Point(290, 56);
            this.DatePicker2.MinDate = new System.DateTime(1975, 1, 1, 0, 0, 0, 0);
            this.DatePicker2.Name = "DatePicker2";
            this.DatePicker2.Size = new System.Drawing.Size(104, 19);
            this.DatePicker2.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(270, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "～";
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(400, 155);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(60, 25);
            this.searchButton.TabIndex = 51;
            this.searchButton.Text = "検索";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(415, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "病名";
            // 
            // ListView
            // 
            this.ListView.AllowUserToAddRows = false;
            this.ListView.AllowUserToDeleteRows = false;
            this.ListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.ListView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ListView.DefaultCellStyle = dataGridViewCellStyle2;
            this.ListView.Location = new System.Drawing.Point(5, 185);
            this.ListView.MultiSelect = false;
            this.ListView.Name = "ListView";
            this.ListView.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.ListView.RowHeadersVisible = false;
            this.ListView.RowTemplate.Height = 21;
            this.ListView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ListView.Size = new System.Drawing.Size(1175, 570);
            this.ListView.TabIndex = 19;
            this.ListView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListView_CellDoubleClick);
            this.ListView.Sorted += new System.EventHandler(this.ListView_Sorted);
            // 
            // DiagBox1
            // 
            this.DiagBox1.FormattingEnabled = true;
            this.DiagBox1.Location = new System.Drawing.Point(415, 30);
            this.DiagBox1.Name = "DiagBox1";
            this.DiagBox1.Size = new System.Drawing.Size(140, 20);
            this.DiagBox1.TabIndex = 21;
            // 
            // excelButton
            // 
            this.excelButton.Location = new System.Drawing.Point(465, 155);
            this.excelButton.Name = "excelButton";
            this.excelButton.Size = new System.Drawing.Size(60, 25);
            this.excelButton.TabIndex = 52;
            this.excelButton.Text = "excel";
            this.excelButton.UseVisualStyleBackColor = true;
            this.excelButton.Click += new System.EventHandler(this.excelButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(570, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 23;
            this.label2.Text = "検査名";
            // 
            // KensaBox1
            // 
            this.KensaBox1.FormattingEnabled = true;
            this.KensaBox1.Location = new System.Drawing.Point(575, 30);
            this.KensaBox1.Name = "KensaBox1";
            this.KensaBox1.Size = new System.Drawing.Size(140, 20);
            this.KensaBox1.TabIndex = 31;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(188, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "病名と検査の対象患者を検索します。";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 12);
            this.label4.TabIndex = 26;
            this.label4.Text = "対象期間（最長１年間）";
            // 
            // DeptListBox
            // 
            this.DeptListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DeptListBox.FormattingEnabled = true;
            this.DeptListBox.Location = new System.Drawing.Point(730, 30);
            this.DeptListBox.MultiColumn = true;
            this.DeptListBox.Name = "DeptListBox";
            this.DeptListBox.Size = new System.Drawing.Size(450, 144);
            this.DeptListBox.TabIndex = 41;
            // 
            // NumLabel
            // 
            this.NumLabel.AutoEllipsis = true;
            this.NumLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.NumLabel.Location = new System.Drawing.Point(70, 155);
            this.NumLabel.Name = "NumLabel";
            this.NumLabel.Size = new System.Drawing.Size(320, 23);
            this.NumLabel.TabIndex = 28;
            this.NumLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 29;
            this.label6.Text = "検索結果";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(730, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(153, 12);
            this.label7.TabIndex = 30;
            this.label7.Text = "最終受診日を検索する診療科";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(370, 12);
            this.label8.TabIndex = 31;
            this.label8.Text = "※　検査名はアルファベットの大文字・小文字、全角・半角は区別されません。";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 110);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(393, 12);
            this.label9.TabIndex = 32;
            this.label9.Text = "※　診療科の最終受診日は、対象期間に関係なく、最後の受診日を表示します。";
            // 
            // DiagBox2
            // 
            this.DiagBox2.FormattingEnabled = true;
            this.DiagBox2.Location = new System.Drawing.Point(415, 55);
            this.DiagBox2.Name = "DiagBox2";
            this.DiagBox2.Size = new System.Drawing.Size(140, 20);
            this.DiagBox2.TabIndex = 22;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 130);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(537, 12);
            this.label10.TabIndex = 34;
            this.label10.Text = "※　対象者が多すぎると検索できない場合があります。その場合は期間を短くするなどして対象者を絞ってください。";
            // 
            // KensaBox2
            // 
            this.KensaBox2.FormattingEnabled = true;
            this.KensaBox2.Location = new System.Drawing.Point(575, 55);
            this.KensaBox2.Name = "KensaBox2";
            this.KensaBox2.Size = new System.Drawing.Size(140, 20);
            this.KensaBox2.TabIndex = 32;
            // 
            // KensaBox3
            // 
            this.KensaBox3.FormattingEnabled = true;
            this.KensaBox3.Location = new System.Drawing.Point(575, 80);
            this.KensaBox3.Name = "KensaBox3";
            this.KensaBox3.Size = new System.Drawing.Size(140, 20);
            this.KensaBox3.TabIndex = 33;
            // 
            // DiagBox3
            // 
            this.DiagBox3.FormattingEnabled = true;
            this.DiagBox3.Location = new System.Drawing.Point(415, 80);
            this.DiagBox3.Name = "DiagBox3";
            this.DiagBox3.Size = new System.Drawing.Size(140, 20);
            this.DiagBox3.TabIndex = 23;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(311, 12);
            this.label11.TabIndex = 38;
            this.label11.Text = "その患者の科ごとの最終受診日も検索できます。（必要に応じて）";
            // 
            // DoubtBox
            // 
            this.DoubtBox.AutoSize = true;
            this.DoubtBox.Location = new System.Drawing.Point(475, 8);
            this.DoubtBox.Name = "DoubtBox";
            this.DoubtBox.Size = new System.Drawing.Size(77, 16);
            this.DoubtBox.TabIndex = 53;
            this.DoubtBox.Text = "疑いを含む";
            this.DoubtBox.UseVisualStyleBackColor = true;
            // 
            // DiagDataPanel
            // 
            this.DiagDataPanel.Controls.Add(this.DiagDateButton3);
            this.DiagDataPanel.Controls.Add(this.DiagDateButton2);
            this.DiagDataPanel.Controls.Add(this.DiagDateButton1);
            this.DiagDataPanel.Location = new System.Drawing.Point(415, 102);
            this.DiagDataPanel.Name = "DiagDataPanel";
            this.DiagDataPanel.Size = new System.Drawing.Size(210, 25);
            this.DiagDataPanel.TabIndex = 54;
            // 
            // DiagDateButton3
            // 
            this.DiagDateButton3.AutoSize = true;
            this.DiagDateButton3.Location = new System.Drawing.Point(145, 5);
            this.DiagDateButton3.Name = "DiagDateButton3";
            this.DiagDateButton3.Size = new System.Drawing.Size(59, 16);
            this.DiagDateButton3.TabIndex = 2;
            this.DiagDateButton3.TabStop = true;
            this.DiagDateButton3.Text = "未転帰";
            this.DiagDateButton3.UseVisualStyleBackColor = true;
            // 
            // DiagDateButton2
            // 
            this.DiagDateButton2.AutoSize = true;
            this.DiagDateButton2.Location = new System.Drawing.Point(75, 5);
            this.DiagDateButton2.Name = "DiagDateButton2";
            this.DiagDateButton2.Size = new System.Drawing.Size(59, 16);
            this.DiagDateButton2.TabIndex = 1;
            this.DiagDateButton2.TabStop = true;
            this.DiagDateButton2.Text = "登録日";
            this.DiagDateButton2.UseVisualStyleBackColor = true;
            // 
            // DiagDateButton1
            // 
            this.DiagDateButton1.AutoSize = true;
            this.DiagDateButton1.Location = new System.Drawing.Point(5, 5);
            this.DiagDateButton1.Name = "DiagDateButton1";
            this.DiagDateButton1.Size = new System.Drawing.Size(59, 16);
            this.DiagDateButton1.TabIndex = 0;
            this.DiagDateButton1.TabStop = true;
            this.DiagDateButton1.Text = "開始日";
            this.DiagDateButton1.UseVisualStyleBackColor = true;
            // 
            // FilterBox1
            // 
            this.FilterBox1.AutoSize = true;
            this.FilterBox1.Location = new System.Drawing.Point(550, 160);
            this.FilterBox1.Name = "FilterBox1";
            this.FilterBox1.Size = new System.Drawing.Size(135, 16);
            this.FilterBox1.TabIndex = 55;
            this.FilterBox1.Text = "検査していない人を除く";
            this.FilterBox1.UseVisualStyleBackColor = true;
            this.FilterBox1.CheckedChanged += new System.EventHandler(this.FilterBox1_CheckedChanged);
            // 
            // FormDiagSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 762);
            this.Controls.Add(this.FilterBox1);
            this.Controls.Add(this.DiagDataPanel);
            this.Controls.Add(this.DoubtBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.DiagBox3);
            this.Controls.Add(this.KensaBox3);
            this.Controls.Add(this.KensaBox2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.DiagBox2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.NumLabel);
            this.Controls.Add(this.DeptListBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.KensaBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.excelButton);
            this.Controls.Add(this.DiagBox1);
            this.Controls.Add(this.ListView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.DatePicker2);
            this.Controls.Add(this.DatePicker1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDiagSearch";
            this.Text = "病名検索";
            this.Load += new System.EventHandler(this.FormDiagSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListView)).EndInit();
            this.DiagDataPanel.ResumeLayout(false);
            this.DiagDataPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker DatePicker1;
        private System.Windows.Forms.DateTimePicker DatePicker2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView ListView;
        private System.Windows.Forms.ComboBox DiagBox1;
        private System.Windows.Forms.Button excelButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox KensaBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckedListBox DeptListBox;
        private System.Windows.Forms.Label NumLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox DiagBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox KensaBox2;
        private System.Windows.Forms.ComboBox KensaBox3;
        private System.Windows.Forms.ComboBox DiagBox3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox DoubtBox;
        private System.Windows.Forms.Panel DiagDataPanel;
        private System.Windows.Forms.RadioButton DiagDateButton2;
        private System.Windows.Forms.RadioButton DiagDateButton1;
        private System.Windows.Forms.CheckBox FilterBox1;
        private System.Windows.Forms.RadioButton DiagDateButton3;

    }
}

