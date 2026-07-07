using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// MACS DB にある汎用マスター
    /// </summary>
    public class StdMaster1 : StdEntity
    {
        /// <summary>
        /// F01 コード
        /// </summary>
        public string Code = "";

        /// <summary>
        /// F02 名称
        /// </summary>
        public string Name = "";

        /// <summary>
        /// F03 略称
        /// </summary>
        public string Short = "";

        /// <summary>
        /// カナ
        /// </summary>
        public string Kana = "";

        /// <summary>
        /// カテゴリ
        /// </summary>
        public string Category = "";

        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DelFlg = false;

        public static Dictionary<string, StdMaster1> GetDict(string table_name)
        {
            Dictionary<string, StdMaster1> dict = new Dictionary<string, StdMaster1>();

            if (table_name.Length == 0)
            {
                return dict;
            }

            string cmd = "select * from " + table_name;
#if INNO
            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                StdMaster1 obj = new StdMaster1();

                obj.Code = tmp.GetDataString("CODE").Trim();
                obj.Name = tmp.GetDataString("NAME").Trim();
                obj.Short = tmp.GetDataString("S_NAME").Trim();
                obj.Kana = tmp.GetDataString("KANA").Trim();
                obj.Category = tmp.GetDataString("CATEGORY").Trim();
                obj.DelFlg = tmp.GetDataInt("DEL_FLG").Equals(1);

                if (!dict.ContainsKey(obj.Code))
                {
                    dict.Add(obj.Code, obj);
                }
            }
#else
            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                StdMaster1 obj = new StdMaster1();

                obj.Code = tmp.DataDict[table_name + "_F01"].ToString().Trim();
                obj.Name = tmp.DataDict[table_name + "_F02"].ToString().Trim();
                obj.Short = tmp.DataDict[table_name + "_F03"].ToString().Trim();

                if (!dict.ContainsKey(obj.Code))
                {
                    dict.Add(obj.Code, obj);
                }
            }
#endif
            return dict;
        }
    }
}
