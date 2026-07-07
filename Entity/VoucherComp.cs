using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class VoucherComp : StdEntity
    {
        /// <summary>
        /// 伝票番号
        /// </summary>
        public string Code = "";

        /// <summary>
        /// 座標行
        /// </summary>
        public int Row = 1;

        /// <summary>
        /// 座標列
        /// </summary>
        public int Col = 1;

        public string Pos
        {
            get
            {
                return Col.ToString().PadLeft(3, '0') + Row.ToString().PadLeft(3, '0');
            }
        }

        /// <summary>
        /// セル内容
        /// </summary>
        public string ContString = "";

        /// <summary>
        /// テキスト(Kind = 2), チェックボックス(Kind = 3), トグルボタン（Kind = 5）の場合
        /// </summary>
        public VoucherCont Cont1
        {
            get
            {
                return VoucherCont.GetFromString(this.Attr1.Kind, this.ContString);
            }
        }

        /// <summary>
        /// コンボボックス（Kind = 4）の場合のみ
        /// </summary>
        public List<VoucherCont> ContList1
        {
            get
            {
                return VoucherCont.GetListFromString(this.Attr1.Kind, this.ContString);
            }
        }

        /// <summary>
        /// セル属性
        /// </summary>
        public string AttrString = "";

        public VoucherAttr Attr1
        {
            get
            {
                return VoucherAttr.GetFromString(this.AttrString);
            }
        }

        /// <summary>
        /// 列幅
        /// </summary>
        public float Width = 80.0F;

        /// <summary>
        /// 行高
        /// </summary>
        public float Height = 25.0F;

        public bool Checked = false;

        // このコンポーネントが ON/OFF する対象のコンポーネント
        public List<VoucherLink> ChildList = new List<VoucherLink>();

        // このコンポーネントを ON/OFF させる元となるコンポーネント
        public List<VoucherLink> ParentList = new List<VoucherLink>();

        /// <summary>
        /// このコンポーネントが属するグループのリスト
        /// </summary>
        public List<List<VoucherCompGroup>> GroupList = new List<List<VoucherCompGroup>>();


        public static List<VoucherComp> GetList(string code)
        {
            List<VoucherComp> list = new List<VoucherComp>();

            if (code.Length == 0)
            {
                return list;
            }

            string cmd = "select * from macs.ＮＴ伝票コントロールマスター t " +
                " where t.伝票番号 = '" + code + "'" +
                " order by t.座標行, t.座標列";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                VoucherComp obj = new VoucherComp();

                obj.Code = code;
                int.TryParse(tmp.DataDict["座標行"].ToString(), out obj.Row);
                int.TryParse(tmp.DataDict["座標列"].ToString(), out obj.Col);
                obj.ContString = tmp.DataDict["セル内容"].ToString();
                obj.AttrString = tmp.DataDict["セル属性"].ToString();
                float.TryParse(tmp.DataDict["列幅"].ToString(), out obj.Width);
                float.TryParse(tmp.DataDict["行高"].ToString(), out obj.Height);

                if (obj.Height < 25.0F && obj.Height > 0)
                {
                    obj.Height = 25.0F;
                }

                obj.Width *= 10;

                list.Add(obj);
            }


            Dictionary<string, List<VoucherLink>> child_dict = VoucherLink.GetChildDict(code);

            foreach (VoucherComp obj in list)
            {
                if (child_dict.ContainsKey(obj.Pos))
                {
                    obj.ChildList = child_dict[obj.Pos];
                }
            }


            Dictionary<string, List<VoucherLink>> parent_dict = VoucherLink.GetParentDict(code);

            foreach (VoucherComp obj in list)
            {
                if (parent_dict.ContainsKey(obj.Pos))
                {
                    obj.ParentList = parent_dict[obj.Pos];
                }
            }

            Dictionary<int, List<VoucherCompGroup>> group_dict = VoucherCompGroup.GetDict(code);
            bool group_flg = false;

            foreach (VoucherComp obj in list)
            {
                group_flg = false;

                foreach (List<VoucherCompGroup> group_list in group_dict.Values)
                {
                    foreach (VoucherCompGroup group in group_list)
                    {
                        if (obj.Pos.Equals(group.Pos))
                        {
                            obj.GroupList.Add(group_list);
                            group_flg = true;
                            break;
                        }
                    }

                    if (group_flg)
                    {
                        break;
                    }
                }
            }

            return list;
        }
    }

    public class VoucherCont
    {
        public string Code = "";

        public string Name = "";

        public static VoucherCont GetFromString(string kind, string s)
        {
            VoucherCont obj = new VoucherCont();

            if (kind.Equals("2") || kind.Equals("3") || kind.Equals("5"))
            {
                if (s.Contains('#'))
                {
                    obj.Code = s.Split('#')[1];
                    obj.Name = s.Split('#')[0];
                }
                else
                {
                    obj.Name = s;
                }
            }

            return obj;
        }

        public static List<VoucherCont> GetListFromString(string kind, string s)
        {
            List<VoucherCont> list = new List<VoucherCont>();

            if (kind.Equals("4"))
            {
                // コンボボックスの時はタブで複数に区切られる
                foreach (string ss in s.Split('\t'))
                {
                    VoucherCont obj = new VoucherCont();

                    if (ss.Contains('#'))
                    {
                        obj.Code = ss.Split('#')[1];
                        obj.Name = ss.Split('#')[0];
                    }
                    else
                    {
                        obj.Name = ss;
                    }

                    list.Add(obj);
                }
            }

            return list;
        }
    }

    public class VoucherAttr
    {
        /// <summary>
        /// 1: 数値, 2: テキスト, 3: チェックボックス, 4: コンボボックス, 5: トグルボタン, 6: 格納型コンボボックス
        /// </summary>
        public string Kind = "";

        public string ForeColor = "";

        public Color ForeColorValue
        {
            get
            {
                if (VoucherCompColor.Dict.ContainsKey(this.ForeColor))
                {
                    return VoucherCompColor.Dict[this.ForeColor].Color1;
                }
                else
                {
                    return Color.Black;
                }
            }
        }

        public string BackColor = "";

        public Color BackColorValue
        {
            get
            {
                if (VoucherCompColor.Dict.ContainsKey(this.BackColor))
                {
                    return VoucherCompColor.Dict[this.BackColor].Color1;
                }
                else
                {
                    return Color.White;
                }
            }
        }

        public string Border = "";

        public string BorderColor = "";

        public Color BorderColorValue
        {
            get
            {
                if (VoucherCompColor.Dict.ContainsKey(this.BorderColor))
                {
                    return VoucherCompColor.Dict[this.BorderColor].Color1;
                }
                else
                {
                    return Color.Black;
                }
            }
        }

        public string Font = "";

        public string FontSize = "";

        public Font FontValue
        {
            get
            {
                float fs = 9;

                if (this.FontSize.Trim('0', ' ').Length > 0)
                {
                    float.TryParse(this.FontSize.Trim('0'), out fs);
                }

                return new Font(AppFont.DefaultFont.Ft.FontFamily, fs);
            }
        }

        public string ToggleRow1 = "";

        public int ToggleRowValue1
        {
            get
            {
                int i = 0;

                int.TryParse(ToggleRow1, out i);

                return i;
            }
        }

        public string ToggleRow2 = "";

        public int ToggleRowValue2
        {
            get
            {
                int i = 0;

                int.TryParse(ToggleRow2, out i);

                return i;
            }
        }

        public string ToggleRowOn = "";

        public bool ToggleRowOnValue
        {
            get
            {
                return ToggleRowOn.Equals("1") ? true : false;
            }
        }

        public string Embed = "";

        /// <summary>
        /// ＭＳ Ｐゴシック固定
        /// </summary>
        public string FontFamily = "ＭＳ Ｐゴシック";


        public static VoucherAttr GetFromString(string s)
        {
            VoucherAttr obj = new VoucherAttr();

            obj.Kind = s.Substring(0, 1);
            obj.ForeColor = s.Substring(1, 2);
            obj.BackColor = s.Substring(3, 2);
            obj.Border = s.Substring(6, 4);
            obj.BorderColor = s.Substring(10, 2);
            obj.Font = s.Substring(42, 1);
            obj.FontSize = s.Substring(43, 2);
            obj.ToggleRow1 = s.Substring(60, 3);
            obj.ToggleRow2 = s.Substring(63, 3);
            obj.ToggleRowOn = s.Substring(66, 1);
            obj.Embed = s.Substring(94, 1);

            return obj;
        }


        public bool IsToggleRow(int i)
        {
            bool result = false;

            if (this.ToggleRowValue1 <= i && i <= this.ToggleRowValue2)
            {
                result = true;
            }

            return result;
        }
    }

    public class VoucherCell
    {
        public float Width = 20.0F;

        public float Height = 10.0F;

        public float X = 0.0F;

        public float Y = 0.0F;

        public bool Visible = true;


        public static VoucherCell[,] Get(List<VoucherComp> list)
        {
            int Rows = 0;
            int Cols = 0;

            foreach (VoucherComp obj in list)
            {
                if (obj.Col > Cols)
                {
                    Cols = obj.Col;
                }

                if (obj.Row > Rows)
                {
                    Rows = obj.Row;
                }
            }


            VoucherCell[,] cells = new VoucherCell[Rows,Cols];

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    cells[i, j] = new VoucherCell();
                }
            }

            foreach (VoucherComp obj in list)
            {
                for (int i = 0; i < Rows; i++)
                {
                    cells[i, obj.Col - 1].Width = obj.Width;
                }

                for (int j = 0; j < Cols; j++)
                {
                    cells[obj.Row - 1, j].Height = obj.Height;
                }
/*
                if (obj.Attr1.ToggleRowValue1 > 0 && obj.Attr1.ToggleRowValue2 > 0 &&
                    obj.Attr1.ToggleRowValue1 <= obj.Attr1.ToggleRowValue2)
                {
                    for (int i = obj.Attr1.ToggleRowValue1; i <= obj.Attr1.ToggleRowValue2; i++)
                    {
                        for (int j = 0; j < Cols; j++)
                        {
                            if (obj.Checked)
                            {
                                cells[i - 1, j].Visible = true;
                            }
                            else
                            {
                                cells[i - 1, j].Visible = false;
                            }
                        }
                    }
                }
*/
            }

            bool[] Visible_Rows = new bool[Rows];
            bool[] Visible_Cols = new bool[Cols];

            for (int i = 0; i < Rows; i++)
            {
                Visible_Rows[i] = false;
                bool is_toggle = false;

                foreach (VoucherComp obj in list)
                {
                    if (obj.Attr1.IsToggleRow(i + 1))
                    {
                        is_toggle = true;
                        break;
                    }
                }

                if (!is_toggle)
                {
                    Visible_Rows[i] = true;
                }
                else
                {
                    foreach (VoucherComp obj in list)
                    {
                        if (obj.Attr1.IsToggleRow(i + 1) && (obj.Attr1.ToggleRowOnValue || obj.Checked))
                        {
                            Visible_Rows[i] = true;
                            break;
                        }
                    }
                }
            }

            for (int j = 0; j < Cols; j++)
            {
                Visible_Cols[j] = true;
            }

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (i > 0)
                    {
                        if (Visible_Rows[i])
//                        if (cells[i, j].Visible)
                        {
                            cells[i, j].Y = cells[i - 1, j].Y + cells[i - 1, j].Height;
                        }
                        else
                        {
                            cells[i, j].Y = cells[i - 1, j].Y;
                            cells[i, j].Visible = false;
                        }
                    }
                    else
                    {
                        cells[i, j].Y = 0.0F;
                    }

                    if (j > 0)
                    {
                        if (Visible_Cols[j])
//                        if (cells[i, j].Visible)
                        {
                            cells[i, j].X = cells[i, j - 1].X + cells[i, j - 1].Width;
                        }
                        else
                        {
                            cells[i, j].X = cells[i, j - 1].X;
                            cells[i, j].Visible = false;
                        }
                    }
                    else
                    {
                        cells[i, j].X = 0.0F;
                    }
                }
            }

            return cells;
        }
    }


    public class VoucherCompColor
    {
        public string Code = "";

        public Color Color1 = Color.White;


        static Dictionary<string, VoucherCompColor> dict = new Dictionary<string, VoucherCompColor>();


        public VoucherCompColor(string code, Color color)
        {
            this.Code = code;
            this.Color1 = color;
        }

        public static Dictionary<string, VoucherCompColor> Dict
        {
            get
            {
                if (dict.Count == 0)
                {
                    dict.Add("01", new VoucherCompColor("01", Color.Black));
                    dict.Add("02", new VoucherCompColor("02", Color.Blue));
                    dict.Add("03", new VoucherCompColor("03", Color.Purple));
                    dict.Add("04", new VoucherCompColor("04", Color.Red));
                    dict.Add("05", new VoucherCompColor("05", Color.Orange));
                    dict.Add("06", new VoucherCompColor("06", Color.Yellow));
                    dict.Add("07", new VoucherCompColor("07", Color.LightGreen));
                    dict.Add("08", new VoucherCompColor("08", Color.Cyan));
                    dict.Add("09", new VoucherCompColor("09", Color.Black));
                    dict.Add("10", new VoucherCompColor("10", Color.DarkBlue));
                    dict.Add("11", new VoucherCompColor("11", Color.MediumPurple));
                    dict.Add("12", new VoucherCompColor("12", Color.DarkRed));
                    dict.Add("13", new VoucherCompColor("13", Color.Brown));
                    dict.Add("14", new VoucherCompColor("14", Color.LightGoldenrodYellow));
                    dict.Add("15", new VoucherCompColor("15", Color.Green));
                    dict.Add("16", new VoucherCompColor("16", Color.DarkCyan));
                    dict.Add("17", new VoucherCompColor("17", Color.Gray));
                    dict.Add("18", new VoucherCompColor("18", Color.DarkBlue));
                    dict.Add("19", new VoucherCompColor("19", Color.MediumPurple));
                    dict.Add("20", new VoucherCompColor("20", Color.Brown));
                    dict.Add("21", new VoucherCompColor("21", Color.SaddleBrown));
                    dict.Add("22", new VoucherCompColor("22", Color.LightGoldenrodYellow));
                    dict.Add("23", new VoucherCompColor("23", Color.DarkGreen));
                    dict.Add("24", new VoucherCompColor("24", Color.DarkCyan));
                    dict.Add("25", new VoucherCompColor("25", Color.White));
                    dict.Add("26", new VoucherCompColor("26", Color.LightYellow));
                }

                return dict;
            }
        }
    }
}
