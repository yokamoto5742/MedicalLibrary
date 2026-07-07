using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// PATH_M手術指示テンプレート
    /// </summary>
    public class OpeOrderPathTemplate : StdEntity
    {
        /// <summary>
        /// パスコード
        /// </summary>
        public string PathCode = "";

        /// <summary>
        /// 開始日
        /// </summary>
        public int OrderDate = 0;

        /// <summary>
        /// 指示コード
        /// </summary>
        public int Code1 = 0;

        /// <summary>
        /// 連番
        /// </summary>
        public int Code2 = 0;

        /// <summary>
        /// 選択値（すべて 1 に固定？）
        /// </summary>
        public int Value1 = 1;

        /// <summary>
        /// 入力値
        /// </summary>
        public string Cont1 = "";

        public static List<int> GetDateList(string path_code)
        {
            List<int> list = new List<int>();

            if (path_code.Length == 0)
            {
                return list;
            }

            string cmd = "select distinct 日数 from PATH_M手術指示テンプレート " +
                " where パスコード = '" + path_code + "'" +
                " order by 日数";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(int.Parse(tmp.DataDict["日数"].ToString()));
            }

            return list;
        }

        /// <summary>
        /// 指定パスの手術指示を取得。
        /// </summary>
        /// <param name="path_code"></param>
        /// <returns></returns>
        public static Dictionary<int, Dictionary<int, Dictionary<int, OpeOrderPathTemplate>>> Load(string path_code)
        {
            Dictionary<int, Dictionary<int, Dictionary<int, OpeOrderPathTemplate>>> dict = new Dictionary<int, Dictionary<int, Dictionary<int, OpeOrderPathTemplate>>>();

            if (path_code.Length == 0)
            {
                return dict;
            }

            StdDbClass tmp_db = new StdDbClass(Db);

            tmp_db.Table = "PATH_M手術指示テンプレート";
            tmp_db.WhereList.Add("パスコード = '" + path_code + "'");
            tmp_db.OrderByList.Add("指示コード");

            List<StdClass> tmp_list = tmp_db.SelectSQL();

            foreach (StdClass tmp in tmp_list)
            {
                OpeOrderPathTemplate obj = new OpeOrderPathTemplate();

                obj.PathCode = tmp.DataDict["パスコード"].ToString();
                int.TryParse(tmp.DataDict["日数"].ToString(), out obj.OrderDate);
                int.TryParse(tmp.DataDict["指示コード"].ToString(), out obj.Code1);
                int.TryParse(tmp.DataDict["連番"].ToString(), out obj.Code2);
                obj.Cont1 = tmp.DataDict["入力値"].ToString();

                Dictionary<int, Dictionary<int, OpeOrderPathTemplate>> dict2 = new Dictionary<int, Dictionary<int, OpeOrderPathTemplate>>();
                Dictionary<int, OpeOrderPathTemplate> dict3 = new Dictionary<int, OpeOrderPathTemplate>();

                if (dict.ContainsKey(obj.OrderDate))
                {
                    dict2 = dict[obj.OrderDate];
                }
                else
                {
                    dict.Add(obj.OrderDate, dict2);
                }

                if (dict2.ContainsKey(obj.Code1))
                {
                    dict3 = dict2[obj.Code1];
                }
                else
                {
                    dict2.Add(obj.Code1, dict3);
                }

                dict3.Add(obj.Code2, obj);
            }

            return dict;
        }
    }
}
