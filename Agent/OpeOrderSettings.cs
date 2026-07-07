using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class OpeOrderSettings
    {
        public enum Place
        {
            Non = 0,
            Hon = 1,
            Minami = 2
        }

        /// <summary>
        /// PC設置場所（中央 or 南館 or Non）
        /// </summary>
        public Place PCPlace = Place.Non;

        public enum OpenForm
        {
            List = 1,
            Order = 2
        }

        /// <summary>
        /// 最初に開く画面（週間予定表 or 手術指示）
        /// </summary>
        public OpenForm PCOpenForm = OpenForm.List;

        DataSet DSet;

        List<string> doctor_list3 = new List<string>();

        public List<string> DoctorList3
        {
            get
            {
                return doctor_list3;
            }
        }

        List<string> ns_list1 = new List<string>();

        public List<string> NsList1
        {
            get
            {
                return ns_list1;
            }
        }

        List<string> ns_list2 = new List<string>();

        public List<string> NsList2
        {
            get
            {
                return ns_list2;
            }
        }

        List<string> ns_list3 = new List<string>();

        public List<string> NsList3
        {
            get
            {
                return ns_list3;
            }
        }

        List<string> ns_list4 = new List<string>();

        public List<string> NsList4
        {
            get
            {
                return ns_list4;
            }
        }

        List<string> staff_code_list = new List<string>();

        public List<string> StaffCodeList
        {
            get
            {
                return staff_code_list;
            }
        }


        /// <summary>
        /// 手術予定表.xls
        /// </summary>
        public string ExcelTerm = "";

        /// <summary>
        /// 週間手術予定表.xls
        /// </summary>
        public string ExcelWeek = "";


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
        public StdReturn Init()
        {
            StdReturn sr = new StdReturn();
            bool result = false;

            DSet = new DataSet();

            string XmlFile = AppFile.FilePath("OpeOrder.xml");

            if (!File.Exists(XmlFile))
            {
                sr.Errs.Add("設定ファイル OpeOrder.xml が存在しません");
                return sr;
            }

            StreamReader reader = new StreamReader(XmlFile, Encoding.GetEncoding("shift-jis"));

            DSet.ReadXml(reader);

            reader.Close();

            foreach (DataRow r in DSet.Tables["PC"].Rows)
            {
                if (r["Name"].ToString().Equals(Environment.MachineName, StringComparison.CurrentCultureIgnoreCase))
                {
                    if (r["Place"].ToString().Equals("1"))
                    {
                        PCPlace = Place.Hon;
                    }
                    else if (r["Place"].ToString().Equals("2"))
                    {
                        PCPlace = Place.Minami;
                    }
                    else
                    {
                        PCPlace = Place.Non;
                    }

                    if (r["OpenForm"].ToString().Equals("2"))
                    {
                        PCOpenForm = OpenForm.Order;
                    }
                    else
                    {
                        PCOpenForm = OpenForm.List;
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
                        if (r["Place"].ToString().Equals("1"))
                        {
                            PCPlace = Place.Hon;
                        }
                        else if (r["Place"].ToString().Equals("2"))
                        {
                            PCPlace = Place.Minami;
                        }
                        else
                        {
                            PCPlace = Place.Non;
                        }

                        if (r["OpenForm"].ToString().Equals("2"))
                        {
                            PCOpenForm = OpenForm.Order;
                        }
                        else
                        {
                            PCOpenForm = OpenForm.List;
                        }

                        result = true;
                        break;
                    }
                }
            }

            doctor_list3.Clear();
            ns_list1.Clear();
            ns_list2.Clear();
            ns_list3.Clear();
            ns_list4.Clear();
            staff_code_list.Clear();

            foreach (string s in DSet.Tables["Doctor"].Rows[0]["Doctor3"].ToString().Split(','))
            {
                doctor_list3.Add(s);
            }

            foreach (string s in DSet.Tables["Ns"].Rows[0]["Ns1"].ToString().Split(','))
            {
                ns_list1.Add(s);
            }

            foreach (string s in DSet.Tables["Ns"].Rows[0]["Ns2"].ToString().Split(','))
            {
                ns_list2.Add(s);
            }

            foreach (string s in DSet.Tables["Ns"].Rows[0]["Ns3"].ToString().Split(','))
            {
                ns_list3.Add(s);
            }

            foreach (string s in DSet.Tables["Ns"].Rows[0]["Ns4"].ToString().Split(','))
            {
                ns_list4.Add(s);
            }

            foreach (string s in DSet.Tables["Ns"].Rows[0]["StaffCode"].ToString().Split(','))
            {
                staff_code_list.Add(s);
            }

            ExcelTerm = DSet.Tables["Excel"].Rows[0]["Term"].ToString();
            ExcelWeek = DSet.Tables["Excel"].Rows[0]["Week"].ToString();

            this.valid = true;

            return sr;
        }
    }
}
