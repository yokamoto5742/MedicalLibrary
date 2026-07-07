using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class DrugAdvDischarge
    {
        public int SEQ = 0;

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

        public int AdvDate = 0;
        public int AdmDate = 0;
        public int DisDate = 0;

        public int Dept = 0;

        public string DeptName
        {
            get
            {
                string s = "";

                if (Dict.DeptDict.ContainsKey(this.Dept.ToString()))
                {
                    s = Dict.DeptDict[this.Dept.ToString()].ShortName;
                }

                return s;
            }
        }

        public int Ins = 0;

        public string InsName
        {
            get
            {
                string s = "";

                if (this.Pat.InsDict.ContainsKey(this.Ins))
                {
                    s = this.Pat.InsDict[this.Ins].KindNameShort;
                }

                return s;
            }
        }

        public string Allergy1 = "";
        public string Allergy2 = "";
        public string Drug = "";
        public string Adv = "";
        public string StNote = "";
        public string PtNote1 = "";
        public string PtNote2 = "";
        public string PtNote3 = "";
       
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

        public int DelStaff = 0;

        public string DelStaffName
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(this.DelStaff.ToString()))
                {
                    s = Dict.StaffDict[this.DelStaff.ToString()].Name;
                }

                return s;
            }
        }

        public int DelDate = 0;
        public int DelTime = 0;

        public int Status = 0;

        public int PDFSave = 0;
        public int PDFDate = 0;
        public int PDFTime = 0;

        /// <summary>
        /// 変更可能かどうか
        /// PDF保存されている or 指導日から４日以上経過している or 退院日から４日以上経過している場合は不可
        /// </summary>
        public bool Editable
        {
            get
            {
                bool b = true;

                if (this.PDFSave.Equals(1))
                {
                    b = false;
                }
                else if (this.AdvDate <= int.Parse(DateTime.Now.AddDays(-4).ToString("yyyyMMdd")))
                {
                    b = false;
                }
                else if (this.DisDate <= int.Parse(DateTime.Now.AddDays(-4).ToString("yyyyMMdd")))
                {
                    b = false;
                }

                return b;
            }
        }

        public DrugAdvDischarge()
        {
        }

        public DrugAdvDischarge(StdClass tmp)
        {
            this.SEQ = tmp.GetDataInt("ID");
            this.PtId = tmp.GetDataString("PATIENT_ID");
            this.AdvDate = tmp.GetDataInt("ADV_DATE");
            this.AdmDate = tmp.GetDataInt("ADM_DATE");
            this.DisDate = tmp.GetDataInt("DIS_DATE");
            this.Dept = tmp.GetDataInt("DEPT");
            this.Ins = tmp.GetDataInt("INS");
            this.Allergy1 = tmp.GetDataString("ALLERGY1");
            this.Allergy2 = tmp.GetDataString("ALLERGY2");
            this.Drug = tmp.GetDataString("DRUG");
            this.Adv = tmp.GetDataString("ADV");
            this.StNote = tmp.GetDataString("ST_NOTE");
            this.PtNote1 = tmp.GetDataString("PT_NOTE1");
            this.PtNote2 = tmp.GetDataString("PT_NOTE2");
            this.PtNote3 = tmp.GetDataString("PT_NOTE3");
            this.Staff = tmp.GetDataInt("STAFF");
            this.SaveDate = tmp.GetDataInt("SAVE_DATE");
            this.SaveTime = tmp.GetDataInt("SAVE_TIME");
            this.DelStaff = tmp.GetDataInt("DEL_STAFF");
            this.DelDate = tmp.GetDataInt("DEL_DATE");
            this.DelTime = tmp.GetDataInt("DEL_TIME");
            this.Status = tmp.GetDataInt("STATUS");
            this.PDFSave = tmp.GetDataInt("PDF_SAVE");
            this.PDFDate = tmp.GetDataInt("PDF_DATE");
            this.PDFTime = tmp.GetDataInt("PDF_TIME");
        }

        public static List<DrugAdvDischarge> GetListById(string pt_id, string order_by = "")
        {
            List<DrugAdvDischarge> list = new List<DrugAdvDischarge>();

            if (pt_id.Length == 0) return list;

            string cmd = "select * from DRUG_ADV_DISCHARGE" +
                " where PATIENT_ID = " + pt_id;

            if (order_by.Length > 0)
            {
                cmd += " order by " + order_by;
            }

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(new DrugAdvDischarge(tmp));
            }

            return list;
        }

        public static List<DrugAdvDischarge> GetListByIds(List<string> pt_id_list, string order_by = "")
        {
            List<DrugAdvDischarge> list = new List<DrugAdvDischarge>();

            if (AppString.ConcatList(pt_id_list, ",").Length == 0) return list;

            string cmd = "select * from DRUG_ADV_DISCHARGE" +
                " where PATIENT_ID in (" + AppString.ConcatList(pt_id_list, ",") + ")";

            if (order_by.Length > 0)
            {
                cmd += " order by " + order_by;
            }

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(new DrugAdvDischarge(tmp));
            }

            return list;
        }
