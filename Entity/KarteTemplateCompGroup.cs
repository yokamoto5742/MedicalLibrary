using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedicalLibrary.Entity
{
    public class KarteTemplateCompGroup : StdEntity
    {
        /// <summary>
        /// コード
        /// </summary>
        public string Code = "";

        /// <summary>
        /// 種別
        /// 1: 入力, 2: 問診
        /// </summary>
        public string Kind = "";

        /// <summary>
        /// グループＮＯ
        /// </summary>
        public int GroupNum = 0;

        /// <summary>
        /// 座標行
        /// </summary>
        public int Row = 0;

        /// <summary>
        /// 座標列
        /// </summary>
        public int Col = 0;

        public string Pos
        {
            get
            {
                return Col.ToString().PadLeft(3, '0') + Row.ToString().PadLeft(3, '0');
            }
        }

        public static Dictionary<int, List<KarteTemplateCompGroup>> GetDict(string code, string kind)
        {
            Dictionary<int, List<KarteTemplateCompGroup>> dict = new Dictionary<int, List<KarteTemplateCompGroup>>();

            if (code.Length == 0 || kind.Length == 0)
            {
                return dict;
            }

            string cmd = "select * from macs.AMB_TEMPLATE_GROUP t " +
                " where t.コード = " + code + " and t.種別 = " + kind +
                " order by t.グループＮＯ, t.ＮＯ, t.ボタン行, t.ボタン列 ";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                KarteTemplateCompGroup obj = KarteTemplateCompGroup.GetFromStdClass(tmp);

                if (dict.ContainsKey(obj.GroupNum))
                {
                    dict[obj.GroupNum].Add(obj);
                }
                else
                {
                    List<KarteTemplateCompGroup> list = new List<KarteTemplateCompGroup>();
                    list.Add(obj);
                    dict.Add(obj.GroupNum, list);
                }
            }

            return dict;
        }


        static KarteTemplateCompGroup GetFromStdClass(StdClass tmp)
        {
            KarteTemplateCompGroup obj = new KarteTemplateCompGroup();

            obj.Code = tmp.DataDict["コード"].ToString();
            obj.Kind = tmp.DataDict["種別"].ToString();
            int.TryParse(tmp.DataDict["グループＮＯ"].ToString(), out obj.GroupNum);
            int.TryParse(tmp.DataDict["ボタン行"].ToString(), out obj.Row);
            int.TryParse(tmp.DataDict["ボタン列"].ToString(), out obj.Col);

            return obj;
        }
    }
}
