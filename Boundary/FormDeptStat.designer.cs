namespace MedicalLibrary.Boundary
{
    partial class FormDeptStat
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDeptStat));
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.TickBox1 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.kindBox1 = new System.Windows.Forms.GroupBox();
            this.KindButton2 = new System.Windows.Forms.RadioButton();
            this.KindButton1 = new System.Windows.Forms.RadioButton();
            this.label_all_count0 = new System.Windows.Forms.Label();
            this.DatePicker1 = new System.Windows.Forms.DateTimePicker();
            this.DeptListView1 = new System.Windows.Forms.DataGridView();
            this.kindBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DeptListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // TickBox1
            // 
            this.TickBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TickBox1.FormattingEnabled = true;
            this.TickBox1.Location = new System.Drawing.Point(225, 8);
            this.TickBox1.Name = "TickBox1";
            this.TickBox1.Size = new System.Drawing.Size(50, 20);
            this.TickBox1.TabIndex = 36;
            this.TickBox1.SelectedIndexChanged += new System.EventHandler(this.TickBox1_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(145, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 35;
            this.label6.Text = "更新間隔（秒）";
            // 
            // kindBox1
            // 
            this.kindBox1.Controls.Add(this.KindButton2);
            this.kindBox1.Controls.Add(this.KindButton1);
            this.kindBox1.Location = new System.Drawing.Point(150, 30);
            this.kindBox1.Name = "kindBox1";
            this.kindBox1.Size = new System.Drawing.Size(120, 32);
            this.kindBox1.TabIndex = 24;
            this.kindBox1.TabStop = false;
            // 
            // KindButton2
            // 
            this.KindButton2.AutoSize = true;
            this.KindButton2.Location = new System.Drawing.Point(65, 12);
            this.KindButton2.Name = "KindButton2";
            this.KindButton2.Size = new System.Drawing.Size(47, 16);
            this.KindButton2.TabIndex = 1;
            this.KindButton2.TabStop = true;
            this.KindButton2.Text = "救急";
            this.KindButton2.UseVisualStyleBackColor = true;
            this.KindButton2.CheckedChanged += new System.EventHandler(this.KindButton2_CheckedChanged);
            // 
            // KindButton1
            // 
            this.KindButton1.AutoSize = true;
            this.KindButton1.Location = new System.Drawing.Point(10, 12);
            this.KindButton1.Name = "KindButton1";
            this.KindButton1.Size = new System.Drawing.Size(47, 16);
            this.KindButton1.TabIndex = 0;
            this.KindButton1.TabStop = true;
            this.KindButton1.Text = "通常";
            this.KindButton1.UseVisualStyleBackColor = true;
            this.KindButton1.CheckedChanged += new System.EventHandler(this.KindButton1_CheckedChanged);
            // 
            // label_all_count0
            // 
            this.label_all_count0.AutoSize = true;
            this.label_all_count0.Location = new System.Drawing.Point(10, 45);
            this.label_all_count0.Name = "label_all_count0";
            this.label_all_count0.Size = new System.Drawing.Size(53, 12);
            this.label_all_count0.TabIndex = 23;
            this.label_all_count0.Text = "受付人数";
            // 
            // DatePicker1
            // 
            this.DatePicker1.CustomFormat = "yyyy/MM/dd (ddd)";
            this.DatePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatePicker1.Location = new System.Drawing.Point(12, 8);
            this.DatePicker1.MaxDate = new System.DateTime(2015, 12, 31, 0, 0, 0, 0);
            this.DatePicker1.MinDate = new System.DateTime(2013, 1, 1, 0, 0, 0, 0);
            this.DatePicker1.Name = "DatePicker1";
            this.DatePicker1.Size = new System.Drawing.Size(125, 19);
            this.DatePicker1.TabIndex = 22;
            this.DatePicker1.Value = new System.DateTime(2015, 12, 31, 0, 0, 0, 0);
            this.DatePicker1.ValueChanged += new System.EventHandler(this.DatePicker1_ValueChanged);
            // 
            // DeptListView1
            // 
            this.DeptListView1.AllowUserToAddRows = false;
            this.DeptListView1.AllowUserToDeleteRows = false;
            this.DeptListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DeptListView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DeptListView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DeptListView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.DeptListView1.Location = new System.Drawing.Point(12, 63);
            this.DeptListView1.MultiSelect = false;
            this.DeptListView1.Name = "DeptListView1";
            this.DeptListView1.ReadOnly = true;
            this.DeptListView1.RowHeadersVisible = false;
            this.DeptListView1.RowTemplate.Height = 21;
            this.DeptListView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DeptListView1.Size = new System.Drawing.Size(260, 375);
            this.DeptListView1.TabIndex = 8;
            // 
            // FormDeptStat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 442);
            this.Controls.Add(this.TickBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.kindBox1);
            this.Controls.Add(this.label_all_count0);
            this.Controls.Add(this.DatePicker1);
            this.Controls.Add(this.DeptListView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDeptStat";
            this.Text = "外来患者数";
            this.kindBox1.ResumeLayout(false);
            this.kindBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DeptListView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DeptListView1;
        private System.Windows.Forms.DateTimePicker DatePicker1;
        private System.Windows.Forms.Label label_all_count0;
        private System.Windows.Forms.GroupBox kindBox1;
        private System.Windows.Forms.RadioButton KindButton2;
        private System.Windows.Forms.RadioButton KindButton1;
        private System.Windows.Forms.ComboBox TickBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer Timer1;
    }
}