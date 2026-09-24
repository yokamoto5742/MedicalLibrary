using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class EyeOpePass
    {
        public string Id = "";
        public string Cont = "";
        public string Staff = "";
        public string Status = "1";

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

            obj.Table = "EYE_OPE_PASS";

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

        /// <summary>
        /// データベースからロードする。
        /// </summary>
        public static EyeOpePass Load(string ope_id)
        {
            EyeOpePass tmpOpe = new EyeOpePass();

            if (ope_id.Length == 0)
            {
                return tmpOpe;
            }

            string cmd = "select * from EYE_OPE_PASS where ID = " + ope_id;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                tmpOpe.Id = tmp.DataDict["ID"].ToString();
                tmpOpe.Cont = tmp.DataDict["CONT"].ToString();
                tmpOpe.Staff = tmp.DataDict["STAFF"].ToString();
                tmpOpe.Status = tmp.DataDict["STATUS"].ToString();
            }

            return tmpOpe;
        }
    }
}
