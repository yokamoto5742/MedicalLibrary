using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class ComeReportData
    {
        /// <summary>
        /// オーダー番号
        /// </summary>
        public string OrderId = "";

        /// <summary>
        /// レポート番号
        /// </summary>
        public string ReportId = "";

        /// <summary>
        /// タブ名
        /// </summary>
        public string Tab = "";

        /// <summary>
        /// 所見
        /// </summary>
        public string Cont = "";

        /// <summary>
        /// 院外読影
        /// </summary>
        public string OutSide = "";

        /// <summary>
        /// 職員コード
        /// </summary>
        public string StaffCode = "";

        /// <summary>
        /// 職員名
        /// </summary>
        public string StaffName
        {
            get
            {
                if (Dict.StaffDict.ContainsKey(StaffCode))
                {
                    return Dict.StaffDict[StaffCode].Name;
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 作成ステータス
        /// 0 削除, 1 完成, 2 未完成, 8 PDF対象外（手動）, 9 PDF対象外（自動）
        /// </summary>
        public string Status = "";

        /// <summary>
        /// 作成日
        /// </summary>
        public string SaveDate = "";

        /// <summary>
        /// 作成時刻
        /// </summary>
        public string SaveTime = "";

        public string SaveDateTime
        {
            get
            {
                string s = "";

                if (this.SaveDate.Length == 8)
                {
                    s += DateTimeAgent.DateFormat(this.SaveDate, DateTimeAgent.DateFormatKind.LONG) + " ";
                }

                if (this.SaveTime.Length > 0)
                {
                    s += this.SaveTime.PadLeft(6, '0').Substring(0, 4).Insert(2, ":");
                }

                return s;
            }
        }

        /// <summary>
        /// 削除スタッフ
        /// </summary>
        public string DelStaff = "";

        /// <summary>
        /// PDF生成ステータス
        /// 0 未生成, 1 生成
        /// </summary>
        public string PDFStatus = "";

        // シェーマリスト
        public List<ComeReportSchema> SchemaList = new List<ComeReportSchema>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ComeReportData()
        {
            this.Init();
        }

        /// <summary>
        /// すべての値を初期化する
        /// </summary>
        public void Init()
        {
            this.OrderId = "0";
            this.ReportId = "0";
            this.Tab = "";
            this.Cont = "";
            this.OutSide = "0";
            this.StaffCode = "0";
            this.Status = "0";
            this.SaveDate = "";
            this.SaveTime = "";
            this.DelStaff = "0";
            this.PDFStatus = "0";

            if (this.SchemaList != null)
            {
                this.SchemaList.Clear();
            }
            else
            {
                this.SchemaList = new List<ComeReportSchema>();
            }
        }

        /// <summary>
        /// クローンを返す。
        /// </summary>
        /// <returns></returns>
        public ComeReportData Clone()
        {
            ComeReportData rp = new ComeReportData();

            rp.OrderId = this.OrderId;
            rp.ReportId = this.ReportId;
            rp.Cont = this.Cont;
            rp.OutSide = this.OutSide;
            rp.StaffCode = this.StaffCode;
            rp.Status = this.Status;
            rp.SaveDate = this.SaveDate;
            rp.SaveTime = this.SaveTime;
            rp.DelStaff = this.DelStaff;
            rp.PDFStatus = this.PDFStatus;

            for (int i = 0; i < this.SchemaList.Count; i++)
            {
                rp.SchemaList.Add(this.SchemaList[i].Clone());
            }

            return rp;
        }


        public static ComeReportData GetFromStdClass(StdClass tmp)
        {
            ComeReportData obj = new ComeReportData();

            obj.OrderId = tmp.GetDataString("ORDER_ID");
            obj.ReportId = tmp.GetDataString("REPORT_ID");
            obj.Tab = tmp.GetDataString("TAB");
            obj.Cont = tmp.GetDataString("CONT");
            obj.OutSide = tmp.GetDataString("OUTSIDE");
            obj.StaffCode = tmp.GetDataString("STAFF");
            obj.SaveDate = tmp.GetDataString("SAVE_DATE");
            obj.SaveTime = tmp.GetDataString("SAVE_TIME");
            obj.Status = tmp.GetDataString("STATUS");
            obj.DelStaff = tmp.GetDataString("DEL_SATAFF");
            obj.PDFStatus = tmp.GetDataString("PDF_SAVE");

            return obj;
        }


        public static int GetMaxByOrderId(string order_id)
        {
            int i = 0;

            string cmd = "select Max(REPORT_ID) MAX_ID from COME_REPORT " +
                " where ORDER_ID = " + order_id;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                if (!tmp.GetDataInt("MAX_ID").Equals(-1))
                {
                    i = tmp.GetDataInt("MAX_ID");
                }

                break;
            }

            return i;
        }


        /// <summary>
        /// 指定されたオーダー番号のレポートを抽出する
        /// （削除されたものは除く）
        /// </summary>
        /// <param name="order_ids"></param>
        /// <returns></returns>
        public static List<ComeReportData> GetListByOrderIds(List<string> order_ids)
        {
            List<ComeReportData> list = new List<ComeReportData>();

            string orders = "";

            foreach (string s in order_ids)
            {
                if (orders.Length > 0)
                {
                    orders += ",";
                }

                orders += s;
            }

            if (orders.Length == 0)
            {
                return list;
            }

            string cmd = "select * from COME_REPORT " +
                " where ORDER_ID in (" + orders + ")" +
                 " and (STATUS is not NULL and STATUS != 0) " +
                 " order by ORDER_ID, REPORT_ID";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            // CrReportSchema を取得する
            List<CrReportSchema> list2 = CrReportSchema.GetListByOrderIds(order_ids);

            foreach (CrReportSchema rs in list2)
            {
                foreach (ComeReportData r in list)
                {
                    // オーダー番号またはレポート番号が異なる場合は飛ばす
                    if (!rs.OrderId.Equals(r.OrderId) || !rs.ReportId.Equals(r.ReportId))
                    {
                        continue;
                    }

                    ComeReportSchema s = new ComeReportSchema();

                    if (rs.SchemaBg.Length > 0)
                    {
                        s.BgId = rs.SchemaBg;
                    }
                    else if (rs.SchemaFile.Length > 20)
                    {
                        string serverFile = ComeReportSettings.Current.ImgHostPath + "\\" + rs.SchemaFile + ".jpg";
                        string clientFile = System.Environment.GetEnvironmentVariable("TEMP") + "\\" + rs.SchemaFile + ".jpg";

                        if (System.IO.File.Exists(serverFile))
                        {
                            if (!System.IO.File.Exists(clientFile))
                            {
                                System.IO.File.Copy(serverFile, clientFile, true);
                            }

                            s.BgImagePath = clientFile;
                        }
                    }

                    // シェーマアイテム文字列を分解してアイテムを生成する
                    s.MakeSchemaItemListFromText(rs.SchemaItem);

                    r.SchemaList.Add(s);
                }
            }

            return list;
        }


/*
        /// <summary>
        /// 所見上で「読影依頼」されたレポートを抽出する
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="sekou_list"></param>
        /// <returns></returns>
        public static List<CrReport> GetOutSideList(string date1, string date2, string[] sekou_list)
        {
            List<CrReport> list = new List<CrReport>();

            string sekous = "";

            foreach (string s in sekou_list)
            {
                if (sekous.Length > 0)
                {
                    sekous += ",";
                }

                sekous += s;
            }

            if (date1.Length != 8 || date2.Length != 8 || sekous.Length == 0)
            {
                return list;
            }

            string cmd = "select * from COME_REPORT " +
                " where ORDER_ID in " +
                " (select ORDER_ID from COME_REPORT inner join ＮＴオーダーヘッダー" + Env.DB_LINK + " on ORDER_ID = オーダー番号 " +
                "  where (STATUS = 8 or STATUS = 9) and 施行部署１ in (" + sekous + ") and 施行予定日 >= " + date1 + " and 施行予定日 <= " + date2 + ") " +
                " order by ORDER_ID, REPORT_ID";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }
*/
        /// <summary>
        /// 新規タブを作る（中身は空）
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="report_id"></param>
        /// <param name="tab_name"></param>
        public static void TabInsert(string order_id, string report_id, string tab_name)
        {
            if (order_id.Length == 0 || report_id.Length == 0 || tab_name.Length == 0)
            {
                return;
            }

            string cmd = "insert into COME_REPORT " +
                " (ORDER_ID, REPORT_ID, TAB, OUTSIDE, STATUS) " +
                " values " +
                " (" + order_id + ", " + report_id + ", '" + tab_name + "', 0, 2)";

            DB.Db2.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// タブの中身を保存する
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public static StdReturn TabSave(ComeReportData report)
        {
            StdReturn sr = new StdReturn();
            string cmd = "";

            StdDbClass obj = new StdDbClass();
            obj.Db = DB.Db2;

            obj.Table = "COME_REPORT";

            obj.DataList.Add(new StdDbColumn("ORDER_ID", StdDbType.NUMBER, report.OrderId));
            obj.DataList.Add(new StdDbColumn("REPORT_ID", StdDbType.NUMBER, report.ReportId));

            obj.DataList.Add(new StdDbColumn("TAB", StdDbType.VARCHAR2, report.Tab));
            obj.DataList.Add(new StdDbColumn("CONT", StdDbType.VARCHAR2, report.Cont));
            obj.DataList.Add(new StdDbColumn("OUTSIDE", StdDbType.NUMBER, report.OutSide));

            obj.DataList.Add(new StdDbColumn("STAFF", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("SAVE_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("SAVE_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, report.Status));

            obj.WhereList.Add("ORDER_ID = " + report.OrderId);
            obj.WhereList.Add("REPORT_ID = " + report.ReportId);

            sr = obj.UpdateSQL();

            // 古いシェーマがあれば削除する
            if (report.OrderId.Length > 0 && report.ReportId.Length > 0)
            {
                cmd = "delete from COME_REPORT_SCHEMA " +
                    " where ORDER_ID = " + report.OrderId + " and REPORT_ID = " + report.ReportId;

                DB.Db2.ExecuteNonQuery(cmd);
            }

            // 新しいシェーマを登録する
            for (int i = 0; i < report.SchemaList.Count; i++)
            {
                if (report.SchemaList[i].BgId.Length > 0)
                {
                    CrReportSchema.Insert(report.OrderId, report.ReportId, (i + 1).ToString(), report.SchemaList[i].BgId, "", report.SchemaList[i].GetText());
                }
                else if (report.SchemaList[i].BgImagePath.Length > 0)
                {
                    string[] s = report.SchemaList[i].BgImagePath.Split('\\');
                    string file = s[s.Length - 1].Substring(0, s[s.Length - 1].Length - 4);

                    CrReportSchema.Insert(report.OrderId, report.ReportId, (i + 1).ToString(), "", file, report.SchemaList[i].GetText());

                    string srcFile = System.Environment.GetEnvironmentVariable("TEMP") + "\\" + file + ".jpg";
                    string tarFile = ComeReportSettings.Current.ImgHostPath + "\\" + file + ".jpg";

                    if (System.IO.File.Exists(srcFile) && !System.IO.File.Exists(tarFile))
                    {
                        System.IO.File.Copy(srcFile, tarFile, true);
                    }
                }
            }

            return sr;
        }

        /// <summary>
        /// タブ名の変更
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="report_id"></param>
        /// <param name="tab_name"></param>
        public static void TabRename(string order_id, string report_id, string tab_name)
        {
            if (order_id.Length == 0 || report_id.Length == 0 || tab_name.Length == 0)
            {
                return;
            }

            string cmd = "update COME_REPORT set TAB = '" + tab_name + "'" +
                " where ORDER_ID = " + order_id + "and REPORT_ID = " + report_id;

            DB.Db2.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// タブ削除
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="report_id"></param>
        public static void TabDelete(string order_id, string report_id)
        {
            if (order_id.Length == 0 || report_id.Length == 0)
            {
                return;
            }

            string cmd = "update COME_REPORT set STATUS = 0, DEL_STAFF = " + LoginUser.Id + ", DEL_DATE = " + DateTime.Now.ToString("yyyyMMdd") + ", DEL_TIME = " + DateTime.Now.ToString("HHmmss") +
                " where ORDER_ID = " + order_id + "and REPORT_ID = " + report_id;

            DB.Db2.ExecuteNonQuery(cmd);
        }
/*
        /// <summary>
        /// 院外読影にする。（手動。メッセージ送信とDB変更）
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="fromStaff"></param>
        public static void ReportOutSide(string orderId, string fromStaff)
        {
            // 作成途中のものは作成者にメッセージを送る。（読影依頼に出す検査のみ）
            string cmd = "select 患者コード, 施行予定日, 施行部署１, TAB, CONT, STAFF " +
                " from COME_REPORT inner join ＮＴオーダーヘッダー" + Env.DB_LINK + " on ORDER_ID = オーダー番号 " +
                " where ORDER_ID = " + orderId + " and オーダー番号 = " + orderId + " and STATUS = 2 and STAFF is not NULL";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            string msg = "";

            foreach (StdClass tmp in tmp_list)
            {
                msg = CrDict.ComeReportSet.Tables["PDFMaster"].Rows[0]["PreMsg"].ToString() + "\r\n\r\n";

                if (Dict.SekouDict.ContainsKey(tmp.GetDataString("施行部署１")))
                {
                    msg += Dict.SekouDict[tmp.GetDataString("施行部署１")].FullName + "　";
                }

                msg += tmp.GetDataString("施行予定日").Insert(4, "/").Insert(7, "/") + " 施行\r\n\r\n";
                msg += "【" + tmp.GetDataString("TAB") + "】\r\n" + tmp.GetDataString("CONT");

                KarteMessage km = new KarteMessage();
                km.FromCode = fromStaff;
                km.ToCode = tmp.GetDataString("STAFF");
                km.Title = CrDict.ComeReportSet.Tables["PDFMaster"].Rows[0]["Title"].ToString();
                km.Msg = msg;
                km.PtId = tmp.GetDataString("患者コード");
                km.Priority = "2";

                km.Send();
                System.Threading.Thread.Sleep(2000);
            }

            // 指定されたオーダー番号のものを院外にする。
            // （手動なので施行部署や完成・未完成は関係なくすべて）
            cmd = "update COME_REPORT set OUTSIDE = 1 where ORDER_ID = " + orderId;
            DB.Db2.ExecuteNonQuery(cmd);

            // 指定されたオーダー番号のもので未完成（STATUS = 2）のものはPDF対象外（STATUS = 8）にする。
            cmd = "update COME_REPORT set STATUS = 8 where ORDER_ID = " + orderId + " and STATUS = 2";
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

            foreach (string s in CrDict.ComeReportSet.Tables["OutSide"].Rows[0]["Kensa"].ToString().Split(','))
            {
                if (sekou_codes.Length > 0)
                {
                    sekou_codes += ",";
                }

                sekou_codes += s;
            }

            if (sekou_codes.Length > 0)
            {
                sekou_sql = " and 施行部署１ in (" + sekou_codes + ")";
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
            cmd = "select 患者コード, 施行予定日, 施行部署１, TAB, CONT, STAFF " +
                " from COME_REPORT inner join ＮＴオーダーヘッダー" + Env.DB_LINK + " on ORDER_ID = オーダー番号 " +
                " where ORDER_ID in (" + order_ids + ") and オーダー番号 in (" + order_ids + ") and 施行予定日 <= " + end_date + " and STATUS = 2 and STAFF is not NULL" + sekou_sql;

            tmp_list = StdClass.GetList(DB.Db2, cmd);

            string msg = "";

            foreach (StdClass tmp in tmp_list)
            {
                msg = CrDict.ComeReportSet.Tables["PDFMaster"].Rows[0]["PreMsg"].ToString() + "\r\n\r\n";

                if (Dict.SekouDict.ContainsKey(tmp.GetDataString("施行部署１")))
                {
                    msg += Dict.SekouDict[tmp.GetDataString("施行部署１")].FullName + "　";
                }

                msg += tmp.GetDataString("施行予定日").Insert(4, "/").Insert(7, "/") + " 施行\r\n\r\n";
                msg += "【" + tmp.GetDataString("TAB") + "】\r\n" + tmp.GetDataString("CONT");

                KarteMessage km = new KarteMessage();
                km.FromCode = fromStaff;
                km.ToCode = tmp.GetDataString("STAFF");
                km.Title = CrDict.ComeReportSet.Tables["PDFMaster"].Rows[0]["Title"].ToString();
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
                " (select ORDER_ID, REPORT_ID from COME_REPORT inner join ＮＴオーダーヘッダー" + Env.DB_LINK + " on ORDER_ID = オーダー番号 " +
                "  where ORDER_ID in (" + order_ids + ") and オーダー番号 in (" + order_ids + ") and 施行予定日 <= " + end_date + " and STATUS = 2 " + sekou_sql + ")";

            DB.Db2.ExecuteNonQuery(cmd);

            // 指定期間を過ぎても未完成（STATUS = 2）のもので
            // 読影依頼に出さない検査（＝指定外の施行部署コードの検査）はPDF対象外（STATUS = 9）にする。
            cmd = "update COME_REPORT set STATUS = 9 " +
                " where (ORDER_ID, REPORT_ID) in " +
                " (select ORDER_ID, REPORT_ID from COME_REPORT inner join ＮＴオーダーヘッダー" + Env.DB_LINK + " on ORDER_ID = オーダー番号 " +
                "  where ORDER_ID in (" + order_ids + ") and オーダー番号 in (" + order_ids + ") and 施行予定日 <= " + end_date + " and STATUS = 2)";

            DB.Db2.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// COME_REPORT_OUTSIDE にデータをセットする
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="outside_done"></param>
        public static void DoneOutSide(string order_id, string outside_done)
        {
            string cmd = "delete from COME_REPORT_OUTSIDE where ORDER_ID = " + order_id;
            DB.Db2.ExecuteNonQuery(cmd);

            System.Threading.Thread.Sleep(200);

            cmd = "insert into COME_REPORT_OUTSIDE (ORDER_ID, OUTSIDE_DONE) values (" + order_id + ", " + outside_done + ")";
            DB.Db2.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// PDF生成対象のオーダー番号のリストを取得する
        /// </summary>
        /// <returns></returns>
        public static List<string> GetOrderIdsToPDF()
        {
            List<string> list = new List<string>();

            // 完成している所見があるものを取得する
            string cmd = "select distinct ORDER_ID from COME_REPORT where STATUS = 1 and (PDF_SAVE is NULL or PDF_SAVE = 0)";
            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                if (!list.Contains(tmp.GetDataString("ORDER_ID")))
                {
                    list.Add(tmp.GetDataString("ORDER_ID"));
                }
            }

            // 未完成の所見があれば除外する
            cmd = "select distinct ORDER_ID from COME_REPORT where STATUS = 2";
            tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                if (list.Contains(tmp.GetDataString("ORDER_ID")))
                {
                    list.Remove(tmp.GetDataString("ORDER_ID"));
                }
            }

            return list;
        }

        /// <summary>
        /// 完成された所見のPDFステータスを 1 にする
        /// </summary>
        /// <param name="order_id"></param>
        public static void DonePDF(string order_id)
        {
            string cmd = "update COME_REPORT set PDF_SAVE = 1, PDF_DATE = " + DateTime.Now.ToString("yyyyMMdd") + ", PDF_TIME = " + DateTime.Now.ToString("HHmmss") +
                " where ORDER_ID = " + order_id + " and STATUS = 1";

            DB.Db2.ExecuteNonQuery(cmd);
        }
 */
    }

    public class CrReportSchema
    {
        /// <summary>
        /// オーダー番号
        /// </summary>
        public string OrderId = "";

        /// <summary>
        /// レポート番号
        /// </summary>
        public string ReportId = "";

        /// <summary>
        /// シェーマ番号
        /// </summary>
        public string SchemaId = "";

        /// <summary>
        /// シェーマ背景
        /// </summary>
        public string SchemaBg = "";

        /// <summary>
        /// シェーマファイル
        /// </summary>
        public string SchemaFile = "";

        /// <summary>
        /// シェーマアイテム
        /// </summary>
        public string SchemaItem = "";


        public static List<CrReportSchema> GetListByOrderIds(List<string> order_ids)
        {
            List<CrReportSchema> list = new List<CrReportSchema>();

            string orders = "";

            foreach (string s in order_ids)
            {
                if (orders.Length > 0)
                {
                    orders += ",";
                }

                orders += s;
            }

            if (orders.Length == 0)
            {
                return list;
            }

            string cmd = "select * from COME_REPORT_SCHEMA " +
                " where ORDER_ID in (" + orders + ")" +
                " order by ORDER_ID, REPORT_ID";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                CrReportSchema obj = new CrReportSchema();

                obj.OrderId = tmp.GetDataString("ORDER_ID");
                obj.ReportId = tmp.GetDataString("REPORT_ID");
                obj.SchemaId = tmp.GetDataString("SCHEMA_ID");
                obj.SchemaBg = tmp.GetDataString("SCHEMA_BG");
                obj.SchemaFile = tmp.GetDataString("SCHEMA_FILE");
                obj.SchemaItem = tmp.GetDataString("SCHEMA_ITEM");

                list.Add(obj);
            }

            return list;
        }


        public static StdReturn Insert(string order_id, string report_id, string schema_id, string schema_bg, string schema_file, string schema_item)
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
            obj.Db = DB.Db2;

            obj.Table = "COME_REPORT_SCHEMA";

            obj.DataList.Add(new StdDbColumn("ORDER_ID", StdDbType.NUMBER, order_id));
            obj.DataList.Add(new StdDbColumn("REPORT_ID", StdDbType.NUMBER, report_id));

            obj.DataList.Add(new StdDbColumn("SCHEMA_ID", StdDbType.NUMBER, schema_id));
            obj.DataList.Add(new StdDbColumn("SCHEMA_BG", StdDbType.NUMBER, schema_bg));

            obj.DataList.Add(new StdDbColumn("SCHEMA_FILE", StdDbType.CHAR, schema_file));
            obj.DataList.Add(new StdDbColumn("SCHEMA_ITEM", StdDbType.VARCHAR2, schema_item));

            sr = obj.InsertSQL();

            return sr;
        }

        public static void Delete(string order_id, string report_id)
        {
            if (order_id.Length == 0 || report_id.Length == 0)
            {
                return;
            }

            string cmd = "delete from COME_REPORT_SCHEMA " +
                " where ORDER_ID = " + order_id + "and REPORT_ID = " + report_id;

            DB.Db2.ExecuteNonQuery(cmd);

        }
    }

    public class ReportAchieve
    {
        public string StaffCode = "";

        public string StaffName
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(this.StaffCode))
                {
                    s = Dict.StaffDict[this.StaffCode].Name;
                }

                return s;
            }
        }

        public int QualCode
        {
            get
            {
                int i = -1;

                if (Dict.StaffDict.ContainsKey(this.StaffCode))
                {
                    i = Dict.StaffDict[this.StaffCode].QualCode;
                }

                return i;
            }
        }

        public string SekouCode = "";

        public string SekouName
        {
            get
            {
                string s = "";

                if (Dict.SekouDict.ContainsKey(this.SekouCode))
                {
                    s = Dict.SekouDict[this.SekouCode].ShortName;
                }

                return s;
            }
        }

        public string Tab = "";

        public int Count = 0;


        public static List<ReportAchieve> GetList1(string date1, string date2, string[] sekou_list)
        {
            List<ReportAchieve> list = new List<ReportAchieve>();

            if (date1.Length != 8 || date2.Length != 8 || AppString.ConcatList(sekou_list, ",").Length == 0)
            {
                return list;
            }
#if INNO
            string cmd = "select STAFF, SEKOU_CODE 施行部署１, count(*) CNT from " +
                " (select distinct ORDER_NO, STAFF, SEKOU_CODE " +
                " from COME_REPORT inner join D_ORDER_HEADER" + Env.DB_LINK + " on ORDER_ID = ORDER_NO " +
                " where ORDER_DATE >= " + date1 + " and ORDER_DATE <= " + date2 + " and SEKOU_CODE in (" + AppString.ConcatList(sekou_list, ",") + ") and STATUS = 1 and STAFF is not NULL) " +
                " group by STAFF, SEKOU_CODE";
#else
            string cmd = "select STAFF, 施行部署１, count(*) CNT from " +
                " (select distinct オーダー番号, STAFF, 施行部署１ " +
                " from COME_REPORT inner join ＮＴオーダーヘッダー" + Env.DB_LINK + " on ORDER_ID = オーダー番号 " +
                " where 施行予定日 >= " + date1 + " and 施行予定日 <= " + date2 + " and 施行部署１ in (" + AppString.ConcatList(sekou_list, ",") + ") and STATUS = 1 and STAFF is not NULL) " +
                " group by STAFF, 施行部署１";
#endif
            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                ReportAchieve obj = new ReportAchieve();

                obj.StaffCode = tmp.GetDataString("STAFF");
                obj.SekouCode = tmp.GetDataString("施行部署１");
                obj.Count = tmp.GetDataInt("CNT");

                list.Add(obj);
            }

            return list;
        }


        public static List<ReportAchieve> GetList2(string date1, string date2, string[] sekou_list, string staff)
        {
            List<ReportAchieve> list = new List<ReportAchieve>();

            if (date1.Length != 8 || date2.Length != 8 || AppString.ConcatList(sekou_list, ",").Length == 0)
            {
                return list;
            }

            if (staff.Length == 0)
            {
                return list;
            }

            string cmd = "select t2.SEKOU_CODE, t1.TAB, count(*) CNT " +
                " from COME_REPORT t1 inner join D_ORDER_HEADER" + Env.DB_LINK + " t2 on ORDER_ID = ORDER_NO " +
                " where ORDER_DATE >= " + date1 + " and ORDER_DATE <= " + date2 +
                " and SEKOU_CODE in (" + AppString.ConcatList(sekou_list, ",") + ") " +
                " and STATUS = 1 and STAFF is not NULL " +
                " and STAFF = " + staff +
                " group by t2.SEKOU_CODE, t1.TAB" +
                " order by t2.SEKOU_CODE, t1.TAB";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                ReportAchieve obj = new ReportAchieve();

                obj.SekouCode = tmp.GetDataString("SEKOU_CODE");
                obj.Tab = tmp.GetDataString("TAB");
                obj.Count = tmp.GetDataInt("CNT");

                list.Add(obj);
            }

            return list;
        }
    }
}
