using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Boundary;

namespace MedicalLibrary.Boundary
{
    public partial class CtrlDrugYoho1 : UserControl
    {
        CtrlOrderSheetGridView1 OrderSheetView1;

        public enum Kind : int
        {
            None = 0,
            Kind21 = 21,
            Kind22 = 22,
            Kind23 = 23
        }

        public Kind Kind1
        {
            get
            {
                if (KindButton1.Checked)
                {
                    return Kind.Kind21;
                }
                else if (KindButton2.Checked)
                {
                    return Kind.Kind22;
                }
                else if (KindButton3.Checked)
                {
                    return Kind.Kind23;
                }
                else
                {
                    return Kind.None;
                }
            }
            set
            {
                if (value == Kind.Kind21)
                {
                    this.KindButton1.Checked = true;
                    this.KindButton2.Checked = false;
                    this.KindButton3.Checked = false;
                }
                else if (value == Kind.Kind22)
                {
                    this.KindButton1.Checked = false;
                    this.KindButton2.Checked = true;
                    this.KindButton3.Checked = false;
                }
                else if (value == Kind.Kind23)
                {
                    this.KindButton1.Checked = false;
                    this.KindButton2.Checked = false;
                    this.KindButton3.Checked = true;
                }
                else
                {
                    this.KindButton1.Checked = false;
                    this.KindButton2.Checked = false;
                    this.KindButton3.Checked = false;
                }
            }
        }

        public CtrlDrugYoho1()
        {
            InitializeComponent();

            this.BunButton1.CheckedChanged += new EventHandler(BunButton_CheckedChanged);
            this.BunButton2.CheckedChanged += new EventHandler(BunButton_CheckedChanged);
            this.BunButton3.CheckedChanged += new EventHandler(BunButton_CheckedChanged);
            this.BunButton4.CheckedChanged += new EventHandler(BunButton_CheckedChanged);
            this.BunButton5.CheckedChanged += new EventHandler(BunButton_CheckedChanged);
            this.BunButton0.CheckedChanged += new EventHandler(BunButton_CheckedChanged);

            this.TimeBox1.CheckedChanged += new EventHandler(TimeBox_CheckedChanged);
            this.TimeBox2.CheckedChanged += new EventHandler(TimeBox_CheckedChanged);
            this.TimeBox3.CheckedChanged += new EventHandler(TimeBox_CheckedChanged);
            this.TimeBox4.CheckedChanged += new EventHandler(TimeBox_CheckedChanged);
            this.TimeBox5.CheckedChanged += new EventHandler(TimeBox_CheckedChanged);

            this.MealButton1.CheckedChanged += new EventHandler(MealButton_CheckedChanged);
            this.MealButton2.CheckedChanged += new EventHandler(MealButton_CheckedChanged);
            this.MealButton3.CheckedChanged += new EventHandler(MealButton_CheckedChanged);
            this.MealButton4.CheckedChanged += new EventHandler(MealButton_CheckedChanged);
            this.MealButton5.CheckedChanged += new EventHandler(MealButton_CheckedChanged);
            this.MealButton6.CheckedChanged += new EventHandler(MealButton_CheckedChanged);
            this.MealButton0.CheckedChanged += new EventHandler(MealButton_CheckedChanged);

            this.SideButton11.CheckedChanged += new EventHandler(SideButton1_CheckedChanged);
            this.SideButton12.CheckedChanged += new EventHandler(SideButton1_CheckedChanged);
            this.SideButton13.CheckedChanged += new EventHandler(SideButton1_CheckedChanged);
            this.SideButton10.CheckedChanged += new EventHandler(SideButton1_CheckedChanged);

            this.SideButton21.CheckedChanged += new EventHandler(SideButton2_CheckedChanged);
            this.SideButton22.CheckedChanged += new EventHandler(SideButton2_CheckedChanged);
            this.SideButton23.CheckedChanged += new EventHandler(SideButton2_CheckedChanged);
            this.SideButton20.CheckedChanged += new EventHandler(SideButton2_CheckedChanged);

            this.SideButton31.CheckedChanged += new EventHandler(SideButton3_CheckedChanged);
            this.SideButton32.CheckedChanged += new EventHandler(SideButton3_CheckedChanged);
            this.SideButton33.CheckedChanged += new EventHandler(SideButton3_CheckedChanged);
            this.SideButton30.CheckedChanged += new EventHandler(SideButton3_CheckedChanged);

            this.SideButton41.CheckedChanged += new EventHandler(SideButton4_CheckedChanged);
            this.SideButton42.CheckedChanged += new EventHandler(SideButton4_CheckedChanged);
            this.SideButton43.CheckedChanged += new EventHandler(SideButton4_CheckedChanged);
            this.SideButton40.CheckedChanged += new EventHandler(SideButton4_CheckedChanged);

            this.FukinBox1.Items.Add("1");
            this.FukinBox1.Items.Add("2");
            this.FukinBox1.Items.Add("3");
            this.FukinBox1.Items.Add("4");
            this.FukinBox1.Items.Add("5");

            this.FukinBox2.Items.Add("1");
            this.FukinBox2.Items.Add("2");
            this.FukinBox2.Items.Add("3");
            this.FukinBox2.Items.Add("4");
            this.FukinBox2.Items.Add("5");

            this.FukinBox3.Items.Add("1");
            this.FukinBox3.Items.Add("2");
            this.FukinBox3.Items.Add("3");
            this.FukinBox3.Items.Add("4");
            this.FukinBox3.Items.Add("5");

            this.FukinBox4.Items.Add("1");
            this.FukinBox4.Items.Add("2");
            this.FukinBox4.Items.Add("3");
            this.FukinBox4.Items.Add("4");
            this.FukinBox4.Items.Add("5");

            this.FukinBox5.Items.Add("1");
            this.FukinBox5.Items.Add("2");
            this.FukinBox5.Items.Add("3");
            this.FukinBox5.Items.Add("4");
            this.FukinBox5.Items.Add("5");

            this.PanelsShow();
        }

