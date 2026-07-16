using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;
using System.Runtime.CompilerServices;

namespace MedicalLibrary.Entity
{
    public class Staff
    {
        public int Code = 0;
        public string Name = "";

        public string LastName
        {
            get
            {
                string result = "";

                if (Name.Contains(" "))
                {
                    result = Name.Split(' ')[0];
                }
                else
                {
                    result = Name;
                }

                return result;
            }
        }

        public string FirstName
        {
            get
            {
                string result = "";

                if (Name.Contains(" "))
                {
                    result = Name.Split(' ')[1];
                }

                return result;
            }
        }

        public string Kana = "";
        public int QualCode = 0;

        public string QualFullName
        {
            get
            {
                string s = "";

                if (Dict.QualDict.ContainsKey(this.QualCode.ToString()))
                {
                    s = Dict.QualDict[this.QualCode.ToString()].FullName;
                }

                return s;
            }
        }

        public string QualShortName
        {
            get
            {
                string s = "";

                if (Dict.QualDict.ContainsKey(this.QualCode.ToString()))
                {
                    s = Dict.QualDict[this.QualCode.ToString()].ShortName;
                }

                return s;
            }
        }

        public int Status = 0;

        public string StatusName
        {
            get
            {
                string s = "";

                if (this.Status == 1)
                {
                    s = "職員";
                }
                else if (this.Status == 2)
                {
                    s = "臨時";
                }
                else if (this.Status == 3)
                {
                    s = "パート";
                }
                else if (this.Status == 4)
                {
                    s = "委託・派遣";
                }
                else if (this.Status == 9)
                {
                    s = "退職者";
                }

                return s;
            }
        }

        public int SectionCode = 0;

        public string SectionFullName
        {
            get
            {
                string s = "";

                if (Dict.SectionDict.ContainsKey(this.SectionCode.ToString()))
                {
                    s = Dict.SectionDict[this.SectionCode.ToString()].FullName;
                }

                return s;
            }
        }

        public string SectionShortName
        {
            get
            {
                string s = "";

                if (Dict.SectionDict.ContainsKey(this.SectionCode.ToString()))
                {
                    s = Dict.SectionDict[this.SectionCode.ToString()].ShortName;
                }

                return s;
            }
        }

        public int DeptCode = 0;

        public string DeptFullName
        {
            get
            {
                string s = "";

                if (Dict.DeptDict.ContainsKey(this.DeptCode.ToString()))
                {
                    s = Dict.DeptDict[this.DeptCode.ToString()].FullName;
                }

                return s;
            }
        }

        public string DeptShortName
        {
            get
            {
                string s = "";

                if (Dict.DeptDict.ContainsKey(this.DeptCode.ToString()))
                {
                    s = Dict.DeptDict[this.DeptCode.ToString()].ShortName;
                }

                return s;
            }
        }

        public int DoctorCode = 0;

        public bool IsDoctor
        {
            get
            {
                bool result = false;

                if (QualCode.Equals(1))
                {
                    result = true;
                }

                return result;
            }
        }

        public bool IsNurse
        {
            get
            {
                bool result = false;

                if (QualCode.Equals(16))
                {
                    result = true;
                }

                return result;
            }
        }

        public bool IsDrug
        {
            get
            {
                bool result = false;

                if (QualCode.Equals(4))
                {
                    result = true;
                }

                return result;
            }
        }

        public Staff()
        {
        }

