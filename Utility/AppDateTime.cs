using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedicalLibrary.Utility
{
    public class AppDateTime
    {
        public enum LANG : int
        {
            ENG = 0,
            JPN = 1
        }

        public static string DateWeekdayStringFromLong(long date)
        {
            string s = "";

            if (date.ToString().Length == 8 &&
                date >= 19000000 && date < 22000000)
            {
                s = date.ToString().Insert(4, "/").Insert(7, "/");
                s += " (" + DateTime.Parse(s).ToString("ddd") + ")";
            }

            return s;
        }

        public static string DateWeekdayStringFromString(string date)
        {
            string s = "";

            long d = 0;
            long.TryParse(date, out d);

            if (d > 19000000 && d < 21000000)
            {
                s = date.Insert(4, "/").Insert(7, "/");
                s += " (" + DateTime.Parse(s).ToString("ddd") + ")";
            }

            return s;
        }

        public static string DateStringFromLong(long date)
        {
            string s = "";

            if (date.ToString().Length == 8 &&
                date >= 19000000 && date < 22000000)
            {
                s = date.ToString().Insert(4, "/").Insert(7, "/");
            }

            return s;
        }

        public static string DateStringFromString(string date)
        {
            string s = "";

            long d = 0;
            long.TryParse(date, out d);

            if (d > 19000000 && d < 21000000)
            {
                s = date.Insert(4, "/").Insert(7, "/");
            }

            return s;
        }

        public static string TimeStringFromInt6(int time)
        {
            string s = "";

            if (time.ToString().Length <= 6 &&
                time >= 0 && time <= 235959)
            {
                s = time.ToString().PadLeft(6, '0').Insert(2, ":").Insert(5, ":");
            }

            return s;
        }

        public static string TimeStringFromString6(string time)
        {
            string s = "";

            int t = 0;
            int.TryParse(time, out t);

            if (t >= 0 && t <= 235959)
            {
                s = time.PadLeft(6, '0').Insert(2, ":").Insert(5, ":");
            }

            return s;
        }

        public static string TimeStringFromInt4(int time)
        {
            string s = "";

            if (time.ToString().Length <= 4 &&
                time >= 0 && time <= 2359)
            {
                s = time.ToString().PadLeft(4, '0').Insert(2, ":");
            }

            return s;
        }

        public static string TimeStringFromString4(string time)
        {
            string s = "";

            int t = 0;
            int.TryParse(time, out t);

            if (t >= 0 && t <= 2359)
            {
                s = time.PadLeft(4, '0').Insert(2, ":");
            }

            return s;
        }

        public static string HourStringFromInt4(int time)
        {
            string s = "";

            if (time.ToString().Length <= 4 &&
                time >= 0 && time <= 2359)
            {
                s = time.ToString().PadLeft(4, '0').Substring(0, 2);
            }

            return s;
        }

        public static string HourStringFromString4(string time)
        {
            string s = "";

            int t = 0;
            int.TryParse(time, out t);

            if (t >= 0 && t <= 2359)
            {
                s = time.PadLeft(4, '0').Substring(0, 2);
            }

            return s;
        }

        public static int HourIntFromInt4(int time)
        {
            int s = 0;

            if (time.ToString().Length <= 4 &&
                time >= 0 && time <= 2359)
            {
                s = (int)(time / 100);
            }

            return s;
        }

        public static int HourIntFromString4(string time)
        {
            int s = 0;

            int t = 0;
            int.TryParse(time, out t);

            if (t >= 0 && t <= 2359)
            {
                s = (int)(t / 100);
            }

            return s;
        }

        public static string MinuteStringFromInt4(int time)
        {
            string s = "";

            if (time.ToString().Length <= 4 &&
                time >= 0 && time <= 2359)
            {
                s = time.ToString().PadLeft(4, '0').Substring(2, 2);
            }

            return s;
        }

        public static string MinuteStringFromString4(string time)
        {
            string s = "";

            int t = 0;
            int.TryParse(time, out t);

            if (t >= 0 && t <= 2359)
            {
                s = time.PadLeft(4, '0').Substring(2, 2);
            }

            return s;
        }

        public static int MinuteIntFromInt4(int time)
        {
            int s = 0;

            if (time.ToString().Length <= 4 &&
                time >= 0 && time <= 2359)
            {
                s = (int)(time % 100);
            }

            return s;
        }

        public static int MinuteIntFromString4(string time)
        {
            int s = 0;

            int t = 0;
            int.TryParse(time, out t);

            if (t >= 0 && t <= 2359)
            {
                s = (int)(t % 100);
            }

            return s;
        }
    }
}
