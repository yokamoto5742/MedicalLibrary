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
    public partial class FormInCal1 : StdForm1
    {
        public FormInCal1()
        {
            InitializeComponent();
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            this.InHistoryBox2.Init(p.Id);
        }

        public void CalClear()
        {
            this.CalGridView1.Rows.Clear();
            this.CalGridView1.Columns.Clear();
        }

        public void CalShow()
        {
            this.CalClear();

            PatIn pin = (PatIn)this.InHistoryBox2.PatIn1;

            if (pin.InDate.Length == 0 || pin.InDate.Equals("0"))
            {
                return;
            }

            this.CalGridView1.Columns.Add("種別", "種別");

            int d1 = int.Parse(this.DatePicker1.Value.ToString("yyyyMMdd"));
            int d2 = int.Parse(this.DatePicker1.Value.AddDays(13).ToString("yyyyMMdd"));

            for (int i = 0; i < 14; i++)
            {
                int d = DateTimeAgent.AddDays(this.DatePicker1.Value.ToString("yyyyMMdd"), i);

                this.CalGridView1.Columns.Add(d.ToString(), DateTimeAgent.DateFormat(d, DateTimeAgent.DateFormatKind.MDW));
            }

            int j = 0;

            this.CalGridView1.Rows.Add(1);
            DataGridViewRow r = this.CalGridView1.Rows[j++];
            r.Cells[0].Value = "入院日";

            this.CalGridView1.Rows.Add(1);
            r = this.CalGridView1.Rows[j++];
            r.Cells[0].Value = "日数";

            this.CalGridView1.Rows.Add(1);
            r = this.CalGridView1.Rows[j++];
            r.Cells[0].Value = "病棟";

            this.CalGridView1.Rows.Add(1);
            r = this.CalGridView1.Rows[j++];
            r.Cells[0].Value = "部屋";

            this.CalGridView1.Rows.Add(1);
            r = this.CalGridView1.Rows[j++];
            r.Cells[0].Value = "診療科";

            this.CalGridView1.Rows.Add(1);
            r = this.CalGridView1.Rows[j++];
            r.Cells[0].Value = "主治医";

            this.CalGridView1.Columns[0].DividerWidth = 1;
            this.CalGridView1.Columns[0].Frozen = true;


            PatIn tmp_pin = (PatIn)pin;

            if (tmp_pin.InDateInt <= d2 && (tmp_pin.OutDateInt == 0 || tmp_pin.OutDateInt >= d1))
            {
                // 入院日
                DataGridViewRow tmp_row = this.CalGridView1.Rows[0];

                // 日数
                DataGridViewRow tmp_row1 = this.CalGridView1.Rows[1];

                for (int i = 0; i < 14; i++)
                {
                    int d = DateTimeAgent.AddDays(this.DatePicker1.Value.ToString("yyyyMMdd"), i);

                    // 退院後なら終了
                    if (tmp_pin.OutDateInt > 0 && d > tmp_pin.OutDateInt)
                    {
                        break;
                    }

                    // 入院前なら飛ばす
                    if (d < tmp_pin.InDateInt)
                    {
                        continue;
                    }

                    tmp_row.Cells[d.ToString()].Value = DateTimeAgent.DateFormat(tmp_pin.InDate, DateTimeAgent.DateFormatKind.MD);
                    tmp_row1.Cells[d.ToString()].Value = DateTimeAgent.IntervalDays(tmp_pin.InDateInt, d) + 1;
                }


                // 病棟
                tmp_row = this.CalGridView1.Rows[2];

                // 病室
                tmp_row1 = this.CalGridView1.Rows[3];


                // 病棟・病室
                List<PatIn> tmp_list1 = new List<PatIn>();

                if (tmp_pin.Status == PatInStatus.Yet)
                {
                    // 入院予定の場合
                    tmp_list1.Add(tmp_pin);
                }
                else
                {
                    List<PatIn> in_list = new List<PatIn>();
                    in_list.Add(tmp_pin);

                    // 入院確定の場合
                    tmp_list1 = PatIn.GetRoomList(in_list, false);
                }

                foreach (PatIn tmp in tmp_list1)
                {
                    for (int i = 0; i < 14; i++)
                    {
                        int d = DateTimeAgent.AddDays(this.DatePicker1.Value.ToString("yyyyMMdd"), i);

                        // 退院後なら終了
                        if (tmp_pin.OutDateInt > 0 && d > tmp_pin.OutDateInt)
                        {
                            break;
                        }

                        // 入院前なら飛ばす
                        if (d < tmp_pin.InDateInt)
                        {
                            continue;
                        }

                        // 異動日の翌日以降であればいったん消す。
                        // 異動日当日ならば消さない。
                        if (d > tmp.DoDateInt)
                        {
                            tmp_row.Cells[d.ToString()].Value = "";
                            tmp_row1.Cells[d.ToString()].Value = "";
                        }

                        if (d >= tmp.DoDateInt)
                        {
                            if (tmp_row.Cells[d.ToString()].Value != null &&
                                tmp_row.Cells[d.ToString()].Value.ToString().Length > 0)
                            {
                                tmp_row.Cells[d.ToString()].Value += "-";
                                tmp_row.Cells[d.ToString()].Style.ForeColor = Color.Red;
                            }

                            tmp_row.Cells[d.ToString()].Value += tmp.WardName;

                            if (tmp_row1.Cells[d.ToString()].Value != null &&
                                tmp_row1.Cells[d.ToString()].Value.ToString().Length > 0)
                            {
                                tmp_row1.Cells[d.ToString()].Value += "-";
                                tmp_row1.Cells[d.ToString()].Style.ForeColor = Color.Red;
                            }

                            tmp_row1.Cells[d.ToString()].Value += tmp.Room;
                        }
                    }
                }


                // 診療科
                tmp_row = this.CalGridView1.Rows[4];

                // 診療科
                List<PatIn> tmp_list2 = new List<PatIn>();

                if (tmp_pin.Status == PatInStatus.Yet)
                {
                    // 入院予定の場合
                    tmp_list2.Add(tmp_pin);
                }
                else
                {
                    List<PatIn> in_list = new List<PatIn>();
                    in_list.Add(tmp_pin);

                    // 入院確定の場合
                    tmp_list2 = PatIn.GetDeptList(in_list, false);
                }

                foreach (PatIn tmp in tmp_list2)
                {
                    for (int i = 0; i < 14; i++)
                    {
                        int d = DateTimeAgent.AddDays(this.DatePicker1.Value.ToString("yyyyMMdd"), i);

                        // 退院後なら終了
                        if (tmp_pin.OutDateInt > 0 && d > tmp_pin.OutDateInt)
                        {
                            break;
                        }

                        // 入院前なら飛ばす
                        if (d < tmp_pin.InDateInt)
                        {
                            continue;
                        }

                        // 異動日の翌日以降であればいったん消す。
                        // 異動日当日ならば消さない。
                        if (d > tmp.DoDateInt)
                        {
                            tmp_row.Cells[d.ToString()].Value = "";
                        }

                        if (d >= tmp.DoDateInt)
                        {
                            if (tmp_row.Cells[d.ToString()].Value != null &&
                                tmp_row.Cells[d.ToString()].Value.ToString().Length > 0)
                            {
                                tmp_row.Cells[d.ToString()].Value += "-";
                                tmp_row.Cells[d.ToString()].Style.ForeColor = Color.Red;
                            }

                            tmp_row.Cells[d.ToString()].Value += tmp.DeptName;
                        }
                    }
                }


                // 医師
                tmp_row = this.CalGridView1.Rows[5];

                // 医師
                List<PatIn> tmp_list3 = new List<PatIn>();

                if (tmp_pin.Status == PatInStatus.Yet)
                {
                    // 入院予定の場合
                    tmp_list3.Add(tmp_pin);
                }
                else
                {
                    List<PatIn> in_list = new List<PatIn>();
                    in_list.Add(tmp_pin);

                    // 入院確定の場合
                    tmp_list3 = PatIn.GetDoctorList(in_list, false);
                }

                foreach (PatIn tmp in tmp_list3)
                {
                    for (int i = 0; i < 14; i++)
                    {
                        int d = DateTimeAgent.AddDays(this.DatePicker1.Value.ToString("yyyyMMdd"), i);

                        // 退院後なら終了
                        if (tmp_pin.OutDateInt > 0 && d > tmp_pin.OutDateInt)
                        {
                            break;
                        }

                        // 入院前なら飛ばす
                        if (d < tmp_pin.InDateInt)
                        {
                            continue;
                        }

                        // 異動日の翌日以降であればいったん消す。
                        // 異動日当日ならば消さない。
                        if (d > tmp.DoDateInt)
                        {
                            tmp_row.Cells[d.ToString()].Value = "";
                        }

                        if (d >= tmp.DoDateInt)
                        {
                            if (tmp_row.Cells[d.ToString()].Value != null &&
                                tmp_row.Cells[d.ToString()].Value.ToString().Length > 0)
                            {
                                tmp_row.Cells[d.ToString()].Value += "-";
                                tmp_row.Cells[d.ToString()].Style.ForeColor = Color.Red;
                            }

                            tmp_row.Cells[d.ToString()].Value += tmp.DoctorName;
                        }
                    }
                }
            }
        }

        private void InHistoryBox2_ValueChanged(object sender, EventArgs e)
        {
            PatIn pin = (PatIn)this.InHistoryBox2.PatIn1;

            if (pin.InDate.Length == 0 || pin.InDate.Equals("0"))
            {
                return;
            }

            this.DatePicker1.Value = pin.InDateValue;

            this.CalShow();
        }

        private void DatePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.CalShow();
        }

        private void ShowButton1_Click(object sender, EventArgs e)
        {
            this.CalShow();
        }

        private void PrevButton1_Click(object sender, EventArgs e)
        {
            this.DatePicker1.Value = this.DatePicker1.Value.AddDays(-14);
            this.CalShow();
        }

        private void NextButton1_Click(object sender, EventArgs e)
        {
            this.DatePicker1.Value = this.DatePicker1.Value.AddDays(14);
            this.CalShow();
        }
    }
}
