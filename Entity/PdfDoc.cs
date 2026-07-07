using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class PdfDoc : StdEntity
    {
        public string PtId = "";

        public string YearFolder = "";

        public string DocFolder = @"";

        public string PdfFile = "";

        public string PdfCode = "";

        public string PdfName = "";

        public string DeptCode
        {
            get
            {
                string s = "";

                // PDFファイル名が36文字以上の場合
                if (this.PdfFile.Length >= 36)
                {
                    s = this.PdfFile.Substring(14, 3).TrimStart('0');
                }

                return s;
            }
        }

        public string DeptName
        {
            get
            {
                string s = "";

                if (Dict.DeptDict.ContainsKey(DeptCode))
                {
                    s = Dict.DeptDict[DeptCode].FullName;
                }

                return s;
            }
        }

        public string StaffCode
        {
            get
            {
                string s = "";

                // PDFファイル名が36文字以上の場合
                if (this.PdfFile.Length >= 36)
                {
                    s = this.PdfFile.Substring(17, 5).TrimStart('0');
                }

                return s;
            }
        }

        public string StaffName
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(StaffCode))
                {
                    s = Dict.StaffDict[StaffCode].Name;
                }

                return s;
            }
        }

        public int PdfDate
        {
            get
            {
                int i = 0;

                // PDFファイル名が36文字以上の場合
                if (this.PdfFile.Length >= 36)
                {
                    int.TryParse(this.PdfFile.Substring(22, 8), out i);
                }

                return i;
            }
        }

        public int PdfTime
        {
            get
            {
                int i = 0;

                // PDFファイル名が36文字以上の場合
                if (this.PdfFile.Length >= 36)
                {
                    int.TryParse(this.PdfFile.Substring(30, 6), out i);
                }

                return i;
            }
        }
/*
        public int RegDate = 0;

        public int RegTime = 0;

        public int UpDate = 0;

        public int UpTime = 0;
*/
        public string Key
        {
            get
            {
                string s = "";

                s += YearFolder + "\\" + PtId.PadLeft(9, '0') + "\\" + DocFolder + "\\" + PdfFile;

                return s;
            }
        }

        public string PdfFilePath
        {
            get
            {
                return LibSettings.Current.PdfServerFolder + "\\" + this.Key;
            }
        }

        public bool PdfFileExist
        {
            get
            {
                return File.Exists(this.PdfFilePath);
            }
        }


        public static Dictionary<string, Dictionary<string, PdfDoc>> GetDict(string pt_id, List<string> date_list)
        {
            Dictionary<string, Dictionary<string, PdfDoc>> dict = new Dictionary<string, Dictionary<string, PdfDoc>>();

            if (pt_id.Length == 0)
            {
                return dict;
            }

            if (AppString.ConcatList(date_list, ",").Length == 0)
            {
                return dict;
            }

#if INNO
            string cmd = "select t.P_ID, t.FIGURE_1, t.FIGURE_2, t.FIGURE_3, t.FIGURE_4, t.SOAP_TEXT " +
                " from D_SOAP_DETAIL t " +
                " where t.P_ID = " + pt_id +
                " and t.SOAP_TYPE = 90 " +
                " and t.FIGURE_5 = 1 " +
                " and (t.DEL_FLG is null or t.DEL_FLG = 0) " +
                " and t.SOAP_DATE in (" + AppString.ConcatList(date_list, ",") + ")";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PdfDoc obj = new PdfDoc();

                obj.PtId = pt_id;
                obj.YearFolder = tmp.GetDataString("FIGURE_1");
                obj.DocFolder = tmp.GetDataString("FIGURE_2");
                obj.PdfFile = tmp.GetDataString("FIGURE_3");
                obj.PdfCode = tmp.GetDataString("FIGURE_4");
                obj.PdfName = tmp.GetDataString("SOAP_TEXT");

                if (dict.ContainsKey(obj.PdfDate.ToString()))
                {
                    dict[obj.PdfDate.ToString()].Add(obj.Key, obj);
                }
                else
                {
                    Dictionary<string, PdfDoc> pdf_dict = new Dictionary<string, PdfDoc>();
                    pdf_dict.Add(obj.Key, obj);
                    dict.Add(obj.PdfDate.ToString(), pdf_dict);
                }
            }
#else
            string cmd = "select t.患者コード, t.年次フォルダ, t.書類フォルダ, t.ＰＤＦファイル名, t.書類コード " +
                ", (select tm.書類名 from ＰＤＦ書類マスター tm where tm.書類コード = t.書類コード) 書類名 " +
//                ", t.科コード, t.入力者コード, t.ＰＤＦ登録日, t.ＰＤＦ登録時刻 " +
                " from ＰＤＦ登録データ t " +
                " where t.患者コード = " + pt_id +
                " and t.書類コード in (select tm.書類コード from ＰＤＦ書類マスター tm where tm.表示対象フラグ = 1) " +
                " and t.削除区分 = 0 " +
                " and t.ＰＤＦ登録日 in (" + AppString.ConcatList(date_list, ",") + ")";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                PdfDoc obj = new PdfDoc();

                obj.PtId = pt_id;
                obj.YearFolder = tmp.GetDataString("年次フォルダ");
                obj.DocFolder = tmp.GetDataString("書類フォルダ");
                obj.PdfFile = tmp.GetDataString("ＰＤＦファイル名");
                obj.PdfCode = tmp.GetDataString("書類コード");
                obj.PdfName = tmp.GetDataString("書類名");
