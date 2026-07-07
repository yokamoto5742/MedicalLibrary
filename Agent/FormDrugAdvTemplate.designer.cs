namespace MedicalLibrary.Agent
{
    partial class FormDrugAdvTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDrugAdvTemplate));
            this.advTreeView = new System.Windows.Forms.TreeView();
            this.treeContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nodeContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tempContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.delTempToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nodeBox = new System.Windows.Forms.ComboBox();
            this.advBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.regButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.modeBox = new System.Windows.Forms.TextBox();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tempIdBox = new System.Windows.Forms.TextBox();
            this.applyButton = new System.Windows.Forms.Button();
            this.makeTreeButton = new System.Windows.Forms.Button();
            this.applyBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.treeContextMenuStrip.SuspendLayout();
            this.nodeContextMenuStrip.SuspendLayout();
            this.tempContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // advTreeView
            // 
            this.advTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.advTreeView.ContextMenuStrip = this.treeContextMenuStrip;
            this.advTreeView.Location = new System.Drawing.Point(5, 30);
            this.advTreeView.Name = "advTreeView";
            this.advTreeView.Size = new System.Drawing.Size(240, 350);
            this.advTreeView.TabIndex = 0;
            this.advTreeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.advTreeView_AfterLabelEdit);
            this.advTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.advTreeView_NodeMouseClick);
            // 
            // treeContextMenuStrip
            // 
            this.treeContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTreeToolStripMenuItem});
            this.treeContextMenuStrip.Name = "treeContextMenuStrip";
            this.treeContextMenuStrip.Size = new System.Drawing.Size(125, 26);
            // 
            // newTreeToolStripMenuItem
            // 
            this.newTreeToolStripMenuItem.Name = "newTreeToolStripMenuItem";
            this.newTreeToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.newTreeToolStripMenuItem.Text = "新規分類";
            this.newTreeToolStripMenuItem.Click += new System.EventHandler(this.newTreeToolStripMenuItem_Click);
            // 
            // nodeContextMenuStrip
            // 
            this.nodeContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newNodeToolStripMenuItem,
            this.renameNodeToolStripMenuItem,
            this.delNodeToolStripMenuItem});
            this.nodeContextMenuStrip.Name = "nodeContextMenuStrip";
            this.nodeContextMenuStrip.Size = new System.Drawing.Size(173, 70);
            // 
            // newNodeToolStripMenuItem
            // 
            this.newNodeToolStripMenuItem.Name = "newNodeToolStripMenuItem";
            this.newNodeToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.newNodeToolStripMenuItem.Text = "新規テンプレート";
            this.newNodeToolStripMenuItem.Click += new System.EventHandler(this.newNodeToolStripMenuItem_Click);
            // 
            // renameNodeToolStripMenuItem
            // 
            this.renameNodeToolStripMenuItem.Name = "renameNodeToolStripMenuItem";
            this.renameNodeToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.renameNodeToolStripMenuItem.Text = "分類の名称変更";
            this.renameNodeToolStripMenuItem.Click += new System.EventHandler(this.renameNodeToolStripMenuItem_Click);
            // 
            // delNodeToolStripMenuItem
            // 
            this.delNodeToolStripMenuItem.Name = "delNodeToolStripMenuItem";
            this.delNodeToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.delNodeToolStripMenuItem.Text = "分類の削除";
            this.delNodeToolStripMenuItem.Click += new System.EventHandler(this.delNodeToolStripMenuItem_Click);
            // 
            // tempContextMenuStrip
            // 
            this.tempContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.delTempToolStripMenuItem});
            this.tempContextMenuStrip.Name = "tempContextMenuStrip";
            this.tempContextMenuStrip.Size = new System.Drawing.Size(153, 48);
            // 
            // delTempToolStripMenuItem
            // 
            this.delTempToolStripMenuItem.Name = "delTempToolStripMenuItem";
            this.delTempToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.delTempToolStripMenuItem.Text = "削除";
            this.delTempToolStripMenuItem.Click += new System.EventHandler(this.delTempToolStripMenuItem_Click);
            // 
            // nodeBox
            // 
            this.nodeBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nodeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nodeBox.FormattingEnabled = true;
            this.nodeBox.Location = new System.Drawing.Point(285, 54);
            this.nodeBox.Name = "nodeBox";
            this.nodeBox.Size = new System.Drawing.Size(295, 20);
            this.nodeBox.TabIndex = 3;
            // 
            // advBox
            // 
            this.advBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.advBox.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.advBox.Location = new System.Drawing.Point(285, 80);
            this.advBox.MaxLength = 1000;
            this.advBox.Multiline = true;
            this.advBox.Name = "advBox";
            this.advBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.advBox.Size = new System.Drawing.Size(295, 245);
            this.advBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(250, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "分類";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "一覧";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(250, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "内容";
            // 
            // regButton
            // 
            this.regButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.regButton.Location = new System.Drawing.Point(395, 356);
            this.regButton.Name = "regButton";
            this.regButton.Size = new System.Drawing.Size(90, 23);
            this.regButton.TabIndex = 8;
            this.regButton.Text = "登録";
            this.regButton.UseVisualStyleBackColor = true;
            this.regButton.Click += new System.EventHandler(this.regButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearButton.Location = new System.Drawing.Point(490, 356);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(70, 23);
            this.clearButton.TabIndex = 9;
            this.clearButton.Text = "クリア";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // modeBox
            // 
            this.modeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.modeBox.Location = new System.Drawing.Point(520, 5);
            this.modeBox.Name = "modeBox";
            this.modeBox.ReadOnly = true;
            this.modeBox.Size = new System.Drawing.Size(59, 19);
            this.modeBox.TabIndex = 11;
            this.modeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nameBox
            // 
            this.nameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameBox.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.nameBox.Location = new System.Drawing.Point(285, 30);
            this.nameBox.MaxLength = 40;
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(295, 19);
            this.nameBox.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(250, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "名称";
            // 
            // tempIdBox
            // 
            this.tempIdBox.Location = new System.Drawing.Point(285, 5);
            this.tempIdBox.Name = "tempIdBox";
            this.tempIdBox.ReadOnly = true;
            this.tempIdBox.Size = new System.Drawing.Size(94, 19);
            this.tempIdBox.TabIndex = 14;
            this.tempIdBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tempIdBox.Visible = false;
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.Location = new System.Drawing.Point(300, 356);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(90, 23);
            this.applyButton.TabIndex = 15;
            this.applyButton.Text = "適用";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // makeTreeButton
            // 
            this.makeTreeButton.Location = new System.Drawing.Point(130, 5);
            this.makeTreeButton.Name = "makeTreeButton";
            this.makeTreeButton.Size = new System.Drawing.Size(75, 21);
            this.makeTreeButton.TabIndex = 16;
            this.makeTreeButton.Text = "更新";
            this.makeTreeButton.UseVisualStyleBackColor = true;
            this.makeTreeButton.Click += new System.EventHandler(this.makeTreeButton_Click);
            // 
            // applyBox
            // 
            this.applyBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.applyBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.applyBox.FormattingEnabled = true;
            this.applyBox.Location = new System.Drawing.Point(295, 330);
            this.applyBox.Name = "applyBox";
            this.applyBox.Size = new System.Drawing.Size(285, 20);
            this.applyBox.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(250, 333);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "適用欄";
            // 
            // FormAdvTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 382);
            this.Controls.Add(this.applyBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.makeTreeButton);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.tempIdBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.modeBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.advBox);
            this.Controls.Add(this.nodeBox);
            this.Controls.Add(this.advTreeView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.regButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAdvTemplate";
            this.Text = "指導相談内容テンプレート";
            this.Load += new System.EventHandler(this.FormDrugAdvTemplate_Load);
            this.treeContextMenuStrip.ResumeLayout(false);
            this.nodeContextMenuStrip.ResumeLayout(false);
            this.tempContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView advTreeView;
        private System.Windows.Forms.ContextMenuStrip tempContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem delTempToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip nodeContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem delNodeToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip treeContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem newTreeToolStripMenuItem;
        private System.Windows.Forms.ComboBox nodeBox;
        private System.Windows.Forms.TextBox advBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button regButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.TextBox modeBox;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem renameNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newNodeToolStripMenuItem;
        private System.Windows.Forms.TextBox tempIdBox;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button makeTreeButton;
        private System.Windows.Forms.ComboBox applyBox;
        private System.Windows.Forms.Label label5;
    }
}