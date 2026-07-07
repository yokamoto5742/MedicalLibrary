namespace MedicalLibrary.Boundary
{
    partial class FormAnesRecord
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAnesRecord));
            this.PtListDate = new System.Windows.Forms.DateTimePicker();
            this.PtListView = new System.Windows.Forms.DataGridView();
            this.AnesListView = new System.Windows.Forms.DataGridView();
            this.AnesImgBox = new System.Windows.Forms.PictureBox();
            this.PtListShowButton = new System.Windows.Forms.Button();
            this.WaitBox1 = new System.Windows.Forms.PictureBox();
            this.WaitLabel1 = new System.Windows.Forms.Label();
            this.stdControlPat11 = new MedicalLibrary.Boundary.StdControlPat1();
            ((System.ComponentModel.ISupportInitialize)(this.PtListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AnesListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AnesImgBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaitBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // PtListDate
            // 
            this.PtListDate.Location = new System.Drawing.Point(5, 280);
            this.PtListDate.Name = "PtListDate";
            this.PtListDate.Size = new System.Drawing.Size(112, 19);
            this.PtListDate.TabIndex = 0;
            // 
            // PtListView
            // 
            this.PtListView.AllowUserToAddRows = false;
            this.PtListView.AllowUserToDeleteRows = false;
            this.PtListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PtListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.PtListView.DefaultCellStyle = dataGridViewCellStyle1;
            this.PtListView.Location = new System.Drawing.Point(5, 305);
            this.PtListView.MultiSelect = false;
            this.PtListView.Name = "PtListView";
            this.PtListView.ReadOnly = true;
            this.PtListView.RowHeadersVisible = false;
            this.PtListView.RowTemplate.Height = 21;
            this.PtListView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PtListView.Size = new System.Drawing.Size(215, 312);
            this.PtListView.TabIndex = 1;
            this.PtListView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PtListView_CellClick);
            // 
            // AnesListView
            // 
            this.AnesListView.AllowUserToAddRows = false;
            this.AnesListView.AllowUserToDeleteRows = false;
            this.AnesListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.AnesListView.DefaultCellStyle = dataGridViewCellStyle2;
            this.AnesListView.Location = new System.Drawing.Point(5, 40);
            this.AnesListView.MultiSelect = false;
            this.AnesListView.Name = "AnesListView";
            this.AnesListView.ReadOnly = true;
            this.AnesListView.RowHeadersVisible = false;
            this.AnesListView.RowTemplate.Height = 21;
            this.AnesListView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AnesListView.Size = new System.Drawing.Size(215, 233);
            this.AnesListView.TabIndex = 5;
            this.AnesListView.SelectionChanged += new System.EventHandler(this.AnesListView_SelectionChanged);
            // 
            // AnesImgBox
            // 
            this.AnesImgBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AnesImgBox.BackColor = System.Drawing.Color.White;
            this.AnesImgBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.AnesImgBox.Location = new System.Drawing.Point(225, 40);
            this.AnesImgBox.Name = "AnesImgBox";
            this.AnesImgBox.Size = new System.Drawing.Size(780, 580);
            this.AnesImgBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.AnesImgBox.TabIndex = 6;
            this.AnesImgBox.TabStop = false;
            this.AnesImgBox.DoubleClick += new System.EventHandler(this.AnesImgBox_DoubleClick);
            // 
            // PtListShowButton
            // 
            this.PtListShowButton.Location = new System.Drawing.Point(125, 278);
            this.PtListShowButton.Name = "PtListShowButton";
            this.PtListShowButton.Size = new System.Drawing.Size(75, 22);
            this.PtListShowButton.TabIndex = 8;
            this.PtListShowButton.Text = "表示";
            this.PtListShowButton.UseVisualStyleBackColor = true;
            this.PtListShowButton.Click += new System.EventHandler(this.PtListShowButton_Click);
            // 
            // WaitBox1
            // 
            this.WaitBox1.Image = ((System.Drawing.Image)(resources.GetObject("WaitBox1.Image")));
            this.WaitBox1.Location = new System.Drawing.Point(450, 225);
            this.WaitBox1.Name = "WaitBox1";
            this.WaitBox1.Size = new System.Drawing.Size(150, 150);
            this.WaitBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.WaitBox1.TabIndex = 9;
            this.WaitBox1.TabStop = false;
            this.WaitBox1.Visible = false;
            // 
            // WaitLabel1
            // 
            this.WaitLabel1.AutoSize = true;
            this.WaitLabel1.BackColor = System.Drawing.Color.Transparent;
            this.WaitLabel1.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.WaitLabel1.Location = new System.Drawing.Point(429, 387);
            this.WaitLabel1.Name = "WaitLabel1";
            this.WaitLabel1.Size = new System.Drawing.Size(204, 14);
            this.WaitLabel1.TabIndex = 10;
            this.WaitLabel1.Text = "処理中です。しばらくお待ちください...";
            this.WaitLabel1.Visible = false;
            // 
            // stdControlPat11
            // 
            this.stdControlPat11.Location = new System.Drawing.Point(5, 5);
            this.stdControlPat11.Mode1 = MedicalLibrary.Boundary.StdControlPat1.Mode.Normal;
            this.stdControlPat11.Name = "stdControlPat11";
            this.stdControlPat11.ReadOnly = false;
            this.stdControlPat11.Size = new System.Drawing.Size(450, 30);
            this.stdControlPat11.TabIndex = 11;
            // 
            // FormAnesRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 622);
            this.Controls.Add(this.stdControlPat11);
            this.Controls.Add(this.WaitLabel1);
            this.Controls.Add(this.WaitBox1);
            this.Controls.Add(this.PtListShowButton);
            this.Controls.Add(this.AnesImgBox);
            this.Controls.Add(this.AnesListView);
            this.Controls.Add(this.PtListView);
            this.Controls.Add(this.PtListDate);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormAnesRecord";
            this.Text = "麻酔記録";
            this.Load += new System.EventHandler(this.FormAnesRecord_Load);
            this.Shown += new System.EventHandler(this.FormAnesRecord_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.PtListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AnesListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AnesImgBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaitBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker PtListDate;
        private System.Windows.Forms.DataGridView PtListView;
        private System.Windows.Forms.DataGridView AnesListView;
        private System.Windows.Forms.PictureBox AnesImgBox;
        private System.Windows.Forms.Button PtListShowButton;
        private System.Windows.Forms.PictureBox WaitBox1;
        private System.Windows.Forms.Label WaitLabel1;
        private StdControlPat1 stdControlPat11;
    }
}