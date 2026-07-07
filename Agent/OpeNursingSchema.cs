using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class OpeNursingSchema : StdEntity
    {
        new public static DB Db = DB.Db2;

        public int OpeNursingId = 0;

        public int Id = 0;

        public int Bg = -1;

        public string Item = "";

        static OpeNursingSchema GetFromStdClass(StdClass tmp)
        {
            OpeNursingSchema obj = new OpeNursingSchema();

            int.TryParse(tmp.DataDict["OPE_NURSING_ID"].ToString(), out obj.OpeNursingId);
            int.TryParse(tmp.DataDict["SCHEMA_ID"].ToString(), out obj.Id);
            int.TryParse(tmp.DataDict["SCHEMA_BG"].ToString(), out obj.Bg);
            obj.Item = tmp.DataDict["SCHEMA_ITEM"].ToString();

            return obj;
        }

        public static Dictionary<string, OpeNursingSchema> GetDict(int ope_nursing_id)
        {
            Dictionary<string, OpeNursingSchema> dict = new Dictionary<string, OpeNursingSchema>();

            string cmd = "select * from OPE_NURSING_SCHEMA " +
                " where OPE_NURSING_ID = " + ope_nursing_id +
                " order by SCHEMA_ID";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                OpeNursingSchema p = GetFromStdClass(tmp);

                if (!dict.ContainsKey(p.Id.ToString()))
                {
                    dict.Add(p.Id.ToString(), p);
                }
            }

            return dict;
        }
    }
}
