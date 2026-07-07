using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class PatIns : StdKarte1
    {
        public int SEQ = 0;

        /// <summary>
        /// F03 保険番号
        /// </summary>
        public string Code = "";

        /// <summary>
        /// F04 被保険者記号
        /// </summary>
        public string Name1 = "";

        /// <summary>
        /// F05 被保険者番号
        /// </summary>
        public string Name2 = "";

        public int StartDate = 0;

        public string StartDateString
        {
            get
            {
                string s = "";

                if (this.StartDate > 0 && this.StartDate < 22000000)
                {
                    s = DateTimeAgent.DateFormat(this.StartDate, DateTimeAgent.DateFormatKind.LONG);
                }

                return s;
            }
        }

        public int EndDate = 0;

        public string EndDateString
        {
            get
            {
                string s = "";

                if (this.EndDate > 0 && this.EndDate < 22000000)
                {
                    s = DateTimeAgent.DateFormat(this.EndDate, DateTimeAgent.DateFormatKind.LONG);
                }

                return s;
            }
        }

        public bool IsValid
        {
            get
            {
                bool b = false;

                int today = int.Parse(DateTime.Now.ToString("yyyyMMdd"));

                if (today >= this.StartDate &&
                    (this.EndDate == 0 || this.EndDate >= today))
                {
                    b = true;
                }

                return b;
            }
        }

        /// <summary>
        /// F43 保険区分
        /// </summary>
        public int KindCode = 0;

        /// <summary>
        /// 保険種別
        /// </summary>
        public string KindName
        {
            get
            {
                string s = "";

                if (Insurance.Dict.ContainsKey(this.KindCode.ToString()))
                {
                    s = Insurance.Dict[this.KindCode.ToString()].FullName;
                }

                return s;
            }
        }

        /// <summary>
        /// 保険略称
        /// </summary>
        public string KindNameShort
        {
            get
            {
                string s = "";

                if (Insurance.Dict.ContainsKey(this.KindCode.ToString()))
                {
                    s = Insurance.Dict[this.KindCode.ToString()].ShortName;
                }

                return s;
            }
        }

        /// <summary>
        /// 外来負担率
        /// </summary>
        public int Per1 = 0;

        /// <summary>
        /// 入院負担率
        /// </summary>
        public int Per2 = 0;

        public override string ToString()
        {
            return this.Code + " " + this.KindNameShort;
        }


        public static Dictionary<int, PatIns> GetDict(string pt_id)
        {
            Dictionary<int, PatIns> dict = new Dictionary<int, PatIns>();

            if (pt_id.Length == 0)
            {
                return dict;
            }

            string cmd = "select * from M_PATIENT_HOKEN t " +
                " where t.P_ID = " + pt_id +
                " order by t.P_HOKEN";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatIns obj = GetFromStdClass(tmp);

                if (!dict.ContainsKey(obj.SEQ))
                {
                    dict.Add(obj.SEQ, obj);
                }
            }

            return dict;
        }

        public static PatIns GetData(string pt_id, string seq)
        {
            PatIns obj = new PatIns();

            if (!AppString.IsNumber(pt_id) || !AppString.IsNumber(seq))
            {
                return obj;
            }

            string cmd = "select * from M_PATIENT_HOKEN t " +
                " where t.P_ID = " + pt_id +
                " and t.P_HOKEN = " + seq +
                " order by t.P_HOKEN";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                obj = GetFromStdClass(tmp);
                break;
            }

            return obj;
        }

        public static List<PatIns> GetList(List<PatInsSEQ> pt_list)
        {
            List<PatIns> list = new List<PatIns>();

            string cmd = "select * from M_PATIENT_HOKEN t " +
                " where (t.P_ID, t.P_HOKEN) in (" + AppString.ConcatList(pt_list.ConvertAll((x) => { return "(" + x.PtId + "," + x.SEQ + ")"; }), ",") + ") " +
                " order by t.P_ID";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }
/*
        public static PatIns Load(string pt_id, string seq)
        {
            PatIns obj = new PatIns();

            if (pt_id.Length == 0 || seq.Length == 0)
            {
                return obj;
            }

            string cmd = "select * from IM02RC t " +
                " where t.IM02RC_F01 = " + pt_id + " and t.IM02RC_F02 = " + seq;

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                obj = GetFromStdClass(tmp);
                break;
            }

            return obj;
        }
*/
        static PatIns GetFromStdClass(StdClass tmp)
        {
            PatIns obj = new PatIns();

            obj.PtId = tmp.GetDataString("P_ID");
            int.TryParse(tmp.DataDict["P_HOKEN"].ToString(), out obj.SEQ);
            obj.Code = tmp.DataDict["MAIN_NO"].ToString().Trim();
            obj.Name1 = tmp.DataDict["MAIN_KIGOU"].ToString().Trim();
            obj.Name2 = tmp.DataDict["MAIN_BANGOU"].ToString().Trim();
            int.TryParse(tmp.DataDict["MAIN_DATE_S"].ToString(), out obj.StartDate);
            int.TryParse(tmp.DataDict["MAIN_DATE_E"].ToString(), out obj.EndDate);
            int.TryParse(tmp.DataDict["HOKEN_TYPE"].ToString(), out obj.KindCode);
            int.TryParse(tmp.DataDict["MAIN_RATE_OUT"].ToString(), out obj.Per1);
            int.TryParse(tmp.DataDict["MAIN_RATE_IN"].ToString(), out obj.Per2);

            return obj;
        }
    }

    public class PatInsSEQ : StdKarte1
    {
        /// <summary>
        /// 保険パターン番号
        /// </summary>
        public int SEQ = 0;
    }
}
