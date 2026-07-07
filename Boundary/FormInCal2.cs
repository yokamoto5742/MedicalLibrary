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
    public partial class FormInCal2 : StdForm1
    {
        public FormInCal2()
        {
            InitializeComponent();
        }

        private void FormInCal2_Load(object sender, EventArgs e)
        {
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            this.InHistoryBox2.Init(p.Id);
        }

        void CalShow()
        {
            this.CalPanel.Controls.Clear();

            // カレンダー初日からの日数
            int d = 0;

            // 入院からの日数
            int dd = 1;

            // ラベルの幅
            int width = (this.CalPanel.Width - 35) / 7;

            // ラベルを配置する高さ
            int h = 10;

            DateTime dt01 = DateTime.Parse(DatePicker1.Value.ToString("yyyy/MM/01"));
            DateTime dt02 = DateTime.Parse(DatePicker1.Value.ToString("yyyy/MM/01")).AddMonths(1).AddDays(-1);

            // カレンダー表示する初日と最終日
            DateTime dt1 = dt01.AddDays((double)(0 - dt01.DayOfWeek));
            DateTime dt2 = dt02.AddDays((double)(6 - dt02.DayOfWeek));

            // 患者IDと入院番号の組み合わせ
            List<PatIn> in_list = new List<PatIn>();

            foreach (PatIn p_in in PatIn.GetHistory(this.Pat.Id))
            {
                // カレンダー表示期間内であれば対象とする
                if (p_in.InDate.CompareTo(dt2.ToString("yyyyMMdd")) <= 0 &&
                    (!AppString.IsDate(p_in.OutDate) || p_in.OutDate.CompareTo(dt1.ToString("yyyyMMdd")) >= 0))
                {
                    in_list.Add(p_in);
                }
            }

            // 部屋移動歴
            List<PatIn> list1 = PatIn.GetRoomList(in_list, false);

            list1.Sort((x, y) =>
                {
                    int i = x.DoDate.CompareTo(y.DoDate);

                    if (i == 0)
                    {
                        i = x.DoTime.CompareTo(y.DoTime);
                    }

                    if (i == 0)
                    {
                        i = x.SEQ2 - y.SEQ2;
                    }

                    return i;
                });


            // 科移動歴
            List<PatIn> list2 = PatIn.GetDeptList(in_list, false);

            list2.Sort((x, y) =>
            {
                int i = x.DoDate.CompareTo(y.DoDate);

                if (i == 0)
                {
                    i = x.DoTime.CompareTo(y.DoTime);
                }

                if (i == 0)
                {
                    i = x.SEQ2 - y.SEQ2;
                }

                return i;
            });


            // 医師移動歴
            List<PatIn> list3 = PatIn.GetDoctorList(in_list, false);

            list3.Sort((x, y) =>
            {
                int i = x.DoDate.CompareTo(y.DoDate);

                if (i == 0)
                {
                    i = x.DoTime.CompareTo(y.DoTime);
                }

                if (i == 0)
                {
                    i = x.SEQ2 - y.SEQ2;
                }

                return i;
            });


            // 各入院日のデータ
            List<PatInCal> list = new List<PatInCal>();

            while (true)
            {
                DateTime dt = dt1.AddDays(d);

                if (dt.ToString("yyyyMMdd").CompareTo(dt2.ToString("yyyyMMdd")) > 0)
                {
                    // 終了日を超えたら終了
                    break;
                }
                else if (d > 42)
                {
                    // 最大で６週間
                    break;
                }

                foreach (PatIn p_in in in_list)
                {
                    if (p_in.InDate.CompareTo(dt.ToString("yyyyMMdd")) <= 0 &&
                        (p_in.OutDate.CompareTo(dt.ToString("yyyyMMdd")) >= 0 || !AppString.IsDate(p_in.OutDate)))
                    {
                        // 入院日以後　かつ
                        // 退院日以前　または　退院未定

                        // 入院日の場合は dd をリセット
                        if (p_in.InDate.Equals(dt.ToString("yyyyMMdd")))
                        {
                            dd = 1;
                        }

                        PatInCal obj = new PatInCal();

                        obj.Date = dt.ToString("yyyyMMdd");
                        obj.Id = p_in.Id;
                        obj.SEQ = p_in.SEQ;
                        obj.InDate = p_in.InDate;
                        obj.OutDate = p_in.OutDate;

                        list.Add(obj);

                        dd++;
                    }
                }

                d++;
            }


            // 病室
            foreach (PatIn obj in list1)
            {
                foreach (PatInCal cal in list)
                {
                    // 入院番号が異なれば飛ばす
                    if (!obj.SEQ.Equals(cal.SEQ)) continue;

                    // 本日より後なら飛ばす
                    if (obj.DoDate.CompareTo(cal.Date) > 0) continue;

                    if (obj.DoDate.Equals(cal.Date) && cal.Room.Length > 0)
                    {
                        // 本日の異動 かつ 前の病室が存在する場合
                        cal.Room += "-" + obj.Room;
                    }
                    else
                    {
                        cal.Room = obj.Room;
                    }

                    if (obj.DoDate.Equals(cal.Date))
                    {
                        // 本日の異動の場合

                        if (!cal.WardNameLast.Equals(obj.WardName))
                        {
                            // 前の病棟と異なる場合
                            if (cal.WardName.Length > 0)
                            {
                                cal.WardName += "-";
                            }

                            cal.WardName += obj.WardName;
                        }
                    }
                    else
                    {
                        cal.WardName = obj.WardName;
                    }
                }
            }

            // 科
            foreach (PatIn obj in list2)
            {
                foreach (PatInCal cal in list)
                {
                    // 入院番号が異なれば飛ばす
                    if (!obj.SEQ.Equals(cal.SEQ)) continue;

                    // 本日より後なら飛ばす
                    if (obj.DoDate.CompareTo(cal.Date) > 0) continue;

                    if (obj.DoDate.Equals(cal.Date) && cal.DeptName.Length > 0)
                    {
                        // 本日の異動 かつ 前の科が存在する場合
                        cal.DeptName += "-" + (Dict.DeptDict.ContainsKey(obj.Dept) ? Dict.DeptDict[obj.Dept].ShortName : " ");
                    }
                    else
                    {
                        cal.DeptName = Dict.DeptDict.ContainsKey(obj.Dept) ? Dict.DeptDict[obj.Dept].ShortName : "";
                    }
                }
            }

            // 医師
            foreach (PatIn obj in list3)
            {
                foreach (PatInCal cal in list)
                {
                    // 入院番号が異なれば飛ばす
                    if (!obj.SEQ.Equals(cal.SEQ)) continue;

                    // 本日より後なら飛ばす
                    if (obj.DoDate.CompareTo(cal.Date) > 0) continue;

                    if (obj.DoDate.Equals(cal.Date) && cal.DoctorName.Length > 0)
                    {
                        // 本日の異動 かつ 前の医師が存在する場合
                        cal.DoctorName += "-" + (Dict.DoctorDict.ContainsKey(obj.Doctor) ? Dict.DoctorDict[obj.Doctor].ShortName.Replace("　", " ").Replace(" ", "") : " ");
                    }
                    else
                    {
                        cal.DoctorName = Dict.DoctorDict.ContainsKey(obj.Doctor) ? Dict.DoctorDict[obj.Doctor].ShortName.Replace("　", " ").Replace(" ", "") : "";
                    }
                }
            }

            d = 0;

            while (true)
            {
                DateTime dt = dt1.AddDays(d);

                if (dt.ToString("yyyyMMdd").CompareTo(dt2.ToString("yyyyMMdd")) > 0)
                {
                    // 終了日を超えたら終了
                    break;
                }
                else if (d > 42)
                {
                    // 最大で６週間
                    break;
                }


                int w = (int)dt.DayOfWeek;

                Label lb_date = new Label();
                lb_date.Name = "Date" + dt.ToString("yyyyMMdd");
                lb_date.AutoSize = false;
                lb_date.Location = new Point(w * width + 10, h);
                lb_date.Size = new Size(width - 2, 23);
                lb_date.Text = dt.ToString("M/d (ddd)");
                lb_date.TextAlign = ContentAlignment.MiddleCenter;
                lb_date.Font = AppFont.FB9.Ft;

                if (w == 0)
                {
                    lb_date.BackColor = Color.LightPink;
                }
                else if (w == 6)
                {
                    lb_date.BackColor = Color.LightCyan;
                }
                else
                {
                    lb_date.BackColor = Color.LightYellow;
                }

                // 本日の場合
                if (dt.ToString("yyyyMMdd").Equals(DateTime.Now.ToString("yyyyMMdd")))
                {
                    lb_date.ForeColor = Color.Red;
                }

                this.CalPanel.Controls.Add(lb_date);


                PatInCal obj = new PatInCal();

                foreach (PatInCal cal in list)
                {
                    if (cal.Date.Equals(dt.ToString("yyyyMMdd")))
                    {
                        obj = cal;
                        break;
                    }
                }

                if (AppString.IsDate(obj.Date))
                {
                    if (obj.Days >= 0)
                    {
                        lb_date.Text += " " + obj.Days.ToString() + "日目";
                    }

                    // 病棟
                    Label lb_ward = new Label();
                    lb_ward.Name = "Ward" + obj.Date;
                    lb_ward.AutoSize = false;
                    lb_ward.Location = new Point(w * width + 10, h + 25);
                    lb_ward.Size = new Size(width - 2, 23);
                    lb_ward.Text = obj.WardName;
                    lb_ward.AutoEllipsis = true;
                    lb_ward.BackColor = Color.White;
                    lb_ward.TextAlign = ContentAlignment.MiddleCenter;

                    if (obj.WardName.Contains('-'))
                    {
                        lb_ward.ForeColor = Color.Red;
                    }

                    this.CalPanel.Controls.Add(lb_ward);

                    // 病室
                    Label lb_room = new Label();
                    lb_room.Name = "Room" + obj.Date;
                    lb_room.AutoSize = false;
                    lb_room.Location = new Point(w * width + 10, h + 50);
                    lb_room.Size = new Size(width - 2, 23);
                    lb_room.Text = obj.Room;
                    lb_room.AutoEllipsis = true;
                    lb_room.BackColor = Color.White;
                    lb_room.TextAlign = ContentAlignment.MiddleCenter;

                    if (obj.Room.Contains('-'))
                    {
                        lb_room.ForeColor = Color.Red;
                    }

                    this.CalPanel.Controls.Add(lb_room);

                    // 科
                    Label lb_dept = new Label();
                    lb_dept.Name = "Dept" + obj.Date;
                    lb_dept.AutoSize = false;
                    lb_dept.Location = new Point(w * width + 10, h + 75);
                    lb_dept.Size = new Size(width - 2, 23);
                    lb_dept.Text = obj.DeptName;
                    lb_dept.AutoEllipsis = true;
                    lb_dept.BackColor = Color.White;
                    lb_dept.TextAlign = ContentAlignment.MiddleCenter;

                    if (obj.DeptName.Contains('-'))
                    {
                        lb_dept.ForeColor = Color.Red;
                    }

                    this.CalPanel.Controls.Add(lb_dept);

                    // 医師
                    Label lb_doctor = new Label();
                    lb_doctor.Name = "Doctor" + obj.Date;
                    lb_doctor.AutoSize = false;
                    lb_doctor.Location = new Point(w * width + 10, h + 100);
                    lb_doctor.Size = new Size(width - 2, 23);
                    lb_doctor.Text = obj.DoctorName;
                    lb_doctor.AutoEllipsis = true;
                    lb_doctor.BackColor = Color.White;
                    lb_doctor.TextAlign = ContentAlignment.MiddleCenter;

                    if (obj.DoctorName.Contains('-'))
                    {
                        lb_doctor.ForeColor = Color.Red;
                    }

                    this.CalPanel.Controls.Add(lb_doctor);
                }
                else
                {
                    Label lb_none = new Label();
                    lb_none.AutoSize = false;
                    lb_none.Location = new Point(w * width + 10, h + 25);
                    lb_none.Size = new Size(width - 2, 98);
                    lb_none.BackColor = Color.White;

                    this.CalPanel.Controls.Add(lb_none);
                }

                if (w == 6) h += 125;

                d++;
            }
        }

        private void DatePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.CalShow();
        }

        private void PrevButton_Click(object sender, EventArgs e)
        {
            this.DatePicker1.Value = DateTime.Parse(this.DatePicker1.Value.ToString("yyyy/MM/01")).AddMonths(-1);
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            this.DatePicker1.Value = DateTime.Parse(this.DatePicker1.Value.ToString("yyyy/MM/01")).AddMonths(1);
        }

        private void InHistoryBox2_ValueChanged(object sender, EventArgs e)
        {
            PatIn pin = (PatIn)this.InHistoryBox2.PatIn1;

            if (!AppString.IsDate(pin.InDate))
            {
                return;
            }

            if (pin.Status == PatInStatus.Yet)
            {
                // 入院予定の場合は入院日
                this.DatePicker1.Value = pin.InDateValue;
            }
            else if (pin.Status == PatInStatus.Now)
            {
                // 入院中の場合は本日
                this.DatePicker1.Value = DateTime.Now;
            }
            else if (pin.Status == PatInStatus.Done)
            {
                // 退院済の場合は退院日
                this.DatePicker1.Value = pin.OutDateValue;
            }
        }

        private void CalPanel_SizeChanged(object sender, EventArgs e)
        {
            this.CalShow();
        }
    }

    class PatInCal
    {
        public string Date = "";

        public string Id = "";

        public int SEQ = 0;

        public string InDate = "";

        public string OutDate = "";

        /// <summary>
        /// 入院後何日目か
        /// </summary>
        public int Days
        {
            get
            {
                int i = 0;

                if (AppString.IsDate(this.InDate) && AppString.IsDate(this.Date))
                {
                    i = DateTimeAgent.IntervalDays(this.InDate, this.Date) + 1;
                }

                return i;
            }
        }

        public string WardName = "";

        public string WardNameLast
        {
            get
            {
                string s = "";

                if (this.WardName.Contains('-'))
                {
                    s = this.WardName.Substring(this.WardName.LastIndexOf('-') + 1);
                }
                else
                {
                    s = this.WardName;
                }

                return s;
            }
        }

        public string Room = "";

        public string RoomLast
        {
            get
            {
                string s = "";

                if (this.Room.Contains('-'))
                {
                    s = this.Room.Substring(this.Room.LastIndexOf('-') + 1);
                }
                else
                {
                    s = this.Room;
                }

                return s;
            }
        }

        public string DeptName = "";

        public string DeptNameLast
        {
            get
            {
                string s = "";

                if (this.DeptName.Contains('-'))
                {
                    s = this.DeptName.Substring(this.DeptName.LastIndexOf('-') + 1);
                }
                else
                {
                    s = this.DeptName;
                }

                return s;
            }
        }

        public string DoctorName = "";

        public string DoctorNameLast
        {
            get
            {
                string s = "";

                if (this.DoctorName.Contains('-'))
                {
                    s = this.DoctorName.Substring(this.DoctorName.LastIndexOf('-') + 1);
                }
                else
                {
                    s = this.DoctorName;
                }

                return s;
            }
        }
    }
}
