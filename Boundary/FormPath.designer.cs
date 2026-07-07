namespace MedicalLibrary.Boundary
{
    partial class FormPath
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
            this.FormPathSelectorButton1 = new System.Windows.Forms.Button();
            this.PathMasterBox1 = new System.Windows.Forms.TextBox();
            this.PathBox1 = new System.Windows.Forms.PictureBox();
            this.stdControlPat11 = new MedicalLibrary.Boundary.StdControlPat1();
            this.PathDate1 = new System.Windows.Forms.DateTimePicker();
            this.PathDatePanel1 = new System.Windows.Forms.Panel();
            this.PathDateBox1 = new System.Windows.Forms.PictureBox();
            this.PathCategoryPanel1 = new System.Windows.Forms.Panel();
            this.PathCategoryBox1 = new System.Windows.Forms.PictureBox();
            this.PathPanel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.PathBox1)).BeginInit();
            this.PathDatePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PathDateBox1)).BeginInit();
            this.PathCategoryPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PathCategoryBox1)).BeginInit();
            this.PathPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FormPathSelectorButton1
            // 
            this.FormPathSelectorButton1.Location = new System.Drawing.Point(1112, 7);
            this.FormPathSelectorButton1.Name = "FormPathSelectorButton1";
            this.FormPathSelectorButton1.Size = new System.Drawing.Size(75, 23);
            this.FormPathSelectorButton1.TabIndex = 1;
            this.FormPathSelectorButton1.Text = "選択";
            this.FormPathSelectorButton1.UseVisualStyleBackColor = true;
            this.FormPathSelectorButton1.Click += new System.EventHandler(this.FormPathSelectorButton1_Click);
            // 
            // PathMasterBox1
            // 
            this.PathMasterBox1.Location = new System.Drawing.Point(1006, 9);
            this.PathMasterBox1.Name = "PathMasterBox1";
            this.PathMasterBox1.Size = new System.Drawing.Size(100, 19);
            this.PathMasterBox1.TabIndex = 2;
            // 
            // PathBox1
            // 
            this.PathBox1.BackColor = System.Drawing.SystemColors.Control;
            this.PathBox1.Location = new System.Drawing.Point(3, 3);
            this.PathBox1.Name = "PathBox1";
            this.PathBox1.Size = new System.Drawing.Size(875, 494);
            this.PathBox1.TabIndex = 16;
            this.PathBox1.TabStop = false;
            // 
            // stdControlPat11
            // 
            this.stdControlPat11.Location = new System.Drawing.Point(10, 5);
            this.stdControlPat11.Name = "stdControlPat11";
            this.stdControlPat11.Size = new System.Drawing.Size(500, 30);
            this.stdControlPat11.TabIndex = 19;
            // 
            // PathDate1
            // 
            this.PathDate1.CustomFormat = "yyyy/MM/dd (ddd)";
            this.PathDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.PathDate1.Location = new System.Drawing.Point(520, 12);
            this.PathDate1.Name = "PathDate1";
            this.PathDate1.Size = new System.Drawing.Size(128, 19);
            this.PathDate1.TabIndex = 18;
            this.PathDate1.ValueChanged += new System.EventHandler(this.PathDate1_ValueChanged);
            // 
            // PathDatePanel1
            // 
            this.PathDatePanel1.Controls.Add(this.PathDateBox1);
            this.PathDatePanel1.Location = new System.Drawing.Point(100, 40);
            this.PathDatePanel1.Name = "PathDatePanel1";
            this.PathDatePanel1.Size = new System.Drawing.Size(900, 50);
            this.PathDatePanel1.TabIndex = 20;
            // 
            // PathDateBox1
            // 
            this.PathDateBox1.BackColor = System.Drawing.Color.White;
            this.PathDateBox1.Location = new System.Drawing.Point(3, 3);
            this.PathDateBox1.Name = "PathDateBox1";
            this.PathDateBox1.Size = new System.Drawing.Size(875, 42);
            this.PathDateBox1.TabIndex = 0;
            this.PathDateBox1.TabStop = false;
            // 
            // PathCategoryPanel1
            // 
            this.PathCategoryPanel1.Controls.Add(this.PathCategoryBox1);
            this.PathCategoryPanel1.Location = new System.Drawing.Point(10, 95);
            this.PathCategoryPanel1.Name = "PathCategoryPanel1";
            this.PathCategoryPanel1.Size = new System.Drawing.Size(80, 640);
            this.PathCategoryPanel1.TabIndex = 21;
            // 
            // PathCategoryBox1
            // 
            this.PathCategoryBox1.BackColor = System.Drawing.Color.White;
            this.PathCategoryBox1.Location = new System.Drawing.Point(3, 3);
            this.PathCategoryBox1.Name = "PathCategoryBox1";
            this.PathCategoryBox1.Size = new System.Drawing.Size(74, 494);
            this.PathCategoryBox1.TabIndex = 1;
            this.PathCategoryBox1.TabStop = false;
            // 
            // PathPanel1
            // 
            this.PathPanel1.AutoScroll = true;
            this.PathPanel1.Controls.Add(this.PathBox1);
            this.PathPanel1.Location = new System.Drawing.Point(100, 95);
            this.PathPanel1.Name = "PathPanel1";
            this.PathPanel1.Size = new System.Drawing.Size(900, 660);
            this.PathPanel1.TabIndex = 21;
            this.PathPanel1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.PathPanel1_Scroll);
            // 
            // FormPath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 762);
            this.Controls.Add(this.PathPanel1);
            this.Controls.Add(this.PathCategoryPanel1);
            this.Controls.Add(this.PathDatePanel1);
            this.Controls.Add(this.stdControlPat11);
            this.Controls.Add(this.PathDate1);
            this.Controls.Add(this.PathMasterBox1);
            this.Controls.Add(this.FormPathSelectorButton1);
            this.Name = "FormPath";
            this.Text = "パス";
            ((System.ComponentModel.ISupportInitialize)(this.PathBox1)).EndInit();
            this.PathDatePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PathDateBox1)).EndInit();
            this.PathCategoryPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PathCategoryBox1)).EndInit();
            this.PathPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FormPathSelectorButton1;
        private System.Windows.Forms.TextBox PathMasterBox1;
        private System.Windows.Forms.PictureBox PathBox1;
        private MedicalLibrary.Boundary.StdControlPat1 stdControlPat11;
        private System.Windows.Forms.DateTimePicker PathDate1;
        private System.Windows.Forms.Panel PathDatePanel1;
        private System.Windows.Forms.Panel PathCategoryPanel1;
        private System.Windows.Forms.Panel PathPanel1;
        private System.Windows.Forms.PictureBox PathDateBox1;
        private System.Windows.Forms.PictureBox PathCategoryBox1;
    }
}