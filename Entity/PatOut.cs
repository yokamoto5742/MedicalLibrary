using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class PatOut : PatBase
    {
        /// <summary>
        /// 受付日
        /// </summary>
        public string ComeDate = "";

        /// <summary>
        /// 受付番号（通番）
        /// </summary>
        public string Seq1 = "";

        /// <summary>
        /// 連番
        /// </summary>
        public string Seq2 = "";

        /// <summary>
        /// 受付番号（科番）
        /// </summary>
        public string Seq3 = "";

        /// <summary>
        /// 受付種別コード
        /// </summary>
        public string Kind = "";

        /// <summary>
        /// 受付種別
        /// </summary>
        public string KindName = "";

        /// <summary>
        /// 外来区分
        /// 1: 通常, 2: 救急
        /// →　inno では 0: 通常, 1: 救急, 2: 在宅
        /// </summary>
        public string Mode = "";

        /// <summary>
        /// 外来区分
        /// 1: 通常, 2: 救急
        /// →　inno では 0: 通常, 1: 救急, 2: 在宅
        /// </summary>
        public string ModeName
        {
            get
            {
                string s = "";
#if INNO
                if (this.Mode.Equals("1"))
                {
                    s = "救急";
                }
                else if (this.Mode.Equals("2"))
                {
                    s = "在宅";
                }
                else
                {
                    s = "通常";
                }
#else
                if (this.Mode.Equals("1"))
                {
                    s = "通常";
                }
                else if (this.Mode.Equals("2"))
                {
                    s = "救急";
                }
#endif
                return s;
            }
        }

        /// <summary>
        /// 受付日時
        /// </summary>
        public string DateTime1
        {
            get
            {
                return DateTimeAgent.DateFormat(this.ComeDate, DateTimeAgent.DateFormatKind.LONG) + " " + DateTimeAgent.TimeFormat(this.Time1);
            }
        }

        /// <summary>
        /// 受付時刻
        /// </summary>
        public string Time1 = "";

        public string TimeString1
        {
            get
            {
#if INNO
                return DateTimeAgent.TimeFormat6(this.Time1, 4, false);
#else
                return DateTimeAgent.TimeFormat(this.Time1);
#endif
            }
        }

        /// <summary>
        /// 診察開始
        /// </summary>
        public string Time2 = "";

        public string TimeString2
        {
            get
            {
#if INNO
                return DateTimeAgent.TimeFormat6(this.Time2, 4, false);
#else
                return DateTimeAgent.TimeFormat(this.Time2);
#endif
            }
        }

        /// <summary>
        /// 診察中断
        /// </summary>
        public string Time3 = "";

        public string TimeString3
        {
            get
            {
#if INNO
                return DateTimeAgent.TimeFormat6(this.Time3, 4, false);
#else
                return DateTimeAgent.TimeFormat(this.Time3);
#endif
            }
        }

        /// <summary>
        /// 診察終了
        /// </summary>
        public string Time4 = "";

        public string TimeString4
        {
            get
            {
#if INNO
                return DateTimeAgent.TimeFormat6(this.Time4, 4, false);
#else
                return DateTimeAgent.TimeFormat(this.Time4);
#endif
            }
        }

        /// <summary>
        /// 会計
        /// </summary>
        public string Time5 = "";

        public string TimeString5
        {
            get
            {
#if INNO
                return DateTimeAgent.TimeFormat6(this.Time5, 4, false);
#else
                return DateTimeAgent.TimeFormat(this.Time5);
#endif
            }
        }

        /// <summary>
        /// 会計入力者
        /// </summary>
        public string Kaikei = "";

        /// <summary>
        /// 会計入力者
        /// </summary>
        public string KaikeiName
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(this.Kaikei))
                {
                    s = Dict.StaffDict[this.Kaikei].Name;
                }

                return s;
            }
        }

        /// <summary>
        /// 外来患者リストを取得する。
        /// </summary>
        /// <param name="come_date">受付日（yyyyMMdd）</param>
        /// <param name="dept">診療科コード。指定必須。</param>
        /// <param name="doctor">医師コード。指定がなければ "" で可</param>
        /// <returns></returns>
        public static List<PatOut> GetList(string come_date, string dept, string doctor)
        {
            List<PatOut> tmpList = new List<PatOut>();

            if (come_date.Length != 8)
            {
                return tmpList;
            }

#if INNO
            string sqlDept = "";

            if (dept.Length > 0)
            {
                sqlDept = " and (CHANGE_DEPT = " + dept + " or (DEPT = " + dept + " and (CHANGE_DEPT is null or CHANGE_DEPT = 0)))";
            }

            string sqlDoctor = "";

            if (doctor.Length > 0)
            {
                sqlDoctor = " and (SHINSATSU_DR = " + doctor +
                    " or (CHANGE_DR = " + doctor + " and (SHINSATSU_DR is null or SHINSATSU_DR = 0)) " +
                    " or (DR = " + doctor + " and (CHANGE_DR is null or CHANGE_DR = 0) and (SHINSATSU_DR is null or SHINSATSU_DR = 0))" +
                    ")";
            }

            string cmd = "Select UKE_NO, UKE_INDEX, DEPT_NO, t1.P_HOKEN, BILL_USR, t4.HOKEN_TYPE, t5.PROPERTY_4, UKE_TIME " +
                " , t1.P_ID, t1.P_KANA, t1.P_NAME, t1.P_SEX, t1.P_BIRTHDAY, t1.P_AGE, UKE_TYPE, Trim(t3.S_NAME) as UKE_TYPE_NAME, GAIRAI_TYPE " +
                " , case when CHANGE_DEPT > 0 then CHANGE_DEPT else DEPT end DEPT " +
                " , case when SHINSATSU_DR > 0 then SHINSATSU_DR when CHANGE_DR > 0 then CHANGE_DR else DR end DR " +
                " , SHINSATSU_TIME_S, SHINSATSU_TIME_M, SHINSATSU_TIME_E, BILL_TIME " +
                " from D_UKETSUKE t1, M_UKENAME t3, M_PATIENT_HOKEN t4, M_PATIENT t5 " +
                " where t1.UKE_DATE = " + come_date + sqlDept + sqlDoctor +
                " and t1.UKE_TYPE = t3.CODE " +
                " and t1.P_ID = t4.P_ID and t1.P_HOKEN = t4.P_HOKEN " +
                " and t1.P_ID = t5.P_ID " +
                " order by P_ID, UKE_NO, UKE_INDEX";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatOut tmpPat = new PatOut();

                tmpPat.Id = tmp.GetDataString("P_ID");
                tmpPat.Kana = tmp.GetDataString("P_KANA");
                tmpPat.Name = tmp.GetDataString("P_NAME");
                tmpPat.Sex = tmp.GetDataString("P_SEX");
                tmpPat.Birth = tmp.GetDataString("P_BIRTHDAY");
                tmpPat.Age = tmp.GetDataString("P_AGE");
                tmpPat.NoteCode = tmp.GetDataString("PROPERTY_4");

                tmpPat.ComeDate = come_date;
                tmpPat.Seq1 = tmp.DataDict["UKE_NO"].ToString();
                tmpPat.Seq2 = tmp.DataDict["UKE_INDEX"].ToString();
                tmpPat.Seq3 = tmp.DataDict["DEPT_NO"].ToString();
                tmpPat.Kind = tmp.DataDict["UKE_TYPE"].ToString();
                tmpPat.KindName = tmp.DataDict["UKE_TYPE_NAME"].ToString();
                tmpPat.Mode = tmp.DataDict["GAIRAI_TYPE"].ToString();
                tmpPat.Ins = tmp.DataDict["P_HOKEN"].ToString();
                tmpPat.InsKind = tmp.DataDict["HOKEN_TYPE"].ToString();

                tmpPat.Dept = tmp.DataDict["DEPT"].ToString();
                tmpPat.Doctor = tmp.DataDict["DR"].ToString();

                tmpPat.Time1 = tmp.DataDict["UKE_TIME"].ToString();
                tmpPat.Time2 = tmp.DataDict["SHINSATSU_TIME_S"].ToString();
                tmpPat.Time3 = tmp.DataDict["SHINSATSU_TIME_M"].ToString();
                tmpPat.Time4 = tmp.DataDict["SHINSATSU_TIME_E"].ToString();
                tmpPat.Time5 = tmp.DataDict["BILL_TIME"].ToString();

                tmpPat.Kaikei = tmp.DataDict["BILL_USR"].ToString();

                tmpList.Add(tmpPat);
            }
