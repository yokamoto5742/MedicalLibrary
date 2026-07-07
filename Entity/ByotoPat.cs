using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    /*
    public class ByotoPat : StdEntity
    {
        public enum ByotoTime : int
        {
            NO = 0,
            AM = 1,
            PM = 2,
            MT = 3
        }
        

        public string PtId = "";

        public string Name = "";

        public string Kana = "";

        public string Sex = "";

        public string SexString
        {
            get
            {
                string s = "";

                if (this.Sex.Equals("1"))
                {
                    s = "男";
                }
                else if (this.Sex.Equals("2"))
                {
                    s = "女";
                }

                return s;
            }
        }

        public string Birth = "";

        public int Age
        {
            get
            {
                return DateTimeAgent.AgeCalc(this.Birth, DateTime.Now.ToString("yyyyMMdd"));
            }
        }

        public string ByotoCode = "";

        public string RoomCode = "";

        public int BedSEQ = 1;


        public static List<ByotoPat> GetList(string date)
        {
            List<ByotoPat> list = new List<ByotoPat>();

            // 現在の入院患者の取得
            string cmd = "select t1.*, Trim(t2.IM01RC_F04) 氏名, t2.IM01RC_F05, t2.IM01RC_F10 " +
                " from AMP_患者入院マスター t1, IM01RC t2 " +
                " where t1.退院日 = 0 and t1.患者コード = t2.IM01RC_F01";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                ByotoPat obj = new ByotoPat();

                obj.PtId = tmp.DataDict["患者コード"].ToString();
                obj.Name = tmp.DataDict["氏名"].ToString();
                obj.Kana = tmp.DataDict["カナ氏名"].ToString();
                obj.Sex = tmp.DataDict["IM01RC_F05"].ToString();
                obj.Birth = tmp.DataDict["IM01RC_F10"].ToString();
                obj.ByotoCode = tmp.DataDict["病棟コード"].ToString();
                obj.RoomCode = tmp.DataDict["病室コード"].ToString();
                int.TryParse(tmp.DataDict["ベッド番号"].ToString(), out obj.BedSEQ);

                list.Add(obj);
            }

            return list;
        }

        public static List<ByotoPat> GetList(string date, ByotoTime byoto_time)
        {
            List<ByotoPat> list = new List<ByotoPat>();

            // 現在の入院患者の取得
            string cmd = "select t1.*, Trim(t2.IM01RC_F04) 氏名, t2.IM01RC_F05, t2.IM01RC_F10 " +
                " from AMP_患者入院マスター t1, IM01RC t2 " +
                " where t1.退院日 = 0 and t1.患者コード = t2.IM01RC_F01";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                ByotoPat obj = new ByotoPat();

                obj.PtId = tmp.DataDict["患者コード"].ToString();
                obj.Name = tmp.DataDict["氏名"].ToString();
                obj.Kana = tmp.DataDict["カナ氏名"].ToString();
                obj.Sex = tmp.DataDict["IM01RC_F05"].ToString();
                obj.Birth = tmp.DataDict["IM01RC_F10"].ToString();
                obj.ByotoCode = tmp.DataDict["病棟コード"].ToString();
                obj.RoomCode = tmp.DataDict["病室コード"].ToString();
                int.TryParse(tmp.DataDict["ベッド番号"].ToString(), out obj.BedSEQ);

                list.Add(obj);
            }

            return list;
        }


        public static Dictionary<string, List<ByotoPat>> GetDict(string date, ByotoTime byoto_time)
        {
            Dictionary<string, List<ByotoPat>> dict = new Dictionary<string, List<ByotoPat>>();

            List<ByotoPat> list = GetList(date, byoto_time);

            foreach (ByotoRoom r in ByotoRoom.Dict.Values)
            {
                List<ByotoPat> tmp_list = new List<ByotoPat>();

                foreach (ByotoPat p in list)
                {
                    if (p.RoomCode.Equals(r.Code))
                    {
                        tmp_list.Add(p);
                    }
                }

                dict.Add(r.Code, tmp_list);
            }

            return dict;
        }
    }
     */
}
