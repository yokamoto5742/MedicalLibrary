using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class KensaMaster : StdEntity
    {
        /// <summary>
        /// 患者コード
        /// </summary>
        public string PtId = "";

        /// <summary>
        /// 項目番号
        /// </summary>
        public string Code = "";

        /// <summary>
        /// 項目名
        /// </summary>
        public string Name = "";

        /// <summary>
        /// 表示順
        /// </summary>
        public int ShowSEQ = 1;


        /// <summary>
        /// 患者の検査結果の項目名一覧を取得する
        /// </summary>
        /// <param name="pt_id"></param>
        /// <returns></returns>
        public static Dictionary<string, KensaMaster> GetDict(string pt_id)
        {
            Dictionary<string, KensaMaster> dict = new Dictionary<string, KensaMaster>();

            if (pt_id.Length == 0)
            {
                return dict;
            }

            // 検査項目名を取得
#if INNO
            string cmd = "select t.ITEM_NO, m.NAME, max(m.DISP_SEQ) DISP_SEQ " +
                " from D_KENSA_RESULT t, M_KENSA_DISPLAY m " +
                " where t.P_ID = " + pt_id +
                " and t.ITEM_NO = m.CODE " +
                " group by t.ITEM_NO, m.NAME " +
                " order by DISP_SEQ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                KensaMaster obj = new KensaMaster();

                obj.PtId = pt_id;
                obj.Code = tmp.DataDict["ITEM_NO"].ToString();
                obj.Name = AppString.ZenToHan(tmp.DataDict["NAME"].ToString()).TrimEnd(' ', '　');
                int.TryParse(tmp.DataDict["DISP_SEQ"].ToString(), out obj.ShowSEQ);

                dict.Add(obj.Code, obj);
            }
#else
            string cmd = "select t.項目番号, m.項目名, max(m.表示順) 表示順 " +
                " from macs.ＮＴ検査結果データ t, macs.AMB_検査結果表示順マスター m " +
                " where t.患者コード = " + pt_id +
                " and t.項目番号 = to_char(m.項目番号) " +
                " group by t.項目番号, m.項目名 " +
                " order by 表示順";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                KensaMaster obj = new KensaMaster();

                obj.PtId = pt_id;
                obj.Code = tmp.DataDict["項目番号"].ToString();
                obj.Name = AppString.ZenToHan(tmp.DataDict["項目名"].ToString()).TrimEnd(' ', '　');
                int.TryParse(tmp.DataDict["表示順"].ToString(), out obj.ShowSEQ);

                dict.Add(obj.Code, obj);
            }
#endif
            return dict;
        }


        /// <summary>
        /// 患者の検査結果の項目名一覧を取得する
        /// </summary>
        /// <param name="pt_id"></param>
        /// <returns></returns>
        public static Dictionary<string, KensaMaster> GetInfectionDict(string pt_id)
        {
            Dictionary<string, KensaMaster> dict = new Dictionary<string, KensaMaster>();

            if (pt_id.Length == 0)
            {
                return dict;
            }

            // 検査項目名を取得
#if INNO
            string cmd = "select t.ITEM_NO, m.NAME, max(m.DISP_SEQ) DISP_SEQ " +
                " from D_KENSA_RESULT t, M_KENSA_DISPLAY m, M_INFECTION tm " +
                " where t.P_ID = " + pt_id +
                " and t.ITEM_NO = m.CODE " +
                " and to_number(t.ITEM_NO) = tm.CODE " +
                " group by t.ITEM_NO, m.NAME " +
                " order by DISP_SEQ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                KensaMaster obj = new KensaMaster();

                obj.PtId = pt_id;
                obj.Code = tmp.DataDict["ITEM_NO"].ToString();
                obj.Name = AppString.ZenToHan(tmp.DataDict["NAME"].ToString()).TrimEnd(' ', '　');
                int.TryParse(tmp.DataDict["DISP_SEQ"].ToString(), out obj.ShowSEQ);

                dict.Add(obj.Code, obj);
            }
