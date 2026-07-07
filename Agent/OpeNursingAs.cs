using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class OpeNursingAs : StdEntity
    {
        new public static DB Db = DB.Db2;

        public int OpeNursingId = 0;

        public int Id = 0;

        public string Title = "";

        public string Text = "";

        static OpeNursingAs GetFromStdClass(StdClass tmp)
        {
            OpeNursingAs obj = new OpeNursingAs();

            int.TryParse(tmp.DataDict["OPE_NURSING_ID"].ToString(), out obj.OpeNursingId);
            int.TryParse(tmp.DataDict["AS_ID"].ToString(), out obj.Id);
            obj.Title = tmp.DataDict["AS_TITLE"].ToString();
            obj.Text = tmp.DataDict["AS_TEXT"].ToString();

            return obj;
        }

        public static Dictionary<string, OpeNursingAs> GetDict(int ope_nursing_id)
        {
            Dictionary<string, OpeNursingAs> dict = new Dictionary<string, OpeNursingAs>();

            string cmd = "select * from OPE_NURSING_AS " +
                " where OPE_NURSING_ID = " + ope_nursing_id +
                " order by AS_ID";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                OpeNursingAs p = GetFromStdClass(tmp);

                if (!dict.ContainsKey(p.Id.ToString()))
                {
                    dict.Add(p.Id.ToString(), p);
                }
            }

            return dict;
        }
    }
}
