using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// ICD留意病名
    /// </summary>
    public class DPCICDNotice
    {
        public string Name = "";

        /// <summary>
        /// ICDコード
        /// </summary>
        public string Code = "";

        /// <summary>
        /// ICDコード 正規表現
        /// </summary>
        public string Code2 = "";

        public string Cont = "";

        public string Flg1 = "";

        public string Flg2 = "";

        /// <summary>
        /// Flg1 = ○ となっているもので
        /// Code に一致 または Code2 に正規表現が合致
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool IsMatch(string code)
        {
            bool b = false;

            if (code.Length > 0 && this.Flg1.Contains("○"))
            {
                if (this.Code.Equals(code))
                {
                    b = true;
                }
                else if (this.Code2.Length > 0 && Regex.IsMatch(code, this.Code2))
                {
                    b = true;
                }
            }

            return b;
        }


        static DPCICDNotice GetFromStdClass(StdClass tmp)
        {
            DPCICDNotice obj = new DPCICDNotice();

            obj.Name = tmp.GetDataString("NAME").Trim();
            obj.Code = tmp.GetDataString("CODE").Trim();
            obj.Code2 = tmp.GetDataString("CODE2").Trim();
            obj.Cont = tmp.GetDataString("CONT").Trim();
            obj.Flg1 = tmp.GetDataString("FLG1").Trim();
            obj.Flg2 = tmp.GetDataString("FLG2").Trim();

            return obj;
        }


        public static List<DPCICDNotice> GetList(List<string> code_list)
        {
            List<DPCICDNotice> list = new List<DPCICDNotice>();

            // まず CODE が一致するものを探す
            if (AppString.ConcatList(code_list, ",", "'").Length == 0) return list;

            string cmd = "select * from DPC_ICD_NOTICE " +
                " where CODE in (" + AppString.ConcatList(code_list, ",", "'") + ")";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            // 次に CODE2 が合致するものを探す
            List<string> code2_list = new List<string>();
            string code2_sql = "";

            foreach (string code in code_list)
            {
                if (!code2_list.Contains(code.Substring(0, 1)))
                {
                    code2_list.Add(code.Substring(0, 1));

                    if (code2_sql.Length > 0)
                    {
                        code2_sql += " or ";
                    }

                    code2_sql += " CODE2 like '" + code.Substring(0, 1) + "%'";
                }
            }

            if (code2_sql.Length > 0)
            {
                // CODE2 の頭文字が一致するものを探す
                cmd = "select * from DPC_ICD_NOTICE " +
                    " where " + code2_sql;

                tmp_list = StdClass.GetList(DB.Db2, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    DPCICDNotice obj = GetFromStdClass(tmp);

                    // CODE2 に合致する code を探す
                    if (obj.Code2.Length > 0)
                    {
                        foreach (string code in code_list)
                        {
                            if (Regex.IsMatch(code, obj.Code2))
                            {
                                list.Add(obj);
                                break;
                            }
                        }
                    }
                }
            }

            return list;
        }
    }
}
