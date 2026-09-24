using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace MedicalLibrary.Agent
{
    /// <summary>
    /// XMLで定義されている検査項目マスター
    /// </summary>
    public class EyeKensaItemMaster
    {
        public string Code = "";
        public string Name = "";
        public string Type = "";
        public string Text = "";
        public string X = "";
        public string Y = "";
        public string Width = "";
        public string Height = "";
        public string Ime = "";
        public string Align = "";

        /// <summary>
        /// 該当する検査種別について、XMLで定義されている検査項目マスターの辞書を返す。
        /// （Codeが入っているもののみ）
        /// </summary>
        /// <param name="kensa_id"></param>
        /// <returns></returns>
        public static List<EyeKensaItemMaster> ListByKensaId(string kensa_id)
        {
            List<EyeKensaItemMaster> list = new List<EyeKensaItemMaster>();

            DataRow[] pages = EyeDict.EyeSet.Tables["KensaPage"].Select("ID = '" + kensa_id + "'");

            if (pages.Length == 0)
            {
                return list;
            }

            DataRow[] rows = EyeDict.EyeSet.Tables["KensaItem"].Select("KensaPage_ID = '" + pages[0]["KensaPage_ID"].ToString() + "'");

            foreach (DataRow r in rows)
            {
                if (r["Code"].ToString().Length > 0)
                {
                    EyeKensaItemMaster kensa = new EyeKensaItemMaster();
                    kensa.Code = r["Code"].ToString();
                    kensa.Name = r["Name"].ToString();
                    kensa.Type = r["Type"].ToString();
                    kensa.Text = r["Text"].ToString();
                    kensa.X = r["X"].ToString();
                    kensa.Y = r["Y"].ToString();
                    kensa.Width = r["Width"].ToString();
                    kensa.Height = r["Height"].ToString();
                    kensa.Ime = r["Ime"].ToString();
                    kensa.Align = r["Align"].ToString();

                    list.Add(kensa);
                }
            }

            return list;
        }
    }
}
