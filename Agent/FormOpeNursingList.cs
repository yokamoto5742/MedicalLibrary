using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using MedicalLibrary.Boundary;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class FormOpeNursingList : Form
    {
        public DataSet mSet;
        DataSet dSet;

        public Dictionary<string, string> asTitleDict = new Dictionary<string, string>();

        class deptCount
        {
            public int count1 = 0;
            public int count2 = 0;
        }

        Dictionary<string, deptCount> countDict = new Dictionary<string, deptCount>();

        public FormOpeNursingList()
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

        private void FormOpeNursingList_Load(object sender, EventArgs e)
        {
            try
            {
                LibSettings.Init();

                // 設定ファイル
                string file1 = AppFile.FilePath("OpeNursing.xml");

                yearBox.Items.Add("");
                monthBox.Items.Add("");

                for (int yyyy = 2007; yyyy <= DateTime.Now.Year + 1; yyyy++)
                {
                    this.yearBox.Items.Add(yyyy.ToString());
                }

                this.yearBox.Text = DateTime.Now.Year.ToString();

                for (int mm = 1; mm <= 12; mm++)
                {
                    this.monthBox.Items.Add(mm.ToString());
                }

                this.monthBox.Text = DateTime.Now.Month.ToString();

                mSet = new DataSet();
                mSet.ReadXml(new System.IO.StreamReader(file1, Encoding.GetEncoding("shift-jis")));

                DataTable tmpTable = mSet.Tables["Dept"];

                this.deptBox.Items.Add("");

                foreach (DataRow tmpRow in tmpTable.Rows)
                {
                    if (!this.deptBox.Items.Contains(tmpRow.ItemArray[0].ToString()))
                    {
                        this.deptBox.Items.Add(tmpRow.ItemArray[0].ToString());
                    }
                }

                recKindBox.Items.Add("");
                recKindBox.Items.Add("術前");
                recKindBox.Items.Add("術中");
                recKindBox.Items.Add("術後");

                this.anesBox.Items.Add("");
                this.anesBox.Items.Add("全身麻酔");
                this.anesBox.Items.Add("脊椎麻酔");
                this.anesBox.Items.Add("局所麻酔");

                tmpTable = mSet.Tables["AsTitle"];

                this.asTitleDict.Add("0", "");

                foreach (DataRow tmpRow in tmpTable.Rows)
                {
                    this.asTitleDict.Add(tmpRow.ItemArray[0].ToString(), tmpRow.ItemArray[1].ToString());
                }

                dSet = new DataSet();
                tmpTable = dSet.Tables.Add("手術患者");
                tmpTable.Columns.Add("ID");
                tmpTable.Columns.Add("手術日");
                tmpTable.Columns.Add("患者ID");
                tmpTable.Columns.Add("氏名");
                tmpTable.Columns.Add("生年月日");
                tmpTable.Columns.Add("年齢");
                tmpTable.Columns.Add("性別");
                tmpTable.Columns.Add("入外コード");
                tmpTable.Columns.Add("入外");
                tmpTable.Columns.Add("緊急");
                tmpTable.Columns.Add("種別");
                tmpTable.Columns.Add("診療科コード");
                tmpTable.Columns.Add("診療科");
                tmpTable.Columns.Add("麻酔");
                tmpTable.Columns.Add("執刀医");
                tmpTable.Columns.Add("助手医");
                tmpTable.Columns.Add("麻酔医");
                tmpTable.Columns.Add("術式");
                tmpTable.Columns.Add("部位");
                tmpTable.Columns.Add("入室時刻");
                tmpTable.Columns.Add("退室時刻");
                tmpTable.Columns.Add("麻酔開始");
                tmpTable.Columns.Add("麻酔終了");
                tmpTable.Columns.Add("手術開始");
                tmpTable.Columns.Add("手術終了");
                tmpTable.Columns.Add("室");
                tmpTable.Columns.Add("感染");
                tmpTable.Columns.Add("器械Ns");
                tmpTable.Columns.Add("外回Ns");
                tmpTable.Columns.Add("記録Ns");
                tmpTable.Columns.Add("作成者");
                tmpTable.Columns.Add("ステータス");
                tmpTable.Columns.Add("完成");

                tmpTable = dSet.Tables.Add("科別件数");
                tmpTable.Columns.Add("DEPT");
                tmpTable.Columns.Add("診療科");
                tmpTable.Columns.Add("外来");
                tmpTable.Columns.Add("入院");

                tmpTable = dSet.Tables.Add("手術申込患者");
                tmpTable.Columns.Add("日付");
                tmpTable.Columns.Add("ID");
                tmpTable.Columns.Add("氏名");
                tmpTable.Columns.Add("医師");

                this.makeTable();
                this.filterView();

                this.toolTip1.SetToolTip(this.countBox1, "表示されている手術件数です");
                this.toolTip1.SetToolTip(this.countBox3, "手術申込伝票が印刷された人です");

                this.InitShow(Environment.GetCommandLineArgs());

                if (LoginUser.Status == LoginUser.STATUS.NONE)
                {
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
                FormOpeNursingPat fp1 = new FormOpeNursingPat(this, pat_id.ToString());
                fp1.Show();
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void makeTable()
        {
            string ope_date_start = "";
            string ope_date_end = "";

            if (monthBox.Text == "")
            {
                ope_date_start = yearBox.Text + "0101";
                ope_date_end = yearBox.Text + "1231";
            }
            else
            {
                ope_date_start = yearBox.Text + monthBox.Text.PadLeft(2, '0') + "01";
                ope_date_end = yearBox.Text + monthBox.Text.PadLeft(2, '0') + "99";
            }

            DataTable table = dSet.Tables["手術患者"];
            table.Clear();

            List<OpeNursingData> list = OpeNursingData.Find(ope_date_start, ope_date_end);

            foreach (OpeNursingData p in list)
            {
                DataRow r = table.NewRow();

                r["ID"] = p.Id;
                r["手術日"] = p.OpeDateStringShort;
                r["患者ID"] = p.Pat.Id;
                r["氏名"] = p.Pat.Name;
                r["生年月日"] = p.Pat.BirthString;
                r["年齢"] = p.Pat.AgeCalc(p.OpeDate);
                r["性別"] = p.Pat.Sex;
                r["入外コード"] = p.InOut;
                r["入外"] = p.InOutStringShort;
                r["緊急"] = p.TimeOutString;
                r["種別"] = p.RecKindString;
                r["診療科コード"] = p.Dept;
                r["診療科"] = p.DeptShort;
                r["麻酔"] = p.Anes;
                r["執刀医"] = p.Doctor1;
                r["助手医"] = p.Doctor2;
                r["麻酔医"] = p.Doctor3;
                r["術式"] = p.Ope;
                r["部位"] = p.Part;
                r["入室時刻"] = p.RoomTimeString1;
                r["退室時刻"] = p.RoomTimeString2;
                r["麻酔開始"] = p.AnesTimeString1;
                r["麻酔終了"] = p.AnesTimeString2;
                r["手術開始"] = p.OpeTimeString1;
                r["手術終了"] = p.OpeTimeString2;
                r["室"] = p.Room;
                r["感染"] = p.InfectionFlg;
                r["器械Ns"] = p.Ns1;
                r["外回Ns"] = p.Ns2;
                r["記録Ns"] = p.RecHist;
                r["作成者"] = p.StaffName;
                r["ステータス"] = p.Status;
                r["完成"] = p.StatusFlg;

                table.Rows.Add(r);
            }
        }

        private void makeTable3()
        {
            DataTable tmpTable = dSet.Tables["手術申込患者"];
            tmpTable.Clear();

            List<PatOpeOrder> list = PatOpeOrder.Load(int.Parse(startDate.Value.ToString("yyyyMMdd")), int.Parse(endDate.Value.ToString("yyyyMMdd")));

            foreach (PatOpeOrder p in list)
            {
                DataRow r = tmpTable.NewRow();

                r["日付"] = DateTimeAgent.DateFormat(p.Date, DateTimeAgent.DateFormatKind.MD);
                r["ID"] = p.Pat.Id;
                r["氏名"] = p.Pat.Name;
                r["医師"] = p.OpeDoctor;

                tmpTable.Rows.Add(r);
            }

            DataView tmpView3 = new DataView(tmpTable);

            statView3.DataSource = tmpView3;

            statView3.Columns["日付"].Width = 40;
            statView3.Columns["日付"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            statView3.Columns["ID"].Width = 45;
            statView3.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            statView3.Columns["氏名"].Width = 80;

            statView3.Columns["医師"].Width = 60;

            this.countBox3.Text = statView3.RowCount.ToString();
        }

        private void filterView()
        {
            DataTable tmpTable = dSet.Tables["手術患者"];

            DataView tmpView = new DataView(tmpTable);

            tmpView.RowFilter = "";

            if (this.deptBox.Text.Length > 0)
            {
                if (honkanBox.Checked)
                {
                    tmpView.RowFilter = "診療科コード = " + this.deptBox.Text.Split(' ')[0].Trim() + " and 診療科コード <> '7'";
                }
                else
                {
                    tmpView.RowFilter = "診療科コード = " + this.deptBox.Text.Split(' ')[0].Trim();
                }
            }
            else
            {
                if (honkanBox.Checked)
                {
                    tmpView.RowFilter = "診療科コード <> '7'";
                }
            }

            if (this.recKindBox.Text.Length > 0)
            {
                if (tmpView.RowFilter.Length > 0)
                {
                    tmpView.RowFilter += " and 種別 = '" + this.recKindBox.Text + "'";
                }
                else
                {
                    tmpView.RowFilter = "種別 = '" + this.recKindBox.Text + "'";
                }
            }

            if (this.anesBox.Text.Length > 0)
            {
                string anesFilter = "";

                if (this.anesBox.Text.Equals("全身麻酔"))
                {
                    anesFilter = "麻酔 like '%全%'";
                }
                else if (this.anesBox.Text.Equals("脊椎麻酔"))
                {
                    anesFilter = "麻酔 like '%脊%'";
                }
                else if (this.anesBox.Text.Equals("局所麻酔"))
                {
                    anesFilter = "麻酔 like '%局%'";
                }
                else
                {
                    anesFilter = "麻酔 like '%" + this.anesBox.Text + "%'";
                }

                if (tmpView.RowFilter.Length > 0)
                {
                    tmpView.RowFilter += " and " + anesFilter;
                }
                else
                {
                    tmpView.RowFilter = anesFilter;
                }
            }

            this.statView1.DataSource = tmpView;

            statView1.Columns["ID"].Visible = false;

            statView1.Columns["手術日"].Width = 55;
            statView1.Columns["手術日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            statView1.Columns["患者ID"].Width = 45;
            statView1.Columns["患者ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            statView1.Columns["氏名"].Width = 80;
            statView1.Columns["氏名"].DefaultCellStyle.Font = new Font("MS UI Gothic", 9);

            statView1.Columns["生年月日"].Visible = false;
            statView1.Columns["生年月日"].Width = 75;
            statView1.Columns["生年月日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            statView1.Columns["年齢"].Width = 35;
            statView1.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            statView1.Columns["性別"].Visible = false;

            statView1.Columns["入外コード"].Visible = false;
            statView1.Columns["入外"].Width = 35;
            statView1.Columns["入外"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            statView1.Columns["緊急"].Width = 35;
            statView1.Columns["緊急"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            statView1.Columns["種別"].Width = 35;
            statView1.Columns["種別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            statView1.Columns["診療科コード"].Visible = false;
            statView1.Columns["診療科"].HeaderText = "科";
            statView1.Columns["診療科"].Width = 40;
            statView1.Columns["診療科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            statView1.Columns["麻酔"].Width = 50;

            statView1.Columns["執刀医"].Width = 55;

            statView1.Columns["助手医"].Visible = false;
            statView1.Columns["助手医"].Width = 50;

            statView1.Columns["麻酔医"].Width = 50;

            statView1.Columns["術式"].Width = 90;

            statView1.Columns["部位"].Width = 50;

            statView1.Columns["入室時刻"].Visible = false;
            statView1.Columns["入室時刻"].Width = 50;
            statView1.Columns["入室時刻"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            statView1.Columns["退室時刻"].Visible = false;
            statView1.Columns["退室時刻"].Width = 50;
            statView1.Columns["退室時刻"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            statView1.Columns["麻酔開始"].Visible = false;
            statView1.Columns["麻酔開始"].Width = 50;
            statView1.Columns["麻酔開始"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            statView1.Columns["麻酔終了"].Visible = false;
            statView1.Columns["麻酔終了"].Width = 50;
            statView1.Columns["麻酔終了"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            statView1.Columns["手術開始"].Visible = false;
            statView1.Columns["手術開始"].Width = 50;
            statView1.Columns["手術開始"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            statView1.Columns["手術終了"].Visible = false;
            statView1.Columns["手術終了"].Width = 50;
            statView1.Columns["手術終了"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            statView1.Columns["室"].Visible = false;

            statView1.Columns["感染"].Width = 35;
            statView1.Columns["感染"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            statView1.Columns["器械Ns"].Visible = false;
            statView1.Columns["器械Ns"].Width = 55;
            statView1.Columns["外回Ns"].Visible = false;
            statView1.Columns["外回Ns"].Width = 55;
            statView1.Columns["記録Ns"].Visible = false;
            statView1.Columns["記録Ns"].Width = 55;

            statView1.Columns["作成者"].Visible = false;
            statView1.Columns["作成者"].Width = 55;

            statView1.Columns["ステータス"].Visible = false;

            statView1.Columns["完成"].Width = 35;
            statView1.Columns["完成"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // 削除された記録は背景グレー・削除線を引く
            Font inActiveFont = new Font("MS UI Gothic", 8, FontStyle.Strikeout);

            // 科別件数Dictionaryをリセットする
            countDict.Clear();

            for (int i = 0; i < statView1.RowCount; i++)
            {
                DataGridViewRow tmpRow = statView1.Rows[i];

                if (tmpRow.Cells["ステータス"].Value.ToString() == "0")
                {
                    tmpRow.DefaultCellStyle.Font = inActiveFont;
                    tmpRow.DefaultCellStyle.BackColor = Color.LightGray;
                }

                if (tmpRow.Cells["性別"].Value.ToString().Equals("2"))
                {
                    tmpRow.Cells["氏名"].Style.ForeColor = Color.Red;
                }

                string tmp_dept_id = tmpRow.Cells["診療科コード"].Value.ToString();
                deptCount dc;

                if (countDict.ContainsKey(tmp_dept_id))
                {
                    dc = countDict[tmp_dept_id];
                }
                else
                {
                    dc = new deptCount();
                    countDict.Add(tmp_dept_id, dc);
                }

                if (tmpRow.Cells["入外コード"].Value.ToString().Equals("1"))
                {
                    dc.count1 += 1;
                }
                else if (tmpRow.Cells["入外コード"].Value.ToString().Equals("2"))
                {
                    dc.count2 += 1;
                }
            }

            this.countBox1.Text = statView1.RowCount.ToString();

            // 科別件数Tableをリセットする
            tmpTable = dSet.Tables["科別件数"];
            tmpTable.Clear();

            foreach (string s in countDict.Keys)
            {
                DataRow r = tmpTable.NewRow();

                r["DEPT"] = s;
                r["診療科"] = Dict.DeptDict.ContainsKey(s) ? Dict.DeptDict[s].ShortName : "";
                r["外来"] = countDict[s].count1.ToString();
                r["入院"] = countDict[s].count2.ToString();

                tmpTable.Rows.Add(r);
            }

            DataView tmpView2 = new DataView(tmpTable);

            tmpView2.Sort = "DEPT";

            statView2.DataSource = tmpView2;

            statView2.Columns["DEPT"].Visible = false;
            statView2.Columns["診療科"].Width = 100;
            statView2.Columns["外来"].Width = 50;
            statView2.Columns["外来"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            statView2.Columns["入院"].Width = 50;
            statView2.Columns["入院"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            this.makeTable();
            this.filterView();
        }

        private void FileCarteMenuItem_Click(object sender, EventArgs e)
        {
            FormOpeNursingPat fp1 = new FormOpeNursingPat(this, "");
            fp1.Show();
        }

        private void carteContextMenuItem1_Click(object sender, EventArgs e)
        {
            FormOpeNursingPat fp1 = new FormOpeNursingPat(this, statView1.CurrentRow.Cells["患者ID"].Value.ToString());
            fp1.Show();
        }

        private void carteContextMenuItem3_Click(object sender, EventArgs e)
        {
            FormOpeNursingPat fp1 = new FormOpeNursingPat(this, statView3.CurrentRow.Cells["ID"].Value.ToString());
            fp1.Show();
        }

        private void statView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FormOpeNursingPat fp1 = new FormOpeNursingPat(this, statView1.Rows[e.RowIndex].Cells["患者ID"].Value.ToString());
            fp1.Show();
        }

        private void deptBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.filterView();
        }

        private void anesBox_TextChanged(object sender, EventArgs e)
        {
            this.filterView();
        }

        private void honkanBox_CheckedChanged(object sender, EventArgs e)
        {
            this.filterView();
        }

        private void recKindBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.filterView();
        }

        private void startDate_ValueChanged(object sender, EventArgs e)
        {
            if (startDate.Value > endDate.Value)
            {
                endDate.Value = startDate.Value;
            }
        }

        private void endDate_ValueChanged(object sender, EventArgs e)
        {
            if (startDate.Value > endDate.Value)
            {
                startDate.Value = endDate.Value;
            }
        }

        private void showButton3_Click(object sender, EventArgs e)
        {
            this.makeTable3();
        }

        private void fileSaveButton_Click(object sender, EventArgs e)
        {
            string file_name = AppFile.FilePath("手術台帳.xls");

            if (!System.IO.File.Exists(file_name))
            {
                return;
            }

            Excel.Application exApp = new Excel.Application();
            Excel._Workbook exWorkbook;
            Excel._Worksheet exWorksheet1;

            exApp.Visible = true;

            exWorkbook = (Excel._Workbook)(exApp.Workbooks.Open(file_name,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value));

            exWorksheet1 = (Excel._Worksheet)(exWorkbook.Sheets["手術台帳"]);

            try
            {
                int y = 2;
                int x = 1;

                foreach (DataGridViewRow r in statView1.Rows)
                {
                    exWorksheet1.Cells[y, x++] = r.Cells["手術日"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["患者ID"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["氏名"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["生年月日"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["年齢"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["入外"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["緊急"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["診療科"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["麻酔"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["執刀医"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["助手医"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["麻酔医"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["術式"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["部位"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["麻酔開始"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["麻酔終了"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["手術開始"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["手術終了"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["器械Ns"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["外回Ns"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["作成者"].Value.ToString();

                    y++;
                    x = 1;
                }

                exWorksheet1.get_Range("A1", "U" + (y - 1)).Borders.LineStyle = Excel.XlLineStyle.xlDash;

                string exFileName = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\手術台帳" + yearBox.Text + "年" + monthBox.Text.PadLeft(2, '0') + "月.xls";

                exWorkbook.SaveAs(exFileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex);
            }
            finally
            {
                Marshal.ReleaseComObject(exWorksheet1);
                Marshal.ReleaseComObject(exWorkbook);
                Marshal.ReleaseComObject(exApp);

                GC.Collect();
            }
        }

        private void FileExitMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ToolEyeCenterMenuItem_Click(object sender, EventArgs e)
        {
            Launcher.EyeCenter("", "-r");
        }

        private void ToolOpeOrderMenuItem_Click(object sender, EventArgs e)
        {
            Launcher.OpeOrder();
        }

        private void ToolAnesSupportMenuItem_Click(object sender, EventArgs e)
        {
            Launcher.MedicalAgent("-anes");
        }

        private void LabelPrintButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in statView3.Rows)
            {
                Launcher.PatLabelLight(r.Cells["ID"].Value.ToString(), "", false);
            }
        }

        private void LabelPrintContextMenuItem3_Click(object sender, EventArgs e)
        {
            if (statView3.SelectedRows.Count > 0)
            {
                Launcher.PatLabelLight(statView3.SelectedRows[0].Cells["ID"].Value.ToString(), "", false);
            }
        }

        private void LoginUsrLabel_DoubleClick(object sender, EventArgs e)
        {
            LoginPrompt lc = new LoginPrompt();
            lc.ShowDialog();
            this.LoginUsrLabel.Text = LoginUser.Name;
        }

        private void FormOpeNursingList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                LoginChange lc = new LoginChange();
                lc.ShowDialog();
                this.LoginUsrLabel.Text = LoginUser.Name;
            }
        }
    }
}