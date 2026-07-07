using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedicalLibrary.Agent
{
    public class Invoice
    {
        public string PtId = "";

        public string BillId = "";

        public string InsString = "";

        /// <summary>
        /// 明細書 PDF原本（パスなし）
        /// </summary>
        public string SrcPdfFileName
        {
            get
            {
                return this.SrcFileName + ".pdf";
            }
        }

        /// <summary>
        /// 明細書 PDF原本のファイル名（パスなし・拡張子 .pdf/.txt なし）
        /// </summary>
        public string SrcFileName
        {
            get
            {
                return this.SrcTxtFileName.Substring(0, this.SrcTxtFileName.LastIndexOf('.'));
            }
        }

        /// <summary>
        /// 明細書 テキスト変換ファイル（パスなし）
        /// </summary>
        public string SrcTxtFileName = "";

        /// <summary>
        /// 明細書 リネーム後PDF（パスなし）
        /// </summary>
        public string DstPdfFileName = "";

        /// <summary>
        /// 割り当てられた請求書の数（0 or 1）
        /// </summary>
        public int BillCount = 0;
    }
}
