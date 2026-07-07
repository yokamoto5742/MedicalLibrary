using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace MedicalLibrary.Boundary
{
    public class CtrlPanel1 : Panel
    {
        enum Action : int
        {
            None = 0,
            Moving = 1,
            Sizing = 2
        }

        enum Direction : int
        {
            None = 0,
            N = 1,
            S = 2,
            W = 3,
            E = 4,
            NW = 5,
            NE = 6,
            SW = 7,
            SE = 8
        }

        Action _Action = Action.None;
        Direction _Direction = Direction.None;

        // パネルの位置
        int X = 0;
        int Y = 0;

        // マウスイベントの位置
        int EX = 0;
        int EY = 0;

        Label CloseLabel = new Label();

        bool _CloseButton = true;

        /// <summary>
        /// 閉じるボタンの有無
        /// </summary>
        [Browsable(true)]
        public bool CloseButton
        {
            get
            {
                return this._CloseButton;
            }
            set
            {
                this._CloseButton = value;
                this.CloseLabel.Visible = value;
            }
        }
        

        public CtrlPanel1()
        {
            this.Click += new EventHandler(CtrlPanel1_Click);
            this.MouseDown += new MouseEventHandler(CtrlPanel1_MouseDown);
            this.MouseMove += new MouseEventHandler(CtrlPanel1_MouseMove);
            this.MouseUp += new MouseEventHandler(CtrlPanel1_MouseUp);
            this.MouseLeave += new EventHandler(CtrlPanel1_MouseLeave);

            this.CloseLabel.Text = "×";
            CloseLabel.AutoSize = true;
            CloseLabel.Location = new Point(this.Width - 30, 5);
            CloseLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CloseLabel.BorderStyle = BorderStyle.FixedSingle;
            CloseLabel.Click += new EventHandler(CloseLabel_Click);

            this.Controls.Add(CloseLabel);
        }

        void CtrlPanel1_Click(object sender, EventArgs e)
        {
            this.BringToFront();
        }

        void CloseLabel_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void CtrlPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            this.X = e.X;
            this.Y = e.Y;

            if (this.Cursor == Cursors.Arrow)
            {
                this._Action = Action.Moving;
            }
            else
            {
                this._Action = Action.Sizing;
            }
        }

        private void CtrlPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this._Action == Action.Moving)
            {
                this.Location = new Point(this.Location.X + (e.X - this.X), this.Location.Y + (e.Y - this.Y));
            }
            else if (this._Action == Action.Sizing)
            {
                // マウスイベントの位置が変わらなければ終了
                if (EX == e.X) return;
                if (EY == e.Y) return;

                this.EX = e.X;
                this.EY = e.Y;

                int x = e.X - this.X;
                int y = e.Y - this.Y;

                switch (this._Direction)
                {
                    case Direction.NW:
                        this.Location = new Point(this.Location.X + x, this.Location.Y + y);
                        this.Width -= x;
                        this.Height -= y;
                        break;

                    case Direction.NE:
                        this.Location = new Point(this.Location.X, this.Location.Y + y);
                        this.Width += x;
                        this.Height -= y;
                        break;

                    case Direction.SW:
                        this.Location = new Point(this.Location.X + x, this.Location.Y);
                        this.Width -= x;
                        this.Height += y;
                        break;

                    case Direction.SE:
                        this.Width += x;
                        this.Height += y;
                        break;

                    case Direction.N:
                        this.Location = new Point(this.Location.X, this.Location.Y + y);
                        this.Height -= y;
                        break;

                    case Direction.S:
                        this.Height += y;
                        break;

                    case Direction.W:
                        this.Location = new Point(this.Location.X + x, this.Location.Y);
                        this.Width -= x;
                        break;

                    case Direction.E:
                        this.Width += x;
                        break;

                    default:
                        break;
                }
            }
            else if (this._Action == Action.None)
            {
                bool bx1 = false;
                bool bx2 = false;

                bool by1 = false;
                bool by2 = false;

                if (e.X <= 5)
                {
                    bx1 = true;
                }
                else if (e.X >= this.Width - 5)
                {
                    bx2 = true;
                }

                if (e.Y <= 5)
                {
                    by1 = true;
                }
                else if (e.Y >= this.Height - 5)
                {
                    by2 = true;
                }

                if ((bx1 && by1))
                {
                    this._Direction = Direction.NW;
                    this.Cursor = Cursors.SizeNWSE;
                }
                else if ((bx2 && by2))
                {
                    this._Direction = Direction.SE;
                    this.Cursor = Cursors.SizeNWSE;
                }
                else if ((bx1 && by2))
                {
                    this._Direction = Direction.SW;
                    this.Cursor = Cursors.SizeNESW;
                }
                else if ((bx2 && by1))
                {
                    this._Direction = Direction.NE;
                    this.Cursor = Cursors.SizeNESW;
                }
                else if (bx1)
                {
                    this._Direction = Direction.W;
                    this.Cursor = Cursors.SizeWE;
                }
                else if (bx2)
                {
                    this._Direction = Direction.E;
                    this.Cursor = Cursors.SizeWE;
                }
                else if (by1)
                {
                    this._Direction = Direction.N;
                    this.Cursor = Cursors.SizeNS;
                }
                else if (by2)
                {
                    this._Direction = Direction.S;
                    this.Cursor = Cursors.SizeNS;
                }
                else
                {
                    this._Direction = Direction.None;
                    this.Cursor = Cursors.Arrow;
                }
            }
        }

        private void CtrlPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            this._Action = Action.None;
            this._Direction = Direction.None;
        }

        void CtrlPanel1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            this._Direction = Direction.None;
        }
    }
}
