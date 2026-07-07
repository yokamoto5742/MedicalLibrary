using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class RsvNameMaster : StdEntity
    {
        /// <summary>
        /// 予約種別
        /// </summary>
        public string Code = "";

        /// <summary>
        /// 予約種別名称
        /// </summary>
        public string FullName = "";

        /// <summary>
        /// 予約種別名称
        /// </summary>
        public string ShortName = "";

        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DelFlg = false;

        /// <summary>
        /// 科
        /// </summary>
        public string DeptCode = "";

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
        /// ＤＲ
        /// </summary>
        public string DoctorCode = "";

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

        public override string ToString()
        {
            return this.ShortName;
        }

        static List<RsvNameMaster> _List = new List<RsvNameMaster>();

        public static RsvNameMaster Load(string code)
        {
            RsvNameMaster m = new RsvNameMaster();

            if (_List == null || _List.Count == 0)
            {
                _List = GetList();
            }

            foreach (RsvNameMaster mm in _List)
            {
                if (mm.Code.Equals(code))
                {
                    m = mm;
                    break;
                }
            }

            return m;
        }

        public static List<RsvNameMaster> GetList()
        {
            List<RsvNameMaster> list = new List<RsvNameMaster>();

#if INNO
            string cmd = "select * from M_YOYAKU_NAME t " +
                " order by t.CODE";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
            RsvNameMaster obj;

            foreach (StdClass tmp in tmp_list)
            {
                obj = new RsvNameMaster();

                obj.Code = tmp.GetDataString("CODE");
                obj.FullName = tmp.GetDataString("NAME").Trim();
                obj.ShortName = tmp.GetDataString("S_NAME").Trim();

                list.Add(obj);
            }
#else
            string cmd = "select * from macs.TM50RC t " +
                " where t.TM50RC_F01 = 50 " +
                " order by t.TM50RC_F02";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
            RsvNameMaster obj;

            foreach (StdClass tmp in tmp_list)
            {
                obj = new RsvNameMaster();

                obj.Code = tmp.GetDataString("TM50RC_F02");
                obj.FullName = tmp.GetDataString("TM50RC_F03").Trim();
                obj.ShortName = tmp.GetDataString("TM50RC_F04").Trim();

                if (obj.Code.Equals("160"))
                {
                    // 超音波 160 の場合はリストに追加せず 161, 162 を足す
                    obj = new RsvNameMaster();
                    obj.Code = "161";
                    obj.FullName = "超音波1F";
                    obj.ShortName = "超音波1F";
                    list.Add(obj);

                    obj = new RsvNameMaster();
                    obj.Code = "162";
                    obj.FullName = "超音波2F";
                    obj.ShortName = "超音波2F";
                    list.Add(obj);
                }
                else
                {
                    list.Add(obj);
                }
            }
#endif

            return list;
        }
    }
}
