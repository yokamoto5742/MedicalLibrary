using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class StdKarte1 : StdEntity
    {
        /// <summary>
        /// 患者コード
        /// </summary>
        public string PtId = "";

        protected PatBase _Pat = new PatBase();

        public PatBase Pat
        {
            get
            {
                if (!this._Pat.Id.Equals(this.PtId) || this._Pat.Name.Length == 0)
                {
                    this._Pat = PatBase.Load(this.PtId);
                }

                return this._Pat;
            }
        }

        /// <summary>
        /// 登録日
        /// </summary>
        public int RegDate = 0;

        /// <summary>
        /// 登録時間
        /// </summary>
        public int RegTime = 0;

        /// <summary>
        /// 登録日時
        /// </summary>
        public string RegDateTime
        {
            get
            {
                string s = DateTimeAgent.DateFormat(this.RegDate, DateTimeAgent.DateFormatKind.LONG) +
                    " " + DateTimeAgent.TimeFormat(this.RegTime.ToString().PadLeft(6, '0').Substring(0, 4));

                return s;
            }
        }

        /// <summary>
        /// 登録日時
        /// yy/MM/dd HH:mm
        /// </summary>
        public string RegDateTimeShort
        {
            get
            {
                string s = DateTimeAgent.DateFormat(this.RegDate, DateTimeAgent.DateFormatKind.SHORT) +
                    " " + DateTimeAgent.TimeFormat(this.RegTime.ToString().PadLeft(6, '0').Substring(0, 4));

                return s;
            }
        }

        /// <summary>
        /// 登録者コード
        /// </summary>
        public string RegStaffCode = "";

        /// <summary>
        /// 登録者
        /// </summary>
        public string RegStaffName
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(RegStaffCode))
                {
                    s = Dict.StaffDict[RegStaffCode].Name;
                }

                return s;
            }
        }

        /// <summary>
        /// 代行登録者コード
        /// </summary>
        public string RegStaffCode2 = "";

        /// <summary>
        /// 代行登録者
        /// </summary>
        public string RegStaffName2
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(RegStaffCode2))
                {
                    s = Dict.StaffDict[RegStaffCode2].Name;
                }

                return s;
            }
        }

        /// <summary>
        /// 更新日
        /// </summary>
        public int UpDate = 0;

        /// <summary>
        /// 更新時間
        /// </summary>
        public int UpTime = 0;

        /// <summary>
        /// 更新日時
        /// </summary>
        public string UpDateTime
        {
            get
            {
                string s = DateTimeAgent.DateFormat(this.UpDate, DateTimeAgent.DateFormatKind.LONG) +
                    " " + DateTimeAgent.TimeFormat(this.UpTime.ToString().PadLeft(6, '0').Substring(0, 4));

                return s;
            }
        }

        /// <summary>
        /// 更新日時
        /// yy/MM/dd HH:mm
        /// </summary>
        public string UpDateTimeShort
        {
            get
            {
                string s = DateTimeAgent.DateFormat(this.UpDate, DateTimeAgent.DateFormatKind.SHORT) +
                    " " + DateTimeAgent.TimeFormat(this.UpTime.ToString().PadLeft(6, '0').Substring(0, 4));

                return s;
            }
        }

        /// <summary>
        /// 更新者コード
        /// </summary>
        public string UpStaffCode = "";

        /// <summary>
        /// 更新者
        /// </summary>
        public string UpStaffName
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(UpStaffCode))
                {
                    s = Dict.StaffDict[UpStaffCode].Name;
                }

                return s;
            }
        }

        /// <summary>
        /// 代行更新者コード
        /// </summary>
        public string UpStaffCode2 = "";

        /// <summary>
        /// 代行更新者
        /// </summary>
        public string UpStaffName2
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(UpStaffCode2))
                {
                    s = Dict.StaffDict[UpStaffCode2].Name;
                }

                return s;
            }
        }


        public void BaseFromStdClass(StdClass tmp)
        {
            this.PtId = tmp.GetDataString("P_ID");
            this.RegDate = tmp.GetDataInt("REG_DATE");
            this.RegTime = tmp.GetDataInt("REG_TIME");
            this.RegStaffCode = tmp.GetDataString("REG_USR");
            this.RegStaffCode2 = tmp.GetDataString("REG_AGENT");
            this.UpDate = tmp.GetDataInt("UP_DATE");
            this.UpTime = tmp.GetDataInt("UP_TIME");
            this.UpStaffCode = tmp.GetDataString("UP_USR");
            this.UpStaffCode2 = tmp.GetDataString("UP_AGENT");
        }
    }
}
