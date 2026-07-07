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

            PtId = "";

            return result;
        }

        
        /// <summary>
        /// 指定した Automation Id に一致する AutomationElement を返す
        /// </summary>
        /// <param name="root_element"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private static AutomationElement FindElementById(AutomationElement root_element, string id)
        {
            return root_element.FindFirst(
                TreeScope.Element | TreeScope.Descendants,
                new PropertyCondition(AutomationElement.AutomationIdProperty, id));
        }

        /// <summary>
        /// 指定した Name に一致する AutomationElement をすべて返す
        /// </summary>
        /// <param name="root_element"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static AutomationElementCollection FindElementsByName(AutomationElement root_element, string name)
        {
            return root_element.FindAll(
                TreeScope.Element | TreeScope.Descendants,
                new PropertyCondition(AutomationElement.NameProperty, name));
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
    }
}
