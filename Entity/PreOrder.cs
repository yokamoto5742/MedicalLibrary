using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// 先行実施
    /// </summary>
    public class PreOrder : StdEntity
    {
        public string KouiCode = "";

        public string KouiName
        {
            get
            {
                string s = "";

                if (Dict.KouiDict.ContainsKey(this.KouiCode))
                {
                    s = Dict.KouiDict[this.KouiCode];
                }

                return s;
            }
        }

        public string OrderCode = "";

        public string OrderName = "";

        public float Qty = 0;

        public string Unit = "";

        /// <summary>
        /// ログに記録する文字列
        /// </summary>
        public string LogString
        {
            get
            {
                string s = "";

                s += this.KouiCode + " " + this.KouiName + " " + this.OrderCode + " " + this.OrderName + " " + this.Qty + " " + this.Unit;

                return s;
            }
        }
    }
}
