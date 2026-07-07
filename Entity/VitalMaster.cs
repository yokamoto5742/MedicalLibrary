using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// バイタルマスター
    /// AMC_バイタル画面CTL
    /// </summary>
    public class VitalMaster
    {
        /// <summary>
        /// データ区分
        /// </summary>
        public string Code = "";

        /// <summary>
        /// 測定名称
        /// </summary>
        public string Name = "";

        /// <summary>
        /// ボタン表示順
        /// </summary>
        public int ButtonNum = 0;

        /// <summary>
        /// 測定値数
        /// </summary>
        public int Count = 1;

        /// <summary>
        /// 項目名１
        /// </summary>
        public string Name1 = "";

        /// <summary>
        /// 単位１
        /// </summary>
        public string Unit1 = "";

        /// <summary>
        /// 項目名２
        /// </summary>
        public string Name2 = "";

        /// <summary>
        /// 単位２
        /// </summary>
        public string Unit2 = "";

        /// <summary>
        /// バイタルチャート表示順
        /// </summary>
        public int ChartNum = 0;


        /// <summary>
        /// 表示順（AMC_バイタル画面CTL には無い）
        /// </summary>
        public int Num = 0;

        /// <summary>
        /// 表示色（AMC_バイタル画面CTL には無い）
        /// </summary>
        public Brush ChartBrush = Brushes.Black;


        /// <summary>
        /// 測定値１のデータ型
        /// </summary>
        public DataType DataType1 = DataType.None;

        /// <summary>
        /// 測定値１の下限
        /// </summary>
        public float Limit11 = 0.0F;

        /// <summary>
        /// 測定値１の上限
        /// </summary>
        public float Limit12 = 0.0F;


        /// <summary>
        /// 測定値２のデータ型
        /// </summary>
        public DataType DataType2 = DataType.None;

        /// <summary>
        /// 測定値２の下限
        /// </summary>
        public float Limit21 = 0.0F;

        /// <summary>
        /// 測定値２の上限
        /// </summary>
        public float Limit22 = 0.0F;


        static Dictionary<string, VitalMaster> dict = new Dictionary<string, VitalMaster>();

        public static Dictionary<string, VitalMaster> Dict
        {
            get
            {
                if (dict.Count == 0)
                {
                    int c = 1;

                    VitalMaster m = new VitalMaster();
                    m.Code = "1";
                    m.Name = "体温(T)";
                    m.Name1 = "体温(T)";
                    m.Unit1 = "℃";
                    m.DataType1 = DataType.Float;
                    m.Limit11 = 10;
                    m.Limit12 = 50;
                    m.ButtonNum = 1;
                    m.ChartNum = 1;
                    m.Num = c++;
                    m.ChartBrush = Brushes.Blue;
                    dict.Add(m.Code, m);

                    m = new VitalMaster();
                    m.Code = "3";
                    m.Name = "脈拍(P)";
                    m.Name1 = "脈拍(P)";
                    m.Unit1 = "回/分";
                    m.DataType1 = DataType.Int;
                    m.Limit11 = 1;
                    m.Limit12 = 300;
                    m.ButtonNum = 2;
                    m.ChartNum = 2;
                    m.Num = c++;
                    m.ChartBrush = Brushes.Red;
                    dict.Add(m.Code, m);

                    m = new VitalMaster();
                    m.Code = "2";
                    m.Name = "血圧(BP)";
                    m.Name1 = "高";
                    m.Unit1 = "mmHg";
                    m.DataType1 = DataType.Int;
                    m.Limit11 = 0;
                    m.Limit12 = 300;
                    m.Name2 = "低";
                    m.Unit2 = "mmHg";
                    m.DataType2 = DataType.Int;
                    m.Limit21 = 0;
                    m.Limit22 = 200;
                    m.ButtonNum = 3;
                    m.ChartNum = 3;
                    m.Num = c++;
                    m.ChartBrush = Brushes.Green;
                    dict.Add(m.Code, m);

                    m = new VitalMaster();
                    m.Code = "7";
                    m.Name = "SpO2";
                    m.Name1 = "SpO2";
                    m.Unit1 = "%";
                    m.DataType1 = DataType.Alpha;
                    m.ButtonNum = 4;
                    m.ChartNum = 4;
                    m.Num = c++;
                    dict.Add(m.Code, m);

                    m = new VitalMaster();
                    m.Code = "10";
                    m.Name = "食事";
                    m.Name1 = "主食";
                    m.Unit1 = "";
                    m.DataType1 = DataType.Hiragana;
                    m.Name2 = "副食";
                    m.Unit2 = "";
                    m.DataType2 = DataType.Hiragana;
                    m.ButtonNum = 5;
                    m.ChartNum = 5;
                    m.Num = c++;
                    dict.Add(m.Code, m);

                    m = new VitalMaster();
                    m.Code = "4";
                    m.Name = "尿/便回数";
                    m.Name1 = "尿回数";
                    m.Unit1 = "回";
                    m.DataType1 = DataType.Alpha;
                    m.Name2 = "便回数";
                    m.Unit2 = "回";
                    m.DataType2 = DataType.Alpha;
                    m.ButtonNum = 6;
                    m.ChartNum = 6;
                    m.Num = c++;
                    dict.Add(m.Code, m);

                    m = new VitalMaster();
                    m.Code = "8";
                    m.Name = "尿量";
                    m.Name1 = "尿量";
                    m.Unit1 = "ml";
                    m.DataType1 = DataType.Int;
                    m.Limit11 = 0;
                    m.Limit12 = 100000;
                    m.ButtonNum = 7;
                    m.ChartNum = 7;
                    m.Num = c++;
                    dict.Add(m.Code, m);

                    m = new VitalMaster();
                    m.Code = "5";
                    m.Name = "血糖値";
                    m.Name1 = "血糖値";
                    m.Unit1 = "";
                    m.DataType1 = DataType.Int;
                    m.Limit11 = 0;
                    m.Limit12 = 1000;
                    m.ButtonNum = 8;
                    m.ChartNum = 9;
                    m.Num = c++;
                    dict.Add(m.Code, m);

                    m = new VitalMaster();
                    m.Code = "6";
                    m.Name = "体重";
                    m.Name1 = "体重";
                    m.Unit1 = "kg";
                    m.DataType1 = DataType.Float;
                    m.Limit11 = 0;
                    m.Limit12 = 300;
                    m.ButtonNum = 9;
                    m.ChartNum = 8;
                    m.Num = c++;
                    dict.Add(m.Code, m);

                    m = new VitalMaster();
                    m.Code = "901";
                    m.Name = "看護サ処検";
                    m.Name1 = "看護サ処検";
                    m.Unit1 = "";
                    m.DataType1 = DataType.Hiragana;
                    m.ButtonNum = 91;
                    m.ChartNum = 11;
                    m.Num = c++;
                    dict.Add(m.Code, m);

                    m = new VitalMaster();
                    m.Code = "902";
                    m.Name = "フリー２";
                    m.Name1 = "フリー２";
                    m.Unit1 = "";
                    m.DataType1 = DataType.Alpha;
                    m.ButtonNum = 92;
                    m.ChartNum = 12;
                    m.Num = c++;
                    dict.Add(m.Code, m);

                    m = new VitalMaster();
                    m.Code = "903";
                    m.Name = "フリー３";
                    m.Name1 = "フリー３";
                    m.Unit1 = "";
                    m.DataType1 = DataType.Alpha;
                    m.ButtonNum = 93;
                    m.ChartNum = 13;
                    m.Num = c++;
                    dict.Add(m.Code, m);

                    m = new VitalMaster();
                    m.Code = "904";
                    m.Name = "フリー４";
                    m.Name1 = "フリー４";
                    m.Unit1 = "";
                    m.DataType1 = DataType.Alpha;
                    m.ButtonNum = 94;
                    m.ChartNum = 14;
                    m.Num = c++;
                    dict.Add(m.Code, m);

                    m = new VitalMaster();
                    m.Code = "905";
                    m.Name = "フリー５";
                    m.Name1 = "フリー５";
                    m.Unit1 = "";
                    m.DataType1 = DataType.Alpha;
                    m.ButtonNum = 95;
                    m.ChartNum = 15;
                    m.Num = c++;
                    dict.Add(m.Code, m);

                    m = new VitalMaster();
                    m.Code = "906";
                    m.Name = "フリー６";
                    m.Name1 = "フリー６";
                    m.Unit1 = "";
                    m.DataType1 = DataType.Alpha;
                    m.ButtonNum = 96;
                    m.ChartNum = 16;
                    m.Num = c++;
                    dict.Add(m.Code, m);

                    m = new VitalMaster();
                    m.Code = "9";
                    m.Name = "予備";
                    m.Name1 = "予備";
                    m.Unit1 = "";
                    m.DataType1 = DataType.Alpha;
                    m.ButtonNum = 19;
                    m.ChartNum = 19;
                    m.Num = c++;
                    dict.Add(m.Code, m);
                }

                return dict;
            }
        }

        public enum DataType : int
        {
            None = 0,
            Int = 1,
            Float = 2,
            Alpha = 11,
            Hiragana = 12
        }
    }
}
