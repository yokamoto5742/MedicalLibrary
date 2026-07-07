namespace MedicalLibrary.Boundary
{
    partial class FormDPCCheck1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDPCCheck1));
            this.ListView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.SendButton1 = new System.Windows.Forms.Button();
            this.ShowButton1 = new System.Windows.Forms.Button();
            this.SendButton2 = new System.Windows.Forms.Button();
            this.ListView2 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.SendButton3 = new System.Windows.Forms.Button();
            this.DaysBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DiagBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.InOutBox = new System.Windows.Forms.ComboBox();
            this.ListPanel1 = new System.Windows.Forms.Panel();
            this.ListLabel1 = new System.Windows.Forms.Label();
            this.ListLabel2 = new System.Windows.Forms.Label();
            this.ListPanel2 = new System.Windows.Forms.Panel();
            this.DoubtBox = new System.Windows.Forms.CheckBox();
            this.ShowButton2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.DaysBox2 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListView2)).BeginInit();
            this.ListPanel1.SuspendLayout();
            this.ListPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListView1
            // 
            this.ListView1.AllowUserToAddRows = false;
            this.ListView1.AllowUserToDeleteRows = false;
            this.ListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ListView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ListView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListView1.Location = new System.Drawing.Point(5, 30);
            this.ListView1.MultiSelect = false;
            this.ListView1.Name = "ListView1";
            this.ListView1.ReadOnly = true;
            this.ListView1.RowHeadersVisible = false;
            this.ListView1.RowTemplate.Height = 21;
            this.ListView1.Size = new System.Drawing.Size(855, 360);
            this.ListView1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "入院・退院患者とサマリ・DPC病名";
            // 
            // SendButton1
            // 
            this.SendButton1.Location = new System.Drawing.Point(580, 5);
            this.SendButton1.Name = "SendButton1";
            this.SendButton1.Size = new System.Drawing.Size(75, 23);
            this.SendButton1.TabIndex = 2;
            this.SendButton1.Text = "送信1";
            this.SendButton1.UseVisualStyleBackColor = true;
            this.SendButton1.Click += new System.EventHandler(this.SendButton1_Click);
            // 
            // ShowButton1
            // 
            this.ShowButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowButton1.Location = new System.Drawing.Point(785, 5);
            this.ShowButton1.Name = "ShowButton1";
            this.ShowButton1.Size = new System.Drawing.Size(75, 23);
            this.ShowButton1.TabIndex = 3;
            this.ShowButton1.Text = "表示";
            this.ShowButton1.UseVisualStyleBackColor = true;
            this.ShowButton1.Click += new System.EventHandler(this.ShowButton1_Click);
            // 
            // SendButton2
            // 
            this.SendButton2.Location = new System.Drawing.Point(660, 5);
            this.SendButton2.Name = "SendButton2";
            this.SendButton2.Size = new System.Drawing.Size(75, 23);
            this.SendButton2.TabIndex = 4;
            this.SendButton2.Text = "送信2";
            this.SendButton2.UseVisualStyleBackColor = true;
            this.SendButton2.Click += new System.EventHandler(this.SendButton2_Click);
            // 
            // ListView2
            // 
            this.ListView2.AllowUserToAddRows = false;
            this.ListView2.AllowUserToDeleteRows = false;
            this.ListView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ListView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ListView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListView2.Location = new System.Drawing.Point(5, 420);
            this.ListView2.MultiSelect = false;
            this.ListView2.Name = "ListView2";
            this.ListView2.ReadOnly = true;
            this.ListView2.RowHeadersVisible = false;
            this.ListView2.RowTemplate.Height = 21;
            this.ListView2.Size = new System.Drawing.Size(855, 180);
            this.ListView2.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 400);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "病名検索";
            // 
            // SendButton3
            // 
            this.SendButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SendButton3.Location = new System.Drawing.Point(660, 395);
            this.SendButton3.Name = "SendButton3";
            this.SendButton3.Size = new System.Drawing.Size(75, 23);
            this.SendButton3.TabIndex = 7;
            this.SendButton3.Text = "送信3";
            this.SendButton3.UseVisualStyleBackColor = true;
            this.SendButton3.Click += new System.EventHandler(this.SendButton3_Click);
            // 
            // DaysBox1
            // 
            this.DaysBox1.FormattingEnabled = true;
            this.DaysBox1.Location = new System.Drawing.Point(295, 6);
            this.DaysBox1.Name = "DaysBox1";
            this.DaysBox1.Size = new System.Drawing.Size(40, 20);
            this.DaysBox1.TabIndex = 8;
            this.DaysBox1.TextChanged += new System.EventHandler(this.DaysBox1_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(250, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "退院後";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(340, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "日以内";
            // 
            // DiagBox
            // 
            this.DiagBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DiagBox.FormattingEnabled = true;
            this.DiagBox.Location = new System.Drawing.Point(180, 395);
            this.DiagBox.Name = "DiagBox";
            this.DiagBox.Size = new System.Drawing.Size(121, 20);
            this.DiagBox.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(90, 400);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "病名またはコード";
            // 
            // InOutBox
            // 
            this.InOutBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.InOutBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InOutBox.FormattingEnabled = true;
            this.InOutBox.Location = new System.Drawing.Point(310, 395);
            this.InOutBox.Name = "InOutBox";
            this.InOutBox.Size = new System.Drawing.Size(60, 20);
            this.InOutBox.TabIndex = 13;
            // 
            // ListPanel1
            // 
            this.ListPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ListPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(224)))));
            this.ListPanel1.Controls.Add(this.ListLabel1);
            this.ListPanel1.Location = new System.Drawing.Point(480, 320);
            this.ListPanel1.Name = "ListPanel1";
            this.ListPanel1.Size = new System.Drawing.Size(350, 40);
            this.ListPanel1.TabIndex = 14;
            // 
            // ListLabel1
            // 
            this.ListLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListLabel1.Location = new System.Drawing.Point(5, 5);
            this.ListLabel1.Name = "ListLabel1";
            this.ListLabel1.Size = new System.Drawing.Size(340, 30);
            this.ListLabel1.TabIndex = 15;
            this.ListLabel1.Text = "対象1: 入院後３日以上たってもDPC主病名がチェックされていない人\r\n対象2: 前日にDPC病名がチェックされた人";
            this.ListLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ListLabel2
            // 
            this.ListLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListLabel2.Location = new System.Drawing.Point(5, 5);
            this.ListLabel2.Name = "ListLabel2";
            this.ListLabel2.Size = new System.Drawing.Size(290, 20);
            this.ListLabel2.TabIndex = 15;
            this.ListLabel2.Text = "対象: 過去１年以内に指定の病名がついた人";
            this.ListLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ListPanel2
            // 
            this.ListPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ListPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(224)))));
            this.ListPanel2.Controls.Add(this.ListLabel2);
            this.ListPanel2.Location = new System.Drawing.Point(480, 550);
            this.ListPanel2.Name = "ListPanel2";
            this.ListPanel2.Size = new System.Drawing.Size(300, 30);
            this.ListPanel2.TabIndex = 16;
            // 
            // DoubtBox
            // 
            this.DoubtBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DoubtBox.AutoSize = true;
            this.DoubtBox.Location = new System.Drawing.Point(390, 398);
            this.DoubtBox.Name = "DoubtBox";
            this.DoubtBox.Size = new System.Drawing.Size(101, 16);
            this.DoubtBox.TabIndex = 17;
            this.DoubtBox.Text = "疑い病名を含む";
            this.DoubtBox.UseVisualStyleBackColor = true;
            // 
            // ShowButton2
            // 
            this.ShowButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowButton2.Location = new System.Drawing.Point(785, 395);
            this.ShowButton2.Name = "ShowButton2";
            this.ShowButton2.Size = new System.Drawing.Size(75, 23);
            this.ShowButton2.TabIndex = 18;
            this.ShowButton2.Text = "表示";
            this.ShowButton2.UseVisualStyleBackColor = true;
            this.ShowButton2.Click += new System.EventHandler(this.ShowButton2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(600, 400);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 21;
            this.label6.Text = "日以内";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(510, 400);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 20;
            this.label7.Text = "退院後";
            // 
            // DaysBox2
            // 
            this.DaysBox2.FormattingEnabled = true;
            this.DaysBox2.Location = new System.Drawing.Point(555, 396);
            this.DaysBox2.Name = "DaysBox2";
            this.DaysBox2.Size = new System.Drawing.Size(40, 20);
            this.DaysBox2.TabIndex = 19;
            this.DaysBox2.TextChanged += new System.EventHandler(this.DaysBox2_TextChanged);
            // 
            // FormDPCCheck1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 602);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.DaysBox2);
            this.Controls.Add(this.ShowButton2);
            this.Controls.Add(this.DoubtBox);
            this.Controls.Add(this.ListPanel2);
            this.Controls.Add(this.ListPanel1);
            this.Controls.Add(this.InOutBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.DiagBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DaysBox1);
            this.Controls.Add(this.SendButton3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ListView2);
            this.Controls.Add(this.SendButton2);
            this.Controls.Add(this.ShowButton1);
            this.Controls.Add(this.SendButton1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormDPCCheck1";
            this.Text = "DPCチェック";
            this.Load += new System.EventHandler(this.FormDPCCheck1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormDPCCheck1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListView2)).EndInit();
            this.ListPanel1.ResumeLayout(false);
            this.ListPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ListView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SendButton1;
        private System.Windows.Forms.Button ShowButton1;
        private System.Windows.Forms.Button SendButton2;
        private System.Windows.Forms.DataGridView ListView2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SendButton3;
        private System.Windows.Forms.ComboBox DaysBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox DiagBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox InOutBox;
        private System.Windows.Forms.Panel ListPanel1;
        private System.Windows.Forms.Label ListLabel1;
        private System.Windows.Forms.Label ListLabel2;
        private System.Windows.Forms.Panel ListPanel2;
        private System.Windows.Forms.CheckBox DoubtBox;
        private System.Windows.Forms.Button ShowButton2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox DaysBox2;
    }
}