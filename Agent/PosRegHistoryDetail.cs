using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Utility;
using MedicalLibrary.Entity;

namespace MedicalLibrary.Agent
{
    public class PosRegHistoryDetail
    {
        /// <summary>
        /// 入金日時
        /// </summary>
        public string ADateTime = "";

        public string ADate
        {
            get
            {
                string s = "";

                if (this.ADateTime.Length >= 8 &&
                    DateTimeAgent.IsDate(this.ADateTime.Substring(0, 8)))
                {
                    s = this.ADateTime.Substring(0, 8);
                }

                return s;
            }
        }

        public string ATime
        {
            get
            {
                string s = "";

                if (this.ADateTime.Length == 14)
                {
                    s = this.ADateTime.Substring(8, 6);
                }

                return s;
            }
        }

        public string PtId = "";

        PatBase _Pat = new PatBase();

        public PatBase Pat
        {
            get
            {
                if (!this._Pat.Id.Equals(this.PtId))
                {
                    this._Pat = PatBase.Load(this.PtId);
                }

                return this._Pat;
            }
        }

        public int PayMoney = 0;

        public string BillId = "";

        /// <summary>
        /// 支払区分
        /// </summary>
        public string PayKubun = "";

        /// <summary>
        /// 支払区分
        /// 1: 現金, 2: クレジット, 3: デビット, 4: 振込, 5: 金券, 6: 減額
        /// </summary>
        public string PayKubunString
        {
            get
            {
                string s = "";

                switch (this.PayKubun)
                {
                    case "1":
                        s = "現金";
                        break;
                    case "2":
                        s = "クレジット";
                        break;
                    case "3":
                        s = "デビット";
                        break;
                    case "4":
                        s = "振込";
                        break;
                    case "5":
                        s = "金券";
                        break;
                    case "6":
                        s = "減額";
                        break;
                    default:
                        break;
                }

                return s;
            }
        }

        /// <summary>
        /// 支払区分
        /// 1: 現金, 2: クレジット, 3: デビット, 4: 振込, 5: 金券, 6: 減額
        /// </summary>
        public string PayKubunStringShort
        {
            get
            {
                string s = "";

                switch (this.PayKubun)
                {
                    case "1":
                        s = "現金";
                        break;
                    case "2":
                        s = "クレ";
                        break;
                    case "3":
                        s = "デビ";
                        break;
                    case "4":
                        s = "振込";
                        break;
                    case "5":
                        s = "金券";
                        break;
                    case "6":
                        s = "減額";
                        break;
                    default:
                        break;
                }

                return s;
            }
        }


        public static PosRegHistoryDetail GetFromStdClass(StdClass tmp)
        {
            PosRegHistoryDetail obj = new PosRegHistoryDetail();

            obj.ADateTime = tmp.GetDataString("DATETIME");
            obj.PtId = tmp.GetDataString("PT_ID");
            obj.BillId = tmp.GetDataString("BILL_ID");
            obj.PayMoney = tmp.GetDataInt("PAY_MONEY");
            obj.PayKubun = tmp.GetDataString("PAY_KUBUN");

            return obj;
        }

        /// <summary>
        /// 指定した患者の履歴を取得する
        /// </summary>
        /// <param name="pt_id"></param>
        /// <returns></returns>
        public static List<PosRegHistoryDetail> GetListByPat(string pt_id)
        {
            List<PosRegHistoryDetail> list = new List<PosRegHistoryDetail>();

            if (pt_id.Length == 0)
            {
                return list;
            }

            string cmd = "select * from POSREG_HISTORY_DETAIL" +
                " where PT_ID = " + pt_id +
                " order by DATETIME desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }

        /// <summary>
        /// 指定した患者の、指定した日の入金リストを取得する
        /// </summary>
        /// <param name="pt_id_list"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<PosRegHistoryDetail> GetListByPatDate(List<string> pt_id_list, string date)
        {
            List<PosRegHistoryDetail> list = new List<PosRegHistoryDetail>();

            if (pt_id_list.Count == 0 || AppString.ConcatList(pt_id_list, ",").Length == 0)
            {
                return list;
            }

            if (!DateTimeAgent.IsDate(date))
            {
                return list;
            }

            foreach (string s in AppString.ConcatLists(pt_id_list, ","))
            {
                string cmd = "select * from POSREG_HISTORY_DETAIL" +
                    " where PT_ID in (" + s + ") " +
                    " and trunc(DATETIME / 1000000, 0) = " + date;

                List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    list.Add(GetFromStdClass(tmp));
                }
            }

            return list;
        }
    }
}
