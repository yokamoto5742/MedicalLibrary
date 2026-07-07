using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class BringDrug
    {
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

        public int SEQ = 0;

        public int StartDate = 0;

        public int EndDate = 0;

        public string Cont = "";

        public string Info1
        {
            get
            {
                string s = "";

                if (this.StartDate > 0)
                {
                    s += DateTimeAgent.DateFormat(StartDate, DateTimeAgent.DateFormatKind.LONG);
                }

                s += "～";

                if (this.EndDate > 0)
                {
                    s += DateTimeAgent.DateFormat(EndDate, DateTimeAgent.DateFormatKind.LONG);
                }

                s += " " + this.Cont;

                return s;
            }
        }

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

        public int DelFlg = 0;

        public string Bikou1 = "";

        public string Bikou2 = "";


        public BringDrug(StdClass tmp)
        {
            this.PtId = tmp.GetDataString("患者コード");
            this.SEQ = tmp.GetDataInt("連番");
            this.StartDate = tmp.GetDataInt("開始日");
            this.EndDate = tmp.GetDataInt("終了日");
            this.Cont = tmp.GetDataString("名称");
            this.Staff = tmp.GetDataInt("入力者コード");
            this.SaveDate = tmp.GetDataInt("入力日");
            this.DelFlg = tmp.GetDataInt("削除フラグ");
            this.Bikou1 = tmp.GetDataString("BIKOU1");
            this.Bikou2 = tmp.GetDataString("BIKOU2");
        }


        public static List<BringDrug> GetListById(string pt_id)
        {
            List<BringDrug> list = new List<BringDrug>();

            if (pt_id.Length == 0) return list;
            string cmd = "select * from ADT_持参薬登録データ" +
                " where 患者コード = " + pt_id +
                " order by 開始日 desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(new BringDrug(tmp));
            }

            return list;
        }

        public static List<BringDrug> GetListByIds(List<string> pt_id_list)
        {
            List<BringDrug> list = new List<BringDrug>();

            if (pt_id_list.Count == 0) return list;

            string cmd = "select * from ADT_持参薬登録データ" +
                " where 患者コード in (" + AppString.ConcatList(pt_id_list, ",") + ")" +
                " order by 開始日 desc, 終了日 desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(new BringDrug(tmp));
            }

            return list;
        }
    }
}
