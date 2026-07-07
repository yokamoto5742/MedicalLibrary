using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using MedicalLibrary.Boundary;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class InspectSettings
    {
        public static InspectSettings Current = new InspectSettings();

        /// <summary>
        /// 自動起動する科
        /// </summary>
        public string AutoExecDept = "";

        public List<InspectSets> InspectSetsList = new List<InspectSets>();

        public List<InspectSet> InspectSetList = new List<InspectSet>();

        public List<InspectKind> InspectKindList = new List<InspectKind>();


        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="force"></param>
        public static void Init(bool force = false)
        {
            // force = false かつ InspectList が存在する場合は終了
            if (!force && Current.InspectSetList.Count > 0)
            {
                return;
            }

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(AppFile.FilePath("MedicalSupport_Settings.xml"));

                // いったんクリアする
                Current.AutoExecDept = "";
                Current.InspectSetList.Clear();
                Current.InspectKindList.Clear();

                if (xmlDoc.SelectSingleNode("MedicalSupport/AutoExecDept") != null)
                {
                    Current.AutoExecDept = xmlDoc.SelectSingleNode("MedicalSupport/AutoExecDept").InnerText;
                }

                foreach (XmlNode n in xmlDoc.SelectNodes("MedicalSupport/InspectSets"))
                {
                    XmlElement nn = (XmlElement)n;

                    InspectSets obj = new InspectSets();

                    if (nn.GetAttribute("Dept") != null)
                    {
                        obj.Dept = nn.GetAttribute("Dept");
                    }

                    obj.Sets = n.InnerText;

                    Current.InspectSetsList.Add(obj);
                }

                foreach (XmlNode n in xmlDoc.SelectNodes("MedicalSupport/InspectSet"))
                {
                    XmlElement nn = (XmlElement)n;

                    if (nn.GetAttribute("Id") == null ||
                        n.SelectSingleNode("Name") == null)
                    {
                        continue;
                    }

                    InspectSet obj = new InspectSet();

                    obj.Id = nn.GetAttribute("Id");
                    obj.Name = n.SelectSingleNode("Name").InnerText;

                    if (n.SelectSingleNode("Kinds") != null) obj.Kinds = n.SelectSingleNode("Kinds").InnerText;
                    if (n.SelectSingleNode("MonthAgo") != null) obj.MonthAgo = n.SelectSingleNode("MonthAgo").InnerText;

                    Current.InspectSetList.Add(obj);
                }

                foreach (XmlNode n in xmlDoc.SelectNodes("MedicalSupport/InspectKind"))
                {
                    XmlElement nn = (XmlElement)n;

                    if (nn.GetAttribute("Id") == null ||
                        n.SelectSingleNode("Name") == null)
                    {
                        continue;
                    }

                    InspectKind obj = new InspectKind();

                    obj.Id = nn.GetAttribute("Id");
                    obj.Name = n.SelectSingleNode("Name").InnerText;

                    if (n.SelectSingleNode("RedFlg") != null) obj.RedFlg = n.SelectSingleNode("RedFlg").InnerText;
                    if (n.SelectSingleNode("OutFlg") != null) obj.OutFlg = n.SelectSingleNode("OutFlg").InnerText;

                    foreach (XmlNode nnn in n.SelectNodes("Order"))
                    {
                        if (nnn.SelectSingleNode("OrderCode") == null) continue;

                        InspectOrder obj2 = new InspectOrder();

                        obj2.OrderCode = nnn.SelectSingleNode("OrderCode").InnerText;
                        obj2.Name = nnn.SelectSingleNode("Name").InnerText;

                        obj.OrderList.Add(obj2);
                    }

                    Current.InspectKindList.Add(obj);
                }
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex, false);
            }
        }
    }

    /// <summary>
    /// 科ごとの InspectSet のリスト
    /// </summary>
    public class InspectSets
    {
        public string Dept = "";

        public string Sets = "";

        /// <summary>
        /// InspectSet のリスト
        /// </summary>
        public List<InspectSet> SetList
        {
            get
            {
                List<InspectSet> list = new List<InspectSet>();

                foreach (string s in this.Sets.Split(','))
                {
                    foreach (InspectSet obj in InspectSettings.Current.InspectSetList)
                    {
                        if (obj.Id.Equals(s))
                        {
                            list.Add(obj);
                        }
                    }
                }
                return list;
            }
        }

        /// <summary>
        /// 科に該当するデータを取得する
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public static InspectSets GetDataByDept(string dept)
        {
            InspectSets obj = new InspectSets();

            foreach (InspectSets obj2 in InspectSettings.Current.InspectSetsList)
            {
                if (obj2.Dept.Equals(dept))
                {
                    obj = obj2;
                    break;
                }
            }

            return obj;
        }
    }

    /// <summary>
    /// 検索する検査セット
    /// </summary>
    public class InspectSet
    {
        public string Id = "";

        public string Name = "";

        public string Kinds = "";

        public string MonthAgo = "12";


        /// <summary>
        /// 検索対象の検査リスト
        /// </summary>
        public List<InspectKind> KindList
        {
            get
            {
                List<InspectKind> list = new List<InspectKind>();

                foreach (string s in this.Kinds.Split(','))
                {
                    foreach (InspectKind obj in InspectSettings.Current.InspectKindList)
                    {
                        if (obj.Id.Equals(s))
                        {
                            list.Add(obj);
                            break;
                        }
                    }
                }

                return list;
            }
        }

        /// <summary>
        /// データを取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static InspectSet GetData(string id)
        {
            InspectSet obj = new InspectSet();

            foreach (InspectSet obj2 in InspectSettings.Current.InspectSetList)
            {
                if (obj2.Id.Equals(id))
                {
                    obj = obj2;
                    break;
                }
            }

            return obj;
        }
    }

    /// <summary>
    /// 検索対象の検査
    /// </summary>
    public class InspectKind
    {
        public string Id = "";

        public string Name = "";

        public string RedFlg = "";

        public string OutFlg = "";

        public List<InspectOrder> OrderList = new List<InspectOrder>();

        /// <summary>
        /// データを取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static InspectKind GetData(string id)
        {
            InspectKind obj = new InspectKind();

            foreach (InspectKind obj2 in InspectSettings.Current.InspectKindList)
            {
                if (obj2.Id.Equals(id))
                {
                    obj = obj2;
                    break;
                }
            }

            return obj;
        }
    }

    /// <summary>
    /// 検索対象のオーダーコード
    /// </summary>
    public class InspectOrder
    {
        /// <summary>
        /// オーダーコード（カンマ区切り）
        /// </summary>
        public string OrderCode = "";

        public List<string> OrderCodeList
        {
            get
            {
                return this.OrderCode.Split(',').ToList<string>();
            }
        }

        /// <summary>
        /// 表示名称
        /// </summary>
        public string Name = "";
    }


    /// <summary>
    /// 検査データ
    /// </summary>
    public class InspectData
    {
        public string PtId = "";

        PatBase _Pat = new PatBase();

        public PatBase Pat
        {
            get
            {
                if (!this._Pat.Id.Equals(this.PtId))
                {
                    this._Pat = PatBase.Load(this.PtId);
                }

                return this._Pat;
            }
        }

        public string InspectDate = "";

        public string Kind = "";

        public string KindName
        {
            get
            {
                return InspectKind.GetData(this.Kind).Name;
            }
        }

        public string Cont = "";

        public string ContValue
        {
            get
            {
                string s = DateTimeAgent.DateFormat(this.InspectDate, DateTimeAgent.DateFormatKind.SHORT) + " " + this.Cont;

                if (this.DoctorName.Length > 0)
                {
                    s += " [" + this.DoctorName + "]";
                }
                else if (this.StaffName.Length > 0)
                {
                    s += " [" + this.StaffName + "]";
                }

                if (!this.SekouFlg)
                {
                    s += " ▲";
                }

                if (this.OutFlg)
                {
                    s += " ☆";
                }

                return s;
            }
        }

        public string Doctor = "";

        public string DoctorName
        {
            get
            {
                return Dict.DoctorDict.ContainsKey(this.Doctor) ? Dict.DoctorDict[this.Doctor].Name.Replace("　", " ").Replace(" ", "") : "";
            }
        }

        public string Staff = "";

        public string StaffName
        {
            get
            {
                return Dict.StaffDict.ContainsKey(this.Staff) ? Dict.StaffDict[this.Staff].Name.Replace("　", " ").Replace(" ", "") : "";
            }
        }

        public string SaveDate = "";

        public string SaveTime = "";

        /// <summary>
        /// 施行済フラグ
        /// </summary>
        public bool SekouFlg = false;

        /// <summary>
        /// 院外検査
        /// </summary>
        public bool OutFlg = false;


        public static List<InspectData> GetOutList(string pt_id, string date1, string date2)
        {
            List<InspectData> list = new List<InspectData>();

            if (pt_id.Length == 0 || !DateTimeAgent.IsDate(date1) || !DateTimeAgent.IsDate(date2))
            {
                return list;
            }

            string cmd = "select * from INSPECT_OUT" +
                " where PT_ID = " + pt_id +
                " and INSPECT_DATE >= " + date1 + " and INSPECT_DATE <= " + date2 +
                " order by INSPECT_DATE desc, KIND";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                InspectData obj = GetFromStdClass(tmp);

                // DBから取得するのは基本的に院外検査のみ・施行済
                obj.SekouFlg = true;
                obj.OutFlg = true;

                list.Add(obj);
            }

            return list;
        }

        static InspectData GetFromStdClass(StdClass tmp)
        {
            InspectData obj = new InspectData();

            obj.PtId = tmp.GetDataString("PT_ID");
            obj.InspectDate = tmp.GetDataString("INSPECT_DATE");
            obj.Kind = tmp.GetDataString("KIND").Trim();
            obj.Cont = tmp.GetDataString("CONT").Trim();
            obj.Staff = tmp.GetDataString("SAVE_STAFF");
            obj.SaveDate = tmp.GetDataString("SAVE_DATE");
            obj.SaveTime = tmp.GetDataString("SAVE_TIME");

            // DBから取得するのは基本的に院外検査のみ・施行済
            obj.SekouFlg = true;
            obj.OutFlg = true;

            return obj;
        }

        public StdReturn Save()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();

            obj.Table = "INSPECT_OUT";
            obj.Db = DB.Db2;

            obj.DataList.Add(new StdDbColumn("CONT", StdDbType.VARCHAR2, this.Cont));
            obj.DataList.Add(new StdDbColumn("SAVE_STAFF", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("SAVE_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("SAVE_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));

            obj.WhereList.Add("PT_ID = " + this.PtId);
            obj.WhereList.Add("INSPECT_DATE = " + this.InspectDate);
            obj.WhereList.Add("KIND = " + this.Kind);

            sr = obj.UpdateSQL();

            if (sr.IntValue == 0)
            {
                obj.DataList.Add(new StdDbColumn("PT_ID", StdDbType.NUMBER, this.PtId));
                obj.DataList.Add(new StdDbColumn("INSPECT_DATE", StdDbType.NUMBER, this.InspectDate));
                obj.DataList.Add(new StdDbColumn("KIND", StdDbType.NUMBER, this.Kind));
                sr = obj.InsertSQL();
            }

            return sr;
        }
    }


    /// <summary>
    /// 検査グループ（基本的に院外検査）
    /// </summary>
    class InspectDataGroup
    {
        public string PtId = "";

        public string InspectDate = "";

        public string Cont
        {
            get
            {
                string s = "";

                foreach (InspectData io in this.InspectDataList)
                {
                    if (s.Length > 0)
                    {
                        s += Environment.NewLine;
                    }

                    s += "【" + InspectKind.GetData(io.Kind).Name + "】 " + io.Cont;
                }

                return s;
            }
        }

        public string Staff
        {
            get
            {
                string s = "";

                foreach (InspectData io in this.InspectDataList)
                {
                    s += io.Staff;
                    break;
                }

                return s;
            }
        }

        public string StaffName
        {
            get
            {
                return Dict.StaffDict.ContainsKey(this.Staff) ? Dict.StaffDict[this.Staff].Name : "";
            }
        }

        public string SaveDate
        {
            get
            {
                string s = "";

                foreach (InspectData io in this.InspectDataList)
                {
                    s += io.SaveDate;
                    break;
                }

                return s;
            }
        }

        public string SaveTime
        {
            get
            {
                string s = "";

                foreach (InspectData io in this.InspectDataList)
                {
                    s += io.SaveTime;
                    break;
                }

                return s;
            }
        }

        public List<InspectData> InspectDataList = new List<InspectData>();


        public static List<InspectDataGroup> GetList(List<InspectData> io_list)
        {
            List<InspectDataGroup> list = new List<InspectDataGroup>();

            foreach (InspectData io in io_list)
            {
                bool b = false;

                foreach (InspectDataGroup g in list)
                {
                    // すでに list に存在する場合
                    if (io.PtId.Equals(g.PtId) && io.InspectDate.Equals(g.InspectDate))
                    {
                        g.InspectDataList.Add(io);
                        b = true;
                        break;
                    }
                }

                // list に存在しない場合
                if (!b)
                {
                    InspectDataGroup g = new InspectDataGroup();

                    g.PtId = io.PtId;
                    g.InspectDate = io.InspectDate;
                    g.InspectDataList.Add(io);

                    list.Add(g);
                }
            }

            return list;
        }


        /// <summary>
        /// 削除だけは一括で行う（登録は InspectData ごと）
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="date"></param>
        public static void Delete(string pt_id, string date)
        {
            if (pt_id.Length == 0 || !DateTimeAgent.IsDate(date))
            {
                return;
            }

            DB.Db2.ExecuteNonQuery("delete from INSPECT_OUT where PT_ID = " + pt_id + " and INSPECT_DATE = " + date);
        }
    }
}
