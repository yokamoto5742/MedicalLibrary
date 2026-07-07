using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class OrderMaster : StdEntity
    {
        public string OrderCode = "";

        public int SEQ = 1;

        /// <summary>
        /// 名称
        /// </summary>
        public string FullName = "";

        /// <summary>
        /// 略称
        /// </summary>
        public string ShortName = "";

        /// <summary>
        /// 文字数
        /// </summary>
        public int Chars = 0;

        public int[] KouiCodes = new int[11];

        public string[] KouiNames
        {
            get
            {
                string[] ss = new string[11];

                for (int i = 0; i < 11; i++)
                {
                    if (Dict.KouiDict.ContainsKey(this.KouiCodes[i].ToString()))
                    {
                        ss[i] = Dict.KouiDict[this.KouiCodes[i].ToString()];
                    }
                }

                return ss;
            }
        }

        /// <summary>
        /// 単位
        /// </summary>
        public string Unit = "";

        /// <summary>
        /// 固定数量
        /// </summary>
        public int FixedQty = 0;

        /// <summary>
        /// 固定回数
        /// </summary>
        public int FixedTimes = 0;

        /// <summary>
        /// データ区分
        /// 1: 固定, 2: 薬剤, 3: 器材, 4: コメント, 5: セット, 6: フィルム, 7: 伝票名称, 8: １２３１２３
        /// </summary>
        public int Kind = 1;

        public string KindName
        {
            get
            {
                string s = "";

                if (this.Kind.Equals(1))
                {
                    s = "固定";
                }
                else if (this.Kind.Equals(2))
                {
                    s = "薬剤";
                }
                else if (this.Kind.Equals(3))
                {
                    s = "器材";
                }
                else if (this.Kind.Equals(4))
                {
                    s = "コメント";
                }
                else if (this.Kind.Equals(5))
                {
                    s = "セット";
                }
                else if (this.Kind.Equals(6))
                {
                    s = "フィルム";
                }
                else if (this.Kind.Equals(7))
                {
                    s = "伝票名称";
                }
                else if (this.Kind.Equals(8))
                {
                    s = "１２３１２３";
                }

                return s;
            }
        }

        /// <summary>
        /// 数量編集
        /// </summary>
        public int QtyEdit = 1;

        public string QtyEditName
        {
            get
            {
                string s = "";

                if (this.QtyEdit.Equals(1))
                {
                    s = "年月日";
                }
                else if (this.QtyEdit.Equals(2))
                {
                    s = "時分";
                }
                else if (this.QtyEdit.Equals(3))
                {
                    s = "日時";
                }
                else if (this.QtyEdit.Equals(4))
                {
                    s = "日～日";
                }
                else if (this.QtyEdit.Equals(5))
                {
                    s = "月日";
                }
                else if (this.QtyEdit.Equals(6))
                {
                    s = "（欠番）";
                }
                else if (this.QtyEdit.Equals(7))
                {
                    s = "時間分";
                }

                return s;
            }
        }

        /// <summary>
        /// 施行部署コード
        /// </summary>
        public string SekouDeptCode = "";

        /// <summary>
        /// 検索コード
        /// </summary>
        public string[] FindCodes = new string[4];

        /// <summary>
        /// 請求不可フラグ
        /// </summary>
        public int NoBillFlg = 0;

        /// <summary>
        /// 施行確認フラグ
        /// </summary>
        public int SekouVerifyFlg = 0;

        /// <summary>
        /// 検索区分
        /// </summary>
        public int FindKind = 0;

        /// <summary>
        /// 札臨コード（検査コード）
        /// </summary>
        public string KensaCode = "";

        /// <summary>
        /// 予備フラグ
        /// </summary>
        public int[] EtcFlgs = new int[22];

        /// <summary>
        /// 表示フラグ
        /// </summary>
        public int[] ShowFlgs = new int[4];

        /// <summary>
        /// 乗算換算
        /// </summary>
        public float MultiValue = 0;

        /// <summary>
        /// 除算換算
        /// </summary>
        public float DivideValue = 0;

        /// <summary>
        /// 入力単位
        /// </summary>
        public string InputUnit = "";

        /// <summary>
        /// 外部コード
        /// </summary>
        public int[] OutCodes = new int[11];


        static Dictionary<string, OrderMaster> yoho_dict21 = new Dictionary<string, OrderMaster>();

        static Dictionary<string, OrderMaster> yoho_dict22 = new Dictionary<string, OrderMaster>();

        static Dictionary<string, OrderMaster> yoho_dict23 = new Dictionary<string, OrderMaster>();


        public static Dictionary<string, OrderMaster> YohoDict21
        {
            get
            {
                if (yoho_dict21.Count == 0)
                {
                    List<OrderMaster> list = OrderMaster.FindByCode(".21%");

                    foreach (OrderMaster obj in list)
                    {
                        // .2100 は用法ではないので飛ばす
                        if (obj.OrderCode.Trim().Equals(".2100"))
                        {
                            continue;
                        }

                        // .219 も飛ばす
                        if (obj.OrderCode.StartsWith(".219"))
                        {
                            continue;
                        }

                        yoho_dict21.Add(obj.OrderCode, obj);
                    }
                }

                return yoho_dict21;
            }
        }

        public static Dictionary<string, OrderMaster> YohoDict22
        {
            get
            {
                if (yoho_dict22.Count == 0)
                {
                    List<OrderMaster> list = OrderMaster.FindByCode(".22%");

                    foreach (OrderMaster obj in list)
                    {
                        yoho_dict22.Add(obj.OrderCode, obj);
                    }
                }

                return yoho_dict22;
            }
        }

        public static Dictionary<string, OrderMaster> YohoDict23
        {
            get
            {
                if (yoho_dict23.Count == 0)
                {
                    List<OrderMaster> list = OrderMaster.FindByCode(".23%");

                    foreach (OrderMaster obj in list)
                    {
                        yoho_dict23.Add(obj.OrderCode, obj);
                    }

                    list = OrderMaster.FindByCode(".24%");

                    foreach (OrderMaster obj in list)
                    {
                        yoho_dict23.Add(obj.OrderCode, obj);
                    }

                    list = OrderMaster.FindByCode(".25%");

                    foreach (OrderMaster obj in list)
                    {
                        // .259 は除く
                        if (obj.OrderCode.StartsWith(".259"))
                        {
                            continue;
                        }

                        yoho_dict23.Add(obj.OrderCode, obj);
                    }
                }

                return yoho_dict23;
            }
        }


        static Dictionary<string, OrderMaster> part_dict = new Dictionary<string, OrderMaster>();

        public static Dictionary<string, OrderMaster> PartDict
        {
            get
            {
                if (part_dict.Count == 0)
                {
                    List<OrderMaster> list = OrderMaster.FindByCode(".28%");

                    foreach (OrderMaster obj in list)
                    {
                        part_dict.Add(obj.OrderCode, obj);
                    }
                }

                return part_dict;
            }
        }


        static Dictionary<string, OrderMaster> inject_etc_kind_dict = new Dictionary<string, OrderMaster>();

        public static Dictionary<string, OrderMaster> InjectEtcKindDict
        {
            get
            {
                if (inject_etc_kind_dict.Count == 0)
                {
                    List<OrderMaster> list = OrderMaster.FindByColumnNameNumber("予備フラグ１３", "2");

                    foreach (OrderMaster obj in list)
                    {
                        inject_etc_kind_dict.Add(obj.OrderCode, obj);
                    }
                }

                return inject_etc_kind_dict;
            }
        }


        static Dictionary<string, OrderMaster> inject_time_dict = new Dictionary<string, OrderMaster>();

        public static Dictionary<string, OrderMaster> InjectTimeDict
        {
            get
            {
                if (inject_time_dict.Count == 0)
                {
                    List<OrderMaster> list = OrderMaster.FindByCode("3970%");

                    foreach (OrderMaster obj in list)
                    {
                        inject_time_dict.Add(obj.OrderCode, obj);
                    }
                }

                return inject_time_dict;
            }
        }


        public override string ToString()
        {
            return this.FullName;
        }


        public static OrderMaster Load(string order_code)
        {
            OrderMaster obj = new OrderMaster();

            if (order_code.Length == 0)
            {
                return obj;
            }

            string cmd = "select * from ＮＴオーダーマスター t " +
                " where t.オーダーコード = '" + order_code + "'";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                obj = OrderMaster.GetFromStdClass(tmp);
                break;
            }

            return obj;
        }


        /// <summary>
        /// オーダーコードで検索する。
        /// </summary>
        /// <param name="order_code_list">オーダーコードのリスト。ワイルドカード検索（%）はできない。</param>
        /// <returns></returns>
        public static List<OrderMaster> LoadByCodes(List<string> order_code_list)
        {
            List<OrderMaster> list = new List<OrderMaster>();

            if (order_code_list.Count == 0)
            {
                return list;
            }

            string cmd = "select * from ＮＴオーダーマスター t " +
                " where t.オーダーコード in ('" + AppString.ConcatList(order_code_list, "','") + "')" +
                " order by t.オーダーコード";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(OrderMaster.GetFromStdClass(tmp));
            }

            return list;
        }


        /// <summary>
        /// オーダーコードで検索する。
        /// </summary>
        /// <param name="order_code">ワイルドカード検索するときは % をつける必要がある</param>
        /// <returns></returns>
        public static List<OrderMaster> FindByCode(string order_code)
        {
            List<OrderMaster> list = new List<OrderMaster>();

            if (order_code.Length == 0)
            {
                return list;
            }

            string cmd = "select * from ＮＴオーダーマスター t " +
                " where t.オーダーコード like '" + order_code + "'" +
                " order by t.オーダーコード";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(OrderMaster.GetFromStdClass(tmp));
            }

            return list;
        }


        /// <summary>
        /// カラム名を指定して文字列検索する。
        /// </summary>
        /// <param name="find_val">ワイルドカード検索するときは % をつける必要がある</param>
        /// <returns></returns>
        public static List<OrderMaster> FindByColumnNameString(string column_name, string find_val)
        {
            List<OrderMaster> list = new List<OrderMaster>();

            if (column_name.Length == 0 || find_val.Length == 0)
            {
                return list;
            }

            string cmd = "select * from ＮＴオーダーマスター t " +
                " where " + column_name + " like '" + find_val + "'" +
                " order by t.オーダーコード";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(OrderMaster.GetFromStdClass(tmp));
            }

            return list;
        }


        /// <summary>
        /// カラム名を指定して数値型で検索する。
        /// </summary>
        /// <param name="find_val">検索する値</param>
        /// <returns></returns>
        public static List<OrderMaster> FindByColumnNameNumber(string column_name, string find_val)
        {
            List<OrderMaster> list = new List<OrderMaster>();

            if (column_name.Length == 0 || find_val.Length == 0)
            {
                return list;
            }

            string cmd = "select * from ＮＴオーダーマスター t " +
                " where " + column_name + " = " + find_val +
                " order by t.オーダーコード";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(OrderMaster.GetFromStdClass(tmp));
            }

            return list;
        }


        public static List<OrderMaster> FindByName(string name)
        {
            List<OrderMaster> list = new List<OrderMaster>();

            if (name.Length == 0)
            {
                return list;
            }

            string cmd = "select * from ＮＴオーダーマスター t " +
                " where t.検索コード０１ like '%" + name + "%'" +
                " or t.検索コード０２ like '%" + name + "%'" +
                " or t.検索コード０３ like '%" + name + "%'" +
                " order by t.オーダーコード";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }


        public static List<OrderMaster> FindByNames(List<string> name_list)
        {
            List<OrderMaster> list = new List<OrderMaster>();

            if (name_list.Count == 0)
            {
                return list;
            }

            string ss = "";

            foreach (string s in name_list)
            {
                if (ss.Length > 0)
                {
                    ss += " or ";
                }

                ss += " t.検索コード０１ like '%" + s + "%'" +
                    " or t.検索コード０２ like '%" + s + "%'" +
                    " or t.検索コード０３ like '%" + s + "%' ";
            }

            string cmd = "select * from ＮＴオーダーマスター t " +
                " where " + ss;

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }

        static OrderMaster GetFromStdClass(StdClass tmp)
        {
            OrderMaster obj = new OrderMaster();

            obj.OrderCode = tmp.DataDict["オーダーコード"].ToString().Trim();
            obj.FullName = tmp.DataDict["名称"].ToString().Trim();
            obj.ShortName = tmp.DataDict["略称"].ToString().Trim();
            obj.Unit = tmp.DataDict["単位"].ToString().Trim();
            obj.InputUnit = tmp.DataDict["入力単位"].ToString().Trim();
            obj.SekouDeptCode = tmp.DataDict["施行部署コード"].ToString();

            for (int i = 1; i < obj.FindCodes.Length; i++)
            {
                obj.FindCodes[i] = tmp.DataDict["検索コード" + AppString.HanToZen(i.ToString().PadLeft(2, '0'))].ToString();
            }

            obj.KensaCode = tmp.DataDict["札臨コード"].ToString();

            int.TryParse(tmp.DataDict["連番"].ToString(), out obj.SEQ);
            int.TryParse(tmp.DataDict["文字数"].ToString(), out obj.Chars);

            for (int i = 1; i < obj.KouiCodes.Length; i++)
            {
                obj.KouiCodes[i] = 0;
                int.TryParse(tmp.DataDict["診療区分" + AppString.HanToZen(i.ToString().PadLeft(2, '0'))].ToString(), out obj.KouiCodes[i]);
            }

            int.TryParse(tmp.DataDict["固定数量"].ToString(), out obj.FixedQty);
            int.TryParse(tmp.DataDict["固定回数"].ToString(), out obj.FixedTimes);
            int.TryParse(tmp.DataDict["データ区分"].ToString(), out obj.Kind);
            int.TryParse(tmp.DataDict["数量編集"].ToString(), out obj.QtyEdit);
            int.TryParse(tmp.DataDict["請求不可フラグ"].ToString(), out obj.NoBillFlg);
            int.TryParse(tmp.DataDict["施行確認フラグ"].ToString(), out obj.SekouVerifyFlg);
            int.TryParse(tmp.DataDict["検索区分"].ToString(), out obj.FindKind);

            for (int i = 1; i < obj.EtcFlgs.Length; i++)
            {
                obj.EtcFlgs[i] = 0;
                int.TryParse(tmp.DataDict["予備フラグ" + AppString.HanToZen(i.ToString())].ToString(), out obj.EtcFlgs[i]);
            }

            for (int i = 1; i < obj.ShowFlgs.Length; i++)
            {
                obj.ShowFlgs[i] = 0;
                int.TryParse(tmp.DataDict["表示フラグ" + AppString.HanToZen(i.ToString())].ToString(), out obj.ShowFlgs[i]);
            }

            float.TryParse(tmp.DataDict["乗算換算"].ToString(), out obj.MultiValue);
            float.TryParse(tmp.DataDict["除算換算"].ToString(), out obj.DivideValue);

            for (int i = 1; i < obj.OutCodes.Length; i++)
            {
                obj.OutCodes[i] = 0;
                int.TryParse(tmp.DataDict["外部コード" + AppString.HanToZen(i.ToString())].ToString(), out obj.OutCodes[i]);
            }

            return obj;
        }
    }
}
