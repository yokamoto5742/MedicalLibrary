using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Windows.Forms;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class EyeDict
    {
        public static DataSet EyeSet;

        /// <summary>
        /// 手術種別の辞書
        /// </summary>
        static Dictionary<string, string> opeKindDict = new Dictionary<string, string>();

        /// <summary>
        /// 手術種別の辞書
        /// </summary>
        public static Dictionary<string, string> OpeKindDict
        {
            get
            {
                if (opeKindDict.Count == 0)
                {
                    opeKindDict.Add("0", "");

                    foreach (DataRow r in EyeSet.Tables["OpeKind"].Rows)
                    {
                        opeKindDict.Add(r["ID"].ToString(), r["Name"].ToString());
                    }
                }

                return opeKindDict;
            }
        }

        static Dictionary<string, string> opeRoomDict = new Dictionary<string, string>();

        public static Dictionary<string, string> OpeRoomDict
        {
            get
            {
                if (opeRoomDict.Count == 0)
                {
                    opeRoomDict.Add("0", "");

                    foreach (DataRow r in EyeSet.Tables["OpeRoom"].Rows)
                    {
                        opeRoomDict.Add(r["ID"].ToString(), r["Value"].ToString());
                    }
                }

                return opeRoomDict;
            }
        }

        /// <summary>
        /// 初期化されているかどうか
        /// </summary>
        static bool init = false;

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="force"></param>
        public static void Init(bool force = false)
        {
            if (force || !init)
            {
                EyeSet = new DataSet();

                string file = AppFile.FilePath("EyeData.xml");

                if (File.Exists(file))
                {
                    StreamReader reader = new StreamReader(file, Encoding.GetEncoding("shift-jis"));

                    EyeSet.ReadXml(reader);

                    reader.Close();

                    init = true;
                }
            }
        }

        public static string GetEnd(string ope_kind, string ope_date)
        {
            foreach (DataRow r in EyeSet.Tables["OpeTime"].Rows)
            {
                // 種別が一致するものを選択
                if (ope_kind.Equals(r["OpeKind"].ToString()))
                {
                    // 手術日が対象範囲内にあるものを選択
                    if (Int32.Parse(r["Start"].ToString()) <= Int32.Parse(ope_date) && (r["End"].ToString().Length == 0 || Int32.Parse(r["End"].ToString()) >= Int32.Parse(ope_date)))
                    {
                        return r["End"].ToString();
                    }
                }
            }

            return "29991231";
        }

        /// <summary>
        /// 手術日の時間枠を配列で返す。
        /// </summary>
        /// <param name="ope_kind"></param>
        /// <param name="ope_date"></param>
        /// <returns></returns>
        public static string[] GetWakus(string ope_kind, string ope_date)
        {
            foreach (DataRow r in EyeSet.Tables["OpeTime"].Rows)
            {
                // 種別が一致するものを選択
                if (ope_kind.Equals(r["OpeKind"].ToString()))
                {
                    // 手術日が対象範囲内にあるものを選択
                    if (Int32.Parse(r["Start"].ToString()) <= Int32.Parse(ope_date) && (r["End"].ToString().Length == 0 || Int32.Parse(r["End"].ToString()) >= Int32.Parse(ope_date)))
                    {
                        return r["Waku"].ToString().Split(',');
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 手術日の時間枠ごとの人数を配列で返す。
        /// </summary>
        /// <param name="ope_kind"></param>
        /// <param name="ope_date"></param>
        /// <returns></returns>
        public static string[] GetWakuNums(string ope_kind, string ope_date)
        {
            foreach (DataRow r in EyeSet.Tables["OpeTime"].Rows)
            {
                // 種別が一致するものを選択
                if (ope_kind.Equals(r["OpeKind"].ToString()))
                {
                    // 手術日が対象範囲内にあるものを選択
                    if (Int32.Parse(r["Start"].ToString()) <= Int32.Parse(ope_date) && (r["End"].ToString().Length == 0 || Int32.Parse(r["End"].ToString()) >= Int32.Parse(ope_date)))
                    {
                        // 曜日が一致するものを選択
                        string wday = ((int)DateTime.Parse(ope_date.Insert(4, "/").Insert(7, "/")).DayOfWeek).ToString();

                        foreach (string s in r["Time"].ToString().Split('\n'))
                        {
                            if (s.Trim('\r').Length > 0 && s.Trim('\r').StartsWith(wday))
                            {
                                return s.Trim('\r').Split('=')[1].Split(',');
                            }
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 身長・体重からビスダイン溶液の量を計算する。
        /// </summary>
        /// <returns></returns>
        public static double CalcVisdine(double height, double weight)
        {
            return Math.Round(Math.Pow(height, 0.725) * Math.Pow(weight, 0.425) * 0.007184 * 3, 1);
        }

        /// <summary>
        /// 身長・体重からブドウ糖液の量を計算する。
        /// </summary>
        /// <returns></returns>
        public static double CalcGrape(double height, double weight)
        {
            return (30.0 - Math.Round(Math.Pow(height, 0.725) * Math.Pow(weight, 0.425) * 0.007184 * 3, 1));
        }

        /// <summary>
        /// 等価球面度数を計算する。
        /// </summary>
        /// <param name="s">球面度数</param>
        /// <param name="c">円柱度数</param>
        /// <returns></returns>
        public static double CalcSE(double s, double c)
        {
            return (s + c / 2);
        }

        /// <summary>
        /// IOL誤差を計算する。
        /// </summary>
        /// <param name="se">等価球面度数</param>
        /// <param name="r">術前予想屈折</param>
        /// <returns></returns>
        public static double CalcIOLGosa(double se, double r)
        {
            return (se - r);
        }

        /// <summary>
        /// 眼圧平均値を計算する。
        /// </summary>
        /// <param name="t1">1回目</param>
        /// <param name="t2">2回目</param>
        /// <param name="t3">3回目</param>
        /// <returns></returns>
        public static double CalcTensionAvg(double t1, double t2, double t3)
        {
            int i = 0;
            double total = 0;
            double avg = 0;

            if (t1 > 0)
            {
                total += t1;
                i++;
            }

            if (t2 > 0)
            {
                total += t2;
                i++;
            }

            if (t3 > 0)
            {
                total += t3;
                i++;
            }

            if (i > 0)
            {
                avg = Math.Round(total / i, 1);
            }

            return avg;
        }

        /// <summary>
        /// 検査データ文字列から遠見視力データを取得する。
        /// </summary>
        /// <param name="cont"></param>
        /// <returns></returns>
        public static string GetSightString(string cont)
        {
            string sight_r = "";
            string sight_l = "";

            Dictionary<string, string> tmpDict = new Dictionary<string, string>();

            tmpDict.Add("101R", "");
            tmpDict.Add("102R", "");
            tmpDict.Add("103R", "");
            tmpDict.Add("104R", "");
            tmpDict.Add("105R", "");
            tmpDict.Add("106R", "");
            tmpDict.Add("101L", "");
            tmpDict.Add("102L", "");
            tmpDict.Add("103L", "");
            tmpDict.Add("104L", "");
            tmpDict.Add("105L", "");
            tmpDict.Add("106L", "");
            tmpDict.Add("107R", "");
            tmpDict.Add("107L", "");
            tmpDict.Add("108R", "");
            tmpDict.Add("108L", "");
            tmpDict.Add("109R", "");
            tmpDict.Add("110R", "");
            tmpDict.Add("109L", "");
            tmpDict.Add("110L", "");
            tmpDict.Add("109B", "");

            string tmpValue = "";

            foreach (string line in cont.Split('\r', '\n'))
            {
                string[] s = line.Split(',');

                if (tmpDict.ContainsKey(s[0]))
                {
                    tmpValue = "";

                    for (int i = 1; i < s.Length; i++)
                    {
                        if (tmpValue.Length > 0)
                        {
                            tmpValue += ",";
                        }

                        tmpValue += s[i].Replace("<CR+LF>", "\r\n");
                    }

                    tmpDict[s[0]] = tmpValue;
                }
            }

            // 遠見視力（右）データ作成
            if (tmpDict["108R"].Equals("1"))
            {
                sight_r = "RV=" + tmpDict["109R"] + "(" + tmpDict["110R"] + ")";
            }
            else if (tmpDict["107R"].Equals("1"))
            {
                sight_r = "RV=" + tmpDict["101R"] + "(" + tmpDict["102R"] + "×" + tmpDict["103R"] + "D)";
            }
            else
            {
                sight_r = "RV=" + tmpDict["101R"] + "(" + tmpDict["102R"] + "×" + tmpDict["103R"] + "D=cyl" + tmpDict["104R"] + "D Ax" + tmpDict["105R"] + "°)";
            }

            // 遠見視力（左）データ作成
            if (tmpDict["108L"].Equals("1"))
            {
                sight_l = "LV=" + tmpDict["109L"] + "(" + tmpDict["110L"] + ")";
            }
            else if (tmpDict["107L"].Equals("1"))
            {
                sight_l = "LV=" + tmpDict["101L"] + "(" + tmpDict["102L"] + "×" + tmpDict["103L"] + "D)";
            }
            else
            {
                sight_l = "LV=" + tmpDict["101L"] + "(" + tmpDict["102L"] + "×" + tmpDict["103L"] + "D=cyl" + tmpDict["104L"] + "D Ax" + tmpDict["105L"] + "°)";
            }

            return sight_r + "\r\n" + sight_l;
        }

        /// <summary>
        /// 検査データ文字列から眼圧データを取得する。
        /// </summary>
        /// <param name="cont"></param>
        /// <returns></returns>
        public static string GetTensionString(string cont)
        {
            string tension_r = "";
            string tension_l = "";

            Dictionary<string, string> tmpDict = new Dictionary<string, string>();

            tmpDict.Add("301R", "");
            tmpDict.Add("302R", "");
            tmpDict.Add("303R", "");
            tmpDict.Add("304R", "");
            tmpDict.Add("301L", "");
            tmpDict.Add("302L", "");
            tmpDict.Add("303L", "");
            tmpDict.Add("304L", "");

            string tmpValue = "";

            foreach (string line in cont.Split('\r', '\n'))
            {
                string[] s = line.Split(',');

                if (tmpDict.ContainsKey(s[0]))
                {
                    tmpValue = "";

                    for (int i = 1; i < s.Length; i++)
                    {
                        if (tmpValue.Length > 0)
                        {
                            tmpValue += ",";
                        }

                        tmpValue += s[i].Replace("<CR+LF>", "\r\n");
                    }

                    tmpDict[s[0]] = tmpValue;
                }
            }

            // 眼圧（右）データ作成
            tension_r = "RT=" + tmpDict["301R"] + "," + tmpDict["302R"] + "," + tmpDict["303R"] + " (AVG)" + tmpDict["304R"] + "mmHg";

            // 眼圧（左）データ作成
            tension_l = "LT=" + tmpDict["301L"] + "," + tmpDict["302L"] + "," + tmpDict["303L"] + " (AVG)" + tmpDict["304L"] + "mmHg";

            return tension_r + "\r\n" + tension_l;
        }
    }
}
