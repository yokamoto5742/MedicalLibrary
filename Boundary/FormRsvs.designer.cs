namespace MedicalLibrary.Boundary
{
    partial class FormRsvs
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ListView1 = new System.Windows.Forms.DataGridView();
            this.ListView2 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.KeywordBox1 = new System.Windows.Forms.TextBox();
            this.KindButton2 = new System.Windows.Forms.RadioButton();
            this.KindButton1 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListView2)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListView1
            // 
            this.ListView1.AllowUserToAddRows = false;
            this.ListView1.AllowUserToDeleteRows = false;
            this.ListView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ListView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ListView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListView1.Location = new System.Drawing.Point(10, 35);
            this.ListView1.Name = "ListView1";
            this.ListView1.ReadOnly = true;
            this.ListView1.RowHeadersVisible = false;
            this.ListView1.RowTemplate.Height = 21;
            this.ListView1.Size = new System.Drawing.Size(230, 420);
            this.ListView1.TabIndex = 1;
            this.ListView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListView1_CellClick);
            this.ListView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListView1_CellDoubleClick);
            // 
            // ListView2
            // 
            this.ListView2.AllowUserToAddRows = false;
            this.ListView2.AllowUserToDeleteRows = false;
            this.ListView2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ListView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ListView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListView2.Location = new System.Drawing.Point(10, 460);
            this.ListView2.Name = "ListView2";
            this.ListView2.ReadOnly = true;
            this.ListView2.RowHeadersVisible = false;
            this.ListView2.RowTemplate.Height = 21;
            this.ListView2.Size = new System.Drawing.Size(230, 98);
            this.ListView2.TabIndex = 2;
            this.ListView2.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListView2_CellDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.KeywordBox1);
            this.panel1.Controls.Add(this.KindButton2);
            this.panel1.Controls.Add(this.KindButton1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 560);
            this.panel1.TabIndex = 3;
            // 
            // KeywordBox1
            // 
            this.KeywordBox1.Location = new System.Drawing.Point(140, 8);
            this.KeywordBox1.Name = "KeywordBox1";
            this.KeywordBox1.Size = new System.Drawing.Size(100, 19);
            this.KeywordBox1.TabIndex = 4;
            this.KeywordBox1.TextChanged += new System.EventHandler(this.KeywordBox1_TextChanged);
            // 
            // KindButton2
            // 
            this.KindButton2.AutoSize = true;
            this.KindButton2.Location = new System.Drawing.Point(75, 10);
            this.KindButton2.Name = "KindButton2";
            this.KindButton2.Size = new System.Drawing.Size(47, 16);
            this.KindButton2.TabIndex = 5;
            this.KindButton2.TabStop = true;
            this.KindButton2.Text = "検査";
            this.KindButton2.UseVisualStyleBackColor = true;
            this.KindButton2.CheckedChanged += new System.EventHandler(this.KindButton2_CheckedChanged);
            // 
            // KindButton1
            // 
            this.KindButton1.AutoSize = true;
            this.KindButton1.Location = new System.Drawing.Point(15, 10);
            this.KindButton1.Name = "KindButton1";
            this.KindButton1.Size = new System.Drawing.Size(47, 16);
            this.KindButton1.TabIndex = 4;
            this.KindButton1.TabStop = true;
            this.KindButton1.Text = "診察";
            this.KindButton1.UseVisualStyleBackColor = true;
            this.KindButton1.CheckedChanged += new System.EventHandler(this.KindButton1_CheckedChanged);
            // 
            // FormRsvs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 562);
            this.Controls.Add(this.ListView2);
            this.Controls.Add(this.ListView1);
            this.Controls.Add(this.panel1);
            this.IsMdiContainer = true;
            this.Name = "FormRsvs";
            this.Padding = new System.Windows.Forms.Padding(250, 0, 0, 0);
            this.Text = "予約";
            this.Load += new System.EventHandler(this.FormRsvs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListView2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ListView1;
        private System.Windows.Forms.DataGridView ListView2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton KindButton2;
        private System.Windows.Forms.RadioButton KindButton1;
        private System.Windows.Forms.TextBox KeywordBox1;
    }
}