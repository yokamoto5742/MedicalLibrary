namespace MedicalLibrary.Boundary
{
    partial class FormProblem1
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
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.DatePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.StaffLabel2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.InOutShowBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ResolveBox1 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.InOutBox1 = new System.Windows.Forms.ComboBox();
            this.StaffLabel1 = new System.Windows.Forms.Label();
            this.DatePicker1 = new System.Windows.Forms.DateTimePicker();
            this.RegButton1 = new System.Windows.Forms.Button();
            this.ClearButton1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ResolveShowBox1 = new System.Windows.Forms.CheckBox();
            this.ContBox1 = new System.Windows.Forms.TextBox();
            this.ProblemMasterTreeView1 = new System.Windows.Forms.TreeView();
            this.DeptBox2 = new MedicalLibrary.Boundary.CtrlDeptBox1();
            this.SectionBox1 = new MedicalLibrary.Boundary.CtrlSectionBox1();
            this.DeptBox1 = new MedicalLibrary.Boundary.CtrlDeptBox1();
            this.ProblemGridView1 = new MedicalLibrary.Boundary.CtrlProblemGridView1();
            this.stdControlPat11 = new MedicalLibrary.Boundary.StdControlPat1();
            this.DeleteButton1 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ResolveMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.ProblemGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(420, 345);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 40;
            this.label8.Text = "登録者";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 345);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 39;
            this.label7.Text = "登録者";
            // 
            // DatePicker2
            // 
            this.DatePicker2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DatePicker2.CustomFormat = "yyyy/MM/dd (ddd)";
            this.DatePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatePicker2.Location = new System.Drawing.Point(463, 295);
            this.DatePicker2.Name = "DatePicker2";
            this.DatePicker2.Size = new System.Drawing.Size(125, 19);
            this.DatePicker2.TabIndex = 38;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(420, 299);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 37;
            this.label9.Text = "解決日";
            // 
            // StaffLabel2
            // 
            this.StaffLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.StaffLabel2.AutoEllipsis = true;
            this.StaffLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.StaffLabel2.Location = new System.Drawing.Point(465, 341);
            this.StaffLabel2.Name = "StaffLabel2";
            this.StaffLabel2.Size = new System.Drawing.Size(120, 18);
            this.StaffLabel2.TabIndex = 36;
            this.StaffLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(420, 322);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 33;
            this.label6.Text = "解決科";
            // 
            // InOutShowBox1
            // 
            this.InOutShowBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InOutShowBox1.FormattingEnabled = true;
            this.InOutShowBox1.Location = new System.Drawing.Point(300, 32);
            this.InOutShowBox1.Name = "InOutShowBox1";
            this.InOutShowBox1.Size = new System.Drawing.Size(60, 20);
            this.InOutShowBox1.TabIndex = 32;
            this.InOutShowBox1.SelectedIndexChanged += new System.EventHandler(this.InOutShowBox1_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(665, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(174, 24);
            this.label5.TabIndex = 30;
            this.label5.Text = "プロブレムマスター（病棟看護用）\r\n※該当するものをダブルクリック";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 12);
            this.label4.TabIndex = 29;
            this.label4.Text = "プロブレムリスト";
            // 
            // ResolveBox1
            // 
            this.ResolveBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ResolveBox1.AutoSize = true;
            this.ResolveBox1.Location = new System.Drawing.Point(425, 275);
            this.ResolveBox1.Name = "ResolveBox1";
            this.ResolveBox1.Size = new System.Drawing.Size(48, 16);
            this.ResolveBox1.TabIndex = 28;
            this.ResolveBox1.Text = "解決";
            this.ResolveBox1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 276);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 27;
            this.label3.Text = "入外";
            // 
            // InOutBox1
            // 
            this.InOutBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.InOutBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InOutBox1.FormattingEnabled = true;
            this.InOutBox1.Location = new System.Drawing.Point(45, 271);
            this.InOutBox1.Name = "InOutBox1";
            this.InOutBox1.Size = new System.Drawing.Size(60, 20);
            this.InOutBox1.TabIndex = 26;
            // 
            // StaffLabel1
            // 
            this.StaffLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.StaffLabel1.AutoEllipsis = true;
            this.StaffLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.StaffLabel1.Location = new System.Drawing.Point(45, 341);
            this.StaffLabel1.Name = "StaffLabel1";
            this.StaffLabel1.Size = new System.Drawing.Size(120, 18);
            this.StaffLabel1.TabIndex = 25;
            this.StaffLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DatePicker1
            // 
            this.DatePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DatePicker1.CustomFormat = "yyyy/MM/dd (ddd)";
            this.DatePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatePicker1.Location = new System.Drawing.Point(45, 295);
            this.DatePicker1.Name = "DatePicker1";
            this.DatePicker1.Size = new System.Drawing.Size(125, 19);
            this.DatePicker1.TabIndex = 23;
            // 
            // RegButton1
            // 
            this.RegButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RegButton1.Location = new System.Drawing.Point(595, 316);
            this.RegButton1.Name = "RegButton1";
            this.RegButton1.Size = new System.Drawing.Size(60, 42);
            this.RegButton1.TabIndex = 21;
            this.RegButton1.Text = "登録";
            this.RegButton1.UseVisualStyleBackColor = true;
            this.RegButton1.Click += new System.EventHandler(this.RegButton1_Click);
            // 
            // ClearButton1
            // 
            this.ClearButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ClearButton1.Location = new System.Drawing.Point(595, 292);
            this.ClearButton1.Name = "ClearButton1";
            this.ClearButton1.Size = new System.Drawing.Size(60, 22);
            this.ClearButton1.TabIndex = 20;
            this.ClearButton1.Text = "クリア";
            this.ClearButton1.UseVisualStyleBackColor = true;
            this.ClearButton1.Click += new System.EventHandler(this.ClearButton1_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 299);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = "発生日";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 322);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "発生科";
            // 
            // ResolveShowBox1
            // 
            this.ResolveShowBox1.AutoSize = true;
            this.ResolveShowBox1.Location = new System.Drawing.Point(400, 35);
            this.ResolveShowBox1.Name = "ResolveShowBox1";
            this.ResolveShowBox1.Size = new System.Drawing.Size(96, 16);
            this.ResolveShowBox1.TabIndex = 17;
            this.ResolveShowBox1.Text = "解決済み表示";
            this.ResolveShowBox1.UseVisualStyleBackColor = true;
            this.ResolveShowBox1.CheckedChanged += new System.EventHandler(this.ResolveShowBox1_CheckedChanged);
            // 
            // ContBox1
            // 
            this.ContBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ContBox1.Location = new System.Drawing.Point(180, 270);
            this.ContBox1.Multiline = true;
            this.ContBox1.Name = "ContBox1";
            this.ContBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ContBox1.Size = new System.Drawing.Size(235, 88);
            this.ContBox1.TabIndex = 16;
            // 
            // ProblemMasterTreeView1
            // 
            this.ProblemMasterTreeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProblemMasterTreeView1.Location = new System.Drawing.Point(660, 53);
            this.ProblemMasterTreeView1.Name = "ProblemMasterTreeView1";
            this.ProblemMasterTreeView1.Size = new System.Drawing.Size(200, 305);
            this.ProblemMasterTreeView1.TabIndex = 2;
            this.ProblemMasterTreeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ProblemMasterTreeView1_NodeMouseDoubleClick);
            // 
            // DeptBox2
            // 
            this.DeptBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeptBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeptBox2.FormattingEnabled = true;
            this.DeptBox2.Location = new System.Drawing.Point(463, 317);
            this.DeptBox2.Name = "DeptBox2";
            this.DeptBox2.Size = new System.Drawing.Size(121, 20);
            this.DeptBox2.TabIndex = 34;
            // 
            // SectionBox1
            // 
            this.SectionBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SectionBox1.FormattingEnabled = true;
            this.SectionBox1.Location = new System.Drawing.Point(180, 32);
            this.SectionBox1.Name = "SectionBox1";
            this.SectionBox1.Size = new System.Drawing.Size(105, 20);
            this.SectionBox1.TabIndex = 31;
            this.SectionBox1.SelectedIndexChanged += new System.EventHandler(this.SectionBox1_SelectedIndexChanged);
            // 
            // DeptBox1
            // 
            this.DeptBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeptBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeptBox1.FormattingEnabled = true;
            this.DeptBox1.Location = new System.Drawing.Point(45, 317);
            this.DeptBox1.Name = "DeptBox1";
            this.DeptBox1.Size = new System.Drawing.Size(121, 20);
            this.DeptBox1.TabIndex = 24;
            // 
            // ProblemGridView1
            // 
            this.ProblemGridView1.AllowUserToAddRows = false;
            this.ProblemGridView1.AllowUserToDeleteRows = false;
            this.ProblemGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ProblemGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ProblemGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ProblemGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProblemGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.ProblemGridView1.Location = new System.Drawing.Point(2, 53);
            this.ProblemGridView1.Name = "ProblemGridView1";
            this.ProblemGridView1.ReadOnly = true;
            this.ProblemGridView1.RowHeadersVisible = false;
            this.ProblemGridView1.RowTemplate.Height = 21;
            this.ProblemGridView1.Size = new System.Drawing.Size(655, 210);
            this.ProblemGridView1.TabIndex = 1;
            this.ProblemGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProblemGridView1_CellClick);
            // 
            // stdControlPat11
            // 
            this.stdControlPat11.Location = new System.Drawing.Point(2, 2);
            this.stdControlPat11.Mode1 = MedicalLibrary.Boundary.StdControlPat1.Mode.Normal;
            this.stdControlPat11.Name = "stdControlPat11";
            this.stdControlPat11.ReadOnly = false;
            this.stdControlPat11.Size = new System.Drawing.Size(450, 30);
            this.stdControlPat11.TabIndex = 0;
            // 
            // DeleteButton1
            // 
            this.DeleteButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeleteButton1.Location = new System.Drawing.Point(595, 268);
            this.DeleteButton1.Name = "DeleteButton1";
            this.DeleteButton1.Size = new System.Drawing.Size(60, 22);
            this.DeleteButton1.TabIndex = 41;
            this.DeleteButton1.Text = "削除";
            this.DeleteButton1.UseVisualStyleBackColor = true;
            this.DeleteButton1.Click += new System.EventHandler(this.DeleteButton1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ResolveMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // ResolveMenuItem1
            // 
            this.ResolveMenuItem1.Name = "ResolveMenuItem1";
            this.ResolveMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.ResolveMenuItem1.Text = "解決";
            this.ResolveMenuItem1.Click += new System.EventHandler(this.ResolveMenuItem1_Click);
            // 
            // FormProblem1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 362);
            this.Controls.Add(this.DeleteButton1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.DatePicker2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.StaffLabel2);
            this.Controls.Add(this.DeptBox2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.InOutShowBox1);
            this.Controls.Add(this.SectionBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ResolveBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.InOutBox1);
            this.Controls.Add(this.StaffLabel1);
            this.Controls.Add(this.DeptBox1);
            this.Controls.Add(this.DatePicker1);
            this.Controls.Add(this.RegButton1);
            this.Controls.Add(this.ClearButton1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ResolveShowBox1);
            this.Controls.Add(this.ContBox1);
            this.Controls.Add(this.ProblemMasterTreeView1);
            this.Controls.Add(this.ProblemGridView1);
            this.Controls.Add(this.stdControlPat11);
            this.Name = "FormProblem1";
            this.Text = "プロブレム";
            ((System.ComponentModel.ISupportInitialize)(this.ProblemGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StdControlPat1 stdControlPat11;
        private CtrlProblemGridView1 ProblemGridView1;
        private System.Windows.Forms.TreeView ProblemMasterTreeView1;
        private System.Windows.Forms.Label StaffLabel1;
        private CtrlDeptBox1 DeptBox1;
        private System.Windows.Forms.DateTimePicker DatePicker1;
        private System.Windows.Forms.Button RegButton1;
        private System.Windows.Forms.Button ClearButton1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ResolveShowBox1;
        private System.Windows.Forms.TextBox ContBox1;
        private System.Windows.Forms.ComboBox InOutBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ResolveBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private CtrlSectionBox1 SectionBox1;
        private System.Windows.Forms.ComboBox InOutShowBox1;
        private CtrlDeptBox1 DeptBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label StaffLabel2;
        private System.Windows.Forms.DateTimePicker DatePicker2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button DeleteButton1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ResolveMenuItem1;
    }
}