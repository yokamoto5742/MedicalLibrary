using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class OpeNursingData : StdEntity
    {
        new public static DB Db = DB.Db2;

        public static string PDFFolder = "手術記録";

        public static string PDFCode = "34411";

        public int Id = 0;

        PatBase _Pat = new PatBase();

        public PatBase Pat
        {
            get
            {
                return this._Pat;
            }
        }

        /// <summary>
        /// 入外区分コード
        /// </summary>
        public string InOut = "";

        /// <summary>
        /// 入外区分
        /// </summary>
        public string InOutString
        {
            get
            {
                string result = "";

                if (InOut.Equals("1"))
                {
                    result = "外来";
                }
                else if (InOut.Equals("2"))
                {
                    result = "入院";
                }

                return result;
            }
        }

        /// <summary>
        /// 入外区分
        /// </summary>
        public string InOutStringShort
        {
            get
            {
                string result = "";

                if (InOut.Equals("1"))
                {
                    result = "外";
                }
                else if (InOut.Equals("2"))
                {
                    result = "入";
                }

                return result;
            }
        }

        /// <summary>
        /// 診療科コード
        /// </summary>
        public string Dept = "";

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
                    result = Dict.DeptDict[Dept].FullName;
                }

                return result;
            }
        }

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

        public string Infection = "";

        /// <summary>
        /// 感染症検査結果に "+" が含まれていれば +, なければ - を返す。
        /// </summary>
        public string InfectionFlg
        {
            get
            {
                string result = "";

                if (Infection.Contains("+"))
                {
                    result = "+";
                }
                else
                {
                    result = "-";
                }

                return result;
            }
        }

        public string Ins = "";

        public string InsName
        {
            get
            {
                return PatIns.GetData(this.Pat.Id, this.Ins).KindNameShort;
            }
        }

        public string OpeDate = "";

        public string OpeDateString
        {
            get
            {
                return DateTimeAgent.DateFormat(OpeDate, DateTimeAgent.DateFormatKind.LONG);
            }
        }

        public string OpeDateStringShort
        {
            get
            {
                return DateTimeAgent.DateFormat(OpeDate, DateTimeAgent.DateFormatKind.SHORT);
            }
        }

        /// <summary>
        /// 記録種別
        /// 1 術前, 2 術中, 3 術後
        /// </summary>
        public string RecKind = "";

        public string RecKindString
        {
            get
            {
                string result = "";

                if (RecKind.Equals("1"))
                {
                    result = "術前";
                }
                else if (RecKind.Equals("2"))
                {
                    result = "術中";
                }
                else if (RecKind.Equals("3"))
                {
                    result = "術後";
                }

                return result;
            }
        }

        public string RecKindStringShort
        {
            get
            {
                string result = "";

                if (RecKind.Equals("1"))
                {
                    result = "前";
                }
                else if (RecKind.Equals("2"))
                {
                    result = "中";
                }
                else if (RecKind.Equals("3"))
                {
                    result = "後";
                }

                return result;
            }
        }

        /// <summary>
        /// 1 緊内, 2 緊外
        /// </summary>
        public string TimeOut = "";

        public string TimeOutString
        {
            get
            {
                string result = "";

                if (TimeOut.Equals("1"))
                {
                    result = "緊内";
                }
                else if (TimeOut.Equals("2"))
                {
                    result = "緊外";
                }

                return result;
            }
        }

        public string Ope = "";
        public string Part = "";
        public string Anes = "";
        public string Doctor1 = "";
        public string Doctor2 = "";
        public string Doctor3 = "";

        public string Ns1 = "";
        public string Ns2 = "";
        public string Ns3 = "";

        public string Room = "";
        public string NsRec = "";

        public int AnesTime1 = -1;

        public string AnesTimeString1
        {
            get
            {
                string result = "";

                if (AnesTime1 >= 0)
                {
                    result = AnesTime1.ToString().PadLeft(4, '0').Insert(2, ":");
                }

                return result;
            }
        }

        public int AnesTime2 = -1;

        public string AnesTimeString2
        {
            get
            {
                string result = "";

                if (AnesTime2 >= 0)
                {
                    result = AnesTime2.ToString().PadLeft(4, '0').Insert(2, ":");
                }

                return result;
            }
        }

        public int OpeTime1 = -1;

        public string OpeTimeString1
        {
            get
            {
                string result = "";

                if (OpeTime1 >= 0)
                {
                    result = OpeTime1.ToString().PadLeft(4, '0').Insert(2, ":");
                }

                return result;
            }
        }

        public int OpeTime2 = -1;

        public string OpeTimeString2
        {
            get
            {
                string result = "";

                if (OpeTime2 >= 0)
                {
                    result = OpeTime2.ToString().PadLeft(4, '0').Insert(2, ":");
                }

                return result;
            }
        }

        /// <summary>
        /// 入室時刻
        /// </summary>
        public int RoomTime1 = -1;

        public string RoomTimeString1
        {
            get
            {
                string result = "";

                if (RoomTime1 >= 0)
                {
                    result = RoomTime1.ToString().PadLeft(4, '0').Insert(2, ":");
                }

                return result;
            }
        }

        /// <summary>
        /// 退室時刻
        /// </summary>
        public int RoomTime2 = -1;

        public string RoomTimeString2
        {
            get
            {
                string result = "";

                if (RoomTime2 >= 0)
                {
                    result = RoomTime2.ToString().PadLeft(4, '0').Insert(2, ":");
                }

                return result;
            }
        }

        public string RecHist = "";

        public string Status = "";

        public string StatusFlg
        {
            get
            {
                string result = "";

                if (Status.Equals("1"))
                {
                    result = "●";
                }

                return result;
            }
        }

        public string SaveDate = "";

        public string SaveTime = "";

        public string PDFSave = "";

        public string PDFFile
        {
            get
            {
                return this.Pat.Id.PadLeft(9, '0') + OpeNursingData.PDFCode + this.Dept.PadLeft(3, '0') + this.Staff.PadLeft(5, '0') + this.OpeDate.PadRight(8, '0') + this.SaveTime.PadRight(6, '0') + ".pdf";
            }
        }

        public Dictionary<string, OpeNursingAs> AsDict = new Dictionary<string, OpeNursingAs>();

        public Dictionary<string, OpeNursingSchema> SchemaDict = new Dictionary<string, OpeNursingSchema>();

        public void GetAsDict()
        {
            AsDict = OpeNursingAs.GetDict(Id);
        }

        public void GetSchemaDict()
        {
            SchemaDict = OpeNursingSchema.GetDict(Id);
        }

        static OpeNursingData GetFromStdClass(StdClass tmp)
        {
            OpeNursingData obj = new OpeNursingData();

            obj.Id = tmp.GetDataInt("ID");

            obj._Pat.Id = tmp.GetDataString("PATIENT_ID");
            obj._Pat.Name = tmp.GetDataString("氏名").Trim();
            obj._Pat.Kana = tmp.GetDataString("カナ").Trim();
            obj._Pat.Sex = tmp.GetDataString("性別");
            obj._Pat.Birth = tmp.GetDataString("生年月日");

            obj.Infection = tmp.GetDataString("INFECTION");
            obj.Ins = tmp.GetDataString("INS");
            obj.OpeDate = tmp.GetDataString("OPE_DATE");
            obj.InOut = tmp.GetDataString("IN_OUT");
            obj.TimeOut = tmp.GetDataString("TIME_OUT");
            obj.Dept = tmp.GetDataString("DEPT");
            obj.Ope = tmp.GetDataString("OPE");
            obj.Part = tmp.GetDataString("PART");
            obj.Anes = tmp.GetDataString("ANES");
            obj.Doctor1 = tmp.GetDataString("DOCTOR1");
            obj.Doctor2 = tmp.GetDataString("DOCTOR2");
            obj.Doctor3 = tmp.GetDataString("DOCTOR3");
            obj.Ns1 = tmp.GetDataString("NS1");
            obj.Ns2 = tmp.GetDataString("NS2");
            obj.Ns3 = tmp.GetDataString("NS3");
            obj.Room = tmp.GetDataString("ROOM");
            obj.NsRec = tmp.GetDataString("NS_REC");
            obj.Staff = tmp.GetDataString("STAFF");
            obj.Status = tmp.GetDataString("STATUS");
            obj.SaveDate = tmp.GetDataString("SAVE_DATE");
            obj.SaveTime = tmp.GetDataString("SAVE_TIME");
            obj.PDFSave = tmp.GetDataString("PDF_SAVE");

            obj.AnesTime1 = tmp.GetDataInt("ANES_TIME1", 0);
            obj.AnesTime2 = tmp.GetDataInt("ANES_TIME2", 0);
            obj.OpeTime1 = tmp.GetDataInt("OPE_TIME1", 0);
            obj.OpeTime2 = tmp.GetDataInt("OPE_TIME2", 0);

            obj.RoomTime1 = tmp.GetDataInt("ROOM_TIME1", 0);
            obj.RoomTime2 = tmp.GetDataInt("ROOM_TIME2", 0);

            obj.RecHist = tmp.GetDataString("REC_HIST");
            obj.RecKind = tmp.GetDataString("REC_KIND");

            return obj;
        }

        public static OpeNursingData Load(string id)
        {
            OpeNursingData p = new OpeNursingData();
#if INNO
            string cmd = "select OPE_NURSING.*, Trim(P_KANA) as カナ, Trim(P_NAME) as 氏名, P_SEX as 性別, P_BIRTHDAY_AD as 生年月日 " +
                " from OPE_NURSING inner join M_PATIENT" + Env.DB_LINK + " on PATIENT_ID = P_ID " +
                " where ID = " + id;
#else
            string cmd = "select OPE_NURSING.*, Trim(IM01RC_F03) as カナ, Trim(IM01RC_F04) as 氏名, IM01RC_F05 as 性別, IM01RC_F10 as 生年月日 " +
                " from OPE_NURSING inner join IM01RC" + Env.DB_LINK + " on PATIENT_ID = IM01RC_F01 " +
                " where ID = " + id;
#endif
            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                p = GetFromStdClass(tmp);
                break;
            }

            return p;
        }

        /// <summary>
        /// 手術看護記録を検索する。
        /// </summary>
        /// <param name="pt_id"></param>
        /// <returns></returns>
        public static List<OpeNursingData> Find(string pt_id)
        {
            List<OpeNursingData> list = new List<OpeNursingData>();
#if INNO
            string cmd = "select OPE_NURSING.*, Trim(P_KANA) as カナ, Trim(P_NAME) as 氏名, P_SEX as 性別, P_BIRTHDAY_AD as 生年月日 " +
                " from OPE_NURSING inner join M_PATIENT" + Env.DB_LINK + " on PATIENT_ID = P_ID " +
                " where PATIENT_ID = " + pt_id +
                " order by OPE_DATE desc";
#else
            string cmd = "select OPE_NURSING.*, Trim(IM01RC_F03) as カナ, Trim(IM01RC_F04) as 氏名, IM01RC_F05 as 性別, IM01RC_F10 as 生年月日 " +
                " from OPE_NURSING inner join IM01RC" + Env.DB_LINK + " on PATIENT_ID = IM01RC_F01 " +
                " where PATIENT_ID = " + pt_id +
                " order by OPE_DATE desc";
#endif
            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }

        /// <summary>
        /// 手術看護記録を検索する。
        /// </summary>
        /// <param name="start_date"></param>
        /// <param name="end_date"></param>
        /// <returns></returns>
        public static List<OpeNursingData> Find(string start_date, string end_date)
        {
            List<OpeNursingData> list = new List<OpeNursingData>();
#if INNO
            string cmd = "select OPE_NURSING.*, Trim(P_KANA) as カナ, Trim(P_NAME) as 氏名, P_SEX as 性別, P_BIRTHDAY_AD as 生年月日 " +
                " from OPE_NURSING inner join M_PATIENT" + Env.DB_LINK + " on PATIENT_ID = P_ID " +
                " where OPE_NURSING.OPE_DATE >= " + start_date + " and OPE_NURSING.OPE_DATE <= " + end_date + " and STATUS != 0 " +
                " order by OPE_DATE desc";
#else
            string cmd = "select OPE_NURSING.*, Trim(IM01RC_F03) as カナ, Trim(IM01RC_F04) as 氏名, IM01RC_F05 as 性別, IM01RC_F10 as 生年月日 " +
                " from OPE_NURSING inner join IM01RC" + Env.DB_LINK + " on PATIENT_ID = IM01RC_F01 " +
                " where OPE_NURSING.OPE_DATE >= " + start_date + " and OPE_NURSING.OPE_DATE <= " + end_date + " and STATUS != 0 " +
                " order by OPE_DATE desc";
#endif
            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }

        public StdReturn Save()
        {
            StdReturn sr = new StdReturn();
            string cmd = "";

            StdDbClass obj = new StdDbClass();
            obj.Db = DB.Db2;

            obj.Table = "OPE_NURSING";

            obj.DataList.Add(new StdDbColumn("PATIENT_ID", StdDbType.NUMBER, _Pat.Id));
            obj.DataList.Add(new StdDbColumn("INFECTION", StdDbType.VARCHAR2, Infection));
            obj.DataList.Add(new StdDbColumn("INS", StdDbType.NUMBER, Ins));
            obj.DataList.Add(new StdDbColumn("OPE_DATE", StdDbType.NUMBER, OpeDate));
            obj.DataList.Add(new StdDbColumn("IN_OUT", StdDbType.NUMBER, InOut));
            obj.DataList.Add(new StdDbColumn("TIME_OUT", StdDbType.NUMBER, TimeOut));
            obj.DataList.Add(new StdDbColumn("DEPT", StdDbType.NUMBER, Dept));
            obj.DataList.Add(new StdDbColumn("OPE", StdDbType.VARCHAR2, Ope));
            obj.DataList.Add(new StdDbColumn("PART", StdDbType.VARCHAR2, Part));
            obj.DataList.Add(new StdDbColumn("ANES", StdDbType.VARCHAR2, Anes));
            obj.DataList.Add(new StdDbColumn("DOCTOR1", StdDbType.VARCHAR2, Doctor1));
            obj.DataList.Add(new StdDbColumn("DOCTOR2", StdDbType.VARCHAR2, Doctor2));
            obj.DataList.Add(new StdDbColumn("DOCTOR3", StdDbType.VARCHAR2, Doctor3));
            obj.DataList.Add(new StdDbColumn("NS1", StdDbType.VARCHAR2, Ns1));
            obj.DataList.Add(new StdDbColumn("NS2", StdDbType.VARCHAR2, Ns2));
            obj.DataList.Add(new StdDbColumn("NS3", StdDbType.VARCHAR2, Ns3));
            obj.DataList.Add(new StdDbColumn("ROOM", StdDbType.VARCHAR2, Room));
            obj.DataList.Add(new StdDbColumn("NS_REC", StdDbType.VARCHAR2, NsRec));

            obj.DataList.Add(new StdDbColumn("ANES_TIME1", StdDbType.NUMBER, AnesTime1));
            obj.DataList.Add(new StdDbColumn("ANES_TIME2", StdDbType.NUMBER, AnesTime2));
            obj.DataList.Add(new StdDbColumn("OPE_TIME1", StdDbType.NUMBER, OpeTime1));
            obj.DataList.Add(new StdDbColumn("OPE_TIME2", StdDbType.NUMBER, OpeTime2));

            obj.DataList.Add(new StdDbColumn("ROOM_TIME1", StdDbType.NUMBER, RoomTime1));
            obj.DataList.Add(new StdDbColumn("ROOM_TIME2", StdDbType.NUMBER, RoomTime2));

            obj.DataList.Add(new StdDbColumn("REC_HIST", StdDbType.VARCHAR2, RecHist));
            obj.DataList.Add(new StdDbColumn("REC_KIND", StdDbType.NUMBER, RecKind));

            obj.DataList.Add(new StdDbColumn("STAFF", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("SAVE_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("SAVE_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));

            obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, Status));

            // DELフラグをリセット
            obj.DataList.Add(new StdDbColumn("DEL_STAFF", StdDbType.NUMBER, null));
            obj.DataList.Add(new StdDbColumn("DEL_DATE", StdDbType.NUMBER, null));
            obj.DataList.Add(new StdDbColumn("DEL_TIME", StdDbType.NUMBER, null));

            // PDFフラグもリセット
            obj.DataList.Add(new StdDbColumn("PDF_SAVE", StdDbType.NUMBER, null));
            obj.DataList.Add(new StdDbColumn("PDF_DATE", StdDbType.NUMBER, null));
            obj.DataList.Add(new StdDbColumn("PDF_TIME", StdDbType.NUMBER, null));

            obj.WhereList.Add("ID = " + this.Id);

            sr = obj.UpdateSQL();

            // update 対象が無ければ新規登録
            if (sr.IntValue == 0)
            {
                cmd = "select OPE_NURSING_SEQ.nextval ID from DUAL";

                List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    int.TryParse(tmp.DataDict["ID"].ToString(), out this.Id);
                    break;
                }

                obj.DataList.Add(new StdDbColumn("ID", StdDbType.NUMBER, this.Id));

                sr = obj.InsertSQL();
            }

            int seq = 1;

            cmd = "delete from OPE_NURSING_AS where OPE_NURSING_ID = " + this.Id;
            DB.Db2.ExecuteNonQuery(cmd);

            foreach (OpeNursingAs p in AsDict.Values)
            {
                obj.Table = "OPE_NURSING_AS";
                obj.DataList.Clear();
                obj.WhereList.Clear();

                obj.DataList.Add(new StdDbColumn("OPE_NURSING_ID", StdDbType.NUMBER, this.Id));
                obj.DataList.Add(new StdDbColumn("AS_ID", StdDbType.NUMBER, seq));
                obj.DataList.Add(new StdDbColumn("AS_TITLE", StdDbType.VARCHAR2, p.Title));
                obj.DataList.Add(new StdDbColumn("AS_TEXT", StdDbType.VARCHAR2, p.Text));

                obj.InsertSQL();
                seq++;
            }

            seq = 1;

            cmd = "delete from OPE_NURSING_SCHEMA where OPE_NURSING_ID = " + this.Id;
            DB.Db2.ExecuteNonQuery(cmd);

            foreach (OpeNursingSchema p in SchemaDict.Values)
            {
                obj.Table = "OPE_NURSING_SCHEMA";
                obj.DataList.Clear();
                obj.WhereList.Clear();

                obj.DataList.Add(new StdDbColumn("OPE_NURSING_ID", StdDbType.NUMBER, this.Id));
                obj.DataList.Add(new StdDbColumn("SCHEMA_ID", StdDbType.NUMBER, seq));
                obj.DataList.Add(new StdDbColumn("SCHEMA_BG", StdDbType.NUMBER, p.Bg));
                obj.DataList.Add(new StdDbColumn("SCHEMA_ITEM", StdDbType.VARCHAR2, p.Item));

                obj.InsertSQL();
                seq++;
            }

            return sr;
        }

        public static StdReturn Delete(int id)
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
            obj.Db = DB.Db2;

            obj.Table = "OPE_NURSING";
            obj.DataList.Add(new StdDbColumn("DEL_STAFF", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("DEL_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("DEL_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, 0));

            // PDFフラグもリセット
            obj.DataList.Add(new StdDbColumn("PDF_SAVE", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("PDF_DATE", StdDbType.NUMBER, null));
            obj.DataList.Add(new StdDbColumn("PDF_TIME", StdDbType.NUMBER, null));

            obj.WhereList.Add("ID = " + id);

            sr = obj.UpdateSQL();

            return sr;
        }
/*
        /// <summary>
        /// PDFフラグをリセットする
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static StdReturn PdfClear(int id)
        {
            StdReturn sr = new StdReturn();

            string cmd = "update OPE_NURSING " +
                " set PDF_SAVE = null, PDF_DATE = null, PDF_TIME = null " +
                " where ID = " + id;

            sr.IntValue = DB.Db2.ExecuteNonQuery(cmd);

            return sr;
        }
 */
    }
}
