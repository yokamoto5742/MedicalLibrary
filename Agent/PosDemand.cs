using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Utility;
using MedicalLibrary.Entity;

namespace MedicalLibrary.Agent
{
    public class PosDemand
    {
        public string BillId = "";

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

        /// <summary>
        /// 更新区分
        /// </summary>
        public string Kind = "";

        /// <summary>
        /// 請求日
        /// </summary>
        public string DemDate = "";

		/// <summary>
		/// 請求期間開始日
		/// </summary>
		public string DemStartDate = "";

		/// <summary>
		/// 請求期間終了日
		/// </summary>
		public string DemEndDate = "";

		/// <summary>
		/// 請求期間
		/// </summary>
		public string DemTerm
		{
			get
			{
				string s = DateTimeAgent.DateFormat(this.DemStartDate, DateTimeAgent.DateFormatKind.SHORT);

				if (DateTimeAgent.IsDate(this.DemEndDate) && !this.DemStartDate.Equals(this.DemEndDate))
				{
					s += "-" + DateTimeAgent.DateFormat(this.DemEndDate, DateTimeAgent.DateFormatKind.SHORT);
				}

				return s;
			}
		}

        /// <summary>
        /// 請求時刻
        /// </summary>
        public string DemTime = "";

        /// <summary>
        /// 最終支払日
        /// </summary>
        public string LastPayDate = "";

        /// <summary>
        /// 保険パターン番号
        /// </summary>
        public int Ins = 0;

        /// <summary>
        /// 科
        /// </summary>
        public string Dept = "";

        public string DeptName
        {
            get
            {
                string s = "";

                if (Dict.DeptDict.ContainsKey(this.Dept))
                {
                    s = Dict.DeptDict[this.Dept].ShortName;
                }

                return s;
            }
        }

        /// <summary>
        /// 医師
        /// </summary>
        public string Doctor = "";

        public string DoctorName
        {
            get
            {
                string s = "";

                if (Dict.DoctorDict.ContainsKey(this.Doctor))
                {
                    s = Dict.DoctorDict[this.Doctor].Name;
                }

                return s;
            }
        }

        /// <summary>
        /// 0: 未納, 1: 完納, 2: 一部入金, 9: 書損
        /// </summary>
        public int OkFlg = 0;

        public string OkFlgString
        {
            get
            {
                string s = "";

                switch (this.OkFlg)
                {
                    case 0:
                        s = "未納";
                        break;
                    case 1:
                        s = "完納";
                        break;
                    case 2:
                        s = "一部";
                        break;
                    case 9:
                        s = "書損";
                        break;
                    default:
                        break;
                }

                return s;
            }
        }

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

        /// <summary>
        /// 請求額
        /// </summary>
        public int BillMoney = 0;

        /// <summary>
        /// 一部額
        /// </summary>
        public int PartMoney = 0;

        /// <summary>
        /// 未収額
        /// </summary>
        public int YetMoney
        {
            get
            {
                int i = 0;

                if (this.OkFlg.Equals(0) || this.OkFlg.Equals(2))
                {
                    i = this.BillMoney - this.PartMoney;
                }

                return i;
            }
        }

        /// <summary>
        /// 備考
        /// </summary>
        public string Bikou = "";

        public static PosDemand GetFromStdClass(StdClass tmp)
        {
            PosDemand obj = new PosDemand();

            obj.BillId = tmp.GetDataString("BILL_ID");
            obj.PtId = tmp.GetDataString("PT_ID");
            obj.Kind = tmp.GetDataString("KOUSHIN_KUBUN");
            obj.DemDate = tmp.GetDataString("DEM_DATE");
			obj.DemStartDate = tmp.GetDataString("DEM_START_DATE");
			obj.DemEndDate = tmp.GetDataString("DEM_END_DATE");
			obj.DemTime = tmp.GetDataString("DEM_TIME");
            obj.LastPayDate = tmp.GetDataString("LAST_PAY_DATE");
            obj.Ins = tmp.GetDataInt("HOKEN_SEQ");
            obj.Dept = tmp.GetDataString("DEPT");
            obj.Doctor = tmp.GetDataString("DR_CODE");
            obj.OkFlg = tmp.GetDataInt("OK_FLG");
            obj.PayKubun = tmp.GetDataString("PAY_KUBUN");
            obj.BillMoney = tmp.GetDataInt("BILL_MONEY");
            obj.PartMoney = tmp.GetDataInt("BILL_MONEY_PART");

            return obj;
        }

