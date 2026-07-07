using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class EyeIV
    {
        public string Id = "";
        public string PtId = "";
        public string IVDate = "";
        public string Cont = "";
        public string Staff = "";
        public string SaveDate = "";
        public string SaveTime = "";
        public string Status = "1";
        public string PDFSave = "";

        /// <summary>
        /// データベースに保存する。
        /// </summary>
        public void Save()
        {

            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
            obj.Db = DB.Db2;

            obj.Table = "EYE_INTERVIEW";

            obj.DataList.Add(new StdDbColumn("IV_DATE", StdDbType.NUMBER, this.IVDate));
            obj.DataList.Add(new StdDbColumn("CONT", StdDbType.VARCHAR2, this.Cont));
            obj.DataList.Add(new StdDbColumn("STAFF", StdDbType.NUMBER, this.Staff));
            obj.DataList.Add(new StdDbColumn("SAVE_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("SAVE_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));

            if (this.Id.Length > 0)
            {
                obj.WhereList.Add("ID = " + this.Id);
                sr = obj.UpdateSQL();
            }
            else
            {
                obj.DataList.Add(new StdDbColumn("ID", StdDbType.TEXT, "EYE_INTERVIEW_SEQ.nextval"));
                obj.DataList.Add(new StdDbColumn("PATIENT_ID", StdDbType.NUMBER, this.PtId));
                obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, "1"));
                obj.DataList.Add(new StdDbColumn("PDF_SAVE", StdDbType.NUMBER, "0"));
                sr = obj.InsertSQL();
            }
        }

        /// <summary>
        /// データベースからロードする。
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="start_date"></param>
        /// <returns></returns>
        public static List<EyeIV> Load(string pt_id, string start_date)
        {
            List<EyeIV> tmpList = new List<EyeIV>();

            if (pt_id.Length == 0)
            {
                return tmpList;
            }

            string cmd = "select * from EYE_INTERVIEW " +
                " where PATIENT_ID = " + pt_id + " and IV_DATE >= " + start_date +
                " order by IV_DATE desc, ID desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                EyeIV tmpIV = new EyeIV();

                tmpIV.Id = tmp.DataDict["ID"].ToString();
                tmpIV.PtId = tmp.DataDict["PATIENT_ID"].ToString();
                tmpIV.Cont = tmp.DataDict["CONT"].ToString();
                tmpIV.IVDate = tmp.DataDict["IV_DATE"].ToString();
                tmpIV.Staff = tmp.DataDict["STAFF"].ToString();
                tmpIV.SaveDate = tmp.DataDict["SAVE_DATE"].ToString();
                tmpIV.SaveTime = tmp.DataDict["SAVE_TIME"].ToString();
                tmpIV.Status = tmp.DataDict["STATUS"].ToString();
                tmpIV.PDFSave = tmp.DataDict["PDF_SAVE"].ToString();

                tmpList.Add(tmpIV);
            }

            return tmpList;
        }

        /// <summary>
        /// データベースからロードする。
        /// </summary>
        /// <param name="pt_id"></param>
        /// <returns></returns>
        public static List<EyeIV> Load(string pt_id)
        {
            return Load(pt_id, "20090101");
        }

        /// <summary>
        /// データベースから削除する。
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(string id)
        {
            if (id.Length == 0)
            {
                return;
            }

            DB.Db2.ExecuteNonQuery("update EYE_INTERVIEW set STATUS = 0 where ID = " + id);
        }
    }
}
