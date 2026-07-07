using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// 看護指示マスター
    /// </summary>
    public class NursingOrderMaster : StdEntity
    {
        /// <summary>
        /// 指示コード
        /// </summary>
        public int Code1 = 0;

        /// <summary>
        /// 連番
        /// </summary>
        public int Code2 = 0;

        /// <summary>
        /// タイトル
        /// </summary>
        public string Title = "";

        /// <summary>
        /// 属性。1 RadioButton, 2 CheckBox, 3 TextBox
        /// </summary>
        public int Kind = 1;

        /// <summary>
        /// 選択肢名称
        /// </summary>
        public string Text1 = "";

        /// <summary>
        /// 選択肢名称２
        /// </summary>
        public string Text2 = "";

        /// <summary>
        /// テキスト有無
        /// </summary>
        public int Limit = 0;

        static Dictionary<int, Dictionary<int, NursingOrderMaster>> dict = new Dictionary<int, Dictionary<int, NursingOrderMaster>>();

        public static Dictionary<int, Dictionary<int, NursingOrderMaster>> Dict
        {
            get
            {
                if (dict.Count == 0)
                {
                    DictUpdate();
                }

                return dict;
            }
        }

        /// <summary>
        /// 辞書を更新する。
        /// </summary>
        public static void DictUpdate()
        {
            dict.Clear();

            Db.Open();

            string cmd = "Select * from PATH看護指示マスター order by 指示コード, 連番";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                NursingOrderMaster master = new NursingOrderMaster();

                int.TryParse(tmp.DataDict["指示コード"].ToString(), out master.Code1);
                int.TryParse(tmp.DataDict["連番"].ToString(), out master.Code2);
                master.Title = tmp.DataDict["タイトル"].ToString().Trim();
                int.TryParse(tmp.DataDict["属性"].ToString(), out master.Kind);
                master.Text1 = tmp.DataDict["選択肢名称"].ToString();
                master.Text2 = tmp.DataDict["選択肢名称２"].ToString();
                int.TryParse(tmp.DataDict["テキスト有無"].ToString(), out master.Limit);

                if (dict.ContainsKey(master.Code1))
                {
                    dict[master.Code1].Add(master.Code2, master);
                }
                else
                {
                    Dictionary<int, NursingOrderMaster> tmp_dict = new Dictionary<int, NursingOrderMaster>();
                    tmp_dict.Add(master.Code2, master);
                    dict.Add(master.Code1, tmp_dict);
                }
            }

            Db.Close();
        }
    }
}