#else
            string cmd = "select t.項目番号, tm.項目名, max(m.表示順) 表示順 " +
                " from macs.ＮＴ検査結果データ t, macs.AMB_検査結果表示順マスター m, AMB_感染症項目マスター tm " +
                " where t.患者コード = " + pt_id +
                " and t.項目番号 = to_char(m.項目番号) " +
                " and to_number(t.項目番号) = tm.感染症コード " +
                " group by t.項目番号, tm.項目名 " +
                " order by 表示順";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                KensaMaster obj = new KensaMaster();

                obj.PtId = pt_id;
                obj.Code = tmp.DataDict["項目番号"].ToString();
                obj.Name = AppString.ZenToHan(tmp.DataDict["項目名"].ToString()).TrimEnd(' ', '　');
                int.TryParse(tmp.DataDict["表示順"].ToString(), out obj.ShowSEQ);

                dict.Add(obj.Code, obj);
            }
#endif
            return dict;
        }
    }

    public class KensaData : StdEntity
    {
        /// <summary>
        /// 患者コード
        /// </summary>
        public string PtId = "";

        /// <summary>
        /// 採取日
        /// </summary>
        public int KensaDate = 0;

        /// <summary>
        /// 項目番号
        /// </summary>
        public string KensaCode = "";

        /// <summary>
        /// 項目名
        /// </summary>
        public string KensaName = "";

        /// <summary>
        /// 単位
        /// </summary>
        public string Unit = "";

        /// <summary>
        /// 院内区分
        /// </summary>
        public int Inside = 0;

        /// <summary>
        /// 検査結果
        /// </summary>
        public string Result = "";

        /// <summary>
        /// 基準値
        /// </summary>
        public string Normal = "";

        /// <summary>
        /// 変更フラグ
        /// 0: 異常なし, 1: 低異常, 2: 高異常
        /// </summary>
        public int ModFlg = 0;

        /// <summary>
        /// 表示順
        /// </summary>
        public int ShowSEQ = 1;


        public static Dictionary<string, Dictionary<string, KensaData>> GetDict(string pt_id)
        {
            Dictionary<string, Dictionary<string, KensaData>> dict = new Dictionary<string, Dictionary<string, KensaData>>();

            if (pt_id.Length == 0)
            {
                return dict;
            }

#if INNO
            string cmd = "select t.P_ID, t.EXAMIN_DATE, t.EXAMIN_TIME, t.ITEM_NO " +
                ", m.NAME, t.RESULT_UNIT, t.RESULT, t.INNAI_TYPE, t.STANDARD, t.CHANGE_FLG, m.DISP_SEQ " +
                " from D_KENSA_RESULT t, M_KENSA_DISPLAY m " +
                " where t.P_ID = " + pt_id +
                " and t.ITEM_NO = m.CODE " +
                " order by t.EXAMIN_DATE desc, m.DISP_SEQ, t.EXAMIN_NO desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            string tmp_date = "";
            Dictionary<string, KensaData> tmp_dict = new Dictionary<string, KensaData>();

            foreach (StdClass tmp in tmp_list)
            {
                KensaData obj = GetFromStdClass(tmp);

                // 採取日ごとにリストを作る
                if (!tmp_date.Equals(tmp.DataDict["EXAMIN_DATE"].ToString()))
                {
                    if (tmp_date.Length > 0)
                    {
                        dict.Add(tmp_date, tmp_dict);
                    }

                    tmp_date = tmp.DataDict["EXAMIN_DATE"].ToString();
                    tmp_dict = new Dictionary<string, KensaData>();
                    tmp_dict.Add(obj.KensaCode, obj);
                }

                // 同一日にすでに検査結果が入っていれば、すでに入っているデータ
                // （受付番号が後）を優先する（＝何もしない）
                if (!tmp_dict.ContainsKey(obj.KensaCode))
                {
                    tmp_dict.Add(obj.KensaCode, obj);
                }
            }

            if (tmp_date.Length > 0)
            {
                dict.Add(tmp_date, tmp_dict);
            }
#else
            string cmd = "select t.患者コード, t.採取日, t.採取時間, t.項目番号 " +
                ", m.項目名, t.検体コメント, t.検査結果, t.院内区分, t.基準値, t.変更フラグ, m.表示順 " +
                " from macs.ＮＴ検査結果データ t, macs.AMB_検査結果表示順マスター m " +
                " where t.患者コード = " + pt_id +
                " and t.項目番号 = to_char(m.項目番号) " +
                " order by t.採取日 desc, m.表示順, t.受付番号 desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            string tmp_date = "";
            Dictionary<string, KensaData> tmp_dict = new Dictionary<string, KensaData>();

            foreach (StdClass tmp in tmp_list)
            {
                KensaData obj = GetFromStdClass(tmp);

                // 採取日ごとにリストを作る
                if (!tmp_date.Equals(tmp.DataDict["採取日"].ToString()))
                {
                    if (tmp_date.Length > 0)
                    {
                        dict.Add(tmp_date, tmp_dict);
                    }

                    tmp_date = tmp.DataDict["採取日"].ToString();
                    tmp_dict = new Dictionary<string, KensaData>();
                    tmp_dict.Add(obj.KensaCode, obj);
                }

                // 同一日にすでに検査結果が入っていれば、すでに入っているデータ
                // （受付番号が後）を優先する（＝何もしない）
                if (!tmp_dict.ContainsKey(obj.KensaCode))
                {
                    tmp_dict.Add(obj.KensaCode, obj);
                }
            }

            if (tmp_date.Length > 0)
            {
                dict.Add(tmp_date, tmp_dict);
            }
#endif
            return dict;
        }


        /// <summary>
        /// 感染症検査の結果のみ
        /// </summary>
        /// <param name="pt_id"></param>
        /// <returns></returns>
        public static Dictionary<string, Dictionary<string, KensaData>> GetInfectionDict(string pt_id)
        {
            Dictionary<string, Dictionary<string, KensaData>> dict = new Dictionary<string, Dictionary<string, KensaData>>();

            if (pt_id.Length == 0)
            {
                return dict;
            }
#if INNO
            string cmd = "select t.P_ID, t.EXAMIN_DATE, t.EXAMIN_TIME, t.EXAMIN_NO, t.ITEM_NO " +
                ", m.NAME, t.RESULT_UNIT, t.RESULT, t.INNAI_TYPE, t.STANDARD, t.CHANGE_FLG, m.DISP_SEQ " +
                " from D_KENSA_RESULT t, M_KENSA_DISPLAY m " +
                " where t.P_ID = " + pt_id +
                " and t.ITEM_NO = m.CODE " +
                " and t.ITEM_NO in (select tt.CODE from M_INFECTION tt) " +
                " order by t.EXAMIN_NO desc, m.DISP_SEQ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            string tmp_uke = "";
            Dictionary<string, KensaData> tmp_dict = new Dictionary<string, KensaData>();

            foreach (StdClass tmp in tmp_list)
            {
                KensaData obj = GetFromStdClass(tmp);

                // 受付番号ごとにリストを作る
                if (!tmp_uke.Equals(tmp.DataDict["EXAMIN_NO"].ToString()))
                {
                    if (tmp_uke.Length > 0)
                    {
                        dict.Add(tmp_uke, tmp_dict);
                    }

                    tmp_uke = tmp.DataDict["EXAMIN_NO"].ToString();
                    tmp_dict = new Dictionary<string, KensaData>();
                    tmp_dict.Add(obj.KensaCode, obj);
                }

                // 同一受付番号にすでに検査結果が入っていれば、すでに入っているデータ
                // （受付番号が後）を優先する（＝何もしない）
                if (!tmp_dict.ContainsKey(obj.KensaCode))
                {
                    tmp_dict.Add(obj.KensaCode, obj);
                }
            }

            if (tmp_uke.Length > 0)
            {
                dict.Add(tmp_uke, tmp_dict);
            }
#else
            string cmd = "select t.患者コード, t.採取日, t.採取時間, t.受付番号, t.項目番号 " +
                ", m.項目名, t.検体コメント, t.検査結果, t.院内区分, t.基準値, t.変更フラグ, m.表示順 " +
                " from macs.ＮＴ検査結果データ t, macs.AMB_検査結果表示順マスター m " +
                " where t.患者コード = " + pt_id +
                " and t.項目番号 = to_char(m.項目番号) " +
                " and to_number(t.項目番号) in (select tt.感染症コード from macs.AMB_感染症項目マスター tt) " +
                " order by t.受付番号 desc, m.表示順";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            string tmp_uke = "";
            Dictionary<string, KensaData> tmp_dict = new Dictionary<string, KensaData>();

            foreach (StdClass tmp in tmp_list)
            {
                KensaData obj = GetFromStdClass(tmp);

                // 受付番号ごとにリストを作る
                if (!tmp_uke.Equals(tmp.DataDict["受付番号"].ToString()))
                {
                    if (tmp_uke.Length > 0)
                    {
                        dict.Add(tmp_uke, tmp_dict);
                    }

                    tmp_uke = tmp.DataDict["受付番号"].ToString();
                    tmp_dict = new Dictionary<string, KensaData>();
                    tmp_dict.Add(obj.KensaCode, obj);
                }

                // 同一受付番号にすでに検査結果が入っていれば、すでに入っているデータ
                // （受付番号が後）を優先する（＝何もしない）
                if (!tmp_dict.ContainsKey(obj.KensaCode))
                {
                    tmp_dict.Add(obj.KensaCode, obj);
                }
            }

            if (tmp_uke.Length > 0)
            {
                dict.Add(tmp_uke, tmp_dict);
            }
#endif
            return dict;
        }

        /// <summary>
        /// 検査データを項目名と期間指定（１年間以内）で検索する
        /// </summary>
        /// <param name="kensa_name_list">項目名のリスト</param>
        /// <param name="date1">開始日</param>
        /// <param name="date2">終了日</param>
        /// <returns></returns>
        public static List<KensaData> GetListByKensaNamesDates(List<string> kensa_name_list, int date1, int date2)
        {
            List<KensaData> list = new List<KensaData>();

            DateTime dt1 = DateTimeAgent.DateTimeFromInt(date1);
            DateTime dt2 = DateTimeAgent.DateTimeFromInt(date2);

            // 終了日が開始日より１年以上後ならば１年後に変更する
            if (dt1.AddYears(1) < dt2)
            {
                dt2 = dt1.AddYears(1);
            }

            List<string> ss = new List<string>();

            foreach (string s in kensa_name_list)
            {
                if (s.Length == 0)
                {
                    continue;
                }
#if INNO
                if (Regex.IsMatch(s, @"^\d+$"))
                {
                    // 数字のみの場合
                    ss.Add("CODE = '" + s + "'");
                }
                else
                {
                    // NVARCHAR2 の場合は、半角→全角変換がうまくいかないため to_char しなければならない
                    ss.Add("to_multi_byte(upper(to_char(NAME))) like '%" + AppString.HanToZen(s.ToUpper()) + "%'");
                }
#else
                ss.Add("to_multi_byte(upper(項目名)) like '%" + AppString.HanToZen(s.ToUpper()) + "%'");
#endif
            }

            if (ss.Count == 0)
            {
                return list;
            }

#if INNO
            string cmd = "select t.P_ID, t.EXAMIN_DATE, t.EXAMIN_TIME, t.EXAMIN_NO, t.ITEM_NO " +
                ", m.NAME, t.RESULT_UNIT, t.RESULT, t.INNAI_TYPE, t.STANDARD, t.CHANGE_FLG, m.DISP_SEQ " +
                " from D_KENSA_RESULT t, M_KENSA_DISPLAY m " +
                " where t.EXAMIN_DATE >= " + dt1.ToString("yyyyMMdd") + " and t.EXAMIN_DATE <= " + dt2.ToString("yyyyMMdd") +
                " and t.ITEM_NO in " +
                " (select mm.CODE from M_KENSA_DISPLAY mm " +
                "  where (" + AppString.ConcatList(ss, " or ") + ")) " +
                " and t.ITEM_NO = m.CODE " +
                " order by t.P_ID, t.EXAMIN_DATE desc, t.EXAMIN_NO desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
#else
            string cmd = "select t.患者コード, t.採取日, t.採取時間, t.受付番号, t.項目番号 " +
                ", m.項目名, t.検体コメント, t.検査結果, t.院内区分, t.基準値, t.変更フラグ, m.表示順 " +
                " from ＮＴ検査結果データ t, AMB_検査結果表示順マスター m " +
                " where t.受付日付 >= " + dt1.ToString("yyyyMMdd") + " and t.受付日付 <= " + dt2.ToString("yyyyMMdd") +
                " and t.項目番号 in " +
                " (select mm.項目番号 from AMB_検査結果表示順マスター mm " +
                "  where (" + AppString.ConcatList(ss, " or ") + ")) " +
                " and t.項目番号 = m.項目番号 " +
                " order by t.患者コード, t.採取日, t.受付番号";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif

            foreach (StdClass tmp in tmp_list)
            {
                KensaData obj = KensaData.GetFromStdClass(tmp);
                list.Add(obj);
            }

            return list;
        }

        static KensaData GetFromStdClass(StdClass tmp)
        {
            KensaData obj = new KensaData();
#if INNO
            obj.PtId = tmp.DataDict["P_ID"].ToString();
            int.TryParse(tmp.DataDict["EXAMIN_DATE"].ToString(), out obj.KensaDate);
            obj.KensaCode = tmp.DataDict["ITEM_NO"].ToString();
            obj.KensaName = AppString.ZenToHan(tmp.DataDict["NAME"].ToString()).TrimEnd(' ', '　');
            obj.Unit = tmp.DataDict["RESULT_UNIT"].ToString();
            int.TryParse(tmp.DataDict["INNAI_TYPE"].ToString(), out obj.Inside);
            obj.Result = tmp.DataDict["RESULT"].ToString();
            obj.Normal = tmp.DataDict["STANDARD"].ToString();
            int.TryParse(tmp.DataDict["CHANGE_FLG"].ToString(), out obj.ModFlg);
            int.TryParse(tmp.DataDict["DISP_SEQ"].ToString(), out obj.ShowSEQ);
#else
            obj.PtId = tmp.DataDict["患者コード"].ToString();
            int.TryParse(tmp.DataDict["採取日"].ToString(), out obj.KensaDate);
            obj.KensaCode = tmp.DataDict["項目番号"].ToString();
            obj.KensaName = AppString.ZenToHan(tmp.DataDict["項目名"].ToString()).TrimEnd(' ', '　');
            obj.Unit = tmp.DataDict["検体コメント"].ToString();
            int.TryParse(tmp.DataDict["院内区分"].ToString(), out obj.Inside);
            obj.Result = tmp.DataDict["検査結果"].ToString();
            obj.Normal = tmp.DataDict["基準値"].ToString();
            int.TryParse(tmp.DataDict["変更フラグ"].ToString(), out obj.ModFlg);
            int.TryParse(tmp.DataDict["表示順"].ToString(), out obj.ShowSEQ);
#endif
            return obj;
        }
    }
}
