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
            string result = "";

            if (org_date.Length == 8)
            {
                if (kind == DateFormatKind.LONG)
                {
                    result = org_date.Insert(4, "/").Insert(7, "/");
                }
                else if (kind == DateFormatKind.SHORT)
                {
                    result = org_date.Substring(2).Insert(2, "/").Insert(5, "/");
                }
                else if (kind == DateFormatKind.WLONG)
                {
                    result = DateTime.Parse(org_date.Insert(4, "/").Insert(7, "/")).ToString("yyyy/MM/dd(ddd)");
                }
                else if (kind == DateFormatKind.WSHORT)
                {
                    result = DateTime.Parse(org_date.Insert(4, "/").Insert(7, "/")).ToString("yy/MM/dd(ddd)");
                }
                else if (kind == DateFormatKind.J1)
                {
                    // .NET Framework が令和対応していないPCのため
                    if (org_date.CompareTo("20190501") >= 0)
                    {
                        result = "令和" + (int.Parse(org_date.Substring(0, 4)) - 2018).ToString().PadLeft(2, '0') + "年" +
                            DateTime.Parse(org_date.Insert(4, "/").Insert(7, "/")).ToString("M月d日", DefaultCulture);
                    }
                    else
                    {
                        result = DateTime.Parse(org_date.Insert(4, "/").Insert(7, "/")).ToString("ggyy年M月d日", DefaultCulture);
                    }
                }
                else if (kind == DateFormatKind.J2)
                {
                    // .NET Framework が令和対応していないPCのため
                    if (org_date.CompareTo("20190501") >= 0)
                    {
                        result = "令和" + (int.Parse(org_date.Substring(0, 4)) - 2018).ToString().PadLeft(2, '0') + "/" +
                            DateTime.Parse(org_date.Insert(4, "/").Insert(7, "/")).ToString("M/d", DefaultCulture);
                    }
                    else
                    {
                        result = DateTime.Parse(org_date.Insert(4, "/").Insert(7, "/")).ToString("gyy/M/d", DefaultCulture);
                    }

                    if (result.StartsWith("明治"))
                    {
                        result = result.Replace("明治", "M");
                    }
                    else if (result.StartsWith("大正"))
                    {
                        result = result.Replace("大正", "T");
                    }
                    else if (result.StartsWith("昭和"))
                    {
                        result = result.Replace("昭和", "S");
                    }
                    else if (result.StartsWith("平成"))
                    {
                        result = result.Replace("平成", "H");
                    }
					else if (result.StartsWith("令和"))
					{
						result = result.Replace("令和", "R");
					}
				}
                else if (kind == DateFormatKind.JW1)
                {
                    // .NET Framework が令和対応していないPCのため
                    if (org_date.CompareTo("20190501") >= 0)
                    {
                        result = "令和" + (int.Parse(org_date.Substring(0, 4)) - 2018).ToString().PadLeft(2, '0') + "年" +
                            DateTime.Parse(org_date.Insert(4, "/").Insert(7, "/")).ToString("M月d日(ddd)", DefaultCulture);
                    }
                    else
                    {
                        result = DateTime.Parse(org_date.Insert(4, "/").Insert(7, "/")).ToString("ggyy年M月d日(ddd)", DefaultCulture);
                    }
                }
                else if (kind == DateFormatKind.JW2)
                {
                    // .NET Framework が令和対応していないPCのため
                    if (org_date.CompareTo("20190501") >= 0)
                    {
                        result = "令和" + (int.Parse(org_date.Substring(0, 4)) - 2018).ToString().PadLeft(2, '0') + "/" +
                            DateTime.Parse(org_date.Insert(4, "/").Insert(7, "/")).ToString("M/d(ddd)", DefaultCulture);
                    }
                    else
                    {
                        result = DateTime.Parse(org_date.Insert(4, "/").Insert(7, "/")).ToString("gyy/M/d(ddd)", DefaultCulture);
                    }

                    if (result.StartsWith("明治"))
                    {
                        result = result.Replace("明治", "M");
                    }
                    else if (result.StartsWith("大正"))
                    {
                        result = result.Replace("大正", "T");
                    }
                    else if (result.StartsWith("昭和"))
                    {
                        result = result.Replace("昭和", "S");
                    }
                    else if (result.StartsWith("平成"))
                    {
                        result = result.Replace("平成", "H");
                    }
					else if (result.StartsWith("令和"))
					{
						result = result.Replace("令和", "R");
					}
				}
                else if (kind == DateFormatKind.MD)
                {
                    result = DateTime.Parse(org_date.Insert(4, "/").Insert(7, "/")).ToString("M/d", DefaultCulture);
                }
                else if (kind == DateFormatKind.MDD)
                {
                    result = DateTime.Parse(org_date.Insert(4, "/").Insert(7, "/")).ToString("M/dd", DefaultCulture);
                }
                else if (kind == DateFormatKind.MDW)
                {
                    result = DateTime.Parse(org_date.Insert(4, "/").Insert(7, "/")).ToString("M/d(ddd)", DefaultCulture);
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
        /// 日付文字列（/有り）を数字で返す。
        /// </summary>
        /// <param name="org_date">日付文字列（/有り）</param>
        /// <returns></returns>
        public static int DateToInt(string org_date)
        {
            int result = 0;
            DateTime dt = new DateTime();

            if (DateTime.TryParse(org_date, out dt))
            {
                result = dt.Year * 10000 + dt.Month * 100 + dt.Day;
            }

            return result;
        }

        /// <summary>
        /// DateTimeオブジェクトを返す。
        /// </summary>
        /// <param name="org_date"></param>
        /// <returns></returns>
        public static DateTime DateTimeFromInt(int org_date)
        {
            DateTime dt = DateTime.Now;

            if (org_date.ToString().Length == 8)
            {
                DateTime.TryParse(org_date.ToString().Insert(4, "/").Insert(7, "/"), out dt);
            }

            return dt;
        }

        /// <summary>
        /// 日付を計算する。
        /// </summary>
        /// <param name="crit_date">基準日</param>
        /// <param name="days">足す日数</param>
        /// <returns></returns>
        public static int AddDays(int crit_date, int days)
        {
            return int.Parse(DateTime.Parse(crit_date.ToString().Insert(4, "/").Insert(7, "/")).AddDays(days).ToString("yyyyMMdd"));
        }

        /// <summary>
        /// 日付を計算する。
        /// </summary>
        /// <param name="crit_date">基準日。8桁数字。スラッシュなし</param>
        /// <param name="days">足す日数</param>
        /// <returns></returns>
        public static int AddDays(string crit_date, int days)
        {
            return int.Parse(DateTime.Parse(crit_date.Insert(4, "/").Insert(7, "/")).AddDays(days).ToString("yyyyMMdd"));
        }

        /// <summary>
        /// 日付を計算する。
        /// </summary>
        /// <param name="crit_date">基準日</param>
        /// <param name="days">足す月数</param>
        /// <returns></returns>
        public static int AddMonths(int crit_date, int months)
        {
            return int.Parse(DateTime.Parse(crit_date.ToString().Insert(4, "/").Insert(7, "/")).AddMonths(months).ToString("yyyyMMdd"));
        }

        /// <summary>
        /// 該当月の日数を返す
        /// </summary>
        /// <param name="crit_date"></param>
        /// <returns></returns>
        public static int Days(DateTime crit_date)
        {
            return int.Parse(DateTime.Parse(crit_date.AddMonths(1).ToString("yyyy/MM/01")).AddDays(-1).ToString("dd").TrimStart('0'));
        }

        /// <summary>
        /// 当月最終日を返す
        /// </summary>
        /// <param name="crit_date">基準日</param>
        /// <returns></returns>
        public static DateTime LastDateOfMonth(DateTime crit_date)
        {
            return DateTime.Parse(crit_date.ToString("yyyy/MM/01")).AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// 当月最終日を返す
        /// </summary>
        /// <param name="crit_date">基準日</param>
        /// <returns></returns>
        public static int LastDateOfMonth(int crit_date)
        {
            int result = 0;
            DateTime dt = DateTime.Now;

            if (crit_date.ToString().Length == 8 && DateTime.TryParse(crit_date.ToString().Insert(4, "/").Insert(7, "/"), out dt))
            {
                result = int.Parse(DateTime.Parse(dt.ToString("yyyy/MM/01")).AddMonths(1).AddDays(-1).ToString("yyyyMMdd"));
            }

            return result;
        }

        /// <summary>
        /// 前月最終日を返す
        /// </summary>
        /// <param name="crit_date">基準日</param>
        /// <returns></returns>
        public static DateTime LastDateOfPrevMonth(DateTime crit_date)
        {
            return DateTime.Parse(crit_date.ToString("yyyy/MM/01")).AddDays(-1);
        }

        /// <summary>
        /// 前月最終日を返す
        /// </summary>
        /// <param name="crit_date">基準日</param>
        /// <returns></returns>
        public static int LastDateOfPrevMonth(int crit_date)
        {
            int result = 0;
            DateTime dt = DateTime.Now;

            if (crit_date.ToString().Length == 8 && DateTime.TryParse(crit_date.ToString().Insert(4, "/").Insert(7, "/"), out dt))
            {
                result = int.Parse(DateTime.Parse(dt.ToString("yyyy/MM/01")).AddDays(-1).ToString("yyyyMMdd"));
            }

            return result;
        }

        /// <summary>
        /// 日数差を返す。
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static int IntervalDays(int date1, int date2)
        {
            return IntervalDays(date1.ToString(), date2.ToString());
        }

        /// <summary>
        /// 日数差を返す。
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static int IntervalDays(string date1, string date2)
        {
            string d1 = AppString.IsDate(date1) ? date1 : DateTime.Now.ToString("yyyyMMdd");
            string d2 = AppString.IsDate(date2) ? date2 : DateTime.Now.ToString("yyyyMMdd");

            DateTime dt1 = DateTime.Parse(d1.Insert(4, "/").Insert(7, "/"));
            DateTime dt2 = DateTime.Parse(d2.Insert(4, "/").Insert(7, "/"));

            return dt2.Subtract(dt1).Days;
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
        /// 時間の差分を返す。（分単位）
        /// </summary>
        /// <param name="start_time"></param>
        /// <param name="end_time"></param>
        /// <returns></returns>
        public static int IntervalMinutes(int start_time, int end_time)
        {
            int h = end_time / 100 - start_time / 100;
            int m = end_time % 100 - start_time % 100;

            if (m < 0)
            {
                h -= 1;
                m += 60;
            }

            return h * 60 + m;
        }

        /// <summary>
        /// 時刻文字列（:有り）を数字で返す。
        /// </summary>
        /// <param name="org_time">時刻文字列（/有り）</param>
        /// <returns></returns>
        public static int TimeToInt(string org_time)
        {
            int result = 0;
            DateTime dt = new DateTime();

            if (DateTime.TryParse(org_time, out dt))
            {
                result = dt.Hour * 100 + dt.Minute;
            }

            return result;
        }

        /// <summary>
        /// 時刻文字列（:有り）を数字で返す。
        /// </summary>
        /// <param name="org_time">時刻文字列（/有り）</param>
        /// <returns></returns>
        public static int TimeToInt6(string org_time)
        {
            int result = 0;
            DateTime dt = new DateTime();

            if (DateTime.TryParse(org_time, out dt))
            {
                result = dt.Hour * 10000 + dt.Minute * 100 + dt.Second;
            }

            return result;
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
        /// 和暦を西暦に変換する
        /// </summary>
        /// <param name="gen">1: 明治, 2: 大正, 3: 昭和, 4: 平成, 5: 令和</param>
        /// <param name="gy"></param>
        /// <param name="m"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static int JtoW(int gen, int gy, int m, int d)
        {
            int i = 0;

            if (gen < 1 || gen > 5)
            {
                return i;
            }

            if (gen == 1)
            {
                i += (gy + 1867) * 10000;
            }
            else if (gen == 2)
            {
                i += (gy + 1911) * 10000;
            }
            else if (gen == 3)
            {
                i += (gy + 1925) * 10000;
            }
            else if (gen == 4)
            {
                i += (gy + 1988) * 10000;
            }
			else if (gen == 5)
			{
				i += (gy + 2018) * 10000;
			}

            i += m * 100 + d;

            return i;
        }

        /// <summary>
        /// 和暦を西暦に変換する
        /// </summary>
        /// <param name="gen">1: 明治, 2: 大正, 3: 昭和, 4: 平成, 5: 令和</param>
        /// <param name="gyymmdd"></param>
        /// <returns></returns>
        public static int JtoW(int gen, int gyymmdd)
        {
            int i = 0;

            if (gen < 1 || gen > 5)
            {
                return i;
            }

            if (gen == 1)
            {
                i += gyymmdd + 1867 * 10000;
            }
            else if (gen == 2)
            {
                i += gyymmdd + 1911 * 10000;
            }
            else if (gen == 3)
            {
                i += gyymmdd + 1925 * 10000;
            }
            else if (gen == 4)
            {
                i += gyymmdd + 1988 * 10000;
            }
			else if (gen == 5)
			{
				i += gyymmdd + 2018 * 10000;
			}

            return i;
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
