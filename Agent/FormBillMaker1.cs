using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Imaging;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MedicalLibrary.Boundary;
using MedicalLibrary.Utility;
using MedicalLibrary.Entity;

namespace MedicalLibrary.Agent
{
    public partial class FormBillMaker1 : Form
    {
        Timer Timer1 = new Timer();

        enum WinMode : int
        {
            Stop = 1,
            Move = 2
        }

        WinMode WinMode1 = WinMode.Stop;

        /// <summary>
        /// マウスで動かす場合に記憶しておく最初の位置
        /// </summary>
        int startX = 0;
        int startY = 0;

        enum TickMode : int
        {
            PdfToText = 1,
            PdfGen = 2
        }

        TickMode TickMode1 = TickMode.PdfToText;


        public FormBillMaker1()
        {
            InitializeComponent();

            // 最初の表示位置
            this.Location = new System.Drawing.Point(50, 50);

            // 最小化する
            this.WindowState = FormWindowState.Minimized;
        }

        private void FormBillMaker1_Load(object sender, EventArgs e)
        {
            // 設定ファイルの読み込み
            try
            {
                LibSettings.Init();

                // バージョンファイルの読み込み
                string file = AppFile.FilePath("BillMaker1_Version.txt");

                if (File.Exists(file))
                {
                    StreamReader reader = new StreamReader(file, Encoding.Default);

                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Length > 0)
                        {
                            this.VersionLabel.Text = line;
                            break;
                        }
                    }
                }

                bool start_flg = false;

                string[] args = Environment.GetCommandLineArgs();

                for (int i = 1; i < args.Length; i++)
                {
                    if (args[i].Equals("-start"))
                    {
                        start_flg = true;
                    }
                }

                string msg = "";

                BillMakerSettings.Init();

                // PDF -> Text 変換ツールがなければエラー
                if (!File.Exists(BillMakerSettings.Current.PdfToText))
                {
                    msg += "PdfToText 実行ファイル " + BillMakerSettings.Current.PdfToText + " が存在しません" + Environment.NewLine;
                }

                // 請求書 PDF原本フォルダ
                if (!Directory.Exists(BillMakerSettings.Current.BillPdfSrc))
                {
                    Directory.CreateDirectory(BillMakerSettings.Current.BillPdfSrc);
                }

                // 請求書 変換テキストフォルダ
                if (!Directory.Exists(BillMakerSettings.Current.BillTxtSrc))
                {
                    Directory.CreateDirectory(BillMakerSettings.Current.BillTxtSrc);
                }

                // 請求書 PDF移動先フォルダ
                if (!Directory.Exists(BillMakerSettings.Current.BillPdfDst))
                {
                    msg += "請求書 PDF移動先フォルダ " + BillMakerSettings.Current.BillPdfDst + " が存在しません" + Environment.NewLine;
                }

                // 請求書 変換テキスト移動先フォルダ
                if (!Directory.Exists(BillMakerSettings.Current.BillTxtDst))
                {
                    Directory.CreateDirectory(BillMakerSettings.Current.BillTxtDst);
                }

                // 請求書 PDF中間フォルダ
                if (!Directory.Exists(BillMakerSettings.Current.BillPdfMid))
                {
                    Directory.CreateDirectory(BillMakerSettings.Current.BillPdfMid);
                }

                // 請求書 PDF/TXT エラーフォルダ
                if (!Directory.Exists(BillMakerSettings.Current.BillErr))
                {
                    Directory.CreateDirectory(BillMakerSettings.Current.BillErr);
                }

                // 明細書 PDF原本フォルダ
                if (!Directory.Exists(BillMakerSettings.Current.InvoicePdfSrc))
                {
                    Directory.CreateDirectory(BillMakerSettings.Current.InvoicePdfSrc);
                }

                // 明細書 変換テキストフォルダ
                if (!Directory.Exists(BillMakerSettings.Current.InvoiceTxtSrc))
                {
                    Directory.CreateDirectory(BillMakerSettings.Current.InvoiceTxtSrc);
                }

