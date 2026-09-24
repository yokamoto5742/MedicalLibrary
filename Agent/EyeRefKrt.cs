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

            string data_str;

            using (StreamReader reader = new StreamReader(file_name, Encoding.Default))
            {
                data_str = reader.ReadLine();

                // 空ファイル
                if (data_str == null)
                {
                    return false;
                }

                if (data_str.StartsWith("CANON", StringComparison.CurrentCultureIgnoreCase) ||
                    data_str.StartsWith("NIDEK", StringComparison.CurrentCultureIgnoreCase))
                {
                    text_box.Text = data_str + Environment.NewLine;
                    text_box.Text += reader.ReadToEnd();

                    return true;
                }
            }

            string[] data = data_str.Split(',');

            if (data.Length < 100)
            {
                return false;
            }

            EyeRefKrt tmpRefKrt = new EyeRefKrt();

            tmpRefKrt.Serial = data[4];
            tmpRefKrt.Date = data[6];

            // 左眼の項目は右眼の 45 列後ろにある
            ReadEye(data, 0, tmpRefKrt.R);
            ReadEye(data, 45, tmpRefKrt.L);

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

            return true;
        }

        /// <summary>
        /// 片眼分のレフ・ケラトのデータを読み取る。
        /// </summary>
        /// <param name="data">CSV の列</param>
        /// <param name="offset">右眼は 0、左眼は 45</param>
        /// <param name="e"></param>
        static void ReadEye(string[] data, int offset, EyeRefKrtElement e)
        {
            for (int i = 0; i < 10; i++)
            {
                EyeRefElement tmpRef = new EyeRefElement();
                tmpRef.Mode = data[offset + 7 + i * 3].Substring(0, 1);
                tmpRef.Liability = data[offset + 7 + i * 3].Substring(1, 2);
                tmpRef.SPH = data[offset + 8 + i * 3].Substring(0, 6);
                tmpRef.CYL = data[offset + 8 + i * 3].Substring(6, 6);
                tmpRef.AXIS = data[offset + 8 + i * 3].Substring(12, 3);
                tmpRef.SE = data[offset + 9 + i * 3];

                e.RefList.Add(tmpRef);
            }

            string ref_avg = data[offset + 37];

            e.Ref_SPH = ref_avg.Substring(0, 6);
            e.Ref_CYL = ref_avg.Substring(6, 6);
            e.Ref_AXIS = ref_avg.Substring(12, 3);
            e.Ref_SE = data[offset + 38];

            for (int i = 0; i < 10; i++)
            {
                string krt = data[offset + 39 + i];

                EyeKrtElement tmpKrt = new EyeKrtElement();
                tmpKrt.Mode = krt.Substring(0, 1);
                tmpKrt.R1 = krt.Substring(1, 5);
                tmpKrt.D1 = krt.Substring(6, 5);
                tmpKrt.A1 = krt.Substring(11, 3);
                tmpKrt.R2 = krt.Substring(14, 5);
                tmpKrt.D2 = krt.Substring(19, 5);
                tmpKrt.A2 = krt.Substring(24, 3);
                tmpKrt.RAVE = krt.Substring(27, 5);
                tmpKrt.DAVE = CalcDAve(tmpKrt.D1, tmpKrt.D2);
                tmpKrt.CYL = krt.Substring(32, 6);
                tmpKrt.AXIS = krt.Substring(38, 3);

                e.KrtList.Add(tmpKrt);
            }

            string krt_avg = data[offset + 49];

            e.Krt_R1 = krt_avg.Substring(0, 5);
            e.Krt_D1 = krt_avg.Substring(5, 5);
            e.Krt_A1 = krt_avg.Substring(10, 3);
            e.Krt_R2 = krt_avg.Substring(13, 5);
            e.Krt_D2 = krt_avg.Substring(18, 5);
            e.Krt_A2 = krt_avg.Substring(23, 3);
            e.Krt_RAVE = krt_avg.Substring(26, 5);
            e.Krt_DAVE = CalcDAve(e.Krt_D1, e.Krt_D2);
            e.Krt_CYL = krt_avg.Substring(31, 6);
            e.Krt_AXIS = krt_avg.Substring(37, 3);
            e.CD = data[offset + 50];
            e.SP = data[offset + 51];
        }

        /// <summary>
        /// 角膜屈折力 D1・D2 の平均を5文字で返す（0.25 刻みに切り上げる）。
        /// 数値でなければ空文字。
        /// </summary>
        static string CalcDAve(string d1, string d2)
        {
            float f1 = 0;
            float f2 = 0;

            if (!float.TryParse(d1, out f1) || !float.TryParse(d2, out f2))
            {
                return "";
            }

            float f = f1 + f2;
            string ave = "";

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
                return ave.PadRight(5, '0').Substring(0, 5);
            }
            else
            {
                return (ave + ".").PadRight(5, '0').Substring(0, 5);
            }
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
