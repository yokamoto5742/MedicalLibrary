namespace MedicalLibrary.Agent
{
    partial class ReceStatListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReceStatListForm));
            this.PatListView = new System.Windows.Forms.DataGridView();
            this.PatListMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PatListLabelPrintMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.DeptBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.updateButton = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.label_all_count0 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label_count1 = new System.Windows.Forms.Label();
            this.label_count2 = new System.Windows.Forms.Label();
            this.label_count3 = new System.Windows.Forms.Label();
            this.showEnd = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.ModeBox1 = new System.Windows.Forms.ComboBox();
            this.KarteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.PatListView)).BeginInit();
            this.PatListMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // PatListView
            // 
            this.PatListView.AllowUserToAddRows = false;
            this.PatListView.AllowUserToDeleteRows = false;
            this.PatListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PatListView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.PatListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PatListView.ContextMenuStrip = this.PatListMenuStrip;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.PatListView.DefaultCellStyle = dataGridViewCellStyle2;
            this.PatListView.Location = new System.Drawing.Point(165, 50);
            this.PatListView.Name = "PatListView";
            this.PatListView.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PatListView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.PatListView.RowHeadersVisible = false;
            this.PatListView.RowTemplate.Height = 21;
            this.PatListView.Size = new System.Drawing.Size(835, 685);
            this.PatListView.TabIndex = 0;
            this.PatListView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
            // 
            // PatListMenuStrip
            // 
            this.PatListMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.KarteMenuItem,
            this.PatListLabelPrintMenuItem});
            this.PatListMenuStrip.Name = "PatListMenuStrip";
            this.PatListMenuStrip.Size = new System.Drawing.Size(153, 70);
            // 
            // PatListLabelPrintMenuItem
            // 
            this.PatListLabelPrintMenuItem.Name = "PatListLabelPrintMenuItem";
            this.PatListLabelPrintMenuItem.Size = new System.Drawing.Size(152, 22);
            this.PatListLabelPrintMenuItem.Text = "ラベル発行";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy/MM/dd (ddd)";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(56, 7);
            this.dateTimePicker1.MinDate = new System.DateTime(2007, 5, 1, 0, 0, 0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(125, 19);
            this.dateTimePicker1.TabIndex = 1;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "受付日";
            // 
            // DeptBox1
            // 
            this.DeptBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeptBox1.FormattingEnabled = true;
            this.DeptBox1.Location = new System.Drawing.Point(360, 7);
            this.DeptBox1.Name = "DeptBox1";
            this.DeptBox1.Size = new System.Drawing.Size(107, 20);
            this.DeptBox1.TabIndex = 3;
            this.DeptBox1.SelectedIndexChanged += new System.EventHandler(this.DeptBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(315, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "診療科";
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(600, 7);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(87, 22);
            this.updateButton.TabIndex = 5;
            this.updateButton.Text = "更新 (F5)";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView2.Location = new System.Drawing.Point(7, 50);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowTemplate.Height = 21;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(150, 350);
            this.dataGridView2.TabIndex = 7;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView3.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView3.Location = new System.Drawing.Point(7, 420);
            this.dataGridView3.MultiSelect = false;
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.RowHeadersVisible = false;
            this.dataGridView3.RowTemplate.Height = 21;
            this.dataGridView3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView3.Size = new System.Drawing.Size(150, 315);
            this.dataGridView3.TabIndex = 8;
            this.dataGridView3.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView3_CellClick);
            // 
            // label_all_count0
            // 
            this.label_all_count0.AutoSize = true;
            this.label_all_count0.Location = new System.Drawing.Point(5, 35);
            this.label_all_count0.Name = "label_all_count0";
            this.label_all_count0.Size = new System.Drawing.Size(53, 12);
            this.label_all_count0.TabIndex = 9;
            this.label_all_count0.Text = "受付人数";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 405);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "診察未終了人数";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(165, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "受付患者一覧";
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(935, 7);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(68, 22);
            this.closeButton.TabIndex = 12;
            this.closeButton.Text = "閉じる";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(700, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(191, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "※画面は3分おきに自動更新されます。";
            // 
            // label_count1
            // 
            this.label_count1.AutoSize = true;
            this.label_count1.BackColor = System.Drawing.Color.White;
            this.label_count1.Location = new System.Drawing.Point(480, 35);
            this.label_count1.Name = "label_count1";
            this.label_count1.Size = new System.Drawing.Size(65, 12);
            this.label_count1.TabIndex = 15;
            this.label_count1.Text = "診察開始前";
            // 
            // label_count2
            // 
            this.label_count2.AutoSize = true;
            this.label_count2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label_count2.Location = new System.Drawing.Point(621, 35);
            this.label_count2.Name = "label_count2";
            this.label_count2.Size = new System.Drawing.Size(41, 12);
            this.label_count2.TabIndex = 16;
            this.label_count2.Text = "診察中";
            // 
            // label_count3
            // 
            this.label_count3.AutoSize = true;
            this.label_count3.BackColor = System.Drawing.Color.DarkGray;
            this.label_count3.Location = new System.Drawing.Point(761, 35);
            this.label_count3.Name = "label_count3";
            this.label_count3.Size = new System.Drawing.Size(53, 12);
            this.label_count3.TabIndex = 17;
            this.label_count3.Text = "診察終了";
            // 
            // showEnd
            // 
            this.showEnd.AutoSize = true;
            this.showEnd.Location = new System.Drawing.Point(490, 10);
            this.showEnd.Name = "showEnd";
            this.showEnd.Size = new System.Drawing.Size(96, 16);
            this.showEnd.TabIndex = 18;
            this.showEnd.Text = "診察終了表示";
            this.showEnd.UseVisualStyleBackColor = true;
            this.showEnd.CheckedChanged += new System.EventHandler(this.showEnd_CheckedChanged);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(190, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "区分";
            // 
            // ModeBox1
            // 
            this.ModeBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ModeBox1.FormattingEnabled = true;
            this.ModeBox1.Location = new System.Drawing.Point(225, 7);
            this.ModeBox1.Name = "ModeBox1";
            this.ModeBox1.Size = new System.Drawing.Size(75, 20);
            this.ModeBox1.TabIndex = 19;
            this.ModeBox1.SelectedIndexChanged += new System.EventHandler(this.ModeBox1_SelectedIndexChanged);
            // 
            // KarteMenuItem
            // 
            this.KarteMenuItem.Name = "KarteMenuItem";
            this.KarteMenuItem.Size = new System.Drawing.Size(152, 22);
            this.KarteMenuItem.Text = "カルテ";
            this.KarteMenuItem.Click += new System.EventHandler(this.KarteMenuItem_Click);
            // 
            // ReceStatListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 742);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ModeBox1);
            this.Controls.Add(this.showEnd);
            this.Controls.Add(this.label_count3);
            this.Controls.Add(this.label_count2);
            this.Controls.Add(this.label_count1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label_all_count0);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DeptBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.PatListView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "ReceStatListForm";
            this.Text = "診察状況";
            this.Load += new System.EventHandler(this.ReceStatListForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReceStatListForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.PatListView)).EndInit();
            this.PatListMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView PatListView;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox DeptBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Label label_all_count0;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label_count1;
        private System.Windows.Forms.Label label_count2;
        private System.Windows.Forms.Label label_count3;
        private System.Windows.Forms.CheckBox showEnd;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip PatListMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem PatListLabelPrintMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ModeBox1;
        private System.Windows.Forms.ToolStripMenuItem KarteMenuItem;
    }
}

