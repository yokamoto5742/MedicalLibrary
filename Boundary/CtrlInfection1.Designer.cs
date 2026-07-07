namespace MedicalLibrary.Boundary
{
    partial class CtrlInfection1
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.KensaDataView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.KensaDataView1)).BeginInit();
            this.SuspendLayout();
            // 
            // KensaDataView1
            // 
            this.KensaDataView1.AllowUserToAddRows = false;
            this.KensaDataView1.AllowUserToDeleteRows = false;
            this.KensaDataView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.KensaDataView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.KensaDataView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.KensaDataView1.Location = new System.Drawing.Point(5, 4);
            this.KensaDataView1.Name = "KensaDataView1";
            this.KensaDataView1.ReadOnly = true;
            this.KensaDataView1.RowHeadersVisible = false;
            this.KensaDataView1.RowTemplate.Height = 21;
            this.KensaDataView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.KensaDataView1.Size = new System.Drawing.Size(410, 170);
            this.KensaDataView1.TabIndex = 1;
            // 
            // CtrlInfection1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.KensaDataView1);
            this.Name = "CtrlInfection1";
            this.Size = new System.Drawing.Size(420, 180);
            this.Load += new System.EventHandler(this.CtrlInfection1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.KensaDataView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView KensaDataView1;
    }
}
