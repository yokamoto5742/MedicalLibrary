using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// 患者の外来オーダーの診療区分と施行状況
    /// </summary>
    public class PatOutKoui : StdEntity
    {
        /// <summary>
        /// 患者コード
        /// </summary>
        public string Id = "";

        /// <summary>
        /// 受付番号
        /// </summary>
        public string UkeNo = "";

        /// <summary>
        /// 科番号
        /// </summary>
        public string DeptNo = "";

        /// <summary>
        /// 科コード
        /// </summary>
        public string Dept = "";

        /// <summary>
        /// 診療区分
        /// </summary>
        public int Koui = 0;

        /// <summary>
        /// 院内・院外処方
        /// 0: 院内, 1: 院外
        /// </summary>
        public string PharmacyInOut = "";

        public string PharmacyInOutName
        {
            get
            {
                string s = "";

                if (this.PharmacyInOut.Equals("0"))
                {
                    s = "院内";
                }
                else if (this.PharmacyInOut.Equals("1"))
                {
                    s = "院外";
                }

                return s;
            }
        }

        /// <summary>
        /// 施行フラグ
        /// </summary>
        public int Sekou = 0;

        public static List<PatOutKoui> GetList(string come_date, string dept)
        {
            List<PatOutKoui> list = new List<PatOutKoui>();

            if (come_date.Length != 8)
            {
                return list;
            }

#if INNO
            string deptSql1 = "";
            string deptSql2 = "";

            if (dept.Length > 0)
            {
                deptSql1 = " and DEPT = " + dept;
                deptSql2 = " and (DEPT = " + dept + " or CHANGE_DEPT = " + dept + ")";
            }

            // D_ORDER_HEADER の UKE_NO, DEPT_NO がどうなるのか？ 2018/09/07
            string cmd = "select distinct ts.P_ID, ts.UKE_NO, ts.DEPT_NO, ts.DEPT, th.SHINKU, th.IN_TYPE, th.SEKOU_FLG from " +
                " (select t.P_ID, t.UKE_NO, t.DEPT_NO, case when (t.CHANGE_DEPT is not null and t.CHANGE_DEPT > 0) then t.CHANGE_DEPT else t.DEPT end DEPT " +
                " from D_UKETSUKE t " +
                " where t.UKE_DATE = " + come_date + deptSql2 + ") ts, " +
                " (select t.P_ID, t.UKE_NO, t.DEPT_NO, t.SHINKU, t.DEPT, t.IN_TYPE, t.SEKOU_FLG " +
                " from D_ORDER_HEADER t " +
                " where t.ORDER_DATE = " + come_date + deptSql1 + " and t.INOUT = 1) th " +
//                " where ts.P_ID = th.P_ID and ts.UKE_NO = th.UKE_NO and ts.DEPT_NO = th.DEPT_NO and ts.DEPT = th.DEPT " +
                " where ts.P_ID = th.P_ID and ts.DEPT = th.DEPT " +
                " order by ts.P_ID, ts.DEPT, th.SHINKU, th.IN_TYPE, th.SEKOU_FLG desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatOutKoui obj = new PatOutKoui();

                obj.Id = tmp.GetDataString("P_ID");
//                obj.UkeNo = tmp.GetDataString("UKE_NO");
                obj.DeptNo = tmp.GetDataString("DEPT_NO");
                obj.Dept = tmp.GetDataString("DEPT");
                obj.Koui = tmp.GetDataInt("SHINKU");
                obj.PharmacyInOut = tmp.GetDataString("IN_TYPE");
                obj.Sekou = tmp.GetDataInt("SEKOU_FLG");

                list.Add(obj);
            }
#else
            string deptSql = "";

            if (dept.Length > 0)
            {
                deptSql = " and 科コード = " + dept;
            }

            string cmd = "select distinct ts.患者コード, ts.科コード, th.診療区分, th.院内区分, th.施行フラグ from " +
                " (select t.患者コード, case when t.変更科コード != 0 then t.変更科コード else t.科コード end 科コード " +
                " from macs.ADT_診察状況データ t " +
                " where t.受付日 = " + come_date + deptSql + ") ts, " +
                " (select t.患者コード, t.診療区分, t.科コード, t.院内区分, t.施行フラグ " +
                " from macs.ＮＴオーダーヘッダー t " +
                " where t.施行予定日 = " + come_date + deptSql + " and t.入外区分 = 1) th " +
                " where ts.患者コード = th.患者コード and ts.科コード = th.科コード " +
                " order by ts.患者コード, ts.科コード, th.診療区分, th.院内区分, th.施行フラグ desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PatOutKoui obj = new PatOutKoui();

                obj.Id = tmp.DataDict["患者コード"].ToString();
                obj.Dept = tmp.DataDict["科コード"].ToString();
                int.TryParse(tmp.DataDict["診療区分"].ToString(), out obj.Koui);
                obj.PharmacyInOut = tmp.DataDict["院内区分"].ToString();
                int.TryParse(tmp.DataDict["施行フラグ"].ToString(), out obj.Sekou);

                list.Add(obj);
            }
#endif
            return list;
        }
    }
}
