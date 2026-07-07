using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class DPCOpeMaster
    {
        public int SEQ = 0;

        public string KCode = "";

        public string Name = "";

        public string STEM7 = "";

        public string Cont = "";


        static DPCOpeMaster GetFromStdClass(StdClass tmp)
        {
            DPCOpeMaster obj = new DPCOpeMaster();

            obj.SEQ = tmp.GetDataInt("SEQ");
            obj.KCode = tmp.GetDataString("KCODE");
            obj.Name = tmp.GetDataString("NAME");
            obj.STEM7 = tmp.GetDataString("STEM7");
            obj.Cont = tmp.GetDataString("CONT");

            return obj;
        }


        public static List<DPCOpeMaster> GetListByName(string name)
        {
            List<DPCOpeMaster> list = new List<DPCOpeMaster>();

            if (name.Length == 0) return list;

            string cmd = "select * from DPC_OPE_MASTER " +
                " where NAME like '%" + name + "%'" +
                " order by SEQ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }


        public static List<DPCOpeMaster> GetListByKCode(string kcode)
        {
            List<DPCOpeMaster> list = new List<DPCOpeMaster>();

            if (kcode.Length == 0) return list;

            string cmd = "select * from DPC_OPE_MASTER " +
                " where KCODE like '%" + kcode + "%'" +
                " or to_multi_byte(upper(to_char(replace(KCODE, ' ', '')))) like '%" + AppString.HanToZen(kcode) + "%'" +
                " order by SEQ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }


        public static List<DPCOpeMaster> GetListBySTEM7(string stem7)
        {
            List<DPCOpeMaster> list = new List<DPCOpeMaster>();

            if (stem7.Length == 0) return list;

            string cmd = "select * from DPC_OPE_MASTER " +
                " where STEM7 like '%" + stem7 + "%'" +
                " or replace(STEM7, ' ', '') like '%" + stem7 + "%'" +
                " order by SEQ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }
    }
}
