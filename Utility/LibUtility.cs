using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using MedicalLibrary.Entity;

namespace MedicalLibrary.Utility
{
    public class LibUtility
    {
        /// <summary>
        /// ログレベル
        /// 設定XMLファイルの LogLevel の値に応じて出力の有無が決定される
        /// </summary>
        public enum LogLevel : int
        {
            /// <summary>
            /// LogLevel 値に関係なく出力する
            /// </summary>
            Level1 = 1,

            /// <summary>
            /// LogLevel が 1, 2 の場合のみ出力する
            /// </summary>
            Level2 = 2,

            /// <summary>
            /// LogLevel が 3 の場合のみ出力する
            /// </summary>
            Level3 = 3
        }
/*
        /// <summary>
        /// ログを書き込む TextBox
        /// </summary>
        public static TextBox LogBox;
*/
        /// <summary>
        /// カウンタ
        /// </summary>
        public static int Counter = 0;
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
        /// ログの書き込み
        /// </summary>
        /// <param name="msg">メッセージ</param>
        /// <param name="level">ログレベル 1: LogLevel が 1 の場合のみ出力する, 2: LogLevel が 2, 3 の場合のみ出力する, 3: LogLevel 値に関係なく出力する</param>
        public static void Log(string msg, string pt_id = "", bool db = true, bool file = false, LogLevel level = LogLevel.Level1)
        {
            if (LibSettings.Current.LogLevel == 1)
            {
                if (level == LogLevel.Level2 || level == LogLevel.Level3) return;
            }
            else if (LibSettings.Current.LogLevel == 2)
            {
                if (level == LogLevel.Level3) return;
            }

            if (db)
            {
                KarteLog obj = new KarteLog();
                obj.PtId = pt_id;
                obj.Cont = msg;
                obj.Save();
            }
/*
            if (file)
            {
                string s = "";

                s += DateTime.Now.ToString("yy/MM/dd HH:mm:ss") + " ";

                if (LoginUser.Id.Length > 0)
                {
                    s += "User:" + LoginUser.Id + " ";
                }

                if (pt_id.Length > 0)
                {
                    s += "Pat:" + pt_id + " ";
                }

                s += "Machine:" + Environment.MachineName + " ";
                s += msg;

                if (Directory.Exists(LibSettings.Current.LogFolderPath))
                {
                    using (StreamWriter sw1 = new StreamWriter(LibSettings.Current.LogFolderPath + "\\MedicalLibrary_" + DateTime.Now.ToString("yyMMdd") + ".log", true, Encoding.Default))
                    {
                        sw1.WriteLine(s);
                    }
                }

                if (Directory.Exists(LibSettings.Current.LogServerFolderPath))
                {
                    using (StreamWriter sw1 = new StreamWriter(LibSettings.Current.LogServerFolderPath + "\\MedicalLibrary_" + DateTime.Now.ToString("yyMMdd") + ".log", true, Encoding.Default))
                    {
                        sw1.WriteLine(s);
                    }
                }
            }
 */
        }

        /// <summary>
        /// 例外を受け取って処理する
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="msg_box">MessageBox を表示するか</param>
        public static void Except(Exception ex, bool msg_box = true)
        {
            string s = ex.Message;

            if (ex.TargetSite.Name.Length > 0)
            {
                s += Environment.NewLine;
                s += "[TargetSite]" + Environment.NewLine;
                s += ex.TargetSite.Name;
            }

            if (ex.StackTrace.Length > 0)
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

        public static void FormShow(Form f)
        {
            f.Show();
            f.Activate();
            f.BringToFront();

            if (f.WindowState == FormWindowState.Minimized)
            {
                f.WindowState = FormWindowState.Normal;
            }
        }
    }
}
