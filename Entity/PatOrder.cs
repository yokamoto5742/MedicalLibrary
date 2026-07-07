using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class PatOrder : StdEntity
    {
        /// <summary>
        /// オーダー番号
        /// </summary>
        public string OrderId = "";

        /// <summary>
        /// 受付番号
        /// </summary>
        public string UkeId = "";

        /// <summary>
        /// 連番
        /// 同一受付番号での連番
        /// </summary>
        public int UkeSEQ = 0;

        public string SekouDate = "";

        public string SekouDateString
        {
            get
            {
                return DateTimeAgent.DateFormat(SekouDate, DateTimeAgent.DateFormatKind.LONG);
            }
        }

        public string SekouDateStringShort
        {
            get
            {
                return DateTimeAgent.DateFormat(SekouDate, DateTimeAgent.DateFormatKind.SHORT);
            }
        }

        public string SekouTime = "";

        public string SekouTimeString
        {
            get
            {
                return DateTimeAgent.TimeFormat6(this.SekouTime, 4, false);
            }
        }

        PatBase _Pat = new PatBase();

        public PatBase Pat
        {
            get
            {
                return this._Pat;
            }
        }

        string shinku = "";

        public string Shinku
        {
            set
            {
                this.shinku = value;
            }
            get
            {
                if (this.shinku.Length > 0)
                {
                    return this.shinku;
                }
                else
                {
                    string s = "";

                    foreach (PatOrderDetail detail in this.DetailList)
                    {
                        if (detail.KouiCode.Length > 0)
                        {
                            s = detail.KouiCode;
                            break;
                        }
                    }

                    return s;
                }
            }
        }

        public string ShinkuString
        {
            get
            {
                string result = "";

                if (Dict.KouiDict.ContainsKey(Shinku))
                {
                    result = Dict.KouiDict[Shinku];
                }

                return result;
            }
        }

        public string InOut = "";

        public string InOutName
        {
            get
            {
                string result = "";

                if (InOut.Equals("1"))
                {
                    result = "外来";
                }
                else if (InOut.Equals("2"))
                {
                    result = "入院";
                }

                return result;
            }
        }

        public string InOutNameShort
        {
            get
            {
                string result = "";

                if (InOut.Equals("1"))
                {
                    result = "外";
                }
                else if (InOut.Equals("2"))
                {
                    result = "入";
                }

                return result;
            }
        }

        public string Dept = "";

        public string DeptName
        {
            get
            {
                string result = "";

                if (Dict.DeptDict.ContainsKey(Dept))
                {
                    result = Dict.DeptDict[Dept].ShortName;
                }

                return result;
            }
        }

        public string Doctor = "";

        public string DoctorName
        {
            get
            {
                string result = "";

                if (Dict.DoctorDict.ContainsKey(Doctor))
                {
                    result = Dict.DoctorDict[Doctor].Name;
                }

                return result;
            }
        }

        /// <summary>
        /// 施行部署１
        /// </summary>
        public string Sekou1 = "";

        public string SekouName1
        {
            get
            {
                string result = "";

                if (Dict.SekouDict.ContainsKey(Sekou1))
                {
                    result = Dict.SekouDict[Sekou1].ShortName;
                }

                return result;
            }
        }

        /// <summary>
        /// 施行部署２
        /// </summary>
        public string Sekou2 = "";

        public string SekouName2
        {
            get
            {
                string result = "";

                if (Dict.SekouDict.ContainsKey(Sekou2))
                {
                    result = Dict.SekouDict[Sekou2].ShortName;
                }

                return result;
            }
        }

        /// <summary>
        /// 指示日
        /// </summary>
        public string OrderDate = "";

        public string OrderDateString
        {
            get
            {
                return DateTimeAgent.DateFormat(OrderDate, DateTimeAgent.DateFormatKind.LONG);
            }
        }

        public string OrderDateStringShort
        {
            get
            {
                return DateTimeAgent.DateFormat(OrderDate, DateTimeAgent.DateFormatKind.SHORT);
            }
        }

        public string OrderTime = "";

        public string OrderTimeString
        {
            get
            {
                return DateTimeAgent.TimeFormat6(this.OrderTime, 4, false);
            }
        }

        /// <summary>
        /// 予約番号
        /// </summary>
        public long RsvSEQ = 0;

        /// <summary>
        /// 予約日
        /// </summary>
        public string RsvDate = "";

        public string RsvDateString
        {
            get
            {
                return DateTimeAgent.DateFormat(RsvDate, DateTimeAgent.DateFormatKind.LONG);
            }
        }

        public string RsvDateStringShort
        {
            get
            {
                return DateTimeAgent.DateFormat(RsvDate, DateTimeAgent.DateFormatKind.SHORT);
            }
        }

        public string RsvTime = "";

        public string RsvTimeString
        {
            get
            {
                return DateTimeAgent.TimeFormat6(this.RsvTime, 4, false);
            }
        }

        /// <summary>
        /// 施行フラグ
        /// </summary>
        public string SekouFlg = "";

        /// <summary>
        /// 会計フラグ
        /// </summary>
        public string KaikeiFlg = "";

        /// <summary>
        /// 指示箋フラグ
        /// </summary>
        public string PaperFlg = "";

        /// <summary>
        /// 院内区分
        /// 0: 院内, 1: 院外
        /// </summary>
        public string InnaiFlg = "";

        /// <summary>
        /// 臨時処方フラグ
        /// 1: 臨時処方
        /// </summary>
        public string RinjiFlg = "";

        /// <summary>
        /// 退院処方フラグ
        /// 1: 退院処方
        /// </summary>
        public string TaiinFlg = "";

        public string StartDate = "";

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

        public string EndDate = "";

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
        /// 入力者コード
        /// </summary>
        public string Staff = "";

        /// <summary>
        /// 入力者
        /// </summary>
        public string StaffName
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(this.Staff))
                {
                    s = Dict.StaffDict[this.Staff].Name;
                }

                return s;
            }
        }

        /// <summary>
        /// 施行者コード
        /// </summary>
        public string SekouStaff = "";

        /// <summary>
        /// 施行者
        /// </summary>
        public string SekouStaffName
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(this.SekouStaff))
                {
                    s = Dict.StaffDict[this.SekouStaff].Name;
                }

                return s;
            }
        }

        string _SOAP = "";

        public string SOAP
        {
            get
            {
                string s = "";

                if (this._SOAP.Length > 0)
                {
                    s = this._SOAP;
                }
                else
                {
                    s = this.DetailString;
                }

                return s;
            }
            set
            {
                this._SOAP = value;
            }
        }

        /// <summary>
        /// SOAP表示名称で最初に「×」が出てくるところまで
        /// </summary>
        public string SOAPHeader
        {
            get
            {
                string s = this.SOAP;

                int i = this.SOAP.IndexOf('×');

                if (i > 1)
                {
                    s = this.SOAP.Substring(0, i).TrimEnd(' ');
                }

                return s;
            }
        }

        /// <summary>
        /// 日/回数
        /// </summary>
        float times = 0.0F;

        /// <summary>
        /// 日/回数
        /// </summary>
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

                    foreach (PatOrderDetail detail in this.DetailList)
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

        /// <summary>
        /// 処方薬剤のリスト（医事連携プログラム用）
        /// </summary>
        public List<PatOrderDetail> DetailDrugList
        {
            get
            {
                List<PatOrderDetail> list = new List<PatOrderDetail>();

                // 診療区分が20番台でなければ対象外
                if (this.Shinku.CompareTo("20") < 0 ||
                    this.Shinku.CompareTo("30") >= 0)
                {
                    return list;
                }

                foreach (PatOrderDetail detail in this.DetailList)
                {
                    if (detail.DataType.Equals(2))
                    {
                        list.Add(detail);
                    }
                }

                return list;
            }
        }

        /// <summary>
        /// 処方用法コメントのリスト（医事連携プログラム用）
        /// </summary>
        public List<PatOrderDetail> DetailDrugDirectionCommentList
        {
            get
            {
                List<PatOrderDetail> list = new List<PatOrderDetail>();

                // 診療区分が20番台でなければ対象外
                if (this.Shinku.CompareTo("20") < 0 ||
                    this.Shinku.CompareTo("30") >= 0)
                {
                    return list;
                }

                foreach (PatOrderDetail detail in this.DetailList)
                {
                    if (!detail.DataType.Equals(2))
                    {
                        list.Add(detail);
                    }
                }

                return list;
            }
        }

        /// <summary>
        /// 医事に飛ばすかどうか
        /// </summary>
        public bool IsRece
        {
            get
            {
                bool b = false;

                foreach (PatOrderDetail detail in this.DetailList)
                {
                    // ReceCode が１つでもあれば飛ばす
                    if (detail.GetReceCode(this.InOut).Length > 0)
                    {
                        b = true;
                        break;
                    }
                }

                return b;
            }
        }

        public List<PatOrderDetail> DetailList = new List<PatOrderDetail>();

        /// <summary>
        /// DetailList から生成したＳＯＡＰ表示名称
        /// </summary>
        public string DetailString
        {
            get
            {
                string s = "";

                foreach (PatOrderDetail detail in this.DetailList)
                {
                    if (s.Length > 0)
                    {
                        s += ", ";
                    }

                    s += detail.Name;

                    if (detail.Unit.Length > 0 || !detail.Qty.Equals(1))
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

        public static Panel GetPanel(string order_id, int width, int height)
        {
            int w = width;
            int h = height;

            if (w < 200)
            {
                w = 200;
            }

            if (h < 200)
            {
                h = 200;
            }

            Panel p = new Panel();
            p.Width = w;
            p.Height = h;
            p.AutoScroll = true;

            PatOrder obj = PatOrder.Load(order_id);

            Label lb = new Label();
            lb.Location = new System.Drawing.Point(5, 5);
            lb.Size = new System.Drawing.Size(p.Width - 10, p.Height - 10);
            lb.AutoEllipsis = true;
            lb.Text = obj.SOAP;
            lb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb.BackColor = Color.White;

            p.Controls.Add(lb);

            return p;
        }


        public static List<PatOrder> GetListByPatCond(string pt_id, List<string> cond_list, string order_by = "", bool detail = false)
        {
            List<PatOrder> list = new List<PatOrder>();
            string cmd = "select * from D_ORDER_HEADER " +
                " where P_ID = " + pt_id;

            if (cond_list.Count > 0)
            {
                cmd += " and " + AppString.ConcatList(cond_list, " and ");
            }

            if (order_by.Length > 0)
            {
                cmd += " order by " + order_by;
            }
            else
            {
                cmd += " order by ORDER_DATE desc, ORDER_NO desc";
            }

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
            List<string> order_id_list = new List<string>();

            foreach (StdClass tmp in tmp_list)
            {
                PatOrder p = PatOrder.GetFromStdClass(tmp);

                order_id_list.Add(p.OrderId);

                list.Add(p);
            }

            if (detail)
            {
                List<PatOrderDetail> list2 = PatOrderDetail.Load(order_id_list);

                foreach (PatOrderDetail d in list2)
                {
                    foreach (PatOrder p in list)
                    {
                        if (p.OrderId.Equals(d.OrderId))
                        {
                            p.DetailList.Add(d);
                            break;
                        }
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 開始日付・終了日付を見て、該当期間中のオーダーを取得する
        /// （ワークシートの投薬データを取得するのに有効）
        /// </summary>
        /// <param name="pt_id">患者コード</param>
        /// <param name="start_date">開始日。空ならば今日</param>
        /// <param name="end_date">終了日。空ならば 29991231 </param>
        /// <param name="in_out">入外。空ならば両方</param>
        /// <param name="shinku_list">診療区分リスト</param>
        /// <param name="dept_list">診療科リスト</param>
        /// <param name="detail">ディティールを取得するか否か</param>
        /// <returns></returns>
        public static List<PatOrder> GetListByPatDates(string pt_id, string start_date, string end_date, string in_out, List<string> shinku_list = null, List<string> dept_list = null, bool detail = false)
        {
            List<PatOrder> tmpList = new List<PatOrder>();

            if (pt_id.Length == 0)
            {
                return tmpList;
            }

            string date1 = DateTimeAgent.IsDate(start_date) ? start_date : DateTime.Now.ToString("yyyyMMdd");
            string date2 = DateTimeAgent.IsDate(end_date) ? end_date : DateTime.Now.ToString("yyyyMMdd");

            string date_sql = "";

            // 診療区分に 21, 22 が含まれる場合は DATE_S, DATE_E で絞る

            if (shinku_list != null && (shinku_list.Contains("21") || shinku_list.Contains("22")))
            {
                date_sql = " and DATE_E >= " + date1 + " and DATE_S <= " + date2;
            }
            else
            {
                date_sql = " and ORDER_DATE >= " + date1 + " and ORDER_DATE <= " + date2;
            }

            string cmd = "select * from D_ORDER_HEADER " +
                " where P_ID = " + pt_id + date_sql;

            if (in_out.Equals("1"))
            {
                cmd += " and INOUT = 1";
            }
            else if (in_out.Equals("2"))
            {
                cmd += " and INOUT = 2";
            }

            if (shinku_list != null && shinku_list.Count > 0)
            {
                cmd += " and SHINKU in (" + AppString.ConcatList(shinku_list, ",") + ")";
            }

            if (dept_list != null && dept_list.Count > 0)
            {
                cmd += " and DEPT in (" + AppString.ConcatList(dept_list, ",") + ")";
            }

            cmd += " order by ORDER_DATE desc, RP_NO, ORDER_SEQ, ORDER_NO";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
            List<string> order_id_list = new List<string>();

            foreach (StdClass tmp in tmp_list)
            {
                PatOrder p = PatOrder.GetFromStdClass(tmp);

                order_id_list.Add(p.OrderId);

                tmpList.Add(p);
            }
            if (detail)
            {
                List<PatOrderDetail> tmpList2 = PatOrderDetail.Load(order_id_list);

                foreach (PatOrderDetail d in tmpList2)
                {
                    foreach (PatOrder p in tmpList)
                    {
                        if (p.OrderId.Equals(d.OrderId))
                        {
                            p.DetailList.Add(d);
                            break;
                        }
                    }
                }
            }

            return tmpList;
        }


        public static List<PatOrder> GetListBySekouDates(string start_date, string end_date, string in_out, List<string> shinku_list, List<string> dept_list, List<string> sekou1_list, bool mitei = false, bool detail = false)
        {
            List<PatOrder> tmpList = new List<PatOrder>();

            if (!DateTimeAgent.IsDate(start_date))
            {
                return tmpList;
            }

            List<string> sqls = new List<string>();

            if (DateTimeAgent.IsDate(end_date))
            {
                if (mitei)
                {
                    sqls.Add("((t1.ORDER_DATE >= " + start_date + " and t1.ORDER_DATE <= " + end_date + ") or t1.ORDER_DATE = 99999999)");
                }
                else
                {
                    sqls.Add("(t1.ORDER_DATE >= " + start_date + " and t1.ORDER_DATE <= " + end_date + ")");
                }
            }
            else
            {
                if (mitei)
                {
                    sqls.Add("(t1.ORDER_DATE >= " + start_date + ")");
                }
                else
                {
                    sqls.Add("(t1.ORDER_DATE >= " + start_date + " and t1.ORDER_DATE < 99999999)");
                }
            }

            if (in_out.Equals("1"))
            {
                sqls.Add("(t1.INOUT = 1)");
            }
            else if (in_out.Equals("2"))
            {
                sqls.Add("(t1.INOUT = 2)");
            }

            if (shinku_list.Count > 0)
            {
                sqls.Add("t1.SHINKU in (" + AppString.ConcatList(shinku_list, ",") + ")");
            }

            if (dept_list.Count > 0)
            {
                sqls.Add("t1.DEPT in (" + AppString.ConcatList(dept_list, ",") + ")");
            }

            if (sekou1_list.Count > 0)
            {
                sqls.Add("t1.SEKOU_CODE in (" + AppString.ConcatList(sekou1_list, ",") + ")");
            }

            // 条件が３つ未満なら件数が増えすぎるため終了
            if (sqls.Count < 3)
            {
                return tmpList;
            }

            string cmd = "select t1.*, t2.P_KANA, t2.P_NAME, t2.P_SEX, t2.P_BIRTHDAY_AD " +
                " from D_ORDER_HEADER t1, M_PATIENT t2 " +
                " where " + AppString.ConcatList(sqls, " and ") +
                " and t1.P_ID = t2.P_ID " +
                " order by t1.P_ID, t1.ORDER_DATE desc, t1.RP_NO, t1.ORDER_SEQ, t1.ORDER_NO";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
            List<string> order_id_list = new List<string>();

            foreach (StdClass tmp in tmp_list)
            {
                PatOrder p = PatOrder.GetFromStdClass(tmp);

                order_id_list.Add(p.OrderId);

                tmpList.Add(p);
            }
            if (detail)
            {
                List<PatOrderDetail> tmpList2 = PatOrderDetail.Load(order_id_list);

                foreach (PatOrderDetail d in tmpList2)
                {
                    foreach (PatOrder p in tmpList)
                    {
                        if (p.OrderId.Equals(d.OrderId))
                        {
                            p.DetailList.Add(d);
                            break;
                        }
                    }
                }
            }

            return tmpList;
        }


        /// <summary>
        /// 患者IDと施行予定日（ORDER_DATE）で検索
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="start_date"></param>
        /// <param name="end_date"></param>
        /// <param name="in_out"></param>
        /// <param name="shinku_list"></param>
        /// <param name="dept_list"></param>
        /// <param name="detail"></param>
        /// <returns></returns>
        public static List<PatOrder> GetListByPatSekouDates(string pt_id, string start_date, string end_date, string in_out, List<string> shinku_list, List<string> dept_list, List<string> sekou1_list, bool mitei = false, bool detail = false)
        {
            List<PatOrder> tmpList = new List<PatOrder>();

            if (pt_id.Length == 0)
            {
                return tmpList;
            }

            List<string> sqls = new List<string>();

            if (DateTimeAgent.IsDate(start_date))
            {
                if (DateTimeAgent.IsDate(end_date))
                {
                    if (mitei)
                    {
                        sqls.Add("((t1.ORDER_DATE >= " + start_date + " and t1.ORDER_DATE <= " + end_date + ") or t1.ORDER_DATE = 99999999)");
                    }
                    else
                    {
                        sqls.Add("(t1.ORDER_DATE >= " + start_date + " and t1.ORDER_DATE <= " + end_date + ")");
                    }
                }
                else
                {
                    if (mitei)
                    {
                        sqls.Add("(t1.ORDER_DATE >= " + start_date + ")");
                    }
                    else
                    {
                        sqls.Add("(t1.ORDER_DATE >= " + start_date + " and t1.ORDER_DATE < 99999999)");
                    }
                }
            }

            if (in_out.Equals("1"))
            {
                sqls.Add("(t1.INOUT = 1)");
            }
            else if (in_out.Equals("2"))
            {
                sqls.Add("(t1.INOUT = 2)");
            }

            if (shinku_list.Count > 0)
            {
                sqls.Add("t1.SHINKU in (" + AppString.ConcatList(shinku_list, ",") + ")");
            }

            if (dept_list.Count > 0)
            {
                sqls.Add("t1.DEPT in (" + AppString.ConcatList(dept_list, ",") + ")");
            }

            if (sekou1_list.Count > 0)
            {
                sqls.Add("t1.SEKOU_CODE in (" + AppString.ConcatList(sekou1_list, ",") + ")");
            }

            if (sqls.Count == 0)
            {
                return tmpList;
            }

            string cmd = "select t1.*, t2.P_KANA, t2.P_NAME, t2.P_SEX, t2.P_BIRTHDAY_AD " +
                " from D_ORDER_HEADER t1, M_PATIENT t2 " +
                " where t1.P_ID = " + pt_id +
                " and " + AppString.ConcatList(sqls, " and ") +
                " and t1.P_ID = t2.P_ID " +
                " order by t1.P_ID, t1.ORDER_DATE desc, t1.RP_NO, t1.ORDER_SEQ, t1.ORDER_NO";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
            List<string> order_id_list = new List<string>();

            foreach (StdClass tmp in tmp_list)
            {
                PatOrder p = PatOrder.GetFromStdClass(tmp);

                order_id_list.Add(p.OrderId);

                tmpList.Add(p);
            }

            if (detail)
            {
                List<PatOrderDetail> tmpList2 = PatOrderDetail.Load(order_id_list);

                foreach (PatOrderDetail d in tmpList2)
                {
                    foreach (PatOrder p in tmpList)
                    {
                        if (p.OrderId.Equals(d.OrderId))
                        {
                            p.DetailList.Add(d);
                            break;
                        }
                    }
                }
            }

            return tmpList;
        }


        /// <summary>
        /// 施行部署と施行予定日（ORDER_DATE）で検索
        /// </summary>
        /// <param name="date"></param>
        /// <param name="in_out"></param>
        /// <param name="shinku_list"></param>
        /// <param name="dept_list"></param>
        /// <param name="sekou1_list"></param>
        /// <param name="mitei"></param>
        /// <param name="detail"></param>
        /// <returns></returns>
        public static List<PatOrder> GetListByDate(string date, string in_out, List<string> shinku_list, List<string> dept_list, List<string> sekou1_list, bool mitei = false, bool detail = false)
        {
            List<PatOrder> tmpList = new List<PatOrder>();

            if (!DateTimeAgent.IsDate(date))
            {
                return tmpList;
            }

            List<string> sqls = new List<string>();

            if (mitei)
            {
                sqls.Add("t1.ORDER_DATE in (" + date + ", 99999999)");
            }
            else
            {
                sqls.Add("t1.ORDER_DATE = " + date);
            }

            if (in_out.Equals("1"))
            {
                sqls.Add("(t1.INOUT = 1)");
            }
            else if (in_out.Equals("2"))
            {
                sqls.Add("(t1.INOUT = 2)");
            }

            if (shinku_list.Count > 0)
            {
                sqls.Add("t1.SHINKU in (" + AppString.ConcatList(shinku_list, ",") + ")");
            }

            if (dept_list.Count > 0)
            {
                sqls.Add("t1.DEPT in (" + AppString.ConcatList(dept_list, ",") + ")");
            }

            if (sekou1_list.Count > 0)
            {
                sqls.Add("t1.SEKOU_CODE in (" + AppString.ConcatList(sekou1_list, ",") + ")");
            }

            if (sqls.Count == 0)
            {
                return tmpList;
            }

            string cmd = "select t1.*, t2.P_KANA, t2.P_NAME, t2.P_SEX, t2.P_BIRTHDAY_AD, t3.HOKEN_TYPE " +
                " from D_ORDER_HEADER t1, M_PATIENT t2, M_PATIENT_HOKEN t3 " +
                " where " + AppString.ConcatList(sqls, " and ") +
                " and t1.P_ID = t2.P_ID " +
                " and t1.P_ID = t3.P_ID and t1.P_HOKEN = t3.P_HOKEN " +
                " order by t1.P_ID, t1.ORDER_DATE desc, t1.RP_NO, t1.ORDER_SEQ, t1.ORDER_NO";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
            List<string> order_id_list = new List<string>();

            foreach (StdClass tmp in tmp_list)
            {
                PatOrder p = PatOrder.GetFromStdClass(tmp);

                order_id_list.Add(p.OrderId);

                tmpList.Add(p);
            }

            if (detail)
            {
                List<PatOrderDetail> tmpList2 = PatOrderDetail.Load(order_id_list);

                foreach (PatOrderDetail d in tmpList2)
                {
                    foreach (PatOrder p in tmpList)
                    {
                        if (p.OrderId.Equals(d.OrderId))
                        {
                            p.DetailList.Add(d);
                            break;
                        }
                    }
                }
            }

            return tmpList;
        }


        /// <summary>
        /// 施行予定日の期間を指定して会計未取込みオーダーを検索
        /// </summary>
        /// <param name="start_date"></param>
        /// <param name="end_date"></param>
        /// <param name="in_out"></param>
        /// <param name="dept_list"></param>
        /// <param name="detail"></param>
        /// <returns></returns>
        public static List<PatOrder> GetListByDatesNotKaikei(string start_date, string end_date, string in_out, List<string> dept_list, bool detail = false)
        {
            List<PatOrder> tmpList = new List<PatOrder>();

            if (!DateTimeAgent.IsDate(start_date) || !DateTimeAgent.IsDate(end_date))
            {
                return tmpList;
            }

            List<string> sqls = new List<string>();

            sqls.Add("(t1.ORDER_DATE >= " + start_date + " and t1.ORDER_DATE <= " + end_date + ")");

            if (in_out.Equals("1"))
            {
                sqls.Add("(t1.INOUT = 1)");
            }
            else if (in_out.Equals("2"))
            {
                sqls.Add("(t1.INOUT = 2)");
            }

            if (dept_list.Count > 0)
            {
                sqls.Add("t1.DEPT in (" + AppString.ConcatList(dept_list, ",") + ")");
            }

            if (sqls.Count == 0)
            {
                return tmpList;
            }

            string cmd = "select t1.*, t2.P_KANA, t2.P_NAME, t2.P_SEX, t2.P_BIRTHDAY_AD, t2.PROPERTY_4, t3.HOKEN_TYPE " +
                " from D_ORDER_HEADER t1, M_PATIENT t2, M_PATIENT_HOKEN t3 " +
                " where " + AppString.ConcatList(sqls, " and ") +
                " and t1.SEKOU_FLG = 1 and (t1.BILL_FLG is null or t1.BILL_FLG = 0) " +
                " and t1.P_ID = t2.P_ID " +
                " and t1.P_ID = t3.P_ID and t1.P_HOKEN = t3.P_HOKEN " +
                " order by t1.P_ID, t1.ORDER_DATE desc, t1.RP_NO, t1.ORDER_SEQ, t1.ORDER_NO";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
            List<string> order_id_list = new List<string>();

            foreach (StdClass tmp in tmp_list)
            {
                PatOrder p = PatOrder.GetFromStdClass(tmp);

                order_id_list.Add(p.OrderId);

                tmpList.Add(p);
            }

            if (detail)
            {
                List<PatOrderDetail> tmpList2 = PatOrderDetail.Load(order_id_list);

                foreach (PatOrderDetail d in tmpList2)
                {
                    foreach (PatOrder p in tmpList)
                    {
                        if (p.OrderId.Equals(d.OrderId))
                        {
                            p.DetailList.Add(d);
                            break;
                        }
                    }
                }
            }

            return tmpList;
        }



        /// <summary>
        /// 施行予定日の期間を指定して削除オーダーを検索
        /// </summary>
        /// <param name="start_date"></param>
        /// <param name="end_date"></param>
        /// <param name="in_out"></param>
        /// <param name="dept_list"></param>
        /// <param name="kaikei_done">true: 会計反映済も取り込む, false: 会計反映済は取り込まない</param>
        /// <param name="detail"></param>
        /// <returns></returns>
        public static List<PatOrder> GetDelListByDates(string start_date, string end_date, string in_out, List<string> dept_list, bool kaikei_done = false, bool detail = false)
        {
            List<PatOrder> tmpList = new List<PatOrder>();

            if (!DateTimeAgent.IsDate(start_date) || !DateTimeAgent.IsDate(end_date))
            {
                return tmpList;
            }

            List<string> sqls = new List<string>();

            sqls.Add("(t1.ORDER_DATE >= " + start_date + " and t1.ORDER_DATE <= " + end_date + ")");

            if (in_out.Equals("1"))
            {
                sqls.Add("(t1.INOUT = 1)");
            }
            else if (in_out.Equals("2"))
            {
                sqls.Add("(t1.INOUT = 2)");
            }

            if (dept_list.Count > 0)
            {
                sqls.Add("t1.DEPT in (" + AppString.ConcatList(dept_list, ",") + ")");
            }

            if (!kaikei_done)
            {
                sqls.Add("(t1.BILL_FLG is null or t1.BILL_FLG = 0)");
            }

            if (sqls.Count == 0)
            {
                return tmpList;
            }

            string cmd = "select t1.*, t2.P_KANA, t2.P_NAME, t2.P_SEX, t2.P_BIRTHDAY_AD, t2.PROPERTY_4, t3.HOKEN_TYPE " +
                " from L_ORDER_HEADER t1, M_PATIENT t2, M_PATIENT_HOKEN t3 " +
                " where " + AppString.ConcatList(sqls, " and ") +
                " and t1.UP_TYPE in (1, 2) " +
                " and t1.P_ID = t2.P_ID " +
                " and t1.P_ID = t3.P_ID and t1.P_HOKEN = t3.P_HOKEN " +
                " order by t1.P_ID, t1.ORDER_DATE desc, t1.RP_NO, t1.ORDER_SEQ, t1.ORDER_NO";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
            List<string> order_id_list = new List<string>();

            foreach (StdClass tmp in tmp_list)
            {
                PatOrder p = PatOrder.GetFromStdClass(tmp);

                order_id_list.Add(p.OrderId);

                tmpList.Add(p);
            }

            if (detail)
            {
                List<PatOrderDetail> tmpList2 = PatOrderDetail.Load(order_id_list);

                foreach (PatOrderDetail d in tmpList2)
                {
                    foreach (PatOrder p in tmpList)
                    {
                        if (p.OrderId.Equals(d.OrderId))
                        {
                            p.DetailList.Add(d);
                            break;
                        }
                    }
                }
            }

            return tmpList;
        }

        public static List<PatOrder> GetListByPatOrderCode(string pt_id, List<string> order_code_list, bool detail = false)
        {
            List<PatOrder> tmpList = new List<PatOrder>();

            if (pt_id.Length == 0 || AppString.ConcatList(order_code_list, ",").Length == 0)
            {
                return tmpList;
            }

            string cmd = "select t1.*, t2.P_KANA, t2.P_NAME, t2.P_SEX, t2.P_BIRTHDAY_AD, t3.HOKEN_TYPE " +
                " from D_ORDER_HEADER t1, M_PATIENT t2, M_PATIENT_HOKEN t3 " +
                " where ORDER_NO in " +
                " (select ORDER_NO from D_ORDER_DETAIL where P_ID = " + pt_id + " and ORDER_CODE in (" + AppString.ConcatList(order_code_list, ",", "'") + "))" +
                " and t1.P_ID = t2.P_ID " +
                " and t1.P_ID = t3.P_ID and t1.P_HOKEN = t3.P_HOKEN " +
                " order by t1.P_ID, t1.ORDER_DATE desc, t1.RP_NO, t1.ORDER_SEQ, t1.ORDER_NO";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
            List<string> order_id_list = new List<string>();

            foreach (StdClass tmp in tmp_list)
            {
                PatOrder p = PatOrder.GetFromStdClass(tmp);

                order_id_list.Add(p.OrderId);

                tmpList.Add(p);
            }
            if (detail)
            {
                List<PatOrderDetail> tmpList2 = PatOrderDetail.Load(order_id_list);

                foreach (PatOrderDetail d in tmpList2)
                {
                    foreach (PatOrder p in tmpList)
                    {
                        if (p.OrderId.Equals(d.OrderId))
                        {
                            p.DetailList.Add(d);
                            break;
                        }
                    }
                }
            }

            return tmpList;
        }

        /// <summary>
        /// 指定したオーダー番号のオーダーを取得する
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns></returns>
        public static PatOrder Load(string order_id)
        {
            PatOrder obj = new PatOrder();

            if (order_id.Length < 13)
            {
                return obj;
            }

            string cmd = "select * from D_ORDER_HEADER " +
                " where ORDER_NO = " + order_id;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                obj = GetFromStdClass(tmp);
                break;
            }

            return obj;
        }

        static PatOrder GetFromStdClass(StdClass tmp)
        {
            PatOrder obj = new PatOrder();

            obj.OrderId = tmp.GetDataString("ORDER_NO");
            obj.UkeId = tmp.GetDataString("RP_NO");
            obj.UkeSEQ = tmp.GetDataInt("ORDER_SEQ");
            obj.Pat.Id = tmp.GetDataString("P_ID");

            obj.Pat.Kana = tmp.GetDataString("P_KANA").Trim();
            obj.Pat.Name = tmp.GetDataString("P_NAME").Trim();
            obj.Pat.Sex = tmp.GetDataString("P_SEX");
            obj.Pat.Birth = tmp.GetDataString("P_BIRTHDAY_AD");
            obj.Pat.NoteCode = tmp.GetDataString("PROPERTY_4");

            obj.SekouDate = tmp.GetDataString("ORDER_DATE");
            obj.SekouTime = tmp.GetDataString("SEKOU_TIME");
            obj.Pat.Ins = tmp.GetDataString("P_HOKEN");
            obj.Pat.InsKind = tmp.GetDataString("HOKEN_TYPE");
            obj.Shinku = tmp.GetDataString("SHINKU");
            obj.InOut = tmp.GetDataString("INOUT");
            obj.Dept = tmp.GetDataString("DEPT");
            obj.Doctor = tmp.GetDataString("DR");
            obj.OrderDate = tmp.GetDataString("DIRECTION_DATE");
            obj.OrderTime = tmp.GetDataString("DIRECTION_TIME");
            obj.RsvSEQ = tmp.GetDataLong("YOYAKU_NO");
            obj.RsvDate = tmp.GetDataString("YOYAKU_DATE");
            obj.RsvTime = tmp.GetDataString("YOYAKU_TIME");
            obj.StartDate = tmp.GetDataString("DATE_S");
            obj.EndDate = tmp.GetDataString("DATE_E");
            //            obj.SOAP = tmp.GetDataString("ＳＯＡＰ表示名称");
            obj.SekouFlg = tmp.GetDataString("SEKOU_FLG");
            obj.KaikeiFlg = tmp.GetDataString("BILL_FLG");
            obj.PaperFlg = tmp.GetDataString("PRINT_FLG");
            obj.InnaiFlg = tmp.GetDataString("IN_TYPE");
            obj.RinjiFlg = tmp.GetDataString("SYOHO_TYPE").Equals("1") ? "1" : "";
            obj.TaiinFlg = tmp.GetDataString("SYOHO_TYPE").Equals("2") ? "1" : "";
            obj.Sekou1 = tmp.GetDataString("SEKOU_CODE");
            //            obj.Sekou2 = tmp.GetDataString("施行部署２");
            obj.SekouStaff = tmp.GetDataString("SEKOU_USR");
            obj.Staff = tmp.GetDataString("REG_USR");
            return obj;
        }

        public static void SetKaikeiFlg(string order_id, int flg, string user_id = "")
        {
            if (order_id.Length == 0)
            {
                return;
            }

            if (!flg.Equals(0) && !flg.Equals(1))
            {
                return;
            }

			int date = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
			int time = int.Parse(DateTime.Now.ToString("HHmmss"));

			int uid = 0;
			int.TryParse(user_id, out uid);

			if (flg.Equals(0))
			{
				date = 0;
				time = 0;
				uid = 0;
			}

            string cmd = "update D_ORDER_HEADER " +
                " set BILL_FLG = " + flg +
				" , BILL_USR = " + uid +
				" , BILL_DATE = " + date +
				" , BILL_TIME = " + time +
                " where ORDER_NO = " + order_id;

            DB.Db3.ExecuteNonQuery(cmd);

            cmd = "update L_ORDER_HEADER " +
                " set BILL_FLG = " + flg +
				" , BILL_USR = " + uid +
				" , BILL_DATE = " + date +
				" , BILL_TIME = " + time +
				" where ORDER_NO = " + order_id;

            DB.Db3.ExecuteNonQuery(cmd);
        }


        #region MACS

        /// <summary>
        /// 開始日付・終了日付を見て、該当期間中のオーダーを取得する
        /// </summary>
        /// <param name="pt_id">患者コード</param>
        /// <param name="start_date">開始日。空ならば今日</param>
        /// <param name="end_date">終了日。空ならば 29991231 </param>
        /// <param name="in_out">入外。空ならば両方</param>
        /// <param name="shinku_list">診療区分リスト</param>
        /// <param name="dept_list">診療科リスト</param>
        /// <param name="detail">ディティールを取得するか否か</param>
        /// <returns></returns>
        public static List<PatOrder> GetListByPatDatesMacs(string pt_id, string start_date, string end_date, string in_out, List<string> shinku_list = null, List<string> dept_list = null, bool detail = false)
        {
            List<PatOrder> tmpList = new List<PatOrder>();

            if (pt_id.Length == 0)
            {
                return tmpList;
            }

            string date1 = DateTimeAgent.IsDate(start_date) ? start_date : DateTime.Now.ToString("yyyyMMdd");
            string date2 = DateTimeAgent.IsDate(end_date) ? end_date : DateTime.Now.ToString("yyyyMMdd");

            string date_sql = "";

            // 診療区分に 21, 22 が含まれる場合は DATE_S, DATE_E で絞る

            if (shinku_list != null && (shinku_list.Contains("21") || shinku_list.Contains("22")))
            {
                date_sql = " and 終了日付 >= " + date1 + " and 開始日付 <= " + date2;
            }
            else
            {
                date_sql = " and 施行予定日 >= " + date1 + " and 施行予定日 <= " + date2;
            }

            string cmd = "select * from ＮＴオーダーヘッダー " +
                " where 患者コード = " + pt_id + date_sql;

            if (in_out.Equals("1"))
            {
                cmd += " and 入外区分 = 1";
            }
            else if (in_out.Equals("2"))
            {
                cmd += " and 入外区分 = 2";
            }

            if (shinku_list != null && shinku_list.Count > 0)
            {
                cmd += " and 診療区分 in (" + AppString.ConcatList(shinku_list, ",") + ")";
            }

            if (dept_list != null && dept_list.Count > 0)
            {
                cmd += " and 科コード in (" + AppString.ConcatList(dept_list, ",") + ")";
            }

            cmd += " order by 施行予定日 desc, 受付番号, 連番, オーダー番号";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);
            List<string> order_id_list = new List<string>();

            foreach (StdClass tmp in tmp_list)
            {
                PatOrder p = PatOrder.GetFromStdClassMacs(tmp);

                order_id_list.Add(p.OrderId);

                tmpList.Add(p);
            }

            if (detail)
            {
                List<PatOrderDetail> tmpList2 = PatOrderDetail.LoadMacs(order_id_list);

                foreach (PatOrderDetail d in tmpList2)
                {
                    foreach (PatOrder p in tmpList)
                    {
                        if (p.OrderId.Equals(d.OrderId))
                        {
                            p.DetailList.Add(d);
                            break;
                        }
                    }
                }
            }

            return tmpList;
        }

        static PatOrder GetFromStdClassMacs(StdClass tmp)
        {
            PatOrder obj = new PatOrder();

            obj.OrderId = tmp.GetDataString("オーダー番号");
            obj.UkeId = tmp.GetDataString("受付番号");
            obj.UkeSEQ = tmp.GetDataInt("連番");
            obj.Pat.Id = tmp.GetDataString("患者コード");
            obj.SekouDate = tmp.GetDataString("施行予定日");
            obj.SekouTime = tmp.GetDataString("装置番号");
            obj.Pat.Ins = tmp.GetDataString("保険ビット");
            obj.Shinku = tmp.GetDataString("診療区分");
            obj.InOut = tmp.GetDataString("入外区分");
            obj.Dept = tmp.GetDataString("科コード");
            obj.Doctor = tmp.GetDataString("指示医コード");
            obj.OrderDate = tmp.GetDataString("指示日");
            obj.OrderTime = tmp.GetDataString("指示時間");
            obj.RsvSEQ = tmp.GetDataLong("予約番号");
            obj.RsvDate = tmp.GetDataString("予約日");
            obj.RsvTime = tmp.GetDataString("予約時間");
            obj.StartDate = tmp.GetDataString("開始日付");
            obj.EndDate = tmp.GetDataString("終了日付");
            obj.SOAP = tmp.GetDataString("ＳＯＡＰ表示名称");
            obj.SekouFlg = tmp.GetDataString("施行フラグ");
            obj.KaikeiFlg = tmp.GetDataString("会計フラグ");
            obj.PaperFlg = tmp.GetDataString("指示箋フラグ");
            obj.InnaiFlg = tmp.GetDataString("院内区分");
            obj.RinjiFlg = tmp.GetDataString("伝票種別").Equals("1") ? "1" : "";
            obj.TaiinFlg = tmp.GetDataString("伝票種別").Equals("2") ? "1" : "";
            obj.Sekou1 = tmp.GetDataString("施行部署１");
            obj.Sekou2 = tmp.GetDataString("施行部署２");
            obj.SekouStaff = tmp.GetDataString("施行者コード");
            obj.Staff = tmp.GetDataString("入力者コード");

            return obj;
        }

        #endregion
    }
}
