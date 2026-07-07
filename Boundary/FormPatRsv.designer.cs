namespace MedicalLibrary.Boundary
{
    partial class FormPatRsv
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPatRsv));
            this.ListView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.PastBox = new System.Windows.Forms.CheckBox();
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
            this.ListView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListView1.Location = new System.Drawing.Point(5, 30);
            this.ListView1.Name = "ListView1";
            this.ListView1.ReadOnly = true;
            this.ListView1.RowHeadersVisible = false;
            this.ListView1.RowTemplate.Height = 21;
            this.ListView1.Size = new System.Drawing.Size(575, 230);
            this.ListView1.TabIndex = 0;
            this.ListView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListView1_CellDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(20, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "赤字は本日の予約です。";
            // 
            // PastBox
            // 
            this.PastBox.AutoSize = true;
            this.PastBox.Location = new System.Drawing.Point(400, 10);
            this.PastBox.Name = "PastBox";
            this.PastBox.Size = new System.Drawing.Size(140, 16);
            this.PastBox.TabIndex = 2;
            this.PastBox.Text = "過去の予約も表示する";
            this.PastBox.UseVisualStyleBackColor = true;
            this.PastBox.CheckStateChanged += new System.EventHandler(this.PastBox_CheckStateChanged);
            // 
            // FormPatRsv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 262);
            this.Controls.Add(this.PastBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListView1);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPatRsv";
            this.Text = "予約";
            this.Load += new System.EventHandler(this.FormPatRsv_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ListView1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox PastBox;
    }
}