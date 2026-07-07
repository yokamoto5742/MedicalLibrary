using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class PatOpeOrder2 : PatOpeOrder
    {
        /// <summary>
        /// 手術室コード
        /// </summary>
        public string OpeRoom = "0";

        /// <summary>
        /// 手術室
        /// </summary>
        public string OpeRoomName
        {
            get
            {
                string result = "";

                if (OpeRoomDict.ContainsKey(OpeRoom))
                {
                    result = OpeRoomDict[OpeRoom];
                }

                return result;
            }
        }

        /// <summary>
        /// 手術室コード
        /// </summary>
        public int OpeRoomNum
        {
            get
            {
                int result = 0;

                int.TryParse(OpeRoom, out result);

                return result;
            }
        }

        /// <summary>
        /// 入室時刻
        /// </summary>
        public int StartTime = 0;

        /// <summary>
        /// 入室時刻
        /// </summary>
        public string StartTimeString
        {
            get
            {
                return DateTimeAgent.TimeFormat(StartTime, false);
            }
        }

        /// <summary>
        /// 退室時刻
        /// </summary>
        public int EndTime = 0;

        /// <summary>
        /// 退室時刻
        /// </summary>
        public string EndTimeString
        {
            get
            {
                return DateTimeAgent.TimeFormat(EndTime, false);
            }
        }

        /// <summary>
        /// 入退室時間
        /// H:mm-H:mm または 未定
        /// </summary>
        public string StartEndTimeString
        {
            get
            {
                string result = "(未定)";

                if (StartTimeString.Length > 0)
                {
                    result = StartTimeString + "-" + EndTimeString;
                }

                return result;
            }
        }

        /// <summary>
        /// 所要時間（分）
        /// </summary>
        public int Minutes = 0;

        /// <summary>
        /// コメント
        /// </summary>
        public string Cont = "";

        /// <summary>
        /// 印刷済み 0 未, 1 済
        /// </summary>
        public int Print1 = 0;

        /// <summary>
        /// 印刷済み
        /// </summary>
        public string PrintMark1
        {
            get
            {
                string result = "";

                if (Print1.Equals(1))
                {
                    result = "○";
                }

                return result;
            }
        }

        /// <summary>
        /// ラベル印刷済み 0 未, 1 済
        /// </summary>
        public int Print2 = 0;

        /// <summary>
        /// ラベル印刷済み
        /// </summary>
        public string PrintMark2
        {
            get
            {
                string result = "";

                if (Print2.Equals(1))
                {
                    result = "○";
                }

                return result;
            }
        }

        /// <summary>
        /// 麻酔装置への送信済み 0 未, 1 済
        /// </summary>
        public int Sent1 = 0;

        /// <summary>
        /// 麻酔装置への送信済み
        /// </summary>
        public string SentMark1
        {
            get
            {
                string result = "";

                if (Sent1.Equals(1))
                {
                    result = "○";
                }

                return result;
            }
        }

        /// <summary>
        /// Time Free
        /// </summary>
        public bool TimeFree = false;

        public string TimeFreeMark
        {
            get
            {
                string result = "";

                if (TimeFree)
                {
                    result = "◆";
                }

                return result;
            }
        }

        /// <summary>
        /// 緊急・時間帯
        /// 0: 通常, 1: 予約外, 2: 緊急
        /// </summary>
        public int TimeOut = 0;

        /// <summary>
        /// 緊急・時間帯
        /// "通常", "予約外", "緊急"
        /// </summary>
        public string TimeOutString
        {
            get
            {
                string result = "";

                if (TimeOut.Equals(0))
                {
                    result = "通常";
                }
                else if (TimeOut.Equals(1))
                {
                    result = "予約外";
                }
                else if (TimeOut.Equals(2))
                {
                    result = "緊急";
                }

                return result;
            }
        }

        /// <summary>
        /// 緊急・時間帯
        /// "通常", "予約外", "緊急"
        /// </summary>
        public string TimeOutStringShort
        {
            get
            {
                string result = "";

                if (TimeOut.Equals(0))
                {
                    result = "通常";
                }
                else if (TimeOut.Equals(1))
                {
                    result = "予約外";
                }
                else if (TimeOut.Equals(2))
                {
                    result = "緊急";
                }

                return result;
            }
        }

        /// <summary>
        /// 緊急・時間帯
        /// "", "▲", "●"
        /// </summary>
        public string TimeOutMark
        {
            get
            {
                string result = "";

                if (TimeOut.Equals(1))
                {
                    result = "▲";
                }
                else if (TimeOut.Equals(2))
                {
                    result = "●";
                }

                return result;
            }
        }

        /// <summary>
        /// 緊急
        /// "", "▲", "●"
        /// </summary>
        public string TimeOutMarkShort
        {
            get
            {
                string result = "";

                if (TimeOut.Equals(1))
                {
                    result = "▲";
                }
                else if (TimeOut.Equals(2))
                {
                    result = "●";
                }

                return result;
            }
        }

        /// <summary>
        /// 診療科コード
        /// </summary>
        string _Dept = "";

        public string Dept
        {
            get
            {
                string s = "0";

                if (this._Dept.Length > 0 && !this._Dept.Equals("0"))
                {
                    s = this._Dept;
                }
                else
                {
                    if (this.OpePlace.Contains("南館"))
                    {
                        // 南館の場合は眼科
                        s = "7";
                    }
                }

                return s;
            }
            set
            {
                this._Dept = value;
            }
        }

        /// <summary>
        /// 診療科名称
        /// </summary>
        public string DeptName
        {
            get
            {
                string result = "";

                if (Dict.DeptDict.ContainsKey(Dept))
                {
                    result = Dict.DeptDict[Dept].ShortName;
                }

                return result;
            }
        }
/*
        /// <summary>
        /// 診療科略称
        /// </summary>
        public string DeptShort
        {
            get
            {
                string result = "";

                if (Dict.DeptDict.ContainsKey(Dept))
                {
                    result = Dict.DeptDict[Dept].ShortName;
                }

                return result;
            }
        }
*/
        /// <summary>
        /// 感染症
        /// </summary>
        public string Infection = "";

        /// <summary>
        /// 感染症有無フラグ
        /// </summary>
        public string InfectionFlg
        {
            get
            {
                string result = "-";

                if (Infection.Contains("+"))
                {
                    result = "+";
                }

                return result;
            }
        }

        public string Doctor1 = "";
        public string Doctor2 = "";
        public string Doctor3 = "";

        /// <summary>
        /// 麻酔医師（括弧つき）
        /// </summary>
        public string Doctor3WithBrackets
        {
            get
            {
                string result = "";

                if (Doctor3.Length > 0)
                {
                    result = "(" + Doctor3 + ")";
                }

                return result;
            }
        }

        public string Ns1 = "";
        public string Ns2 = "";
        public string Ns3 = "";
        public string Ns4 = "";

        public string Staff = "";

        /// <summary>
        /// スタッフ名
        /// </summary>
        public string StaffName
        {
            get
            {
                string result = "";

                if (Dict.StaffDict.ContainsKey(Staff))
                {
                    result = Dict.StaffDict[Staff].Name;
                }

                return result;
            }
        }

        /// <summary>
        /// スタッフ名（括弧つき）
        /// </summary>
        public string StaffNameWithBrackets
        {
            get
            {
                string result = "";

                if (Dict.StaffDict.ContainsKey(Staff))
                {
                    result = "(" + Dict.StaffDict[Staff].Name + ")";
                }

                return result;
            }
        }

        public string SaveDate = "";

        public string SaveDateString
        {
            get
            {
                return DateTimeAgent.DateFormat(SaveDate, DateTimeAgent.DateFormatKind.SHORT);
            }
        }

        public string SaveTime = "";

        public string SaveTimeString
        {
            get
            {
                string result = "";

                if (SaveTime.Length > 0)
                {
                    result = SaveTime.PadLeft(6, '0').Substring(0, 4).Insert(2, ":");
                }

                return result;
            }
        }

        /// <summary>
        /// ステータス。デフォルト 0, キャンセル 9
        /// </summary>
        public string Status = "0";

        /// <summary>
        /// Status が 9 ならば true を返す。
        /// </summary>
        public bool IsCanceled
        {
            get
            {
                bool result = false;

                if (Status.Equals("9"))
                {
                    result = true;
                }

                return result;
            }
        }

        static Dictionary<string, string> opeRoomDict = new Dictionary<string, string>();

        public static Dictionary<string, string> OpeRoomDict
        {
            get
            {
                if (opeRoomDict.Count == 0)
                {
                    opeRoomDict.Add("1", "中1");
                    opeRoomDict.Add("2", "中2");
                    opeRoomDict.Add("3", "中3");
                    opeRoomDict.Add("11", "南1");
                    opeRoomDict.Add("12", "南2");
                    opeRoomDict.Add("13", "南3");
                }

                return opeRoomDict;
            }
        }

        // データベースに登録されている情報
        string _OpeName = "";
        string _OpeAnes = "";
        string _OpePart = "";

        /// <summary>
        /// 術式
        /// </summary>
        public new string OpeName
        {
            get
            {
                string result = "";

                if (this._OpeName.Length > 0)
                {
                    // データが登録されている場合
                    result = this._OpeName;
                }
                else
                {
                    // データが登録されていなければ手術指示から取得する
                    result = base.OpeName;
                }

                return result;
            }
            set
            {
                this._OpeName = value;
            }
        }

        /// <summary>
        /// 麻酔
        /// </summary>
        public new string OpeAnes
        {
            get
            {
                string result = "";

                if (this._OpeAnes.Length > 0)
                {
                    result = this._OpeAnes;
                }
                else
                {
                    result = base.OpeAnes;
                }

                return result;
            }
            set
            {
                this._OpeAnes = value;
            }
        }

        /// <summary>
        /// 部位
        /// </summary>
        public string OpePart
        {
            get
            {
                return this._OpePart;
            }
            set
            {
                this._OpePart = value;
            }
        }

        public PatOpeOrder2()
        {
        }

        public PatOpeOrder2(StdClass tmp)
            : base(tmp)
        {
            if (this.Pat.Id.Length == 0) this.Pat.Id = tmp.GetDataString("PATIENT_ID");
            if (this.Date <= 0) this.Date = tmp.GetDataInt("OPE_DATE", 0);
            this.OpeRoom = tmp.GetDataString("OPE_ROOM");
            this.StartTime = tmp.GetDataInt("START_TIME");
            this.EndTime = tmp.GetDataInt("END_TIME");
            this.Minutes = tmp.GetDataInt("MINUTES", 0);
            this.Cont = tmp.GetDataString("CONT");
            this.Print1 = tmp.GetDataInt("PRINT1");
            this.Print2 = tmp.GetDataInt("PRINT2");
            this.Sent1 = tmp.GetDataInt("SENT1");
            this.TimeFree = tmp.GetDataInt("TIME_FREE").Equals(1);
            this.TimeOut = tmp.GetDataInt("TIME_OUT");
            this.Infection = tmp.GetDataString("INFECTION");
            this.Doctor1 = tmp.GetDataString("DOCTOR1");
            this.Doctor2 = tmp.GetDataString("DOCTOR2");
            this.Doctor3 = tmp.GetDataString("DOCTOR3");
            this.Ns1 = tmp.GetDataString("NS1");
            this.Ns2 = tmp.GetDataString("NS2");
            this.Ns3 = tmp.GetDataString("NS3");
            this.Ns4 = tmp.GetDataString("NS4");
            this.Staff = tmp.GetDataString("STAFF");
            this.SaveDate = tmp.GetDataString("SAVE_DATE");
            this.SaveTime = tmp.GetDataString("SAVE_TIME");
            this.Status = tmp.GetDataString("STATUS");

            if (this._Dept.Length == 0) this._Dept = tmp.GetDataString("DEPT");
            this._OpeName = tmp.GetDataString("OPE_NAME");
            this._OpePart = tmp.GetDataString("OPE_PART");
            this._OpeAnes = tmp.GetDataString("OPE_ANES");
        }

        public void Merge(PatOpeOrder2 tmp)
        {
            if (this.Pat.Id.Length == 0) this.Pat.Id = tmp.Pat.Id;
            if (this.Date <= 0) this.Date = tmp.Date;
            this.OpeRoom = tmp.OpeRoom;
            this.StartTime = tmp.StartTime;
            this.EndTime = tmp.EndTime;
            this.Minutes = tmp.Minutes;
            this.Cont = tmp.Cont;
            this.Print1 = tmp.Print1;
            this.Print2 = tmp.Print2;
            this.Sent1 = tmp.Sent1;
            this.TimeFree = tmp.TimeFree;
            this.TimeOut = tmp.TimeOut;
            this.Infection = tmp.Infection;
            this.Doctor1 = tmp.Doctor1;
            this.Doctor2 = tmp.Doctor2;
            this.Doctor3 = tmp.Doctor3;
            this.Ns1 = tmp.Ns1;
            this.Ns2 = tmp.Ns2;
            this.Ns3 = tmp.Ns3;
            this.Ns4 = tmp.Ns4;
            this.Staff = tmp.Staff;
            this.SaveDate = tmp.SaveDate;
            this.SaveTime = tmp.SaveTime;
            this.Status = tmp.Status;

            if (this._Dept.Length == 0) this._Dept = tmp.Dept;
            this._OpeName = tmp._OpeName;
            this._OpePart = tmp._OpePart;
            this._OpeAnes = tmp._OpeAnes;
        }

        /// <summary>
        /// 当該日の手術指示データ一覧を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public new static List<PatOpeOrder2> Load(int date)
        {
            List<PatOpeOrder2> list = new List<PatOpeOrder2>();

            if (date.ToString().Length != 8)
            {
                return list;
            }
#if INNO
            string cmd = "Select td.*, tm.P_NAME, tm.P_SEX, tm.P_BIRTHDAY_AD " +
                " from D_DIRECTION td inner join M_PATIENT tm on td.P_ID = tm.P_ID " +
                " where DIRECTION_DATE = " + date +
                " and CATEGORY_TYPE = 12 " +
                " order by td.P_ID, td.SEQ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
#else
            string cmd = "Select 患者コード, 指示コード, 連番, 入力値, Trim(IM01RC_F04) as 氏名, Trim(IM01RC_F05) as 性別, Trim(IM01RC_F10) as 生年月日, 日付 " +
                " from PATH手術指示データ inner join IM01RC on 患者コード = IM01RC_F01 " +
                " where 日付 = " + date +
                " order by 患者コード, 指示コード, 連番";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif
            List<string> pt_list = new List<string>();

            foreach (StdClass tmp in tmp_list)
            {
                bool flg = false;

                PatOpeOrder2 ope = new PatOpeOrder2(tmp);
                PatOpeOrderData data = new PatOpeOrderData(tmp);

                foreach (PatOpeOrder2 p in list)
                {
                    if (p.Pat.Id.Equals(ope.Pat.Id) && p.Date.Equals(ope.Date))
                    {
                        if (!p.DictData.ContainsKey(data.KeyCode))
                        {
                            p.DictData.Add(data.KeyCode, data);
                        }

                        flg = true;
                        break;
                    }
                }

                if (!flg)
                {
                    ope.DictData.Add(data.KeyCode, data);
                    list.Add(ope);

                    pt_list.Add(ope.Pat.Id);
                }
            }

            cmd = "select * from OPE_ORDER " +
                " where OPE_DATE = " + date +
                " order by PATIENT_ID";

            tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatOpeOrder2 p = new PatOpeOrder2(tmp);

                foreach (PatOpeOrder2 ope2 in list)
                {
                    if (ope2.Pat.Id.Equals(p.Pat.Id) && ope2.Date.Equals(p.Date))
                    {
                        ope2.Merge(p);
                        break;
                    }
                }
            }

            if (pt_list.Count > 0)
            {
                Dictionary<string, PatIn> dict = PatIn.GetDict(pt_list);

                foreach (PatOpeOrder2 ope3 in list)
                {
                    if (dict.ContainsKey(ope3.Pat.Id) && dict[ope3.Pat.Id].InOut.Equals("2"))
                    {
                        // Dept が登録されていなければセットする
                        if (ope3.Dept.Length == 0)
                        {
                            ope3.Dept = dict[ope3.Pat.Id].Dept;
                        }

                        ope3.Pat.Doctor = dict[ope3.Pat.Id].Doctor;
                        ope3.Pat.Ward = dict[ope3.Pat.Id].Ward;
                        ope3.Pat.Room = dict[ope3.Pat.Id].Room;
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 当該日の手術指示データ一覧を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <param name="pt_list">対象患者リスト</param>
        /// <returns></returns>
        public new static List<PatOpeOrder2> Load(int date, List<string> pt_list)
        {
            List<PatOpeOrder2> list = new List<PatOpeOrder2>();

            if (date.ToString().Length != 8 || pt_list.Count == 0 || AppString.ConcatList(pt_list, ",").Length == 0)
            {
                return list;
            }
#if INNO
            string cmd = "Select td.*, tm.P_NAME, tm.P_SEX, tm.P_BIRTHDAY_AD " +
                " from D_DIRECTION td inner join M_PATIENT tm on td.P_ID = tm.P_ID " +
                " where DIRECTION_DATE = " + date +
                " and CATEGORY_TYPE = 12 " +
                " and td.P_ID in (" + AppString.ConcatList(pt_list, ",") + ") " +
                " order by td.P_ID, td.SEQ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
#else
            string cmd = "Select 患者コード, 指示コード, 連番, 入力値, Trim(IM01RC_F04) as 氏名, Trim(IM01RC_F05) as 性別, Trim(IM01RC_F10) as 生年月日, 日付 " +
                " from PATH手術指示データ inner join IM01RC on 患者コード = IM01RC_F01 " +
                " where 日付 = " + date +
                " and 患者コード in (" + AppString.ConcatList(pt_list, ",") + ") " +
                " order by 患者コード, 指示コード, 連番";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif
            foreach (StdClass tmp in tmp_list)
            {
                bool flg = false;

                PatOpeOrder2 ope = new PatOpeOrder2(tmp);
                PatOpeOrderData data = new PatOpeOrderData(tmp);

                foreach (PatOpeOrder2 p in list)
                {
                    if (p.Pat.Id.Equals(ope.Pat.Id) && p.Date.Equals(ope.Date))
                    {
                        if (!p.DictData.ContainsKey(data.KeyCode))
                        {
                            p.DictData.Add(data.KeyCode, data);
                        }

                        flg = true;
                        break;
                    }
                }

                if (!flg)
                {
                    ope.DictData.Add(data.KeyCode, data);
                    list.Add(ope);
                }
            }

            cmd = "select * from OPE_ORDER " +
                " where OPE_DATE = " + date +
                " and PATIENT_ID in (" + AppString.ConcatList(pt_list, ",") + ") " +
                " order by PATIENT_ID";

            tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatOpeOrder2 p = new PatOpeOrder2(tmp);

                foreach (PatOpeOrder2 ope2 in list)
                {
                    if (ope2.Pat.Id.Equals(p.Pat.Id) && ope2.Date.Equals(p.Date))
                    {
                        ope2.Merge(p);
                        break;
                    }
                }
            }

            if (pt_list.Count > 0)
            {
                Dictionary<string, PatIn> dict = PatIn.GetDict(pt_list);

                foreach (PatOpeOrder2 ope3 in list)
                {
                    if (dict.ContainsKey(ope3.Pat.Id) && dict[ope3.Pat.Id].InOut.Equals("2"))
                    {
                        // Dept が登録されていなければセットする
                        if (ope3.Dept.Length == 0)
                        {
                            ope3.Dept = dict[ope3.Pat.Id].Dept;
                        }

                        ope3.Pat.Doctor = dict[ope3.Pat.Id].Doctor;
                        ope3.Pat.Ward = dict[ope3.Pat.Id].Ward;
                        ope3.Pat.Room = dict[ope3.Pat.Id].Room;
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 当該期間の手術指示データ一覧を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public new static List<PatOpeOrder2> Load(int start_date, int end_date)
        {
            List<PatOpeOrder2> list = new List<PatOpeOrder2>();

            if (start_date.ToString().Length != 8 || end_date.ToString().Length != 8)
            {
                return list;
            }
#if INNO
            string cmd = "Select td.*, tm.P_NAME, tm.P_SEX, tm.P_BIRTHDAY_AD " +
                " from D_DIRECTION td inner join M_PATIENT tm on td.P_ID = tm.P_ID " +
                " where DIRECTION_DATE >= " + start_date + " and DIRECTION_DATE <= " + end_date +
                " and CATEGORY_TYPE = 12 " +
                " order by td.DIRECTION_DATE, td.P_ID, td.SEQ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
#else
            string cmd = "Select 患者コード, 指示コード, 連番, 入力値, Trim(IM01RC_F04) as 氏名, Trim(IM01RC_F05) as 性別, Trim(IM01RC_F10) as 生年月日, 日付 " +
                " from PATH手術指示データ inner join IM01RC on 患者コード = IM01RC_F01 " +
                " where 日付 >= " + start_date + " and 日付 <= " + end_date +
                " order by 日付, 患者コード, 指示コード, 連番";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif
            List<string> pt_list = new List<string>();

            foreach (StdClass tmp in tmp_list)
            {
                bool flg = false;

                PatOpeOrder2 ope = new PatOpeOrder2(tmp);
                PatOpeOrderData data = new PatOpeOrderData(tmp);

                foreach (PatOpeOrder p in list)
                {
                    if (p.Pat.Id.Equals(ope.Pat.Id) && p.Date.Equals(ope.Date))
                    {
                        if (!p.DictData.ContainsKey(data.KeyCode))
                        {
                            p.DictData.Add(data.KeyCode, data);
                        }

                        flg = true;
                        break;
                    }
                }

                if (!flg)
                {
                    ope.DictData.Add(data.KeyCode, data);
                    list.Add(ope);

                    pt_list.Add(ope.Pat.Id);
                }
            }

            cmd = "select * from OPE_ORDER " +
                " where OPE_DATE >= " + start_date + " and OPE_DATE <= " + end_date +
                " order by OPE_DATE, PATIENT_ID";

            tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatOpeOrder2 p = new PatOpeOrder2(tmp);

                foreach (PatOpeOrder2 ope2 in list)
                {
                    if (ope2.Pat.Id.Equals(p.Pat.Id) && ope2.Date.Equals(p.Date))
                    {
                        ope2.Merge(p);
                        break;
                    }
                }
            }

            if (pt_list.Count > 0)
            {
                Dictionary<string, PatIn> dict = PatIn.GetDict(pt_list);

                foreach (PatOpeOrder2 ope3 in list)
                {
                    if (dict.ContainsKey(ope3.Pat.Id) && dict[ope3.Pat.Id].InOut.Equals("2"))
                    {
                        // Dept が登録されていなければセットする
                        if (ope3.Dept.Length == 0)
                        {
                            ope3.Dept = dict[ope3.Pat.Id].Dept;
                        }

                        ope3.Pat.Doctor = dict[ope3.Pat.Id].Doctor;
                        ope3.Pat.Ward = dict[ope3.Pat.Id].Ward;
                        ope3.Pat.Room = dict[ope3.Pat.Id].Room;
                    }
                }
            }

            list.Sort(delegate(PatOpeOrder2 o1, PatOpeOrder2 o2)
            {
                if (o1.Date != o2.Date)
                {
                    return o1.Date > o2.Date ? 1 : -1;
                }
                else if (o1.OpeRoomNum != o2.OpeRoomNum)
                {
                    return o1.OpeRoomNum > o2.OpeRoomNum ? 1 : -1;
                }
                else if (o1.TimeFree != o2.TimeFree)
                {
                    return o1.TimeFree && !o2.TimeFree ? 1 : -1;
                }
                else if (o1.StartTime != o2.StartTime)
                {
                    return o1.StartTime > o2.StartTime ? 1 : -1;
                }
                else
                {
                    return 0;
                }
            });

            return list;
        }

        public StdReturn Save()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();

            obj.Table = "OPE_ORDER";
            obj.Db = DB.Db2;

            obj.DataList.Add(new StdDbColumn("OPE_ROOM", StdDbType.NUMBER, this.OpeRoom));
            obj.DataList.Add(new StdDbColumn("START_TIME", StdDbType.NUMBER, this.StartTime));
            obj.DataList.Add(new StdDbColumn("END_TIME", StdDbType.NUMBER, this.EndTime));
            obj.DataList.Add(new StdDbColumn("MINUTES", StdDbType.NUMBER, this.Minutes));
            obj.DataList.Add(new StdDbColumn("CONT", StdDbType.VARCHAR2, this.Cont));
            obj.DataList.Add(new StdDbColumn("PRINT1", StdDbType.NUMBER, this.Print1));
            obj.DataList.Add(new StdDbColumn("PRINT2", StdDbType.NUMBER, this.Print2));
            obj.DataList.Add(new StdDbColumn("SENT1", StdDbType.NUMBER, this.Sent1));
            obj.DataList.Add(new StdDbColumn("TIME_FREE", StdDbType.NUMBER, this.TimeFree));
            obj.DataList.Add(new StdDbColumn("TIME_OUT", StdDbType.NUMBER, this.TimeOut));
            obj.DataList.Add(new StdDbColumn("DEPT", StdDbType.NUMBER, this.Dept));
            obj.DataList.Add(new StdDbColumn("INFECTION", StdDbType.VARCHAR2, this.Infection));
            obj.DataList.Add(new StdDbColumn("DOCTOR1", StdDbType.VARCHAR2, this.Doctor1));
            obj.DataList.Add(new StdDbColumn("DOCTOR2", StdDbType.VARCHAR2, this.Doctor2));
            obj.DataList.Add(new StdDbColumn("DOCTOR3", StdDbType.VARCHAR2, this.Doctor3));
            obj.DataList.Add(new StdDbColumn("NS1", StdDbType.VARCHAR2, this.Ns1));
            obj.DataList.Add(new StdDbColumn("NS2", StdDbType.VARCHAR2, this.Ns2));
            obj.DataList.Add(new StdDbColumn("NS3", StdDbType.VARCHAR2, this.Ns3));
            obj.DataList.Add(new StdDbColumn("NS4", StdDbType.VARCHAR2, this.Ns4));
            obj.DataList.Add(new StdDbColumn("STAFF", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("SAVE_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("SAVE_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, this.Status));
            obj.DataList.Add(new StdDbColumn("OPE_NAME", StdDbType.VARCHAR2, this._OpeName));
            obj.DataList.Add(new StdDbColumn("OPE_PART", StdDbType.VARCHAR2, this._OpePart));
            obj.DataList.Add(new StdDbColumn("OPE_ANES", StdDbType.VARCHAR2, this._OpeAnes));

            obj.WhereList.Add("PATIENT_ID = " + this.Pat.Id);
            obj.WhereList.Add("OPE_DATE = " + this.Date);

            sr = obj.UpdateSQL();

            if (sr.IntValue == 0)
            {
                obj.DataList.Add(new StdDbColumn("PATIENT_ID", StdDbType.NUMBER, this.Pat.Id));
                obj.DataList.Add(new StdDbColumn("OPE_DATE", StdDbType.NUMBER, this.Date));

                sr = obj.InsertSQL();
            }

            return sr;
        }

        /// <summary>
        /// 該当レコードのフラグを立てる
        /// </summary>
        /// <param name="date"></param>
        /// <param name="pt_list"></param>
        /// <returns></returns>
        public static bool SetFlg(Flg flg, int date, List<string> pt_list)
        {
            if (date.ToString().Length != 8 || pt_list.Count == 0)
            {
                return false;
            }

            string pt_list_str = "";
            List<string> pt_list2 = new List<string>();

            foreach (string s in pt_list)
            {
                if (pt_list_str.Length > 0)
                {
                    pt_list_str += ",";
                }

                pt_list_str += s;
                pt_list2.Add(s);
            }

            string cmd = "select PATIENT_ID from OPE_ORDER " +
                " where OPE_DATE = " + date + " and PATIENT_ID in (" + pt_list_str + ")";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            // OPE_ORDER に当該日にレコードが存在するものは除く。
            foreach (StdClass tmp in tmp_list)
            {
                if (pt_list2.Contains(tmp.DataDict["PATIENT_ID"].ToString()))
                {
                    pt_list2.Remove(tmp.DataDict["PATIENT_ID"].ToString());
                }
            }

            string sql_update = "";
            string sql_insert = "";

            if (flg == Flg.Print1)
            {
                sql_update = "PRINT1";
                sql_insert = "1, 0, 0";
            }
            else if (flg == Flg.Print2)
            {
                sql_update = "PRINT2";
                sql_insert = "0, 1, 0";
            }
            else if (flg == Flg.Sent1)
            {
                sql_update = "SENT1";
                sql_insert = "0, 0, 1";
            }
            else
            {
                return false;
            }

            // OPE_ORDER にレコードが存在するものはフラグを更新する。
            cmd = "update OPE_ORDER set " + sql_update + " = 1 where OPE_DATE = " + date + " and PATIENT_ID in (" + pt_list_str + ")";
            DB.Db2.ExecuteNonQuery(cmd);

            // OPE_ORDER にレコードが存在しないものは作成する。
            foreach (string s in pt_list2)
            {
                cmd = "insert into OPE_ORDER " +
                    " (PATIENT_ID, OPE_DATE, OPE_ROOM, START_TIME, END_TIME, MINUTES, CONT, PRINT1, PRINT2, SENT1, TIME_FREE, TIME_OUT, DEPT, INFECTION, DOCTOR1, DOCTOR2, DOCTOR3, NS1, NS2, NS3, NS4) " +
                    " values " +
                    " (" + s + ", " + date + ", '0', 0, 0, 0, null, " + sql_insert + ", 0, 0, 0, null, null, null, null, null, null, null, null)";

                DB.Db2.ExecuteNonQuery(cmd);
            }

            return true;
        }

        /// <summary>
        /// フラグ種別
        /// </summary>
        public enum Flg : int
        {
            Print1 = 1,
            Print2 = 2,
            Sent1 = 3
        }
    }
}