        public void Init(CtrlOrderSheetGridView1 sheet_view)
        {
            this.OrderSheetView1 = sheet_view;
        }

        void SideButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.PartBoxShow1();
        }

        void SideButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.PartBoxShow2();
        }

        void SideButton3_CheckedChanged(object sender, EventArgs e)
        {
            this.PartBoxShow3();
        }

        void SideButton4_CheckedChanged(object sender, EventArgs e)
        {
            this.PartBoxShow4();
        }

        private void KindButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.PanelsShow();
        }

        private void KindButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.PanelsShow();
        }

        private void KindButton3_CheckedChanged(object sender, EventArgs e)
        {
            this.PanelsShow();
        }

        void PanelsShow()
        {
            int h = this.KindPanel1.Height + 5;

            if (this.Kind1 == Kind.Kind21)
            {
                this.CondPanel1.Visible = true;
                this.PartPanel1.Visible = false;
                this.YohoPanel1.Visible = true;
                this.MakeButton1.Visible = true;

                this.CondPanel1.Location = new Point(3, h);
                h += this.CondPanel1.Height + 5;

                this.YohoPanel1.Location = new Point(3, h);
                h += this.YohoPanel1.Height + 5;

                this.MakeButton1.Location = new Point(240, h);

                this.YohoPanelShow();
            }
            else if (this.Kind1 == Kind.Kind22)
            {
                this.CondPanel1.Visible = false;
                this.PartPanel1.Visible = true;
                this.YohoPanel1.Visible = true;
                this.MakeButton1.Visible = true;

                this.PartPanel1.Location = new Point(3, h);
                h += this.PartPanel1.Height + 5;

                this.YohoPanel1.Location = new Point(3, h);
                h += this.YohoPanel1.Height + 5;

                this.MakeButton1.Location = new Point(240, h);

                this.PartPanelShow();
                this.YohoPanelShow();
            }
            else if (this.Kind1 == Kind.Kind23)
            {
                this.CondPanel1.Visible = false;
                this.PartPanel1.Visible = true;
                this.YohoPanel1.Visible = true;
                this.MakeButton1.Visible = true;

                this.PartPanel1.Location = new Point(3, h);
                h += this.PartPanel1.Height + 5;

                this.YohoPanel1.Location = new Point(3, h);
                h += this.YohoPanel1.Height + 5;

                this.MakeButton1.Location = new Point(240, h);

                this.PartPanelShow();
                this.YohoPanelShow();
            }
            else
            {
                this.CondPanel1.Visible = false;
                this.PartPanel1.Visible = false;
                this.YohoPanel1.Visible = false;
                this.MakeButton1.Visible = false;
            }
        }

        void YohoPanelShow()
        {
            this.YohoBoxShow();
            this.TimesBoxShow();
        }

        void PartPanelShow()
        {
            this.PartBoxShow1();
            this.PartBoxShow2();
            this.PartBoxShow3();
            this.PartBoxShow4();
        }

        void YohoBoxShow()
        {
            this.YohoBox1.Items.Clear();

            this.YohoBox1.Items.Add("");

            if (this.Kind1 == Kind.Kind21)
            {
                // 分1～分5 に応じて絞り込む

                foreach (OrderMaster obj in OrderMaster.YohoDict21.Values)
                {
                    if (BunButton1.Checked)
                    {
                        if (!obj.FullName.Contains("分１") && !obj.FullName.Contains("１回"))
                        {
                            continue;
                        }
                    }
                    else if (BunButton2.Checked)
                    {
                        if (!obj.FullName.Contains("分２") && !obj.FullName.Contains("２回"))
                        {
                            continue;
                        }
                    }
                    else if (BunButton3.Checked)
                    {
                        if (!obj.FullName.Contains("分３") && !obj.FullName.Contains("３回"))
                        {
                            continue;
                        }
                    }
                    else if (BunButton4.Checked)
                    {
                        if (!obj.FullName.Contains("分４") && !obj.FullName.Contains("４回"))
                        {
                            continue;
                        }
                    }
                    else if (BunButton5.Checked)
                    {
                        if (!obj.FullName.Contains("分５") && !obj.FullName.Contains("５回"))
                        {
                            continue;
                        }
                    }

                    if (TimeBox1.Checked)
                    {
                        if (!obj.FullName.Contains("起"))
                        {
                            continue;
                        }
                    }

                    if (TimeBox2.Checked)
                    {
                        if (!obj.FullName.Contains("朝"))
                        {
                            continue;
                        }
                    }

                    if (TimeBox3.Checked)
                    {
                        if (!obj.FullName.Contains("昼"))
                        {
                            continue;
                        }
                    }

                    if (TimeBox4.Checked)
                    {
                        if (!obj.FullName.Contains("夕"))
                        {
                            continue;
                        }
                    }

                    if (TimeBox5.Checked)
                    {
                        if (!obj.FullName.Contains("寝"))
                        {
                            continue;
                        }
                    }

                    if (MealButton1.Checked)
                    {
                        if (!obj.FullName.Contains("食前"))
                        {
                            continue;
                        }
                    }
                    else if (MealButton2.Checked)
                    {
                        if (!obj.FullName.Contains("食中"))
                        {
                            continue;
                        }
                    }
                    else if (MealButton3.Checked)
                    {
                        if (!obj.FullName.Contains("食後"))
                        {
                            continue;
                        }
                    }
                    else if (MealButton4.Checked)
                    {
                        if (!obj.FullName.Contains("食間"))
                        {
                            continue;
                        }
                    }
                    else if (MealButton5.Checked)
                    {
                        if (!obj.FullName.Contains("食直前"))
                        {
                            continue;
                        }
                    }
                    else if (MealButton6.Checked)
                    {
                        if (!obj.FullName.Contains("食直後"))
                        {
                            continue;
                        }
                    }

                    this.YohoBox1.Items.Add(obj);
                }
            }
            else if (this.Kind1 == Kind.Kind22)
            {
                foreach (OrderMaster obj in OrderMaster.YohoDict22.Values)
                {
                    this.YohoBox1.Items.Add(obj);
                }
            }
            else if (this.Kind1 == Kind.Kind23)
            {
                foreach (OrderMaster obj in OrderMaster.YohoDict23.Values)
                {
                    this.YohoBox1.Items.Add(obj);
                }
            }
        }

        void TimesBoxShow()
        {
            this.TimesBox1.Items.Clear();

            if (this.Kind1 == Kind.Kind21)
            {
                this.TimesLabel1.Visible = true;
                this.TimesBox1.Visible = true;

                this.TimesLabel1.Text = "日数";

                this.TimesBox1.Items.Add("1");
                this.TimesBox1.Items.Add("2");
                this.TimesBox1.Items.Add("3");
                this.TimesBox1.Items.Add("4");
                this.TimesBox1.Items.Add("5");
                this.TimesBox1.Items.Add("6");
                this.TimesBox1.Items.Add("7");
                this.TimesBox1.Items.Add("8");
                this.TimesBox1.Items.Add("9");
                this.TimesBox1.Items.Add("10");
                this.TimesBox1.Items.Add("14");
                this.TimesBox1.Items.Add("20");
                this.TimesBox1.Items.Add("21");
                this.TimesBox1.Items.Add("28");
                this.TimesBox1.Items.Add("30");
                this.TimesBox1.Items.Add("35");
                this.TimesBox1.Items.Add("42");
                this.TimesBox1.Items.Add("49");
                this.TimesBox1.Items.Add("56");
                this.TimesBox1.Items.Add("60");
                this.TimesBox1.Items.Add("90");

                this.TimesBox1.Text = "14";
            }
            else if (this.Kind1 == Kind.Kind22)
            {
                this.TimesLabel1.Visible = true;
                this.TimesBox1.Visible = true;

                this.TimesLabel1.Text = "回数";

                this.TimesBox1.Items.Add("1");
                this.TimesBox1.Items.Add("2");
                this.TimesBox1.Items.Add("3");
                this.TimesBox1.Items.Add("4");
                this.TimesBox1.Items.Add("5");
                this.TimesBox1.Items.Add("6");
                this.TimesBox1.Items.Add("7");
                this.TimesBox1.Items.Add("8");
                this.TimesBox1.Items.Add("9");
                this.TimesBox1.Items.Add("10");
                this.TimesBox1.Items.Add("20");
                this.TimesBox1.Items.Add("30");

                this.TimesBox1.Text = "1";
            }
            else if (this.Kind1 == Kind.Kind23)
            {
                this.TimesLabel1.Visible = false;
                this.TimesBox1.Visible = false;

                this.TimesLabel1.Text = "回数";

                this.TimesBox1.Items.Add("1");

                this.TimesBox1.Text = "1";
            }
        }

        void PartBoxShow1()
        {
            this.PartBox1.Items.Clear();

            foreach (OrderMaster obj in OrderMaster.PartDict.Values)
            {
                if (this.SideButton11.Checked)
                {
                    if (!obj.FullName.Contains("右"))
                    {
                        continue;
                    }
                }

                if (this.SideButton12.Checked)
                {
                    if (!obj.FullName.Contains("左"))
                    {
                        continue;
                    }
                }

                if (this.SideButton13.Checked)
                {
                    if (!obj.FullName.Contains("両"))
                    {
                        continue;
                    }
                }

                this.PartBox1.Items.Add(obj);
            }
        }

        void PartBoxShow2()
        {
            this.PartBox2.Items.Clear();

            foreach (OrderMaster obj in OrderMaster.PartDict.Values)
            {
                if (this.SideButton21.Checked)
                {
                    if (!obj.FullName.Contains("右"))
                    {
                        continue;
                    }
                }

                if (this.SideButton22.Checked)
                {
                    if (!obj.FullName.Contains("左"))
                    {
                        continue;
                    }
                }

                if (this.SideButton23.Checked)
                {
                    if (!obj.FullName.Contains("両"))
                    {
                        continue;
                    }
                }

                this.PartBox2.Items.Add(obj);
            }
        }

        void PartBoxShow3()
        {
            this.PartBox3.Items.Clear();

            foreach (OrderMaster obj in OrderMaster.PartDict.Values)
            {
                if (this.SideButton31.Checked)
                {
                    if (!obj.FullName.Contains("右"))
                    {
                        continue;
                    }
                }

                if (this.SideButton32.Checked)
                {
                    if (!obj.FullName.Contains("左"))
                    {
                        continue;
                    }
                }

                if (this.SideButton33.Checked)
                {
                    if (!obj.FullName.Contains("両"))
                    {
                        continue;
                    }
                }

                this.PartBox3.Items.Add(obj);
            }
        }

        void PartBoxShow4()
        {
            this.PartBox4.Items.Clear();

            foreach (OrderMaster obj in OrderMaster.PartDict.Values)
            {
                if (this.SideButton41.Checked)
                {
                    if (!obj.FullName.Contains("右"))
                    {
                        continue;
                    }
                }

                if (this.SideButton42.Checked)
                {
                    if (!obj.FullName.Contains("左"))
                    {
                        continue;
                    }
                }

                if (this.SideButton43.Checked)
                {
                    if (!obj.FullName.Contains("両"))
                    {
                        continue;
                    }
                }

                this.PartBox4.Items.Add(obj);
            }
        }

        void BunButton_CheckedChanged(object sender, EventArgs e)
        {
            this.YohoBoxShow();
        }

        void TimeBox_CheckedChanged(object sender, EventArgs e)
        {
            this.YohoBoxShow();
        }

        void MealButton_CheckedChanged(object sender, EventArgs e)
        {
            this.YohoBoxShow();
        }

        private void MakeButton1_Click(object sender, EventArgs e)
        {
            if (this.OrderSheetView1 == null)
            {
                return;
            }

            OrderHeader order = new OrderHeader();

            if (this.Kind1 == Kind.Kind21)
            {
                order.KouiCode = "21";
            }
            else if (this.Kind1 == Kind.Kind22)
            {
                order.KouiCode = "22";

                // 部位
                for (int i = 1; i <= 4; i++)
                {
                    if (PartPanel1.Controls.ContainsKey("PartBox" + i.ToString()))
                    {
                        ComboBox cb = (ComboBox)(PartPanel1.Controls["PartBox" + i.ToString()]);

                        if (cb.Text.Length > 0)
                        {
                            OrderDetail detail = new OrderDetail();
                            detail.OrderCode = "88888888";
                            detail.OrderName = cb.Text;
                            detail.Qty = 1;

                            order.DetailList.Add(detail);
                        }
                    }
                }
            }
            else if (this.Kind1 == Kind.Kind23)
            {
                order.KouiCode = "23";

                // 部位
                for (int i = 1; i <= 4; i++)
                {
                    if (PartPanel1.Controls.ContainsKey("PartBox" + i.ToString()))
                    {
                        ComboBox cb = (ComboBox)(PartPanel1.Controls["PartBox" + i.ToString()]);

                        if (cb.Text.Length > 0)
                        {
                            OrderDetail detail = new OrderDetail();
                            detail.OrderCode = "88888888";
                            detail.OrderName = cb.Text;
                            detail.Qty = 1;

                            order.DetailList.Add(detail);
                        }
                    }
                }
            }


            // コメント
            if (this.CommentBox1.Text.Length > 0)
            {
                OrderDetail detail = new OrderDetail();
                detail.OrderCode = "88888888";
                detail.OrderName = this.CommentBox1.Text;
                detail.Qty = 1;

                order.DetailList.Add(detail);
            }

            if (this.CommentBox2.Text.Length > 0)
            {
                OrderDetail detail = new OrderDetail();
                detail.OrderCode = "88888888";
                detail.OrderName = this.CommentBox2.Text;
                detail.Qty = 1;

                order.DetailList.Add(detail);
            }

            // 特別指示
            if (this.EtcButton1.Checked)
            {
                OrderDetail detail = new OrderDetail();
                detail.OrderCode = ".21900000001";
                detail.Qty = 1;

                order.DetailList.Add(detail);
            }
            else if (this.EtcButton2.Checked)
            {
                OrderDetail detail = new OrderDetail();
                detail.OrderCode = ".21900000002";
                detail.Qty = 1;

                order.DetailList.Add(detail);
            }
            else if (this.EtcButton3.Checked)
            {
                OrderDetail detail = new OrderDetail();
                detail.OrderCode = ".21900000003";
                detail.Qty = 1;

                order.DetailList.Add(detail);
            }
            else if (this.EtcButton4.Checked)
            {
                OrderDetail detail = new OrderDetail();
                detail.OrderCode = ".21900000004";
                detail.Qty = 1;

                order.DetailList.Add(detail);
            }
            else if (this.EtcButton5.Checked)
            {
                OrderDetail detail = new OrderDetail();
                detail.OrderCode = ".21900000005";
                detail.Qty = 1;

                order.DetailList.Add(detail);
            }

            // 後発品不可
            if (this.EtcBox1.Checked)
            {
                OrderDetail detail = new OrderDetail();
                detail.OrderCode = "88888803";
                detail.Qty = 1;

                order.DetailList.Add(detail);
            }


            // 用法
            if (this.YohoBox1.Text.Length > 0)
            {
                OrderDetail detail = new OrderDetail();
                detail.OrderCode = ((OrderMaster)(this.YohoBox1.SelectedItem)).OrderCode;
                detail.Qty = 1;

                order.DetailList.Add(detail);
            }
            else
            {
                MessageBox.Show("用法が選択されていません");
                return;
            }


            // リストの最後の行に回数をセットする

            if (order.DetailList.Count > 0)
            {
                if (this.Kind1 == Kind.Kind21 || this.Kind1 == Kind.Kind22)
                {
                    float.TryParse(this.TimesBox1.Text, out order.DetailList[order.DetailList.Count - 1].Times);
                }
                else if (this.Kind1 == Kind.Kind23)
                {
                    order.DetailList[order.DetailList.Count - 1].Times = 1;
                }
            }

            this.OrderSheetView1.OrderDetailInsert(order.DetailList);
        }
    }
}
