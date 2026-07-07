namespace MedicalLibrary.Boundary
{
    partial class FormOpeOrder
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
            this.ChangeBox1 = new System.Windows.Forms.TextBox();
            this.DeleteButton1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DatePicker1 = new System.Windows.Forms.DateTimePicker();
            this.SaveButton1 = new System.Windows.Forms.Button();
            this.DateBox1 = new System.Windows.Forms.ComboBox();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.stdControlPat11 = new MedicalLibrary.Boundary.StdControlPat1();
            this.SuspendLayout();
            // 
            // ChangeBox1
            // 
            this.ChangeBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChangeBox1.Location = new System.Drawing.Point(820, 40);
            this.ChangeBox1.Multiline = true;
            this.ChangeBox1.Name = "ChangeBox1";
            this.ChangeBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ChangeBox1.Size = new System.Drawing.Size(180, 510);
            this.ChangeBox1.TabIndex = 40;
            // 
            // DeleteButton1
            // 
            this.DeleteButton1.Location = new System.Drawing.Point(665, 10);
            this.DeleteButton1.Name = "DeleteButton1";
            this.DeleteButton1.Size = new System.Drawing.Size(50, 23);
            this.DeleteButton1.TabIndex = 39;
            this.DeleteButton1.Text = "削除";
            this.DeleteButton1.UseVisualStyleBackColor = true;
            this.DeleteButton1.Click += new System.EventHandler(this.DeleteButton1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(725, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 38;
            this.label2.Text = "適用開始日";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(525, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 37;
            this.label1.Text = "履歴";
            // 
            // DatePicker1
            // 
            this.DatePicker1.CustomFormat = "yyyy/MM/dd (ddd)";
            this.DatePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatePicker1.Location = new System.Drawing.Point(795, 12);
            this.DatePicker1.Name = "DatePicker1";
            this.DatePicker1.Size = new System.Drawing.Size(120, 19);
            this.DatePicker1.TabIndex = 36;
            // 
            // SaveButton1
            // 
            this.SaveButton1.Location = new System.Drawing.Point(925, 10);
            this.SaveButton1.Name = "SaveButton1";
            this.SaveButton1.Size = new System.Drawing.Size(75, 23);
            this.SaveButton1.TabIndex = 35;
            this.SaveButton1.Text = "登録";
            this.SaveButton1.UseVisualStyleBackColor = true;
            this.SaveButton1.Click += new System.EventHandler(this.SaveButton1_Click);
            // 
            // DateBox1
            // 
            this.DateBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DateBox1.FormattingEnabled = true;
            this.DateBox1.Location = new System.Drawing.Point(560, 11);
            this.DateBox1.Name = "DateBox1";
            this.DateBox1.Size = new System.Drawing.Size(100, 20);
            this.DateBox1.TabIndex = 34;
            this.DateBox1.SelectedIndexChanged += new System.EventHandler(this.DateBox1_SelectedIndexChanged);
            // 
            // Panel1
            // 
            this.Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel1.AutoScroll = true;
            this.Panel1.Location = new System.Drawing.Point(12, 40);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(800, 510);
            this.Panel1.TabIndex = 2;
            // 
            // stdControlPat11
            // 
            this.stdControlPat11.Location = new System.Drawing.Point(10, 5);
            this.stdControlPat11.Name = "stdControlPat11";
            this.stdControlPat11.Size = new System.Drawing.Size(500, 30);
            this.stdControlPat11.TabIndex = 0;
            // 
            // FormOpeOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 562);
            this.Controls.Add(this.ChangeBox1);
            this.Controls.Add(this.DeleteButton1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DatePicker1);
            this.Controls.Add(this.SaveButton1);
            this.Controls.Add(this.DateBox1);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.stdControlPat11);
            this.Name = "FormOpeOrder";
            this.Text = "手術指示";
            this.Load += new System.EventHandler(this.FormOpeOrder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MedicalLibrary.Boundary.StdControlPat1 stdControlPat11;
        private System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.Button DeleteButton1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker DatePicker1;
        private System.Windows.Forms.Button SaveButton1;
        private System.Windows.Forms.ComboBox DateBox1;
        private System.Windows.Forms.TextBox ChangeBox1;
    }
}