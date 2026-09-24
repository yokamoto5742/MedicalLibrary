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
    public class InnoProgram
    {
        /// <summary>
        /// プログラムが存在するか
        /// </summary>
        public static bool Exists
        {
            get
            {
                return File.Exists(@"c:\InnoKarte\InnoKarte.exe");
            }
        }

        public static bool KarteShow(string pt_id)
        {
            bool result = false;

            if (pt_id.Length == 0)
            {
                return result;
            }

            // カルテ記載ウィンドウを探す
            IntPtr hwnd = FindWindowByTitle("SOAP入力");

            // カルテ記載ウィンドウが見つかった場合
            if (hwnd != IntPtr.Zero)
            {
                AutomationElement aeForm = AutomationElement.FromHandle(hwnd);

                AutomationElement e2 = aeForm.FindFirst(
                    TreeScope.Element | TreeScope.Descendants,
                    new PropertyCondition(AutomationElement.AutomationIdProperty, "BtnReturn"));

                if (e2 != null)
                {
                    InvokePattern p2 = (InvokePattern)e2.GetCurrentPattern(InvokePattern.Pattern);

                    p2.Invoke();

                    // ウェイト
                    Thread.Sleep(150);
                }
            }

            // 外来患者一覧または入院一覧ウィンドウを探す
            hwnd = FindWindowByTitle("外来患者一覧", "入院一覧");

            // 外来患者一覧または入院一覧ウィンドウが見つかった場合
            if (hwnd != IntPtr.Zero)
            {
                AutomationElement aeForm = AutomationElement.FromHandle(hwnd);

                AutomationElement e1 = aeForm.FindFirst(
                    TreeScope.Element | TreeScope.Descendants,
                    new PropertyCondition(AutomationElement.AutomationIdProperty, "TxtPatientID"));

                AutomationElement e2 = aeForm.FindFirst(
                    TreeScope.Element | TreeScope.Descendants,
                    new PropertyCondition(AutomationElement.AutomationIdProperty, "BtnKarte"));

                // 患者IDの入力欄かカルテボタンが見つからなければ何もしない
                if (e1 == null || e2 == null)
                {
                    return result;
                }

                ValuePattern p1 = (ValuePattern)e1.GetCurrentPattern(ValuePattern.Pattern);
                InvokePattern p2 = (InvokePattern)e2.GetCurrentPattern(InvokePattern.Pattern);

                p1.SetValue(pt_id);

                // ウェイト
                Thread.Sleep(150);

                // キーを送る(戻りを待つ)
                SendKeys.SendWait("{ENTER}");

                // ウェイト
                Thread.Sleep(150);

                p2.Invoke();

                result = true;
            }

            return result;
        }

        /// <summary>
        /// 最前面から順に、タイトルに titles のいずれかを含む表示中のウィンドウを探す。
        /// </summary>
        /// <returns>見つからなければ IntPtr.Zero</returns>
        static IntPtr FindWindowByTitle(params string[] titles)
        {
            const int GW_HWNDNEXT = 2;
            StringBuilder sb = new StringBuilder(100);
            IntPtr hwnd = WinAPI.GetForegroundWindow();

            while (hwnd != IntPtr.Zero)
            {
                if (WinAPI.IsWindowVisible(hwnd))
                {
                    // タイトルバー文字列を取得
                    WinAPI.GetWindowText(hwnd, sb, sb.Capacity);

                    foreach (string title in titles)
                    {
                        if (sb.ToString().IndexOf(title) != -1)
                        {
                            return hwnd;
                        }
                    }
                }

                hwnd = WinAPI.GetWindow(hwnd, GW_HWNDNEXT);
            }

            return IntPtr.Zero;
        }
    }
}
