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
        static string legacy_home = "";
        static string agent_home = "";
        static string karte_home = "";

        static string inno_home = "";
        static string shin_home = "";

        static string db_link = "";

        public static string LEGACY_HOME
        {
            get
            {
                if (legacy_home == null || legacy_home.Length == 0)
                {
                    init();
                }

                return legacy_home;
            }
        }

        public static string AGENT_HOME
        {
            get
            {
                if (agent_home == null || agent_home.Length == 0)
                {
                    init();
                }

                return agent_home;
            }
        }

        public static string KARTE_HOME
        {
            get
            {
                if (karte_home == null || karte_home.Length == 0)
                {
                    init();
                }

                return karte_home;
            }
            set
            {
                karte_home = value;
            }
        }

        public static string INNO_HOME
        {
            get
            {
                if (inno_home == null || inno_home.Length == 0)
                {
                    init();
                }

                return inno_home;
            }
            set
            {
                inno_home = value;
            }
        }

        public static string SHIN_HOME
        {
            get
            {
                if (shin_home == null || shin_home.Length == 0)
                {
                    init();
                }

                return shin_home;
            }
            set
            {
                shin_home = value;
            }
        }

        public static string DB_LINK
        {
            get
            {
                if (db_link == null || db_link.Length == 0)
                {
                    init();
                }

                return db_link;
            }
            set
            {
                // アプリによっては初期設定時に変更する場合がある
                db_link = value;
            }
        }

        private static void init()
        {
            legacy_home = @"C:\macs";
            agent_home = @"C:\macs\utility";
            karte_home = @"c:\karte";
            inno_home = @"c:\innokarte";
            shin_home = @"c:\shinseikai";

#if INNO
            db_link = "@INNO.WORLD";
#else
            db_link = "@IJI.WORLD";
#endif
        }
    }
}
