namespace MedicalLibrary.Agent
{
    partial class MWMForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MWMForm));
            this.HandlePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.HandleButton1 = new System.Windows.Forms.RadioButton();
            this.HandleButton2 = new System.Windows.Forms.RadioButton();
            this.PastBox = new System.Windows.Forms.CheckBox();
            this.NextBox = new System.Windows.Forms.CheckBox();
            this.PtOrderView = new System.Windows.Forms.DataGridView();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.ModeBox0 = new System.Windows.Forms.CheckBox();
            this.OrderDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.PtInfoBox = new System.Windows.Forms.TextBox();
            this.PtIdBox = new System.Windows.Forms.TextBox();
            this.MakeCSVButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CommentPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SentListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SentDelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PtListView = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.PtSeqBox = new System.Windows.Forms.TextBox();
            this.ModeBox1 = new System.Windows.Forms.CheckBox();
            this.CommentLabel = new System.Windows.Forms.Label();
            this.AutoBox = new System.Windows.Forms.CheckBox();
            this.HandlePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PtOrderView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SentListMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PtListView)).BeginInit();
            this.SuspendLayout();
            // 
            // HandlePanel
            // 
            this.HandlePanel.Controls.Add(this.HandleButton1);
            this.HandlePanel.Controls.Add(this.HandleButton2);
            this.HandlePanel.Location = new System.Drawing.Point(480, 30);
            this.HandlePanel.Name = "HandlePanel";
            this.HandlePanel.Size = new System.Drawing.Size(130, 20);
            this.HandlePanel.TabIndex = 30;
            this.HandlePanel.Visible = false;
            // 
            // HandleButton1
            // 
            this.HandleButton1.AutoSize = true;
            this.HandleButton1.Location = new System.Drawing.Point(3, 3);
            this.HandleButton1.Name = "HandleButton1";
            this.HandleButton1.Size = new System.Drawing.Size(48, 16);
            this.HandleButton1.TabIndex = 0;
            this.HandleButton1.TabStop = true;
            this.HandleButton1.Text = "Start";
            this.HandleButton1.UseVisualStyleBackColor = true;
            // 
            // HandleButton2
            // 
            this.HandleButton2.AutoSize = true;
            this.HandleButton2.Location = new System.Drawing.Point(57, 3);
            this.HandleButton2.Name = "HandleButton2";
            this.HandleButton2.Size = new System.Drawing.Size(65, 16);
            this.HandleButton2.TabIndex = 15;
            this.HandleButton2.TabStop = true;
            this.HandleButton2.Text = "Reserve";
            this.HandleButton2.UseVisualStyleBackColor = true;
            // 
            // PastBox
            // 
            this.PastBox.AutoSize = true;
            this.PastBox.Location = new System.Drawing.Point(408, 33);
            this.PastBox.Name = "PastBox";
            this.PastBox.Size = new System.Drawing.Size(69, 16);
            this.PastBox.TabIndex = 29;
            this.PastBox.Text = "全て表示";
            this.PastBox.UseVisualStyleBackColor = true;
            this.PastBox.CheckedChanged += new System.EventHandler(this.PastBox_CheckedChanged);
            // 
            // NextBox
            // 
            this.NextBox.AutoSize = true;
            this.NextBox.Location = new System.Drawing.Point(300, 274);
            this.NextBox.Name = "NextBox";
            this.NextBox.Size = new System.Drawing.Size(72, 16);
            this.NextBox.TabIndex = 28;
            this.NextBox.Text = "日付未定";
            this.NextBox.UseVisualStyleBackColor = true;
            this.NextBox.CheckedChanged += new System.EventHandler(this.NextBox_CheckedChanged);
            // 
            // PtOrderView
            // 
            this.PtOrderView.AllowUserToAddRows = false;
            this.PtOrderView.AllowUserToDeleteRows = false;
            this.PtOrderView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PtOrderView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PtOrderView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.PtOrderView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PtOrderView.DefaultCellStyle = dataGridViewCellStyle2;
            this.PtOrderView.Location = new System.Drawing.Point(7, 56);
            this.PtOrderView.MultiSelect = false;
            this.PtOrderView.Name = "PtOrderView";
            this.PtOrderView.RowHeadersVisible = false;
            this.PtOrderView.RowTemplate.Height = 21;
            this.PtOrderView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PtOrderView.Size = new System.Drawing.Size(1000, 209);
            this.PtOrderView.TabIndex = 27;
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(210, 271);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(80, 22);
            this.UpdateButton.TabIndex = 26;
            this.UpdateButton.Text = "更新 (F5)";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // ModeBox0
            // 
            this.ModeBox0.AutoSize = true;
            this.ModeBox0.Location = new System.Drawing.Point(430, 274);
            this.ModeBox0.Name = "ModeBox0";
            this.ModeBox0.Size = new System.Drawing.Size(112, 16);
            this.ModeBox0.TabIndex = 25;
            this.ModeBox0.Text = "未施行を表示する";
            this.ModeBox0.UseVisualStyleBackColor = true;
            this.ModeBox0.CheckedChanged += new System.EventHandler(this.ModeBox0_CheckedChanged);
            // 
            // OrderDateTimePicker
            // 
            this.OrderDateTimePicker.CustomFormat = "yyyy年MM月dd日 (ddd)";
            this.OrderDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.OrderDateTimePicker.Location = new System.Drawing.Point(60, 272);
            this.OrderDateTimePicker.MinDate = new System.DateTime(2007, 1, 1, 0, 0, 0, 0);
            this.OrderDateTimePicker.Name = "OrderDateTimePicker";
            this.OrderDateTimePicker.Size = new System.Drawing.Size(145, 19);
            this.OrderDateTimePicker.TabIndex = 23;
            // 
            // PtInfoBox
            // 
            this.PtInfoBox.BackColor = System.Drawing.Color.LightYellow;
            this.PtInfoBox.Location = new System.Drawing.Point(65, 31);
            this.PtInfoBox.Name = "PtInfoBox";
            this.PtInfoBox.Size = new System.Drawing.Size(290, 19);
            this.PtInfoBox.TabIndex = 22;
            // 
            // PtIdBox
            // 
            this.PtIdBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.PtIdBox.Location = new System.Drawing.Point(5, 31);
            this.PtIdBox.MaxLength = 9;
            this.PtIdBox.Name = "PtIdBox";
            this.PtIdBox.Size = new System.Drawing.Size(55, 19);
            this.PtIdBox.TabIndex = 20;
            this.PtIdBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PtIdBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PtIdBox_KeyDown);
            // 
            // MakeCSVButton
            // 
            this.MakeCSVButton.Location = new System.Drawing.Point(945, 30);
            this.MakeCSVButton.Name = "MakeCSVButton";
            this.MakeCSVButton.Size = new System.Drawing.Size(62, 23);
            this.MakeCSVButton.TabIndex = 18;
            this.MakeCSVButton.Text = "送信";
            this.MakeCSVButton.UseVisualStyleBackColor = true;
            this.MakeCSVButton.Click += new System.EventHandler(this.MakeCSVButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1016, 24);
            this.menuStrip1.TabIndex = 32;
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
            // CommentPanel
            // 
            this.CommentPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CommentPanel.Location = new System.Drawing.Point(670, 30);
            this.CommentPanel.Name = "CommentPanel";
            this.CommentPanel.Size = new System.Drawing.Size(220, 20);
            this.CommentPanel.TabIndex = 33;
            // 
            // SentListMenu
            // 
            this.SentListMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SentDelMenuItem});
            this.SentListMenu.Name = "SentListMenu";
            this.SentListMenu.Size = new System.Drawing.Size(99, 26);
            this.SentListMenu.Opening += new System.ComponentModel.CancelEventHandler(this.SentListMenu_Opening);
            // 
            // SentDelMenuItem
            // 
            this.SentDelMenuItem.Name = "SentDelMenuItem";
            this.SentDelMenuItem.Size = new System.Drawing.Size(98, 22);
            this.SentDelMenuItem.Text = "取消";
            this.SentDelMenuItem.Click += new System.EventHandler(this.SentDelMenuItem_Click);
            // 
            // PtListView
            // 
            this.PtListView.AllowUserToAddRows = false;
            this.PtListView.AllowUserToDeleteRows = false;
            this.PtListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.PtListView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.PtListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PtListView.ContextMenuStrip = this.SentListMenu;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.PtListView.DefaultCellStyle = dataGridViewCellStyle4;
            this.PtListView.Location = new System.Drawing.Point(7, 297);
            this.PtListView.MultiSelect = false;
            this.PtListView.Name = "PtListView";
            this.PtListView.ReadOnly = true;
            this.PtListView.RowHeadersVisible = false;
            this.PtListView.RowTemplate.Height = 21;
            this.PtListView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.PtListView.Size = new System.Drawing.Size(1000, 470);
            this.PtListView.TabIndex = 19;
            this.PtListView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PtListView_CellClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 275);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 12);
            this.label2.TabIndex = 35;
            this.label2.Text = "オーダー";
            // 
            // PtSeqBox
            // 
            this.PtSeqBox.BackColor = System.Drawing.Color.LightYellow;
            this.PtSeqBox.Location = new System.Drawing.Point(360, 31);
            this.PtSeqBox.Name = "PtSeqBox";
            this.PtSeqBox.Size = new System.Drawing.Size(45, 19);
            this.PtSeqBox.TabIndex = 36;
            this.PtSeqBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ModeBox1
            // 
            this.ModeBox1.AutoSize = true;
            this.ModeBox1.Location = new System.Drawing.Point(550, 274);
            this.ModeBox1.Name = "ModeBox1";
            this.ModeBox1.Size = new System.Drawing.Size(112, 16);
            this.ModeBox1.TabIndex = 37;
            this.ModeBox1.Text = "施行済を表示する";
            this.ModeBox1.UseVisualStyleBackColor = true;
            this.ModeBox1.CheckedChanged += new System.EventHandler(this.ModeBox1_CheckedChanged);
            // 
            // CommentLabel
            // 
            this.CommentLabel.Location = new System.Drawing.Point(615, 32);
            this.CommentLabel.Name = "CommentLabel";
            this.CommentLabel.Size = new System.Drawing.Size(50, 16);
            this.CommentLabel.TabIndex = 34;
            this.CommentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AutoBox
            // 
            this.AutoBox.AutoSize = true;
            this.AutoBox.Location = new System.Drawing.Point(895, 33);
            this.AutoBox.Name = "AutoBox";
            this.AutoBox.Size = new System.Drawing.Size(48, 16);
            this.AutoBox.TabIndex = 38;
            this.AutoBox.Text = "自動";
            this.AutoBox.UseVisualStyleBackColor = true;
            this.AutoBox.CheckedChanged += new System.EventHandler(this.AutoBox_CheckedChanged);
            // 
            // MWMForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 773);
            this.Controls.Add(this.AutoBox);
            this.Controls.Add(this.ModeBox1);
            this.Controls.Add(this.PtSeqBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PtListView);
            this.Controls.Add(this.CommentLabel);
            this.Controls.Add(this.CommentPanel);
            this.Controls.Add(this.HandlePanel);
            this.Controls.Add(this.PastBox);
            this.Controls.Add(this.NextBox);
            this.Controls.Add(this.PtOrderView);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.OrderDateTimePicker);
            this.Controls.Add(this.ModeBox0);
            this.Controls.Add(this.PtInfoBox);
            this.Controls.Add(this.PtIdBox);
            this.Controls.Add(this.MakeCSVButton);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MWMForm";
            this.Text = "MWM";
            this.Load += new System.EventHandler(this.MWMForm_Load);
            this.Shown += new System.EventHandler(this.MWMForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MWMForm_KeyDown);
            this.HandlePanel.ResumeLayout(false);
            this.HandlePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PtOrderView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.SentListMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PtListView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel HandlePanel;
        private System.Windows.Forms.RadioButton HandleButton1;
        private System.Windows.Forms.RadioButton HandleButton2;
        private System.Windows.Forms.CheckBox PastBox;
        private System.Windows.Forms.CheckBox NextBox;
        private System.Windows.Forms.DataGridView PtOrderView;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.CheckBox ModeBox0;
        private System.Windows.Forms.DateTimePicker OrderDateTimePicker;
        private System.Windows.Forms.TextBox PtInfoBox;
        private System.Windows.Forms.TextBox PtIdBox;
        private System.Windows.Forms.Button MakeCSVButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileExitMenuItem;
        private System.Windows.Forms.FlowLayoutPanel CommentPanel;
        private System.Windows.Forms.ContextMenuStrip SentListMenu;
        private System.Windows.Forms.ToolStripMenuItem SentDelMenuItem;
        private System.Windows.Forms.DataGridView PtListView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PtSeqBox;
        private System.Windows.Forms.CheckBox ModeBox1;
        private System.Windows.Forms.Label CommentLabel;
        private System.Windows.Forms.CheckBox AutoBox;
    }
}

