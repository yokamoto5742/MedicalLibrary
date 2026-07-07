using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// オーダー画面のタブ表示設定
    /// </summary>
    public class Cust1 : StdEntity
    {
        public string DeptCode = "";

        public string DoctorCode = "";

        public string[] TabNames = new string[11];

        public string[] FieldNames = new string[6];

        /// <summary>
        /// 科共通データとＤＲ固有データを比較して、実際に表示するデータを取得する
        /// </summary>
        /// <param name="dept_code"></param>
        /// <param name="doctor_code"></param>
        /// <returns></returns>
        public static Cust1 Get(string dept_code, string doctor_code)
        {
            Cust1 obj = new Cust1();

            // 科コード = 0 のデータは存在しない（ＤＲコード = 0 は科共通）
            if (dept_code.Length == 0 || dept_code == "0" || doctor_code.Length == 0)
            {
                return obj;
            }

            string cmd = "select * from macs.ＣＵＳＴマスター１Ｒ t" +
                " where t.科コード = " + dept_code +
                " and t.ＤＲコード in (0, " + doctor_code + ")" +
                " order by t.ＤＲコード";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                if (tmp.DataDict["ＤＲコード"].ToString().Equals("0"))
                {
                    // DRコード = 0 の場合は科共通なので初期設定する

                    obj.DeptCode = dept_code;
                    obj.DoctorCode = "0";

                    for (int i = 1; i <= 10; i++)
                    {
                        obj.TabNames[i] = tmp.DataDict["タブ名称" + AppString.HanToZen(i.ToString())].ToString();
                    }

                    for (int i = 1; i <= 5; i++)
                    {
                        obj.FieldNames[i] = tmp.DataDict["汎用フィールド" + AppString.HanToZen(i.ToString())].ToString();
                    }
                }
                else
                {
                    // DRコード > 0 の場合はＤＲ専用なので上書きする

                    obj.DeptCode = dept_code;
                    obj.DoctorCode = doctor_code;

                    for (int i = 1; i <= 10; i++)
                    {
                        obj.TabNames[i] = tmp.DataDict["タブ名称" + AppString.HanToZen(i.ToString())].ToString();
                    }

                    for (int i = 1; i <= 5; i++)
                    {
                        obj.FieldNames[i] = tmp.DataDict["汎用フィールド" + AppString.HanToZen(i.ToString())].ToString();
                    }
                }
            }

            return obj;
        }
    }
}
