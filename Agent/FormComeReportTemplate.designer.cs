namespace MedicalLibrary.Agent
{
    partial class FormComeReportTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormComeReportTemplate));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.FileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReportPanel = new System.Windows.Forms.Panel();
            this.MakeReportButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.KensaBox = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(585, 26);
            this.menuStrip1.TabIndex = 26;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileMenu
            // 
            this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileExitMenuItem});
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new System.Drawing.Size(68, 22);
            this.FileMenu.Text = "ファイル";
            // 
            // FileExitMenuItem
            // 
            this.FileExitMenuItem.Name = "FileExitMenuItem";
            this.FileExitMenuItem.Size = new System.Drawing.Size(100, 22);
            this.FileExitMenuItem.Text = "終了";
            this.FileExitMenuItem.Click += new System.EventHandler(this.FileExitMenuItem_Click_1);
            // 
            // ReportPanel
            // 
            this.ReportPanel.AutoScroll = true;
            this.ReportPanel.BackColor = System.Drawing.Color.Silver;
            this.ReportPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ReportPanel.Location = new System.Drawing.Point(17, 60);
            this.ReportPanel.Name = "ReportPanel";
            this.ReportPanel.Size = new System.Drawing.Size(550, 660);
            this.ReportPanel.TabIndex = 35;
            // 
            // MakeReportButton
            // 
            this.MakeReportButton.Location = new System.Drawing.Point(235, 32);
            this.MakeReportButton.Name = "MakeReportButton";
            this.MakeReportButton.Size = new System.Drawing.Size(74, 22);
            this.MakeReportButton.TabIndex = 34;
            this.MakeReportButton.Text = "所見作成";
            this.MakeReportButton.UseVisualStyleBackColor = true;
            this.MakeReportButton.Click += new System.EventHandler(this.MakeReportButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 33;
            this.label5.Text = "種別";
            // 
            // KensaBox
            // 
            this.KensaBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.KensaBox.FormattingEnabled = true;
            this.KensaBox.Location = new System.Drawing.Point(71, 33);
            this.KensaBox.Name = "KensaBox";
            this.KensaBox.Size = new System.Drawing.Size(124, 20);
            this.KensaBox.TabIndex = 32;
            this.KensaBox.SelectedIndexChanged += new System.EventHandler(this.KensaBox_SelectedIndexChanged);
            // 
            // FormReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 741);
            this.Controls.Add(this.ReportPanel);
            this.Controls.Add(this.MakeReportButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.KensaBox);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormReport";
            this.Text = "所見作成";
            this.Load += new System.EventHandler(this.FormComeReportTemplate_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileMenu;
        private System.Windows.Forms.ToolStripMenuItem FileExitMenuItem;
        private System.Windows.Forms.Panel ReportPanel;
        private System.Windows.Forms.Button MakeReportButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox KensaBox;
    }
}