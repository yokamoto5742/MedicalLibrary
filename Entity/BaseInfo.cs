using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// 患者基本情報
    /// ADT_患者基本情報データ
    /// </summary>
    public class BaseInfo : StdKarte1
    {
        /// <summary>
        /// 項目ユニークキー
        /// </summary>
        public string ItemKey = "";

        /// <summary>
        /// 連番
        /// </summary>
        public int SEQ = 1;

        /// <summary>
        /// 項目名タイトル１
        /// </summary>
        public string Name1 = "";

        /// <summary>
        /// 項目名タイトル２
        /// </summary>
        public string Name2 = "";

        /// <summary>
        /// データ種類（9で固定）
        /// </summary>
        public int DataKind = 9;

        /// <summary>
        /// データ値
        /// </summary>
        public string Value = "";

        /// <summary>
        /// キーは「患者コード」->「項目ユニークキー」
        /// それぞれのキーに対して
        /// テキストボックス・コンボボックスの場合は、１個だけのリスト
        /// チェックボックスの場合は、複数個のリスト（１個しか選択されていなければ１個）
        /// </summary>
        /// <param name="pt_list"></param>
        /// <returns></returns>
        public static Dictionary<string, Dictionary<string, List<BaseInfo>>> GetDict(List<string> pt_list)
        {
            Dictionary<string, Dictionary<string, List<BaseInfo>>> dict = new Dictionary<string, Dictionary<string, List<BaseInfo>>>();

            if (pt_list.Count == 0)
            {
                return dict;
            }
#if INNO
            // データの取得
            string cmd = "select * from " +
                " (select td.*, tg.ITEM_TITLE1, tg.ITEM_TITLE2 " +
                "  , row_number() over (partition by td.P_ID, td.ITEM_KEY order by td.REG_DATE desc, td.REG_TIME desc) rn " +
                "  from D_PATIENTINFO_DATA td, D_GENERAL_INPUTCTL tg " +
                "  where td.P_ID in (" + AppString.ConcatList(pt_list, ",") + ") " +
                "  and (tg.DISPNO || '-' || tg.TABNO || '-' || tg.ROW_ORDER) = td.ITEM_KEY) t " +
                " where t.RN = 1";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                BaseInfo obj = new BaseInfo();

                obj.PtId = tmp.DataDict["P_ID"].ToString();
                obj.ItemKey = tmp.DataDict["ITEM_KEY"].ToString();
                int.TryParse(tmp.DataDict["SEQ"].ToString(), out obj.SEQ);
                obj.Name1 = tmp.DataDict["ITEM_TITLE1"].ToString();
                obj.Name1 = tmp.DataDict["ITEM_TITLE2"].ToString();
                obj.Value = tmp.DataDict["DATA"].ToString();

                obj.BaseFromStdClass(tmp);

                if (!dict.ContainsKey(obj.PtId))
                {
                    Dictionary<string, List<BaseInfo>> dict2 = new Dictionary<string, List<BaseInfo>>();

                    List<BaseInfo> list = new List<BaseInfo>();
                    list.Add(obj);
                    dict2.Add(obj.ItemKey, list);

                    dict.Add(obj.PtId, dict2);
                }
                else
                {
                    Dictionary<string, List<BaseInfo>> dict2 = dict[obj.PtId];

                    if (!dict2.ContainsKey(obj.ItemKey))
                    {
                        List<BaseInfo> list = new List<BaseInfo>();
                        list.Add(obj);
                        dict2.Add(obj.ItemKey, list);
                    }
                    else
                    {
                        List<BaseInfo> list = dict2[obj.ItemKey];
                        list.Add(obj);
                    }
                }
            }
#else
            // データの取得
            string cmd = "select * from ADT_患者基本情報データ t " +
                " where t.患者コード in (" + AppString.ConcatList(pt_list, ",") + ") " +
                " order by t.患者コード, t.項目ユニークキー, t.連番";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                BaseInfo obj = new BaseInfo();

                obj.PtId = tmp.DataDict["患者コード"].ToString();
                obj.ItemKey = tmp.DataDict["項目ユニークキー"].ToString();
                int.TryParse(tmp.DataDict["連番"].ToString(), out obj.SEQ);
                obj.Name1 = tmp.DataDict["項目名タイトル１"].ToString();
                obj.Name1 = tmp.DataDict["項目名タイトル２"].ToString();
                int.TryParse(tmp.DataDict["データ種類"].ToString(), out obj.DataKind);
                obj.Value = tmp.DataDict["データ値"].ToString();

                obj.BaseFromStdClass(tmp);

                if (!dict.ContainsKey(obj.PtId))
                {
                    Dictionary<string, List<BaseInfo>> dict2 = new Dictionary<string, List<BaseInfo>>();

                    List<BaseInfo> list = new List<BaseInfo>();
                    list.Add(obj);
                    dict2.Add(obj.ItemKey, list);

                    dict.Add(obj.PtId, dict2);
                }
                else
                {
                    Dictionary<string, List<BaseInfo>> dict2 = dict[obj.PtId];

                    if (!dict2.ContainsKey(obj.ItemKey))
                    {
                        List<BaseInfo> list = new List<BaseInfo>();
                        list.Add(obj);
                        dict2.Add(obj.ItemKey, list);
                    }
                    else
                    {
                        List<BaseInfo> list = dict2[obj.ItemKey];
                        list.Add(obj);
                    }
                }
            }
