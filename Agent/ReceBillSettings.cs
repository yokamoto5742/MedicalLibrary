using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class ReceBillSettings
    {
        DataSet DSet = new DataSet();

        Dictionary<string, string> _ExcludeCodeDict = new Dictionary<string, string>();

        public Dictionary<string, string> ExcludeCodeDict
        {
            get
            {
                return this._ExcludeCodeDict;
            }
        }


        /// <summary>
        /// 初期化。プログラムの最初に実行する。
        /// </summary>
        /// <returns>XMLファイル内に該当PCの定義がなければ印刷できないため false を返す。</returns>
        public bool Init()
        {
            DSet = new DataSet();

            string XmlFile = AppFile.FilePath("ReceBill.xml");

            if (!File.Exists(XmlFile))
            {
                return false;
            }

            StreamReader reader = new StreamReader(XmlFile, Encoding.GetEncoding("shift-jis"));

            DSet.ReadXml(reader);

            reader.Close();

            bool result = false;

            foreach (DataRow r in DSet.Tables["ExcludeCode"].Rows)
            {
                if (!_ExcludeCodeDict.ContainsKey(r["Code"].ToString()))
                {
                    _ExcludeCodeDict.Add(r["Code"].ToString(), r["Name"].ToString());
                }
            }

            result = true;

            return result;
        }
    }
}
