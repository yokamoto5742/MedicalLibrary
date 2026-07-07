using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class StdKarte2 : StdKarte1
    {
        /// <summary>
        /// PDF出力区分
        /// </summary>
        public bool PdfFlg = false;

        public int PdfFlgVal
        {
            get
            {
                return (this.PdfFlg == true) ? 1 : 0;
            }
            set
            {
                if (value == 1)
                {
                    this.PdfFlg = true;
                }
                else
                {
                    this.PdfFlg = false;
                }
            }
        }

        /// <summary>
        /// PDF出力日
        /// </summary>
        public int PdfDate = 0;

        /// <summary>
        /// PDF出力時間
        /// </summary>
        public int PdfTime = 0;

        /// <summary>
        /// PDF出力日時
        /// </summary>
        public string PdfDateTime
        {
            get
            {
                string s = DateTimeAgent.DateFormat(this.PdfDate, DateTimeAgent.DateFormatKind.LONG) +
                    " " + DateTimeAgent.TimeFormat(this.PdfTime.ToString().PadLeft(6, '0').Substring(0, 4));

                return s;
            }
        }

        /// <summary>
        /// PDF出力日時
        /// yy/MM/dd HH:mm
        /// </summary>
        public string PdfDateTimeShort
        {
            get
            {
                string s = DateTimeAgent.DateFormat(this.PdfDate, DateTimeAgent.DateFormatKind.SHORT) +
                    " " + DateTimeAgent.TimeFormat(this.PdfTime.ToString().PadLeft(6, '0').Substring(0, 4));

                return s;
            }
        }


        public new void BaseFromStdClass(StdClass tmp)
        {
            base.BaseFromStdClass(tmp);
        }
    }
}
