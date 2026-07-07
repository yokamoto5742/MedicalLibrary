namespace MedicalLibrary.Boundary
{
    partial class FormPreOrderAgree
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
            this.label1 = new System.Windows.Forms.Label();
            this.DatePicker1 = new System.Windows.Forms.DateTimePicker();
            this.AgreeListView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.AgreeListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(155, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "以降実施分";
            // 
            // DatePicker1
            // 
            this.DatePicker1.CustomFormat = "yyyy/MM/dd (ddd)";
            this.DatePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatePicker1.Location = new System.Drawing.Point(15, 10);
            this.DatePicker1.Name = "DatePicker1";
            this.DatePicker1.Size = new System.Drawing.Size(130, 19);
            this.DatePicker1.TabIndex = 1;
            this.DatePicker1.ValueChanged += new System.EventHandler(this.DatePicker1_ValueChanged);
            // 
            // AgreeListView1
            // 
            this.AgreeListView1.AllowUserToAddRows = false;
            this.AgreeListView1.AllowUserToDeleteRows = false;
            this.AgreeListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AgreeListView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AgreeListView1.Location = new System.Drawing.Point(12, 35);
            this.AgreeListView1.Name = "AgreeListView1";
            this.AgreeListView1.RowHeadersVisible = false;
            this.AgreeListView1.RowTemplate.Height = 21;
            this.AgreeListView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AgreeListView1.Size = new System.Drawing.Size(940, 320);
            this.AgreeListView1.TabIndex = 0;
            // 
            // FormPreOrderAgree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 362);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DatePicker1);
            this.Controls.Add(this.AgreeListView1);
            this.Name = "FormPreOrderAgree";
            this.Text = "実施承認";
            this.Load += new System.EventHandler(this.FormPreOrderAgree_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AgreeListView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView AgreeListView1;
        private System.Windows.Forms.DateTimePicker DatePicker1;
        private System.Windows.Forms.Label label1;
    }
}