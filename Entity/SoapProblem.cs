using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// プロブレム
    /// </summary>
    public class SoapProblem : SoapKey
    {
        /// <summary>
        /// プロブレムＮＯ
        /// </summary>
        public string ProblemCode = "";

        /// <summary>
        /// 問題内容
        /// </summary>
        public string Cont = "";

        public string ContShow(float font_size, int width)
        {
            return AppString.Wrap(this.Cont, font_size, width);
        }


        public static Dictionary<string, List<SoapProblem>> GetDict(string pt_id, List<string> date_list)
        {
            Dictionary<string, List<SoapProblem>> dict = new Dictionary<string, List<SoapProblem>>();

            if (date_list.Count == 0)
            {
                return dict;
            }

            string date_str = "";

            foreach (string date in date_list)
            {
                if (date_str.Length > 0)
                {
                    date_str += ",";
                }

                date_str += date;
            }

            return dict;
        }

        static SoapProblem GetFromStdClass(StdClass tmp)
        {
            SoapProblem obj = new SoapProblem();

            return obj;
        }
    }
}
