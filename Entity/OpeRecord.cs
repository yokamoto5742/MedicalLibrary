using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class OpeRecord
    {
        public int SEQ = 0;

        public string PtId = "";

        PatBase _Pat = new PatBase();

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
        /// 手術日
        /// </summary>
        public string OpeDate = "";

        /// <summary>
        /// 入院日
        /// </summary>
        public string AdmDate = "";

        /// <summary>
        /// 身長
        /// </summary>
        public int Height = 0;

        /// <summary>
        /// 体重
        /// </summary>
        public float Weight = 0.0F;

        /// <summary>
        /// 血液型
        /// </summary>
        public int BloodType = 0;

        public string BloodTypeName
        {
            get
            {
                return OpeRecordBloodType.GetData(this.BloodType).Name;
            }
        }

        /// <summary>
        /// 主科
        /// </summary>
        public string MainDept = "";

        public string MainDeptName
        {
            get
            {
                return Dict.DeptDict.ContainsKey(this.MainDept) ? Dict.DeptDict[this.MainDept].ShortName : "";
            }
        }

        /// <summary>
        /// 主治医
        /// </summary>
        public string MainDoctor
        {
            get
            {
                string s = "";

                if (this.MainDoctor1.Length > 0 && Dict.DoctorDict.ContainsKey(this.MainDoctor1))
                {
                    s += Dict.DoctorDict[this.MainDoctor1].ShortName;
                }

                if (this.MainDoctor2.Length > 0 && Dict.DoctorDict.ContainsKey(this.MainDoctor2))
                {
                    if (s.Length > 0)
                    {
                        s += "／";
                    }

                    s += Dict.DoctorDict[this.MainDoctor2].ShortName;
                }

                return s;
            }
        }

        /// <summary>
        /// 主治医１
        /// </summary>
        public string MainDoctor1 = "";

        public string MainDoctorName1
        {
            get
            {
                return Dict.DoctorDict.ContainsKey(this.MainDoctor1) ? Dict.DoctorDict[this.MainDoctor1].Name : "";
            }
        }

        /// <summary>
        /// 主治医２
        /// </summary>
        public string MainDoctor2 = "";

        public string MainDoctorName2
        {
            get
            {
                return Dict.DoctorDict.ContainsKey(this.MainDoctor2) ? Dict.DoctorDict[this.MainDoctor2].Name : "";
            }
        }

        /// <summary>
        /// 副科
        /// </summary>
        public string SubDept = "";

        public string SubDeptName
        {
            get
            {
                return Dict.DeptDict.ContainsKey(this.SubDept) ? Dict.DeptDict[this.SubDept].ShortName : "";
            }
        }

        /// <summary>
        /// 副科担当医１
        /// </summary>
        public string SubDoctor1 = "";

        public string SubDoctorName1
        {
            get
            {
                return Dict.DoctorDict.ContainsKey(this.SubDoctor1) ? Dict.DoctorDict[this.SubDoctor1].Name : "";
            }
        }


        /// <summary>
        /// 手術医
        /// </summary>
        public string OpeDoctor
        {
            get
            {
                string s = "";

                if (this.OpeDoctor1.Length > 0 && Dict.DoctorDict.ContainsKey(this.OpeDoctor1))
                {
                    s += Dict.DoctorDict[OpeDoctor1].ShortName;
                }

                if (this.OpeDoctor2.Length > 0 && Dict.DoctorDict.ContainsKey(this.OpeDoctor2))
                {
                    if (s.Length > 0)
                    {
                        s += "／";
                    }

                    s += Dict.DoctorDict[OpeDoctor2].ShortName;
                }

                if (this.OpeDoctor3.Length > 0 && Dict.DoctorDict.ContainsKey(this.OpeDoctor3))
                {
                    if (s.Length > 0)
                    {
                        s += "／";
                    }

                    s += Dict.DoctorDict[OpeDoctor3].ShortName;
                }

                return s;
            }
        }

        /// <summary>
        /// 手術医１
        /// </summary>
        public string OpeDoctor1 = "";

        public string OpeDoctorName1
        {
            get
            {
                return Dict.DoctorDict.ContainsKey(this.OpeDoctor1) ? Dict.DoctorDict[this.OpeDoctor1].Name : "";
            }
        }

        /// <summary>
        /// 手術医２
        /// </summary>
        public string OpeDoctor2 = "";

        public string OpeDoctorName2
        {
            get
            {
                return Dict.DoctorDict.ContainsKey(this.OpeDoctor2) ? Dict.DoctorDict[this.OpeDoctor2].Name : "";
            }
        }

        /// <summary>
        /// 手術医３
        /// </summary>
        public string OpeDoctor3 = "";

        public string OpeDoctorName3
        {
            get
            {
                return Dict.DoctorDict.ContainsKey(this.OpeDoctor3) ? Dict.DoctorDict[this.OpeDoctor3].Name : "";
            }
        }

        /// <summary>
        /// 麻酔 30字
        /// </summary>
        public string Anes = "";

        /// <summary>
        /// 手術時間
        /// </summary>
        public int OpeTime = 0;

        /// <summary>
        /// 麻酔時間
        /// </summary>
        public int AnesTime = 0;

        /// <summary>
        /// 術式
        /// </summary>
        public string OpeName
        {
            get
            {
                string s = "";

                foreach (OpeRecordICD9 obj in this.ICD9_List)
                {
                    if (obj.Name.Length == 0)
                    {
                        continue;
                    }

                    if (s.Length > 0)
                    {
                        s += "／";
                    }

                    s += obj.Name;
                }

                return s;
            }
        }

        /// <summary>
        /// 術式リスト
        /// </summary>
        public List<OpeRecordICD9> ICD9_List = new List<OpeRecordICD9>();

        /// <summary>
        /// 診断
        /// </summary>
        public string DiagName
        {
            get
            {
                string s = "";

                foreach (OpeRecordICD10 obj in this.ICD10_List)
                {
                    if (obj.Name.Length == 0)
                    {
                        continue;
                    }

                    if (s.Length > 0)
                    {
                        s += "／";
                    }

                    s += obj.Name;
                }

                return s;
            }
        }

        /// <summary>
        /// 診断リスト
        /// </summary>
        public List<OpeRecordICD10> ICD10_List = new List<OpeRecordICD10>();

        /// <summary>
        /// 術中出血（mL）
        /// </summary>
        public int BloodOut = 0;

        /// <summary>
        /// 輸血単位（U）
        /// </summary>
        public int BloodIn = 0;

        /// <summary>
        /// 術前所見 1500字
        /// </summary>
        public string BeforeComment = "";

        /// <summary>
        /// 手術所見 10～1500字
        /// </summary>
        public string OpeComment = "";

        /// <summary>
        /// 病理所見 200字
        /// </summary>
        public string Pathology = "";

        /// <summary>
        /// 備考 1500字
        /// </summary>
        public string Note = "";


        public int DeleteFlg = 0;

        public int PdfOk = 0;

        public int PdfExist = 0;

        public int SaveTime = 0;


        static OpeRecord GetFromStdClass(StdClass tmp)
        {
            OpeRecord obj = new OpeRecord();

            obj.SEQ = tmp.GetDataInt("OPE_RECORD_ID");
            obj.PtId = tmp.GetDataString("PATIENT_ID");
            obj.AdmDate = tmp.GetDataString("ADMISSION_DATE").Length > 0 ? DateTime.Parse(tmp.GetDataString("ADMISSION_DATE")).ToString("yyyyMMdd") : "";
            obj.OpeDate = tmp.GetDataString("OPE_DATE").Length > 0 ? DateTime.Parse(tmp.GetDataString("OPE_DATE")).ToString("yyyyMMdd") : "";

            obj.Height = tmp.GetDataInt("HEIGHT");
            obj.Weight = tmp.GetDataFloat("WEIGHT");
            obj.BloodType = tmp.GetDataInt("BLOOD_TYPE");

            obj.MainDept = tmp.GetDataString("MAIN_DEPARTMENT").TrimStart('0');
            obj.MainDoctor1 = tmp.GetDataString("MAIN_DOCTOR1");
            obj.MainDoctor2 = tmp.GetDataString("MAIN_DOCTOR2");

            obj.SubDept = tmp.GetDataString("SUB_DEPARTMENT").TrimStart('0');
            obj.SubDoctor1 = tmp.GetDataString("SUB_DOCTOR1");

            obj.OpeDoctor1 = tmp.GetDataString("OPE_DOCTOR1");
            obj.OpeDoctor2 = tmp.GetDataString("OPE_DOCTOR2");
            obj.OpeDoctor3 = tmp.GetDataString("OPE_DOCTOR3");

            obj.Anes = tmp.GetDataString("ANESTHESIA");

            obj.OpeTime = tmp.GetDataInt("OPE_TIME");
            obj.AnesTime = tmp.GetDataInt("ANES_TIME");

            for (int i = 1; i <= 5; i++)
            {
                OpeRecordICD10 obj2 = new OpeRecordICD10();

                obj2.Code = tmp.GetDataString("DIAGNOSIS_CODE" + i);
                obj2.SubCode = tmp.GetDataString("DIAGNOSIS_SUB_CODE" + i);
                obj2.Name = tmp.GetDataString("DIAGNOSIS_NAME" + i);
                obj2.PreNote = tmp.GetDataString("DIAGNOSIS_PRE_NOTE" + i);
                obj2.Note = tmp.GetDataString("DIAGNOSIS_NOTE" + i);

                obj.ICD10_List.Add(obj2);
            }

            for (int i = 1; i <= 5; i++)
            {
                OpeRecordICD9 obj2 = new OpeRecordICD9();

                obj2.Code = tmp.GetDataString("OPE_CODE" + i);
                obj2.SubCode = tmp.GetDataString("OPE_SUB_CODE" + i);
                obj2.Name = tmp.GetDataString("OPE_NAME" + i);
                obj2.PreNote = tmp.GetDataString("OPE_PRE_NOTE" + i);
                obj2.Note = tmp.GetDataString("OPE_NOTE" + i);

                obj.ICD9_List.Add(obj2);
            }

            obj.BloodOut = tmp.GetDataInt("BLOOD_OUT");
            obj.BloodIn = tmp.GetDataInt("BLOOD_IN");

            obj.BeforeComment = tmp.GetDataString("BEFORE_COMMENT");
            obj.OpeComment = tmp.GetDataString("OPE_COMMENT");
            obj.Pathology = tmp.GetDataString("PATHOLOGY");
            obj.Note = tmp.GetDataString("NOTE");

            obj.DeleteFlg = tmp.GetDataInt("DELETE_FLG", 0);
            obj.PdfOk = tmp.GetDataInt("PDF_OK", 0);
            obj.PdfExist = tmp.GetDataInt("PDF_EXIST", 0);
            obj.SaveTime = tmp.GetDataInt("SAVE_TIME", 0);

            return obj;
        }


        /// <summary>
        /// 指定した患者の手術記録を取得する
        /// </summary>
        /// <param name="pt_id"></param>
        /// <returns></returns>
        public static List<OpeRecord> GetListByPat(string pt_id)
        {
            List<OpeRecord> list = new List<OpeRecord>();

            if (pt_id.Length == 0)
            {
                return list;
            }

            string cmd = "select * from OPE_RECORD " +
                " where PATIENT_ID = " + pt_id +
                " order by OPE_DATE desc, OPE_RECORD_ID desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }


        /// <summary>
        /// 指定期間中の手術記録を取得する
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static List<OpeRecord> GetListByDates(string date1, string date2)
        {
            List<OpeRecord> list = new List<OpeRecord>();

            if (!DateTimeAgent.IsDate(date1) || !DateTimeAgent.IsDate(date2))
            {
                return list;
            }

            string cmd = "select * from OPE_RECORD " +
                " where OPE_DATE >= to_date('" + date1 + "') and OPE_DATE <= to_date('" + date2 + "') " +
                " order by OPE_DATE desc, OPE_RECORD_ID desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }
    }


    /// <summary>
    /// 血液型
    /// </summary>
    public class OpeRecordBloodType
    {
        public int Code = 0;

        public string Name = "";

        static List<OpeRecordBloodType> _List = new List<OpeRecordBloodType>();

        public static List<OpeRecordBloodType> List
        {
            get
            {
                if (_List == null || _List.Count == 0)
                {
                    OpeRecordBloodType obj;

                    obj = new OpeRecordBloodType();
                    obj.Code = 0;
                    obj.Name = "不明";
                    _List.Add(obj);

                    obj = new OpeRecordBloodType();
                    obj.Code = 10;
                    obj.Name = "A型(+)";
                    _List.Add(obj);

                    obj = new OpeRecordBloodType();
                    obj.Code = 20;
                    obj.Name = "B型(+)";
                    _List.Add(obj);

                    obj = new OpeRecordBloodType();
                    obj.Code = 30;
                    obj.Name = "AB型(+)";
                    _List.Add(obj);

                    obj = new OpeRecordBloodType();
                    obj.Code = 40;
                    obj.Name = "O型(+)";
                    _List.Add(obj);

                    obj = new OpeRecordBloodType();
                    obj.Code = 11;
                    obj.Name = "A型(-)";
                    _List.Add(obj);

                    obj = new OpeRecordBloodType();
                    obj.Code = 21;
                    obj.Name = "B型(-)";
                    _List.Add(obj);

                    obj = new OpeRecordBloodType();
                    obj.Code = 31;
                    obj.Name = "AB型(-)";
                    _List.Add(obj);

                    obj = new OpeRecordBloodType();
                    obj.Code = 41;
                    obj.Name = "O型(-)";
                    _List.Add(obj);
                }

                return _List;
            }
        }

        public static OpeRecordBloodType GetData(int code)
        {
            OpeRecordBloodType obj = new OpeRecordBloodType();

            foreach (OpeRecordBloodType b in OpeRecordBloodType.List)
            {
                if (b.Code.Equals(obj.Code))
                {
                    obj = b;
                    break;
                }
            }

            return obj;
        }
    }


    public class OpeRecordICD9
    {
        public string Code = "";

        public string SubCode = "";

        /// <summary>
        /// 手術名
        /// </summary>
        public string Name = "";

        /// <summary>
        /// 前方コメント
        /// </summary>
        public string PreNote = "";

        /// <summary>
        /// 後方コメント
        /// </summary>
        public string Note = "";
    }


    public class OpeRecordICD10
    {
        public string Code = "";

        public string SubCode = "";

        /// <summary>
        /// 診断名
        /// </summary>
        public string Name = "";

        /// <summary>
        /// 前方コメント
        /// </summary>
        public string PreNote = "";

        /// <summary>
        /// 後方コメント
        /// </summary>
        public string Note = "";
    }
}
