using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedicalLibrary.Entity
{
    public class PathCategoryMaster : StdEntity
    {
        /// <summary>
        /// カテゴリコード
        /// </summary>
        public string Code = "";

        /// <summary>
        /// カテゴリ名
        /// </summary>
        public string Name = "";

        /// <summary>
        /// 背景色
        /// </summary>
        public string BackColorString = "";

        /// <summary>
        /// 並び順
        /// </summary>
        public int SortValue = 0;

        /// <summary>
        /// オーダー診療区分１
        /// </summary>
        public int KouiCode1 = 0;

        /// <summary>
        /// オーダー診療区分２
        /// </summary>
        public int KouiCode2 = 0;

        /// <summary>
        /// オーダー施行部署１
        /// </summary>
        public int SekouDeptCode1 = 0;

        /// <summary>
        /// オーダー施行部署２
        /// </summary>
        public int SekouDeptCode2 = 0;

        /// <summary>
        /// 特殊処理フラグ
        /// </summary>
        public int EtcFlg = 0;


        static Dictionary<string, PathCategoryMaster> dict = new Dictionary<string, PathCategoryMaster>();

        static List<PathCategoryMaster> list_all = new List<PathCategoryMaster>();

        static List<PathCategoryMaster> list_active = new List<PathCategoryMaster>();


        public static Dictionary<string, PathCategoryMaster> Dict
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

        public static List<PathCategoryMaster> ListAll
        {
            get
            {
                if (list_all.Count == 0)
                {
                    Init();
                }

                return list_all;
            }
        }

        public static List<PathCategoryMaster> ListActive
        {
            get
            {
                if (list_active.Count == 0)
                {
                    Init();
                }

                return list_active;
            }
        }


        static void Init()
        {
            string cmd = "select * from macs.PATH_カテゴリマスタ t " +
                " order by t.並び順";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PathCategoryMaster obj = GetFromStdClass(tmp);

                dict.Add(obj.Code, obj);
                list_all.Add(obj);

                if (obj.SortValue > -1)
                {
                    list_active.Add(obj);
                }
            }
        }


        static PathCategoryMaster GetFromStdClass(StdClass tmp)
        {
            PathCategoryMaster obj = new PathCategoryMaster();

            obj.Code = tmp.DataDict["カテゴリコード"].ToString();
            obj.Name = tmp.DataDict["カテゴリ名"].ToString();
            obj.BackColorString = tmp.DataDict["背景色"].ToString();
            int.TryParse(tmp.DataDict["並び順"].ToString(), out obj.SortValue);
            int.TryParse(tmp.DataDict["オーダー診療区分１"].ToString(), out obj.KouiCode1);
            int.TryParse(tmp.DataDict["オーダー診療区分２"].ToString(), out obj.KouiCode2);
            int.TryParse(tmp.DataDict["オーダー施行部署１"].ToString(), out obj.SekouDeptCode1);
            int.TryParse(tmp.DataDict["オーダー施行部署２"].ToString(), out obj.SekouDeptCode2);
            int.TryParse(tmp.DataDict["特殊処理フラグ"].ToString(), out obj.EtcFlg);

            return obj;
        }
    }
}
