using System;
using System.Collections.Generic;
using System.Text;
//using Oracle.DataAccess.Client;
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

        public int InDateInt
        {
            get
            {
                int d = 0;
                int.TryParse(this.InDate, out d);

                return d;
            }
        }

        public string InDateString
        {
            get
            {
                return DateTimeAgent.DateFormat(InDate, DateTimeAgent.DateFormatKind.LONG);
            }
        }

        public string InDateStringShort
        {
            get
            {
                return DateTimeAgent.DateFormat(InDate, DateTimeAgent.DateFormatKind.SHORT);
            }
        }

        public DateTime InDateValue
        {
            get
            {
                DateTime dt = DateTime.Now;

                DateTime.TryParse(this.InDateString, out dt);

                return dt;
            }
        }

        /// <summary>
        /// 入院時間帯
        /// </summary>
        public string InTime = "";

        public string InTimeString
        {
            get
            {
                string s = "";
#if INNO
                if (this.InTime.Equals("1"))
                {
                    s = "朝";
                }
                else if (this.InTime.Equals("2"))
                {
                    s = "昼";
                }
                else if (this.InTime.Equals("3"))
                {
                    s = "夕";
                }
                else if (this.InTime.Equals("9"))
                {
                    s = "未定";
                }
#else
                if (this.InTime.Equals("1"))
                {
                    s = "午前";
                }
                else if (this.InTime.Equals("2"))
                {
                    s = "午後";
                }
                else if (this.InTime.Equals("3"))
                {
                    s = "未定";
                }
#endif
                return s;
            }
        }

        /// <summary>
        /// 退院日
        /// </summary>
        public string OutDate = "";

        public int OutDateInt
        {
            get
            {
                int d = 0;
                int.TryParse(this.OutDate, out d);

                return d;
            }
        }

        public string OutDateString
        {
            get
            {
                return DateTimeAgent.DateFormat(OutDate, DateTimeAgent.DateFormatKind.LONG);
            }
        }

        public string OutDateStringShort
        {
            get
            {
                return DateTimeAgent.DateFormat(OutDate, DateTimeAgent.DateFormatKind.SHORT);
            }
        }

        public DateTime OutDateValue
        {
            get
            {
                DateTime dt = DateTime.Now;

                DateTime.TryParse(this.OutDateString, out dt);

                return dt;
            }
        }

        /// <summary>
        /// 退院時間帯
        /// </summary>
        public string OutTime = "";

        public string OutTimeString
        {
            get
            {
                string s = "";
#if INNO
                if (this.OutTime.Equals("1"))
                {
                    s = "朝";
                }
                else if (this.OutTime.Equals("2"))
                {
                    s = "昼";
                }
                else if (this.OutTime.Equals("3"))
                {
                    s = "夕";
                }
                else if (this.OutTime.Equals("9"))
                {
                    s = "未定";
                }
#else
                if (this.OutTime.Equals("1"))
                {
                    s = "午前";
                }
                else if (this.OutTime.Equals("2"))
                {
                    s = "午後";
                }
                else if (this.OutTime.Equals("3"))
                {
                    s = "未定";
                }
#endif
                return s;
            }
        }

        /// <summary>
        /// 適用日
        /// </summary>
        public string DoDate = "";

        public int DoDateInt
        {
            get
            {
                int d = 0;
                int.TryParse(this.DoDate, out d);

                return d;
            }
        }

        public string DoDateString
        {
            get
            {
                return DateTimeAgent.DateFormat(DoDate, DateTimeAgent.DateFormatKind.LONG);
            }
        }

        public string DoDateStringShort
        {
            get
            {
                return DateTimeAgent.DateFormat(DoDate, DateTimeAgent.DateFormatKind.SHORT);
            }
        }

        public DateTime DoDateValue
        {
            get
            {
                DateTime dt = DateTime.Now;

                DateTime.TryParse(this.DoDateString, out dt);

                return dt;
            }
        }

        /// <summary>
        /// 適用時間帯
        /// </summary>
        public string DoTime = "";

        public string DoTimeString
        {
            get
            {
                string s = "";
#if INNO
                if (this.DoTime.Equals("1"))
                {
                    s = "朝";
                }
                else if (this.DoTime.Equals("2"))
                {
                    s = "昼";
                }
                else if (this.DoTime.Equals("3"))
                {
                    s = "夕";
                }
                else if (this.DoTime.Equals("9"))
                {
                    s = "未定";
                }
#else
                if (this.DoTime.Equals("1"))
                {
                    s = "午前";
                }
                else if (this.DoTime.Equals("2"))
                {
                    s = "午後";
                }
                else if (this.DoTime.Equals("3"))
                {
                    s = "未定";
                }
#endif
                return s;
            }
        }

        /// <summary>
        /// 病棟コード（わかば 03, さくら 04, あやめ 05）
        /// </summary>
        public string Ward = "";

        /// <summary>
        /// 病棟
        /// </summary>
        public string WardName
        {
            get
            {
                string result = "";

                if (Dict.WardDict.ContainsKey(this.Ward))
                {
                    result = Dict.WardDict[Ward].Name;
                }

                return result;
            }
        }

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

        public string InKindString
        {
            get
            {
                string s = "";

                if (this.InKind.Equals("1"))
                {
                    s = "通常";
                }
                else if (this.InKind.Equals("2"))
                {
                    s = "転院";
                }
                else if (this.InKind.Equals("9"))
                {
                    s = "救急";
                }

                return s;
            }
        }

        /// <summary>
        /// 退院区分（転帰コード）
        /// 1 治癒, 2 死亡, 3 中止, 4 転医, 5 軽快, 6 転院, 7 一時退院, 8 不変
        /// </summary>
        public string OutKind = "";

        public string OutKindString
        {
            get
            {
                string s = "";

                if (this.OutKind.Equals("1"))
                {
                    s = "治癒";
                }
                else if (this.OutKind.Equals("2"))
                {
                    s = "死亡";
                }
                else if (this.OutKind.Equals("3"))
                {
                    s = "中止";
                }
                else if (this.OutKind.Equals("4"))
                {
                    s = "転医";
                }
                else if (this.OutKind.Equals("5"))
                {
                    s = "軽快";
                }
                else if (this.OutKind.Equals("6"))
                {
                    s = "転院";
                }
                else if (this.OutKind.Equals("7"))
                {
                    s = "一時退院";
                }
                else if (this.OutKind.Equals("8"))
                {
                    s = "不変";
                }

                return s;
            }
        }

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


        public string InfoIn1
        {
            get
            {
                string result = "";

                result += "ID：" + Id + "\r\n" + Info1 + "\r\n";
                result += "入院日　" + InDateString + "\r\n";
                result += "主治医　" + DeptName + "　" + DoctorName + "\r\n";
                result += WardName + "　" + Room;

                return result;
            }
        }

        /// <summary>
        /// 面会状況
        /// 1: 面会謝絶, 2: 非表示
        /// </summary>
        public string UserInfo1 = "";

        public string UserInfoString1
        {
            get
            {
                string s = "";

                if (this.UserInfo1.Equals("1"))
                {
                    s = "面会謝絶";
                }
                else if (this.UserInfo1.Equals("2"))
                {
                    s = "非表示";
                }

                return s;
            }
        }

        public PatInStatus Status = PatInStatus.Now;

        /// <summary>
        /// 入院日数
        /// </summary>
        public int Days
        {
            get
            {
                int i = 0;

                if (AppString.IsDate(this.InDate))
                {
                    if (AppString.IsDate(this.OutDate))
                    {
                        // 退院日がある場合
                        i = DateTimeAgent.IntervalDays(this.InDate, this.OutDate) + 1;
                    }
                    else
                    {
                        // 退院日が無い場合
                        i = DateTimeAgent.IntervalDays(this.InDate, DateTime.Now.ToString("yyyyMMdd")) + 1;
                    }
                }

                if (i < 0) i = 0;

                return i;
            }
        }

        /// <summary>
        /// 前回退院日からの日数
        /// （２回目以降の入院）
        /// </summary>
        public int IntervalDays = 0;


        public new static PatIn GetFromStdClass(StdClass tmp)
        {
            PatIn obj = new PatIn();
#if INNO
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
#else
            obj.Id = tmp.GetDataString("IM01RC_F01");
            obj.Kana = tmp.GetDataString("IM01RC_F03").Trim();
            obj.Name = tmp.GetDataString("IM01RC_F04").Trim();
            obj.Sex = tmp.GetDataString("IM01RC_F05");
            obj.Birth = tmp.GetDataString("IM01RC_F10");

            if (tmp.GetDataString("IM21RC_F03").Length > 0)
            {
                // IM2... テーブルから取得する場合
                obj.InDate = tmp.GetDataString("IM21RC_F03");
                obj.OutDate = tmp.GetDataString("IM21RC_F04");
                obj.Dept = tmp.GetDataString("IM22RC_F05");
                obj.Doctor = tmp.GetDataString("IM23RC_F05");
                obj.Ward = tmp.GetDataString("IM24RC_F05").Trim();
                obj.Room = tmp.GetDataString("IM24RC_F06").Trim();
                obj.InKind = tmp.GetDataString("IM21RC_F05");
                obj.OutKind = tmp.GetDataString("IM21RC_F06");
            }
            else
            {
                // ADT_入退院予定データから取得する場合
                obj.InDate = tmp.GetDataString("入院予定日");
                obj.InTime = tmp.GetDataString("入院予定時間");
                obj.OutDate = tmp.GetDataString("退院予定日");
                obj.OutTime = tmp.GetDataString("退院予定時間");
                obj.Dept = tmp.GetDataString("科コード");
                obj.Doctor = tmp.GetDataString("ＤＲ１コード");
                obj.Ward = tmp.GetDataString("病棟コード").Trim();
                obj.Room = tmp.GetDataString("病室コード").Trim();
                obj.InKind = tmp.GetDataString("入院区分");
                obj.OutKind = tmp.GetDataString("退院区分");
            }
#endif
            return obj;
        }

        /// <summary>
        /// 患者IDと入院番号から、入院データを取得する
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="in_date"></param>
        /// <returns></returns>
        public static PatIn GetDataBySEQ(string pt_id, int in_seq)
        {
            PatIn obj = new PatIn();

            List<PatIn> list = GetHistory(pt_id);

            foreach (PatIn pin in list)
            {
                if (pin.SEQ.Equals(in_seq))
                {
                    obj = pin;
                    break;
                }
            }

            return obj;
        }

        /// <summary>
        /// 患者IDと入院日から、入院データを取得する
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="in_date"></param>
        /// <returns></returns>
        public static PatIn GetDataByDate(string pt_id, string in_date)
        {
            PatIn obj = new PatIn();

            List<PatIn> list = GetHistory(pt_id);

            foreach (PatIn pin in list)
            {
                if (pin.InDate.Equals(in_date))
                {
                    obj = pin;
                    break;
                }
            }

            return obj;
        }

        /// <summary>
        /// 入退院の履歴を取得する
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="yet">true 入退院予定も取得する, false 取得しない</param>
        /// <returns></returns>
        public static List<PatIn> GetHistory(string pt_id, bool yet = true)
        {
            List<PatIn> list = new List<PatIn>();

            if (!AppString.IsNumber(pt_id))
            {
                return list;
            }

            string cmd = "";
            
#if INNO
            string today = DateTime.Now.ToString("yyyyMMdd");

            // 過去の入院
            cmd = "select t.P_ID, t.NYUIN_NO, t.NYUIN_DATE, t.NYUIN_ZONE, t.TAIIN_DATE, t.TAIIN_ZONE, t.NYUIN_TYPE, t.TAIIN_TYPE, t.BYOTO, t.ROOM, t.DEPT, t.DR, t.PROCESS " +
                " from D_NYUIN t where t.P_ID = " + pt_id + " and t.PROCESS in (19) and (t.DEL_FLG is null or t.DEL_FLG = 0)";

            cmd += " union " +
                "select t.P_ID, t.NYUIN_NO, t.NYUIN_DATE, t.NYUIN_ZONE, t.TAIIN_PLAN_DATE, t.TAIIN_PLAN_ZONE, t.NYUIN_TYPE, t.TAIIN_TYPE, t.BYOTO, t.ROOM, t.DEPT, t.DR, null process " +
                " from D_NYUIN_NOW t where t.P_ID = " + pt_id + " and t.NYUIN_DATE > 0 and (t.TAIIN_DATE is null or t.TAIIN_DATE = 0)";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatIn obj = GetFromStdClass(tmp);

                if (obj.Process.Equals("19"))
                {
                    // 現在の入院
                    obj.Status = PatInStatus.Done;
                }
                else
                {
                    // 過去の入院
                    obj.Status = PatInStatus.Now;
                }

                // 同じデータが存在しなければ登録する
                if (list.FindAll((x) => { return x.Id.Equals(obj.Id) && x.SEQ.Equals(obj.SEQ); }).Count == 0)
                {
                    list.Add(obj);
                }
            }

            if (yet)
            {
                // 未来の入院
                cmd = "select tt.P_ID, tt.NYUIN_NO " +
                    " , case when tt.NYUIN_DATE > 0 then tt.NYUIN_DATE else tt.NYUIN_PLAN_DATE end nyuin_date " +
                    " , case when tt.NYUIN_ZONE > 0 then tt.NYUIN_ZONE else tt.NYUIN_PLAN_ZONE end nyuin_zone " +
                    " , tt.TAIIN_PLAN_DATE, tt.TAIIN_PLAN_ZONE, tt.NYUIN_TYPE, tt.TAIIN_TYPE, tt.BYOTO, tt.ROOM, tt.DEPT, tt.DR, tt.PROCESS " +
                    " from (select row_number() over(partition by NYUIN_NO order by APPLY_DATE desc, APPLY_ZONE desc, NYUIN_INDEX desc) rn, t.* " +
                    " from D_NYUIN t " +
                    " where t.P_ID = " + pt_id + " and t.PROCESS in (10, 18) and (t.DEL_FLG is null or t.DEL_FLG = 0) " +
                    " ) tt where tt.RN = 1";

                tmp_list = StdClass.GetList(DB.Db3, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    PatIn obj = GetFromStdClass(tmp);
                    obj.Status = PatInStatus.Yet;

                    // 同じデータが存在しなければ登録する
                    if (list.FindAll((x) => { return x.Id.Equals(obj.Id) && x.SEQ.Equals(obj.SEQ); }).Count == 0)
                    {
                        list.Add(obj);
                    }
                }
            }

