using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class PatOrderDetail : StdEntity
    {
        /// <summary>
        /// オーダー番号
        /// </summary>
        public string OrderId = "";

        /// <summary>
        /// 連番
        /// </summary>
        public string DetailId = "";

        PatBase _Pat = new PatBase();

        public PatBase Pat
        {
            get
            {
                return this._Pat;
            }
        }

        /// <summary>
        /// 施行予定日
        /// </summary>
        public string SekouDate = "";

        /// <summary>
        /// 診療行為
        /// </summary>
        public string KouiCode = "";

        public string KouiName
        {
            get
            {
                string s = "";

                if (Dict.KouiDict.ContainsKey(this.KouiCode))
                {
                    s = Dict.KouiDict[this.KouiCode];
                }

                return s;
            }
        }

        /// <summary>
        /// オーダーコード
        /// </summary>
        public string Code = "";

        /// <summary>
        /// 名称
        /// </summary>
        public string Name = "";

        /// <summary>
        /// 略称（マスターと結合して取得）
        /// </summary>
        public string ShortName = "";

        /// <summary>
        /// 数量
        /// 1 以下の小数の場合、たとえば 0.5 の場合は「.5」と登録される。
        /// </summary>
        public float Qty = 0;

        /// <summary>
        /// 表示数量
        /// 1 以下の小数の場合、たとえば 0.5 の場合は「0.5」と登録される。
        /// </summary>
        public string QtyString = "";

        /// <summary>
        /// 数量編集（マスターから）
        /// 1 以上の場合は特殊処理（「表示数量」を追記する）
        /// </summary>
        public int QtyFormat = 0;

        /// <summary>
        /// データ区分
        /// 1:固定, 2:薬剤, 3:器材, 4:コメント, 5:セット, 6:フィルム, 7:伝票名称
        /// </summary>
        public int DataType = 0;

        /// <summary>
        /// 単位
        /// </summary>
        public string Unit = "";

        /// <summary>
        /// 日/回数
        /// </summary>
        public float Times = 0;

        /// <summary>
        /// 予約種別コード
        /// </summary>
        string _RsvCode = "";

        /// <summary>
        /// 予約種別コード
        /// </summary>
        public string RsvCode
        {
            get
            {
                string s = "";

                if (this._RsvCode.Length > 0 &&
                    !this._RsvCode.Equals("0") &&
                    !this._RsvCode.Equals("-1"))
                {
                    s = this._RsvCode;
                }

                return s;
            }
        }


        /// <summary>
        /// 医事コード（外来）
        /// </summary>
        public string _ReceCode1 = "";

        public string ReceCode1
        {
            set
            {
                this._ReceCode1 = value;
            }
            get
            {
                string s = "";

                if (this._ReceCode1.Length > 0)
                {
                    s = this._ReceCode1;
                }
                else if (this.Code.StartsWith("88888889"))
                {
                    // 自費コメント
                    s = "CH00003095";
                }

                return s;
            }
        }

        /// <summary>
        /// 医事コード（入院）
        /// </summary>
        public string _ReceCode2 = "";

        public string ReceCode2
        {
            set
            {
                this._ReceCode2 = value;
            }
            get
            {
                string s = "";

                if (this._ReceCode2.Length > 0)
                {
                    s = this._ReceCode2;
                }
                else if (this.Code.StartsWith("88888889"))
                {
                    // 自費コメント
                    s = "CH00003095";
                }

                return s;
            }
        }

        /// <summary>
        /// 医事診療区分（外来）
        /// </summary>
        public string ReceShinku1 = "";

        /// <summary>
        /// 医事診療区分（入院）
        /// </summary>
        public string ReceShinku2 = "";


        public string GetReceCode(string in_out)
        {
            if (in_out.Equals("2"))
            {
                return this.ReceCode2;
            }
            else
            {
                return this.ReceCode1;
            }
        }

        public string GetReceShinku(string in_out)
        {
            if (in_out.Equals("2"))
            {
                return this.ReceShinku2;
            }
            else
            {
                return this.ReceShinku1;
            }
        }


        /// <summary>
        /// 一般名フラグ
        /// </summary>
        public string IppanNameFlg = "";


        public PatOrderDetail()
        {
        }

        public PatOrderDetail(StdClass tmp)
        {
#if INNO
            this.OrderId = tmp.GetDataString("ORDER_NO");
            this.DetailId = tmp.GetDataString("DETAIL_SEQ");
            this.Pat.Id = tmp.GetDataString("P_ID");
            this.SekouDate = tmp.GetDataString("ORDER_DATE");
            this.KouiCode = tmp.GetDataString("SDCD");
            this.Code = tmp.GetDataString("ORDER_CODE");
            this.Name = tmp.GetDataString("ORDER_COMMENT").Trim();
            this.ShortName = tmp.GetDataString("S_NAME").Trim();
            this.Qty = tmp.GetDataFloat("QTY");
            this.QtyString = tmp.GetDataString("QTY_DISP");
            this.QtyFormat = tmp.GetDataInt("QTY_FORMAT");
            this.DataType = tmp.GetDataInt("DATA_TYPE");
            this.Unit = tmp.GetDataString("UNIT").Trim();
            this.Times = tmp.GetDataFloat("TIMES");
            this._RsvCode = tmp.GetDataString("YOYAKU_TYPE");
            this.ReceCode1 = tmp.GetDataString("IJI_CODE_G");
            this.ReceCode2 = tmp.GetDataString("IJI_CODE_N");
            this.ReceShinku1 = tmp.GetDataString("IJI_SHINKU_G");
            this.ReceShinku2 = tmp.GetDataString("IJI_SHINKU_N");
            this.IppanNameFlg = tmp.GetDataString("IPPAN_FLG");
#else
            this.OrderId = tmp.GetDataString("オーダー番号").Trim();
            this.DetailId = tmp.GetDataString("明細連番").Trim();
            this.Pat.Id = tmp.GetDataString("患者コード").Trim();
            this.SekouDate = tmp.GetDataString("施行予定日").Trim();
            this.KouiCode = tmp.GetDataString("ＳＤＣＤ");
            this.Code = tmp.GetDataString("オーダーコード").Trim();
            this.Name = tmp.GetDataString("コメント").Trim();
            this.Qty = tmp.GetDataFloat("数量");
            this.QtyString = tmp.GetDataString("表示数量").Trim();
            this.QtyFormat = tmp.GetDataInt("数量編集");
            this.DataType = tmp.GetDataInt("データ区分");
            this.Unit = tmp.GetDataString("単位").Trim();
            this.Times = tmp.GetDataFloat("回数");
            this._RsvCode = tmp.GetDataString("予備フラグ７");

			// 2019/03/22
			// MACSPROASCONV2 からの取得
			// 動作検証しにくいため保留とする
//			this.ReceCode1 = tmp.GetDataString("IJI_CODE_G").Length > 0 ? tmp.GetDataString("IJI_CODE_G") : this.Code;
//			this.ReceCode2 = tmp.GetDataString("IJI_CODE_N").Length > 0 ? tmp.GetDataString("IJI_CODE_N") : this.Code;
#endif
        }

        /// <summary>
        /// オーダーディティールを返す。（オーダー番号が１つの場合）
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns></returns>
        public static List<PatOrderDetail> Load(string order_id)
        {
            List<PatOrderDetail> tmpList = new List<PatOrderDetail>();

            if (order_id.Length == 0)
            {
                return tmpList;
            }
#if INNO
            string cmd = "select td.*, tm.S_NAME, tm.UNIT, tm.QTY_FORMAT, tm.DATA_TYPE, tm.YOYAKU_TYPE " +
                " , tm.IJI_CODE_G, tm.IJI_CODE_N, tm.IJI_SHINKU_G, tm.IJI_SHINKU_N, tm.IPPAN_FLG " +
                " from D_ORDER_DETAIL td, M_ORDER tm " +
                " where td.ORDER_NO = " + order_id +
                " and tm.CODE(+) = td.ORDER_CODE " +
				" and tm.SEDAI(+) = td.SEDAI " +
//				" and tm.DATE_S <= td.ORDER_DATE and tm.DATE_E >= td.ORDER_DATE" +
                " order by td.DETAIL_SEQ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
#else
            string cmd = "select td.*, tm.単位, tm.数量編集, tm.データ区分, tm.予備フラグ７ " +
//				" , (select t2.プロアスコード from macs.MACSPROASCONV2 t2 where t2.オーダーコード = td.オーダーコード and t2.入外区分 = 1 and t2.世代区分 = 2) IJI_CODE_G " +
//				" , (select t2.プロアスコード from macs.MACSPROASCONV2 t2 where t2.オーダーコード = td.オーダーコード and t2.入外区分 = 2 and t2.世代区分 = 2) IJI_CODE_N " +
                " from ＮＴオーダーディティール td, ＮＴオーダーマスター tm " +
                " where td.オーダー番号 = " + order_id +
                " and tm.オーダーコード(+) = td.オーダーコード " +
                " order by td.明細連番";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif
            foreach (StdClass tmp in tmp_list)
            {
                tmpList.Add(new PatOrderDetail(tmp));
            }

            return tmpList;
        }

        /// <summary>
        /// オーダーディティールのリストを返す。（オーダー番号が複数の場合）
        /// </summary>
        /// <param name="order_id_list"></param>
        /// <returns></returns>
        public static List<PatOrderDetail> Load(List<string> order_id_list)
        {
            List<PatOrderDetail> tmpList = new List<PatOrderDetail>();

            if (order_id_list.Count == 0)
            {
                return tmpList;
            }

            foreach (string s in AppString.ConcatLists(order_id_list, ","))
            {
#if INNO
                string cmd = "select td.*, tm.S_NAME, tm.UNIT, tm.QTY_FORMAT, tm.DATA_TYPE, tm.YOYAKU_TYPE " +
                    " , tm.IJI_CODE_G, tm.IJI_CODE_N, tm.IJI_SHINKU_G, tm.IJI_SHINKU_N, tm.IPPAN_FLG " +
                    " from D_ORDER_DETAIL td, M_ORDER tm " +
                    " where td.ORDER_NO in (" + s + ")" +
                    " and tm.CODE(+) = td.ORDER_CODE " +
					" and tm.SEDAI(+) = td.SEDAI " +
//					" and tm.DATE_S <= td.ORDER_DATE and tm.DATE_E >= td.ORDER_DATE" +
                    " order by td.ORDER_DATE desc, td.ORDER_NO, td.DETAIL_SEQ";

                List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
#else
                string cmd = "select td.*, tm.単位, tm.数量編集, tm.データ区分, tm.予備フラグ７ " +
//					" , (select t2.プロアスコード from macs.MACSPROASCONV2 t2 where t2.オーダーコード = td.オーダーコード and t2.入外区分 = 1 and t2.世代区分 = 2) IJI_CODE_G " +
//					" , (select t2.プロアスコード from macs.MACSPROASCONV2 t2 where t2.オーダーコード = td.オーダーコード and t2.入外区分 = 2 and t2.世代区分 = 2) IJI_CODE_N " +
					" from ＮＴオーダーディティール td, ＮＴオーダーマスター tm " +
                    " where td.オーダー番号 in (" + s + ")" +
                    " and tm.オーダーコード(+) = td.オーダーコード " +
                    " order by td.施行予定日 desc, td.オーダー番号, td.明細連番";

                List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif
                foreach (StdClass tmp in tmp_list)
                {
                    tmpList.Add(new PatOrderDetail(tmp));
                }
            }

            return tmpList;
        }

        /// <summary>
        /// 対象期間中の該当オーダーディティールのリストを出す。
        /// </summary>
        /// <param name="master_code_list"></param>
        /// <param name="start_date"></param>
        /// <param name="end_date"></param>
        /// <returns></returns>
        public static List<PatOrderDetail> FindByMasterCode(List<string> master_code_list, string start_date, string end_date)
        {
            List<PatOrderDetail> tmpList = new List<PatOrderDetail>();

            if (master_code_list.Count == 0)
            {
                return tmpList;
            }

            string s_date = "";

            if (start_date.Length == 8)
            {
                s_date = start_date;
            }
            else
            {
                s_date = DateTime.Now.ToString("yyyyMM") + "00";
            }

            string e_date = "";

            if (end_date.Length == 8)
            {
                e_date = end_date;
            }
            else
            {
                e_date = DateTime.Now.ToString("yyyyMM") + "99";
            }

            string master_codes = "";

            foreach (string s in master_code_list)
            {
                if (master_codes.Length > 0)
                {
                    master_codes += ",";
                }

                master_codes += "'" + s + "'";
            }
#if INNO
            string cmd = "select td.*, tm.S_NAME, tm.UNIT, tm.QTY_FORMAT, tm.DATA_TYPE, tm.YOYAKU_TYPE " +
                " , tm.IJI_CODE_G, tm.IJI_CODE_N, tm.IJI_SHINKU_G, tm.IJI_SHINKU_N, tm.IPPAN_FLG " +
                " from D_ORDER_DETAIL td, M_ORDER tm " +
                " where td.ORDER_DATE >= " + s_date +
                " and td.ORDER_DATE <= " + e_date +
                " and td.ORDER_CODE in (" + master_codes + ")" +
                " and tm.CODE(+) = td.ORDER_CODE " +
				" and tm.SEDAI(+) = td.SEDAI " +
//				" and tm.DATE_S <= td.ORDER_DATE and tm.DATE_E >= td.ORDER_DATE" +
                " order by td.ORDER_DATE desc, td.ORDER_NO, td.DETAIL_SEQ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                tmpList.Add(new PatOrderDetail(tmp));
            }
#else
            string cmd = "select td.*, tm.単位, tm.数量編集, tm.データ区分, tm.予備フラグ７ " +
//				" , (select t2.プロアスコード from macs.MACSPROASCONV2 t2 where t2.オーダーコード = td.オーダーコード and t2.入外区分 = 1 and t2.世代区分 = 2) IJI_CODE_G " +
//				" , (select t2.プロアスコード from macs.MACSPROASCONV2 t2 where t2.オーダーコード = td.オーダーコード and t2.入外区分 = 2 and t2.世代区分 = 2) IJI_CODE_N " +
				" from ＮＴオーダーディティール td, ＮＴオーダーマスター tm " +
                " where td.施行予定日 >= " + s_date +
                " and td.施行予定日 <= " + e_date +
                " and td.オーダーコード in (" + master_codes + ")" +
                " and tm.オーダーコード(+) = td.オーダーコード " +
                " order by td.施行予定日 desc, td.オーダー番号, td.明細連番";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                tmpList.Add(new PatOrderDetail(tmp));
            }
