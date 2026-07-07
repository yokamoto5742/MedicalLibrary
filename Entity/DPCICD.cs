using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class DPCICD : StdEntity
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
        /// ICDコード
        /// </summary>
        public string ICDCode = "";

        public string ICDCodeWildCard
        {
            get
            {
                string s = "";

                if (this.ICDCode.Length >= 4)
                {
                    s = this.ICDCode.Substring(0, this.ICDCode.Length - 1) + "$";
                }

                return s;
            }
        }

        public string ICDCodeWildCard2
        {
            get
            {
                string s = "";

                if (this.ICDCode.Length >= 5)
                {
                    s = this.ICDCode.Substring(0, this.ICDCode.Length - 2) + "$";
                }

                return s;
            }
        }

        /// <summary>
        /// ICD名称
        /// </summary>
        public string ICDName = "";

        /// <summary>
        /// 病名
        /// </summary>
        public string DiagName = "";

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


        public static List<DPCICD> GetListByDiagNameOrICD(string diag_name, string icd)
        {
            List<DPCICD> list = new List<DPCICD>();

            if (diag_name.Length == 0 && icd.Length == 0)
            {
                return list;
            }

            // まずは病名を取得
            List<DiagMaster> diag_list = DiagMaster.LoadByDiagNameOrICD(diag_name, icd);

            // ICD10リストを作成する
            List<string> icd_list = new List<string>();

            foreach (DiagMaster diag in diag_list)
            {
                if (diag.ICD10_1.Trim().Length == 0)
                {
                    continue;
                }

                DPCICD obj = new DPCICD();

                obj.DiagName = diag.Name;
                obj.ICDCode = diag.ICD10_1;

                if (!icd_list.Contains("'" + diag.ICD10_1 + "'"))
                {
                    icd_list.Add("'" + diag.ICD10_1 + "'");

                    if (diag.ICD10_1.Length >= 4)
                    {
                        icd_list.Add("'" + diag.ICD10_1.Substring(0, diag.ICD10_1.Length - 1) + "$'");
                    }

                    if (diag.ICD10_1.Length >= 5)
                    {
                        icd_list.Add("'" + diag.ICD10_1.Substring(0, diag.ICD10_1.Length - 2) + "$'");
                    }
                }

                list.Add(obj);
            }

            if (icd_list.Count == 0)
            {
                return list;
            }

            string cmd = "select * from macs.DPCICD t " +
                " where t.ICDコード in (" + AppString.ConcatList(icd_list, ",") + ") " +
                " and t.世代区分 = (select max(世代区分) from macs.DPCICD) " +
                " and t.開始日 <= " + CritDate + " and (t.終了日 = 99999999 or t.終了日 >= " + CritDate + ")";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            List<DPCICD> list2 = new List<DPCICD>();

            foreach (StdClass tmp in tmp_list)
            {
                list2.Add(GetFromStdClass(tmp));
            }

            foreach (DPCICD obj in list)
            {
                foreach (DPCICD obj2 in list2)
                {
                    if (obj.ICDCode.Equals(obj2.ICDCode) || obj.ICDCodeWildCard.Equals(obj2.ICDCode) || obj.ICDCodeWildCard2.Equals(obj2.ICDCode))
                    {
                        obj.GenSEQ = obj2.GenSEQ;
                        obj.MDC = obj2.MDC;
                        obj.Group = obj2.Group;
                        obj.ICDName = obj2.ICDName;
                        obj.Kind1 = obj2.Kind1;
                        obj.Date1 = obj2.Date1;
                        obj.Date2 = obj2.Date2;
                        obj.Date3 = obj2.Date3;

                        break;
                    }
                }
            }

            return list;
        }

