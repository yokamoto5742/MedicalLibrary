using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class SoapHeader : SoapKey
    {
        /// <summary>
        /// 保険パターン
        /// 1 健保, 2 労災, 3 自賠責, 4 自費, 5 健診
        /// </summary>
        public class SoapIns
        {
            public string Code = "";

            public string Name = "";

            public override string ToString()
            {
                return this.Name;
            }

            public SoapIns(string code = "", string name = "")
            {
                this.Code = code;
                this.Name = name;
            }

            static Dictionary<string, SoapIns> dict = new Dictionary<string, SoapIns>();

            public static Dictionary<string, SoapIns> Dict
            {
                get
                {
                    if (dict.Count == 0)
                    {
                        dict.Add("0", new SoapIns("0", ""));
                        dict.Add("1", new SoapIns("1", "健保"));
                        dict.Add("2", new SoapIns("2", "労災"));
                        dict.Add("3", new SoapIns("3", "自賠責"));
                        dict.Add("4", new SoapIns("4", "自費"));
                        dict.Add("5", new SoapIns("5", "健診"));
                    }

                    return dict;
                }
            }
        }

        public string Dept = "";

        public string DeptName
        {
            get
            {
                string s = "";

                if (Dict.DeptDict.ContainsKey(Dept))
                {
                    s = Dict.DeptDict[Dept].FullName;
                }

                return s;
            }
        }

        /// <summary>
        /// ＳＯＡＰ対象日
        /// </summary>
        public int SoapDate = 0;

        public int UpDate = 0;

        public int UpTime = 0;

        public string UpStaff = "";

        public string UpStaffName
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(UpStaff))
                {
                    s = Dict.StaffDict[UpStaff].Name;
                }

                return s;
            }
        }

#if INNO
        /// <summary>
        /// 保険パターン
        /// </summary>
        public string Ins = "";
