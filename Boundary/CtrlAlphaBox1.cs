using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows.Forms;

namespace MedicalLibrary.Boundary
{
    public class CtrlAlphaBox1 : TextBox
    {
        /// <summary>
        /// 正規表現パターン
        /// </summary>
        public string Pattern = "";

        /// <summary>
        /// 入力例
        /// </summary>
        public string Sample = "";

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.ImeMode = ImeMode.Disable;
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);

            if (this.Text.Length > 0)
            {
                string s = "";

                if (this.Pattern.Length > 0 && !Regex.IsMatch(this.Text, this.Pattern))
                {
                    s = "入力された書式が正しくありません";

                    if (this.Sample.Length > 0)
                    {
                        s += Environment.NewLine + "（例）" + this.Sample;
                    }
                }

                if (s.Length > 0)
                {
                    s += Environment.NewLine +
                        "修正しますか？" + Environment.NewLine + Environment.NewLine +
                        "Yes … 修正する　No … 消して修正する　 Cancel … 修正しない";

                    DialogResult dr = MessageBox.Show(s, "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                    if (dr == DialogResult.Yes)
                    {
                        this.Focus();
                    }
                    else if (dr == DialogResult.No)
                    {
                        this.Clear();
                        this.Focus();
                    }
                }
            }
        }
    }
}
