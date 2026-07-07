using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// 基本的指示事項マスター
    /// </summary>
    public class BaseOrderMaster : StdEntity
    {
        /// <summary>
        /// 連番
        /// </summary>
        public int SEQ = 0;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name1 = "";

        /// <summary>
        /// 約束条件（真ん中の値）リスト
        /// </summary>
        public List<string> List1 = new List<string>();

        /// <summary>
        /// 約束処方（右側の値）リスト
        /// </summary>
        public List<string> List2 = new List<string>();


        public static Dictionary<string, BaseOrderMaster> GetDict()
        {
            Dictionary<string, BaseOrderMaster> dict = new Dictionary<string, BaseOrderMaster>();

            // 項目名を取得
            string cmd = "select * from macs.TM50RC t " +
                " where t.TM50RC_F01 = 1000 " +
                " order by t.TM50RC_F02 ";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                BaseOrderMaster obj = new BaseOrderMaster();

                int.TryParse(tmp.DataDict["TM50RC_F02"].ToString(), out obj.SEQ);
                obj.Name1 = tmp.DataDict["TM50RC_F03"].ToString().Trim();

                dict.Add(obj.SEQ.ToString(), obj);
            }

            // 約束条件（真ん中の値）リストを取得
            cmd = "select * from macs.TM50RC t " +
                " where t.TM50RC_F01 >= 1001 and t.TM50RC_F01 <= 1020 " +
                " order by t.TM50RC_F01, t.TM50RC_F02 ";

            tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                string tmp_seq = tmp.DataDict["TM50RC_F01"].ToString().Substring(1).TrimStart('0');

                if (dict.ContainsKey(tmp_seq))
                {
                    BaseOrderMaster obj = dict[tmp_seq];

                    obj.List1.Add(tmp.DataDict["TM50RC_F03"].ToString().TrimEnd());
                }
            }

            // 約束処方（右の値）リストを取得
            cmd = "select * from macs.TM50RC t " +
                " where t.TM50RC_F01 >= 1051 and t.TM50RC_F01 <= 1070 " +
                " order by t.TM50RC_F01, t.TM50RC_F02 ";

            tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                string tmp_seq = tmp.DataDict["TM50RC_F01"].ToString().Substring(1).TrimStart('0');

                // 1051～1070 がそれぞれ 1 ～ 20 に該当する
                tmp_seq = (int.Parse(tmp_seq) - 50).ToString();

                if (dict.ContainsKey(tmp_seq))
                {
                    BaseOrderMaster obj = dict[tmp_seq];

                    obj.List2.Add(tmp.DataDict["TM50RC_F03"].ToString().TrimEnd());
                }
            }

            return dict;
        }
    }
}
