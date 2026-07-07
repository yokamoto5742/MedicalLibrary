using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedicalLibrary.Entity
{
    public class VoucherLink : StdEntity
    {
        /// <summary>
        /// 伝票番号
        /// </summary>
        public string Code = "";

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


        public static Dictionary<string, List<VoucherLink>> GetChildDict(string voucher_code)
        {
            Dictionary<string, List<VoucherLink>> dict = new Dictionary<string, List<VoucherLink>>();

            if (voucher_code.Length == 0)
            {
                return dict;
            }

            string cmd = "select * from macs.伝票チェック連動マスター t " +
                " where t.伝票番号 = '" + voucher_code + "'" +
                " order by t.ボタン行, t.ボタン列";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                VoucherLink obj = new VoucherLink();

                obj.Code = voucher_code;
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
                    List<VoucherLink> list = new List<VoucherLink>();
                    list.Add(obj);
                    dict.Add(obj.Pos, list);
                }
            }

            return dict;
        }


        public static Dictionary<string, List<VoucherLink>> GetParentDict(string voucher_code)
        {
            Dictionary<string, List<VoucherLink>> dict = new Dictionary<string, List<VoucherLink>>();

            if (voucher_code.Length == 0)
            {
                return dict;
            }

            string cmd = "select * from macs.伝票チェック連動マスター t " +
                " where t.伝票番号 = '" + voucher_code + "'" +
                " order by t.チェック行, t.チェック列";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                VoucherLink obj = new VoucherLink();

                obj.Code = voucher_code;
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
                    List<VoucherLink> list = new List<VoucherLink>();
                    list.Add(obj);
                    dict.Add(obj.ChildPos, list);
                }
            }

            return dict;
        }
    }
}
