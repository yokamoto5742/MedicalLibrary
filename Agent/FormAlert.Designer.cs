namespace MedicalLibrary.Agent
{
    partial class FormAlert
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
            this.SaveButton = new System.Windows.Forms.Button();
            this.StatusBox = new System.Windows.Forms.CheckBox();
            this.InfoLabel = new System.Windows.Forms.Label();
            this.ContBox = new System.Windows.Forms.TextBox();
            this.AlertPanel = new System.Windows.Forms.Panel();
            this.AlertLabel = new System.Windows.Forms.Label();
            this.stdControlPat11 = new MedicalLibrary.Boundary.StdControlPat1();
            this.AlertPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SaveButton.Location = new System.Drawing.Point(500, 235);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 25);
            this.SaveButton.TabIndex = 6;
            this.SaveButton.Text = "登録";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // StatusBox
            // 
            this.StatusBox.AutoSize = true;
            this.StatusBox.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.StatusBox.Location = new System.Drawing.Point(420, 240);
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.Size = new System.Drawing.Size(65, 17);
            this.StatusBox.TabIndex = 5;
            this.StatusBox.Text = "非表示";
            this.StatusBox.UseVisualStyleBackColor = true;
            // 
            // InfoLabel
            // 
            this.InfoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.InfoLabel.AutoEllipsis = true;
            this.InfoLabel.BackColor = System.Drawing.Color.White;
            this.InfoLabel.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.InfoLabel.Location = new System.Drawing.Point(450, 75);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(125, 15);
            this.InfoLabel.TabIndex = 4;
            this.InfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ContBox
            // 
            this.ContBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContBox.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ContBox.Location = new System.Drawing.Point(8, 140);
            this.ContBox.MaxLength = 200;
            this.ContBox.Multiline = true;
            this.ContBox.Name = "ContBox";
            this.ContBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ContBox.Size = new System.Drawing.Size(570, 90);
            this.ContBox.TabIndex = 2;
            // 
            // AlertPanel
            // 
            this.AlertPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AlertPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.AlertPanel.Controls.Add(this.InfoLabel);
            this.AlertPanel.Controls.Add(this.AlertLabel);
            this.AlertPanel.Location = new System.Drawing.Point(0, 0);
            this.AlertPanel.Name = "AlertPanel";
            this.AlertPanel.Size = new System.Drawing.Size(585, 100);
            this.AlertPanel.TabIndex = 1;
            // 
            // AlertLabel
            // 
            this.AlertLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AlertLabel.AutoEllipsis = true;
            this.AlertLabel.BackColor = System.Drawing.Color.White;
            this.AlertLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.AlertLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.AlertLabel.Location = new System.Drawing.Point(8, 8);
            this.AlertLabel.Name = "AlertLabel";
            this.AlertLabel.Size = new System.Drawing.Size(570, 85);
            this.AlertLabel.TabIndex = 0;
            this.AlertLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.AlertLabel.DoubleClick += new System.EventHandler(this.AlertLabel_DoubleClick);
            // 
            // stdControlPat11
            // 
            this.stdControlPat11.Location = new System.Drawing.Point(10, 105);
            this.stdControlPat11.Mode1 = MedicalLibrary.Boundary.StdControlPat1.Mode.Normal;
            this.stdControlPat11.Name = "stdControlPat11";
            this.stdControlPat11.ReadOnly = true;
            this.stdControlPat11.Size = new System.Drawing.Size(450, 30);
            this.stdControlPat11.TabIndex = 3;
            // 
            // FormAlert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(584, 262);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.StatusBox);
            this.Controls.Add(this.stdControlPat11);
            this.Controls.Add(this.ContBox);
            this.Controls.Add(this.AlertPanel);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            this.Name = "FormAlert";
            this.Text = "要確認";
            this.Load += new System.EventHandler(this.FormAlert_Load);
            this.AlertPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label AlertLabel;
        private System.Windows.Forms.Panel AlertPanel;
        private System.Windows.Forms.TextBox ContBox;
        private Boundary.StdControlPat1 stdControlPat11;
        private System.Windows.Forms.Label InfoLabel;
        private System.Windows.Forms.CheckBox StatusBox;
        private System.Windows.Forms.Button SaveButton;
    }
}