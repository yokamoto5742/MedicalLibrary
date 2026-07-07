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

#if INNO
            string cmd = "select * from D_PROBLEM t " +
                " where t.P_ID = " + pt_id +
                " and t.PROBLEM_NO = " + problem_no;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
#else
            string cmd = "select * from macs.ADT_プロブレムデータ t " +
                " where t.患者コード = " + pt_id +
                " and t.プロブレムＮＯ = " + problem_no;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif

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

#if INNO
            string cmd = "select * from D_PROBLEM t " +
                " where t.P_ID = " + pt_id +
                " order by t.PROBLEM_NO desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
#else
            string cmd = "select * from macs.ADT_プロブレムデータ t " +
                " where t.患者コード = " + pt_id +
                " order by t.プロブレムＮＯ desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif

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

#if INNO
#else
            string cmd = "select max(プロブレムＮＯ) ＮＯ from ADT_プロブレムデータ t " +
                " where t.患者コード = " + pt_id +
                " and t.入外区分 = " + in_out;

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                if (tmp.DataDict["ＮＯ"].ToString().Length > 0)
                {
                    seq = int.Parse(tmp.DataDict["ＮＯ"].ToString());
                }

                break;
            }
#endif

            return seq;
        }


        static ProblemData GetFromStdClass(StdClass tmp)
        {
            ProblemData obj = new ProblemData();

#if INNO
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
#else
            obj.InOut = tmp.DataDict["入外区分"].ToString();
            int.TryParse(tmp.DataDict["プロブレムＮＯ"].ToString(), out obj.SEQ);
            obj.Date1 = tmp.DataDict["発生日"].ToString();
            obj.DeptCode1 = tmp.DataDict["発生登録科"].ToString();
            obj.StaffCode1 = tmp.DataDict["発生登録者"].ToString();
            obj.SectionCode1 = tmp.DataDict["資格コード"].ToString();
            obj.Date2 = tmp.DataDict["解決日"].ToString();
            obj.DeptCode2 = tmp.DataDict["解決登録科"].ToString();
            obj.StaffCode2 = tmp.DataDict["解決登録者"].ToString();
            obj.Cont = tmp.DataDict["問題内容"].ToString();
            obj.DeleteFlg = tmp.DataDict["削除フラグ"].ToString().Equals("1") ? true : false;
#endif
            obj.BaseFromStdClass(tmp);

            return obj;
        }

        public StdReturn Insert()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();

#if INNO
#else
            obj.Db = DB.Db1;
            obj.Table = "ADT_プロブレムデータ";

            // 連番と表示順を取得する
            this.SEQ = ProblemData.GetMaxSEQ(this.PtId, this.InOut) + 1;

            obj.DataList.Add(new StdDbColumn("患者コード", StdDbType.NUMBER, this.PtId));
            obj.DataList.Add(new StdDbColumn("入外区分", StdDbType.NUMBER, this.InOut));
            obj.DataList.Add(new StdDbColumn("プロブレムＮＯ", StdDbType.NUMBER, this.SEQ));

            obj.DataList.Add(new StdDbColumn("発生日", StdDbType.NUMBER, this.Date1));
            obj.DataList.Add(new StdDbColumn("発生登録科", StdDbType.NUMBER, this.DeptCode1));

            obj.DataList.Add(new StdDbColumn("発生登録者", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("資格コード", StdDbType.NUMBER, LoginUser.SectionId));

            obj.DataList.Add(new StdDbColumn("問題内容", StdDbType.VARCHAR2, this.Cont));

            obj.DataList.Add(new StdDbColumn("解決日", StdDbType.NUMBER, this.Date2));
            obj.DataList.Add(new StdDbColumn("解決登録科", StdDbType.NUMBER, this.DeptCode2));
            obj.DataList.Add(new StdDbColumn("解決登録者", StdDbType.NUMBER, this.StaffCode2));

            obj.DataList.Add(new StdDbColumn("登録日", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("登録時間", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("登録者", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("代行登録者", StdDbType.NUMBER, LoginUser.Id2));

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
            obj.Table = "ADT_プロブレムデータ";

            obj.DataList.Add(new StdDbColumn("発生日", StdDbType.NUMBER, this.Date1));
            obj.DataList.Add(new StdDbColumn("発生登録科", StdDbType.NUMBER, this.DeptCode1));

            // 発生登録者と資格コードは変更できない
            /*
            obj.DataList.Add(new StdDbColumn("発生登録者", StdDbType.NUMBER, this.StaffCode1));
            obj.DataList.Add(new StdDbColumn("資格コード", StdDbType.NUMBER, this.SectionCode1));
            */

            obj.DataList.Add(new StdDbColumn("問題内容", StdDbType.VARCHAR2, this.Cont));

            obj.DataList.Add(new StdDbColumn("解決日", StdDbType.NUMBER, this.Date2));
            obj.DataList.Add(new StdDbColumn("解決登録科", StdDbType.NUMBER, this.DeptCode2));
            obj.DataList.Add(new StdDbColumn("解決登録者", StdDbType.NUMBER, this.StaffCode2));

            obj.DataList.Add(new StdDbColumn("更新日", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("更新時間", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("更新者", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("代行更新者", StdDbType.NUMBER, LoginUser.Id2));

            obj.WhereList.Add("患者コード = " + this.PtId);
            obj.WhereList.Add("入外区分 = " + this.InOut);
            obj.WhereList.Add("プロブレムＮＯ = " + this.SEQ);

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
            obj.Table = "ADT_プロブレムデータ";

            obj.DataList.Add(new StdDbColumn("削除フラグ", StdDbType.NUMBER, 1));

            obj.DataList.Add(new StdDbColumn("更新日", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("更新時間", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("更新者", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("代行更新者", StdDbType.NUMBER, LoginUser.Id2));

            obj.WhereList.Add("患者コード = " + this.PtId);
            obj.WhereList.Add("入外区分 = " + this.InOut);
            obj.WhereList.Add("プロブレムＮＯ = " + this.SEQ);

            sr = obj.UpdateSQL();
#endif
            return sr;
        }
    }
}
