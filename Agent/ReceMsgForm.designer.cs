namespace MedicalLibrary.Agent
{
    partial class ReceMsgForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReceMsgForm));
            this.label8 = new System.Windows.Forms.Label();
            this.e_msg = new System.Windows.Forms.TextBox();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.menuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItem32 = new System.Windows.Forms.ToolStripMenuItem();
            this.label9 = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.menuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItem22 = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.sendButton31 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.logButton = new System.Windows.Forms.Button();
            this.sendToDefaultButton = new System.Windows.Forms.Button();
            this.sendToNoneButton = new System.Windows.Forms.Button();
            this.sendToAllButton = new System.Windows.Forms.Button();
            this.sendButton3 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.user_name = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.updateButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pt_msg = new System.Windows.Forms.TextBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.sendButton2 = new System.Windows.Forms.Button();
            this.sendButton1 = new System.Windows.Forms.Button();
            this.pt_name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pt_id = new System.Windows.Forms.TextBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.menuStrip3.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 12);
            this.label8.TabIndex = 59;
            this.label8.Text = "緊急メッセージ";
            // 
            // e_msg
            // 
            this.e_msg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.e_msg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.e_msg.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.e_msg.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.e_msg.Location = new System.Drawing.Point(5, 25);
            this.e_msg.MaxLength = 500;
            this.e_msg.Multiline = true;
            this.e_msg.Name = "e_msg";
            this.e_msg.ReadOnly = true;
            this.e_msg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.e_msg.Size = new System.Drawing.Size(230, 107);
            this.e_msg.TabIndex = 58;
            // 
            // listBox3
            // 
            this.listBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox3.ContextMenuStrip = this.menuStrip3;
            this.listBox3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listBox3.FormattingEnabled = true;
            this.listBox3.HorizontalScrollbar = true;
            this.listBox3.ItemHeight = 12;
            this.listBox3.Location = new System.Drawing.Point(5, 619);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(230, 88);
            this.listBox3.TabIndex = 46;
            this.listBox3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox3_KeyDown);
            // 
            // menuStrip3
            // 
            this.menuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem32});
            this.menuStrip3.Name = "menuStrip3";
            this.menuStrip3.Size = new System.Drawing.Size(101, 26);
            this.menuStrip3.Opening += new System.ComponentModel.CancelEventHandler(this.menuStrip3_Opening);
            // 
            // menuItem32
            // 
            this.menuItem32.Name = "menuItem32";
            this.menuItem32.Size = new System.Drawing.Size(100, 22);
            this.menuItem32.Text = "削除";
            this.menuItem32.Click += new System.EventHandler(this.menuItem32_Click);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 604);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 53;
            this.label9.Text = "コメントのみ";
            // 
            // listBox2
            // 
            this.listBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox2.ContextMenuStrip = this.menuStrip2;
            this.listBox2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.HorizontalScrollbar = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(5, 513);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(230, 88);
            this.listBox2.TabIndex = 45;
            this.listBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox2_KeyDown);
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem22});
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(101, 26);
            this.menuStrip2.Opening += new System.ComponentModel.CancelEventHandler(this.menuStrip2_Opening);
            // 
            // menuItem22
            // 
            this.menuItem22.Name = "menuItem22";
            this.menuItem22.Size = new System.Drawing.Size(100, 22);
            this.menuItem22.Text = "削除";
            this.menuItem22.Click += new System.EventHandler(this.menuItem22_Click);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.ContextMenuStrip = this.menuStrip1;
            this.listBox1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(5, 407);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(230, 88);
            this.listBox1.TabIndex = 43;
            this.listBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox1_KeyDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem11,
            this.menuItem12});
            this.menuStrip1.Name = "menuStrip11";
            this.menuStrip1.Size = new System.Drawing.Size(185, 48);
            this.menuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.menuStrip1_Opening);
            // 
            // menuItem11
            // 
            this.menuItem11.Name = "menuItem11";
            this.menuItem11.Size = new System.Drawing.Size(184, 22);
            this.menuItem11.Text = "入力完了メッセージ";
            this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Name = "menuItem12";
            this.menuItem12.Size = new System.Drawing.Size(184, 22);
            this.menuItem12.Text = "削除";
            this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 498);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 12);
            this.label3.TabIndex = 35;
            this.label3.Text = "会計入力できました";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 392);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 12);
            this.label1.TabIndex = 32;
            this.label1.Text = "会計入力急いでください";
            // 
            // sendButton31
            // 
            this.sendButton31.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sendButton31.Location = new System.Drawing.Point(127, 223);
            this.sendButton31.Name = "sendButton31";
            this.sendButton31.Size = new System.Drawing.Size(52, 40);
            this.sendButton31.TabIndex = 6;
            this.sendButton31.Text = "会計ストップ";
            this.sendButton31.UseVisualStyleBackColor = true;
            this.sendButton31.Click += new System.EventHandler(this.sendButton31_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // logButton
            // 
            this.logButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.logButton.Location = new System.Drawing.Point(95, 711);
            this.logButton.Name = "logButton";
            this.logButton.Size = new System.Drawing.Size(66, 26);
            this.logButton.TabIndex = 57;
            this.logButton.Text = "送信ログ";
            this.logButton.UseVisualStyleBackColor = true;
            this.logButton.Click += new System.EventHandler(this.logButton_Click);
            // 
            // sendToDefaultButton
            // 
            this.sendToDefaultButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sendToDefaultButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.sendToDefaultButton.Location = new System.Drawing.Point(5, 361);
            this.sendToDefaultButton.Name = "sendToDefaultButton";
            this.sendToDefaultButton.Size = new System.Drawing.Size(79, 24);
            this.sendToDefaultButton.TabIndex = 56;
            this.sendToDefaultButton.Text = "デフォルト";
            this.sendToDefaultButton.UseVisualStyleBackColor = true;
            this.sendToDefaultButton.Click += new System.EventHandler(this.sendToDefaultButton_Click);
            // 
            // sendToNoneButton
            // 
            this.sendToNoneButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sendToNoneButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.sendToNoneButton.Location = new System.Drawing.Point(167, 361);
            this.sendToNoneButton.Name = "sendToNoneButton";
            this.sendToNoneButton.Size = new System.Drawing.Size(68, 24);
            this.sendToNoneButton.TabIndex = 55;
            this.sendToNoneButton.Text = "全非選択";
            this.sendToNoneButton.UseVisualStyleBackColor = true;
            this.sendToNoneButton.Click += new System.EventHandler(this.sendToNoneButton_Click);
            // 
            // sendToAllButton
            // 
            this.sendToAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sendToAllButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.sendToAllButton.Location = new System.Drawing.Point(90, 361);
            this.sendToAllButton.Name = "sendToAllButton";
            this.sendToAllButton.Size = new System.Drawing.Size(71, 24);
            this.sendToAllButton.TabIndex = 54;
            this.sendToAllButton.Text = "全選択";
            this.sendToAllButton.UseVisualStyleBackColor = true;
            this.sendToAllButton.Click += new System.EventHandler(this.sendToAllButton_Click);
            // 
            // sendButton3
            // 
            this.sendButton3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sendButton3.Location = new System.Drawing.Point(185, 223);
            this.sendButton3.Name = "sendButton3";
            this.sendButton3.Size = new System.Drawing.Size(50, 40);
            this.sendButton3.TabIndex = 7;
            this.sendButton3.Text = "コメントのみ";
            this.sendButton3.UseVisualStyleBackColor = true;
            this.sendButton3.Click += new System.EventHandler(this.sendButton3_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(115, 141);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 52;
            this.label7.Text = "3文字以内";
            // 
            // user_name
            // 
            this.user_name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.user_name.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.user_name.Location = new System.Drawing.Point(48, 138);
            this.user_name.MaxLength = 3;
            this.user_name.Name = "user_name";
            this.user_name.Size = new System.Drawing.Size(61, 19);
            this.user_name.TabIndex = 1;
            this.user_name.KeyDown += new System.Windows.Forms.KeyEventHandler(this.user_name_KeyDown);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 49;
            this.label6.Text = "入力者";
            // 
            // updateButton
            // 
            this.updateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.updateButton.Location = new System.Drawing.Point(5, 711);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(84, 26);
            this.updateButton.TabIndex = 48;
            this.updateButton.Text = "リスト更新";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 268);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 47;
            this.label5.Text = "送信先";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 24);
            this.label4.TabIndex = 44;
            this.label4.Text = "コメント\r\n50字内";
            // 
            // pt_msg
            // 
            this.pt_msg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pt_msg.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.pt_msg.Location = new System.Drawing.Point(48, 187);
            this.pt_msg.MaxLength = 50;
            this.pt_msg.Multiline = true;
            this.pt_msg.Name = "pt_msg";
            this.pt_msg.Size = new System.Drawing.Size(187, 32);
            this.pt_msg.TabIndex = 3;
            this.pt_msg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pt_msg_KeyDown);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.ColumnWidth = 75;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.HorizontalScrollbar = true;
            this.checkedListBox1.Location = new System.Drawing.Point(5, 283);
            this.checkedListBox1.MultiColumn = true;
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(230, 74);
            this.checkedListBox1.TabIndex = 42;
            // 
            // sendButton2
            // 
            this.sendButton2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sendButton2.Location = new System.Drawing.Point(66, 223);
            this.sendButton2.Name = "sendButton2";
            this.sendButton2.Size = new System.Drawing.Size(55, 40);
            this.sendButton2.TabIndex = 5;
            this.sendButton2.Text = "入力完了";
            this.sendButton2.UseVisualStyleBackColor = true;
            this.sendButton2.Click += new System.EventHandler(this.sendButton2_Click);
            // 
            // sendButton1
            // 
            this.sendButton1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sendButton1.Location = new System.Drawing.Point(5, 223);
            this.sendButton1.Name = "sendButton1";
            this.sendButton1.Size = new System.Drawing.Size(55, 40);
            this.sendButton1.TabIndex = 4;
            this.sendButton1.Text = "入力至急";
            this.sendButton1.UseVisualStyleBackColor = true;
            this.sendButton1.Click += new System.EventHandler(this.sendButton1_Click);
            // 
            // pt_name
            // 
            this.pt_name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pt_name.Location = new System.Drawing.Point(111, 163);
            this.pt_name.MaxLength = 50;
            this.pt_name.Name = "pt_name";
            this.pt_name.ReadOnly = true;
            this.pt_name.Size = new System.Drawing.Size(124, 19);
            this.pt_name.TabIndex = 36;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 12);
            this.label2.TabIndex = 34;
            this.label2.Text = "患者ID";
            // 
            // pt_id
            // 
            this.pt_id.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pt_id.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.pt_id.Location = new System.Drawing.Point(48, 163);
            this.pt_id.MaxLength = 9;
            this.pt_id.Name = "pt_id";
            this.pt_id.Size = new System.Drawing.Size(61, 19);
            this.pt_id.TabIndex = 2;
            this.pt_id.Click += new System.EventHandler(this.pt_id_Click);
            this.pt_id.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pt_id_KeyDown);
            this.pt_id.Leave += new System.EventHandler(this.pt_id_Leave);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Location = new System.Drawing.Point(167, 711);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(68, 26);
            this.closeButton.TabIndex = 33;
            this.closeButton.Text = "閉じる";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // ReceMsgForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 741);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.e_msg);
            this.Controls.Add(this.listBox3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sendButton31);
            this.Controls.Add(this.logButton);
            this.Controls.Add(this.sendToDefaultButton);
            this.Controls.Add(this.sendToNoneButton);
            this.Controls.Add(this.sendToAllButton);
            this.Controls.Add(this.sendButton3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.user_name);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pt_msg);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.sendButton2);
            this.Controls.Add(this.sendButton1);
            this.Controls.Add(this.pt_name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pt_id);
            this.Controls.Add(this.closeButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReceMsgForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "レセメッセージ";
            this.Load += new System.EventHandler(this.ReceMsgForm_Load);
            this.menuStrip3.ResumeLayout(false);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox e_msg;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button sendButton31;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button logButton;
        private System.Windows.Forms.Button sendToDefaultButton;
        private System.Windows.Forms.Button sendToNoneButton;
        private System.Windows.Forms.Button sendToAllButton;
        private System.Windows.Forms.Button sendButton3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox user_name;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox pt_msg;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button sendButton2;
        private System.Windows.Forms.Button sendButton1;
        private System.Windows.Forms.TextBox pt_name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pt_id;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.ContextMenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItem11;
        private System.Windows.Forms.ToolStripMenuItem menuItem12;
        private System.Windows.Forms.ContextMenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem menuItem22;
        private System.Windows.Forms.ContextMenuStrip menuStrip3;
        private System.Windows.Forms.ToolStripMenuItem menuItem32;
    }
}