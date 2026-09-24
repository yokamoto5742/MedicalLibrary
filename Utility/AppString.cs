using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace MedicalLibrary.Utility
{
    public class AppString
    {
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
    }
}
