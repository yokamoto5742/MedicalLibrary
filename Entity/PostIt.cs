using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// 付箋
    /// 入外区分は存在するが、実際は外来でしか使われていない。
    /// </summary>
    public class PostIt : StdKarte1
    {
        /// <summary>
        /// 入外区分
        /// 実際は外来でしか使われていない
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
        /// 科コード
        /// 外来では受付科となる
        /// </summary>
        public string DeptCode = "";

        /// <summary>
        /// 科
        /// </summary>
        public string DeptName
        {
            get
            {
                string s = "";

                if (Dict.DeptDict.ContainsKey(this.DeptCode))
                {
                    s = Dict.DeptDict[this.DeptCode].ShortName;
                }

                return s;
            }
        }

        /// <summary>
        /// 対象日
        /// </summary>
        public int DoDate = 0;

        public DateTime DoDateValue
        {
            get
            {
                if (this.DoDate.ToString().Length != 8)
                {
                    return DateTime.Now;
                }
                else
                {
                    DateTime dt = DateTime.Now;

                    DateTime.TryParse(DateTimeAgent.DateFormat(this.DoDate, DateTimeAgent.DateFormatKind.LONG), out dt);

                    return dt;
                }
            }
        }

        /// <summary>
        /// 連番
        /// </summary>
        public int SEQ1 = 1;

        /// <summary>
        /// 表示順
        /// </summary>
        public int SEQ2 = 1;

        /// <summary>
        /// 付箋内容
        /// </summary>
        public string Cont1 = "";

        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DeleteFlg = false;


        public static PostIt Load(string pt_id, string in_out, string dept_code, string do_date, string seq1)
        {
            PostIt obj = new PostIt();

            if (pt_id.Length == 0 || in_out.Length == 0 || dept_code.Length == 0 ||
                do_date.Length != 8 || seq1.Length == 0)
            {
                return obj;
            }

            string cmd = "select * from D_KARTE_TAG t " +
                " where t.P_ID = " + pt_id +
                " and t.INOUT = " + in_out +
                " and t.DEPT = " + dept_code +
                " and t.TAG_DATE = " + do_date +
                " and t.SEQ = " + seq1;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                obj = GetFromStdClass(tmp);
                break;
            }

            return obj;
        }


        public static List<PostIt> GetList(string pt_id)
        {
            List<PostIt> list = new List<PostIt>();

            if (pt_id.Length == 0)
            {
                return list;
            }

            // 削除されたものも含めて取得する
            string cmd = "select * from D_KARTE_TAG t " +
                " where t.P_ID = " + pt_id +
                " order by t.INOUT, t.DEPT, t.TAG_DATE desc, t.DISP_SEQ";
            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }


        public static List<PostIt> GetListByDate(string do_date, string in_out)
        {
            List<PostIt> list = new List<PostIt>();

            if (do_date.Length != 8)
            {
                return list;
            }

            string InOutSql = "";

            if (in_out.Length > 0)
            {
                InOutSql = " and t.INOUT = " + in_out;
            }

            // 削除されたものは飛ばす
            string cmd = "select * from D_KARTE_TAG t " +
                " where t.TAG_DATE = " + do_date + InOutSql +
                " and (t.DEL_FLG is null or t.DEL_FLG = 0) " +
                " order by t.P_ID, t.INOUT, t.DEPT, t.TAG_DATE desc, t.DISP_SEQ";

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
        /// <param name="in_out"></param>
        /// <param name="dept"></param>
        /// <param name="do_date"></param>
        /// <returns></returns>
        public static int GetMaxSEQ1(string pt_id, string in_out, string dept, string do_date)
        {
            int seq = 0;

            if (pt_id.Length == 0 || in_out.Length == 0 || dept.Length == 0 || do_date.Length != 8)
            {
                return seq;
            }

            string cmd = "select max(SEQ) 連番 from D_KARTE_TAG t " +
                " where t.P_ID = " + pt_id +
                " and t.INOUT = " + in_out +
                " and t.DEPT = " + dept +
                " and t.TAG_DATE = " + do_date;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                seq = tmp.GetDataInt("連番", 0);
                break;
            }

            return seq;
        }

        static PostIt GetFromStdClass(StdClass tmp)
        {
            PostIt obj = new PostIt();

            obj.BaseFromStdClass(tmp);

            obj.InOut = tmp.GetDataString("INOUT");
            obj.DeptCode = tmp.GetDataString("DEPT");
            obj.DoDate = tmp.GetDataInt("TAG_DATE");
            obj.SEQ1 = tmp.GetDataInt("SEQ");
            obj.SEQ2 = tmp.GetDataInt("DISP_SEQ");
            obj.Cont1 = tmp.GetDataString("TAG_COMMENT");
            obj.DeleteFlg = tmp.GetDataString("DEL_FLG").Equals("1") ? true : false;

            return obj;
        }

        public StdReturn Insert()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();

            // 連番と表示順を取得する
            this.SEQ1 = PostIt.GetMaxSEQ1(this.PtId, this.InOut, this.DeptCode, this.DoDate.ToString()) + 1;
            this.SEQ2 = PostIt.GetMaxSEQ1(this.PtId, this.InOut, this.DeptCode, this.DoDate.ToString()) + 1;

            obj.Db = DB.Db3;
            obj.Table = "D_KARTE_TAG";

            obj.DataList.Add(new StdDbColumn("P_ID", StdDbType.NUMBER, this.PtId));
            obj.DataList.Add(new StdDbColumn("INOUT", StdDbType.NUMBER, this.InOut));
            obj.DataList.Add(new StdDbColumn("DEPT", StdDbType.NUMBER, this.DeptCode));
            obj.DataList.Add(new StdDbColumn("TAG_DATE", StdDbType.NUMBER, this.DoDate));
            obj.DataList.Add(new StdDbColumn("SEQ", StdDbType.NUMBER, this.SEQ1));

            obj.DataList.Add(new StdDbColumn("TAG_COMMENT", StdDbType.VARCHAR2, this.Cont1));
            obj.DataList.Add(new StdDbColumn("DISP_SEQ", StdDbType.NUMBER, this.SEQ2));
            obj.DataList.Add(new StdDbColumn("DEL_FLG", StdDbType.NUMBER, 0));

            obj.DataList.Add(new StdDbColumn("REG_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("REG_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("REG_USR", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("REG_PCNAME", StdDbType.VARCHAR2, Environment.MachineName));
            sr = obj.InsertSQL();

            return sr;
        }

        public StdReturn Update()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
            obj.Db = DB.Db3;
            obj.Table = "D_KARTE_TAG";

            obj.DataList.Add(new StdDbColumn("TAG_COMMENT", StdDbType.VARCHAR2, this.Cont1));
            obj.DataList.Add(new StdDbColumn("DISP_SEQ", StdDbType.NUMBER, this.SEQ2));
            obj.DataList.Add(new StdDbColumn("DEL_FLG", StdDbType.NUMBER, 0));

            obj.DataList.Add(new StdDbColumn("UP_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("UP_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("UP_USR", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("UP_PCNAME", StdDbType.VARCHAR2, Environment.MachineName));

            obj.WhereList.Add("P_ID = " + this.PtId);
            obj.WhereList.Add("INOUT = " + this.InOut);
            obj.WhereList.Add("DEPT = " + this.DeptCode);
            obj.WhereList.Add("TAG_DATE = " + this.DoDate);
            obj.WhereList.Add("SEQ = " + this.SEQ1);
            sr = obj.UpdateSQL();

            return sr;
        }

        public StdReturn Delete()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
            obj.Db = DB.Db3;
            obj.Table = "D_KARTE_TAG";

            obj.DataList.Add(new StdDbColumn("DEL_FLG", StdDbType.NUMBER, 1));

            obj.DataList.Add(new StdDbColumn("UP_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("UP_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("UP_USR", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("UP_PCNAME", StdDbType.VARCHAR2, Environment.MachineName));

            obj.WhereList.Add("P_ID = " + this.PtId);
            obj.WhereList.Add("INOUT = " + this.InOut);
            obj.WhereList.Add("DEPT = " + this.DeptCode);
            obj.WhereList.Add("TAG_DATE = " + this.DoDate);
            obj.WhereList.Add("SEQ = " + this.SEQ1);
            sr = obj.UpdateSQL();

            return sr;
        }
    }
}
