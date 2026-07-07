using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class FormByotoLayout : StdForm1
    {
        Graphics gb3;
        Graphics gb4;
        Graphics gb5;

        Pen p1 = new Pen(Color.LightGray, 1);
        Pen p2 = new Pen(Color.Black, 2);

        Pen prd1 = new Pen(Color.Red, 1);

        Pen pr2 = new Pen(Color.Red, 2);
        Pen pg2 = new Pen(Color.Green, 2);
        Pen pb2 = new Pen(Color.Blue, 2);

        public FormByotoLayout()
        {
            InitializeComponent();
        }

        private void FormByotoLayout_Load(object sender, EventArgs e)
        {
            this.DrawByoto();
        }

        public void DrawByoto()
        {
            ByotoBox3.Height = 900;
            ByotoBox4.Height = 900;
            ByotoBox5.Height = 900;

            /*
            // 時間帯。デフォルトは午前。
            ByotoPat.ByotoTime byoto_time = ByotoPat.ByotoTime.AM;

            // 12時以降ならば午後とする。
            if (DateTime.Now.Hour >= 12)
            {
                byoto_time = ByotoPat.ByotoTime.PM;
            }

            // 入院患者データの取得
            Dictionary<string, List<ByotoPat>> pat_dict = ByotoPat.GetDict(DateTime.Now.ToString("yyyyMMdd"), byoto_time);
             */

            // 入院患者データの取得
            List<PatIn> pat_list = PatIn.GetList();

            // わかば 描画
            Bitmap bb3 = new Bitmap(ByotoBox3.Width, ByotoBox3.Height);
            ByotoBox3.Image = bb3;
            gb3 = Graphics.FromImage(bb3);

            foreach (ByotoRoom r in ByotoRoom.Dict.Values)
            {
                // わかば以外ならば飛ばす
                if (!r.ByotoCode.Equals("03"))
                {
                    continue;
                }

                gb3.FillRectangle(Brushes.LightYellow, r.X, r.Y, r.Width, r.Height);
                gb3.DrawRectangle(p1, r.X, r.Y, r.Width, r.Height);
                gb3.DrawString(r.Code, AppStat.CurrentFont.Ft, Brushes.Black, r.X + 2, r.Y + 2);

                // ベッドを取得
                List<ByotoBed> bed_list = ByotoBed.GetList(r.Code);

                foreach (ByotoBed obj in bed_list)
                {
                    // ベッド
                    Label b = new Label();
                    b.TextAlign = ContentAlignment.MiddleLeft;
                    b.Font = AppStat.CurrentFont.Ft;
                    b.AutoSize = false;
                    b.Width = obj.Width;
                    b.Height = obj.Height;
                    b.BackColor = Color.White;
                    b.BorderStyle = BorderStyle.FixedSingle;
                    b.Location = new Point(r.X + obj.X, r.Y + obj.Y);
                    b.DoubleClick += new EventHandler(b_DoubleClick);

                    // 病室に入院患者がある場合
                    foreach (PatIn p in pat_list)
                    {
                        if (!p.Room.Equals(obj.RoomCode) || !p.Bed.Equals(obj.SEQ.ToString()))
                        {
                            continue;
                        }

                        if (p.Sex.Equals("2"))
                        {
                            b.ForeColor = Color.Red;
                        }

                        b.Text = p.Name;
                        b.Tag = p.Id;
                        break;
                    }

                    /*
                    if (pat_dict.ContainsKey(r.Code))
                    {
                        foreach (ByotoPat p in pat_dict[r.Code])
                        {
                            // ベッドに入院患者がある場合
                            if (p.BedSEQ.Equals(obj.SEQ))
                            {
                                if (p.Sex.Equals("2"))
                                {
                                    b.ForeColor = Color.Red;
                                }

                                b.Text = p.Name;
                                b.Tag = p.PtId;
                                break;
                            }
                        }
                    }
                     */

                    ByotoBox3.Controls.Add(b);

                    // ベッド番号
                    Label b2 = new Label();
                    b2.TextAlign = ContentAlignment.MiddleCenter;
                    b2.Font = AppStat.CurrentFont.Ft;
                    b2.AutoSize = false;
                    b2.Width = 15;
                    b2.Height = 12;
                    b2.BackColor = Color.LightYellow;
                    b2.BorderStyle = BorderStyle.None;

                    if (obj.Alignment == ByotoBedNumberAlignment.TopLeft)
                    {
                        b2.Location = new Point(r.X + obj.X + 2, r.Y + obj.Y - 13);
                    }
                    else
                    {
                        b2.Location = new Point(r.X + obj.X - 15, r.Y + obj.Y + 10);
                    }

                    b2.Text = obj.SEQ.ToString();

                    ByotoBox3.Controls.Add(b2);
                }
            }


            // さくら 描画
            Bitmap bb4 = new Bitmap(ByotoBox4.Width, ByotoBox4.Height);
            ByotoBox4.Image = bb4;
            gb4 = Graphics.FromImage(bb4);

            foreach (ByotoRoom r in ByotoRoom.Dict.Values)
            {
                // さくら以外ならば飛ばす
                if (!r.ByotoCode.Equals("04"))
                {
                    continue;
                }

                gb4.FillRectangle(Brushes.LightYellow, r.X, r.Y, r.Width, r.Height);
                gb4.DrawRectangle(p1, r.X, r.Y, r.Width, r.Height);
                gb4.DrawString(r.Code, AppStat.CurrentFont.Ft, Brushes.Black, r.X + 2, r.Y + 2);

                // ベッドを取得
                List<ByotoBed> bed_list = ByotoBed.GetList(r.Code);

                foreach (ByotoBed obj in bed_list)
                {
                    // ベッド
                    Label b = new Label();
                    b.TextAlign = ContentAlignment.MiddleLeft;
                    b.Font = AppStat.CurrentFont.Ft;
                    b.AutoSize = false;
                    b.Width = obj.Width;
                    b.Height = obj.Height;
                    b.BackColor = Color.White;
                    b.BorderStyle = BorderStyle.FixedSingle;
                    b.Location = new Point(r.X + obj.X, r.Y + obj.Y);
                    b.DoubleClick += new EventHandler(b_DoubleClick);

                    // 病室に入院患者がある場合
                    foreach (PatIn p in pat_list)
                    {
                        if (!p.Room.Equals(obj.RoomCode) || !p.Bed.Equals(obj.SEQ.ToString()))
                        {
                            continue;
                        }

                        if (p.Sex.Equals("2"))
                        {
                            b.ForeColor = Color.Red;
                        }

                        b.Text = p.Name;
                        b.Tag = p.Id;
                        break;
                    }

                    /*
                    if (pat_dict.ContainsKey(r.Code))
                    {
                        foreach (ByotoPat p in pat_dict[r.Code])
                        {
                            // ベッドに入院患者がある場合
                            if (p.BedSEQ.Equals(obj.SEQ))
                            {
                                if (p.Sex.Equals("2"))
                                {
                                    b.ForeColor = Color.Red;
                                }

                                b.Text = p.Name;
                                b.Tag = p.PtId;
                                break;
                            }
                        }
                    }
                    */

                    ByotoBox4.Controls.Add(b);

                    // ベッド番号
                    Label b2 = new Label();
                    b2.TextAlign = ContentAlignment.MiddleCenter;
                    b2.Font = AppStat.CurrentFont.Ft;
                    b2.AutoSize = false;
                    b2.Width = 15;
                    b2.Height = 12;
                    b2.BackColor = Color.LightYellow;
                    b2.BorderStyle = BorderStyle.None;

                    if (obj.Alignment == ByotoBedNumberAlignment.TopLeft)
                    {
                        b2.Location = new Point(r.X + obj.X + 2, r.Y + obj.Y - 13);
                    }
                    else
                    {
                        b2.Location = new Point(r.X + obj.X - 15, r.Y + obj.Y + 10);
                    }

                    b2.Text = obj.SEQ.ToString();

                    ByotoBox4.Controls.Add(b2);
                }
            }


            // あやめ 描画
            Bitmap bb5 = new Bitmap(ByotoBox5.Width, ByotoBox5.Height);
            ByotoBox5.Image = bb5;
            gb5 = Graphics.FromImage(bb5);

            foreach (ByotoRoom r in ByotoRoom.Dict.Values)
            {
                // あやめ以外ならば飛ばす
                if (!r.ByotoCode.Equals("05"))
                {
                    continue;
                }

                gb5.FillRectangle(Brushes.LightYellow, r.X, r.Y, r.Width, r.Height);
                gb5.DrawRectangle(p1, r.X, r.Y, r.Width, r.Height);
                gb5.DrawString(r.Code, AppStat.CurrentFont.Ft, Brushes.Black, r.X + 2, r.Y + 2);

                // ベッドを取得
                List<ByotoBed> bed_list = ByotoBed.GetList(r.Code);

                foreach (ByotoBed obj in bed_list)
                {
                    // ベッド
                    Label b = new Label();
                    b.TextAlign = ContentAlignment.MiddleLeft;
                    b.Font = AppStat.CurrentFont.Ft;
                    b.AutoSize = false;
                    b.Width = obj.Width;
                    b.Height = obj.Height;
                    b.BackColor = Color.White;
                    b.BorderStyle = BorderStyle.FixedSingle;
                    b.Location = new Point(r.X + obj.X, r.Y + obj.Y);
                    b.DoubleClick += new EventHandler(b_DoubleClick);

                    // 病室に入院患者がある場合
                    foreach (PatIn p in pat_list)
                    {
                        if (!p.Room.Equals(obj.RoomCode) || !p.Bed.Equals(obj.SEQ.ToString()))
                        {
                            continue;
                        }

                        if (p.Sex.Equals("2"))
                        {
                            b.ForeColor = Color.Red;
                        }

                        b.Text = p.Name;
                        b.Tag = p.Id;
                        break;
                    }

                    /*
                    if (pat_dict.ContainsKey(r.Code))
                    {
                        foreach (ByotoPat p in pat_dict[r.Code])
                        {
                            // ベッドに入院患者がある場合
                            if (p.BedSEQ.Equals(obj.SEQ))
                            {
                                if (p.Sex.Equals("2"))
                                {
                                    b.ForeColor = Color.Red;
                                }

                                b.Text = p.Name;
                                b.Tag = p.PtId;
                                break;
                            }
                        }
                    }
                     */

                    ByotoBox5.Controls.Add(b);

                    // ベッド番号
                    Label b2 = new Label();
                    b2.TextAlign = ContentAlignment.MiddleCenter;
                    b2.Font = AppStat.CurrentFont.Ft;
                    b2.AutoSize = false;
                    b2.Width = 15;
                    b2.Height = 12;
                    b2.BackColor = Color.LightYellow;
                    b2.BorderStyle = BorderStyle.None;

                    if (obj.Alignment == ByotoBedNumberAlignment.TopLeft)
                    {
                        b2.Location = new Point(r.X + obj.X + 2, r.Y + obj.Y - 13);
                    }
                    else
                    {
                        b2.Location = new Point(r.X + obj.X - 15, r.Y + obj.Y + 10);
                    }

                    b2.Text = obj.SEQ.ToString();

                    ByotoBox5.Controls.Add(b2);
                }
            }
        }

        void b_DoubleClick(object sender, EventArgs e)
        {
            Label tb = (Label)sender;

            FormControl.FormPat_Show(PatBase.Load(tb.Tag.ToString()));
        }
    }
}
