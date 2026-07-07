namespace MedicalLibrary.Boundary
{
    partial class FormDPCList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDPCList));
            this.ImportButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.DatePicker21 = new System.Windows.Forms.DateTimePicker();
            this.ShowButton = new System.Windows.Forms.Button();
            this.ListView1 = new System.Windows.Forms.DataGridView();
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.PrrismButton1 = new System.Windows.Forms.Button();
            this.CSVButton1 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.DateBox11 = new MedicalLibrary.Boundary.CtrlDateBox1();
            this.CountLabel1 = new System.Windows.Forms.Label();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.PrrismButton2 = new System.Windows.Forms.Button();
            this.CSVButton2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.DatePicker22 = new System.Windows.Forms.DateTimePicker();
            this.CountLabel2 = new System.Windows.Forms.Label();
            this.ListView2 = new System.Windows.Forms.DataGridView();
            this.WardBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.FilterBox = new System.Windows.Forms.TextBox();
            this.DoctorBox = new MedicalLibrary.Boundary.CtrlDoctorBox1();
            this.stdControlFont11 = new MedicalLibrary.Boundary.StdControlFont1();
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).BeginInit();
            this.TabControl1.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListView2)).BeginInit();
            this.SuspendLayout();
            // 
            // ImportButton
            // 
            this.ImportButton.Location = new System.Drawing.Point(960, 4);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(75, 23);
            this.ImportButton.TabIndex = 14;
            this.ImportButton.Text = "Import";
            this.ImportButton.UseVisualStyleBackColor = true;
            this.ImportButton.Click += new System.EventHandler(this.ImportButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "退院日";
            // 
            // DatePicker21
            // 
            this.DatePicker21.CustomFormat = "yyyy/M/d (ddd)";
            this.DatePicker21.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatePicker21.Location = new System.Drawing.Point(60, 7);
            this.DatePicker21.Name = "DatePicker21";
            this.DatePicker21.Size = new System.Drawing.Size(130, 19);
            this.DatePicker21.TabIndex = 10;
            // 
            // ShowButton
            // 
            this.ShowButton.Location = new System.Drawing.Point(840, 4);
            this.ShowButton.Name = "ShowButton";
            this.ShowButton.Size = new System.Drawing.Size(75, 23);
            this.ShowButton.TabIndex = 9;
            this.ShowButton.Text = "表示";
            this.ShowButton.UseVisualStyleBackColor = true;
            this.ShowButton.Click += new System.EventHandler(this.ShowButton_Click);
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ListView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ListView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListView1.Location = new System.Drawing.Point(5, 30);
            this.ListView1.MultiSelect = false;
            this.ListView1.Name = "ListView1";
            this.ListView1.RowHeadersVisible = false;
            this.ListView1.RowTemplate.Height = 21;
            this.ListView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ListView1.Size = new System.Drawing.Size(1235, 489);
            this.ListView1.TabIndex = 8;
            this.ListView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListView1_CellDoubleClick);
            this.ListView1.Sorted += new System.EventHandler(this.ListView1_Sorted);
            // 
            // TabControl1
            // 
            this.TabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl1.Controls.Add(this.TabPage1);
            this.TabControl1.Controls.Add(this.TabPage2);
            this.TabControl1.Location = new System.Drawing.Point(5, 10);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(1255, 549);
            this.TabControl1.TabIndex = 16;
            this.TabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControl1_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.Controls.Add(this.PrrismButton1);
            this.TabPage1.Controls.Add(this.CSVButton1);
            this.TabPage1.Controls.Add(this.label9);
            this.TabPage1.Controls.Add(this.DateBox11);
            this.TabPage1.Controls.Add(this.CountLabel1);
            this.TabPage1.Controls.Add(this.ListView1);
            this.TabPage1.Location = new System.Drawing.Point(4, 22);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1247, 523);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "入院患者";
            this.TabPage1.UseVisualStyleBackColor = true;
            // 
            // PrrismButton1
            // 
            this.PrrismButton1.Location = new System.Drawing.Point(950, 5);
            this.PrrismButton1.Name = "PrrismButton1";
            this.PrrismButton1.Size = new System.Drawing.Size(75, 23);
            this.PrrismButton1.TabIndex = 90;
            this.PrrismButton1.Text = "Prrism";
            this.PrrismButton1.UseVisualStyleBackColor = true;
            this.PrrismButton1.Click += new System.EventHandler(this.PrrismButton1_Click);
            // 
            // CSVButton1
            // 
            this.CSVButton1.Location = new System.Drawing.Point(550, 5);
            this.CSVButton1.Name = "CSVButton1";
            this.CSVButton1.Size = new System.Drawing.Size(75, 23);
            this.CSVButton1.TabIndex = 89;
            this.CSVButton1.Text = "CSV出力";
            this.CSVButton1.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 78;
            this.label9.Text = "入院日";
            // 
            // DateBox11
            // 
            this.DateBox11.DateInt = 0;
            this.DateBox11.DateString = "";
            this.DateBox11.Location = new System.Drawing.Point(60, 3);
            this.DateBox11.Name = "DateBox11";
            this.DateBox11.Size = new System.Drawing.Size(150, 26);
            this.DateBox11.TabIndex = 10;
            this.DateBox11.ValueChanged += new System.EventHandler<System.EventArgs>(this.DateBox11_ValueChanged);
            // 
            // CountLabel1
            // 
            this.CountLabel1.BackColor = System.Drawing.Color.LightYellow;
            this.CountLabel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.CountLabel1.Location = new System.Drawing.Point(400, 7);
            this.CountLabel1.Name = "CountLabel1";
            this.CountLabel1.Size = new System.Drawing.Size(120, 18);
            this.CountLabel1.TabIndex = 9;
            this.CountLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TabPage2
            // 
            this.TabPage2.Controls.Add(this.PrrismButton2);
            this.TabPage2.Controls.Add(this.CSVButton2);
            this.TabPage2.Controls.Add(this.label6);
            this.TabPage2.Controls.Add(this.DatePicker22);
            this.TabPage2.Controls.Add(this.CountLabel2);
            this.TabPage2.Controls.Add(this.ListView2);
            this.TabPage2.Controls.Add(this.DatePicker21);
            this.TabPage2.Controls.Add(this.label1);
            this.TabPage2.Location = new System.Drawing.Point(4, 22);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1247, 523);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "退院患者";
            this.TabPage2.UseVisualStyleBackColor = true;
            // 
            // PrrismButton2
            // 
            this.PrrismButton2.Location = new System.Drawing.Point(950, 5);
            this.PrrismButton2.Name = "PrrismButton2";
            this.PrrismButton2.Size = new System.Drawing.Size(75, 23);
            this.PrrismButton2.TabIndex = 90;
            this.PrrismButton2.Text = "Prrism";
            this.PrrismButton2.UseVisualStyleBackColor = true;
            this.PrrismButton2.Click += new System.EventHandler(this.PrrismButton2_Click);
            // 
            // CSVButton2
            // 
            this.CSVButton2.Location = new System.Drawing.Point(550, 5);
            this.CSVButton2.Name = "CSVButton2";
            this.CSVButton2.Size = new System.Drawing.Size(75, 23);
            this.CSVButton2.TabIndex = 89;
            this.CSVButton2.Text = "CSV出力";
            this.CSVButton2.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(195, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "～";
            // 
            // DatePicker22
            // 
            this.DatePicker22.CustomFormat = "yyyy/M/d (ddd)";
            this.DatePicker22.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatePicker22.Location = new System.Drawing.Point(215, 7);
            this.DatePicker22.Name = "DatePicker22";
            this.DatePicker22.Size = new System.Drawing.Size(130, 19);
            this.DatePicker22.TabIndex = 14;
            // 
            // CountLabel2
            // 
            this.CountLabel2.BackColor = System.Drawing.Color.LightYellow;
            this.CountLabel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.CountLabel2.Location = new System.Drawing.Point(400, 7);
            this.CountLabel2.Name = "CountLabel2";
            this.CountLabel2.Size = new System.Drawing.Size(120, 18);
            this.CountLabel2.TabIndex = 13;
            this.CountLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ListView2
            // 
            this.ListView2.AllowUserToAddRows = false;
            this.ListView2.AllowUserToDeleteRows = false;
            this.ListView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ListView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ListView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListView2.Location = new System.Drawing.Point(5, 30);
            this.ListView2.MultiSelect = false;
            this.ListView2.Name = "ListView2";
            this.ListView2.RowHeadersVisible = false;
            this.ListView2.RowTemplate.Height = 21;
            this.ListView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ListView2.Size = new System.Drawing.Size(1235, 490);
            this.ListView2.TabIndex = 9;
            this.ListView2.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListView2_CellDoubleClick);
            this.ListView2.Sorted += new System.EventHandler(this.ListView2_Sorted);
            // 
            // WardBox
            // 
            this.WardBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WardBox.FormattingEnabled = true;
            this.WardBox.Location = new System.Drawing.Point(275, 5);
            this.WardBox.Name = "WardBox";
            this.WardBox.Size = new System.Drawing.Size(121, 20);
            this.WardBox.TabIndex = 17;
            this.WardBox.TextChanged += new System.EventHandler(this.WardBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(240, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "病棟";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(420, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "担当医";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(600, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "絞込み（ID, 氏名）";
            // 
            // FilterBox
            // 
            this.FilterBox.Location = new System.Drawing.Point(700, 6);
            this.FilterBox.Name = "FilterBox";
            this.FilterBox.Size = new System.Drawing.Size(100, 19);
            this.FilterBox.TabIndex = 15;
            this.FilterBox.TextChanged += new System.EventHandler(this.FilterBox_TextChanged);
            // 
            // DoctorBox
            // 
            this.DoctorBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DoctorBox.FormattingEnabled = true;
            this.DoctorBox.Location = new System.Drawing.Point(465, 5);
            this.DoctorBox.Name = "DoctorBox";
            this.DoctorBox.Size = new System.Drawing.Size(121, 20);
            this.DoctorBox.TabIndex = 18;
            this.DoctorBox.TextChanged += new System.EventHandler(this.DoctorBox_TextChanged);
            // 
            // stdControlFont11
            // 
            this.stdControlFont11.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.stdControlFont11.Location = new System.Drawing.Point(160, 0);
            this.stdControlFont11.Name = "stdControlFont11";
            this.stdControlFont11.Size = new System.Drawing.Size(60, 30);
            this.stdControlFont11.TabIndex = 20;
            // 
            // FormDPCList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 561);
            this.Controls.Add(this.stdControlFont11);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.FilterBox);
            this.Controls.Add(this.DoctorBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ShowButton);
            this.Controls.Add(this.WardBox);
            this.Controls.Add(this.ImportButton);
            this.Controls.Add(this.TabControl1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDPCList";
            this.Text = "DPCデータ";
            this.Load += new System.EventHandler(this.FormDPCList_Load);
            this.Shown += new System.EventHandler(this.FormDPCList_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).EndInit();
            this.TabControl1.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ImportButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker DatePicker21;
        private System.Windows.Forms.Button ShowButton;
        private System.Windows.Forms.DataGridView ListView1;
        private System.Windows.Forms.TabControl TabControl1;
        private System.Windows.Forms.TabPage TabPage1;
        private System.Windows.Forms.TabPage TabPage2;
        private System.Windows.Forms.DataGridView ListView2;
        private System.Windows.Forms.ComboBox WardBox;
        private System.Windows.Forms.Label label2;
        private CtrlDoctorBox1 DoctorBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox FilterBox;
        private System.Windows.Forms.Label CountLabel1;
        private System.Windows.Forms.Label CountLabel2;
        private System.Windows.Forms.DateTimePicker DatePicker22;
        private System.Windows.Forms.Label label6;
        private CtrlDateBox1 DateBox11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button CSVButton1;
        private System.Windows.Forms.Button CSVButton2;
        private StdControlFont1 stdControlFont11;
        private System.Windows.Forms.Button PrrismButton1;
        private System.Windows.Forms.Button PrrismButton2;
    }
}