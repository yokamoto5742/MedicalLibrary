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
            string cmd = "select * from D_KARTE_MEMO t " +
                " where t.P_ID = " + pt_id +
                " order by t.INOUT";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

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

            string cmd = "select * from D_KARTE_MEMO t " +
                " where t.P_ID = " + pt_id + " and t.INOUT = " + in_out;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

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

            obj.InOut = tmp.DataDict["INOUT"].ToString();
            obj.Cont = tmp.DataDict["KARTE_MEMO"].ToString();

            obj.BaseFromStdClass(tmp);

            return obj;
        }


        public StdReturn Save()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();

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

            return sr;
        }
    }
}
