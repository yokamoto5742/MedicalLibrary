namespace MedicalLibrary.Agent
{
    partial class OushinListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OushinListForm));
            this.ListView = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.RegButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.PtIdBox = new System.Windows.Forms.TextBox();
            this.ShowDeadBox = new System.Windows.Forms.CheckBox();
            this.PtNameBox = new System.Windows.Forms.TextBox();
            this.PtContBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ExcelButton = new System.Windows.Forms.Button();
            this.ShowDeleteBox = new System.Windows.Forms.CheckBox();
            this.CountLabel = new System.Windows.Forms.Label();
            this.PtAddrBox = new System.Windows.Forms.TextBox();
            this.PtMarkBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ShowBikouBox2 = new System.Windows.Forms.CheckBox();
            this.ShowBikouBox5 = new System.Windows.Forms.CheckBox();
            this.ShowBikouBox0 = new System.Windows.Forms.CheckBox();
            this.FilterBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ListView)).BeginInit();
            this.SuspendLayout();
            // 
            // ListView
            // 
            this.ListView.AllowUserToAddRows = false;
            this.ListView.AllowUserToDeleteRows = false;
            this.ListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListView.DefaultCellStyle = dataGridViewCellStyle1;
            this.ListView.Location = new System.Drawing.Point(5, 105);
            this.ListView.MultiSelect = false;
            this.ListView.Name = "ListView";
            this.ListView.ReadOnly = true;
            this.ListView.RowHeadersVisible = false;
            this.ListView.RowTemplate.Height = 21;
            this.ListView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ListView.Size = new System.Drawing.Size(935, 535);
            this.ListView.TabIndex = 0;
            this.ListView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListView_CellClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "ID";
            // 
            // RegButton
            // 
            this.RegButton.Location = new System.Drawing.Point(760, 5);
            this.RegButton.Name = "RegButton";
            this.RegButton.Size = new System.Drawing.Size(56, 23);
            this.RegButton.TabIndex = 4;
            this.RegButton.Text = "登録";
            this.RegButton.UseVisualStyleBackColor = true;
            this.RegButton.Click += new System.EventHandler(this.RegButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(880, 5);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(56, 23);
            this.DeleteButton.TabIndex = 5;
            this.DeleteButton.Text = "削除";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Visible = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(820, 5);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(56, 23);
            this.ClearButton.TabIndex = 6;
            this.ClearButton.Text = "クリア";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // PtIdBox
            // 
            this.PtIdBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.PtIdBox.Location = new System.Drawing.Point(30, 30);
            this.PtIdBox.MaxLength = 9;
            this.PtIdBox.Name = "PtIdBox";
            this.PtIdBox.Size = new System.Drawing.Size(70, 19);
            this.PtIdBox.TabIndex = 7;
            this.PtIdBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PtIdBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PtIdBox_KeyDown);
            // 
            // ShowDeadBox
            // 
            this.ShowDeadBox.AutoSize = true;
            this.ShowDeadBox.Location = new System.Drawing.Point(370, 87);
            this.ShowDeadBox.Name = "ShowDeadBox";
            this.ShowDeadBox.Size = new System.Drawing.Size(60, 16);
            this.ShowDeadBox.TabIndex = 8;
            this.ShowDeadBox.Text = "逝去者";
            this.ShowDeadBox.UseVisualStyleBackColor = true;
            this.ShowDeadBox.CheckedChanged += new System.EventHandler(this.ShowDeadBox_CheckedChanged);
            // 
            // PtNameBox
            // 
            this.PtNameBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.PtNameBox.Location = new System.Drawing.Point(105, 30);
            this.PtNameBox.MaxLength = 300;
            this.PtNameBox.Name = "PtNameBox";
            this.PtNameBox.ReadOnly = true;
            this.PtNameBox.Size = new System.Drawing.Size(320, 19);
            this.PtNameBox.TabIndex = 9;
            // 
            // PtContBox
            // 
            this.PtContBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PtContBox.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.PtContBox.Location = new System.Drawing.Point(540, 30);
            this.PtContBox.MaxLength = 100;
            this.PtContBox.Multiline = true;
            this.PtContBox.Name = "PtContBox";
            this.PtContBox.Size = new System.Drawing.Size(400, 52);
            this.PtContBox.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(540, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "コメント";
            // 
            // ExcelButton
            // 
            this.ExcelButton.Location = new System.Drawing.Point(890, 82);
            this.ExcelButton.Name = "ExcelButton";
            this.ExcelButton.Size = new System.Drawing.Size(50, 23);
            this.ExcelButton.TabIndex = 12;
            this.ExcelButton.Text = "Excel";
            this.ExcelButton.UseVisualStyleBackColor = true;
            this.ExcelButton.Click += new System.EventHandler(this.ExcelButton_Click);
            // 
            // ShowDeleteBox
            // 
            this.ShowDeleteBox.AutoSize = true;
            this.ShowDeleteBox.Location = new System.Drawing.Point(440, 87);
            this.ShowDeleteBox.Name = "ShowDeleteBox";
            this.ShowDeleteBox.Size = new System.Drawing.Size(122, 16);
            this.ShowDeleteBox.TabIndex = 13;
            this.ShowDeleteBox.Text = "本システム未登録者";
            this.ShowDeleteBox.UseVisualStyleBackColor = true;
            this.ShowDeleteBox.CheckedChanged += new System.EventHandler(this.ShowDeleteBox_CheckedChanged);
            // 
            // CountLabel
            // 
            this.CountLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.CountLabel.Location = new System.Drawing.Point(835, 87);
            this.CountLabel.Name = "CountLabel";
            this.CountLabel.Size = new System.Drawing.Size(50, 14);
            this.CountLabel.TabIndex = 14;
            this.CountLabel.Text = "0 名";
            this.CountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PtAddrBox
            // 
            this.PtAddrBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.PtAddrBox.Location = new System.Drawing.Point(30, 50);
            this.PtAddrBox.MaxLength = 300;
            this.PtAddrBox.Multiline = true;
            this.PtAddrBox.Name = "PtAddrBox";
            this.PtAddrBox.ReadOnly = true;
            this.PtAddrBox.Size = new System.Drawing.Size(505, 32);
            this.PtAddrBox.TabIndex = 15;
            // 
            // PtMarkBox
            // 
            this.PtMarkBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.PtMarkBox.Location = new System.Drawing.Point(430, 30);
            this.PtMarkBox.MaxLength = 300;
            this.PtMarkBox.Name = "PtMarkBox";
            this.PtMarkBox.ReadOnly = true;
            this.PtMarkBox.Size = new System.Drawing.Size(105, 19);
            this.PtMarkBox.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(30, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(466, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "医事会計システムで「口座引落」または「往診・在宅」として登録されている人を中心に表示します。";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "表示対象者";
            // 
            // ShowBikouBox2
            // 
            this.ShowBikouBox2.AutoSize = true;
            this.ShowBikouBox2.Location = new System.Drawing.Point(90, 87);
            this.ShowBikouBox2.Name = "ShowBikouBox2";
            this.ShowBikouBox2.Size = new System.Drawing.Size(72, 16);
            this.ShowBikouBox2.TabIndex = 19;
            this.ShowBikouBox2.Text = "口座引落";
            this.ShowBikouBox2.UseVisualStyleBackColor = true;
            this.ShowBikouBox2.CheckedChanged += new System.EventHandler(this.ShowBikouBox2_CheckedChanged);
            // 
            // ShowBikouBox5
            // 
            this.ShowBikouBox5.AutoSize = true;
            this.ShowBikouBox5.Location = new System.Drawing.Point(170, 87);
            this.ShowBikouBox5.Name = "ShowBikouBox5";
            this.ShowBikouBox5.Size = new System.Drawing.Size(78, 16);
            this.ShowBikouBox5.TabIndex = 20;
            this.ShowBikouBox5.Text = "往診・在宅";
            this.ShowBikouBox5.UseVisualStyleBackColor = true;
            this.ShowBikouBox5.CheckedChanged += new System.EventHandler(this.ShowBikouBox5_CheckedChanged);
            // 
            // ShowBikouBox0
            // 
            this.ShowBikouBox0.AutoSize = true;
            this.ShowBikouBox0.Location = new System.Drawing.Point(255, 87);
            this.ShowBikouBox0.Name = "ShowBikouBox0";
            this.ShowBikouBox0.Size = new System.Drawing.Size(102, 16);
            this.ShowBikouBox0.TabIndex = 21;
            this.ShowBikouBox0.Text = "引落・往診以外";
            this.ShowBikouBox0.UseVisualStyleBackColor = true;
            this.ShowBikouBox0.CheckedChanged += new System.EventHandler(this.ShowBikouBox0_CheckedChanged);
            // 
            // FilterBox
            // 
            this.FilterBox.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.FilterBox.Location = new System.Drawing.Point(700, 84);
            this.FilterBox.MaxLength = 20;
            this.FilterBox.Name = "FilterBox";
            this.FilterBox.Size = new System.Drawing.Size(130, 19);
            this.FilterBox.TabIndex = 22;
            this.FilterBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FilterBox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(580, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "絞込（ID,氏名,コメント）";
            // 
            // OushinListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 642);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FilterBox);
            this.Controls.Add(this.ShowBikouBox0);
            this.Controls.Add(this.ShowBikouBox5);
            this.Controls.Add(this.ShowBikouBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.PtMarkBox);
            this.Controls.Add(this.PtAddrBox);
            this.Controls.Add(this.CountLabel);
            this.Controls.Add(this.ShowDeleteBox);
            this.Controls.Add(this.ExcelButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PtContBox);
            this.Controls.Add(this.PtNameBox);
            this.Controls.Add(this.ShowDeadBox);
            this.Controls.Add(this.PtIdBox);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.RegButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ListView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OushinListForm";
            this.Text = "往診患者一覧";
            this.Load += new System.EventHandler(this.OushinListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ListView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button RegButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.TextBox PtIdBox;
        private System.Windows.Forms.CheckBox ShowDeadBox;
        private System.Windows.Forms.TextBox PtNameBox;
        private System.Windows.Forms.TextBox PtContBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ExcelButton;
        private System.Windows.Forms.CheckBox ShowDeleteBox;
        private System.Windows.Forms.Label CountLabel;
        private System.Windows.Forms.TextBox PtAddrBox;
        private System.Windows.Forms.TextBox PtMarkBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox ShowBikouBox2;
        private System.Windows.Forms.CheckBox ShowBikouBox5;
        private System.Windows.Forms.CheckBox ShowBikouBox0;
        private System.Windows.Forms.TextBox FilterBox;
        private System.Windows.Forms.Label label1;
    }
}

