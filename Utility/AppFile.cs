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
    }
}
