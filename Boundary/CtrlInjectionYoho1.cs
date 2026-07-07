using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Entity;

namespace MedicalLibrary.Boundary
{
    public partial class CtrlInjectionYoho1 : UserControl
    {
        CtrlOrderSheetGridView1 OrderSheetView1;


        public CtrlInjectionYoho1()
        {
            InitializeComponent();

            this.KindBox1.Items.Add("");

            foreach (OrderMaster obj in OrderMaster.InjectEtcKindDict.Values)
            {
                this.KindBox1.Items.Add(obj);
            }

            this.IntervalBox1.Items.Add("1");
            this.IntervalBox1.Items.Add("2");
            this.IntervalBox1.Items.Add("3");
            this.IntervalBox1.Items.Add("4");
            this.IntervalBox1.Items.Add("5");
            this.IntervalBox1.Items.Add("6");
            this.IntervalBox1.Items.Add("7");
            this.IntervalBox1.Items.Add("8");
            this.IntervalBox1.Items.Add("9");
            this.IntervalBox1.Items.Add("10");
            this.IntervalBox1.Items.Add("11");
            this.IntervalBox1.Items.Add("12");

            this.SpeedBox1.Items.Add("1");
            this.SpeedBox1.Items.Add("2");
            this.SpeedBox1.Items.Add("3");
            this.SpeedBox1.Items.Add("4");
            this.SpeedBox1.Items.Add("5");
            this.SpeedBox1.Items.Add("6");
            this.SpeedBox1.Items.Add("7");
            this.SpeedBox1.Items.Add("8");
            this.SpeedBox1.Items.Add("9");
            this.SpeedBox1.Items.Add("10");
            this.SpeedBox1.Items.Add("15");
            this.SpeedBox1.Items.Add("20");
            this.SpeedBox1.Items.Add("25");
            this.SpeedBox1.Items.Add("30");
            this.SpeedBox1.Items.Add("35");
            this.SpeedBox1.Items.Add("40");
            this.SpeedBox1.Items.Add("50");
            this.SpeedBox1.Items.Add("100");
            this.SpeedBox1.Items.Add("200");
            this.SpeedBox1.Items.Add("300");
            this.SpeedBox1.Items.Add("400");

            foreach (OrderMaster obj in OrderMaster.InjectTimeDict.Values)
            {
                this.TimeBoxA.Items.Add(obj);
                this.TimeBoxB.Items.Add(obj);
            }

            int x = 25;
            int y = 120;

            for (int i = 0; i <= 23; i++)
            {
                CheckBox cb = new CheckBox();
                cb.Name = "TimeBox" + i.ToString();
                cb.Text = i.ToString() + "時";
                cb.AutoSize = true;
                cb.Location = new Point(x, y);

                this.Controls.Add(cb);

                x += 50;

                if ((i + 1) % 8 == 0)
                {
                    x = 25;
                    y += 20;
                }
            }

            this.YohoPanel1.Location = new Point(3, y);
            y += this.YohoPanel1.Height + 5;

            this.MakeButton1.Location = new Point(250, y);
        }

        public void Init(CtrlOrderSheetGridView1 sheet_view)
        {
            this.OrderSheetView1 = sheet_view;
        }

        private void MakeButton1_Click(object sender, EventArgs e)
        {
            if (this.OrderSheetView1 == null)
            {
                return;
            }

            OrderHeader order = new OrderHeader();

            if (this.KindButton1.Checked)
            {
                OrderDetail detail = new OrderDetail();
                detail.OrderCode = "130000510";
                detail.Qty = 1;

                order.DetailList.Add(detail);
            }
            else if (this.KindButton2.Checked)
            {
                OrderDetail detail = new OrderDetail();
                detail.OrderCode = "130000520";
                detail.Qty = 1;

                order.DetailList.Add(detail);
            }
            else if (this.KindButton3.Checked)
            {
                OrderDetail detail = new OrderDetail();
                detail.OrderCode = "130003510";
                detail.Qty = 1;

                order.DetailList.Add(detail);
            }
            else if (this.KindButton4.Checked)
            {
                OrderDetail detail = new OrderDetail();
                detail.OrderCode = "33000000";
                detail.Qty = 1;

                order.DetailList.Add(detail);
            }
            else if (this.KindButton5.Checked)
            {
                OrderDetail detail = new OrderDetail();
                detail.OrderCode = "34000000";
                detail.Qty = 1;

                order.DetailList.Add(detail);
            }
            else if (this.KindButton8.Checked)
            {
                if (this.KindBox1.Text.Length > 0)
                {
                    OrderDetail detail = new OrderDetail();
                    detail.OrderCode = ((OrderMaster)(this.KindBox1.SelectedItem)).OrderCode;
                    detail.Qty = 1;

                    order.DetailList.Add(detail);
                }
                else
                {
                    MessageBox.Show("手技が選択されていません");
                    return;
                }
            }
            else
            {
                MessageBox.Show("手技が選択されていません");
                return;
            }

            // 朝昼夕・○時・持続

            // 時間毎
            if (this.IntervalBox1.Text.Length > 0)
            {
                OrderDetail detail = new OrderDetail();
                detail.OrderCode = "88888888";
                detail.OrderName = this.IntervalBox1.Text + "時間毎";
                detail.Qty = 1;

                order.DetailList.Add(detail);
            }

            // 速度
            if (this.SpeedBox1.Text.Length > 0)
            {
                OrderDetail detail = new OrderDetail();
                detail.OrderCode = "88888888";
                detail.OrderName = "速度" + this.SpeedBox1.Text + "ｍｌ／ｈ";
                detail.Qty = 1;

                order.DetailList.Add(detail);
            }

            // 時間帯
            if (this.TimeBoxA.Text.Length > 0)
            {
                OrderDetail detail = new OrderDetail();
                detail.OrderCode = "88888888";
                detail.OrderName = this.TimeBoxA.Text;
                detail.Qty = 1;

                order.DetailList.Add(detail);
            }

            if (this.TimeBoxB.Text.Length > 0)
            {
                OrderDetail detail = new OrderDetail();
                detail.OrderCode = "88888888";
                detail.OrderName = this.TimeBoxB.Text;
                detail.Qty = 1;

                order.DetailList.Add(detail);
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

            // 側管
            if (this.EtcBox1.Checked)
            {
                OrderDetail detail = new OrderDetail();
                detail.OrderCode = "390000000";
                detail.Qty = 1;

                order.DetailList.Add(detail);
            }

            // 医師の指示通り
            if (this.EtcBox2.Checked)
            {
                OrderDetail detail = new OrderDetail();
                detail.OrderCode = ".050110022";
                detail.Qty = 1;

                order.DetailList.Add(detail);
            }


            // リストの最後の行に回数をセットする

            if (order.DetailList.Count > 0)
            {
                order.DetailList[order.DetailList.Count - 1].Times = 1;
            }

            this.OrderSheetView1.OrderDetailInsert(order.DetailList);
        }
    }
}
