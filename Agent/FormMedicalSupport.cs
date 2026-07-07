using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using MedicalLibrary.Boundary;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class FormMedicalSupport : StdForm1
    {
        bool _ReadOnly = false;

        public bool ReadOnly
        {
            get
            {
                return this._ReadOnly;
            }
            set
            {
                this._ReadOnly = value;

                if (value)
                {
                }
                else
                {
                }
            }
        }

        DataSet DSet = new DataSet();

        /// <summary>
        /// 表示する Inspect
        /// </summary>
        InspectSet _Inspect = new InspectSet();

        /// <summary>
        /// アラートフォーム
        /// </summary>
        FormAlert _FormAlert1;

        /// <summary>
        /// アラート
        /// </summary>
        Alert _Alert = null;

        /// <summary>
        /// 入院情報
        /// </summary>
        PatIn _PatIn = null;

        /// <summary>
        /// DPC情報
        /// </summary>
        DPCHeader _DPCHeader = null;


        public FormMedicalSupport(bool read_only = true)
        {
            InitializeComponent();

            this.ReadOnly = read_only;
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

        private void FormMedicalSupport_Load(object sender, EventArgs e)
        {
            // 電子カルテが起動している場合
            if (Process.GetProcessesByName("InnoKarte").Length > 0)
            {
                this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width - 10, 190);
                this.Height = 560;
            }
            else
            {
                this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width - 10, 160);
                this.Height = 600;
            }

            this.LocationLabel.Text = this.Location.X + "," + this.Location.Y;
            this.SizeLabel.Text = this.Width + "," + this.Height;

            InspectSettings.Init();

            // true: 科に関係なく強制起動
            bool force = false;

            string[] args = Environment.GetCommandLineArgs();

            for (int i = 1; i < args.Length; i++)
            {
                if (args[i].Equals("-force", StringComparison.CurrentCultureIgnoreCase))
                {
                    force = true;
                }
            }

            // Pat.csv 読み込み
            PatBase p = PatBase.ReadPatCSV();

            // 強制起動でない場合
            if (!force)
            {
                if (p.Doctor.Length > 0)
                {
                    // 医師がログインしている場合は アラート・DPC関連入力があるか確認する
                    LibSettings.Init();

                    this._Alert = Alert.GetData(p.Id);
                    this._PatIn = this.GetPatIn(p.Id, p.Doctor);
                    this._DPCHeader = this.GetDPCHeader(p.Id, p.Doctor);

                    // 強制起動でない（自動起動）場合
                    if (!force)
                    {
                        if (this._Alert.Cont.Length == 0 && this._PatIn == null && this._DPCHeader == null)
                        {
                            // アラート・DPC関連入力が無い場合

                            // 外来でない場合は終了
                            if (!p.InOut.Equals("1"))
                            {
                                this.Dispose();
                                return;
                            }

                            if (p.Dept.Length == 0)
                            {
                                this.Dispose();
                                return;
                            }

                            // 対象科でない場合は終了
                            if (!InspectSettings.Current.AutoExecDept.Split(',').ToList<string>().Contains(p.Dept))
                            {
                                this.Dispose();
                                return;
                            }
                        }
                    }
                }
                else
                {
                    // 医師でなければ終了
                    this.Dispose();
                    return;
                }
            }

            LibSettings.Init();

            // 2018/12/31 から 60 か月後（2023/12/31）までは MACS オーダー履歴も取得する
            // （神経内科で６０か月見るものがあるため）
            // → open に移したため不要 2019/06/19
            /*
            if (DateTime.Now.ToString("yyyyMMdd").CompareTo("20231231") <= 0)
            {
                try
                {
                    DB.Db1.Init(LibSettings.Current.DBConnectionString1);
                }
                catch (Exception ex)
                {
                    LibUtility.Except(ex, false);
                }
            }
             */

            this.ctrlDeptBox11.Init();
            this.stdControlPat11.FontSet(AppFont.F10);

            DataTable table = DSet.Tables.Add("Inspect");

            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Name");
            table.Columns.Add("CancerFlg");
            table.Columns.Add("Cont1");
            table.Columns.Add("Cont2");
            table.Columns.Add("Result");

            this.InitShow(Environment.GetCommandLineArgs());
        }

        private void FormMedicalSupport_Shown(object sender, EventArgs e)
        {
            // 初期表示の時は ListView の背景が赤くならないので
            // いったん ListFormat してみる
            this.ListFormat();

            this.stdControlPat11.Focus();
        }

        private void OpeRecordButton_Click(object sender, EventArgs e)
        {
            FormOpeRecord f = new FormOpeRecord();
            f.PatSet(this.Pat);
            f.ShowDialog(this);
        }

        private void DPCButton_Click(object sender, EventArgs e)
        {
            FormDPCList2 f = new FormDPCList2();
            f.PatSet(this.Pat);
            f.Show(this);
        }

        private void DPCDiagButton_Click(object sender, EventArgs e)
        {
            FormDiagDPC f = new FormDiagDPC();
            f.PatSet(this.Pat);
            f.Show(this);
        }

        private void AlertButton_Click(object sender, EventArgs e)
        {
            if (this._FormAlert1 == null || !this._FormAlert1.Created)
            {
                this._FormAlert1 = new FormAlert();
                this._FormAlert1.Show(this);
            }

            this._FormAlert1.Mode = 0;
            this._FormAlert1.PatSet(this.Pat);
        }

        private void InspectDataButton_Click(object sender, EventArgs e)
        {
            if (this.Pat.Id.Length == 0)
            {
                MessageBox.Show("患者番号を入力してください");
                this.stdControlPat11.Focus();
                return;
            }

            FormInspectData f = new FormInspectData();
            f.PatSet(this.Pat);
            f.ShowDialog(this);

            this.ListShow();
        }

        private void ReadButton_Click(object sender, EventArgs e)
        {
            this.ReadPatCsv();
        }

        private void FormMedicalSupport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                this.ReadPatCsv();
            }
            else if (e.KeyCode == Keys.F8)
            {
                LoginChange lc = new LoginChange();
                lc.ShowDialog(this);
            }
        }

        /// <summary>
        /// Pat.csv を読み込んで患者と科をセットする
        /// </summary>
        /// <returns></returns>
        void ReadPatCsv()
        {
            // Pat.csv 読み込み
            PatBase p = PatBase.ReadPatCSV();

            // 患者をセットする
            if (p.Id.Length > 0)
            {
                // 変更がある場合のみ
                if (!this.Pat.Id.Equals(p.Id))
                {
                    this.PatSet(p);
                }
            }
            else
            {
                MessageBox.Show("患者情報ファイル Pat.csv がありません");
                return;
            }

            // 科をセットする。指定がなければ内科
            string dept = "1";

            if (dept.Length > 0)
            {
                dept = p.Dept;
            }
            else if (LoginUser.DeptId.Length > 0)
            {
                dept = LoginUser.DeptId;
            }

            // 科は変更する場合のみセットする
            if (!ctrlDeptBox11.GetDept().Code.ToString().Equals(dept))
            {
                ctrlDeptBox11.SetDept(dept);
            }

            return;
        }

        void InitShow(string[] args = null)
        {
            LoginUser.Init(true, args);

            // この時点でログインされていなければ終了
            if (LoginUser.Status == LoginUser.STATUS.NONE)
            {
                this.Dispose();
            }

            int pat_id = 0;

            // Pat.csv を見る
            PatBase p = PatBase.ReadPatCSV();
            int.TryParse(p.Id, out pat_id);

            for (int i = 1; i < args.Length; i++)
            {
                if (args[i].Equals("-p", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (i < args.Length - 1 && int.TryParse(args[i + 1], out pat_id))
                    {
                        // 次のパラメータが数字ならば患者IDとみなす
                        p = PatBase.Load(pat_id.ToString());
                        i++;
                    }
                }
            }

            // 患者をセットする。
            if (p.Id.Length > 0)
            {
                // 変更がある場合のみ
                if (!this.Pat.Id.Equals(p.Id))
                {
                    this.PatSet(p);
                }
            }

            // 科をセットする。指定がなければ内科
            string dept = "1";

            if (p.Dept.Length > 0)
            {
                dept = p.Dept;
            }
            else if (LoginUser.DeptId.Length > 0)
            {
                dept = LoginUser.DeptId;
            }

            // 科は変更する場合のみセットする
            if (!ctrlDeptBox11.GetDept().Code.ToString().Equals(dept))
            {
                ctrlDeptBox11.SetDept(dept);
            }

            this.WindowState = FormWindowState.Normal;
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            // アラート・DPC関連データを未取得の場合は再取得する
            if (this._Alert == null || !this._Alert.PtId.Equals(p.Id) ||
                this._PatIn == null || !this._PatIn.Id.Equals(p.Id) ||
                this._DPCHeader == null || !this._DPCHeader.Id.Equals(p.Id))
            {
                this._Alert = Alert.GetData(p.Id);
                this._PatIn = this.GetPatIn(p.Id, LoginUser.DoctorId);
                this._DPCHeader = this.GetDPCHeader(p.Id, LoginUser.DoctorId);

                foreach (Form f in this.OwnedForms)
                {
                    f.Dispose();
                }
            }

            bool b = false;

            // アラートがある場合
            if (this._Alert.Cont.Length > 0)
            {
                if (this._Alert.Status.Equals("1"))
                {
                    b = true;
                }

                this.AlertButton.ForeColor = Color.Red;
                this.AlertButton.BackColor = Color.Ivory;
            }
            else
            {
                this.AlertButton.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                this.AlertButton.BackColor = Color.FromKnownColor(KnownColor.Control);
            }

            if (b)
            {
                // アラートを表示する
                if (this._FormAlert1 == null || !this._FormAlert1.Created)
                {
                    this._FormAlert1 = new FormAlert();
                    this._FormAlert1.Show(this);
                }

                this._FormAlert1.Mode = 1;
                this._FormAlert1.PatSet(p);
            }
            else
            {
                // アラートを表示しない
                if (this._FormAlert1 != null && this._FormAlert1.Created)
                {
                    this._FormAlert1.Dispose();
                }
            }

            b = false;

            // DPC病名チェックされていない場合
            if (this._PatIn != null)
            {
                if (true)
//                if (LoginUser.Id.Equals("181") || LoginUser.Id.Equals("519") || LoginUser.Id.Equals("77777"))
                {
                    // 2019/07/25 本稼働までは本多副院長のみ
                    //  → 公開 2019/08/29
                    FormDiagDPC f_diag = new FormDiagDPC();
                    f_diag.PatSet(p);
                    f_diag.Show(this);

                    b = true;
                }

                this.DPCDiagButton.ForeColor = Color.Red;
                this.DPCDiagButton.BackColor = Color.Ivory;
            }
            else
            {
                this.DPCDiagButton.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                this.DPCDiagButton.BackColor = Color.FromKnownColor(KnownColor.Control);
            }

            // DPC入力が完了していない場合
            if (this._DPCHeader != null)
            {
                if (true)
//                if (LoginUser.Id.Equals("181") || LoginUser.Id.Equals("519") || LoginUser.Id.Equals("77777"))
                {
                    // 2019/07/25 本稼働までは本多副院長のみ
                    //  → 2019/08/29 公開
                    FormDPCData2 f_dpc = new FormDPCData2(this._DPCHeader);
                    f_dpc.Show(this);

                    b = true;
                }

                this.DPCButton.ForeColor = Color.Red;
                this.DPCButton.BackColor = Color.Ivory;
            }
            else
            {
                this.DPCButton.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                this.DPCButton.BackColor = Color.FromKnownColor(KnownColor.Control);
            }

            if (b)
            {
                // 2019/08/29 公開
//                MessageBox.Show("この画面は、本稼働までは、暫定的に本多副院長にのみ表示しています");
            }

            this.ListShow();
        }

        PatIn GetPatIn(string pt_id, string doctor_id = "")
        {
            PatIn obj = null;

            // 入院中または退院後７日以内の入退院履歴を取得する
            string dt7 = DateTime.Now.AddDays(-7).ToString("yyyyMMdd");

            List<PatIn> in_list = PatIn.GetHistory(pt_id, false).FindAll((x) =>
            {
                bool pin_flg = false;

                if (x.Status == PatInStatus.Now)
                {
                    pin_flg = true;
                }
                else if (x.Status == PatInStatus.Done)
                {
                    if (x.OutDate.CompareTo(dt7) >= 0)
                    {
                        pin_flg = true;
                    }
                }

                return pin_flg;
            });

            // DPC病名を取得する
            List<DiagDPC> diag_list = DiagDPC.GetList(in_list);

            bool diag_flg = false;

            foreach (PatIn pn in in_list)
            {
                // 主治医でなければ飛ばす
                if (!pn.Doctor.Equals(doctor_id)) continue;

                diag_flg = false;

                foreach (DiagDPC d in diag_list)
                {
                    // 削除されていれば飛ばす
                    if (d.DeleteFlg) continue;

                    // 患者IDが異なれば飛ばす（通常は無い）
                    if (!d.PtId.Equals(pn.Id)) continue;

                    // 入院SEQ が異なれば飛ばす
                    if (!d.InSEQ.Equals(pn.SEQ)) continue;

                    if (d.MainFlg)
                    {
                        diag_flg = true;
                        break;
                    }
                }

                // DPC病名チェックされていない場合
                if (!diag_flg)
                {
                    obj = pn;
                    break;
                }
            }

            return obj;
        }

        DPCHeader GetDPCHeader(string pt_id, string doctor_id = "")
        {
            DPCHeader obj = null;

            // DPC入力
            List<DPCHeader> header_list = DPCHeader.GetList(pt_id);

            foreach (DPCHeader h in header_list)
            {
                // 無効の場合は飛ばす
                if (!h.Status.Equals("1")) continue;

                // 医師の担当部分が完成している場合は飛ばす
                if (h.Status2.Equals("1")) continue;

                // 自身の担当でなければ飛ばす
                if (!h.Doctor1.Equals(doctor_id) &&
                    !h.Doctor2.Equals(doctor_id))
                {
                    continue;
                }

                obj = h;
                break;
            }

            return obj;
        }

        private void ctrlDeptBox11_TextChanged(object sender, EventArgs e)
        {
            this.KindPanelShow();
        }

        void KindPanelShow()
        {
            // クリアする
            this._Inspect = new InspectSet();
            this.KindPanel.Controls.Clear();

            // 選択されている診療科を取得する
            Dept dept = this.ctrlDeptBox11.GetDept();

            if (dept.Code <= 0)
            {
                return;
            }

            if (dept.Code.Equals(6))
            {
                // 耳鼻科の場合は、旧手術記録を表示する
                this.OpeRecordButton.Visible = true;
            }
            else
            {
                this.OpeRecordButton.Visible = false;
            }

            // 診療科に応じた Inspect リストを取得する
            InspectSets sets = InspectSets.GetDataByDept(dept.Code.ToString()).SetList.Count > 0 ? InspectSets.GetDataByDept(dept.Code.ToString()) : InspectSets.GetDataByDept("");

            foreach (InspectSet obj in sets.SetList)
            {
                RadioButton b = new RadioButton();
                b.Name = "Inspect_" + obj.Id;
                b.Text = obj.Name;
                b.AutoEllipsis = true;
                b.Size = new Size(130, 25);
                b.Appearance = Appearance.Button;
                b.FlatStyle = FlatStyle.Flat;
                b.Tag = obj;
                b.Click += new EventHandler(b_Click);

                this.KindPanel.Controls.Add(b);
            }

            // 最初のボタンを選択する
            if (sets.SetList.Count > 0)
            {
                RadioButton b = (RadioButton)this.KindPanel.Controls[0];
                this._Inspect = (InspectSet)b.Tag;
                b.Checked = true;

                this.ListShow();
            }
        }

        void b_Click(object sender, EventArgs e)
        {
            RadioButton b = (RadioButton)sender;
            this._Inspect = (InspectSet)b.Tag;

            this.ListShow();
        }

        void ListShow()
        {
            if (!DSet.Tables.Contains("Inspect"))
            {
                return;
            }

            this.InspectLabel.Text = "";
            this.InspectLabel.BackColor = Color.Transparent;

            this.InspectResultLabel.Text = "";
            this.InspectResultLabel.BackColor = Color.Transparent;

            DataTable table = DSet.Tables["Inspect"];
            table.Rows.Clear();

            // 患者がセットされていなければ終了
            if (this.Pat.Id.Length == 0)
            {
                return;
            }

            // Inspect が選ばれていなければ終了
            if (this._Inspect.Id.Length == 0)
            {
                return;
            }

            InspectSet obj = InspectSet.GetData(this._Inspect.Id);

            this.InspectLabel.Text = obj.Name;

            // デフォルト 12か月前～6か月後
            int m1 = -12;
            int.TryParse("-" + obj.MonthAgo, out m1);

            // 2019/01/01 以降のオーダーしかないため
            /*
            if (DateTime.Now.AddMonths(m1) < DateTime.Parse("2019/01/01"))
            {
                m1 = DateTime.Parse("2019/01/01").Subtract(DateTime.Now).Days / 30;
            }
             */

            int m2 = 6;

            this.InspectLabel.Text += "　" + (0 - m1) + "か月前 ～ " + m2 + "か月後";

            // 本日
            string today = DateTime.Now.ToString("yyyyMMdd");

            // 検索期間の開始日・終了日
            string date1 = DateTime.Now.AddMonths(m1).ToString("yyyyMMdd");
            string date2 = DateTime.Now.AddMonths(m2).ToString("yyyyMMdd");
            List<string> shinku_list = new List<string> { "13", "60", "70" };

            // オーダー履歴を取得する
            List<PatOrder> order_list = PatOrder.GetListByPatDates(this.Pat.Id, date1, date2, "", shinku_list, null, true).FindAll((x) =>
                {
                    if (x.SekouDate.CompareTo(date1) >= 0 && x.SekouDate.CompareTo(date2) <= 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });

            // 2018/12/31 以前のオーダーが必要な場合は MACS オーダー履歴も取得する
            if (date1.CompareTo("20181231") <= 0)
            {
                try
                {
                    List<PatOrder> order_list2 = PatOrder.GetListByPatDatesMacs(this.Pat.Id, date1, date2, "", shinku_list, null, true).FindAll((x) =>
                    {
                        if (x.SekouDate.CompareTo(date1) >= 0 && x.SekouDate.CompareTo(date2) <= 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    });

                    order_list.AddRange(order_list2);
                }
                catch (Exception ex)
                {
                    LibUtility.Except(ex, false);
                }
            }

            // 施行日の降順に並べ替える
            order_list.Sort((x, y) =>
            {
                return y.SekouDate.CompareTo(x.SekouDate);
            });


            // 院外検査
            List<InspectData> out_list = InspectData.GetOutList(this.Pat.Id, date1, date2);


            // 検査ごとの表示内容の有無
            bool b = true;
            bool bb = false;

            // 過去（本日まで）オーダー
//            List<string> result_list1 = new List<string>();
            List<InspectData> in_list1 = new List<InspectData>();

            // 未来（明日以降）オーダー
//            List<string> result_list2 = new List<string>();
            List<InspectData> in_list2 = new List<InspectData>();

            string ss = "";

            // がんフラグ = true かつ 未検査のもの
            string ks = "";

            foreach (InspectKind k in obj.KindList)
            {
                DataRow r = table.NewRow();

                // Id でソートするため数値型に変換
//                int kid = 99999;
//                int.TryParse(k.Id, out kid);

//                r["Id"] = kid;
                r["Id"] = k.Id;
                r["Name"] = k.Name;
                r["CancerFlg"] = k.RedFlg;

//                result_list1.Clear();
//                result_list2.Clear();

                in_list1.Clear();
                in_list2.Clear();

                foreach (PatOrder po in order_list)
                {
                    // オーダーが条件に該当するかチェックする
                    b = false;
                    ss = "";

                    foreach (InspectOrder io in k.OrderList)
                    {
                        // オーダーコードが無ければ飛ばす
                        if (io.OrderCodeList.Count == 0)
                        {
                            continue;
                        }

                        b = true;

                        foreach (string code in io.OrderCodeList)
                        {
                            bb = false;

                            // 該当のオーダーコードが出ているかチェックする
                            foreach (PatOrderDetail detail in po.DetailList)
                            {
                                // オーダーコードが異なれば飛ばす
                                if (!detail.Code.Equals(code))
                                {
                                    continue;
                                }

                                // 該当のオーダーコードが出ていればOK
                                if (ss.Length > 0)
                                {
                                    ss += " ";
                                }

                                ss += detail.Name;
                                bb = true;
                                break;
                            }

                            // 該当のオーダーコードが１つでも出ていない場合、このオーダーは条件に該当しない
                            if (!bb)
                            {
                                ss = "";
                                b = false;
                                break;
                            }
                        }

                        // オーダーが条件に該当すれば終了する
                        if (b)
                        {
                            // 表示名称が存在すれば置き換える
                            if (io.Name.Length > 0) ss = io.Name;

                            break;
                        }
                    }

                    // オーダーが条件に該当する場合
                    if (b)
                    {
                        InspectData data = new InspectData();
                        data.Kind = k.Id;
                        data.InspectDate = po.SekouDate;
                        data.Doctor = po.Doctor;
                        data.Staff = po.Staff;
                        data.SekouFlg = po.SekouFlg.Equals("1");
                        data.Cont = ss;

                        ss = po.SekouDateStringShort + " " + ss;

                        // 医師名がある場合
                        if (po.DoctorName.Length > 0)
                        {
                            ss += " [" + po.DoctorName.Replace("　", " ").Replace(" ", "") + "]";
                        }

                        if (po.SekouDate.CompareTo(today) <= 0)
                        {
                            // 過去（本日まで）の場合

                            // 未施行の場合
                            if (!po.SekouFlg.Equals("1"))
                            {
                                ss += " ▲";
                            }
/*
                            if (!result_list1.Contains(ss))
                            {
                                result_list1.Add(ss);
                            }
*/
                            if (in_list1.FindAll((x) => { return x.Cont.Equals(data.Cont); }).Count == 0)
                            {
                                in_list1.Add(data);
                            }
                        }
                        else
                        {
                            // 未来（明日以降）の場合
/*
                            if (!result_list2.Contains(ss))
                            {
                                result_list2.Add(ss);
                            }
*/
                            if (in_list2.FindAll((x) => { return x.Cont.Equals(data.Cont); }).Count == 0)
                            {
                                in_list2.Add(data);
                            }
                        }
                    }
                }

                // 院外検査も確認する
                foreach (InspectData io in out_list)
                {
                    if (io.Kind.Equals(k.Id))
                    {
                        ss = DateTimeAgent.DateFormat(io.InspectDate, DateTimeAgent.DateFormatKind.SHORT) + " " + io.Cont;
                        
                        if (io.StaffName.Length > 0)
                        {
                            ss += " [" + io.StaffName.Replace("　", " ").Replace(" ", "") + "]";
                        }
                        
                        ss += " ☆";

                        if (io.InspectDate.CompareTo(today) <= 0)
                        {
                            // 過去（本日まで）の場合
//                            result_list1.Add(ss);

                            if (in_list1.FindAll((x) => { return x.Cont.Equals(io.Cont); }).Count == 0)
                            {
                                in_list1.Add(io);
                            }
                        }
                        else
                        {
                            // 未来（明日以降）の場合
//                            result_list2.Add(ss);

                            if (in_list2.FindAll((x) => { return x.Cont.Equals(io.Cont); }).Count == 0)
                            {
                                in_list2.Add(io);
                            }
                        }
                    }
                }

                // 日付の降順に並べ替える
/*
                result_list1.Sort((x, y) => { return y.CompareTo(x); });
                result_list2.Sort((x, y) => { return y.CompareTo(x); });

                r["Cont1"] = AppString.ConcatList(result_list1, Environment.NewLine);
                r["Cont2"] = AppString.ConcatList(result_list2, Environment.NewLine);
*/
                in_list1.Sort((x, y) => { return y.InspectDate.CompareTo(x.InspectDate); });
                in_list2.Sort((x, y) => { return y.InspectDate.CompareTo(x.InspectDate); });

                r["Cont1"] = AppString.ConcatList(in_list1.ConvertAll((x) => { return x.ContValue; }), Environment.NewLine);
                r["Cont2"] = AppString.ConcatList(in_list2.ConvertAll((x) => { return x.ContValue; }), Environment.NewLine);

                // がんフラグ = true かつ 未検査の場合
                if (k.RedFlg.Equals("1"))
                {
                    if (in_list1.Count == 0)
                    {
                        if (in_list2.Count == 0)
                        {
                            // 過去にも未来にも検査が無い
                            r["Result"] = "2";
                        }
                        else
                        {
                            // 過去には無いが、未来には有る
                            r["Result"] = "1";
                        }

                        if (ks.Length > 0)
                        {
                            ks += ", ";
                        }

                        ks += k.Name;
                    }
                    else
                    {
                        r["Result"] = "0";
                    }
                }
                else
                {
                    r["Result"] = "0";
                }

                table.Rows.Add(r);
            }

            // がんフラグ = true かつ 未検査のもの
            /*
            if (ks.Length > 0)
            {
                this.InspectResultLabel.Text = ks;
                this.InspectResultLabel.BackColor = Color.LightPink;
            }
             */

            this.ListFormat();
        }

        void ListFormat()
        {
            if (!DSet.Tables.Contains("Inspect"))
            {
                return;
            }

            DataTable table = DSet.Tables["Inspect"];

            DataView view = new DataView(table);

            ListView.DataSource = view;
//            view.Sort = "Result desc, Id";
            view.Sort = "Result desc";

            ListView.Columns["Id"].Visible = false;

            ListView.Columns["Name"].Width = 80;
            ListView.Columns["Name"].HeaderText = "種別";

            ListView.Columns["CancerFlg"].Width = 30;
            ListView.Columns["CancerFlg"].Visible = false;

            ListView.Columns["Cont1"].Width = 250;
            ListView.Columns["Cont1"].HeaderText = "過去（本日まで）";
            ListView.Columns["Cont1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;

            ListView.Columns["Cont2"].Width = 220;
            ListView.Columns["Cont2"].HeaderText = "未来（明日以降）";
            ListView.Columns["Cont2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;

            ListView.Columns["Result"].Visible = false;

            foreach (DataGridViewRow r in this.ListView.Rows)
            {
                switch (r.Cells["Result"].Value.ToString())
                {
                    case "2":
                        r.Cells["Name"].Style.BackColor = Color.FromArgb(255, 192, 192);
                        r.Cells["Name"].Style.Font = AppFont.FB11.Ft;
                        break;
                    case "1":
                        r.Cells["Name"].Style.BackColor = Color.FromArgb(255, 255, 160);
                        r.Cells["Name"].Style.Font = AppFont.F11.Ft;
                        break;
                    default:
                        r.Cells["Name"].Style.Font = AppFont.F11.Ft;
                        break;
                }
            }

            // 非選択状態にしておく
            ListView.ClearSelection();
        }

        // 以下は位置調整のため

        private void FormMedicalSupport_Move(object sender, EventArgs e)
        {
            this.LocationLabel.Text = this.Location.X + "," + this.Location.Y;
        }

        private void FormMedicalSupport_Resize(object sender, EventArgs e)
        {
            this.SizeLabel.Text = this.Width + "," + this.Height;
        }

        private void LocationLabel_DoubleClick(object sender, EventArgs e)
        {
            if (this.LocationLabel.ForeColor == Color.Black)
            {
                this.LocationLabel.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                this.LocationLabel.ForeColor = Color.Black;
            }
        }

        private void SizeLabel_DoubleClick(object sender, EventArgs e)
        {
            if (this.SizeLabel.ForeColor == Color.Black)
            {
                this.SizeLabel.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                this.SizeLabel.ForeColor = Color.Black;
            }
        }
    }
}