//                obj.DeptCode = tmp.GetDataString("科コード");
//                obj.StaffCode = tmp.GetDataString("入力者コード");
//                obj.PdfDate = tmp.GetDataInt("ＰＤＦ登録日");
//                obj.PdfTime = tmp.GetDataInt("ＰＤＦ登録時刻");

                if (dict.ContainsKey(obj.PdfDate.ToString()))
                {
                    dict[obj.PdfDate.ToString()].Add(obj.Key, obj);
                }
                else
                {
                    Dictionary<string, PdfDoc> pdf_dict = new Dictionary<string, PdfDoc>();
                    pdf_dict.Add(obj.Key, obj);
                    dict.Add(obj.PdfDate.ToString(), pdf_dict);
                }
            }
#endif

            return dict;
        }

        /// <summary>
        /// PDFファイルのフルパスを返す
        /// </summary>
        /// <param name="file">PDFファイル名</param>
        /// <param name="folder">フォルダ名（不明な場合は省略可）</param>
        /// <returns></returns>
        public static string GetFullPath(string file, string folder = "")
        {
            string s = "";

            if (!Regex.IsMatch(file, "[0-9]{36}.pdf"))
            {
                return s;
            }

            if (folder.Length > 0)
            {
                // 年次フォルダ
                if (Directory.Exists(LibSettings.Current.PdfServerFolder + "\\" + file.Substring(22, 4) + "\\" + file.Substring(0, 9) + "\\" + folder))
                {
                    foreach (string f in Directory.GetFiles(LibSettings.Current.PdfServerFolder + "\\" + file.Substring(22, 4) + "\\" + file.Substring(0, 9) + "\\" + folder))
                    {
                        if (file.Equals(Path.GetFileName(f)))
                        {
                            s = f;
                            break;
                        }
                    }
                }

                if (s.Length == 0)
                {
                    // 年次フォルダになければ InKarteフォルダも調べる
                    if (Directory.Exists(LibSettings.Current.PdfServerFolder + "\\InKarte\\" + file.Substring(0, 9) + "\\" + folder))
                    {
                        foreach (string f in Directory.GetFiles(LibSettings.Current.PdfServerFolder + "\\InKarte\\" + file.Substring(0, 9) + "\\" + folder))
                        {
                            if (file.Equals(Path.GetFileName(f)))
                            {
                                s = f;
                                break;
                            }
                        }
                    }
                }
            }

            if (s.Length == 0)
            {
                // ここまでで存在しなければ、同一年次・同一患者の別フォルダも調べる
                if (Directory.Exists(LibSettings.Current.PdfServerFolder + "\\" + file.Substring(22, 4) + "\\" + file.Substring(0, 9)))
                {
                    foreach (string dir in Directory.GetDirectories(LibSettings.Current.PdfServerFolder + "\\" + file.Substring(22, 4) + "\\" + file.Substring(0, 9)))
                    {
                        foreach (string dir2 in Directory.GetDirectories(dir))
                        {
                            foreach (string f in Directory.GetFiles(dir2))
                            {
                                if (file.Equals(Path.GetFileName(f)))
                                {
                                    s = f;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            if (s.Length == 0)
            {
                // ここまでで存在しなければ、InKarte の同一患者の別フォルダも調べる
                if (Directory.Exists(LibSettings.Current.PdfServerFolder + "\\InKarte\\" + file.Substring(0, 9)))
                {
                    foreach (string dir in Directory.GetDirectories(LibSettings.Current.PdfServerFolder + "\\InKarte\\" + file.Substring(0, 9)))
                    {
                        foreach (string dir2 in Directory.GetDirectories(dir))
                        {
                            foreach (string f in Directory.GetFiles(dir2))
                            {
                                if (file.Equals(Path.GetFileName(f)))
                                {
                                    s = f;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return s;
        }

        /// <summary>
        /// PDFファイルが存在すれば削除する
        /// </summary>
        /// <param name="file">PDFファイル名</param>
        /// <param name="folder">フォルダ名（不明な場合は省略可）</param>
        /// <returns></returns>
        public static bool Delete(string file, string folder = "")
        {
            bool b = false;

            try
            {
                if (PdfDoc.GetFullPath(file, folder).Length > 0)
                {
                    string f = Path.GetFileNameWithoutExtension(file) + "_" + (LoginUser.Id.Length > 0 ? LoginUser.Id : "0") + "_D.pdf";

                    FileStream fs = File.Create(f);
                    fs.Close();

                    File.Move(f, LibSettings.Current.PdfSendFolder + "\\" + f);
                    b = true;
                }
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex, false);
                b = false;
            }

            return b;
        }
    }
}
