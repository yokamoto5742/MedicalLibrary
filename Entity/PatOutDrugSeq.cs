using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// お薬番号
    /// </summary>
    public class PatOutDrugSeq : PatBase
    {
        public string ComeDate = "";

        /// <summary>
        /// 院内/院外処方区分
        /// 0: 院内, 1: 院外
        /// </summary>
        public string DrugInOut = "";

        /// <summary>
        /// 0: 処方, 1: 注射
        /// （inno のみ）
        /// </summary>
        public string DrugKind = "";

        /// <summary>
        /// お薬番号
        /// 5000番台は院外
        /// </summary>
        public string DrugSeq = "";

        /// <summary>
        /// お薬番号（院内のみ）
        /// </summary>
        public string DrugSeqIn
        {
            get
            {
                string s = "";

                int i = 0;
                int.TryParse(this.DrugSeq, out i);

                if (i > 0 && i < 5000)
                {
                    s = i.ToString();
                }

                return s;
            }
        }

        /// <summary>
        /// 受付番号
        /// </summary>
        public string Seq1 = "";

        /// <summary>
        /// 薬番のリスト
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="come_date"></param>
        /// <returns></returns>
        public static List<PatOutDrugSeq> GetListByDate(string pt_id, string come_date)
        {
            List<PatOutDrugSeq> list = new List<PatOutDrugSeq>();

            if (pt_id.Length == 0)
            {
                return list;
            }

            if (come_date.Length != 8)
            {
                return list;
            }
#if INNO
            string cmd = "select distinct tk.P_ID, tk.DEPT, tk.IN_TYPE, tk.KUSURI_TYPE, tk.KUSURI_NO, tk.P_HOKEN, tk.UKE_NO " +
                " from D_ORDER_KUSURI tk " +
                " where tk.ORDER_NO in " +
                " (select td.ORDER_NO from D_ORDER_DETAIL td, M_ORDER m " +
                "  where td.ORDER_NO in " +
                "  (select t.ORDER_NO from D_ORDER_KUSURI t " +
                "   where t.P_ID = " + pt_id + " and t.ORDER_DATE = " + come_date +
                "   and t.INOUT = 1 and t.KUSURI_TYPE = 0 " +
                "   and (t.DEL_FLG is null or t.DEL_FLG = 0)) " +
                "  and td.ORDER_CODE = m.CODE and td.SEDAI = m.SEDAI " +
                "  and m.DATA_TYPE = 2) " +
                " order by tk.KUSURI_NO";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatOutDrugSeq obj = new PatOutDrugSeq();

                obj.ComeDate = come_date;
                obj.Id = tmp.GetDataString("P_ID");
                obj.Dept = tmp.GetDataString("DEPT");
                obj.DrugInOut = tmp.GetDataString("IN_TYPE");
                obj.DrugKind = tmp.GetDataString("KUSURI_TYPE");
                obj.DrugSeq = tmp.GetDataString("KUSURI_NO");
                obj.Ins = tmp.GetDataString("P_HOKEN");
                obj.Seq1 = tmp.GetDataInt("UKE_NO") > 0 ? tmp.GetDataInt("UKE_NO").ToString() : "";

                list.Add(obj);
            }
#else
            string cmd = "select * from macs.ID80RC t80" +
                " where (t80.ID80RC_F01, t80.ID80RC_F02, t80.ID80RC_F06) in " +
                " (select distinct t81.ID81RC_F02, t81.ID81RC_F03, t81.ID81RC_F04 " +
                " from macs.ＮＴオーダーヘッダー th, macs.ID81RC t81 " +
                " where t81.ID81RC_F03 = " + pt_id +
                " and t81.ID81RC_F02 = " + come_date +
                " and th.受付番号 = t81.ID81RC_F01)" +
                " order by t80.ID80RC_F06";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatOutDrugSeq obj = new PatOutDrugSeq();

                obj.ComeDate = come_date;
                obj.Id = tmp.DataDict["ID80RC_F02"].ToString();
                obj.Doctor = tmp.DataDict["ID80RC_F03"].ToString();
                obj.Dept = tmp.DataDict["ID80RC_F04"].ToString();
                obj.DrugInOut = tmp.DataDict["ID80RC_F05"].ToString();
                obj.DrugSeq = tmp.DataDict["ID80RC_F06"].ToString();
                obj.Ins = tmp.DataDict["ID80RC_F07"].ToString();
                obj.Seq1 = tmp.DataDict["ID80RC_F08"].ToString();

                list.Add(obj);
            }
#endif
            return list;
        }
/*
        /// <summary>
        /// 院内処方の薬番のリスト
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="come_date"></param>
        /// <returns></returns>
        public static List<PatOutDrugSeq> GetInListByDate(string pt_id, string come_date)
        {
            List<PatOutDrugSeq> list = new List<PatOutDrugSeq>();

            if (pt_id.Length == 0)
            {
                return list;
            }

            if (come_date.Length != 8)
            {
                return list;
            }
#if INNO
            string cmd = "select * from D_ORDER_KUSURI t " +
                " where t.P_ID = " + pt_id +
                " and t.ORDER_DATE = " + come_date +
                " and t.INOUT = 1 and t.KUSURI_TYPE = 0 " +
                " and t.IN_TYPE = 0 " +
                " and (t.DEL_FLG is null or t.DEL_FLG = 0) " +
                " order by t.KUSURI_NO";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatOutDrugSeq obj = new PatOutDrugSeq();

                obj.ComeDate = come_date;
                obj.Id = tmp.GetDataString("P_ID");
                obj.Dept = tmp.GetDataString("DEPT");
                obj.DrugInOut = tmp.GetDataString("IN_TYPE");
                obj.DrugKind = tmp.GetDataString("KUSURI_TYPE");
                obj.DrugSeq = tmp.GetDataString("KUSURI_NO");
                obj.Ins = tmp.GetDataString("P_HOKEN");
                obj.Seq1 = tmp.GetDataInt("UKE_NO") > 0 ? tmp.GetDataInt("UKE_NO").ToString() : "";

                list.Add(obj);
            }
#else
            string cmd = "select * from macs.ID80RC t80" +
                " where (t80.ID80RC_F01, t80.ID80RC_F02, t80.ID80RC_F06) in " +
                " (select distinct t81.ID81RC_F02, t81.ID81RC_F03, t81.ID81RC_F04 " +
                " from macs.ＮＴオーダーヘッダー th, macs.ID81RC t81 " +
                " where t81.ID81RC_F03 = " + pt_id +
                " and t81.ID81RC_F02 = " + come_date +
                " and th.受付番号 = t81.ID81RC_F01)" +
                " and t80.ID80RC_F05 = 0" +
                " order by t80.ID80RC_F06";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatOutDrugSeq obj = new PatOutDrugSeq();

                obj.ComeDate = come_date;
                obj.Id = tmp.DataDict["ID80RC_F02"].ToString();
                obj.Doctor = tmp.DataDict["ID80RC_F03"].ToString();
                obj.Dept = tmp.DataDict["ID80RC_F04"].ToString();
                obj.DrugInOut = tmp.DataDict["ID80RC_F05"].ToString();
                obj.DrugSeq = tmp.DataDict["ID80RC_F06"].ToString();
                obj.Ins = tmp.DataDict["ID80RC_F07"].ToString();
                obj.Seq1 = tmp.DataDict["ID80RC_F08"].ToString();

                list.Add(obj);
            }
#endif
            return list;
        }
 */
    }
}
