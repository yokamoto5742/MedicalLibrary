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
#if INNO
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
#else
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
#endif
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
#if INNO
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
#else
            string cmd = "select th.患者コード, th.入外区分, th.登録日, th.登録時間, th.登録者, th.連番, th.ＳＯＡＰ対象日 " +
                " , tm.ＳＯＡＰ区分, tm.ＳＯＡＰ内容, tm.図ファイル名１, tm.図ファイル名２, tm.図ファイル名３, tm.図ファイル名４, tm.図ファイル名５ " +
                " from macs.ADT_ＳＯＡＰデータヘッダ th, macs.ADT_ＳＯＡＰデータ明細 tm " +
                " where th.患者コード = " + pt_id + " and tm.患者コード = " + pt_id +
                " and th.ＳＯＡＰ対象日 in (" + AppString.ConcatList(date_list, ",") + ") " +
                " and th.削除フラグ = 0 " +
                " and th.患者コード = tm.患者コード and th.入外区分 = tm.入外区分 " +
                " and th.登録日 = tm.登録日 and th.登録時間 = tm.登録時間 and th.登録者 = tm.登録者 " +
                " and th.連番 = tm.連番 " +
                " order by 更新日 desc, 更新時間 desc, ＳＯＡＰ区分";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                SoapDetail obj = new SoapDetail();

                obj.PtId = pt_id;
                obj.InOut = tmp.GetDataInt("入外区分");
                obj.RegDate = tmp.GetDataInt("登録日");
                obj.RegTime = tmp.GetDataInt("登録時間");
                obj.RegStaff = tmp.GetDataString("登録者");
                obj.SEQ = tmp.GetDataInt("連番");

                obj.SoapDate = tmp.GetDataInt("ＳＯＡＰ対象日");
                obj.Kind = tmp.GetDataString("ＳＯＡＰ区分");
                obj.Cont = tmp.GetDataString("ＳＯＡＰ内容");

                int j = 1;

                for (int i = 1; i <= 5; i++)
                {
                    if (tmp.GetDataString("図ファイル名" + AppString.HanToZen(i.ToString())).Length == 0)
                    {
                        continue;
                    }

                    obj.ImgDict.Add(j.ToString(), new SoapImg(j.ToString(), tmp.GetDataString("図ファイル名" + AppString.HanToZen(i.ToString())), obj.RegDate));
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
#endif
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

#if INNO
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
#else
            foreach (string s in AppString.ConcatLists(pt_list, ","))
            {
                cmd = "select * from " +
                    "(select th.患者コード, th.登録日, th.登録時間, th.登録者, th.ＳＯＡＰ対象日," +
                    " td.ＳＯＡＰ区分, td.ＳＯＡＰ内容," +
                    " row_number() over (partition by td.患者コード order by th.ＳＯＡＰ対象日 desc, th.登録日 desc, th.登録時間 desc) rn" +
                    " from ADT_ＳＯＡＰデータヘッダ th, ADT_ＳＯＡＰデータ明細 td" +
                    " where th.患者コード in (" + AppString.ConcatList(pt_list, ",") + ")" +
                    " and th.入外区分 = 1" +
                    " and th.登録日 < " + crit_date +
                    " and th.登録科 = " + dept +
                    " and td.患者コード in (" + AppString.ConcatList(pt_list, ",") + ")" +
                    " and td.入外区分 = 1" +
                    " and td.登録日 < " + crit_date +
                    " and td.ＳＯＡＰ区分 = " + kind +
                    " and th.患者コード = td.患者コード" +
                    " and th.入外区分 = td.入外区分" +
                    " and th.登録日 = td.登録日" +
                    " and th.登録時間 = td.登録時間" +
                    " and th.登録者 = td.登録者" +
                    " and th.連番 = td.連番" +
                    " ) tt" +
                    " where tt.RN = 1";

                tmp_list = StdClass.GetList(DB.Db1, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    SoapDetail obj = new SoapDetail();

                    obj.PtId = tmp.GetDataString("患者コード");
                    obj.InOut = 1;
                    obj.RegDate = tmp.GetDataInt("登録日");
                    obj.RegTime = tmp.GetDataInt("登録時間");
                    obj.RegStaff = tmp.GetDataString("登録者");
                    obj.SoapDate = tmp.GetDataInt("ＳＯＡＰ対象日");
                    obj.Kind = tmp.GetDataString("ＳＯＡＰ区分");
                    obj.Cont = tmp.GetDataString("ＳＯＡＰ内容");

                    list.Add(obj);
                }
            }
#endif
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
#if INNO
#else
            StdDbClass obj = new StdDbClass();

            obj.Table = "ADT_ＳＯＡＰデータ明細";
            obj.Db = DB.Db1;

            obj.DataList.Add(new StdDbColumn("患者コード", StdDbType.NUMBER, key.PtId));
            obj.DataList.Add(new StdDbColumn("入外区分", StdDbType.NUMBER, key.InOut));
            obj.DataList.Add(new StdDbColumn("登録日", StdDbType.NUMBER, key.RegDate));
            obj.DataList.Add(new StdDbColumn("登録時間", StdDbType.NUMBER, key.RegTime));
            obj.DataList.Add(new StdDbColumn("登録者", StdDbType.NUMBER, key.RegStaff));
            obj.DataList.Add(new StdDbColumn("連番", StdDbType.NUMBER, key.SEQ));

            obj.DataList.Add(new StdDbColumn("ＳＯＡＰ区分", StdDbType.NUMBER, this.Kind));
            obj.DataList.Add(new StdDbColumn("ＳＯＡＰ内容", StdDbType.VARCHAR2, this.Cont));

            int j = 1;

            for (int i = 1; i <= 5; i++)
            {
                if (!this.ImgDict.ContainsKey(i.ToString()) || !this.ImgDict[i.ToString()].ImgTmpExist)
                {
                    continue;
                }

                SoapImg img = this.ImgDict[i.ToString()];

                // サーバーに保存するファイル名
                string fname = key.PtId.PadLeft(9, '0') + key.InOut.ToString() + DateTime.Now.ToString("yyyyMMddHHmmss") + LoginUser.Id.PadLeft(5, '0') + this.Kind + img.Code.PadLeft(3, '0') + ".jpg";

                // サーバーに保存する
                File.Copy(img.ImgTmpPath, LibSettings.Current.SoapImageFolder + "\\" + key.RegDate.ToString().Substring(0, 4) + "\\" + fname, true);

                // サーバーに保存したファイル名でＤＢ保存する
                obj.DataList.Add(new StdDbColumn("図ファイル名" + AppString.HanToZen(j.ToString()), StdDbType.VARCHAR2, fname));

                j++;
            }

            sr = obj.InsertSQL();
#endif
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
