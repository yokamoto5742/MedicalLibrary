using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Oracle.ManagedDataAccess.Client;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class Dict
    {
        static Dictionary<string, string> inOutDict;

        static Dictionary<string, Ward> wardDict;

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

        static Dictionary<string, string> soapDict;

        static Dictionary<string, string> soapShortDict;

        private static void InitDict()
        {
            inOutDict = new Dictionary<string, string>();

            inOutDict.Add("0", "");
            inOutDict.Add("1", "外来");
            inOutDict.Add("2", "入院");

            wardDict = new Dictionary<string, Ward>();
            Ward ward = new Ward();
            ward.SEQ = 0;
            ward.Code = "0";
            ward.Name = "";
            ward.Short = "";
            ward.BackColor = Color.White;
            wardDict.Add(ward.Code, ward);

            ward = new Ward();
            ward.SEQ = 3;
            ward.Code = "3";
            ward.Name = "わかば";
            ward.Short = "若";
            ward.DeptCode = "31";
            ward.BackColor = Color.LightGreen;
            wardDict.Add(ward.Code, ward);

            ward = new Ward();
            ward.SEQ = 4;
            ward.Code = "4";
            ward.Name = "さくら";
            ward.Short = "桜";
            ward.DeptCode = "41";
            ward.BackColor = Color.Pink;
            wardDict.Add(ward.Code, ward);

            ward = new Ward();
            ward.SEQ = 5;
            ward.Code = "5";
            ward.Name = "あやめ";
            ward.Short = "菖";
            ward.DeptCode = "33";
            ward.BackColor = Color.Lavender;
            wardDict.Add(ward.Code, ward);

            ward = new Ward();
            ward.SEQ = 99;
            ward.Code = "99";
            ward.Name = "未定";
            ward.Short = "未";
            ward.BackColor = Color.White;
            wardDict.Add(ward.Code, ward);

            soapDict = new Dictionary<string, string>();

            soapDict.Add("1", "S");
            soapDict.Add("2", "O");
            soapDict.Add("3", "A");
            soapDict.Add("4", "P");
            soapDict.Add("5", "F");
            soapDict.Add("6", "I");
            soapDict.Add("7", "E");
            soapDict.Add("9", "ｻﾏﾘ");

            soapShortDict = new Dictionary<string, string>();

            soapShortDict.Add("1", "S");
            soapShortDict.Add("2", "O");
            soapShortDict.Add("3", "A");
            soapShortDict.Add("4", "P");
            soapShortDict.Add("5", "F");
            soapShortDict.Add("6", "I");
            soapShortDict.Add("7", "E");
            soapShortDict.Add("9", "ｻ");

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

    public class Ward
    {
        public int SEQ = 0;
        public string Code = "";

        public string Name = "";
        public string Short = "";
        public string DeptCode = "";
        public Color BackColor = Color.White;
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
