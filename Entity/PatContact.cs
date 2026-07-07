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
#if INNO
            string cmd = "select * from M_PATIENT_FAMILY t " +
                " where t.P_ID = " + pt_id +
                " and t.P_SEQ = " + seq;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
#else
            string cmd = "select * from ADT_患者家族連絡先データ t " +
                " where t.患者コード = " + pt_id +
                " and t.連番 = " + seq;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif
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
#if INNO
            string cmd = "select * from M_PATIENT_FAMILY t " +
                " where t.P_ID = " + pt_id +
                " order by t.CONTACT_ORDER";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
#else
            string cmd = "select * from ADT_患者家族連絡先データ t " +
                " where t.患者コード = " + pt_id +
                " order by t.連絡優先順位";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif
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
#if INNO
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
#else
            int.TryParse(tmp.DataDict["連番"].ToString(), out obj.SEQ);
            int.TryParse(tmp.DataDict["連絡優先順位"].ToString(), out obj.ShowSEQ);
            obj.Kana = tmp.DataDict["カナ氏名"].ToString();
            obj.Name = tmp.DataDict["漢字氏名"].ToString();
            obj.Birth = tmp.DataDict["生年月日"].ToString();
            obj.RelationCode = tmp.DataDict["続柄区分"].ToString();
            obj.RelationComment = tmp.DataDict["続柄コメント"].ToString();
            obj.Tel1 = tmp.DataDict["電話番号１"].ToString();
            obj.KindCode1 = tmp.DataDict["連絡先区分１"].ToString();
            obj.Tel2 = tmp.DataDict["電話番号２"].ToString();
            obj.KindCode2 = tmp.DataDict["連絡先区分２"].ToString();
            obj.Tel3 = tmp.DataDict["電話番号３"].ToString();
            obj.KindCode3 = tmp.DataDict["連絡先区分３"].ToString();
            obj.Health = tmp.DataDict["健康状態"].ToString();
            obj.ResidentCode = tmp.DataDict["同別居区分"].ToString();
            obj.Care = tmp.DataDict["介護役割"].ToString();
            obj.Cont = tmp.DataDict["備考"].ToString();
