namespace MedicalLibrary.Agent
{
    partial class FormOpeNursingList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOpeNursingList));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileCarteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolEyeCenterMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolOpeOrderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolAnesSupportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statView1 = new System.Windows.Forms.DataGridView();
            this.statContextMenu1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.carteContextMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.yearBox = new System.Windows.Forms.ComboBox();
            this.monthBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.showButton = new System.Windows.Forms.Button();
            this.statView2 = new System.Windows.Forms.DataGridView();
            this.deptBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.countBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.statView3 = new System.Windows.Forms.DataGridView();
            this.statContextMenu3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.carteContextMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.LabelPrintContextMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.countBox3 = new System.Windows.Forms.TextBox();
            this.startDate = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.endDate = new System.Windows.Forms.DateTimePicker();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.fileSaveButton = new System.Windows.Forms.Button();
            this.showButton3 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.anesBox = new System.Windows.Forms.ComboBox();
            this.LabelPrintButton = new System.Windows.Forms.Button();
            this.honkanBox = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.recKindBox = new System.Windows.Forms.ComboBox();
            this.LoginUsrLabel = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statView1)).BeginInit();
            this.statContextMenu1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statView3)).BeginInit();
            this.statContextMenu3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.ToolMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1016, 26);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileCarteMenuItem,
            this.FileExitMenuItem});
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(68, 22);
            this.FileMenuItem.Text = "ファイル";
            // 
            // FileCarteMenuItem
            // 
            this.FileCarteMenuItem.Name = "FileCarteMenuItem";
            this.FileCarteMenuItem.Size = new System.Drawing.Size(152, 22);
            this.FileCarteMenuItem.Text = "カルテ";
            this.FileCarteMenuItem.Click += new System.EventHandler(this.FileCarteMenuItem_Click);
            // 
            // FileExitMenuItem
            // 
            this.FileExitMenuItem.Name = "FileExitMenuItem";
            this.FileExitMenuItem.Size = new System.Drawing.Size(152, 22);
            this.FileExitMenuItem.Text = "終了";
            this.FileExitMenuItem.Click += new System.EventHandler(this.FileExitMenuItem_Click);
            // 
            // ToolMenuItem
            // 
            this.ToolMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolEyeCenterMenuItem,
            this.ToolOpeOrderMenuItem,
            this.ToolAnesSupportMenuItem});
            this.ToolMenuItem.Name = "ToolMenuItem";
            this.ToolMenuItem.Size = new System.Drawing.Size(56, 22);
            this.ToolMenuItem.Text = "ツール";
            // 
            // ToolEyeCenterMenuItem
            // 
            this.ToolEyeCenterMenuItem.Name = "ToolEyeCenterMenuItem";
            this.ToolEyeCenterMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ToolEyeCenterMenuItem.Text = "眼科システム";
            this.ToolEyeCenterMenuItem.Click += new System.EventHandler(this.ToolEyeCenterMenuItem_Click);
            // 
            // ToolOpeOrderMenuItem
            // 
            this.ToolOpeOrderMenuItem.Name = "ToolOpeOrderMenuItem";
            this.ToolOpeOrderMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ToolOpeOrderMenuItem.Text = "手術指示一覧";
            this.ToolOpeOrderMenuItem.Click += new System.EventHandler(this.ToolOpeOrderMenuItem_Click);
            // 
            // ToolAnesSupportMenuItem
            // 
            this.ToolAnesSupportMenuItem.Name = "ToolAnesSupportMenuItem";
            this.ToolAnesSupportMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ToolAnesSupportMenuItem.Text = "麻酔記録";
            this.ToolAnesSupportMenuItem.Click += new System.EventHandler(this.ToolAnesSupportMenuItem_Click);
            // 
            // statView1
            // 
            this.statView1.AllowUserToAddRows = false;
            this.statView1.AllowUserToDeleteRows = false;
            this.statView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.statView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.statView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.statView1.ContextMenuStrip = this.statContextMenu1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(218)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.statView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.statView1.Location = new System.Drawing.Point(258, 55);
            this.statView1.MultiSelect = false;
            this.statView1.Name = "statView1";
            this.statView1.ReadOnly = true;
            this.statView1.RowHeadersVisible = false;
            this.statView1.RowTemplate.Height = 21;
            this.statView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.statView1.Size = new System.Drawing.Size(746, 674);
            this.statView1.TabIndex = 6;
            this.statView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.statView1_CellDoubleClick);
            // 
            // statContextMenu1
            // 
            this.statContextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.carteContextMenuItem1});
            this.statContextMenu1.Name = "statContextMenu1";
            this.statContextMenu1.Size = new System.Drawing.Size(113, 26);
            // 
            // carteContextMenuItem1
            // 
            this.carteContextMenuItem1.Name = "carteContextMenuItem1";
            this.carteContextMenuItem1.Size = new System.Drawing.Size(112, 22);
            this.carteContextMenuItem1.Text = "カルテ";
            this.carteContextMenuItem1.Click += new System.EventHandler(this.carteContextMenuItem1_Click);
            // 
            // yearBox
            // 
            this.yearBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.yearBox.FormattingEnabled = true;
            this.yearBox.Location = new System.Drawing.Point(8, 30);
            this.yearBox.Name = "yearBox";
            this.yearBox.Size = new System.Drawing.Size(60, 20);
            this.yearBox.TabIndex = 7;
            // 
            // monthBox
            // 
            this.monthBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.monthBox.FormattingEnabled = true;
            this.monthBox.Location = new System.Drawing.Point(91, 30);
            this.monthBox.Name = "monthBox";
            this.monthBox.Size = new System.Drawing.Size(43, 20);
            this.monthBox.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "年";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "月";
            // 
            // showButton
            // 
            this.showButton.Location = new System.Drawing.Point(167, 29);
            this.showButton.Name = "showButton";
            this.showButton.Size = new System.Drawing.Size(60, 23);
            this.showButton.TabIndex = 11;
            this.showButton.Text = "表示";
            this.showButton.UseVisualStyleBackColor = true;
            this.showButton.Click += new System.EventHandler(this.showButton_Click);
            // 
            // statView2
            // 
            this.statView2.AllowUserToAddRows = false;
            this.statView2.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.statView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.statView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(218)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.statView2.DefaultCellStyle = dataGridViewCellStyle4;
            this.statView2.Location = new System.Drawing.Point(8, 55);
            this.statView2.Name = "statView2";
            this.statView2.RowHeadersVisible = false;
            this.statView2.RowTemplate.Height = 21;
            this.statView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.statView2.Size = new System.Drawing.Size(244, 214);
            this.statView2.TabIndex = 12;
            // 
            // deptBox
            // 
            this.deptBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.deptBox.FormattingEnabled = true;
            this.deptBox.Location = new System.Drawing.Point(305, 30);
            this.deptBox.Name = "deptBox";
            this.deptBox.Size = new System.Drawing.Size(89, 20);
            this.deptBox.TabIndex = 13;
            this.deptBox.SelectedIndexChanged += new System.EventHandler(this.deptBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(261, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "診療科";
            // 
            // countBox1
            // 
            this.countBox1.BackColor = System.Drawing.Color.LightYellow;
            this.countBox1.Location = new System.Drawing.Point(830, 31);
            this.countBox1.Name = "countBox1";
            this.countBox1.ReadOnly = true;
            this.countBox1.Size = new System.Drawing.Size(50, 19);
            this.countBox1.TabIndex = 16;
            this.countBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(770, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "手術件数";
            // 
            // statView3
            // 
            this.statView3.AllowUserToAddRows = false;
            this.statView3.AllowUserToDeleteRows = false;
            this.statView3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.statView3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.statView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.statView3.ContextMenuStrip = this.statContextMenu3;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(218)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.statView3.DefaultCellStyle = dataGridViewCellStyle6;
            this.statView3.Location = new System.Drawing.Point(8, 323);
            this.statView3.MultiSelect = false;
            this.statView3.Name = "statView3";
            this.statView3.RowHeadersVisible = false;
            this.statView3.RowTemplate.Height = 21;
            this.statView3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.statView3.Size = new System.Drawing.Size(244, 406);
            this.statView3.TabIndex = 18;
            // 
            // statContextMenu3
            // 
            this.statContextMenu3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.carteContextMenuItem3,
            this.LabelPrintContextMenuItem3});
            this.statContextMenu3.Name = "statContextMenu1";
            this.statContextMenu3.Size = new System.Drawing.Size(137, 48);
            // 
            // carteContextMenuItem3
            // 
            this.carteContextMenuItem3.Name = "carteContextMenuItem3";
            this.carteContextMenuItem3.Size = new System.Drawing.Size(136, 22);
            this.carteContextMenuItem3.Text = "カルテ";
            this.carteContextMenuItem3.Click += new System.EventHandler(this.carteContextMenuItem3_Click);
            // 
            // LabelPrintContextMenuItem3
            // 
            this.LabelPrintContextMenuItem3.Name = "LabelPrintContextMenuItem3";
            this.LabelPrintContextMenuItem3.Size = new System.Drawing.Size(136, 22);
            this.LabelPrintContextMenuItem3.Text = "ラベル発行";
            this.LabelPrintContextMenuItem3.Click += new System.EventHandler(this.LabelPrintContextMenuItem3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 279);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "手術申込患者";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(885, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 20;
            this.label6.Text = "件";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(143, 279);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "名";
            // 
            // countBox3
            // 
            this.countBox3.BackColor = System.Drawing.Color.White;
            this.countBox3.Location = new System.Drawing.Point(94, 275);
            this.countBox3.Name = "countBox3";
            this.countBox3.ReadOnly = true;
            this.countBox3.Size = new System.Drawing.Size(45, 19);
            this.countBox3.TabIndex = 21;
            this.countBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // startDate
            // 
            this.startDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.startDate.Location = new System.Drawing.Point(10, 299);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(86, 19);
            this.startDate.TabIndex = 23;
            this.startDate.ValueChanged += new System.EventHandler(this.startDate_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(97, 302);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 24;
            this.label8.Text = "～";
            // 
            // endDate
            // 
            this.endDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.endDate.Location = new System.Drawing.Point(115, 299);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(85, 19);
            this.endDate.TabIndex = 25;
            this.endDate.ValueChanged += new System.EventHandler(this.endDate_ValueChanged);
            // 
            // fileSaveButton
            // 
            this.fileSaveButton.Location = new System.Drawing.Point(915, 29);
            this.fileSaveButton.Name = "fileSaveButton";
            this.fileSaveButton.Size = new System.Drawing.Size(90, 23);
            this.fileSaveButton.TabIndex = 26;
            this.fileSaveButton.Text = "ファイル保存";
            this.fileSaveButton.UseVisualStyleBackColor = true;
            this.fileSaveButton.Click += new System.EventHandler(this.fileSaveButton_Click);
            // 
            // showButton3
            // 
            this.showButton3.Location = new System.Drawing.Point(206, 298);
            this.showButton3.Name = "showButton3";
            this.showButton3.Size = new System.Drawing.Size(46, 21);
            this.showButton3.TabIndex = 27;
            this.showButton3.Text = "表示";
            this.showButton3.UseVisualStyleBackColor = true;
            this.showButton3.Click += new System.EventHandler(this.showButton3_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.ReshowDelay = 100;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(625, 34);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 29;
            this.label9.Text = "麻酔";
            // 
            // anesBox
            // 
            this.anesBox.FormattingEnabled = true;
            this.anesBox.Location = new System.Drawing.Point(660, 30);
            this.anesBox.Name = "anesBox";
            this.anesBox.Size = new System.Drawing.Size(89, 20);
            this.anesBox.TabIndex = 28;
            this.anesBox.TextChanged += new System.EventHandler(this.anesBox_TextChanged);
            // 
            // LabelPrintButton
            // 
            this.LabelPrintButton.Location = new System.Drawing.Point(177, 274);
            this.LabelPrintButton.Name = "LabelPrintButton";
            this.LabelPrintButton.Size = new System.Drawing.Size(75, 21);
            this.LabelPrintButton.TabIndex = 30;
            this.LabelPrintButton.Text = "ラベル一括";
            this.LabelPrintButton.UseVisualStyleBackColor = true;
            this.LabelPrintButton.Click += new System.EventHandler(this.LabelPrintButton_Click);
            // 
            // honkanBox
            // 
            this.honkanBox.AutoSize = true;
            this.honkanBox.Location = new System.Drawing.Point(530, 33);
            this.honkanBox.Name = "honkanBox";
            this.honkanBox.Size = new System.Drawing.Size(75, 16);
            this.honkanBox.TabIndex = 31;
            this.honkanBox.Text = "眼科を除く";
            this.honkanBox.UseVisualStyleBackColor = true;
            this.honkanBox.CheckedChanged += new System.EventHandler(this.honkanBox_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(405, 34);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 34;
            this.label10.Text = "種別";
            // 
            // recKindBox
            // 
            this.recKindBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.recKindBox.FormattingEnabled = true;
            this.recKindBox.Location = new System.Drawing.Point(440, 30);
            this.recKindBox.Name = "recKindBox";
            this.recKindBox.Size = new System.Drawing.Size(70, 20);
            this.recKindBox.TabIndex = 33;
            this.recKindBox.SelectedIndexChanged += new System.EventHandler(this.recKindBox_SelectedIndexChanged);
            // 
            // LoginUsrLabel
            // 
            this.LoginUsrLabel.BackColor = System.Drawing.Color.LightYellow;
            this.LoginUsrLabel.Location = new System.Drawing.Point(900, 4);
            this.LoginUsrLabel.MaxLength = 30;
            this.LoginUsrLabel.Name = "LoginUsrLabel";
            this.LoginUsrLabel.ReadOnly = true;
            this.LoginUsrLabel.Size = new System.Drawing.Size(100, 19);
            this.LoginUsrLabel.TabIndex = 35;
            this.LoginUsrLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.LoginUsrLabel.DoubleClick += new System.EventHandler(this.LoginUsrLabel_DoubleClick);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(650, 8);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(215, 12);
            this.label28.TabIndex = 164;
            this.label28.Text = "※ F8 キーでログインユーザーを変更できます";
            // 
            // FormList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.LoginUsrLabel);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.recKindBox);
            this.Controls.Add(this.honkanBox);
            this.Controls.Add(this.LabelPrintButton);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.anesBox);
            this.Controls.Add(this.fileSaveButton);
            this.Controls.Add(this.endDate);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.startDate);
            this.Controls.Add(this.showButton3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.countBox3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.statView3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.countBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.deptBox);
            this.Controls.Add(this.statView2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.showButton);
            this.Controls.Add(this.monthBox);
            this.Controls.Add(this.yearBox);
            this.Controls.Add(this.statView1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormList";
            this.Text = "手術患者一覧";
            this.Load += new System.EventHandler(this.FormOpeNursingList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormOpeNursingList_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statView1)).EndInit();
            this.statContextMenu1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.statView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statView3)).EndInit();
            this.statContextMenu3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileExitMenuItem;
        private System.Windows.Forms.DataGridView statView1;
        private System.Windows.Forms.ToolStripMenuItem FileCarteMenuItem;
        private System.Windows.Forms.ComboBox yearBox;
        private System.Windows.Forms.ComboBox monthBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button showButton;
        private System.Windows.Forms.DataGridView statView2;
        private System.Windows.Forms.ComboBox deptBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox countBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView statView3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox countBox3;
        private System.Windows.Forms.DateTimePicker startDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker endDate;
        private System.Windows.Forms.ContextMenuStrip statContextMenu1;
        private System.Windows.Forms.ToolStripMenuItem carteContextMenuItem1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button fileSaveButton;
        private System.Windows.Forms.Button showButton3;
        private System.Windows.Forms.ContextMenuStrip statContextMenu3;
        private System.Windows.Forms.ToolStripMenuItem carteContextMenuItem3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox anesBox;
        private System.Windows.Forms.ToolStripMenuItem ToolMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolEyeCenterMenuItem;
        private System.Windows.Forms.Button LabelPrintButton;
        private System.Windows.Forms.ToolStripMenuItem LabelPrintContextMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem ToolOpeOrderMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolAnesSupportMenuItem;
        private System.Windows.Forms.CheckBox honkanBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox recKindBox;
        private System.Windows.Forms.TextBox LoginUsrLabel;
        private System.Windows.Forms.Label label28;
    }
}

