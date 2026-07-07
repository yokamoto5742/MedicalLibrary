namespace MedicalLibrary.Boundary
{
    partial class FormByotoLayout
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
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.ByotoBox3 = new System.Windows.Forms.PictureBox();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.ByotoBox4 = new System.Windows.Forms.PictureBox();
            this.TabPage5 = new System.Windows.Forms.TabPage();
            this.ByotoBox5 = new System.Windows.Forms.PictureBox();
            this.TabControl1.SuspendLayout();
            this.TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ByotoBox3)).BeginInit();
            this.TabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ByotoBox4)).BeginInit();
            this.TabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ByotoBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // TabControl1
            // 
            this.TabControl1.Controls.Add(this.TabPage3);
            this.TabControl1.Controls.Add(this.TabPage4);
            this.TabControl1.Controls.Add(this.TabPage5);
            this.TabControl1.Location = new System.Drawing.Point(5, 12);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(997, 700);
            this.TabControl1.TabIndex = 1;
            // 
            // TabPage3
            // 
            this.TabPage3.AutoScroll = true;
            this.TabPage3.Controls.Add(this.ByotoBox3);
            this.TabPage3.Location = new System.Drawing.Point(4, 22);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage3.Size = new System.Drawing.Size(989, 674);
            this.TabPage3.TabIndex = 0;
            this.TabPage3.Text = "わかば";
            this.TabPage3.UseVisualStyleBackColor = true;
            // 
            // ByotoBox3
            // 
            this.ByotoBox3.Location = new System.Drawing.Point(6, 6);
            this.ByotoBox3.Name = "ByotoBox3";
            this.ByotoBox3.Size = new System.Drawing.Size(980, 662);
            this.ByotoBox3.TabIndex = 0;
            this.ByotoBox3.TabStop = false;
            // 
            // TabPage4
            // 
            this.TabPage4.AutoScroll = true;
            this.TabPage4.Controls.Add(this.ByotoBox4);
            this.TabPage4.Location = new System.Drawing.Point(4, 22);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage4.Size = new System.Drawing.Size(989, 674);
            this.TabPage4.TabIndex = 1;
            this.TabPage4.Text = "さくら";
            this.TabPage4.UseVisualStyleBackColor = true;
            // 
            // ByotoBox4
            // 
            this.ByotoBox4.Location = new System.Drawing.Point(6, 6);
            this.ByotoBox4.Name = "ByotoBox4";
            this.ByotoBox4.Size = new System.Drawing.Size(980, 662);
            this.ByotoBox4.TabIndex = 1;
            this.ByotoBox4.TabStop = false;
            // 
            // TabPage5
            // 
            this.TabPage5.AutoScroll = true;
            this.TabPage5.Controls.Add(this.ByotoBox5);
            this.TabPage5.Location = new System.Drawing.Point(4, 22);
            this.TabPage5.Name = "TabPage5";
            this.TabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage5.Size = new System.Drawing.Size(989, 674);
            this.TabPage5.TabIndex = 2;
            this.TabPage5.Text = "あやめ";
            this.TabPage5.UseVisualStyleBackColor = true;
            // 
            // ByotoBox5
            // 
            this.ByotoBox5.Location = new System.Drawing.Point(6, 6);
            this.ByotoBox5.Name = "ByotoBox5";
            this.ByotoBox5.Size = new System.Drawing.Size(980, 662);
            this.ByotoBox5.TabIndex = 2;
            this.ByotoBox5.TabStop = false;
            // 
            // FormByotoLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.TabControl1);
            this.Name = "FormByotoLayout";
            this.Text = "病棟レイアウト";
            this.Load += new System.EventHandler(this.FormByotoLayout_Load);
            this.TabControl1.ResumeLayout(false);
            this.TabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ByotoBox3)).EndInit();
            this.TabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ByotoBox4)).EndInit();
            this.TabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ByotoBox5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ByotoBox3;
        private System.Windows.Forms.TabControl TabControl1;
        private System.Windows.Forms.TabPage TabPage3;
        private System.Windows.Forms.TabPage TabPage4;
        private System.Windows.Forms.PictureBox ByotoBox4;
        private System.Windows.Forms.TabPage TabPage5;
        private System.Windows.Forms.PictureBox ByotoBox5;
    }
}