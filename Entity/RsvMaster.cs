using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// 予約種別
    /// 0 検査, 1 診察
    /// </summary>
    public enum RsvKind : int
    {
        Kensa = 0,
        Doctor = 1
    }

    public class RsvMaster : StdEntity
    {
        /// <summary>
        /// 予約種別
        /// </summary>
        public string Code1 = "";

        /// <summary>
        /// 予約詳細
        /// </summary>
        public string Code2 = "";

        /// <summary>
        /// 予約種別名称
        /// </summary>
        public string Name1 = "";

        /// <summary>
        /// 予約詳細名称
        /// </summary>
        public string Name2 = "";

        /// <summary>
        /// 適用開始日
        /// </summary>
        public int StartDate = 0;

        /// <summary>
        /// 適用終了日
        /// 0 の場合は現在も適用中
        /// </summary>
        public int EndDate = 0;


        public bool IsOn(int date)
        {
            bool result = false;

            if (date >= this.StartDate && (date <= this.EndDate || this.EndDate == 0))
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 科
        /// </summary>
        public string DeptCode = "";

        public string DeptName
        {
            get
            {
                string s = "";

                if (Dict.DeptDict.ContainsKey(this.DeptCode))
                {
                    s = Dict.DeptDict[this.DeptCode].ShortName;
                }

                return s;
            }
        }

        /// <summary>
        /// ＤＲ
        /// </summary>
        public string DoctorCode = "";

        public string DoctorName
        {
            get
            {
                string s = "";

                if (Dict.DoctorDict.ContainsKey(this.DoctorCode))
                {
                    s = Dict.DoctorDict[this.DoctorCode].Name;
                }

                return s;
            }
        }

        /// <summary>
        /// 予約可能な時間帯
        /// </summary>
        public int Time1 = 0;

        /// <summary>
        /// 予約可能な時間帯
        /// </summary>
        public int Time2 = 0;

        /// <summary>
        /// 予約出来ない時間帯
        /// </summary>
        public int TimeOut1 = 0;

        /// <summary>
        /// 予約出来ない時間帯
        /// </summary>
        public int TimeOut2 = 0;

        /// <summary>
        /// 表示単位（分）
        /// </summary>
        public int Interval = 0;

        /// <summary>
        /// 種別
        /// 0 検査, 1 診察
        /// </summary>
        public RsvKind Kind1 = RsvKind.Kensa;

        /// <summary>
        /// 時間枠
        /// </summary>
        public RsvWaku[,] Wakus = new RsvWaku[4, 8];

        /// <summary>
        /// 備考
        /// </summary>
        public string[] Bikous = new string[8];

        /// <summary>
        /// 所用時間
        /// </summary>
        public string[] Conts = new string[4];

        /// <summary>
        /// 予約備考
        /// </summary>
        public List<string>[] Comments = new List<string>[3];


        public RsvMaster()
        {
            this.Comments[1] = new List<string>();
            this.Comments[2] = new List<string>();
        }


/*
        /// <summary>
        /// 予約種別名称を取得する
        /// </summary>
        /// <param name="code1">予約種別コード</param>
        /// <returns></returns>
        public static RsvMaster GetName1(string code1)
        {
            RsvMaster obj = new RsvMaster();

            if (code1.Length == 0)
            {
                return obj;
            }

            string cmd = "select t.TM50RC_F04 from macs.TM50RC t " +
                " where t.TM50RC_F01 = 50 and t.TM50RC_F02 = " + code1;

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                obj.Code1 = code1;
                obj.Name1 = tmp.DataDict["TM50RC_F04"].ToString();

                break;
            }

            return obj;
        }

        /// <summary>
        /// 予約詳細名称を取得する
        /// </summary>
        /// <param name="code2"></param>
        /// <returns></returns>
        public static string GetName2(string code2)
        {
            string s = "";

            if (code2.Length == 0)
            {
                return s;
            }

            string cmd = "select t.TM50RC_F04 from macs.TM50RC t " +
                " where t.TM50RC_F01 = 51 and t.TM50RC_F02 = " + code2;

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                s = tmp.DataDict["TM50RC_F04"].ToString();
                break;
            }

            return s;
        }
 */

        /// <summary>
        /// 予約種別から、予約詳細のリストを取得する
        /// </summary>
        /// <param name="code1">予約種別コード</param>
        /// <returns></returns>
        public static List<RsvMaster> GetDetailList(string code1)
        {
            List<RsvMaster> list = new List<RsvMaster>();

            if (code1.Length == 0)
            {
                return list;
            }

            string cmd = "select t.TM50RC_F02, t.TM50RC_F04 from macs.TM50RC t " +
                " where t.TM50RC_F01 = 51 " +
                " and t.TM50RC_F02 in (select distinct t55.TM55RC_F02 from macs.TM55RC t55 where t55.TM55RC_F01 = " + code1 + ") " +
                " order by t.TM50RC_F02";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                RsvMaster obj = new RsvMaster();

                obj.Code1 = code1;
                obj.Code2 = tmp.DataDict["TM50RC_F02"].ToString();
                obj.Name2 = tmp.DataDict["TM50RC_F04"].ToString();

                list.Add(obj);
            }

            return list;
        }


        public static List<RsvMaster> GetDataList(string code1, string code2)
        {
            List<RsvMaster> list = new List<RsvMaster>();

            if (code1.Length == 0)
            {
                return list;
            }

            // 予約種別名称
            string name1 = "";

            string cmd = "select t.TM50RC_F03 from macs.TM50RC t " +
                " where t.TM50RC_F01 = 50 and t.TM50RC_F02 = " + code1;

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                name1 = tmp.DataDict["TM50RC_F03"].ToString().Trim();
                break;
            }


            // 予約詳細名称
            string name2 = "";

            if (code2.Length > 0)
            {
                cmd = "select t.TM50RC_F03 from macs.TM50RC t " +
                    " where t.TM50RC_F01 = 51 and t.TM50RC_F02 = " + code2;

                tmp_list = StdClass.GetList(Db, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    name2 = tmp.DataDict["TM50RC_F03"].ToString().Trim();
                    break;
                }
            }


            // 予約マスターのリストを作る
            cmd = "select * from macs.TM55RC t " +
                " where t.TM55RC_F01 = " + code1;

            if (code2.Length > 0)
            {
                cmd += " and t.TM55RC_F02 = " + code2;
            }
            else
            {
                cmd += " and t.TM55RC_F02 = 0";
            }

            cmd += " order by t.TM55RC_F15 desc";

            tmp_list = StdClass.GetList(Db, cmd);
            int tmp_start = 0;

            foreach (StdClass tmp in tmp_list)
            {
                RsvMaster obj = new RsvMaster();

                obj.Code1 = code1;
                obj.Name1 = name1;

                if (code2.Length > 0)
                {
                    obj.Code2 = code2;
                }
                else
                {
                    obj.Code2 = "0";
                }

                obj.Name2 = name2;

                int.TryParse(tmp.DataDict["TM55RC_F15"].ToString(), out obj.StartDate);

                if (tmp_start > 0)
                {
                    // 前のデータの前日が EndDate としてセットされる。
                    int.TryParse(DateTimeAgent.DateTimeFromInt(tmp_start).AddDays(-1).ToString("yyyyMMdd"), out obj.EndDate);
                }

                obj.DeptCode = tmp.DataDict["TM55RC_F12"].ToString();
                obj.DoctorCode = tmp.DataDict["TM55RC_F13"].ToString();

                if (tmp.DataDict["TM55RC_F14"].ToString().Equals("1"))
                {
                    obj.Kind1 = RsvKind.Doctor;
                }
                else
                {
                    obj.Kind1 = RsvKind.Kensa;
                }

                int.TryParse(tmp.DataDict["TM55RC_F04"].ToString(), out obj.Time1);
                int.TryParse(tmp.DataDict["TM55RC_F05"].ToString(), out obj.Time2);
                int.TryParse(tmp.DataDict["TM55RC_F06"].ToString(), out obj.TimeOut1);
                int.TryParse(tmp.DataDict["TM55RC_F07"].ToString(), out obj.TimeOut2);
                int.TryParse(tmp.DataDict["TM55RC_F10"].ToString(), out obj.Interval);

                tmp_start = obj.StartDate;

                list.Add(obj);
            }


            // 取得したリストについて、時間枠データを取得する

            cmd = "select * from macs.TM56RC t " +
                " where t.TM56RC_F01 = " + code1;

            if (code2.Length > 0)
            {
                cmd += " and t.TM56RC_F02 = " + code2;
            }
            else
            {
                cmd += " and t.TM56RC_F02 = 0";
            }

            cmd += " order by t.TM56RC_F11 desc";

            tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                foreach (RsvMaster obj in list)
                {
                    if (tmp.DataDict["TM56RC_F11"].ToString().Equals(obj.StartDate.ToString()))
                    {
                        for (int i = 1; i <= 7; i++)
                        {
                            obj.Wakus[1, i] = new RsvWaku();
                            obj.Wakus[1, i].Wday = i;
                            obj.Wakus[1, i].Group = RsvWakuGroup.Morning;
                            int.TryParse(tmp.DataDict["TM56RC_F05_" + i].ToString(), out obj.Wakus[1, i].Time1);
                            int.TryParse(tmp.DataDict["TM56RC_F06_" + i].ToString(), out obj.Wakus[1, i].Time2);

                            obj.Wakus[2, i] = new RsvWaku();
                            obj.Wakus[2, i].Wday = i;
                            obj.Wakus[2, i].Group = RsvWakuGroup.Noon;
                            int.TryParse(tmp.DataDict["TM56RC_F07_" + i].ToString(), out obj.Wakus[2, i].Time1);
                            int.TryParse(tmp.DataDict["TM56RC_F08_" + i].ToString(), out obj.Wakus[2, i].Time2);

                            obj.Wakus[3, i] = new RsvWaku();
                            obj.Wakus[3, i].Wday = i;
                            obj.Wakus[3, i].Group = RsvWakuGroup.Evening;
                            int.TryParse(tmp.DataDict["TM56RC_F09_" + i].ToString(), out obj.Wakus[3, i].Time1);
                            int.TryParse(tmp.DataDict["TM56RC_F10_" + i].ToString(), out obj.Wakus[3, i].Time2);
                        }

                        break;
                    }
                }
            }

            // 取得したリストについて、人数枠データを取得する

            cmd = "select * from macs.TM57RC t " +
                " where t.TM57RC_F01 = " + code1;

            if (code2.Length > 0)
            {
                cmd += " and t.TM57RC_F02 = " + code2;
            }
            else
            {
                cmd += " and t.TM57RC_F02 = 0";
            }

            cmd += " order by t.TM57RC_F11 desc";

            tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                foreach (RsvMaster obj in list)
                {
                    if (tmp.DataDict["TM57RC_F11"].ToString().Equals(obj.StartDate.ToString()))
                    {
                        for (int i = 1; i <= 7; i++)
                        {
                            int.TryParse(tmp.DataDict["TM57RC_F04_" + i].ToString(), out obj.Wakus[1, i].RsvMax);
                            int.TryParse(tmp.DataDict["TM57RC_F05_" + i].ToString(), out obj.Wakus[2, i].RsvMax);
                            int.TryParse(tmp.DataDict["TM57RC_F06_" + i].ToString(), out obj.Wakus[3, i].RsvMax);

                            int.TryParse(tmp.DataDict["TM57RC_F08_" + i].ToString(), out obj.Wakus[1, i].TodayMax);
                            int.TryParse(tmp.DataDict["TM57RC_F09_" + i].ToString(), out obj.Wakus[2, i].TodayMax);
                            int.TryParse(tmp.DataDict["TM57RC_F10_" + i].ToString(), out obj.Wakus[3, i].TodayMax);
                        }

                        break;
                    }
                }
            }

            // 取得したリストについて、所用時間コメントを取得する

            cmd = "select * from macs.TM58RC t " +
                " where t.TM58RC_F01 = " + code1;

            if (code2.Length > 0)
            {
                cmd += " and t.TM58RC_F02 = " + code2;
            }
            else
            {
                cmd += " and t.TM58RC_F02 = 0";
            }

            cmd += " order by t.TM58RC_F07 desc";

            tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                foreach (RsvMaster obj in list)
                {
                    if (tmp.DataDict["TM58RC_F07"].ToString().Equals(obj.StartDate.ToString()))
                    {
                        obj.Conts[1] = tmp.DataDict["TM58RC_F04"].ToString();
                        obj.Conts[2] = tmp.DataDict["TM58RC_F05"].ToString();
                        obj.Conts[3] = tmp.DataDict["TM58RC_F06"].ToString();

                        break;
                    }
                }
            }

            // 取得したリストについて、予約備考マスターを取得する

            cmd = "select * from 予約備考マスター t " +
                " where 予約種別コード = " + code1;

            if (code2.Length > 0)
            {
                cmd += " and 予約詳細コード = " + code2;
            }
            else
            {
                cmd += " and 予約詳細コード = 0";
            }

            cmd += " order by 備考種別, 連番";

            tmp_list = StdClass.GetList(Db, cmd);

            foreach (RsvMaster obj in list)
            {
                foreach (StdClass tmp in tmp_list)
                {
                    int i = int.Parse(tmp.DataDict["備考種別"].ToString());
                    obj.Comments[i].Add(tmp.DataDict["備考項目"].ToString());
                }
            }

            return list;
        }

        public static RsvMaster GetData(string code1, string code2, string crit_date)
        {
            RsvMaster obj = new RsvMaster();

            if (code1.Length == 0)
            {
                return obj;
            }

            string date = crit_date;

            if (crit_date.Length != 8)
            {
                date = DateTime.Now.ToString("yyyyMMdd");
            }

            List<RsvMaster> list = RsvMaster.GetDataList(code1, code2);

            foreach (RsvMaster obj2 in list)
            {
                if (obj2.StartDate > int.Parse(date))
                {
                    continue;
                }

                obj = obj2;
                break;
            }

            return obj;
        }

        /// <summary>
        /// 指定日時点で有効な予約マスターのリスト
        /// </summary>
        /// <returns></returns>
        public static List<RsvMaster> GetListByDate1(string crit_date)
        {
            List<RsvMaster> list = new List<RsvMaster>();

            string date = crit_date;

            if (crit_date.Length != 8)
            {
                date = DateTime.Now.ToString("yyyyMMdd");
            }

            string cmd = "select t1.*, t2.TM55RC_F12 科, t2.TM55RC_F13 ＤＲ, t2.TM55RC_F14 種別, t2.TM55RC_F15 開始日 " +
                " from TM50RC t1, " +
                " (select * from TM55RC t55 " +
                " where " +
                " (t55.TM55RC_F01, t55.TM55RC_F02, t55.TM55RC_F15) in " +
                " (select t5.TM55RC_F01, t5.TM55RC_F02, max(t5.TM55RC_F15) " +
                "  from TM55RC t5 " +
                "  where t5.TM55RC_F15 <= " + crit_date +
                "  group by t5.TM55RC_F01, t5.TM55RC_F02)) t2 " +
                " where t1.TM50RC_F01 = 50 and t1.TM50RC_F02 = t2.TM55RC_F01 and t2.TM55RC_F02 = 0 and t1.TM50RC_F03 is not null " +
                " order by t1.TM50RC_F02 ";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                // 名称がなければリストに追加しない
                if (tmp.DataDict["TM50RC_F03"].ToString().Trim().Length == 0)
                {
                    continue;
                }

                // 種別がなければリストに追加しない
                if (tmp.DataDict["種別"].ToString().Length == 0)
                {
                    continue;
                }

                RsvMaster obj = new RsvMaster();

                obj.Code1 = tmp.DataDict["TM50RC_F02"].ToString();
                obj.Name1 = tmp.DataDict["TM50RC_F03"].ToString().Trim();
                obj.DeptCode = tmp.DataDict["科"].ToString().Trim();
                obj.DoctorCode = tmp.DataDict["ＤＲ"].ToString().Trim();

                if (tmp.DataDict["種別"].ToString().Equals("1"))
                {
                    obj.Kind1 = RsvKind.Doctor;
                }
                else
                {
                    obj.Kind1 = RsvKind.Kensa;
                }

                int.TryParse(tmp.DataDict["開始日"].ToString(), out obj.StartDate);

                list.Add(obj);
            }

            return list;
        }

        /// <summary>
        /// 指定日時点で有効な予約マスターのリスト
        /// </summary>
        /// <returns></returns>
        public static List<RsvMaster> GetListByDate2(string crit_date)
        {
            List<RsvMaster> list = new List<RsvMaster>();

            string date = crit_date;

            if (crit_date.Length != 8)
            {
                date = DateTime.Now.ToString("yyyyMMdd");
            }

            string cmd = "select t1.TM50RC_F02, t2.TM55RC_F02, trim(t1.TM50RC_F04) 種別 " +
                " , case when t2.TM55RC_F02 > 0 then (select TM50RC_F04 from TM50RC where TM50RC_F01 = 51 and TM50RC_F02 = t2.TM55RC_F02) else null end 詳細 " +
                " , t2.TM55RC_F12, t2.TM55RC_F13, t2.TM55RC_F14, t2.開始日 from macs.TM50RC t1 " +
                " , (select t.TM55RC_F01, t.TM55RC_F02, t.TM55RC_F12, t.TM55RC_F13, t.TM55RC_F14, max(t.TM55RC_F15) 開始日 from macs.TM55RC t " +
                " where t.TM55RC_F15 <= " + date +
                " group by t.TM55RC_F01, t.TM55RC_F02, t.TM55RC_F12, t.TM55RC_F13, t.TM55RC_F14) t2 " +
                " where t1.TM50RC_F01 = 50 " +
                " and t1.TM50RC_F02 = t2.TM55RC_F01 " +
                " order by t1.TM50RC_F02, t2.TM55RC_F02";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                RsvMaster obj = new RsvMaster();

                obj.Code1 = tmp.DataDict["TM50RC_F02"].ToString();
                obj.Code2 = tmp.DataDict["TM55RC_F02"].ToString();
                obj.Name1 = tmp.DataDict["種別"].ToString();
                obj.Name2 = tmp.DataDict["詳細"].ToString();
                obj.DeptCode = tmp.DataDict["TM55RC_F12"].ToString();
                obj.DoctorCode = tmp.DataDict["TM55RC_F13"].ToString();

                if (tmp.DataDict["TM55RC_F14"].ToString().Equals("1"))
                {
                    obj.Kind1 = RsvKind.Doctor;
                }
                else
                {
                    obj.Kind1 = RsvKind.Kensa;
                }

                int.TryParse(tmp.DataDict["開始日"].ToString(), out obj.StartDate);

                // 詳細コードが有っても、詳細名がなければリストに追加しない
                if (obj.Code2.Length > 0 && !obj.Code2.Equals("0") && obj.Name2.Length == 0)
                {
                    continue;
                }

                list.Add(obj);
            }

            return list;
        }
    }

    /// <summary>
    /// 時間帯（朝・昼・夕）
    /// </summary>
    public enum RsvWakuGroup : int
    {
        Morning = 1,
        Noon = 2,
        Evening = 3
    }

    public class RsvWaku
    {
        /// <summary>
        /// 曜日。1 日, 2 月, 3 火, 4 水, 5 木, 6 金, 7 土
        /// </summary>
        public int Wday = 0;

        /// <summary>
        /// 時間帯（朝・昼・夕）
        /// </summary>
        public RsvWakuGroup Group = RsvWakuGroup.Morning;

        /// <summary>
        /// 開始
        /// </summary>
        public int Time1 = 0;

        /// <summary>
        /// 終了
        /// </summary>
        public int Time2 = 0;

        public int Interval
        {
            get
            {
                int i = 0;

                i = DateTimeAgent.IntervalMinutes(this.Time1, this.Time2);

                return i;
            }
        }

        /// <summary>
        /// 予約最大数
        /// </summary>
        public int RsvMax = 0;

        /// <summary>
        /// 当日最大数
        /// </summary>
        public int TodayMax = 0;
    }

/*
    class StartEndDate
    {
        public int StartDate = 0;

        public int EndDate = 0;

        /// <summary>
        /// StartDate にもとづいて EndDate をセットする
        /// </summary>
        /// <param name="list">StartDate が降順になっているリスト</param>
        /// <returns></returns>
        public static List<StartEndDate> MakeEndDate(List<StartEndDate> list)
        {
            int tmp_start = 0;

            foreach (StartEndDate tmp in list)
            {
                // 最初は EndDate = 0 のまま。tmp_start をセットして次へ。
                if (tmp_start == 0)
                {
                    tmp_start = tmp.StartDate;
                    continue;
                }

                // 前のデータの前日が EndDate としてセットされる。
                int.TryParse(DateTimeAgent.DateTimeFromInt(tmp_start).AddDays(-1).ToString("yyyyMMdd"), out tmp.EndDate);

                tmp_start = tmp.StartDate;
            }

            return list;
        }
    }

 */
}
