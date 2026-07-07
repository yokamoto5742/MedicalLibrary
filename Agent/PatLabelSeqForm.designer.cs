namespace MedicalLibrary.Agent
{
    partial class PatLabelSeqForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatLabelSeqForm));
            this.PtIdBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ListView = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.PtInfoLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ComeDate = new System.Windows.Forms.DateTimePicker();
            this.PrintButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.PtNameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ListView)).BeginInit();
            this.SuspendLayout();
            // 
            // PtIdBox
            // 
            this.PtIdBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.PtIdBox.Location = new System.Drawing.Point(48, 25);
            this.PtIdBox.MaxLength = 9;
            this.PtIdBox.Name = "PtIdBox";
            this.PtIdBox.Size = new System.Drawing.Size(65, 19);
            this.PtIdBox.TabIndex = 0;
            this.PtIdBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PtIdBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PtIdBox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "患者ID";
            // 
            // ListView
            // 
            this.ListView.AllowUserToAddRows = false;
            this.ListView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ListView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListView.Location = new System.Drawing.Point(7, 75);
            this.ListView.MultiSelect = false;
            this.ListView.Name = "ListView";
            this.ListView.RowHeadersVisible = false;
            this.ListView.RowTemplate.Height = 21;
            this.ListView.Size = new System.Drawing.Size(278, 122);
            this.ListView.TabIndex = 2;
            this.ListView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListView_CellContentClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "受付日";
            // 
            // PtInfoLabel
            // 
            this.PtInfoLabel.BackColor = System.Drawing.Color.LightYellow;
            this.PtInfoLabel.Location = new System.Drawing.Point(115, 26);
            this.PtInfoLabel.Name = "PtInfoLabel";
            this.PtInfoLabel.Size = new System.Drawing.Size(170, 18);
            this.PtInfoLabel.TabIndex = 4;
            this.PtInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(47, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(218, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "患者IDをクリアするにはF5を押してください";
            // 
            // ComeDate
            // 
            this.ComeDate.Location = new System.Drawing.Point(48, 49);
            this.ComeDate.Name = "ComeDate";
            this.ComeDate.Size = new System.Drawing.Size(108, 19);
            this.ComeDate.TabIndex = 6;
            // 
            // PrintButton
            // 
            this.PrintButton.Location = new System.Drawing.Point(64, 206);
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.Size = new System.Drawing.Size(75, 23);
            this.PrintButton.TabIndex = 7;
            this.PrintButton.Text = "印刷 (F8)";
            this.PrintButton.UseVisualStyleBackColor = true;
            this.PrintButton.Click += new System.EventHandler(this.PrintButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(145, 206);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 8;
            this.CloseButton.Text = "閉じる (F9)";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
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
            // PtNameLabel
            // 
            this.PtNameLabel.Location = new System.Drawing.Point(185, 53);
            this.PtNameLabel.Name = "PtNameLabel";
            this.PtNameLabel.Size = new System.Drawing.Size(100, 12);
            this.PtNameLabel.TabIndex = 9;
            this.PtNameLabel.Visible = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 238);
            this.Controls.Add(this.PtNameLabel);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.PrintButton);
            this.Controls.Add(this.ComeDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PtInfoLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ListView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PtIdBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormMain";
            this.Text = "受付番号発行";
            this.Load += new System.EventHandler(this.PatLabelSeqForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PatLabelSeqForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ListView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox PtIdBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView ListView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label PtInfoLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker ComeDate;
        private System.Windows.Forms.Button PrintButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Label PtNameLabel;
    }
}

