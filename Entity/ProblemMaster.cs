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
                }

                return all_list;
            }
        }

        public static List<ProblemMaster> GetList(int seq1 = 1)
        {
            List<ProblemMaster> list = new List<ProblemMaster>();

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
                }

                return dict;
            }
        }
    }
}
