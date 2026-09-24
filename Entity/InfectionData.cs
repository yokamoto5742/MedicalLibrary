using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// 感染症検査結果
    /// </summary>
    public class InfectionData : StdEntity
    {
        /// <summary>
        /// 患者ID
        /// </summary>
        public string Id = "";

        /// <summary>
        /// 検査日
        /// </summary>
        public string Date = "";

        /// <summary>
        /// 検査結果
        /// </summary>
        public Dictionary<string, InfectionDetail> DictData = new Dictionary<string, InfectionDetail>();

        /// <summary>
        /// 検査結果文字列
        /// </summary>
        public string ResultString
        {
            get
            {
                string result = DateTimeAgent.DateFormat(Date, DateTimeAgent.DateFormatKind.LONG);

                foreach (InfectionDetail data in DictData.Values)
                {
                    result += ", " + data.Name + " " + data.Data;
                }

                return result;
            }
        }

        /// <summary>
        /// 最終の感染症検査結果を取得する。
        /// </summary>
        /// <param name="pt_id">患者ID</param>
        /// <returns></returns>
        public static InfectionData GetInfectionData(string pt_id)
        {
            InfectionData i = new InfectionData();

            if (pt_id.Length == 0)
            {
                return i;
            }
            string cmd = "select td.*, tm.NAME " +
                " from medb.M_INFECTION tm, medb.D_INFECTION td " +
                " where td.P_ID = " + pt_id +
                " and (td.DEL_FLG is null or td.DEL_FLG = 0) " +
                " and td.CODE = tm.CODE " +
                " order by td.EXAMIN_DATE desc, td.UP_DATE desc, td.UP_TIME desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                // 最初のデータならばIDと日付をセットする
                if (i.Id.Length == 0)
                {
                    i.Id = tmp.GetDataString("P_ID");
                    i.Date = tmp.GetDataString("EXAMIN_DATE");
                }

                // 日付が異なれば飛ばす
                if (!i.Date.Equals(tmp.GetDataString("EXAMIN_DATE")))
                {
                    continue;
                }

                if (!i.DictData.ContainsKey(tmp.GetDataString("CODE")))
                {
                    i.DictData.Add(tmp.GetDataString("CODE"), new InfectionDetail(tmp.GetDataString("CODE"), tmp.GetDataString("NAME"), tmp.GetDataString("RESULT")));
                }
            }
            return i;
        }
    }

    public class InfectionDetail
    {
        public string Code = "";
        public string Name = "";
        public string Data = "";

        public InfectionDetail(string code, string name, string data)
        {
            Code = code;
            Name = name;
            Data = data;
        }
    }
}
