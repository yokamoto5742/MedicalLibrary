using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MedicalLibrary.Boundary;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class FormComeReportList : StdForm1
    {
        DataSet DSet = new DataSet();

        FormComeReportOutSide formOutSide;

        /// <summary>
        /// COME_REPORT のステータス
        /// </summary>
        class ReportStatus
        {
            public string OrderId = "";
            public string OutSide = "";
            public string Record = "";
            public string Status = "";
        }

        public FormComeReportList()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WinAPI.WM_COPYDATA)
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

        private void FormComeReportList_Load(object sender, EventArgs e)
        {
            try
            {
                LibSettings.Init();
                ComeReportSettings.Init();

                this.InitShow(Environment.GetCommandLineArgs());

                if (LoginUser.Status == LoginUser.STATUS.NONE)
                {
                    this.Dispose();
                }

                ComeReportSettings.InitUser(LoginUser.Id, LoginUser.DoctorId, LoginUser.QualId);

                // Form1 のオーダー一覧に使用するテーブル
                DataTable tmpTable = DSet.Tables.Add("オーダー");
                tmpTable.Columns.Add("オーダー番号");
                tmpTable.Columns.Add("施行部署１");
                tmpTable.Columns.Add("種別");
                tmpTable.Columns.Add("施行日");
                tmpTable.Columns.Add("ID");
                tmpTable.Columns.Add("氏名");
                tmpTable.Columns.Add("年齢");
                tmpTable.Columns.Add("性別");
                tmpTable.Columns.Add("入外区分");
                tmpTable.Columns.Add("入外");
                tmpTable.Columns.Add("内容");
                tmpTable.Columns.Add("科コード");
                tmpTable.Columns.Add("診療科");
                tmpTable.Columns.Add("指示医コード");
                tmpTable.Columns.Add("指示医");
                tmpTable.Columns.Add("施行");
                tmpTable.Columns.Add("院外");
                tmpTable.Columns.Add("記録");
                tmpTable.Columns.Add("完成");


                foreach (string s in ComeReportSettings.Current.GroupDict.Keys)
                {
                    this.groupBox.Items.Add(s + " " + ComeReportSettings.Current.GroupDict[s].Name);
                }

                foreach (string g in this.groupBox.Items)
                {
                    if (g.Contains(" ") && g.Split(' ')[0] == ComeReportSettings.Current.MyGroup)
                    {
                        this.groupBox.SelectedItem = g;
                    }
                }

                // kensaBox の初期設定
                this.kensaBox.Items.Clear();

                foreach (string s in ComeReportSettings.Current.GroupDict[ComeReportSettings.Current.MyGroup].Kensa.Split(','))
                {
                    if (ComeReportSettings.Current.KensaDict.ContainsKey(s))
                    {
                        if (ComeReportSettings.Current.MyKensaList.Contains(s))
                        {
                            this.kensaBox.Items.Add(s + " " + ComeReportSettings.Current.KensaDict[s].Name, true);
                        }
                        else
                        {
                            this.kensaBox.Items.Add(s + " " + ComeReportSettings.Current.KensaDict[s].Name, false);
                        }
                    }
                }

                // partBox の初期設定
                this.partBox.Items.Clear();
                this.keywordBox.Text = "";

                if (ComeReportSettings.Current.GroupDict[ComeReportSettings.Current.MyGroup].Part.Length > 0)
                {
                    if (ComeReportSettings.Current.GroupDict[ComeReportSettings.Current.MyGroup].Part.Contains(","))
                    {
                        foreach (string s in ComeReportSettings.Current.GroupDict[ComeReportSettings.Current.MyGroup].Part.Split(','))
                        {
                            if (ComeReportSettings.Current.MyPartList.Contains(s))
                            {
                                this.partBox.Items.Add(s, true);
                            }
                            else
                            {
                                this.partBox.Items.Add(s, false);
                            }
                        }
                    }
                    else
                    {
                        if (ComeReportSettings.Current.MyPartList.Contains(ComeReportSettings.Current.GroupDict[ComeReportSettings.Current.MyGroup].Part))
                        {
                            this.partBox.Items.Add(ComeReportSettings.Current.GroupDict[ComeReportSettings.Current.MyGroup].Part, true);
                        }
                        else
                        {
                            this.partBox.Items.Add(ComeReportSettings.Current.GroupDict[ComeReportSettings.Current.MyGroup].Part, false);
                        }
                    }
                }

                deptBox.Items.Add("");

                foreach (string k in Dict.DeptDict.Keys)
                {
                    deptBox.Items.Add(k + " " + Dict.DeptDict[k].ShortName);
                }

                doctorBox.Items.Add("");

                foreach (string k in Dict.DoctorDict.Keys)
                {
                    doctorBox.Items.Add(k + " " + Dict.DoctorDict[k].Name);
                }

                // 初期表示期間を設定する
                int showDays = -3;

                if (int.TryParse("-" + ComeReportSettings.Current.MyShowDays, out showDays))
                {
                    showDays = int.Parse("-" + ComeReportSettings.Current.MyShowDays);
                }

                this.kensaDate1.Value = DateTime.Now.AddDays(showDays + 1);

                // 「院内読影」にチェックを入れておく
                this.inBox.Checked = true;

                // 「院外読影」にはチェックを入れない
                this.outBox.Checked = false;

                // 「未完成のみ」にチェックを入れておく
                this.unBox.Checked = true;

                this.makeTable1();
                this.filterView1();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Dispose();
            }
        }

        /// <summary>
        /// 初期処理
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
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
                FormComeReportPat.FormShow(pat_id.ToString());
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void showButton1_Click(object sender, EventArgs e)
        {
            if (kensaDate2.Value.Subtract(kensaDate1.Value).Days >= 15)
            {
                MessageBox.Show("検索が重くなるため１５日間以上は表示できません。\r\n対象期間は１４日間以下にしてください。");
            }
            else
            {
                this.ListShow();
            }
        }

        // オーダー一覧を更新する
        public void ListShow()
        {
            // １５日以上の期間がある場合は１４日に設定する
            if (kensaDate2.Value.Subtract(kensaDate1.Value).Days >= 15)
            {
                kensaDate2.Value = kensaDate1.Value.AddDays(14);
            }

            this.makeTable1();

            this.filterView1();
        }

        private void makeTable1()
        {
            DataTable tmpTable = DSet.Tables["オーダー"];
            tmpTable.Clear();

            List<ComeReportOrder> list = ComeReportOrder.GetList2(kensaDate1.Value.ToString("yyyyMMdd"), kensaDate2.Value.ToString("yyyyMMdd"), ComeReportSettings.Current.GroupDict["0"].Kensa.Split(','));

            foreach (ComeReportOrder obj in list)
            {
                DataRow r = tmpTable.NewRow();

                r["オーダー番号"] = obj.OrderId;
                r["施行部署１"] = obj.Sekou1;
                r["種別"] = obj.SekouName1;
                r["施行日"] = DateTimeAgent.DateFormat(obj.SekouDate, DateTimeAgent.DateFormatKind.LONG);
                r["ID"] = obj.Pat.Id;
                r["氏名"] = obj.Pat.Name;
                r["年齢"] = obj.Pat.AgeCalc(obj.SekouDate);
                r["性別"] = obj.Pat.Sex;
                r["入外区分"] = obj.InOut;
                r["入外"] = obj.InOutName;
                r["内容"] = obj.SOAP;
                r["科コード"] = obj.Dept;
                r["診療科"] = obj.DeptName;
                r["指示医コード"] = obj.Doctor;
                r["指示医"] = obj.DoctorName;
                r["施行"] = obj.SekouFlg.Equals("1") ? "○" : "";
                r["院外"] = obj.OutSideFlg ? "○" : "";
                r["記録"] = obj.RecordFlg ? "○" : "";
                r["完成"] = obj.ReportListString;

                tmpTable.Rows.Add(r);
            }
        }

        private void filterView1()
        {
            DataTable table = DSet.Tables["オーダー"];

            if (table.Rows.Count == 0)
            {
                return;
            }

            DataView tmpView = new DataView(table);

            string kensaFilter = "";

            for (int i = 0; i < kensaBox.CheckedItems.Count; i++)
            {
                if (kensaFilter.Length > 0)
                {
                    kensaFilter += "," + kensaBox.CheckedItems[i].ToString().Split(' ')[0];
                }
                else
                {
                    kensaFilter = kensaBox.CheckedItems[i].ToString().Split(' ')[0];
                }
            }

            if (kensaFilter.Length > 0)
            {
                tmpView.RowFilter = "施行部署１ in (" + kensaFilter + ")";
            }
            else
            {
                tmpView.RowFilter = "施行部署１ in (" + ComeReportSettings.Current.GroupDict[ComeReportSettings.Current.MyGroup].Kensa + ")";
            }

            if (this.deptBox.Text.Contains(" "))
            {
                tmpView.RowFilter += " and 科コード = " + this.deptBox.Text.Split(' ')[0];
            }

            if (this.doctorBox.Text.Contains(" "))
            {
                tmpView.RowFilter += " and 指示医コード = " + this.doctorBox.Text.Split(' ')[0];
            }

            // SOAP表示名称の検索
            string partFilter1 = "";

            for (int i = 0; i < partBox.CheckedItems.Count; i++)
            {
                if (partFilter1.Length > 0)
                {
                    partFilter1 += " or (内容 like '%" + partBox.CheckedItems[i].ToString() + "%')";
                }
                else
                {
                    partFilter1 = "(内容 like '%" + partBox.CheckedItems[i].ToString() + "%')";
                }
            }

            if (keywordBox.Text.Length > 0)
            {
                if (keywordBox.Text.Contains(" ") || keywordBox.Text.Contains("　"))
                {
                    foreach (string s in keywordBox.Text.Split(' ', '　'))
                    {
                        if (partFilter1.Length > 0)
                        {
                            partFilter1 += " or (内容 like '%" + s + "%')";
                        }
                        else
                        {
                            partFilter1 = "(内容 like '%" + s + "%')";
                        }
                    }
                }
                else
                {
                    if (partFilter1.Length > 0)
                    {
                        partFilter1 += " or (内容 like '%" + keywordBox.Text + "%')";
                    }
                    else
                    {
                        partFilter1 = "(内容 like '%" + keywordBox.Text + "%')";
                    }
                }
            }

            if (partFilter1.Length > 0)
            {
                tmpView.RowFilter += " and (" + partFilter1 + ")";
            }

            if (inBox.Checked && !outBox.Checked)
            {
                tmpView.RowFilter += " and 院外 <> '○'";
            }
            else if (!inBox.Checked && outBox.Checked)
            {
                tmpView.RowFilter += " and 院外 = '○'";
            }
            else if (!inBox.Checked && !outBox.Checked)
            {
                tmpView.RowFilter += " and 院外 <> '○' and 院外 = '○'";
            }

            if (unBox.Checked)
            {
                tmpView.RowFilter += " and (完成 = '' or 完成 like '%△%')";
            }

            this.gridView1.DataSource = tmpView;

            gridView1.Columns["オーダー番号"].Visible = false;

            gridView1.Columns["施行部署１"].Visible = false;

            gridView1.Columns["種別"].Width = 65;
            gridView1.Columns["種別"].DefaultCellStyle.Font = new Font("MS UI Gothic", 9);

            gridView1.Columns["施行日"].Width = 70;
            gridView1.Columns["施行日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridView1.Columns["ID"].Width = 55;
            gridView1.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            gridView1.Columns["氏名"].Width = 80;
            gridView1.Columns["氏名"].DefaultCellStyle.Font = new Font("MS UI Gothic", 9);

            gridView1.Columns["年齢"].Width = 35;
            gridView1.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridView1.Columns["性別"].Visible = false;

            gridView1.Columns["入外区分"].Visible = false;

            gridView1.Columns["入外"].Width = 35;
            gridView1.Columns["入外"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridView1.Columns["内容"].Width = 270;
            gridView1.Columns["内容"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            gridView1.Columns["科コード"].Visible = false;

            gridView1.Columns["診療科"].Width = 60;
            gridView1.Columns["診療科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridView1.Columns["指示医コード"].Visible = false;

            gridView1.Columns["指示医"].Width = 70;

            gridView1.Columns["施行"].Width = 35;
            gridView1.Columns["施行"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridView1.Columns["院外"].Width = 35;
            gridView1.Columns["院外"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridView1.Columns["記録"].Width = 35;
            gridView1.Columns["記録"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridView1.Columns["完成"].Width = 105;
            gridView1.Columns["完成"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewRow r in gridView1.Rows)
            {
                if (r.Cells["性別"].Value.ToString().Equals("2"))
                {
                    r.Cells["氏名"].Style.ForeColor = Color.Red;
                }
            }
        }

        /// <summary>
        /// オーダーまたは所見一覧画面にフィルタをかける。
        /// DBは見に行かず、既に取得されているデータにフィルタをかける。
        /// </summary>
        public void filterView()
        {
            this.filterView1();
        }

        private void groupBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (groupBox.Text.Contains(" "))
            {
                ComeReportSettings.Current.MyGroup = groupBox.Text.Split(' ')[0];

                this.kensaBox.Items.Clear();

                foreach (string s in ComeReportSettings.Current.GroupDict[ComeReportSettings.Current.MyGroup].Kensa.Split(','))
                {
                    if (ComeReportSettings.Current.KensaDict.ContainsKey(s))
                    {
                        this.kensaBox.Items.Add(s + " " + ComeReportSettings.Current.KensaDict[s].Name);
                    }
                }

                this.partBox.Items.Clear();
                this.keywordBox.Text = "";

                if (ComeReportSettings.Current.GroupDict[ComeReportSettings.Current.MyGroup].Part.Length > 0)
                {
                    if (ComeReportSettings.Current.GroupDict[ComeReportSettings.Current.MyGroup].Part.Contains(","))
                    {
                        foreach (string s in ComeReportSettings.Current.GroupDict[ComeReportSettings.Current.MyGroup].Part.Split(','))
                        {
                            this.partBox.Items.Add(s);
                        }
                    }
                    else
                    {
                        this.partBox.Items.Add(ComeReportSettings.Current.GroupDict[ComeReportSettings.Current.MyGroup].Part);
                    }
                }

                this.filterView();
            }
        }

        private void FileReportMenuItem_Click(object sender, EventArgs e)
        {
            FormComeReportPat.FormShow("");
        }

        private void gridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridView1.SelectedRows.Count > 0)
            {
                FormComeReportPat.FormShow(gridView1.SelectedRows[0].Cells["ID"].Value.ToString(), gridView1.SelectedRows[0].Cells["オーダー番号"].Value.ToString());
                /*
                FormPat fp1 = new FormPat(gridView1.SelectedRows[0].Cells["ID"].Value.ToString(), gridView1.SelectedRows[0].Cells["オーダー番号"].Value.ToString());
                fp1.Show();
                 */
            }
        }

        private void OpenReportMenuItem_Click(object sender, EventArgs e)
        {
            string pt_id = "";

            if (gridView1.SelectedRows.Count > 0)
            {
                pt_id = gridView1.SelectedRows[0].Cells[4].Value.ToString();
            }

            if (pt_id.Length > 0)
            {
                FormComeReportPat.FormShow(pt_id);
                /*
                FormPat fp1 = new FormPat(pt_id);
                fp1.Show();
                 */
            }
        }

        private void deptBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (deptBox.Text.Contains(" "))
            {
                this.filterView();
            }
        }

        private void doctorBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (doctorBox.Text.Contains(" "))
            {
                this.filterView();
            }
        }

        private void kensaBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.filterView();
        }

        private void partBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.filterView();
        }

        private void keywordBox_TextChanged(object sender, EventArgs e)
        {
            this.filterView();
        }

        private void inBox_CheckedChanged(object sender, EventArgs e)
        {
            this.filterView();
        }

        private void outBox_CheckedChanged(object sender, EventArgs e)
        {
            this.filterView();
        }

        private void unBox_CheckedChanged(object sender, EventArgs e)
        {
            this.filterView();
        }

        private void ToolMenu_Click(object sender, EventArgs e)
        {
            if (LoginUser.QualId.Equals("1") || ComeReportSettings.Current.AchieveStaffList.Contains(LoginUser.Id))
            {
                ToolAchieveMenuItem.Enabled = true;
            }
            else
            {
                ToolAchieveMenuItem.Enabled = false;
            }
        }

        private void ToolOutSideMenuItem_Click(object sender, EventArgs e)
        {
            if (formOutSide == null || !formOutSide.Created)
            {
                formOutSide = new FormComeReportOutSide(this);
            }

            LibUtility.FormShow(formOutSide);

//            FormControl.FormOutSide_Show();
        }

        private void ToolAchieveMenuItem_Click(object sender, EventArgs e)
        {
            FormComeReportAchieve f = new FormComeReportAchieve();
            f.ShowDialog(this);

//            FormControl.FormAchieve_Show();
        }

        private void HelpManualMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string file = ComeReportSettings.Current.Manual;

                if (File.Exists(file))
                {
                    Launcher.Browser(file);
                }
                else
                {
                    MessageBox.Show("ファイル" + file + "がありません");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void FileExitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoginUsrLabel_DoubleClick(object sender, EventArgs e)
        {
            LoginChange lc = new LoginChange();
            lc.ShowDialog();
            this.LoginUsrLabel.Text = LoginUser.Name;
        }
    }
}