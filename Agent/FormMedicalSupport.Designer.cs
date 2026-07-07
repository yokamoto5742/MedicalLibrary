namespace MedicalLibrary.Agent
{
    partial class FormMedicalSupport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMedicalSupport));
            this.InspectResultLabel = new System.Windows.Forms.Label();
            this.InspectLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.KindPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ListView = new System.Windows.Forms.DataGridView();
            this.ctrlDeptBox11 = new MedicalLibrary.Boundary.CtrlDeptBox1();
            this.ReadButton = new System.Windows.Forms.Button();
            this.stdControlPat11 = new MedicalLibrary.Boundary.StdControlPat1();
            this.ColorLabel1 = new System.Windows.Forms.Label();
            this.DescLabel1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.OpeRecordButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.InspectDataButton = new System.Windows.Forms.Button();
            this.LocationLabel = new System.Windows.Forms.Label();
            this.SizeLabel = new System.Windows.Forms.Label();
            this.AlertButton = new System.Windows.Forms.Button();
            this.DPCDiagButton = new System.Windows.Forms.Button();
            this.DPCButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ListView)).BeginInit();
            this.SuspendLayout();
            // 
            // InspectResultLabel
            // 
            this.InspectResultLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InspectResultLabel.AutoEllipsis = true;
            this.InspectResultLabel.BackColor = System.Drawing.SystemColors.Control;
            this.InspectResultLabel.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.InspectResultLabel.Location = new System.Drawing.Point(515, 175);
            this.InspectResultLabel.Name = "InspectResultLabel";
            this.InspectResultLabel.Size = new System.Drawing.Size(65, 20);
            this.InspectResultLabel.TabIndex = 6;
            this.InspectResultLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // InspectLabel
            // 
            this.InspectLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InspectLabel.AutoEllipsis = true;
            this.InspectLabel.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.InspectLabel.Location = new System.Drawing.Point(10, 175);
            this.InspectLabel.Name = "InspectLabel";
            this.InspectLabel.Size = new System.Drawing.Size(500, 20);
            this.InspectLabel.TabIndex = 5;
            this.InspectLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(10, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "診療科";
            // 
            // KindPanel
            // 
            this.KindPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.KindPanel.AutoScroll = true;
            this.KindPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.KindPanel.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KindPanel.Location = new System.Drawing.Point(5, 70);
            this.KindPanel.Name = "KindPanel";
            this.KindPanel.Size = new System.Drawing.Size(575, 100);
            this.KindPanel.TabIndex = 3;
            // 
            // ListView
            // 
            this.ListView.AllowUserToAddRows = false;
            this.ListView.AllowUserToDeleteRows = false;
            this.ListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ListView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListView.DefaultCellStyle = dataGridViewCellStyle2;
            this.ListView.Location = new System.Drawing.Point(5, 200);
            this.ListView.Name = "ListView";
            this.ListView.ReadOnly = true;
            this.ListView.RowHeadersVisible = false;
            this.ListView.RowTemplate.Height = 21;
            this.ListView.Size = new System.Drawing.Size(575, 275);
            this.ListView.TabIndex = 2;
            // 
            // ctrlDeptBox11
            // 
            this.ctrlDeptBox11.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ctrlDeptBox11.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ctrlDeptBox11.FormattingEnabled = true;
            this.ctrlDeptBox11.Location = new System.Drawing.Point(70, 41);
            this.ctrlDeptBox11.Name = "ctrlDeptBox11";
            this.ctrlDeptBox11.Size = new System.Drawing.Size(121, 23);
            this.ctrlDeptBox11.TabIndex = 1;
            this.ctrlDeptBox11.TextChanged += new System.EventHandler(this.ctrlDeptBox11_TextChanged);
            // 
            // ReadButton
            // 
            this.ReadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ReadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReadButton.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ReadButton.Location = new System.Drawing.Point(520, 40);
            this.ReadButton.Margin = new System.Windows.Forms.Padding(0);
            this.ReadButton.Name = "ReadButton";
            this.ReadButton.Size = new System.Drawing.Size(60, 25);
            this.ReadButton.TabIndex = 7;
            this.ReadButton.Text = "再読込";
            this.ReadButton.UseVisualStyleBackColor = true;
            this.ReadButton.Click += new System.EventHandler(this.ReadButton_Click);
            // 
            // stdControlPat11
            // 
            this.stdControlPat11.Location = new System.Drawing.Point(5, 0);
            this.stdControlPat11.Mode1 = MedicalLibrary.Boundary.StdControlPat1.Mode.Normal;
            this.stdControlPat11.Name = "stdControlPat11";
            this.stdControlPat11.ReadOnly = true;
            this.stdControlPat11.Size = new System.Drawing.Size(530, 35);
            this.stdControlPat11.TabIndex = 8;
            // 
            // ColorLabel1
            // 
            this.ColorLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ColorLabel1.AutoEllipsis = true;
            this.ColorLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ColorLabel1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ColorLabel1.Location = new System.Drawing.Point(10, 480);
            this.ColorLabel1.Name = "ColorLabel1";
            this.ColorLabel1.Size = new System.Drawing.Size(70, 18);
            this.ColorLabel1.TabIndex = 9;
            this.ColorLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DescLabel1
            // 
            this.DescLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DescLabel1.AutoEllipsis = true;
            this.DescLabel1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DescLabel1.Location = new System.Drawing.Point(90, 480);
            this.DescLabel1.Name = "DescLabel1";
            this.DescLabel1.Size = new System.Drawing.Size(110, 18);
            this.DescLabel1.TabIndex = 10;
            this.DescLabel1.Text = "期間中に検査なし";
            this.DescLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoEllipsis = true;
            this.label2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(290, 480);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 18);
            this.label2.TabIndex = 12;
            this.label2.Text = "検査予定あり";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoEllipsis = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(160)))));
            this.label3.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(210, 480);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 18);
            this.label3.TabIndex = 11;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoEllipsis = true;
            this.label4.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(390, 480);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 18);
            this.label4.TabIndex = 13;
            this.label4.Text = "▲　未施行";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OpeRecordButton
            // 
            this.OpeRecordButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OpeRecordButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpeRecordButton.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OpeRecordButton.Location = new System.Drawing.Point(200, 40);
            this.OpeRecordButton.Margin = new System.Windows.Forms.Padding(0);
            this.OpeRecordButton.Name = "OpeRecordButton";
            this.OpeRecordButton.Size = new System.Drawing.Size(55, 25);
            this.OpeRecordButton.TabIndex = 4;
            this.OpeRecordButton.Text = "旧手術";
            this.OpeRecordButton.UseVisualStyleBackColor = true;
            this.OpeRecordButton.Click += new System.EventHandler(this.OpeRecordButton_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoEllipsis = true;
            this.label5.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(480, 480);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 18);
            this.label5.TabIndex = 17;
            this.label5.Text = "☆　院外検査";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // InspectDataButton
            // 
            this.InspectDataButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.InspectDataButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InspectDataButton.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.InspectDataButton.Location = new System.Drawing.Point(445, 40);
            this.InspectDataButton.Margin = new System.Windows.Forms.Padding(0);
            this.InspectDataButton.Name = "InspectDataButton";
            this.InspectDataButton.Size = new System.Drawing.Size(70, 25);
            this.InspectDataButton.TabIndex = 5;
            this.InspectDataButton.Text = "院外検査";
            this.InspectDataButton.UseVisualStyleBackColor = true;
            this.InspectDataButton.Click += new System.EventHandler(this.InspectDataButton_Click);
            // 
            // LocationLabel
            // 
            this.LocationLabel.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LocationLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.LocationLabel.Location = new System.Drawing.Point(530, 2);
            this.LocationLabel.Name = "LocationLabel";
            this.LocationLabel.Size = new System.Drawing.Size(50, 15);
            this.LocationLabel.TabIndex = 15;
            this.LocationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LocationLabel.DoubleClick += new System.EventHandler(this.LocationLabel_DoubleClick);
            // 
            // SizeLabel
            // 
            this.SizeLabel.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SizeLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.SizeLabel.Location = new System.Drawing.Point(530, 20);
            this.SizeLabel.Name = "SizeLabel";
            this.SizeLabel.Size = new System.Drawing.Size(50, 15);
            this.SizeLabel.TabIndex = 16;
            this.SizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SizeLabel.DoubleClick += new System.EventHandler(this.SizeLabel_DoubleClick);
            // 
            // AlertButton
            // 
            this.AlertButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AlertButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AlertButton.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.AlertButton.Location = new System.Drawing.Point(380, 40);
            this.AlertButton.Margin = new System.Windows.Forms.Padding(0);
            this.AlertButton.Name = "AlertButton";
            this.AlertButton.Size = new System.Drawing.Size(60, 25);
            this.AlertButton.TabIndex = 18;
            this.AlertButton.Text = "アラート";
            this.AlertButton.UseVisualStyleBackColor = true;
            this.AlertButton.Click += new System.EventHandler(this.AlertButton_Click);
            // 
            // DPCDiagButton
            // 
            this.DPCDiagButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DPCDiagButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DPCDiagButton.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DPCDiagButton.Location = new System.Drawing.Point(310, 40);
            this.DPCDiagButton.Margin = new System.Windows.Forms.Padding(0);
            this.DPCDiagButton.Name = "DPCDiagButton";
            this.DPCDiagButton.Size = new System.Drawing.Size(65, 25);
            this.DPCDiagButton.TabIndex = 19;
            this.DPCDiagButton.Text = "DPC病名";
            this.DPCDiagButton.UseVisualStyleBackColor = true;
            this.DPCDiagButton.Click += new System.EventHandler(this.DPCDiagButton_Click);
            // 
            // DPCButton
            // 
            this.DPCButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DPCButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DPCButton.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DPCButton.Location = new System.Drawing.Point(260, 40);
            this.DPCButton.Margin = new System.Windows.Forms.Padding(0);
            this.DPCButton.Name = "DPCButton";
            this.DPCButton.Size = new System.Drawing.Size(45, 25);
            this.DPCButton.TabIndex = 20;
            this.DPCButton.Text = "様式1";
            this.DPCButton.UseVisualStyleBackColor = true;
            this.DPCButton.Click += new System.EventHandler(this.DPCButton_Click);
            // 
            // FormMedicalSupport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 502);
            this.Controls.Add(this.DPCButton);
            this.Controls.Add(this.DPCDiagButton);
            this.Controls.Add(this.AlertButton);
            this.Controls.Add(this.InspectDataButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.SizeLabel);
            this.Controls.Add(this.LocationLabel);
            this.Controls.Add(this.OpeRecordButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DescLabel1);
            this.Controls.Add(this.ColorLabel1);
            this.Controls.Add(this.stdControlPat11);
            this.Controls.Add(this.ReadButton);
            this.Controls.Add(this.InspectResultLabel);
            this.Controls.Add(this.InspectLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.KindPanel);
            this.Controls.Add(this.ListView);
            this.Controls.Add(this.ctrlDeptBox11);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormMedicalSupport";
            this.Text = "診療支援システム";
            this.Load += new System.EventHandler(this.FormMedicalSupport_Load);
            this.Shown += new System.EventHandler(this.FormMedicalSupport_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMedicalSupport_KeyDown);
            this.Move += new System.EventHandler(this.FormMedicalSupport_Move);
            this.Resize += new System.EventHandler(this.FormMedicalSupport_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.ListView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MedicalLibrary.Boundary.CtrlDeptBox1 ctrlDeptBox11;
        private System.Windows.Forms.DataGridView ListView;
        private System.Windows.Forms.FlowLayoutPanel KindPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label InspectLabel;
        private System.Windows.Forms.Label InspectResultLabel;
        private System.Windows.Forms.Button ReadButton;
        private MedicalLibrary.Boundary.StdControlPat1 stdControlPat11;
        private System.Windows.Forms.Label ColorLabel1;
        private System.Windows.Forms.Label DescLabel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button OpeRecordButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button InspectDataButton;
        private System.Windows.Forms.Label LocationLabel;
        private System.Windows.Forms.Label SizeLabel;
        private System.Windows.Forms.Button AlertButton;
        private System.Windows.Forms.Button DPCDiagButton;
        private System.Windows.Forms.Button DPCButton;
    }
}