/*
        public static List<DPCICD> GetListByICD(string icd)
        {
            List<DPCICD> list = new List<DPCICD>();

            if (icd.Length == 0)
            {
                return list;
            }

            // ICD10リストを作成する
            List<string> icd_list = new List<string>();

            icd_list.Add("'" + icd + "'");

            if (icd.Length >= 2)
            {
                icd_list.Add("'" + icd.Substring(0, icd.Length - 1) + "$'");
            }

            if (icd_list.Count == 0)
            {
                return list;
            }

            string cmd = "select * from macs.DPCICD t " +
                " where t.ICDコード in (" + AppString.ConcatList(icd_list, ",") + ") " +
                " and t.世代区分 = (select max(世代区分) from macs.DPCICD) " +
                " and t.開始日 <= " + CritDate + " and (t.終了日 = 99999999 or t.終了日 >= " + CritDate + ")";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            List<DPCICD> list2 = new List<DPCICD>();

            foreach (StdClass tmp in tmp_list)
            {
                list2.Add(GetFromStdClass(tmp));
            }

            foreach (DPCICD obj in list)
            {
                foreach (DPCICD obj2 in list2)
                {
                    if (obj.ICDCode.Equals(obj2.ICDCode) || obj.ICDCodeWildCard.Equals(obj2.ICDCode))
                    {
                        obj.GenSEQ = obj2.GenSEQ;
                        obj.MDC = obj2.MDC;
                        obj.Group = obj2.Group;
                        obj.ICDName = obj2.ICDName;
                        obj.Kind1 = obj2.Kind1;
                        obj.Date1 = obj2.Date1;
                        obj.Date2 = obj2.Date2;
                        obj.Date3 = obj2.Date3;

                        break;
                    }
                }
            }

            return list;
        }


        public static List<DPCICD> GetListByDiagNameOrICD(string diag_name, string icd)
        {
            List<DPCICD> list = new List<DPCICD>();

            // ICD10リストを作成する
            List<string> icd_list = new List<string>();

            if (diag_name.Length > 0)
            {
                // まずは病名を取得
                List<DiagMaster> diag_list = DiagMaster.FindList(diag_name);

                foreach (DiagMaster diag in diag_list)
                {
                    if (diag.ICD10.Length == 0)
                    {
                        continue;
                    }

                    DPCICD obj = new DPCICD();

                    obj.DiagName = diag.Name;
                    obj.ICDCode = diag.ICD10;

                    if (!icd_list.Contains("'" + diag.ICD10 + "'"))
                    {
                        icd_list.Add("'" + diag.ICD10 + "'");

                        if (diag.ICD10.Length >= 2)
                        {
                            icd_list.Add("'" + diag.ICD10.Substring(0, diag.ICD10.Length - 1) + "$'");
                        }
                    }

                    list.Add(obj);
                }
            }


            if (icd_list.Count == 0)
            {
                return list;
            }

            string cmd = "select * from macs.DPCICD t " +
                " where t.ICDコード in (" + AppString.ConcatList(icd_list, ",") + ") " +
                " and t.世代区分 = (select max(世代区分) from macs.DPCICD) " +
                " and t.開始日 <= " + CritDate + " and (t.終了日 = 99999999 or t.終了日 >= " + CritDate + ")";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            List<DPCICD> list2 = new List<DPCICD>();

            foreach (StdClass tmp in tmp_list)
            {
                list2.Add(GetFromStdClass(tmp));
            }

            foreach (DPCICD obj in list)
            {
                foreach (DPCICD obj2 in list2)
                {
                    if (obj.ICDCode.Equals(obj2.ICDCode) || obj.ICDCodeWildCard.Equals(obj2.ICDCode))
                    {
                        obj.GenSEQ = obj2.GenSEQ;
                        obj.MDC = obj2.MDC;
                        obj.Group = obj2.Group;
                        obj.ICDName = obj2.ICDName;
                        obj.Kind1 = obj2.Kind1;
                        obj.Date1 = obj2.Date1;
                        obj.Date2 = obj2.Date2;
                        obj.Date3 = obj2.Date3;

                        break;
                    }
                }
            }

            return list;
        }
*/

        static DPCICD GetFromStdClass(StdClass tmp)
        {
            DPCICD obj = new DPCICD();

            int.TryParse(tmp.DataDict["世代区分"].ToString(), out obj.GenSEQ);
            obj.MDC = tmp.DataDict["MDCコード"].ToString().PadLeft(2, '0');
            obj.Group = tmp.DataDict["分類コード"].ToString();
            obj.ICDCode = tmp.DataDict["ICDコード"].ToString();
            obj.ICDName = tmp.DataDict["ICD名称"].ToString();
            int.TryParse(tmp.DataDict["変更区分"].ToString(), out obj.Kind1);
            int.TryParse(tmp.DataDict["開始日"].ToString(), out obj.Date1);
            int.TryParse(tmp.DataDict["終了日"].ToString(), out obj.Date2);
            int.TryParse(tmp.DataDict["更新日"].ToString(), out obj.Date3);

            return obj;
        }
    }
}
