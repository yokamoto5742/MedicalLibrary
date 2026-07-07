using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedicalLibrary.Entity
{
    public class PathMaster : StdEntity
    {
        /// <summary>
        /// パスコード
        /// </summary>
        public string Code = "";

        /// <summary>
        /// 名称
        /// </summary>
        public string Name = "";

        /// <summary>
        /// 日数
        /// </summary>
        public int Days = 0;

        /// <summary>
        /// 有効フラグ
        /// </summary>
        public int Flg = 1;

        /// <summary>
        /// 入外区分
        /// </summary>
        public int InOut = 2;

        /// <summary>
        /// コメント
        /// </summary>
        public string Cont = "";

        /// <summary>
        /// タグ名称
        /// </summary>
        public string TagName = "";

        public string[] TagNames
        {
            get
            {
                return this.TagName.Split('\\');
            }
        }

        /// <summary>
        /// マスターディティールのリスト
        /// </summary>
        List<PathMasterTask> task_list = new List<PathMasterTask>();

        public List<PathMasterTask> TaskList
        {
            get
            {
                if (this.task_list.Count == 0)
                {
                    this.task_list = PathMasterTask.GetList(this.Code);
                }

                return this.task_list;
            }
        }


        /// <summary>
        /// PATH_M看護指示テンプレート
        /// </summary>
        Dictionary<int, Dictionary<int, Dictionary<int, NursingOrderPathTemplate>>> nursing_dict = new Dictionary<int, Dictionary<int, Dictionary<int, NursingOrderPathTemplate>>>();

        public Dictionary<int, Dictionary<int, Dictionary<int, NursingOrderPathTemplate>>> NursingDict
        {
            get
            {
                if (this.nursing_dict.Count == 0)
                {
                    this.nursing_dict = NursingOrderPathTemplate.Load(this.Code);
                }

                return this.nursing_dict;
            }
        }


        /// <summary>
        /// PATH_M手術指示テンプレート
        /// </summary>
        Dictionary<int, Dictionary<int, Dictionary<int, OpeOrderPathTemplate>>> ope_dict = new Dictionary<int, Dictionary<int, Dictionary<int, OpeOrderPathTemplate>>>();

        public Dictionary<int, Dictionary<int, Dictionary<int, OpeOrderPathTemplate>>> OpeDict
        {
            get
            {
                if (this.ope_dict.Count == 0)
                {
                    this.ope_dict = OpeOrderPathTemplate.Load(this.Code);
                }

                return this.ope_dict;
            }
        }


        /// <summary>
        /// PATH_M基本指示テンプレート
        /// </summary>
        List<BaseOrderPathTemplate> base_list = new List<BaseOrderPathTemplate>();

        public List<BaseOrderPathTemplate> BaseList
        {
            get
            {
                if (this.base_list.Count == 0)
                {
                    this.base_list = BaseOrderPathTemplate.GetList(this.Code);
                }

                return this.base_list;
            }
        }


        /// <summary>
        /// 全パスのリスト
        /// </summary>
        static List<PathMaster> list_all = new List<PathMaster>();

        /// <summary>
        /// タグありパスのリスト
        /// </summary>
        static List<PathMaster> list_active = new List<PathMaster>();

        /// <summary>
        /// タグなしパスのリスト
        /// </summary>
        static List<PathMaster> list_inactive = new List<PathMaster>();


        public static List<PathMaster> ListAll
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

        public static List<PathMaster> ListActive
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

        public static List<PathMaster> ListInactive
        {
            get
            {
                if (list_inactive.Count == 0)
                {
                    Init();
                }

                return list_inactive;
            }
        }


        static void Init()
        {
            string cmd = "select * from macs.PATH_マスタヘッダー t1, macs.PATH_タグマスタ t2 " +
                " where t1.パスコード = t2.関連付けコード(+) " +
                " order by t1.パスコード";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PathMaster obj = GetFromStdClass(tmp);

                // 全パスのリストには追加する
                list_all.Add(obj);

                if (!obj.Flg.Equals(1))
                {
                    continue;
                }

                // タグの有無で区別して追加する
                if (obj.TagName.Length > 0)
                {
                    list_active.Add(obj);
                }
                else
                {
                    list_inactive.Add(obj);
                }
            }
        }

        public static PathMaster Load(string code)
        {
            PathMaster obj = new PathMaster();

            foreach (PathMaster tmp in PathMaster.ListAll)
            {
                if (tmp.Code.Equals(code))
                {
                    obj = tmp;
                    break;
                }
            }

            return obj;
        }

        static PathMaster GetFromStdClass(StdClass tmp)
        {
            PathMaster obj = new PathMaster();

            obj.Code = tmp.DataDict["パスコード"].ToString();
            obj.Name = tmp.DataDict["名称"].ToString();
            int.TryParse(tmp.DataDict["日数"].ToString(), out obj.Days);
            int.TryParse(tmp.DataDict["有効フラグ"].ToString(), out obj.Flg);
            int.TryParse(tmp.DataDict["入外区分"].ToString(), out obj.InOut);
            obj.Cont = tmp.DataDict["コメント"].ToString();
            obj.TagName = tmp.DataDict["タグ名称"].ToString();

            return obj;
        }
    }
}
