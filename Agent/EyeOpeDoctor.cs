using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class EyeOpeDoctor
    {
        public string Id = "";
        public string PreCont = "";
        public string DoCont = "";
        public string Staff = "";
        public string Status = "2";

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

            StdDbClass obj = new StdDbClass();
            obj.Db = DB.Db2;

            obj.Table = "EYE_OPE_DOCTOR";

            obj.DataList.Add(new StdDbColumn("PRE_CONT", StdDbType.VARCHAR2, this.PreCont));
            obj.DataList.Add(new StdDbColumn("DO_CONT", StdDbType.VARCHAR2, this.DoCont));
            obj.DataList.Add(new StdDbColumn("STAFF", StdDbType.NUMBER, this.Staff));
            obj.DataList.Add(new StdDbColumn("SAVE_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("SAVE_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, this.Status));

            obj.UpdateOrInsert(new List<StdDbColumn>
            {
                new StdDbColumn("ID", StdDbType.NUMBER, this.Id)
            });
        }

        /// <summary>
        /// データベースからロードする。
        /// </summary>
        public static EyeOpeDoctor Load(string ope_id)
        {
            EyeOpeDoctor tmpOpe = new EyeOpeDoctor();

            if (ope_id.Length == 0)
            {
                return tmpOpe;
            }

            string cmd = "select * from EYE_OPE_DOCTOR where ID = " + ope_id;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                tmpOpe.Id = tmp.DataDict["ID"].ToString();
                tmpOpe.PreCont = tmp.DataDict["PRE_CONT"].ToString();
                tmpOpe.DoCont = tmp.DataDict["DO_CONT"].ToString();
                tmpOpe.Staff = tmp.DataDict["STAFF"].ToString();
                tmpOpe.Status = tmp.DataDict["STATUS"].ToString();
            }

            return tmpOpe;
        }
    }
}
