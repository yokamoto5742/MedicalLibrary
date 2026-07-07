using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public class CtrlDateBox2 : TextBox
    {
        public CtrlDateBox2()
        {
            this.MaxLength = 8;
            this.ImeMode = ImeMode.Disable;
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if ((e.KeyChar >= '0' && e.KeyChar <= '9') ||
                e.KeyChar == '/' || e.KeyChar == '\b')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.F3)
            {
                FormDateSelector f = new FormDateSelector(this.Text);

                if (f.ShowDialog() == DialogResult.OK)
                {
                    if (AppString.IsDate(f.Date))
                    {
                        this.Text = f.Date;
                    }
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (this.Text.Equals("9"))
                {
                    this.Text = "99999999";
                }
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);

            if (this.Text.Length > 0 && !AppString.IsDate(this.Text) && !this.Text.Equals("99999999"))
            {
                string s = "日付を数字８桁で入力してください。（例）" + DateTime.Now.ToString("yyyyMMdd") + Environment.NewLine +
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
