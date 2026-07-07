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
#if INNO
#else
            string cmd = "select td.*, tm.問題内容 from macs.ADT_ＳＯＡＰ関連プロブレム td, macs.ADT_プロブレムデータ tm " +
                " where td.患者コード = " + pt_id +
                " and tm.患者コード = " + pt_id +
                " and td.登録日 in (" + date_str + ")" +
                " and td.プロブレムＮＯ = tm.プロブレムＮＯ" +
                " order by td.プロブレムＮＯ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                SoapProblem obj = GetFromStdClass(tmp);

                if (dict.ContainsKey(obj.Key))
                {
                    dict[obj.Key].Add(obj);
                }
                else
                {
                    List<SoapProblem> soap_problem_list = new List<SoapProblem>();
                    soap_problem_list.Add(obj);
                    dict.Add(obj.Key, soap_problem_list);
                }
            }
#endif

            return dict;
        }

        static SoapProblem GetFromStdClass(StdClass tmp)
        {
            SoapProblem obj = new SoapProblem();

#if INNO
#else
            obj.PtId = tmp.DataDict["患者コード"].ToString();
            int.TryParse(tmp.DataDict["入外区分"].ToString(), out obj.InOut);
            int.TryParse(tmp.DataDict["登録日"].ToString(), out obj.RegDate);
            int.TryParse(tmp.DataDict["登録時間"].ToString(), out obj.RegTime);
            obj.RegStaff = tmp.DataDict["登録者"].ToString();
            int.TryParse(tmp.DataDict["連番"].ToString(), out obj.SEQ);
            obj.ProblemCode = tmp.DataDict["プロブレムＮＯ"].ToString();
            obj.Cont = tmp.DataDict["問題内容"].ToString();
#endif
            return obj;
        }
    }
}
