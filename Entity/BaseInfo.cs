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
                obj.Name2 = tmp.DataDict["ITEM_TITLE2"].ToString();
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
                obj.Name2 = tmp.DataDict["ITEM_TITLE2"].ToString();
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
            return dict;
        }
    }
}
