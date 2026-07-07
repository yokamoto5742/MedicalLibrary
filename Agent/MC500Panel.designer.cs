namespace MedicalLibrary.Agent
{
    partial class MC500Panel
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

        #region コンポーネント デザイナで生成されたコード

        /// <summary> 
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ListView = new System.Windows.Forms.DataGridView();
            this.ListMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ListMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.StaffBox = new System.Windows.Forms.TextBox();
            this.ContBox = new System.Windows.Forms.TextBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.EyeBox = new System.Windows.Forms.ComboBox();
            this.LaserDateBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ListView)).BeginInit();
            this.ListMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListView
            // 
            this.ListView.AllowUserToAddRows = false;
            this.ListView.AllowUserToDeleteRows = false;
            this.ListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ListView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListView.ContextMenuStrip = this.ListMenuStrip;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Wheat;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ListView.DefaultCellStyle = dataGridViewCellStyle2;
            this.ListView.Location = new System.Drawing.Point(5, 30);
            this.ListView.Name = "ListView";
            this.ListView.RowHeadersVisible = false;
            this.ListView.RowTemplate.Height = 21;
            this.ListView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ListView.Size = new System.Drawing.Size(548, 120);
            this.ListView.TabIndex = 0;
            // 
            // ListMenuStrip
            // 
            this.ListMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ListMenuItem1});
            this.ListMenuStrip.Name = "ListMenuStrip";
            this.ListMenuStrip.Size = new System.Drawing.Size(113, 26);
            // 
            // ListMenuItem1
            // 
            this.ListMenuItem1.Name = "ListMenuItem1";
            this.ListMenuItem1.Size = new System.Drawing.Size(112, 22);
            this.ListMenuItem1.Text = "コピー";
            this.ListMenuItem1.Click += new System.EventHandler(this.ListMenuItem1_Click);
            // 
            // StaffBox
            // 
            this.StaffBox.BackColor = System.Drawing.Color.LightYellow;
            this.StaffBox.Location = new System.Drawing.Point(420, 5);
            this.StaffBox.Name = "StaffBox";
            this.StaffBox.ReadOnly = true;
            this.StaffBox.Size = new System.Drawing.Size(100, 19);
            this.StaffBox.TabIndex = 3;
            this.StaffBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ContBox
            // 
            this.ContBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ContBox.Location = new System.Drawing.Point(558, 30);
            this.ContBox.MaxLength = 200;
            this.ContBox.Multiline = true;
            this.ContBox.Name = "ContBox";
            this.ContBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ContBox.Size = new System.Drawing.Size(120, 120);
            this.ContBox.TabIndex = 4;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(550, 3);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(60, 22);
            this.SaveButton.TabIndex = 5;
            this.SaveButton.Text = "登録";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(615, 3);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(60, 22);
            this.DeleteButton.TabIndex = 7;
            this.DeleteButton.Text = "削除";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // EyeBox
            // 
            this.EyeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EyeBox.FormattingEnabled = true;
            this.EyeBox.Location = new System.Drawing.Point(220, 5);
            this.EyeBox.Name = "EyeBox";
            this.EyeBox.Size = new System.Drawing.Size(45, 20);
            this.EyeBox.TabIndex = 8;
            // 
            // LaserDateBox
            // 
            this.LaserDateBox.BackColor = System.Drawing.Color.LightYellow;
            this.LaserDateBox.Location = new System.Drawing.Point(60, 6);
            this.LaserDateBox.Name = "LaserDateBox";
            this.LaserDateBox.ReadOnly = true;
            this.LaserDateBox.Size = new System.Drawing.Size(90, 19);
            this.LaserDateBox.TabIndex = 9;
            this.LaserDateBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(185, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "術眼";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "手術日";
            // 
            // MC500Panel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LavenderBlush;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LaserDateBox);
            this.Controls.Add(this.EyeBox);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.ContBox);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.ListView);
            this.Controls.Add(this.StaffBox);
            this.Name = "MC500Panel";
            this.Size = new System.Drawing.Size(683, 153);
            ((System.ComponentModel.ISupportInitialize)(this.ListView)).EndInit();
            this.ListMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ListView;
        private System.Windows.Forms.TextBox StaffBox;
        private System.Windows.Forms.TextBox ContBox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.ContextMenuStrip ListMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ListMenuItem1;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.ComboBox EyeBox;
        private System.Windows.Forms.TextBox LaserDateBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
