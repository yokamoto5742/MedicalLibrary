using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace MedicalLibrary.Utility
{
    public class LibUtility
    {
/*
        /// <summary>
        /// ログを書き込む TextBox
        /// </summary>
        public static TextBox LogBox;
*/
/*
        /// <summary>
        /// ログボックスのセット
        /// </summary>
        public static void SetLogBox(TextBox log_box)
        {
            LogBox = log_box;
        }
*/

        /// <summary>
        /// 例外を受け取って処理する
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="msg_box">MessageBox を表示するか</param>
        public static void Except(Exception ex, bool msg_box = true)
        {
            string s = ex.Message;

            // 型初期化子例外などでは実際の原因が InnerException に入っているため辿って表示する
            for (Exception inner = ex.InnerException; inner != null; inner = inner.InnerException)
            {
                s += Environment.NewLine;
                s += "[InnerException]" + Environment.NewLine;
                s += inner.Message;
            }

            // JITコンパイル時の例外などでは TargetSite / StackTrace が null のことがある
            if (ex.TargetSite != null && ex.TargetSite.Name.Length > 0)
            {
                s += Environment.NewLine;
                s += "[TargetSite]" + Environment.NewLine;
                s += ex.TargetSite.Name;
            }

            if (ex.StackTrace != null && ex.StackTrace.Length > 0)
            {
                s += Environment.NewLine;
                s += "[StackTrace]" + Environment.NewLine;
                s += ex.StackTrace;
            }

            if (msg_box)
            {
                MessageBox.Show(s, "例外");
            }
        }
    }
}