#else
            string sqlDept = "";

            if (dept.Length > 0)
            {
                sqlDept = " and (変更科コード = " + dept + " or (科コード = " + dept + " and 変更科コード = 0))";
            }

            string sqlDoctor = "";

            if (doctor.Length > 0)
            {
                sqlDoctor = " and (変更ＤＲコード = " + doctor + " or (ＤＲコード = " + doctor + " and 変更ＤＲコード = 0))";
            }

            string cmd = "Select 受付ＮＯ, 連番, ID701RC_F11, ID701RC_F10, ID701RC_F20, t4.IM02RC_F43, t5.IM01RC_F13_4, 受付時間, 患者コード, カナ氏名, 漢字氏名, 性別, 年齢, 受付種別コード, Trim(IM5034RC_F03) as 種別, 外来区分 " +
                " , 科コード, 変更科コード, ＤＲコード, 変更ＤＲコード, 診察開始時間, 診察中断時間, 診察終了時間, 会計時間 " +
                " from ADT_診察状況データ t1, ID701RC t2, AMB_受付種別名マスター t3, IM02RC t4, IM01RC t5 " +
                " where t1.受付日 = " + come_date + " and t2.ID701RC_F01 = " + come_date + sqlDept + sqlDoctor +
                " and t1.患者コード = t2.ID701RC_F03 " +
                " and t1.受付日 = t2.ID701RC_F01 " +
                " and t1.受付ＮＯ = t2.ID701RC_F05 " +
                " and t1.科コード = t2.ID701RC_F04 " +
                " and t1.受付種別コード = t3.IM5034RC_F01 " +
                " and t1.患者コード = t4.IM02RC_F01 and t2.ID701RC_F10 = t4.IM02RC_F02 " +
                " and t1.患者コード = t5.IM01RC_F01 " +
                " order by 患者コード, 受付ＮＯ, 連番";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatOut tmpPat = new PatOut();

                tmpPat.Id = tmp.DataDict["患者コード"].ToString();
                tmpPat.Kana = tmp.DataDict["カナ氏名"].ToString();
                tmpPat.Name = tmp.DataDict["漢字氏名"].ToString();
                tmpPat.Sex = tmp.DataDict["性別"].ToString();
                tmpPat.Age = tmp.DataDict["年齢"].ToString();
                tmpPat.NoteCode = tmp.DataDict["IM01RC_F13_4"].ToString();

                tmpPat.ComeDate = come_date;
                tmpPat.Seq1 = tmp.DataDict["受付ＮＯ"].ToString();
                tmpPat.Seq2 = tmp.DataDict["連番"].ToString();
                tmpPat.Seq3 = tmp.DataDict["ID701RC_F11"].ToString();
                tmpPat.Kind = tmp.DataDict["受付種別コード"].ToString();
                tmpPat.KindName = tmp.DataDict["種別"].ToString();
                tmpPat.Mode = tmp.DataDict["外来区分"].ToString();
                tmpPat.Ins = tmp.DataDict["ID701RC_F10"].ToString();
                tmpPat.InsKind = tmp.DataDict["IM02RC_F43"].ToString();

                if (tmp.DataDict["変更科コード"].ToString().Length > 0 && !tmp.DataDict["変更科コード"].ToString().Equals("0"))
                {
                    tmpPat.Dept = tmp.DataDict["変更科コード"].ToString();
                }
                else if (tmp.DataDict["科コード"].ToString().Length > 0)
                {
                    tmpPat.Dept = tmp.DataDict["科コード"].ToString();
                }

                if (tmp.DataDict["変更ＤＲコード"].ToString().Length > 0 && !tmp.DataDict["変更ＤＲコード"].ToString().Equals("0"))
                {
                    tmpPat.Doctor = tmp.DataDict["変更ＤＲコード"].ToString();
                }
                else if (tmp.DataDict["ＤＲコード"].ToString().Length > 0)
                {
                    tmpPat.Doctor = tmp.DataDict["ＤＲコード"].ToString();
                }

                tmpPat.Time1 = tmp.DataDict["受付時間"].ToString();
                tmpPat.Time2 = tmp.DataDict["診察開始時間"].ToString();
                tmpPat.Time3 = tmp.DataDict["診察中断時間"].ToString();
                tmpPat.Time4 = tmp.DataDict["診察終了時間"].ToString();
                tmpPat.Time5 = tmp.DataDict["会計時間"].ToString();

                tmpPat.Kaikei = tmp.DataDict["ID701RC_F20"].ToString();

                tmpList.Add(tmpPat);
            }
