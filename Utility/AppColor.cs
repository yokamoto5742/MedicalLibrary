using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MedicalLibrary.Utility
{
    public class AppColor
    {
        /// <summary>
        /// 外来
        /// </summary>
        public static Color Gairai = Color.LightGreen;

        /// <summary>
        /// 入院
        /// </summary>
        public static Color Nyuin = Color.LightPink;

        /// <summary>
        /// 院内
        /// </summary>
        public static Color InNai = Color.Blue;

        /// <summary>
        /// 院外
        /// </summary>
        public static Color InGai = Color.Red;

        /// <summary>
        /// 未施行
        /// </summary>
        public static Color MiSekou = Color.White;

        /// <summary>
        /// 施行済
        /// </summary>
        public static Color Sekou = Color.FromArgb(255, 255, 160);

        /// <summary>
        /// 会計済
        /// </summary>
        public static Color Kaikei = Color.PeachPuff;

        /// <summary>
        /// わかば
        /// </summary>
        public static Color Wakaba = Color.LightGreen;

        /// <summary>
        /// さくら
        /// </summary>
        public static Color Sakura = Color.Pink;

        /// <summary>
        /// あやめ
        /// </summary>
        public static Color Ayame = Color.Lavender;
    }
}
