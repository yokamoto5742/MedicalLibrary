using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using MedicalLibrary.Entity;

namespace MedicalLibrary.Utility
{
    public class Launcher
    {
        public static Process Browser(string url)
        {
            return Process.Start(@"C:\Program Files\Internet Explorer\iexplore.exe", url);
        }

        public static Process Dicom(string pt_id, DicomKind kind)
        {
            if (kind == DicomKind.PRO)
            {
                return Launcher.Browser("\"http://dicomweb/EXtViewer_pro/cgi/login.cgi?USER=test&PASSWORD=test&ID=" + pt_id + "&DATE=\"\"\"\"\"");
            }
            else if (kind == DicomKind.EV)
            {
                return Launcher.Browser("\"http://dicomweb/EVService/EVService.dll?clientCall&USER=shinseikai&ID=" + pt_id);
            }
            else
            {
                return null;
            }
        }

        public static Process Dicom(string pt_id)
        {
            return Dicom(pt_id, DicomKind.EV);
        }

        public enum DicomKind : int
        {
            PRO = 1,
            EV = 2
        }

        /// <summary>
        /// 画像ファイルを表示する
        /// </summary>
        /// <param name="img_path"></param>
        public static Process ImageViewer(string img_path)
        {
            return Process.Start("rundll32.exe", "shimgvw.dll,ImageView_Fullscreen " + img_path);
        }

        /// <summary>
        /// 院内開発プログラムの Process を起動する
        /// </summary>
        /// <param name="exe_file">exeファイル</param>
        /// <param name="argument">パラメータ</param>
        /// <returns></returns>
        public static Process Start(string exe_file, string arguments = "")
        {
            string exe = AppFile.FilePath(exe_file);

            if (File.Exists(exe))
            {
                return Process.Start(exe, arguments);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// MedicalAgent を起動する
        /// </summary>
        /// <param name="argument">-list1: 外来患者一覧, -list2: 入院患者一覧, -anes: 麻酔</param>
        /// <returns></returns>
        public static Process MedicalAgent(string argument = "")
        {
            string exe = AppFile.FilePath("MedicalAgent.exe");
            string args = "";

            if (LoginUser.Id.Length > 0)
            {
                args += " -U " + LoginUser.Id;
            }

            if (argument.Length > 0)
            {
                args += " " + argument;
            }

            if (File.Exists(exe))
            {
                return Process.Start(exe, args);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 眼科システムを起動する
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="argument">-l: 患者一覧, -r: 予約一覧</param>
        /// <returns></returns>
        public static Process EyeCenter(string pt_id = "", string argument = "")
        {
            string exe = AppFile.FilePath("EyeCenter.exe");
            string args = "";

            if (LoginUser.Id.Length > 0)
            {
                args += " -U " + LoginUser.Id;
            }

            if (pt_id.Length > 0)
            {
                args += " -P " + pt_id;
            }

            if (argument.Length > 0)
            {
                args += " " + argument;
            }

            if (File.Exists(exe))
            {
                return Process.Start(exe, args);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 患者ラベル印刷を起動する
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="date">施行予定日（デフォルトは当日）</param>
        /// <returns></returns>
        public static Process PatLabel(string pt_id = "", string date = "")
        {
            string exe = AppFile.FilePath("PatLabel.exe");
            string args = "";

            if (pt_id.Length > 0)
            {
                args += " -P " + pt_id;
            }

            if (DateTimeAgent.IsDate(date))
            {
                args += " -D " + date;
            }

            if (File.Exists(exe))
            {
                return Process.Start(exe, args);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 患者ラベル印刷を起動する
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="date">施行予定日（デフォルトは当日）</param>
        /// <param name="print_order">オーダーを印刷するか</param>
        /// <returns></returns>
        public static Process PatLabelLight(string pt_id = "", string date = "", bool print_order = true)
        {
            string exe = AppFile.FilePath("PatLabelLight.exe");
            string args = "";

            if (pt_id.Length > 0)
            {
                args += " -P " + pt_id;
            }

            if (DateTimeAgent.IsDate(date))
            {
                args += " -D " + date;
            }

            if (!print_order)
            {
                args += " -N";
            }

            if (File.Exists(exe))
            {
                return Process.Start(exe, args);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// PDFファイルを印刷する
        /// </summary>
        /// <param name="file"></param>
        /// <param name="printer"></param>
        /// <returns></returns>
        public static Process PDFPrint(string file, string printer = "")
        {
            if (file.Length == 0 || !File.Exists(file))
            {
                return null;
            }

            string path = Environment.GetEnvironmentVariable("path");
            string args = "";

            // まずは LibSettings の PdfPrinterExe を探す
            string exe = LibSettings.Current.PC.PdfPrinterExe;

            if (!File.Exists(exe))
            {
                // なければ PDF X-Change Editor を探す
                exe = @"c:\Program Files\Tracker Software\PDF Editor\PDFXEdit.exe";
            }

            if (!File.Exists(exe))
            {
                // なければ PATH 環境変数から PDF X-Change Editor を探す
                foreach (string p in path.Split(';'))
                {
                    if (File.Exists(p.TrimEnd('\\') + @"\PDFXEdit.exe"))
                    {
                        exe = p.TrimEnd('\\') + @"\PDFXEdit.exe";
                        break;
                    }
                }
            }

            if (!File.Exists(exe))
            {
                // なければ PDF X-Change Viewer を探す
                exe = @"c:\Program Files\Tracker Software\PDF Viewer\PDFXCView.exe";
            }

            if (!File.Exists(exe))
            {
                // なければ PATH 環境変数から PDF X-Change Viewer を探す
                foreach (string p in path.Split(';'))
                {
                    if (File.Exists(p.TrimEnd('\\') + @"\PDFXCView.exe"))
                    {
                        exe = p.TrimEnd('\\') + @"\PDFXCView.exe";
                        break;
                    }
                }
            }

            if (!File.Exists(exe))
            {
                // 最終的になければ AcroRd32.exe
                exe = "AcroRd32.exe";
            }

            if (Path.GetFileName(exe).Equals("PDFXEdit.exe", StringComparison.CurrentCultureIgnoreCase) ||
                Path.GetFileName(exe).Equals("PDFXCView.exe", StringComparison.CurrentCultureIgnoreCase))
            {
                // PDF X-Change Editor, Viewer いずれかの場合
                args += " /print";

                if (printer.Length > 0)
                {
                    args += ":printer=\"" + printer + "\"";
                }

                args += " " + file;
            }
            else if (Path.GetFileName(exe).ToLower().Contains("acrord"))
            {
                // Acrobat Reader の場合
                args += " /t " + file;

                if (printer.Length > 0)
                {
                    args += " \"" + printer + "\"";
                }
            }
            else
            {
                // 上記いずれでもない場合の args は不定
            }

            return Process.Start(exe, args);
        }

        /// <summary>
        /// PdfViewerを起動する
        /// </summary>
        /// <returns></returns>
        public static Process PdfViewer()
        {
            string exe = AppFile.FilePath(@"c:\shinseikai\PdfView\PdfView.exe");
            string args = "";

            if (File.Exists(exe))
            {
                return Process.Start(exe, args);
            }
            else
            {
                return null;
            }
        }
    }
}