#endif

            return tmpList;
        }

        /// <summary>
        /// 外来患者の受診歴を表示する。
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="start_date"></param>
        /// <returns></returns>
        public static List<PatOut> GetHistory(string pt_id, string start_date)
        {
            List<PatOut> tmpList = new List<PatOut>();

            int i = 0;

            if (pt_id.Length == 0 || !int.TryParse(pt_id, out i))
            {
                return tmpList;
            }

            string startDate = start_date;

            if (startDate.Length != 8)
            {
                startDate = DateTime.Now.AddMonths(-6).ToString("yyyyMMdd");
            }

#if INNO
            string cmd = "select UKE_DATE " +
                " , case when CHANGE_DEPT > 0 then CHANGE_DEPT else DEPT end DEPT " +
                " , case when SHINSATSU_DR > 0 then SHINSATSU_DR when CHANGE_DR > 0 then CHANGE_DR else DR end DR " +
                " , UKE_TIME, SHINSATSU_TIME_S, SHINSATSU_TIME_M, SHINSATSU_TIME_E, BILL_TIME " +
                " from D_UKETSUKE " +
                " where P_ID = " + pt_id + " and UKE_DATE >= " + startDate +
                " order by UKE_DATE desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatOut tmpPat = new PatOut();

                tmpPat.Id = pt_id;
                tmpPat.ComeDate = tmp.DataDict["UKE_DATE"].ToString().PadRight(8, '0');
                tmpPat.Dept = tmp.DataDict["DEPT"].ToString();
                tmpPat.Doctor = tmp.DataDict["DR"].ToString();
                tmpPat.Time1 = tmp.DataDict["UKE_TIME"].ToString();
                tmpPat.Time2 = tmp.DataDict["SHINSATSU_TIME_S"].ToString();
                tmpPat.Time3 = tmp.DataDict["SHINSATSU_TIME_M"].ToString();
                tmpPat.Time4 = tmp.DataDict["SHINSATSU_TIME_E"].ToString();
                tmpPat.Time5 = tmp.DataDict["BILL_TIME"].ToString();

                tmpList.Add(tmpPat);
            }
#else
            string cmd = "select 受付日, 変更科コード, 変更ＤＲコード, 受付時間, 診察開始時間, 診察中断時間, 診察終了時間, 会計時間 " +
                " from ADT_診察状況データ " +
                " where 患者コード = " + pt_id + " and 受付日 >= " + startDate +
                " order by 受付日 desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatOut tmpPat = new PatOut();

                tmpPat.Id = pt_id;
                tmpPat.ComeDate = tmp.DataDict["受付日"].ToString().PadRight(8, '0');
                tmpPat.Dept = tmp.DataDict["変更科コード"].ToString();
                tmpPat.Doctor = tmp.DataDict["変更ＤＲコード"].ToString();
                tmpPat.Time1 = tmp.DataDict["受付時間"].ToString();
                tmpPat.Time2 = tmp.DataDict["診察開始時間"].ToString();
                tmpPat.Time3 = tmp.DataDict["診察中断時間"].ToString();
                tmpPat.Time4 = tmp.DataDict["診察終了時間"].ToString();
                tmpPat.Time5 = tmp.DataDict["会計時間"].ToString();

                tmpList.Add(tmpPat);
            }
