namespace MedicalLibrary.Boundary
{
    partial class FormKarteMessage1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormKarteMessage1));
            this.ListView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ReadMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.UnReadMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ContBox1 = new System.Windows.Forms.TextBox();
            this.KindButton1 = new System.Windows.Forms.RadioButton();
            this.KindButton2 = new System.Windows.Forms.RadioButton();
            this.TitleBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ReplyButton = new System.Windows.Forms.Button();
            this.DeleteButton1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.PtBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ReadButton = new System.Windows.Forms.Button();
            this.UnReadButton = new System.Windows.Forms.Button();
            this.PtNameBox1 = new System.Windows.Forms.TextBox();
            this.NewButton = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.ShowButton = new System.Windows.Forms.Button();
            this.ReplyAllButton = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.PriorityBox1 = new System.Windows.Forms.TextBox();
            this.KarteButton1 = new System.Windows.Forms.Button();
            this.FromNameBox1 = new System.Windows.Forms.TextBox();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListView1
            // 
            this.ListView1.AllowUserToAddRows = false;
            this.ListView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.ListView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListView1.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ListView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.ListView1.Location = new System.Drawing.Point(10, 40);
            this.ListView1.Name = "ListView1";
            this.ListView1.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ListView1.RowHeadersWidth = 30;
            this.ListView1.RowTemplate.Height = 21;
            this.ListView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ListView1.Size = new System.Drawing.Size(770, 160);
            this.ListView1.TabIndex = 4;
            this.ListView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListView1_RowEnter);
            this.ListView1.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListView1_RowLeave);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ReadMenuItem1,
            this.UnReadMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 48);
            // 
            // ReadMenuItem1
            // 
            this.ReadMenuItem1.Name = "ReadMenuItem1";
            this.ReadMenuItem1.Size = new System.Drawing.Size(160, 22);
            this.ReadMenuItem1.Text = "開封済みにする";
            this.ReadMenuItem1.Click += new System.EventHandler(this.ReadMenuItem1_Click);
            // 
            // UnReadMenuItem1
            // 
            this.UnReadMenuItem1.Name = "UnReadMenuItem1";
            this.UnReadMenuItem1.Size = new System.Drawing.Size(160, 22);
            this.UnReadMenuItem1.Text = "未開封にする";
            this.UnReadMenuItem1.Click += new System.EventHandler(this.UnReadMenuItem1_Click);
            // 
            // ContBox1
            // 
            this.ContBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContBox1.BackColor = System.Drawing.SystemColors.Info;
            this.ContBox1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ContBox1.Location = new System.Drawing.Point(230, 235);
            this.ContBox1.Multiline = true;
            this.ContBox1.Name = "ContBox1";
            this.ContBox1.ReadOnly = true;
            this.ContBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ContBox1.Size = new System.Drawing.Size(550, 140);
            this.ContBox1.TabIndex = 7;
            // 
            // KindButton1
            // 
            this.KindButton1.AutoSize = true;
            this.KindButton1.Location = new System.Drawing.Point(20, 14);
            this.KindButton1.Name = "KindButton1";
            this.KindButton1.Size = new System.Drawing.Size(75, 16);
            this.KindButton1.TabIndex = 2;
            this.KindButton1.TabStop = true;
            this.KindButton1.Text = "受信メール";
            this.KindButton1.UseVisualStyleBackColor = true;
            this.KindButton1.CheckedChanged += new System.EventHandler(this.KindButton1_CheckedChanged);
            // 
            // KindButton2
            // 
            this.KindButton2.AutoSize = true;
            this.KindButton2.Location = new System.Drawing.Point(120, 14);
            this.KindButton2.Name = "KindButton2";
            this.KindButton2.Size = new System.Drawing.Size(98, 16);
            this.KindButton2.TabIndex = 3;
            this.KindButton2.TabStop = true;
            this.KindButton2.Text = "送信済みメール";
            this.KindButton2.UseVisualStyleBackColor = true;
            this.KindButton2.CheckedChanged += new System.EventHandler(this.KindButton2_CheckedChanged);
            // 
            // TitleBox1
            // 
            this.TitleBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TitleBox1.BackColor = System.Drawing.SystemColors.Info;
            this.TitleBox1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TitleBox1.Location = new System.Drawing.Point(480, 210);
            this.TitleBox1.Name = "TitleBox1";
            this.TitleBox1.ReadOnly = true;
            this.TitleBox1.Size = new System.Drawing.Size(300, 19);
            this.TitleBox1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(10, 213);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "メール内容";
            // 
            // ReplyButton
            // 
            this.ReplyButton.Location = new System.Drawing.Point(540, 378);
            this.ReplyButton.Name = "ReplyButton";
            this.ReplyButton.Size = new System.Drawing.Size(70, 24);
            this.ReplyButton.TabIndex = 8;
            this.ReplyButton.Text = "返信";
            this.ReplyButton.UseVisualStyleBackColor = true;
            this.ReplyButton.Click += new System.EventHandler(this.ReplyButton_Click);
            // 
            // DeleteButton1
            // 
            this.DeleteButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteButton1.Location = new System.Drawing.Point(720, 378);
            this.DeleteButton1.Name = "DeleteButton1";
            this.DeleteButton1.Size = new System.Drawing.Size(60, 24);
            this.DeleteButton1.TabIndex = 9;
            this.DeleteButton1.Text = "削除";
            this.DeleteButton1.UseVisualStyleBackColor = true;
            this.DeleteButton1.Click += new System.EventHandler(this.DeleteButton1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(445, 213);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 24;
            this.label6.Text = "件名";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(85, 213);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 12);
            this.label7.TabIndex = 26;
            this.label7.Text = "ID";
            // 
            // PtBox1
            // 
            this.PtBox1.BackColor = System.Drawing.SystemColors.Info;
            this.PtBox1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.PtBox1.Location = new System.Drawing.Point(105, 210);
            this.PtBox1.Name = "PtBox1";
            this.PtBox1.ReadOnly = true;
            this.PtBox1.Size = new System.Drawing.Size(60, 19);
            this.PtBox1.TabIndex = 5;
            this.PtBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.KindButton2);
            this.groupBox1.Controls.Add(this.KindButton1);
            this.groupBox1.Location = new System.Drawing.Point(10, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 35);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ReadButton
            // 
            this.ReadButton.Location = new System.Drawing.Point(230, 378);
            this.ReadButton.Name = "ReadButton";
            this.ReadButton.Size = new System.Drawing.Size(100, 24);
            this.ReadButton.TabIndex = 29;
            this.ReadButton.Text = "開封済みにする";
            this.ReadButton.UseVisualStyleBackColor = true;
            this.ReadButton.Click += new System.EventHandler(this.ReadButton_Click);
            // 
            // UnReadButton
            // 
            this.UnReadButton.Location = new System.Drawing.Point(335, 378);
            this.UnReadButton.Name = "UnReadButton";
            this.UnReadButton.Size = new System.Drawing.Size(90, 24);
            this.UnReadButton.TabIndex = 30;
            this.UnReadButton.Text = "未開封にする";
            this.UnReadButton.UseVisualStyleBackColor = true;
            this.UnReadButton.Click += new System.EventHandler(this.UnReadButton_Click);
            // 
            // PtNameBox1
            // 
            this.PtNameBox1.BackColor = System.Drawing.SystemColors.Info;
            this.PtNameBox1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.PtNameBox1.Location = new System.Drawing.Point(168, 210);
            this.PtNameBox1.Name = "PtNameBox1";
            this.PtNameBox1.ReadOnly = true;
            this.PtNameBox1.Size = new System.Drawing.Size(100, 19);
            this.PtNameBox1.TabIndex = 31;
            this.PtNameBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // NewButton
            // 
            this.NewButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NewButton.Location = new System.Drawing.Point(710, 10);
            this.NewButton.Name = "NewButton";
            this.NewButton.Size = new System.Drawing.Size(70, 24);
            this.NewButton.TabIndex = 39;
            this.NewButton.Text = "新規作成";
            this.NewButton.UseVisualStyleBackColor = true;
            this.NewButton.Click += new System.EventHandler(this.NewButton_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(340, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(248, 12);
            this.label10.TabIndex = 41;
            this.label10.Text = "太字は未開封メール、細字は開封済みメールです。";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(340, 7);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(362, 12);
            this.label11.TabIndex = 42;
            this.label11.Text = "メールの内容を表示するには、下記リストから該当メールをクリックしてください。";
            // 
            // ShowButton
            // 
            this.ShowButton.Location = new System.Drawing.Point(260, 10);
            this.ShowButton.Name = "ShowButton";
            this.ShowButton.Size = new System.Drawing.Size(70, 24);
            this.ShowButton.TabIndex = 43;
            this.ShowButton.Text = "更新";
            this.ShowButton.UseVisualStyleBackColor = true;
            this.ShowButton.Click += new System.EventHandler(this.ShowButton_Click);
            // 
            // ReplyAllButton
            // 
            this.ReplyAllButton.Location = new System.Drawing.Point(615, 378);
            this.ReplyAllButton.Name = "ReplyAllButton";
            this.ReplyAllButton.Size = new System.Drawing.Size(80, 24);
            this.ReplyAllButton.TabIndex = 46;
            this.ReplyAllButton.Text = "全員に返信";
            this.ReplyAllButton.UseVisualStyleBackColor = true;
            this.ReplyAllButton.Click += new System.EventHandler(this.ReplyAllButton_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(15, 265);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 53;
            this.label12.Text = "送信先";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(345, 213);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 54;
            this.label13.Text = "重要度";
            // 
            // PriorityBox1
            // 
            this.PriorityBox1.BackColor = System.Drawing.SystemColors.Info;
            this.PriorityBox1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.PriorityBox1.Location = new System.Drawing.Point(390, 210);
            this.PriorityBox1.Name = "PriorityBox1";
            this.PriorityBox1.ReadOnly = true;
            this.PriorityBox1.Size = new System.Drawing.Size(40, 19);
            this.PriorityBox1.TabIndex = 60;
            this.PriorityBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // KarteButton1
            // 
            this.KarteButton1.Location = new System.Drawing.Point(270, 207);
            this.KarteButton1.Name = "KarteButton1";
            this.KarteButton1.Size = new System.Drawing.Size(60, 24);
            this.KarteButton1.TabIndex = 61;
            this.KarteButton1.Text = "カルテ";
            this.KarteButton1.UseVisualStyleBackColor = true;
            this.KarteButton1.Click += new System.EventHandler(this.KarteButton1_Click);
            // 
            // FromNameBox1
            // 
            this.FromNameBox1.BackColor = System.Drawing.SystemColors.Info;
            this.FromNameBox1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.FromNameBox1.Location = new System.Drawing.Point(62, 235);
            this.FromNameBox1.Name = "FromNameBox1";
            this.FromNameBox1.ReadOnly = true;
            this.FromNameBox1.Size = new System.Drawing.Size(90, 19);
            this.FromNameBox1.TabIndex = 63;
            this.FromNameBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Panel1
            // 
            this.Panel1.AutoScroll = true;
            this.Panel1.Location = new System.Drawing.Point(60, 260);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(168, 140);
            this.Panel1.TabIndex = 64;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(15, 238);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 71;
            this.label14.Text = "送信者";
            // 
            // FormKarteMessage1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 412);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.FromNameBox1);
            this.Controls.Add(this.KarteButton1);
            this.Controls.Add(this.PriorityBox1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.ReplyAllButton);
            this.Controls.Add(this.ShowButton);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.NewButton);
            this.Controls.Add(this.PtNameBox1);
            this.Controls.Add(this.UnReadButton);
            this.Controls.Add(this.ReadButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.PtBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.DeleteButton1);
            this.Controls.Add(this.ReplyButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TitleBox1);
            this.Controls.Add(this.ContBox1);
            this.Controls.Add(this.ListView1);
            this.Controls.Add(this.Panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormKarteMessage1";
            this.Text = "カルテメッセージ";
            this.Load += new System.EventHandler(this.FormKarteMessage1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ListView1;
        private System.Windows.Forms.TextBox ContBox1;
        private System.Windows.Forms.RadioButton KindButton1;
        private System.Windows.Forms.RadioButton KindButton2;
        private System.Windows.Forms.TextBox TitleBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ReplyButton;
        private System.Windows.Forms.Button DeleteButton1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox PtBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button ReadButton;
        private System.Windows.Forms.Button UnReadButton;
        private System.Windows.Forms.TextBox PtNameBox1;
        private System.Windows.Forms.Button NewButton;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button ShowButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ReadMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem UnReadMenuItem1;
        private System.Windows.Forms.Button ReplyAllButton;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox PriorityBox1;
        private System.Windows.Forms.Button KarteButton1;
        private System.Windows.Forms.TextBox FromNameBox1;
        private System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.Label label14;
    }
}

