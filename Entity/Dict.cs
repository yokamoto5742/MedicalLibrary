using System;
using System.Collections.Generic;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class Dict
    {
        static Dictionary<string, Dept> deptDict;

        public static Dictionary<string, Dept> DeptDict
        {
            get
            {
                if (deptDict == null || deptDict.Count == 0)
                {
                    InitDict();
                }

                return deptDict;
            }
        }

        static Dictionary<string, Doctor> doctorDict;

        public static Dictionary<string, Doctor> DoctorDict
        {
            get
            {
                if (doctorDict == null || doctorDict.Count == 0)
                {
                    InitDict();
                }

                return doctorDict;
            }
        }

        static Dictionary<string, Staff> staffDict;

        public static Dictionary<string, Staff> StaffDict
        {
            get
            {
                if (staffDict == null || staffDict.Count == 0)
                {
                    InitDict();
                }

                return staffDict;
            }
        }

        static Dictionary<string, string> holidayDict;

        public static Dictionary<string, string> HolidayDict
        {
            get
            {
                if (holidayDict == null || holidayDict.Count == 0)
                {
                    InitDict2();
                }

                return holidayDict;
            }
        }

        private static void InitDict()
        {
            deptDict = new Dictionary<string, Dept>();
            doctorDict = new Dictionary<string, Doctor>();
            staffDict = new Dictionary<string, Staff>();

            // 接続の Open/Close は GetList ごとに行う（外側で Open すると入れ子になり接続が閉じない）
            DB db = DB.Db3;

            // 診療科
            string cmd = "select CODE, NAME, S_NAME, CATEGORY from M_DEPT order by CODE";

            List<StdClass> tmp_list = StdClass.GetList(db, cmd);

            deptDict.Add("0", new Dept());

            foreach (StdClass tmp in tmp_list)
            {
                Dept obj = new Dept();

                int.TryParse(tmp.DataDict["CODE"].ToString(), out obj.Code);
                obj.FullName = tmp.DataDict["NAME"].ToString().Trim();
                obj.ShortName = tmp.DataDict["S_NAME"].ToString().Trim();
                int.TryParse(tmp.DataDict["CATEGORY"].ToString(), out obj.Status);

                deptDict.Add(obj.Code.ToString(), obj);
            }

            // 医師
            cmd = "select td.CODE, td.NAME, td.S_NAME, td.CATEGORY, td.VAL_4, tu.CODE U_CODE, tu.NAME U_NAME " +
                " from M_DR td, M_USR tu where td.CODE = tu.DR and tu.SYOZOKU = 1 order by td.CODE";

            tmp_list = StdClass.GetList(db, cmd);

            doctorDict.Add("0", new Doctor());

            foreach (StdClass tmp in tmp_list)
            {
                Doctor obj = new Doctor();

                int.TryParse(tmp.DataDict["CODE"].ToString(), out obj.Code);
                obj.Name = tmp.GetDataString("NAME").Trim();
                obj.ShortName = tmp.GetDataString("S_NAME").Trim();
                int.TryParse(tmp.DataDict["CATEGORY"].ToString(), out obj.Status);
                int.TryParse(tmp.DataDict["VAL_4"].ToString(), out obj.DeptCode);
                int.TryParse(tmp.DataDict["U_CODE"].ToString(), out obj.StaffCode);
                obj.StaffName = tmp.DataDict["U_NAME"].ToString().Trim();

                doctorDict.Add(obj.Code.ToString(), obj);
            }

            // 入力者
            cmd = "select t.CODE, t.NAME, t.KANA " +
                ", case when t.DEL_FLG = 1 then 9 else 1 end STATUS " +
                ", t.SYOZOKU, t.SHIKAKU, t.DEPT, t.DR " +
                " from M_USR t " +
                " order by CODE";

            tmp_list = StdClass.GetList(db, cmd);

            staffDict.Add("0", new Staff());

            foreach (StdClass tmp in tmp_list)
            {
                Staff obj = new Staff();

                int.TryParse(tmp.DataDict["CODE"].ToString(), out obj.Code);
                obj.Name = tmp.DataDict["NAME"].ToString().Trim();
                obj.Kana = tmp.DataDict["KANA"].ToString().Trim();
                int.TryParse(tmp.DataDict["STATUS"].ToString(), out obj.Status);
                int.TryParse(tmp.DataDict["SYOZOKU"].ToString(), out obj.SectionCode);
                int.TryParse(tmp.DataDict["SHIKAKU"].ToString(), out obj.QualCode);
                int.TryParse(tmp.DataDict["DEPT"].ToString(), out obj.DeptCode);
                int.TryParse(tmp.DataDict["DR"].ToString(), out obj.DoctorCode);

                staffDict.Add(obj.Code.ToString(), obj);
            }
        }

        /// <summary>
        /// マスター辞書作成（休日）
        /// </summary>
        private static void InitDict2()
        {
            holidayDict = new Dictionary<string, string>();
        }
    }

    public class Dept
    {
        public int Code;
        public string FullName;
        public string ShortName;
        public int Status;

        public Dept()
        {
            Code = 0;
            FullName = "";
            ShortName = "";
            Status = 0;
        }

        public Dept(int code, string fullName, string shortName, int status = 0)
        {
            Code = code;
            FullName = fullName;
            ShortName = shortName;
            Status = status;
        }

        public override string ToString()
        {
            return this.FullName;
        }
    }

    public class Doctor
    {
        public int Code = 0;
        public string Name = "";
        public string ShortName = "";
        public int Status = 0;
        public int DeptCode = 0;
        public int StaffCode = 0;
        public string StaffName = "";

        public Doctor()
        {
            Code = 0;
            Name = "";
            ShortName = "";
            Status = 0;
            DeptCode = 0;
            StaffCode = 0;
            StaffName = "";
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
