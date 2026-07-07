using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class MC500
    {
        public string XMLPath = "";
        public string XMLFile = "";

        public string PtId = "";
        public string LaserDate = "";
        public string LaserTime = "";
        public int SEQ = 0;

        // R, L, B
        public string Eye = "";

        public string EyeJ
        {
            get
            {
                string result = "";

                switch (Eye)
                {
                    case "R":
                        result = "右";
                        break;

                    case "L":
                        result = "左";
                        break;

                    case "B":
                        result = "両";
                        break;
                }

                return result;
            }
        }

        public List<MC500Element> DataList = new List<MC500Element>();

        public string Cont1
        {
            get
            {
                string result = "";

                if (DataList.Count > 0)
                {
                    result += DataList[0].MakeDBString;
                }

                return result;
            }
        }

        public string Cont2
        {
            get
            {
                string result = "";

                if (DataList.Count > 1)
                {
                    result += DataList[1].MakeDBString;
                }

                return result;
            }
        }

        public string Cont3
        {
            get
            {
                string result = "";

                if (DataList.Count > 2)
                {
                    result += DataList[2].MakeDBString;
                }

                return result;
            }
        }

        public string Cont4
        {
            get
            {
                string result = "";

                if (DataList.Count > 3)
                {
                    result += DataList[3].MakeDBString;
                }

                return result;
            }
        }

        /// <summary>
        /// 医師のコメント
        /// </summary>
        public string Cont = "";

        public string Staff = "1034";
        public string SaveDate = "";
        public string SaveTime = "";

        static MC500 GetFromStdClass(StdClass tmp)
        {
            MC500 obj = new MC500();

            obj.PtId = tmp.DataDict["PATIENT_ID"].ToString();
            obj.LaserDate = tmp.DataDict["LASER_DATE"].ToString();
            int.TryParse(tmp.DataDict["LASER_SEQ"].ToString(), out obj.SEQ);
            obj.Eye = tmp.DataDict["EYE"].ToString();

            for (int i = 1; i <= 4; i++)
            {
                obj.DataList.Add(MC500Element.LoadFromDBString(tmp.DataDict["CONT" + i].ToString()));
            }

            obj.Cont = tmp.DataDict["CONT"].ToString();
            obj.Staff = tmp.DataDict["STAFF"].ToString();
            obj.SaveDate = tmp.DataDict["SAVE_DATE"].ToString();
            obj.SaveTime = tmp.DataDict["SAVE_TIME"].ToString();

            return obj;
        }

        public static List<MC500> LoadByPt(string pt_id)
        {
            List<MC500> list = new List<MC500>();

            if (pt_id.Length == 0)
            {
                return list;
            }

            string cmd = "select * from EYE_LASER " +
                " where PATIENT_ID = " + pt_id +
                " order by LASER_DATE";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }

        public static List<MC500> LoadByDate(string date1, string date9)
        {
            List<MC500> list = new List<MC500>();

            if (date1.Length != 8)
            {
                date1 = "20100101";
            }
            
            if (date9.Length != 8)
            {
                date9 = "29991231";
            }

            string cmd = "select * from EYE_LASER " +
                " where LASER_DATE >= " + date1 + " and LASER_DATE <= " + date9;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }


        /// <summary>
        /// 患者ID・日付の最大値を返す
        /// </summary>
        /// <param name="pt_id">患者ID</param>
        /// <param name="date">日付</param>
        /// <returns></returns>
        public static int MaxSEQByPtDate(string pt_id, string date)
        {
            int seq = 0;

            string cmd = "select max(LASER_SEQ) max_seq from EYE_LASER " +
                " where PATIENT_ID = " + pt_id + " and LASER_DATE = " + date;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                int.TryParse(tmp.DataDict["max_seq"].ToString(), out seq);
                break;
            }

            return seq;
        }

        /// <summary>
        /// 保存する
        /// </summary>
        public StdReturn Save(string staff_id)
        {
            if (staff_id.Length > 0)
            {
                this.Staff = staff_id;
            }

            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();
            obj.Db = DB.Db2;

            obj.Table = "EYE_LASER";

            obj.DataList.Add(new StdDbColumn("EYE", StdDbType.CHAR, this.Eye));
            obj.DataList.Add(new StdDbColumn("CONT1", StdDbType.VARCHAR2, this.Cont1));
            obj.DataList.Add(new StdDbColumn("CONT2", StdDbType.VARCHAR2, this.Cont2));
            obj.DataList.Add(new StdDbColumn("CONT3", StdDbType.VARCHAR2, this.Cont3));
            obj.DataList.Add(new StdDbColumn("CONT4", StdDbType.VARCHAR2, this.Cont4));
            obj.DataList.Add(new StdDbColumn("CONT", StdDbType.VARCHAR2, this.Cont));
            obj.DataList.Add(new StdDbColumn("STAFF", StdDbType.NUMBER, this.Staff));
            obj.DataList.Add(new StdDbColumn("SAVE_DATE", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("SAVE_TIME", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));

            if (this.SEQ < 1)
            {
                this.SEQ = MaxSEQByPtDate(this.PtId, this.LaserDate);
                this.SEQ++;

                obj.DataList.Add(new StdDbColumn("PATIENT_ID", StdDbType.NUMBER, this.PtId));
                obj.DataList.Add(new StdDbColumn("LASER_DATE", StdDbType.NUMBER, this.LaserDate));
                obj.DataList.Add(new StdDbColumn("LASER_SEQ", StdDbType.NUMBER, this.SEQ));

                sr = obj.InsertSQL();
            }
            else
            {
                obj.WhereList.Add("PATIENT_ID = " + this.PtId);
                obj.WhereList.Add("LASER_DATE = " + this.LaserDate);
                obj.WhereList.Add("LASER_SEQ = " + this.SEQ);

                sr = obj.UpdateSQL();
            }

            return sr;
        }

        /// <summary>
        /// 削除する
        /// </summary>
        public bool Delete()
        {
            bool result = false;

            string cmd = "delete from EYE_LASER " +
                " where PATIENT_ID = " + this.PtId + " and LASER_DATE = " + this.LaserDate + " and LASER_SEQ = " + this.SEQ;

            DB.Db2.ExecuteNonQuery(cmd);

            result = true;

            return result;
        }

        public string GetMsg()
        {
            string result = "Eye = " + Eye + "\n";

            foreach (MC500Element m in this.DataList)
            {
                result += m.GetMsg();
            }

            return result;
        }
    }

    /// <summary>
    /// LaseShot ごとのデータ
    /// 1つのXMLファイルの中に4つある
    /// </summary>
    public class MC500Element
    {
        public string LaserShot = "";

        public string ShotNo = "";
        public string ActEnergy = "";
        public string WaveLength = "";

        public string PowerMin = "";
        public string PowerMax = "";
        public string PowerAve = "";

        public string TimeMin = "";
        public string TimeMax = "";
        public string TimeAve = "";
        public string TimeActAve = "";

        public string SpotSizeMin = "";
        public string SpotSizeMax = "";
        public string SpotSizeAve = "";

        public string SP = "";

        public string GetMsg()
        {
            string result = "";

            result += "LasetShot = " + LaserShot + ", ";

            result += "ShotNo = " + ShotNo + ", ";
            result += "ActEnergy = " + ActEnergy + ", ";
            result += "WaveLength = " + WaveLength + ", ";

            result += "PowerMin = " + PowerMin + ", ";
            result += "PowerMax = " + PowerMax + ", ";
            result += "PowerAve = " + PowerAve + ", ";

            result += "TimeMin = " + TimeMin + ", ";
            result += "TimeMax = " + TimeMax + ", ";
            result += "TimeAve = " + TimeAve + ", ";
            result += "TimeActAve = " + TimeActAve + ", ";

            result += "SpotSizeMin = " + SpotSizeMin + ", ";
            result += "SpotSizeMax = " + SpotSizeMax + ", ";
            result += "SpotSizeAve = " + SpotSizeAve + ", ";

            result += "SP = " + SP + "\n";

            return result;
        }

        /// <summary>
        /// データベースの文字列からロードする
        /// </summary>
        public static MC500Element LoadFromDBString(string cont)
        {
            MC500Element m = new MC500Element();

            try
            {
                Dictionary<string, string> dict = DBStringToDict(cont);

                m.LaserShot = dict["LaserShot"];

                m.ShotNo = dict["ShotNo"];
                m.ActEnergy = dict["ActEnergy"];
                m.WaveLength = dict["WaveLength"];

                m.PowerMin = dict["PowerMin"];
                m.PowerMax = dict["PowerMax"];
                m.PowerAve = dict["PowerAve"];

                m.TimeMin = dict["TimeMin"];
                m.TimeMax = dict["TimeMax"];
                m.TimeAve = dict["TimeAve"];
                m.TimeActAve = dict["TimeActAve"];

                m.SpotSizeMin = dict["SpotSizeMin"];
                m.SpotSizeMax = dict["SpotSizeMax"];
                m.SpotSizeAve = dict["SpotSizeAve"];

                if (dict.ContainsKey("SP"))
                {
                    m.SP = dict["SP"];
                }
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex);
            }

            return m;
        }

        /// <summary>
        /// データベースに保存する文字列
        /// </summary>
        public string MakeDBString
        {
            get
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();

                dict["LaserShot"] = LaserShot;

                dict["ShotNo"] = ShotNo;
                dict["ActEnergy"] = ActEnergy;
                dict["WaveLength"] = WaveLength;

                dict["PowerMin"] = PowerMin;
                dict["PowerMax"] = PowerMax;
                dict["PowerAve"] = PowerAve;

                dict["TimeMin"] = TimeMin;
                dict["TimeMax"] = TimeMax;
                dict["TimeAve"] = TimeAve;
                dict["TimeActAve"] = TimeActAve;

                dict["SpotSizeMin"] = SpotSizeMin;
                dict["SpotSizeMax"] = SpotSizeMax;
                dict["SpotSizeAve"] = SpotSizeAve;

                dict["SP"] = SP;

                return DictToDBString(dict);
            }
        }

        /// <summary>
        /// 電子カルテに貼り付ける文字列
        /// </summary>
        public string GetKarteString
        {
            get
            {
                string result = "";

                result += LaserShot + ",";
                result += SpotSizeAve + ",";
                result += TimeAve + ",";
                result += PowerAve + ",";
                result += ShotNo + ",";
                result += SP;

                return result;
            }
        }

        /// <summary>
        /// 文字列辞書からDB保存文字列に変換
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        string DictToDBString(Dictionary<string, string> dict)
        {
            string result = "";

            foreach (string key in dict.Keys)
            {
                if (result.Length > 0)
                {
                    result += "\r\n";
                }

                result += key + "," + dict[key].Replace("\r\n", "<CR+LF>");
            }

            return result;
        }

        /// <summary>
        /// DB保存文字列から文字列辞書に変換
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static Dictionary<string, string> DBStringToDict(string str)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            string[] strs = str.Split('\n');

            foreach (string s in strs)
            {
                string ss = s.Trim('\r');

                if (!ss.Contains(","))
                {
                    continue;
                }

                string s1 = "";
                string s2 = "";

                if (ss.IndexOf(',') > 0)
                {
                    s1 = ss.Substring(0, ss.IndexOf(','));
                }

                if (ss.Length > ss.IndexOf(',') + 1)
                {
                    s2 = ss.Substring(ss.IndexOf(',') + 1).Replace("<CR+LF>", "\r\n");
                }

                dict[s1] = s2;
            }

            return dict;
        }
    }
}
