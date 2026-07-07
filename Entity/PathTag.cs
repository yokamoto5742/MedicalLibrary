using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedicalLibrary.Entity
{
    public class PathTag : StdEntity
    {
        /// <summary>
        /// タグ名称
        /// </summary>
        public string Name = "";

        /// <summary>
        /// 関連付けコード
        /// </summary>
        public string Code = "";


        public string[] Names
        {
            get
            {
                return this.Name.Split('\\');
            }
        }


        static List<PathTag> list = new List<PathTag>();

        public static List<PathTag> ListAll
        {
            get
            {
                if (list.Count == 0)
                {
                    string cmd = "select * from macs.PATH_タグマスタ t " +
                        " order by t.タグ名称";

                    List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

                    foreach (StdClass tmp in tmp_list)
                    {
                        list.Add(GetFromStdClass(tmp));
                    }
                }

                return list;
            }
        }


        static PathTag GetFromStdClass(StdClass tmp)
        {
            PathTag obj = new PathTag();

            obj.Code = tmp.DataDict["関連付けコード"].ToString();
            obj.Name = tmp.DataDict["タグ名称"].ToString();

            return obj;
        }
    }
/*
    public class PathItem
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name = "";

        /// <summary>
        /// 階層の深さ
        /// </summary>
        public int Level = 0;

        /// <summary>
        /// 子リスト
        /// </summary>
        public List<PathItem> ChildList = new List<PathItem>();


        static List<PathItem> list = new List<PathItem>();

        public static List<PathItem> ListAll
        {
            get
            {
                if (list.Count == 0)
                {
                    foreach (PathTag obj in PathTag.ListAll)
                    {
                        int i = 0;

                        foreach (string s in obj.Name.Split('\\'))
                        {
                            PathItem tmp = new PathItem();

                            tmp.Name = s;
                            tmp.Level = i;

                            bool exist_flg = false;

                            // リストに存在しなければ追加する。
                            foreach (PathItem item in list)
                            {
                                if (item.Name.Equals(s))
                                {
                                    exist_flg = true;
                                    break;
                                }
                            }

                            if (!exist_flg)
                            {
                                list.Add(tmp);
                            }

                            i++;
                        }

                        // 子リストを作る
                        for (int j = 0; j < obj.Name.Split('\\').Length - 1; j++)
                        {
                            string sp = obj.Name.Split('\\')[j];
                            string sc = obj.Name.Split('\\')[j + 1];

                            PathItem p = new PathItem();

                            // 自分自身を探す
                            foreach (PathItem item in list)
                            {
                                if (item.Name.Equals(sp))
                                {
                                    p = item;
                                    break;
                                }
                            }

                            // 子供を探す
                            foreach (PathItem item in list)
                            {
                                if (item.Name.Equals(sc))
                                {
                                    p.ChildList.Add(item);
                                    break;
                                }
                            }
                        }
                    }
                }

                return list;
            }
        }
    }
 */
}
