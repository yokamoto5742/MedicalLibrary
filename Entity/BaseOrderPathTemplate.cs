using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// PATH_M基本指示テンプレート
    /// </summary>
    public class BaseOrderPathTemplate : StdEntity
    {
        /// <summary>
        /// パスコード
        /// </summary>
        public string PathCode = "";

        /// <summary>
        /// 指示日
        /// </summary>
        public int OrderDate = 0;

        /// <summary>
        /// コメント
        /// </summary>
        public string Cont1 = "";

        /// <summary>
        /// データ 1～20
        /// </summary>
        public Dictionary<int, BaseOrderDataItem> ItemDict = new Dictionary<int, BaseOrderDataItem>();


        public static List<BaseOrderPathTemplate> GetList(string path_code)
        {
            List<BaseOrderPathTemplate> list = new List<BaseOrderPathTemplate>();

            if (path_code.Length == 0)
            {
                return list;
            }

            StdDbClass tmp_db = new StdDbClass(Db);

            tmp_db.Table = "PATH_M基本指示テンプレート";
            tmp_db.WhereList.Add("パスコード = '" + path_code + "'");
            tmp_db.OrderByList.Add("指示日");

            List<StdClass> tmp_list = tmp_db.SelectSQL();

            foreach (StdClass tmp in tmp_list)
            {
                BaseOrderPathTemplate obj = new BaseOrderPathTemplate();

                obj.PathCode = path_code;
                int.TryParse(tmp.DataDict["指示日"].ToString(), out obj.OrderDate);
                obj.Cont1 = tmp.DataDict["その他"].ToString();

                for (int i = 1; i <= 20; i++)
                {
                    BaseOrderDataItem item = new BaseOrderDataItem();
                    item.SEQ = i;

                    string tmp_i = AppString.HanToZen(i.ToString());

                    if (tmp.DataDict.ContainsKey("約束名称" + tmp_i))
                    {
                        item.Name = tmp.DataDict["約束名称" + tmp_i].ToString();
                    }

                    if (tmp.DataDict.ContainsKey("約束条件" + tmp_i))
                    {
                        item.Value1 = tmp.DataDict["約束条件" + tmp_i].ToString();
                    }

                    if (tmp.DataDict.ContainsKey("約束処方" + tmp_i))
                    {
                        item.Value2 = tmp.DataDict["約束処方" + tmp_i].ToString();
                    }

                    if (!obj.ItemDict.ContainsKey(i))
                    {
                        obj.ItemDict.Add(i, item);
                    }
                }

                list.Add(obj);
            }

            return list;
        }
    }
}
