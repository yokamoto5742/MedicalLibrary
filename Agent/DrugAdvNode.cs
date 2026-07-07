using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class DrugAdvNode
    {
        public int SEQ = 0;

        public string Name = "";

        public int Status = 0;

        public DrugAdvNode()
        {
        }

        public DrugAdvNode(StdClass tmp)
        {
            this.SEQ = tmp.GetDataInt("NODE_ID");
            this.Name = tmp.GetDataString("NODE_NAME");
            this.Status = tmp.GetDataInt("STATUS");
        }

        public static List<DrugAdvNode> GetList()
        {
            List<DrugAdvNode> list = new List<DrugAdvNode>();

            string cmd = "select * from DRUG_ADV_NODE order by NODE_ID";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(new DrugAdvNode(tmp));
            }

            return list;
        }
/*
        static int NextVal()
        {
            int i = 0;
            string cmd = "select DRUG_ADV_NODE_SEQ.nextval NV from DUAL";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                i = tmp.GetDataInt("NV");
                break;
            }

            return i;
        }
*/

        public StdReturn Save()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();

            obj.Table = "DRUG_ADV_NODE";
            obj.Db = DB.Db2;

            obj.DataList.Add(new StdDbColumn("NODE_NAME", StdDbType.VARCHAR2, this.Name));
            obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, 1));

            obj.WhereList.Add("NODE_ID = " + this.SEQ);

            sr = obj.UpdateSQL();

            if (sr.IntValue == 0)
            {
                obj.DataList.Add(new StdDbColumn("NODE_ID", StdDbType.TEXT, "DRUG_ADV_NODE_SEQ.nextval"));
                sr = obj.InsertSQL();
            }

            return sr;
        }

        public static StdReturn Delete(int seq)
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();

            obj.Table = "DRUG_ADV_NODE";
            obj.Db = DB.Db2;

            obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, 0));

            obj.WhereList.Add("NODE_ID = " + seq);

            sr = obj.UpdateSQL();

            return sr;
        }
    }
}