#endif
            return dict;
        }


        /// <summary>
        /// キーは「項目ユニークキー」
        /// それぞれのキーに対して
        /// テキストボックス・コンボボックスの場合は、１個だけのリスト
        /// チェックボックスの場合は、複数個のリスト（１個しか選択されていなければ１個）
        /// </summary>
        /// <param name="pt_id"></param>
        /// <returns></returns>
        public static Dictionary<string, List<BaseInfo>> GetDict(string pt_id)
        {
            Dictionary<string, List<BaseInfo>> dict = new Dictionary<string, List<BaseInfo>>();

            if (pt_id.Length == 0)
            {
                return dict;
            }
#if INNO
            // データの取得
            string cmd = "select * from " +
                " (select td.*, tg.ITEM_TITLE1, tg.ITEM_TITLE2 " +
                "  , row_number() over (partition by td.P_ID, td.ITEM_KEY order by td.REG_DATE desc, td.REG_TIME desc) rn " +
                "  from D_PATIENTINFO_DATA td, D_GENERAL_INPUTCTL tg " +
                "  where td.P_ID = " + pt_id +
                "  and (tg.DISPNO || '-' || tg.TABNO || '-' || tg.ROW_ORDER) = td.ITEM_KEY) t " +
                " where t.RN = 1";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                BaseInfo obj = new BaseInfo();

                obj.PtId = tmp.DataDict["P_ID"].ToString();
                obj.ItemKey = tmp.DataDict["ITEM_KEY"].ToString();
                int.TryParse(tmp.DataDict["SEQ"].ToString(), out obj.SEQ);
                obj.Name1 = tmp.DataDict["ITEM_TITLE1"].ToString();
                obj.Name1 = tmp.DataDict["ITEM_TITLE2"].ToString();
                obj.Value = tmp.DataDict["DATA"].ToString();

                obj.BaseFromStdClass(tmp);

                if (!dict.ContainsKey(obj.ItemKey))
                {
                    List<BaseInfo> list = new List<BaseInfo>();
                    list.Add(obj);
                    dict.Add(obj.ItemKey, list);
                }
                else
                {
                    List<BaseInfo> list = dict[obj.ItemKey];
                    list.Add(obj);
                }
            }
