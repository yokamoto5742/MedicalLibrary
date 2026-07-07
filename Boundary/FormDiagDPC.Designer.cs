namespace MedicalLibrary.Boundary
{
    partial class FormDiagDPC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDiagDPC));
            this.label1 = new System.Windows.Forms.Label();
            this.stdControlPat11 = new MedicalLibrary.Boundary.StdControlPat1();
            this.InHistoryBox1 = new MedicalLibrary.Boundary.CtrlInHistoryBox1();
            this.DiagCheckBox2 = new System.Windows.Forms.CheckBox();
            this.DiagCheckBox1 = new System.Windows.Forms.CheckBox();
            this.DiagDPCGridView1 = new MedicalLibrary.Boundary.CtrlDiagDPCGridView1();
            this.SaveButton1 = new System.Windows.Forms.Button();
            this.SaveLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.stdControlFont11 = new MedicalLibrary.Boundary.StdControlFont1();
            ((System.ComponentModel.ISupportInitialize)(this.DiagDPCGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(600, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "入院期間";
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
            // InHistoryBox1
            // 
            this.InHistoryBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InHistoryBox1.FormattingEnabled = true;
            this.InHistoryBox1.Location = new System.Drawing.Point(660, 10);
            this.InHistoryBox1.Name = "InHistoryBox1";
            this.InHistoryBox1.Size = new System.Drawing.Size(180, 20);
            this.InHistoryBox1.TabIndex = 4;
            this.InHistoryBox1.SelectedIndexChanged += new System.EventHandler(this.InHistoryBox1_SelectedIndexChanged);
            // 
            // DiagCheckBox2
            // 
            this.DiagCheckBox2.AutoSize = true;
            this.DiagCheckBox2.Location = new System.Drawing.Point(335, 40);
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
            this.DiagCheckBox1.Location = new System.Drawing.Point(260, 40);
            this.DiagCheckBox1.Name = "DiagCheckBox1";
            this.DiagCheckBox1.Size = new System.Drawing.Size(60, 16);
            this.DiagCheckBox1.TabIndex = 37;
            this.DiagCheckBox1.Text = "現病名";
            this.DiagCheckBox1.UseVisualStyleBackColor = true;
            this.DiagCheckBox1.CheckedChanged += new System.EventHandler(this.DiagCheckBox1_CheckedChanged);
            // 
            // DiagDPCGridView1
            // 
            this.DiagDPCGridView1.AllowUserToAddRows = false;
            this.DiagDPCGridView1.AllowUserToDeleteRows = false;
            this.DiagDPCGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DiagDPCGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DiagDPCGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DiagDPCGridView1.Location = new System.Drawing.Point(5, 60);
            this.DiagDPCGridView1.Name = "DiagDPCGridView1";
            this.DiagDPCGridView1.RowHeadersVisible = false;
            this.DiagDPCGridView1.RowHeadersWidth = 20;
            this.DiagDPCGridView1.RowTemplate.Height = 21;
            this.DiagDPCGridView1.Size = new System.Drawing.Size(1055, 300);
            this.DiagDPCGridView1.TabIndex = 39;
            // 
            // SaveButton1
            // 
            this.SaveButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton1.Location = new System.Drawing.Point(980, 10);
            this.SaveButton1.Name = "SaveButton1";
            this.SaveButton1.Size = new System.Drawing.Size(75, 45);
            this.SaveButton1.TabIndex = 40;
            this.SaveButton1.Text = "登録";
            this.SaveButton1.UseVisualStyleBackColor = true;
            this.SaveButton1.Click += new System.EventHandler(this.SaveButton1_Click);
            // 
            // SaveLabel
            // 
            this.SaveLabel.AutoEllipsis = true;
            this.SaveLabel.BackColor = System.Drawing.Color.LightYellow;
            this.SaveLabel.Location = new System.Drawing.Point(660, 38);
            this.SaveLabel.Name = "SaveLabel";
            this.SaveLabel.Size = new System.Drawing.Size(180, 18);
            this.SaveLabel.TabIndex = 41;
            this.SaveLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(600, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 42;
            this.label2.Text = "登録日時";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(20, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 12);
            this.label3.TabIndex = 43;
            this.label3.Text = "ICD が赤字のものは留意病名";
            // 
            // stdControlFont11
            // 
            this.stdControlFont11.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.stdControlFont11.Location = new System.Drawing.Point(470, 3);
            this.stdControlFont11.Name = "stdControlFont11";
            this.stdControlFont11.Size = new System.Drawing.Size(60, 30);
            this.stdControlFont11.TabIndex = 44;
            // 
            // FormDiagDPC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 362);
            this.Controls.Add(this.stdControlFont11);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SaveLabel);
            this.Controls.Add(this.SaveButton1);
            this.Controls.Add(this.DiagDPCGridView1);
            this.Controls.Add(this.DiagCheckBox2);
            this.Controls.Add(this.DiagCheckBox1);
            this.Controls.Add(this.InHistoryBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stdControlPat11);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDiagDPC";
            this.Text = "DPC病名";
            this.Load += new System.EventHandler(this.FormDiagDPC_Load);
            this.Shown += new System.EventHandler(this.FormDiagDPC_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.DiagDPCGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StdControlPat1 stdControlPat11;
        private System.Windows.Forms.Label label1;
        private CtrlInHistoryBox1 InHistoryBox1;
        private System.Windows.Forms.CheckBox DiagCheckBox2;
        private System.Windows.Forms.CheckBox DiagCheckBox1;
        private CtrlDiagDPCGridView1 DiagDPCGridView1;
        private System.Windows.Forms.Button SaveButton1;
        private System.Windows.Forms.Label SaveLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private StdControlFont1 stdControlFont11;
    }
}