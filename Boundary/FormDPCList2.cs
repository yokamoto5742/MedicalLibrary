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
    public partial class FormDPCList2 : StdForm1
    {
        DataSet DSet = new DataSet();

        ContextMenuStrip _Menu1 = new ContextMenuStrip();
        ToolStripMenuItem _Item1 = new ToolStripMenuItem();
        ToolStripMenuItem _Item2 = new ToolStripMenuItem();

        DPCHeader _Header = new DPCHeader();

        public DPCHeader Header
        {
            get
            {
                return this._Header;
            }
            set
            {
                this._Header = value;
            }
        }

        public FormDPCList2()
        {
            InitializeComponent();
        }

        public FormDPCList2(DPCHeader _header)
        {
            InitializeComponent();

            this._Header = _header;
            this.Pat = PatBase.Load(_header.Id);
        }

        private void FormDPCList2_Load(object sender, EventArgs e)
        {
            DataTable table = DSet.Tables.Add("List");

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

            table.Columns.Add("サマリ");
            table.Columns.Add("サマリ登録者");

            table.Columns.Add("DPC病名");
            table.Columns.Add("DPC病名登録者");

            table.Columns.Add("診療科");

            table.Columns.Add("医師1");
            table.Columns.Add("医師2");
            table.Columns.Add("担当医");
            table.Columns.Add("メモ");

            table.Columns.Add("登録日");
            table.Columns.Add("共通");
            table.Columns.Add("手術");
            table.Columns.Add("医師");
            table.Columns.Add("看護師（入棟）");
            table.Columns.Add("看護師（退棟）");
            table.Columns.Add("Prrism");

            table.Columns.Add("Obj", typeof(DPCHeader));

            this._Menu1.Opening += new CancelEventHandler(Menu1_Opening);

            this._Item1.Text = "開く";
            this._Item1.Click += new EventHandler(HeaderShow);
            this._Menu1.Items.Add(this._Item1);

            if (this.Owner is FormDPCData2)
            {
                this._Item2.Text = "コピー";
                this._Item2.Click += new EventHandler(DetailCopy);
                this._Menu1.Items.Add(this._Item2);
            }

            this.ListView.ContextMenuStrip = this._Menu1;

            this.ListView.CellDoubleClick += new DataGridViewCellEventHandler(ListView_CellDoubleClick);
        }

        private void FormDPCList2_Shown(object sender, EventArgs e)
        {

            switch (LoginUser.QualId)
            {
                case "1":
                    // 医師
                    // フォントを大きくする
                    this.FontSet(AppFont.F10);

                    break;

                case "16":
                    // 看護師
                    // フォントを大きくする
                    this.FontSet(AppFont.F10);

                    break;

                case "17":
                    // 看護助手
                    // フォントを大きくする
                    this.FontSet(AppFont.F10);

                    break;
            }

            if (this.Pat.Id.Length > 0)
            {
                this.PatSet(this.Pat);
            }
        }

        void Menu1_Opening(object sender, CancelEventArgs e)
        {
            if (this.ListView.SelectedRows.Count == 0) return;

            DPCHeader header = (DPCHeader)this.ListView.SelectedRows[0].Cells["Obj"].Value;

            if (this._Header.Id.Equals(header.Id) &&
                this._Header.StartDate.Equals(header.StartDate) &&
                this._Header.Ward.Equals(header.Ward))
            {
                // 元データと選択行データが同じ場合
                this._Item1.Enabled = false;
                this._Item2.Enabled = false;
            }
            else
            {
                this._Item1.Enabled = true;
                this._Item2.Enabled = true;
            }
        }

        void HeaderShow(object sender, EventArgs e)
        {
            if (this.ListView.SelectedRows.Count == 0) return;

            DPCHeader header = (DPCHeader)this.ListView.SelectedRows[0].Cells["Obj"].Value;

            FormDPCData2 f = new FormDPCData2(header);
            f.Show(this);
        }

        void DetailCopy(object sender, EventArgs e)
        {
            if (this.ListView.SelectedRows.Count == 0) return;

            this._Header = (DPCHeader)this.ListView.SelectedRows[0].Cells["Obj"].Value;

            this.DialogResult = DialogResult.OK;
        }

        void ListView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            DPCHeader header = (DPCHeader)this.ListView.Rows[e.RowIndex].Cells["Obj"].Value;

            if (this._Header.Id.Equals(header.Id) &&
                this._Header.StartDate.Equals(header.StartDate) &&
                this._Header.Ward.Equals(header.Ward))
            {
                // 元データと選択行データが同じ場合
                return;
            }

            FormDPCData2 f = new FormDPCData2(header);
            f.Show(this);
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            this.ListShow();
        }

        public void ListShow()
        {
            if (!DSet.Tables.Contains("List")) return;

            DataTable table = DSet.Tables["List"];
            table.Clear();

            List<PatIn> in_list = PatIn.GetHistory(this.Pat.Id);

            // 対象患者のDPC病名とサマリのリストを取得する
            List<DiagDPC> diag_list = DiagDPC.GetList(in_list);
            List<DischargeSummary> discharge_list = DischargeSummary.GetList(in_list);

            // 対象患者の病棟移動歴を取得する
            List<PatInDPCWard> ward_list = PatInDPCWard.GetList(in_list, true);

            // 入院日の降順、病棟移動番号の昇順で並べ替える
            ward_list.Sort((x, y) =>
            {
                int i = y.InDate.CompareTo(x.InDate);

                if (i == 0)
                {
                    i = x.SEQ3 - y.SEQ3;
                }

                return i;
            });

            // 対象患者のDPCデータを取得する
            List<DPCHeader> header_list = DPCHeader.GetList(this.Pat.Id);


            for (int i = 0; i < ward_list.Count; i++)
            {
                PatInDPCWard obj = ward_list[i];

                DataRow r = table.NewRow();

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

                        // 最終登録日時
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
                string dis_date = obj.OutDate;

                if (i < ward_list.Count - 1)
                {
                    PatInDPCWard obj2 = ward_list[i + 1];

                    if (obj2.Id.Equals(obj.Id) &&
                        obj2.SEQ.Equals(obj.SEQ))
                    {
                        dis_date = "";
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
                    header.EndDate = obj.EndDate;

                    r["Obj"] = header;

                    // 診療科
                    r["診療科"] = DPCDept.GetData(header.Dept).Name;

                    // 担当医
                    r["医師1"] = header.Doctor1;
                    r["医師2"] = header.Doctor2;
                    r["担当医"] = header.DoctorName1 + ((header.DoctorName1.Length > 0 && header.DoctorName2.Length > 0) ? "／" : "") + (header.DoctorName2.Length > 0 ? header.DoctorName2 : "");

                    // メモ
                    r["メモ"] = header.Cont;

                    r["登録日"] = DateTimeAgent.DateFormat(header.SaveDate, DateTimeAgent.DateFormatKind.MD);

                    if (header.Status1.Equals("1"))
                    {
                        r["共通"] = "● " + DateTimeAgent.DateFormat(header.SaveDate1, DateTimeAgent.DateFormatKind.MD);
                    }
                    else if (header.SaveDate1.Length == 8)
                    {
                        r["共通"] = "○ " + DateTimeAgent.DateFormat(header.SaveDate1, DateTimeAgent.DateFormatKind.MD);
                    }

                    if (header.Status5.Equals("1"))
                    {
                        r["手術"] = "● " + DateTimeAgent.DateFormat(header.SaveDate5, DateTimeAgent.DateFormatKind.MD);
                    }
                    else if (header.SaveDate5.Length == 8)
                    {
                        r["手術"] = "○ " + DateTimeAgent.DateFormat(header.SaveDate5, DateTimeAgent.DateFormatKind.MD);
                    }

                    if (header.Status2.Equals("1"))
                    {
                        r["医師"] = "● " + DateTimeAgent.DateFormat(header.SaveDate2, DateTimeAgent.DateFormatKind.MD);
                    }
                    else if (header.SaveDate2.Length == 8)
                    {
                        r["医師"] = "○ " + DateTimeAgent.DateFormat(header.SaveDate2, DateTimeAgent.DateFormatKind.MD);
                    }

                    if (header.Status3.Equals("1"))
                    {
                        r["看護師（入棟）"] = "● " + DateTimeAgent.DateFormat(header.SaveDate3, DateTimeAgent.DateFormatKind.MD);
                    }
                    else if (header.SaveDate3.Length == 8)
                    {
                        r["看護師（入棟）"] = "○ " + DateTimeAgent.DateFormat(header.SaveDate3, DateTimeAgent.DateFormatKind.MD);
                    }

                    if (header.Status4.Equals("1"))
                    {
                        r["看護師（退棟）"] = "● " + DateTimeAgent.DateFormat(header.SaveDate4, DateTimeAgent.DateFormatKind.MD);
                    }
                    else if (header.SaveDate4.Length == 8)
                    {
                        r["看護師（退棟）"] = "○ " + DateTimeAgent.DateFormat(header.SaveDate4, DateTimeAgent.DateFormatKind.MD);
                    }

                    if (header.PrDate.Length == 8)
                    {
                        r["Prrism"] = DateTimeAgent.DateFormat(header.PrDate, DateTimeAgent.DateFormatKind.MD);
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
                    header.EndDate = obj.EndDate;
                    header.Ward = obj.Ward;

                    // 新規登録の場合は 0 とする
                    header.SEQ = 0;

                    r["Obj"] = header;
                }

                table.Rows.Add(r);
            }

            this.ListFormat();
        }

        void ListFormat()
        {
            if (!DSet.Tables.Contains("List")) return;

            DataTable table = DSet.Tables["List"];
            DataView view = new DataView(table);

            view.Sort = "入院日 desc, 番号 desc";

            this.ListView.DataSource = view;

            foreach (DataGridViewColumn c in this.ListView.Columns)
            {
                switch (c.Name)
                {
                    case "入院日":
                        c.Visible = true;
                        c.Width = 55;
                        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;

                    case "退院日":
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
                        c.Width = 48;
                        break;

                    case "担当医":
                        c.Visible = true;
                        c.Width = 55;
                        break;

                    case "メモ":
                        c.Visible = true;
                        c.Width = 75;
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

                    case "Prrism":
                        c.Visible = true;
                        c.Width = 45;
                        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;

                    default:
                        c.Visible = false;
                        break;
                }
            }

            foreach (DataGridViewRow r in this.ListView.Rows)
            {
                DPCHeader header = (DPCHeader)r.Cells["Obj"].Value;

                string code = r.Cells["病棟コード"].Value.ToString();

                if (Dict.WardDict.ContainsKey(code))
                {
                    r.Cells["病棟"].Style.BackColor = Dict.WardDict[code].BackColor;
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

                r.Cells["サマリ"].ToolTipText = Staff.Load(summary_staff).Name;
                r.Cells["DPC病名"].ToolTipText = Staff.Load(diag_staff).Name;

                r.Cells["登録日"].ToolTipText = header.StaffName;
                r.Cells["共通"].ToolTipText = header.StaffName1;
                r.Cells["手術"].ToolTipText = header.StaffName5;
                r.Cells["医師"].ToolTipText = header.StaffName2;
                r.Cells["看護師（入棟）"].ToolTipText = header.StaffName3;
                r.Cells["看護師（退棟）"].ToolTipText = header.StaffName4;
                r.Cells["Prrism"].ToolTipText = header.PrStaffName;
            }

            if (this.Font.Size > 9)
            {
                foreach (DataGridViewColumn c in this.ListView.Columns)
                {
                    c.Width = (int)(c.Width * (float)(this.Font.Size / 9));
                }
            }
        }
    }
}
