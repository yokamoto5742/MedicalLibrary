using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.Common;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class DPCHeader
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id = "";

        PatBase _Pat = new PatIn();

        public PatBase Pat
        {
            get
            {
                if (!this._Pat.Id.Equals(this.Id))
                {
                    this._Pat = PatBase.Load(this.Id);
                }

                return this._Pat;
            }
        }

        /// <summary>
        /// 入院日
        /// </summary>
        public string AdmDate = "";

        /// <summary>
        /// 入院番号
        /// </summary>
        public int SEQ = 0;

        /// <summary>
        /// 退院日
        /// （転棟する場合は空。最後の病棟だけ必要）
        /// </summary>
        public string DisDate = "";

        /// <summary>
        /// 入棟日
        /// </summary>
        public string StartDate = "";

        /// <summary>
        /// 退棟日
        /// </summary>
        public string EndDate = "";

        /// <summary>
        /// 病棟コード
        /// </summary>
        public string Ward = "";

        public string WardName
        {
            get
            {
                return Dict.WardDict.ContainsKey(this.Ward) ? Dict.WardDict[this.Ward].Name : "";
            }
        }

        /// <summary>
        /// 診療科
        /// </summary>
        public string Dept = "";

        /// <summary>
        /// 診療科
        /// </summary>
        public string DeptName
        {
            get
            {
                return DPCDept.GetData(this.Dept).Name.Length > 0 ? DPCDept.GetData(this.Dept).Name : "";
            }
        }

        /// <summary>
        /// 診断
        /// （複数の場合はスペースで連結）
        /// </summary>
        public string Diag = "";

        /// <summary>
        /// 診断リスト
        /// </summary>
        public List<string> DiagList
        {
            get
            {
                return this.Diag.Split(' ').ToList<string>();
            }
        }

        /// <summary>
        /// 診断名リスト
        /// </summary>
        public string DiagNames
        {
            get
            {
                string s = "";

                foreach (string diag in this.DiagList)
                {
                    if (s.Length > 0) s += " ";

                    s += DPCDiag.GetData(diag).Name;
                }

                return s;
            }
        }

        /// <summary>
        /// 医師１
        /// </summary>
        public string Doctor1 = "";

        public string DoctorName1
        {
            get
            {
                return Dict.DoctorDict.ContainsKey(this.Doctor1) ? Dict.DoctorDict[this.Doctor1].ShortName.Replace("　", "").Replace(" ", "") : "";
            }
        }

        /// <summary>
        /// 医師２
        /// </summary>
        public string Doctor2 = "";

        public string DoctorName2
        {
            get
            {
                return Dict.DoctorDict.ContainsKey(this.Doctor2) ? Dict.DoctorDict[this.Doctor2].ShortName.Replace("　", "").Replace(" ", "") : "";
            }
        }

        /// <summary>
        /// コメント
        /// </summary>
        public string Cont = "";


        /// <summary>
        /// ステータス
        /// 1 有効, 0 無効
        /// </summary>
        public string Status = "";

        /// <summary>
        /// 作成者コード
        /// </summary>
        public string StaffCode = "";

        /// <summary>
        /// 作成者
        /// </summary>
        public string StaffName
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(this.StaffCode))
                {
                    s = Dict.StaffDict[this.StaffCode].Name;
                }

                return s;
            }
        }

        /// <summary>
        /// 保存日
        /// </summary>
        public string SaveDate = "";

        /// <summary>
        /// 保存時刻
        /// </summary>
        public string SaveTime = "";

        /// <summary>
        /// 保存日時
        /// </summary>
        public string SaveDateTime
        {
            get
            {
                string s = "";

                if (this.SaveDate.Length == 8)
                {
                    s = DateTimeAgent.DateFormat(this.SaveDate, DateTimeAgent.DateFormatKind.LONG)
                        + " " + DateTimeAgent.TimeFormat6(this.SaveTime, 4);
                }

                return s;
            }
        }

        /// <summary>
        /// ステータス
        /// 1 完成
        /// </summary>
        public string Status1 = "";

        /// <summary>
        /// 作成者コード
        /// </summary>
        public string StaffCode1 = "";

        /// <summary>
        /// 作成者
        /// </summary>
        public string StaffName1
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(this.StaffCode1))
                {
                    s = Dict.StaffDict[this.StaffCode1].Name;
                }

                return s;
            }
        }

        /// <summary>
        /// 保存日
        /// </summary>
        public string SaveDate1 = "";

        /// <summary>
        /// 保存時刻
        /// </summary>
        public string SaveTime1 = "";

        /// <summary>
        /// 保存日時
        /// </summary>
        public string SaveDateTime1
        {
            get
            {
                string s = "";

                if (this.SaveDate1.Length == 8)
                {
                    s = DateTimeAgent.DateFormat(this.SaveDate1, DateTimeAgent.DateFormatKind.LONG)
                        + " " + DateTimeAgent.TimeFormat6(this.SaveTime1, 4);
                }

                return s;
            }
        }

        /// <summary>
        /// ステータス
        /// 1 完成
        /// </summary>
        public string Status2 = "";

        /// <summary>
        /// 作成者コード
        /// </summary>
        public string StaffCode2 = "";

        /// <summary>
        /// 作成者
        /// </summary>
        public string StaffName2
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(this.StaffCode2))
                {
                    s = Dict.StaffDict[this.StaffCode2].Name;
                }

                return s;
            }
        }

        /// <summary>
        /// 保存日
        /// </summary>
        public string SaveDate2 = "";

        /// <summary>
        /// 保存時刻
        /// </summary>
        public string SaveTime2 = "";

        /// <summary>
        /// 保存日時
        /// </summary>
        public string SaveDateTime2
        {
            get
            {
                string s = "";

                if (this.SaveDate2.Length == 8)
                {
                    s = DateTimeAgent.DateFormat(this.SaveDate2, DateTimeAgent.DateFormatKind.LONG)
                        + " " + DateTimeAgent.TimeFormat6(this.SaveTime2, 4);
                }

                return s;
            }
        }

        /// <summary>
        /// ステータス
        /// 1 完成
        /// </summary>
        public string Status3 = "";

        /// <summary>
        /// 作成者コード
        /// </summary>
        public string StaffCode3 = "";

        /// <summary>
        /// 作成者
        /// </summary>
        public string StaffName3
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(this.StaffCode3))
                {
                    s = Dict.StaffDict[this.StaffCode3].Name;
                }

                return s;
            }
        }

        /// <summary>
        /// 保存日
        /// </summary>
        public string SaveDate3 = "";

        /// <summary>
        /// 保存時刻
        /// </summary>
        public string SaveTime3 = "";

        /// <summary>
        /// 保存日時
        /// </summary>
        public string SaveDateTime3
        {
            get
            {
                string s = "";

                if (this.SaveDate3.Length == 8)
                {
                    s = DateTimeAgent.DateFormat(this.SaveDate3, DateTimeAgent.DateFormatKind.LONG)
                        + " " + DateTimeAgent.TimeFormat6(this.SaveTime3, 4);
                }

                return s;
            }
        }

        /// <summary>
        /// ステータス
        /// 1 完成
        /// </summary>
        public string Status4 = "";

        /// <summary>
        /// 作成者コード
        /// </summary>
        public string StaffCode4 = "";

        /// <summary>
        /// 作成者
        /// </summary>
        public string StaffName4
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(this.StaffCode4))
                {
                    s = Dict.StaffDict[this.StaffCode4].Name;
                }

                return s;
            }
        }

        /// <summary>
        /// 保存日
        /// </summary>
        public string SaveDate4 = "";

        /// <summary>
        /// 保存時刻
        /// </summary>
        public string SaveTime4 = "";

        /// <summary>
        /// 保存日時
        /// </summary>
        public string SaveDateTime4
        {
            get
            {
                string s = "";

                if (this.SaveDate4.Length == 8)
                {
                    s = DateTimeAgent.DateFormat(this.SaveDate4, DateTimeAgent.DateFormatKind.LONG)
                        + " " + DateTimeAgent.TimeFormat6(this.SaveTime4, 4);
                }

                return s;
            }
        }

        /// <summary>
        /// ステータス
        /// 1 完成
        /// </summary>
        public string Status5 = "";

        /// <summary>
        /// 作成者コード
        /// </summary>
        public string StaffCode5 = "";

        /// <summary>
        /// 作成者
        /// </summary>
        public string StaffName5
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(this.StaffCode5))
                {
                    s = Dict.StaffDict[this.StaffCode5].Name;
                }

                return s;
            }
        }

        /// <summary>
        /// 保存日
        /// </summary>
        public string SaveDate5 = "";

        /// <summary>
        /// 保存時刻
        /// </summary>
        public string SaveTime5 = "";

        /// <summary>
        /// 保存日時
        /// </summary>
        public string SaveDateTime5
        {
            get
            {
                string s = "";

                if (this.SaveDate5.Length == 8)
                {
                    s = DateTimeAgent.DateFormat(this.SaveDate5, DateTimeAgent.DateFormatKind.LONG)
                        + " " + DateTimeAgent.TimeFormat6(this.SaveTime5, 4);
                }

                return s;
            }
        }


        /// <summary>
        /// PRRISM出力者コード
        /// </summary>
        public string PrStaff = "";

        /// <summary>
        /// PRRISM出力者
        /// </summary>
        public string PrStaffName
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(this.PrStaff))
                {
                    s = Dict.StaffDict[this.PrStaff].Name;
                }

                return s;
            }
        }

        /// <summary>
        /// PRRISM出力日
        /// </summary>
        public string PrDate = "";

        /// <summary>
        /// PRRISM出力時刻
        /// </summary>
        public string PrTime = "";

        /// <summary>
        /// PRRISM出力日時
        /// </summary>
        public string PrDateTime
        {
            get
            {
                string s = "";

                if (this.PrDate.Length == 8)
                {
                    s = DateTimeAgent.DateFormat(this.PrDate, DateTimeAgent.DateFormatKind.LONG)
                        + " " + DateTimeAgent.TimeFormat6(this.PrTime, 4);
                }

                return s;
            }
        }

        /// <summary>
        /// PRRISM出力可能か
        /// </summary>
        public bool Prable
        {
            get
            {
                bool b = true;

                // 診療科がなければ false
                if (this.Dept.Length == 0) b = false;

                return b;
            }
        }

        public List<DPCDetail> DetailList = new List<DPCDetail>();


        static DPCHeader GetFromStdClass(StdClass tmp)
        {
            DPCHeader obj = new DPCHeader();

            obj.Id = tmp.GetDataString("PATIENT_ID");
            obj.AdmDate = tmp.GetDataString("ADM_DATE");
            obj.SEQ = tmp.GetDataInt("SEQ");
            obj.DisDate = tmp.GetDataString("DIS_DATE");
            obj.StartDate = tmp.GetDataString("START_DATE");
            obj.EndDate = tmp.GetDataString("END_DATE");
            obj.Ward = tmp.GetDataString("WARD");
            obj.Diag = tmp.GetDataString("DIAG");

            obj.Dept = tmp.GetDataString("DEPT");
            obj.Doctor1 = tmp.GetDataString("DOCTOR1");
            obj.Doctor2 = tmp.GetDataString("DOCTOR2");
            obj.Cont = tmp.GetDataString("CONT");

            obj.Status = tmp.GetDataString("STATUS");
            obj.StaffCode = tmp.GetDataString("STAFF");
            obj.SaveDate = tmp.GetDataString("SAVE_DATE");
            obj.SaveTime = tmp.GetDataString("SAVE_TIME");

            obj.Status1 = tmp.GetDataString("STATUS1");
            obj.StaffCode1 = tmp.GetDataString("STAFF1");
            obj.SaveDate1 = tmp.GetDataString("SAVE_DATE1");
            obj.SaveTime1 = tmp.GetDataString("SAVE_TIME1");

            obj.Status2 = tmp.GetDataString("STATUS2");
            obj.StaffCode2 = tmp.GetDataString("STAFF2");
            obj.SaveDate2 = tmp.GetDataString("SAVE_DATE2");
            obj.SaveTime2 = tmp.GetDataString("SAVE_TIME2");

            obj.Status3 = tmp.GetDataString("STATUS3");
            obj.StaffCode3 = tmp.GetDataString("STAFF3");
            obj.SaveDate3 = tmp.GetDataString("SAVE_DATE3");
            obj.SaveTime3 = tmp.GetDataString("SAVE_TIME3");

            obj.Status4 = tmp.GetDataString("STATUS4");
            obj.StaffCode4 = tmp.GetDataString("STAFF4");
            obj.SaveDate4 = tmp.GetDataString("SAVE_DATE4");
            obj.SaveTime4 = tmp.GetDataString("SAVE_TIME4");

            obj.Status5 = tmp.GetDataString("STATUS5");
            obj.StaffCode5 = tmp.GetDataString("STAFF5");
            obj.SaveDate5 = tmp.GetDataString("SAVE_DATE5");
            obj.SaveTime5 = tmp.GetDataString("SAVE_TIME5");

            obj.PrStaff = tmp.GetDataString("PR_STAFF");
            obj.PrDate = tmp.GetDataString("PR_DATE");
            obj.PrTime = tmp.GetDataString("PR_TIME");

            return obj;
        }


        public static DPCHeader GetData(string pt_id, string start_date, string ward, bool detail = false)
        {
            DPCHeader header = new DPCHeader();

            if (!AppString.IsNumber(pt_id) || !AppString.IsDate(start_date) || ward.Length == 0)
            {
                return header;
            }

            string cmd = "select * from DPC_HEADER " +
                " where PATIENT_ID = " + pt_id +
                " and START_DATE = " + start_date +
                " and WARD = '" + ward + "'";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                header = GetFromStdClass(tmp);
            }

            if (detail)
            {
                header.DetailList = DPCDetail.GetList(header);
            }

            return header;
        }


        public static List<DPCHeader> GetList(string pt_id, bool detail = false)
        {
            List<DPCHeader> list = new List<DPCHeader>();

            if (!AppString.IsNumber(pt_id))
            {
                return list;
            }

            string cmd = "select * from DPC_HEADER " +
                " where PATIENT_ID = " + pt_id +
                " order by ADM_DATE, START_DATE, SEQ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            if (detail)
            {
                foreach (DPCDetail d in DPCDetail.GetList(list))
                {
                    foreach (DPCHeader header in list)
                    {
                        if (header.Id.Equals(d.Id) &&
                            header.StartDate.Equals(d.StartDate) &&
                            header.Ward.Equals(d.Ward))
                        {
                            header.DetailList.Add(d);
                            break;
                        }
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 入院情報（患者ID, 適用日）を指定してリストを取得する
        /// </summary>
        /// <param name="pt_list"></param>
        /// <param name="detail">true: Detail も取得する</param>
        /// <returns></returns>
        public static List<DPCHeader> GetList(List<PatIn> pt_list, bool detail = false)
        {
            List<DPCHeader> list = new List<DPCHeader>();

            string ss = "";

            foreach (PatIn obj in pt_list)
            {
                if (obj.Id.Length == 0 || !AppString.IsDate(obj.DoDate))
                {
                    continue;
                }

                if (ss.Length > 0)
                {
                    ss += ", ";
                }
                
                ss += "(" + obj.Id + ", " + obj.DoDate + ")";
            }

            if (ss.Length == 0)
            {
                return list;
            }

            string cmd = "";

            cmd = "select * from DPC_HEADER " +
                " where (PATIENT_ID, START_DATE) in (" + ss + ") " +
                " order by PATIENT_ID, START_DATE, WARD";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            if (detail)
            {
                foreach (DPCDetail d in DPCDetail.GetList(list))
                {
                    foreach (DPCHeader header in list)
                    {
                        if (header.Id.Equals(d.Id) &&
                            header.StartDate.Equals(d.StartDate) &&
                            header.Ward.Equals(d.Ward))
                        {
                            header.DetailList.Add(d);
                            break;
                        }
                    }
                }
            }

            return list;
        }
/*
        static int GetMaxSEQ(string pt_id, string adm_date)
        {
            int i = 0;

            if (!AppString.IsNumber(pt_id) || !AppString.IsDate(adm_date))
            {
                return i;
            }

            string cmd = "select max(t.SEQ) C " +
                " from DPC_HEADER t " +
                " where t.PATIENT_ID = " + pt_id + " and t.ADM_DATE = " + adm_date;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                i = tmp.GetDataInt("C");
                break;
            }

            return i;
        }
*/

        /*
        public static StdReturn PrSave(string pt_id, string start_date, string ward)
        {
            StdReturn sr = new StdReturn();

            string cmd = "update DPC_HEADER set " +
                " PR_STAFF = " + LoginUser.Id + ", PR_DATE = " + DateTime.Now.ToString("yyyyMMdd") + ", PR_TIME = " + DateTime.Now.ToString("HHmmss") +
                " where PATIENT_ID = " + pt_id +
                " and START_DATE = " + start_date +
                " and WARD = '" + ward + "'";

            sr.IntValue = DB.Db2.ExecuteNonQuery(cmd);

            return sr;
        }
         */

        public static StdReturn PrSave(List<DPCHeader> header_list)
        {
            StdReturn sr = new StdReturn();

            if (header_list.Count == 0) return sr;

            string cmd = "update DPC_HEADER set " +
                " PR_STAFF = " + LoginUser.Id + ", PR_DATE = " + DateTime.Now.ToString("yyyyMMdd") + ", PR_TIME = " + DateTime.Now.ToString("HHmmss") +
                " where (PATIENT_ID, START_DATE, WARD) in (" + AppString.ConcatList(header_list.ConvertAll((x) => { return "(" + x.Id + "," + x.StartDate + ",'" + x.Ward + "')"; }), ",") + ")";

            sr.IntValue = DB.Db2.ExecuteNonQuery(cmd);

            return sr;
        }

        public enum SaveMode : int
        { 
            IMPORT = 0,
            HEADER = 1,
            DETAIL = 2
        }

        /// <summary>
        /// 保存する
        /// </summary>
        /// <param name="kind">0: 全体, 1: 共通, 2: 医師, 3: 看護師（入棟）, 4: 看護師（退棟）, 5: 手術</param>
        /// <param name="header_only">true: HEADER のみ保存</param>
        /// <param name="detail_delete">true: DETAIL をいったん消して Insert, false: DETAIL を消さず Update or Insert</param>
        /// <returns></returns>
        public StdReturn Save(SaveMode mode, int kind = 0)
        {
            StdReturn sr = new StdReturn();

            if (DB.Db2.Connection.State != ConnectionState.Open) DB.Db2.Open();

            using (DbTransaction tran = DB.Db2.Connection.BeginTransaction())
            {
                try
                {
                    StdDbClass obj = new StdDbClass();
                    obj.Db = DB.Db2;

                    obj.Table = "DPC_HEADER";

                    /*
                    if (this.SEQ <= 0)
                    {
                        // 新規作成の場合
                        // SEQ = 最大値 + 1 をセットする
                        this.SEQ = DPCHeader.GetMaxSEQ(this.Id, this.AdmDate) + 1;
                    }
                    */

                    // 入院番号を取得してセットする
                    this.SEQ = PatIn.GetDataByDate(this.Id, this.AdmDate).SEQ;

                    obj.DataList.Add(new StdDbColumn("ADM_DATE", StdDbType.NUMBER, this.AdmDate));
                    obj.DataList.Add(new StdDbColumn("DIS_DATE", StdDbType.NUMBER, this.DisDate));
                    obj.DataList.Add(new StdDbColumn("SEQ", StdDbType.NUMBER, this.SEQ));
                    obj.DataList.Add(new StdDbColumn("END_DATE", StdDbType.NUMBER, this.EndDate));

                    if (mode == SaveMode.IMPORT)
                    {
                        obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, this.Status));
                        obj.DataList.Add(new StdDbColumn("STAFF", StdDbType.NUMBER, LoginUser.Id));
                        obj.DataList.Add(new StdDbColumn("SAVE_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
                        obj.DataList.Add(new StdDbColumn("SAVE_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));

                        // Detail が登録される場合
                        bool b1 = false;
                        bool b2 = false;
                        bool b3 = false;
                        bool b4 = false;
                        bool b5 = false;

                        foreach (DPCDetail detail in this.DetailList)
                        {
                            DPCItem2 item2 = DPCItem2.Dict.ContainsKey(detail.Code) ? DPCItem2.Dict[detail.Code] : null;

                            if (item2 == null) continue;

                            if (item2.Kind.Equals("1")) b1 = true;
                            if (item2.Kind.Equals("2")) b2 = true;
                            if (item2.Kind.Equals("3")) b3 = true;
                            if (item2.Kind.Equals("4")) b4 = true;
                            if (item2.Kind.Equals("5")) b5 = true;
                        }

                        if (b1)
                        {
                            obj.DataList.Add(new StdDbColumn("STAFF1", StdDbType.NUMBER, LoginUser.Id));
                            obj.DataList.Add(new StdDbColumn("SAVE_DATE1", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
                            obj.DataList.Add(new StdDbColumn("SAVE_TIME1", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
                        }

                        if (b2)
                        {
                            obj.DataList.Add(new StdDbColumn("STAFF2", StdDbType.NUMBER, LoginUser.Id));
                            obj.DataList.Add(new StdDbColumn("SAVE_DATE2", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
                            obj.DataList.Add(new StdDbColumn("SAVE_TIME2", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
                        }

                        if (b3)
                        {
                            obj.DataList.Add(new StdDbColumn("STAFF3", StdDbType.NUMBER, LoginUser.Id));
                            obj.DataList.Add(new StdDbColumn("SAVE_DATE3", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
                            obj.DataList.Add(new StdDbColumn("SAVE_TIME3", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
                        }

                        if (b4)
                        {
                            obj.DataList.Add(new StdDbColumn("STAFF4", StdDbType.NUMBER, LoginUser.Id));
                            obj.DataList.Add(new StdDbColumn("SAVE_DATE4", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
                            obj.DataList.Add(new StdDbColumn("SAVE_TIME4", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
                        }

                        if (b5)
                        {
                            obj.DataList.Add(new StdDbColumn("STAFF5", StdDbType.NUMBER, LoginUser.Id));
                            obj.DataList.Add(new StdDbColumn("SAVE_DATE5", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
                            obj.DataList.Add(new StdDbColumn("SAVE_TIME5", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
                        }
                    }
                    else if (mode == SaveMode.HEADER)
                    {
                        obj.DataList.Add(new StdDbColumn("DIAG", StdDbType.VARCHAR2, this.Diag));
                        obj.DataList.Add(new StdDbColumn("DEPT", StdDbType.NUMBER, this.Dept));
                        obj.DataList.Add(new StdDbColumn("DOCTOR1", StdDbType.NUMBER, this.Doctor1));
                        obj.DataList.Add(new StdDbColumn("DOCTOR2", StdDbType.NUMBER, this.Doctor2));
                        obj.DataList.Add(new StdDbColumn("CONT", StdDbType.VARCHAR2, this.Cont));

                        obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, this.Status));
                        obj.DataList.Add(new StdDbColumn("STAFF", StdDbType.NUMBER, LoginUser.Id));
                        obj.DataList.Add(new StdDbColumn("SAVE_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
                        obj.DataList.Add(new StdDbColumn("SAVE_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
                    }
                    else if (mode == SaveMode.DETAIL)
                    {
                        // Header の該当の STATUS, STAFF, SAVE_DATE, SAVE_TIME

                        switch (kind)
                        {
                            case 1:
                                // 共通
                                obj.DataList.Add(new StdDbColumn("STATUS1", StdDbType.NUMBER, this.Status1));
                                obj.DataList.Add(new StdDbColumn("STAFF1", StdDbType.NUMBER, LoginUser.Id));
                                obj.DataList.Add(new StdDbColumn("SAVE_DATE1", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
                                obj.DataList.Add(new StdDbColumn("SAVE_TIME1", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
                                /*
                                obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, this.Status));
                                obj.DataList.Add(new StdDbColumn("STAFF", StdDbType.NUMBER, LoginUser.Id));
                                obj.DataList.Add(new StdDbColumn("SAVE_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
                                obj.DataList.Add(new StdDbColumn("SAVE_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
                                 */
                                break;

                            case 2:
                                // 医師
                                obj.DataList.Add(new StdDbColumn("STATUS2", StdDbType.NUMBER, this.Status2));
                                obj.DataList.Add(new StdDbColumn("STAFF2", StdDbType.NUMBER, LoginUser.Id));
                                obj.DataList.Add(new StdDbColumn("SAVE_DATE2", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
                                obj.DataList.Add(new StdDbColumn("SAVE_TIME2", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
                                break;

                            case 3:
                                // 看護師（入棟）
                                obj.DataList.Add(new StdDbColumn("STATUS3", StdDbType.NUMBER, this.Status3));
                                obj.DataList.Add(new StdDbColumn("STAFF3", StdDbType.NUMBER, LoginUser.Id));
                                obj.DataList.Add(new StdDbColumn("SAVE_DATE3", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
                                obj.DataList.Add(new StdDbColumn("SAVE_TIME3", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
                                break;

                            case 4:
                                // 看護師（退棟）
                                obj.DataList.Add(new StdDbColumn("STATUS4", StdDbType.NUMBER, this.Status4));
                                obj.DataList.Add(new StdDbColumn("STAFF4", StdDbType.NUMBER, LoginUser.Id));
                                obj.DataList.Add(new StdDbColumn("SAVE_DATE4", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
                                obj.DataList.Add(new StdDbColumn("SAVE_TIME4", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
                                break;

                            case 5:
                                // 手術
                                obj.DataList.Add(new StdDbColumn("STATUS5", StdDbType.NUMBER, this.Status5));
                                obj.DataList.Add(new StdDbColumn("STAFF5", StdDbType.NUMBER, LoginUser.Id));
                                obj.DataList.Add(new StdDbColumn("SAVE_DATE5", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
                                obj.DataList.Add(new StdDbColumn("SAVE_TIME5", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
                                /*
                                obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, this.Status));
                                obj.DataList.Add(new StdDbColumn("STAFF", StdDbType.NUMBER, LoginUser.Id));
                                obj.DataList.Add(new StdDbColumn("SAVE_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
                                obj.DataList.Add(new StdDbColumn("SAVE_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
                                 */
                                break;

                            default:
                                break;
                        }
                    }


                    obj.WhereList.Add("PATIENT_ID = " + this.Id);
                    obj.WhereList.Add("START_DATE = " + this.StartDate);
                    obj.WhereList.Add("WARD = '" + this.Ward + "'");

                    sr = obj.UpdateSQL();

                    if (sr.IntValue == 0)
                    {
                        obj.DataList.Add(new StdDbColumn("PATIENT_ID", StdDbType.NUMBER, this.Id));
                        obj.DataList.Add(new StdDbColumn("START_DATE", StdDbType.NUMBER, this.StartDate));
                        obj.DataList.Add(new StdDbColumn("WARD", StdDbType.VARCHAR2, this.Ward));

                        sr = obj.InsertSQL();
                    }


                    obj.Table = "DPC_DETAIL";

                    if (mode == SaveMode.IMPORT)
                    {
                        // 既存の登録があれば上書きする
                        foreach (DPCDetail detail in this.DetailList)
                        {
                            obj.DataList.Clear();
                            obj.WhereList.Clear();

                            obj.DataList.Add(new StdDbColumn("CONT", StdDbType.VARCHAR2, detail.Cont));

                            obj.WhereList.Add("PATIENT_ID = " + this.Id);
                            obj.WhereList.Add("START_DATE = " + this.StartDate);
                            obj.WhereList.Add("WARD = '" + this.Ward + "'");
                            obj.WhereList.Add("CODE = '" + detail.Code + "'");

                            sr = obj.UpdateSQL();

                            if (sr.IntValue == 0)
                            {
                                obj.DataList.Add(new StdDbColumn("PATIENT_ID", StdDbType.NUMBER, this.Id));
                                obj.DataList.Add(new StdDbColumn("START_DATE", StdDbType.NUMBER, this.StartDate));
                                obj.DataList.Add(new StdDbColumn("WARD", StdDbType.VARCHAR2, this.Ward));
                                obj.DataList.Add(new StdDbColumn("CODE", StdDbType.VARCHAR2, detail.Code));

                                sr = obj.InsertSQL();
                            }
                        }
                    }
                    else if (mode == SaveMode.DETAIL)
                    {
                        // いったん該当 kind のデータを消す
                        List<string> codes = new List<string>();

                        foreach (DPCItem2 item2 in DPCItem2.List)
                        {
                            if (item2.Kind.Equals(kind.ToString()))
                            {
                                if (!codes.Contains(item2.Code)) codes.Add(item2.Code);
                            }
                        }

                        if (codes.Count > 0)
                        {
                            DB.Db2.ExecuteNonQuery("delete from DPC_DETAIL where PATIENT_ID = " + this.Id + " and START_DATE = " + this.StartDate + " and WARD = '" + this.Ward + "' and CODE in (" + AppString.ConcatList(codes, ",", "'") + ")");
                        }

                        foreach (DPCDetail detail in this.DetailList)
                        {
                            DPCItem2 item2 = DPCItem2.Dict.ContainsKey(detail.Code) ? DPCItem2.Dict[detail.Code] : null;

                            // 該当 kind でないデータは飛ばす
                            if (item2 == null || !item2.Kind.Equals(kind.ToString())) continue;

                            obj.DataList.Clear();
                            obj.WhereList.Clear();

                            obj.DataList.Add(new StdDbColumn("CONT", StdDbType.VARCHAR2, detail.Cont));

                            obj.WhereList.Add("PATIENT_ID = " + this.Id);
                            obj.WhereList.Add("START_DATE = " + this.StartDate);
                            obj.WhereList.Add("WARD = '" + this.Ward + "'");
                            obj.WhereList.Add("CODE = '" + detail.Code + "'");

                            sr = obj.UpdateSQL();

                            if (sr.IntValue == 0)
                            {
                                obj.DataList.Add(new StdDbColumn("PATIENT_ID", StdDbType.NUMBER, this.Id));
                                obj.DataList.Add(new StdDbColumn("START_DATE", StdDbType.NUMBER, this.StartDate));
                                obj.DataList.Add(new StdDbColumn("WARD", StdDbType.VARCHAR2, this.Ward));
                                obj.DataList.Add(new StdDbColumn("CODE", StdDbType.VARCHAR2, detail.Code));

                                sr = obj.InsertSQL();
                            }
                        }
                    }

                    // Null データは不要なので消す
                    DB.Db2.ExecuteNonQuery("delete from DPC_DETAIL where PATIENT_ID = " + this.Id + " and START_DATE = " + this.StartDate + " and WARD = '" + this.Ward + "' and CONT is null");

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                finally
                {
                    DB.Db2.Close();
                }
            }

            return sr;
        }
    }

    public class DPCDetail
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id = "";

        PatBase _Pat = new PatBase();

        public PatBase Pat
        {
            get
            {
                if (!this._Pat.Id.Equals(this.Id))
                {
                    this._Pat = PatBase.Load(this.Id);
                }

                return this._Pat;
            }
        }

        /// <summary>
        /// 入棟日
        /// </summary>
        public string StartDate = "";

        /// <summary>
        /// 病棟コード
        /// </summary>
        public string Ward = "";

        /// <summary>
        /// コード
        /// </summary>
        public string Code = "";

        /// <summary>
        /// 内容
        /// </summary>
        public string Cont = "";


        public static DPCDetail GetFromStdClass(StdClass tmp)
        {
            DPCDetail obj = new DPCDetail();

            obj.Id = tmp.GetDataString("PATIENT_ID");
            obj.StartDate = tmp.GetDataString("START_DATE");
            obj.Ward = tmp.GetDataString("WARD");
            obj.Code = tmp.GetDataString("CODE");
            obj.Cont = tmp.GetDataString("CONT");

            return obj;
        }

/*
        public static List<DPCDetail> GetList(string pt_id, string start_date, string ward)
        {
            List<DPCDetail> list = new List<DPCDetail>();

            if (!AppString.IsNumber(pt_id) || !AppString.IsDate(start_date) || ward.Length == 0)
            {
                return list;
            }

            string cmd = "select * from DPC_DETAIL " +
                " where PATIENT_ID = " + pt_id +
                " and START_DATE = " + start_date +
                " and WARD = '" + ward + "'" +
                " order by CODE";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }
*/

        public static List<DPCDetail> GetList(DPCHeader header)
        {
            List<DPCDetail> list = new List<DPCDetail>();

            if (!AppString.IsNumber(header.Id) || !AppString.IsDate(header.StartDate) || header.Ward.Length == 0)
            {
                return list;
            }

            string cmd = "select * from DPC_DETAIL " +
                " where PATIENT_ID = " + header.Id +
                " and START_DATE = " + header.StartDate +
                " and WARD = '" + header.Ward + "'" +
                " order by CODE";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }


        public static List<DPCDetail> GetList(List<DPCHeader> header_list)
        {
            List<DPCDetail> list = new List<DPCDetail>();

            string header_str = AppString.ConcatList(header_list.ConvertAll((x) => { return "(" + x.Id + "," + x.StartDate + ",'" + x.Ward + "')"; }), ",");

            if (header_str.Length == 0)
            {
                return list;
            }

            string cmd = "select * from DPC_DETAIL " +
                " where (PATIENT_ID, START_DATE, WARD) in (" + header_str + ")" +
                " order by PATIENT_ID, START_DATE, WARD, CODE";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }
    }
}
