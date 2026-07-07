using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class PatOutKarteStatus : StdEntity
    {
        /// <summary>
        /// 患者コード
        /// </summary>
        public string Id = "";

        /// <summary>
        /// 受付ＮＯ（通番）
        /// </summary>
        public string Seq1 = "";

        /// <summary>
        /// 受付した科の合計数
        /// </summary>
        public int Amount = 0;

        /// <summary>
        /// 診察終了した科の数
        /// </summary>
        public int End = 0;

        public static List<PatOutKarteStatus> GetList(string come_date)
        {
            List<PatOutKarteStatus> list = new List<PatOutKarteStatus>();

            if (come_date.Length != 8)
            {
                return list;
            }

#if INNO
            string cmd = "select tt.P_ID, tt.UKE_NO, count(*) AMOUNT, sum(tt.SHINSATSU_END) ENDS " +
                " from " +
                " (select t.P_ID, t.UKE_NO " +
                " , case when (t.CHANGE_DEPT is not null and t.CHANGE_DEPT != 0) then t.CHANGE_DEPT else t.DEPT end DEPT " +
                " , case when (t.SHINSATSU_TIME_E is not null and t.SHINSATSU_TIME_E >= 0) then 1 else 0 end SHINSATSU_END " +
                " from D_UKETSUKE t " +
                " where t.UKE_DATE = " + come_date + ") tt " +
                " group by tt.P_ID, tt.UKE_NO " +
                " order by tt.P_ID, tt.UKE_NO";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatOutKarteStatus obj = new PatOutKarteStatus();

                obj.Id = tmp.DataDict["P_ID"].ToString();
                obj.Seq1 = tmp.DataDict["UKE_NO"].ToString();
                int.TryParse(tmp.DataDict["AMOUNT"].ToString(), out obj.Amount);
                int.TryParse(tmp.DataDict["ENDS"].ToString(), out obj.End);

                list.Add(obj);
            }
#else
            string cmd = "select tt.患者コード, tt.受付ＮＯ, count(*) 合計, sum(tt.診察終了) 終了 " +
                " from " +
                " (select t.患者コード, t.受付ＮＯ " +
                " , case when t.変更科コード != 0 then t.変更科コード else t.科コード end 科コード " +
                " , decode(t.診察終了時間, 0, 0, 1) 診察終了 " +
                " from macs.ADT_診察状況データ t " +
                " where t.受付日 = " + come_date + ") tt " +
                " group by tt.患者コード, tt.受付ＮＯ " +
                " order by tt.患者コード, tt.受付ＮＯ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatOutKarteStatus obj = new PatOutKarteStatus();

                obj.Id = tmp.DataDict["患者コード"].ToString();
                obj.Seq1 = tmp.DataDict["受付ＮＯ"].ToString();
                int.TryParse(tmp.DataDict["合計"].ToString(), out obj.Amount);
                int.TryParse(tmp.DataDict["終了"].ToString(), out obj.End);

                list.Add(obj);
            }
#endif
            return list;
        }
    }
}