/*
        static int NextVal()
        {
            int i = 0;
            string cmd = "select DRUG_ADV_DISCHARGE_SEQ.nextval NV from DUAL";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                i = tmp.GetDataInt("NV");
                break;
            }

            return i;
        }
*/
        public StdReturn Save()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();

            obj.Table = "DRUG_ADV_DISCHARGE";
            obj.Db = DB.Db2;

            obj.DataList.Add(new StdDbColumn("PATIENT_ID", StdDbType.NUMBER, this.PtId));
            obj.DataList.Add(new StdDbColumn("ADV_DATE", StdDbType.NUMBER, this.AdvDate));
            obj.DataList.Add(new StdDbColumn("ADM_DATE", StdDbType.NUMBER, this.AdmDate));
            obj.DataList.Add(new StdDbColumn("DIS_DATE", StdDbType.NUMBER, this.DisDate));
            obj.DataList.Add(new StdDbColumn("DEPT", StdDbType.NUMBER, this.Dept));
            obj.DataList.Add(new StdDbColumn("INS", StdDbType.NUMBER, this.Ins));
            obj.DataList.Add(new StdDbColumn("ALLERGY1", StdDbType.VARCHAR2, this.Allergy1));
            obj.DataList.Add(new StdDbColumn("ALLERGY2", StdDbType.VARCHAR2, this.Allergy2));
            obj.DataList.Add(new StdDbColumn("DRUG", StdDbType.VARCHAR2, this.Drug));
            obj.DataList.Add(new StdDbColumn("ADV", StdDbType.VARCHAR2, this.Adv));
            obj.DataList.Add(new StdDbColumn("ST_NOTE", StdDbType.VARCHAR2, this.StNote));
            obj.DataList.Add(new StdDbColumn("PT_NOTE1", StdDbType.VARCHAR2, this.PtNote1));
            obj.DataList.Add(new StdDbColumn("PT_NOTE2", StdDbType.VARCHAR2, this.PtNote2));
            obj.DataList.Add(new StdDbColumn("PT_NOTE3", StdDbType.VARCHAR2, this.PtNote3));
            obj.DataList.Add(new StdDbColumn("STAFF", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("SAVE_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("SAVE_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, 1));
            obj.DataList.Add(new StdDbColumn("PDF_SAVE", StdDbType.NUMBER, 0));

            obj.WhereList.Add("ID = " + this.SEQ);

            sr = obj.UpdateSQL();

            if (sr.IntValue == 0)
            {
                obj.DataList.Add(new StdDbColumn("ID", StdDbType.TEXT, "DRUG_ADV_DISCHARGE_SEQ.nextval"));
                sr = obj.InsertSQL();
            }

            return sr;
        }

        public static StdReturn Delete(int seq)
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();

            obj.Table = "DRUG_ADV_DISCHARGE";
            obj.Db = DB.Db2;

            obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("DEL_STAFF", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("DEL_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("DEL_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));

            obj.WhereList.Add("ID = " + seq);

            sr = obj.UpdateSQL();

            return sr;
        }
    }
}
