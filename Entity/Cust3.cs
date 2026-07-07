using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// オーダー画面の処方マスター
    /// </summary>
    public class Cust3 : StdEntity
    {
        public string DeptCode = "";

        public string DoctorCode = "";

        public int PageNum = 0;

        public int SEQ1 = 0;

        public int SEQ2 = 0;

        public string SDCD = "";

        public string KouiName = "";

        public string Name = "";

        public string Code = "";

        public string Value = "";

        public float Qty = 0;

        public float Times = 0;

        /// <summary>
        /// 先頭行なら 1, それ以外なら 0
        /// </summary>
        public int Kind
        {
            get
            {
                int result = 0;

                // 診療区分が入っていれば先頭行とみなす
                if (this.KouiName.Length > 0)
                {
                    result = 1;

                }

                return result;
            }
        }


        /// <summary>
        /// 科共通データとＤＲ固有データの両方を取得する。
        /// </summary>
        /// <param name="dept_code"></param>
        /// <param name="doctor_code"></param>
        /// <returns></returns>
        public static Dictionary<Tuple<string, int, int>, List<Cust3>> GetDict(string dept_code, string doctor_code)
        {
            Dictionary<Tuple<string, int, int>, List<Cust3>> dict = new Dictionary<Tuple<string, int, int>, List<Cust3>>();

            // 科コード = 0 のデータは存在しない（ＤＲコード = 0 は科共通）
            if (dept_code.Length == 0 || dept_code == "0" || doctor_code.Length == 0)
            {
                return dict;
            }

            string cmd = "select * from macs.ＣＵＳＴマスター３ t" +
                " where t.科コード = " + dept_code +
                " and t.ＤＲコード in (0, " + doctor_code + ")" +
                " order by t.ＤＲコード, t.ページ番号, t.連番, t.明細連番";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                Cust3 obj = new Cust3();

                obj.DeptCode = dept_code;
                obj.DoctorCode = tmp.DataDict["ＤＲコード"].ToString();
                int.TryParse(tmp.DataDict["ページ番号"].ToString(), out obj.PageNum);
                int.TryParse(tmp.DataDict["連番"].ToString(), out obj.SEQ1);
                int.TryParse(tmp.DataDict["明細連番"].ToString(), out obj.SEQ2);
                obj.SDCD = tmp.DataDict["ＳＤＣＤ"].ToString();
                obj.KouiName = tmp.DataDict["診療区分"].ToString();
                obj.Name = tmp.DataDict["名称"].ToString();
                obj.Code = tmp.DataDict["コード"].ToString();
                obj.Value = tmp.DataDict["値"].ToString();
                float.TryParse(tmp.DataDict["数量"].ToString(), out obj.Qty);
                float.TryParse(tmp.DataDict["回数"].ToString(), out obj.Times);

                Tuple<string, int, int> t1 = new Tuple<string, int, int>(obj.DoctorCode, obj.PageNum, obj.SEQ1);

                if (dict.ContainsKey(t1))
                {
                    dict[t1].Add(obj);
                }
                else
                {
                    List<Cust3> list = new List<Cust3>();
                    list.Add(obj);
                    dict.Add(t1, list);
                }
            }

            return dict;
        }
    }
}
