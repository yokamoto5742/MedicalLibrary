using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MedicalLibrary.Utility
{
    public class AppFile
    {
        /// <summary>
        /// カレント, KARTE_HOME, AGENT_HOME, LEGACY_HOME, INNO_HOME の順にファイルを探す
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static string FilePath(string f)
        {
            string s = "";
            if (File.Exists(f))
            {
                s = Path.GetFullPath(f);
            }
            else if (File.Exists(Directory.GetCurrentDirectory() + "\\" + f))
            {
                s = Directory.GetCurrentDirectory() + "\\" + f;
            }
            else if (File.Exists(Env.SHIN_HOME + "\\" + f))
            {
                s = Env.SHIN_HOME + "\\" + f;
            }
            else if (File.Exists(Env.KARTE_HOME + "\\" + f))
            {
                s = Env.KARTE_HOME + "\\" + f;
            }
            else if (File.Exists(Env.AGENT_HOME + "\\" + f))
            {
                s = Env.AGENT_HOME + "\\" + f;
            }
            else if (File.Exists(Env.INNO_HOME + "\\" + f))
            {
                s = Env.INNO_HOME + "\\" + f;
            }
            else if (File.Exists(Env.LEGACY_HOME + "\\" + f))
            {
                s = Env.LEGACY_HOME + "\\" + f;
            }
            return s;
        }
/*
        /// <summary>
        /// ファイルのフルパスからファイル名だけを取り出す
        /// </summary>
        /// <param name="path">ファイルのフルパス</param>
        /// <returns></returns>
        public static string FileName(string path)
        {
            string s = "";

            if (path.Contains("\\"))
            {
                string[] ss = path.Split('\\');
                s = ss[ss.Length - 1];
            }

            return s;
        }
*/
        /// <summary>
        /// ファイルのフルパスからパス名だけを取り出す
        /// </summary>
        /// <param name="path">ファイルのフルパス</param>
        /// <returns></returns>
        public static string PathName(string path)
        {
            string s = "";

            FileInfo f = new FileInfo(path);

            if (f.Attributes == FileAttributes.Directory)
            {
                // ディレクトリの場合はそのまま
                s = path;
            }
            else
            {
                // ファイルの場合は親ディレクトリ
                s = f.DirectoryName;
            }

            return s;
        }
/*
        /// <summary>
        /// ファイル名から拡張子だけを取り出す（. あり）
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string Extension(string file)
        {
            string s = "";

            FileInfo f = new FileInfo(file);
            s = f.Extension;

            s = Path.GetExtension(file);

            return s;
        }
 */
    }
}
