using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class DPCItem1
    {
        public string Kind = "";

        public string Code = "";

        public string Name = "";

        public string Box = "";

        public List<DPCSubItem1> SubItemList = new List<DPCSubItem1>();


        static List<DPCItem1> list = new List<DPCItem1>();

        public static List<DPCItem1> List
        {
            get
            {
                if (list.Count == 0)
                {
                    Init();
                }

                return list;
            }
        }

        public static DPCItem1 GetData(string code)
        {
            DPCItem1 obj = new DPCItem1();

            foreach (DPCItem1 obj2 in List)
            {
                if (obj2.Code.Equals(code))
                {
                    obj = obj2;
                    break;
                }
            }

            return obj;
        }

        public DPCSubItem1 GetSubItem(string code)
        {
            DPCSubItem1 obj = new DPCSubItem1();

            foreach (DPCSubItem1 obj2 in this.SubItemList)
            {
                if (obj2.Code.Equals(code))
                {
                    obj = obj2;
                    break;
                }
            }

            return obj;
        }



        /// <summary>
        /// XMLファイルを読み込んでアイテムリストを作成する
        /// </summary>
        public static void Init()
        {
            string xml_file = AppFile.FilePath("DPCItem1.xml");

            if (!File.Exists(xml_file))
            {
                return;
            }

            StreamReader reader = new StreamReader(xml_file, Encoding.GetEncoding("shift-jis"));

            DataSet dSet = new DataSet();
            dSet.ReadXml(reader);

            DataTable table1 = dSet.Tables["DPCItem1"];
            DataTable table2 = dSet.Tables["Values"];

            foreach (DataRow r1 in table1.Rows)
            {
                DPCItem1 item = new DPCItem1();

                item.Kind = r1["Kind"].ToString();
                item.Code = r1["Code"].ToString();
                item.Name = r1["Name"].ToString();
                item.Box = r1["Box"].ToString();

                foreach (DataRow r2 in table2.Rows)
                {
                    if (r2["DPCItem1_id"].ToString().Equals(r1["DPCItem1_id"].ToString()))
                    {
                        DPCSubItem1 sub1 = new DPCSubItem1();
                        sub1.Code = r2["Code"].ToString();
                        sub1.Text = r2["Text"].ToString();

                        item.SubItemList.Add(sub1);
                    }
                }

                list.Add(item);
            }

            reader.Close();
        }
    }

    public class DPCSubItem1
    {
        public string Code = "";
        public string Text = "";
    }
}
