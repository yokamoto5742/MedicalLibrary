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

#if INNO
            s += DateTimeAgent.TimeFormat6(this.Time1, 4, false);
            s += delimiter;
            s += DateTimeAgent.TimeFormat6(this.Time2, 4, false);
#else
            s += DateTimeAgent.TimeFormat(this.Time1, false);
            s += delimiter;
            s += DateTimeAgent.TimeFormat(this.Time2, false);
#endif

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
#if INNO
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
#else
            obj.Code1 = tmp.DataDict["予約種別コード"].ToString();
            obj.Code2 = tmp.DataDict["予約詳細コード"].ToString();
            obj.Pat.Id = tmp.DataDict["患者コード"].ToString();
            obj.Cont1 = tmp.DataDict["備考１"].ToString().TrimEnd();
            obj.Cont2 = tmp.DataDict["備考２"].ToString().TrimEnd();
            int.TryParse(tmp.DataDict["予約年月日"].ToString(), out obj.RsvDate);
            int.TryParse(tmp.DataDict["予約開始時間"].ToString(), out obj.Time1);
            int.TryParse(tmp.DataDict["予約終了時間"].ToString(), out obj.Time2);
            long.TryParse(tmp.DataDict["連番"].ToString(), out obj.SEQ);

            if (tmp.DataDict.ContainsKey("予約種別"))
            {
                obj.Name1 = tmp.DataDict["予約種別"].ToString().Trim();
            }

            if (tmp.DataDict.ContainsKey("予約詳細"))
            {
                obj.Name2 = tmp.DataDict["予約詳細"].ToString().Trim();
            }

            obj.Staff = tmp.GetDataString("入力者コード");

            obj.Pat.Name = tmp.GetDataString("IM01RC_F04").Trim();
            obj.Pat.Sex = tmp.GetDataString("IM01RC_F05");
            obj.Pat.Birth = tmp.GetDataString("IM01RC_F10");
#endif
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
#if INNO
            string cmd = "select t.*, Trim(tp.P_NAME) 氏名, tp.P_SEX 性別, tp.P_BIRTHDAY_AD 生年月日 " +
                " from D_YOYAKU t, M_PATIENT tp " +
                " where t.YOYAKU_CODE = " + code1 +
                " and t.YOYAKU_DATE >= " + date1 + " and t.YOYAKU_DATE <= " + date2 +
                " and t.P_ID = tp.P_ID " +
                " order by t.YOYAKU_DATE, t.YOYAKU_TIME_S, t.YOYAKU_NO";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
#else
            string cmd = "select t.*, Trim(tp.IM01RC_F04) 氏名, tp.IM01RC_F05 性別, tp.IM01RC_F10 生年月日 " +
                " from macs.予約データ t, macs.IM01RC tp " +
                " where t.予約種別コード = " + code1;

            if (code2.Length > 0)
            {
                cmd += " and t.予約詳細コード = " + code2;
            }
            else
            {
                cmd += " and t.予約詳細コード = 0";
            }

            cmd += " and t.予約年月日 >= " + date1 + " and t.予約年月日 <= " + date2 +
                " and t.患者コード = tp.IM01RC_F01 " +
                " order by t.予約年月日, t.予約開始時間, t.連番";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif
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
#if INNO
            string cmd = "select t.*, Trim(tp.P_NAME) 氏名, tp.P_SEX 性別, tp.P_BIRTHDAY_AD 生年月日 " +
                " from D_YOYAKU t, M_PATIENT tp " +
                " where t.YOYAKU_CODE = " + code1 +
                " and t.YOYAKU_DATE >= " + date1 + " and t.YOYAKU_DATE <= " + date2 +
                " and t.P_ID = tp.P_ID " +
                " order by t.YOYAKU_DATE, t.YOYAKU_TIME_S, t.YOYAKU_NO";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
#else
            string cmd = "select t.*, Trim(tp.IM01RC_F04) 氏名, tp.IM01RC_F05 性別, tp.IM01RC_F10 生年月日 " +
                " from macs.予約データ t, macs.IM01RC tp " +
                " where t.予約種別コード = " + code1;

            if (code2.Length > 0)
            {
                cmd += " and t.予約詳細コード = " + code2;
            }
            else
            {
                cmd += " and t.予約詳細コード = 0";
            }

            cmd += " and t.予約年月日 >= " + date1 + " and t.予約年月日 <= " + date2 +
                " and t.患者コード = tp.IM01RC_F01 " +
                " order by t.予約年月日, t.予約開始時間, t.連番";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif
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
#if INNO
            string cmd = "select t.*, Trim(tp.P_NAME) 氏名, tp.P_SEX 性別, tp.P_BIRTHDAY_AD 生年月日 " +
                " from D_YOYAKU t, M_PATIENT tp " +
                " where t.YOYAKU_CODE = " + code1 +
                " and t.YOYAKU_DATE = " + rsv_date +
                " and t.YOYAKU_TIME_S < " + time2 +
                " and t.YOYAKU_TIME_E > " + time1 +
                " and t.P_ID = tp.P_ID " +
                " order by t.YOYAKU_DATE, t.YOYAKU_TIME_S, t.YOYAKU_NO";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
