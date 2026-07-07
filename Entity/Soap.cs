using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class SoapDate : StdEntity
    {
        public string PtId = "";

        public int Date = 0;

        public int InOut = 1;

        public string Dept = "";

        public string DeptName
        {
            get
            {
                string s = "";

                if (Dict.DeptDict.ContainsKey(Dept))
                {
                    s = Dict.DeptDict[Dept].FullName;
                }

                return s;
            }
        }
/*
        /// <summary>
        /// 日付ごとに辞書を作る
        /// </summary>
        /// <param name="pt_id">患者コード</param>
        /// <param name="crit_date">基準日</param>
        /// <returns></returns>
        public static List<string> GetDateList(string pt_id, string crit_date)
        {
            List<string> list = new List<string>();

            if (pt_id.Length == 0 || crit_date.Length != 8)
            {
                return list;
            }

            string cmd = "select t1.更新日 登録日 from macs.ADT_ＳＯＡＰデータヘッダ t1 " +
                " where t1.患者コード = " + pt_id +
                " and t1.更新日 <= " + crit_date +
                " and t1.削除フラグ = 0 " +
                "  union " +
                " select t2.ＰＤＦ登録日 登録日 from macs.ＰＤＦ登録データ t2 " +
                " where t2.患者コード = " + pt_id +
                " and t2.ＰＤＦ登録日 <= " + crit_date +
                " and t2.書類コード in (select tm.書類コード from macs.ＰＤＦ書類マスター tm where tm.表示対象フラグ = 1) " +
                " and t2.削除区分 = 0 " +
                "  union " +
                " select t3.施行予定日 登録日 from macs.ＮＴオーダーヘッダー t3 " +
                " where t3.患者コード = " + pt_id +
                " and t3.施行予定日 <= " + crit_date +
                " order by 登録日 desc";


            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(tmp.DataDict["登録日"].ToString());
            }

            return list;
        }
*/
        /// <summary>
        /// 日付ごとに辞書を作る
        /// </summary>
        /// <param name="pt_id">患者コード</param>
        /// <param name="crit_date">基準日</param>
        /// <returns></returns>
        public static Dictionary<string, List<SoapDate>> GetDict(string pt_id, string crit_date)
        {
            Dictionary<string, List<SoapDate>> dict = new Dictionary<string, List<SoapDate>>();

            if (pt_id.Length == 0 || crit_date.Length != 8)
            {
                return dict;
            }
#if INNO
            string cmd = "select distinct t.SOAP_DATE, t.INOUT, t.DEPT " +
                " from medb.D_SOAP_HEADER t " +
                " where t.P_ID = " + pt_id +
                " and t.SOAP_DATE <= " + crit_date +
                " and (t.DEL_FLG is null or t.DEL_FLG = 0) " +
                " and (t.RESERVE_FLG is null or t.RESERVE_FLG = 0) " +
                " order by t.SOAP_DATE desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                SoapDate obj = new SoapDate();

                obj.PtId = pt_id;
                obj.Date = tmp.GetDataInt("SOAP_DATE");
                obj.InOut = tmp.GetDataInt("INOUT");
                obj.Dept = tmp.GetDataString("DEPT");

                if (dict.ContainsKey(obj.Date.ToString()))
                {
                    dict[obj.Date.ToString()].Add(obj);
                }
                else
                {
                    List<SoapDate> soap_list = new List<SoapDate>();
                    soap_list.Add(obj);
                    dict.Add(obj.Date.ToString(), soap_list);
                }
            }
#else
            string cmd = "select t1.ＳＯＡＰ対象日 登録日, t1.入外区分 入外, t1.登録科 科コード " +
                " from macs.ADT_ＳＯＡＰデータヘッダ t1 " +
                " where t1.患者コード = " + pt_id +
                " and t1.ＳＯＡＰ対象日 <= " + crit_date +
                " and t1.削除フラグ = 0 " +
                "  union " +
                " select t2.ＰＤＦ登録日 登録日, 0 入外, t2.科コード from macs.ＰＤＦ登録データ t2 " +
                " where t2.患者コード = " + pt_id +
                " and t2.ＰＤＦ登録日 <= " + crit_date +
                " and t2.書類コード in (select tm.書類コード from macs.ＰＤＦ書類マスター tm where tm.表示対象フラグ = 1) " +
                " and t2.削除区分 = 0 " +
                "  union " +
                " select t3.施行予定日 登録日, t3.入外区分 入外, t3.科コード from macs.ＮＴオーダーヘッダー t3 " +
                " where t3.患者コード = " + pt_id +
                " and t3.施行予定日 <= " + crit_date +
                " and t3.施行予定日 < 99999999 " +
                " order by 登録日 desc";


            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                SoapDate obj = new SoapDate();

                obj.PtId = pt_id;
                int.TryParse(tmp.DataDict["登録日"].ToString(), out obj.Date);
                int.TryParse(tmp.DataDict["入外"].ToString(), out obj.InOut);
                obj.Dept = tmp.DataDict["科コード"].ToString();

                if (dict.ContainsKey(obj.Date.ToString()))
                {
                    dict[obj.Date.ToString()].Add(obj);
                }
                else
                {
                    List<SoapDate> soap_list = new List<SoapDate>();
                    soap_list.Add(obj);
                    dict.Add(obj.Date.ToString(), soap_list);
                }
            }
#endif
            return dict;
        }
    }

    public class SoapKey : StdEntity
    {
        public string PtId = "";

        public int InOut = 1;

        public int RegDate = 0;

        public int RegTime = 0;

        public string RegStaff = "";

        public string RegStaffName
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(RegStaff))
                {
                    s = Dict.StaffDict[RegStaff].Name;
                }

                return s;
            }
        }

        public int SEQ = 0;

        string _Key = "";

        public string Key
        {
            set
            {
                this._Key = value;
            }
            get
            {
                if (this._Key.Length > 0)
                {
                    return this._Key;
                }
                else
                {
                    return PtId + "-" + InOut.ToString() + "-" + RegDate.ToString() + "-" + RegTime.ToString() + "-" + RegStaff + "-" + SEQ.ToString();
                }
            }
        }
    }
}
