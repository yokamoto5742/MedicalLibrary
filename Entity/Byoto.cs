using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedicalLibrary.Entity
{
    public class Byoto
    {
        public string Code = "";

        public string Name = "";

        public Byoto(string code, string name)
        {
            this.Code = code;
            this.Name = name;
        }

        static Dictionary<string, Byoto> dict = new Dictionary<string, Byoto>();

        public static Dictionary<string, Byoto> Dict
        {
            get
            {
                if (dict.Count == 0)
                {
                    dict.Add("03", new Byoto("03", "わかば"));
                    dict.Add("04", new Byoto("04", "さくら"));
                    dict.Add("05", new Byoto("05", "あやめ"));
                }

                return dict;
            }
        }
    }
}
