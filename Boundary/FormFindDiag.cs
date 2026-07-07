using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class FormFindDiag : Form
    {
        Diag _Diag = new Diag();

        public Diag Diag
        {
            get
            {
                return this._Diag;
            }
        }

        public FormFindDiag(string s = "")
        {
            InitializeComponent();

            this.DiagFindBox1.Text = s;
        }

        private void FormFindDiag_Load(object sender, EventArgs e)
        {
            if (this.DiagFindBox1.Text.Length > 0)
            {
                this.MakeDiagName();
            }
        }

        void DataClear()
        {
            this._Diag = new Diag();

            this.DiagNameBox1.Clear();
            this.MainNameBox1.Clear();
            this.DiagCodeBox1.Clear();
            this.PrefixCodeBox1.Clear();
            this.SuffixCodeBox1.Clear();
            this.ICDBox1.Clear();
            this.ICDBox2.Clear();
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
            this.DiagFindPanel1.Controls.Clear();

            // 入力文字列をスペースで区切る
            string[] ss = DiagFindBox1.Text.Split(' ', '　');

            if (ss.Length > 0)
            {
                for (int i = 0; i < ss.Length; i++)
                {
                    if (ss[i].Length > 0)
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
            this.DataClear();

            // 新しくつける病名
            string diag_name = "";
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

                        name = obj.Name;
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
                    diag_name += obj.Name;
                }
            }

            DiagNameBox1.Text = diag_name;
            MainNameBox1.Text = name;
            DiagCodeBox1.Text = code.TrimStart(' ');
            PrefixCodeBox1.Text = prefix.TrimStart(' ');
            SuffixCodeBox1.Text = suffix.TrimStart(' ');
            ICDBox1.Text = icd1;
            ICDBox2.Text = icd2;

            this._Diag.DiagName = diag_name;
            this._Diag.MainName = name;
            this._Diag.DiagCode = code.TrimStart(' ');
            this._Diag.PrefixList = prefix.TrimStart(' ').Split(' ');
            this._Diag.SuffixList = suffix.TrimStart(' ').Split(' ');
            this._Diag.ICDCode1 = icd1;
            this._Diag.ICDCode2 = icd2;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
