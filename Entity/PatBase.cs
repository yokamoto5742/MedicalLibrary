using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class PatBase : StdEntity
    {
        string _Id = "";

        public string Id
        {
            set
            {
                this._Id = value;
            }
            get
            {
                return this._Id;
            }
        }

        string _Name = "";

        public string Name
        {
            set
            {
                this._Name = value;
            }
            get
            {
                return this._Name;
            }
        }

        string _Kana = "";

        public string Kana
        {
            set
            {
                this._Kana = value;
            }
            get
            {
                return this._Kana;
            }
        }

        string _Birth = "";

        public string Birth
        {
            set
            {
                this._Birth = value;
            }
            get
            {
                return this._Birth;
            }
        }

        public string BirthString
        {
            get
            {
                return DateTimeAgent.DateFormat(Birth, DateTimeAgent.DateFormatKind.LONG);
            }
        }

        public string BirthStringJ
        {
            get
            {
                return DateTimeAgent.DateFormat(Birth, DateTimeAgent.DateFormatKind.J1);
            }
        }

        string _Age = "";

        /// <summary>
        /// 年齢
        /// </summary>
        public string Age
        {
            get
            {
                string result = "";

                if (_Age.Length > 0)
                {
                    result = _Age;
                }
                else if (DateTimeAgent.IsDate(Birth))
                {
                    result = DateTimeAgent.AgeCalc(Birth, DateTime.Now.ToString("yyyyMMdd")).ToString();
                }

                return result;
            }
            set
            {
                _Age = value;
            }
        }

        /// <summary>
        /// 基準日時点での年齢
        /// </summary>
        /// <param name="crit_date"></param>
        /// <returns></returns>
        public string AgeCalc(string crit_date)
        {
            string result = "";

            if (DateTimeAgent.IsDate(Birth))
            {
                DateTime dt = DateTime.Now;

                if (DateTimeAgent.IsDate(crit_date))
                {
                    result = DateTimeAgent.AgeCalc(Birth, crit_date).ToString();
                }
                else
                {
                    result = DateTimeAgent.AgeCalc(Birth, DateTime.Now.ToString("yyyyMMdd")).ToString();
                }
            }

            return result;
        }

        /// <summary>
        /// 基準日時点での年齢
        /// </summary>
        /// <param name="crit_date"></param>
        /// <returns></returns>
        public string AgeCalc(int crit_date)
        {
            string result = "";

            if (Birth.Length == 8 && crit_date.ToString().Length == 8)
            {
                result = DateTimeAgent.AgeCalc(Birth, crit_date.ToString()).ToString();
            }

            return result;
        }

        public string Sex = "";

        public string SexName
        {
            get
            {
                string result = "";

                if (Sex.Equals("1"))
                {
                    result = "男性";
                }
                else if (Sex.Equals("2"))
                {
                    result = "女性";
                }

                return result;
            }
        }

        public string SexNameShort
        {
            get
            {
                string result = "";

                if (Sex.Equals("1"))
                {
                    result = "男";
                }
                else if (Sex.Equals("2"))
                {
                    result = "女";
                }

                return result;
            }
        }

        public string SexNameEng
        {
            get
            {
                string result = "";

                if (Sex.Equals("1"))
                {
                    result = "M";
                }
                else if (Sex.Equals("2"))
                {
                    result = "F";
                }

                return result;
            }
        }

        /// <summary>
        /// 診療科コード
        /// </summary>
        public string Dept = "";

        /// <summary>
        /// 医師コード
        /// </summary>
        public string Doctor = "";


        /// <summary>
        /// 保険
        /// </summary>
        public string Ins = "";

        /// <summary>
        /// 保険区分（IM02RC_F43）
        /// →　Inno カルテでは HOKEN_TYPE
        /// </summary>
        public string InsKind = "";

        /// <summary>
        /// 備考
        /// </summary>
        public string NoteCode = "";

        /// <summary>
        /// 他施設
        /// </summary>
        public string FacilityCode = "";

        /// <summary>
        /// 表示する患者情報（氏名, カナ, 性別, 生年月日, 年齢）
        /// </summary>
        public string Info1
        {
            get
            {
                string s = "";

                if (this.Name.Length > 0)
                {
                    s += Name;

                    if (this.Kana.Length > 0)
                    {
                        s += " (" + this.Kana + ")";
                    }

                    s += " 様";
                }

                if (this.SexName.Length > 0)
                {
                    s += " " + this.SexName;
                }

                if (this.BirthStringJ.Length > 0)
                {
                    s += " " + this.BirthStringJ + "生";
                }

                if (this.Age.Length > 0)
                {
                    s += " " + this.Age + "歳";
                }

                return s;
            }
        }

        public string Tel = "";
        public string Post = "";

        public string Addr1 = "";
        public string Addr2 = "";

        public string Addr
        {
            get
            {
                string s = this.Addr1;

                if (this.Addr2.Length > 0)
                {
                    s += " " + this.Addr2;
                }

                return s;
            }
        }

        public string Dead = "";

        public string InOut = "";




        /// <summary>
        /// 患者を取得する。
        /// </summary>
        /// <param name="pt_id"></param>
        /// <returns></returns>
        public static PatBase Load(string pt_id)
        {
            PatBase obj = new PatBase();

            if (pt_id.Length == 0)
            {
                return obj;
            }

            int id = 0;

            if (!int.TryParse(pt_id, out id))
            {
                return obj;
            }

            string cmd = "select * from M_PATIENT " +
                " where P_ID = " + pt_id;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                obj = GetFromStdClass(tmp);
                break;
            }

            return obj;
        }


        public static List<PatBase> GetList(List<string> pt_list, DB db = null, Action<string> progress = null)
        {
            List<PatBase> list = new List<PatBase>();

            if (pt_list.Count == 0)
            {
                return list;
            }

            if (db == null)
            {
                db = DB.Db3;
            }

            // 検索結果の全患者をまとめて取得するため、select * だと
            // 患者マスタの未使用列（LOB/LONG を含む）まで 32bit プロセスに抱え込むことになる。
            // GetFromStdClass が参照する列だけを取得する（列を増やすときは両方を直すこと）。
            string columns = "P_ID, P_NAME, P_KANA, P_SEX, P_BIRTHDAY_AD, TEL, POST, ADDR_1, ADDR_2, " +
                " IN_HOSPITAL, HOKEN_NOW, PROPERTY_2, PROPERTY_3, PROPERTY_4";

            foreach (string pts in AppString.ConcatLists(pt_list, ",", "", 1000))
            {
                if (pts.Length == 0) break;

                if (progress != null)
                {
                    progress("患者情報を取得中 " + list.Count.ToString("#,0") + " / " + pt_list.Count.ToString("#,0") + "人");
                }

                string cmd = "select " + columns + " from M_PATIENT " +
                    " where P_ID in (" + pts + ")";

                List<StdClass> tmp_list = StdClass.GetList(db, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    list.Add(GetFromStdClass(tmp));
                }
            }

            return list;
        }

        /// <summary>
        /// 患者IDリストから患者情報の辞書（キー: 患者ID）を取得する。
        /// IN句の1000件制限を避けるため分割して取得する。
        /// </summary>
        /// <param name="pt_list">患者IDリスト</param>
        /// <param name="db">使用するDB接続（省略時は DB.Db3）</param>
        /// <param name="progress">進捗の通知先（省略可）</param>
        /// <returns></returns>
        public static Dictionary<string, PatBase> GetDict(List<string> pt_list, DB db = null, Action<string> progress = null)
        {
            Dictionary<string, PatBase> dict = new Dictionary<string, PatBase>();

            foreach (PatBase pat in GetList(pt_list, db, progress))
            {
                if (!dict.ContainsKey(pat.Id))
                {
                    dict.Add(pat.Id, pat);
                }
            }

            return dict;
        }

        /// <summary>
        /// 検索結果リストの PATIENT_ID 列から患者情報の辞書（キー: 患者ID）を取得する。
        /// </summary>
        /// <param name="tmp_list">PATIENT_ID 列を持つ検索結果リスト</param>
        /// <param name="db">使用するDB接続（省略時は DB.Db3）</param>
        /// <param name="progress">進捗の通知先（省略可）</param>
        /// <returns></returns>
        public static Dictionary<string, PatBase> GetDict(List<StdClass> tmp_list, DB db = null, Action<string> progress = null)
        {
            HashSet<string> pt_set = new HashSet<string>();

            foreach (StdClass tmp in tmp_list)
            {
                pt_set.Add(tmp.GetDataString("PATIENT_ID"));
            }

            return GetDict(new List<string>(pt_set), db, progress);
        }

        public static List<PatBase> GetListByNameKanaBirth(string name = "", string kana = "", string birth = "")
        {
            List<PatBase> list = new List<PatBase>();

            if (name.Length == 0 && kana.Length == 0 && birth.Length != 8)
            {
                return list;
            }

            List<string> conds = new List<string>();
            string cond = "";

            if (name.Length > 0)
            {
                cond = "(P_NAME like '%" + name + "%' ";

                if (name.Contains(" "))
                {
                    cond += " or P_NAME like '%" + name.Replace(' ', '　') + "%' ";
                }
                else if (name.Contains("　"))
                {
                    cond += " or P_NAME like '%" + name.Replace('　', ' ') + "%' ";
                }

                cond += ")";

                conds.Add(cond);
            }

            if (kana.Length > 0)
            {
                cond = "(P_KANA like '%" + kana + "%' ";

                if (kana.Contains(" "))
                {
                    cond += " or P_KANA like '%" + kana.Replace(' ', '　') + "%' ";
                }
                else if (name.Contains("　"))
                {
                    cond += " or P_KANA like '%" + kana.Replace('　', ' ') + "%' ";
                }

                cond += ")";

                conds.Add(cond);
            }

            if (birth.Length == 8)
            {
                conds.Add("(P_BIRTHDAY_AD = " + birth + ")");
            }

            string cmd = "select * from M_PATIENT t " +
                " where " + AppString.ConcatList(conds, " and ") +
                " order by P_ID";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }

        public static PatBase GetFromStdClass(StdClass tmp)
        {
            PatBase obj = new PatBase();

            obj.Id = tmp.GetDataString("P_ID");
            obj.Name = tmp.GetDataString("P_NAME").Trim();
            obj.Kana = tmp.GetDataString("P_KANA").Trim();
            obj.Sex = tmp.GetDataString("P_SEX");
            obj.Birth = tmp.GetDataString("P_BIRTHDAY_AD");

            obj.Tel = tmp.GetDataString("TEL").Trim();
            obj.Post = tmp.GetDataString("POST").Trim();
            obj.Addr1 = tmp.GetDataString("ADDR_1").Trim();
            obj.Addr2 = tmp.GetDataString("ADDR_2").Trim();

            if (tmp.GetDataString("IN_HOSPITAL").Equals("1"))
            {
                obj.InOut = "2";
            }
            else
            {
                obj.InOut = "1";
            }

            obj.Ins = tmp.GetDataString("HOKEN_NOW");

            obj.Dead = tmp.GetDataString("PROPERTY_2");
            obj.FacilityCode = tmp.GetDataString("PROPERTY_3");
            obj.NoteCode = tmp.GetDataString("PROPERTY_4");
            return obj;
        }

        /// <summary>
        /// Pat.csv 患者情報読み込み
        /// </summary>
        public static PatBase ReadPatCSV()
        {
            PatBase p = new PatBase();

            string patFile = AppFile.FilePath("pat.csv");

            if (!File.Exists(patFile))
            {
                return p;
            }

            StreamReader reader = new StreamReader(patFile, Encoding.Default);

            string line;
            string[] patCont = new string[50];

            if ((line = reader.ReadLine()) != null)
            {
                for (int i = 0; i < 50; i++)
                {
                    patCont[i] = line.Split(',')[i];
                }

                p.Id = patCont[2].TrimStart('0');
                p.Name = patCont[3];
                p.Kana = patCont[4];
                p.Sex = patCont[5];
                p.Birth = patCont[6];

                p.InOut = patCont[20];

                p.Dept = patCont[13].TrimStart('0');
                p.Doctor = patCont[11].TrimStart('0');

                p.Ins = patCont[31];
            }

            reader.Dispose();

            return p;
        }


        /// <summary>
        /// Pat.csv 生成
        /// </summary>
        public void WritePatCSV()
        {
            string[] writeCont = new string[50];

            writeCont[0] = DateTime.Now.ToString("yyyyMMdd");
            writeCont[2] = Id;
            writeCont[3] = Name;
            writeCont[4] = Kana;
            writeCont[5] = Sex;
            writeCont[6] = Birth;
            writeCont[9] = LoginUser.Id;
            writeCont[10] = LoginUser.Name;

            if (LoginUser.DoctorId.Length > 0 && !LoginUser.DoctorId.Equals("0"))
            {
                writeCont[11] = LoginUser.DoctorId;
                writeCont[12] = LoginUser.DoctorName;
            }

            if (Dept.Length > 0)
            {
                writeCont[13] = Dept;
                writeCont[14] = Dict.DeptDict.ContainsKey(Dept) ? Dict.DeptDict[Dept].ShortName : "";
            }
            else if (LoginUser.DeptId.Length > 0 && !LoginUser.DeptId.Equals("0"))
            {
                writeCont[13] = LoginUser.DeptId;
                writeCont[14] = LoginUser.DeptName;
            }
            else
            {
                writeCont[13] = "";
                writeCont[14] = "";
            }

            if (InOut.Equals("2"))
            {
                writeCont[20] = "2";
            }
            else
            {
                writeCont[20] = "1";
            }

            writeCont[27] = LoginUser.SectionId;

            writeCont[31] = Ins;

            if (LoginUser.Id2.Length > 0 && !LoginUser.Id2.Equals("0"))
            {
                writeCont[32] = LoginUser.Id2;
            }

            try
            {
                string patDir = Env.INNO_HOME;
                if (Directory.Exists(patDir))
                {
                    string patFile = patDir + @"\pat.csv";
                    StreamWriter writer = new StreamWriter(new System.IO.FileStream(patFile, System.IO.FileMode.Create), Encoding.Default);

                    for (int i = 0; i < writeCont.Length; i++)
                    {
                        if (i > 0)
                        {
                            writer.Write(',');
                        }

                        writer.Write(writeCont[i]);
                    }

                    writer.Write("\r\n");
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex);
            }
        }


    }
}
