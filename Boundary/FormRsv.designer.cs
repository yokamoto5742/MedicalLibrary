namespace MedicalLibrary.Boundary
{
    partial class FormRsv
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
            this.CodeBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ShowButton = new System.Windows.Forms.Button();
            this.CodeBox2 = new System.Windows.Forms.ComboBox();
            this.RsvView1 = new System.Windows.Forms.DataGridView();
            this.RsvPanel1 = new System.Windows.Forms.Panel();
            this.RsvBox1 = new System.Windows.Forms.PictureBox();
            this.RsvTimePanel = new System.Windows.Forms.Panel();
            this.RsvTimeBox1 = new System.Windows.Forms.PictureBox();
            this.RsvDatePanel = new System.Windows.Forms.Panel();
            this.RsvDateBox1 = new System.Windows.Forms.PictureBox();
            this.DateBox1 = new System.Windows.Forms.DateTimePicker();
            this.RsvLabel1 = new System.Windows.Forms.Label();
            this.stdControlPat11 = new MedicalLibrary.Boundary.StdControlPat1();
            this.NewRsvButton1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.RsvView1)).BeginInit();
            this.RsvPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RsvBox1)).BeginInit();
            this.RsvTimePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RsvTimeBox1)).BeginInit();
            this.RsvDatePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RsvDateBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // CodeBox1
            // 
            this.CodeBox1.Enabled = false;
            this.CodeBox1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.CodeBox1.Location = new System.Drawing.Point(802, 9);
            this.CodeBox1.MaxLength = 5;
            this.CodeBox1.Name = "CodeBox1";
            this.CodeBox1.Size = new System.Drawing.Size(60, 19);
            this.CodeBox1.TabIndex = 1;
            this.CodeBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CodeBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CodeBox1_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(745, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "予約種別";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(867, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "予約詳細";
            // 
            // ShowButton
            // 
            this.ShowButton.Location = new System.Drawing.Point(680, 8);
            this.ShowButton.Name = "ShowButton";
            this.ShowButton.Size = new System.Drawing.Size(50, 23);
            this.ShowButton.TabIndex = 5;
            this.ShowButton.Text = "表示";
            this.ShowButton.UseVisualStyleBackColor = true;
            this.ShowButton.Click += new System.EventHandler(this.ShowButton_Click);
            // 
            // CodeBox2
            // 
            this.CodeBox2.Enabled = false;
            this.CodeBox2.FormattingEnabled = true;
            this.CodeBox2.Location = new System.Drawing.Point(923, 8);
            this.CodeBox2.Name = "CodeBox2";
            this.CodeBox2.Size = new System.Drawing.Size(80, 20);
            this.CodeBox2.TabIndex = 6;
            // 
            // RsvView1
            // 
            this.RsvView1.AllowUserToAddRows = false;
            this.RsvView1.AllowUserToDeleteRows = false;
            this.RsvView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.RsvView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.RsvView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RsvView1.Location = new System.Drawing.Point(10, 65);
            this.RsvView1.Name = "RsvView1";
            this.RsvView1.ReadOnly = true;
            this.RsvView1.RowHeadersVisible = false;
            this.RsvView1.RowHeadersWidth = 21;
            this.RsvView1.RowTemplate.Height = 21;
            this.RsvView1.Size = new System.Drawing.Size(330, 490);
            this.RsvView1.TabIndex = 7;
            this.RsvView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RsvView1_CellClick);
            this.RsvView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RsvView1_CellDoubleClick);
            // 
            // RsvPanel1
            // 
            this.RsvPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RsvPanel1.AutoScroll = true;
            this.RsvPanel1.Controls.Add(this.RsvBox1);
            this.RsvPanel1.Location = new System.Drawing.Point(405, 100);
            this.RsvPanel1.Name = "RsvPanel1";
            this.RsvPanel1.Size = new System.Drawing.Size(600, 460);
            this.RsvPanel1.TabIndex = 8;
            this.RsvPanel1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.RsvPanel1_Scroll);
            // 
            // RsvBox1
            // 
            this.RsvBox1.BackColor = System.Drawing.Color.White;
            this.RsvBox1.Location = new System.Drawing.Point(3, 3);
            this.RsvBox1.Name = "RsvBox1";
            this.RsvBox1.Size = new System.Drawing.Size(592, 450);
            this.RsvBox1.TabIndex = 0;
            this.RsvBox1.TabStop = false;
            this.RsvBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RsvBox1_MouseClick);
            // 
            // RsvTimePanel
            // 
            this.RsvTimePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.RsvTimePanel.Controls.Add(this.RsvTimeBox1);
            this.RsvTimePanel.Location = new System.Drawing.Point(345, 100);
            this.RsvTimePanel.Name = "RsvTimePanel";
            this.RsvTimePanel.Size = new System.Drawing.Size(53, 460);
            this.RsvTimePanel.TabIndex = 9;
            // 
            // RsvTimeBox1
            // 
            this.RsvTimeBox1.BackColor = System.Drawing.Color.White;
            this.RsvTimeBox1.Location = new System.Drawing.Point(3, 3);
            this.RsvTimeBox1.Name = "RsvTimeBox1";
            this.RsvTimeBox1.Size = new System.Drawing.Size(50, 450);
            this.RsvTimeBox1.TabIndex = 0;
            this.RsvTimeBox1.TabStop = false;
            // 
            // RsvDatePanel
            // 
            this.RsvDatePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RsvDatePanel.Controls.Add(this.RsvDateBox1);
            this.RsvDatePanel.Location = new System.Drawing.Point(405, 35);
            this.RsvDatePanel.Name = "RsvDatePanel";
            this.RsvDatePanel.Size = new System.Drawing.Size(600, 63);
            this.RsvDatePanel.TabIndex = 10;
            // 
            // RsvDateBox1
            // 
            this.RsvDateBox1.BackColor = System.Drawing.Color.White;
            this.RsvDateBox1.Location = new System.Drawing.Point(3, 3);
            this.RsvDateBox1.Name = "RsvDateBox1";
            this.RsvDateBox1.Size = new System.Drawing.Size(592, 60);
            this.RsvDateBox1.TabIndex = 0;
            this.RsvDateBox1.TabStop = false;
            // 
            // DateBox1
            // 
            this.DateBox1.CustomFormat = "yyyy/MM/dd (ddd)";
            this.DateBox1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateBox1.Location = new System.Drawing.Point(546, 10);
            this.DateBox1.Name = "DateBox1";
            this.DateBox1.Size = new System.Drawing.Size(120, 19);
            this.DateBox1.TabIndex = 11;
            // 
            // RsvLabel1
            // 
            this.RsvLabel1.BackColor = System.Drawing.Color.LightYellow;
            this.RsvLabel1.Location = new System.Drawing.Point(12, 40);
            this.RsvLabel1.Name = "RsvLabel1";
            this.RsvLabel1.Size = new System.Drawing.Size(180, 20);
            this.RsvLabel1.TabIndex = 12;
            this.RsvLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // stdControlPat11
            // 
            this.stdControlPat11.Location = new System.Drawing.Point(10, 2);
            this.stdControlPat11.Mode1 = MedicalLibrary.Boundary.StdControlPat1.Mode.Normal;
            this.stdControlPat11.Name = "stdControlPat11";
            this.stdControlPat11.ReadOnly = false;
            this.stdControlPat11.Size = new System.Drawing.Size(500, 30);
            this.stdControlPat11.TabIndex = 14;
            // 
            // NewRsvButton1
            // 
            this.NewRsvButton1.Location = new System.Drawing.Point(265, 40);
            this.NewRsvButton1.Name = "NewRsvButton1";
            this.NewRsvButton1.Size = new System.Drawing.Size(75, 23);
            this.NewRsvButton1.TabIndex = 15;
            this.NewRsvButton1.Text = "新規";
            this.NewRsvButton1.UseVisualStyleBackColor = true;
            this.NewRsvButton1.Click += new System.EventHandler(this.NewRsvButton1_Click);
            // 
            // FormRsv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1008, 562);
            this.Controls.Add(this.NewRsvButton1);
            this.Controls.Add(this.stdControlPat11);
            this.Controls.Add(this.RsvLabel1);
            this.Controls.Add(this.DateBox1);
            this.Controls.Add(this.RsvDatePanel);
            this.Controls.Add(this.RsvTimePanel);
            this.Controls.Add(this.RsvPanel1);
            this.Controls.Add(this.RsvView1);
            this.Controls.Add(this.CodeBox2);
            this.Controls.Add(this.ShowButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CodeBox1);
            this.Name = "FormRsv";
            this.Text = "予約";
            this.Load += new System.EventHandler(this.FormRsv_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RsvView1)).EndInit();
            this.RsvPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RsvBox1)).EndInit();
            this.RsvTimePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RsvTimeBox1)).EndInit();
            this.RsvDatePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RsvDateBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox CodeBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ShowButton;
        private System.Windows.Forms.ComboBox CodeBox2;
        private System.Windows.Forms.DataGridView RsvView1;
        private System.Windows.Forms.Panel RsvPanel1;
        private System.Windows.Forms.PictureBox RsvBox1;
        private System.Windows.Forms.Panel RsvTimePanel;
        private System.Windows.Forms.PictureBox RsvTimeBox1;
        private System.Windows.Forms.Panel RsvDatePanel;
        private System.Windows.Forms.PictureBox RsvDateBox1;
        private System.Windows.Forms.DateTimePicker DateBox1;
        private System.Windows.Forms.Label RsvLabel1;
        private MedicalLibrary.Boundary.StdControlPat1 stdControlPat11;
        private System.Windows.Forms.Button NewRsvButton1;
    }
}