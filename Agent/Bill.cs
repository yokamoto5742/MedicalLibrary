using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MedicalLibrary.Utility;
using MedicalLibrary.Entity;

namespace MedicalLibrary.Agent
{
    public class Bill
    {
        public enum Kind : int
        {
            Main = 1,
            Sub = 2
        }

        public string PtId = "";

        PatBase _Pat = new PatBase();

        public PatBase Pat
        {
            get
            {
                if (!this._Pat.Id.Equals(this.PtId))
                {
                    this._Pat = PatBase.Load(this.PtId);
                }

                return this._Pat;
            }
        }

        public string BillId = "";

        /// <summary>
        /// 保険名（複数ある場合は全角スペース区切り）
        /// </summary>
        public string InsNames = "";

        /// <summary>
        /// 請求書 PDF原本（パスなし）
        /// </summary>
        public string SrcPdfFileName
        {
            get
            {
                return this.SrcFileName + ".pdf";
            }
        }

        /// <summary>
        /// 請求書 PDF原本のファイル名（パスなし・拡張子 .pdf/.txt なし）
        /// </summary>
        public string SrcFileName
        {
            get
            {
                return this.SrcTxtFileName.Substring(0, this.SrcTxtFileName.LastIndexOf('.'));
            }
        }

        /// <summary>
        /// 請求書 テキスト変換ファイル（パスなし）
        /// </summary>
        public string SrcTxtFileName = "";

        /// <summary>
        /// 請求書 リネーム後PDF（パスなし）
        /// </summary>
        public string DstPdfFileName
        {
            get
            {
                return "Bill_" + this.PtId + "_" + this.BillId + ".pdf";
            }
        }

        /// <summary>
        /// 請求書 リネーム後PDF控え（パスなし）
        /// </summary>
        public string DstPdfFileName2
        {
            get
            {
                return "Bill_" + this.PtId + "_" + this.BillId + "_sub.pdf";
            }
        }

        /// <summary>
        /// 割り当てられた明細書の数（0以上）
        /// </summary>
        public int InvoiceCount = 0;

        public string PcName = "";

        public string PcAddr = "";

        public string SaveDate = "";

        public string SaveTime = "";

        /// <summary>
        /// ステータス
        /// 0: データのみ（通常は存在しない）, 1: PDF作成済, 2: 印刷済
        /// </summary>
        public int Status = 0;


        public void PDFGen(string path_in, string path_out, string uke_seq, List<string> drug_seq_list, bool drug_out = false, string mark = "", Kind kind = Kind.Main)
        {
            // B5サイズを指定（ピクセル）
            //            Document doc = new Document(new Rectangle(595, 420, 1));
            Document doc = new Document(new Rectangle(728, 516, 1));

            // マージン設定。（ここに受付番号が印字される）
            //            doc.SetMargins(60, 0, 20, 0);

            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(path_out, FileMode.Create));

            doc.Open();

            PdfContentByte content = writer.DirectContent;

            ColumnText ct = new ColumnText(content);

