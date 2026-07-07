namespace MedicalLibrary.Boundary
{
    partial class FormPatBoard
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
            this.NursingOrderButton1 = new System.Windows.Forms.Button();
            this.OpeOrderButton1 = new System.Windows.Forms.Button();
            this.DoneBox1 = new System.Windows.Forms.CheckBox();
            this.BaseOrderButton1 = new System.Windows.Forms.Button();
            this.InactiveBox1 = new System.Windows.Forms.CheckBox();
            this.ReplyStaffCodeLabel2 = new System.Windows.Forms.Label();
            this.OrderStaffCodeLabel2 = new System.Windows.Forms.Label();
            this.ReplyStaffCodeLabel1 = new System.Windows.Forms.Label();
            this.OrderStaffCodeLabel1 = new System.Windows.Forms.Label();
            this.CountLabel1 = new System.Windows.Forms.Label();
            this.ReplyStaffNameLabel1 = new System.Windows.Forms.Label();
            this.ReplyDateTimeLabel1 = new System.Windows.Forms.Label();
            this.OrderStaffNameLabel1 = new System.Windows.Forms.Label();
            this.OrderDateTimeLabel1 = new System.Windows.Forms.Label();
            this.ReplyDeleteButton1 = new System.Windows.Forms.Button();
            this.ReplySaveButton1 = new System.Windows.Forms.Button();
            this.OrderDeleteButton1 = new System.Windows.Forms.Button();
            this.OrderSaveButton1 = new System.Windows.Forms.Button();
            this.StatusBox1 = new System.Windows.Forms.CheckBox();
            this.ReplyNewButton1 = new System.Windows.Forms.Button();
            this.OrderNewButton1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ReplyBox1 = new System.Windows.Forms.TextBox();
            this.SEQLabel2 = new System.Windows.Forms.Label();
            this.SEQLabel1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.KindBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OrderBox1 = new System.Windows.Forms.TextBox();
            this.ListView1 = new System.Windows.Forms.DataGridView();
            this.stdControlFont11 = new MedicalLibrary.Boundary.StdControlFont1();
            this.stdControlPat11 = new MedicalLibrary.Boundary.StdControlPat1();
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // NursingOrderButton1
            // 
            this.NursingOrderButton1.Location = new System.Drawing.Point(660, 535);
            this.NursingOrderButton1.Name = "NursingOrderButton1";
            this.NursingOrderButton1.Size = new System.Drawing.Size(75, 23);
            this.NursingOrderButton1.TabIndex = 55;
            this.NursingOrderButton1.Text = "看護指示";
            this.NursingOrderButton1.UseVisualStyleBackColor = true;
            this.NursingOrderButton1.Click += new System.EventHandler(this.NursingOrderButton1_Click);
            // 
            // OpeOrderButton1
            // 
            this.OpeOrderButton1.Location = new System.Drawing.Point(580, 535);
            this.OpeOrderButton1.Name = "OpeOrderButton1";
            this.OpeOrderButton1.Size = new System.Drawing.Size(75, 23);
            this.OpeOrderButton1.TabIndex = 54;
            this.OpeOrderButton1.Text = "手術指示";
            this.OpeOrderButton1.UseVisualStyleBackColor = true;
            this.OpeOrderButton1.Click += new System.EventHandler(this.OpeOrderButton1_Click);
            // 
            // DoneBox1
            // 
            this.DoneBox1.AutoSize = true;
            this.DoneBox1.Location = new System.Drawing.Point(640, 15);
            this.DoneBox1.Name = "DoneBox1";
            this.DoneBox1.Size = new System.Drawing.Size(72, 16);
            this.DoneBox1.TabIndex = 51;
            this.DoneBox1.Text = "完了表示";
            this.DoneBox1.UseVisualStyleBackColor = true;
            this.DoneBox1.CheckedChanged += new System.EventHandler(this.DoneBox1_CheckedChanged);
            // 
            // BaseOrderButton1
            // 
            this.BaseOrderButton1.Location = new System.Drawing.Point(500, 535);
            this.BaseOrderButton1.Name = "BaseOrderButton1";
            this.BaseOrderButton1.Size = new System.Drawing.Size(75, 23);
            this.BaseOrderButton1.TabIndex = 50;
            this.BaseOrderButton1.Text = "基本指示";
            this.BaseOrderButton1.UseVisualStyleBackColor = true;
            this.BaseOrderButton1.Click += new System.EventHandler(this.BaseOrderButton1_Click);
            // 
            // InactiveBox1
            // 
            this.InactiveBox1.AutoSize = true;
            this.InactiveBox1.Location = new System.Drawing.Point(740, 15);
            this.InactiveBox1.Name = "InactiveBox1";
            this.InactiveBox1.Size = new System.Drawing.Size(72, 16);
            this.InactiveBox1.TabIndex = 49;
            this.InactiveBox1.Text = "削除表示";
            this.InactiveBox1.UseVisualStyleBackColor = true;
            this.InactiveBox1.CheckedChanged += new System.EventHandler(this.InactiveBox1_CheckedChanged);
            // 
            // ReplyStaffCodeLabel2
            // 
            this.ReplyStaffCodeLabel2.BackColor = System.Drawing.Color.White;
            this.ReplyStaffCodeLabel2.Location = new System.Drawing.Point(420, 422);
            this.ReplyStaffCodeLabel2.Name = "ReplyStaffCodeLabel2";
            this.ReplyStaffCodeLabel2.Size = new System.Drawing.Size(40, 18);
            this.ReplyStaffCodeLabel2.TabIndex = 48;
            this.ReplyStaffCodeLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OrderStaffCodeLabel2
            // 
            this.OrderStaffCodeLabel2.BackColor = System.Drawing.Color.White;
            this.OrderStaffCodeLabel2.Location = new System.Drawing.Point(15, 512);
            this.OrderStaffCodeLabel2.Name = "OrderStaffCodeLabel2";
            this.OrderStaffCodeLabel2.Size = new System.Drawing.Size(40, 18);
            this.OrderStaffCodeLabel2.TabIndex = 47;
            this.OrderStaffCodeLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ReplyStaffCodeLabel1
            // 
            this.ReplyStaffCodeLabel1.BackColor = System.Drawing.Color.White;
            this.ReplyStaffCodeLabel1.Location = new System.Drawing.Point(420, 447);
            this.ReplyStaffCodeLabel1.Name = "ReplyStaffCodeLabel1";
            this.ReplyStaffCodeLabel1.Size = new System.Drawing.Size(40, 18);
            this.ReplyStaffCodeLabel1.TabIndex = 46;
            this.ReplyStaffCodeLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OrderStaffCodeLabel1
            // 
            this.OrderStaffCodeLabel1.BackColor = System.Drawing.Color.White;
            this.OrderStaffCodeLabel1.Location = new System.Drawing.Point(15, 537);
            this.OrderStaffCodeLabel1.Name = "OrderStaffCodeLabel1";
            this.OrderStaffCodeLabel1.Size = new System.Drawing.Size(40, 18);
            this.OrderStaffCodeLabel1.TabIndex = 45;
            this.OrderStaffCodeLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CountLabel1
            // 
            this.CountLabel1.BackColor = System.Drawing.Color.White;
            this.CountLabel1.Location = new System.Drawing.Point(300, 330);
            this.CountLabel1.Name = "CountLabel1";
            this.CountLabel1.Size = new System.Drawing.Size(50, 18);
            this.CountLabel1.TabIndex = 44;
            this.CountLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ReplyStaffNameLabel1
            // 
            this.ReplyStaffNameLabel1.BackColor = System.Drawing.Color.White;
            this.ReplyStaffNameLabel1.Location = new System.Drawing.Point(585, 447);
            this.ReplyStaffNameLabel1.Name = "ReplyStaffNameLabel1";
            this.ReplyStaffNameLabel1.Size = new System.Drawing.Size(100, 18);
            this.ReplyStaffNameLabel1.TabIndex = 43;
            this.ReplyStaffNameLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ReplyDateTimeLabel1
            // 
            this.ReplyDateTimeLabel1.BackColor = System.Drawing.Color.White;
            this.ReplyDateTimeLabel1.Location = new System.Drawing.Point(465, 447);
            this.ReplyDateTimeLabel1.Name = "ReplyDateTimeLabel1";
            this.ReplyDateTimeLabel1.Size = new System.Drawing.Size(110, 18);
            this.ReplyDateTimeLabel1.TabIndex = 42;
            this.ReplyDateTimeLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OrderStaffNameLabel1
            // 
            this.OrderStaffNameLabel1.BackColor = System.Drawing.Color.White;
            this.OrderStaffNameLabel1.Location = new System.Drawing.Point(180, 537);
            this.OrderStaffNameLabel1.Name = "OrderStaffNameLabel1";
            this.OrderStaffNameLabel1.Size = new System.Drawing.Size(100, 18);
            this.OrderStaffNameLabel1.TabIndex = 41;
            this.OrderStaffNameLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OrderDateTimeLabel1
            // 
            this.OrderDateTimeLabel1.BackColor = System.Drawing.Color.White;
            this.OrderDateTimeLabel1.Location = new System.Drawing.Point(60, 537);
            this.OrderDateTimeLabel1.Name = "OrderDateTimeLabel1";
            this.OrderDateTimeLabel1.Size = new System.Drawing.Size(110, 18);
            this.OrderDateTimeLabel1.TabIndex = 40;
            this.OrderDateTimeLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ReplyDeleteButton1
            // 
            this.ReplyDeleteButton1.Location = new System.Drawing.Point(765, 445);
            this.ReplyDeleteButton1.Name = "ReplyDeleteButton1";
            this.ReplyDeleteButton1.Size = new System.Drawing.Size(40, 23);
            this.ReplyDeleteButton1.TabIndex = 39;
            this.ReplyDeleteButton1.Text = "削除";
            this.ReplyDeleteButton1.UseVisualStyleBackColor = true;
            this.ReplyDeleteButton1.Click += new System.EventHandler(this.ReplyDeleteButton1_Click);
            // 
            // ReplySaveButton1
            // 
            this.ReplySaveButton1.Location = new System.Drawing.Point(695, 445);
            this.ReplySaveButton1.Name = "ReplySaveButton1";
            this.ReplySaveButton1.Size = new System.Drawing.Size(65, 23);
            this.ReplySaveButton1.TabIndex = 38;
            this.ReplySaveButton1.Text = "登録";
            this.ReplySaveButton1.UseVisualStyleBackColor = true;
            this.ReplySaveButton1.Click += new System.EventHandler(this.ReplySaveButton1_Click);
            // 
            // OrderDeleteButton1
            // 
            this.OrderDeleteButton1.Location = new System.Drawing.Point(360, 535);
            this.OrderDeleteButton1.Name = "OrderDeleteButton1";
            this.OrderDeleteButton1.Size = new System.Drawing.Size(40, 23);
            this.OrderDeleteButton1.TabIndex = 37;
            this.OrderDeleteButton1.Text = "削除";
            this.OrderDeleteButton1.UseVisualStyleBackColor = true;
            this.OrderDeleteButton1.Click += new System.EventHandler(this.OrderDeleteButton1_Click);
            // 
            // OrderSaveButton1
            // 
            this.OrderSaveButton1.Location = new System.Drawing.Point(290, 535);
            this.OrderSaveButton1.Name = "OrderSaveButton1";
            this.OrderSaveButton1.Size = new System.Drawing.Size(65, 23);
            this.OrderSaveButton1.TabIndex = 36;
            this.OrderSaveButton1.Text = "登録";
            this.OrderSaveButton1.UseVisualStyleBackColor = true;
            this.OrderSaveButton1.Click += new System.EventHandler(this.OrderSaveButton1_Click);
            // 
            // StatusBox1
            // 
            this.StatusBox1.AutoSize = true;
            this.StatusBox1.Location = new System.Drawing.Point(355, 332);
            this.StatusBox1.Name = "StatusBox1";
            this.StatusBox1.Size = new System.Drawing.Size(48, 16);
            this.StatusBox1.TabIndex = 35;
            this.StatusBox1.Text = "完了";
            this.StatusBox1.UseVisualStyleBackColor = true;
            this.StatusBox1.CheckedChanged += new System.EventHandler(this.StatusBox1_CheckedChanged);
            // 
            // ReplyNewButton1
            // 
            this.ReplyNewButton1.Location = new System.Drawing.Point(420, 380);
            this.ReplyNewButton1.Name = "ReplyNewButton1";
            this.ReplyNewButton1.Size = new System.Drawing.Size(40, 23);
            this.ReplyNewButton1.TabIndex = 34;
            this.ReplyNewButton1.Text = "新規";
            this.ReplyNewButton1.UseVisualStyleBackColor = true;
            this.ReplyNewButton1.Click += new System.EventHandler(this.ReplyNewButton1_Click);
            // 
            // OrderNewButton1
            // 
            this.OrderNewButton1.Location = new System.Drawing.Point(15, 380);
            this.OrderNewButton1.Name = "OrderNewButton1";
            this.OrderNewButton1.Size = new System.Drawing.Size(40, 23);
            this.OrderNewButton1.TabIndex = 33;
            this.OrderNewButton1.Text = "新規";
            this.OrderNewButton1.UseVisualStyleBackColor = true;
            this.OrderNewButton1.Click += new System.EventHandler(this.OrderNewButton1_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(420, 365);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 32;
            this.label3.Text = "返信";
            // 
            // ReplyBox1
            // 
            this.ReplyBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ReplyBox1.Location = new System.Drawing.Point(465, 360);
            this.ReplyBox1.Multiline = true;
            this.ReplyBox1.Name = "ReplyBox1";
            this.ReplyBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ReplyBox1.Size = new System.Drawing.Size(340, 80);
            this.ReplyBox1.TabIndex = 31;
            // 
            // SEQLabel2
            // 
            this.SEQLabel2.BackColor = System.Drawing.Color.White;
            this.SEQLabel2.Location = new System.Drawing.Point(245, 330);
            this.SEQLabel2.Name = "SEQLabel2";
            this.SEQLabel2.Size = new System.Drawing.Size(50, 18);
            this.SEQLabel2.TabIndex = 30;
            this.SEQLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SEQLabel1
            // 
            this.SEQLabel1.BackColor = System.Drawing.Color.White;
            this.SEQLabel1.Location = new System.Drawing.Point(190, 330);
            this.SEQLabel1.Name = "SEQLabel1";
            this.SEQLabel1.Size = new System.Drawing.Size(50, 18);
            this.SEQLabel1.TabIndex = 29;
            this.SEQLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 365);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 28;
            this.label2.Text = "指示";
            // 
            // KindBox1
            // 
            this.KindBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.KindBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.KindBox1.FormattingEnabled = true;
            this.KindBox1.Location = new System.Drawing.Point(60, 330);
            this.KindBox1.Name = "KindBox1";
            this.KindBox1.Size = new System.Drawing.Size(120, 20);
            this.KindBox1.TabIndex = 27;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 335);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "区分";
            // 
            // OrderBox1
            // 
            this.OrderBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OrderBox1.Location = new System.Drawing.Point(60, 360);
            this.OrderBox1.Multiline = true;
            this.OrderBox1.Name = "OrderBox1";
            this.OrderBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.OrderBox1.Size = new System.Drawing.Size(340, 170);
            this.OrderBox1.TabIndex = 25;
            // 
            // ListView1
            // 
            this.ListView1.AllowUserToAddRows = false;
            this.ListView1.AllowUserToDeleteRows = false;
            this.ListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.ListView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.ListView1.Location = new System.Drawing.Point(12, 39);
            this.ListView1.Name = "ListView1";
            this.ListView1.ReadOnly = true;
            this.ListView1.RowHeadersWidth = 20;
            this.ListView1.RowTemplate.Height = 21;
            this.ListView1.Size = new System.Drawing.Size(800, 280);
            this.ListView1.TabIndex = 24;
            this.ListView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListView1_CellClick);
            // 
            // stdControlFont11
            // 
            this.stdControlFont11.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            this.stdControlFont11.Location = new System.Drawing.Point(570, 5);
            this.stdControlFont11.Name = "stdControlFont11";
            this.stdControlFont11.Size = new System.Drawing.Size(60, 30);
            this.stdControlFont11.TabIndex = 53;
            // 
            // stdControlPat11
            // 
            this.stdControlPat11.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            this.stdControlPat11.Location = new System.Drawing.Point(10, 5);
            this.stdControlPat11.Name = "stdControlPat11";
            this.stdControlPat11.Size = new System.Drawing.Size(500, 30);
            this.stdControlPat11.TabIndex = 52;
            // 
            // FormPatBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 562);
            this.Controls.Add(this.NursingOrderButton1);
            this.Controls.Add(this.OpeOrderButton1);
            this.Controls.Add(this.stdControlFont11);
            this.Controls.Add(this.stdControlPat11);
            this.Controls.Add(this.DoneBox1);
            this.Controls.Add(this.BaseOrderButton1);
            this.Controls.Add(this.InactiveBox1);
            this.Controls.Add(this.ReplyStaffCodeLabel2);
            this.Controls.Add(this.OrderStaffCodeLabel2);
            this.Controls.Add(this.ReplyStaffCodeLabel1);
            this.Controls.Add(this.OrderStaffCodeLabel1);
            this.Controls.Add(this.CountLabel1);
            this.Controls.Add(this.ReplyStaffNameLabel1);
            this.Controls.Add(this.ReplyDateTimeLabel1);
            this.Controls.Add(this.OrderStaffNameLabel1);
            this.Controls.Add(this.OrderDateTimeLabel1);
            this.Controls.Add(this.ReplyDeleteButton1);
            this.Controls.Add(this.ReplySaveButton1);
            this.Controls.Add(this.OrderDeleteButton1);
            this.Controls.Add(this.OrderSaveButton1);
            this.Controls.Add(this.StatusBox1);
            this.Controls.Add(this.ReplyNewButton1);
            this.Controls.Add(this.OrderNewButton1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ReplyBox1);
            this.Controls.Add(this.SEQLabel2);
            this.Controls.Add(this.SEQLabel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.KindBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OrderBox1);
            this.Controls.Add(this.ListView1);
            this.Name = "FormPatBoard";
            this.Text = "患者掲示板";
            this.Load += new System.EventHandler(this.FormPatBoard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ListView1;
        private System.Windows.Forms.TextBox OrderBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox KindBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label SEQLabel1;
        private System.Windows.Forms.Label SEQLabel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ReplyBox1;
        private System.Windows.Forms.Button OrderNewButton1;
        private System.Windows.Forms.Button ReplyNewButton1;
        private System.Windows.Forms.CheckBox StatusBox1;
        private System.Windows.Forms.Button OrderSaveButton1;
        private System.Windows.Forms.Button OrderDeleteButton1;
        private System.Windows.Forms.Button ReplyDeleteButton1;
        private System.Windows.Forms.Button ReplySaveButton1;
        private System.Windows.Forms.Label OrderDateTimeLabel1;
        private System.Windows.Forms.Label OrderStaffNameLabel1;
        private System.Windows.Forms.Label ReplyStaffNameLabel1;
        private System.Windows.Forms.Label ReplyDateTimeLabel1;
        private System.Windows.Forms.Label CountLabel1;
        private System.Windows.Forms.Label OrderStaffCodeLabel1;
        private System.Windows.Forms.Label ReplyStaffCodeLabel1;
        private System.Windows.Forms.Label OrderStaffCodeLabel2;
        private System.Windows.Forms.Label ReplyStaffCodeLabel2;
        private System.Windows.Forms.CheckBox InactiveBox1;
        private System.Windows.Forms.Button BaseOrderButton1;
        private System.Windows.Forms.CheckBox DoneBox1;
        private MedicalLibrary.Boundary.StdControlPat1 stdControlPat11;
        private MedicalLibrary.Boundary.StdControlFont1 stdControlFont11;
        private System.Windows.Forms.Button OpeOrderButton1;
        private System.Windows.Forms.Button NursingOrderButton1;
    }
}