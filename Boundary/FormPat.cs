using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Agent;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class FormPat : StdForm1
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
            }
        }

        List<Button> ButtonList = new List<Button>();

        /// <summary>
        /// 患者基本情報
        /// </summary>
        FormBaseInfo formBaseInfo;

        /// <summary>
        /// 病名
        /// </summary>
        FormDiag formDiag;

		/// <summary>
		/// 病名ビューア
		/// </summary>
		FormDiagView formDiagView;

        /// <summary>
        /// プロブレム
        /// </summary>
        FormProblem1 formProblem1;

        /// <summary>
        /// SOAP
        /// </summary>
        FormSoap formSoap;

        /// <summary>
        /// バイタル
        /// </summary>
        FormVital formVital;

        /// <summary>
        /// 検査結果
        /// </summary>
        FormKensa formKensa;

        /// <summary>
        /// オーダー
        /// </summary>
        FormOrder formOrder;

        /// <summary>
        /// 会計取込み
        /// </summary>
        FormOrderKaikei formOrderKaikei;

        /// <summary>
        /// 実施記録
        /// </summary>
        FormExec formExec;

        /// <summary>
        /// 患者掲示板
        /// </summary>
        FormPatBoard formPatBoard;

        /// <summary>
        /// 基本的指示事項
        /// </summary>
        FormBaseOrder formBaseOrder;

        /// <summary>
        /// 手術指示
        /// </summary>
        FormOpeOrder formOpeOrder;

        /// <summary>
        /// 看護指示
        /// </summary>
        FormNursingOrder formNursingOrder;

        /// <summary>
        /// 予約
        /// </summary>
        FormPatRsv formPatRsv;

        /// <summary>
        /// DPC
        /// </summary>
        FormDPCData1 formDPCData1;

        /// <summary>
        /// 伝達事項
        /// </summary>
        FormMemo formMemo1;

        /// <summary>
        /// 付箋
        /// </summary>
        FormPostIt1 formPostIt1;

        /// <summary>
        /// 連絡先
        /// </summary>
        FormPatContact formPatContact;


        public FormPat(bool read_only = false)
        {
            InitializeComponent();

            this.ReadOnly = read_only;
        }

        private void FormPat_Load(object sender, EventArgs e)
        {
            // Inno カルテはログイン必須
            if (LoginUser.Status == LoginUser.STATUS.NONE)
            {
                LoginUser.Init();
            }

            // この時点でログインされていなければ終了する
            if (LoginUser.Status == LoginUser.STATUS.NONE)
            {
                this.Dispose();
            }

            ButtonList.Add(this.FormBaseInfoButton1);
            ButtonList.Add(this.FormDiagButton1);
            ButtonList.Add(this.FormProblemButton1);
            ButtonList.Add(this.FormSoapButton1);
            ButtonList.Add(this.FormVitalButton1);
            ButtonList.Add(this.FormKensaButton1);
            ButtonList.Add(this.FormOrderButton1);
            ButtonList.Add(this.FormOrderKaikeiButton1);
            ButtonList.Add(this.FormExecButton1);
            ButtonList.Add(this.FormPatBoardButton1);
            ButtonList.Add(this.FormPatRsvButton1);
            ButtonList.Add(this.FormDPCButton1);
            ButtonList.Add(this.FormDPCListButton2);
            ButtonList.Add(this.FormMemoButton1);
            ButtonList.Add(this.FormMedicalSupportButton1);
            ButtonList.Add(this.FormPostItButton1);
            ButtonList.Add(this.FormPatContactButton1);
            ButtonList.Add(this.EyeCenterButton1);
            ButtonList.Add(this.FormKarteMessageButton1);

            this.FormBaseInfoButton1.Enabled = true;
            this.FormDiagButton1.Enabled = true;
            this.FormProblemButton1.Enabled = false;
            this.FormSoapButton1.Enabled = true;
            this.FormVitalButton1.Enabled = false;
            this.FormKensaButton1.Enabled = true;
            this.FormOrderButton1.Enabled = true;
            this.FormExecButton1.Enabled = false;
            this.FormPatBoardButton1.Enabled = false;
            this.FormPatRsvButton1.Enabled = true;
            this.FormDPCButton1.Enabled = true;
            this.FormDPCListButton2.Enabled = true;
            this.FormMemoButton1.Enabled = true;
            this.FormMedicalSupportButton1.Enabled = false;
            this.FormPostItButton1.Enabled = true;
            this.FormPatContactButton1.Enabled = false;
            this.EyeCenterButton1.Enabled = true;
            this.FormKarteMessageButton1.Enabled = true;

            if (this.ReadOnly)
            {
                this.FormOrderKaikeiButton1.Enabled = false;
            }
            else
            {
                this.FormOrderKaikeiButton1.Enabled = true;
            }


            this.ButtonLayout();

            if (Screen.PrimaryScreen.Bounds.Width < this.Width)
            {
                this.Width = Screen.PrimaryScreen.Bounds.Width - 20;
            }

            this.Height = Screen.PrimaryScreen.Bounds.Height - 50;
            this.Location = new Point(this.Location.X, 20);

            foreach (Control c in this.Controls)
            {
                if (c is MdiClient)
                {
                    c.BackColor = Color.LightYellow;
                    c.Invalidate();
                }
            }

            this.stdControlPat11.Focus();
        }

        private void FormPat_Shown(object sender, EventArgs e)
        {
            if (this.Pat.Name.Length > 0)
            {
                this.FormSoap_Show();
            }
        }

        void ButtonLayout()
        {
            int h = 45;

            foreach (Button b in this.ButtonList)
            {
                if (b.Enabled)
                {
                    b.Location = new Point(12, h);
                    h += 30;
                }
                else
                {
                    b.Visible = false;
                }
            }
        }

        public override void FontSet(AppFont f)
        {
            base.FontSet(f);

            this.stdControlFont11.FontSet(f);

            // 子ウィンドウのフォントを変更する

            if (formSoap != null && formSoap.Created)
            {
                formSoap.FontSet(f);
            }

            if (formDiag != null && formDiag.Created)
            {
                formDiag.FontSet(f);
            }

            if (formDiagView != null && formDiagView.Created)
            {
                formDiagView.FontSet(f);
            }

            if (formProblem1 != null && formProblem1.Created)
            {
                formProblem1.FontSet(f);
            }

            if (formVital != null && formVital.Created)
            {
                formVital.FontSet(f);
            }

            if (formBaseInfo != null && formBaseInfo.Created)
            {
                formBaseInfo.FontSet(f);
            }

            if (formOrder != null && formOrder.Created)
            {
                formOrder.FontSet(f);
            }

            if (formOrderKaikei != null && formOrderKaikei.Created)
            {
                formOrderKaikei.FontSet(f);
            }

            if (formExec != null && formExec.Created)
            {
                formExec.FontSet(f);
            }

            if (formKensa != null && formKensa.Created)
            {
                formKensa.FontSet(f);
            }

            if (formPatBoard != null && formPatBoard.Created)
            {
                formPatBoard.FontSet(f);
            }

            if (formBaseOrder != null && formBaseOrder.Created)
            {
                formBaseOrder.FontSet(f);
            }

            if (formNursingOrder != null && formNursingOrder.Created)
            {
                formNursingOrder.FontSet(f);
            }

            if (formOpeOrder != null && formOpeOrder.Created)
            {
                formOpeOrder.FontSet(f);
            }

            if (formPatRsv != null && formPatRsv.Created)
            {
                formPatRsv.FontSet(f);
            }

            if (formDPCData1 != null && formDPCData1.Created)
            {
                formDPCData1.FontSet(f);
            }

            if (formMemo1 != null && formMemo1.Created)
            {
                formMemo1.FontSet(f);
            }

            if (formPostIt1 != null && formPostIt1.Created)
            {
                formPostIt1.FontSet(f);
            }

            if (formPatContact != null && formPatContact.Created)
            {
                formPatContact.FontSet(f);
            }
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            AppStat.CurrentPat = p;

            this.stdControlPat11.PatSet(p);

            // 子ウィンドウの患者情報を変更する

            if (formSoap != null && formSoap.Created)
            {
                formSoap.PatSet(p);
            }

            if (formDiag != null && formDiag.Created)
            {
                formDiag.PatSet(p);
            }

            if (formDiagView != null && formDiagView.Created)
            {
                formDiagView.PatSet(p);
            }

            if (formProblem1 != null && formProblem1.Created)
            {
                formProblem1.PatSet(p);
            }

            if (formVital != null && formVital.Created)
            {
                formVital.PatSet(p);
            }

            if (formBaseInfo != null && formBaseInfo.Created)
            {
                formBaseInfo.PatSet(p);
            }

            if (formOrder != null && formOrder.Created)
            {
                formOrder.PatSet(p);
            }

            if (formOrderKaikei != null && formOrderKaikei.Created)
            {
                formOrderKaikei.PatSet(p);
            }

            if (formExec != null && formExec.Created)
            {
                formExec.PatSet(p);
            }

            if (formKensa != null && formKensa.Created)
            {
                formKensa.PatSet(p);
            }

            if (formPatBoard != null && formPatBoard.Created)
            {
                formPatBoard.PatSet(p);
            }

            if (formBaseOrder != null && formBaseOrder.Created)
            {
                formBaseOrder.PatSet(p);
            }

            if (formNursingOrder != null && formNursingOrder.Created)
            {
                formNursingOrder.PatSet(p);
            }

            if (formOpeOrder != null && formOpeOrder.Created)
            {
                formOpeOrder.PatSet(p);
            }

            if (formPatRsv != null && formPatRsv.Created)
            {
                formPatRsv.PatSet(p);
            }

            if (formDPCData1 != null && formDPCData1.Created)
            {
                formDPCData1.PatSet(p);
            }

            if (formMemo1 != null && formMemo1.Created)
            {
                formMemo1.PatSet(p);
            }

            if (formPostIt1 != null && formPostIt1.Created)
            {
                formPostIt1.PatSet(p);
            }

            if (formPatContact != null && formPatContact.Created)
            {
                formPatContact.PatSet(p);
            }
        }

        void FormShow(StdForm1 f)
        {
            f.Show();
            f.Activate();
            f.BringToFront();

            if (f.WindowState == FormWindowState.Minimized)
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        /// <summary>
        /// SOAPを表示する
        /// </summary>
        void FormSoap_Show()
        {
            if (formSoap == null || !formSoap.Created)
            {
                formSoap = new FormSoap(true);
                formSoap.PatSet(this.Pat);
                formSoap.FontSet(this.Fnt);
                formSoap.MdiParent = this;
                formSoap.Location = new Point(0, 0);
                formSoap.Height = this.Height - this.Padding.Top - 50;
            }

            FormShow(formSoap);
        }

        /// <summary>
        /// 基本的指示事項を表示する
        /// </summary>
        public void FormBaseOrder_Show()
        {
            if (formBaseOrder == null || !formBaseOrder.Created)
            {
                formBaseOrder = new FormBaseOrder();
                formBaseOrder.PatSet(this.Pat);
                formBaseOrder.FontSet(this.Fnt);
                formBaseOrder.MdiParent = this;
            }

            FormShow(formBaseOrder);
        }

        /// <summary>
        /// 看護指示を表示する
        /// </summary>
        public void FormNursingOrder_Show()
        {
            if (formNursingOrder == null || !formNursingOrder.Created)
            {
                formNursingOrder = new FormNursingOrder();
                formNursingOrder.PatSet(this.Pat);
                formNursingOrder.FontSet(this.Fnt);
                formNursingOrder.MdiParent = this;
            }

            FormShow(formNursingOrder);
        }

        /// <summary>
        /// 手術指示を表示する
        /// </summary>
        public void FormOpeOrder_Show()
        {
            if (formOpeOrder == null || !formBaseOrder.Created)
            {
                formOpeOrder = new FormOpeOrder();
                formOpeOrder.PatSet(this.Pat);
                formOpeOrder.FontSet(this.Fnt);
                formOpeOrder.MdiParent = this;
            }

            FormShow(formOpeOrder);
        }

        private void FormSoapButton1_Click(object sender, EventArgs e)
        {
            this.FormSoap_Show();
        }

        private void FormDiagButton1_Click(object sender, EventArgs e)
        {
            // 情報室または診療情報管理室
            if (LoginUser.QualId.Equals("99") ||
                LoginUser.QualId.Equals("21"))
            {
                if (formDiag == null || !formDiag.Created)
                {
                    formDiag = new FormDiag();
                    formDiag.PatSet(this.Pat);
                    formDiag.FontSet(this.Fnt);
                    formDiag.MdiParent = this;
                }

                FormShow(formDiag);
            }
            else
            {
                if (formDiagView == null || !formDiagView.Created)
                {
                    formDiagView = new FormDiagView();
                    formDiagView.PatSet(this.Pat);
                    formDiagView.FontSet(this.Fnt);
                    formDiagView.MdiParent = this;
                }

                FormShow(formDiagView);
            }
        }

        private void FormVitalButton1_Click(object sender, EventArgs e)
        {
            if (formVital == null || !formVital.Created)
            {
                formVital = new FormVital();
                formVital.PatSet(this.Pat);
                formVital.FontSet(this.Fnt);
                formVital.MdiParent = this;
            }

            FormShow(formVital);
        }

        private void FormBaseInfoButton1_Click(object sender, EventArgs e)
        {
            if (formBaseInfo == null || !formBaseInfo.Created)
            {
                formBaseInfo = new FormBaseInfo(true);
                formBaseInfo.PatSet(this.Pat);
                formBaseInfo.FontSet(this.Fnt);
                formBaseInfo.MdiParent = this;
            }

            FormShow(formBaseInfo);
        }

        private void FormOrderButton1_Click(object sender, EventArgs e)
        {
            if (formOrder == null || !formOrder.Created)
            {
                formOrder = new MedicalLibrary.Boundary.FormOrder(true);
                formOrder.PatSet(this.Pat);
                formOrder.FontSet(this.Fnt);
                formOrder.MdiParent = this;
            }

            FormShow(formOrder);
        }

        private void FormOrderKaikeiButton1_Click(object sender, EventArgs e)
        {
            if (formOrderKaikei == null || !formOrderKaikei.Created)
            {
                formOrderKaikei = new FormOrderKaikei();
                formOrderKaikei.PatSet(this.Pat);
                formOrderKaikei.FontSet(this.Fnt);
                formOrderKaikei.MdiParent = this;
            }

            FormShow(formOrderKaikei);
        }

        private void FormExecButton1_Click(object sender, EventArgs e)
        {
            if (formExec == null || !formExec.Created)
            {
                formExec = new FormExec();
                formExec.PatSet(this.Pat);
                formExec.FontSet(this.Fnt);
                formExec.MdiParent = this;
            }

            FormShow(formExec);
        }

        private void FormKensaButton1_Click(object sender, EventArgs e)
        {
            if (formKensa == null || !formKensa.Created)
            {
                formKensa = new FormKensa();
                formKensa.PatSet(this.Pat);
                formKensa.FontSet(this.Fnt);
                formKensa.MdiParent = this;
            }

            FormShow(formKensa);
        }

        private void FormPatBoardButton1_Click(object sender, EventArgs e)
        {
            if (formPatBoard == null || !formPatBoard.Created)
            {
                formPatBoard = new FormPatBoard();
                formPatBoard.PatSet(this.Pat);
                formPatBoard.FontSet(this.Fnt);
                formPatBoard.MdiParent = this;
            }

            FormShow(formPatBoard);
        }

        private void FormPatRsvButton1_Click(object sender, EventArgs e)
        {
            if (formPatRsv == null || !formPatRsv.Created)
            {
                formPatRsv = new FormPatRsv();
                formPatRsv.PatSet(this.Pat);
                formPatRsv.FontSet(this.Fnt);
                formPatRsv.MdiParent = this;
            }

            FormShow(formPatRsv);
        }

        private void FormDPCButton1_Click(object sender, EventArgs e)
        {
            if (formDPCData1 == null || !formDPCData1.Created)
            {
                formDPCData1 = new FormDPCData1();
                formDPCData1.PatSet(this.Pat);
                formDPCData1.FontSet(this.Fnt);
                formDPCData1.MdiParent = this;
            }

            FormShow(formDPCData1);
        }

        private void FormDPCListButton2_Click(object sender, EventArgs e)
        {
            FormDPCList2 f = new FormDPCList2();
            f.PatSet(this.Pat);
            f.Show();
        }

        private void FormMemoButton1_Click(object sender, EventArgs e)
        {
            if (formMemo1 == null || !formMemo1.Created)
            {
                formMemo1 = new FormMemo(this.ReadOnly);
                formMemo1.PatSet(this.Pat);
                formMemo1.FontSet(this.Fnt);
                formMemo1.MdiParent = this;
            }

            FormShow(formMemo1);
        }

        private void FormMedicalSupportButton1_Click(object sender, EventArgs e)
        {
            Launcher.Start("MedicalSupport.exe", "-force");
        }

        private void FormPostItButton1_Click(object sender, EventArgs e)
        {
            if (formPostIt1 == null || !formPostIt1.Created)
            {
                formPostIt1 = new FormPostIt1(this.ReadOnly);
                formPostIt1.PatSet(this.Pat);
                formPostIt1.FontSet(this.Fnt);
                formPostIt1.MdiParent = this;

                // 当日の受付科があればセットする
                List<PatOut> list = PatOut.GetOneday(this.Pat.Id, DateTime.Now.ToString("yyyyMMdd"), "UKE_NO desc");

                foreach (PatOut obj in list)
                {
                    formPostIt1.DeptSet(obj.Dept);
                    break;
                }
            }

            FormShow(formPostIt1);
        }

        private void FormProblemButton1_Click(object sender, EventArgs e)
        {
            if (formProblem1 == null || !formProblem1.Created)
            {
                formProblem1 = new FormProblem1(this.ReadOnly);
                formProblem1.PatSet(this.Pat);
                formProblem1.FontSet(this.Fnt);
                formProblem1.MdiParent = this;
            }

            FormShow(formProblem1);
        }

        private void FormPatContactButton1_Click(object sender, EventArgs e)
        {
            if (formPatContact == null || !formPatContact.Created)
            {
                formPatContact = new FormPatContact();
                formPatContact.PatSet(this.Pat);
                formPatContact.FontSet(this.Fnt);
                formPatContact.MdiParent = this;
            }

            FormShow(formPatContact);
        }

        private void EyeCenterButton1_Click(object sender, EventArgs e)
        {
            Launcher.EyeCenter(this.Pat.Id);
        }

        private void FormKarteMessageButton1_Click(object sender, EventArgs e)
        {
            FormKarteMessage2 f = new FormKarteMessage2(this.Pat.Id);
            f.Show(this);
        }
    }
}
