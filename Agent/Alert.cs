using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using MedicalLibrary.Utility;
using MedicalLibrary.Entity;

namespace MedicalLibrary.Agent
{
    public class Alert
    {
        public string PtId = "";

        PatBase _Pat = new PatBase();

        public PatBase Pat
        {
            get
            {
                if (!this._Pat.Id.Equals(this.PtId) || this._Pat.Name.Length == 0)
                {
                    this._Pat = PatBase.Load(this.PtId);
                }

                return this._Pat;
            }
        }

        public string Cont = "";

        public string Status = "";

        public string SaveDate = "";

        public string SaveTime = "";

        public string Staff = "";

        public string StaffName
        {
            get
            {
                return Dict.StaffDict.ContainsKey(this.Staff) ? Dict.StaffDict[this.Staff].Name : "";
            }
        }

        static Alert GetFromStdClass(StdClass tmp)
        {
            Alert obj = new Alert();

            obj.PtId = tmp.GetDataString("PATIENT_ID");
            obj.Cont = tmp.GetDataString("CONT");
            obj.Staff = tmp.GetDataString("STAFF");
            obj.SaveDate = tmp.GetDataString("SAVE_DATE");
            obj.SaveTime = tmp.GetDataString("SAVE_TIME");
            obj.Status = tmp.GetDataString("STATUS");

            return obj;
        }

        public static Alert GetData(string pt_id)
        {
            Alert obj = new Alert();

            if (!AppString.IsNumber(pt_id)) return obj;

            string cmd = "select * from D_ALERT " +
                " where PATIENT_ID = " + pt_id;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                obj = GetFromStdClass(tmp);
                break;
            }

            return obj;
        }


        /// <summary>
        /// 保存する
        /// </summary>
        /// <returns></returns>
        public StdReturn Save()
        {
            StdReturn sr = new StdReturn();

            if (DB.Db2.Connection.State != ConnectionState.Open) DB.Db2.Open();

            using (DbTransaction tran = DB.Db2.Connection.BeginTransaction())
            {
                try
                {
                    StdDbClass obj = new StdDbClass();
                    obj.Db = DB.Db2;

                    obj.Table = "D_ALERT";

                    obj.DataList.Add(new StdDbColumn("CONT", StdDbType.VARCHAR2, this.Cont));
                    obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, this.Status));
                    obj.DataList.Add(new StdDbColumn("STAFF", StdDbType.NUMBER, LoginUser.Id));
                    obj.DataList.Add(new StdDbColumn("SAVE_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
                    obj.DataList.Add(new StdDbColumn("SAVE_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));

                    obj.WhereList.Add("PATIENT_ID = " + this.PtId);

                    sr = obj.UpdateSQL();

                    if (sr.IntValue == 0)
                    {
                        obj.DataList.Add(new StdDbColumn("PATIENT_ID", StdDbType.NUMBER, this.PtId));

                        sr = obj.InsertSQL();
                    }

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
}
