using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class NidekARK1
    {
        public static NidekARK1Settings Settings = new NidekARK1Settings();

        /// <summary>
        /// 元のXMLデータファイル
        /// </summary>
        public string SourceFile = "";

        /// <summary>
        /// テキスト変換後のデータ
        /// </summary>
        public string ConvertData
        {
            get
            {
                return Convert(this.SourceFile);
            }
        }

        /// <summary>
        /// 元のXMLデータファイル保存日
        /// </summary>
        public string SaveDate
        {
            get
            {
                string s = "";
                string f = Path.GetFileName(this.SourceFile);

                if (Path.GetExtension(f).Equals(".xml") && f.Length >= 18)
                {
                    s = f.Substring(f.Length - 18, 8);
                }

                return s;
            }
        }

        /// <summary>
        /// 元のXMLデータファイル保存時刻
        /// </summary>
        public string SaveTime
        {
            get
            {
                string s = "";
                string f = Path.GetFileName(this.SourceFile);

                if (Path.GetExtension(f).Equals(".xml") && f.Length >= 10)
                {
                    s = f.Substring(f.Length - 10, 6);
                }

                return s;
            }
        }

        /// <summary>
        /// 元データフォルダからXMLファイルのリストを取得する
        /// （日時降順）
        /// </summary>
        /// <returns></returns>
        public static List<NidekARK1> GetList()
        {
            List<NidekARK1> list = new List<NidekARK1>();

            // 元データフォルダが存在しなければ終了
            if (!Directory.Exists(Settings.SourcePath))
            {
                return list;
            }

            foreach (string file in Directory.GetFiles(Settings.SourcePath))
            {
                // XMLファイルでなければ飛ばす
                if (!Path.GetExtension(file).Equals(".xml"))
                {
                    continue;
                }

                NidekARK1 obj = new NidekARK1();
                obj.SourceFile = file;

                list.Add(obj);
            }

            // 日時降順に並べ替える
            list.Sort((x, y) =>
                {
                    int i = 0;

                    i = y.SaveDate.CompareTo(x.SaveDate);

                    if (i == 0)
                    {
                        i = y.SaveTime.CompareTo(x.SaveTime);
                    }

                    return i;
                }
            );

            return list;
        }

        /// <summary>
        /// 最新のデータファイルをテキスト変換して保存する。
        /// </summary>
        /// <returns></returns>
        public static string ConvertSaveLast()
        {
            string s = "";

            List<NidekARK1> list = NidekARK1.GetList();

            // 最新ファイルのデータを取得する
            foreach (NidekARK1 obj in list)
            {
                s = obj.ConvertData;

                if (s.Length > 0)
                {
                    break;
                }
            }

            // データが存在すればファイル保存
            if (s.Length > 0)
            {
                FileSave(s);
            }

            return s;
        }

        /// <summary>
        /// 指定されたデータファイルをテキスト変換する。
        /// </summary>
        /// <param name="file">元データファイル（XML）</param>
        /// <returns></returns>
        public static string Convert(string file)
        {
            string s = "";

            if (!File.Exists(file))
            {
                return s;
            }

            if (!Path.GetExtension(file).Equals(".xml"))
            {
                return s;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            try
            {
                s += doc.SelectSingleNode("Data/Company").InnerText + " " + doc.SelectSingleNode("Data/ModelName").InnerText + Environment.NewLine;
                s += doc.SelectSingleNode("Data/Date").InnerText + " " + doc.SelectSingleNode("Data/Time").InnerText + Environment.NewLine;
                s += "VD = " + doc.SelectSingleNode("Data/VD").InnerText + Environment.NewLine + Environment.NewLine;

                // 右眼
                s += "<R>\r\n";
                ConvertEye(doc, "Data/R/", ref s);

                s += "\r\n\r\n";

                // 左眼
                s += "<L>\r\n";
                ConvertEye(doc, "Data/L/", ref s);

                // 最終データ
                if (doc.SelectSingleNode("Data/PD/PDList/FarPD") != null)
                {
                    s += "\r\n\r\n";
                    s += "PD  ";
                    s += doc.SelectSingleNode("Data/PD/PDList/FarPD").InnerText.PadLeft(5, ' ');
                }

                return s;
            }
            catch (Exception ex)
            {
                s += Environment.NewLine;
                s += ex.Message;

                return s;
            }
        }

        /// <summary>
        /// 片眼分のデータをテキスト変換して s に追加する。
        /// （途中で例外になった場合も、それまでの出力を残すため ref で受け取る）
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="root">"Data/R/" または "Data/L/"</param>
        /// <param name="s"></param>
        static void ConvertEye(XmlDocument doc, string root, ref string s)
        {
            XmlNode xe;
            string ss = "";

            foreach (XmlNode xnn in doc.SelectNodes(root + "AR/ARList"))
            {
                ss += "\r\n";
                ss += Text(xnn, "Sphere").PadRight(7, ' ');
                ss += Text(xnn, "Cylinder").PadRight(7, ' ');
                ss += Text(xnn, "Axis").PadRight(4, ' ');
                ss += Text(xnn, "ConfidenceIndex");
            }

            xe = doc.SelectSingleNode(root + "AR/ARMedian");

            if (xe != null)
            {
                ss += "\r\n";
                ss += "Avg\r\n";
                ss += Text(xe, "Sphere").PadRight(7, ' ');
                ss += Text(xe, "Cylinder").PadRight(7, ' ');
                ss += Text(xe, "Axis").PadRight(4, ' ');
            }

            xe = doc.SelectSingleNode(root + "AR/ARPeriData");

            if (xe != null)
            {
                ss += "\r\n";
                ss += "L.DATA\r\n";
                ss += Text(xe, "Sphere").PadRight(7, ' ');
                ss += Text(xe, "Cylinder").PadRight(7, ' ');
                ss += Text(xe, "Axis").PadRight(4, ' ');
            }

            if (ss.Length > 0)
            {
                s += "S      C      A";
                s += ss;
                s += "\r\n";
            }

            xe = doc.SelectSingleNode(root + "PS/PSList/Size");

            if (xe != null)
            {
                s += "\r\n";
                s += "PS  ";
                s += xe.InnerText.PadLeft(5, ' ');
            }

            xe = doc.SelectSingleNode(root + "AC2");

            if (xe != null)
            {
                s += "\r\n\r\n";
                s += "負荷調節       AGE\r\n";
                s += "         AVG   2SD   BLK%\r\n";

                s += "T1Ref ";
                s += Text(xe, "T1Average").PadLeft(7, ' ');
                s += Text(xe, "T12SD").PadLeft(6, ' ');
                s += Text(xe, "T1BlinkRate").PadLeft(5, ' ');
                s += "\r\n";

                s += "L1Lag ";
                s += Text(xe, "L1Average").PadLeft(7, ' ');
                s += Text(xe, "L12SD").PadLeft(6, ' ');
                s += Text(xe, "L1BlinkRate").PadLeft(5, ' ');
                s += "\r\n";

                s += "Amp. ";
                s += Text(xe, "AMP").PadLeft(8, ' ');
                s += "  FarP ";
                s += Text(xe, "FarPoint").PadLeft(6, ' ');
                s += "\r\n";

                s += "(PS MIN ";
                s += Text(xe, "MinPS2").PadLeft(5, ' ');
                s += "  MAX ";
                s += Text(xe, "MaxPS2").PadLeft(5, ' ');
                s += ")";
            }

            s += "\r\n";

            ss = "";

            xe = doc.SelectSingleNode(root + "KM/KMMedian/R1");

            if (xe != null)
            {
                ss += "\r\n";
                ss += "R1 ";
                ss += Text(xe, "Radius").PadLeft(7, ' ');
                ss += Text(xe, "Power").PadLeft(7, ' ');
                ss += Text(xe, "Axis").PadLeft(5, ' ');
            }

            xe = doc.SelectSingleNode(root + "KM/KMMedian/R2");

            if (xe != null)
            {
                ss += "\r\n";
                ss += "R2 ";
                ss += Text(xe, "Radius").PadLeft(7, ' ');
                ss += Text(xe, "Power").PadLeft(7, ' ');
                ss += Text(xe, "Axis").PadLeft(5, ' ');
            }

            xe = doc.SelectSingleNode(root + "KM/KMMedian/Average");

            if (xe != null)
            {
                ss += "\r\n";
                ss += "Avg";
                ss += Text(xe, "Radius").PadLeft(7, ' ');
                ss += Text(xe, "Power").PadLeft(7, ' ');
            }

            xe = doc.SelectSingleNode(root + "KM/KMMedian/KMCylinder");

            if (xe != null)
            {
                ss += "\r\n";
                ss += "CYL       ";
                ss += Text(xe, "Power").PadLeft(7, ' ');
                ss += Text(xe, "Axis").PadLeft(5, ' ');
            }

            if (ss.Length > 0)
            {
                s += "\r\n";
                s += "      mm    D      deg";
                s += ss;
                s += "\r\n";
            }

            xe = doc.SelectSingleNode(root + "CS/CSList/Size");

            if (xe != null)
            {
                s += "\r\n";
                s += "CS  ";
                s += xe.InnerText.PadLeft(5, ' ');
            }
        }

        /// <summary>
        /// 子要素のテキストを取得する
        /// </summary>
        static string Text(XmlNode node, string name)
        {
            return node.SelectSingleNode(name).InnerText;
        }

        /// <summary>
        /// ファイル保存
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool FileSave(string s)
        {
            bool b = false;

            if (s.Length == 0)
            {
                return b;
            }

            try
            {
                // 既にあれば何もしない
                Directory.CreateDirectory(AppFile.PathName(Settings.TargetFile));

                StreamWriter writer = new StreamWriter(new FileStream(Settings.TargetFile, FileMode.Create), Encoding.Default);
                writer.Write(s);
                writer.Close();
                b = true;

                return b;
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex);
                return b;
            }
        }
    }
}