#else
            string cmd = "select t.*, Trim(tp.IM01RC_F04) 氏名, tp.IM01RC_F05 性別, tp.IM01RC_F10 生年月日 " +
                " from macs.予約データ t, macs.IM01RC tp " +
                " where t.予約種別コード = " + code1;

            if (code2.Length > 0)
            {
                cmd += " and t.予約詳細コード = " + code2;
            }
            else
            {
                cmd += " and t.予約詳細コード = 0";
            }

            cmd += " and t.予約年月日 = " + rsv_date +
                " and t.予約開始時間 < " + time2 +
                " and t.予約終了時間 > " + time1 +
                " and t.患者コード = tp.IM01RC_F01 " +
                " order by t.予約年月日, t.予約開始時間, t.連番";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif
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
#if INNO
            string cmd = "select t1.*, m.P_NAME, m.P_SEX, m.P_BIRTHDAY_AD " +
                " from D_YOYAKU t1, M_PATIENT m " +
                " where t1.YOYAKU_DATE = " + crit_date +
                " and t1.P_ID = m.P_ID ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
#else
            string cmd = "select t1.*, m.IM01RC_F04, m.IM01RC_F05, m.IM01RC_F10 " +
                " from macs.予約データ t1, macs.IM01RC m " +
                " where t1.予約年月日 = " + crit_date +
                " and t1.患者コード = m.IM01RC_F01 ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif
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
#if INNO
            string cmd = "select t1.*, m.P_NAME, m.P_SEX, m.P_BIRTHDAY_AD " +
                " from D_YOYAKU t1, M_PATIENT m " +
                " where t1.YOYAKU_DATE = " + crit_date +
                " and t1.YOYAKU_CODE in (" + AppString.ConcatList(code_list, ",") + ")" +
                " and t1.P_ID = m.P_ID " +
                " order by t1.P_ID, t1.YOYAKU_TIME_S, t1.YOYAKU_TIME_E";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
#else
            string cmd = "select t1.*, m.IM01RC_F04, m.IM01RC_F05, m.IM01RC_F10 " +
                " from macs.予約データ t1, macs.IM01RC m " +
                " where t1.予約年月日 = " + crit_date +
                " and (t1.予約種別コード in (" + AppString.ConcatList(code_list, ",") + ") or t1.予約詳細コード in (" + AppString.ConcatList(code_list, ",") + "))" +
                " and t1.患者コード = m.IM01RC_F01 " +
                " order by t1.患者コード, t1.予約開始時間, t1.予約終了時間";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif
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
#if INNO
            string cmd = "select td.*, " +
                " (select tm.S_NAME from M_YOYAKU_NAME tm where tm.CODE = td.YOYAKU_CODE) YOYAKU_NAME " +
                " from D_YOYAKU td " +
                " where td.P_ID = " + pt_id +
                " order by td.YOYAKU_DATE desc, td.YOYAKU_TIME_S";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
#else
            string cmd = "select td.*, " +
                " (select tm.TM50RC_F04 from macs.TM50RC tm where tm.TM50RC_F01 = 50 and tm.TM50RC_F02 = td.予約種別コード) 予約種別, " +
                " case when td.予約詳細コード > 0 then (select tm.TM50RC_F04 from macs.TM50RC tm where tm.TM50RC_F01 = 51 and tm.TM50RC_F02 = td.予約詳細コード) else null end 予約詳細 " +
                " from macs.予約データ td " +
                " where td.患者コード = " + pt_id +
                " order by td.予約年月日 desc, td.予約開始時間";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif
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
#if INNO
            string cmd = "select td.*, " +
                " (select tm.S_NAME from M_YOYAKU_NAME tm where tm.CODE = td.YOYAKU_CODE) YOYAKU_NAME " +
                " from D_YOYAKU td " +
                " where td.P_ID = " + pt_id +
                " and td.YOYAKU_DATE >= " + date1 + " and td.YOYAKU_DATE <= " + date2 +
                " order by td.YOYAKU_DATE, td.YOYAKU_CODE, td.YOYAKU_TIME_S";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
