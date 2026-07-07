using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class DPCData : StdEntity
    {
        new public static DB Db = DB.Db2;

        /// <summary>
        /// ID
        /// </summary>
        public string Id = "";

        /// <summary>
        /// 入院日
        /// </summary>
        public string AdmDate = "";

        /// <summary>
        /// 種別
        /// 1: 一般病棟, 2: 地域包括ケア病棟, 3: 一般病棟（２）
        /// </summary>
        public string Kind = "";

        public string KindName
        {
            get
            {
                string s = "";

                if (this.Kind.Equals("8"))
                {
                    s = "氏名表示";
                }
                else if (this.Kind.StartsWith("1"))
                {
                    if (this.Kind.Equals("11"))
                    {
                        s = "様式1";
                    }
                    else if (this.Kind.Equals("13"))
                    {
                        s = "医師完了";
                    }
                    else if (this.Kind.Equals("14"))
                    {
                        s = "看護師完了";
                    }
                    else if (this.Kind.Equals("15"))
                    {
                        s = "レセ完了";
                    }
                    else if (this.Kind.Equals("16"))
                    {
                        s = "DPC完了";
                    }
                    else if (this.Kind.Equals("17"))
                    {
                        s = "DPCコメント";
                    }
                }
                else if (this.Kind.StartsWith("2"))
                {
                    if (this.Kind.Equals("21"))
                    {
                        s = "様式1";
                    }
                    else if (this.Kind.Equals("23"))
                    {
                        s = "医師完了";
                    }
                    else if (this.Kind.Equals("24"))
                    {
                        s = "看護師完了";
                    }
                    else if (this.Kind.Equals("25"))
                    {
                        s = "レセ完了";
                    }
                    else if (this.Kind.Equals("26"))
                    {
                        s = "DPC完了";
                    }
                    else if (this.Kind.Equals("27"))
                    {
                        s = "DPCコメント";
                    }
                }
                else if (this.Kind.StartsWith("3"))
                {
                    if (this.Kind.Equals("31"))
                    {
                        s = "様式1";
                    }
                    else if (this.Kind.Equals("33"))
                    {
                        s = "医師完了";
                    }
                    else if (this.Kind.Equals("34"))
                    {
                        s = "看護師完了";
                    }
                    else if (this.Kind.Equals("35"))
                    {
                        s = "レセ完了";
                    }
                    else if (this.Kind.Equals("36"))
                    {
                        s = "DPC完了";
                    }
                    else if (this.Kind.Equals("37"))
                    {
                        s = "DPCコメント";
                    }
                }

                return s;
            }
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string Cont = "";

        /// <summary>
        /// 内容を分割したもの。様式1のみ
        /// </summary>
        public Dictionary<string, DPCDataCont> ContDict
        {
            get
            {
                Dictionary<string, DPCDataCont> dict = new Dictionary<string, DPCDataCont>();

                if (this.Cont.Length > 0)
                {
                    string[] s = this.Cont.Split('\n');

                    foreach (string ss in s)
                    {
                        string sss = ss.TrimEnd('\r','\n');

                        if (sss.Length == 0)
                        {
                            continue;
                        }

                        if (!sss.Contains(','))
                        {
                            continue;
                        }

                        string code = sss.Substring(0, sss.IndexOf(','));
//                        string val = sss.Substring(sss.IndexOf(',') + 1).Replace("<CR+LF>", "\r\n");

                        DPCDataCont cont = new DPCDataCont();
                        cont.Code = code;
                        cont.Text = sss.Substring(sss.IndexOf(',') + 1).Replace("<CR+LF>", "\r\n");

                        dict.Add(code, cont);
                    }
                }

                return dict;
            }
        }

        /// <summary>
        /// ステータス
        /// 1 完成, 2 未完成
        /// </summary>
        public string Status = "";

        /// <summary>
        /// 作成者コード
        /// </summary>
        public string StaffCode = "";

        /// <summary>
        /// 作成者
        /// </summary>
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

        /// <summary>
        /// 保存日
        /// </summary>
        public string SaveDate = "";

        /// <summary>
        /// 保存時刻
        /// </summary>
        public string SaveTime = "";

        /// <summary>
        /// 保存日時
        /// </summary>
        public string SaveDateTime
        {
            get
            {
                string s = "";

                if (this.SaveDate.Length == 8)
                {
                    s = DateTimeAgent.DateFormat(this.SaveDate, DateTimeAgent.DateFormatKind.LONG)
                        + " " + DateTimeAgent.TimeFormat6(this.SaveTime, 4);
                }

                return s;
            }
        }

        public static DPCData Load(string pt_id, string adm_date, string kind)
        {
            DPCData obj = new DPCData();

            if (pt_id.Length == 0 || adm_date.Length != 8 || kind.Length == 0)
            {
                return obj;
            }

            string cmd = "select * from open.DPC_DATA " +
                " where PATIENT_ID = " + pt_id +
                " and ADM_DATE = " + adm_date +
                " and KIND = " + kind;

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                obj = GetFromStdClass(tmp);
                break;
            }

            return obj;
        }


        public static Dictionary<string, DPCData> GetDict(string pt_id, string adm_date)
        {
            Dictionary<string, DPCData> dict = new Dictionary<string, DPCData>();

            if (pt_id.Length == 0 || adm_date.Length != 8)
            {
                return dict;
            }

            string cmd = "select * from open.DPC_DATA " +
                " where PATIENT_ID = " + pt_id +
                " and ADM_DATE = " + adm_date +
                " order by KIND";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                DPCData obj = GetFromStdClass(tmp);

                if (!dict.ContainsKey(obj.Kind))
                {
                    dict.Add(obj.Kind, obj);
                }
            }

            return dict;
        }


        public static List<DPCData> GetList(List<string> pt_list)
        {
            List<DPCData> list = new List<DPCData>();

            if (pt_list.Count == 0)
            {
                return list;
            }

            string pt_id_str = AppString.ConcatList(pt_list, ",");

            string cmd = "select * from open.DPC_DATA " +
                " where PATIENT_ID in (" + pt_id_str + ")" +
                " order by ADM_DATE, KIND";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }


        static DPCData GetFromStdClass(StdClass tmp)
        {
            DPCData obj = new DPCData();

            obj.Id = tmp.DataDict["PATIENT_ID"].ToString();
            obj.AdmDate = tmp.DataDict["ADM_DATE"].ToString();
            obj.Kind = tmp.DataDict["KIND"].ToString();
            
            obj.Cont = tmp.DataDict["CONT"].ToString();
            obj.Status = tmp.DataDict["STATUS"].ToString();
            obj.StaffCode = tmp.DataDict["STAFF"].ToString();
            obj.SaveDate = tmp.DataDict["SAVE_DATE"].ToString();
            obj.SaveTime = tmp.DataDict["SAVE_TIME"].ToString();

            return obj;
        }


        public StdReturn Save()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
            obj.Db = DB.Db2;

            obj.Table = "DPC_DATA";

            obj.DataList.Add(new StdDbColumn("CONT", StdDbType.VARCHAR2, this.Cont));
            obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, this.Status));
            obj.DataList.Add(new StdDbColumn("STAFF", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("SAVE_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("SAVE_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));

            obj.WhereList.Add("PATIENT_ID = " + this.Id);
            obj.WhereList.Add("ADM_DATE = " + this.AdmDate);
            obj.WhereList.Add("KIND = " + this.Kind);

            sr = obj.UpdateSQL();

            if (sr.IntValue == 0)
            {
                obj.DataList.Add(new StdDbColumn("PATIENT_ID", StdDbType.NUMBER, this.Id));
                obj.DataList.Add(new StdDbColumn("ADM_DATE", StdDbType.NUMBER, this.AdmDate));
                obj.DataList.Add(new StdDbColumn("KIND", StdDbType.NUMBER, this.Kind));

                sr = obj.InsertSQL();
            }

            return sr;
        }
    }

    public class DPCDataCont
    {
        public string Code = "";

        string _Text = "";

        public string Text
        {
            set
            {
                this._Text = value;
            }
            get
            {
                string s = "";

                if (this._Text.Length > 0)
                {
                    s = this._Text;
                }
                else if (this._Value.Length > 0)
                {
                    s = this._Value + ": " + DPCItem1.GetData(this.Code).GetSubItem(this._Value).Text;
                }

                return s;
            }
        }

        string _Value = "";

        public string Value
        {
            set
            {
                this._Value = value;
            }
            get
            {
                string s = "";

                if (this._Value.Length > 0)
                {
                    s = this._Value;
                }
                else if (this._Text.Contains(':'))
                {
                    s = this._Text.Split(':')[0];
                }

                return s;
            }
        }

        public string Msg = "";
    }
}