#endif
            return tmpList;
        }

        /// <summary>
        /// 該当日の受付データを取得する。
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="come_date"></param>
        /// <param name="order_by"></param>
        /// <returns></returns>
        public static List<PatOut> GetOneday(string pt_id, string come_date, string order_by = "")
        {
            List<PatOut> tmpList = new List<PatOut>();

            int i = 0;

            if (pt_id.Length == 0 || !int.TryParse(pt_id, out i))
            {
                return tmpList;
            }

            string comeDate = come_date;

            if (comeDate.Length != 8)
            {
                comeDate = DateTime.Now.ToString("yyyyMMdd");
            }

#if INNO
            string orderBy = order_by;

            if (order_by.Length == 0)
            {
                orderBy = "UKE_NO, UKE_INDEX";
            }

            string cmd = "select UKE_NO, UKE_INDEX, P_KANA " +
                " , case when CHANGE_DEPT > 0 then CHANGE_DEPT else DEPT end DEPT " +
                " , case when SHINSATSU_DR > 0 then SHINSATSU_DR when CHANGE_DR > 0 then CHANGE_DR else DR end DR " +
                " , UKE_TIME, SHINSATSU_TIME_S, SHINSATSU_TIME_M, SHINSATSU_TIME_E, BILL_TIME " +
                " from D_UKETSUKE " +
                " where P_ID = " + pt_id + " and UKE_DATE = " + comeDate +
                " order by " + orderBy;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatOut tmpPat = new PatOut();

                tmpPat.Id = pt_id;
                tmpPat.ComeDate = comeDate;
                tmpPat.Kana = tmp.DataDict["P_KANA"].ToString();
                tmpPat.Seq1 = tmp.DataDict["UKE_NO"].ToString();
                tmpPat.Seq2 = tmp.DataDict["UKE_INDEX"].ToString();
                tmpPat.Dept = tmp.DataDict["DEPT"].ToString();
                tmpPat.Doctor = tmp.DataDict["DR"].ToString();
                tmpPat.Time1 = tmp.DataDict["UKE_TIME"].ToString();
                tmpPat.Time2 = tmp.DataDict["SHINSATSU_TIME_S"].ToString();
                tmpPat.Time3 = tmp.DataDict["SHINSATSU_TIME_M"].ToString();
                tmpPat.Time4 = tmp.DataDict["SHINSATSU_TIME_E"].ToString();
                tmpPat.Time5 = tmp.DataDict["BILL_TIME"].ToString();

                tmpList.Add(tmpPat);
            }
#else
            string orderBy = order_by;

            if (order_by.Length == 0)
            {
                orderBy = "受付ＮＯ, 連番";
            }

            string cmd = "select 受付ＮＯ, 連番, カナ氏名, 受付時間, 会計時間, 科コード, 変更科コード " +
                " from ADT_診察状況データ " +
                " where 患者コード = " + pt_id + " and 受付日 = " + comeDate +
                " order by " + orderBy;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatOut tmpPat = new PatOut();

                tmpPat.Id = pt_id;
                tmpPat.ComeDate = comeDate;
                tmpPat.Kana = tmp.DataDict["カナ氏名"].ToString();
                tmpPat.Seq1 = tmp.DataDict["受付ＮＯ"].ToString();
                tmpPat.Seq2 = tmp.DataDict["連番"].ToString();
                tmpPat.Time1 = tmp.DataDict["受付時間"].ToString();
                tmpPat.Time5 = tmp.DataDict["会計時間"].ToString();

                if (tmp.DataDict["変更科コード"].ToString().Length > 0 && !tmp.DataDict["変更科コード"].ToString().Equals("0"))
                {
                    tmpPat.Dept = tmp.DataDict["変更科コード"].ToString();
                }
                else
                {
                    tmpPat.Dept = tmp.DataDict["科コード"].ToString();
                }

                tmpList.Add(tmpPat);
            }
#endif

            return tmpList;
        }

        /// <summary>
        /// 該当日の受付データ（受付ＮＯのみ）を取得する。
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="come_date"></param>
        /// <returns></returns>
        public static List<int> GetOnedaySeqs(string pt_id, string come_date)
        {
            List<int> tmpList = new List<int>();

            int i = 0;

            if (pt_id.Length == 0 || !int.TryParse(pt_id, out i))
            {
                return tmpList;
            }

            string comeDate = come_date;

            if (comeDate.Length != 8)
            {
                comeDate = DateTime.Now.ToString("yyyyMMdd");
            }

#if INNO
            string cmd = "select distinct UKE_NO " +
                " from D_UKETSUKE " +
                " where P_ID = " + pt_id + " and UKE_DATE = " + comeDate +
                " order by UKE_NO";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                int seq1 = 0;
                int.TryParse(tmp.DataDict["UKE_NO"].ToString(), out seq1);

                tmpList.Add(seq1);
            }
