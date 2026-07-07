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
    public partial class CtrlSoapWrite1 : UserControl
    {
        SoapKey SoapKey1 = new SoapKey();

        string PtId = "";

        string in_out = "1";

        public string InOut
        {
            get
            {
                return this.in_out;
            }
            set
            {
                this.in_out = value;

                if (this.in_out.Equals("1"))
                {
                    this.InOutLabel1.Text = "外来";
                    this.BackColor = Color.LightGreen;
                }
                else if (this.in_out.Equals("2"))
                {
                    this.InOutLabel1.Text = "入院";
                    this.BackColor = Color.LightPink;
                }
            }
        }

        public CtrlSoapWrite1()
        {
            InitializeComponent();
        }

        public void PatSet(string pt_id)
        {
            this.PtId = pt_id;
        }

        public void DataShow(SoapHeader header, bool append = true)
        {
            this.Clear();

            this.SoapKey1 = (SoapKey)header;

            this.DatePicker1.Value = DateTimeAgent.DateTimeFromInt(header.SoapDate);
            this.DeptBox1.SetDept(header.Dept);
            this.InsBox1.Text = header.InsKindName;
            this.InOut = header.InOut.ToString();

            foreach (SoapDetail data in header.DetailList)
            {
                if (this.Controls.ContainsKey("soap" + data.Kind))
                {
                    CtrlSoapPanel1 p = (CtrlSoapPanel1)this.Controls["soap" + data.Kind];
                    p.DataShow(data, append);
                }
            }
        }

        public void SoapWrite(Dictionary<string, string> dict, bool append = true)
        {
            foreach (string key in dict.Keys)
            {
                if (dict.ContainsKey(key))
                {
                    if (this.Controls.ContainsKey("soap" + key))
                    {
                        CtrlSoapPanel1 p = (CtrlSoapPanel1)this.Controls["soap" + key];
                        p.Write(dict[key], append);
                    }
                }
            }
        }
/*
        public void SoapWrite(string kind, string cont, bool append = true)
        {
            if (this.Controls.ContainsKey("soap" + kind))
            {
                CtrlSoapPanel1 p = (CtrlSoapPanel1)this.Controls["soap" + kind];
                p.Write(cont, append);
            }
        }

        public void SoapWrite(string kind, SoapData data, bool append = true)
        {
            if (this.Controls.ContainsKey("soap" + kind))
            {
                CtrlSoapPanel1 p = (CtrlSoapPanel1)this.Controls["soap" + kind];
                p.Write(data, append);
            }
        }
*/
        private void CtrlSoapWrite1_Load(object sender, EventArgs e)
        {
        }

        public void Init()
        {
            int h = 50;

            foreach (string key in Dict.SoapDict.Keys)
            {
                CtrlSoapPanel1 p = new CtrlSoapPanel1(key);
                p.Location = new Point(0, h);
                p.Name = "soap" + key;

                p.Width = this.Width - 25;

                // I と E は小さくする
                if (key.Equals("6"))
                {
                    p.Height = 50;
                }
                else if (key.Equals("7"))
                {
                    p.Height = 50;
                }

                this.Controls.Add(p);

                h += p.Height;
            }

            this.DeptBox1.Init();

            this.InsBox1.Items.Clear();

            foreach (SoapHeader.SoapIns obj in SoapHeader.SoapIns.Dict.Values)
            {
                this.InsBox1.Items.Add(obj);
            }
        }

        public void Clear()
        {
            this.SoapKey1 = new SoapKey();

            this.DatePicker1.Value = DateTime.Now;
            this.DeptBox1.Text = "";
            this.InsBox1.Text = "";

            foreach (string kind in Dict.SoapDict.Keys)
            {
                if (!this.Controls.ContainsKey("soap" + kind))
                {
                    continue;
                }

                CtrlSoapPanel1 p = (CtrlSoapPanel1)this.Controls["soap" + kind];
                p.Clear();
            }
        }

        private void CtrlSoapWrite1_DoubleClick(object sender, EventArgs e)
        {
            this.InOutChange();
        }

        private void InOutLabel1_DoubleClick(object sender, EventArgs e)
        {
            this.InOutChange();
        }

        private void InOutLabel2_DoubleClick(object sender, EventArgs e)
        {
            this.InOutChange();
        }

        /// <summary>
        /// 外来・入院モード切り替え
        /// </summary>
        void InOutChange()
        {
            if (this.InOut.Equals("1"))
            {
                this.InOut = "2";
            }
            else if (this.InOut.Equals("2"))
            {
                this.InOut = "1";
            }
        }

        public void Save()
        {
            List<string> msgs = new List<string>();

            if (this.PtId.Length == 0)
            {
                msgs.Add("患者コードがセットされていません");
            }

            if (this.DeptBox1.Text.Length == 0)
            {
                msgs.Add("診療科が選択されていません");
            }

            if (this.InsBox1.Text.Length == 0)
            {
                msgs.Add("保険が選択されていません");
            }

            SoapHeader header = new SoapHeader();

            // SOAP記載を確認し、リストに追加する。
            foreach (string kind in Dict.SoapDict.Keys)
            {
                if (this.Controls.ContainsKey("soap" + kind) &&
                    this.Controls["soap" + kind] is CtrlSoapPanel1)
                {
                    CtrlSoapPanel1 p = (CtrlSoapPanel1)this.Controls["soap" + kind];

                    SoapDetail data = new SoapDetail();
                    data.PtId = this.PtId;
                    int.TryParse(this.InOut, out data.InOut);
                    data.Kind = kind;
                    data.Cont = p.Cont;
                    data.ImgDict = p.ImgDict;

                    // 中身が書かれているか、画像があれば登録する。
                    if (data.Cont.Length > 0 || data.ImgDict.Count > 0)
                    {
                        header.DetailList.Add(data);
                    }
                }
            }

            if (header.DetailList.Count == 0)
            {
                msgs.Add("SOAP記載がありません");
            }

            if (msgs.Count > 0)
            {
                MessageBox.Show(AppString.ConcatList(msgs, Environment.NewLine));
                return;
            }
            else if (MessageBox.Show("確定しますか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            string reg_date = DateTime.Now.ToString("yyyyMMdd");
            string reg_time = DateTime.Now.ToString("HHmmss");

            header.PtId = this.PtId;

            if (this.SoapKey1.RegDate > 0)
            {
                header.InOut = this.SoapKey1.InOut;
                header.RegDate = this.SoapKey1.RegDate;
                header.RegTime = this.SoapKey1.RegTime;
                header.RegStaff = this.SoapKey1.RegStaff;
                header.SEQ = this.SoapKey1.SEQ;

                header.Delete();
            }

            header.InOut = int.Parse(this.InOut);
            header.InsKind = ((SoapHeader.SoapIns)this.InsBox1.SelectedItem).Code;
            header.Dept = this.DeptBox1.GetDept().Code.ToString();
            int.TryParse(this.DatePicker1.Value.ToString("yyyyMMdd"), out header.SoapDate);

            header.Insert();

            this.Clear();
        }
    }
}
