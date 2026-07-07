using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class PatOrderSettings
    {
        DataSet DSet;

        List<string> pc_shinku_list = new List<string>();

        public List<string> PcShinkuList
        {
            get
            {
                return pc_shinku_list;
            }
        }

        List<string> pc_dept_list = new List<string>();

        public List<string> PcDeptList
        {
            get
            {
                return pc_dept_list;
            }
        }

        List<string> pc_sekou_list = new List<string>();

        public List<string> PcSekouList
        {
            get
            {
                return pc_sekou_list;
            }
        }

        List<string> pc_in_out_list = new List<string>();

        public List<string> PcInOutList
        {
            get
            {
                return pc_in_out_list;
            }
        }

        Dictionary<string, string> shinku_dict = new Dictionary<string, string>();

        public Dictionary<string, string> ShinkuDict
        {
            get
            {
                return shinku_dict;
            }
        }

        Dictionary<string, string> dept_dict = new Dictionary<string, string>();

        public Dictionary<string, string> DeptDict
        {
            get
            {
                return dept_dict;
            }
        }

        Dictionary<string, string> sekou_dict = new Dictionary<string, string>();

        public Dictionary<string, string> SekouDict
        {
            get
            {
                return sekou_dict;
            }
        }

        string printer = "";

        public string Printer
        {
            get
            {
                return printer;
            }
        }

        bool preview = false;

        public bool Preview
        {
            get
            {
                return preview;
            }
        }

        /// <summary>
        /// 初期化されているかどうか
        /// </summary>
        bool valid = false;

        public bool IsValid
        {
            get
            {
                return this.valid;
            }
            set
            {
                this.valid = value;
            }
        }


        /// <summary>
        /// 初期化。プログラムの最初に実行する。
        /// </summary>
        /// <returns>XMLファイル内に該当PCの定義がなければ印刷できないため false を返す。</returns>
        public bool Init()
        {
            DSet = new DataSet();

            string XmlFile = AppFile.FilePath("PatOrderSheet.xml");

            if (!File.Exists(XmlFile))
            {
                return false;
            }

            DSet.ReadXml(new StreamReader(XmlFile, Encoding.GetEncoding("shift-jis")));

            string pc_name = Environment.GetEnvironmentVariable("COMPUTERNAME");

            pc_shinku_list.Clear();
            pc_dept_list.Clear();
            pc_sekou_list.Clear();
            pc_in_out_list.Clear();

            bool result = false;

            foreach (DataRow r in DSet.Tables["PC"].Rows)
            {
                if (pc_name.Equals(r["Name"].ToString()))
                {
                    foreach (string s in r["Shinku"].ToString().Split(','))
                    {
                        pc_shinku_list.Add(s.Trim());
                    }

                    foreach (string s in r["Dept"].ToString().Split(','))
                    {
                        pc_dept_list.Add(s.Trim());
                    }

                    foreach (string s in r["Sekou"].ToString().Split(','))
                    {
                        pc_sekou_list.Add(s.Trim());
                    }

                    foreach (string s in r["InOut"].ToString().Split(','))
                    {
                        pc_in_out_list.Add(s.Trim());
                    }

                    printer = r["Printer"].ToString();

                    if (r["Preview"].ToString().Equals("1"))
                    {
                        preview = true;
                    }

                    result = true;
                    break;
                }
            }

            // PC設定が存在しなければデフォルト設定を適用する
            if (!result)
            {
                foreach (DataRow r in DSet.Tables["PC"].Rows)
                {
                    if (r["Name"].ToString().Length == 0)
                    {
                        foreach (string s in r["Shinku"].ToString().Split(','))
                        {
                            pc_shinku_list.Add(s.Trim());
                        }

                        foreach (string s in r["Dept"].ToString().Split(','))
                        {
                            pc_dept_list.Add(s.Trim());
                        }

                        foreach (string s in r["Sekou"].ToString().Split(','))
                        {
                            pc_sekou_list.Add(s.Trim());
                        }

                        foreach (string s in r["InOut"].ToString().Split(','))
                        {
                            pc_in_out_list.Add(s.Trim());
                        }

                        printer = r["Printer"].ToString();

                        if (r["Preview"].ToString().Equals("1"))
                        {
                            preview = true;
                        }

                        result = true;
                        break;
                    }
                }
            }

            shinku_dict.Clear();

            foreach (DataRow r in DSet.Tables["Shinku"].Rows)
            {
                if (!shinku_dict.ContainsKey(r["Code"].ToString()))
                {
                    shinku_dict.Add(r["Code"].ToString(), r["Name"].ToString());
                }
            }

            dept_dict.Clear();

            foreach (Dept dept in Dict.DeptDict.Values)
            {
                // 名称が無い場合
                if (dept.ShortName.Length == 0)
                {
                    continue;
                }

                // 無効な場合
                if (dept.Status.Equals(9))
                {
                    continue;
                }

                // すでに辞書に登録されている場合
                if (dept_dict.ContainsKey(dept.Code.ToString()))
                {
                    continue;
                }

                dept_dict.Add(dept.Code.ToString(), dept.ShortName);
            }

            sekou_dict.Clear();

            foreach (DataRow r in DSet.Tables["Sekou"].Rows)
            {
                if (!sekou_dict.ContainsKey(r["Code"].ToString()))
                {
                    sekou_dict.Add(r["Code"].ToString(), r["Name"].ToString());
                }
            }

            this.IsValid = true;

            return result;
        }
    }
}
