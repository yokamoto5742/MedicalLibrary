using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class DrugAdvSettings
    {
        /// <summary>
        /// 現在の設定情報オブジェクト
        /// </summary>
        public static DrugAdvSettings Current = new DrugAdvSettings();

        /// <summary>
        /// 服薬指導算定オーダーコード
        /// 複数あればカンマ区切り
        /// </summary>
        public string AdvOrderMasterCode = "";

        /// <summary>
        /// アプリケーション設定ファイルの読み込み
        /// </summary>
        /// <param name="xml_file"></param>
        public static StdReturn Init()
        {
            StdReturn sr = new StdReturn();

            string xml_file = AppFile.FilePath("DrugAdvice.xml");

            if (File.Exists(xml_file))
            {
                XmlSerializer s = new XmlSerializer(typeof(DrugAdvSettings));

                FileStream fs = new FileStream(xml_file, FileMode.Open);
                Current = (DrugAdvSettings)s.Deserialize(new StreamReader(fs, Encoding.GetEncoding("Shift_JIS")));
                fs.Close();
            }
            else
            {
                sr.Errs.Add("設定ファイル DrugAdvice.xml が存在しません");
            }

            return sr;
        }
    }
}
