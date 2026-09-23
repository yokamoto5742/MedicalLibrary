using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MedicalLibrary.Utility
{
    public class AppFont
    {
        public Font Ft = new Font("MS UI Gothic", 10);

        public AppFont(Font font, FontStyle font_style)
        {
            this.Ft = new Font(font.Name, font.Size, font_style);
        }

        public AppFont(string font_name, float font_size, FontStyle font_style)
        {
            this.Ft = new Font(font_name, font_size, font_style);
        }

        public AppFont(string font_name, float font_size)
        {
            this.Ft = new Font(font_name, font_size);
        }

        public AppFont(float font_size, FontStyle font_style)
        {
            this.Ft = new Font("MS UI Gothic", font_size, font_style);
        }

        public AppFont(float font_size)
        {
            this.Ft = new Font("MS UI Gothic", font_size);
        }

        public override string ToString()
        {
            return Ft.Size.ToString();
        }

        public static AppFont F9 = new AppFont(9);

        public static AppFont F10 = new AppFont(10.5F);

        public static AppFont F11 = new AppFont(11);

        public static AppFont F12 = new AppFont(12);

        public static AppFont F14 = new AppFont("MS UI Gothic", 14);

        /// <summary>
        /// MSゴシック
        /// </summary>
        public static AppFont FN9 = new AppFont("ＭＳ ゴシック", 9);

        /// <summary>
        /// MSゴシック
        /// </summary>
        public static AppFont FN10 = new AppFont("ＭＳ ゴシック", 10.5F);


        /// <summary>
        /// 太字
        /// </summary>
        public static AppFont FB9 = new AppFont(9, FontStyle.Bold);

        /// <summary>
        /// 太字
        /// </summary>
        public static AppFont FB10 = new AppFont(10.5F, FontStyle.Bold);

        /// <summary>
        /// 太字
        /// </summary>
        public static AppFont FB11 = new AppFont(11, FontStyle.Bold);

        /// <summary>
        /// 太字
        /// </summary>
        public static AppFont FB12 = new AppFont(12, FontStyle.Bold);


        /// <summary>
        /// 削除データ
        /// </summary>
        public static AppFont FD9 = new AppFont("MS UI Gothic", 9, FontStyle.Strikeout);

        /// <summary>
        /// 削除データ
        /// </summary>
        public static AppFont FD10 = new AppFont("MS UI Gothic", 10.5F, FontStyle.Strikeout);


        public static AppFont DefaultFont = F10;
    }
}
