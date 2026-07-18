using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class EyeSummary
    {
        public string PtId = "";
        public string Diag = "";
        public string Kind1 = "";
        public string Kind2 = "";
        public string Kind3 = "";
        public string Plan = "";
        public string Pass = "";
        public string Hist = "";
        public string Cont1 = "";
        public string Cont2 = "";
        public string Cont3 = "";
        public string Cont4 = "";
        public string Staff = "";
        public string SaveDate = "";
        public string SaveTime = "";

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
        /// データベースに保存する。
        /// </summary>
        public void Save()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
            obj.Db = DB.Db2;

            obj.Table = "EYE_SUMMARY";

            obj.DataList.Add(new StdDbColumn("DIAG", StdDbType.VARCHAR2, this.Diag));
            obj.DataList.Add(new StdDbColumn("KIND1", StdDbType.VARCHAR2, this.Kind1));
            obj.DataList.Add(new StdDbColumn("KIND2", StdDbType.VARCHAR2, this.Kind2));
            obj.DataList.Add(new StdDbColumn("KIND3", StdDbType.VARCHAR2, this.Kind3));
            obj.DataList.Add(new StdDbColumn("PLAN", StdDbType.VARCHAR2, this.Plan));
            obj.DataList.Add(new StdDbColumn("PASS", StdDbType.VARCHAR2, this.Pass));
            obj.DataList.Add(new StdDbColumn("HIST", StdDbType.VARCHAR2, this.Hist));
            obj.DataList.Add(new StdDbColumn("CONT1", StdDbType.VARCHAR2, this.Cont1));
            obj.DataList.Add(new StdDbColumn("CONT2", StdDbType.VARCHAR2, this.Cont2));
            obj.DataList.Add(new StdDbColumn("CONT3", StdDbType.VARCHAR2, this.Cont3));
            obj.DataList.Add(new StdDbColumn("CONT4", StdDbType.VARCHAR2, this.Cont4));
            obj.DataList.Add(new StdDbColumn("STAFF", StdDbType.NUMBER, this.Staff));
            obj.DataList.Add(new StdDbColumn("SAVE_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("SAVE_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.WhereList.Add("PATIENT_ID = " + this.PtId);

            sr = obj.UpdateSQL();

            // update 対象が無ければ新規登録
            if (sr.IntValue == 0)
            {
                obj.DataList.Add(new StdDbColumn("PATIENT_ID", StdDbType.NUMBER, this.PtId));
                sr = obj.InsertSQL();
            }
        }

        static EyeSummary GetFromStdClass(StdClass tmp)
        {
            EyeSummary sum = new EyeSummary();

            sum.PtId = tmp.DataDict["PATIENT_ID"].ToString();
            sum.Diag = tmp.DataDict["DIAG"].ToString();
            sum.Kind1 = tmp.DataDict["KIND1"].ToString();
            sum.Kind2 = tmp.DataDict["KIND2"].ToString();
            sum.Kind3 = tmp.DataDict["KIND3"].ToString();
            sum.Plan = tmp.DataDict["PLAN"].ToString();
            sum.Pass = tmp.DataDict["PASS"].ToString();
            sum.Hist = tmp.DataDict["HIST"].ToString();
            sum.Cont1 = tmp.DataDict["CONT1"].ToString();
            sum.Cont2 = tmp.DataDict["CONT2"].ToString();
            sum.Cont3 = tmp.DataDict["CONT3"].ToString();
            sum.Cont4 = tmp.DataDict["CONT4"].ToString();
            sum.Staff = tmp.DataDict["STAFF"].ToString();
            sum.SaveDate = tmp.DataDict["SAVE_DATE"].ToString();
            sum.SaveTime = tmp.DataDict["SAVE_TIME"].ToString();

            return sum;
        }

        /// <summary>
        /// データベースからロードする。
        /// </summary>
        /// <param name="patient_id"></param>
        /// <returns></returns>
        public static EyeSummary Load(string patient_id)
        {
            EyeSummary sum = new EyeSummary();

            if (patient_id.Length == 0)
            {
                return sum;
            }

            string cmd = "select * from EYE_SUMMARY where PATIENT_ID = " + patient_id;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                sum = GetFromStdClass(tmp);
                break;
            }

            return sum;
        }

        /// <summary>
        /// すべて取得する
        /// </summary>
        /// <returns></returns>
        public static List<EyeSummary> LoadAll()
        {
            List<EyeSummary> list = new List<EyeSummary>();

            string cmd = "select EYE_SUMMARY.*, Trim(tm.P_NAME) as 氏名 " +
                " from EYE_SUMMARY " +
                " inner join M_PATIENT" + Env.DB_LINK + " tm on PATIENT_ID = tm.P_ID";
            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                EyeSummary obj = GetFromStdClass(tmp);

                obj._Pat.Id = tmp.GetDataString("PATIENT_ID");
                obj._Pat.Name = tmp.GetDataString("氏名").TrimEnd();

                list.Add(obj);
            }

            return list;
        }

        /// <summary>
        /// 患者コードのリストで検索する。
        /// </summary>
        /// <param name="pt_list"></param>
        /// <returns></returns>
        public static List<EyeSummary> GetListByPats(List<string> pt_list)
        {
            List<EyeSummary> list = new List<EyeSummary>();

            // ORA-01795対策: IN句の上限が1000件のため分割して取得する
            for (int start = 0; start < pt_list.Count; start += 1000)
            {
                List<string> part_list = pt_list.GetRange(start, Math.Min(1000, pt_list.Count - start));

                if (AppString.ConcatList(part_list, ",").Length == 0)
                {
                    continue;
                }

                string cmd = "select * from EYE_SUMMARY where PATIENT_ID in (" + AppString.ConcatList(part_list, ",") + ")";

                List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    list.Add(GetFromStdClass(tmp));
                }
            }

            return list;
        }

        /// <summary>
        /// 検査日を指定して取得する
        /// </summary>
        /// <param name="kensa_date"></param>
        /// <returns></returns>
        public static List<EyeSummary> GetListByKensaDate(string kensa_date)
        {
            List<EyeSummary> list = new List<EyeSummary>();

            string cmd = "select EYE_SUMMARY.*, Trim(tm.P_NAME) as 氏名 " +
                " from EYE_SUMMARY " +
                " inner join M_PATIENT" + Env.DB_LINK + " tm on PATIENT_ID = tm.P_ID " +
                " where CONT2 like '%," + kensa_date + " %' order by PATIENT_ID";
            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                EyeSummary obj = GetFromStdClass(tmp);

                obj._Pat.Id = tmp.GetDataString("PATIENT_ID");
                obj._Pat.Name = tmp.GetDataString("氏名").TrimEnd();

                list.Add(obj);
            }

            return list;
        }

        /// <summary>
        /// データベースから検索する。
        /// </summary>
        /// <param name="diag">主病名</param>
        /// <param name="kind1">分類1</param>
        /// <param name="kind2">分類2</param>
        /// <param name="kind3">分類3</param>
        /// <param name="limit">取得件数の上限（0は無制限）</param>
        /// <param name="db">眼科DBへの接続（省略時は DB.Db2）</param>
        /// <param name="pat_db">患者マスタDBへの接続（省略時は DB.Db3）</param>
        /// <returns></returns>
        public static List<EyeSummary> Find(string diag, string kind1, string kind2, string kind3, int limit = 0, DB db = null, DB pat_db = null)
        {
            List<EyeSummary> list = new List<EyeSummary>();

            string param = "";

            // 病名をスペースで区切って複数入力した場合に and 検索が出来るように。
            // 舘先生・岡本康宏さんの要望, by sakane, 14/03/03
            if (diag.Length > 0)
            {
                string[] ss = diag.Replace('　', ' ').Split(' ');

                foreach (string s in ss)
                {
                    if (param.Length > 0)
                    {
                        param += " and ";
                    }

                    param += "DIAG like '%" + s + "%'";
                }
            }

            if (kind1.Length > 0)
            {
                if (param.Length > 0)
                {
                    param += " and ";
                }

                param += "KIND1 like '%" + kind1 + "%'";
            }

            if (kind2.Length > 0)
            {
                if (param.Length > 0)
                {
                    param += " and ";
                }

                param += "KIND2 like '%" + kind2 + "%'";
            }

            if (kind3.Length > 0)
            {
                if (param.Length > 0)
                {
                    param += " and ";
                }

                param += "KIND3 like '%" + kind3 + "%'";
            }

            if (param.Length == 0)
            {
                return list;
            }
            // 患者マスタ（DBリンク先）とは結合せず、眼科DBだけで検索する。
            // 患者情報は検索結果の患者IDからまとめて取得する（DBリンク越しの結合による負荷・ハング対策）。
            string cmd = "select * from EYE_SUMMARY where " + param + " order by PATIENT_ID";

            if (limit > 0)
            {
                cmd = "select * from (" + cmd + ") where ROWNUM <= " + limit;
            }

            List<StdClass> tmp_list = StdClass.GetList(db == null ? DB.Db2 : db, cmd);

            List<string> pt_list = new List<string>();
            HashSet<string> pt_set = new HashSet<string>();

            foreach (StdClass tmp in tmp_list)
            {
                string pt_id = tmp.GetDataString("PATIENT_ID");

                if (pt_set.Add(pt_id))
                {
                    pt_list.Add(pt_id);
                }
            }

            Dictionary<string, PatBase> pat_dict = PatBase.GetDict(pt_list, pat_db);

            foreach (StdClass tmp in tmp_list)
            {
                EyeSummary sum = GetFromStdClass(tmp);

                string pt_id = tmp.GetDataString("PATIENT_ID");

                // 患者マスタに存在しないIDは除外する（従来の inner join と同じ扱い）
                if (!pat_dict.ContainsKey(pt_id))
                {
                    continue;
                }

                sum._Pat = pat_dict[pt_id];

                list.Add(sum);
            }

            return list;
        }
    }
}
