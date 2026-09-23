using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace MedicalLibrary.Utility
{
    public class Launcher
    {
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
