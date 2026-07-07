using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class FCRForm : Form
    {
        DataSet orderSet = new DataSet();

        PatBase _Pat = new PatBase();

        public PatBase Pat
        {
            get
            {
                if (!this._Pat.Id.Equals(this.ptIdBox.Text))
                {
                    this._Pat = PatBase.Load(this.ptIdBox.Text);
                }

                return this._Pat;
            }
        }

        public class StudyInfo
        {
            public PatBase Pat;
            public string AccessionNumber;
            public string StudyID;
            public string ScheduledStartDate;
            public string ScheduledStartTime;
            public string DepartmentCode;
            public string DepartmentNameDbcs;

            public void Clear()
            {
                Pat = new PatBase();
                AccessionNumber = "";
                StudyID = "";
                ScheduledStartDate = "";
                ScheduledStartTime = "";
                DepartmentCode = "";
                DepartmentNameDbcs = "";
            }
        }

        /// <summary>
        /// StudyInfo を患者ごとにグループ化したもの
        /// </summary>
        public class StudyInfoGroup
        {
            public PatBase Pat = new PatBase();

            public List<StudyInfo> StudyInfoList = new List<StudyInfo>();

            public static List<StudyInfoGroup> GetList(List<StudyInfo> info_list)
            {
                List<StudyInfoGroup> list = new List<StudyInfoGroup>();

                foreach (StudyInfo info in info_list)
                {
                    bool b = false;

                    foreach (StudyInfoGroup group in list)
                    {
                        if (info.Pat.Id.Equals(group.Pat.Id))
                        {
                            group.StudyInfoList.Add(info);
                            b = true;
                            break;
                        }
                    }

                    if (!b)
                    {
                        StudyInfoGroup group = new StudyInfoGroup();
                        group.Pat = info.Pat;
                        group.StudyInfoList.Add(info);
                        list.Add(group);
                    }
                }

                return list;
            }
        }

        public FCRForm()
        {
            InitializeComponent();
        }

        private void FCRForm_Load(object sender, EventArgs e)
        {
            try
            {
                LibSettings.Init();

				List<string> errs = new List<string>();

				if (!File.Exists(AppFile.FilePath("FCR.xml")))
				{
					errs.Add("設定ファイル FCR.xml が存在しません");
				}

				orderSet.ReadXml(new StreamReader(AppFile.FilePath("FCR.xml"), Encoding.GetEncoding("shift-jis")));

                DataTable tmpTable = orderSet.Tables["FCR"];

                foreach (DataRow r in tmpTable.Rows)
                {
                    CheckBox tmpBox = new CheckBox();

                    tmpBox.Name = "SendToBox" + r["Id"].ToString();
                    tmpBox.Text = r["Location"].ToString();
					tmpBox.Tag = r["Path"].ToString().TrimEnd('\\');
                    tmpBox.AutoSize = true;

                    SendToBoxPanel.Controls.Add(tmpBox);
                }

				if (errs.Count > 0)
				{
					throw new Exception(AppString.ConcatList(errs, Environment.NewLine));
				}

                string[] args = Environment.GetCommandLineArgs();

                for (int i = 0; i < args.Length - 1; i++)
                {
                    if (args[i].Equals("-s", StringComparison.CurrentCultureIgnoreCase))
                    {
                        foreach (CheckBox tmpBox in SendToBoxPanel.Controls)
                        {
                            if (tmpBox.Name.Equals("SendToBox" + args[i + 1]))
                            {
                                tmpBox.Checked = true;
                            }
                        }
                    }
                }

                tmpTable = orderSet.Tables.Add("オーダー");
                tmpTable.Columns.Add("オーダー番号");
                tmpTable.Columns.Add("施行予定日");
                tmpTable.Columns.Add("受付番号");
                tmpTable.Columns.Add("種別");
                tmpTable.Columns.Add("未");
                tmpTable.Columns.Add("時間");
                tmpTable.Columns.Add("ID");
                tmpTable.Columns.Add("カナ");
                tmpTable.Columns.Add("氏名");
                tmpTable.Columns.Add("性別");
                tmpTable.Columns.Add("生年月日");
                tmpTable.Columns.Add("年齢");
                tmpTable.Columns.Add("電話番号");
                tmpTable.Columns.Add("入外");
                tmpTable.Columns.Add("科");
                tmpTable.Columns.Add("医師");
                tmpTable.Columns.Add("指示日");
                tmpTable.Columns.Add("内容");
                tmpTable.Columns.Add("施行フラグ");
                tmpTable.Columns.Add("送信", typeof(bool));
                tmpTable.Columns.Add("Obj", typeof(FCROrder));

                tmpTable = orderSet.Tables.Add("患者オーダー");
                tmpTable.Columns.Add("オーダー番号");
                tmpTable.Columns.Add("施行予定日");
                tmpTable.Columns.Add("種別");
                tmpTable.Columns.Add("入外");
                tmpTable.Columns.Add("科コード");
                tmpTable.Columns.Add("科");
                tmpTable.Columns.Add("医師");
                tmpTable.Columns.Add("指示日");
                tmpTable.Columns.Add("内容");
                tmpTable.Columns.Add("施行フラグ");
                tmpTable.Columns.Add("送信", typeof(bool));
                tmpTable.Columns.Add("Obj", typeof(PatOrder));

                HandleButton2.Checked = true;

                ModeBox.Checked = true;
                ModeBox.BackColor = Color.Azure;
                ModeBox.Text = "施行済";

                this.ShowList();
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex);
                this.Dispose();
            }
        }

        private void FCRForm_Shown(object sender, EventArgs e)
        {
            List<string> sendToList = new List<string>();

            DataTable tmpTable = orderSet.Tables["FCR"];

            foreach (DataRow r in tmpTable.Rows)
            {
                sendToList.Add(r["Path"].ToString().TrimEnd('\\'));
            }

            // FCR連携ファイルの出力先フォルダの確認
            List<string> errs = new List<string>();

            foreach (string path in sendToList)
            {
                if (!Directory.Exists(path))
                {
                    errs.Add("FCR連携ファイルの出力先フォルダ " + path + " が存在しません");
                }
                else
                {
                    FileAttributes fas = File.GetAttributes(path);

                    if ((fas & FileAttributes.Directory) != FileAttributes.Directory)
                    {
                        errs.Add(path + " はフォルダではありません");
                    }

                    if ((fas & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        errs.Add(path + " は読み取り専用です。書き込み権限を付与してください");
                    }
                }
            }

            if (errs.Count > 0)
            {
                MessageBox.Show(AppString.ConcatList(errs, Environment.NewLine));
            }
        }

        private void MakeXMLButton_Click(object sender, EventArgs e)
        {
            if (this.Pat.Id.Length == 0 || this.Pat.Id.Length > 9)
            {
                MessageBox.Show("患者情報が正しくありません。");
                return;
            }

            List<StudyInfo> studyInfoList = new List<StudyInfo>();

            foreach (DataGridViewRow r in ptOrderView.Rows)
            {
                if (r.Cells["送信"].Value.ToString().Equals("true", StringComparison.CurrentCultureIgnoreCase))
                {
                    PatOrder obj = (PatOrder)r.Cells["Obj"].Value;

                    StudyInfo info = new StudyInfo();
                    info.Pat = this.Pat;
                    info.AccessionNumber = obj.OrderId;
                    info.StudyID = obj.OrderId;
                    info.ScheduledStartDate = DateTime.Now.ToString("yyyyMMdd");
                    info.ScheduledStartTime = DateTime.Now.ToString("HHmmss");
                    info.DepartmentCode = obj.Dept;
                    info.DepartmentNameDbcs = obj.DeptName;

                    studyInfoList.Add(info);
                }
            }

            if (studyInfoList.Count == 0)
            {
                MessageBox.Show("送信情報がありません。");
                return;
            }

            this.Send(studyInfoList);

#if SEAL_PRINT
            printDocument1.Print();

//            printPreviewDialog1.Document = printDocument1;
//            printPreviewDialog1.ShowDialog();
#endif
        }

        private void MakeXMLsButton_Click(object sender, EventArgs e)
        {
            List<StudyInfo> info_list = new List<StudyInfo>();

            foreach (DataGridViewRow r in this.orderView.Rows)
            {
                if (!r.Cells["送信"].Value.ToString().Equals("true", StringComparison.CurrentCultureIgnoreCase)) continue;

                FCROrder obj = (FCROrder)r.Cells["Obj"].Value;

                StudyInfo info = new StudyInfo();
                info.Pat = obj.Pat;
                info.AccessionNumber = obj.OrderId;
                info.StudyID = obj.OrderId;
                info.ScheduledStartDate = DateTime.Now.ToString("yyyyMMdd");
                info.ScheduledStartTime = DateTime.Now.ToString("HHmmss");
                info.DepartmentCode = obj.Dept;
                info.DepartmentNameDbcs = obj.DeptName;

                info_list.Add(info);
            }

            this.Send(info_list);

            // 送信後はリストを更新する
            this.ShowList();
        }

        void Send(List<StudyInfo> studyInfoList)
        {
            List<string> sendToList = new List<string>();

            foreach (CheckBox tmpBox in SendToBoxPanel.Controls)
            {
                if (tmpBox.Checked)
                {
                    sendToList.Add(tmpBox.Tag.ToString());
                }
            }

            if (sendToList.Count == 0)
            {
                MessageBox.Show("送信先がありません。");
                return;
            }

            // FCR連携ファイルの出力先フォルダの確認
            List<string> errs = new List<string>();

            foreach (string path in sendToList)
            {
                if (!Directory.Exists(path))
                {
                    errs.Add("FCR連携ファイルの出力先フォルダ " + path + " が存在しません");
                }
                else
                {
                    FileAttributes fas = File.GetAttributes(path);

                    if ((fas & FileAttributes.Directory) != FileAttributes.Directory)
                    {
                        errs.Add(path + " はフォルダではありません");
                    }

                    if ((fas & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        errs.Add(path + " は読み取り専用です。書き込み権限を付与してください");
                    }
                }
            }

            if (errs.Count > 0)
            {
                MessageBox.Show(AppString.ConcatList(errs, Environment.NewLine));
                return;
            }

            string handle = "";

            if (HandleButton1.Checked)
            {
                handle = "Start";
            }
            else if (HandleButton2.Checked)
            {
                handle = "Reserve";
            }
            else
            {
                MessageBox.Show("Start/Reserve が選択されていません。");
                return;
            }

            List<StudyInfoGroup> list = StudyInfoGroup.GetList(studyInfoList);

            int c = 0;
            bool b = false;

            foreach (StudyInfoGroup group in list)
            {
                b = FCRData.MakeXML(group.Pat, group.StudyInfoList, sendToList, handle);

                if (b) c++;
            }

            MessageBox.Show(c + " 名のデータを送信しました");
        }

        private void ModeBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ModeBox.Checked)
            {
                ModeBox.BackColor = Color.Azure;
                ModeBox.Text = "施行済";
            }
            else
            {
                ModeBox.BackColor = Color.White;
                ModeBox.Text = "未施行";
            }

            this.FormatList();
        }

        private void NextBox_CheckedChanged(object sender, EventArgs e)
        {
            this.FormatList();
        }

        private void ListButton_Click(object sender, EventArgs e)
        {
            this.ShowList();
        }

        private void FCRForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                this.ShowList();
            }
        }

        // 当該日のオーダー一覧を表示
        private void ShowList()
        {
            DataTable tmpTable = orderSet.Tables["オーダー"];
            tmpTable.Clear();

            List<FCROrder> list = FCROrder.GetListFCROrder(orderDate.Value.ToString("yyyyMMdd"));
            List<string> pt_id_list = new List<string>();

            foreach (FCROrder obj in list)
            {
                DataRow r = tmpTable.NewRow();

                r["オーダー番号"] = obj.OrderId;
                r["施行予定日"] = obj.SekouDate;

                if (obj.Sekou1.Equals("101"))
                {
                    r["種別"] = "単純";
                }
                else if (obj.Sekou1.Equals("112"))
                {
                    r["種別"] = "下部";
                }

                if (obj.SekouDate.Equals("99999999"))
                {
                    r["未"] = "●";
                }

                r["時間"] = obj.SekouTimeString;

                r["ID"] = obj.Pat.Id;

                if (!pt_id_list.Contains(obj.Pat.Id))
                {
                    pt_id_list.Add(obj.Pat.Id);
                }

                r["カナ"] = obj.Pat.Kana;
                r["氏名"] = obj.Pat.Name;
                r["性別"] = obj.Pat.SexNameEng;
                r["生年月日"] = obj.Pat.BirthString;
                r["年齢"] = obj.Pat.AgeCalc(orderDate.Value.ToString("yyyyMMdd"));
                r["電話番号"] = obj.Pat.Tel;
                r["入外"] = obj.InOutNameShort;
                r["科"] = obj.DeptName;
                r["医師"] = obj.DoctorName;
                r["指示日"] = obj.OrderDateString;
                r["内容"] = obj.SOAP;
                r["施行フラグ"] = obj.SekouFlg;
                r["Obj"] = obj;

                tmpTable.Rows.Add(r);
            }

            Dictionary<string, string> tmpDict = PatOut.GetOnedayLastSeq(pt_id_list, orderDate.Value.ToString("yyyyMMdd"));

            foreach (DataRow r in tmpTable.Rows)
            {
                if (tmpDict.ContainsKey(r["ID"].ToString()))
                {
                    r["受付番号"] = tmpDict[r["ID"].ToString()];
                }
            }

            this.FormatList();
        }

        private void FormatList()
        {
            DataTable tmpTable = orderSet.Tables["オーダー"];
            DataView tmpView = new DataView(tmpTable);

            if (ModeBox.Checked)
            {
                tmpView.RowFilter = "施行フラグ = 1";
                orderView.DefaultCellStyle.BackColor = Color.Azure;
            }
            else
            {
                tmpView.RowFilter = "施行フラグ = 0";
                orderView.DefaultCellStyle.BackColor = Color.White;
            }

            if (!NextBox.Checked)
            {
                tmpView.RowFilter += " and 未 is null";
            }

            this.orderView.DataSource = tmpView;

            Font f9 = new Font("ＭＳ Ｐゴシック", 9, FontStyle.Regular);

            orderView.Columns["オーダー番号"].Visible = false;

            orderView.Columns["施行予定日"].Visible = false;

            orderView.Columns["受付番号"].HeaderText = "受付";
            orderView.Columns["受付番号"].Width = 35;
            orderView.Columns["受付番号"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            orderView.Columns["種別"].Width = 40;
            orderView.Columns["種別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            orderView.Columns["未"].Width = 30;
            orderView.Columns["未"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            orderView.Columns["時間"].HeaderText = "時";
            orderView.Columns["時間"].Width = 40;
            orderView.Columns["時間"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            orderView.Columns["ID"].Width = 50;
            orderView.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            orderView.Columns["ID"].DefaultCellStyle.Font = f9;

            orderView.Columns["カナ"].Width = 70;

            orderView.Columns["氏名"].Width = 80;
            orderView.Columns["氏名"].DefaultCellStyle.Font = f9;

            orderView.Columns["性別"].Width = 40;
            orderView.Columns["性別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            orderView.Columns["性別"].Visible = false;

            orderView.Columns["生年月日"].Visible = false;

            orderView.Columns["年齢"].Width = 35;
            orderView.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            orderView.Columns["電話番号"].Visible = false;

            orderView.Columns["入外"].Width = 35;
            orderView.Columns["入外"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            orderView.Columns["科"].Width = 60;

            orderView.Columns["医師"].Width = 70;

            orderView.Columns["指示日"].Width = 75;
            orderView.Columns["指示日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            orderView.Columns["内容"].Width = 170;

            orderView.Columns["施行フラグ"].Visible = false;

            orderView.Columns["送信"].Width = 35;

            orderView.Columns["Obj"].Visible = false;

            foreach (DataGridViewColumn c in orderView.Columns)
            {
                if (!c.Name.Equals("送信"))
                {
                    c.ReadOnly = true;
                }
            }

            foreach (DataGridViewRow r in orderView.Rows)
            {
                if (r.Cells["性別"].Value.ToString().Equals("F"))
                {
                    r.Cells["カナ"].Style.ForeColor = Color.Red;
                    r.Cells["氏名"].Style.ForeColor = Color.Red;
                }
            }
        }

        // 当該患者のその日のオーダーを表示
        private void ShowOrder(string OrderId)
        {
            DataTable tmpTable = orderSet.Tables["患者オーダー"];
            tmpTable.Clear();
#if INNO
            List<string> cond_list = new List<string>();
            cond_list.Add("SEKOU_CODE in (101, 112)");

            List<PatOrder> list = PatOrder.GetListByPatCond(this.ptIdBox.Text, cond_list, "ORDER_DATE desc, SEKOU_TIME desc", true);
#else
            List<string> cond_list = new List<string>();
            cond_list.Add("施行部署１ in (101, 112)");

            List<PatOrder> list = PatOrder.GetListByPatCond(this.ptIdBox.Text, cond_list, "施行予定日 desc, 装置番号 desc", false);
#endif
            foreach (PatOrder obj in list)
            {
                DataRow r = tmpTable.NewRow();

                r["オーダー番号"] = obj.OrderId;

                if (obj.SekouDate.Equals("99999999"))
                {
                    r["施行予定日"] = "未定";
                }
                else
                {
                    r["施行予定日"] = obj.SekouDateString;
                }

                if (obj.Sekou1.Equals("101"))
                {
                    r["種別"] = "単純";
                }
                else if (obj.Sekou1.Equals("112"))
                {
                    r["種別"] = "下部";
                }
                
                r["入外"] = obj.InOutNameShort;
                r["科"] = obj.DeptName;
                r["医師"] = obj.DoctorName;
                r["指示日"] = obj.OrderDateString;
                r["内容"] = obj.SOAP;
                r["施行フラグ"] = obj.SekouFlg;

                if (OrderId.Equals(obj.OrderId))
                {
                    r["送信"] = true;
                }

                r["Obj"] = obj;

                tmpTable.Rows.Add(r);
            }

            this.FormatOrder();
        }

        private void FormatOrder()
        {
            DataTable tmpTable = orderSet.Tables["患者オーダー"];
            DataView tmpView = new DataView(tmpTable);
            
            if (!PastBox.Checked)
            {
                tmpView.RowFilter = "施行予定日 >= '" + orderDate.Value.ToString("yyyy/MM/dd") + "' or 施行予定日 = '未定'";
            }
            
            this.ptOrderView.DataSource = tmpView;

            ptOrderView.Columns["オーダー番号"].Visible = false;

            ptOrderView.Columns["施行予定日"].HeaderText = "施行日";
            ptOrderView.Columns["施行予定日"].Width = 75;
            ptOrderView.Columns["施行予定日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ptOrderView.Columns["種別"].Width = 50;
            ptOrderView.Columns["種別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ptOrderView.Columns["入外"].Width = 40;
            ptOrderView.Columns["入外"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ptOrderView.Columns["科コード"].Visible = false;
            ptOrderView.Columns["科"].Width = 60;

            ptOrderView.Columns["医師"].Width = 70;
            ptOrderView.Columns["指示日"].Width = 75;
            ptOrderView.Columns["内容"].Width = 400;

            ptOrderView.Columns["施行フラグ"].Visible = false;

            ptOrderView.Columns["送信"].Width = 35;

            ptOrderView.Columns["Obj"].Visible = false;

            foreach (DataGridViewRow tmpRow in ptOrderView.Rows)
            {
                if (tmpRow.Cells["施行フラグ"].Value.ToString().Equals("1"))
                {
                    tmpRow.DefaultCellStyle.BackColor = Color.Azure;
                }
                else
                {
                    tmpRow.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void FileExitMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void orderView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex < orderView.Columns.Count - 2)
            {
                DataGridViewRow tmpRow = orderView.Rows[e.RowIndex];

                this.ptIdBox.Text = tmpRow.Cells["ID"].Value.ToString();
                this.ptInfoBox.Text = this.Pat.Info1;
                this.PtSeqBox.Text = tmpRow.Cells["受付番号"].Value.ToString();

                this.ShowOrder(tmpRow.Cells["オーダー番号"].Value.ToString());
            }
        }

        private void PastBox_CheckedChanged(object sender, EventArgs e)
        {
            this.FormatOrder();
        }

        private void CheckAllButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in this.orderView.Rows)
            {
                r.Cells["送信"].Value = true;
            }
        }

        private void ptIdBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ptIdBox.Text.Length > 0 && this.ptIdBox.Text.Length <= 9)
                {
                    this.ptInfoBox.Text = this.Pat.Info1;

                    PtSeqBox.Clear();
                    List<PatOut> tmpList = PatOut.GetOnedayLast(this.ptIdBox.Text, orderDate.Value.ToString("yyyyMMdd"));

                    foreach (PatOut p in tmpList)
                    {
                        PtSeqBox.Text = p.Seq1;
                        break;
                    }

                    this.ShowOrder("");
                }
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font f20 = new Font("", 20);
            Font f20i = new Font("", 20, FontStyle.Italic);
            Font f16 = new Font("", 16);
            Font f16i = new Font("", 16, FontStyle.Italic);
            Font f14 = new Font("", 14);
            Font f12 = new Font("", 12);
            Font f10 = new Font("", 10);

            Pen pen = new Pen(Brushes.Black, 2);

            List<string> printDeptList = new List<string>();

            foreach (DataGridViewRow tmpRow in ptOrderView.Rows)
            {
                if ((bool)(tmpRow.Cells["送信"].Value))
                {
                    if (!printDeptList.Contains(tmpRow.Cells["科"].Value.ToString()))
                    {
                        printDeptList.Add(tmpRow.Cells["科"].Value.ToString());
                    }
                }
            }

            string dept = "";

            foreach (string s in printDeptList)
            {
                if (dept.Length > 0)
                {
                    dept += ", ";
                }

                dept += s;
            }

            e.Graphics.DrawString(dept, f20, Brushes.Black, 50, 20);
            e.Graphics.DrawString("IDNo    " + this.Pat.Id, f20i, Brushes.Black, 50, 60);
            e.Graphics.DrawString(this.Pat.Kana, f16, Brushes.Black, 70, 100);
            e.Graphics.DrawString(this.Pat.Name + "　様", f20, Brushes.Black, 70, 130);
            e.Graphics.DrawString("撮影部位", f16i, Brushes.Black, 50, 170);
            e.Graphics.DrawString("方向", f16i, Brushes.Black, 350, 170);
            e.Graphics.DrawString("撮影日　" + DateTime.Now.ToString("yyyy年MM月dd日"), f16i, Brushes.Black, 450, 60);
            e.Graphics.DrawString("生年月日　" + this.Pat.Birth.Insert(4, "年").Insert(7, "月").Insert(10, "日"), f16i, Brushes.Black, 450, 100);
            e.Graphics.DrawString(this.Pat.Age + " 才", f16, Brushes.Black, 450, 130);
            e.Graphics.DrawString("性別　" + this.Pat.SexNameEng, f16i, Brushes.Black, 650, 130);
        }
    }
}