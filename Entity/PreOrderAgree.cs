using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class PreOrderAgree : StdEntity
    {
        /// <summary>
        /// オーダー番号
        /// </summary>
        public string OrderId = "";

        PatBase _Pat = new PatBase();

        public PatBase Pat
        {
            get
            {
                return this._Pat;
            }
        }

        /// <summary>
        /// 病棟コード
        /// </summary>
        public string WardCode = "";

        /// <summary>
        /// 病棟
        /// </summary>
        public string WardName
        {
            get
            {
                string s = "";

                if (Dict.WardDict.ContainsKey(this.WardCode))
                {
                    s = Dict.WardDict[this.WardCode].Name;
                }

                return s;
            }
        }

        /// <summary>
        /// 病室
        /// </summary>
        public string Room = "";

        /// <summary>
        /// 科コード
        /// </summary>
        public string DeptCode = "";

        /// <summary>
        /// 科
        /// </summary>
        public string DeptName
        {
            get
            {
                string s = "";

                if (Dict.DeptDict.ContainsKey(this.DeptCode))
                {
                    s = Dict.DeptDict[this.DeptCode].ShortName;
                }

                return s;
            }
        }

        /// <summary>
        /// 主治医コード
        /// </summary>
        public string DoctorCode = "";

        /// <summary>
        /// 主治医
        /// </summary>
        public string DoctorName
        {
            get
            {
                string s = "";

                if (Dict.DoctorDict.ContainsKey(this.DoctorCode))
                {
                    s = Dict.DoctorDict[this.DoctorCode].Name;
                }

                return s;
            }
        }

        /// <summary>
        /// 実施日時
        /// </summary>
        public string ExecDateTime = "";

        /// <summary>
        /// タイトル
        /// </summary>
        public string Title = "";

        /// <summary>
        /// SOAP表示名称
        /// </summary>
        public string SOAP = "";

        /// <summary>
        /// 実施者コード
        /// </summary>
        public string StaffCode = "";

        /// <summary>
        /// 実施者
        /// </summary>
        public string StaffName
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(this.StaffCode))
                {
                    s = Dict.StaffDict[this.StaffCode].Name;
                }

                return s;
            }
        }


        /// <summary>
        /// 未承認リストを取得
        /// </summary>
        /// <returns></returns>
        public static List<PreOrderAgree> GetYetList(string crit_date)
        {
            List<PreOrderAgree> list = new List<PreOrderAgree>();

            if (crit_date.Length != 8)
            {
                return list;
            }
#if INNO
#else
            string cmd = "select t3.IM01RC_F01, t3.IM01RC_F04, t3.IM01RC_F05, t3.IM01RC_F10 " +
                " , t5.IM20RC_F12, t5.IM20RC_F13, t5.IM20RC_F03, t5.IM20RC_F05 " +
                " , to_char(t1.日時, 'YYYY/MM/DD HH24:MI') 施行日時 " +
                " , t1.タイトル, t2.オーダー番号, t2.ＳＯＡＰ表示名称, t4.実施者コード " +
                " from macs.実施行為データ t1, macs.ＮＴオーダーヘッダー t2, macs.IM01RC t3, macs.実施行為実施者データ t4, macs.IM20RC t5 " +
                " where t1.日時 >= '" + crit_date + "' " +
                " and t1.移行フラグ = 1 " +
                " and t1.診療区分 >= 20 and t1.診療区分 < 40 " +
                " and t2.指示医コード = 0 " +
                " and t1.オーダー番号 = t2.オーダー番号 " +
                " and t1.患者コード = t3.IM01RC_F01 " +
                " and t1.実施番号 = t4.実施番号 " +
                " and t1.患者コード = t5.IM20RC_F01 " +
                " order by t1.日時";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PreOrderAgree obj = GetFromStdClass(tmp);

                list.Add(obj);
            }
#endif
            return list;
        }

        static PreOrderAgree GetFromStdClass(StdClass tmp)
        {
            PreOrderAgree obj = new PreOrderAgree();
#if INNO
#else
            obj._Pat.Id = tmp.GetDataString("IM01RC_F01");
            obj._Pat.Name = tmp.GetDataString("IM01RC_F04").Trim();
            obj._Pat.Sex = tmp.GetDataString("IM01RC_F05");
            obj._Pat.Birth = tmp.GetDataString("IM01RC_F10");

            obj.WardCode = tmp.GetDataString("IM20RC_F12").Trim();
            obj.Room = tmp.GetDataString("IM20RC_F13").Trim();
            obj.DeptCode = tmp.GetDataString("IM20RC_F03");
            obj.DoctorCode = tmp.GetDataString("IM20RC_F05");
            obj.ExecDateTime = tmp.GetDataString("施行日時");
            obj.Title = tmp.GetDataString("タイトル");
            obj.SOAP = tmp.GetDataString("ＳＯＡＰ表示名称");
            obj.StaffCode = tmp.GetDataString("実施者コード");
            obj.OrderId = tmp.GetDataString("オーダー番号");
#endif
            return obj;
        }
    }
}
