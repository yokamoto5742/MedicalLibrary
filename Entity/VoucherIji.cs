using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedicalLibrary.Entity
{
    public class VoucherIji : StdEntity
    {
        /// <summary>
        /// 伝票番号
        /// </summary>
        public string Code = "";

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

        public List<VoucherIji> TriggerList = new List<VoucherIji>();



        public static List<VoucherIji> GetList(string voucher_code)
        {
            List<VoucherIji> list = new List<VoucherIji>();

            if (voucher_code.Length == 0)
            {
                return list;
            }

            string cmd = "select * from macs.伝票医事変換マスター t " +
                " where t.伝票番号 = '" + voucher_code + "'" +
                " order by t.区切連番, t.明細連番";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            Dictionary<int, List<VoucherIji>> dict = VoucherIji.GetTriggerDict(voucher_code);

            foreach (VoucherIji obj in list)
            {
                if (dict.ContainsKey(obj.SEQ1))
                {
                    obj.TriggerList = dict[obj.SEQ1];
                }
            }

            return list;
        }


        private static Dictionary<int, List<VoucherIji>> GetTriggerDict(string voucher_code)
        {
            Dictionary<int, List<VoucherIji>> dict = new Dictionary<int, List<VoucherIji>>();

            if (voucher_code.Length == 0)
            {
                return dict;
            }

            string cmd = "select * from macs.伝票医事変換マスター t " +
                " where t.伝票番号 = '" + voucher_code + "'" +
                " and t.トリガー = 1 " +
                " order by t.区切連番, t.明細連番";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                VoucherIji obj = GetFromStdClass(tmp);

                if (dict.ContainsKey(obj.SEQ1))
                {
                    dict[obj.SEQ1].Add(obj);
                }
                else
                {
                    List<VoucherIji> list = new List<VoucherIji>();
                    list.Add(obj);
                    dict.Add(obj.SEQ1, list);
                }
            }

            return dict;
        }

        static VoucherIji GetFromStdClass(StdClass tmp)
        {
            VoucherIji obj = new VoucherIji();

            obj.Code = tmp.DataDict["伝票番号"].ToString();
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

            return obj;
        }
    }
}
