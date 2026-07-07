using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class ComeReportSettings
    {
        /// <summary>
        /// 現在のアプリの設定。アプリ起動時に Init する。
        /// </summary>
        public static ComeReportSettings Current = new ComeReportSettings();

        DataSet ComeReportSet;

        public string MyGroup = "0";
        public string MyDoctor = "";
        public List<string> MyKensaList = new List<string>();
        public List<string> MyPartList = new List<string>();
        public List<string> MyTabList = new List<string>();
        public string MyShowDays = "1";

        public class KensaGroup
        {
            public string Name = "";
            public string Kensa = "";
            public string Part = "";
        }

        public class Kensa
        {
            public string Code = "";
            public string Name = "";
            public List<string> TabList = new List<string>();
            public List<PrintSet> PrintSetList = new List<PrintSet>();
        }

        /// <summary>
        /// オーダーディテールから所見に取り込む情報。
        /// ComeReport.xml 上で検査種別ごとに定義されている。
        /// </summary>
        public class PrintSet
        {
            public string Word = "";
            public string Unit = "";
            public List<PrintElement> ElemList;

            public PrintSet()
            {
                this.Word = "";
                this.Unit = "";
                this.ElemList = new List<PrintElement>();
            }

            public PrintSet(string word)
            {
                this.Word = word;
                this.Unit = "";
                this.ElemList = new List<PrintElement>();
            }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="word">印字名称（診断、目的、部位 など）</param>
            /// <param name="unit">印字単位（cm など）</param>
            /// <param name="orgElement">ComeReport.xml 上のオリジナル文字列</param>
            public PrintSet(string word, string unit, string orgElement)
            {
                this.Word = word;
                this.Unit = unit;
                this.ElemList = new List<PrintElement>();

                string[] s = (orgElement.Trim('\r') + "|").Split('|');

                foreach (string ss in s)
                {
                    if (ss.Length > 0 && ss.Contains(","))
                    {
                        PrintElement tmpElem = new PrintElement(ss.Split(',')[0], ss.Split(',')[1]);
                        this.ElemList.Add(tmpElem);
                    }
                }
            }
        }

        /// <summary>
        /// オーダーディテールから所見に取り込む情報の各構成要素。
        /// オーダーマスターコードとその種類によって構成される。
        /// </summary>
        public class PrintElement
        {
            public string Code = "";
            public string Kind = "";

            public PrintElement(string code, string kind)
            {
                this.Code = code;
                this.Kind = kind;
            }
        }

        public Dictionary<string, Kensa> KensaDict = new Dictionary<string, Kensa>();
        public Dictionary<string, KensaGroup> GroupDict = new Dictionary<string, KensaGroup>();
        public Dictionary<string, string> StatusDict = new Dictionary<string, string>();

        public class SchemaBg
        {
            public string Id = "";
            public string Tab = "";
            public string Path = "";
            public string Status = "";
        }

        public Dictionary<string, SchemaBg> SchemaBgDict = new Dictionary<string, SchemaBg>();

        /// <summary>
        /// 院外読影の有り得る検査のリスト
        /// </summary>
        public List<string> OutSideList = new List<string>();

        public string Manual = "";

        public string ImgHostPath = "";

        public string ImgHostVisible = "";

        /// <summary>
        /// 読影実績
        /// </summary>
        public List<string> AchieveKensaList = new List<string>();
        public List<string> AchieveStaffList = new List<string>();
        public List<string> AchieveMailList = new List<string>();

        /// <summary>
        /// カルテメッセージ
        /// </summary>
        public List<string> MessageStaffList = new List<string>();
        public string MessageSubject = "";
        public string MessageBody = "";

        public class PDFMaster
        {
            public string PrinterName = "";
            public string BeforeDays = "";
            public string From = "";
            public string Title = "";
            public string PreMsg = "";
        }

        public PDFMaster PDFMaster1 = new PDFMaster();

        public class PDFTitle
        {
            public string Code = "";
            public string File = "";
            public string Title = "";
        }

        /// <summary>
        ///  PDF文書
        /// </summary>
        public Dictionary<string, PDFTitle> PDFDict = new Dictionary<string, PDFTitle>();

        /// <summary>
        /// 初期化。アプリ起動時に実行する。
        /// </summary>
        public static void Init()
        {
            Current.Read("ComeReport.xml");
        }

        /// <summary>
        /// 初期化。アプリ起動時に実行する。
        /// </summary>
        public static void InitUser(string user_id, string doctor_id, string qual_id)
        {
            Current.ReadUser("ComeReport.xml", user_id, doctor_id, qual_id);
        }

        /// <summary>
        /// 初期設定ファイルの読み込み。
        /// </summary>
        /// <param name="file"></param>
        private void ReadUser(string file, string user_id, string doctor_id, string qual_id)
        {
            ComeReportSet = new DataSet();
            ComeReportSet.ReadXml(new System.IO.StreamReader(AppFile.FilePath(file), Encoding.GetEncoding("shift-jis")));

            // Manual
            DataTable tmpTable = ComeReportSet.Tables["Manual"];

            // Qual 関連情報の初期設定
            tmpTable = ComeReportSet.Tables["Qual"];

            foreach (DataRow tmpRow in tmpTable.Rows)
            {
                if (tmpRow["Code"].ToString().Trim() == qual_id)
                {
                    MyGroup = tmpRow["Group"].ToString();

                    if (tmpRow["Doctor"].ToString().Equals("[Self]") || tmpRow["Doctor"].ToString().Equals("[self]"))
                    {
                        if (doctor_id.Length > 0)
                        {
                            MyDoctor = doctor_id;
                        }
                    }
                    else
                    {
                        MyDoctor = tmpRow["Doctor"].ToString();
                    }

                    string[] s = (tmpRow["Kensa"].ToString() + ",").Split(',');

                    foreach (string ss in s)
                    {
                        if (ss.Length > 0 && !MyKensaList.Contains(ss))
                        {
                            MyKensaList.Add(ss);
                        }
                    }

                    s = (tmpRow["Part"].ToString() + ",").Split(',');

                    foreach (string ss in s)
                    {
                        if (ss.Length > 0 && !MyPartList.Contains(ss))
                        {
                            MyPartList.Add(ss);
                        }
                    }

                    s = (tmpRow["Tab"].ToString() + ",").Split(',');

                    foreach (string ss in s)
                    {
                        if (ss.Length > 0 && !MyTabList.Contains(ss))
                        {
                            MyTabList.Add(ss);
                        }
                    }

                    if (tmpRow["ShowDays"].ToString().Length > 0)
                    {
                        MyShowDays = tmpRow["ShowDays"].ToString();
                    }

                    break;
                }
            }

            // Staff 関連情報の初期設定
            tmpTable = ComeReportSet.Tables["Staff"];

            foreach (DataRow tmpRow in tmpTable.Rows)
            {
                if (tmpRow["Code"].ToString().Trim() == user_id)
                {
                    MyGroup = tmpRow["Group"].ToString();

                    if (tmpRow["Doctor"].ToString().Equals("[Self]", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (doctor_id.Length > 0)
                        {
                            MyDoctor = doctor_id;
                        }
                    }
                    else
                    {
                        MyDoctor = tmpRow["Doctor"].ToString();
                    }

                    string[] s = (tmpRow["Kensa"].ToString() + ",").Split(',');

                    foreach (string ss in s)
                    {
                        if (ss.Length > 0 && !MyKensaList.Contains(ss))
                        {
                            MyKensaList.Add(ss);
                        }
                    }

                    s = (tmpRow["Part"].ToString() + ",").Split(',');

                    foreach (string ss in s)
                    {
                        if (ss.Length > 0 && !MyPartList.Contains(ss))
                        {
                            MyPartList.Add(ss);
                        }
                    }

                    s = (tmpRow["Tab"].ToString() + ",").Split(',');

                    foreach (string ss in s)
                    {
                        if (ss.Length > 0 && !MyTabList.Contains(ss))
                        {
                            MyTabList.Add(ss);
                        }
                    }

                    if (tmpRow["ShowDays"].ToString().Length > 0)
                    {
                        MyShowDays = tmpRow["ShowDays"].ToString();
                    }

                    break;
                }
            }
        }

        /// <summary>
        /// 初期設定ファイルの読み込み。
        /// </summary>
        /// <param name="file"></param>
        private void Read(string file)
        {
            ComeReportSet = new DataSet();
            ComeReportSet.ReadXml(new System.IO.StreamReader(AppFile.FilePath(file), Encoding.GetEncoding("shift-jis")));

            // Manual
            DataTable tmpTable = ComeReportSet.Tables["Manual"];

            foreach (DataRow tmpRow in tmpTable.Rows)
            {
                this.Manual = tmpRow["Path"].ToString();
                break;
            }

            // ImgHost
            tmpTable = ComeReportSet.Tables["ImgHost"];

            foreach (DataRow tmpRow in tmpTable.Rows)
            {
                this.ImgHostPath = tmpRow["Path"].ToString().TrimEnd('\\');
                this.ImgHostVisible = tmpRow["Visible"].ToString();
                break;
            }

            // Achieve
            tmpTable = ComeReportSet.Tables["Achieve"];

            foreach (DataRow tmpRow in tmpTable.Rows)
            {
                foreach (string s in tmpRow["Kensa"].ToString().Split(','))
                {
                    this.AchieveKensaList.Add(s);
                }

                foreach (string s in tmpRow["Staff"].ToString().Split(','))
                {
                    this.AchieveStaffList.Add(s);
                }
/*
                foreach (string s in tmpRow["Mail"].ToString().Split(','))
                {
                    this.AchieveMailList.Add(s);
                }
*/
                break;
            }

            // Message
            if (ComeReportSet.Tables.Contains("Message"))
            {
                tmpTable = ComeReportSet.Tables["Message"];

                foreach (DataRow tmpRow in tmpTable.Rows)
                {
                    foreach (string s in tmpRow["Staff"].ToString().Split(','))
                    {
                        this.MessageStaffList.Add(s);
                    }

                    this.MessageSubject = tmpRow["Subject"].ToString();
                    this.MessageBody = tmpRow["Body"].ToString();

                    break;
                }
            }

            // GroupDict の初期設定
            tmpTable = ComeReportSet.Tables["Group"];

            foreach (DataRow tmpRow in tmpTable.Rows)
            {
                if (!GroupDict.ContainsKey(tmpRow["ID"].ToString()))
                {
                    KensaGroup tmpGroup = new KensaGroup();
                    tmpGroup.Name = tmpRow["Name"].ToString();
                    tmpGroup.Kensa = tmpRow["Kensa"].ToString();
                    tmpGroup.Part = tmpRow["Part"].ToString();

                    GroupDict.Add(tmpRow["ID"].ToString(), tmpGroup);
                }
            }

            // KensaDict の初期設定
            tmpTable = ComeReportSet.Tables["Kensa"];

            KensaDict.Add("0", new Kensa());

            foreach (DataRow tmpRow in tmpTable.Rows)
            {
                if (!KensaDict.ContainsKey(tmpRow["Code"].ToString()))
                {
                    Kensa tmpKensa = new Kensa();
                    tmpKensa.Code = tmpRow["Code"].ToString();
                    tmpKensa.Name = tmpRow["Name"].ToString();

                    string[] s1 = (tmpRow["Tab"].ToString() + "|").Split('|');

                    foreach (string ss in s1)
                    {
                        if (ss.Length > 0)
                        {
                            tmpKensa.TabList.Add(ss);
                        }
                    }

                    string[] s2 = (tmpRow["Print"].ToString() + "\n").Split('\n');

                    foreach (string ss in s2)
                    {
                        if (ss.Trim('\r').Length > 0 && ss.Contains("="))
                        {
                            tmpKensa.PrintSetList.Add(new PrintSet(ss.Split('=')[0], ss.Split('=')[2], ss.Split('=')[1]));
                        }
                    }

                    KensaDict.Add(tmpRow["Code"].ToString(), tmpKensa);
                }
            }

            // PC 関連情報の初期設定
            tmpTable = ComeReportSet.Tables["PC"];

            string pcName = System.Environment.GetEnvironmentVariable("COMPUTERNAME");

            foreach (DataRow tmpRow in tmpTable.Rows)
            {
                if (tmpRow["Name"].ToString().Trim() == pcName)
                {
                    MyGroup = tmpRow["Group"].ToString();

                    if (tmpRow["ShowDays"].ToString().Length > 0)
                    {
                        MyShowDays = tmpRow["ShowDays"].ToString();
                    }

                    break;
                }
            }

            // StatusDict の初期設定
            StatusDict.Add("0", "");
            StatusDict.Add("1", "○");

            // SchemaBgDict の初期設定
            tmpTable = ComeReportSet.Tables["Schema"];

            SchemaBgDict.Add("0", new SchemaBg());

            foreach (DataRow tmpRow in tmpTable.Rows)
            {
                if (!SchemaBgDict.ContainsKey(tmpRow["ID"].ToString()))
                {
                    string path = AppFile.FilePath(tmpRow["Path"].ToString());

                    if (path.Length > 0)
                    {
                        SchemaBg tmpBg = new SchemaBg();
                        tmpBg.Id = tmpRow["ID"].ToString();
                        tmpBg.Tab = tmpRow["Tab"].ToString();
                        tmpBg.Path = path;
                        tmpBg.Status = tmpRow["Status"].ToString();

                        SchemaBgDict.Add(tmpBg.Id, tmpBg);
                    }
                }
            }

            // 院外読影の有り得る検査の初期設定
            string[] os = ComeReportSet.Tables["OutSide"].Rows[0]["Kensa"].ToString().Split(',');

            foreach (string ss in os)
            {
                OutSideList.Add(ss);
            }

            // PDFDoc
            tmpTable = ComeReportSet.Tables["PDFDoc"];

            foreach (DataRow r in tmpTable.Rows)
            {
                PDFTitle tmpPDF = new PDFTitle();
                tmpPDF.Code = r["Code"].ToString();
                tmpPDF.File = r["File"].ToString();
                tmpPDF.Title = r["Title"].ToString();

                PDFDict.Add(r["Code"].ToString(), tmpPDF);
            }

            // PDFMaster
            tmpTable = ComeReportSet.Tables["PDFMaster"];

            foreach (DataRow tmpRow in tmpTable.Rows)
            {
                this.PDFMaster1.PrinterName = tmpRow["PrinterName"].ToString();
                this.PDFMaster1.BeforeDays = tmpRow["BeforeDays"].ToString();
                this.PDFMaster1.From = tmpRow["From"].ToString();
                this.PDFMaster1.Title = tmpRow["Title"].ToString();
                this.PDFMaster1.PreMsg = tmpRow["PreMsg"].ToString();
                break;
            }
        }
    }
}
