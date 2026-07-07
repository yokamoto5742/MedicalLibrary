using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using MedicalLibrary.Entity;

namespace MedicalLibrary.Agent
{
    class MWMSettings
    {
        DataSet DSet = new DataSet();

        public List<MWMCommentItem> CommentItemList = new List<MWMCommentItem>();

        public string CommentId = "";

        public string CommentText = "";


        public Dictionary<string, string> ModalityDict;

        public MWMKind Kind = MWMKind.SekouCode;

        List<string> codeList = new List<string>();

        public List<string> CodeList
        {
            get
            {
                if (codeList.Count == 0)
                {
                    DataTable table = DSet.Tables["Modality"];

                    if (Kind == MWMKind.SekouCode)
                    {
                        foreach (DataRow r in table.Rows)
                        {
                            foreach (string s in r["SekouCode"].ToString().Split(','))
                            {
                                codeList.Add(s);
                            }
                        }
                    }
                    else if (Kind == MWMKind.OrderCode)
                    {
                        foreach (DataRow r in table.Rows)
                        {
                            foreach (string s in r["OrderCode"].ToString().Split(','))
                            {
                                codeList.Add(s);
                            }
                        }
                    }
                }

                return codeList;
            }
        }

        public string Name = "";

        public string Title = "";

        public string Handle = "";

        public string Path = "";

        public string Mode = "";

        public int Interval = 0;


        /// <summary>
        /// 有効かどうか
        /// </summary>
        public bool IsValid
        {
            get
            {
                bool b = false;

                if (this.Title.Length > 0)
                {
                    b = true;
                }

                return b;
            }
        }


        public void Init(string xml_file)
        {
            if (!File.Exists(xml_file))
            {
                return;
            }

            DSet.ReadXml(new StreamReader(xml_file, Encoding.GetEncoding("shift-jis")));

            ModalityDict = new Dictionary<string, string>();

            DataTable table = DSet.Tables["Modality"];

            foreach (DataRow r in table.Rows)
            {
                this.Name = r["Name"].ToString();
                break;
            }

            if (table.Columns.Contains("SekouCode"))
            {
                Kind = MWMKind.SekouCode;

                foreach (DataRow r in table.Rows)
                {
                    string name = r["Name"].ToString();
                    string code = r["SekouCode"].ToString();

                    foreach (string s in code.Split(','))
                    {
                        if (!ModalityDict.ContainsKey(s))
                        {
                            ModalityDict.Add(s, name);
                        }
                    }
                }
            }
            else if (table.Columns.Contains("OrderCode"))
            {
                Kind = MWMKind.OrderCode;

                foreach (DataRow r in table.Rows)
                {
                    string name = r["Name"].ToString();
                    string code = r["OrderCode"].ToString();

                    foreach (string s in code.Split(','))
                    {
                        if (!ModalityDict.ContainsKey(s))
                        {
                            ModalityDict.Add(s, name);
                        }
                    }
                }
            }

            if (DSet.Tables.Contains("Comment"))
            {
                table = DSet.Tables["Comment"];

                foreach (DataRow r in table.Rows)
                {
                    this.CommentId = r["Default"].ToString();
                    this.CommentText = r["Text"].ToString();
                    break;
                }
            }

            if (DSet.Tables.Contains("CommentItem"))
            {
                table = DSet.Tables["CommentItem"];

                foreach (DataRow r in table.Rows)
                {
                    MWMCommentItem obj = new MWMCommentItem();

                    obj.Id = r["Id"].ToString();
                    obj.Text = r["Text"].ToString();
                    obj.Value = r["Value"].ToString();

                    this.CommentItemList.Add(obj);
                }
            }

            if (DSet.Tables.Contains("File"))
            {
                table = DSet.Tables["File"];

                foreach (DataRow r in table.Rows)
                {
					// MWM連携ファイルの出力先フォルダ
					this.Path = r["Path"].ToString().TrimEnd('\\');
                    break;
                }
            }

            if (DSet.Tables.Contains("Title"))
            {
                table = DSet.Tables["Title"];

                foreach (DataRow r in table.Rows)
                {
                    this.Title = r["Text"].ToString();
                    break;
                }
            }

            if (DSet.Tables.Contains("Handle"))
            {
                table = DSet.Tables["Handle"];

                foreach (DataRow r in table.Rows)
                {
                    this.Handle = r["Default"].ToString();
                    break;
                }
            }

            if (DSet.Tables.Contains("Mode"))
            {
                table = DSet.Tables["Mode"];

                foreach (DataRow r in table.Rows)
                {
                    this.Mode = r["Default"].ToString();
                    break;
                }
            }

            if (DSet.Tables.Contains("Auto"))
            {
                table = DSet.Tables["Auto"];

                foreach (DataRow r in table.Rows)
                {
                    int.TryParse(r["Interval"].ToString(), out this.Interval);
                    break;
                }
            }
        }
    }

    enum MWMKind : int
    {
        SekouCode = 1,
        OrderCode = 2
    }

    class MWMCommentItem
    {
        public string Id = "";

        public string Text = "";

        public string Value = "";
    }
}
