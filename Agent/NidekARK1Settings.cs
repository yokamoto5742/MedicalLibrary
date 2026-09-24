using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class NidekARK1Settings
    {
        /// <summary>
        /// 当PCの設定が存在するか
        /// </summary>
        public bool IsNidek = false;

        /// <summary>
        /// オリジナルXMLファイルのパス
        /// </summary>
        public string SourcePath = @"c:\demo\rkt\txt";

        /// <summary>
        /// 出力先ファイル
        /// </summary>
        public string TargetFile = @"c:\transfile\data\ref.dat";

        public NidekARK1Settings()
        {
            this.Init();
        }

        public void Init()
        {
            string file = AppFile.FilePath("NidekARK1.xml");

            if (!File.Exists(file))
            {
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            XmlElement xe;

            foreach (XmlNode xn in doc.SelectNodes("NidekARK1/PC"))
            {
                xe = (XmlElement)xn;

                // PC名が空の場合はデフォルト設定
                // PC名が入っていて、かつ異なれば飛ばす
                if (xe.SelectSingleNode("Name").InnerText.Length > 0 &&
                    !xe.SelectSingleNode("Name").InnerText.Equals(Environment.MachineName))
                {
                    continue;
                }

                // 当PCの設定が存在する場合
                if (xe.SelectSingleNode("Name").InnerText.Equals(Environment.MachineName))
                {
                    this.IsNidek = true;
                }

                this.SourcePath = xe.SelectSingleNode("SourcePath").InnerText;
                this.TargetFile = xe.SelectSingleNode("TargetFile").InnerText;
            }
        }
    }
}
