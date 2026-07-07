using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class CtrlInHistoryBox2 : UserControl
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

        PatIn _PatIn = new PatIn();

        public PatIn PatIn1
        {
            get
            {
                return _PatIn;
            }
            set
            {
                this._PatIn = value;

                this.InTermBox.Text = this._PatIn.ToString();
            }
        }

        string _PtId = "";

        public string PtId
        {
            get
            {
                return this._PtId;
            }
            set
            {
                this._PtId = value;
            }
        }

        bool _ClearButtonVisible = true;

        public bool ClearButtonVisible
        {
            get
            {
                return this._ClearButtonVisible;
            }
            set
            {
                this._ClearButtonVisible = value;

                if (this._ClearButtonVisible)
                {
                    this.ClearButton.Visible = true;
                }
                else
                {
                    this.ClearButton.Visible = false;
                }
            }
        }

        public int InDate
        {
            get
            {
                int s = 0;

                if (this.InTermBox.Text.Contains(" "))
                {
                    s = DateTimeAgent.DateToInt(this.InTermBox.Text.Split(' ')[0]);
                }

                return s;
            }
        }

        public string InDateString
        {
            get
            {
                string s = "";

                if (this.InTermBox.Text.Contains(" "))
                {
                    s = this.InTermBox.Text.Split(' ')[0];
                }

                return s;
            }
        }

        public CtrlInHistoryBox2()
        {
            InitializeComponent();
            this.InTermBox.TextChanged += new EventHandler(InTermLabel_TextChanged);
        }

        void InTermLabel_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged(EventArgs.Empty);
        }

        public void Init(string pt_id, string in_date = "")
        {
            this._PtId = pt_id;

            List<PatIn> list = PatIn.GetHistory(pt_id);

            if (list.Count > 0)
            {
                if (AppString.IsDate(in_date))
                {
                    foreach (PatIn pin in list)
                    {
                        if (pin.InDate.Equals(in_date))
                        {
                            this._PatIn = pin;
                            this.InTermBox.Text = this._PatIn.ToString();
                            break;
                        }
                    }
                }
                else
                {
                    this._PatIn = list[0];
                    this.InTermBox.Text = this._PatIn.ToString();
                }
            }
        }

        public void Init(string pt_id, int in_seq)
        {
            this._PtId = pt_id;

            List<PatIn> list = PatIn.GetHistory(pt_id);

            if (list.Count > 0)
            {
                if (in_seq > 0)
                {
                    foreach (PatIn pin in list)
                    {
                        if (pin.SEQ.Equals(in_seq))
                        {
                            this._PatIn = pin;
                            this.InTermBox.Text = this._PatIn.ToString();
                            break;
                        }
                    }
                }
                else
                {
                    this._PatIn = list[0];
                    this.InTermBox.Text = this._PatIn.ToString();
                }
            }
        }

        private void InTermBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                this.DataSelect();
            }
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            this.DataSelect();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            this.DataClear();
        }

        void DataSelect()
        {
            PatIn p = FormFindPatIn.FindPatIn(this.PtId);

            if (p.InDate.Length == 8)
            {
                this._PatIn = p;
                this.InTermBox.Text = p.ToString();
                OnValueChanged(EventArgs.Empty);
            }
        }

        void DataClear()
        {
            this._PatIn = new PatIn();
            this.InTermBox.Clear();
        }
    }
}
