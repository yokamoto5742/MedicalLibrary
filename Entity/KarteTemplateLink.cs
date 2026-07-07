using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedicalLibrary.Entity
{
    public class KarteTemplateLink : StdEntity
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

        public int Row = 0;

        public int Col = 0;

        public string Pos
        {
            get
            {
                return Col.ToString().PadLeft(3, '0') + Row.ToString().PadLeft(3, '0');
            }
        }

        public int ChildRow = 0;

        public int ChildCol = 0;

        public string ChildPos
        {
            get
            {
                return ChildCol.ToString().PadLeft(3, '0') + ChildRow.ToString().PadLeft(3, '0');
            }
        }


        public static Dictionary<string, List<KarteTemplateLink>> GetChildDict(string code, string kind)
        {
            Dictionary<string, List<KarteTemplateLink>> dict = new Dictionary<string, List<KarteTemplateLink>>();

            if (code.Length == 0 || kind.Length == 0)
            {
                return dict;
            }

            string cmd = "select * from macs.AMB_TEMPLATE_CHECK t " +
                " where t.コード = " + code + " and t.種別 = " + kind +
                " order by t.ボタン行, t.ボタン列";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                KarteTemplateLink obj = new KarteTemplateLink();

                obj.Code = code;
                obj.Kind = kind;
                int.TryParse(tmp.DataDict["ボタン行"].ToString(), out obj.Row);
                int.TryParse(tmp.DataDict["ボタン列"].ToString(), out obj.Col);
                int.TryParse(tmp.DataDict["チェック行"].ToString(), out obj.ChildRow);
                int.TryParse(tmp.DataDict["チェック列"].ToString(), out obj.ChildCol);

                if (dict.ContainsKey(obj.Pos))
                {
                    dict[obj.Pos].Add(obj);
                }
                else
                {
                    List<KarteTemplateLink> list = new List<KarteTemplateLink>();
                    list.Add(obj);
                    dict.Add(obj.Pos, list);
                }
            }

            return dict;
        }


        public static Dictionary<string, List<KarteTemplateLink>> GetParentDict(string code, string kind)
        {
            Dictionary<string, List<KarteTemplateLink>> dict = new Dictionary<string, List<KarteTemplateLink>>();

            if (code.Length == 0 || kind.Length == 0)
            {
                return dict;
            }

            string cmd = "select * from macs.AMB_TEMPLATE_CHECK t " +
                " where t.コード = " + code + " and t.種別 = " + kind +
                " order by t.チェック行, t.チェック列";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                KarteTemplateLink obj = new KarteTemplateLink();

                obj.Code = code;
                obj.Kind = kind;
                int.TryParse(tmp.DataDict["ボタン行"].ToString(), out obj.Row);
                int.TryParse(tmp.DataDict["ボタン列"].ToString(), out obj.Col);
                int.TryParse(tmp.DataDict["チェック行"].ToString(), out obj.ChildRow);
                int.TryParse(tmp.DataDict["チェック列"].ToString(), out obj.ChildCol);

                if (dict.ContainsKey(obj.ChildPos))
                {
                    dict[obj.ChildPos].Add(obj);
                }
                else
                {
                    List<KarteTemplateLink> list = new List<KarteTemplateLink>();
                    list.Add(obj);
                    dict.Add(obj.ChildPos, list);
                }
            }

            return dict;
        }
    }
}
