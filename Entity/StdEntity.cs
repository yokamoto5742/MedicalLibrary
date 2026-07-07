using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class StdEntity
    {
#if INNO
        public static DB Db = DB.Db3;
#else
        public static DB Db = DB.Db1;
#endif
    }
}
