using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// 病名・修飾語マスター
    /// </summary>
    public class DiagMaster : StdEntity
    {
        public enum DiagMasterKind : int
        {
            Diag = 0,
            Prefix = 1,
            Suffix = 2
        }

        /// <summary>
        /// コード
        /// </summary>
        public string Code = "";

        /// <summary>
        /// 名称
        /// </summary>
        public string Name = "";

        /// <summary>
        /// カナ
        /// </summary>
        public string Kana = "";

        /// <summary>
        /// ICD10
        /// </summary>
        public string ICD10_1 = "";

        /// <summary>
        /// ICD10補助コード
        /// </summary>
        public string ICD10_2 = "";

        /// <summary>
        /// 種別
        /// 1～7 接頭語, 8 接尾語, 0 病名
        /// </summary>
        public string KindCode = "";

        /// <summary>
        /// 種別
        /// </summary>
        public DiagMasterKind Kind
        {
            get
            {
                DiagMasterKind k = DiagMasterKind.Diag;

                if (KindCode.Equals("8"))
                {
                    k = DiagMasterKind.Suffix;
                }
                else if (KindCode.Length > 0)
                {
                    k = DiagMasterKind.Prefix;
                }

                return k;
            }
        }


        public override string ToString()
        {
            return this.Name;
        }


        /// <summary>
        /// 病名検索（修飾語も含む）
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<DiagMaster> FindList(string name)
        {
            List<DiagMaster> list = new List<DiagMaster>();

            // 病名マスターの取得
            string cmd = "select * from M_BYOUMEI " +
                    " where FLD_06 like '%" + name + "%'";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                DiagMaster obj = new DiagMaster();

                obj.Code = tmp.DataDict["CODE"].ToString();
                obj.Name = tmp.DataDict["FLD_06"].ToString();
                obj.Kana = tmp.DataDict["FLD_10"].ToString();
                obj.ICD10_1 = tmp.DataDict["FLD_16"].ToString();
                obj.ICD10_2 = tmp.DataDict["FLD_17"].ToString();

                list.Add(obj);
            }


            // 修飾語マスターの取得
            cmd = "select * from M_BYOUMEI_Z " +
                    " where FLD_07 like '%" + name + "%'";

            tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                DiagMaster obj = new DiagMaster();

                obj.Code = tmp.DataDict["CODE"].ToString();
                obj.Name = tmp.DataDict["FLD_07"].ToString();
                obj.Kana = tmp.DataDict["FLD_10"].ToString();

                // INNO では A1000000 ～ A8000000 となっているので２文字目のみ取得する
                if (tmp.GetDataString("FLD_19").Length >= 2)
                {
                    obj.KindCode = tmp.GetDataString("FLD_19").Substring(1, 1);
                }

                list.Add(obj);
            }
            return list;
        }


        /// <summary>
        /// 病名または ICD10 による病名検索
        /// </summary>
        /// <param name="diag_name"></param>
        /// <param name="icd"></param>
        /// <returns></returns>
        public static List<DiagMaster> LoadByDiagNameOrICD(string diag_name, string icd)
        {
            List<DiagMaster> list = new List<DiagMaster>();

            if (diag_name.Length == 0 && icd.Length == 0)
            {
                return list;
            }
            string diag_sql = "";

            if (diag_name.Length > 0)
            {
                diag_sql += " FLD_06 like '%" + diag_name + "%'";
            }

            if (icd.Length > 0)
            {
                if (diag_sql.Length > 0)
                {
                    diag_sql += " or ";
                }

                diag_sql += " FLD_16 like '%" + icd + "%' or FLD_17 like '%" + icd + "%' or " +
                    " FLD_16 like '%" + icd.ToUpper() + "%' or FLD_17 like '%" + icd.ToUpper() + "%'";
            }

            // 病名マスターの取得
            string cmd = "select * from M_BYOUMEI " +
                    " where " + diag_sql;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                // IM73RC_F08, IM73RC_F13 いずれに合致しても、他方のものもリストに追加する。

                DiagMaster obj = new DiagMaster();

                obj.Code = tmp.DataDict["CODE"].ToString();
                obj.Name = tmp.DataDict["FLD_06"].ToString();
                obj.Kana = tmp.DataDict["FLD_10"].ToString();
                obj.ICD10_1 = tmp.DataDict["FLD_16"].ToString();
                obj.ICD10_2 = tmp.DataDict["FLD_17"].ToString();

                list.Add(obj);
/*
                obj = new DiagMaster();

                obj.Code = tmp.DataDict["CODE"].ToString();
                obj.Name = tmp.DataDict["FLD_06"].ToString();
                obj.Kana = tmp.DataDict["FLD_10"].ToString();
                obj.ICD10_1 = tmp.DataDict["FLD_17"].ToString();

                list.Add(obj);
 */
            }
            return list;
        }
    }
}
