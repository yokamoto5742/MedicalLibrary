using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows.Forms;

namespace MedicalLibrary.Boundary
{
    public class CtrlNumBox1 : TextBox
    {
        /// <summary>
        /// 最大値
        /// デフォルト 99999999
        /// </summary>
        public double MaxValue = 99999999;

        /// <summary>
        /// 最小値
        /// デフォルト -99999999
        /// </summary>
        public double MinValue = -99999999;

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

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if ((e.KeyChar >= '0' && e.KeyChar <= '9') ||
                e.KeyChar == '.' || e.KeyChar == '-' || e.KeyChar == '\b')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
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
                else
                {
                    double f = 0;

                    if (double.TryParse(this.Text, out f))
                    {
                        if (f > this.MaxValue)
                        {
                            s = "入力できる最大値は " + this.MaxValue.ToString() + " です";
                        }
                        else if (f < this.MinValue)
                        {
                            s = "入力できる最小値は " + this.MinValue.ToString() + " です";
                        }
                    }
                    else
                    {
                        s = "数値ではありません";
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
