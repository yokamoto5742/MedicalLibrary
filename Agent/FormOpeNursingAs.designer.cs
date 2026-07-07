namespace MedicalLibrary.Agent
{
    partial class FormOpeNursingAs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOpeNursingAs));
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.asTitleBox = new System.Windows.Forms.ComboBox();
            this.makeAsButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recPanel = new System.Windows.Forms.Panel();
            this.makeRecButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.recTitleBox = new System.Windows.Forms.ComboBox();
            this.makeEvalButton = new System.Windows.Forms.Button();
            this.readRecFileButton = new System.Windows.Forms.Button();
            this.recKindLabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 24;
            this.label4.Text = "種別";
            // 
            // tabControl1
            // 
            this.tabControl1.Location = new System.Drawing.Point(12, 62);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(425, 650);
            this.tabControl1.TabIndex = 23;
            // 
            // asTitleBox
            // 
            this.asTitleBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.asTitleBox.FormattingEnabled = true;
            this.asTitleBox.Location = new System.Drawing.Point(58, 38);
            this.asTitleBox.Name = "asTitleBox";
            this.asTitleBox.Size = new System.Drawing.Size(124, 20);
            this.asTitleBox.TabIndex = 22;
            this.asTitleBox.SelectedIndexChanged += new System.EventHandler(this.asTitleBox_SelectedIndexChanged);
            // 
            // makeAsButton
            // 
            this.makeAsButton.Location = new System.Drawing.Point(222, 38);
            this.makeAsButton.Name = "makeAsButton";
            this.makeAsButton.Size = new System.Drawing.Size(74, 22);
            this.makeAsButton.TabIndex = 25;
            this.makeAsButton.Text = "診断作成";
            this.makeAsButton.UseVisualStyleBackColor = true;
            this.makeAsButton.Click += new System.EventHandler(this.makeAsButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1016, 26);
            this.menuStrip1.TabIndex = 26;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(68, 22);
            this.fileToolStripMenuItem.Text = "ファイル";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.exitToolStripMenuItem.Text = "終了";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // recPanel
            // 
            this.recPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.recPanel.AutoScroll = true;
            this.recPanel.BackColor = System.Drawing.Color.Silver;
            this.recPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.recPanel.Location = new System.Drawing.Point(454, 67);
            this.recPanel.Name = "recPanel";
            this.recPanel.Size = new System.Drawing.Size(550, 645);
            this.recPanel.TabIndex = 35;
            // 
            // makeRecButton
            // 
            this.makeRecButton.Location = new System.Drawing.Point(720, 39);
            this.makeRecButton.Name = "makeRecButton";
            this.makeRecButton.Size = new System.Drawing.Size(74, 22);
            this.makeRecButton.TabIndex = 34;
            this.makeRecButton.Text = "記録作成";
            this.makeRecButton.UseVisualStyleBackColor = true;
            this.makeRecButton.Click += new System.EventHandler(this.makeRecButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(460, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 33;
            this.label5.Text = "種別";
            // 
            // recTitleBox
            // 
            this.recTitleBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.recTitleBox.FormattingEnabled = true;
            this.recTitleBox.Location = new System.Drawing.Point(500, 39);
            this.recTitleBox.Name = "recTitleBox";
            this.recTitleBox.Size = new System.Drawing.Size(124, 20);
            this.recTitleBox.TabIndex = 32;
            this.recTitleBox.SelectedIndexChanged += new System.EventHandler(this.recTitleBox_SelectedIndexChanged);
            // 
            // makeEvalButton
            // 
            this.makeEvalButton.Location = new System.Drawing.Point(318, 38);
            this.makeEvalButton.Name = "makeEvalButton";
            this.makeEvalButton.Size = new System.Drawing.Size(74, 22);
            this.makeEvalButton.TabIndex = 37;
            this.makeEvalButton.Text = "評価作成";
            this.makeEvalButton.UseVisualStyleBackColor = true;
            this.makeEvalButton.Click += new System.EventHandler(this.makeEvalButton_Click);
            // 
            // readRecFileButton
            // 
            this.readRecFileButton.Location = new System.Drawing.Point(940, 39);
            this.readRecFileButton.Name = "readRecFileButton";
            this.readRecFileButton.Size = new System.Drawing.Size(65, 22);
            this.readRecFileButton.TabIndex = 38;
            this.readRecFileButton.Text = "再読込";
            this.readRecFileButton.UseVisualStyleBackColor = true;
            this.readRecFileButton.Click += new System.EventHandler(this.readRecFileButton_Click);
            // 
            // recKindLabel
            // 
            this.recKindLabel.BackColor = System.Drawing.Color.White;
            this.recKindLabel.Location = new System.Drawing.Point(640, 40);
            this.recKindLabel.Name = "recKindLabel";
            this.recKindLabel.Size = new System.Drawing.Size(50, 18);
            this.recKindLabel.TabIndex = 39;
            this.recKindLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormAs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.recKindLabel);
            this.Controls.Add(this.readRecFileButton);
            this.Controls.Add(this.makeEvalButton);
            this.Controls.Add(this.recPanel);
            this.Controls.Add(this.makeRecButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.recTitleBox);
            this.Controls.Add(this.makeAsButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.asTitleBox);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormAs";
            this.Text = "看護診断・記録作成";
            this.Load += new System.EventHandler(this.FormOpeNursingAs_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ComboBox asTitleBox;
        private System.Windows.Forms.Button makeAsButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Panel recPanel;
        private System.Windows.Forms.Button makeRecButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox recTitleBox;
        private System.Windows.Forms.Button makeEvalButton;
        private System.Windows.Forms.Button readRecFileButton;
        private System.Windows.Forms.Label recKindLabel;
    }
}