namespace MedicalLibrary.Boundary
{
    partial class FormDiagNan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDiagNan));
            this.ListView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.FilterBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ListView)).BeginInit();
            this.SuspendLayout();
            // 
            // ListView
            // 
            this.ListView.AllowUserToAddRows = false;
            this.ListView.AllowUserToDeleteRows = false;
            this.ListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListView.Location = new System.Drawing.Point(5, 30);
            this.ListView.MultiSelect = false;
            this.ListView.Name = "ListView";
            this.ListView.ReadOnly = true;
            this.ListView.RowHeadersVisible = false;
            this.ListView.RowTemplate.Height = 21;
            this.ListView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ListView.Size = new System.Drawing.Size(455, 230);
            this.ListView.TabIndex = 0;
            this.ListView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListView_CellDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(250, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "絞込み";
            // 
            // FilterBox
            // 
            this.FilterBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FilterBox.Location = new System.Drawing.Point(295, 7);
            this.FilterBox.Name = "FilterBox";
            this.FilterBox.Size = new System.Drawing.Size(165, 19);
            this.FilterBox.TabIndex = 2;
            this.FilterBox.TextChanged += new System.EventHandler(this.FilterBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "該当のものを選んでダブルクリックしてください";
            // 
            // FormDiagNan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 262);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FilterBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDiagNan";
            this.Text = "指定難病";
            this.Load += new System.EventHandler(this.FormDiagNan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ListView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FilterBox;
        private System.Windows.Forms.Label label2;
    }
}