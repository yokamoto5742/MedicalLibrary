using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class DPCMaster : StdEntity
    {
        new public static DB Db = DB.Db2;

        /// <summary>
        /// 世代区分
        /// </summary>
        public int GenSEQ = 0;

        /// <summary>
        /// 番号
        /// </summary>
        public int SEQ = 0;

        /// <summary>
        /// 診断群分類番号
        /// </summary>
        public string Code = "";

        /// <summary>
        /// MDCコード
        /// </summary>
        public string MDC
        {
            get
            {
                string s = "";

                if (this.Code.Length >= 2)
                {
                    s = this.Code.Substring(0, 2);
                }

                return s;
            }
        }

        /// <summary>
        /// 分類コード
        /// </summary>
        public string Group
        {
            get
            {
                string s = "";

                if (this.Code.Length >= 6)
                {
                    s = this.Code.Substring(2, 4);
                }

                return s;
            }
        }

        /// <summary>
        /// MDC+Group 6桁
        /// </summary>
        public string MDCGroup
        {
            get
            {
                string s = "";

                if (this.Code.Length >= 6)
                {
                    s = this.Code.Substring(0, 6);
                }

                return s;
            }
        }

        /// <summary>
        /// 傷病名
        /// </summary>
        public string DiagName = "";

        /// <summary>
        /// JCS
        /// x: なし, 0: 0～9, 1: 10以上
        /// </summary>
        public string JCSFlg
        {
            get
            {
                string s = "";

                if (this.Code.Length >= 8)
                {
                    s = this.Code.Substring(7, 1);
                }

                return s;
            }
        }

        /// <summary>
        /// 手術フラグ
        /// xx: 手術なし, 99: 手術なし, 01等: 手術あり
        /// </summary>
        public string OpeFlg
        {
            get
            {
                string s = "";

                if (this.Code.Length >= 10)
                {
                    s = this.Code.Substring(8, 2);
                }

                return s;
            }
        }

        /// <summary>
        /// 手術名
        /// </summary>
        public string OpeName = "";

        /// <summary>
        /// 手術処置1
        /// </summary>
        public string Ope1 = "";

        /// <summary>
        /// 手術処置2
        /// </summary>
        public string Ope2 = "";

        /// <summary>
        /// 副傷病
        /// </summary>
        public string Diag2 = "";

        /// <summary>
        /// 副傷病フラグ
        /// x: なし, 0: なし, 1: あり
        /// </summary>
        public string DiagFlg2
        {
            get
            {
                string s = "";

                if (this.Code.Length >= 13)
                {
                    s = this.Code.Substring(12, 1);
                }

                return s;
            }
        }

        /// <summary>
        /// 重症度
        /// </summary>
        public string Heavy = "";

        /// <summary>
        /// 入院I
        /// </summary>
        public int Day1 = 0;

        /// <summary>
        /// 入院II
        /// </summary>
        public int Day2 = 0;

        /// <summary>
        /// 入院III
        /// </summary>
        public int Day3 = 0;

        /// <summary>
        /// 点数I
        /// </summary>
        public int Point1 = 0;

        /// <summary>
        /// 点数II
        /// </summary>
        public int Point2 = 0;

        /// <summary>
        /// 点数III
        /// </summary>
        public int Point3 = 0;

        /// <summary>
        /// 変更区分
        /// </summary>
        public int Kind1 = 0;

        /// <summary>
        /// 開始日
        /// </summary>
        public int Date1 = 0;

        /// <summary>
        /// 終了日
        /// </summary>
        public int Date2 = 0;

        /// <summary>
        /// 更新日
        /// </summary>
        public int Date3 = 0;

        /// <summary>
        /// 基準日
        /// </summary>
        static int crit_date = 0;

        /// <summary>
        /// 基準日
        /// </summary>
        public static int CritDate
        {
            set
            {
                crit_date = value;
            }
            get
            {
                if (crit_date == 0)
                {
                    int.TryParse(DateTime.Now.ToString("yyyyMMdd"), out crit_date);
                }

                return crit_date;
            }
        }

        /// <summary>
        /// 手術リスト
        /// </summary>
        public List<DPCOpe> OpeList = new List<DPCOpe>();

        /// <summary>
        /// 手術リストからKコードリストを取得する
        /// </summary>
        public string KCodeString(string delimiter)
        {
            List<string> list = new List<string>();

            foreach (DPCOpe obj in this.OpeList)
            {
                if (!list.Contains(obj.OpeCode1))
                {
                    list.Add(obj.OpeCode1);
                }

                if (!list.Contains(obj.OpeCode2))
                {
                    list.Add(obj.OpeCode2);
                }

                if (!list.Contains(obj.OpeCode3))
                {
                    list.Add(obj.OpeCode3);
                }

                if (!list.Contains(obj.OpeCode4))
                {
                    list.Add(obj.OpeCode4);
                }

                if (!list.Contains(obj.OpeCode5))
                {
                    list.Add(obj.OpeCode5);
                }
            }

            return AppString.ConcatList(list, delimiter);
        }


        public List<DPCDiag2> DiagList2 = new List<DPCDiag2>();

        /// <summary>
        /// 副傷病リストからICDコードリストを取得する
        /// </summary>
        public string ICDString(string delimiter)
        {
            List<string> list = new List<string>();

            foreach (DPCDiag2 obj in this.DiagList2)
            {
                if (!list.Contains(obj.ICD))
                {
                    list.Add(obj.ICD);
                }
            }

            return AppString.ConcatList(list, delimiter);
        }


        /// <summary>
        /// 合計点数
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public int PointSum(int days)
        {
            int i1 = 0;
            int i2 = 0;
            int i3 = 0;

            if (days > this.Day3)
            {
                i3 = (this.Day3 - this.Day2) * this.Point3;
                i2 = (this.Day2 - this.Day1) * this.Point2;
                i1 = this.Day1 * this.Point1;
            }
            else if (days > this.Day2)
            {
                i3 = (days - this.Day2) * this.Point3;
                i2 = (this.Day2 - this.Day1) * this.Point2;
                i1 = this.Day1 * this.Point1;
            }
            else if (days > this.Day1)
            {
                i2 = (days - this.Day1) * this.Point2;
                i1 = this.Day1 * this.Point1;
            }
            else
            {
                i1 = days * this.Point1;
            }

            return i1 + i2 + i3;
        }


        public static List<DPCMaster> GetListByDiagName(string diag_name)
        {
            List<DPCMaster> list = new List<DPCMaster>();

            if (diag_name.Length < 2)
            {
                return list;
            }

            string cmd = "select * from open.DPC_診断群分類点数表 t " +
                " where t.傷病名 like '%" + diag_name + "%'" +
                " and t.開始日 <= " + CritDate + " and (t.終了日 = 99999999 or t.終了日 >= " + CritDate + ")" +
                " order by t.番号";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            List<string> mdcgroup_list = new List<string>();

            foreach (StdClass tmp in tmp_list)
            {
                DPCMaster obj = GetFromStdClass(tmp);

                list.Add(obj);

                if (!mdcgroup_list.Contains(obj.MDCGroup))
                {
                    mdcgroup_list.Add(obj.MDCGroup);
                }
            }

            foreach (string mdcgroup in mdcgroup_list)
            {
                if (mdcgroup.Length < 6)
                {
                    continue;
                }

                List<DPCOpe> ope_list = DPCOpe.GetList(mdcgroup.Substring(0, 2).TrimStart('0'), mdcgroup.Substring(2, 4));
                List<DPCDiag2> diag_list = DPCDiag2.GetList(mdcgroup.Substring(0, 2).TrimStart('0'), mdcgroup.Substring(2, 4));

                foreach (DPCMaster obj in list)
                {
                    foreach (DPCOpe ope in ope_list)
                    {
                        if (obj.MDC.Equals(ope.MDC) && obj.Group.Equals(ope.Group) && obj.OpeFlg.Equals(ope.Code))
                        {
                            obj.OpeList.Add(ope);
                        }
                    }

                    // 副傷病ありの場合のみ
                    if (obj.DiagFlg2.Equals("1"))
                    {
                        foreach (DPCDiag2 diag in diag_list)
                        {
                            if (obj.MDC.Equals(diag.MDC) && obj.Group.Equals(diag.Group))
                            {
                                if (obj.OpeFlg.Equals("xx") || obj.OpeFlg.Equals("99"))
                                {
                                    // 手術なし
                                    if (diag.DiagFlg2.Equals("1") || diag.DiagFlg2.Equals("2"))
                                    {
                                        obj.DiagList2.Add(diag);
                                    }
                                }
                                else
                                {
                                    // 手術あり
                                    if (diag.DiagFlg2.Equals("1") || diag.DiagFlg2.Equals("3"))
                                    {
                                        obj.DiagList2.Add(diag);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return list;
        }

        public static List<DPCMaster> GetListByMDC(string mdc)
        {
            List<DPCMaster> list = new List<DPCMaster>();

            if (mdc.Length < 3)
            {
                return list;
            }

            string cmd = "select * from open.DPC_診断群分類点数表 t " +
                " where t.診断群分類番号 like '" + mdc + "%'" +
                " and t.開始日 <= " + CritDate + " and (t.終了日 = 99999999 or t.終了日 >= " + CritDate + ")" +
                " order by t.番号";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            List<string> mdcgroup_list = new List<string>();

            foreach (StdClass tmp in tmp_list)
            {
                DPCMaster obj = GetFromStdClass(tmp);

                list.Add(obj);

                if (!mdcgroup_list.Contains(obj.MDCGroup))
                {
                    mdcgroup_list.Add(obj.MDCGroup);
                }
            }

            foreach (string mdcgroup in mdcgroup_list)
            {
                if (mdcgroup.Length < 6)
                {
                    continue;
                }

                List<DPCOpe> ope_list = DPCOpe.GetList(mdcgroup.Substring(0, 2).TrimStart('0'), mdcgroup.Substring(2, 4));
                List<DPCDiag2> diag_list = DPCDiag2.GetList(mdcgroup.Substring(0, 2).TrimStart('0'), mdcgroup.Substring(2, 4));

                foreach (DPCMaster obj in list)
                {
                    foreach (DPCOpe ope in ope_list)
                    {
                        if (obj.MDC.Equals(ope.MDC) && obj.Group.Equals(ope.Group) && obj.OpeFlg.Equals(ope.Code))
                        {
                            obj.OpeList.Add(ope);
                        }
                    }

                    // 副傷病ありの場合のみ
                    if (obj.DiagFlg2.Equals("1"))
                    {
                        foreach (DPCDiag2 diag in diag_list)
                        {
                            if (obj.MDC.Equals(diag.MDC) && obj.Group.Equals(diag.Group))
                            {
                                if (obj.OpeFlg.Equals("xx") || obj.OpeFlg.Equals("99"))
                                {
                                    // 手術なし
                                    if (diag.DiagFlg2.Equals("1") || diag.DiagFlg2.Equals("2"))
                                    {
                                        obj.DiagList2.Add(diag);
                                    }
                                }
                                else
                                {
                                    // 手術あり
                                    if (diag.DiagFlg2.Equals("1") || diag.DiagFlg2.Equals("3"))
                                    {
                                        obj.DiagList2.Add(diag);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return list;
        }

        public static List<DPCMaster> GetListByMDCAndDiagName(string mdc, string diag_name)
        {
            List<DPCMaster> list = new List<DPCMaster>();

            if (mdc.Length < 3 || diag_name.Length < 2)
            {
                return list;
            }

            string cmd = "select * from open.DPC_診断群分類点数表 t " +
                " where t.診断群分類番号 like '" + mdc + "%'" +
                " and t.傷病名 like '%" + diag_name + "%'" +
                " and t.開始日 <= " + CritDate + " and (t.終了日 = 99999999 or t.終了日 >= " + CritDate + ")" +
                " order by t.番号";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            List<string> mdcgroup_list = new List<string>();

            foreach (StdClass tmp in tmp_list)
            {
                DPCMaster obj = GetFromStdClass(tmp);

                list.Add(obj);

                if (!mdcgroup_list.Contains(obj.MDCGroup))
                {
                    mdcgroup_list.Add(obj.MDCGroup);
                }
            }

            foreach (string mdcgroup in mdcgroup_list)
            {
                if (mdcgroup.Length < 6)
                {
                    continue;
                }

                List<DPCOpe> ope_list = DPCOpe.GetList(mdcgroup.Substring(0, 2).TrimStart('0'), mdcgroup.Substring(2, 4));
                List<DPCDiag2> diag_list = DPCDiag2.GetList(mdcgroup.Substring(0, 2).TrimStart('0'), mdcgroup.Substring(2, 4));

                foreach (DPCMaster obj in list)
                {
                    foreach (DPCOpe ope in ope_list)
                    {
                        if (obj.MDC.Equals(ope.MDC) && obj.Group.Equals(ope.Group) && obj.OpeFlg.Equals(ope.Code))
                        {
                            obj.OpeList.Add(ope);
                        }
                    }

                    // 副傷病ありの場合のみ
                    if (obj.DiagFlg2.Equals("1"))
                    {
                        foreach (DPCDiag2 diag in diag_list)
                        {
                            if (obj.MDC.Equals(diag.MDC) && obj.Group.Equals(diag.Group))
                            {
                                if (obj.OpeFlg.Equals("xx") || obj.OpeFlg.Equals("99"))
                                {
                                    // 手術なし
                                    if (diag.DiagFlg2.Equals("1") || diag.DiagFlg2.Equals("2"))
                                    {
                                        obj.DiagList2.Add(diag);
                                    }
                                }
                                else
                                {
                                    // 手術あり
                                    if (diag.DiagFlg2.Equals("1") || diag.DiagFlg2.Equals("3"))
                                    {
                                        obj.DiagList2.Add(diag);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return list;
        }


        static DPCMaster GetFromStdClass(StdClass tmp)
        {
            DPCMaster obj = new DPCMaster();

            int.TryParse(tmp.DataDict["世代区分"].ToString(), out obj.GenSEQ);
            int.TryParse(tmp.DataDict["番号"].ToString(), out obj.SEQ);
            obj.Code = tmp.DataDict["診断群分類番号"].ToString();
            obj.DiagName = tmp.DataDict["傷病名"].ToString();
            obj.OpeName = tmp.DataDict["手術名"].ToString();
            obj.Ope1 = tmp.DataDict["手術処置1"].ToString();
            obj.Ope2 = tmp.DataDict["手術処置2"].ToString();
            obj.Diag2 = tmp.DataDict["副傷病"].ToString();
            obj.Heavy = tmp.DataDict["重症度"].ToString();
            int.TryParse(tmp.DataDict["入院I"].ToString(), out obj.Day1);
            int.TryParse(tmp.DataDict["入院II"].ToString(), out obj.Day2);
            int.TryParse(tmp.DataDict["入院III"].ToString(), out obj.Day3);
            int.TryParse(tmp.DataDict["点数I"].ToString(), out obj.Point1);
            int.TryParse(tmp.DataDict["点数II"].ToString(), out obj.Point2);
            int.TryParse(tmp.DataDict["点数III"].ToString(), out obj.Point3);
            int.TryParse(tmp.DataDict["開始日"].ToString(), out obj.Date1);
            int.TryParse(tmp.DataDict["終了日"].ToString(), out obj.Date2);
            int.TryParse(tmp.DataDict["更新日"].ToString(), out obj.Date3);

            return obj;
        }
    }
}
