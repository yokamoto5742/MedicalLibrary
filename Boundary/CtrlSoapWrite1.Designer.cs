namespace MedicalLibrary.Boundary
{
    partial class CtrlSoapWrite1
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
            this.InOutLabel1 = new System.Windows.Forms.Label();
            this.InOutLabel2 = new System.Windows.Forms.Label();
            this.DatePicker1 = new System.Windows.Forms.DateTimePicker();
            this.InsBox1 = new System.Windows.Forms.ComboBox();
            this.DeptBox1 = new MedicalLibrary.Boundary.CtrlDeptBox1();
            this.SuspendLayout();
            // 
            // InOutLabel1
            // 
            this.InOutLabel1.AutoSize = true;
            this.InOutLabel1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.InOutLabel1.Location = new System.Drawing.Point(5, 30);
            this.InOutLabel1.Name = "InOutLabel1";
            this.InOutLabel1.Size = new System.Drawing.Size(42, 16);
            this.InOutLabel1.TabIndex = 16;
            this.InOutLabel1.Text = "外来";
            this.InOutLabel1.DoubleClick += new System.EventHandler(this.InOutLabel1_DoubleClick);
            // 
            // InOutLabel2
            // 
            this.InOutLabel2.AutoSize = true;
            this.InOutLabel2.Location = new System.Drawing.Point(80, 33);
            this.InOutLabel2.Name = "InOutLabel2";
            this.InOutLabel2.Size = new System.Drawing.Size(254, 12);
            this.InOutLabel2.TabIndex = 17;
            this.InOutLabel2.Text = "※ダブルクリックで外来・入院モードが切り替わります。";
            this.InOutLabel2.DoubleClick += new System.EventHandler(this.InOutLabel2_DoubleClick);
            // 
            // DatePicker1
            // 
            this.DatePicker1.CustomFormat = "yyyy年M月d日 (ddd)";
            this.DatePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatePicker1.Location = new System.Drawing.Point(5, 5);
            this.DatePicker1.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.DatePicker1.Name = "DatePicker1";
            this.DatePicker1.Size = new System.Drawing.Size(150, 19);
            this.DatePicker1.TabIndex = 18;
            // 
            // InsBox1
            // 
            this.InsBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InsBox1.FormattingEnabled = true;
            this.InsBox1.Location = new System.Drawing.Point(255, 4);
            this.InsBox1.Name = "InsBox1";
            this.InsBox1.Size = new System.Drawing.Size(70, 20);
            this.InsBox1.TabIndex = 21;
            // 
            // DeptBox1
            // 
            this.DeptBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeptBox1.FormattingEnabled = true;
            this.DeptBox1.Location = new System.Drawing.Point(160, 4);
            this.DeptBox1.Name = "DeptBox1";
            this.DeptBox1.Size = new System.Drawing.Size(90, 20);
            this.DeptBox1.TabIndex = 22;
            // 
            // CtrlSoapWrite1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DeptBox1);
            this.Controls.Add(this.InsBox1);
            this.Controls.Add(this.DatePicker1);
            this.Controls.Add(this.InOutLabel2);
            this.Controls.Add(this.InOutLabel1);
            this.Name = "CtrlSoapWrite1";
            this.Size = new System.Drawing.Size(500, 700);
            this.Load += new System.EventHandler(this.CtrlSoapWrite1_Load);
            this.DoubleClick += new System.EventHandler(this.CtrlSoapWrite1_DoubleClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label InOutLabel1;
        private System.Windows.Forms.Label InOutLabel2;
        private System.Windows.Forms.DateTimePicker DatePicker1;
        private System.Windows.Forms.ComboBox InsBox1;
        private CtrlDeptBox1 DeptBox1;
    }
}
