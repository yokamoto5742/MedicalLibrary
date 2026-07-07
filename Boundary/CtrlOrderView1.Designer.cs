namespace MedicalLibrary.Boundary
{
    partial class CtrlOrderView1
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
            this.components = new System.ComponentModel.Container();
            this.DoBox1 = new System.Windows.Forms.CheckBox();
            this.DateLabel1 = new System.Windows.Forms.Label();
            this.ContLabel1 = new System.Windows.Forms.Label();
            this.DoctorLabel1 = new System.Windows.Forms.Label();
            this.TimesLabel1 = new System.Windows.Forms.Label();
            this.TermLabel1 = new System.Windows.Forms.Label();
            this.TimesLabel0 = new System.Windows.Forms.Label();
            this.KouiLabel1 = new System.Windows.Forms.Label();
            this.DeptLabel1 = new System.Windows.Forms.Label();
            this.CtrlOrderMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.KaikeiFlgMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KaikeiFlg1MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KaikeiFlg0MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SekouFlgMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SekouFlg1MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SekouFlg0MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PaperFlgMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PaperFlg1MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PaperFlg0MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CtrlOrderMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DoBox1
            // 
            this.DoBox1.AutoSize = true;
            this.DoBox1.Location = new System.Drawing.Point(5, 5);
            this.DoBox1.Name = "DoBox1";
            this.DoBox1.Size = new System.Drawing.Size(15, 14);
            this.DoBox1.TabIndex = 0;
            this.DoBox1.UseVisualStyleBackColor = true;
            this.DoBox1.CheckedChanged += new System.EventHandler(this.DoBox1_CheckedChanged);
            // 
            // DateLabel1
            // 
            this.DateLabel1.AutoEllipsis = true;
            this.DateLabel1.BackColor = System.Drawing.Color.White;
            this.DateLabel1.Location = new System.Drawing.Point(25, 3);
            this.DateLabel1.Name = "DateLabel1";
            this.DateLabel1.Padding = new System.Windows.Forms.Padding(2);
            this.DateLabel1.Size = new System.Drawing.Size(70, 18);
            this.DateLabel1.TabIndex = 1;
            this.DateLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ContLabel1
            // 
            this.ContLabel1.AutoEllipsis = true;
            this.ContLabel1.BackColor = System.Drawing.Color.White;
            this.ContLabel1.Location = new System.Drawing.Point(80, 25);
            this.ContLabel1.Name = "ContLabel1";
            this.ContLabel1.Padding = new System.Windows.Forms.Padding(2);
            this.ContLabel1.Size = new System.Drawing.Size(395, 18);
            this.ContLabel1.TabIndex = 3;
            // 
            // DoctorLabel1
            // 
            this.DoctorLabel1.AutoEllipsis = true;
            this.DoctorLabel1.BackColor = System.Drawing.Color.LightYellow;
            this.DoctorLabel1.Location = new System.Drawing.Point(180, 3);
            this.DoctorLabel1.Name = "DoctorLabel1";
            this.DoctorLabel1.Padding = new System.Windows.Forms.Padding(2);
            this.DoctorLabel1.Size = new System.Drawing.Size(85, 18);
            this.DoctorLabel1.TabIndex = 4;
            this.DoctorLabel1.Text = "\r\n";
            this.DoctorLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TimesLabel1
            // 
            this.TimesLabel1.AutoEllipsis = true;
            this.TimesLabel1.BackColor = System.Drawing.Color.LightYellow;
            this.TimesLabel1.Location = new System.Drawing.Point(317, 3);
            this.TimesLabel1.Name = "TimesLabel1";
            this.TimesLabel1.Padding = new System.Windows.Forms.Padding(2);
            this.TimesLabel1.Size = new System.Drawing.Size(35, 18);
            this.TimesLabel1.TabIndex = 5;
            this.TimesLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TermLabel1
            // 
            this.TermLabel1.AutoEllipsis = true;
            this.TermLabel1.BackColor = System.Drawing.Color.LightYellow;
            this.TermLabel1.Location = new System.Drawing.Point(355, 3);
            this.TermLabel1.Name = "TermLabel1";
            this.TermLabel1.Padding = new System.Windows.Forms.Padding(2);
            this.TermLabel1.Size = new System.Drawing.Size(120, 18);
            this.TermLabel1.TabIndex = 6;
            this.TermLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TimesLabel0
            // 
            this.TimesLabel0.AutoSize = true;
            this.TimesLabel0.Location = new System.Drawing.Point(270, 6);
            this.TimesLabel0.Name = "TimesLabel0";
            this.TimesLabel0.Size = new System.Drawing.Size(47, 12);
            this.TimesLabel0.TabIndex = 7;
            this.TimesLabel0.Text = "日/回数";
            // 
            // KouiLabel1
            // 
            this.KouiLabel1.AutoEllipsis = true;
            this.KouiLabel1.BackColor = System.Drawing.Color.White;
            this.KouiLabel1.Location = new System.Drawing.Point(25, 25);
            this.KouiLabel1.Name = "KouiLabel1";
            this.KouiLabel1.Padding = new System.Windows.Forms.Padding(2);
            this.KouiLabel1.Size = new System.Drawing.Size(50, 18);
            this.KouiLabel1.TabIndex = 8;
            this.KouiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DeptLabel1
            // 
            this.DeptLabel1.AutoEllipsis = true;
            this.DeptLabel1.BackColor = System.Drawing.Color.LightYellow;
            this.DeptLabel1.Location = new System.Drawing.Point(100, 3);
            this.DeptLabel1.Name = "DeptLabel1";
            this.DeptLabel1.Padding = new System.Windows.Forms.Padding(2);
            this.DeptLabel1.Size = new System.Drawing.Size(75, 18);
            this.DeptLabel1.TabIndex = 9;
            this.DeptLabel1.Text = "\r\n";
            this.DeptLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CtrlOrderMenuStrip1
            // 
            this.CtrlOrderMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SekouFlgMenuItem,
            this.KaikeiFlgMenuItem,
            this.PaperFlgMenuItem});
            this.CtrlOrderMenuStrip1.Name = "CtrlOrderMenuStrip1";
            this.CtrlOrderMenuStrip1.Size = new System.Drawing.Size(153, 92);
            // 
            // KaikeiFlgMenuItem
            // 
            this.KaikeiFlgMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.KaikeiFlg1MenuItem,
            this.KaikeiFlg0MenuItem});
            this.KaikeiFlgMenuItem.Name = "KaikeiFlgMenuItem";
            this.KaikeiFlgMenuItem.Size = new System.Drawing.Size(148, 22);
            this.KaikeiFlgMenuItem.Text = "会計フラグ";
            // 
            // KaikeiFlg1MenuItem
            // 
            this.KaikeiFlg1MenuItem.Name = "KaikeiFlg1MenuItem";
            this.KaikeiFlg1MenuItem.Size = new System.Drawing.Size(112, 22);
            this.KaikeiFlg1MenuItem.Text = "取込済";
            this.KaikeiFlg1MenuItem.Click += new System.EventHandler(this.KaikeiFlg1MenuItem_Click);
            // 
            // KaikeiFlg0MenuItem
            // 
            this.KaikeiFlg0MenuItem.Name = "KaikeiFlg0MenuItem";
            this.KaikeiFlg0MenuItem.Size = new System.Drawing.Size(112, 22);
            this.KaikeiFlg0MenuItem.Text = "未取込";
            this.KaikeiFlg0MenuItem.Click += new System.EventHandler(this.KaikeiFlg0MenuItem_Click);
            // 
            // SekouFlgMenuItem
            // 
            this.SekouFlgMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SekouFlg1MenuItem,
            this.SekouFlg0MenuItem});
            this.SekouFlgMenuItem.Name = "SekouFlgMenuItem";
            this.SekouFlgMenuItem.Size = new System.Drawing.Size(148, 22);
            this.SekouFlgMenuItem.Text = "施行フラグ";
            // 
            // SekouFlg1MenuItem
            // 
            this.SekouFlg1MenuItem.Name = "SekouFlg1MenuItem";
            this.SekouFlg1MenuItem.Size = new System.Drawing.Size(112, 22);
            this.SekouFlg1MenuItem.Text = "施行済";
            this.SekouFlg1MenuItem.Click += new System.EventHandler(this.SekouFlg1MenuItem_Click);
            // 
            // SekouFlg0MenuItem
            // 
            this.SekouFlg0MenuItem.Name = "SekouFlg0MenuItem";
            this.SekouFlg0MenuItem.Size = new System.Drawing.Size(112, 22);
            this.SekouFlg0MenuItem.Text = "未施行";
            this.SekouFlg0MenuItem.Click += new System.EventHandler(this.SekouFlg0MenuItem_Click);
            // 
            // PaperFlgMenuItem
            // 
            this.PaperFlgMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PaperFlg1MenuItem,
            this.PaperFlg0MenuItem});
            this.PaperFlgMenuItem.Name = "PaperFlgMenuItem";
            this.PaperFlgMenuItem.Size = new System.Drawing.Size(152, 22);
            this.PaperFlgMenuItem.Text = "指示箋フラグ";
            this.PaperFlgMenuItem.Visible = false;
            // 
            // PaperFlg1MenuItem
            // 
            this.PaperFlg1MenuItem.Name = "PaperFlg1MenuItem";
            this.PaperFlg1MenuItem.Size = new System.Drawing.Size(152, 22);
            this.PaperFlg1MenuItem.Text = "発行済";
            this.PaperFlg1MenuItem.Click += new System.EventHandler(this.PaperFlg1MenuItem_Click);
            // 
            // PaperFlg0MenuItem
            // 
            this.PaperFlg0MenuItem.Name = "PaperFlg0MenuItem";
            this.PaperFlg0MenuItem.Size = new System.Drawing.Size(152, 22);
            this.PaperFlg0MenuItem.Text = "未発行";
            this.PaperFlg0MenuItem.Click += new System.EventHandler(this.PaperFlg0MenuItem_Click);
            // 
            // CtrlOrderView1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContextMenuStrip = this.CtrlOrderMenuStrip1;
            this.Controls.Add(this.DeptLabel1);
            this.Controls.Add(this.KouiLabel1);
            this.Controls.Add(this.TimesLabel0);
            this.Controls.Add(this.TermLabel1);
            this.Controls.Add(this.TimesLabel1);
            this.Controls.Add(this.DoctorLabel1);
            this.Controls.Add(this.ContLabel1);
            this.Controls.Add(this.DateLabel1);
            this.Controls.Add(this.DoBox1);
            this.Name = "CtrlOrderView1";
            this.Size = new System.Drawing.Size(480, 150);
            this.CtrlOrderMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox DoBox1;
        private System.Windows.Forms.Label DateLabel1;
        private System.Windows.Forms.Label ContLabel1;
        private System.Windows.Forms.Label DoctorLabel1;
        private System.Windows.Forms.Label TimesLabel1;
        private System.Windows.Forms.Label TermLabel1;
        private System.Windows.Forms.Label TimesLabel0;
        private System.Windows.Forms.Label KouiLabel1;
        private System.Windows.Forms.Label DeptLabel1;
        private System.Windows.Forms.ContextMenuStrip CtrlOrderMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem KaikeiFlgMenuItem;
        private System.Windows.Forms.ToolStripMenuItem KaikeiFlg1MenuItem;
        private System.Windows.Forms.ToolStripMenuItem KaikeiFlg0MenuItem;
        private System.Windows.Forms.ToolStripMenuItem SekouFlgMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SekouFlg1MenuItem;
        private System.Windows.Forms.ToolStripMenuItem SekouFlg0MenuItem;
        private System.Windows.Forms.ToolStripMenuItem PaperFlgMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PaperFlg1MenuItem;
        private System.Windows.Forms.ToolStripMenuItem PaperFlg0MenuItem;
    }
}
