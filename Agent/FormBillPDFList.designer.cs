namespace MedicalLibrary.Agent
{
    partial class FormBillPDFList
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ListView1 = new System.Windows.Forms.DataGridView();
            this.FilterBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ShowSubBox1 = new System.Windows.Forms.CheckBox();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.ShowButton1 = new System.Windows.Forms.Button();
            this.ShowPastBox1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TickIntervalBox1 = new System.Windows.Forms.ComboBox();
            this.TickModeButton1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.PrintButton1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.FilterBox2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.FilterBox3 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListView1
            // 
            this.ListView1.AllowUserToAddRows = false;
            this.ListView1.AllowUserToDeleteRows = false;
            this.ListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ListView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.ListView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ListView1.DefaultCellStyle = dataGridViewCellStyle6;
            this.ListView1.Location = new System.Drawing.Point(5, 60);
            this.ListView1.MultiSelect = false;
            this.ListView1.Name = "ListView1";
            this.ListView1.ReadOnly = true;
            this.ListView1.RowHeadersVisible = false;
            this.ListView1.RowTemplate.Height = 21;
            this.ListView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ListView1.Size = new System.Drawing.Size(775, 500);
            this.ListView1.TabIndex = 1;
            this.ListView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListView1_CellDoubleClick);
            this.ListView1.Sorted += new System.EventHandler(this.ListView1_Sorted);
            // 
            // FilterBox1
            // 
            this.FilterBox1.Location = new System.Drawing.Point(40, 16);
            this.FilterBox1.MaxLength = 9;
            this.FilterBox1.Name = "FilterBox1";
            this.FilterBox1.Size = new System.Drawing.Size(60, 19);
            this.FilterBox1.TabIndex = 2;
            this.FilterBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FilterBox1.TextChanged += new System.EventHandler(this.FilterBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "ID";
            // 
            // ShowSubBox1
            // 
            this.ShowSubBox1.AutoSize = true;
            this.ShowSubBox1.Location = new System.Drawing.Point(385, 35);
            this.ShowSubBox1.Name = "ShowSubBox1";
            this.ShowSubBox1.Size = new System.Drawing.Size(117, 16);
            this.ShowSubBox1.TabIndex = 5;
            this.ShowSubBox1.Text = "請求書控えも表示";
            this.ShowSubBox1.UseVisualStyleBackColor = true;
            this.ShowSubBox1.Visible = false;
            this.ShowSubBox1.CheckedChanged += new System.EventHandler(this.ShowSubBox1_CheckedChanged);
            // 
            // Timer1
            // 
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // ShowButton1
            // 
            this.ShowButton1.Location = new System.Drawing.Point(715, 5);
            this.ShowButton1.Name = "ShowButton1";
            this.ShowButton1.Size = new System.Drawing.Size(65, 23);
            this.ShowButton1.TabIndex = 6;
            this.ShowButton1.Text = "更新(F5)";
            this.ShowButton1.UseVisualStyleBackColor = true;
            this.ShowButton1.Click += new System.EventHandler(this.ShowButton1_Click);
            // 
            // ShowPastBox1
            // 
            this.ShowPastBox1.AutoSize = true;
            this.ShowPastBox1.Location = new System.Drawing.Point(385, 10);
            this.ShowPastBox1.Name = "ShowPastBox1";
            this.ShowPastBox1.Size = new System.Drawing.Size(115, 16);
            this.ShowPastBox1.TabIndex = 7;
            this.ShowPastBox1.Text = "過去データも表示";
            this.ShowPastBox1.UseVisualStyleBackColor = true;
            this.ShowPastBox1.CheckedChanged += new System.EventHandler(this.ShowPastBox1_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(510, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "更新間隔（秒）";
            // 
            // TickIntervalBox1
            // 
            this.TickIntervalBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TickIntervalBox1.FormattingEnabled = true;
            this.TickIntervalBox1.Location = new System.Drawing.Point(590, 7);
            this.TickIntervalBox1.Name = "TickIntervalBox1";
            this.TickIntervalBox1.Size = new System.Drawing.Size(50, 20);
            this.TickIntervalBox1.TabIndex = 9;
            this.TickIntervalBox1.SelectedIndexChanged += new System.EventHandler(this.TickIntervalBox1_SelectedIndexChanged);
            // 
            // TickModeButton1
            // 
            this.TickModeButton1.BackColor = System.Drawing.SystemColors.Control;
            this.TickModeButton1.Location = new System.Drawing.Point(645, 5);
            this.TickModeButton1.Name = "TickModeButton1";
            this.TickModeButton1.Size = new System.Drawing.Size(65, 23);
            this.TickModeButton1.TabIndex = 10;
            this.TickModeButton1.Text = "自動更新";
            this.TickModeButton1.UseVisualStyleBackColor = false;
            this.TickModeButton1.Click += new System.EventHandler(this.TickModeButton1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(515, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(192, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "選択している書類を印刷できます　→";
            // 
            // PrintButton1
            // 
            this.PrintButton1.Location = new System.Drawing.Point(715, 30);
            this.PrintButton1.Name = "PrintButton1";
            this.PrintButton1.Size = new System.Drawing.Size(65, 23);
            this.PrintButton1.TabIndex = 12;
            this.PrintButton1.Text = "印刷(F8)";
            this.PrintButton1.UseVisualStyleBackColor = true;
            this.PrintButton1.Click += new System.EventHandler(this.PrintButton1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(163, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "※ F3 を押すとクリアされます。";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(105, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "氏名";
            // 
            // FilterBox2
            // 
            this.FilterBox2.Location = new System.Drawing.Point(135, 16);
            this.FilterBox2.MaxLength = 20;
            this.FilterBox2.Name = "FilterBox2";
            this.FilterBox2.Size = new System.Drawing.Size(80, 19);
            this.FilterBox2.TabIndex = 14;
            this.FilterBox2.TextChanged += new System.EventHandler(this.FilterBox2_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(225, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "請求書";
            // 
            // FilterBox3
            // 
            this.FilterBox3.Location = new System.Drawing.Point(265, 16);
            this.FilterBox3.MaxLength = 20;
            this.FilterBox3.Name = "FilterBox3";
            this.FilterBox3.Size = new System.Drawing.Size(80, 19);
            this.FilterBox3.TabIndex = 18;
            this.FilterBox3.TextChanged += new System.EventHandler(this.FilterBox3_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(5, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 55);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // FormPDFList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.FilterBox3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.FilterBox2);
            this.Controls.Add(this.PrintButton1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TickModeButton1);
            this.Controls.Add(this.TickIntervalBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ShowPastBox1);
            this.Controls.Add(this.ShowButton1);
            this.Controls.Add(this.ShowSubBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FilterBox1);
            this.Controls.Add(this.ListView1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            this.KeyPreview = true;
            this.Name = "FormPDFList";
            this.Text = "請求書・明細書PDFファイル一覧";
            this.Load += new System.EventHandler(this.FormBillPDFList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormPDFView_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ListView1;
        private System.Windows.Forms.TextBox FilterBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ShowSubBox1;
        private System.Windows.Forms.Timer Timer1;
        private System.Windows.Forms.Button ShowButton1;
        private System.Windows.Forms.CheckBox ShowPastBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox TickIntervalBox1;
        private System.Windows.Forms.Button TickModeButton1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button PrintButton1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox FilterBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox FilterBox3;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}