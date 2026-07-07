using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// プロブレムマスター
    /// </summary>
    public class ProblemMaster : StdEntity
    {
        /// <summary>
        /// 上位ＩＤ
        /// </summary>
        public int SEQ1 = 0;

        /// <summary>
        /// 連番
        /// </summary>
        public int SEQ2 = 0;

        /// <summary>
        /// 問題内容
        /// </summary>
        public string Cont = "";

        static List<ProblemMaster> all_list = new List<ProblemMaster>();

        public static List<ProblemMaster> AllList
        {
            get
            {
                if (all_list.Count == 0)
                {
#if INNO
#else
                    string cmd = "select t1.上位ＩＤ, t1.連番, t2.問題内容 from macs.AMB_プロブレム階層関係マスター t1, macs.AMB_プロブレムマスター t2 " +
                        " where t1.連番 = t2.連番 " +
                        " and t2.入力者コード = 0 " +
                        " order by t1.連番";

                    List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

                    foreach (StdClass tmp in tmp_list)
                    {
                        ProblemMaster obj = new ProblemMaster();

                        int.TryParse(tmp.DataDict["上位ＩＤ"].ToString(), out obj.SEQ1);
                        int.TryParse(tmp.DataDict["連番"].ToString(), out obj.SEQ2);
                        obj.Cont = tmp.DataDict["問題内容"].ToString();

                        all_list.Add(obj);
                    }
#endif
                }

                return all_list;
            }
        }

        public static List<ProblemMaster> GetList(int seq1 = 1)
        {
            List<ProblemMaster> list = new List<ProblemMaster>();

#if INNO
#else
            string cmd = "select t1.上位ＩＤ, t1.連番, t2.問題内容 from macs.AMB_プロブレム階層関係マスター t1, macs.AMB_プロブレムマスター t2 " +
                " where t1.連番 = t2.連番 " +
                " and t2.入力者コード = 0 " +
                " and t1.上位ＩＤ = " + seq1 +
                " order by t1.連番";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                ProblemMaster obj = new ProblemMaster();

                int.TryParse(tmp.DataDict["上位ＩＤ"].ToString(), out obj.SEQ1);
                int.TryParse(tmp.DataDict["連番"].ToString(), out obj.SEQ2);
                obj.Cont = tmp.DataDict["問題内容"].ToString();

                list.Add(obj);
            }
#endif
            return list;
        }
    }

    public class ProblemTopMaster : StdEntity
    {
        /// <summary>
        /// 階層ＩＤ
        /// </summary>
        public int SEQ1 = 0;

        /// <summary>
        /// 階層名称
        /// </summary>
        public string Name = "";

        static Dictionary<int, ProblemTopMaster> dict = new Dictionary<int,ProblemTopMaster>();

        public static Dictionary<int, ProblemTopMaster> Dict
        {
            get
            {
                if (dict.Count == 0)
                {
#if INNO
#else
                    string cmd = "select * from macs.AMB_プロブレム階層名称マスター t" +
                        " order by t.階層ＩＤ";

                    List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

                    foreach (StdClass tmp in tmp_list)
                    {
                        ProblemTopMaster obj = new ProblemTopMaster();

                        int.TryParse(tmp.DataDict["階層ＩＤ"].ToString(), out obj.SEQ1);
                        obj.Name = tmp.DataDict["階層名称"].ToString();

                        dict.Add(obj.SEQ1, obj);
                    }
#endif
                }

                return dict;
            }
        }
    }
}