#else
            if (yet)
            {
                cmd = "select IM01RC_F01, IM01RC_F03, IM01RC_F04, IM01RC_F05, IM01RC_F10, " +
                    " t2.* " +
                    " from IM01RC t1, ADT_入退院予定データ t2 " +
                    " where t1.IM01RC_F01 = " + pt_id + " and t2.患者コード = " + pt_id +
                    " and t1.IM01RC_F01 = t2.患者コード and t2.入院確定フラグ = 0 " +
                    " order by t2.入院予定日 desc";

                List<StdClass> yet_list = StdClass.GetList(DB.Db1, cmd);

                foreach (StdClass tmp in yet_list)
                {
                    PatIn obj = GetFromStdClass(tmp);
                    obj.Yet = true;

                    // 同じデータが存在しなければ登録する
                    if (list.FindAll((x) => { return x.Id.Equals(obj.Id) && x.InDate.Equals(obj.InDate); }).Count == 0)
                    {
                        list.Add(obj);
                    }
                }
            }

            cmd = "select IM01RC_F01, IM01RC_F03, IM01RC_F04, IM01RC_F05, IM01RC_F10 " +
                ", t21.IM21RC_F03, t21.IM21RC_F04, t21.IM21RC_F05, t21.IM21RC_F06 " +
                ", t24.IM24RC_F05, t24.IM24RC_F06, t22.IM22RC_F05, t23.IM23RC_F05 " +
                " from IM01RC t1, IM21RC t21, " +
                " (select * from " +
                " (select t.*, row_number() over (partition by t.IM22RC_F02 order by t.IM22RC_F04 desc) rn " +
                "  from IM22RC t " +
                "  where (t.IM22RC_F01, t.IM22RC_F02) in (select t21.IM21RC_F01, t21.IM21RC_F03 from IM21RC t21 where t21.IM21RC_F01 = " + pt_id + ")" +
                "  order by t.IM22RC_F01, t.IM22RC_F02 desc, t.IM22RC_F03 desc) tt " +
                " where tt.RN = 1) t22, " +
                " (select * from " +
                " (select t.*, row_number() over (partition by t.IM23RC_F02 order by t.IM23RC_F04 desc) rn " +
                "  from IM23RC t " +
                "  where (t.IM23RC_F01, t.IM23RC_F02) in (select t21.IM21RC_F01, t21.IM21RC_F03 from IM21RC t21 where t21.IM21RC_F01 = " + pt_id + ")" +
                "  order by t.IM23RC_F01, t.IM23RC_F02 desc, t.IM23RC_F03 desc) tt " +
                "  where tt.RN = 1) t23, " +
                " (select * from " +
                " (select t.*, row_number() over (partition by t.IM24RC_F02 order by t.IM24RC_F04 desc) rn " +
                "  from IM24RC t " +
                "  where (t.IM24RC_F01, t.IM24RC_F02) in (select t21.IM21RC_F01, t21.IM21RC_F03 from IM21RC t21 where t21.IM21RC_F01 = " + pt_id + ")" +
                "  order by t.IM24RC_F01, t.IM24RC_F02 desc, t.IM24RC_F03 desc) tt " +
                "  where tt.RN = 1) t24 " +
                " where " +
                "  t21.IM21RC_F01 = " + pt_id +
                "  and t1.IM01RC_F01 = " + pt_id +
                "  and t22.IM22RC_F02 = t21.IM21RC_F03" +
                "  and t23.IM23RC_F02 = t21.IM21RC_F03" +
                "  and t24.IM24RC_F02 = t21.IM21RC_F03" +
                " order by IM21RC_F02 desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatIn obj = GetFromStdClass(tmp);

                // 同じデータが存在しなければ登録する
                if (list.FindAll((x) => { return x.Id.Equals(obj.Id) && x.InDate.Equals(obj.InDate); }).Count == 0)
                {
                    list.Add(obj);
                }
            }
