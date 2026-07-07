using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class OpeNursingSettings
    {
        public static OpeNursingSettings Current = new OpeNursingSettings();

        public class SchemaBg
        {
            public string Id = "";
            public string Tab = "";
            public string Path = "";
            public string Status = "";
        }

        public struct PictureTag
        {
            public string Id;
            public string Tab;
            public string Path;
        }

        public Dictionary<string, SchemaBg> SchemaBgDict = new Dictionary<string, SchemaBg>();

        static bool init = false;

        /// <summary>
        /// アプリケーション設定ファイルの読み込み
        /// </summary>
        public static void Init(bool force = false)
        {
            if (!force && init) return;

            string xml_file = AppFile.FilePath("OpeNursing.xml");

            if (File.Exists(xml_file))
            {
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(xml_file);

                    // いったんクリアする
                    Current = new OpeNursingSettings();

                    foreach (XmlNode n in xmlDoc.SelectNodes("OpeNursing/Schema"))
                    {
                        if (n.SelectSingleNode("ID") == null ||
                            n.SelectSingleNode("Tab") == null ||
                            n.SelectSingleNode("Path") == null ||
                            n.SelectSingleNode("Status") == null)
                        {
                            continue;
                        }

                        SchemaBg bg = new SchemaBg();

                        bg.Id = n.SelectSingleNode("ID").InnerText;
                        bg.Tab = n.SelectSingleNode("Tab").InnerText;
                        bg.Path = n.SelectSingleNode("Path").InnerText;
                        bg.Status = n.SelectSingleNode("Status").InnerText;

                        if (!Current.SchemaBgDict.ContainsKey(bg.Id))
                        {
                            Current.SchemaBgDict.Add(bg.Id, bg);
                        }
                    }

                    init = true;
                }
                catch (Exception ex)
                {
                    LibUtility.Except(ex, false);
                }
            }
        }
    }
}