                // 明細書 PDF移動先フォルダ
                if (!Directory.Exists(BillMakerSettings.Current.InvoicePdfDst))
                {
                    msg += "明細書 PDF移動先フォルダ " + BillMakerSettings.Current.InvoicePdfDst + " が存在しません" + Environment.NewLine;
                }

                // 明細書 変換テキスト移動先フォルダ
                if (!Directory.Exists(BillMakerSettings.Current.InvoiceTxtDst))
                {
                    Directory.CreateDirectory(BillMakerSettings.Current.InvoiceTxtDst);
                }

                // 明細書 PDF中間フォルダ
                if (!Directory.Exists(BillMakerSettings.Current.InvoicePdfMid))
                {
                    Directory.CreateDirectory(BillMakerSettings.Current.InvoicePdfMid);
                }

                // 明細書 PDF/TXT エラーフォルダ
                if (!Directory.Exists(BillMakerSettings.Current.InvoiceErr))
                {
                    Directory.CreateDirectory(BillMakerSettings.Current.InvoiceErr);
                }

                // フォルダのクリア
                foreach (string s in Directory.GetFiles(BillMakerSettings.Current.BillErr, "*.*"))
                {
                    File.Delete(s);
                }

                foreach (string s in Directory.GetFiles(BillMakerSettings.Current.BillTxtDst, "*.txt"))
                {
                    File.Delete(s);
                }

                foreach (string s in Directory.GetFiles(BillMakerSettings.Current.InvoiceErr, "*.*"))
                {
                    File.Delete(s);
                }

                foreach (string s in Directory.GetFiles(BillMakerSettings.Current.InvoiceTxtDst, "*.txt"))
                {
                    File.Delete(s);
                }


                // 以下のフォルダは、当日以外に作成されたファイルは消去する

                string today = DateTime.Now.ToString("yyyyMMdd");

                foreach (string s in Directory.GetFiles(BillMakerSettings.Current.BillPdfMid, "*.pdf"))
                {
                    FileInfo f = new FileInfo(s);

                    if (!f.LastWriteTime.ToString("yyyyMMdd").Equals(today))
                    {
                        File.Delete(s);
                    }
                }

                foreach (string s in Directory.GetFiles(BillMakerSettings.Current.BillTxtSrc, "*.txt"))
                {
                    FileInfo f = new FileInfo(s);

                    if (!f.LastWriteTime.ToString("yyyyMMdd").Equals(today))
                    {
                        File.Delete(s);
                    }
                }

                foreach (string s in Directory.GetFiles(BillMakerSettings.Current.InvoicePdfMid, "*.pdf"))
                {
                    FileInfo f = new FileInfo(s);

                    if (!f.LastWriteTime.ToString("yyyyMMdd").Equals(today))
                    {
                        File.Delete(s);
                    }
                }

                foreach (string s in Directory.GetFiles(BillMakerSettings.Current.InvoiceTxtSrc, "*.txt"))
                {
                    FileInfo f = new FileInfo(s);

                    if (!f.LastWriteTime.ToString("yyyyMMdd").Equals(today))
                    {
                        File.Delete(s);
                    }
                }

                Timer1.Interval = BillMakerSettings.Current.TickInterval1 * 1000;
                Timer1.Tick += new EventHandler(Timer1_Tick);

                if (msg.Length > 0)
                {
                    throw new Exception(msg);
                }
                else
                {
                    // 自動的にスタートさせることにする
                    // 2015/07/15 sakane
                    start_flg = true;

                    if (start_flg)
                    {
                        this.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "エラー");
                this.Dispose();
            }
        }

