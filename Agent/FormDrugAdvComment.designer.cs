namespace MedicalLibrary.Agent
{
    partial class FormDrugAdvComment
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDrugAdvComment));
            this.label4 = new System.Windows.Forms.Label();
            this.CommentListView = new System.Windows.Forms.DataGridView();
            this.ContBox = new System.Windows.Forms.TextBox();
            this.PtInfoBox = new System.Windows.Forms.TextBox();
            this.PtIdBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TitleBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.DateTimeLabel = new System.Windows.Forms.Label();
            this.ContextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.CommentListView)).BeginInit();
            this.ContextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "履歴";
            // 
            // CommentListView
            // 
            this.CommentListView.AllowUserToAddRows = false;
            this.CommentListView.AllowUserToDeleteRows = false;
            this.CommentListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CommentListView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.CommentListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CommentListView.ContextMenuStrip = this.ContextMenuStrip1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CommentListView.DefaultCellStyle = dataGridViewCellStyle2;
            this.CommentListView.Location = new System.Drawing.Point(45, 35);
            this.CommentListView.MultiSelect = false;
            this.CommentListView.Name = "CommentListView";
            this.CommentListView.RowHeadersVisible = false;
            this.CommentListView.RowTemplate.Height = 21;
            this.CommentListView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CommentListView.Size = new System.Drawing.Size(335, 160);
            this.CommentListView.TabIndex = 18;
            this.CommentListView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CommentListView_CellClick);
            // 
            // ContBox
            // 
            this.ContBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContBox.Location = new System.Drawing.Point(45, 225);
            this.ContBox.MaxLength = 1000;
            this.ContBox.Multiline = true;
            this.ContBox.Name = "ContBox";
            this.ContBox.Size = new System.Drawing.Size(335, 100);
            this.ContBox.TabIndex = 17;
            // 
            // PtInfoBox
            // 
            this.PtInfoBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PtInfoBox.BackColor = System.Drawing.Color.LightYellow;
            this.PtInfoBox.Location = new System.Drawing.Point(85, 8);
            this.PtInfoBox.MaxLength = 9;
            this.PtInfoBox.Name = "PtInfoBox";
            this.PtInfoBox.ReadOnly = true;
            this.PtInfoBox.Size = new System.Drawing.Size(295, 19);
            this.PtInfoBox.TabIndex = 14;
            // 
            // PtIdBox
            // 
            this.PtIdBox.Location = new System.Drawing.Point(8, 8);
            this.PtIdBox.MaxLength = 9;
            this.PtIdBox.Name = "PtIdBox";
            this.PtIdBox.Size = new System.Drawing.Size(70, 19);
            this.PtIdBox.TabIndex = 13;
            this.PtIdBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PtIdBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PtIdBox_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 203);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "件名";
            // 
            // TitleBox
            // 
            this.TitleBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TitleBox.Location = new System.Drawing.Point(45, 200);
            this.TitleBox.MaxLength = 50;
            this.TitleBox.Name = "TitleBox";
            this.TitleBox.Size = new System.Drawing.Size(335, 19);
            this.TitleBox.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 233);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "内容";
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SaveButton.Location = new System.Drawing.Point(305, 330);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 25);
            this.SaveButton.TabIndex = 21;
            this.SaveButton.Text = "登録";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ClearButton.Location = new System.Drawing.Point(250, 330);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(50, 25);
            this.ClearButton.TabIndex = 23;
            this.ClearButton.Text = "クリア";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // DateTimeLabel
            // 
            this.DateTimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DateTimeLabel.BackColor = System.Drawing.Color.LightYellow;
            this.DateTimeLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DateTimeLabel.Location = new System.Drawing.Point(45, 330);
            this.DateTimeLabel.Name = "DateTimeLabel";
            this.DateTimeLabel.Size = new System.Drawing.Size(200, 22);
            this.DateTimeLabel.TabIndex = 24;
            this.DateTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ContextMenuStrip1
            // 
            this.ContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewMenuItem,
            this.DeleteMenuItem});
            this.ContextMenuStrip1.Name = "ContextMenuStrip1";
            this.ContextMenuStrip1.Size = new System.Drawing.Size(125, 48);
            // 
            // NewMenuItem
            // 
            this.NewMenuItem.Name = "NewMenuItem";
            this.NewMenuItem.Size = new System.Drawing.Size(124, 22);
            this.NewMenuItem.Text = "新規作成";
            this.NewMenuItem.Click += new System.EventHandler(this.NewMenuItem_Click);
            // 
            // DeleteMenuItem
            // 
            this.DeleteMenuItem.Name = "DeleteMenuItem";
            this.DeleteMenuItem.Size = new System.Drawing.Size(124, 22);
            this.DeleteMenuItem.Text = "削除";
            this.DeleteMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
            // 
            // FormComment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 362);
            this.Controls.Add(this.DateTimeLabel);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CommentListView);
            this.Controls.Add(this.ContBox);
            this.Controls.Add(this.PtInfoBox);
            this.Controls.Add(this.PtIdBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TitleBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormComment";
            this.Text = "コメント";
            this.Load += new System.EventHandler(this.FormDrugAdvComment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CommentListView)).EndInit();
            this.ContextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView CommentListView;
        private System.Windows.Forms.TextBox ContBox;
        private System.Windows.Forms.TextBox PtInfoBox;
        private System.Windows.Forms.TextBox PtIdBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TitleBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Label DateTimeLabel;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem NewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteMenuItem;
    }
}