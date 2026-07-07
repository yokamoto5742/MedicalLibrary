using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Oracle.DataAccess.Client;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class Dict
    {
        static Dictionary<string, string> inOutDict;

        public static Dictionary<string, string> InOutDict
        {
            get
            {
                if (inOutDict == null || inOutDict.Count == 0)
                {
                    InitDict();
                }

                return inOutDict;
            }
        }

        static Dictionary<string, Ward> wardDict;

        public static Dictionary<string, Ward> WardDict
        {
            get
            {
                if (wardDict == null || wardDict.Count == 0)
                {
                    InitDict();
                }

                return wardDict;
            }
        }
/*
        static Dictionary<string, string> wardShortDict;

        public static Dictionary<string, string> WardShortDict
        {
            get
            {
                if (wardShortDict == null || wardShortDict.Count == 0)
                {
                    InitDict();
                }

                return wardShortDict;
            }
        }
 */

        static Dictionary<string, string> kouiDict;

        public static Dictionary<string, string> KouiDict
        {
            get
            {
                if (kouiDict == null || kouiDict.Count == 0)
                {
                    InitDict();
                }

                return kouiDict;
            }
        }

        static Dictionary<string, Sekou> sekouDict;

        public static Dictionary<string, Sekou> SekouDict
        {
            get
            {
                if (sekouDict == null || sekouDict.Count == 0)
                {
                    InitDict();
                }

                return sekouDict;
            }
        }

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

        /// <summary>
        /// èäëÆ
        /// </summary>
        static Dictionary<string, Section> sectionDict;

        /// <summary>
        /// èäëÆ
        /// </summary>
        public static Dictionary<string, Section> SectionDict
        {
            get
            {
                if (sectionDict == null || sectionDict.Count == 0)
                {
                    InitDict();
                }

                return sectionDict;
            }
        }

        /// <summary>
        /// éëäi
        /// </summary>
        static Dictionary<string, Qual> qualDict;

        /// <summary>
        /// éëäi
        /// </summary>
        public static Dictionary<string, Qual> QualDict
        {
            get
            {
                if (qualDict == null || qualDict.Count == 0)
                {
                    InitDict();
                }

                return qualDict;
            }
        }

        static Dictionary<string, string> soapDict;

        public static Dictionary<string, string> SoapDict
        {
            get
            {
                if (soapDict == null || soapDict.Count == 0)
                {
                    InitDict();
                }

                return soapDict;
            }
        }

        static Dictionary<string, string> soapShortDict;

        public static Dictionary<string, string> SoapShortDict
        {
            get
            {
                if (soapShortDict == null || soapShortDict.Count == 0)
                {
                    InitDict();
                }

                return soapShortDict;
            }
        }

        private static void InitDict()
        {
            inOutDict = new Dictionary<string, string>();

            inOutDict.Add("0", "");
            inOutDict.Add("1", "äOóà");
            inOutDict.Add("2", "ì¸â@");

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
            ward.Name = "ÇÌÇ©ÇŒ";
            ward.Short = "é·";
            ward.DeptCode = "31";
            ward.BackColor = Color.LightGreen;
            wardDict.Add(ward.Code, ward);

            ward = new Ward();
            ward.SEQ = 4;
            ward.Code = "4";
            ward.Name = "Ç≥Ç≠ÇÁ";
            ward.Short = "ç˜";
            ward.DeptCode = "41";
            ward.BackColor = Color.Pink;
            wardDict.Add(ward.Code, ward);

            ward = new Ward();
            ward.SEQ = 5;
            ward.Code = "5";
            ward.Name = "Ç†Ç‚Çﬂ";
            ward.Short = "è“";
            ward.DeptCode = "33";
            ward.BackColor = Color.Lavender;
            wardDict.Add(ward.Code, ward);

            ward = new Ward();
            ward.SEQ = 99;
            ward.Code = "99";
            ward.Name = "ñ¢íË";
            ward.Short = "ñ¢";
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
            soapDict.Add("9", "ªœÿ");

            soapShortDict = new Dictionary<string, string>();

            soapShortDict.Add("1", "S");
            soapShortDict.Add("2", "O");
            soapShortDict.Add("3", "A");
            soapShortDict.Add("4", "P");
            soapShortDict.Add("5", "F");
            soapShortDict.Add("6", "I");
            soapShortDict.Add("7", "E");
            soapShortDict.Add("9", "ª");

            kouiDict = new Dictionary<string, string>();
            sekouDict = new Dictionary<string, Sekou>();
            deptDict = new Dictionary<string, Dept>();
            doctorDict = new Dictionary<string, Doctor>();
            staffDict = new Dictionary<string, Staff>();
            sectionDict = new Dictionary<string, Section>();
            qualDict = new Dictionary<string, Qual>();

            DB db = DB.Db3;

            db.Open();

            // êfó√ãÊï™
            string cmd = "select CODE, Trim(NAME) NAME from M_SHINKU order by CODE";

            List<StdClass> tmp_list = StdClass.GetList(db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                kouiDict.Add(tmp.DataDict["CODE"].ToString(), tmp.DataDict["NAME"].ToString());
            }

            // é{çsïîèê
            cmd = "select CODE, Trim(NAME) NAME, Trim(S_NAME) S_NAME from M_SEKOU order by CODE";

            tmp_list = StdClass.GetList(db, cmd);

            sekouDict.Add("0", new Sekou());

            foreach (StdClass tmp in tmp_list)
            {
                Sekou obj = new Sekou();

                int.TryParse(tmp.DataDict["CODE"].ToString(), out obj.Code);
                obj.FullName = tmp.DataDict["NAME"].ToString();
                obj.ShortName = tmp.DataDict["S_NAME"].ToString();

                sekouDict.Add(obj.Code.ToString(), obj);
            }

            // êfó√â»
            cmd = "select CODE, NAME, S_NAME, CATEGORY from M_DEPT order by CODE";

            tmp_list = StdClass.GetList(db, cmd);

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

            // à„ét
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

            // ì¸óÕé“
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

            // èäëÆ
            cmd = "select t.CODE, t.NAME, t.S_NAME " +
                ", case when t.DEL_FLG = 1 then 9 else 0 end STATUS " +
                " from M_SYOZOKU t " +
                " order by t.CODE";

            tmp_list = StdClass.GetList(db, cmd);

            sectionDict.Add("0", new Section());

            foreach (StdClass tmp in tmp_list)
            {
                Section obj = new Section();

                int.TryParse(tmp.DataDict["CODE"].ToString(), out obj.Code);
                obj.FullName = tmp.DataDict["NAME"].ToString().Trim();
                obj.ShortName = tmp.DataDict["S_NAME"].ToString().Trim();
                int.TryParse(tmp.DataDict["STATUS"].ToString(), out obj.Kind1);

                sectionDict.Add(obj.Code.ToString(), obj);
            }

            // éëäi
            cmd = "select t.CODE, t.NAME, t.S_NAME " +
                ", case when t.DEL_FLG = 1 then 9 else 0 end STATUS " +
                " from M_SHIKAKU t " +
                " order by t.CODE";

            tmp_list = StdClass.GetList(db, cmd);

            qualDict.Add("0", new Qual());

            foreach (StdClass tmp in tmp_list)
            {
                Qual obj = new Qual();

                int.TryParse(tmp.DataDict["CODE"].ToString(), out obj.Code);
                obj.FullName = tmp.DataDict["NAME"].ToString().Trim();
                obj.ShortName = tmp.DataDict["S_NAME"].ToString().Trim();
                int.TryParse(tmp.DataDict["STATUS"].ToString(), out obj.Kind1);

                qualDict.Add(obj.Code.ToString(), obj);
            }

            db.Close();
        }

        /// <summary>
        /// É}ÉXÉ^Å[é´èëçÏê¨Åiãxì˙Åj
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

        /// <summary>
        /// É[ÉçñÑÇﬂÉRÅ[Éh
        /// </summary>
        public string Code2
        {
            get
            {
                return this.Code.PadLeft(2, '0');
            }
        }

        public string Name = "";
        public string Short = "";
        public string DeptCode = "";
        public Color BackColor = Color.White;

        /*
        static Dictionary<string, Ward> dict = new Dictionary<string, Ward>();

        static Dictionary<string, Ward> Dict
        {
            get
            {
                if (dict.Count == 0)
                {
                    Ward obj = new Ward();
                    obj.SEQ = 0;
                    obj.Code = "0";
                    obj.Name = "";
                    obj.Short = "";
                    obj.BackColor = Color.White;

                    dict.Add(obj.Code, obj);

                    obj = new Ward();
                    obj.SEQ = 3;
                    obj.Code = "3";
                    obj.Name = "ÇÌÇ©ÇŒ";
                    obj.Short = "é·";
                    obj.DeptCode = "31";
                    obj.BackColor = Color.LightGreen;

                    dict.Add(obj.Code, obj);

                    obj = new Ward();
                    obj.SEQ = 4;
                    obj.Code = "4";
                    obj.Name = "Ç≥Ç≠ÇÁ";
                    obj.Short = "ç˜";
                    obj.DeptCode = "41";
                    obj.BackColor = Color.Pink;

                    dict.Add(obj.Code, obj);

                    obj = new Ward();
                    obj.SEQ = 5;
                    obj.Code = "5";
                    obj.Name = "Ç†Ç‚Çﬂ";
                    obj.Short = "è“";
                    obj.DeptCode = "33";
                    obj.BackColor = Color.Lavender;

                    dict.Add(obj.Code, obj);

                    obj = new Ward();
                    obj.SEQ = 99;
                    obj.Code = "99";
                    obj.Name = "ñ¢íË";
                    obj.Short = "ñ¢";
                    obj.BackColor = Color.White;

                    dict.Add(obj.Code, obj);
                }

                return dict;
            }
        }
         */
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

        public static Dept Load(string code)
        {
            Dept obj = new Dept();

            foreach (string key in Dict.DeptDict.Keys)
            {
                if (key.Equals(code))
                {
                    obj = Dict.DeptDict[key];
                }
            }

            return obj;
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

        public static Doctor Load(string code)
        {
            Doctor obj = new Doctor();

            foreach (string key in Dict.DoctorDict.Keys)
            {
                if (key.Equals(code))
                {
                    obj = Dict.DoctorDict[key];
                }
            }

            return obj;
        }
    }

    public class Sekou
    {
        public int Code;
        public string FullName;
        public string ShortName;

        public Sekou()
        {
            Code = 0;
            FullName = "";
            ShortName = "";
        }

        public override string ToString()
        {
            return this.FullName;
        }
    }

    /// <summary>
    /// èäëÆ
    /// </summary>
    public class Section
    {
        public int Code;
        public string FullName;
        public string ShortName;
        public int Kind1;

        public Section()
        {
            Code = 0;
            FullName = "";
            ShortName = "";
            Kind1 = 0;
        }

        public override string ToString()
        {
            return this.FullName;
        }

        public static Section Load(string code)
        {
            Section obj = new Section();

            foreach (string key in Dict.SectionDict.Keys)
            {
                if (key.Equals(code))
                {
                    obj = Dict.SectionDict[key];
                }
            }

            return obj;
        }
    }

    /// <summary>
    /// éëäi
    /// </summary>
    public class Qual
    {
        public int Code = 0;
        public string FullName;
        public string ShortName;
        public int Kind1;

        public Qual()
        {
            Code = 0;
            FullName = "";
            ShortName = "";
            Kind1 = 0;
        }

        public override string ToString()
        {
            return this.FullName;
        }

        public static Qual Load(string code)
        {
            Qual obj = new Qual();

            foreach (string key in Dict.QualDict.Keys)
            {
                if (key.Equals(code))
                {
                    obj = Dict.QualDict[key];
                }
            }

            return obj;
        }
    }

    public class Insurance
    {
        public int Code;
        public string FullName;
        public string ShortName;
        public string Kana;
        public int Kind1;

        public Insurance()
        {
            this.Code = 0;
            this.FullName = "";
            this.ShortName = "";
            this.Kana = "";
            this.Kind1 = 0;
        }

        public override string ToString()
        {
            return this.FullName;
        }

        static Dictionary<string, Insurance> dict = new Dictionary<string, Insurance>();

        public static Dictionary<string, Insurance> Dict
        {
            get
            {
                if (dict.Count == 0)
                {
                    Init();
                }

                return dict;
            }
        }

        static void Init()
        {
            dict.Clear();

            string cmd = "select * from M_HOKEN t " +
                " order by t.CODE";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                Insurance obj = new Insurance();

                int.TryParse(tmp.DataDict["CODE"].ToString(), out obj.Code);
                obj.FullName = tmp.DataDict["NAME"].ToString().Trim();
                obj.ShortName = tmp.DataDict["S_NAME"].ToString().Trim();
                obj.Kana = tmp.DataDict["KANA"].ToString().Trim();
                int.TryParse(tmp.DataDict["VAL_3"].ToString(), out obj.Kind1);

                if (!dict.ContainsKey(obj.Code.ToString()))
                {
                    dict.Add(obj.Code.ToString(), obj);
                }
            }
        }


        public static Insurance Load(string code)
        {
            Insurance obj = new Insurance();

            if (Dict.ContainsKey(code))
            {
                obj = Dict[code];
            }

            return obj;
        }
    }
}
