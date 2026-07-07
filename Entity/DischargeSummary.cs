using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class DischargeSummary : StdKarte1
    {
        /// <summary>
        /// 入院番号
        /// </summary>
        public int InSEQ = 0;

        /// <summary>
        /// 主病名
        /// </summary>
        public string MainDiag = "";

        /// <summary>
        /// 病歴
        /// </summary>
        public string Cont1 = "";

        /// <summary>
        /// 入院所見
        /// </summary>
        public string Cont2 = "";

        /// <summary>
        /// 入院経過
        /// </summary>
        public string Cont3 = "";

        /// <summary>
        /// 退院メッセージ
        /// </summary>
        public string Cont4 = "";

        /// <summary>
        /// アレルギー
        /// </summary>
        public string Cont5 = "";

        /// <summary>
        /// 考察
        /// </summary>
        public string Cont6 = "";


        public List<DischargeSummaryOpe> OpeList = new List<DischargeSummaryOpe>();


        /// <summary>
        /// 1: 医師チェック, 2: 管理士チェック
        /// </summary>
        public int InputCheck = 0;

        /// <summary>
        /// 確定日
        /// </summary>
        public string StatusDate = "";


        public DischargeSummary()
        {
            for (int i = 0; i <= 5; i++)
            {
                this.OpeList.Add(new DischargeSummaryOpe());
            }
        }

        public static DischargeSummary GetData(string pt_id, int in_seq)
        {
            DischargeSummary obj = new DischargeSummary();

            if (pt_id.Length == 0)
            {
                return obj;
            }

            string cmd = "select * from D_TAIIN_SUMMARY t " +
                " where t.P_ID = " + pt_id +
                " and t.NYUIN_NO = " + in_seq +
                " and (t.DEL_FLG is null or t.DEL_FLG = 0)";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                obj = GetFromStdClass(tmp);
                break;
            }

            return obj;
        }

        public static List<DischargeSummary> GetList(List<PatIn> p_list)
        {
            List<DischargeSummary> list = new List<DischargeSummary>();

            if (p_list.Count == 0)
            {
                return list;
            }

            string cmd = "select * from D_TAIIN_SUMMARY t " +
                " where (t.P_ID, t.NYUIN_NO) in (" + AppString.ConcatList(p_list.ConvertAll((x) => { return "(" + x.Id + "," + x.SEQ + ")"; }), ",") + ")" +
                " and (t.DEL_FLG is null or t.DEL_FLG = 0)";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }

        static DischargeSummary GetFromStdClass(StdClass tmp)
        {
            DischargeSummary obj = new DischargeSummary();

            obj.PtId = tmp.GetDataString("P_ID");
            obj.InSEQ = tmp.GetDataInt("NYUIN_NO", 0);
            obj.MainDiag = tmp.GetDataString("MAIN_BYOUMEI");

            obj.Cont1 = tmp.GetDataString("BYOUREKI");
            obj.Cont2 = tmp.GetDataString("NYUIN_SYOKEN");
            obj.Cont3 = tmp.GetDataString("NYUIN_KEIKA");
            obj.Cont4 = tmp.GetDataString("TAIIN_MESSAGE");
            obj.Cont5 = tmp.GetDataString("ALLERGY");
            obj.Cont6 = tmp.GetDataString("KOUSATSU");

            for (int i = 1; i <= 5; i++)
            {
                obj.OpeList[i].Name = tmp.GetDataString("OPE" + i + "_NAME");
                obj.OpeList[i].Doctor = tmp.GetDataString("OPE" + i + "_DOCTOR");
                obj.OpeList[i].Date = tmp.GetDataString("OPE" + i + "_DATE");
                obj.OpeList[i].Time = tmp.GetDataString("OPE" + i + "_TIME");
                obj.OpeList[i].Anes = tmp.GetDataString("OPE" + i + "_MASUI");
                obj.OpeList[i].Cont = tmp.GetDataString("OPE" + i + "_SYOKEN");
            }

            obj.InputCheck = tmp.GetDataInt("INPUT_CHECK", 0);
            obj.StatusDate = tmp.GetDataString("REG_STATUS");

            obj.BaseFromStdClass(tmp);

            return obj;
        }
    }

    public class DischargeSummaryOpe
    {
        /// <summary>
        /// 術式
        /// </summary>
        public string Name = "";

        /// <summary>
        /// 術者
        /// </summary>
        public string Doctor = "";

        /// <summary>
        /// 手術日
        /// </summary>
        public string Date = "";

        /// <summary>
        /// 手術時間
        /// </summary>
        public string Time = "";

        /// <summary>
        /// 麻酔
        /// </summary>
        public string Anes = "";

        /// <summary>
        /// 所見
        /// </summary>
        public string Cont = "";
    }
}
