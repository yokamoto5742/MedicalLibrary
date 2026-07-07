namespace MedicalLibrary.Boundary
{
    partial class FormBaseInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBaseInfo));
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.TabPageFixed = new System.Windows.Forms.TabPage();
            this.DateTimeLabel = new System.Windows.Forms.Label();
            this.SaveFixedButton = new System.Windows.Forms.Button();
            this.StaffLabel = new System.Windows.Forms.Label();
            this.TabPageInfection = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.ctrlAllergy11 = new MedicalLibrary.Boundary.CtrlAllergy1();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlInfection11 = new MedicalLibrary.Boundary.CtrlInfection1();
            this.stdControlPat11 = new MedicalLibrary.Boundary.StdControlPat1();
            this.TabControl1.SuspendLayout();
            this.TabPageFixed.SuspendLayout();
            this.TabPageInfection.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl1
            // 
            this.TabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl1.Controls.Add(this.TabPageFixed);
            this.TabControl1.Controls.Add(this.TabPageInfection);
            this.TabControl1.Location = new System.Drawing.Point(5, 40);
            this.TabControl1.Multiline = true;
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(815, 600);
            this.TabControl1.TabIndex = 0;
            this.TabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControl1_SelectedIndexChanged);
            // 
            // TabPageFixed
            // 
            this.TabPageFixed.BackColor = System.Drawing.SystemColors.Control;
            this.TabPageFixed.Controls.Add(this.DateTimeLabel);
            this.TabPageFixed.Controls.Add(this.SaveFixedButton);
            this.TabPageFixed.Controls.Add(this.StaffLabel);
            this.TabPageFixed.Location = new System.Drawing.Point(4, 22);
            this.TabPageFixed.Name = "TabPageFixed";
            this.TabPageFixed.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageFixed.Size = new System.Drawing.Size(807, 574);
            this.TabPageFixed.TabIndex = 0;
            this.TabPageFixed.Text = "固定情報";
            // 
            // DateTimeLabel
            // 
            this.DateTimeLabel.BackColor = System.Drawing.SystemColors.Info;
            this.DateTimeLabel.Location = new System.Drawing.Point(165, 10);
            this.DateTimeLabel.Name = "DateTimeLabel";
            this.DateTimeLabel.Size = new System.Drawing.Size(120, 16);
            this.DateTimeLabel.TabIndex = 2;
            this.DateTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SaveFixedButton
            // 
            this.SaveFixedButton.Location = new System.Drawing.Point(300, 5);
            this.SaveFixedButton.Name = "SaveFixedButton";
            this.SaveFixedButton.Size = new System.Drawing.Size(75, 23);
            this.SaveFixedButton.TabIndex = 1;
            this.SaveFixedButton.Tag = "Fixed";
            this.SaveFixedButton.Text = "登録";
            this.SaveFixedButton.UseVisualStyleBackColor = true;
            this.SaveFixedButton.Click += new System.EventHandler(this.SaveFixedButton_Click);
            // 
            // StaffLabel
            // 
            this.StaffLabel.BackColor = System.Drawing.SystemColors.Info;
            this.StaffLabel.Location = new System.Drawing.Point(60, 10);
            this.StaffLabel.Name = "StaffLabel";
            this.StaffLabel.Size = new System.Drawing.Size(100, 16);
            this.StaffLabel.TabIndex = 0;
            this.StaffLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TabPageInfection
            // 
            this.TabPageInfection.BackColor = System.Drawing.SystemColors.Control;
            this.TabPageInfection.Controls.Add(this.label2);
            this.TabPageInfection.Controls.Add(this.ctrlAllergy11);
            this.TabPageInfection.Controls.Add(this.label1);
            this.TabPageInfection.Controls.Add(this.ctrlInfection11);
            this.TabPageInfection.Location = new System.Drawing.Point(4, 22);
            this.TabPageInfection.Name = "TabPageInfection";
            this.TabPageInfection.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageInfection.Size = new System.Drawing.Size(807, 574);
            this.TabPageInfection.TabIndex = 1;
            this.TabPageInfection.Text = "感染・禁忌";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 230);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "禁忌";
            // 
            // ctrlAllergy11
            // 
            this.ctrlAllergy11.Location = new System.Drawing.Point(5, 245);
            this.ctrlAllergy11.Name = "ctrlAllergy11";
            this.ctrlAllergy11.ReadOnly = true;
            this.ctrlAllergy11.Size = new System.Drawing.Size(580, 300);
            this.ctrlAllergy11.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "感染症";
            // 
            // ctrlInfection11
            // 
            this.ctrlInfection11.Location = new System.Drawing.Point(5, 25);
            this.ctrlInfection11.Name = "ctrlInfection11";
            this.ctrlInfection11.Size = new System.Drawing.Size(580, 200);
            this.ctrlInfection11.TabIndex = 0;
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
            // FormBaseInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 642);
            this.Controls.Add(this.TabControl1);
            this.Controls.Add(this.stdControlPat11);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormBaseInfo";
            this.Text = "患者基本情報";
            this.Load += new System.EventHandler(this.FormBaseInfo_Load);
            this.TabControl1.ResumeLayout(false);
            this.TabPageFixed.ResumeLayout(false);
            this.TabPageInfection.ResumeLayout(false);
            this.TabPageInfection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl1;
        private System.Windows.Forms.TabPage TabPageFixed;
        private MedicalLibrary.Boundary.StdControlPat1 stdControlPat11;
        private System.Windows.Forms.Label DateTimeLabel;
        private System.Windows.Forms.Button SaveFixedButton;
        private System.Windows.Forms.Label StaffLabel;
        private System.Windows.Forms.TabPage TabPageInfection;
        private MedicalLibrary.Boundary.CtrlInfection1 ctrlInfection11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private MedicalLibrary.Boundary.CtrlAllergy1 ctrlAllergy11;
    }
}