        /// <summary>
        /// 請求書と明細書のテキストファイルを突合し、ファイル名を「患者ID_請求書番号.pdf」に変更して
        /// 所定のフォルダに出力する
        /// </summary>
        /// <param name="sub">true: 控えを生成する, false: 控えを生成しない</param>
        void PdfGen(bool sub = false)
        {
            // 請求書
            List<Bill> bill_list = new List<Bill>();
            string[] files = Directory.GetFiles(BillMakerSettings.Current.BillTxtSrc, "*.txt");

            foreach (string file in files)
            {
                Bill bill = new Bill();
                bill.SrcTxtFileName = file.Substring(file.LastIndexOf('\\') + 1);
                
                StreamReader reader = new StreamReader(file, Encoding.Default);
                int pt_id = 0;

                // 「診療費請求書」「患者番号」いずれかの行が出てくるまで、行を全角スペースで連結させて
                // 「患者番号＋請求書番号＋保険（＋保険・・・）」という文字列を作る。
                string s = "";

                while (reader.Peek() >= 0)
                {
                    // 半角スペースは全角に変換する
                    string line = reader.ReadLine().Replace(' ', '　');

                    if (line.Contains("診療費請求書") || line.Contains("患者番号"))
                    {
                        break;
                    }

                    if (s.Length > 0)
                    {
                        s += "　";
                    }

                    s += line;
                }

                reader.Close();

                string[] ss = s.Trim().Split('　');

                if (ss.Length >= 3)
                {
                    bill.PtId = AppString.ZenToHan(ss[0]);
                    bill.BillId = AppString.ZenToHan(ss[1]);

                    for (int i = 2; i < ss.Length; i++)
                    {
                        // 「マル長」という文字が出てきたら飛ばす
                        if (ss[i].Equals("マル長"))
                        {
                            continue;
                        }

                        if (bill.InsNames.Length > 0)
                        {
                            bill.InsNames += "　";
                        }

                        bill.InsNames += ss[i];
                    }
                }

                // 患者ID または請求書番号が不正と思われる場合はエラーフォルダへ移動
                if (bill.PtId.Length == 0 || bill.PtId.Length > 9 || !int.TryParse(bill.PtId, out pt_id) || bill.BillId.Length == 0)
                {
                    string pdf_src = BillMakerSettings.Current.BillPdfSrc + "\\" + bill.SrcPdfFileName;
                    string err_file = BillMakerSettings.Current.BillErr + "\\" + bill.SrcPdfFileName;

                    // サーバーにもコピー
                    string err_server_file = BillMakerSettings.Current.LogServerFolderPath + "\\" + bill.SrcPdfFileName;

                    // 元のPDFファイルを移動
                    if (File.Exists(pdf_src))
                    {
                        File.Copy(pdf_src, err_file, true);
                        File.Copy(pdf_src, err_server_file, true);
                        File.Delete(pdf_src);
                    }

                    err_file = BillMakerSettings.Current.BillErr + "\\" + bill.SrcFileName + ".txt";
                    err_server_file = BillMakerSettings.Current.LogServerFolderPath + "\\" + bill.SrcFileName + ".txt";

                    // テキストファイルを移動
                    if (File.Exists(file))
                    {
                        File.Copy(file, err_file, true);
                        File.Copy(file, err_server_file, true);
                        File.Delete(file);
                    }

                    this.Log("★エラー 請求書 PDF生成 " + bill.SrcPdfFileName);
                    continue;
                }

                bill_list.Add(bill);
            }

            // 明細書
            List<Invoice> invoice_list = new List<Invoice>();
            files = Directory.GetFiles(BillMakerSettings.Current.InvoiceTxtSrc, "*.txt");

            foreach (string file in files)
            {
                Invoice invoice = new Invoice();
                invoice.SrcTxtFileName = file.Substring(file.LastIndexOf('\\') + 1);

                StreamReader reader = new StreamReader(file, Encoding.Default);
                int pt_id = 0;

                // 「診療明細書」「患者番号」いずれかの行が出てくるまで、行を全角スペースで連結させて
                // 「患者番号＋保険（＋保険・・・）」という文字列を作る。
                string s = "";

                while (reader.Peek() >= 0)
                {
                    // 半角スペースは全角に変換する
                    string line = reader.ReadLine().Replace(' ', '　');

                    if (line.Contains("診療明細書") || line.Contains("患者番号"))
                    {
                        break;
                    }

                    if (s.Length > 0)
                    {
                        s += "　";
                    }

                    s += line;
                }

                reader.Close();

                string[] ss = s.Trim().Split('　');

                if (ss.Length >= 2)
                {
                    invoice.PtId = AppString.ZenToHan(ss[0]);

                    for (int i = 1; i < ss.Length; i++)
                    {
                        if (invoice.InsString.Length > 0)
                        {
                            invoice.InsString += "　";
                        }

                        invoice.InsString += ss[i];
                    }
                }

                // 患者ID が不正と思われる場合はエラーフォルダへ移動
                if (invoice.PtId.Length == 0 || invoice.PtId.Length > 9 || !int.TryParse(invoice.PtId, out pt_id))
                {
                    string pdf_src = BillMakerSettings.Current.InvoicePdfSrc + "\\" + invoice.SrcPdfFileName;
                    string err_file = BillMakerSettings.Current.InvoiceErr + "\\" + invoice.SrcPdfFileName;

                    // サーバーにもコピー
                    string err_server_file = BillMakerSettings.Current.LogServerFolderPath + "\\" + invoice.SrcPdfFileName;

                    // 元のPDFファイルを移動
                    if (File.Exists(pdf_src))
                    {
                        File.Copy(pdf_src, err_file, true);
                        File.Copy(pdf_src, err_server_file, true);
                        File.Delete(pdf_src);
                    }

                    err_file = BillMakerSettings.Current.InvoiceErr + "\\" + invoice.SrcFileName + ".txt";
                    err_server_file = BillMakerSettings.Current.LogServerFolderPath + "\\" + invoice.SrcFileName + ".txt";

                    // テキストファイルを移動
                    if (File.Exists(file))
                    {
                        File.Copy(file, err_file, true);
                        File.Copy(file, err_server_file, true);
                        File.Delete(file);
                    }

                    this.Log("★エラー 明細書 PDF生成 " + invoice.SrcPdfFileName);
                    continue;
                }

                invoice_list.Add(invoice);
            }

            // 請求書と明細書を組み合わせる
            foreach (Bill bill in bill_list)
            {
                foreach (Invoice invoice in invoice_list)
                {
                    if (invoice.BillCount > 0)
                    {
                        continue;
                    }

                    // 患者IDと保険が一致すれば紐づける
                    if (bill.PtId.Equals(invoice.PtId) && bill.InsNames.Equals(invoice.InsString))
                    {
                        bill.InvoiceCount++;
                        invoice.BillCount++;
                        invoice.BillId = bill.BillId;
                        invoice.DstPdfFileName = "Invoice_" + invoice.PtId + "_" + invoice.BillId + "_" + bill.InvoiceCount + ".pdf";
                    }
                }
            }

            foreach (Bill bill in bill_list)
            {
                if (bill.InvoiceCount > 0)
                {
                    // リネームして生成後フォルダへ移動。

                    string pdf_src = BillMakerSettings.Current.BillPdfSrc + "\\" + bill.SrcPdfFileName;

                    if (!File.Exists(pdf_src))
                    {
                        // 元フォルダになければ中間フォルダを探す
                        pdf_src = BillMakerSettings.Current.BillPdfMid + "\\" + bill.SrcPdfFileName;
                    }

                    string pdf_dst = BillMakerSettings.Current.BillPdfDst + "\\" + bill.DstPdfFileName;
                    string pdf_dst2 = BillMakerSettings.Current.BillPdfDst + "\\" + bill.DstPdfFileName2;

                    // 受付番号を取得する
                    List<int> uke_list = PatOut.GetOnedaySeqs(bill.PtId, DateTime.Now.ToString("yyyyMMdd"));
                    string uke_seq = "（なし）";

                    if (uke_list.Count > 0)
                    {
                        uke_seq = uke_list[uke_list.Count - 1].ToString();
                    }

                    // お薬番号を取得する
                    List<PatOutDrugSeq> drug_list = PatOutDrugSeq.GetListByDate(bill.PtId, DateTime.Now.ToString("yyyyMMdd"));
                    List<string> drug_seq_list = new List<string>();
                    bool drug_out = false;

                    if (drug_list.Count > 0)
                    {
                        foreach (PatOutDrugSeq obj in drug_list)
                        {
                            if (obj.DrugInOut.Equals("0"))
                            {
                                // 院内処方
                                if (!drug_seq_list.Contains(obj.DrugSeqIn))
                                {
                                    drug_seq_list.Add(obj.DrugSeqIn);
                                }
                            }
                            else
                            {
                                // 院外処方
                                drug_out = true;
                            }
                        }
                    }

                    // 分割払い・口座引き落としマークを取得する
                    string mark = PatBase.Load(bill.PtId).Note1.BillMark;

                    if (File.Exists(pdf_src))
                    {
                        bill.PDFGen(pdf_src, pdf_dst, uke_seq, drug_seq_list, drug_out, mark, Bill.Kind.Main);

                        // 控えを生成する場合
                        if (sub)
                        {
                            bill.PDFGen(pdf_src, pdf_dst2, uke_seq, drug_seq_list, drug_out, mark, Bill.Kind.Sub);
                        }

                        // 元のPDFは削除する
                        File.Delete(pdf_src);
                    }

                    // TXTファイルを移動させる
                    string txt_src = BillMakerSettings.Current.BillTxtSrc + "\\" + bill.SrcTxtFileName;
                    string txt_dst = BillMakerSettings.Current.BillTxtDst + "\\" + bill.SrcTxtFileName;

                    if (File.Exists(txt_src))
                    {
                        File.Copy(txt_src, txt_dst, true);
                        File.Delete(txt_src);
                    }

                    this.Log("請求書 PDF生成 " + bill.SrcPdfFileName + " -> " + bill.DstPdfFileName);
                }
                else
                {
                    // 中間フォルダにあればそのまま。なければ中間フォルダへ移動。

                    string pdf_src = BillMakerSettings.Current.BillPdfSrc + "\\" + bill.SrcPdfFileName;
                    string pdf_dst = BillMakerSettings.Current.BillPdfMid + "\\" + bill.SrcPdfFileName;

                    if (File.Exists(pdf_src))
                    {
                        File.Copy(pdf_src, pdf_dst, true);
                        File.Delete(pdf_src);
                    }
                }
            }

            foreach (Invoice invoice in invoice_list)
            {
                if (invoice.BillCount > 0)
                {
                    // リネームして生成後フォルダへ移動。

                    string pdf_src = BillMakerSettings.Current.InvoicePdfSrc + "\\" + invoice.SrcPdfFileName;

                    if (!File.Exists(pdf_src))
                    {
                        // 元フォルダになければ中間フォルダを探す
                        pdf_src = BillMakerSettings.Current.InvoicePdfMid + "\\" + invoice.SrcPdfFileName;
                    }

                    string pdf_dst = BillMakerSettings.Current.InvoicePdfDst + "\\" + invoice.DstPdfFileName;

                    if (File.Exists(pdf_src))
                    {
                        File.Copy(pdf_src, pdf_dst, true);
                        File.Delete(pdf_src);
                    }

                    // TXTファイルを移動させる
                    string txt_src = BillMakerSettings.Current.InvoiceTxtSrc + "\\" + invoice.SrcTxtFileName;
                    string txt_dst = BillMakerSettings.Current.InvoiceTxtDst + "\\" + invoice.SrcTxtFileName;

                    if (File.Exists(txt_src))
                    {
                        File.Copy(txt_src, txt_dst, true);
                        File.Delete(txt_src);
                    }

                    this.Log("明細書 PDF生成 " + invoice.SrcPdfFileName + " -> " + invoice.DstPdfFileName);
                }
                else
                {
                    // 中間フォルダにあればそのまま。なければ中間フォルダへ移動。

                    string pdf_src = BillMakerSettings.Current.InvoicePdfSrc + "\\" + invoice.SrcPdfFileName;
                    string pdf_dst = BillMakerSettings.Current.InvoicePdfMid + "\\" + invoice.SrcPdfFileName;

                    if (File.Exists(pdf_src))
                    {
                        File.Copy(pdf_src, pdf_dst, true);
                        File.Delete(pdf_src);
                    }
                }
            }


            // DB登録 2018/12/27 by sakane
            foreach (Bill bill in bill_list)
            {
                if (bill.InvoiceCount > 0)
                {
                    try
                    {
                        bill.Status = 1;
                        bill.Save();
                    }
                    catch (Exception ex)
                    {
                        this.Log("★エラー DB登録 " + ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// PdfToText.exe を使って請求書・明細書PDFファイルのテキストを出力する
        /// </summary>
        void PdfToText()
        {
            // 請求書
            string[] files = Directory.GetFiles(BillMakerSettings.Current.BillPdfSrc, "*.pdf");

            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);

                // 明らかにファイルサイズが小さすぎる場合は飛ばす
                if (fi.Length < 10)
                {
                    continue;
                }

                string s = file.Substring(file.LastIndexOf('\\') + 1);
                string ss = s.Substring(0, s.LastIndexOf('.'));

                Process p = new Process();
                p.StartInfo.FileName = BillMakerSettings.Current.PdfToText;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.Arguments = file + " -enc Shift-JIS " + BillMakerSettings.Current.BillTxtSrc + "\\" + ss + ".txt";
                p.Start();

                p.WaitForExit();

                if (p.ExitCode > 0)
                {
                    // 元のPDFファイルはエラーフォルダへ移動
                    if (File.Exists(file))
                    {
                        string err_file = BillMakerSettings.Current.BillErr + "\\" + s;

                        // サーバーにもコピー
                        string err_server_file = BillMakerSettings.Current.LogServerFolderPath + "\\" + s;

                        File.Copy(file, err_server_file, true);
                        File.Move(file, err_file);
                    }

                    this.Log("★エラー 請求書 PdfToText 変換 " + s);
                }
                else
                {
                    this.Log("請求書 PdfToText 変換 " + s + " -> " + ss + ".txt");
                }
            }


            // 明細書
            files = Directory.GetFiles(BillMakerSettings.Current.InvoicePdfSrc, "*.pdf");

            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);

                // 明らかにファイルサイズが小さすぎる場合は飛ばす
                if (fi.Length < 10)
                {
                    continue;
                }

                string s = file.Substring(file.LastIndexOf('\\') + 1);
                string ss = s.Substring(0, s.LastIndexOf('.'));

                Process p = new Process();
                p.StartInfo.FileName = BillMakerSettings.Current.PdfToText;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.Arguments = file + " -enc Shift-JIS " + BillMakerSettings.Current.InvoiceTxtSrc + "\\" + ss + ".txt";
                p.Start();

                p.WaitForExit();

                if (p.ExitCode > 0)
                {
                    // 元のPDFファイルはエラーフォルダへ移動
                    if (File.Exists(file))
                    {
                        string err_file = BillMakerSettings.Current.InvoiceErr + "\\" + s;

                        // サーバーにもコピー
                        string err_server_file = BillMakerSettings.Current.LogServerFolderPath + "\\" + s;

                        File.Copy(file, err_server_file, true);
                        File.Move(file, err_file);
                    }

                    this.Log("★エラー 明細書 PdfToText 変換 " + s);
                }
                else
                {
                    this.Log("明細書 PdfToText 変換 " + s + " -> " + ss + ".txt");
                }
            }
        }

        private void StartButton1_Click(object sender, EventArgs e)
        {
            this.Start();
        }

        private void StopButton1_Click(object sender, EventArgs e)
        {
            this.Stop();
        }

        void Start()
        {
            this.InfoLabel1.Text = "稼働中...";
            this.InfoLabel1.ForeColor = System.Drawing.Color.Black;
            this.InfoLabel1.BackColor = System.Drawing.Color.LightPink;

            this.StartButton1.Enabled = false;
            this.StopButton1.Enabled = true;

            this.Log("開始");

            this.Timer1.Start();
        }

        void Stop()
        {
            this.InfoLabel1.Text = "停止中";
            this.InfoLabel1.ForeColor = System.Drawing.Color.Red;
            this.InfoLabel1.BackColor = System.Drawing.Color.LightYellow;

            this.StartButton1.Enabled = true;
            this.StopButton1.Enabled = false;

            this.Log("停止");

            this.Timer1.Stop();
        }

        void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this.TickMode1 == TickMode.PdfToText)
                {
                    this.PdfToText();
                    this.TickMode1 = TickMode.PdfGen;
                }
                else
                {
                    this.PdfGen(false);
                    this.TickMode1 = TickMode.PdfToText;
                }
            }
            catch (Exception ex)
            {
                this.Log("★例外★ " + ex.Message);
            }
        }

        public void Log(string msg)
        {
            // 文字数が上限に近づいたら消去する
            if (LogBox1.Text.Length > LogBox1.MaxLength - 1000)
            {
                LogBox1.Clear();
            }

            string dt = DateTime.Now.ToString("yy/MM/dd HH:mm:ss");

            LogBox1.Text += dt + " " + msg + Environment.NewLine;

            LogBox1.SelectionStart = LogBox1.Text.Length;
            LogBox1.Focus();
            LogBox1.ScrollToCaret();
        }

        private void FileClearButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("処理中の請求書・明細書ファイルが削除される恐れがありますので、処理が重くなった場合のみ行ってください。" + Environment.NewLine + "クリアしますか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                foreach (string s in Directory.GetFiles(BillMakerSettings.Current.BillPdfSrc, "*.pdf"))
                {
                    File.Delete(s);
                }

                foreach (string s in Directory.GetFiles(BillMakerSettings.Current.BillTxtSrc, "*.txt"))
                {
                    File.Delete(s);
                }

                foreach (string s in Directory.GetFiles(BillMakerSettings.Current.BillTxtDst, "*.txt"))
                {
                    File.Delete(s);
                }

                foreach (string s in Directory.GetFiles(BillMakerSettings.Current.BillPdfMid, "*.pdf"))
                {
                    File.Delete(s);
                }

                foreach (string s in Directory.GetFiles(BillMakerSettings.Current.BillErr, "*.*"))
                {
                    File.Delete(s);
                }

                foreach (string s in Directory.GetFiles(BillMakerSettings.Current.InvoicePdfSrc, "*.pdf"))
                {
                    File.Delete(s);
                }

                foreach (string s in Directory.GetFiles(BillMakerSettings.Current.InvoiceTxtSrc, "*.txt"))
                {
                    File.Delete(s);
                }

                foreach (string s in Directory.GetFiles(BillMakerSettings.Current.InvoiceTxtDst, "*.txt"))
                {
                    File.Delete(s);
                }

                foreach (string s in Directory.GetFiles(BillMakerSettings.Current.InvoicePdfMid, "*.pdf"))
                {
                    File.Delete(s);
                }

                foreach (string s in Directory.GetFiles(BillMakerSettings.Current.InvoiceErr, "*.*"))
                {
                    File.Delete(s);
                }
            }
        }

        private void ExitButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("終了しますか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.Dispose();
            }
        }

        private void PdfViewerButton1_Click(object sender, EventArgs e)
        {
            FormControl.FormBillPDFList_Show();
        }

        private void MinimizeButton1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void InfoLabel1_DoubleClick(object sender, EventArgs e)
        {
            StartButton1.Visible = !StartButton1.Visible;
            StopButton1.Visible = !StopButton1.Visible;
            FileClearButton1.Visible = !FileClearButton1.Visible;
            ExitButton1.Visible = !ExitButton1.Visible;
        }

        private void TitleLabel_MouseDown(object sender, MouseEventArgs e)
        {
            this.WinMode1 = WinMode.Move;
            this.startX = e.X;
            this.startY = e.Y;
        }

        private void TitleLabel_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.WinMode1 == WinMode.Move)
            {
                this.Location = new System.Drawing.Point(this.Location.X + (e.X - startX), this.Location.Y + (e.Y - startY));
            }
        }

        private void TitleLabel_MouseUp(object sender, MouseEventArgs e)
        {
            this.WinMode1 = WinMode.Stop;
            this.startX = 0;
            this.startY = 0;
        }

        private void VersionLabel_Click(object sender, EventArgs e)
        {
            string file = AppFile.FilePath("BillMaker1_RevHistory.txt");

            if (File.Exists(file))
            {
                FormInfo1 f = new FormInfo1("情報");
                f.FileRead(file);
                f.ShowDialog();
            }
        }
    }
}
