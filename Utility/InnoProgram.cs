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

            const int GW_HWNDNEXT = 2;
            StringBuilder sb = new StringBuilder(100);
            IntPtr hwnd = WinAPI.GetForegroundWindow();

            // カルテ記載ウィンドウを探す
            while (hwnd != IntPtr.Zero)
            {
                if (WinAPI.IsWindowVisible(hwnd))
                {
                    // タイトルバー文字列を取得
                    WinAPI.GetWindowText(hwnd, sb, sb.Capacity);

                    if ((sb.ToString().IndexOf("SOAP入力") != -1))
                    {
                        break;
                    }
                }

                hwnd = WinAPI.GetWindow(hwnd, GW_HWNDNEXT);
            }

            // カルテ記載ウィンドウが見つかった場合
            if (hwnd != IntPtr.Zero)
            {
                AutomationElement aeForm = AutomationElement.FromHandle(hwnd);

                AutomationElement e2 = aeForm.FindFirst(
                    TreeScope.Element | TreeScope.Descendants,
                    new PropertyCondition(AutomationElement.AutomationIdProperty, "BtnReturn"));

                InvokePattern p2 = (InvokePattern)e2.GetCurrentPattern(InvokePattern.Pattern);

                p2.Invoke();

                // ウェイト
                Thread.Sleep(150);
            }

            hwnd = WinAPI.GetForegroundWindow();

            // 外来患者一覧または入院一覧ウィンドウを探す
            while (hwnd != IntPtr.Zero)
            {
                if (WinAPI.IsWindowVisible(hwnd))
                {
                    // タイトルバー文字列を取得
                    WinAPI.GetWindowText(hwnd, sb, sb.Capacity);

                    if ((sb.ToString().IndexOf("外来患者一覧") != -1) || (sb.ToString().IndexOf("入院一覧") != -1))
                    {
                        break;
                    }
                }

                hwnd = WinAPI.GetWindow(hwnd, GW_HWNDNEXT);
            }

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

        
    }
}