        /// <summary>
        /// 指定した患者の履歴を調べる
        /// </summary>
        /// <param name="pt_id"></param>
        /// <returns></returns>
        public static List<PosDemand> GetListByPat(string pt_id)
        {
            List<PosDemand> list = new List<PosDemand>();

            if (pt_id.Length == 0)
            {
                return list;
            }

            string cmd = "select * from POS_DEMAND" +
                " where PT_ID = " + pt_id +
                " order by DEM_DATE desc, BILL_ID desc, KOUSHIN_KUBUN desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }

        /// <summary>
        /// 指定した患者の請求データを調べる
        /// </summary>
        /// <param name="pt_id_list"></param>
        /// <returns></returns>
        public static List<PosDemand> GetListByPat(List<string> pt_id_list)
        {
            List<PosDemand> list = new List<PosDemand>();

            if (pt_id_list.Count == 0 || AppString.ConcatList(pt_id_list, ",").Length == 0)
            {
                return list;
            }

            foreach (string s in AppString.ConcatLists(pt_id_list, ","))
            {
                string cmd = "select * from POS_DEMAND" +
                    " where PT_ID in (" + s + ")" +
                    " order by PT_ID, DEM_DATE desc, BILL_ID desc, KOUSHIN_KUBUN desc";

                List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    list.Add(GetFromStdClass(tmp));
                }
            }

            return list;
        }

        /// <summary>
        /// 指定した患者の未収金を調べる
        /// </summary>
        /// <param name="pt_id_list"></param>
        /// <returns></returns>
        public static List<PosDemand> GetYetListByPat(List<string> pt_id_list)
        {
            List<PosDemand> list = new List<PosDemand>();

            if (pt_id_list.Count == 0 || AppString.ConcatList(pt_id_list, ",").Length == 0)
            {
                return list;
            }

            foreach (string s in AppString.ConcatLists(pt_id_list, ","))
            {
                string cmd = "select * from POS_DEMAND" +
                    " where PT_ID in (" + s + ")" +
                    " and OK_FLG in (0, 2) " +
                    " and BILL_MONEY > 0";

                List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    list.Add(GetFromStdClass(tmp));
                }
            }

            return list;
        }

        /// <summary>
        /// BillPay 向け請求データを調べる
        /// 対象患者の指定日の請求データ + 前日以前の未収データ
        /// </summary>
        /// <param name="pt_id_list">患者リスト</param>
        /// <param name="date">指定日</param>
        /// <returns></returns>
        public static List<PosDemand> GetListForBillPay(List<string> pt_id_list, string date)
        {
            List<PosDemand> list = new List<PosDemand>();

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
                string cmd = "select * from POS_DEMAND" +
                    " where PT_ID in (" + s + ")" +
                    " and (DEM_DATE = " + date + " or (DEM_DATE < " + date + " and OK_FLG in (0, 2) and BILL_MONEY > 0)) " +
                    " order by PT_ID, DEM_DATE desc, BILL_ID desc, KOUSHIN_KUBUN desc";

                List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    list.Add(GetFromStdClass(tmp));
                }
            }

            return list;
        }

        /// <summary>
        /// 指定した請求書IDの請求データを調べる
        /// </summary>
        /// <param name="bill_id_list">患者リスト</param>
        /// <returns></returns>
        public static List<PosDemand> GetListByBillId(List<string> bill_id_list)
        {
            List<PosDemand> list = new List<PosDemand>();

            if (bill_id_list.Count == 0 || AppString.ConcatList(bill_id_list, ",").Length == 0)
            {
                return list;
            }

            foreach (string s in AppString.ConcatLists(bill_id_list, ",", "'"))
            {
                string cmd = "select * from POS_DEMAND" +
                    " where BILL_ID in (" + s + ")" +
                    " order by PT_ID, DEM_DATE desc, BILL_ID desc, KOUSHIN_KUBUN desc";

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
