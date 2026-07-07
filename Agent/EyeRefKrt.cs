using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MedicalLibrary.Agent
{
    public class EyeRefKrt
    {
        public string Serial = "";
        public string Date = "";

        public EyeRefKrtElement R = new EyeRefKrtElement("R");
        public EyeRefKrtElement L = new EyeRefKrtElement("L");

        public string PD = "";
        public string VD = "";

        public static bool Read(string file_name, TextBox text_box)
        {
            if (!File.Exists(file_name))
            {
                return false;
            }

            StreamReader reader = new StreamReader(file_name, Encoding.Default);

            string data_str = reader.ReadLine();

            if (data_str.StartsWith("CANON", StringComparison.CurrentCultureIgnoreCase) ||
                data_str.StartsWith("NIDEK", StringComparison.CurrentCultureIgnoreCase))
            {
                text_box.Text = data_str + Environment.NewLine;
                text_box.Text += reader.ReadToEnd();

                reader.Close();
            }
            else
            {
                reader.Close();

                string[] data = data_str.Split(',');

                if (data.Length < 100)
                {
                    return false;
                }

                EyeRefKrt tmpRefKrt = new EyeRefKrt();

                tmpRefKrt.Serial = data[4];
                tmpRefKrt.Date = data[6];

                float f = 0;
                float f1 = 0;
                float f2 = 0;
                string ave = "";

                for (int i = 0; i < 10; i++)
                {
                    EyeRefElement tmpRef = new EyeRefElement();
                    tmpRef.Mode = data[7 + i * 3].Substring(0, 1);
                    tmpRef.Liability = data[7 + i * 3].Substring(1, 2);
                    tmpRef.SPH = data[8 + i * 3].Substring(0, 6);
                    tmpRef.CYL = data[8 + i * 3].Substring(6, 6);
                    tmpRef.AXIS = data[8 + i * 3].Substring(12, 3);
                    tmpRef.SE = data[9 + i * 3];

                    tmpRefKrt.R.RefList.Add(tmpRef);
                }

                tmpRefKrt.R.Ref_SPH = data[37].Substring(0, 6);
                tmpRefKrt.R.Ref_CYL = data[37].Substring(6, 6);
                tmpRefKrt.R.Ref_AXIS = data[37].Substring(12, 3);
                tmpRefKrt.R.Ref_SE = data[38];

                for (int i = 0; i < 10; i++)
                {
                    EyeKrtElement tmpKrt = new EyeKrtElement();
                    tmpKrt.Mode = data[39 + i].Substring(0, 1);
                    tmpKrt.R1 = data[39 + i].Substring(1, 5);
                    tmpKrt.D1 = data[39 + i].Substring(6, 5);
                    tmpKrt.A1 = data[39 + i].Substring(11, 3);
                    tmpKrt.R2 = data[39 + i].Substring(14, 5);
                    tmpKrt.D2 = data[39 + i].Substring(19, 5);
                    tmpKrt.A2 = data[39 + i].Substring(24, 3);
                    tmpKrt.RAVE = data[39 + i].Substring(27, 5);

                    if (float.TryParse(tmpKrt.D1, out f1) && float.TryParse(tmpKrt.D2, out f2))
                    {
                        f = f1 + f2;

                        if (f * 2 - Math.Floor(f * 2) == 0)
                        {
                            ave = Math.Round(f / 2, 2).ToString();
                        }
                        else
                        {
                            ave = Math.Round((f + 0.25) / 2, 2).ToString();
                        }

                        if (ave.Contains("."))
                        {
                            tmpKrt.DAVE = ave.PadRight(5, '0').Substring(0, 5);
                        }
                        else
                        {
                            tmpKrt.DAVE = (ave + ".").PadRight(5, '0').Substring(0, 5);
                        }
                    }

                    tmpKrt.CYL = data[39 + i].Substring(32, 6);
                    tmpKrt.AXIS = data[39 + i].Substring(38, 3);

                    tmpRefKrt.R.KrtList.Add(tmpKrt);
                }

                tmpRefKrt.R.Krt_R1 = data[49].Substring(0, 5);
                tmpRefKrt.R.Krt_D1 = data[49].Substring(5, 5);
                tmpRefKrt.R.Krt_A1 = data[49].Substring(10, 3);
                tmpRefKrt.R.Krt_R2 = data[49].Substring(13, 5);
                tmpRefKrt.R.Krt_D2 = data[49].Substring(18, 5);
                tmpRefKrt.R.Krt_A2 = data[49].Substring(23, 3);
                tmpRefKrt.R.Krt_RAVE = data[49].Substring(26, 5);

                if (float.TryParse(tmpRefKrt.R.Krt_D1, out f1) && float.TryParse(tmpRefKrt.R.Krt_D2, out f2))
                {
                    f = f1 + f2;

                    if (f * 2 - Math.Floor(f * 2) == 0)
                    {
                        ave = Math.Round(f / 2, 2).ToString();
                    }
                    else
                    {
                        ave = Math.Round((f + 0.25) / 2, 2).ToString();
                    }

                    if (ave.Contains("."))
                    {
                        tmpRefKrt.R.Krt_DAVE = ave.PadRight(5, '0').Substring(0, 5);
                    }
                    else
                    {
                        tmpRefKrt.R.Krt_DAVE = (ave + ".").PadRight(5, '0').Substring(0, 5);
                    }
                }

                tmpRefKrt.R.Krt_CYL = data[49].Substring(31, 6);
                tmpRefKrt.R.Krt_AXIS = data[49].Substring(37, 3);
                tmpRefKrt.R.CD = data[50];
                tmpRefKrt.R.SP = data[51];

                for (int i = 0; i < 10; i++)
                {
                    EyeRefElement tmpRef = new EyeRefElement();
                    tmpRef.Mode = data[52 + i * 3].Substring(0, 1);
                    tmpRef.Liability = data[52 + i * 3].Substring(1, 2);
                    tmpRef.SPH = data[53 + i * 3].Substring(0, 6);
                    tmpRef.CYL = data[53 + i * 3].Substring(6, 6);
                    tmpRef.AXIS = data[53 + i * 3].Substring(12, 3);
                    tmpRef.SE = data[54 + i * 3];

                    tmpRefKrt.L.RefList.Add(tmpRef);
                }

                tmpRefKrt.L.Ref_SPH = data[82].Substring(0, 6);
                tmpRefKrt.L.Ref_CYL = data[82].Substring(6, 6);
                tmpRefKrt.L.Ref_AXIS = data[82].Substring(12, 3);
                tmpRefKrt.L.Ref_SE = data[83];

                for (int i = 0; i < 10; i++)
                {
                    EyeKrtElement tmpKrt = new EyeKrtElement();
                    tmpKrt.Mode = data[84 + i].Substring(0, 1);
                    tmpKrt.R1 = data[84 + i].Substring(1, 5);
                    tmpKrt.D1 = data[84 + i].Substring(6, 5);
                    tmpKrt.A1 = data[84 + i].Substring(11, 3);
                    tmpKrt.R2 = data[84 + i].Substring(14, 5);
                    tmpKrt.D2 = data[84 + i].Substring(19, 5);
                    tmpKrt.A2 = data[84 + i].Substring(24, 3);
                    tmpKrt.RAVE = data[84 + i].Substring(27, 5);

                    if (float.TryParse(tmpKrt.D1, out f1) && float.TryParse(tmpKrt.D2, out f2))
                    {
                        f = f1 + f2;

                        if (f * 2 - Math.Floor(f * 2) == 0)
                        {
                            ave = Math.Round(f / 2, 2).ToString();
                        }
                        else
                        {
                            ave = Math.Round((f + 0.25) / 2, 2).ToString();
                        }

                        if (ave.Contains("."))
                        {
                            tmpKrt.DAVE = ave.PadRight(5, '0').Substring(0, 5);
                        }
                        else
                        {
                            tmpKrt.DAVE = (ave + ".").PadRight(5, '0').Substring(0, 5);
                        }
                    }

                    tmpKrt.CYL = data[84 + i].Substring(32, 6);
                    tmpKrt.AXIS = data[84 + i].Substring(38, 3);

                    tmpRefKrt.L.KrtList.Add(tmpKrt);
                }

                tmpRefKrt.L.Krt_R1 = data[94].Substring(0, 5);
                tmpRefKrt.L.Krt_D1 = data[94].Substring(5, 5);
                tmpRefKrt.L.Krt_A1 = data[94].Substring(10, 3);
                tmpRefKrt.L.Krt_R2 = data[94].Substring(13, 5);
                tmpRefKrt.L.Krt_D2 = data[94].Substring(18, 5);
                tmpRefKrt.L.Krt_A2 = data[94].Substring(23, 3);
                tmpRefKrt.L.Krt_RAVE = data[94].Substring(26, 5);

                if (float.TryParse(tmpRefKrt.L.Krt_D1, out f1) && float.TryParse(tmpRefKrt.L.Krt_D2, out f2))
                {
                    f = f1 + f2;

                    if (f * 2 - Math.Floor(f * 2) == 0)
                    {
                        ave = Math.Round(f / 2, 2).ToString();
                    }
                    else
                    {
                        ave = Math.Round((f + 0.25) / 2, 2).ToString();
                    }

                    if (ave.Contains("."))
                    {
                        tmpRefKrt.L.Krt_DAVE = ave.PadRight(5, '0').Substring(0, 5);
                    }
                    else
                    {
                        tmpRefKrt.L.Krt_DAVE = (ave + ".").PadRight(5, '0').Substring(0, 5);
                    }
                }

                tmpRefKrt.L.Krt_CYL = data[94].Substring(31, 6);
                tmpRefKrt.L.Krt_AXIS = data[94].Substring(37, 3);
                tmpRefKrt.L.CD = data[95];
                tmpRefKrt.L.SP = data[96];

                tmpRefKrt.PD = data[97];
                tmpRefKrt.VD = data[98];

                string str = "     VD  : " + tmpRefKrt.VD + "\r\n\r\n";

                if (tmpRefKrt.R.RefString().Length > 0 || tmpRefKrt.L.RefString().Length > 0)
                {
                    str += tmpRefKrt.R.RefString() + "\r\n" + tmpRefKrt.L.RefString() + "\r\n";
                }

                if (tmpRefKrt.R.KrtString().Length > 0 || tmpRefKrt.L.KrtString().Length > 0)
                {
                    str += " KRT. DATA\r\n" + tmpRefKrt.R.KrtString() + "\r\n KRT. DATA\r\n" + tmpRefKrt.L.KrtString() + "\r\n";
                }

                str += "     PD = " + tmpRefKrt.PD + "mm";

                text_box.Text = str;
            }

            return true;
        }
    }

    public class EyeRefKrtElement
    {
        public string Eye = "";

        public List<EyeRefElement> RefList = new List<EyeRefElement>();

        public string Ref_SPH = "";
        public string Ref_CYL = "";
        public string Ref_AXIS = "";
        public string Ref_SE = "";

        public List<EyeKrtElement> KrtList = new List<EyeKrtElement>();

        public string Krt_R1 = "";
        public string Krt_D1 = "";
        public string Krt_A1 = "";
        public string Krt_R2 = "";
        public string Krt_D2 = "";
        public string Krt_A2 = "";
        public string Krt_RAVE = "";
        public string Krt_DAVE = "";
        public string Krt_CYL = "";
        public string Krt_AXIS = "";

        public string CD = "";
        public string SP = "";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="eye">必ず R か L となる。</param>
        public EyeRefKrtElement(string eye)
        {
            if (eye.Equals("R") || eye.Equals("L"))
            {
                Eye = eye;
            }
        }

        public string RefString()
        {
            string str = "";

            for (int i = 0; i < RefList.Count; i++)
            {
                if (RefList[i].ToString().Length > 0)
                {
                    if (str.Length == 0)
                    {
                        str = "<" + Eye + ">    S      C    A\r\n";
                    }

                    str += RefList[i].ToString() + "\r\n";
                }
            }

            if (Ref_SPH.Length > 0 || Ref_CYL.Length > 0 || Ref_AXIS.Length > 0 || Ref_SE.Length > 0)
            {
                str += "\r\n *  " + Ref_SPH + " " + Ref_CYL + " " + Ref_AXIS + "\r\n\r\n  S.E.     " + Ref_SE + "\r\n\r\n";
            }

            return str;
        }

        public string KrtString()
        {
            string str = "";

            if (Krt_R1.Length > 0 || Krt_D1.Length > 0 || Krt_A1.Length > 0 || Krt_R2.Length > 0 || Krt_D2.Length > 0 || Krt_A2.Length > 0 || Krt_RAVE.Length > 0 || Krt_DAVE.Length > 0 || Krt_CYL.Length > 0 || Krt_AXIS.Length > 0)
            {
                str = "<" + Eye + ">   D     MM   A\r\n H  " + Krt_D1 + " " + Krt_R1 + " " + Krt_A1 + "\r\n V  " + Krt_D2 + " " + Krt_R2 + " " + Krt_A2 + "\r\n\r\nAVE " + Krt_DAVE + " " + Krt_RAVE + "\r\n     CYL " + Krt_CYL + " " + Krt_AXIS + "\r\n\r\n  ----------------\r\n";
            }

            for (int i = 0; i < KrtList.Count; i++)
            {
                if (KrtList[i].ToString().Length > 0)
                {
                    str += "-" + (i + 1) + "-   D     MM   A\r\n" + KrtList[i].ToString() + "\r\n";
                }
            }

            return str;
        }
    }

    public class EyeRefElement
    {
        public string Mode = "";
        public string Liability = "";
        public string SPH = "";
        public string CYL = "";
        public string AXIS = "";
        public string SE = "";

        public override string ToString()
        {
            string str = "";

            if (!Mode.Equals(" ") || !Liability.Equals("  ") || !SPH.Equals("      ") || !CYL.Equals("      ") || !AXIS.Equals("   "))
            {
                if (SPH.StartsWith("@11"))
                {
                    str = Mode + Liability + " OVER-SPH";
                }
                else if (SPH.StartsWith("@12"))
                {
                    str = Mode + Liability + " OVER-CYL";
                }
                else if (SPH.StartsWith("@13"))
                {
                    str = Mode + Liability + " NO TARGET";
                }
                else if (SPH.StartsWith("@14"))
                {
                    str = Mode + Liability + " AGAIN";
                }
                else if (SPH.StartsWith("@15"))
                {
                    str = Mode + Liability + " NO CENTER";
                }
                else if (SPH.StartsWith("@16"))
                {
                    str = Mode + Liability + " ERROR";
                }
                else if (SPH.StartsWith("@19"))
                {
                    str = Mode + Liability + " ERROR";
                }
                else if (SPH.StartsWith("@99"))
                {
                    str = Mode + Liability + " ERROR";
                }
                else
                {
                    str = Mode + Liability + " " + SPH + " " + CYL + " " + AXIS;
                }
            }

            return str;
        }
    }

    public class EyeKrtElement
    {
        public string Mode = "";
        public string R1 = "";
        public string D1 = "";
        public string A1 = "";
        public string R2 = "";
        public string D2 = "";
        public string A2 = "";
        public string RAVE = "";
        public string DAVE = "";
        public string CYL = "";
        public string AXIS = "";

        public override string ToString()
        {
            string str = "";

            if (!R1.Equals("     ") || !D1.Equals("     ") || !A1.Equals("   ") || !R2.Equals("     ") || !D2.Equals("     ") || !A2.Equals("   ") || !RAVE.Equals("     ") || !CYL.Equals("      ") || !AXIS.Equals("   "))
            {
                if (R1.StartsWith("@12"))
                {
                    str = "    OVER-CYL";
                }
                else if (R1.StartsWith("@13"))
                {
                    str = "    NO TARGET";
                }
                else if (R1.StartsWith("@14"))
                {
                    str = "    AGAIN";
                }
                else if (R1.StartsWith("@15"))
                {
                    str = "    NO CENTER";
                }
                else if (R1.StartsWith("@16"))
                {
                    str = "    ERROR";
                }
                else if (R1.StartsWith("@20"))
                {
                    str = "    OVER-R";
                }
                else if (R1.StartsWith("@99"))
                {
                    str = "    ERROR";
                }
                else
                {
                    str = " H  " + D1 + " " + R1 + " " + A1 + "\r\n V  " + D2 + " " + R2 + " " + A2 + "\r\nAVE " + DAVE + " " + RAVE + "\r\n     CYL " + CYL + " " + AXIS;
                }
            }

            return str;
        }
    }
}
