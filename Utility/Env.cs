using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalLibrary.Utility
{
    /// <summary>
    /// システム環境を表すクラス
    /// </summary>
    public static class Env
    {
        public static string LEGACY_HOME { get; } = @"C:\macs";
        public static string AGENT_HOME { get; } = @"C:\macs\utility";
        public static string KARTE_HOME { get; set; } = @"c:\karte";
        public static string INNO_HOME { get; set; } = @"c:\innokarte";
        public static string SHIN_HOME { get; set; } = @"c:\shinseikai";
    }
}
