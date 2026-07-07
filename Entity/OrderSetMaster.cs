using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class OrderSetMaster : StdEntity
    {
        /// <summary>
        /// オーダーセットコード
        /// </summary>
        public string OrderSetCode = "";

        /// <summary>
        /// 連番
        /// </summary>
        public int SEQ = 1;

        /// <summary>
        /// 項目数
        /// </summary>
        public int Count = 1;

        /// <summary>
        /// 診療コード
        /// </summary>
        public int KouiCode = 0;

        /// <summary>
        /// オーダーコード
        /// </summary>
        public string OrderCode = "";

        /// <summary>
        /// オーダーマスター
        /// </summary>
        public OrderMaster OrderMaster1 = new OrderMaster();

        /// <summary>
        /// 数量
        /// </summary>
        public float Qty = 0;

        /// <summary>
        /// 回数
        /// </summary>
        public int Times = 0;


        public static List<OrderSetMaster> GetList(string order_set_code)
        {
            List<OrderSetMaster> list = new List<OrderSetMaster>();

            if (order_set_code.Length == 0)
            {
                return list;
            }

            string cmd = "select ts.*, tm.名称, tm.略称, tm.単位, tm.入力単位 " +
                " from ＮＴオーダーセットマスター ts, ＮＴオーダーマスター tm " +
                " where ts.オーダーセットコード = '" + order_set_code + "' " +
                " and ts.世代区分 = tm.世代区分 and ts.オーダーコード = tm.オーダーコード " +
                " order by ts.連番";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                OrderSetMaster obj = OrderSetMaster.GetFromStdClass(tmp);

                obj.OrderMaster1.FullName = tmp.DataDict["名称"].ToString().Trim();
                obj.OrderMaster1.ShortName = tmp.DataDict["略称"].ToString().Trim();
                obj.OrderMaster1.Unit = tmp.DataDict["単位"].ToString().Trim();
                obj.OrderMaster1.InputUnit = tmp.DataDict["入力単位"].ToString().Trim();

                list.Add(obj);
            }

            return list;
        }

        static OrderSetMaster GetFromStdClass(StdClass tmp)
        {
            OrderSetMaster obj = new OrderSetMaster();

            obj.OrderSetCode = tmp.DataDict["オーダーセットコード"].ToString().Trim();
            int.TryParse(tmp.DataDict["連番"].ToString(), out obj.SEQ);
            int.TryParse(tmp.DataDict["項目数"].ToString(), out obj.Count);
            int.TryParse(tmp.DataDict["診療コード"].ToString(), out obj.KouiCode);
            obj.OrderCode = tmp.DataDict["オーダーコード"].ToString().Trim();
            float.TryParse(tmp.DataDict["数量"].ToString(), out obj.Qty);
            int.TryParse(tmp.DataDict["回数"].ToString(), out obj.Times);

            return obj;
        }
    }
}
