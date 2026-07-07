namespace MedicalLibrary.Agent
{
    partial class FormComeReportOutSide
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormComeReportOutSide));
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.UndoneOutBox1 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DateTime12 = new System.Windows.Forms.DateTimePicker();
            this.ShowButton1 = new System.Windows.Forms.Button();
            this.DateTime11 = new System.Windows.Forms.DateTimePicker();
            this.GridView1 = new System.Windows.Forms.DataGridView();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.UndoneOutBox2 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ShowButton2 = new System.Windows.Forms.Button();
            this.DateTime22 = new System.Windows.Forms.DateTimePicker();
            this.DateTime21 = new System.Windows.Forms.DateTimePicker();
            this.GridView2 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.FileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TabControl1.SuspendLayout();
            this.TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridView1)).BeginInit();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridView2)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl1
            // 
            this.TabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl1.Controls.Add(this.TabPage1);
            this.TabControl1.Controls.Add(this.TabPage2);
            this.TabControl1.Location = new System.Drawing.Point(12, 38);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(1000, 700);
            this.TabControl1.TabIndex = 0;
            // 
            // TabPage1
            // 
            this.TabPage1.Controls.Add(this.UndoneOutBox1);
            this.TabPage1.Controls.Add(this.label4);
            this.TabPage1.Controls.Add(this.label3);
            this.TabPage1.Controls.Add(this.DateTime12);
            this.TabPage1.Controls.Add(this.ShowButton1);
            this.TabPage1.Controls.Add(this.DateTime11);
            this.TabPage1.Controls.Add(this.GridView1);
            this.TabPage1.Location = new System.Drawing.Point(4, 22);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(992, 674);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "オーダー";
            this.TabPage1.UseVisualStyleBackColor = true;
            // 
            // UndoneOutBox1
            // 
            this.UndoneOutBox1.AutoSize = true;
            this.UndoneOutBox1.Checked = true;
            this.UndoneOutBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UndoneOutBox1.Location = new System.Drawing.Point(452, 12);
            this.UndoneOutBox1.Name = "UndoneOutBox1";
            this.UndoneOutBox1.Size = new System.Drawing.Size(81, 16);
            this.UndoneOutBox1.TabIndex = 8;
            this.UndoneOutBox1.Text = "未依頼のみ";
            this.UndoneOutBox1.UseVisualStyleBackColor = true;
            this.UndoneOutBox1.CheckedChanged += new System.EventHandler(this.UndoneOutBox1_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(168, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "～";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "期間";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DateTime12
            // 
            this.DateTime12.Location = new System.Drawing.Point(188, 10);
            this.DateTime12.Name = "DateTime12";
            this.DateTime12.Size = new System.Drawing.Size(113, 19);
            this.DateTime12.TabIndex = 5;
            // 
            // ShowButton1
            // 
            this.ShowButton1.Location = new System.Drawing.Point(319, 8);
            this.ShowButton1.Name = "ShowButton1";
            this.ShowButton1.Size = new System.Drawing.Size(75, 23);
            this.ShowButton1.TabIndex = 4;
            this.ShowButton1.Text = "表示";
            this.ShowButton1.UseVisualStyleBackColor = true;
            this.ShowButton1.Click += new System.EventHandler(this.ShowButton1_Click);
            // 
            // DateTime11
            // 
            this.DateTime11.Location = new System.Drawing.Point(50, 10);
            this.DateTime11.Name = "DateTime11";
            this.DateTime11.Size = new System.Drawing.Size(113, 19);
            this.DateTime11.TabIndex = 2;
            // 
            // GridView1
            // 
            this.GridView1.AllowUserToAddRows = false;
            this.GridView1.AllowUserToDeleteRows = false;
            this.GridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.GridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.GridView1.Location = new System.Drawing.Point(6, 40);
            this.GridView1.Name = "GridView1";
            this.GridView1.RowHeadersVisible = false;
            this.GridView1.RowTemplate.Height = 21;
            this.GridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridView1.Size = new System.Drawing.Size(980, 630);
            this.GridView1.TabIndex = 1;
            this.GridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridView1_CellContentClick);
            this.GridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridView1_CellDoubleClick);
            // 
            // TabPage2
            // 
            this.TabPage2.Controls.Add(this.UndoneOutBox2);
            this.TabPage2.Controls.Add(this.label2);
            this.TabPage2.Controls.Add(this.label1);
            this.TabPage2.Controls.Add(this.ShowButton2);
            this.TabPage2.Controls.Add(this.DateTime22);
            this.TabPage2.Controls.Add(this.DateTime21);
            this.TabPage2.Controls.Add(this.GridView2);
            this.TabPage2.Location = new System.Drawing.Point(4, 22);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(992, 674);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "所見";
            this.TabPage2.UseVisualStyleBackColor = true;
            // 
            // UndoneOutBox2
            // 
            this.UndoneOutBox2.AutoSize = true;
            this.UndoneOutBox2.Checked = true;
            this.UndoneOutBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UndoneOutBox2.Location = new System.Drawing.Point(452, 12);
            this.UndoneOutBox2.Name = "UndoneOutBox2";
            this.UndoneOutBox2.Size = new System.Drawing.Size(81, 16);
            this.UndoneOutBox2.TabIndex = 9;
            this.UndoneOutBox2.Text = "未依頼のみ";
            this.UndoneOutBox2.UseVisualStyleBackColor = true;
            this.UndoneOutBox2.CheckedChanged += new System.EventHandler(this.UndoneOutBox2_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "期間";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(168, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "～";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ShowButton2
            // 
            this.ShowButton2.Location = new System.Drawing.Point(319, 8);
            this.ShowButton2.Name = "ShowButton2";
            this.ShowButton2.Size = new System.Drawing.Size(75, 23);
            this.ShowButton2.TabIndex = 3;
            this.ShowButton2.Text = "表示";
            this.ShowButton2.UseVisualStyleBackColor = true;
            this.ShowButton2.Click += new System.EventHandler(this.ShowButton2_Click);
            // 
            // DateTime22
            // 
            this.DateTime22.Location = new System.Drawing.Point(188, 10);
            this.DateTime22.Name = "DateTime22";
            this.DateTime22.Size = new System.Drawing.Size(113, 19);
            this.DateTime22.TabIndex = 2;
            // 
            // DateTime21
            // 
            this.DateTime21.Location = new System.Drawing.Point(50, 10);
            this.DateTime21.Name = "DateTime21";
            this.DateTime21.Size = new System.Drawing.Size(113, 19);
            this.DateTime21.TabIndex = 1;
            // 
            // GridView2
            // 
            this.GridView2.AllowUserToAddRows = false;
            this.GridView2.AllowUserToDeleteRows = false;
            this.GridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GridView2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.GridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridView2.DefaultCellStyle = dataGridViewCellStyle2;
            this.GridView2.Location = new System.Drawing.Point(6, 40);
            this.GridView2.Name = "GridView2";
            this.GridView2.RowHeadersVisible = false;
            this.GridView2.RowTemplate.Height = 21;
            this.GridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridView2.Size = new System.Drawing.Size(980, 630);
            this.GridView2.TabIndex = 0;
            this.GridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridView2_CellContentClick);
            this.GridView2.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridView2_CellDoubleClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1016, 26);
            this.menuStrip1.TabIndex = 1;
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
            this.FileExitMenuItem.Click += new System.EventHandler(this.FileExitMenuItem_Click);
            // 
            // FormOutSide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.TabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormOutSide";
            this.Text = "読影依頼";
            this.Load += new System.EventHandler(this.FormComeReportOutSide_Load);
            this.TabControl1.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridView1)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridView2)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl1;
        private System.Windows.Forms.TabPage TabPage1;
        private System.Windows.Forms.TabPage TabPage2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileMenu;
        private System.Windows.Forms.ToolStripMenuItem FileExitMenuItem;
        private System.Windows.Forms.DataGridView GridView2;
        private System.Windows.Forms.DataGridView GridView1;
        private System.Windows.Forms.Button ShowButton1;
        private System.Windows.Forms.DateTimePicker DateTime11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ShowButton2;
        private System.Windows.Forms.DateTimePicker DateTime22;
        private System.Windows.Forms.DateTimePicker DateTime21;
        private System.Windows.Forms.DateTimePicker DateTime12;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox UndoneOutBox1;
        private System.Windows.Forms.CheckBox UndoneOutBox2;
    }
}