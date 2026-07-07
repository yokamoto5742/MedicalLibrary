using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;
using MedicalLibrary.Agent;

namespace MedicalLibrary.Boundary
{
    public class FormControl
    {
        /// <summary>
        /// 外来患者一覧
        /// </summary>
        static FormPatList formPatList;

        /// <summary>
        /// 外来科別人数
        /// </summary>
        static FormDeptStat formDeptStat;

        /// <summary>
        /// 病棟患者一覧
        /// </summary>
        static FormByotoList formByotoList;

        /// <summary>
        /// 病棟レイアウト
        /// </summary>
        static FormByotoLayout formByotoLayout;

        /// <summary>
        /// 実施記録
        /// </summary>
        static FormExec formExec;

        /// <summary>
        /// 予約
        /// </summary>
        static FormRsvs formRsvs;

        /// <summary>
        /// 患者カルテ
        /// </summary>
        static FormPat formPat;

        /// <summary>
        /// パス
        /// </summary>
        static FormPath formPath;

        /// <summary>
        /// DPC病名
        /// </summary>
        static FormDiagDPC formDPCDiag;

        /// <summary>
        /// DPC入力フォーム
        /// </summary>
        static FormDPCData1 formDPCData1;

        /// <summary>
        /// DPCチェック
        /// </summary>
        static FormDPCCheck1 formDPCCheck1;

        /// <summary>
        /// DPCリスト
        /// </summary>
        static FormDPCList formDPCList;

        /// <summary>
        /// DPCリスト
        /// </summary>
        static FormDPCList2 formDPCHeaderList;

        /// <summary>
        /// オーダー会計取込み
        /// </summary>
        static FormOrderKaikei formOrderKaikei;
/*
        /// <summary>
        /// カルテテンプレート
        /// </summary>
        static FormKarteTemplate1 formKarteTemplate1;
*/
        /// <summary>
        /// カルテメッセージ
        /// </summary>
        static FormKarteMessage1 formKarteMessage1;

        /// <summary>
        /// 麻酔記録
        /// </summary>
        static FormAnesRecord formAnesRecord;

        /// <summary>
        /// 病名検索
        /// </summary>
        static FormDiagSearch formDiagSearch;

        /// <summary>
        /// 請求書・明細書PDF
        /// </summary>
        static FormBillPDFList formBillPDFList;


