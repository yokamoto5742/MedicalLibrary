namespace MedicalLibrary.Agent
{
    partial class NidekARK1ListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NidekARK1ListForm));
            this.ListView1 = new System.Windows.Forms.DataGridView();
            this.Date1 = new System.Windows.Forms.DateTimePicker();
            this.DataBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ListShowButton = new System.Windows.Forms.Button();
            this.CopyButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ListView1
            // 
            this.ListView1.AllowUserToAddRows = false;
            this.ListView1.AllowUserToDeleteRows = false;
            this.ListView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ListView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListView1.Location = new System.Drawing.Point(5, 30);
            this.ListView1.Name = "ListView1";
            this.ListView1.ReadOnly = true;
            this.ListView1.RowHeadersVisible = false;
            this.ListView1.RowTemplate.Height = 21;
            this.ListView1.Size = new System.Drawing.Size(270, 525);
            this.ListView1.TabIndex = 0;
            this.ListView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListView1_CellClick);
            // 
            // Date1
            // 
            this.Date1.CustomFormat = "yyyy年MM月dd日 (ddd)";
            this.Date1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Date1.Location = new System.Drawing.Point(55, 7);
            this.Date1.MaxDate = new System.DateTime(2999, 12, 31, 0, 0, 0, 0);
            this.Date1.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.Date1.Name = "Date1";
            this.Date1.Size = new System.Drawing.Size(150, 19);
            this.Date1.TabIndex = 1;
            this.Date1.ValueChanged += new System.EventHandler(this.Date1_ValueChanged);
            // 
            // DataBox1
            // 
            this.DataBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataBox1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DataBox1.Location = new System.Drawing.Point(280, 30);
            this.DataBox1.Multiline = true;
            this.DataBox1.Name = "DataBox1";
            this.DataBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.DataBox1.Size = new System.Drawing.Size(300, 525);
            this.DataBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "検査日";
            // 
            // ListShowButton
            // 
            this.ListShowButton.Location = new System.Drawing.Point(210, 5);
            this.ListShowButton.Name = "ListShowButton";
            this.ListShowButton.Size = new System.Drawing.Size(60, 23);
            this.ListShowButton.TabIndex = 5;
            this.ListShowButton.Text = "表示";
            this.ListShowButton.UseVisualStyleBackColor = true;
            this.ListShowButton.Click += new System.EventHandler(this.ListShowButton_Click);
            // 
            // CopyButton
            // 
            this.CopyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CopyButton.Location = new System.Drawing.Point(480, 5);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(100, 23);
            this.CopyButton.TabIndex = 6;
            this.CopyButton.Text = "クリップボード";
            this.CopyButton.UseVisualStyleBackColor = true;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // NidekARK1ListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.CopyButton);
            this.Controls.Add(this.ListShowButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DataBox1);
            this.Controls.Add(this.Date1);
            this.Controls.Add(this.ListView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NidekARK1ListForm";
            this.Text = "Nidek ARK1";
            this.Load += new System.EventHandler(this.NidekARK1ListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ListView1;
        private System.Windows.Forms.DateTimePicker Date1;
        private System.Windows.Forms.TextBox DataBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ListShowButton;
        private System.Windows.Forms.Button CopyButton;
    }
}

