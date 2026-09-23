using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Utility
{
    public class AppStat
    {
        /// <summary>
        /// デバッグモード
        /// </summary>
        public static bool Debug = false;

        /// <summary>
        /// 現在のフォント
        /// </summary>
        public static AppFont CurrentFont = AppFont.F9;

        /// <summary>
        /// 現在の患者
        /// </summary>
        public static PatBase CurrentPat = new PatBase();

        /// <summary>
        /// 予約カレンダーに表示する日数
        /// </summary>
        public static int RsvDays = 28;
    }
}
