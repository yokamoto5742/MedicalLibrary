using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class Voucher : StdEntity
    {
        /// <summary>
        /// 伝票番号
        /// </summary>
        public string Code = "";

        /// <summary>
        /// 伝票名称
        /// </summary>
        public string Name = "";


        public static List<Voucher> GetList(string dept_code)
        {
            List<Voucher> list = new List<Voucher>();

            string cmd = "select td.*, tm.伝票名称 from macs.ＮＴ伝票検索用医師別科別 td, macs.ＮＴ伝票マスター tm" +
                " where td.コード = " + dept_code +
                " and td.オーダーコード = tm.伝票番号 " +
                " and td.区分 = 1 and td.表示フラグ = 0 " +
                " order by td.オーダーコード";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                Voucher obj = new Voucher();

                obj.Code = tmp.DataDict["オーダーコード"].ToString().TrimEnd(' ', '　');
                obj.Name = tmp.DataDict["伝票名称"].ToString().TrimEnd(' ', '　');

                list.Add(obj);
            }

            return list;
        }
    }
}
