using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class SchemaImage
    {
        public string Group = "";

        public string Code = "";

        public string Name = "";

        public string File = "";

        public string FilePath
        {
            get
            {
                return LibSettings.Current.SchemaFolder + "\\" + File;
            }
        }


        static Dictionary<string, SchemaImage> dict = new Dictionary<string, SchemaImage>();

        public static Dictionary<string, SchemaImage> Dict
        {
            get
            {
                if (dict.Count == 0)
                {
                    Init();
                }

                return dict;
            }
        }


        /// <summary>
        /// XMLファイルを読み込んでリストを作成する
        /// </summary>
        public static void Init()
        {
            string xml_file = AppFile.FilePath("Schema.xml");

            if (!System.IO.File.Exists(xml_file))
            {
                return;
            }

            StreamReader reader = new StreamReader(xml_file, Encoding.GetEncoding("shift-jis"));

            DataSet dSet = new DataSet();
            dSet.ReadXml(reader);

            DataTable table = dSet.Tables["Image"];

            foreach (DataRow r in table.Rows)
            {
                SchemaImage image = new SchemaImage();

                image.Code = r["Code"].ToString();
                image.Name = r["Name"].ToString();
                image.Group = r["Group"].ToString();
                image.File = r["File"].ToString();

                dict.Add(image.Code, image);
            }

            reader.Close();
        }
    }

    public class SchemaGroup
    {
        public string Code = "";

        public string Name = "";

        public List<SchemaImage> ImageList = new List<SchemaImage>();


        static List<SchemaGroup> list = new List<SchemaGroup>();

        public static List<SchemaGroup> List
        {
            get
            {
                if (list.Count == 0)
                {
                    Init();
                }

                return list;
            }
        }


        /// <summary>
        /// XMLファイルを読み込んでリストを作成する
        /// </summary>
        public static void Init()
        {
            string xml_file = AppFile.FilePath("Schema.xml");

            if (!File.Exists(xml_file))
            {
                return;
            }

            StreamReader reader = new StreamReader(xml_file, Encoding.GetEncoding("shift-jis"));

            DataSet dSet = new DataSet();
            dSet.ReadXml(reader);

            DataTable table = dSet.Tables["Group"];

            foreach (DataRow r in table.Rows)
            {
                SchemaGroup group = new SchemaGroup();

                group.Code = r["Code"].ToString();
                group.Name = r["Name"].ToString();

                foreach (SchemaImage image in SchemaImage.Dict.Values)
                {
                    if (image.Group.Equals(group.Code))
                    {
                        group.ImageList.Add(image);
                    }
                }

                list.Add(group);
            }

            reader.Close();
        }
    }
}
