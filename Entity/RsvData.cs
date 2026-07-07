using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class RsvData : StdEntity
    {
        /// <summary>
        /// 予約種別コード
        /// </summary>
        public string Code1 = "";

        /// <summary>
        /// 予約種別
        /// </summary>
        public string Name1 = "";

        /// <summary>
        /// 予約詳細コード
        /// </summary>
        public string Code2 = "";

        /// <summary>
        /// 予約詳細
        /// </summary>
        public string Name2 = "";

        /// <summary>
        /// 予約種別コード
        /// </summary>
        public string Code
        {
            get
            {
                string s = "";

                if (this.Code2.Length > 0 && !this.Code2.Equals("0"))
                {
                    // 予約詳細がある場合
                    s = this.Code2;
                }
                else
                {
                    // 予約詳細が無い場合
                    s = this.Code1;
                }

                return s;
            }
        }

        /// <summary>
        /// 予約種別名称
        /// </summary>
        public string Name
        {
            get
            {
                string s = "";

                if (this.Name2.Length > 0)
                {
                    // 予約詳細がある場合
                    s = this.Name2;
                }
                else
                {
                    // 予約詳細が無い場合
                    s = this.Name1;
                }

                return s;
            }
        }

        /// <summary>
        /// 予約年月日
        /// </summary>
        public int RsvDate = 0;

        /// <summary>
        /// 予約開始時間
        /// </summary>
        public int Time1 = 0;

        /// <summary>
        /// 予約終了時間
        /// </summary>
        public int Time2 = 0;

        public string TimeString(string delimiter = "-")
        {
            string s = "";

            s += DateTimeAgent.TimeFormat6(this.Time1, 4, false);
            s += delimiter;
            s += DateTimeAgent.TimeFormat6(this.Time2, 4, false);

            return s;
        }

        PatBase _Pat = new PatBase();

        public PatBase Pat
        {
            get
            {
                return this._Pat;
            }
        }

        /// <summary>
        /// 備考１
        /// </summary>
        public string Cont1 = "";

        /// <summary>
        /// 備考２
        /// </summary>
        public string Cont2 = "";

        /// <summary>
        /// 連番
        /// </summary>
        public long SEQ = 0;

        /// <summary>
        /// 入力者コード
        /// </summary>
        public string Staff = "";

        public string StaffName
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(this.Staff))
                {
                    s = Dict.StaffDict[this.Staff].Name;
                }

                return s;
            }
        }


        /// <summary>
        /// StdClass からオブジェクトを取得する
        /// </summary>
        /// <param name="tmp"></param>
        /// <returns></returns>
        static RsvData GetFromStdClass(StdClass tmp)
        {
            RsvData obj = new RsvData();
            obj.Code1 = tmp.DataDict["YOYAKU_CODE"].ToString();
            obj.Pat.Id = tmp.DataDict["P_ID"].ToString();
            obj.Cont1 = tmp.DataDict["COMMENT_1"].ToString().TrimEnd();
            obj.Cont2 = tmp.DataDict["COMMENT_2"].ToString().TrimEnd();
            int.TryParse(tmp.DataDict["YOYAKU_DATE"].ToString(), out obj.RsvDate);
            int.TryParse(tmp.DataDict["YOYAKU_TIME_S"].ToString(), out obj.Time1);
            int.TryParse(tmp.DataDict["YOYAKU_TIME_E"].ToString(), out obj.Time2);
            long.TryParse(tmp.DataDict["YOYAKU_NO"].ToString(), out obj.SEQ);

            if (tmp.DataDict.ContainsKey("YOYAKU_NAME"))
            {
                obj.Name1 = tmp.DataDict["YOYAKU_NAME"].ToString().Trim();
            }

            obj.Staff = tmp.GetDataString("UP_USR").Length > 0 ? tmp.GetDataString("UP_USR") : tmp.GetDataString("REG_USR");

            obj.Pat.Name = tmp.GetDataString("P_NAME").Trim();
            obj.Pat.Sex = tmp.GetDataString("P_SEX");
            obj.Pat.Birth = tmp.GetDataString("P_BIRTHDAY_AD");
            return obj;
        }

        /// <summary>
        /// 指定種別・指定期間の予約一覧を取得する
        /// </summary>
        /// <param name="code1"></param>
        /// <param name="code2"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static Dictionary<int, List<RsvData>> GetDict(string code1, string code2, int date1, int date2)
        {
            Dictionary<int, List<RsvData>> dict = new Dictionary<int, List<RsvData>>();

            if (code1.Length == 0)
            {
                return dict;
            }
            string cmd = "select t.*, Trim(tp.P_NAME) 氏名, tp.P_SEX 性別, tp.P_BIRTHDAY_AD 生年月日 " +
                " from D_YOYAKU t, M_PATIENT tp " +
                " where t.YOYAKU_CODE = " + code1 +
                " and t.YOYAKU_DATE >= " + date1 + " and t.YOYAKU_DATE <= " + date2 +
                " and t.P_ID = tp.P_ID " +
                " order by t.YOYAKU_DATE, t.YOYAKU_TIME_S, t.YOYAKU_NO";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
            foreach (StdClass tmp in tmp_list)
            {
                RsvData obj = RsvData.GetFromStdClass(tmp);

                obj.Pat.Name = tmp.DataDict["氏名"].ToString().Trim();
                obj.Pat.Sex = tmp.DataDict["性別"].ToString();
                obj.Pat.Birth = tmp.DataDict["生年月日"].ToString();

                if (dict.ContainsKey(obj.RsvDate))
                {
                    dict[obj.RsvDate].Add(obj);
                }
                else
                {
                    List<RsvData> list = new List<RsvData>();
                    list.Add(obj);
                    dict.Add(obj.RsvDate, list);
                }
            }

            return dict;
        }

        /// <summary>
        /// 指定種別・指定期間の予約一覧を取得する
        /// </summary>
        /// <param name="code1"></param>
        /// <param name="code2"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static List<RsvData> GetListByDates(string code1, string code2, int date1, int date2)
        {
            List<RsvData> list = new List<RsvData>();

            if (code1.Length == 0)
            {
                return list;
            }
            string cmd = "select t.*, Trim(tp.P_NAME) 氏名, tp.P_SEX 性別, tp.P_BIRTHDAY_AD 生年月日 " +
                " from D_YOYAKU t, M_PATIENT tp " +
                " where t.YOYAKU_CODE = " + code1 +
                " and t.YOYAKU_DATE >= " + date1 + " and t.YOYAKU_DATE <= " + date2 +
                " and t.P_ID = tp.P_ID " +
                " order by t.YOYAKU_DATE, t.YOYAKU_TIME_S, t.YOYAKU_NO";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
            foreach (StdClass tmp in tmp_list)
            {
                RsvData obj = RsvData.GetFromStdClass(tmp);

                obj.Pat.Name = tmp.DataDict["氏名"].ToString().Trim();
                obj.Pat.Sex = tmp.DataDict["性別"].ToString();
                obj.Pat.Birth = tmp.DataDict["生年月日"].ToString();

                list.Add(obj);
            }

            return list;
        }

        /// <summary>
        /// 指定種別・指定日・指定時間帯の予約一覧を取得する
        /// </summary>
        /// <param name="code1"></param>
        /// <param name="code2"></param>
        /// <param name="rsv_date"></param>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <returns></returns>
        public static List<RsvData> GetListByDateTime(string code1, string code2, int rsv_date, int time1, int time2)
        {
            List<RsvData> list = new List<RsvData>();

            if (code1.Length == 0)
            {
                return list;
            }
            string cmd = "select t.*, Trim(tp.P_NAME) 氏名, tp.P_SEX 性別, tp.P_BIRTHDAY_AD 生年月日 " +
                " from D_YOYAKU t, M_PATIENT tp " +
                " where t.YOYAKU_CODE = " + code1 +
                " and t.YOYAKU_DATE = " + rsv_date +
                " and t.YOYAKU_TIME_S < " + time2 +
                " and t.YOYAKU_TIME_E > " + time1 +
                " and t.P_ID = tp.P_ID " +
                " order by t.YOYAKU_DATE, t.YOYAKU_TIME_S, t.YOYAKU_NO";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
            foreach (StdClass tmp in tmp_list)
            {
                RsvData obj = RsvData.GetFromStdClass(tmp);

                obj.Pat.Name = tmp.DataDict["氏名"].ToString().Trim();
                obj.Pat.Sex = tmp.DataDict["性別"].ToString();
                obj.Pat.Birth = tmp.DataDict["生年月日"].ToString();

                list.Add(obj);
            }

            return list;
        }

        /// <summary>
        /// 指定日の予約データ一覧を取得する
        /// </summary>
        /// <param name="crit_date"></param>
        /// <returns></returns>
        public static List<RsvData> GetListByDate(string crit_date)
        {
            List<RsvData> list = new List<RsvData>();

            if (crit_date.Length != 8)
            {
                return list;
            }
            string cmd = "select t1.*, m.P_NAME, m.P_SEX, m.P_BIRTHDAY_AD " +
                " from D_YOYAKU t1, M_PATIENT m " +
                " where t1.YOYAKU_DATE = " + crit_date +
                " and t1.P_ID = m.P_ID ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
            foreach (StdClass tmp in tmp_list)
            {
                RsvData obj = RsvData.GetFromStdClass(tmp);

                list.Add(obj);
            }

            return list;
        }

        /// <summary>
        /// 指定日・指定種別の予約データ一覧を取得する
        /// </summary>
        /// <param name="crit_date">指定日</param>
        /// <param name="code_list">指定種別リスト</param>
        /// <param name="pat_info">true: 患者情報（氏名, 生年月日, 性別）を取得する</param>
        /// <returns></returns>
        public static List<RsvData> GetListByDateCodes(string crit_date, List<string> code_list)
        {
            List<RsvData> list = new List<RsvData>();

            if (crit_date.Length != 8)
            {
                return list;
            }

            if (code_list.Count == 0 || AppString.ConcatList(code_list, ",").Length == 0)
            {
                return list;
            }
            string cmd = "select t1.*, m.P_NAME, m.P_SEX, m.P_BIRTHDAY_AD " +
                " from D_YOYAKU t1, M_PATIENT m " +
                " where t1.YOYAKU_DATE = " + crit_date +
                " and t1.YOYAKU_CODE in (" + AppString.ConcatList(code_list, ",") + ")" +
                " and t1.P_ID = m.P_ID " +
                " order by t1.P_ID, t1.YOYAKU_TIME_S, t1.YOYAKU_TIME_E";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
            foreach (StdClass tmp in tmp_list)
            {
                RsvData obj = RsvData.GetFromStdClass(tmp);

                list.Add(obj);
            }

            return list;
        }

        /// <summary>
        /// 指定患者の予約一覧を取得する
        /// </summary>
        /// <param name="pt_id"></param>
        /// <returns></returns>
        public static List<RsvData> GetListByPat(string pt_id)
        {
            List<RsvData> list = new List<RsvData>();

            if (pt_id.Length == 0)
            {
                return list;
            }
            string cmd = "select td.*, " +
                " (select tm.S_NAME from M_YOYAKU_NAME tm where tm.CODE = td.YOYAKU_CODE) YOYAKU_NAME " +
                " from D_YOYAKU td " +
                " where td.P_ID = " + pt_id +
                " order by td.YOYAKU_DATE desc, td.YOYAKU_TIME_S";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
            foreach (StdClass tmp in tmp_list)
            {
                RsvData obj = RsvData.GetFromStdClass(tmp);

                list.Add(obj);
            }

            return list;
        }

        /// <summary>
        /// 指定患者・指定期間の予約一覧を取得する
        /// </summary>
        /// <param name="pt_id">患者コード</param>
        /// <param name="date1">開始日</param>
        /// <param name="date2">終了日</param>
        /// <returns></returns>
        public static List<RsvData> GetListByPatDates(string pt_id, string date1, string date2)
        {
            List<RsvData> list = new List<RsvData>();

            if (pt_id.Length == 0)
            {
                return list;
            }
            string cmd = "select td.*, " +
                " (select tm.S_NAME from M_YOYAKU_NAME tm where tm.CODE = td.YOYAKU_CODE) YOYAKU_NAME " +
                " from D_YOYAKU td " +
                " where td.P_ID = " + pt_id +
                " and td.YOYAKU_DATE >= " + date1 + " and td.YOYAKU_DATE <= " + date2 +
                " order by td.YOYAKU_DATE, td.YOYAKU_CODE, td.YOYAKU_TIME_S";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
            foreach (StdClass tmp in tmp_list)
            {
                RsvData obj = RsvData.GetFromStdClass(tmp);

                list.Add(obj);
            }

            return list;
        }


        public StdReturn Insert()
        {
            StdReturn sr = new StdReturn();
            return sr;
        }

        public StdReturn Delete()
        {
            StdReturn sr = new StdReturn();
            return sr;
        }
    }
}
