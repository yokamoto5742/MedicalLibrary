using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace MedicalLibrary.Utility
{
    public class AppString
    {
        static char[] zen = { '０', '１', '２', '３', '４', '５', '６', '７', '８', '９', 'Ａ', 'Ｂ', 'Ｃ', 'Ｄ', 'Ｅ', 'Ｆ', 'Ｇ', 'Ｈ', 'Ｉ', 'Ｊ', 'Ｋ', 'Ｌ', 'Ｍ', 'Ｎ', 'Ｏ', 'Ｐ', 'Ｑ', 'Ｒ', 'Ｓ', 'Ｔ', 'Ｕ', 'Ｖ', 'Ｗ', 'Ｘ', 'Ｙ', 'Ｚ', 'ａ', 'ｂ', 'ｃ', 'ｄ', 'ｅ', 'ｆ', 'ｇ', 'ｈ', 'ｉ', 'ｊ', 'ｋ', 'ｌ', 'ｍ', 'ｎ', 'ｏ', 'ｐ', 'ｑ', 'ｒ', 'ｓ', 'ｔ', 'ｕ', 'ｖ', 'ｗ', 'ｘ', 'ｙ', 'ｚ', '！', '”', '＃', '＄', '％', '＆', '’', '（', '）', '＝', '－', '＋', '＊', '＾', '～', '￥', '｜', '「', '」', '｛', '｝', '＠', '‘', '；', '：', '、', '．', '＜', '＞', '？', '／', '＿' };
        static char[] han = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '!', '"', '#', '$', '%', '&', '\'', '(', ')', '=', '-', '+', '*', '^', '~', '\\', '|', '[', ']', '{', '}', '@', '`', ';', ':', ',', '.', '<', '>', '?', '/', '_' };

        /// <summary>
        /// 全角文字列を半角に変換する
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ZenToHan(string s)
        {
            return Strings.StrConv(s, VbStrConv.Narrow);
/*
            string ret = "";

            for (int i = 0; i < s.Length; i++)
            {
                for (int j = 0; j < zen.Length; j++)
                {
                    if (s[i].Equals(zen[j]))
                    {
                        ret += han[j].ToString();
                        break;
                    }

                    // リストに無かった場合はそのまま追加
                    if (j == zen.Length - 1)
                    {
                        ret += s[i];
                    }
                }
            }

            return ret;
 */
        }

        /// <summary>
        /// 半角文字列を全角に変換する
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string HanToZen(string s)
        {
            return Strings.StrConv(s, VbStrConv.Wide);

/*
            string ret = "";

            for (int i = 0; i < s.Length; i++)
            {
                for (int j = 0; j < han.Length; j++)
                {
                    if (s[i].Equals(han[j]))
                    {
                        ret += zen[j].ToString();
                        break;
                    }

                    // リストに無かった場合はそのまま追加
                    if (j == han.Length - 1)
                    {
                        ret += s[i];
                    }
                }
            }

            return ret;
 */
        }

        public static string HiraToZenkana(string s)
        {
            return Strings.StrConv(s, VbStrConv.Katakana);
        }

        public static string HiraToHankana(string s)
        {
            return Strings.StrConv(Strings.StrConv(s, VbStrConv.Katakana), VbStrConv.Narrow);
        }

        /// <summary>
        /// 文字列のバイト長を調べる
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int LenB(string s)
        {
            return Encoding.GetEncoding("Shift_JIS").GetByteCount(s);
        }

        /// <summary>
        /// 文字列を指定したバイト長になるまで指定文字で穴埋めする
        /// </summary>
        /// <param name="s"></param>
        /// <param name="n"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string PadRightB(string s, int n, char c)
        {
            string ss = s;

            if (LenB(s) < n)
            {
                for (int i = 0; i < n - LenB(s); i++)
                {
                    ss += c;
                }
            }

            return ss;
        }

        public static string Wrap(string s, float font_size, int width)
        {
            string ss = "";

            int n = (int)(width * 1.35) / (int)font_size;
            int tmp_len = 0;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].Equals(Environment.NewLine[0]))
                {
                    ss += s[i];
                    tmp_len = 0;
                    i += Environment.NewLine.Length - 1;
                    continue;
                }

                if (tmp_len >= n)
                {
                    ss += Environment.NewLine;
                    tmp_len = 0;
                }

                ss += s[i];
                tmp_len += AppString.LenB(s[i].ToString());
            }

            return ss;
        }

        /// <summary>
        /// 文字列リストを結合する。
        /// </summary>
        /// <param name="list"></param>
        /// <param name="delimiter">デリミタ。必要に応じて空白を含む。and でつなぐ場合は " and " とする</param>
        /// <param name="quote">文字列の前後の Quote</param>
        /// <returns></returns>
        public static string ConcatList(List<string> list, string delimiter, string quote = "")
        {
            return ConcatList(list.ToArray(), delimiter, quote);
        }

        /// <summary>
        /// 文字列リストを結合する。
        /// </summary>
        /// <param name="list"></param>
        /// <param name="delimiter">デリミタ。必要に応じて空白を含む。and でつなぐ場合は " and " とする</param>
        /// <param name="quote">文字列の前後の Quote</param>
        /// <returns></returns>
        public static string ConcatList(string[] list, string delimiter, string quote = "")
        {
            string s = "";

            foreach (string ss in list)
            {
                if (ss.Length == 0)
                {
                    continue;
                }

                if (s.Length > 0)
                {
                    s += delimiter;
                }

                s += quote + ss + quote;
            }

            return s;
        }

        /// <summary>
        /// 文字列リストを結合した文字列リストを生成する。
        /// </summary>
        /// <param name="list"></param>
        /// <param name="delimiter">デリミタ。必要に応じて空白を含む。and でつなぐ場合は " and " とする</param>
        /// <param name="quote">文字列の前後の Quote</param>
        /// <param name="max">リスト１つに含まれる最大文字列数</param>
        /// <returns></returns>
        public static List<string> ConcatLists(List<string> list, string delimiter, string quote = "", int max = 1000)
        {
            return ConcatLists(list.ToArray(), delimiter, quote);
        }

        /// <summary>
        /// 文字列リストを結合した文字列リストを生成する。
        /// </summary>
        /// <param name="list"></param>
        /// <param name="delimiter">デリミタ。必要に応じて空白を含む。and でつなぐ場合は " and " とする</param>
        /// <param name="quote">文字列の前後の Quote</param>
        /// <param name="max">リスト１つに含まれる最大文字列数</param>
        /// <returns></returns>
        public static List<string> ConcatLists(string[] list, string delimiter, string quote = "", int max = 1000)
        {
            List<string> lists = new List<string>();
            string s = "";
            int i = 0;

            while (i < list.Length)
            {
                if (s.Length > 0)
                {
                    s += delimiter;
                }

                if (list[i].Length > 0)
                {
                    s += quote + list[i] + quote;
                }

                if (i % 1000 == 999 || i == list.Length - 1)
                {
                    if (s.Length > 0) lists.Add(s);
                    s = "";
                }

                i++;
            }

            return lists;
        }

        public static bool IsNumber(string s, bool minus = false)
        {
            if (minus)
            {
                return Regex.IsMatch(s, @"[\-]*[0-9]+");
            }
            else
            {
                return Regex.IsMatch(s, @"[0-9]+");
            }
        }

        public static bool IsDate(string s)
        {
            return DateTimeAgent.IsDate(s);
        }
    }
}
