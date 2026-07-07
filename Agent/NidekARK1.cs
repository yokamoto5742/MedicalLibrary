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

                if (Path.GetExtension(f).Equals(".xml"))
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

                if (Path.GetExtension(f).Equals(".xml"))
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
            string ss = "";

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

            XmlElement xe;

            try
            {
                s += doc.SelectSingleNode("Data/Company").InnerText + " " + doc.SelectSingleNode("Data/ModelName").InnerText + Environment.NewLine;
                s += doc.SelectSingleNode("Data/Date").InnerText + " " + doc.SelectSingleNode("Data/Time").InnerText + Environment.NewLine;
                s += "VD = " + doc.SelectSingleNode("Data/VD").InnerText + Environment.NewLine + Environment.NewLine;

                // 右眼
                s += "<R>\r\n";

                ss = "";

                if (doc.SelectNodes("Data/R/AR/ARList") != null)
                {
                    foreach (XmlNode xnn in doc.SelectNodes("Data/R/AR/ARList"))
                    {
                        xe = (XmlElement)xnn;
                        ss += "\r\n";
                        ss += xe.SelectSingleNode("Sphere").InnerText.PadRight(7, ' ');
                        ss += xe.SelectSingleNode("Cylinder").InnerText.PadRight(7, ' ');
                        ss += xe.SelectSingleNode("Axis").InnerText.PadRight(4, ' ');
                        ss += xe.SelectSingleNode("ConfidenceIndex").InnerText;
                    }
                }

                if (doc.SelectSingleNode("Data/R/AR/ARMedian") != null)
                {
                    xe = (XmlElement)doc.SelectSingleNode("Data/R/AR/ARMedian");
                    ss += "\r\n";
                    ss += "Avg\r\n";
                    ss += xe.SelectSingleNode("Sphere").InnerText.PadRight(7, ' ');
                    ss += xe.SelectSingleNode("Cylinder").InnerText.PadRight(7, ' ');
                    ss += xe.SelectSingleNode("Axis").InnerText.PadRight(4, ' ');
                }

                if (doc.SelectSingleNode("Data/R/AR/ARPeriData") != null)
                {
                    xe = (XmlElement)doc.SelectSingleNode("Data/R/AR/ARPeriData");
                    ss += "\r\n";
                    ss += "L.DATA\r\n";
                    ss += xe.SelectSingleNode("Sphere").InnerText.PadRight(7, ' ');
                    ss += xe.SelectSingleNode("Cylinder").InnerText.PadRight(7, ' ');
                    ss += xe.SelectSingleNode("Axis").InnerText.PadRight(4, ' ');
                }

                if (ss.Length > 0)
                {
                    s += "S      C      A";
                    s += ss;
                    s += "\r\n";
                }

                if (doc.SelectSingleNode("Data/R/PS/PSList/Size") != null)
                {
                    s += "\r\n";
                    s += "PS  ";
                    s += doc.SelectSingleNode("Data/R/PS/PSList/Size").InnerText.PadLeft(5, ' ');
                }

                if (doc.SelectSingleNode("Data/R/AC2") != null)
                {
                    xe = (XmlElement)doc.SelectSingleNode("Data/R/AC2");

                    s += "\r\n\r\n";
                    s += "負荷調節       AGE\r\n";
                    s += "         AVG   2SD   BLK%\r\n";

                    s += "T1Ref ";
                    s += xe.SelectSingleNode("T1Average").InnerText.PadLeft(7, ' ');
                    s += xe.SelectSingleNode("T12SD").InnerText.PadLeft(6, ' ');
                    s += xe.SelectSingleNode("T1BlinkRate").InnerText.PadLeft(5, ' ');
                    s += "\r\n";

                    s += "L1Lag ";
                    s += xe.SelectSingleNode("L1Average").InnerText.PadLeft(7, ' ');
                    s += xe.SelectSingleNode("L12SD").InnerText.PadLeft(6, ' ');
                    s += xe.SelectSingleNode("L1BlinkRate").InnerText.PadLeft(5, ' ');
                    s += "\r\n";

                    s += "Amp. ";
                    s += xe.SelectSingleNode("AMP").InnerText.PadLeft(8, ' ');
                    s += "  FarP ";
                    s += xe.SelectSingleNode("FarPoint").InnerText.PadLeft(6, ' ');
                    s += "\r\n";

                    s += "(PS MIN ";
                    s += xe.SelectSingleNode("MinPS2").InnerText.PadLeft(5, ' ');
                    s += "  MAX ";
                    s += xe.SelectSingleNode("MaxPS2").InnerText.PadLeft(5, ' ');
                    s += ")";
                }

                s += "\r\n";

                ss = "";

                if (doc.SelectSingleNode("Data/R/KM/KMMedian/R1") != null)
                {
                    xe = (XmlElement)doc.SelectSingleNode("Data/R/KM/KMMedian/R1");
                    ss += "\r\n";
                    ss += "R1 ";
                    ss += xe.SelectSingleNode("Radius").InnerText.PadLeft(7, ' ');
                    ss += xe.SelectSingleNode("Power").InnerText.PadLeft(7, ' ');
                    ss += xe.SelectSingleNode("Axis").InnerText.PadLeft(5, ' ');
                }

                if (doc.SelectSingleNode("Data/R/KM/KMMedian/R2") != null)
                {
                    xe = (XmlElement)doc.SelectSingleNode("Data/R/KM/KMMedian/R2");
                    ss += "\r\n";
                    ss += "R2 ";
                    ss += xe.SelectSingleNode("Radius").InnerText.PadLeft(7, ' ');
                    ss += xe.SelectSingleNode("Power").InnerText.PadLeft(7, ' ');
                    ss += xe.SelectSingleNode("Axis").InnerText.PadLeft(5, ' ');
                }

                if (doc.SelectSingleNode("Data/R/KM/KMMedian/Average") != null)
                {
                    xe = (XmlElement)doc.SelectSingleNode("Data/R/KM/KMMedian/Average");
                    ss += "\r\n";
                    ss += "Avg";
                    ss += xe.SelectSingleNode("Radius").InnerText.PadLeft(7, ' ');
                    ss += xe.SelectSingleNode("Power").InnerText.PadLeft(7, ' ');
                }

                if (doc.SelectSingleNode("Data/R/KM/KMMedian/KMCylinder") != null)
                {
                    xe = (XmlElement)doc.SelectSingleNode("Data/R/KM/KMMedian/KMCylinder");
                    ss += "\r\n";
                    ss += "CYL       ";
                    ss += xe.SelectSingleNode("Power").InnerText.PadLeft(7, ' ');
                    ss += xe.SelectSingleNode("Axis").InnerText.PadLeft(5, ' ');
                }

                if (ss.Length > 0)
                {
                    s += "\r\n";
                    s += "      mm    D      deg";
                    s += ss;
                    s += "\r\n";
                }

                if (doc.SelectSingleNode("Data/R/CS/CSList/Size") != null)
                {
                    s += "\r\n";
                    s += "CS  ";
                    s += doc.SelectSingleNode("Data/R/CS/CSList/Size").InnerText.PadLeft(5, ' ');
                }

                s += "\r\n\r\n";

                // 左眼
                s += "<L>\r\n";

                ss = "";

                if (doc.SelectNodes("Data/L/AR/ARList") != null)
                {
                    foreach (XmlNode xnn in doc.SelectNodes("Data/L/AR/ARList"))
                    {
                        xe = (XmlElement)xnn;
                        ss += "\r\n";
                        ss += xe.SelectSingleNode("Sphere").InnerText.PadRight(7, ' ');
                        ss += xe.SelectSingleNode("Cylinder").InnerText.PadRight(7, ' ');
                        ss += xe.SelectSingleNode("Axis").InnerText.PadRight(4, ' ');
                        ss += xe.SelectSingleNode("ConfidenceIndex").InnerText;
                    }
                }

                if (doc.SelectSingleNode("Data/L/AR/ARMedian") != null)
                {
                    xe = (XmlElement)doc.SelectSingleNode("Data/L/AR/ARMedian");
                    ss += "\r\n";
                    ss += "Avg\r\n";
                    ss += xe.SelectSingleNode("Sphere").InnerText.PadRight(7, ' ');
                    ss += xe.SelectSingleNode("Cylinder").InnerText.PadRight(7, ' ');
                    ss += xe.SelectSingleNode("Axis").InnerText.PadRight(4, ' ');
                }

                if (doc.SelectSingleNode("Data/L/AR/ARPeriData") != null)
                {
                    xe = (XmlElement)doc.SelectSingleNode("Data/L/AR/ARPeriData");
                    ss += "\r\n";
                    ss += "L.DATA\r\n";
                    ss += xe.SelectSingleNode("Sphere").InnerText.PadRight(7, ' ');
                    ss += xe.SelectSingleNode("Cylinder").InnerText.PadRight(7, ' ');
                    ss += xe.SelectSingleNode("Axis").InnerText.PadRight(4, ' ');
                }

                if (ss.Length > 0)
                {
                    s += "S      C      A";
                    s += ss;
                    s += "\r\n";
                }

                if (doc.SelectSingleNode("Data/L/PS/PSList/Size") != null)
                {
                    s += "\r\n";
                    s += "PS  ";
                    s += doc.SelectSingleNode("Data/L/PS/PSList/Size").InnerText.PadLeft(5, ' ');
                }

                if (doc.SelectSingleNode("Data/L/AC2") != null)
                {
                    xe = (XmlElement)doc.SelectSingleNode("Data/L/AC2");

                    s += "\r\n\r\n";
                    s += "負荷調節       AGE\r\n";
                    s += "         AVG   2SD   BLK%\r\n";

                    s += "T1Ref ";
                    s += xe.SelectSingleNode("T1Average").InnerText.PadLeft(7, ' ');
                    s += xe.SelectSingleNode("T12SD").InnerText.PadLeft(6, ' ');
                    s += xe.SelectSingleNode("T1BlinkRate").InnerText.PadLeft(5, ' ');
                    s += "\r\n";

                    s += "L1Lag ";
                    s += xe.SelectSingleNode("L1Average").InnerText.PadLeft(7, ' ');
                    s += xe.SelectSingleNode("L12SD").InnerText.PadLeft(6, ' ');
                    s += xe.SelectSingleNode("L1BlinkRate").InnerText.PadLeft(5, ' ');
                    s += "\r\n";

                    s += "Amp. ";
                    s += xe.SelectSingleNode("AMP").InnerText.PadLeft(8, ' ');
                    s += "  FarP ";
                    s += xe.SelectSingleNode("FarPoint").InnerText.PadLeft(6, ' ');
                    s += "\r\n";

                    s += "(PS MIN ";
                    s += xe.SelectSingleNode("MinPS2").InnerText.PadLeft(5, ' ');
                    s += "  MAX ";
                    s += xe.SelectSingleNode("MaxPS2").InnerText.PadLeft(5, ' ');
                    s += ")";
                }

                s += "\r\n";

                ss = "";

                if (doc.SelectSingleNode("Data/L/KM/KMMedian/R1") != null)
                {
                    xe = (XmlElement)doc.SelectSingleNode("Data/L/KM/KMMedian/R1");
                    ss += "\r\n";
                    ss += "R1 ";
                    ss += xe.SelectSingleNode("Radius").InnerText.PadLeft(7, ' ');
                    ss += xe.SelectSingleNode("Power").InnerText.PadLeft(7, ' ');
                    ss += xe.SelectSingleNode("Axis").InnerText.PadLeft(5, ' ');
                }

                if (doc.SelectSingleNode("Data/L/KM/KMMedian/R2") != null)
                {
                    xe = (XmlElement)doc.SelectSingleNode("Data/L/KM/KMMedian/R2");
                    ss += "\r\n";
                    ss += "R2 ";
                    ss += xe.SelectSingleNode("Radius").InnerText.PadLeft(7, ' ');
                    ss += xe.SelectSingleNode("Power").InnerText.PadLeft(7, ' ');
                    ss += xe.SelectSingleNode("Axis").InnerText.PadLeft(5, ' ');
                }

                if (doc.SelectSingleNode("Data/L/KM/KMMedian/Average") != null)
                {
                    xe = (XmlElement)doc.SelectSingleNode("Data/L/KM/KMMedian/Average");
                    ss += "\r\n";
                    ss += "Avg";
                    ss += xe.SelectSingleNode("Radius").InnerText.PadLeft(7, ' ');
                    ss += xe.SelectSingleNode("Power").InnerText.PadLeft(7, ' ');
                }

                if (doc.SelectSingleNode("Data/L/KM/KMMedian/KMCylinder") != null)
                {
                    xe = (XmlElement)doc.SelectSingleNode("Data/L/KM/KMMedian/KMCylinder");
                    ss += "\r\n";
                    ss += "CYL       ";
                    ss += xe.SelectSingleNode("Power").InnerText.PadLeft(7, ' ');
                    ss += xe.SelectSingleNode("Axis").InnerText.PadLeft(5, ' ');
                }

                if (ss.Length > 0)
                {
                    s += "\r\n";
                    s += "      mm    D      deg";
                    s += ss;
                    s += "\r\n";
                }

                if (doc.SelectSingleNode("Data/L/CS/CSList/Size") != null)
                {
                    s += "\r\n";
                    s += "CS  ";
                    s += doc.SelectSingleNode("Data/L/CS/CSList/Size").InnerText.PadLeft(5, ' ');
                }

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
            finally
            {
            }
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
                if (!Directory.Exists(AppFile.PathName(@"c:\transfile\data")))
//                if (!Directory.Exists(AppFile.PathName(Settings.TargetFile)))
                {
                    Directory.CreateDirectory(AppFile.PathName(Settings.TargetFile));
                }

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
