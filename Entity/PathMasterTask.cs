using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class PathMasterTask : StdEntity
    {
        /// <summary>
        /// パスコード
        /// </summary>
        public string PathCode = "";

        /// <summary>
        /// 連番
        /// </summary>
        public int SEQ = 0;

        /// <summary>
        /// カテゴリコード
        /// </summary>
        public string CategoryCode = "";

        /// <summary>
        /// 開始日
        /// </summary>
        public int StartDay = 0;

        /// <summary>
        /// 開始時間
        /// </summary>
        public int StartTime = -1;

        public string StartTimeString
        {
            get
            {
                return DateTimeAgent.TimeFormat(this.StartTime);
            }
        }

        /// <summary>
        /// タスクコード
        /// </summary>
        public string TaskCode = "";

        /// <summary>
        /// タスク名称
        /// </summary>
        public string TaskName = "";

        /// <summary>
        /// 日数
        /// 実際のデータはすべて 1
        /// </summary>
        public int Days = 1;

        /// <summary>
        /// 入外区分
        /// </summary>
        public int InOut = 2;

        /// <summary>
        /// 状態
        /// 0: 有効, -1: 無効
        /// </summary>
        public int Status = 0;

        /// <summary>
        /// 実施区分
        /// 実際のデータはほとんど 2。たまに 0, 1 がある。
        /// </summary>
        public int Exec = 2;

        /// <summary>
        /// タスクのオーダーディティール
        /// </summary>
        public List<PathMasterTaskDetail> DetailList = new List<PathMasterTaskDetail>();


        public static List<PathMasterTask> GetList(string path_code)
        {
            List<PathMasterTask> list = new List<PathMasterTask>();

            if (path_code.Length == 0)
            {
                return list;
            }

            string cmd = "select * from macs.PATH_マスタディティール t " +
                " where t.パスコード = '" + path_code + "' " +
                " order by t.連番";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);
            List<string> task_code_list = new List<string>();

            foreach (StdClass tmp in tmp_list)
            {
                PathMasterTask obj = GetFromStdClass(tmp);
                list.Add(obj);
                task_code_list.Add(obj.TaskCode);
            }


            List<PathMasterTaskDetail> detail_list = PathMasterTaskDetail.GetList(task_code_list);

            foreach (PathMasterTaskDetail detail in detail_list)
            {
                foreach (PathMasterTask task in list)
                {
                    if (task.TaskCode.Equals(detail.TaskCode))
                    {
                        task.DetailList.Add(detail);
                        break;
                    }
                }
            }

            return list;
        }


        static PathMasterTask GetFromStdClass(StdClass tmp)
        {
            PathMasterTask obj = new PathMasterTask();

            obj.PathCode = tmp.DataDict["パスコード"].ToString();
            int.TryParse(tmp.DataDict["連番"].ToString(), out obj.SEQ);
            obj.CategoryCode = tmp.DataDict["カテゴリコード"].ToString();
            int.TryParse(tmp.DataDict["開始日"].ToString(), out obj.StartDay);
            int.TryParse(tmp.DataDict["開始時間"].ToString(), out obj.StartTime);
            obj.TaskCode = tmp.DataDict["タスクコード"].ToString();
            obj.TaskName = tmp.DataDict["タスク名称"].ToString();
            int.TryParse(tmp.DataDict["日数"].ToString(), out obj.Days);
            int.TryParse(tmp.DataDict["入外区分"].ToString(), out obj.InOut);
            int.TryParse(tmp.DataDict["状態"].ToString(), out obj.Status);
            int.TryParse(tmp.DataDict["実施区分"].ToString(), out obj.Exec);

            return obj;
        }
    }
}
