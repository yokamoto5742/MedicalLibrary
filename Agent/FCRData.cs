using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    class FCRData
    {
        public static bool MakeXML(PatBase ptInfo, List<FCRForm.StudyInfo> studyInfoList, List<string> sendToList, string handle)
        {
            bool b = false;

            try
            {
                string seq = "000000";

                string cmd = "select FCR_SEQ.nextval N from DUAL";

                List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    seq = tmp.GetDataString("N").PadLeft(6, '0');
                    break;
                }

                foreach (string path in sendToList)
                {
                    XmlTextWriter writer = new XmlTextWriter(path + "\\" + DateTime.Now.ToString("yyyyMMdd") + "-" + seq + ".xml", Encoding.GetEncoding("shift-jis"));
                    writer.Formatting = Formatting.Indented;

                    writer.WriteStartDocument(true);

                    writer.WriteStartElement("Data");
                    writer.WriteElementString("Format", "StudyInfo");
                    writer.WriteElementString("Handle", handle);

                    writer.WriteStartElement("Patient");
                    writer.WriteElementString("Patient.ID", ptInfo.Id);
                    writer.WriteElementString("Patient.NameKana", ptInfo.Kana);
                    writer.WriteElementString("Patient.NameKanji", ptInfo.Name);
                    writer.WriteElementString("Patient.Sex", ptInfo.SexNameEng);
                    writer.WriteElementString("Patient.BirthDate", ptInfo.Birth);
                    writer.WriteElementString("Patient.PhoneNumber", ptInfo.Tel);

                    foreach (FCRForm.StudyInfo studyInfo in studyInfoList)
                    {
                        writer.WriteStartElement("Study");
                        writer.WriteElementString("AccessionNumber", studyInfo.AccessionNumber);
                        writer.WriteElementString("StudyID", studyInfo.StudyID);
                        writer.WriteElementString("ScheduleStartDate", studyInfo.ScheduledStartDate);
                        writer.WriteElementString("ScheduleStartTime", studyInfo.ScheduledStartTime);
                        writer.WriteElementString("Department.Code", studyInfo.DepartmentCode);
                        writer.WriteElementString("Department.NameDbcs", studyInfo.DepartmentNameDbcs);
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();

                    writer.WriteEndElement();

                    writer.WriteEndDocument();

                    writer.Close();
                }

                b = true;
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex, false);
            }
            finally
            {
            }

            return b;
        }
    }
}
