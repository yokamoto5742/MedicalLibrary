namespace MedicalLibrary.Boundary
{
    partial class CtrlAllergy1
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ListView1 = new System.Windows.Forms.DataGridView();
            this.SaveButton1 = new System.Windows.Forms.Button();
            this.StaffLabel1 = new System.Windows.Forms.Label();
            this.DateTimeLabel1 = new System.Windows.Forms.Label();
            this.GroupBox1 = new System.Windows.Forms.ComboBox();
            this.MasterBox1 = new System.Windows.Forms.ComboBox();
            this.ContBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ClearButton1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.DeleteButton1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ListView1
            // 
            this.ListView1.AllowUserToAddRows = false;
            this.ListView1.AllowUserToDeleteRows = false;
            this.ListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ListView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.ListView1.Location = new System.Drawing.Point(5, 5);
            this.ListView1.MultiSelect = false;
            this.ListView1.Name = "ListView1";
            this.ListView1.ReadOnly = true;
            this.ListView1.RowHeadersVisible = false;
            this.ListView1.RowTemplate.Height = 21;
            this.ListView1.Size = new System.Drawing.Size(490, 200);
            this.ListView1.TabIndex = 0;
            this.ListView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListView1_CellClick);
            // 
            // SaveButton1
            // 
            this.SaveButton1.Location = new System.Drawing.Point(310, 272);
            this.SaveButton1.Name = "SaveButton1";
            this.SaveButton1.Size = new System.Drawing.Size(75, 23);
            this.SaveButton1.TabIndex = 1;
            this.SaveButton1.Text = "登録";
            this.SaveButton1.UseVisualStyleBackColor = true;
            this.SaveButton1.Click += new System.EventHandler(this.SaveButton1_Click);
            // 
            // StaffLabel1
            // 
            this.StaffLabel1.BackColor = System.Drawing.SystemColors.Info;
            this.StaffLabel1.Location = new System.Drawing.Point(70, 275);
            this.StaffLabel1.Name = "StaffLabel1";
            this.StaffLabel1.Size = new System.Drawing.Size(100, 16);
            this.StaffLabel1.TabIndex = 2;
            this.StaffLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DateTimeLabel1
            // 
            this.DateTimeLabel1.BackColor = System.Drawing.SystemColors.Info;
            this.DateTimeLabel1.Location = new System.Drawing.Point(175, 275);
            this.DateTimeLabel1.Name = "DateTimeLabel1";
            this.DateTimeLabel1.Size = new System.Drawing.Size(120, 16);
            this.DateTimeLabel1.TabIndex = 3;
            this.DateTimeLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GroupBox1
            // 
            this.GroupBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GroupBox1.FormattingEnabled = true;
            this.GroupBox1.Location = new System.Drawing.Point(50, 215);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(100, 20);
            this.GroupBox1.TabIndex = 4;
            this.GroupBox1.SelectedIndexChanged += new System.EventHandler(this.GroupBox1_SelectedIndexChanged);
            // 
            // MasterBox1
            // 
            this.MasterBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MasterBox1.FormattingEnabled = true;
            this.MasterBox1.Location = new System.Drawing.Point(50, 240);
            this.MasterBox1.Name = "MasterBox1";
            this.MasterBox1.Size = new System.Drawing.Size(100, 20);
            this.MasterBox1.TabIndex = 5;
            // 
            // ContBox1
            // 
            this.ContBox1.Location = new System.Drawing.Point(215, 215);
            this.ContBox1.Multiline = true;
            this.ContBox1.Name = "ContBox1";
            this.ContBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ContBox1.Size = new System.Drawing.Size(280, 50);
            this.ContBox1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 220);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "分類";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 245);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "項目";
            // 
            // ClearButton1
            // 
            this.ClearButton1.Location = new System.Drawing.Point(390, 272);
            this.ClearButton1.Name = "ClearButton1";
            this.ClearButton1.Size = new System.Drawing.Size(50, 23);
            this.ClearButton1.TabIndex = 9;
            this.ClearButton1.Text = "クリア";
            this.ClearButton1.UseVisualStyleBackColor = true;
            this.ClearButton1.Click += new System.EventHandler(this.ClearButton1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(170, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "コメント";
            // 
            // DeleteButton1
            // 
            this.DeleteButton1.Location = new System.Drawing.Point(445, 272);
            this.DeleteButton1.Name = "DeleteButton1";
            this.DeleteButton1.Size = new System.Drawing.Size(50, 23);
            this.DeleteButton1.TabIndex = 13;
            this.DeleteButton1.Text = "削除";
            this.DeleteButton1.UseVisualStyleBackColor = true;
            this.DeleteButton1.Click += new System.EventHandler(this.DeleteButton1_Click);
            // 
            // CtrlAllergy1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DeleteButton1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ClearButton1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ContBox1);
            this.Controls.Add(this.MasterBox1);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.DateTimeLabel1);
            this.Controls.Add(this.StaffLabel1);
            this.Controls.Add(this.SaveButton1);
            this.Controls.Add(this.ListView1);
            this.Name = "CtrlAllergy1";
            this.Size = new System.Drawing.Size(500, 300);
            this.Load += new System.EventHandler(this.CtrlAllergy1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ListView1;
        private System.Windows.Forms.Button SaveButton1;
        private System.Windows.Forms.Label StaffLabel1;
        private System.Windows.Forms.Label DateTimeLabel1;
        private System.Windows.Forms.ComboBox GroupBox1;
        private System.Windows.Forms.ComboBox MasterBox1;
        private System.Windows.Forms.TextBox ContBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ClearButton1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button DeleteButton1;
    }
}
