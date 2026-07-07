using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class CtrlDateBox1 : UserControl
    {
        public event EventHandler<EventArgs> ValueChanged;

        [Browsable(true)]
        protected virtual void OnValueChanged(EventArgs e)
        {
            EventHandler<EventArgs> eventHandler = ValueChanged;

            if (eventHandler != null)
            {
                eventHandler(this, e);
            }
        }

        public bool SelectShow
        {
            set
            {
                if (this.SelectButton1 != null)
                {
                    this.SelectButton1.Enabled = value;
                }
            }
        }

        public bool ClearShow
        {
            set
            {
                if (this.ClearButton1 != null)
                {
                    this.ClearButton1.Enabled = value;
                }
            }
        }

        public string DateString
        {
            get
            {
                string s = "";

                DateTime dt = DateTime.Now;

                if (DateTime.TryParse(this.DateBox1.Text, out dt))
                {
                    s = dt.ToString("yyyy/MM/dd"); 
                }

                return s;
            }
            set
            {
                if (value.Length == 10)
                {
                    this.DateBox1.Text = value;
                }
                else if (value.Length == 8)
                {
                    this.DateBox1.Text = value.Insert(4, "/").Insert(7, "/");
                }
            }
        }

        /// <summary>
        /// get のみ。set はできない（set すると初期値がおかしくなる）。
        /// </summary>
        public DateTime DateValue
        {
            get
            {
                DateTime dt = DateTime.Now;

                DateTime.TryParse(this.DateBox1.Text, out dt);

                return dt;
            }
        }

        public int DateInt
        {
            get
            {
                int d = 0;

                DateTime dt = DateTime.Now;

                if (DateTime.TryParse(this.DateBox1.Text, out dt))
                {
                    d = int.Parse(dt.ToString("yyyyMMdd"));
                }

                return d;
            }
            set
            {
                if (value.ToString().Length == 8)
                {
                    this.DateBox1.Text = value.ToString().Insert(4, "/").Insert(7, "/");
                }
            }
        }

        public CtrlDateBox1()
        {
            InitializeComponent();
            this.DateBox1.TextChanged += new EventHandler(DateBox1_TextChanged);
        }

        void DateBox1_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged(EventArgs.Empty);
        }

        public void Clear()
        {
            this.DateBox1.Clear();
        }

        private void SelectButton1_Click(object sender, EventArgs e)
        {
            this.DateSelect();
        }

        void DateSelect()
        {
            FormDateSelector f = new FormDateSelector(this.DateBox1.Text);

            if (f.ShowDialog() == DialogResult.OK)
            {
                this.DateBox1.Text = DateTimeAgent.DateFormat(f.Date, DateTimeAgent.DateFormatKind.LONG);
            }
        }

        private void ClearButton1_Click(object sender, EventArgs e)
        {
            this.DateBox1.Clear();
        }

        private void DateBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // 空白ならば、今日の日付を入力する
                if (this.DateBox1.Text.Length == 0)
                {
                    this.DateBox1.Text = DateTime.Now.ToString("yyyy/MM/dd");
                }
                else
                {
                    DateTime dt = DateTime.Now;

                    if (!DateTime.TryParse(this.DateBox1.Text, out dt))
                    {
                        MessageBox.Show("日付の形式が正しくありません");
                        this.DateBox1.Clear();
                    }
                }
            }
            else if (e.KeyCode == Keys.F3)
            {
                this.DateSelect();
            }
        }

        private void DateBox1_Leave(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;

            if (this.DateBox1.Text.Length > 0)
            {
                if (!DateTime.TryParse(this.DateBox1.Text, out dt))
                {
                    MessageBox.Show("日付の形式が正しくありません");
                    this.DateBox1.Clear();
                }
            }
        }
    }
}
