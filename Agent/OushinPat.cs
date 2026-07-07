using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    class OushinPat
    {
        public string PtId = "";

        PatBase _Pat = new PatBase();

        public PatBase Pat
        {
            get
            {
                if (!this._Pat.Id.Equals(this.PtId))
                {
                    this._Pat = PatBase.Load(this.PtId);
                }

                return this._Pat;
            }
        }

        /*
        public string Id = "";
        public string Name = "";
        public string Kana = "";
        public string Sex = "";
        public string Birth = "";

        public string Age
        {
            get
            {
                string result = "";

                if (Birth.Replace("/", "").Length == 8)
                {
                    result = DateConvert.CalcAge(Birth.Replace("/", ""), DateTime.Now.ToString("yyyyMMdd")).ToString();
                }

                return result;
            }
        }
        */

//        public string Bikou = "";
        public string Cont = "";
        public string StaffCode = "";

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

        public string SaveDate = "";
        public string Status = "";

        static OushinPat GetFromStdClass(StdClass tmp)
        {
            OushinPat obj = new OushinPat();

            obj.Cont = tmp.GetDataString("CONT");
            obj.StaffCode = tmp.GetDataString("STAFF");
            obj.SaveDate = tmp.GetDataString("SAVE_DATE");
            obj.Status = tmp.GetDataString("STATUS");

#if INNO
            obj.PtId = tmp.GetDataString("P_ID");
            obj._Pat.Id = tmp.GetDataString("P_ID");
            obj._Pat.Name = tmp.GetDataString("P_NAME").Trim();
            obj._Pat.Kana = tmp.GetDataString("P_KANA").Trim();
            obj._Pat.Sex = tmp.GetDataString("P_SEX");
            obj._Pat.Birth = tmp.GetDataString("P_BIRTHDAY_AD");

            obj._Pat.Tel = tmp.GetDataString("TEL");
            obj._Pat.Post = tmp.GetDataString("POST");
            obj._Pat.Addr1 = tmp.GetDataString("ADDR_1").Trim();
            obj._Pat.Addr2 = tmp.GetDataString("ADDR_2").Trim();
            obj._Pat.Dead = tmp.GetDataString("PROPERTY_2");
            obj._Pat.NoteCode = tmp.GetDataString("PROPERTY_4");
#else
            obj.PtId = tmp.GetDataString("IM01RC_F01");
            obj._Pat.Id = tmp.GetDataString("IM01RC_F01");
            obj._Pat.Name = tmp.GetDataString("IM01RC_F04").Trim();
            obj._Pat.Kana = tmp.GetDataString("IM01RC_F03").Trim();
            obj._Pat.Sex = tmp.GetDataString("IM01RC_F05");
            obj._Pat.Birth = tmp.GetDataString("IM01RC_F10");

            obj._Pat.Tel = tmp.GetDataString("IM01RC_F08");
            obj._Pat.Post = tmp.GetDataString("IM01RC_F14");
            obj._Pat.Addr1 = tmp.GetDataString("IM01RC_F15").Trim();
            obj._Pat.Addr2 = tmp.GetDataString("IM01RC_F16").Trim();
            obj._Pat.Dead = tmp.GetDataString("IM01RC_F13_2");
            obj._Pat.NoteCode = tmp.GetDataString("IM01RC_F13_4");
#endif
            return obj;
        }

        /// <summary>
        /// 往診患者データをＤＢからロードする。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static OushinPat Load(string id)
        {
            OushinPat pat = new OushinPat();

            if (id.Length == 0)
            {
                return pat;
            }

#if INNO
            string cmd = "select * " +
                " from OUSHIN t, M_PATIENT" + Env.DB_LINK + " tm " +
                " where tm.P_ID = " + id +
                " and t.PATIENT_ID(+) = tm.P_ID";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);
#else
            string cmd = "select * " +
                " from OUSHIN t, IM01RC" + Env.DB_LINK + " tm " +
                " where tm.IM01RC_F01 = " + id +
                " and t.PATIENT_ID(+) = tm.IM01RC_F01";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);
