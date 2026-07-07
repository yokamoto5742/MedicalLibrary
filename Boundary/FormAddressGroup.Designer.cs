namespace MedicalLibrary.Boundary
{
    partial class FormAddressGroup
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
            this.ListBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ListView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.NameBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SaveButton1 = new System.Windows.Forms.Button();
            this.ClearButton1 = new System.Windows.Forms.Button();
            this.DeleteButton1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListBox1
            // 
            this.ListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListBox1.FormattingEnabled = true;
            this.ListBox1.ItemHeight = 12;
            this.ListBox1.Location = new System.Drawing.Point(5, 25);
            this.ListBox1.Name = "ListBox1";
            this.ListBox1.Size = new System.Drawing.Size(200, 232);
            this.ListBox1.TabIndex = 0;
            this.ListBox1.SelectedIndexChanged += new System.EventHandler(this.ListBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "グループ";
            // 
            // ListView1
            // 
            this.ListView1.AllowUserToAddRows = false;
            this.ListView1.AllowUserToDeleteRows = false;
            this.ListView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListView1.ContextMenuStrip = this.contextMenuStrip1;
            this.ListView1.Location = new System.Drawing.Point(210, 25);
            this.ListView1.MultiSelect = false;
            this.ListView1.Name = "ListView1";
            this.ListView1.ReadOnly = true;
            this.ListView1.RowHeadersVisible = false;
            this.ListView1.RowTemplate.Height = 21;
            this.ListView1.Size = new System.Drawing.Size(250, 175);
            this.ListView1.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddMenuItem1,
            this.DeleteMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 48);
            // 
            // AddMenuItem1
            // 
            this.AddMenuItem1.Name = "AddMenuItem1";
            this.AddMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.AddMenuItem1.Text = "追加";
            this.AddMenuItem1.Click += new System.EventHandler(this.AddMenuItem1_Click);
            // 
            // DeleteMenuItem1
            // 
            this.DeleteMenuItem1.Name = "DeleteMenuItem1";
            this.DeleteMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.DeleteMenuItem1.Text = "削除";
            this.DeleteMenuItem1.Click += new System.EventHandler(this.DeleteMenuItem1_Click);
            // 
            // NameBox1
            // 
            this.NameBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NameBox1.Location = new System.Drawing.Point(240, 4);
            this.NameBox1.MaxLength = 24;
            this.NameBox1.Name = "NameBox1";
            this.NameBox1.Size = new System.Drawing.Size(220, 19);
            this.NameBox1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(210, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "名称";
            // 
            // SaveButton1
            // 
            this.SaveButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton1.Location = new System.Drawing.Point(210, 234);
            this.SaveButton1.Name = "SaveButton1";
            this.SaveButton1.Size = new System.Drawing.Size(90, 23);
            this.SaveButton1.TabIndex = 5;
            this.SaveButton1.Text = "グループ登録";
            this.SaveButton1.UseVisualStyleBackColor = true;
            this.SaveButton1.Click += new System.EventHandler(this.SaveButton1_Click);
            // 
            // ClearButton1
            // 
            this.ClearButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearButton1.Location = new System.Drawing.Point(305, 234);
            this.ClearButton1.Name = "ClearButton1";
            this.ClearButton1.Size = new System.Drawing.Size(60, 23);
            this.ClearButton1.TabIndex = 6;
            this.ClearButton1.Text = "クリア";
            this.ClearButton1.UseVisualStyleBackColor = true;
            this.ClearButton1.Click += new System.EventHandler(this.ClearButton1_Click);
            // 
            // DeleteButton1
            // 
            this.DeleteButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteButton1.Location = new System.Drawing.Point(370, 234);
            this.DeleteButton1.Name = "DeleteButton1";
            this.DeleteButton1.Size = new System.Drawing.Size(90, 23);
            this.DeleteButton1.TabIndex = 7;
            this.DeleteButton1.Text = "グループ削除";
            this.DeleteButton1.UseVisualStyleBackColor = true;
            this.DeleteButton1.Click += new System.EventHandler(this.DeleteButton1_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(210, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(238, 24);
            this.label3.TabIndex = 8;
            this.label3.Text = "メンバーの追加・削除は右クリックで行ってください。\r\n最後に「グループ登録」ボタンをクリックしてください。";
            // 
            // FormAddressGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 262);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DeleteButton1);
            this.Controls.Add(this.ClearButton1);
            this.Controls.Add(this.SaveButton1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NameBox1);
            this.Controls.Add(this.ListView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListBox1);
            this.Name = "FormAddressGroup";
            this.Text = "アドレス帳";
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox ListBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView ListView1;
        private System.Windows.Forms.TextBox NameBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SaveButton1;
        private System.Windows.Forms.Button ClearButton1;
        private System.Windows.Forms.Button DeleteButton1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem DeleteMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem AddMenuItem1;
        private System.Windows.Forms.Label label3;
    }
}