#else
            // データの取得
            string cmd = "select * from ADT_患者基本情報データ t " +
                " where t.患者コード = " + pt_id +
                " order by t.患者コード, t.項目ユニークキー, t.連番";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                BaseInfo obj = new BaseInfo();

                obj.PtId = pt_id;
                obj.ItemKey = tmp.DataDict["項目ユニークキー"].ToString();
                int.TryParse(tmp.DataDict["連番"].ToString(), out obj.SEQ);
                obj.Name1 = tmp.DataDict["項目名タイトル１"].ToString();
                obj.Name1 = tmp.DataDict["項目名タイトル２"].ToString();
                int.TryParse(tmp.DataDict["データ種類"].ToString(), out obj.DataKind);
                obj.Value = tmp.DataDict["データ値"].ToString();

                obj.BaseFromStdClass(tmp);

                if (!dict.ContainsKey(obj.ItemKey))
                {
                    List<BaseInfo> list = new List<BaseInfo>();
                    list.Add(obj);
                    dict.Add(obj.ItemKey, list);
                }
                else
                {
                    List<BaseInfo> list = dict[obj.ItemKey];
                    list.Add(obj);
                }
            }
#endif
            return dict;
        }

        public static StdReturn Save(string pt_id, string tab_code, List<BaseInfo> list)
        {
            StdReturn sr = new StdReturn();

            if (pt_id.Length == 0 || tab_code.Length == 0)
            {
                return sr;
            }
#if INNO
#else
            List<BaseInfoItemMaster> mlist = new List<BaseInfoItemMaster>();

            if (BaseInfoItemMaster.Dict.ContainsKey(tab_code))
            {
                mlist = BaseInfoItemMaster.Dict[tab_code];
            }
            else
            {
                // タブコードが存在しなければ終了
                return sr;
            }

            // 該当タブのデータをＬＧに挿入した上で削除する
            // （ＬＧ挿入は未実装）

            string cmd = "delete from ADT_患者基本情報データ t " +
                " where t.患者コード = " + pt_id +
                " and t.項目ユニークキー in " +
                " (select tc.項目ユニークキー from AMC_汎用入力画面ＣＴＬ tc " +
                "  where tc.画面ＮＯ = 150 and tc.ＴＡＢＮＯ = " + tab_code + ")";

            Db.ExecuteNonQuery(cmd);


            // データを新規挿入する
            StdDbClass obj = new StdDbClass();

            obj.Table = "ADT_患者基本情報データ";

            string reg_date = DateTime.Now.ToString("yyyyMMdd");
            string reg_time = DateTime.Now.ToString("HHmmss");

            foreach (BaseInfo data in list)
            {
                obj.DataList.Clear();

                obj.DataList.Add(new StdDbColumn("患者コード", StdDbType.NUMBER, pt_id));
                obj.DataList.Add(new StdDbColumn("項目ユニークキー", StdDbType.VARCHAR2, data.ItemKey));
                obj.DataList.Add(new StdDbColumn("連番", StdDbType.NUMBER, data.SEQ));
                obj.DataList.Add(new StdDbColumn("項目名タイトル１", StdDbType.VARCHAR2, data.Name1));
                obj.DataList.Add(new StdDbColumn("項目名タイトル２", StdDbType.VARCHAR2, data.Name2));
                obj.DataList.Add(new StdDbColumn("データ種類", StdDbType.NUMBER, data.DataKind));
                obj.DataList.Add(new StdDbColumn("データ値", StdDbType.VARCHAR2, data.Value));

                obj.DataList.Add(new StdDbColumn("登録日", StdDbType.NUMBER, reg_date));
                obj.DataList.Add(new StdDbColumn("登録時間", StdDbType.NUMBER, reg_time));
                obj.DataList.Add(new StdDbColumn("登録者", StdDbType.NUMBER, LoginUser.Id));
                obj.DataList.Add(new StdDbColumn("代行登録者", StdDbType.NUMBER, LoginUser.Id2));
                obj.DataList.Add(new StdDbColumn("更新日", StdDbType.NUMBER, reg_date));
                obj.DataList.Add(new StdDbColumn("更新時間", StdDbType.NUMBER, reg_time));
                obj.DataList.Add(new StdDbColumn("更新者", StdDbType.NUMBER, LoginUser.Id));
                obj.DataList.Add(new StdDbColumn("代行更新者", StdDbType.NUMBER, LoginUser.Id2));

                sr = obj.InsertSQL();
            }
#endif
            return sr;
        }
    }

    /// <summary>
    /// ADT_患者固定情報データ
    /// </summary>
    public class BaseInfoFixed : StdKarte1
    {
        /// <summary>
        /// 該当患者の ADT_患者固定情報データ
        /// </summary>
        /// <param name="pt_id"></param>
        /// <returns></returns>
        public static StdClass GetData(string pt_id)
        {
            StdClass obj = new StdClass();

            if (pt_id.Length == 0)
            {
                return obj;
            }

#if INNO
            string cmd = "select * from D_PATIENT_BASE t " +
                " where t.P_ID = " + pt_id;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
#else
            string cmd = "select * from macs.ADT_患者固定情報データ t " +
                " where t.患者コード = " + pt_id;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif
            if (tmp_list.Count > 0)
            {
                obj = tmp_list[0];
            }
            
            return obj;
        }

        /// <summary>
        /// 該当日の外来患者の ADT_患者固定情報データ
        /// </summary>
        /// <param name="do_date"></param>
        /// <returns></returns>
        public static List<StdClass> GetListByOutDate(string do_date)
        {
            List<StdClass> list = new List<StdClass>();

            if (do_date.Length != 8)
            {
                return list;
            }
#if INNO
            string cmd = "select * from D_PATIENT_BASE t " +
                " where (t.P_ID) in " +
                " (select ts.P_ID from D_UKETSUKE ts " +
                " where ts.UKE_DATE = " + do_date + ")";

            list = StdClass.GetList(DB.Db3, cmd);
#else
            string cmd = "select * from macs.ADT_患者固定情報データ t " +
                " where (t.患者コード) in " +
                " (select ts.患者コード from macs.ADT_診察状況データ ts " +
                " where ts.受付日 = " + do_date + ")";

            list = StdClass.GetList(DB.Db1, cmd);
#endif
            return list;
        }

        public static StdReturn Save(string pt_id, Dictionary<string, string> dict)
        {
            StdReturn sr = new StdReturn();

            if (pt_id.Length == 0)
            {
                return sr;
            }

#if INNO
#else
            StdDbClass obj = new StdDbClass();

            obj.Db = DB.Db1;
            obj.Table = "ADT_患者固定情報データ";

            string reg_date = DateTime.Now.ToString("yyyyMMdd");
            string reg_time = DateTime.Now.ToString("HHmmss");

            foreach (BaseInfoFixedMaster fm in BaseInfoFixedMaster.List)
            {
                if (dict.ContainsKey(fm.Code + "_1"))
                {
                    obj.DataList.Add(new StdDbColumn(fm.DbColumnName1, fm.DbColumnType1, dict[fm.Code + "_1"]));
                }

                if (dict.ContainsKey(fm.Code + "_2"))
                {
                    obj.DataList.Add(new StdDbColumn(fm.DbColumnName2, fm.DbColumnType2, dict[fm.Code + "_2"]));
                }
            }

            obj.DataList.Add(new StdDbColumn("更新日", StdDbType.NUMBER, reg_date));
            obj.DataList.Add(new StdDbColumn("更新時間", StdDbType.NUMBER, reg_time));
            obj.DataList.Add(new StdDbColumn("更新者", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("代行更新者", StdDbType.NUMBER, LoginUser.Id2));

            obj.WhereList.Add("患者コード = " + pt_id);

            sr = obj.UpdateSQL();

            if (sr.IntValue == 0)
            {
                obj.DataList.Add(new StdDbColumn("患者コード", StdDbType.NUMBER, pt_id));

                obj.DataList.Add(new StdDbColumn("登録日", StdDbType.NUMBER, reg_date));
                obj.DataList.Add(new StdDbColumn("登録時間", StdDbType.NUMBER, reg_time));
                obj.DataList.Add(new StdDbColumn("登録者", StdDbType.NUMBER, LoginUser.Id));
                obj.DataList.Add(new StdDbColumn("代行登録者", StdDbType.NUMBER, LoginUser.Id2));

                sr = obj.InsertSQL();
            }
#endif
            return sr;
        }
    }
}
