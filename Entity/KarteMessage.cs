using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class KarteMessage : StdEntity
    {
        /// <summary>
        /// 受信者コード
        /// </summary>
        public string ToCode = "";

        public string ToName
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(this.ToCode))
                {
                    s = Dict.StaffDict[this.ToCode].Name;
                }

                return s;
            }
        }

        /// <summary>
        /// 送信者コード
        /// </summary>
        public string FromCode = "";

        public string FromName
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(this.FromCode))
                {
                    s = Dict.StaffDict[this.FromCode].Name;
                }

                return s;
            }
        }

        /// <summary>
        /// 送信日
        /// </summary>
        public string SendDate = "";

        /// <summary>
        /// 送信時刻
        /// </summary>
        public string SendTime = "";

        /// <summary>
        /// 送信日時
        /// </summary>
        public string SendDateTime
        {
            get
            {
                string s = "";

                if (this.SendDate.Length == 8)
                {
                    s = DateTimeAgent.DateFormat(this.SendDate, DateTimeAgent.DateFormatKind.LONG)
                        + " " + DateTimeAgent.TimeFormat6(this.SendTime, 4);
                }

                return s;
            }
        }

        /// <summary>
        /// 件名
        /// </summary>
        public string Title = "";

        /// <summary>
        /// 本文
        /// </summary>
        public string Msg = "";

        /// <summary>
        /// 患者コード
        /// </summary>
        public string PtId = "";

        /// <summary>
        /// 患者名
        /// </summary>
        public string PtName = "";

        /// <summary>
        /// 重要度
        /// </summary>
        public string Priority = "";

        public string PriorityValue
        {
            get
            {
                string s = "";

                if (this.Priority.Equals("0"))
                {
                    s = "中";
                }
                else if (this.Priority.Equals("1"))
                {
                    s = "高";
                }
                else if (this.Priority.Equals("2"))
                {
                    s = "低";
                }

                return s;
            }
        }

        /// <summary>
        /// 開封フラグ
        /// </summary>
        public bool ReadFlg = false;

        /// <summary>
        /// 開封日
        /// </summary>
        public string ReadDate = "";

        /// <summary>
        /// 開封時刻
        /// </summary>
        public string ReadTime = "";

        /// <summary>
        /// 開封日時
        /// </summary>
        public string ReadDateTime
        {
            get
            {
                string s = "";

                if (this.ReadDate.Length == 8)
                {
                    s = DateTimeAgent.DateFormat(this.ReadDate, DateTimeAgent.DateFormatKind.LONG)
                        + " " + DateTimeAgent.TimeFormat6(this.ReadTime, 4);
                }

                return s;
            }
        }

        /// <summary>
        /// 受信者削除フラグ
        /// </summary>
        public bool ToDeleteFlg = false;

        /// <summary>
        /// 送信者削除フラグ
        /// </summary>
        public bool FromDeleteFlg = false;



        /// <summary>
        /// 受信メッセージを取得
        /// </summary>
        /// <param name="to_code">受信者コード</param>
        /// <param name="deleted">true: 削除も取得する, false: 削除は取得しない</param>
        /// <param name="read">true: 既読も取得する, false: 既読は取得しない</param>
        /// <returns></returns>
        public static List<KarteMessage> GetRcvList(string to_code, bool deleted = false, bool read = true)
        {
            List<KarteMessage> list = new List<KarteMessage>();

            if (to_code.Length == 0)
            {
                return list;
            }
#if INNO
            string cmd = "select td.*, tm.P_NAME " +
                " from D_KARTE_MESSAGE td, M_PATIENT tm " +
                " where td.RECV_USR = " + to_code +
                " and td.P_ID = tm.P_ID";

            if (!read)
            {
                cmd += " and OPEN_FLG = 0 ";
            }

            if (!deleted)
            {
                cmd += " and DEL_FLG_RECV = 0 ";
            }

            cmd += " order by SEND_DATE desc, SEND_TIME desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }
#else
            string cmd = "select t1.*, t2.IM01RC_F04 from ADT_メールデータ t1, IM01RC t2 " +
                " where t1.受信者コード = " + to_code +
                " and t1.患者コード = t2.IM01RC_F01";

            if (!read)
            {
                cmd += " and 開封フラグ = 0 ";
            }

            if (!deleted)
            {
                cmd += " and 受信者削除フラグ = 0 ";
            }

            cmd += " order by 送信日 desc, 送信時間 desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }
#endif
            return list;
        }


        public static List<KarteMessage> GetSendList(string from_code, bool deleted = false)
        {
            List<KarteMessage> list = new List<KarteMessage>();

            if (from_code.Length == 0)
            {
                return list;
            }
#if INNO
            string cmd = "select td.*, tm.P_NAME " +
                " from D_KARTE_MESSAGE td, M_PATIENT tm " +
                " where td.SEND_USR = " + from_code +
                " and td.P_ID = tm.P_ID";

            if (!deleted)
            {
                cmd += " and DEL_FLG_SEND = 0 ";
            }

            cmd += " order by SEND_DATE desc, SEND_TIME desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }
#else
            string cmd = "select t1.*, t2.IM01RC_F04 from ADT_メールデータ t1, IM01RC t2 " +
                " where t1.送信者コード = " + from_code +
                " and t1.患者コード = t2.IM01RC_F01";

            if (!deleted)
            {
                cmd += " and 送信者削除フラグ = 0 ";
            }

            cmd += " order by 送信日 desc, 送信時間 desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }
#endif
            return list;
        }


        public static List<KarteMessage> GetSameList(string from_code, string send_date, string send_time, bool deleted = false)
        {
            List<KarteMessage> list = new List<KarteMessage>();

            if (from_code.Length == 0 || send_date.Length != 8)
            {
                return list;
            }
#if INNO
            string cmd = "select td.*, tm.P_NAME " +
                " from D_KARTE_MESSAGE td, M_PATIENT tm " +
                " where td.SEND_USR = " + from_code +
                " and td.SEND_DATE = " + send_date +
                " and td.SEND_TIME = " + send_time +
                " and td.P_ID = tm.P_ID";

            if (!deleted)
            {
                cmd += " and DEL_FLG_RECV = 0 ";
            }

            cmd += " order by SEND_DATE desc, SEND_TIME desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }
#else
            string cmd = "select t1.*, t2.IM01RC_F04 from ADT_メールデータ t1, IM01RC t2 " +
                " where t1.送信者コード = " + from_code +
                " and t1.送信日 = " + send_date +
                " and t1.送信時間 = " + send_time +
                " and t1.患者コード = t2.IM01RC_F01";

            if (!deleted)
            {
                cmd += " and 受信者削除フラグ = 0 ";
            }

            cmd += " order by 送信日 desc, 送信時間 desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }
#endif
            return list;
        }


        static KarteMessage GetFromStdClass(StdClass tmp)
        {
            KarteMessage obj = new KarteMessage();
#if INNO
            obj.ToCode = tmp.GetDataString("RECV_USR");
            obj.FromCode = tmp.GetDataString("SEND_USR");
            obj.SendDate = tmp.GetDataString("SEND_DATE");
            obj.SendTime = tmp.GetDataString("SEND_TIME");
            obj.Title = tmp.GetDataString("TITLE");
            obj.Msg = tmp.GetDataString("MAIL_TEXT");
            obj.PtId = tmp.GetDataString("P_ID");
            obj.PtName = tmp.GetDataString("P_NAME").Trim();
            obj.Priority = tmp.GetDataString("MAIL_LEVEL");
            obj.ReadFlg = tmp.GetDataString("OPEN_FLG").Equals("1");
            obj.ReadDate = tmp.GetDataString("OPEN_DATE");
            obj.ReadTime = tmp.GetDataString("OPEN_TIME");
            obj.ToDeleteFlg = tmp.GetDataString("DEL_FLG_RECV").Equals("1");
            obj.FromDeleteFlg = tmp.GetDataString("DEL_FLG_SEND").Equals("1");
#else
            obj.ToCode = tmp.DataDict["受信者コード"].ToString();
            obj.FromCode = tmp.DataDict["送信者コード"].ToString();
            obj.SendDate = tmp.DataDict["送信日"].ToString();
            obj.SendTime = tmp.DataDict["送信時間"].ToString();
            obj.Title = tmp.DataDict["件名"].ToString();
            obj.Msg = tmp.DataDict["本文"].ToString();
            obj.PtId = tmp.DataDict["患者コード"].ToString();
            obj.PtName = tmp.DataDict["IM01RC_F04"].ToString().Trim();
            obj.Priority = tmp.DataDict["重要度"].ToString();
            obj.ReadFlg = tmp.DataDict["開封フラグ"].ToString().Equals("1") ? true : false;
            obj.ReadDate = tmp.DataDict["開封日"].ToString();
            obj.ReadTime = tmp.DataDict["開封時間"].ToString();
            obj.ToDeleteFlg = tmp.DataDict["受信者削除フラグ"].ToString().Equals("1") ? true : false;
            obj.FromDeleteFlg = tmp.DataDict["送信者削除フラグ"].ToString().Equals("1") ? true : false;
#endif
            return obj;
        }


        /// <summary>
        /// 送信
        /// </summary>
        public StdReturn Send()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
