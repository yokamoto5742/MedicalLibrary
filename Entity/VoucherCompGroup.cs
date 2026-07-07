using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedicalLibrary.Entity
{
    public class VoucherCompGroup : StdEntity
    {
        /// <summary>
        /// 伝票番号
        /// </summary>
        public string Code = "";

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

        public static Dictionary<int, List<VoucherCompGroup>> GetDict(string voucher_code)
        {
            Dictionary<int, List<VoucherCompGroup>> dict = new Dictionary<int, List<VoucherCompGroup>>();

            if (voucher_code.Length == 0)
            {
                return dict;
            }

            string cmd = "select * from macs.伝票グループマスター t " +
                " where t.伝票番号 = '" + voucher_code + "'" +
                " order by t.グループＮＯ, t.ボタン行, t.ボタン列 ";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                VoucherCompGroup obj = VoucherCompGroup.GetFromStdClass(tmp);

                if (dict.ContainsKey(obj.GroupNum))
                {
                    dict[obj.GroupNum].Add(obj);
                }
                else
                {
                    List<VoucherCompGroup> list = new List<VoucherCompGroup>();
                    list.Add(obj);
                    dict.Add(obj.GroupNum, list);
                }
            }

            return dict;
        }


        static VoucherCompGroup GetFromStdClass(StdClass tmp)
        {
            VoucherCompGroup obj = new VoucherCompGroup();

            obj.Code = tmp.DataDict["伝票番号"].ToString();
            int.TryParse(tmp.DataDict["グループＮＯ"].ToString(), out obj.GroupNum);
            int.TryParse(tmp.DataDict["ボタン行"].ToString(), out obj.Row);
            int.TryParse(tmp.DataDict["ボタン列"].ToString(), out obj.Col);

            return obj;
        }
    }
}
