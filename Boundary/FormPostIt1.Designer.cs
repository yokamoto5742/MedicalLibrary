namespace MedicalLibrary.Boundary
{
    partial class FormPostIt1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPostIt1));
            this.stdControlPat11 = new MedicalLibrary.Boundary.StdControlPat1();
            this.PostItView1 = new MedicalLibrary.Boundary.CtrlPostItGridView1();
            this.PostItMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ContBox1 = new System.Windows.Forms.TextBox();
            this.CheckBox1 = new System.Windows.Forms.CheckBox();
            this.CheckBox2 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ClearButton1 = new System.Windows.Forms.Button();
            this.RegButton1 = new System.Windows.Forms.Button();
            this.RegDateTimeLabel1 = new System.Windows.Forms.Label();
            this.DoDatePicker1 = new System.Windows.Forms.DateTimePicker();
            this.DeptBox1 = new MedicalLibrary.Boundary.CtrlDeptBox1();
            this.RegStaffLabel1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DeleteButton1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PostItView1)).BeginInit();
            this.PostItMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // stdControlPat11
            // 
            this.stdControlPat11.Location = new System.Drawing.Point(2, 2);
            this.stdControlPat11.Mode1 = MedicalLibrary.Boundary.StdControlPat1.Mode.Normal;
            this.stdControlPat11.Name = "stdControlPat11";
            this.stdControlPat11.ReadOnly = true;
            this.stdControlPat11.Size = new System.Drawing.Size(450, 30);
            this.stdControlPat11.TabIndex = 10;
            // 
            // PostItView1
            // 
            this.PostItView1.AllowUserToAddRows = false;
            this.PostItView1.AllowUserToDeleteRows = false;
            this.PostItView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PostItView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PostItView1.ContextMenuStrip = this.PostItMenuStrip1;
            this.PostItView1.Location = new System.Drawing.Point(5, 53);
            this.PostItView1.Name = "PostItView1";
            this.PostItView1.ReadOnly = true;
            this.PostItView1.RowHeadersWidth = 30;
            this.PostItView1.RowTemplate.Height = 21;
            this.PostItView1.Size = new System.Drawing.Size(440, 138);
            this.PostItView1.TabIndex = 1;
            this.PostItView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PostItView1_CellClick);
            // 
            // PostItMenuStrip1
            // 
            this.PostItMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteMenuItem1});
            this.PostItMenuStrip1.Name = "contextMenuStrip1";
            this.PostItMenuStrip1.Size = new System.Drawing.Size(101, 26);
            this.PostItMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.PostItMenuStrip1_Opening);
            // 
            // DeleteMenuItem1
            // 
            this.DeleteMenuItem1.Name = "DeleteMenuItem1";
            this.DeleteMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.DeleteMenuItem1.Text = "削除";
            this.DeleteMenuItem1.Click += new System.EventHandler(this.DeleteMenuItem1_Click);
            // 
            // ContBox1
            // 
            this.ContBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContBox1.Location = new System.Drawing.Point(190, 217);
            this.ContBox1.MaxLength = 128;
            this.ContBox1.Name = "ContBox1";
            this.ContBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ContBox1.Size = new System.Drawing.Size(255, 19);
            this.ContBox1.TabIndex = 0;
            // 
            // CheckBox1
            // 
            this.CheckBox1.AutoSize = true;
            this.CheckBox1.Location = new System.Drawing.Point(215, 35);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(84, 16);
            this.CheckBox1.TabIndex = 3;
            this.CheckBox1.Text = "当日分のみ";
            this.CheckBox1.UseVisualStyleBackColor = true;
            this.CheckBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // CheckBox2
            // 
            this.CheckBox2.AutoSize = true;
            this.CheckBox2.Location = new System.Drawing.Point(315, 35);
            this.CheckBox2.Name = "CheckBox2";
            this.CheckBox2.Size = new System.Drawing.Size(72, 16);
            this.CheckBox2.TabIndex = 4;
            this.CheckBox2.Text = "削除表示";
            this.CheckBox2.UseVisualStyleBackColor = true;
            this.CheckBox2.CheckedChanged += new System.EventHandler(this.CheckBox2_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 222);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "診療科";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 200);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "対象日";
            // 
            // ClearButton1
            // 
            this.ClearButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearButton1.Location = new System.Drawing.Point(290, 238);
            this.ClearButton1.Name = "ClearButton1";
            this.ClearButton1.Size = new System.Drawing.Size(60, 22);
            this.ClearButton1.TabIndex = 9;
            this.ClearButton1.Text = "クリア";
            this.ClearButton1.UseVisualStyleBackColor = true;
            this.ClearButton1.Click += new System.EventHandler(this.ClearButton1_Click);
            // 
            // RegButton1
            // 
            this.RegButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RegButton1.Location = new System.Drawing.Point(355, 238);
            this.RegButton1.Name = "RegButton1";
            this.RegButton1.Size = new System.Drawing.Size(90, 22);
            this.RegButton1.TabIndex = 10;
            this.RegButton1.Text = "登録";
            this.RegButton1.UseVisualStyleBackColor = true;
            this.RegButton1.Click += new System.EventHandler(this.RegButton1_Click);
            // 
            // RegDateTimeLabel1
            // 
            this.RegDateTimeLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RegDateTimeLabel1.AutoEllipsis = true;
            this.RegDateTimeLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.RegDateTimeLabel1.Location = new System.Drawing.Point(5, 240);
            this.RegDateTimeLabel1.Name = "RegDateTimeLabel1";
            this.RegDateTimeLabel1.Size = new System.Drawing.Size(90, 18);
            this.RegDateTimeLabel1.TabIndex = 11;
            this.RegDateTimeLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DoDatePicker1
            // 
            this.DoDatePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DoDatePicker1.CustomFormat = "yyyy/MM/dd (ddd)";
            this.DoDatePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DoDatePicker1.Location = new System.Drawing.Point(55, 197);
            this.DoDatePicker1.Name = "DoDatePicker1";
            this.DoDatePicker1.Size = new System.Drawing.Size(125, 19);
            this.DoDatePicker1.TabIndex = 13;
            // 
            // DeptBox1
            // 
            this.DeptBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeptBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeptBox1.FormattingEnabled = true;
            this.DeptBox1.Location = new System.Drawing.Point(55, 217);
            this.DeptBox1.Name = "DeptBox1";
            this.DeptBox1.Size = new System.Drawing.Size(121, 20);
            this.DeptBox1.TabIndex = 14;
            // 
            // RegStaffLabel1
            // 
            this.RegStaffLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RegStaffLabel1.AutoEllipsis = true;
            this.RegStaffLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.RegStaffLabel1.Location = new System.Drawing.Point(97, 240);
            this.RegStaffLabel1.Name = "RegStaffLabel1";
            this.RegStaffLabel1.Size = new System.Drawing.Size(85, 18);
            this.RegStaffLabel1.TabIndex = 15;
            this.RegStaffLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(190, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "付箋内容";
            // 
            // DeleteButton1
            // 
            this.DeleteButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteButton1.Location = new System.Drawing.Point(225, 238);
            this.DeleteButton1.Name = "DeleteButton1";
            this.DeleteButton1.Size = new System.Drawing.Size(60, 22);
            this.DeleteButton1.TabIndex = 17;
            this.DeleteButton1.Text = "削除";
            this.DeleteButton1.UseVisualStyleBackColor = true;
            this.DeleteButton1.Click += new System.EventHandler(this.DeleteButton1_Click);
            // 
            // FormPostIt1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 262);
            this.Controls.Add(this.DeleteButton1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RegStaffLabel1);
            this.Controls.Add(this.DeptBox1);
            this.Controls.Add(this.DoDatePicker1);
            this.Controls.Add(this.RegDateTimeLabel1);
            this.Controls.Add(this.RegButton1);
            this.Controls.Add(this.ClearButton1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CheckBox2);
            this.Controls.Add(this.CheckBox1);
            this.Controls.Add(this.ContBox1);
            this.Controls.Add(this.PostItView1);
            this.Controls.Add(this.stdControlPat11);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPostIt1";
            this.Text = "付箋";
            ((System.ComponentModel.ISupportInitialize)(this.PostItView1)).EndInit();
            this.PostItMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StdControlPat1 stdControlPat11;
        private CtrlPostItGridView1 PostItView1;
        private System.Windows.Forms.TextBox ContBox1;
        private System.Windows.Forms.CheckBox CheckBox1;
        private System.Windows.Forms.CheckBox CheckBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ClearButton1;
        private System.Windows.Forms.Button RegButton1;
        private System.Windows.Forms.Label RegDateTimeLabel1;
        private System.Windows.Forms.ContextMenuStrip PostItMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem DeleteMenuItem1;
        private System.Windows.Forms.DateTimePicker DoDatePicker1;
        private CtrlDeptBox1 DeptBox1;
        private System.Windows.Forms.Label RegStaffLabel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button DeleteButton1;
    }
}