#endif
            // 入院日の降順に並べ替える
            list.Sort((x, y) => { return y.InDateInt - x.InDateInt; });

            for (int i = 0; i < list.Count - 1; i++)
            {
                list[i].IntervalDays = DateTimeAgent.IntervalDays(list[i + 1].OutDate, list[i].InDate);
            }

            return list;
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
#if INNO
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
//                    " and (t.TAIIN_PLAN_DATE is null or t.TAIIN_PLAN_DATE = 0 or t.TAIIN_PLAN_DATE >= " + adm_date + ")" +
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
//                    " and (ttt.TAIIN_PLAN_DATE is null or ttt.TAIIN_PLAN_DATE = 0 or ttt.TAIIN_PLAN_DATE >= " + adm_date + ")" +
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
#else
            string sqlWard = "";

            if (ward.Length > 0)
            {
                sqlWard = " and IM24RC_F05 = '" + ward + "'";
            }

            string sqlDept = "";

            if (dept.Length > 0)
            {
                sqlDept = " and IM22RC_F05 = '" + dept + "'";
            }

            string cmd = "select IM01RC_F01, IM01RC_F03, IM01RC_F04, IM01RC_F05, IM01RC_F10 " +
                ", t21.IM21RC_F03, t21.IM21RC_F04, t21.IM21RC_F05, t21.IM21RC_F06 " +
                ", t24.IM24RC_F05, t24.IM24RC_F06, t22.IM22RC_F05, t23.IM23RC_F05 " +
                " from IM01RC t1, IM21RC t21, " +
                " (select * from " +
                " (select t.*, row_number() over (partition by t.IM22RC_F01 order by t.IM22RC_F04 desc) rn " +
                "  from IM22RC t " +
                "  where (t.IM22RC_F01, t.IM22RC_F02) in (select t21.IM21RC_F01, t21.IM21RC_F03 from IM21RC t21 where t21.IM21RC_F03 <= " + adm_date + " and (t21.IM21RC_F04 = 0 or t21.IM21RC_F04 >= " + adm_date + ")) " +
                "  order by t.IM22RC_F01, t.IM22RC_F02 desc, t.IM22RC_F03 desc) tt " +
                " where tt.RN = 1) t22, " +
                " (select * from " +
                " (select t.*, row_number() over (partition by t.IM23RC_F01 order by t.IM23RC_F04 desc) rn " +
                "  from IM23RC t " +
                "  where (t.IM23RC_F01, t.IM23RC_F02) in (select t21.IM21RC_F01, t21.IM21RC_F03 from IM21RC t21 where t21.IM21RC_F03 <= " + adm_date + " and (t21.IM21RC_F04 = 0 or t21.IM21RC_F04 >= " + adm_date + ")) " +
                "  order by t.IM23RC_F01, t.IM23RC_F02 desc, t.IM23RC_F03 desc) tt " +
                "  where tt.RN = 1) t23, " +
                " (select * from " +
                " (select t.*, row_number() over (partition by t.IM24RC_F01 order by t.IM24RC_F04 desc) rn " +
                "  from IM24RC t " +
                "  where (t.IM24RC_F01, t.IM24RC_F02) in (select t21.IM21RC_F01, t21.IM21RC_F03 from IM21RC t21 where t21.IM21RC_F03 <= " + adm_date + " and (t21.IM21RC_F04 = 0 or t21.IM21RC_F04 >= " + adm_date + ")) " +
                "  order by t.IM24RC_F01, t.IM24RC_F02 desc, t.IM24RC_F03 desc) tt " +
                "  where tt.RN = 1) t24 " +
                " where " +
                "  t21.IM21RC_F03 <= " + adm_date + " and (t21.IM21RC_F04 = 0 or t21.IM21RC_F04 >= " + adm_date + ") " +
                "  and t1.IM01RC_F01 in (select t.IM21RC_F01 from IM21RC t where t.IM21RC_F03 <= " + adm_date + " and (t.IM21RC_F04 >= " + adm_date + " or t.IM21RC_F04 = 0)) " +
                "  and t21.IM21RC_F01 = t1.IM01RC_F01 " +
                "  and t22.IM22RC_F01 = t1.IM01RC_F01 " +
                "  and t23.IM23RC_F01 = t1.IM01RC_F01 " +
                "  and t24.IM24RC_F01 = t1.IM01RC_F01 " +
                sqlDept + sqlWard +
                " order by t24.IM24RC_F06, t21.IM21RC_F03 desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatIn obj = GetFromStdClass(tmp);

                // 同じデータが存在しなければ登録する
                if (list.FindAll((x) => { return x.Id.Equals(obj.Id) && x.InDate.Equals(obj.InDate); }).Count == 0)
                {
                    list.Add(obj);
                }
            }
