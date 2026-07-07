using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class DrugAdvAgree
    {
        public int RcvStaff = 0;

        public string RcvStaffName
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(this.RcvStaff.ToString()))
                {
                    s = Dict.StaffDict[this.RcvStaff.ToString()].Name;
                }

                return s;
            }
        }

        public int SendStaff = 0;

        public string SendStaffName
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(this.SendStaff.ToString()))
                {
                    s = Dict.StaffDict[this.SendStaff.ToString()].Name;
                }

                return s;
            }
        }

        public int SendDate = 0;

        public int SendTime = 0;

        public int PtId = 0;


        PatBase _Pat = new PatBase();

        public PatBase Pat
        {
            get
            {
                if (!this._Pat.Id.Equals(this.PtId.ToString()))
                {
                    this._Pat = PatBase.Load(this.PtId.ToString());
                }

                return this._Pat;
            }
        }

        public int Status = 0;

        public DrugAdvAgree()
        {
        }

        public DrugAdvAgree(StdClass tmp)
        {
            this.RcvStaff = tmp.GetDataInt("受信者コード");
            this.SendStaff = tmp.GetDataInt("送信者コード");
            this.SendDate = tmp.GetDataInt("送信日");
            this.SendTime = tmp.GetDataInt("送信時間");
            this.PtId = tmp.GetDataInt("患者コード");
            this.Status = tmp.GetDataInt("ステータス");
        }

        public static List<DrugAdvAgree> GetListById(string pt_id, string order_by = "")
        {
            List<DrugAdvAgree> list = new List<DrugAdvAgree>();

            if (pt_id.Length == 0) return list;

            string cmd = "select * from DRUG_ADV_AGREE" +
                " where 患者コード = " + pt_id;

            if (order_by.Length > 0)
            {
                cmd += " order by " + order_by;
            }

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(new DrugAdvAgree(tmp));
            }

            return list;
        }

        public static List<DrugAdvAgree> GetListByIds(List<string> pt_id_list, string order_by = "")
        {
            List<DrugAdvAgree> list = new List<DrugAdvAgree>();

            if (AppString.ConcatList(pt_id_list, ",").Length == 0) return list;

            string cmd = "select * from DRUG_ADV_AGREE" +
                "  where 患者コード in (" + AppString.ConcatList(pt_id_list, ",") + ")";

            if (order_by.Length > 0)
            {
                cmd += " order by " + order_by;
            }

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(new DrugAdvAgree(tmp));
            }

            return list;
        }

        public StdReturn Insert()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();

            obj.Table = "DRUG_ADV_AGREE";
            obj.Db = DB.Db2;

            obj.DataList.Add(new StdDbColumn("受信者コード", StdDbType.NUMBER, this.RcvStaff));
            obj.DataList.Add(new StdDbColumn("送信者コード", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("送信日", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("送信時間", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("患者コード", StdDbType.NUMBER, this.PtId));
            obj.DataList.Add(new StdDbColumn("ステータス", StdDbType.NUMBER, this.Status));

            sr = obj.InsertSQL();

            return sr;
        }
/*
        public StdReturn Update()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();

            obj.Table = "DRUG_ADV_AGREE";
            obj.Db = DB.Db2;

            obj.DataList.Add(new StdDbColumn("受信者コード", StdDbType.NUMBER, this.RcvStaff));
            obj.DataList.Add(new StdDbColumn("送信者コード", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("送信日", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("送信時間", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("患者コード", StdDbType.NUMBER, this.PtId));
            obj.DataList.Add(new StdDbColumn("ステータス", StdDbType.NUMBER, this.Status));

            obj.WhereList.Add("受信者コード = " + this.RcvStaff);
            obj.WhereList.Add("送信者コード = " + this.SendStaff);
            obj.WhereList.Add("送信日 = " + this.SendDate);
            obj.WhereList.Add("送信時間 = " + this.SendTime);

            sr = obj.UpdateSQL();

            return sr;
        }
*/
        public static StdReturn StatusChange(string rcv_staff, string send_staff, string send_date, string send_time, int status = 1)
        {
            StdReturn sr = new StdReturn();

            if (rcv_staff.Length == 0 || send_staff.Length == 0 || send_date.Length != 8 || send_time.Length == 0)
            {
                return sr;
            }

            StdDbClass obj = new StdDbClass();

            obj.Table = "DRUG_ADV_AGREE";
            obj.Db = DB.Db2;

            obj.DataList.Add(new StdDbColumn("ステータス", StdDbType.NUMBER, status));

            obj.WhereList.Add("受信者コード = " + rcv_staff);
            obj.WhereList.Add("送信者コード = " + send_staff);
            obj.WhereList.Add("送信日 = " + send_date);
            obj.WhereList.Add("送信時間 = " + send_time);

            sr = obj.UpdateSQL();

            return sr;
        }
    }
}