#else
            string cmd = "select distinct 受付ＮＯ " +
                " from ADT_診察状況データ " +
                " where 患者コード = " + pt_id + " and 受付日 = " + comeDate +
                " order by 受付ＮＯ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                int seq1 = 0;
                int.TryParse(tmp.DataDict["受付ＮＯ"].ToString(), out seq1);

                tmpList.Add(seq1);
            }
#endif

            return tmpList;
        }

        /// <summary>
        /// 該当日で最後の受付データを取得する。
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="come_date"></param>
        /// <returns></returns>
        public static List<PatOut> GetOnedayLast(string pt_id, string come_date)
        {
            List<PatOut> tmpList = new List<PatOut>();

            int i = 0;

            if (pt_id.Length == 0 || !int.TryParse(pt_id, out i))
            {
                return tmpList;
            }

            string comeDate = come_date;

            if (comeDate.Length != 8)
            {
                comeDate = DateTime.Now.ToString("yyyyMMdd");
            }

#if INNO
            string cmd = "select UKE_NO, UKE_INDEX, P_KANA " +
                " , case when CHANGE_DEPT > 0 then CHANGE_DEPT else DEPT end DEPT " +
                " , case when SHINSATSU_DR > 0 then SHINSATSU_DR when CHANGE_DR > 0 then CHANGE_DR else DR end DR " +
                " , UKE_TIME, SHINSATSU_TIME_S, SHINSATSU_TIME_M, SHINSATSU_TIME_E, BILL_TIME " +
                " from D_UKETSUKE " +
                " where P_ID = " + pt_id + " and UKE_DATE = " + comeDate +
                " order by UKE_TIME desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            string time1 = "";

            foreach (StdClass tmp in tmp_list)
            {
                if (time1.Length > 0 && !tmp.DataDict["UKE_TIME"].ToString().Equals(time1))
                {
                    break;
                }
                else
                {
                    time1 = tmp.DataDict["UKE_TIME"].ToString();
                }

                PatOut tmpPat = new PatOut();

                tmpPat.Id = pt_id;
                tmpPat.ComeDate = comeDate;
                tmpPat.Kana = tmp.DataDict["P_KANA"].ToString();
                tmpPat.Seq1 = tmp.DataDict["UKE_NO"].ToString();
                tmpPat.Seq2 = tmp.DataDict["UKE_INDEX"].ToString();
                tmpPat.Dept = tmp.DataDict["DEPT"].ToString();
                tmpPat.Time1 = tmp.DataDict["UKE_TIME"].ToString();

                tmpList.Add(tmpPat);
            }
#else
            string cmd = "select 受付ＮＯ, 連番, カナ氏名, 受付時間, 科コード, 変更科コード " +
                " from ADT_診察状況データ " +
                " where 患者コード = " + pt_id + " and 受付日 = " + comeDate +
                " order by 受付時間 desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            string time1 = "";

            foreach (StdClass tmp in tmp_list)
            {
                if (time1.Length > 0 && !tmp.DataDict["受付時間"].ToString().Equals(time1))
                {
                    break;
                }
                else
                {
                    time1 = tmp.DataDict["受付時間"].ToString();
                }

                PatOut tmpPat = new PatOut();

                tmpPat.Id = pt_id;
                tmpPat.ComeDate = comeDate;
                tmpPat.Kana = tmp.DataDict["カナ氏名"].ToString();
                tmpPat.Seq1 = tmp.DataDict["受付ＮＯ"].ToString();
                tmpPat.Seq2 = tmp.DataDict["連番"].ToString();
                tmpPat.Time1 = tmp.DataDict["受付時間"].ToString();

                if (tmp.DataDict["変更科コード"].ToString().Length > 0 && !tmp.DataDict["変更科コード"].ToString().Equals("0"))
                {
                    tmpPat.Dept = tmp.DataDict["変更科コード"].ToString();
                }
                else
                {
                    tmpPat.Dept = tmp.DataDict["科コード"].ToString();
                }

                tmpList.Add(tmpPat);
            }
#endif

            return tmpList;
        }

        /// <summary>
        /// 該当日の受付番号（複数ある人は最後だけ）を取得する。
        /// </summary>
        /// <param name="pt_id_list"></param>
        /// <param name="come_date"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetOnedayLastSeq(List<string> pt_id_list, string come_date)
        {
            Dictionary<string, string> tmpDict = new Dictionary<string, string>();

            List<PatOut> tmpList = new List<PatOut>();

            if (pt_id_list.Count == 0 || AppString.ConcatList(pt_id_list, ",").Length == 0)
            {
                return tmpDict;
            }

            string comeDate = come_date;

            if (comeDate.Length != 8)
            {
                comeDate = DateTime.Now.ToString("yyyyMMdd");
            }

#if INNO
            foreach (string s in AppString.ConcatLists(pt_id_list, ","))
            {
                string cmd = "select UKE_NO, P_ID " +
                    " from D_UKETSUKE " +
                    " where P_ID in (" + s + ") and UKE_DATE = " + comeDate +
                    " order by P_ID, UKE_TIME desc";

                List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

                string pt_id = "";

                foreach (StdClass tmp in tmp_list)
                {
                    if (tmp.DataDict["P_ID"].ToString().Equals(pt_id))
                    {
                        continue;
                    }
                    else
                    {
                        pt_id = tmp.DataDict["P_ID"].ToString();
                    }

                    if (!tmpDict.ContainsKey(pt_id))
                    {
                        tmpDict.Add(pt_id, tmp.DataDict["UKE_NO"].ToString());
                    }
                }
            }
#else
            foreach (string s in AppString.ConcatLists(pt_id_list, ","))
            {
                string cmd = "select 受付ＮＯ, 患者コード from ADT_診察状況データ " +
                    " where 患者コード in (" + s + ") and 受付日 = " + comeDate +
                    " order by 患者コード, 受付時間 desc";

                List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

                string pt_id = "";

                foreach (StdClass tmp in tmp_list)
                {
                    if (tmp.DataDict["患者コード"].ToString().Equals(pt_id))
                    {
                        continue;
                    }
                    else
                    {
                        pt_id = tmp.DataDict["患者コード"].ToString();
                    }

                    if (!tmpDict.ContainsKey(pt_id))
                    {
                        tmpDict.Add(pt_id, tmp.DataDict["受付ＮＯ"].ToString());
                    }
                }
            }
#endif

            return tmpDict;
        }

        /// <summary>
        /// 会計入力者コードをセットする
        /// （ID701RC.F20 にセット）
        /// </summary>
        /// <param name="come_date">受付日</param>
        /// <param name="seq1">受付番号</param>
        /// <param name="seq2">連番</param>
        public static void SetKaikeiStaffBySEQ(string staff_code, string come_date, string seq1, string seq2 = "")
        {
            if (staff_code.Length == 0 || come_date.Length != 8 || seq1.Length == 0)
            {
                return;
            }
#if INNO
            string cmd = "update D_UKETSUKE " +
                " set BILL_USR = " + staff_code +
                " where UKE_DATE = " + come_date +
                " and UKE_NO = " + seq1;

            if (seq2.Length > 0)
            {
                cmd += " and UKE_INDEX = " + seq2;
            }

            DB.Db3.ExecuteNonQuery(cmd);
#else
            string cmd = "update ID701RC " +
                " set ID701RC_F20 = '" + staff_code + "' " +
                " where ID701RC_F01 = " + come_date +
                " and ID701RC_F05 = " + seq1;

            if (seq2.Length > 0)
            {
                cmd += " and ID701RC_F23 = " + seq2;
            }

            DB.Db1.ExecuteNonQuery(cmd);
#endif
        }

        /// <summary>
        /// 会計入力者コードをセットする
        /// （ID701RC.F20 にセット）
        /// </summary>
        /// <param name="come_date">受付日</param>
        /// <param name="seq1">受付番号</param>
        /// <param name="seq2">連番</param>
        public static void SetKaikeiStaffByPtId(string staff_code, string come_date, string pt_id)
        {
            if (staff_code.Length == 0 || come_date.Length != 8 || pt_id.Length == 0)
            {
                return;
            }
#if INNO
            string cmd = "update D_UKETSUKE " +
                " set BILL_USR = " + staff_code +
                " where UKE_DATE = " + come_date +
                " and P_ID = " + pt_id +
                " and (BILL_USR is null or BILL_USR = 0)";

            DB.Db3.ExecuteNonQuery(cmd);
#else
            string cmd = "update ID701RC " +
                " set ID701RC_F20 = '" + staff_code + "' " +
                " where ID701RC_F01 = " + come_date +
                " and ID701RC_F03 = " + pt_id +
                " and (ID701RC_F20 is null or ID701RC_F20 = '0')";

            DB.Db1.ExecuteNonQuery(cmd);
#endif
        }

        /// <summary>
        /// 会計時間をセットする
        /// （ID701RC.F22 にセット）
        /// </summary>
        /// <param name="time5">会計時間</param>
        /// <param name="come_date">受付日</param>
        /// <param name="seq1">受付番号</param>
        /// <param name="seq2">連番</param>
        public static void SetKaikeiTimeBySEQ(int time5, string come_date, string seq1, string seq2 = "")
        {
#if INNO
            if (time5 < 0 || time5 >= 240000 || time5 % 10000 >= 6000 || time5 % 100 >= 60 || come_date.Length != 8 || seq1.Length == 0)
            {
                return;
            }

            DateTime dt = DateTime.Now;

            if (!DateTime.TryParse(time5.ToString().PadLeft(6, '0').Insert(2, ":").Insert(5, ":"), out dt))
            {
                return;
            }

            string cmd = "update D_UKETSUKE " +
                " set BILL_TIME = " + time5 +
                " where UKE_DATE = " + come_date +
                " and UKE_NO = " + seq1;

            if (seq2.Length > 0)
            {
                cmd += " and UKE_INDEX = " + seq2;
            }

            DB.Db3.ExecuteNonQuery(cmd);
#else
            if (time5 < 0 || time5 >= 2400 || time5 % 100 >= 60 || come_date.Length != 8 || seq1.Length == 0)
            {
                return;
            }

            string cmd = "update ID701RC " +
                " set ID701RC_F22 = " + time5 +
                " where ID701RC_F01 = " + come_date +
                " and ID701RC_F05 = " + seq1;

            if (seq2.Length > 0)
            {
                cmd += " and ID701RC_F23 = " + seq2;
            }

            DB.Db1.ExecuteNonQuery(cmd);
#endif
        }

        /// <summary>
        /// 診察開始時間をセットする
        /// （ADT_診察状況データ.診察開始時間 にセット）
        /// </summary>
        /// <param name="time2">診察開始時間</param>
        /// <param name="come_date">受付日</param>
        /// <param name="seq1">受付番号</param>
        /// <param name="seq2">連番</param>
        /// <param name="dept_code">変更科コード</param>
        /// <param name="doctor_code">変更ＤＲコード</param>
        public static void SetKarteStartTimeBySEQ(int time2, string come_date, string seq1, string seq2 = "1", string dept_code = "0", string doctor_code = "0")
        {
#if INNO
            if (time2 < 0 || time2 >= 240000 || time2 % 10000 >= 6000 || time2 % 100 >= 60 || come_date.Length != 8 || seq1.Length == 0)
            {
                return;
            }

            DateTime dt = DateTime.Now;

            if (!DateTime.TryParse(time2.ToString().PadLeft(6, '0').Insert(2, ":").Insert(5, ":"), out dt))
            {
                return;
            }

            if (dept_code.Length == 0 || doctor_code.Length == 0)
            {
                return;
            }

            string cmd = "update D_UKETSUKE " +
                " set SHINSATSU_TIME_S = " + time2 +
                " , CHANGE_DEPT = " + dept_code +
                " , CHANGE_DR = " + doctor_code +
                " , SHINSATSU_DR = " + doctor_code +
                " where UKE_DATE = " + come_date +
                " and UKE_NO = " + seq1;

            if (seq2.Length > 0)
            {
                cmd += " and UKE_INDEX = " + seq2;
            }

            DB.Db3.ExecuteNonQuery(cmd);
#else
            if (time2 < 0 || time2 >= 2400 || time2 % 100 >= 60 || come_date.Length != 8 || seq1.Length == 0)
            {
                return;
            }

            if (dept_code.Length == 0 || doctor_code.Length == 0)
            {
                return;
            }

            string cmd = "update ADT_診察状況データ " +
                " set 診察開始時間 = " + time2 +
                " , 変更科コード = " + dept_code +
                " , 変更ＤＲコード = " + doctor_code +
                " where 受付日 = " + come_date +
                " and 受付ＮＯ = " + seq1;

            if (seq2.Length > 0)
            {
                cmd += " and 連番 = " + seq2;
            }

            DB.Db1.ExecuteNonQuery(cmd);
#endif
        }

        /// <summary>
        /// 診察中断時間をセットする
        /// （ADT_診察状況データ.診察中断時間 にセット）
        /// </summary>
        /// <param name="time3">診察中断時間</param>
        /// <param name="come_date">受付日</param>
        /// <param name="seq1">受付番号</param>
        /// <param name="seq2">連番</param>
        public static void SetKarteStopTimeBySEQ(int time3, string come_date, string seq1, string seq2 = "1")
        {
#if INNO
            if (time3 < 0 || time3 >= 240000 || time3 % 10000 >= 6000 || time3 % 100 >= 60 || come_date.Length != 8 || seq1.Length == 0)
            {
                return;
            }

            DateTime dt = DateTime.Now;

            if (!DateTime.TryParse(time3.ToString().PadLeft(6, '0').Insert(2, ":").Insert(5, ":"), out dt))
            {
                return;
            }

            string cmd = "update D_UKETSUKE " +
                " set SHINSATSU_TIME_M = " + time3 +
                " where UKE_DATE = " + come_date +
                " and UKE_NO = " + seq1;

            if (seq2.Length > 0)
            {
                cmd += " and UKE_INDEX = " + seq2;
            }

            DB.Db3.ExecuteNonQuery(cmd);
#else
            if (time3 < 0 || time3 >= 2400 || time3 % 100 >= 60 || come_date.Length != 8 || seq1.Length == 0)
            {
                return;
            }

            string cmd = "update ADT_診察状況データ " +
                " set 診察中断時間 = " + time3 +
                " where 受付日 = " + come_date +
                " and 受付ＮＯ = " + seq1;

            if (seq2.Length > 0)
            {
                cmd += " and 連番 = " + seq2;
            }

            DB.Db1.ExecuteNonQuery(cmd);
#endif
        }

        /// <summary>
        /// 診察終了時間をセットする
        /// （ADT_診察状況データ.診察終了時間 にセット）
        /// </summary>
        /// <param name="time4">診察終了時間</param>
        /// <param name="come_date">受付日</param>
        /// <param name="seq1">受付番号</param>
        /// <param name="seq2">連番</param>
        public static void SetKarteEndTimeBySEQ(int time4, string come_date, string seq1, string seq2 = "1")
        {
#if INNO
            if (time4 < 0 || time4 >= 240000 || time4 % 10000 >= 6000 || time4 % 100 >= 60 || come_date.Length != 8 || seq1.Length == 0)
            {
                return;
            }

            DateTime dt = DateTime.Now;

            if (!DateTime.TryParse(time4.ToString().PadLeft(6, '0').Insert(2, ":").Insert(5, ":"), out dt))
            {
                return;
            }

            string cmd = "update D_UKETSUKE " +
                " set SHINSATSU_TIME_E = " + time4 +
                " where UKE_DATE = " + come_date +
                " and UKE_NO = " + seq1;

            if (seq2.Length > 0)
            {
                cmd += " and UKE_INDEX = " + seq2;
            }

            DB.Db3.ExecuteNonQuery(cmd);
#else
            if (time4 < 0 || time4 >= 2400 || time4 % 100 >= 60 || come_date.Length != 8 || seq1.Length == 0)
            {
                return;
            }

            string cmd = "update ADT_診察状況データ " +
                " set 診察終了時間 = " + time4 +
                " where 受付日 = " + come_date +
                " and 受付ＮＯ = " + seq1;

            if (seq2.Length > 0)
            {
                cmd += " and 連番 = " + seq2;
            }

            DB.Db1.ExecuteNonQuery(cmd);

            // MACS医事会計を使っていないので ID701RC_F15 は更新しなくてよい
            /*
            cmd = "update ID701RC " +
                " set ID701RC_F15 = " + time4 +
                " where ID701RC_F01 = " + come_date +
                " and ID701RC_F05 = " + seq1;

            if (seq2.Length > 0)
            {
                cmd += " and ID701RC_F23 = " + seq2;
            }

            DB.Db1.ExecuteNonQuery(cmd);
             */
#endif
        }
    }

    /// <summary>
    /// 診療科ごとの受診者数
    /// </summary>
    public class DeptOut : StdEntity
    {
        public string DeptCode = "";

        /// <summary>
        /// 診療科
        /// </summary>
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
        /// 外来区分
        /// 1: 通常, 2: 救急
        /// →　inno では 0: 通常, 1: 救急, 2: 在宅
        /// </summary>
        public string ModeCode = "";

        public string ModeName
        {
            get
            {
                string s = "";
#if INNO
                if (this.ModeCode.Equals("1"))
                {
                    s = "救急";
                }
                else if (this.ModeCode.Equals("2"))
                {
                    s = "在宅";
                }
                else
                {
                    s = "通常";
                }
#else
                if (this.ModeCode.Equals("1"))
                {
                    s = "通常";
                }
                else if (this.ModeCode.Equals("2"))
                {
                    s = "救急";
                }
#endif
                return s;
            }
        }

        public int Count = 0;

        public static List<DeptOut> GetList(string come_date)
        {
            List<DeptOut> list = new List<DeptOut>();

            string comeDate = come_date;

            if (comeDate.Length != 8)
            {
                comeDate = DateTime.Now.ToString("yyyyMMdd");
            }
#if INNO
            string cmd = "select tt.GAIRAI_TYPE, tt.DEPT, count(*) as AMOUNT " +
                " from " +
                " (select t.GAIRAI_TYPE, case when CHANGE_DEPT > 0 then CHANGE_DEPT else DEPT end DEPT from D_UKETSUKE t where t.UKE_DATE = " + comeDate + ") tt " +
                " group by tt.GAIRAI_TYPE, tt.DEPT" +
                " order by tt.GAIRAI_TYPE, tt.DEPT";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                DeptOut obj = new DeptOut();

                obj.DeptCode = tmp.DataDict["DEPT"].ToString();
                obj.ModeCode = tmp.DataDict["GAIRAI_TYPE"].ToString();
                int.TryParse(tmp.DataDict["AMOUNT"].ToString(), out obj.Count);

                list.Add(obj);
            }
#else
            string cmd = "select 科コード, 外来区分, count(*) as 人数 " +
                " from ADT_診察状況データ " +
                " where 受付日 = " + comeDate +
                " group by 外来区分, 科コード" +
                " order by 外来区分, 科コード";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                DeptOut obj = new DeptOut();

                obj.DeptCode = tmp.DataDict["科コード"].ToString();
                obj.ModeCode = tmp.DataDict["外来区分"].ToString();
                int.TryParse(tmp.DataDict["人数"].ToString(), out obj.Count);

                list.Add(obj);
            }
