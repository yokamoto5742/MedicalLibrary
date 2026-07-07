namespace MedicalLibrary.Agent
{
    partial class FormComeReportAchieve
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormComeReportAchieve));
            this.GridView = new System.Windows.Forms.DataGridView();
            this.YearBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.MonthBox = new System.Windows.Forms.ComboBox();
            this.ShowButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.GridView2 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // GridView
            // 
            this.GridView.AllowUserToAddRows = false;
            this.GridView.AllowUserToDeleteRows = false;
            this.GridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.GridView.Location = new System.Drawing.Point(5, 55);
            this.GridView.Name = "GridView";
            this.GridView.ReadOnly = true;
            this.GridView.RowHeadersVisible = false;
            this.GridView.RowTemplate.Height = 21;
            this.GridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridView.Size = new System.Drawing.Size(320, 220);
            this.GridView.TabIndex = 0;
            this.GridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridView_CellClick);
            // 
            // YearBox
            // 
            this.YearBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.YearBox.FormattingEnabled = true;
            this.YearBox.Location = new System.Drawing.Point(12, 7);
            this.YearBox.Name = "YearBox";
            this.YearBox.Size = new System.Drawing.Size(60, 20);
            this.YearBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "年";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(156, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "月";
            // 
            // MonthBox
            // 
            this.MonthBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MonthBox.FormattingEnabled = true;
            this.MonthBox.Location = new System.Drawing.Point(103, 7);
            this.MonthBox.Name = "MonthBox";
            this.MonthBox.Size = new System.Drawing.Size(50, 20);
            this.MonthBox.TabIndex = 3;
            // 
            // ShowButton
            // 
            this.ShowButton.Location = new System.Drawing.Point(185, 5);
            this.ShowButton.Name = "ShowButton";
            this.ShowButton.Size = new System.Drawing.Size(63, 23);
            this.ShowButton.TabIndex = 5;
            this.ShowButton.Text = "表示";
            this.ShowButton.UseVisualStyleBackColor = true;
            this.ShowButton.Click += new System.EventHandler(this.ShowButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(253, 5);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(63, 23);
            this.CloseButton.TabIndex = 6;
            this.CloseButton.Text = "閉じる";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(252, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "件数はオーダー単位です。部位単位ではありません。";
            // 
            // GridView2
            // 
            this.GridView2.AllowUserToAddRows = false;
            this.GridView2.AllowUserToDeleteRows = false;
            this.GridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridView2.DefaultCellStyle = dataGridViewCellStyle2;
            this.GridView2.Location = new System.Drawing.Point(5, 280);
            this.GridView2.Name = "GridView2";
            this.GridView2.ReadOnly = true;
            this.GridView2.RowHeadersVisible = false;
            this.GridView2.RowTemplate.Height = 21;
            this.GridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridView2.Size = new System.Drawing.Size(320, 280);
            this.GridView2.TabIndex = 8;
            // 
            // FormComeReportAchieve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 561);
            this.Controls.Add(this.GridView2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.ShowButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MonthBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.YearBox);
            this.Controls.Add(this.GridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormComeReportAchieve";
            this.Text = "読影実績";
            this.Load += new System.EventHandler(this.FormComeReportAchieve_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView GridView;
        private System.Windows.Forms.ComboBox YearBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox MonthBox;
        private System.Windows.Forms.Button ShowButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView GridView2;
    }
}