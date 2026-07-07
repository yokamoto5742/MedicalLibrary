namespace MedicalLibrary.Agent
{
    partial class PatLabelForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatLabelForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.PrintButton = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.PtIdBox = new System.Windows.Forms.TextBox();
            this.PtInfoLabel = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SekouDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.MiteiBox = new System.Windows.Forms.CheckBox();
            this.InOutBox1 = new System.Windows.Forms.CheckBox();
            this.InOutBox2 = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DeptPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ShinkuPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.OrderView = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            ((System.ComponentModel.ISupportInitialize)(this.OrderView)).BeginInit();
            this.SuspendLayout();
            // 
            // PrintButton
            // 
            this.PrintButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PrintButton.Location = new System.Drawing.Point(155, 535);
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.Size = new System.Drawing.Size(75, 23);
            this.PrintButton.TabIndex = 0;
            this.PrintButton.Text = "印刷 (F8)";
            this.PrintButton.UseVisualStyleBackColor = true;
            this.PrintButton.Click += new System.EventHandler(this.PrintButton_Click);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "患者ID";
            // 
            // PtIdBox
            // 
            this.PtIdBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.PtIdBox.Location = new System.Drawing.Point(50, 28);
            this.PtIdBox.MaxLength = 9;
            this.PtIdBox.Name = "PtIdBox";
            this.PtIdBox.Size = new System.Drawing.Size(72, 19);
            this.PtIdBox.TabIndex = 2;
            this.PtIdBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PtIdBox.Click += new System.EventHandler(this.PtIdBox_Click);
            this.PtIdBox.TextChanged += new System.EventHandler(this.PtIdBox_TextChanged);
            this.PtIdBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PtIdBox_KeyDown);
            // 
            // PtInfoLabel
            // 
            this.PtInfoLabel.BackColor = System.Drawing.Color.LightYellow;
            this.PtInfoLabel.Location = new System.Drawing.Point(125, 29);
            this.PtInfoLabel.Name = "PtInfoLabel";
            this.PtInfoLabel.Size = new System.Drawing.Size(394, 18);
            this.PtInfoLabel.TabIndex = 3;
            this.PtInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CloseButton.Location = new System.Drawing.Point(240, 535);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 5;
            this.CloseButton.Text = "閉じる (F9)";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // SekouDate
            // 
            this.SekouDate.Location = new System.Drawing.Point(50, 51);
            this.SekouDate.Name = "SekouDate";
            this.SekouDate.Size = new System.Drawing.Size(109, 19);
            this.SekouDate.TabIndex = 6;
            this.SekouDate.ValueChanged += new System.EventHandler(this.SekouDate_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "施行日";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "診療科";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 232);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "オーダー内容";
            // 
            // MiteiBox
            // 
            this.MiteiBox.AutoSize = true;
            this.MiteiBox.Location = new System.Drawing.Point(174, 54);
            this.MiteiBox.Name = "MiteiBox";
            this.MiteiBox.Size = new System.Drawing.Size(103, 16);
            this.MiteiBox.TabIndex = 12;
            this.MiteiBox.Text = "日付未定を含む";
            this.MiteiBox.UseVisualStyleBackColor = true;
            this.MiteiBox.CheckedChanged += new System.EventHandler(this.MiteiBox_CheckedChanged);
            // 
            // InOutBox1
            // 
            this.InOutBox1.AutoSize = true;
            this.InOutBox1.Checked = true;
            this.InOutBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.InOutBox1.Location = new System.Drawing.Point(363, 54);
            this.InOutBox1.Name = "InOutBox1";
            this.InOutBox1.Size = new System.Drawing.Size(48, 16);
            this.InOutBox1.TabIndex = 13;
            this.InOutBox1.Text = "外来";
            this.InOutBox1.UseVisualStyleBackColor = true;
            this.InOutBox1.CheckedChanged += new System.EventHandler(this.InOutBox1_CheckedChanged);
            // 
            // InOutBox2
            // 
            this.InOutBox2.AutoSize = true;
            this.InOutBox2.Location = new System.Drawing.Point(417, 54);
            this.InOutBox2.Name = "InOutBox2";
            this.InOutBox2.Size = new System.Drawing.Size(48, 16);
            this.InOutBox2.TabIndex = 14;
            this.InOutBox2.Text = "入院";
            this.InOutBox2.UseVisualStyleBackColor = true;
            this.InOutBox2.CheckedChanged += new System.EventHandler(this.InOutBox2_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 185);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "診区";
            // 
            // DeptPanel
            // 
            this.DeptPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DeptPanel.AutoScroll = true;
            this.DeptPanel.BackColor = System.Drawing.Color.LightYellow;
            this.DeptPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DeptPanel.Location = new System.Drawing.Point(50, 74);
            this.DeptPanel.Name = "DeptPanel";
            this.DeptPanel.Size = new System.Drawing.Size(470, 105);
            this.DeptPanel.TabIndex = 18;
            // 
            // ShinkuPanel
            // 
            this.ShinkuPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ShinkuPanel.AutoScroll = true;
            this.ShinkuPanel.BackColor = System.Drawing.Color.LightYellow;
            this.ShinkuPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ShinkuPanel.Location = new System.Drawing.Point(50, 182);
            this.ShinkuPanel.Name = "ShinkuPanel";
            this.ShinkuPanel.Size = new System.Drawing.Size(470, 44);
            this.ShinkuPanel.TabIndex = 19;
            // 
            // OrderView
            // 
            this.OrderView.AllowUserToAddRows = false;
            this.OrderView.AllowUserToDeleteRows = false;
            this.OrderView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OrderView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.OrderView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.OrderView.DefaultCellStyle = dataGridViewCellStyle1;
            this.OrderView.Location = new System.Drawing.Point(7, 247);
            this.OrderView.MultiSelect = false;
            this.OrderView.Name = "OrderView";
            this.OrderView.RowHeadersVisible = false;
            this.OrderView.RowTemplate.Height = 21;
            this.OrderView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.OrderView.Size = new System.Drawing.Size(513, 285);
            this.OrderView.TabIndex = 20;
            this.OrderView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OrderView_CellContentClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label6.Location = new System.Drawing.Point(49, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(259, 12);
            this.label6.TabIndex = 21;
            this.label6.Text = "患者IDをクリアするには F5 キーを押してください。";
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 562);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.OrderView);
            this.Controls.Add(this.ShinkuPanel);
            this.Controls.Add(this.DeptPanel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.InOutBox2);
            this.Controls.Add(this.InOutBox1);
            this.Controls.Add(this.MiteiBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SekouDate);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.PtInfoLabel);
            this.Controls.Add(this.PtIdBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PrintButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormMain";
            this.Text = "患者ラベル印刷";
            this.Load += new System.EventHandler(this.PatLabelForm_Load);
            this.Shown += new System.EventHandler(this.PatLabelForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PatLabelForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.OrderView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button PrintButton;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PtIdBox;
        private System.Windows.Forms.Label PtInfoLabel;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.DateTimePicker SekouDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox MiteiBox;
        private System.Windows.Forms.CheckBox InOutBox1;
        private System.Windows.Forms.CheckBox InOutBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.FlowLayoutPanel DeptPanel;
        private System.Windows.Forms.FlowLayoutPanel ShinkuPanel;
        private System.Windows.Forms.DataGridView OrderView;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PrintDialog printDialog1;
    }
}