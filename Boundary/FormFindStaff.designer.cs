namespace MedicalLibrary.Boundary
{
    partial class FormFindStaff
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFindStaff));
            this.SelectButton1 = new System.Windows.Forms.Button();
            this.ListView1 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.FindButton1 = new System.Windows.Forms.Button();
            this.KanaBox1 = new System.Windows.Forms.TextBox();
            this.NameBox1 = new System.Windows.Forms.TextBox();
            this.DoctorButton1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ClearButton1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // SelectButton1
            // 
            this.SelectButton1.Location = new System.Drawing.Point(480, 10);
            this.SelectButton1.Name = "SelectButton1";
            this.SelectButton1.Size = new System.Drawing.Size(60, 22);
            this.SelectButton1.TabIndex = 21;
            this.SelectButton1.Text = "選択";
            this.SelectButton1.UseVisualStyleBackColor = true;
            this.SelectButton1.Click += new System.EventHandler(this.SelectButton1_Click);
            // 
            // ListView1
            // 
            this.ListView1.AllowUserToAddRows = false;
            this.ListView1.AllowUserToDeleteRows = false;
            this.ListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ListView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ListView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListView1.Location = new System.Drawing.Point(5, 40);
            this.ListView1.Name = "ListView1";
            this.ListView1.ReadOnly = true;
            this.ListView1.RowHeadersVisible = false;
            this.ListView1.RowTemplate.Height = 21;
            this.ListView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ListView1.Size = new System.Drawing.Size(535, 260);
            this.ListView1.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(130, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "カナ氏名";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "氏名";
            // 
            // FindButton1
            // 
            this.FindButton1.Location = new System.Drawing.Point(350, 10);
            this.FindButton1.Name = "FindButton1";
            this.FindButton1.Size = new System.Drawing.Size(70, 22);
            this.FindButton1.TabIndex = 16;
            this.FindButton1.Text = "検索 (F5)";
            this.FindButton1.UseVisualStyleBackColor = true;
            this.FindButton1.Click += new System.EventHandler(this.FindButton1_Click);
            // 
            // KanaBox1
            // 
            this.KanaBox1.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.KanaBox1.Location = new System.Drawing.Point(180, 13);
            this.KanaBox1.Name = "KanaBox1";
            this.KanaBox1.Size = new System.Drawing.Size(83, 19);
            this.KanaBox1.TabIndex = 13;
            this.KanaBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KanaBox1_KeyDown);
            // 
            // NameBox1
            // 
            this.NameBox1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.NameBox1.Location = new System.Drawing.Point(45, 12);
            this.NameBox1.Name = "NameBox1";
            this.NameBox1.Size = new System.Drawing.Size(82, 19);
            this.NameBox1.TabIndex = 12;
            this.NameBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NameBox1_KeyDown);
            // 
            // DoctorButton1
            // 
            this.DoctorButton1.Location = new System.Drawing.Point(270, 10);
            this.DoctorButton1.Name = "DoctorButton1";
            this.DoctorButton1.Size = new System.Drawing.Size(70, 22);
            this.DoctorButton1.TabIndex = 22;
            this.DoctorButton1.Text = "常勤医師";
            this.DoctorButton1.UseVisualStyleBackColor = true;
            this.DoctorButton1.Click += new System.EventHandler(this.DoctorButton1_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 305);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(263, 12);
            this.label3.TabIndex = 23;
            this.label3.Text = "Ctrlキーを押しながらクリックすると複数選択ができます。";
            // 
            // ClearButton1
            // 
            this.ClearButton1.Location = new System.Drawing.Point(425, 10);
            this.ClearButton1.Name = "ClearButton1";
            this.ClearButton1.Size = new System.Drawing.Size(50, 22);
            this.ClearButton1.TabIndex = 24;
            this.ClearButton1.Text = "クリア";
            this.ClearButton1.UseVisualStyleBackColor = true;
            this.ClearButton1.Click += new System.EventHandler(this.ClearButton1_Click);
            // 
            // FormFindStaff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 322);
            this.Controls.Add(this.ClearButton1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DoctorButton1);
            this.Controls.Add(this.SelectButton1);
            this.Controls.Add(this.ListView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FindButton1);
            this.Controls.Add(this.KanaBox1);
            this.Controls.Add(this.NameBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormFindStaff";
            this.Text = "送信先";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormFindStaff_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SelectButton1;
        private System.Windows.Forms.DataGridView ListView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button FindButton1;
        private System.Windows.Forms.TextBox KanaBox1;
        private System.Windows.Forms.TextBox NameBox1;
        private System.Windows.Forms.Button DoctorButton1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ClearButton1;
    }
}