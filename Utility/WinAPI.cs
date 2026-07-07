using System;
using System.Collections.Generic;
using System.Text;
//DLLImport を利用するために必須
using System.Runtime.InteropServices;

namespace MedicalLibrary.Utility
{
    /// <summary>
    /// WindwsAPI を利用するための静的クラス
    /// </summary>
    public static class WinAPI
    {
        public delegate bool EnumWindowProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool EnumChildWindows(IntPtr hWndParent, EnumWindowProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int EnumWindows(EnumWindowProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int GetClassName(IntPtr hWnd, [Out] StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern int GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern int GetParent(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpStr, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, int bRepaint);

        [DllImport("user32.dll")]
        public static extern bool IsChild(IntPtr hWndParent, IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool SetFocus(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public enum ShowWindowEnum : int
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

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int SetWindowText(IntPtr hWnd, string strText);

        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, ShowWindowEnum nCmdShow);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern IntPtr SendMessage(IntPtr hWnd, IntPtr Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern IntPtr SendMessage(IntPtr hWnd, IntPtr Msg, IntPtr wParam, ref COPYDATASTRUCT lParam);

        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public IntPtr cbData;
            public string lpData;
        }

        public const int WM_COPYDATA = 0x4A;
        public const int WM_USER = 0x400;

        public const int VK_CAPITAL = 0x14;
        public const int VK_KANA = 0x15;
        public const int VK_INSERT = 0x2D;
        public const int VK_NUMLOCK = 0x90;
        public const int VK_SCROLL = 0x91;

        [DllImport("user32.dll")]
        public static extern int GetKeyState(int nVirtKey);

        [DllImport("kernel32.dll")]
        public static extern bool Beep(uint dwFreq, uint dwDuration);

        [DllImport("comctl32.dll")]
        public static extern bool InitCommonControls();

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImageList_BeginDrag(IntPtr himlTrack, int
            iTrack, int dxHotspot, int dyHotspot);

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImageList_DragMove(int x, int y);

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern void ImageList_EndDrag();

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImageList_DragEnter(IntPtr hwndLock, int x, int y);

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImageList_DragLeave(IntPtr hwndLock);

        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath,
                uint dwFileAttributes, ref SHFILEINFO psfi,
                uint cbSizeFileInfo, uint uFlags);

        public const uint SHGFI_ICON = 0x100;    //アイコンリソースの取得
        public const uint SHGFI_LARGEICON = 0x0; //大きいアイコン
        public const uint SHGFI_SMALLICON = 0x1; //小さいアイコン

        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }
    }
}