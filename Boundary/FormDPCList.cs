using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class FormDPCList : StdForm1
    {
        DataSet DSet = new DataSet();

        ContextMenuStrip _MenuStrip = new ContextMenuStrip();

        /// <summary>
        /// カルテ
        /// </summary>
        ToolStripMenuItem _MenuItem1 = new ToolStripMenuItem();

        /// <summary>
        /// 入院カレンダー
        /// </summary>
        ToolStripMenuItem _MenuItem2 = new ToolStripMenuItem();

        /// <summary>
        /// 様式1
        /// </summary>
        ToolStripMenuItem _MenuItem3 = new ToolStripMenuItem();

        /// <summary>
        /// 様式1 履歴
        /// </summary>
        ToolStripMenuItem _MenuItem4 = new ToolStripMenuItem();

        /// <summary>
        /// DPC病名
        /// </summary>
        ToolStripMenuItem _MenuItem5 = new ToolStripMenuItem();

        /// <summary>
        /// 退院時サマリ
        /// </summary>
        ToolStripMenuItem _MenuItem6 = new ToolStripMenuItem();

        /// <summary>
        /// メッセージ
        /// </summary>
        ToolStripMenuItem _MenuItem7 = new ToolStripMenuItem();


        string ListViewSort1 = "";
        SortOrder ListViewSortOrder1 = SortOrder.Ascending;

        string ListViewSort2 = "";
        SortOrder ListViewSortOrder2 = SortOrder.Ascending;

        public FormDPCList()
        {
            InitializeComponent();
        }

        private void FormDPCList_Load(object sender, EventArgs e)
        {
            // 病棟
            this.WardBox.Items.Add("");

            foreach (Ward ward in Dict.WardDict.Values)
            {
                if (ward.Name.Length == 0) continue;

                this.WardBox.Items.Add(ward.Code + ": " + ward.Name);
            }

            // 担当医
            this.DoctorBox.Init();

            // 退院患者は２週間前～本日
            this.DatePicker21.Value = DateTime.Now.AddDays(-14);

            DataTable table = null;

            for (int i = 1; i <= 2; i++)
            {
                table = DSet.Tables.Add("DataList" + i);

                table.Columns.Add("ID");
                table.Columns.Add("氏名");
                table.Columns.Add("性別");
                table.Columns.Add("年齢");

                table.Columns.Add("入院日");
                table.Columns.Add("退院日");
                table.Columns.Add("入院日数");
                table.Columns.Add("入棟日");
                table.Columns.Add("退棟日");
                table.Columns.Add("入棟日数");
                table.Columns.Add("病棟コード");
                table.Columns.Add("病棟");
                table.Columns.Add("病室");
                table.Columns.Add("番号");
                table.Columns.Add("IsLast", typeof(bool));

                table.Columns.Add("サマリ");
                table.Columns.Add("サマリ登録者");

                table.Columns.Add("DPC病名");
                table.Columns.Add("DPC病名登録者");

                table.Columns.Add("診療科");

                table.Columns.Add("医師1");
                table.Columns.Add("医師2");
                table.Columns.Add("担当医");

                table.Columns.Add("主治医コード");
                table.Columns.Add("主治医");

                table.Columns.Add("メモ");
                table.Columns.Add("診断");

                table.Columns.Add("登録日");
                table.Columns.Add("共通");
                table.Columns.Add("手術");
                table.Columns.Add("医師");
                table.Columns.Add("看護師（入棟）");
                table.Columns.Add("看護師（退棟）");
                table.Columns.Add("Prrism");

                table.Columns.Add("Obj", typeof(DPCHeader));
                table.Columns.Add("選択", typeof(bool));
            }

            // MenuStrip
            this._MenuItem1.Text = "カルテ";
            this._MenuItem1.Click += new EventHandler(_MenuItem1_Click);

            this._MenuItem2.Text = "入院カレンダー";
            this._MenuItem2.Click += new EventHandler(_MenuItem2_Click);

            this._MenuItem3.Text = "様式1";
            this._MenuItem3.Click += new EventHandler(_MenuItem3_Click);

            this._MenuItem4.Text = "様式1 履歴";
            this._MenuItem4.Click += new EventHandler(_MenuItem4_Click);

            this._MenuItem5.Text = "DPC病名";
            this._MenuItem5.Click += new EventHandler(_MenuItem5_Click);

            this._MenuItem6.Text = "退院時サマリ";
            this._MenuItem6.Click += new EventHandler(_MenuItem6_Click);

            this._MenuItem7.Text = "メッセージ";
            this._MenuItem7.Click += new EventHandler(_MenuItem7_Click);

            this._MenuStrip.Items.Add(this._MenuItem1);
            this._MenuStrip.Items.Add(this._MenuItem2);
            this._MenuStrip.Items.Add(this._MenuItem3);
            this._MenuStrip.Items.Add(this._MenuItem4);
            this._MenuStrip.Items.Add(this._MenuItem5);
            this._MenuStrip.Items.Add(this._MenuItem6);
            this._MenuStrip.Items.Add(this._MenuItem7);

            this.ListView1.ContextMenuStrip = this._MenuStrip;
            this.ListView2.ContextMenuStrip = this._MenuStrip;

            this.CSVButton1.Click += new EventHandler(CSVButton_Click);
            this.CSVButton2.Click += new EventHandler(CSVButton_Click);
        }

        public override void FontSet(AppFont f)
        {
            base.FontSet(f);

            this.stdControlFont11.FontSet(f);

            this.ListFormat();
        }

        DPCHeader SelectedDPCHeader()
        {
            DPCHeader header = null;

            if (this._MenuStrip.SourceControl.Name.Equals("ListView1"))
            {
                if (this.ListView1.SelectedRows.Count > 0)
                {
                    header = (DPCHeader)this.ListView1.SelectedRows[0].Cells["Obj"].Value;
                }
            }
            else if (this._MenuStrip.SourceControl.Name.Equals("ListView2"))
            {
                if (this.ListView2.SelectedRows.Count > 0)
                {
                    header = (DPCHeader)this.ListView2.SelectedRows[0].Cells["Obj"].Value;
                }
            }

            return header;
        }

        void _MenuItem1_Click(object sender, EventArgs e)
        {
            DPCHeader header = this.SelectedDPCHeader();

            if (header != null)
            {
                FormControl.FormPat_Show(PatBase.Load(header.Id));
            }
        }

        void _MenuItem2_Click(object sender, EventArgs e)
        {
            DPCHeader header = this.SelectedDPCHeader();

            if (header != null)
            {
                FormInCal2 f = new FormInCal2();
                f.PatSet(header.Pat);
                f.Show();
            }
        }

        void _MenuItem3_Click(object sender, EventArgs e)
        {
            DPCHeader header = this.SelectedDPCHeader();

            if (header != null)
            {
                FormDPCData2 f = new FormDPCData2(header);
                f.Show(this);
            }
        }

        void _MenuItem4_Click(object sender, EventArgs e)
        {
            DPCHeader header = this.SelectedDPCHeader();

            if (header != null)
            {
                FormDPCList2 f = new FormDPCList2();
                f.PatSet(header.Pat);
                f.Show(this);
            }
        }

        void _MenuItem5_Click(object sender, EventArgs e)
        {
            DPCHeader header = this.SelectedDPCHeader();

            if (header != null)
            {
                FormDiagDPC f = new FormDiagDPC();
                f.PatSet(header.Pat);
                f.ShowDialog(this);
            }
        }

        void _MenuItem6_Click(object sender, EventArgs e)
        {
            DPCHeader header = this.SelectedDPCHeader();

            if (header != null)
            {
                PatIn pin = PatIn.GetDataByDate(header.Pat.Id, header.AdmDate);
                FormDischargeSummary f = new FormDischargeSummary(header.Pat.Id, pin.SEQ);
                f.Show();
            }
        }

        void _MenuItem7_Click(object sender, EventArgs e)
        {
            DPCHeader header = this.SelectedDPCHeader();

            if (header != null)
            {
                List<string> send_to_list = new List<string>();

                if (header.Doctor1.Length > 0) send_to_list.Add(Doctor.Load(header.Doctor1).StaffCode.ToString());
                if (header.Doctor2.Length > 0) send_to_list.Add(Doctor.Load(header.Doctor2).StaffCode.ToString());

                FormKarteMessage2 f = new FormKarteMessage2(header.Pat.Id, send_to_list);
                f.Show();
            }
        }

        private void FormDPCList_Shown(object sender, EventArgs e)
        {
            switch (LoginUser.QualId)
            {
                case "99":
                    // 情報室
                    this.CSVButton1.Enabled = true;
                    this.CSVButton2.Enabled = true;
                    this.ImportButton.Enabled = true;
                    this.PrrismButton1.Enabled = true;
                    this.PrrismButton2.Enabled = true;

                    break;

                case "21":
                    // 医事課・診療情報管理士
                    this.CSVButton1.Enabled = true;
                    this.CSVButton2.Enabled = true;
                    this.ImportButton.Enabled = true;
                    this.PrrismButton1.Enabled = true;
                    this.PrrismButton2.Enabled = true;

                    break;

                case "1":
                    // 医師
                    this.DoctorBox.SetDoctor(LoginUser.DoctorId);

                    // デフォルトでは退院一覧を表示する
                    this.TabControl1.SelectedIndex = 1;

                    this.CSVButton1.Enabled = false;
                    this.CSVButton2.Enabled = false;
                    this.ImportButton.Enabled = false;
                    this.PrrismButton1.Enabled = false;
                    this.PrrismButton2.Enabled = false;

                    // フォントを大きくする
                    this.FontSet(AppFont.F10);

                    break;

                case "16":
                    // 看護師
                    this.CSVButton1.Enabled = false;
                    this.CSVButton2.Enabled = false;
                    this.ImportButton.Enabled = false;
                    this.PrrismButton1.Enabled = false;
                    this.PrrismButton2.Enabled = false;

                    // フォントを大きくする
                    this.FontSet(AppFont.F10);

                    break;

                case "17":
                    // 看護助手
                    this.CSVButton1.Enabled = false;
                    this.CSVButton2.Enabled = false;
                    this.ImportButton.Enabled = false;
                    this.PrrismButton1.Enabled = false;
                    this.PrrismButton2.Enabled = false;

                    // フォントを大きくする
                    this.FontSet(AppFont.F10);

                    break;

                default:
                    this.CSVButton1.Enabled = false;
                    this.CSVButton2.Enabled = false;
                    this.ImportButton.Enabled = false;
                    this.PrrismButton1.Enabled = false;
                    this.PrrismButton2.Enabled = false;

                    break;
            }

            this.ListShow();
        }

        public void ListShow()
        {
            switch (this.TabControl1.SelectedIndex)
            {
                case 0:
                    this.ListShow(1);
                    break;

                case 1:
                    this.ListShow(2);
                    break;
            }
        }

        void ListShow(int k)
        {
            if (k != 1 && k != 2) return;

            DataGridView list_view;

            // 対象の患者リスト
            List<PatIn> in_list;

            if (k == 1)
            {
                list_view = this.ListView1;

                in_list = PatIn.GetList();
            }
            else
            {
                list_view = this.ListView2;

                if (DatePicker22.Value >= DatePicker21.Value.AddMonths(1))
                {
                    MessageBox.Show("対象期間が長すぎると処理が重くなりますので、１か月以内でお願いします");
                    return;
                }

                in_list = PatIn.GetOutList(
                    DatePicker21.Value.ToString("yyyyMMdd"),
                    DatePicker22.Value.ToString("yyyyMMdd")
                    );
            }

            // 元の選択行とスクロール位置を取得
            string pt_id1 = "";
            string in_date1 = "";
            string seq1 = "";
            int y1 = list_view.FirstDisplayedScrollingRowIndex;

            if (list_view.SelectedRows.Count > 0)
            {
                pt_id1 = list_view.SelectedRows[0].Cells["ID"].Value.ToString();
                in_date1 = list_view.SelectedRows[0].Cells["入院日"].Value.ToString();
                seq1 = list_view.SelectedRows[0].Cells["番号"].Value.ToString();
            }

            DataTable table;

            table = DSet.Tables["DataList" + k];
            table.Clear();

            // 対象患者のDPC病名とサマリのリストを取得する
            List<DiagDPC> diag_list = DiagDPC.GetList(in_list);
            List<DischargeSummary> discharge_list = DischargeSummary.GetList(in_list);

            // 対象患者の病棟移動歴を取得する
            List<PatInDPCWard> ward_list = PatInDPCWard.GetList(in_list, true);

            // 対象患者のDPCデータを取得する
            List<DPCHeader> header_list = DPCHeader.GetList(ward_list.ConvertAll((x) => {
                PatIn pin = new PatIn();
                pin.Id = x.Id;
                pin.DoDate = x.DoDate;
                return pin;
            }));


            // 入院のべ数
            List<PatIn> pt_list = new List<PatIn>();

            for (int i = 0; i < ward_list.Count; i++)
            {
                PatInDPCWard obj = ward_list[i];

                if (pt_list.FindAll((x) => { return x.Id.Equals(obj.Id) && x.SEQ.Equals(obj.SEQ); }).Count == 0)
                {
                    pt_list.Add(obj);
                }

                DataRow r = table.NewRow();

                r["ID"] = obj.Id;
                r["氏名"] = obj.Name;
                r["性別"] = obj.Sex;
                r["年齢"] = obj.Age;

                r["入院日"] = DateTimeAgent.DateFormat(obj.InDate, DateTimeAgent.DateFormatKind.SHORT);
                r["退院日"] = DateTimeAgent.DateFormat(obj.OutDate, DateTimeAgent.DateFormatKind.SHORT);
                r["入院日数"] = obj.Days;

                r["入棟日"] = DateTimeAgent.DateFormat(obj.DoDate, DateTimeAgent.DateFormatKind.SHORT);
                r["退棟日"] = DateTimeAgent.DateFormat(obj.EndDate, DateTimeAgent.DateFormatKind.SHORT);
                r["入棟日数"] = obj.WardDays;

                r["病棟コード"] = obj.Ward;
                r["病棟"] = obj.WardName;
                r["病室"] = obj.Room;
                r["番号"] = obj.SEQ3;
                r["IsLast"] = obj.IsLast;

                foreach (DischargeSummary d in discharge_list)
                {
                    if (d.PtId.Equals(obj.Id) && d.InSEQ.Equals(obj.SEQ))
                    {
                        if (d.InputCheck.Equals(1))
                        {
                            r["サマリ"] = "▲ " + DateTime.Parse(DateTimeAgent.DateFormat((d.UpDate > d.RegDate ? d.UpDate : d.RegDate), DateTimeAgent.DateFormatKind.LONG)).ToString("M/dd").PadLeft(5, ' ');
                        }
                        else if (d.InputCheck.Equals(2))
                        {
                            r["サマリ"] = "● " + DateTime.Parse(DateTimeAgent.DateFormat((d.UpDate > d.RegDate ? d.UpDate : d.RegDate), DateTimeAgent.DateFormatKind.LONG)).ToString("M/dd").PadLeft(5, ' ');
                        }
                        else
                        {
                            r["サマリ"] = "△ " + DateTime.Parse(DateTimeAgent.DateFormat((d.UpDate > d.RegDate ? d.UpDate : d.RegDate), DateTimeAgent.DateFormatKind.LONG)).ToString("M/dd").PadLeft(5, ' ');
                        }

                        r["サマリ登録者"] = d.UpStaffName.Length > 0 ? d.UpStaffCode : d.RegStaffCode;

                        break;
                    }
                }

                // DPC主病名がチェックされているか
                bool diag_flg = false;

                // 最終登録日時
                int diag_date = 0;

                // 登録者
                string diag_staff = "";

                foreach (DiagDPC d in diag_list)
                {
                    // 削除されていれば飛ばす
                    if (d.DeleteFlg) continue;

                    if (d.PtId.Equals(obj.Id) && d.InSEQ.Equals(obj.SEQ))
                    {
                        // DPC主病名がチェックされている場合
                        if (d.MainFlg)
                        {
                            diag_flg = true;
                        }

                        // 最終登録日時・登録者
                        if (d.RegDate >= diag_date || d.UpDate >= diag_date)
                        {
                            diag_date = d.UpDate > d.RegDate ? d.UpDate : d.RegDate;
                            diag_staff = d.UpStaffName.Length > 0 ? d.UpStaffCode : d.RegStaffCode;
                        }
                    }
                }

                // DPC主病名がチェックされている
                if (diag_flg)
                {
                    r["DPC病名"] = "○ " + DateTime.Parse(DateTimeAgent.DateFormat(diag_date, DateTimeAgent.DateFormatKind.LONG)).ToString("M/dd").PadLeft(5, ' ');
                    r["DPC病名登録者"] = diag_staff;
                }

                // 対象の DPCHeader の有無
                bool b = false;

                // DPC処理上の退院日（転棟ならば退院日は空にする）
                string dis_date = AppString.IsDate(obj.OutDate) ? obj.OutDate : "";

                if (i < ward_list.Count - 1)
                {
                    PatInDPCWard obj2 = ward_list[i + 1];

                    if (obj2.Id.Equals(obj.Id) &&
                        obj2.SEQ.Equals(obj.SEQ))
                    {
                        dis_date = "";
                    }
                }

                // DPC処理上の退棟日（入院中で、最後の病棟ならば退棟日は空にする）
                string end_date = AppString.IsDate(obj.EndDate) ? obj.EndDate : "";

                if (k == 1 && obj.IsLast)
                {
                    end_date = "";
                }

                foreach (PatIn pin in in_list)
                {
                    if (pin.Id.Equals(obj.Id) && pin.InDate.Equals(obj.InDate))
                    {
                        r["主治医コード"] = pin.Doctor;
                        r["主治医"] = pin.DoctorName;
                        break;
                    }
                }

                foreach (DPCHeader header in header_list)
                {
                    // 患者ID・入棟日・病棟のいずれかが異なれば飛ばす
                    if (!header.Id.Equals(obj.Id) ||
                        !header.StartDate.Equals(obj.DoDate) ||
                        !header.Ward.Equals(obj.Ward))
                    {
                        continue;
                    }

                    header.DisDate = dis_date;
                    header.EndDate = end_date;

                    r["Obj"] = header;

                    r["診療科"] = DPCDept.GetData(header.Dept).Name;

                    r["医師1"] = header.Doctor1;
                    r["医師2"] = header.Doctor2;
                    r["担当医"] = header.DoctorName1 + ((header.DoctorName1.Length > 0 && header.DoctorName2.Length > 0) ? "／" : "") + (header.DoctorName2.Length > 0 ? header.DoctorName2 : "");

                    r["メモ"] = header.Cont;
                    r["診断"] = header.DiagNames;

                    r["登録日"] = DateTimeAgent.DateFormat(header.SaveDate, DateTimeAgent.DateFormatKind.MDD).PadLeft(5, ' ');

                    if (header.Status1.Equals("1"))
                    {
                        r["共通"] = "● " + DateTimeAgent.DateFormat(header.SaveDate1, DateTimeAgent.DateFormatKind.MDD).PadLeft(5, ' ');
                    }
                    else if (header.SaveDate1.Length == 8)
                    {
                        r["共通"] = "○ " + DateTimeAgent.DateFormat(header.SaveDate1, DateTimeAgent.DateFormatKind.MDD).PadLeft(5, ' ');
                    }

                    if (header.Status5.Equals("1"))
                    {
                        r["手術"] = "● " + DateTimeAgent.DateFormat(header.SaveDate5, DateTimeAgent.DateFormatKind.MDD).PadLeft(5, ' ');
                    }
                    else if (header.SaveDate5.Length == 8)
                    {
                        r["手術"] = "○ " + DateTimeAgent.DateFormat(header.SaveDate5, DateTimeAgent.DateFormatKind.MDD).PadLeft(5, ' ');
                    }

                    if (header.Status2.Equals("1"))
                    {
                        r["医師"] = "● " + DateTimeAgent.DateFormat(header.SaveDate2, DateTimeAgent.DateFormatKind.MDD).PadLeft(5, ' ');
                    }
                    else if (header.SaveDate2.Length == 8)
                    {
                        r["医師"] = "○ " + DateTimeAgent.DateFormat(header.SaveDate2, DateTimeAgent.DateFormatKind.MDD).PadLeft(5, ' ');
                    }

                    if (header.Status3.Equals("1"))
                    {
                        r["看護師（入棟）"] = "● " + DateTimeAgent.DateFormat(header.SaveDate3, DateTimeAgent.DateFormatKind.MDD).PadLeft(5, ' ');
                    }
                    else if (header.SaveDate3.Length == 8)
                    {
                        r["看護師（入棟）"] = "○ " + DateTimeAgent.DateFormat(header.SaveDate3, DateTimeAgent.DateFormatKind.MDD).PadLeft(5, ' ');
                    }

                    if (header.Status4.Equals("1"))
                    {
                        r["看護師（退棟）"] = "● " + DateTimeAgent.DateFormat(header.SaveDate4, DateTimeAgent.DateFormatKind.MDD).PadLeft(5, ' ');
                    }
                    else if (header.SaveDate4.Length == 8)
                    {
                        r["看護師（退棟）"] = "○ " + DateTimeAgent.DateFormat(header.SaveDate4, DateTimeAgent.DateFormatKind.MDD).PadLeft(5, ' ');
                    }

                    if (header.PrDate.Length == 8)
                    {
                        r["Prrism"] = DateTimeAgent.DateFormat(header.PrDate, DateTimeAgent.DateFormatKind.MDD).PadLeft(5, ' ');
                    }

                    b = true;
                    break;
                }

                // 一致する DPCHeader が無かった場合は新規作成する
                if (!b)
                {
                    DPCHeader header = new DPCHeader();
                    header.Id = obj.Id;
                    header.AdmDate = obj.InDate;
                    header.DisDate = dis_date;
                    header.StartDate = obj.DoDate;
                    header.EndDate = end_date;
                    header.Ward = obj.Ward;

                    // 新規登録の場合は 0 とする
                    header.SEQ = 0;

                    r["Obj"] = header;
                }

                table.Rows.Add(r);
            }

            this.ListFormat(k);

            // 元の選択行・スクロール位置をセット
            if (pt_id1.Length > 0 && in_date1.Length > 0 && seq1.Length > 0)
            {
                foreach (DataGridViewRow r in list_view.Rows)
                {
                    if (r.Cells["ID"].Value.ToString().Equals(pt_id1) &&
                        r.Cells["入院日"].Value.ToString().Equals(in_date1) &&
                        r.Cells["番号"].Value.ToString().Equals(seq1))
                    {
                        r.Selected = true;
                        break;
                    }
                }
            }

            if (y1 >= 0 && y1 < list_view.RowCount)
            {
                list_view.FirstDisplayedScrollingRowIndex = y1;
            }
        }

        void ListFormat()
        {
            switch (this.TabControl1.SelectedIndex)
            {
                case 0:
                    this.ListFormat(1);
                    break;

                case 1:
                    this.ListFormat(2);
                    break;
            }
        }

        void ListFormat(int i)
        {
            if (i != 1 && i != 2) return;

            if (!DSet.Tables.Contains("DataList" + i)) return;

            DataGridView list_view;
            DataTable table;
            DataView view;

            table = DSet.Tables["DataList" + i];
            view = new DataView(table);

            List<string> filters = new List<string>();

            if (this.WardBox.Text.Contains(':'))
            {
                filters.Add("(病棟コード = '" + this.WardBox.Text.Split(':')[0] + "')");
            }

            if (this.DoctorBox.GetDoctor().Code > 0)
            {
                filters.Add("(医師1 = '" + this.DoctorBox.GetDoctor().Code + "' or 医師2 = '" + this.DoctorBox.GetDoctor().Code + "')");
            }

            if (this.FilterBox.Text.Length > 0)
            {
                filters.Add("(ID like '%" + this.FilterBox.Text + "%' or 氏名 like '%" + this.FilterBox.Text + "%')");
            }

            if (this.DateBox11.DateInt > 0 && i == 1)
            {
                filters.Add("(入院日 = '" + this.DateBox11.DateValue.ToString("yy/MM/dd") + "')");
            }

            view.RowFilter = AppString.ConcatList(filters, " and ");

            if (i == 2)
            {
                list_view = this.ListView2;

                if (this.ListViewSort2.Length > 0)
                {
                    view.Sort = this.ListViewSort2;
                }
                else
                {
                    if (LoginUser.QualId.Equals("16") || LoginUser.QualId.Equals("17"))
                    {
                        // 16 看護師, 17 看護助手 の場合は、病棟・病室でソート
                        view.Sort = "病棟コード, 病室";
                    }
                    else
                    {
                        view.Sort = "退院日 desc, ID, 番号";
                    }
                }

                if (this.ListViewSortOrder2 == SortOrder.Descending)
                {
                    view.Sort += " desc";
                }
            }
            else
            {
                list_view = this.ListView1;

                if (this.ListViewSort1.Length > 0)
                {
                    view.Sort = this.ListViewSort1;
                }
                else
                {
                    if (LoginUser.QualId.Equals("16") || LoginUser.QualId.Equals("17"))
                    {
                        // 16 看護師, 17 看護助手 の場合は、病棟・病室でソート
                        view.Sort = "病棟コード, 病室";
                    }
                    else
                    {
                        view.Sort = "入院日 desc, ID, 番号";
                    }
                }

                if (this.ListViewSortOrder1 == SortOrder.Descending)
                {
                    view.Sort += " desc";
                }
            }

            list_view.DataSource = view;

            foreach (DataGridViewColumn c in list_view.Columns)
            {
                if (c.Name.Equals("選択"))
                {
                    // 選択カラムは編集可能
                    c.ReadOnly = false;
                }
                else
                {
                    // 他は ReadOnly
                    c.ReadOnly = true;
                }

                switch (c.Name)
                {
                    case "ID":
                        c.Visible = true;
                        c.Width = 45;
                        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;

                    case "氏名":
                        c.Visible = true;
                        c.Width = 80;
                        break;

                    case "年齢":
                        c.Visible = true;
                        c.Width = 28;
                        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;

                    case "入院日":
                        c.Visible = true;
                        c.Width = 55;
                        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;

                    case "退院日":
                        if (i == 1) c.HeaderText = "退院予定";
                        c.Visible = true;
                        c.Width = 55;
                        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;

                    case "入院日数":
                        c.Visible = true;
                        c.Width = 27;
                        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;

                    case "入棟日":
                        c.Visible = true;
                        c.Width = 55;
                        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;

                    case "退棟日":
                        c.Visible = true;
                        c.Width = 55;
                        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;

                    case "入棟日数":
                        c.Visible = true;
                        c.Width = 25;
                        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;

                    case "病棟":
                        c.Visible = true;
                        c.Width = 45;
                        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;

                    case "病室":
                        c.Visible = true;
                        c.Width = 33;
                        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;

                    case "番号":
                        c.Visible = true;
                        c.Width = 22;
                        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;

                    case "サマリ":
                        c.Visible = true;
                        c.Width = 55;
                        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        c.ToolTipText = "▲ 医師, ● 管理士, △ 未チェック";
                        break;

                    case "DPC病名":
                        c.Visible = true;
                        c.Width = 55;
                        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        c.ToolTipText = "○ DPC主病名あり";
                        c.Frozen = true;
                        c.DividerWidth = 1;
                        break;

                    case "診療科":
                        c.Visible = true;
                        c.Width = 38;
                        break;

                    case "担当医":
                        c.Visible = true;
                        c.Width = 38;
                        break;

                    case "メモ":
                        c.Visible = true;
                        c.Width = 65;
                        break;

                    case "診断":
                        c.Visible = true;
                        c.Width = 43;
                        break;

                    case "登録日":
                        c.Visible = true;
                        c.Width = 45;
                        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;

                    case "共通":
                        c.Visible = true;
                        c.Width = 55;
                        break;

                    case "手術":
                        c.Visible = true;
                        c.Width = 55;
                        break;

                    case "医師":
                        c.HeaderText = "Dr";
                        c.Visible = true;
                        c.Width = 55;
                        break;

                    case "看護師（入棟）":
                        c.HeaderText = "Ns入棟";
                        c.Visible = true;
                        c.Width = 55;
                        break;

                    case "看護師（退棟）":
                        c.HeaderText = "Ns退棟";
                        c.Visible = true;
                        c.Width = 55;
                        break;

                    case "選択":
                        c.Width = 30;
                        break;

                    case "Prrism":
                        c.Width = 45;
                        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;

                    default:
                        c.Visible = false;
                        break;
                }
            }

            List<string> pt_list = new List<string>();

            AppDataGridView.SexColor(list_view);

            foreach (DataGridViewRow r in list_view.Rows)
            {
                DPCHeader header = (DPCHeader)r.Cells["Obj"].Value;

                string code = r.Cells["病棟コード"].Value.ToString();

                if (Dict.WardDict.ContainsKey(header.Ward))
                {
                    r.Cells["病棟"].Style.BackColor = Dict.WardDict[header.Ward].BackColor;
                }

                string summary_staff = r.Cells["サマリ登録者"].Value.ToString();
                string diag_staff = r.Cells["DPC病名登録者"].Value.ToString();

                // 自分の資格以外の人が登録した場合は赤字にする
                // （診療情報管理士・医事課の場合のみ）
                if (LoginUser.QualId.Equals("21"))
                {
                    if (header.StaffName2.Length > 0 && !LoginUser.QualId.Equals(Staff.Load(header.StaffCode2).QualCode.ToString()))
                    {
                        r.Cells["医師"].Style.ForeColor = Color.Red;
                    }

                    if (header.StaffName3.Length > 0 && !LoginUser.QualId.Equals(Staff.Load(header.StaffCode3).QualCode.ToString()))
                    {
                        r.Cells["看護師（入棟）"].Style.ForeColor = Color.Red;
                    }

                    if (header.StaffName4.Length > 0 && !LoginUser.QualId.Equals(Staff.Load(header.StaffCode4).QualCode.ToString()))
                    {
                        r.Cells["看護師（退棟）"].Style.ForeColor = Color.Red;
                    }
                }

                r.Cells["ID"].ToolTipText = header.Pat.Id;
                r.Cells["氏名"].ToolTipText = header.Pat.Name;

                r.Cells["サマリ"].ToolTipText = Staff.Load(summary_staff).Name;
                r.Cells["DPC病名"].ToolTipText = Staff.Load(diag_staff).Name;

                r.Cells["登録日"].ToolTipText = header.StaffName;
                r.Cells["共通"].ToolTipText = header.StaffName1;
                r.Cells["手術"].ToolTipText = header.StaffName5;
                r.Cells["医師"].ToolTipText = header.StaffName2;
                r.Cells["看護師（入棟）"].ToolTipText = header.StaffName3;
                r.Cells["看護師（退棟）"].ToolTipText = header.StaffName4;
                r.Cells["Prrism"].ToolTipText = header.PrStaffName;

                // 最終病棟で、担当医と主治医が異なる場合
                bool is_last = (bool)r.Cells["IsLast"].Value;

                if (is_last)
                {
                    string doctor1 = r.Cells["医師1"].Value.ToString();
                    string doctor = r.Cells["主治医コード"].Value.ToString();

                    if (doctor1.Length > 0 && !doctor1.Equals(doctor))
                    {
                        r.Cells["担当医"].Style.ForeColor = Color.Red;
                        r.Cells["担当医"].ToolTipText = r.Cells["主治医"].Value.ToString();
                    }
                }

                if (!pt_list.Contains(header.Id)) pt_list.Add(header.Id);
            }

            if (i == 1)
            {
                this.CountLabel1.Text = pt_list.Count + " 名 / " + list_view.Rows.Count + " 件";
            }
            else
            {
                this.CountLabel2.Text = pt_list.Count + " 名 / " + list_view.Rows.Count + " 件";
            }

            if (this.Font.Size > 9)
            {
                foreach (DataGridViewColumn c in list_view.Columns)
                {
                    c.Width = (int)(c.Width * (float)(this.Font.Size / 9));
                }
            }
        }

        private void ListView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (this.ListView1.Rows[e.RowIndex].Cells["Obj"].Value == null)
            {
                return;
            }

            DPCHeader header = (DPCHeader)this.ListView1.Rows[e.RowIndex].Cells["Obj"].Value;

            FormDPCData2 f = new FormDPCData2(header);
            f.Show(this);
        }

        private void ListView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (this.ListView2.Rows[e.RowIndex].Cells["Obj"].Value == null)
            {
                return;
            }

            DPCHeader header = (DPCHeader)this.ListView2.Rows[e.RowIndex].Cells["Obj"].Value;

            FormDPCData2 f = new FormDPCData2(header);
            f.Show(this);
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            this.ListShow();
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();

            f.Title = "取り込むファイルを選択してください";

            if (Directory.Exists(DPCDir.Proas.Dir))
            {
                f.InitialDirectory = DPCDir.Proas.Dir;
            }

            if (f.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                string[] cols = new string[1];
                string line;

                StreamReader fr = new StreamReader(f.FileName, Encoding.GetEncoding("shift_jis"));

                while ((line = fr.ReadLine()) != null)
                {
                    string[] ss = line.Split('\t');

                    // 最初の行の場合
                    if (ss[0].StartsWith("a"))
                    {
                        cols = ss;
                        continue;
                    }

                    // 2行目以降の場合
                    if (cols != null)
                    {
                        // Header を作る
                        DPCHeader header = new DPCHeader();
                        header.Id = ss[11].TrimStart('0');
                        header.AdmDate = AppString.IsDate(ss[18]) ? ss[18] : "0";
                        header.DisDate = ss[20];
                        header.StartDate = AppString.IsDate(ss[9]) ? ss[9] : header.AdmDate;
                        header.EndDate = ss[10];
                        header.Ward = ss[5];
                        header.Dept = ss[1];
                        header.Status = "1";

                        // すでにデータが存在するか確認
                        DPCHeader header2 = DPCHeader.GetData(header.Id, header.StartDate, header.Ward);
                        DialogResult dr = DialogResult.Yes;

                        if (header2.Status.Equals("1"))
                        {
                            dr = MessageBox.Show("ID " + header.Id + " " + ss[12] + "\r\nすでにデータが存在します。同一項目のデータがある場合は上書きしますか？\r\nYes … 上書きする\r\nNo … 上書きしない\r\nCancel … この方のデータを取り込まない", "確認", MessageBoxButtons.YesNoCancel);

                            // Yes でも No でもなければ飛ばす
                            if (dr != DialogResult.Yes && dr != DialogResult.No)
                            {
                                continue;
                            }
                        }

                        // Detail を作る
                        for (int i = 0; i < ss.Length; i++)
                        {
                            if (ss[i].Length == 0)
                            {
                                continue;
                            }

                            DPCDetail detail = new DPCDetail();

                            detail.Code = cols[i];
                            detail.Cont = ss[i];

                            // a2 診療科
                            if (DPCItem2.Dict.ContainsKey(detail.Code))
                            {
                                DPCItem2 item2 = DPCItem2.Dict[detail.Code];
                                
                                // 固定値の場合は飛ばす
                                if (item2.Fix.Length > 0) continue;

                                // Kind が無い かつ キーにならないものは電子カルテから取得するため飛ばす
                                if (item2.Kind.Length == 0)
                                {
                                    // a9 病棟コード, a16 入棟日, b1 データ識別番号
                                    if (!detail.Code.Equals("a9") &&
                                        !detail.Code.Equals("a16") &&
                                        !detail.Code.Equals("b1"))
                                    {
                                        continue;
                                    }
                                }
                            }

                            // 重複する場合は飛ばす
                            if (header.DetailList.FindAll((x) => { return x.Code.Equals(detail.Code); }).Count > 0)
                            {
                                continue;
                            }

                            if (dr == DialogResult.Yes)
                            {
                                // 上書きする場合
                                header.DetailList.Add(detail);
                            }
                            else if (dr == DialogResult.No)
                            {
                                // 上書きしない場合
                                if (header2.DetailList.FindAll((x) => { return x.Code.Equals(detail.Code); }).Count == 0)
                                {
                                    header.DetailList.Add(detail);
                                }
                            }
                        }

                        header.Save(DPCHeader.SaveMode.IMPORT);
                    }
                }

                fr.Close();

                MessageBox.Show("取込みが完了しました");
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex, true);
            }
        }

        void Prrism(int i)
        {
            try
            {
                List<string> errs = new List<string>();

                List<DPCHeader> list = new List<DPCHeader>();
                DataTable table;

                // まず対象の DPCHeader リストを作る
                table = DSet.Tables["DataList" + i];

                // データ行
                foreach (DataRow r in table.Rows)
                {
                    // 選択されていなければ飛ばす
                    if (!r["選択"].ToString().Equals("true", StringComparison.CurrentCultureIgnoreCase)) continue;

                    DPCHeader header = (DPCHeader)r["Obj"];

                    if (header.Prable)
                    {
                        // PRRISM出力可能な場合
                        list.Add(header);
                    }
                    else
                    {
                        // PRRISM出力できない場合
                        string s = "ID:" + header.Pat.Id + " " + header.Pat.Name.Replace("　", " ").Replace(" ", "") + "（" + header.WardName + "）";

                        if (header.Dept.Length == 0)
                        {
                            s += " 診療科が設定されていません";
                        }

                        errs.Add(s);
                    }
                }

                if (errs.Count > 0)
                {
                    MessageBox.Show("下記のデータは出力できません" + Environment.NewLine + Environment.NewLine +
                        AppString.ConcatList(errs, Environment.NewLine));
                }

                if (list.Count == 0)
                {
                    MessageBox.Show("出力するデータがありません");
                    return;
                }

                // DPCHeader に対応した DPCDetail を取得する
                foreach (DPCDetail detail in DPCDetail.GetList(list))
                {
                    foreach (DPCHeader header in list)
                    {
                        if (header.Id.Equals(detail.Id) &&
                            header.StartDate.Equals(detail.StartDate) &&
                            header.Ward.Equals(detail.Ward))
                        {
                            header.DetailList.Add(detail);
                        }
                    }
                }

                // 出力フォルダがなければ作成する
                if (!Directory.Exists(DPCDir.Prrism.Dir))
                {
                    try
                    {
                        Directory.CreateDirectory(DPCDir.Prrism.Dir);
                    }
                    catch (Exception ex)
                    {
                        LibUtility.Except(ex, false);
                    }
                }

                SaveFileDialog f = new SaveFileDialog();
                f.Title = "ファイルを保存する";

                if (Directory.Exists(DPCDir.Prrism.Dir))
                {
                    f.InitialDirectory = DPCDir.Prrism.Dir;
                }

                f.FileName = @"F11_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
                f.Filter = "テキストファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*";
                f.FilterIndex = 1;

                if (f.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                // 出力したデータの選択チェックを外し、出力日時をセットする

                // データ行
                foreach (DataRow r in table.Rows)
                {
                    // 選択されていなければ飛ばす
                    if (!r["選択"].ToString().Equals("true", StringComparison.CurrentCultureIgnoreCase)) continue;

                    // 選択チェックを外す
                    r["選択"] = false;

                    DPCHeader header = (DPCHeader)r["Obj"];

                    if (header.Prable)
                    {
                        // PRRISM出力日をセットする（デバッグモード以外）
                        if (!AppStat.Debug)
                        {
                            r["Prrism"] = DateTime.Now.ToString("M/dd");
                        }
                    }
                }

                StreamWriter fw = new StreamWriter(f.FileName, false, Encoding.GetEncoding("shift_jis"));

                string line = "";

                foreach (DPCItem2 item in DPCItem2.List)
                {
                    // Label, Button は飛ばす
                    if (item.Box.Equals("Label", StringComparison.CurrentCultureIgnoreCase) ||
                        item.Box.Equals("Button", StringComparison.CurrentCultureIgnoreCase))
                    {
                        continue;
                    }

                    // 末尾がアルファベットのものは飛ばす
                    if (Regex.IsMatch(item.Code, @"[a-z]{1}$"))
                    {
                        continue;
                    }

                    line += item.Code + "\t";
                }

                // 最後のタブは削除
                line = line.TrimEnd('\t');

                fw.WriteLine(line);

                foreach (DPCHeader header in list)
                {
                    line = "";
                    bool b = false;

                    foreach (DPCItem2 item in DPCItem2.List)
                    {
                        // 末尾がアルファベットのものは飛ばす
                        if (Regex.IsMatch(item.Code, @"[a-z]{1}$"))
                        {
                            continue;
                        }

                        // 固定値の場合
                        if (item.Fix.Length > 0)
                        {
                            line += item.Fix + "\t";
                            continue;
                        }

                        // DETAIL よりも HEADER を取得するもの
                        switch (item.Code)
                        {
                            case "a2":
                                // 診療科コード
                                line += header.Dept + "\t";
                                continue;

                            case "a9":
                                // 病棟コード
                                line += header.Ward + "\t";
                                continue;

                            case "a10":
                                // 記入日
                                line += header.SaveDate + "\t";
                                continue;

                            case "a16":
                                // 入棟日
                                line += header.StartDate + "\t";
                                continue;

                            case "a17":
                                // 退棟日
                                line += header.EndDate + "\t";
                                continue;

                            case "b1":
                                // データ識別番号
                                line += header.Pat.Id.PadLeft(10, '0') + "\t";
                                continue;

                            case "b2":
                                // データ識別名
                                line += header.Pat.Name + "\t";
                                continue;

                            case "b3":
                                // 性別
                                line += header.Pat.Sex + "\t";
                                continue;

                            case "b4":
                                // 生年月日
                                line += header.Pat.Birth + "\t";
                                continue;

                            case "b5":
                                // 郵便番号
                                line += header.Pat.Post.Replace("-", "") + "\t";
                                continue;

                            case "c2":
                                // 入院日
                                line += header.AdmDate + "\t";
                                continue;

                            case "c4":
                                // 退院日
                                // 入院中の場合は、退院予定日が入っていても空にする
                                line += (i == 1 ? "" : header.DisDate) + "\t";
                                continue;
                        }

                        b = false;

                        foreach (DPCDetail detail in header.DetailList)
                        {
                            if (detail.Code.Equals(item.Code))
                            {
                                if (detail.Code.Equals("d141"))
                                {
                                    // がん Stage 部位
                                    foreach (DPCSubItem2 item2 in item.SubItemList)
                                    {
                                        if (detail.Cont.Equals(item2.Code))
                                        {
                                            line += item2.Name + "\t";
                                            b = true;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    line += detail.Cont + "\t";
                                    b = true;
                                }

                                break;
                            }
                        }

                        if (!b)
                        {
                            line += "\t";
                        }
                    }

                    // 最後のタブは削除
                    line = line.TrimEnd('\t');

                    fw.WriteLine(line);
                }

                fw.Close();

                // PRRISM出力日を記録する（デバッグモード以外）
                if (!AppStat.Debug)
                {
                    DPCHeader.PrSave(list);
                }

                if (MessageBox.Show("出力が完了しました。出力先のフォルダを開きますか？", "確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    FileInfo fi = new FileInfo(f.FileName);
                    Process.Start(fi.Directory.FullName);
                }
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex, true);
            }
        }

        private void PrrismButton1_Click(object sender, EventArgs e)
        {
            this.Prrism(1);
        }

        private void PrrismButton2_Click(object sender, EventArgs e)
        {
            this.Prrism(2);
        }

        void CSVButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            DataGridView view = new DataGridView();
            string file_prefix = "DPC入院一覧";

            if (button.Name.Equals("CSVButton1"))
            {
                view = this.ListView1;
                file_prefix = "DPC入院一覧";
            }
            else if (button.Name.Equals("CSVButton2"))
            {
                view = this.ListView2;
                file_prefix = "DPC退院一覧";
            }

            //SaveFileDialogクラスのインスタンスを作成
            SaveFileDialog sfd = new SaveFileDialog();

            //はじめのファイル名を指定する
            sfd.FileName = file_prefix + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
            //はじめに表示されるフォルダを指定する
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //[ファイルの種類]に表示される選択肢を指定する
            sfd.Filter =
                "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";
            //[ファイルの種類]ではじめに
            //「すべてのファイル」が選択されているようにする
            sfd.FilterIndex = 1;
            //タイトルを設定する
            sfd.Title = "保存先のファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            sfd.RestoreDirectory = true;
            //既に存在するファイル名を指定したとき警告する
            //デフォルトでTrueなので指定する必要はない
            sfd.OverwritePrompt = true;
            //存在しないパスが指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            sfd.CheckPathExists = true;

            //ダイアログを表示する
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                // CSVデータを生成する
                List<List<string>> list = new List<List<string>>();

                // 先頭行
                List<string> ss = new List<string>();

                foreach (DataGridViewColumn c in view.Columns)
                {
                    if (c.Visible)
                    {
                        ss.Add(c.Name);
                    }
                }

                list.Add(ss);

                // レコード行
                foreach (DataGridViewRow r in view.Rows)
                {
                    ss = new List<string>();

                    foreach (DataGridViewCell c in r.Cells)
                    {
                        if (c.Visible)
                        {
                            ss.Add(c.Value.ToString());
                        }
                    }

                    list.Add(ss);
                }

                CsvWriter writer = new CsvWriter(sfd.FileName);
                writer.Write(list);

                writer.Dispose();

                if (MessageBox.Show("出力が完了しました。出力先のフォルダを開きますか？", "確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    FileInfo fi = new FileInfo(sfd.FileName);
                    Process.Start(fi.Directory.FullName);
                }
            }
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ListShow();
        }

        private void WardBox_TextChanged(object sender, EventArgs e)
        {
            this.ListFormat();
        }

        private void DoctorBox_TextChanged(object sender, EventArgs e)
        {
            this.ListFormat();
        }

        private void FilterBox_TextChanged(object sender, EventArgs e)
        {
            this.ListFormat();
        }

        private void DateBox11_ValueChanged(object sender, EventArgs e)
        {
            this.ListFormat();
        }

        private void ListView1_Sorted(object sender, EventArgs e)
        {
            this.ListViewSort1 = this.ListView1.SortedColumn.Name;
            this.ListViewSortOrder1 = this.ListView1.SortOrder;

            this.ListFormat(1);
        }

        private void ListView2_Sorted(object sender, EventArgs e)
        {
            this.ListViewSort2 = this.ListView2.SortedColumn.Name;
            this.ListViewSortOrder2 = this.ListView2.SortOrder;

            this.ListFormat(2);
        }
    }
}
