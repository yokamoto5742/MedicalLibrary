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

        /// <summary>
        /// 登録日
        /// </summary>
        public int RegDate = 0;

        /// <summary>
        /// 登録時間
        /// </summary>
        public int RegTime = 0;

        /// <summary>
        /// 登録者コード
        /// </summary>
        public string RegStaffCode = "";

        /// <summary>
        /// 代行登録者コード
        /// </summary>
        public string RegStaffCode2 = "";

        /// <summary>
        /// 更新日
        /// </summary>
        public int UpDate = 0;

        /// <summary>
        /// 更新時間
        /// </summary>
        public int UpTime = 0;

        /// <summary>
        /// 更新者コード
        /// </summary>
        public string UpStaffCode = "";

        /// <summary>
        /// 代行更新者コード
        /// </summary>
        public string UpStaffCode2 = "";


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