            BaseFont bf = BaseFont.CreateFont(@"c:\windows\fonts\MSGOTHIC.TTC, 0", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font ft1 = new Font(bf, 14);
            Font ft2 = new Font(bf, 24);

            Phrase p1 = new Phrase("受付番号  " + uke_seq, ft1);

            ct.SetSimpleColumn(580, 465, 735, 485, 0, Element.ALIGN_LEFT);
            ct.AddText(p1);
            ct.Go();

            int y = 390;
            int t = 18;
            int j = 0;

            if (drug_seq_list.Count <= 3)
            {
                y = 390;
                t = 30;
            }
            else if (drug_seq_list.Count <= 6)
            {
                y = 408;
                t = 22;
                ft2 = new Font(bf, 18);
            }
            else if (drug_seq_list.Count <= 9)
            {
                y = 415;
                t = 18;
                ft2 = new Font(bf, 16);
            }
            else
            {
                y = 415;
                t = 18;
                ft2 = new Font(bf, 16);
            }

            for (int i = 0; i < drug_seq_list.Count; )
            {
                string drug_seq = drug_seq_list[i++].PadLeft(3, ' ');

                if (i < drug_seq_list.Count)
                {
                    drug_seq += " " + drug_seq_list[i++].PadLeft(3, ' ');
                }
                else
                {
                    drug_seq += "    ";
                }

                if (i < drug_seq_list.Count)
                {
                    drug_seq += " " + drug_seq_list[i++].PadLeft(3, ' ');
                }
                else
                {
                    drug_seq += "    ";
                }

                // １０個以上の場合は、１行あたり４個印字する
                if (drug_seq_list.Count >= 10)
                {
                    if (i < drug_seq_list.Count)
                    {
                        drug_seq += " " + drug_seq_list[i++].PadLeft(3, ' ');
                    }
                    else
                    {
                        drug_seq += "    ";
                    }
                }

                Phrase p2 = new Phrase(drug_seq.ToString(), ft2);

                ct.SetSimpleColumn(565, y - j * t, 715, y - (j - 1) * t, 1, Element.ALIGN_CENTER);
                ct.AddText(p2);
                ct.Go();

                j++;
            }

            if (drug_out)
            {
                Phrase p3 = new Phrase("院外", ft2);

                ct.SetSimpleColumn(565, y - j * t, 715, y - (j - 1) * t, 1, Element.ALIGN_CENTER);
                ct.AddText(p3);
                ct.Go();
            }

            // 患者IDのバーコード　←　Excel で出すので印字しないことになった 2015/08/10 sakane
            /*
            Barcode39 bar39 = new Barcode39();
            bar39.Code = this.PtId;
            bar39.BarHeight = 20;

            Image img39 = bar39.CreateImageWithBarcode(content, null, null);
            img39.SetAbsolutePosition(370, 470);
            content.AddImage(img39);
            */

            // 分割払い・口座引き落としのマーク
            if (mark.Length > 0)
            {
                Phrase pm1 = new Phrase(mark, ft1);

                ct.SetSimpleColumn(260, 415, 280, 440, 0, Element.ALIGN_LEFT);
                ct.AddText(pm1);
                ct.Go();
            }


            // Sub の場合は（控）を印字する
            if (kind == Kind.Sub)
            {
                BaseFont bfm = BaseFont.CreateFont(@"c:\windows\fonts\MSMINCHO.TTC, 0", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                Font fs1 = new Font(bfm, 16);
                Phrase ps1 = new Phrase("（控）", fs1);

                ct.SetSimpleColumn(280, 460, 350, 485, 0, Element.ALIGN_LEFT);
                ct.AddText(ps1);
                ct.Go();
            }

            if (this.InsNames.Equals("自賠責") || this.InsNames.Equals("労災"))
            {
                // 患者名を取得する
                PatBase p = PatBase.Load(this.PtId);

                content.BeginText();
                content.SetFontAndSize(bf, 12);

                content.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "ID： " + p.Id + "　　" + p.Name + " 様", 50, 330, 0);

                content.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "保険： " + this.InsNames, 60, 280, 0);
                content.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "本日のご請求はありません。", 60, 250, 0);

