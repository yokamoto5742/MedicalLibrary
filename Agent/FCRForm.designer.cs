namespace MedicalLibrary.Agent
{
    partial class FCRForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FCRForm));
            this.MakeXMLButton = new System.Windows.Forms.Button();
            this.orderView = new System.Windows.Forms.DataGridView();
            this.ptIdBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ptInfoBox = new System.Windows.Forms.TextBox();
            this.orderDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.ModeBox = new System.Windows.Forms.CheckBox();
            this.ListButton = new System.Windows.Forms.Button();
            this.ptOrderView = new System.Windows.Forms.DataGridView();
            this.NextBox = new System.Windows.Forms.CheckBox();
            this.PastBox = new System.Windows.Forms.CheckBox();
            this.HandlePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.HandleButton1 = new System.Windows.Forms.RadioButton();
            this.HandleButton2 = new System.Windows.Forms.RadioButton();
            this.SendToBoxPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.PtSeqBox = new System.Windows.Forms.TextBox();
            this.MakeXMLsButton = new System.Windows.Forms.Button();
            this.CheckAllButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.orderView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptOrderView)).BeginInit();
            this.HandlePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MakeXMLButton
            // 
            this.MakeXMLButton.Location = new System.Drawing.Point(796, 34);
            this.MakeXMLButton.Name = "MakeXMLButton";
            this.MakeXMLButton.Size = new System.Drawing.Size(62, 23);
            this.MakeXMLButton.TabIndex = 0;
            this.MakeXMLButton.Text = "送信";
            this.MakeXMLButton.UseVisualStyleBackColor = true;
            this.MakeXMLButton.Click += new System.EventHandler(this.MakeXMLButton_Click);
            // 
            // orderView
            // 
            this.orderView.AllowUserToAddRows = false;
            this.orderView.AllowUserToDeleteRows = false;
            this.orderView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.orderView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.orderView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.orderView.DefaultCellStyle = dataGridViewCellStyle2;
            this.orderView.Location = new System.Drawing.Point(7, 250);
            this.orderView.MultiSelect = false;
            this.orderView.Name = "orderView";
            this.orderView.RowHeadersVisible = false;
            this.orderView.RowTemplate.Height = 21;
            this.orderView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.orderView.Size = new System.Drawing.Size(850, 420);
            this.orderView.TabIndex = 1;
            this.orderView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.orderView_CellClick);
            // 
            // ptIdBox
            // 
            this.ptIdBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ptIdBox.Location = new System.Drawing.Point(36, 36);
            this.ptIdBox.MaxLength = 9;
            this.ptIdBox.Name = "ptIdBox";
            this.ptIdBox.Size = new System.Drawing.Size(60, 19);
            this.ptIdBox.TabIndex = 2;
            this.ptIdBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ptIdBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ptIdBox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "ID";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(864, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileExitMenuItem});
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(53, 20);
            this.FileMenuItem.Text = "ファイル";
            // 
            // FileExitMenuItem
            // 
            this.FileExitMenuItem.Name = "FileExitMenuItem";
            this.FileExitMenuItem.Size = new System.Drawing.Size(98, 22);
            this.FileExitMenuItem.Text = "終了";
            this.FileExitMenuItem.Click += new System.EventHandler(this.FileExitMenuItem_Click);
            // 
            // ptInfoBox
            // 
            this.ptInfoBox.BackColor = System.Drawing.Color.LightYellow;
            this.ptInfoBox.Location = new System.Drawing.Point(98, 36);
            this.ptInfoBox.Name = "ptInfoBox";
            this.ptInfoBox.Size = new System.Drawing.Size(260, 19);
            this.ptInfoBox.TabIndex = 5;
            // 
            // orderDate
            // 
            this.orderDate.CustomFormat = "yyyy年MM月dd日 (ddd)";
            this.orderDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.orderDate.Location = new System.Drawing.Point(60, 225);
            this.orderDate.MinDate = new System.DateTime(2007, 1, 1, 0, 0, 0, 0);
            this.orderDate.Name = "orderDate";
            this.orderDate.Size = new System.Drawing.Size(145, 19);
            this.orderDate.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 230);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "オーダー";
            // 
            // ModeBox
            // 
            this.ModeBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.ModeBox.Location = new System.Drawing.Point(420, 222);
            this.ModeBox.Name = "ModeBox";
            this.ModeBox.Size = new System.Drawing.Size(80, 25);
            this.ModeBox.TabIndex = 9;
            this.ModeBox.Text = "未施行";
            this.ModeBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ModeBox.UseVisualStyleBackColor = true;
            this.ModeBox.CheckedChanged += new System.EventHandler(this.ModeBox_CheckedChanged);
            // 
            // ListButton
            // 
            this.ListButton.Location = new System.Drawing.Point(210, 224);
            this.ListButton.Name = "ListButton";
            this.ListButton.Size = new System.Drawing.Size(80, 23);
            this.ListButton.TabIndex = 10;
            this.ListButton.Text = "更新 (F5)";
            this.ListButton.UseVisualStyleBackColor = true;
            this.ListButton.Click += new System.EventHandler(this.ListButton_Click);
            // 
            // ptOrderView
            // 
            this.ptOrderView.AllowUserToAddRows = false;
            this.ptOrderView.AllowUserToDeleteRows = false;
            this.ptOrderView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ptOrderView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.ptOrderView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ptOrderView.DefaultCellStyle = dataGridViewCellStyle4;
            this.ptOrderView.Location = new System.Drawing.Point(7, 60);
            this.ptOrderView.Name = "ptOrderView";
            this.ptOrderView.RowHeadersVisible = false;
            this.ptOrderView.RowTemplate.Height = 21;
            this.ptOrderView.Size = new System.Drawing.Size(850, 158);
            this.ptOrderView.TabIndex = 11;
            // 
            // NextBox
            // 
            this.NextBox.AutoSize = true;
            this.NextBox.Location = new System.Drawing.Point(300, 228);
            this.NextBox.Name = "NextBox";
            this.NextBox.Size = new System.Drawing.Size(72, 16);
            this.NextBox.TabIndex = 12;
            this.NextBox.Text = "日付未定";
            this.NextBox.UseVisualStyleBackColor = true;
            this.NextBox.CheckedChanged += new System.EventHandler(this.NextBox_CheckedChanged);
            // 
            // PastBox
            // 
            this.PastBox.AutoSize = true;
            this.PastBox.Location = new System.Drawing.Point(411, 38);
            this.PastBox.Name = "PastBox";
            this.PastBox.Size = new System.Drawing.Size(110, 16);
            this.PastBox.TabIndex = 13;
            this.PastBox.Text = "過去オーダー表示";
            this.PastBox.UseVisualStyleBackColor = true;
            this.PastBox.CheckedChanged += new System.EventHandler(this.PastBox_CheckedChanged);
            // 
            // HandlePanel
            // 
            this.HandlePanel.Controls.Add(this.HandleButton1);
            this.HandlePanel.Controls.Add(this.HandleButton2);
            this.HandlePanel.Location = new System.Drawing.Point(524, 35);
            this.HandlePanel.Name = "HandlePanel";
            this.HandlePanel.Size = new System.Drawing.Size(131, 20);
            this.HandlePanel.TabIndex = 14;
            // 
            // HandleButton1
            // 
            this.HandleButton1.AutoSize = true;
            this.HandleButton1.Location = new System.Drawing.Point(3, 3);
            this.HandleButton1.Name = "HandleButton1";
            this.HandleButton1.Size = new System.Drawing.Size(48, 16);
            this.HandleButton1.TabIndex = 0;
            this.HandleButton1.Text = "Start";
            this.HandleButton1.UseVisualStyleBackColor = true;
            // 
            // HandleButton2
            // 
            this.HandleButton2.AutoSize = true;
            this.HandleButton2.Checked = true;
            this.HandleButton2.Location = new System.Drawing.Point(57, 3);
            this.HandleButton2.Name = "HandleButton2";
            this.HandleButton2.Size = new System.Drawing.Size(65, 16);
            this.HandleButton2.TabIndex = 15;
            this.HandleButton2.TabStop = true;
            this.HandleButton2.Text = "Reserve";
            this.HandleButton2.UseVisualStyleBackColor = true;
            // 
            // SendToBoxPanel
            // 
            this.SendToBoxPanel.AutoScroll = true;
            this.SendToBoxPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SendToBoxPanel.Location = new System.Drawing.Point(661, 34);
            this.SendToBoxPanel.Name = "SendToBoxPanel";
            this.SendToBoxPanel.Size = new System.Drawing.Size(129, 24);
            this.SendToBoxPanel.TabIndex = 17;
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
            // PtSeqBox
            // 
            this.PtSeqBox.BackColor = System.Drawing.Color.LightYellow;
            this.PtSeqBox.Location = new System.Drawing.Point(360, 36);
            this.PtSeqBox.Name = "PtSeqBox";
            this.PtSeqBox.Size = new System.Drawing.Size(45, 19);
            this.PtSeqBox.TabIndex = 18;
            this.PtSeqBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MakeXMLsButton
            // 
            this.MakeXMLsButton.Location = new System.Drawing.Point(796, 222);
            this.MakeXMLsButton.Name = "MakeXMLsButton";
            this.MakeXMLsButton.Size = new System.Drawing.Size(62, 23);
            this.MakeXMLsButton.TabIndex = 19;
            this.MakeXMLsButton.Text = "送信";
            this.MakeXMLsButton.UseVisualStyleBackColor = true;
            this.MakeXMLsButton.Click += new System.EventHandler(this.MakeXMLsButton_Click);
            // 
            // CheckAllButton
            // 
            this.CheckAllButton.Location = new System.Drawing.Point(700, 222);
            this.CheckAllButton.Name = "CheckAllButton";
            this.CheckAllButton.Size = new System.Drawing.Size(80, 23);
            this.CheckAllButton.TabIndex = 20;
            this.CheckAllButton.Text = "すべて選択";
            this.CheckAllButton.UseVisualStyleBackColor = true;
            this.CheckAllButton.Click += new System.EventHandler(this.CheckAllButton_Click);
            // 
            // FCRForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 673);
            this.Controls.Add(this.CheckAllButton);
            this.Controls.Add(this.MakeXMLsButton);
            this.Controls.Add(this.PtSeqBox);
            this.Controls.Add(this.HandlePanel);
            this.Controls.Add(this.PastBox);
            this.Controls.Add(this.SendToBoxPanel);
            this.Controls.Add(this.NextBox);
            this.Controls.Add(this.ptOrderView);
            this.Controls.Add(this.ListButton);
            this.Controls.Add(this.ModeBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.orderDate);
            this.Controls.Add(this.ptInfoBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ptIdBox);
            this.Controls.Add(this.orderView);
            this.Controls.Add(this.MakeXMLButton);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FCRForm";
            this.Text = "FCR";
            this.Load += new System.EventHandler(this.FCRForm_Load);
            this.Shown += new System.EventHandler(this.FCRForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FCRForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.orderView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptOrderView)).EndInit();
            this.HandlePanel.ResumeLayout(false);
            this.HandlePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button MakeXMLButton;
        private System.Windows.Forms.DataGridView orderView;
        private System.Windows.Forms.TextBox ptIdBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileExitMenuItem;
        private System.Windows.Forms.TextBox ptInfoBox;
        private System.Windows.Forms.DateTimePicker orderDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ModeBox;
        private System.Windows.Forms.Button ListButton;
        private System.Windows.Forms.DataGridView ptOrderView;
        private System.Windows.Forms.CheckBox NextBox;
        private System.Windows.Forms.CheckBox PastBox;
        private System.Windows.Forms.FlowLayoutPanel HandlePanel;
        private System.Windows.Forms.RadioButton HandleButton1;
        private System.Windows.Forms.RadioButton HandleButton2;
        private System.Windows.Forms.FlowLayoutPanel SendToBoxPanel;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.TextBox PtSeqBox;
        private System.Windows.Forms.Button MakeXMLsButton;
        private System.Windows.Forms.Button CheckAllButton;
    }
}

