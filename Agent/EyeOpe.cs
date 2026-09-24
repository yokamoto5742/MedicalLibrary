using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class EyeOpe
    {
        protected PatBase _Pat = new PatBase();

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

        public string Id = "";
        public string PtId = "";
        public string OpeDate = "";
        public string OpeTime = "0";
        public string OpeKind = "";
        public string OpeRoom = "";
        public string OpeName = "";
        public string Doctor = "";
        public string PlanTime = "";
        public string Anes = "";
        public string Diag = "";
        public string InOut = "";
        public string InRoom = "";
        public string InDate = "";
        public string InTime = "";
        public string InTerm = "";
        public string EyeR = "0";
        public string EyeL = "0";
        public string Height = "";
        public string Weight = "";
        public string Infection = "";
        public string PostDeal = "";
        public string Past = "";
        public string Comment = "";
        public string AllCheck = "0";
        public string Explain = "0";
        public string EyeDrop = "0";

        /// <summary>
        /// 禁忌確認
        /// （当初は「同意書渡し済み」だったが、同意書システム稼働と同時に必要なくなったため「禁忌確認」に用途変更 2010/08/29）
        /// </summary>
        public string Agree = "0";

        /// <summary>
        /// 術前チェック
        /// </summary>
        public string PreCheck = "0";

        /// <summary>
        /// 短期滞在手術等基本料3
        /// </summary>
        public string ShortOpe3 = "0";

        /// <summary>
        /// 手術日を早めても可
        /// </summary>
        public string EarlierOK = "0";

        public string Staff = "";
        public string Status = "1";

        /// <summary>
        /// 記録（EYE_OPE_RECORD と結合する場合）
        /// </summary>
        public string OpeRecord = "";

        /// <summary>
        /// 経過（EYE_OPE_PASS と結合する場合）
        /// </summary>
        public string OpePass = "";

        /// <summary>
        /// 適切な値が入っているかをチェックする。
        /// </summary>
        private bool DataCheck()
        {
            if (PtId.Length == 0) return false;

            if (OpeDate.Length != 8) return false;

            if (OpeTime.Length > 6) return false;

            return true;
        }

        /// <summary>
        /// データベースに保存する。
        /// </summary>
        public void Save()
        {
            if (!DataCheck()) return;

            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
            obj.Db = DB.Db2;

            obj.Table = "EYE_OPE";

            obj.DataList.Add(new StdDbColumn("PATIENT_ID", StdDbType.NUMBER, this.PtId));
            obj.DataList.Add(new StdDbColumn("OPE_DATE", StdDbType.NUMBER, this.OpeDate));

            if (OpeTime.Length > 0)
            {
                obj.DataList.Add(new StdDbColumn("OPE_TIME", StdDbType.NUMBER, this.OpeTime));
            }
            else
            {
                obj.DataList.Add(new StdDbColumn("OPE_TIME", StdDbType.NUMBER, DBNull.Value));
            }

            obj.DataList.Add(new StdDbColumn("OPE_KIND", StdDbType.NUMBER, this.OpeKind));
            obj.DataList.Add(new StdDbColumn("OPE_ROOM", StdDbType.VARCHAR2, this.OpeRoom));
            obj.DataList.Add(new StdDbColumn("OPE_NAME", StdDbType.VARCHAR2, this.OpeName));
            obj.DataList.Add(new StdDbColumn("DOCTOR", StdDbType.VARCHAR2, this.Doctor));

            if (PlanTime.Length > 0)
            {
                obj.DataList.Add(new StdDbColumn("PLAN_TIME", StdDbType.NUMBER, this.PlanTime));
            }
            else
            {
                obj.DataList.Add(new StdDbColumn("PLAN_TIME", StdDbType.NUMBER, DBNull.Value));
            }

            obj.DataList.Add(new StdDbColumn("ANES", StdDbType.VARCHAR2, this.Anes));
            obj.DataList.Add(new StdDbColumn("DIAG", StdDbType.VARCHAR2, this.Diag));
            obj.DataList.Add(new StdDbColumn("IN_OUT", StdDbType.VARCHAR2, this.InOut));

            if (InOut.Contains("外来"))
            {
                obj.DataList.Add(new StdDbColumn("IN_ROOM", StdDbType.VARCHAR2, DBNull.Value));
                obj.DataList.Add(new StdDbColumn("IN_DATE", StdDbType.NUMBER, DBNull.Value));
                obj.DataList.Add(new StdDbColumn("IN_TIME", StdDbType.NUMBER, DBNull.Value));
                obj.DataList.Add(new StdDbColumn("IN_TERM", StdDbType.VARCHAR2, DBNull.Value));
            }
            else
            {
                obj.DataList.Add(new StdDbColumn("IN_ROOM", StdDbType.VARCHAR2, this.InRoom));

                if (InDate.Length > 0)
                {
                    obj.DataList.Add(new StdDbColumn("IN_DATE", StdDbType.NUMBER, InDate));
                }
                else
                {
                    obj.DataList.Add(new StdDbColumn("IN_DATE", StdDbType.NUMBER, DBNull.Value));
                }

                if (InTime.Length > 0)
                {
                    obj.DataList.Add(new StdDbColumn("IN_TIME", StdDbType.NUMBER, this.InTime));
                }
                else
                {
                    obj.DataList.Add(new StdDbColumn("IN_TIME", StdDbType.NUMBER, DBNull.Value));
                }

                obj.DataList.Add(new StdDbColumn("IN_TERM", StdDbType.VARCHAR2, this.InTerm));
            }

            obj.DataList.Add(new StdDbColumn("EYE_R", StdDbType.NUMBER, this.EyeR));
            obj.DataList.Add(new StdDbColumn("EYE_L", StdDbType.NUMBER, this.EyeL));
            obj.DataList.Add(new StdDbColumn("HEIGHT", StdDbType.VARCHAR2, this.Height));
            obj.DataList.Add(new StdDbColumn("WEIGHT", StdDbType.VARCHAR2, this.Weight));
            obj.DataList.Add(new StdDbColumn("INFECTION", StdDbType.VARCHAR2, this.Infection));
            obj.DataList.Add(new StdDbColumn("POST_DEAL", StdDbType.VARCHAR2, this.PostDeal));
            obj.DataList.Add(new StdDbColumn("PAST", StdDbType.VARCHAR2, this.Past));
            obj.DataList.Add(new StdDbColumn("COMT", StdDbType.VARCHAR2, this.Comment));
            obj.DataList.Add(new StdDbColumn("ALL_CHECK", StdDbType.NUMBER, this.AllCheck));
            obj.DataList.Add(new StdDbColumn("EXPLAIN", StdDbType.NUMBER, this.Explain));
            obj.DataList.Add(new StdDbColumn("EYE_DROP", StdDbType.NUMBER, this.EyeDrop));
            obj.DataList.Add(new StdDbColumn("AGREE", StdDbType.NUMBER, this.Agree));
            obj.DataList.Add(new StdDbColumn("PRE_CHECK", StdDbType.NUMBER, this.PreCheck));
            obj.DataList.Add(new StdDbColumn("SHORT_OPE3", StdDbType.NUMBER, this.ShortOpe3));
            obj.DataList.Add(new StdDbColumn("EARLIER_OK", StdDbType.NUMBER, this.EarlierOK));
            obj.DataList.Add(new StdDbColumn("STAFF", StdDbType.NUMBER, this.Staff));
            obj.DataList.Add(new StdDbColumn("SAVE_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("SAVE_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("STATUS", StdDbType.NUMBER, this.Status));

            if (Id.Length > 0)
            {
                obj.WhereList.Add("ID = " + this.Id);
                sr = obj.UpdateSQL();
            }
            else
            {
                obj.DataList.Add(new StdDbColumn("ID", StdDbType.TEXT, "EYE_OPE_SEQ.nextval"));
                sr = obj.InsertSQL();
            }
        }

        public static EyeOpe GetFromStdClass(StdClass tmp)
        {
            EyeOpe obj = new EyeOpe();

            obj.Id = tmp.DataDict["ID"].ToString();
            obj.PtId = tmp.DataDict["PATIENT_ID"].ToString();
            obj.OpeDate = tmp.DataDict["OPE_DATE"].ToString();
            obj.OpeTime = tmp.DataDict["OPE_TIME"].ToString();
            obj.OpeKind = tmp.DataDict["OPE_KIND"].ToString();
            obj.OpeRoom = tmp.DataDict["OPE_ROOM"].ToString();
            obj.OpeName = tmp.DataDict["OPE_NAME"].ToString();
            obj.Doctor = tmp.DataDict["DOCTOR"].ToString();
            obj.PlanTime = tmp.DataDict["PLAN_TIME"].ToString();
            obj.Anes = tmp.DataDict["ANES"].ToString();
            obj.Diag = tmp.DataDict["DIAG"].ToString();
            obj.InOut = tmp.DataDict["IN_OUT"].ToString();
            obj.InRoom = tmp.GetDataString("IN_ROOM");
            obj.InDate = tmp.DataDict["IN_DATE"].ToString();
            obj.InTime = tmp.DataDict["IN_TIME"].ToString();
            obj.InTerm = tmp.DataDict["IN_TERM"].ToString();
            obj.EyeR = tmp.DataDict["EYE_R"].ToString();
            obj.EyeL = tmp.DataDict["EYE_L"].ToString();
            obj.Height = tmp.DataDict["HEIGHT"].ToString();
            obj.Weight = tmp.DataDict["WEIGHT"].ToString();
            obj.Infection = tmp.DataDict["INFECTION"].ToString();
            obj.PostDeal = tmp.DataDict["POST_DEAL"].ToString();
            obj.Past = tmp.DataDict["PAST"].ToString();
            obj.Comment = tmp.DataDict["COMT"].ToString();
            obj.AllCheck = tmp.DataDict["ALL_CHECK"].ToString();
            obj.Explain = tmp.DataDict["EXPLAIN"].ToString();
            obj.EyeDrop = tmp.DataDict["EYE_DROP"].ToString();
            obj.Agree = tmp.DataDict["AGREE"].ToString();
            obj.PreCheck = tmp.DataDict["PRE_CHECK"].ToString();
            obj.ShortOpe3 = tmp.DataDict["SHORT_OPE3"].ToString();
            obj.EarlierOK = tmp.DataDict["EARLIER_OK"].ToString();
            obj.Staff = tmp.DataDict["STAFF"].ToString();
            obj.Status = tmp.DataDict["STATUS"].ToString();

            return obj;
        }

        /// <summary>
        /// データベースからロードする。
        /// </summary>
        public static EyeOpe Load(string ope_id)
        {
            EyeOpe tmpOpe = new EyeOpe();

            if (ope_id.Length == 0)
            {
                return tmpOpe;
            }

            string cmd = "select * from EYE_OPE where ID = " + ope_id;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                tmpOpe = GetFromStdClass(tmp);
                break;
            }

            return tmpOpe;
        }

        /// <summary>
        /// データベースから削除する。
        /// </summary>
        /// <param name="ope_id">手術記録ID</param>
        /// <param name="del_staff">ログインユーザーID</param>
        public static void Delete(string ope_id, string del_staff)
        {
            if (ope_id.Length == 0 || del_staff.Length == 0)
            {
                return;
            }

            string cmd = "update EYE_OPE set STATUS = 0, DEL_STAFF = " + del_staff + ", DEL_DATE = " + DateTime.Now.ToString("yyyyMMdd") + ", DEL_TIME = " + DateTime.Now.ToString("HHmmss") +
                " where ID = " + ope_id;

            DB.Db2.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 種別・日付・時間帯を指定して取得する
        /// </summary>
        /// <param name="kind"></param>
        /// <param name="date"></param>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <returns></returns>
        public static List<EyeOpe> GetListByKindDateTimes(string kind, string date, string time1, string time2)
        {
            List<EyeOpe> list = new List<EyeOpe>();

            if (!DateTimeAgent.IsDate(date))
            {
                return list;
            }

            List<string> cond_list = new List<string>();

            cond_list.Add("OPE_DATE = " + date);

            if (kind.Length > 0)
            {
                cond_list.Add("OPE_KIND = " + kind);
            }

            if (time1.Length > 0)
            {
                cond_list.Add("OPE_TIME >= " + time1);
            }

            if (time2.Length > 0)
            {
                cond_list.Add("OPE_TIME <= " + time2);
            }

            return GetListByConds(cond_list);
        }

        /// <summary>
        /// 種別・開始日・終了日を指定して取得する
        /// </summary>
        /// <param name="kind"></param>
        /// <param name="start_date"></param>
        /// <param name="end_date"></param>
        /// <returns></returns>
        public static List<EyeOpe> GetListByKindDates(string kind, string start_date, string end_date)
        {
            List<EyeOpe> list = new List<EyeOpe>();

            if (!DateTimeAgent.IsDate(start_date) || !DateTimeAgent.IsDate(end_date))
            {
                return list;
            }

            List<string> cond_list = new List<string>();

            if (kind.Length > 0)
            {
                cond_list.Add("OPE_KIND = " + kind);
            }

            cond_list.Add("OPE_DATE >= " + start_date);
            cond_list.Add("OPE_DATE <= " + end_date);

            return GetListByConds(cond_list);
        }

        /// <summary>
        /// GetListByKindDateTimes / GetListByKindDates の共通部分
        /// </summary>
        /// <param name="cond_list">Where 句の条件</param>
        /// <returns></returns>
        static List<EyeOpe> GetListByConds(List<string> cond_list)
        {
            List<EyeOpe> list = new List<EyeOpe>();

            // 患者マスタ（DBリンク先）とは結合せず、眼科DB単独で検索する。
            // 患者情報は検索結果の患者IDからまとめて取得する（DBリンク越しの結合による負荷・ハング対策）。
            string cmd = "select * from EYE_OPE " +
                " where " + AppString.ConcatList(cond_list, " and ") + " and STATUS != 0 " +
                " order by OPE_KIND, OPE_DATE, OPE_TIME";
            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            Dictionary<string, PatBase> pat_dict = PatBase.GetDict(tmp_list);

            foreach (StdClass tmp in tmp_list)
            {
                EyeOpe obj = GetFromStdClass(tmp);

                obj._Pat.Id = tmp.GetDataString("PATIENT_ID");

                // 患者マスタに存在するIDのみ患者情報を設定する（従来の left join と同一挙動）
                if (pat_dict.ContainsKey(obj.PtId))
                {
                    obj._Pat = pat_dict[obj.PtId];
                }

                list.Add(obj);
            }

            return list;
        }

        /// <summary>
        /// 条件に合致する手術記録をデータベースから検索する。
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="start_date"></param>
        /// <param name="end_date"></param>
        /// <returns></returns>
        public static List<EyeOpe> GetListByPatDates(string pt_id, string start_date, string end_date)
        {
            List<EyeOpe> tmpList = new List<EyeOpe>();

            int i = 0;

            if (pt_id.Length == 0 || !int.TryParse(pt_id, out i))
            {
                return tmpList;
            }

            string pt_sql = " and PATIENT_ID = " + pt_id;

            string date_sql = "";

            if (start_date.Length == 8 && end_date.Length == 8)
            {
                date_sql = " and OPE_DATE >= " + start_date + " and OPE_DATE <= " + end_date;
            }

            string cmd = "select EYE_OPE.*, EYE_OPE_RECORD.CONT as 記録 " +
                " from EYE_OPE left join EYE_OPE_RECORD on EYE_OPE.ID = EYE_OPE_RECORD.ID " +
                " where EYE_OPE.STATUS != 0 " + pt_sql + date_sql +
                " order by OPE_DATE desc, OPE_TIME desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                EyeOpe obj = GetFromStdClass(tmp);

                obj.OpeRecord = tmp.GetDataString("記録");

                tmpList.Add(obj);
            }

            return tmpList;
        }

        /// <summary>
        /// 条件に合致する手術記録をデータベースから検索する。
        /// </summary>
        /// <param name="start_date"></param>
        /// <param name="end_date"></param>
        /// <param name="diag"></param>
        /// <param name="ope"></param>
        /// <param name="doctor"></param>
        /// <param name="record11"></param>
        /// <param name="record12"></param>
        /// <param name="record13"></param>
        /// <param name="record21"></param>
        /// <param name="record22"></param>
        /// <param name="record23"></param>
        /// <param name="limit">取得件数の上限（0は無制限）</param>
        /// <param name="db">眼科DBへの接続（省略時は DB.Db2）</param>
        /// <param name="pat_db">患者マスタDBへの接続（省略時は DB.Db3）</param>
        /// <param name="progress">進捗の通知先（省略可）</param>
        /// <returns></returns>
        public static List<EyeOpe> GetList(string start_date, string end_date, string diag, string ope, string doctor, string record11, string record12, string record13, string record21, string record22, string record23, int limit = 0, DB db = null, DB pat_db = null, Action<string> progress = null)
        {
            List<EyeOpe> tmpList = new List<EyeOpe>();

            // 入力された文字列は SQL に直接埋め込まず、バインド変数で渡す
            // （パラメータ名は登録順の番号で一意にする）
            List<StdDbColumn> param_list = new List<StdDbColumn>();

            string date_sql = "";

            if (start_date.Length == 8 && end_date.Length == 8)
            {
                date_sql = " and OPE_DATE >= :START_DATE and OPE_DATE <= :END_DATE";
                param_list.Add(new StdDbColumn("START_DATE", StdDbType.NUMBER, start_date));
                param_list.Add(new StdDbColumn("END_DATE", StdDbType.NUMBER, end_date));
            }

            string diag_sql = "";

            if (diag.Length > 0)
            {
                foreach (string s in diag.Split(' ', '　'))
                {
                    if (diag_sql.Length > 0)
                    {
                        diag_sql += " and ";
                    }

                    string p = "P" + param_list.Count;
                    diag_sql += "DIAG like :" + p;
                    param_list.Add(new StdDbColumn(p, StdDbType.VARCHAR2, "%" + s + "%"));
                }

                if (diag_sql.Length > 0)
                {
                    diag_sql = " and (" + diag_sql + ")";
                }
            }

            string ope_sql = "";

            if (ope.Length > 0)
            {
                foreach (string s in ope.Split(new[] { ' ', '　' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (ope_sql.Length > 0)
                    {
                        ope_sql += " or ";
                    }

                    string p = "P" + param_list.Count;
                    ope_sql += "OPE_NAME like :" + p;
                    param_list.Add(new StdDbColumn(p, StdDbType.VARCHAR2, "%" + s + "%"));
                }

                if (ope_sql.Length > 0)
                {
                    ope_sql = " and (" + ope_sql + ")";
                }
            }

            string doctor_sql = "";

            if (doctor.Length > 0)
            {
                foreach (string s in doctor.Split(new[] { ' ', '　' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (doctor_sql.Length > 0)
                    {
                        doctor_sql += " or ";
                    }

                    string p = "P" + param_list.Count;
                    doctor_sql += "DOCTOR like :" + p;
                    param_list.Add(new StdDbColumn(p, StdDbType.VARCHAR2, "%" + s + "%"));
                }

                if (doctor_sql.Length > 0)
                {
                    doctor_sql = " and (" + doctor_sql + ")";
                }
            }

            string record_sql1 = "";

            if (record11.Length > 0)
            {
                record_sql1 = " and EYE_OPE_RECORD.CONT like :RECORD1";
                param_list.Add(new StdDbColumn("RECORD1", StdDbType.VARCHAR2, "%" + record12 + "," + record13 + "%"));
            }

            string record_sql2 = "";

            if (record21.Length > 0)
            {
                record_sql2 = " and EYE_OPE_RECORD.CONT like :RECORD2";
                param_list.Add(new StdDbColumn("RECORD2", StdDbType.VARCHAR2, "%" + record22 + "," + record23 + "%"));
            }
            // 患者マスタ（DBリンク先）とは結合せず、眼科DBだけで検索する。
            // 患者情報は検索結果の患者IDからまとめて取得する（DBリンク越しの結合による負荷・ハング対策）。
            string cmd = "select EYE_OPE.*, EYE_OPE_RECORD.CONT as REC_CONT, EYE_OPE_PASS.CONT as PASS_CONT " +
                " from EYE_OPE left join EYE_OPE_RECORD on EYE_OPE.ID = EYE_OPE_RECORD.ID left join EYE_OPE_PASS on EYE_OPE.ID = EYE_OPE_PASS.ID " +
                " where EYE_OPE.STATUS != 0 " + date_sql + diag_sql + ope_sql + doctor_sql + record_sql1 + record_sql2 +
                " order by OPE_DATE desc, OPE_TIME desc";

            if (limit > 0)
            {
                cmd = "select * from (" + cmd + ") where ROWNUM <= " + limit;
            }

            List<StdClass> tmp_list = StdClass.GetList(db == null ? DB.Db2 : db, cmd, param_list, progress);

            Dictionary<string, PatBase> pat_dict = PatBase.GetDict(tmp_list, pat_db, progress);

            foreach (StdClass tmp in tmp_list)
            {
                EyeOpe obj = GetFromStdClass(tmp);

                // 患者マスタに存在しないIDは除外する（従来の inner join と同じ扱い）
                if (!pat_dict.ContainsKey(obj.PtId))
                {
                    continue;
                }

                obj._Pat = pat_dict[obj.PtId];

                obj.OpeRecord = tmp.GetDataString("REC_CONT");
                obj.OpePass = tmp.GetDataString("PASS_CONT");

                tmpList.Add(obj);
            }

            return tmpList;
        }
    }
}