        public Staff(int code, string name, string kana, int sectionCode, int status, int qualCode, int deptCode, int doctorCode)
        {
            Code = code;
            Name = name;
            Kana = kana;
            SectionCode = sectionCode;
            Status = status;
            QualCode = qualCode;
            DeptCode = deptCode;
            DoctorCode = doctorCode;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public static Staff Load(string code)
        {
            Staff obj = new Staff();

            foreach (Staff tmp in Dict.StaffDict.Values)
            {
                if (tmp.Code.ToString().Equals(code))
                {
                    obj = tmp;
                    break;
                }
            }

            return obj;
        }

        public static List<Staff> GetListByDoctorCode(string doctor_code)
        {
            List<Staff> list = new List<Staff>();

            foreach (Staff obj in Dict.StaffDict.Values)
            {
                if (obj.DoctorCode.ToString().Equals(doctor_code))
                {
                    list.Add(obj);
                }
            }

            return list;
        }

        public static List<Staff> GetListByNameKana(string name = "", string kana = "")
        {
            List<Staff> list = new List<Staff>();

            string name2 = "";
            string kana2 = "";

            if (name.Length > 0)
            {
                if (name.Contains(" "))
                {
                    name2 = name.Replace(' ', '　');
                }
                else if (name.Contains("　"))
                {
                    name2 = name.Replace('　', ' ');
                }
            }

            if (kana.Length > 0)
            {
                if (kana.Contains(" "))
                {
                    kana2 = kana.Replace(' ', '　');
                }
                else if (kana.Contains("　"))
                {
                    kana2 = kana.Replace('　', ' ');
                }
            }

            foreach (Staff obj in Dict.StaffDict.Values)
            {
                if (name.Length > 0 && kana.Length > 0)
                {
                    if (obj.Name.Contains(name) || (name2.Length > 0 && obj.Name.Contains(name2)))
                    {
                        if (obj.Kana.Contains(kana) || (kana2.Length > 0 && obj.Kana.Contains(kana2)))
                        {
                            list.Add(obj);
                        }
                    }
                }
                else if (name.Length > 0)
                {
                    if (obj.Name.Contains(name) || (name2.Length > 0 && obj.Name.Contains(name2)))
                    {
                        list.Add(obj);
                    }
                }
                else if (kana.Length > 0)
                {
                    if (obj.Kana.Contains(kana) || (kana2.Length > 0 && obj.Kana.Contains(kana2)))
                    {
                        list.Add(obj);
                    }
                }
            }

            return list;
        }

        public static List<Staff> GetListBySectionStatus(string section = "", string status = "")
        {
            List<Staff> list = new List<Staff>();

            foreach (Staff obj in Dict.StaffDict.Values)
            {
                if (section.Length > 0 && status.Length > 0)
                {
                    if (obj.SectionCode.ToString().Equals(section) && obj.Status.ToString().Equals(status))
                    {
                        list.Add(obj);
                    }
                }
                else if (section.Length > 0)
                {
                    if (obj.SectionCode.ToString().Equals(section))
                    {
                        list.Add(obj);
                    }
                }
                else if (status.Length > 0)
                {
                    if (obj.Status.ToString().Equals(status))
                    {
                        list.Add(obj);
                    }
                }
            }

            return list;
        }

		/// <summary>
		/// InnoUketsukeLib による認証。
		/// DLL が無い環境では呼び出し時点で FileNotFoundException が発生するため、
		/// 呼び出し元の try/catch で捕捉できるよう別メソッドに分離（インライン化禁止）。
		/// </summary>
		[MethodImpl(MethodImplOptions.NoInlining)]
		static bool VerifyInnoUketsukeLib(int i, string pw)
		{
			return InnoUketsukeLib.Entity.M_USR.g_Usr1.GetData(i, pw);
		}

        public static Staff Verify(string id, string pw)
        {
            Staff obj = new Staff();

			try
			{
				// InnoUketsukeLib で認証
				int i = 0;
				int.TryParse(id, out i);

				// 認証に失敗したら終了
				if (!VerifyInnoUketsukeLib(i, pw)) return obj;
			}
			catch (Exception ex)
			{
				// InnoUketsukeLib の例外が生じたらスルー
				LibUtility.Except(ex, false);
			}

            string cmd = "select CODE コード, Trim(NAME) 氏名, SYOZOKU 所属, SHIKAKU 資格, DEPT 科コード, DR 医師コード " +
                " from M_USR " +
                " where CODE = " + id;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                int.TryParse(tmp.DataDict["コード"].ToString(), out obj.Code);
                obj.Name = tmp.DataDict["氏名"].ToString();
                int.TryParse(tmp.DataDict["所属"].ToString(), out obj.SectionCode);
                int.TryParse(tmp.DataDict["資格"].ToString(), out obj.QualCode);

                if (tmp.DataDict["科コード"].ToString().Length > 0 && tmp.DataDict["科コード"].ToString() != "0")
                {
                    int.TryParse(tmp.DataDict["科コード"].ToString(), out obj.DeptCode);
                }

                if (tmp.DataDict["医師コード"].ToString().Length > 0 && tmp.DataDict["医師コード"].ToString() != "0")
                {
                    int.TryParse(tmp.DataDict["医師コード"].ToString(), out obj.DoctorCode);
                }

                break;
            }

            return obj;
        }
    }
}
