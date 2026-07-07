using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// 手術指示データ
    /// </summary>
    public class OpeOrderData : StdEntity
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

            string cmd = "select distinct 日付 from PATH手術指示データ " +
                " where 患者コード = " + pt_id +
                " order by 日付 desc";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(int.Parse(tmp.DataDict["日付"].ToString()));
            }

            return list;
        }

        /// <summary>
        /// 指定患者・指定日の手術指示を取得。
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="order_date">yyyyMMdd</param>
        /// <returns></returns>
        public static Dictionary<int, Dictionary<int, OpeOrderData>> LoadByDate(string pt_id, int order_date)
        {
            Dictionary<int, Dictionary<int, OpeOrderData>> dict = new Dictionary<int, Dictionary<int, OpeOrderData>>();

            if (pt_id.Length == 0)
            {
                return dict;
            }

            StdDbClass tmp_db = new StdDbClass(Db);

            tmp_db.Table = "PATH手術指示データ";
            tmp_db.WhereList.Add("患者コード = " + pt_id);
            tmp_db.WhereList.Add("日付 = " + order_date);
            tmp_db.OrderByList.Add("指示コード");

            List<StdClass> tmp_list = tmp_db.SelectSQL();

            foreach (StdClass tmp in tmp_list)
            {
                OpeOrderData obj = new OpeOrderData();

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
                    Dictionary<int, OpeOrderData> dict2 = new Dictionary<int, OpeOrderData>();
                    dict2.Add(obj.Code2, obj);
                    dict.Add(obj.Code1, dict2);
                }
            }

            return dict;
        }


        /// <summary>
        /// 指定患者・指定日時点で有効な手術指示を取得。
        /// 通常はゼロか１つ。
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="crit_date">yyyyMMdd</param>
        /// <returns></returns>
        public static Dictionary<int, Dictionary<int, OpeOrderData>> LoadPrevByDate(string pt_id, int crit_date)
        {
            Dictionary<int, Dictionary<int, OpeOrderData>> dict = new Dictionary<int, Dictionary<int, OpeOrderData>>();

            if (pt_id.Length == 0 || crit_date.ToString().Length != 8)
            {
                return dict;
            }

            string cmd = "select * from macs.PATH手術指示データ tt " +
                " where tt.患者コード = " + pt_id +
                " and tt.日付 = " +
                " (select max(t.日付) from macs.PATH手術指示データ t " +
                " where t.患者コード = " + pt_id +
                " and t.日付 <= " + crit_date +
                " group by t.患者コード) " +
                " order by tt.指示コード, tt.連番";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                OpeOrderData obj = new OpeOrderData();

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
                    Dictionary<int, OpeOrderData> dict2 = new Dictionary<int, OpeOrderData>();
                    dict2.Add(obj.Code2, obj);
                    dict.Add(obj.Code1, dict2);
                }
            }

            return dict;
        }

        /// <summary>
        /// 保存。手術指示データの１つの指示コードのみ。
        /// Insert のみ実行するので、事前に Delete されていることが前提。
        /// </summary>
        /// <returns></returns>
        public StdReturn Save()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();

            obj.Table = "PATH手術指示データ";

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

            string cmd = "delete from macs.PATH手術指示データ " +
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