#endif
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
        /// 入院予定患者リストを取得する。
        /// </summary>
        /// <param name="crit_date">入院日がこれ以降。指定がなければ "" で可</param>
        /// <param name="ward">病棟コード。指定がなければ "" で可</param>
        /// <param name="dept">診療科コード。指定がなければ "" で可</param>
        /// <returns></returns>
        public static List<PatIn> GetYoteiList(string crit_date, string ward = "", string dept = "")
        {
            List<PatIn> list = new List<PatIn>();
#if INNO
            string sqlDate = "";

            if (crit_date.Length > 0)
            {
                sqlDate = " and NYUIN_PLAN_DATE >= " + crit_date;
            }

            string sqlWard = "";

            if (ward.Length > 0)
            {
                sqlWard = " and BYOTO = " + ward;
            }

            string sqlDept = "";

            if (dept.Length > 0)
            {
                sqlDept = " and DEPT = " + dept;
            }

            string cmd = "select tt.*, tm.P_KANA, tm.P_NAME, tm.P_SEX, tm.P_BIRTHDAY_AD " +
                " from " +
                "(select row_number() over (partition by td.P_ID, td.NYUIN_NO order by td.PROCESS desc, td.NYUIN_INDEX desc) rn, td.* " +
                " from D_NYUIN td " +
                " where (td.P_ID, td.NYUIN_NO) in " +
                " (select t.P_ID, t.NYUIN_NO from D_NYUIN t " +
                " where t.PROCESS in (10) and (t.DEL_FLG is null or t.DEL_FLG = 0) " +
                " minus " +
                " select t.P_ID, t.NYUIN_NO from D_NYUIN t " +
                " where t.PROCESS in (11) and (t.DEL_FLG is null or t.DEL_FLG = 0)) " +
                " and td.PROCESS in (10, 18) and (td.DEL_FLG is null or td.DEL_FLG = 0)) tt, M_PATIENT tm " +
                " where tt.RN = 1 and tt.P_ID = tm.P_ID" + sqlDate + sqlWard + sqlDept;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatIn obj = GetFromStdClass(tmp);
                obj.Status = PatInStatus.Yet;

                // 同じデータが存在しなければ登録する
                if (list.FindAll((x) => { return x.Id.Equals(obj.Id) && x.SEQ.Equals(obj.SEQ); }).Count == 0)
                {
                    list.Add(obj);
                }
            }
#else
            string sqlDate = "";

            if (crit_date.Length > 0)
            {
                sqlDate = " and 入院予定日 >= " + crit_date;
            }

            string sqlWard = "";

            if (ward.Length > 0)
            {
                sqlWard = " and 病棟コード = '" + ward + "'";
            }

            string sqlDept = "";

            if (dept.Length > 0)
            {
                sqlDept = " and 科コード = '" + dept + "'";
            }

            string cmd = "select IM01RC_F01, IM01RC_F03, IM01RC_F04, IM01RC_F05, IM01RC_F10, "
                            + " t2.* "
                            + " from IM01RC t1, ADT_入退院予定データ t2 "
                            + " where t1.IM01RC_F01 = t2.患者コード and t2.入院確定フラグ = 0 "
                            + sqlDate + sqlDept + sqlWard;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatIn obj = GetFromStdClass(tmp);

                // 同じデータが存在しなければ登録する
                if (list.FindAll((x) => { return x.Id.Equals(obj.Id) && x.InDate.Equals(obj.InDate); }).Count == 0)
                {
                    list.Add(obj);
                }
            }
#endif
            // 入院予定日の順に並べ替える
            list.Sort((x, y) => { return x.InDateInt - y.InDateInt; });

            return list;
        }

        /// <summary>
        /// 入院患者リストを取得する。
        /// </summary>
        /// <param name="ward">病棟コード。指定がなければ "" で可</param>
        /// <param name="dept">診療科コード。指定がなければ "" で可</param>
        /// <param name="pat_info">true: 患者情報を取得する</param>
        /// <returns></returns>
        public static List<PatIn> GetList(string ward = "", string dept = "", bool pat_info = true)
        {
            List<PatIn> list = new List<PatIn>();
#if INNO
            string sqlWard = "";

            if (ward.Length > 0)
            {
                sqlWard = " and BYOTO = " + ward;
            }

            string sqlDept = "";

            if (dept.Length > 0)
            {
                sqlDept = " and DEPT = " + dept;
            }

            string cmd = "";

            if (pat_info)
            {
                cmd = "select td.*, tm.P_KANA, tm.P_NAME, tm.P_SEX, tm.P_BIRTHDAY_AD " +
                    " from D_NYUIN_NOW td, M_PATIENT tm " +
                    " where td.P_ID = tm.P_ID " +
                    " and td.NYUIN_DATE > 0 " +
                    " and td.NYUIN_DATE <= " + DateTime.Now.ToString("yyyyMMdd") +
                    " and (td.TAIIN_DATE is null or td.TAIIN_DATE = 0) " + sqlWard + sqlDept +
                    " order by td.BYOTO, td.ROOM, td.BED";
            }
            else
            {
                cmd = "select td.* " +
                    " from D_NYUIN_NOW td " +
                    " where td.NYUIN_DATE > 0 " +
                    " and td.NYUIN_DATE <= " + DateTime.Now.ToString("yyyyMMdd") +
                    " and (td.TAIIN_DATE is null or td.TAIIN_DATE = 0) " + sqlWard + sqlDept +
                    " order by td.BYOTO, td.ROOM, td.BED";
            }

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatIn obj = GetFromStdClass(tmp);
                obj.Status = PatInStatus.Now;

                // 同じデータが存在しなければ登録する
                if (list.FindAll((x) => { return x.Id.Equals(obj.Id) && x.InDate.Equals(obj.InDate); }).Count == 0)
                {
                    list.Add(obj);
                }
            }
#else
            string sqlWard = "";

            if (ward.Length > 0)
            {
                sqlWard = " and 病棟コード = '" + ward + "'";
            }

            string sqlDept = "";

            if (dept.Length > 0)
            {
                sqlDept = " and 科コード = '" + dept + "'";
            }

            string cmd = "select IM01RC_F01, IM01RC_F03, IM01RC_F04, IM01RC_F05, IM01RC_F10, "
                            + " t2.* "
                            + " from IM01RC t1, AMP_患者入院マスター t2 "
                            + " where t1.IM01RC_F01 = t2.患者コード and t2.履歴区分 = 0 "
                            + sqlDept + sqlWard;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatIn obj = GetFromStdClass(tmp);

                // 同じデータが存在しなければ登録する
                if (list.FindAll((x) => { return x.Id.Equals(obj.Id) && x.InDate.Equals(obj.InDate); }).Count == 0)
                {
                    list.Add(obj);
                }
            }
#endif
            return list;
        }

        /// <summary>
        /// 入院患者リストの Dictionary を取得する。
        /// </summary>
        /// <param name="pt_list"></param>
        /// <returns></returns>
        public static Dictionary<string, PatIn> GetDict(List<string> pt_list)
        {
            Dictionary<string, PatIn> dict = new Dictionary<string, PatIn>();

            if (pt_list.Count == 0)
            {
                return dict;
            }

            List<PatIn> list = GetList();

            foreach (PatIn obj in list)
            {
                if (pt_list.Contains(obj.Id) && !dict.ContainsKey(obj.Id))
                {
                    obj.Status = PatInStatus.Now;
                    dict.Add(obj.Id, obj);
                }
            }

            return dict;
        }

        /// <summary>
        /// 退院患者リストを取得する。
        /// </summary>
        /// <param name="date1">退院日がこれ以降。指定がなければ "" で可。デフォルトで 14 日前。</param>
        /// <param name="date2">退院日がこれ以前。指定がなければ "" で可。</param>
        /// <param name="ward">病棟コード。指定がなければ "" で可</param>
        /// <param name="dept">診療科コード。指定がなければ "" で可</param>
        /// <returns></returns>
        public static List<PatIn> GetOutList(string date1 = "", string date2 = "", string ward = "", string dept = "")
        {
            List<PatIn> list = new List<PatIn>();
#if INNO
            string sqlDate1 = date1;

            if (date1.Length != 8)
            {
                sqlDate1 = DateTime.Now.AddDays(-14).ToString("yyyyMMdd");
            }

            string sqlDate2 = date2;

            if (date2.Length != 8)
            {
                sqlDate2 = DateTime.Now.ToString("yyyyMMdd");
            }

            string sqlWard = "";

            if (ward.Length > 0)
            {
                sqlWard = " and BYOTO = " + ward;
            }

            string sqlDept = "";

            if (dept.Length > 0)
            {
                sqlDept = " and DEPT = " + dept;
            }

            string cmd = "select td.*, tm.P_KANA, tm.P_NAME, tm.P_SEX, tm.P_BIRTHDAY_AD " +
                " from D_NYUIN td, M_PATIENT tm " +
                " where td.TAIIN_DATE >= " + sqlDate1 + " and td.TAIIN_DATE <= " + sqlDate2 +
                " and td.PROCESS in (19) and (td.DEL_FLG is null or td.DEL_FLG = 0) " +
                " and td.P_ID = tm.P_ID " + sqlWard + sqlDept +
                " order by td.TAIIN_DATE desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatIn obj = GetFromStdClass(tmp);
                obj.Status = PatInStatus.Done;

                // 同じデータが存在しなければ登録する
                if (list.FindAll((x) => { return x.Id.Equals(obj.Id) && x.SEQ.Equals(obj.SEQ); }).Count == 0)
                {
                    list.Add(obj);
                }
            }
