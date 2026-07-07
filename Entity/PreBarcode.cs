using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;

namespace MedicalLibrary.Entity
{
    public class PreBarcode : StdEntity
    {
        /// <summary>
        /// シート種別
        /// 3: わかば, 4: さくら, 24 あやめ 等
        /// </summary>
        public string Sheet = "";

        /// <summary>
        /// 分類種別
        /// 2: 内服薬, 3: 外用薬, 4: 注射薬 等
        /// </summary>
        public string Kind = "";

        /// <summary>
        /// バーコード
        /// </summary>
        public string Barcode = "";

        public string KouiCode = "";

        public string KouiName
        {
            get
            {
                string s = "";

                if (Dict.KouiDict.ContainsKey(this.KouiCode))
                {
                    s = Dict.KouiDict[this.KouiCode];
                }

                return s;
            }
        }

        public string OrderCode = "";

        public string OrderName = "";

        public float Qty = 0;

        /// <summary>
        /// ログに記録する文字列
        /// </summary>
        public string LogString
        {
            get
            {
                string s = "";

                s += this.KouiCode + " " + this.OrderCode + " " + this.Qty + " " + this.OrderName;

                return s;
            }
        }


        /// <summary>
        /// 先行実施オーダーマスターのリスト
        /// </summary>
        public List<PreOrder> PreOrderList = new List<PreOrder>();


        /// <summary>
        /// 実施バーコードマスターのリスト
        /// </summary>
        public static List<PreBarcode> MasterList = new List<PreBarcode>();


        static PreBarcode GetFromStdClass(StdClass tmp)
        {
            PreBarcode obj = new PreBarcode();

            obj.Sheet = tmp.DataDict["シート種別"].ToString();
            obj.Kind = tmp.DataDict["分類種別"].ToString();
            obj.Barcode = tmp.DataDict["バーコードコード"].ToString().Trim();

            // 以下はバーコード情報から取得して設定する
            obj.KouiCode = obj.Barcode.Substring(0, 2);
            obj.OrderCode = obj.Barcode.Substring(2, 12).TrimEnd(' ');

            if (obj.Barcode.Substring(14, 5).TrimStart('0').Length > 0)
            {
                obj.Qty = float.Parse(obj.Barcode.Substring(14, 5).TrimStart('0')) / 100;
            }

            return obj;
        }

        /// <summary>
        /// マスターリストの初期化。FormExec を起動したときに行う。
        /// </summary>
        public static void Init()
        {
            string cmd = "select * from 実施バーコードマスター " +
                " order by データ区分, シート種別, 分類種別, 連番";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PreBarcode bm = GetFromStdClass(tmp);

                // オーダーマスターから取得
                OrderMaster om = OrderMaster.Load(bm.OrderCode);

                // バーコードの名称を取得
                bm.OrderName = om.ShortName;

                if (om.Kind.Equals(5))
                {
                    // データ区分 = 5 ならばオーダーセットマスター

                    List<OrderSetMaster> master_list = OrderSetMaster.GetList(bm.OrderCode);

                    foreach (OrderSetMaster master in master_list)
                    {
                        PreOrder obj = new PreOrder();
                        obj.KouiCode = master.KouiCode.ToString();
                        obj.OrderCode = master.OrderCode;
                        obj.Qty = master.Qty * bm.Qty;
                        obj.OrderName = master.OrderMaster1.ShortName;
                        obj.Unit = master.OrderMaster1.Unit;

                        bm.PreOrderList.Add(obj);
                    }
                }
                else
                {
                    PreOrder obj = new PreOrder();
                    obj.KouiCode = bm.KouiCode;
                    obj.OrderCode = bm.OrderCode;
                    obj.Qty = bm.Qty;
                    obj.OrderName = om.ShortName;
                    obj.Unit = om.Unit;

                    bm.PreOrderList.Add(obj);
                }
                
                MasterList.Add(bm);
            }
        }
    }
}
