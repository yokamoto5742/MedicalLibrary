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
        /// オーダー転送アプリのパス
        /// </summary>
        public string OrderXmlExe = @"c:\shinseikai\ProasAgent.exe";

        /// <summary>
        /// 医事会計システムのパス
        /// </summary>
        public string ReceExe = @"c:\医事会計ｼｽﾃﾑ（クライアントアプリ2）\apx.exe";

        /// <summary>
        /// 医事会計APIアプリのパス
        /// </summary>
        public string ReceApiExe = @"c:\shinseikai\ProasKaikeiApi.exe";

        /// <summary>
        /// オーダー転送してから医事会計を起動するまでの間隔（秒）
        /// </summary>
        public string OrderReceApiInterval = "4";

        /// <summary>
        /// シェーマのフォルダ
        /// </summary>
        public string SchemaFolder = @"c:\shinseikai\Schema";

        /// <summary>
        /// SOAP画像フォルダ
        /// </summary>
        public string SoapImageFolder = @"\\lily\soapimg$";

        /// <summary>
        /// SOAP画像テンポラリフォルダ
        /// </summary>
        public string SoapImageTemporaryFolder = @"c:\shinseikai\MedicalLibrary\soapimg";

        /// <summary>
        /// PDFサーバーフォルダ
        /// </summary>
        public string PdfServerFolder = @"\\pdfkartenas2\pdfserver$";

        /// <summary>
        /// PDF送信フォルダ
        /// </summary>
        public string PdfSendFolder = @"\\lily\TmpPdf";

        /// <summary>
        /// 麻酔データフォルダ
        /// </summary>
        public string AnesDataFolder = @"\\lily\anesdat";

        /// <summary>
        /// 麻酔記録フォルダ
        /// </summary>
        public string AnesImageFolder = @"\\lily\anesimg";

        /// <summary>
        /// 請求書PDFフォルダ
        /// </summary>
        public string BillPdfFolder = @"\\172.17.10.20\tmp\請求書PDF\output\bill";

        /// <summary>
        /// 明細書PDFフォルダ
        /// </summary>
        public string InvoicePdfFolder = @"\\172.17.10.20\tmp\請求書PDF\output\invoice";

        /// <summary>
        /// DPC調整係数
        /// </summary>
        public string DPCValue = "";

        /// <summary>
        /// ログフォルダ
        /// </summary>
        public string LogFolderPath = "";

        /// <summary>
        /// ログサーバーフォルダ
        /// </summary>
        public string LogServerFolderPath = "";

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
        /// DPCメッセージ1
        /// DPC病名がついていない人
        /// </summary>
        public DPCMsg1 DPCMsg1 = new DPCMsg1();

        /// <summary>
        /// DPCメッセージ2
        /// DPC病名が更新された人
        /// </summary>
        public List<DPCMsg2> DPCMsgList2 = new List<DPCMsg2>();

        /// <summary>
        /// DPCメッセージ3
        /// 糖尿病の入院病名がついている人
        /// </summary>
        public DPCMsg3 DPCMsg3 = new DPCMsg3();


        /// <summary>
        /// PCごとの設定
        /// </summary>
        [XmlElement(ElementName = "PCS")]
        public PCS PCS = new PCS();

        /// <summary>
        /// プロアス設定
        /// </summary>
        [XmlElement(ElementName = "Proas")]
        public Proas Proas = new Proas();

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

                    if (xmlDoc.SelectSingleNode("LibSettings/DBConnectionString1") != null)
                    {
                        Current.DBConnectionString1 = xmlDoc.SelectSingleNode("LibSettings/DBConnectionString1").InnerText;
                    }

                    if (xmlDoc.SelectSingleNode("LibSettings/DBConnectionString2") != null)
                    {
                        Current.DBConnectionString2 = xmlDoc.SelectSingleNode("LibSettings/DBConnectionString2").InnerText;
                    }

                    if (xmlDoc.SelectSingleNode("LibSettings/DBConnectionString3") != null)
                    {
                        Current.DBConnectionString3 = xmlDoc.SelectSingleNode("LibSettings/DBConnectionString3").InnerText;
                    }

                    if (xmlDoc.SelectSingleNode("LibSettings/OrderXmlExe") != null)
                    {
                        Current.OrderXmlExe = xmlDoc.SelectSingleNode("LibSettings/OrderXmlExe").InnerText;
                    }

                    if (xmlDoc.SelectSingleNode("LibSettings/ReceExe") != null)
                    {
                        Current.ReceExe = xmlDoc.SelectSingleNode("LibSettings/ReceExe").InnerText;
                    }

                    if (xmlDoc.SelectSingleNode("LibSettings/ReceApiExe") != null)
                    {
                        Current.ReceApiExe = xmlDoc.SelectSingleNode("LibSettings/ReceApiExe").InnerText;
                    }

                    if (xmlDoc.SelectSingleNode("LibSettings/OrderReceApiInterval") != null)
                    {
                        Current.OrderReceApiInterval = xmlDoc.SelectSingleNode("LibSettings/OrderReceApiInterval").InnerText;
                    }

                    if (xmlDoc.SelectSingleNode("LibSettings/SchemaFolder") != null)
                    {
                        Current.SchemaFolder = xmlDoc.SelectSingleNode("LibSettings/SchemaFolder").InnerText;
                    }

                    if (xmlDoc.SelectSingleNode("LibSettings/SoapImageFolder") != null)
                    {
                        Current.SoapImageFolder = xmlDoc.SelectSingleNode("LibSettings/SoapImageFolder").InnerText;
                    }

                    if (xmlDoc.SelectSingleNode("LibSettings/SoapImageTemporaryFolder") != null)
                    {
                        Current.SoapImageTemporaryFolder = xmlDoc.SelectSingleNode("LibSettings/SoapImageTemporaryFolder").InnerText;
                    }

                    if (xmlDoc.SelectSingleNode("LibSettings/PdfServerFolder") != null)
                    {
                        Current.PdfServerFolder = xmlDoc.SelectSingleNode("LibSettings/PdfServerFolder").InnerText;
                    }

                    if (xmlDoc.SelectSingleNode("LibSettings/PdfSendFolder") != null)
                    {
                        Current.PdfSendFolder = xmlDoc.SelectSingleNode("LibSettings/PdfSendFolder").InnerText;
                    }

                    if (xmlDoc.SelectSingleNode("LibSettings/AnesDataFolder") != null)
                    {
                        Current.AnesDataFolder = xmlDoc.SelectSingleNode("LibSettings/AnesDataFolder").InnerText;
                    }

                    if (xmlDoc.SelectSingleNode("LibSettings/AnesImageFolder") != null)
                    {
                        Current.AnesImageFolder = xmlDoc.SelectSingleNode("LibSettings/AnesImageFolder").InnerText;
                    }

                    if (xmlDoc.SelectSingleNode("LibSettings/BillPdfFolder") != null)
                    {
                        Current.BillPdfFolder = xmlDoc.SelectSingleNode("LibSettings/BillPdfFolder").InnerText;
                    }

                    if (xmlDoc.SelectSingleNode("LibSettings/InvoicePdfFolder") != null)
                    {
                        Current.InvoicePdfFolder = xmlDoc.SelectSingleNode("LibSettings/InvoicePdfFolder").InnerText;
                    }

                    if (xmlDoc.SelectSingleNode("LibSettings/DPCValue") != null)
                    {
                        Current.DPCValue = xmlDoc.SelectSingleNode("LibSettings/DPCValue").InnerText;
                    }

                    // 患者基本情報コード
                    foreach (XmlNode n in xmlDoc.SelectNodes("LibSettings/BaseInfoCodes/BaseInfoCode"))
                    {
                        BaseInfoCode obj = new BaseInfoCode();

                        obj.Code = n.Attributes["Code"].Value;

                        if (n.SelectSingleNode("Name") != null)
                        {
                            obj.Name = n.SelectSingleNode("Name").InnerText;
                        }

                        Current.BaseInfoCodes.BaseInfoCodeList.Add(obj);
                    }

                    // DPCメッセージ1
                    if (xmlDoc.SelectSingleNode("LibSettings/DPC/Msg1/From1") != null)
                    {
                        Current.DPCMsg1.From1 = xmlDoc.SelectSingleNode("LibSettings/DPC/Msg1/From1").InnerText;
                    }

                    if (xmlDoc.SelectSingleNode("LibSettings/DPC/Msg1/From2") != null)
                    {
                        Current.DPCMsg1.From2 = xmlDoc.SelectSingleNode("LibSettings/DPC/Msg1/From2").InnerText;
                    }

                    if (xmlDoc.SelectSingleNode("LibSettings/DPC/Msg1/To2") != null)
                    {
                        Current.DPCMsg1.To2 = xmlDoc.SelectSingleNode("LibSettings/DPC/Msg1/To2").InnerText;
                    }

                    // DPCメッセージ2
                    foreach (XmlNode n in xmlDoc.SelectNodes("LibSettings/DPC/Msg2"))
                    {
                        DPCMsg2 obj = new DPCMsg2();

                        obj.Ward = n.Attributes["Ward"].Value;

                        if (n.SelectSingleNode("From") != null)
                        {
                            obj.From = n.SelectSingleNode("From").InnerText;
                        }

                        if (n.SelectSingleNode("To") != null)
                        {
                            obj.To = n.SelectSingleNode("To").InnerText;
                        }

                        Current.DPCMsgList2.Add(obj);
                    }

                    // DPCメッセージ3
                    if (xmlDoc.SelectSingleNode("LibSettings/DPC/Msg3/From") != null)
                    {
                        Current.DPCMsg3.From = xmlDoc.SelectSingleNode("LibSettings/DPC/Msg3/From").InnerText;
                    }

                    if (xmlDoc.SelectSingleNode("LibSettings/DPC/Msg3/To") != null)
                    {
                        Current.DPCMsg3.To = xmlDoc.SelectSingleNode("LibSettings/DPC/Msg3/To").InnerText;
                    }

                    // PCごとの設定
                    foreach (XmlNode n in xmlDoc.SelectNodes("LibSettings/PCS/PC"))
                    {
                        PC obj = new PC();

                        obj.Name = n.Attributes["Name"].Value;

                        if (n.SelectSingleNode("BillPrinter") != null)
                        {
                            obj.BillPrinter = n.SelectSingleNode("BillPrinter").InnerText;
                        }

                        if (n.SelectSingleNode("InvoicePrinter") != null)
                        {
                            obj.InvoicePrinter = n.SelectSingleNode("InvoicePrinter").InnerText;
                        }

                        if (n.SelectSingleNode("PdfPrinterExe") != null)
                        {
                            obj.PdfPrinterExe = n.SelectSingleNode("PdfPrinterExe").InnerText;
                        }

                        if (n.SelectSingleNode("BillPayPlace") != null)
                        {
                            obj.BillPayPlace = n.SelectSingleNode("BillPayPlace").InnerText;
                        }

                        if (n.SelectSingleNode("BillPrintAuto") != null)
                        {
                            obj.BillPrintAuto = n.SelectSingleNode("BillPrintAuto").InnerText;
                        }

                        Current.PCS.PCList.Add(obj);
                    }

                    // Proasオーダー連携設定
                    if (xmlDoc.SelectSingleNode("LibSettings/Proas/OrderXmlTmpFolder") != null)
                    {
                        Current.Proas.OrderXmlTmpFolder = xmlDoc.SelectSingleNode("LibSettings/Proas/OrderXmlTmpFolder").InnerText;
                    }

                    if (xmlDoc.SelectSingleNode("LibSettings/Proas/OrderXmlDstFolder") != null)
                    {
                        Current.Proas.OrderXmlDstFolder = xmlDoc.SelectSingleNode("LibSettings/Proas/OrderXmlDstFolder").InnerText;
                    }

                    if (xmlDoc.SelectSingleNode("LibSettings/Proas/OrderXmlLogFolder") != null)
                    {
                        Current.Proas.OrderXmlLogFolder = xmlDoc.SelectSingleNode("LibSettings/Proas/OrderXmlLogFolder").InnerText;
                    }

                    foreach (XmlNode n in xmlDoc.SelectNodes("LibSettings/Proas/Shinku"))
                    {
                        ProasShinku obj = new ProasShinku();

                        obj.Code = n.Attributes["Code"].Value;

                        if (n.SelectSingleNode("ClassCode") != null)
                        {
                            obj.ClassCode = n.SelectSingleNode("ClassCode").InnerText;
                        }

                        Current.Proas.ShinkuList.Add(obj);
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
        public string Diag
        {
            get
            {
                string s = "";

                foreach (BaseInfoCode bc in this.BaseInfoCodeList)
                {
                    if (bc.Name.Equals("Diag"))
                    {
                        s = bc.Code;
                        break;
                    }
                }

                return s;
            }
        }

        /// <summary>
        /// アレルギー
        /// </summary>
        public string Allergy
        {
            get
            {
                string s = "";

                foreach (BaseInfoCode bc in this.BaseInfoCodeList)
                {
                    if (bc.Name.Equals("Allergy"))
                    {
                        s = bc.Code;
                        break;
                    }
                }

                return s;
            }
        }

        /// <summary>
        /// 内服・外用
        /// </summary>
        public string Drug
        {
            get
            {
                string s = "";

                foreach (BaseInfoCode bc in this.BaseInfoCodeList)
                {
                    if (bc.Name.Equals("Drug"))
                    {
                        s = bc.Code;
                        break;
                    }
                }

                return s;
            }
        }

        /// <summary>
        /// 身長（短期入院）
        /// </summary>
        public string Height
        {
            get
            {
                string s = "";

                foreach (BaseInfoCode bc in this.BaseInfoCodeList)
                {
                    if (bc.Name.Equals("Height"))
                    {
                        s = bc.Code;
                        break;
                    }
                }

                return s;
            }
        }

        /// <summary>
        /// 体重（短期入院）
        /// </summary>
        public string Weight
        {
            get
            {
                string s = "";

                foreach (BaseInfoCode bc in this.BaseInfoCodeList)
                {
                    if (bc.Name.Equals("Weight"))
                    {
                        s = bc.Code;
                        break;
                    }
                }

                return s;
            }
        }

    }

    public class BaseInfoCode
    {
        [XmlAttribute(AttributeName = "Code")]
        public string Code = "";

        [XmlElement(ElementName = "Name")]
        public string Name = "";
    }

    public class PCS
    {
        [XmlElement(ElementName = "PC")]
        public List<PC> PCList = new List<PC>();
    }

    public class PC
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name = "";

        [XmlElement(ElementName = "IP")]
        public string IP = "";

        [XmlElement(ElementName = "Dept")]
        public string Dept = "";

        [XmlElement(ElementName = "BillPrinter")]
        public string BillPrinter = "";

        [XmlElement(ElementName = "InvoicePrinter")]
        public string InvoicePrinter = "";

        [XmlElement(ElementName = "PdfPrinterExe")]
        public string PdfPrinterExe = "";

        [XmlElement(ElementName = "BillPayPlace")]
        public string BillPayPlace = "";

        [XmlElement(ElementName = "BillPrintAuto")]
        public string BillPrintAuto = "";
    }

    public class Proas
    {
        [XmlElement(ElementName = "OrderXmlTmpFolder")]
        public string OrderXmlTmpFolder = "";

        [XmlElement(ElementName = "OrderXmlDstFolder")]
        public string OrderXmlDstFolder = "";

		[XmlElement(ElementName = "OrderXmlLogFolder")]
		public string OrderXmlLogFolder = "";

        [XmlElement(ElementName = "Shinku")]
        public List<ProasShinku> ShinkuList = new List<ProasShinku>();
    }

    public class ProasShinku
    {
        [XmlAttribute(AttributeName = "Code")]
        public string Code = "";

        [XmlElement(ElementName = "ClassCode")]
        public string ClassCode = "";
    }


    /// <summary>
    /// DPCメッセージ1
    /// DPC病名がついていない人
    /// </summary>
    public class DPCMsg1
    {
        public string From1 = "355";

        public string From2 = "530";

        public string To2 = "355";
    }

    /// <summary>
    /// DPCメッセージ2
    /// DPC病名が更新された人
    /// </summary>
    public class DPCMsg2
    {
        public string Ward = "";

        public string From = "530";

        public string To = "355";
    }

    /// <summary>
    /// DPCメッセージ3
    /// 糖尿病の入院病名がついている人
    /// </summary>
    public class DPCMsg3
    {
        public string From = "530";

        public string To = "355";
    }
}
