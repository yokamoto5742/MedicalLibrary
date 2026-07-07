namespace MedicalLibrary.Boundary
{
    partial class FormInCal2
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
            MedicalLibrary.Entity.PatIn patIn1 = new MedicalLibrary.Entity.PatIn();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInCal2));
            this.DatePicker1 = new System.Windows.Forms.DateTimePicker();
            this.PrevButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.CalPanel = new System.Windows.Forms.Panel();
            this.InHistoryBox2 = new MedicalLibrary.Boundary.CtrlInHistoryBox2();
            this.stdControlPat11 = new MedicalLibrary.Boundary.StdControlPat1();
            this.SuspendLayout();
            // 
            // DatePicker1
            // 
            this.DatePicker1.CustomFormat = "yyyy年M月";
            this.DatePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatePicker1.Location = new System.Drawing.Point(700, 10);
            this.DatePicker1.MaxDate = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
            this.DatePicker1.MinDate = new System.DateTime(1980, 1, 1, 0, 0, 0, 0);
            this.DatePicker1.Name = "DatePicker1";
            this.DatePicker1.Size = new System.Drawing.Size(110, 19);
            this.DatePicker1.TabIndex = 0;
            this.DatePicker1.ValueChanged += new System.EventHandler(this.DatePicker1_ValueChanged);
            // 
            // PrevButton
            // 
            this.PrevButton.Location = new System.Drawing.Point(820, 8);
            this.PrevButton.Name = "PrevButton";
            this.PrevButton.Size = new System.Drawing.Size(40, 23);
            this.PrevButton.TabIndex = 1;
            this.PrevButton.Text = "前";
            this.PrevButton.UseVisualStyleBackColor = true;
            this.PrevButton.Click += new System.EventHandler(this.PrevButton_Click);
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(865, 8);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(40, 23);
            this.NextButton.TabIndex = 2;
            this.NextButton.Text = "次";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // CalPanel
            // 
            this.CalPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CalPanel.AutoScroll = true;
            this.CalPanel.Location = new System.Drawing.Point(5, 40);
            this.CalPanel.Name = "CalPanel";
            this.CalPanel.Size = new System.Drawing.Size(935, 760);
            this.CalPanel.TabIndex = 3;
            this.CalPanel.SizeChanged += new System.EventHandler(this.CalPanel_SizeChanged);
            // 
            // InHistoryBox2
            // 
            this.InHistoryBox2.ClearButtonVisible = true;
            this.InHistoryBox2.Location = new System.Drawing.Point(460, 8);
            this.InHistoryBox2.Name = "InHistoryBox2";
            patIn1.Age = "";
            patIn1.Birth = "";
            patIn1.Id = "";
            patIn1.Kana = "";
            patIn1.Name = "";
            this.InHistoryBox2.PatIn1 = patIn1;
            this.InHistoryBox2.PtId = "";
            this.InHistoryBox2.Size = new System.Drawing.Size(235, 25);
            this.InHistoryBox2.TabIndex = 4;
            this.InHistoryBox2.ValueChanged += new System.EventHandler<System.EventArgs>(this.InHistoryBox2_ValueChanged);
            // 
            // stdControlPat11
            // 
            this.stdControlPat11.Location = new System.Drawing.Point(5, 5);
            this.stdControlPat11.Mode1 = MedicalLibrary.Boundary.StdControlPat1.Mode.Normal;
            this.stdControlPat11.Name = "stdControlPat11";
            this.stdControlPat11.ReadOnly = false;
            this.stdControlPat11.Size = new System.Drawing.Size(450, 30);
            this.stdControlPat11.TabIndex = 5;
            // 
            // FormInCal2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 802);
            this.Controls.Add(this.stdControlPat11);
            this.Controls.Add(this.InHistoryBox2);
            this.Controls.Add(this.CalPanel);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.PrevButton);
            this.Controls.Add(this.DatePicker1);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormInCal2";
            this.Text = "入院カレンダー";
            this.Load += new System.EventHandler(this.FormInCal2_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker DatePicker1;
        private System.Windows.Forms.Button PrevButton;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Panel CalPanel;
        private CtrlInHistoryBox2 InHistoryBox2;
        private StdControlPat1 stdControlPat11;
    }
}