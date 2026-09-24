using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class AllergyGroupMaster : StdEntity
    {
        /// <summary>
        /// 禁忌分類コード
        /// </summary>
        public string Code = "";

        /// <summary>
        /// 項目名
        /// </summary>
        public string Name = "";

        /// <summary>
        /// 表示順
        /// </summary>
        public int ShowSEQ = 0;

        public List<AllergyMaster> MasterList = new List<AllergyMaster>();

        public override string ToString()
        {
            return this.Name;
        }

        static List<AllergyGroupMaster> list = new List<AllergyGroupMaster>();

        public static List<AllergyGroupMaster> ListAll
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

        static void Init()
        {
            List<AllergyMaster> am_list = AllergyMaster.ListAll;

            list.Clear();
            string cmd = "select * from M_ALLERGY_CLASS t " +
                " order by t.CLASS_NO, t.DISP_SEQ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                AllergyGroupMaster obj = new AllergyGroupMaster();

                obj.Code = tmp.DataDict["CLASS_NO"].ToString();
                obj.Name = tmp.DataDict["NAME"].ToString();
                int.TryParse(tmp.DataDict["DISP_SEQ"].ToString(), out obj.ShowSEQ);

                foreach (AllergyMaster am in am_list)
                {
                    if (am.GroupCode.Equals(obj.Code))
                    {
                        obj.MasterList.Add(am);
                    }
                }

                list.Add(obj);
            }
        }
    }

    public class AllergyMaster : StdEntity
    {
        /// <summary>
        /// 禁忌分類コード
        /// </summary>
        public string GroupCode = "";

        /// <summary>
        /// 連番
        /// </summary>
        public int SEQ = 0;

        /// <summary>
        /// 項目名
        /// </summary>
        public string Name = "";

        /// <summary>
        /// 表示順
        /// </summary>
        public int ShowSEQ = 0;

        public override string ToString()
        {
            return this.Name;
        }

        static List<AllergyMaster> list = new List<AllergyMaster>();

        public static List<AllergyMaster> ListAll
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

        static void Init()
        {
            list.Clear();
            string cmd = "select * from M_ALLERGY_ITEM t " +
                " where INPUT_TYPE = 0 " +
                " order by t.CLASS_NO, t.ITEM_NO";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                AllergyMaster obj = new AllergyMaster();

                obj.GroupCode = tmp.DataDict["CLASS_NO"].ToString();
                int.TryParse(tmp.DataDict["ITEM_NO"].ToString(), out obj.SEQ);
                obj.Name = tmp.DataDict["NAME"].ToString();
                int.TryParse(tmp.DataDict["DISP_SEQ"].ToString(), out obj.ShowSEQ);

                list.Add(obj);
            }
        }
    }

    public class AllergyData : StdKarte1
    {
        /// <summary>
        /// 禁忌分類コード
        /// </summary>
        public string GroupCode = "";

        public string GroupName
        {
            get
            {
                string s = "";

                foreach (AllergyGroupMaster m in AllergyGroupMaster.ListAll)
                {
                    if (this.GroupCode.Equals(m.Code))
                    {
                        s = m.Name;
                        break;
                    }
                }

                return s;
            }
        }

        /// <summary>
        /// 禁忌項目連番
        /// </summary>
        public int SEQ = 0;

        /// <summary>
        /// 禁忌対象項目
        /// </summary>
        public string Name = "";

        /// <summary>
        /// コメント
        /// </summary>
        public string Cont = "";

        public static List<AllergyData> GetList(string pt_id)
        {
            List<AllergyData> list = new List<AllergyData>();

            if (pt_id.Length == 0)
            {
                return list;
            }
            string cmd = "select * from D_ALLERGY t " +
                " where t.P_ID = " + pt_id +
                " and INPUT_TYPE = 0 " +
                " order by t.CLASS_NO, t.ITEM_NO";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }
            return list;
        }


        static AllergyData GetFromStdClass(StdClass tmp)
        {
            AllergyData obj = new AllergyData();

            obj.BaseFromStdClass(tmp);
            obj.GroupCode = tmp.DataDict["CLASS_NO"].ToString();
            int.TryParse(tmp.DataDict["ITEM_NO"].ToString(), out obj.SEQ);
            obj.Name = tmp.DataDict["ITEM_NAME"].ToString();
            obj.Cont = tmp.DataDict["ITEM_COMMENT"].ToString();
            return obj;
        }

    }
}
