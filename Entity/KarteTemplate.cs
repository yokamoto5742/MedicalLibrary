using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class KarteTemplate : StdEntity
    {
        public string Key1 = "";

        public string Key2 = "";

        /// <summary>
        /// コード
        /// </summary>
        public string Code = "";

        /// <summary>
        /// 名称
        /// </summary>
        public string Name = "";

        /// <summary>
        /// 種別
        /// 1: 入力, 2: 問診
        /// </summary>
        public string Kind = "";

        public bool CellOver = false;

        public override string ToString()
        {
            string s = this.Code + " " + this.Name;

            if (this.Kind.Equals("2"))
            {
                s += "【問診】";
            }

            return s;
        }


        public static List<KarteTemplate> GetList(string key1, string key2)
        {
            List<KarteTemplate> list = new List<KarteTemplate>();

            string cmd = "select * from macs.AMB_TEMPLATE_MASTER t ";

            List<string> conds = new List<string>();

            if (key1.Length > 0 && !key1.Equals("0"))
            {
                conds.Add("t.階層キー１ = " + key1);
            }

            if (key2.Length > 0 && !key2.Equals("0"))
            {
                conds.Add("t.階層キー２ = " + key2);
            }

            if (conds.Count > 0)
            {
                cmd += " where " + AppString.ConcatList(conds, " and ");
            }

            cmd += " order by t.コード, t.種別";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }

        static KarteTemplate GetFromStdClass(StdClass tmp)
        {
            KarteTemplate obj = new KarteTemplate();

            obj.Key1 = tmp.DataDict["階層キー１"].ToString();
            obj.Key2 = tmp.DataDict["階層キー２"].ToString();
            obj.Code = tmp.DataDict["コード"].ToString();
            obj.Name = tmp.DataDict["名称"].ToString();
            obj.Kind = tmp.DataDict["種別"].ToString();
            obj.CellOver = tmp.DataDict["セルオーバー"].ToString().Equals("1") ? true : false;

            return obj;
        }
    }
}