#else
            string cmd = "select td.*, " +
                " (select tm.TM50RC_F04 from macs.TM50RC tm where tm.TM50RC_F01 = 50 and tm.TM50RC_F02 = td.予約種別コード) 予約種別, " +
                " case when td.予約詳細コード > 0 then (select tm.TM50RC_F04 from macs.TM50RC tm where tm.TM50RC_F01 = 51 and tm.TM50RC_F02 = td.予約詳細コード) else null end 予約詳細 " +
                " from macs.予約データ td " +
                " where td.患者コード = " + pt_id +
                " and td.予約年月日 >= " + date1 + " and td.予約年月日 <= " + date2 +
                " order by td.予約年月日, td.予約種別コード, td.予約開始時間";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif
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
#if INNO
#else
            int yyMMdd = int.Parse(DateTime.Now.ToString("yyMMdd"));

            // その日の最初の予約かどうか
            bool first_flg = false;


            // 連番を取得する
            long num = 0;

            string cmd = "select 連番 from ＮＴ連番ファイル t " +
                " where t.区分 = 2 and t.年月日 = " + yyMMdd;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                long.TryParse(tmp.DataDict["連番"].ToString(), out num);
                break;
            }

            if (num == 0)
            {
                num = yyMMdd * 100000000 + 1;
                first_flg = true;
            }

            StdDbClass obj = new StdDbClass();

            // 予約を挿入する
            obj.Db = DB.Db1;
            obj.Table = "予約データ";

            obj.DataList.Add(new StdDbColumn("予約種別コード", StdDbType.NUMBER, this.Code1));
            obj.DataList.Add(new StdDbColumn("予約年月日", StdDbType.NUMBER, this.RsvDate));
            obj.DataList.Add(new StdDbColumn("予約開始時間", StdDbType.NUMBER, this.Time1));
            obj.DataList.Add(new StdDbColumn("患者コード", StdDbType.NUMBER, this.Pat.Id));
            obj.DataList.Add(new StdDbColumn("予約終了時間", StdDbType.NUMBER, this.Time2));

            obj.DataList.Add(new StdDbColumn("備考１", StdDbType.VARCHAR2, this.Cont1));
            obj.DataList.Add(new StdDbColumn("備考２", StdDbType.VARCHAR2, this.Cont2));

            obj.DataList.Add(new StdDbColumn("入力者コード", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("予約詳細コード", StdDbType.NUMBER, (this.Code2.Length > 0) ? this.Code2 : "0"));
            obj.DataList.Add(new StdDbColumn("特別枠", StdDbType.NUMBER, 1));
            obj.DataList.Add(new StdDbColumn("連番", StdDbType.NUMBER, num));

            sr = obj.InsertSQL();


            // 連番をインクリメントする
            obj.Table = "ＮＴ連番ファイル";

            num++;
            obj.DataList.Clear();

            if (first_flg)
            {
                obj.DataList.Add(new StdDbColumn("区分", StdDbType.NUMBER, 2));
                obj.DataList.Add(new StdDbColumn("科コード", StdDbType.NUMBER, 0));
                obj.DataList.Add(new StdDbColumn("年月日", StdDbType.NUMBER, yyMMdd));
                obj.DataList.Add(new StdDbColumn("連番", StdDbType.NUMBER, num));

                sr = obj.InsertSQL();
            }
            else
            {
                obj.DataList.Add(new StdDbColumn("連番", StdDbType.NUMBER, num));

                obj.WhereList.Add("区分 = 2");
                obj.WhereList.Add("科コード = 0");
                obj.WhereList.Add("年月日 = " + yyMMdd);

                sr = obj.UpdateSQL();
            }
#endif
            return sr;
        }

        public StdReturn Delete()
        {
            StdReturn sr = new StdReturn();
#if INNO
#else
            StdDbClass obj = new StdDbClass();

            obj.Db = DB.Db1;
            obj.Table = "予約データ";

            obj.WhereList.Add("予約種別コード = " + this.Code1);
            obj.WhereList.Add("予約年月日 = " + this.RsvDate);
            obj.WhereList.Add("予約開始時間 = " + this.Time1);
            obj.WhereList.Add("患者コード = " + this.Pat.Id);

            sr = obj.DeleteSQL();
#endif
            return sr;
        }
    }
}