#endif
            foreach (StdClass tmp in tmp_list)
            {
                pat = GetFromStdClass(tmp);
                break;
            }
/*

            // cmd.CommandText = "select CONT, STAFF, SAVE_DATE, STATUS, Trim(IM01RC_F03) as カナ, Trim(IM01RC_F04) as 氏名, IM01RC_F05 as 性別, IM01RC_F10 as 生年月日, IM01RC_F13_4 as 備考 from IM01RC" + Env.DB_LINK + " left join OUSHIN on IM01RC_F01 = PATIENT_ID where IM01RC_F01 = " + id;
            // 20100821 matsui 
            cmd.CommandText = "select i.IM01RC_F01 as 患者コード, Trim(i.IM01RC_F04) as 氏名, Trim(i.IM01RC_F03) as カナ,i.IM01RC_F05 as 性別, i.IM01RC_F05 as 性別, i.IM01RC_F10 as 生年月日, i.IM01RC_F13_4 as 備考, i.IM01RC_F13_2 as おくやみ , o.CONT ,o.STAFF , o.SAVE_DATE , o.STATUS  from IM01RC" + Env.DB_LINK + " i left outer join OUSHIN o on i.IM01RC_F01 = o.PATIENT_ID  where  ( i.IM01RC_F13_4 = 2 or i.IM01RC_F13_4 = 5) and  IM01RC_F01 = " + id;

            OleDbDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                pat.Id = id;
                pat.Name = reader["氏名"].ToString();
                pat.Kana = reader["カナ"].ToString();

                if (reader["性別"].ToString().Equals("1"))
                {
                    pat.Sex = "男";
                }
                else if (reader["性別"].ToString().Equals("2"))
                {
                    pat.Sex = "女";
                }

                pat.Birth = reader["生年月日"].ToString().Insert(4, "/").Insert(7, "/");

                if (reader["備考"].ToString().Equals("2"))
                {
                    pat.Bikou = "☆";
                }

                // 20100821 matsui 削除ずみのレコードもそのまま表示する。
                pat.Cont = reader["CONT"].ToString();

                //pat.StaffCode = reader["STAFF"].ToString();

                //if (Dict.StaffDict.ContainsKey(pat.StaffCode))
                //{
                //    pat.StaffName = Dict.StaffDict[pat.StaffCode].Name;
                //}

                //if (reader["SAVE_DATE"].ToString().Length == 8)
                //{
                //    pat.SaveDate = reader["SAVE_DATE"].ToString().Insert(4, "/").Insert(7, "/");
                //}

                //pat.Status = reader["STATUS"].ToString();

                pat.StaffCode = "";
                pat.SaveDate = "";

                if (reader["おくやみ"].ToString().Equals("0"))
                {
                    pat.Status = "1";
                }
                else
                {
                    pat.Status = "0";
                }
            }

*/

            return pat;
        }

        /// <summary>
        /// 往診患者リストをＤＢからロードする。
        /// </summary>
        /// <returns></returns>
        public static List<OushinPat> LoadAll()
        {
            List<OushinPat> list = new List<OushinPat>();
#if INNO
            string cmd = "select * " +
                " from OUSHIN t, M_PATIENT" + Env.DB_LINK + " tm " +
                " where (tm.PROPERTY_4 in (2, 5) or tm.P_ID in (t.PATIENT_ID)) " +
                " and (t.STATUS is null or t.STATUS != 0) " +
                " and t.PATIENT_ID(+) = tm.P_ID" +
                " order by tm.P_ID";
#else
            string cmd = "select * " +
                " from OUSHIN t, IM01RC" + Env.DB_LINK + " tm " +
                " where (tm.IM01RC_F13_4 in (2, 5) or tm.IM01RC_F01 in (t.PATIENT_ID)) " +
                " and (t.STATUS is null or t.STATUS != 0) " +
                " and t.PATIENT_ID(+) = tm.IM01RC_F01" +
                " order by tm.IM01RC_F01";
#endif
            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                OushinPat pat = GetFromStdClass(tmp);
                list.Add(pat);
            }

            /*
            OleDbConnection con = DBConn.GetOpenDBConn();
            con.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = con;

            // cmd.CommandText = "select PATIENT_ID, CONT, STAFF, SAVE_DATE, STATUS, Trim(IM01RC_F03) as カナ, Trim(IM01RC_F04) as 氏名, IM01RC_F05 as 性別, IM01RC_F10 as 生年月日, IM01RC_F13_4 as 備考 from OUSHIN inner join IM01RC" + Env.DB_LINK + " on PATIENT_ID = IM01RC_F01";
            // 20100821 matsui 
            cmd.CommandText = "select i.IM01RC_F01 as 患者コード, Trim(i.IM01RC_F04) as 氏名, Trim(i.IM01RC_F03) as カナ,i.IM01RC_F05 as 性別, i.IM01RC_F05 as 性別, i.IM01RC_F10 as 生年月日, i.IM01RC_F13_4 as 備考, i.IM01RC_F13_2 as おくやみ , o.CONT ,o.STAFF , o.SAVE_DATE , o.STATUS  from IM01RC" + Env.DB_LINK + " i left outer join OUSHIN o on i.IM01RC_F01 = o.PATIENT_ID  where  i.IM01RC_F13_4 = 2 or i.IM01RC_F13_4 = 5";

            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                OushinPat pat = new OushinPat();

                pat.Id = reader["患者コード"].ToString();
                pat.Name = reader["氏名"].ToString();
                pat.Kana = reader["カナ"].ToString();

                if (reader["性別"].ToString().Equals("1"))
                {
                    pat.Sex = "男";
                }
                else if (reader["性別"].ToString().Equals("2"))
                {
                    pat.Sex = "女";
                }

                pat.Birth = reader["生年月日"].ToString().Insert(4, "/").Insert(7, "/");

                if (reader["備考"].ToString().Equals("2"))
                {
                    pat.Bikou = "☆";
                }

                // 20100821 matsui 
                // OUSHIN テーブルからは CONT (コメント)のみ取り出す。
                pat.Cont = reader["CONT"].ToString();

                //pat.StaffCode = reader["STAFF"].ToString();

                //if (Dict.StaffDict.ContainsKey(pat.StaffCode))
                //{
                //    pat.StaffName = Dict.StaffDict[pat.StaffCode].Name;
                //}
                
                //pat.SaveDate = reader["SAVE_DATE"].ToString().Insert(4, "/").Insert(7, "/");
                // pat.Status = reader["STATUS"].ToString();

                pat.StaffCode = "";
                pat.SaveDate = "";

                if (reader["おくやみ"].ToString().Equals("0"))
                {
                    pat.Status = "1";
                }
                else
                {
                    pat.Status = "0";
                }

                list.Add(pat);
            }

            con.Close();
            */

            return list;
        }

        public StdReturn Save()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();

            obj.Table = "OUSHIN";
            obj.Db = DB.Db2;

            obj.DataList.Add(new StdDbColumn("CONT", StdDbType.VARCHAR2, this.Cont));
            obj.DataList.Add(new StdDbColumn("STAFF", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("SAVE_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, this.Status));

            obj.WhereList.Add("PATIENT_ID = " + this.PtId);

            sr = obj.UpdateSQL();

            if (sr.IntValue == 0)
            {
                obj.DataList.Add(new StdDbColumn("PATIENT_ID", StdDbType.NUMBER, this.PtId));
                sr = obj.InsertSQL();
            }

            return sr;
        }

        public static StdReturn Delete(string pt_id)
        {
            StdReturn sr = new StdReturn();

            if (pt_id.Length == 0)
            {
                return sr;
            }

            StdDbClass obj = new StdDbClass();

            obj.Table = "OUSHIN";
            obj.Db = DB.Db2;

            obj.DataList.Add(new StdDbColumn("STAFF", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("SAVE_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, 0));

            obj.WhereList.Add("PATIENT_ID = " + pt_id);

            sr = obj.UpdateSQL();

            return sr;
        }
    }
}
