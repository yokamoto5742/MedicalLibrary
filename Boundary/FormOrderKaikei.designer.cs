namespace MedicalLibrary.Boundary
{
    partial class FormOrderKaikei
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOrderKaikei));
            this.stdControlFont11 = new MedicalLibrary.Boundary.StdControlFont1();
            this.ctrlDoctorBox11 = new MedicalLibrary.Boundary.CtrlDoctorBox1();
            this.ctrlDeptBox11 = new MedicalLibrary.Boundary.CtrlDeptBox1();
            this.SEQBox1 = new System.Windows.Forms.ComboBox();
            this.SEQLabel1 = new System.Windows.Forms.Label();
            this.InOutPanel = new System.Windows.Forms.Panel();
            this.InOutButton2 = new System.Windows.Forms.RadioButton();
            this.InOutButton1 = new System.Windows.Forms.RadioButton();
            this.KaikeiButton1 = new System.Windows.Forms.Button();
            this.DatePicker2 = new System.Windows.Forms.DateTimePicker();
            this.SelectReceYetButton1 = new System.Windows.Forms.Button();
            this.SelectNoneButton1 = new System.Windows.Forms.Button();
            this.SelectAllButton1 = new System.Windows.Forms.Button();
            this.HistoryPanel1 = new System.Windows.Forms.Panel();
            this.ShowButton1 = new System.Windows.Forms.Button();
            this.DateLabel2 = new System.Windows.Forms.Label();
            this.DatePicker1 = new System.Windows.Forms.DateTimePicker();
            this.stdControlPat11 = new MedicalLibrary.Boundary.StdControlPat1();
            this.InsLabel1 = new System.Windows.Forms.Label();
            this.InsBox1 = new System.Windows.Forms.TextBox();
            this.InsNameLabel1 = new System.Windows.Forms.Label();
            this.StatusLabel1 = new System.Windows.Forms.Label();
            this.StatusLabel2 = new System.Windows.Forms.Label();
            this.StatusLabel3 = new System.Windows.Forms.Label();
            this.OrderKaikeiMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.KaikeiFlgMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KaikeiFlg1MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KaikeiFlg0MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InOutPanel.SuspendLayout();
            this.OrderKaikeiMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // stdControlFont11
            // 
            this.stdControlFont11.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            this.stdControlFont11.Location = new System.Drawing.Point(460, 6);
            this.stdControlFont11.Name = "stdControlFont11";
            this.stdControlFont11.Size = new System.Drawing.Size(60, 30);
            this.stdControlFont11.TabIndex = 23;
            // 
            // ctrlDoctorBox11
            // 
            this.ctrlDoctorBox11.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ctrlDoctorBox11.FormattingEnabled = true;
            this.ctrlDoctorBox11.Location = new System.Drawing.Point(117, 70);
            this.ctrlDoctorBox11.Name = "ctrlDoctorBox11";
            this.ctrlDoctorBox11.Size = new System.Drawing.Size(121, 20);
            this.ctrlDoctorBox11.TabIndex = 22;
            // 
            // ctrlDeptBox11
            // 
            this.ctrlDeptBox11.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ctrlDeptBox11.FormattingEnabled = true;
            this.ctrlDeptBox11.Location = new System.Drawing.Point(12, 70);
            this.ctrlDeptBox11.Name = "ctrlDeptBox11";
            this.ctrlDeptBox11.Size = new System.Drawing.Size(100, 20);
            this.ctrlDeptBox11.TabIndex = 21;
            // 
            // SEQBox1
            // 
            this.SEQBox1.FormattingEnabled = true;
            this.SEQBox1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SEQBox1.Location = new System.Drawing.Point(385, 70);
            this.SEQBox1.Name = "SEQBox1";
            this.SEQBox1.Size = new System.Drawing.Size(32, 20);
            this.SEQBox1.TabIndex = 20;
            this.SEQBox1.Text = "1";
            // 
            // SEQLabel1
            // 
            this.SEQLabel1.AutoSize = true;
            this.SEQLabel1.Location = new System.Drawing.Point(420, 74);
            this.SEQLabel1.Name = "SEQLabel1";
            this.SEQLabel1.Size = new System.Drawing.Size(29, 12);
            this.SEQLabel1.TabIndex = 19;
            this.SEQLabel1.Text = "回目";
            // 
            // InOutPanel
            // 
            this.InOutPanel.Controls.Add(this.InOutButton2);
            this.InOutPanel.Controls.Add(this.InOutButton1);
            this.InOutPanel.Location = new System.Drawing.Point(12, 38);
            this.InOutPanel.Name = "InOutPanel";
            this.InOutPanel.Size = new System.Drawing.Size(130, 25);
            this.InOutPanel.TabIndex = 17;
            // 
            // InOutButton2
            // 
            this.InOutButton2.AutoSize = true;
            this.InOutButton2.Location = new System.Drawing.Point(65, 5);
            this.InOutButton2.Name = "InOutButton2";
            this.InOutButton2.Size = new System.Drawing.Size(47, 16);
            this.InOutButton2.TabIndex = 17;
            this.InOutButton2.TabStop = true;
            this.InOutButton2.Text = "入院";
            this.InOutButton2.UseVisualStyleBackColor = true;
            this.InOutButton2.CheckedChanged += new System.EventHandler(this.InOutButton2_CheckedChanged);
            // 
            // InOutButton1
            // 
            this.InOutButton1.AutoSize = true;
            this.InOutButton1.Location = new System.Drawing.Point(5, 5);
            this.InOutButton1.Name = "InOutButton1";
            this.InOutButton1.Size = new System.Drawing.Size(47, 16);
            this.InOutButton1.TabIndex = 16;
            this.InOutButton1.TabStop = true;
            this.InOutButton1.Text = "外来";
            this.InOutButton1.UseVisualStyleBackColor = true;
            this.InOutButton1.CheckedChanged += new System.EventHandler(this.InOutButton1_CheckedChanged);
            // 
            // KaikeiButton1
            // 
            this.KaikeiButton1.Location = new System.Drawing.Point(450, 68);
            this.KaikeiButton1.Name = "KaikeiButton1";
            this.KaikeiButton1.Size = new System.Drawing.Size(60, 25);
            this.KaikeiButton1.TabIndex = 15;
            this.KaikeiButton1.Text = "プロアス";
            this.KaikeiButton1.UseVisualStyleBackColor = true;
            this.KaikeiButton1.Click += new System.EventHandler(this.KaikeiButton1_Click);
            // 
            // DatePicker2
            // 
            this.DatePicker2.CustomFormat = "yyyy/MM/dd (ddd)";
            this.DatePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatePicker2.Location = new System.Drawing.Point(315, 42);
            this.DatePicker2.Name = "DatePicker2";
            this.DatePicker2.Size = new System.Drawing.Size(130, 19);
            this.DatePicker2.TabIndex = 13;
            // 
            // SelectReceYetButton1
            // 
            this.SelectReceYetButton1.Location = new System.Drawing.Point(77, 99);
            this.SelectReceYetButton1.Name = "SelectReceYetButton1";
            this.SelectReceYetButton1.Size = new System.Drawing.Size(120, 25);
            this.SelectReceYetButton1.TabIndex = 12;
            this.SelectReceYetButton1.Text = "施行済・未会計選択";
            this.SelectReceYetButton1.UseVisualStyleBackColor = true;
            this.SelectReceYetButton1.Click += new System.EventHandler(this.SelectReceYetButton1_Click);
            // 
            // SelectNoneButton1
            // 
            this.SelectNoneButton1.Location = new System.Drawing.Point(202, 99);
            this.SelectNoneButton1.Name = "SelectNoneButton1";
            this.SelectNoneButton1.Size = new System.Drawing.Size(65, 25);
            this.SelectNoneButton1.TabIndex = 11;
            this.SelectNoneButton1.Text = "全非選択";
            this.SelectNoneButton1.UseVisualStyleBackColor = true;
            this.SelectNoneButton1.Click += new System.EventHandler(this.SelectNoneButton1_Click);
            // 
            // SelectAllButton1
            // 
            this.SelectAllButton1.Location = new System.Drawing.Point(12, 99);
            this.SelectAllButton1.Name = "SelectAllButton1";
            this.SelectAllButton1.Size = new System.Drawing.Size(60, 25);
            this.SelectAllButton1.TabIndex = 10;
            this.SelectAllButton1.Text = "全選択";
            this.SelectAllButton1.UseVisualStyleBackColor = true;
            this.SelectAllButton1.Click += new System.EventHandler(this.SelectAllButton1_Click);
            // 
            // HistoryPanel1
            // 
            this.HistoryPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HistoryPanel1.AutoScroll = true;
            this.HistoryPanel1.BackColor = System.Drawing.Color.White;
            this.HistoryPanel1.Location = new System.Drawing.Point(12, 130);
            this.HistoryPanel1.Name = "HistoryPanel1";
            this.HistoryPanel1.Size = new System.Drawing.Size(520, 540);
            this.HistoryPanel1.TabIndex = 9;
            // 
            // ShowButton1
            // 
            this.ShowButton1.Location = new System.Drawing.Point(450, 38);
            this.ShowButton1.Name = "ShowButton1";
            this.ShowButton1.Size = new System.Drawing.Size(60, 25);
            this.ShowButton1.TabIndex = 8;
            this.ShowButton1.Text = "表示";
            this.ShowButton1.UseVisualStyleBackColor = true;
            this.ShowButton1.Click += new System.EventHandler(this.ShowButton1_Click);
            // 
            // DateLabel2
            // 
            this.DateLabel2.AutoSize = true;
            this.DateLabel2.Location = new System.Drawing.Point(292, 46);
            this.DateLabel2.Name = "DateLabel2";
            this.DateLabel2.Size = new System.Drawing.Size(17, 12);
            this.DateLabel2.TabIndex = 6;
            this.DateLabel2.Text = "～";
            // 
            // DatePicker1
            // 
            this.DatePicker1.CustomFormat = "yyyy/MM/dd (ddd)";
            this.DatePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatePicker1.Location = new System.Drawing.Point(155, 42);
            this.DatePicker1.Name = "DatePicker1";
            this.DatePicker1.Size = new System.Drawing.Size(130, 19);
            this.DatePicker1.TabIndex = 5;
            // 
            // stdControlPat11
            // 
            this.stdControlPat11.Location = new System.Drawing.Point(10, 5);
            this.stdControlPat11.Mode1 = MedicalLibrary.Boundary.StdControlPat1.Mode.Normal;
            this.stdControlPat11.Name = "stdControlPat11";
            this.stdControlPat11.ReadOnly = false;
            this.stdControlPat11.Size = new System.Drawing.Size(500, 30);
            this.stdControlPat11.TabIndex = 0;
            // 
            // InsLabel1
            // 
            this.InsLabel1.AutoSize = true;
            this.InsLabel1.Location = new System.Drawing.Point(250, 74);
            this.InsLabel1.Name = "InsLabel1";
            this.InsLabel1.Size = new System.Drawing.Size(29, 12);
            this.InsLabel1.TabIndex = 24;
            this.InsLabel1.Text = "保険";
            // 
            // InsBox1
            // 
            this.InsBox1.BackColor = System.Drawing.Color.White;
            this.InsBox1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.InsBox1.Location = new System.Drawing.Point(280, 70);
            this.InsBox1.MaxLength = 2;
            this.InsBox1.Name = "InsBox1";
            this.InsBox1.Size = new System.Drawing.Size(30, 19);
            this.InsBox1.TabIndex = 25;
            this.InsBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.InsBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InsBox1_KeyDown);
            // 
            // InsNameLabel1
            // 
            this.InsNameLabel1.BackColor = System.Drawing.Color.LightYellow;
            this.InsNameLabel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.InsNameLabel1.Location = new System.Drawing.Point(315, 70);
            this.InsNameLabel1.Name = "InsNameLabel1";
            this.InsNameLabel1.Size = new System.Drawing.Size(50, 19);
            this.InsNameLabel1.TabIndex = 26;
            this.InsNameLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusLabel1
            // 
            this.StatusLabel1.BackColor = System.Drawing.Color.White;
            this.StatusLabel1.Location = new System.Drawing.Point(350, 102);
            this.StatusLabel1.Name = "StatusLabel1";
            this.StatusLabel1.Size = new System.Drawing.Size(50, 20);
            this.StatusLabel1.TabIndex = 27;
            this.StatusLabel1.Text = "未施行";
            this.StatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StatusLabel2
            // 
            this.StatusLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(160)))));
            this.StatusLabel2.Location = new System.Drawing.Point(405, 102);
            this.StatusLabel2.Name = "StatusLabel2";
            this.StatusLabel2.Size = new System.Drawing.Size(50, 20);
            this.StatusLabel2.TabIndex = 28;
            this.StatusLabel2.Text = "施行済";
            this.StatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StatusLabel3
            // 
            this.StatusLabel3.BackColor = System.Drawing.Color.PeachPuff;
            this.StatusLabel3.Location = new System.Drawing.Point(460, 102);
            this.StatusLabel3.Name = "StatusLabel3";
            this.StatusLabel3.Size = new System.Drawing.Size(50, 20);
            this.StatusLabel3.TabIndex = 29;
            this.StatusLabel3.Text = "会計済";
            this.StatusLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OrderKaikeiMenuStrip1
            // 
            this.OrderKaikeiMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.KaikeiFlgMenuItem});
            this.OrderKaikeiMenuStrip1.Name = "OrderKaikeiMenuStrip1";
            this.OrderKaikeiMenuStrip1.Size = new System.Drawing.Size(153, 48);
            // 
            // KaikeiFlgMenuItem
            // 
            this.KaikeiFlgMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.KaikeiFlg1MenuItem,
            this.KaikeiFlg0MenuItem});
            this.KaikeiFlgMenuItem.Name = "KaikeiFlgMenuItem";
            this.KaikeiFlgMenuItem.Size = new System.Drawing.Size(152, 22);
            this.KaikeiFlgMenuItem.Text = "会計フラグ";
            // 
            // KaikeiFlg1MenuItem
            // 
            this.KaikeiFlg1MenuItem.Name = "KaikeiFlg1MenuItem";
            this.KaikeiFlg1MenuItem.Size = new System.Drawing.Size(152, 22);
            this.KaikeiFlg1MenuItem.Text = "取込済";
            this.KaikeiFlg1MenuItem.Click += new System.EventHandler(this.KaikeiFlg1MenuItem_Click);
            // 
            // KaikeiFlg0MenuItem
            // 
            this.KaikeiFlg0MenuItem.Name = "KaikeiFlg0MenuItem";
            this.KaikeiFlg0MenuItem.Size = new System.Drawing.Size(152, 22);
            this.KaikeiFlg0MenuItem.Text = "未取込";
            this.KaikeiFlg0MenuItem.Click += new System.EventHandler(this.KaikeiFlg0MenuItem_Click);
            // 
            // FormOrderKaikei
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 682);
            this.ContextMenuStrip = this.OrderKaikeiMenuStrip1;
            this.Controls.Add(this.StatusLabel3);
            this.Controls.Add(this.StatusLabel2);
            this.Controls.Add(this.StatusLabel1);
            this.Controls.Add(this.InsNameLabel1);
            this.Controls.Add(this.InsBox1);
            this.Controls.Add(this.InsLabel1);
            this.Controls.Add(this.stdControlFont11);
            this.Controls.Add(this.ctrlDoctorBox11);
            this.Controls.Add(this.ctrlDeptBox11);
            this.Controls.Add(this.SEQBox1);
            this.Controls.Add(this.SEQLabel1);
            this.Controls.Add(this.InOutPanel);
            this.Controls.Add(this.KaikeiButton1);
            this.Controls.Add(this.DatePicker2);
            this.Controls.Add(this.SelectReceYetButton1);
            this.Controls.Add(this.SelectNoneButton1);
            this.Controls.Add(this.SelectAllButton1);
            this.Controls.Add(this.HistoryPanel1);
            this.Controls.Add(this.ShowButton1);
            this.Controls.Add(this.DateLabel2);
            this.Controls.Add(this.DatePicker1);
            this.Controls.Add(this.stdControlPat11);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormOrderKaikei";
            this.Text = "オーダ取込み";
            this.Load += new System.EventHandler(this.FormOrderKaikei_Load);
            this.InOutPanel.ResumeLayout(false);
            this.InOutPanel.PerformLayout();
            this.OrderKaikeiMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MedicalLibrary.Boundary.StdControlPat1 stdControlPat11;
        private System.Windows.Forms.DateTimePicker DatePicker1;
        private System.Windows.Forms.Button ShowButton1;
        private System.Windows.Forms.Panel HistoryPanel1;
        private System.Windows.Forms.Button SelectAllButton1;
        private System.Windows.Forms.Button SelectNoneButton1;
        private System.Windows.Forms.Button SelectReceYetButton1;
        private System.Windows.Forms.Label DateLabel2;
        private System.Windows.Forms.DateTimePicker DatePicker2;
        private System.Windows.Forms.Button KaikeiButton1;
        private System.Windows.Forms.RadioButton InOutButton1;
        private System.Windows.Forms.Panel InOutPanel;
        private System.Windows.Forms.RadioButton InOutButton2;
        private System.Windows.Forms.Label SEQLabel1;
        private System.Windows.Forms.ComboBox SEQBox1;
        private MedicalLibrary.Boundary.CtrlDeptBox1 ctrlDeptBox11;
        private MedicalLibrary.Boundary.CtrlDoctorBox1 ctrlDoctorBox11;
        private MedicalLibrary.Boundary.StdControlFont1 stdControlFont11;
        private System.Windows.Forms.Label InsLabel1;
        private System.Windows.Forms.TextBox InsBox1;
        private System.Windows.Forms.Label InsNameLabel1;
        private System.Windows.Forms.Label StatusLabel1;
        private System.Windows.Forms.Label StatusLabel2;
        private System.Windows.Forms.Label StatusLabel3;
        private System.Windows.Forms.ContextMenuStrip OrderKaikeiMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem KaikeiFlgMenuItem;
        private System.Windows.Forms.ToolStripMenuItem KaikeiFlg1MenuItem;
        private System.Windows.Forms.ToolStripMenuItem KaikeiFlg0MenuItem;
    }
}