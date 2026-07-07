namespace MedicalLibrary.Boundary
{
    partial class FormInCal1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInCal1));
            this.stdControlPat11 = new MedicalLibrary.Boundary.StdControlPat1();
            this.label1 = new System.Windows.Forms.Label();
            this.CalGridView1 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.DatePicker1 = new System.Windows.Forms.DateTimePicker();
            this.ShowButton1 = new System.Windows.Forms.Button();
            this.PrevButton1 = new System.Windows.Forms.Button();
            this.NextButton1 = new System.Windows.Forms.Button();
            this.InHistoryBox2 = new MedicalLibrary.Boundary.CtrlInHistoryBox2();
            ((System.ComponentModel.ISupportInitialize)(this.CalGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // stdControlPat11
            // 
            this.stdControlPat11.Location = new System.Drawing.Point(2, 2);
            this.stdControlPat11.Mode1 = MedicalLibrary.Boundary.StdControlPat1.Mode.Normal;
            this.stdControlPat11.Name = "stdControlPat11";
            this.stdControlPat11.ReadOnly = false;
            this.stdControlPat11.Size = new System.Drawing.Size(450, 30);
            this.stdControlPat11.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(470, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "入院期間";
            // 
            // CalGridView1
            // 
            this.CalGridView1.AllowUserToAddRows = false;
            this.CalGridView1.AllowUserToDeleteRows = false;
            this.CalGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CalGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CalGridView1.Location = new System.Drawing.Point(5, 35);
            this.CalGridView1.Name = "CalGridView1";
            this.CalGridView1.ReadOnly = true;
            this.CalGridView1.RowHeadersVisible = false;
            this.CalGridView1.RowTemplate.Height = 21;
            this.CalGridView1.Size = new System.Drawing.Size(1075, 220);
            this.CalGridView1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(735, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "表示開始日";
            // 
            // DatePicker1
            // 
            this.DatePicker1.CustomFormat = "yyyy/MM/dd (ddd)";
            this.DatePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatePicker1.Location = new System.Drawing.Point(805, 8);
            this.DatePicker1.Name = "DatePicker1";
            this.DatePicker1.Size = new System.Drawing.Size(120, 19);
            this.DatePicker1.TabIndex = 6;
            this.DatePicker1.ValueChanged += new System.EventHandler(this.DatePicker1_ValueChanged);
            // 
            // ShowButton1
            // 
            this.ShowButton1.Location = new System.Drawing.Point(1030, 6);
            this.ShowButton1.Name = "ShowButton1";
            this.ShowButton1.Size = new System.Drawing.Size(50, 23);
            this.ShowButton1.TabIndex = 7;
            this.ShowButton1.Text = "表示";
            this.ShowButton1.UseVisualStyleBackColor = true;
            this.ShowButton1.Click += new System.EventHandler(this.ShowButton1_Click);
            // 
            // PrevButton1
            // 
            this.PrevButton1.Location = new System.Drawing.Point(930, 7);
            this.PrevButton1.Name = "PrevButton1";
            this.PrevButton1.Size = new System.Drawing.Size(45, 22);
            this.PrevButton1.TabIndex = 8;
            this.PrevButton1.Text = "<< 前";
            this.PrevButton1.UseVisualStyleBackColor = true;
            this.PrevButton1.Click += new System.EventHandler(this.PrevButton1_Click);
            // 
            // NextButton1
            // 
            this.NextButton1.Location = new System.Drawing.Point(980, 7);
            this.NextButton1.Name = "NextButton1";
            this.NextButton1.Size = new System.Drawing.Size(45, 22);
            this.NextButton1.TabIndex = 9;
            this.NextButton1.Text = "次 >>";
            this.NextButton1.UseVisualStyleBackColor = true;
            this.NextButton1.Click += new System.EventHandler(this.NextButton1_Click);
            // 
            // InHistoryBox2
            // 
            this.InHistoryBox2.ClearButtonVisible = true;
            this.InHistoryBox2.Location = new System.Drawing.Point(525, 5);
            this.InHistoryBox2.Name = "InHistoryBox2";
            patIn1.Age = "";
            patIn1.Birth = "";
            patIn1.Id = "";
            patIn1.Kana = "";
            patIn1.Name = "";
            this.InHistoryBox2.PatIn1 = patIn1;
            this.InHistoryBox2.PtId = "";
            this.InHistoryBox2.Size = new System.Drawing.Size(195, 25);
            this.InHistoryBox2.TabIndex = 14;
            this.InHistoryBox2.ValueChanged += new System.EventHandler<System.EventArgs>(this.InHistoryBox2_ValueChanged);
            // 
            // FormInCal1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 262);
            this.Controls.Add(this.InHistoryBox2);
            this.Controls.Add(this.NextButton1);
            this.Controls.Add(this.PrevButton1);
            this.Controls.Add(this.ShowButton1);
            this.Controls.Add(this.DatePicker1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CalGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stdControlPat11);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormInCal1";
            this.Text = "入院カレンダー";
            ((System.ComponentModel.ISupportInitialize)(this.CalGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StdControlPat1 stdControlPat11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView CalGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker DatePicker1;
        private System.Windows.Forms.Button ShowButton1;
        private System.Windows.Forms.Button PrevButton1;
        private System.Windows.Forms.Button NextButton1;
        private CtrlInHistoryBox2 InHistoryBox2;
    }
}