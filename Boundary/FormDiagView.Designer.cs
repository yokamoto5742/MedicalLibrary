namespace MedicalLibrary.Boundary
{
    partial class FormDiagView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDiagView));
            this.ListView1 = new MedicalLibrary.Boundary.CtrlDiagGridView1();
            this.stdControlPat11 = new MedicalLibrary.Boundary.StdControlPat1();
            this.DiagCheckBox2 = new System.Windows.Forms.CheckBox();
            this.DiagCheckBox1 = new System.Windows.Forms.CheckBox();
            this.DPCButton1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).BeginInit();
            this.SuspendLayout();
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
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
            this.ListView1.Size = new System.Drawing.Size(775, 220);
            this.ListView1.TabIndex = 0;
            // 
            // stdControlPat11
            // 
            this.stdControlPat11.Location = new System.Drawing.Point(10, 5);
            this.stdControlPat11.Mode1 = MedicalLibrary.Boundary.StdControlPat1.Mode.Short;
            this.stdControlPat11.Name = "stdControlPat11";
            this.stdControlPat11.ReadOnly = false;
            this.stdControlPat11.Size = new System.Drawing.Size(270, 30);
            this.stdControlPat11.TabIndex = 1;
            // 
            // DiagCheckBox2
            // 
            this.DiagCheckBox2.AutoSize = true;
            this.DiagCheckBox2.Location = new System.Drawing.Point(415, 12);
            this.DiagCheckBox2.Name = "DiagCheckBox2";
            this.DiagCheckBox2.Size = new System.Drawing.Size(72, 16);
            this.DiagCheckBox2.TabIndex = 38;
            this.DiagCheckBox2.Text = "転帰病名";
            this.DiagCheckBox2.UseVisualStyleBackColor = true;
            this.DiagCheckBox2.CheckedChanged += new System.EventHandler(this.DiagCheckBox2_CheckedChanged);
            // 
            // DiagCheckBox1
            // 
            this.DiagCheckBox1.AutoSize = true;
            this.DiagCheckBox1.Location = new System.Drawing.Point(340, 12);
            this.DiagCheckBox1.Name = "DiagCheckBox1";
            this.DiagCheckBox1.Size = new System.Drawing.Size(60, 16);
            this.DiagCheckBox1.TabIndex = 37;
            this.DiagCheckBox1.Text = "現病名";
            this.DiagCheckBox1.UseVisualStyleBackColor = true;
            this.DiagCheckBox1.CheckedChanged += new System.EventHandler(this.DiagCheckBox1_CheckedChanged);
            // 
            // DPCButton1
            // 
            this.DPCButton1.Location = new System.Drawing.Point(540, 8);
            this.DPCButton1.Name = "DPCButton1";
            this.DPCButton1.Size = new System.Drawing.Size(75, 23);
            this.DPCButton1.TabIndex = 47;
            this.DPCButton1.Text = "DPC";
            this.DPCButton1.UseVisualStyleBackColor = true;
            this.DPCButton1.Click += new System.EventHandler(this.DPCButton1_Click);
            // 
            // FormDiagView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 262);
            this.Controls.Add(this.DPCButton1);
            this.Controls.Add(this.DiagCheckBox2);
            this.Controls.Add(this.DiagCheckBox1);
            this.Controls.Add(this.stdControlPat11);
            this.Controls.Add(this.ListView1);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDiagView";
            this.Text = "病名";
            this.Load += new System.EventHandler(this.FormDiagView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CtrlDiagGridView1 ListView1;
        private StdControlPat1 stdControlPat11;
        private System.Windows.Forms.CheckBox DiagCheckBox2;
        private System.Windows.Forms.CheckBox DiagCheckBox1;
        private System.Windows.Forms.Button DPCButton1;
    }
}