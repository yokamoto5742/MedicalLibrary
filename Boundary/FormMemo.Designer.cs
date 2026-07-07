namespace MedicalLibrary.Boundary
{
    partial class FormMemo
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
            this.stdControlPat11 = new MedicalLibrary.Boundary.StdControlPat1();
            this.RegButton1 = new System.Windows.Forms.Button();
            this.MemoBox = new System.Windows.Forms.TextBox();
            this.RegDateTimeLabel1 = new System.Windows.Forms.Label();
            this.RegStaffLabel1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // stdControlPat11
            // 
            this.stdControlPat11.Location = new System.Drawing.Point(2, 2);
            this.stdControlPat11.Mode1 = MedicalLibrary.Boundary.StdControlPat1.Mode.Normal;
            this.stdControlPat11.Name = "stdControlPat11";
            this.stdControlPat11.ReadOnly = false;
            this.stdControlPat11.Size = new System.Drawing.Size(450, 30);
            this.stdControlPat11.TabIndex = 10;
            // 
            // RegButton1
            // 
            this.RegButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RegButton1.Location = new System.Drawing.Point(375, 235);
            this.RegButton1.Name = "RegButton1";
            this.RegButton1.Size = new System.Drawing.Size(75, 23);
            this.RegButton1.TabIndex = 1;
            this.RegButton1.Text = "登録";
            this.RegButton1.UseVisualStyleBackColor = true;
            this.RegButton1.Click += new System.EventHandler(this.RegButton1_Click);
            // 
            // MemoBox
            // 
            this.MemoBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MemoBox.Location = new System.Drawing.Point(5, 35);
            this.MemoBox.Multiline = true;
            this.MemoBox.Name = "MemoBox";
            this.MemoBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MemoBox.Size = new System.Drawing.Size(445, 195);
            this.MemoBox.TabIndex = 0;
            // 
            // RegDateTimeLabel1
            // 
            this.RegDateTimeLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RegDateTimeLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.RegDateTimeLabel1.Location = new System.Drawing.Point(75, 235);
            this.RegDateTimeLabel1.Name = "RegDateTimeLabel1";
            this.RegDateTimeLabel1.Size = new System.Drawing.Size(120, 20);
            this.RegDateTimeLabel1.TabIndex = 3;
            this.RegDateTimeLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RegStaffLabel1
            // 
            this.RegStaffLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RegStaffLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.RegStaffLabel1.Location = new System.Drawing.Point(205, 235);
            this.RegStaffLabel1.Name = "RegStaffLabel1";
            this.RegStaffLabel1.Size = new System.Drawing.Size(100, 20);
            this.RegStaffLabel1.TabIndex = 4;
            this.RegStaffLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "最終更新";
            // 
            // FormMemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 262);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RegStaffLabel1);
            this.Controls.Add(this.RegDateTimeLabel1);
            this.Controls.Add(this.MemoBox);
            this.Controls.Add(this.RegButton1);
            this.Controls.Add(this.stdControlPat11);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            this.Name = "FormMemo";
            this.Text = "伝達情報";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StdControlPat1 stdControlPat11;
        private System.Windows.Forms.Button RegButton1;
        private System.Windows.Forms.TextBox MemoBox;
        private System.Windows.Forms.Label RegDateTimeLabel1;
        private System.Windows.Forms.Label RegStaffLabel1;
        private System.Windows.Forms.Label label2;
    }
}