using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class DPCDiag2 : StdEntity
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
        /// 副傷病フラグ
        /// </summary>
        public string DiagFlg2 = "";

        /// <summary>
        /// 副傷病名
        /// </summary>
        public string DiagName2 = "";

        /// <summary>
        /// ICDコード
        /// </summary>
        public string ICD = "";

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


        public static List<DPCDiag2> GetList(string mdc, string group, bool ope)
        {
            List<DPCDiag2> list = new List<DPCDiag2>();
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

            string code_sql = "";

            if (ope)
            {
                // 手術あり
                code_sql = " and t.副傷病フラグ in (1, 3) ";
            }
            else
            {
                // 手術なし
                code_sql = " and t.副傷病フラグ in (1, 2) ";
            }

            string cmd = "select * from macs.DPC副傷病名 t " +
                " where t.MDCコード = " + mdc.TrimStart('0') +
                " and t.分類コード = '" + group + "'" +
                code_sql +
                " and t.世代区分 = (select max(世代区分) from macs.DPC処置等1) " +
                " and t.開始日 <= " + CritDate + " and (t.終了日 = 99999999 or t.終了日 >= " + CritDate + ")";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }


        public static List<DPCDiag2> GetList(string mdc, string group)
        {
            List<DPCDiag2> list = new List<DPCDiag2>();
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

            string cmd = "select * from macs.DPC副傷病名 t " +
                " where t.MDCコード = " + mdc.TrimStart('0') +
                " and t.分類コード = '" + group + "'" +
                " and t.世代区分 = (select max(世代区分) from macs.DPC処置等1) " +
                " and t.開始日 <= " + CritDate + " and (t.終了日 = 99999999 or t.終了日 >= " + CritDate + ")";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }

        static DPCDiag2 GetFromStdClass(StdClass tmp)
        {
            DPCDiag2 obj = new DPCDiag2();

            int.TryParse(tmp.DataDict["世代区分"].ToString(), out obj.GenSEQ);
            obj.MDC = tmp.DataDict["MDCコード"].ToString().PadLeft(2, '0');
            obj.Group = tmp.DataDict["分類コード"].ToString();
            obj.Code = tmp.DataDict["対応コード"].ToString();
            obj.DiagFlg2 = tmp.DataDict["副傷病フラグ"].ToString();
            obj.DiagName2 = tmp.DataDict["副傷病名"].ToString();
            obj.ICD = tmp.DataDict["ICDコード"].ToString();
            int.TryParse(tmp.DataDict["変更区分"].ToString(), out obj.Kind1);
            int.TryParse(tmp.DataDict["開始日"].ToString(), out obj.Date1);
            int.TryParse(tmp.DataDict["終了日"].ToString(), out obj.Date2);
            int.TryParse(tmp.DataDict["更新日"].ToString(), out obj.Date3);

            return obj;
        }
    }
}
