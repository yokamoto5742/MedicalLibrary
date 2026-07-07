using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// 感染症項目マスター
    /// </summary>
    public class InfectionMaster : StdEntity
    {
        /// <summary>
        /// 感染症コード
        /// </summary>
        public string Code = "";

        /// <summary>
        /// 項目名
        /// </summary>
        public string Name = "";

        /// <summary>
        /// 感染症区分
        /// </summary>
        public string KindCode = "";

        /// <summary>
        /// 表示順
        /// </summary>
        public int ShowSEQ = 0;

        /// <summary>
        /// 検査値のリスト
        /// </summary>
        public List<InfectionValueMaster> ValueMasterList = new List<InfectionValueMaster>();

        static List<InfectionMaster> list = new List<InfectionMaster>();

        public static List<InfectionMaster> ListAll
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
            List<InfectionValueMaster> vm_list = InfectionValueMaster.ListAll;

            list.Clear();
#if INNO
            string cmd = "select * from M_INFECTION t " +
                " order by t.DISP_SEQ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                InfectionMaster obj = new InfectionMaster();

                obj.Code = tmp.DataDict["CODE"].ToString();
                obj.Name = tmp.DataDict["NAME"].ToString();
                obj.KindCode = tmp.DataDict["INFECTION_TYPE"].ToString();
                int.TryParse(tmp.DataDict["DISP_SEQ"].ToString(), out obj.ShowSEQ);

                // 検査値マスターのリストをセット
                foreach (InfectionValueMaster vm in vm_list)
                {
                    if (vm.Code.Equals(obj.Code))
                    {
                        obj.ValueMasterList.Add(vm);
                    }
                }

                list.Add(obj);
            }
#else
            string cmd = "select * from AMB_感染症項目マスター t " +
                " order by t.表示順";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                InfectionMaster obj = new InfectionMaster();

                obj.Code = tmp.DataDict["感染症コード"].ToString();
                obj.Name = tmp.DataDict["項目名"].ToString();
                obj.KindCode = tmp.DataDict["感染症区分"].ToString();
                int.TryParse(tmp.DataDict["表示順"].ToString(), out obj.ShowSEQ);

                // 検査値マスターのリストをセット
                foreach (InfectionValueMaster vm in vm_list)
                {
                    if (vm.Code.Equals(obj.Code))
                    {
                        obj.ValueMasterList.Add(vm);
                    }
                }

                list.Add(obj);
            }
#endif
        }
    }

    /// <summary>
    /// 感染症検査値マスター
    /// </summary>
    public class InfectionValueMaster : StdEntity
    {
        /// <summary>
        /// 感染症コード
        /// </summary>
        public string Code = "";

        /// <summary>
        /// 連番
        /// </summary>
        public int SEQ = 0;

        /// <summary>
        /// 検査値
        /// </summary>
        public string Val = "";

        /// <summary>
        /// 検査結果区分
        /// </summary>
        public string KindCode = "";

        /// <summary>
        /// 表示順
        /// </summary>
        public int ShowSEQ = 0;

        static List<InfectionValueMaster> list = new List<InfectionValueMaster>();

        public static List<InfectionValueMaster> ListAll
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
            string cmd = "select * from M_INFECTION_TYPE t " +
                " order by t.CODE, t.DISP_SEQ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                InfectionValueMaster obj = new InfectionValueMaster();

                obj.Code = tmp.DataDict["CODE"].ToString();
                int.TryParse(tmp.DataDict["SEQ"].ToString(), out obj.SEQ);
                obj.Val = tmp.DataDict["RESULT"].ToString();
                obj.KindCode = tmp.DataDict["RESULT_TYPE"].ToString();
                int.TryParse(tmp.DataDict["DISP_SEQ"].ToString(), out obj.ShowSEQ);

                list.Add(obj);
            }
#else
            string cmd = "select * from AMB_感染症検査値マスター t " +
                " order by t.感染症コード, t.表示順";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                InfectionValueMaster obj = new InfectionValueMaster();

                obj.Code = tmp.DataDict["感染症コード"].ToString();
                int.TryParse(tmp.DataDict["連番"].ToString(), out obj.SEQ);
                obj.Val = tmp.DataDict["検査値"].ToString();
                obj.KindCode = tmp.DataDict["検査結果区分"].ToString();
                int.TryParse(tmp.DataDict["表示順"].ToString(), out obj.ShowSEQ);

                list.Add(obj);
            }
#endif
        }
    }
}