#endif
            return tmpList;
        }

        #region MACS

        static PatOrderDetail GetFromStdClassMacs(StdClass tmp)
        {
            PatOrderDetail obj = new PatOrderDetail();

            obj.OrderId = tmp.GetDataString("オーダー番号").Trim();
            obj.DetailId = tmp.GetDataString("明細連番").Trim();
            obj.Pat.Id = tmp.GetDataString("患者コード").Trim();
            obj.SekouDate = tmp.GetDataString("施行予定日").Trim();
            obj.KouiCode = tmp.GetDataString("ＳＤＣＤ");
            obj.Code = tmp.GetDataString("オーダーコード").Trim();
            obj.Name = tmp.GetDataString("コメント").Trim();
            obj.Qty = tmp.GetDataFloat("数量");
            obj.QtyString = tmp.GetDataString("表示数量").Trim();
            obj.QtyFormat = tmp.GetDataInt("数量編集");
            obj.DataType = tmp.GetDataInt("データ区分");
            obj.Unit = tmp.GetDataString("単位").Trim();
            obj.Times = tmp.GetDataFloat("回数");
            obj._RsvCode = tmp.GetDataString("予備フラグ７");

            return obj;
        }

        /// <summary>
        /// オーダーディティールのリストを返す。（オーダー番号が複数の場合）
        /// </summary>
        /// <param name="order_id_list"></param>
        /// <returns></returns>
        public static List<PatOrderDetail> LoadMacs(List<string> order_id_list)
        {
            List<PatOrderDetail> tmpList = new List<PatOrderDetail>();

            if (order_id_list.Count == 0)
            {
                return tmpList;
            }

            foreach (string s in AppString.ConcatLists(order_id_list, ","))
            {
                string cmd = "select td.*, tm.単位, tm.数量編集, tm.データ区分, tm.予備フラグ７ " +
                    //					" , (select t2.プロアスコード from macs.MACSPROASCONV2 t2 where t2.オーダーコード = td.オーダーコード and t2.入外区分 = 1 and t2.世代区分 = 2) IJI_CODE_G " +
                    //					" , (select t2.プロアスコード from macs.MACSPROASCONV2 t2 where t2.オーダーコード = td.オーダーコード and t2.入外区分 = 2 and t2.世代区分 = 2) IJI_CODE_N " +
                    " from macs.ＮＴオーダーディティール td, macs.ＮＴオーダーマスター tm " +
                    " where td.オーダー番号 in (" + s + ")" +
                    " and tm.オーダーコード(+) = td.オーダーコード " +
                    " order by td.施行予定日 desc, td.オーダー番号, td.明細連番";

                List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    tmpList.Add(GetFromStdClassMacs(tmp));
                }
            }

            return tmpList;
        }

        #endregion
    }
}