#endif

        /// <summary>
        /// 保険区分
        /// MACS では 1～5
        /// inno では 1～99
        /// </summary>
        public string InsKind = "";

        public string InsKindName
        {
            get
            {
                string s = "";
#if INNO
                if (Insurance.Dict.ContainsKey(this.InsKind))
                {
                    s = Insurance.Dict[this.InsKind].ShortName;
                }
#else
                if (SoapIns.Dict.ContainsKey(this.InsKind))
                {
                    s = SoapIns.Dict[this.InsKind].Name;
                }
#endif
                return s;
            }
        }

        /// <summary>
        /// Problem（Innoカルテのみ）
        /// </summary>
        public string Problem = "";


        public List<SoapDetail> DetailList = new List<SoapDetail>();


        public static Dictionary<string, List<SoapHeader>> GetDict(string pt_id, List<string> date_list)
        {
            Dictionary<string, List<SoapHeader>> dict = new Dictionary<string, List<SoapHeader>>();

            if (date_list.Count == 0 || AppString.ConcatList(date_list, ",").Length == 0)
            {
                return dict;
            }
#if INNO
            string cmd = "select t.*, th.HOKEN_TYPE " +
                " from D_SOAP_HEADER t, M_PATIENT_HOKEN th " +
                " where t.P_ID = " + pt_id +
                " and t.SOAP_DATE in (" + AppString.ConcatList(date_list, ",") + ") " +
                " and (t.DEL_FLG is null or t.DEL_FLG = 0) " +
                " and t.P_ID = th.P_ID and t.P_HOKEN = th.P_HOKEN " +
                " order by t.KEY_SOAP desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
#else
            string cmd = "select * from macs.ADT_ＳＯＡＰデータヘッダ t " +
                " where t.患者コード = " + pt_id +
                " and t.ＳＯＡＰ対象日 in (" + AppString.ConcatList(date_list, ",") + ") " +
                " and t.削除フラグ = 0 " +
                " order by 更新日 desc, 更新時間 desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif
            foreach (StdClass tmp in tmp_list)
            {
                SoapHeader obj = GetFromStdClass(tmp);

                if (dict.ContainsKey(obj.SoapDate.ToString()))
                {
                    dict[obj.SoapDate.ToString()].Add(obj);
                }
                else
                {
                    List<SoapHeader> list = new List<SoapHeader>();
                    list.Add(obj);
                    dict.Add(obj.SoapDate.ToString(), list);
                }
            }

            // 日付内で時間降順に並べ替える
            foreach (string date in dict.Keys)
            {
                dict[date].Sort((x, y) =>
                {
                    return y.RegTime - x.RegTime;
                });
            }

            // SoapData のデータを取得する
            Dictionary<string, Dictionary<string, List<SoapDetail>>> data_dict = SoapDetail.GetDataDict(pt_id, date_list);

            foreach (string date in data_dict.Keys)
            {
                if (!dict.ContainsKey(date))
                {
                    continue;
                }

                // 該当日の SoapData 辞書
                Dictionary<string, List<SoapDetail>> day_dict = data_dict[date];

                foreach (SoapHeader header in dict[date])
                {
                    if (day_dict.ContainsKey(header.Key))
                    {
                        header.DetailList = day_dict[header.Key];
                    }
                }
            }

            return dict;
        }

        static SoapHeader GetFromStdClass(StdClass tmp)
        {
            SoapHeader obj = new SoapHeader();
#if INNO
            obj.Key = tmp.GetDataString("KEY_SOAP");

            obj.PtId = tmp.GetDataString("P_ID");
            obj.InOut = tmp.GetDataInt("INOUT");
            obj.RegDate = tmp.GetDataInt("REG_DATE");
            obj.RegTime = tmp.GetDataInt("REG_TIME");
            obj.RegStaff = tmp.GetDataString("REG_USR");

            obj.Dept = tmp.GetDataString("DEPT");
            obj.SoapDate = tmp.GetDataInt("SOAP_DATE");
            obj.Ins = tmp.GetDataString("P_HOKEN");
            obj.InsKind = tmp.GetDataString("HOKEN_TYPE");

            obj.Problem = tmp.GetDataString("PROBLEM_INFO").Trim();

            obj.UpDate = tmp.GetDataInt("UP_DATE");
            obj.UpTime = tmp.GetDataInt("UP_TIME");
            obj.UpStaff = tmp.GetDataString("UP_USR");
#else
            obj.PtId = tmp.GetDataString("患者コード");
            obj.InOut = tmp.GetDataInt("入外区分");
            obj.RegDate = tmp.GetDataInt("登録日");
            obj.RegTime = tmp.GetDataInt("登録時間");
            obj.RegStaff = tmp.GetDataString("登録者");
            obj.SEQ = tmp.GetDataInt("連番");

            obj.Dept = tmp.GetDataString("登録科");
            obj.SoapDate = tmp.GetDataInt("ＳＯＡＰ対象日");
            obj.InsKind = tmp.GetDataString("保険区分");

            obj.UpDate = tmp.GetDataInt("更新日");
            obj.UpTime = tmp.GetDataInt("更新時間");
            obj.UpStaff = tmp.GetDataString("更新者");
#endif
            return obj;
        }

        public static int GetMaxSEQ(string pt_id, int in_out, int reg_date, int reg_time, string reg_staff)
        {
            int i = 0;

            if (pt_id.Length == 0 || in_out == 0 || reg_date == 0 || reg_staff.Length == 0)
            {
                return i;
            }
#if INNO
#else
            string cmd = "select max(連番) 連番 from ADT_ＳＯＡＰデータヘッダ t " +
                " where t.患者コード = " + pt_id +
                " and t.入外区分 = " + in_out +
                " and t.登録日 = " + reg_date +
                " and t.登録時間 = " + reg_time +
                " and t.登録者 = " + reg_staff;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                int.TryParse(tmp.DataDict["連番"].ToString(), out i);
                break;
            }
