namespace MedicalLibrary.Agent
{
    partial class FormInspectData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInspectData));
            this.stdControlPat11 = new MedicalLibrary.Boundary.StdControlPat1();
            this.ListView = new System.Windows.Forms.DataGridView();
            this.InspectPanel = new System.Windows.Forms.Panel();
            this.DatePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.RegButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.HelpPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.HelpCloseLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ListView)).BeginInit();
            this.HelpPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // stdControlPat11
            // 
            this.stdControlPat11.Location = new System.Drawing.Point(5, 0);
            this.stdControlPat11.Mode1 = MedicalLibrary.Boundary.StdControlPat1.Mode.Normal;
            this.stdControlPat11.Name = "stdControlPat11";
            this.stdControlPat11.ReadOnly = true;
            this.stdControlPat11.Size = new System.Drawing.Size(450, 30);
            this.stdControlPat11.TabIndex = 0;
            // 
            // ListView
            // 
            this.ListView.AllowUserToAddRows = false;
            this.ListView.AllowUserToDeleteRows = false;
            this.ListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListView.DefaultCellStyle = dataGridViewCellStyle1;
            this.ListView.Location = new System.Drawing.Point(5, 35);
            this.ListView.MultiSelect = false;
            this.ListView.Name = "ListView";
            this.ListView.ReadOnly = true;
            this.ListView.RowHeadersVisible = false;
            this.ListView.RowTemplate.Height = 21;
            this.ListView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ListView.Size = new System.Drawing.Size(475, 200);
            this.ListView.TabIndex = 1;
            this.ListView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListView_CellClick);
            // 
            // InspectPanel
            // 
            this.InspectPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InspectPanel.AutoScroll = true;
            this.InspectPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.InspectPanel.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.InspectPanel.Location = new System.Drawing.Point(5, 265);
            this.InspectPanel.Name = "InspectPanel";
            this.InspectPanel.Size = new System.Drawing.Size(475, 295);
            this.InspectPanel.TabIndex = 2;
            // 
            // DatePicker
            // 
            this.DatePicker.CalendarFont = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DatePicker.CustomFormat = "yyyy年M月d日 (ddd)";
            this.DatePicker.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatePicker.Location = new System.Drawing.Point(60, 242);
            this.DatePicker.MaxDate = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
            this.DatePicker.MinDate = new System.DateTime(1980, 1, 1, 0, 0, 0, 0);
            this.DatePicker.Name = "DatePicker";
            this.DatePicker.Size = new System.Drawing.Size(180, 20);
            this.DatePicker.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(10, 245);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "検査日";
            // 
            // RegButton
            // 
            this.RegButton.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 10F);
            this.RegButton.Location = new System.Drawing.Point(405, 238);
            this.RegButton.Name = "RegButton";
            this.RegButton.Size = new System.Drawing.Size(75, 25);
            this.RegButton.TabIndex = 5;
            this.RegButton.Text = "登録";
            this.RegButton.UseVisualStyleBackColor = true;
            this.RegButton.Click += new System.EventHandler(this.RegButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 10F);
            this.ClearButton.Location = new System.Drawing.Point(340, 238);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(60, 25);
            this.ClearButton.TabIndex = 6;
            this.ClearButton.Text = "クリア";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // HelpPanel
            // 
            this.HelpPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(224)))));
            this.HelpPanel.Controls.Add(this.HelpCloseLabel);
            this.HelpPanel.Controls.Add(this.label2);
            this.HelpPanel.Location = new System.Drawing.Point(315, 195);
            this.HelpPanel.Name = "HelpPanel";
            this.HelpPanel.Size = new System.Drawing.Size(160, 36);
            this.HelpPanel.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "削除するには、右クリックして\r\n「削除」を選んでください";
            // 
            // HelpCloseLabel
            // 
            this.HelpCloseLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HelpCloseLabel.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HelpCloseLabel.Location = new System.Drawing.Point(138, 18);
            this.HelpCloseLabel.Name = "HelpCloseLabel";
            this.HelpCloseLabel.Size = new System.Drawing.Size(18, 14);
            this.HelpCloseLabel.TabIndex = 0;
            this.HelpCloseLabel.Text = "×";
            this.HelpCloseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.HelpCloseLabel.Click += new System.EventHandler(this.HelpCloseLabel_Click);
            // 
            // FormInspectData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 562);
            this.Controls.Add(this.HelpPanel);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.RegButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DatePicker);
            this.Controls.Add(this.InspectPanel);
            this.Controls.Add(this.ListView);
            this.Controls.Add(this.stdControlPat11);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormInspectData";
            this.Text = "院外検査";
            this.Load += new System.EventHandler(this.FormInspectData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListView)).EndInit();
            this.HelpPanel.ResumeLayout(false);
            this.HelpPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Boundary.StdControlPat1 stdControlPat11;
        private System.Windows.Forms.DataGridView ListView;
        private System.Windows.Forms.Panel InspectPanel;
        private System.Windows.Forms.DateTimePicker DatePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RegButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Panel HelpPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label HelpCloseLabel;
    }
}