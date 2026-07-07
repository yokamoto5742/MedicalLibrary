namespace MedicalLibrary.Boundary
{
    partial class CtrlKarteTemplate1
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
            this.SectionBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DeptBox1 = new System.Windows.Forms.ComboBox();
            this.TemplateBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.KarteTemplatePanel1 = new System.Windows.Forms.Panel();
            this.MakeButton1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SectionBox1
            // 
            this.SectionBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SectionBox1.FormattingEnabled = true;
            this.SectionBox1.Location = new System.Drawing.Point(45, 5);
            this.SectionBox1.Name = "SectionBox1";
            this.SectionBox1.Size = new System.Drawing.Size(120, 20);
            this.SectionBox1.TabIndex = 0;
            this.SectionBox1.SelectedIndexChanged += new System.EventHandler(this.SectionBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "所属";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(180, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "診療科";
            // 
            // DeptBox1
            // 
            this.DeptBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeptBox1.FormattingEnabled = true;
            this.DeptBox1.Location = new System.Drawing.Point(225, 5);
            this.DeptBox1.Name = "DeptBox1";
            this.DeptBox1.Size = new System.Drawing.Size(120, 20);
            this.DeptBox1.TabIndex = 2;
            this.DeptBox1.SelectedIndexChanged += new System.EventHandler(this.DeptBox1_SelectedIndexChanged);
            // 
            // TemplateBox1
            // 
            this.TemplateBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TemplateBox1.FormattingEnabled = true;
            this.TemplateBox1.Location = new System.Drawing.Point(45, 30);
            this.TemplateBox1.Name = "TemplateBox1";
            this.TemplateBox1.Size = new System.Drawing.Size(300, 20);
            this.TemplateBox1.TabIndex = 4;
            this.TemplateBox1.SelectedIndexChanged += new System.EventHandler(this.TemplateBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "名称";
            // 
            // KarteTemplatePanel1
            // 
            this.KarteTemplatePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.KarteTemplatePanel1.AutoScroll = true;
            this.KarteTemplatePanel1.BackColor = System.Drawing.Color.White;
            this.KarteTemplatePanel1.Location = new System.Drawing.Point(5, 55);
            this.KarteTemplatePanel1.Name = "KarteTemplatePanel1";
            this.KarteTemplatePanel1.Size = new System.Drawing.Size(590, 540);
            this.KarteTemplatePanel1.TabIndex = 6;
            // 
            // MakeButton1
            // 
            this.MakeButton1.Location = new System.Drawing.Point(350, 28);
            this.MakeButton1.Name = "MakeButton1";
            this.MakeButton1.Size = new System.Drawing.Size(75, 23);
            this.MakeButton1.TabIndex = 7;
            this.MakeButton1.Text = "確定";
            this.MakeButton1.UseVisualStyleBackColor = true;
            this.MakeButton1.Click += new System.EventHandler(this.MakeButton1_Click);
            // 
            // CtrlKarteTemplate1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MakeButton1);
            this.Controls.Add(this.KarteTemplatePanel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TemplateBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DeptBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SectionBox1);
            this.Name = "CtrlKarteTemplate1";
            this.Size = new System.Drawing.Size(600, 600);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox SectionBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox DeptBox1;
        private System.Windows.Forms.ComboBox TemplateBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel KarteTemplatePanel1;
        private System.Windows.Forms.Button MakeButton1;
    }
}
