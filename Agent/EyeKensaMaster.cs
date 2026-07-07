using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace MedicalLibrary.Agent
{
    /// <summary>
    /// XMLで定義されている検査種別マスター
    /// </summary>
    public class EyeKensaMaster
    {
        public string Tab = "";
        public string Id = "";
        public string Name = "";
        public string Text = "";
        public string Header = "";
        public string PageVisible = "";
        public string PageType = "";

        static Dictionary<string, EyeKensaMaster> dict = new Dictionary<string, EyeKensaMaster>();

        public static Dictionary<string, EyeKensaMaster> Dict
        {
            get
            {
                if (dict.Count == 0)
                {
                    dict.Add("0", new EyeKensaMaster());

                    foreach (DataRow r in EyeDict.EyeSet.Tables["KensaPage"].Rows)
                    {
                        if (r["ID"].ToString().Length > 0 && !dict.ContainsKey(r["ID"].ToString()))
                        {
                            EyeKensaMaster kensa = new EyeKensaMaster();
                            kensa.Tab = r["Tab"].ToString();
                            kensa.Id = r["ID"].ToString();
                            kensa.Name = r["Name"].ToString();
                            kensa.Text = r["Text"].ToString();
                            kensa.Header = r["Header"].ToString();
                            kensa.PageVisible = r["PageVisible"].ToString();
                            kensa.PageType = r["PageType"].ToString();

                            dict.Add(kensa.Id, kensa);
                        }
                    }
                }

                return dict;
            }
        }
    }
}
