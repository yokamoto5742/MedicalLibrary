using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class DiagNan
    {
        public string Code = "";

        public string Name = "";

        public string Cont1 = "";

        public string Status = "";

        static List<DiagNan> _List = new List<DiagNan>();

        public static List<DiagNan> List
        {
            get
            {
                if (_List.Count == 0)
                {
                    Init();
                }

                return _List;
            }
        }

        static void Init()
        {
            _List.Clear();

            string cmd = "select * from DIAG_NAN_MASTER order by CODE";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                _List.Add(GetFromStdClass(tmp));
            }
        }

        static DiagNan GetFromStdClass(StdClass tmp)
        {
            DiagNan obj = new DiagNan();

            obj.Code = tmp.GetDataString("CODE");
            obj.Name = tmp.GetDataString("NAME");
            obj.Cont1 = tmp.GetDataString("CONT1");
            obj.Status = tmp.GetDataString("STATUS");

            return obj;
        }

        public static DiagNan GetData(string code)
        {
            DiagNan obj = new DiagNan();

            if (!AppString.IsNumber(code)) return obj;

            string cmd = "select * from DIAG_NAN_MASTER " +
                " where CODE = " + code;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                obj = GetFromStdClass(tmp);
                break;
            }

            return obj;
        }

        public static List<DiagNan> GetListByName(string name)
        {
            List<DiagNan> list = new List<DiagNan>();

            if (name.Length == 0) return list;

            string cmd = "select * from DIAG_NAN_MASTER " +
                " where NAME like '%" + name + "%'" +
                " order by CODE";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }
    }
}
