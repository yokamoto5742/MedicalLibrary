using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Automation;

namespace MedicalLibrary.Utility
{
    public class MacsProgram
    {
        /// <summary>
        /// プログラムが存在するか
        /// </summary>
        public static bool Exists
        {
            get
            {
                return File.Exists(@"C:\macs\HyMarks\Karte.exe");
            }
        }

        static string PtId = "";

        public static bool KarteShow(string pt_id)
        {
            bool result = false;

            if (pt_id.Length == 0)
            {
                return result;
            }

            const int GW_HWNDNEXT = 2;
            StringBuilder sb = new StringBuilder(100);
            IntPtr hwnd = WinAPI.GetForegroundWindow();

            PtId = pt_id;

            while(hwnd != IntPtr.Zero)
            {
                if (WinAPI.IsWindowVisible(hwnd))
                {
                    // タイトルバー文字列を取得
                    WinAPI.GetWindowText(hwnd, sb, sb.Capacity);

                    if ((sb.ToString().IndexOf("外来受付一覧") != -1) || (sb.ToString().IndexOf("入院情報（患者一覧）") != -1))
                    {
                        break;
                    }
                }

                hwnd = WinAPI.GetWindow(hwnd, GW_HWNDNEXT);
            }

            result = !WinAPI.EnumChildWindows(hwnd, EnumChildFunc1, hwnd);

            PtId = "";

            return result;
        }

        enum ShowWindowEnum : int
        {
            SW_HIDE = 0,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZE = 2,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_MAX = 11
        }

        private static bool EnumChildFunc1(IntPtr hWnd, IntPtr lParam)
        {
            if (PtId.Length == 0)
            {
                return false;
            }

            if (hWnd != IntPtr.Zero)
            {
                StringBuilder sbClassName = new StringBuilder(256);

                //APIでクラス名を取得
                WinAPI.GetClassName(hWnd, sbClassName, sbClassName.Capacity);

                if (sbClassName.Length > 0 && WinAPI.IsWindowVisible(hWnd))
                {
                    StringBuilder sbWindowText = new StringBuilder(256);

                    // APIでウィンドウ文字列を取得
                    WinAPI.GetWindowText(hWnd, sbWindowText, sbWindowText.Capacity);

                    // 検証用コード
//                    string msg = "sbClassName = " + sbClassName.ToString() + ", sbWindowText = " + sbWindowText.ToString();
//                    LibUtility.Log(msg);

                    if (sbClassName.ToString() == "ThunderRT6TextBox"
                            && sbWindowText.ToString() == "123456789")
                    {
                        // 電子カルテの数値入力欄は SetWindowTextでは書き換えられない
                        // 最小化されていたら戻す
                        if (WinAPI.IsIconic(lParam))
                        {
                            WinAPI.ShowWindowAsync(lParam, WinAPI.ShowWindowEnum.SW_RESTORE);
                            //ウェイト
                            Thread.Sleep(100);
                        }

                        // アクティブにする
                        WinAPI.SetForegroundWindow(hWnd);

                        // ウェイト
                        Thread.Sleep(150);

                        // フォーカスを設定
                        WinAPI.SetFocus(lParam);

                        // ウェイト
                        Thread.Sleep(150);

                        // キーを送る(戻りを待つ)
                        SendKeys.SendWait("^A{DEL}" + PtId + "{ENTER}");

                        // ウェイト
                        Thread.Sleep(150);

                        // キーを送る(戻りを待つ)
                        SendKeys.SendWait("{ENTER}");

                        return false;
                    }
                }

                //ウィンドウの列挙を続行する
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Process PdfKarteShow()
        {
            string execFile = @"c:\pdfkarte\Exe\eKrtView\eKrtView.exe";

            if (System.IO.File.Exists(execFile))
            {
                return System.Diagnostics.Process.Start(execFile);
            }

            return null;
        }

        public static bool OrderShow()
        {
            string execFile = Env.LEGACY_HOME + @"\order32\appl\toyama_hp\od_syoho_new_WIDE.exe";

            if (System.IO.File.Exists(execFile))
            {
                System.Diagnostics.Process.Start(execFile);
                return true;
            }

            return false;
        }

        public static bool PathShow()
        {
            string execFile = Env.LEGACY_HOME + @"\order32\appl\PATH\PathEDIT.exe";

            if (System.IO.File.Exists(execFile))
            {
                System.Diagnostics.Process.Start(execFile);
                return true;
            }

            return false;
        }
    }
}
