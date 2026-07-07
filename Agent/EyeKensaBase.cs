using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Entity;

namespace MedicalLibrary.Agent
{
    /// <summary>
    /// 検査データの基本クラス
    /// </summary>
    public class EyeKensaBase
    {
        public string PtId = "";

        protected PatBase _Pat = new PatBase();

        public PatBase Pat
        {
            get
            {
                if (!this._Pat.Id.Equals(this.PtId))
                {
                    this._Pat = PatBase.Load(this.PtId);
                }

                return this._Pat;
            }
        }

        /// <summary>
        /// 検査日
        /// </summary>
        public string KensaDate = "";

        /// <summary>
        /// 検査ID
        /// </summary>
        public string KensaId = "";

        /// <summary>
        /// 検査名
        /// </summary>
        public string KensaName
        {
            get
            {
                string result = "";

                if (EyeKensaMaster.Dict.ContainsKey(KensaId))
                {
                    result = EyeKensaMaster.Dict[KensaId].Name;
                }

                return result;
            }
        }

        /// <summary>
        /// 検査略称
        /// </summary>
        public string KensaShort
        {
            get
            {
                string result = "";

                if (EyeKensaMaster.Dict.ContainsKey(KensaId))
                {
                    result = EyeKensaMaster.Dict[KensaId].Header;
                }

                return result;
            }
        }

        /// <summary>
        /// 検査結果
        /// </summary>
        public string Cont = "";

        /// <summary>
        /// スタッフ
        /// </summary>
        public string Staff = "";

        /// <summary>
        /// スタッフ名
        /// </summary>
        public string StaffName
        {
            get
            {
                string result = "";

                if (Dict.StaffDict.ContainsKey(Staff))
                {
                    result = Dict.StaffDict[Staff].Name;
                }

                return result;
            }
        }

        /// <summary>
        /// 保存日
        /// </summary>
        public string SaveDate = "";

        /// <summary>
        /// 保存時刻
        /// </summary>
        public string SaveTime = "";

        /// <summary>
        /// PDF保存フラグ
        /// </summary>
        public string PDFSave = "";

        /// <summary>
        /// PDF保存時刻
        /// </summary>
        public string PDFDate = "";

        /// <summary>
        /// PDF保存時刻
        /// </summary>
        public string PDFTime = "";
    }
}
