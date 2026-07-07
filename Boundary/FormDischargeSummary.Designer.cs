namespace MedicalLibrary.Boundary
{
    partial class FormDischargeSummary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDischargeSummary));
            this.stdControlPat11 = new MedicalLibrary.Boundary.StdControlPat1();
            this.label1 = new System.Windows.Forms.Label();
            this.MainDiagBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ContBox1 = new System.Windows.Forms.TextBox();
            this.ContBox2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.CheckBox1 = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.CheckBox2 = new System.Windows.Forms.CheckBox();
            this.ContBox3 = new System.Windows.Forms.TextBox();
            this.ContBox4 = new System.Windows.Forms.TextBox();
            this.ContBox5 = new System.Windows.Forms.TextBox();
            this.ContBox6 = new System.Windows.Forms.TextBox();
            this.StatusDateBox = new MedicalLibrary.Boundary.CtrlDateBox2();
            this.DateTimeLabel = new System.Windows.Forms.Label();
            this.ctrlInHistoryBox21 = new MedicalLibrary.Boundary.CtrlInHistoryBox2();
            this.SuspendLayout();
            // 
            // stdControlPat11
            // 
            this.stdControlPat11.Location = new System.Drawing.Point(5, 5);
            this.stdControlPat11.Mode1 = MedicalLibrary.Boundary.StdControlPat1.Mode.Normal;
            this.stdControlPat11.Name = "stdControlPat11";
            this.stdControlPat11.ReadOnly = true;
            this.stdControlPat11.Size = new System.Drawing.Size(450, 30);
            this.stdControlPat11.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "主病名";
            // 
            // MainDiagBox
            // 
            this.MainDiagBox.Location = new System.Drawing.Point(70, 40);
            this.MainDiagBox.Name = "MainDiagBox";
            this.MainDiagBox.Size = new System.Drawing.Size(580, 19);
            this.MainDiagBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "現病歴";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 195);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "入院時\r\n所見";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 320);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 24);
            this.label4.TabIndex = 5;
            this.label4.Text = "入院時\r\n経過観察";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 445);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 24);
            this.label5.TabIndex = 6;
            this.label5.Text = "退院\r\n申し送り";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 570);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 24);
            this.label6.TabIndex = 7;
            this.label6.Text = "禁忌\r\nアレルギ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 615);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "考察";
            // 
            // ContBox1
            // 
            this.ContBox1.Location = new System.Drawing.Point(70, 65);
            this.ContBox1.Multiline = true;
            this.ContBox1.Name = "ContBox1";
            this.ContBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ContBox1.Size = new System.Drawing.Size(580, 120);
            this.ContBox1.TabIndex = 9;
            // 
            // ContBox2
            // 
            this.ContBox2.Location = new System.Drawing.Point(70, 190);
            this.ContBox2.Multiline = true;
            this.ContBox2.Name = "ContBox2";
            this.ContBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ContBox2.Size = new System.Drawing.Size(580, 120);
            this.ContBox2.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(230, 690);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "確定日";
            // 
            // CheckBox1
            // 
            this.CheckBox1.AutoSize = true;
            this.CheckBox1.Location = new System.Drawing.Point(85, 690);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(48, 16);
            this.CheckBox1.TabIndex = 12;
            this.CheckBox1.Text = "医師";
            this.CheckBox1.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 690);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 13;
            this.label9.Text = "完了確認";
            // 
            // CheckBox2
            // 
            this.CheckBox2.AutoSize = true;
            this.CheckBox2.Location = new System.Drawing.Point(150, 690);
            this.CheckBox2.Name = "CheckBox2";
            this.CheckBox2.Size = new System.Drawing.Size(60, 16);
            this.CheckBox2.TabIndex = 14;
            this.CheckBox2.Text = "管理士";
            this.CheckBox2.UseVisualStyleBackColor = true;
            // 
            // ContBox3
            // 
            this.ContBox3.Location = new System.Drawing.Point(70, 315);
            this.ContBox3.Multiline = true;
            this.ContBox3.Name = "ContBox3";
            this.ContBox3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ContBox3.Size = new System.Drawing.Size(580, 120);
            this.ContBox3.TabIndex = 15;
            // 
            // ContBox4
            // 
            this.ContBox4.Location = new System.Drawing.Point(70, 440);
            this.ContBox4.Multiline = true;
            this.ContBox4.Name = "ContBox4";
            this.ContBox4.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ContBox4.Size = new System.Drawing.Size(580, 120);
            this.ContBox4.TabIndex = 16;
            // 
            // ContBox5
            // 
            this.ContBox5.Location = new System.Drawing.Point(70, 565);
            this.ContBox5.Multiline = true;
            this.ContBox5.Name = "ContBox5";
            this.ContBox5.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ContBox5.Size = new System.Drawing.Size(580, 40);
            this.ContBox5.TabIndex = 17;
            // 
            // ContBox6
            // 
            this.ContBox6.Location = new System.Drawing.Point(70, 610);
            this.ContBox6.Multiline = true;
            this.ContBox6.Name = "ContBox6";
            this.ContBox6.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ContBox6.Size = new System.Drawing.Size(580, 70);
            this.ContBox6.TabIndex = 18;
            // 
            // StatusDateBox
            // 
            this.StatusDateBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.StatusDateBox.Location = new System.Drawing.Point(280, 685);
            this.StatusDateBox.MaxLength = 8;
            this.StatusDateBox.Name = "StatusDateBox";
            this.StatusDateBox.Size = new System.Drawing.Size(80, 19);
            this.StatusDateBox.TabIndex = 19;
            // 
            // DateTimeLabel
            // 
            this.DateTimeLabel.BackColor = System.Drawing.Color.LightYellow;
            this.DateTimeLabel.Location = new System.Drawing.Point(380, 685);
            this.DateTimeLabel.Name = "DateTimeLabel";
            this.DateTimeLabel.Size = new System.Drawing.Size(200, 20);
            this.DateTimeLabel.TabIndex = 21;
            this.DateTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ctrlInHistoryBox21
            // 
            this.ctrlInHistoryBox21.ClearButtonVisible = false;
            this.ctrlInHistoryBox21.Location = new System.Drawing.Point(455, 10);
            this.ctrlInHistoryBox21.Name = "ctrlInHistoryBox21";
            patIn1.Age = "";
            patIn1.Birth = "";
            patIn1.Id = "";
            patIn1.Kana = "";
            patIn1.Name = "";
            this.ctrlInHistoryBox21.PatIn1 = patIn1;
            this.ctrlInHistoryBox21.PtId = "";
            this.ctrlInHistoryBox21.Size = new System.Drawing.Size(200, 25);
            this.ctrlInHistoryBox21.TabIndex = 23;
            this.ctrlInHistoryBox21.ValueChanged += new System.EventHandler<System.EventArgs>(this.ctrlInHistoryBox21_ValueChanged);
            // 
            // FormDischargeSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1184, 712);
            this.Controls.Add(this.ctrlInHistoryBox21);
            this.Controls.Add(this.DateTimeLabel);
            this.Controls.Add(this.StatusDateBox);
            this.Controls.Add(this.ContBox6);
            this.Controls.Add(this.ContBox5);
            this.Controls.Add(this.ContBox4);
            this.Controls.Add(this.ContBox3);
            this.Controls.Add(this.CheckBox2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.CheckBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ContBox2);
            this.Controls.Add(this.ContBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MainDiagBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stdControlPat11);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDischargeSummary";
            this.Text = "退院時サマリ";
            this.Load += new System.EventHandler(this.FormDischargeSummary_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StdControlPat1 stdControlPat11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox MainDiagBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox ContBox1;
        private System.Windows.Forms.TextBox ContBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox CheckBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox CheckBox2;
        private System.Windows.Forms.TextBox ContBox3;
        private System.Windows.Forms.TextBox ContBox4;
        private System.Windows.Forms.TextBox ContBox5;
        private System.Windows.Forms.TextBox ContBox6;
        private CtrlDateBox2 StatusDateBox;
        private System.Windows.Forms.Label DateTimeLabel;
        private CtrlInHistoryBox2 ctrlInHistoryBox21;
    }
}