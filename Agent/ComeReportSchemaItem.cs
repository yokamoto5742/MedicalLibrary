using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MedicalLibrary.Agent
{
    public class ComeReportSchemaItem
    {
        string Kind;
        Color C;
        Point Start;
        Point End;
        string Text;
        List<Point> PointList = new List<Point>();

        /// <summary>
        /// 直線・楕円・四角・フリー
        /// 0 非選択, 1 始点選択, 2 終点選択, 3 その他選択
        /// </summary>
        int PointMode;

        /// <summary>
        /// 直線・楕円・四角の場合のコンストラクタ
        /// </summary>
        /// <param name="kind"></param>
        /// <param name="c"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        public ComeReportSchemaItem(string kind, Color c, int startX, int startY, int endX, int endY)
        {
            if (kind.Equals("直線") || kind.Equals("楕円") || kind.Equals("四角"))
            {
                this.SetKind(kind);
                this.C = c;
                this.Start = new Point(startX, startY);
                this.End = new Point(endX, endY);

                this.MakePointList();
            }
        }

        /// <summary>
        /// 文字の場合のコンストラクタ
        /// </summary>
        /// <param name="kind"></param>
        /// <param name="c"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="text"></param>
        public ComeReportSchemaItem(string kind, Color c, int startX, int startY, string text)
        {
            if (kind.Equals("文字"))
            {
                this.SetKind(kind);
                this.C = c;
                this.Start = new Point(startX, startY);

                if (kind.Equals("文字"))
                {
                    this.Text = text;
                }
                else
                {
                    this.Text = "";
                }
            }
        }

        /// <summary>
        /// フリーの場合のコンストラクタ
        /// </summary>
        /// <param name="kind"></param>
        /// <param name="c"></param>
        /// <param name="pointList"></param>
        public ComeReportSchemaItem(string kind, Color c, List<Point> pointList)
        {
            if (kind.Equals("フリー"))
            {
                this.Kind = kind;
                this.C = c;
                this.PointList = pointList;
            }
        }

        /// <summary>
        /// 文字列からシェーマを作る場合のコンストラクタ
        /// </summary>
        /// <param name="schemaItem"></param>
        public ComeReportSchemaItem(string schemaItem)
        {
            if (schemaItem.Contains(","))
            {
                string[] ss = schemaItem.Split(',');

                if (ss[0].Trim().Equals("直線") || ss[0].Trim().Equals("楕円") || ss[0].Trim().Equals("四角"))
                {
                    this.Kind = ss[0].Trim();
                    this.C = Color.FromName(ss[1].Trim());
                    this.Start = new Point(int.Parse(ss[2].Trim()), int.Parse(ss[3].Trim()));
                    this.End = new Point(int.Parse(ss[4].Trim()), int.Parse(ss[5].Trim()));

                    this.MakePointList();
                }
                else if (ss[0].Trim().Equals("文字"))
                {
                    this.Kind = "文字";
                    this.C = Color.FromName(ss[1].Trim());
                    this.Start = new Point(int.Parse(ss[2].Trim()), int.Parse(ss[3].Trim()));
                    this.Text = "";

                    if (ss.Length >= 5)
                    {
                        for (int j = 4; j < ss.Length; j++)
                        {
                            if (ss[j] != null && ss[j].Length > 0)
                            {
                                if (j >= 5)
                                {
                                    this.Text += ",";
                                }

                                this.Text += ss[j].Trim().Replace("<CR+LF>", "\r\n");
                            }
                        }
                    }
                }
                else if (ss[0].Trim().Equals("フリー"))
                {
                    this.Kind = "フリー";
                    this.C = Color.FromName(ss[1].Trim());
                    this.PointList = new List<Point>();

                    for (int j = 2; j < ss.Length; j = j + 2)
                    {
                        this.PointList.Add(new Point(int.Parse(ss[j].Trim()), int.Parse(ss[j + 1].Trim())));
                    }
                }
            }
        }

        public bool Equals(ComeReportSchemaItem tmpItem)
        {
            if (this.Kind.Equals("直線") || this.Kind.Equals("楕円") || this.Kind.Equals("四角"))
            {
                if (this.Kind.Equals(tmpItem.Kind) && this.C.Name.Equals(tmpItem.C.Name) && this.Start.Equals(tmpItem.Start) && this.End.Equals(tmpItem.End))
                {
                    return true;
                }
            }
            else if (this.Kind.Equals("文字"))
            {
                if (this.Kind.Equals(tmpItem.Kind) && this.C.Name.Equals(tmpItem.C.Name) && this.Start.Equals(tmpItem.Start) && this.Text.Equals(tmpItem.Text))
                {
                    return true;
                }
            }
            else if (this.Kind.Equals("フリー"))
            {
                if (!this.Kind.Equals(tmpItem.Kind) || !this.C.Name.Equals(tmpItem.C.Name))
                {
                    return false;
                }

                for (int i = 0; i < this.PointList.Count; i++)
                {
                    if (!this.PointList[i].Equals(tmpItem.PointList[i]))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// シェーマアイテムが指定のポイントを通過するかどうか。
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool PassPoint(Point point)
        {
            for (int i = 0; i < PointList.Count; i++)
            {
                Point p = this.PointList[i];

                for (int x = point.X - 5; x <= point.X + 5; x++)
                {
                    for (int y = point.Y - 5; y <= point.Y + 5; y++)
                    {
                        if (p.X.Equals(x) && p.Y.Equals(y))
                        {
                            if (Kind.Equals("直線"))
                            {
                                if (i >= 0 && i <= 2)
                                {
                                    PointMode = 1;
                                }
                                else if (i >= 14 && i <= 16)
                                {
                                    PointMode = 2;
                                }
                                else
                                {
                                    PointMode = 3;
                                }
                            }
                            else if (Kind.Equals("楕円"))
                            {
                                if (i >= 0 && i <= 7)
                                {
                                    PointMode = 1;
                                }
                                else if (i >= 8 && i <= 15)
                                {
                                    PointMode = 2;
                                }
                                else
                                {
                                    PointMode = 3;
                                }
                            }
                            else if (Kind.Equals("四角"))
                            {
                                if (i == 0)
                                {
                                    PointMode = 1;
                                }
                                else if (i == 4 || i == 8 || i == 12)
                                {
                                    PointMode = 2;
                                }
                                else
                                {
                                    PointMode = 3;
                                }
                            }
                            else if (Kind.Equals("フリー"))
                            {
                                PointMode = 3;
                            }

                            return true;
                        }
                    }
                }
            }

            PointMode = 0;
            return false;
        }

        /// <summary>
        /// シェーマアイテムを描画する。
        /// </summary>
        /// <param name="pBox"></param>
        /// <param name="pen"></param>
        /// <param name="zoom"></param>
        public void Draw(PictureBox pBox, Pen pen, double zoom)
        {
            Graphics g = Graphics.FromImage(pBox.Image);

            float startx = (float)(Start.X * zoom);
            float starty = (float)(Start.Y * zoom);
            float endx = (float)(End.X * zoom);
            float endy = (float)(End.Y * zoom);

            if (Kind.Equals("直線"))
            {
                g.DrawLine(pen, startx, starty, endx, endy);
            }
            else if (Kind.Equals("楕円"))
            {
                g.DrawEllipse(pen, startx, starty, endx - startx, endy - starty);
            }
            else if (Kind.Equals("四角"))
            {
                g.DrawRectangle(pen, startx, starty, endx - startx, endy - starty);
            }
            else if (Kind.Equals("文字"))
            {
                Label tmp = new Label();
                tmp.Location = new Point((int)startx, (int)starty);
                tmp.BorderStyle = BorderStyle.None;
                tmp.Text = Text;
                tmp.ForeColor = C;
                tmp.BackColor = Color.LightYellow;
                tmp.AutoSize = true;
                tmp.MaximumSize = new Size(pBox.Size.Width - tmp.Location.X, pBox.Size.Height - tmp.Location.Y);
                tmp.Font = new Font("", (float)(9 * zoom));

                pBox.Controls.Add(tmp);
            }
            else if (Kind.Equals("フリー"))
            {
                for (int i = 1; i < PointList.Count; i++)
                {
                    g.DrawLine(pen, (float)(PointList[i - 1].X * zoom), (float)(PointList[i - 1].Y * zoom), (float)(PointList[i].X * zoom), (float)(PointList[i].Y * zoom));
                }
            }
        }

        /// <summary>
        /// シェーマアイテムを描画する。
        /// </summary>
        /// <param name="pBox"></param>
        public void Draw(PictureBox pBox, double zoom)
        {
            Pen pen;

            if (zoom >= 1.0)
            {
                pen = new Pen(C, 2);
            }
            else
            {
                pen = new Pen(C, 1);
            }

            this.Draw(pBox, pen, zoom);
        }

        /// <summary>
        /// シェーマアイテムをテキスト表現する。
        /// データベースに保存する時に使用する。
        /// </summary>
        /// <returns></returns>
        public string MakeText()
        {
            string ret = "";

            if (this.Kind.Equals("直線") || this.Kind.Equals("楕円") || this.Kind.Equals("四角"))
            {
                ret = this.Kind + "," + this.C.Name + "," + this.Start.X + "," + this.Start.Y + "," + this.End.X + "," + this.End.Y;
            }
            else if (this.Kind.Equals("文字"))
            {
                ret = "文字," + this.C.Name + "," + this.Start.X + "," + this.Start.Y + ",";

                if (this.Text != null && this.Text.Length > 0)
                {
                    ret += this.Text.Replace("\r\n", "<CR+LF>");
                }
            }
            else if (this.Kind.Equals("フリー"))
            {
                ret = "フリー," + this.C.Name;

                foreach (Point p in this.PointList)
                {
                    ret += "," + p.X + "," + p.Y;
                }
            }

            return ret;
        }

        public string GetKind()
        {
            return Kind;
        }

        public void SetKind(string kind)
        {
            if (kind.Equals("直線") || kind.Equals("楕円") || kind.Equals("四角") || kind.Equals("文字") || kind.Equals("フリー"))
            {
                Kind = kind;
            }
        }

        public Color GetColor()
        {
            return C;
        }

        public void SetColor(Color c)
        {
            C = c;
        }

        public Point GetStart()
        {
            return Start;
        }

        public void SetStart(Point start)
        {
            Start = start;
        }

        public Point GetEnd()
        {
            return End;
        }

        public void SetEnd(Point end)
        {
            End = end;
        }

        public string GetText()
        {
            if (Kind.Equals("文字"))
            {
                return Text;
            }
            else
            {
                return "";
            }
        }

        public void SetText(string text)
        {
            if (Kind.Equals("文字"))
            {
                Text = text;
            }
        }

        public List<Point> GetPointList()
        {
            return PointList;
        }

        public void SetPointList(List<Point> pointList)
        {
            PointList = pointList;
        }

        public int GetPointMode()
        {
            return PointMode;
        }

        public void SetPointMode(int pointMode)
        {
            PointMode = pointMode;
        }

        /// <summary>
        /// シェーマをテキスト保存する場合のバイト数を計算する。
        /// Oracle VARCHAR2 は4000バイト制限があるため事前にバイト数計算が必要。
        /// </summary>
        /// <returns></returns>
        public int GetByteCount()
        {
            return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(this.MakeText() + "\r\n");
        }

        /// <summary>
        /// 直線・楕円・四角の場合、クリックに反応する点の集合を保有する。
        /// </summary>
        public void MakePointList()
        {
            if (this.Kind.Equals("直線"))
            {
                this.PointList.Clear();
                this.PointList.Add(this.Start);
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 1 / 16, this.Start.Y + (this.End.Y - this.Start.Y) * 1 / 16));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 2 / 16, this.Start.Y + (this.End.Y - this.Start.Y) * 2 / 16));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 3 / 16, this.Start.Y + (this.End.Y - this.Start.Y) * 3 / 16));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 4 / 16, this.Start.Y + (this.End.Y - this.Start.Y) * 4 / 16));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 5 / 16, this.Start.Y + (this.End.Y - this.Start.Y) * 5 / 16));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 6 / 16, this.Start.Y + (this.End.Y - this.Start.Y) * 6 / 16));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 7 / 16, this.Start.Y + (this.End.Y - this.Start.Y) * 7 / 16));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 8 / 16, this.Start.Y + (this.End.Y - this.Start.Y) * 8 / 16));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 9 / 16, this.Start.Y + (this.End.Y - this.Start.Y) * 9 / 16));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 10 / 16, this.Start.Y + (this.End.Y - this.Start.Y) * 10 / 16));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 11 / 16, this.Start.Y + (this.End.Y - this.Start.Y) * 11 / 16));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 12 / 16, this.Start.Y + (this.End.Y - this.Start.Y) * 12 / 16));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 13 / 16, this.Start.Y + (this.End.Y - this.Start.Y) * 13 / 16));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 14 / 16, this.Start.Y + (this.End.Y - this.Start.Y) * 14 / 16));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 15 / 16, this.Start.Y + (this.End.Y - this.Start.Y) * 15 / 16));
                this.PointList.Add(this.End);
            }
            else if (this.Kind.Equals("楕円"))
            {
                this.PointList.Clear();
                this.PointList.Add(new Point(this.Start.X, this.Start.Y + (this.End.Y - this.Start.Y) / 2));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 1 / 8, this.Start.Y + (this.End.Y - this.Start.Y) / 4));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 2 / 8, this.Start.Y + (this.End.Y - this.Start.Y) / 8));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 3 / 8, this.Start.Y + (this.End.Y - this.Start.Y) / 16));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 4 / 8, this.Start.Y));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 5 / 8, this.Start.Y + (this.End.Y - this.Start.Y) / 16));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 6 / 8, this.Start.Y + (this.End.Y - this.Start.Y) / 8));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 7 / 8, this.Start.Y + (this.End.Y - this.Start.Y) / 4));
                this.PointList.Add(new Point(this.End.X, this.Start.Y + (this.End.Y - this.Start.Y) / 2));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 7 / 8, this.Start.Y + (this.End.Y - this.Start.Y) * 3 / 4));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 6 / 8, this.Start.Y + (this.End.Y - this.Start.Y) * 7 / 8));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 5 / 8, this.Start.Y + (this.End.Y - this.Start.Y) * 15 / 16));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 4 / 8, this.End.Y));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 3 / 8, this.Start.Y + (this.End.Y - this.Start.Y) * 15 / 16));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 2 / 8, this.Start.Y + (this.End.Y - this.Start.Y) * 7 / 8));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 1 / 8, this.Start.Y + (this.End.Y - this.Start.Y) * 3 / 4));
            }
            else if (this.Kind.Equals("四角"))
            {
                this.PointList.Clear();
                this.PointList.Add(this.Start);
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 1 / 4, this.Start.Y));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 2 / 4, this.Start.Y));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 3 / 4, this.Start.Y));
                this.PointList.Add(new Point(this.End.X, this.Start.Y));
                this.PointList.Add(new Point(this.End.X, this.Start.Y + (this.End.Y - this.Start.Y) * 1 / 4));
                this.PointList.Add(new Point(this.End.X, this.Start.Y + (this.End.Y - this.Start.Y) * 2 / 4));
                this.PointList.Add(new Point(this.End.X, this.Start.Y + (this.End.Y - this.Start.Y) * 3 / 4));
                this.PointList.Add(new Point(this.End.X, this.End.Y));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 3 / 4, this.End.Y));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 2 / 4, this.End.Y));
                this.PointList.Add(new Point(this.Start.X + (this.End.X - this.Start.X) * 1 / 4, this.End.Y));
                this.PointList.Add(new Point(this.Start.X, this.End.Y));
                this.PointList.Add(new Point(this.Start.X, this.Start.Y + (this.End.Y - this.Start.Y) * 3 / 4));
                this.PointList.Add(new Point(this.Start.X, this.Start.Y + (this.End.Y - this.Start.Y) * 2 / 4));
                this.PointList.Add(new Point(this.Start.X, this.Start.Y + (this.End.Y - this.Start.Y) * 1 / 4));
            }
        }

        /// <summary>
        /// クローンを返す。
        /// </summary>
        /// <returns></returns>
        public ComeReportSchemaItem Clone()
        {
            if (this.Kind.Equals("直線") || this.Kind.Equals("楕円") || this.Kind.Equals("四角"))
            {
                ComeReportSchemaItem si = new ComeReportSchemaItem(this.Kind, this.C, this.Start.X, this.Start.Y, this.End.X, this.End.Y);
                return si;
            }
            else if (this.Kind.Equals("文字"))
            {
                ComeReportSchemaItem si = new ComeReportSchemaItem(this.Kind, this.C, this.Start.X, this.Start.Y, this.Text);
                return si;
            }
            else if (this.Kind.Equals("フリー"))
            {
                List<Point> lp = new List<Point>();

                for (int i = 0; i < this.PointList.Count; i++)
                {
                    Point p = new Point(this.PointList[i].X, this.PointList[i].Y);
                    lp.Add(p);
                }

                ComeReportSchemaItem si = new ComeReportSchemaItem(this.Kind, this.C, lp);
                return si;
            }

            return null;
        }
    }
}
