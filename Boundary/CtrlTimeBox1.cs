using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MedicalLibrary.Boundary
{
    public class CtrlTimeBox1 : TextBox
    {
        public CtrlTimeBox1()
        {
            this.ImeMode = ImeMode.Disable;
            this.TextAlign = HorizontalAlignment.Center;

            this.KeyDown += new KeyEventHandler(CtrlTimeBox1_KeyDown);
            this.Leave += new EventHandler(CtrlTimeBox1_Leave);
        }

        void CtrlTimeBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!this.Check())
                {
                    if (MessageBox.Show("時刻形式が正しくありません。HHmm 形式（例 8:15 または 08:15 または 815 または 0815）で入力してください。\r\n入力内容をクリアしますか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        base.Clear();
                    }

                    base.Select();
                }
            }
        }

        void CtrlTimeBox1_Leave(object sender, EventArgs e)
        {
            if (!this.Check())
            {
                if (MessageBox.Show("時刻形式が正しくありません。HHmm 形式（例 8:15 または 08:15 または 815 または 0815）で入力してください。\r\n入力内容をクリアしますか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    base.Clear();
                }

                base.Select();
            }
        }

        /// <summary>
        /// 入力された値が正しい時刻データかチェックする
        /// </summary>
        /// <returns></returns>
        bool Check()
        {
            bool result = true;
            string tmp_time = base.Text;

            if (tmp_time.Length > 0)
            {
                if (tmp_time.Contains(":"))
                {
                    tmp_time = tmp_time.Replace(":", "");
                }

                tmp_time = tmp_time.PadLeft(4, '0').Insert(2, ":");

                DateTime dt = new DateTime();
                result = DateTime.TryParse(tmp_time, out dt);

                if (result)
                {
                    base.Text = dt.ToString("HH:mm");
                }
            }

            return result;
        }

        /// <summary>
        /// 入力値を数値型（HHmm）で返す。
        /// 空または正しい日付値でなければ 0 を返す。
        /// </summary>
        /// <returns>yyyyMMdd</returns>
        public int ToInt()
        {
            int result = 0;

            if (Check())
            {
                int.TryParse(base.Text.Replace(":", ""), out result);
            }

            return result;
        }

        /// <summary>
        /// 引数が日付を表す数値（HHmm）ならば、それを入力値とする。
        /// そうでなければ空にする。
        /// </summary>
        /// <param name="date"></param>
        public void FromInt(int time)
        {
            base.Text = "";

            if (time.ToString().Length <= 4)
            {
                DateTime dt = new DateTime();

                if (DateTime.TryParse(time.ToString().PadLeft(4, '0').Insert(2, ":"), out dt))
                {
                    base.Text = dt.ToString("HH:mm");
                }
            }
        }
    }
}
