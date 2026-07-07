namespace MedicalLibrary.Boundary
{
    partial class FormRsvPatList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRsvPatList));
            this.MasterListPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.MasterSelectPanel = new System.Windows.Forms.Panel();
            this.LastPBox7 = new System.Windows.Forms.CheckBox();
            this.OtherBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MasterSelectCloseButton = new System.Windows.Forms.Button();
            this.MasterSelectShowButton = new System.Windows.Forms.Button();
            this.ListView = new System.Windows.Forms.DataGridView();
            this.DatePicker1 = new System.Windows.Forms.DateTimePicker();
            this.ExcelButton = new System.Windows.Forms.Button();
            this.MasterSelectPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListView)).BeginInit();
            this.SuspendLayout();
            // 
            // MasterListPanel
            // 
            this.MasterListPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MasterListPanel.AutoScroll = true;
            this.MasterListPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.MasterListPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MasterListPanel.Location = new System.Drawing.Point(5, 30);
            this.MasterListPanel.Name = "MasterListPanel";
            this.MasterListPanel.Size = new System.Drawing.Size(990, 325);
            this.MasterListPanel.TabIndex = 0;
            // 
            // MasterSelectPanel
            // 
            this.MasterSelectPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.MasterSelectPanel.Controls.Add(this.LastPBox7);
            this.MasterSelectPanel.Controls.Add(this.OtherBox);
            this.MasterSelectPanel.Controls.Add(this.label1);
            this.MasterSelectPanel.Controls.Add(this.MasterSelectCloseButton);
            this.MasterSelectPanel.Controls.Add(this.MasterListPanel);
            this.MasterSelectPanel.Location = new System.Drawing.Point(30, 60);
            this.MasterSelectPanel.Name = "MasterSelectPanel";
            this.MasterSelectPanel.Size = new System.Drawing.Size(1000, 360);
            this.MasterSelectPanel.TabIndex = 1;
            // 
            // LastPBox7
            // 
            this.LastPBox7.AutoSize = true;
            this.LastPBox7.Location = new System.Drawing.Point(500, 10);
            this.LastPBox7.Name = "LastPBox7";
            this.LastPBox7.Size = new System.Drawing.Size(158, 16);
            this.LastPBox7.TabIndex = 3;
            this.LastPBox7.Text = "眼科の最終受診日Pも表示";
            this.LastPBox7.UseVisualStyleBackColor = true;
            // 
            // OtherBox
            // 
            this.OtherBox.AutoSize = true;
            this.OtherBox.Location = new System.Drawing.Point(280, 10);
            this.OtherBox.Name = "OtherBox";
            this.OtherBox.Size = new System.Drawing.Size(153, 16);
            this.OtherBox.TabIndex = 0;
            this.OtherBox.Text = "選択していない検査も表示";
            this.OtherBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "一覧表示する予約種別を選択してください";
            // 
            // MasterSelectCloseButton
            // 
            this.MasterSelectCloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MasterSelectCloseButton.Location = new System.Drawing.Point(915, 5);
            this.MasterSelectCloseButton.Name = "MasterSelectCloseButton";
            this.MasterSelectCloseButton.Size = new System.Drawing.Size(75, 23);
            this.MasterSelectCloseButton.TabIndex = 1;
            this.MasterSelectCloseButton.Text = "閉じる";
            this.MasterSelectCloseButton.UseVisualStyleBackColor = true;
            this.MasterSelectCloseButton.Click += new System.EventHandler(this.MasterSelectCloseButton_Click);
            // 
            // MasterSelectShowButton
            // 
            this.MasterSelectShowButton.Location = new System.Drawing.Point(10, 10);
            this.MasterSelectShowButton.Name = "MasterSelectShowButton";
            this.MasterSelectShowButton.Size = new System.Drawing.Size(80, 30);
            this.MasterSelectShowButton.TabIndex = 2;
            this.MasterSelectShowButton.Text = "種別選択";
            this.MasterSelectShowButton.UseVisualStyleBackColor = true;
            this.MasterSelectShowButton.Click += new System.EventHandler(this.MasterSelectShowButton_Click);
            // 
            // ListView
            // 
            this.ListView.AllowUserToAddRows = false;
            this.ListView.AllowUserToDeleteRows = false;
            this.ListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ListView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListView.DefaultCellStyle = dataGridViewCellStyle2;
            this.ListView.Location = new System.Drawing.Point(5, 45);
            this.ListView.Name = "ListView";
            this.ListView.ReadOnly = true;
            this.ListView.RowHeadersVisible = false;
            this.ListView.RowTemplate.Height = 21;
            this.ListView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ListView.Size = new System.Drawing.Size(1175, 515);
            this.ListView.TabIndex = 3;
            // 
            // DatePicker1
            // 
            this.DatePicker1.CalendarFont = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DatePicker1.CustomFormat = "yyyy年MM月dd日 (ddd)";
            this.DatePicker1.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DatePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatePicker1.Location = new System.Drawing.Point(100, 15);
            this.DatePicker1.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.DatePicker1.Name = "DatePicker1";
            this.DatePicker1.Size = new System.Drawing.Size(160, 21);
            this.DatePicker1.TabIndex = 0;
            this.DatePicker1.ValueChanged += new System.EventHandler(this.DatePicker1_ValueChanged);
            // 
            // ExcelButton
            // 
            this.ExcelButton.Location = new System.Drawing.Point(600, 10);
            this.ExcelButton.Name = "ExcelButton";
            this.ExcelButton.Size = new System.Drawing.Size(80, 30);
            this.ExcelButton.TabIndex = 4;
            this.ExcelButton.Text = "Excel出力";
            this.ExcelButton.UseVisualStyleBackColor = true;
            this.ExcelButton.Click += new System.EventHandler(this.ExcelButton_Click);
            // 
            // FormRsvPatList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 562);
            this.Controls.Add(this.ExcelButton);
            this.Controls.Add(this.DatePicker1);
            this.Controls.Add(this.MasterSelectShowButton);
            this.Controls.Add(this.MasterSelectPanel);
            this.Controls.Add(this.ListView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormRsvPatList";
            this.Text = "予約一覧";
            this.Load += new System.EventHandler(this.FormRsvPatList_Load);
            this.MasterSelectPanel.ResumeLayout(false);
            this.MasterSelectPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel MasterListPanel;
        private System.Windows.Forms.Panel MasterSelectPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button MasterSelectCloseButton;
        private System.Windows.Forms.Button MasterSelectShowButton;
        private System.Windows.Forms.DataGridView ListView;
        private System.Windows.Forms.DateTimePicker DatePicker1;
        private System.Windows.Forms.CheckBox OtherBox;
        private System.Windows.Forms.CheckBox LastPBox7;
        private System.Windows.Forms.Button ExcelButton;
    }
}