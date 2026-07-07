using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class DrugAdv
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

        public int InOut = 0;

        public int AdmDate = 0;

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

        public string Inject = "";

        public string Drug = "";

        public string BringDrug = "";

        public string DoubleDrug = "";

        public string TabooMix = "";

        public string Adv = "";

        public int Agree = 0;

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
        /// 該当のPDFファイル
        /// </summary>
        public PdfDoc PDFFile
        {
            get
            {
                PdfDoc obj = new PdfDoc();

                List<string> date_list = new List<string>();
                date_list.Add(this.AdvDate.ToString());

                Dictionary<string, Dictionary<string, PdfDoc>> dict = PdfDoc.GetDict(this.Pat.Id, date_list);

                foreach (Dictionary<string, PdfDoc> dict2 in dict.Values)
                {
                    foreach (PdfDoc p in dict2.Values)
                    {
                        if (!p.PdfCode.Equals("36321")) continue;
                        if (!p.PdfDate.Equals(this.SaveDate)) continue;
                        if (!p.PdfTime.Equals(this.SaveTime)) continue;

                        obj = p;
                        break;
                    }
                }

                return obj;
            }
        }

        /// <summary>
        /// 変更可能かどうか
        /// PDF保存されている or 指導日から４日以上経過している場合は不可
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

                return b;
            }
        }

        public DrugAdv()
        {
        }

        public DrugAdv(StdClass tmp, bool pat = true)
        {
            this.SEQ = tmp.GetDataInt("DRUG_ADV_ID");
            this.PtId = tmp.GetDataString("PATIENT_ID");
            this.AdvDate = tmp.GetDataInt("ADV_DATE");
            this.InOut = tmp.GetDataInt("IN_OUT");
            this.AdmDate = tmp.GetDataInt("ADM_DATE");
            this.Dept = tmp.GetDataInt("DEPT");
            this.Ins = tmp.GetDataInt("INS");
            this.Allergy1 = tmp.GetDataString("ALLERGY1");
            this.Allergy2 = tmp.GetDataString("ALLERGY2");
            this.Inject = tmp.GetDataString("INJECT");
            this.Drug = tmp.GetDataString("DRUG");
            this.BringDrug = tmp.GetDataString("BRING_DRUG");
            this.DoubleDrug = tmp.GetDataString("DOUBLE_DRUG");
            this.TabooMix = tmp.GetDataString("TABOO_MIX");
            this.Adv = tmp.GetDataString("ADV");
            this.Agree = tmp.GetDataInt("AGREE");
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

            if (pat)
            {
                this._Pat.Id = tmp.GetDataString("PATIENT_ID");
#if INNO
                this._Pat.Name = tmp.GetDataString("P_NAME").Trim();
                this._Pat.Kana = tmp.GetDataString("P_KANA").Trim();
                this._Pat.Sex = tmp.GetDataString("P_SEX");
                this._Pat.Birth = tmp.GetDataString("P_BIRTHDAY_AD");
#else
                this._Pat.Name = tmp.GetDataString("IM01RC_F04").Trim();
                this._Pat.Kana = tmp.GetDataString("IM01RC_F03").Trim();
                this._Pat.Sex = tmp.GetDataString("IM01RC_F05");
                this._Pat.Birth = tmp.GetDataString("IM01RC_F10");
#endif
            }
        }


        public static List<DrugAdv> GetListById(string pt_id, string order_by = "")
        {
            List<DrugAdv> list = new List<DrugAdv>();

            if (pt_id.Length == 0) return list;

            string cmd = "select * from DRUG_ADV" +
                " where PATIENT_ID = " + pt_id;

            if (order_by.Length > 0)
            {
                cmd += " order by " + order_by;
            }

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(new DrugAdv(tmp));
            }            

            return list;
        }


        public static List<DrugAdv> GetListByIds(List<string> pt_id_list, string order_by = "")
        {
            List<DrugAdv> list = new List<DrugAdv>();

            if (AppString.ConcatList(pt_id_list, ",").Length == 0) return list;

            string cmd = "select * from DRUG_ADV" +
                " where PATIENT_ID in (" + AppString.ConcatList(pt_id_list, ",") + ")";

            if (order_by.Length > 0)
            {
                cmd += " order by " + order_by;
            }

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(new DrugAdv(tmp));
            }

            return list;
        }

        public static List<DrugAdv> GetListByDates(string date1, string date2, string order_by = "", bool pat = true)
        {
            List<DrugAdv> list = new List<DrugAdv>();

            DateTime dt = DateTime.Now;
            string d1 = date1;
            string d2 = date2;

            string cmd = "select * from DRUG_ADV " +
                " where ADV_DATE >= " + date1 + " and ADV_DATE <= " + date2;

            if (pat)
            {
#if INNO
                cmd = "select td.*, tm.P_KANA, tm.P_NAME, tm.P_SEX, tm.P_BIRTHDAY_AD" +
                    " from DRUG_ADV td, M_PATIENT" + Env.DB_LINK + " tm" +
                    " where ADV_DATE >= " + date1 + " and ADV_DATE <= " + date2 +
                    " and td.PATIENT_ID = tm.P_ID";
#else
                cmd = "select td.*, tm.IM01RC_F03, tm.IM01RC_F04, tm.IM01RC_F05, tm.IM01RC_F10" +
                    " from DRUG_ADV td, IM01RC tm" +
                    " where ADV_DATE >= " + date1 + " and ADV_DATE <= " + date2 +
                    " and td.PATIENT_ID = tm.IM01RC_F01";
#endif
            }

            if (order_by.Length > 0)
            {
                cmd += " order by " + order_by;
            }

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(new DrugAdv(tmp, pat));
            }

            return list;
        }

        /// <summary>
        /// 内服・頓用・外用歴を取得する（通常の服薬指導）
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="date">施行予定日がこの日以降</param>
        /// <returns></returns>
        public static string GetDrugHistory1(string pt_id, string date)
        {
            string drug = "";
            List<string> cond_list = new List<string>();

#if INNO
            cond_list.Add("SHINKU in (21, 22, 23)");
            cond_list.Add("ORDER_DATE >= " + date);

            List<PatOrder> list = PatOrder.GetListByPatCond(pt_id, cond_list, "ORDER_DATE desc, ORDER_NO desc", true);
#else
            cond_list.Add("診療区分 in (21, 22, 23)");
            cond_list.Add("施行予定日 >= " + date);

            List<PatOrder> list = PatOrder.GetListByPatCond(pt_id, cond_list, "施行予定日 desc, オーダー番号 desc", true);
#endif

            string drug_type = "";
            int drug_count = 0;

            foreach (PatOrder obj in list)
            {
                if (drug_count < 10 && drug.Length < 900)
                {
                    drug_type = "";

                    if (obj.Shinku.Equals("21"))
                    {
                        drug_type = "内服";
                    }
                    else if (obj.Shinku.Equals("22"))
                    {
                        drug_type = "頓用";
                    }
                    else if (obj.Shinku.Equals("23"))
                    {
                        drug_type = "外用";
                    }

                    drug += DateTimeAgent.DateFormat(obj.SekouDate, DateTimeAgent.DateFormatKind.LONG) + " " + drug_type + " " + obj.DetailString + Environment.NewLine;
                    drug_count++;
                }
                else
                {
                    break;
                }
            }

            return drug;
        }

        /// <summary>
        /// 内服・頓用・外用歴を取得する（退院時服薬指導）
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="dis_date">退院日</param>
        /// <param name="date">施行予定日がこの日以降</param>
        /// <returns></returns>
        public static string GetDrugHistory2(string pt_id, string dis_date, string date)
        {
            string drug = "";
            List<string> cond_list = new List<string>();
#if INNO
            cond_list.Add("INOUT = 2");
            cond_list.Add("SHINKU in (14, 21, 22, 23)");
            cond_list.Add("ORDER_DATE >= " + date);

            List<PatOrder> list = PatOrder.GetListByPatCond(pt_id, cond_list, "ORDER_DATE desc, ORDER_NO desc", true);
#else
            cond_list.Add("入外区分 = 2");
            cond_list.Add("診療区分 in (14, 21, 22, 23)");
            cond_list.Add("施行予定日 >= " + date);

            List<PatOrder> list = PatOrder.GetListByPatCond(pt_id, cond_list, "施行予定日 desc, オーダー番号 desc", true);
#endif
            string drug_type = "";
            int drug_count = 0;
            bool write_flg = false;

            // 退院日
            int d_date = int.Parse(DateTime.Now.ToString("yyyyMMdd"));

            // dis_date が有効な日付であれば、その日を d_date にセットする
            if (DateTimeAgent.IsDate(dis_date)) int.TryParse(dis_date, out d_date);

            foreach (PatOrder obj in list)
            {
                if (drug_count < 10 && drug.Length < 900)
                {
                    drug_type = "";
                    write_flg = false;

                    if (obj.Shinku.Equals("14"))
                    {
                        drug_type = "自己注射";
                        write_flg = true;
                    }
                    else if (obj.Shinku.Equals("21"))
                    {
                        drug_type = "内服";

                        // 退院日以降であれば write
                        if (d_date <= int.Parse(obj.SekouDate))
                        {
                            write_flg = true;
                        }
                        else
                        {
                            write_flg = false;
                        }
                    }
                    else if (obj.Shinku.Equals("22"))
                    {
                        drug_type = "頓用";
                        write_flg = true;
                    }
                    else if (obj.Shinku.Equals("23"))
                    {
                        drug_type = "外用";
                        write_flg = true;
                    }

                    if (write_flg)
                    {
                        drug += DateTimeAgent.DateFormat(obj.SekouDate, DateTimeAgent.DateFormatKind.LONG) + " " + drug_type + " " + obj.DetailString + Environment.NewLine;
                        drug_count++;
                    }
                }
                else
                {
                    break;
                }
            }

            return drug;
        }

        /// <summary>
        /// 注射歴を取得する
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="date">施行予定日がこの日以降</param>
        /// <returns></returns>
        public static string GetInjectHistory(string pt_id, string date)
        {
            string inject = "";
            List<string> cond_list = new List<string>();

            Dictionary<string, string> injectDict = new Dictionary<string, string>();

#if INNO
            cond_list.Add("SHINKU in (31, 32, 33)");
            cond_list.Add("ORDER_DATE >= " + date);

            List<PatOrder> list = PatOrder.GetListByPatCond(pt_id, cond_list, "ORDER_DATE desc, ORDER_NO desc", true);
#else
            cond_list.Add("診療区分 in (31, 32, 33)");
            cond_list.Add("施行予定日 >= " + date);

            List<PatOrder> list = PatOrder.GetListByPatCond(pt_id, cond_list, "施行予定日 desc, オーダー番号 desc", true);
#endif

            string tmp_inject = "";
            string tmp_date = "";

            foreach (PatOrder obj in list)
            {
                tmp_inject = obj.DetailString;
                tmp_date = obj.SekouDate;

                if (injectDict.ContainsKey(tmp_inject) && tmp_date.Length == 8)
                {
                    injectDict[tmp_inject] = tmp_date.Insert(4, "/").Insert(7, "/") + ", " + injectDict[tmp_inject];
                }
                else
                {
                    injectDict.Add(tmp_inject, tmp_date.Insert(4, "/").Insert(7, "/"));
                }
            }

            int inject_count = 0;

            foreach (string s in injectDict.Keys)
            {
                if (inject_count < 10 && inject.Length < 900)
                {
                    tmp_inject = "";

                    if (s.Contains("注射,"))
                    {
                        tmp_inject = injectDict[s] + "\r\n" + s.Substring(s.IndexOf("注射,") + 3).Trim();
                    }
                    else if (s.Contains("注射，"))
                    {
                        tmp_inject = injectDict[s] + "\r\n" + s.Substring(s.IndexOf("注射，") + 3).Trim();
                    }
                    else if (s.Contains("点滴,"))
                    {
                        tmp_inject = injectDict[s] + "\r\n" + s.Substring(s.IndexOf("点滴,") + 3).Trim();
                    }
                    else if (s.Contains("点滴，"))
                    {
                        tmp_inject = injectDict[s] + "\r\n" + s.Substring(s.IndexOf("点滴，") + 3).Trim();
                    }
                    else
                    {
                        tmp_inject = injectDict[s] + "\r\n" + s.Trim();
                    }

                    inject += tmp_inject + "\r\n";
                    inject_count++;
                }
                else
                {
                    break;
                }
            }

            return inject;
        }

