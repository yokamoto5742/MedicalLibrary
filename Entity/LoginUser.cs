using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Oracle.DataAccess.Client;
using MedicalLibrary.Boundary;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class LoginUser : StdEntity
    {
        static string id = "";
        static string name = "";

        /// <summary>
        /// 代行入力の場合、本当の入力者
        /// </summary>
        static string id2 = "";
        static string name2 = "";

        static string section_id = "";
        static string qual_id = "";
        static string dept_id = "";
        static string doctor_id = "";


        public static string Id
        {
            get
            {
                return id;
            }
        }

        public static string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// 代行入力の場合、本当の入力者のID
        /// </summary>
        public static string Id2
        {
            get
            {
                return id2;
            }
            set
            {
                id2 = value;
            }
        }

        /// <summary>
        /// 代行入力の場合、本当の入力者の氏名
        /// </summary>
        public static string Name2
        {
            get
            {
                return name2;
            }
        }

        public static string SectionId
        {
            get
            {
                return section_id;
            }
        }

        public static string SectionName
        {
            get
            {
                string s = "";

                if (Dict.SectionDict.ContainsKey(section_id))
                {
                    s = Dict.SectionDict[section_id].ShortName;
                }

                return s;
            }
        }

        public static string QualId
        {
            get
            {
                return qual_id;
            }
        }

        public static string QualName
        {
            get
            {
                string s = "";

                if (Dict.QualDict.ContainsKey(qual_id))
                {
                    s = Dict.QualDict[qual_id].ShortName;
                }

                return s;
            }
        }

        public static string DeptId
        {
            get
            {
                return dept_id;
            }
        }

        public static string DeptName
        {
            get
            {
                string result = "";

                if (dept_id.Length > 0 && Dict.DeptDict.ContainsKey(dept_id))
                {
                    result = Dict.DeptDict[dept_id].ShortName;
                }

                return result;
            }
        }

        public static string DoctorId
        {
            get
            {
                return doctor_id;
            }
        }

        public static string DoctorName
        {
            get
            {
                string result = "";

                if (doctor_id.Length > 0 && Dict.DoctorDict.ContainsKey(doctor_id))
                {
                    result = Dict.DoctorDict[doctor_id].Name;
                }

                return result;
            }
        }

        /// <summary>
        /// 管理者か否か
        /// </summary>
        public static bool IsAdmin
        {
            get
            {
                bool b = false;

                if (id.Equals("519"))
                {
                    b = true;
                }

                return b;
            }
        }

        /// <summary>
        /// 医師か否か
        /// </summary>
        public static bool IsDoctor
        {
            get
            {
                bool result = false;

                if (section_id.Equals("1"))
                {
                    result = true;
                }

                return result;
            }
        }

        /// <summary>
        /// 看護部か否か
        /// </summary>
        public static bool IsNurse
        {
            get
            {
                bool result = false;

                if (section_id.Equals("11"))
                {
                    result = true;
                }

                return result;
            }
        }

        /// <summary>
        /// 薬剤課か否か
        /// </summary>
        public static bool IsDrug
        {
            get
            {
                bool result = false;

                if (section_id.Equals("21"))
                {
                    result = true;
                }

                return result;
            }
        }

        /// <summary>
        /// リハビリ課か否か
        /// </summary>
        public static bool IsReha
        {
            get
            {
                bool result = false;

                if (section_id.Equals("25"))
                {
                    result = true;
                }
                
                return result;
            }
        }

        /// <summary>
        /// DPCスタッフ（338 山本洋介、355 荒谷真由美、827 関口佳津子、所属 = 9 診療支援課）
        /// </summary>
        public static bool IsDPC
        {
            get
            {
                bool result = false;

                if (id.Equals("519") || id.Equals("338") || id.Equals("355") || id.Equals("827") ||
                    section_id.Equals("9"))
                {
                    result = true;
                }

                return result;
            }
        }

        public static Staff LoginStaff
        {
            get
            {
                Staff s = new Staff();

                int.TryParse(id, out s.Code);
                s.Name = name;

                int.TryParse(qual_id, out s.QualCode);
                int.TryParse(section_id, out s.SectionCode);

                int.TryParse(dept_id, out s.DeptCode);
                int.TryParse(doctor_id, out s.DoctorCode);

                return s;
            }
        }


        public enum STATUS : int
        {
            NONE = 0,
            STAFF = 1,
//            CLERK = 2,
            BOTH = 4
        }

        /// <summary>
        /// ログイン状態
        /// </summary>
        public static STATUS Status
        {
            get
            {
                STATUS s = STATUS.NONE;

                if (id.Length > 0)
                {
                    if (id2.Length > 0)
                    {
                        s = STATUS.BOTH;
                    }
                    else
                    {
                        s = STATUS.STAFF;
                    }
                }

                return s;
            }
        }

        public static void Clear()
        {
            id = "";
            name = "";

            id2 = "";
            name2 = "";

            section_id = "";
            qual_id = "";

            dept_id = "";
            doctor_id = "";
        }

        public static bool Init(bool read_pat_csv = true, string[] args = null)
        {
            // Pat.csv を見る
            if (read_pat_csv)
            {
                LoginUser.ReadPatCSV();
            }

            // パラメータに -u があればそちらを優先する
            if (args != null)
            {
                int user_id = 0;

                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].Equals("-u", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (i < args.Length - 1 && int.TryParse(args[i + 1], out user_id))
                        {
                            // 次のパラメータが数字ならばユーザーIDとみなす
                            break;
                        }
                    }
                }

                if (user_id > 0)
                {
                    LoginUser.SetUser(user_id.ToString());
                }
            }

            // この時点で Id が無かったらログイン画面を表示する
            if (LoginUser.Id.Length == 0)
            {
                showLoginPrompt();
            }

            if (LoginUser.Id.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ログイン画面を表示する。
        /// </summary>
        private static void showLoginPrompt()
        {
            LoginPrompt lp = new LoginPrompt();
            lp.ShowDialog();
        }

        /// <summary>
        /// Pat.csv からログインユーザーの情報のみ読み込む。
        /// </summary>
        public static void ReadPatCSV()
        {
            string patFile = AppFile.FilePath("pat.csv");

            if (!File.Exists(patFile))
            {
                return;
            }

            LoginUser.Clear();

            StreamReader reader = new StreamReader(patFile, Encoding.Default);

            string line;
            string[] patCont = new string[50];

            if ((line = reader.ReadLine()) != null)
            {
                for (int i = 0; i < 50; i++)
                {
                    patCont[i] = line.Split(',')[i];
                }

                id = patCont[9].TrimStart('0');
                name = patCont[10];

                doctor_id = patCont[11].TrimStart('0');
                dept_id = patCont[13].TrimStart('0');

                section_id = patCont[27];
                id2 = patCont[32].TrimStart('0');
            }

            reader.Dispose();

            // 取得した id に基づいて各種情報をセットする。
            if (id.Length > 0)
            {
                SetUser(id, id2);
            }
        }
/*
        /// <summary>
        /// ログインIDをセットする。
        /// さらにユーザー名・セクションID・セクション名・資格ID・資格名・診療科ID・医師ID・医師名を取得してセットする。
        /// </summary>
        /// <param name="_id">ユーザーID</param>
        public static void SetUser(string _id)
        {
            int i = 0;

            if (_id.Length == 0 || !int.TryParse(_id, out i))
            {
                return;
            }

            id = _id;

            string cmd = "select IM90RC_F01 コード, Trim(IM90RC_F03) 氏名, IM90RC_F04 所属, IM90RC_F08 資格, IM90RC_F13 科コード, IM90RC_F14 医師コード " +
                " from IM90RC " +
                " where IM90RC_F01 = " + _id;

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                id = tmp.DataDict["コード"].ToString();
                name = tmp.DataDict["氏名"].ToString();
                section_id = tmp.DataDict["所属"].ToString();
                qual_id = tmp.DataDict["資格"].ToString();

                if (tmp.DataDict["科コード"].ToString().Length > 0 && tmp.DataDict["科コード"].ToString() != "0")
                {
                    dept_id = tmp.DataDict["科コード"].ToString();
                }

                if (tmp.DataDict["医師コード"].ToString().Length > 0 && tmp.DataDict["医師コード"].ToString() != "0")
                {
                    doctor_id = tmp.DataDict["医師コード"].ToString();
                }

                break;
            }
        }
*/
        /// <summary>
        /// ログインIDをセットする。
        /// さらにユーザー名・セクションID・セクション名・資格ID・資格名・診療科ID・医師ID・医師名を取得してセットする。
        /// </summary>
        /// <param name="_id">ユーザーID</param>
        /// <param name="_id2">代行者ID</param>
        public static void SetUser(string _id, string _id2 = "")
        {
            int i = 0;

            if (_id.Length == 0 || !int.TryParse(_id, out i))
            {
                return;
            }

            id = _id;

            string cmd = "select CODE コード, Trim(NAME) 氏名, SYOZOKU 所属, SHIKAKU 資格, DEPT 科コード, DR 医師コード " +
                " from M_USR " +
                " where CODE = " + _id;

            if (_id2.Length > 0)
            {
                cmd += " or CODE = " + _id2;
            }

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                if (tmp.DataDict["コード"].ToString().Equals(_id))
                {
                    id = tmp.DataDict["コード"].ToString();
                    name = tmp.DataDict["氏名"].ToString();
                    section_id = tmp.DataDict["所属"].ToString();
                    qual_id = tmp.DataDict["資格"].ToString();

                    if (tmp.DataDict["科コード"].ToString().Length > 0 && tmp.DataDict["科コード"].ToString() != "0")
                    {
                        dept_id = tmp.DataDict["科コード"].ToString();
                    }

                    if (tmp.DataDict["医師コード"].ToString().Length > 0 && tmp.DataDict["医師コード"].ToString() != "0")
                    {
                        doctor_id = tmp.DataDict["医師コード"].ToString();
                    }
                }
                else if (tmp.DataDict["コード"].ToString().Equals(_id2))
                {
                    id2 = tmp.DataDict["コード"].ToString();
                    name2 = tmp.DataDict["氏名"].ToString();
                }
            }
        }
    }
}
