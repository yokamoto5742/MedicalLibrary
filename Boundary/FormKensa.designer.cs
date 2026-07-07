namespace MedicalLibrary.Boundary
{
    partial class FormKensa
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
            this.components = new System.ComponentModel.Container();
            this.KensaDataView1 = new System.Windows.Forms.DataGridView();
            this.ContextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CopyItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KensaDatePanel = new System.Windows.Forms.Panel();
            this.stdControlPat11 = new MedicalLibrary.Boundary.StdControlPat1();
            ((System.ComponentModel.ISupportInitialize)(this.KensaDataView1)).BeginInit();
            this.ContextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // KensaDataView1
            // 
            this.KensaDataView1.AllowUserToAddRows = false;
            this.KensaDataView1.AllowUserToDeleteRows = false;
            this.KensaDataView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.KensaDataView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.KensaDataView1.ContextMenuStrip = this.ContextMenuStrip1;
            this.KensaDataView1.Location = new System.Drawing.Point(200, 50);
            this.KensaDataView1.Name = "KensaDataView1";
            this.KensaDataView1.ReadOnly = true;
            this.KensaDataView1.RowHeadersVisible = false;
            this.KensaDataView1.RowTemplate.Height = 21;
            this.KensaDataView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.KensaDataView1.Size = new System.Drawing.Size(796, 500);
            this.KensaDataView1.TabIndex = 0;
            this.KensaDataView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KensaDataView1_KeyDown);
            // 
            // ContextMenuStrip1
            // 
            this.ContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyItem});
            this.ContextMenuStrip1.Name = "ContextMenuStrip1";
            this.ContextMenuStrip1.Size = new System.Drawing.Size(113, 26);
            // 
            // CopyItem
            // 
            this.CopyItem.Name = "CopyItem";
            this.CopyItem.Size = new System.Drawing.Size(112, 22);
            this.CopyItem.Text = "コピー";
            this.CopyItem.Click += new System.EventHandler(this.CopyItem_Click);
            // 
            // KensaDatePanel
            // 
            this.KensaDatePanel.AutoScroll = true;
            this.KensaDatePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.KensaDatePanel.Location = new System.Drawing.Point(10, 50);
            this.KensaDatePanel.Name = "KensaDatePanel";
            this.KensaDatePanel.Size = new System.Drawing.Size(180, 500);
            this.KensaDatePanel.TabIndex = 22;
            // 
            // stdControlPat11
            // 
            this.stdControlPat11.Location = new System.Drawing.Point(10, 5);
            this.stdControlPat11.Mode1 = MedicalLibrary.Boundary.StdControlPat1.Mode.Normal;
            this.stdControlPat11.Name = "stdControlPat11";
            this.stdControlPat11.ReadOnly = false;
            this.stdControlPat11.Size = new System.Drawing.Size(500, 30);
            this.stdControlPat11.TabIndex = 23;
            // 
            // FormKensa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 562);
            this.Controls.Add(this.KensaDatePanel);
            this.Controls.Add(this.KensaDataView1);
            this.Controls.Add(this.stdControlPat11);
            this.Name = "FormKensa";
            this.Text = "検査結果";
            this.Load += new System.EventHandler(this.FormKensa_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormKensa_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.KensaDataView1)).EndInit();
            this.ContextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView KensaDataView1;
        private System.Windows.Forms.Panel KensaDatePanel;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem CopyItem;
        private MedicalLibrary.Boundary.StdControlPat1 stdControlPat11;
    }
}