#if INNO
            obj.Db = DB.Db3;
            obj.Table = "D_KARTE_MESSAGE";

            obj.DataList.Add(new StdDbColumn("RECV_USR", StdDbType.NUMBER, this.ToCode));
            obj.DataList.Add(new StdDbColumn("SEND_USR", StdDbType.NUMBER, this.FromCode));
            obj.DataList.Add(new StdDbColumn("SEND_DATE", StdDbType.NUMBER, this.SendDate.Length == 8 ? this.SendDate : DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("SEND_TIME", StdDbType.NUMBER, this.SendTime.Length > 0 ? this.SendTime : DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("TITLE", StdDbType.VARCHAR2, this.Title));
            obj.DataList.Add(new StdDbColumn("MAIL_TEXT", StdDbType.VARCHAR2, this.Msg));
            obj.DataList.Add(new StdDbColumn("P_ID", StdDbType.NUMBER, this.PtId));
            obj.DataList.Add(new StdDbColumn("MAIL_LEVEL", StdDbType.NUMBER, this.Priority.Length > 0 ? this.Priority : "0"));
            obj.DataList.Add(new StdDbColumn("OPEN_FLG", StdDbType.NUMBER, this.ReadFlg == true ? 1 : 0));
            obj.DataList.Add(new StdDbColumn("OPEN_DATE", StdDbType.NUMBER, this.ReadDate));
            obj.DataList.Add(new StdDbColumn("OPEN_TIME", StdDbType.NUMBER, this.ReadTime));
            obj.DataList.Add(new StdDbColumn("DEL_FLG_RECV", StdDbType.NUMBER, this.ToDeleteFlg == true ? 1 : 0));
            obj.DataList.Add(new StdDbColumn("DEL_FLG_SEND", StdDbType.NUMBER, this.FromDeleteFlg == true ? 1 : 0));

            sr = obj.InsertSQL();
#else
            obj.Db = DB.Db1;
            obj.Table = "ADT_メールデータ";

            obj.DataList.Add(new StdDbColumn("受信者コード", StdDbType.NUMBER, this.ToCode));
            obj.DataList.Add(new StdDbColumn("送信者コード", StdDbType.NUMBER, this.FromCode));
            obj.DataList.Add(new StdDbColumn("送信日", StdDbType.NUMBER, this.SendDate.Length == 8 ? this.SendDate : DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("送信時間", StdDbType.NUMBER, this.SendTime.Length > 0 ? this.SendTime : DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("件名", StdDbType.VARCHAR2, this.Title));
            obj.DataList.Add(new StdDbColumn("本文", StdDbType.VARCHAR2, this.Msg));
            obj.DataList.Add(new StdDbColumn("患者コード", StdDbType.NUMBER, this.PtId));
            obj.DataList.Add(new StdDbColumn("重要度", StdDbType.NUMBER, this.Priority.Length > 0 ? this.Priority : "0"));
            obj.DataList.Add(new StdDbColumn("開封フラグ", StdDbType.NUMBER, this.ReadFlg == true ? 1 : 0));
            obj.DataList.Add(new StdDbColumn("開封日", StdDbType.NUMBER, this.ReadDate));
            obj.DataList.Add(new StdDbColumn("開封時間", StdDbType.NUMBER, this.ReadTime));
            obj.DataList.Add(new StdDbColumn("受信者削除フラグ", StdDbType.NUMBER, this.ToDeleteFlg == true ? 1 : 0));
            obj.DataList.Add(new StdDbColumn("送信者削除フラグ", StdDbType.NUMBER, this.FromDeleteFlg == true ? 1 : 0));

            sr = obj.InsertSQL();
#endif
            return sr;
        }


        /// <summary>
        /// 開封・未開封
        /// </summary>
        public static StdReturn Read(string to_code, string from_code, string send_date, string send_time, bool read_flg = true)
        {
            StdReturn sr = new StdReturn();

            if (to_code.Length == 0 || from_code.Length == 0 || send_date.Length != 8 || send_time.Length == 0)
            {
                return sr;
            }

            StdDbClass obj = new StdDbClass();
#if INNO
            obj.Db = DB.Db3;
            obj.Table = "D_KARTE_MESSAGE";

            obj.DataList.Add(new StdDbColumn("OPEN_FLG", StdDbType.NUMBER, read_flg == true ? 1 : 0));

            if (read_flg)
            {
                obj.DataList.Add(new StdDbColumn("OPEN_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
                obj.DataList.Add(new StdDbColumn("OPEN_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            }
            else
            {
                obj.DataList.Add(new StdDbColumn("OPEN_DATE", StdDbType.NUMBER, 0));
                obj.DataList.Add(new StdDbColumn("OPEN_TIME", StdDbType.NUMBER, 0));
            }

            obj.WhereList.Add("RECV_USR = " + to_code);
            obj.WhereList.Add("SEND_USR = " + from_code);
            obj.WhereList.Add("SEND_DATE = " + send_date);
            obj.WhereList.Add("SEND_TIME = " + send_time);

            sr = obj.UpdateSQL();
#else
            obj.Db = DB.Db1;
            obj.Table = "ADT_メールデータ";

            obj.DataList.Add(new StdDbColumn("開封フラグ", StdDbType.NUMBER, read_flg == true ? 1 : 0));

            if (read_flg)
            {
                obj.DataList.Add(new StdDbColumn("開封日", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
                obj.DataList.Add(new StdDbColumn("開封時間", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            }
            else
            {
                obj.DataList.Add(new StdDbColumn("開封日", StdDbType.NUMBER, 0));
                obj.DataList.Add(new StdDbColumn("開封時間", StdDbType.NUMBER, 0));
            }

            obj.WhereList.Add("受信者コード = " + to_code);
            obj.WhereList.Add("送信者コード = " + from_code);
            obj.WhereList.Add("送信日 = " + send_date);
            obj.WhereList.Add("送信時間 = " + send_time);

            sr = obj.UpdateSQL();
#endif
            return sr;
        }


        /// <summary>
        /// 受信者削除
        /// </summary>
        public static StdReturn RsvDelete(string to_code, string from_code, string send_date, string send_time, bool delete_flg = true)
        {
            StdReturn sr = new StdReturn();

            if (to_code.Length == 0 || from_code.Length == 0 || send_date.Length != 8 || send_time.Length == 0)
            {
                return sr;
            }

            StdDbClass obj = new StdDbClass();
#if INNO
            obj.Db = DB.Db3;
            obj.Table = "D_KARTE_MESSAGE";

            obj.DataList.Add(new StdDbColumn("DEL_FLG_RECV", StdDbType.NUMBER, delete_flg == true ? 1 : 0));

            obj.WhereList.Add("RECV_USR = " + to_code);
            obj.WhereList.Add("SEND_USR = " + from_code);
            obj.WhereList.Add("SEND_DATE = " + send_date);
            obj.WhereList.Add("SEND_TIME = " + send_time);

            sr = obj.UpdateSQL();
#else
            obj.Db = DB.Db1;
            obj.Table = "ADT_メールデータ";

            obj.DataList.Add(new StdDbColumn("受信者削除フラグ", StdDbType.NUMBER, delete_flg == true ? 1 : 0));

            obj.WhereList.Add("受信者コード = " + to_code);
            obj.WhereList.Add("送信者コード = " + from_code);
            obj.WhereList.Add("送信日 = " + send_date);
            obj.WhereList.Add("送信時間 = " + send_time);

            sr = obj.UpdateSQL();
#endif
            return sr;
        }


        /// <summary>
        /// 送信者削除
        /// </summary>
        public static StdReturn SendDelete(string to_code, string from_code, string send_date, string send_time, bool delete_flg = true)
        {
            StdReturn sr = new StdReturn();

            if (to_code.Length == 0 || from_code.Length == 0 || send_date.Length != 8 || send_time.Length == 0)
            {
                return sr;
            }

            StdDbClass obj = new StdDbClass();
#if INNO
            obj.Db = DB.Db3;
            obj.Table = "D_KARTE_MESSAGE";

            obj.DataList.Add(new StdDbColumn("DEL_FLG_SEND", StdDbType.NUMBER, delete_flg == true ? 1 : 0));

            obj.WhereList.Add("RECV_USR = " + to_code);
            obj.WhereList.Add("SEND_USR = " + from_code);
            obj.WhereList.Add("SEND_DATE = " + send_date);
            obj.WhereList.Add("SEND_TIME = " + send_time);

            sr = obj.UpdateSQL();
#else
            obj.Db = DB.Db1;
            obj.Table = "ADT_メールデータ";

            obj.DataList.Add(new StdDbColumn("送信者削除フラグ", StdDbType.NUMBER, delete_flg == true ? 1 : 0));

            obj.WhereList.Add("受信者コード = " + to_code);
            obj.WhereList.Add("送信者コード = " + from_code);
            obj.WhereList.Add("送信日 = " + send_date);
            obj.WhereList.Add("送信時間 = " + send_time);

            sr = obj.UpdateSQL();
#endif
            return sr;
        }


        /// <summary>
        /// 受信者削除フラグの変更
        /// </summary>
        public StdReturn ToDeleteFlgChange()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
#if INNO
            obj.Db = DB.Db3;
            obj.Table = "D_KARTE_MESSAGE";

            obj.DataList.Add(new StdDbColumn("DEL_FLG_RECV", StdDbType.NUMBER, this.ToDeleteFlg == true ? 1 : 0));

            obj.WhereList.Add("RECV_USR = " + this.ToCode);
            obj.WhereList.Add("SEND_USR = " + this.FromCode);
            obj.WhereList.Add("SEND_DATE = " + this.SendDate);
            obj.WhereList.Add("SEND_TIME = " + this.SendTime);

            sr = obj.UpdateSQL();
#else
            obj.Db = DB.Db1;
            obj.Table = "ADT_メールデータ";

            obj.DataList.Add(new StdDbColumn("受信者削除フラグ", StdDbType.NUMBER, this.ToDeleteFlg == true ? 1 : 0));

            obj.WhereList.Add("受信者コード = " + this.ToCode);
            obj.WhereList.Add("送信者コード = " + this.FromCode);
            obj.WhereList.Add("送信日 = " + this.SendDate);
            obj.WhereList.Add("送信時間 = " + this.SendTime);

            sr = obj.UpdateSQL();
#endif
            return sr;
        }


        /// <summary>
        /// 送信者削除フラグの変更
        /// </summary>
        public StdReturn FromDeleteFlgChange()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
#if INNO
            obj.Db = DB.Db3;
            obj.Table = "D_KARTE_MESSAGE";

            obj.DataList.Add(new StdDbColumn("DEL_FLG_SEND", StdDbType.NUMBER, this.FromDeleteFlg == true ? 1 : 0));

            obj.WhereList.Add("RECV_USR = " + this.ToCode);
            obj.WhereList.Add("SEND_USR = " + this.FromCode);
            obj.WhereList.Add("SEND_DATE = " + this.SendDate);
            obj.WhereList.Add("SEND_TIME = " + this.SendTime);

            sr = obj.UpdateSQL();
#else
            obj.Db = DB.Db1;
            obj.Table = "ADT_メールデータ";

            obj.DataList.Add(new StdDbColumn("送信者削除フラグ", StdDbType.NUMBER, this.FromDeleteFlg == true ? 1 : 0));

            obj.WhereList.Add("受信者コード = " + this.ToCode);
            obj.WhereList.Add("送信者コード = " + this.FromCode);
            obj.WhereList.Add("送信日 = " + this.SendDate);
            obj.WhereList.Add("送信時間 = " + this.SendTime);

            sr = obj.UpdateSQL();
#endif
            return sr;
        }
    }
}
