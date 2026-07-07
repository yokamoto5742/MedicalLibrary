namespace MedicalLibrary.Agent
{
    partial class FormBillPayCheckIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBillPayCheckIn));
            this.PlaceBox = new System.Windows.Forms.ComboBox();
            this.PlaceLabel = new System.Windows.Forms.Label();
            this.NumLabel1 = new System.Windows.Forms.Label();
            this.NumLabel2 = new System.Windows.Forms.Label();
            this.MsgLabel = new System.Windows.Forms.Label();
            this.PatSeqNeedBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // PlaceBox
            // 
            this.PlaceBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PlaceBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PlaceBox.Enabled = false;
            this.PlaceBox.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PlaceBox.FormattingEnabled = true;
            this.PlaceBox.Location = new System.Drawing.Point(814, 2);
            this.PlaceBox.Name = "PlaceBox";
            this.PlaceBox.Size = new System.Drawing.Size(121, 23);
            this.PlaceBox.TabIndex = 1;
            this.PlaceBox.Visible = false;
            // 
            // PlaceLabel
            // 
            this.PlaceLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PlaceLabel.AutoSize = true;
            this.PlaceLabel.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PlaceLabel.Location = new System.Drawing.Point(764, 6);
            this.PlaceLabel.Name = "PlaceLabel";
            this.PlaceLabel.Size = new System.Drawing.Size(37, 15);
            this.PlaceLabel.TabIndex = 2;
            this.PlaceLabel.Text = "場所";
            this.PlaceLabel.Visible = false;
            this.PlaceLabel.DoubleClick += new System.EventHandler(this.PlaceLabel_DoubleClick);
            // 
            // NumLabel1
            // 
            this.NumLabel1.Font = new System.Drawing.Font("HG丸ｺﾞｼｯｸM-PRO", 54F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NumLabel1.Location = new System.Drawing.Point(0, 100);
            this.NumLabel1.Name = "NumLabel1";
            this.NumLabel1.Size = new System.Drawing.Size(412, 110);
            this.NumLabel1.TabIndex = 3;
            this.NumLabel1.Text = "受付番号";
            this.NumLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // NumLabel2
            // 
            this.NumLabel2.Font = new System.Drawing.Font("HG丸ｺﾞｼｯｸM-PRO", 120F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NumLabel2.Location = new System.Drawing.Point(412, 45);
            this.NumLabel2.Name = "NumLabel2";
            this.NumLabel2.Size = new System.Drawing.Size(612, 200);
            this.NumLabel2.TabIndex = 4;
            this.NumLabel2.Text = "9999";
            this.NumLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MsgLabel
            // 
            this.MsgLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MsgLabel.Font = new System.Drawing.Font("HG丸ｺﾞｼｯｸM-PRO", 72F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MsgLabel.Location = new System.Drawing.Point(0, 320);
            this.MsgLabel.Name = "MsgLabel";
            this.MsgLabel.Size = new System.Drawing.Size(1014, 400);
            this.MsgLabel.TabIndex = 5;
            this.MsgLabel.Text = "受付票を\r\nバーコードリーダーに\r\n通してください";
            this.MsgLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.MsgLabel.DoubleClick += new System.EventHandler(this.MsgLabel_DoubleClick);
            // 
            // PatSeqNeedBox
            // 
            this.PatSeqNeedBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PatSeqNeedBox.AutoSize = true;
            this.PatSeqNeedBox.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PatSeqNeedBox.Location = new System.Drawing.Point(524, 6);
            this.PatSeqNeedBox.Name = "PatSeqNeedBox";
            this.PatSeqNeedBox.Size = new System.Drawing.Size(161, 19);
            this.PatSeqNeedBox.TabIndex = 6;
            this.PatSeqNeedBox.Text = "受付番号がある人のみ";
            this.PatSeqNeedBox.UseVisualStyleBackColor = true;
            this.PatSeqNeedBox.Visible = false;
            // 
            // FormBillPayCheckIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 800);
            this.Controls.Add(this.PatSeqNeedBox);
            this.Controls.Add(this.MsgLabel);
            this.Controls.Add(this.NumLabel2);
            this.Controls.Add(this.NumLabel1);
            this.Controls.Add(this.PlaceLabel);
            this.Controls.Add(this.PlaceBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormBillPayCheckIn";
            this.Text = "会計受付";
            this.Load += new System.EventHandler(this.FormBillPayCheckIn_Load);
            this.DoubleClick += new System.EventHandler(this.FormBillPayCheckIn_DoubleClick);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormBillPayCheckIn_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormBillPayCheckIn_KeyPress);
            this.Resize += new System.EventHandler(this.FormBillPayCheckIn_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox PlaceBox;
        private System.Windows.Forms.Label PlaceLabel;
        private System.Windows.Forms.Label NumLabel1;
        private System.Windows.Forms.Label NumLabel2;
        private System.Windows.Forms.Label MsgLabel;
        private System.Windows.Forms.CheckBox PatSeqNeedBox;
    }
}