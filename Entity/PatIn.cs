using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class PatIn : PatBase
    {
        /// <summary>
        /// 入院処理区分
        /// 10:入院予定, 11:入院確定, 13:部屋変更, 14:科変更, 15:医師変更, 18:退院予定, 19:退院確定
        /// </summary>
        public string Process = "";

        /// <summary>
        /// NYUIN_NO
        /// </summary>
        public int SEQ = 0;

        /// <summary>
        /// NYUIN_INDEX
        /// </summary>
        public int SEQ2 = 1;

        /// <summary>
        /// 入院日
        /// </summary>
        public string InDate = "";

        public string InDateString
        {
            get
            {
                return DateTimeAgent.DateFormat(InDate, DateTimeAgent.DateFormatKind.LONG);
            }
        }

        /// <summary>
        /// 入院時間帯
        /// </summary>
        public string InTime = "";

        /// <summary>
        /// 退院日
        /// </summary>
        public string OutDate = "";

        public string OutDateString
        {
            get
            {
                return DateTimeAgent.DateFormat(OutDate, DateTimeAgent.DateFormatKind.LONG);
            }
        }

        /// <summary>
        /// 退院時間帯
        /// </summary>
        public string OutTime = "";

        /// <summary>
        /// 適用日
        /// </summary>
        public string DoDate = "";

        /// <summary>
        /// 適用時間帯
        /// </summary>
        public string DoTime = "";

        /// <summary>
        /// 病棟コード（わかば 03, さくら 04, あやめ 05）
        /// </summary>
        public string Ward = "";

        /// <summary>
        /// 病室
        /// </summary>
        public string Room = "";

        /// <summary>
        /// ベッド
        /// </summary>
        public string Bed = "";

        /// <summary>
        /// 入院区分
        /// 1 通常, 2 転院, 9 救急
        /// </summary>
        public string InKind = "";

        /// <summary>
        /// 退院区分（転帰コード）
        /// 1 治癒, 2 死亡, 3 中止, 4 転医, 5 軽快, 6 転院, 7 一時退院, 8 不変
        /// </summary>
        public string OutKind = "";

        public override string ToString()
        {
            string s = "";

            if (this.InDate.Length > 0 && !this.InDate.Equals("0"))
            {
                s += this.InDateString + " ～ ";
            }

            if (this.OutDate.Length > 0 && !this.OutDate.Equals("0"))
            {
                s += this.OutDateString;
            }

            return s;
        }


        /// <summary>
        /// 面会状況
        /// 1: 面会謝絶, 2: 非表示
        /// </summary>
        public string UserInfo1 = "";

        public PatInStatus Status = PatInStatus.Now;


        public new static PatIn GetFromStdClass(StdClass tmp)
        {
            PatIn obj = new PatIn();
            obj.Id = tmp.GetDataString("P_ID");
            obj.Kana = tmp.GetDataString("P_KANA").Trim();
            obj.Name = tmp.GetDataString("P_NAME").Trim();
            obj.Sex = tmp.GetDataString("P_SEX");
            obj.Birth = tmp.GetDataString("P_BIRTHDAY_AD");

            obj.SEQ = tmp.GetDataInt("NYUIN_NO", 0);
            obj.SEQ2 = tmp.GetDataInt("NYUIN_INDEX", 0);
            obj.Process = tmp.GetDataString("PROCESS");
            obj.InDate = tmp.GetDataString("NYUIN_DATE").Length == 8 ? tmp.GetDataString("NYUIN_DATE") : tmp.GetDataString("NYUIN_PLAN_DATE").Length == 8 ? tmp.GetDataString("NYUIN_PLAN_DATE") : "0";
            obj.InTime = tmp.GetDataString("NYUIN_ZONE").Length > 0 && !tmp.GetDataString("NYUIN_ZONE").Equals("0") ? tmp.GetDataString("NYUIN_ZONE") : tmp.GetDataString("NYUIN_PLAN_ZONE");
            obj.OutDate = tmp.GetDataString("TAIIN_DATE").Length == 8 ? tmp.GetDataString("TAIIN_DATE") : tmp.GetDataString("TAIIN_PLAN_DATE").Length == 8 ? tmp.GetDataString("TAIIN_PLAN_DATE") : "0";
            obj.OutTime = tmp.GetDataString("TAIIN_ZONE").Length > 0 && !tmp.GetDataString("TAIIN_ZONE").Equals("0") ? tmp.GetDataString("TAIIN_ZONE") : tmp.GetDataString("TAIIN_PLAN_ZONE");
            obj.DoDate = tmp.GetDataString("APPLY_DATE");
            obj.DoTime = tmp.GetDataString("APPLY_ZONE");
            obj.Dept = tmp.GetDataString("DEPT");
            obj.Doctor = tmp.GetDataString("DR");
            obj.Ward = tmp.GetDataString("BYOTO").Trim();
            obj.Room = tmp.GetDataString("ROOM").Trim();
            obj.Bed = tmp.GetDataString("BED");
            obj.InKind = tmp.GetDataString("NYUIN_TYPE");
            obj.OutKind = tmp.GetDataString("TAIIN_TYPE");
            return obj;
        }

        /// <summary>
        /// 指定日時点の入院患者リストを取得する。
        /// </summary>
        /// <param name="adm_date">該当日（yyyyMMdd）。指定がなければ本日。</param>
        /// <param name="ward">病棟コード。指定がなければ "" で可</param>
        /// <param name="dept">診療科コード。指定がなければ "" で可</param>
        /// <returns></returns>
        public static List<PatIn> GetListByDate(string adm_date = "", string ward = "", string dept = "")
        {
            List<PatIn> list = new List<PatIn>();

            string today = DateTime.Now.ToString("yyyyMMdd");

            if (adm_date.Length != 8)
            {
                adm_date = today;
            }
            string cmd = "";
            List<StdClass> tmp_list;

            // 病棟・病室・科・医師の履歴を取得する（本日以外の場合）
            List<PatIn> seq_list = new List<PatIn>();

            if (adm_date.CompareTo(today) >= 0)
            {
                // 未来の場合
                // 現在の入院患者＋これから入院する患者　のリストを取得する

                // 現在の入院患者
                cmd = "select t.P_ID, t.NYUIN_NO, t.NYUIN_DATE, t.NYUIN_ZONE, t.TAIIN_PLAN_DATE, t.TAIIN_PLAN_ZONE, t.NYUIN_TYPE, t.TAIIN_TYPE, t.BYOTO, t.ROOM, t.BED, t.DEPT, t.DR, null process " +
                    " , tm.P_KANA, tm.P_NAME, tm.P_SEX, tm.P_BIRTHDAY_AD " +
                    " from D_NYUIN_NOW t, M_PATIENT tm " +
                    " where t.NYUIN_DATE > 0 and t.NYUIN_DATE <= " + today +
                    " and (t.TAIIN_DATE is null or t.TAIIN_DATE = 0)" +
                    " and t.P_ID = tm.P_ID ";

                tmp_list = StdClass.GetList(DB.Db3, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    PatIn obj = GetFromStdClass(tmp);
                    obj.Status = PatInStatus.Now;

                    list.Add(obj);

                    // 本日でない場合は履歴を取得する
                    if (!adm_date.Equals(today))
                    {
                        seq_list.Add(obj);
                    }
                }

                // これから入院する患者
                cmd = "select ttt.P_ID, ttt.NYUIN_NO, ttt.NYUIN_DATE, ttt.NYUIN_ZONE, ttt.TAIIN_PLAN_DATE, ttt.TAIIN_PLAN_ZONE, ttt.NYUIN_TYPE, ttt.TAIIN_TYPE, ttt.BYOTO, ttt.ROOM, ttt.BED, ttt.DEPT, ttt.DR, ttt.PROCESS " +
                    " , tm.P_KANA, tm.P_NAME, tm.P_SEX, tm.P_BIRTHDAY_AD " +
                    " from (select * from " +
                    " (select row_number() over (partition by P_ID, NYUIN_NO order by PROCESS desc, NYUIN_INDEX desc) rn" +
                    "  , t.P_ID, t.NYUIN_NO " +
                    "  , case when t.NYUIN_DATE > 0 then t.NYUIN_DATE else t.NYUIN_PLAN_DATE end nyuin_date" +
                    "  , case when t.NYUIN_ZONE > 0 then t.NYUIN_ZONE else t.NYUIN_PLAN_ZONE end nyuin_zone" +
                    "  , t.TAIIN_PLAN_DATE, t.TAIIN_PLAN_ZONE, t.NYUIN_TYPE, t.TAIIN_TYPE, t.BYOTO, t.ROOM, t.BED, t.DEPT, t.DR, t.PROCESS" +
                    "  from D_NYUIN t " +
                    "  where t.PROCESS in (10,18) and (t.DEL_FLG is null or t.DEL_FLG = 0) and (t.NYUIN_PLAN_DATE >= " + today + " or t.NYUIN_DATE >= " + today + ")) tt " +
                    "  where tt.RN = 1) ttt, M_PATIENT tm " +
                    " where ttt.NYUIN_DATE <= " + adm_date +
                    " and ttt.P_ID = tm.P_ID";

                tmp_list = StdClass.GetList(DB.Db3, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    PatIn obj = GetFromStdClass(tmp);

                    // 入院予定
                    bool b = false;

                    foreach (PatIn p in list)
                    {
                        // 入院中リストにある場合はリストに追加しない
                        if (obj.Id.Equals(p.Id) && obj.SEQ.Equals(p.SEQ))
                        {
                            b = true;
                            break;
                        }
                    }

                    // 入院中リストにない場合（＝入院予定のみ）はリストに追加する
                    if (!b)
                    {
                        obj.Status = PatInStatus.Yet;
                        list.Add(obj);
                    }
                }
            }
            else if (adm_date.CompareTo(today) < 0)
            {
                // 過去の場合
                // すでに退院した患者＋現在の入院患者　のリストを取得する

                // すでに退院した患者
                cmd = "select t.P_ID, t.NYUIN_NO, t.NYUIN_DATE, t.NYUIN_ZONE, t.TAIIN_DATE, t.TAIIN_ZONE, t.NYUIN_TYPE, t.TAIIN_TYPE, t.BYOTO, t.ROOM, t.BED, t.DEPT, t.DR, t.PROCESS " +
                    " , tm.P_KANA, tm.P_NAME, tm.P_SEX, tm.P_BIRTHDAY_AD " +
                    " from D_NYUIN t, M_PATIENT tm " +
                    " where t.PROCESS in (19) and t.NYUIN_DATE <= " + adm_date + " and t.TAIIN_DATE >= " + adm_date + " and (t.DEL_FLG is null or t.DEL_FLG = 0) and t.P_ID = tm.P_ID";

                // 現在の入院患者
                cmd += " union " +
                    "select t.P_ID, t.NYUIN_NO, t.NYUIN_DATE, t.NYUIN_ZONE, t.TAIIN_PLAN_DATE, t.TAIIN_PLAN_ZONE, t.NYUIN_TYPE, t.TAIIN_TYPE, t.BYOTO, t.ROOM, t.BED, t.DEPT, t.DR, null process " +
                    " , tm.P_KANA, tm.P_NAME, tm.P_SEX, tm.P_BIRTHDAY_AD " +
                    " from D_NYUIN_NOW t, M_PATIENT tm " +
                    " where t.NYUIN_DATE > 0 and t.NYUIN_DATE <= " + adm_date + " and (t.TAIIN_DATE is null or t.TAIIN_DATE = 0) and t.P_ID = tm.P_ID";

                tmp_list = StdClass.GetList(DB.Db3, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    PatIn obj = GetFromStdClass(tmp);

                    if (obj.Process.Equals("19"))
                    {
                        obj.Status = PatInStatus.Done;
                    }
                    else
                    {
                        obj.Status = PatInStatus.Now;
                    }

                    list.Add(obj);

                    PatIn obj2 = new PatIn();

                    obj2.Id = tmp.GetDataString("P_ID");
                    obj2.SEQ = tmp.GetDataInt("NYUIN_NO");

                    seq_list.Add(obj2);
                }
            }

            // 本日でない場合は履歴を取得する
            if (!adm_date.Equals(today))
            {
                List<PatIn> list1 = PatIn.GetRoomList(seq_list, adm_date);
                List<PatIn> list2 = PatIn.GetDeptList(seq_list, adm_date);
                List<PatIn> list3 = PatIn.GetDoctorList(seq_list, adm_date);

                foreach (PatIn obj in list)
                {
                    foreach (PatIn obj2 in list1)
                    {
                        if (obj.Id.Equals(obj2.Id))
                        {
                            obj.Ward = obj2.Ward;
                            obj.Room = obj2.Room;
                            break;
                        }
                    }

                    foreach (PatIn obj2 in list2)
                    {
                        if (obj.Id.Equals(obj2.Id))
                        {
                            obj.Dept = obj2.Dept;
                            break;
                        }
                    }

                    foreach (PatIn obj2 in list3)
                    {
                        if (obj.Id.Equals(obj2.Id))
                        {
                            obj.Doctor = obj2.Doctor;
                            break;
                        }
                    }
                }
            }
            // 部屋順に並べかえる
            list.Sort((x, y) =>
            {
                return x.Room.CompareTo(y.Room);
            });

            // 条件に合致したものを返す
            return list.FindAll((x) => {
                bool b = true;

                if (ward.Length > 0 && !x.Ward.Equals(ward)) b = false;
                if (dept.Length > 0 && !x.Dept.Equals(dept)) b = false;

                return b;
            });
        }

        /// <summary>
        /// 指定した患者＋入院番号の、指定日時点での科を取得する
        /// </summary>
        /// <param name="seq_list"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<PatIn> GetDeptList(List<PatIn> seq_list, string date)
        {
            return GetListAt(seq_list, date, "11, 14, 19", "t.DEPT is not null and t.DEPT > 0");
        }

        /// <summary>
        /// 指定した（患者, 入院番号）の組み合わせの、科の移動歴を取得する
        /// </summary>
        /// <param name="seq_list"></param>
        /// <param name="pat_info">true: 患者情報を取得する</param>
        /// <returns></returns>
        public static List<PatIn> GetDeptList(List<PatIn> seq_list, bool pat_info = true)
        {
            return GetMoveList(seq_list, pat_info, "11, 14, 19", "td.DEPT", (x, y) => x.Dept.Equals(y.Dept));
        }

        /// <summary>
        /// 指定した患者＋入院番号の、指定日時点での医師を取得する
        /// </summary>
        /// <param name="seq_list"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<PatIn> GetDoctorList(List<PatIn> seq_list, string date)
        {
            return GetListAt(seq_list, date, "11, 15, 19", "t.DR is not null and t.DR > 0");
        }

        /// <summary>
        /// 指定した（患者, 入院番号）の組み合わせの、病室の移動歴を取得する
        /// </summary>
        /// <param name="seq_list"></param>
        /// <param name="pat_info">true: 患者情報を取得する</param>
        /// <returns></returns>
        public static List<PatIn> GetDoctorList(List<PatIn> seq_list, bool pat_info = true)
        {
            return GetMoveList(seq_list, pat_info, "11, 15, 19", "td.DR", (x, y) => x.Doctor.Equals(y.Doctor));
        }

        /// <summary>
        /// 指定した（患者, 入院番号）の組み合わせの、指定日時点での病棟・病室を取得する
        /// </summary>
        /// <param name="seq_list"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<PatIn> GetRoomList(List<PatIn> seq_list, string date)
        {
            return GetListAt(seq_list, date, "11, 13, 19", "t.ROOM is not null and trim(t.ROOM) != '0'");
        }


        /// <summary>
        /// 指定した（患者, 入院番号）の組み合わせの、病室の移動歴を取得する
        /// </summary>
        /// <param name="seq_list"></param>
        /// <param name="pat_info">true: 患者情報を取得する</param>
        /// <returns></returns>
        public static List<PatIn> GetRoomList(List<PatIn> seq_list, bool pat_info = true)
        {
            return GetMoveList(seq_list, pat_info, "11, 13, 19", "td.BYOTO, td.ROOM, td.BED", (x, y) => x.Room.Equals(y.Room));
        }

        /// <summary>
        /// （患者, 入院番号）の組み合わせを IN 句用の "(id,seq),(id,seq)" にする
        /// </summary>
        static string SeqInList(List<PatIn> seq_list)
        {
            return AppString.ConcatList(seq_list.ConvertAll((x) => { return "(" + x.Id + "," + x.SEQ + ")"; }), ",");
        }

        /// <summary>
        /// GetDeptList / GetDoctorList / GetRoomList の指定日時点版の共通部分
        /// </summary>
        /// <param name="seq_list"></param>
        /// <param name="date"></param>
        /// <param name="process">対象の PROCESS（"11, 14, 19" など）</param>
        /// <param name="not_empty">対象の列に値があることの条件</param>
        /// <returns></returns>
        static List<PatIn> GetListAt(List<PatIn> seq_list, string date, string process, string not_empty)
        {
            List<PatIn> list = new List<PatIn>();

            string ss = SeqInList(seq_list);

            if (ss.Length == 0 || !DateTimeAgent.IsDate(date))
            {
                return list;
            }

            string cmd = "select * from " +
                " (select row_number() over (partition by P_ID, NYUIN_NO order by APPLY_DATE desc, APPLY_ZONE desc, NYUIN_INDEX desc) rn, t.* " +
                "  from D_NYUIN t " +
                "  where t.PROCESS in (" + process + ") and (t.DEL_FLG is null or t.DEL_FLG = 0) " +
                "  and (" + not_empty + ") " +
                "  and (t.P_ID, t.NYUIN_NO) in (" + ss + ") " +
                "  and t.APPLY_DATE <= " + date + ") tt " +
                " where tt.RN = 1";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(PatIn.GetFromStdClass(tmp));
            }

            return list;
        }

        /// <summary>
        /// GetDeptList / GetDoctorList / GetRoomList の移動歴版の共通部分
        /// </summary>
        /// <param name="seq_list"></param>
        /// <param name="pat_info">true: 患者情報を取得する</param>
        /// <param name="process">対象の PROCESS（"11, 14, 19" など）</param>
        /// <param name="cols">取得する D_NYUIN の列</param>
        /// <param name="same">直前の行と同じ値かどうか</param>
        /// <returns></returns>
        static List<PatIn> GetMoveList(List<PatIn> seq_list, bool pat_info, string process, string cols, Func<PatIn, PatIn, bool> same)
        {
            List<PatIn> list = new List<PatIn>();

            // 現在または過去の入院から取得
            string ss = SeqInList(seq_list.FindAll((x) =>
            {
                return (x.Status == PatInStatus.Done || x.Status == PatInStatus.Now);
            }));

            if (ss.Length > 0)
            {
                string pat_cols = pat_info ? ", tm.P_KANA, tm.P_NAME, tm.P_SEX, tm.P_BIRTHDAY_AD " : "";
                string pat_from = pat_info ? "M_PATIENT tm, " : "";
                string pat_where = pat_info ? " and td.P_ID = tm.P_ID " : "";

                string cmd = "select tt.P_ID, tt.NYUIN_NO, tt.NYUIN_DATE, tt.NYUIN_ZONE, tt.TAIIN_DATE, tt.TAIIN_ZONE " +
                    ", td.APPLY_DATE, td.APPLY_ZONE, td.NYUIN_INDEX, " + cols + " " +
                    pat_cols +
                    " from D_NYUIN td, " + pat_from +
                    " (select t.P_ID, t.NYUIN_NO, t.NYUIN_DATE, t.NYUIN_ZONE, t.TAIIN_DATE, t.TAIIN_ZONE from D_NYUIN t " +
                    "  where (t.P_ID, t.NYUIN_NO) in (" + ss + ") and t.PROCESS in (19) and (t.DEL_FLG is null or t.DEL_FLG = 0) " +
                    "  union " +
                    "  select t.P_ID, t.NYUIN_NO, t.NYUIN_DATE, t.NYUIN_ZONE, t.TAIIN_PLAN_DATE, t.TAIIN_PLAN_ZONE from D_NYUIN_NOW t " +
                    "  where (t.P_ID, t.NYUIN_NO) in (" + ss + ") and (t.TAIIN_DATE is null or t.TAIIN_DATE = 0)) tt " +
                    " where td.PROCESS in (" + process + ") and (td.DEL_FLG is null or td.DEL_FLG = 0) " +
                    " and td.P_ID = tt.P_ID and td.NYUIN_NO = tt.NYUIN_NO " +
                    pat_where +
                    " order by tt.P_ID, tt.NYUIN_NO, td.APPLY_DATE, td.APPLY_ZONE, td.NYUIN_INDEX";

                List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    PatIn obj = PatIn.GetFromStdClass(tmp);

                    // 同一患者・同一入院で、直前と同じ値なら飛ばす
                    if (list.Count > 0)
                    {
                        PatIn obj2 = list[list.Count - 1];

                        if (obj2.Id.Equals(obj.Id) &&
                            obj2.SEQ.Equals(obj.SEQ) &&
                            same(obj2, obj))
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }
            }

            // 未来入院があれば足す
            foreach (PatIn pin in seq_list)
            {
                if (pin.Status == PatInStatus.Yet)
                {
                    list.Add(pin);
                }
            }

            return list;
        }
    }

    public enum PatInStatus : int
    {
        Yet = 1,
        Now = 2,
        Done = 3
    }

}