                content.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "〒939-0243　射水市下若89-10", 300, 80, 0);
                content.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "医療法人真生会　真生会富山病院", 300, 60, 0);

                content.SetFontAndSize(bf, 10);
                content.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "く す り 引 換 券", 595, 455, 0);

                content.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "日付　" + DateTime.Now.ToString("yyyy年MM月dd日"), 570, 330, 0);
                content.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "ID　　" + p.Id, 570, 310, 0);
                content.ShowTextAligned(PdfContentByte.ALIGN_LEFT, p.Kana, 580, 290, 0);

                content.SetFontAndSize(bf, 12);
                content.ShowTextAligned(PdfContentByte.ALIGN_LEFT, p.Name + " 様", 580, 277, 0);

                content.SetFontAndSize(bf, 10);
                content.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "※くすりを受け取る際は必ず", 570, 230, 0);
                content.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "　番号・名前を確かめてくだ", 570, 215, 0);
                content.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "　さい。", 570, 200, 0);

                content.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "※くすりは内容により番号順", 570, 180, 0);
                content.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "　にできない場合があります。", 570, 165, 0);
                content.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "　ご了承ください。", 570, 150, 0);

                content.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "〒939-0243 射水市下若89-10", 570, 80, 0);
                content.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "医療法人真生会", 600, 65, 0);

                content.SetFontAndSize(bf, 14);
                content.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "真生会富山病院", 600, 48, 0);

                content.EndText();

                content.Rectangle(570, 370, 145, 100);
                content.Stroke();

                doc.Close();
            }
            else
            {
                PdfReader reader = new PdfReader(path_in);

                int pageNum = reader.NumberOfPages;

                for (int i = 1; i <= pageNum; i++)
                {
                    PdfImportedPage page = writer.GetImportedPage(reader, i);

                    Image img = Image.GetInstance(page);
                    img.SetAbsolutePosition(0, 0);

                    content.AddImage(img);
                }

                doc.Close();
                reader.Close();
            }
        }

        static Bill GetFromStdClass(StdClass tmp)
        {
            Bill obj = new Bill();

            obj.BillId = tmp.GetDataString("BILL_ID");
            obj.PtId = tmp.GetDataString("PATIENT_ID");
            obj.InsNames = tmp.GetDataString("INS_NAMES");
            obj.InvoiceCount = tmp.GetDataInt("INVOICE_COUNT");
            obj.PcName = tmp.GetDataString("PC_NAME");
            obj.PcAddr = tmp.GetDataString("PC_ADDR");
            obj.SaveDate = tmp.GetDataString("SAVE_DATE");
            obj.SaveTime = tmp.GetDataString("SAVE_TIME");
            obj.Status = tmp.GetDataInt("STATUS");

            return obj;
        }

        public static Bill Load(string bill_id)
        {
            Bill obj = new Bill();

            if (bill_id.Length == 0)
            {
                return obj;
            }

            string cmd = "";

            cmd = "select * from BILL where BILL_ID = " + bill_id;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                obj = GetFromStdClass(tmp);
                break;
            }

            return obj;
        }

        public static List<Bill> GetListByDate(string date)
        {
            List<Bill> list = new List<Bill>();

            if (!DateTimeAgent.IsDate(date))
            {
                return list;
            }

            string cmd = "";

            cmd = "select * from BILL where SAVE_DATE = " + date;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                Bill obj = GetFromStdClass(tmp);
                list.Add(obj);
            }

            return list;
        }


        public StdReturn Save()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();

            obj.Table = "BILL";
            obj.Db = DB.Db2;

            obj.DataList.Add(new StdDbColumn("PATIENT_ID", StdDbType.NUMBER, this.PtId));
            obj.DataList.Add(new StdDbColumn("INS_NAMES", StdDbType.VARCHAR2, this.InsNames));
            obj.DataList.Add(new StdDbColumn("INVOICE_COUNT", StdDbType.NUMBER, this.InvoiceCount));
            obj.DataList.Add(new StdDbColumn("PC_NAME", StdDbType.VARCHAR2, Environment.MachineName));
            obj.DataList.Add(new StdDbColumn("PC_ADDR", StdDbType.VARCHAR2, AppStat.IP4));
            obj.DataList.Add(new StdDbColumn("SAVE_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("SAVE_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, this.Status));

            obj.WhereList.Add("BILL_ID = " + this.BillId);

            sr = obj.UpdateSQL();

            if (sr.IntValue == 0)
            {
                obj.DataList.Add(new StdDbColumn("BILL_ID", StdDbType.VARCHAR2, this.BillId));
                sr = obj.InsertSQL();
            }

            return sr;
        }


        public static StdReturn StatusChange(string bill_id, int status)
        {
            StdReturn sr = new StdReturn();

            if (bill_id.Length == 0)
            {
                return sr;
            }

            StdDbClass obj = new StdDbClass();

            obj.Table = "BILL";
            obj.Db = DB.Db2;

            obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, status));

            obj.WhereList.Add("BILL_ID = " + bill_id);

            sr = obj.UpdateSQL();

            return sr;
        }
    }
}
