using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class SoapDetail : SoapKey
    {
        /// <summary>
        /// ADT_ＳＯＡＰデータ明細には存在しないカラムだが、辞書を作るために必要
        /// </summary>
        public int SoapDate = 0;

        public string Kind = "";

        public string KindName
        {
            get
            {
                string s = "";
                // 90: PDFファイル（PdfDoc で取得するためここには表記しない）

                if (Kind.Equals("1"))
                {
                    s = "S >";
                }
                else if (Kind.Equals("2"))
                {
                    s = "O >";
                }
                else if (Kind.Equals("3"))
                {
                    s = "A >";
                }
                else if (Kind.Equals("4"))
                {
                    s = "P >";
                }
                else if (Kind.Equals("5"))
                {
                    s = "F >";
                }
                else if (Kind.Equals("6"))
                {
                    s = "I >";
                }
                else if (Kind.Equals("7"))
                {
                    s = "E >";
                }
                else if (Kind.Equals("9"))
                {
                    s = "サ>";
                }
                else if (Kind.Equals("10"))
                {
                    s = "H>";
                }
                else if (Kind.Equals("11"))
                {
                    s = "シ>";
                }
                else if (Kind.Equals("20"))
                {
                    s = "オ>";
                }
                else if (Kind.Equals("21"))
                {
                    s = "オ>";
                }
                return s;
            }
        }

        public string Cont = "";

        public string ContShow(float font_size, int width)
        {
            return AppString.Wrap(this.Cont, font_size, width);
        }

        public Dictionary<string, SoapImg> ImgDict = new Dictionary<string, SoapImg>();

        /// <summary>
        /// 画像がサーバー上にあるかどうか
        /// </summary>
        public bool ImgExist
        {
            get
            {
                bool exist = false;

                foreach (SoapImg img in ImgDict.Values)
                {
                    if (img.ImgExist)
                    {
                        exist = true;
                        break;
                    }
                }

                return exist;
            }
        }

        public static Dictionary<string, List<SoapDetail>> GetDict(string pt_id, List<string> date_list)
        {
            Dictionary<string, List<SoapDetail>> dict = new Dictionary<string, List<SoapDetail>>();

            if (date_list.Count == 0 || AppString.ConcatList(date_list, ",").Length == 0)
            {
                return dict;
            }
            string cmd = "select th.KEY_SOAP, th.P_ID, th.INOUT, th.REG_DATE, th.REG_TIME, th.REG_USR, th.SOAP_DATE " +
                " , tm.SOAP_TYPE, tm.SOAP_TEXT, tm.FIGURE_1, tm.FIGURE_2, tm.FIGURE_3, tm.FIGURE_4, tm.FIGURE_5 " +
                " from D_SOAP_HEADER th, D_SOAP_DETAIL tm " +
                " where th.P_ID = " + pt_id + " and tm.P_ID = " + pt_id +
                " and th.SOAP_DATE in (" + AppString.ConcatList(date_list, ",") + ") " +
                " and (th.DEL_FLG is null or th.DEL_FLG = 0) " +
                " and tm.SOAP_DATE in (" + AppString.ConcatList(date_list, ",") + ") " +
                " and (tm.DEL_FLG is null or tm.DEL_FLG = 0) " +
                " and th.KEY_SOAP = tm.KEY_SOAP " +
                " order by KEY_SOAP desc, SOAP_TYPE";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                SoapDetail obj = new SoapDetail();

                obj.Key = tmp.GetDataString("KEY_SOAP");
                obj.PtId = pt_id;
                obj.InOut = tmp.GetDataInt("INOUT");
                obj.RegDate = tmp.GetDataInt("REG_DATE");
                obj.RegTime = tmp.GetDataInt("REG_TIME");
                obj.RegStaff = tmp.GetDataString("REG_USR");

                obj.SoapDate = tmp.GetDataInt("SOAP_DATE");
                obj.Kind = tmp.GetDataString("SOAP_TYPE");
                obj.Cont = tmp.GetDataString("SOAP_TEXT");

                int j = 1;

                for (int i = 1; i <= 5; i++)
                {
                    if (tmp.GetDataString("FIGURE_" + i).Length == 0)
                    {
                        continue;
                    }

                    obj.ImgDict.Add(j.ToString(), new SoapImg(j.ToString(), tmp.GetDataString("FIGURE_" + i), obj.RegDate));
                    j++;
                }

                if (dict.ContainsKey(obj.SoapDate.ToString()))
                {
                    dict[obj.SoapDate.ToString()].Add(obj);
                }
                else
                {
                    List<SoapDetail> soap_data_list = new List<SoapDetail>();
                    soap_data_list.Add(obj);
                    dict.Add(obj.SoapDate.ToString(), soap_data_list);
                }
            }
            return dict;
        }

        public static Dictionary<string, Dictionary<string, List<SoapDetail>>> GetDataDict(string pt_id, List<string> date_list)
        {
            Dictionary<string, Dictionary<string, List<SoapDetail>>> dict = new Dictionary<string, Dictionary<string, List<SoapDetail>>>();

            if (date_list.Count == 0)
            {
                return dict;
            }

            Dictionary<string, List<SoapDetail>> dict_data = SoapDetail.GetDict(pt_id, date_list);

            foreach (string date in dict_data.Keys)
            {
                List<SoapDetail> data_list = dict_data[date];

                if (dict.ContainsKey(date))
                {
                    Dictionary<string, List<SoapDetail>> tmp_dict = dict[date];

                    foreach (SoapDetail data in data_list)
                    {
                        if (tmp_dict.ContainsKey(data.Key))
                        {
                            tmp_dict[data.Key].Add(data);
                        }
                        else
                        {
                            List<SoapDetail> tmp_list = new List<SoapDetail>();
                            tmp_list.Add(data);
                            tmp_dict.Add(data.Key, tmp_list);
                        }
                    }
                }
                else
                {
                    Dictionary<string, List<SoapDetail>> tmp_dict = new Dictionary<string, List<SoapDetail>>();

                    foreach (SoapDetail data in data_list)
                    {
                        if (tmp_dict.ContainsKey(data.Key))
                        {
                            tmp_dict[data.Key].Add(data);
                        }
                        else
                        {
                            List<SoapDetail> tmp_list = new List<SoapDetail>();
                            tmp_list.Add(data);
                            tmp_dict.Add(data.Key, tmp_list);
                        }
                    }

                    dict.Add(date, tmp_dict);
                }
            }

            return dict;
        }

        /// <summary>
        /// 前回の外来SOAPの内容を取得する（眼科）
        /// </summary>
        /// <param name="pt_list">対象患者リスト</param>
        /// <param name="crit_date">この日より前に書かれたSOAP</param>
        /// <param name="dept">科</param>
        /// <param name="kind">SOAP区分</param>
        /// <returns></returns>
        public static List<SoapDetail> GetLastList(List<string> pt_list, string crit_date, string dept, string kind)
        {
            List<SoapDetail> list = new List<SoapDetail>();

            if (pt_list.Count == 0 || AppString.ConcatList(pt_list, ",").Length == 0 ||
                !DateTimeAgent.IsDate(crit_date) || dept.Length == 0 || kind.Length == 0)
            {
                return list;
            }

            string cmd = "";
            List<StdClass> tmp_list;

            foreach (string s in AppString.ConcatLists(pt_list, ","))
            {
                cmd = "select * from " +
                    "(select th.P_ID, th.REG_DATE, th.REG_TIME, th.REG_USR, th.SOAP_DATE," +
                    " td.SOAP_TYPE, td.SOAP_TEXT," +
                    " row_number() over (partition by td.P_ID order by th.SOAP_DATE desc, th.REG_DATE desc, th.REG_TIME desc) rn" +
                    " from D_SOAP_HEADER th, D_SOAP_DETAIL td" +
                    " where th.P_ID in (" + AppString.ConcatList(pt_list, ",") + ")" +
                    " and th.INOUT = 1" +
                    " and th.SOAP_DATE < " + crit_date +
                    " and th.DEPT = " + dept +
                    " and td.P_ID in (" + AppString.ConcatList(pt_list, ",") + ")" +
                    " and td.SOAP_TYPE = " + kind +
                    " and th.KEY_SOAP = td.KEY_SOAP" +
                    " ) tt" +
                    " where tt.RN = 1";

                tmp_list = StdClass.GetList(DB.Db3, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    SoapDetail obj = new SoapDetail();

                    obj.PtId = tmp.GetDataString("P_ID");
                    obj.InOut = 1;
                    obj.RegDate = tmp.GetDataInt("REG_DATE");
                    obj.RegTime = tmp.GetDataInt("REG_TIME");
                    obj.RegStaff = tmp.GetDataString("REG_USR");
                    obj.SoapDate = tmp.GetDataInt("SOAP_DATE");
                    obj.Kind = tmp.GetDataString("SOAP_TYPE");
                    obj.Cont = tmp.GetDataString("SOAP_TEXT");

                    list.Add(obj);
                }
            }
            return list;
        }

        public StdReturn Insert(SoapKey key)
        {
            StdReturn sr = new StdReturn();

            if (key.PtId.Length == 0 || key.InOut == 0 || key.RegDate == 0 ||
                key.RegStaff.Length == 0 || key.SEQ == 0)
            {
                return sr;
            }
            return sr;
        }
    }

    public class SoapImg : SoapKey
    {
        /// <summary>
        /// 1～5 の数字。同一SOAP内の一意の番号。
        /// </summary>
        public string Code = "";

        /// <summary>
        /// 元のシェーマ画像または貼り付け画像
        /// </summary>
        public Image OrgImage;

        public string ImgFile = "";

        public string ImgPath
        {
            get
            {
                return LibSettings.Current.SoapImageFolder + "\\" + RegDate.ToString().Substring(0, 4) + "\\" + ImgFile;
            }
        }

        public string ImgTmpPath
        {
            get
            {
                return LibSettings.Current.SoapImageTemporaryFolder + "\\" + ImgFile;
            }
        }

        public bool ImgExist
        {
            get
            {
                return File.Exists(ImgPath);
            }
        }

        public bool ImgTmpExist
        {
            get
            {
                return File.Exists(ImgTmpPath);
            }
        }

        public SoapImg(string img_file, int reg_date)
        {
            this.ImgFile = img_file;
            this.RegDate = reg_date;
        }

        public SoapImg(string code, string img_file, int reg_date)
        {
            this.Code = code;
            this.ImgFile = img_file;
            this.RegDate = reg_date;
        }
    }
}
