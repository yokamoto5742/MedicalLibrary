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
#if INNO
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
#else
            string cmd = "select * from AMB_禁忌分類項目マスター t " +
                " order by t.禁忌分類コード, t.表示順";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                AllergyGroupMaster obj = new AllergyGroupMaster();

                obj.Code = tmp.DataDict["禁忌分類コード"].ToString();
                obj.Name = tmp.DataDict["項目名"].ToString();
                int.TryParse(tmp.DataDict["表示順"].ToString(), out obj.ShowSEQ);

                foreach (AllergyMaster am in am_list)
                {
                    if (am.GroupCode.Equals(obj.Code))
                    {
                        obj.MasterList.Add(am);
                    }
                }

                list.Add(obj);
            }
#endif
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
#if INNO
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
#else
            string cmd = "select * from AMB_禁忌項目マスター t " +
                " order by t.禁忌分類コード, t.表示順";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                AllergyMaster obj = new AllergyMaster();

                obj.GroupCode = tmp.DataDict["禁忌分類コード"].ToString();
                int.TryParse(tmp.DataDict["連番"].ToString(), out obj.SEQ);
                obj.Name = tmp.DataDict["項目名"].ToString();
                int.TryParse(tmp.DataDict["表示順"].ToString(), out obj.ShowSEQ);

                list.Add(obj);
            }
#endif
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
#if INNO
            string cmd = "select * from D_ALLERGY t " +
                " where t.P_ID = " + pt_id +
                " and INPUT_TYPE = 0 " +
                " order by t.CLASS_NO, t.ITEM_NO";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }
#else
            string cmd = "select * from ADT_アレルギー禁忌情報データ t " +
                " where t.患者コード = " + pt_id +
                " order by t.禁忌分類コード, t.禁忌項目連番";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }
#endif
            return list;
        }


        static AllergyData GetFromStdClass(StdClass tmp)
        {
            AllergyData obj = new AllergyData();

            obj.BaseFromStdClass(tmp);
#if INNO
            obj.GroupCode = tmp.DataDict["CLASS_NO"].ToString();
            int.TryParse(tmp.DataDict["ITEM_NO"].ToString(), out obj.SEQ);
            obj.Name = tmp.DataDict["ITEM_NAME"].ToString();
            obj.Cont = tmp.DataDict["ITEM_COMMENT"].ToString();
#else
            obj.GroupCode = tmp.DataDict["禁忌分類コード"].ToString();
            int.TryParse(tmp.DataDict["禁忌項目連番"].ToString(), out obj.SEQ);
            obj.Name = tmp.DataDict["禁忌対象項目"].ToString();
            obj.Cont = tmp.DataDict["コメント"].ToString();
#endif
            return obj;
        }

        public StdReturn Save()
        {
            StdReturn sr = new StdReturn();

            if (this.PtId.Length == 0 || this.GroupCode.Length == 0 || this.SEQ == 0)
            {
                return sr;
            }

            StdDbClass obj = new StdDbClass();
#if INNO
#else
            obj.Db = DB.Db1;
            obj.Table = "ADT_アレルギー禁忌情報データ";

            string reg_date = DateTime.Now.ToString("yyyyMMdd");
            string reg_time = DateTime.Now.ToString("HHmmss");

            obj.DataList.Add(new StdDbColumn("禁忌対象項目", StdDbType.VARCHAR2, this.Name));
            obj.DataList.Add(new StdDbColumn("コメント", StdDbType.VARCHAR2, this.Cont));
            obj.DataList.Add(new StdDbColumn("更新日", StdDbType.NUMBER, reg_date));
            obj.DataList.Add(new StdDbColumn("更新時間", StdDbType.NUMBER, reg_time));
            obj.DataList.Add(new StdDbColumn("更新者", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("代行更新者", StdDbType.NUMBER, LoginUser.Id2));

            obj.WhereList.Add("患者コード = " + this.PtId);
            obj.WhereList.Add("禁忌分類コード = " + this.GroupCode);
            obj.WhereList.Add("禁忌項目連番 = " + this.SEQ);

            sr = obj.UpdateSQL();

            if (sr.IntValue == 0)
            {
                obj.DataList.Add(new StdDbColumn("患者コード", StdDbType.NUMBER, this.PtId));
                obj.DataList.Add(new StdDbColumn("禁忌分類コード", StdDbType.NUMBER, this.GroupCode));
                obj.DataList.Add(new StdDbColumn("禁忌項目連番", StdDbType.NUMBER, this.SEQ));

                obj.DataList.Add(new StdDbColumn("登録日", StdDbType.NUMBER, reg_date));
                obj.DataList.Add(new StdDbColumn("登録時間", StdDbType.NUMBER, reg_time));
                obj.DataList.Add(new StdDbColumn("登録者", StdDbType.NUMBER, LoginUser.Id));
                obj.DataList.Add(new StdDbColumn("代行登録者", StdDbType.NUMBER, LoginUser.Id2));

                sr = obj.InsertSQL();
            }
#endif
            return sr;
        }

        public StdReturn Delete()
        {
            StdReturn sr = new StdReturn();

            if (this.PtId.Length == 0 || this.GroupCode.Length == 0 || this.SEQ == 0)
            {
                return sr;
            }

            StdDbClass obj = new StdDbClass();
#if INNO
#else
            obj.Db = DB.Db1;
            obj.Table = "ADT_アレルギー禁忌情報データ";

            obj.WhereList.Add("患者コード = " + this.PtId);
            obj.WhereList.Add("禁忌分類コード = " + this.GroupCode);
            obj.WhereList.Add("禁忌項目連番 = " + this.SEQ);

            sr = obj.DeleteSQL();
#endif
            return sr;
        }
    }
}
