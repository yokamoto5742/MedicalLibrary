using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
//using Oracle.DataAccess.Client;
using MedicalLibrary.Boundary;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class OrderHeader : StdEntity
    {
        public string SekouDate = "";

        public string SekouDateString
        {
            get
            {
                return DateTimeAgent.DateFormat(SekouDate, DateTimeAgent.DateFormatKind.LONG);
            }
            set
            {
                DateTime dt = DateTime.Now;

                if (DateTime.TryParse(value, out dt))
                {
                    this.SekouDate = dt.ToString("yyyyMMdd");
                }
            }
        }

        public string SekouDateStringShort
        {
            get
            {
                return DateTimeAgent.DateFormat(SekouDate, DateTimeAgent.DateFormatKind.SHORT);
            }
            set
            {
                DateTime dt = DateTime.Now;

                if (DateTime.TryParse("20" + value, out dt))
                {
                    this.SekouDate = dt.ToString("yyyyMMdd");
                }
            }
        }

        public string PtId = "";

        public string InOut = "";

        string kouiCode = "";

        public string KouiCode
        {
            set
            {
                this.kouiCode = value;
            }
            get
            {
                string s = "";

                foreach (OrderDetail detail in this.DetailList)
                {
                    if (detail.SDCD.Length > 0)
                    {
                        s = detail.SDCD;
                        break;
                    }
                }

                return s;
            }
        }

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

        public string DeptCode = "";

        public string DeptName
        {
            get
            {
                string s = "";

                if (Dict.DeptDict.ContainsKey(this.DeptCode))
                {
                    s = Dict.DeptDict[this.DeptCode].ShortName;
                }

                return s;
            }
        }

        public string DoctorCode = "";

        public string DoctorName
        {
            get
            {
                string s = "";

                if (Dict.DoctorDict.ContainsKey(this.DoctorCode))
                {
                    s = Dict.DoctorDict[this.DoctorCode].Name;
                }

                return s;
            }
        }

        public string Ins = "";

        string startDate = "";

        public string StartDate
        {
            set
            {
                this.startDate = value;
            }
            get
            {
                string s = this.startDate;

                if (this.startDate.Length != 8)
                {
                    s = DateTime.Now.ToString("yyyyMMdd");
                }

                return s;
            }
        }

        public string StartDateString
        {
            get
            {
                return DateTimeAgent.DateFormat(StartDate, DateTimeAgent.DateFormatKind.LONG);
            }
        }

        public string StartDateStringShort
        {
            get
            {
                return DateTimeAgent.DateFormat(StartDate, DateTimeAgent.DateFormatKind.SHORT);
            }
        }

        string endDate = "";

        public string EndDate
        {
            set
            {
                endDate = value;
            }
            get
            {
                string s = "";

                if (this.endDate.Length == 8)
                {
                    s = this.endDate;
                }
                else if (!this.KouiCode.Equals("21"))
                {
                    s = this.StartDate;
                }
                else if (this.DetailList.Count > 0)
                {
                    s = this.StartDate;

                    foreach (OrderDetail detail in this.DetailList)
                    {
                        if (detail.Times >= 1)
                        {
                            s = DateTimeAgent.AddDays(s, (int)detail.Times - 1).ToString();
                        }
                    }
                }

                return s;
            }
        }

        public string EndDateString
        {
            get
            {
                return DateTimeAgent.DateFormat(EndDate, DateTimeAgent.DateFormatKind.LONG);
            }
        }

        public string EndDateStringShort
        {
            get
            {
                return DateTimeAgent.DateFormat(EndDate, DateTimeAgent.DateFormatKind.SHORT);
            }
        }

        /// <summary>
        /// 受付番号
        /// </summary>
        public long UkeSEQ = 0;

        /// <summary>
        /// 連番
        /// </summary>
        public int SEQ = 0;

        /// <summary>
        /// オーダー番号
        /// </summary>
        public long OrderSEQ = 0;

        /// <summary>
        /// 施行部署１
        /// </summary>
        public string SekouCode1 = "";

        /// <summary>
        /// 施行部署２
        /// </summary>
        public string SekouCode2 = "";

        /// <summary>
        /// 予約番号
        /// </summary>
        public long RsvSEQ = 0;

        /// <summary>
        /// 予約日
        /// </summary>
        public int RsvDate = 0;

        /// <summary>
        /// 予約時間
        /// </summary>
        public int RsvTime1 = 0;



        /// <summary>
        /// 日/回数
        /// </summary>
        float times = 0.0F;

        public float Times
        {
            set
            {
                this.times = value;
            }
            get
            {
                if (this.times > 0)
                {
                    return this.times;
                }
                else
                {
                    float f = 1.0F;

                    foreach (OrderDetail detail in this.DetailList)
                    {
                        if (detail.Times > 0)
                        {
                            f = detail.Times;
                            break;
                        }
                    }

                    return f;
                }
            }
        }

        public string SOAP = "";

        /// <summary>
        /// DetailList から生成したＳＯＡＰ表示名称
        /// </summary>
        public string DetailString
        {
            get
            {
                string s = "";

                foreach (OrderDetail detail in this.DetailList)
                {
                    if (s.Length > 0)
                    {
                        s += ", ";
                    }

                    s += detail.OrderName;

                    if (detail.Unit.Length > 0)
                    {
                        s += " " + detail.Qty + detail.Unit;
                    }
                }

                if (this.Times > 0)
                {
                    s += " ×" + this.Times;
                }

                return s;
            }
        }

        /// <summary>
        /// 施行フラグ
        /// </summary>
        public bool SekouFlg = false;

        /// <summary>
        /// 会計フラグ
        /// </summary>
        public bool KaikeiFlg = false;

        /// <summary>
        /// 指示箋フラグ
        /// </summary>
        public bool PaperFlg = false;

        /// <summary>
        /// ラベルフラグ
        /// </summary>
        public bool LabelFlg = false;

        /// <summary>
        /// 調剤済みフラグ
        /// 0: 未調剤, 9: 調剤済み
        /// </summary>
        public bool DrugFlg = false;

        /// <summary>
        /// 院内区分
        /// 0: 院内処方, 1: 院外処方
        /// </summary>
        public bool IngaiFlg = false;

        /// <summary>
        /// 〆後フラグ（オーダー区分）
        /// 0: 〆前オーダー, 1: 〆後オーダー
        /// </summary>
        public bool AfterFlg = false;

        /// <summary>
        /// 臨時フラグ
        /// 伝票種別 = 1
        /// </summary>
        public bool RinjiFlg = false;

        /// <summary>
        /// 退院時処方
        /// 伝票種別 = 2
        /// </summary>
        public bool TaiinFlg = false;


        /// <summary>
        /// 指示日
        /// </summary>
        public int OrderDate = 0;

        /// <summary>
        /// 指示時間
        /// </summary>
        public int OrderTime = 0;

        /// <summary>
        /// 指示日時
        /// </summary>
        public string OrderDateTime
        {
            get
            {
                string s = DateTimeAgent.DateFormat(this.OrderDate, DateTimeAgent.DateFormatKind.LONG) +
                    " " + DateTimeAgent.TimeFormat(this.OrderTime.ToString().PadLeft(6, '0').Substring(0, 4));

                return s;
            }
        }

        /// <summary>
        /// 指示日時
        /// yy/MM/dd HH:mm
        /// </summary>
        public string OrderDateTimeShort
        {
            get
            {
                string s = DateTimeAgent.DateFormat(this.OrderDate, DateTimeAgent.DateFormatKind.SHORT) +
                    " " + DateTimeAgent.TimeFormat(this.OrderTime.ToString().PadLeft(6, '0').Substring(0, 4));

                return s;
            }
        }

        public int OrderTime4
        {
            get
            {
                return this.OrderTime / 100;
            }
        }

        /// <summary>
        /// 入力者コード
        /// </summary>
        public string OrderStaffCode = "";

        /// <summary>
        /// 入力者
        /// </summary>
        public string OrderStaffName
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(OrderStaffCode))
                {
                    s = Dict.StaffDict[OrderStaffCode].Name;
                }

                return s;
            }
        }

        /// <summary>
        /// 施行者コード
        /// </summary>
        public string SekouStaffCode = "";

        /// <summary>
        /// 施行者
        /// </summary>
        public string SekouStaffName
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(SekouStaffCode))
                {
                    s = Dict.StaffDict[SekouStaffCode].Name;
                }

                return s;
            }
        }

        /// <summary>
        /// 会計者コード
        /// </summary>
        public string KaikeiStaffCode = "";

        /// <summary>
        /// 会計者
        /// </summary>
        public string KaikeiStaffName
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(KaikeiStaffCode))
                {
                    s = Dict.StaffDict[KaikeiStaffCode].Name;
                }

                return s;
            }
        }

        /// <summary>
        /// 削除日
        /// </summary>
        public int DeleteDate = 0;

        /// <summary>
        /// 削除時間
        /// </summary>
        public int DeleteTime = 0;

        /// <summary>
        /// 削除日時
        /// </summary>
        public string DeleteDateTime
        {
            get
            {
                string s = DateTimeAgent.DateFormat(this.DeleteDate, DateTimeAgent.DateFormatKind.LONG) +
                    " " + DateTimeAgent.TimeFormat(this.DeleteTime.ToString().PadLeft(6, '0').Substring(0, 4));

                return s;
            }
        }

        /// <summary>
        /// 削除日時
        /// yy/MM/dd HH:mm
        /// </summary>
        public string DeleteDateTimeShort
        {
            get
            {
                string s = DateTimeAgent.DateFormat(this.DeleteDate, DateTimeAgent.DateFormatKind.SHORT) +
                    " " + DateTimeAgent.TimeFormat(this.DeleteTime.ToString().PadLeft(6, '0').Substring(0, 4));

                return s;
            }
        }

        /// <summary>
        /// 削除者コード
        /// </summary>
        public string DeleteStaffCode = "";

        /// <summary>
        /// 削除者
        /// </summary>
        public string DeleteStaffName
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(DeleteStaffCode))
                {
                    s = Dict.StaffDict[DeleteStaffCode].Name;
                }

                return s;
            }
        }

        public List<OrderDetail> DetailList = new List<OrderDetail>();

        public OrderHeader()
        {
        }

        public OrderHeader(PatOrder order)
        {
            this.KouiCode = order.Shinku;

            foreach (PatOrderDetail detail in order.DetailList)
            {
                this.DetailList.Add(new OrderDetail(detail));
            }
        }

        /// <summary>
        /// 施行フラグをセットする
        /// </summary>
        /// <param name="order_seq">オーダー番号 14桁</param>
        /// <param name="val">0 or 1</param>
        /// <param name="log_table">ログテーブルにもセットするか</param>
        public static void SetSekouFlg(string order_seq, int val, bool log_table = false)
        {
            if (order_seq.Length == 0)
            {
                return;
            }

#if INNO
            string cmd = "update D_ORDER_HEADER " +
                " set SEKOU_FLG = " + val +
                " where ORDER_NO = " + order_seq;

            DB.Db3.ExecuteNonQuery(cmd);

            if (log_table)
            {
                cmd = "update L_ORDER_HEADER " +
                    " set SEKOU_FLG = " + val +
                    " where ORDER_NO = " + order_seq;

                DB.Db3.ExecuteNonQuery(cmd);
            }
#else
            string cmd = "update ＮＴオーダーヘッダー " +
                " set 施行フラグ = " + val +
                " where オーダー番号 = " + order_seq;

            DB.Db1.ExecuteNonQuery(cmd);
#endif
        }

        /// <summary>
        /// 会計フラグをセットする
        /// </summary>
        /// <param name="order_seq">オーダー番号 14桁</param>
        /// <param name="val">0 or 1</param>
        /// <param name="log_table">ログテーブルにもセットするか</param>
        public static void SetKaikeiFlg(string order_seq, int val, bool log_table = false)
        {
            if (order_seq.Length == 0)
            {
                return;
            }

#if INNO
            string cmd = "update D_ORDER_HEADER " +
                " set BILL_FLG = " + val +
                " where ORDER_NO = " + order_seq;

            DB.Db3.ExecuteNonQuery(cmd);

            if (log_table)
            {
                cmd = "update L_ORDER_HEADER " +
                    " set BILL_FLG = " + val +
                    " where ORDER_NO = " + order_seq;

                DB.Db3.ExecuteNonQuery(cmd);
            }
#else
            string cmd = "update ＮＴオーダーヘッダー " +
                " set 会計フラグ = " + val +
                " where オーダー番号 = " + order_seq;

            DB.Db1.ExecuteNonQuery(cmd);
#endif
        }

        /// <summary>
        /// 指示箋フラグをセットする
        /// </summary>
        /// <param name="order_seq">オーダー番号 14桁</param>
        /// <param name="val">0 or 1</param>
        /// <param name="log_table">ログテーブルにもセットするか</param>
        public static void SetPaperFlg(string order_seq, int val, bool log_table = false)
        {
            if (order_seq.Length == 0)
            {
                return;
            }
#if INNO
            string cmd = "update D_ORDER_HEADER " +
                " set PRINT_FLG = " + val +
                " where ORDER_NO = " + order_seq;

            DB.Db3.ExecuteNonQuery(cmd);

            if (log_table)
            {
                cmd = "update L_ORDER_HEADER " +
                    " set PRINT_FLG = " + val +
                    " where ORDER_NO = " + order_seq;

                DB.Db3.ExecuteNonQuery(cmd);
            }
#else
            string cmd = "update ＮＴオーダーヘッダー " +
                " set 指示箋フラグ = " + val +
                " where オーダー番号 = " + order_seq;

            DB.Db1.ExecuteNonQuery(cmd);
#endif
        }


        static OrderHeader GetFromStdClass(StdClass tmp)
        {
            OrderHeader obj = new OrderHeader();

            obj.SekouDate = tmp.DataDict["施行予定日"].ToString();
            obj.PtId = tmp.DataDict["患者コード"].ToString();
            obj.Ins = tmp.DataDict["保険ビット"].ToString();
            obj.kouiCode = tmp.DataDict["診療区分"].ToString();
            int.TryParse(tmp.DataDict["連番"].ToString(), out obj.SEQ);
            obj.InOut = tmp.DataDict["入外区分"].ToString();
            obj.AfterFlg = tmp.DataDict["オーダー区分"].ToString().Equals("1");
            obj.DeptCode = tmp.DataDict["科コード"].ToString();
            obj.DoctorCode = tmp.DataDict["指示医コード"].ToString();
            long.TryParse(tmp.DataDict["オーダー番号"].ToString(), out obj.OrderSEQ);
            obj.SekouCode1 = tmp.DataDict["施行部署１"].ToString();
            obj.SekouCode2 = tmp.DataDict["施行部署２"].ToString();
            obj.OrderStaffCode = tmp.DataDict["入力者コード"].ToString();
            obj.SekouStaffCode = tmp.DataDict["施行者コード"].ToString();
            obj.KaikeiStaffCode = tmp.DataDict["会計者コード"].ToString();
            int.TryParse(tmp.DataDict["指示番号"].ToString(), out obj.OrderTime);
            int.TryParse(tmp.DataDict["指示日"].ToString(), out obj.OrderDate);
            long.TryParse(tmp.DataDict["予約番号"].ToString(), out obj.RsvSEQ);
            int.TryParse(tmp.DataDict["予約日"].ToString(), out obj.RsvDate);
            int.TryParse(tmp.DataDict["予約時間"].ToString(), out obj.RsvTime1);
            obj.SekouFlg = tmp.DataDict["施行フラグ"].ToString().Equals("1");
            obj.KaikeiFlg = tmp.DataDict["会計フラグ"].ToString().Equals("1");
            obj.PaperFlg = tmp.DataDict["指示箋フラグ"].ToString().Equals("1");
            obj.LabelFlg = tmp.DataDict["ラベルフラグ"].ToString().Equals("1");
            obj.DrugFlg = tmp.DataDict["調剤済みフラグ"].ToString().Equals("9");
            obj.TaiinFlg = tmp.DataDict["伝票種別"].ToString().Equals("2");
            obj.IngaiFlg = tmp.DataDict["院内区分"].ToString().Equals("1");
            obj.startDate = tmp.DataDict["開始日付"].ToString();
            obj.endDate = tmp.DataDict["終了日付"].ToString();
            long.TryParse(tmp.DataDict["受付番号"].ToString(), out obj.UkeSEQ);
            obj.SOAP = tmp.DataDict["ＳＯＡＰ表示名称"].ToString();

            return obj;
        }


        public static StdReturn Insert(string pt_id, string ins_code, string in_out, string dept_code, string doctor_code, int uke_seq, bool ingai, List<OrderHeader> list)
        {
            StdReturn sr = new StdReturn();

            // SQLを実行するかどうか true: 実行する, false: 実行せずログのみ（開発用）
            bool execute = true;

            string kubun_code = "";

            if (in_out.Equals("1"))
            {
                kubun_code = "6";
            }
            else if (in_out.Equals("2"))
            {
                kubun_code = "9";
            }
            else
            {
                return sr;
            }

            if (dept_code.Length == 0)
            {
                return sr;
            }

            int sekou_date = 0;

            int yyyyMMdd = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int yyMMdd = yyyyMMdd % 1000000;
            int HHmmss = int.Parse(DateTime.Now.ToString("HHmmss"));
            int HHmm = HHmmss / 100;

            // 施行日・入外・診療科によって、受付番号とオーダー番号が決まる。
            // 入外と診療科は共通なので、施行日ごとの受付番号とオーダー番号の辞書を作成する。

            Dictionary<int, OrderHeaderSEQ> dict = new Dictionary<int, OrderHeaderSEQ>();

            foreach (OrderHeader header in list)
            {
                if (!int.TryParse(header.SekouDate, out sekou_date))
                {
                    continue;
                }

                if (!dict.ContainsKey(sekou_date))
                {
                    OrderHeaderSEQ header_seq = new OrderHeaderSEQ(sekou_date);

                    // 受付番号 num1 とオーダー番号 num2 を取得する
                    long num1 = 0;
                    long num2 = 0;

                    string cmd = "select 区分, 連番 from ＮＴ連番ファイル t " +
                        " where t.年月日 = " + header_seq.SekouDateShort +
                        " and (t.区分 = " + kubun_code +
                        " or (t.区分 = 3 and t.科コード = " + dept_code + "))";

                    List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

                    foreach (StdClass tmp in tmp_list)
                    {
                        if (tmp.DataDict["区分"].ToString().Equals(kubun_code))
                        {
                            // 受付番号
                            long.TryParse(tmp.DataDict["連番"].ToString(), out num1);
                            num1++;
                        }
                        else if (tmp.DataDict["区分"].ToString().Equals("3"))
                        {
                            // オーダー番号
                            long.TryParse(tmp.DataDict["連番"].ToString(), out num2);
                            num2++;
                        }
                    }

                    if (num1 == 0)
                    {
                        if (in_out.Equals("1"))
                        {
                            num1 = 1;
                        }
                        else if (in_out.Equals("2"))
                        {
                            num1 = 50001;
                        }

                        header_seq.UkeFirst = true;
                    }

                    if (num2 == 0)
                    {
                        num2 = 1;
                        header_seq.OrderFirst = true;
                    }

                    header_seq.UkeSEQ = (long)header_seq.SekouDateShort * 100000 + num1;
                    header_seq.OrderSEQ = (long)header_seq.SekouDateShort * 100000000 + (long)(int.Parse(dept_code)) * 100000 + num2;

                    dict.Add(sekou_date, header_seq);
                }
            }

            using (DbTransaction tran = Db.Connection.BeginTransaction())
            {
                try
                {
                    foreach (OrderHeader header in list)
                    {
                        if (!int.TryParse(header.SekouDate, out sekou_date))
                        {
                            continue;
                        }

                        if (!dict.ContainsKey(sekou_date))
                        {
                            continue;
                        }

                        OrderHeaderSEQ header_seq = dict[sekou_date];

                        header.UkeSEQ = header_seq.UkeSEQ;
                        header.SEQ = header_seq.SEQ++;
                        header.OrderSEQ = header_seq.OrderSEQ++;

                        header.PtId = pt_id;
                        header.Ins = ins_code;
                        header.InOut = in_out;
                        header.DeptCode = dept_code;
                        header.DoctorCode = doctor_code;

                        header.OrderDate = yyyyMMdd;
                        header.OrderTime = HHmmss;
                        header.OrderStaffCode = LoginUser.Id;

                        header.StartDate = header.SekouDate;

                        sr.Msgs.Add(header.Insert(execute).Msg);
                    }

                    StdDbClass obj = new StdDbClass();

                    // 連番を更新する
                    foreach (OrderHeaderSEQ hseq in dict.Values)
                    {
                        obj = new StdDbClass();

                        obj.Table = "ＮＴ連番ファイル";

                        obj.DataList.Add(new StdDbColumn("区分", StdDbType.NUMBER, kubun_code));
                        obj.DataList.Add(new StdDbColumn("科コード", StdDbType.NUMBER, 0));
                        obj.DataList.Add(new StdDbColumn("年月日", StdDbType.NUMBER, hseq.SekouDateShort));
                        obj.DataList.Add(new StdDbColumn("連番", StdDbType.NUMBER, hseq.UkeSEQShort));

                        sr.Msgs.Add("受付番号 連番 = " + hseq.UkeSEQShort);

                        if (hseq.UkeFirst)
                        {
                            sr.Msgs.Add(obj.InsertSQL(execute).Msg);
                        }
                        else
                        {
                            obj.WhereList.Add("区分 = " + kubun_code);
                            obj.WhereList.Add("科コード = 0");
                            obj.WhereList.Add("年月日 = " + hseq.SekouDateShort);

                            sr.Msgs.Add(obj.UpdateSQL(execute).Msg);
                        }

                        obj.DataList.Clear();
                        obj.WhereList.Clear();

                        obj.DataList.Add(new StdDbColumn("区分", StdDbType.NUMBER, 3));
                        obj.DataList.Add(new StdDbColumn("科コード", StdDbType.NUMBER, dept_code));
                        obj.DataList.Add(new StdDbColumn("年月日", StdDbType.NUMBER, hseq.SekouDateShort));
                        obj.DataList.Add(new StdDbColumn("連番", StdDbType.NUMBER, hseq.OrderSEQShort - 1));

                        sr.Msgs.Add("オーダー番号 連番 = " + (hseq.OrderSEQShort - 1));

                        if (hseq.OrderFirst)
                        {
                            sr.Msgs.Add(obj.InsertSQL(execute).Msg);
                        }
                        else
                        {
                            obj.WhereList.Add("区分 = 3");
                            obj.WhereList.Add("科コード = " + dept_code);
                            obj.WhereList.Add("年月日 = " + hseq.SekouDateShort);

                            sr.Msgs.Add(obj.UpdateSQL(execute).Msg);
                        }
                    }


                    // 薬番関連 IC08RC, ID80RC, ID81RC を更新するかどうか。
                    bool ren_flg = false;

                    foreach (OrderHeader header in list)
                    {
                        // 処方以外は飛ばす
                        if (!header.KouiCode.Equals("21") &&
                            !header.KouiCode.Equals("22") &&
                            !header.KouiCode.Equals("23"))
                        {
                            continue;
                        }

                        // 施行日が今日以外のものは飛ばす
                        if (!header.SekouDate.Equals(yyyyMMdd.ToString()))
                        {
                            continue;
                        }

                        ren_flg = true;
                        break;
                    }


                    // 今日の処方がある場合のみ更新する。
                    if (ren_flg)
                    {
                        // その日の受付番号
                        int order_uke_seq = 0;

                        foreach (OrderHeaderSEQ hseq in dict.Values)
                        {
                            if (!hseq.SekouDate.Equals(yyyyMMdd))
                            {
                                continue;
                            }

                            order_uke_seq = hseq.UkeSEQShort;
                            break;
                        }

                        // 薬番の最大値を取得
                        int drug_seq = OrderDrugSEQ.GetMaxSEQ(yyyyMMdd, in_out, ingai);

                        // その日の最初の薬番かどうか
                        bool drug_first = (drug_seq == 0 || drug_seq == 5000 || drug_seq == 7000);


                        // 薬番インクリメント
                        drug_seq++;


                        // 一意制約違反を防ぐため IC08RC を先に更新する。
                        obj.Table = "IC08RC";

                        obj.DataList.Clear();
                        obj.WhereList.Clear();

                        obj.DataList.Add(new StdDbColumn("IC08RC_F01", StdDbType.NUMBER, ingai ? 5 : 6));
                        obj.DataList.Add(new StdDbColumn("IC08RC_F02", StdDbType.NUMBER, in_out));
                        obj.DataList.Add(new StdDbColumn("IC08RC_F03", StdDbType.NUMBER, 0));
                        obj.DataList.Add(new StdDbColumn("IC08RC_F04", StdDbType.NUMBER, yyyyMMdd));
                        obj.DataList.Add(new StdDbColumn("IC08RC_F05", StdDbType.NUMBER, drug_seq));
                        obj.DataList.Add(new StdDbColumn("IC08RC_F06", StdDbType.NUMBER, 0));

                        sr.Msgs.Add("IC08RC = " + drug_seq);

                        if (drug_first)
                        {
                            sr.Msgs.Add(obj.InsertSQL(execute).Msg);
                        }
                        else
                        {
                            obj.WhereList.Add("IC08RC_F01 = " + (ingai ? 5 : 6));
                            obj.WhereList.Add("IC08RC_F02 = " + in_out);
                            obj.WhereList.Add("IC08RC_F03 = 0");
                            obj.WhereList.Add("IC08RC_F04 = " + yyyyMMdd);

                            sr.Msgs.Add(obj.UpdateSQL(execute).Msg);
                        }

                        // ID80RC, ID81RC に挿入する

                        obj.Table = "ID80RC";
                        obj.DataList.Clear();
                        obj.WhereList.Clear();

                        obj.DataList.Add(new StdDbColumn("ID80RC_F01", StdDbType.NUMBER, yyyyMMdd));
                        obj.DataList.Add(new StdDbColumn("ID80RC_F02", StdDbType.NUMBER, pt_id));
                        obj.DataList.Add(new StdDbColumn("ID80RC_F03", StdDbType.NUMBER, doctor_code));
                        obj.DataList.Add(new StdDbColumn("ID80RC_F04", StdDbType.NUMBER, dept_code));
                        obj.DataList.Add(new StdDbColumn("ID80RC_F05", StdDbType.NUMBER, ingai ? 1 : 0));
                        obj.DataList.Add(new StdDbColumn("ID80RC_F06", StdDbType.NUMBER, drug_seq));
                        obj.DataList.Add(new StdDbColumn("ID80RC_F07", StdDbType.NUMBER, ins_code));
                        obj.DataList.Add(new StdDbColumn("ID80RC_F08", StdDbType.NUMBER, uke_seq));

                        sr.Msgs.Add("ID80RC : 薬番 = " + drug_seq);
                        sr.Msgs.Add(obj.InsertSQL(execute).Msg);

                        // ID81RC は院内のみ
                        if (!ingai)
                        {
                            obj.Table = "ID81RC";
                            obj.DataList.Clear();
                            obj.WhereList.Clear();

                            obj.DataList.Add(new StdDbColumn("ID81RC_F01", StdDbType.NUMBER, order_uke_seq));
                            obj.DataList.Add(new StdDbColumn("ID81RC_F02", StdDbType.NUMBER, yyyyMMdd));
                            obj.DataList.Add(new StdDbColumn("ID81RC_F03", StdDbType.NUMBER, pt_id));
                            obj.DataList.Add(new StdDbColumn("ID81RC_F04", StdDbType.NUMBER, drug_seq));

                            sr.Msgs.Add("ID81RC : 薬番 = " + drug_seq + ", オーダー受付番号 = " + order_uke_seq);
                            sr.Msgs.Add(obj.InsertSQL(execute).Msg);
                        }
                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw new Exception(ex.Message);
                }
            }

            return sr;
        }

        public StdReturn Insert(bool execute = true)
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();

            obj.Table = "ＮＴオーダーヘッダー";

            obj.DataList.Add(new StdDbColumn("施行予定日", StdDbType.NUMBER, this.SekouDate));
            obj.DataList.Add(new StdDbColumn("施行予定時間", StdDbType.NUMBER, 9999));
            obj.DataList.Add(new StdDbColumn("患者コード", StdDbType.NUMBER, this.PtId));
            obj.DataList.Add(new StdDbColumn("保険ビット", StdDbType.NUMBER, this.Ins));
            obj.DataList.Add(new StdDbColumn("診療区分", StdDbType.NUMBER, this.KouiCode));
            obj.DataList.Add(new StdDbColumn("連番", StdDbType.NUMBER, this.SEQ));
            obj.DataList.Add(new StdDbColumn("入外区分", StdDbType.NUMBER, this.InOut));
            obj.DataList.Add(new StdDbColumn("オーダー区分", StdDbType.NUMBER, this.AfterFlg ? 1 : 0));
            obj.DataList.Add(new StdDbColumn("科コード", StdDbType.NUMBER, this.DeptCode));
            obj.DataList.Add(new StdDbColumn("指示医コード", StdDbType.NUMBER, this.DoctorCode));
            obj.DataList.Add(new StdDbColumn("オーダー番号", StdDbType.NUMBER, this.OrderSEQ));
            obj.DataList.Add(new StdDbColumn("施行部署１", StdDbType.NUMBER, this.SekouCode1));
            obj.DataList.Add(new StdDbColumn("施行部署２", StdDbType.NUMBER, this.SekouCode2));
            obj.DataList.Add(new StdDbColumn("装置番号", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("入力者コード", StdDbType.NUMBER, this.OrderStaffCode));
            obj.DataList.Add(new StdDbColumn("施行者コード", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("会計者コード", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("指示番号", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("指示日", StdDbType.NUMBER, this.OrderDate));
            obj.DataList.Add(new StdDbColumn("指示時間", StdDbType.NUMBER, this.OrderTime4));
            obj.DataList.Add(new StdDbColumn("予約番号", StdDbType.NUMBER, this.RsvSEQ));
            obj.DataList.Add(new StdDbColumn("予約日", StdDbType.NUMBER, this.RsvDate));
            obj.DataList.Add(new StdDbColumn("予約時間", StdDbType.NUMBER, this.RsvTime1));
            obj.DataList.Add(new StdDbColumn("施行フラグ", StdDbType.NUMBER, this.SekouFlg ? 1 : 0));
            obj.DataList.Add(new StdDbColumn("会計フラグ", StdDbType.NUMBER, this.KaikeiFlg ? 1 : 0));
            obj.DataList.Add(new StdDbColumn("指示箋フラグ", StdDbType.NUMBER, this.PaperFlg ? 1 : 0));
            obj.DataList.Add(new StdDbColumn("ラベルフラグ", StdDbType.NUMBER, this.LabelFlg ? 1 : 0));
            obj.DataList.Add(new StdDbColumn("施行票フラグ", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("調剤済みフラグ", StdDbType.NUMBER, this.DrugFlg ? 9 : 0));
            obj.DataList.Add(new StdDbColumn("伝票種別", StdDbType.NUMBER, this.TaiinFlg ? 2 : 0));
            obj.DataList.Add(new StdDbColumn("まとめフラグ", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("院内区分", StdDbType.NUMBER, this.IngaiFlg ? 1 : 0));
            obj.DataList.Add(new StdDbColumn("開始日付", StdDbType.NUMBER, this.StartDate));
            obj.DataList.Add(new StdDbColumn("終了日付", StdDbType.NUMBER, this.EndDate));
            obj.DataList.Add(new StdDbColumn("受付番号", StdDbType.NUMBER, this.UkeSEQ));
            //            obj.DataList.Add(new StdDbColumn("受付連番", StdDbType.NUMBER, null));
            obj.DataList.Add(new StdDbColumn("所属コード", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("外来区分", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("初診フラグ", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("ＳＯＡＰ表示名称", StdDbType.VARCHAR2, this.DetailString));

            sr.Msgs.Add(obj.InsertSQL(execute).Msg);


            obj.Table = "ＬＧオーダーヘッダー";

            obj.DataList.Clear();

            obj.DataList.Add(new StdDbColumn("施行予定日", StdDbType.NUMBER, this.SekouDate));
            obj.DataList.Add(new StdDbColumn("施行予定時間", StdDbType.NUMBER, 9999));
            obj.DataList.Add(new StdDbColumn("患者コード", StdDbType.NUMBER, this.PtId));
            obj.DataList.Add(new StdDbColumn("保険ビット", StdDbType.NUMBER, this.Ins));
            obj.DataList.Add(new StdDbColumn("診療区分", StdDbType.NUMBER, this.KouiCode));
            obj.DataList.Add(new StdDbColumn("連番", StdDbType.NUMBER, this.SEQ));
            obj.DataList.Add(new StdDbColumn("入外区分", StdDbType.NUMBER, this.InOut));
            obj.DataList.Add(new StdDbColumn("オーダー区分", StdDbType.NUMBER, this.AfterFlg ? 1 : 0));
            obj.DataList.Add(new StdDbColumn("科コード", StdDbType.NUMBER, this.DeptCode));
            obj.DataList.Add(new StdDbColumn("指示医コード", StdDbType.NUMBER, this.DoctorCode));
            obj.DataList.Add(new StdDbColumn("オーダー番号", StdDbType.NUMBER, this.OrderSEQ));
            obj.DataList.Add(new StdDbColumn("施行部署１", StdDbType.NUMBER, this.SekouCode1));
            obj.DataList.Add(new StdDbColumn("施行部署２", StdDbType.NUMBER, this.SekouCode2));
            obj.DataList.Add(new StdDbColumn("装置番号", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("入力者コード", StdDbType.NUMBER, this.OrderStaffCode));
            obj.DataList.Add(new StdDbColumn("施行者コード", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("会計者コード", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("指示番号", StdDbType.NUMBER, this.OrderTime));
            obj.DataList.Add(new StdDbColumn("指示日", StdDbType.NUMBER, this.OrderDate));
            obj.DataList.Add(new StdDbColumn("指示時間", StdDbType.NUMBER, this.OrderTime4));
            obj.DataList.Add(new StdDbColumn("予約番号", StdDbType.NUMBER, this.RsvSEQ));
            obj.DataList.Add(new StdDbColumn("予約日", StdDbType.NUMBER, this.RsvDate));
            obj.DataList.Add(new StdDbColumn("予約時間", StdDbType.NUMBER, this.RsvTime1));
            obj.DataList.Add(new StdDbColumn("施行フラグ", StdDbType.NUMBER, this.SekouFlg ? 1 : 0));
            obj.DataList.Add(new StdDbColumn("会計フラグ", StdDbType.NUMBER, this.KaikeiFlg ? 1 : 0));
            obj.DataList.Add(new StdDbColumn("指示箋フラグ", StdDbType.NUMBER, this.PaperFlg ? 1 : 0));
            obj.DataList.Add(new StdDbColumn("ラベルフラグ", StdDbType.NUMBER, this.LabelFlg ? 1 : 0));
            obj.DataList.Add(new StdDbColumn("施行票フラグ", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("調剤済みフラグ", StdDbType.NUMBER, this.DrugFlg ? 9 : 0));
            obj.DataList.Add(new StdDbColumn("伝票種別", StdDbType.NUMBER, this.TaiinFlg ? 2 : 0));
            obj.DataList.Add(new StdDbColumn("まとめフラグ", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("院内区分", StdDbType.NUMBER, this.IngaiFlg ? 1 : 0));
            obj.DataList.Add(new StdDbColumn("開始日付", StdDbType.NUMBER, this.StartDate));
            obj.DataList.Add(new StdDbColumn("終了日付", StdDbType.NUMBER, this.EndDate));
            obj.DataList.Add(new StdDbColumn("受付番号", StdDbType.NUMBER, this.UkeSEQ));
            //            obj.DataList.Add(new StdDbColumn("受付連番", StdDbType.NUMBER, null));
            obj.DataList.Add(new StdDbColumn("所属コード", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("外来区分", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("初診フラグ", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("ＳＯＡＰ表示名称", StdDbType.VARCHAR2, this.DetailString));

            obj.DataList.Add(new StdDbColumn("削除日", StdDbType.NUMBER, this.DeleteDate));
            obj.DataList.Add(new StdDbColumn("削除時間", StdDbType.NUMBER, this.DeleteTime));
            obj.DataList.Add(new StdDbColumn("削除者コード", StdDbType.NUMBER, this.DeleteStaffCode));
            obj.DataList.Add(new StdDbColumn("削除者プログラム", StdDbType.NUMBER, this.DeleteDate > 0 ? 1 : 0));
            obj.DataList.Add(new StdDbColumn("削除種別", StdDbType.NUMBER, this.DeleteDate > 0 ? 1 : 0));

            sr.Msgs.Add(obj.InsertSQL(execute).Msg);


            obj.Table = "ＮＴフラグヘッダー";

            obj.DataList.Clear();

            obj.DataList.Add(new StdDbColumn("施行予定日", StdDbType.NUMBER, this.SekouDate));
            obj.DataList.Add(new StdDbColumn("施行予定時間", StdDbType.NUMBER, 9999));
            obj.DataList.Add(new StdDbColumn("患者コード", StdDbType.NUMBER, this.PtId));
            obj.DataList.Add(new StdDbColumn("保険ビット", StdDbType.NUMBER, this.Ins));
            obj.DataList.Add(new StdDbColumn("診療区分", StdDbType.NUMBER, this.KouiCode));
            obj.DataList.Add(new StdDbColumn("連番", StdDbType.NUMBER, this.SEQ));
            obj.DataList.Add(new StdDbColumn("入外区分", StdDbType.NUMBER, this.InOut));
            obj.DataList.Add(new StdDbColumn("オーダー区分", StdDbType.NUMBER, this.AfterFlg ? 1 : 0));
            obj.DataList.Add(new StdDbColumn("科コード", StdDbType.NUMBER, this.DeptCode));
            obj.DataList.Add(new StdDbColumn("指示医コード", StdDbType.NUMBER, this.DoctorCode));
            obj.DataList.Add(new StdDbColumn("オーダー番号", StdDbType.NUMBER, this.OrderSEQ));

            obj.DataList.Add(new StdDbColumn("レポート発行", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("レポート有無", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("汎用フラグ１", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("汎用フラグ２", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("汎用フラグ３", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("汎用フラグ４", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("汎用フラグ５", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("汎用コード１", StdDbType.VARCHAR2, null));
            obj.DataList.Add(new StdDbColumn("汎用コード２", StdDbType.VARCHAR2, null));
            obj.DataList.Add(new StdDbColumn("汎用コード３", StdDbType.VARCHAR2, null));
            obj.DataList.Add(new StdDbColumn("汎用コード４", StdDbType.VARCHAR2, null));
            obj.DataList.Add(new StdDbColumn("汎用コード５", StdDbType.VARCHAR2, null));
            obj.DataList.Add(new StdDbColumn("指示受コード", StdDbType.NUMBER, 0));

            sr.Msgs.Add(obj.InsertSQL(execute).Msg);


            obj.Table = "ＮＴオーダーディティール";

            // 最初のディティールのみ「診療区分」をセットする。
            int i = 1;

            foreach (OrderDetail detail in this.DetailList)
            {
                obj.DataList.Clear();

                obj.DataList.Add(new StdDbColumn("オーダー番号", StdDbType.NUMBER, this.OrderSEQ));
                obj.DataList.Add(new StdDbColumn("施行予定日", StdDbType.NUMBER, this.SekouDate));
                obj.DataList.Add(new StdDbColumn("施行予定時間", StdDbType.NUMBER, 9999));
                obj.DataList.Add(new StdDbColumn("患者コード", StdDbType.NUMBER, this.PtId));
                obj.DataList.Add(new StdDbColumn("保険ビット", StdDbType.NUMBER, this.Ins));
                obj.DataList.Add(new StdDbColumn("ＳＤＣＤ", StdDbType.NUMBER, detail.SDCD));
                obj.DataList.Add(new StdDbColumn("診療区分", StdDbType.CHAR, i == 1 ? this.KouiCode : ""));
                obj.DataList.Add(new StdDbColumn("明細連番", StdDbType.NUMBER, i));
                obj.DataList.Add(new StdDbColumn("オーダーコード", StdDbType.CHAR, detail.OrderCode));
                obj.DataList.Add(new StdDbColumn("表示数量", StdDbType.VARCHAR2, detail.Qty));
                obj.DataList.Add(new StdDbColumn("数量", StdDbType.NUMBER, detail.Qty));
                obj.DataList.Add(new StdDbColumn("表示回数", StdDbType.VARCHAR2, detail.Times > 0 ? detail.Times.ToString() : null));
                obj.DataList.Add(new StdDbColumn("回数", StdDbType.NUMBER, detail.Times));
                obj.DataList.Add(new StdDbColumn("コメント", StdDbType.VARCHAR2, detail.OrderName));
                obj.DataList.Add(new StdDbColumn("施行区分", StdDbType.NUMBER, 0));
                obj.DataList.Add(new StdDbColumn("分１", StdDbType.NUMBER, 0));
                obj.DataList.Add(new StdDbColumn("分２", StdDbType.NUMBER, 0));
                obj.DataList.Add(new StdDbColumn("分３", StdDbType.NUMBER, 0));
                obj.DataList.Add(new StdDbColumn("分４", StdDbType.NUMBER, 0));
                obj.DataList.Add(new StdDbColumn("分５", StdDbType.NUMBER, 0));
                obj.DataList.Add(new StdDbColumn("分６", StdDbType.NUMBER, 0));
                obj.DataList.Add(new StdDbColumn("分７", StdDbType.NUMBER, 0));
                obj.DataList.Add(new StdDbColumn("分８", StdDbType.NUMBER, 0));

                sr.Msgs.Add(obj.InsertSQL(execute).Msg);

                i++;
            }


            obj.Table = "ＬＧオーダーディティール";

            // 最初のディティールのみ「診療区分」をセットする。
            i = 1;

            foreach (OrderDetail detail in this.DetailList)
            {
                obj.DataList.Clear();

                obj.DataList.Add(new StdDbColumn("オーダー番号", StdDbType.NUMBER, this.OrderSEQ));
                obj.DataList.Add(new StdDbColumn("施行予定日", StdDbType.NUMBER, this.SekouDate));
                obj.DataList.Add(new StdDbColumn("施行予定時間", StdDbType.NUMBER, 9999));
                obj.DataList.Add(new StdDbColumn("患者コード", StdDbType.NUMBER, this.PtId));
                obj.DataList.Add(new StdDbColumn("保険ビット", StdDbType.NUMBER, this.Ins));
                obj.DataList.Add(new StdDbColumn("ＳＤＣＤ", StdDbType.NUMBER, detail.SDCD));
                obj.DataList.Add(new StdDbColumn("診療区分", StdDbType.CHAR, i == 1 ? this.KouiCode : ""));
                obj.DataList.Add(new StdDbColumn("明細連番", StdDbType.NUMBER, i));
                obj.DataList.Add(new StdDbColumn("オーダーコード", StdDbType.CHAR, detail.OrderCode));
                obj.DataList.Add(new StdDbColumn("表示数量", StdDbType.VARCHAR2, detail.Qty));
                obj.DataList.Add(new StdDbColumn("数量", StdDbType.NUMBER, detail.Qty));
                obj.DataList.Add(new StdDbColumn("表示回数", StdDbType.VARCHAR2, detail.Times > 0 ? detail.Times.ToString() : null));
                obj.DataList.Add(new StdDbColumn("回数", StdDbType.NUMBER, detail.Times));
                obj.DataList.Add(new StdDbColumn("コメント", StdDbType.VARCHAR2, detail.OrderName));
                obj.DataList.Add(new StdDbColumn("施行区分", StdDbType.NUMBER, 0));
                obj.DataList.Add(new StdDbColumn("分１", StdDbType.NUMBER, 0));
                obj.DataList.Add(new StdDbColumn("分２", StdDbType.NUMBER, 0));
                obj.DataList.Add(new StdDbColumn("分３", StdDbType.NUMBER, 0));
                obj.DataList.Add(new StdDbColumn("分４", StdDbType.NUMBER, 0));
                obj.DataList.Add(new StdDbColumn("分５", StdDbType.NUMBER, 0));
                obj.DataList.Add(new StdDbColumn("分６", StdDbType.NUMBER, 0));
                obj.DataList.Add(new StdDbColumn("分７", StdDbType.NUMBER, 0));
                obj.DataList.Add(new StdDbColumn("分８", StdDbType.NUMBER, 0));

                obj.DataList.Add(new StdDbColumn("削除日", StdDbType.NUMBER, this.DeleteDate));
                obj.DataList.Add(new StdDbColumn("削除時間", StdDbType.NUMBER, this.DeleteTime));
                obj.DataList.Add(new StdDbColumn("削除者コード", StdDbType.NUMBER, this.DeleteStaffCode));
                obj.DataList.Add(new StdDbColumn("削除種別", StdDbType.NUMBER, this.DeleteDate > 0 ? 1 : 0));

                sr.Msgs.Add(obj.InsertSQL(execute).Msg);

                i++;
            }

            return sr;
        }

        public StdReturn Delete()
        {
            StdReturn sr = new StdReturn();

            int yyyyMMdd = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int HHmmss = int.Parse(DateTime.Now.ToString("HHmmss"));

            using (DbTransaction tran = Db.Connection.BeginTransaction())
            {
                try
                {
                    StdDbClass obj = new StdDbClass();

                    obj.Table = "ＬＧオーダーヘッダー";

                    obj.DataList.Add(new StdDbColumn("削除日", StdDbType.NUMBER, yyyyMMdd));
                    obj.DataList.Add(new StdDbColumn("削除時間", StdDbType.NUMBER, HHmmss));
                    obj.DataList.Add(new StdDbColumn("削除者コード", StdDbType.NUMBER, LoginUser.Id));

                    obj.WhereList.Add("オーダー番号 = " + this.OrderSEQ);

                    sr = obj.UpdateSQL();


                    obj.Table = "ＮＴオーダーヘッダー";

                    obj.WhereList.Add("オーダー番号 = " + this.OrderSEQ);

                    sr = obj.DeleteSQL();

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw new Exception(ex.Message);
                }
            }

            return sr;
        }
/*

        public StdControlOrder1 GetStdControlOrder1()
        {
            PatOrder order = new PatOrder();

            order.PtId = this.PtId;
            order.SekouDate = this.SekouDate;
            order.InOut = this.InOut;
            order.Shinku = this.KouiCode;
            order.Ins = this.Ins;
            order.Dept = this.DeptCode;
            order.Doctor = this.DoctorCode;

            int i = 1;

            foreach (OrderDetail tmp in this.DetailList)
            {
                PatOrderDetail detail = new PatOrderDetail();

                detail.DetailId = i.ToString();
                detail.PtId = this.PtId;
                detail.SekouDate = this.SekouDate;
                detail.Code = tmp.OrderCode;
                detail.Name = tmp.OrderName;
                detail.Qty = tmp.Qty;
                detail.QtyString = tmp.Qty.ToString();
                detail.Unit = tmp.Unit;
                detail.Times = tmp.Times;

                order.DetailList.Add(detail);

                i++;
            }

            return new StdControlOrder1(order);
        }
 */

        class OrderHeaderSEQ
        {
            public int SekouDate = 0;

            public int SekouDateShort
            {
                get
                {
                    return this.SekouDate % 1000000;
                }
            }

            /// <summary>
            /// 受付番号
            /// </summary>
            public long UkeSEQ = 0;

            public int UkeSEQShort
            {
                get
                {
                    return (int)(this.UkeSEQ % 100000);
                }
            }

            public int SEQ = 1;

            /// <summary>
            /// オーダー番号
            /// </summary>
            public long OrderSEQ = 0;

            public int OrderSEQShort
            {
                get
                {
                    return (int)(this.OrderSEQ % 100000);
                }
            }


            /// <summary>
            /// その日の最初の受付番号かどうか
            /// </summary>
            public bool UkeFirst = false;

            /// <summary>
            /// その日の最初のオーダー番号かどうか
            /// </summary>
            public bool OrderFirst = false;


            public OrderHeaderSEQ(int sekou_date)
            {
                this.SekouDate = sekou_date;
            }
        }


        class OrderDrugSEQ
        {
            /// <summary>
            /// 薬番の最大値を取得
            /// </summary>
            /// <param name="date"></param>
            /// <param name="in_out"></param>
            /// <param name="ingai"></param>
            /// <returns></returns>
            public static int GetMaxSEQ(int date, string in_out, bool ingai)
            {
                int seq = 0;

                if (in_out.Equals("1") && ingai)
                {
                    seq = 5000;
                }
                else if (in_out.Equals("2"))
                {
                    seq = 7000;
                }

                if (date.ToString().Length != 8 || in_out.Length == 0)
                {
                    return seq;
                }

                string cmd = "select * from IC08RC t" +
                    " where t.IC08RC_F04 = " + date +
                    " and t.IC08RC_F02 = " + in_out +
                    " and t.IC08RC_F01 = " + (ingai ? 5 : 6);

                List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    int.TryParse(tmp.DataDict["IC08RC_F05"].ToString(), out seq);
                    break;
                }

                return seq;
            }
        }
    }
}
