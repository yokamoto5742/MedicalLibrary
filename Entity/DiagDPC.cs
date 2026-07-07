using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class DiagDPC : StdKarte1
    {
        public new static DB Db = DB.Db3;

        /// <summary>
        /// 連番
        /// </summary>
        public int SEQ = 0;

        /// <summary>
        /// 主傷病
        /// </summary>
        public bool MainFlg = false;

        /// <summary>
        /// 契機傷病
        /// </summary>
        public bool TriggerFlg = false;

        /// <summary>
        /// 資源投入1
        /// </summary>
        public bool ResourceFlg1 = false;

        /// <summary>
        /// 資源投入2
        /// </summary>
        public bool ResourceFlg2 = false;

        /// <summary>
        /// 併存症
        /// </summary>
        public bool SubFlg = false;

        /// <summary>
        /// 入院後発症
        /// </summary>
        public bool AfterFlg = false;

        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DeleteFlg = false;

        /// <summary>
        /// 入院番号
        /// </summary>
        public int InSEQ = 0;


        static DiagDPC GetFromStdClass(StdClass tmp)
        {
            DiagDPC obj = new DiagDPC();

            obj.PtId = tmp.GetDataString("P_ID");
            obj.InSEQ = tmp.GetDataInt("NYUIN_NO", 0);
            obj.SEQ = tmp.GetDataInt("SEQ", 0);
            obj.MainFlg = tmp.GetDataString("FLG_MAIN").Equals("1");
            obj.TriggerFlg = tmp.GetDataString("FLG_MOMENT").Equals("1");
            obj.ResourceFlg1 = tmp.GetDataString("FLG_RESOURCE_1").Equals("1");
            obj.ResourceFlg2 = tmp.GetDataString("FLG_RESOURCE_2").Equals("1");
            obj.SubFlg = tmp.GetDataString("FLG_PARALLEL").Equals("1");
            obj.AfterFlg = tmp.GetDataString("FLG_AFTER").Equals("1");
            obj.DeleteFlg = tmp.GetDataString("DEL_FLG").Equals("1");

            obj.BaseFromStdClass(tmp);

            return obj;
        }

        public static List<DiagDPC> GetList(List<PatIn> p_list)
        {
            List<DiagDPC> list = new List<DiagDPC>();

            if (p_list.Count == 0)
            {
                return list;
            }

            string cmd = "select * from D_BYOUMEI_DPC t " +
                " where (t.P_ID, t.NYUIN_NO) in (" + AppString.ConcatList(p_list.ConvertAll((x) => { return "(" + x.Id + "," + x.SEQ + ")"; }), ",") + ")" +
                " and (t.DEL_FLG is null or t.DEL_FLG = 0)";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }

        public static List<DiagDPC> GetList(string pt_id, int in_seq)
        {
            List<DiagDPC> list = new List<DiagDPC>();

            if (pt_id.Length == 0)
            {
                return list;
            }

            string cmd = "select * from D_BYOUMEI_DPC t " +
                " where t.P_ID = " + pt_id +
                " and t.NYUIN_NO = " + in_seq +
                " and (t.DEL_FLG is null or t.DEL_FLG = 0)";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }

        public static List<DiagDPC> GetList(string pt_id, string in_date)
        {
            List<DiagDPC> list = new List<DiagDPC>();

            if (!AppString.IsNumber(pt_id) || !AppString.IsDate(in_date))
            {
                return list;
            }

            string cmd = "select * from D_BYOUMEI_DPC t " +
                " where (t.P_ID, t.NYUIN_NO) in " +
                " (select td.P_ID, td.NYUIN_NO from D_NYUIN" + (Db == DB.Db2 ? Env.DB_LINK : "") + " td " +
                "  where td.P_ID = " + pt_id + " and td.NYUIN_DATE = " + in_date + " and td.PROCESS in (11)) " +
                " and (t.DEL_FLG is null or t.DEL_FLG = 0)";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }

        public static List<DiagDPC> GetListByDiag(Diag diag)
        {
            List<DiagDPC> list = new List<DiagDPC>();

            if (diag.PtId.Length == 0 || diag.SEQ == 0)
            {
                return list;
            }

            string cmd = "select * from D_BYOUMEI_DPC t " +
                " where t.P_ID = " + diag.PtId +
                " and t.SEQ = " + diag.SEQ +
                " order by t.NYUIN_NO";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }


        public static StdReturn Save(string pt_id, int in_seq, List<DiagDPC> diag_list)
        {
            StdReturn sr = new StdReturn();

            if (!AppString.IsNumber(pt_id))
            {
                return sr;
            }

            string reg_date = DateTime.Now.ToString("yyyyMMdd");
            string reg_time = DateTime.Now.ToString("HHmmss");
/*
            if (DB.Db2.Connection.State != ConnectionState.Open) DB.Db2.Open();

            using (DbTransaction tran = DB.Db2.Connection.BeginTransaction())
            {
                try
                {
                    // 全て Update（すべて 0 にする）
                    string cmd = "update D_BYOUMEI_DPC t set " +
                        " FLG_MAIN = 0, FLG_MOMENT = 0" +
                        ", FLG_RESOURCE_1 = 0, FLG_RESOURCE_2 = 0" +
                        ", FLG_PARALLEL = 0, FLG_AFTER = 0" +
//                        ", DEL_FLG = 1" +
                        ", UP_USR = " + (LoginUser.Id.Length > 0 ? LoginUser.Id : "null") +
                        ", UP_AGENT = " + (LoginUser.Id2.Length > 0 ? LoginUser.Id2 : "null") +
                        ", UP_DATE =  " + reg_date +
                        ", UP_TIME =  " + reg_time +
                        ", UP_PCNAME = '" + Environment.MachineName + "'" +
                        " where t.P_ID = " + pt_id + " and t.NYUIN_NO = " + in_seq;

                    DB.Db2.Command.CommandText = cmd;
                    DB.Db2.Command.ExecuteNonQuery();

                    foreach (DiagDPC obj in diag_list)
                    {
                        // まず Update
                        cmd = "update D_BYOUMEI_DPC t set " +
                            " FLG_MAIN = " + (obj.MainFlg ? 1 : 0) +
                            ", FLG_MOMENT = " + (obj.TriggerFlg ? 1 : 0) +
                            ", FLG_RESOURCE_1 = " + (obj.ResourceFlg1 ? 1 : 0) +
                            ", FLG_RESOURCE_2 = " + (obj.ResourceFlg2 ? 1 : 0) +
                            ", FLG_PARALLEL = " + (obj.SubFlg ? 1 : 0) +
                            ", FLG_AFTER = " + (obj.AfterFlg ? 1 : 0) +
                            ", DEL_FLG = " + (obj.DeleteFlg ? 1 : 0) +
                            " where t.P_ID = " + pt_id + " and t.NYUIN_NO = " + in_seq + " and t.SEQ = " + obj.SEQ;

                        DB.Db2.Command.CommandText = cmd;
                        int i = DB.Db2.Command.ExecuteNonQuery();

                        if (i == 0)
                        {
                            // なければ Insert
                            cmd = "insert into D_BYOUMEI_DPC (" +
                                " P_ID, NYUIN_NO, SEQ, " +
                                " FLG_MAIN, FLG_MOMENT, " +
                                " FLG_RESOURCE_1, FLG_RESOURCE_2, " +
                                " FLG_PARALLEL, FLG_AFTER, " +
                                " DEL_FLG, " +
                                " REG_USR, " +
                                " REG_AGENT, " +
                                " REG_DATE, REG_TIME, REG_PCNAME " +
                                " ) values ( " +
                                pt_id + ", " + in_seq + ", " + obj.SEQ + ", " +
                                (obj.MainFlg ? 1 : 0) + ", " + (obj.TriggerFlg ? 1 : 0) + ", " +
                                (obj.ResourceFlg1 ? 1 : 0) + ", " + (obj.ResourceFlg2 ? 1 : 0) + ", " +
                                (obj.SubFlg ? 1 : 0) + ", " + (obj.AfterFlg ? 1 : 0) + ", " +
                                (obj.DeleteFlg ? 1 : 0) + ", " +
                                (LoginUser.Id.Length > 0 ? LoginUser.Id : "null") + ", " +
                                (LoginUser.Id2.Length > 0 ? LoginUser.Id2 : "null") + ", " +
                                reg_date + ", " + reg_time + ", '" + Environment.MachineName + "' " +
                                " )";

                            DB.Db2.Command.CommandText = cmd;
                            DB.Db2.Command.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                finally
                {
                    DB.Db2.Close();
                }
            }
*/
            if (DB.Db3.Connection.State != ConnectionState.Open) DB.Db3.Open();

            using (DbTransaction tran = DB.Db3.Connection.BeginTransaction())
            {
                try
                {
                    // 全て Update（すべて 0 にする）
                    string cmd = "update D_BYOUMEI_DPC t set " +
                        " FLG_MAIN = 0, FLG_MOMENT = 0" +
                        ", FLG_RESOURCE_1 = 0, FLG_RESOURCE_2 = 0" +
                        ", FLG_PARALLEL = 0, FLG_AFTER = 0" +
//                        ", DEL_FLG = 1" +
                        ", UP_USR = " + (LoginUser.Id.Length > 0 ? LoginUser.Id : "null") +
                        ", UP_AGENT = " + (LoginUser.Id2.Length > 0 ? LoginUser.Id2 : "null") +
                        ", UP_DATE =  " + reg_date +
                        ", UP_TIME =  " + reg_time +
                        ", UP_PCNAME = '" + Environment.MachineName + "'" +
                        " where t.P_ID = " + pt_id + " and t.NYUIN_NO = " + in_seq;

                    DB.Db3.Command.CommandText = cmd;
                    DB.Db3.Command.ExecuteNonQuery();

                    foreach (DiagDPC obj in diag_list)
                    {
                        // まず Update
                        cmd = "update D_BYOUMEI_DPC t set " +
                            " FLG_MAIN = " + (obj.MainFlg ? 1 : 0) +
                            ", FLG_MOMENT = " + (obj.TriggerFlg ? 1 : 0) +
                            ", FLG_RESOURCE_1 = " + (obj.ResourceFlg1 ? 1 : 0) +
                            ", FLG_RESOURCE_2 = " + (obj.ResourceFlg2 ? 1 : 0) +
                            ", FLG_PARALLEL = " + (obj.SubFlg ? 1 : 0) +
                            ", FLG_AFTER = " + (obj.AfterFlg ? 1 : 0) +
                            ", DEL_FLG = " + (obj.DeleteFlg ? 1 : 0) +
                            " where t.P_ID = " + pt_id + " and t.NYUIN_NO = " + in_seq + " and t.SEQ = " + obj.SEQ;

                        DB.Db3.Command.CommandText = cmd;
                        int i = DB.Db3.Command.ExecuteNonQuery();

                        if (i == 0)
                        {
                            // なければ Insert
                            cmd = "insert into D_BYOUMEI_DPC (" +
                                " P_ID, NYUIN_NO, SEQ, " +
                                " FLG_MAIN, FLG_MOMENT, " +
                                " FLG_RESOURCE_1, FLG_RESOURCE_2, " +
                                " FLG_PARALLEL, FLG_AFTER, " +
                                " DEL_FLG, " +
                                " REG_USR, " +
                                " REG_AGENT, " +
                                " REG_DATE, REG_TIME, REG_PCNAME " +
                                " ) values ( " +
                                pt_id + ", " + in_seq + ", " + obj.SEQ + ", " +
                                (obj.MainFlg ? 1 : 0) + ", " + (obj.TriggerFlg ? 1 : 0) + ", " +
                                (obj.ResourceFlg1 ? 1 : 0) + ", " + (obj.ResourceFlg2 ? 1 : 0) + ", " +
                                (obj.SubFlg ? 1 : 0) + ", " + (obj.AfterFlg ? 1 : 0) + ", " +
                                (obj.DeleteFlg ? 1 : 0) + ", " +
                                (LoginUser.Id.Length > 0 ? LoginUser.Id : "null") + ", " +
                                (LoginUser.Id2.Length > 0 ? LoginUser.Id2 : "null") + ", " +
                                reg_date + ", " + reg_time + ", '" + Environment.MachineName + "' " +
                                " )";

                            DB.Db3.Command.CommandText = cmd;
                            DB.Db3.Command.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                finally
                {
                    DB.Db3.Close();
                }
            }

            return sr;
        }
    }
}
