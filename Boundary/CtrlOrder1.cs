using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Boundary;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class CtrlOrder1 : UserControl
    {
        public OrderHeader Order1 = new OrderHeader();

        public CtrlOrder1()
        {
            InitializeComponent();
        }

        public CtrlOrder1(string order_id)
        {
            InitializeComponent();

            PatOrder order = PatOrder.Load(order_id);
            order.DetailList = PatOrderDetail.Load(order_id);

            this.Order1 = new OrderHeader(order);

            this.OrderShow();
        }

        public CtrlOrder1(PatOrder order)
        {
            InitializeComponent();

            this.Order1 = new OrderHeader(order);

            this.OrderShow();
        }

        public CtrlOrder1(OrderHeader order)
        {
            InitializeComponent();

            this.Order1 = order;

            this.OrderShow();
        }

        void OrderShow()
        {
            if (this.Order1.SekouDate.Equals("99999999"))
            {
                this.DateLabel1.Text = "日付未定";
            }
            else
            {
                this.DateLabel1.Text = this.Order1.SekouDateString;
            }

            if (this.Order1.InOut.Equals("1"))
            {
                this.DateLabel1.BackColor = Color.LightGreen;
            }
            else if (this.Order1.InOut.Equals("2"))
            {
                this.DateLabel1.BackColor = Color.LightPink;
            }

            this.DeptLabel1.Text = this.Order1.DeptName;
            this.DoctorLabel1.Text = this.Order1.DoctorName;

            string cont1 = "";

            foreach (OrderDetail detail in this.Order1.DetailList)
            {
                cont1 += detail.OrderName;
                cont1 += "  x " + detail.Qty.ToString() + detail.Unit;

                cont1 += Environment.NewLine;
            }

            this.ContLabel1.Text = cont1;

            this.TimesLabel1.Text = this.Order1.Times.ToString();

            if (!this.Order1.StartDate.Equals(this.Order1.EndDate))
            {
                this.TermLabel1.Text = this.Order1.StartDateStringShort + "～" + this.Order1.EndDateStringShort;
            }

            this.KouiLabel1.Text = this.Order1.KouiName;

            if (this.ContLabel1.PreferredHeight > this.ContLabel1.Height)
            {
                this.ContLabel1.Height = this.ContLabel1.PreferredHeight;
            }

            this.Height = this.ContLabel1.Height + 30;
        }

        private void CtrlOrder1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(this, e.X, e.Y);
            }
        }

        private void RemoveMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Parent is CtrlOrderPanel1)
            {
                ((CtrlOrderPanel1)this.Parent).Remove(this);
            }
        }
    }
}
