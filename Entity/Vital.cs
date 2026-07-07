using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class Vital : StdKarte1
    {
        public string Code = "";

        public string VitalDate = "";

        public int SEQ = 0;

        public string VitalTime = "";

        public string VitalTimeString
        {
            get
            {
                return DateTimeAgent.TimeFormat(this.VitalTime);
            }
        }

        public string StaffCode = "";

        public string Data1 = "";

        public string Data2 = "";

        public int Num
        {
            get
            {
                int result = 0;

                if (VitalMaster.Dict.ContainsKey(this.Code))
                {
                    result = VitalMaster.Dict[this.Code].Num;
                }

                return result;
            }
        }

        public static List<Vital> GetListByDates(string pt_id, string date1, string date2)
        {
            List<Vital> list = new List<Vital>();

            if (pt_id.Length == 0 || date1.Length != 8 || date2.Length != 8)
            {
                return list;
            }

            // バイタルデータの取得
            // データ区分
            // 1:体温, 2:血圧, 3:脈拍, 4:尿/便回数, 7:SpO2 %, 10:食事
            // 901～906 フリーコメント:看護サ処検, 902:総除水量, 903:排液量, 905:レスキュー, 906:PTCD
            string cmd = "select * from ADT_バイタルデータ " +
                    " where 患者コード = " + pt_id +
                    " and 測定日 >= " + date1 +
                    " and 測定日 <= " + date2 +
                    " order by データ区分, 測定日, 測定時間";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }

        public static List<Vital> GetListByDate(string pt_id, string date1)
        {
            List<Vital> list = new List<Vital>();

            if (pt_id.Length == 0 || date1.Length != 8)
            {
                return list;
            }

            // バイタルデータの取得
            // データ区分
            // 1:体温, 2:血圧, 3:脈拍, 4:尿/便回数, 7:SpO2 %, 10:食事
            // 901～906 フリーコメント:看護サ処検, 902:総除水量, 903:排液量, 905:レスキュー, 906:PTCD
            string cmd = "select * from ADT_バイタルデータ " +
                    " where 患者コード = " + pt_id +
                    " and 測定日 = " + date1 +
                    " order by データ区分, 測定時間";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }

        public static List<Vital> GetListByDateCode(string pt_id, string date1, string code)
        {
            List<Vital> list = new List<Vital>();

            if (pt_id.Length == 0 || date1.Length != 8 || code.Length == 0)
            {
                return list;
            }

            // バイタルデータの取得
            // データ区分
            // 1:体温, 2:血圧, 3:脈拍, 4:尿/便回数, 7:SpO2 %, 10:食事
            // 901～906 フリーコメント:看護サ処検, 902:総除水量, 903:排液量, 905:レスキュー, 906:PTCD
            string cmd = "select * from ADT_バイタルデータ " +
                    " where 患者コード = " + pt_id +
                    " and 測定日 = " + date1 +
                    " and データ区分 = " + code +
                    " order by 測定時間";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }

        public static int GetMaxSEQ(string pt_id, string date1, string code)
        {
            int i = 0;

            if (pt_id.Length == 0 || date1.Length != 8 || code.Length == 0)
            {
                return i;
            }

            // バイタルデータの取得
            // データ区分
            // 1:体温, 2:血圧, 3:脈拍, 4:尿/便回数, 7:SpO2 %, 10:食事
            // 901～906 フリーコメント:看護サ処検, 902:総除水量, 903:排液量, 905:レスキュー, 906:PTCD
            string cmd = "select max(連番) 連番 from ADT_バイタルデータ " +
                    " where 患者コード = " + pt_id + " and " +
                    " 測定日 = " + date1 +
                    " and データ区分 = " + code +
                    " order by 測定時間";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                int.TryParse(tmp.DataDict["連番"].ToString(), out i);
                break;
            }

            return i;
        }

        static Vital GetFromStdClass(StdClass tmp)
        {
            Vital obj = new Vital();

            obj.BaseFromStdClass(tmp);
            obj.PtId = tmp.DataDict["患者コード"].ToString();
            obj.Code = tmp.DataDict["データ区分"].ToString();
            obj.VitalDate = tmp.DataDict["測定日"].ToString();
            int.TryParse(tmp.DataDict["連番"].ToString(), out obj.SEQ);
            obj.VitalTime = tmp.DataDict["測定時間"].ToString();
            obj.Data1 = tmp.DataDict["測定値１"].ToString();
            obj.Data2 = tmp.DataDict["測定値２"].ToString();

            return obj;
        }

        public StdReturn Insert()
        {
            StdReturn sr = new StdReturn();

            if (this.PtId.Length == 0 || this.Code.Length == 0 || this.VitalDate.Length != 8)
            {
                return sr;
            }

            StdDbClass obj = new StdDbClass();

            obj.Table = "ADT_バイタルデータ";

            string reg_date = DateTime.Now.ToString("yyyyMMdd");
            string reg_time = DateTime.Now.ToString("HHmmss");
            int seq = GetMaxSEQ(this.PtId, this.VitalDate, this.Code) + 1;

            obj.DataList.Add(new StdDbColumn("患者コード", StdDbType.NUMBER, this.PtId));
            obj.DataList.Add(new StdDbColumn("データ区分", StdDbType.NUMBER, this.Code));
            obj.DataList.Add(new StdDbColumn("測定日", StdDbType.NUMBER, this.VitalDate));
            obj.DataList.Add(new StdDbColumn("連番", StdDbType.NUMBER, seq));

            obj.DataList.Add(new StdDbColumn("測定時間", StdDbType.NUMBER, this.VitalTime));
            obj.DataList.Add(new StdDbColumn("測定者", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("測定値１", StdDbType.VARCHAR2, this.Data1));
            obj.DataList.Add(new StdDbColumn("測定値２", StdDbType.VARCHAR2, this.Data2));

            obj.DataList.Add(new StdDbColumn("ＰＤＦ出力区分", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("ＰＤＦ出力日", StdDbType.NUMBER, null));
            obj.DataList.Add(new StdDbColumn("ＰＤＦ出力時間", StdDbType.NUMBER, null));

            obj.DataList.Add(new StdDbColumn("登録日", StdDbType.NUMBER, reg_date));
            obj.DataList.Add(new StdDbColumn("登録時間", StdDbType.NUMBER, reg_time));
            obj.DataList.Add(new StdDbColumn("登録者", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("代行登録者", StdDbType.NUMBER, LoginUser.Id2));

            obj.DataList.Add(new StdDbColumn("更新日", StdDbType.NUMBER, reg_date));
            obj.DataList.Add(new StdDbColumn("更新時間", StdDbType.NUMBER, reg_time));
            obj.DataList.Add(new StdDbColumn("更新者", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("代行更新者", StdDbType.NUMBER, LoginUser.Id2));

            sr = obj.InsertSQL();

            return sr;
        }

        public StdReturn Update()
        {
            StdReturn sr = new StdReturn();

            if (this.PtId.Length == 0 || this.Code.Length == 0 || this.VitalDate.Length != 8)
            {
                return sr;
            }

            StdDbClass obj = new StdDbClass();

            obj.Table = "ADT_バイタルデータ";

            string reg_date = DateTime.Now.ToString("yyyyMMdd");
            string reg_time = DateTime.Now.ToString("HHmmss");

            obj.DataList.Add(new StdDbColumn("測定時間", StdDbType.NUMBER, this.VitalTime));
            obj.DataList.Add(new StdDbColumn("測定者", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("測定値１", StdDbType.VARCHAR2, this.Data1));
            obj.DataList.Add(new StdDbColumn("測定値２", StdDbType.VARCHAR2, this.Data2));

            obj.DataList.Add(new StdDbColumn("ＰＤＦ出力区分", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("ＰＤＦ出力日", StdDbType.NUMBER, null));
            obj.DataList.Add(new StdDbColumn("ＰＤＦ出力時間", StdDbType.NUMBER, null));

            obj.DataList.Add(new StdDbColumn("更新日", StdDbType.NUMBER, reg_date));
            obj.DataList.Add(new StdDbColumn("更新時間", StdDbType.NUMBER, reg_time));
            obj.DataList.Add(new StdDbColumn("更新者", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("代行更新者", StdDbType.NUMBER, LoginUser.Id2));

            obj.WhereList.Add("患者コード = " + this.PtId);
            obj.WhereList.Add("データ区分 = " + this.Code);
            obj.WhereList.Add("測定日 = " + this.VitalDate);
            obj.WhereList.Add("連番 = " + this.SEQ);

            sr = obj.UpdateSQL();

            if (sr.IntValue == 0)
            {
                int seq = GetMaxSEQ(this.PtId, this.VitalDate, this.Code) + 1;

                obj.DataList.Add(new StdDbColumn("患者コード", StdDbType.NUMBER, this.PtId));
                obj.DataList.Add(new StdDbColumn("データ区分", StdDbType.NUMBER, this.Code));
                obj.DataList.Add(new StdDbColumn("測定日", StdDbType.NUMBER, this.VitalDate));
                obj.DataList.Add(new StdDbColumn("連番", StdDbType.NUMBER, seq));

                obj.DataList.Add(new StdDbColumn("登録日", StdDbType.NUMBER, reg_date));
                obj.DataList.Add(new StdDbColumn("登録時間", StdDbType.NUMBER, reg_time));
                obj.DataList.Add(new StdDbColumn("登録者", StdDbType.NUMBER, LoginUser.Id));
                obj.DataList.Add(new StdDbColumn("代行登録者", StdDbType.NUMBER, LoginUser.Id2));

                sr = obj.InsertSQL();
            }

            return sr;
        }

        public StdReturn Delete()
        {
            StdReturn sr = new StdReturn();

            if (this.PtId.Length == 0 || this.Code.Length == 0 || this.VitalDate.Length != 8)
            {
                return sr;
            }

            StdDbClass obj = new StdDbClass();

            obj.Table = "ADT_バイタルデータ";

            obj.WhereList.Add("患者コード = " + this.PtId);
            obj.WhereList.Add("データ区分 = " + this.Code);
            obj.WhereList.Add("測定日 = " + this.VitalDate);
            obj.WhereList.Add("連番 = " + this.SEQ);

            sr = obj.DeleteSQL();

            return sr;
        }
    }
}
