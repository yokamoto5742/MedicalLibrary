using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class ComeReportOrder : PatOrder
    {
        public string OutSideDone = "";

        public List<ComeReportData> ReportList = new List<ComeReportData>();

        /// <summary>
        /// 有効な所見が存在するか
        /// </summary>
        public bool RecordFlg
        {
            get
            {
                bool b = false;

                foreach (ComeReportData r in this.ReportList)
                {
                    if (r.Status.Length == 0 || r.Status.Equals("0"))
                    {
                        continue;
                    }

                    // １つでも所見があれば true
                    b = true;
                    break;
                }

                return b;
            }
        }

        /// <summary>
        /// オーダーに「読影依頼」という文字が存在するか、
        /// もしくは所見が１つでも院外（読影依頼）になっているか
        /// </summary>
        public bool OutSideFlg
        {
            get
            {
                bool b = false;

                if (this.SOAP.Contains("読影依頼"))
                {
                    b = true;
                }
                else
                {
                    foreach (ComeReportData r in this.ReportList)
                    {
                        if (r.Status.Length == 0 || r.Status.Equals("0"))
                        {
                            continue;
                        }

                        // １つでも院外があれば読影依頼
                        if (r.OutSide.Equals("1"))
                        {
                            b = true;
                            break;
                        }
                    }
                }

                return b;
            }
        }

        /// <summary>
        /// 所見タブの名称とステータスを連結
        /// </summary>
        public string ReportListString
        {
            get
            {
                string s = "";

                foreach (ComeReportData r in this.ReportList)
                {
                    if (r.Status.Length == 0 || r.Status.Equals("0"))
                    {
                        continue;
                    }

                    if (s.Length > 0)
                    {
                        s += " ";
                    }

                    if (r.PDFStatus.Equals("1"))
                    {
                        // PDF生成済
                        s += "●";
                    }
                    else if (r.Status.Equals("1"))
                    {
                        // 完成
                        s += "○";
                    }
                    else if (r.Status.Equals("2"))
                    {
                        // 未完成
                        s += "△";
                    }
                    else if (r.Status.Equals("8") || r.Status.Equals("9"))
                    {
                        // 院外読影
                        s += "×";
                    }

                    s += r.Tab;
                }

                return s;
            }
        }


        static ComeReportOrder GetFromStdClass(StdClass tmp)
        {
            ComeReportOrder obj = new ComeReportOrder();

            obj.Pat.Id = tmp.GetDataString("P_ID");
            obj.Pat.Name = tmp.GetDataString("P_NAME").Trim();
            obj.Pat.Sex = tmp.GetDataString("P_SEX");
            obj.Pat.Birth = tmp.GetDataString("P_BIRTHDAY_AD");
            obj.OrderId = tmp.GetDataString("ORDER_NO");
            obj.OrderDate = tmp.GetDataString("DIRECTION_DATE");
            obj.SekouDate = tmp.GetDataString("ORDER_DATE");
            obj.SekouTime = tmp.GetDataString("SEKOU_TIME");
            obj.Sekou1 = tmp.GetDataString("SEKOU_CODE");
            obj.Dept = tmp.GetDataString("DEPT");
            obj.Doctor = tmp.GetDataString("DR");
//            obj.Staff = tmp.GetDataString("REG_USR");
            obj.InOut = tmp.GetDataString("INOUT");
            obj.Pat.Ins = tmp.GetDataString("P_HOKEN");
            obj.Pat.InsKind = tmp.GetDataString("HOKEN_TYPE");
            obj.SekouFlg = tmp.GetDataString("SEKOU_FLG");
            obj.OutSideDone = tmp.GetDataString("OUTSIDE_DONE");
            return obj;
        }


        public static List<ComeReportOrder> GetList1(string pt_id, string[] sekou_list, bool get_report = true)
        {
            List<ComeReportOrder> list = new List<ComeReportOrder>();

            if (pt_id.Length == 0 || AppString.ConcatList(sekou_list, ",").Length == 0)
            {
                return list;
            }
            string cmd = "select * from D_ORDER_HEADER t, M_PATIENT_HOKEN m" +
                " where t.P_ID = " + pt_id + " and t.SEKOU_CODE in (" + AppString.ConcatList(sekou_list, ",") + ") and t.ORDER_DATE <= " + DateTime.Now.ToString("yyyyMMdd") +
                " and t.P_ID = m.P_ID and t.P_HOKEN = m.P_HOKEN " +
                " order by t.ORDER_DATE desc, t.ORDER_NO desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
            List<string> order_ids = new List<string>();

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
                order_ids.Add(tmp.GetDataString("ORDER_NO"));
            }

            List<PatOrderDetail> detail_list = PatOrderDetail.Load(order_ids);

            foreach (PatOrderDetail detail in detail_list)
            {
                foreach (ComeReportOrder order in list)
                {
                    if (order.OrderId.Equals(detail.OrderId))
                    {
                        order.DetailList.Add(detail);
                        break;
                    }
                }
            }
            // ComeReportData も取得する場合
            if (get_report && order_ids.Count > 0)
            {
                List<ComeReportData> list2 = ComeReportData.GetListByOrderIds(order_ids);

                foreach (ComeReportData r in list2)
                {
                    foreach (ComeReportOrder order in list)
                    {
                        if (!order.OrderId.Equals(r.OrderId))
                        {
                            continue;
                        }

                        order.ReportList.Add(r);
                    }
                }
            }

            return list;
        }


        public static List<ComeReportOrder> GetList2(string date1, string date2, string[] sekou_list, bool get_report = true)
        {
            List<ComeReportOrder> list = new List<ComeReportOrder>();

            if (date1.Length != 8 || date2.Length != 8 || AppString.ConcatList(sekou_list, ",").Length == 0)
            {
                return list;
            }
            string cmd = "select ORDER_NO, SEKOU_CODE, ORDER_DATE, DEPT, DR, INOUT, SEKOU_FLG, t2.P_ID, P_NAME, P_SEX, P_BIRTHDAY_AD " +
                " from D_ORDER_HEADER t1 inner join M_PATIENT t2 on t1.P_ID = t2.P_ID " +
                " where t1.SEKOU_CODE in (" + AppString.ConcatList(sekou_list, ",") + ") and t1.ORDER_DATE >= " + date1 + " and t1.ORDER_DATE <= " + date2 +
                " order by ORDER_DATE desc, ORDER_NO desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
            List<string> order_ids = new List<string>();

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
                order_ids.Add(tmp.GetDataString("ORDER_NO"));
            }

            List<PatOrderDetail> detail_list = PatOrderDetail.Load(order_ids);

            foreach (PatOrderDetail detail in detail_list)
            {
                foreach (ComeReportOrder order in list)
                {
                    if (order.OrderId.Equals(detail.OrderId))
                    {
                        order.DetailList.Add(detail);
                        break;
                    }
                }
            }
            // ComeReportData も取得する場合
            if (get_report && order_ids.Count > 0)
            {
                List<ComeReportData> list2 = ComeReportData.GetListByOrderIds(order_ids);

                foreach (ComeReportData r in list2)
                {
                    foreach (ComeReportOrder order in list)
                    {
                        if (!order.OrderId.Equals(r.OrderId))
                        {
                            continue;
                        }

                        order.ReportList.Add(r);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 施行予定日の期間および施行部署コードを指定したオーダーのうち
        /// オーダーに「読影依頼」があるものを COME_REPORT_OUTSIDE のデータと合わせて抽出する
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="sekou_list"></param>
        /// <returns></returns>
        public static List<ComeReportOrder> GetList3(string date1, string date2, string[] sekou_list, bool get_report = true)
        {
            List<ComeReportOrder> list = new List<ComeReportOrder>();

            if (date1.Length != 8 || date2.Length != 8 || AppString.ConcatList(sekou_list, ",").Length == 0)
            {
                return list;
            }
            string cmd = "select ORDER_NO, SEKOU_CODE, ORDER_DATE, DEPT, DR, INOUT, SEKOU_FLG, t2.P_ID, P_NAME, P_SEX, P_BIRTHDAY_AD, OUTSIDE_DONE " +
                " from D_ORDER_HEADER" + Env.DB_LINK + " t1 inner join M_PATIENT" + Env.DB_LINK + " t2 on t1.P_ID = t2.P_ID left join COME_REPORT_OUTSIDE t3 on t1.ORDER_NO = t3.ORDER_ID " +
                " where t1.SEKOU_CODE in (" + AppString.ConcatList(sekou_list, ",") + ") and t1.ORDER_DATE >= " + date1 + " and t1.ORDER_DATE <= " + date2 +
                " and t1.ORDER_NO in (select ORDER_NO from D_ORDER_DETAIL" + Env.DB_LINK + " where ORDER_DATE >= " + date1 + " and ORDER_DATE <= " + date2 + "and ORDER_COMMENT like '%読影依頼%')" +
                " order by ORDER_DATE desc, ORDER_NO desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);
            List<string> order_ids = new List<string>();

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
                order_ids.Add(tmp.GetDataString("ORDER_NO"));
            }

            List<PatOrderDetail> detail_list = PatOrderDetail.Load(order_ids);

            foreach (PatOrderDetail detail in detail_list)
            {
                foreach (ComeReportOrder order in list)
                {
                    if (order.OrderId.Equals(detail.OrderId))
                    {
                        order.DetailList.Add(detail);
                        break;
                    }
                }
            }
            // ComeReportData も取得する場合
            if (get_report && order_ids.Count > 0)
            {
                List<ComeReportData> list2 = ComeReportData.GetListByOrderIds(order_ids);

                foreach (ComeReportData r in list2)
                {
                    foreach (ComeReportOrder order in list)
                    {
                        if (!order.OrderId.Equals(r.OrderId))
                        {
                            continue;
                        }

                        order.ReportList.Add(r);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 施行予定日の期間および施行部署コードを指定したオーダーのうち
        /// 所見で「読影依頼」が指定されたものを COME_REPORT_OUTSIDE のデータと合わせて抽出する
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="sekou_list"></param>
        /// <returns></returns>
        public static List<ComeReportOrder> GetList4(string date1, string date2, string[] sekou_list, bool get_report = true)
        {
            List<ComeReportOrder> list = new List<ComeReportOrder>();

            if (date1.Length != 8 || date2.Length != 8 || AppString.ConcatList(sekou_list, ",").Length == 0)
            {
                return list;
            }
            string cmd = "select distinct ORDER_NO, SEKOU_CODE, ORDER_DATE, DEPT, DR, INOUT, SEKOU_FLG, t2.P_ID, P_NAME, P_SEX, P_BIRTHDAY_AD " +
                ", (select OUTSIDE_DONE from COME_REPORT_OUTSIDE t where t.ORDER_ID = t1.ORDER_NO) OUTSIDE_DONE " +
                " from D_ORDER_HEADER" + Env.DB_LINK + " t1, M_PATIENT" + Env.DB_LINK + " t2, COME_REPORT t3 " +
                " where t1.SEKOU_CODE in (" + AppString.ConcatList(sekou_list, ",") + ") and t1.ORDER_DATE >= " + date1 + " and t1.ORDER_DATE <= " + date2 +
                " and t3.STATUS in (8,9) " +
                " and t1.P_ID = t2.P_ID" +
                " and t1.ORDER_NO = t3.ORDER_ID" +
                " order by ORDER_DATE desc, ORDER_NO desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);
            List<string> order_ids = new List<string>();

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
                order_ids.Add(tmp.GetDataString("ORDER_NO"));
            }

            List<PatOrderDetail> detail_list = PatOrderDetail.Load(order_ids);

            foreach (PatOrderDetail detail in detail_list)
            {
                foreach (ComeReportOrder order in list)
                {
                    if (order.OrderId.Equals(detail.OrderId))
                    {
                        order.DetailList.Add(detail);
                        break;
                    }
                }
            }
            // ComeReportData も取得する場合
            if (get_report && order_ids.Count > 0)
            {
                List<ComeReportData> list2 = ComeReportData.GetListByOrderIds(order_ids);

                foreach (ComeReportData r in list2)
                {
                    foreach (ComeReportOrder order in list)
                    {
                        if (!order.OrderId.Equals(r.OrderId))
                        {
                            continue;
                        }

                        order.ReportList.Add(r);
                    }
                }
            }

            return list;
        }


        /// <summary>
        /// PDF生成対象を抽出する
        /// （すべての所見が STATUS = 1 となっていて、かつ PDF_SAVE がセットされていないもの）
        /// </summary>
        /// <returns></returns>
        public static List<ComeReportOrder> GetList5(bool get_report = true)
        {
            List<ComeReportOrder> list = new List<ComeReportOrder>();

            List<string> order_ids = new List<string>();
            string orders = "";

            // 完成している所見があるものを取得する
            string cmd = "select distinct ORDER_ID from COME_REPORT where STATUS = 1 and (PDF_SAVE is NULL or PDF_SAVE = 0)";
            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                if (!order_ids.Contains(tmp.GetDataString("ORDER_ID")))
                {
                    order_ids.Add(tmp.GetDataString("ORDER_ID"));

                    if (orders.Length > 0)
                    {
                        orders += ",";
                    }

                    orders += tmp.GetDataString("ORDER_ID");
                }
            }

            // 未完成の所見があれば除外する
            cmd = "select distinct ORDER_ID from COME_REPORT where STATUS = 2 and ORDER_ID in (" + orders + ")";
            tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                if (order_ids.Contains(tmp.GetDataString("ORDER_ID")))
                {
                    order_ids.Remove(tmp.GetDataString("ORDER_ID"));
                }
            }

            cmd = "select ORDER_NO, SEKOU_CODE, ORDER_DATE, DEPT, DR, INOUT, SEKOU_FLG, tm.P_ID, P_NAME, P_SEX, P_BIRTHDAY_AD " +
                " from D_ORDER_HEADER th inner join M_PATIENT tm on th.P_ID = tm.P_ID " +
                " where ORDER_NO in (" + AppString.ConcatList(order_ids, ",") + ") " +
                " order by ORDER_DATE desc, ORDER_NO desc";

            tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            List<PatOrderDetail> detail_list = PatOrderDetail.Load(order_ids);

            foreach (PatOrderDetail detail in detail_list)
            {
                foreach (ComeReportOrder order in list)
                {
                    if (order.OrderId.Equals(detail.OrderId))
                    {
                        order.DetailList.Add(detail);
                        break;
                    }
                }
            }

            // ComeReportData も取得する場合
            if (get_report && order_ids.Count > 0)
            {
                List<ComeReportData> list2 = ComeReportData.GetListByOrderIds(order_ids);

                foreach (ComeReportData r in list2)
                {
                    foreach (ComeReportOrder order in list)
                    {
                        if (!order.OrderId.Equals(r.OrderId))
                        {
                            continue;
                        }

                        order.ReportList.Add(r);
                    }
                }
            }

            return list;
        }


        /// <summary>
        /// 完成された所見のPDFステータスを 1 にする
        /// </summary>
        /// <param name="order_id"></param>
        public void DonePDF()
        {
            if (this.OrderId.Length == 0)
            {
                return;
            }

            string cmd = "update COME_REPORT set PDF_SAVE = 1, PDF_DATE = " + DateTime.Now.ToString("yyyyMMdd") + ", PDF_TIME = " + DateTime.Now.ToString("HHmmss") +
                " where ORDER_ID = " + this.OrderId + " and STATUS = 1";

            DB.Db2.ExecuteNonQuery(cmd);
        }


        /// <summary>
        /// 院外読影にする。（手動。メッセージ送信とDB変更）
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="fromStaff"></param>
        public static void ReportOutSide(string order_id, string fromStaff)
        {
            if (order_id.Length == 0)
            {
                return;
            }

            // 作成途中のものは作成者にメッセージを送る。（読影依頼に出す検査のみ）
            string cmd = "select th.P_ID 患者コード, th.ORDER_DATE 施行予定日, th.SEKOU_CODE 施行部署１, TAB, CONT, STAFF " +
                " from COME_REPORT tr inner join D_ORDER_HEADER" + Env.DB_LINK + " th on tr.ORDER_ID = th.ORDER_NO " +
                " where tr.ORDER_ID = " + order_id + " and th.ORDER_NO = " + order_id + " and STATUS = 2 and STAFF is not NULL";
            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            string msg = "";

            foreach (StdClass tmp in tmp_list)
            {
                msg = ComeReportSettings.Current.PDFMaster1.PreMsg + "\r\n\r\n";

                if (Dict.SekouDict.ContainsKey(tmp.GetDataString("施行部署１")))
                {
                    msg += Dict.SekouDict[tmp.GetDataString("施行部署１")].FullName + "　";
                }

                msg += tmp.GetDataString("施行予定日").Insert(4, "/").Insert(7, "/") + " 施行\r\n\r\n";
                msg += "【" + tmp.GetDataString("TAB") + "】\r\n" + tmp.GetDataString("CONT");

                KarteMessage km = new KarteMessage();
                km.FromCode = fromStaff;
                km.ToCode = tmp.GetDataString("STAFF");
                km.Title = ComeReportSettings.Current.PDFMaster1.Title;
                km.Msg = msg;
                km.PtId = tmp.GetDataString("患者コード");
                km.Priority = "2";

                km.Send();
                System.Threading.Thread.Sleep(2000);
            }

            // 指定されたオーダー番号のものを院外にする。
            // （手動なので施行部署や完成・未完成は関係なくすべて）
            cmd = "update COME_REPORT set OUTSIDE = 1 where ORDER_ID = " + order_id;
            DB.Db2.ExecuteNonQuery(cmd);

            // 指定されたオーダー番号のもので未完成（STATUS = 2）のものはPDF対象外（STATUS = 8）にする。
            cmd = "update COME_REPORT set STATUS = 8 where ORDER_ID = " + order_id + " and STATUS = 2";
            DB.Db2.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 院外読影にする。（自動。メッセージ送信とDB変更）
        /// 指定日（デフォルト：１週間前）以前のオーダーで未完成のものは院外にする。
        /// 作成途中のものは作成者にメッセージを送る。
        /// </summary>
        /// <param name="end_date">指定日。これ以前の施行予定日のオーダーが対象となる。</param>
        /// <param name="fromStaff"></param>
        public static void ReportOutSideAll(string end_date, string fromStaff)
        {
            string sekou_sql = "";
            string sekou_codes = "";

            foreach (string s in ComeReportSettings.Current.OutSideList)
            {
                if (sekou_codes.Length > 0)
                {
                    sekou_codes += ",";
                }

                sekou_codes += s;
            }

            if (sekou_codes.Length > 0)
            {
                sekou_sql = " and SEKOU_CODE in (" + sekou_codes + ")";
            }

            string order_ids = "";

            // 作成途中のオーダー番号を抽出
            string cmd = "select distinct ORDER_ID from COME_REPORT where STATUS = 2";
            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                if (order_ids.Length > 0)
                {
                    order_ids += ",";
                }

                order_ids += tmp.GetDataString("ORDER_ID");
            }

            if (sekou_codes.Length == 0 || order_ids.Length == 0)
            {
                return;
            }

            // 作成途中のものは作成者にメッセージを送る。（読影依頼に出す検査のみ）
            cmd = "select th.P_ID 患者コード, th.ORDER_DATE 施行予定日, th.SEKOU_CODE 施行部署１, TAB, CONT, STAFF " +
                " from COME_REPORT tr inner join D_ORDER_HEADER" + Env.DB_LINK + " th on tr.ORDER_ID = th.ORDER_NO " +
                " where tr.ORDER_ID in (" + order_ids + ") and th.ORDER_NO in (" + order_ids + ") and th.ORDER_DATE <= " + end_date + " and STATUS = 2 and STAFF is not NULL" + sekou_sql;
            tmp_list = StdClass.GetList(DB.Db2, cmd);

            string msg = "";

            foreach (StdClass tmp in tmp_list)
            {
                msg = ComeReportSettings.Current.PDFMaster1.PreMsg + "\r\n\r\n";

                if (Dict.SekouDict.ContainsKey(tmp.GetDataString("施行部署１")))
                {
                    msg += Dict.SekouDict[tmp.GetDataString("施行部署１")].FullName + "　";
                }

                msg += tmp.GetDataString("施行予定日").Insert(4, "/").Insert(7, "/") + " 施行\r\n\r\n";
                msg += "【" + tmp.GetDataString("TAB") + "】\r\n" + tmp.GetDataString("CONT");

                KarteMessage km = new KarteMessage();
                km.FromCode = fromStaff;
                km.ToCode = tmp.GetDataString("STAFF");
                km.Title = ComeReportSettings.Current.PDFMaster1.Title;
                km.Msg = msg;
                km.PtId = tmp.GetDataString("患者コード");
                km.Priority = "2";

                km.Send();
                System.Threading.Thread.Sleep(2000);
            }

            // 指定期間を過ぎても未完成（STATUS = 2）のもので
            // 読影依頼に出す検査（＝指定した施行部署コードの検査）は院外（OUTSIDE = 1）にしてPDF対象外（STATUS = 9）にする。
            cmd = "update COME_REPORT set STATUS = 9, OUTSIDE = 1 " +
                " where (ORDER_ID, REPORT_ID) in " +
                " (select ORDER_ID, REPORT_ID from COME_REPORT tr inner join D_ORDER_HEADER" + Env.DB_LINK + " th on tr.ORDER_ID = th.ORDER_NO " +
                "  where tr.ORDER_ID in (" + order_ids + ") and th.ORDER_NO in (" + order_ids + ") and th.ORDER_DATE <= " + end_date + " and STATUS = 2 " + sekou_sql + ")";
            DB.Db2.ExecuteNonQuery(cmd);

            // 指定期間を過ぎても未完成（STATUS = 2）のもので
            // 読影依頼に出さない検査（＝指定外の施行部署コードの検査）はPDF対象外（STATUS = 9）にする。
            cmd = "update COME_REPORT set STATUS = 9 " +
                " where (ORDER_ID, REPORT_ID) in " +
                " (select ORDER_ID, REPORT_ID from COME_REPORT tr inner join D_ORDER_HEADER" + Env.DB_LINK + " th on tr.ORDER_ID = th.ORDER_NO " +
                "  where tr.ORDER_ID in (" + order_ids + ") and th.ORDER_NO in (" + order_ids + ") and th.ORDER_DATE <= " + end_date + " and STATUS = 2)";
            DB.Db2.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// COME_REPORT_OUTSIDE にデータをセットする
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="outside_done"></param>
        public static void DoneOutSide(string order_id, string outside_done)
        {
            if (order_id.Length == 0)
            {
                return;
            }

            string cmd = "delete from COME_REPORT_OUTSIDE where ORDER_ID = " + order_id;
            DB.Db2.ExecuteNonQuery(cmd);

            if (outside_done.Equals("1"))
            {
                System.Threading.Thread.Sleep(200);

                cmd = "insert into COME_REPORT_OUTSIDE (ORDER_ID, OUTSIDE_DONE) values (" + order_id + ", " + outside_done + ")";
                DB.Db2.ExecuteNonQuery(cmd);
            }
        }
    }
/*
    public class CrOrderDetail
    {
        public string OrderId = "";
        public string Code = "";
        public string Name = "";
        public double Qty = 0.0;
        public float Times = 0;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CrOrderDetail()
        {
            OrderId = "";
            Code = "";
            Name = "";
            Qty = 0.0;
            Times = 0;
        }


        static CrOrderDetail GetFromStdClass(StdClass tmp)
        {
            CrOrderDetail obj = new CrOrderDetail();
            obj.OrderId = tmp.GetDataString("ORDER_NO");
            obj.Code = tmp.GetDataString("ORDER_CODE").Trim();
            obj.Name = tmp.GetDataString("ORDER_COMMENT").Trim();
            obj.Qty = tmp.GetDataDouble("QTY");
            obj.Times = tmp.GetDataFloat("TIMES");
            return obj;
        }


        public static List<CrOrderDetail> GetListByOrderId(string order_id)
        {
            List<CrOrderDetail> list = new List<CrOrderDetail>();

            if (order_id.Length == 0)
            {
                return list;
            }
            string cmd = "select * from D_ORDER_DETAIL " +
                " where ORDER_NO = " + order_id +
                " order by DETAIL_SEQ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }
            return list;
        }
    }
 */
}