#endif
            return obj;
        }

        static int GetMaxSEQ(string pt_id)
        {
            int seq = 0;

            if (pt_id.Length == 0)
            {
                return seq;
            }
#if INNO
            string cmd = "select max(P_SEQ) 連番 from M_PATIENT_FAMILY t " +
                " where t.P_ID = " + pt_id;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
#else
            string cmd = "select max(連番) 連番 from ADT_患者家族連絡先データ t " +
                " where t.患者コード = " + pt_id;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif
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
#if INNO
#else
            obj.Db = DB.Db1;
            obj.Table = "ADT_患者家族連絡先データ";

            // 連番を取得する
            this.SEQ = GetMaxSEQ(this.PtId) + 1;

            string reg_date = DateTime.Now.ToString("yyyyMMdd");
            string reg_time = DateTime.Now.ToString("HHmmss");

            obj.DataList.Add(new StdDbColumn("患者コード", StdDbType.NUMBER, this.PtId));
            obj.DataList.Add(new StdDbColumn("連番", StdDbType.NUMBER, this.SEQ));
            obj.DataList.Add(new StdDbColumn("連絡優先順位", StdDbType.NUMBER, this.ShowSEQ));
            obj.DataList.Add(new StdDbColumn("カナ氏名", StdDbType.VARCHAR2, this.Kana));
            obj.DataList.Add(new StdDbColumn("漢字氏名", StdDbType.VARCHAR2, this.Name));
            obj.DataList.Add(new StdDbColumn("生年月日", StdDbType.NUMBER, (this.Birth.Length == 8) ? this.Birth : "0"));
            obj.DataList.Add(new StdDbColumn("続柄区分", StdDbType.NUMBER, this.RelationCode));
            obj.DataList.Add(new StdDbColumn("続柄コメント", StdDbType.VARCHAR2, this.RelationComment));
            obj.DataList.Add(new StdDbColumn("電話番号１", StdDbType.VARCHAR2, this.Tel1));
            obj.DataList.Add(new StdDbColumn("連絡先区分１", StdDbType.NUMBER, this.KindCode1));
            obj.DataList.Add(new StdDbColumn("電話番号２", StdDbType.VARCHAR2, this.Tel2));
            obj.DataList.Add(new StdDbColumn("連絡先区分２", StdDbType.NUMBER, this.KindCode2));
            obj.DataList.Add(new StdDbColumn("電話番号３", StdDbType.VARCHAR2, this.Tel3));
            obj.DataList.Add(new StdDbColumn("連絡先区分３", StdDbType.NUMBER, this.KindCode3));
            obj.DataList.Add(new StdDbColumn("健康状態", StdDbType.VARCHAR2, this.Health));
            obj.DataList.Add(new StdDbColumn("同別居区分", StdDbType.NUMBER, this.ResidentCode));
            obj.DataList.Add(new StdDbColumn("介護役割", StdDbType.VARCHAR2, this.Care));
            obj.DataList.Add(new StdDbColumn("備考", StdDbType.VARCHAR2, this.Cont));
            obj.DataList.Add(new StdDbColumn("登録日", StdDbType.NUMBER, reg_date));
            obj.DataList.Add(new StdDbColumn("登録時間", StdDbType.NUMBER, reg_time));
            obj.DataList.Add(new StdDbColumn("登録者", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("代行登録者", StdDbType.NUMBER, LoginUser.Id2));
            obj.DataList.Add(new StdDbColumn("更新日", StdDbType.NUMBER, reg_date));
            obj.DataList.Add(new StdDbColumn("更新時間", StdDbType.NUMBER, reg_time));
            obj.DataList.Add(new StdDbColumn("更新者", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("代行更新者", StdDbType.NUMBER, LoginUser.Id2));

            sr = obj.InsertSQL();
#endif
            return sr;
        }


        public StdReturn Update()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
#if INNO
#else
            obj.Db = DB.Db1;
            obj.Table = "ADT_患者家族連絡先データ";

            string reg_date = DateTime.Now.ToString("yyyyMMdd");
            string reg_time = DateTime.Now.ToString("HHmmss");

            obj.DataList.Add(new StdDbColumn("連絡優先順位", StdDbType.NUMBER, this.ShowSEQ));
            obj.DataList.Add(new StdDbColumn("カナ氏名", StdDbType.VARCHAR2, this.Kana));
            obj.DataList.Add(new StdDbColumn("漢字氏名", StdDbType.VARCHAR2, this.Name));
            obj.DataList.Add(new StdDbColumn("生年月日", StdDbType.NUMBER, (this.Birth.Length == 8) ? this.Birth : "0"));
            obj.DataList.Add(new StdDbColumn("続柄区分", StdDbType.NUMBER, this.RelationCode));
            obj.DataList.Add(new StdDbColumn("続柄コメント", StdDbType.VARCHAR2, this.RelationComment));
            obj.DataList.Add(new StdDbColumn("電話番号１", StdDbType.VARCHAR2, this.Tel1));
            obj.DataList.Add(new StdDbColumn("連絡先区分１", StdDbType.NUMBER, this.KindCode1));
            obj.DataList.Add(new StdDbColumn("電話番号２", StdDbType.VARCHAR2, this.Tel2));
            obj.DataList.Add(new StdDbColumn("連絡先区分２", StdDbType.NUMBER, this.KindCode2));
            obj.DataList.Add(new StdDbColumn("電話番号３", StdDbType.VARCHAR2, this.Tel3));
            obj.DataList.Add(new StdDbColumn("連絡先区分３", StdDbType.NUMBER, this.KindCode3));
            obj.DataList.Add(new StdDbColumn("健康状態", StdDbType.VARCHAR2, this.Health));
            obj.DataList.Add(new StdDbColumn("同別居区分", StdDbType.NUMBER, this.ResidentCode));
            obj.DataList.Add(new StdDbColumn("介護役割", StdDbType.VARCHAR2, this.Care));
            obj.DataList.Add(new StdDbColumn("備考", StdDbType.VARCHAR2, this.Cont));
            obj.DataList.Add(new StdDbColumn("更新日", StdDbType.NUMBER, reg_date));
            obj.DataList.Add(new StdDbColumn("更新時間", StdDbType.NUMBER, reg_time));
            obj.DataList.Add(new StdDbColumn("更新者", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("代行更新者", StdDbType.NUMBER, LoginUser.Id2));

            obj.WhereList.Add("患者コード = " + this.PtId);
            obj.WhereList.Add("連番 = " + this.SEQ);

            sr = obj.UpdateSQL();
#endif
            return sr;
        }


        public StdReturn Delete()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
#if INNO
#else
            obj.Db = DB.Db1;
            obj.Table = "ADT_患者家族連絡先データ";

            obj.WhereList.Add("患者コード = " + this.PtId);
            obj.WhereList.Add("連番 = " + this.SEQ);

            sr = obj.DeleteSQL();
#endif
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
