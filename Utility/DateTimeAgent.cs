using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace MedicalLibrary.Utility
{
    public class DateTimeAgent
    {
        public enum DateFormatKind : int
        {
            /// <summary>
            /// 西暦 yyyy/MM/dd
            /// </summary>
            LONG = 1,

            /// <summary>
            /// 西暦 yy/MM/dd
            /// </summary>
            SHORT = 2,

            /// <summary>
            /// 西暦 yyyy/MM/dd(ddd)
            /// </summary>
            WLONG = 3,

            /// <summary>
            /// 西暦 yy/MM/dd(ddd)
            /// </summary>
            WSHORT = 4,

            /// <summary>
            /// 和暦 ggyy年M月d日（例：平成20年5月1日）
            /// </summary>
            J1 = 5,

            /// <summary>
            /// 和暦 M/T/S/H yy/M/d（例：H20/5/1）
            /// </summary>
            J2 = 6,

            /// <summary>
            /// 和暦 ggyy年M月d日(ddd)
            /// </summary>
            JW1 = 7,

            /// <summary>
            /// 和暦 gyy/M/d(ddd)
            /// </summary>
            JW2 = 8,

            /// <summary>
            /// M/d
            /// </summary>
            MD = 9,

            /// <summary>
            /// M/dd
            /// </summary>
            MDD = 10,

            /// <summary>
            /// M/d(ddd)
            /// </summary>
            MDW = 11
        }

        public static CultureInfo DefaultCulture
        {
            get
            {
                CultureInfo result = new CultureInfo("ja-JP", true);

                result.DateTimeFormat.Calendar = new JapaneseCalendar();

                return result;
            }
        }

        /// <summary>
        /// 日付かどうかの判定
        /// </summary>
        /// <param name="date">日付文字列（yyyy/MM/dd）または8桁の日付（yyyyMMdd）</param>
        /// <returns></returns>
        public static bool IsDate(string date)
        {
            bool b = false;
            DateTime dt = DateTime.Now;

            b = DateTime.TryParse(date, out dt);

            if (!b) b = DateTime.TryParse(DateTimeAgent.DateFormat(date, DateFormatKind.LONG), out dt);

            return b;
        }

        /// <summary>
        /// 日付かどうかの判定
        /// </summary>
        /// <param name="date">8桁の日付数字（yyyyMMdd）</param>
        /// <returns></returns>
        public static bool IsDate(int date)
        {
            return IsDate(date.ToString());
        }

        /// <summary>
        /// 日付文字列のフォーマット
        /// </summary>
        /// <param name="org_date">8桁の日付文字列</param>
        /// <param name="kind">J1: 平成20年5月1日, J2: H20/5/1, JW1: 平成20年5月1日(木), WLONG: 2008/05/01(木), MDD: 5/1(木)</param>
        /// <returns></returns>
        public static string DateFormat(string org_date, DateFormatKind kind)
        {
            if (org_date.Length != 8)
            {
                return "";
            }

            // LONG / SHORT は文字列の区切りを入れるだけ（日付として正しいかは見ない）
            if (kind == DateFormatKind.LONG)
            {
                return org_date.Insert(4, "/").Insert(7, "/");
            }
            else if (kind == DateFormatKind.SHORT)
            {
                return org_date.Substring(2).Insert(2, "/").Insert(5, "/");
            }

            DateTime dt;

            // "00000000" のような日付として不正な値は空文字とする
            if (!DateTime.TryParseExact(org_date, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {
                return "";
            }

            switch (kind)
            {
                case DateFormatKind.WLONG:
                    return dt.ToString("yyyy/MM/dd(ddd)");
                case DateFormatKind.WSHORT:
                    return dt.ToString("yy/MM/dd(ddd)");
                case DateFormatKind.J1:
                    return JFormat(dt, "年", "M月d日", false);
                case DateFormatKind.J2:
                    return JFormat(dt, "/", "M/d", true);
                case DateFormatKind.JW1:
                    return JFormat(dt, "年", "M月d日(ddd)", false);
                case DateFormatKind.JW2:
                    return JFormat(dt, "/", "M/d(ddd)", true);
                case DateFormatKind.MD:
                    return dt.ToString("M/d", DefaultCulture);
                case DateFormatKind.MDD:
                    return dt.ToString("M/dd", DefaultCulture);
                case DateFormatKind.MDW:
                    return dt.ToString("M/d(ddd)", DefaultCulture);
            }

            return "";
        }

        /// <summary>
        /// 元号の置き換え（J2 / JW2 用）
        /// </summary>
        static readonly string[,] era_alphabet =
        {
            { "明治", "M" }, { "大正", "T" }, { "昭和", "S" }, { "平成", "H" }, { "令和", "R" }
        };

        /// <summary>
        /// 和暦の書式にする。
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="year_sep">年と月日の区切り（"年" または "/"）</param>
        /// <param name="md_format">月日の書式</param>
        /// <param name="alphabet">true: 元号をアルファベット1文字にする</param>
        /// <returns></returns>
        static string JFormat(DateTime dt, string year_sep, string md_format, bool alphabet)
        {
            string result;

            // .NET Framework が令和対応していないPCのため
            if (dt >= new DateTime(2019, 5, 1))
            {
                result = "令和" + (dt.Year - 2018).ToString().PadLeft(2, '0') + year_sep + dt.ToString(md_format, DefaultCulture);
            }
            else
            {
                result = dt.ToString("ggyy" + year_sep + md_format, DefaultCulture);
            }

            if (alphabet)
            {
                for (int i = 0; i < era_alphabet.GetLength(0); i++)
                {
                    if (result.StartsWith(era_alphabet[i, 0]))
                    {
                        result = result.Replace(era_alphabet[i, 0], era_alphabet[i, 1]);
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 日付数字のフォーマット
        /// </summary>
        /// <param name="org_date">8桁の日付数字</param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static string DateFormat(int org_date, DateFormatKind kind)
        {
            string result = "";

            if (org_date.ToString().Length == 8)
            {
                result = DateFormat(org_date.ToString(), kind);
            }

            return result;
        }

        /// <summary>
        /// 時刻数字のフォーマット
        /// </summary>
        /// <param name="org_time">4桁の時刻数字</param>
        /// <param name="zero">0 は有効な時刻（00:00）か</param>
        /// <returns></returns>
        public static string TimeFormat(string org_time, bool zero = false)
        {
            string result = "";

            if (org_time.Length == 0)
            {
                return result;
            }

            // 0 が無効の場合
            if (!zero && org_time.Equals("0"))
            {
                return result;
            }

            DateTime dt = new DateTime();

            if (org_time.Length <= 4 && DateTime.TryParse(org_time.PadLeft(4, '0').Insert(2, ":"), out dt))
            {
                result = dt.ToString("HH:mm");
            }

            return result;
        }

        /// <summary>
        /// 時間数字のフォーマット
        /// </summary>
        /// <param name="org_time">4桁以下の時間数字</param>
        /// <param name="zero">0 は有効な時刻（00:00）か</param>
        /// <returns></returns>
        public static string TimeFormat(int org_time, bool zero = false)
        {
            string result = "";

            // 0 が無効の場合
            if (!zero && org_time.Equals(0))
            {
                return result;
            }

            DateTime dt = new DateTime();

            if (org_time.ToString().Length <= 4 && DateTime.TryParse(org_time.ToString().PadLeft(4, '0').Insert(2, ":"), out dt))
            {
                result = dt.ToString("HH:mm");
            }

            return result;
        }

        /// <summary>
        /// 時刻数字のフォーマット
        /// </summary>
        /// <param name="org_time">6桁の時刻数字</param>
        /// <param name="len">返す時刻数字の長さ。6 または 4</param>
        /// <param name="zero">0 は有効な時刻（00:00）か</param>
        /// <returns></returns>
        public static string TimeFormat6(string org_time, int len = 6, bool zero = false)
        {
            string result = "";

            if (org_time.Length == 0)
            {
                return result;
            }

            // 0 が無効の場合
            if (!zero && org_time.Equals("0"))
            {
                return result;
            }

            DateTime dt = new DateTime();

            if (org_time.Length <= 6 && DateTime.TryParse(org_time.PadLeft(6, '0').Insert(2, ":").Insert(5, ":"), out dt))
            {
                if (len == 6)
                {
                    result = dt.ToString("HH:mm:ss");
                }
                else if (len == 4)
                {
                    result = dt.ToString("HH:mm");
                }
            }

            return result;
        }

        /// <summary>
        /// 時間数字のフォーマット
        /// </summary>
        /// <param name="org_time">6桁以下の時間数字</param>
        /// <param name="len">返す時刻数字の長さ。6 または 4</param>
        /// <param name="zero">0 は有効な時刻（00:00）か</param>
        /// <returns></returns>
        public static string TimeFormat6(int org_time, int len = 6, bool zero = false)
        {
            string result = "";

            // 0 が無効の場合
            if (!zero && org_time.Equals(0))
            {
                return result;
            }

            DateTime dt = new DateTime();

            if (org_time.ToString().Length <= 6 && DateTime.TryParse(org_time.ToString().PadLeft(6, '0').Insert(2, ":").Insert(5, ":"), out dt))
            {
                if (len == 6)
                {
                    result = dt.ToString("HH:mm:ss");
                }
                else if (len == 4)
                {
                    result = dt.ToString("HH:mm");
                }
            }

            return result;
        }

        /// <summary>
        /// 時間を足す。
        /// </summary>
        /// <param name="crit_time">時分を表す数字4桁。HHmm</param>
        /// <param name="minutes">分 mm</param>
        /// <returns></returns>
        public static int AddTime(int crit_time, int minutes)
        {
            int rh = crit_time / 100 + minutes / 60;
            int rm = crit_time % 100 + minutes % 60;

            if (rm >= 60)
            {
                rh += 1;
                rm -= 60;
            }

            return rh * 100 + rm;
        }

        /// <summary>
        /// 年齢を計算する
        /// </summary>
        /// <param name="birth"></param>
        /// <param name="today"></param>
        /// <returns></returns>
        public static int AgeCalc(int birth, int today)
        {
            int result = -1;

            if (birth.ToString().Length == 8 && today.ToString().Length == 8)
            {
                int y1 = birth / 10000;
                int m1 = (birth % 10000) / 100;
                int d1 = birth % 100;

                int y2 = today / 10000;
                int m2 = (today % 10000) / 100;
                int d2 = today % 100;

                if (m1 < m2 || (m1 == m2 && d1 <= d2))
                {
                    result = y2 - y1;
                }
                else
                {
                    result = y2 - y1 - 1;
                }
            }

            return result;
        }

        /// <summary>
        /// 年齢を計算する
        /// </summary>
        /// <param name="birth"></param>
        /// <param name="today"></param>
        /// <returns></returns>
        public static int AgeCalc(string birth, string today)
        {
            int b = 0;
            int t = 0;

            int.TryParse(birth, out b);
            int.TryParse(today, out t);

            return AgeCalc(b, t);
        }

        /// <summary>
        /// 元号ごとの基準年（元号の年 + 基準年 = 西暦）。添字は gen（1: 明治, 2: 大正, 3: 昭和, 4: 平成, 5: 令和）
        /// </summary>
        static readonly int[] era_base = { 0, 1867, 1911, 1925, 1988, 2018 };

        /// <summary>
        /// 和暦を西暦に変換する
        /// </summary>
        /// <param name="gen">1: 明治, 2: 大正, 3: 昭和, 4: 平成, 5: 令和</param>
        /// <param name="gy"></param>
        /// <param name="m"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static int JtoW(int gen, int gy, int m, int d)
        {
            if (gen < 1 || gen > 5)
            {
                return 0;
            }

            return (gy + era_base[gen]) * 10000 + m * 100 + d;
        }

        /// <summary>
        /// 和暦を西暦に変換する
        /// </summary>
        /// <param name="gen">1: 明治, 2: 大正, 3: 昭和, 4: 平成, 5: 令和</param>
        /// <param name="gyymmdd"></param>
        /// <returns></returns>
        public static int JtoW(int gen, int gyymmdd)
        {
            if (gen < 1 || gen > 5)
            {
                return 0;
            }

            return gyymmdd + era_base[gen] * 10000;
        }

        /// <summary>
        /// 曜日を返す。
        /// 0 日, 1 月, 2 火, 3 水, 4 木, 5 金, 6 土
        /// </summary>
        /// <param name="orgDate">日付8桁</param>
        /// <returns></returns>
        public static string JWeekday(string orgDate)
        {
            string wday = "";

            DateTime tmpDate;

            if (orgDate.Length != 8)
            {
                return wday;
            }

            if (!DateTime.TryParse(orgDate.Insert(4, "/").Insert(7, "/"), out tmpDate))
            {
                return wday;
            }

            switch (tmpDate.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    wday = "日";
                    break;
                case DayOfWeek.Monday:
                    wday = "月";
                    break;
                case DayOfWeek.Tuesday:
                    wday = "火";
                    break;
                case DayOfWeek.Wednesday:
                    wday = "水";
                    break;
                case DayOfWeek.Thursday:
                    wday = "木";
                    break;
                case DayOfWeek.Friday:
                    wday = "金";
                    break;
                case DayOfWeek.Saturday:
                    wday = "土";
                    break;
            }

            return wday;
        }
    }
}
