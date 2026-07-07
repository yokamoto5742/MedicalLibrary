using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class FormDiag : StdForm1
    {
        bool _ReadOnly = true;

        public bool ReadOnly
        {
            get
            {
                return this._ReadOnly;
            }
            set
            {
                this._ReadOnly = value;

                if (value)
                {
                    this.SEQLabel.Visible = false;
                    this.ModeLabel.Visible = false;
                    this.SaveButton1.Visible = false;
                    this.ClearButton1.Visible = false;
                    this.DeleteButton1.Visible = false;
                }
                else
                {
                    this.SEQLabel.Visible = true;
                    this.ModeLabel.Visible = true;
                    this.SaveButton1.Visible = true;
                    this.ClearButton1.Visible = true;
                    this.DeleteButton1.Visible = true;
                }
            }
        }

        public enum Mode : int
        {
            New = 1,
            Modify = 2
        }

        /// <summary>
        /// 現在表示されている Diag
        /// </summary>
        Diag Diag1 = new Diag();

        string InOutCode = "";

        string InOutString
        {
            get
            {
                string s = "";

                if (Diag.DiagInOut.Dict.ContainsKey(this.InOutCode))
                {
                    s = Diag.DiagInOut.Dict[this.InOutCode].Name;
                }

                return s;
            }
        }

        string DeptCode = "";
        string DoctorCode = "";

        Mode mode1 = Mode.New;

        public Mode Mode1
        {
            get
            {
                return this.mode1;
            }
            set
            {
                this.mode1 = value;

                if (this.mode1 == Mode.Modify)
                {
                    this.ModeLabel.BackColor = Color.LightPink;
                    this.ModeLabel.Text = "修正";
                }
                else
                {
                    this.ModeLabel.BackColor = Color.White;
                    this.ModeLabel.Text = "新規";
                }
            }
        }


        public FormDiag(string in_out_code = "", string dept_code = "", string doctor_code = "")
        {
            InitializeComponent();

            this.InOutCode = in_out_code;

            foreach (Diag.DiagInOut obj in Diag.DiagInOut.Dict.Values)
            {
                this.InOutBox1.Items.Add(obj);
            }

            this.InOutBox1.Text = this.InOutString;

            foreach (Diag.DiagIns obj in Diag.DiagIns.Dict.Values)
            {
                this.InsBox1.Items.Add(obj);
            }

            foreach (Diag.DiagOutcome obj in Diag.DiagOutcome.Dict.Values)
            {
                this.OutcomeBox1.Items.Add(obj);
            }

            this.DeptCode = dept_code;

            this.DeptBox1.Init(true, false, true);
            this.DeptBox1.SetDept(this.DeptCode);

            this.DoctorCode = doctor_code;

            this.DoctorBox1.Init(true, true);
            this.DoctorBox1.SetDoctor(this.DoctorCode);

            this.DiagCheckBox1.Checked = true;
        }

        private void FormDiag_Load(object sender, EventArgs e)
        {
            this.ReadOnly = true;
            this.Mode1 = Mode.New;
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            this.ListShow();
        }

        void DataClear()
        {
            this.Mode1 = Mode.New;

            this.Diag1 = new Diag();

            this.SEQLabel.Text = "";

            this.MainFlgBox1.Checked = false;
            this.NoticeFlgBox1.Checked = false;
            this.InsFlgBox1.Checked = false;
            this.DoubtFlgBox1.Checked = false;

            this.DiagNameBox1.Clear();
            this.DiagCodeBox1.Clear();
            this.PrefixCodeBox1.Clear();
            this.SuffixCodeBox1.Clear();

            this.InOutBox1.Text = this.InOutString;

            this.DatePicker1.Value = DateTime.Now;
            this.DateBox2.Clear();

            this.DeptBox1.SetDept(this.DeptCode);
            this.DoctorBox1.SetDoctor(this.DoctorCode);

            this.InsBox1.Text = "";
            this.OutcomeBox1.Text = "";

            this.ICDBox1.Clear();
            this.ICDBox2.Clear();

            this.DiagFindPanel1.Controls.Clear();
        }

        void ListShow()
        {
            this.ListView1.ListShow(this.Pat.Id, "開始日 desc, 連番 desc", "");

            this.ListFormat();
            this.DataClear();
        }

        void ListFormat()
        {
            List<string> filters = new List<string>();

            if (this.DiagCheckBox1.Checked)
            {
                filters.Add("転帰区分 = ''");
            }

            if (this.DiagCheckBox2.Checked)
            {
                filters.Add("転帰区分 <> ''");
            }

            if (filters.Count == 0)
            {
                filters.Add("転帰区分 = 'A'");
            }

            this.ListView1.ListFormat("開始日 desc, 連番 desc", AppString.ConcatList(filters, " or "));

            this.ListView1.Columns["病名"].Width = 180;
        }

        private void DiagFindBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.FindDiagName();
            }
        }

        /// <summary>
        /// 病名を検索する
        /// </summary>
        void FindDiagName()
        {
            this.DataClear();

            // 入力文字列をスペースで区切る
            string[] ss = DiagFindBox1.Text.Split(' ','　');

            if (ss.Length > 0)
            {
                for (int i = 0; i < ss.Length; i++)
                {
                    if (ss[i].Equals("t") || ss[i].Equals("T") ||
                        ss[i].Equals("ｔ") || ss[i].Equals("Ｔ") ||
                        ss[i].Equals("た"))
                    {
                        // 短期病名
                        this.InsFlgBox1.Checked = true;
                    }
                    else if (ss[i].Equals("u") || ss[i].Equals("U") ||
                        ss[i].Equals("ｕ") || ss[i].Equals("Ｕ") ||
                        ss[i].Equals("う"))
                    {
                        // 疑い病名
                        this.DoubtFlgBox1.Checked = true;
                    }
                    else if (ss[i].Length > 0)
                    {
                        // 通常の病名または修飾語
                        List<DiagMaster> list = DiagMaster.FindList(ss[i]);

                        if (list.Count > 0)
                        {
                            ListBox b = new ListBox();

                            b.Items.Add("");

                            foreach (DiagMaster obj in list)
                            {
                                b.Items.Add(obj);

                                // ぴったり一致していれば選択する
                                if (ss[i].Equals(obj.Name))
                                {
                                    b.SelectedItem = obj;
                                }
                            }

                            b.Width = 150;
                            b.Height = DiagFindPanel1.Height;
                            b.SelectedValueChanged += new EventHandler(b_SelectedValueChanged);

                            DiagFindPanel1.Controls.Add(b);
                        }
                    }
                }

                this.MakeDiagName();
            }
        }

        void b_SelectedValueChanged(object sender, EventArgs e)
        {
            this.MakeDiagName();
        }

        /// <summary>
        /// 検索結果をもとに病名を作成する
        /// </summary>
        void MakeDiagName()
        {
            // いったん病名をクリアする
            DiagNameBox1.Clear();
            DiagCodeBox1.Clear();
            PrefixCodeBox1.Clear();
            SuffixCodeBox1.Clear();
            ICDBox1.Clear();
            ICDBox2.Clear();

            // 新しくつける病名
            string name = "";
            string code = "";
            string prefix = "";
            string suffix = "";
            string icd1 = "";
            string icd2 = "";

            // 病名は１つしか選べないので、すでに選ばれたかどうかのフラグを持つ。
            bool diagflg = false;

            foreach (Control c in DiagFindPanel1.Controls)
            {
                ListBox b = (ListBox)c;

                if (b.SelectedItem != null && b.SelectedItem.ToString().Length > 0)
                {
                    DiagMaster obj = (DiagMaster)b.SelectedItem;

                    if (obj.Kind == DiagMaster.DiagMasterKind.Diag)
                    {
                        // 選択されたものが病名である場合

                        if (diagflg)
                        {
                            // すでに病名が選ばれていれば、エラーメッセージを出して、選択を解除する
                            MessageBox.Show("病名は１つしか選べません。");
                            b.SelectedItem = null;
                            return;
                        }
                        else
                        {
                            // まだ病名が選ばれていなければ、これをもって病名選択フラグを立てる
                            diagflg = true;
                        }

                        code += " " + obj.Code;

                        icd1 = obj.ICD10_1;
                        icd2 = obj.ICD10_2;
                    }
                    else if (obj.Kind == DiagMaster.DiagMasterKind.Prefix)
                    {
                        // 選択されたものが接頭語である場合
                        prefix += " " + obj.Code;
                    }
                    else if (obj.Kind == DiagMaster.DiagMasterKind.Suffix)
                    {
                        // 選択されたものが接尾語である場合
                        suffix += " " + obj.Code;
                    }

                    // 病名を追記する
                    name += obj.Name;
                }
            }

            // 「疑い」にチェックが入っていれば、末尾に付加する
            if (DoubtFlgBox1.Checked)
            {
                name += "の疑い";
                suffix += " 8002";
            }

            DiagNameBox1.Text = name;
            DiagCodeBox1.Text = code.TrimStart(' ');
            PrefixCodeBox1.Text = prefix.TrimStart(' ');
            SuffixCodeBox1.Text = suffix.TrimStart(' ');
            ICDBox1.Text = icd1;
            ICDBox2.Text = icd2;
        }

        private void DoubtFlgBox1_CheckedChanged(object sender, EventArgs e)
        {
            string s = DiagNameBox1.Text;

            // すでに病名が入力されている場合のみ処理する
            if (s.Length > 0)
            {
                if (DoubtFlgBox1.Checked)
                {
                    // 「疑い」にチェックが入っていて、「の疑い」の文字が末尾に入っていなければ付加する
                    if (!s.EndsWith("の疑い"))
                    {
                        s += "の疑い";
                    }
                }
                else
                {
                    // 「疑い」にチェックが入っておらず、「の疑い」の文字が末尾に入っていれば削除する
                    if (s.EndsWith("の疑い"))
                    {
                        s = s.Substring(0, s.Length - 3);
                    }
                }
            }

            DiagNameBox1.Text = s;
        }

        private void DiagCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.ListFormat();
        }

        private void DiagCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.ListFormat();
        }

        private void ListView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.DataClear();

            this.Mode1 = Mode.Modify;

            this.Diag1 = this.ListView1.GetDiag(e.RowIndex);

            this.SEQLabel.Text = this.Diag1.SEQ.ToString();

            if (this.Diag1.MainFlg)
            {
                this.MainFlgBox1.Checked = true;
            }

            if (this.Diag1.NoticeFlg)
            {
                this.NoticeFlgBox1.Checked = true;
            }

            if (this.Diag1.InsFlg)
            {
                this.InsFlgBox1.Checked = true;
            }

            if (this.Diag1.DiagName.EndsWith("の疑い"))
            {
                this.DoubtFlgBox1.Checked = true;
            }

            this.DiagNameBox1.Text = this.Diag1.DiagName;
            this.DiagCodeBox1.Text = this.Diag1.DiagCode;

            string s = "";

            for (int i = 1; i <= 15; i++)
            {
                if (this.Diag1.PrefixList[i].Length > 0)
                {
                    if (s.Length > 0)
                    {
                        s += " ";
                    }

                    s += this.Diag1.PrefixList[i];
                }
            }

            this.PrefixCodeBox1.Text = s;

            s = "";

            for (int i = 1; i <= 5; i++)
            {
                if (this.Diag1.SuffixList[i].Length > 0)
                {
                    if (s.Length > 0)
                    {
                        s += " ";
                    }

                    s += this.Diag1.SuffixList[i];
                }
            }

            this.SuffixCodeBox1.Text = s;

            this.InOutBox1.Text = this.Diag1.InOutString;

            this.DatePicker1.Value = this.Diag1.StartDateValue;
            this.DateBox2.DateInt = AppString.IsDate(this.Diag1.OutcomeDate) ? int.Parse(this.Diag1.OutcomeDate) : 0;

            this.DeptBox1.SetDept(this.Diag1.Dept);
            this.DoctorBox1.SetDoctor(this.Diag1.Doctor);

            this.InsBox1.Text = this.Diag1.InsKindName;
            this.OutcomeBox1.Text = this.Diag1.OutcomeString;

            this.ICDBox1.Text = this.Diag1.ICDCode1;
            this.ICDBox2.Text = this.Diag1.ICDCode2;
        }

        private void DPCButton1_Click(object sender, EventArgs e)
        {
            FormDiagDPC f = new FormDiagDPC();
            f.PatSet(this.Pat);
            f.ShowDialog(this);
        }

        private void SaveButton1_Click(object sender, EventArgs e)
        {
        }

        private void ClearButton1_Click(object sender, EventArgs e)
        {
            this.DataClear();
        }

        private void DeleteButton1_Click(object sender, EventArgs e)
        {
        }
    }
}
