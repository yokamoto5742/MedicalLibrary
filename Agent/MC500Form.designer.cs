namespace MedicalLibrary.Agent
{
    partial class MC500Form
    {
        /// <summary>
        /// 必要なデザイナ変数です。
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

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MC500Form));
            this.ReadButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.LoginUserBox = new System.Windows.Forms.TextBox();
            this.ReadFileView = new System.Windows.Forms.DataGridView();
            this.ReadFileStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ReadFileStripMenu1 = new System.Windows.Forms.ToolStripMenuItem();
            this.DataButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ReadFileView)).BeginInit();
            this.ReadFileStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ReadButton
            // 
            this.ReadButton.Location = new System.Drawing.Point(15, 334);
            this.ReadButton.Name = "ReadButton";
            this.ReadButton.Size = new System.Drawing.Size(75, 23);
            this.ReadButton.TabIndex = 0;
            this.ReadButton.Text = "読み込み";
            this.ReadButton.UseVisualStyleBackColor = true;
            this.ReadButton.Click += new System.EventHandler(this.ReadButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(100, 334);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(65, 23);
            this.ClearButton.TabIndex = 2;
            this.ClearButton.Text = "クリア";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(175, 334);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 3;
            this.SaveButton.Text = "データ保存";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(260, 334);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(60, 23);
            this.ExitButton.TabIndex = 4;
            this.ExitButton.Text = "終了";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(165, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "ユーザー";
            // 
            // LoginUserBox
            // 
            this.LoginUserBox.BackColor = System.Drawing.Color.LightYellow;
            this.LoginUserBox.Location = new System.Drawing.Point(219, 6);
            this.LoginUserBox.Name = "LoginUserBox";
            this.LoginUserBox.ReadOnly = true;
            this.LoginUserBox.Size = new System.Drawing.Size(100, 19);
            this.LoginUserBox.TabIndex = 6;
            this.LoginUserBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ReadFileView
            // 
            this.ReadFileView.AllowUserToAddRows = false;
            this.ReadFileView.AllowUserToDeleteRows = false;
            this.ReadFileView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ReadFileView.ContextMenuStrip = this.ReadFileStrip;
            this.ReadFileView.Location = new System.Drawing.Point(15, 30);
            this.ReadFileView.Name = "ReadFileView";
            this.ReadFileView.RowHeadersVisible = false;
            this.ReadFileView.RowHeadersWidth = 21;
            this.ReadFileView.RowTemplate.Height = 21;
            this.ReadFileView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ReadFileView.Size = new System.Drawing.Size(305, 300);
            this.ReadFileView.TabIndex = 7;
            // 
            // ReadFileStrip
            // 
            this.ReadFileStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ReadFileStripMenu1});
            this.ReadFileStrip.Name = "ReadFileStrip";
            this.ReadFileStrip.Size = new System.Drawing.Size(101, 26);
            // 
            // ReadFileStripMenu1
            // 
            this.ReadFileStripMenu1.Name = "ReadFileStripMenu1";
            this.ReadFileStripMenu1.Size = new System.Drawing.Size(100, 22);
            this.ReadFileStripMenu1.Text = "削除";
            this.ReadFileStripMenu1.Click += new System.EventHandler(this.ReadFileStripMenu1_Click);
            // 
            // DataButton
            // 
            this.DataButton.Location = new System.Drawing.Point(15, 4);
            this.DataButton.Name = "DataButton";
            this.DataButton.Size = new System.Drawing.Size(75, 23);
            this.DataButton.TabIndex = 8;
            this.DataButton.Text = "データ参照";
            this.DataButton.UseVisualStyleBackColor = true;
            this.DataButton.Click += new System.EventHandler(this.DataButton_Click);
            // 
            // FormMC500
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 362);
            this.Controls.Add(this.ReadFileView);
            this.Controls.Add(this.LoginUserBox);
            this.Controls.Add(this.DataButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.ReadButton);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMC500";
            this.Text = "MC500取り込み";
            this.Load += new System.EventHandler(this.MC500Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ReadFileView)).EndInit();
            this.ReadFileStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ReadButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox LoginUserBox;
        private System.Windows.Forms.DataGridView ReadFileView;
        private System.Windows.Forms.Button DataButton;
        private System.Windows.Forms.ContextMenuStrip ReadFileStrip;
        private System.Windows.Forms.ToolStripMenuItem ReadFileStripMenu1;
    }
}

