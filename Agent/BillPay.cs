using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Utility;
using MedicalLibrary.Entity;

namespace MedicalLibrary.Agent
{
    public class BillPay
    {
        public string ArDate = "";

        public string ArTime = "";

        /// <summary>
        /// 1: 本館総合, 2: 南館内科, 99: 南館救急
        /// </summary>
        public string ArPlace = "";

        /// <summary>
        /// 待ち時間（分）
        /// </summary>
        public int WaitMinutes
        {
            get
            {
                int i = 0;

                DateTime dt = DateTime.Now;

                // Status = 8 or 9 の場合は SaveDate + SaveTime
                if (this.Status.Equals(8) || this.Status.Equals(9))
                {
                    DateTime.TryParse(DateTimeAgent.DateFormat(this.SaveDate, DateTimeAgent.DateFormatKind.LONG) + " " + DateTimeAgent.TimeFormat6(this.SaveTime, 6, true), out dt);
                }

                DateTime d1 = DateTime.Now;

                if (DateTime.TryParse(DateTimeAgent.DateFormat(this.ArDate, DateTimeAgent.DateFormatKind.LONG) + " " + DateTimeAgent.TimeFormat6(this.ArTime, 6, true), out d1))
                {
                    i = (int)dt.Subtract(d1).TotalMinutes;
                }

                return i;
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

        /// <summary>
        /// 明細書フラグ
        /// true: 印字する, false: 印字しない
        /// </summary>
        public bool InvoiceFlg = true;

        /// <summary>
        /// 過去の未収金の有無
        /// 1: 未収あり
        /// </summary>
        public int YetPays = 0;

        public string PcName = "";

        public string PcAddr = "";

        public string SaveDate = "";

        public string SaveTime = "";

        public string SaveDateTime
        {
            get
            {
                return DateTimeAgent.DateFormat(this.SaveDate, DateTimeAgent.DateFormatKind.LONG) + " " + DateTimeAgent.TimeFormat6(this.SaveTime, 6, true);
            }
        }

        /// <summary>
        /// ステータス
        /// 0: 不定（通常は存在しない）, 1: 通常, 2: 保留, 8: 手動終了, 9: 自動終了（入金済）
        /// </summary>
        public int Status = 0;


        public List<PatOut> PatOutList = new List<PatOut>();

        /// <summary>
        /// 受付番号
        /// </summary>
        public string Seq1
        {
            get
            {
                List<string> list = new List<string>();

                foreach (PatOut obj in this.PatOutList)
                {
                    if (!list.Contains(obj.Seq1))
                    {
                        list.Add(obj.Seq1);
                    }
                }

                // 降順にソート
                list.Sort((x, y) =>
                    {
                        return y.CompareTo(x);
                    }
                );

                return AppString.ConcatList(list, ", ");
            }
        }

        public string DeptNames
        {
            get
            {
                List<string> list = new List<string>();

                foreach (PatOut obj in this.PatOutList)
                {
                    if (!list.Contains(obj.DeptName))
                    {
                        list.Add(obj.DeptName);
                    }
                }

                return AppString.ConcatList(list, " ");
            }
        }

        public string InsNames
        {
            get
            {
                List<string> list = new List<string>();

                foreach (PatOut obj in this.PatOutList)
                {
                    if (!list.Contains(obj.InsKindName))
                    {
                        list.Add(obj.InsKindName);
                    }
                }

                return AppString.ConcatList(list, " ");
            }
        }

        /// <summary>
        /// 要注意の保険種別
        /// 87:自費（自賠責）Ｊ自, 88:自費（労災）Ｒ自, 89:自費（第三者行為）三自, 90:公費単独, 91:自賠, 92:労災, 93:公害, 95:公災
        /// </summary>
        public string MarkInsNames
        {
            get
            {
                List<string> list = new List<string>();

                List<string> mark_list = new List<string>();
                mark_list.Add("87");
                mark_list.Add("88");
                mark_list.Add("89");
                mark_list.Add("90");
                mark_list.Add("91");
                mark_list.Add("92");
                mark_list.Add("93");
                mark_list.Add("95");

                foreach (PatOut obj in this.PatOutList)
                {
                    if (!mark_list.Contains(obj.InsKind))
                    {
                        continue;
                    }

                    if (!list.Contains(obj.InsKindName))
                    {
                        list.Add(obj.InsKindName);
                    }
                }

                return AppString.ConcatList(list, " ");
            }
        }

        /// <summary>
        /// 会計入力が終了していない科
        /// </summary>
        public string KaikeiYetDeptNames
        {
            get
            {
                List<string> list = new List<string>();

                foreach (PatOut obj in this.PatOutList)
                {
                    // 会計入力されている科は飛ばす
                    if (obj.TimeString5.Length > 0)
                    {
                        continue;

                    }

                    if (!list.Contains(obj.DeptName))
                    {
                        list.Add(obj.DeptName);
                    }
                }

                return AppString.ConcatList(list, " ");
            }
        }

        public List<Bill> BillList = new List<Bill>();

        /// <summary>
        /// 印刷済の請求書リスト
        /// </summary>
        public List<Bill> BillPrintDoneList
        {
            get
            {
                return this.BillList.FindAll((Bill obj) => {
                    return obj.Status.Equals(2);
                });
            }
        }

        /// <summary>
        /// 未印刷の請求書リスト
        /// </summary>
        public List<Bill> BillPrintYetList
        {
            get
            {
                return this.BillList.FindAll((Bill obj) =>
                {
                    return obj.Status.Equals(1);
                });
            }
        }

        public string BillStatus
        {
            get
            {
                string s = "";

                if (this.BillList.Count > 0)
                {
                    s = "印刷 " + this.BillPrintDoneList.Count + "/" + this.BillList.Count;
                }

                return s;
            }
        }

        public List<PosDemand> PosDemandList = new List<PosDemand>();

        /// <summary>
        /// 未収情報
        /// </summary>
        public int PayYetMoney
        {
            get
            {
                int sum = 0;

                foreach (PosDemand obj in this.PosDemandList)
                {
                    // 完納なら飛ばす
                    if (obj.OkFlg.Equals(1))
                    {
                        continue;
                    }

                    sum += obj.YetMoney;
                }

                return sum;
            }
        }

        /// <summary>
        /// 昨日までの未収情報
        /// </summary>
        public int PayYetMoneyYesterday
        {
            get
            {
                int sum = 0;
                string today = DateTime.Now.ToString("yyyyMMdd");

                foreach (PosDemand obj in this.PosDemandList)
                {
                    // 完納なら飛ばす
                    if (obj.OkFlg.Equals(1))
                    {
                        continue;
                    }

                    // 請求期間開始日が本日以降なら飛ばす
                    if (obj.DemStartDate.CompareTo(today) >= 0)
                    {
                        continue;
                    }

                    sum += obj.YetMoney;
                }

                return sum;
            }
        }

        /// <summary>
        /// 本日の請求額
        /// </summary>
        public int BillMoneyToday
        {
            get
            {
                int sum = 0;
                string today = DateTime.Now.ToString("yyyyMMdd");

                foreach (PosDemand obj in this.PosDemandList)
                {
                    // 本日でなければ飛ばす
                    if (!obj.DemDate.Equals(today))
                    {
                        continue;
                    }

                    sum += obj.BillMoney;
                }

                return sum;
            }
        }

        public List<PosRegHistoryDetail> PosRegHistoryList = new List<PosRegHistoryDetail>();

        /// <summary>
        /// 備考リスト
        /// </summary>
        public List<string> CommentList = new List<string>();

        /// <summary>
        /// 備考リストを文字列で返す
        /// </summary>
        public string Comment
        {
            get
            {
                // 患者備考があれば追加
                if (this.Pat.Note1.Short.Length > 0)
                {
                    this.CommentList.Add(this.Pat.Note1.Short);
                }

                // 明細不要ならば追加
                if (!this.InvoiceFlg)
                {
                    this.CommentList.Add("明細不要");
                }

                return AppString.ConcatList(this.CommentList, ", ");
            }
        }

        /// <summary>
        /// 各種データ取得後のステータス
        /// </summary>
        public int Status2
        {
            get
            {
                int i = this.Status;

                // 入金されていれば終了する
                // ただし Status = 0 の場合は、いきなり終了しない
                if (i.Equals(1) || i.Equals(2))
                {
                    foreach (PosRegHistoryDetail obj in this.PosRegHistoryList)
                    {
                        // 到着時刻より入金時刻の方が後の場合
                        if (this.ArTime.PadLeft(6, '0').CompareTo(obj.ATime) < 0)
                        {
                            i = 9;
                            break;
                        }
                    }
                }

                // 未完了の場合
                if (i.Equals(0) || i.Equals(1) || i.Equals(2))
                {
                    // いったん Staus = 1 とする
                    i = 1;

                    // 患者備考
                    if (this.Pat.NoteCode.Equals("2") ||
                        this.Pat.NoteCode.Equals("3"))
                    {
                        // 2: 口座引落, 3: 透析
                        i = 2;
                    }
					else if (this.PayYetMoneyYesterday != 0)
					{
						// 過去未収ありの場合は Status = 2
						i = 2;
					}
					else
					{
						// 当日救急を受信している場合
						foreach (PatOut obj in this.PatOutList)
						{
							if (obj.ModeName.Contains("救急"))
							{
								i = 2;
								break;
							}
						}
					}
                }

                return i;
            }
        }



        public static BillPay GetFromStdClass(StdClass tmp)
        {
            BillPay obj = new BillPay();

            obj.ArDate = tmp.GetDataString("AR_DATE");
            obj.ArTime = tmp.GetDataString("AR_TIME");
            obj.ArPlace = tmp.GetDataString("AR_PLACE");
            obj.YetPays = tmp.GetDataInt("YET_PAYS");
            obj.PcName = tmp.GetDataString("PC_NAME");
            obj.PcAddr = tmp.GetDataString("PC_ADDR");
            obj.SaveDate = tmp.GetDataString("SAVE_DATE");
            obj.SaveTime = tmp.GetDataString("SAVE_TIME");
            obj.Status = tmp.GetDataInt("STATUS");

            obj.PtId = tmp.GetDataString("PATIENT_ID");
#if INNO
            obj._Pat.Id = tmp.GetDataString("PATIENT_ID");
            obj._Pat.Name = tmp.GetDataString("P_NAME").Trim();
            obj._Pat.Kana = tmp.GetDataString("P_KANA").Trim();
            obj._Pat.Sex = tmp.GetDataString("P_SEX");
            obj._Pat.Birth = tmp.GetDataString("P_BIRTHDAY_AD");
            obj._Pat.FacilityCode = tmp.GetDataString("PROPERTY_3");
            obj._Pat.NoteCode = tmp.GetDataString("PROPERTY_4");
            obj.InvoiceFlg = !tmp.GetDataInt("PROPERTY_6", 0).Equals(1);
#else
            obj._Pat.Id = tmp.GetDataString("PATIENT_ID");
            obj._Pat.Name = tmp.GetDataString("IM01RC_F04").Trim();
            obj._Pat.Kana = tmp.GetDataString("IM01RC_F03").Trim();
            obj._Pat.Sex = tmp.GetDataString("IM01RC_F05");
            obj._Pat.Birth = tmp.GetDataString("IM01RC_F10");
            obj._Pat.FacilityCode = tmp.GetDataString("IM01RC_F13_3");
            obj._Pat.NoteCode = tmp.GetDataString("IM01RC_F13_4");
            obj.InvoiceFlg = !tmp.GetDataInt("IM0101RC_F02", 0).Equals(1);
#endif

            return obj;
        }


        /// <summary>
        /// リストに表示する対象者を取得する（総合用）
        /// </summary>
        /// <param name="date">日付</param>
        /// <param name="place">1: 本館総合, 2: 南館内科, 99: 南館救急</param>
        /// <param name="live_minutes">正: 終了後 live_minutes 分以内, 負: すべて, 0: 終了者は対象外</param>
        /// <returns></returns>
        public static List<BillPay> GetLiveListByDate(string date, string place = "", int live_minutes = 30)
        {
            List<BillPay> list = new List<BillPay>();

            if (!DateTimeAgent.IsDate(date))
            {
                return list;
            }

            string cmd = "";

#if INNO
            cmd = "select tp.*, tm.* " +
                " from BILL_PAY tp, M_PATIENT" + Env.DB_LINK + " tm " +
                " where tp.AR_DATE = " + date +
                " and tp.PATIENT_ID = tm.P_ID";
#else
            cmd = "select tp.*, tm.*, tmm.* " +
                " from BILL_PAY tp, IM01RC" + Env.DB_LINK + " tm, IM0101RC" + Env.DB_LINK + " tmm " +
                " where tp.AR_DATE = " + date +
                " and tp.PATIENT_ID = tm.IM01RC_F01" +
                " and tp.PATIENT_ID = tmm.IM0101RC_F01(+)";
#endif

            if (place.Length > 0)
            {
                cmd += " and tp.AR_PLACE = " + place;
            }

            if (live_minutes == 0)
            {
                cmd += " and tp.STATUS in (0, 1, 2)";
            }

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            string today = DateTime.Now.ToString("yyyyMMdd");
            string time = "";

            if (live_minutes > 0)
            {
                // ★深夜0時少し過ぎの場合は、前日夜の時間帯になってしまう場合もあるので
                // 夜間は使わない前提とする
                time = DateTime.Now.AddMinutes(0 - live_minutes).ToString("HHmmss");
            }

            // リスト対象になるかどうか
            bool b = false;

            foreach (StdClass tmp in tmp_list)
            {
                b = false;

                BillPay obj = GetFromStdClass(tmp);

                if (live_minutes > 0)
                {
                    // Status in (8,9) の場合は、本日の live_minutes 分以内に save されたデータのみ
                    if (obj.Status.Equals(8) || obj.Status.Equals(9))
                    {
                        if (obj.SaveDate.Equals(today) &&
                            obj.SaveTime.PadLeft(6, '0').CompareTo(time) > 0)
                        {
                            b = true;
                        }
                    }
                    else
                    {
                        b = true;
                    }
                }
                else if (live_minutes == 0)
                {
                    // Status in (8,9) の場合は、対象外
                    if (obj.Status.Equals(8) || obj.Status.Equals(9))
                    {
                        b = false;
                    }
                    else
                    {
                        b = true;
                    }
                }
                else if (live_minutes < 0)
                {
                    // すべて対象
                    b = true;
                }

                if (b)
                {
                    list.Add(obj);
                }
            }

            return list;
        }


        /// <summary>
        /// リストに表示する対象者を取得する（未終了のみ・外来一覧用）
        /// </summary>
        /// <param name="date">日付</param>
        /// <param name="place">1: 本館総合, 2: 南館内科, 99: 南館救急</param>
        /// <returns></returns>
        public static List<BillPay> GetLiveListByDate(string date, string place = "")
        {
            List<BillPay> list = new List<BillPay>();

            if (!DateTimeAgent.IsDate(date))
            {
                return list;
            }

            string cmd = "";

#if INNO
            cmd = "select tp.*, tm.* " +
                " from BILL_PAY tp, M_PATIENT" + Env.DB_LINK + " tm " +
                " where tp.AR_DATE = " + date +
                " and tp.STATUS in (0, 1, 2)" +
                " and tp.PATIENT_ID = tm.P_ID";
#else
            cmd = "select tp.*, tm.* " +
                " from BILL_PAY tp, IM01RC" + Env.DB_LINK + " tm " +
                " where tp.AR_DATE = " + date +
                " and tp.STATUS in (0, 1, 2)" +
                " and tp.PATIENT_ID = tm.IM01RC_F01";
#endif

            if (place.Length > 0)
            {
                cmd += " and tp.AR_PLACE = " + place;
            }

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }


        public StdReturn Save()
        {
            StdReturn sr = new StdReturn();

            if (this.PtId.Length == 0)
            {
                return sr;
            }

            if (!DateTimeAgent.IsDate(this.ArDate))
            {
                this.ArDate = DateTime.Now.ToString("yyyyMMdd");
            }

            if (this.ArTime.Length == 0)
            {
                this.ArTime = DateTime.Now.ToString("HHmmss");
            }

            if (this.ArPlace.Length == 0)
            {
                this.ArPlace = "0";
            }

            StdDbClass obj = new StdDbClass();

            obj.Table = "BILL_PAY";
            obj.Db = DB.Db2;

            obj.DataList.Add(new StdDbColumn("AR_TIME", StdDbType.NUMBER, this.ArTime));
            obj.DataList.Add(new StdDbColumn("YET_PAYS", StdDbType.NUMBER, this.YetPays));
            obj.DataList.Add(new StdDbColumn("PC_NAME", StdDbType.VARCHAR2, Environment.MachineName));
            obj.DataList.Add(new StdDbColumn("PC_ADDR", StdDbType.VARCHAR2, AppStat.IP4));
            obj.DataList.Add(new StdDbColumn("SAVE_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("SAVE_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, this.Status));

            obj.WhereList.Add("AR_DATE = " + this.ArDate);
            obj.WhereList.Add("AR_PLACE = " + this.ArPlace);
            obj.WhereList.Add("PATIENT_ID = " + this.PtId);

            sr = obj.UpdateSQL();

            if (sr.IntValue == 0)
            {
                obj.DataList.Add(new StdDbColumn("AR_DATE", StdDbType.NUMBER, this.ArDate));
                obj.DataList.Add(new StdDbColumn("AR_PLACE", StdDbType.NUMBER, this.ArPlace));
                obj.DataList.Add(new StdDbColumn("PATIENT_ID", StdDbType.NUMBER, this.PtId));
                sr = obj.InsertSQL();
            }

            return sr;
        }


        public static StdReturn StatusChange(string ar_date, string ar_place, string pt_id, int status)
        {
            StdReturn sr = new StdReturn();

            if (!DateTimeAgent.IsDate(ar_date) || pt_id.Length == 0)
            {
                return sr;
            }

            StdDbClass obj = new StdDbClass();

            obj.Table = "BILL_PAY";
            obj.Db = DB.Db2;

            obj.DataList.Add(new StdDbColumn("PC_NAME", StdDbType.VARCHAR2, Environment.MachineName));
            obj.DataList.Add(new StdDbColumn("PC_ADDR", StdDbType.VARCHAR2, AppStat.IP4));
            obj.DataList.Add(new StdDbColumn("SAVE_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("SAVE_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, status));

            obj.WhereList.Add("AR_DATE = " + ar_date);
            obj.WhereList.Add("AR_PLACE = " + ar_place);
            obj.WhereList.Add("PATIENT_ID = " + pt_id);

            sr = obj.UpdateSQL();

            return sr;
        }
    }

    public class BillPayPlace
    {
        public string Code = "";

        public string Name = "";

        public override string ToString()
        {
            return this.Code + " " + this.Name;
        }

        public BillPayPlace(string code, string name)
        {
            this.Code = code;
            this.Name = name;
        }

        static Dictionary<string, BillPayPlace> dict = new Dictionary<string, BillPayPlace>();

        public static Dictionary<string, BillPayPlace> Dict
        {
            get
            {
                if (dict.Count == 0)
                {
                    dict.Add("1", new BillPayPlace("1", "本館総合"));
                    dict.Add("2", new BillPayPlace("2", "南館内科"));
                    dict.Add("99", new BillPayPlace("99", "南館救急"));
                }

                return dict;
            }
        }
    }
}
