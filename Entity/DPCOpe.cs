using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class DPCOpe : StdEntity
    {
        /// <summary>
        /// 世代区分
        /// </summary>
        public int GenSEQ = 0;

        /// <summary>
        /// MDCコード
        /// </summary>
        public string MDC = "";

        /// <summary>
        /// 分類コード
        /// </summary>
        public string Group = "";

        /// <summary>
        /// 値
        /// </summary>
        public int Value1 = 0;

        /// <summary>
        /// 手術フラグ
        /// </summary>
        public string OpeFlg = "";

        /// <summary>
        /// 年齢出生時体重別の値
        /// </summary>
        public int Value2 = 0;

        /// <summary>
        /// 対応コード
        /// </summary>
        public string Code = "";

        /// <summary>
        /// 手術1点数表名称
        /// </summary>
        public string OpeName1 = "";

        /// <summary>
        /// 手術1Kコード
        /// </summary>
        public string OpeCode1 = "";

        /// <summary>
        /// 手術2点数表名称
        /// </summary>
        public string OpeName2 = "";

        /// <summary>
        /// 手術2Kコード
        /// </summary>
        public string OpeCode2 = "";

        /// <summary>
        /// 手術3点数表名称
        /// </summary>
        public string OpeName3 = "";

        /// <summary>
        /// 手術3Kコード
        /// </summary>
        public string OpeCode3 = "";

        /// <summary>
        /// 手術4点数表名称
        /// </summary>
        public string OpeName4 = "";

        /// <summary>
        /// 手術4Kコード
        /// </summary>
        public string OpeCode4 = "";

        /// <summary>
        /// 手術5点数表名称
        /// </summary>
        public string OpeName5 = "";

        /// <summary>
        /// 手術5Kコード
        /// </summary>
        public string OpeCode5 = "";

        /// <summary>
        /// 変更区分
        /// </summary>
        public int Kind1 = 0;

        /// <summary>
        /// 開始日
        /// </summary>
        public int Date1 = 0;

        /// <summary>
        /// 終了日
        /// </summary>
        public int Date2 = 0;

        /// <summary>
        /// 更新日
        /// </summary>
        public int Date3 = 0;

        /// <summary>
        /// 基準日
        /// </summary>
        static int crit_date = 0;

        /// <summary>
        /// 基準日
        /// </summary>
        public static int CritDate
        {
            set
            {
                crit_date = value;
            }
            get
            {
                if (crit_date == 0)
                {
                    int.TryParse(DateTime.Now.ToString("yyyyMMdd"), out crit_date);
                }

                return crit_date;
            }
        }


        public static List<DPCOpe> GetList(string mdc, string group, string code)
        {
            List<DPCOpe> list = new List<DPCOpe>();

/*
            if (mdc.TrimStart('0').Length == 0 || mdc.Contains('x') ||
                group.Length == 0 || group.Contains('x') ||
                code.Length == 0 || code.Contains('x'))
            {
                return list;
            }
            */

            if (mdc.TrimStart('0').Length == 0 ||
                group.Length == 0 ||
                code.Length == 0)
            {
                return list;
            }

            string cmd = "select * from macs.DPC手術 t " +
                " where t.MDCコード = " + mdc.TrimStart('0') +
                " and t.分類コード = '" + group + "'" +
                " and t.変換テーブルコード = '" + code + "'" +
                " and t.世代区分 = (select max(世代区分) from macs.DPC手術) " +
                " and t.開始日 <= " + CritDate + " and (t.終了日 = 99999999 or t.終了日 >= " + CritDate + ")";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }


        public static List<DPCOpe> GetList(string mdc, string group)
        {
            List<DPCOpe> list = new List<DPCOpe>();
/*
            if (mdc.TrimStart('0').Length == 0 || mdc.Contains('x') ||
                group.Length == 0 || group.Contains('x'))
            {
                return list;
            }
            */

            if (mdc.TrimStart('0').Length == 0 ||
                group.Length == 0)
            {
                return list;
            }

            string cmd = "select * from macs.DPC手術 t " +
                " where t.MDCコード = " + mdc.TrimStart('0') +
                " and t.分類コード = '" + group + "'" +
                " and t.世代区分 = (select max(世代区分) from macs.DPC手術) " +
                " and t.開始日 <= " + CritDate + " and (t.終了日 = 99999999 or t.終了日 >= " + CritDate + ")";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }

        static DPCOpe GetFromStdClass(StdClass tmp)
        {
            DPCOpe obj = new DPCOpe();

            int.TryParse(tmp.DataDict["世代区分"].ToString(), out obj.GenSEQ);
            obj.MDC = tmp.DataDict["MDCコード"].ToString().PadLeft(2, '0');
            obj.Group = tmp.DataDict["分類コード"].ToString();
            int.TryParse(tmp.DataDict["値"].ToString(), out obj.Value1);
            obj.OpeFlg = tmp.DataDict["手術フラグ"].ToString();
            int.TryParse(tmp.DataDict["年齢出生時体重別の値"].ToString(), out obj.Value2);
            obj.Code= tmp.DataDict["変換テーブルコード"].ToString();
            obj.OpeName1 = tmp.DataDict["手術1点数表名称"].ToString();
            obj.OpeCode1 = tmp.DataDict["手術1Kコード"].ToString();
            obj.OpeName2 = tmp.DataDict["手術2点数表名称"].ToString();
            obj.OpeCode2 = tmp.DataDict["手術2Kコード"].ToString();
            obj.OpeName3 = tmp.DataDict["手術3点数表名称"].ToString();
            obj.OpeCode3 = tmp.DataDict["手術3Kコード"].ToString();
            obj.OpeName4 = tmp.DataDict["手術4点数表名称"].ToString();
            obj.OpeCode4 = tmp.DataDict["手術4Kコード"].ToString();
            obj.OpeName5 = tmp.DataDict["手術5点数表名称"].ToString();
            obj.OpeCode5 = tmp.DataDict["手術5Kコード"].ToString();
            int.TryParse(tmp.DataDict["変更区分"].ToString(), out obj.Kind1);
            int.TryParse(tmp.DataDict["開始日"].ToString(), out obj.Date1);
            int.TryParse(tmp.DataDict["終了日"].ToString(), out obj.Date2);
            int.TryParse(tmp.DataDict["更新日"].ToString(), out obj.Date3);

            return obj;
        }
    }
}
