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

#if INNO
            string cmd = "select * from D_KARTE_TAG t " +
                " where t.P_ID = " + pt_id +
                " and t.INOUT = " + in_out +
                " and t.DEPT = " + dept_code +
                " and t.TAG_DATE = " + do_date +
                " and t.SEQ = " + seq1;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
#else
            string cmd = "select * from macs.ADT_付箋データ t " +
                " where t.患者コード = " + pt_id +
                " and t.入外区分 = " + in_out +
                " and t.科コード = " + dept_code +
                " and t.対象日 = " + do_date +
                " and t.連番 = " + seq1;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif

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

#if INNO
            // 削除されたものも含めて取得する
            string cmd = "select * from D_KARTE_TAG t " +
                " where t.P_ID = " + pt_id +
                " order by t.INOUT, t.DEPT, t.TAG_DATE desc, t.DISP_SEQ";
            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
#else
            // 削除されたものも含めて取得する
            string cmd = "select * from macs.ADT_付箋データ t " +
                " where t.患者コード = " + pt_id +
                " order by t.入外区分, t.科コード, t.対象日 desc, t.表示順";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif

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

#if INNO
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
#else
            string InOutSql = "";

            if (in_out.Length > 0)
            {
                InOutSql = " and t.入外区分 = " + in_out;
            }

            // 削除されたものは飛ばす
            string cmd = "select * from macs.ADT_付箋データ t " +
                " where t.対象日 = " + do_date + InOutSql +
                " and t.削除フラグ = 0 " +
                " order by t.患者コード, t.入外区分, t.科コード, t.対象日 desc, t.表示順";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif

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

#if INNO
            string cmd = "select max(SEQ) 連番 from D_KARTE_TAG t " +
                " where t.P_ID = " + pt_id +
                " and t.INOUT = " + in_out +
                " and t.DEPT = " + dept +
                " and t.TAG_DATE = " + do_date;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
#else
            string cmd = "select max(連番) 連番 from macs.ADT_付箋データ t " +
                " where t.患者コード = " + pt_id +
                " and t.入外区分 = " + in_out +
                " and t.科コード = " + dept +
                " and t.対象日 = " + do_date;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif

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

#if INNO
            obj.InOut = tmp.GetDataString("INOUT");
            obj.DeptCode = tmp.GetDataString("DEPT");
            obj.DoDate = tmp.GetDataInt("TAG_DATE");
            obj.SEQ1 = tmp.GetDataInt("SEQ");
            obj.SEQ2 = tmp.GetDataInt("DISP_SEQ");
            obj.Cont1 = tmp.GetDataString("TAG_COMMENT");
            obj.DeleteFlg = tmp.GetDataString("DEL_FLG").Equals("1") ? true : false;
#else
            obj.InOut = tmp.GetDataString("入外区分");
            obj.DeptCode = tmp.GetDataString("科コード");
            obj.DoDate = tmp.GetDataInt("対象日");
            obj.SEQ1 = tmp.GetDataInt("連番");
            obj.SEQ2 = tmp.GetDataInt("表示順");
            obj.Cont1 = tmp.GetDataString("付箋内容");
            obj.DeleteFlg = tmp.GetDataString("削除フラグ").Equals("1") ? true : false;
#endif

            return obj;
        }

        public StdReturn Insert()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();

            // 連番と表示順を取得する
            this.SEQ1 = PostIt.GetMaxSEQ1(this.PtId, this.InOut, this.DeptCode, this.DoDate.ToString()) + 1;
            this.SEQ2 = PostIt.GetMaxSEQ1(this.PtId, this.InOut, this.DeptCode, this.DoDate.ToString()) + 1;

#if INNO
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
#else
            obj.Db = DB.Db1;
            obj.Table = "ADT_付箋データ";

            obj.DataList.Add(new StdDbColumn("患者コード", StdDbType.NUMBER, this.PtId));
            obj.DataList.Add(new StdDbColumn("入外区分", StdDbType.NUMBER, this.InOut));
            obj.DataList.Add(new StdDbColumn("科コード", StdDbType.NUMBER, this.DeptCode));
            obj.DataList.Add(new StdDbColumn("対象日", StdDbType.NUMBER, this.DoDate));
            obj.DataList.Add(new StdDbColumn("連番", StdDbType.NUMBER, this.SEQ1));

            obj.DataList.Add(new StdDbColumn("付箋内容", StdDbType.VARCHAR2, this.Cont1));
            obj.DataList.Add(new StdDbColumn("表示順", StdDbType.NUMBER, this.SEQ2));
            obj.DataList.Add(new StdDbColumn("削除フラグ", StdDbType.NUMBER, 1));

            obj.DataList.Add(new StdDbColumn("登録日", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("登録時間", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("登録者", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("代行登録者", StdDbType.NUMBER, LoginUser.Id2));
#endif
            sr = obj.InsertSQL();

            return sr;
        }

        public StdReturn Update()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
#if INNO
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
#else
            obj.Db = DB.Db1;
            obj.Table = "ADT_付箋データ";

            obj.DataList.Add(new StdDbColumn("付箋内容", StdDbType.VARCHAR2, this.Cont1));
            obj.DataList.Add(new StdDbColumn("表示順", StdDbType.NUMBER, this.SEQ2));
            obj.DataList.Add(new StdDbColumn("削除フラグ", StdDbType.NUMBER, 0));

            obj.DataList.Add(new StdDbColumn("更新日", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("更新時間", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("更新者", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("代行更新者", StdDbType.NUMBER, LoginUser.Id2));

            obj.WhereList.Add("患者コード = " + this.PtId);
            obj.WhereList.Add("入外区分 = " + this.InOut);
            obj.WhereList.Add("科コード = " + this.DeptCode);
            obj.WhereList.Add("対象日 = " + this.DoDate);
            obj.WhereList.Add("連番 = " + this.SEQ1);
#endif
            sr = obj.UpdateSQL();

            return sr;
        }

        public StdReturn Delete()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
#if INNO
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
#else
            obj.Db = DB.Db1;
            obj.Table = "ADT_付箋データ";

            obj.DataList.Add(new StdDbColumn("削除フラグ", StdDbType.NUMBER, 1));

            obj.DataList.Add(new StdDbColumn("更新日", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("更新時間", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("更新者", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("代行更新者", StdDbType.NUMBER, LoginUser.Id2));

            obj.WhereList.Add("患者コード = " + this.PtId);
            obj.WhereList.Add("入外区分 = " + this.InOut);
            obj.WhereList.Add("科コード = " + this.DeptCode);
            obj.WhereList.Add("対象日 = " + this.DoDate);
            obj.WhereList.Add("連番 = " + this.SEQ1);
#endif
            sr = obj.UpdateSQL();

            return sr;
        }
    }
}
