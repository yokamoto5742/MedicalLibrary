using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalLibrary.Agent
{
    public class EyeLensMeter
    {
        public string SPH_R = "";
        public string SPH_L = "";

        public string CYL_R = "";
        public string CYL_L = "";

        public string AXIS_R = "";
        public string AXIS_L = "";

        public string ADD1_R = "";
        public string ADD1_L = "";

        public string ADD2_R = "";
        public string ADD2_L = "";

        public string PRISM_R = "";
        public string PRISM_L = "";

        public string BASE_R = "";
        public string BASE_L = "";

        public void ReadData(string file)
        {
            if (!System.IO.File.Exists(file))
            {
                return;
            }

            System.IO.StreamReader reader = new System.IO.StreamReader(file, Encoding.Default);

            string tmp_line = "";
            string line = "";

            while ((tmp_line = reader.ReadLine()) != null)
            {
                line = tmp_line;
            }

            reader.Close();

            string[] s = line.Split(',');

            // ファイルが空、または列が足りない場合は何もしない
            if (s.Length >= 20)
            {
                if (!s[1].Contains("---"))
                {
                    SPH_R = s[1];
                }

                if (!s[2].Contains("---"))
                {
                    CYL_R = s[2];
                }

                if (!s[3].Contains("---"))
                {
                    AXIS_R = s[3];
                }

                if (!s[5].Contains("---"))
                {
                    SPH_L = s[5];
                }

                if (!s[6].Contains("---"))
                {
                    CYL_L = s[6];
                }

                if (!s[7].Contains("---"))
                {
                    AXIS_L = s[7];
                }

                if (!s[9].Contains("---"))
                {
                    ADD1_R = s[9];
                }

                if (!s[10].Contains("---"))
                {
                    ADD2_R = s[10];
                }

                if (!s[12].Contains("---"))
                {
                    ADD1_L = s[12];
                }

                if (!s[13].Contains("---"))
                {
                    ADD2_L = s[13];
                }

                if (!s[15].Contains("---") && !s[16].Equals("---"))
                {
                    PRISM_R = s[15].Substring(0, 5);
                    BASE_R = s[15].Substring(5, 1) + s[16].Substring(0, 2);
                }

                if (!s[18].Contains("---") && !s[19].Equals("---"))
                {
                    PRISM_L = s[18].Substring(0, 5);
                    BASE_L = s[18].Substring(5, 1) + s[19].Substring(0, 2);
                }
            }
        }
    }
}
