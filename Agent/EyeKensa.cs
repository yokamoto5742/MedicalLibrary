using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class EyeKensa : EyeKensaBase
    {
        /// <summary>
        /// データベースに保存する。
        /// </summary>
        public void Save()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
            obj.Db = DB.Db2;

            obj.Table = "EYE_KENSA";

            obj.DataList.Add(new StdDbColumn("CONT", StdDbType.VARCHAR2, this.Cont));
            obj.DataList.Add(new StdDbColumn("STAFF", StdDbType.NUMBER, this.Staff));
            obj.DataList.Add(new StdDbColumn("SAVE_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("SAVE_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));

            obj.WhereList.Add("PATIENT_ID = " + this.PtId);
            obj.WhereList.Add("KENSA_ID = " + this.KensaId);
            obj.WhereList.Add("KENSA_DATE = " + this.KensaDate);

            sr = obj.UpdateSQL();

            // update 対象が無ければ新規登録
            if (sr.IntValue == 0)
            {
                obj.DataList.Add(new StdDbColumn("PATIENT_ID", StdDbType.NUMBER, this.PtId));
                obj.DataList.Add(new StdDbColumn("KENSA_ID", StdDbType.NUMBER, this.KensaId));
                obj.DataList.Add(new StdDbColumn("KENSA_DATE", StdDbType.NUMBER, this.KensaDate));
                sr = obj.InsertSQL();
            }
        }

        static EyeKensa GetFromStdClass(StdClass tmp)
        {
            EyeKensa obj = new EyeKensa();

            obj.PtId = tmp.DataDict["PATIENT_ID"].ToString();
            obj.KensaDate = tmp.DataDict["KENSA_DATE"].ToString();
            obj.KensaId = tmp.DataDict["KENSA_ID"].ToString();
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
        public static List<EyeKensa> LoadByPatient(string patient_id, string start_date = "")
        {
            string start_date_2 = "";

            List<EyeKensa> tmpList = new List<EyeKensa>();

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

            string cmd = "select * from EYE_KENSA " +
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
        public static List<EyeKensa> LoadByPatDate(string patient_id, string kensa_date)
        {
            List<EyeKensa> tmpList = new List<EyeKensa>();

            if (patient_id.Length == 0 || kensa_date.Length != 8)
            {
                return tmpList;
            }

            string cmd = "select * from EYE_KENSA " +
                " where PATIENT_ID = " + patient_id + " and KENSA_DATE = " + kensa_date;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                tmpList.Add(GetFromStdClass(tmp));
            }

            return tmpList;
        }

        /// <summary>
        /// データベースからロードする。（履歴）
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="kensa_id"></param>
        /// <param name="pat"></param>
        /// <returns></returns>
        public static List<EyeKensa> LoadByPatKensa(string patient_id, string kensa_id, bool pat = false)
        {
            List<EyeKensa> tmpList = new List<EyeKensa>();

            if (patient_id.Length == 0 || kensa_id.Length == 0)
            {
                return tmpList;
            }

            string cmd = "";

            if (pat)
            {
                // 患者マスタ（DBリンク先）とは結合せず、眼科DB単独で検索する。
                // 患者情報は検索結果の患者IDからまとめて取得する（DBリンク越しの結合による負荷・ハング対策）。
                cmd = "select * from EYE_KENSA " +
                    " where PATIENT_ID = " + patient_id + " and KENSA_ID = " + kensa_id +
                    " order by KENSA_DATE desc";
            }
            else
            {
                cmd = "select * from EYE_KENSA " +
                    " where PATIENT_ID = " + patient_id + " and KENSA_ID = " + kensa_id +
                    " order by KENSA_DATE desc";
            }

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            Dictionary<string, PatBase> pat_dict = null;

            if (pat)
            {
                pat_dict = PatBase.GetDict(tmp_list);
            }

            foreach (StdClass tmp in tmp_list)
            {
                EyeKensa obj = GetFromStdClass(tmp);

                if (pat)
                {
                    // 患者マスタに存在しないIDは除外する（従来の inner join と同一挙動）
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
        /// データベースからロードする。（履歴）
        /// </summary>
        /// <param name="kensa_id_list"></param>
        /// <param name="start_date"></param>
        /// <param name="end_date"></param>
        /// <param name="pat"></param>
        /// <param name="limit">取得件数の上限（0は無制限）</param>
        /// <param name="db">眼科DBへの接続（省略時は DB.Db2）</param>
        /// <param name="pat_db">患者マスタDBへの接続（省略時は DB.Db3）</param>
        /// <returns></returns>
        public static List<EyeKensa> LoadByKensasDates(List<string> kensa_id_list, string start_date, string end_date, bool pat = false, int limit = 0, DB db = null, DB pat_db = null)
        {
            List<EyeKensa> tmpList = new List<EyeKensa>();

            if (kensa_id_list.Count == 0 || start_date.Length != 8 || end_date.Length != 8)
            {
                return tmpList;
            }

            // 患者マスタ（DBリンク先）とは結合せず、眼科DBだけで検索する。
            // 患者情報は検索結果の患者IDからまとめて取得する（DBリンク越しの結合による負荷・ハング対策）。
            string cmd = "select * from EYE_KENSA " +
                " where KENSA_ID in (" + AppString.ConcatList(kensa_id_list, ",") + ") and KENSA_DATE >= " + start_date + " and KENSA_DATE <= " + end_date +
                " order by KENSA_DATE desc";

            if (limit > 0)
            {
                cmd = "select * from (" + cmd + ") where ROWNUM <= " + limit;
            }

            List<StdClass> tmp_list = StdClass.GetList(db == null ? DB.Db2 : db, cmd);

            Dictionary<string, PatBase> pat_dict = null;

            if (pat)
            {
                pat_dict = PatBase.GetDict(tmp_list, pat_db);
            }

            foreach (StdClass tmp in tmp_list)
            {
                EyeKensa obj = GetFromStdClass(tmp);

                if (pat)
                {
                    // 患者マスタに存在しないIDは除外する（従来の inner join と同じ扱い）
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
        /// データベースからロードする。（履歴）
        /// </summary>
        /// <param name="pt_list"></param>
        /// <param name="kensa_id_list"></param>
        /// <returns></returns>
        public static List<EyeKensa> LoadByPatsKensas(List<string> pt_list, List<string> kensa_id_list)
        {
            List<EyeKensa> tmpList = new List<EyeKensa>();

            if (pt_list.Count == 0 || kensa_id_list.Count == 0)
            {
                return tmpList;
            }

            string cmd = "select * from EYE_KENSA " +
                " where PATIENT_ID in (" + AppString.ConcatList(pt_list, ",") + ") and KENSA_ID in (" + AppString.ConcatList(kensa_id_list, ",") + ") " +
                " order by PATIENT_ID, KENSA_DATE desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                EyeKensa obj = GetFromStdClass(tmp);

                tmpList.Add(obj);
            }

            return tmpList;
        }

        /// <summary>
        /// データベースから削除する。
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="kensa_id"></param>
        /// <param name="kensa_date"></param>
        public static void Delete(string patient_id, string kensa_id, string kensa_date)
        {
            if (patient_id.Length == 0 || kensa_id.Length == 0 || kensa_date.Length != 8)
            {
                return;
            }

            string cmd = "delete from EYE_KENSA " +
                " where PATIENT_ID = " + patient_id + " and KENSA_ID = " + kensa_id + " and KENSA_DATE = " + kensa_date;

            DB.Db2.ExecuteNonQuery(cmd);
        }
    }
}
