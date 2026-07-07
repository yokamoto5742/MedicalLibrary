using System;
using System.Collections.Generic;
using Oracle.DataAccess.Client;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class KarteLog
    {
        public int SEQ = 0;
        public string LogTime = "";
        public string Staff = "";

        public string StaffName
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(this.Staff))
                {
                    s = Dict.StaffDict[this.Staff].Name;
                }

                return s;
            }
        }

        public string PtId = "";
        public string Cont = "";

        public string LogCont
        {
            get
            {
                string s = "";

                if (this.Cont.Length > 500)
                {
                    s = this.Cont.Substring(0, 500);
                }
                else
                {
                    s = this.Cont;
                }

                return s;
            }
        }

        public string PcName = "";

        
        /// <summary>
        /// データベースに保存する。
        /// </summary>
        public StdReturn Save()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();

            obj.Table = "KARTE_LOG";
            obj.Db = DB.Db2;

            obj.DataList.Add(new StdDbColumn("KARTE_LOG_ID", StdDbType.TEXT, "KARTE_LOG_SEQ.nextval"));
            obj.DataList.Add(new StdDbColumn("LOG_TIME", StdDbType.TEXT, "to_date('" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "', 'YYYY/MM/DD HH24:MI:SS')"));

            if (this.PtId.Length > 0)
            {
                obj.DataList.Add(new StdDbColumn("PATIENT_ID", StdDbType.NUMBER, this.PtId));
            }

            obj.DataList.Add(new StdDbColumn("STAFF", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("LOG_CONT", StdDbType.VARCHAR2, this.LogCont));
            obj.DataList.Add(new StdDbColumn("PC_NAME", StdDbType.VARCHAR2, Environment.MachineName));

            sr = obj.InsertSQL();

/*
            OracleCommand Cmd = DB.Db2.Command;

            Cmd.CommandText = "insert into KARTE_LOG " +
                " (KARTE_LOG_ID, LOG_TIME, STAFF, PATIENT_ID, LOG_CONT, PC_NAME) " +
                " values " +
                " (KARTE_LOG_SEQ.nextval, to_date('" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "', 'YYYY/MM/DD HH24:MI:SS'), " + LoginUser.Id + ", :PATIENT_ID, :LOG_CONT, :PC_NAME)";

            Cmd.Parameters.Clear();

            if (this.PtId.Length > 0)
            {
                Cmd.Parameters.Add(":PATIENT_ID", OracleDbType.Decimal).Value = this.PtId;
            }
            else
            {
                Cmd.Parameters.Add(":PATIENT_ID", OracleDbType.Decimal).Value = DBNull.Value;
            }

            Cmd.Parameters.Add(":LOG_CONT", OracleDbType.NVarchar2).Value = this.LogCont;
            Cmd.Parameters.Add(":PC_NAME", OracleDbType.NVarchar2).Value = Environment.MachineName;

            DB.Db2.Open();
            sr.IntValue = Cmd.ExecuteNonQuery();
            DB.Db2.Close();
            Cmd.Parameters.Clear();
*/
            return sr;
        }
    }
}
