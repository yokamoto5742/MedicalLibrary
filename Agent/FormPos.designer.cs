namespace MedicalLibrary.Agent
{
    partial class FormPos
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPos));
			this.ListView1 = new System.Windows.Forms.DataGridView();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.ListView2 = new System.Windows.Forms.DataGridView();
			this.stdControlPat11 = new MedicalLibrary.Boundary.StdControlPat1();
			this.BillIdBox = new System.Windows.Forms.TextBox();
			this.PayYetBox = new System.Windows.Forms.CheckBox();
			this.BillIdBoxClearButton = new System.Windows.Forms.Button();
			this.BillPdfBrowser = new System.Windows.Forms.WebBrowser();
			this.BillPdfLabel = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.PayYetLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.ListView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ListView2)).BeginInit();
			this.SuspendLayout();
			// 
			// ListView1
			// 
			this.ListView1.AllowUserToAddRows = false;
			this.ListView1.AllowUserToDeleteRows = false;
			this.ListView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.ListView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.ListView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.ListView1.Location = new System.Drawing.Point(5, 55);
			this.ListView1.MultiSelect = false;
			this.ListView1.Name = "ListView1";
			this.ListView1.ReadOnly = true;
			this.ListView1.RowHeadersVisible = false;
			this.ListView1.RowTemplate.Height = 21;
			this.ListView1.Size = new System.Drawing.Size(955, 200);
			this.ListView1.TabIndex = 10;
			this.ListView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListView1_CellClick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 35);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(67, 15);
			this.label1.TabIndex = 11;
			this.label1.Text = "請求履歴";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(10, 260);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(67, 15);
			this.label2.TabIndex = 13;
			this.label2.Text = "入金履歴";
			// 
			// ListView2
			// 
			this.ListView2.AllowUserToAddRows = false;
			this.ListView2.AllowUserToDeleteRows = false;
			this.ListView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.ListView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.ListView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.ListView2.Location = new System.Drawing.Point(5, 280);
			this.ListView2.MultiSelect = false;
			this.ListView2.Name = "ListView2";
			this.ListView2.ReadOnly = true;
			this.ListView2.RowHeadersVisible = false;
			this.ListView2.RowTemplate.Height = 21;
			this.ListView2.Size = new System.Drawing.Size(430, 330);
			this.ListView2.TabIndex = 12;
			// 
			// stdControlPat11
			// 
			this.stdControlPat11.Location = new System.Drawing.Point(10, 0);
			this.stdControlPat11.Margin = new System.Windows.Forms.Padding(4);
			this.stdControlPat11.Mode1 = MedicalLibrary.Boundary.StdControlPat1.Mode.Normal;
			this.stdControlPat11.Name = "stdControlPat11";
			this.stdControlPat11.ReadOnly = false;
			this.stdControlPat11.Size = new System.Drawing.Size(600, 30);
			this.stdControlPat11.TabIndex = 14;
			// 
			// BillIdBox
			// 
			this.BillIdBox.BackColor = System.Drawing.Color.White;
			this.BillIdBox.Location = new System.Drawing.Point(220, 256);
			this.BillIdBox.Name = "BillIdBox";
			this.BillIdBox.ReadOnly = true;
			this.BillIdBox.Size = new System.Drawing.Size(140, 22);
			this.BillIdBox.TabIndex = 15;
			// 
			// PayYetBox
			// 
			this.PayYetBox.AutoSize = true;
			this.PayYetBox.Location = new System.Drawing.Point(150, 35);
			this.PayYetBox.Name = "PayYetBox";
			this.PayYetBox.Size = new System.Drawing.Size(81, 19);
			this.PayYetBox.TabIndex = 16;
			this.PayYetBox.Text = "未収のみ";
			this.PayYetBox.UseVisualStyleBackColor = true;
			this.PayYetBox.CheckedChanged += new System.EventHandler(this.PayYetBox_CheckedChanged);
			// 
			// BillIdBoxClearButton
			// 
			this.BillIdBoxClearButton.Location = new System.Drawing.Point(365, 256);
			this.BillIdBoxClearButton.Name = "BillIdBoxClearButton";
			this.BillIdBoxClearButton.Size = new System.Drawing.Size(60, 24);
			this.BillIdBoxClearButton.TabIndex = 17;
			this.BillIdBoxClearButton.Text = "クリア";
			this.BillIdBoxClearButton.UseVisualStyleBackColor = true;
			this.BillIdBoxClearButton.Click += new System.EventHandler(this.BillIdBoxClearButton_Click);
			// 
			// BillPdfBrowser
			// 
			this.BillPdfBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.BillPdfBrowser.Location = new System.Drawing.Point(440, 280);
			this.BillPdfBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.BillPdfBrowser.Name = "BillPdfBrowser";
			this.BillPdfBrowser.Size = new System.Drawing.Size(520, 330);
			this.BillPdfBrowser.TabIndex = 18;
			// 
			// BillPdfLabel
			// 
			this.BillPdfLabel.Location = new System.Drawing.Point(440, 259);
			this.BillPdfLabel.Name = "BillPdfLabel";
			this.BillPdfLabel.Size = new System.Drawing.Size(480, 16);
			this.BillPdfLabel.TabIndex = 19;
			this.BillPdfLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(150, 260);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(66, 15);
			this.label3.TabIndex = 20;
			this.label3.Text = "請求書ID";
			// 
			// PayYetLabel
			// 
			this.PayYetLabel.Location = new System.Drawing.Point(300, 35);
			this.PayYetLabel.Name = "PayYetLabel";
			this.PayYetLabel.Size = new System.Drawing.Size(180, 16);
			this.PayYetLabel.TabIndex = 21;
			this.PayYetLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FormPos
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(964, 612);
			this.Controls.Add(this.PayYetLabel);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.BillPdfLabel);
			this.Controls.Add(this.BillPdfBrowser);
			this.Controls.Add(this.BillIdBoxClearButton);
			this.Controls.Add(this.PayYetBox);
			this.Controls.Add(this.BillIdBox);
			this.Controls.Add(this.stdControlPat11);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.ListView2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ListView1);
			this.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "FormPos";
			this.Text = "POS";
			this.Load += new System.EventHandler(this.PosForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.ListView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ListView2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ListView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView ListView2;
        private MedicalLibrary.Boundary.StdControlPat1 stdControlPat11;
        private System.Windows.Forms.TextBox BillIdBox;
        private System.Windows.Forms.CheckBox PayYetBox;
        private System.Windows.Forms.Button BillIdBoxClearButton;
        private System.Windows.Forms.WebBrowser BillPdfBrowser;
        private System.Windows.Forms.Label BillPdfLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label PayYetLabel;
    }
}