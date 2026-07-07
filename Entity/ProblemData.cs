using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// プロブレムデータ
    /// </summary>
    public class ProblemData : StdKarte1
    {
        /// <summary>
        /// プロブレムＮＯ
        /// </summary>
        public int SEQ = 0;

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

                if (this.InOut.Equals("1"))
                {
                    s = "外来";
                }
                else if (this.InOut.Equals("2"))
                {
                    s = "入院";
                }
                else if (this.InOut.Equals("0"))
                {
                    s = "共用";
                }

                return s;
            }
        }

        /// <summary>
        /// 発生日
        /// </summary>
        public string Date1 = "";

        public string DateString1
        {
            get
            {
                return DateTimeAgent.DateFormat(this.Date1, DateTimeAgent.DateFormatKind.LONG);
            }
        }

        public DateTime DateValue1
        {
            get
            {
                if (this.Date1.Length != 8)
                {
                    return DateTime.Now;
                }
                else
                {
                    DateTime dt = DateTime.Now;

                    DateTime.TryParse(DateTimeAgent.DateFormat(this.Date1, DateTimeAgent.DateFormatKind.LONG), out dt);

                    return dt;
                }
            }
        }

        /// <summary>
        /// 発生科コード
        /// </summary>
        public string DeptCode1 = "";

        /// <summary>
        /// 発生科
        /// </summary>
        public string DeptName1
        {
            get
            {
                string s = "";

                if (Dict.DeptDict.ContainsKey(this.DeptCode1))
                {
                    s = Dict.DeptDict[this.DeptCode1].ShortName;
                }

                return s;
            }
        }

        /// <summary>
        /// 発生登録者コード
        /// </summary>
        public string StaffCode1 = "";

        /// <summary>
        /// 発生登録者
        /// </summary>
        public string StaffName1
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(this.StaffCode1))
                {
                    s = Dict.StaffDict[this.StaffCode1].Name;
                }

                return s;
            }
        }

        /// <summary>
        /// 資格コード
        /// 実際は所属コードと思われる
        /// </summary>
        public string SectionCode1 = "";

        public string SectionName1
        {
            get
            {
                string s = "";

                if (Dict.SectionDict.ContainsKey(this.SectionCode1))
                {
                    s = Dict.SectionDict[this.SectionCode1].FullName;
                }

                return s;
            }
        }

        /// <summary>
        /// 解決日
        /// </summary>
        public string Date2 = "";

        public string DateString2
        {
            get
            {
                return DateTimeAgent.DateFormat(this.Date2, DateTimeAgent.DateFormatKind.LONG);
            }
        }

        public DateTime DateValue2
        {
            get
            {
                if (this.Date2.Length != 8)
                {
                    return DateTime.Now;
                }
                else
                {
                    DateTime dt = DateTime.Now;

                    DateTime.TryParse(DateTimeAgent.DateFormat(this.Date2, DateTimeAgent.DateFormatKind.LONG), out dt);

                    return dt;
                }
            }
        }

        /// <summary>
        /// 解決科コード
        /// </summary>
        public string DeptCode2 = "";

        /// <summary>
        /// 解決科
        /// </summary>
        public string DeptName2
        {
            get
            {
                string s = "";

                if (Dict.DeptDict.ContainsKey(this.DeptCode2))
                {
                    s = Dict.DeptDict[this.DeptCode2].ShortName;
                }

                return s;
            }
        }

        /// <summary>
        /// 解決登録者コード
        /// </summary>
        public string StaffCode2 = "";

        /// <summary>
        /// 解決登録者
        /// </summary>
        public string StaffName2
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(this.StaffCode2))
                {
                    s = Dict.StaffDict[this.StaffCode2].Name;
                }

                return s;
            }
        }

        /// <summary>
        /// 問題内容
        /// </summary>
        public string Cont = "";

        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DeleteFlg = false;


        public static ProblemData Load(string pt_id, string problem_no)
        {
            ProblemData obj = new ProblemData();

            if (pt_id.Length == 0 || problem_no.Length == 0)
            {
                return obj;
            }

            string cmd = "select * from D_PROBLEM t " +
                " where t.P_ID = " + pt_id +
                " and t.PROBLEM_NO = " + problem_no;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                obj = GetFromStdClass(tmp);
                break;
            }

            return obj;
        }

        public static List<ProblemData> GetList(string pt_id)
        {
            List<ProblemData> list = new List<ProblemData>();

            if (pt_id.Length == 0)
            {
                return list;
            }

            string cmd = "select * from D_PROBLEM t " +
                " where t.P_ID = " + pt_id +
                " order by t.PROBLEM_NO desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }

        /// <summary>
        /// プロブレムＮＯの最大値を取得する
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="in_out"></param>
        /// <returns></returns>
        public static int GetMaxSEQ(string pt_id, string in_out)
        {
            int seq = 0;

            if (pt_id.Length == 0 || in_out.Length == 0)
            {
                return seq;
            }


            return seq;
        }


        static ProblemData GetFromStdClass(StdClass tmp)
        {
            ProblemData obj = new ProblemData();

            obj.InOut = tmp.DataDict["INOUT"].ToString();
            int.TryParse(tmp.DataDict["PROBLEM_NO"].ToString(), out obj.SEQ);
            obj.Date1 = tmp.DataDict["PROBLEM_DATE"].ToString();
            obj.DeptCode1 = tmp.DataDict["DEPT"].ToString();
            obj.StaffCode1 = tmp.DataDict["REG_USR"].ToString();
            obj.SectionCode1 = tmp.DataDict["SHIKAKU"].ToString();
            obj.Date2 = tmp.DataDict["RESOLUTION_DATE"].ToString();
            obj.DeptCode2 = tmp.DataDict["DEPT"].ToString();
            obj.StaffCode2 = tmp.DataDict["UP_USR"].ToString();
            obj.Cont = tmp.DataDict["PROBLEM_TITLE"].ToString() + " " + tmp.DataDict["PROBLEM_TEXT"].ToString();
            obj.DeleteFlg = tmp.DataDict["DEL_FLG"].ToString().Equals("1") ? true : false;
            obj.BaseFromStdClass(tmp);

            return obj;
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
}
