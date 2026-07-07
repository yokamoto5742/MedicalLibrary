namespace MedicalLibrary.Boundary
{
    partial class FormFindOpe
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFindOpe));
            this.label1 = new System.Windows.Forms.Label();
            this.OpeNameBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.OpeFindBox = new System.Windows.Forms.TextBox();
            this.ListView = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.KCodeBox = new System.Windows.Forms.TextBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.ContBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.STEMBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ListView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 197);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 72;
            this.label1.Text = "術式";
            // 
            // OpeNameBox
            // 
            this.OpeNameBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.OpeNameBox.Location = new System.Drawing.Point(50, 194);
            this.OpeNameBox.Name = "OpeNameBox";
            this.OpeNameBox.ReadOnly = true;
            this.OpeNameBox.Size = new System.Drawing.Size(430, 19);
            this.OpeNameBox.TabIndex = 71;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 70;
            this.label5.Text = "手術検索";
            // 
            // OpeFindBox
            // 
            this.OpeFindBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OpeFindBox.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.OpeFindBox.Location = new System.Drawing.Point(65, 5);
            this.OpeFindBox.Name = "OpeFindBox";
            this.OpeFindBox.Size = new System.Drawing.Size(415, 19);
            this.OpeFindBox.TabIndex = 68;
            this.OpeFindBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OpeFindBox_KeyDown);
            // 
            // ListView
            // 
            this.ListView.AllowUserToAddRows = false;
            this.ListView.AllowUserToDeleteRows = false;
            this.ListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ListView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListView.DefaultCellStyle = dataGridViewCellStyle2;
            this.ListView.Location = new System.Drawing.Point(5, 30);
            this.ListView.MultiSelect = false;
            this.ListView.Name = "ListView";
            this.ListView.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.ListView.RowHeadersVisible = false;
            this.ListView.RowTemplate.Height = 21;
            this.ListView.Size = new System.Drawing.Size(475, 160);
            this.ListView.TabIndex = 73;
            this.ListView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListView_CellClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 220);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 12);
            this.label2.TabIndex = 75;
            this.label2.Text = "Kコード";
            // 
            // KCodeBox
            // 
            this.KCodeBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.KCodeBox.Location = new System.Drawing.Point(50, 217);
            this.KCodeBox.Name = "KCodeBox";
            this.KCodeBox.ReadOnly = true;
            this.KCodeBox.Size = new System.Drawing.Size(100, 19);
            this.KCodeBox.TabIndex = 74;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(400, 235);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 76;
            this.OKButton.Text = "確定";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // ContBox
            // 
            this.ContBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ContBox.Location = new System.Drawing.Point(50, 240);
            this.ContBox.Name = "ContBox";
            this.ContBox.ReadOnly = true;
            this.ContBox.Size = new System.Drawing.Size(340, 19);
            this.ContBox.TabIndex = 77;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(170, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 79;
            this.label3.Text = "STEM7";
            // 
            // STEMBox
            // 
            this.STEMBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.STEMBox.Location = new System.Drawing.Point(220, 217);
            this.STEMBox.Name = "STEMBox";
            this.STEMBox.ReadOnly = true;
            this.STEMBox.Size = new System.Drawing.Size(120, 19);
            this.STEMBox.TabIndex = 78;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 243);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 12);
            this.label4.TabIndex = 80;
            this.label4.Text = "コメント";
            // 
            // FormFindOpe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 262);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.STEMBox);
            this.Controls.Add(this.ContBox);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.KCodeBox);
            this.Controls.Add(this.ListView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OpeNameBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.OpeFindBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormFindOpe";
            this.Text = "手術検索";
            this.Load += new System.EventHandler(this.FormFindOpe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox OpeNameBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox OpeFindBox;
        private System.Windows.Forms.DataGridView ListView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox KCodeBox;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.TextBox ContBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox STEMBox;
        private System.Windows.Forms.Label label4;
    }
}