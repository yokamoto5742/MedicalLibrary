using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// オーダー画面の処方用法マスター
    /// </summary>
    public class Cust2 : StdEntity
    {
        public string DeptCode = "";

        public string DoctorCode = "";

        public int PageNum = 0;

        public int SEQ = 0;

        public string Name = "";

        public string Code = "";

        public string Value = "";

        public float Times = 0;

        /// <summary>
        /// すべて 2 で固定
        /// </summary>
        public int Kind
        {
            get
            {
                // 用法の場合は 2
                return 2;
            }
        }


        public static Dictionary<Tuple<string, int, int>, Cust2> GetDict(string dept_code, string doctor_code)
        {
            Dictionary<Tuple<string, int, int>, Cust2> dict = new Dictionary<Tuple<string, int, int>, Cust2>();

            // 科コード = 0 は全科共通
            // ＤＲコード = 0 はその科共通
            if (dept_code.Length == 0 || doctor_code.Length == 0)
            {
                return dict;
            }

            string cmd = "select * from macs.ＣＵＳＴマスター２ t" +
                " where t.科コード = " + dept_code +
                " and t.ＤＲコード in (0, " + doctor_code + ")" +
                " order by t.ＤＲコード, t.ページ番号, t.連番";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                Cust2 obj = new Cust2();

                obj.DeptCode = dept_code;
                obj.DoctorCode = tmp.DataDict["ＤＲコード"].ToString();
                int.TryParse(tmp.DataDict["ページ番号"].ToString(), out obj.PageNum);
                int.TryParse(tmp.DataDict["連番"].ToString(), out obj.SEQ);
                obj.Name = tmp.DataDict["名称"].ToString();
                obj.Code = tmp.DataDict["コード"].ToString();
                obj.Value = tmp.DataDict["値"].ToString();
                float.TryParse(tmp.DataDict["回数"].ToString(), out obj.Times);

                Tuple<string, int, int> t1 = new Tuple<string, int, int>(obj.DoctorCode, obj.PageNum, obj.SEQ);

                if (dict.ContainsKey(t1))
                {
                    dict[t1] = obj;
                }
                else
                {
                    dict.Add(t1, obj);
                }
            }

            return dict;
        }
    }
}
