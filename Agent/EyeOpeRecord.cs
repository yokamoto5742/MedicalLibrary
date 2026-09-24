using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class EyeOpeRecord
    {
        public string Id = "";
        public string Cont = "";
        public string Staff = "";
        public string Status = "2";

        public string SaveDate = "";
        public string SaveTime = "";

        public string PDFSave = "";
        public string PDFDate = "";
        public string PDFTime = "";

        /// <summary>
        /// EYE_OPE と結合する場合
        /// </summary>
        public EyeOpe Ope = new EyeOpe();

        /// <summary>
        /// 適切な値が入っているかをチェックする。
        /// </summary>
        private bool DataCheck()
        {
            if (Id.Length == 0) return false;

            return true;
        }

        /// <summary>
        /// データベースに保存する。
        /// </summary>
        public void Save()
        {
            if (!DataCheck()) return;

            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
            obj.Db = DB.Db2;

            obj.Table = "EYE_OPE_RECORD";

            obj.DataList.Add(new StdDbColumn("CONT", StdDbType.VARCHAR2, this.Cont));
            obj.DataList.Add(new StdDbColumn("STAFF", StdDbType.NUMBER, this.Staff));
            obj.DataList.Add(new StdDbColumn("SAVE_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("SAVE_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, this.Status));

            obj.WhereList.Add("ID = " + this.Id);

            sr = obj.UpdateSQL();

            // update 対象が無ければ新規登録
            if (sr.IntValue == 0)
            {
                obj.DataList.Add(new StdDbColumn("ID", StdDbType.NUMBER, this.Id));
                sr = obj.InsertSQL();
            }
        }

        static EyeOpeRecord GetFromStdClass(StdClass tmp)
        {
            EyeOpeRecord obj = new EyeOpeRecord();

            obj.Id = tmp.GetDataString("ID");
            obj.Cont = tmp.GetDataString("CONT");
            obj.Staff = tmp.GetDataString("STAFF");
            obj.Status = tmp.GetDataString("STATUS");

            obj.SaveDate = tmp.GetDataString("SAVE_DATE");
            obj.SaveTime = tmp.GetDataString("SAVE_TIME");
            obj.PDFSave = tmp.GetDataString("PDF_SAVE");
            obj.PDFDate = tmp.GetDataString("PDF_DATE");
            obj.PDFTime = tmp.GetDataString("PDF_TIME");

            return obj;
        }

        /// <summary>
        /// データベースからロードする。
        /// </summary>
        public static EyeOpeRecord Load(string ope_id)
        {
            EyeOpeRecord obj = new EyeOpeRecord();

            if (ope_id.Length == 0)
            {
                return obj;
            }

            string cmd = "select * from EYE_OPE_RECORD where ID = " + ope_id;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                obj = GetFromStdClass(tmp);
                break;
            }

            return obj;
        }

    }
}