#endif
            return i;
        }

        public StdReturn Insert()
        {
            StdReturn sr = new StdReturn();

            if (this.PtId.Length == 0 || this.InOut == 0 || this.SoapDate == 0)
            {
                return sr;
            }
#if INNO
#else
            StdDbClass obj = new StdDbClass();

            obj.Table = "ADT_ＳＯＡＰデータヘッダ";

            string reg_date = DateTime.Now.ToString("yyyyMMdd");
            string reg_time = DateTime.Now.ToString("HHmmss");

            if (this.RegDate == 0)
            {
                this.RegDate = int.Parse(reg_date);
                this.RegTime = int.Parse(reg_time);
            }

            this.RegStaff = this.RegStaff.Length > 0 ? this.RegStaff : LoginUser.Id;
            this.SEQ = GetMaxSEQ(this.PtId, this.InOut, this.RegDate, this.RegTime, this.RegStaff) + 1;

            obj.DataList.Add(new StdDbColumn("患者コード", StdDbType.NUMBER, this.PtId));
            obj.DataList.Add(new StdDbColumn("入外区分", StdDbType.NUMBER, this.InOut));
            obj.DataList.Add(new StdDbColumn("登録日", StdDbType.NUMBER, this.RegDate));
            obj.DataList.Add(new StdDbColumn("登録時間", StdDbType.NUMBER, this.RegTime));
            obj.DataList.Add(new StdDbColumn("登録者", StdDbType.NUMBER, this.RegStaff));
            obj.DataList.Add(new StdDbColumn("連番", StdDbType.NUMBER, this.SEQ));

            obj.DataList.Add(new StdDbColumn("登録科", StdDbType.NUMBER, this.Dept));
            obj.DataList.Add(new StdDbColumn("ＳＯＡＰ対象日", StdDbType.NUMBER, this.SoapDate));
            obj.DataList.Add(new StdDbColumn("受付連番", StdDbType.NUMBER, null));
            obj.DataList.Add(new StdDbColumn("所属コード", StdDbType.NUMBER, LoginUser.SectionId));
            obj.DataList.Add(new StdDbColumn("外来区分", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("初診フラグ", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("マーク１", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("マーク２", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("保険区分", StdDbType.NUMBER, this.InsKind));
            obj.DataList.Add(new StdDbColumn("編集フラグ", StdDbType.NUMBER, 1));

            obj.DataList.Add(new StdDbColumn("ＰＤＦ出力区分", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("ＰＤＦ出力日", StdDbType.NUMBER, null));
            obj.DataList.Add(new StdDbColumn("ＰＤＦ出力時間", StdDbType.NUMBER, null));
            obj.DataList.Add(new StdDbColumn("代行登録者", StdDbType.NUMBER, LoginUser.Id2));

            obj.DataList.Add(new StdDbColumn("削除フラグ", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("削除理由", StdDbType.VARCHAR2, null));
            obj.DataList.Add(new StdDbColumn("削除日", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("削除時間", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("削除者", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("代行削除者", StdDbType.NUMBER, null));

            obj.DataList.Add(new StdDbColumn("更新日", StdDbType.NUMBER, reg_date));
            obj.DataList.Add(new StdDbColumn("更新時間", StdDbType.NUMBER, reg_time));
            obj.DataList.Add(new StdDbColumn("更新者", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("代行更新者", StdDbType.NUMBER, LoginUser.Id2));

            using (DbTransaction tran = Db.Connection.BeginTransaction())
            {
                try
                {
                    sr = obj.InsertSQL();

                    SoapKey key = (SoapKey)this;

                    foreach (SoapDetail data in this.DetailList)
                    {
                        data.Insert(key);
                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw new Exception(ex.Message);
                }
            }
#endif
            return sr;
        }

        public StdReturn Delete()
        {
            StdReturn sr = new StdReturn();

            if (this.PtId.Length == 0 || this.InOut == 0 ||
                this.RegDate == 0 || this.RegStaff.Length == 0 || this.SEQ == 0)
            {
                return sr;
            }
#if INNO
#else
            StdDbClass obj = new StdDbClass();

            obj.Table = "ADT_ＳＯＡＰデータヘッダ";

            string reg_date = DateTime.Now.ToString("yyyyMMdd");
            string reg_time = DateTime.Now.ToString("HHmmss");

            obj.DataList.Add(new StdDbColumn("削除フラグ", StdDbType.NUMBER, 1));
            obj.DataList.Add(new StdDbColumn("削除理由", StdDbType.VARCHAR2, null));
            obj.DataList.Add(new StdDbColumn("削除日", StdDbType.NUMBER, reg_date));
            obj.DataList.Add(new StdDbColumn("削除時間", StdDbType.NUMBER, reg_time));
            obj.DataList.Add(new StdDbColumn("削除者", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("代行削除者", StdDbType.NUMBER, LoginUser.Id2));

            obj.WhereList.Add("患者コード = " + this.PtId);
            obj.WhereList.Add("入外区分 = " + this.InOut);
            obj.WhereList.Add("登録日 = " + this.RegDate);
            obj.WhereList.Add("登録時間 = " + this.RegTime);
            obj.WhereList.Add("登録者 = " + this.RegStaff);
            obj.WhereList.Add("連番 = " + this.SEQ);

            sr = obj.UpdateSQL();
#endif
            return sr;
        }
    }
}
