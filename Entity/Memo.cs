using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// 患者サマリデータ（伝達事項）
    /// </summary>
    public class Memo : StdKarte1
    {
        /// <summary>
        /// 入外区分
        /// 現在はすべて 0 であり、使われていない。
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

                return s;
            }
        }

        /// <summary>
        /// サマリ内容
        /// </summary>
        public string Cont = "";


        public static List<Memo> Load(string pt_id)
        {
            List<Memo> list = new List<Memo>();

            if (pt_id.Length == 0)
            {
                return list;
            }
#if INNO
            string cmd = "select * from D_KARTE_MEMO t " +
                " where t.P_ID = " + pt_id +
                " order by t.INOUT";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
#else
            string cmd = "select * from macs.ADT_患者サマリデータ t " +
                " where t.患者コード = " + pt_id +
                " order by t.入外区分";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);
#endif

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }


        public static Memo Load(string pt_id, string in_out = "0")
        {
            Memo obj = new Memo();

            if (pt_id.Length == 0 || in_out.Length == 0)
            {
                return obj;
            }

#if INNO
            string cmd = "select * from D_KARTE_MEMO t " +
                " where t.P_ID = " + pt_id + " and t.INOUT = " + in_out;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
#else
            string cmd = "select * from macs.ADT_患者サマリデータ t " +
                " where t.患者コード = " + pt_id + " and t.入外区分 = " + in_out;

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);
#endif

            foreach (StdClass tmp in tmp_list)
            {
                obj = GetFromStdClass(tmp);
                break;
            }

            return obj;
        }


        public static string LoadString(string pt_id)
        {
            string s = "";

            List<Memo> list = Load(pt_id);

            foreach (Memo obj in list)
            {
                if (s.Length > 0)
                {
                    s += Environment.NewLine;
                }

                if (obj.InOutString.Length > 0)
                {
                    s += "【" + obj.InOutString + "】" + Environment.NewLine;
                }

                s += obj.Cont + Environment.NewLine;
            }

            return s;
        }


        static Memo GetFromStdClass(StdClass tmp)
        {
            Memo obj = new Memo();

#if INNO
            obj.InOut = tmp.DataDict["INOUT"].ToString();
            obj.Cont = tmp.DataDict["KARTE_MEMO"].ToString();
#else
            obj.InOut = tmp.DataDict["入外区分"].ToString();
            obj.Cont = tmp.DataDict["サマリ内容"].ToString();
#endif

            obj.BaseFromStdClass(tmp);

            return obj;
        }


        public StdReturn Save()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();

#if INNO
            obj.Db = DB.Db3;
            obj.Table = "D_KARTE_MEMO";

            obj.DataList.Add(new StdDbColumn("KARTE_MEMO", StdDbType.VARCHAR2, this.Cont));

            obj.DataList.Add(new StdDbColumn("UP_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("UP_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("UP_USR", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("UP_PCNAME", StdDbType.VARCHAR2, Environment.MachineName));

            obj.WhereList.Add("P_ID = " + this.PtId);
            obj.WhereList.Add("INOUT = " + this.InOut);

            sr = obj.UpdateSQL();

            if (sr.IntValue == 0)
            {
                obj.DataList.Add(new StdDbColumn("P_ID", StdDbType.NUMBER, this.PtId));
                obj.DataList.Add(new StdDbColumn("INOUT", StdDbType.NUMBER, this.InOut));

                obj.DataList.Add(new StdDbColumn("REG_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
                obj.DataList.Add(new StdDbColumn("REG_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
                obj.DataList.Add(new StdDbColumn("REG_USR", StdDbType.NUMBER, LoginUser.Id));
                obj.DataList.Add(new StdDbColumn("REG_PCNAME", StdDbType.VARCHAR2, Environment.MachineName));

                sr = obj.InsertSQL();
            }
#else
            obj.Db = DB.Db1;
            obj.Table = "ADT_患者サマリデータ";

            obj.DataList.Add(new StdDbColumn("サマリ内容", StdDbType.VARCHAR2, this.Cont));

            obj.DataList.Add(new StdDbColumn("更新日", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("更新時間", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("更新者", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("代行更新者", StdDbType.NUMBER, LoginUser.Id2));

            obj.WhereList.Add("患者コード = " + this.PtId);
            obj.WhereList.Add("入外区分 = " + this.InOut);

            sr = obj.UpdateSQL();

            if (sr.IntValue == 0)
            {
                obj.DataList.Add(new StdDbColumn("患者コード", StdDbType.NUMBER, this.PtId));
                obj.DataList.Add(new StdDbColumn("入外区分", StdDbType.NUMBER, this.InOut));

                obj.DataList.Add(new StdDbColumn("登録日", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
                obj.DataList.Add(new StdDbColumn("登録時間", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
                obj.DataList.Add(new StdDbColumn("登録者", StdDbType.NUMBER, LoginUser.Id));
                obj.DataList.Add(new StdDbColumn("代行登録者", StdDbType.NUMBER, LoginUser.Id2));

                sr = obj.InsertSQL();
            }
#endif

            return sr;
        }
    }
}