#else
            string sqlDate1 = date1;

            if (date1.Length != 8)
            {
                sqlDate1 = DateTime.Now.AddDays(-14).ToString("yyyyMMdd");
            }

            string sqlDate2 = date2;

            if (date2.Length != 8)
            {
                sqlDate2 = DateTime.Now.ToString("yyyyMMdd");
            }

            string sqlWard = "";

            if (ward.Length > 0)
            {
                sqlWard = " and IM24RC_F05 = '" + ward + "'";
            }

            string sqlDept = "";

            if (dept.Length > 0)
            {
                sqlDept = " and IM22RC_F05 = '" + dept + "'";
            }

            string cmd = "select IM01RC_F01, IM01RC_F03, IM01RC_F04, IM01RC_F05, IM01RC_F10 " +
                ", t21.IM21RC_F03, t21.IM21RC_F04, t21.IM21RC_F05, t21.IM21RC_F06 " +
                ", t24.IM24RC_F05, t24.IM24RC_F06, t22.IM22RC_F05, t23.IM23RC_F05 " +
                " from IM01RC t1, IM21RC t21, " +
                " (select * from " +
                " (select t.*, row_number() over (partition by t.IM22RC_F01 order by t.IM22RC_F04 desc, t.IM22RC_F03 desc) rn " +
                "  from IM22RC t " +
                "  where (t.IM22RC_F01, t.IM22RC_F02) in (select t21.IM21RC_F01, t21.IM21RC_F03 from IM21RC t21 where t21.IM21RC_F04 >= " + sqlDate1 + " and t21.IM21RC_F04 <= " + sqlDate2 + ") " +
                "  order by t.IM22RC_F01, t.IM22RC_F02 desc, t.IM22RC_F03 desc) tt " +
                " where tt.RN = 1) t22, " +
                " (select * from " +
                " (select t.*, row_number() over (partition by t.IM23RC_F01 order by t.IM23RC_F04 desc, t.IM23RC_F03 desc) rn " +
                "  from IM23RC t " +
                "  where (t.IM23RC_F01, t.IM23RC_F02) in (select t21.IM21RC_F01, t21.IM21RC_F03 from IM21RC t21 where t21.IM21RC_F04 >= " + sqlDate1 + " and t21.IM21RC_F04 <= " + sqlDate2 + ") " +
                "  order by t.IM23RC_F01, t.IM23RC_F02 desc, t.IM23RC_F03 desc) tt " +
                "  where tt.RN = 1) t23, " +
                " (select * from " +
                " (select t.*, row_number() over (partition by t.IM24RC_F01 order by t.IM24RC_F04 desc, t.IM24RC_F03 desc) rn " +
                "  from IM24RC t " +
                "  where (t.IM24RC_F01, t.IM24RC_F02) in (select t21.IM21RC_F01, t21.IM21RC_F03 from IM21RC t21 where t21.IM21RC_F04 >= " + sqlDate1 + " and t21.IM21RC_F04 <= " + sqlDate2 + ") " +
                "  order by t.IM24RC_F01, t.IM24RC_F02 desc, t.IM24RC_F03 desc) tt " +
                "  where tt.RN = 1) t24 " +
                " where " +
                "  t21.IM21RC_F04 >= " + sqlDate1 + " and t21.IM21RC_F04 <= " + sqlDate2 +
                "  and t1.IM01RC_F01 in (select t.IM21RC_F01 from IM21RC t where t.IM21RC_F04 >= " + sqlDate1 + " and t.IM21RC_F04 <= " + sqlDate2 + ") " +
                "  and t21.IM21RC_F01 = t1.IM01RC_F01 " +
                "  and t22.IM22RC_F01 = t1.IM01RC_F01 " +
                "  and t23.IM23RC_F01 = t1.IM01RC_F01 " +
                "  and t24.IM24RC_F01 = t1.IM01RC_F01 " +
                sqlDept + sqlWard +
                " order by t24.IM24RC_F06, t21.IM21RC_F03 desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatIn obj = GetFromStdClass(tmp);

                // 同じデータが存在しなければ登録する
                if (list.FindAll((x) => { return x.Id.Equals(obj.Id) && x.InDate.Equals(obj.InDate); }).Count == 0)
                {
                    list.Add(obj);
                }
            }
