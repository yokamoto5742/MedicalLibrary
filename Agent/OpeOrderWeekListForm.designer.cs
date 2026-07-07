namespace MedicalLibrary.Agent
{
    partial class OpeOrderWeekListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpeOrderWeekListForm));
            this.OpeListView = new System.Windows.Forms.DataGridView();
            this.CritDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.ShowButton = new System.Windows.Forms.Button();
            this.ExcelPlanButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.PlaceFilterBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.OpeListView)).BeginInit();
            this.SuspendLayout();
            // 
            // OpeListView
            // 
            this.OpeListView.AllowUserToAddRows = false;
            this.OpeListView.AllowUserToDeleteRows = false;
            this.OpeListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OpeListView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.OpeListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.OpeListView.DefaultCellStyle = dataGridViewCellStyle1;
            this.OpeListView.Location = new System.Drawing.Point(3, 33);
            this.OpeListView.Name = "OpeListView";
            this.OpeListView.ReadOnly = true;
            this.OpeListView.RowHeadersVisible = false;
            this.OpeListView.RowTemplate.Height = 21;
            this.OpeListView.Size = new System.Drawing.Size(1000, 690);
            this.OpeListView.TabIndex = 0;
            // 
            // CritDate
            // 
            this.CritDate.CustomFormat = "yyyy年MM月dd日(ddd)";
            this.CritDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.CritDate.Location = new System.Drawing.Point(59, 7);
            this.CritDate.Name = "CritDate";
            this.CritDate.Size = new System.Drawing.Size(145, 19);
            this.CritDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "基準日";
            // 
            // ShowButton
            // 
            this.ShowButton.Location = new System.Drawing.Point(210, 5);
            this.ShowButton.Name = "ShowButton";
            this.ShowButton.Size = new System.Drawing.Size(60, 23);
            this.ShowButton.TabIndex = 3;
            this.ShowButton.Text = "表示";
            this.ShowButton.UseVisualStyleBackColor = true;
            this.ShowButton.Click += new System.EventHandler(this.ShowButton_Click);
            // 
            // ExcelPlanButton
            // 
            this.ExcelPlanButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExcelPlanButton.Location = new System.Drawing.Point(670, 5);
            this.ExcelPlanButton.Name = "ExcelPlanButton";
            this.ExcelPlanButton.Size = new System.Drawing.Size(70, 23);
            this.ExcelPlanButton.TabIndex = 46;
            this.ExcelPlanButton.Text = "Excel出力";
            this.ExcelPlanButton.UseVisualStyleBackColor = true;
            this.ExcelPlanButton.Click += new System.EventHandler(this.ExcelPlanButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(308, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 49;
            this.label2.Text = "場所";
            // 
            // PlaceFilterBox
            // 
            this.PlaceFilterBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PlaceFilterBox.FormattingEnabled = true;
            this.PlaceFilterBox.Location = new System.Drawing.Point(341, 6);
            this.PlaceFilterBox.Name = "PlaceFilterBox";
            this.PlaceFilterBox.Size = new System.Drawing.Size(70, 20);
            this.PlaceFilterBox.TabIndex = 48;
            this.PlaceFilterBox.SelectedIndexChanged += new System.EventHandler(this.PlaceFilterBox_SelectedIndexChanged);
            // 
            // OpeOrderListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PlaceFilterBox);
            this.Controls.Add(this.ExcelPlanButton);
            this.Controls.Add(this.ShowButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CritDate);
            this.Controls.Add(this.OpeListView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OpeOrderListForm";
            this.Text = "週間手術一覧";
            this.Load += new System.EventHandler(this.OpeOrderWeekListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.OpeListView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView OpeListView;
        private System.Windows.Forms.DateTimePicker CritDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ShowButton;
        private System.Windows.Forms.Button ExcelPlanButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox PlaceFilterBox;
    }
}

