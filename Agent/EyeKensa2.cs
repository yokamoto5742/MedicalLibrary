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
        /// データベースからロードする。（履歴）
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="kensa_id"></param>
        /// <returns></returns>
        public static List<EyeKensa2> LoadByPatKensa(string patient_id, string kensa_id)
        {
            List<EyeKensa2> tmpList = new List<EyeKensa2>();

            if (patient_id.Length == 0 || kensa_id.Length == 0)
            {
                return tmpList;
            }

            string cmd = "select * from EYE_KENSA2 " +
                " where PATIENT_ID = " + patient_id + " and KENSA_ID = " + kensa_id +
                " order by KENSA_DATE desc";

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