#endif
            return list;
        }

        public static List<DeptOut> GetYetList(string come_date)
        {
            List<DeptOut> list = new List<DeptOut>();

            string comeDate = come_date;

            if (comeDate.Length != 8)
            {
                comeDate = DateTime.Now.ToString("yyyyMMdd");
            }
#if INNO
            string cmd = "select tt.GAIRAI_TYPE, tt.DEPT, count(*) as AMOUNT " +
                " from " +
                " (select t.GAIRAI_TYPE, case when CHANGE_DEPT > 0 then CHANGE_DEPT else DEPT end DEPT " +
                "  from D_UKETSUKE t " +
                "  where t.UKE_DATE = " + comeDate + " and (t.SHINSATSU_TIME_E is null or t.SHINSATSU_TIME_E = 0)) tt " +
                " group by tt.GAIRAI_TYPE, tt.DEPT" +
                " order by tt.GAIRAI_TYPE, tt.DEPT";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                DeptOut obj = new DeptOut();

                obj.ModeCode = tmp.DataDict["GAIRAI_TYPE"].ToString();
                obj.DeptCode = tmp.DataDict["DEPT"].ToString();
                int.TryParse(tmp.DataDict["AMOUNT"].ToString(), out obj.Count);

                list.Add(obj);
            }
#else
            string cmd = "select 科コード, 外来区分, count(*) as 人数 " +
                " from ADT_診察状況データ " +
                " where 受付日 = " + comeDate + " and 診察終了時間 = 0 " +
                " group by 外来区分, 科コード" +
                " order by 外来区分, 科コード";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                DeptOut obj = new DeptOut();

                obj.DeptCode = tmp.DataDict["科コード"].ToString();
                obj.ModeCode = tmp.DataDict["外来区分"].ToString();
                int.TryParse(tmp.DataDict["人数"].ToString(), out obj.Count);

                list.Add(obj);
            }
#endif
            return list;
        }
    }
}
