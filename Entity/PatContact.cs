using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class PatContact : StdKarte1
    {
        /// <summary>
        /// 連番
        /// </summary>
        public int SEQ = 0;

        /// <summary>
        /// 優先順位
        /// </summary>
        public int ShowSEQ = 0;

        /// <summary>
        /// 氏名
        /// </summary>
        public string Name = "";

        public string Kana = "";

        public string Birth = "";

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

        string age = "";

        /// <summary>
        /// 年齢
        /// </summary>
        public string Age
        {
            get
            {
                string result = "";

                if (age.Length > 0)
                {
                    result = age;
                }
                else if (Birth.Length == 8)
                {
                    result = DateTimeAgent.AgeCalc(Birth, DateTime.Now.ToString("yyyyMMdd")).ToString();
                }

                return result;
            }
            set
            {
                age = value;
            }
        }

        /// <summary>
        /// 続柄
        /// </summary>
        public string RelationCode = "";

        /// <summary>
        /// 続柄
        /// </summary>
        public string RelationVal
        {
            get
            {
                string s = "";

                if (PatContactRelation.Dict.ContainsKey(this.RelationCode))
                {
                    s = PatContactRelation.Dict[this.RelationCode].Val;
                }

                return s;
            }
        }

        /// <summary>
        /// 続柄コメント
        /// </summary>
        public string RelationComment = "";

        /// <summary>
        /// 電話番号１
        /// </summary>
        public string Tel1 = "";

        /// <summary>
        /// 連絡先区分１
        /// </summary>
        public string KindCode1 = "";

        /// <summary>
        /// 連絡先区分１
        /// </summary>
        public string KindVal1
        {
            get
            {
                string s = "";

                if (PatContactKind.Dict.ContainsKey(this.KindCode1))
                {
                    s = PatContactKind.Dict[this.KindCode1].Val;
                }

                return s;
            }
        }


        /// <summary>
        /// 電話番号２
        /// </summary>
        public string Tel2 = "";

        /// <summary>
        /// 連絡先区分２
        /// </summary>
        public string KindCode2 = "";

        /// <summary>
        /// 連絡先区分２
        /// </summary>
        public string KindVal2
        {
            get
            {
                string s = "";

                if (PatContactKind.Dict.ContainsKey(this.KindCode2))
                {
                    s = PatContactKind.Dict[this.KindCode2].Val;
                }

                return s;
            }
        }

        /// <summary>
        /// 電話番号３
        /// </summary>
        public string Tel3 = "";

        /// <summary>
        /// 連絡先区分３
        /// </summary>
        public string KindCode3 = "";

        /// <summary>
        /// 連絡先区分３
        /// </summary>
        public string KindVal3
        {
            get
            {
                string s = "";

                if (PatContactKind.Dict.ContainsKey(this.KindCode3))
                {
                    s = PatContactKind.Dict[this.KindCode3].Val;
                }

                return s;
            }
        }

        /// <summary>
        /// 健康状態
        /// </summary>
        public string Health = "";

        /// <summary>
        /// 同別居区分
        /// </summary>
        public string ResidentCode = "";

        /// <summary>
        /// 同別居
        /// </summary>
        public string ResidentVal
        {
            get
            {
                string s = "";

                if (PatContactResident.Dict.ContainsKey(this.ResidentCode))
                {
                    s = PatContactResident.Dict[this.ResidentCode].Val;
                }

                return s;
            }
        }


        /// <summary>
        /// 介護役割
        /// </summary>
        public string Care = "";


        /// <summary>
        /// 備考
        /// </summary>
        public string Cont = "";


        public static PatContact Load(string pt_id, int seq = 1)
        {
            PatContact obj = new PatContact();

            if (pt_id.Length == 0)
            {
                return obj;
            }
            string cmd = "select * from M_PATIENT_FAMILY t " +
                " where t.P_ID = " + pt_id +
                " and t.P_SEQ = " + seq;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
            foreach (StdClass tmp in tmp_list)
            {
                obj = GetFromStdClass(tmp);
                break;
            }

            return obj;
        }

        public static List<PatContact> GetList(string pt_id)
        {
            List<PatContact> list = new List<PatContact>();

            if (pt_id.Length == 0)
            {
                return list;
            }
            string cmd = "select * from M_PATIENT_FAMILY t " +
                " where t.P_ID = " + pt_id +
                " order by t.CONTACT_ORDER";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }

        static PatContact GetFromStdClass(StdClass tmp)
        {
            PatContact obj = new PatContact();

            obj.BaseFromStdClass(tmp);
            int.TryParse(tmp.DataDict["P_SEQ"].ToString(), out obj.SEQ);
            int.TryParse(tmp.DataDict["CONTACT_ORDER"].ToString(), out obj.ShowSEQ);
            obj.Kana = tmp.DataDict["KANA"].ToString().Trim();
            obj.Name = tmp.DataDict["NAME"].ToString().Trim();
            obj.Birth = tmp.DataDict["BIRTHDAY_AD"].ToString();
            obj.RelationCode = tmp.DataDict["RELATIONSHIP"].ToString();
            obj.RelationComment = tmp.DataDict["RE_COMMENT"].ToString();
            obj.Tel1 = tmp.DataDict["TEL_1"].ToString();
            obj.KindCode1 = tmp.DataDict["TEL_TYPE_1"].ToString();
            obj.Tel2 = tmp.DataDict["TEL_2"].ToString();
            obj.KindCode2 = tmp.DataDict["TEL_TYPE_2"].ToString();
            obj.Tel3 = tmp.DataDict["TEL_3"].ToString();
            obj.KindCode3 = tmp.DataDict["TEL_TYPE_3"].ToString();
            obj.Health = tmp.DataDict["COMMENT_1"].ToString();
            obj.ResidentCode = tmp.DataDict["LIVE_TYPE"].ToString();
            obj.Care = tmp.DataDict["CARE_TYPE"].ToString();
            obj.Cont = tmp.DataDict["COMMENT_2"].ToString();
            return obj;
        }

        static int GetMaxSEQ(string pt_id)
        {
            int seq = 0;

            if (pt_id.Length == 0)
            {
                return seq;
            }
            string cmd = "select max(P_SEQ) 連番 from M_PATIENT_FAMILY t " +
                " where t.P_ID = " + pt_id;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
            foreach (StdClass tmp in tmp_list)
            {
                if (tmp.DataDict["連番"].ToString().Length > 0)
                {
                    seq = int.Parse(tmp.DataDict["連番"].ToString());
                }

                break;
            }

            return seq;
        }

        public StdReturn Insert()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
            return sr;
        }


        public StdReturn Update()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
            return sr;
        }


        public StdReturn Delete()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
            return sr;
        }
    }


    /// <summary>
    /// 続柄
    /// </summary>
    public class PatContactRelation
    {
        public string Code = "";

        public string Val = "";

        public override string ToString()
        {
            return this.Val;
        }

        public PatContactRelation(string code = "", string val = "")
        {
            this.Code = code;
            this.Val = val;
        }

        /// <summary>
        /// 続柄
        /// </summary>
        static Dictionary<string, PatContactRelation> dict = new Dictionary<string, PatContactRelation>();

        /// <summary>
        /// 続柄
        /// </summary>
        public static Dictionary<string, PatContactRelation> Dict
        {
            get
            {
                if (dict.Count == 0)
                {
                    dict.Add("0", new PatContactRelation("0", ""));
                    dict.Add("1", new PatContactRelation("1", "本人"));
                    dict.Add("2", new PatContactRelation("2", "夫"));
                    dict.Add("3", new PatContactRelation("3", "妻"));
                    dict.Add("4", new PatContactRelation("4", "息子"));
                    dict.Add("5", new PatContactRelation("5", "娘"));
                    dict.Add("6", new PatContactRelation("6", "父"));
                    dict.Add("7", new PatContactRelation("7", "母"));
                    dict.Add("8", new PatContactRelation("8", "兄"));
                    dict.Add("9", new PatContactRelation("9", "姉"));
                    dict.Add("10", new PatContactRelation("10", "弟"));
                    dict.Add("11", new PatContactRelation("11", "妹"));
                    dict.Add("12", new PatContactRelation("12", "叔父・伯父"));
                    dict.Add("13", new PatContactRelation("13", "叔母・伯母"));
                    dict.Add("14", new PatContactRelation("14", "甥"));
                    dict.Add("15", new PatContactRelation("15", "姪"));
                    dict.Add("16", new PatContactRelation("16", "祖父"));
                    dict.Add("17", new PatContactRelation("17", "祖母"));
                    dict.Add("18", new PatContactRelation("18", "親族"));
                    dict.Add("19", new PatContactRelation("19", "同居人"));
                    dict.Add("20", new PatContactRelation("20", "知人・友人"));
                    dict.Add("21", new PatContactRelation("21", "その他"));
                }

                return dict;
            }
        }
    }

    /// <summary>
    /// 連絡先区分
    /// </summary>
    public class PatContactKind
    {
        public string Code = "";

        public string Val = "";

        public override string ToString()
        {
            return this.Val;
        }

        public PatContactKind(string code, string val)
        {
            this.Code = code;
            this.Val = val;
        }

        static Dictionary<string, PatContactKind> dict = new Dictionary<string, PatContactKind>();

        public static Dictionary<string, PatContactKind> Dict
        {
            get
            {
                if (dict.Count == 0)
                {
                    dict.Add("0", new PatContactKind("0", ""));
                    dict.Add("1", new PatContactKind("1", "自宅"));
                    dict.Add("2", new PatContactKind("2", "勤務先"));
                    dict.Add("3", new PatContactKind("3", "携帯"));
                    dict.Add("4", new PatContactKind("4", "その他"));
                }

                return dict;
            }
        }
    }

    /// <summary>
    /// 同別居
    /// </summary>
    public class PatContactResident
    {
        public string Code = "";

        public string Val = "";

        public override string ToString()
        {
            return this.Val;
        }

        public PatContactResident(string code, string val)
        {
            this.Code = code;
            this.Val = val;
        }

        static Dictionary<string, PatContactResident> dict = new Dictionary<string, PatContactResident>();

        public static Dictionary<string, PatContactResident> Dict
        {
            get
            {
                if (dict.Count == 0)
                {
                    dict.Add("0", new PatContactResident("0", ""));
                    dict.Add("1", new PatContactResident("1", "同居"));
                    dict.Add("2", new PatContactResident("2", "別居"));
                }

                return dict;
            }
        }
    }
}
