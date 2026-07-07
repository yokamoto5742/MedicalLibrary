using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class PatDept : PatBase
    {
        /// <summary>
        /// 最終受診日
        /// </summary>
        public string LastDate = "";


        /// <summary>
        /// 患者を指定して最終科と最終受診日のリストを取得する
        /// </summary>
        /// <param name="pt_id">患者</param>
        /// <returns></returns>
        public static List<PatDept> GetListByPats(string pt_id)
        {
            List<PatDept> list = new List<PatDept>();

            if (pt_id.Length == 0) return list;

#if INNO
            string cmd = "select * from ZERO_KAREKI tt " +
                " where (tt.PT_ID, tt.KAREKI_DATE) in " +
                " (select t.PT_ID, max(t.KAREKI_DATE) KAREKI_DATE " +
                "  from ZERO_KAREKI t " +
                "  where t.PT_ID = " + pt_id +
                "  group by t.PT_ID)";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);
#else
            string cmd = "select * from IM03RC tt " +
                " where (tt.IM03RC_F01, tt.IM03RC_F04) in " +
                " (select t.IM03RC_F01, max(t.IM03RC_F04) IM03RC_F04 " +
                "  from macs.IM03RC t " +
                "  where t.IM03RC_F01 = " + pt_id +
                "  group by t.IM03RC_F01)";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif
            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }


        /// <summary>
        /// 患者と科を指定して最終受診日のリストを取得する
        /// </summary>
        /// <param name="pt_list">患者リスト</param>
        /// <param name="dept_list">科リスト</param>
        /// <returns></returns>
        public static List<PatDept> GetListByPatsDepts(List<string> pt_list, List<string> dept_list)
        {
            List<PatDept> list = new List<PatDept>();

            List<string> pts = new List<string>();
            List<string> dps = new List<string>();

            foreach (string s in pt_list)
            {
                // 空の場合と、重複する場合は飛ばす
                if (s.Length == 0) continue;
                if (pts.Contains(s)) continue;

                pts.Add(s);
            }

            // 対象患者がいなければ終了
            if (pts.Count == 0)
            {
                return list;
            }

            foreach (string s in dept_list)
            {
                // 空の場合と、重複する場合は飛ばす
                if (s.Length == 0) continue;
                if (dps.Contains(s)) continue;

                dps.Add(s);
            }

            string cmd = "";

            foreach (string s in AppString.ConcatLists(pts, ",", "", 1000))
            {
                if (s.Length == 0) break;
#if INNO
                cmd = "select t.PT_ID, t.DEPT, max(t.KAREKI_DATE) KAREKI_DATE " +
                    " from ZERO_KAREKI t " +
                    " where t.PT_ID in (" + s + ") ";

                if (dps.Count > 0)
                {
                    cmd += " and t.DEPT in (" + AppString.ConcatList(dps, ",") + ")";
                }

                cmd += " group by t.PT_ID, t.DEPT";

                List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);
#else
                cmd = "select t.IM03RC_F01, t.IM03RC_F03, max(t.IM03RC_F04) IM03RC_F04 " +
                    " from macs.IM03RC t " +
                    " where t.IM03RC_F01 in (" + s + ") ";

                if (dps.Count > 0)
                {
                    cmd += " and t.IM03RC_F03 in (" + AppString.ConcatList(dps, ",") + ")";
                }

                cmd += " group by t.IM03RC_F01, t.IM03RC_F03";

                List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif
                foreach (StdClass tmp in tmp_list)
                {
                    list.Add(GetFromStdClass(tmp));
                }
            }

            return list;
        }


        new public static PatDept GetFromStdClass(StdClass tmp)
        {
            PatDept obj = new PatDept();
#if INNO
            obj.Id = tmp.GetDataString("PT_ID");
            obj.Ins = tmp.GetDataString("HOKEN_P");
            obj.Dept = tmp.GetDataString("DEPT");
            obj.LastDate = tmp.GetDataString("KAREKI_DATE");
#else
            obj.Id = tmp.GetDataString("IM03RC_F01");
            obj.Ins = tmp.GetDataString("IM03RC_F02");
            obj.InsKind = tmp.GetDataString("IM02RC_F43");
            obj.Dept = tmp.GetDataString("IM03RC_F03");
            obj.LastDate = tmp.GetDataString("IM03RC_F04");
#endif
            return obj;
        }
    }
}
