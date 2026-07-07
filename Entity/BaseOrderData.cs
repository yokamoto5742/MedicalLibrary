using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// 基本的指示事項データ
    /// </summary>
    public class BaseOrderData : StdEntity
    {
        /// <summary>
        /// 患者コード
        /// </summary>
        public string PtId = "";

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


        public static List<BaseOrderData> GetList(string pt_id)
        {
            List<BaseOrderData> list = new List<BaseOrderData>();

            if (pt_id.Length == 0)
            {
                return list;
            }

            /*
            string cmd = "select * from macs.ＮＴ約束処方２ t " +
                " where t.患者コード = " + pt_id +
                " order by t.指示日 desc";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);
            */

            StdDbClass tmp_db = new StdDbClass(Db);

            tmp_db.Table = "ＮＴ約束処方２";
            tmp_db.WhereList.Add("患者コード = " + pt_id);
            tmp_db.OrderByList.Add("指示日 desc");

            List<StdClass> tmp_list = tmp_db.SelectSQL();

            foreach (StdClass tmp in tmp_list)
            {
                BaseOrderData obj = new BaseOrderData();

                obj.PtId = pt_id;
                int.TryParse(tmp.DataDict["指示日"].ToString(), out obj.OrderDate);
                obj.Cont1 = tmp.DataDict["その他"].ToString();

                for (int i = 1; i <= 20; i++)
                {
                    BaseOrderDataItem item = new BaseOrderDataItem();
                    item.SEQ = i;

                    string tmp_i = AppString.HanToZen(i.ToString());

                    item.Name = tmp.GetDataString("約束名称" + tmp_i);
                    item.Value1 = tmp.GetDataString("約束条件" + tmp_i);
                    item.Value2 = tmp.GetDataString("約束処方" + tmp_i);

                    if (!obj.ItemDict.ContainsKey(i))
                    {
                        obj.ItemDict.Add(i, item);
                    }
                }

                list.Add(obj);
            }

            return list;
        }


        /// <summary>
        /// 指定患者・指定日の基本的指示事項を取得。
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="order_date">yyyyMMdd</param>
        /// <returns></returns>
        public static BaseOrderData LoadByDate(string pt_id, int order_date)
        {
            BaseOrderData obj = new BaseOrderData();

            if (pt_id.Length == 0 || order_date.ToString().Length != 8)
            {
                return obj;
            }

            /*
            string cmd = "select * from macs.ＮＴ約束処方２ t " +
                " where t.患者コード = " + pt_id +
                " and t.指示日 = " + order_date +
                " order by t.指示日 desc";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);
            */

            StdDbClass tmp_db = new StdDbClass(Db);

            tmp_db.Table = "ＮＴ約束処方２";
            tmp_db.WhereList.Add("患者コード = " + pt_id);
            tmp_db.WhereList.Add("指示日 = " + order_date);
            tmp_db.OrderByList.Add("指示日 desc");

            List<StdClass> tmp_list = tmp_db.SelectSQL();

            foreach (StdClass tmp in tmp_list)
            {
                obj = new BaseOrderData();

                obj.PtId = pt_id;
                int.TryParse(tmp.DataDict["指示日"].ToString(), out obj.OrderDate);
                obj.Cont1 = tmp.DataDict["その他"].ToString();

                for (int i = 1; i <= 20; i++)
                {
                    BaseOrderDataItem item = new BaseOrderDataItem();
                    item.SEQ = i;

                    string tmp_i = AppString.HanToZen(i.ToString());

                    item.Name = tmp.GetDataString("約束名称" + tmp_i);
                    item.Value1 = tmp.GetDataString("約束条件" + tmp_i);
                    item.Value2 = tmp.GetDataString("約束処方" + tmp_i);

                    if (!obj.ItemDict.ContainsKey(i))
                    {
                        obj.ItemDict.Add(i, item);
                    }
                }

                break;
            }

            return obj;
        }


        /// <summary>
        /// 指定患者・指定期間の基本的指示事項を取得。
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="date1">yyyyMMdd</param>
        /// <param name="date2">yyyyMMdd</param>
        /// <returns></returns>
        public static Dictionary<int, BaseOrderData> LoadByDates(string pt_id, int date1, int date2)
        {
            Dictionary<int, BaseOrderData> dict = new Dictionary<int, BaseOrderData>();

            if (pt_id.Length == 0 || date1.ToString().Length != 8 || date2.ToString().Length != 8)
            {
                return dict;
            }

            /*
            string cmd = "select * from macs.ＮＴ約束処方２ t " +
                " where t.患者コード = " + pt_id +
                " and t.指示日 = " + order_date +
                " order by t.指示日 desc";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);
            */

            StdDbClass tmp_db = new StdDbClass(Db);

            tmp_db.Table = "ＮＴ約束処方２";
            tmp_db.WhereList.Add("患者コード = " + pt_id);
            tmp_db.WhereList.Add("指示日 >= " + date1);
            tmp_db.WhereList.Add("指示日 <= " + date2);
            tmp_db.OrderByList.Add("指示日");

            List<StdClass> tmp_list = tmp_db.SelectSQL();

            foreach (StdClass tmp in tmp_list)
            {
                BaseOrderData obj = new BaseOrderData();

                obj.PtId = pt_id;
                int.TryParse(tmp.DataDict["指示日"].ToString(), out obj.OrderDate);
                obj.Cont1 = tmp.DataDict["その他"].ToString();

                for (int i = 1; i <= 20; i++)
                {
                    BaseOrderDataItem item = new BaseOrderDataItem();
                    item.SEQ = i;

                    string tmp_i = AppString.HanToZen(i.ToString());

                    item.Name = tmp.GetDataString("約束名称" + tmp_i);
                    item.Value1 = tmp.GetDataString("約束条件" + tmp_i);
                    item.Value2 = tmp.GetDataString("約束処方" + tmp_i);

                    if (!obj.ItemDict.ContainsKey(i))
                    {
                        obj.ItemDict.Add(i, item);
                    }
                }

                dict.Add(obj.OrderDate, obj);
            }

            return dict;
        }


        /// <summary>
        /// 指定患者・指定日時点で有効な基本的指示事項を取得。
        /// 通常はゼロか１つ。
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="crit_date">yyyyMMdd</param>
        /// <returns></returns>
        public static BaseOrderData LoadPrevByDate(string pt_id, int crit_date)
        {
            BaseOrderData obj = new BaseOrderData();

            if (pt_id.Length == 0 || crit_date.ToString().Length != 8)
            {
                return obj;
            }

            List<BaseOrderData> tmp_list = BaseOrderData.GetList(pt_id);

            foreach (BaseOrderData tmp in tmp_list)
            {
                if (tmp.OrderDate > crit_date)
                {
                    continue;
                }

                // 当日以前のデータがあれば、それが該当する。
                obj = tmp;
                break;
            }

            return obj;
        }

        public StdReturn Save()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();

            obj.Table = "ＮＴ約束処方２";

            obj.DataList.Add(new StdDbColumn("区分", StdDbType.NUMBER, 1));
            obj.DataList.Add(new StdDbColumn("その他", StdDbType.VARCHAR2, this.Cont1));

            for (int i = 1; i <= 20; i++)
            {
                obj.DataList.Add(new StdDbColumn("約束名称" + AppString.HanToZen(i.ToString()), StdDbType.VARCHAR2, this.ItemDict[i].Name));
                obj.DataList.Add(new StdDbColumn("約束条件" + AppString.HanToZen(i.ToString()), StdDbType.VARCHAR2, this.ItemDict[i].Value1));
                obj.DataList.Add(new StdDbColumn("約束処方" + AppString.HanToZen(i.ToString()), StdDbType.VARCHAR2, this.ItemDict[i].Value2));
            }

            // すでにデータが存在するか確認
            BaseOrderData tmp_obj = BaseOrderData.LoadByDate(this.PtId, this.OrderDate);

            if (tmp_obj.ItemDict.Count > 0)
            {
                // すでにデータが存在する場合

                obj.WhereList.Add("患者コード = " + this.PtId);
                obj.WhereList.Add("指示日 = " + this.OrderDate);

                sr = obj.UpdateSQL(false);
            }
            else
            {
                // データが存在しない場合

                obj.DataList.Add(new StdDbColumn("患者コード", StdDbType.NUMBER, this.PtId));
                obj.DataList.Add(new StdDbColumn("指示日", StdDbType.NUMBER, this.OrderDate));

                sr = obj.InsertSQL(false);
            }

            return sr;
        }

        public StdReturn Delete()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();

            obj.Table = "ＮＴ約束処方２";

            obj.WhereList.Add("患者コード = " + this.PtId);
            obj.WhereList.Add("指示日 = " + this.OrderDate);

            sr = obj.DeleteSQL(false);

            return sr;
        }
    }

    public class BaseOrderDataItem
    {
        /// <summary>
        /// 番号。1～20
        /// </summary>
        public int SEQ = 0;

        /// <summary>
        /// 約束名称
        /// </summary>
        public string Name = "";

        /// <summary>
        /// 約束条件
        /// </summary>
        public string Value1 = "";

        /// <summary>
        /// 約束処方
        /// </summary>
        public string Value2 = "";
    }
}
