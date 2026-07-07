using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// 看護指示データ
    /// </summary>
    public class NursingOrderData : StdEntity
    {
        /// <summary>
        /// 患者コード
        /// </summary>
        public string PtId = "";

        /// <summary>
        /// 日付
        /// </summary>
        public int OrderDate = 0;

        /// <summary>
        /// 指示コード
        /// </summary>
        public int Code1 = 0;

        /// <summary>
        /// 連番
        /// </summary>
        public int Code2 = 0;

        /// <summary>
        /// 選択値（すべて 1 に固定？）
        /// </summary>
        public int Value1 = 1;

        /// <summary>
        /// 入力値
        /// </summary>
        public string Cont1 = "";

        public static List<int> GetDateList(string pt_id)
        {
            List<int> list = new List<int>();

            if (pt_id.Length == 0)
            {
                return list;
            }

            string cmd = "select distinct 日付 from PATH看護指示データ " +
                " where 患者コード = " + pt_id +
                " order by 日付 desc";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(int.Parse(tmp.DataDict["日付"].ToString()));
            }

            return list;
        }

        public static List<int> GetDateListByDates(string pt_id, int date1, int date2)
        {
            List<int> list = new List<int>();

            if (pt_id.Length == 0)
            {
                return list;
            }

            string cmd = "select distinct 日付 from PATH看護指示データ " +
                " where 患者コード = " + pt_id +
                " and 日付 >= " + date1 + " and 日付 <= " + date2 +
                " order by 日付 desc";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(int.Parse(tmp.DataDict["日付"].ToString()));
            }

            return list;
        }

        /// <summary>
        /// 指定患者・指定日時点で有効な看護指示の日付を取得。
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="date1"></param>
        /// <returns></returns>
        public static int GetPrevDateByDate(string pt_id, int date1)
        {
            int date = 0;

            if (pt_id.Length == 0)
            {
                return date;
            }

            string cmd = "select max(日付) 日付 from " +
                " (select distinct 日付 from PATH看護指示データ " +
                " where 患者コード = " + pt_id +
                " and 日付 <= " + date1 + ")";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                int.TryParse(tmp.DataDict["日付"].ToString(), out date);
                break;
            }

            return date;
        }

        /// <summary>
        /// 指定患者・指定日の看護指示を取得。
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="order_date">yyyyMMdd</param>
        /// <returns></returns>
        public static Dictionary<int, Dictionary<int, NursingOrderData>> LoadByDate(string pt_id, int order_date)
        {
            Dictionary<int, Dictionary<int, NursingOrderData>> dict = new Dictionary<int, Dictionary<int, NursingOrderData>>();

            if (pt_id.Length == 0)
            {
                return dict;
            }

            StdDbClass tmp_db = new StdDbClass(Db);

            tmp_db.Table = "PATH看護指示データ";
            tmp_db.WhereList.Add("患者コード = " + pt_id);
            tmp_db.WhereList.Add("日付 = " + order_date);
            tmp_db.OrderByList.Add("指示コード");

            List<StdClass> tmp_list = tmp_db.SelectSQL();

            foreach (StdClass tmp in tmp_list)
            {
                NursingOrderData obj = new NursingOrderData();

                obj.PtId = tmp.DataDict["患者コード"].ToString();
                int.TryParse(tmp.DataDict["日付"].ToString(), out obj.OrderDate);
                int.TryParse(tmp.DataDict["指示コード"].ToString(), out obj.Code1);
                int.TryParse(tmp.DataDict["連番"].ToString(), out obj.Code2);
                obj.Cont1 = tmp.DataDict["入力値"].ToString();

                if (dict.ContainsKey(obj.Code1))
                {
                    dict[obj.Code1].Add(obj.Code2, obj);
                }
                else
                {
                    Dictionary<int, NursingOrderData> dict2 = new Dictionary<int, NursingOrderData>();
                    dict2.Add(obj.Code2, obj);
                    dict.Add(obj.Code1, dict2);
                }
            }

            return dict;
        }

        /// <summary>
        /// 指定患者・指定期間の看護指示を取得。
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="date1">yyyyMMdd</param>
        /// <param name="date2">yyyyMMdd</param>
        /// <returns></returns>
        public static Dictionary<int, Dictionary<int, Dictionary<int, NursingOrderData>>> LoadByDates(string pt_id, int date1, int date2)
        {
            Dictionary<int, Dictionary<int, Dictionary<int, NursingOrderData>>> dict = new Dictionary<int, Dictionary<int, Dictionary<int, NursingOrderData>>>();

            if (pt_id.Length == 0)
            {
                return dict;
            }

            StdDbClass tmp_db = new StdDbClass(Db);

            tmp_db.Table = "PATH看護指示データ";
            tmp_db.WhereList.Add("患者コード = " + pt_id);
            tmp_db.WhereList.Add("日付 >= " + date1);
            tmp_db.WhereList.Add("日付 <= " + date2);
            tmp_db.OrderByList.Add("日付");
            tmp_db.OrderByList.Add("指示コード");

            List<StdClass> tmp_list = tmp_db.SelectSQL();

            foreach (StdClass tmp in tmp_list)
            {
                NursingOrderData obj = new NursingOrderData();

                obj.PtId = tmp.DataDict["患者コード"].ToString();
                int.TryParse(tmp.DataDict["日付"].ToString(), out obj.OrderDate);
                int.TryParse(tmp.DataDict["指示コード"].ToString(), out obj.Code1);
                int.TryParse(tmp.DataDict["連番"].ToString(), out obj.Code2);
                obj.Cont1 = tmp.DataDict["入力値"].ToString();

                Dictionary<int, Dictionary<int, NursingOrderData>> dict2 = new Dictionary<int, Dictionary<int, NursingOrderData>>();
                Dictionary<int, NursingOrderData> dict3 = new Dictionary<int, NursingOrderData>();

                if (dict.ContainsKey(obj.OrderDate))
                {
                    dict2 = dict[obj.OrderDate];
                }
                else
                {
                    dict.Add(obj.OrderDate, dict2);
                }

                if (dict2.ContainsKey(obj.Code1))
                {
                    dict3 = dict2[obj.Code1];
                }
                else
                {
                    dict2.Add(obj.Code1, dict3);
                }

                dict3.Add(obj.Code2, obj);
            }

            return dict;
        }


        /// <summary>
        /// 指定患者・指定日時点で有効な看護指示を取得。
        /// 通常はゼロか１つ。
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="crit_date">yyyyMMdd</param>
        /// <returns></returns>
        public static Dictionary<int, Dictionary<int, NursingOrderData>> LoadPrevByDate(string pt_id, int crit_date)
        {
            Dictionary<int, Dictionary<int, NursingOrderData>> dict = new Dictionary<int, Dictionary<int, NursingOrderData>>();

            if (pt_id.Length == 0 || crit_date.ToString().Length != 8)
            {
                return dict;
            }

            string cmd = "select * from macs.PATH看護指示データ tt " +
                " where tt.患者コード = " + pt_id +
                " and tt.日付 = " +
                " (select max(t.日付) from macs.PATH看護指示データ t " +
                " where t.患者コード = " + pt_id +
                " and t.日付 <= " + crit_date +
                " group by t.患者コード) " +
                " order by tt.指示コード, tt.連番";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                NursingOrderData obj = new NursingOrderData();

                obj.PtId = tmp.DataDict["患者コード"].ToString();
                int.TryParse(tmp.DataDict["日付"].ToString(), out obj.OrderDate);
                int.TryParse(tmp.DataDict["指示コード"].ToString(), out obj.Code1);
                int.TryParse(tmp.DataDict["連番"].ToString(), out obj.Code2);
                obj.Cont1 = tmp.DataDict["入力値"].ToString();

                if (dict.ContainsKey(obj.Code1))
                {
                    dict[obj.Code1].Add(obj.Code2, obj);
                }
                else
                {
                    Dictionary<int, NursingOrderData> dict2 = new Dictionary<int, NursingOrderData>();
                    dict2.Add(obj.Code2, obj);
                    dict.Add(obj.Code1, dict2);
                }
            }

            return dict;
        }

        /// <summary>
        /// 保存。看護指示データの１つの指示コードのみ。
        /// Insert のみ実行するので、事前に Delete されていることが前提。
        /// </summary>
        /// <returns></returns>
        public StdReturn Save()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();

            obj.Table = "PATH看護指示データ";

            obj.DataList.Add(new StdDbColumn("患者コード", StdDbType.NUMBER, this.PtId));
            obj.DataList.Add(new StdDbColumn("日付", StdDbType.NUMBER, this.OrderDate));
            obj.DataList.Add(new StdDbColumn("指示コード", StdDbType.NUMBER, this.Code1));
            obj.DataList.Add(new StdDbColumn("連番", StdDbType.NUMBER, this.Code2));
            obj.DataList.Add(new StdDbColumn("選択値", StdDbType.NUMBER, this.Value1));
            obj.DataList.Add(new StdDbColumn("入力値", StdDbType.VARCHAR2, this.Cont1));

            // データが存在しないことが前提
            sr = obj.InsertSQL(false);

            return sr;
        }

        /// <summary>
        /// 削除。
        /// 保存とは異なり、指定患者・指定日の指示を一括で削除する。
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="order_date"></param>
        /// <returns></returns>
        public static StdReturn Delete(string pt_id, int order_date)
        {
            StdReturn sr = new StdReturn();

            if (pt_id.Length == 0)
            {
                return sr;
            }

            string cmd = "delete from macs.PATH看護指示データ " +
                " where 患者コード = " + pt_id +
                " and 日付 = " + order_date;

            sr.Msgs.Add(cmd);
            /*
                        Db.Open();
                        sr.IntValue = Db.ExecuteNonQuery(cmd);
                        Db.Close();
            */
            return sr;
        }
    }
}
