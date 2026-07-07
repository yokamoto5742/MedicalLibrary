using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class AddressGroup : StdEntity
    {
        /// <summary>
        /// 入力者コード
        /// </summary>
        public string StaffCode = "";

        /// <summary>
        /// グループコード
        /// </summary>
        public int SEQ = 0;

        /// <summary>
        /// グループ名
        /// </summary>
        public string Name = "";

        public override string ToString()
        {
            return this.Name;
        }


        public static List<AddressGroup> GetList(string staff_code)
        {
            List<AddressGroup> list = new List<AddressGroup>();

            if (staff_code.Length == 0)
            {
                return list;
            }
            string cmd = "select * from M_KARTE_MESSAGE_GROUP t " +
                " where t.CODE = " + staff_code +
                " order by t.GROUP_NO ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }
            return list;
        }

        static AddressGroup GetFromStdClass(StdClass tmp)
        {
            AddressGroup obj = new AddressGroup();
            obj.StaffCode = tmp.DataDict["CODE"].ToString();
            int.TryParse(tmp.DataDict["GROUP_NO"].ToString(), out obj.SEQ);
            obj.Name = tmp.DataDict["GROUP_NAME"].ToString();
            return obj;
        }

        public static List<Staff> GetMembers(string staff_code, int seq)
        {
            List<Staff> list = new List<Staff>();

            if (staff_code.Length == 0 || seq == 0)
            {
                return list;
            }
            string cmd = "select * from M_KARTE_MESSAGE_ADDRESS t " +
                " where t.CODE = " + staff_code +
                " and t.GROUP_NO = " + seq;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                string code = tmp.DataDict["MEMBER_CODE"].ToString();

                if (Dict.StaffDict.ContainsKey(code))
                {
                    list.Add(Dict.StaffDict[code]);
                }
            }
            return list;
        }

        public static int GetMaxSEQ(string staff_code)
        {
            int seq = 0;

            if (staff_code.Length == 0)
            {
                return seq;
            }
            string cmd = "select max(GROUP_NO) MAX_NO from M_KARTE_MESSAGE_GROUP t " +
                " where t.CODE = " + staff_code;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                if (tmp.DataDict["MAX_NO"].ToString().Length > 0)
                {
                    seq = int.Parse(tmp.DataDict["MAX_NO"].ToString());
                }

                break;
            }
            return seq;
        }

        public StdReturn Insert(List<Staff> list)
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
            obj.Db = DB.Db3;
            obj.Table = "M_KARTE_MESSAGE_GROUP";

            // 連番を取得する
            this.SEQ = AddressGroup.GetMaxSEQ(LoginUser.Id) + 1;

            obj.DataList.Add(new StdDbColumn("CODE", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("GROUP_NO", StdDbType.NUMBER, this.SEQ));
            obj.DataList.Add(new StdDbColumn("GROUP_NAME", StdDbType.VARCHAR2, this.Name));

            sr = obj.InsertSQL();
            // メンバーを登録
            RegMembers(LoginUser.Id, this.SEQ, list);

            return sr;
        }

        public StdReturn Update(List<Staff> list)
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
            obj.Db = DB.Db3;
            obj.Table = "M_KARTE_MESSAGE_GROUP";

            obj.DataList.Add(new StdDbColumn("GROUP_NAME", StdDbType.VARCHAR2, this.Name));

            obj.WhereList.Add("CODE = " + LoginUser.Id);
            obj.WhereList.Add("GROUP_NO = " + this.SEQ);

            sr = obj.UpdateSQL();
            // メンバーを登録
            RegMembers(LoginUser.Id, this.SEQ, list);

            return sr;
        }

        public StdReturn Delete()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
            obj.Db = DB.Db3;
            obj.Table = "M_KARTE_MESSAGE_GROUP";

            obj.WhereList.Add("CODE = " + LoginUser.Id);
            obj.WhereList.Add("GROUP_NO = " + this.SEQ);

            sr = obj.DeleteSQL();
            // メンバーも削除
            DeleteMembers(LoginUser.Id, this.SEQ);

            return sr;
        }

        public static StdReturn DeleteMembers(string staff_code, int seq)
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
            obj.Db = DB.Db3;
            obj.Table = "M_KARTE_MESSAGE_ADDRESS";

            obj.WhereList.Add("CODE = " + staff_code);
            obj.WhereList.Add("GROUP_NO = " + seq);

            sr = obj.DeleteSQL();
            return sr;
        }

        public static StdReturn RegMembers(string staff_code, int seq, List<Staff> list)
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
            obj.Db = DB.Db3;
            obj.Table = "M_KARTE_MESSAGE_ADDRESS";

            // いったん全メンバーを削除
            obj.WhereList.Add("CODE = " + staff_code);
            obj.WhereList.Add("GROUP_NO = " + seq);

            sr = obj.DeleteSQL();

            obj.WhereList.Clear();

            // メンバーを挿入
            foreach (Staff staff in list)
            {
                obj.DataList.Clear();

                obj.DataList.Add(new StdDbColumn("CODE", StdDbType.NUMBER, staff_code));
                obj.DataList.Add(new StdDbColumn("GROUP_NO", StdDbType.NUMBER, seq));
                obj.DataList.Add(new StdDbColumn("MEMBER_CODE", StdDbType.NUMBER, staff.Code));

                sr = obj.InsertSQL();
            }
            return sr;
        }
    }
}
