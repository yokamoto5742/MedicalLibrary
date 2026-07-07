namespace MedicalLibrary.Boundary
{
    partial class FormLibSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.ReceExeBox1 = new System.Windows.Forms.TextBox();
            this.ReceExeButton1 = new System.Windows.Forms.Button();
            this.OrderXmlExeButton1 = new System.Windows.Forms.Button();
            this.OrderXmlExeBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SaveButton1 = new System.Windows.Forms.Button();
            this.CancelButton1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.OrderReceApiIntervalBox1 = new System.Windows.Forms.ComboBox();
            this.ReceApiExeButton1 = new System.Windows.Forms.Button();
            this.ReceApiExeBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "医事会計アプリ";
            // 
            // ReceExeBox1
            // 
            this.ReceExeBox1.BackColor = System.Drawing.Color.White;
            this.ReceExeBox1.Location = new System.Drawing.Point(120, 8);
            this.ReceExeBox1.Name = "ReceExeBox1";
            this.ReceExeBox1.ReadOnly = true;
            this.ReceExeBox1.Size = new System.Drawing.Size(250, 19);
            this.ReceExeBox1.TabIndex = 1;
            // 
            // ReceExeButton1
            // 
            this.ReceExeButton1.Location = new System.Drawing.Point(370, 6);
            this.ReceExeButton1.Name = "ReceExeButton1";
            this.ReceExeButton1.Size = new System.Drawing.Size(60, 23);
            this.ReceExeButton1.TabIndex = 2;
            this.ReceExeButton1.Text = "選択";
            this.ReceExeButton1.UseVisualStyleBackColor = true;
            this.ReceExeButton1.Click += new System.EventHandler(this.ReceExeButton1_Click);
            // 
            // OrderXmlExeButton1
            // 
            this.OrderXmlExeButton1.Location = new System.Drawing.Point(370, 31);
            this.OrderXmlExeButton1.Name = "OrderXmlExeButton1";
            this.OrderXmlExeButton1.Size = new System.Drawing.Size(60, 23);
            this.OrderXmlExeButton1.TabIndex = 5;
            this.OrderXmlExeButton1.Text = "選択";
            this.OrderXmlExeButton1.UseVisualStyleBackColor = true;
            this.OrderXmlExeButton1.Click += new System.EventHandler(this.OrderXmlExeButton1_Click);
            // 
            // OrderXmlExeBox1
            // 
            this.OrderXmlExeBox1.BackColor = System.Drawing.Color.White;
            this.OrderXmlExeBox1.Location = new System.Drawing.Point(120, 33);
            this.OrderXmlExeBox1.Name = "OrderXmlExeBox1";
            this.OrderXmlExeBox1.ReadOnly = true;
            this.OrderXmlExeBox1.Size = new System.Drawing.Size(250, 19);
            this.OrderXmlExeBox1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "オーダー転送アプリ";
            // 
            // SaveButton1
            // 
            this.SaveButton1.Location = new System.Drawing.Point(140, 230);
            this.SaveButton1.Name = "SaveButton1";
            this.SaveButton1.Size = new System.Drawing.Size(75, 23);
            this.SaveButton1.TabIndex = 6;
            this.SaveButton1.Text = "登録";
            this.SaveButton1.UseVisualStyleBackColor = true;
            this.SaveButton1.Click += new System.EventHandler(this.SaveButton1_Click);
            // 
            // CancelButton1
            // 
            this.CancelButton1.Location = new System.Drawing.Point(230, 230);
            this.CancelButton1.Name = "CancelButton1";
            this.CancelButton1.Size = new System.Drawing.Size(75, 23);
            this.CancelButton1.TabIndex = 7;
            this.CancelButton1.Text = "キャンセル";
            this.CancelButton1.UseVisualStyleBackColor = true;
            this.CancelButton1.Click += new System.EventHandler(this.CancelButton1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(298, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "オーダー転送してから医事会計APIを起動するまでの間隔（秒）";
            // 
            // OrderReceApiIntervalBox1
            // 
            this.OrderReceApiIntervalBox1.FormattingEnabled = true;
            this.OrderReceApiIntervalBox1.Location = new System.Drawing.Point(320, 82);
            this.OrderReceApiIntervalBox1.Name = "OrderReceApiIntervalBox1";
            this.OrderReceApiIntervalBox1.Size = new System.Drawing.Size(50, 20);
            this.OrderReceApiIntervalBox1.TabIndex = 9;
            // 
            // ReceApiExeButton1
            // 
            this.ReceApiExeButton1.Location = new System.Drawing.Point(370, 56);
            this.ReceApiExeButton1.Name = "ReceApiExeButton1";
            this.ReceApiExeButton1.Size = new System.Drawing.Size(60, 23);
            this.ReceApiExeButton1.TabIndex = 12;
            this.ReceApiExeButton1.Text = "選択";
            this.ReceApiExeButton1.UseVisualStyleBackColor = true;
            this.ReceApiExeButton1.Click += new System.EventHandler(this.ReceApiExeButton1_Click);
            // 
            // ReceApiExeBox1
            // 
            this.ReceApiExeBox1.BackColor = System.Drawing.Color.White;
            this.ReceApiExeBox1.Location = new System.Drawing.Point(120, 58);
            this.ReceApiExeBox1.Name = "ReceApiExeBox1";
            this.ReceApiExeBox1.ReadOnly = true;
            this.ReceApiExeBox1.Size = new System.Drawing.Size(250, 19);
            this.ReceApiExeBox1.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "医事会計APIアプリ";
            // 
            // FormLibSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 262);
            this.Controls.Add(this.ReceApiExeButton1);
            this.Controls.Add(this.ReceApiExeBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.OrderReceApiIntervalBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CancelButton1);
            this.Controls.Add(this.SaveButton1);
            this.Controls.Add(this.OrderXmlExeButton1);
            this.Controls.Add(this.OrderXmlExeBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ReceExeButton1);
            this.Controls.Add(this.ReceExeBox1);
            this.Controls.Add(this.label1);
            this.Name = "FormLibSettings";
            this.Text = "アプリ設定";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ReceExeBox1;
        private System.Windows.Forms.Button ReceExeButton1;
        private System.Windows.Forms.Button OrderXmlExeButton1;
        private System.Windows.Forms.TextBox OrderXmlExeBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SaveButton1;
        private System.Windows.Forms.Button CancelButton1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox OrderReceApiIntervalBox1;
        private System.Windows.Forms.Button ReceApiExeButton1;
        private System.Windows.Forms.TextBox ReceApiExeBox1;
        private System.Windows.Forms.Label label4;
    }
}