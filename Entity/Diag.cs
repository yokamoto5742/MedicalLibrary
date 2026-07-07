using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class Diag : StdKarte2
    {
        /// <summary>
        /// 保険パターン
        /// 1 健保, 2 労災, 3 自賠責, 4 自費, 5 健診
        /// </summary>
        public class DiagIns
        {
            public string Code = "";

            public string Name = "";

            public override string ToString()
            {
                return this.Name;
            }

            public DiagIns(string code = "", string name = "")
            {
                this.Code = code;
                this.Name = name;
            }

            static Dictionary<string, DiagIns> dict = new Dictionary<string, DiagIns>();

            public static Dictionary<string, DiagIns> Dict
            {
                get
                {
                    if (dict.Count == 0)
                    {
                        dict.Add("0", new DiagIns("0", ""));
                    }

                    return dict;
                }
            }
        }

        /// <summary>
        /// 転帰種別
        /// 1 治癒, 2 死亡, 3 中止, 4 保険変更
        /// </summary>
        public class DiagOutcome
        {
            public string Code = "";

            public string Name = "";

            public string Short = "";

            public override string ToString()
            {
                return this.Name;
            }

            public DiagOutcome(string code = "", string name = "", string _short = "")
            {
                this.Code = code;
                this.Name = name;
                this.Short = _short;
            }

            static Dictionary<string, DiagOutcome> dict = new Dictionary<string, DiagOutcome>();

            public static Dictionary<string, DiagOutcome> Dict
            {
                get
                {
                    if (dict.Count == 0)
                    {
                        dict.Add("0", new DiagOutcome("0", ""));
                        dict.Add("1", new DiagOutcome("1", "治癒", "治癒"));
                        dict.Add("2", new DiagOutcome("2", "死亡", "死亡"));
                        dict.Add("3", new DiagOutcome("3", "中止", "中止"));
                        dict.Add("4", new DiagOutcome("4", "保険変更", "保変"));
                    }

                    return dict;
                }
            }
        }

        /// <summary>
        /// 保険パターン
        /// </summary>
        public string Ins = "";

        /// <summary>
        /// 保険区分
        /// MACS では 1～5
        /// inno では 1～99
        /// </summary>
        public string InsKind = "";

        public string InsKindName
        {
            get
            {
                string s = "";
                if (Insurance.Dict.ContainsKey(this.InsKind))
                {
                    s = Insurance.Dict[this.InsKind].ShortName;
                }
                return s;
            }
        }

        /// <summary>
        /// 入外区分
        /// 0 共用, 1 外来, 2 入院
        /// </summary>
        public class DiagInOut
        {
            public string Code = "";

            public string Name = "";

            public string Short = "";

            public override string ToString()
            {
                return this.Name;
            }

            public DiagInOut(string code = "", string name = "", string _short = "")
            {
                this.Code = code;
                this.Name = name;
                this.Short = _short;
            }

            static Dictionary<string, DiagInOut> dict = new Dictionary<string, DiagInOut>();

            public static Dictionary<string, DiagInOut> Dict
            {
                get
                {
                    if (dict.Count == 0)
                    {
                        dict.Add("0", new DiagInOut("0", "共用", "共"));
                        dict.Add("1", new DiagInOut("1", "外来", "外"));
                        dict.Add("2", new DiagInOut("2", "入院", "入"));
                    }

                    return dict;
                }
            }
        }

        /// <summary>
        /// 入外区分
        /// </summary>
        public string InOut = "";

        /// <summary>
        /// 入外区分
        /// </summary>
        public string InOutString
        {
            get
            {
                string s = "";

                if (DiagInOut.Dict.ContainsKey(this.InOut))
                {
                    s = DiagInOut.Dict[this.InOut].Name;
                }

                return s;
            }
        }

        /// <summary>
        /// 入外区分
        /// </summary>
        public string InOutStringShort
        {
            get
            {
                string s = "";

                if (DiagInOut.Dict.ContainsKey(this.InOut))
                {
                    s = DiagInOut.Dict[this.InOut].Short;
                }

                return s;
            }
        }

        /// <summary>
        /// 科コード
        /// </summary>
        public string Dept = "";

        public string DeptName
        {
            get
            {
                return Dict.DeptDict.ContainsKey(this.Dept) ? Dict.DeptDict[this.Dept].ShortName : "";
            }
        }

        /// <summary>
        /// 開始日
        /// </summary>
        public string StartDate = "";

        public DateTime StartDateValue
        {
            get
            {
                if (!AppString.IsDate(this.StartDate))
                {
                    return DateTime.Now;
                }
                else
                {
                    DateTime dt = DateTime.Now;

                    DateTime.TryParse(DateTimeAgent.DateFormat(this.StartDate, DateTimeAgent.DateFormatKind.LONG), out dt);

                    return dt;
                }
            }
        }

        /// <summary>
        /// 連番
        /// </summary>
        public int SEQ = 0;

        /// <summary>
        /// 主病名フラグ
        /// </summary>
        public bool MainFlg = false;

        /// <summary>
        /// 告知フラグ
        /// </summary>
        public bool NoticeFlg = false;

        /// <summary>
        /// 保険病名フラグ
        /// </summary>
        public bool InsFlg = false;

        /// <summary>
        /// 接頭語０１～１５
        /// </summary>
        public string[] PrefixList = new string[16];

        /// <summary>
        /// 接尾語０１～０５
        /// </summary>
        public string[] SuffixList = new string[6];

        /// <summary>
        /// 修飾語の連結リスト
        /// </summary>
        public string PreSuffixString(string delimiter = " ")
        {
            string s = "";

            // 接頭辞
            foreach (string ss in this.PrefixList)
            {
                if (ss == null) continue;
                if (!ss.Length.Equals(4)) continue;

                if (s.Length > 0) s += delimiter;

                s += ss;
            }

            // 接尾辞
            foreach (string ss in this.SuffixList)
            {
                if (ss == null) continue;
                if (!ss.Length.Equals(4)) continue;

                if (s.Length > 0) s += delimiter;

                s += ss;
            }

            return s;
        }

        /// <summary>
        /// 病名コード（レセ電算）
        /// </summary>
        public string DiagCode = "";

        /// <summary>
        /// ICD10コード（病名マスターから取得） 
        /// </summary>
        public string ICDCode1 = "";

        /// <summary>
        /// ICD10補助コード（病名マスターから取得） 
        /// </summary>
        public string ICDCode2 = "";

        /// <summary>
        /// 病名
        /// </summary>
        public string DiagName = "";

        /// <summary>
        /// 本体病名
        /// </summary>
        public string MainName = "";

        /// <summary>
        /// 付加コード
        /// </summary>
        public string PlusCode = "";

        /// <summary>
        /// 医師コード
        /// </summary>
        public string Doctor = "";

        public string DoctorName
        {
            get
            {
                return Dict.DoctorDict.ContainsKey(this.Doctor) ? Dict.DoctorDict[this.Doctor].Name : "";
            }
        }

        /// <summary>
        /// 確定フラグ
        /// </summary>
        public bool FixFlg = false;

        /// <summary>
        /// 確定日
        /// </summary>
        public int FixDate = 0;

        /// <summary>
        /// 転帰区分
        /// </summary>
        public string OutcomeCode = "";

        /// <summary>
        /// 転帰名称
        /// </summary>
        public string OutcomeString
        {
            get
            {
                string s = "";

                if (DiagOutcome.Dict.ContainsKey(this.OutcomeCode))
                {
                    s = DiagOutcome.Dict[this.OutcomeCode].Name;
                }

                return s;
            }
        }

        /// <summary>
        /// 転帰名称
        /// </summary>
        public string OutcomeStringShort
        {
            get
            {
                string s = "";

                if (DiagOutcome.Dict.ContainsKey(this.OutcomeCode))
                {
                    s = DiagOutcome.Dict[this.OutcomeCode].Short;
                }

                return s;
            }
        }

        /// <summary>
        /// 転帰日
        /// </summary>
        public string OutcomeDate = "";

        public DateTime OutcomeDateValue
        {
            get
            {
                if (!AppString.IsDate(this.OutcomeDate))
                {
                    return DateTime.Now;
                }
                else
                {
                    DateTime dt = DateTime.Now;

                    DateTime.TryParse(DateTimeAgent.DateFormat(this.OutcomeDate, DateTimeAgent.DateFormatKind.LONG), out dt);

                    return dt;
                }
            }
        }

        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DeleteFlg = false;

/*
        public static Diag Load(string pt_id, string ins_kind, string in_out, string dept_code, string start_date, string seq)
        {
            Diag obj = new Diag();

            if (pt_id.Length == 0 || ins_kind.Length == 0 || in_out.Length == 0 ||
                dept_code.Length == 0 || start_date.Length != 8 || seq.Length == 0)
            {
                return obj;
            }

            string cmd = "select t.*, t2.IM73RC_F13 from macs.ADT_病名データ t, macs.IM73RC t2 " +
                " where t.患者コード = " + pt_id +
                " and t.保険パターン = " + ins_kind +
                " and t.入外区分 = " + in_out +
                " and t.科コード = " + dept_code +
                " and t.開始日 = " + start_date +
                " and t.連番 = " + seq +
                " and t.病名コード = t2.IM73RC_F02(+) ";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                obj = GetFromStdClass(tmp);
                break;
            }

            return obj;
        }
*/

        public static Diag GetData(string pt_id, int seq)
        {
            Diag obj = new Diag();

            if (!AppString.IsNumber(pt_id)) return obj;

            string cmd = "select td.*, tm.FLD_06, tm.FLD_16, tm.FLD_17, th.HOKEN_TYPE " +
                " from D_BYOUMEI td, M_BYOUMEI tm, M_PATIENT_HOKEN th" +
                " where td.P_ID = " + pt_id + "and td.SEQ = " + seq +
                " and td.P_ID = th.P_ID and td.P_HOKEN = th.P_HOKEN " +
                " and td.CODE = tm.CODE(+) ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                obj = GetFromStdClass(tmp);
                break;
            }

            return obj;
        }

        /// <summary>
        /// 病名リストを取得する
        /// </summary>
        /// <param name="pt_id"></param>
        /// <returns></returns>
        public static List<Diag> GetList(string pt_id)
        {
            List<Diag> list = new List<Diag>();

            if (!AppString.IsNumber(pt_id))
            {
                return list;
            }
            // 病名データの取得
            string cmd = "select td.*, tm.FLD_06, tm.FLD_16, tm.FLD_17, th.HOKEN_TYPE " +
                " from D_BYOUMEI td, M_BYOUMEI tm, M_PATIENT_HOKEN th" +
                " where td.P_ID = " + pt_id + " and (td.DEL_FLG is null or td.DEL_FLG = 0) " +
                " and td.P_ID = th.P_ID and td.P_HOKEN = th.P_HOKEN " +
                " and td.CODE = tm.CODE(+) ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }


        /// <summary>
        /// 連番の最大値を取得する
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="ins_kind"></param>
        /// <param name="in_out"></param>
        /// <param name="dept_code"></param>
        /// <param name="start_date"></param>
        /// <returns></returns>
        public static int GetMaxSEQ(string pt_id, string ins_kind, string in_out, string dept_code, int start_date)
        {
            int i = 0;

            if (pt_id.Length == 0 || ins_kind.Length == 0 || in_out.Length == 0 || dept_code.Length == 0 || start_date.ToString().Length != 8)
            {
                return i;
            }

            return i;
        }


        /// <summary>
        /// 該当日の外来患者の未転帰病名
        /// </summary>
        /// <param name="do_date"></param>
        /// <param name="in_out"></param>
        /// <param name="dept"></param>
        /// <returns></returns>
        public static List<Diag> GetYetListByOutDate(string do_date, string dept)
        {
            List<Diag> list = new List<Diag>();

            if (do_date.Length != 8)
            {
                return list;
            }
            string DeptSql = "";

            if (dept.Length > 0)
            {
                DeptSql = " and DEPT = " + dept;
            }

            string cmd = "select td.*, tm.FLD_06, tm.FLD_16, tm.FLD_17, th.HOKEN_TYPE " +
                " from D_BYOUMEI td, M_BYOUMEI tm, M_PATIENT_HOKEN th " +
                " where (td.P_ID, td.DEPT) in " +
                " (select ts.P_ID " +
                " , case when ts.CHANGE_DEPT > 0 then ts.CHANGE_DEPT else ts.DEPT end DEPT " +
                " from D_UKETSUKE ts " +
                " where ts.UKE_DATE = " + do_date + ") " +
                " and td.INOUT in (0,1) " + DeptSql +
                " and (td.DEL_FLG is null or td.DEL_FLG = 0) " +
                " and td.DATE_S <= " + do_date + " and (td.TENKI_DATE is null or td.TENKI_DATE = 0 or td.TENKI_DATE >= " + do_date + ") " +
                " and td.P_ID = th.P_ID and td.P_HOKEN = th.P_HOKEN " +
                " and td.CODE = tm.CODE(+) ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }

        /// <summary>
        /// 病名を期間指定（１年間以内）で検索する
        /// </summary>
        /// <param name="diag_list">病名またはコードのリスト</param>
        /// <param name="date1">開始日</param>
        /// <param name="date2">終了日</param>
        /// <param name="doubt">true: 疑い病名を含む, false: 疑い病名を除く</param>
        /// <param name="pat_info">true: 患者情報（氏名・性別・生年月日）を取得する</param>
        /// <param name="date_type">1: 開始日, 2: 登録日, 3: 未転帰</param>
        /// <param name="pt_list">対象患者リスト</param>
        /// <returns></returns>
        public static List<Diag> GetListByDiagNamesDates(List<string> diag_list, int date1, int date2, bool doubt = false, bool pat_info = false, int date_type = 1, List<string> pt_list = null)
        {
            List<Diag> list = new List<Diag>();

            DateTime dt1 = DateTimeAgent.DateTimeFromInt(date1);
            DateTime dt2 = DateTimeAgent.DateTimeFromInt(date2);

            // 終了日が開始日より１年以上後ならば１年後に変更する
            if (dt1.AddYears(1) < dt2)
            {
                dt2 = dt1.AddYears(1);
            }

            List<string> ss = new List<string>();
            List<string> ss_doubt = new List<string>();

            foreach (string s in diag_list)
            {
                if (s.Length == 0)
                {
                    continue;
                }

                if (AppString.IsNumber(s))
                {
                    ss.Add("(td.CODE = " + s + ")");
                }
                else
                {
                    ss.Add("(BYOUMEI_COMMENT like '%" + s + "%')");
                }
            }

            if (ss.Count == 0)
            {
                return list;
            }

            string cmd = "";

            string pt_str = "";

            if (pt_list != null && AppString.ConcatList(pt_list, ",").Length > 0)
            {
                pt_str = " and td.P_ID in (" + AppString.ConcatList(pt_list, ",") + ")";
            }

            string date_str = "";

            switch (date_type)
            {
                case 1:
                    date_str = " and td.DATE_S >= " + dt1.ToString("yyyyMMdd") + " and td.DATE_S <= " + dt2.ToString("yyyyMMdd");
                    break;

                case 2:
                    date_str = " and td.REG_DATE >= " + dt1.ToString("yyyyMMdd") + " and td.REG_DATE <= " + dt2.ToString("yyyyMMdd");
                    break;

                case 3:
                    date_str = " and (td.TENKI_TYPE is null or td.TENKI_TYPE = 0)";
                    break;
            }

            if (pat_info)
            {
                cmd = "select td.*, tm.FLD_06, tm.FLD_16, tm.FLD_17, th.HOKEN_TYPE " +
                    " , m.P_NAME, m.P_KANA, m.P_SEX, m.P_BIRTHDAY_AD " +
                    " from D_BYOUMEI td, M_BYOUMEI tm, M_PATIENT_HOKEN th, M_PATIENT m " +
                    " where (td.DEL_FLG is null or td.DEL_FLG = 0) and (" + AppString.ConcatList(ss, " or ") + ") " +
                    pt_str +
                    date_str +
                    " and td.P_ID = th.P_ID and td.P_HOKEN = th.P_HOKEN " +
                    " and td.CODE = tm.CODE(+) " +
                    " and td.P_ID = m.P_ID";
            }
            else
            {
                cmd = "select td.*, tm.FLD_06, tm.FLD_16, tm.FLD_17, th.HOKEN_TYPE " +
                    " from D_BYOUMEI td, M_BYOUMEI tm, M_PATIENT_HOKEN th " +
                    " where (td.DEL_FLG is null or td.DEL_FLG = 0) and (" + AppString.ConcatList(ss, " or ") + ") " +
                    pt_str +
                    date_str +
                    " and td.P_ID = th.P_ID and td.P_HOKEN = th.P_HOKEN " +
                    " and td.CODE = tm.CODE(+) ";
            }

            // 疑い病名を除く場合
            if (!doubt)
            {
                for (int i = 1; i <= 5; i++)
                {
                    cmd += " and (td.SUFFIX_0" + i + " is null or td.SUFFIX_0" + i + " != 8002)";
                }
            }

            cmd += " order by td.DATE_S desc, td.P_ID";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                Diag obj = Diag.GetFromStdClass(tmp);

                // 疑い病名を除く場合
                if (!doubt)
                {
                    if (obj.DiagName.Contains("の疑い"))
                    {
                        continue;
                    }
                }

                list.Add(obj);
            }

            return list;
        }


        static Diag GetFromStdClass(StdClass tmp)
        {
            Diag obj = new Diag();

            obj.BaseFromStdClass(tmp);
            obj._Pat.Id = tmp.GetDataString("P_ID");
            obj._Pat.Name = tmp.GetDataString("P_NAME");
            obj._Pat.Kana = tmp.GetDataString("P_KANA");
            obj._Pat.Sex = tmp.GetDataString("P_SEX");
            obj._Pat.Birth = tmp.GetDataString("P_BIRTHDAY_AD");

            obj.PtId = tmp.GetDataString("P_ID");
            obj.Ins = tmp.GetDataString("P_HOKEN");
            obj.InsKind = tmp.GetDataString("HOKEN_TYPE");
            obj.InOut = tmp.GetDataString("INOUT");
            obj.Dept = tmp.GetDataString("DEPT");
            obj.StartDate = tmp.GetDataString("DATE_S");
            obj.SEQ = tmp.GetDataInt("SEQ");
            obj.MainFlg = tmp.GetDataString("MAIN_FLG").Equals("1");
            obj.NoticeFlg = tmp.GetDataString("INFORM_FLG").Equals("1");
            obj.InsFlg = tmp.GetDataString("HOKEN_FLG").Equals("1");

            for (int i = 1; i <= 15; i++)
            {
                if (tmp.GetDataString("PREFIX_" + i.ToString().PadLeft(2, '0')).Length == 4)
                {
                    obj.PrefixList[i] = tmp.GetDataString("PREFIX_" + i.ToString().PadLeft(2, '0'));
                }
                else
                {
                    obj.PrefixList[i] = "";
                }
            }

            for (int i = 1; i <= 5; i++)
            {
                if (tmp.GetDataString("SUFFIX_" + i.ToString().PadLeft(2, '0')).Length == 4)
                {
                    obj.SuffixList[i] = tmp.GetDataString("SUFFIX_" + i.ToString().PadLeft(2, '0'));
                }
                else
                {
                    obj.SuffixList[i] = "";
                }
            }

            obj.DiagCode = tmp.GetDataString("CODE");
            obj.MainName = tmp.GetDataString("FLD_06").Trim();
            obj.ICDCode1 = tmp.GetDataString("FLD_16");
            obj.ICDCode2 = tmp.GetDataString("FLD_17");
            obj.DiagName = tmp.GetDataString("BYOUMEI_COMMENT");
            obj.PlusCode = tmp.GetDataString("HUKA_CODE").Length > 0 && !tmp.GetDataString("HUKA_CODE").Equals("0") ? tmp.GetDataString("HUKA_CODE") : "";
            obj.Doctor = tmp.GetDataString("DR");
            obj.FixDate = tmp.GetDataInt("ADMIT_DATE");
            obj.OutcomeCode = tmp.GetDataString("TENKI_TYPE");
            obj.OutcomeDate = tmp.GetDataString("TENKI_DATE");
            obj.DeleteFlg = tmp.GetDataInt("DEL_FLG").Equals(1);

            obj.FixFlg = tmp.GetDataInt("ADMIT_TYPE", 0).Equals(1);
            return obj;
        }

        public static bool IsKeyDifferent(Diag diag1, Diag diag2)
        {
            bool b = false;
            if (!diag1.PtId.Equals(diag2.PtId) ||
                !diag1.SEQ.Equals(diag2.SEQ))
            {
                b = true;
            }
            return b;
        }

        public StdReturn Insert()
        {
            StdReturn sr = new StdReturn();

            if (this.PtId.Length == 0 || this.InsKind.Length == 0 || this.InOut.Length == 0 ||
                this.Dept.Length == 0 || !AppString.IsDate(this.StartDate))
            {
                return sr;
            }

            StdDbClass obj = new StdDbClass();

            return sr;
        }

        public StdReturn Update()
        {
            StdReturn sr = new StdReturn();

            if (this.PtId.Length == 0 || this.InsKind.Length == 0 || this.InOut.Length == 0 ||
                this.Dept.Length == 0 || !AppString.IsDate(this.StartDate) || this.SEQ == 0)
            {
                return sr;
            }

            StdDbClass obj = new StdDbClass();

            return sr;
        }

        public StdReturn Delete()
        {
            StdReturn sr = new StdReturn();

            if (this.PtId.Length == 0 || this.InsKind.Length == 0 || this.InOut.Length == 0 ||
                this.Dept.Length == 0 || !AppString.IsDate(this.StartDate) || this.SEQ == 0)
            {
                return sr;
            }

            StdDbClass obj = new StdDbClass();

            return sr;
        }
    }
}
