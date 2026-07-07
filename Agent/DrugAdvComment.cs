using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class DrugAdvComment
    {
        public string PtId = "";

        PatBase _Pat = new PatBase();

        public PatBase Pat
        {
            get
            {
                if (!this._Pat.Id.Equals(this.PtId) || this._Pat.Name.Length == 0)
                {
                    this._Pat = PatBase.Load(this.PtId);
                }

                return this._Pat;
            }
        }

        /// <summary>
        /// 件名
        /// </summary>
        public string Title = "";

        /// <summary>
        /// 内容
        /// </summary>
        public string Cont = "";

        public int Staff = 0;

        public string StaffName
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(this.Staff.ToString()))
                {
                    s = Dict.StaffDict[this.Staff.ToString()].Name;
                }

                return s;
            }
        }

        public int SaveDate = 0;

        public int SaveTime = 0;

        public int Status = 1;

        public static DrugAdvComment GetFromStdClass(StdClass tmp)
        {
            DrugAdvComment obj = new DrugAdvComment();

            obj.PtId = tmp.GetDataString("PATIENT_ID");
            obj.Title = tmp.GetDataString("TITLE");
            obj.Cont = tmp.GetDataString("CONT");
            obj.Staff = tmp.GetDataInt("STAFF");
            obj.SaveDate = tmp.GetDataInt("SAVE_DATE");
            obj.SaveTime = tmp.GetDataInt("SAVE_TIME");
            obj.Status = tmp.GetDataInt("STATUS");

            return obj;
        }

        public static List<DrugAdvComment> GetListById(string pt_id, string order_by = "")
        {
            List<DrugAdvComment> list = new List<DrugAdvComment>();

            if (pt_id.Length == 0) return list;

            string cmd = "select * from DRUG_ADV_COMMENT" +
                " where PATIENT_ID = " + pt_id;

            if (order_by.Length > 0)
            {
                cmd += " order by " + order_by;
            }

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }

        public static List<DrugAdvComment> GetListByIds(List<string> pt_id_list, string order_by = "")
        {
            List<DrugAdvComment> list = new List<DrugAdvComment>();

            if (AppString.ConcatList(pt_id_list, ",").Length == 0) return list;

            string cmd = "select * from DRUG_ADV_COMMENT" +
                " where PATIENT_ID in (" + AppString.ConcatList(pt_id_list, ",") + ")";

            if (order_by.Length > 0)
            {
                cmd += " order by " + order_by;
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

            StdDbClass obj = new StdDbClass();

            obj.Table = "DRUG_ADV_COMMENT";
            obj.Db = DB.Db2;

            obj.DataList.Add(new StdDbColumn("PATIENT_ID", StdDbType.NUMBER, this.PtId));
            obj.DataList.Add(new StdDbColumn("TITLE", StdDbType.VARCHAR2, this.Title));
            obj.DataList.Add(new StdDbColumn("CONT", StdDbType.VARCHAR2, this.Cont));
            obj.DataList.Add(new StdDbColumn("STAFF", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("SAVE_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("SAVE_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, 1));

            obj.WhereList.Add("PATIENT_ID = " + this.PtId);
            obj.WhereList.Add("STAFF = " + this.Staff);
            obj.WhereList.Add("SAVE_DATE = " + this.SaveDate);
            obj.WhereList.Add("SAVE_TIME = " + this.SaveTime);

            sr = obj.UpdateSQL();

            if (sr.IntValue == 0)
            {
                sr = obj.InsertSQL();
            }

            return sr;
        }

        public StdReturn StatusChange(int status)
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();

            obj.Table = "DRUG_ADV_COMMENT";
            obj.Db = DB.Db2;

            obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, status));

            obj.WhereList.Add("PATIENT_ID = " + this.PtId);
            obj.WhereList.Add("STAFF = " + this.Staff);
            obj.WhereList.Add("SAVE_DATE = " + this.SaveDate);
            obj.WhereList.Add("SAVE_TIME = " + this.SaveTime);

            sr = obj.UpdateSQL();

            return sr;
        }
    }
}