/*
        static int NextVal()
        {
            int i = 0;
            string cmd = "select DRUG_ADV_SEQ.nextval NV from DUAL";

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

            obj.Table = "DRUG_ADV";
            obj.Db = DB.Db2;

            obj.DataList.Add(new StdDbColumn("PATIENT_ID", StdDbType.NUMBER, this.PtId));
            obj.DataList.Add(new StdDbColumn("ADV_DATE", StdDbType.NUMBER, this.AdvDate));
            obj.DataList.Add(new StdDbColumn("IN_OUT", StdDbType.NUMBER, this.InOut));
            obj.DataList.Add(new StdDbColumn("ADM_DATE", StdDbType.NUMBER, this.AdmDate));
            obj.DataList.Add(new StdDbColumn("DEPT", StdDbType.NUMBER, this.Dept));
            obj.DataList.Add(new StdDbColumn("INS", StdDbType.NUMBER, this.Ins));
            obj.DataList.Add(new StdDbColumn("ALLERGY1", StdDbType.VARCHAR2, this.Allergy1));
            obj.DataList.Add(new StdDbColumn("ALLERGY2", StdDbType.VARCHAR2, this.Allergy2));
            obj.DataList.Add(new StdDbColumn("INJECT", StdDbType.VARCHAR2, this.Inject));
            obj.DataList.Add(new StdDbColumn("DRUG", StdDbType.VARCHAR2, this.Drug));
            obj.DataList.Add(new StdDbColumn("BRING_DRUG", StdDbType.VARCHAR2, this.BringDrug));
            obj.DataList.Add(new StdDbColumn("DOUBLE_DRUG", StdDbType.VARCHAR2, this.DoubleDrug));
            obj.DataList.Add(new StdDbColumn("TABOO_MIX", StdDbType.VARCHAR2, this.TabooMix));
            obj.DataList.Add(new StdDbColumn("ADV", StdDbType.VARCHAR2, this.Adv));
            obj.DataList.Add(new StdDbColumn("AGREE", StdDbType.NUMBER, this.Agree));
            obj.DataList.Add(new StdDbColumn("STAFF", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("SAVE_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("SAVE_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, 1));
            obj.DataList.Add(new StdDbColumn("PDF_SAVE", StdDbType.NUMBER, 0));

            obj.WhereList.Add("DRUG_ADV_ID = " + this.SEQ);

            sr = obj.UpdateSQL();

            if (sr.IntValue == 0)
            {
                obj.DataList.Add(new StdDbColumn("DRUG_ADV_ID", StdDbType.TEXT, "DRUG_ADV_SEQ.nextval"));
                sr = obj.InsertSQL();
            }

            return sr;
        }

        public static StdReturn Delete(int seq)
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();

            obj.Table = "DRUG_ADV";
            obj.Db = DB.Db2;

            obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, 0));
            obj.DataList.Add(new StdDbColumn("DEL_STAFF", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("DEL_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("DEL_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));

            obj.WhereList.Add("DRUG_ADV_ID = " + seq);

            sr = obj.UpdateSQL();

            return sr;
        }
    }
}