        static void FormShow(Form f)
        {
            f.Show();
            f.Activate();
            f.BringToFront();

            if (f.WindowState == FormWindowState.Minimized)
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        public static void AllDispose()
        {
            if (formPatList != null)
            {
                formPatList.Dispose();
            }

            if (formDeptStat != null)
            {
                formDeptStat.Dispose();
            }

            if (formByotoList != null)
            {
                formByotoList.Dispose();
            }

            if (formByotoLayout != null)
            {
                formByotoLayout.Dispose();
            }

            if (formExec != null)
            {
                formExec.Dispose();
            }

            if (formRsvs != null)
            {
                formRsvs.Dispose();
            }

            if (formPat != null)
            {
                formPat.Dispose();
            }

            if (formPath != null)
            {
                formPath.Dispose();
            }

            if (formDPCDiag != null)
            {
                formDPCDiag.Dispose();
            }

            if (formDPCData1 != null)
            {
                formDPCData1.Dispose();
            }

            if (formDPCCheck1 != null)
            {
                formDPCCheck1.Dispose();
            }

            if (formDPCList != null)
            {
                formDPCList.Dispose();
            }

            if (formDPCHeaderList != null)
            {
                formDPCHeaderList.Dispose();
            }

            if (formOrderKaikei != null)
            {
                formOrderKaikei.Dispose();
            }
/*
            if (formKarteTemplate1 != null)
            {
                formKarteTemplate1.Dispose();
            }
*/
            if (formKarteMessage1 != null)
            {
                formKarteMessage1.Dispose();
            }

            if (formAnesRecord != null)
            {
                formAnesRecord.Dispose();
            }

            if (formDiagSearch != null)
            {
                formDiagSearch.Dispose();
            }

            if (formBillPDFList != null)
            {
                formBillPDFList.Dispose();
            }
        }


        public static void FormPatList_Show()
        {
            if (formPatList == null || !formPatList.Created)
            {
                formPatList = new FormPatList();
            }

            FormShow(formPatList);
        }

        public static void FormDeptStat_Show()
        {
            if (formDeptStat == null || !formDeptStat.Created)
            {
                formDeptStat = new FormDeptStat();
            }

            FormShow(formDeptStat);
        }

        public static void FormByotoList_Show()
        {
            if (formByotoList == null || !formByotoList.Created)
            {
                formByotoList = new FormByotoList();
            }

            FormShow(formByotoList);
        }

        public static void FormByotoList_ListShow()
        {
            if (formByotoList != null && formByotoList.Created)
            {
                formByotoList.ListShow();
            }
        }

        public static void FormByotoLayout_Show()
        {
            if (formByotoLayout == null || !formByotoLayout.Created)
            {
                formByotoLayout = new FormByotoLayout();
            }

            FormShow(formByotoLayout);
        }

        public static void FormExec_Show()
        {
            if (formExec == null || !formExec.Created)
            {
                formExec = new FormExec();
            }

            FormShow(formExec);
        }

        public static void FormRsvs_Show()
        {
            if (formRsvs == null || !formRsvs.Created)
            {
                formRsvs = new FormRsvs();
            }

            FormShow(formRsvs);
        }

        public static void FormRsvs_Show(int crit_date, string code1, string name1, string code2 = "", string name2 = "")
        {
            if (formRsvs == null || !formRsvs.Created)
            {
                formRsvs = new FormRsvs();
            }

            FormShow(formRsvs);

            formRsvs.RsvShow(crit_date, code1, name1, code2, name2);
        }

        public static void FormPat_Show(PatBase p, bool read_only = false)
        {
            if (formPat == null || !formPat.Created)
            {
                formPat = new FormPat(read_only);
            }

            formPat.PatSet(p);
            FormShow(formPat);
        }
/*
        public static void FormPat_Show(bool read_only = false)
        {
            if (formPat == null || !formPat.Created)
            {
                formPat = new FormPat(read_only);
            }

            FormShow(formPat);
        }
*/
        public static void FormOrderKaikei_Show(FormOrderKaikei.Mode mode)
        {
            if (formOrderKaikei == null || !formOrderKaikei.Created)
            {
                formOrderKaikei = new FormOrderKaikei(mode);
            }

            FormShow(formOrderKaikei);
            formOrderKaikei.Mode1 = mode;
            formOrderKaikei.PatShow();
        }

        public static void FormOrderKaikei_Show(PatBase p, FormOrderKaikei.Mode mode, string date1 = "", string date2 = "", int seq = 1, string dept = "", string doctor = "", string ins = "")
        {
            if (formOrderKaikei == null || !formOrderKaikei.Created)
            {
                formOrderKaikei = new FormOrderKaikei();
            }

            FormShow(formOrderKaikei);
            formOrderKaikei.PatSet(p);
            formOrderKaikei.Mode1 = mode;

			if (DateTimeAgent.IsDate(date1))
			{
				formOrderKaikei.Date1 = date1;
			}

			if (DateTimeAgent.IsDate(date2))
			{
				formOrderKaikei.Date2 = date2;
			}

            formOrderKaikei.SEQ = seq;
            formOrderKaikei.DeptDoctorSet(dept, doctor);
            formOrderKaikei.InsSet(ins);
            formOrderKaikei.PatShow();
        }

        public static bool IsFormOrderKaikeiOpen
        {
            get
            {
                bool result = false;

                if (formOrderKaikei != null && formOrderKaikei.Created)
                {
                    result = true;
                }

                return result;
            }
        }

        public static void FormPath_Show()
        {
            if (formPath == null || !formPath.Created)
            {
                formPath = new FormPath();
            }

            FormShow(formPath);
        }

        public static void FormDPCDiag_Show(PatBase p)
        {
            if (formDPCDiag == null || !formDPCDiag.Created)
            {
                formDPCDiag = new FormDiagDPC();
            }

            formDPCDiag.PatSet(p);
            FormShow(formDPCDiag);
        }

        public static bool IsFormDPCData1Open
        {
            get
            {
                bool result = false;

                if (formDPCData1 != null && formDPCData1.Created)
                {
                    result = true;
                }

                return result;
            }
        }

        public static void FormDPCData1_Show(PatBase p, string in_date)
        {
            if (formDPCData1 == null || !formDPCData1.Created)
            {
                formDPCData1 = new FormDPCData1();
            }

            formDPCData1.PatInDateSet(p, in_date);
            FormShow(formDPCData1);
        }

        public static void FormDPCCheck1_Show()
        {
            if (formDPCCheck1 == null || !formDPCCheck1.Created)
            {
                formDPCCheck1 = new FormDPCCheck1();
            }

            FormShow(formDPCCheck1);
        }

        public static void FormDPCHeaderList_Show(PatBase p)
        {
            if (formDPCHeaderList == null || !formDPCHeaderList.Created)
            {
                formDPCHeaderList = new FormDPCList2();
            }

            formDPCHeaderList.PatSet(p);
            FormShow(formDPCHeaderList);
        }

        public static void FormDPCList_Show()
        {
            if (formDPCList == null || !formDPCList.Created)
            {
                formDPCList = new FormDPCList();
            }

            FormShow(formDPCList);
        }


/*
        public static void FormKarteTemplate1_Show(CtrlSoapWrite1 soap_write)
        {
            if (formKarteTemplate1 == null || !formKarteTemplate1.Created)
            {
                formKarteTemplate1 = new FormKarteTemplate1();
                formKarteTemplate1.Init(soap_write);
            }

            FormShow(formKarteTemplate1);
        }
*/
        public static void FormKarteMessage1_Show()
        {
            if (formKarteMessage1 == null || !formKarteMessage1.Created)
            {
                formKarteMessage1 = new FormKarteMessage1();
            }

            FormShow(formKarteMessage1);
        }

        public static void FormAnesRecord_Show(PatBase p)
        {
            if (formAnesRecord == null || !formAnesRecord.Created)
            {
                formAnesRecord = new FormAnesRecord();
            }

            formAnesRecord.PatSet(p);
            FormShow(formAnesRecord);
        }

        public static void FormDiagSearch_Show()
        {
            if (formDiagSearch == null || !formDiagSearch.Created)
            {
                formDiagSearch = new FormDiagSearch();
            }

            FormShow(formDiagSearch);
        }

        public static void FormBillPDFList_Show()
        {
            if (formBillPDFList == null || !formBillPDFList.Created)
            {
                formBillPDFList = new FormBillPDFList();
            }

            FormShow(formBillPDFList);
        }
    }
}
