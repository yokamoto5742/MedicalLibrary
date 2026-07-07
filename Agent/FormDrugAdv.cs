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
    public partial class FormDrugAdv : StdForm1
    {
        static List<FormDrugAdv> FormDrugAdvList = new List<FormDrugAdv>();

        public static FormDrugAdv FormShow(string pt_id = "")
        {
            FormDrugAdv f = null;

            foreach (FormDrugAdv ff in FormDrugAdvList)
            {
                if (ff.Pat.Id.Equals(pt_id))
                {
                    f = ff;
                    break;
                }
            }

            if (f == null || !f.Created)
            {
                f = new FormDrugAdv(pt_id);
                FormDrugAdvList.Add(f);
            }

            f.Show();
            f.BringToFront();
            f.WindowState = FormWindowState.Normal;

            return f;
        }

        string advMode = "新規";
        string disMode = "新規";

        DataSet DSet = new DataSet();

        /// <summary>
        /// 印刷用変数
        /// </summary>
        int pageNumber = 1;
        string printStr = "";
        string printMode = "";

        public FormDrugAdv(string pt_id)
        {
            InitializeComponent();

            this.PatSet(PatBase.Load(pt_id));
        }

        private void FormDrugAdv_Load(object sender, EventArgs e)
        {
            this.changeAdvMode("新規");
            this.changeDisMode("新規");

            this.advDeptBox.Items.Add("");
            this.advDeptFilterBox.Items.Add("");
            this.disDeptBox.Items.Add("");

            foreach (Dept d in Dict.DeptDict.Values)
            {
                if (!d.Code.Equals(0))
                {
                    this.advDeptBox.Items.Add(d.Code + " " + d.ShortName);
                    this.advDeptFilterBox.Items.Add(d.ShortName);
                    this.disDeptBox.Items.Add(d.Code + " " + d.ShortName);
                }
            }

            this.advStaffFilterBox.Items.Add("");

            foreach (Staff s in Dict.StaffDict.Values)
            {
                if (s.IsDrug)
                {
                    this.advStaffFilterBox.Items.Add(s.Name);
                }
            }

            DataTable tmpTable = this.DSet.Tables.Add("服薬指導歴");
            tmpTable.Columns.Add("DRUG_ADV_ID");
            tmpTable.Columns.Add("指導日");
            tmpTable.Columns.Add("入外");
            tmpTable.Columns.Add("入院日");
            tmpTable.Columns.Add("診療科");
            tmpTable.Columns.Add("アレルギ歴");
            tmpTable.Columns.Add("副作用歴");
            tmpTable.Columns.Add("指導内容");
            tmpTable.Columns.Add("担当者");
            tmpTable.Columns.Add("STATUS");
            tmpTable.Columns.Add("PDF_SAVE");
            tmpTable.Columns.Add("Obj", typeof(DrugAdv));

            tmpTable = this.DSet.Tables.Add("退院時指導歴");
            tmpTable.Columns.Add("ID");
            tmpTable.Columns.Add("指導日");
            tmpTable.Columns.Add("入院日");
            tmpTable.Columns.Add("退院日");
            tmpTable.Columns.Add("診療科");
            tmpTable.Columns.Add("指導内容");
            tmpTable.Columns.Add("特記事項");
            tmpTable.Columns.Add("担当者");
            tmpTable.Columns.Add("STATUS");
            tmpTable.Columns.Add("PDF_SAVE");
            tmpTable.Columns.Add("Obj", typeof(DrugAdvDischarge));

            this.makePtData();
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            this.makePtData();
        }

        private void clearAdvData()
        {
            this.advIdBox.Text = "";
            this.advAllergyBox1.Text = "";
            this.advAllergyBox2.Text = "";
            this.advInjectBox.Text = "";
            this.advDrugBox.Text = "";
            this.advBringBox.Text = "";
            this.advDoubleBox.Text = "";
            this.advTabooBox.Text = "";
            this.advAdvBox.Text = "";
            this.advStaffBox.Text = "";
            this.advPdfBox.Checked = false;
            this.advPrintButton.Enabled = false;
            this.advPrintButton.Tag = null;

            // 指導日
            this.advAdvDateTimePicker.Value = DateTime.Now;

            // 入院日・退院日
            this.advInOutButton1.Checked = true;
            this.advInOutButton2.Checked = false;
            this.advAdmDateBox.Text = "";
        }

        private void clearDisData()
        {
            this.disIdBox.Text = "";
            this.disAllergyBox1.Text = "";
            this.disAllergyBox2.Text = "";
            this.disDrugBox.Text = "";
            this.disAdvBox.Text = "";
            this.disStNoteBox.Text = "";
            this.disPtNoteBox1.Text = "";
            this.disPtNoteBox2.Text = "";
            this.disPtNoteBox3.Text = "";
            this.disStaffBox.Text = "";
            this.disPdfBox.Checked = false;
            this.disPrintButton.Enabled = false;
            this.disPrintButton.Tag = null;

            // 指導日
            this.disAdvDateTimePicker.Value = DateTime.Now;

            // 入院日・退院日
            this.disAdmDateTimePicker.Value = DateTime.Now;
            this.disDisDateTimePicker.Value = DateTime.Now;
        }

        private void makePtData()
        {
            this.clearAdvData();
            this.clearDisData();

            if (this.Pat.Id.Length == 0)
            {
                return;
            }

            this.Text = this.Pat.Name;

            // 入退院歴の取得
            List<PatIn> list1 = PatIn.GetHistory(this.Pat.Id, false);

            this.advAdmDateBox.Items.Clear();
            this.advAdmDateFilterBox.Items.Clear();
            this.advAdmDateFilterBox.Items.Add("");

            this.advAdmDateBox.Items.Add("");

            int today = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int rownum = 1;

            foreach (PatIn p in list1)
            {
                this.advAdmDateBox.Items.Add(p);
                this.advAdmDateFilterBox.Items.Add(p.InDateString);

                // 退院時服薬指導の場合は、直近の入退院データをセットする
                if (rownum == 1)
                {
                    this.disAdmDateTimePicker.Value = p.InDateValue;

                    if (p.OutDate.Length == 8)
                    {
                        this.disDisDateTimePicker.Value = p.OutDateValue;
                    }

                    this.disDeptBox.Text = p.Dept + " " + p.DeptName;
                }

                // 通常の服薬指導の場合は、現在入院中であれば、そのデータをセットする
                if (p.InDateInt <= today && (p.OutDateInt >= today || p.OutDateInt == 0))
                {
                    this.advInOutButton2.Checked = true;
                    this.advAdmDateBox.Text = p.ToString();
                    this.advDeptBox.Text = p.Dept + " " + p.DeptName;
                }

                rownum++;
            }

            this.advCommentDateBox.Clear();
            this.advCommentTitleBox.Clear();
            this.advCommentContBox.Clear();

            List<DrugAdvComment> list3 = DrugAdvComment.GetListById(this.Pat.Id, "SAVE_DATE desc, SAVE_TIME desc");

            foreach(DrugAdvComment obj in list3)
            {
                if (!obj.Status.Equals(1)) continue;

                this.advCommentDateBox.Text = DateTimeAgent.DateFormat(obj.SaveDate.ToString(), DateTimeAgent.DateFormatKind.SHORT);
                this.advCommentTitleBox.Text = obj.Title;
                this.advCommentContBox.Text = obj.Cont;

                break;
            }

            this.makeAdvHistory();
            this.makeDisHistory();

            // ログ
            LibUtility.Log("服薬指導カルテ参照", this.Pat.Id);
        }

        private void makeAdvHistory()
        {
            if (!this.DSet.Tables.Contains("服薬指導歴"))
            {
                return;
            }

            DataTable table = this.DSet.Tables["服薬指導歴"];
            table.Clear();

            List<DrugAdv> list1 = DrugAdv.GetListById(this.Pat.Id, "ADV_DATE desc, DRUG_ADV_ID desc");

            foreach (DrugAdv obj in list1)
            {
                DataRow r = table.NewRow();

                r["DRUG_ADV_ID"] = obj.SEQ;
                r["指導日"] = DateTimeAgent.DateFormat(obj.AdvDate, DateTimeAgent.DateFormatKind.SHORT);
                r["入外"] = obj.InOut;
                r["入院日"] = DateTimeAgent.DateFormat(obj.AdmDate, DateTimeAgent.DateFormatKind.SHORT);
                r["診療科"] = obj.DeptName;
                r["アレルギ歴"] = obj.Allergy1;
                r["副作用歴"] = obj.Allergy2;
                r["指導内容"] = obj.Adv;
                r["担当者"] = obj.StaffName;
                r["STATUS"] = obj.Status;
                r["PDF_SAVE"] = obj.PDFSave;
                r["Obj"] = obj;

                table.Rows.Add(r);
            }

            this.formatAdvHistory();
        }

        private void makeDisHistory()
        {
            if (!this.DSet.Tables.Contains("退院時指導歴"))
            {
                return;
            }

            DataTable table = this.DSet.Tables["退院時指導歴"];
            table.Clear();

            List<DrugAdvDischarge> list = DrugAdvDischarge.GetListById(this.Pat.Id);

            foreach (DrugAdvDischarge obj in list)
            {
                DataRow r = table.NewRow();

                r["ID"] = obj.SEQ;
                r["指導日"] = DateTimeAgent.DateFormat(obj.AdvDate, DateTimeAgent.DateFormatKind.SHORT);
                r["入院日"] = DateTimeAgent.DateFormat(obj.AdmDate, DateTimeAgent.DateFormatKind.SHORT);
                r["退院日"] = DateTimeAgent.DateFormat(obj.DisDate, DateTimeAgent.DateFormatKind.SHORT);
                r["診療科"] = obj.DeptName;
                r["指導内容"] = obj.Adv;
                r["特記事項"] = obj.StNote;
                r["担当者"] = obj.StaffName;

                if (obj.Status.Equals(1))
                {
                    r["STATUS"] = "●";
                }
                else if (obj.Status.Equals(2))
                {
                    r["STATUS"] = "○";
                }

                r["PDF_SAVE"] = obj.PDFSave;
                r["Obj"] = obj;

                table.Rows.Add(r);
            }

            this.formatDisHistory();
        }

        private void formatAdvHistory()
        {
            if (!this.DSet.Tables.Contains("服薬指導歴"))
            {
                return;
            }

            DataView tmpView = new DataView(this.DSet.Tables["服薬指導歴"]);

            List<string> filters = new List<string>();

            if (this.advOutFilterBox.Checked)
            {
                filters.Add("入外 = '1'");
            }

            if (this.advAdmDateFilterBox.Text.Length >= 2)
            {
                filters.Add("入院日 = '" + this.advAdmDateFilterBox.Text.Substring(2) + "'");
            }

            if (this.advDeptFilterBox.Text.Length > 0)
            {
                filters.Add("診療科 = '" + this.advDeptFilterBox.Text + "'");
            }

            if (this.advStaffFilterBox.Text.Length > 0)
            {
                filters.Add("担当者 = '" + this.advStaffFilterBox.Text + "'");
            }

            if (!this.advInActiveBox.Checked)
            {
                filters.Add("STATUS = 1");
            }

            tmpView.RowFilter = AppString.ConcatList(filters, " and ");

            advHistoryGridView.DataSource = tmpView;

            advHistoryGridView.Columns["DRUG_ADV_ID"].Visible = false;

            advHistoryGridView.Columns["指導日"].Width = 60;

            advHistoryGridView.Columns["入外"].Visible = false;

            advHistoryGridView.Columns["入院日"].Width = 60;

            advHistoryGridView.Columns["診療科"].Width = 60;
            advHistoryGridView.Columns["アレルギ歴"].Width = 65;
            advHistoryGridView.Columns["副作用歴"].Width = 65;
            advHistoryGridView.Columns["指導内容"].Width = advHistoryGridView.Width - 400;
            advHistoryGridView.Columns["担当者"].Width = 65;

            advHistoryGridView.Columns["STATUS"].Visible = false;
            advHistoryGridView.Columns["PDF_SAVE"].Visible = false;
            advHistoryGridView.Columns["Obj"].Visible = false;

            // 削除された指導は背景グレー・削除線を引く
            Font inActiveFont = new Font(new Font("MS UI Gothic", 8), FontStyle.Strikeout);

            for (int i = 0; i < advHistoryGridView.RowCount; i++)
            {
                if (advHistoryGridView.Rows[i].Cells["STATUS"].Value.ToString() == "0")
                {
                    advHistoryGridView.Rows[i].DefaultCellStyle.Font = inActiveFont;
                    advHistoryGridView.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }

        private void formatDisHistory()
        {
            if (!this.DSet.Tables.Contains("退院時指導歴"))
            {
                return;
            }

            DataView tmpView = new DataView(this.DSet.Tables["退院時指導歴"]);

            tmpView.RowFilter = "";

            if (!this.disInActiveBox.Checked)
            {
                tmpView.RowFilter = "STATUS <> ''";
            }

            disHistoryGridView.DataSource = tmpView;

            disHistoryGridView.Columns["ID"].Visible = false;
            disHistoryGridView.Columns["指導日"].Width = 60;
            disHistoryGridView.Columns["入院日"].Width = 60;
            disHistoryGridView.Columns["退院日"].Width = 60;
            disHistoryGridView.Columns["診療科"].Width = 60;

            disHistoryGridView.Columns["指導内容"].Width = 200;
            disHistoryGridView.Columns["特記事項"].Width = 85;
            disHistoryGridView.Columns["担当者"].Width = 65;

            disHistoryGridView.Columns["STATUS"].HeaderText = "完成";
            disHistoryGridView.Columns["STATUS"].Width = 35;
            disHistoryGridView.Columns["STATUS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            disHistoryGridView.Columns["PDF_SAVE"].Visible = false;
            disHistoryGridView.Columns["Obj"].Visible = false;

            // 削除された指導は背景グレー・削除線を引く
            Font inActiveFont = new Font(new Font("MS UI Gothic", 8), FontStyle.Strikeout);

            for (int i = 0; i < disHistoryGridView.RowCount; i++)
            {
                if (disHistoryGridView.Rows[i].Cells["STATUS"].Value.ToString() == "")
                {
                    disHistoryGridView.Rows[i].DefaultCellStyle.Font = inActiveFont;
                    disHistoryGridView.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }

        // 服薬指導歴の内容を表示する
        private void showAdvData()
        {
            if (advHistoryGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow tmpRow = advHistoryGridView.SelectedRows[0];
                DrugAdv obj = (DrugAdv)tmpRow.Cells["Obj"].Value;

                this.advIdBox.Text = obj.SEQ.ToString();
                this.advAdvDateTimePicker.Value = DateTimeAgent.DateTimeFromInt(obj.AdvDate);

                this.advInOutButton1.Checked = obj.InOut.Equals(1);
                this.advInOutButton2.Checked = obj.InOut.Equals(2);

                this.advDeptBox.Text = obj.Dept.ToString() + " " + obj.DeptName;

                this.advAdmDateBox.Text = "";

                for (int i = 0; i < advAdmDateBox.Items.Count; i++)
                {
                    if (advAdmDateBox.Items[i].ToString().Contains(" ") &&
                        advAdmDateBox.Items[i].ToString().Split(' ')[0] == DateTimeAgent.DateFormat(obj.AdmDate, DateTimeAgent.DateFormatKind.LONG))
                    {
                        this.advAdmDateBox.Text = advAdmDateBox.Items[i].ToString();
                        break;
                    }
                }

                this.advAllergyBox1.Text = obj.Allergy1;
                this.advAllergyBox2.Text = obj.Allergy2;
                this.advInjectBox.Text = obj.Inject;
                this.advDrugBox.Text = obj.Drug;
                this.advBringBox.Text = obj.BringDrug;
                this.advDoubleBox.Text = obj.DoubleDrug;
                this.advTabooBox.Text = obj.TabooMix;
                this.advAdvBox.Text = obj.Adv;

                this.advStaffBox.Text = obj.StaffName;

                if (obj.PDFSave.Equals(1))
                {
                    this.advPdfBox.Checked = true;
                    this.advRegButton.Enabled = false;
                }
                else
                {
                    this.advPdfBox.Checked = false;
                }

                this.advPrintButton.Tag = obj;
                /*
                this.advPrintButton.Tag = obj.PDFFile;
                this.advPrintButton.Enabled = obj.PDFFile.PdfFileExist;
                 */
            }
        }

        // 退院時指導歴の内容を表示する
        private void showDisData()
        {
            if (disHistoryGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow tmpRow = disHistoryGridView.SelectedRows[0];
                DrugAdvDischarge obj = (DrugAdvDischarge)tmpRow.Cells["Obj"].Value;

                this.disIdBox.Text = obj.SEQ.ToString();
                this.disAdvDateTimePicker.Value = DateTimeAgent.DateTimeFromInt(obj.AdvDate);
                this.disAdmDateTimePicker.Value = DateTimeAgent.DateTimeFromInt(obj.AdmDate);
                this.disDisDateTimePicker.Value = DateTimeAgent.DateTimeFromInt(obj.DisDate);

                this.disDeptBox.Text = obj.Dept.ToString() + " " + obj.DeptName;

                this.disAllergyBox1.Text = obj.Allergy1;
                this.disAllergyBox2.Text = obj.Allergy2;
                this.disDrugBox.Text = obj.Drug;
                this.disAdvBox.Text = obj.Adv;
                this.disStNoteBox.Text = obj.StNote;
                this.disPtNoteBox1.Text = obj.PtNote1;
                this.disPtNoteBox2.Text = obj.PtNote2;
                this.disPtNoteBox3.Text = obj.PtNote3;

                this.disStaffBox.Text = obj.StaffName;

                if (obj.Status.Equals(1))
                {
                    this.disCompleteBox.Checked = true;
                }
                else
                {
                    this.disCompleteBox.Checked = false;
                }

                if (obj.PDFSave.Equals(1))
                {
                    this.disPdfBox.Checked = true;
                    this.disRegButton.Enabled = false;
                }
                else
                {
                    this.disPdfBox.Checked = false;
                }

                this.disPrintButton.Tag = obj;
            }
        }

        private void fileNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.changeAdvMode("新規");
        }

        private void ptIdBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.changeAdvMode("新規");
                this.changeDisMode("新規");
                this.makePtData();
            }
        }

        // 服薬指導の文字数制限をオーバーするデータをカットする
        private void cutAdvOverData()
        {
            // アレルギ歴・副作用歴は100字以上であれば削る
            if (this.advAllergyBox1.Text.Length >= 100)
            {
                this.advAllergyBox1.Text = this.advAllergyBox1.Text.Substring(0, 99);
            }

            if (this.advAllergyBox2.Text.Length >= 100)
            {
                this.advAllergyBox2.Text = this.advAllergyBox2.Text.Substring(0, 99);
            }

            // 注射歴・内服歴は1000字以上であれば削る
            if (this.advInjectBox.Text.Length >= 1000)
            {
                this.advInjectBox.Text = this.advInjectBox.Text.Substring(0, 999);
            }

            if (this.advDrugBox.Text.Length >= 1000)
            {
                this.advDrugBox.Text = this.advDrugBox.Text.Substring(0, 999);
            }

            // 持参薬は1000字以上であれば削る
            if (this.advBringBox.Text.Length >= 1000)
            {
                this.advBringBox.Text = this.advBringBox.Text.Substring(0, 999);
            }

            // 重複投薬・配合禁忌は100字以上であれば削る
            if (this.advDoubleBox.Text.Length >= 100)
            {
                this.advDoubleBox.Text = this.advDoubleBox.Text.Substring(0, 99);
            }

            if (this.advTabooBox.Text.Length >= 100)
            {
                this.advTabooBox.Text = this.advTabooBox.Text.Substring(0, 99);
            }

            // 患者指導・相談内容は1000字以上であれば削る
            if (this.advAdvBox.Text.Length >= 1000)
            {
                this.advAdvBox.Text = this.advAdvBox.Text.Substring(0, 999);
            }
        }

        // 退院時指導の文字数制限をオーバーするデータをカットする
        private void cutDisOverData()
        {
            // アレルギ歴・副作用歴は100字以上であれば削る
            if (this.disAllergyBox1.Text.Length >= 100)
            {
                this.disAllergyBox1.Text = this.disAllergyBox1.Text.Substring(0, 99);
            }

            if (this.disAllergyBox2.Text.Length >= 100)
            {
                this.disAllergyBox2.Text = this.disAllergyBox2.Text.Substring(0, 99);
            }

            // 退院時処方・指導内容・特記事項は1000字以上であれば削る
            if (this.disDrugBox.Text.Length >= 1000)
            {
                this.disDrugBox.Text = this.disDrugBox.Text.Substring(0, 999);
            }

            if (this.disAdvBox.Text.Length >= 1000)
            {
                this.disAdvBox.Text = this.disAdvBox.Text.Substring(0, 999);
            }

            if (this.disStNoteBox.Text.Length >= 1000)
            {
                this.disStNoteBox.Text = this.disStNoteBox.Text.Substring(0, 999);
            }

            // 服薬注意点・調剤工夫・その他は500字以上であれば削る
            if (this.disPtNoteBox1.Text.Length >= 500)
            {
                this.disPtNoteBox1.Text = this.disPtNoteBox1.Text.Substring(0, 499);
            }

            if (this.disPtNoteBox2.Text.Length >= 500)
            {
                this.disPtNoteBox2.Text = this.disPtNoteBox2.Text.Substring(0, 499);
            }

            if (this.disPtNoteBox3.Text.Length >= 500)
            {
                this.disPtNoteBox3.Text = this.disPtNoteBox3.Text.Substring(0, 499);
            }
        }

        private void advInOutButton1_CheckedChanged(object sender, EventArgs e)
        {
            // 入院期間の選択を外す
            this.advAdmDateBox.Text = "";
        }

        private void advInOutButton2_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void advReadDataButton_Click(object sender, EventArgs e)
        {
            if (this.Pat.Id.Length == 0)
            {
                MessageBox.Show("患者を指定してください");
                return;
            }

            if (MessageBox.Show("データを取り込みますか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }

            // 基準日はデフォルトで１か月前
            // 入院日が指定されていれば、その日とする
            string crit_date = DateTime.Now.AddMonths(-1).ToString("yyyyMMdd");

            if (advAdmDateBox.Text.Length >= 10)
            {
                crit_date = advAdmDateBox.Text.Substring(0, 10).Replace("/", "");
            }

            // アレルギ歴・副作用歴・重複投薬・配合禁忌・指導内容・同意の前回データ取得
            // →　指導内容は引用対象から外し、SOAPから最新データを取得する

            this.advAllergyBox1.Text = "なし";
            this.advAllergyBox2.Text = "なし";
            this.advDoubleBox.Text = "なし";
            this.advTabooBox.Text = "なし";

            List<DrugAdv> list1 = DrugAdv.GetListById(this.Pat.Id, "ADV_DATE desc, DRUG_ADV_ID desc");

            foreach (DrugAdv obj in list1)
            {
                if (!obj.Status.Equals(1)) continue;

                this.advAllergyBox1.Text = obj.Allergy1;
                this.advAllergyBox2.Text = obj.Allergy2;
                this.advDoubleBox.Text = obj.DoubleDrug;
                this.advTabooBox.Text = obj.TabooMix;

                break;
            }

            // アレルギ歴・副作用歴は前回データが無ければ電子カルテから取得
            if (this.advAllergyBox1.Text.Length == 0 || this.advAllergyBox2.Text.Length == 0)
            {
                List<AllergyData> list2 = AllergyData.GetList(this.Pat.Id);

                bool allergy1_flg = (this.advAllergyBox1.Text.Length == 0) ? true : false;
                bool allergy2_flg = (this.advAllergyBox2.Text.Length == 0) ? true : false;

                foreach (AllergyData obj in list2)
                {
                    if (obj.GroupCode.Equals("1") || obj.GroupCode.Equals("3"))
                    {
                        if (allergy1_flg)
                        {
                            if (obj.Cont.Length > 0)
                            {
                                this.advAllergyBox1.Text += obj.Cont + Environment.NewLine;
                            }
                            else
                            {
                                this.advAllergyBox1.Text += obj.Name + Environment.NewLine;
                            }
                        }
                    }
                    else if (obj.GroupCode.Equals("2"))
                    {
                        if (allergy2_flg)
                        {
                            if (obj.Cont.Length > 0)
                            {
                                this.advAllergyBox2.Text += obj.Cont + Environment.NewLine;
                            }
                            else
                            {
                                this.advAllergyBox2.Text += obj.Name + Environment.NewLine;
                            }
                        }
                    }
                }
            }

            List<string> cond_list = new List<string>();

            // 注射歴の取得
            this.advInjectBox.Text = DrugAdv.GetInjectHistory(this.Pat.Id, crit_date);

            // 内服・頓用・外用歴の取得
            this.advDrugBox.Text = DrugAdv.GetDrugHistory1(this.Pat.Id, DateTime.Now.AddMonths(-1).ToString("yyyyMMdd"));

            // 持参薬のデータ取得
            List<BringDrug> list4 = BringDrug.GetListById(this.Pat.Id);

            // 開始日 desc・終了日 desc の順で並べ替える
            list4.Sort((x, y) =>
            {
                int i = y.StartDate - x.StartDate;

                if (i == 0)
                {
                    i = y.EndDate - x.EndDate;
                }

                return i;
            });

            foreach (BringDrug d in list4)
            {
                if (d.DelFlg.Equals(1)) continue;
                if (d.StartDate > int.Parse(advAdvDateTimePicker.Value.ToString("yyyyMMdd"))) continue;
                if (d.EndDate < int.Parse(crit_date)) continue;

                this.advBringBox.Text += d.Info1 + Environment.NewLine;
            }

            // 指導内容を当日の薬剤師が書いたＳＯＡＰから取得
            List<string> date_list = new List<string>();
            date_list.Add(this.advAdvDateTimePicker.Value.ToString("yyyyMMdd"));

            Dictionary<string, List<SoapHeader>> soap_header_dict = SoapHeader.GetDict(this.Pat.Id, date_list);
            string soap = "";

            foreach (List<SoapHeader> list5 in soap_header_dict.Values)
            {
                foreach (SoapHeader header in list5)
                {
                    // 薬剤師でなければ飛ばす
                    if (!Staff.Load(header.UpStaff).IsDrug &&
                        !Staff.Load(header.RegStaff).IsDrug)
                    {
                        continue;
                    }

                    foreach (SoapDetail data in header.DetailList)
                    {
                        if (soap.Length > 0)
                        {
                            soap += Environment.NewLine + Environment.NewLine;
                        }

                        if (data.Kind.Equals("1"))
                        {
                            // S
                            soap += "Ｓ：";
                        }
                        else if (data.Kind.Equals("2"))
                        {
                            // O
                            soap += "Ｏ：";
                        }
                        else if (data.Kind.Equals("3"))
                        {
                            // A
                            soap += "Ａ：";
                        }
                        else if (data.Kind.Equals("4"))
                        {
                            // P
                            soap += "Ｐ：";
                        }
                        else if (data.Kind.Equals("5"))
                        {
                            // F
                            soap += "Ｆ：";
                        }

                        soap += Environment.NewLine;
                        soap += data.Cont.TrimEnd(Environment.NewLine.ToCharArray());
                    }

                    break;
                }

                if (soap.Length > 0) break;
            }

            // すでに記載されている場合は、前に改行を追加する
            if (this.advAdvBox.Text.Length > 0)
            {
                this.advAdvBox.Text += Environment.NewLine + Environment.NewLine;
            }

            this.advAdvBox.Text += soap;

            this.cutAdvOverData();
        }

        private void disReadDataButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("データを取り込みますか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }

            string admDate = disAdmDateTimePicker.Value.ToString("yyyyMMdd");

            List<DrugAdv> list1 = DrugAdv.GetListById(this.Pat.Id, "ADV_DATE desc, DRUG_ADV_ID desc");

            // アレルギ歴・副作用歴の服薬指導データ取得

            this.disAllergyBox1.Text = "なし";
            this.disAllergyBox2.Text = "なし";

            foreach (DrugAdv obj in list1)
            {
                if (!obj.Status.Equals(1)) continue;
                if (!obj.AdmDate.ToString().Equals(admDate)) continue;

                this.disAllergyBox1.Text = obj.Allergy1;
                this.disAllergyBox2.Text = obj.Allergy2;
            }

            // アレルギ歴・副作用歴は服薬指導データが無ければ電子カルテから取得
            if (this.disAllergyBox1.Text.Length == 0 || this.disAllergyBox2.Text.Length == 0)
            {
                List<AllergyData> list2 = AllergyData.GetList(this.Pat.Id);

                bool allergy1_flg = (this.disAllergyBox1.Text.Length == 0) ? true : false;
                bool allergy2_flg = (this.disAllergyBox2.Text.Length == 0) ? true : false;

                foreach (AllergyData obj in list2)
                {
                    if (obj.GroupCode.Equals("1") || obj.GroupCode.Equals("3"))
                    {
                        if (allergy1_flg)
                        {
                            if (obj.Cont.Length > 0)
                            {
                                this.disAllergyBox1.Text += obj.Cont + Environment.NewLine;
                            }
                            else
                            {
                                this.disAllergyBox1.Text += obj.Name + Environment.NewLine;
                            }
                        }
                    }
                    else if (obj.GroupCode.Equals("2"))
                    {
                        if (allergy2_flg)
                        {
                            if (obj.Cont.Length > 0)
                            {
                                this.disAllergyBox2.Text += obj.Cont + Environment.NewLine;
                            }
                            else
                            {
                                this.disAllergyBox2.Text += obj.Name + Environment.NewLine;
                            }
                        }
                    }
                }
            }

            // 内服・頓用・外用歴の取得
            this.disDrugBox.Text = DrugAdv.GetDrugHistory2(this.Pat.Id, this.disDisDateTimePicker.Value.ToString("yyyyMMdd"), DateTime.Now.AddMonths(-1).ToString("yyyyMMdd"));

            // 指導内容を当日の薬剤師が書いたＳＯＡＰから取得
            List<string> date_list = new List<string>();
            date_list.Add(this.disAdvDateTimePicker.Value.ToString("yyyyMMdd"));

            Dictionary<string, List<SoapHeader>> soap_header_dict = SoapHeader.GetDict(this.Pat.Id, date_list);
            string soap = "";

            foreach (List<SoapHeader> list5 in soap_header_dict.Values)
            {
                foreach (SoapHeader header in list5)
                {
                    // 薬剤師でなければ飛ばす
                    if (!Staff.Load(header.UpStaff).IsDrug &&
                        !Staff.Load(header.RegStaff).IsDrug)
                    {
                        continue;
                    }

                    foreach (SoapDetail data in header.DetailList)
                    {
                        if (soap.Length > 0)
                        {
                            soap += Environment.NewLine + Environment.NewLine;
                        }

                        if (data.Kind.Equals("1"))
                        {
                            // S
                            soap += "Ｓ：";
                        }
                        else if (data.Kind.Equals("2"))
                        {
                            // O
                            soap += "Ｏ：";
                        }
                        else if (data.Kind.Equals("3"))
                        {
                            // A
                            soap += "Ａ：";
                        }
                        else if (data.Kind.Equals("4"))
                        {
                            // P
                            soap += "Ｐ：";
                        }
                        else if (data.Kind.Equals("5"))
                        {
                            // F
                            soap += "Ｆ：";
                        }

                        soap += Environment.NewLine;
                        soap += data.Cont.TrimEnd(Environment.NewLine.ToCharArray());
                    }

                    break;
                }

                if (soap.Length > 0) break;
            }

            // すでに記載されている場合は、前に改行を追加する
            if (this.disAdvBox.Text.Length > 0)
            {
                this.disAdvBox.Text += Environment.NewLine + Environment.NewLine;
            }

            this.disAdvBox.Text += soap;

            this.cutDisOverData();
        }

        private void advTemplateButton_Click(object sender, EventArgs e)
        {
            FormDrugAdvTemplate fat1 = new FormDrugAdvTemplate(this, 1);
            fat1.ShowDialog();
        }

        private void disTemplateButton_Click(object sender, EventArgs e)
        {
            FormDrugAdvTemplate fat1 = new FormDrugAdvTemplate(this, 2);
            fat1.ShowDialog();
        }

        private void advOutFilterBox_CheckedChanged(object sender, EventArgs e)
        {
            this.formatAdvHistory();
        }

        private void advAdmDateFilterBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.formatAdvHistory();
        }

        private void advDeptFilterBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.formatAdvHistory();
        }

        private void advStaffFilterBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.formatAdvHistory();
        }

        private void advInActiveBox_CheckedChanged(object sender, EventArgs e)
        {
            this.formatAdvHistory();
        }

        private void disInActiveBox_CheckedChanged(object sender, EventArgs e)
        {
            this.formatDisHistory();
        }

        private void changeAdvMode(string Mode)
        {
            if (Mode == "新規")
            {
//                this.clearAdvData();

                this.advMode = Mode;
                this.advModeLabel.Text = Mode;
                this.advModeLabel.BackColor = Color.LightYellow;

//                this.advIdBox.Text = "";
                this.clearAdvData();

                this.advRegButton.Enabled = true;
            }
            else if (Mode == "修正")
            {
                this.advMode = Mode;
                this.advModeLabel.Text = Mode;
                this.advModeLabel.BackColor = Color.LightPink;

                this.showAdvData();

                this.advRegButton.Enabled = true;

                if (this.advPrintButton.Tag != null)
                {
                    this.advPrintButton.Enabled = true;
                }
            }
            else if (Mode == "参照")
            {
                this.advMode = Mode;
                this.advModeLabel.Text = Mode;
                this.advModeLabel.BackColor = Color.White;

                this.showAdvData();

                this.advRegButton.Enabled = false;

                if (this.advPrintButton.Tag != null)
                {
                    this.advPrintButton.Enabled = true;
                }
            }

            if (!LoginUser.IsDrug)
            {
                advRegButton.Enabled = false;
                advBringDrugButton.Enabled = false;
                advTemplateButton.Enabled = false;
                advClearButton.Enabled = false;
            }
        }

        private void changeDisMode(string Mode)
        {
            if (Mode == "新規")
            {
//                this.clearDisData();

                this.disMode = Mode;
                this.disModeLabel.Text = Mode;
                this.disModeLabel.BackColor = Color.LightYellow;

//                this.disIdBox.Text = "";
                this.clearDisData();

                this.disRegButton.Enabled = true;
                this.disPrintButton.Enabled = false;
            }
            else if (Mode == "修正")
            {
                this.disMode = Mode;
                this.disModeLabel.Text = Mode;
                this.disModeLabel.BackColor = Color.LightPink;

                this.showDisData();

                this.disRegButton.Enabled = true;

                if (this.disPrintButton.Tag != null)
                {
                    this.disPrintButton.Enabled = true;
                }
            }
            else if (Mode == "参照")
            {
                this.disMode = Mode;
                this.disModeLabel.Text = Mode;
                this.disModeLabel.BackColor = Color.White;

                this.showDisData();

                this.disRegButton.Enabled = false;

                if (this.disPrintButton.Tag != null)
                {
                    this.disPrintButton.Enabled = true;
                }
            }

            if (!LoginUser.IsDrug)
            {
                disRegButton.Enabled = false;
                disClearButton.Enabled = false;
            }
        }

        private void advRegButton_Click(object sender, EventArgs e)
        {
            List<string> errs = new List<string>();

            if (this.Pat.Id.Length == 0)
            {
                errs.Add("患者を指定してください");
            }

            if (!this.advInOutButton1.Checked && !this.advInOutButton2.Checked)
            {
                errs.Add("入外を指定してください");
            }
            else if (this.advInOutButton2.Checked && this.advAdmDateBox.Text.Length < 10)
            {
                errs.Add("入院日を選んでください");
            }

            if (this.advDeptBox.Text.Length == 0)
            {
                errs.Add("診療科を選んでください");
            }

            if (this.advPdfBox.Checked)
            {
                errs.Add("既にPDF化されている記録は保存できません");
            }

            if (this.advAdvDateTimePicker.Value <= DateTime.Now.AddDays(-4))
            {
                errs.Add("４日以上前の記録は保存できません");
            }

            if (errs.Count > 0)
            {
                MessageBox.Show(AppString.ConcatList(errs, Environment.NewLine));
                return;
            }

            string admDate = "0";

            // 入院かつ入院日が入っている場合
            if (this.advInOutButton2.Checked && advAdmDateBox.Text.Length >= 10)
            {
                admDate = advAdmDateBox.Text.Substring(0, 10).Replace("/", "");
            }

            string deptId = "0";

            if (advDeptBox.Text.Contains(" "))
            {
                deptId = advDeptBox.Text.Split(' ')[0].Trim();
            }

            this.cutAdvOverData();

            DrugAdv obj = new DrugAdv();

            int.TryParse(this.advIdBox.Text, out obj.SEQ);
            obj.PtId = this.Pat.Id;
            obj.AdvDate = int.Parse(advAdvDateTimePicker.Value.ToString("yyyyMMdd"));
            obj.InOut = this.advInOutButton1.Checked ? 1 : 2;
            int.TryParse(admDate, out obj.AdmDate);
            int.TryParse(deptId, out obj.Dept);
            obj.Allergy1 = this.advAllergyBox1.Text;
            obj.Allergy2 = this.advAllergyBox2.Text;
            obj.Inject = this.advInjectBox.Text;
            obj.Drug = this.advDrugBox.Text;
            obj.BringDrug = this.advBringBox.Text;
            obj.DoubleDrug = this.advDoubleBox.Text;
            obj.TabooMix = this.advTabooBox.Text;
            obj.Adv = this.advAdvBox.Text;

            if (!obj.Save().ErrExist)
            {
                MessageBox.Show("登録しました");
            }

            this.clearAdvData();
            this.makeAdvHistory();
        }

        private void disRegButton_Click(object sender, EventArgs e)
        {
            List<string> errs = new List<string>();

            if (this.Pat.Id.Length == 0)
            {
                errs.Add("患者を指定してください");
            }

            if (this.disDeptBox.Text.Length == 0)
            {
                errs.Add("診療科を選んでください");
            }

            if (this.disPdfBox.Checked)
            {
                errs.Add("既にPDF化されている記録は保存できません");
            }

            if (this.disDisDateTimePicker.Value <= DateTime.Now.AddDays(-4))
            {
                errs.Add("退院後４日以上経過した記録は保存できません");
            }

            if (errs.Count > 0)
            {
                MessageBox.Show(AppString.ConcatList(errs, Environment.NewLine));
                return;
            }

            string deptId = "0";

            if (disDeptBox.Text.Contains(" "))
            {
                deptId = disDeptBox.Text.Split(' ')[0].Trim();
            }

            this.cutDisOverData();

            DrugAdvDischarge obj = new DrugAdvDischarge();

            int.TryParse(this.disIdBox.Text, out obj.SEQ);
            obj.PtId = this.Pat.Id;
            obj.AdvDate = int.Parse(disAdvDateTimePicker.Value.ToString("yyyyMMdd"));
            obj.AdmDate = int.Parse(disAdmDateTimePicker.Value.ToString("yyyyMMdd"));
            obj.DisDate = int.Parse(disDisDateTimePicker.Value.ToString("yyyyMMdd"));
            int.TryParse(deptId, out obj.Dept);
            obj.Allergy1 = this.disAllergyBox1.Text;
            obj.Allergy2 = this.disAllergyBox2.Text;
            obj.Drug = this.disDrugBox.Text;
            obj.Adv = this.disAdvBox.Text;
            obj.StNote = this.disStNoteBox.Text;
            obj.PtNote1 = this.disPtNoteBox1.Text;
            obj.PtNote2 = this.disPtNoteBox2.Text;
            obj.PtNote3 = this.disPtNoteBox3.Text;
            obj.Status = this.disCompleteBox.Checked ? 1 : 2;

            if (!obj.Save().ErrExist)
            {
                if (disCompleteBox.Checked)
                {
                    DialogResult dr = MessageBox.Show("登録しました。「退院時のお知らせ」を印刷しますか？\r\n Yes(はい): 印刷する\r\n No(いいえ): プレビューする", "印刷", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (dr == DialogResult.Yes)
                    {
                        printDialog1.PrinterSettings = new System.Drawing.Printing.PrinterSettings();

                        if (printDialog1.ShowDialog() == DialogResult.OK)
                        {
                            printDocument2.PrinterSettings = printDialog1.PrinterSettings;
                            printDocument2.Print();
                        }
                    }
                    else if (dr == DialogResult.No)
                    {
                        printPreviewDialog1.Document = printDocument2;
                        printPreviewDialog1.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("登録しました");
                }
            }

            this.clearDisData();
            this.makeDisHistory();
        }

        private void advClearButton_Click(object sender, EventArgs e)
        {
            this.changeAdvMode("新規");
        }

        private void disClearButton_Click(object sender, EventArgs e)
        {
            this.changeDisMode("新規");
        }

        private void advHistoryContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (advHistoryGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow tmpRow = advHistoryGridView.SelectedRows[0];
                DrugAdv d = (DrugAdv)(tmpRow.Cells["Obj"].Value);

                if (d.Editable && LoginUser.IsDrug)
                {
                    advHistoryContextMenuStrip.Items[0].Enabled = true;
                    advHistoryContextMenuStrip.Items[1].Enabled = true;
                    advHistoryContextMenuStrip.Items[2].Enabled = true;
                }
                else
                {
                    advHistoryContextMenuStrip.Items[0].Enabled = false;
                    advHistoryContextMenuStrip.Items[1].Enabled = false;
                    advHistoryContextMenuStrip.Items[2].Enabled = false;
                }
            }
        }

        private void disHistoryContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (disHistoryGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow tmpRow = disHistoryGridView.SelectedRows[0];
                DrugAdvDischarge d = (DrugAdvDischarge)(tmpRow.Cells["Obj"].Value);

                if (d.Editable && LoginUser.IsDrug)
                {
                    disHistoryContextMenuStrip.Items[0].Enabled = true;
                    disHistoryContextMenuStrip.Items[1].Enabled = true;
                    disHistoryContextMenuStrip.Items[2].Enabled = true;
                }
                else
                {
                    disHistoryContextMenuStrip.Items[0].Enabled = false;
                    disHistoryContextMenuStrip.Items[1].Enabled = false;
                    disHistoryContextMenuStrip.Items[2].Enabled = false;
                }
            }
        }

        private void advHistoryGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow r = advHistoryGridView.Rows[e.RowIndex];
            DrugAdv d = (DrugAdv)r.Cells["Obj"].Value;

            if (d.Editable && LoginUser.IsDrug)
            {
                DialogResult dr = MessageBox.Show("修正モードで開きますか？\r\n Yes(はい) 修正モード\r\n No(いいえ) 参照モード", "確認", MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    this.changeAdvMode("修正");
                }
                else
                {
                    this.changeAdvMode("参照");
                }
            }
            else
            {
                this.changeAdvMode("参照");
            }
        }

        private void disHistoryGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow r = disHistoryGridView.Rows[e.RowIndex];
            DrugAdvDischarge d = (DrugAdvDischarge)r.Cells["Obj"].Value;

            if (d.Editable && LoginUser.IsDrug)
            {
                DialogResult dr = MessageBox.Show("修正モードで開きますか？\r\n Yes(はい) 修正モード\r\n No(いいえ) 参照モード", "確認", MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    this.changeDisMode("修正");
                }
                else
                {
                    this.changeDisMode("参照");
                }
            }
            else
            {
                this.changeDisMode("参照");
            }
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.changeAdvMode("新規");
        }

        private void newToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.changeDisMode("新規");
        }

        private void modifyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.changeAdvMode("修正");
        }

        private void modifyToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.changeDisMode("修正");
        }

        private void delToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (advHistoryGridView.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("削除しますか？", "削除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (this.advHistoryGridView.SelectedRows.Count > 0)
                    {
                        int seq = 0;

                        if (int.TryParse(advHistoryGridView.SelectedRows[0].Cells["DRUG_ADV_ID"].Value.ToString(), out seq))
                        {
                            StdReturn sr = DrugAdv.Delete(seq);

                            if (!sr.ErrExist && sr.IntValue > 0)
                            {
                                MessageBox.Show("削除しました");
                            }

                            this.clearAdvData();
                            this.makeAdvHistory();
                        }
                    }
                }
            }
        }

        private void delToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (disHistoryGridView.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("削除しますか？", "削除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (this.disHistoryGridView.SelectedRows.Count > 0)
                    {
                        int seq = 0;

                        if (int.TryParse(disHistoryGridView.SelectedRows[0].Cells["ID"].Value.ToString(), out seq))
                        {
                            StdReturn sr = DrugAdvDischarge.Delete(seq);

                            if (!sr.ErrExist && sr.IntValue > 0)
                            {
                                MessageBox.Show("削除しました");
                            }

                            this.clearDisData();
                            this.makeDisHistory();
                        }
                    }
                }
            }
        }

        private void advCommentButton_Click(object sender, EventArgs e)
        {
            FormDrugAdvComment f = new FormDrugAdvComment(this.Pat.Id);
            f.ShowDialog(this);
        }

        private void advBringDrugButton_Click(object sender, EventArgs e)
        {
            Launcher.BringDrug(this.Pat.Id);
        }

        private void advPrintButton_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("印刷しますか？\r\n Yes(はい): 印刷する\r\n No(いいえ): プレビューする", "印刷", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            pageNumber = 1;
            printStr = "";
            printMode = "3";

            if (dr == DialogResult.Yes)
            {
                printDialog1.PrinterSettings = new System.Drawing.Printing.PrinterSettings();

                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                    printDocument1.Print();
                }
            }
            else if (dr == DialogResult.No)
            {
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
            }

            pageNumber = 1;
            printStr = "";
            printMode = "";
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (this.advPrintButton.Tag != null && this.advPrintButton.Tag is DrugAdv)
            {
                DrugAdv obj = (DrugAdv)this.advPrintButton.Tag;

                Font fg16 = new Font("ＭＳ ゴシック", 16);
                Font f16 = new Font("ＭＳ 明朝", 16);
                Font f14 = new Font("ＭＳ 明朝", 14);
                Font f12 = new Font("ＭＳ 明朝", 12);
                Font f11 = new Font("ＭＳ 明朝", 11);
                Font f10 = new Font("ＭＳ 明朝", 10);
                Font fg10 = new Font("", 10);
                Font f9 = new Font("ＭＳ 明朝", 9);
                Font f8 = new Font("ＭＳ 明朝", 8);
                Font f7 = new Font("ＭＳ 明朝", 7);

                Pen p1 = new Pen(Brushes.Black, 1);
                Pen p2 = new Pen(Brushes.Black, 2);

                if (printMode == "3")
                {
                    // 1ページ目
                    e.Graphics.DrawString("薬剤管理指導記録", f14, Brushes.Black, 55, 50);
                    e.Graphics.DrawString("ID: " + obj.Pat.Id + " " + obj.Pat.GetInfo1(obj.AdvDate.ToString(), AppDateTime.LANG.ENG, false), f10, Brushes.Black, 240, 55);
                    e.Graphics.DrawString(pageNumber.ToString(), f10, Brushes.Black, 780, 35);

                    /* 服薬指導 */
                    printStr = "";
                    printStr += "【アレルギー歴】\r\n" + obj.Allergy1 + "\r\n\r\n";
                    printStr += "――――――――――――――――――――――――――――――――――――――――――――――――――――――\r\n";
                    printStr += "【副作用歴】\r\n" + obj.Allergy2 + "\r\n\r\n";
                    printStr += "――――――――――――――――――――――――――――――――――――――――――――――――――――――\r\n";
                    printStr += "【注射歴】\r\n" + obj.Inject + "\r\n\r\n";
                    printStr += "――――――――――――――――――――――――――――――――――――――――――――――――――――――\r\n";
                    printStr += "【内服・頓用・外用歴】\r\n" + obj.Drug + "\r\n\r\n";
                    printStr += "――――――――――――――――――――――――――――――――――――――――――――――――――――――\r\n";
                    printStr += "【持参薬】\r\n" + obj.BringDrug + "\r\n\r\n";
                    printStr += "――――――――――――――――――――――――――――――――――――――――――――――――――――――\r\n";
                    printStr += "【重複投薬】\r\n" + obj.DoubleDrug + "\r\n\r\n";
                    printStr += "――――――――――――――――――――――――――――――――――――――――――――――――――――――\r\n";
                    printStr += "【配合禁忌】\r\n" + obj.TabooMix + "\r\n\r\n";
                    printStr += "――――――――――――――――――――――――――――――――――――――――――――――――――――――\r\n";
                    printStr += "【患者指導　相談内容】\r\n" + obj.Adv + "\r\n\r\n";
                    printStr += "――――――――――――――――――――――――――――――――――――――――――――――――――――――\r\n";

                    if (obj.InOut.Equals(2))
                    {
                        printStr += "【入院日】" + DateTimeAgent.DateFormat(obj.AdmDate, DateTimeAgent.DateFormatKind.LONG) + "　";
                    }

                    printStr += "【指導日】" + DateTimeAgent.DateFormat(obj.AdvDate, DateTimeAgent.DateFormatKind.LONG) + "　";
                    printStr += "【作成日】" + DateTimeAgent.DateFormat(obj.SaveDate, DateTimeAgent.DateFormatKind.LONG) + "　";
                    printStr += "【薬剤師】" + obj.StaffName + "\r\n";
                    printStr += "――――――――――――――――――――――――――――――――――――――――――――――――――――――";

                    int line = 1;
                    int n = 1;
                    int index = 0;
                    Encoding enc = Encoding.GetEncoding("Shift_JIS");

                    for (index = 0; index < printStr.Length && line <= 83; index++)
                    {
                        if (printStr[index].Equals('\n') || n >= 112)
                        {
                            line++;
                            n = 1;
                        }
                        else
                        {
                            n += enc.GetByteCount(printStr[index].ToString());
                        }
                    }

                    e.Graphics.DrawString(printStr.Substring(0, index), f9, Brushes.Black, new RectangleF(60, 80, 720, 1050));
                    e.Graphics.DrawRectangle(p1, 55, 75, 730, 1050);

                    if (index < printStr.Length)
                    {
                        printMode = "3-1";
                        printStr = printStr.Substring(index);
                        e.HasMorePages = true;
                    }
                    else
                    {
                        e.HasMorePages = false;
                    }

                    pageNumber++;
                }
                else if (printMode == "3-1")
                {
                    // 2ページ目以降
                    e.Graphics.DrawString("薬剤管理指導記録", f14, Brushes.Black, 55, 50);
                    e.Graphics.DrawString("ID: " + obj.Pat.Id + " " + obj.Pat.GetInfo1(obj.AdvDate.ToString(), AppDateTime.LANG.ENG, false), f10, Brushes.Black, 240, 55);
                    e.Graphics.DrawString(pageNumber.ToString(), f10, Brushes.Black, 780, 35);

                    int line = 1;
                    int n = 1;
                    int index = 0;
                    Encoding enc = Encoding.GetEncoding("Shift_JIS");

                    for (index = 0; index < printStr.Length && line <= 83; index++)
                    {
                        if (printStr[index].Equals('\n') || n >= 112)
                        {
                            line++;
                            n = 1;
                        }
                        else
                        {
                            n += enc.GetByteCount(printStr[index].ToString());
                        }
                    }

                    e.Graphics.DrawString(printStr.Substring(0, index), f9, Brushes.Black, new RectangleF(60, 80, 720, 1050));
                    e.Graphics.DrawRectangle(p1, 55, 75, 730, 1050);

                    if (index < printStr.Length)
                    {
                        printMode = "3-1";
                        printStr = printStr.Substring(index);
                        e.HasMorePages = true;
                    }
                    else
                    {
                        e.HasMorePages = false;
                    }

                    pageNumber++;
                }
            }
        }

        private void disPrintButton_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("印刷しますか？\r\n Yes(はい): 印刷する\r\n No(いいえ): プレビューする", "印刷", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            pageNumber = 1;
            printStr = "";
            printMode = "";

            if (dr == DialogResult.Yes)
            {
                printDialog1.PrinterSettings = new System.Drawing.Printing.PrinterSettings();

                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument2.PrinterSettings = printDialog1.PrinterSettings;
                    printDocument2.Print();
                }
            }
            else if (dr == DialogResult.No)
            {
                printPreviewDialog1.Document = printDocument2;
                printPreviewDialog1.ShowDialog();
            }

            pageNumber = 1;
            printStr = "";
            printMode = "";
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (this.disPrintButton.Tag != null && this.disPrintButton.Tag is DrugAdvDischarge)
            {
                DrugAdvDischarge obj = (DrugAdvDischarge)this.disPrintButton.Tag;

                Font f20 = new Font("", 20);
                Font f16 = new Font("", 16);
                Font f14 = new Font("", 14);
                Font f12 = new Font("", 12);
                Font f10 = new Font("", 10);
                Font f9 = new Font("", 9);

                Pen p1 = new Pen(Brushes.Black, 1);

                e.Graphics.DrawString("退院時のお薬についてのお知らせ", f16, Brushes.Black, 200, 35);
                e.Graphics.DrawString("【ID】　" + obj.Pat.Id + "　" + this.Pat.GetInfo1(obj.AdvDate.ToString(), AppDateTime.LANG.ENG, false), f10, Brushes.Black, 50, 80);
                e.Graphics.DrawString("【退院日】　" + DateTimeAgent.DateFormat(obj.DisDate, DateTimeAgent.DateFormatKind.LONG), f10, Brushes.Black, 50, 100);
                e.Graphics.DrawString("入院中における貴方のお薬に関する特記事項や退院後の注意点についてお知らせします。\r\n※病院・診療所や調剤を受ける薬局などにかかられるとき持参すると、貴方に関する情報を伝えることができます。\r\n※退院時にお持ち帰りになるお薬と内容は、別紙の「薬剤情報提供書」をご参照ください。", f10, Brushes.Black, new RectangleF(50, 130, 700, 80));

                printStr = "";
                printStr += "【退院時にお持ち帰りになるお薬】\r\n" + obj.Drug + "\r\n\r\n";
                printStr += "【退院後の服薬上の注意点】\r\n" + obj.PtNote1 + "\r\n\r\n";
                printStr += "【アレルギーの有無】\r\n" + obj.Allergy1 + "\r\n\r\n";
                printStr += "【薬剤副作用の経験の有無】\r\n" + obj.Allergy2 + "\r\n\r\n";
                printStr += "【調剤上の工夫】\r\n" + obj.PtNote2 + "\r\n\r\n";
                printStr += "【その他】\r\n" + obj.PtNote3 + "\r\n\r\n";
                printStr += "　　　　　担当薬剤師　　" + obj.StaffName + "　　　　　病院名：　真生会富山病院";

                e.Graphics.DrawString(printStr, f10, Brushes.Black, new RectangleF(50, 200, 700, 950));
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("終了しますか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void advHistoryGridView_Resize(object sender, EventArgs e)
        {
            formatAdvHistory();
        }

        private void disHistoryGridView_Resize(object sender, EventArgs e)
        {
            formatDisHistory();
        }

        private void FormDrugAdv_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormDrugAdvList.Remove(this);
        }
    }
}