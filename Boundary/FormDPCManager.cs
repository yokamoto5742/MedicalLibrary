using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Boundary;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class FormDPCManager : StdForm1
    {
        DataSet dSet = new DataSet();

        public FormDPCManager()
        {
            InitializeComponent();

            DataTable table = dSet.Tables.Add("ICD");
            table.Columns.Add("病名");
            table.Columns.Add("ICD名称");
            table.Columns.Add("ICD");
            table.Columns.Add("MDC");

            table = dSet.Tables.Add("DPCMaster");
            table.Columns.Add("世代区分");
            table.Columns.Add("番号");
            table.Columns.Add("診断群分類番号");
            table.Columns.Add("傷病名");
            table.Columns.Add("JCS");
            table.Columns.Add("手術");
            table.Columns.Add("手術名");
            table.Columns.Add("手術処置1");
            table.Columns.Add("手術処置2");
            table.Columns.Add("副傷病");
            table.Columns.Add("重症度");
            table.Columns.Add("Kコード");
            table.Columns.Add("ICDコード");
            table.Columns.Add("入院I", typeof(int));
            table.Columns.Add("入院II", typeof(int));
            table.Columns.Add("入院III", typeof(int));
            table.Columns.Add("点数I", typeof(int));
            table.Columns.Add("点数II", typeof(int));
            table.Columns.Add("点数III", typeof(int));
            table.Columns.Add("基本料", typeof(int));
            table.Columns.Add("変更区分");
            table.Columns.Add("開始日");
            table.Columns.Add("終了日");
            table.Columns.Add("更新日");
            table.Columns.Add("報酬点数", typeof(int));
            table.Columns.Add("係数有", typeof(int));

            table = dSet.Tables.Add("DPCOpe");
            table.Columns.Add("世代区分");
            table.Columns.Add("MDCコード");
            table.Columns.Add("分類コード");
            table.Columns.Add("値");
            table.Columns.Add("手術フラグ");
            table.Columns.Add("年齢出生時体重別の値");
            table.Columns.Add("対応コード");
            table.Columns.Add("手術1点数表名称");
            table.Columns.Add("手術1Kコード");
            table.Columns.Add("手術2点数表名称");
            table.Columns.Add("手術2Kコード");
            table.Columns.Add("手術3点数表名称");
            table.Columns.Add("手術3Kコード");
            table.Columns.Add("手術4点数表名称");
            table.Columns.Add("手術4Kコード");
            table.Columns.Add("手術5点数表名称");
            table.Columns.Add("手術5Kコード");
            table.Columns.Add("変更区分");
            table.Columns.Add("開始日");
            table.Columns.Add("終了日");
            table.Columns.Add("更新日");

            table = dSet.Tables.Add("DPCOpe1");
            table.Columns.Add("世代区分");
            table.Columns.Add("MDCコード");
            table.Columns.Add("分類コード");
            table.Columns.Add("対応コード");
            table.Columns.Add("処置1フラグ");
            table.Columns.Add("手術との組み合わせ条件");
            table.Columns.Add("処置等1名称");
            table.Columns.Add("処置等1コード");
            table.Columns.Add("処置等2名称");
            table.Columns.Add("処置等2コード");
            table.Columns.Add("変更区分");
            table.Columns.Add("開始日");
            table.Columns.Add("終了日");
            table.Columns.Add("更新日");

            table = dSet.Tables.Add("DPCOpe2");
            table.Columns.Add("世代区分");
            table.Columns.Add("MDCコード");
            table.Columns.Add("分類コード");
            table.Columns.Add("対応コード");
            table.Columns.Add("処置2フラグ");
            table.Columns.Add("処置等1名称");
            table.Columns.Add("処置等1コード");
            table.Columns.Add("処置等2名称");
            table.Columns.Add("処置等2コード");
            table.Columns.Add("変更区分");
            table.Columns.Add("開始日");
            table.Columns.Add("終了日");
            table.Columns.Add("更新日");

            table = dSet.Tables.Add("DPCDiag2");
            table.Columns.Add("世代区分");
            table.Columns.Add("MDCコード");
            table.Columns.Add("分類コード");
            table.Columns.Add("対応コード");
            table.Columns.Add("副傷病フラグ");
            table.Columns.Add("副傷病名");
            table.Columns.Add("ICDコード");
            table.Columns.Add("変更区分");
            table.Columns.Add("開始日");
            table.Columns.Add("終了日");
            table.Columns.Add("更新日");

            this.JCSBox1.Items.Add("");
            this.JCSBox1.Items.Add("0～9");
            this.JCSBox1.Items.Add("10以上");

            this.OpeBox1.Items.Add("");
            this.OpeBox1.Items.Add("あり");
            this.OpeBox1.Items.Add("なし");

            this.DaysBox1.Text = "0";
            this.DPCBox1.Text = LibSettings.Current.DPCValueFloat.ToString();
        }

        public void MDCSet(string mdc)
        {
            this.MDCBox1.Text = mdc;
        }

        public void DPCSet(string dpc)
        {
            float f = 1.0F;

            if (float.TryParse(dpc, out f))
            {
                this.DPCBox1.Text = dpc;
            }
            else
            {
                this.DPCBox1.Text = "1.0";
            }
        }

        void ListShow0()
        {
            if (this.DiagFindBox1.Text.Length == 0 && this.ICDFindBox1.Text.Length == 0)
            {
                return;
            }

            List<DPCICD> list = DPCICD.GetListByDiagNameOrICD(this.DiagFindBox1.Text, this.ICDFindBox1.Text);

            DataTable table = dSet.Tables["ICD"];
            table.Rows.Clear();

            foreach (DPCICD obj in list)
            {
                DataRow r = table.NewRow();

                r["病名"] = obj.DiagName;
                r["ICD名称"] = obj.ICDName;
                r["ICD"] = obj.ICDCode;
                r["MDC"] = obj.MDC + obj.Group;

                table.Rows.Add(r);
            }

            this.ListFormat0();
        }

        void ListFormat0()
        {
            DataView view = new DataView(dSet.Tables["ICD"]);

            ListView0.DataSource = view;

            ListView0.Columns["病名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView0.Columns["病名"].Width = 170;

            ListView0.Columns["ICD名称"].Visible = false;

            ListView0.Columns["ICD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView0.Columns["ICD"].Width = 40;

            ListView0.Columns["MDC"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView0.Columns["MDC"].Width = 45;

            if (this.Font.Size > 9)
            {
                foreach (DataGridViewColumn c in ListView0.Columns)
                {
                    c.Width = (int)(c.Width * (float)(this.Font.Size / 9));
                }
            }
        }

        void ListShow1()
        {
            List<DPCMaster> list = new List<DPCMaster>();
            
            if (this.MDCBox1.Text.Length == 6)
            {
                list = DPCMaster.GetListByMDC(this.MDCBox1.Text);
            }

            DataTable table = dSet.Tables["DPCMaster"];
            table.Rows.Clear();

            foreach (DPCMaster obj in list)
            {
                DataRow r = table.NewRow();

                r["世代区分"] = obj.GenSEQ;
                r["番号"] = obj.SEQ;
                r["診断群分類番号"] = obj.Code;
                r["傷病名"] = obj.DiagName;
                r["JCS"] = obj.JCSFlg;
                r["手術"] = obj.OpeFlg;
                r["手術名"] = obj.OpeName;
                r["手術処置1"] = obj.Ope1;
                r["手術処置2"] = obj.Ope2;
                r["副傷病"] = obj.Diag2;
                r["重症度"] = obj.Heavy;
                r["Kコード"] = obj.KCodeString(" ");
                r["ICDコード"] = obj.ICDString(" ");
                r["入院I"] = obj.Day1;
                r["入院II"] = obj.Day2;
                r["入院III"] = obj.Day3;
                r["点数I"] = obj.Point1;
                r["点数II"] = obj.Point2;
                r["点数III"] = obj.Point3;
                r["基本料"] = 1591;
                r["変更区分"] = obj.Kind1;
                r["開始日"] = obj.Date1;
                r["終了日"] = obj.Date2;
                r["更新日"] = obj.Date3;

                table.Rows.Add(r);
            }

            this.ListFormat1();
        }

        void ListFormat1()
        {
            DataView view = new DataView(dSet.Tables["DPCMaster"]);

            List<string> filters = new List<string>();

            if (this.JCSBox1.Text.Equals("0～9"))
            {
                filters.Add("(JCS = '0')");
            }
            else if (this.JCSBox1.Text.Equals("10以上"))
            {
                filters.Add("(JCS = '1')");
            }

            if (this.OpeBox1.Text.Equals("あり"))
            {
                filters.Add("(手術 <> 'xx' and 手術 <> '99')");
            }
            else if (this.OpeBox1.Text.Equals("なし"))
            {
                filters.Add("(手術 = 'xx' or 手術 = '99')");
            }

            if (this.KCodeBox1.Text.Length > 0)
            {
                filters.Add("(Kコード like '%" + this.KCodeBox1.Text + "%')");
            }

            if (this.ICDBox2.Text.Length > 0)
            {
                filters.Add("(ICDコード like '%" + this.ICDBox2.Text + "%')");
            }

            view.RowFilter = AppString.ConcatList(filters, " and ");

            ListView1.DataSource = view;

            ListView1.Columns["世代区分"].Visible = false;
            ListView1.Columns["番号"].Visible = false;

            ListView1.Columns["診断群分類番号"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView1.Columns["診断群分類番号"].Width = 95;

            ListView1.Columns["傷病名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView1.Columns["傷病名"].Width = 150;

            ListView1.Columns["JCS"].Visible = false;
            ListView1.Columns["手術"].Visible = false;

            ListView1.Columns["手術名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView1.Columns["手術名"].Width = 110;

            ListView1.Columns["手術処置1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView1.Columns["手術処置1"].Width = 45;

            ListView1.Columns["手術処置2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView1.Columns["手術処置2"].Width = 45;

            ListView1.Columns["副傷病"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView1.Columns["副傷病"].Width = 45;

            ListView1.Columns["重症度"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView1.Columns["重症度"].Width = 35;

            ListView1.Columns["Kコード"].Visible = false;
            ListView1.Columns["ICDコード"].Visible = false;

            ListView1.Columns["入院I"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            ListView1.Columns["入院I"].Width = 35;

            ListView1.Columns["入院II"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            ListView1.Columns["入院II"].Width = 35;

            ListView1.Columns["入院III"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            ListView1.Columns["入院III"].Width = 35;

            ListView1.Columns["点数I"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            ListView1.Columns["点数I"].Width = 50;
            ListView1.Columns["点数I"].DefaultCellStyle.Format = "#,0";

            ListView1.Columns["点数II"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            ListView1.Columns["点数II"].Width = 50;
            ListView1.Columns["点数II"].DefaultCellStyle.Format = "#,0";

            ListView1.Columns["点数III"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            ListView1.Columns["点数III"].Width = 50;
            ListView1.Columns["点数III"].DefaultCellStyle.Format = "#,0";

            ListView1.Columns["基本料"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            ListView1.Columns["基本料"].Width = 50;
            ListView1.Columns["基本料"].DefaultCellStyle.Format = "#,0";

            ListView1.Columns["変更区分"].Visible = false;
            ListView1.Columns["開始日"].Visible = false;
            ListView1.Columns["終了日"].Visible = false;
            ListView1.Columns["更新日"].Visible = false;

            ListView1.Columns["報酬点数"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            ListView1.Columns["報酬点数"].Width = 60;
            ListView1.Columns["報酬点数"].DefaultCellStyle.Format = "#,0";

            ListView1.Columns["係数有"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            ListView1.Columns["係数有"].Width = 60;
            ListView1.Columns["係数有"].DefaultCellStyle.Format = "#,0";

            CountLabel1.Text = ListView1.Rows.Count + "件";

            foreach (DataGridViewColumn c in ListView1.Columns)
            {
                c.SortMode = DataGridViewColumnSortMode.NotSortable;

                if (this.Font.Size > 9)
                {
                    c.Width = (int)(c.Width * (float)(this.Font.Size / 9));
                }
            }

            if (this.DaysBox1.Text.Length > 0)
            {
                int days = 0;
                int.TryParse(this.DaysBox1.Text, out days);

                // 入院基本料の報酬点数
                int i0 = 0;

                // DPC の報酬点数
                int i1 = 0;
                int i2 = 0;
                int i3 = 0;

                int day1 = 0;
                int day2 = 0;
                int day3 = 0;

                int point1 = 0;
                int point2 = 0;
                int point3 = 0;

                // 入院基本料の日別点数
                int point0 = 1591;

                foreach (DataGridViewRow r in ListView1.Rows)
                {
                    i0 = 0;
                    i1 = 0;
                    i2 = 0;
                    i3 = 0;

                    day1 = 0;
                    day2 = 0;
                    day3 = 0;

                    point1 = 0;
                    point2 = 0;
                    point3 = 0;

                    int.TryParse(r.Cells["入院I"].Value.ToString(), out day1);
                    int.TryParse(r.Cells["入院II"].Value.ToString(), out day2);
                    int.TryParse(r.Cells["入院III"].Value.ToString(), out day3);

                    int.TryParse(r.Cells["点数I"].Value.ToString(), out point1);
                    int.TryParse(r.Cells["点数II"].Value.ToString(), out point2);
                    int.TryParse(r.Cells["点数III"].Value.ToString(), out point3);


                    if ((point1 + point2 + point3) == 0)
                    {
                        i0 = days * point0;

                        r.DefaultCellStyle.BackColor = Color.LightPink;
                    }
                    else
                    {
                        if (days > day3)
                        {
                            i0 = (days - day3) * point0;
                            i3 = (day3 - day2) * point3;
                            i2 = (day2 - day1) * point2;
                            i1 = day1 * point1;

                            r.Cells["報酬点数"].Style.BackColor = Color.LightPink;
                            r.Cells["係数有"].Style.BackColor = Color.LightPink;
                        }
                        else if (days > day2)
                        {
                            i3 = (days - day2) * point3;
                            i2 = (day2 - day1) * point2;
                            i1 = day1 * point1;
                        }
                        else if (days > day1)
                        {
                            i2 = (days - day1) * point2;
                            i1 = day1 * point1;
                        }
                        else
                        {
                            i1 = days * point1;
                        }
                    }

                    r.Cells["報酬点数"].Value = i0 + i1 + i2 + i3;

                    float f = 1.0F;
                    float.TryParse(this.DPCBox1.Text, out f);
                    r.Cells["係数有"].Value = (int)((i0 + i1 + i2 + i3) * f);
                }
            }
        }

        private void MDCBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.MDCBox1.Text.Length < 6)
            {
                return;
            }

            this.ListShow1();
        }

        private void DaysBox1_TextChanged(object sender, EventArgs e)
        {
            this.ListFormat1();
        }

        private void DPCBox1_TextChanged(object sender, EventArgs e)
        {
            this.ListFormat1();
        }

        private void JCSBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ListFormat1();
        }

        private void OpeBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ListFormat1();
        }

        private void KCodeBox1_TextChanged(object sender, EventArgs e)
        {
            this.ListFormat1();
        }

        private void ICDBox2_TextChanged(object sender, EventArgs e)
        {
            this.ListFormat1();
        }

        void ListShow2(string mdc, string group, string ope, string ope1, string ope2, bool diag2)
        {
            List<DPCOpe> ope_list = DPCOpe.GetList(mdc, group, ope);

            DataTable table = dSet.Tables["DPCOpe"];
            table.Rows.Clear();

            foreach (DPCOpe obj in ope_list)
            {
                DataRow r = table.NewRow();

                r["世代区分"] = obj.GenSEQ;
                r["MDCコード"] = obj.MDC;
                r["分類コード"] = obj.Group;
                r["値"] = obj.Value1;
                r["手術フラグ"] = obj.OpeFlg;
                r["年齢出生時体重別の値"] = obj.Value2;
                r["対応コード"] = obj.Code;
                r["手術1点数表名称"] = obj.OpeName1;
                r["手術1Kコード"] = obj.OpeCode1;
                r["手術2点数表名称"] = obj.OpeName2;
                r["手術2Kコード"] = obj.OpeCode2;
                r["手術3点数表名称"] = obj.OpeName3;
                r["手術3Kコード"] = obj.OpeCode3;
                r["手術4点数表名称"] = obj.OpeName4;
                r["手術4Kコード"] = obj.OpeCode4;
                r["手術5点数表名称"] = obj.OpeName5;
                r["手術5Kコード"] = obj.OpeCode5;
                r["変更区分"] = obj.Kind1;
                r["開始日"] = obj.Date1;
                r["終了日"] = obj.Date2;
                r["更新日"] = obj.Date3;

                table.Rows.Add(r);
            }

            List<DPCOpe1> ope_list1 = DPCOpe1.GetList(mdc, group, ope1);

            table = dSet.Tables["DPCOpe1"];
            table.Rows.Clear();

            foreach (DPCOpe1 obj in ope_list1)
            {
                DataRow r = table.NewRow();

                r["世代区分"] = obj.GenSEQ;
                r["MDCコード"] = obj.MDC;
                r["分類コード"] = obj.Group;
                r["対応コード"] = obj.Code;
                r["処置1フラグ"] = obj.OpeFlg1;
                r["手術との組み合わせ条件"] = obj.Cont1;
                r["処置等1名称"] = obj.OpeName1;
                r["処置等1コード"] = obj.OpeCode1;
                r["処置等2名称"] = obj.OpeName2;
                r["処置等2コード"] = obj.OpeCode2;
                r["変更区分"] = obj.Kind1;
                r["開始日"] = obj.Date1;
                r["終了日"] = obj.Date2;
                r["更新日"] = obj.Date3;

                table.Rows.Add(r);
            }

            List<DPCOpe2> ope_list2 = DPCOpe2.GetList(mdc, group, ope2);

            table = dSet.Tables["DPCOpe2"];
            table.Rows.Clear();

            foreach (DPCOpe2 obj in ope_list2)
            {
                DataRow r = table.NewRow();

                r["世代区分"] = obj.GenSEQ;
                r["MDCコード"] = obj.MDC;
                r["分類コード"] = obj.Group;
                r["対応コード"] = obj.Code;
                r["処置2フラグ"] = obj.OpeFlg2;
                r["処置等1名称"] = obj.OpeName1;
                r["処置等1コード"] = obj.OpeCode1;
                r["処置等2名称"] = obj.OpeName2;
                r["処置等2コード"] = obj.OpeCode2;
                r["変更区分"] = obj.Kind1;
                r["開始日"] = obj.Date1;
                r["終了日"] = obj.Date2;
                r["更新日"] = obj.Date3;

                table.Rows.Add(r);
            }

            List<DPCDiag2> diag_list2 = new List<DPCDiag2>();

            // 副傷病ありの場合のみデータを取得・表示する。
            if (diag2)
            {
                if (ope.Equals("xx") || ope.Equals("99"))
                {
                    // 手術なし
                    diag_list2 = DPCDiag2.GetList(mdc, group, false);
                }
                else
                {
                    // 手術あり
                    diag_list2 = DPCDiag2.GetList(mdc, group, true);
                }
            }

            table = dSet.Tables["DPCDiag2"];
            table.Rows.Clear();

            foreach (DPCDiag2 obj in diag_list2)
            {
                DataRow r = table.NewRow();

                r["世代区分"] = obj.GenSEQ;
                r["MDCコード"] = obj.MDC;
                r["分類コード"] = obj.Group;
                r["対応コード"] = obj.Code;
                r["副傷病フラグ"] = obj.DiagFlg2;
                r["副傷病名"] = obj.DiagName2;
                r["ICDコード"] = obj.ICD;
                r["変更区分"] = obj.Kind1;
                r["開始日"] = obj.Date1;
                r["終了日"] = obj.Date2;
                r["更新日"] = obj.Date3;

                table.Rows.Add(r);
            }

            this.ListFormat2();
            this.ListFormat3();
            this.ListFormat4();
            this.ListFormat5();
        }

        void ListFormat2()
        {
            DataView view = new DataView(dSet.Tables["DPCOpe"]);

            List<string> filters = new List<string>();

            if (this.OpeNameFilterBox1.Text.Length > 0)
            {
                filters.Add("(手術1点数表名称 like '%" + this.OpeNameFilterBox1.Text + "%')");
                filters.Add("(手術2点数表名称 like '%" + this.OpeNameFilterBox1.Text + "%')");
                filters.Add("(手術3点数表名称 like '%" + this.OpeNameFilterBox1.Text + "%')");
                filters.Add("(手術4点数表名称 like '%" + this.OpeNameFilterBox1.Text + "%')");
                filters.Add("(手術5点数表名称 like '%" + this.OpeNameFilterBox1.Text + "%')");

                filters.Add("(手術1Kコード like '%" + this.OpeNameFilterBox1.Text + "%')");
                filters.Add("(手術2Kコード like '%" + this.OpeNameFilterBox1.Text + "%')");
                filters.Add("(手術3Kコード like '%" + this.OpeNameFilterBox1.Text + "%')");
                filters.Add("(手術4Kコード like '%" + this.OpeNameFilterBox1.Text + "%')");
                filters.Add("(手術5Kコード like '%" + this.OpeNameFilterBox1.Text + "%')");
            }

            view.RowFilter = AppString.ConcatList(filters, " or ");

            ListView2.DataSource = view;

            ListView2.Columns["世代区分"].Visible = false;
            ListView2.Columns["MDCコード"].Visible = false;
            ListView2.Columns["分類コード"].Visible = false;
            ListView2.Columns["値"].Visible = false;
            ListView2.Columns["手術フラグ"].Visible = false;
            ListView2.Columns["年齢出生時体重別の値"].Visible = false;
            ListView2.Columns["対応コード"].Visible = false;

            ListView2.Columns["手術1点数表名称"].HeaderText = "手術1名称";
            ListView2.Columns["手術1点数表名称"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView2.Columns["手術1点数表名称"].Width = 200;

            ListView2.Columns["手術1Kコード"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView2.Columns["手術1Kコード"].Width = 50;

            ListView2.Columns["手術2点数表名称"].HeaderText = "手術2名称";
            ListView2.Columns["手術2点数表名称"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView2.Columns["手術2点数表名称"].Width = 120;

            ListView2.Columns["手術2Kコード"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView2.Columns["手術2Kコード"].Width = 50;

            ListView2.Columns["手術3点数表名称"].HeaderText = "手術3名称";
            ListView2.Columns["手術3点数表名称"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView2.Columns["手術3点数表名称"].Width = 100;

            ListView2.Columns["手術3Kコード"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView2.Columns["手術3Kコード"].Width = 50;

            ListView2.Columns["手術4点数表名称"].HeaderText = "手術4名称";
            ListView2.Columns["手術4点数表名称"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView2.Columns["手術4点数表名称"].Width = 100;

            ListView2.Columns["手術4Kコード"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView2.Columns["手術4Kコード"].Width = 50;

            ListView2.Columns["手術5点数表名称"].HeaderText = "手術5名称";
            ListView2.Columns["手術5点数表名称"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView2.Columns["手術5点数表名称"].Width = 100;

            ListView2.Columns["手術5Kコード"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView2.Columns["手術5Kコード"].Width = 50;

            ListView2.Columns["変更区分"].Visible = false;
            ListView2.Columns["開始日"].Visible = false;
            ListView2.Columns["終了日"].Visible = false;
            ListView2.Columns["更新日"].Visible = false;

            CountLabel2.Text = ListView2.Rows.Count + "件";
        }

        void ListFormat3()
        {
            DataView view = new DataView(dSet.Tables["DPCOpe1"]);

            List<string> filters = new List<string>();

            if (this.OpeFilterBox1.Text.Length > 0)
            {
                filters.Add("(処置等1名称 like '%" + this.OpeFilterBox1.Text + "%')");
                filters.Add("(処置等2名称 like '%" + this.OpeFilterBox1.Text + "%')");

                filters.Add("(処置等1コード like '%" + this.OpeFilterBox1.Text + "%')");
                filters.Add("(処置等2コード like '%" + this.OpeFilterBox1.Text + "%')");
            }

            view.RowFilter = AppString.ConcatList(filters, " or ");

            ListView3.DataSource = view;

            ListView3.Columns["世代区分"].Visible = false;
            ListView3.Columns["MDCコード"].Visible = false;
            ListView3.Columns["分類コード"].Visible = false;
            ListView3.Columns["対応コード"].Visible = false;
            ListView3.Columns["処置1フラグ"].Visible = false;
            ListView3.Columns["手術との組み合わせ条件"].Visible = false;

            ListView3.Columns["処置等1名称"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView3.Columns["処置等1名称"].Width = 180;

            ListView3.Columns["処置等1コード"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView3.Columns["処置等1コード"].Width = 50;

            ListView3.Columns["処置等2名称"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView3.Columns["処置等2名称"].Width = 100;

            ListView3.Columns["処置等2コード"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView3.Columns["処置等2コード"].Width = 50;

            ListView3.Columns["変更区分"].Visible = false;
            ListView3.Columns["開始日"].Visible = false;
            ListView3.Columns["終了日"].Visible = false;
            ListView3.Columns["更新日"].Visible = false;

            CountLabel3.Text = ListView3.Rows.Count + "件";
        }

        void ListFormat4()
        {
            DataView view = new DataView(dSet.Tables["DPCOpe2"]);

            List<string> filters = new List<string>();

            if (this.OpeFilterBox2.Text.Length > 0)
            {
                filters.Add("(処置等1名称 like '%" + this.OpeFilterBox2.Text + "%')");
                filters.Add("(処置等2名称 like '%" + this.OpeFilterBox2.Text + "%')");

                filters.Add("(処置等1コード like '%" + this.OpeFilterBox2.Text + "%')");
                filters.Add("(処置等2コード like '%" + this.OpeFilterBox2.Text + "%')");
            }

            view.RowFilter = AppString.ConcatList(filters, " or ");

            ListView4.DataSource = view;

            ListView4.Columns["世代区分"].Visible = false;
            ListView4.Columns["MDCコード"].Visible = false;
            ListView4.Columns["分類コード"].Visible = false;
            ListView4.Columns["対応コード"].Visible = false;
            ListView4.Columns["処置2フラグ"].Visible = false;

            ListView4.Columns["処置等1名称"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView4.Columns["処置等1名称"].Width = 180;

            ListView4.Columns["処置等1コード"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView4.Columns["処置等1コード"].Width = 50;

            ListView4.Columns["処置等2名称"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView4.Columns["処置等2名称"].Width = 100;

            ListView4.Columns["処置等2コード"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView4.Columns["処置等2コード"].Width = 50;

            ListView4.Columns["変更区分"].Visible = false;
            ListView4.Columns["開始日"].Visible = false;
            ListView4.Columns["終了日"].Visible = false;
            ListView4.Columns["更新日"].Visible = false;

            CountLabel4.Text = ListView4.Rows.Count + "件";
        }

        void ListFormat5()
        {
            DataView view = new DataView(dSet.Tables["DPCDiag2"]);

            List<string> filters = new List<string>();

            if (this.DiagFilterBox2.Text.Length > 0)
            {
                filters.Add("(副傷病名 like '%" + this.DiagFilterBox2.Text + "%')");
                filters.Add("(ICDコード like '%" + this.DiagFilterBox2.Text + "%')");
            }

            view.RowFilter = AppString.ConcatList(filters, " or ");

            ListView5.DataSource = view;

            ListView5.Columns["世代区分"].Visible = false;
            ListView5.Columns["MDCコード"].Visible = false;
            ListView5.Columns["分類コード"].Visible = false;
            ListView5.Columns["対応コード"].Visible = false;
            ListView5.Columns["副傷病フラグ"].Visible = false;

            ListView5.Columns["副傷病名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView5.Columns["副傷病名"].Width = 240;

            ListView5.Columns["ICDコード"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ListView5.Columns["ICDコード"].Width = 50;

            ListView5.Columns["変更区分"].Visible = false;
            ListView5.Columns["開始日"].Visible = false;
            ListView5.Columns["終了日"].Visible = false;
            ListView5.Columns["更新日"].Visible = false;

            CountLabel5.Text = ListView5.Rows.Count + "件";
        }

        private void ListView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            string code = ListView1.Rows[e.RowIndex].Cells["診断群分類番号"].Value.ToString();

            if (code.Length < 14)
            {
                return;
            }

            string mdc = code.Substring(0, 2);
            string group = code.Substring(2, 4);
            string ope = code.Substring(8, 2);
            string ope1 = code.Substring(10, 1);
            string ope2 = code.Substring(11, 1);
            string diag2 = code.Substring(12, 1);

            if (diag2.Equals("1"))
            {
                // 副傷病あり
                this.ListShow2(mdc, group, ope, ope1, ope2, true);
            }
            else
            {
                // 副傷病なし
                this.ListShow2(mdc, group, ope, ope1, ope2, false);
            }
        }

        private void OpeNameFilterBox1_TextChanged(object sender, EventArgs e)
        {
            this.ListFormat2();
        }

        private void OpeFilterBox1_TextChanged(object sender, EventArgs e)
        {
            this.ListFormat3();
        }

        private void OpeFilterBox2_TextChanged(object sender, EventArgs e)
        {
            this.ListFormat4();
        }

        private void DiagFilterBox2_TextChanged(object sender, EventArgs e)
        {
            this.ListFormat5();
        }

        private void ListView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            string code = ListView1.Rows[e.RowIndex].Cells["診断群分類番号"].Value.ToString();

            Clipboard.SetText(code);
        }

        private void DiagFindBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ListShow0();
            }
        }

        private void ICDFindBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ListShow0();
            }
        }

        private void ListView0_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            string mdc = ListView0.Rows[e.RowIndex].Cells["MDC"].Value.ToString();

            if (mdc.Length > 0)
            {
                this.MDCBox1.Text = mdc;
            }
        }
    }
}
