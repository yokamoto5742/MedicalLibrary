using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class BillMakerSettings
    {
        /// <summary>
        /// 現在の設定情報オブジェクト
        /// </summary>
        public static BillMakerSettings Current = new BillMakerSettings();

        /// <summary>
        /// PdfToText.exe フルパス
        /// </summary>
        public string PdfToText = "";

        /// <summary>
        /// 請求書 PDF原本フォルダ
        /// </summary>
        public string BillPdfSrc = "";

        /// <summary>
        /// 請求書 変換テキストフォルダ
        /// </summary>
        public string BillTxtSrc = "";

        /// <summary>
        /// 請求書 PDF移動先フォルダ
        /// </summary>
        public string BillPdfDst = "";

        /// <summary>
        /// 請求書 変換テキスト移動先フォルダ
        /// </summary>
        public string BillTxtDst = "";

        /// <summary>
        /// 請求書 PDF中間フォルダ
        /// </summary>
        public string BillPdfMid = "";

        /// <summary>
        /// 請求書 PDF/TXT エラーフォルダ
        /// </summary>
        public string BillErr = "";

        /// <summary>
        /// 明細書 PDF原本フォルダ
        /// </summary>
        public string InvoicePdfSrc = "";

        /// <summary>
        /// 明細書 変換テキストフォルダ
        /// </summary>
        public string InvoiceTxtSrc = "";

        /// <summary>
        /// 明細書 PDF移動先フォルダ
        /// </summary>
        public string InvoicePdfDst = "";

        /// <summary>
        /// 明細書 変換テキスト移動先フォルダ
        /// </summary>
        public string InvoiceTxtDst = "";

        /// <summary>
        /// 明細書 PDF中間フォルダ
        /// </summary>
        public string InvoicePdfMid = "";

        /// <summary>
        /// 明細書 PDF/TXT エラーフォルダ
        /// </summary>
        public string InvoiceErr = "";

        /// <summary>
        /// 処理インターバル（秒）
        /// </summary>
        public int TickInterval1 = 5;

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
        /// アプリケーション設定ファイルの読み込み
        /// </summary>
        public static void Init()
        {
            string xml_file = AppFile.FilePath("BillMaker1_Settings.xml");

            if (File.Exists(xml_file))
            {
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(xml_file);

                    // いったんクリアする
                    Current = new BillMakerSettings();

                    if (xmlDoc.SelectSingleNode("BillMakerSettings/PdfToText") != null) Current.PdfToText = xmlDoc.SelectSingleNode("BillMakerSettings/PdfToText").InnerText;

                    if (xmlDoc.SelectSingleNode("BillMakerSettings/BillPdfSrc") != null) Current.BillPdfSrc = xmlDoc.SelectSingleNode("BillMakerSettings/BillPdfSrc").InnerText;
                    if (xmlDoc.SelectSingleNode("BillMakerSettings/BillTxtSrc") != null) Current.BillTxtSrc = xmlDoc.SelectSingleNode("BillMakerSettings/BillTxtSrc").InnerText;
                    if (xmlDoc.SelectSingleNode("BillMakerSettings/BillPdfDst") != null) Current.BillPdfDst = xmlDoc.SelectSingleNode("BillMakerSettings/BillPdfDst").InnerText;
                    if (xmlDoc.SelectSingleNode("BillMakerSettings/BillTxtDst") != null) Current.BillTxtDst = xmlDoc.SelectSingleNode("BillMakerSettings/BillTxtDst").InnerText;
                    if (xmlDoc.SelectSingleNode("BillMakerSettings/BillPdfMid") != null) Current.BillPdfMid = xmlDoc.SelectSingleNode("BillMakerSettings/BillPdfMid").InnerText;
                    if (xmlDoc.SelectSingleNode("BillMakerSettings/BillErr") != null) Current.BillErr = xmlDoc.SelectSingleNode("BillMakerSettings/BillErr").InnerText;

                    if (xmlDoc.SelectSingleNode("BillMakerSettings/InvoicePdfSrc") != null) Current.InvoicePdfSrc = xmlDoc.SelectSingleNode("BillMakerSettings/InvoicePdfSrc").InnerText;
                    if (xmlDoc.SelectSingleNode("BillMakerSettings/InvoiceTxtSrc") != null) Current.InvoiceTxtSrc = xmlDoc.SelectSingleNode("BillMakerSettings/InvoiceTxtSrc").InnerText;
                    if (xmlDoc.SelectSingleNode("BillMakerSettings/InvoicePdfDst") != null) Current.InvoicePdfDst = xmlDoc.SelectSingleNode("BillMakerSettings/InvoicePdfDst").InnerText;
                    if (xmlDoc.SelectSingleNode("BillMakerSettings/InvoiceTxtDst") != null) Current.InvoiceTxtDst = xmlDoc.SelectSingleNode("BillMakerSettings/InvoiceTxtDst").InnerText;
                    if (xmlDoc.SelectSingleNode("BillMakerSettings/InvoicePdfMid") != null) Current.InvoicePdfMid = xmlDoc.SelectSingleNode("BillMakerSettings/InvoicePdfMid").InnerText;
                    if (xmlDoc.SelectSingleNode("BillMakerSettings/InvoiceErr") != null) Current.InvoiceErr = xmlDoc.SelectSingleNode("BillMakerSettings/InvoiceErr").InnerText;

                    if (xmlDoc.SelectSingleNode("BillMakerSettings/TickInterval1") != null)
                    {
                        int i = 5;
                        int.TryParse(xmlDoc.SelectSingleNode("BillMakerSettings/TickInterval1").InnerText, out i);
                        Current.TickInterval1 = i;
                    }

                    if (xmlDoc.SelectSingleNode("BillMakerSettings/LogFolderPath") != null) Current.LogFolderPath = xmlDoc.SelectSingleNode("BillMakerSettings/LogFolderPath").InnerText;
                    if (xmlDoc.SelectSingleNode("BillMakerSettings/LogServerFolderPath") != null) Current.LogServerFolderPath = xmlDoc.SelectSingleNode("BillMakerSettings/LogServerFolderPath").InnerText;

                    if (xmlDoc.SelectSingleNode("BillMakerSettings/LogLevel") != null)
                    {
                        int i = 1;
                        int.TryParse(xmlDoc.SelectSingleNode("BillMakerSettings/LogLevel").InnerText, out i);
                        Current.LogLevel = i;
                    }
                }
                catch (Exception ex)
                {
                    LibUtility.Except(ex, false);
                }
            }

            try
            {
                // エラーチェック
                if (!Directory.Exists(Current.LogFolderPath))
                {
                    Directory.CreateDirectory(Current.LogFolderPath);
                }

                Current.LogServerFolderPath = Current.LogServerFolderPath + "\\" + AppStat.IP4;

                if (!Directory.Exists(Current.LogServerFolderPath))
                {
                    Directory.CreateDirectory(Current.LogServerFolderPath);
                }

                string msg = "";

                // 請求書 PDF原本フォルダ
                if (!Directory.Exists(Current.BillPdfSrc))
                {
                    Directory.CreateDirectory(Current.BillPdfSrc);
                }

                // 請求書 変換テキストフォルダ
                if (!Directory.Exists(Current.BillTxtSrc))
                {
                    Directory.CreateDirectory(Current.BillTxtSrc);
                }

                // 請求書 PDF移動先フォルダ
                if (!Directory.Exists(Current.BillPdfDst))
                {
                    msg += "請求書 PDF移動先フォルダ " + Current.BillPdfDst + " が存在しません" + Environment.NewLine;
                }

                // 請求書 変換テキスト移動先フォルダ
                if (!Directory.Exists(Current.BillTxtDst))
                {
                    Directory.CreateDirectory(Current.BillTxtDst);
                }

                // 請求書 PDF中間フォルダ
                if (!Directory.Exists(Current.BillPdfMid))
                {
                    Directory.CreateDirectory(Current.BillPdfMid);
                }

                // 請求書 PDF/TXT エラーフォルダ
                if (!Directory.Exists(Current.BillErr))
                {
                    Directory.CreateDirectory(Current.BillErr);
                }

                // 明細書 PDF原本フォルダ
                if (!Directory.Exists(Current.InvoicePdfSrc))
                {
                    Directory.CreateDirectory(Current.InvoicePdfSrc);
                }

                // 明細書 変換テキストフォルダ
                if (!Directory.Exists(Current.InvoiceTxtSrc))
                {
                    Directory.CreateDirectory(Current.InvoiceTxtSrc);
                }

                // 明細書 PDF移動先フォルダ
                if (!Directory.Exists(Current.InvoicePdfDst))
                {
                    msg += "明細書 PDF移動先フォルダ " + Current.InvoicePdfDst + " が存在しません" + Environment.NewLine;
                }

                // 明細書 変換テキスト移動先フォルダ
                if (!Directory.Exists(Current.InvoiceTxtDst))
                {
                    Directory.CreateDirectory(Current.InvoiceTxtDst);
                }

                // 明細書 PDF中間フォルダ
                if (!Directory.Exists(Current.InvoicePdfMid))
                {
                    Directory.CreateDirectory(Current.InvoicePdfMid);
                }

                // 明細書 PDF/TXT エラーフォルダ
                if (!Directory.Exists(Current.InvoiceErr))
                {
                    Directory.CreateDirectory(Current.InvoiceErr);
                }

                if (Current.TickInterval1 < 5)
                {
                    Current.TickInterval1 = 5;
                }

                if (msg.Length > 0)
                {
                    throw new Exception(msg);
                }
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex, false);
            }
        }
    }
}
