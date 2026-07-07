using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class DrugAdvTemplate
    {
        public int SEQ = 0;

        public int NodeSEQ = 0;

        public string Name = "";

        public string Adv = "";

        public int Status = 0;

        public DrugAdvTemplate()
        {
        }

        public DrugAdvTemplate(StdClass tmp)
        {
            this.SEQ = tmp.GetDataInt("TEMP_ID");
            this.NodeSEQ = tmp.GetDataInt("TEMP_NODE");
            this.Name = tmp.GetDataString("TEMP_NAME");
            this.Adv = tmp.GetDataString("ADV");
            this.Status = tmp.GetDataInt("STATUS");
        }

        public static List<DrugAdvTemplate> GetList()
        {
            List<DrugAdvTemplate> list = new List<DrugAdvTemplate>();

            string cmd = "select * from DRUG_ADV_TEMPLATE order by TEMP_NODE, TEMP_ID";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(new DrugAdvTemplate(tmp));
            }

            return list;
        }

        static int NextVal()
        {
            int i = 0;
            string cmd = "select DRUG_ADV_TEMPLATE_SEQ.nextval NV from DUAL";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                i = tmp.GetDataInt("NV");
                break;
            }

            return i;
        }


        public StdReturn Save()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();

            obj.Table = "DRUG_ADV_TEMPLATE";
            obj.Db = DB.Db2;

            obj.DataList.Add(new StdDbColumn("TEMP_NODE", StdDbType.NUMBER, this.NodeSEQ));
            obj.DataList.Add(new StdDbColumn("TEMP_NAME", StdDbType.VARCHAR2, this.Name));
            obj.DataList.Add(new StdDbColumn("ADV", StdDbType.VARCHAR2, this.Adv));
            obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, 1));

            obj.WhereList.Add("TEMP_ID = " + this.SEQ);

            sr = obj.UpdateSQL();

            if (sr.IntValue == 0)
            {
                obj.DataList.Add(new StdDbColumn("TEMP_ID", StdDbType.TEXT, "DRUG_ADV_TEMPLATE_SEQ.nextval"));
                sr = obj.InsertSQL();
            }

            return sr;
        }

        public static StdReturn Delete(int seq)
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();

            obj.Table = "DRUG_ADV_TEMPLATE";
            obj.Db = DB.Db2;

            obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, 0));

            obj.WhereList.Add("TEMP_ID = " + seq);

            sr = obj.UpdateSQL();

            return sr;
        }
    }
}
