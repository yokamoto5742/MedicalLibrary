namespace MedicalLibrary.Agent
{
    partial class FormQ26
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormQ26));
            this.ListView1 = new System.Windows.Forms.DataGridView();
            this.DatePicker1 = new System.Windows.Forms.DateTimePicker();
            this.ShinkuBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.FilterBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.InOutBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TmpPathLabel = new System.Windows.Forms.Label();
            this.SendButton = new System.Windows.Forms.Button();
            this.ListView2 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.SetKaikeiFlgBox = new System.Windows.Forms.CheckBox();
            this.DoneBox = new System.Windows.Forms.ComboBox();
            this.DstPathLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.DoujituRaiinsuBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.LogBox = new System.Windows.Forms.TextBox();
            this.ModeBox = new System.Windows.Forms.CheckBox();
            this.IntervalBox = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.ListShowButton = new System.Windows.Forms.Button();
            this.OrderLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListView2)).BeginInit();
            this.SuspendLayout();
            // 
            // ListView1
            // 
            this.ListView1.AllowUserToAddRows = false;
            this.ListView1.AllowUserToDeleteRows = false;
            this.ListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ListView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ListView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.ListView1.Location = new System.Drawing.Point(260, 30);
            this.ListView1.MultiSelect = false;
            this.ListView1.Name = "ListView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.ListView1.RowHeadersVisible = false;
            this.ListView1.RowTemplate.Height = 21;
            this.ListView1.Size = new System.Drawing.Size(1000, 415);
            this.ListView1.TabIndex = 1;
            // 
            // DatePicker1
            // 
            this.DatePicker1.CustomFormat = "yyyy/MM/dd (ddd)";
            this.DatePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatePicker1.Location = new System.Drawing.Point(50, 5);
            this.DatePicker1.Name = "DatePicker1";
            this.DatePicker1.Size = new System.Drawing.Size(125, 19);
            this.DatePicker1.TabIndex = 3;
            this.DatePicker1.ValueChanged += new System.EventHandler(this.DatePicker1_ValueChanged);
            // 
            // ShinkuBox
            // 
            this.ShinkuBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ShinkuBox.FormattingEnabled = true;
            this.ShinkuBox.Location = new System.Drawing.Point(495, 6);
            this.ShinkuBox.Name = "ShinkuBox";
            this.ShinkuBox.Size = new System.Drawing.Size(80, 20);
            this.ShinkuBox.TabIndex = 5;
            this.ShinkuBox.TextChanged += new System.EventHandler(this.ShinkuBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(465, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "診区";
            // 
            // FilterBox
            // 
            this.FilterBox.Location = new System.Drawing.Point(615, 6);
            this.FilterBox.Name = "FilterBox";
            this.FilterBox.Size = new System.Drawing.Size(90, 19);
            this.FilterBox.TabIndex = 7;
            this.FilterBox.TextChanged += new System.EventHandler(this.FilterBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(585, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "検索";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(365, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "入外";
            // 
            // InOutBox
            // 
            this.InOutBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InOutBox.FormattingEnabled = true;
            this.InOutBox.Location = new System.Drawing.Point(395, 6);
            this.InOutBox.Name = "InOutBox";
            this.InOutBox.Size = new System.Drawing.Size(60, 20);
            this.InOutBox.TabIndex = 9;
            this.InOutBox.TextChanged += new System.EventHandler(this.InOutBox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "出力先";
            // 
            // TmpPathLabel
            // 
            this.TmpPathLabel.BackColor = System.Drawing.Color.White;
            this.TmpPathLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TmpPathLabel.Location = new System.Drawing.Point(50, 70);
            this.TmpPathLabel.Name = "TmpPathLabel";
            this.TmpPathLabel.Size = new System.Drawing.Size(200, 18);
            this.TmpPathLabel.TabIndex = 14;
            this.TmpPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TmpPathLabel.Click += new System.EventHandler(this.TmpPathLabel_Click);
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(1210, 5);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(50, 23);
            this.SendButton.TabIndex = 15;
            this.SendButton.Text = "送信";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // ListView2
            // 
            this.ListView2.AllowUserToAddRows = false;
            this.ListView2.AllowUserToDeleteRows = false;
            this.ListView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ListView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.ListView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListView2.DefaultCellStyle = dataGridViewCellStyle5;
            this.ListView2.Location = new System.Drawing.Point(260, 470);
            this.ListView2.MultiSelect = false;
            this.ListView2.Name = "ListView2";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.ListView2.RowHeadersVisible = false;
            this.ListView2.RowTemplate.Height = 21;
            this.ListView2.Size = new System.Drawing.Size(1000, 170);
            this.ListView2.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(265, 452);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "削除オーダー";
            // 
            // SetKaikeiFlgBox
            // 
            this.SetKaikeiFlgBox.AutoSize = true;
            this.SetKaikeiFlgBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SetKaikeiFlgBox.Location = new System.Drawing.Point(880, 9);
            this.SetKaikeiFlgBox.Name = "SetKaikeiFlgBox";
            this.SetKaikeiFlgBox.Size = new System.Drawing.Size(152, 16);
            this.SetKaikeiFlgBox.TabIndex = 20;
            this.SetKaikeiFlgBox.Text = "送信後に会計フラグをセット";
            this.SetKaikeiFlgBox.UseVisualStyleBackColor = true;
            // 
            // DoneBox
            // 
            this.DoneBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DoneBox.FormattingEnabled = true;
            this.DoneBox.Location = new System.Drawing.Point(295, 6);
            this.DoneBox.Name = "DoneBox";
            this.DoneBox.Size = new System.Drawing.Size(60, 20);
            this.DoneBox.TabIndex = 21;
            this.DoneBox.TextChanged += new System.EventHandler(this.DoneBox_TextChanged);
            // 
            // DstPathLabel
            // 
            this.DstPathLabel.BackColor = System.Drawing.Color.White;
            this.DstPathLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DstPathLabel.Location = new System.Drawing.Point(50, 92);
            this.DstPathLabel.Name = "DstPathLabel";
            this.DstPathLabel.Size = new System.Drawing.Size(200, 18);
            this.DstPathLabel.TabIndex = 23;
            this.DstPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DstPathLabel.Click += new System.EventHandler(this.DstPathLabel_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 22;
            this.label8.Text = "移動先";
            // 
            // DoujituRaiinsuBox
            // 
            this.DoujituRaiinsuBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DoujituRaiinsuBox.FormattingEnabled = true;
            this.DoujituRaiinsuBox.Location = new System.Drawing.Point(830, 6);
            this.DoujituRaiinsuBox.Name = "DoujituRaiinsuBox";
            this.DoujituRaiinsuBox.Size = new System.Drawing.Size(40, 20);
            this.DoujituRaiinsuBox.TabIndex = 24;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(715, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 12);
            this.label7.TabIndex = 25;
            this.label7.Text = "同日来院回数（外来）";
            // 
            // LogBox
            // 
            this.LogBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LogBox.BackColor = System.Drawing.Color.White;
            this.LogBox.Location = new System.Drawing.Point(5, 140);
            this.LogBox.MaxLength = 10000;
            this.LogBox.Multiline = true;
            this.LogBox.Name = "LogBox";
            this.LogBox.ReadOnly = true;
            this.LogBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.LogBox.Size = new System.Drawing.Size(245, 500);
            this.LogBox.TabIndex = 26;
            // 
            // ModeBox
            // 
            this.ModeBox.AutoSize = true;
            this.ModeBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ModeBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ModeBox.ForeColor = System.Drawing.Color.Red;
            this.ModeBox.Location = new System.Drawing.Point(5, 120);
            this.ModeBox.Name = "ModeBox";
            this.ModeBox.Size = new System.Drawing.Size(72, 16);
            this.ModeBox.TabIndex = 27;
            this.ModeBox.Text = "自動送信";
            this.ModeBox.UseVisualStyleBackColor = false;
            this.ModeBox.CheckedChanged += new System.EventHandler(this.ModeBox_CheckedChanged);
            // 
            // IntervalBox
            // 
            this.IntervalBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.IntervalBox.FormattingEnabled = true;
            this.IntervalBox.Location = new System.Drawing.Point(110, 117);
            this.IntervalBox.Name = "IntervalBox";
            this.IntervalBox.Size = new System.Drawing.Size(50, 20);
            this.IntervalBox.TabIndex = 29;
            this.IntervalBox.TextChanged += new System.EventHandler(this.IntervalBox_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 30;
            this.label9.Text = "基準日";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(165, 122);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 12);
            this.label10.TabIndex = 31;
            this.label10.Text = "秒おき";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(265, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 32;
            this.label11.Text = "対象";
            // 
            // ListShowButton
            // 
            this.ListShowButton.Location = new System.Drawing.Point(175, 3);
            this.ListShowButton.Name = "ListShowButton";
            this.ListShowButton.Size = new System.Drawing.Size(75, 23);
            this.ListShowButton.TabIndex = 33;
            this.ListShowButton.Text = "リスト表示";
            this.ListShowButton.UseVisualStyleBackColor = true;
            this.ListShowButton.Click += new System.EventHandler(this.ListShowButton_Click);
            // 
            // OrderLabel
            // 
            this.OrderLabel.BackColor = System.Drawing.Color.LightYellow;
            this.OrderLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.OrderLabel.Location = new System.Drawing.Point(10, 30);
            this.OrderLabel.Name = "OrderLabel";
            this.OrderLabel.Size = new System.Drawing.Size(240, 35);
            this.OrderLabel.TabIndex = 34;
            this.OrderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormQ26
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 642);
            this.Controls.Add(this.OrderLabel);
            this.Controls.Add(this.ListShowButton);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.IntervalBox);
            this.Controls.Add(this.ModeBox);
            this.Controls.Add(this.LogBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.DoujituRaiinsuBox);
            this.Controls.Add(this.DstPathLabel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.DoneBox);
            this.Controls.Add(this.SetKaikeiFlgBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListView2);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.TmpPathLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.InOutBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.FilterBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ShinkuBox);
            this.Controls.Add(this.DatePicker1);
            this.Controls.Add(this.ListView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormQ26";
            this.Text = "Proasオーダー連携";
            this.Load += new System.EventHandler(this.FormQ26_Load);
            this.Shown += new System.EventHandler(this.FormQ26_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ListView1;
        private System.Windows.Forms.DateTimePicker DatePicker1;
        private System.Windows.Forms.ComboBox ShinkuBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox FilterBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox InOutBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label TmpPathLabel;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.DataGridView ListView2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox SetKaikeiFlgBox;
        private System.Windows.Forms.ComboBox DoneBox;
        private System.Windows.Forms.Label DstPathLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox DoujituRaiinsuBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox LogBox;
		private System.Windows.Forms.CheckBox ModeBox;
        private System.Windows.Forms.ComboBox IntervalBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Button ListShowButton;
        private System.Windows.Forms.Label OrderLabel;
    }
}

