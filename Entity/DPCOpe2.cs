using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class DPCOpe2 : StdEntity
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
        /// 対応コード
        /// </summary>
        public string Code = "";

        /// <summary>
        /// 処置2フラグ
        /// </summary>
        public string OpeFlg2 = "";

        /// <summary>
        /// 処置等1名称
        /// </summary>
        public string OpeName1 = "";

        /// <summary>
        /// 処置等1コード
        /// </summary>
        public string OpeCode1 = "";

        /// <summary>
        /// 処置等2名称
        /// </summary>
        public string OpeName2 = "";

        /// <summary>
        /// 処置等2コード
        /// </summary>
        public string OpeCode2 = "";

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


        public static List<DPCOpe2> GetList(string mdc, string group, string code)
        {
            List<DPCOpe2> list = new List<DPCOpe2>();
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
                code.Length == 0 || code.Contains('x'))
            {
                return list;
            }

            string cmd = "select * from macs.DPC処置等2 t " +
                " where t.MDCコード = " + mdc.TrimStart('0') +
                " and t.分類コード = '" + group + "'" +
                " and t.対応コード = '" + code + "'" +
                " and t.世代区分 = (select max(世代区分) from macs.DPC処置等1) " +
                " and t.開始日 <= " + CritDate + " and (t.終了日 = 99999999 or t.終了日 >= " + CritDate + ")";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }

        static DPCOpe2 GetFromStdClass(StdClass tmp)
        {
            DPCOpe2 obj = new DPCOpe2();

            int.TryParse(tmp.DataDict["世代区分"].ToString(), out obj.GenSEQ);
            obj.MDC = tmp.DataDict["MDCコード"].ToString().PadLeft(2, '0');
            obj.Group = tmp.DataDict["分類コード"].ToString();
            obj.Code = tmp.DataDict["対応コード"].ToString();
            obj.OpeFlg2 = tmp.DataDict["処置2フラグ"].ToString();
            obj.OpeName1 = tmp.DataDict["処置等1名称"].ToString();
            obj.OpeCode1 = tmp.DataDict["処置等1コード"].ToString();
            obj.OpeName2 = tmp.DataDict["処置等2名称"].ToString();
            obj.OpeCode2 = tmp.DataDict["処置等2コード"].ToString();
            int.TryParse(tmp.DataDict["変更区分"].ToString(), out obj.Kind1);
            int.TryParse(tmp.DataDict["開始日"].ToString(), out obj.Date1);
            int.TryParse(tmp.DataDict["終了日"].ToString(), out obj.Date2);
            int.TryParse(tmp.DataDict["更新日"].ToString(), out obj.Date3);

            return obj;
        }
    }
}
