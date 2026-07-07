using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class FormDPCCheck1 : Form
    {
        DataSet DSet = new DataSet();

        public FormDPCCheck1()
        {
            InitializeComponent();
        }

        private void FormDPCCheck1_Load(object sender, EventArgs e)
        {
            DataTable table = DSet.Tables.Add("List1");

            table.Columns.Add("病棟");
            table.Columns.Add("病室");
            table.Columns.Add("ID");
            table.Columns.Add("カナ");
            table.Columns.Add("氏名");
            table.Columns.Add("性別");
            table.Columns.Add("年齢", typeof(int));
            table.Columns.Add("科コード");
            table.Columns.Add("科");
            table.Columns.Add("医師コード");
            table.Columns.Add("医師");
            table.Columns.Add("入院日");
            table.Columns.Add("退院日");
            table.Columns.Add("サマリ");
            table.Columns.Add("DPC病名");
            table.Columns.Add("DPC病名更新日");
            table.Columns.Add("Obj", typeof(PatIn));

            // 入院後3日経ってもDPC病名がついていない
            table.Columns.Add("対象1");

            // 前日にDPC病名が更新された
            table.Columns.Add("対象2");


            table = DSet.Tables.Add("List2");

            table.Columns.Add("ID");
            table.Columns.Add("氏名");
            table.Columns.Add("性別");
            table.Columns.Add("生年月日");
            table.Columns.Add("年齢");
            table.Columns.Add("主");
            table.Columns.Add("コード");
            table.Columns.Add("病名");
            table.Columns.Add("入外");
            table.Columns.Add("科");
            table.Columns.Add("医師");
            table.Columns.Add("開始日");
            table.Columns.Add("転帰");
            table.Columns.Add("転帰日");
            table.Columns.Add("登録日");
            table.Columns.Add("退院日");
            table.Columns.Add("Obj", typeof(Diag));
            table.Columns.Add("Obj2", typeof(PatIn));


            this.DaysBox1.Items.Add("3");
            this.DaysBox1.Items.Add("7");
            this.DaysBox1.Items.Add("14");
            this.DaysBox1.Text = "7";

            this.DaysBox2.Items.Add("30");
            this.DaysBox2.Items.Add("60");
            this.DaysBox2.Items.Add("90");
            this.DaysBox2.Text = "60";

            this.DiagBox.Items.Add("2500013");
            this.DiagBox.Text = "2500013";

            this.InOutBox.Items.Add("");
            this.InOutBox.Items.Add("外来");
            this.InOutBox.Items.Add("入院");
            this.InOutBox.Text = "入院";

            this.DoubtBox.Checked = false;

            this.ListShow1();
            this.ListShow2();
        }

        void ListShow1()
        {
            if (!DSet.Tables.Contains("List1"))
            {
                return;
            }

            DataTable table = DSet.Tables["List1"];
            table.Rows.Clear();

            // 3日前
            int _3days = int.Parse(DateTime.Now.AddDays(-3).ToString("yyyyMMdd"));

            // 前日
            int yesterday = int.Parse(DateTime.Now.AddDays(-1).ToString("yyyyMMdd"));

            // 入院患者リスト
            List<PatIn> list = PatIn.GetList();

            // 退院患者リスト（デフォルト: 7日前～今日）
            int i = 7;
            int.TryParse(this.DaysBox1.Text, out i);
            list.AddRange(PatIn.GetOutList(DateTime.Now.AddDays(0 - i).ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd"), "", ""));

            // DPC病名リスト
            List<DiagDPC> diag_list = DiagDPC.GetList(list);

            // 退院サマリーリスト
            List<DischargeSummary> discharge_list = DischargeSummary.GetList(list);

            foreach (PatIn obj in list)
            {
                DataRow r = table.NewRow();

                r["病棟"] = obj.WardName;
                r["病室"] = obj.Room;
                r["ID"] = obj.Id;
                r["カナ"] = obj.Kana;
                r["氏名"] = obj.Name;
                r["性別"] = obj.SexNameShort;
                r["年齢"] = obj.Age;
                r["科コード"] = obj.Dept;
                r["科"] = obj.DeptName;
                r["医師コード"] = obj.Doctor;
                r["医師"] = obj.DoctorName;
                r["入院日"] = obj.InDateString;
                r["退院日"] = obj.OutDateString;
                r["Obj"] = obj;

                foreach (DischargeSummary d in discharge_list)
                {
                    if (d.PtId.Equals(obj.Id))
                    {
                        if (d.InputCheck.Equals(1))
                        {
                            r["サマリ"] = "▲";
                            break;
                        }
                        else if (d.InputCheck.Equals(2))
                        {
                            r["サマリ"] = "●";
                            break;
                        }
                    }
                }

                // DPC主病名がチェックされているか
                bool diag_flg = false;

                // 最終登録日時
                int diag_date = 0;

                foreach (DiagDPC d in diag_list)
                {
                    // 削除されていれば飛ばす
                    if (d.DeleteFlg) continue;

                    if (d.PtId.Equals(obj.Id))
                    {
                        // DPC主病名がチェックされている場合
                        if (d.MainFlg)
                        {
                            diag_flg = true;
                        }

                        // 最終登録日時
                        if (d.RegDate >= diag_date || d.UpDate >= diag_date)
                        {
                            diag_date = d.UpDate > d.RegDate ? d.UpDate : d.RegDate;
                        }
                    }
                }

                r["DPC病名"] = diag_flg ? "○" : "";
                r["DPC病名更新日"] = DateTimeAgent.DateFormat(diag_date, DateTimeAgent.DateFormatKind.LONG);

                // 入院後3日経ってもDPC病名がついていない
                r["対象1"] = obj.InDateInt <= _3days && !diag_flg ? "○" : "";

                // 前日にDPC病名が更新された
                r["対象2"] = diag_date.Equals(yesterday) ? "○" : "";

                table.Rows.Add(r);
            }

            this.ListFormat1();
        }

        void ListFormat1()
        {
            if (!DSet.Tables.Contains("List1"))
            {
                return;
            }

            DataTable table = DSet.Tables["List1"];

            DataView view = new DataView(table);

            view.Sort = "入院日, 退院日";

            this.ListView1.DataSource = view;

            ListView1.Columns["病棟"].HeaderText = "病棟";
            ListView1.Columns["病棟"].Width = 45;
            ListView1.Columns["病棟"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView1.Columns["病室"].HeaderText = "病室";
            ListView1.Columns["病室"].Width = 35;
            ListView1.Columns["病室"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView1.Columns["ID"].HeaderText = "ID";
            ListView1.Columns["ID"].Width = 55;
            ListView1.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            ListView1.Columns["カナ"].Width = 70;

            ListView1.Columns["氏名"].Width = 90;

            ListView1.Columns["性別"].HeaderText = "性別";
            ListView1.Columns["性別"].Width = 35;
            ListView1.Columns["性別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView1.Columns["年齢"].HeaderText = "年齢";
            ListView1.Columns["年齢"].Width = 35;
            ListView1.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView1.Columns["科コード"].Visible = false;

            ListView1.Columns["科"].HeaderText = "科";
            ListView1.Columns["科"].Width = 50;
            ListView1.Columns["科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            ListView1.Columns["医師コード"].Visible = false;

            ListView1.Columns["医師"].HeaderText = "医師";
            ListView1.Columns["医師"].Width = 70;
            ListView1.Columns["医師"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            ListView1.Columns["入院日"].HeaderText = "入院日";
            ListView1.Columns["入院日"].Width = 70;
            ListView1.Columns["入院日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView1.Columns["退院日"].HeaderText = "退院日";
            ListView1.Columns["退院日"].Width = 70;
            ListView1.Columns["退院日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView1.Columns["サマリ"].Width = 35;
            ListView1.Columns["サマリ"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView1.Columns["サマリ"].ToolTipText = "▲ 医師, ● 管理士";

            ListView1.Columns["DPC病名"].Width = 35;
            ListView1.Columns["DPC病名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView1.Columns["DPC病名"].ToolTipText = "○ DPC主病名あり";

            ListView1.Columns["DPC病名更新日"].Width = 70;
            ListView1.Columns["DPC病名更新日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView1.Columns["対象1"].Width = 35;
            ListView1.Columns["対象1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView1.Columns["対象2"].Width = 35;
            ListView1.Columns["対象2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView1.Columns["Obj"].Visible = false;
        }

        void ListShow2()
        {
            if (!DSet.Tables.Contains("List2"))
            {
                return;
            }

            if (this.DiagBox.Text.Length == 0)
            {
                return;
            }

            DataTable table = DSet.Tables["List2"];
            table.Rows.Clear();

            // 入院患者リスト
            List<PatIn> pt_list = PatIn.GetList();

            // 退院患者リスト（デフォルト: 60日前～今日）
            int i = 60;
            int.TryParse(this.DaysBox2.Text, out i);
            pt_list.AddRange(PatIn.GetOutList(DateTime.Now.AddDays(0 - i).ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd"), "", ""));

            List<string> diag_list = new List<string>();
            diag_list.Add(this.DiagBox.Text);

            int date1 = int.Parse(DateTime.Now.AddYears(-1).ToString("yyyyMMdd"));
            int date2 = int.Parse(DateTime.Now.ToString("yyyyMMdd"));

            List<Diag> list = Diag.GetListByDiagNamesDates(diag_list, date1, date2, DoubtBox.Checked, true, 2, pt_list.ConvertAll((x) => { return x.Id; }));

            foreach (Diag d in list)
            {
                // 削除されたものは飛ばす
                if (d.DeleteFlg) continue;

                // 未確定のものは飛ばす
                if (!d.FixFlg) continue;

                if (this.InOutBox.Text.Equals("外来"))
                {
                    // 外来病名でなければ飛ばす
                    if (!d.InOut.Equals("1"))
                    {
                        continue;
                    }
                }
                else if (this.InOutBox.Text.Equals("入院"))
                {
                    // 入院病名でなければ飛ばす
                    if (!d.InOut.Equals("2"))
                    {
                        continue;
                    }
                }

                DataRow r = table.NewRow();

                r["ID"] = d.Pat.Id;
                r["氏名"] = d.Pat.Name;
                r["性別"] = d.Pat.SexNameShort;
                r["生年月日"] = d.Pat.BirthString;
                r["年齢"] = d.Pat.AgeCalc(d.StartDate.ToString());
                r["主"] = d.MainFlg ? "●" : "";
                r["コード"] = d.DiagCode;
                r["病名"] = d.DiagName;
                r["入外"] = d.InOutStringShort;
                r["科"] = d.DeptName;
                r["医師"] = d.DoctorName;
                r["開始日"] = DateTimeAgent.DateFormat(d.StartDate, DateTimeAgent.DateFormatKind.SHORT);
                r["転帰"] = d.OutcomeString;
                r["転帰日"] = DateTimeAgent.DateFormat(d.OutcomeDate, DateTimeAgent.DateFormatKind.SHORT);
                r["登録日"] = DateTimeAgent.DateFormat(d.RegDate, DateTimeAgent.DateFormatKind.SHORT);

                foreach (PatIn pin in pt_list)
                {
                    if (pin.Id.Equals(d.Pat.Id))
                    {
                        r["退院日"] = pin.OutDateStringShort;
                        r["Obj2"] = pin;
                        break;
                    }
                }

                r["Obj"] = d;

                table.Rows.Add(r);
            }

            this.ListFormat2();
        }

        void ListFormat2()
        {
            if (!DSet.Tables.Contains("List2"))
            {
                return;
            }

            DataTable table = DSet.Tables["List2"];
            DataView view = new DataView(table);

            this.ListView2.DataSource = view;

            ListView2.Columns["ID"].Width = 65;
            ListView2.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            ListView2.Columns["氏名"].Width = 80;
            ListView2.Columns["氏名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            ListView2.Columns["性別"].Visible = false;
            ListView2.Columns["性別"].Width = 30;
            ListView2.Columns["性別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView2.Columns["生年月日"].Visible = false;
            ListView2.Columns["生年月日"].Width = 75;
            ListView2.Columns["生年月日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView2.Columns["年齢"].Width = 35;
            ListView2.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView2.Columns["主"].Width = 30;
            ListView2.Columns["主"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView2.Columns["コード"].Width = 55;
            ListView2.Columns["コード"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView2.Columns["病名"].Width = 120;
            ListView2.Columns["病名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            ListView2.Columns["入外"].Width = 30;
            ListView2.Columns["入外"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView2.Columns["科"].Width = 60;
            ListView2.Columns["科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            ListView2.Columns["医師"].Width = 75;
            ListView2.Columns["医師"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            ListView2.Columns["開始日"].Width = 60;
            ListView2.Columns["開始日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView2.Columns["転帰"].Width = 40;
            ListView2.Columns["転帰"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView2.Columns["転帰日"].Width = 60;
            ListView2.Columns["転帰日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView2.Columns["登録日"].Width = 60;
            ListView2.Columns["登録日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView2.Columns["退院日"].Width = 60;
            ListView2.Columns["退院日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView2.Columns["Obj"].Visible = false;
            ListView2.Columns["Obj2"].Visible = false;
        }

        private void DaysBox1_TextChanged(object sender, EventArgs e)
        {
            int i = 0;

            if (!int.TryParse(this.DaysBox1.Text, out i) || i < 0 || i > 14)
            {
                this.DaysBox1.Text = "";
                return;
            }
        }

        private void DaysBox2_TextChanged(object sender, EventArgs e)
        {
            int i = 0;

            if (!int.TryParse(this.DaysBox2.Text, out i) || i < 0 || i > 180)
            {
                this.DaysBox2.Text = "";
                return;
            }
        }

        private void FormDPCCheck1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                this.ListShow1();
                this.ListShow2();
            }
        }

        private void ShowButton1_Click(object sender, EventArgs e)
        {
            this.ListShow1();
        }

        private void ShowButton2_Click(object sender, EventArgs e)
        {
            this.ListShow2();
        }

        int SendMessage1()
        {
            int i = 0;

            // 担当者に送るメッセージ
            string msg = "";

            foreach (DataGridViewRow r in this.ListView1.Rows)
            {
                if (r.Cells["対象1"].Value.ToString().Equals("○"))
                {
                    PatIn obj = (PatIn)r.Cells["Obj"].Value;

                    foreach (Staff staff in Staff.GetListByDoctorCode(obj.Doctor))
                    {
                        // 医師でない場合は飛ばす
                        if (!staff.QualCode.Equals(1))
                        {
                            continue;
                        }

                        KarteMessage m1 = new KarteMessage();
                        m1.FromCode = LibSettings.Current.DPCMsg1.From1;
                        m1.ToCode = staff.Code.ToString();
                        m1.Title = "【自動送信】DPC関連入力のお願い";
                        m1.PtId = obj.Id;

                        if (obj.Status == PatInStatus.Done && obj.OutDateInt > 0)
                        {
                            m1.Msg = obj.InDateString + " に入院され、" + obj.OutDateString + " に退院されました。DPC病名の入力をお願いいたします。";
                        }
                        else
                        {
                            m1.Msg = obj.InDateString + " に入院されました。DPC病名の入力をお願いいたします。";
                        }

                        m1.Send();
                        i++;

                        Thread.Sleep(2000);
                    }

                    msg += obj.Id.PadLeft(6, ' ') + "  ";
                    msg += obj.Name.Replace("　", " ").Replace(" ", "").PadRight(7, '　') + " ";
                    msg += obj.DeptName.PadRight(6, '　') + " ";
                    msg += obj.DoctorName.Replace("　", " ").Replace(" ", "").PadRight(6, '　') + " ";
                    msg += obj.InDateStringShort;

                    if (obj.OutDateInt > 0)
                    {
                        msg += "  " + obj.OutDateStringShort;
                    }

                    msg += Environment.NewLine;
                }
            }


            if (msg.Length > 0)
            {
                KarteMessage m2 = new KarteMessage();
                m2.FromCode = LibSettings.Current.DPCMsg1.From2;
                m2.Title = "【自動送信】DPC関連入力のお願い";
                m2.PtId = "0";

                m2.Msg = "以下の通り、DPC病名入力を依頼しました。" + Environment.NewLine + Environment.NewLine;
                m2.Msg += "ID      氏名           科           医師         入院日    退院日" + Environment.NewLine;
                m2.Msg += "-------------------------------------------------------------------" + Environment.NewLine;
                m2.Msg += msg;

                foreach (string to in LibSettings.Current.DPCMsg1.ToList2)
                {
                    m2.ToCode = to;
                    m2.Send();

                    Thread.Sleep(2000);
                }
            }

            return i;
        }

        int SendMessage2()
        {
            int i = 0;

            foreach (DataGridViewRow r in this.ListView1.Rows)
            {
                if (r.Cells["対象2"].Value.ToString().Equals("○"))
                {
                    PatIn obj = (PatIn)r.Cells["Obj"].Value;

                    foreach (DPCMsg2 d in LibSettings.Current.DPCMsgList2)
                    {
                        // 病棟が異なる場合は飛ばす
                        if (!d.Ward.Equals(obj.Ward))
                        {
                            continue;
                        }

                        foreach (string to in d.ToList)
                        {
                            KarteMessage m1 = new KarteMessage();
                            m1.FromCode = d.From;
                            m1.ToCode = to;
                            m1.Title = "【自動送信】病名が昨日変更されました";
                            m1.PtId = obj.Id;
                            m1.Msg = "【自動送信】病名が昨日変更されました。";

                            m1.Send();
                            i++;

                            Thread.Sleep(2000);
                        }
                    }
                }
            }

            return i;
        }

        int SendMessage3()
        {
            int i = 0;

            // 担当者に送るメッセージ
            string msg = "";

            foreach (DataGridViewRow r in this.ListView2.Rows)
            {
                Diag obj = (Diag)r.Cells["Obj"].Value;
                PatIn obj2 = (PatIn)r.Cells["Obj2"].Value;

                msg += obj.Pat.Id.PadLeft(6, ' ') + "  ";
                msg += obj.Pat.Name.Replace("　", " ").Replace(" ", "").PadRight(7, '　') + " ";
                msg += obj.DeptName.PadRight(6, '　') + " ";
                msg += obj.DoctorName.Replace("　", " ").Replace(" ", "").PadRight(6, '　') + " ";
                msg += obj.StartDateValue.ToString("yy/MM/dd");

                if (AppString.IsDate(obj.OutcomeDate))
                {
                    msg += "  " + obj.OutcomeDateValue.ToString("yy/MM/dd");
                }
                else
                {
                    msg += "          ";
                }

                msg += DateTimeAgent.DateFormat(obj.RegDate, DateTimeAgent.DateFormatKind.SHORT).PadLeft(10, ' ');
                msg += DateTimeAgent.DateFormat(obj2.OutDate, DateTimeAgent.DateFormatKind.SHORT).PadLeft(10, ' ');

                msg += Environment.NewLine;
            }

            if (msg.Length > 0)
            {
                KarteMessage m3 = new KarteMessage();
                m3.FromCode = LibSettings.Current.DPCMsg3.From;
                m3.Title = "【自動送信】糖尿病コード変更のお願い";
                m3.PtId = "0";

                m3.Msg = "【自動送信】糖尿病コード変更のお願い" + Environment.NewLine + Environment.NewLine;
                m3.Msg += "ID      氏名           科           医師         開始日    転帰日    登録日    退院日" + Environment.NewLine;
                m3.Msg += "---------------------------------------------------------------------------------------" + Environment.NewLine;
                m3.Msg += msg;

                foreach (string to in LibSettings.Current.DPCMsg3.ToList)
                {
                    m3.ToCode = to;
                    m3.Send();
                    i++;

                    Thread.Sleep(2000);
                }
            }

            return i;
        }

        private void SendButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("DPC病名がチェックされていない対象者のメッセージを送信しますか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            // 送信前に更新しておく
            this.ListShow1();

            int i = this.SendMessage1();

            MessageBox.Show(i + " 件のメッセージを送信しました");
        }

        private void SendButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("DPC病名チェックが変更された対象者のメッセージを送信しますか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            // 送信前に更新しておく
            this.ListShow1();

            int i = this.SendMessage2();

            MessageBox.Show(i + " 件のメッセージを送信しました");
        }

        private void SendButton3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("病名検索の対象者のメッセージを送信しますか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            // 送信前に更新しておく
            this.ListShow2();

            int i = this.SendMessage3();

            MessageBox.Show(i + " 件のメッセージを送信しました");
        }

        public int SendMessage()
        {
            // 送信前に更新しておく
            this.ListShow1();
            this.ListShow2();

            int i = 0;

            i += this.SendMessage1();
            i += this.SendMessage2();
            i += this.SendMessage3();

            return i;
        }
    }
}
