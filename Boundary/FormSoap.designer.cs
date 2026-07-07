namespace MedicalLibrary.Boundary
{
    partial class FormSoap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSoap));
            this.CritDate1 = new System.Windows.Forms.DateTimePicker();
            this.SoapDataPanel = new System.Windows.Forms.Panel();
            this.ShowButton1 = new System.Windows.Forms.Button();
            this.SoapDatePanel = new System.Windows.Forms.Panel();
            this.SoapWrite1 = new MedicalLibrary.Boundary.CtrlSoapWrite1();
            this.stdControlPat11 = new MedicalLibrary.Boundary.StdControlPat1();
            this.KarteTemplateButton1 = new System.Windows.Forms.Button();
            this.SoapMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ModifyMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearButton1 = new System.Windows.Forms.Button();
            this.SaveButton1 = new System.Windows.Forms.Button();
            this.SoapMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CritDate1
            // 
            this.CritDate1.CustomFormat = "yyyy/MM/dd (ddd)";
            this.CritDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.CritDate1.Location = new System.Drawing.Point(470, 12);
            this.CritDate1.Name = "CritDate1";
            this.CritDate1.Size = new System.Drawing.Size(128, 19);
            this.CritDate1.TabIndex = 26;
            // 
            // SoapDataPanel
            // 
            this.SoapDataPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SoapDataPanel.AutoScroll = true;
            this.SoapDataPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SoapDataPanel.Location = new System.Drawing.Point(680, 38);
            this.SoapDataPanel.Name = "SoapDataPanel";
            this.SoapDataPanel.Size = new System.Drawing.Size(500, 712);
            this.SoapDataPanel.TabIndex = 24;
            this.SoapDataPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SoapDataPanel_MouseClick);
            // 
            // ShowButton1
            // 
            this.ShowButton1.Location = new System.Drawing.Point(605, 10);
            this.ShowButton1.Name = "ShowButton1";
            this.ShowButton1.Size = new System.Drawing.Size(60, 23);
            this.ShowButton1.TabIndex = 22;
            this.ShowButton1.Text = "表示";
            this.ShowButton1.UseVisualStyleBackColor = true;
            this.ShowButton1.Click += new System.EventHandler(this.ShowButton1_Click);
            // 
            // SoapDatePanel
            // 
            this.SoapDatePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SoapDatePanel.AutoScroll = true;
            this.SoapDatePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SoapDatePanel.Location = new System.Drawing.Point(455, 38);
            this.SoapDatePanel.Name = "SoapDatePanel";
            this.SoapDatePanel.Size = new System.Drawing.Size(220, 712);
            this.SoapDatePanel.TabIndex = 0;
            // 
            // SoapWrite1
            // 
            this.SoapWrite1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SoapWrite1.AutoScroll = true;
            this.SoapWrite1.BackColor = System.Drawing.Color.LightGreen;
            this.SoapWrite1.InOut = "1";
            this.SoapWrite1.Location = new System.Drawing.Point(0, 40);
            this.SoapWrite1.Name = "SoapWrite1";
            this.SoapWrite1.Size = new System.Drawing.Size(450, 710);
            this.SoapWrite1.TabIndex = 29;
            // 
            // stdControlPat11
            // 
            this.stdControlPat11.Location = new System.Drawing.Point(5, 5);
            this.stdControlPat11.Mode1 = MedicalLibrary.Boundary.StdControlPat1.Mode.Normal;
            this.stdControlPat11.Name = "stdControlPat11";
            this.stdControlPat11.ReadOnly = false;
            this.stdControlPat11.Size = new System.Drawing.Size(450, 30);
            this.stdControlPat11.TabIndex = 27;
            // 
            // KarteTemplateButton1
            // 
            this.KarteTemplateButton1.Location = new System.Drawing.Point(680, 10);
            this.KarteTemplateButton1.Name = "KarteTemplateButton1";
            this.KarteTemplateButton1.Size = new System.Drawing.Size(75, 23);
            this.KarteTemplateButton1.TabIndex = 30;
            this.KarteTemplateButton1.Text = "テンプレート";
            this.KarteTemplateButton1.UseVisualStyleBackColor = true;
            this.KarteTemplateButton1.Click += new System.EventHandler(this.KarteTemplateButton1_Click);
            // 
            // SoapMenuStrip1
            // 
            this.SoapMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModifyMenuItem1});
            this.SoapMenuStrip1.Name = "SoapMenuStrip1";
            this.SoapMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // ModifyMenuItem1
            // 
            this.ModifyMenuItem1.Name = "ModifyMenuItem1";
            this.ModifyMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.ModifyMenuItem1.Text = "修正";
            this.ModifyMenuItem1.Click += new System.EventHandler(this.ModifyMenuItem1_Click);
            // 
            // ClearButton1
            // 
            this.ClearButton1.Location = new System.Drawing.Point(760, 10);
            this.ClearButton1.Name = "ClearButton1";
            this.ClearButton1.Size = new System.Drawing.Size(50, 23);
            this.ClearButton1.TabIndex = 31;
            this.ClearButton1.Text = "クリア";
            this.ClearButton1.UseVisualStyleBackColor = true;
            this.ClearButton1.Click += new System.EventHandler(this.ClearButton1_Click);
            // 
            // SaveButton1
            // 
            this.SaveButton1.Location = new System.Drawing.Point(815, 10);
            this.SaveButton1.Name = "SaveButton1";
            this.SaveButton1.Size = new System.Drawing.Size(80, 23);
            this.SaveButton1.TabIndex = 32;
            this.SaveButton1.Text = "確定";
            this.SaveButton1.UseVisualStyleBackColor = true;
            this.SaveButton1.Click += new System.EventHandler(this.SaveButton1_Click);
            // 
            // FormSoap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 762);
            this.Controls.Add(this.SaveButton1);
            this.Controls.Add(this.ClearButton1);
            this.Controls.Add(this.KarteTemplateButton1);
            this.Controls.Add(this.SoapWrite1);
            this.Controls.Add(this.stdControlPat11);
            this.Controls.Add(this.CritDate1);
            this.Controls.Add(this.SoapDataPanel);
            this.Controls.Add(this.ShowButton1);
            this.Controls.Add(this.SoapDatePanel);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSoap";
            this.Text = "SOAP";
            this.Load += new System.EventHandler(this.FormSoap_Load);
            this.SoapMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel SoapDatePanel;
        private System.Windows.Forms.Button ShowButton1;
        private System.Windows.Forms.Panel SoapDataPanel;
        private System.Windows.Forms.DateTimePicker CritDate1;
        private MedicalLibrary.Boundary.StdControlPat1 stdControlPat11;
        private MedicalLibrary.Boundary.CtrlSoapWrite1 SoapWrite1;
        private System.Windows.Forms.Button KarteTemplateButton1;
        private System.Windows.Forms.ContextMenuStrip SoapMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ModifyMenuItem1;
        private System.Windows.Forms.Button ClearButton1;
        private System.Windows.Forms.Button SaveButton1;
    }
}