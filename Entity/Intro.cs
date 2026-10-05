using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class Intro
    {
        public string PtId = "";

        /// <summary>
        /// 種別コード
        /// </summary>
        string _Kind = "";

        public string Kind
        {
            get
            {
                string s = "";

                if (this._Kind.Length > 0)
                {
                    s = this._Kind;
                }
                else
                {
                    if (this.KindString.StartsWith("紹介"))
                    {
                        s = "1";
                    }
                    else if (this.KindString.StartsWith("返事"))
                    {
                        s = "2";
                    }
                    else if (this.KindString.StartsWith("依頼"))
                    {
                        s = "3";
                    }
                }

                return s;
            }
            set
            {
                this._Kind = value;
            }
        }

        /// <summary>
        /// 種別
        /// </summary>
        string _KindString = "";

        public string KindString
        {
            get
            {
                string s = "";

                if (this._KindString.Length > 0)
                {
                    s = this._KindString;
                }
                else
                {
                    if (this.Kind.Equals("1"))
                    {
                        s = "紹介";
                    }
                    else if (this.Kind.Equals("2"))
                    {
                        s = "返事(眼科)";
                    }
                    else if (this.Kind.Equals("3"))
                    {
                        s = "依頼";
                    }
                    else if (this.Kind.Equals("4"))
                    {
                        s = "返信Fax";
                    }
                    else if (this.Kind.Equals("5"))
                    {
                        s = "返事(途中)";
                    }
                    else if (this.Kind.Equals("6"))
                    {
                        s = "返事(最終)";
                    }
                }

                return s;
            }
            set
            {
                this._KindString = value;
            }
        }

        public string IntroDate = "";
        public string Hospital = "";
        public string DeptTo = "";
        public string DoctorTo = "";

        public string DeptFromCode = "";

        public string DeptFromName
        {
            get
            {
                string s = "";

                if (Dict.DeptDict.ContainsKey(this.DeptFromCode))
                {
                    s = Dict.DeptDict[this.DeptFromCode].ShortName;
                }

                return s;
            }
        }

        public string DoctorFromCode = "";

        string _DoctorFromName = "";

        public string DoctorFromName
        {
            get
            {
                string s = "";

                if (this._DoctorFromName.Length > 0)
                {
                    s = this._DoctorFromName;
                }
                else if (Dict.DoctorDict.ContainsKey(this.DoctorFromCode))
                {
                    s = Dict.DoctorDict[this.DoctorFromCode].Name;
                }

                return s;
            }
            set
            {
                this._DoctorFromName = value;
            }
        }

        static Intro GetFromStdClass(StdClass tmp)
        {
            Intro obj = new Intro();

            if (tmp.GetDataString("PATIENT_ID").Length > 0)
            {
                // 旧システム
                obj.PtId = tmp.GetDataString("PATIENT_ID");
                obj.Kind = tmp.GetDataString("INTRO_KIND");
                obj.IntroDate = tmp.GetDataString("INTRO_DATE");
                obj.Hospital = tmp.GetDataString("HOSPITAL").Trim();
                obj.DeptTo = tmp.GetDataString("DEPT_TO").Trim();
                obj.DoctorTo = tmp.GetDataString("DOCTOR_TO").Trim();
                obj.DeptFromCode = tmp.GetDataString("DEPT_FROM").TrimStart('0');
                obj.DoctorFromCode = tmp.GetDataString("DOCTOR_FROM");
            }
            else if (tmp.GetDataString("PATIENTID").Length > 0)
            {
                // 新システム
                obj.PtId = tmp.GetDataString("PATIENTID");
                obj.KindString = tmp.GetDataString("KIND");
                obj.IntroDate = tmp.GetDataString("INTRO_DATE");
                obj.Hospital = tmp.GetDataString("DESTNAME").Trim();
                obj.DeptTo = tmp.GetDataString("DESTDEPARTMENTNAME").Trim();
                obj.DoctorTo = tmp.GetDataString("DESTDOCTORNAME").Trim();
                obj.DeptFromCode = tmp.GetDataString("FROMDEPARTMENTID");
                obj.DoctorFromName = tmp.GetDataString("FROMDOCTORNAME");
            }

            return obj;
        }

        /// <summary>
        /// 新システムの PATIENTINTRODUCTION の所有者（null は未確認、空文字はテーブルなし）
        /// </summary>
        static string newOwner = null;

        public static List<Intro> GetList(string pt_id)
        {
            List<Intro> list = new List<Intro>();

            if (pt_id.Length == 0)
            {
                return list;
            }

            string cmd = "";
            List<StdClass> tmp_list;

            // 新システムのテーブルが存在するか（所有者は変わらないので初回だけ調べる）
            if (newOwner == null)
            {
                string owner = "";
                cmd = "select * from dba_tables where table_name = 'PATIENTINTRODUCTION'";

                tmp_list = StdClass.GetList(DB.Db3, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    owner = tmp.GetDataString("OWNER");
                    break;
                }

                newOwner = owner;
            }

            // 新システムのテーブルが存在する場合
            if (newOwner.Length > 0)
            {
                cmd = "select PATIENTID, KIND, to_char(CREATEDATE, 'YYYYMMDD') INTRO_DATE, DESTNAME, DESTDEPARTMENTNAME, DESTDOCTORNAME, FROMDEPARTMENTID, FROMDOCTORNAME " +
                    " from " + newOwner + ".PATIENTINTRODUCTION " +
                    " where PATIENTID = " + pt_id + " and (ARCHIVESTATE is null or ARCHIVESTATE != 9) " +
                    " order by CREATEDATE desc";

                tmp_list = StdClass.GetList(DB.Db3, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    list.Add(GetFromStdClass(tmp));
                }
            }

            // 旧システム
            cmd = "select PATIENT_ID, INTRO_KIND, to_char(INTRO_DATE, 'YYYYMMDD') INTRO_DATE, HOSPITAL, DEPT_TO, DOCTOR_TO, DEPT_FROM, DOCTOR_FROM " +
                " from INTRODUCTION " +
                " where PATIENT_ID = " + pt_id + " and DELETE_FLAG != 1 " +
                " order by INTRO_DATE desc";

            tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            // 日付で降順に並べ替える
            list.Sort((x, y) =>
                {
                    int i = y.IntroDate.CompareTo(x.IntroDate);

                    return i;
                });

            return list;
        }
    }
}
