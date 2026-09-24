using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace MedicalLibrary.Utility
{
    /// <summary>
    /// 設定情報
    /// Setting.xml に保存されている情報
    /// </summary>
    public class LibSettings
    {
        /// <summary>
        /// 現在の設定情報オブジェクト
        /// </summary>
        public static LibSettings Current = new LibSettings();

        /// <summary>
        /// DB情報
        /// </summary>
        public string DBConnectionString1 = "User Id=macs;Password=system;Data Source=wgs_odbc_orcl;";

        /// <summary>
        /// DB情報
        /// </summary>
        public string DBConnectionString2 = "User Id=open;Password=system;Data Source=macs_open;";
        /// <summary>
        /// DB情報
        /// </summary>
        public string DBConnectionString3 = "User Id=medb;Password=system;Data Source=inno_orcl;";

        /// <summary>
        /// ログ出力レベル
        /// </summary>
        public int LogLevel = 1;

        /// <summary>
        /// 患者基本情報の項目コードと名称のセット
        /// </summary>
        [XmlElement(ElementName = "BaseInfoCodes")]
        public BaseInfoCodes BaseInfoCodes = new BaseInfoCodes();

        /// <summary>
        /// アプリケーション設定ファイルの読み込み
        /// </summary>
        /// <param name="xml_file"></param>
        private static void Read(string xml_file)
        {
            if (File.Exists(xml_file))
            {
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(xml_file);

                    // いったんクリアする
                    Current = new LibSettings();

                    Current.DBConnectionString1 = ReadText(xmlDoc, "LibSettings/DBConnectionString1", Current.DBConnectionString1);
                    Current.DBConnectionString2 = ReadText(xmlDoc, "LibSettings/DBConnectionString2", Current.DBConnectionString2);
                    Current.DBConnectionString3 = ReadText(xmlDoc, "LibSettings/DBConnectionString3", Current.DBConnectionString3);

                    // 患者基本情報コード
                    foreach (XmlNode n in xmlDoc.SelectNodes("LibSettings/BaseInfoCodes/BaseInfoCode"))
                    {
                        BaseInfoCode obj = new BaseInfoCode();

                        obj.Code = n.Attributes["Code"].Value;
                        obj.Name = ReadText(n, "Name", obj.Name);

                        Current.BaseInfoCodes.BaseInfoCodeList.Add(obj);
                    }

                    // エラーチェック
                    //  →　今後は必要ないと思われるので外す 2019/05/09
                    /*
                    if (!Directory.Exists(Current.LogFolderPath))
                    {
                        Directory.CreateDirectory(Current.LogFolderPath);
                    }

                    Current.LogServerFolderPath = Current.LogServerFolderPath + "\\" + AppStat.IP4;

                    if (!Directory.Exists(Current.LogServerFolderPath))
                    {
                        Directory.CreateDirectory(Current.LogServerFolderPath);
                    }
                     */
                }
                catch (Exception ex)
                {
                    LibUtility.Except(ex, false);
                }
            }
        }

        /// <summary>
        /// xpath の要素のテキストを取得する。要素が無ければ default_value を返す。
        /// </summary>
        static string ReadText(XmlNode node, string xpath, string default_value)
        {
            XmlNode n = node.SelectSingleNode(xpath);

            return n != null ? n.InnerText : default_value;
        }

        static bool init = false;

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="forced">強制的に初期化する</param>
        public static void Init(bool forced = false)
        {
            if (forced || !init)
            {
                string msg = "";

                string xml_file = AppFile.FilePath("MedicalLibrary_Settings.xml");

                if (File.Exists(xml_file))
                {
                    LibSettings.Read(xml_file);

                    // DBアクセスの初期化
                    DB.Db2.Init(LibSettings.Current.DBConnectionString2);
                    DB.Db3.Init(LibSettings.Current.DBConnectionString3);
                }
                else
                {
                    msg += "設定ファイル MedicalLibrary_Settings.xml が存在しません" + Environment.NewLine;
                }

                if (msg.Length > 0)
                {
                    throw new Exception(msg);
                }

                init = true;
            }
        }
    }

    public class BaseInfoCodes
    {
        [XmlElement(ElementName = "BaseInfoCode")]
        public List<BaseInfoCode> BaseInfoCodeList = new List<BaseInfoCode>();

        /// <summary>
        /// 既往歴
        /// </summary>
        public string Diag { get { return CodeByName("Diag"); } }

        /// <summary>
        /// アレルギー
        /// </summary>
        public string Allergy { get { return CodeByName("Allergy"); } }

        /// <summary>
        /// 内服・外用
        /// </summary>
        public string Drug { get { return CodeByName("Drug"); } }

        /// <summary>
        /// 身長（短期入院）
        /// </summary>
        public string Height { get { return CodeByName("Height"); } }

        /// <summary>
        /// 体重（短期入院）
        /// </summary>
        public string Weight { get { return CodeByName("Weight"); } }

        /// <summary>
        /// 名称に対応する項目コードを返す。該当が無ければ空文字。
        /// </summary>
        string CodeByName(string name)
        {
            foreach (BaseInfoCode bc in this.BaseInfoCodeList)
            {
                if (bc.Name.Equals(name))
                {
                    return bc.Code;
                }
            }

            return "";
        }
    }

    public class BaseInfoCode
    {
        [XmlAttribute(AttributeName = "Code")]
        public string Code = "";

        [XmlElement(ElementName = "Name")]
        public string Name = "";
    }
}