#endif
            // 退院日の降順に並べ替える
            list.Sort((x, y) => { return y.OutDateInt - x.OutDateInt; });

            return list;
        }

        /// <summary>
        /// 指定した患者＋入院番号の、指定日時点での科を取得する
        /// </summary>
        /// <param name="seq_list"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<PatIn> GetDeptList(List<PatIn> seq_list, string date)
        {
            List<PatIn> list = new List<PatIn>();

            string ss = AppString.ConcatList(seq_list.ConvertAll((x) => { return "(" + x.Id + "," + x.SEQ + ")"; }), ",");

            if (ss.Length == 0 || !DateTimeAgent.IsDate(date))
            {
                return list;
            }

            string cmd = "select * from " +
                " (select row_number() over (partition by P_ID, NYUIN_NO order by APPLY_DATE desc, APPLY_ZONE desc, NYUIN_INDEX desc) rn, t.* " +
                "  from D_NYUIN t " +
                "  where t.PROCESS in (11, 14, 19) and (t.DEL_FLG is null or t.DEL_FLG = 0) " +
                "  and (t.DEPT is not null and t.DEPT > 0) " +
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
        /// 指定した（患者, 入院番号）の組み合わせの、科の移動歴を取得する
        /// </summary>
        /// <param name="seq_list"></param>
        /// <param name="pat_info">true: 患者情報を取得する</param>
        /// <returns></returns>
        public static List<PatIn> GetDeptList(List<PatIn> seq_list, bool pat_info = true)
        {
            List<PatIn> list = new List<PatIn>();

            // 現在または過去の入院から取得
            string ss = AppString.ConcatList(seq_list.FindAll((x) =>
            {
                return (x.Status == PatInStatus.Done || x.Status == PatInStatus.Now);
            })
            .ConvertAll((x) =>
            {
                return "(" + x.Id + "," + x.SEQ + ")";
            }), ",");

            if (ss.Length > 0)
            {
                string cmd = "";

                if (pat_info)
                {
                    cmd = "select tt.P_ID, tt.NYUIN_NO, tt.NYUIN_DATE, tt.NYUIN_ZONE, tt.TAIIN_DATE, tt.TAIIN_ZONE " +
                        ", td.APPLY_DATE, td.APPLY_ZONE, td.NYUIN_INDEX, td.DEPT " +
                        ", tm.P_KANA, tm.P_NAME, tm.P_SEX, tm.P_BIRTHDAY_AD " +
                        " from D_NYUIN td, M_PATIENT tm, " +
                        " (select t.P_ID, t.NYUIN_NO, t.NYUIN_DATE, t.NYUIN_ZONE, t.TAIIN_DATE, t.TAIIN_ZONE from D_NYUIN t " +
                        "  where (t.P_ID, t.NYUIN_NO) in (" + ss + ") and t.PROCESS in (19) and (t.DEL_FLG is null or t.DEL_FLG = 0) " +
                        "  union " +
                        "  select t.P_ID, t.NYUIN_NO, t.NYUIN_DATE, t.NYUIN_ZONE, t.TAIIN_PLAN_DATE, t.TAIIN_PLAN_ZONE from D_NYUIN_NOW t " +
                        "  where (t.P_ID, t.NYUIN_NO) in (" + ss + ") and (t.TAIIN_DATE is null or t.TAIIN_DATE = 0)) tt " +
                        " where td.PROCESS in (11, 14, 19) and (td.DEL_FLG is null or td.DEL_FLG = 0) " +
                        " and td.P_ID = tt.P_ID and td.NYUIN_NO = tt.NYUIN_NO " +
                        " and td.P_ID = tm.P_ID " +
                        " order by tt.P_ID, tt.NYUIN_NO, td.APPLY_DATE, td.APPLY_ZONE, td.NYUIN_INDEX";
                }
                else
                {
                    cmd = "select tt.P_ID, tt.NYUIN_NO, tt.NYUIN_DATE, tt.NYUIN_ZONE, tt.TAIIN_DATE, tt.TAIIN_ZONE " +
                        ", td.APPLY_DATE, td.APPLY_ZONE, td.NYUIN_INDEX, td.DEPT " +
                        " from D_NYUIN td, " +
                        " (select t.P_ID, t.NYUIN_NO, t.NYUIN_DATE, t.NYUIN_ZONE, t.TAIIN_DATE, t.TAIIN_ZONE from D_NYUIN t " +
                        "  where (t.P_ID, t.NYUIN_NO) in (" + ss + ") and t.PROCESS in (19) and (t.DEL_FLG is null or t.DEL_FLG = 0) " +
                        "  union " +
                        "  select t.P_ID, t.NYUIN_NO, t.NYUIN_DATE, t.NYUIN_ZONE, t.TAIIN_PLAN_DATE, t.TAIIN_PLAN_ZONE from D_NYUIN_NOW t " +
                        "  where (t.P_ID, t.NYUIN_NO) in (" + ss + ") and (t.TAIIN_DATE is null or t.TAIIN_DATE = 0)) tt " +
                        " where td.PROCESS in (11, 14, 19) and (td.DEL_FLG is null or td.DEL_FLG = 0) " +
                        " and td.P_ID = tt.P_ID and td.NYUIN_NO = tt.NYUIN_NO " +
                        " order by tt.P_ID, tt.NYUIN_NO, td.APPLY_DATE, td.APPLY_ZONE, td.NYUIN_INDEX";
                }

                List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    PatIn obj = PatIn.GetFromStdClass(tmp);

                    // 同一患者・同一入院で、直前の科と同じなら飛ばす
                    if (list.Count > 0)
                    {
                        PatIn obj2 = list[list.Count - 1];

                        if (obj2.Id.Equals(obj.Id) &&
                            obj2.SEQ.Equals(obj.SEQ) &&
                            obj2.Dept.Equals(obj.Dept))
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

        /// <summary>
        /// 指定した患者＋入院番号の、指定日時点での医師を取得する
        /// </summary>
        /// <param name="seq_list"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<PatIn> GetDoctorList(List<PatIn> seq_list, string date)
        {
            List<PatIn> list = new List<PatIn>();

            string ss = AppString.ConcatList(seq_list.ConvertAll((x) => { return "(" + x.Id + "," + x.SEQ + ")"; }), ",");

            if (ss.Length == 0 || !DateTimeAgent.IsDate(date))
            {
                return list;
            }

            string cmd = "select * from " +
                " (select row_number() over (partition by P_ID, NYUIN_NO order by APPLY_DATE desc, APPLY_ZONE desc, NYUIN_INDEX desc) rn, t.* " +
                "  from D_NYUIN t " +
                "  where t.PROCESS in (11, 15, 19) and (t.DEL_FLG is null or t.DEL_FLG = 0) " +
                "  and (t.DR is not null and t.DR > 0) " +
                "  and (t.P_ID, t.NYUIN_NO) in (" + ss + ") " +
                "  and t.APPLY_DATE <= " + date + ") tt " +
                " where tt.RN = 1";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatIn obj = PatIn.GetFromStdClass(tmp);

                list.Add(obj);
            }

            return list;
        }

        /// <summary>
        /// 指定した（患者, 入院番号）の組み合わせの、病室の移動歴を取得する
        /// </summary>
        /// <param name="seq_list"></param>
        /// <param name="pat_info">true: 患者情報を取得する</param>
        /// <returns></returns>
        public static List<PatIn> GetDoctorList(List<PatIn> seq_list, bool pat_info = true)
        {
            List<PatIn> list = new List<PatIn>();

            // 現在または過去の入院から取得
            string ss = AppString.ConcatList(seq_list.FindAll((x) =>
            {
                return (x.Status == PatInStatus.Done || x.Status == PatInStatus.Now);
            })
            .ConvertAll((x) =>
            {
                return "(" + x.Id + "," + x.SEQ + ")";
            }), ",");

            if (ss.Length > 0)
            {
                string cmd = "";

                if (pat_info)
                {
                    cmd = "select tt.P_ID, tt.NYUIN_NO, tt.NYUIN_DATE, tt.NYUIN_ZONE, tt.TAIIN_DATE, tt.TAIIN_ZONE " +
                        ", td.APPLY_DATE, td.APPLY_ZONE, td.NYUIN_INDEX, td.DR " +
                        ", tm.P_KANA, tm.P_NAME, tm.P_SEX, tm.P_BIRTHDAY_AD " +
                        " from D_NYUIN td, M_PATIENT tm, " +
                        " (select t.P_ID, t.NYUIN_NO, t.NYUIN_DATE, t.NYUIN_ZONE, t.TAIIN_DATE, t.TAIIN_ZONE from D_NYUIN t " +
                        "  where (t.P_ID, t.NYUIN_NO) in (" + ss + ") and t.PROCESS in (19) and (t.DEL_FLG is null or t.DEL_FLG = 0) " +
                        "  union " +
                        "  select t.P_ID, t.NYUIN_NO, t.NYUIN_DATE, t.NYUIN_ZONE, t.TAIIN_PLAN_DATE, t.TAIIN_PLAN_ZONE from D_NYUIN_NOW t " +
                        "  where (t.P_ID, t.NYUIN_NO) in (" + ss + ") and (t.TAIIN_DATE is null or t.TAIIN_DATE = 0)) tt " +
                        " where td.PROCESS in (11, 15, 19) and (td.DEL_FLG is null or td.DEL_FLG = 0) " +
                        " and td.P_ID = tt.P_ID and td.NYUIN_NO = tt.NYUIN_NO " +
                        " and td.P_ID = tm.P_ID " +
                        " order by tt.P_ID, tt.NYUIN_NO, td.APPLY_DATE, td.APPLY_ZONE, td.NYUIN_INDEX";
                }
                else
                {
                    cmd = "select tt.P_ID, tt.NYUIN_NO, tt.NYUIN_DATE, tt.NYUIN_ZONE, tt.TAIIN_DATE, tt.TAIIN_ZONE " +
                        ", td.APPLY_DATE, td.APPLY_ZONE, td.NYUIN_INDEX, td.DR " +
                        " from D_NYUIN td, " +
                        " (select t.P_ID, t.NYUIN_NO, t.NYUIN_DATE, t.NYUIN_ZONE, t.TAIIN_DATE, t.TAIIN_ZONE from D_NYUIN t " +
                        "  where (t.P_ID, t.NYUIN_NO) in (" + ss + ") and t.PROCESS in (19) and (t.DEL_FLG is null or t.DEL_FLG = 0) " +
                        "  union " +
                        "  select t.P_ID, t.NYUIN_NO, t.NYUIN_DATE, t.NYUIN_ZONE, t.TAIIN_PLAN_DATE, t.TAIIN_PLAN_ZONE from D_NYUIN_NOW t " +
                        "  where (t.P_ID, t.NYUIN_NO) in (" + ss + ") and (t.TAIIN_DATE is null or t.TAIIN_DATE = 0)) tt " +
                        " where td.PROCESS in (11, 15, 19) and (td.DEL_FLG is null or td.DEL_FLG = 0) " +
                        " and td.P_ID = tt.P_ID and td.NYUIN_NO = tt.NYUIN_NO " +
                        " order by tt.P_ID, tt.NYUIN_NO, td.APPLY_DATE, td.APPLY_ZONE, td.NYUIN_INDEX";
                }

                List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    PatIn obj = PatIn.GetFromStdClass(tmp);

                    // 同一患者・同一入院で、直前の医師と同じなら飛ばす
                    if (list.Count > 0)
                    {
                        PatIn obj2 = list[list.Count - 1];

                        if (obj2.Id.Equals(obj.Id) &&
                            obj2.SEQ.Equals(obj.SEQ) &&
                            obj2.Doctor.Equals(obj.Doctor))
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

        /// <summary>
        /// 入院中の患者の病棟・病室の移動歴を取得する
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, List<PatIn>> GetRoomDict()
        {
            Dictionary<string, List<PatIn>> dict = new Dictionary<string, List<PatIn>>();

            List<PatIn> list = new List<PatIn>();
#if INNO
            string cmd = "select * from D_NYUIN td " +
                " where td.PROCESS in (11, 13) and (td.DEL_FLG is null or td.DEL_FLG = 0) " +
                " and (td.P_ID, td.NYUIN_NO) in " +
                " (select t.P_ID, t.NYUIN_NO from D_NYUIN_NOW t " +
                "  where t.NYUIN_DATE <= " + DateTime.Now.ToString("yyyyMMdd") + " and (t.TAIIN_DATE is null or t.TAIIN_DATE = 0)) " +
                " order by P_ID, NYUIN_NO, APPLY_DATE, APPLY_ZONE, NYUIN_INDEX";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
#else
            string cmd = "select * from IM24RC t " +
                " where (t.IM24RC_F01, t.IM24RC_F02) in " +
                " (select tt.患者コード, tt.入院日 from macs.AMP_患者入院マスター tt where tt.履歴区分 = 0) " +
                " order by t.IM24RC_F01, t.IM24RC_F03";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif
            foreach (StdClass tmp in tmp_list)
            {
                PatIn obj = PatIn.GetFromStdClass(tmp);

                // 直前と同じ患者・入院番号・部屋であれば飛ばす
                if (list.Count > 0)
                {
                    PatIn obj2 = list[list.Count - 1];

                    if (obj.Id.Equals(obj2.Id) &&
                        obj.SEQ.Equals(obj2.SEQ) &&
                        obj.Room.Equals(obj2.Room))
                    {
                        continue;
                    }
                }

                list.Add(obj);
            }

            foreach (PatIn obj in list)
            {
                if (dict.ContainsKey(obj.Id))
                {
                    dict[obj.Id].Add(obj);
                }
                else
                {
                    List<PatIn> list2 = new List<PatIn>();
                    list2.Add(obj);
                    dict.Add(obj.Id, list2);
                }
            }

            return dict;
        }

        /// <summary>
        /// 指定した（患者, 入院番号）の組み合わせの、指定日時点での病棟・病室を取得する
        /// </summary>
        /// <param name="seq_list"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<PatIn> GetRoomList(List<PatIn> seq_list, string date)
        {
            List<PatIn> list = new List<PatIn>();

            string ss = AppString.ConcatList(seq_list.ConvertAll((x) => { return "(" + x.Id + "," + x.SEQ + ")"; }), ",");

            if (ss.Length == 0 || !DateTimeAgent.IsDate(date))
            {
                return list;
            }

            string cmd = "select * from " +
                " (select row_number() over (partition by P_ID, NYUIN_NO order by APPLY_DATE desc, APPLY_ZONE desc, NYUIN_INDEX desc) rn, t.* " +
                "  from D_NYUIN t " +
                "  where t.PROCESS in (11, 13, 19) and (t.DEL_FLG is null or t.DEL_FLG = 0) " +
                "  and (t.ROOM is not null and trim(t.ROOM) != '0') " +
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
        /// 指定した（患者, 入院番号）の組み合わせの、病室の移動歴を取得する
        /// </summary>
        /// <param name="seq_list"></param>
        /// <param name="pat_info">true: 患者情報を取得する</param>
        /// <returns></returns>
        public static List<PatIn> GetRoomList(List<PatIn> seq_list, bool pat_info = true)
        {
            List<PatIn> list = new List<PatIn>();

            // 現在または過去の入院から取得
            string ss = AppString.ConcatList(seq_list.FindAll((x) =>
            {
                return (x.Status == PatInStatus.Done || x.Status == PatInStatus.Now);
            })
            .ConvertAll((x) =>
            {
                return "(" + x.Id + "," + x.SEQ + ")";
            }), ",");

            if (ss.Length > 0)
            {
                string cmd = "";

                if (pat_info)
                {
                    cmd = "select tt.P_ID, tt.NYUIN_NO, tt.NYUIN_DATE, tt.NYUIN_ZONE, tt.TAIIN_DATE, tt.TAIIN_ZONE " +
                        ", td.APPLY_DATE, td.APPLY_ZONE, td.NYUIN_INDEX, td.BYOTO, td.ROOM, td.BED " +
                        ", tm.P_KANA, tm.P_NAME, tm.P_SEX, tm.P_BIRTHDAY_AD " +
                        " from D_NYUIN td, M_PATIENT tm, " +
                        " (select t.P_ID, t.NYUIN_NO, t.NYUIN_DATE, t.NYUIN_ZONE, t.TAIIN_DATE, t.TAIIN_ZONE from D_NYUIN t " +
                        "  where (t.P_ID, t.NYUIN_NO) in (" + ss + ") and t.PROCESS in (19) and (t.DEL_FLG is null or t.DEL_FLG = 0) " +
                        "  union " +
                        "  select t.P_ID, t.NYUIN_NO, t.NYUIN_DATE, t.NYUIN_ZONE, t.TAIIN_PLAN_DATE, t.TAIIN_PLAN_ZONE from D_NYUIN_NOW t " +
                        "  where (t.P_ID, t.NYUIN_NO) in (" + ss + ") and (t.TAIIN_DATE is null or t.TAIIN_DATE = 0)) tt " +
                        " where td.PROCESS in (11, 13, 19) and (td.DEL_FLG is null or td.DEL_FLG = 0) " +
                        " and td.P_ID = tt.P_ID and td.NYUIN_NO = tt.NYUIN_NO " +
                        " and td.P_ID = tm.P_ID " +
                        " order by tt.P_ID, tt.NYUIN_NO, td.APPLY_DATE, td.APPLY_ZONE, td.NYUIN_INDEX";
                }
                else
                {
                    cmd = "select tt.P_ID, tt.NYUIN_NO, tt.NYUIN_DATE, tt.NYUIN_ZONE, tt.TAIIN_DATE, tt.TAIIN_ZONE " +
                        ", td.APPLY_DATE, td.APPLY_ZONE, td.NYUIN_INDEX, td.BYOTO, td.ROOM, td.BED " +
                        " from D_NYUIN td, " +
                        " (select t.P_ID, t.NYUIN_NO, t.NYUIN_DATE, t.NYUIN_ZONE, t.TAIIN_DATE, t.TAIIN_ZONE from D_NYUIN t " +
                        "  where (t.P_ID, t.NYUIN_NO) in (" + ss + ") and t.PROCESS in (19) and (t.DEL_FLG is null or t.DEL_FLG = 0) " +
                        "  union " +
                        "  select t.P_ID, t.NYUIN_NO, t.NYUIN_DATE, t.NYUIN_ZONE, t.TAIIN_PLAN_DATE, t.TAIIN_PLAN_ZONE from D_NYUIN_NOW t " +
                        "  where (t.P_ID, t.NYUIN_NO) in (" + ss + ") and (t.TAIIN_DATE is null or t.TAIIN_DATE = 0)) tt " +
                        " where td.PROCESS in (11, 13, 19) and (td.DEL_FLG is null or td.DEL_FLG = 0) " +
                        " and td.P_ID = tt.P_ID and td.NYUIN_NO = tt.NYUIN_NO " +
                        " order by tt.P_ID, tt.NYUIN_NO, td.APPLY_DATE, td.APPLY_ZONE, td.NYUIN_INDEX";
                }

                List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    PatIn obj = PatIn.GetFromStdClass(tmp);

                    // 同一患者・同一入院で、直前の部屋と同じなら飛ばす
                    if (list.Count > 0)
                    {
                        PatIn obj2 = list[list.Count - 1];

                        if (obj2.Id.Equals(obj.Id) &&
                            obj2.SEQ.Equals(obj.SEQ) &&
                            obj2.Room.Equals(obj.Room))
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

    public class PatInDPCWard : PatIn
    {
        public string EndDate = "";

        public string EndTime = "";

        /// <summary>
        /// 病棟移動番号
        /// </summary>
        public int SEQ3 = 0;

        /// <summary>
        /// その入院の最終病棟かどうか
        /// </summary>
        public bool IsLast = false;

        /// <summary>
        /// 日数
        /// </summary>
        public int WardDays
        {
            get
            {
                int i = 0;

                if (AppString.IsDate(this.DoDate))
                {
                    if (AppString.IsDate(this.EndDate))
                    {
                        i = DateTimeAgent.IntervalDays(this.DoDate, this.EndDate) + 1;
                    }
                    else
                    {
                        i = DateTimeAgent.IntervalDays(this.DoDate, DateTime.Now.ToString("yyyyMMdd")) + 1;
                    }
                }

                if (i < 0) i = 0;

                return i;
            }
        }


        public PatInDPCWard()
        {
        }

        public PatInDPCWard(PatIn pin)
        {
            this.Id = pin.Id;
            this.Name = pin.Name;
            this.Kana = pin.Kana;
            this.Sex = pin.Sex;
            this.Birth = pin.Birth;

            this.InDate = pin.InDate;
            this.InTime = pin.InTime;
            this.OutDate = pin.OutDate;
            this.OutTime = pin.OutTime;
            this.InKind = pin.InKind;
            this.OutKind = pin.OutKind;

            this.SEQ = pin.SEQ;
            this.SEQ2 = pin.SEQ2;

            this.Ward = pin.Ward;
            this.Room = pin.Room;
            this.Bed = pin.Bed;

            this.DoDate = pin.DoDate;
            this.DoTime = pin.DoTime;
        }


        public static List<PatInDPCWard> GetList(List<PatIn> seq_list, bool pat_info = true)
        {
            List<PatInDPCWard> list = new List<PatInDPCWard>();

            // 病室移動歴を取得して患者ID・入院順・病室移動順にグループ化する
            List<PatInRoomGroup> group_list = PatInRoomGroup.GetList(PatIn.GetRoomList(seq_list, pat_info));

            for (int i = 0; i < group_list.Count; i++)
            {
                PatInRoomGroup group = group_list[i];

                for (int j = 0; j < group.PatInRoomList.Count; j++)
                {
                    PatIn rm = group.PatInRoomList[j];

                    PatInDPCWard obj = new PatInDPCWard(rm);

                    if (j < group.PatInRoomList.Count - 1)
                    {
                        // 転棟している場合は、退棟日・退棟時刻は次のデータの適用日・適用時刻となる
                        obj.EndDate = group.PatInRoomList[j + 1].DoDate;
                        obj.EndTime = group.PatInRoomList[j + 1].DoTime;
                    }
                    else
                    {
                        // 転棟していない場合は、退棟日・退棟時刻は退院日・退院時刻となる
                        if (AppString.IsDate(group.OutDate))
                        {
                            obj.EndDate = group.OutDate;
                            obj.EndTime = group.OutTime;
                        }
                    }

                    obj.SEQ3 = j + 1;
                    obj.IsLast = obj.SEQ3.Equals(group.PatInRoomList.Count);

                    list.Add(obj);
                }
            }

            return list;
        }
    }

    /// <summary>
    /// 同一患者・同一入院ごとにグループ化して、病棟移動歴
    /// </summary>
    class PatInRoomGroup : PatIn
    {
        public List<PatIn> PatInRoomList = new List<PatIn>();

        public static List<PatInRoomGroup> GetList(List<PatIn> list)
        {
            List<PatInRoomGroup> group_list = new List<PatInRoomGroup>();

            // いったんソートする
            list.Sort((x, y) =>
            {
                // 患者ID
                int i = x.Id.CompareTo(y.Id);

                if (i == 0)
                {
                    // 入院日
                    i = x.InDate.CompareTo(y.InDate);
                }

                if (i == 0)
                {
                    // 入院時間
                    i = x.InTime.CompareTo(y.InTime);
                }

                if (i == 0)
                {
                    // 移動日
                    i = x.DoDate.CompareTo(y.DoDate);
                }

                if (i == 0)
                {
                    // 移動時間
                    i = x.DoTime.CompareTo(y.DoTime);
                }

                if (i == 0)
                {
                    // 連番2
                    i = x.SEQ2 - y.SEQ2;
                }

                return i;
            });


            List<PatIn> list2 = new List<PatIn>();

            for (int i = 0; i < list.Count; i++)
            {
                PatIn obj = list[i];
                bool b = true;

                // 次のデータと比較
                if (i < list.Count - 1)
                {
                    PatIn obj2 = list[i + 1];

                    // 同一患者・同一入院の場合
                    if (obj.Id.Equals(obj2.Id) &&
                        obj.SEQ.Equals(obj2.SEQ))
                    {
                        if (obj.DoDate.Equals(obj2.DoDate))
                        {
                            // 同一日に移った場合は追加しない
                            //  → 当面は全部追加する 2019/08/15
//                            b = false;
                        }
                    }
                }

                if (b) list2.Add(obj);
            }

            List<PatIn> list3 = new List<PatIn>();

            for (int i = 0; i < list2.Count; i++)
            {
                PatIn obj = list2[i];
                bool b = true;

                // 前のデータと比較
                if (i > 0)
                {
                    PatIn obj2 = list2[i - 1];

                    // 同一患者・同一入院の場合
                    if (obj.Id.Equals(obj2.Id) &&
                        obj.SEQ.Equals(obj2.SEQ))
                    {
                        if (obj.Ward.Equals(obj2.Ward))
                        {
                            // 同一病棟に移る場合は追加しない
                            b = false;

                            // 病室だけは変わったことにしておく
                            obj2.Room = obj.Room;
                        }
                        else if (!obj.Ward.Equals("3") && !obj2.Ward.Equals("3"))
                        {
                            // 移動前後に地域包括（わかば）を含まない場合は追加しない
                            b = false;
                        }
                    }
                }

                if (b) list3.Add(obj);
            }

            foreach (PatIn obj in list3)
            {
                bool b = false;

                foreach (PatInRoomGroup group in group_list)
                {
                    if (group.Id.Equals(obj.Id) &&
                        group.SEQ.Equals(obj.SEQ))
                    {
                        // group に入院日がセットされておらず
                        // obj に入院日がセットされている場合（通常は無い）
                        if (!DateTimeAgent.IsDate(group.InDate) &&
                            DateTimeAgent.IsDate(obj.InDate))
                        {
                            group.InDate = obj.InDate;
                            group.InTime = obj.InTime;
                        }

                        // group に退院日がセットされておらず
                        // obj に退院日がセットされている場合（通常は無い）
                        if (!DateTimeAgent.IsDate(group.OutDate) &&
                            DateTimeAgent.IsDate(obj.OutDate))
                        {
                            group.OutDate = obj.OutDate;
                            group.OutTime = obj.OutTime;
                        }

                        group.PatInRoomList.Add(obj);
                        b = true;
                        break;
                    }
                }

                if (!b)
                {
                    PatInRoomGroup g = new PatInRoomGroup();
                    g.Id = obj.Id;
                    g.Name = obj.Name;
                    g.Kana = obj.Kana;
                    g.Sex = obj.Sex;
                    g.Birth = obj.Birth;
                    g.SEQ = obj.SEQ;
                    g.InDate = obj.InDate;
                    g.InTime = obj.InTime;
                    g.OutDate = obj.OutDate;
                    g.OutTime = obj.OutTime;

                    g.PatInRoomList.Add(obj);
                    group_list.Add(g);
                }
            }

            foreach (PatInRoomGroup group in group_list)
            {
                // ソートする
                group.PatInRoomList.Sort((x, y) =>
                {
                    int i = x.DoDate.CompareTo(y.DoDate);

                    if (i == 0)
                    {
                        i = x.DoTime.CompareTo(y.DoTime);
                    }

                    if (i == 0)
                    {
                        i = x.SEQ2 - y.SEQ2;
                    }

                    return i;
                });
            }

            return group_list;
        }
    }
}
