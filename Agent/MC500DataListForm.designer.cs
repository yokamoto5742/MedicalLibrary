namespace MedicalLibrary.Agent
{
    partial class MC500DataListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MC500DataListForm));
            this.MC500Panels1 = new MedicalLibrary.Agent.MC500Panels();
            this.PtIdBox = new System.Windows.Forms.TextBox();
            this.ShowButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // MC500Panels1
            // 
            this.MC500Panels1.AutoScroll = true;
            this.MC500Panels1.Location = new System.Drawing.Point(12, 40);
            this.MC500Panels1.Name = "MC500Panels1";
            this.MC500Panels1.Size = new System.Drawing.Size(715, 442);
            this.MC500Panels1.TabIndex = 0;
            // 
            // PtIdBox
            // 
            this.PtIdBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.PtIdBox.Location = new System.Drawing.Point(70, 9);
            this.PtIdBox.Name = "PtIdBox";
            this.PtIdBox.Size = new System.Drawing.Size(100, 19);
            this.PtIdBox.TabIndex = 1;
            this.PtIdBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PtIdBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PtIdBox_KeyDown);
            // 
            // ShowButton
            // 
            this.ShowButton.Location = new System.Drawing.Point(187, 7);
            this.ShowButton.Name = "ShowButton";
            this.ShowButton.Size = new System.Drawing.Size(59, 23);
            this.ShowButton.TabIndex = 2;
            this.ShowButton.Text = "表示";
            this.ShowButton.UseVisualStyleBackColor = true;
            this.ShowButton.Click += new System.EventHandler(this.ShowButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "患者ID";
            // 
            // FormDataList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(734, 502);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ShowButton);
            this.Controls.Add(this.PtIdBox);
            this.Controls.Add(this.MC500Panels1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDataList";
            this.Text = "MC500データ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MedicalLibrary.Agent.MC500Panels MC500Panels1;
        private System.Windows.Forms.TextBox PtIdBox;
        private System.Windows.Forms.Button ShowButton;
        private System.Windows.Forms.Label label1;
    }
}