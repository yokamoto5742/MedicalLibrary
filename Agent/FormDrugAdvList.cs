using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Boundary;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class FormDrugAdvList : StdForm1
    {
        // 持参薬登録がある患者
        List<BringDrug> BringList = new List<BringDrug>();

        // コメントがある患者
        List<DrugAdvComment> CommentList = new List<DrugAdvComment>();

        /// <summary>
        /// 服薬指導算定
        /// </summary>
        struct AdvOrder
        {
            public string Pt_id;
            public string Order_date;
        }

        /// <summary>
        /// 服薬指導算定患者
        /// </summary>
        Dictionary<string, AdvOrder> AdvOrderDict = new Dictionary<string, AdvOrder>();

        DataSet drugSet = new DataSet();

        public FormDrugAdvList()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == MedicalLibrary.Utility.WinAPI.WM_COPYDATA)
            {
                // 文字列が送信されて来た
                WinAPI.COPYDATASTRUCT mystr = new WinAPI.COPYDATASTRUCT();
                Type mytype = mystr.GetType();
                mystr = (WinAPI.COPYDATASTRUCT)m.GetLParam(mytype);

                if (mystr.lpData.Split(' ').Length > 0)
                {
                    this.InitShow(mystr.lpData.Split(' '));
                }
            }

            base.WndProc(ref m);
        }

        private void FormDrugAdvList_Load(object sender, EventArgs e)
        {
            LibSettings.Init();

            StdReturn sr = DrugAdvSettings.Init();

            if (sr.ErrExist)
            {
                MessageBox.Show(sr.Err);
            }

            this.InitShow(Environment.GetCommandLineArgs());

            // この時点でログインされていなければ終了
            if (LoginUser.Status == LoginUser.STATUS.NONE)
            {
                this.Dispose();
            }
        }

        void InitShow(string[] args = null)
        {
            LoginUser.Init(true, args);
            LoginUsrLabel.Text = LoginUser.Name;

            int pat_id = 0;

            // Pat.csv を見る
            int.TryParse(PatBase.ReadPatCSV().Id, out pat_id);

            for (int i = 1; i < args.Length; i++)
            {
                if (args[i].Equals("-p", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (i < args.Length - 1 && int.TryParse(args[i + 1], out pat_id))
                    {
                        // 次のパラメータが数字ならば患者IDとみなす
                        i++;
                    }
                }
            }

            if (pat_id > 0)
            {
                FormDrugAdv.FormShow(pat_id.ToString());
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void FormDrugAdvList_Shown(object sender, EventArgs e)
        {
            ListMode1.Checked = true;
            IjiBox1.Checked = false;

            DataTable tmpTable = this.drugSet.Tables.Add("入院患者");
            tmpTable.Columns.Add("ID");
            tmpTable.Columns.Add("氏名");
            tmpTable.Columns.Add("性別");
            tmpTable.Columns.Add("年齢");
            tmpTable.Columns.Add("科");
            tmpTable.Columns.Add("医師");
            tmpTable.Columns.Add("入院日");
            tmpTable.Columns.Add("退院日");
            tmpTable.Columns.Add("病棟");
            tmpTable.Columns.Add("病室");
            tmpTable.Columns.Add("持参薬");
            tmpTable.Columns.Add("退院指導");
            tmpTable.Columns.Add("前回指導日");
            tmpTable.Columns.Add("前回指導者");
            tmpTable.Columns.Add("登録日");
            tmpTable.Columns.Add("登録者");
            tmpTable.Columns.Add("件名");
            tmpTable.Columns.Add("内容");
            tmpTable.Columns.Add("前回算定");
            tmpTable.Columns.Add("Obj", typeof(PatIn));

            this.ListShow1();

            tmpTable = this.drugSet.Tables.Add("服薬指導患者");
            tmpTable.Columns.Add("指導日");
            tmpTable.Columns.Add("ID");
            tmpTable.Columns.Add("氏名");
            tmpTable.Columns.Add("性別");
            tmpTable.Columns.Add("年齢");
            tmpTable.Columns.Add("入外");
            tmpTable.Columns.Add("入院日");
            tmpTable.Columns.Add("診療科");
            tmpTable.Columns.Add("退院指導");
            tmpTable.Columns.Add("担当者");
            tmpTable.Columns.Add("Obj", typeof(DrugAdv));

            this.yearBox.Items.Add("");

            for (int i = 2007; i <= DateTime.Now.Year + 1; i++)
            {
                this.yearBox.Items.Add(i.ToString());
            }

            this.yearBox.Text = DateTime.Now.Year.ToString();

            this.monthBox.Items.Add("");

            for (int i = 1; i <= 12; i++)
            {
                this.monthBox.Items.Add(i.ToString());
            }

            this.monthBox.Text = DateTime.Now.Month.ToString();

            this.ListShow2();

            this.WardBox.Items.Add("");
            this.WardBox.Items.Add("わかば");
            this.WardBox.Items.Add("さくら");
            this.WardBox.Items.Add("あやめ");
//            this.WardBox.Text = "";
        }

        /// <summary>
        /// 入退院患者一覧を取得する
        /// </summary>
        private void ListShow1()
        {
            // 入退院患者一覧
            List<PatIn> list = new List<PatIn>();

            if (ListMode2.Checked)
            {
                // 退院患者一覧
                list = PatIn.GetOutList();
                list.Sort((x, y) => y.OutDateInt - x.OutDateInt);
            }
            else
            {
                // 入院患者一覧
                list = PatIn.GetList();
                list.Sort((x, y) =>
                {
                    int i = 0;
                    int x1 = 0;
                    int y1 = 0;

                    int.TryParse(x.Ward, out x1);
                    int.TryParse(y.Ward, out y1);
                    i = x1 - y1;

                    if (i == 0)
                    {
                        int.TryParse(x.Room, out x1);
                        int.TryParse(y.Room, out y1);
                        i = x1 - y1;
                    }

                    return i;
                });
            }

            // 入院患者コードのリスト
            List<string> pt_list = new List<string>();

            foreach (PatIn p in list)
            {
                if (!pt_list.Contains(p.Id)) pt_list.Add(p.Id);
            }

            int critDate = int.Parse(DateTime.Now.ToString("yyyyMMdd"));

            // 服薬指導
            List<DrugAdv> list1 = DrugAdv.GetListByIds(pt_list, "PATIENT_ID, ADV_DATE desc, DRUG_ADV_ID desc");

            // 退院時服薬指導
            List<DrugAdvDischarge> list2 = DrugAdvDischarge.GetListByIds(pt_list, "PATIENT_ID, ADV_DATE desc, ID desc");

            // コメント
            List<DrugAdvComment> list3 = DrugAdvComment.GetListByIds(pt_list, "PATIENT_ID, SAVE_DATE desc, SAVE_TIME desc");

            // 持参薬
            List<BringDrug> list4 = BringDrug.GetListByIds(pt_list);

            // 患者ID・開始日 desc・終了日 desc の順で並べ替える
            list4.Sort((x, y) =>
            {
                int i = x.PtId.CompareTo(y.PtId);

                if (i == 0)
                {
                    i = y.StartDate - x.StartDate;
                }

                if (i == 0)
                {
                    i = y.EndDate - x.EndDate;
                }

                return i;
            });

            // 前回算定
            this.AdvOrderDict.Clear();

            if (this.IjiBox1.Checked)
            {
                List<string> adv_order_master_list = new List<string>();

                foreach (string s in DrugAdvSettings.Current.AdvOrderMasterCode.Split(','))
                {
                    adv_order_master_list.Add(s);
                }

                List<PatOrderDetail> adv_order_list = PatOrderDetail.FindByMasterCode(adv_order_master_list, DateTime.Now.AddMonths(-2).ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd"));

                foreach (PatOrderDetail order in adv_order_list)
                {
                    if (AdvOrderDict.ContainsKey(order.Pat.Id))
                    {
                        AdvOrder ao = AdvOrderDict[order.Pat.Id];

                        if (int.Parse(ao.Order_date) < int.Parse(order.SekouDate))
                        {
                            ao.Order_date = order.SekouDate;
                            AdvOrderDict[order.Pat.Id] = ao;
                        }
                    }
                    else
                    {
                        AdvOrder ao = new AdvOrder();
                        ao.Pt_id = order.Pat.Id;
                        ao.Order_date = order.SekouDate;

                        AdvOrderDict.Add(ao.Pt_id, ao);
                    }
                }
            }


            DataTable table = this.drugSet.Tables["入院患者"];
            table.Clear();

            foreach (PatIn p in list)
            {
                DataRow r = table.NewRow();

                r["ID"] = p.Id;

                r["氏名"] = p.Name;
                r["性別"] = p.Sex;
                r["年齢"] = p.AgeCalc(critDate.ToString());
                r["科"] = p.DeptName;
                r["医師"] = p.DoctorName;

                r["入院日"] = p.InDateStringShort;
                r["退院日"] = p.OutDateStringShort;
                r["病棟"] = p.WardName;
                r["病室"] = p.Room;

                // 服薬指導
                foreach (DrugAdv d in list1)
                {
                    if (!d.PtId.Equals(p.Id)) continue;
                    if (!d.Status.Equals(1) && !d.Status.Equals(2)) continue;
                    if (!d.AdmDate.ToString().Equals(p.InDate)) continue;

                    r["前回指導日"] = DateTimeAgent.DateFormat(d.AdvDate, DateTimeAgent.DateFormatKind.SHORT);
                    r["前回指導者"] = d.StaffName;
                    break;
                }

                // 退院指導
                foreach (DrugAdvDischarge d in list2)
                {
                    if (!d.PtId.Equals(p.Id)) continue;
                    if (!d.Status.Equals(1) && !d.Status.Equals(2)) continue;
                    if (!d.AdmDate.ToString().Equals(p.InDate)) continue;

                    if (d.Status.Equals(1))
                    {
                        r["退院指導"] = "●";
                    }
                    else if (d.Status.Equals(2))
                    {
                        r["退院指導"] = "○";
                    }
                }

                // 持参薬
                foreach (BringDrug d in list4)
                {
                    if (!d.PtId.Equals(p.Id)) continue;
                    if (d.DelFlg.Equals(1)) continue;
                    if (d.StartDate > critDate) continue;
                    if (d.EndDate < critDate) continue;

                    r["持参薬"] = "●";
                    break;
                }

                // コメント
                foreach (DrugAdvComment d in list3)
                {
                    if (!d.PtId.Equals(p.Id)) continue;
                    if (!d.Status.Equals(1)) continue;
//                    if (d.SaveDate < int.Parse(p.InDate)) continue;

                    r["登録日"] = DateTimeAgent.DateFormat(d.SaveDate, DateTimeAgent.DateFormatKind.SHORT);
                    r["登録者"] = d.StaffName;
                    r["件名"] = d.Title;
                    r["内容"] = d.Cont;
                    break;
                }

                if (this.IjiBox1.Checked)
                {
                    if (AdvOrderDict.ContainsKey(p.Id))
                    {
                        r["前回算定"] = DateTimeAgent.DateFormat(AdvOrderDict[p.Id].Order_date, DateTimeAgent.DateFormatKind.SHORT);
                    }
                }

                r["Obj"] = p;

                table.Rows.Add(r);
            }

            ListFormat1();
        }

        void ListFormat1()
        {
            DataView tmpView = new DataView(this.drugSet.Tables["入院患者"]);

            ListView1.DataSource = tmpView;

            string filter = "";

            if (this.WardBox.Text.Length > 0)
            {
                filter = "病棟 = '" + this.WardBox.Text + "'";
            }

            tmpView.RowFilter = filter;

            ListView1.Columns["ID"].Width = 60;
            ListView1.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            ListView1.Columns["氏名"].Width = 90;
            ListView1.Columns["性別"].Visible = false;

            ListView1.Columns["年齢"].Width = 35;
            ListView1.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView1.Columns["科"].Width = 65;
            ListView1.Columns["科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView1.Columns["医師"].Width = 75;
            ListView1.Columns["医師"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView1.Columns["入院日"].Width = 65;
            ListView1.Columns["入院日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView1.Columns["退院日"].Width = 65;
            ListView1.Columns["退院日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView1.Columns["病棟"].Width = 45;
            ListView1.Columns["病棟"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView1.Columns["病室"].Width = 40;
            ListView1.Columns["病室"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView1.Columns["持参薬"].Width = 35;
            ListView1.Columns["持参薬"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView1.Columns["退院指導"].Width = 35;
            ListView1.Columns["退院指導"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView1.Columns["退院指導"].Visible = true;

            ListView1.Columns["前回指導日"].Width = 65;
            ListView1.Columns["前回指導日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView1.Columns["前回指導者"].Width = 80;
            ListView1.Columns["前回指導者"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView1.Columns["登録日"].Width = 65;
            ListView1.Columns["登録日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView1.Columns["登録日"].HeaderText = "コメント";

            ListView1.Columns["登録者"].Width = 80;
            ListView1.Columns["登録者"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView1.Columns["登録者"].Visible = false;

            ListView1.Columns["件名"].Width = 80;
            ListView1.Columns["件名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView1.Columns["件名"].HeaderText = "分類";

            ListView1.Columns["内容"].Width = 120;
            ListView1.Columns["内容"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView1.Columns["内容"].HeaderText = "備考";

            if (this.AdvOrderDict.Count > 0)
            {
                ListView1.Columns["前回算定"].Visible = true;
                ListView1.Columns["前回算定"].Width = 65;
                ListView1.Columns["前回算定"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            else
            {
                ListView1.Columns["前回算定"].Visible = false;
            }

            ListView1.Columns["Obj"].Visible = false;

            foreach (DataGridViewRow r in ListView1.Rows)
            {
                if (r.Cells["性別"].Value.ToString().Equals("2"))
                {
                    r.Cells["氏名"].Style.ForeColor = Color.Red;
                }
            }
        }

        private void ShowButton1_Click(object sender, EventArgs e)
        {
            if (IjiBox1.Checked)
            {
                if (MessageBox.Show("前回算定表示は非常に重い処理ですので過去2ヶ月以内のみ表示します。\r\n数分かかる場合がありますが、表示しますか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    this.ListShow1();
                }
            }
            else
            {
                this.ListShow1();
            }
        }

        private void ShowButton2_Click(object sender, EventArgs e)
        {
            this.ListShow2();
        }

        private void ListShow2()
        {
            if (this.yearBox.Text.Length == 0 || this.monthBox.Text.Length == 0)
            {
                return;
            }

            string start_date = this.yearBox.Text + this.monthBox.Text.PadLeft(2, '0') + "01";
            string end_date = DateTime.Parse(start_date.Insert(4, "/").Insert(7, "/")).AddMonths(1).AddDays(-1).ToString("yyyyMMdd");

            List<DrugAdv> list1 = DrugAdv.GetListByDates(start_date, end_date, "ADV_DATE desc, DRUG_ADV_ID desc", true);

            // 対象患者のリスト
            List<string> pt_list = new List<string>();

            foreach (DrugAdv d in list1)
            {
                if (!pt_list.Contains(d.PtId)) pt_list.Add(d.PtId);
            }

            // 退院時服薬指導
            List<DrugAdvDischarge> list2 = DrugAdvDischarge.GetListByIds(pt_list, "PATIENT_ID, ADV_DATE desc, ID desc");

            DataTable table = this.drugSet.Tables["服薬指導患者"];
            table.Clear();

            foreach (DrugAdv d in list1)
            {
                // 削除されていれば飛ばす
                if (d.Status.Equals(0)) continue;

                DataRow r = table.NewRow();

                r["指導日"] = DateTimeAgent.DateFormat(d.AdvDate, DateTimeAgent.DateFormatKind.SHORT);
                r["ID"] = d.PtId;
                r["氏名"] = d.Pat.Name;
                r["性別"] = d.Pat.Sex;
                r["年齢"] = d.Pat.AgeCalc(d.AdvDate.ToString());
                r["入外"] = d.InOut.Equals(1) ? "外" : "入";
                r["入院日"] = DateTimeAgent.DateFormat(d.AdmDate, DateTimeAgent.DateFormatKind.SHORT);
                r["診療科"] = d.DeptName;

                // 退院指導
                foreach (DrugAdvDischarge d2 in list2)
                {
                    if (!d2.PtId.Equals(d.PtId)) continue;
                    if (!d2.Status.Equals(1) && !d.Status.Equals(2)) continue;
                    if (!d2.AdmDate.Equals(d.AdmDate)) continue;

                    if (d2.Status.Equals(1))
                    {
                        r["退院指導"] = "●";
                    }
                    else if (d2.Status.Equals(2))
                    {
                        r["退院指導"] = "○";
                    }

                    break;
                }

                r["担当者"] = d.StaffName;
                r["Obj"] = d;

                table.Rows.Add(r);
            }

            ListFormat2();
        }

        void ListFormat2()
        {
            DataView tmpView = new DataView(this.drugSet.Tables["服薬指導患者"]);

            ListView2.DataSource = tmpView;

            ListView2.Columns["指導日"].Width = 70;
            ListView2.Columns["指導日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView2.Columns["ID"].Width = 50;
            ListView2.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            ListView2.Columns["氏名"].Width = 85;
            ListView2.Columns["性別"].Visible = false;

            ListView2.Columns["年齢"].Width = 35;
            ListView2.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView2.Columns["入外"].Width = 35;
            ListView2.Columns["入外"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView2.Columns["入院日"].Width = 70;
            ListView2.Columns["入院日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListView2.Columns["入院日"].DefaultCellStyle.Format = "yy/MM/dd";

            ListView2.Columns["診療科"].Width = 65;
            ListView2.Columns["診療科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView2.Columns["退院指導"].Width = 35;
            ListView2.Columns["退院指導"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView2.Columns["担当者"].Width = 70;
            ListView2.Columns["担当者"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ListView2.Columns["Obj"].Visible = false;

            foreach (DataGridViewRow r in ListView2.Rows)
            {
                if (r.Cells["性別"].Value.ToString().Equals("2"))
                {
                    r.Cells["氏名"].Style.ForeColor = Color.Red;
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex.Equals(0))
            {
                ListFormat1();
            }
            else if (tabControl1.SelectedIndex.Equals(1))
            {
                ListFormat2();
            }
        }

        private void WardBox_TextChanged(object sender, EventArgs e)
        {
            this.ListFormat1();
        }

        /// <summary>
        /// 退院時服薬指導の内容を表示する
        /// </summary>
        private void DrugAdvShow2()
        {
            if (ListView2.SelectedRows.Count > 0)
            {
                DataGridViewRow tmpRow = ListView2.SelectedRows[0];
                DrugAdv d = (DrugAdv)(tmpRow.Cells["Obj"].Value);

                ptIdBox.Text = d.PtId;
                ptInfoBox.Text = d.Pat.GetInfo1(d.AdvDate.ToString(), AppDateTime.LANG.ENG, false);
                deptBox.Text = d.DeptName;
                insBox.Text = d.InsName;
                advDateBox.Text = DateTimeAgent.DateFormat(d.AdvDate, DateTimeAgent.DateFormatKind.LONG);
                admDateBox.Text = DateTimeAgent.DateFormat(d.AdmDate, DateTimeAgent.DateFormatKind.LONG);
                allergyBox1.Text = d.Allergy1;
                allergyBox2.Text = d.Allergy2;
                injectBox.Text = d.Inject;
                drugBox.Text = d.Drug;
                bringBox.Text = d.BringDrug;
                doubleBox.Text = d.DoubleDrug;
                tabooBox.Text = d.TabooMix;
                advBox.Text = d.Adv;
                staffBox.Text = d.StaffName;
            }
        }

        private void DrugAdvShow(string pt_id)
        {
            FormDrugAdv.FormShow(pt_id);
            /*
            FormDrugAdv fda1 = new FormDrugAdv(pt_id);
            fda1.Show();
             */
        }

        private void BringDrugShow(string pt_id)
        {
            Launcher.BringDrug(pt_id);
        }

        private void ListView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ListView1.SelectedRows.Count > 0)
            {
                FormDrugAdv.FormShow(ListView1.SelectedRows[0].Cells["ID"].Value.ToString());
//                this.DrugAdvShow(ListView1.SelectedRows[0].Cells["ID"].Value.ToString());
            }
        }

        private void ListView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ListView2.SelectedRows.Count > 0)
            {
                FormDrugAdv.FormShow(ListView2.SelectedRows[0].Cells["ID"].Value.ToString());
//                this.DrugAdvShow(ListView2.SelectedRows[0].Cells["ID"].Value.ToString());
            }
        }

        private void drugAdvToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ListView1.SelectedRows.Count > 0)
            {
                FormDrugAdv.FormShow(ListView1.SelectedRows[0].Cells["ID"].Value.ToString());
//                this.DrugAdvShow(ListView1.SelectedRows[0].Cells["ID"].Value.ToString());
            }
        }

        private void bringToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ListView1.SelectedRows.Count > 0)
            {
                this.BringDrugShow(ListView1.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void commentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ListView1.SelectedRows.Count > 0)
            {
                string pt_id = ListView1.SelectedRows[0].Cells[0].Value.ToString();

                FormDrugAdvComment f = new FormDrugAdvComment(pt_id);
                f.ShowDialog(this);
            }
        }

        private void drugAdvToolStripMenuItem0_Click(object sender, EventArgs e)
        {
            FormDrugAdv.FormShow("");
//            this.DrugAdvShow("");
        }

        private void bringToolStripMenuItem0_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                this.BringDrugShow(ListView1.SelectedRows[0].Cells[0].Value.ToString());
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                this.BringDrugShow(ListView2.SelectedRows[0].Cells[1].Value.ToString());
            }
        }

        private void tempToolStripMenuItem0_Click(object sender, EventArgs e)
        {
            FormDrugAdvTemplate fat1 = new FormDrugAdvTemplate();
            fat1.ShowDialog(this);
        }

        private void ListView2_SelectionChanged(object sender, EventArgs e)
        {
            this.DrugAdvShow2();
        }

        private void drugAdvToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (ListView2.SelectedRows.Count > 0)
            {
                FormDrugAdv.FormShow(ListView2.SelectedRows[0].Cells["ID"].Value.ToString());
//                this.DrugAdvShow(ListView2.SelectedRows[0].Cells["ID"].Value.ToString());
            }
        }

        private void bringToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (ListView2.SelectedRows.Count > 0)
            {
                this.BringDrugShow(ListView2.SelectedRows[0].Cells[1].Value.ToString());
            }
        }

        private void commentToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (ListView2.SelectedRows.Count > 0)
            {
                string pt_id = ListView2.SelectedRows[0].Cells[0].Value.ToString();

                FormDrugAdvComment f = new FormDrugAdvComment(pt_id);
                f.ShowDialog(this);
            }
        }

        private void LoginUsrLabel_Click(object sender, EventArgs e)
        {
            LoginPrompt lc = new LoginPrompt();
            lc.ShowDialog();
            this.LoginUsrLabel.Text = LoginUser.Name;

            // 職員バーコードを読む場合は LoginChange を使う
            /*
            LoginChange lc = new LoginChange();
            lc.ShowDialog();
            this.LoginUsrLabel.Text = LoginUser.Name;
             */
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}