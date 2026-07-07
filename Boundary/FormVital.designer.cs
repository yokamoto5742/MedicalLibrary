namespace MedicalLibrary.Boundary
{
    partial class FormVital
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
            this.ChartBox1 = new System.Windows.Forms.PictureBox();
            this.DataPanel1 = new System.Windows.Forms.Panel();
            this.DataBox1 = new System.Windows.Forms.PictureBox();
            this.ChartDate1 = new System.Windows.Forms.DateTimePicker();
            this.stdControlPat11 = new MedicalLibrary.Boundary.StdControlPat1();
            ((System.ComponentModel.ISupportInitialize)(this.ChartBox1)).BeginInit();
            this.DataPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ChartBox1
            // 
            this.ChartBox1.BackColor = System.Drawing.Color.White;
            this.ChartBox1.Location = new System.Drawing.Point(5, 40);
            this.ChartBox1.Name = "ChartBox1";
            this.ChartBox1.Size = new System.Drawing.Size(1175, 305);
            this.ChartBox1.TabIndex = 15;
            this.ChartBox1.TabStop = false;
            // 
            // DataPanel1
            // 
            this.DataPanel1.AutoScroll = true;
            this.DataPanel1.Controls.Add(this.DataBox1);
            this.DataPanel1.Location = new System.Drawing.Point(5, 356);
            this.DataPanel1.Name = "DataPanel1";
            this.DataPanel1.Size = new System.Drawing.Size(1175, 380);
            this.DataPanel1.TabIndex = 14;
            // 
            // DataBox1
            // 
            this.DataBox1.BackColor = System.Drawing.Color.White;
            this.DataBox1.Location = new System.Drawing.Point(4, 8);
            this.DataBox1.Name = "DataBox1";
            this.DataBox1.Size = new System.Drawing.Size(1104, 337);
            this.DataBox1.TabIndex = 11;
            this.DataBox1.TabStop = false;
            // 
            // ChartDate1
            // 
            this.ChartDate1.CustomFormat = "yyyy/MM/dd (ddd)";
            this.ChartDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ChartDate1.Location = new System.Drawing.Point(520, 12);
            this.ChartDate1.Name = "ChartDate1";
            this.ChartDate1.Size = new System.Drawing.Size(128, 19);
            this.ChartDate1.TabIndex = 16;
            this.ChartDate1.ValueChanged += new System.EventHandler(this.ChartDate1_ValueChanged);
            // 
            // stdControlPat11
            // 
            this.stdControlPat11.Location = new System.Drawing.Point(10, 5);
            this.stdControlPat11.Name = "stdControlPat11";
            this.stdControlPat11.Size = new System.Drawing.Size(500, 30);
            this.stdControlPat11.TabIndex = 17;
            // 
            // FormVital
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1184, 762);
            this.Controls.Add(this.stdControlPat11);
            this.Controls.Add(this.ChartDate1);
            this.Controls.Add(this.ChartBox1);
            this.Controls.Add(this.DataPanel1);
            this.Name = "FormVital";
            this.Text = "バイタル";
            this.Load += new System.EventHandler(this.FormVital_Load);
            this.Resize += new System.EventHandler(this.FormVital_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.ChartBox1)).EndInit();
            this.DataPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ChartBox1;
        private System.Windows.Forms.Panel DataPanel1;
        private System.Windows.Forms.PictureBox DataBox1;
        private System.Windows.Forms.DateTimePicker ChartDate1;
        private MedicalLibrary.Boundary.StdControlPat1 stdControlPat11;
    }
}