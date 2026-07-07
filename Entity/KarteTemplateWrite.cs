using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedicalLibrary.Entity
{
    public class KarteTemplateWrite : StdEntity
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

        public int SEQ1 = 0;

        public int SEQ2 = 0;

        public int Row = 0;

        public int Col = 0;

        public string Pos
        {
            get
            {
                return Col.ToString().PadLeft(3, '0') + Row.ToString().PadLeft(3, '0');
            }
        }

        public int QtyRow = 0;

        public int QtyCol = 0;

        public string QtyPos
        {
            get
            {
                return QtyCol.ToString().PadLeft(3, '0') + QtyRow.ToString().PadLeft(3, '0');
            }
        }

        public float QtyValue = 0.0F;

        public int TimesRow = 0;

        public int TimesCol = 0;

        public string TimesPos
        {
            get
            {
                return TimesCol.ToString().PadLeft(3, '0') + TimesRow.ToString().PadLeft(3, '0');
            }
        }

        public int TimesValue = 0;

        public bool Trigger = false;

        public bool Newline = false;

        public string SoapKind = "";

        public List<KarteTemplateWrite> TriggerList = new List<KarteTemplateWrite>();



        public static List<KarteTemplateWrite> GetList(string code, string kind)
        {
            List<KarteTemplateWrite> list = new List<KarteTemplateWrite>();

            if (code.Length == 0 || kind.Length == 0)
            {
                return list;
            }

            string cmd = "select * from macs.AMB_TEMPLATE_CONVERT t " +
                " where t.コード = " + code + " and t.種別 = " + kind +
                " order by t.区切連番, t.明細連番";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            Dictionary<int, List<KarteTemplateWrite>> dict = KarteTemplateWrite.GetTriggerDict(code, kind);

            foreach (KarteTemplateWrite obj in list)
            {
                if (dict.ContainsKey(obj.SEQ1))
                {
                    obj.TriggerList = dict[obj.SEQ1];
                }
            }

            return list;
        }


        private static Dictionary<int, List<KarteTemplateWrite>> GetTriggerDict(string code, string kind)
        {
            Dictionary<int, List<KarteTemplateWrite>> dict = new Dictionary<int, List<KarteTemplateWrite>>();

            if (code.Length == 0 || kind.Length == 0)
            {
                return dict;
            }

            string cmd = "select * from macs.AMB_TEMPLATE_CONVERT t " +
                " where t.コード = " + code + " and t.種別 = " + kind +
                " and t.トリガー = 1 " +
                " order by t.区切連番, t.明細連番";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                KarteTemplateWrite obj = GetFromStdClass(tmp);

                if (dict.ContainsKey(obj.SEQ1))
                {
                    dict[obj.SEQ1].Add(obj);
                }
                else
                {
                    List<KarteTemplateWrite> list = new List<KarteTemplateWrite>();
                    list.Add(obj);
                    dict.Add(obj.SEQ1, list);
                }
            }

            return dict;
        }

        static KarteTemplateWrite GetFromStdClass(StdClass tmp)
        {
            KarteTemplateWrite obj = new KarteTemplateWrite();

            obj.Code = tmp.DataDict["コード"].ToString();
            obj.Kind = tmp.DataDict["種別"].ToString();
            int.TryParse(tmp.DataDict["区切連番"].ToString(), out obj.SEQ1);
            int.TryParse(tmp.DataDict["明細連番"].ToString(), out obj.SEQ2);
            int.TryParse(tmp.DataDict["オーダー行"].ToString(), out obj.Row);
            int.TryParse(tmp.DataDict["オーダー列"].ToString(), out obj.Col);
            int.TryParse(tmp.DataDict["数量行"].ToString(), out obj.QtyRow);
            int.TryParse(tmp.DataDict["数量列"].ToString(), out obj.QtyCol);
            float.TryParse(tmp.DataDict["数量値"].ToString(), out obj.QtyValue);
            int.TryParse(tmp.DataDict["回数行"].ToString(), out obj.TimesRow);
            int.TryParse(tmp.DataDict["回数列"].ToString(), out obj.TimesCol);
            int.TryParse(tmp.DataDict["回数値"].ToString(), out obj.TimesValue);
            obj.Trigger = tmp.DataDict["トリガー"].ToString().Equals("1") ? true : false;
            obj.Newline = tmp.DataDict["改行"].ToString().Equals("1") ? true : false;
            obj.SoapKind = tmp.DataDict["ＳＯＡＰ区分"].ToString();

            return obj;
        }
    }
}
