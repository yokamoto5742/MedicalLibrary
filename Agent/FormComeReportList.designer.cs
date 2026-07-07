namespace MedicalLibrary.Agent
{
    partial class FormComeReportList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormComeReportList));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.kensaDate2 = new System.Windows.Forms.DateTimePicker();
            this.showButton1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.gridView1 = new System.Windows.Forms.DataGridView();
            this.GridMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenReportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kensaDate1 = new System.Windows.Forms.DateTimePicker();
            this.inBox = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.FileReportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolOutSideMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolAchieveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpManualMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deptBox = new System.Windows.Forms.ComboBox();
            this.doctorBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.kensaBox = new System.Windows.Forms.CheckedListBox();
            this.partBox = new System.Windows.Forms.CheckedListBox();
            this.keywordBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.unBox = new System.Windows.Forms.CheckBox();
            this.outBox = new System.Windows.Forms.CheckBox();
            this.LoginUsrLabel = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.GridMenuStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 115);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(992, 622);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.kensaDate2);
            this.tabPage1.Controls.Add(this.showButton1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.gridView1);
            this.tabPage1.Controls.Add(this.kensaDate1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(984, 596);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "オーダー";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(164, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 15;
            this.label10.Text = "～";
            // 
            // kensaDate2
            // 
            this.kensaDate2.Location = new System.Drawing.Point(185, 12);
            this.kensaDate2.Name = "kensaDate2";
            this.kensaDate2.Size = new System.Drawing.Size(109, 19);
            this.kensaDate2.TabIndex = 14;
            // 
            // showButton1
            // 
            this.showButton1.Location = new System.Drawing.Point(319, 10);
            this.showButton1.Name = "showButton1";
            this.showButton1.Size = new System.Drawing.Size(75, 23);
            this.showButton1.TabIndex = 12;
            this.showButton1.Text = "表示";
            this.showButton1.UseVisualStyleBackColor = true;
            this.showButton1.Click += new System.EventHandler(this.showButton1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "期間";
            // 
            // gridView1
            // 
            this.gridView1.AllowUserToAddRows = false;
            this.gridView1.AllowUserToDeleteRows = false;
            this.gridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridView1.ContextMenuStrip = this.GridMenuStrip;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridView1.Location = new System.Drawing.Point(6, 40);
            this.gridView1.Name = "gridView1";
            this.gridView1.ReadOnly = true;
            this.gridView1.RowHeadersVisible = false;
            this.gridView1.RowTemplate.Height = 21;
            this.gridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridView1.Size = new System.Drawing.Size(972, 551);
            this.gridView1.TabIndex = 1;
            this.gridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridView1_CellDoubleClick);
            // 
            // GridMenuStrip
            // 
            this.GridMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenReportMenuItem});
            this.GridMenuStrip.Name = "GridMenuStrip";
            this.GridMenuStrip.Size = new System.Drawing.Size(99, 26);
            // 
            // OpenReportMenuItem
            // 
            this.OpenReportMenuItem.Name = "OpenReportMenuItem";
            this.OpenReportMenuItem.Size = new System.Drawing.Size(98, 22);
            this.OpenReportMenuItem.Text = "所見";
            this.OpenReportMenuItem.Click += new System.EventHandler(this.OpenReportMenuItem_Click);
            // 
            // kensaDate1
            // 
            this.kensaDate1.Location = new System.Drawing.Point(50, 12);
            this.kensaDate1.Name = "kensaDate1";
            this.kensaDate1.Size = new System.Drawing.Size(109, 19);
            this.kensaDate1.TabIndex = 0;
            // 
            // inBox
            // 
            this.inBox.AutoSize = true;
            this.inBox.Location = new System.Drawing.Point(901, 37);
            this.inBox.Name = "inBox";
            this.inBox.Size = new System.Drawing.Size(72, 16);
            this.inBox.TabIndex = 13;
            this.inBox.Text = "院内読影";
            this.inBox.UseVisualStyleBackColor = true;
            this.inBox.CheckedChanged += new System.EventHandler(this.inBox_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.ToolMenu,
            this.HelpMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1016, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileMenu
            // 
            this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileReportMenuItem,
            this.FileExitMenuItem});
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new System.Drawing.Size(53, 20);
            this.FileMenu.Text = "ファイル";
            // 
            // FileReportMenuItem
            // 
            this.FileReportMenuItem.Name = "FileReportMenuItem";
            this.FileReportMenuItem.Size = new System.Drawing.Size(98, 22);
            this.FileReportMenuItem.Text = "所見";
            this.FileReportMenuItem.Click += new System.EventHandler(this.FileReportMenuItem_Click);
            // 
            // FileExitMenuItem
            // 
            this.FileExitMenuItem.Name = "FileExitMenuItem";
            this.FileExitMenuItem.Size = new System.Drawing.Size(98, 22);
            this.FileExitMenuItem.Text = "終了";
            this.FileExitMenuItem.Click += new System.EventHandler(this.FileExitMenuItem_Click);
            // 
            // ToolMenu
            // 
            this.ToolMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolOutSideMenuItem,
            this.ToolAchieveMenuItem});
            this.ToolMenu.Name = "ToolMenu";
            this.ToolMenu.Size = new System.Drawing.Size(46, 20);
            this.ToolMenu.Text = "ツール";
            this.ToolMenu.Click += new System.EventHandler(this.ToolMenu_Click);
            // 
            // ToolOutSideMenuItem
            // 
            this.ToolOutSideMenuItem.Name = "ToolOutSideMenuItem";
            this.ToolOutSideMenuItem.Size = new System.Drawing.Size(146, 22);
            this.ToolOutSideMenuItem.Text = "読影依頼一覧";
            this.ToolOutSideMenuItem.Click += new System.EventHandler(this.ToolOutSideMenuItem_Click);
            // 
            // ToolAchieveMenuItem
            // 
            this.ToolAchieveMenuItem.Name = "ToolAchieveMenuItem";
            this.ToolAchieveMenuItem.Size = new System.Drawing.Size(146, 22);
            this.ToolAchieveMenuItem.Text = "読影実績";
            this.ToolAchieveMenuItem.Click += new System.EventHandler(this.ToolAchieveMenuItem_Click);
            // 
            // HelpMenu
            // 
            this.HelpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpManualMenuItem});
            this.HelpMenu.Name = "HelpMenu";
            this.HelpMenu.Size = new System.Drawing.Size(48, 20);
            this.HelpMenu.Text = "ヘルプ";
            // 
            // HelpManualMenuItem
            // 
            this.HelpManualMenuItem.Name = "HelpManualMenuItem";
            this.HelpManualMenuItem.Size = new System.Drawing.Size(119, 22);
            this.HelpManualMenuItem.Text = "マニュアル";
            this.HelpManualMenuItem.Click += new System.EventHandler(this.HelpManualMenuItem_Click);
            // 
            // deptBox
            // 
            this.deptBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.deptBox.FormattingEnabled = true;
            this.deptBox.Location = new System.Drawing.Point(64, 56);
            this.deptBox.Name = "deptBox";
            this.deptBox.Size = new System.Drawing.Size(111, 20);
            this.deptBox.TabIndex = 4;
            this.deptBox.SelectedIndexChanged += new System.EventHandler(this.deptBox_SelectedIndexChanged);
            // 
            // doctorBox
            // 
            this.doctorBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.doctorBox.FormattingEnabled = true;
            this.doctorBox.Location = new System.Drawing.Point(64, 82);
            this.doctorBox.Name = "doctorBox";
            this.doctorBox.Size = new System.Drawing.Size(111, 20);
            this.doctorBox.TabIndex = 5;
            this.doctorBox.SelectedIndexChanged += new System.EventHandler(this.doctorBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "診療科";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "医師";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "種別";
            // 
            // groupBox
            // 
            this.groupBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.groupBox.FormattingEnabled = true;
            this.groupBox.Location = new System.Drawing.Point(64, 30);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(111, 20);
            this.groupBox.TabIndex = 8;
            this.groupBox.SelectedIndexChanged += new System.EventHandler(this.groupBox_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(190, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "検査";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(510, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 17;
            this.label8.Text = "部位";
            // 
            // kensaBox
            // 
            this.kensaBox.CheckOnClick = true;
            this.kensaBox.FormattingEnabled = true;
            this.kensaBox.Location = new System.Drawing.Point(230, 30);
            this.kensaBox.MultiColumn = true;
            this.kensaBox.Name = "kensaBox";
            this.kensaBox.Size = new System.Drawing.Size(265, 60);
            this.kensaBox.TabIndex = 18;
            this.kensaBox.SelectedIndexChanged += new System.EventHandler(this.kensaBox_SelectedIndexChanged);
            // 
            // partBox
            // 
            this.partBox.CheckOnClick = true;
            this.partBox.FormattingEnabled = true;
            this.partBox.Location = new System.Drawing.Point(551, 30);
            this.partBox.MultiColumn = true;
            this.partBox.Name = "partBox";
            this.partBox.Size = new System.Drawing.Size(297, 88);
            this.partBox.TabIndex = 19;
            this.partBox.SelectedIndexChanged += new System.EventHandler(this.partBox_SelectedIndexChanged);
            // 
            // keywordBox
            // 
            this.keywordBox.Location = new System.Drawing.Point(249, 95);
            this.keywordBox.Name = "keywordBox";
            this.keywordBox.Size = new System.Drawing.Size(247, 19);
            this.keywordBox.TabIndex = 20;
            this.keywordBox.TextChanged += new System.EventHandler(this.keywordBox_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(190, 99);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 21;
            this.label9.Text = "内容検索";
            // 
            // unBox
            // 
            this.unBox.AutoSize = true;
            this.unBox.Location = new System.Drawing.Point(901, 97);
            this.unBox.Name = "unBox";
            this.unBox.Size = new System.Drawing.Size(84, 16);
            this.unBox.TabIndex = 16;
            this.unBox.Text = "未完成のみ";
            this.unBox.UseVisualStyleBackColor = true;
            this.unBox.CheckedChanged += new System.EventHandler(this.unBox_CheckedChanged);
            // 
            // outBox
            // 
            this.outBox.AutoSize = true;
            this.outBox.Location = new System.Drawing.Point(901, 58);
            this.outBox.Name = "outBox";
            this.outBox.Size = new System.Drawing.Size(72, 16);
            this.outBox.TabIndex = 22;
            this.outBox.Text = "院外読影";
            this.outBox.UseVisualStyleBackColor = true;
            this.outBox.CheckedChanged += new System.EventHandler(this.outBox_CheckedChanged);
            // 
            // LoginUsrLabel
            // 
            this.LoginUsrLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LoginUsrLabel.BackColor = System.Drawing.Color.LightYellow;
            this.LoginUsrLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LoginUsrLabel.Location = new System.Drawing.Point(900, 3);
            this.LoginUsrLabel.Name = "LoginUsrLabel";
            this.LoginUsrLabel.Size = new System.Drawing.Size(100, 20);
            this.LoginUsrLabel.TabIndex = 23;
            this.LoginUsrLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LoginUsrLabel.DoubleClick += new System.EventHandler(this.LoginUsrLabel_DoubleClick);
            // 
            // FormComeReportList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.LoginUsrLabel);
            this.Controls.Add(this.outBox);
            this.Controls.Add(this.unBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.keywordBox);
            this.Controls.Add(this.inBox);
            this.Controls.Add(this.partBox);
            this.Controls.Add(this.kensaBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.doctorBox);
            this.Controls.Add(this.deptBox);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormComeReportList";
            this.Text = "検査所見システム";
            this.Load += new System.EventHandler(this.FormComeReportList_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.GridMenuStrip.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DateTimePicker kensaDate1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileMenu;
        private System.Windows.Forms.ToolStripMenuItem FileReportMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileExitMenuItem;
        private System.Windows.Forms.ComboBox deptBox;
        private System.Windows.Forms.ComboBox doctorBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button showButton1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox groupBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckedListBox kensaBox;
        private System.Windows.Forms.CheckedListBox partBox;
        private System.Windows.Forms.TextBox keywordBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox inBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker kensaDate2;
        private System.Windows.Forms.CheckBox unBox;
        private System.Windows.Forms.CheckBox outBox;
        private System.Windows.Forms.ContextMenuStrip GridMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem OpenReportMenuItem;
        protected internal System.Windows.Forms.DataGridView gridView1;
        protected internal System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStripMenuItem ToolMenu;
        private System.Windows.Forms.ToolStripMenuItem ToolOutSideMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolAchieveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpMenu;
        private System.Windows.Forms.ToolStripMenuItem HelpManualMenuItem;
        private System.Windows.Forms.Label LoginUsrLabel;
    }
}

