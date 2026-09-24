using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class EyeKensa2 : EyeKensaBase
    {
        public string KensaSEQ = "";

        /// <summary>
        /// データベースに保存する。
        /// </summary>
        public void Save()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
            obj.Db = DB.Db2;

            obj.Table = "EYE_KENSA2";

            obj.DataList.Add(new StdDbColumn("CONT", StdDbType.VARCHAR2, this.Cont));
            obj.DataList.Add(new StdDbColumn("STAFF", StdDbType.NUMBER, this.Staff));
            obj.DataList.Add(new StdDbColumn("SAVE_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("SAVE_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));

            obj.WhereList.Add("PATIENT_ID = " + this.PtId);
            obj.WhereList.Add("KENSA_ID = " + this.KensaId);
            obj.WhereList.Add("KENSA_DATE = " + this.KensaDate);
            obj.WhereList.Add("KENSA_SEQ = " + this.KensaSEQ);

            sr = obj.UpdateSQL();

            // update 対象が無ければ新規登録
            if (sr.IntValue == 0)
            {
                obj.DataList.Add(new StdDbColumn("PATIENT_ID", StdDbType.NUMBER, this.PtId));
                obj.DataList.Add(new StdDbColumn("KENSA_ID", StdDbType.NUMBER, this.KensaId));
                obj.DataList.Add(new StdDbColumn("KENSA_DATE", StdDbType.NUMBER, this.KensaDate));
                obj.DataList.Add(new StdDbColumn("KENSA_SEQ", StdDbType.NUMBER, this.KensaSEQ));
                sr = obj.InsertSQL();
            }
        }

        static EyeKensa2 GetFromStdClass(StdClass tmp)
        {
            EyeKensa2 obj = new EyeKensa2();

            obj.PtId = tmp.DataDict["PATIENT_ID"].ToString();
            obj.KensaDate = tmp.DataDict["KENSA_DATE"].ToString();
            obj.KensaId = tmp.DataDict["KENSA_ID"].ToString();
            obj.KensaSEQ = tmp.DataDict["KENSA_SEQ"].ToString();
            obj.Cont = tmp.DataDict["CONT"].ToString();
            obj.Staff = tmp.DataDict["STAFF"].ToString();
            obj.SaveDate = tmp.DataDict["SAVE_DATE"].ToString();
            obj.SaveTime = tmp.DataDict["SAVE_TIME"].ToString();
            obj.PDFSave = tmp.DataDict["PDF_SAVE"].ToString();

            return obj;
        }

        /// <summary>
        /// データベースからロードする。
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="start_date"></param>
        /// <returns></returns>
        public static List<EyeKensa2> LoadByPatient(string patient_id, string start_date = "")
        {
            string start_date_2 = "";

            List<EyeKensa2> tmpList = new List<EyeKensa2>();

            if (patient_id.Length == 0)
            {
                return tmpList;
            }

            if (start_date.Length == 8)
            {
                start_date_2 = start_date;
            }
            else
            {
                start_date_2 = "19900101";
            }

            string cmd = "select * from EYE_KENSA2 " +
                " where PATIENT_ID = " + patient_id + " and KENSA_DATE >= " + start_date_2 +
                " order by KENSA_DATE desc, KENSA_ID";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                tmpList.Add(GetFromStdClass(tmp));
            }

            return tmpList;
        }

        /// <summary>
        /// データベースからロードする。
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="kensa_date"></param>
        /// <returns></returns>
        public static List<EyeKensa2> LoadByPatDate(string patient_id, string kensa_date)
        {
            List<EyeKensa2> tmpList = new List<EyeKensa2>();

            if (patient_id.Length == 0 || kensa_date.Length != 8)
            {
                return tmpList;
            }

            string cmd = "select * from EYE_KENSA2 " +
                " where PATIENT_ID = " + patient_id + " and KENSA_DATE = " + kensa_date;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                tmpList.Add(GetFromStdClass(tmp));
            }

            return tmpList;
        }

        /// <summary>
        /// データベースからロードする。
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="kensa_id"></param>
        /// <param name="kensa_date"></param>
        /// <returns></returns>
        public static List<EyeKensa2> LoadByPatKensaDate(string patient_id, string kensa_id, string kensa_date)
        {
            List<EyeKensa2> tmpList = new List<EyeKensa2>();

            if (patient_id.Length == 0 || kensa_id.Length == 0 || kensa_date.Length != 8)
            {
                return tmpList;
            }

            string cmd = "select * from EYE_KENSA2 " +
                " where PATIENT_ID = " + patient_id + " and KENSA_ID = " + kensa_id + " and KENSA_DATE = " + kensa_date +
                " order by KENSA_SEQ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                tmpList.Add(GetFromStdClass(tmp));
            }

            return tmpList;
        }

        /// <summary>
        /// データベースからロードする。（検索）
        /// </summary>
        /// <param name="kensa_id"></param>
        /// <param name="start_date"></param>
        /// <param name="end_date"></param>
        /// <param name="pat"></param>
        /// <param name="limit">取得件数の上限（0は無制限）</param>
        /// <param name="db">眼科DBへの接続（省略時は DB.Db2）</param>
        /// <param name="pat_db">患者マスタDBへの接続（省略時は DB.Db3）</param>
        /// <param name="progress">進捗の通知先（省略可）</param>
        /// <returns></returns>
        public static List<EyeKensa2> LoadByKensaDates(string kensa_id, string start_date, string end_date, bool pat = false, int limit = 0, DB db = null, DB pat_db = null, Action<string> progress = null)
        {
            List<EyeKensa2> tmpList = new List<EyeKensa2>();

            if (kensa_id.Length == 0 || start_date.Length != 8 || end_date.Length != 8)
            {
                return tmpList;
            }

            // 患者マスタ（DBリンク先）とは結合せず、眼科DBだけで検索する。
            string cmd = "select * from EYE_KENSA2 " +
                " where KENSA_ID = " + kensa_id + " and KENSA_DATE >= " + start_date + " and KENSA_DATE <= " + end_date +
                " order by KENSA_DATE desc, PATIENT_ID, KENSA_SEQ";

            if (limit > 0)
            {
                cmd = "select * from (" + cmd + ") where ROWNUM <= " + limit;
            }

            List<StdClass> tmp_list = StdClass.GetList(db == null ? DB.Db2 : db, cmd, null, progress);

            Dictionary<string, PatBase> pat_dict = null;

            if (pat)
            {
                pat_dict = PatBase.GetDict(tmp_list, pat_db, progress);
            }

            foreach (StdClass tmp in tmp_list)
            {
                EyeKensa2 obj = GetFromStdClass(tmp);

                if (pat)
                {
                    // 患者マスタに存在しないIDは除外する
                    if (!pat_dict.ContainsKey(obj.PtId))
                    {
                        continue;
                    }

                    obj._Pat = pat_dict[obj.PtId];
                }

                tmpList.Add(obj);
            }

            return tmpList;
        }

        /// <summary>
        /// データベースからロードする。
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="kensa_id"></param>
        /// <param name="kensa_date"></param>
        /// <param name="kensa_seq"></param>
        /// <returns></returns>
        public static EyeKensa2 LoadByPatKensaDateSEQ(string patient_id, string kensa_id, string kensa_date, string kensa_seq)
        {
            EyeKensa2 tmpKensa = new EyeKensa2();

            if (patient_id.Length == 0 || kensa_id.Length == 0 || kensa_date.Length != 8 || kensa_seq.Length == 0)
            {
                return tmpKensa;
            }

            string cmd = "select * from EYE_KENSA2 " +
                " where PATIENT_ID = " + patient_id + " and KENSA_ID = " + kensa_id + " and KENSA_DATE = " + kensa_date + " and KENSA_SEQ = " + kensa_seq;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                tmpKensa = GetFromStdClass(tmp);
                break;
            }

            return tmpKensa;
        }

        /// <summary>
        /// データベースから削除する。
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="kensa_id"></param>
        /// <param name="kensa_date"></param>
        public static void Delete(string patient_id, string kensa_id, string kensa_date, string kensa_seq)
        {
            if (patient_id.Length == 0 || kensa_id.Length == 0 || kensa_date.Length != 8 || kensa_seq.Length == 0)
            {
                return;
            }

            string cmd = "delete from EYE_KENSA2 " +
                " where PATIENT_ID = " + patient_id + " and KENSA_ID = " + kensa_id + " and KENSA_DATE = " + kensa_date + " and KENSA_SEQ = " + kensa_seq;

            DB.Db2.ExecuteNonQuery(cmd);
        }
    }
}
