using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    /// <summary>
    /// 手術予約の診療・休診設定クラス
    /// </summary>
    public class EyeOpeRsv
    {
        public string OpeDate = "";
        public string OpeWaku = "";
        public string OpeKind = "";
        public string RsvKind = "";
        public string Comment = "";

        /// <summary>
        /// データベースに保存する。
        /// </summary>
        public void Save()
        {
            Save(OpeDate, OpeWaku, OpeKind, RsvKind, Comment);
        }

        /// <summary>
        /// データベースに保存する。
        /// </summary>
        /// <param name="ope_date"></param>
        /// <param name="ope_waku"></param>
        /// <param name="ope_kind"></param>
        /// <param name="rsv_kind"></param>
        /// <param name="comment"></param>
        public static void Save(string ope_date, string ope_waku, string ope_kind, string rsv_kind, string comment)
        {
            if (ope_date.Length != 8)
            {
                throw new Exception("日付が正しくありません");
            }

            if (ope_waku.Length < 3 || ope_waku.Length > 9)
            {
                throw new Exception("枠が正しくありません");
            }

            if (ope_kind.Length < 1 || ope_kind.Length > 2)
            {
                throw new Exception("種別が正しくありません");
            }

            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
            obj.Db = DB.Db2;

            obj.Table = "EYE_OPE_RSV";

            obj.DataList.Add(new StdDbColumn("RSV_KIND", StdDbType.NUMBER, rsv_kind));
            obj.DataList.Add(new StdDbColumn("COMT", StdDbType.VARCHAR2, comment));
            obj.WhereList.Add("OPE_DATE = " + ope_date);
            obj.WhereList.Add("OPE_WAKU = '" + ope_waku + "'");
            obj.WhereList.Add("OPE_KIND = " + ope_kind);

            sr = obj.UpdateSQL();

            // update 対象が無ければ新規登録
            if (sr.IntValue == 0)
            {
                obj.DataList.Add(new StdDbColumn("OPE_DATE", StdDbType.NUMBER, ope_date));
                obj.DataList.Add(new StdDbColumn("OPE_WAKU", StdDbType.VARCHAR2, ope_waku));
                obj.DataList.Add(new StdDbColumn("OPE_KIND", StdDbType.NUMBER, ope_kind));

                sr = obj.InsertSQL();
            }
        }

        /// <summary>
        /// 指定した期間・種別の診療・休診情報リストを取得する。
        /// </summary>
        /// <param name="start_date"></param>
        /// <param name="end_date"></param>
        /// <param name="ope_kind"></param>
        /// <returns></returns>
        public static List<EyeOpeRsv> Find(string start_date, string end_date, string ope_kind)
        {
            if (start_date.Length != 8 || end_date.Length != 8)
            {
                throw new Exception("日付が正しくありません");
            }

            if (ope_kind.Length < 1 || ope_kind.Length > 2)
            {
                throw new Exception("種別が正しくありません");
            }

            List<EyeOpeRsv> tmpList = new List<EyeOpeRsv>();

            string cmd = "select OPE_DATE, OPE_WAKU, RSV_KIND, COMT from EYE_OPE_RSV " +
                " where OPE_DATE >= " + start_date + " and OPE_DATE <= " + end_date + " and OPE_KIND = " + ope_kind +
                " order by OPE_DATE, OPE_WAKU";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                EyeOpeRsv tmpRsv = new EyeOpeRsv();

                tmpRsv.OpeDate = tmp.DataDict["OPE_DATE"].ToString();
                tmpRsv.OpeWaku = tmp.DataDict["OPE_WAKU"].ToString();
                tmpRsv.OpeKind = ope_kind;
                tmpRsv.RsvKind = tmp.DataDict["RSV_KIND"].ToString();
                tmpRsv.Comment = tmp.DataDict["COMT"].ToString();

                tmpList.Add(tmpRsv);
            }

            return tmpList;
        }

        /// <summary>
        /// 指定した日・種別・枠の診療・休診情報を削除する。
        /// </summary>
        /// <param name="ope_date"></param>
        /// <param name="ope_waku"></param>
        /// <param name="ope_kind"></param>
        public static void Delete(string ope_date, string ope_waku, string ope_kind)
        {
            if (ope_date.Length != 8)
            {
                throw new Exception("日付が正しくありません");
            }

            if (ope_waku.Length < 3 || ope_waku.Length > 9)
            {
                throw new Exception("枠が正しくありません");
            }

            if (ope_kind.Length < 1 || ope_kind.Length > 2)
            {
                throw new Exception("種別が正しくありません");
            }

            string cmd = "delete from EYE_OPE_RSV " +
                " where OPE_DATE = " + ope_date + " and OPE_WAKU = '" + ope_waku + "' and OPE_KIND = " + ope_kind;

            DB.Db2.ExecuteNonQuery(cmd);
        }
    }
}
