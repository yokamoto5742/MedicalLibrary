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

                // 実行ファイル自身のフォルダを優先して探す（電子カルテ登録ボタン経由の起動はカレントディレクトリが exe のフォルダと一致しないため）
                string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EyeData.xml");

                if (!File.Exists(file))
                {
                    file = AppFile.FilePath("EyeData.xml");
                }

                if (File.Exists(file))
                {
                    StreamReader reader = new StreamReader(file, Encoding.GetEncoding("shift-jis"));

                    EyeSet.ReadXml(reader);

                    reader.Close();

                    init = true;
                }
            }
        }

        /// <summary>
        /// OpeTime の行が、指定した種別で、手術日を対象範囲に含むかどうか。
        /// </summary>
        static bool IsOpeTime(DataRow r, string ope_kind, string ope_date)
        {
            // 種別が一致するものを選択
            if (!ope_kind.Equals(r["OpeKind"].ToString()))
            {
                return false;
            }

            // 手術日が対象範囲内にあるものを選択
            int date = Int32.Parse(ope_date);

            return Int32.Parse(r["Start"].ToString()) <= date && (r["End"].ToString().Length == 0 || Int32.Parse(r["End"].ToString()) >= date);
        }

        public static string GetEnd(string ope_kind, string ope_date)
        {
            foreach (DataRow r in EyeSet.Tables["OpeTime"].Rows)
            {
                if (IsOpeTime(r, ope_kind, ope_date))
                {
                    return r["End"].ToString();
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
                if (IsOpeTime(r, ope_kind, ope_date))
                {
                    return r["Waku"].ToString().Split(',');
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
                if (IsOpeTime(r, ope_kind, ope_date))
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
            return 30.0 - CalcVisdine(height, weight);
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

    }
}
