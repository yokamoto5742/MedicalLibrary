namespace MedicalLibrary.Boundary
{
    partial class FormVoucher
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
            this.VoucherGridView1 = new System.Windows.Forms.DataGridView();
            this.CodeBox1 = new System.Windows.Forms.TextBox();
            this.ShowButton1 = new System.Windows.Forms.Button();
            this.VoucherPanel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.VoucherGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // VoucherGridView1
            // 
            this.VoucherGridView1.AllowUserToAddRows = false;
            this.VoucherGridView1.AllowUserToDeleteRows = false;
            this.VoucherGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.VoucherGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.VoucherGridView1.Location = new System.Drawing.Point(12, 48);
            this.VoucherGridView1.Name = "VoucherGridView1";
            this.VoucherGridView1.RowTemplate.Height = 21;
            this.VoucherGridView1.Size = new System.Drawing.Size(456, 502);
            this.VoucherGridView1.TabIndex = 0;
            // 
            // CodeBox1
            // 
            this.CodeBox1.Location = new System.Drawing.Point(12, 12);
            this.CodeBox1.Name = "CodeBox1";
            this.CodeBox1.Size = new System.Drawing.Size(100, 19);
            this.CodeBox1.TabIndex = 1;
            // 
            // ShowButton1
            // 
            this.ShowButton1.Location = new System.Drawing.Point(135, 10);
            this.ShowButton1.Name = "ShowButton1";
            this.ShowButton1.Size = new System.Drawing.Size(75, 23);
            this.ShowButton1.TabIndex = 2;
            this.ShowButton1.Text = "表示";
            this.ShowButton1.UseVisualStyleBackColor = true;
            this.ShowButton1.Click += new System.EventHandler(this.ShowButton1_Click);
            // 
            // VoucherPanel1
            // 
            this.VoucherPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VoucherPanel1.AutoScroll = true;
            this.VoucherPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.VoucherPanel1.Location = new System.Drawing.Point(480, 48);
            this.VoucherPanel1.Name = "VoucherPanel1";
            this.VoucherPanel1.Size = new System.Drawing.Size(517, 502);
            this.VoucherPanel1.TabIndex = 3;
            // 
            // FormVoucher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 562);
            this.Controls.Add(this.VoucherPanel1);
            this.Controls.Add(this.ShowButton1);
            this.Controls.Add(this.CodeBox1);
            this.Controls.Add(this.VoucherGridView1);
            this.Name = "FormVoucher";
            this.Text = "FormVoucher";
            this.Load += new System.EventHandler(this.FormVoucher_Load);
            ((System.ComponentModel.ISupportInitialize)(this.VoucherGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView VoucherGridView1;
        private System.Windows.Forms.TextBox CodeBox1;
        private System.Windows.Forms.Button ShowButton1;
        private System.Windows.Forms.Panel VoucherPanel1;
    }
}