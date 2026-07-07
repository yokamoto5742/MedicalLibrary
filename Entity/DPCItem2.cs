using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class DPCItem2
    {
        public string Code = "";

        public string Name = "";

        public bool Visible = true;

        /// <summary>
        /// 表示するタブ
        /// 1: 共通, 2: 医師, 3: 看護師（入棟）, 4: 看護師（退棟）, 5: 手術
        /// </summary>
        public string Kind = "";

        /// <summary>
        /// デフォルト値
        /// </summary>
        public string Default = "";

        /// <summary>
        /// 固定値
        /// </summary>
        public string Fix = "";

        /// <summary>
        /// 診断コード
        /// （値が入っている場合は、患者の診断に含まれる場合のみ表示する。複数ある場合はカンマ区切り）
        /// </summary>
        public string Diag = "";

        public List<string> DiagList
        {
            get
            {
                return this.Diag.Split(',').ToList<string>();
            }
        }

        public string Box = "";

        /// <summary>
        /// 方向
        /// 1: 縦, 2: 横
        /// </summary>
        public string Location = "1";

        /// <summary>
        /// 左マージン
        /// </summary>
        public int MarginLeft = 0;

        /// <summary>
        /// 上マージン
        /// </summary>
        public int MarginTop = 0;

        /// <summary>
        /// 幅
        /// </summary>
        public int Width = 160;

        /// <summary>
        /// 高さ（最低でも 21）
        /// </summary>
        public int Height = 21;

        /// <summary>
        /// 複数行（TextBox のみ）
        /// </summary>
        public bool Multiline = false;

        /// <summary>
        /// 最大文字数
        /// </summary>
        public int MaxLength = 300;

        /// <summary>
        /// 正規表現パターン
        /// </summary>
        public string Pattern = "";

        /// <summary>
        /// 入力例
        /// </summary>
        public string Sample = "";

        /// <summary>
        /// ImeMode（TextBox のみ）
        /// </summary>
        public string ImeMode = "";

        /// <summary>
        /// 最大値（NumBox のみ）
        /// </summary>
        public double MaxValue = 99999999;

        /// <summary>
        /// 最小値（NumBox のみ）
        /// </summary>
        public double MinValue = -99999999;

        /// <summary>
        /// ComboBox のスタイル
        /// （デフォルト DropDownList）
        /// </summary>
        public string DropDownStyle = "DropDownList";

        /// <summary>
        /// ReadOnly（TextBox 系のみ）
        /// </summary>
        public bool ReadOnly = false;


        public List<DPCSubItem2> SubItemList = new List<DPCSubItem2>();


        static List<DPCItem2> list = new List<DPCItem2>();

        public static List<DPCItem2> List
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

        static Dictionary<string, DPCItem2> dict = new Dictionary<string, DPCItem2>();

        public static Dictionary<string, DPCItem2> Dict
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
        /// XMLファイルを読み込んでアイテムリストを作成する
        /// </summary>
        /// <param name="force"></param>
        public static void Init(bool force = false)
        {
            if (!force && list.Count > 0)
            {
                return;
            }

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(AppFile.FilePath("DPCItem2.xml"));

                foreach (XmlNode n in xmlDoc.SelectNodes("MedicalLibrary/DPCItem2"))
                {
                    if (n.SelectSingleNode("Code") == null ||
                        n.SelectSingleNode("Name") == null)
                    {
                        continue;
                    }

                    DPCItem2 obj = new DPCItem2();

                    obj.Code = n.SelectSingleNode("Code").InnerText;
                    obj.Name = n.SelectSingleNode("Name").InnerText;

                    if (n.SelectSingleNode("Visible") != null) obj.Visible = n.SelectSingleNode("Visible").InnerText.Equals("1");
                    if (n.SelectSingleNode("Kind") != null) obj.Kind = n.SelectSingleNode("Kind").InnerText;
                    if (n.SelectSingleNode("Diag") != null) obj.Diag = n.SelectSingleNode("Diag").InnerText;

                    if (n.SelectSingleNode("Box") != null)
                    {
                        XmlElement nn = (XmlElement)n.SelectSingleNode("Box");
                        obj.Box = nn.InnerText.Length > 0 ? nn.InnerText : "TextBox";

                        if (!int.TryParse(nn.GetAttribute("Width"), out obj.Width))
                        {
                            if (obj.Box.Equals("DateBox", StringComparison.CurrentCultureIgnoreCase))
                            {
                                obj.Width = 75;
                            }
                            else
                            {
                                obj.Width = 160;
                            }
                        }

                        if (!int.TryParse(nn.GetAttribute("Height"), out obj.Height) || obj.Height < 21) obj.Height = 21;
                        if (!int.TryParse(nn.GetAttribute("MaxLength"), out obj.MaxLength) || obj.MaxLength > 300) obj.MaxLength = 300;
                        if (nn.GetAttribute("Pattern") != null) obj.Pattern = nn.GetAttribute("Pattern");
                        if (nn.GetAttribute("Sample") != null) obj.Sample = nn.GetAttribute("Sample");
                        if (nn.GetAttribute("Default") != null) obj.Default = nn.GetAttribute("Default");
                        if (nn.GetAttribute("ReadOnly") != null) obj.ReadOnly = nn.GetAttribute("ReadOnly").Equals("True", StringComparison.CurrentCultureIgnoreCase);

                        if (obj.Box.Equals("TextBox", StringComparison.CurrentCultureIgnoreCase))
                        {
                            if (nn.GetAttribute("Multiline") != null) obj.Multiline = nn.GetAttribute("Multiline").Equals("True", StringComparison.CurrentCultureIgnoreCase);
                            if (nn.GetAttribute("ImeMode") != null) obj.ImeMode = nn.GetAttribute("ImeMode");
                        }
                        else if (obj.Box.Equals("NumBox", StringComparison.CurrentCultureIgnoreCase))
                        {
                            if (!double.TryParse(nn.GetAttribute("MaxValue"), out obj.MaxValue)) obj.MaxValue = 99999999;
                            if (!double.TryParse(nn.GetAttribute("MinValue"), out obj.MinValue)) obj.MinValue = -99999999;
                        }
                        else if (obj.Box.Equals("ComboBox", StringComparison.CurrentCultureIgnoreCase))
                        {
                            if (nn.GetAttribute("DropDownStyle") != null) obj.DropDownStyle = nn.GetAttribute("DropDownStyle");
                        }
                    }
                    else
                    {
                        obj.Box = "TextBox";
                    }

                    if (n.SelectSingleNode("Fix") != null) obj.Fix = n.SelectSingleNode("Fix").InnerText;

                    foreach (XmlNode nn in n.SelectNodes("Values"))
                    {
                        if (nn.SelectSingleNode("Text") == null)
                        {
                            continue;
                        }

                        DPCSubItem2 item = new DPCSubItem2();
                        item.Code = (nn.SelectSingleNode("Code") != null) ? nn.SelectSingleNode("Code").InnerText : "";
                        item.Name = (nn.SelectSingleNode("Name") != null) ? nn.SelectSingleNode("Name").InnerText : "";
                        item.Text = nn.SelectSingleNode("Text").InnerText;

                        obj.SubItemList.Add(item);
                    }

                    if (n.SelectSingleNode("Location") != null)
                    {
                        XmlElement nn = (XmlElement)n.SelectSingleNode("Location");
                        obj.Location = nn.InnerText;

                        if (!int.TryParse(nn.GetAttribute("MarginLeft"), out obj.MarginLeft)) obj.MarginLeft = 0;
                        if (!int.TryParse(nn.GetAttribute("MarginTop"), out obj.MarginTop)) obj.MarginTop = 0;
                    }

                    list.Add(obj);
                    dict.Add(obj.Code, obj);
                }
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex, false);
            }
        }
    }

    public class DPCSubItem2
    {
        public string Code = "";

        public string Name = "";

        public string Text = "";

        public override string ToString()
        {
            return (this.Code.Length > 0) ? (this.Code + ": " + this.Text) : this.Text;
        }

        public string Value
        {
            get
            {
                return (this.Code.Length > 0) ? this.Code : this.Text;
            }
        }
    }


    public class DPCDept
    {
        public string Code = "";

        public string Code2 = "";

        public string Name = "";

        static List<DPCDept> list = new List<DPCDept>();

        public static List<DPCDept> List
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

        public override string ToString()
        {
            return this.Code + " " + this.Name;
        }

        /// <summary>
        /// XMLファイルを読み込んでアイテムリストを作成する
        /// </summary>
        /// <param name="force"></param>
        public static void Init(bool force = false)
        {
            if (!force && list.Count > 0)
            {
                return;
            }

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(AppFile.FilePath("DPCItem2.xml"));

                foreach (XmlNode n in xmlDoc.SelectNodes("MedicalLibrary/DPCDept"))
                {
                    if (n.SelectSingleNode("Code") == null ||
                        n.SelectSingleNode("Name") == null)
                    {
                        continue;
                    }

                    DPCDept obj = new DPCDept();

                    obj.Code = n.SelectSingleNode("Code").InnerText;
                    obj.Code2 = n.SelectSingleNode("Code2").InnerText;
                    obj.Name = n.SelectSingleNode("Name").InnerText;

                    list.Add(obj);
                }
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex, false);
            }
        }

        public static DPCDept GetData(string code)
        {
            DPCDept obj = new DPCDept();

            foreach (DPCDept obj2 in List)
            {
                if (obj2.Code.Equals(code))
                {
                    obj = obj2;
                    break;
                }
            }

            return obj;
        }

        /// <summary>
        /// 院内の科コードからDPC科を検索する
        /// </summary>
        /// <param name="code2"></param>
        /// <returns></returns>
        public static DPCDept GetDataByCode2(string code2)
        {
            DPCDept obj = new DPCDept();

            foreach (DPCDept obj2 in List)
            {
                if (obj2.Code2.Split(',').ToList<string>().Contains(code2))
                {
                    obj = obj2;
                    break;
                }
            }

            return obj;
        }
    }

    public class DPCDiag
    {
        public string Code = "";

        public string Name = "";

        static List<DPCDiag> list = new List<DPCDiag>();

        public static List<DPCDiag> List
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
        /// XMLファイルを読み込んでアイテムリストを作成する
        /// </summary>
        /// <param name="force"></param>
        public static void Init(bool force = false)
        {
            if (!force && list.Count > 0)
            {
                return;
            }

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(AppFile.FilePath("DPCItem2.xml"));

                foreach (XmlNode n in xmlDoc.SelectNodes("MedicalLibrary/DPCDiag"))
                {
                    if (n.SelectSingleNode("Code") == null)
                    {
                        continue;
                    }

                    DPCDiag obj = new DPCDiag();

                    obj.Code = n.SelectSingleNode("Code").InnerText;
                    obj.Name = n.SelectSingleNode("Name").InnerText;

                    list.Add(obj);
                }
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex, false);
            }
        }

        public static DPCDiag GetData(string code)
        {
            DPCDiag obj = new DPCDiag();

            foreach (DPCDiag obj2 in List)
            {
                if (obj2.Code.Equals(code))
                {
                    obj = obj2;
                    break;
                }
            }

            return obj;
        }
    }

    public class DPCDir
    {
        public string No = "";

        public string Name = "";

        public string Dir = "";

        static List<DPCDir> list = new List<DPCDir>();

        public static List<DPCDir> List
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
        /// XMLファイルを読み込んでアイテムリストを作成する
        /// </summary>
        /// <param name="force"></param>
        public static void Init(bool force = false)
        {
            if (!force && list.Count > 0)
            {
                return;
            }

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(AppFile.FilePath("DPCItem2.xml"));

                foreach (XmlNode n in xmlDoc.SelectNodes("MedicalLibrary/DPCDir"))
                {
                    if (n.SelectSingleNode("Name") == null ||
                        n.SelectSingleNode("Dir") == null)
                    {
                        continue;
                    }

                    DPCDir obj = new DPCDir();
                    XmlElement nn = (XmlElement)n;

                    if (nn.GetAttribute("No") != null) obj.No = nn.GetAttribute("No");
                    obj.Name = n.SelectSingleNode("Name").InnerText;
                    obj.Dir = n.SelectSingleNode("Dir").InnerText;

                    list.Add(obj);
                }
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex, false);
            }
        }

        /// <summary>
        /// Prrism出力ディレクトリ
        /// </summary>
        public static DPCDir Prrism
        {
            get
            {
                DPCDir obj = new DPCDir();

                foreach (DPCDir dir in List)
                {
                    if (dir.No.Equals("1"))
                    {
                        obj = dir;
                        break;
                    }
                }

                return obj;
            }
        }

        /// <summary>
        /// Proasインポート用ディレクトリ
        /// </summary>
        public static DPCDir Proas
        {
            get
            {
                DPCDir obj = new DPCDir();

                foreach (DPCDir dir in List)
                {
                    if (dir.No.Equals("2"))
                    {
                        obj = dir;
                        break;
                    }
                }

                return obj;
            }
        }

        public static List<DPCDir> GetListByName(string name)
        {
            List<DPCDir> list = new List<DPCDir>();

            foreach (DPCDir dir in List)
            {
                if (dir.Name.Contains(name))
                {
                    list.Add(dir);
                }
            }

            return list;
        }
    }

    public class DPCFile
    {
        public string No = "";

        public string Name = "";

        public string File = "";

        static List<DPCFile> list = new List<DPCFile>();

        public static List<DPCFile> List
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
        /// XMLファイルを読み込んでアイテムリストを作成する
        /// </summary>
        /// <param name="force"></param>
        public static void Init(bool force = false)
        {
            if (!force && list.Count > 0)
            {
                return;
            }

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(AppFile.FilePath("DPCItem2.xml"));

                foreach (XmlNode n in xmlDoc.SelectNodes("MedicalLibrary/DPCFile"))
                {
                    if (n.SelectSingleNode("Name") == null ||
                        n.SelectSingleNode("File") == null)
                    {
                        continue;
                    }

                    DPCFile obj = new DPCFile();
                    XmlElement nn = (XmlElement)n;

                    if (nn.GetAttribute("No") != null) obj.No = nn.GetAttribute("No");
                    obj.Name = n.SelectSingleNode("Name").InnerText;
                    obj.File = n.SelectSingleNode("File").InnerText;

                    if (!System.IO.File.Exists(obj.File)) continue;

                    list.Add(obj);
                }
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex, false);
            }
        }

        public static DPCFile GetData(string no)
        {
            DPCFile obj = new DPCFile();

            foreach (DPCFile file in List)
            {
                if (file.No.Equals(no))
                {
                    obj = file;
                    break;
                }
            }

            return obj;
        }

        public static List<DPCFile> GetListByName(string name)
        {
            List<DPCFile> list = new List<DPCFile>();

            foreach (DPCFile file in List)
            {
                if (file.Name.Contains(name))
                {
                    list.Add(file);
                }
            }

            return list;
        }
    }
}
